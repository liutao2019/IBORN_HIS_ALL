using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.Components.Material.Check
{
    public partial class tvCheckList : FS.FrameWork.WinForms.Controls.NeuTreeView
    {
        public tvCheckList()
        {
            InitializeComponent();
        }

        public tvCheckList(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        ///��ʾ�̵㵥�б�
        /// </summary>
        public void ShowCheckList(FS.FrameWork.Models.NeuObject checkDept, string checkState, FS.FrameWork.Models.NeuObject checkOper)
        {
            //����б�
            this.Nodes.Clear();

            this.privDept = checkDept;

            FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();

            //��ǰ���ԶԷ����˵��жϣ�������ʾȫ�������̵㵥            
            try
            {
                ArrayList checkList = new ArrayList();
                checkList = itemManager.QueryCheckStatic(checkDept.ID, checkState);
                if (checkList == null)
                {
                    System.Windows.Forms.MessageBox.Show(Language.Msg(itemManager.Err));
                    return;
                }
                if (checkList.Count == 0)
                {
                    this.Nodes.Add(new System.Windows.Forms.TreeNode("û�з����̵㵥", 0, 0));
                }
                else
                {
                    this.Nodes.Add(new System.Windows.Forms.TreeNode("�����̵㵥�б�", 0, 0));
                    //��ʾ�̵㵥�б�
                    System.Windows.Forms.TreeNode newNode;
                    foreach (FS.HISFC.Models.Material.Check check in checkList)
                    {
                        newNode = new System.Windows.Forms.TreeNode();

                        //��÷�����Ա����
                        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
                        FS.HISFC.Models.Base.Employee employee = personManager.GetPersonByID(check.FOper.ID);
                        if (employee == null)
                        {
                            System.Windows.Forms.MessageBox.Show(Language.Msg("��÷�����Ա��Ϣʱ������Ա����Ϊ" + check.FOper.ID + "����Ա������"));
                            return;
                        }
                        check.FOper.Name = employee.Name;

                        if (check.CheckName == "")
                            newNode.Text = check.CheckCode + "-" + check.FOper.Name;		    //�̵㵥��+������
                        else
                            newNode.Text = check.CheckName;

                        newNode.ImageIndex = 4;
                        newNode.SelectedImageIndex = 5;

                        newNode.Tag = check;

                        this.Nodes[0].Nodes.Add(newNode);
                    }
                    this.Nodes[0].ExpandAll();

                    this.SelectedNode = this.Nodes[0];
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg(ex.Message));
            }
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.TreeNode node = this.GetNodeAt(e.X, e.Y);

            if (node != null)
            {
                System.Windows.Forms.ContextMenu popMenu = new System.Windows.Forms.ContextMenu();

                System.Windows.Forms.MenuItem reNameMenu = new System.Windows.Forms.MenuItem("�޸�����");
                reNameMenu.Click += new EventHandler(reNameMenu_Click);

                popMenu.MenuItems.Add(reNameMenu);

                this.ContextMenu = popMenu;

                this.SelectedNode = node;
            }

            base.OnMouseDown(e);
        }

        private void reNameMenu_Click(object sender, EventArgs e)
        {
            if (this.SelectedNode != null && this.SelectedNode.Parent != null)
            {
                this.LabelEdit = true;
                if (!this.SelectedNode.IsEditing)
                    this.SelectedNode.BeginEdit();
            }
        }

        protected override void OnAfterLabelEdit(System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@', '.', ',', '!' }) == -1)
                    {
                        e.Node.EndEdit(false);
                    }
                    else
                    {
                        e.CancelEdit = true;
                        System.Windows.Forms.MessageBox.Show(Language.Msg("������Ч�ַ�!����������"));
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    e.CancelEdit = true;
                    System.Windows.Forms.MessageBox.Show(Language.Msg("ģ�����Ʋ���Ϊ��"));
                    e.Node.BeginEdit();
                }

                FS.HISFC.Models.Material.Check check = e.Node.Tag as FS.HISFC.Models.Material.Check;

                //FS.HISFC.BizLogic.Material.Store itemManager = new FS.HISFC.BizLogic.Material.Store();

                //check.CheckName = e.Label;

                //if (check.CheckNO != "")
                //{
                //    if (itemManager.UpdateCheckListName(this.privDept.ID, check.CheckNO, e.Label) == -1)
                //    {
                //        System.Windows.Forms.MessageBox.Show(Language.Msg("�����̵�ͳ����Ϣ���̵㵥���Ƴ���"));
                //        return;
                //    }
                //}
            }

            base.OnAfterLabelEdit(e);
        }
    }
}
