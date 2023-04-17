using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Controls;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.ControlType;
using FS.HISFC.Components.Common.Controls;
using System.Collections;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Implement
{
    public partial class ucLeftQueryCondition : FS.FrameWork.WinForms.Controls.ucBaseControl,ILeftQueryCondition
    {
        private Dictionary<string, string> filterDictionary = new Dictionary<string, string>();

        public ucLeftQueryCondition()
        {
            InitializeComponent();
        }

        #region ITopQueryCondition 成员

        public int Init()
        {
            this.Controls.Clear();
            filterDictionary.Clear();

            return 1;
        }

        public void AddControls(List<QueryControl> list)
        {
            if (list == null)
            {
                return;
            }
            NeuTabControl tbControl = new NeuTabControl();

            foreach (QueryControl common in list)
            {
                TabPage tp = new TabPage();
                tp.Name = "tp" + common.Name;
                tp.Text = common.Text;
                tbControl.TabPages.Add(tp);
                tbControl.Dock = DockStyle.Fill;

                baseTreeView treeView = new baseTreeView();
                if (common.ControlType is TreeViewType || common.ControlType is FilterTreeView)//正常的文本框
                {
                    #region TreeViewType
                    TreeViewType treeViewType = common.ControlType as TreeViewType;
                    treeView.Name = common.Name;
                    treeView.TabIndex = common.Index;
                    treeView.CheckBoxes = treeViewType.IsCheckBox;
                    treeView.ImageList = treeView.groupImageList;
                    treeView.SelectedImageIndex = 2;
                    treeView.ImageIndex = 4;

                    if (treeViewType.DataSource != null)
                    {
                        TreeNode[] listNode = new TreeNode[treeViewType.DataSource.Count];
                        for (int i = 0; i < listNode.Length; i++)
                        {
                            FS.FrameWork.Models.NeuObject obj = treeViewType.DataSource[i] as FS.FrameWork.Models.NeuObject;
                            TreeNode node = new TreeNode();
                            node.Tag = obj;
                            node.Name = obj.ID;
                            node.Text = obj.Name;
                            listNode[i] = node;
                        }

                        if (treeViewType.IsAddAll)
                        {
                            TreeNode parentNode = new TreeNode();
                            parentNode.Name = treeViewType.AllValue.ID;
                            parentNode.Text = treeViewType.AllValue.Name;
                            parentNode.Tag = treeViewType;
                            parentNode.Nodes.AddRange(listNode);
                            treeView.Nodes.Add(parentNode);
                        }
                        else
                        {
                            treeView.Nodes.AddRange(listNode);
                        }
                    }

                    if (treeViewType.IsAddAll)
                    {
                        //加载事件
                        treeView.AfterCheck += new TreeViewEventHandler(treeView_AfterCheck);
                    }
                    treeView.ExpandAll();
                    #endregion
                }
                else if (common.ControlType is GroupTreeViewType)//正常的文本框
                {
                    #region GroupTreeViewType
                    GroupTreeViewType treeViewType = common.ControlType as GroupTreeViewType;
                    treeView.Name = common.Name;
                    treeView.TabIndex = common.Index;
                    treeView.CheckBoxes = treeViewType.IsCheckBox;
                    treeView.ImageList = treeView.groupImageList;
                    treeView.SelectedImageIndex = 2;
                    treeView.ImageIndex = 4;

                    TreeNodeCollection parentNodeCollection = null;
                    if (treeViewType.IsAddAll)
                    {
                        TreeNode parentNode = new TreeNode();
                        parentNode.Name = treeViewType.AllValue.ID;
                        parentNode.Text = treeViewType.AllValue.Name;
                        parentNode.Tag = treeViewType;
                        treeView.Nodes.Add(parentNode);
                        parentNodeCollection = parentNode.Nodes;
                    }
                    else
                    {
                        parentNodeCollection = treeView.Nodes;
                    }

                    if (treeViewType.DataSource != null)
                    {
                        TreeNode[] listNode = new TreeNode[treeViewType.DataSource.Count];
                        foreach (KeyValuePair<FS.FrameWork.Models.NeuObject, ArrayList> keyValue in treeViewType.DataSource)
                        {
                            TreeNode node = new TreeNode();
                            node.Tag = keyValue.Key;
                            node.Text = keyValue.Key.Name;
                            node.Name = keyValue.Key.ID;

                            TreeNode[] listChildNode = new TreeNode[keyValue.Value.Count];
                            for (int i = 0; i < keyValue.Value.Count; i++)
                            {
                                FS.HISFC.Models.Base.Spell s = keyValue.Value[i] as FS.HISFC.Models.Base.Spell;
                                TreeNode childNode = new TreeNode();
                                childNode.Tag = s;
                                childNode.Name = s.ID;
                                childNode.Text = s.Name;
                                listChildNode[i] = childNode;
                            }
                            node.Nodes.AddRange(listChildNode);

                            parentNodeCollection.Add(node);
                        }

                    }

                    if (treeViewType.IsAddAll)
                    {
                        //加载事件
                        treeView.AfterCheck += new TreeViewEventHandler(treeView_AfterCheck);
                        treeView.Nodes[0].Expand();
                    }

                    #endregion
                }

                treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);

                if (common.ControlType is FilterTreeView)
                {
                    FilterTreeView filterTreeView = common.ControlType as FilterTreeView;
                    filterDictionary.Add(common.Name, filterTreeView.FilterStr);
                }

                treeView.Dock = DockStyle.Fill;

                //treeView.Sort();

                if (treeView.Nodes.Count > 0)
                {
                    treeView.Nodes[0].Checked = true;
                    this.treeView_AfterSelect(null, new TreeViewEventArgs(treeView.Nodes[0]));
                }

                tp.Controls.Add(treeView);
            }


            this.Controls.Add(tbControl);
        }

        void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if ( e.Node.TreeView.CheckBoxes)//说明是全部选中
            {
                foreach (System.Windows.Forms.TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }

            if (e.Node.Checked)
            {
                e.Node.ImageIndex = 3;
            }
            else
            {
                e.Node.ImageIndex = 4;
            }
        }

        void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (e.Node.TreeView.CheckBoxes)
            //{
            //    e.Node.Checked = !e.Node.Checked;
            //}

            if (this.OnFilterHandler != null)
            {
                string filter = "";
                //取数据
                foreach (KeyValuePair<string, string> item in filterDictionary)
                {
                    //找控件
                    Control[] c = this.Controls.Find(item.Key, true);
                    if (c != null && c.Length >= 0)
                    {
                        if (c[0] is NeuTreeView)
                        {
                            //去掉无效字符
                            filter += " " + string.Format(item.Value, FS.FrameWork.Public.String.TakeOffSpecialChar(((NeuTreeView)c[0]).SelectedNode.Name));
                        }
                    }
                }

                this.OnFilterHandler(filter);
            }
        }

        public object GetValue(QueryControl common)
        {

            Control[] controls = this.Controls.Find(common.Name, true);
            TreeNodeCollection treeNodeCollection = null;
            StringBuilder sb = new StringBuilder();

            if (controls != null && controls.Length > 0)
            {
                if (common.ControlType is TreeViewType||common.ControlType is FilterTreeView)
                {
                    #region TreeViewType

                    NeuTreeView treeView = controls[0] as NeuTreeView;
                    if (treeView.Nodes.Count <= 0)
                    {
                        return string.Empty;
                    }

                    TreeViewType treeViewType = common.ControlType as TreeViewType;
                    if (treeViewType.IsCheckBox)
                    {
                        if (treeViewType.IsAddAll && treeView.Nodes[0].Checked)
                        {
                            return treeView.Nodes[0].Name;
                        }
                        else
                        {
                            treeNodeCollection = treeView.Nodes.Count > 1 ? treeView.Nodes : treeView.Nodes[0].Nodes;
                            foreach (TreeNode node in treeNodeCollection)
                            {
                                if (node.Checked)
                                {
                                    sb.Append("'" + node.Name + "',");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (treeView.SelectedNode != null)
                        {
                            return treeView.SelectedNode.Name;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    #endregion
                }
                else if (common.ControlType is GroupTreeViewType)
                {

                    #region GroupTreeViewType

                    NeuTreeView treeView = controls[0] as NeuTreeView;

                    if (treeView.Nodes.Count <= 0)
                    {
                        return string.Empty;
                    }

                    GroupTreeViewType treeViewType = common.ControlType as GroupTreeViewType;
                    if (treeViewType.IsCheckBox)
                    {
                        if (treeViewType.IsAddAll && treeView.Nodes[0].Checked)
                        {
                            return treeView.Nodes[0].Name;
                        }
                        else
                        {
                            treeNodeCollection = treeView.Nodes.Count > 1 ? treeView.Nodes : treeView.Nodes[0].Nodes;
                            foreach (TreeNode node in treeNodeCollection)
                            {
                                foreach (TreeNode childNode in node.Nodes)
                                {
                                    if (childNode.Checked)
                                    {
                                        sb.Append("'" + childNode.Name + "',");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (treeView.SelectedNode != null)
                        {
                            return treeView.SelectedNode.Name;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    #endregion
                }
            }

            string retStr = sb.ToString();
            if (retStr.StartsWith("'"))
            {
                sb = sb.Remove(0, 1);
            }

            if (retStr.EndsWith("',"))
            {
                sb = sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }
        
        public object GetText(QueryControl common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {
                if (common.ControlType is TreeViewType || common.ControlType is FilterTreeView)
                {
                    NeuTreeView treeView = controls[0] as NeuTreeView;
                    TreeViewType treeViewType = common.ControlType as TreeViewType;
                    if (treeViewType.IsCheckBox)
                    {
                        if (treeViewType.IsAddAll && treeView.Nodes[0].Checked)
                        {
                            return treeView.Nodes[0].Text;
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            if (treeView.Nodes.Count > 1)
                            {
                                foreach (TreeNode node in treeView.Nodes)
                                {
                                    if (node.Checked)
                                    {
                                        sb.Append(node.Text + ",");
                                    }
                                }
                            }
                            else
                            {
                                foreach (TreeNode node in treeView.Nodes[0].Nodes)
                                {
                                    if (node.Checked)
                                    {
                                        sb.Append(node.Text + ",");
                                    }
                                }
                            }


                            string retStr = sb.ToString();

                            if (retStr.EndsWith(","))
                            {
                                sb = sb.Remove(sb.Length - 1, 1);
                            }
                            return sb.ToString();
                        }
                    }
                    else
                    {
                        if (treeView.SelectedNode != null)
                        {
                            return treeView.SelectedNode.Name;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }

            return "";
        }

        public object GetValues(QueryControl common)
        {

            Control[] controls = this.Controls.Find(common.Name, true);
            StringBuilder sb = new StringBuilder();
            TreeNodeCollection treeNodeCollection = null;
            if (controls != null && controls.Length > 0)
            {
                if (common.ControlType is TreeViewType || common.ControlType is FilterTreeView)
                {
                    #region TreeViewType
                    NeuTreeView treeView = controls[0] as NeuTreeView;

                    if (treeView.Nodes.Count <= 0)
                    {
                        return string.Empty;
                    }

                    TreeViewType treeViewType = common.ControlType as TreeViewType;
                    if (treeView.CheckBoxes)
                    {
                        treeNodeCollection = treeViewType.IsAddAll ? treeView.Nodes[0].Nodes : treeView.Nodes;
                        foreach (TreeNode node in treeNodeCollection)
                        {
                            if (node.Checked)
                            {
                                sb.Append("'" + node.Name + "',");
                            }
                        }
                    }
                    else
                    {
                        if (treeViewType.IsAddAll)
                        {
                            if (treeView.SelectedNode != null && treeView.SelectedNode.Nodes.Count == 0)
                            {
                                return treeView.SelectedNode.Name;
                            }
                            else
                            {
                                treeNodeCollection = treeView.SelectedNode == null ? treeView.Nodes[0].Nodes : treeView.SelectedNode.Nodes;
                                foreach (TreeNode node in treeNodeCollection)
                                {
                                    sb.Append("'" + node.Name + "',");
                                }
                            }
                        }
                        else
                        {
                            return treeView.SelectedNode == null ? string.Empty : treeView.SelectedNode.Name;
                        }
                    }
                    #endregion
                }
                else if (common.ControlType is GroupTreeViewType)
                {
                    #region GroupTreeViewType
                    NeuTreeView treeView = controls[0] as NeuTreeView;
                    if (treeView.Nodes.Count <= 0)
                    {
                        return string.Empty;
                    }

                    GroupTreeViewType treeViewType = common.ControlType as GroupTreeViewType;
                    if (treeView.CheckBoxes)
                    {
                        treeNodeCollection = treeViewType.IsAddAll ? treeView.Nodes[0].Nodes : treeView.Nodes;

                        foreach (TreeNode node in treeNodeCollection)
                        {
                            foreach (TreeNode childNode in node.Nodes)
                            {
                                if (childNode.Checked)
                                {
                                    sb.Append("'" + childNode.Name + "',");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (treeViewType.IsAddAll)
                        {
                            if (treeView.SelectedNode != null && treeView.SelectedNode.Nodes.Count == 0)
                            {
                                return treeView.SelectedNode.Name;
                            }
                            else
                            {
                                treeNodeCollection = treeView.SelectedNode == null ? treeView.Nodes[0].Nodes : treeView.SelectedNode.Nodes;
                                foreach (TreeNode node in treeNodeCollection)
                                {
                                    foreach (TreeNode childNode in node.Nodes)
                                    {
                                        sb.Append("'" + childNode.Name + "',");
                                    }
                                }
                            }
                        }
                        else
                        {
                            return treeView.SelectedNode == null ? string.Empty : treeView.SelectedNode.Name;
                        }
                    }
                    #endregion
                }
            }

            string retStr = sb.ToString();
            if (retStr.StartsWith("'"))
            {
                sb = sb.Remove(0, 1);
            }

            if (retStr.EndsWith("',"))
            {
                sb = sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }

        public object GetTexts(QueryControl common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {
                if (common.ControlType is TreeViewType || common.ControlType is FilterTreeView)
                {
                    NeuTreeView treeView = controls[0] as NeuTreeView;
                    TreeViewType treeViewType = common.ControlType as TreeViewType;
                    if (treeViewType.IsAddAll)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (TreeNode node in treeView.Nodes[0].Nodes)
                        {
                            if (node.Checked==false&&treeViewType.IsCheckBox)
                            {
                                continue;
                            }
                            sb.Append(node.Text + ",");
                        }

                        string retStr = sb.ToString();

                        if (retStr.EndsWith(","))
                        {
                            sb = sb.Remove(sb.Length - 1, 1);
                        }
                        return sb.ToString();
                    }
                    else
                    {
                        if (treeView.SelectedNode != null)
                        {
                            return treeView.SelectedNode.Name;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }

            return "";

        }

        public event ICommonReportController.FilterHandler OnFilterHandler;

        #endregion
    }
}
