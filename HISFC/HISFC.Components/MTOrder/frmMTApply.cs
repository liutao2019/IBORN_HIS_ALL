using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.MTOrder
{
    public partial class frmMTApply : Form
    {
        public frmMTApply()
        {
            InitializeComponent();
            #region 事件绑定
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            this.neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);
            this.treeView1.BeforeSelect += new TreeViewCancelEventHandler(treeView1_BeforeSelect);
            this.treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            this.ucUserText1.OnDoubleClick += new EventHandler(ucUserText1_OnDoubleClick);
            #endregion


            #region 初始化图标
            btnSave.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.B保存);
            btnPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.D打印);
            btnSearch.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.C查找);
            btnExit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.T退出);
            #endregion
        }


        #region 列
        enum cols
        {
            /// <summary>
            /// 申请项目名称
            /// </summary>
            ItemName,
            /// <summary>
            /// 项目状态
            /// </summary>
            Status,
            /// <summary>
            /// 预约项目
            /// </summary>
            ApplyItem,
            /// <summary>
            /// 项目类型
            /// </summary>
            ApplyType,
            /// <summary>
            /// 执行医生
            /// </summary>
            DoctName,
            /// <summary>
            /// 执行科室
            /// </summary>
            DeptName,
            /// <summary>
            /// 预约日期
            /// </summary>
            ApplyDate,
            /// <summary>
            /// 预约时间段
            /// </summary>
            ApplyTime,
            /// <summary>
            /// 开立医生
            /// </summary>
            ApplyDoct,
            /// <summary>
            /// 预约序号
            /// </summary>
            ApplySeq
        }
        #endregion

        #region 域

        #region 属性
        /// <summary>
        /// 医嘱列表
        /// </summary>
        public List<FS.HISFC.Models.Order.Order> Orders { get { return orders; } set { orders = value; } }
        private List<FS.HISFC.Models.Order.Order> orders = new List<FS.HISFC.Models.Order.Order>();

        /// <summary>
        /// 当前病人信息
        /// </summary>
        public FS.HISFC.Models.RADT.Patient PatientInfo { get; set; }

        public string SelectedOrderID { get; set; }
        /// <summary>
        /// 检查是否有数据变更
        /// </summary>
        private bool IsChange
        {
            get
            {
                if (AppointmentList.Where(app => app.ApplyStatus == HISFC.Models.MedicalTechnology.EnumApplyStatus.未保存
                    || app.ApplyStatus == HISFC.Models.MedicalTechnology.EnumApplyStatus.准备取消).Count() > 0)
                    return true;
                else return false;
            }
        }
        /// <summary>
        /// 判断是否是门诊
        /// </summary>
        private bool IsClinic
        {
            get
            {
                return this.PatientInfo is HISFC.Models.Registration.Register;
                //return (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).CurrentGroup.Name.Contains("门诊");
            }
        }

        private bool isShowBtnSearch = false;

        private bool isUseCurrentUserAsDoctor = false;

        /// <summary>
        /// 是否显示查找按钮
        /// </summary>
        public bool IsShowBtnSearch { get { return isShowBtnSearch; } set { isShowBtnSearch = value; } }

        private int AllOrderDays = 30;
        #endregion

        #region 数据保存实体
        /// <summary>
        /// 申请历史信息列表
        /// </summary>
        private DataTable dsItems = new DataTable();
        private DataView dv;

        /// <summary>
        /// 医技列表
        /// </summary>
        private List<FS.HISFC.Models.Base.Const> MTList = new List<FS.HISFC.Models.Base.Const>();

        /// <summary>
        /// 预约列表
        /// </summary>
        private List<FS.HISFC.Models.MedicalTechnology.Appointment> AppointmentList = new List<FS.HISFC.Models.MedicalTechnology.Appointment>();
        #endregion

        #region 管理类
        /// <summary>
        /// 预约管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalTechnology.Appointment appMgr = new FS.HISFC.BizLogic.MedicalTechnology.Appointment();
        /// <summary>
        /// 病人信息管理类
        /// </summary>
        private HISFC.BizLogic.Registration.Register registerMgr = new HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 住院病人信息管理类
        /// </summary>
        private HISFC.BizLogic.RADT.InPatient inPatMgr = new HISFC.BizLogic.RADT.InPatient();
        /// <summary>
        /// 门诊医嘱单管理类
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.Order outPatientMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        /// <summary>
        /// 住院医嘱单管理类
        /// </summary>
        FS.HISFC.BizLogic.Order.Order inPatientMgr = new FS.HISFC.BizLogic.Order.Order();
        /// <summary>
        /// 诊断管理类
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
        #endregion

        #endregion

        #region 初始化

        private void frmMTApply_Load(object sender, EventArgs e)
        {
            #region 初始化控件
            //是否显示搜索按钮,默认不显示这个按钮
            btnSearch.Visible = isShowBtnSearch;
            //cbArrivalPattern.SelectedIndex = 0;
            #endregion

            InitDataTable();
            InitMT();
            InitArrivalPattern();
            if (PatientInfo != null)
            {
                InitPatientInfo();
            }
        }
        private void InitArrivalPattern()
        {
            ArrayList al = constantMgr.GetList("ArrivalPattern");
            if (al == null || al.Count < 1)
            {
                MessageBox.Show("获取[到达方式]列表时出错!" + constantMgr.Err, "提示");
                return;
            }
            this.cbArrivalPattern.AddItems(al);
        }
        private void InitOrders()
        {
            ArrayList al = null;
            //分类查询门诊和住院医嘱信息
            if (IsClinic)
            {
                //FS.HISFC.Models.Registration.Register reg = PatientInfo as FS.HISFC.Models.Registration.Register;
                //al = outPatientMgr.QueryOrder(reg.ID, reg.DoctorInfo.SeeNO.ToString());
                al = new ArrayList();
                ArrayList alOrder = outPatientMgr.QueryOrderByCardNOandClinicNO(PatientInfo.PID.CardNO, PatientInfo.ID);
                foreach (FS.HISFC.Models.Order.OutPatient.Order orderObject in alOrder)
                {
                    if (string.IsNullOrEmpty(orderObject.ConfirmOper.ID))
                    {
                        al.Add(orderObject);
                    }
                }
            }
            else
            {
                al = new ArrayList();
                ArrayList alOrder = inPatientMgr.QueryOrder(this.PatientInfo.ID);
                foreach (FS.HISFC.Models.Order.Inpatient.Order orderObj in alOrder)
                {
                    if (("0".Contains(orderObj.Status.ToString()) || "1".Contains(orderObj.Status.ToString())) && !orderObj.OrderType.IsDecompose//保存和已审核的
                        )
                    {
                        al.Add(orderObj);
                    }
                }
            }

            if (al == null)
            {
                MessageBox.Show("获取医嘱单出错" + outPatientMgr.Err);
                return;
            }
            if (al.Count < 1)
            {
                MessageBox.Show("未找到医嘱信息");
                return;
            }

            foreach (FS.HISFC.Models.Order.Order order in al)
            {
                if (MTList.Where(mt => mt.ID == GetItemType(order)).Count() > 0 && AppointmentList.Where(ap => ap.SequenceNo == order.ID).Count() < 1)
                    Orders.Add(order);
            }
            if (Orders.Count < 1)
            {
                //如果不是从树点击进来的,那么提示
                if (treeView1.SelectedNode == null)
                {
                    MessageBox.Show("没有找到可以进行预约项目");
                    return;
                }
            }
            if (Orders.Count > 0)
                InitMTTabControl();
        }
        /// <summary>
        /// 初始化DataTable
        /// </summary>
        private void InitDataTable()
        {
            dsItems = new DataTable("Templet");

            dsItems.Columns.AddRange(new DataColumn[]
			{
				new DataColumn(cols.ItemName.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.Status.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ApplyItem.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.ApplyType.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.DoctName.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.DeptName.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ApplyDate.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ApplyTime.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.ApplyDoct.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ApplySeq.ToString(),System.Type.GetType("System.String"))
			});
        }
        /// <summary>
        /// 获取医嘱项目类型代码
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private string GetItemType(FS.HISFC.Models.Order.Order order)
        {
            FS.HISFC.Models.Fee.Item.Undrug item = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
            if (item == null) return string.Empty;
            else
                return item.MinFee.ID;
        }
        /// <summary>
        /// 初始化TabControl
        /// </summary>
        private void InitMTTabControl()
        {
            for (int i = 1; i < neuTabControl1.TabCount; i++)
            {
                neuTabControl1.TabPages.RemoveAt(i);
            }
            if (Orders != null && Orders.Count > 0)
            {
                Orders.ForEach(
                    t =>
                    {
                        TabPage tp = new TabPage();

                        ucMTList MtArrange = new ucMTList();
                        MtArrange.Dock = DockStyle.Fill;
                        MtArrange.MedTechItem = MTList.Where(mt => mt.ID == GetItemType(t)).FirstOrDefault();
                        tp.Text = MtArrange.MedTechItem.Name;
                        MtArrange.MedTechOrder = t;
                        MtArrange.ApplyType = IsClinic ? Models.MedicalTechnology.EnumApplyType.Clinic : Models.MedicalTechnology.EnumApplyType.Hospital;
                        MtArrange.ApplyMedticalTechnology = new ucMTList.ApplyEvent(MedicalTechnology_Applying);
                        tp.Controls.Add(MtArrange);
                        neuTabControl1.TabPages.Add(tp);
                        if (!string.IsNullOrEmpty(SelectedOrderID) && SelectedOrderID == t.ID)
                            neuTabControl1.SelectedTab = tp;
                    }
                    );
            }
        }
        /// <summary>
        /// 初始化病人信息
        /// </summary>
        private void InitPatientInfo()
        {
            InitHistory();
            InitOrders();

            if (PatientInfo == null) return;
            ArrayList al = null;
            Models.MedicalTechnology.Appointment app=null;
            if (AppointmentList != null && AppointmentList.Count > 0)
            {
                app = AppointmentList.OrderByDescending(t => t.ApplyDate).FirstOrDefault();
            }
            else if (IsClinic)
            {
                lbCardNo.Text = PatientInfo.PID.CardNO;
                al = diagManager.QueryCaseDiagnoseForClinic(PatientInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            }
            else
            {
                lbCardNo.Text = PatientInfo.PID.PatientNO;
                al = diagManager.QueryCaseDiagnose(PatientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);
            }
            lbSex.Text = PatientInfo.Sex.ToString();
            txtTelephone.Text = PatientInfo.PhoneHome;
            lbName.Text = PatientInfo.Name;
            lbAge.Text = outPatientMgr.GetAge(PatientInfo.Birthday);
            if (al == null&&app==null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            string strDiag = string.Empty;
            if (app != null)
            {
                lbCardNo.Text = app.CardNo;
                strDiag = app.Diagnosis;
                txtMediaclHistory.Text = app.MedicalHistory;
            }
            else
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag += diag.DiagInfo.ICD10.Name + "/";
                }
            }
            txtDiagnoses.Text = strDiag.TrimEnd('/');
        }
        /// <summary>
        /// 初始化该病人的历史预约记录
        /// </summary>
        private void InitHistory()
        {
            string CardNo = string.Empty;
            if (IsClinic)
                CardNo = PatientInfo.PID.CardNO;
            else CardNo = PatientInfo.PID.PatientNO;
            AppointmentList = appMgr.GetHistoryByCardNoAndClinicNo(CardNo, PatientInfo.ID);
            AppList_Changed();
        }
        /// <summary>
        /// 清除信息
        /// </summary>
        private void Clear()
        {
            this.PatientInfo = null;
            this.Orders = new List<FS.HISFC.Models.Order.Order>();
            AppointmentList = new List<FS.HISFC.Models.MedicalTechnology.Appointment>();
            lbCardNo.Text = string.Empty;
            lbSex.Text = string.Empty;
            lbName.Text = string.Empty;
            lbAge.Text = string.Empty;
            txtDiagnoses.Text = string.Empty;
            txtMediaclHistory.Text = string.Empty;
            cbArrivalPattern.Text = "";
            txtTelephone.Text = string.Empty;
            dsItems.Clear();
            dsItems.AcceptChanges();
            List<TabPage> removeTag = new List<TabPage>();

            for (int i = 1; i < neuTabControl1.TabCount; i++)
            {
                removeTag.Add(neuTabControl1.TabPages[i]);
            }
            removeTag.ForEach(t => neuTabControl1.TabPages.Remove(t));
        }
        /// <summary>
        /// 生成今日历史记录
        /// </summary>
        private void InitMT()
        {
            this.treeView1.Nodes.Clear();
            TreeNode parent = new TreeNode(FS.FrameWork.Management.Language.Msg("预约记录"));
            this.treeView1.ImageList = this.treeView1.deptImageList;
            parent.ImageIndex = 5;
            parent.SelectedImageIndex = 5;
            this.treeView1.Nodes.Add(parent);

            ArrayList al = constantMgr.GetList("MT#MINFEE");
            if (al == null)
            {
                MessageBox.Show("获取医技列表时出错!" + constantMgr.Err, "提示");
                return;
            }
            List<FS.HISFC.Models.MedicalTechnology.Appointment> AppList = appMgr.GetDoctHistory(FS.FrameWork.Management.Connection.Operator.ID, 1);
            if (AppList == null)
            {
                MessageBox.Show("获取历史记录时出错!" + constantMgr.Err, "提示");
                return;
            }
            foreach (FS.HISFC.Models.Base.Const MedTech in al)
            {
                TreeNode node = new TreeNode();
                node.Text = MedTech.Name;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 1;
                node.Tag = MedTech;

                parent.Nodes.Add(node);
                MTList.Add(MedTech);

                AppList.Where(app =>
                {
                    bool Conditions = false;
                    if (IsClinic)
                        Conditions = app.OrderType == FS.HISFC.Models.MedicalTechnology.EnumApplyType.Clinic;
                    else
                        Conditions = app.OrderType == FS.HISFC.Models.MedicalTechnology.EnumApplyType.Hospital;
                    return app.MTCode == MedTech.ID && Conditions;
                }).ToList().ForEach(app =>
                {
                    bool isContains = false;
                    foreach (TreeNode tn in node.Nodes)
                    {
                        if (tn.Text == app.Name)
                        {
                            isContains = true;
                            break;
                        }
                    }
                    if (isContains == false)
                    {
                        TreeNode patientNode = new TreeNode(app.Name);
                        patientNode.Tag = app;
                        node.Nodes.Add(patientNode);
                    }
                });
            }
            parent.ExpandAll();
        }

        private void AppList_Changed()
        {
            try
            {
                dsItems.Clear();
                AppointmentList.ForEach(app =>
                {
                    dsItems.Rows.Add(new object[]
                    {
                    app.ItemName,
                    app.ApplyStatus.ToString(),
                    app.MTName,
                    app.TypeName,
                    app.ExecDoctName,
                    app.ExecDeptName,
                    app.ArrangeDate.ToString("yyyy-MM-dd"),
                    app.ArrangeTime,
                    app.DoctName,
                    app.OrderNo
                    });
                });
            }
            catch (Exception e)
            {
                MessageBox.Show("历史信息生成DataTable时出错!" + e.Message, "提示");
                return;
            }
            dsItems.AcceptChanges();
            dv = dsItems.DefaultView;
            neuSpread1_Sheet1.DataSource = dv;
            SetFpFormat();
        }
        /// <summary>
        /// 设置Farpoint显示格式
        /// </summary>
        private void SetFpFormat()
        {

            this.neuSpread1_Sheet1.Columns[(int)cols.ItemName].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Status].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ApplyItem].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ApplyType].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)cols.DoctName].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.DeptName].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ApplyDate].Width = 70F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ApplyTime].Width = 90F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ApplyDoct].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ApplySeq].Width = 40F;
        }


        #endregion

        #region 操作
        private bool Print(List<FS.HISFC.Models.MedicalTechnology.Appointment> NewApp)
        {
            try
            {
                NewApp.ForEach(a =>
                    {
                        FS.HISFC.Components.MTOrder.OrderPrint.ucApplyOrderPrint print = new FS.HISFC.Components.MTOrder.OrderPrint.ucApplyOrderPrint();
                        print.SetValues(a);
                    });
            }
            catch { return false; }
            return true;
        }
        /// <summary>
        /// 保存
        /// </summary>
        private bool Save()
        {
            List<FS.HISFC.Models.MedicalTechnology.Appointment> NewApp = AppointmentList.Where(app => app.ApplyStatus == FS.HISFC.Models.MedicalTechnology.EnumApplyStatus.未保存).ToList();
            List<FS.HISFC.Models.MedicalTechnology.Appointment> CancledApp = AppointmentList.Where(app => app.ApplyStatus == FS.HISFC.Models.MedicalTechnology.EnumApplyStatus.准备取消).ToList();

            if (NewApp.Count < 1 && CancledApp.Count < 1)
            {
                MessageBox.Show("没有需要保存的信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            try
            {
                if (!Add(NewApp))
                {
                    return false;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.appMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (!Cancle(CancledApp))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("取消失败,原因:" + appMgr.Err);
                    return false;
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                }

                MessageBox.Show("保存成功");
                return true;
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("保存失败:" + e.Message, "提示");
                return false;
            }
        }

        /// <summary>
        /// 添加预约
        /// </summary>
        /// <param name="NewApp"></param>
        /// <returns></returns>
        private bool Add(List<FS.HISFC.Models.MedicalTechnology.Appointment> NewApp)
        {
            DateTime operDate = appMgr.GetDateTimeFromSysDateTime();
            foreach (FS.HISFC.Models.MedicalTechnology.Appointment app in NewApp)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                app.OperCode = FS.FrameWork.Management.Connection.Operator.ID;
                app.OperDate = operDate;

                if (appMgr.Insert(app) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("【" + app.ItemName + "】预约失败,原因:" + appMgr.Err);
                    return false;
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
            if (MessageBox.Show("是否打印预约单?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Print(NewApp);
            }
            return true;
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="CancledApp"></param>
        /// <returns></returns>
        private bool Cancle(List<FS.HISFC.Models.MedicalTechnology.Appointment> CancledApp)
        {
            DateTime operDate = appMgr.GetDateTimeFromSysDateTime();
            foreach (FS.HISFC.Models.MedicalTechnology.Appointment app in CancledApp)
            {
                app.OperCode = FS.FrameWork.Management.Connection.Operator.ID;
                app.OperDate = operDate;
                if (appMgr.Cancle(app, Models.Base.CancelTypes.Canceled) == -1)
                {
                    return false;
                }
            }
            return true;
        }
        private string TranslateOrderType(Models.MedicalTechnology.EnumApplyType applyType)
        {
            switch (applyType)
            {
                case Models.MedicalTechnology.EnumApplyType.Clinic: return "门诊";
                default: return "住院";
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// TabControl里的预约双击事件
        /// </summary>
        /// <param name="arrange"></param>
        /// <param name="order"></param>
        private void MedicalTechnology_Applying(FS.HISFC.Models.MedicalTechnology.Arrange arrange, FS.HISFC.Models.Order.Order order)
        {
            if (arrange.EndTime <= DateTime.Now)
            {
                MessageBox.Show("该排班已过期,无法预约", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtMediaclHistory.Text))
            {
                MessageBox.Show("请输入该病人的病史与体征", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtTelephone.Text))
            {
                MessageBox.Show("请输入该病人的联系电话", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cbArrivalPattern.Text))
            {
                MessageBox.Show("请选择该病人的到达方式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #region 检查该项目是否已经预约过了
            if (AppointmentList.Where(t => t.SequenceNo == order.ID).Count() > 0)
            {
                MessageBox.Show("该项目已预约,无需再预约", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion
            FS.HISFC.Models.MedicalTechnology.Appointment app = new FS.HISFC.Models.MedicalTechnology.Appointment();
            #region 赋值

            app.ID = appMgr.GetSequence("MedicalTechnology.Appointment.ID");
            if (IsClinic)
            {
                app.CardNo = PatientInfo.PID.CardNO;
                app.OrderType = Models.MedicalTechnology.EnumApplyType.Clinic;
                app.BedNo = "无";
            }
            else 
            {
                app.CardNo = PatientInfo.PID.PatientNO;
                app.OrderType = Models.MedicalTechnology.EnumApplyType.Hospital;
                app.BedNo = (this.PatientInfo as HISFC.Models.RADT.PatientInfo).PVisit.PatientLocation.Bed.Name;
            }

            app.ClinicCode = PatientInfo.ID;
            app.Name = PatientInfo.Name;
            app.Sex = PatientInfo.Sex.ToString();
            app.Telephone = txtTelephone.Text.Trim();
            app.ArrivalPattern = cbArrivalPattern.Text;
            app.Age = lbAge.Text;
            app.SequenceNo = order.ID;
            app.ItemCode = order.Item.ID;
            app.ItemName = order.Item.Name;
            app.ItemCost = order.Item.Price * order.Item.Qty;
            app.MTCode = arrange.ItemCode;
            app.MTName = arrange.ItemName;
            app.TypeCode = arrange.TypeCode;
            app.TypeName = arrange.TypeName;
            app.ArrangeID = arrange.ID;
            app.ArrangeDate = arrange.SeeDate;
            app.BeginTime = arrange.BeginTime;
            app.EndTime = arrange.EndTime;
            app.OrderNo = appMgr.GetOrderNo(arrange);
            app.MedicalHistory = txtMediaclHistory.Text;
            app.Diagnosis = txtDiagnoses.Text;
            app.ExecDeptCode = order.ExeDept.ID;
            app.ExecDeptName = order.ExeDept.Name;
            app.ExecDoctCode = arrange.DoctCode;
            app.ExecDoctName = arrange.DoctName;
            if (isUseCurrentUserAsDoctor)
            {
                app.DeptCode = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                app.DeptName = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.Name;
                app.DoctCode = FS.FrameWork.Management.Connection.Operator.ID;
                app.DoctName = FS.FrameWork.Management.Connection.Operator.Name;
            }
            else
            {
                string deptName = string.Empty;
                FS.HISFC.Models.Base.Department dept = deptMgr.GetDeptmentById(order.ReciptDept.ID);
                if (dept != null)
                    deptName = dept.Name;
                app.DeptCode = order.ReciptDept.ID;
                app.DeptName = deptName;
                app.DoctCode = order.ReciptDoctor.ID;
                app.DoctName = order.ReciptDoctor.Name;
            }

            app.ApplyDate = DateTime.Now;
            app.IsValid = true;

            app.ApplyStatus = Models.MedicalTechnology.EnumApplyStatus.未保存;

            #endregion

            AppointmentList.Add(app);
            AppList_Changed();
            MessageBox.Show("预约成功");
            this.neuTabControl1.SelectedIndex = 0;
        }
        /// <summary>
        /// 保存预约单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
                this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 退出预约界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        /// <summary>
        /// 重新打印申请单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (neuSpread1_Sheet1.ActiveRowIndex > AppointmentList.Count || AppointmentList.Count==0)
            {
                MessageBox.Show("没有找到该项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (AppointmentList[neuSpread1_Sheet1.ActiveRowIndex].ApplyStatus == Models.MedicalTechnology.EnumApplyStatus.未缴费 ||
                AppointmentList[neuSpread1_Sheet1.ActiveRowIndex].ApplyStatus == Models.MedicalTechnology.EnumApplyStatus.已缴费)
            {
                List<HISFC.Models.MedicalTechnology.Appointment> rePrintList = new List<HISFC.Models.MedicalTechnology.Appointment>();
                rePrintList.Add(AppointmentList[neuSpread1_Sheet1.ActiveRowIndex]);
                if (!Print(rePrintList))
                    MessageBox.Show("打印失败");
            }
            else
            {
                MessageBox.Show("该项目不可打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// 双击取消预约
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            switch (AppointmentList[neuSpread1_Sheet1.ActiveRowIndex].ApplyStatus)
            {
                case HISFC.Models.MedicalTechnology.EnumApplyStatus.已缴费:
                    {
                        MessageBox.Show("已缴费项目不允许取消, 请先退费再进行该操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                case HISFC.Models.MedicalTechnology.EnumApplyStatus.已执行:
                    {
                        MessageBox.Show("已执行项目不允许取消", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                case HISFC.Models.MedicalTechnology.EnumApplyStatus.未缴费:
                    {
                        //离执行时间不足24小时
                        bool isExcuteTime = AppointmentList[neuSpread1_Sheet1.ActiveRowIndex].BeginTime < DateTime.Now.AddDays(1);

                        //离项目开单不足24小时
                        bool isOpenTime = AppointmentList[neuSpread1_Sheet1.ActiveRowIndex].ApplyDate > DateTime.Now.AddDays(-1);



                        if (isExcuteTime && !isOpenTime)//开单超过24小时且离执行时间不足24小时的不能取消预约
                        {
                            MessageBox.Show("此项目离执行时间不足24小时,不能取消预约!!\r\n若仍需取消预约,请联系【"
                                + AppointmentList[neuSpread1_Sheet1.ActiveRowIndex].ExecDeptName + "】",
                                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        //if (!isOpenTime)
                        //{
                        //    MessageBox.Show("此项目开单时间已超24小时,不能取消预约!!\r\n若仍需取消预约,请联系【"
                        //        + AppointmentList[neuSpread1_Sheet1.ActiveRowIndex].ExecDeptName + "】",
                        //        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}

                        //if (AppointmentList[neuSpread1_Sheet1.ActiveRowIndex].BeginTime < DateTime.Now.AddDays(1))
                        //{
                        //    MessageBox.Show("此项目离执行时间不足24小时,不能取消预约!!\r\n若仍需取消预约,请联系【"
                        //        + AppointmentList[neuSpread1_Sheet1.ActiveRowIndex].ExecDeptName + "】",
                        //        "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}



                        if (DialogResult.Yes == MessageBox.Show("是否取消该项目?", "提示", MessageBoxButtons.YesNo))
                        {
                            AppointmentList[neuSpread1_Sheet1.ActiveRowIndex].ApplyStatus = HISFC.Models.MedicalTechnology.EnumApplyStatus.准备取消;
                        }
                        else return;
                        break;
                    }
                case HISFC.Models.MedicalTechnology.EnumApplyStatus.未保存:
                    {
                        AppointmentList.RemoveAt(neuSpread1_Sheet1.ActiveRowIndex);
                        break;
                    }
            }

            AppList_Changed();
            MessageBox.Show("取消预约成功");
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            txtCardNo.Focus();
            if (IsChange == true)
            {
                e.Cancel = true;
                MessageBox.Show("请保存后再执行该动作!", "提示");
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //如果父结点就是根结点,那么不重置病人信息
            if (treeView1.SelectedNode.Parent == null || treeView1.SelectedNode.Parent.Text == "预约记录")
                return;

            Clear();
            FS.HISFC.Models.MedicalTechnology.Appointment app = this.treeView1.SelectedNode.Tag as FS.HISFC.Models.MedicalTechnology.Appointment;
            if (IsClinic)
                this.PatientInfo = registerMgr.GetByClinic(app.ClinicCode);
            else this.PatientInfo = inPatMgr.QueryPatientInfoByInpatientNO(app.ClinicCode);
            if (PatientInfo == null)
            {
                return;
            }
            InitPatientInfo();
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (IsChange == true)
                {
                    MessageBox.Show("请保存后再执行该动作!", "提示");
                    return;
                }
                string cardNo = txtCardNo.Text;
                Clear();
                cardNo = cardNo.PadLeft(10, '0');
                List<HISFC.Models.MedicalTechnology.Appointment> his = appMgr.GetHistory(cardNo, 0);
                frmSelectHistory newFrm = new frmSelectHistory();
                newFrm.appHist = his;
                newFrm.Show();
                newFrm.Hide();
                newFrm.Location = groupBox1.PointToScreen(new Point(txtCardNo.Location.X, txtCardNo.Location.Y + txtCardNo.Height));
                if (newFrm.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(newFrm.ClinicNo))
                    return;

                if (IsClinic)
                    this.PatientInfo = registerMgr.GetByClinic(newFrm.ClinicNo);
                else this.PatientInfo = inPatMgr.QueryPatientInfoByInpatientNO(newFrm.ClinicNo);
                if (PatientInfo == null)
                {
                    return;
                }
                InitPatientInfo();

            }
        }
        /// <summary>
        /// 快捷输入体征
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucUserText1_OnDoubleClick(object sender, EventArgs e)
        {
            FS.HISFC.Models.Base.UserText userText = this.ucUserText1.GetSelectedNode().Tag as FS.HISFC.Models.Base.UserText;
            if (userText != null)
            {
                this.txtMediaclHistory.Text = userText.Text;
            }
        }

        private void addMenu_Click(object sender, EventArgs e)
        {
            ucUserTextControl u = new ucUserTextControl();
            FS.HISFC.Models.Base.UserText uT = new FS.HISFC.Models.Base.UserText();
            uT.Text = this.txtMediaclHistory.Text;
            if (string.IsNullOrEmpty(uT.Text.ToString()))
            {
                uT.Text = "MS999";
            }
            //uT.Name = this.userText.Name;
            uT.RichText = "";
            u.UserText = uT;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(u);
            this.ucUserText1.RefreshList();
        }

        /// <summary>
        /// 按门诊/住院流水号查找病人信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSearch_Click(object sender, EventArgs e)
        {
            Clear();
            FS.HISFC.Components.MTOrder.Forms.frmSelectPatient frm = new FS.HISFC.Components.MTOrder.Forms.frmSelectPatient();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Yes)
            {
                if (frm.SelectedType == Models.MedicalTechnology.EnumApplyType.Clinic)
                    this.PatientInfo = registerMgr.GetByClinic(frm.SelectedClinicNo);
                else this.PatientInfo = inPatMgr.QueryPatientInfoByInpatientNO(frm.SelectedClinicNo);

                if (this.PatientInfo == null)
                {
                    MessageBox.Show("找不到病人信息","提示");
                    return;
                }

                InitPatientInfo();
            }
        }
        #endregion


    }
}
