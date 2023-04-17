using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{

    public partial class frmLocate : Form
    {
        private IceBoxManage iceBoxManage;
        private IceBoxLayerManage layerManage;
        private ShelfManage shelfManage;
        private ArrayList arrIceBoxList;
        private ArrayList arrShelfList;
        private ArrayList arrLayerList;
       
        public string container;
        public static int containerId = 0;
        public static string location = "";
        public delegate void SetContainerId(object sender, EventArgs e);
        public event SetContainerId OnSetContainerId;

        public frmLocate()
        {
            InitializeComponent();
            container = "";
            iceBoxManage = new IceBoxManage();
            layerManage = new IceBoxLayerManage();
            shelfManage = new ShelfManage();
            arrIceBoxList = new ArrayList();
            arrShelfList = new ArrayList();
            arrLayerList = new ArrayList();
        }

        /// <summary>
        /// 获取所有冰箱
        /// </summary>
        private void GetAllIceBox()
        {
            if (arrIceBoxList != null)
            {
                arrIceBoxList.Clear();
            }
            //arrIceBoxList = new ArrayList();
            arrIceBoxList = iceBoxManage.GetAllIceBox();
        }

        /// <summary>
        /// 根据冰箱层ID获取所有架子
        /// </summary>
        /// <param name="layerId"></param>
        private void GetShelfByLayerId(string layerId)
        {
            arrShelfList.Clear();
            //arrShelfList = new ArrayList();
            arrShelfList = shelfManage.GetShelfByLayerID(layerId);
        }

        /// <summary>
        /// 根据冰箱Id获取冰箱层
        /// </summary>
        /// <param name="iceBoxId"></param>
        private void GetLayerByIceBoxId(string iceBoxId)
        {
            arrLayerList.Clear();
            //arrLayerList = new ArrayList();
            arrLayerList = layerManage.GetIceBoxLayers(iceBoxId);
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitLayerTree()
        {
            GetAllIceBox();
            if (arrIceBoxList == null || arrIceBoxList.Count <= 0)
            {
                return;
            }
            this.tvLocate.Font = new Font("宋体", 12);
            //foreach (ColumnHeader ch in nlvSpecContainer.c)
            //{

            //}
            //nlvSpecContainer.Font = new Font ("宋体", 14);
            foreach (IceBox i in arrIceBoxList)
            {
                TreeNode root = new TreeNode();
                root.Tag = i;
                root.Text = i.IceBoxName;
                this.tvLocate.Nodes.Add(root);
                GetLayerByIceBoxId(i.IceBoxId.ToString());
                foreach (IceBoxLayer layer in arrLayerList)
                {
                    TreeNode layerRoot = new TreeNode();
                    layerRoot.Tag = layer;
                    layerRoot.Text = "第 " + layer.LayerNum + " 层";
                    root.Nodes.Add(layerRoot);
                }
            }
        }

        private void InitLayerShelf()
        {
            GetAllIceBox();
            if (arrIceBoxList == null || arrIceBoxList.Count <= 0)
            {
                return;
            }
            this.tvLocate.Font = new Font("宋体", 12);
            //foreach (ColumnHeader ch in nlvSpecContainer.c)
            //{

            //}
            //nlvSpecContainer.Font = new Font ("宋体", 14);
            foreach (IceBox i in arrIceBoxList)
            {
                TreeNode root = new TreeNode();
                root.Tag = i;
                root.Text = i.IceBoxName;
                this.tvLocate.Nodes.Add(root);
                GetLayerByIceBoxId(i.IceBoxId.ToString());
                foreach (IceBoxLayer layer in arrLayerList)
                {
                    TreeNode layerRoot = new TreeNode();
                    layerRoot.Tag = layer;
                    layerRoot.Text = "第 " + layer.LayerNum + " 层";
                    root.Nodes.Add(layerRoot);

                    GetShelfByLayerId(layer.LayerId.ToString());
                    foreach (Shelf shelf in arrShelfList)
                    {
                        TreeNode shelfRoot = new TreeNode();
                        shelfRoot.Tag = shelf;
                        string shelfBarCode = shelf.SpecBarCode;
                        shelfRoot.Text = shelfBarCode;
                        layerRoot.Nodes.Add(shelfRoot);
                    }
                }
            }
        }

        private void frmLocate_Load(object sender, EventArgs e)
        {
            containerId = 0;
            if (container == "s")
            {
                InitLayerShelf();
            }
            if (container == "l")
            {
                InitLayerTree(); 
            }
        }

        private void tvLocate_DoubleClick(object sender, EventArgs e)
        {          
            TreeNode treeNode = tvLocate.SelectedNode;
            if (container == "s")
            {
                if (treeNode.Tag == null || !treeNode.Tag.GetType().ToString().Contains("Shelf"))
                {
                    return;
                }
                Shelf shelf = treeNode.Tag as Shelf;
                containerId = shelf.ShelfID;
                location = treeNode.Parent.Parent.Text + " " + treeNode.Parent.Text+" " + treeNode.Text;
            }
            if (container == "l")
            {
                if (treeNode.Tag == null || !treeNode.Tag.GetType().ToString().Contains("IceBoxLayer"))
                {
                    return;
                }
                IceBoxLayer layer = treeNode.Tag as IceBoxLayer;
                containerId = layer.LayerId;
                location = treeNode.Parent.Text + "  " + treeNode.Text;

            }
            OnSetContainerId(this, new EventArgs());
            this.Close();
        }
    }
}