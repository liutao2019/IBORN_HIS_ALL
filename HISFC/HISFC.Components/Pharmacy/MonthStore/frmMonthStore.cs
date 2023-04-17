using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.MonthStore
{
    public partial class frmMonthStore : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmMonthStore()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ������
        /// </summary>
        FS.HISFC.BizLogic.Manager.Job jobManager = new FS.HISFC.BizLogic.Manager.Job();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҩƷ�½�����
        /// </summary>
        FS.HISFC.Models.Base.Job job = new FS.HISFC.Models.Base.Job();

        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        DateTime sysTime = System.DateTime.MinValue;

        /// <summary>
        /// ҩƷ�½�Ȩ��
        /// </summary>
        bool isMonthStorePriv = false;

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// Ȩ����Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject privOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���幦��
        /// </summary>
        private FS.HISFC.Models.IMA.EnumModuelType winFun = FS.HISFC.Models.IMA.EnumModuelType.Phamacy;

        /// <summary>
        /// �½�����ַ���
        /// </summary>
        private string monthStoreType = "PHA_MS";

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾ�����²���ť
        /// </summary>
        [Description("�Ƿ���ʾ�����²���ť"),Category("����"),DefaultValue(false)]
        public bool IsShowButton
        {
            get
            {
                return this.penelButon.Visible;
            }
            set
            {
                this.penelButon.Visible = value;
            }
        }

        /// <summary>
        /// ���幦��
        /// </summary>
        [Description("���幦�� ��������ΪAll ��ͬ����ΪPharmacy"), Category("����"), DefaultValue(FS.HISFC.Models.IMA.EnumModuelType.Phamacy)]
        public FS.HISFC.Models.IMA.EnumModuelType WinFun
        {
            get
            {
                return this.winFun;
            }
            set
            {
                this.winFun = value;

                switch (value)
                {
                    case FS.HISFC.Models.IMA.EnumModuelType.Phamacy:           //ҩƷ
                        this.monthStoreType = "PHA_MS";
                        break;
                    case FS.HISFC.Models.IMA.EnumModuelType.Material:          //����
                        this.monthStoreType = "MAT_MS";
                        break;
                    case FS.HISFC.Models.IMA.EnumModuelType.Equipment:         //�豸
                        this.monthStoreType = "EQU_MS";
                        break;
                    default:
                        this.winFun = FS.HISFC.Models.IMA.EnumModuelType.Phamacy;
                        this.monthStoreType = "PHA_MS";
                        break;
                }
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.privOper = this.jobManager.Operator;
            this.job = this.jobManager.GetJob(this.monthStoreType);
            if (this.job == null)
            {
                MessageBox.Show(Language.Msg("����ҩƷ�½�����ȡҩƷ�½�����ʧ��"));
                return;
            }
            if (this.job.ID != "")
            {
                if (this.job.Type == "0")
                    this.cmbType.Text = "�ֶ�";
                else
                    this.cmbType.Text = "�Զ�";               

                this.dtpLast.Enabled = false;       //�ϴ��½����ʱ�䲻���޸�
            }
            else
            {
                this.job.ID = this.monthStoreType;
                this.job.Name = "ȫԺ�½�";
                this.job.State.ID = "M";
                this.job.LastTime = this.sysTime.AddMonths(-1);
                this.job.NextTime = this.sysTime;
                this.job.Type = "0";
                this.job.IntervalDays = 30;
                this.job.Department.ID = "0";

                this.cmbType.Text = "�Զ�";

                this.jobManager.SetJob(this.job);
            }

            this.dtpLast.Value = this.job.LastTime;
            this.dtpNext.Value = this.job.NextTime;
        }

        /// <summary>
        /// �½�ʱ������
        /// </summary>
        /// <returns></returns>
        private bool JudgeMonthStoreTime()
        {
            DialogResult rs = MessageBox.Show(Language.Msg("�ϴ��½�ʱ��Ϊ" + this.job.LastTime.ToString() + "\n ȷ�����ڽ����½���?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.No)
                return false;

            rs = MessageBox.Show(Language.Msg("�Ƿ�����½����ʱ������ ѡ��'��' �����½��ֹʱ�� ѡ��'��' �����½����ʱ��Ϊ��ǰ����"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (rs == DialogResult.Yes)		//�����½�ʱ��
            {
                ucMonthStoreSet uc = new ucMonthStoreSet();
                uc.SetJob(this.job.Clone(), this.sysTime.AddSeconds(-1));

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                if (uc.Result == DialogResult.Cancel)
                    return false;

                this.job.NextTime = uc.NextTime;
            }
            else
            {
                //��һ�� ��֤�洢�������ж�ʱ���Ƿ��ܹ��½�ʱ ��������ִ��
                this.job.NextTime = this.sysTime.AddSeconds(-1);
            }

            if (this.jobManager.SetJob(this.job) != 1)
            {
                MessageBox.Show(Language.Msg("���ݵ�ǰʱ�������½�ʱ�� ��������"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual int Save()
        {
            switch (this.winFun)
            {
                case FS.HISFC.Models.IMA.EnumModuelType.Phamacy:           //ҩƷ
                case FS.HISFC.Models.IMA.EnumModuelType.All:
                    if (this.isMonthStorePriv)
                    {
                        //{3E00895B-09AD-47c5-AFCF-32D523F4E616} �����½�ʧ��ʱ ��ع��½��¼״̬
                        if (this.SavePHAMS() == -1)
                        {
                            this.job.State.ID = "M";

                            if (this.jobManager.SetJob(this.job) != 1)
                            {
                                MessageBox.Show(Language.Msg("�½ᷢ������ �ع��½�Job״̬ ��������" + this.jobManager.Err));
                                return -1;
                            }
                        }
                    }
                    break;
                case FS.HISFC.Models.IMA.EnumModuelType.Material:          //����
                    break;
                case FS.HISFC.Models.IMA.EnumModuelType.Equipment:         //�豸
                    break;
            }

            return 1;
        }

        /// <summary>
        /// �½�ִ��
        /// </summary>
        private int SavePHAMS()
        {
            DialogResult result;
            //��ʾ�û�ѡ���Ƿ����
            result = MessageBox.Show(Language.Msg("ȷ�Ͻ����½������"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return -1;
            }

            if (!this.SavePHAVlaid())
                return -1;

            if (!this.JudgeMonthStoreTime())
                return -1;

            //��������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڽ����½ᴦ�� �½�ʱ��ܳ�(4��5��Сʱ).�����ĵȺ�..."));
            Application.DoEvents();

            try
            {
                this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.itemManager.ExecMonthStore(this.privOper.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg("�½����ʧ��" + this.itemManager.Err);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMsg("�½����ʧ��" + ex.Message);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            Function.ShowMsg("�½�����ɹ�");

            #region ����Com_Job�� �����´��½�ʱ��

            this.job.LastTime = this.job.NextTime;
            this.job.NextTime = this.job.NextTime.AddMonths(1);

            if (this.jobManager.SetJob(this.job) != 1)
            {
                MessageBox.Show(Language.Msg("���ݵ�ǰʱ�������½�ʱ�� ��������"));
                return -1;
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// �ж��Ƿ����δ�����̵㵥 ������ʾ
        /// </summary>
        /// <returns>������� True ���� False</returns>
        private bool SavePHAVlaid()
        {
            //�ж�ȫԺ���̵���� 
            List<FS.FrameWork.Models.NeuObject> checkAl = this.itemManager.QueryCheckList("0");
            if (checkAl == null)
            {
                MessageBox.Show(Language.Msg("��ȡ�̵㵥��Ϣ ��������"));
                return false;
            }
            if (checkAl.Count > 0)
            {
                DialogResult rs = MessageBox.Show(Language.Msg("������δ�����̵㵥 �Ƿ���������½�"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rs == DialogResult.No)
                {
                    FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();

                    FS.FrameWork.WinForms.Classes.Function.ChooseItem(new ArrayList(checkAl.ToArray()), ref info);

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// �½�����
        /// </summary>
        private void Set()
        {
            DialogResult result;
            //��ʾ�û�ѡ���Ƿ����
            result = MessageBox.Show(Language.Msg("ȷ�Ͻ����Զ�/�ֶ��½�������"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return;
            }

            if (this.cmbType.Text == "�ֶ�")
                this.job.Type = "0";
            else
                this.job.Type = "1";

            if (this.jobManager.SetJob(this.job) == -1)
            {
                MessageBox.Show(Language.Msg("Jobʵ�屣��ʧ��"));
            }

            MessageBox.Show(Language.Msg("����ɹ�"));
        }

        protected override void OnLoad(EventArgs e)
        {         
            if (!this.DesignMode)
            {
                //�жϲ���Ա�Ƿ�ӵ��Ȩ�ޣ����û������������˴���
                List<FS.FrameWork.Models.NeuObject> alPrivDept = FS.HISFC.Components.Common.Classes.Function.QueryPrivList("0303",true);
                if (alPrivDept == null || alPrivDept.Count == 0)
                    return;

                this.isMonthStorePriv = true;

                this.sysTime = this.itemManager.GetDateTimeFromSysDateTime();

                this.Init();
            }

            this.cmbType.Enabled = false;

            base.OnLoad(e);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Save() == 1)
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
