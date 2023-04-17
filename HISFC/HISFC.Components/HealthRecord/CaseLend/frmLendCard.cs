using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Object.HealthRecord.EnumServer;
namespace UFC.HealthRecord.CaseLend
{
    public partial class frmLendCard : Form
    {
        public frmLendCard()
        {
            InitializeComponent();
        }
        #region ȫ�ֱ���
        private Neusoft.HISFC.Management.HealthRecord.CaseCard card = new Neusoft.HISFC.Management.HealthRecord.CaseCard();
        private Neusoft.HISFC.Management.HealthRecord.Base baseDml = new Neusoft.HISFC.Management.HealthRecord.Base();
        private System.Data.DataTable dt = null;
        private ArrayList Caselist = null;
        //���Ŀ���Ϣ 
        private Neusoft.HISFC.Object.HealthRecord.ReadCard Cardinfo = null;
        //������Ϣ
        private Neusoft.HISFC.Object.HealthRecord.Base PatientCaseInfo = null;
        #endregion
        private void caseNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {
                    if (this.caseNo.Text == "")
                    {
                        this.caseNo.Focus();

                        MessageBox.Show("�����벡����");
                        return;
                    }
                    Caselist = null;
                    Caselist = baseDml.QueryCaseBaseInfoByCaseNO(this.caseNo.Text);
                    if (Caselist == null)
                    {
                        MessageBox.Show("��ѯ������Ϣ����");
                        return;
                    }
                    if (Caselist.Count == 0)
                    {
                        MessageBox.Show("û�в鵽�����Ϣ");
                        return;
                    }
                    //�ж��Ƿ��Ѿ������ 
                    Neusoft.HISFC.Object.HealthRecord.Base info = (Neusoft.HISFC.Object.HealthRecord.Base)Caselist[0];
                    if (info.LendStat == "O") //����ĸ O 
                    {
                        MessageBox.Show("�ò����Ѿ����.");
                        return;
                    }
                    AddTableInfo(Caselist);
                    this.CardNO.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private void AddTableInfo(ArrayList list)
        {
            dt.Clear();
            int i = 0;
            foreach (Neusoft.HISFC.Object.HealthRecord.Base info in list)
            {
                string Sex = "";
                if (info.PatientInfo.Sex.ID != null)
                {
                    if (info.PatientInfo.Sex.ID.ToString() == "M")
                    {
                        Sex = "��";
                    }
                    else if (info.PatientInfo.Sex.ID.ToString() == "F")
                    {
                        Sex = "Ů";
                    }
                }
                if (i == 0)
                {
                    PatientCaseInfo = info.Clone(); //����
                    SetInfo(PatientCaseInfo);
                    i++;
                }
                dt.Rows.Add(new object[] {info.PatientInfo.PID.PatientNO,
										   info.PatientInfo.Name,
										   Sex,
										   info.InDept.Name,
										   info.OutDept.Name,
										   info.PatientInfo.PVisit.InTime,
										   info.PatientInfo.PVisit.OutTime ,
										   info.PatientInfo.Birthday,
										   info.PatientInfo.InTimes.ToString()
										  });
            }

            this.fpSpread1_Sheet1.Columns[0].Width = 60;
            this.fpSpread1_Sheet1.Columns[1].Width = 60;
            this.fpSpread1_Sheet1.Columns[2].Width = 30;
            this.fpSpread1_Sheet1.Columns[3].Width = 60;
            this.fpSpread1_Sheet1.Columns[4].Width = 60;
            this.fpSpread1_Sheet1.Columns[5].Width = 60;
            this.fpSpread1_Sheet1.Columns[6].Width = 60;
            this.fpSpread1_Sheet1.Columns[7].Width = 60;
            this.fpSpread1_Sheet1.Columns[8].Width = 60;

        }
        /// <summary>
        /// ��ֵ
        /// </summary>
        /// <param name="info"></param>
        private void SetInfo(Neusoft.HISFC.Object.HealthRecord.Base info)
        {
            string Sex = "";
            if (info.PatientInfo.Sex.ID != null)
            {
                if (info.PatientInfo.Sex.ID.ToString() == "M")
                {
                    Sex = "��";
                }
                else if (info.PatientInfo.Sex.ID.ToString() == "F")
                {
                    Sex = "Ů";
                }
            }
            caseNo.Text = info.CaseNO;
            txName.Text = info.PatientInfo.Name;
            txSex.Text = Sex;
            txDeptIn.Text = info.InDept.Name;
            txDeptOut.Text = info.OutDept.ID;
            dtInDate.Text = info.PatientInfo.PVisit.InTime.ToString();
            dtOutDate.Text = info.PatientInfo.PVisit.OutTime.ToString();
            dtBirthDate.Text = info.PatientInfo.Birthday.ToString();
        }
        /// <summary>
        /// ��ղ�����Ϣ
        /// </summary>
        private void ClearCase()
        {
            caseNo.Text = "";
            txName.Text = "";
            txSex.Text = "";
            txDeptIn.Text = "";
            txDeptOut.Text = "";
            dtInDate.Text = "";
            dtOutDate.Text = "";
            dtBirthDate.Text = "";
            if (dt != null)
            {
                dt.Clear();
            }
        }
        /// <summary>
        /// �����Ա��Ϣ
        /// </summary>
        private void ClearPerson()
        {
            CardNO.Text = "";
            comPerson.Text = "";
            //			txDays.Text = "";
            comType.Text = "";
            txReturnTime.Value = Convert.ToDateTime("3000-1-1");
        }
        private void frmLendCard_Load(object sender, System.EventArgs e)
        {
            InitDateTable();
            Neusoft.HISFC.Management.Manager.Person person = new Neusoft.HISFC.Management.Manager.Person();
            //��ȡ��Ա�б�
            ArrayList DoctorList = person.GetEmployeeAll();
            this.comPerson.AppendItems(DoctorList);
            txReturnTime.Value = Neusoft.NFC.Function.NConvert.ToDateTime(baseDml.GetSysDate()).AddDays(14);
            comPerson.BackColor = System.Drawing.Color.White;
        }
        private void InitDateTable()
        {
            dt = new System.Data.DataTable();
            Type strType = typeof(System.String);
            Type intType = typeof(System.Int32);
            Type dtType = typeof(System.DateTime);
            Type boolType = typeof(System.Boolean);

            dt.Columns.AddRange(new DataColumn[]{   new DataColumn("������", strType),
													new DataColumn("����", strType),
													new DataColumn("�Ա�", strType),
													new DataColumn("��Ժ����", strType),
													new DataColumn("��Ժ����", strType),
													new DataColumn("��Ժ����", dtType),
													new DataColumn("��Ժ����", dtType),
													new DataColumn("��������", dtType),
													new DataColumn("����", intType)});
            this.fpSpread1_Sheet1.DataSource = dt;
        }

        private void CardNO_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (CardNO.Text == "")
                {
                    CardNO.Focus();
                    MessageBox.Show("�����뿨��");
                    return;
                }
                Cardinfo = null;
                Cardinfo = card.GetCardInfo(this.CardNO.Text);
                if (Cardinfo == null)
                {
                    MessageBox.Show("��ѯ����");
                    return;
                }
                if (Cardinfo.CardID == null || Cardinfo.CardID == "")
                {
                    MessageBox.Show("û�в鵽�ÿ��ŵ������Ϣ");
                    return;
                }
                CardNO.Text = Cardinfo.CardID;
                comPerson.Text = Cardinfo.EmployeeInfo.Name;
                comPerson.Tag = Cardinfo.EmployeeInfo.ID;
                //				this.txDays.Focus();
                this.comType.Focus();
            }
        }

