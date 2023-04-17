using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.Components.DCP.Classes;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// ucDiseaseReport<br></br>
    /// [功能描述: 报卡信息uc]<br></br>
    /// [创 建 者: zj]<br></br>
    /// [创建时间: 2008-09-17]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucDiseaseReport : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucDiseaseReport()
            : this(false, FS.SOC.HISFC.DCP.Enum.PatientType.O)
        {

        }

        public ucDiseaseReport(bool isInit,FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            InitializeComponent();
            this.PatientType = patientType;
            if (isInit)
            {
                this.Init();
            }
            this.isInit = isInit;
            this.ucDiseaseQuery1.ShowInfo += new ucDiseaseQuery.SelectNode(ucDiseaseQuery1_ShowInfo);
            this.ucDiseaseInfo1.AdditionEvent += new ucDiseaseInfo.AddAddtion(ucDiseaseInfo1_AdditionEvent); 
        }

        public ucDiseaseReport(bool isInit, ArrayList alReport)
        {
            InitializeComponent();
            //ucDiseaseQuery.Init函数会根据AlReport在界面显示的时候把报告卡显示到treeview，而且保证clear的时候不被清除
            this.ucDiseaseQuery1.AlReport = alReport;
            this.PatientType = FS.SOC.HISFC.DCP.Enum.PatientType.O;
            if (isInit)
            {
                this.Init();
            }
            this.isInit = isInit;
            this.ucDiseaseQuery1.ShowInfo += new ucDiseaseQuery.SelectNode(ucDiseaseQuery1_ShowInfo);
            this.ucDiseaseInfo1.AdditionEvent += new ucDiseaseInfo.AddAddtion(ucDiseaseInfo1_AdditionEvent);
        }

        #region 变量
        private bool isInit = false;

        private FS.SOC.HISFC.BizLogic.DCP.DiseaseReport diseaseMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();
        /// <summary>
        /// 操作类型
        /// </summary>
        private OperType operType;

        /// <summary>
        /// 是否预报预防控制权限
        /// </summary>
        private bool isCDCPriv = false;

        /// <summary>
        /// 疾控权限相关科室
        /// </summary>
        List<FS.FrameWork.Models.NeuObject> cdcPrivDeptList = new List<FS.FrameWork.Models.NeuObject>();

        public FS.HISFC.DCP.Object.CommonReport CommonReport
        {
            get
            {
                return this.ucDiseaseQuery1.CommonReport;
            }
            set
            {
                this.ucDiseaseQuery1.CommonReport = value;
            }
        }

        /// <summary>
        /// 附卡接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCPInterface.IAddition iAdditionReport;

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();

        #endregion

        #region 属性

        /// <summary>
        /// 操作患者的类型
        /// </summary>
        public FS.SOC.HISFC.DCP.Enum.PatientType PatientType
        {
            get
            {
                return this.ucDiseaseQuery1.PatientType;
            }
            set
            {
                this.ucDiseaseQuery1.PatientType = value;
            }
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return this.ucDiseaseQuery1.Patient;
            }
            set
            {
                this.ucDiseaseQuery1.Patient = value;
                if (value != null)
                {
                    ucDiseaseQuery1_ShowInfo(value);
                }
            }
        }

        /// <summary>
        /// 设置患者查询的默认天数
        /// </summary>
        [Category("参数设置"), Description("设置患者查询的默认天数")]
        public int Days
        {
            get
            {
                return this.ucDiseaseQuery1.Days;
            }
            set
            {
                this.ucDiseaseQuery1.Days = value;
            }
        }

        /// <summary>
        /// 指定疾病编码
        /// </summary>
        public string InfectCode
        {
            get { return this.ucDiseaseInfo1.InfectCode; }
            set { this.ucDiseaseInfo1.InfectCode = value; }
        }

        /// <summary>
        /// 是否需要附卡
        /// </summary>
        private bool isNeedAdd = false;

        /// <summary>
        /// 是否需要附卡
        /// </summary>
        public bool IsNeedAdd
        {
            get
            {
                return isNeedAdd;
            }
            set
            {
                isNeedAdd = value;
            }
        }

        private FS.SOC.HISFC.DCP.Enum.ReportOperResult reportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.Other;

        /// <summary>
        /// 报卡错作结果
        /// </summary>
        public FS.SOC.HISFC.DCP.Enum.ReportOperResult ReportOperResult
        {
            get
            {
                return this.reportOperResult;
            }
            set
            {
                this.reportOperResult = value;
            }
        }

        #endregion

        #region 工具栏
        /// <summary>
        /// ToolBar服务类
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 工具栏的初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("新建", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

            toolBarService.AddToolButton("合格", "Eligible", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z执行, true, false, null);
            toolBarService.AddToolButton("不合格", "UnEligible", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z注销, true, false, null);

            toolBarService.AddToolButton("订正", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X下一个, true, false, null);
            toolBarService.AddToolButton("恢复", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M默认, true, false, null);
            toolBarService.AddToolButton("作废", "Cancel", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            toolBarService.AddToolButton("续填", "Cancel", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);

            toolBarService.AddToolButton("删除", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.ToolStrip_ItemClicked(e.ClickedItem.Text);
        }


        /// <summary>
        ///  单击工具栏按钮是调用,写出函数,以便被外部调用
        /// </summary>
        /// <param name="clickedItemText"></param>
        public void ToolStrip_ItemClicked(string clickedItemText)
        {
            switch (clickedItemText)
            {
                case "新建":
                    if (this.SetOperType(OperType.新增) == 1)
                    {
                        this.Clear();
                    }
                    break;
                case "续填":
                    if (this.SetOperType(OperType.续填) == 1)
                    {
                        this.CreateNextReport();
                    }
                    break;
                case "合格":
                    this.SetOperType(OperType.合格);
                    break;
                case "不合格":
                    this.SetOperType(OperType.不合格);
                    break;
                case "订正":
                    if (this.SetOperType(OperType.订正) == 1)
                    {
                        this.OnCorrect();
                    }
                    break;
                case "恢复":
                    this.SetOperType(OperType.恢复);
                    break;
                case "作废":
                    this.SetOperType(OperType.作废);
                    break;
                case "删除":
                    if (this.SetOperType(OperType.删除) == 1)
                    {
                        this.ucDiseaseQuery1.DeleteReport();
                    }
                    break;
                case "查询":
                    this.ucDiseaseQuery1.AlReport.Clear();
                    this.ucDiseaseQuery1.Query();
                    break;
                case "保存":
                    this.OnSave(new object(), new object());
                    break;
                case "打印":
                    this.OnPrint(null, null);
                    break;
                case "退出":
                    //this.reportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.Cancel;
                    break;
                case "传染病知识":
                    this.Help();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 方法

        public int Init()
        {
            DateTime sysdate = this.diseaseMgr.GetDateTimeFromSysDateTime();

            this.PreArrange();

            this.ucDiseaseInfo1.Init(sysdate);
            this.ucPatientInfo1.Init(sysdate);
            this.ucReportButtom1.Init(sysdate);
            this.ucReportTop1.Init(sysdate);
            this.ucDiseaseQuery1.Init();


            // {2671947C-3F17-4eee-A72F-1479665EEB16}将界面中报告时间设置为不可修改
            //this.ucReportButtom1.SetReportTime = this.isCDCPriv;
            return 1;
        }

        public int QueryByDoctorNO(FS.SOC.HISFC.DCP.Enum.ReportState state)
        {
            this.ucDiseaseQuery1.QueryByDoctorNO(state,this.diseaseMgr.Operator.ID);
            return 1;
        }

        public int QueryByDeptNO(FS.SOC.HISFC.DCP.Enum.ReportState state)
        {
            return this.ucDiseaseQuery1.QueryDeptReport(FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible, ((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID);
        }

        /// <summary>
        /// 设置操作员的操作类型
        /// </summary>
        /// <param name="operType"></param>
        /// <returns>操作类型结果 1 成功 -1 失败</returns>
        private int SetOperType(OperType operType)
        {
            if (operType == OperType.合格 || operType == OperType.不合格 || operType == OperType.恢复)
            {
                if (PreArrange() == -1)
                {
                    MessageBox.Show("您无预控审核权限，无法进行相应操作", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            this.operType = operType;
             
            if (this.operType == OperType.新增||this.operType== OperType.查询 || this.operType== OperType.续填)
            {
                return 1;
            }

            FS.HISFC.DCP.Object.CommonReport report = null;
            if (this.CommonReport != null)
            {
                report = this.CommonReport.Clone();
            }
            else
            {
                if (this.operType == OperType.合格 || this.operType == OperType.不合格 || this.operType == OperType.订正 || this.operType == OperType.恢复 || this.operType == OperType.作废 || this.operType == OperType.删除)
                {
                    this.MyMessageBox("请选择报卡进行操作！", "提示>>");
                    return -1;
                }
            }

            if (this.GetValue(ref report) == -1)
            {
                return -1;
            }
            if (IsAllowOper(report, operType) == false)
            {
                return -1;
            }

            if (report == null || string.IsNullOrEmpty(report.ID) == true)
            {
                if (this.operType == OperType.保存)     //对于新建的单独处理
                {
                    return 1;
                }
                return -1;
            }
            
            switch (this.operType)
            {
                case OperType.合格:
                    this.UpdateReportState(report, FS.SOC.HISFC.DCP.Enum.ReportState.Eligible);
                    break;
                case OperType.不合格:
                    if (this.ucReportButtom1.GetValue(ref report, OperType.不合格) == -1)
                    {
                        return -1;
                    }
                    this.UpdateReportState(report, FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible);
                    break;
                case OperType.作废:
                    
                    if (this.diseaseMgr.Operator.ID == report.Oper.ID)
                    {
                        this.UpdateReportState(report, FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel);
                    }
                    else
                    {
                        this.UpdateReportState(report, FS.SOC.HISFC.DCP.Enum.ReportState.Cancel);
                    }
                    break;
                case OperType.恢复:
                    if (this.diseaseMgr.Operator.ID == report.Oper.ID)
                    {
                        this.UpdateReportState(report, FS.SOC.HISFC.DCP.Enum.ReportState.New);
                    }
                    else
                    {
                        this.UpdateReportState(report, FS.SOC.HISFC.DCP.Enum.ReportState.Cancel);
                    }
                    break;
            }

            return 1;
        }

        /// <summary>
        /// 订正方法
        /// </summary>
        public void OnCorrect()
        {
            if (this.SaveCorrectReport() == 0)
            {
                this.Clear();

                this.ucDiseaseQuery1.QueryByCache();
                this.SetOperType(OperType.查询);

                // 下诊断后是否填写了报告卡，“保存成功”不可少
                this.Text += "--保存成功";
            }
        }

        public int Save()
        {
            FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
            if (this.CommonReport != null)
            {
                report = this.CommonReport.Clone();
            }

            #region 取值

            if (this.GetValue(ref report) == -1)
            {
                return -1;
            }
            report.State = Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New);

            #endregion

            #region 数据保存

            if (this.ucDiseaseInfo1.InfectionClassEnable == true
              && MessageBox.Show(this, "您选择了【" + report.Disease.Name + "】\n保存后电子报卡由系统自动上传【疾病预防科】\n确认保存吗？", "温馨提示>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
            {
                return -1;
            }


            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存,请稍候....");
            Application.DoEvents();

            //报告编号为空 作为新卡插入数据库

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.diseaseMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (string.IsNullOrEmpty(report.ID) && string.IsNullOrEmpty(report.ReportNO))
            {
                #region 新卡插入处理

                //如果是订正 需要更新原卡
                if (this.diseaseMgr.InsertCommonReport(report) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    report.ID = string.Empty;
                    report.ReportNO = string.Empty;
                    this.MyMessageBox("报告卡保存失败>>" + this.diseaseMgr.Err, "err");
                    return -1;
                }

                //附卡保存
                if (IsNeedAdd)
                {
                    if (this.UpdateAdditionInfo(this.operType, report) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("存储附卡信息失败"));
                        return -1;
                    }
                }

                #endregion
            }
            else
            {
                #region 旧卡更新处理
                //从数据库里面获取

                FS.HISFC.DCP.Object.CommonReport mainreport = this.diseaseMgr.GetCommonReportByID(report.ID);
                if (mainreport == null)
                {
                    mainreport = this.diseaseMgr.GetCommonReportByNO(report.ReportNO); //ID查不到了查reportNO
                    if (mainreport == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.MyMessageBox("报告卡保存失败>>" + this.diseaseMgr.Err, "err");
                        return -1;
                    }
                }

                report.ID = mainreport.ID;
                report.ReportNO = mainreport.ReportNO;
                report.CorrectFlag = mainreport.CorrectFlag;
                report.CorrectReportNO = mainreport.CorrectReportNO;
                report.CorrectedReportNO = mainreport.CorrectedReportNO;
                report.ExtendInfo3 = mainreport.ExtendInfo3;
                if (this.diseaseMgr.UpdateCommonReport(report.Clone()) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.MyMessageBox("报告卡保存失败>>" + this.diseaseMgr.Err, "err");
                    return -1;
                }
                //附卡保存
                if (IsNeedAdd)
                {
                    this.UpdateAdditionInfo(this.operType, report.Clone());
                }

                #endregion
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            if ( this.PatientType != FS.SOC.HISFC.DCP.Enum.PatientType.O)
            {
                this.InfectCode = "";
                this.reportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.OK;
            }
            #endregion

            this.GetMessage(report);
           // this.MyMessageBox("报告卡成功保存并上报!\n\n", "提示>>");

            this.Clear();

            return 1;
        }

        /// <summary>
        /// 保存订正，更新原卡
        /// </summary>
        /// <returns></returns>
        public int SaveCorrectReport()
        {
            FS.HISFC.DCP.Object.CommonReport mainreport = new FS.HISFC.DCP.Object.CommonReport();//原卡
            FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();//订正卡

            if (this.CommonReport != null)
            {
                mainreport = this.CommonReport.Clone();
                report = this.CommonReport.Clone();
            }
            if (mainreport == null)
            {
                return -1;
            }

            //验证信息
            if (this.GetValue(ref report) == -1)
            {
                return -1;
            }

            report.State = Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New);

            //提示信息
            if (this.ucDiseaseInfo1.InfectionClassEnable == true
              && MessageBox.Show(this, "您选择了【" + report.Disease.Name + "】\n保存后电子报卡由系统自动上传【疾病预防科】\n确认保存吗？", "温馨提示>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
            {
                return -1;
            }

            //获取订正卡信息
            if (this.operType == OperType.订正)
            {
                if (mainreport.CorrectFlag == "1")
                {
                    if (MessageBox.Show(this, "此卡以订正过，是否继续订正？", "温馨提示>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                    {
                        return -1;
                    }
                }

                //订正卡               
                report.CorrectedReportNO = mainreport.ID;
                report.ExtendInfo3 = "订正卡原卡为[" + mainreport.ReportNO + "]";
                //备注中加入原病名
                if (report.Memo.IndexOf("//原病名[" + mainreport.Disease.Name + "]") == -1)
                {
                    report.Memo += "//原病名[" + mainreport.Disease.Name + "]";
                }
                //原卡
                mainreport.ExtendInfo3 = "已订正";
                mainreport.CorrectFlag = "1";
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存,请稍候....");
            Application.DoEvents();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.diseaseMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.DCP.Object.CommonReport tempReport = report;
            tempReport.CorrectedReportNO = report.ID;
            //备注中加入原病名
            if (report.Memo.IndexOf("//原病名[" + tempReport.Disease.Name + "]") == -1)
            {
                report.Memo += "//原病名[" + tempReport.Disease.Name + "]";
            }

            if (diseaseMgr.InsertCommonReport(tempReport) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                this.MyMessageBox("订正卡保存失败>>" + this.diseaseMgr.Err, "err");
                return -1;
            }
            report = tempReport;

            //附卡保存
            if (this.IsNeedAdd)
            {
                if (this.UpdateAdditionInfo(this.operType, report) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("存储附卡信息失败"));
                    return -1;
                }
            }

            //修改原卡
            mainreport.CorrectReportNO = report.ID;
            if (this.diseaseMgr.UpdateCommonReport(mainreport) != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                this.MyMessageBox("报告卡保存失败>>" + this.diseaseMgr.Err, "err");
                return -1;
            }

            //附卡保存
            //if (this.IsNeedAdd)
            //{
            //    if (this.UpdateAdditionInfo(this.operType, mainreport) == -1)
            //    {
            //        MessageBox.Show(FS.FrameWork.Management.Language.Msg("存储附卡信息失败"));
            //        return -1;
            //    }
            //}

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            if ( this.PatientType != FS.SOC.HISFC.DCP.Enum.PatientType.O )
            {
                this.InfectCode = "";
                this.reportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.OK;
            }

            this.GetMessage(report);
            // 附加信息
            //this.MyMessageBox( "报告卡成功保存并上报!\n\n", "提示>>");

            return 0;
        }

        /// <summary>
        /// 附加提示信息
        /// </summary>
        /// <param name="report">疾病编号</param>
        /// <returns>附加信息</returns>
        private void GetMessage(FS.HISFC.DCP.Object.CommonReport report)
        {
            string message = "报告卡成功保存并上报!\n\n";
            string diseaseID = report.Disease.ID;

           
            //周末电话开关

            ArrayList altemp = new ArrayList();
            altemp = this.commonProcess.QueryConstantList("SWITCH");
            string strtelephone = "";
            foreach (FS.HISFC.Models.Base.Const conOb in altemp)
            {
                strtelephone += conOb.Memo + "\n";
            }

            if (strtelephone == "") //取消节假日日开关的优先级 2011-3-9
            {
                //电话通知
                if (this.ucDiseaseInfo1.HshNeedTelInfect.Contains(diseaseID))
                {
                    ArrayList al = new ArrayList();
                    al = this.commonProcess.QueryConstantList("MESSAGE");
                    foreach (FS.HISFC.Models.Base.Const con in al)
                    {
                        message += con.Memo + "\n";
                    }
                }
            }
            if (message != "" && message != null)
            {
                this.MyMessageBox( message, "提示>>");
            }

            if (strtelephone != "")
            {
                MessageBox.Show(this, strtelephone, "温馨提示>>", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            int diseasecode = FS.FrameWork.Function.NConvert.ToInt32(diseaseID);

            if (diseasecode == 1002 || diseasecode == 1003)
            {

            }
            if (diseasecode >= 1033 && diseasecode <= 1038)
            {
                this.MyMessageBox("请及时将患者转往【性病中心】诊治", "性病归口管理提示>>");
            }
            else if (diseasecode >= 7001 && diseasecode <= 7005)
            {
                this.MyMessageBox("请及时将患者转往【性病中心】诊治", "性病归口管理提示>>");
            }

            //this.MyMessageBox(diseasecode.ToString(),"");

            this.ShowMessageAfterSave(diseasecode.ToString());
        }

        private void ShowMessageAfterSave(string diseaseId)
        {
            string msg = "";
            if (diseaseId == "3003")
            {
                msg = @"对所有AFP病例应采集双份大便标本用于病毒分离

⑴标本的采集要求是：在麻痹出现后14天内采集，两份标本采集时间至少间隔24小时；每份标本重量≥5克（约为成人的大拇指末节大小）。标本采集应填写《采样送检单》。

⑵采样器及《采样送检单》可派人到疾病预防科领取。

⑶标本送检地点：标本采集后可通知禅城区疾病预防控制中心（）派人前来领取。

";
            }
            if (msg != "")
            {
                this.MyMessageBox(msg, "提示");
            }
        }

        public void Clear()
        {
            this.ucReportTop1.Clear();
            this.ucPatientInfo1.Clear();
            this.ucDiseaseInfo1.Clear();
            this.ucReportButtom1.Clear();
            
            this.isNeedAdd = false;
            this.neuPanel1.Controls.Clear();
            this.neuPanel1.Height = 0;
        }

        /// <summary>
        /// 续填，主要是共用患者信息
        /// </summary>
        public void CreateNextReport()
        {
            if (this.CommonReport != null)
            {
                this.CommonReport = this.CommonReport.Clone();
                this.CommonReport.ID = "";
                this.CommonReport.CorrectFlag = "";
                this.CommonReport.ReportNO = "";

                this.ucReportTop1.SetValue(this.CommonReport);
                this.ucDiseaseInfo1.Clear();
                this.ucReportButtom1.Clear();
        
            }
            
        }

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="type">err错误 其它作标题</param>
        private void MyMessageBox(string message, string type)
        {
            switch (type)
            {
                case "err":
                    MessageBox.Show(message, "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
        }

        private int GetValue(ref FS.HISFC.DCP.Object.CommonReport report)
        {
            this.GetDefaultValue(ref report);
            if (this.ucReportTop1.GetValue(ref report) == -1)
            {
                return -1;
            }
            if (this.ucPatientInfo1.GetValue(ref report) == -1)
            {
                return -1;
            }
            if (this.ucDiseaseInfo1.GetValue(ref report) == -1)
            {
                return -1;
            }
            if (this.ucReportButtom1.GetValue(ref report) == -1)
            {
                return -1;
            }

            //是否有附卡
            if (this.IsNeedAdd)
            {
                if (this.JudgeAdditionInfo() <= 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        private int GetDefaultValue(ref FS.HISFC.DCP.Object.CommonReport report)
        {
            if (report == null)
            {
                report = new FS.HISFC.DCP.Object.CommonReport();
            }
            report.ReportTime = this.diseaseMgr.GetDateTimeFromSysDateTime();
            if (this.operType == OperType.新增)
            {
                report.State = Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New);
            }
            else if (this.operType == OperType.保存 && string.IsNullOrEmpty(report.State))
            {
                report.State = Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New);
            }
            report.PatientType = this.PatientType.ToString();
            report.Oper = this.diseaseMgr.Operator;
            report.OperDept = ((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept;
            report.OperTime = this.diseaseMgr.GetDateTimeFromSysDateTime();
             
            return 1;
        }

        /// <summary>
        /// 设置报卡信息的状态
        /// </summary>
        /// <param name="report"></param>
        /// <param name="reportState"></param>
        private void UpdateReportState(FS.HISFC.DCP.Object.CommonReport report, FS.SOC.HISFC.DCP.Enum.ReportState reportState)
        {
            System.DateTime now = this.diseaseMgr.GetDateTimeFromSysDateTime();

            //操作信息
            report.Oper.ID = this.diseaseMgr.Operator.ID;
            report.OperDept.ID = ((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID;
            report.OperTime = now;// 

            //状态变化后返回 在更改期间有其他人操作
            string tempstate = "";
            try
            {
                FS.HISFC.DCP.Object.CommonReport reportTemp = this.diseaseMgr.GetCommonReportByID(report.ID);
                tempstate = reportTemp.State;
            }
            catch (Exception ex)
            {
                this.MyMessageBox("更新数据时转换报告卡状态失败！" + ex.Message, "err");
                return;
            }
            if (report.State != tempstate)
            {
                if (reportState == FS.SOC.HISFC.DCP.Enum.ReportState.New || tempstate == Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New))
                {
                    //修改合格
                }
                else
                {
                    this.MyMessageBox("操作失败：报告卡装态已发生变化\n按[确定]后系统自动刷新", "提示>>");
                    return;
                }
            }

            //新的状态
            //在此处理作废、审核人
            report.State = Function.ConvertState(reportState);
            if (reportState == FS.SOC.HISFC.DCP.Enum.ReportState.Cancel || reportState == FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel)
            {
                report.CancelOper.ID = report.Oper.ID;
                report.CancelTime = report.OperTime;
            }
            else if (reportState == FS.SOC.HISFC.DCP.Enum.ReportState.Eligible || reportState== FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible)
            {
                report.ApproveOper.ID = report.Oper.ID;
                report.ApproveTime = report.OperTime;
            }
            //更新数据库;

            if (this.diseaseMgr.UpdateCommonReport(report) != -1)
            {
                this.MyMessageBox("操作成功！", "提示>>");
                this.Clear();
            }
            else
            {
                this.MyMessageBox("操作失败！" + this.diseaseMgr.Err, "err");
            }

            #region

            //非审核时的刷新
            this.ucDiseaseQuery1.QueryByCache();

            #endregion
        }

        #region 附卡

        /// <summary>
        /// 取附卡信息
        /// </summary>
        public void GetAdditionInfo(FS.HISFC.DCP.Object.CommonReport diseaseReport)
        {
            if (this.isNeedAdd)
            {
                this.iAdditionReport = new ucBaseAddition();
                this.iAdditionReport.SetAdditionInfo(this.iAdditionReport.GetAdditionInfo(diseaseReport.ReportNO),this.neuPanel1);

            }
        }

        /// <summary>
        /// 修改附卡信息
        /// </summary>
        /// <param name="operType"></param>
        /// <param name="patientNO"></param>
        /// <param name="patientName"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        public int UpdateAdditionInfo(OperType operType, FS.HISFC.DCP.Object.CommonReport report)
        {
            if(this.isNeedAdd)
            {
                FS.HISFC.DCP.Object.AdditionReport additionReport = new FS.HISFC.DCP.Object.AdditionReport();
                this.iAdditionReport = new ucBaseAddition();
                this.iAdditionReport.PatientNO = report.Patient.ID;
                this.iAdditionReport.PatientName = report.Patient.Name;
                this.iAdditionReport.Report = report;
                additionReport = (FS.HISFC.DCP.Object.AdditionReport)this.iAdditionReport.GetAdditionInfo(/*this.tcReport.TabPages["tpAddition"]*/this.neuPanel1);
                additionReport.PatientNO = report.Patient.PID.ID;
                additionReport.PatientName = report.Patient.Name;
                additionReport.Memo = report.Disease.ID;

                int state = 0;
                if (string.IsNullOrEmpty(report.ID) == true)
                {
                    return this.iAdditionReport.InsertAdditionInfo(additionReport);
                }
                else if (operType == OperType.订正)
                {
                    state = this.iAdditionReport.UpdateAdditionInfo(additionReport);
                    if (state <= 1)
                    {
                        state = this.iAdditionReport.InsertAdditionInfo(additionReport);
                    }
                    return state;
                }
                else
                {
                    return this.iAdditionReport.UpdateAdditionInfo(additionReport);
                }
            }
            else
            {
                this.iAdditionReport = new ucBaseAddition();
                this.iAdditionReport.PatientNO = report.Patient.ID;
                this.iAdditionReport.PatientName = report.Patient.Name;
                this.iAdditionReport.Report = report;
                this.iAdditionReport.DeleteAdditionInfo();
            }
            return 1;
        }

        /// <summary>
        /// 验证附卡信息的完整
        /// </summary>
        /// <returns>-1,不完整 1,完整</returns>
        public int JudgeAdditionInfo()
        {
            string msg = "";
            int i = 0;
            if (this.isNeedAdd)
            {
                foreach (Control c in this.neuPanel1.Controls)
                {
                    if (c.GetType().IsSubclassOf(typeof(ucBaseAddition)))
                    {
                        i = ((ucBaseAddition)c).IsValid(ref msg);
                    }
                    if (i < 0)
                    {
                        msg = "以下附卡信息：" + msg + "不完整";
                        this.MyMessageBox(msg, "err");
                        this.tcReport.SelectedIndex = 1;
                        return -1;
                    }
                }
            }
            return i;
        }

        #endregion       

        #region 打印

        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.CommonReport != null)
            {
                if (string.IsNullOrEmpty(this.CommonReport.ReportNO))
                {
                    this.MyMessageBox("请先保存", "提示>>");
                    return -1;
                }
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList al = con.GetList("DCPPRINT");
                //按界面打印 否则按标准格式打印 2012-10-22 chengym
                if (al == null || (al != null && al.Count == 0))
                {
                    this.PrePrint();


                    FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                    print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

                    FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                    FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("DcpPaper");
                    if (pSize == null)
                    {
                        pSize = new FS.HISFC.Models.Base.PageSize("Letter", 700, 1110);
                    }
                    print.SetPageSize(pSize);

                    //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                    {
                        print.PrintPreview(pSize.Left, pSize.Top, tpMainReport);
                    }
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(pSize.Printer))
                    //    {
                    //        print.PrintDocument.PrinterSettings.PrinterName = pSize.Printer;
                    //    }
                    //    print.PrintPage(pSize.Left, pSize.Top, this.tpMainReport);
                    //}

                    this.Printed();
                }
                else
                {
                    ucInfectPrint uc = new ucInfectPrint();
                    uc.SetValues(this.CommonReport);
                    FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                    print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                    print.PrintPreview(0, 10, uc);
                }
            }

            return 1;
        }

        public  void PrePrint()
        {
            this.ucDiseaseInfo1.PrePrint();
            this.ucPatientInfo1.PrePrint(); 
            this.ucReportButtom1.PrePrint();
            this.ucReportTop1.PrePrint();

            this.tpMainReport.BackColor = Color.White;

            if (this.isNeedAdd)
            {
                for (int i = 0; i < this.neuPanel1.Controls.Count; i++)
                {
                    if (this.neuPanel1.Controls[i].GetType().IsSubclassOf(typeof(ucBaseMainReport)))
                    {
                        ((ucBaseMainReport)this.neuPanel1.Controls[i]).PrePrint();
                    }
                }
            }
        }

        public  void Printed()
        {
            this.ucDiseaseInfo1.Printed();
            this.ucPatientInfo1.Printed();
            this.ucReportButtom1.Printed();
            this.ucReportTop1.Printed();
            this.tpMainReport.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            if (this.isNeedAdd)
            {
                for (int i = 0; i < this.neuPanel1.Controls.Count; i++)
                {
                    if (this.neuPanel1.Controls[i].GetType().IsSubclassOf(typeof(ucBaseMainReport)))
                    {
                        ((ucBaseMainReport)this.neuPanel1.Controls[i]).Printed();
                    }
                }
            }
        }

        #endregion

        #endregion
        #region 帮助
        /// <summary>
        /// 帮助
        /// </summary>
        public void Help()
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\EpidemicHelp.CHM");
            }
            catch (Exception ex)
            {
                this.showMyMessageBox("帮助无法使用>>\n" + Application.StartupPath + @"\EpidemicHelp.CHM\n" + ex.Message, "err");
            }
        }

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="type">err错误 其它作标题</param>
        private bool showMyMessageBox(string message, string type)
        {
            System.Windows.Forms.DialogResult dr = new DialogResult();
            switch (type)
            {
                case "err":
                    dr = MessageBox.Show(message, "错误>>", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    break;
                case "info":
                    dr = MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
                default:
                    dr = MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region 权限相关操作

        /// <summary>
        /// 初始权限科室
        /// </summary>
        protected void InitPrivInformation()
        {
            FS.SOC.HISFC.BizProcess.DCP.Permission permissionProcess = new FS.SOC.HISFC.BizProcess.DCP.Permission();
            this.cdcPrivDeptList = permissionProcess.QueryUserPriv(FS.FrameWork.Management.Connection.Operator.ID, "8001");

            if (this.cdcPrivDeptList == null)
            {
                MessageBox.Show("获取CDC权限科室发生错误" + permissionProcess.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            /*
             * 医生可以进行的操作
             *  1、新建、查询
             *  2、保存、订正
             *  3、作废、删除
             *  4、修改本人建的卡
             * 
             * CDC权限运行进行的操作
             *  1、审核（合格、不合格）
             *  2、修改
             *  3、恢复
            */

            //疾控权限设置
            toolBarService.SetToolButtonEnabled("合格", this.isCDCPriv);
            toolBarService.SetToolButtonEnabled("不合格", this.isCDCPriv);

            // {2671947C-3F17-4eee-A72F-1479665EEB16}将界面中报告科室设置为不可修改
            //this.ucReportButtom1.SetControlEnable(this.isCDCPriv);
            //this.cmbDoctorDept.Enabled = this.isCDCPriv;
        }

        /// <summary>
        /// 判断是否为CDC权限科室
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功返回True 失败返回False</returns>
        protected bool IsCDCDept(string deptCode)
        {
            if (this.cdcPrivDeptList != null)
            {
                foreach (FS.FrameWork.Models.NeuObject info in this.cdcPrivDeptList)
                {
                    if (info.ID == deptCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 操作权限的判断
        /// </summary>
        /// <param name="report">报告实体</param>
        /// <param name="operType">操作方式</param>
        /// <returns>true有操作权限 false无操作权限</returns>
        private bool IsAllowOper(FS.HISFC.DCP.Object.CommonReport report, OperType operType)
        {
            if (report == null)
            {
                return true;
            }

            bool isAllow = false;

            switch (operType)
            {
                case OperType.保存:

                    #region Modify

                    switch (Int32.Parse(report.State))
                    {
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.New:               //新建卡
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible:        //不合格卡
                            
                            if (this.diseaseMgr.Operator.ID == report.ReportDoctor.ID)                //填报人与当前操作员一致
                            {
                                isAllow = true;
                            }
                            else
                            {
                                if (this.IsCDCDept(((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID) == true)              //判断是否有CDC权限，如有权限运行修改
                                {
                                    isAllow = true;
                                }
                            }
                            if (isAllow == false)
                            {
                                this.MyMessageBox("提示：不可修改他人填写的报告", "提示>>");
                            }
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible:
                            this.MyMessageBox("提示：报告已经合格", "提示>>");
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel:
                            this.MyMessageBox("提示：报告已经作废 不能再修改", "提示>>");
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel:
                            this.MyMessageBox("提示：报告审核时已经作废 不能修改", "提示>>");
                            break;
                    }

                    #endregion

                    break;
                case OperType.作废:

                    #region Cancel

                    switch (Int32.Parse(report.State))
                    {
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.New:
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible:
                            if (this.diseaseMgr.Operator.ID == report.ReportDoctor.ID)
                            {
                                isAllow = true;
                            }
                            else
                            {
                                if (this.IsCDCDept(((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID) == true)              //判断是否有CDC权限，如有权限运行修改
                                {
                                    isAllow = true;
                                }
                            }
                            if (isAllow == false)
                            {
                                this.MyMessageBox("提示：不可修改他人填写的报告", "提示>>");
                            }
                            else
                            {
                                if (MessageBox.Show(this, "作废后报告卡将不能恢复，是否作废？", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2)
                                    == System.Windows.Forms.DialogResult.No)
                                {
                                    isAllow = false;
                                }

                                frmSampleInput frmSampleInput = new frmSampleInput();

                                frmSampleInput.Title = "作废原因：";
                                if (frmSampleInput.ShowDialog() == DialogResult.Cancel)
                                {
                                    isAllow = false;
                                }
                                else
                                {
                                    report.Memo = frmSampleInput.InputText;
                                }
                                frmSampleInput.Close();

                            }
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible:
                            this.MyMessageBox("提示：报告已经合格 不能作废", "提示>>");
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel:
                            this.MyMessageBox("提示：报告已经被报告人作废", "提示>>");
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel:
                            this.MyMessageBox("提示：报告审核时已经作废", "提示>>");
                            break;
                    }
                    #endregion

                    break;
                case OperType.删除:

                    #region 删除

                    if (FS.FrameWork.Function.NConvert.ToInt32(report.State) != (int)FS.SOC.HISFC.DCP.Enum.ReportState.New)
                    {
                        this.MyMessageBox("非新建卡，不能进行删除操作", "提示>>");
                    }
                    else
                    {
                        if (this.diseaseMgr.Operator.ID != report.ReportDoctor.ID)
                        {
                            MessageBox.Show(this, "提示：不可删除他人填写的报告", "提示>>");
                        }
                        else
                        {
                            return true;
                        }
                    }

                    #endregion

                    break;

                case OperType.合格:

                    #region 合格

                    if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.New)
                    {
                        isAllow = true;
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible)
                    {
                        this.MyMessageBox("提示：报告已审核合格", "提示>>");
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible)
                    {
                        if (MessageBox.Show(this, "报告卡已审核，是否再审？", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2)
                            == System.Windows.Forms.DialogResult.Yes)
                            isAllow = true;
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel)
                    {
                        this.MyMessageBox("提示：报告已经被报人作废", "提示>>");
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel)
                    {
                        this.MyMessageBox("提示：报告审核时已经作废", "提示>>");
                    }
                    #endregion

                    break;
                case OperType.不合格:
                    #region
                    if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.New)
                    {
                        isAllow = true;
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible)
                    {
                        this.MyMessageBox("提示：报告已审核不合格", "提示>>");
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible)
                    {
                        if (MessageBox.Show(this, "报告卡已审核，是否再审？", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2)
                            == System.Windows.Forms.DialogResult.Yes)
                            isAllow = true;
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel)
                    {
                        this.MyMessageBox("提示：报告已经被报人作废", "提示>>");
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel)
                    {
                        this.MyMessageBox("提示：报告审核时已经作废", "提示>>");
                    }
                    break;
                    #endregion
                case OperType.恢复:
                    #region 恢复
                    if (this.IsCDCDept(((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID) == false)
                    {
                        this.MyMessageBox("提示：恢复报告卡请于疾病预防科联系", "提示>>");
                    }
                    else
                    {
                        if (Int32.Parse(report.State) != (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel && Int32.Parse(report.State) != (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel)
                        {
                            this.MyMessageBox("提示：非作废的报告卡不允许恢复", "提示>>");
                            break;
                        }
                        isAllow = true;
                        if (MessageBox.Show(this, "确实要恢复吗？", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2)
                            == System.Windows.Forms.DialogResult.No)
                        {
                            isAllow = false;
                        }
                    }
                    break;
                    #endregion
                case OperType.订正:
                    if (this.diseaseMgr.Operator.ID == report.ReportDoctor.ID)
                    {
                        isAllow = true;

                        string state = this.diseaseMgr.GetCommonReportByID(report.ID).State;
                        if (state == ((int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible).ToString())
                        {
                            this.MyMessageBox("报告卡已经审核通过，不能进行订正操作", "警告>>");
                            isAllow = false;
                        }
                        else if (state == ((int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel).ToString() || state == ((int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel).ToString())
                        {
                            this.MyMessageBox("报告卡已经作废，不能进行订正操作", "警告>>");
                            isAllow = false;
                        }
                        else if (state == ((int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible).ToString())
                        {
                            this.MyMessageBox("报告卡不合格，不能进行订正操作，请先修改", "警告>>");
                            isAllow = false;
                        }
                    }
                    else
                    {
                        this.MyMessageBox("提示：不可对他人填写的报告进行订正操作", "提示>>");
                    }
                    break;

            }
            return isAllow;
        }
        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            this.isCDCPriv = Function.CheckUserPriv(FS.FrameWork.Management.Connection.Operator.ID, "8001");

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager == false)
            {
                if (isCDCPriv == false)
                {
                    return -1;
                }
            }

            this.InitPrivInformation();

            return 1;
        }

        #endregion

        #region 事件

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Clear();

            this.ucDiseaseQuery1.AlReport.Clear();
            if (this.ucDiseaseQuery1.Query() == -1)
            {
                return -1;
            }

            return base.OnQuery(sender, neuObject);
        }

        protected void ucDiseaseInfo1_AdditionEvent(bool isNeed, ArrayList al)
        {
            if (isNeed)
            {
                if (al != null&&al.Count>0)
                {
                    foreach (ucBaseAddition baseAddition in al)
                    {
                        baseAddition.Dock = DockStyle.Top;
                     

                        this.neuPanel1.Controls.Add(baseAddition);
                        this.neuPanel1.Height += baseAddition.Height;
                    }

               
                    this.IsNeedAdd = true;
                }

            }
            else
            {
                this.neuPanel1.Controls.Clear();
                this.neuPanel1.Height = 0;
                this.IsNeedAdd = false;
              
            }

        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.SetOperType(OperType.保存) == 1 && this.Save() == 1)
            {
                this.Clear();
                this.ucDiseaseQuery1.QueryByCache();
                this.SetOperType(OperType.查询);
                // 下诊断后是否填写了报告卡，“保存成功”不可少
                this.Text += "--保存成功";
            }
            return 1;
        }

        private void ucDiseaseQuery1_ShowInfo(FS.FrameWork.Models.NeuObject obj)
        {
            if (obj == null)
            {
                return;
            }

            this.Clear();
            if (obj is FS.HISFC.DCP.Object.CommonReport)
            {
                this.CommonReport = obj as FS.HISFC.DCP.Object.CommonReport;
                this.ucDiseaseInfo1.SetValue(this.CommonReport);
                this.ucPatientInfo1.SetValue(this.CommonReport);
                this.ucReportButtom1.SetValue(this.CommonReport);
                this.ucReportTop1.SetValue(this.CommonReport);
                //附卡
                if (IsNeedAdd)
                {
                    this.GetAdditionInfo(CommonReport);
                }

            }
            else if (obj is FS.HISFC.Models.RADT.PatientInfo)
            {
                this.ucDiseaseInfo1.SetValue((FS.HISFC.Models.RADT.Patient)obj, FS.SOC.HISFC.DCP.Enum.PatientType.I);
                this.ucPatientInfo1.SetValue((FS.HISFC.Models.RADT.Patient)obj, FS.SOC.HISFC.DCP.Enum.PatientType.I);
                this.ucReportButtom1.SetValue((FS.HISFC.Models.RADT.Patient)obj, FS.SOC.HISFC.DCP.Enum.PatientType.I);
                this.ucReportTop1.SetValue((FS.HISFC.Models.RADT.Patient)obj, FS.SOC.HISFC.DCP.Enum.PatientType.I);
            }
            else if (obj is FS.HISFC.Models.Registration.Register)
            {
                this.ucDiseaseInfo1.SetValue((FS.HISFC.Models.Registration.Register)obj, FS.SOC.HISFC.DCP.Enum.PatientType.C);
                this.ucPatientInfo1.SetValue((FS.HISFC.Models.Registration.Register)obj, FS.SOC.HISFC.DCP.Enum.PatientType.C);
                this.ucReportButtom1.SetValue((FS.HISFC.Models.Registration.Register)obj, FS.SOC.HISFC.DCP.Enum.PatientType.C);
                this.ucReportTop1.SetValue((FS.HISFC.Models.Registration.Register)obj, FS.SOC.HISFC.DCP.Enum.PatientType.C);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.isInit == false)
            {
                this.Init();
            }
            this.ucDiseaseQuery1.Clear();
            base.OnLoad(e);
        }

        #endregion
    }


    /// <summary>
    /// 操作的类型
    /// </summary>
    public enum OperType
    {
        新增,
        续填,
        保存,
        合格,
        不合格,
        订正,
        作废,
        恢复,
        删除,
        查询
    }
}
