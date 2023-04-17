using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace FS.HISFC.Components.Account.Controls
{
    public partial class ucAccountManagerQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAccountManagerQuery()
        {
            InitializeComponent();
        }
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        private void btQuery_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Text.Trim();
            string idenNo = this.txtIDNO.Text.Trim();
            string homeStr = this.txthome.Text.Trim();
            DateTime birthday = this.dtpbirthday.Value.Date;
            if (name == string.Empty && idenNo == string.Empty && birthday == this.dtpbirthday.MinDate && homeStr == string.Empty)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������Ҫ��ѯ��������"), FS.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg("���ڼ������ݣ����Ժ�^^��"));
                Application.DoEvents();
                //��û�����������
                //List<FS.HISFC.Models.RADT.PatientInfo> list = accountManager.GetPatientInfo(name, idenNo, birthday.ToString(), homeStr);
                List<FS.HISFC.Models.RADT.PatientInfo> list = null;
                int count = this.neuSpread1_Sheet1.Rows.Count;
                if (count > 0)
                {
                    this.neuSpread1_Sheet1.Rows.Remove(0, count);
                }
                if (list == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѯ����ʧ�ܣ�"), FS.FrameWork.Management.Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (FS.HISFC.Models.RADT.PatientInfo patient in list)
                {
                    count = this.neuSpread1_Sheet1.Rows.Count;
                    this.neuSpread1_Sheet1.Rows.Add(count, 1);
                    this.neuSpread1_Sheet1.Cells[count , 0].Text = patient.PID.CardNO;
                    this.neuSpread1_Sheet1.Cells[count , 1].Text = patient.Name;
                    this.neuSpread1_Sheet1.Cells[count , 2].Text = patient.Birthday.ToString();
                    this.neuSpread1_Sheet1.Cells[count , 3].Text = patient.Sex.Name;
                    this.neuSpread1_Sheet1.Cells[count , 4].Text = patient.IDCard;
                    this.neuSpread1_Sheet1.Cells[count, 5].Text = patient.AddressHome;
                    this.neuSpread1_Sheet1.Rows[count].Tag = patient;
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0) return;
            string cardNo = (this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.RADT.PatientInfo).PID.CardNO;
            List < FS.HISFC.Models.Account.AccountCard > list = accountManager.GetMarkList(cardNo);
            if (list == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѯ����ʧ�ܣ�"), FS.FrameWork.Management.Language.Msg("����"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int count=this.neuSpread2_Sheet1.Rows.Count;
            if (count > 0)
            {
                this.neuSpread2_Sheet1.Rows.Remove(0, count);
            }
            foreach (FS.HISFC.Models.Account.AccountCard accountcard in list)
            { 
                count=this.neuSpread2_Sheet1.Rows.Count;
                neuSpread2_Sheet1.Rows.Add(count, 1);
                
                this.neuSpread2_Sheet1.Cells[count, 0].Text = accountcard.MarkType.Name;
                this.neuSpread2_Sheet1.Cells[count, 1].Text = accountcard.MarkNO;
                if(accountcard.IsValid)
                    this.neuSpread2_Sheet1.Cells[count, 2].Text = "����";
                else
                    this.neuSpread2_Sheet1.Cells[count, 2].Text = "ͣ��";
            }
        }

        private void ucAccountManagerQuery_Load(object sender, EventArgs e)
        {
            this.dtpbirthday.Value = this.dtpbirthday.MinDate;
            
        }
    }
}
