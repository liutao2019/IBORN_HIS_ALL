using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    /// <summary>
    /// 病案回收系统
    /// </summary>
    public partial class ucCaseHistory : FS.FrameWork.WinForms.Controls.ucBaseControl//, FS.NFC.Interface.Forms.IInterfaceContainer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ucCaseHistory()
        {
            InitializeComponent();
            this.neuComboBox1.SelectedIndex = 0;
            this.neuFpEnter1_Sheet1.RowCount = this.neuFpEnter2_Sheet1.RowCount = 0;
            this.neuTabControl1.SelectedIndex = 1;
            this.neuFpEnter1_Sheet1.Columns[1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuFpEnter2_Sheet1.Columns[1].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            this.neuFpEnter1_Sheet1.GrayAreaBackColor = Color.White;
            this.neuFpEnter2_Sheet1.GrayAreaBackColor = Color.White;

            this.neuTextBoxIsNotCBPatientNO.KeyDown += new KeyEventHandler(neuTextBoxIsNotCBPatientNO_KeyDown);
            this.neuTextBoxPatientNO.KeyDown += new KeyEventHandler(neuTextBoxPatientNO_KeyDown);

            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            alDept = deptMgr.GetDeptmentIn(FS.HISFC.Models.Base.EnumDepartmentType.I);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "全部";
            obj.Memo = "";
            alDept.Add(obj);
            if (alDept != null)
            {
                this.neuCmbDept.AddItems(alDept);
                this.neuCmbDept1.AddItems(alDept);
            }
            this.neuCmbDept.SelectedIndex = alDept.Count - 1;
            this.neuCmbDept1.SelectedIndex = alDept.Count - 1;

            this.neuCmbModifyStyle.SelectedIndex = 0; //并按回收日期更改类型 是按照天数更改还是按照日期更改  默认按照天数更改
            bool b = this.neuCmbModifyStyle.SelectedIndex == 0 ? (this.neuDtModifier.Visible =
                      !(this.ucAdvanceDays1.Visible = false)) : (this.neuDtModifier.Visible = !(this.ucAdvanceDays1.Visible = true));
            

        }

        #region 私有变量&公有属性

        ArrayList alDept = null;

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 包容控件的窗体
        /// </summary>
        MyForm form = new MyForm();
        /// <summary>
        /// 回收率控件
        /// </summary>
        ucCaseCallbackPercent caseCallbackPercent = new ucCaseCallbackPercent();
        /// <summary>
        /// 超时病案明细信息控件
        /// </summary>
        ucTimeoutCase timeoutCase = new ucTimeoutCase();
        /// <summary>
        /// 回收数量查询控件
        /// </summary>
        ucCallBackNum callbackNum = new ucCallBackNum();

        //TreeView tv = null;
        /// <summary>
        /// 病案回收实体
        /// </summary>
        FS.HISFC.Models.HealthRecord.CaseHistory.CallBack callBack = new FS.HISFC.Models.HealthRecord.CaseHistory.CallBack();

        /// <summary>
        /// 病案回收管理类
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack callBackMgr = null;

        private bool ISCaseCallBackLimits = true;//回收权限 通过常数（无权限人员常数）CaseCallBackLimits控制 暂时先按照这种方式控制 2011-6-27 chengym
        /// <summary>
        /// 弹出窗体
        /// </summary>
        Form listForm = null;

        ListView lv = null;

        string specifyDept ="";
        /// <summary>
        /// 特殊回收天数的科室数组 ,隔开
        /// </summary>
        [Category("特殊回收科室"), Description("一般科室为5&7天回收 此类科室为？天回收")]
        public string SpecifyDept
        {
            get { return specifyDept; }
            set { specifyDept = value; }
        }

        bool isNeedAuthority = false;
        /// <summary>
        /// 设置访问权限开关  true为开 常数CaseCallBackLimits名单有访问权
        /// </summary>
        [Category("设置访问权限开关"), Description("true为开，false为关 开后需要维护常数CaseCallBackLimits名单")]
        public bool IsNeedAuthority
        {
            get { return this.isNeedAuthority; }
            set { this.isNeedAuthority = value; }
        }
        DateTime dtBeginUse = new DateTime(2011,1,1);
        /// <summary>
        /// 回收功能开始使用时间
        /// </summary>
        [Category("设置开始使用时间开关"), Description("开始使用的时间，限制未回收查询开始日期")]
        public DateTime DtBeginUse
        {
            get { return this.dtBeginUse; }
            set { this.dtBeginUse = value; }
        }

        int firTimeOut = 5;
        /// <summary>
        ///最小超时天数
        /// </summary>
        [Category("最小超时天数"), Description("一般科室为5天回收")]
        public int FirTimeOut
        {
            get { return firTimeOut; }
            set { firTimeOut = value; }
        }

        int deaTimeOut = 7;
        /// <summary>
        ///最小超时天数
        /// </summary>
        [Category("死亡出院最小超时天数"), Description("一般科室为7天回收")]
        public int DeaTimeOut
        {
            get { return deaTimeOut; }
            set { deaTimeOut = value; }
        }

        int secTimeOut = 8;
        /// <summary>
        ///最大超时天数
        /// </summary>
        [Category("最大超时天数"), Description("一般科室为8天回收")]
        public int SecTimeOut
        {
            get { return secTimeOut; }
            set { secTimeOut = value; }
        }
        /// <summary>
        /// 回收状态枚举
        /// </summary>
        public enum CaseType
        {
            /// <summary>
            /// 未回收
            /// </summary>
            UnCallBack=0,
            /// <summary>
            /// 已回收
            /// </summary>
            CallBack =1
        }
        #endregion 

        #region 左侧树 暂时未用
        //protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        //{
        //    this.tv = sender as TreeView;
        //    return base.OnInit(sender, neuObject, param);
        //}
        #endregion
        /// <summary>
        /// load初始化界面
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            callBackMgr = new FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack(specifyDept,firTimeOut,deaTimeOut,secTimeOut);
            if (this.isNeedAuthority)
            {
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                FS.FrameWork.Models.NeuObject obj = con.GetConstant("CaseCallBackLimits", con.Operator.ID);
                if (obj.ID == "")
                {
                    MessageBox.Show("您无分配“病案回收”权限！", "提示-病案管理 请集成平台增加 常数“CaseCallBackLimits”记录");
                    this.neuTabControl1.Enabled = false;
                    this.ISCaseCallBackLimits = false;
                    return;
                }
            }
            base.OnLoad(e);
        }

        #region 病案操作处理函数
        /// <summary>
        /// 根据病案回收实体信息 初始化fp
        /// </summary>
        /// <param name="cb"></param>
        private void InitFp(FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb, FarPoint.Win.Spread.SheetView sheetView)
        {
            if (cb == null || cb.Patient.ID == "")
            {
                return;
            }

            sheetView.RowCount++;
            sheetView.Cells[sheetView.RowCount - 1, 0].Text = cb.Patient.ID;
            sheetView.Cells[sheetView.RowCount - 1, 1].Text = cb.Patient.PID.PatientNO.TrimStart('0');
            sheetView.Cells[sheetView.RowCount - 1, 2].Text = cb.Patient.Name;
            sheetView.Cells[sheetView.RowCount - 1, 3].Text = cb.Patient.PVisit.PatientLocation.Dept.ID;
            sheetView.Cells[sheetView.RowCount - 1, 4].Text = cb.Patient.PVisit.PatientLocation.Bed.ID;
            sheetView.Cells[sheetView.RowCount - 1, 5].Text = cb.Patient.PVisit.PatientLocation.Dept.Name;
            sheetView.Cells[sheetView.RowCount - 1, 6].Text = cb.Patient.PVisit.AdmittingDoctor.ID;
            sheetView.Cells[sheetView.RowCount - 1, 7].Text = cb.Patient.PVisit.AdmittingDoctor.Name;
            sheetView.Cells[sheetView.RowCount - 1, 8].Text = cb.Patient.PVisit.OutTime.ToShortDateString();
            sheetView.Cells[sheetView.RowCount - 1, 9].Text = cb.IsCallback == "0" ? "未回收" : "已回收";
            sheetView.Cells[sheetView.RowCount - 1, 10].Text = cb.CallbackOper.ID;
            sheetView.Cells[sheetView.RowCount - 1, 11].Text = cb.CallbackOper.Name;
            if (cb.IsCallback == "1")
            {
                sheetView.Cells[sheetView.RowCount - 1, 12].Text = cb.CallbackOper.OperTime.ToShortDateString();
            }
            else
            {
                sheetView.Cells[sheetView.RowCount - 1, 12].Text = "";
            }

            sheetView.Rows[sheetView.RowCount - 1].Tag = cb;
            #region 设置默认显示为最后一行
            sheetView.ActiveRowIndex = sheetView.RowCount - 1;

            if (sheetView == this.neuFpEnter1_Sheet1)
            {
                this.neuFpEnter1.ShowRow(0, this.neuFpEnter1_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
            }
            else
            {
                this.neuFpEnter2.ShowRow(0, this.neuFpEnter2_Sheet1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);
            }
            #endregion
        }


        /// <summary>
        /// 判断当前选中那个fp 
        /// </summary>
        /// <param name="selectTabText"></param>
        /// <returns></returns>
        private FarPoint.Win.Spread.SheetView JudgeSelectedFp(string selectTabText)
        {
            FarPoint.Win.Spread.SheetView sheetView = null;// new FarPoint.Win.Spread.SheetView();
            FarPoint.Win.Spread.SheetView[] sv = new FarPoint.Win.Spread.SheetView[2]
                                                {
                                                    this.neuFpEnter1_Sheet1, this.neuFpEnter2_Sheet1
                                                };
            sheetView = (selectTabText == "未回收病案") ? sv[0] : sv[1];
            // sheetView.RemoveRows(0, sheetView.RowCount);
            //fp的行数初值为0
            //sheetView.RowCount = 0;

            sheetView.Columns[3].Visible = false;
            sheetView.Columns[0].Visible = false;
            //sheetView.Columns[4].Visible = false;
            sheetView.Columns[6].Visible = false;
            sheetView.Columns[10].Visible = false;

            return sheetView;
        }

        /// <summary>
        /// 综合查询已经回收或者未回收之病案
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="isOrNotCallBack">是否回收</param>
        private void QueryIsOrNotCallBack(DateTime begin, DateTime end, string isOrNotCallBack)
        {
            FarPoint.Win.Spread.SheetView sheetView = JudgeSelectedFp(this.neuTabControl1.SelectedTab.Text);
            //判断是回收科科室 还是未回收的科室
            string deptCode = isOrNotCallBack == "0" ? this.neuCmbDept.SelectedItem.ID.ToString() : this.neuCmbDept1.SelectedItem.ID.ToString();
            List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> listCallBack = callBackMgr.QueryCaseHistoryCallBackInfo(isOrNotCallBack, begin, end, deptCode);
            if (listCallBack == null)
            {
                return;
            }

            foreach (FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb in listCallBack)
            {
                //if (!this.ht.Contains(cb.Patient.ID))
                //{
                //    this.ht.Add(cb.Patient.ID, cb.Patient.Name);
                //}
                //else
                //{
                //    MessageBox.Show("住院号" + this.callBack.Patient.PID.PatientNO + "已在界面上显示"); 
                //}
                this.InitFp(cb, sheetView);
            }

        }

        /// <summary>
        /// 批量查询未回收病案时使用
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void QueryIsOrNotCallBack(DateTime begin, DateTime end)
        {
            FarPoint.Win.Spread.SheetView sheetView = JudgeSelectedFp(this.neuTabControl1.SelectedTab.Text);

            List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> listCallBack = callBackMgr.QueryCaseHistoryCallBackInfo(begin, end, this.neuCmbDept1.SelectedItem.ID.ToString());
            if (listCallBack == null)
            {
                return;
            }

            foreach (FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb in listCallBack)
            {
                this.InitFp(cb, sheetView);
            }

        }

        /// <summary>
        /// 根据住院号查询单个患者病案回收信息
        /// </summary>
        private void QueryIsOrNotCallBackByPatientNO(string patientNO,CaseType caseType)
        {
            FarPoint.Win.Spread.SheetView sv = JudgeSelectedFp(this.neuTabControl1.SelectedTab.Text);
            List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> cb = null;
            if (caseType == CaseType.CallBack)
            {
                cb = callBackMgr.QueryCaseHistorycallBackInfoByPatientNO(patientNO.TrimStart('0').PadLeft(10,'0'),"1");
            }
            else
            {
                cb = callBackMgr.QueryCaseHistorycallBackInfoByPatientNO(patientNO.TrimStart('0').PadLeft(10, '0'), "0");
            }
            
            if (cb == null || cb.Count == 0)
            {
                if (caseType == CaseType.UnCallBack)
                {
                    MessageBox.Show("未查找到住院号为【" + patientNO + "】的患者的信息");
                }
                else
                {
                    MessageBox.Show("住院号为【" + patientNO + "】的患者病案未回收");
                }
                return;
            }
            //患者多次住院 弹出对话框供选择 这里暂时没有控制能否在界面上显示两次同样的住院流水号
            else if (cb.Count > 1)
            {
                this.callBack = null;
                this.SelectListBox(cb);
                if (this.callBack == null)
                {
                    MessageBox.Show("没有查找到患者信息");
                    return;
                }

                this.InitFp(this.callBack, sv);
                return;
            }
            this.callBack = cb[0];
            //if (!this.ht.Contains(this.callBack.Patient.ID))
            //{
            //    this.ht.Add(this.callBack.Patient.ID, this.callBack.Name);
            //}
            //else
            //{
            //    MessageBox.Show("住院号为" + this.callBack.Patient.PID.PatientNO + "的病案已在界面上显示");
            //    return;
            //}
            //控制不能已回收病案不能再未回收病案中显示 反之，则不能在已回收中显示
            if (this.callBack.IsCallback == "0")
            {
                if (this.neuTabControl1.SelectedTab.Text == "已回收病案")
                {
                    MessageBox.Show("住院号为" +this.callBack.Patient.PID.PatientNO +"的病案尚未回收");
                    return;
                }
            }
            else
            {
                if(this.neuTabControl1.SelectedTab.Text == "未回收病案")
                {
                    MessageBox.Show("住院号为" + this.callBack.Patient.PID.PatientNO +"的病案已经回收");
                    return;
                }
            }

            this.InitFp(cb[0], sv);
        }


        /// <summary>
        /// 当查询到一个患者多次住院时 弹出小窗体供用户选择
        /// </summary>
        /// <param name="cb"></param>
        private void SelectListBox(List<FS.HISFC.Models.HealthRecord.CaseHistory.CallBack> cb)
        {
            listForm = new Form();
            //listBox = new ListBox();
            lv = new ListView();
            lv.Dock = DockStyle.Fill;
            lv.GridLines = true; //是否有表格显示
            lv.MultiSelect = false;
            lv.HeaderStyle = ColumnHeaderStyle.Nonclickable; //列标题是否可响应事件
            lv.View = View.Details; //项显示方式
            lv.Columns.Add("住院号", 80);
            lv.Columns.Add("姓名", 90);
            lv.Columns.Add("出院日期", 120);
            listForm.Size = new Size(300, 200);
            listForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            foreach (FS.HISFC.Models.HealthRecord.CaseHistory.CallBack callBack in cb)
            {
                //每一项为一个CallBack实体
                ListViewItem lvItem = new ListViewItem();
                lvItem.SubItems.Clear();
                lvItem.SubItems[0].Text =callBack.Patient.PID.PatientNO;
                lvItem.SubItems[0].ForeColor = Color.Blue;
                lvItem.SubItems.Add(callBack.Name);
                lvItem.SubItems.Add(callBack.Patient.PVisit.OutTime.ToString("yyyy年MM月dd日"));
                lvItem.Tag = callBack;
                lv.Items.Add(lvItem);
            }
            //listBox.Visible = true;
            //listBox.DoubleClick += new EventHandler(listBox_DoubleClick);  //方法还未实现
            //listBox.Show();
            lv.DoubleClick += new EventHandler(lv_DoubleClick);
            lv.KeyDown += new KeyEventHandler(lv_KeyDown);
            listForm.Controls.Add(lv);
            listForm.TopMost = true;
            //listForm.Show();
            listForm.ShowDialog();
            if (this.neuTabControl1.SelectedTab.Text == "未回收病案")
            {
                listForm.Location = this.neuTextBoxIsNotCBPatientNO.PointToScreen(new Point(this.neuTextBoxIsNotCBPatientNO.Width / 2 +
                            this.neuTextBoxIsNotCBPatientNO.Left, this.neuTextBoxIsNotCBPatientNO.Height + this.neuTextBoxIsNotCBPatientNO.Top));
            }
            else
            {
                listForm.Location = this.neuTextBoxPatientNO.PointToScreen(new Point(this.neuTextBoxPatientNO.Width / 2 +
                            this.neuTextBoxPatientNO.Left, this.neuTextBoxPatientNO.Height + this.neuTextBoxPatientNO.Top));
            }
            //默认选中第一个

            
        }

        /// <summary>
        /// 双击弹出的患者弹出窗体
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void lv_DoubleClick(object o, EventArgs e)
        {
            this.callBack = this.lv.FocusedItem.Tag as FS.HISFC.Models.HealthRecord.CaseHistory.CallBack;
            this.listForm.Hide();
            this.listForm.Dispose();
        }


        private void lv_KeyDown(object o, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.callBack = this.lv.FocusedItem.Tag as FS.HISFC.Models.HealthRecord.CaseHistory.CallBack;
                this.listForm.Hide();
                this.listForm.Dispose();
                Application.DoEvents();
            }
        }
        /// <summary>
        /// 回收病历函数 界面显示的患者全部回收
        /// </summary>
        /// <param name="cb"></param>
        private int CallBackCaseHistory(FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb)
        {
            //if (this.callBackMgr.UpdateCaseHistoryCallBackInfo(cb) < 0)
            //{
            //    MessageBox.Show("住院流水号为" + cb.Patient.ID + "患者的病案回收失败");
            //    return -1;
            //}
            if (this.callBackMgr.InsertCaseHistoryCallBackInfo(cb) < 0)
            {
                return -1;
            }
            return 0;
            //MessageBox.Show("病案回收成功");
        }

        /// <summary>
        /// 刷新查询到的病案信息 批量查询时刷新界面
        /// </summary>
        private void RefreshCaseHistory()
        {
            this.QueryIsOrNotCallBack(this.neudtIsNotFrom.Value, this.neudtIsNotTo.Value,
                this.neuTabControl1.SelectedTab.Text == "未回收病案" ? "0" : "1");
        }

        /// <summary>
        /// shuxin  dange huanzhe de shihou   单个查询的时候刷新 因为单个查询只要是有
        /// 结果的话就会给全局的callBack赋值 所以 这里假如callBack为空的话 就返回了
        /// 否则刷新界面
        /// </summary>
        private void RefreshCaseHistorySingle(string patientNO)
        {
            FarPoint.Win.Spread.SheetView sv = JudgeSelectedFp(this.neuTabControl1.SelectedTab.Text);
            //FS.HISFC.Models.HealthRecord.CaseHistory.CallBack callBack = callBackMgr.QueryCaseHistorycallBackInfoByInpatientNO(patientNO);

            if (this.callBack == null)
            {
                //MessageBox.Show("未查找到住院流水号为" + this.neuTextBoxIsNotCBPatientNO.Text + "的患者的信息");
                return;
            }

            this.InitFp(this.callBack, sv);
        }

        /// <summary>
        /// 显示查询结果控件
        /// </summary>
        /// <param name="c">结果控件</param>
        private int ShowControl(Control c)
        {
            //add 2011-6-27 by chengym  权宜之计的修改
            if (ISCaseCallBackLimits == false)
            {
                MessageBox.Show("您无分配“病案回收”权限！", "提示-病案管理 请删除 常数“CaseCallBackLimits”记录");
                return -1;
            }
            // end 
            try
            {
                form.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                if (form.Controls.Count > 0)
                {
                    form.Controls.Clear();
                }
                if (!form.Controls.Contains(c))
                {
                    form.Controls.Add(c);
                }
                form.Size = new Size(c.Width +10, c.Height + 50);
                //form.TopMost = true;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.LocationChanged += new EventHandler(form_LocationChanged);
                form.DragDrop +=new DragEventHandler(form_DragDrop);
                //form.Opacity = 0;
                form.Show();
                //for (double i = 0; i <= 1; i += 0.001)
                //{
                //    form.Opacity = i;
                //    Application.DoEvents();
                //}
                //form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("控件加载错误:" + c.Name + ex.Message.ToString());
                return -1;
            }

            return 0;
        }


        /// <summary>
        /// 病案回收率查询
        /// </summary>
        private void OnQueryCaseCallbackPercent()
        {

            this.ShowControl(caseCallbackPercent);
        }

        private void OnQueryTimeoutCaseInfo()
        {
            this.ShowControl(timeoutCase);
        }

        private void OnQueryCallbackNum()
        {
            this.ShowControl(callbackNum);
        }

        private void OnCallDateModifier()
        {
            //this.ShowControl(ucModifier);
        }

        private void OnCallDateModifierBatch()
        {
            //this.ShowControl(ucModifierBatch);
        }
        #endregion

        #region 事件

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("回收率", "病案回收率",FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolBarService.AddToolButton("超时病案明细", "超时回收的病案的明细信息", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolBarService.AddToolButton("回收数量查询", "按照科室和出院时间查询回收数量", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolBarService.AddToolButton("更改回收时间", "更改回收时间",FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarService.AddToolButton("批量更改回收时间", "批量更改回收时间", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarService.AddToolButton("查病案状态", "查病案状态", FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);//add by chengym 2011-6-24

            //return base.OnInit(sender, neuObject, param);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "回收率":
                    this.OnQueryCaseCallbackPercent();
                    break;
                case "超时病案明细":
                    this.OnQueryTimeoutCaseInfo();
                    break;
                case "回收数量查询":
                    this.OnQueryCallbackNum();
                    break;
                case "更改回收时间":
                    this.OnCallDateModifier();
                    break;
                case "批量更改回收时间":
                    this.OnCallDateModifierBatch();
                    break;
                case "查病案状态":
                    this.QueryCaseStoreAndCallBackState();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void form_LocationChanged(object sender, EventArgs e)
        {
            //form.Opacity = 0.5;
        }

        private void form_DragDrop(object sender, DragEventArgs e)
        {
            //form.Opacity = 1;
        }

        /// <summary>
        /// 未回收病历查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuIsNotQuery_Click(object sender, EventArgs e)
        {
            //this.ht.Clear();
            this.neuFpEnter1_Sheet1.RowCount = 0;
            if (this.neudtIsNotFrom.Value.Date < this.dtBeginUse)
            {
                if (MessageBox.Show("查询开始日期应该大于等于系统模块使用日期" + this.dtBeginUse.ToShortDateString() + "，否则查询结果将都为未回收状态！是否继续查询", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }
            this.QueryIsOrNotCallBack(this.neudtIsNotFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00), this.neudtIsNotTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59), "0");
            this.neuLblNoCallback.Text = "未回收数量：" + this.neuFpEnter1_Sheet1.RowCount;
        }

        /// <summary>
        /// 回收病历
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnCallBack_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确实回收界面上显示的所有病历吗？选择“是”将回收所有显示的病历.", "回收", 
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
            {
                return;
            }
            callBackMgr.ArraySpecifyDept = this.specifyDept;

            DateTime dtTemp = this.neuDtCallBackDate.Value;
            
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            callBackMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.neuFpEnter1_Sheet1.RowCount; i++)
            {
                callBack = this.neuFpEnter1_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.CaseHistory.CallBack;
                callBack.IsCallback = "1";//回收状态 1已回收 0 未回收 
                callBack.CallbackOper.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID;//回收人
                callBack.CallbackOper.OperTime = dtTemp; //回收日期
                callBack.IsDocument = "0";//归档状态
                callBack.DocumentOper.ID = "";//归档人
                callBack.DocumentOper.OperTime = System.DateTime.MinValue;//归档日期
                if (this.CallBackCaseHistory(callBack) < 0)
                {
                    if (this.callBackMgr.UpdateCaseHistoryCallBackInfo(callBack) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("住院流水号为" + callBack.Patient.ID + "患者的病案回收失败");
                        return;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("病案回收成功");
            this.neuFpEnter1_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 已经回收病历查询 批量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnQuery_Click(object sender, EventArgs e)
        {
            this.neuFpEnter2_Sheet1.RowCount = 0;
            //出院日期
            if (this.neuComboBox1.SelectedIndex == 0)
            {
                this.QueryIsOrNotCallBack(this.neuDtIsFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00), this.neudtIsTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59), "1");
            }
            else if (this.neuComboBox1.SelectedIndex == 1)
            {
                this.QueryIsOrNotCallBack(this.neuDtIsFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00), this.neudtIsTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
            }
            else
            {
                MessageBox.Show("您未选中任何一种查询条件！");
            }
            this.neuLblIsCallback.Text = "已回收数量：" + this.neuFpEnter2_Sheet1.RowCount;
            
        }

        /// <summary>
        /// 撤销病历回收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确实撤销回收界面上显示的所有病历吗？选择“是”将所有病历置为未回收状态.", "撤销回收", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
            {
                return;
            }

            if (this.neuFpEnter2_Sheet1.RowCount <= 0)
            {
                return;
            }

            string inpatientNO;// = this.neuFpEnter2_Sheet1.Cells[this.neuFpEnter2_Sheet1.ActiveRowIndex, 0].Text.ToString();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            callBackMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.neuFpEnter2_Sheet1.RowCount; i++)
            {
                inpatientNO = this.neuFpEnter2_Sheet1.Cells[i, 0].Text.ToString();
                if (this.callBackMgr.CancelCaseHistoryCallBackInfo(inpatientNO) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("住院流水号为" + inpatientNO + "患者的病案回收撤销失败");
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("病案回收撤销成功");
            //this.ht.Clear();
            this.neuFpEnter2_Sheet1.RowCount = 0;
            //this.RefreshCaseHistory();
        }

        /// <summary>
        /// 根据住院号单个查询 已回收病案信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnSingleQuery_Click(object sender, EventArgs e)
        {
            this.QueryIsOrNotCallBackByPatientNO(this.neuTextBoxPatientNO.Text,CaseType.CallBack);
            //this.RefreshCaseHistorySingle(this.neuTextBoxPatientNO.Text);
        }

        /// <summary>
        /// 回车 查询病人未回收病案信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBoxIsNotCBPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryIsOrNotCallBackByPatientNO(this.neuTextBoxIsNotCBPatientNO.Text,CaseType.UnCallBack);
                //this.RefreshCaseHistorySingle(this.neuTextBoxIsNotCBPatientNO.Text);
                this.neuTextBoxIsNotCBPatientNO.Text = "";
            }

        }
        /// <summary>
        /// 回车 查询病人已回收病案信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBoxPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryIsOrNotCallBackByPatientNO(this.neuTextBoxPatientNO.Text,CaseType.CallBack);
                //this.RefreshCaseHistorySingle(this.neuTextBoxIsNotCBPatientNO.Text);
                this.neuTextBoxIsNotCBPatientNO.Text = "";
            }
        }

        /// <summary>
        /// 单个病人病案回收信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void neuTextBoxPatientNO_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        this.QueryIsOrNotCallBackByPatientNO(this.neuTextBoxPatientNO.Text);
        //       // this.RefreshCaseHistorySingle(this.neuTextBoxPatientNO.Text);
        //    }
        //}

        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedTab.Text == "未回收病案")
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.PrintPage(this.neuPanel2.Left, this.neuPanel2.Top, this.neuPanel2);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.PrintPage(this.neuPanel3.Left, this.neuPanel3.Top, this.neuPanel3);
            }
            return base.OnPrint(sender, neuObject);
        }

        /// <summary>
        /// 清除界面显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnClear_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("您确实想清除界面显示的数据吗？", "清屏", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
            {
                return;
            }

            this.neuFpEnter1_Sheet1.RowCount = 0;
            //this.ht.Clear();

        }

        private void neuBtnClearN_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("您确实想清除界面显示的数据吗？", "清屏", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
            {
                return;
            }
            this.neuFpEnter2_Sheet1.RowCount = 0;
            //this.ht.Clear();
        }

        private void neuFpEnter1_CellDoubleClick_1(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == true || e.RowHeader == true)
            {
                return;
            }

            int rowNum = e.Row + 1;
            DialogResult dr = MessageBox.Show("是否删除第" + rowNum + "行的数据", "删除", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                return;
            }
            //this.ht.Remove(this.neuFpEnter1_Sheet1.Cells[e.Row, 0].Text.ToString().Trim());
            this.neuFpEnter1_Sheet1.RemoveRows(e.Row, 1);
            
            //this.neuFpEnter1_Sheet1.RowCount--;
        }

        private void neuFpEnter2_CellDoubleClick_1(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == true || e.RowHeader == true)
            {
                return;
            }

            int rowNum = e.Row + 1;
            DialogResult dr = MessageBox.Show("是否撤销回收第" + rowNum + "行的数据", "撤销回收", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                return;
            }
            string patientNO = this.neuFpEnter2_Sheet1.Cells[e.Row, 0].Text.ToString().Trim();
            if (!string.IsNullOrEmpty(patientNO))
            {
                if (this.callBackMgr.CancelCaseHistoryCallBackInfo(patientNO) < 0)
                {
                    MessageBox.Show("撤销回收失败！");
                    return;
                }
            }
            else
            {
                return;
            }
            //this.ht.Remove(this.neuFpEnter2_Sheet1.Cells[e.Row, 0].Text.ToString().Trim());
            this.neuFpEnter2_Sheet1.RemoveRows(e.Row, 1);

            //this.neuFpEnter2_Sheet1.RowCount--;
        }
        #endregion

        /// <summary>
        /// 更改病案回收时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuBtnModifier_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确实更改界面上显示的所有病历的回收时间吗？", "更改回收时间",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.No)
            {
                return;
            }

            if (this.neuFpEnter2_Sheet1.RowCount <= 0)
            {
                return;
            }


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            callBackMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.neuFpEnter2_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.HealthRecord.CaseHistory.CallBack callBack = this.neuFpEnter2_Sheet1.Rows[i].Tag as FS.HISFC.Models.HealthRecord.CaseHistory.CallBack;
                
                if (this.neuDtModifier.Visible)
                {
                    callBack.CallbackOper.OperTime = this.neuDtModifier.Value;
                }
                else if (this.ucAdvanceDays1.Visible)
                {
                    callBack.CallbackOper.OperTime = callBack.CallbackOper.OperTime.AddDays(-this.ucAdvanceDays1.AdvanceDays);
                }
                if (callBack.IsCallback == "0")
                {
                    callBack.IsCallback = "1";
                }
                if (this.callBackMgr.UpdateCaseHistoryCallBackInfo(callBack) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("住院号为" + callBack.Patient.PID.PatientNO + "患者的病案回收时间更新失败");
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("病案回收时间更新成功");
            this.neuFpEnter2_Sheet1.RowCount = 0;
        }

        private void neuCmbModifyStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.neuCmbModifyStyle.SelectedIndex == 0)
            //{
            //    this.ucAdvanceDays1.Visible = false;
            //    this.neuDtModifier.Visible = true;
            //}
            //else if(this.neuCmbModifyStyle.SelectedIndex == 1)
            //{
            //    this.ucAdvanceDays1.Visible = true;
            //}

            bool b = this.neuCmbModifyStyle.SelectedIndex == 0 ? (this.neuDtModifier.Visible = 
                !(this.ucAdvanceDays1.Visible = false)) : (this.neuDtModifier.Visible = !(this.ucAdvanceDays1.Visible = true));
        }

        private void QueryCaseStoreAndCallBackState()
        {
            //FS.HISFC.Components.HealthRecord.Case.frmCaseStoreQuery frm = new FS.HISFC.Components.HealthRecord.Case.frmCaseStoreQuery();
            //frm.ShowDialog();
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Title = "导出到Excel";
            saveFileDlg.CheckFileExists = false;
            saveFileDlg.CheckPathExists = true;
            saveFileDlg.DefaultExt = "*.xls";
            saveFileDlg.Filter = "(*.xls)|*.xls";

            DialogResult dr = saveFileDlg.ShowDialog();
            if (dr == DialogResult.Cancel || string.IsNullOrEmpty(saveFileDlg.FileName))
            {
                return;
            }

            neuFpEnter1.SaveExcel(saveFileDlg.FileName,FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
        }
    }


    /// <summary>
    /// 之所以这样 目的是重载OnClosing函数 使之不能析构窗体
    /// </summary>
    class MyForm : Form
    {
        public MyForm()
        {
            this.Opacity = 1;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //e.Cancel = true;
            this.Hide();
            e.Cancel = true;
            base.OnClosing(e);
        }

    }

}
