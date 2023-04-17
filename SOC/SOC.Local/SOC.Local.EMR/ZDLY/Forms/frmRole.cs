using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace Neusoft.SOC.Local.EMR.ZDLY.Forms
{
    public partial class frmRole : Form
    {
        public frmRole()
        {
            InitializeComponent();
        }
        Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.RoleInfoLogic clsrole= new Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.RoleInfoLogic();

        private void frmRole_Load(object sender, EventArgs e)
        {
            chkedit.DisplayValueChecked = "1";
            chkedit.DisplayValueUnchecked = "0";
            chkedit.ValueChecked = (decimal)1;
            chkedit.ValueUnchecked = (decimal)0;
            chkedit.Click += new EventHandler(chkedit_Click);
            this.treeListColumn2.ColumnEdit = chkedit;
            InitRole();
            InitTemplate();
            this.WindowState = FormWindowState.Maximized;
        }

        decimal intcheck = 0;
        /// <summary>
        /// 左侧病历树选中控件
        /// </summary>
        DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkedit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
        /// <summary>
        /// 角色对应的权限表
        /// </summary>
        DataTable dtpriveleges =new DataTable ();

        #region 选中树节点
        void chkedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tlTemplate.FocusedNode.ParentNode == null)
                {
                    if (!this.tlTemplate.FocusedNode.Checked)
                    {
                        intcheck = 1;
                        this.tlTemplate.FocusedNode.Checked = true;
                        this.tlTemplate.FocusedNode.SetValue(1, 1);
                    }
                    else
                    {
                        intcheck = 0;
                        this.tlTemplate.FocusedNode.Checked = false;
                        this.tlTemplate.FocusedNode.SetValue(1, 0);
                    }
                    foreach (TreeListNode node in this.tlTemplate.FocusedNode.Nodes)
                    {
                        node.SetValue(1, intcheck);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 初始化树节点
        private void InitTemplate()
        {
            this.tlTemplate.DataSource = clsrole.GetTemplateTree();
        }
        #endregion

        #region 获取有效角色
        private void InitRole()
        {
            DataTable dtrole = new DataTable();
            dtrole = clsrole.GetUseRole();
            this.lueRole.Properties.DataSource = dtrole.DefaultView;
            this.lueRole.Properties.DisplayMember = "NAME";
            this.lueRole.Properties.ValueMember = "ID";
        }
        #endregion

        #region 选择角色，初始化角色对应的权限
        private void lueRole_EditValueChanged(object sender, EventArgs e)
        {
            dtpriveleges = clsrole.GetRolePrivileges(this.lueRole.EditValue.ToString());
            this.gcRole.DataSource = dtpriveleges.DefaultView;
        }
        #endregion

        #region 全选
        /// <summary>
        /// 全选方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckeAll1_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chk = sender as DevExpress.XtraEditors.CheckEdit;
            if (chk != null)
            {
                Control ctl = chk.Parent;
                foreach (Control check in ctl.Controls)
                {
                    if (check.GetType() == typeof(DevExpress.XtraEditors.CheckEdit))
                    {
                        DevExpress.XtraEditors.CheckEdit chk1 = check as DevExpress.XtraEditors.CheckEdit;
                        chk1.Checked = chk.Checked;
                    }
                }
            }
        }
        #endregion

        #region 启动过滤
        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            
            if (!chkFilter.Checked)
            {
                dtpriveleges.DefaultView.RowFilter = "1=1";
                this.gcRole.DataSource = dtpriveleges.DefaultView;
                return;
            }
            FilterOrAdd("Filter");

        }
        #endregion

        #region 过滤与新增权限公用方法
        private void FilterOrAdd(string strAddOrFilter)
        {
            if (this.dtpriveleges.Rows.Count == 0)
            {
                return;
            }

            #region 过滤用变量
            string pname = "",    //患者来源过滤变量
                 pstatus = "",     //病历状态过滤变量
                 pprivilege = "",  //操作权限过滤变量
                 middle = "",        //中间变量
                 treenodes = "";   //病历节点过滤变量
            string strFilters = " 1=1"; //过滤条件变量
            #endregion

            #region 新增权限用变量
            List<string> lstnodes = new List<string>();
            List<string> lstpname = new List<string>();
            List<string> lstpstatus = new List<string>();
            List<string> lstprivileges = new List<string>();
            List<string> lstmiddle =null;
            #endregion

            #region 获取以上三个过滤条件
            foreach (Control group in this.pnlGroup.Controls)
            {
                middle = "";
                lstmiddle = new List<string>(); //数组需要初始化指针
                if (group.GetType() == typeof(DevExpress.XtraEditors.GroupControl))
                {
                    foreach (Control check in group.Controls)
                    {
                        DevExpress.XtraEditors.CheckEdit chk1 = check as DevExpress.XtraEditors.CheckEdit;
                        if (chk1.Checked && chk1.Tag != null)
                        {
                            middle = middle + "'" + chk1.Tag.ToString() + "',";
                            lstmiddle.Add(chk1.Tag.ToString());
                        }
                    }
                    switch (group.Name)
                    {
                        case "grouppatient":
                            {
                                pname = middle;
                                if (pname.EndsWith(","))
                                { 
                                    strFilters = strFilters + " and PCODE in (" + pname.TrimEnd(',') + ")";
                                    lstpname=lstmiddle;
                                }
                            }
                            break;
                        case "groupstatus":
                            {
                                pstatus = middle;
                                if (pstatus.EndsWith(","))
                                { 
                                    strFilters = strFilters + " and RSCODE in (" + pstatus.TrimEnd(',') + ")";
                                    lstpstatus = lstmiddle;
                                }

                            }
                            break;
                        default:
                            {
                                pprivilege = middle;
                                if (pprivilege.EndsWith(","))
                                { 
                                    strFilters = strFilters + " and ROCODE in (" + pprivilege.TrimEnd(',') + ")";
                                    lstprivileges = lstmiddle;
                                }

                            }
                            break;
                    }
                }

            }
            #endregion

            #region 遍历树节点，获取选中节点
            foreach (TreeListNode node1 in this.tlTemplate.Nodes)
            {
                if (node1.HasChildren)
                {
                    foreach (TreeListNode node2 in node1.Nodes)
                    {
                        if (node2.GetValue("FLAG").ToString() == "1")
                        {
                            treenodes = treenodes + "'" + node2.GetValue("VALUE").ToString() + "',";
                            lstnodes.Add(node2.GetValue("VALUE").ToString());
                        }
                    }
                }
            }
            #endregion

            #region 过滤用
            if (strAddOrFilter == "Filter")
            {
                if (treenodes.EndsWith(","))
                {
                    strFilters = strFilters + " and TRCODE in (" + treenodes.TrimEnd(',') + ")";
                }
                if (!strFilters.EndsWith("1"))
                {
                    dtpriveleges.DefaultView.RowFilter = strFilters;
                    this.gcRole.DataSource = dtpriveleges.DefaultView;
                }
            }
            #endregion

            if (strAddOrFilter == "Filter")
            {
                return;
            }

            #region 生成新增权限表
            DataTable dtnew = new DataTable("priv");
            dtnew.Columns.Add("PCODE");
            dtnew.Columns.Add("TRCODE");
            dtnew.Columns.Add("RSCODE");
            dtnew.Columns.Add("ROCODE");

            if (strAddOrFilter == "Add")
            {
                string filter = "";
                for (int i1 = 0; i1 < lstnodes.Count; i1++)
                {
                    for (int i2 = 0; i2 < lstpname.Count;i2++)
                    {
                        for (int i3 = 0; i3 < lstpstatus.Count; i3++)
                        {
                            for (int i4 = 0; i4 < lstprivileges.Count; i4++)
                            {
                                filter = "TRCODE='" + lstnodes[i1] + "' and PCODE='" +
                                    lstpname[i2] + "' and RSCODE='" + lstpstatus[i3] + "' and ROCODE='" + lstprivileges[i4] + "'";
                                DataRow[] dr= dtpriveleges.Select(filter);
                                if (dr.Length == 0)
                                {
                                    dtnew.Rows.Add(new object[]{ lstpname[i2],lstnodes[i1],lstpstatus[i3],lstprivileges[i4]});
                                }
                            }
                        }
                    }

                }
            }
            #endregion

            if (dtnew.Rows.Count == 0)
            {
                return;
            }
            if (clsrole.InsertPriveleges(dtnew, Convert.ToInt32(this.lueRole.EditValue)) == 1)
            {
                MessageBox.Show("添加权限成功");
                lueRole_EditValueChanged(new object(), new EventArgs());
                chkFilter_CheckedChanged(new object(), new EventArgs());
            }
            else
            {

                MessageBox.Show("添加权限失败");
            }
        }
        #endregion

        #region 新增按钮
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.lueRole.Text == "")
            {
                MessageBox.Show("请选择角色");
                return;
            }
            FilterOrAdd("Add");
        }
        #endregion

        #region 更新选中权限状态
        private void btnDelele_Click(object sender, EventArgs e)
        {
            int[] rowid = this.gvRole.GetSelectedRows();
            if (rowid.Length == 0)
            {
                return;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("status");
            for (int i = 0; i < rowid.Length; i++)
            {
                dt.Rows.Add(new object[] { this.gvRole.GetRowCellValue(rowid[i], "ID").ToString(), this.gvRole.GetRowCellValue(rowid[i], "FLAG") });

            }
            try
            {
                if (clsrole.UpdatePrivelegeStatus(dt) == 1)
                {
                    MessageBox.Show("更新成功");
                    lueRole_EditValueChanged(new object(), new EventArgs());
                    chkFilter_CheckedChanged(new object(), new EventArgs());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region 展开树,合拢树
        private void lblTree_Click(object sender, EventArgs e)
        {
            if (this.lblTree.Text.EndsWith("展开树"))
            {
                this.tlTemplate.ExpandAll();
                this.lblTree.Text = "         合拢树";
            }
            else
            {
                this.tlTemplate.CollapseAll();
                this.lblTree.Text = "         展开树";
            }
        }
        #endregion

        //以下两个方法是第2个tabpage页用的。

        #region 角色维护、人员维护切换页
        private void tabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.tabControl.SelectedTabPageIndex == 1)
            {
                if (this.tabpageEmployee.Controls.Count < 1)
                {
                    ucEmployeeRole ucEmployee = new ucEmployeeRole();
                    this.tabpageEmployee.Controls.Add(ucEmployee);
                    ucEmployee.Dock = DockStyle.Fill;
                }

            }
        }
        #endregion

        #region 为第二2个tabpage页提供方法
        public DataTable AddEmployeePerm(DataTable dtEmployeeHas, string roleid,string empid)
        {
            if (this.lueRole.EditValue == null || this.lueRole.EditValue.ToString() != roleid)
            {
                dtpriveleges = clsrole.GetRolePrivileges(roleid);
                this.gcRole.DataSource = dtpriveleges.DefaultView;
            }

            #region 过滤用变量
            string middle = "",        //中间变量
                   treenodes = "";   //病历节点过滤变量
            #endregion

            List<string> lstnodes = new List<string>();
            List<string> lstpname = new List<string>();
            List<string> lstpstatus = new List<string>();
            List<string> lstprivileges = new List<string>();
            List<string> lstmiddle = null;

            #region 获取以上三个过滤条件
            foreach (Control group in this.pnlGroup.Controls)
            {
                middle = "";
                lstmiddle = new List<string>(); //数组需要初始化指针
                if (group.GetType() == typeof(DevExpress.XtraEditors.GroupControl))
                {
                    foreach (Control check in group.Controls)
                    {
                        DevExpress.XtraEditors.CheckEdit chk1 = check as DevExpress.XtraEditors.CheckEdit;
                        if (chk1.Checked && chk1.Tag != null)
                        {
                            middle = middle + "'" + chk1.Tag.ToString() + "',";
                            lstmiddle.Add(chk1.Tag.ToString());
                        }
                    }
                    switch (group.Name)
                    {
                        case "grouppatient":
                            {
                                if (middle.EndsWith(","))
                                {
                                    lstpname = lstmiddle;
                                }
                            }
                            break;
                        case "groupstatus":
                            {
                                if (middle.EndsWith(","))
                                {
                                    lstpstatus = lstmiddle;
                                }

                            }
                            break;
                        default:
                            {
                                if (middle.EndsWith(","))
                                {
                                    lstprivileges = lstmiddle;
                                }

                            }
                            break;
                    }
                }

            }
            #endregion

            #region 遍历树节点，获取选中节点
            foreach (TreeListNode node1 in this.tlTemplate.Nodes)
            {
                if (node1.HasChildren)
                {
                    foreach (TreeListNode node2 in node1.Nodes)
                    {
                        if (node2.GetValue("FLAG").ToString() == "1")
                        {
                            treenodes = treenodes + "'" + node2.GetValue("VALUE").ToString() + "',";
                            lstnodes.Add(node2.GetValue("VALUE").ToString());
                        }
                    }
                }
            }
            #endregion

            DataTable dtnew = new DataTable("priv");
            dtnew.Columns.Add("PCODE");
            dtnew.Columns.Add("TRCODE");
            dtnew.Columns.Add("RSCODE");
            dtnew.Columns.Add("ROCODE");
            string filter = "";
            for (int i1 = 0; i1 < lstnodes.Count; i1++)
            {
                for (int i2 = 0; i2 < lstpname.Count; i2++)
                {
                    for (int i3 = 0; i3 < lstpstatus.Count; i3++)
                    {
                        for (int i4 = 0; i4 < lstprivileges.Count; i4++)
                        {
                            filter = "TRCODE='" + lstnodes[i1] + "' and PCODE='" +
                                lstpname[i2] + "' and RSCODE='" + lstpstatus[i3] + "' and ROCODE='" + lstprivileges[i4] + "'";
                            DataRow[] dr = dtpriveleges.Select(filter);
                            DataRow[] dr2 = dtEmployeeHas.Select(filter + " and EID=" + empid);

                            if (dr.Length == 0 && dr2.Length == 0)
                            {
                                dtnew.Rows.Add(new object[] { lstpname[i2], lstnodes[i1], lstpstatus[i3], lstprivileges[i4] });
                            }
                        }
                    }
                }

            }
            return dtnew;
        }
        #endregion



    }
}