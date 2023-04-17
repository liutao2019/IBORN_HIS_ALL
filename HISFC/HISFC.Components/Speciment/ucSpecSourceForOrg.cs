using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucSpecSourceForOrg : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ˽�б���
        private ucSubSpecForOrg ucSpecOrg;
        private ucDiagnose ucDiaInFlp;
        private ucPatientInfo ucPatient;

        private SpecDiagnose diagInfo;
        private SpecSource specSource;
        private SpecSourcePlan specSourcePlan;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        private SpecPatient specPatient;
        private SubSpec subSpec;
        private OperApply operApply;

        private ArrayList arrSpecSourcePlan;
        private ArrayList alDepts;

        private DisTypeManage disTypeManage;
        private SpecSourcePlanManage specSourcePlanManage;
        private OperApplyManage operApplyManage;
        private SpecSourceManage specSourceMange;
        private SubSpecManage subSpecManage;
        private DiagnoseManage diagManage;
        private PatientManage specPatientManage;
        private SpecBarCodeManage barCodeManage;

        private string title;
        private string tumorPro;//��������
        private string patientId;
        private string oper;
        private string detSeq;//��ǰ�걾�ķ�װ�걾�����к�

        private int specIndex;
        private int specID;

        private int times = 0;

        /// <summary>
        /// ԭ�걾ID
        /// </summary>
        public int SpecId
        {
            get
            {
                return specID;
            }
            set
            {
                specID = value;
            }
        }
        #endregion

        private string bradyIp = "";
        public string BradyIp
        {
            get
            {
                return bradyIp;
            }
            set
            {
                bradyIp = value;
            }
        }

        #region ���캯��
        public ucSpecSourceForOrg()
        {
            InitializeComponent();
            arrSpecSourcePlan = new ArrayList();
            //arrSpecOrgInFlp = new ArrayList();
            ucSpecOrg = new ucSubSpecForOrg();
            specSource = new SpecSource();
            disTypeManage = new DisTypeManage();
            specSourcePlan = new SpecSourcePlan();
            specSourcePlanManage = new SpecSourcePlanManage();
            specSourceMange = new SpecSourceManage();
            operApplyManage = new OperApplyManage();
            subSpecManage = new SubSpecManage();
            barCodeManage = new SpecBarCodeManage();
            subSpec = new SubSpec();
            ucPatient = new ucPatientInfo();
            specPatient = new SpecPatient();
            diagManage = new DiagnoseManage();
            diagInfo = new SpecDiagnose();
            specPatientManage = new PatientManage();
            ucDiaInFlp = new ucDiagnose();
            alDepts = new ArrayList();
            specIndex = 1;
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            tumorPro = "";
            title = "��֯�걾¼��";
            patientId = "";
            oper = "";
            detSeq = "";
        }
        #endregion

        #region ˽�к�������ʼ������

        private string GenerateSql()
        {
            string sql = "select distinct * from SPEC_OPERAPPLY where ORGORBLOOD='O'";
            if (dtpStart.Value != null)
                sql += " and OPERTIME>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
            if (dtpEnd.Value != null)
                sql += " and OPERTIME<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
            if (cmbOperDept.Tag != null && cmbOperDept.Text != "") sql += " and OPERDEPTNAME = '" + cmbOperDept.Text.Trim() + "'";
            return sql;
        }

        /// <summary>
        /// �����������뵥
        /// </summary>
        private void LoadOperApply()
        {
            string sql = GenerateSql();
            ArrayList arrList = operApplyManage.GetOperApplyBySql(sql);
            if (arrList == null || arrList.Count <= 0)
            {
                return;
            }
            foreach (OperApply apply in arrList)
            {
                //δȡ�걾
                if (apply.HadCollect == "0")
                {
                    TreeNode tnNotGet = tvApply.Nodes[0];
                    TreeNode tn = new TreeNode();
                    tn.Text = apply.PatientName + " ��" + apply.OperTime.ToString() + "�� " + apply.OperLocation;
                    tn.Tag = apply;
                    tnNotGet.Nodes.Add(tn);
                }

                //���ռ�
                if (apply.HadCollect == "1")
                {
                    TreeNode tnNotGet = tvApply.Nodes[1];
                    TreeNode tn = new TreeNode();
                    tn.Text = apply.PatientName + " ��" + apply.OperDeptName + "�� " + apply.OperLocation +" "+apply.HadCollect; 
                    tn.Tag = apply;
                    tnNotGet.Nodes.Add(tn);
                }

                //ȡ����ȡ
                if (apply.HadCollect == "3")
                {
                    TreeNode tnNotGet = tvApply.Nodes[2];
                    TreeNode tn = new TreeNode();
                    tn.Text = apply.PatientName + " ��" + apply.OperDeptName + "�� " + apply.OperLocation;
                    tn.Tag = apply;
                    tnNotGet.Nodes.Add(tn);
                }
            }

            tvApply.Nodes[0].Text = "δȡ����  (��:" + tvApply.Nodes[0].Nodes.Count.ToString() + " ��)";
            tvApply.Nodes[1].Text = "��ȡ����  (��:" + tvApply.Nodes[1].Nodes.Count.ToString() + " ��)";
            tvApply.Nodes[2].Text = "��ȡ������  (��:" + tvApply.Nodes[2].Nodes.Count.ToString() + " ��)";
            //if (tvApply.Nodes[0].Nodes.Count > 0)
            //{
            //    tvApply.SelectedNode = tvApply.Nodes[0];
            //}
        }

        /// <summary>
        /// �󶨲�������
        /// </summary>
        private void DisTypeBinding()
        {
            //Dictionary<int, string> dicDisType = disTypeManage.GetOrgDisType();
            //if (dicDisType.Count > 0)
            //{
            //    BindingSource bs = new BindingSource();
            //    bs.DataSource = dicDisType;
            //    cmbDisType.DisplayMember = "Value";
            //    cmbDisType.ValueMember = "Key";
            //    cmbDisType.DataSource = bs;
            //}
            //cmbDisType.Text = "";
            ArrayList alDisType = disTypeManage.GetAllValidDisType();
            if (alDisType != null)
            {
                if (alDisType.Count > 0)
                {
                    cmbDisType.AddItems(alDisType);
                }
            }
            cmbDisType.Text = "";
        }

        /// <summary>
        /// ��ȡ��¼�û���ID
        /// </summary>
        private void GetLogEmpInfo()
        {
            loginPerson = new Employee();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            txtOperDoc.Text = loginPerson.Name;
        }

        private void GetAllEmp()
        {
            FS.HISFC.BizLogic.Manager.Person personList = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList arrPerson = personList.GetEmployee(EnumEmployeeType.D);
            cmbSendDoc.AddItems(arrPerson);
            cmbOperDoc.AddItems(arrPerson);
        }

        /// <summary>
        /// ����flp�еĿؼ�
        /// </summary>
        private void FlpBinding()
        {
            //tpTumor.Tag = ucSpecOrg;
            //tpTumor.Controls.Add(ucSpecOrg);
            tpTumor.Dispose();

            //��ʼ���ʹ沿��
            FS.HISFC.BizLogic.Manager.Department manager = new FS.HISFC.BizLogic.Manager.Department();
            alDepts = manager.GetDeptmentByType("I");//.GetMultiDept(loginPerson.ID);
            this.cmbDept.AddItems(alDepts);
            this.cmbOperDept.AddItems(alDepts);
            this.cmbDept.Tag = loginPerson.Dept.ID;

            //��ʼ���ʹ�ҽ��
            FS.HISFC.BizLogic.Manager.Person personList = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList arrPerson = personList.GetEmployeeAll();
            cmbSendDoc.AddItems(arrPerson);
            //this.cmbSendDoc.Tag = loginPerson.ID;
            this.cmbSendDoc.Text = "";
            this.cmbSendDoc.Tag = "";
            cmbOperDept.AddItems(alDepts);
            //���ز�����Ϣ�ؼ�
            this.flpPatientInfo.Controls.Add(ucPatient);
            //    flpCureInfo.Controls.Add(ucCureFunInFlp);
            flpDiagInfo.Controls.Add(ucDiaInFlp);
        }

        private void ResetForm()
        {
            this.detSeq = "";
            ucPatient.ClearControlContent();
            for (int i = tpColInfo.TabPages.Count - 1; i >= 0; i--)
            {
                if (tpColInfo.TabPages[i].Tag != null)
                {
                    tpColInfo.TabPages[i].Dispose();
                }
            }

            SetColSpecDet("C");
            ucDiaInFlp.ClearContent();
            chkFirst.Checked = false;
            chkSecond.Checked = false;
            chkTransfer.Checked = false;
            chkMed.Checked = false;
            chkRad.Checked = false;
            chkOther.Checked = false;
            chkNone.Checked = false;
            txtOperPos.Text = "";
            txtComment.Text = "";
            txtFlowCode.Text = "";
            cmbSendDoc.Tag = "";
            cmbSendDoc.Text = "";
            cmbOperDoc.Tag = "";
            cmbOperDoc.Text = "";
            txtMedComment.Text = "";
            txtOperDoc.Text = loginPerson.Name;
            cmbDisType.Text = "";
            txtTumorPos.Tag = "";
            txtTumorPos.Text = "";
            cmbDiagnose.Text = "";
            cmbDiagnose.Tag = "";
            cmbDept.Text = "";
            cmbDept.Tag = "";

        }

        private void TumorTypeBinding()
        {
            Dictionary<int, Constant.TumorType> dicTumorTypeSource = new Dictionary<int, Constant.TumorType>();
            for (int i = 1; i <= 8; i++)
            {
                if (i.ToString() == ((Constant.TumorType)i).ToString())
                    continue;
                dicTumorTypeSource.Add(i, (Constant.TumorType)i);
            }
            BindingSource bsTmp = new BindingSource();
            bsTmp.DataSource = dicTumorTypeSource;
            cmbTumorType.ValueMember = "Key";
            cmbTumorType.DisplayMember = "Value";
            cmbTumorType.DataSource = bsTmp;
            cmbTumorType.SelectedIndex = 0;
        }

        private int ValidateData()
        {
            if (txtSpecCode.Text.Trim() == "")
            {
                MessageBox.Show("Դ�걾ID����Ϊ�գ������½���ť�����µ�ID��", title);
                return 0;
            }
            try
            {
                Convert.ToInt32(txtSpecCode.Text.Trim());
            }
            catch
            {
                MessageBox.Show("�걾�ű���������", title);
                return 0;
            }
            if (txtSpecCode.Text.Trim() == "")
            {
                MessageBox.Show("�걾�Ų���Ϊ��", title);
                return 0;
            }
            if (cmbDept.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ�����", title);
                return 0;
            }
            if (operApply != null)
            {
                if (cmbDept.Tag.ToString() != operApply.OperDeptId.ToString())
                {
                    DialogResult result = MessageBox.Show("�ʹ�������������뵥��Ϣ��һ�£��Ƿ������", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No)
                    {
                        return 0;
                    }
                }
            }
            if (cmbSendDoc.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ��ҽ��", title);
                return 0;
            }
            tumorPro = "";
            if (chkFirst.Checked) tumorPro = "1";
            if (chkSecond.Checked) tumorPro += "2";
            if (chkTransfer.Checked) tumorPro += "3";
            if (tumorPro != null && tumorPro.Length > 2)
            {
                MessageBox.Show("�걾���Բ���ͬʱ����2�����ϣ�", title);
                return 0;
            }
            return 1;
        }

        /// <summary>
        ///��ԭ�걾����
        /// </summary>
        private void SpecSourceSet()
        {
            if (oper == "" && !ucPatient.IsExisted)
            {
                specSource.Patient.PatientID = Convert.ToInt32(patientId);
            }
            if (ucPatient.IsExisted)
                specSource.Patient.PatientID = specPatient.PatientID;
            specSource.SpecId = Convert.ToInt32(txtSpecCode.Text.Trim());
            specSource.HisBarCode = txtFlowCode.Text.Trim();
            Employee operDoc = new Employee();
            operDoc.Name = txtOperDoc.Text;
            specSource.OperEmp = operDoc;
            Employee sendDoctor = new Employee();
            sendDoctor.ID = cmbSendDoc.Tag == null ? "" : cmbSendDoc.Tag.ToString();
            specSource.SendDoctor = sendDoctor;
            specSource.OperTime = dtpOperTime.Value;
            specSource.OrgOrBoold = "O";
            //Get PatientNo by Blood HisbarCode
            specSource.InPatientNo = specPatient.InHosNum;
            //Get CardNo by Blood HisbarCode
            specSource.CardNo = specPatient.CardNo;
            specSource.Commet = txtComment.Text;
            if (cmbDept.Tag != null) specSource.DeptNo = cmbDept.Tag.ToString();
            DiseaseType disType = new DiseaseType();
            disType.DisTypeID = Convert.ToInt32(cmbDisType.Tag.ToString());
            specSource.DiseaseType = disType;
            specSource.ICCardNO = "";
            specSource.SendTime = dtpSendDate.Value;

            if (chkIsHis.Checked)
                specSource.IsHis = '1';
            else specSource.IsHis = '0';

            //���ò���������Ϣ
            string period = "";
            if (chkMed.Checked) period = Convert.ToInt32(Constant.GetPeriod.���ƺ�).ToString();
            if (chkRad.Checked) period += Convert.ToInt32(Constant.GetPeriod.���ƺ�).ToString();// "4";            
            if (chkNone.Checked) period += Convert.ToInt32(Constant.GetPeriod.��).ToString();// "8";
            if (chkOther.Checked) period += Convert.ToInt32(Constant.GetPeriod.����).ToString();// "7";
            specSource.GetSpecPeriod = period;
            specSource.OperPosName = txtOperPos.Text;
            specSource.MedComment = txtMedComment.Text;

            if (cmbDept.Tag != null && cmbDept.Text.Trim() != "")
            {
                specSource.DeptNo = cmbDept.Tag.ToString();
            }
            if (cmbGrp.Tag != null && cmbGrp.Text.Trim() != "")
            {
                specSource.MedGrp = cmbGrp.Tag.ToString();
            }
            specSource.TumorPor = tumorPro;
            if (cmbOperDoc.Tag != null && cmbOperDoc.Text.Trim() != "")
            {
                specSource.MediDoc.MainDoc.ID = cmbOperDoc.Tag.ToString();
                specSource.MediDoc.MainDoc.Name = cmbOperDoc.Text;
            }
            if (operApply != null && operApply.OperId.Trim() != "" && specSource.OperApplyId == "")
            {
                specSource.OperApplyId = operApply.OperId;
            }
            if (cmbDiagnose.Text.Trim() != "")
            {
                specSource.Ext2 = cmbDiagnose.Text.Trim();
            }
        }

        /// <summary>
        /// ���ݶ�ȡ�ı걾Դ��Ϣ����ҳ����Ӧ�Ŀؼ�ֵ
        /// </summary>
        private void GetSpecInfo()
        {

            if (specSource.DiseaseType.DisTypeID != 0)
            {
                cmbDisType.Tag = specSource.DiseaseType.DisTypeID;
                try
                {
                    cmbDisType.Text = (disTypeManage.SelectDisByID(specSource.DiseaseType.DisTypeID.ToString())).DiseaseName;
                }
                catch
                { }
            }
            chkIsHis.Checked = specSource.IsHis == '1' ? true : false;
            txtFlowCode.Text = specSource.HisBarCode;
            txtSpecCode.Text = specSource.SpecId.ToString();
            cmbDept.Tag = specSource.DeptNo;
            cmbSendDoc.Tag = specSource.SendDoctor.ID;
            txtOperDoc.Text = specSource.OperEmp.Name;
            dtpOperTime.Value = specSource.OperTime;
            dtpSendDate.Value = specSource.SendTime;
            txtComment.Text = specSource.Commet;
            txtOperPos.Text = specSource.OperPosName;
            txtSpecCode.Text = specSource.SpecId.ToString();
            cmbDiagnose.Text = specSource.Ext2;
            if (!string.IsNullOrEmpty(specSource.Ext2))
            {
                cmbDiagnose.Text = specSource.Ext2.ToString();
            }
            this.specID = specSource.SpecId;

            char[] tumorPor = specSource.TumorPor.ToCharArray();
            foreach (char p in tumorPor)
            {
                Constant.TumorPro pro = (Constant.TumorPro)Convert.ToInt32(p.ToString());
                if (pro == Constant.TumorPro.������)
                {
                    chkSecond.Checked = true;
                }
                if (pro == Constant.TumorPro.ԭ����)
                {
                    chkFirst.Checked = true;
                }
                if (pro == Constant.TumorPro.ת�ư�)
                {
                    chkTransfer.Checked = true;
                }
            }
            char[] tumorType = specSource.GetSpecPeriod.ToCharArray();
            foreach (char t in tumorType)
            {
                Constant.GetPeriod period = (Constant.GetPeriod)Convert.ToInt32(t.ToString());
                if (period == Constant.GetPeriod.���ƺ�)
                {
                    chkRad.Checked = true;
                }
                if (period == Constant.GetPeriod.���ƺ�)
                {
                    chkMed.Checked = true;
                }
                if (period == Constant.GetPeriod.����)
                {
                    chkOther.Checked = true;
                }
                if (period == Constant.GetPeriod.��)
                {
                    chkNone.Checked = true;
                }
            }
            txtMedComment.Text = specSource.MedComment;
            GetPatientInfo(specSource.Patient.PatientID.ToString());
            GetSavePlan(txtSpecCode.Text.Trim());
            GetDiagnoseInfo(txtSpecCode.Text.Trim());
        }

        /// <summary>
        /// �����������뵥����ҳ��
        /// </summary>
        /// <param name="operApply">�������뵥</param>
        private void GetSpecInfoByOperApply()
        {
            chkIsHis.Checked = true;
            cmbDept.Tag = operApply.OperDeptId;
            cmbDept.Text = operApply.OperDeptName;
            cmbSendDoc.Tag = operApply.MediDoc.MainDoc.ID;
            cmbSendDoc.Text = operApply.MediDoc.MainDoc.Name;
            txtOperPos.Text = operApply.OperName;

            cmbOperDoc.Tag = operApply.MediDoc.MainDoc.ID;
            cmbOperDoc.Text = operApply.MediDoc.MainDoc.Name;

            if (!string.IsNullOrEmpty(operApply.MainDiaName))
            {
                cmbDiagnose.Text = operApply.MainDiaName;
            }

            char[] tumorPor = operApply.TumorPor.ToCharArray();
            foreach (char p in tumorPor)
            {
                Constant.TumorPro pro = (Constant.TumorPro)Convert.ToInt32(p.ToString());
                if (pro == Constant.TumorPro.������)
                {
                    chkSecond.Checked = true;
                }
                if (pro == Constant.TumorPro.ԭ����)
                {
                    chkFirst.Checked = true;
                }
                if (pro == Constant.TumorPro.ת�ư�)
                {
                    chkTransfer.Checked = true;
                }
            }
            char[] tumorType = operApply.GetPeriod.ToCharArray();
            foreach (char t in tumorType)
            {
                Constant.GetPeriod period = (Constant.GetPeriod)Convert.ToInt32(t.ToString());
                if (period == Constant.GetPeriod.���ƺ�)
                {
                    chkRad.Checked = true;
                }
                if (period == Constant.GetPeriod.���ƺ�)
                {
                    chkMed.Checked = true;
                }
                if (period == Constant.GetPeriod.����)
                {
                    chkOther.Checked = true;
                }
                if (period == Constant.GetPeriod.��)
                {
                    chkNone.Checked = true;
                }
            }
            //string sequence = "";
            //specSourceMange.GetNextSequence(ref sequence);
            specSource.HisBarCode = operApply.OperApplyId.ToString().PadLeft(12, '0');
            //txtSpecCode.Text = sequence;
            txtFlowCode.Text = specSource.HisBarCode;
            //specID = Convert.ToInt32(sequence);
            //�����������뵥�ϵ�סԺ��ˮ�Ų�ѯ�����ò�����Ϣ
            ucPatient.GetPatientByInHosNum(operApply.InHosNum);
        }

        /// <summary>
        /// ����ÿ����װ�걾������
        /// </summary>
        /// <param name="oper">D�����֣�P��PlanID T:�������ﲿλ, S:����sequence��C,��շ�װ�걾��Sequence</param>
        private void SetColSpecDet(string oper)
        {
            foreach (Control c in tpColInfo.Controls)
            {
                if (c.GetType().ToString().Contains("TabPage") && c.Tag != null)
                {
                    ucSubSpecForOrg tmp = c.Tag as ucSubSpecForOrg;
                    tmp.DisTypeId = Convert.ToInt32(cmbDisType.Tag.ToString());
                    GroupBox grp = tmp.Controls[0] as GroupBox;
                    foreach (Control uc in grp.Controls)
                    {
                        if (uc.Name == "flpPlan")
                        {
                            FlowLayoutPanel fp = uc as FlowLayoutPanel;
                            foreach (Control fc in fp.Controls)
                            {
                                ucColForOrg ucDet = fc as ucColForOrg;
                                ucDet.DisTypeName = cmbDisType.Text;
                                //ucDet.SpecId = specSource.SpecId;
                                if (this.txtTumorPos.Tag != null)
                                {
                                    ucDet.TumorPosCode = txtTumorPos.Tag.ToString();
                                }
                                ucDet.TumorName = txtTumorPos.Text;
                                if (oper == "P") ucDet.SpecSourcePlan.PlanID = 0;
                                //if ("S") ucDet.SeqFromSource = detSeq;
                                //if (detSeq.Length > 3) 
                                ucDet.SeqFromSource = detSeq;

                                try
                                {
                                    specID = Convert.ToInt32(txtSpecCode.Text.Trim());
                                }
                                catch
                                {
                                    specID = 0;
                                }

                                ucDet.SpecId = specID;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ȡ������
        /// </summary>
        private void CancelSpec()
        {
            TreeNode tn = tvApply.SelectedNode;
            if (tn == null)
            {
                MessageBox.Show("��ѡ����Ҫȡ��������");
                return;
            }
            if (tn.Tag == null)
            {
                MessageBox.Show("��ȡ������Ϣʧ��");
                return;
            }
            operApply = tn.Tag as OperApply;
            if (operApplyManage.UpdateColFlag(operApply.OperApplyId.ToString(), "3") == -1)
            {
                MessageBox.Show("����ʧ��");
                return;
            }
            MessageBox.Show("�����ɹ�");
        }

        #endregion

        #region ������Ϣ

        /// <summary>
        /// ����ƻ�ȡ�ı걾ʵ��
        /// </summary>
        /// <returns></returns>       
        private int SaveSpecSourcePlan(System.Data.IDbTransaction trans)
        {
            int result = -1;

            foreach (Control flp in tpColInfo.Controls)
            {
                if (flp.Tag != null)
                {
                    ucSubSpecForOrg c = flp.Controls[0] as ucSubSpecForOrg;
                    c.SpecId = Convert.ToInt32(txtSpecCode.Text.Trim());
                    c.TumorPor = tumorPro;
                    c.DisTypeId = specSource.DiseaseType.DisTypeID;
                    ///�걾�Ĳ��
                    if (rbtLeft.Checked) c.SideFrom = "L";
                    if (rbtRight.Checked) c.SideFrom = "R";
                    if (rbtAll.Checked) c.SideFrom = "A";
                    if (rbtUp.Checked) c.SideFrom = "U";
                    if (rbtBottom.Checked) c.SideFrom = "B";
                    if (rbtMid.Checked) c.SideFrom = "M";
                    c.TumorPos = this.txtTumorPos.Text;
                    c.TransPos = this.txtTransfer.Text;
                    c.PosDet = txtPosDet.Text;

                    result = c.SaveSourcePlan(trans);

                    //���PLANID
                    if (result == -2 && oper == "")
                    {
                        SetColSpecDet("P");
                    }
                    if (result <= -1)
                    {
                        MessageBox.Show("�����װ�걾ʧ��!", title);
                        return result;
                    }
                }
            }
            if (oper != "")
            {
                return 1;
            }

            return result;
        }

        /// <summary>
        /// ����ԭ�걾
        /// </summary>
        private int SaveSpecSource()
        {
            SpecSourceSet();

            for (int i = 0; i <= tpColInfo.TabPages[2].Controls.Count; i++)
            {
                try
                {
                    string seq = (tpColInfo.TabPages[2].Controls[i] as ucSubSpecForOrg).Seq;
                    if (seq != null && seq != string.Empty)
                    {
                        specSource.SpecNo = seq;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                catch
                {
                    continue;
                }
            }

            if (specSource.SpecNo.Trim() == string.Empty)
            {
                MessageBox.Show("��ȡ�걾��ʧ�ܣ�");
                return -1;
            }
            int sourceResult = -1;
            if (oper == "")
            {
                sourceResult = specSourceMange.InsertSpecSource(specSource);
            }
            if (oper == "M")
            {
                sourceResult = specSourceMange.UpdateSpecSource(specSource);
            }
            if (sourceResult <= 0)
            {
                MessageBox.Show("����ԭ�걾ʧ��!", title);
                return -1;
            }
            return sourceResult;
        }

        /// <summary>
        /// ���没����Ϣ�Ͳ�����Ϣ
        /// </summary>
        private int SavePatientInfo()
        {
            //��ucPatientInfo�û��ؼ���ȡ������Ϣ
            specPatient = ucPatient.SetPatient();
            if (specPatient.PatientName.Trim() == "")
            {
                MessageBox.Show("����д������Ϣ��");
                return -1;
            }
            int patientResult = -1;
            if (!ucPatient.IsExisted)
            {
                specPatientManage.GetNextSequence(ref patientId);
                specPatient.PatientID = Convert.ToInt32(patientId);
                patientResult = specPatientManage.InsertPatient(specPatient);
                if (patientResult == 1)
                    ucPatient.IsExisted = true;
                return patientResult;
            }
            //������±걾��Ϣ
            //if(oper!="" && ucPatient.IsExisted)
            //{
            //    patientResult = specPatientManage.UpdatePatient(specPatient);
            //    return patientResult;
            //}

            //����걾�����Ѿ����ڸò��˵���Ϣ�������ҳ�����õ���Ϣ ����
            if (ucPatient.IsExisted)
            {
                if (specPatient.PatientID <= 0 && patientId != "")
                {
                    specPatient.PatientID = Convert.ToInt32(patientId);
                }
                patientResult = specPatientManage.UpdatePatient(specPatient);
                return patientResult;
            }
            if (patientResult == -1)
            {
                MessageBox.Show("������Ϣ����ʧ�ܣ�", title);
            }
            return patientResult;
        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <returns></returns>
        private int SaveDiagnoseInfo()
        {
            //�Ƿ�¼�������
            if (ucDiaInFlp.IsInputDiagnose())
            {
                diagInfo = new SpecDiagnose();
                diagInfo = ucDiaInFlp.SpecDiag;
                //���ò��������Ϣ                  
                int result = -1;
                string sql = "select * from spec_diagnose where specid=" + txtSpecCode.Text.Trim();
                ArrayList arr = diagManage.GetBaseInfo(sql);
                diagInfo.SpecSource.SpecId = Convert.ToInt32(txtSpecCode.Text.Trim());
                if (oper == "" || arr.Count <= 0)
                {
                    string sequence = "";
                    diagManage.GetNextSequence(ref sequence);
                    diagInfo.BaseID = Convert.ToInt32(sequence);
                    result = diagManage.InsertDiagnose(diagInfo);
                }
                else
                {
                    result = diagManage.UpdateDiagnose(diagInfo);
                }
                int updateResult = specSourceMange.UpdateInBase(txtSpecCode.Text.Trim());
                if (result <= -1)
                {
                    MessageBox.Show("�����Ϣ����ʧ�ܣ�", title);
                }
                return result;

                // Ҫ���� diagInfo.Main_DiagState;
            }
            return 0;
        }

        /// <summary>
        /// �����ռ���־
        /// </summary>
        /// <returns></returns>
        private int UpdateColFlag()
        {
            return operApplyManage.UpdateColFlag(specSource.OperApplyId.ToString(), "1");
        }

        /// <summary>
        /// ���·�װ�걾���
        /// </summary>
        /// <returns></returns>
        private int UpdateDisSeq()
        {
            string tmpDis = cmbDisType.Text.Trim();
            string tmpSeq = barCodeManage.GetMaxSeqByDisAndType(tmpDis, "2");
            if (tmpSeq == null)
                return 0;
            return barCodeManage.UpdateMaxSeqByDisAndType(tmpDis, "2", tmpSeq);

        }
        #endregion

        #region �޸���֯�걾��Ϣ

        /// <summary>
        /// �����������뵥Id��ȡ�걾��Ϣ
        /// </summary>
        /// <param name="operApplyId"></param>
        private void GetSpecSourceInfo(string operApplyId)
        {
            //OPERAPPLYID
            ArrayList arrSpecSource = new ArrayList();
            if (operApplyId != "")
            {
                arrSpecSource = specSourceMange.GetSpecSource(" select * from spec_source where HISBARCODE = '" + operApplyId.PadLeft(12, '0') + "'");
            }
            if (arrSpecSource == null || arrSpecSource.Count <= 0)
            {
                return;
            }
            specSource = arrSpecSource[0] as SpecSource;
            GetSpecInfo();
        }

        /// <summary>
        /// ��ȡ�걾Դ��Ϣ
        /// </summary>
        private void GetSpecSourceInfo(string specSourceID, string specBarCode)
        {
            ArrayList arrSpecSource = new ArrayList();
            if (specSourceID != "")
            {
                arrSpecSource = specSourceMange.GetSpecSource(" select * from spec_source where specid = " + txtSpecCode.Text.Trim());
            }
            if (specBarCode != "")
            {
                arrSpecSource = specSourceMange.GetSpecSource(" select * from spec_source where HISBARCODE = '" + txtBarCode.Text.Trim().PadLeft(12, '0') + "'");
            }
            foreach (SpecSource spec in arrSpecSource)
            {
                specSource = new SpecSource();
                specSource = spec;
                break;
            }
            GetSpecInfo();
        }

        /// <summary>
        /// ����PatientId��ȡ������Ϣ
        /// </summary>
        /// <param name="patientId"></param>
        private void GetPatientInfo(string patientId)
        {
            try
            {
                ArrayList arrPatient = specPatientManage.GetPatientInfo("select * from spec_patient where patientid =" + patientId);
                if (arrPatient != null && arrPatient.Count > 0)
                {
                    foreach (SpecPatient s in arrPatient)
                    {
                        specPatient = new SpecPatient();
                        specPatient = s;
                        specPatient.InHosNum = specSource.InPatientNo;
                        this.patientId = specPatient.PatientID.ToString();
                        ucPatient.IsExisted = true;
                        break;
                    }
                }
                ucPatient.SpecPatient = specPatient;
                ucPatient.GetPatient();
            }
            catch
            {

            }

        }

        /// <summary>
        /// ��ȡ��װ�걾�洢��Ϣ
        /// </summary>
        /// <param name="specId"></param>
        private void GetSavePlan(string specId)
        {
            string sql = "select distinct * from spec_source_store where specid=" + specId;
            ArrayList arrSpecSource = specSourcePlanManage.GetSourcePlan(sql);
            Dictionary<SpecSourcePlan, List<StoreInfo>> dicParsed = new Dictionary<SpecSourcePlan, List<StoreInfo>>();

            //����һ���걾Դ�зֽ��˼�����������
            if (arrSpecSource != null && arrSpecSource.Count > 0)
            {
                dicParsed = specSourcePlanManage.ParseSourcePlan(arrSpecSource);
            }
            if (arrSpecSource == null || arrSpecSource.Count == 0)
            {
                foreach (Control c in tpColInfo.Controls)
                {
                    if (c.GetType().ToString().Contains("TabPage") && c.Tag != null)
                    {
                        TabPage tab = c as TabPage;
                        tab.Dispose();
                    }
                }
                return;
            }
            try
            {
                if (dicParsed != null && dicParsed.Count > 0)
                {
                    foreach (Control c in tpColInfo.Controls)
                    {
                        if (c.GetType().ToString().Contains("TabPage") && c.Tag != null)
                        {
                            TabPage tab = c as TabPage;
                            tab.Dispose();
                        }
                    }
                    int index = 0;
                    ucSubSpecForOrg[] ucSpecForOrgList = new ucSubSpecForOrg[dicParsed.Count];
                    foreach (KeyValuePair<SpecSourcePlan, List<StoreInfo>> tmpPair in dicParsed)
                    {
                        ucSpecForOrgList[index] = new ucSubSpecForOrg();
                        ucSpecForOrgList[index].TumorType = Convert.ToInt32(tmpPair.Key.TumorType);
                        ucSpecForOrgList[index].GetPlanSource(tmpPair.Key);
                        ucSpecForOrgList[index].Flag = "1";
                        ucSpecForOrgList[index].SpecId = Convert.ToInt32(specId);
                        string typeName = ((Constant.TumorType)Convert.ToInt32(tmpPair.Key.TumorType)).ToString();
                        ArrayList arrTmpPlan = new ArrayList();
                        foreach (StoreInfo info in tmpPair.Value)
                        {
                            //��ȡStoreID�е����б걾       
                            tmpPair.Key.SubSpecCodeList.Clear();
                            ArrayList arrSubSpec = subSpecManage.GetSubSpecByStoreId(info.StoreId.ToString());
                            SpecSourcePlan sp = new SpecSourcePlan();
                            sp = specSourcePlanManage.GetPlanById(info.StoreId.ToString(), "");

                            if (sp.SideFrom == "L")
                                rbtLeft.Checked = true;
                            if (sp.SideFrom == "R")
                                rbtRight.Checked = true;
                            if (sp.SideFrom == "A")
                                rbtAll.Checked = true;
                            if (sp.SideFrom == "B")
                                rbtBottom.Checked = true;
                            if (sp.SideFrom == "U")
                                rbtUp.Checked = true;
                            if (sp.SideFrom == "M")
                                rbtMid.Checked = true;
                            txtTransfer.Text = sp.TransPos;
                            txtTumorPos.Text = sp.TumorPos;
                            txtTumorPos.Tag = subSpecManage.ExecSqlReturnOne("select code from com_dictionary where  type ='SpecPos' and name ='" + sp.TumorPos + "'");
                            txtPosDet.Text = sp.BaoMoEntire;
                            foreach (SubSpec s in arrSubSpec)
                            {
                                sp.SubSpecCodeList.Add(s.SubBarCode);
                            }
                            arrTmpPlan.Add(sp);
                        }
                        ucSpecForOrgList[index].InitSpecPlan(arrTmpPlan);
                        TabPage tab = new TabPage();
                        tab.Name = "tab" + index.ToString();
                        tab.Text = typeName;
                        tab.Tag = ucSpecForOrgList[index];
                        tab.BackColor = Color.White;
                        tab.Controls.Add(ucSpecForOrgList[index]);
                        tpColInfo.TabPages.Add(tab);
                        index++;
                    }
                }
            }
            catch
            {

            }
        }

        //��ȡ���������Ϣ
        private void GetDiagnoseInfo(string specId)
        {
            string sql = "select * from spec_diagnose where specid=" + specId;
            ArrayList arr = new ArrayList();
            arr = diagManage.GetBaseInfo(sql);
            diagInfo = new SpecDiagnose();
            if (arr != null && arr.Count > 0)
            {
                foreach (SpecDiagnose s in arr)
                {
                    diagInfo = s;
                    break;
                }
            }
            //specSource.InDeptNo = diagInfo.inDeptNo;
            //specSource.MedGrp = diagInfo.medGrp;
            ucDiaInFlp.GetDiagnoseInfo(diagInfo);

        }

        private void PrintLocation()
        {
            ArrayList arrSubSpec = new ArrayList();
            arrSubSpec = subSpecManage.GetSubSpecBySpecId(txtSpecCode.Text.Trim());
            if (arrSubSpec == null)
            {
                return;
            }
            List<SubSpec> subSpecList = new List<SubSpec>();
            foreach (SubSpec s in arrSubSpec)
                subSpecList.Add(s);
            if (subSpecList.Count > 0)
            {
                SpecOutOper oper = new SpecOutOper();
                oper.PrintOutSpec(subSpecList, null);
            }
        }

        private void PrintOrgLabel()
        {
            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("�Ƿ��ӡȫ����ǩ�������飩��", "��ǩ��ӡ", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                return;
            }
            ArrayList arrSubSpec = new ArrayList();
            arrSubSpec = subSpecManage.GetSubSpecBySpecId(txtSpecCode.Text.Trim());
            if (arrSubSpec == null)
            {
                return;
            }
            List<string> subSpecList = new List<string>();
            foreach (SubSpec s in arrSubSpec)
            {
                if (s.SpecTypeId == 7)
                    continue;
                subSpecList.Add(s.SubBarCode);
            }
            if (subSpecList.Count > 0)
            {
                PrintLabel.bradyIP = this.bradyIp;
                PrintLabel.PrintBarCode(subSpecList);
            }
        }

        private string GetHisBarCode(string curBarCode, int i)
        {
            ArrayList arr = new ArrayList();
            string tmp = Convert.ToChar(('A' + i)).ToString() + curBarCode;
            arr = specSourceMange.GetSpecSource(" select * from spec_source where HISBARCODE = '" + tmp + "'");
            if (arr != null && arr.Count > 0)
            {
                i++;
                return GetHisBarCode(curBarCode, i);
            }
            else
            {
                return tmp;
            }

        }

        /// <summary>
        /// һ��������Ӷ������
        /// </summary>
        private void AddAnotherSpec()
        {

            TreeNode tn = tvApply.SelectedNode;
            operApply = tn.Tag as OperApply;
            if (tn.Parent.Tag.ToString() == "1")
            {
                GetSpecInfoByOperApply();
                specSource.HisBarCode = GetHisBarCode(operApply.OperApplyId.ToString().PadLeft(11, '0'), 0);

                txtFlowCode.Text = specSource.HisBarCode;
                string curSql = " select substr(ss.SUBBARCODE,1,6) from SPEC_SOURCE s, spec_subspec ss where  s.specid = ss.specid and s.HISBARCODE = '" + operApply.OperApplyId.ToString().PadLeft(12, '0') + "'";

                curSql += " union select substr(ss.SUBBARCODE,1,6) from SPEC_SOURCE s, spec_subspec ss where s.ORGORBLOOD = 'O' and s.specid = ss.specid and s.OPERAPPLYID = '" + operApply.OperId + "'";
                string subSeq = specSourceMange.ExecSqlReturnOne(curSql);
                if (subSeq.Length > 3)
                {
                    detSeq = subSeq;
                }
                for (int i = tpColInfo.TabPages.Count - 1; i >= 0; i--)
                {
                    if (tpColInfo.TabPages[i].Tag != null)
                    {
                        tpColInfo.TabPages[i].Dispose();
                    }
                }
                txtTumorPos.Text = "";
                txtTransfer.Text = "";
            }
        }

        #endregion

        #region �¼�
        public override int Save(object sender, object neuObject)
        {
            int result = this.ValidateData();
            if (result == 0)
                return 0;
            try
            {
                if (specSourceMange.ChkSpecExsit(specID.ToString()) && oper == "")
                {
                    MessageBox.Show("ԭ�걾ID���ڣ����������ã�", title);
                    return -1;
                }
                //SpecSourceSet();

            }
            catch
            {

            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            operApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            diagManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specSourceMange.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specPatientManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            barCodeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            
            try
            {
                if (SavePatientInfo() <= -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���没����Ϣʧ�ܣ�", title);
                    return -1;
                }
                if (SaveSpecSource() <= -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����Դ�걾ʧ�ܣ�", title);
                    return -1;
                }
                if (SaveSpecSourcePlan(FS.FrameWork.Management.PublicTrans.Trans) <= -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����װ�걾ʧ�ܣ�", title);
                    return -1;
                }
                if (SaveDiagnoseInfo() <= -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���������Ϣʧ�ܣ�", title);
                    return -1;
                }
                if (specSource.OperApplyId != "" && oper == "")
                {
                    if (UpdateColFlag() <= -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����ռ���־ʧ�ܣ�", title);
                        return -1;
                    }
                }
                if (oper == "" && UpdateDisSeq() <= -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�������ʧ�ܣ�", title);
                    return -1;
                }

                if (operApply!= null)
                {
                    if (operApplyManage.UpdateColFlag(operApply.OperApplyId.ToString(), "1") == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���²ɼ����ʧ�ܣ�", title);
                        return -1;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.detSeq = "";
                times = 0;
                MessageBox.Show("����ɹ���", title);

            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ�ܣ�", title);
                return -1;
            }
            try
            {
                //��ǰ�걾������
                #region ���±걾��

                if (oper == "")
                {
                    PrintOrgLabel();
                }
                oper = "M";
                txtSpecCode.Enabled = false;               
                foreach (Control c in tpColInfo.Controls)
                {
                    if (c.Tag != null)
                    {
                        ucSubSpecForOrg org = c.Controls[0] as ucSubSpecForOrg;
                        if (org.UseFullBoxList != null)
                        {
                            foreach (SpecBox box in org.UseFullBoxList)
                            {
                                MessageBox.Show(box.BoxBarCode + " �걾�м���������������µı걾�У�", title);
                                //��ʾ�û�����µı걾��
                                FS.HISFC.Components.Speciment.Setting.frmSpecBox newSpecBox = new FS.HISFC.Components.Speciment.Setting.frmSpecBox();
                                if (box.DesCapType == 'B')
                                    continue;
                                else
                                    newSpecBox.CurShelfId = box.DesCapID;
                                newSpecBox.DisTypeId = box.DiseaseType.DisTypeID;
                                newSpecBox.OrgOrBlood = box.OrgOrBlood;
                                newSpecBox.SpecTypeId = box.SpecTypeID;
                                newSpecBox.Show();
                            }
                            org.UseFullBoxList = null;
                        }
                    }
                }
                #endregion
            }
            catch
            { }
            Query(null, null);

            return base.Save(sender, neuObject);
        }

        private void chkNone_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNone.Checked)
            {
                chkMed.Checked = false;
                chkRad.Checked = false;
                chkOther.Checked = false;
            }
        }

        private void chkRad_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRad.Checked)
                chkNone.Checked = false;
        }

        private void chkMed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMed.Checked)
                chkNone.Checked = false;
        }

        private void btnAddSpec_Click(object sender, EventArgs e)
        {
            if (cmbDisType.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ����!");
                tpColInfo.SelectedIndex = 0;
                return;
            }
            if (txtTumorPos.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ������!");
                tpColInfo.SelectedIndex = 0;
                return;
            }
            if (oper == "")
            {
                if (times == 0)
                {
                    string sequence = "";
                    specSourceMange.GetNextSequence(ref sequence);
                    txtSpecCode.Text = sequence;
                    specID = Convert.ToInt32(sequence);
                }
            }
            int curTumorType = 0;
            if (cmbTumorType.SelectedValue != null)
                curTumorType = Convert.ToInt32(cmbTumorType.SelectedValue.ToString());
            ucSpecOrg = new ucSubSpecForOrg();
            ucSpecOrg.TumorType = curTumorType;
            //ucSpecOrg.Dock = DockStyle.Fill;
            ControlCollection cts = ucSpecOrg.Controls;
            foreach (Control c in cts)
            {
                if (c.Name == "grp")
                {
                    GroupBox grp = c as GroupBox;
                    grp.Text = cmbTumorType.Text;
                    specIndex++;
                }
                if (oper == "" && c.Name == "btnUpdate")
                {
                    c.Enabled = false;
                }
            }
            if (oper == "M" && txtSpecCode.Text.Trim() != "")
            {
                ucSpecOrg.SpecId = Convert.ToInt32(txtSpecCode.Text.Trim());
            }
            TabPage tabType = new TabPage();
            tabType.Tag = ucSpecOrg;
            tabType.BackColor = Color.White;
            tabType.Name = "newOne";
            tabType.Text = cmbTumorType.Text;
            tabType.Controls.Add(ucSpecOrg);
            tpColInfo.TabPages.Add(tabType);
            tpColInfo.SelectedIndex = tpColInfo.TabCount - 1;
            try
            {
                SetColSpecDet("D");
                times += 1;
            }
            catch
            { }
        }

        private void ucSpecSourceForOrg_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //������Ժ���
            ArrayList arrDiagnoseList = con.GetList("DiagnosebyNurse");
            if (arrDiagnoseList != null && arrDiagnoseList.Count > 0)
            {
                cmbDiagnose.AddItems(arrDiagnoseList);
            }


            ArrayList posList = new ArrayList();
            posList = con.GetList("SpecPos");
            this.txtTumorPos.AddItems(posList);

            DisTypeBinding();
            GetLogEmpInfo();
            GetAllEmp();
            TumorTypeBinding();
            FlpBinding();
            if (SpecId > 0)
            {
                try
                {
                    txtSpecCode.Text = specID.ToString();
                    GetSpecSourceInfo("SpecId", "");
                    btnSave.Visible = true;
                    btnPrint.Visible = true;
                    oper = "M";
                }
                catch
                {

                }
            }
            LoadOperApply();
            SetColSpecDet("D");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("�޸�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            this.toolBarService.AddToolButton("��ӡλ����Ϣ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("ȡ������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("ͬ���������", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            this.toolBarService.AddToolButton("���", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string sequence = "";
            switch (e.ClickedItem.Text.Trim())
            {
                case "�޸�":
                    txtSpecCode.Enabled = true;
                    oper = "M";
                    break;
                case "���":
                    ResetForm();
                    specSource = new SpecSource();
                    specPatient = new SpecPatient();
                    operApply = new OperApply();
                    specSourceMange.GetNextSequence(ref sequence);
                    specID = Convert.ToInt32(sequence);
                    txtSpecCode.Text = sequence;
                    break;
                case "��ӡλ����Ϣ":
                    PrintLocation();
                    break;
                case "ȡ������":
                    CancelSpec();
                    Query(null, null);
                    break;
                case "ͬ���������":
                    oper = "";
                    ResetForm();
                    //�����ѡ�����Ѿ���װ�˵���������������2��������Ҫ��װ
                    specSource = new SpecSource();
                    specPatient = new SpecPatient();
                    ucPatient.SpecPatient = specPatient;
                    ucPatient.IsExisted = false;
                    specSourceMange.GetNextSequence(ref sequence);
                    specID = Convert.ToInt32(sequence);
                    txtSpecCode.Text = sequence;
                    try
                    {
                        AddAnotherSpec();
                        return;
                    }
                    catch
                    { }
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void txtSpecCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                try
                {
                    specID = Convert.ToInt32(txtSpecCode.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("�걾�ű���Ϊ���֣�", title);
                    return;
                }

                try
                {
                    ResetForm();
                    GetSpecSourceInfo("specId", "");
                }
                catch
                {

                }
            }
        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save(sender, null);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintLocation();
        }

        public override int Query(object sender, object neuObject)
        {
            tvApply.Nodes[0].Nodes.Clear();
            tvApply.Nodes[1].Nodes.Clear();
            tvApply.Nodes[2].Nodes.Clear();
            LoadOperApply();
            return base.Query(sender, neuObject);
        }

        private void tvApply_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ResetForm();
            //���ѡ���Ǹ����ڵ㷵��
            TreeNode tn = tvApply.SelectedNode;
            if (tn == null)
                return;
            if (tn.Tag == null || tn.Tag.ToString() == "0" || tn.Tag.ToString() == "1")
            {
                return;
            }
            operApply = tn.Tag as OperApply;
            if (operApply == null)
            {
                return;
            }
            if (tn.Parent.Tag.ToString() == "0")
            {
                oper = "";
                GetSpecInfoByOperApply();
            }
            if (tn.Parent.Tag.ToString() == "1")
            {
                oper = "M";
                GetSpecSourceInfo(operApply.OperApplyId.ToString());
            }
        }

        private void cmsAdd_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "��������")
            {
                TreeNode tn = tvApply.SelectedNode;
                if (tn == null)
                {
                    return;
                }
                if (tn.Tag == null)
                {
                    return;
                }
                operApply = tn.Tag as OperApply;
                if (operApply == null)
                {
                    return;
                }
                if (tn.Parent.Tag.ToString() == "2")
                {
                    return;
                }
                else
                {

                    DialogResult result = MessageBox.Show("ȷ����������?", "��������", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    OperApply oa = new OperApply();
                    oa = operApply.Clone();
                    oa.HadCollect = "0";
                    oa.OperApplyId = operApplyManage.GetSequence();
                    if (operApplyManage.InsertOperApply(oa) > 0)
                    {
                        MessageBox.Show("�����ɹ�");
                    }
                    else
                    {
                        MessageBox.Show("����ʧ��");
                        return;
                    }
                    this.Query(null, null);
                    //operApplyManage

                }
            }
        }

        private void cmbDisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetColSpecDet("D");
        }

        private void tpColInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tpColInfo.SelectedIndex != 0)
                {
                    if (cmbDisType.Text.Trim() == "")
                    {
                        MessageBox.Show("��ѡ����!");
                        tpColInfo.SelectedIndex = 0;
                        return;
                    }
                    if (txtTumorPos.Text.Trim() == "")
                    {
                        MessageBox.Show("��ѡ������!");
                        tpColInfo.SelectedIndex = 0;
                        return;
                    }
                }
                if (tpColInfo.SelectedIndex == 0)
                    return;
                SetColSpecDet("D");
            }
            catch
            { }
        }
        #endregion

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    ResetForm();
                    operApply = operApplyManage.GetById(Convert.ToInt32(txtBarCode.Text.Trim().ToString()).ToString(), "O");
                    if (operApply == null)
                    {
                        return;
                    }
                    if (operApply.HadCollect == "1")
                    {
                        oper = "M";
                        GetSpecSourceInfo(operApply.OperApplyId.ToString());
                    }
                    else
                    {
                        oper = "";
                        GetSpecInfoByOperApply();

                    }
                    txtBarCode.Text = "";
                }
                catch
                {
                    GetSpecSourceInfo(txtBarCode.Text);
                }
            }
        }

        private void txtTumorPos_TextChanged(object sender, EventArgs e)
        {
            txtPosDet.Text = txtTumorPos.Text;
        }

        private void txtSpecNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbDisType.Tag == null)
                {
                    MessageBox.Show("��ѡ����");
                    return;
                }
                else
                {
                    string tmpId = specSourceMange.ExecSqlReturnOne("select SPECID from SPEC_SOURCE where ORGORBLOOD = 'O' and SPEC_NO = '" + txtSpecNo.Text.PadLeft(6, '0') + "' and DISEASETYPEID = " + cmbDisType.Tag.ToString());
                    if (tmpId == "-1")
                        return;
                    txtSpecCode.Text = tmpId;
                    specID = Convert.ToInt32(tmpId);
                    this.txtSpecCode_KeyDown(sender, e);
                }
            }
        }

    }
}
