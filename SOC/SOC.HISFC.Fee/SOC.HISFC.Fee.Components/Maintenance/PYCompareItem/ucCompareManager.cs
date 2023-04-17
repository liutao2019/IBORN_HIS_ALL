using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.PYCompareItem
{
    public partial class ucCompareManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucCompareManager()
        {
            InitializeComponent();
        }

        private string strPacts = "";
        public string StrPacts
        {
            get { return strPacts; }
            set { strPacts = value; }
        }

        FS.SOC.HISFC.Fee.Models.CompareItemModel currentItem = null;
        public FS.SOC.HISFC.Fee.Models.CompareItemModel CurrentItem
        {
            get { return currentItem; }
        }

        FS.SOC.HISFC.Fee.Components.Maintenance.PYCompareItem.ucFeeItem localItem = null;
        FS.SOC.HISFC.Fee.Components.Maintenance.PYCompareItem.ucCenterControl centerControl = null;
        FS.SOC.HISFC.Fee.Components.Maintenance.PYCompareItem.ucFeeItem centerItem = null;
        private void LoadUC()
        {
            centerControl = new ucCenterControl();
            this.pnlCenter.Controls.Clear();
            //加载界面
            centerControl.Dock = DockStyle.Fill;
            this.pnlCenter.Controls.Add(centerControl);

            this.strPacts = centerControl.StrPacts;
           

            localItem = new  ucFeeItem(1);
            localItem.SelectedItemChingedEvent -= new ucFeeItem.SelectedItemChingedHandle(localItem_SelectedItemChingedEvent);
            localItem.SelectedItemChingedEvent += new ucFeeItem.SelectedItemChingedHandle(localItem_SelectedItemChingedEvent);

           
            this.gbLeft.Controls.Clear();
           
            //加载界面
            localItem.Dock = DockStyle.Fill;
            this.gbLeft.Controls.Add(localItem);

            this.currentItem = localItem.currentItem;


            //localItem.SelectedItemChingedEvent -= new ucFeeItem.SelectedItemChingedHandle(localItem_SelectedItemChingedEvent);
            //localItem.SelectedItemChingedEvent += new ucFeeItem.SelectedItemChingedHandle(localItem_SelectedItemChingedEvent);


            centerItem = new  ucFeeItem(2);
            centerItem.SelectedItemChingedEvent -= new ucFeeItem.SelectedItemChingedHandle(localItem_SelectedItemChingedEvent);
            centerItem.SelectedItemChingedEvent += new ucFeeItem.SelectedItemChingedHandle(localItem_SelectedItemChingedEvent);

            centerItem.StrPacts = this.strPacts;
            this.gbRight.Controls.Clear();
            //加载界面
            centerItem.Dock = DockStyle.Fill;
            this.gbRight.Controls.Add(centerItem);



            centerControl.itemSelectedEvent += new ucCenterControl.ItemSelectedHandle(centerControl_itemSelectedEvent);
           
 
        }
        protected override void OnLoad(EventArgs e)
        {
            LoadUC();
            base.OnLoad(e);
        }
        /// <summary>
        /// 项目选择事件
        /// </summary>
        /// <param name="item"></param>
        private void localItem_SelectedItemChingedEvent(FS.SOC.HISFC.Fee.Models.CompareItemModel item)
        {
            currentItem = item;
            this.centerControl.SetLocalItem(item);
        }

        private void centerControl_itemSelectedEvent(string strPacts)
        {
            centerItem.initBaseData(strPacts);
        }


        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("未对照", "查询未对照本地项目", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查找, true, false, null);
            this.toolBarService.AddToolButton("全部", "查询所有医院项目", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
         
            return this.toolBarService;
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "未对照":
                    this.localItem.IsUnCompare = true;
                    this.localItem.filterItem();
                    break;
                case "全部":
                    this.localItem.IsUnCompare = false;
                    this.localItem.filterItem();
                    break;
               
            }

            base.ToolStrip_ItemClicked(sender, e);
        }
        public override int Export(object sender, object neuObject)
        {
             this.localItem.Export(sender, neuObject);
            if (DialogResult.Yes == MessageBox.Show("是否导出农保中心项目","提示",MessageBoxButtons.YesNo))
            {
                this.centerItem.Export(sender,neuObject);
            }
            return 0;
        }
    }
}
