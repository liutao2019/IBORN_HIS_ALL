using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Privilege.Service;
using FS.HISFC.BizLogic.Privilege.Model;

namespace FS.HISFC.Components.Privilege
{
    public partial class frmReport :FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        /// <summary>
        /// 授权报表
        /// </summary>
        public frmReport()
        {
            InitializeComponent();
        }

        //定义操作接口
        private FS.FrameWork.WinForms.Forms.IReport iReport = null;
        private Role currentRole = null;
        private FS.HISFC.BizLogic.Admin.FunSetting myService = new FS.HISFC.BizLogic.Admin.FunSetting();
        private List<RoleResourceMapping> currentRoleResourcList = null;
        private FS.HISFC.BizLogic.Manager.UserPowerDetailManager myPriv = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        private FS.HISFC.BizLogic.Manager.ItemInfoQuery queryManager = new FS.HISFC.BizLogic.Manager.ItemInfoQuery();

        private void frmReport_Load(object sender, EventArgs e)
        {
            if (this.toolStrip1 != null)
            {
                this.toolStrip1.BackColor = FS.FrameWork.WinForms.Classes.Function.GetSysColor(FS.FrameWork.WinForms.Classes.EnumSysColor.Blue);
            }

            this.tbQuery.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C查询);
            this.tbExport.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D导出);
            this.tbPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印);
            this.tbExit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T退出);

            this.currentRole = (myService.Operator as FS.HISFC.Models.Base.Employee).CurrentGroup as FS.HISFC.BizLogic.Privilege.Model.Role;
            this.splitContainer1.SplitterDistance = 140;
            this.WindowState = FormWindowState.Maximized;

            try
            {
                this.ShowList();
            }
            catch { }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == this.tbQuery)
            {
                this.iReport.Query();
            }
            else if (e.ClickedItem == this.tbExport)
            {
                this.iReport.Export();
            }
            else if (e.ClickedItem == this.tbPrint)
            {
                this.iReport.Print();
            }
            else if (e.ClickedItem == this.tbExit)
            {
                if (MessageBox.Show("确定退出？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //刷新数据
            this.RefreshData();
        }

        /// <summary>
        /// 刷新明细数据
        /// </summary>
        public void RefreshData()
        {
            if (this.TvReports.SelectedNode == null || this.TvReports.SelectedNode.Tag == null || this.TvReports.SelectedNode.Tag.ToString() == "root")
            {
                return;
            }

            RoleResourceMapping currentReport = this.TvReports.SelectedNode.Tag as RoleResourceMapping;

            this.splitContainer1.Panel2.Controls.Clear();

            //实例化报表维护控件所用到的实体
            System.Runtime.Remoting.ObjectHandle handle;
            Control c = new Control();

            try
            {
                handle = System.Activator.CreateInstance(currentReport.Resource.DllName, currentReport.Resource.WinName);
                c = handle.Unwrap() as Control;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //获取报表维护控件的iReport操作接口
            iReport = c as FS.FrameWork.WinForms.Forms.IReport;
            if (iReport == null)
            {
                MessageBox.Show("控件没有实现接口FS.FrameWork.WinForms.Forms.IReport，不能显示报表", "未实现接口");
                return;
            }

            try
            {
                //加载控件
                this.splitContainer1.Panel2.Controls.Clear();
                c.Dock = System.Windows.Forms.DockStyle.Fill;
                c.BringToFront();
                this.splitContainer1.Panel2.Controls.Add(c);
            }
            catch
            {
                MessageBox.Show("创建实体" + currentReport.Resource.WinName + "时出错！");
                return;
            }

            //调用接口，显示数据
            try
            {
                //获取调用参数
                iReport.SetParm(currentReport.Parameter,currentReport.Id,currentReport.EmplSql,currentReport.DeptSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 显示列表
        /// </summary>
        public void ShowList()
        {
            //清空列表
            this.TvReports.Nodes.Clear();

            //显示根节点
            TreeNode node = new TreeNode();
            node.Text = "授权报表";
            node.ImageIndex = 0;
            
            node.SelectedImageIndex = node.ImageIndex;
            node.Tag = "root";
            this.TvReports.Nodes.Add(node);

            PrivilegeService _proxy = Common.Util.CreateProxy();
            using (_proxy as IDisposable)
            {
                if (currentRole != null)
                {
                    currentRoleResourcList = _proxy.QueryByTypeRoleId("ReportRes", currentRole.ID);
                }
            }

            if (currentRoleResourcList == null || currentRoleResourcList.Count == 0)
            {
                return;
            }


            foreach (RoleResourceMapping sequenceRoleResource in currentRoleResourcList)
            {
                /*
                 *增加判断权限的代码
                 */

                List<FS.FrameWork.Models.NeuObject> allPrivs = this.myPriv.QueryUserPrivCollection(this.myPriv.Operator.ID, "8101",
                    (this.myPriv.Operator as FS.HISFC.Models.Base.Employee).Dept.ID);

                DataSet dsItem = new DataSet();

                if (this.queryManager.QueryItemQueryMend("ALL",sequenceRoleResource.Id, ref dsItem) == -1)
                {
                    continue;
                }
                else
                {
                    bool havePriv = false;

                    if (dsItem == null)
                    {
                        havePriv = true;
                    }
                    else
                    {
                        if (dsItem.Tables.Count <= 0)
                        {
                            havePriv = true;
                        }
                        else
                        {
                            if (dsItem.Tables[0].Rows.Count <= 0)
                            {
                                havePriv = true;
                            }
                            else
                            {
                                DataRow[] privs = dsItem.Tables[0].Select("科室代码 = 'ALL' or 科室代码 ='" + (this.myPriv.Operator as FS.HISFC.Models.Base.Employee).Dept.ID + "'");

                                foreach (DataRow row in privs)
                                {
                                    if (row["权限编码"].ToString() == "")
                                    {
                                        havePriv = true;
                                        break;
                                    }

                                    foreach (FS.FrameWork.Models.NeuObject priv in allPrivs)
                                    {
                                        if (priv.ID == row["权限编码"].ToString())
                                            havePriv = true;
                                    }
                                }
                            }
                        }
                    }

                    if (havePriv)
                    {
                        TreeNode item = new TreeNode();
                        item.Text = sequenceRoleResource.Name;
                        item.Tag = sequenceRoleResource;
                        item.ImageIndex = 1;
                        node.SelectedImageIndex = node.ImageIndex;
                        node.Nodes.Add(item);
                    }
                }
            }

            TvReports.ExpandAll();
        }

    }
}
