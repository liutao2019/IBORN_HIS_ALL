using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic;
using System.Collections;
using FS.FrameWork.Management;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.InpatientFee.Diagnosis
{
    /// <summary>
    /// ������<br>ucDiagnosis</br>
    /// <Font color='#FF1111'>[��������: ��Ժ���¼��]</Font><br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-08-14]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    public partial class ucDiagnosis : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucDiagnosis()
        {
            InitializeComponent();
            this.Init();
        }
        #region ����

        #region ˽��

        /// <summary>
        /// ���תintegrate��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        private int happenNo;//ѡ�м�¼��happenNo

        private String iType = null;//ҳ��ѡ�е�ICD������

        private String icdSpell = "";//ICDƴ����

        private String operType = "";//��� 1 ҽ��վ¼�����  2 ������¼�����

        private String selectedIType = "";//ѡ�м�¼��ԭICD���

        private bool isDiagSelect = false;

        //private String inpatientNO = "";// סԺ��ˮ��

        //private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes operType = null;//ҳ��ѡ�еļ�¼��OperType(���޲�����ϼ�¼)
        
        #endregion

        #region ����

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ��������ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase diagnoseMgr = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
        protected FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBaseMC diagnoseMgrMc = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBaseMC();
        protected FS.HISFC.Models.HealthRecord.Diagnose myDiagnose = new FS.HISFC.Models.HealthRecord.Diagnose();

        protected bool isUpdate = false;

        protected bool isOutMain = false;
        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        #endregion

        #region ����
        #endregion

        #endregion

        #region ����

        /// <summary>
        /// ��ǰ�����Ƿ����ģʽ
        /// </summary>
        protected new bool DesignMode
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");
            }
        }

        #endregion

        #region ����

        #region ��Դ�ͷ�
        #endregion

        #region ��¡
        #endregion

        #region ˽��

        /// <summary>
        /// ҳ�������뻼�߻�����Ϣ
        /// </summary>
        private int FillPatientInfo()
        {
            if (this.ucQueryInpatientNo.InpatientNo == null || this.ucQueryInpatientNo.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("סԺ�Ŵ���û���ҵ��û���", 111);
                this.ucQueryInpatientNo.Focus();
                return -1;
            }
            try
            {
                this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo.InpatientNo);
                //�жϻ���״̬

                if (this.ValidPatient(this.patientInfo) == -1)
                {
                    this.ucQueryInpatientNo.Focus();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg(ex.Message, 211);
                return -1;
            }
            this.Clear();
            this.txtName.Text = this.patientInfo.Name;
            this.txtSex.Text = this.patientInfo.Sex.ToString();
            this.txtInDate.Text = this.patientInfo.PVisit.InTime.ToShortDateString();
            this.txtDept.Text = this.patientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtType.Text = this.patientInfo.Pact.Name;
            return 1;
        }

        /// <summary>
        /// �����ʾ������
        /// </summary>
        private void Clear()
        {
            this.txtName.Text = "";
            this.txtSex.Text = "";
            this.txtInDate.Text = "";
            this.txtDept.Text = "";
            this.txtType.Text = "";
            this.diagnosisType.Text = "";
            this.diagnosisDr.Text = "";
            this.diagnosisDate.Text = "";
            this.diagnosisCode.Text = "";
            this.diagnosis.Text = "";
            this.isMain.Checked = false;
            this.happenNo = 0;
            this.iType = null;
            //this.operType = null;
            this.combICDType.Text = "ICD10";
            this.ucDiagnose1.InitICDMedicare("0");
            this.ucDiagnose1.Visible = false;
            this.isUpdate = false;
            this.icdSpell = "";
            this.operType = "";
            this.isMain.Checked = false;
            this.ucDiagnose1.Visible = false;
            this.isDiagSelect = false;
            //this.inpatientNO = "";
            this.isOutMain = false;

        }
        /// <summary>
        /// ��������
        /// </summary>
        private void ClearAfterSave()
        {
            this.diagnosisType.Text = "";
            this.diagnosisDr.Text = "";
            this.diagnosisDate.Text = "";
            this.diagnosisCode.Text = "";
            this.diagnosis.Text = "";
            this.isMain.Checked = false;
            this.isUpdate = false;
            this.icdSpell = "";
            this.iType = null;
            this.operType = "";
            this.isMain.Checked = false;
            this.combICDType.Text = "ICD10";
            this.ucDiagnose1.Visible = false;
            this.isDiagSelect = false;
            //this.inpatientNO = "";
            this.isOutMain = false;
        }
        /// <summary>
        /// ��ʼ������б�
        /// </summary>
        private void InitDiagnoseList()
        {
            this.listView1.Clear();
            this.listView1.Columns.Add("", 0);
            this.listView1.Columns.Add("�������", 90, HorizontalAlignment.Center);
            this.listView1.Columns.Add("�������", 200, HorizontalAlignment.Center);
            this.listView1.Columns.Add("���ҽʦ", 90, HorizontalAlignment.Center);
            this.listView1.Columns.Add("���ʱ��", 90, HorizontalAlignment.Center);
            this.listView1.Columns.Add("״̬", 0, HorizontalAlignment.Center);
            this.listView1.Columns.Add("", 0);//happenNo
            this.listView1.Columns.Add("", 0);//OperType
            this.listView1.Columns.Add("", 0);//ICD_code
            this.listView1.Columns.Add("ICD���", 70, HorizontalAlignment.Center);//ICD��� 'ICD10';'ҽ��'
            this.listView1.Columns.Add("", 0);//MAIN_FLAG �Ƿ������1��0��
            
        }
        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        private void InitCbox()
        {
            //������
            InitDiganosisType();
            //ҽ��
            InitDr();
            //ICD���
            InitICDType();
        }
        /// <summary>
        /// ��ʼ��ҽ���б�
        /// </summary>
        private void InitDr()
        {
            ArrayList drList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (drList == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("��ʼ��ҽ���б����!") + this.managerIntegrate.Err);
                return;
            }
            this.diagnosisDr.AddItems(drList);
        }

        /// <summary>
        /// ��ʼ��ICD��������б�
        /// </summary>
        private void InitICDType()
        {
            ArrayList ls = new ArrayList();
            FS.HISFC.Models.Base.Item icdItem = null;
            #region д�������б���
       //     icdItem = new Item();
       //     icdItem.ID = "0";
       //     icdItem.Name = "ȫ��";
       //     ls.Add(icdItem);
            icdItem = new Item();
            icdItem.ID = "1";
            icdItem.Name = "ICD10";
            ls.Add(icdItem);
            icdItem = new Item();
            icdItem.ID = "2";
            icdItem.Name = "ҽ��(��)";
            ls.Add(icdItem);
            icdItem = new Item();
            icdItem.ID = "3";
            icdItem.Name = "ҽ��(ʡ)";
            ls.Add(icdItem);
            #endregion
            this.combICDType.AddItems(ls);
            this.combICDType.Text = "ICD10";
        }
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        private void InitDiganosisType()
        {
            ArrayList DTList = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
            if (DTList == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(Language.Msg("��ʼ���������б����!") + this.managerIntegrate.Err);
                return;
            }
            this.diagnosisType.AddItems(DTList);
        }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        private int SetDiagnoseInfo()
        {
            this.myDiagnose.DiagInfo.HappenNo = this.happenNo;// HAPPENNO
            this.myDiagnose.DiagInfo.Patient.ID = this.ucQueryInpatientNo.InpatientNo;// סԺ��ˮ��
            this.myDiagnose.DiagInfo.DiagType.ID = this.diagnosisType.SelectedItem.ID;// �������
            //this.myDiagnose.DiagInfo.DiagType.Name = this.diagnosisType.SelectedItem.Name;// �������
            this.myDiagnose.DiagInfo.ICD10.ID = this.diagnosisCode.Text;// ���ICD��
            this.myDiagnose.DiagInfo.ICD10.Name = this.diagnosis.Text;// �������
            this.myDiagnose.DiagInfo.ICD10.SpellCode = this.icdSpell;//ICDƴ����
            this.myDiagnose.DiagInfo.DiagDate = this.diagnosisDate.Value;// �������
            this.myDiagnose.DiagInfo.Doctor.ID = this.diagnosisDr.SelectedItem.ID;// ҽʦ����
            this.myDiagnose.DiagInfo.Doctor.Name = this.diagnosisDr.SelectedItem.Name;// ҽʦ����(���)
            this.myDiagnose.Pvisit = this.patientInfo.PVisit;// ���߷�����
            //this.myDiagnose.DiagInfo.Patient = this.patientInfo.PVisit;//������Ϣ��
            this.myDiagnose.DiagInfo.IsMain = this.isMain.Checked;// �Ƿ������ 1 ����� 0 �������
            this.myDiagnose.OperType = "1";// ��� 1 ҽ��վ¼�����  2 ������¼�����

            return 1;
        }

        public virtual int valid()
        {
            if (this.patientInfo.PVisit.InTime > this.diagnosisDate.Value)
            {
                MessageBox.Show("������ڲ�������סԺ����");
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        private int Save()
        {
            if (this.valid() < 0)
            {
                return -1;
            }

            if (this.SetDiagnoseInfo() == -1)
            {
                return -1;
            }

            if (this.isUpdate)
            {
                if (!isOutMain)
                {
                    //�����ظ����������
                    if (this.myDiagnose.DiagInfo.IsMain && this.myDiagnose.DiagInfo.DiagType.ID == "1")
                    {
                        //��ȡ��Ժ�����
                        ArrayList outDiagnoses = this.diagnoseMgrMc.GetOutMainDiagnose(this.myDiagnose.DiagInfo.Patient.ID);
                        //�ж��Ƿ��������
                        if (outDiagnoses.Count > 0)
                        {
                            MessageBox.Show(Language.Msg("�Ѿ����ڳ�Ժ�����!"));
                            return -1;
                        }
                    }
                }
                //���������Ϣ
                if (this.iType == "1")//ICD10
                {
                    if (this.selectedIType == "ҽ��(��)" || this.selectedIType == "ҽ��(ʡ)")
                    {
                        MessageBox.Show(Language.Msg("���ܽ�ҽ������޸�ΪICD10���!"));
                        return -1;
                    }
                    if (this.diagnoseMgr.UpdateDiagnose(this.myDiagnose) == -1)
                    {
                        return -1;
                    }
                }
                else//ICDMEDICARE
                {
                    if (this.selectedIType == "ICD10")
                    {
                        MessageBox.Show(Language.Msg("���ܽ�ICD10����޸�Ϊҽ�����!"));
                        return -1;
                    }
                    if (this.diagnoseMgrMc.UpdateDiagnoseMedicare(this.myDiagnose) == -1)
                    {
                        return -1;
                    }
                }
            }
            else
            {
                //�����ظ����������
                if (this.myDiagnose.DiagInfo.IsMain && this.myDiagnose.DiagInfo.DiagType.ID == "1")
                {
                    //��ȡ��Ժ�����
                    ArrayList outDiagnoses = this.diagnoseMgrMc.GetOutMainDiagnose(this.myDiagnose.DiagInfo.Patient.ID);
                    //�ж��Ƿ��������
                    if (outDiagnoses.Count > 0)
                    {
                        MessageBox.Show(Language.Msg("�Ѿ����ڳ�Ժ�����!"));
                        return -1;
                    }
                }
                //���������Ϣ
                if (this.iType == "1")//ICD10
                {
                    if (this.diagnoseMgr.InsertDiagnose(this.myDiagnose) == -1)
                    {
                        return -1;
                    }
                }
                else//ICDMEDICARE
                {
                    if (this.diagnoseMgrMc.InsertDiagnoseMC(this.myDiagnose) == -1)
                    {
                        return -1;
                    }
                }
            }
            this.isUpdate = false;
            this.diagnosisType.Focus();
            return 1;
        }
        /// <summary>
        /// ���������סԺ�Ų�ѯ�����Ϣ
        /// </summary>
        private void QueryDiagnose()
        {
            String InPNo = this.ucQueryInpatientNo.InpatientNo;
            // ��ѯ�����Ϣ
            ArrayList diagnoseList = this.diagnoseMgrMc.QueryDiagnoseBoth(InPNo);
            // ��������б�
            this.FillList(diagnoseList);
        }
        /// <summary>
        /// ��������б�
        /// </summary>
        /// <param name="arr"></param>
        private void FillList(ArrayList arr)
        {
            this.InitDiagnoseList();
            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose diagns = null;
                FS.HISFC.Models.Base.Spell dsType = null;
                FS.HISFC.Models.Base.Employee emp = null;
                String strDsType = "";
                String strDsTypeID = "";
                String strDrName = "";
                String mainFlag = "";
                ArrayList dsTypeList = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
                ArrayList drList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
                for (int i = 0; i < arr.Count; i++)
                {
                    diagns = (FS.HISFC.Models.HealthRecord.Diagnose)arr[i];
                    //�������������������
                    for (int j = 0; j < dsTypeList.Count; j++)
                    {
                        dsType = (FS.HISFC.Models.Base.Spell)dsTypeList[j];
                        if (dsType.ID.ToString() == diagns.DiagInfo.DiagType.ID)
                        {
                            strDsType = dsType.Name;
                            strDsTypeID = dsType.ID;
                            break;
                        }
                    }
                    //�������ҽ������
                    for (int j = 0; j < drList.Count; j++)
                    {
                        emp = (FS.HISFC.Models.Base.Employee)drList[j];
                        if (emp.ID.ToString() == diagns.DiagInfo.Doctor.ID)
                        {
                            strDrName = emp.Name;
                            break;
                        }
                    }
                    //�Ƿ������
                    if (diagns.DiagInfo.IsMain)
                    {
                        mainFlag = "1";
                    }
                    else
                    {
                        mainFlag = "0";
                    }
                    ListViewItem item1 = listView1.Items.Add("");
                    item1.SubItems.Add(strDsType);// �������
                    item1.SubItems.Add(diagns.DiagInfo.ICD10.Name);// �������
                    item1.SubItems.Add(strDrName);// ���ҽʦ
                    item1.SubItems.Add(diagns.DiagInfo.DiagDate.ToShortDateString());// ���ʱ��
                    item1.SubItems.Add("");// ״̬?
                    item1.SubItems.Add(diagns.DiagInfo.HappenNo.ToString());// �������
                    item1.SubItems.Add(diagns.OperType);// ��������
                    item1.SubItems.Add(diagns.DiagInfo.ICD10.ID);//ICD����
                    item1.SubItems.Add(diagns.DiagInfo.User03);//ICD��� 'ICD10';'ҽ��(��)';'ҽ��(ʡ)'
                    item1.SubItems.Add(mainFlag);//MAIN_FLAG �Ƿ������1��0��
                    if (strDsTypeID == "1" && mainFlag == "1" && diagns.DiagInfo.User03 == "ҽ��")
                    {
                        item1.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("���������Ϣ�б����!" + ex.Message));
                this.listView1.Clear();
                return;
            }
        }
        /// <summary>
        /// �õ�ѡ�е�ICD��Ϣ
        /// </summary>
        /// <returns></returns>
        private int GetICDInfo()
        {
            try
            {
                FS.HISFC.Models.HealthRecord.ICDMedicare item = null;
                if (this.ucDiagnose1.GetItemMc(ref item) == -1)
                {
                    return -1;
                }
                if (item == null) return -1;
                //ICD�������
                this.diagnosisCode.Text = item.ID;
                //ICD��ϱ���
                this.diagnosis.Text = item.Name;
                //ICDƴ����
                this.icdSpell = item.SpellCode;
                //ICD��� 1 ICD10�� 2 ��ҽ���� 3ʡҽ��
                this.iType = item.IcdType;

                this.ucDiagnose1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        private void DeleteDiagnose()
        {
            if (this.isUpdate)
            {
                if (MessageBox.Show("��ȷ��ɾ����ѡ�ļ�¼ô��", "����", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                if (iType == "1")//ICD10
                {
                    FS.HISFC.Models.HealthRecord.EnumServer.frmTypes ot = FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC;
                    if (this.operType == "1")//ҽ��վ
                    {
                        ot = FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC;
                    }
                    else//������
                    {
                        ot = FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS;
                    }
                    if (this.diagnoseMgr.DeleteDiagnoseSingle(this.ucQueryInpatientNo.InpatientNo, this.happenNo, ot, ServiceTypes.I) == -1)
                    {
                        MessageBox.Show(Language.Msg("ɾ��ʧ��!"));
                        return;
                    }
                }
                else//ICDMEDICARE
                {
                    if (this.diagnoseMgrMc.DeleteDiagnoseMedicare(this.ucQueryInpatientNo.InpatientNo, this.happenNo) == -1)
                    {
                        MessageBox.Show(Language.Msg("ɾ��ʧ��!"));
                        return;
                    }
                }                
                this.QueryDiagnose();
                MessageBox.Show(Language.Msg("ɾ���ɹ�!"));
                this.ClearAfterSave();
            }
            else
            {
                MessageBox.Show(Language.Msg("��ѡ��һ����¼!"));
                return;
            }
            return;
        }
        private int CheckBeforeSave()
        {
            if (this.txtName.Text == "")
            {
                MessageBox.Show(Language.Msg("�����뻼��סԺ�Ų����س�!"));
                this.ucDiagnose1.Visible = false;
                this.ucQueryInpatientNo.TextBox.Focus();
                return -1;
            }
            if (this.diagnosisType.Text == "")
            {
                MessageBox.Show(Language.Msg("��ѡ���������!"));
                this.ucDiagnose1.Visible = false;
                this.diagnosisType.Focus();
                return -1;
            }
            if (this.diagnosisDr.Text == "")
            {
                MessageBox.Show(Language.Msg("��ѡ�����ҽʦ!"));
                this.ucDiagnose1.Visible = false;
                this.diagnosisDr.Focus();
                return -1;
            }
            if (!this.isDiagSelect)
            {
                MessageBox.Show(Language.Msg("����б���ѡ����ϴ���!"));
                this.diagnosisCode.Focus();
                return -1;
            }
            if (this.diagnosisCode.Text == "")
            {
                MessageBox.Show(Language.Msg("��ѡ����ϴ���!"));
                this.diagnosisCode.Focus();
                return -1;
            }
            if (this.patientInfo.Pact.ID == "2" && this.combICDType.Tag.ToString() == "3")
            {
                MessageBox.Show(Language.Msg("�б����߲�������ʡ�����!"));
                this.combICDType.Focus();
                return -1;
            }
            if (this.patientInfo.Pact.ID == "3" && this.combICDType.Tag.ToString() == "2")
            {
                MessageBox.Show(Language.Msg("ʡ�����߲��������б����!"));
                this.combICDType.Focus();
                return -1;
            }
            //if (this.diagnosis.Text == "")
            //{
            //    MessageBox.Show(Language.Msg("��ѡ���������!"));
            //    return -1;
            //}
            return 1;
        }
        /// <summary>
        /// ��ʼ�����������б�
        /// </summary>
        //private void InitDeptList()
        //{
        //    //���������б�
        //    FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        //    ArrayList deptList = dept.GetDeptmentAll();
        //    this.deptList.AddItems(deptList);
        //}
        #endregion


        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ��,��ȴ�...");
            Application.DoEvents();
            //toolBarService.AddToolButton("ɾ��(F8)", "ɾ��(F8)", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //��ʼ�������б�
            InitCbox();
            //��ʼ���б�
            InitDiagnoseList();
            //��ʼ�������б�
            //InitDeptList();
            //��ʼ��ICD
            this.ucDiagnose1.InitICDMedicare("0");
            this.ucDiagnose1.SelectItem += new FS.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(this.ucDiagnose1_SelectItem);
            this.ucDiagnose1.Visible = false;

            return 1;
        }
        /// <summary>
        /// Ч���ж�
        /// </summary>
        protected virtual int ValidPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo.PVisit.InState.ID.ToString() != "I")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("����״̬���ǲ�������״̬,����¼��סԺ�����Ϣ��", 111);
                this.Clear();
                return -1;
            }
            return 1;
        }

        #endregion

        #region ����
        #endregion

        #endregion

        #region �¼�

        /// <summary>
        /// ucQueryInpatientNo�س��¼�
        /// </summary>
        private void ucQueryInpatientNo_myEvent()
        {
            if (this.FillPatientInfo() == -1)
            {
                return;
            }
            this.QueryDiagnose();
            this.diagnosisType.Focus();
        }
        /// <summary>
        /// ��ϴ����ý���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void diagnosisCode_Focused(object sender, EventArgs e)
        {
            //this.ucDiagnose1.Visible = true;
        }
        /// <summary>
        /// ��ϴ���ʧȥ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void diagnosisCode_LostFocus(object sender, EventArgs e)
        {
            // this.ucDiagnose1.Visible = false;
        }
        /// <summary>
        /// ������ݵõ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void diagnosis_GotFocus(object sender, EventArgs e)
        {
            this.diagnosisCode.Focus();
            this.ucDiagnose1.Visible = true;
        }
        /// <summary>
        /// ICD���仯�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combICDType_TextChanged(object sender, EventArgs e)
        {
            if (this.combICDType.SelectedItem != null)
            {
                String selectedId = this.combICDType.SelectedItem.ID;
                this.ucDiagnose1.InitICDMedicare(selectedId);
                this.diagnosisCode.Text = "";
                this.diagnosis.Text = "";
                this.ucDiagnose1.Visible = false;
           //     this.ucDiagnose1.SelectItem += new FS.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(this.ucDiagnose1_SelectItem);
            }
        }
        /// <summary>
        /// �������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewClick(object sender, EventArgs e)
        {
            this.diagnosisType.Text = this.listView1.SelectedItems[0].SubItems[1].Text;//�������
            this.diagnosis.Text = this.listView1.SelectedItems[0].SubItems[2].Text;//�������
            this.diagnosisDr.Text = this.listView1.SelectedItems[0].SubItems[3].Text;//���ҽ��
            this.diagnosisDate.Text = this.listView1.SelectedItems[0].SubItems[4].Text;//���ʱ��
            this.operType = this.listView1.SelectedItems[0].SubItems[7].Text;//��� 1 ҽ��վ¼�����  2 ������¼�����
            this.selectedIType = this.listView1.SelectedItems[0].SubItems[9].Text;//ICD��� 'ICD10';'ҽ��(��)';'ҽ��(ʡ)'
            //this.combICDType.Text = selectedIType;
            //д��������
            if (selectedIType == "ICD10")
            {
                this.combICDType.Text = selectedIType;
            }
            else//ҽ�����д�뻼�߶�Ӧ��������
            {
                this.combICDType.Tag = this.patientInfo.Pact.ID;
            }

            if (this.listView1.SelectedItems[0].SubItems[10].Text == "1")//�Ƿ������
            {
                this.isMain.Checked = true;
            }
            else
            {
                this.isMain.Checked = false;
            }
            try
            {
                this.happenNo = int.Parse(this.listView1.SelectedItems[0].SubItems[6].Text);//HappenNo
            }
            catch
            {
                MessageBox.Show(Language.Msg("��ȡHappenNo����!"));
                return;
            }
            this.diagnosisCode.Text = this.listView1.SelectedItems[0].SubItems[8].Text;//���ICD����
            this.GetICDInfo();
            this.isDiagSelect = true;
            this.isUpdate = true;
            //�������Ƿ��������{A53A9786-A56A-4f1f-980F-B5E77BB79E4B}
            if (this.listView1.SelectedItems[0].SubItems[10].Text == "1")// && this.listView1.SelectedItems[0].SubItems[1].Tag.ToString()== "1")
            {
                this.isOutMain = true;
            }
            else
            {
                this.isOutMain = false;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            if (isUpdate)
            {
                if (MessageBox.Show("��ȷ��������ѡ�ļ�¼ô��", "����", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return -1;
                }
            }
            if (this.CheckBeforeSave() == -1)
            {
                return -1;
            }
            if (this.Save() == -1)
            {
                MessageBox.Show(Language.Msg("���������Ϣ����!"));
                return -1;
            }
            this.ClearAfterSave();

            MessageBox.Show(Language.Msg("����ɹ�"));
            isUpdate = false;
            this.QueryDiagnose();
            return base.OnSave(sender, neuObject);
        }
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        { 
            this.toolBarService.AddToolButton("ɾ��", "ɾ��", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);  
            return this.toolBarService;
        }

        /// <summary>
        /// ���������Ӱ�ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ɾ��":
                    DeleteDiagnose();
                    break;
                default:
                    break;
            }
        }
       
        /// <summary>
        /// ICD����ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private int ucDiagnose1_SelectItem(Keys key)
        {
            GetICDInfo();
            this.isDiagSelect = true;
            return 0;
        }
        /// <summary>
        /// ��հ�ť��Ӧ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClear_Click(object sender, EventArgs e)
        {
            this.ClearAfterSave();
        }
        /// <summary>
        /// ��ϴ���仯�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void diagnosisCode_TextChanged(object sender, EventArgs e)
        {
            if (this.ucDiagnose1.Visible == false)
            {
                ucDiagnose1.Visible = true;
            }
            this.isDiagSelect = false;
            String str = this.diagnosisCode.Text;
            this.ucDiagnose1.Filter(str);
        }
        /// <summary>
        /// ������崥���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDiagnosis_Click(object sender, EventArgs e)
        {
            this.ucDiagnose1.Visible = false;
        }
        /// <summary>
        /// �����ϴ�������򴥷��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void diagnosisCode_Click(object sender, EventArgs e)
        {
            if (this.ucDiagnose1.Visible)
            {
                this.ucDiagnose1.Visible = false;
            }
            else
            {
                this.ucDiagnose1.Visible = true;
            }
        }
        #endregion

        private void diagnosisType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                this.diagnosisDr.Focus();
            }
        }

        private void diagnosisDr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                this.diagnosisDate.Focus();
            }
        }

        private void diagnosisDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                this.isMain.Focus();
            }
        }

        private void isMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                this.combICDType.Focus();
            }
        }

        private void combICDType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                this.diagnosisCode.Focus();
            }
        }

        private void diagnosisCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (ucDiagnose1.Visible)
            {
                if (e.KeyData == Keys.Up)
                {
                   this.ucDiagnose1.PriorRow();
                }
                if (e.KeyData == Keys.Down)
                {
                    this.ucDiagnose1.NextRow();
                }
                if (e.KeyData == Keys.Enter)
                {
                    ucDiagnose1_SelectItem(Keys.Enter);
                }
            }
        
        }

        #region �ӿ�ʵ��
        #endregion

    }

}