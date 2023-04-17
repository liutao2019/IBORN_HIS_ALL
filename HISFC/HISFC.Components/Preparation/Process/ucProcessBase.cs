using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Preparation.Process
{
    /// <summary>
    /// <br></br>
    /// [��������: ������������¼�����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-11]<br></br>
    /// <˵��>
    /// </˵��>
    /// </summary>
    public partial class ucProcessBase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucProcessBase()
        {
            InitializeComponent();
        }

        public event System.EventHandler ProcessFinished;

        #region �����

        /// <summary>
        /// ���� Control ֵ Processʵ��
        /// </summary>
        protected System.Collections.Hashtable hsProcessItem = new System.Collections.Hashtable();

        /// <summary>
        /// ����ProcessItem ֵ Control
        /// </summary>
        protected System.Collections.Hashtable hsProcessControl = new System.Collections.Hashtable();

        /// <summary>
        /// �Ƽ�ͷ��Ϣ
        /// </summary>
        protected FS.HISFC.Models.Preparation.Preparation preparation = null;

        protected string strPreparation = "�Ƽ���Ʒ��{0}  ���{1}  ���ţ�{2}  �ƻ�����{3}  ��λ��{4}";

        private DialogResult rs = DialogResult.Cancel;

        /// <summary>
        /// �����Ƽ���Ʒ��Ӧ�Ĺ���������Ϣ
        /// </summary>
        private List<FS.HISFC.Models.Preparation.Process> processList = null;

        #endregion

        #region ����

        /// <summary>
        /// ��ȡ�����Ƽ���Ʒ��Ӧ�Ĺ���������Ϣ
        /// </summary>
        public List<FS.HISFC.Models.Preparation.Process> ProcessList
        {
            get
            {
                return this.processList;
            }
        }

        #endregion

        public DialogResult Result
        {
            get
            {
                return rs;
            }
        }

        public virtual int SetPreparation(FS.HISFC.Models.Preparation.Preparation preparation)
        {           
            FS.HISFC.BizLogic.Pharmacy.Preparation pprManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();
            this.processList = pprManager.QueryProcess(preparation.PlanNO, preparation.Drug.ID, ((int)preparation.State).ToString());
            if (this.processList != null && this.processList.Count > 0)
            {
                Function.SetProcessItem(this.processList, this.hsProcessControl);
            }

            this.preparation = preparation;

            return 1;
        }

        /// <summary>
        /// �������̱���
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public virtual int SaveProcess()
        {
            return this.SaveProcess(true);
        }

        /// <summary>
        /// �������̱���
        /// </summary>
        /// <param name="beginTransaction">�Ƿ������� ��False,����Ϊ�������ⲿ���� ���ύ/����ʾ�ɹ�/���Զ���ӡ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public virtual int SaveProcess(bool beginTransaction)
        {
            if (Function.GetProcessItemList(this.panelInput, ref this.hsProcessItem) == 1)
            {
                foreach (FS.HISFC.Models.Preparation.Process info in this.hsProcessItem.Values)
                {
                    info.Preparation = this.preparation.Clone();
                }
            }

            if (beginTransaction)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
            }

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            FS.HISFC.BizLogic.Pharmacy.Preparation pprManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();
            //pprManager.SetTrans(t.Trans);

            DateTime sysTime = pprManager.GetDateTimeFromSysDateTime();

            foreach (FS.HISFC.Models.Preparation.Process p in this.hsProcessItem.Values)
            {
                p.Oper.OperTime = sysTime;
                p.Oper.ID = pprManager.Operator.ID;

                if (pprManager.SetProcess(p) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����Ƽ�����������Ϣʧ��" + pprManager.Err);
                }
            }

            if (beginTransaction)
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("��������ִ����Ϣ����ɹ�");

                this.PrintProcess();
            }

            return 1;
        }

        public virtual int PrintProcess()
        {
            return 1;
        }


        /// <summary>
        /// �ر�
        /// </summary>
        protected void Close()
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        protected virtual void btnCancel_Click(object sender, EventArgs e)
        {
            this.rs = DialogResult.Cancel;

            this.Close();
        }

        protected virtual void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SaveProcess() == 1)
            {
                this.rs = DialogResult.OK;

                if (ProcessFinished != null)
                {
                    this.ProcessFinished(this, System.EventArgs.Empty);
                }

                this.Close();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (MessageBox.Show("�Ƿ�ȷ���˳���", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }

                return true;
            }
            //if (keyData == Keys.Enter)
            //{
            //    System.Windows.Forms.SendKeys.Send("{Tab}");
            //}

            return base.ProcessDialogKey(keyData);
        }
    }
}
