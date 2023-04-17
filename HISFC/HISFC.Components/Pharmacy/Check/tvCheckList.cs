using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.Check
{
    /// <summary>
    /// [��������: ҩƷ�̵��б�ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class tvCheckList : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvCheckList()
        {
            InitializeComponent();

            this.ImageList = this.deptImageList;
        }

        public tvCheckList(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.ImageList = this.deptImageList;
        }       

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();        

        /// <summary>
        ///��ʾ�̵㵥�б�
        /// </summary>
        public void ShowCheckList(FS.FrameWork.Models.NeuObject checkDept,string checkState,FS.FrameWork.Models.NeuObject checkOper)
        {
            //����б�
            this.Nodes.Clear();

            this.privDept = checkDept;

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

            //���Ӷ��̵㵥״̬Ϊ��ȡ����������桱���̵㵥����ʾ����{DCE7937E-C36F-4d9a-B706-4E80F93BFC8B}sel
            string strCheckState = "����";
            switch (checkState)
            {
                case "0": strCheckState = "����"; break;
                case "1": strCheckState = "���"; break;
                case "2": strCheckState = "ȡ��"; break;
                default: strCheckState = "����";  break;
            }

            //��ǰ���ԶԷ����˵��жϣ�������ʾȫ�������̵㵥            
            try
            {
                List<FS.HISFC.Models.Pharmacy.Check> checkList = new List<FS.HISFC.Models.Pharmacy.Check>();
                checkList = itemManager.QueryCheckList(checkDept.ID,checkState,checkOper.ID);
                if (checkList == null)
                {
                    System.Windows.Forms.MessageBox.Show(Language.Msg(itemManager.Err));
                    return;
                }
                if (checkList.Count == 0)
                {
                    this.Nodes.Add(new System.Windows.Forms.TreeNode("û��" + strCheckState + "�̵㵥", 0, 0));//{DCE7937E-C36F-4d9a-B706-4E80F93BFC8B}
                }
                else
                {
                    this.Nodes.Add(new System.Windows.Forms.TreeNode(strCheckState + "�̵㵥�б�", 0, 0));//{DCE7937E-C36F-4d9a-B706-4E80F93BFC8B}
                    //��ʾ�̵㵥�б�
                    System.Windows.Forms.TreeNode newNode;
                    foreach (FS.HISFC.Models.Pharmacy.Check check in checkList)
                    {
                        newNode = new System.Windows.Forms.TreeNode();

                        //��÷�����Ա����
                        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
                        FS.HISFC.Models.Base.Employee employee = personManager.GetPersonByID(check.FOper.ID);
                        if (employee == null)
                        {
                            System.Windows.Forms.MessageBox.Show(Language.Msg("���" + strCheckState + "��Ա��Ϣʱ������Ա����Ϊ" + check.FOper.ID + "����Ա������"));//{DCE7937E-C36F-4d9a-B706-4E80F93BFC8B}
                            return;
                        }
                        check.FOper.Name = employee.Name;

                        if (check.CheckName == "")
                            newNode.Text = check.CheckNO + "-" + check.FOper.Name;		    //�̵㵥��+������
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
                        
                FS.HISFC.Models.Pharmacy.Check check = e.Node.Tag as FS.HISFC.Models.Pharmacy.Check;

                FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

                check.CheckName = e.Label; 

                if (check.CheckNO != "")
                {
                    if (itemManager.UpdateCheckListName(this.privDept.ID,check.CheckNO,e.Label) == -1)
                    {
                        System.Windows.Forms.MessageBox.Show(Language.Msg("�����̵�ͳ����Ϣ���̵㵥���Ƴ���"));
                        return;
                    }
                }
            }

            base.OnAfterLabelEdit(e);
        }
    }
}
