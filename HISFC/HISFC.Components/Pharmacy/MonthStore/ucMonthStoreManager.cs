using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Pharmacy.MonthStore
{
    /// <summary>
    /// [��������: �½��ʹ���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// </summary>
    public partial class ucMonthStoreManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMonthStoreManager()
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
        FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// ҩƷ�½�����
        /// </summary>
        FS.HISFC.Models.Base.Job job = new FS.HISFC.Models.Base.Job();

        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        DateTime sysTime = System.DateTime.MinValue;

        /// <summary>
        /// ҩƷ�½����Ȩ��
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

        /// <summary>
        /// ��ϸ��Ϣ
        /// </summary>
        private DataSet dsDetail = null;

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

        /// <summary>
        /// �Ƿ�����½�Ȩ��
        /// </summary>
        protected bool IsMonthStorePriv
        {
            get
            {
                return this.isMonthStorePriv;
            }
            set
            {
                this.isMonthStorePriv = value;

                this.toolBarService.SetToolButtonEnabled("�½�", value);
                this.toolBarService.SetToolButtonEnabled("����", value);
            }
        }

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        protected bool IsFilter
        {
            get
            {
                return this.txtFilter.Enabled;
            }
            set
            {
                this.txtFilter.Enabled = value;
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "�����½��¼", FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);
            
            toolBarService.AddToolButton("�½�", "���������½��¼", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                this.DelMonthStore();
            }
            if (e.ClickedItem.Text == "�½�")
            {
                this.MonthStore();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.MonthStore();

            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ShowMonthStoreHead();

            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        protected int Init()
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

            #endregion

            this.job = this.jobManager.GetJob(this.monthStoreType);
            if (this.job == null)
            {
                MessageBox.Show(Language.Msg("����ҩƷ�½�����ȡҩƷ�½�����ʧ��"));
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ʾ�½��¼
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        protected int ShowMonthStoreHead()
        {
            if (this.cmbStoreDept.Tag == null || this.cmbStoreDept.Tag.ToString() == "")
            {
                return -1;
            }

            this.neuSpread1.ActiveSheet = this.fpHeadSheet;

            DataSet dsHead = new DataSet();
            if (this.consManager.QueryMonthStoreHead(this.cmbStoreDept.Tag.ToString(),ref dsHead) == -1)
            {
                MessageBox.Show(Language.Msg("��ȡ�½������Ϣʧ��") + this.consManager.Err);
                return -1;
            }
            if (dsHead.Tables.Count <= 0)
            {
                return -1;
            }

            this.fpHeadSheet.DataSource = dsHead;

            return 1;
        }

        /// <summary>
        /// ��ȡ�½���ϸ��Ϣ
        /// </summary>
        /// <returns></returns>
        protected int ShowMonthStoreDetail()
        {
            DateTime dtBeginTime = NConvert.ToDateTime(this.fpHeadSheet.Cells[this.fpHeadSheet.ActiveRowIndex, 0].Text);
            DateTime dtEndTime = NConvert.ToDateTime(this.fpHeadSheet.Cells[this.fpHeadSheet.ActiveRowIndex, 1].Text);

            if (this.cmbStoreDept.Tag == null || this.cmbStoreDept.Tag.ToString() == "")
            {
                return -1;
            }

            dsDetail = new DataSet();
            if (this.consManager.QueryMonthStoreDetail(this.cmbStoreDept.Tag.ToString(), dtBeginTime, dtEndTime, ref  dsDetail) == -1)
            {
                MessageBox.Show(Language.Msg("��ȡ�½���ϸ��Ϣʧ��") + this.consManager.Err);
                return -1;
            }

            if (dsDetail.Tables.Count <= 0)
            {
                return -1;
            }

            this.fpDetailSheet.DataSource = dsDetail.Tables[0].DefaultView;

            if (dsDetail.Tables[0].Rows.Count > 0)
            {
                this.neuSpread1.ActiveSheet = this.fpDetailSheet;
            }

            return 1;

        }

        /// <summary>
        /// �½�ִ��
        /// </summary>
        /// <returns></returns>
        protected int MonthStore()
        {
            frmMonthStore uc = new frmMonthStore();

            uc.IsShowButton = true;

            //FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            uc.ShowDialog();

            this.ShowMonthStoreHead();

            return 1;
        }

        /// <summary>
        /// �½��¼����ɾ��
        /// </summary>
        /// <returns></returns>
        protected int DelMonthStore()
        {
            if (this.neuSpread1.ActiveSheet == this.fpDetailSheet)
            {
                return 0;
            }
            if (this.fpHeadSheet.Rows.Count <= 0)
            {
                return 0;
            }

            //�ж��Ƿ����һ���½��¼ 
            if (this.fpHeadSheet.ActiveRowIndex > 0)
            {
                MessageBox.Show(Language.Msg("���½��ʵ�ɾ��ֻ��ɾ�����һ���½��¼ ���ɿ�ʱ���ɾ�� ����ɾ���˼�¼ǰ�����½��¼"));
                return 0;
            }

            DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ɾ���˴��½��¼�� \n ע�� �˲������ɳ��� �ҽ�ɾ����Ժ���пⷿ�Ĵ˴��½��¼��"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.No)
            {
                return 0;
            }

            DateTime dtBeginTime = NConvert.ToDateTime(this.fpHeadSheet.Cells[this.fpHeadSheet.ActiveRowIndex,0].Text);
            DateTime dtEndTime = NConvert.ToDateTime(this.fpHeadSheet.Cells[this.fpHeadSheet.ActiveRowIndex, 1].Text);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.jobManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.consManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.job.ID == "")
            {
                this.job.ID = this.monthStoreType;
            }

            if (this.fpHeadSheet.Rows.Count == 1)           //ֻ��һ���½��¼ ���job
            {
                if (this.jobManager.DeleteJob(this.job.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("����ͳ��ʱ���ʧ�� ") + this.jobManager.Err);
                    return -1;
                }
            }
            else                                           //�����½�ͳ�Ʊ�
            {
                this.job.LastTime = dtBeginTime;
                this.job.NextTime = dtBeginTime.AddMonths(1);

                if (this.jobManager.UpdateJob(this.job) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("����ͳ��ʱ���ʧ�� ") + this.jobManager.Err);
                    return -1;
                }
            }

            if (this.consManager.DelMonthStore(dtBeginTime, dtEndTime) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("ҩƷ�½��¼���Ϸ�������") + this.consManager.Err);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("����ɹ�"));

            this.ShowMonthStoreHead();

            return 1;
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                //�жϲ���Ա�Ƿ�ӵ��Ȩ�ޣ����û������������˴���
                List<FS.FrameWork.Models.NeuObject> alPrivDept = FS.HISFC.Components.Common.Classes.Function.QueryPrivList("0303", true);
                if (alPrivDept == null || alPrivDept.Count == 0)
                {
                    this.isMonthStorePriv = false;
                }
                else
                {
                    this.isMonthStorePriv = true;
                }

                this.sysTime = this.consManager.GetDateTimeFromSysDateTime();

                if (this.Init() == -1)
                {
                    this.isMonthStorePriv = false;
                    return;
                }

                this.IsFilter = false;
            }

            base.OnLoad(e);
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }

            if (this.neuSpread1.ActiveSheet == this.fpDetailSheet)
            {
                return;
            }

            this.ShowMonthStoreDetail();
        }

        private void cmbStoreDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowMonthStoreHead();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (this.dsDetail == null || this.dsDetail.Tables == null || this.dsDetail.Tables.Count < 0 || this.dsDetail.Tables[0].DefaultView == null)
            {
                return;
            }

            //��ù�������
            string queryCode = "%" + this.txtFilter.Text + "%";

            string filter = Function.GetFilterStr(this.dsDetail.Tables[0].DefaultView, queryCode);

            try
            {
                this.dsDetail.Tables[0].DefaultView.RowFilter = filter;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("���˷����쳣 " + ex.Message));
            }
        }

        private void neuSpread1_ActiveSheetChanged(object sender, EventArgs e)
        {
            if (this.neuSpread1.ActiveSheet == this.fpHeadSheet)
            {
                this.IsFilter = false;
            }
            else
            {
                this.IsFilter = true;
            }
        }
    }
}
