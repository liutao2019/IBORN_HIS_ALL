using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDWY
{
    /// <summary>
    /// 注射查询补打界面
    /// </summary>
    public partial class ucInjectQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 定义域
        /// <summary>
        /// 人员信息类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager personMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 注射函数类
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject injMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// 病人费用信息
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee patientMgr = new FS.HISFC.BizProcess.Integrate.Fee();


        ArrayList alResult = new ArrayList();
        /// <summary>
        /// 最大注射顺序号
        /// </summary>
        int maxInjectOrder = 0;

        /// <summary>
        /// 病人信息集合
        /// </summary>
        Hashtable hsInfos = new Hashtable();
        #endregion

        #region 属性

        /// <summary>
        /// 打印在巡视卡上的用法
        /// </summary>
        private string printUsage = "";

        /// <summary>
        /// 用法是否打印在巡视卡上
        /// {EE46827D-D081-4aa5-8653-1EF9D176A5DC}
        /// </summary>
        [Description("用法是否打印在巡视卡上，以；号结尾"), Category("设置")]
        public string Usage
        {
            get
            {
                return this.printUsage;
            }
            set
            {
                this.printUsage = value;
            }
        }

        /// <summary>
        /// 静脉输液用法（打印瓶签的用法）
        /// </summary>
        private string injectUsage = "";


        [Description("静脉输液用法（打印瓶签的用法） （维护ID,根据符号;分隔）"), Category("设置")]
        public string InjectUsage
        {
            get
            {
                return this.injectUsage;
            }
            set
            {
                this.injectUsage = value;
            }
        }

        #endregion


        public ucInjectQuery()
        {
            InitializeComponent();
            this.Init();
        }

        #region 初始化

        private void Init()
        {
            //操作员
            ArrayList al = new ArrayList();
            al = personMgr.QueryEmployeeAll();
            if (al == null) al = new ArrayList();
            //登记人
            this.cmbRegOper.AddItems(al);
            //配药人
            this.cmbMixOper.AddItems(al);
            //注射人
            this.cmbInjectOper.AddItems(al);

            this.txtCardNo.Focus();
            this.setFP();
            this.dtRegDate.Value = this.injMgr.GetDateTimeFromSysDateTime();
            this.dtInjectDate.Value = this.injMgr.GetDateTimeFromSysDateTime();
            this.dtMixDate.Value = this.injMgr.GetDateTimeFromSysDateTime();
        }

        #endregion

        #region 工具条

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("全选", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            this.toolBarService.AddToolButton("取消", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("打印瓶签", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印签名卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印注射单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("打印巡视单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("取消登记", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            return this.toolBarService;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "全选":
                    this.SelectAll(true);
                    break;
                case "取消":
                    this.SelectAll(false);
                    break;
                case "取消全选":
                    break;
                case "打印瓶签":
                    this.Print();
                    break;
                case "打印签名卡":
                    this.PrintItinerate();
                    break;
                case "打印注射单":
                    this.PrintInject();
                    break;
                case "打印巡视单":
                    this.PrintInjectScoutCard();
                    break;
                case "取消登记":
                    this.Save();
                    break;
                default:
                    break;
            }
        }


        #endregion

        #region 方法

        /// <summary>
        /// 是否需要打印的用法
        /// </summary>
        /// <param name="usageCode"></param>
        /// <returns></returns>
        private bool IsPrintUsage(string usageCode)
        {
            //如果界面没有维护打印用法时，根据院注用法打印
            if (string.IsNullOrEmpty(printUsage))
            {
                return SOC.HISFC.BizProcess.Cache.Common.IsInnerInjectUsage(usageCode);
            }
            else
            {
                return printUsage.Contains(usageCode + ";");
            }
        }

        /// <summary>
        /// 获取SQL语句
        /// </summary>
        /// <returns></returns>
        private int GetSQL()
        {
            alResult = new ArrayList();
            string strSQL = "";
            string strWhere = "";
            bool IsWhere = false;

            #region 获取基本SQL
            try
            {
                strSQL = this.injMgr.GetSqlInjectInfo();
            }
            catch (Exception e)
            {
                MessageBox.Show("获取SQL出错!" + e.Message);
                return -1;
            }
            #endregion

            #region	 根据界面条件，生成WHERE条件
            //1.卡号
            string cardNo = "";
            #region {D9F5A25A-7529-4415-9CAC-20D1C7667B2F}
            //if (this.txtCardNo.Focused)
            //{
            cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');
            this.txtCardNo.Text = cardNo;
            //} 
            #endregion
            if (cardNo != null && cardNo != "" && cardNo != "0000000000")
            {
                strWhere = " where CARD_NO = '" + cardNo + "'";
                IsWhere = true;
            }
            //2.病人名称
            string patientName;
            patientName = this.txtName.Text.Trim();
            if (patientName != null && patientName != "")
            {
                if (!IsWhere)
                {
                    strWhere = strWhere + " where PATIENT_NAME = '" + patientName + "'";
                }
                else
                {
                    strWhere = strWhere + " and PATIENT_NAME = '" + patientName + "'";
                }
                IsWhere = true;
            }
            //3.登记人
            if (this.cbRegOper.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere + " where BOOKER_ID = '" + this.cmbRegOper.Tag.ToString().Trim() + "'";
                }
                else
                {
                    strWhere = strWhere + " and BOOKER_ID = '" + this.cmbRegOper.Tag.ToString().Trim() + "'";
                }
                IsWhere = true;
            }
            //4.登记日期 
            if (this.cbRegDate.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere +
                        " where REGISTER_DATE >= to_date('" + this.dtRegDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                        + " and REGISTER_DATE < to_date('" + this.dtRegDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                }
                else
                {
                    strWhere = strWhere +
                        " and REGISTER_DATE >= to_date('" + this.dtRegDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                        + " and REGISTER_DATE < to_date('" + this.dtRegDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                }
                IsWhere = true;
            }
            //5.流水号
            if (this.cbOrder.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere + "where ORDER_NO = '" + this.txtOrder.Text.ToString().Trim() + "'";
                }
                else
                {
                    strWhere = strWhere + "and ORDER_NO = '" + this.txtOrder.Text.ToString().Trim() + "'";
                }
                IsWhere = true;
            }
            //6.配药护士
            if (this.cbMixOper.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere + " where MIX_ID = '" + this.cmbMixOper.Tag.ToString().Trim() + "'";
                }
                else
                {
                    strWhere = strWhere + " and MIX_ID = '" + this.cmbMixOper.Tag.ToString().Trim() + "'";
                }
                IsWhere = true;
            }
            //7.配药时间
            if (this.cbMixDate.Checked)
            {
                if (!IsWhere)
                {
                    #region {D9F5A25A-7529-4415-9CAC-20D1C7667B2F}
                    strWhere = strWhere +
                        " where MIX_DATE >= to_date('" + this.dtMixDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                        + " and MIX_DATE < to_date('" + this.dtMixDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                    //strWhere = strWhere +
                    //    " where MIX_DATE >= to_date('" + this.dtMixDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                    //    + " and MIX_DATE < to_date('" + this.dtMixDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + ",'yyyy-mm-dd hh24:mi:ss'')"; 
                    #endregion
                }
                else
                {
                    strWhere = strWhere +
                        " and MIX_DATE >= to_date('" + this.dtMixDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                        + " and MIX_DATE < to_date('" + this.dtMixDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                }
                IsWhere = true;
            }
            //8.注射护士
            if (this.cbInjectOper.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere + " where INJECT_CODE = '" + this.cmbInjectOper.Tag.ToString().Trim() + "'";
                }
                else
                {
                    strWhere = strWhere + " and INJECT_CODE = '" + this.cmbInjectOper.Tag.ToString().Trim() + "'";
                }
                IsWhere = true;
            }
            //9.注射时间
            if (this.cbInjectDate.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere +
                        " where INJECT_DATE >= to_date('" + this.dtInjectDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                        + " and INJECT_DATE < to_date('" + this.dtInjectDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                }
                else
                {
                    strWhere = strWhere +
                        " and INJECT_DATE >= to_date('" + this.dtInjectDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "')"
                        + " and INJECT_DATE < to_date('" + this.dtInjectDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss') ";
                }
                IsWhere = true;
            }
            //添加排序
            if (!IsWhere)
            {
                MessageBox.Show("请输入查询条件!");
                return -1;
            }
            strWhere = strWhere + "order by ID";
            #endregion

            strSQL = strSQL + strWhere;
            alResult = this.injMgr.myGetInfo(strSQL);
            return 0;
        }
        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {

            this.Clear();
            if (this.GetSQL() == -1)
            {

                return;
            }

            if (alResult == null || alResult.Count == 0)
            {
                MessageBox.Show("没有符合查询条件的数据!", "提示");
                this.txtCardNo.Focus();
                return;
            }

            this.AddDetail(alResult);
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.lessShow();
                this.SelectAll(false);
                this.SetComb();
            }

            this.setFP();
            this.txtCardNo.Focus();
            if (this.txtCardNo.Text.ToString().Trim() == "0000000000")
            {
                this.txtCardNo.Text = "";
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
        /// <summary>
        /// 查询的结果添加到界面
        /// </summary>
        /// <param name="al"></param>
        private void AddDetail(ArrayList al)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }
            FS.HISFC.BizProcess.Integrate.Manager con = new FS.HISFC.BizProcess.Integrate.Manager();
            foreach (FS.HISFC.Models.Nurse.Inject info in al)
            {
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                int row = this.neuSpread1_Sheet1.RowCount - 1;
                this.neuSpread1_Sheet1.Rows[row].Tag = info;

                #region 实体复制到窗口
                string strTest = this.GetHyTestInfo(((Int32)info.Hypotest).ToString());


                FS.HISFC.BizProcess.Integrate.Manager PsMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee ps = PsMgr.GetEmployeeInfo(info.Booker.ID);

                this.neuSpread1_Sheet1.SetValue(row, 1, info.ID.ToString());//流水号
                this.neuSpread1_Sheet1.SetValue(row, 2, info.OrderNO.ToString());//日顺序号
                this.neuSpread1_Sheet1.Cells[row, 2].Tag = info.OrderNO.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 3, info.Patient.PID.CardNO.ToString());//病例号
                this.neuSpread1_Sheet1.Cells[row, 3].Tag = info.Patient.PID.CardNO.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 4, info.Patient.Name.ToString());//病人名称
                this.neuSpread1_Sheet1.Cells[row, 4].Tag = info.Patient.Name.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 5, info.Item.Order.ReciptDoctor.Name.ToString());//开单医生
                this.neuSpread1_Sheet1.Cells[row, 5].Tag = info.Item.Order.ReciptDoctor.ID.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 6, info.Item.Order.DoctorDept.Name.ToString());//开单科室
                this.neuSpread1_Sheet1.Cells[row, 6].Tag = info.Item.Order.DoctorDept.ID.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 7, info.Item.Name.ToString());//项目名称
                //				this.fpSpread1_Sheet1.SetValue(row,8,info.Item.CombNo.ToString());//组合
                this.neuSpread1_Sheet1.Cells[row, 8].Tag = info.Item.Order.Combo.ID.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 9, info.Item.Order.DoseOnce.ToString() + info.Item.Order.DoseUnit.ToString());//每次量
                this.neuSpread1_Sheet1.SetValue(row, 10, info.Item.Order.Frequency.ID.ToString());//频次
                this.neuSpread1_Sheet1.SetValue(row, 11, info.Item.Order.Usage.Name.ToString());//用法
                this.neuSpread1_Sheet1.SetValue(row, 12, strTest);//皮试
                this.neuSpread1_Sheet1.SetValue(row, 13, ps.Name);//登记人
                this.neuSpread1_Sheet1.SetValue(row, 14, info.Booker.OperTime.ToString());//登记时间
                this.neuSpread1_Sheet1.Cells[row, 14].Tag = info.Booker.OperTime.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 15, info.MixOperInfo.Name.ToString());//配药人
                this.neuSpread1_Sheet1.SetValue(row, 16, info.MixTime.ToString());//配药时间
                this.neuSpread1_Sheet1.SetValue(row, 17, info.InjectOperInfo.Name.ToString());//注射人
                this.neuSpread1_Sheet1.SetValue(row, 18, info.InjectTime.ToString());//注射时间
                this.neuSpread1_Sheet1.SetValue(row, 19, info.InjectSpeed.ToString());//滴速
                FS.HISFC.Models.Base.Employee stopName = PsMgr.GetEmployeeInfo(info.StopOper.ID);
                this.neuSpread1_Sheet1.SetValue(row, 20, stopName);//拔针人
                this.neuSpread1_Sheet1.SetValue(row, 21, info.EndTime.ToString());//拔针时间
                this.neuSpread1_Sheet1.SetValue(row, 22, info.SendemcTime.ToString());//送急诊时间
                this.neuSpread1_Sheet1.SetValue(row, 23, info.Memo.ToString());//特殊情况记录
                #endregion
            }
        }
        /// <summary>
        /// 取消登记//1.必须是当事人取消  2.已经配药，注射的不能取消登记  3.只取消打对勾的
        /// </summary>
        private void Save()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("没有需要保存的数据!");

                return;
            }
            int selectNum = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE" || this.neuSpread1_Sheet1.GetValue(i, 0).ToString() == "")
                {
                    selectNum++;
                }
            }
            if (selectNum >= this.neuSpread1_Sheet1.RowCount)
            {
                MessageBox.Show("请选择数据", "提示");
                return;
            }
            if (MessageBox.Show("您是否要删除所选择的登记信息？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.neuSpread1.StopCellEditing();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //SQLCA.BeginTransaction();

                this.injMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {

                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        FS.HISFC.Models.Nurse.Inject info =
                            (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;

                        #region 有效条件判断
                        if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                        if (info.MixOperInfo.ID != null && info.MixOperInfo.ID != "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("第" + (i + 1).ToString() + "行数据已经配药确认，不能取消登记!");
                            return;
                        }
                        if (info.InjectOperInfo.ID != null && info.InjectOperInfo.ID != "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("第" + (i + 1).ToString() + "行数据已经注射确认，不能取消登记!");
                            return;
                        }
                        if (info.Booker.ID != FS.FrameWork.Management.Connection.Operator.ID)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.HISFC.BizProcess.Integrate.Manager PsMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                            FS.HISFC.Models.Base.Employee ps = PsMgr.GetEmployeeInfo(info.Booker.ID);

                            MessageBox.Show("您不是第" + (i + 1).ToString() + "行记录的登记人，不能取消登记!",
                                "只有[" + ps.Name + "]才能取消这条记录!");
                            return;
                        }
                        #endregion
                        //1.删除met_nuo_inject记录
                        if (this.injMgr.Delete(info.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.patientMgr.Err, "提示");
                            return;
                        }
                        //2.fin_ipb_feeitemlist中，减少数量------------没有考虑并发的问题,以后再说吧----表中没有moorder,郁闷!
                        if (this.patientMgr.UpdateConfirmInject("ALL", info.Item.RecipeNO, info.Item.SequenceNO.ToString(), -1) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.patientMgr.Err, "提示");
                            return;
                        }
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    for (int i = this.neuSpread1_Sheet1.RowCount - 1; i >= 0; i--)
                    {
                        if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "TRUE")
                        {
                            this.neuSpread1_Sheet1.Rows[i].Remove();
                        }
                    }
                    MessageBox.Show("取消登记成功!", "提示");
                }
                catch (Exception e)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
        }
        /// <summary>
        /// 全选
        /// </summary>
        private void SelectAll(bool isSelected)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                this.neuSpread1_Sheet1.SetValue(i, 0, isSelected, false);
            }
        }

        #endregion

        #region 内部公共
        /// <summary>
        /// 设置组合号
        /// </summary>
        private void SetComb()
        {
            try
            {
                int myCount = this.neuSpread1_Sheet1.RowCount;
                int i;
                //第一行
                this.neuSpread1_Sheet1.SetValue(0, 8, "┓");
                //最后行
                this.neuSpread1_Sheet1.SetValue(myCount - 1, 8, "┛");
                //中间行
                for (i = 1; i < myCount - 1; i++)
                {
                    int prior = i - 1;
                    int next = i + 1;
                    string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString();//组合号
                    string currentRegDate = this.neuSpread1_Sheet1.Cells[i, 14].Tag.ToString();//登记时间
                    string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, 8].Tag.ToString();
                    string priorRegDate = this.neuSpread1_Sheet1.Cells[prior, 14].Tag.ToString();//登记时间
                    string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, 8].Tag.ToString();
                    string nextRegDate = this.neuSpread1_Sheet1.Cells[next, 14].Tag.ToString();//登记时间

                    #region """""
                    bool bl1 = true;
                    bool bl2 = true;
                    if (currentRowCombNo != priorRowCombNo || currentRegDate != priorRegDate)
                        bl1 = false;
                    if (currentRowCombNo != nextRowCombNo || currentRegDate != nextRegDate)
                        bl2 = false;
                    //  ┃
                    if (bl1 && bl2)
                    {
                        this.neuSpread1_Sheet1.SetValue(i, 8, "┃");
                    }
                    //  ┛
                    if (bl1 && !bl2)
                    {
                        this.neuSpread1_Sheet1.SetValue(i, 8, "┛");
                    }
                    //  ┓
                    if (!bl1 && bl2)
                    {
                        this.neuSpread1_Sheet1.SetValue(i, 8, "┓");
                    }
                    //  ""
                    if (!bl1 && !bl2)
                    {
                        this.neuSpread1_Sheet1.SetValue(i, 8, "");
                    }
                    #endregion
                }
                //把没有组号的去掉
                for (i = 0; i < myCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString() == "")
                    {
                        this.neuSpread1_Sheet1.SetValue(i, 8, "");
                    }
                }
                //判断首末行 有组号，且只有自己一组数据的情况
                if (myCount == 1)
                {
                    this.neuSpread1_Sheet1.SetValue(0, 8, "");
                }
                //只有首末两行，那么还要判断组号啊
                if (myCount == 2)
                {
                    if (this.neuSpread1_Sheet1.Cells[0, 8].Tag.ToString() != this.neuSpread1_Sheet1.Cells[1, 5].Tag.ToString())
                    {
                        this.neuSpread1_Sheet1.SetValue(0, 8, "");
                        this.neuSpread1_Sheet1.SetValue(1, 8, "");
                    }
                }
                if (myCount > 2)
                {
                    if (this.neuSpread1_Sheet1.GetValue(1, 8).ToString() != "┃"
                        && this.neuSpread1_Sheet1.GetValue(1, 8).ToString() != "┛")
                    {
                        this.neuSpread1_Sheet1.SetValue(0, 8, "");
                    }
                    if (this.neuSpread1_Sheet1.GetValue(myCount - 2, 8).ToString() != "┃"
                        && this.neuSpread1_Sheet1.GetValue(myCount - 2, 8).ToString() != "┓")
                    {
                        this.neuSpread1_Sheet1.SetValue(myCount - 1, 8, "");
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "设置组号错误");
            }
        }
        /// <summary>
        /// 设置格式
        /// </summary>
        private void setFP()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtOnly = new FarPoint.Win.Spread.CellType.TextCellType();
            txtOnly.ReadOnly = true;
            FarPoint.Win.Spread.CellType.CheckBoxCellType chkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1_Sheet1.Columns[1].Visible = false;
            for (int i = 1; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].CellType = txtOnly;
            }
            for (int i = 15; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].Visible = false;
            }
        }
        /// <summary>
        /// 压缩显示
        /// </summary>
        private void lessShow()
        {
            try
            {
                for (int j = 2; j < 5; j++)
                {
                    string startValue = this.neuSpread1_Sheet1.Cells[0, j].Tag.ToString();
                    int startRow = 0; //起点行
                    int endRow = 0;

                    for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        string currentValue = this.neuSpread1_Sheet1.Cells[i, j].Tag.ToString();
                        if (startValue != currentValue)
                        {
                            endRow = i - 1;
                            if (endRow - startRow > 0)
                            {
                                for (int k = startRow + 1; k <= endRow; k++)
                                {
                                    this.neuSpread1_Sheet1.SetValue(k, j, "");
                                }
                                FarPoint.Win.Spread.Model.CellRange cr = new FarPoint.Win.Spread.Model.CellRange(startRow, j, endRow - startRow + 1, 1);
                                this.neuSpread1.ActiveSheet.Models.Span.Add(cr.Row, cr.Column, cr.RowCount, cr.ColumnCount);
                            }
                            startValue = this.neuSpread1_Sheet1.Cells[i, j].Tag.ToString();
                            startRow = i;
                        }
                        //最后收尾
                        if (i == this.neuSpread1_Sheet1.RowCount - 1)
                        {
                            endRow = i - 1;
                            if (i - startRow > 0) //不止一行
                            {
                                for (int k = startRow + 1; k <= i; k++)
                                {
                                    this.neuSpread1_Sheet1.SetValue(k, j, "");
                                }
                                FarPoint.Win.Spread.Model.CellRange cr = new FarPoint.Win.Spread.Model.CellRange(startRow, j, endRow - startRow + 2, 1);
                                this.neuSpread1.ActiveSheet.Models.Span.Add(cr.Row, cr.Column, cr.RowCount, cr.ColumnCount);
                            }
                            else //最后一行单列，不需要合并
                            {
                            }
                        }//end if(i == this.fpSpread1_Sheet1.RowCount-1)
                    }
                }//end private void lessShow()
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "设置压缩显示错误");
            }
        }
        /// <summary>
        /// 获得最大注射顺序
        /// </summary>
        /// <returns></returns>
        private int GetMaxInjectOrder()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0) return 0;
            this.neuSpread1.StopCellEditing();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                    this.neuSpread1_Sheet1.GetText(i, 0) == "") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;

                if (FS.FrameWork.Function.NConvert.ToInt32(info.InjectOrder) > maxInjectOrder)
                {
                    maxInjectOrder = FS.FrameWork.Function.NConvert.ToInt32(info.InjectOrder);
                }
            }
            return maxInjectOrder;
        }

        /// <summary>
        /// 获取皮试信息
        /// </summary>
        /// <param name="hytestID"></param>
        /// <returns></returns>
        private string GetHyTestInfo(string hytestID)
        {
            switch (hytestID)
            {
                case "1":
                    return "免试";
                case "2":
                    return "需皮试";
                case "3":
                    return "阳性";
                case "4":
                    return "阴性";
                case "0":
                default:
                    return "否";
            }
        }

        #endregion

        #region 打印

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (!injectUsage.Contains(info.Item.Order.Usage.ID+";"))
                {
                    continue;
                }

                //临时处理，为了让瓶签在补打界面有标记
                info.User03 = "true";
                al.Add(info);
            }
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            FS.HISFC.Models.Nurse.Inject inJect;

            FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint curePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            if (curePrint == null)
            {

            }
            curePrint.Init(al);
        }


        /// <summary>
        /// 打印注射单
        /// </summary>
        private void PrintInject()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;

                al.Add(info);
            }
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }

            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint injectPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            if (injectPrint == null)
            {

            }

            //避免操作员把补打界面当做 正常打印界面，补打给出提示
            if (MessageBox.Show("是否确认补打单据？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                injectPrint.Init(al);
            }
        }
        /// <summary>
        /// 打印签名卡
        /// </summary>
        private void PrintItinerate()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;

                al.Add(info);
            }
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }

            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {

            }
            //避免操作员把补打界面当做 正常打印界面，补打给出提示
            if (MessageBox.Show("是否确认补打单据？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                itineratePrint.IsReprint = true;
                itineratePrint.Init(al);
            }
            //Nurse.Print.ucPrintItinerate uc = new Nurse.Print.ucPrintItinerate();
            //uc.Init(al);
        }
        /// <summary>
        /// A4纸张的签名卡
        /// </summary>
        private void PrintItinerateLarge()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;
                FS.HISFC.Models.Order.OutPatient.Order orderinfo =
                    (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, 11].Tag;

                al.Add(info);
            }
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }

            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} 打印改为接口方式
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
            }
            //避免操作员把补打界面当做 正常打印界面，补打给出提示
            if (MessageBox.Show("是否确认补打单据？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                itineratePrint.IsReprint = true;
                itineratePrint.Init(al);
            }

        }



        /// <summary>
        /// 获取要打印的数据（可维护用法）
        ///{0D976883-8A45-4a97-AFEF-7D8ED425C89A}
        /// </summary>
        /// <returns></returns>
        private int GetAllPrintInjectList()
        {
            this.neuSpread1.StopCellEditing();
            hsInfos.Clear();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                    continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;


                //if (!string.IsNullOrEmpty(printUsage))
                //{
                //    if (!printUsage.Contains(info.Item.Order.Usage.ID+";"))
                //    {
                //        continue;
                //    }
                //}
                if (!IsPrintUsage(info.Item.Order.Usage.ID))
                {
                    continue;
                }

                if (!hsInfos.ContainsKey(info.Item.Order.ReciptDoctor.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(info);
                    hsInfos.Add(info.Item.Order.ReciptDoctor.ID, al);
                }
                else
                {
                    ((ArrayList)hsInfos[info.Item.Order.ReciptDoctor.ID]).Add(info);
                }
            }
            return 1;
        }

        /// <summary>
        /// {E73C3DF3-9DCF-4a46-ADE0-3D44663F6F7A}
        /// 打印静脉输液巡视卡
        /// </summary>
        private void PrintInjectScoutCard()
        {
            int intReturn = this.GetAllPrintInjectList();
            if (intReturn == -1)
            {
                MessageBox.Show("没有选择数据!");
                return;
            }
            int intCount = 0;
            foreach (ArrayList al in hsInfos.Values)
            {
                FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
                if (itineratePrint == null)
                {
                    return;
                }
                //避免操作员把补打界面当做 正常打印界面，补打给出提示
                if (MessageBox.Show("是否确认补打单据？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    itineratePrint.IsReprint = true;
                    itineratePrint.Init(al);
                }
            }
            this.SelectAll(true);
        }
        #endregion

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
                FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();
                if (this.txtCardNo.Text.Trim() == "")
                {
                    MessageBox.Show("请输入病历号!", "提示");
                    this.txtCardNo.Focus();
                    return;
                }
                string strCardNO = this.txtCardNo.Text.Trim();//.PadLeft(10, '0');
                int iTemp = feeManage.ValidMarkNO(strCardNO, ref objCard);
                if (iTemp <= 0 || objCard == null)
                {
                    MessageBox.Show("无效卡号，请联系管理员！");
                    return;
                }
                string cardNo = objCard.Patient.PID.CardNO;
                this.txtCardNo.Text = cardNo;
                this.Query();
            }
        }

        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Query();
            }
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            this.neuSpread1.StopCellEditing();
            string strTemp = this.neuSpread1_Sheet1.GetValue(e.Row, 0).ToString().ToUpper();
            if (e.Column == 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    //病历号,顺序号,组号,登记时间
                    if (this.neuSpread1_Sheet1.Cells[i, 2].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 2].Tag.ToString()
                        && this.neuSpread1_Sheet1.Cells[i, 3].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 3].Tag.ToString()
                        && this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 8].Tag.ToString()
                        && this.neuSpread1_Sheet1.Cells[i, 8].Tag != null && this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString() != ""
                        && this.neuSpread1_Sheet1.Cells[i, 14].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 14].Tag.ToString())
                    {
                        if (strTemp == "TRUE")
                        {
                            this.neuSpread1_Sheet1.SetValue(i, 0, true, false);
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.SetValue(i, 0, false, false);
                        }
                    }
                }
            }
        }
    }
}
