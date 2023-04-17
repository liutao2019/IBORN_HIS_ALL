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

namespace FS.HISFC.Components.Material
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
        /// ͳ�ƿ���
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept;

        /// <summary>
        /// ͳ����Ʒ
        /// </summary>
        private FS.FrameWork.Models.NeuObject item;

        /// <summary>
        /// �ο����� 
        /// </summary>
        private int intervalDays = 7;

        /// <summary>
        /// ��Ʒ������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemHelper = null;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// �Ƿ�ֻ�Ի��������(�ⷿ����ҩ)�������ͳ��
        /// </summary>
        private bool isOnlyPatientInOut = false;

        #endregion

        #region ����

        /// <summary>
        /// ͳ�ƿⷿ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// ͳ����Ʒ
        /// </summary>
        public FS.FrameWork.Models.NeuObject Item
        {
            set
            {
                this.item = value;
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
                return NConvert.ToDateTime(this.dtpEnd.Text);
            }
        }

        /// <summary>
        /// �Ƿ�ֻ�Ի��������(�ⷿ����ҩ)�������ͳ��
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
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ�����1 �������󷵻�-1</returns>
        public int Init()
        {
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList al = deptMgr.GetDeptmentAll();
            if (al == null)
            {
                MessageBox.Show("��ȡ�ⷿ�б�������" + deptMgr.Err);
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

            FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();
            FS.HISFC.Models.Material.MaterialItem listItem = itemManager.GetMetItemByValid("1");
            if (listItem == null)
            {
                MessageBox.Show("��ȡ��Ʒ�б�������" + itemManager.Err);
                return -1;
            }
            ArrayList alItem = new ArrayList();
            alItem.Add(listItem);
            this.cmbItem.AddItems(alItem);
            this.itemHelper = new FS.FrameWork.Public.ObjectHelper(alItem);
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
        public void SetData(FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject item, int intervalDays)
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

            if (item != null && item.ID != "")
            {
                this.item = item;
                this.cmbItem.Text = item.Name;
                this.cmbItem.Tag = item.ID;
            }

            this.lbItemInfo.Text = string.Format("��Ʒ���룺{0} ���{1} ��λ��{2}", item.Name, item.Memo, item.User01);

            this.Query();

        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        public int Query()
        {
            if (this.dept == null)
            {
                MessageBox.Show("�����ò�ѯ����");
                return -1;
            }
            if (this.item == null)
            {
                MessageBox.Show("�����ò�ѯ��Ʒ");
                return -1;
            }
            if (this.deptHelper != null)
                this.dept = this.itemHelper.GetObjectFromID(this.cmbDept.Tag.ToString());
            if (this.itemHelper != null)
                this.item = this.itemHelper.GetObjectFromID(this.cmbItem.Tag.ToString());

            int intervalDays = (this.DtEnd - this.DtBegin).Days;

            this.lbItemInfo.Text = string.Format("��Ʒ���룺{0} ���{1} ��λ��{2}", this.item.Name, this.item.Memo, this.item.User01);

            FS.HISFC.BizLogic.Material.MetItem itemMgr = new FS.HISFC.BizLogic.Material.MetItem();
            decimal totOutNum = 0;
            decimal perDayOutNum = 0;

            #region ��ʱ���� �Ժ����ʵ�������������
            /*
			if (this.isOnlyPatientInOut)
			{
				
				if (itemMgr.FindByExpand(this.dept.ID, this.drug.ID, intervalDays, this.DtEnd, true, out totOutNum, out perDayOutNum) == -1)
				{
					MessageBox.Show("ͳ����Ʒ��������Ϣʧ�ܣ�" + itemMgr.Err);
					return -1;
				}
			}
			else
			{
				if (itemMgr.FindByExpand(this.dept.ID, this.drug.ID, intervalDays, this.DtEnd, out totOutNum, out perDayOutNum) == -1)
				{
					MessageBox.Show("ͳ����Ʒ��������Ϣʧ�ܣ�" + itemMgr.Err);
					return -1;
				}

			}
			*/
            #endregion

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
