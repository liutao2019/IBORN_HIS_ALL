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
using System.Collections;

namespace FS.HISFC.Components.Privilege.Forms
{
    /// <summary>
    /// 通用报表承载窗体
    /// </summary>
    public partial class frmCommonReport : FS.FrameWork.WinForms.Forms.frmBaseForm
    {
        public frmCommonReport()
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
        /// <summary>
        /// 加载控件，避免重复加载控件
        /// </summary>
        private Dictionary<string, Control> controls = new Dictionary<string, Control>();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.currentRole = (myService.Operator as FS.HISFC.Models.Base.Employee).CurrentGroup as FS.HISFC.BizLogic.Privilege.Model.Role;
            try
            {
                this.ShowList();
            }
            catch { }
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

            //实例化报表维护控件所用到的实体
            Control c = new Control();

            if (controls.ContainsKey(currentReport.Id) == false)
            {
                try
                {
                    //装载程序集
                    System.Reflection.Assembly _assembly;

                    Type _type = Type.GetType(currentReport.Resource.WinName);
                    if (_type == null)
                    {
                        _assembly = System.Reflection.Assembly.LoadFrom(FS.FrameWork.WinForms.Classes.Function.CurrentPath+currentReport.Resource.DllName+".dll");
                    }
                    else
                    {
                        _assembly = System.Reflection.Assembly.GetAssembly(_type);
                    }

                    _type = _assembly.GetType(currentReport.Resource.WinName);
                    c = Activator.CreateInstance(_type, null) as Control;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                controls[currentReport.Id] = c;
            }
            else
            {
                c = controls[currentReport.Id];
            }

            //获取报表维护控件的iReport操作接口
            iReport = c as FS.FrameWork.WinForms.Forms.IReport;
            if (iReport == null)
            {
                MessageBox.Show("控件没有实现接口FS.FrameWork.WinForms.Forms.IReport，不能显示报表", "未实现接口");
                return;
            }
            if (this.panelMain.Controls.Count == 0 || this.panelMain.Controls[0] != c)
            {
                try
                {

                    //加载控件
                    this.panelMain.Controls.Clear();
                    this.isOneControl = true;
                    this.formID = currentReport.Id;
                    this.SetFormID(this.formID, c);

                    c.Dock = System.Windows.Forms.DockStyle.Fill;
                    c.BringToFront();
                    this.AddControl(c, this.panelMain);
                    if (c is FS.FrameWork.WinForms.Forms.IQueryControlable)
                    {
                        this.iQueryControlable = c as FS.FrameWork.WinForms.Forms.IQueryControlable;
                    }
                }
                catch
                {
                    MessageBox.Show("创建实体" + currentReport.Resource.WinName + "时出错！");
                    return;
                }
            }

            //调用接口，显示数据
            try
            {
                //获取调用参数
                iReport.SetParm(currentReport.Parameter, currentReport.Id);
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
            node.ImageIndex = 2;

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

                if (this.queryManager.QueryItemQueryMend("ALL", sequenceRoleResource.Id, ref dsItem) == -1)
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
                        item.ImageIndex = 4;
                        item.SelectedImageIndex = 3;
                        node.Nodes.Add(item);
                    }
                }
            }

            TvReports.ExpandAll();
        }

        private void TvReports_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //刷新数据
            this.RefreshData();
        }

        public void SetFormID(string formid,Control c)
        {
            this.formID = formid;
            FS.HISFC.BizLogic.Admin.FunSetting manager = new FS.HISFC.BizLogic.Admin.FunSetting();
            FS.HISFC.Models.Admin.FunSetting obj = manager.GetFunSetting(formid);
            if (obj == null)
                return;
            this.SetControl(obj.ControlXML, obj.IsShowToolBar, obj.IsShowTreeView, obj.IsShowStatusBar, obj.IsDoubleClick, obj.TextPosition, obj.IsShowSearch,c);

        }

        public  void SetControl(string xml, bool isShowToolBar, bool isShowTree, bool isShowStatusBar, bool isDoubleClick, int textPosition, bool isShowSearch , Control c)
        {
            this.statusBar1.Visible = isShowStatusBar;
            this.toolBar2.Visible = isShowToolBar;
            this.panel2.Visible = isShowTree;
            this.isDoubleSelectValue = isDoubleClick;
            this.IsShowSearchTextBox = isShowSearch;
            FS.FrameWork.WinForms.Forms.ToolBarButtonService.SetButtonProperty(24, textPosition);
            if (this.isOneControl == true)
            {
                this.SetToolBar("default","default", xml);
                ArrayList al = this.ConvertStringToArrayList(xml, "default", "default");
                if (al != null)
                {
                    try
                    {
                        FS.FrameWork.WinForms.Classes.Function.SetPropertyToControl(c, al);
                    }
                    catch { }

                }
            }
            this.AddControl(xml);
        }
    }
}
