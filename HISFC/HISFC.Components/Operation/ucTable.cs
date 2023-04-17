using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ����̨ά��]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-16]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucTable : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucTable()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
            {
                this.Init();
                this.LoadRooms();

            }
        }

        #region �ֶ�

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        private frmOperationRoom room;
        private List<string> modified = new List<string>();
        #endregion

        #region ����
        /// <summary>
        /// ��ǰ������
        /// </summary>
        private OpsRoom CurrentRoom
        {
            get
            {
                return this.treeView1.SelectedNode.Tag as OpsRoom;
            }
        }

        private frmOperationRoom DialogRoom
        {
            get
            {
                if (this.room == null)
                    this.room = new frmOperationRoom();

                return this.room;
            }
        }
        #endregion

        #region ����

        private void Init()
        {
            this.imageList1.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.F�ֽ�));
            this.imageList1.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B����));
        }

        /// <summary>
        /// װ��������
        /// </summary>
        private void LoadRooms()
        {
            treeView1.Nodes.Clear();
            //��ʼ���������б�
            TreeNode root = new TreeNode(Environment.OperatorDept.Name, 0, 0);
            treeView1.Nodes.Add(root);

            ArrayList rooms = Environment.TableManager.GetRoomsByDept(Environment.OperatorDeptID);
            if (rooms != null)
            {
                foreach (OpsRoom room in rooms)
                {
                    //��������̨
                    ArrayList tables = Environment.TableManager.GetOpsTable(room.ID);
                    foreach (OpsTable table in tables)
                    {
                        room.AddTable(table);
                    }

                    TreeNode node = new TreeNode(room.Name, 1, 1);
                    node.Tag = room;
                    root.Nodes.Add(node);
                }
                treeView1.ExpandAll();
            }
        }

        private int Save()
        {
            this.ucTableSpread1.Update();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();
            Environment.TableManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.ucTableSpread1.ValidState() == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            foreach (string id in this.modified)
            {
                foreach (TreeNode node in this.treeView1.Nodes[0].Nodes)
                {
                    OpsRoom room = node.Tag as OpsRoom;
                    if (room.ID == id)
                    {
                        foreach (OpsTable table in room.Tables)
                        {
                            if (!table.IsOK())
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(table.InvalidInfo + ",��ȷ�Ϻ����ԣ�"
                                    , "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return -1;
                            }
                        }
                        if (Environment.TableManager.DelOpsTables(id) == -1)
                        {

                        }

                        if (Environment.TableManager.AddOpsTable(room.Tables) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��������̨ʧ�ܣ�\n����ϵͳ����Ա��ϵ��" + Environment.TableManager.Err
                                , "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                    }
                }
            }

            this.modified.Clear();
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ɹ�");
            return 0;
        }
        #endregion

        #region �¼�
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.CurrentRoom == null)
            {
                this.ucTableSpread1.OperationRoom = null;
                return;
            }
            this.ucTableSpread1.Reset();

            this.ucTableSpread1.OperationRoom = this.CurrentRoom;
            this.ucTableSpread1.AddItem(this.CurrentRoom.Tables);

        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {

            this.ucTableSpread1.Update();
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton( "����", "����", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null );
            this.toolBarService.AddToolButton("ɾ��", "ɾ��", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            this.toolBarService.AddToolButton( "-", "-", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null );
            this.toolBarService.AddToolButton("���ӷ���", "���ӷ���", FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("ɾ������", "ɾ������", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                if (this.CurrentRoom == null)
                {
                    MessageBox.Show("����ѡ���������䣡");
                    return;
                }
                this.ucTableSpread1.AddItem();
            }
            else if (e.ClickedItem.Text == "ɾ��")
            {
                if (CurrentRoom == null)
                {
                    MessageBox.Show( "��ѡ����������" );
                    return;
                }

                this.ucTableSpread1.DeleteItem();

                if (!this.modified.Contains( this.CurrentRoom.ID ))
                {
                    this.modified.Add( this.CurrentRoom.ID );
                }
            }
            else if (e.ClickedItem.Text == "���ӷ���")
            {

                this.DialogRoom.Room = new OpsRoom();
                this.DialogRoom.IsNew = true;

                if (this.DialogRoom.ShowDialog() == DialogResult.OK)
                {
                    OpsRoom room = this.DialogRoom.Room.Clone();

                    this.ucTableSpread1.Reset();
                    TreeNode node = new TreeNode(room.Name, 1, 1);
                    node.Tag = room;
                    this.treeView1.Nodes[0].Nodes.Add(node);
                    this.modified.Add(room.ID);

                }
            }
            else if (e.ClickedItem.Text == "ɾ������")
            {
                if (this.CurrentRoom == null)
                    return;

                if (MessageBox.Show("ɾ�������佫ͬʱɾ�������������̨,�Ƿ�ȷ��ɾ��������:" + this.CurrentRoom.Name + "?",
                           "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                if (Environment.TableManager.DelOpsRoom(this.CurrentRoom) == -1)
                {
                    MessageBox.Show("ɾ��������ʧ�ܣ�\n����ϵͳ����Ա��ϵ��" + Environment.TableManager.Err, "��ʾ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.treeView1.Nodes.Remove(this.treeView1.SelectedNode);
            }
            base.ToolStrip_ItemClicked(sender, e);
        }


        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.CurrentRoom == null)
                return;

            this.DialogRoom.IsNew = false;

            this.DialogRoom.Room = this.CurrentRoom.Clone();
            if (this.DialogRoom.ShowDialog() == DialogResult.OK)
            {
                this.CurrentRoom.Name = this.DialogRoom.Room.Name;
                e.Node.Text = this.DialogRoom.Room.Name;
                this.CurrentRoom.InputCode = this.DialogRoom.Room.InputCode;
                this.CurrentRoom.IsValid = this.DialogRoom.Room.IsValid;

            }
        }

        private void ucTableSpread1_ItemModified(object sender, EventArgs e)
        {
            if (!this.modified.Contains(this.CurrentRoom.ID))
                this.modified.Add(this.CurrentRoom.ID);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        #endregion

    }
}
