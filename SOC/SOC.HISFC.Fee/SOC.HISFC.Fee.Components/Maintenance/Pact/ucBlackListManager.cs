using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    public partial class ucBlackListManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBlackListManager()
        {
            InitializeComponent();
        }
        
        #region 变量属性

        // 人员名单DataTable
        DataTable blackListDt = new DataTable();
        // 黑名单业务处理实例
        private FS.SOC.HISFC.Fee.BizLogic.BlackList blackListBiz = new FS.SOC.HISFC.Fee.BizLogic.BlackList();
        // 合同单位业务处理实例
        private FS.SOC.HISFC.Fee.BizLogic.PactInfo pactInfo = new FS.SOC.HISFC.Fee.BizLogic.PactInfo();
        // 修改黑名单信息窗体
        private FS.SOC.HISFC.Fee.Components.Maintenance.Pact.ucBlackListUpdate blackListUpdateFrm = new FS.SOC.HISFC.Fee.Components.Maintenance.Pact.ucBlackListUpdate();

        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 添加工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)  
        {
            toolBarService.AddToolButton("新增", "新增", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("修改", "修改", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarService.AddToolButton("删除", "删除", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            return toolBarService;
        }
        /// <summary>
        /// 工具栏事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "新增")
            {
                this.Add();
            }
            else if (e.ClickedItem.Text == "修改")
            {
                this.Modify();
            }
            else if (e.ClickedItem.Text == "删除")
            {
                this.Delete();
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Query(object sender, object neuObject)
        {
            this.QueryBlackList();
            return base.Query(sender, neuObject);
        }
        
        #endregion

        # region 初始化
        
        // 页面加载初始化
        protected override void OnLoad(EventArgs e)
        {
            if (this.Init() == 1)//界面初始化成功
            {
                this.QueryBlackList();
            }
            this.neuQueryTxtBox.TextChanged += new EventHandler(neuQueryTxtBox_TextChanged);
            this.neuSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread_CellDoubleClick);
            
            base.OnLoad(e);
        }
        // 初始化界面
        private int Init()
        {
            try
            {
                this.InitBaseData();
                this.InitDataTable();
                this.InitFarPoint();

                this.blackListUpdateFrm.Init();

                this.blackListUpdateFrm.GetNextItem -= new ucBlackListUpdate.GetNextItemHandler(this.frmItem_GetNextItem);
                this.blackListUpdateFrm.GetNextItem += new ucBlackListUpdate.GetNextItemHandler(this.frmItem_GetNextItem);
                this.blackListUpdateFrm.EndSave -= new ucBlackListUpdate.SaveItemHandler(this.frmItem_EndSave);
                this.blackListUpdateFrm.EndSave += new ucBlackListUpdate.SaveItemHandler(this.frmItem_EndSave);
            }
            catch (System.Exception ex)
            {
                CommonController.CreateInstance().MessageBox("初始化失败，请系统管理员报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }
            return 1;
        }
        // 初始化页面基本数据
        private void InitBaseData()
        {
            //合同单位
            this.neuPactCodeCbb.AddItems(pactInfo.QueryPactUnitAll());
        }
        // 初始化表单数据
        private void InitDataTable()
        {

        }
        // 初始化farPoint数据
        private void InitFarPoint()
        {
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "编号")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "姓名")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "合同单位名称")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "医疗证号")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "种类")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "身份证号")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "性别")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "出生日期")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "定点医院1")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "定点医院2")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "定点医院3")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "门诊比例")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "住院比例")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "起始日期（有效期）")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "结束日期（有效期）")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "日限额")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "月限额")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "年限额")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "一次限额")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "床位上限")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "空调上限")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "操作员")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "操作时间")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "门诊启用标记")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "住院启用标记")].AllowAutoSort = true;
            this.neuBlackListSpread.Columns[this.neuSpread.GetColumnIndex(0, "单位编码")].AllowAutoSort = true;
        }
        
        #endregion

        #region 事件

        // neuQueryTxtBox输入框文本变化事件
        void neuQueryTxtBox_TextChanged(object sender, EventArgs e)
        {
            string filter = this.GetFilter();
            DataView blackListDv = blackListDt.DefaultView;
            blackListDv.RowFilter = filter;
            this.neuBlackListSpread.DataSource = blackListDv.ToTable();
            this.InitFarPoint();
        }
        // 表格所选行双击修改数据
        void neuSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Modify();
        }
        // 获取下一项
        void frmItem_GetNextItem(int span)
        {
            this.neuBlackListSpread.ActiveRowIndex += span;
            this.neuSpread.ShowRow(0, this.neuBlackListSpread.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);

            string blackId = this.neuSpread.GetCellText(0, this.neuBlackListSpread.ActiveRowIndex, "编号");
            FS.SOC.HISFC.Fee.BizLogic.BlackList blackListBiz = new FS.SOC.HISFC.Fee.BizLogic.BlackList();
            FS.SOC.HISFC.Fee.Models.BlackList blackList = blackListBiz.GetBlackListById(blackId);
            if (blackList == null)
            {
                FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("判断黑名单人员情况时获取信息失败，请向系统管理员联系并报告错误：" + blackListBiz.Err, MessageBoxIcon.Error);
                return;
            }

            this.blackListUpdateFrm.SetBlackList(blackList);
        }
        // 更新修改数据
        void frmItem_EndSave(FS.SOC.HISFC.Fee.Models.BlackList blackList)
        {
            blackListDt.PrimaryKey = new System.Data.DataColumn[] { blackListDt.Columns[0] };//设置第一列为主键
            DataRow row = this.blackListDt.Rows.Find(blackList.BlackId);
            if(row != null)
            {
                foreach (DataColumn dc in this.blackListDt.Columns)
                {
                    dc.ReadOnly = false;
                }
                this.SetDataRowValue(blackList, row);
                row.AcceptChanges();
                foreach (DataColumn dc in this.blackListDt.Columns)
                {
                    dc.ReadOnly = true;
                }
            }
            else
            {
                this.AddItemObjectToDataTable(blackList);
            }
            this.neuBlackListSpread.DataSource = this.blackListDt;
        }
       
        #endregion

        #region 修改、增加、删除
        
        // 修改
        private void Modify()
        {
            string blackId = this.neuSpread.GetCellText(0, this.neuBlackListSpread.ActiveRowIndex, "编号");
            FS.SOC.HISFC.Fee.BizLogic.BlackList blackListBiz = new FS.SOC.HISFC.Fee.BizLogic.BlackList();
            FS.SOC.HISFC.Fee.Models.BlackList blackList = blackListBiz.GetBlackListById(blackId);
            if (blackList == null)
            {
                FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("判断黑名单人员情况时获取信息失败，请向系统管理员联系并报告错误：" + blackListBiz.Err, MessageBoxIcon.Error);
                return;
            }
            this.blackListUpdateFrm.SetBlackList(blackList);
            this.blackListUpdateFrm.ShowDialog();
        }
        // 新增
        private void Add()
        {
            this.blackListUpdateFrm.SetBlackList(new FS.SOC.HISFC.Fee.Models.BlackList());
            this.blackListUpdateFrm.ShowDialog();
        }
        // 删除
        private void Delete()
        {
            if (MessageBox.Show("删除后的信息不可以恢复，确认删除吗？", "提示>>", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            string blackId = this.neuSpread.GetCellText(0, this.neuBlackListSpread.ActiveRowIndex, "编号");
            FS.SOC.HISFC.Fee.BizLogic.BlackList blackListBiz = new FS.SOC.HISFC.Fee.BizLogic.BlackList();
            FS.SOC.HISFC.Fee.Models.BlackList blackList = blackListBiz.GetBlackListById(blackId);

            if (blackList != null)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                if (blackListBiz.DeleteItem(blackList.BlackId) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("删除失败，请向系统管理员联系并报告错误：" + blackListBiz.Err, MessageBoxIcon.Error);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                blackListDt.PrimaryKey = new System.Data.DataColumn[] { blackListDt.Columns[0] };//设置第一列为主键
                DataRow row = this.blackListDt.Rows.Find(new string[] { blackList.BlackId });
                if (row != null)
                {
                    this.blackListDt.Rows.Remove(row);
                }
            }
        }
        
        #endregion

        #region 方法

        // 查询绑定表数据
        private void QueryBlackList()
        {
            this.neuBlackListSpread.DataSource = null;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据...");
            Application.DoEvents();

            blackListDt = this.blackListBiz.QueryAllBlackList();

            int progress = 1;
            for (int i = 0; i < blackListDt.Rows.Count; i++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress++, blackListDt.Rows.Count);
            }

            string filter = this.GetFilter();
            DataView blackListDv = blackListDt.DefaultView;

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            if (blackListDt != null)
            {
                blackListDt.AcceptChanges();
                blackListDv.RowFilter = filter;
                blackListDt = blackListDv.ToTable();
                this.neuBlackListSpread.DataSource = blackListDt;
            }
            this.InitFarPoint();
        }
        // 姓名卡号过滤（查询）
        private string GetFilter()
        {
            string filter = "";
            if(this.blackListDt == null)
            {
                return filter;
            }
            DataView blackListDv = blackListDt.DefaultView;
            //姓名卡号过滤
            if (!string.IsNullOrEmpty(this.neuQueryTxtBox.Text))
            {
                if (blackListDv.Table.Columns.Contains("姓名"))
                {
                    filter = Function.ConnectFilterStr(filter, string.Format("姓名 like '{0}'", "%" + this.neuQueryTxtBox.Text + "%"), "or");
                }
                if (blackListDv.Table.Columns.Contains("医疗证号"))
                {
                    filter = Function.ConnectFilterStr(filter, string.Format("医疗证号 like '{0}'", "%" + this.neuQueryTxtBox.Text + "%"), "or");
                }
                filter = "(" + filter + ")";
            }
            //合同单位过滤
            if (!string.IsNullOrEmpty(this.neuPactCodeCbb.Text))
            {
                filter = Function.ConnectFilterStr(filter, string.Format("合同单位名称 like '{0}'", "%" + this.neuPactCodeCbb.Text + "%"), "and");
            }
            return filter;
        }
        // 新增添加行数据
        private int AddItemObjectToDataTable(FS.SOC.HISFC.Fee.Models.BlackList blackList)
        {
            if (blackList == null)
            {
                FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("向DataTable中添加黑名单基本信息失败：黑名单基本信息为null" , MessageBoxIcon.Error);
                return -1;
            }
            if (this.blackListDt == null)
            {
                FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().MessageBox("向DataTable中添加黑名单基本信息失败：DataTable为null", MessageBoxIcon.Error);
                return -1;
            }
            DataRow row = this.blackListDt.NewRow();

            this.SetDataRowValue(blackList, row);

            this.blackListDt.Rows.InsertAt(row,0);

            return 1;
        }
        // 更新指定行数据 row选取的字段同步FS.SOC.HISFC.Fee.BizLogic.BlackList 中的columnsTable
        private int SetDataRowValue(FS.SOC.HISFC.Fee.Models.BlackList blackList, DataRow row)
        {
            row["编号"] = blackList.BlackId;
            row["姓名"] = blackList.Name;
            row["合同单位名称"] = blackList.PactName;
            row["医疗证号"] = blackList.McordNo;
            row["种类"] = blackList.KindName;
            row["身份证号"] = blackList.IdDno;
            row["性别"] = blackList.SexName;
            row["出生日期"] = blackList.Birthday;
            row["定点医院1"] = blackList.Ddyy1;
            row["定点医院2"] = blackList.Ddyy2;
            row["定点医院3"] = blackList.Ddyy3;
            row["门诊比例"] = blackList.ClinicRate;
            row["住院比例"] = blackList.InpatientRate;
            row["起始日期（有效期）"] = blackList.BeginDate;
            row["结束日期（有效期）"] = blackList.EndDate;
            row["日限额"] = blackList.DayLimit;
            row["月限额"] = blackList.MonthLimit;
            row["年限额"] = blackList.YearLimit;
            row["一次限额"] = blackList.OnceLimit;
            row["床位上限"] = blackList.BedLimit;
            row["空调上限"] = blackList.AirLimit;
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            row["操作员"] = employee.Name;
            row["操作时间"] = this.blackListBiz.GetDateTimeFromSysDateTime();
            row["门诊启用标记"] = blackList.ClinicFlag;
            row["住院启用标记"] = blackList.InpatientFlag;
            row["单位编码"] = blackList.UnitCode;

            return 1;
        }
        // 导出
        public override int Export(object sender, object neuObject)
        {
            this.neuSpread.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            return base.Export(sender, neuObject);
        }
        
        #endregion
    }
}
