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
    /// [��������: ������Ϣuc]<br></br>
    /// [�� �� ��: zj]<br></br>
    /// [����ʱ��: 2008-09-17]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
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
            //ucDiseaseQuery.Init���������AlReport�ڽ�����ʾ��ʱ��ѱ��濨��ʾ��treeview�����ұ�֤clear��ʱ�򲻱����
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

        #region ����
        private bool isInit = false;

        private FS.SOC.HISFC.BizLogic.DCP.DiseaseReport diseaseMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();
        /// <summary>
        /// ��������
        /// </summary>
        private OperType operType;

        /// <summary>
        /// �Ƿ�Ԥ��Ԥ������Ȩ��
        /// </summary>
        private bool isCDCPriv = false;

        /// <summary>
        /// ����Ȩ����ؿ���
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
        /// �����ӿ�
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCPInterface.IAddition iAdditionReport;

        /// <summary>
        /// ����������
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();

        #endregion

        #region ����

        /// <summary>
        /// �������ߵ�����
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
        /// ������Ϣ
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
        /// ���û��߲�ѯ��Ĭ������
        /// </summary>
        [Category("��������"), Description("���û��߲�ѯ��Ĭ������")]
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
        /// ָ����������
        /// </summary>
        public string InfectCode
        {
            get { return this.ucDiseaseInfo1.InfectCode; }
            set { this.ucDiseaseInfo1.InfectCode = value; }
        }

        /// <summary>
        /// �Ƿ���Ҫ����
        /// </summary>
        private bool isNeedAdd = false;

        /// <summary>
        /// �Ƿ���Ҫ����
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
        /// �����������
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

        #region ������
        /// <summary>
        /// ToolBar������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// �������ĳ�ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("�½�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);

            toolBarService.AddToolButton("�ϸ�", "Eligible", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zִ��, true, false, null);
            toolBarService.AddToolButton("���ϸ�", "UnEligible", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);

            toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X��һ��, true, false, null);
            toolBarService.AddToolButton("�ָ�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��, true, false, null);
            toolBarService.AddToolButton("����", "Cancel", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            toolBarService.AddToolButton("����", "Cancel", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);

            toolBarService.AddToolButton("ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.ToolStrip_ItemClicked(e.ClickedItem.Text);
        }


        /// <summary>
        ///  ������������ť�ǵ���,д������,�Ա㱻�ⲿ����
        /// </summary>
        /// <param name="clickedItemText"></param>
        public void ToolStrip_ItemClicked(string clickedItemText)
        {
            switch (clickedItemText)
            {
                case "�½�":
                    if (this.SetOperType(OperType.����) == 1)
                    {
                        this.Clear();
                    }
                    break;
                case "����":
                    if (this.SetOperType(OperType.����) == 1)
                    {
                        this.CreateNextReport();
                    }
                    break;
                case "�ϸ�":
                    this.SetOperType(OperType.�ϸ�);
                    break;
                case "���ϸ�":
                    this.SetOperType(OperType.���ϸ�);
                    break;
                case "����":
                    if (this.SetOperType(OperType.����) == 1)
                    {
                        this.OnCorrect();
                    }
                    break;
                case "�ָ�":
                    this.SetOperType(OperType.�ָ�);
                    break;
                case "����":
                    this.SetOperType(OperType.����);
                    break;
                case "ɾ��":
                    if (this.SetOperType(OperType.ɾ��) == 1)
                    {
                        this.ucDiseaseQuery1.DeleteReport();
                    }
                    break;
                case "��ѯ":
                    this.ucDiseaseQuery1.AlReport.Clear();
                    this.ucDiseaseQuery1.Query();
                    break;
                case "����":
                    this.OnSave(new object(), new object());
                    break;
                case "��ӡ":
                    this.OnPrint(null, null);
                    break;
                case "�˳�":
                    //this.reportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.Cancel;
                    break;
                case "��Ⱦ��֪ʶ":
                    this.Help();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ����

        public int Init()
        {
            DateTime sysdate = this.diseaseMgr.GetDateTimeFromSysDateTime();

            this.PreArrange();

            this.ucDiseaseInfo1.Init(sysdate);
            this.ucPatientInfo1.Init(sysdate);
            this.ucReportButtom1.Init(sysdate);
            this.ucReportTop1.Init(sysdate);
            this.ucDiseaseQuery1.Init();


            // {2671947C-3F17-4eee-A72F-1479665EEB16}�������б���ʱ������Ϊ�����޸�
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
        /// ���ò���Ա�Ĳ�������
        /// </summary>
        /// <param name="operType"></param>
        /// <returns>�������ͽ�� 1 �ɹ� -1 ʧ��</returns>
        private int SetOperType(OperType operType)
        {
            if (operType == OperType.�ϸ� || operType == OperType.���ϸ� || operType == OperType.�ָ�)
            {
                if (PreArrange() == -1)
                {
                    MessageBox.Show("����Ԥ�����Ȩ�ޣ��޷�������Ӧ����", "Ȩ�޲���", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
            }

            this.operType = operType;
             
            if (this.operType == OperType.����||this.operType== OperType.��ѯ || this.operType== OperType.����)
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
                if (this.operType == OperType.�ϸ� || this.operType == OperType.���ϸ� || this.operType == OperType.���� || this.operType == OperType.�ָ� || this.operType == OperType.���� || this.operType == OperType.ɾ��)
                {
                    this.MyMessageBox("��ѡ�񱨿����в�����", "��ʾ>>");
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
                if (this.operType == OperType.����)     //�����½��ĵ�������
                {
                    return 1;
                }
                return -1;
            }
            
            switch (this.operType)
            {
                case OperType.�ϸ�:
                    this.UpdateReportState(report, FS.SOC.HISFC.DCP.Enum.ReportState.Eligible);
                    break;
                case OperType.���ϸ�:
                    if (this.ucReportButtom1.GetValue(ref report, OperType.���ϸ�) == -1)
                    {
                        return -1;
                    }
                    this.UpdateReportState(report, FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible);
                    break;
                case OperType.����:
                    
                    if (this.diseaseMgr.Operator.ID == report.Oper.ID)
                    {
                        this.UpdateReportState(report, FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel);
                    }
                    else
                    {
                        this.UpdateReportState(report, FS.SOC.HISFC.DCP.Enum.ReportState.Cancel);
                    }
                    break;
                case OperType.�ָ�:
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
        /// ��������
        /// </summary>
        public void OnCorrect()
        {
            if (this.SaveCorrectReport() == 0)
            {
                this.Clear();

                this.ucDiseaseQuery1.QueryByCache();
                this.SetOperType(OperType.��ѯ);

                // ����Ϻ��Ƿ���д�˱��濨��������ɹ���������
                this.Text += "--����ɹ�";
            }
        }

        public int Save()
        {
            FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
            if (this.CommonReport != null)
            {
                report = this.CommonReport.Clone();
            }

            #region ȡֵ

            if (this.GetValue(ref report) == -1)
            {
                return -1;
            }
            report.State = Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New);

            #endregion

            #region ���ݱ���

            if (this.ucDiseaseInfo1.InfectionClassEnable == true
              && MessageBox.Show(this, "��ѡ���ˡ�" + report.Disease.Name + "��\n�������ӱ�����ϵͳ�Զ��ϴ�������Ԥ���ơ�\nȷ�ϱ�����", "��ܰ��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
            {
                return -1;
            }


            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���,���Ժ�....");
            Application.DoEvents();

            //������Ϊ�� ��Ϊ�¿��������ݿ�

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.diseaseMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (string.IsNullOrEmpty(report.ID) && string.IsNullOrEmpty(report.ReportNO))
            {
                #region �¿����봦��

                //����Ƕ��� ��Ҫ����ԭ��
                if (this.diseaseMgr.InsertCommonReport(report) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    report.ID = string.Empty;
                    report.ReportNO = string.Empty;
                    this.MyMessageBox("���濨����ʧ��>>" + this.diseaseMgr.Err, "err");
                    return -1;
                }

                //��������
                if (IsNeedAdd)
                {
                    if (this.UpdateAdditionInfo(this.operType, report) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�洢������Ϣʧ��"));
                        return -1;
                    }
                }

                #endregion
            }
            else
            {
                #region �ɿ����´���
                //�����ݿ������ȡ

                FS.HISFC.DCP.Object.CommonReport mainreport = this.diseaseMgr.GetCommonReportByID(report.ID);
                if (mainreport == null)
                {
                    mainreport = this.diseaseMgr.GetCommonReportByNO(report.ReportNO); //ID�鲻���˲�reportNO
                    if (mainreport == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.MyMessageBox("���濨����ʧ��>>" + this.diseaseMgr.Err, "err");
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
                    this.MyMessageBox("���濨����ʧ��>>" + this.diseaseMgr.Err, "err");
                    return -1;
                }
                //��������
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
           // this.MyMessageBox("���濨�ɹ����沢�ϱ�!\n\n", "��ʾ>>");

            this.Clear();

            return 1;
        }

        /// <summary>
        /// ���涩��������ԭ��
        /// </summary>
        /// <returns></returns>
        public int SaveCorrectReport()
        {
            FS.HISFC.DCP.Object.CommonReport mainreport = new FS.HISFC.DCP.Object.CommonReport();//ԭ��
            FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();//������

            if (this.CommonReport != null)
            {
                mainreport = this.CommonReport.Clone();
                report = this.CommonReport.Clone();
            }
            if (mainreport == null)
            {
                return -1;
            }

            //��֤��Ϣ
            if (this.GetValue(ref report) == -1)
            {
                return -1;
            }

            report.State = Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New);

            //��ʾ��Ϣ
            if (this.ucDiseaseInfo1.InfectionClassEnable == true
              && MessageBox.Show(this, "��ѡ���ˡ�" + report.Disease.Name + "��\n�������ӱ�����ϵͳ�Զ��ϴ�������Ԥ���ơ�\nȷ�ϱ�����", "��ܰ��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
            {
                return -1;
            }

            //��ȡ��������Ϣ
            if (this.operType == OperType.����)
            {
                if (mainreport.CorrectFlag == "1")
                {
                    if (MessageBox.Show(this, "�˿��Զ��������Ƿ����������", "��ܰ��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                    {
                        return -1;
                    }
                }

                //������               
                report.CorrectedReportNO = mainreport.ID;
                report.ExtendInfo3 = "������ԭ��Ϊ[" + mainreport.ReportNO + "]";
                //��ע�м���ԭ����
                if (report.Memo.IndexOf("//ԭ����[" + mainreport.Disease.Name + "]") == -1)
                {
                    report.Memo += "//ԭ����[" + mainreport.Disease.Name + "]";
                }
                //ԭ��
                mainreport.ExtendInfo3 = "�Ѷ���";
                mainreport.CorrectFlag = "1";
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڱ���,���Ժ�....");
            Application.DoEvents();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.diseaseMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.DCP.Object.CommonReport tempReport = report;
            tempReport.CorrectedReportNO = report.ID;
            //��ע�м���ԭ����
            if (report.Memo.IndexOf("//ԭ����[" + tempReport.Disease.Name + "]") == -1)
            {
                report.Memo += "//ԭ����[" + tempReport.Disease.Name + "]";
            }

            if (diseaseMgr.InsertCommonReport(tempReport) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                this.MyMessageBox("����������ʧ��>>" + this.diseaseMgr.Err, "err");
                return -1;
            }
            report = tempReport;

            //��������
            if (this.IsNeedAdd)
            {
                if (this.UpdateAdditionInfo(this.operType, report) == -1)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�洢������Ϣʧ��"));
                    return -1;
                }
            }

            //�޸�ԭ��
            mainreport.CorrectReportNO = report.ID;
            if (this.diseaseMgr.UpdateCommonReport(mainreport) != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                this.MyMessageBox("���濨����ʧ��>>" + this.diseaseMgr.Err, "err");
                return -1;
            }

            //��������
            //if (this.IsNeedAdd)
            //{
            //    if (this.UpdateAdditionInfo(this.operType, mainreport) == -1)
            //    {
            //        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�洢������Ϣʧ��"));
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
            // ������Ϣ
            //this.MyMessageBox( "���濨�ɹ����沢�ϱ�!\n\n", "��ʾ>>");

            return 0;
        }

        /// <summary>
        /// ������ʾ��Ϣ
        /// </summary>
        /// <param name="report">�������</param>
        /// <returns>������Ϣ</returns>
        private void GetMessage(FS.HISFC.DCP.Object.CommonReport report)
        {
            string message = "���濨�ɹ����沢�ϱ�!\n\n";
            string diseaseID = report.Disease.ID;

           
            //��ĩ�绰����

            ArrayList altemp = new ArrayList();
            altemp = this.commonProcess.QueryConstantList("SWITCH");
            string strtelephone = "";
            foreach (FS.HISFC.Models.Base.Const conOb in altemp)
            {
                strtelephone += conOb.Memo + "\n";
            }

            if (strtelephone == "") //ȡ���ڼ����տ��ص����ȼ� 2011-3-9
            {
                //�绰֪ͨ
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
                this.MyMessageBox( message, "��ʾ>>");
            }

            if (strtelephone != "")
            {
                MessageBox.Show(this, strtelephone, "��ܰ��ʾ>>", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            int diseasecode = FS.FrameWork.Function.NConvert.ToInt32(diseaseID);

            if (diseasecode == 1002 || diseasecode == 1003)
            {

            }
            if (diseasecode >= 1033 && diseasecode <= 1038)
            {
                this.MyMessageBox("�뼰ʱ������ת�����Բ����ġ�����", "�Բ���ڹ�����ʾ>>");
            }
            else if (diseasecode >= 7001 && diseasecode <= 7005)
            {
                this.MyMessageBox("�뼰ʱ������ת�����Բ����ġ�����", "�Բ���ڹ�����ʾ>>");
            }

            //this.MyMessageBox(diseasecode.ToString(),"");

            this.ShowMessageAfterSave(diseasecode.ToString());
        }

        private void ShowMessageAfterSave(string diseaseId)
        {
            string msg = "";
            if (diseaseId == "3003")
            {
                msg = @"������AFP����Ӧ�ɼ�˫�ݴ��걾���ڲ�������

�ű걾�Ĳɼ�Ҫ���ǣ�����Գ��ֺ�14���ڲɼ������ݱ걾�ɼ�ʱ�����ټ��24Сʱ��ÿ�ݱ걾������5�ˣ�ԼΪ���˵Ĵ�Ĵָĩ�ڴ�С�����걾�ɼ�Ӧ��д�������ͼ쵥����

�Ʋ��������������ͼ쵥�������˵�����Ԥ������ȡ��

�Ǳ걾�ͼ�ص㣺�걾�ɼ����֪ͨ����������Ԥ���������ģ�������ǰ����ȡ��

";
            }
            if (msg != "")
            {
                this.MyMessageBox(msg, "��ʾ");
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
        /// �����Ҫ�ǹ��û�����Ϣ
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
        /// <param name="message">��ʾ��Ϣ</param>
        /// <param name="type">err���� ����������</param>
        private void MyMessageBox(string message, string type)
        {
            switch (type)
            {
                case "err":
                    MessageBox.Show(message, "��ʾ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
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

            //�Ƿ��и���
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
            if (this.operType == OperType.����)
            {
                report.State = Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New);
            }
            else if (this.operType == OperType.���� && string.IsNullOrEmpty(report.State))
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
        /// ���ñ�����Ϣ��״̬
        /// </summary>
        /// <param name="report"></param>
        /// <param name="reportState"></param>
        private void UpdateReportState(FS.HISFC.DCP.Object.CommonReport report, FS.SOC.HISFC.DCP.Enum.ReportState reportState)
        {
            System.DateTime now = this.diseaseMgr.GetDateTimeFromSysDateTime();

            //������Ϣ
            report.Oper.ID = this.diseaseMgr.Operator.ID;
            report.OperDept.ID = ((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID;
            report.OperTime = now;// 

            //״̬�仯�󷵻� �ڸ����ڼ��������˲���
            string tempstate = "";
            try
            {
                FS.HISFC.DCP.Object.CommonReport reportTemp = this.diseaseMgr.GetCommonReportByID(report.ID);
                tempstate = reportTemp.State;
            }
            catch (Exception ex)
            {
                this.MyMessageBox("��������ʱת�����濨״̬ʧ�ܣ�" + ex.Message, "err");
                return;
            }
            if (report.State != tempstate)
            {
                if (reportState == FS.SOC.HISFC.DCP.Enum.ReportState.New || tempstate == Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New))
                {
                    //�޸ĺϸ�
                }
                else
                {
                    this.MyMessageBox("����ʧ�ܣ����濨װ̬�ѷ����仯\n��[ȷ��]��ϵͳ�Զ�ˢ��", "��ʾ>>");
                    return;
                }
            }

            //�µ�״̬
            //�ڴ˴������ϡ������
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
            //�������ݿ�;

            if (this.diseaseMgr.UpdateCommonReport(report) != -1)
            {
                this.MyMessageBox("�����ɹ���", "��ʾ>>");
                this.Clear();
            }
            else
            {
                this.MyMessageBox("����ʧ�ܣ�" + this.diseaseMgr.Err, "err");
            }

            #region

            //�����ʱ��ˢ��
            this.ucDiseaseQuery1.QueryByCache();

            #endregion
        }

        #region ����

        /// <summary>
        /// ȡ������Ϣ
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
        /// �޸ĸ�����Ϣ
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
                else if (operType == OperType.����)
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
        /// ��֤������Ϣ������
        /// </summary>
        /// <returns>-1,������ 1,����</returns>
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
                        msg = "���¸�����Ϣ��" + msg + "������";
                        this.MyMessageBox(msg, "err");
                        this.tcReport.SelectedIndex = 1;
                        return -1;
                    }
                }
            }
            return i;
        }

        #endregion       

        #region ��ӡ

        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.CommonReport != null)
            {
                if (string.IsNullOrEmpty(this.CommonReport.ReportNO))
                {
                    this.MyMessageBox("���ȱ���", "��ʾ>>");
                    return -1;
                }
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList al = con.GetList("DCPPRINT");
                //�������ӡ ���򰴱�׼��ʽ��ӡ 2012-10-22 chengym
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
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public void Help()
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\EpidemicHelp.CHM");
            }
            catch (Exception ex)
            {
                this.showMyMessageBox("�����޷�ʹ��>>\n" + Application.StartupPath + @"\EpidemicHelp.CHM\n" + ex.Message, "err");
            }
        }

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="message">��ʾ��Ϣ</param>
        /// <param name="type">err���� ����������</param>
        private bool showMyMessageBox(string message, string type)
        {
            System.Windows.Forms.DialogResult dr = new DialogResult();
            switch (type)
            {
                case "err":
                    dr = MessageBox.Show(message, "����>>", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
        #region Ȩ����ز���

        /// <summary>
        /// ��ʼȨ�޿���
        /// </summary>
        protected void InitPrivInformation()
        {
            FS.SOC.HISFC.BizProcess.DCP.Permission permissionProcess = new FS.SOC.HISFC.BizProcess.DCP.Permission();
            this.cdcPrivDeptList = permissionProcess.QueryUserPriv(FS.FrameWork.Management.Connection.Operator.ID, "8001");

            if (this.cdcPrivDeptList == null)
            {
                MessageBox.Show("��ȡCDCȨ�޿��ҷ�������" + permissionProcess.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            /*
             * ҽ�����Խ��еĲ���
             *  1���½�����ѯ
             *  2�����桢����
             *  3�����ϡ�ɾ��
             *  4���޸ı��˽��Ŀ�
             * 
             * CDCȨ�����н��еĲ���
             *  1����ˣ��ϸ񡢲��ϸ�
             *  2���޸�
             *  3���ָ�
            */

            //����Ȩ������
            toolBarService.SetToolButtonEnabled("�ϸ�", this.isCDCPriv);
            toolBarService.SetToolButtonEnabled("���ϸ�", this.isCDCPriv);

            // {2671947C-3F17-4eee-A72F-1479665EEB16}�������б����������Ϊ�����޸�
            //this.ucReportButtom1.SetControlEnable(this.isCDCPriv);
            //this.cmbDoctorDept.Enabled = this.isCDCPriv;
        }

        /// <summary>
        /// �ж��Ƿ�ΪCDCȨ�޿���
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ�����True ʧ�ܷ���False</returns>
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
        /// ����Ȩ�޵��ж�
        /// </summary>
        /// <param name="report">����ʵ��</param>
        /// <param name="operType">������ʽ</param>
        /// <returns>true�в���Ȩ�� false�޲���Ȩ��</returns>
        private bool IsAllowOper(FS.HISFC.DCP.Object.CommonReport report, OperType operType)
        {
            if (report == null)
            {
                return true;
            }

            bool isAllow = false;

            switch (operType)
            {
                case OperType.����:

                    #region Modify

                    switch (Int32.Parse(report.State))
                    {
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.New:               //�½���
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible:        //���ϸ�
                            
                            if (this.diseaseMgr.Operator.ID == report.ReportDoctor.ID)                //����뵱ǰ����Աһ��
                            {
                                isAllow = true;
                            }
                            else
                            {
                                if (this.IsCDCDept(((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID) == true)              //�ж��Ƿ���CDCȨ�ޣ�����Ȩ�������޸�
                                {
                                    isAllow = true;
                                }
                            }
                            if (isAllow == false)
                            {
                                this.MyMessageBox("��ʾ�������޸�������д�ı���", "��ʾ>>");
                            }
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible:
                            this.MyMessageBox("��ʾ�������Ѿ��ϸ�", "��ʾ>>");
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel:
                            this.MyMessageBox("��ʾ�������Ѿ����� �������޸�", "��ʾ>>");
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel:
                            this.MyMessageBox("��ʾ���������ʱ�Ѿ����� �����޸�", "��ʾ>>");
                            break;
                    }

                    #endregion

                    break;
                case OperType.����:

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
                                if (this.IsCDCDept(((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID) == true)              //�ж��Ƿ���CDCȨ�ޣ�����Ȩ�������޸�
                                {
                                    isAllow = true;
                                }
                            }
                            if (isAllow == false)
                            {
                                this.MyMessageBox("��ʾ�������޸�������д�ı���", "��ʾ>>");
                            }
                            else
                            {
                                if (MessageBox.Show(this, "���Ϻ󱨸濨�����ָܻ����Ƿ����ϣ�", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                                    System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2)
                                    == System.Windows.Forms.DialogResult.No)
                                {
                                    isAllow = false;
                                }

                                frmSampleInput frmSampleInput = new frmSampleInput();

                                frmSampleInput.Title = "����ԭ��";
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
                            this.MyMessageBox("��ʾ�������Ѿ��ϸ� ��������", "��ʾ>>");
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel:
                            this.MyMessageBox("��ʾ�������Ѿ�������������", "��ʾ>>");
                            break;
                        case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel:
                            this.MyMessageBox("��ʾ���������ʱ�Ѿ�����", "��ʾ>>");
                            break;
                    }
                    #endregion

                    break;
                case OperType.ɾ��:

                    #region ɾ��

                    if (FS.FrameWork.Function.NConvert.ToInt32(report.State) != (int)FS.SOC.HISFC.DCP.Enum.ReportState.New)
                    {
                        this.MyMessageBox("���½��������ܽ���ɾ������", "��ʾ>>");
                    }
                    else
                    {
                        if (this.diseaseMgr.Operator.ID != report.ReportDoctor.ID)
                        {
                            MessageBox.Show(this, "��ʾ������ɾ��������д�ı���", "��ʾ>>");
                        }
                        else
                        {
                            return true;
                        }
                    }

                    #endregion

                    break;

                case OperType.�ϸ�:

                    #region �ϸ�

                    if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.New)
                    {
                        isAllow = true;
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible)
                    {
                        this.MyMessageBox("��ʾ����������˺ϸ�", "��ʾ>>");
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible)
                    {
                        if (MessageBox.Show(this, "���濨����ˣ��Ƿ�����", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2)
                            == System.Windows.Forms.DialogResult.Yes)
                            isAllow = true;
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel)
                    {
                        this.MyMessageBox("��ʾ�������Ѿ�����������", "��ʾ>>");
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel)
                    {
                        this.MyMessageBox("��ʾ���������ʱ�Ѿ�����", "��ʾ>>");
                    }
                    #endregion

                    break;
                case OperType.���ϸ�:
                    #region
                    if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.New)
                    {
                        isAllow = true;
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible)
                    {
                        this.MyMessageBox("��ʾ����������˲��ϸ�", "��ʾ>>");
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible)
                    {
                        if (MessageBox.Show(this, "���濨����ˣ��Ƿ�����", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2)
                            == System.Windows.Forms.DialogResult.Yes)
                            isAllow = true;
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel)
                    {
                        this.MyMessageBox("��ʾ�������Ѿ�����������", "��ʾ>>");
                    }
                    else if (Int32.Parse(report.State) == (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel)
                    {
                        this.MyMessageBox("��ʾ���������ʱ�Ѿ�����", "��ʾ>>");
                    }
                    break;
                    #endregion
                case OperType.�ָ�:
                    #region �ָ�
                    if (this.IsCDCDept(((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID) == false)
                    {
                        this.MyMessageBox("��ʾ���ָ����濨���ڼ���Ԥ������ϵ", "��ʾ>>");
                    }
                    else
                    {
                        if (Int32.Parse(report.State) != (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel && Int32.Parse(report.State) != (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel)
                        {
                            this.MyMessageBox("��ʾ�������ϵı��濨������ָ�", "��ʾ>>");
                            break;
                        }
                        isAllow = true;
                        if (MessageBox.Show(this, "ȷʵҪ�ָ���", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo,
                            System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button2)
                            == System.Windows.Forms.DialogResult.No)
                        {
                            isAllow = false;
                        }
                    }
                    break;
                    #endregion
                case OperType.����:
                    if (this.diseaseMgr.Operator.ID == report.ReportDoctor.ID)
                    {
                        isAllow = true;

                        string state = this.diseaseMgr.GetCommonReportByID(report.ID).State;
                        if (state == ((int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible).ToString())
                        {
                            this.MyMessageBox("���濨�Ѿ����ͨ�������ܽ��ж�������", "����>>");
                            isAllow = false;
                        }
                        else if (state == ((int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel).ToString() || state == ((int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel).ToString())
                        {
                            this.MyMessageBox("���濨�Ѿ����ϣ����ܽ��ж�������", "����>>");
                            isAllow = false;
                        }
                        else if (state == ((int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible).ToString())
                        {
                            this.MyMessageBox("���濨���ϸ񣬲��ܽ��ж��������������޸�", "����>>");
                            isAllow = false;
                        }
                    }
                    else
                    {
                        this.MyMessageBox("��ʾ�����ɶ�������д�ı�����ж�������", "��ʾ>>");
                    }
                    break;

            }
            return isAllow;
        }
        #endregion

        #region IPreArrange ��Ա

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

        #region �¼�

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
            if (this.SetOperType(OperType.����) == 1 && this.Save() == 1)
            {
                this.Clear();
                this.ucDiseaseQuery1.QueryByCache();
                this.SetOperType(OperType.��ѯ);
                // ����Ϻ��Ƿ���д�˱��濨��������ɹ���������
                this.Text += "--����ɹ�";
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
                //����
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
    /// ����������
    /// </summary>
    public enum OperType
    {
        ����,
        ����,
        ����,
        �ϸ�,
        ���ϸ�,
        ����,
        ����,
        �ָ�,
        ɾ��,
        ��ѯ
    }
}
