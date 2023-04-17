using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Neusoft.SOC.Local.EMR.ZDLY.Forms
{
    public partial class ucEmployeeRole : UserControl
    {
        public ucEmployeeRole()
        {
            InitializeComponent();
        }
        Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.EmployeeInfoLogic clsemployee = new Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.EmployeeInfoLogic();
        Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.DeptInfoLogic clsdept = new Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic.DeptInfoLogic();

        DataTable dtdept = new DataTable();
        DataTable dtemployee = new DataTable();
        DataTable dtaddrole = new DataTable();
        DataTable dtaddperm = new DataTable();

        private void ucEmployeeRole_Load(object sender, EventArgs e)
        {
           
            InitEmployee(); 
            InitDept();
            InitAddRole();
            InitAddPerm();
        }

        #region 初始化人员额外权限
        private void InitAddPerm()
        {
            dtaddperm = clsemployee.GetEmplPerm();
            dtaddperm.DefaultView.RowFilter = "1<>1";
            this.gcAdd.DataSource = dtaddperm.DefaultView;
        }
        #endregion

        #region 初始化人员额外角色
        private void InitAddRole()
        {

            dtaddrole = clsemployee.GetEmplRole();
        }
        #endregion

        #region 初始化人员
        private void InitEmployee()
        {

            dtemployee = clsemployee.GetEmployee();
            this.gcEmployee.DataSource = dtemployee.DefaultView;
        }
        #endregion

        #region 初始化科室
        string defaultfilter = "TYPE ='I'";
        private void InitDept()
        {

            //string sql = 

            dtdept = clsdept.GetDept();
            dtdept.DefaultView.RowFilter = defaultfilter;
            this.gcDept.DataSource = dtdept.DefaultView;
        }
        #endregion

        #region 科室选中改变方法
        private void gvDept_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (this.gvDept.FocusedRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    int i = this.gvEmployee.FocusedRowHandle;
                    this.dtemployee.DefaultView.RowFilter = "DID=" + this.gvDept.GetFocusedRowCellValue("ID").ToString();
                    this.gcEmployee.DataSource = this.dtemployee.DefaultView;
                    if (i == this.gvEmployee.FocusedRowHandle)
                    {
                        RefreshAddData();
                    }
                }
                else
                {
                    this.dtemployee.DefaultView.RowFilter = "1=1";
                    this.gcEmployee.DataSource = this.dtemployee.DefaultView;
                }
                this.chkAllEmployee.Checked = false;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 人员选中改变
        private void gvEmployee_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            RefreshAddData();
        }
        private void RefreshAddData()
        {
            try
            {
                if (this.gvEmployee.FocusedRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    if (this.dtaddperm.Rows.Count == 0)
                    {
                        return;
                    }
                    string id = this.gvEmployee.GetFocusedRowCellValue("ID").ToString();
                    this.dtaddperm.DefaultView.RowFilter = "EID=" + id;
                    this.dtaddrole.DefaultView.RowFilter = "EMPL_ID=" + id;
                    this.listBoxControl1.Items.Clear();
                    if (this.dtaddrole.DefaultView.Count > 0)
                    {
                        for (int i = 0; i < this.dtaddrole.DefaultView.Count; i++)
                        {
                            this.listBoxControl1.Items.Add(this.dtaddrole.Select("ID=" + this.dtaddrole.Select("EMPL_ID=" + id)[i]["ID"])[0]["NAME"] + "," + this.dtaddrole.Select("EMPL_ID=" + id)[i]["ID"]);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 科室全选
        private void chkAllDept_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAllDept.Checked)
            {
                this.defaultfilter = "1=1";
                dtdept.DefaultView.RowFilter = this.defaultfilter;
            }
            else
            {
                this.defaultfilter = "TYPE ='I'";
                dtdept.DefaultView.RowFilter = this.defaultfilter;
            }
        }
        #endregion

        #region 人员全选
        private void chkAllEmployee_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAllEmployee.Checked)
            {
                this.dtemployee.DefaultView.RowFilter = "1=1";
            }
            else
            {
                this.dtemployee.DefaultView.RowFilter = "DID=" + this.gvDept.GetFocusedRowCellValue("ID").ToString();
                this.gcEmployee.DataSource = this.dtemployee.DefaultView;
            }
        }
        #endregion

        #region 为人员添加额外角色或权限
        private void btnAddRole_Click(object sender, EventArgs e)
        {
            if (this.gvEmployee.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                return;
            }
            frmRole frm = FindRoleForm( this) as frmRole;
            ctlret = null;
           
            #region 处理角色
            if (frm.lueRole.Text == "" && this.rgpDealWith.SelectedIndex==0)
            {
                XtraMessageBox.Show("请选中要添加的角色");
            }
            else if(this.rgpDealWith.SelectedIndex==0)
            {

                if (this.gvEmployee.GetFocusedRowCellValue("RNAME").ToString() != frm.lueRole.Text)
                {
                    if (this.dtaddrole.Select("EMPL_ID=" + this.gvEmployee.GetFocusedRowCellValue("ID") + " and ROLE_ID=" + frm.lueRole.EditValue).Length == 0)
                    {
                        //this.listBoxControl1.Items.Add(frm.lueRole.Text);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    XtraMessageBox.Show("该用户已经拥有该角色---"+frm.lueRole.Text);
                    return;
                }
                int empid=Convert.ToInt32(this.gvEmployee.GetFocusedRowCellValue("ID"));
                int roleid=Convert.ToInt32(frm.lueRole.EditValue);
                int seqid = -1;
                if (clsemployee.InsertEmplRole(empid, roleid, ref seqid) == 1)
                {
                    XtraMessageBox.Show("添加角色成功");
                    this.dtaddrole.Rows.Add(new object[] { frm.lueRole.Text, seqid, empid, roleid, "Add" });
                    this.listBoxControl1.Items.Add(frm.lueRole.Text+","+seqid.ToString());
                }
                else
                {
                    XtraMessageBox.Show("添加角色失败");
                }
            }
            #endregion

            #region 处理权限
            if (this.rgpDealWith.SelectedIndex == 1)
            {
                string roleid=this.gvEmployee.GetFocusedRowCellValue("RID").ToString();
                int empid=Convert.ToInt32(this.gvEmployee.GetFocusedRowCellValue("ID"));
                DataTable dtnew = frm.AddEmployeePerm(dtaddperm,roleid,empid.ToString());
                frm.lueRole.EditValue = this.gvEmployee.GetFocusedRowCellValue("RID");
                if (dtnew.Rows.Count == 0)
                {
                    XtraMessageBox.Show("该用户已经拥有权限---");
                }
                else
                {
                    
                    dtnew = clsemployee.InsertEmplPerm(dtnew, empid);  //添加权限，并且该权限存在于其他角色中。dtnew是需要添加的权限表，
                    bool isSuccess = false;
                    if (dtnew==null || dtnew.Rows.Count == 0)
                    {
                        isSuccess = true;
                    }
                    if (!isSuccess) //b表示有添加的权限不存在其他角色中
                    {
                        if (clsemployee.InsertEmplPermNew(dtnew, empid) == 1)
                        {
                            isSuccess = true;
                        }
                    }
                    if (isSuccess)
                    {
                        InitAddPerm();
                        gvEmployee_FocusedRowChanged(null, null);
                        XtraMessageBox.Show("添加权限成功");//添加权限，并且该权限不存在其他角色中。
                    }
                    else
                    {
                        XtraMessageBox.Show("添加权限失败");
                    }

                }
            }
            #endregion

        }
        #endregion 

        #region 删除人员额外角色或权限
        private void btnDeleteAdd_Click(object sender, EventArgs e)
        {
            //角色处理
            if(this.listBoxControl1.SelectedItem!=null &&this.rgpDealWith.SelectedIndex==0)
            {
                DataRow[] drs=this.dtaddrole.Select("ID='" + this.listBoxControl1.SelectedItem.ToString().Split(',')[1] + "'");
                if (drs.Length==1)
                {
                    int roleid = Convert.ToInt32(drs[0]["ID"]);
                    if (XtraMessageBox.Show("确定要删除选中的角色吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                    if (clsemployee.DeleteEmplRole(roleid) == 1)
                    {
                        DataRow dr = this.dtaddrole.Select("ID=" + roleid)[0];
                        this.dtaddrole.Rows.Remove(dr);
                        XtraMessageBox.Show("删除角色成功");
                        this.listBoxControl1.Items.Remove(this.listBoxControl1.SelectedItem);
                    }
                    else
                    {
                        XtraMessageBox.Show("删除角色失败");
                    }
                }
                
            }
            //权限处理
            if (this.rgpDealWith.SelectedIndex == 1 && this.gvAdd.FocusedRowHandle > -1)
            {
                string str = "(-1,";
                foreach (int i in this.gvAdd.GetSelectedRows())
                {
                    str=str+this.gvAdd.GetRowCellValue(i,"ADDID").ToString()+",";
                }
                if (!str.EndsWith("(-1,"))
                {
                    str = str.TrimEnd(',') + ")";
                    if (XtraMessageBox.Show("确定要删除选中的权限吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                    if (clsemployee.DeleteEmplPerm(str) == 1)
                    {
                        XtraMessageBox.Show("删除权限成功");
                        this.gvAdd.DeleteSelectedRows();
                    }
                    else
                    {
                        XtraMessageBox.Show("删除权限失败");
                    }
                }

            }
        }
        #endregion

        #region 查找RoleForm
        Control ctlret = null;
        private Control FindRoleForm(Control ctl)
        {
            if (ctl.Parent != null)
            {
                if (ctl.Parent.GetType() == typeof(frmRole))
                {
                    ctlret = ctl.Parent;
                }
                else
                {
                    FindRoleForm(ctl.Parent);

                }
            }
            else
            {
                if (ctl.GetType() == typeof(frmRole))
                {
                    ctlret = ctl.Parent;
                }
                return ctlret;
            }
            return ctlret;
        }
        #endregion

        #region 科室地位功能
        private void emrFindText1_TextChanged(object sender, EventArgs e)
        {
            string strColValue = "";
            if (this.emrFindText1.Text != this.emrFindText1.HintStr && this.emrFindText1.Text.Trim() != "")
            {
                if (!chkAllDept.Checked)
                {
                    dtdept.DefaultView.RowFilter = "TYPE ='I' and (PY like '%" + this.emrFindText1.Text + "%'" + " or NAME  like '%" + this.emrFindText1.Text + "%' )";
                }
                else
                {
                    dtdept.DefaultView.RowFilter = "NAME  like '%" + this.emrFindText1.Text + "%' or PY like '%" + this.emrFindText1.Text + "%'";
                }
                if (this.gvDept.FocusedRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    strColValue = this.gvDept.GetFocusedRowCellValue(this.gvDept.Columns["ID"]).ToString();
                }
            }
            else
            {
                this.dtdept.DefaultView.RowFilter = defaultfilter;
                this.gvDept.FocusedRowHandle = 0;
                return;
            }
            this.dtdept.DefaultView.RowFilter = defaultfilter;
            SetFocus(strColValue, this.gcDept, "ID");
        }

        private void emrFindText1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                this.gvDept.MoveNext();
            }
            else if (e.KeyData == Keys.Up)
            {
                this.gvDept.MovePrev();
            }
        }

        #region 定位功能
        public static void SetFocus(string p_strSearchText, DevExpress.XtraGrid.GridControl p_gridControl, string p_strColName)
        {
            DevExpress.XtraGrid.Views.Base.ColumnView view = (DevExpress.XtraGrid.Views.Base.ColumnView)p_gridControl.FocusedView;
            DevExpress.XtraGrid.Columns.GridColumn column = view.Columns[p_strColName];
            if (column != null)
            {
                int intFound = view.LocateByDisplayText(view.FocusedRowHandle + 1, column, p_strSearchText);
                if (intFound != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    view.FocusedRowHandle = intFound;
                }
            }
        }

        #endregion

        #endregion

        #region 人员过滤功能
        private void emrFindText2_TextChanged(object sender, EventArgs e)
        {
            if (this.emrFindText2.Text != this.emrFindText2.HintStr && this.emrFindText2.Text.Trim() != "")
            {
                if (chkAllEmployee.Checked)
                {
                    this.dtemployee.DefaultView.RowFilter = "1=1";
                }
                string filter =this.dtemployee.DefaultView.RowFilter;
                string filter2 = "and (CODE like '%{0}%' or NAME like '%{0}%' or PY like '%{0}%' )";
                filter2 = string.Format(filter2, this.emrFindText2.Text.Trim());
                filter = filter + filter2;
                this.dtemployee.DefaultView.RowFilter = filter;
                //this.gcEmployee.DataSource = this.dtemployee.DefaultView;
                return;
            }
            else if (this.gvDept.FocusedRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle && !this.chkAllEmployee.Checked)
            {
                this.dtemployee.DefaultView.RowFilter = "DID=" + this.gvDept.GetFocusedRowCellValue("ID").ToString();
                //this.gcEmployee.DataSource = this.dtemployee.DefaultView;
                this.gvEmployee.FocusedRowHandle = 0;
                return;
            }
            if (chkAllEmployee.Checked)
            {
                this.dtemployee.DefaultView.RowFilter = "1=1";
                //this.gcEmployee.DataSource = this.dtemployee.DefaultView;
            }

        }
        #endregion
    }
}
