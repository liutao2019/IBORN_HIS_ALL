using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Pharmacy.Report
{
    /// <summary>
    /// [��������: ���ݲ����б���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class tvPrivTree : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvPrivTree()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
        }

        public tvPrivTree(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
        }

        #region �����

        /// <summary>
        /// ��ⵥ�ڵ�����
        /// </summary>
        private string inBillName = "��ⵥ";

        /// <summary>
        /// ���ⵥ�ڵ�����
        /// </summary>
        private string outBillName = "���ⵥ";

        /// <summary>
        /// �������ڵ�����
        /// </summary>
        private string attempBillName = "������";

        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        private bool isShowAttempBill = true;

        #endregion

        #region ����

        /// <summary>
        /// ��ⵥ�ڵ�����
        /// </summary>
        public string InBillName
        {
            get
            {
                return inBillName;
            }
            set
            {
                inBillName = value;
            }
        }

        /// <summary>
        /// ���ⵥ�ڵ�����
        /// </summary>
        public string OutBillName
        {
            get
            {
                return outBillName;
            }
            set
            {
                outBillName = value;
            }
        }

        /// <summary>
        /// �������ڵ�����
        /// </summary>
        public string AttempBillName
        {
            get
            {
                return attempBillName;
            }
            set
            {
                attempBillName = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        public bool IsShowAttempBill
        {
            get
            {
                return isShowAttempBill;
            }
            set
            {
                isShowAttempBill = value;

                this.Init();
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            this.ImageList = this.groupImageList;

            this.Nodes.Clear();

            FS.FrameWork.Models.NeuObject tempObject;
            //��Ȩ�޳�ʼ��
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> alPriv = null;

            #region ��ʾ���ڵ�

            alPriv = privManager.QueryUserPriv(privManager.Operator.ID, "0310");
            if (alPriv == null)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("��ȡ���Ȩ�޷�������" + privManager.Err));
                return;
            }
            if (alPriv.Count > 0)
            {
                tempObject = new FS.FrameWork.Models.NeuObject();
                tempObject.Name = "��ⵥ";
                tempObject.ID = "I";
                tempObject.Memo = "Bill";
                
                System.Windows.Forms.TreeNode inParentNode = new System.Windows.Forms.TreeNode();
                inParentNode.Text = tempObject.Name;

                inParentNode.ImageIndex = 0;
                inParentNode.SelectedImageIndex = 0;

                inParentNode.Tag = tempObject;
                this.Nodes.Add(inParentNode);
                foreach (FS.FrameWork.Models.NeuObject obj in alPriv)
                {
                    //{32C46091-AE5F-44b0-BE40-4CF31D307C7C}  �ſ���ҩ��������
                    if (obj.Memo == "PI" || obj.Memo == "P")
                    {
                        System.Windows.Forms.TreeNode nod = new System.Windows.Forms.TreeNode();
                        nod.Text = obj.Name;

                        nod.ImageIndex = 2;
                        nod.SelectedImageIndex = 4;

                        nod.Tag = obj;
                        inParentNode.Nodes.Add(nod);
                    }
                }
            }

            #endregion

            #region ��ʾ����ڵ�

            alPriv = privManager.QueryUserPriv(privManager.Operator.ID, "0320");
            if (alPriv == null)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("���س���Ȩ���б�������" + privManager.Err));
                return;
            }
            if (alPriv.Count > 0)
            {
                tempObject = new FS.FrameWork.Models.NeuObject();
                tempObject.Name = "���ⵥ";
                tempObject.ID = "O";
                tempObject.Memo = "Bill";                

                if (this.isShowAttempBill)
                {
                    #region ��ʾ������

                    System.Windows.Forms.TreeNode outParentNode = new System.Windows.Forms.TreeNode();
                    outParentNode.Text = tempObject.Name;

                    outParentNode.ImageIndex = 0;
                    outParentNode.SelectedImageIndex = 0;

                    outParentNode.Tag = tempObject;
                    this.Nodes.Add(outParentNode);
                    foreach (FS.FrameWork.Models.NeuObject obj in alPriv)
                    {
                        if (obj.Memo == "PI")
                        {
                            System.Windows.Forms.TreeNode nod = new System.Windows.Forms.TreeNode();
                            nod.Text = obj.Name;

                            nod.ImageIndex = 2;
                            nod.SelectedImageIndex = 4;

                            nod.Tag = obj;
                            outParentNode.Nodes.Add(nod);
                        }
                    }
                    tempObject = new FS.FrameWork.Models.NeuObject();
                    tempObject.Name = "������";
                    tempObject.ID = "D";
                    tempObject.Memo = "Bill";                    

                    System.Windows.Forms.TreeNode attempNode = new System.Windows.Forms.TreeNode();
                    attempNode.Text = tempObject.Name;

                    attempNode.ImageIndex = 0;
                    attempNode.SelectedImageIndex = 0;

                    attempNode.Tag = tempObject;
                    this.Nodes.Add(attempNode);
                    foreach (FS.FrameWork.Models.NeuObject obj in alPriv)
                    {
                        if (obj.Memo == "P")
                        {
                            System.Windows.Forms.TreeNode nod = new System.Windows.Forms.TreeNode();
                            nod.Text = obj.Name;

                            nod.ImageIndex = 2;
                            nod.SelectedImageIndex = 4;

                            nod.Tag = obj;
                            attempNode.Nodes.Add(nod);
                        }
                    }

                    #endregion
                }
                else
                {
                    System.Windows.Forms.TreeNode outParentNode = new System.Windows.Forms.TreeNode();

                    outParentNode.Text = tempObject.Name;
                    outParentNode.Tag = tempObject;

                    this.Nodes.Add(outParentNode);
                    foreach (FS.FrameWork.Models.NeuObject obj in alPriv)
                    {
                        if (obj.Memo == "PI" || obj.Memo == "P")
                        {
                            System.Windows.Forms.TreeNode nod = new System.Windows.Forms.TreeNode();
                            nod.Text = obj.Name;
                            nod.Tag = obj;

                            nod.ImageIndex = 2;
                            nod.SelectedImageIndex = 4;

                            outParentNode.Nodes.Add(nod);
                        }
                    }
                }
            }

            #endregion

            if (this.Nodes.Count == 0)
            {
                System.Windows.Forms.TreeNode noPrivNode = new System.Windows.Forms.TreeNode("��Ȩ��");
                noPrivNode.Tag = null;
                this.Nodes.Add(noPrivNode);
            }

            this.ExpandAll();
        }
    }
}
