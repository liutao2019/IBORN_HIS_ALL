using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.WinForms.Report.InPatientInfo
{
    /// <summary>
    /// [��������: ���������ѯ]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-9-13]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucInpatientInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInpatientInfo()
        {
            InitializeComponent();
        }

        #region ����

        #region ҵ�����
        /// <summary>
        /// סԺ���תҵ���
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
        /// <summary>
        /// ��Աҵ�񼯳ɲ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager empManager = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// ��Ա��Ϣҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.InPatient feeManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ��ǰ����
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo currentPatient = new FS.HISFC.Models.RADT.PatientInfo();


        /// <summary>
        /// ������Ϣ��ѯ
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient queryPatient = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// Tab
        /// </summary>
        protected Hashtable hashTableFp = new Hashtable();
        #endregion
        
        #region DataTable���

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        DataTable dtMainPatientInfo = new DataTable();

        /// <summary>
        /// ��������Ϣ��ͼ
        /// </summary>
        DataView dvMainPatientInfo = new DataView();

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        DataTable dtPatientDiagnore = new DataTable();

        /// <summary>
        /// ���������Ϣ��ͼ
        /// </summary>
        DataView dvPatientDiagnore = new DataView();

        /// <summary>
        /// ��Ժ������Ϣ
        /// </summary>
        DataTable dtPatientOutHos = new DataTable();

        /// <summary>
        /// ��Ժ������Ϣ��ͼ
        /// </summary>
        DataView dvPatientOutHos = new DataView();

        /// <summary>
        /// סԺ�����˺���Ϣ
        /// </summary>
        DataTable dtPatientNoFee = new DataTable();

        /// <summary>
        /// סԺ�����˺���Ϣ��ͼ
        /// </summary>
        DataView dvPatientNoFee = new DataView();
        #endregion

        #region ��ѯ��ر���
        /// <summary>
        /// ����״̬
        /// </summary>
        string inState = string.Empty;
        /// <summary>
        /// ���Ҵ���
        /// </summary>
        string deptCode = string.Empty;//סԺ���ұ���
        /// <summary>
        /// ҽ������
        /// </summary>
        string docCode = string.Empty;//ҽ���������
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        string beginTime = string.Empty;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        string endTime = string.Empty;
        /// <summary>
        /// �������
        /// </summary>
        string diagnoreName = string.Empty;

        #endregion

        #endregion

        #region ��ʼ������

        /// <summary>
        /// �����ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInpatientInfo_Load(object sender, EventArgs e)
        {
            //Ĭ����ʾ������Ϣ
            this.neuTabControl1.SelectedTab = this.tabPage1;
            this.Init();
        }

        /// <summary>
        /// ��ʼ�� 
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Init()
        {
            //��ʼ������
            if (this.InitDept() == -1)
            {
                return -1;
            }

            //��ʼ������״̬
            if (this.InitInState() == -1)
            {
                return -1;
            }

            if (this.InitNoFee() == -1)
            {
                return -1;
            }
            if (this.InitDoc() == -1)
            {
                return -1;
            }
            if (this.InitOutHos() == -1)
            {
                return -1;
            }
            if (this.InitPatient()== -1)
            {
                return -1;
            }
            if (this.InitDiagnore() == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��ʼ������״̬
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitInState()
        {
            ArrayList inStateList = FS.HISFC.Models.RADT.InStateEnumService.List();

            this.cmbState.AddItems(inStateList);
            return 1;
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitDept()
        {
            int findAll = 0;
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "%";
            objAll.Name = "ȫ��";

            ArrayList deptList = this.deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            if (deptList == null)
            {
                MessageBox.Show(Language.Msg("���ؿ����б����!") + this.deptManager.Err);

                return -1;
            }

            deptList.Add(objAll);

            findAll = deptList.IndexOf(objAll);

            this.cmbDept.AddItems(deptList);

            if (findAll >= 0)
            {
                this.cmbDept.SelectedIndex = findAll;
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ��ҽ���б�
        /// </summary>
        /// <returns></returns>
        private int InitDoc()
        {
            ArrayList docList = this.empManager.QueryEmployeeAll();
            if (docList == null)
            {
                MessageBox.Show(Language.Msg("����ҽ���б����!") + this.deptManager.Err);

                return -1;
            }
            this.cmbDoc.AddItems(docList);
            return 1;
        }

        Type str = typeof(String);
        Type date = typeof(DateTime);
        Type dec = typeof(Decimal);
        Type bo = typeof(bool);

        #region סԺ������Ϣ��ʼ��
        private int InitPatient()
        {
            
            this.dtMainPatientInfo.Columns.AddRange(new DataColumn[]{new DataColumn("סԺ��ˮ��", str),
																new DataColumn("סԺ��", str),
																new DataColumn("����", str),
																new DataColumn("סԺ����", str),
																new DataColumn("����", str),
																new DataColumn("�������", str),
																new DataColumn("Ԥ����(δ��)", dec),
																new DataColumn("���úϼ�(δ��)", dec),
																new DataColumn("���", dec),
																new DataColumn("�Է�", dec),
																new DataColumn("�Ը�", dec),
																new DataColumn("����", dec),
																new DataColumn("��Ժ����", date),
																new DataColumn("��Ժ״̬", str),
																new DataColumn("��Ժ����", str),
																new DataColumn("Ԥ����(�ѽ�)", dec),
																new DataColumn("���úϼ�(�ѽ�)", dec),
																new DataColumn("��������", date)/*,
																new DataColumn("ҽ�����", str)*/});

            dtMainPatientInfo.PrimaryKey = new DataColumn[] { dtMainPatientInfo.Columns["סԺ��ˮ��"] };

            dvMainPatientInfo = new DataView(dtMainPatientInfo);

            this.neuSpread1_Sheet1.DataSource = dvMainPatientInfo;
            return 1;
        }

        #endregion

        #region �������

        private int InitDiagnore()
        {
            this.dtPatientDiagnore.Columns.AddRange(new DataColumn[] {new DataColumn("סԺ��",str), 
                new DataColumn("����",str),new DataColumn("�Ա�",str),new DataColumn("����",str),
                new DataColumn("����",str),new DataColumn("��Ժ����",date),new DataColumn("��Ժ����",date),
                new DataColumn("��Ҫ���",str)}
               );
            //dtPatientDiagnore.PrimaryKey = new DataColumn[] { dtPatientDiagnore.Columns["סԺ��"] };
            dvPatientDiagnore = new DataView(dtPatientDiagnore);
            this.neuSpread2_Sheet1.DataSource = dtPatientDiagnore;
            return 1;
        }

        #endregion

        #region ��Ժ�������

        private int InitOutHos()
        {
            this.dtPatientOutHos.Columns.AddRange(new DataColumn[] { new DataColumn("����",str),
            new DataColumn("סԺ��",str),new DataColumn("����",str),new DataColumn("�Ա�",str),
                new DataColumn("����",str),new DataColumn("��Ժ����",date),new DataColumn("��Ժ����",date)});
            dtPatientOutHos.PrimaryKey = new DataColumn[] { dtPatientOutHos.Columns["סԺ��"] };
            dvPatientOutHos = new DataView(dtPatientOutHos);
            this.neuSpread3_Sheet1.DataSource = dtPatientOutHos;
            return 1;
        }
        #endregion
      
        #region ��Ժ�����˺����
        private int InitNoFee()
        {
            this.dtPatientNoFee.Columns.AddRange(new DataColumn[] {
             new DataColumn("סԺ��",str),new DataColumn("����",str),new DataColumn("��Ժ�Ʊ�",str),
             new DataColumn("��Ժ����",date),new DataColumn("�˺�����",date)});
            dtPatientNoFee.PrimaryKey = new DataColumn[] { dtPatientNoFee.Columns["סԺ��"] };
            this.neuSpread4_Sheet1.DataSource = dtPatientNoFee;
            return 1;
        }
        #endregion

        #endregion

        #region ��ѯ�¼�

        /// <summary>
        /// ���ϲ�ѯ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSearch_Click(object sender, EventArgs e)
        {
            if (this.cmbState.Text == null)
            {
                 MessageBox.Show(Language.Msg("�����뻼�ߵĲ�ѯ״̬!"));
                return;
            }
            if (this.cmbDept.Text == string.Empty || this.cmbDept.Text == "ȫ��")
            {
                deptCode = "ALL";
                this.cmbDept.Text = "ȫ��";
            }
            else
            {
                deptCode = this.cmbDept.Tag.ToString();
            }
            //ҽ������
            if (this.cmbDoc.Text == string.Empty)
            {
                docCode = "ALL";
            }
            else
            {
                docCode = this.cmbDoc.Tag.ToString();
            }
            //����״̬
            inState = this.cmbState.Tag.ToString();

            Cursor.Current = Cursors.WaitCursor;

            //���������ж�
            if (this.cmbState.Text == "��Ժ�Ǽ����,����״̬")
            {
                inState = "B";
                this.neuTextBox2.Enabled = true;
                this.cmbDoc.Enabled = true;
                this.beginTime = this.dtBeginTime.Value.ToString();
                this.endTime = this.dtEndTime.Value.ToString();
                this.neuTabControl1.SelectedTab = this.tabPage3;
                this.QueryPatientDiagnore(deptCode, diagnoreName, beginTime, endTime);
                this.QueryPatientOutHos(inState, deptCode, docCode, beginTime, endTime);
                ////�ж��ǽ�����ϲ�ѯ���ǳ�Ժ������Ϣ��ѯ
                //if (this.cmbDoc.Text != string.Empty)//Ĭ��Ϊ��Ժ���߲�ѯ
                //{
                //    this.neuTextBox2.Enabled = true;
                //    //docCode = this.cmbDoc.Tag.ToString();
                //    this.neuTabControl1.SelectedTab = this.tabPage3;
                //    this.QueryPatientOutHos(inState, deptCode, docCode, beginTime, endTime);                   
                //}
                //else if (this.cmbDoc.Text == string.Empty)//�ж��Ƿ�Ϊ��ϲ�ѯ
                //{
                //    this.neuTextBox2.Enabled = true;
                //    this.cmbDoc.Enabled = true;
                //    if (this.neuTextBox2.Text != string.Empty)//Ĭ��Ϊ��ϲ�ѯ
                //    {
                //        this.cmbDoc.Enabled = false;
                //        this.diagnoreName = this.neuTextBox2.Text;
                //        this.QueryPatientDiagnore(deptCode, diagnoreName, beginTime, endTime);
                //        this.neuTabControl1.SelectedTab = this.tabPage2;
                //    }
                //    else if (this.neuTextBox2.Text == string.Empty)//��ѯ��Ժ������Ϣ�������Ϣ
                //    {
                //        this.cmbDoc.Enabled = true;
                //        this.neuTextBox2.Enabled = true;
                //        docCode = "ALL";
                //        this.QueryPatientDiagnore(deptCode, diagnoreName, beginTime, endTime);
                //        this.QueryPatientOutHos(inState, deptCode, docCode, beginTime, endTime);
                //    }
                //}
            }
            else if (this.cmbState.Text == "��Ժ�������")
            {
                this.inState = "O";
                this.beginTime = this.dtBeginTime.Value.ToString();
                this.endTime = this.dtEndTime.Value.ToString();
                this.diagnoreName = this.neuTextBox2.Text;
                this.neuTabControl1.SelectedTab = this.tabPage3;
                this.QueryPatientDiagnore(deptCode, diagnoreName, beginTime, endTime);
                this.QueryPatientOutHos(inState, deptCode, docCode, beginTime, endTime);
                //if (this.cmbDoc.Text != string.Empty)//Ĭ��Ϊ��Ժ���߲�ѯ
                //{
                //    this.neuTextBox2.Enabled = true;
                //    docCode = this.cmbDoc.Tag.ToString();
                //    this.neuTabControl1.SelectedTab = this.tabPage3;
                //    this.QueryPatientOutHos(inState, deptCode, docCode, beginTime, endTime);
                //}
                //else if (this.cmbDoc.Text == string.Empty)//�ж��Ƿ�Ϊ��ϲ�ѯ
                //{
                //    this.neuTextBox2.Enabled = true;
                //    this.cmbDoc.Enabled = true;
                //    if (this.neuTextBox2.Text != string.Empty)//Ĭ��Ϊ��ϲ�ѯ
                //    {
                //        this.cmbDoc.Enabled = false;
                //        this.diagnoreName = this.neuTextBox2.Text;
                //        this.QueryPatientDiagnore(deptCode, diagnoreName, beginTime, endTime);
                //        this.neuTabControl1.SelectedTab = this.tabPage2;
                //    }
                //    else if (this.neuTextBox2.Text == string.Empty)//��ѯ��Ժ������Ϣ�������Ϣ
                //    {
                //        this.cmbDoc.Enabled = true;
                //        this.neuTextBox2.Enabled = true;
                //        docCode = "ALL";
                //        this.QueryPatientDiagnore(deptCode, diagnoreName, beginTime, endTime);
                //        this.QueryPatientOutHos(inState, deptCode, docCode, beginTime, endTime);
                //    }
                //}
            }
              else if (this.cmbState.Text == "�޷���Ժ")
                {
                    this.beginTime = this.dtBeginTime.Value.ToString();
                    this.endTime = this.dtEndTime.Value.ToString();
                    this.neuTabControl1.SelectedTab = tabPage4;
                    this.QueryPatientNoFee(deptCode, beginTime, endTime);
                }
                Cursor.Current = Cursors.Arrow;
            //this.Clear();
        }

        /// <summary>
        /// �򵥲�ѯ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btQuery_Click(object sender, EventArgs e)
        {
            if (this.ucQueryInpatientNo1.Text.Length > 0)
            {
                this.ucQueryInpatientNo1_myEvent();
                this.neuTabControl1.SelectedTab = this.tabPage1;
            }
            else//��Ϊ����������ѯ
            {
                this.QueryPatientByName();
                this.neuTabControl1.SelectedTab = this.tabPage1;
            }
            //this.Clear();
        }

        /// <summary>
        /// ˫����ʾ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return;
            }
            string inpatientNO = this.neuSpread1_Sheet1.Cells[e.Row, 0].Text;

            if (inpatientNO == null)
            {
                return;
            }

            this.currentPatient = this.radtManager.QueryPatientInfoByInpatientNO(inpatientNO);
            if (this.currentPatient == null || this.currentPatient.ID == null || this.currentPatient.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("��ѯ���߻�����Ϣ����!") + this.radtManager.Err);

                return;
            }

            //���ò�ѯʱ��
            DateTime beginTime = this.currentPatient.PVisit.InTime;
            DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

            this.QueryAllInformation(beginTime, endTime);
        }

        /// <summary>
        /// �¼�
        /// </summary>
        private void ucQueryInpatientNo1_myEvent()
        {
            this.QueryPatientByInpatientNO();
        }
        #endregion

        #region ����ʵ��

        #region ������Ϣ

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="patients">������Ϣ�б�</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int QueryPatient(ArrayList patients)
        {
            this.dtMainPatientInfo.Rows.Clear();

            Cursor.Current = Cursors.WaitCursor;
            //סԺ������Ϣ
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in patients)
            {
                this.SetPatientToFpMain(patient);
            }

            Cursor.Current = Cursors.Arrow;

            if (patients.Count == 1)
            {
                this.currentPatient = (FS.HISFC.Models.RADT.PatientInfo)patients[0];
               // this.SetPatientInfo();
                this.ucQueryInpatientNo1.Text = this.currentPatient.ID.Substring(4);
                //���ò�ѯʱ��
                //���ò�ѯʱ��
                DateTime beginTime = this.currentPatient.PVisit.InTime;
                DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

                this.QueryAllInformation(beginTime, endTime);
            }
            this.neuSpread1_Sheet1.Columns[13].Width = 180;
            return 1;
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// <param name="patient">�ɹ� 1 ʧ�� -1</param>
        private void SetPatientToFpMain(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            DataRow row = this.dtMainPatientInfo.NewRow();

            try
            {

                row["סԺ��ˮ��"] = patient.ID;
                row["סԺ��"] = patient.PID.PatientNO;
                row["����"] = patient.Name;
                row["סԺ����"] = patient.PVisit.PatientLocation.Dept.Name;
                row["����"] = patient.PVisit.PatientLocation.Bed.ID;
                row["�������"] = patient.Pact.Name;
                row["Ԥ����(δ��)"] = patient.FT.PrepayCost;
                row["���úϼ�(δ��)"] = patient.FT.TotCost;
                row["���"] = patient.FT.LeftCost;
                row["�Է�"] = patient.FT.OwnCost;
                row["�Ը�"] = patient.FT.PayCost;
                row["����"] = patient.FT.PubCost;
                row["��Ժ����"] = patient.PVisit.InTime;
                row["��Ժ״̬"] = patient.PVisit.InState.Name;

                row["��Ժ����"] = patient.PVisit.OutTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : patient.PVisit.OutTime.ToString();

                row["Ԥ����(�ѽ�)"] = patient.FT.BalancedPrepayCost;
                row["���úϼ�(�ѽ�)"] = patient.FT.BalancedCost;
                //row["ҽ�����"] = patient.PVisit.MedicalType.Name;
                row["��������"] = patient.BalanceDate;

                this.dtMainPatientInfo.Rows.Add(row);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }
        }

        /// <summary>
        /// ���������סԺ�Ų�ѯ���߻�����Ϣ
        /// </summary>
        private void QueryPatientByInpatientNO()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == string.Empty)
            {
                MessageBox.Show(Language.Msg("�������סԺ�Ŵ���,����������!"));
                this.Clear();

                return;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtManager.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
            if (patientInfo == null || patientInfo.ID == null || patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("��ȡ���߻�����Ϣ����!") + this.radtManager.Err);
                this.Clear();
                return;
            } 

            this.neuTextBox1.Text = patientInfo.Name;
            this.btSearch.Focus();

            ArrayList patientListTemp = new ArrayList();

            patientListTemp.Add(patientInfo);

            this.QueryPatient(patientListTemp);

            //lvxl
            //this.QueryPatientDiagnore(patientInfo.PVisit.PatientLocation.Dept.ID,
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        protected void QueryAllInformation(DateTime beginTime, DateTime endTime)
        {
            //this.GetQueryItem();
            //this.QueryPatientDiagnore(beginTime, endTime);

            //this.QueryPatientOutHos(beginTime, endTime);

            //this.QueryPatientNoFee(beginTime, endTime);

           // this.QueryPatientBalance(beginTime, endTime);
        }

        /// <summary>
        /// ��������Ļ���������ѯ���߻�����Ϣ
        /// </summary>
        private void QueryPatientByName()
        {
            if (this.neuTextBox1.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Language.Msg("������������Ϊ��!"));
                this.neuTextBox1.Focus();

                return;
            }

            string inputName = "%" + this.neuTextBox1.Text.Trim() + "%";
            //ȥ�������ַ�
            inputName = FS.FrameWork.Public.String.TakeOffSpecialChar(inputName, "'");
            //��������ֱ�Ӳ�ѯ������ϸ��Ϣ
            string name = this.neuTextBox1.Text;
            ArrayList patientListTemp = this.radtManager.QueryPatientInfoByName(inputName);
            if (patientListTemp == null || patientListTemp.Count == 0)
            {
                MessageBox.Show(Language.Msg("�޴˻�����Ϣ!") + this.radtManager.Err);

                this.Clear();
                this.neuTextBox1.Text = name;
                return;
            }

            if (patientListTemp.Count > 0)
            {
                this.Clear();
                this.neuTextBox1.Text = name;
                this.QueryPatient(patientListTemp);
            }

            return;
        }

        /// <summary>
        /// ��ϲ�ѯ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="diagnoreName"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        private void QueryPatientDiagnore(string deptCode, string diagnoreName, string beginTime, string endTime)
        {
            dtPatientDiagnore.Clear();
            ArrayList patientDiagnore = this.queryPatient.PatientDiagnoreQuery(deptCode, diagnoreName, beginTime, endTime);
            if (patientDiagnore == null)
            {
                MessageBox.Show(Language.Msg("��û�����ϳ���!") + this.queryPatient.Err);
                return;
            }
            foreach(FS.HISFC.Models.RADT.PatientInfo patient in patientDiagnore)
            {
                DataRow row = dtPatientDiagnore.NewRow();
                row["סԺ��"] = patient.PID.PatientNO;
                row["����"] =patient.Name;
                row["�Ա�"] =patient.Sex.ID;
                row["����"] =patient.Age;
                row["����"] =patient.PVisit.PatientLocation.Dept.Name;
                row["��Ժ����"] =patient.PVisit.InTime.ToString();
                row["��Ժ����"] =patient.PVisit.OutTime.ToString();
                row["��Ҫ���"] = patient.MainDiagnose;
                this.dtPatientDiagnore.Rows.Add(row);
            }
        }

        /// <summary>
        /// ��Ժ���߲�ѯ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="docCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        private void QueryPatientOutHos(string inState, string deptCode, string docCode, string beginTime, string endTime)
        {
            this.dtPatientOutHos.Rows.Clear();
            ArrayList OutHosPatient = this.queryPatient.PatientOutHosQuery(inState, deptCode, docCode, beginTime, endTime);
            if (OutHosPatient == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��Ժ������Ϣ����!") + this.queryPatient.Err);
                return;
            }
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in OutHosPatient)
            {
                DataRow row = this.dtPatientOutHos.NewRow();
                row["����"] = patient.PVisit.PatientLocation.Bed.ID;
                row["סԺ��"] = patient.PID.PatientNO;
                row["����"] = patient.Name;
                row["�Ա�"] = patient.Sex.ID;
                row["����"] = patient.PVisit.PatientLocation.Dept.Name;
                row["��Ժ����"] = patient.PVisit.InTime.ToString();
                row["��Ժ����"] = patient.PVisit.OutTime.ToString();
                this.dtPatientOutHos.Rows.Add(row);
            }
        }

        /// <summary>
        /// סԺ�����˺Ų�ѯ
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        private void QueryPatientNoFee(string deptCode, string beginTime, string endTime)
        {
            this.dtPatientNoFee.Rows.Clear();
            ArrayList NoFeePatient = this.queryPatient.PatientNoFeeQuery(deptCode, beginTime, endTime);
            if (NoFeePatient == null)
            {
                MessageBox.Show(Language.Msg("��ȡ�޷ѻ�����Ϣ����!") + this.queryPatient.Err);
                return;
            }
            
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in NoFeePatient)
            {
                DataRow row = this.dtPatientNoFee.NewRow();
                row["סԺ��"] = patient.PID.PatientNO;
                row["����"] = patient.Name;
                row["��Ժ�Ʊ�"] = patient.PVisit.PatientLocation.Dept.Name;
                row["��Ժ����"] = patient.PVisit.InTime.ToString();
                row["�˺�����"] = patient.PVisit.OutTime.ToString();
                dtPatientNoFee.Rows.Add(row);
            }
        }

        #endregion


        #region ��������
        /// <summary>
        /// ��պ���
        /// </summary>
        private void Clear()
        {
            this.cmbDept.Text="";
            this.cmbDoc.Text="";
            this.cmbState.Text = "";
            this.ucQueryInpatientNo1.Text = "";
            this.neuTextBox1.Text = "";
            this.dtBeginTime.Value = this.radtManager.GetDateTimeFromSysDateTime();
            this.dtEndTime.Value = this.radtManager.GetDateTimeFromSysDateTime();
            //{F7217BB5-C76E-45a0-9AB0-0D536C8993D1} lvxl 2010-3-11
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                //this.neuSpread1_Sheet1.Rows.Clear();
                dtMainPatientInfo.Clear();
            }
            if (this.neuSpread2_Sheet1.Rows.Count > 0)
            {
                //this.neuSpread2_Sheet1.Rows.Clear();
                dtPatientDiagnore.Clear();
            }
            if (this.neuSpread3_Sheet1.Rows.Count > 0)
            {
                //this.neuSpread3_Sheet1.Rows.Clear();
                dtPatientOutHos.Clear();
            }
            if (this.neuSpread4_Sheet1.Rows.Count > 0)
            {
                //this.neuSpread4_Sheet1.Rows.Clear();
                dtPatientNoFee.Clear();
            }
        }

        #endregion

        /// <summary>
        /// ���ò�ѯ��ʱ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbState.Text == "��Ժ�Ǽ����,����״̬")
            {
                this.neuTabControl1.SelectedTab = this.tabPage3;
                this.neuTextBox2.Enabled = false;
                this.cmbDoc.Enabled = true;
                this.cmbDept.Enabled = true;
                this.dtBeginTime.Enabled = true;
                this.dtEndTime.Enabled = true;
                this.btSearch.Enabled = true;
            }
            else if (this.cmbState.Text == "��Ժ�������")
            {
                this.neuTabControl1.SelectedTab = this.tabPage2;
                this.neuTextBox2.Enabled = true;
                this.cmbDoc.Enabled = true;
                this.cmbDept.Enabled = true;
                this.dtBeginTime.Enabled = true;
                this.dtEndTime.Enabled = true;
                this.btSearch.Enabled = true;
            }
            else if (this.cmbState.Text == "�޷���Ժ")
            {
                this.neuTabControl1.SelectedTab = this.tabPage4;
                this.cmbDoc.Enabled = false;
                this.neuTextBox2.Enabled = false;
                this.cmbDept.Enabled = true;
                this.dtBeginTime.Enabled = true;
                this.dtEndTime.Enabled = true;
                this.btSearch.Enabled = true;
            }
            else
            {
                this.cmbDept.Enabled = false;
                this.cmbDoc.Enabled = false;
                this.dtBeginTime.Enabled = false;
                this.dtEndTime.Enabled = false;
                this.neuTextBox2.Enabled = false;
                this.btSearch.Enabled = false;
            }
        }

        #endregion
    }
}