        private int ValidState(Neusoft.HISFC.Object.HealthRecord.Lend obj)
        {
            if (CardNO.Text == null && CardNO.Text == "")
            {
                MessageBox.Show("��������Ŀ���");
                return -1;
            }
            if (caseNo.Text == null && caseNo.Text == "")
            {
                MessageBox.Show("��������Ŀ���");
                return -1;
            }
            if (comType.Text == "")
            {
                MessageBox.Show("���ķ�ʽ");
                return -1;
            }
            if(this.txReturnTime.Value <= System.DateTime.Now)
            {
                MessageBox.Show("Ԥ�ƹ黹���ڲ���С�ڵ�ǰʱ��");
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ������� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbLend_Click(object sender, System.EventArgs e)
        {
            //�����ж��Ƿ��Ѿ�����ˣ���������û�й黹���������
            //������� 
            Neusoft.HISFC.Object.HealthRecord.Lend obj = GetLendInfo();
            if (obj == null)
            {
                return;
            }
            if (ValidState(obj) == -1)
            {
                return;
            }
            Neusoft.NFC.Management.Transaction trans = new Neusoft.NFC.Management.Transaction(card.Connection);
            trans.BeginTransaction();
            card.SetTrans(trans.Trans);
            if (card.LendCase(obj) < 1)
            {
                trans.RollBack();
                MessageBox.Show("������ļ�¼ʧ��: " + card.Err);
                return;
            }
            if (card.UpdateBase(LendType.O, obj.CaseBase.CaseNO) <= 0)
            {
                trans.RollBack();
                MessageBox.Show("���²�������ʧ��");
                return;
            }
            trans.Commit();
            MessageBox.Show("�����ɹ�");
            this.ClearCase();
            this.ClearPerson();
            this.caseNo.Focus();
        }
        private Neusoft.HISFC.Object.HealthRecord.Lend GetLendInfo()
        {
            if (PatientCaseInfo == null)
            {
                MessageBox.Show("��ѡ����ĵĲ�����Ϣ");
                return null;
            }
            if (Cardinfo == null)
            {
                MessageBox.Show("��ѡ����ĵĲ�����Ϣ");
                return null;
            } 
            Neusoft.HISFC.Object.HealthRecord.Lend Saveinfo = new Neusoft.HISFC.Object.HealthRecord.Lend();
            Saveinfo.SeqNO = this.card.GetSequence("Case.CaseCard.LendCase.Seq");
            if (Saveinfo.SeqNO == null || Saveinfo.SeqNO == "")
            {
                MessageBox.Show("��ȡ���ʧ��");
                return null;
            }
            Saveinfo.CaseBase.CaseNO = PatientCaseInfo.CaseNO;
            Saveinfo.CaseBase.PatientInfo.ID = PatientCaseInfo.PatientInfo.ID;//סԺ��ˮ��
            Saveinfo.CaseBase.CaseNO = PatientCaseInfo.CaseNO;//����סԺ�� 
            Saveinfo.CaseBase.PatientInfo.Name = PatientCaseInfo.PatientInfo.Name; //��������
            Saveinfo.CaseBase.PatientInfo.Sex.ID = PatientCaseInfo.PatientInfo.Sex.ID;//�Ա�
            Saveinfo.CaseBase.PatientInfo.Birthday = PatientCaseInfo.PatientInfo.Birthday;//��������
            Saveinfo.CaseBase.PatientInfo.PVisit.InTime = PatientCaseInfo.PatientInfo.PVisit.InTime;//��Ժ����
            Saveinfo.CaseBase.PatientInfo.PVisit.OutTime = PatientCaseInfo.PatientInfo.PVisit.OutTime;//��Ժ����
            Saveinfo.CaseBase.InDept.ID = PatientCaseInfo.InDept.ID; //��Ժ���Ҵ���
            Saveinfo.CaseBase.InDept.Name = PatientCaseInfo.InDept.Name; //��Ժ��������
            Saveinfo.CaseBase.OutDept.ID = PatientCaseInfo.OutDept.ID;  //��Ժ���Ҵ���
            Saveinfo.CaseBase.OutDept.Name = PatientCaseInfo.OutDept.Name; //��Ժ��������
            Saveinfo.EmployeeInfo.ID = Cardinfo.EmployeeInfo.ID;//�����˴���
            Saveinfo.EmployeeInfo.Name = Cardinfo.EmployeeInfo.Name;//����������
            Saveinfo.EmployeeDept.ID = Cardinfo.DeptInfo.ID; //���������ڿ��Ҵ���
            Saveinfo.EmployeeDept.Name = Cardinfo.DeptInfo.Name; //���������ڿ�������
            Saveinfo.LendDate = Neusoft.NFC.Function.NConvert.ToDateTime(baseDml.GetSysDate()); //��������
            Saveinfo.PrerDate = txReturnTime.Value; //Ԥ������
            if (this.comType.Text == "�ڽ�")
            {
                Saveinfo.LendKind = "1"; ; //��������
            }
            else if (this.comType.Text == "���")
            {
                Saveinfo.LendKind = "2"; ; //��������
            }
            Saveinfo.LendStus = "1"; ;//����״̬ 1���/2����
            Saveinfo.ID = baseDml.Operator.ID; //����Ա����
            Saveinfo.OperInfo.OperTime = Neusoft.NFC.Function.NConvert.ToDateTime(baseDml.GetSysDate()); //����ʱ��
            Saveinfo.ReturnOperInfo.ID = "";   //�黹����Ա����
            Saveinfo.ReturnDate = Neusoft.NFC.Function.NConvert.ToDateTime("3000-1-1");   //ʵ�ʹ黹����
            Saveinfo.CardNO = CardNO.Text;//����
            return Saveinfo;
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                return;
            }
            PatientCaseInfo = (Neusoft.HISFC.Object.HealthRecord.Base)Caselist[this.fpSpread1_Sheet1.ActiveRowIndex];
            SetInfo(PatientCaseInfo);
        }

        private void comType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txReturnTime.Focus();
            }
        }

        private void txReturnTime_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                tbLend_Click(sender, e);
            }
        }
    }
}