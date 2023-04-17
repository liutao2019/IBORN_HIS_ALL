using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Check
{
    public partial class frmSetFilter : Form
    {
        public frmSetFilter()
        {
            InitializeComponent();
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);
            this.nbtOK.Click += new EventHandler(nbtOK_Click);

            this.neuTreeView1.AfterCheck += new TreeViewEventHandler(neuTreeView1_AfterCheck);
            this.neuTreeView2.AfterCheck += new TreeViewEventHandler(neuTreeView1_AfterCheck);
            this.neuTreeView3.AfterCheck += new TreeViewEventHandler(neuTreeView1_AfterCheck);

        }

        void neuTreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
        }

        void nbtOK_Click(object sender, EventArgs e)
        {
            if (this.ReturnSetting() == 1)
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        void nbtCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.Delegate.CheckFilterSetCompletedHander CheckFilterSetCompletedEvent;

        public int Init()
        {
            this.neuTreeView1.CheckBoxes = true;
            this.neuTreeView2.CheckBoxes = true;
            this.neuTreeView3.CheckBoxes = true;

            //FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //ArrayList al = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);

            SOC.HISFC.BizProcess.Cache.Common.InitDrugType();
            SOC.HISFC.BizProcess.Cache.Common.GetDrugQualityName("--");
            SOC.HISFC.BizProcess.Cache.Common.GetDrugDoseModualName("--");

            TreeNode nodeType = new TreeNode();
            nodeType.Text = "药品类别";
            nodeType.ImageIndex = 0;
            nodeType.SelectedImageIndex = 1;
            nodeType.Tag = "Type";
            this.neuTreeView1.Nodes.Add(nodeType);

            foreach (FS.HISFC.Models.Base.Const c in SOC.HISFC.BizProcess.Cache.Common.drugTypeHelper.ArrayObject)
            {
                TreeNode n = new TreeNode();
                n.Tag = c;
                n.Text = c.Name;
                n.ImageIndex = 0;
                n.SelectedImageIndex = 1;
                nodeType.Nodes.Add(n);
            }

            this.neuTreeView1.ExpandAll();

            //al = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);

            TreeNode nodeQuality = new TreeNode();
            nodeQuality.Text = "药品性质";
            nodeQuality.ImageIndex = 0;
            nodeQuality.SelectedImageIndex = 1;
            nodeQuality.Tag = "Type";
            this.neuTreeView2.Nodes.Add(nodeQuality);

            foreach (FS.HISFC.Models.Base.Const c in SOC.HISFC.BizProcess.Cache.Common.drugQualityHelper.ArrayObject)
            {
                TreeNode n = new TreeNode();
                n.Tag = c;
                n.Text = c.Name;
                n.ImageIndex = 0;
                n.SelectedImageIndex = 1;
                nodeQuality.Nodes.Add(n);
            }

            this.neuTreeView2.ExpandAll();


            //al = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM);




            Hashtable hsDosageForm = new Hashtable();

            foreach (FS.HISFC.Models.Base.Const c in SOC.HISFC.BizProcess.Cache.Common.drugDoseModualHelper.ArrayObject)
            {
                TreeNode n = new TreeNode();
                n.Tag = c;
                n.Text = c.Name;
                n.ImageIndex = 0;
                n.SelectedImageIndex = 1;
                TreeNode nodeDosageForm = new TreeNode();

                if (!hsDosageForm.Contains(c.Memo))
                {
                    nodeDosageForm.Text = "药品剂型";
                    if (!string.IsNullOrEmpty(c.Memo))
                    {
                        nodeDosageForm.Text = "药品剂型-" + c.Memo;
                    }
                    nodeDosageForm.ImageIndex = 0;
                    nodeDosageForm.SelectedImageIndex = 1;
                    nodeDosageForm.Tag = "Type";
                    this.neuTreeView3.Nodes.Add(nodeDosageForm);
                    hsDosageForm.Add(c.Memo, nodeDosageForm);
                }
                else
                {
                    nodeDosageForm = hsDosageForm[c.Memo] as TreeNode;
                }
                nodeDosageForm.Nodes.Add(n);
            }
            if (this.neuTreeView3.Nodes.Count == 1)
            {
                this.neuTreeView3.ExpandAll();
            }
            return 1;
        }

        private bool CheckTypeSelected(string type, string typeNO)
        {
            ArrayList alNode = new ArrayList();
            if (type == "0")
            {
                alNode.AddRange(this.neuTreeView1.Nodes[0].Nodes);
            }
            else if (type == "1")
            {
                alNode.AddRange(this.neuTreeView2.Nodes[0].Nodes);
            }
            else if (type == "2")
            {
                foreach (TreeNode node in this.neuTreeView3.Nodes)
                {
                    alNode.AddRange(node.Nodes);
                }
            }
            foreach (TreeNode node in alNode)
            {
                if (node.Checked)
                {
                    FS.HISFC.Models.Base.Const c = node.Tag as FS.HISFC.Models.Base.Const;
                    if (c.ID == typeNO)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        private int ReturnSetting()
        {
            ArrayList alDrugType = new ArrayList();
            foreach(TreeNode node in this.neuTreeView1.Nodes[0].Nodes)
            {
                if (node.Checked)
                {
                    alDrugType.Add(node.Tag);
                }
            }

            ArrayList alDrugQuality = new ArrayList();
            foreach (TreeNode node in this.neuTreeView2.Nodes[0].Nodes)
            {
                if (node.Checked)
                {
                    alDrugQuality.Add(node.Tag);
                }
            }

            ArrayList alDrugDosage = new ArrayList();
            foreach (TreeNode node in this.neuTreeView3.Nodes)
            {
                foreach (TreeNode n in node.Nodes)
                {
                    if (n.Checked)
                    {
                        alDrugDosage.Add(n.Tag);
                    }
                }
            }

            SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.CheckFilterSetting checkFilterSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.CheckFilterSetting();
            checkFilterSetting.AlDrugType = alDrugType;
            checkFilterSetting.AlDrugQuality = alDrugQuality;
            checkFilterSetting.AlDrugDosage = alDrugDosage;
            checkFilterSetting.StartPlaceNO = this.ntxtStartPlaceNO.Text;
            checkFilterSetting.EndPlaceNO = this.ntxtEndPlaceNO.Text;
            checkFilterSetting.StartCustomNO = this.ntxtStartCustomNO.Text;
            checkFilterSetting.EndCustomNO = this.ntxtEndCustomNO.Text;
            
            if (this.CheckFilterSetCompletedEvent != null)
            {
                this.CheckFilterSetCompletedEvent(checkFilterSetting);
            }
            return 1;
        }

    }
}
