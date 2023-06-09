using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using FS.WinForms.Forms;


using FS.HISFC.Components.Privilege;
using FS.HISFC.BizLogic.Privilege.Service;
using FS.FrameWork.WinForms.Forms;
using FS.HISFC.Components.Privilege.Common;
using System.Collections;

namespace FS.HISFC.Components.Privilege
{
    /// <summary>
    /// [功能描述: 资源管理]<br></br>
    /// [创建者:   张凯钧]<br></br>
    /// [创建时间: 2008.6.23]<br></br>
    /// <说明>
    ///     资源主窗体
    /// </说明>
    /// </summary>
    public partial class ResourceForm :FS.HISFC.Components.Privilege.PermissionBaseForm
    {
        #region 私有变量
        private List<FS.HISFC.BizLogic.Privilege.Model.Resource> currentResLists;
        ResourceControl privilegeResourceControl = null;
        FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        #endregion

        #region 公共方法
        public ResourceForm()
        {
            InitializeComponent();
            FS.FrameWork.WinForms.Classes.Function.SetTabControlStyle(this.tabControl1);
            InitToolBar();
            this.MainToolStrip.ItemClicked += new ToolStripItemClickedEventHandler(MainToolStrip_ItemClicked);

        }
        #endregion


        #region 私有方法
        private void InitToolBar()
        {
            ToolBarService _toolBarService = new ToolBarService();
            _toolBarService.AddToolButton("增加分类", "", FS.FrameWork.WinForms.Classes.EnumImageList.F分类添加, true, false, null);
            _toolBarService.AddToolButton("增加子类", "", FS.FrameWork.WinForms.Classes.EnumImageList.F分类添加, true, false, null);
            _toolBarService.AddToolButton("删除分类", "", FS.FrameWork.WinForms.Classes.EnumImageList.F分类删除, true, false, null);
            _toolBarService.AddToolSeparator();
            _toolBarService.AddToolButton("增加菜单", "",FS.FrameWork.WinForms.Classes.EnumImageList.C菜单添加, true, false, null);
            _toolBarService.AddToolButton("删除菜单", "", FS.FrameWork.WinForms.Classes.EnumImageList.C菜单删除, true, false, null);
            _toolBarService.AddToolSeparator();
            _toolBarService.AddToolButton("退出", "", FS.FrameWork.WinForms.Classes.EnumImageList.T退出, true, false, null);

            ArrayList toolButtons = _toolBarService.GetToolButtons();
            for (int i = 0; i < toolButtons.Count; i++)
            {
                this.MainToolStrip.Items.Add(toolButtons[i] as ToolStripItem);
            }
            this.MainToolStrip.Items[0].TextImageRelation = TextImageRelation.ImageAboveText;
            this.MainToolStrip.Items[1].TextImageRelation = TextImageRelation.ImageAboveText;
            this.MainToolStrip.Items[2].TextImageRelation = TextImageRelation.ImageAboveText;

            this.MainToolStrip.Items[4].TextImageRelation = TextImageRelation.ImageAboveText;
            this.MainToolStrip.Items[5].TextImageRelation = TextImageRelation.ImageAboveText;
            this.MainToolStrip.Items[7].TextImageRelation = TextImageRelation.ImageAboveText;
        }


        private void LoadRes(string typeRes)
        {
            PrivilegeService _proxy = Common.Util.CreateProxy();
            currentResLists = new List<FS.HISFC.BizLogic.Privilege.Model.Resource>();
            using (_proxy as IDisposable)
            {
                currentResLists = _proxy.QueryResourcesByType(typeRes);

                List<FS.HISFC.BizLogic.Privilege.Model.Resource> list;
                list = new List<FS.HISFC.BizLogic.Privilege.Model.Resource>(currentResLists);

            }

            AddPrivilegeResControl();
        }

        private void AddPrivilegeResControl()
        {
            tabControl1.SelectedTab.Controls.Clear();
            privilegeResourceControl = new ResourceControl(currentResLists);
            privilegeResourceControl.Dock = DockStyle.Fill;
            tabControl1.SelectedTab.Controls.Add(privilegeResourceControl);
        }
        #endregion

        #region 事件
        private void ResForm_Load(object sender, EventArgs e)
        {
            LoadRes(tabControl1.SelectedTab.Name);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRes(tabControl1.SelectedTab.Name);
        }

        void MainToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "增加分类":
                    privilegeResourceControl.AddType(tabControl1.SelectedTab.Name.Trim());
                    break;
                case "增加子类":
                    privilegeResourceControl.AddChildType(tabControl1.SelectedTab.Name.Trim());
                    break;
                case "删除分类":
                    privilegeResourceControl.RemoveType();
                    break;
                case "增加菜单":
                    privilegeResourceControl.AddRes();
                    break;
                case "删除菜单":
                    privilegeResourceControl.RemoveRes();
                    break;
                case "退出":
                    this.Close();
                    break;
            }
        }

        #endregion

    }
}