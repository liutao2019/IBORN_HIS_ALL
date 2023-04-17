using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem
{
    /// <summary>
    /// 医保对照维护界面
    /// </summary>
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

        private string centerType = "";
        public string CenterType
        {
            get { return centerType; }
            set { centerType = value; }
        }

        FS.SOC.HISFC.Fee.Models.CompareItemModel currentItem = null;
        public FS.SOC.HISFC.Fee.Models.CompareItemModel CurrentItem
        {
            get { return currentItem; }
        }



        int intUnDrug = 0;
        /// <summary>
        /// 显示控制
        /// </summary>
        [Category("控件设置"), Description("全部显示：0 非药品：1：药品:2")]
        public int IntUnDrug
        {
            get
            {
                return this.intUnDrug;
            }
            set
            {
                this.intUnDrug = value;
            }
        }

        string gzsiPact = "2";
        /// <summary>
        /// 广州医保默认合同单位
        /// </summary>
        [Category("默认医保设置"), Description("广州医保默认合同单位")]
        public string GzsiPact
        {
            get
            {
                return this.gzsiPact;
            }
            set
            {
                this.gzsiPact = value;
            }
        }
        string szsiPact = "zszyb";
        /// <summary>
        /// 深圳医保默认合同单位
        /// </summary>
        [Category("默认医保设置"), Description("深圳医保默认合同单位")]
        public string SzsiPact
        {
            get
            {
                return this.szsiPact;
            }
            set
            {
                this.szsiPact = value;
            }
        }
        string ydsiPact = "2";
        /// <summary>
        /// 异地医保默认合同单位
        /// </summary>
        [Category("默认医保设置"), Description("异地医保默认合同单位")]
        public string YdsiPact
        {
            get
            {
                return this.ydsiPact;
            }
            set
            {
                this.ydsiPact = value;
            }
        }

        string dgsiPact = "zdgyb";
        /// <summary>
        /// 东莞医保默认合同单位
        /// </summary>
        [Category("默认医保设置"), Description("东莞医保默认合同单位")]
        public string DgsiPact
        {
            get
            {
                return this.dgsiPact;
            }
            set
            {
                this.dgsiPact = value;
            }
        }



        FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem.ucLocalFeeItem localItem = null;
        FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem.ucCenterControl centerControl = null;
        FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem.ucCenterFeeItem centerItem = null;
        private void LoadUC()
        {
            centerControl = new ucCenterControl();
            this.pnlCenter.Controls.Clear();
            //加载界面
            centerControl.Dock = DockStyle.Fill;
            this.pnlCenter.Controls.Add(centerControl);

            this.strPacts = centerControl.StrPacts;
            this.CenterType = centerControl.CenterType;

            localItem = new  ucLocalFeeItem(1);
            localItem.SelectedItemChingedEvent -= new ucLocalFeeItem.SelectedItemChingedHandle(localItem_SelectedItemChingedEvent);
            localItem.SelectedItemChingedEvent += new ucLocalFeeItem.SelectedItemChingedHandle(localItem_SelectedItemChingedEvent);

           
            this.gbLeft.Controls.Clear();
           
            //加载界面
            localItem.Dock = DockStyle.Fill;
            this.gbLeft.Controls.Add(localItem);

            this.currentItem = localItem.currentItem;


            centerItem = new ucCenterFeeItem(2);
            centerItem.GzsiPact = this.GzsiPact;
            centerItem.SzsiPact = this.SzsiPact;
            centerItem.YdsiPact = this.YdsiPact;
            centerItem.DgsiPact = this.DgsiPact;


            centerItem.SelectedItemChingedEvent -= new ucCenterFeeItem.SelectedItemChingedHandle(localItem_SelectedItemChingedEvent);
            centerItem.SelectedItemChingedEvent += new ucCenterFeeItem.SelectedItemChingedHandle(localItem_SelectedItemChingedEvent);

            centerItem.StrPacts = this.strPacts;
            centerItem.CenterType = this.CenterType;
            this.gbRight.Controls.Clear();
            //加载界面
            centerItem.Dock = DockStyle.Fill;
            this.gbRight.Controls.Add(centerItem);



            centerControl.itemSelectedEvent += new ucCenterControl.ItemSelectedHandle(centerControl_itemSelectedEvent);
            centerControl_itemSelectedEvent(this.strPacts);
 
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
            this.CenterType = centerControl.CenterType;
            centerItem.CenterType = this.CenterType;
            centerItem.initBaseData(strPacts);
        }
    }
}
