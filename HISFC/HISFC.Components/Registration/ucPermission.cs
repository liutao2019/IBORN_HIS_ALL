using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration
{
    public partial class ucPermission : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPermission()
        {
            InitializeComponent();
            //{2EFAB4DC-DBD9-45ed-9D09-B3F34856CA2F}
            //this.Init();
        }
        //{2EFAB4DC-DBD9-45ed-9D09-B3F34856CA2F}
        /// <summary>
        /// �ҺŴ���
        /// </summary>
        private string stringRegWindowsUC = string.Empty;

        /// <summary>
        /// {D623D221-1472-4dc9-B84C-F3E0F4D0C256}
        /// </summary>
        [Category("�ؼ�����"), Description("�����趨�ĹҺŴ���ѡ��ҺŲ���Ա���趨Ҫ���ƵĹҺŴ���")]
        public string StringRegWindowsUC
        {
            get
            {
                return stringRegWindowsUC;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    //{F22A07D9-EEFD-4b1c-8975-1F01A5B31C53}
                    this.stringRegWindowsUC = "FS.HISFC.Components.Registration.ucRegister";
                }
                else
                {
                    stringRegWindowsUC = value;
                }
            }
        }


        private ArrayList al = null;
        /// <summary>
        /// �Һ�ԱȨ�޹�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Permission perMgr = new FS.HISFC.BizLogic.Registration.Permission();
        
        private Hashtable htDept = new Hashtable();

        private void Init()
        {
            this.initTree();
            //�õ��Һſ���
            FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            al = deptMgr.QueryRegDepartment();

            if (al == null) al = new ArrayList();
            this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 1, al);

            foreach (FS.HISFC.Models.Base.Department dept in al)
            {
                htDept.Add(dept.ID, dept.Name);
            }

            this.fpEnter1.KeyEnter += new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);
            this.fpEnter1.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
        }

        /// <summary>
        /// ���ɹҺ�Ա�б�
        /// </summary>
        private void initTree()
        {
            this.treeView1.Nodes.Clear();
            treeView1.ImageList = treeView1.groupImageList;
            TreeNode root = new TreeNode("�Һ�Ա", 22, 22);
            root.ImageIndex = 2;
            root.SelectedImageIndex = 2;
            this.treeView1.Nodes.Add(root);

            //��ò����ҺŴ��ڵ���Ա{2EFAB4DC-DBD9-45ed-9D09-B3F34856CA2F}
            //this.al = this.perMgr.Query("FS.UFC.Registration.ucRegister");
            this.al = this.perMgr.Query(this.stringRegWindowsUC);
            if (al != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    TreeNode node = new TreeNode(obj.Name, 34, 35);
                   
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 1;
                    node.Tag = obj;
                    root.Nodes.Add(node);
                }
            }
            root.Expand();
        }

        /// <summary>
        /// Select change to retrieve dept
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            TreeNode current = this.treeView1.SelectedNode;



            if (current == null || current.Parent == null)
            {
                if (this.fpEnter1_Sheet1.RowCount > 0)
                    this.fpEnter1_Sheet1.Rows.Remove(0, this.fpEnter1_Sheet1.RowCount);

                this.fpEnter1_Sheet1.Tag = null;
            }
            else
            {
                FS.FrameWork.Models.NeuObject person = (FS.FrameWork.Models.NeuObject)current.Tag;
                this.refresh(person);
            }



        }
        /// <summary>
        /// ˢ�¹Һſ���
        /// </summary>
        /// <param name="person"></param>
        private void refresh(FS.FrameWork.Models.NeuObject person)
        {
            if (this.fpEnter1_Sheet1.RowCount > 0)
                this.fpEnter1_Sheet1.Rows.Remove(0, this.fpEnter1_Sheet1.RowCount);

            //��������Ա�ĹҺſ���
            al = this.perMgr.Query(person);

            this.fpEnter1_Sheet1.Tag = person;

            if (al != null)
            {
                int i = 0;

                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.RowCount, 1);
                    int row = this.fpEnter1_Sheet1.RowCount - 1;

                    this.fpEnter1_Sheet1.SetValue(row, 0, person.Name, false);
                    this.fpEnter1_Sheet1.SetValue(row, 1, this.getDeptNameByID(obj.User01), false);
                    this.fpEnter1_Sheet1.SetTag(row, 1, obj.User01);
                    this.fpEnter1_Sheet1.SetValue(row, 2, obj.User02, false);
                    this.fpEnter1_Sheet1.SetValue(row, 3, obj.User03, false);
                    this.fpEnter1_Sheet1.Rows[row].Tag = obj;
                    i++;
                    if (i == 1)
                    {
                        bool isContain = FS.FrameWork.Function.NConvert.ToBoolean(obj.Memo);
                        if (isContain)
                        {
                            this.rdIn.Checked = true;
                        }
                        else
                        {
                            this.rdOut.Checked = true;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// ���ݿ��Ҵ����������
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string getDeptNameByID(string ID)
        {
            IDictionaryEnumerator dict = this.htDept.GetEnumerator();
            while (dict.MoveNext())
            {
                if (dict.Key.ToString() == ID)
                    return dict.Value.ToString();
            }
            return ID;
        }
        /// <summary>
        /// ���һ��
        /// </summary>
        private void Add()
        {
            if (this.fpEnter1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ��Һ�Ա!", "��ʾ");
                return;
            }
            this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.RowCount, 1);
            int row = this.fpEnter1_Sheet1.RowCount - 1;

            string test = string.Empty;
            if (this.fpEnter1_Sheet1.Tag != null)
            {
                test=(this.fpEnter1_Sheet1.Tag as FS.FrameWork.Models.NeuObject).Name;
            }
            else
            {
                test="";
            }
            this.fpEnter1_Sheet1.SetValue(row, 0, test, false);
            this.fpEnter1_Sheet1.SetValue(row, 2, this.perMgr.Operator.ID, false);
            this.fpEnter1_Sheet1.SetValue(row, 3, this.perMgr.GetDateTimeFromSysDateTime().ToString(), false);
            this.fpEnter1.Focus();
            this.fpEnter1_Sheet1.SetActiveCell(row, 1, false);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        private void Del()
        {
            int row = this.fpEnter1_Sheet1.ActiveRowIndex;
            if (row < 0 || this.fpEnter1_Sheet1.RowCount == 0) return;

            if (MessageBox.Show("�Ƿ�Ҫɾ���ü�¼?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            //�Ѿ��������Ŀ,�����ݿ���ɾ��
            if (this.fpEnter1_Sheet1.Rows[row].Tag != null)
            {
                //				FS.neuFC.Management.Transaction t=new FS.neuFC.Management.Transaction(var.con);
                //				t.BeginTransaction();
                //				this.perMgr.SetTrans(t.Trans);
                //
                //				try
                //				{
                //					if(this.perMgr.Delete((FS.neuFC.Object.neuObject)this.fpEnter1_Sheet1.Rows[row].Tag)==-1)
                //					{
                //						t.RollBack();
                //						MessageBox.Show(this.perMgr.Err,"��ʾ");
                //						return;
                //					}
                //					t.Commit();
                //				}
                //				catch(Exception e)
                //				{
                //					t.RollBack();
                //					MessageBox.Show(e.Message,"��ʾ");
                //					return;
                //				}
            }
            this.fpEnter1_Sheet1.Rows.Remove(row, 1);

            //			MessageBox.Show("ɾ���ɹ�!","��ʾ");
        }

        //{C6CA49C6-D27C-49b6-BEF0-14DF2A27F6CD}
        /// <summary>
        /// У��
        /// </summary>
        protected virtual int  Valid()
        {
            int returnValue = this.ValidRepeatDept();
            if (returnValue < 0)
            {
                return -1;
            }
            return 1;
        }

        //{C6CA49C6-D27C-49b6-BEF0-14DF2A27F6CD}
        /// <summary>
        /// У���ظ�����
        /// </summary>
        /// <returns></returns>
        private int ValidRepeatDept()
        {
            for (int i = 0; i < this.fpEnter1_Sheet1.RowCount; i++)
            {
                if (this.fpEnter1_Sheet1.GetTag(i, 1) == null)
                {
                    MessageBox.Show("��" +( i+1) + "�п��Ҳ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                string lsRow = this.fpEnter1_Sheet1.GetTag(i, 1).ToString();
                string lsRowname = this.fpEnter1_Sheet1.Cells[i, 1].Text;
              
                for (int j = i + 1; j < this.fpEnter1_Sheet1.RowCount ; j++)
                {
                    if (this.fpEnter1_Sheet1.GetTag(j, 1)==null)
                    {
                        MessageBox.Show("��" + (j+1 )+ "�п��Ҳ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                    string lsRowAdd =   this.fpEnter1_Sheet1.GetTag(j, 1).ToString();
                    string lsRowAddName = this.fpEnter1_Sheet1.Cells[j, 1].Text;
                    if (lsRow == lsRowAdd )
                    {
                        MessageBox.Show(this.fpEnter1_Sheet1.GetText(i,1)+"�Ѿ�����","��ʾ", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return -1;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Save()
        {
            this.fpEnter1.StopCellEditing();
            //			if(this.fpEnter1_Sheet1.RowCount==0)
            //			{
            //				MessageBox.Show("�޼�¼,���ܱ���!","��ʾ");
            //				return;
            //			}
            if (this.fpEnter1_Sheet1.Tag == null)
            {
                MessageBox.Show("����ѡ��Һ�Ա!", "��ʾ");
                return;
            }

            //У��//{C6CA49C6-D27C-49b6-BEF0-14DF2A27F6CD}
            if (this.Valid() < 0)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.perMgr.con);
            //t.BeginTransaction();

            this.perMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (this.perMgr.Delete((this.fpEnter1_Sheet1.Tag as FS.FrameWork.Models.NeuObject).ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.perMgr.Err, "��ʾ");
                    return;
                }

                for (int i = 0; i < this.fpEnter1_Sheet1.RowCount; i++)
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    //�Һ�Ա����
                    obj.ID = (this.fpEnter1_Sheet1.Tag as FS.FrameWork.Models.NeuObject).ID;

                    if (this.fpEnter1_Sheet1.GetTag(i, 1) == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��ָ����" + (i+1)+"��".ToString() + "�ĹҺſ���!", "��ʾ");
                        this.fpEnter1.Focus();
                        this.fpEnter1_Sheet1.SetActiveCell(i, 1, false);
                        return;
                    }
                    obj.User01 = this.fpEnter1_Sheet1.GetTag(i, 1).ToString();//�Һſ���
                    obj.User02 = this.perMgr.Operator.ID;//����Ա
                    //{8AB04EE1-0A7B-45f9-A897-8CD01CE29ED1}
                    if (rdOut.Checked)
                    {
                        obj.Memo = "0";
                    }
                    else
                    {
                        obj.Memo = "1";
                    }

                    if (this.perMgr.Insert(obj) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.perMgr.Err, "��ʾ");
                        return;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return;
            }
            MessageBox.Show("����ɹ�!", "��ʾ");
        }


        #region fp
        private int fpEnter1_KeyEnter(Keys key)
        {
            if (key == Keys.Enter)
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == 1)
                {
                    FS.FrameWork.WinForms.Controls.PopUpListBox list = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, 1);
                    if (list == null) return -1;

                    FS.FrameWork.Models.NeuObject obj = null;
                    if (list.GetSelectedItem(out obj) == -1) return -1;

                    if (obj == null) return -1;

                    this.fpEnter1_Sheet1.SetText(this.fpEnter1_Sheet1.ActiveRowIndex, 1, obj.Name);
                    this.fpEnter1_Sheet1.SetTag(this.fpEnter1_Sheet1.ActiveRowIndex, 1, obj.ID);

                    list.Visible = false;
                    this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, 2, false);
                }
            }
            return 0;
        }

        private int fpEnter1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            if (obj == null) return -1;

            this.fpEnter1_Sheet1.SetText(this.fpEnter1_Sheet1.ActiveRowIndex, this.fpEnter1_Sheet1.ActiveColumnIndex,
                obj.Name);
            this.fpEnter1_Sheet1.SetTag(this.fpEnter1_Sheet1.ActiveRowIndex, this.fpEnter1_Sheet1.ActiveColumnIndex,
                obj.ID);

            this.fpEnter1.Focus();
            this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, this.fpEnter1_Sheet1.ActiveColumnIndex);
            return 0;
        }
        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.Add)
            //{
            //    this.Add();
            //    this.fpEnter1.Focus();

            //    return true;
            //}
            //else if (keyData == Keys.Subtract)
            //{
            //    this.Del();
            //    this.fpEnter1.Focus();

            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            //{
            //    this.Save();

            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            //{
            //    this.FindForm().Close();
            //    return true;
            //}

            return base.ProcessDialogKey(keyData);
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.Add();
                    this.fpEnter1.Focus();

                    break;
                case "ɾ��":
                    this.Del();
                    this.fpEnter1.Focus();

                    break;                
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return base.OnSave(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            //{2EFAB4DC-DBD9-45ed-9D09-B3F34856CA2F}
            this.Init();
            base.OnLoad(e);
        }
    }
}
