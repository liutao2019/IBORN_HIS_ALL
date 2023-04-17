using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
namespace FS.HISFC.Components.Order.Medical.Controls
{
    /// <summary>
    /// [��������: ��������]
    /// [�� �� ��: wangw]
    /// [����ʱ��: 2008-03-28]
    /// [�޸���:   �ų�]
    /// [�޸�ʱ��: 2008-06-27]
    /// </summary> 
    public partial class ucAllergyIn : FS.FrameWork.WinForms.Controls.ucBaseControl
    {


        public ucAllergyIn()
        {
            InitializeComponent();
        }

        #region ������

        #region �޸ĵĵط�

        private const int WM_KEYDOWN = 0x0100;
        private const int VK_RETURN = 0x0D;

        #endregion

        /// <summary>
        /// ��������ҵ����
        /// </summary>
        private FS.HISFC.BizLogic.Order.Medical.AllergyManager allergyManager = new FS.HISFC.BizLogic.Order.Medical.AllergyManager();

        /// <summary>
        /// סԺ����ʵ��
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatients = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// �����ۺ�ʵ��
        /// </summary>
        private FS.HISFC.Models.Order.Medical.AllergyInfo allergyInfo = null;
                
        private object currentPatient = new object();

        /// <summary>
        /// DataTable
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// DataView
        /// </summary>
        private DataView dv = null;

        private List<FS.HISFC.Models.Pharmacy.Item> items = new List<FS.HISFC.Models.Pharmacy.Item>();

        #endregion

        #region ����

