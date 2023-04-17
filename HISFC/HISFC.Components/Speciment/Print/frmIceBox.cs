using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.Print
{
    public partial class frmIceBox : Form
    {
        public frmIceBox()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitTree()
        {
            this.tvIceBox.Nodes.Clear();
            FS.HISFC.BizLogic.Speciment.IceBoxManage iceBoxManage = new FS.HISFC.BizLogic.Speciment.IceBoxManage();
            System.Collections.ArrayList arrIceBoxList = new System.Collections.ArrayList();
            //arrIceBoxList = new ArrayList();
            arrIceBoxList = iceBoxManage.GetAllIceBox();       
            
            if (arrIceBoxList == null || arrIceBoxList.Count <= 0)
            {
                return;
            }
            this.tvIceBox.Font = new Font("宋体", 12);
          
            foreach (IceBox i in arrIceBoxList)
            {
                if (i.IceBoxTypeId == "2")
                {
                    continue;
                }
                TreeNode root = new TreeNode();
                root.Tag = i.IceBoxId;
                root.Text = i.IceBoxName ;
                this.tvIceBox.Nodes.Add(root);
            }
            tvIceBox.CollapseAll();
        }

        private void Location(bool print)
        {
            if (print)
            {

            }

        }

        private void frmIceBox_Load(object sender, EventArgs e)
        {
            this.InitTree();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TreeNode tn in tvIceBox.Nodes)
                {
                    if (tn.Checked)
                    {
                        ucIceBoxPrint ucPrint = new ucIceBoxPrint();
                        ucPrint.IceBoxId = tn.Tag.ToString();
                        ucPrint.Print();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (TreeNode tn in tvIceBox.Nodes)
                {
                    if (tn.Checked)
                    {
                        ucIceBoxPrint ucPrint = new ucIceBoxPrint();
                        ucPrint.IceBoxId = tn.Tag.ToString();
                        ucPrint.Export();
                    }
                }
            }
            catch
            {
            }
        }
    }
}