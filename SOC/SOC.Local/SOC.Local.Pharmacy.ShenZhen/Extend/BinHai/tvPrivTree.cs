using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Extend.BinHai
{
    /// <summary>
    /// [��������: ���ݲ����б���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// </summary>
    public partial class tvPrivTree : FS.SOC.HISFC.Components.Common.Base.baseTreeView
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


        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
           
            NodeType[] nodeTypes = new NodeType[] {
                new NodeType("��ⵥ","0310") ,
                new NodeType("���ⵥ","0320") , 
            };
            this.ImageList = this.groupImageList;

            this.Nodes.Clear();

            FS.FrameWork.Models.NeuObject tempObject;
            //��Ȩ�޳�ʼ��
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> alPriv = null;

            #region �������ң��ӽ�㵥��
            System.Collections.Hashtable hsPrivDept = new Hashtable();
            foreach (NodeType nodeType in nodeTypes)
            {
                
                alPriv = privManager.QueryUserPriv(privManager.Operator.ID, nodeType.PrivType);
                if (alPriv == null)
                {
                    System.Windows.Forms.MessageBox.Show(Language.Msg("��ȡȨ��" + nodeType.PrivType + "��������" + privManager.Err));
                    return;
                }
                foreach (FS.FrameWork.Models.NeuObject obj in alPriv)
                {
                    tempObject = new FS.FrameWork.Models.NeuObject();
                    tempObject.Name = nodeType.Name;
                    tempObject.ID = nodeType.BillType;
                    tempObject.Memo = "Bill";

                    System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
                    node.Text = tempObject.Name;
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 1;

                    node.Tag = tempObject;

                    System.Windows.Forms.TreeNode parentNode;
                    if (hsPrivDept.Contains(obj.ID))
                    {
                        parentNode = ((System.Windows.Forms.TreeNode)hsPrivDept[obj.ID]);
                        parentNode.Nodes.Add(node);
                    }
                    else
                    {
                        parentNode = new System.Windows.Forms.TreeNode();
                        parentNode.Text = obj.Name;
                        parentNode.ImageIndex = 2;
                        parentNode.SelectedImageIndex = 2;

                        parentNode.Tag = obj;
                        this.Nodes.Add(parentNode);
                        parentNode.Nodes.Add(node);
                        hsPrivDept.Add(obj.ID, parentNode);
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

        /// <summary>
        /// ���ݽڵ㹹��
        /// </summary>
        private struct NodeType
        {

            /// <summary>
            /// ���쵥�ݽڵ�[Ĭ�����пⷿ����]
            /// </summary>
            /// <param name="name">��������</param>
            /// <param name="privType">����Ȩ��</param>
            public NodeType(string name, string privType)
            {
                Name = name;
                PrivType = privType;
                DeptType = "ALL";
                if (privType == "0310")
                {
                    BillType = "I";
                }
                else if (privType == "0320")
                {
                    BillType = "O";
                }

                else BillType = "NO";
                {
                }

            }

            /// <summary>
            /// ���쵥�ݽڵ�
            /// </summary>
            /// <param name="name">��������</param>
            /// <param name="privType">����Ȩ��</param>
            /// <param name="deptType">�ⷿ����[Allȫ���ⷿ����]</param>
            public NodeType(string name, string privType, string deptType)
            {
                Name = name;
                PrivType = privType;
                DeptType = deptType;
                if (privType == "0310")
                {
                    BillType = "I";
                }
                else if (privType == "0320")
                {
                    BillType = "D";
                }

                else
                {
                    BillType = "NO";
                }

            }
            /// <summary>
            /// ��������
            /// </summary>
            public string Name;
            /// <summary>
            /// Ȩ������[����Ȩ��]
            /// </summary>
            public string PrivType;
            /// <summary>
            /// ��������[PIҩ�� Pҩ�� ALLȫ���ⷿ]
            /// </summary>
            public string DeptType;

            public string BillType;
        }
    }
}
