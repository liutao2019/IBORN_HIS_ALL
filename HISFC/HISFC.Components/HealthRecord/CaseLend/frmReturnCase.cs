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
    public partial class frmReturnCase : Form
    {
        public frmReturnCase()
        {
            InitializeComponent();
        }
        #region  ȫ�ֱ���
        private System.Data.DataTable dt = null;
        private Neusoft.HISFC.Management.HealthRecord.CaseCard card = new Neusoft.HISFC.Management.HealthRecord.CaseCard();
        private ArrayList LendList = null;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.HealthRecord.Lend lendInfo = null;
        #endregion
        private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dtReturn.Enabled = this.checkBox2.Checked;
            if (!checkBox2.Checked)
            {
                checkBox1.Checked = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dtContinue.Enabled = this.checkBox1.Checked;
            if (!checkBox1.Checked)
            {
                checkBox2.Checked = true;
            }
        }

        private void frmReturnCase_Load(object sender, System.EventArgs e)
        {
            //InitDateTable();
        }

        private void CardNO_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.CardNO.Text == "")
                {
                    MessageBox.Show("�����뿨��");
                    return;
                }
                LendList = null;
                LendList = this.card.QueryLendInfo(this.CardNO.Text);
                if (LendList == null)
                {
                    MessageBox.Show("��ѯ������Ϣ����");
                    return;
                }
                if (LendList.Count == 0)
                {
                    MessageBox.Show("û�в鵽��δ�黹�Ľ�����Ϣ");
                    return;
                }

                AddDateInfo(LendList);
                this.checkBox1.Focus();
            }
        }
        enum Cols
        {
            Check,
            CaseNO,
            patientName,
            SexID,
            InDeptName,
            OutDeptName,
            InTime,
            OutTime,
            Birthday,
            InTimes
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="list"></param>
        private void AddDateInfo(ArrayList list)
        {
            this.fpSpread1_Sheet1.RowCount = 0;
            foreach (Neusoft.HISFC.Object.HealthRecord.Lend info in list)
            {
                this.fpSpread1_Sheet1.Rows.Add(0, 1);
                string Sex = "";
                if (info.CaseBase.PatientInfo.Sex.ID != null)
                {
                    if (info.CaseBase.PatientInfo.Sex.ID.ToString() == "M")
                    {
                        Sex = "��";
                    }
                    else if (info.CaseBase.PatientInfo.Sex.ID.ToString() == "F")
                    {
                        Sex = "Ů";
                    }
                }
                this.fpSpread1_Sheet1.Columns[0, (int)Cols.Check].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.fpSpread1_Sheet1.Cells[0, (int)Cols.Check].Value = true;
                this.fpSpread1_Sheet1.Cells[0, (int)Cols.CaseNO].Text = info.CaseBase.CaseNO;
                this.fpSpread1_Sheet1.Cells[0, (int)Cols.patientName].Text = info.CaseBase.PatientInfo.Name;
                this.fpSpread1_Sheet1.Cells[0, (int)Cols.SexID].Text = Sex;
                this.fpSpread1_Sheet1.Cells[0, (int)Cols.InDeptName].Text = info.CaseBase.InDept.Name;
                this.fpSpread1_Sheet1.Cells[0, (int)Cols.OutDeptName].Text = info.CaseBase.OutDept.Name;
                this.fpSpread1_Sheet1.Cells[0, (int)Cols.InTime].Text = info.CaseBase.PatientInfo.PVisit.InTime.ToShortDateString();
                this.fpSpread1_Sheet1.Cells[0, (int)Cols.OutTime].Text = info.CaseBase.PatientInfo.PVisit.OutTime.ToShortDateString();
                this.fpSpread1_Sheet1.Cells[0, (int)Cols.Birthday].Text = info.CaseBase.PatientInfo.Birthday.ToShortDateString();
                this.fpSpread1_Sheet1.Cells[0, (int)Cols.InTimes].Text = info.CaseBase.PatientInfo.InTimes.ToString();
                this.fpSpread1_Sheet1.Rows[0].Tag = info;
                this.SetInfo(info);
            }
        }
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        /// <param name="obj"></param>
        private void SetInfo(Neusoft.HISFC.Object.HealthRecord.Lend obj)
        {
            CardNO.Text = obj.CardNO;
            txName.Text = obj.EmployeeInfo.Name;
            txDept.Text = obj.EmployeeDept.Name;
            txLendTime.Text = obj.LendDate.ToShortDateString();
            txLEndtype.Text = obj.LendKind;
            txcaseNo.Text = obj.CaseBase.CaseNO;
            txPatientName.Text = obj.CaseBase.PatientInfo.Name;
            txBirthday.Text = obj.CaseBase.PatientInfo.Birthday.ToShortDateString();
            dtContinue.Value = Neusoft.NFC.Function.NConvert.ToDateTime(card.GetSysDate());
            dtReturn.Value = Neusoft.NFC.Function.NConvert.ToDateTime(card.GetSysDate());
            txReturn.Text = obj.Memo;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOk_Click(object sender, System.EventArgs e)
        {
            if (dtContinue.Value <= System.DateTime.Now)
            {
                System.TimeSpan sp = System.DateTime.Now - dtContinue.Value;
                if (sp.Days > 0)
                {
                    MessageBox.Show("�������ڲ���С�ڵ�ǰ����");
                    return;
                }
            }
            if (!Neusoft.NFC.Public.String.ValidMaxLengh(txReturn.Text, 100))
            {
                MessageBox.Show("�黹�������");
                return;
            }
            ArrayList list = GetInfo(); //��ȡ��Ϣ
            if (list == null)
            {
                MessageBox.Show("��ȡ��Ϣʧ��");
                return;
            }
            Neusoft.NFC.Management.Transaction trans = new Neusoft.NFC.Management.Transaction(card.Connection);
            trans.BeginTransaction();
            card.SetTrans(trans.Trans);
            foreach (Neusoft.HISFC.Object.HealthRecord.Lend obj in list)
            {
                if (card.ReturnCase(lendInfo) < 1)
                {
                    trans.RollBack();
                    MessageBox.Show("����ʧ��: " + card.Err);
                    return;
                }
                if (!checkBox1.Checked) //�黹
                {
                    if (card.UpdateBase(LendType.I, lendInfo.CaseBase.CaseNO) <= 0)
                    {
                        trans.RollBack();
                        MessageBox.Show("���²�������ʧ��");
                        return;
                    }
                }
            }
            trans.Commit();
            MessageBox.Show("�����ɹ�");
        }
        private ArrayList GetInfo()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.fpSpread1_Sheet1.Cells[i, (int)Cols.Check].Value == null || fpSpread1_Sheet1.Cells[i, (int)Cols.Check].Value.ToString().ToUpper() != "TRUE")
                {
                    continue;
                }
                Neusoft.HISFC.Object.HealthRecord.Lend obj = (Neusoft.HISFC.Object.HealthRecord.Lend)fpSpread1_Sheet1.Rows[i].Tag;
                if (obj == null)
                {
                    MessageBox.Show("��ȡ������Ϣ����");
                    return null;
                }
                if (checkBox1.Checked) //����
                {
                    lendInfo.PrerDate = dtContinue.Value;
                }
                else if (checkBox2.Checked) //����
                {
                    obj.ReturnDate = dtReturn.Value; //ʵ�ʷ���ʱ�� 
                    obj.ReturnOperInfo.ID = card.Operator.ID;  //��������Ա
                    obj.LendStus = "2"; //״̬ 
                    obj.Memo = this.txReturn.Text; //�Ƿ���� 
                }
                list.Add(obj);
            }
            return list;
        }

        private void checkBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == e.KeyData)
            {
                if (this.checkBox1.Checked)
                {
                    this.dtContinue.Focus();
                }
                else
                {
                    this.dtReturn.Focus();
                }
            }
        }

        private void checkBox2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == e.KeyData)
            {
                if (this.checkBox2.Checked)
                {
                    this.dtReturn.Focus();
                }
                else
                {
                    this.dtContinue.Focus();
                }
            }
        }

        private void dtContinue_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txReturn.Focus();
            }
        }

        private void dtReturn_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.txReturn.Focus();
            }
        }

        private void txReturn_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btOk_Click(sender, e);
            }
        }
    }
}