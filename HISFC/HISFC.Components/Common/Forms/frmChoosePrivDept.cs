using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmChoosePrivDept : Form
    {
        /// <summary>
        /// ���ݴ���Ķ���/����Ȩ���� ��ʾ��Ȩ�޵���Ϣ
        /// </summary>
        /// <param name="class2Code">����Ȩ����</param>
        /// <param name="class3Code">����Ȩ���� �粻���ж�����Ȩ�� ����null</param>
        public frmChoosePrivDept()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ѡ����ʾ��Ϣ
        /// </summary>
        public string NoticeTitle
        {
            set
            {
                this.lbNotice.Text = value;
            }
        }

        /// <summary>
        /// ��ȡ��ǰѡ����Ϣ
        /// </summary>
        public FS.FrameWork.Models.NeuObject SelectData
        {
            get
            {
                return this.GetSelectItem();
            }
        }

        /// <summary>
        /// ����Ȩ�����ȡȨ����Ϣ
        /// </summary>
        /// <param name="userCode">Ȩ����Ա����</param>
        /// <param name="class2Code">����Ȩ����</param>
        /// <param name="class3Code">����Ȩ���� �粻���ж�����Ȩ�� ����null</param>
        /// <returns>�ɹ�����ӵ��1 ʧ�ܷ���0</returns>
        public virtual int GetPrivDept(string userCode,string class2Code, string class3Code)
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();

            List<FS.FrameWork.Models.NeuObject> alPrivDept = new List<FS.FrameWork.Models.NeuObject>();
            if (class3Code != null && class3Code != "")         //ȡ����Աӵ��Ȩ�޵Ŀ���
            {
                alPrivDept = privManager.QueryUserPriv(userCode, class2Code, class3Code);              
            }
            else
            {
                alPrivDept = privManager.QueryUserPriv(userCode, class2Code);
            }

            if (alPrivDept == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡȨ�޿��Ҽ���ʧ��"));
                return 0;
            }
            this.SetPriv(alPrivDept, true);
            return 1;
        }

        /// <summary>
        /// ����Ȩ����Ա����/���ұ���/����Ȩ���� ��ȡ��ӵ�е�����Ȩ�޼���
        /// </summary>
        /// <param name="userCode">Ȩ����Ա����</param>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="class2Code">����Ȩ�ޱ���</param>
        /// <returns>�ɹ�������ӵ�е�Ȩ��1 ʧ�ܷ���0</returns>
        public virtual int GetUserPriv(string userCode, string deptCode, string class2Code)
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> alPriv = privManager.QueryUserPrivCollection(userCode, class2Code, deptCode);
            if (alPriv == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ����Ȩ�޼���ʧ��"));
                return 0;
            }
            this.SetPriv(alPriv, false);

            return 1;
        }

        /// <summary>
        /// ��ʾȨ�޼�����Ϣ
        /// </summary>
        /// <param name="alPriv">Ȩ�޼�����Ϣ</param>
        /// <param name="isPrivDept">Ȩ�޼�����Ϣ True Ȩ�޿��� False Ȩ�޼���</param>
        public virtual void SetPriv(List<FS.FrameWork.Models.NeuObject> alPriv, bool isPrivDept)
        {
            this.nlvPrivInfo.Items.Clear();

            foreach (FS.FrameWork.Models.NeuObject info in alPriv)            
            {
                ListViewItem lv = new ListViewItem(info.Name);
                
                if (isPrivDept)
                    lv.ImageIndex = 1;
                else
                    lv.ImageIndex = 0;

                if (info.User02 != "")
                {
                    this.NoticeTitle = info.User02 + " �� Ȩ��ѡ��";
                }

                lv.Tag = info;

                this.nlvPrivInfo.Items.Add(lv);
            }
        }

        /// <summary>
        /// ���ص�ǰѡ�нڵ���Ϣ
        /// </summary>
        /// <returns>�ɹ�����ѡ�нڵ���Ϣ �ǲ�����null</returns>
        protected FS.FrameWork.Models.NeuObject GetSelectItem()
        {
            ListView.SelectedListViewItemCollection selectItemCollection = this.nlvPrivInfo.SelectedItems;
            if (selectItemCollection != null && selectItemCollection.Count > 0)
                return selectItemCollection[0].Tag as FS.FrameWork.Models.NeuObject;
            else
                return null;
        }

        private void nlvPrivInfo_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectData != null)
                this.DialogResult = DialogResult.OK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SelectData != null)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}