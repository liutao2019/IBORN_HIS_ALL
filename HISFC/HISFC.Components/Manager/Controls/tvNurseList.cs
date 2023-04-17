using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// 护理站树
    /// </summary>
    public class tvNurseList:FS.FrameWork.WinForms.Controls.NeuTreeView
    {
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;
    
        public tvNurseList()
        {
            this.InitializeComponent();
            InitTree();
        }
        
        /// <summary>
        /// 初始化treeview
        /// </summary>
        public void InitTree()
        {
            this.ImageList = this.imageList1;
             
            TreeNode Selectnode = new TreeNode(); //树的节点 
            this.Nodes.Clear();

            ArrayList arrNurse = new ArrayList();//护士站列表
            
            //arrNurse = FS.HISFC.Models.Fee.FeeCodeStat.List();

            TreeNode tnWard = new TreeNode();
            TreeNode tnRoot = new TreeNode();
            tnRoot.Text = "护士站列表";
            FS.HISFC.BizLogic.Manager.Department cDept = new FS.HISFC.BizLogic.Manager.Department();
            arrNurse = cDept.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.N);
            tnRoot.ImageIndex = 3;
            tnRoot.SelectedImageIndex = 3;

            //药物执行单
            try
            {
                for (int i = 0; i < arrNurse.Count; i++)
                {
                    TreeNode node = new TreeNode(arrNurse[i].ToString());
                    string strNurseID = ((FS.FrameWork.Models.NeuObject)arrNurse[i]).ID.ToString();
                    node.Tag = strNurseID;
                    FS.HISFC.BizLogic.Manager.Bed oCBed = new FS.HISFC.BizLogic.Manager.Bed();

                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 2;
                    ArrayList arrWard = new ArrayList();
                    arrWard = oCBed.GetBedRoom(strNurseID);

                    for (int j = 0; j < arrWard.Count; j++)
                    {
                        tnWard = new TreeNode(arrWard[j].ToString());
                        tnWard.Text = arrWard[j].ToString();
                        tnWard.SelectedImageIndex = 1;
                        tnWard.ImageIndex = 0;
                        node.Nodes.Add(tnWard);
                        if (j == 0 && i == 0)
                        {
                            Selectnode = tnWard;  //保存第一个节点
                        }
                    }
                    tnRoot.Nodes.Add(node);
                }
                this.Nodes.Add(tnRoot);
               // this.ExpandAll(); //展开所有树的节点
                this.SelectedNode = Selectnode; //指定当前选定哪个节点
            }
            catch (Exception ex) { MessageBox.Show(FS.FrameWork.Management.Language.Msg("错误！") + ex.Message); }

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tvNurseList));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "房间16.ico");
            this.imageList1.Images.SetKeyName(1, "房间24.ico");
            this.imageList1.Images.SetKeyName(2, "楼层24.ico");
            this.imageList1.Images.SetKeyName(3, "楼房24.ico");
            this.ResumeLayout(false);

        }
    
        
		
    }
}
