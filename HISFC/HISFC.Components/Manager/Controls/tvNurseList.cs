using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// ����վ��
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
        /// ��ʼ��treeview
        /// </summary>
        public void InitTree()
        {
            this.ImageList = this.imageList1;
             
            TreeNode Selectnode = new TreeNode(); //���Ľڵ� 
            this.Nodes.Clear();

            ArrayList arrNurse = new ArrayList();//��ʿվ�б�
            
            //arrNurse = FS.HISFC.Models.Fee.FeeCodeStat.List();

            TreeNode tnWard = new TreeNode();
            TreeNode tnRoot = new TreeNode();
            tnRoot.Text = "��ʿվ�б�";
            FS.HISFC.BizLogic.Manager.Department cDept = new FS.HISFC.BizLogic.Manager.Department();
            arrNurse = cDept.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.N);
            tnRoot.ImageIndex = 3;
            tnRoot.SelectedImageIndex = 3;

            //ҩ��ִ�е�
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
                            Selectnode = tnWard;  //�����һ���ڵ�
                        }
                    }
                    tnRoot.Nodes.Add(node);
                }
                this.Nodes.Add(tnRoot);
               // this.ExpandAll(); //չ���������Ľڵ�
                this.SelectedNode = Selectnode; //ָ����ǰѡ���ĸ��ڵ�
            }
            catch (Exception ex) { MessageBox.Show(FS.FrameWork.Management.Language.Msg("����") + ex.Message); }

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
            this.imageList1.Images.SetKeyName(0, "����16.ico");
            this.imageList1.Images.SetKeyName(1, "����24.ico");
            this.imageList1.Images.SetKeyName(2, "¥��24.ico");
            this.imageList1.Images.SetKeyName(3, "¥��24.ico");
            this.ResumeLayout(false);

        }
    
        
		
    }
}
