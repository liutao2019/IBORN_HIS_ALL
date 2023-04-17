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
    /// [功能描述: 单据补打列表树]<br></br>
    /// [创 建 者: 梁俊泽]<br></br>
    /// [创建时间: 2006-12]<br></br>
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
        /// 初始化
        /// </summary>
        public void Init()
        {
           
            NodeType[] nodeTypes = new NodeType[] {
                new NodeType("入库单","0310") ,
                new NodeType("出库单","0320") , 
            };
            this.ImageList = this.groupImageList;

            this.Nodes.Clear();

            FS.FrameWork.Models.NeuObject tempObject;
            //按权限初始化
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> alPriv = null;

            #region 父结点科室，子结点单据
            System.Collections.Hashtable hsPrivDept = new Hashtable();
            foreach (NodeType nodeType in nodeTypes)
            {
                
                alPriv = privManager.QueryUserPriv(privManager.Operator.ID, nodeType.PrivType);
                if (alPriv == null)
                {
                    System.Windows.Forms.MessageBox.Show(Language.Msg("获取权限" + nodeType.PrivType + "发生错误" + privManager.Err));
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
                System.Windows.Forms.TreeNode noPrivNode = new System.Windows.Forms.TreeNode("无权限");
                noPrivNode.Tag = null;
                this.Nodes.Add(noPrivNode);
            }

            this.ExpandAll();
        }

        /// <summary>
        /// 单据节点构造
        /// </summary>
        private struct NodeType
        {

            /// <summary>
            /// 构造单据节点[默认所有库房科室]
            /// </summary>
            /// <param name="name">单据名称</param>
            /// <param name="privType">二级权限</param>
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
            /// 构造单据节点
            /// </summary>
            /// <param name="name">单据名称</param>
            /// <param name="privType">二级权限</param>
            /// <param name="deptType">库房类型[All全部库房科室]</param>
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
            /// 单据名称
            /// </summary>
            public string Name;
            /// <summary>
            /// 权限类型[二级权限]
            /// </summary>
            public string PrivType;
            /// <summary>
            /// 科室类型[PI药库 P药房 ALL全部库房]
            /// </summary>
            public string DeptType;

            public string BillType;
        }
    }
}
