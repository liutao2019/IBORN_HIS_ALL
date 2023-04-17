using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.ItemGroup
{
    /// <summary>
    /// [功能描述: 物价组套基本信息管理]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucItemGroupManager : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucItemGroupManager()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 组套列表
        /// </summary>
        private FS.SOC.HISFC.Fee.Interface.Components.IItemGroupQuery IGroupQuery = null;

        /// <summary>
        /// 组套明细
        /// </summary>
        private FS.SOC.HISFC.Fee.Interface.Components.IItemGroupDetail IGroupDetail = null;

        /// <summary>
        /// 选择数据窗口
        /// </summary>
        private FS.FrameWork.WinForms.Forms.frmEasyChoose frmChoose = null;


        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("新增", "新增", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("删除", "删除", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("新增组套", "新增组套项目", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("修改组套", "修改组套项目", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);

            return toolBarService;
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "新增")
            {
                this.add();
            }
            else if (e.ClickedItem.Text == "删除")
            {
                this.delete();
            }
            else if (e.ClickedItem.Text == "新增组套")
            {
                this.addGroupItem();
            }
            else if (e.ClickedItem.Text == "修改组套")
            {
                this.modifyGroupItem();
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 加载基础数据信息
        /// </summary>
        /// <returns></returns>
        private int initBaseData()
        {
            FS.SOC.HISFC.Fee.BizLogic.Undrug itemManager=new FS.SOC.HISFC.Fee.BizLogic.Undrug();
            ArrayList al = itemManager.QueryBriefList("0");
            foreach (FS.SOC.HISFC.Fee.Models.Undrug item in al)
            {
                item.Memo = item.GBCode;
            }
           
            frmChoose = new FS.FrameWork.WinForms.Forms.frmEasyChoose(al);
            frmChoose.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(frmChoose_SelectedItem);
            return 1;
        }

        /// <summary>
        /// 初始化接口信息
        /// </summary>
        /// <returns></returns>
        private int initInterface()
        {
            IGroupQuery = InterfaceManager.GetIItemGroupQuery();
            IGroupDetail = InterfaceManager.GetIItemGroupDetail();
            this.IGroupQuery.SettingFileName = FS.FrameWork.WinForms.Classes.Function.SettingPath+"\\SOC.Fee.ItemGroup.Query.xml";
            this.IGroupDetail.SettingFileName = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\SOC.Fee.ItemGroup.Detail.xml";

            if (this.IGroupQuery is Control)
            {
                this.gbItemGroupQuery.Controls.Clear();
                //加载界面
                this.gbItemGroupQuery.Width = ((Control)this.IGroupQuery).Width + 5;
                ((Control)this.IGroupQuery).Dock = DockStyle.Fill;
                this.gbItemGroupQuery.Controls.Add((Control)this.IGroupQuery);
            }

            if (this.IGroupDetail is Control)
            {
                this.gbItemGroupDetail.Controls.Clear();
                //加载界面
                ((Control)this.IGroupDetail).Dock = DockStyle.Fill;
                this.gbItemGroupDetail.Controls.Add((Control)this.IGroupDetail);
            }

            this.IGroupQuery.Init();
            this.IGroupQuery.SelectedItemGroupChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Fee.Models.Undrug>(curIDataDetail_FilterTextChanged);
            this.IGroupDetail.Init();
            this.IGroupDetail.Clear();

            return 1;
        }

        private int add()
        {
            if (!Function.JugePrive("0801", "02"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有物价基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return 0;
            }
            if (frmChoose == null)
            {
                this.initBaseData();
            }

            frmChoose.ShowDialog(this);

            return 1;
        }

        void frmChoose_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            if (sender is FS.SOC.HISFC.Fee.Models.Undrug)
            {
                this.IGroupDetail.AddItem(sender as FS.SOC.HISFC.Fee.Models.Undrug);
            }
        }

        private int delete()
        {
            return this.IGroupDetail.DeleteItem();
        }

        private int addGroupItem()
        {
            if (!Function.JugePrive("0801", "02"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有物价复合信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return -1;
            }

            return this.IGroupQuery.AddGroupItem();
        }

        private int modifyGroupItem()
        {
            if (!Function.JugePrive("0801", "02"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有物价复合信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return -1;
            }

            return this.IGroupQuery.ModifyGroupItem();
        }


        #endregion

        #region 触发事件

        protected override void OnLoad(EventArgs e)
        {
            this.initInterface();

            base.OnLoad(e);
        }

        void curIDataDetail_FilterTextChanged(FS.SOC.HISFC.Fee.Models.Undrug itemGroup)
        {
            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据...");
            //Application.DoEvents();
            this.IGroupDetail.Clear();
            this.IGroupDetail.AddItemGroup(itemGroup);
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        public override int Export(object sender, object neuObject)
        {
            if (!Function.JugePrive("0801", "02"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有物价基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return 0;
            }
            FS.SOC.Windows.Forms.FpSpread spread = new FS.SOC.Windows.Forms.FpSpread();
            spread.Sheets.Add(this.IGroupQuery.FpSpread.Sheets[0]);
            spread.Sheets.Add(this.IGroupDetail.FpSpread.Sheets[0]);
            spread.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            return base.Export(sender, neuObject);

        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (!Function.JugePrive("0801", "02"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有物价基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return 0;
            }

            if (this.IGroupDetail.Save() > 0)
            {
                this.IGroupQuery.SetFocus();
            }

            return base.OnSave(sender, neuObject);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F10 && ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                this.MessageAllItem();
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region 批量发送消息
        private void MessageAllItem()
        {
            if (!((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在发送消息...");
            Application.DoEvents();

            FS.SOC.HISFC.Fee.BizLogic.UndrugGroup itemGroupManager = new FS.SOC.HISFC.Fee.BizLogic.UndrugGroup();
            FS.SOC.HISFC.Fee.BizLogic.Undrug itemManager = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
            List<FS.SOC.HISFC.Fee.Models.Undrug> lstUndrug = itemManager.QueryAllValidItemGroup();
            foreach (FS.SOC.HISFC.Fee.Models.Undrug itemGroup in lstUndrug)
            {
                //复合项目
                ArrayList lstzt = itemGroupManager.QueryUndrugGroupDetail(itemGroup.ID);

                if (InterfaceManager.GetISaveItemGroupDetail().SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert, lstzt) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    CommonController.CreateInstance().MessageBox(this, "保存失败，请与系统管理员联系并报告错误：" + InterfaceManager.GetISaveItemGroupDetail().Err, MessageBoxIcon.Error);
                    return;
                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            CommonController.CreateInstance().MessageBox("发送成功！", MessageBoxIcon.None);
        }
        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            return 1;
        }

        #endregion
    }
}