        /// <summary>
        /// ����ʵ��
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo MyPatients
        {
            get
            {
                return this.myPatients;
            }
            set
            {
                this.myPatients = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void Init()
        {
            this.cmbAllergyDegree.AddItems(CacheManager.GetConList("ALLERGEN_SYMPTOM"));
            this.cmbAllergyType.AddItems(FS.HISFC.Models.Order.Medical.AllergyTypeEnumService.List());
            this.cmbPatientKind.AddItems(CacheManager.GetConList("APPLICABILITYAREA"));
            this.items = CacheManager.PhaIntegrate.QueryItemList(false);

            //this.cmbPatientKind.SelectedIndex = 0;
            this.cmbAllergyDegree.ResetText();
            this.cmbAllergyType.ResetText();
            this.cmbDrug.ResetText();
            this.lbHappenNo.Text = "�������";
            this.neuTextBox1.Clear();
            this.lbInpatientNo.Visible = false;

            cmbAllergyDegree.IsListOnly = true;
            cmbAllergyType.IsListOnly = true;
            cmbDrug.IsListOnly = false;
        }

        /// <summary>
        /// ��ʼ��FP
        /// </summary>
        private void InitFp()
        {
            this.dt.Reset();

            System.Type dtBol = System.Type.GetType("System.Boolean");
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtInt = System.Type.GetType("System.Int32");
            System.Type dtDateTime = System.Type.GetType("System.DateTime");
            this.dt.Columns.AddRange(
                new System.Data.DataColumn[]{
                    new DataColumn("����/סԺ��",dtStr),
                    new DataColumn("��������",dtStr),
                    new DataColumn("��������",dtStr),
                    new DataColumn("����Դ����",dtStr),
                    new DataColumn("����Դ����",dtStr),
                    new DataColumn("����֢״",dtStr),
                    new DataColumn("��Ч��",dtStr),
                    new DataColumn("��ע",dtStr),
                    new DataColumn("����Ա",dtStr),
                    new DataColumn("����ʱ��",dtDateTime),
                    new DataColumn("������",dtStr),
                    new DataColumn("����ʱ��",dtStr),
                    new DataColumn("��ˮ��",dtStr),
                    new DataColumn("�������",dtInt)
                }
                );
        }

        /// <summary>
        /// ��Ч��
        /// </summary>
        /// <returns></returns>
        private bool Vaild()
        {
            if (this.cmbPatientKind.Text == null || this.cmbPatientKind.Text == "")
            {
                MessageBox.Show("��ѡ��������!");
                return false;
            }
            if (this.cmbAllergyDegree.Text == null || this.cmbAllergyDegree.Text == "")
            {
                MessageBox.Show("��ѡ���߹���֢״!");
                return false;
            }
            if (this.cmbAllergyType.Text == null || this.cmbAllergyType.Text == "")
            {
                MessageBox.Show("��ѡ���߹�������!");
                return false;
            }

            #region �޸ĵĵط�
            if (!FS.FrameWork.Public.String.ValidMaxLengh(neuTextBox1.Text, 100))
            {
                MessageBox.Show("��ע�������ȣ�");
                return false;
            }
            #endregion

            return true;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            if (!this.Vaild())
            {
                return;
            }

            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.allergyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                allergyInfo = new FS.HISFC.Models.Order.Medical.AllergyInfo();
                DateTime sysTime = this.allergyManager.GetDateTimeFromSysDateTime();
                
                //1���ﻼ��/2סԺ����
                allergyInfo.PatientType = (FS.HISFC.Models.Base.ServiceTypes)FrameWork.Function.NConvert.ToInt32(this.cmbPatientKind.Tag);
                //�����Ż���סԺ��
                allergyInfo.PatientNO = this.MyPatients.PID.CaseNO;

                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //ҩƷԺ�ڴ���
                //obj.ID = this.cmbDrug.SelectedItem.ID.ToString();
                //�˴������ֶ��޸Ĺ���Դͷ
                if (string.IsNullOrEmpty(obj.ID))
                {
                    obj.ID = "A999";
                }
                else
                {
                    obj.ID = this.cmbDrug.Tag.ToString();
                }
                //����ҩ��
                //obj.Name = this.cmbDrug.SelectedItem.Name.ToString();
                obj.Name = this.cmbDrug.Text;
                allergyInfo.Allergen = obj;
                //1��Ƥ������ 2���ݿ� 3��ҩ��
                FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
                tempObj.ID = this.cmbAllergyDegree.SelectedItem.ID;
                tempObj.Name = this.cmbAllergyDegree.SelectedItem.Name;
                allergyInfo.Symptom = tempObj;
                //1��Ч/0��Ч
                allergyInfo.ValidState = !this.neuCheckBox1.Checked;
                //��ע
                allergyInfo.Remark = this.neuTextBox1.Text;
                //����Ա����
                allergyInfo.Oper.ID = this.allergyManager.Operator.ID;
                //����ʱ��(����)
                allergyInfo.Oper.OperTime = sysTime;
                if (this.neuCheckBox1.Checked == true)
                {
                    //������
                    allergyInfo.CancelOper.ID = this.allergyManager.Operator.ID;
                    //����ʱ��
                    allergyInfo.CancelOper.OperTime = sysTime;
                }
                //��������
                allergyInfo.Type = (FS.HISFC.Models.Order.Medical.AllergyType)Enum.Parse(typeof(FS.HISFC.Models.Order.Medical.AllergyType), this.cmbAllergyType.Tag.ToString());
                //סԺ��ˮ��
                allergyInfo.ID = this.MyPatients.ID;
                //�������
                if (this.lbHappenNo.Text == "�������")
                {
                    allergyInfo.HappenNo = this.allergyManager.GetMaxHappenNo(allergyInfo.ID, NConvert.ToInt32(allergyInfo.PatientType).ToString());
                }
                else
                {
                    allergyInfo.HappenNo = NConvert.ToInt32(this.lbHappenNo.Text.ToString());
                }
                
                if (this.allergyManager.SetAllergyInfo(allergyInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���������Ϣ��������!");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            this.QueryAllergyInfo();
            MessageBox.Show("����ɹ�");
            //this.Clear();
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        private void QueryAllergyInfo()
        {
            this.dt.Rows.Clear();
            this.neuSpread1_Sheet1.Rows.Count = 0;

            if (this.myPatients == null)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ������Ϣ...���Ժ�!");
            Application.DoEvents();
            ArrayList al = new ArrayList();
            //��ѯ������Ϣ
            if (this.cmbPatientKind.SelectedIndex == 0)
            {
                al = this.allergyManager.QueryAllergyInfo();
            }
            else if (string.Equals(this.currentPatient.GetType(), typeof(FS.HISFC.Models.RADT.PatientInfo)))
            {
                al = this.allergyManager.QueryAllergyInfo((this.currentPatient as FS.HISFC.Models.RADT.PatientInfo).PID.PatientNO,"2");
            }
            else if(string.Equals(this.currentPatient.GetType(), typeof(FS.HISFC.Models.Registration.Register)))
            {
                al = this.allergyManager.QueryAllergyInfo((this.currentPatient as FS.HISFC.Models.Registration.Register).PID.CardNO,"1");
            }

            if (al == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(this.allergyManager.Err);
                return;
            }

            try
            {
                foreach (object obj in al)
                {
                    allergyInfo = obj as FS.HISFC.Models.Order.Medical.AllergyInfo;
                    if (allergyInfo == null)
                    {
                        continue;
                    }

                    this.AddDataToTable(allergyInfo);
                }
                this.dv = new DataView(this.dt);

                this.neuSpread1_Sheet1.DataAutoSizeColumns = true;

                this.neuSpread1_Sheet1.DataSource = this.dv;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColAllergenID].Visible = false;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColInpatientNo].Visible = false;

            }
            catch (System.Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("��ѯ������Ϣ��������!" + ex.Message);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// DataTable��ֵ
        /// </summary>
        /// <param name="allergy"></param>
        protected void AddDataToTable(FS.HISFC.Models.Order.Medical.AllergyInfo allergy)
        {
            try
            {
                if (this.dt == null)
                {
                    this.InitFp();
                }

                string symptomName = string.Empty;

                switch (allergy.Symptom.ID)
                {
                    case "1":
                        symptomName = "Ƥ������";
                        break;
                    case "2":
                        symptomName = "�ݿ�";
                        break;
                    case "3":
                        symptomName = "ҩ��";
                        break;
                }

                string operTime = allergy.CancelOper.OperTime.Year < 1996 ? "" : allergy.CancelOper.OperTime.Year.ToString();


                this.dt.Rows.Add(new object[]{
                                                            allergy.PatientNO,//�����Ż���סԺ��
                                                            string.Equals(allergy.PatientType.ToString(),"I")?"סԺ����":"���ﻼ��",//1���ﻼ��/2סԺ����
                                                            GetAllergyTypeName( allergy.Type.ToString()),//��������
                                                            allergy.Allergen.ID,//ҩƷԺ�ڴ���
                                                            allergy.Allergen.Name,//����ҩ��
                                                            symptomName,//1��Ƥ������ 2���ݿ� 3��ҩ��
                                                            allergy.ValidState?"��Ч":"��Ч",//1��Ч/0��Ч
                                                            allergy.Remark,//��ע
                                                            SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(allergy.Oper.ID),//����Ա����
                                                            allergy.Oper.OperTime.ToString(),//����ʱ��(����)
                                                            SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(allergy.CancelOper.ID),//������
                                                            operTime,//����ʱ��                                                           
                                                            allergy.ID,//����Ż���סԺ��ˮ��
                                                            allergy.HappenNo.ToString()//�������
                                                        });
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("DataTable�ڸ�ֵ��������" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// ˫��Fp��������ӵ��ؼ�
        /// </summary>
        /// <param name="allergy"></param>
        protected void AddDataToControl(FS.HISFC.Models.Order.Medical.AllergyInfo allergy)
        {
            this.MyPatients.ID = allergy.ID;
            this.MyPatients.PID.CaseNO = allergy.PatientNO;

            //��������
            switch (NConvert.ToInt32(allergy.PatientType))
            {
                case 2:
                    this.cmbPatientKind.Tag = "2";
                    break;
                case 1:
                    this.cmbPatientKind.Tag = "1";
                    break;
                default:
                    this.cmbPatientKind.Tag = "0";
                    break;
            }

            this.cmbAllergyType.Tag = allergy.Type.ToString();
            this.cmbDrug.Tag = allergy.Allergen.ID.ToString();
            if (allergy.Allergen.ID.ToString() == "A999")
            {
                this.cmbDrug.Text = allergy.Allergen.Name.ToString();
            }
            this.neuTextBox1.Text = allergy.Remark;
            this.neuCheckBox1.Checked = !allergy.ValidState;
            this.cmbAllergyDegree.Tag = allergy.Symptom.ID;
            this.lbInpatientNo.Text = "��ˮ��:" + this.MyPatients.ID;
            this.lbHappenNo.Text = allergy.HappenNo.ToString();

        }

        #region �޸�
        protected override int OnSetValue(object neuObject, TreeNode e)
        {

            cmbDrug.SelectedIndex = -1;
            cmbAllergyDegree.SelectedIndex = -1;
            cmbAllergyType.SelectedIndex = -1;
            cmbDrug.SelectedIndex = -1;
            if (e == null || e.Parent == null)
            {
                this.Clear();
                cmbPatientKind.Enabled = true;
            }
            else
            {
                this.Clear();
                cmbPatientKind.Enabled = false;
                if (string.Equals(neuObject.GetType(), typeof(FS.HISFC.Models.RADT.PatientInfo)))
                {
                    FS.HISFC.Models.RADT.PatientInfo obj = neuObject as FS.HISFC.Models.RADT.PatientInfo;
                    cmbPatientKind.Tag = "2";
                    this.neuSpread1_Sheet1.RowCount = 0;
                    this.MyPatients.ID = obj.ID;
                    this.MyPatients.PID = obj.PID;
                    this.MyPatients.PID.CaseNO = obj.PID.PatientNO;
                    this.currentPatient = neuObject;
                    this.lblPatientInfo.Text = "������"+obj.Name + " �Ա�"+obj.Sex.Name +" ��������: "+obj.Birthday.ToString() +" ��ϵ�绰��"+obj.PhoneHome;
                }
                if (string.Equals(neuObject.GetType(), typeof(FS.HISFC.Models.Registration.Register)))
                {
                    FS.HISFC.Models.Registration.Register obj = neuObject as FS.HISFC.Models.Registration.Register;
                    cmbPatientKind.Tag = "1";
                    this.neuSpread1_Sheet1.RowCount = 0;
                    this.MyPatients.ID = obj.ID;
                    this.MyPatients.PID = obj.PID;
                    this.currentPatient = neuObject;
                    this.MyPatients.PID.CaseNO = obj.PID.CardNO;

                    this.lblPatientInfo.Text = "������" + obj.Name + " �Ա�" + obj.Sex.Name + " ��������: " + obj.Birthday.ToString() + " ��ϵ�绰��" + obj.PhoneHome;
                }
                this.QueryAllergyInfo();
            }

            return base.OnSetValue(neuObject, e);
        }

        private void Clear()
        {

            this.cmbAllergyDegree.ResetText();
            this.cmbAllergyType.ResetText();
            this.cmbDrug.ResetText();
            this.lbHappenNo.Text = "�������";
            
            this.neuTextBox1.Clear();
            this.lbInpatientNo.Visible = false;
            this.lbInpatientNo.Text = string.Empty;
            this.myPatients = new FS.HISFC.Models.RADT.PatientInfo();
            cmbPatientKind.SelectedIndex = -1;
            this.neuCheckBox1.Checked = false;
        }

        private string GetAllergyTypeName(string type)
        {
            switch (type)
            {
                case "DA": return "ҩ�����";
                case "FA": return "ʳ�����";
                case "MA": return "����͹���";
                case "MC": return "����ͽ���";
            }
            return "";
        }

        #endregion

        #endregion

        #region �¼�

        private void ucAllergyIn_Load(object sender, EventArgs e)
        {
            this.Init();
            this.InitFp();
            this.InitIHypoTest();
            this.dv = new DataView(this.dt);
            this.neuSpread1_Sheet1.DataSource = this.dv;
        }
         /// <summary>
        /// ��ʼ��Ƥ�Խӿ�ʵ��{D1B1616C-3863-40f6-AAD5-11D9161C6B14}
        /// </summary>
        private void InitIHypoTest()
        {
   
        }

        private void cmbPatientKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbInpatientNo.Text = string.Empty;
            if (this.cmbPatientKind.Tag.ToString() == "2")
            {
                this.lbInpatientNo.Visible = false;
            }
            else if (this.cmbPatientKind.Tag.ToString() == "1")
            {
                this.lbInpatientNo.Visible = false;
            }
            else if (this.cmbPatientKind.Tag.ToString() == "0")
            {
                this.lbInpatientNo.Visible = false;
            }
            else
            {
                this.lbInpatientNo.Visible = false;
            }
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void cmbAllergyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbDrug.SelectedIndex = -1;
            ArrayList al = new ArrayList();

            if (this.cmbAllergyType.Tag.ToString() == "FA")
            {
                this.cmbDrug.AddItems(CacheManager.GetConList("ALLERGEN_SOURCE"));
            }
            else
            {
                al = new ArrayList(this.items);
                
                this.cmbDrug.AddItems(al);
            }
            cmbDrug.SelectedIndex = -1;
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string patientNo = this.neuSpread1_Sheet1.Cells[e.Row, 0].Text;
            string inpatientNO = this.neuSpread1_Sheet1.Cells[e.Row, 12].Text;
            int happenNO = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[e.Row, 13].Text);
            ArrayList al = this.allergyManager.GetAllergyInfo(patientNo, inpatientNO, happenNO);
            FS.HISFC.Models.Order.Medical.AllergyInfo allergy = al[0] as FS.HISFC.Models.Order.Medical.AllergyInfo;
            this.AddDataToControl(allergy);
        }

        #endregion

        #region ��ö��

        private enum ColumnSet
        {
            //�����Ż���סԺ��
            ColPatientNO,
            //��������
            ColPatientKind,
            //��������
            ColAllergyType,
            //ҩƷԺ�ڴ���
            ColAllergenID,
            //ҩƷ����
            ColAllergenName,
            //����֢״
            ColSymptomName,
            //��Ч��
            ColVaild,
            //��ע
            ColMark,
            //����Ա����
            ColOperCode,
            //����ʱ��
            ColOperTime,
            //�����˱���
            ColCanclerID,
            //����ʱ��
            ColCancleTime,
            //��ˮ��
            ColInpatientNo,
            //�������
            ColHappenNo
        }

        #endregion
    }
}
