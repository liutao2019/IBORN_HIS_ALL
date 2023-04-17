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
    /// ucPatientInfo<br></br>
    /// [��������: ��ѯ���uc]<br></br>
    /// [�� �� ��: zj]<br></br>
    /// [����ʱ��: 2008-09-17]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDiseaseQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDiseaseQuery()
        {
            InitializeComponent();
            this.tvReport.ImageList = this.tvReport.groupImageList;
            this.tvReport.AfterSelect+=new TreeViewEventHandler(tvReport_AfterSelect);
        }

        #region �����

        

        /// <summary>
        /// ��ѯ������Ϣ�����ͣ�Ĭ��Ϊ���￨�ţ�
        /// </summary>
        private FS.SOC.HISFC.DCP.Enum.PatientType llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;

        /// <summary>
        /// ���濨������
        /// </summary>
        private FS.SOC.HISFC.BizLogic.DCP.DiseaseReport diseaseMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();

        /// <summary>
        /// סԺ���߹�����
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCP.Patient patientProcess = new FS.SOC.HISFC.BizProcess.DCP.Patient();

        /// <summary>
        /// ѡ��ڵ�ί��
        /// </summary>
        /// <param name="patient"></param>
        public delegate void SelectNode(FS.FrameWork.Models.NeuObject obj);

        /// <summary>
        /// ��ʾ�¼�
        /// </summary>
        public event SelectNode ShowInfo;

        /// <summary>
        /// ���漯��
        /// </summary>
        private ArrayList alReport = new ArrayList();

        #endregion

        #region ����

        /// <summary>
        /// �������ߵ����ͣ�Ĭ��Ϊ������
        /// </summary>
        private FS.SOC.HISFC.DCP.Enum.PatientType patientType = FS.SOC.HISFC.DCP.Enum.PatientType.O;
        /// <summary>
        /// �������ߵ�����
        /// </summary>
        public FS.SOC.HISFC.DCP.Enum.PatientType PatientType
        {
            get
            {
                return this.patientType;
            }
            set
            {
                this.patientType = value;
            }
        }

        /// <summary>
        /// ���濨[�ڱ��������ʱ����show֮ǰ��ֵ]
        /// </summary>
        public ArrayList AlReport
        {
            get
            {
                return alReport;
            }
            set
            {
                alReport = value;
            }
        }

        /// <summary>
        /// ���û��߲�ѯ��Ĭ������
        /// </summary>
        private int days = 5;
        public int Days
        {
            get
            {
                return days;
            }
            set
            {
                this.days = value;
            }
        }


        /// <summary>
        /// ����ʵ��
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = null;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        private FS.HISFC.DCP.Object.CommonReport commonReport =null;

        public FS.HISFC.DCP.Object.CommonReport CommonReport
        {
            get
            {
                return this.commonReport;
            }
            set
            {
                this.commonReport = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ѯ���ݳ�ʼ��
        /// </summary>
        private void InitQueryContent()
        {
            //��ʼ����ѯ����
            ArrayList al = new ArrayList();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            if (this.patientType == FS.SOC.HISFC.DCP.Enum.PatientType.O)
            {
                obj.ID = "ReportInfo";
                obj.Name = "ȫԺ������Ϣ��ѯ";
                al.Add(obj);
            }
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "PatientInfo";
            obj.Name = "���߲�ѯ";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "DeptReport";
            obj.Name = "�����ұ�����Ϣ��ѯ";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "DeptUnReport";
            obj.Name = "�����Ҳ��ϸ񱨿���ѯ";
            al.Add(obj);

            //obj = new FS.FrameWork.Models.NeuObject();
            //obj.ID = "deptLisResult";
            //obj.Name = "������ʵ���Ҽ�����Ա���";
            //al.Add(obj);

            //obj = new FS.FrameWork.Models.NeuObject();
            //obj.ID = "FeedBack";
            //obj.Name = "�����Ҵ�Ⱦ��©����ѯ";
            //al.Add(obj);

            this.cmbQueryContent.AddItems(al);
            this.cmbQueryContent.SelectedIndex = 0;
            this.cmbQueryContent.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Clear()
        {
            this.txtDocterNO.Text = "";
            this.txtPatientName.Text = "";
            this.txtPatientNO.Text = "";
            this.txtReportNO.Text = "";
            this.patient = null;
            //����ս���򿪼��صı��濨��Ϣ
            //this.alReport.Clear();
            if (this.AlReport == null || this.AlReport.Count==0)
            {
                this.commonReport = null;
 
            }
            //ʱ��
        }

        public void ClearSelected()
        {
            this.tvReport.SelectedNode = null;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.InitQueryContent();
            this.dtpBeginTime.Value = this.diseaseMgr.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value=this.dtpBeginTime.Value.AddDays(-days).Date;
            this.SetEnablellb(this.PatientType);
            this.Clear();
            this.tvReport.Nodes.Clear();

            this.TreeViewAddReportsIgnorState(this.alReport);

            return 1;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns>-1 ʧ�� 1�ɹ�</returns>
        public int Query()
        {
            this.tvReport.Nodes.Clear();
            this.tvReport.Select();
            switch (this.cmbQueryContent.Tag.ToString())
            {
                case "ReportInfo":
                    return this.QueryReportInfo();
                case "PatientInfo":
                    return this.QueryPatientInfo();
                case "DeptReport":
                    return this.QueryDeptReport();
                    break;
                case "DeptUnReport":
                    return this.QueryDeptReport(FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible);
                    break;
                case "FeedBack":
                    break;
            }
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        private int QueryReportInfo()
        {
            if (this.txtReportNO.Text.Trim() != "")
            {
                this.QueryByReportNO();
            }
            else if (this.txtPatientNO.Text.Trim() != "")
            {
                this.QueryByPatientNO();
            }
            else if (this.txtPatientName.Text != "")
            {
                this.QueryByPatientName();
            }
            else if (this.txtDocterNO.Text.Trim() != "")
            {
                this.QueryByDoctorNO();
            }
            else
            {
                this.QueryOldReport();
            }
            
            return 1;
        }

        /// <summary>
        /// ���濨�����Ų�ѯ
        /// </summary>
        private void QueryByReportNO()
        {
            //���濨�����Ų�ѯ

            this.tvReport.Nodes.Clear();
            ArrayList al = new ArrayList();
            FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
            report = this.diseaseMgr.GetCommonReportByNO(this.txtReportNO.Text);
            if (report != null)
            {
                if (report.ReportTime > this.dtpBeginTime.Value.Date && report.ReportTime < this.dtpEndTime.Value.AddDays(1).Date)
                {
                    al.Add(report);
                }

                this.TreeViewAddReportsIgnorState(al);
            }
        }

        /// <summary>
        /// ������ʷ����[�������� ɾ�� ���ϱ���������ʷ��ť����ʾ����д�����б�]
        /// </summary>
        public void QueryOldReport()
        {
            if (this.PatientType == FS.SOC.HISFC.DCP.Enum.PatientType.O)//������Ⱦ��
            {
                this.QueryHospitalReport();
            }
            else
            {
                this.QueryDeptReport();
            }
        }

        /// <summary>
        /// ��ѯȫԺ��Ⱦ������
        /// </summary>
        private void QueryHospitalReport()
        {
            ArrayList al = new ArrayList();
            if (this.PatientType == FS.SOC.HISFC.DCP.Enum.PatientType.O)
            {
                al = this.diseaseMgr.GetCommonReportListByReportTime(this.dtpBeginTime.Value.Date, this.dtpEndTime.Value.AddDays(1).Date);
            }
            if (al.Count > 0)
            {
                //��ʾ����
                this.TreeViewAddReportsIgnorState(al);
            }
        }

        #region ���Ҳ�ѯ
        private int QueryDeptReport()
        {
            return this.QueryDeptReport("AAA");
        }
        /// <summary>
        /// ���Ҳ�ѯ���濨
        /// </summary>
        private int QueryDeptReport(FS.SOC.HISFC.DCP.Enum.ReportState reportState)
        {
            return QueryDeptReport(((int)reportState).ToString());
        }

        public int QueryDeptReport(FS.SOC.HISFC.DCP.Enum.ReportState reportState,string deptID)
        {
            return QueryDeptReport(((int)reportState).ToString());
        } 

        /// <summary>
        /// ���Ҳ�ѯ���濨
        /// </summary>
        private int QueryDeptReport(string reportState)
        {
            ArrayList al = this.diseaseMgr.GetCommonReportListByMore("report_date", this.dtpBeginTime.Value.Date.ToString(), this.dtpEndTime.Value.AddDays(1).Date.ToString(), reportState, ((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID);
            if (al == null)
            {
                MessageBox.Show("���ݵ�¼���Ҳ�ѯ���߱�����Ϣʱ��������" + this.diseaseMgr.Err);
                return -1;
            }
            this.TreeViewAddReportsIgnorState(al);

            return 1;
        }

        #endregion

        #region ҽ�����Ų�ѯ��ҽ������д����

        /// <summary>
        /// ���ݱ���ҽ����ѯ���濨
        /// </summary>
        private void QueryByDoctorNO()
        {
            this.QueryByDoctorNO(this.txtDocterNO.Text.Trim());
        }

        public void QueryByDoctorNO(string doctorID)
        {
            this.tvReport.Nodes.Clear();
            if (string.IsNullOrEmpty(doctorID))
            {
                return;
            }
            ArrayList al = new ArrayList();
            foreach (FS.SOC.HISFC.DCP.Enum.ReportState s in System.Enum.GetValues(typeof(FS.SOC.HISFC.DCP.Enum.ReportState)))
            {
                al.AddRange(this.diseaseMgr.GetReportListByStateAndDoctor(Function.ConvertState(s), doctorID.PadLeft(6, '0')));
            }

            this.TreeViewAddReportsIgnorState(al);
        }

        public void QueryByDoctorNO(FS.SOC.HISFC.DCP.Enum.ReportState state,string doctorID)
        {
            this.tvReport.Nodes.Clear();
            if (string.IsNullOrEmpty(doctorID))
            {
                return;
            }
            ArrayList al = this.diseaseMgr.GetReportListByStateAndDoctor(Function.ConvertState(state), doctorID.PadLeft(6, '0'));

            this.TreeViewAddReportsIgnorState(al);
        }

        #endregion

        #region ���߲�ѯ

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <returns></returns>
        private int QueryPatientInfo()
        {
            if (this.txtPatientNO.Text.Trim() != "")
            {
                this.QueryByPatientNO();
            }
            else if (this.txtPatientName.Text != "")
            {
                this.QueryByPatientName();
            }
            else
            {
                if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
                {
                    this.QueryPatientByDeptIN();
                }
                else if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)    //--�޸�
                {
                    QueryPatientByDco();
                }
            }

            return 1;
        }

        /// <summary>
        /// ���ߺŲ�ѯ
        /// </summary>
        private void QueryByPatientNO()
        {
            //���ߺŲ�ѯ
            ArrayList al = new ArrayList();
            if (this.txtPatientNO.Text.Trim() == "")
            {
                return;
            }
            string patientno = this.txtPatientNO.Text.Trim().PadLeft(10, '0');

            al = this.diseaseMgr.GetCommonReportListByPatientNO(patientno);
            if (al == null)
            {
                MessageBox.Show("���ݻ���ID��ѯ���߱�����Ϣʱ��������" + this.diseaseMgr.Err);
                return;
            }
            else if (al.Count > 0)
            {
                this.TreeViewAddReportsIgnorState(al);
            }
            else             //�ޱ�����Ϣ ��ʾ������Ϣ
            {
                #region �ޱ�����Ϣʱ��ʾ������Ϣ

                //סԺ����
                if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
                {
                    al = this.patientProcess.GetPatientInfoByPatientNOAll(patientno);
                }
                //���ﻼ��
                else if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
                {
                    al = this.patientProcess.Query(patientno, DateTime.Now.AddYears(-1000));
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show(this, "û�л�����Ϣ��", "��ʾ>>");
                    this.tvReport.Nodes.Clear();
                    return;
                }
                this.TreeViewAddPatientInfo(al);

                #endregion
            }
        }

        /// <summary>
        /// ����������ѯ
        /// </summary>
        private void QueryByPatientName()
        {
            ArrayList al = new ArrayList();
            if (this.txtPatientName.Text.Trim() == "")
            {
                return;
            }
            string patientName = this.txtPatientName.Text.Trim();

            al = this.diseaseMgr.GetReportListByPatientName(patientName);
            if (al == null)
            {
                MessageBox.Show("���ݻ��������������濨��Ϣʱ��������" + this.diseaseMgr.Err);
                return;
            }
            else if (al.Count > 0)
            {
                this.TreeViewAddReportsIgnorState(al);
            }
            else            //�ޱ��濨ʱ��ʾ������Ϣ
            {
                //סԺ����
                if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
                {
                    al = this.patientProcess.GetPatientInfoByPatientName(patientName);
                }
                //���ﻼ��
                else if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
                {
                    al = this.patientProcess.QueryValidPatientsByName(patientName);
                }

                if (al == null || al.Count == 0)
                {
                    MessageBox.Show(this, "û�л�����Ϣ��", "��ʾ>>");
                    this.tvReport.Nodes.Clear();
                    return;
                }

                this.TreeViewAddPatientInfo(al);
            }
        }

        /// <summary>
        /// ����סԺ���Ҳ��һ���
        /// </summary>
        private void QueryPatientByDeptIN()
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.RADT.InStateEnumService instate = new FS.HISFC.Models.RADT.InStateEnumService();
            instate.ID = "I";
            //סԺ����
            if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
            {
                al = this.patientProcess.QueryPatientByDeptCode(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, instate);
            }

            if (al == null || al.Count == 0)
            {
                MessageBox.Show(this, "û�л�����Ϣ��", "��ʾ>>");
                this.tvReport.Nodes.Clear();
                return;
            }

            if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
            {
                this.TreeViewAddPatientInfo(al);
            }
        }
        /// <summary>
        /// ����ҽ����ѯ���ﲡ�� --�޸�
        /// </summary>
        private void QueryPatientByDco()
        {
            ArrayList al = new ArrayList();

            if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                al = this.patientProcess.QueryBySeeDoc(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID, FS.FrameWork.Function.NConvert.ToDateTime(this.diseaseMgr.GetSysDateTime()).AddDays(-this.days).Date, FS.FrameWork.Function.NConvert.ToDateTime(this.diseaseMgr.GetSysDateTime()).AddDays(1).Date, true);
            }

            if (al == null || al.Count == 0)
            {
                MessageBox.Show(this, "û�л�����Ϣ��", "��ʾ>>");
                this.tvReport.Nodes.Clear();
                return;
            }
            if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                //����ҽ��վ �����߲�ѯ��ʱ�� Ӧ����ʾ���ﻼ��
                this.TreeViewAddPatientInfo(al);
            }
        }

        #endregion

        /// <summary>
        /// ���ҷ�����Ϣ
        /// </summary>
        //private void QueryFeedBackByDept()
        //{
        //    ArrayList al =this.diseaseMgr.GetFeedBackByDoctAndDept(this.User.ID,this.User.Dept.ID);
        //    if (al.Count == 0)
        //    {
        //        MessageBox.Show(this, "û�п��ҷ�����Ϣ", "��ʾ>>");
        //        return;
        //    }
        //    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ر����ҷ�����Ϣ");
        //    Application.DoEvents();
        //    this.TreeViewAddFeedBack(al);
        //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        //}

        #region ���ڵ�����
        /// <summary>
        /// ��ʾ������Ϣ����״̬��
        /// </summary>
        /// <param name="al"></param>
        public void TreeViewAddReportsIgnorState(ArrayList al)
        {
            this.tvReport.Nodes.Clear();
            if (al == null || al.Count < 1)
            {
                return;
            }
            string msg = "";
            foreach (FS.HISFC.DCP.Object.CommonReport report in al)
            {
                switch (FS.FrameWork.Function.NConvert.ToInt32(report.State))
                {
                    case (int)FS.SOC.HISFC.DCP.Enum.ReportState.New:
                        this.TreeViewAddReport(Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New), "����", 4, report,ref msg);
                        break;
                    case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible:
                        this.TreeViewAddReport(report.State, "�ϸ�", 4, report,ref msg);
                        break;
                    case (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible://���
                        this.TreeViewAddReport(report.State, "���ϸ����޸ı�����", 3,Color.Red, report,ref msg);
                        break;
                    case (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel:
                        this.TreeViewAddReport(report.State, "��������", 3, Color.Red, report, ref msg);
                        break;
                    case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel://����
                        this.TreeViewAddReport(report.State, "����������", 3, report, ref msg);
                        break;
                    default:
                        break;
                }
            }

            this.tvReport.ExpandAll();
            if (this.tvReport.Nodes.Count == 1 && this.tvReport.Nodes[0].Nodes.Count == 1)
            {
                this.tvReport.SelectedNode = this.tvReport.Nodes[0].Nodes[0];
            }
        }

        /// <summary>
        ///  ��ʾ���濨[ͬ״̬�ı���]
        /// </summary>
        /// <param name="al">ͬ״̬�ı���</param>
        private void TreeViewAddReport(string nodeID, string nodeName, int nodeImageIndex, FS.HISFC.DCP.Object.CommonReport report,ref string msg)
        {
            this.TreeViewAddReport(nodeID, nodeName, nodeImageIndex, Color.Black, report, ref msg);

        }

        private void TreeViewAddReport(string nodeID, string nodeName, int nodeImageIndex,Color color, FS.HISFC.DCP.Object.CommonReport report, ref string msg)
        {
            System.Windows.Forms.TreeNode node = null;
            if (!this.tvReport.Nodes.ContainsKey(nodeID))
            {
                node = new TreeNode();
                node.Name = nodeID;
                node.Text = nodeName;
                node.ForeColor = color;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                node.Tag = "root";
                this.tvReport.Nodes.Add(node);
            }

            TreeNode childNode = new TreeNode();
            childNode.Tag = report;
            if (report.Patient.Name == null || report.Patient.Name == "")
            {
                childNode.Text = report.PatientParents + "[" + report.ReportNO + "]" + report.ExtendInfo3;
            }
            else
            {
                childNode.Text = report.Patient.Name + "[" + report.ReportNO + "]" + report.ExtendInfo3; ;
            }
            childNode.Name = report.ID;

            childNode.ImageIndex = nodeImageIndex;
            childNode.SelectedImageIndex = 2;

            this.tvReport.Nodes[nodeID].Nodes.Add(childNode);

            if (nodeID == Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible) && report.ReportDoctor.ID == this.diseaseMgr.Operator.ID)
            {
                msg += childNode.Text + "||";
            }

        }

        /// <summary>
        /// ��ʾ������Ϣ���������סԺ��
        /// </summary>
        private void TreeViewAddPatientInfo(ArrayList al)
        {
            this.tvReport.Nodes.Clear();
            if (al == null || al.Count == 0)
            {
                return;
            }
            TreeNode node = null;
            if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
            {
                node = new TreeNode("�����б�--[����][��Ժ����][��Ժ����]");
                node.ImageIndex = 1;
                foreach (FS.HISFC.Models.RADT.PatientInfo patient in al)
                {
                    TreeNode childNode = new TreeNode();

                    childNode.ImageIndex = 4;
                    childNode.SelectedImageIndex = 2;
                    childNode.Text = "[" + patient.Name + "][" + patient.PVisit.InTime.ToShortDateString() + "][" + patient.PVisit.PatientLocation.Dept.Name + "]";
                    childNode.Tag = patient;
                    node.Nodes.Add(childNode);
                }
            }
            else if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                node = new TreeNode("�����б�--[����][�Һ�����][�Һſ���]");
                node.ImageIndex = 1;
                foreach (FS.HISFC.Models.Registration.Register patient in al)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.ImageIndex = 4;
                    childNode.SelectedImageIndex = 2;
                    childNode.Text = "[" + patient.Name + "][" + patient.DoctorInfo.SeeDate.ToShortDateString() + "][" + patient.DoctorInfo.Templet.Dept.Name + "]";
                    childNode.Tag = patient;
                    node.Nodes.Add(childNode);
                }
            }

            this.tvReport.Nodes.Add(node);
            this.tvReport.ExpandAll();
            if (this.tvReport.Nodes.Count > 0 && node.Nodes.Count>0)
            {
                this.tvReport.SelectedNode = node.Nodes[0];
            }
            //else
            //{
            //    node = new TreeNode("�����б�--[����][����][����]");
            //    foreach (FS.HISFC.Models.RADT.PatientInfo patient in al)
            //    {
            //        TreeNode childNode = new TreeNode();
            //        childNode.Text = "[" + patient.Name + "][" + patient.PVisit.InTime.ToShortDateString() + "][" + patient.PVisit.PatientLocation.Dept.Name + "]";
            //        childNode.Tag = patient;
            //    }
            //}


        }

        /// <summary>
        /// ��ʾ�����б�[��״̬���б������ӷּ��ڵ�]
        /// </summary>
        private void TreeViewAddReports(ArrayList al, FS.SOC.HISFC.DCP.Enum.ReportState reportState)
        {
            try
            {
                string nodeText = "";
                //���ڵ����� ��ʾ����״̬
                int imagindex = 4;
                Color color = Color.Black;
                switch (reportState)
                {
                    case FS.SOC.HISFC.DCP.Enum.ReportState.New:
                        nodeText = "����";
                        break;
                    case FS.SOC.HISFC.DCP.Enum.ReportState.Eligible:
                        nodeText = "�ϸ�";
                        imagindex = 4;
                        break;
                    case FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible://���
                        nodeText = "���ϸ����޸ı�����";
                        imagindex = 3;
                        color = System.Drawing.Color.Red;
                        break;
                    case FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel:
                        nodeText = "��������";
                        imagindex = 3;
                        color = System.Drawing.Color.Red;
                        break;
                    case FS.SOC.HISFC.DCP.Enum.ReportState.Cancel://����
                        nodeText = "����������";//����������
                        imagindex = 3;
                        break;
                    default:
                        break;
                }
                
                //�ӽڵ���� ��ʾ�������� ������
                string msg = "";
                foreach (FS.HISFC.DCP.Object.CommonReport report in al)
                {
                    this.TreeViewAddReport(Function.ConvertState(reportState), nodeText, imagindex, color, report, ref msg);
                }
                if (msg != "")
                {
                    MessageBox.Show(this, "����д��" + msg + "���濨���ϸ���鿴[�˿�ԭ��]��������Ӧ�޸�", "�˿�ԭ��");
                }
                this.tvReport.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "������ʷ������Ϣʧ��" + ex.Message, "����>>");
            }
        }

        #endregion

        /// <summary>
        /// ����llbPatientNO�Ŀ���
        /// </summary>
        /// <param name="patientType"></param>
        private void SetEnablellb(FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            if (FS.SOC.HISFC.DCP.Enum.PatientType.C == patientType)
            {
                this.llbPatientNO.Enabled = false;
                this.llbPatientNO.Text = "���￨�ţ�";
                this.llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;
            }
            else if (FS.SOC.HISFC.DCP.Enum.PatientType.I == patientType)
            {
                this.llbPatientNO.Enabled = false;
                this.llbPatientNO.Text = "ס Ժ �ţ�";
                this.llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            }
            else
            {
                this.llbPatientNO.Enabled = true;
                this.llbPatientNO.Text = "���￨�ţ�";
                this.llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;
            }
        }

        /// <summary>
        /// ɾ�����濨
        /// </summary>
        /// <param name="ID">���</param>
        public int DeleteReport(string ID)
        {
            System.Windows.Forms.DialogResult dr = new DialogResult();
            dr = MessageBox.Show("ȷ��Ҫɾ�����濨��\nɾ�����ָܻ���", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                int param = this.diseaseMgr.DeleteCommonReport(ID);
                if (param == 1)
                {
                    this.MyMessageBox("���濨ɾ���ɹ�!", "��ʾ>>");
                    return -1;
                }
                else if (param == 0)
                {
                    this.MyMessageBox("���濨�Ѿ����޶������ �޷�����ɾ��", "��ʾ");
                }
                else
                {
                    this.MyMessageBox("���濨ɾ��ʧ��!" + this.diseaseMgr.Err, "����>>");
                }

                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        public void DeleteReport()
        {
            if ( this.tvReport.SelectedNode == null)
            {
                return;
            }
            if (this.tvReport.SelectedNode.Tag.ToString() == "root")
            {
                return;
            }

            FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
            report = (FS.HISFC.DCP.Object.CommonReport)this.tvReport.SelectedNode.Tag;
            if (report != null && report.ID != "")
            {
                if (this.DeleteReport(report.ID) == 0)
                {
                    this.tvReport.Nodes.Remove(this.tvReport.SelectedNode);

                    //��ѯ�¼�
                    ArrayList alTempReport = new ArrayList();
                    foreach (ArrayList al in this.AlReport)
                    {
                        ArrayList altemp = new ArrayList();
                        foreach (FS.HISFC.DCP.Object.CommonReport rpt in al)
                        {
                            if (rpt.ID != report.ID)
                            {
                                altemp.Add(rpt);
                            }
                        }
                        if (altemp != null && altemp.Count > 0)
                        {
                            alTempReport.Add(altemp);
                        }
                    }
                    this.AlReport = alTempReport;
                    this.ReflashTreeView(this.AlReport);
                }
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

        /// <summary>
        /// ���б�ˢ��
        /// </summary>
        /// <param name="alAllState">�б��е����б���</param>
        private void ReflashTreeView(ArrayList alAllState)
        {
            //���������ʱˢ�±����б� ���Ƶ��㷨

            //���ڳ�ʼ����ǰ���濨�����Ѿ���ֵ������������״̬�ı䡣���԰�״̬���·�����ʾ

            try
            {
                ArrayList alNew = new ArrayList();//�¼�
                ArrayList alGood = new ArrayList();//�ϸ�
                ArrayList alBad = new ArrayList();//���ϸ�
                ArrayList alCancel = new ArrayList();//����������

                //ArrayList alfive = new ArrayList();//����������


                foreach (ArrayList alonestate in alAllState)
                {
                    foreach (FS.HISFC.DCP.Object.CommonReport report in alonestate)
                    {
                        FS.HISFC.DCP.Object.CommonReport tempreport = new FS.HISFC.DCP.Object.CommonReport();
                        tempreport = this.diseaseMgr.GetCommonReportByID(report.ID);
                        switch (Int32.Parse(tempreport.State))
                        {
                            case (int)FS.SOC.HISFC.DCP.Enum.ReportState.New:
                                alNew.Add(tempreport);
                                break;
                            case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible:
                                alGood.Add(tempreport);
                                break;
                            case (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible:
                                alBad.Add(tempreport);
                                break;
                            case (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel:
                            case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel:
                                alCancel.Add(tempreport);
                                break;
                        }
                    }
                }
                this.tvReport.Nodes.Clear();
                this.TreeViewAddReports(alNew, FS.SOC.HISFC.DCP.Enum.ReportState.New);
                this.TreeViewAddReports(alGood, FS.SOC.HISFC.DCP.Enum.ReportState.Eligible);
                this.TreeViewAddReports(alBad, FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible);
                this.TreeViewAddReports(alCancel, FS.SOC.HISFC.DCP.Enum.ReportState.Cancel);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ˢ���б����>>" + ex.Message);
            }
        }

        public void QueryByCache()
        {
            if (this.AlReport != null && this.AlReport.Count > 0)
            {
                this.ReflashTreeView(this.AlReport);
            }
            else
            {
                this.QueryOldReport();
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ���ݲ������ߵ�����ȷ�������￨�Ż���סԺ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llbPatientNO_Click(object sender, EventArgs e)
        {
            if (this.llbPatientType==FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                this.llbPatientNO.Text = "ס Ժ �ţ�";
                this.llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            }
            else
            {
                this.llbPatientNO.Text = "���￨�ţ�";
                this.llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;
            }
        }

        /// <summary>
        /// ��ѯ������ı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbQueryContent_SelectedValueChanged(object sender, EventArgs e)
        {
            this.Clear();
            this.tvReport.Nodes.Clear();

            if (this.cmbQueryContent.Tag.ToString() == "PatientInfo")
            {
                //���߻�����Ϣ��ѯ

                this.dtpBeginTime.Enabled = false;
                this.dtpEndTime.Enabled = false;
                this.txtReportNO.Enabled = false;
                this.txtDocterNO.Enabled = false;
                this.txtPatientName.Enabled = true;
                this.txtPatientNO.Enabled = true;
                this.SetEnablellb(this.patientType);
            }
            else if (this.cmbQueryContent.Tag.ToString() == "ReportInfo")
            {
                //���߱�����Ϣ��ѯ

                this.dtpBeginTime.Enabled = true;
                this.dtpEndTime.Enabled = true;
                this.txtReportNO.Enabled = true;
                this.txtDocterNO.Enabled = true;
                this.txtPatientNO.Enabled = true;
                this.txtPatientName.Enabled = true;
                this.SetEnablellb(this.patientType);
            }
            else if (this.cmbQueryContent.Tag.ToString() == "DeptReport" || this.cmbQueryContent.Tag.ToString() == "DeptUnReport" || this.cmbQueryContent.Tag.ToString() == "FeedBack")
            {
                //���ұ�����Ϣ��ѯ
                this.dtpBeginTime.Enabled = true;
                this.dtpEndTime.Enabled = true;
                this.txtDocterNO.Enabled = false;
                this.txtPatientNO.Enabled = false;
                this.txtPatientName.Enabled = false;
                this.txtReportNO.Enabled = false;
            }
            //else if (this.cmbQueryContent.Tag.ToString() == "choose")
            //{
            //    this.txtDoctor.Enabled = false;
            //    this.txtInPatienNo.Enabled = false;
            //    this.txtName.Enabled = false;
            //    this.txtReportNo.Enabled = false;
            //    this.dtBegin.Enabled = false;
            //    this.dtEnd.Enabled = false;
            //}
            //else if (this.cmbQueryContent.Tag.ToString() == "deptLisResult")
            //{
            //    this.groupBox3.Enabled = true;
            //    this.txtDoctor.Enabled = false;
            //    this.txtInPatienNo.Enabled = false;
            //    this.txtName.Enabled = false;
            //    this.txtReportNo.Enabled = false;
            //    this.dtBegin.Enabled = false;
            //    this.dtEnd.Enabled = false;
            //}
        }

        /// <summary>
        /// ѡ��ڵ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvReport_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is FS.HISFC.Models.RADT.PatientInfo || e.Node.Tag is FS.HISFC.Models.Registration.Register)
            {
                this.commonReport = null;
                this.patient = e.Node.Tag as FS.HISFC.Models.RADT.Patient;
                this.ShowInfo.Invoke(this.patient);
            }
            else if(e.Node.Tag is FS.HISFC.DCP.Object.CommonReport)
            {
                this.commonReport = e.Node.Tag as FS.HISFC.DCP.Object.CommonReport;
                this.patient = this.commonReport.Patient;
                this.ShowInfo.Invoke(this.commonReport);
            }
        }

        #endregion 

        private void txtPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
                {
                    FS.HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
                    if (this.txtPatientNO.Text.Trim() == "")
                    {
                        this.MyMessageBox("�����벡����!", "��ʾ<<");
                        this.txtPatientNO.Focus();
                        this.txtPatientNO.SelectAll();
                        return;
                    }
                    string strCardNO = this.txtPatientNO.Text.Trim();
                    FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();
                    int iTemp = feeManage.ValidMarkNO(strCardNO, ref objCard);
                    if (iTemp <= 0 || objCard == null)
                    {
                        this.MyMessageBox("��Ч���ţ�����ϵ����Ա��", "err");
                        this.txtPatientNO.Focus();
                        this.txtPatientNO.SelectAll();
                        return;
                    }
                    string cardNo = objCard.Patient.PID.CardNO;
                    this.txtPatientNO.Text = cardNo;
                }

                this.Query();
            }
        }
    }

    /// <summary>
    /// ״̬
    /// </summary>
    public enum enumTreeViewType
    {
        PatientInfo,
        ReportInfo,
        FeedBackInfo
    }
}
