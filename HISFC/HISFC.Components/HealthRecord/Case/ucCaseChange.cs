using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Case
{
    /// <summary>
    /// ������������
    /// </summary>
    public partial class ucCaseChange : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.Models.Registration.Register register;

        public ucCaseChange()
        {
            InitializeComponent();
        }

        public ucCaseChange(FS.HISFC.Models.Registration.Register register)
        {
            InitializeComponent();
            this.register = register;
        }

        /// <summary>
        /// �ҺŻ���
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.register;
            }
            set
            {
                this.register = value;
            }
        }

        private void Clear()
        {
            this.register = null;
            this.tbOldNumber.Text = "";
            this.tbNewNumber.Text = "";
            this.tbMemo.Text = "";
        }

        private void ucCaseChange_Load(object sender, EventArgs e)
        {
            this.tbOldNumber.Text = this.register.PID.CaseNO;
            this.tbNewNumber.Text = this.register.PID.CaseNO;//�²�����
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.HealthRecord.Case.CaseChangeManager changManager = new FS.HISFC.BizLogic.HealthRecord.Case.CaseChangeManager();

            FS.HISFC.Models.HealthRecord.Case.CaseChange change = new FS.HISFC.Models.HealthRecord.Case.CaseChange();

            change.ID = changManager.GetChangeID();

            change.OldCardNO = this.register.PID.CaseNO;//�ɲ�����
            change.NewCardNO = this.register.PID.CaseNO;//�²�����

            change.DoctorEnv.ID = ((FS.HISFC.Models.Base.Employee)changManager.Operator).ID;
            change.DoctorEnv.OperTime = changManager.GetDateTimeFromSysDateTime();
            change.Memo = this.tbMemo.Text;

            if (changManager.IsApplyExist(change.OldCardNO))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ò����ŵĲ������������Ѿ�����"));
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(changManager.Connection);
            //trans.BeginTransaction();

            changManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (changManager.InsertApply(change) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("������������ʧ��"));
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ò����ŵĲ������������Ѿ�����"));
            this.Clear();
            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}