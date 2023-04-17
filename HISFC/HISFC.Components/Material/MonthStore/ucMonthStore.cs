using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Material.MonthStore
{
    /// <summary>
    /// [��������:���ñ����½�ʱ��]
    /// [�� �� ��:��ά]
    /// [����ʱ��:2008-03]
    /// </summary>
    public partial class ucMonthStore : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public ucMonthStore()
        {
            InitializeComponent();
        }

        #region �����
        /// <summary>
        /// ������
        /// </summary>
        FS.HISFC.BizLogic.Manager.Job jobManager = new FS.HISFC.BizLogic.Manager.Job();

        /// <summary>
        /// ������
        /// </summary>
        FS.HISFC.BizLogic.Material.MetItem metItemManager = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// ���ʹ�����
        /// </summary>
        FS.HISFC.BizLogic.Material.Store matManager = new FS.HISFC.BizLogic.Material.Store();

        /// <summary>
        /// �����½�����
        /// </summary>
        FS.HISFC.Models.Base.Job job = new FS.HISFC.Models.Base.Job();

        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        DateTime sysTime = System.DateTime.MinValue;

        /// <summary>
        /// �����½�Ȩ��
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
        private FS.HISFC.Models.IMA.EnumModuelType winFun = FS.HISFC.Models.IMA.EnumModuelType.Material;

        /// <summary>
        /// �½�����ַ���
        /// </summary>
        private string monthStoreType = "MAT_MS";
        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.job = this.jobManager.GetJob(this.monthStoreType);
            if (this.job == null)
            {
                MessageBox.Show(Language.Msg("���������½�����ȡ�����½�����ʧ��"));
                return;
            }
            if (this.job.ID != "")
            {
                if (this.job.Type == "0")
                {
                    this.cmbType.Text = "�ֶ�";
                }
                else
                {
                    this.cmbType.Text = "�Զ�";
                }
                this.dtpLast.Enabled = false;
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
        /// �����½�ʱ������
        /// </summary>
        /// <returns></returns>
        private bool JudgeMonthStoreTime()
        {
            DialogResult rs = MessageBox.Show(Language.Msg("�ϴ��½�ʱ��Ϊ" + this.job.LastTime.ToString() + "\nȷ�����ڽ����½���"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.No)
            {
                return false;
            }
            rs = MessageBox.Show(Language.Msg("�Ƿ�����½����ʱ������ ѡ��'��' �����½��ֹʱ�� ѡ��'��' �����½����ʱ��Ϊ��ǰ����"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.Yes)
            {
                ucMonthStoreSet uc = new ucMonthStoreSet();
                uc.SetJob(this.job.Clone(), this.sysTime.AddSeconds(-1));

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);

                if (uc.Result == DialogResult.Cancel)
                {
                    return false;
                }

                this.job.NextTime = uc.NextTime;
            }
            else
            {
                //��һ�� ��֤�洢�������ж�ʱ���Ƿ��ܹ��½�ʱ ��������ִ��
                this.job.NextTime = this.sysTime.AddSeconds(-1);
            }

            //����Com_Job���е��´��½�ʱ���ֶΣ�ʵ���½�ʱ���ѡ
            if (this.jobManager.SetJob(this.job) != 1)
            {
                MessageBox.Show(Language.Msg("���ݵ�ǰʱ�������½�ʱ�� ��������"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// ִ���½�
        /// </summary>
        /// <returns></returns>
        public int SaveMATMS()
        {
            DialogResult rs = MessageBox.Show(Language.Msg("ȷ�Ͻ����½������"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign);

            if(rs == DialogResult.No)
            {
                return -1;
            }

            if(!this.SaveMATMSVlaid())
            {
                return -1;
            }

            if(!this.JudgeMonthStoreTime())
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڽ����½ᴦ��.�����Ⱥ�..."));
            Application.DoEvents();

            try
            {
                this.matManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if(this.matManager.ExePrcForMonthStore(this.privOper.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg("�½����ʧ��" + this.matManager.Err);
                    return -1;
                }
            }
            catch (System.Exception ex)
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
        /// �ж��Ƿ����δ�����̵㵥[��ʱû�������]
        /// </summary>
        /// <returns></returns>
        private bool SaveMATMSVlaid()
        {
            //�ж�ȫԺ���̵���� 
            ArrayList checkAl = this.metItemManager.QueryCheckStatic("A", "0");
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

                    FS.FrameWork.WinForms.Classes.Function.ChooseItem(checkAl, ref info);

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

        /// <summary>
        /// ����
        /// </summary>
        public virtual int Save()
        {
            switch (this.winFun)
            {
                case FS.HISFC.Models.IMA.EnumModuelType.Material:           //����
                case FS.HISFC.Models.IMA.EnumModuelType.All:
                    if (this.isMonthStorePriv)
                    {
                        return this.SaveMATMS();
                    }
                    break;
                case FS.HISFC.Models.IMA.EnumModuelType.Phamacy:          //ҩƷ
                    break;
                case FS.HISFC.Models.IMA.EnumModuelType.Equipment:         //�豸
                    break;
            }

            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!this.DesignMode)
            {
                //�жϲ���Ա�Ƿ�ӵ��Ȩ�ޣ����û������������˴���
                List<FS.FrameWork.Models.NeuObject> alPrivDept = FS.HISFC.Components.Common.Classes.Function.QueryPrivList("0503", true);
                if (alPrivDept == null || alPrivDept.Count == 0)
                    return;

                this.isMonthStorePriv = true;

                this.sysTime = this.matManager.GetDateTimeFromSysDateTime();

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
