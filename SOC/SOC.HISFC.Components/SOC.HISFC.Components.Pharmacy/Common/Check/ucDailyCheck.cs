using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    /// <summary>
    /// [功能描述: 药品日盘点管理类]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2012-09]<br></br>
    /// 说明：
    /// 1、属性设置里的sql必须在数据有值
    /// 2、本类支持存储过程日结，或者简单sql备份库存方式
    /// 3、本类支持本地化的日结，包括建表、获取表数据等
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public partial class ucDailyCheck : FS.SOC.HISFC.Components.PharmacyCommon.BaseReport
    {
        public ucDailyCheck()
        {
            InitializeComponent();

            this.PriveClassTwos = "0320+M1,0320+Z1";
            this.MainTitle = "药品交班表";
            this.LeftAdditionTitle = "交班时间";
            this.RightAdditionTitle = "交班人";
            this.MidAdditionTitle = "交班科室";
            this.IsDeptAsCondition = true;
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select distinct d.code,
                                            d.name,
                                            d.mark,
                                            d.spell_code,
                                            d.wb_code,
                                            d.input_code
                              from pha_com_stockinfo s, com_dictionary d
                             where s.dailtycheck_flag = '1'
                               and d.type = 'DRUGQUALITY'
                               and d.code = s.drug_quality";

            this.SQLIndexs = "SOC.Pharmacy.Check.DailyCheck.QueryDataByTime";

            this.QueryDataWhenInit = false;

            this.neuTreeView1.AfterSelect += new TreeViewEventHandler(neuTreeView1_AfterSelect);
            this.fpSpread1_Sheet1.Rows.Default.Height = 30F;
            this.fpSpread1_Sheet1.Rows.Default.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
        }

        #region 属性
        private string curGetListSQLIndex = "SOC.Pharmacy.Check.DailyCheck.QueryList";

        [Category("Query查询设置"),Description("获取表单列表的SQL")]
        public string GetListSQL
        {
            get { return curGetListSQLIndex; }
            set { curGetListSQLIndex = value; }
        }

        private string curFStoreSQLIndex = "SOC.Pharmacy.Check.DailyCheck.FStore";

        [Category("A封账"), Description("封账SQL")]
        public string FStoreSQLIndex
        {
            get { return curFStoreSQLIndex; }
            set { curFStoreSQLIndex = value; }
        }

        private bool isExecuteProcedure = true;

        [Category("A封账"), Description("封账是否采用存储过程，封账SQL将被忽略")]
        public bool IsExecuteProcedure
        {
            get { return isExecuteProcedure; }
            set { isExecuteProcedure = value; }
        }

        #endregion

        #region 函数

        /// <summary>
        /// 显示表列表
        /// 属性设置sql在第一字段返回封账时间，第二个字段返回封账人即可
        /// sql的参数是科室，开始时间，结束时间
        /// </summary>
        /// <returns></returns>
        private int ShowList()
        {
            if (string.IsNullOrEmpty(this.curGetListSQLIndex))
            {
                Function.ShowMessage("系统没有设置获取表单列表的SQL，请咨询开发商！", MessageBoxIcon.Information);
                return 0;
            }
            if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
            {
                Function.ShowMessage("请您选择科室！", MessageBoxIcon.Information);
                this.cmbDept.Select();
                this.cmbDept.Focus();
                return 0;
            }

            this.neuTreeView1.Nodes[0].Nodes.Clear();

            DataSet ds = new DataSet();
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            if (dbMgr.ExecQuery(this.curGetListSQLIndex, ref ds, this.cmbDept.Tag.ToString(), this.dtStart.Value.ToString(), this.dtEnd.Value.ToString()) == -1)
            {
                Function.ShowMessage("获取表单列表发生错误，请与系统管理员联系并报告错误：" + dbMgr.Err, MessageBoxIcon.Information);
                return -1;
            }

            if (ds == null || ds.Tables.Count == 0)
            {
                Function.ShowMessage("获取表单列表发生错误，请与系统管理员联系并报告错误：" + dbMgr.Err, MessageBoxIcon.Information);
                return -1;
            }
            try
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    TreeNode node = new TreeNode();

                    FS.HISFC.Models.Pharmacy.Check check = new FS.HISFC.Models.Pharmacy.Check();
                    check.StockDept.ID = this.cmbDept.Tag.ToString();
                    check.FOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(row[0]);
                    check.FOper.ID = row[1].ToString();

                    node.Text = check.FOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss") + " " + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(check.FOper.ID);
                    node.Tag = check;
                    this.neuTreeView1.Nodes[0].Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                Function.ShowMessage("根据查询的表单数据显示列表发生错误，请与系统管理员联系并报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }
            this.neuTreeView1.ExpandAll();

            return 1;
        }

        /// <summary>
        /// 封账
        /// 封账支持存储过程或者简单sql备份库存方式，默认是后者
        /// </summary>
        /// <returns></returns>
        private int FStore()
        {
            if (string.IsNullOrEmpty(this.FStoreSQLIndex))
            {
                Function.ShowMessage("系统没有设置封账用的SQL，请咨询开发商！", MessageBoxIcon.Information);
                return 0;
            }
            if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
            {
                Function.ShowMessage("请您选择科室！", MessageBoxIcon.Information);
                this.cmbDept.Select();
                this.cmbDept.Focus();
                return 0;
            }

            FS.SOC.HISFC.BizLogic.Pharmacy.Financial financialMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Financial();

            if (MessageBox.Show(this, financialMgr.Operator.Name+"，您好！确认现在交班吗？", "提示>>", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return 0;
            }

            
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍后...");
            Application.DoEvents();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.IsExecuteProcedure)
            {
                if (financialMgr.ExecDailyStatic(this.cmbDept.Tag.ToString()) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    Function.ShowMessage("封账发生错误，请与系统管理员联系并报告错误：" + financialMgr.Err, MessageBoxIcon.Information);
                    return -1;
                }
            }
            else
            {
                string curSQL = "";
                if (financialMgr.Sql.GetSql(this.FStoreSQLIndex, ref curSQL) == -1)
                {
                    Function.ShowMessage("没有找到封账用的SQL，请与系统管理员联系！", MessageBoxIcon.Information);
                    return -1;
                }
                if (financialMgr.ExecNoQuery(curSQL, this.cmbDept.Tag.ToString(), financialMgr.GetDateTimeFromSysDateTime().ToString(), financialMgr.Operator.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    Function.ShowMessage("封账发生错误，请与系统管理员联系并报告错误：" + financialMgr.Err, MessageBoxIcon.Information);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            Function.ShowMessage("操作成功！", MessageBoxIcon.Information);

            this.ShowList();


            return 1;
        }

        #endregion

        #region 工具栏
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("封账", "形成盘点单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F封帐, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "封账")
            {
                this.FStore();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
           
            return this.ShowList();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(this.cmbDept.Tag!=null && !string.IsNullOrEmpty(this.cmbDept.Tag.ToString()))
            {
                this.ShowList();
            }
        }
        #endregion

        #region 事件
        void neuTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is FS.HISFC.Models.Pharmacy.Check)
            {
                FS.HISFC.Models.Pharmacy.Check check = e.Node.Tag as FS.HISFC.Models.Pharmacy.Check;

                DateTime dtBeginTime = this.dtStart.Value;
                DateTime dtEndTime = this.dtEnd.Value;

                this.dtStart.Value = check.FOper.OperTime;
                this.dtEnd.Value = this.dtStart.Value;
                this.cmbDept.Tag = check.StockDept.ID;

                this.QueryData();

                this.dtStart.Value = dtBeginTime;
                this.dtEnd.Value = dtEndTime;

                this.lbAdditionTitleLeft.Text = "交班时间：" + check.FOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
                this.lbAdditionTitleMid.Text = "科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDeptName(check.StockDept.ID) + "      确认签名："; 
                this.lbAdditionTitleRight.Text = "交班人：" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(check.FOper.ID);
            }
        }

        #endregion
    }
}
