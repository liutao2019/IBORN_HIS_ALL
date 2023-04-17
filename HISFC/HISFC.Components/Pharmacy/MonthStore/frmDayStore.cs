using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.MonthStore
{
    /// <summary>
    /// [��������: �ս�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// </summary>
    public partial class frmDayStore : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmDayStore()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ҩƷ������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ��չ����
        /// </summary>
        FS.FrameWork.Management.ExtendParam extManager = new ExtendParam();

        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        DateTime sysTime = System.DateTime.MinValue;

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ����Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject privOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���幦��
        /// </summary>
        private FS.HISFC.Models.IMA.EnumModuelType winFun = FS.HISFC.Models.IMA.EnumModuelType.Phamacy;

        /// <summary>
        /// ��ǰ������
        /// </summary>
        private FS.HISFC.Models.Base.ExtendInfo deptExt = new FS.HISFC.Models.Base.ExtendInfo();

        /// <summary>
        /// �ս����
        /// </summary>
        private string dayStoreTypeParam = "DayStoreLastTime";
        #endregion

        #region ����

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
                        this.dayStoreTypeParam = "DayStoreLastTime";
                        break;
                    case FS.HISFC.Models.IMA.EnumModuelType.Material:          //����
                        this.dayStoreTypeParam = "DayStoreLastTime";
                        break;
                    case FS.HISFC.Models.IMA.EnumModuelType.Equipment:         //�豸
                        this.dayStoreTypeParam = "DayStoreLastTime";
                        break;
                    default:
                        this.winFun = FS.HISFC.Models.IMA.EnumModuelType.Phamacy;
                        this.dayStoreTypeParam = "DayStoreLastTime";
                        break;
                }
            }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        protected DateTime BeginTime
        {
            get
            {
                return NConvert.ToDateTime(this.dtpBegin.Text);
            }            
        }

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        protected DateTime EndTime
        {
            get
            {
                return NConvert.ToDateTime(this.dtpEnd.Text);
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            #region ��ȡ����

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList al = deptManager.GetDeptmentAll();

            ArrayList alDept = new ArrayList();
            foreach (FS.HISFC.Models.Base.Department info in al)
            {
                if (info.DeptType.ID.ToString() == "P" || info.DeptType.ID.ToString() == "PI")
                {
                    alDept.Add(info);
                }
            }

            this.cmbStoreDept.AddItems(alDept);

            int iIndex = 0;
            foreach (FS.HISFC.Models.Base.Department dept in alDept)
            {
                if (dept.ID == this.privDept.ID)
                {
                    break;
                }
                iIndex++;
            }

            this.cmbStoreDept.SelectedIndex = iIndex;
            #endregion

            this.GetLastTime();
        }

        /// <summary>
        /// ��ȡ�������ϴ��ս�ʱ��
        /// </summary>
        /// <returns></returns>
        protected int GetLastTime()
        {
            //��ȡ�������ϴ��ս�ʱ��
            this.deptExt = this.extManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, this.dayStoreTypeParam, this.privDept.ID);
            if (this.deptExt == null)
            {
                MessageBox.Show(Language.Msg("��ȡ������չ�������ϴ��ս�ʱ��ʧ�ܣ�"));
                return -1;
            }
            if (deptExt.ID == "")
            {
                this.deptExt.Item.ID = this.privDept.ID;
                this.deptExt.PropertyCode = this.dayStoreTypeParam;
                this.deptExt.PropertyName = "�����ϴ��ս�ʱ��";
            }
            if (deptExt.DateProperty == System.DateTime.MinValue)
            {
                this.dtpBegin.Value = this.extManager.GetDateTimeFromSysDateTime().AddDays(-1);
                this.dtpBegin.Enabled = true;

                this.lbNotice.Visible = true;
            }
            else
            {
                this.dtpBegin.Value = this.deptExt.DateProperty;
                this.dtpBegin.Enabled = false;

                this.lbNotice.Visible = false;
            }

            return 1;
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
                        return this.SavePHAMS();
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
            if (!this.SavePHAValid())
            {
                return 0;
            }

            DialogResult result;
            //��ʾ�û�ѡ���Ƿ����
            result = MessageBox.Show(Language.Msg("ȷ�Ͻ����ս������"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
                MessageBoxOptions.RightAlign);
            if (result == DialogResult.No)
            {
                return -1;
            }

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڽ����սᴦ�� �ս�ʱ��ܳ�.�����ĵȺ�..."));
            Application.DoEvents();

            try
            {
                this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.itemManager.ExecDayStore(this.privDept.ID,this.BeginTime,this.EndTime,this.privOper.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMsg("�ս����ʧ��" + this.itemManager.Err);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMsg("�ս����ʧ��" + ex.Message);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            Function.ShowMsg("�ս�����ɹ�");

            #region ������չ�� �����´��ս�ʱ��

            this.deptExt.DateProperty = this.EndTime;

            this.dtpBegin.Value = this.EndTime;
            this.dtpEnd.Value = this.dtpBegin.Value.AddDays(1);

            if (this.extManager.SetComExtInfo(this.deptExt) != 1)
            {
                MessageBox.Show(Language.Msg("���ݵ�ǰʱ�������ս�ʱ�� ��������"));
                return -1;
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// �����Ƿ���Խ����ս�
        /// </summary>
        /// <returns></returns>
        private bool SavePHAValid()
        {
            if (this.BeginTime >= this.EndTime)
            {
                MessageBox.Show(Language.Msg("��������ʼʱ�������ֹʱ��"));
                return false;
            }
            return true;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!this.DesignMode)
            {
                //�жϲ���Ա�Ƿ�ӵ��Ȩ�ޣ����û������������˴���
                List<FS.FrameWork.Models.NeuObject> alPrivDept = FS.HISFC.Components.Common.Classes.Function.QueryPrivList("0303", true);
                if (alPrivDept == null || alPrivDept.Count == 0)
                    return;

                this.sysTime = this.itemManager.GetDateTimeFromSysDateTime();

                this.privDept = ((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept;
                this.privOper = this.itemManager.Operator;

                this.Init();
            }

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

        private void cmbStoreDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbStoreDept.Tag == null || this.cmbStoreDept.Tag.ToString() == "")
            {
                return;
            }

            this.privDept.ID = this.cmbStoreDept.Tag.ToString();

            this.GetLastTime();
        }
    }
}