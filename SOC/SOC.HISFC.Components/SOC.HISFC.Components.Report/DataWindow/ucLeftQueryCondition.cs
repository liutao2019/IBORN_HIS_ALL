using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.HISFC.Components.Report.DataWindow
{
    public partial class ucLeftQueryCondition : FS.FrameWork.WinForms.Controls.ucBaseControl,ICommonReportController.ILeftQueryCondition
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

        public void AddControls(List<CommonReportQueryInfo> list)
        {
             if (list == null)
            {
                return;
            }
             NeuTabControl tbControl = new NeuTabControl();

             foreach (CommonReportQueryInfo common in list)
             {
                 TabPage tp = new TabPage();
                 tp.Name = "tp" + common.Name;
                 tp.Text = common.Text;
                 tbControl.TabPages.Add(tp);
                 tbControl.Dock = DockStyle.Fill;
                 
                 if (common.ControlType is TreeViewType||common.ControlType is FilterTreeView)//正常的文本框
                 {
                     TreeViewType treeViewType = common.ControlType as TreeViewType;
                     NeuTreeView treeView = new NeuTreeView();
                     treeView.Name = common.Name;
                     treeView.TabIndex = common.Index;
                     treeView.CheckBoxes = treeViewType.IsCheckBox;

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

                     if (common.ControlType is FilterTreeView)
                     {
                         FilterTreeView filterTreeView = common.ControlType as FilterTreeView;
                         treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
                         filterDictionary.Add(common.Name, filterTreeView.FilterStr);
                     }

                     treeView.Dock = DockStyle.Fill;
                     treeView.ExpandAll();
                     tp.Controls.Add(treeView);
                 }

             }

             this.Controls.Add(tbControl);
        }

        void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
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

        public object GetValue(CommonReportQueryInfo common)
        {
            Control[] controls = this.Controls.Find(common.Name, true);
            if (controls != null && controls.Length > 0)
            {
                if (common.ControlType is TreeViewType||common.ControlType is FilterTreeView)
                {
                    NeuTreeView treeView = controls[0] as NeuTreeView;
                    TreeViewType treeViewType = common.ControlType as TreeViewType;
                    if (treeViewType.IsCheckBox)
                    {
                        if (treeViewType.IsAddAll && treeView.Nodes[0].Checked)
                        {
                            return treeView.Nodes[0].Name;

                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (TreeNode node in treeView.Nodes[0].Nodes)
                            {
                                sb.Append("'" + node.Name + "',");
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
                    }
                    else
                    {
                        return treeView.SelectedNode.Name;
                    }
                }
            }

            return "";
        }

        public event ICommonReportController.FilterHandler OnFilterHandler;

        #endregion
    }
}
