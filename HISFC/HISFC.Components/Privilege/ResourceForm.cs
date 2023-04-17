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
    /// [��������: ��Դ����]<br></br>
    /// [������:   �ſ���]<br></br>
    /// [����ʱ��: 2008.6.23]<br></br>
    /// <˵��>
    ///     ��Դ������
    /// </˵��>
    /// </summary>
    public partial class ResourceForm :FS.HISFC.Components.Privilege.PermissionBaseForm
    {
        #region ˽�б���
        private List<FS.HISFC.BizLogic.Privilege.Model.Resource> currentResLists;
        ResourceControl privilegeResourceControl = null;
        FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        #endregion

        #region ��������
        public ResourceForm()
        {
            InitializeComponent();
            FS.FrameWork.WinForms.Classes.Function.SetTabControlStyle(this.tabControl1);
            InitToolBar();
            this.MainToolStrip.ItemClicked += new ToolStripItemClickedEventHandler(MainToolStrip_ItemClicked);

        }
        #endregion


        #region ˽�з���
        private void InitToolBar()
        {
            ToolBarService _toolBarService = new ToolBarService();
            _toolBarService.AddToolButton("���ӷ���", "", FS.FrameWork.WinForms.Classes.EnumImageList.F�������, true, false, null);
            _toolBarService.AddToolButton("��������", "", FS.FrameWork.WinForms.Classes.EnumImageList.F�������, true, false, null);
            _toolBarService.AddToolButton("ɾ������", "", FS.FrameWork.WinForms.Classes.EnumImageList.F����ɾ��, true, false, null);
            _toolBarService.AddToolSeparator();
            _toolBarService.AddToolButton("���Ӳ˵�", "",FS.FrameWork.WinForms.Classes.EnumImageList.C�˵����, true, false, null);
            _toolBarService.AddToolButton("ɾ���˵�", "", FS.FrameWork.WinForms.Classes.EnumImageList.C�˵�ɾ��, true, false, null);
            _toolBarService.AddToolSeparator();
            _toolBarService.AddToolButton("�˳�", "", FS.FrameWork.WinForms.Classes.EnumImageList.T�˳�, true, false, null);

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

        #region �¼�
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
                case "���ӷ���":
                    privilegeResourceControl.AddType(tabControl1.SelectedTab.Name.Trim());
                    break;
                case "��������":
                    privilegeResourceControl.AddChildType(tabControl1.SelectedTab.Name.Trim());
                    break;
                case "ɾ������":
                    privilegeResourceControl.RemoveType();
                    break;
                case "���Ӳ˵�":
                    privilegeResourceControl.AddRes();
                    break;
                case "ɾ���˵�":
                    privilegeResourceControl.RemoveRes();
                    break;
                case "�˳�":
                    this.Close();
                    break;
            }
        }

        #endregion

    }
}