using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: ������ά��]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    public partial class ucNurseTendGroupManager : UserControl
    {
        public ucNurseTendGroupManager()
        {
            InitializeComponent();
        }

        private Neusoft.FrameWork.Models.NeuObject nurseCell = null;

        /// <summary>
        /// 
        /// </summary>
        public Neusoft.FrameWork.Models.NeuObject NurseCell
        {
            get
            {
                return this.nurseCell;
            }
            set
            {
                this.nurseCell = value;
            }
        }
        Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ˢ���б�
        /// </summary>
        public void RefreshList()
        {
            if (this.nurseCell == null || this.nurseCell.ID == "") return;

            //��սڵ�
            this.listView1.Items.Clear();

            //ȡ������վ��λ�ͻ�������Ϣ
            ArrayList al = manager.QueryBedNurseTendGroupList(this.nurseCell.ID);
            if (al == null)
            {
                MessageBox.Show(manager.Err);
                return;
            }

            //��ʾ��λ�ͻ�����
            foreach (Neusoft.FrameWork.Models.NeuObject obj in al)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(obj.ID);				//����
                item.SubItems.Add(obj.Name);			//������
                item.Text = "��" + obj.ID.Substring(4) + "��" + obj.Name;	//��ʾ�ı�
                item.ImageIndex = 0;
                item.Tag = obj.ID;
                this.listView1.Items.Add(item);
            }
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (this.listView1.SelectedItems.Count <= 0)
            {
                MessageBox.Show("��ѡ�����λ��");
                return -1;
            }

            //ȡ��ǰѡ�еĽڵ�
            ListViewItem item = this.listView1.SelectedItems[0];
            if (manager.UpdateNurseTendGroup(item.Tag.ToString(), this.txtGroup.Text) == -1)
            {
                MessageBox.Show(manager.Err);
                return -1;
            }
            else
            {
                item.Text = "��" + item.Tag.ToString().Substring(4) + "��" + this.txtGroup.Text;
            }
            return 1;
        }


        private void btnOKClick(object sender, System.EventArgs e)
        {
            this.Save();
        }

        private void txtGroup_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.Save();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.listView1.SelectedItems.Count <= 0) return;
            //ȡ��ǰѡ�еĽڵ�
            ListViewItem item = this.listView1.SelectedItems[0];
            string s = item.Text;
            try
            {
                //ȡ��������Ϣ,����ʾ��ά������
                s = s.Substring(s.LastIndexOf("��") + 1);
                this.txtGroup.Text = s;
                this.txtGroup.Focus();
            }
            catch { }
        }


    }
}
