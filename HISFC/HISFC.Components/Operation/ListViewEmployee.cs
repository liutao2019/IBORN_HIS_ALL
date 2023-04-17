using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.ComponentModel;
using FS.FrameWork.Models;
using System.Windows.Forms;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ������Ա�б�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-26]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class ListViewEmployee : FS.FrameWork.WinForms.Controls.NeuListView
    {

        public ListViewEmployee()
        {
            this.imageList.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R��Ա));
            this.LargeImageList = this.imageList;
            this.SmallImageList = this.imageList;
            this.StateImageList = this.imageList;
        }
#region �ֶ�
        private string deptID;
        private ImageList imageList = new ImageList();
#endregion

#region ����
        /// <summary>
        /// ���ұ���
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DeptID
        {
            get
            {
                return this.deptID;
            }

            set
            {
                this.deptID = value;
                this.LoadData(value);
            }
        }
#endregion


#region ����

        private void LoadData(string deptID)
        {
            ArrayList al = Environment.IntegrateManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, deptID);
            foreach(NeuObject obj in al)
            {
                ListViewItem item = new ListViewItem();
                item.Text = obj.Name;
                item.ImageIndex = 0;
                item.Tag = obj;
                this.Items.Add(item);
            }
        }

        public void Refresh()
        {
            this.Items.Clear();
            this.LoadData(this.deptID);
        }

        /// <summary>
        /// �Ƴ���Ա
        /// </summary>
        /// <param name="id">��ԱID</param>
        /// <returns></returns>
        public int RemoveEmployee(string id)
        {
            foreach(ListViewItem item in this.Items)
            {
                if((item.Tag as NeuObject).ID==id)
                {
                    this.Items.Remove(item);
                    return 0;
                }
            }

            return -1;
        }
#endregion
    }
}
