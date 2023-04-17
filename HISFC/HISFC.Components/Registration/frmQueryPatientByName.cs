using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// ��ѯ�Ļ�����Ϣ����һ��ѡ����UC
    /// </summary>
    public partial class frmQueryPatientByName : Form
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public frmQueryPatientByName()
        {
            InitializeComponent();

            this.fpSpread1.KeyDown += new KeyEventHandler(fpSpread1_KeyDown);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
        }

        #region ����
        /// <summary>
        /// ���תҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��Աҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
        /// <summary>
        /// ��ѡ����������Ϣ
        /// </summary>
        public delegate void GetPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo);

        private FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// ��ѡ����������Ϣ�󴥷�
        /// </summary>
        public event GetPatient SelectedPatient;

        /// <summary>
        /// ������Ϣʵ��
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// �������֤��
        /// </summary>
        string idenNO = string.Empty;

        /// <summary>
        /// ���������Ļ�����Ϣ��Ŀ
        /// </summary>
        private int personCount;
        #endregion

        #region ����
        /// <summary>
        /// �������֤��
        /// </summary>
        public string IdenNO
        {
            get
            {
                return this.idenNO;
            }
            set
            {
                try
                {
                    this.idenNO = value;
                    if (this.idenNO == string.Empty || this.idenNO == null)
                    {
                        return;
                    }
                    //����idenNO ��÷��������Ļ�����Ϣ
                    FillPatientInfoByIdenNO();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + this.Name.ToString());
                }
            }
        }

        /// <summary>
        /// ���������Ļ�����Ϣ��Ŀ
        /// </summary>
        public int PersonCount
        {
            get
            {
                return this.personCount;
            }
        }

        /// <summary>
        /// ѡ���Ļ�����Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                this.patientInfo = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �����������Ļ�����Ϣ��ʾ���б��У����ֻ��һ��������ʾ�ÿؼ���ֱ�Ӹ�ֵ������Ϣʵ��
        /// </summary>
        private void FillPatientInfoByIdenNO()
        {
            ArrayList patients = this.radtIntegrate.QueryComPatientInfoListByIDNO(this.idenNO);

            DisplayPatients(patients);
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// <param name="patients">��ѯ�����Ļ����б�</param>
        private void DisplayPatients(ArrayList patients)
        {
            //��û��߻�����Ϣ����
            if (patients == null)
            {
                this.personCount = 0;
                this.patientInfo = null;
                this.SelectedPatient(null);

                return;
            }
            //û���ҵ����������Ļ�����Ϣ
            if (patients.Count == 0)
            {
                this.personCount = 0;
                this.patientInfo = null;
                this.SelectedPatient(null);

                return;
            }
            //���ֻ�ҵ�һ�����������Ļ�����Ϣ����ô����ʾ�ؼ���ֱ�ӷ��ػ��ߵĹҺ�ʵ��
            if (patients.Count == 1)
            {
                this.personCount = 1;
                this.patientInfo = patients[0] as FS.HISFC.Models.RADT.PatientInfo;
                this.SelectedPatient(this.patientInfo);

                return;
            }
            //����ж�����������Ļ�����Ϣ���ڿؼ����б�����ʾ������Ϣ��������Ϣʵ���ڸ��е�tag������
            this.fpSpread1_Sheet1.RowCount = 1; //Ĭ��ֻ��һ��
            this.personCount = patients.Count;
            this.fpSpread1_Sheet1.RowCount = personCount;
            FS.HISFC.Models.RADT.PatientInfo patient = null;

            int index = 0;
            for (int i = personCount - 1; i >= 0; i--)
            {
                patient = patients[i] as FS.HISFC.Models.RADT.PatientInfo;


                this.fpSpread1_Sheet1.Cells[index, 0].Text = patient.PID.CardNO;
                this.fpSpread1_Sheet1.Cells[index, 1].Text = patient.Name;
                this.fpSpread1_Sheet1.Cells[index, 2].Text = patient.Sex.Name;
                this.fpSpread1_Sheet1.Cells[index, 3].Text = patient.Birthday.ToShortDateString();
                this.fpSpread1_Sheet1.Cells[index, 4].Text = personManager.GetAge(patient.Birthday);
                this.fpSpread1_Sheet1.Cells[index, 5].Text = patient.IDCard;
                this.fpSpread1_Sheet1.Cells[index, 6].Text = patient.Pact.Name;
                this.fpSpread1_Sheet1.Cells[index, 7].Text = patient.PhoneHome;
                this.fpSpread1_Sheet1.Cells[index, 8].Text = patient.Kin.RelationPhone;
                this.fpSpread1_Sheet1.Cells[index, 9].Text = patient.AddressHome;

                this.fpSpread1_Sheet1.Rows[index].Tag = patient;
                this.fpSpread1_Sheet1.Rows[index].Height = 30F;
                this.fpSpread1_Sheet1.Rows[index].Font = new System.Drawing.Font("����", 14F);

                index++;
            }
            this.ShowDialog();
        }

        /// <summary>
        /// ������������ѯ���߹Һ���Ϣ
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public int QueryByName(string Name)
        {
            ArrayList al = radtIntegrate.QueryPatientByName(Name);

            if (al == null)
            {
                MessageBox.Show("��ѯ���߹Һ���Ϣʱ����!" + radtIntegrate.Err, "��ʾ");
                return -1;
            }

            return query(al);
        }

        /// <summary>
        /// ������ҽ��֤�Ų�ѯ���߹Һ���Ϣ
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public int QueryByMCardNO(string mCardNO)
        {
            ArrayList al = new ArrayList();

            FS.HISFC.Models.RADT.PatientInfo p = radtIntegrate.QueryComPatientInfoByMcardNO(mCardNO);

            if (p == null)
            {
                MessageBox.Show("��ѯ���߹Һ���Ϣʱ����!" + radtIntegrate.Err, "��ʾ");
                return -1;
            }

            if (string.IsNullOrEmpty(p.PID.CardNO) == false)
            {
                al.Add(p);
            }

            return query(al);
        }

        private int query(ArrayList al)
        {
            string deptid = ((accountMgr.Operator) as FS.HISFC.Models.Base.Employee).Dept.ID;

            bool isyk = false;

            ArrayList ykal = managerIntegrate.GetConstantList("YkDept");

            if (ykal != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in ykal)
                {
                    if (deptid == obj.ID)
                    {
                        isyk = true;
                        break;
                    }
                }
            }


            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            foreach (FS.HISFC.Models.RADT.PatientInfo obj in al)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

                int row = this.fpSpread1_Sheet1.RowCount - 1;
                if (isyk)
                {
                    List<FS.HISFC.Models.Account.AccountCard> list = accountMgr.GetMarkList(obj.PID.CardNO, "2", "1");
                    string markNo = string.Empty;
                    if (list.Count == 0)
                    {
                        //markNo = register.PID.CardNO;
                        this.fpSpread1_Sheet1.SetValue(row, 0, obj.PID.CardNO, false);
                    }
                    else
                    {
                        //markNo = list[0].MarkNO;
                        this.fpSpread1_Sheet1.SetValue(row, 0, list[0].MarkNO, false);
                    }
                }
                else
                {
                    this.fpSpread1_Sheet1.SetValue(row, 0, obj.PID.CardNO, false);
                }
                // this.fpSpread1_Sheet1.SetValue(row, 0, obj.PID.CardNO, false);
                this.fpSpread1_Sheet1.Cells[row, 1].Text = obj.Name;
                this.fpSpread1_Sheet1.Cells[row, 2].Text = obj.Sex.Name;
                this.fpSpread1_Sheet1.Cells[row, 3].Text = obj.Birthday.ToShortDateString();
                this.fpSpread1_Sheet1.Cells[row, 4].Text = personManager.GetAge(obj.Birthday);
                this.fpSpread1_Sheet1.Cells[row, 5].Text = obj.IDCard;
                this.fpSpread1_Sheet1.Cells[row, 6].Text = obj.Pact.Name;
                this.fpSpread1_Sheet1.Cells[row, 7].Text = obj.PhoneHome;
                this.fpSpread1_Sheet1.Cells[row, 8].Text = obj.Kin.RelationPhone;
                this.fpSpread1_Sheet1.Cells[row, 9].Text = obj.AddressHome;
            }

            return al.Count;
        }
        /// <summary>
        /// ��ȡѡ��Ļ��߲�����
        /// </summary>
        public string SelectedCardNo
        {
            get
            {
                if (this.fpSpread1_Sheet1.RowCount == 0) return "";

                int Row = this.fpSpread1_Sheet1.ActiveRowIndex;

                return this.fpSpread1_Sheet1.GetText(Row, 0);
            }
        }
        #endregion

        #region �¼�
        private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpSpread1_Sheet1.Rows.Count > 0)
                {
                    if (this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.ActiveRowIndex].Tag != null)
                    {
                        this.SelectPatient(this.fpSpread1_Sheet1.ActiveRowIndex);
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// ˫��FP�¼�,ѡ��ǰ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || this.fpSpread1_Sheet1.RowCount == 0) return;
            int row = e.Row;
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                if (this.fpSpread1_Sheet1.Rows[row].Tag != null)
                {
                    this.SelectPatient(row);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        /// <summary>
        /// ˫�����س���ѡ����
        /// </summary>
        /// <param name="row">��ǰ��</param>
        private void SelectPatient(int row)
        {
            if (this.SelectedPatient != null)
            {
                this.SelectedPatient((FS.HISFC.Models.RADT.PatientInfo)this.fpSpread1_Sheet1.Rows[row].Tag);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                if (this.SelectedPatient != null)
                {
                    this.SelectedPatient(null);
                }
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ���ؼ���ý����ʱ��,��FP��ý���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmQueryPatientByName_Enter(object sender, EventArgs e)
        {
            this.fpSpread1.Focus();
        }

        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.fpSpread1_Sheet1.RowCount == 0
                || this.fpSpread1_Sheet1.SelectionCount == 0)
            {
                return;
            }
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                if (this.fpSpread1_Sheet1.Rows[row].Tag != null)
                {
                    this.SelectPatient(row);
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// �˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}