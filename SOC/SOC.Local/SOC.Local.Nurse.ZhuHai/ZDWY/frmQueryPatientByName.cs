using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Nurse.ZhuHai.ZDWY
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

        /// <summary>
        /// ��ǰѡ����������Ϣ
        /// </summary>
        public delegate void GetPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo);

        /// <summary>
        /// ��ǰѡ����������Ϣ�󴥷�
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
                this.fpSpread1_Sheet1.Cells[index, 3].Text = patient.PhoneHome;
                this.fpSpread1_Sheet1.Cells[index, 4].Text = patient.Kin.RelationPhone;
                this.fpSpread1_Sheet1.Cells[index, 5].Text = patient.AddressHome;

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
            FS.HISFC.BizProcess.Integrate.RADT Mgr = new FS.HISFC.BizProcess.Integrate.RADT();
            ArrayList al = Mgr.QueryPatientByName(Name);

            if (al == null)
            {
                MessageBox.Show("��ѯ���߹Һ���Ϣʱ����!" + Mgr.Err, "��ʾ");
                return -1;
            }

            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            foreach (FS.HISFC.Models.RADT.PatientInfo obj in al)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

                int row = this.fpSpread1_Sheet1.RowCount - 1;
                this.fpSpread1_Sheet1.SetValue(row, 0, obj.PID.CardNO, false);
                this.fpSpread1_Sheet1.SetValue(row, 1, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, 2, obj.Sex.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, 3, obj.PhoneHome, false);
                this.fpSpread1_Sheet1.SetValue(row, 4, obj.Kin.RelationPhone, false);
                this.fpSpread1_Sheet1.SetValue(row, 5, obj.AddressHome, false);
            }

            return al.Count;
        }

        /// <summary>
        /// ���ݻ�����������ʼ���ڡ��������ڲ�ѯ
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int QueryByNameAndDate(string name, DateTime start, DateTime end, string function)
        {
            Common com = new Common();
            ArrayList al = null;
            if ("UL".Equals(function))
            {
                al = com.QueryPatientWithULItemByNameAndDate(name, start, end);
            }
            else if ("Inject".Equals(function))
            {
                al = com.QueryPatientWithInjectItemByNameAndDate(name, start, end);
            }
            if (al == null)
            {
                MessageBox.Show("��ѯ������Ϣʱ����", "��ʾ");
                return -1;
            }
            this.fpSpread1_Sheet1.RowCount = 0;//�������
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in al)
            {
                int curRow = this.fpSpread1_Sheet1.RowCount++;
                this.fpSpread1_Sheet1.Rows[curRow].Tag = patient;
                this.fpSpread1_Sheet1.Cells[curRow, 0].Text = patient.PID.CardNO;
                this.fpSpread1_Sheet1.Cells[curRow, 1].Text = patient.Name;
                this.fpSpread1_Sheet1.Cells[curRow, 2].Text = patient.Sex.Name;
                this.fpSpread1_Sheet1.Cells[curRow, 3].Text = patient.PhoneHome;
                this.fpSpread1_Sheet1.Cells[curRow, 4].Text = patient.AddressHome;
                this.fpSpread1_Sheet1.Cells[curRow, 5].Text = patient.Kin.Name;
                this.fpSpread1_Sheet1.Cells[curRow, 6].Text = patient.Kin.RelationPhone;
                this.fpSpread1_Sheet1.Rows[curRow].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.fpSpread1_Sheet1.Rows[curRow].Locked = true;
                if ("0".Equals(patient.Memo))
                {
                    this.fpSpread1_Sheet1.Rows[curRow].BackColor = Color.LightGray;
                }
            }
            return al.Count;
        }

        /// <summary>
        /// ���ݻ��߿��š���ʼ���ڡ��������ڲ�ѯ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int QueryByCardNoAndDate(string cardNo, DateTime start, DateTime end, string function)
        {
            Common com = new Common();
            ArrayList al = null;
            if ("UL".Equals(function))
            {
                al = com.QueryPatientWithULItemByCardNoAndDate(cardNo, start, end);
            }
            else if ("Inject".Equals(function))
            {
                //al = com.QueryPatientWithInjectItemByClinicNoAndDate(cardNo, start, end);
                al = null;
            }
            if (al == null)
            {
                MessageBox.Show("��ѯ������Ϣʱ����", "��ʾ");
                return -1;
            }
            this.fpSpread1_Sheet1.RowCount = 0;//�������
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in al)
            {
                int curRow = this.fpSpread1_Sheet1.RowCount++;
                this.fpSpread1_Sheet1.Rows[curRow].Tag = patient;
                this.fpSpread1_Sheet1.Cells[curRow, 0].Text = patient.PID.CardNO;
                this.fpSpread1_Sheet1.Cells[curRow, 1].Text = patient.Name;
                this.fpSpread1_Sheet1.Cells[curRow, 2].Text = patient.Sex.Name;
                this.fpSpread1_Sheet1.Cells[curRow, 3].Text = patient.PhoneHome;
                this.fpSpread1_Sheet1.Cells[curRow, 4].Text = patient.AddressHome;
                this.fpSpread1_Sheet1.Cells[curRow, 5].Text = patient.Kin.Name;
                this.fpSpread1_Sheet1.Cells[curRow, 6].Text = patient.Kin.RelationPhone;
                this.fpSpread1_Sheet1.Rows[curRow].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.fpSpread1_Sheet1.Rows[curRow].Locked = true;
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