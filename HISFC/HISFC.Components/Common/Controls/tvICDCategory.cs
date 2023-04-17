using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// ICD������
    /// </summary>
    public partial class tvICDCategory : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public tvICDCategory()
        {
            InitializeComponent();
        }

        public tvICDCategory(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.ImageList = this.imageList1;
            this.AfterSelect += new TreeViewEventHandler(tvICDCategory_AfterSelect);
        }

        public delegate void NodeSelectedHandler(TreeNode node);

        public event NodeSelectedHandler NodeSelected;

        void tvICDCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (NodeSelected != null)
            {
                NodeSelected(e.Node);
            }
        }

        public ArrayList alIcd = new ArrayList();

        private Hashtable hsAllICD = null;

        public Hashtable HsAllICD
        {
            get
            {
                return hsAllICD;
            }
        }

        public void Init()
        {
            this.Nodes.Clear();

            FS.HISFC.BizLogic.HealthRecord.ICDCategory icdCtgManager = new FS.HISFC.BizLogic.HealthRecord.ICDCategory();

            Hashtable hsICDCategory = new Hashtable();

            ArrayList alICDCategory = icdCtgManager.QueryCategoryBySortID("D");

            if (alICDCategory == null || alICDCategory.Count <= 0)
                return;

            foreach (FS.HISFC.Models.HealthRecord.ICDCategory ICDCategory in alICDCategory)
            {
                if (hsICDCategory.Contains(ICDCategory.ParentID))
                {
                    (hsICDCategory[ICDCategory.ParentID] as List<FS.HISFC.Models.HealthRecord.ICDCategory>).Add(ICDCategory);
                }
                else
                {
                    List<FS.HISFC.Models.HealthRecord.ICDCategory> list = new List<FS.HISFC.Models.HealthRecord.ICDCategory>();
                    list.Add(ICDCategory);
                    hsICDCategory.Add(ICDCategory.ParentID, list);
                }
            }

            #region ���ڵ�

            foreach (FS.HISFC.Models.HealthRecord.ICDCategory rootNodeTag in (hsICDCategory[""] as List<FS.HISFC.Models.HealthRecord.ICDCategory>))
            {
                //���ڵ�
                TreeNode rootNode = new TreeNode(rootNodeTag.Name, 0, 1);
                rootNode.Tag = rootNodeTag;
                //һ���ӽڵ�
                foreach (FS.HISFC.Models.HealthRecord.ICDCategory childNodeTag in (hsICDCategory[rootNodeTag.ID] as List<FS.HISFC.Models.HealthRecord.ICDCategory>))
                {
                    TreeNode childNode = new TreeNode(childNodeTag.Name, 4, 5);
                    childNode.Tag = childNodeTag;
                    //�����ӽڵ�
                    if (hsICDCategory[childNodeTag.ID] != null)
                    {
                        foreach (FS.HISFC.Models.HealthRecord.ICDCategory childNodeTag2 in (hsICDCategory[childNodeTag.ID] as List<FS.HISFC.Models.HealthRecord.ICDCategory>))
                        {
                            TreeNode childNode2 = new TreeNode(childNodeTag2.Name, 4, 5);
                            childNode2.Tag = childNodeTag2;

                            //�����ӽڵ�
                            if (hsICDCategory[childNodeTag2.ID] != null)
                            {
                                foreach (FS.HISFC.Models.HealthRecord.ICDCategory childNodeTag3 in (hsICDCategory[childNodeTag2.ID] as List<FS.HISFC.Models.HealthRecord.ICDCategory>))
                                {
                                    TreeNode childNode3 = new TreeNode(childNodeTag3.Name, 4, 5);
                                    childNode3.Tag = childNodeTag3;

                                    //�ļ��ӽڵ�
                                    if (hsICDCategory[childNodeTag3.ID] != null)
                                    {
                                        foreach (FS.HISFC.Models.HealthRecord.ICDCategory childNodeTag4 in (hsICDCategory[childNodeTag3.ID] as List<FS.HISFC.Models.HealthRecord.ICDCategory>))
                                        {
                                            TreeNode childNode4 = new TreeNode(childNodeTag4.Name, 4, 5);
                                            childNode4.Tag = childNodeTag4;

                                            childNode3.Nodes.Add(childNode4);
                                        }
                                    }

                                    childNode2.Nodes.Add(childNode3);
                                }
                            }

                            childNode.Nodes.Add(childNode2);
                        }
                    }
                    rootNode.Nodes.Add(childNode);
                }
                this.Nodes.Add(rootNode);
            }

            this.Nodes[0].Expand();

            #endregion
        }

        public void InitHsICD(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes icdTypes, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes queryType)
        {
            if (hsAllICD != null)
            {
                return;
            }
            else
            {
                hsAllICD = new Hashtable();
            }
            FS.HISFC.BizLogic.HealthRecord.ICD icdManager = new FS.HISFC.BizLogic.HealthRecord.ICD();

            if (this.alIcd == null || this.alIcd.Count <= 0)
            {
                this.alIcd = icdManager.QueryNew(icdTypes, queryType, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);

            }
            if (alIcd == null)
            {
                return;
            }

            if (this.Nodes.Count <= 0)
                return;

            hsAllICD.Add("ALL", new List<FS.HISFC.Models.HealthRecord.ICD>());
            foreach (FS.HISFC.Models.HealthRecord.ICD icd in alIcd)
            {
                (hsAllICD["ALL"] as List<FS.HISFC.Models.HealthRecord.ICD>).Add(icd);
                if (hsAllICD.Contains(icd.Category.ID))
                {
                    (hsAllICD[icd.Category.ID] as List<FS.HISFC.Models.HealthRecord.ICD>).Add(icd);
                }
                else
                {
                    List<FS.HISFC.Models.HealthRecord.ICD> list = new List<FS.HISFC.Models.HealthRecord.ICD>();
                    list.Add(icd);
                    hsAllICD.Add(icd.Category.ID, list);
                }
            }
        }
    }
}
