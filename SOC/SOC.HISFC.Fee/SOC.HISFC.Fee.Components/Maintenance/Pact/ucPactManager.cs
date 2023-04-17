using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    public partial class ucPactManager : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucPactManager()
        {
            InitializeComponent();
        }

        #region 域变量

        /// <summary>
        /// 组套列表
        /// </summary>
        private FS.SOC.HISFC.Fee.Interface.Components.IPactInfoQuery IPactInfoQuery = null;

        /// <summary>
        /// 组套明细
        /// </summary>
        private FS.SOC.HISFC.Fee.Interface.Components.IPactFeeCodeRateDetail IPactFeeCodeRateDetail = null;

        private FS.SOC.HISFC.Fee.Interface.Components.IPactInfoProperty IPactInfoProperty = null;

        private ucPact ucPact = null;

        private Form frmItem = null;
        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("新增", "新增", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("删除", "删除", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            return toolBarService;
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "新增":
                    this.addNewPactInfo();
                    break;
                case "删除":
                    this.deleteNewPactInfo();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化接口信息
        /// </summary>
        /// <returns></returns>
        private int initInterface()
        {
            IPactInfoQuery = InterfaceManager.GetIPactInfoQuery();
            IPactFeeCodeRateDetail = InterfaceManager.GetIPactFeeCodeRateDetail();
            IPactInfoProperty = InterfaceManager.GetIPactInfoProperty();

            this.IPactInfoQuery.SettingFileName = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\SOC.Fee.Pact.Query.xml";
            this.IPactFeeCodeRateDetail.SettingFileName = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\SOC.Fee.Pact.Detail.xml";

            if (this.IPactInfoQuery is Control)
            {
                this.gbPactInfo.Controls.Clear();
                //加载界面
                ((Control)this.IPactInfoQuery).Dock = DockStyle.Fill;
                this.gbPactInfo.Controls.Add((Control)this.IPactInfoQuery);
            }

            if (this.IPactFeeCodeRateDetail is Control)
            {
                this.gbFeeCodeRateDetail.Controls.Clear();
                //加载界面
                ((Control)this.IPactFeeCodeRateDetail).Dock = DockStyle.Fill;
                this.gbFeeCodeRateDetail.Controls.Add((Control)this.IPactFeeCodeRateDetail);
            }


            if (this.IPactInfoProperty is Control)
            {
                this.gbPropertyGrid.Controls.Clear();
                //加载界面
                ((Control)this.IPactInfoProperty).Dock = DockStyle.Fill;
                this.gbPropertyGrid.Controls.Add((Control)this.IPactInfoProperty);
            }

            this.IPactInfoQuery.Init();
            this.IPactFeeCodeRateDetail.Init();
            this.IPactFeeCodeRateDetail.Clear();

            return 1;
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <returns></returns>
        private int initEvents()
        {

            this.IPactInfoQuery.SelectedPactInfoChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<List<FS.HISFC.Models.Base.PactInfo>>(IPactInfoQuery_SelectedPactInfoChange);
            this.IPactInfoQuery.SelectedPactInfoChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<List<FS.HISFC.Models.Base.PactInfo>>(IPactInfoQuery_SelectedPactInfoChange);

            this.IPactFeeCodeRateDetail.SelectedPactItemRateChange -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<List<FS.HISFC.Models.Base.PactItemRate>>(IPactFeeCodeRateDetail_SelectedPactItemRateChange);
            this.IPactFeeCodeRateDetail.SelectedPactItemRateChange += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<List<FS.HISFC.Models.Base.PactItemRate>>(IPactFeeCodeRateDetail_SelectedPactItemRateChange);

            this.IPactInfoProperty.PropertyValueChanged -= new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Object[]>(IPactInfoProperty_PropertyValueChanged);
            this.IPactInfoProperty.PropertyValueChanged += new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Object[]>(IPactInfoProperty_PropertyValueChanged);

            return 1;
        }

        /// <summary>
        /// 设置药品维护窗口
        /// </summary>
        private void initMaintenanceForm()
        {
            if (this.ucPact == null)
            {
                this.ucPact = new ucPact();
                this.ucPact.Dock = DockStyle.Fill;
                this.ucPact.Init();
                this.ucPact.EndSave -= new ucPact.SaveItemHandler(ucItem_EndSave);
                this.ucPact.EndSave += new ucPact.SaveItemHandler(ucItem_EndSave);
            }
            if (this.frmItem == null)
            {
                this.frmItem = new Form();
                this.frmItem.Width = this.ucPact.Width + 10;
                this.frmItem.Height = this.ucPact.Height + 25;
                this.frmItem.Text = "合同单位维护";
                this.frmItem.StartPosition = FormStartPosition.CenterScreen;
                this.frmItem.ShowInTaskbar = false;
                this.frmItem.HelpButton = false;
                this.frmItem.MaximizeBox = false;
                this.frmItem.MinimizeBox = false;
                this.frmItem.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.frmItem.Controls.Add(this.ucPact);
            }

        }
        #endregion

        #region 方法

        private void addNewPactInfo()
        {
            //判断权限
            if (!Function.JugePrive("0802", "01"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有合同单位维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            //生成PactInfo
            this.initMaintenanceForm();
            this.ucPact.Clear();
            this.frmItem.ShowDialog();

        }

        private void deleteNewPactInfo()
        {
            //判断权限
            if (!Function.JugePrive("0802", "01"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有合同单位维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            //判断权限
            this.IPactInfoQuery.Delete();
        }


        #endregion

        #region 触发事件

        protected override void OnLoad(EventArgs e)
        {
            this.initInterface();
            this.initEvents();
            base.OnLoad(e);
        }

        void IPactFeeCodeRateDetail_SelectedPactItemRateChange(List<FS.HISFC.Models.Base.PactItemRate> pactInfos)
        {
            if (pactInfos != null && pactInfos.Count > 0)
            {
                int num = pactInfos.Count;
                PactItemRateClass[] pactInfoClasses = new PactItemRateClass[num];
                for (int i = 0; i < num; i++)
                {
                    pactInfoClasses[i] = new PactItemRateClass(pactInfos[i].Clone());
                }
                this.IPactInfoProperty.ShowDetailProperty(pactInfoClasses);
            }
            else
            {
                this.IPactInfoProperty.ShowDetailProperty(null);
            }

        }

        void IPactInfoQuery_SelectedPactInfoChange(List<FS.HISFC.Models.Base.PactInfo> pactInfos)
        {
            this.IPactFeeCodeRateDetail.AddItemRange(pactInfos);

            if (pactInfos != null&&pactInfos.Count>0)
            {
                int num = pactInfos.Count;
                PactInfoClass[] pactInfoClasses = new PactInfoClass[num];
                for (int i = 0; i < num; i++)
                {
                    pactInfoClasses[i] = new PactInfoClass(pactInfos[i].Clone());
                }
                this.IPactInfoProperty.ShowProperty(pactInfoClasses);
            }
            else
            {
                this.IPactInfoProperty.ShowProperty(null);
            }
            ((Control)this.IPactInfoQuery).Focus();
            this.IPactInfoQuery.SetFocus();
        }

        void IPactInfoProperty_PropertyValueChanged(Object[] o)
        {
            if (o != null && o.Length > 0)
            {
                if (o[0] is PactInfoClass)
                {
                    List<FS.HISFC.Models.Base.PactInfo> list = new List<FS.HISFC.Models.Base.PactInfo>();
                    foreach (PactInfoClass pactInfoClass in o)
                    {
                        list.Add(pactInfoClass.ToString().Clone());   
                    }

                    this.IPactInfoQuery.AddItemRange(list);
                }
                else if (o[0] is PactItemRateClass)
                {
                    List<FS.HISFC.Models.Base.PactItemRate> list = new List<FS.HISFC.Models.Base.PactItemRate>();
                    foreach (PactItemRateClass pactInfoClass in o)
                    {
                        list.Add(pactInfoClass.ToString().Clone());
                    }

                    this.IPactFeeCodeRateDetail.AddItemRange(list);
                }
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            //判断权限
            if (!Function.JugePrive("0802", "01"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有合同单位维护权限，该操作已取消！", MessageBoxIcon.Information);
                return -1;
            }

            return this.IPactInfoQuery.Save();
        }

        int ucItem_EndSave(FS.HISFC.Models.Base.PactInfo item)
        {
           return   this.IPactInfoQuery.AddItem(item);
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
