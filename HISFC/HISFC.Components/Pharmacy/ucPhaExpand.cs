using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ����������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class ucPhaExpand : UserControl
    {
        public ucPhaExpand()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ͳ��ҩ��
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept;

        /// <summary>
        /// ͳ��ҩƷ
        /// </summary>
        private FS.FrameWork.Models.NeuObject drug;

        /// <summary>
        /// �ο����� 
        /// </summary>
        private int intervalDays = 7;

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper drugHelpter = null;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// �Ƿ�ֻ�Ի��������(ҩ������ҩ)�������ͳ��
        /// </summary>
        private bool isOnlyPatientInOut = false;

        #endregion

        #region ����

        /// <summary>
        /// ͳ��ҩ��
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// ͳ��ҩƷ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Drug
        {
            set
            {
                this.drug = value;
            }
        }

        /// <summary>
        /// �ο�����
        /// </summary>
        public int IntervalDays
        {
            set
            {
                this.intervalDays = value;
            }
        }

        /// <summary>
        /// ͳ����ʼʱ��
        /// </summary>
        private DateTime DtBegin
        {
            get
            {
                return NConvert.ToDateTime(this.dtpBegin.Text);
            }
        }

        /// <summary>
        /// ͳ�ƽ�ֹʱ��
        /// </summary>
        private DateTime DtEnd
        {
            get
            {
                DateTime dateTime = NConvert.ToDateTime(this.dtpEnd.Text);
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            }
        }

        /// <summary>
        /// �Ƿ�ֻ�Ի��������(ҩ������ҩ)�������ͳ��
        /// </summary>
        public bool IsOnlyPatientInOut
        {
            get
            {
                return this.isOnlyPatientInOut;
            }
            set
            {
                this.isOnlyPatientInOut = value;
            }
        }

        #endregion

     
        /// <summary>
        /// ��ʼ��  �ⲿ���ó�ʼ��
        /// </summary>
        /// <returns>�ɹ�����1 �������󷵻�-1</returns>
        public int Init()
        {
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList al = deptMgr.GetDeptmentAll();
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡҩ���б�������" + deptMgr.Err));
                return -1;
            }
            ArrayList alDept = new ArrayList();
            foreach (FS.HISFC.Models.Base.Department info in al)
            {
                if (info.DeptType.ID.ToString() == "P" || info.DeptType.ID.ToString() == "PI")
                {
                    alDept.Add(info);
                }
            }

            FS.FrameWork.Models.NeuObject deptAll = new FS.FrameWork.Models.NeuObject();
            deptAll.ID = "AAAA";
            deptAll.Name = "ȫ��";
            alDept.Insert(0, deptAll);

            this.cmbDept.AddItems(alDept);
            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

            FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            List<FS.HISFC.Models.Pharmacy.Item> listDrug = itemMgr.QueryItemAvailableList(true);
            if (listDrug == null)
            {
                MessageBox.Show(Language.Msg("��ȡҩƷ�б�������" + itemMgr.Err));
                return -1;
            }
            ArrayList alDrug = new ArrayList();
            FS.FrameWork.Models.NeuObject drugInfo = new FS.FrameWork.Models.NeuObject();
            foreach (FS.HISFC.Models.Pharmacy.Item info in listDrug)
            {
                drugInfo = new FS.FrameWork.Models.NeuObject();
                drugInfo.ID = info.ID;
                drugInfo.Name = info.Name;
                drugInfo.Memo = info.Specs;
                drugInfo.User01 = info.MinUnit;

                alDrug.Add(drugInfo);
            }

            this.cmbItem.AddItems(alDrug);
            this.drugHelpter = new FS.FrameWork.Public.ObjectHelper(alDrug);
            return 1;
        }

        /// <summary>
        /// �ر�
        /// </summary>
        public void Close()
        {
            if (this.ParentForm != null)
                this.ParentForm.Close();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public void SetData(FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject drug, int intervalDays)
        {
            FS.FrameWork.Management.DataBaseManger databaseMgr = new FS.FrameWork.Management.DataBaseManger();
            DateTime sysTime = databaseMgr.GetDateTimeFromSysDateTime().Date;

            this.dtpEnd.Value = sysTime;
            this.dtpEnd.Text = sysTime.ToString();
            this.dtpBegin.Value = sysTime.AddDays(-intervalDays);
            this.dtpBegin.Text = sysTime.AddDays(-intervalDays).ToString();

            if (dept != null && dept.ID != "")
            {
                this.Dept = dept;
                this.cmbDept.Text = dept.Name;
                this.cmbDept.Tag = dept.ID;
            }

            if (drug != null && drug.ID != "")
            {
                this.Drug = drug;
                this.cmbItem.Text = drug.Name;
                this.cmbItem.Tag = drug.ID;
            }

            this.lbItemInfo.Text = string.Format("ҩƷ���룺{0} ���{1} ��λ��{2}", drug.Name, drug.Memo, drug.User01);

            this.Query();

        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        public int Query()
        {
            if (this.dept == null)
            {
                MessageBox.Show(Language.Msg("�����ò�ѯҩ��"));
                return -1;
            }
            if (this.drug == null)
            {
                MessageBox.Show(Language.Msg("�����ò�ѯҩƷ"));
                return -1;
            }
            if (this.deptHelper != null)
                this.dept = this.deptHelper.GetObjectFromID(this.cmbDept.Tag.ToString());
            if (this.drugHelpter != null)
                this.drug = this.drugHelpter.GetObjectFromID(this.cmbItem.Tag.ToString());

            int intervalDays = (this.DtEnd - this.DtBegin).Days;

            this.lbItemInfo.Text = string.Format("ҩƷ���룺{0} ���{1} ��λ��{2}", this.drug.Name, this.drug.Memo, this.drug.User01);

            FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            decimal totOutNum = 0;
            decimal perDayOutNum = 0;
            if (this.isOnlyPatientInOut)
            {
                if (itemMgr.FindByExpand(this.dept.ID, this.drug.ID, intervalDays, this.DtEnd, true, out totOutNum, out perDayOutNum) == -1)
                {
                    MessageBox.Show("ͳ��ҩƷ��������Ϣʧ�ܣ�" + itemMgr.Err);
                    return -1;
                }
            }
            else
            {
                if (itemMgr.FindByExpand(this.dept.ID, this.drug.ID, intervalDays, this.DtEnd, out totOutNum, out perDayOutNum) == -1)
                {
                    MessageBox.Show("ͳ��ҩƷ��������Ϣʧ�ܣ�" + itemMgr.Err);
                    return -1;
                }
            }

            this.lbExpandInfo.Text = string.Format("�ο�������{0}�� ����������{1} �����ģ�{2}", intervalDays.ToString(), totOutNum.ToString("N"), perDayOutNum.ToString("N"));

            return 1;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.Query() == -1)
                return;
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            if (this.DtEnd < this.DtBegin)
                this.dtpEnd.Value = this.dtpBegin.Value;
        }
    }
}
