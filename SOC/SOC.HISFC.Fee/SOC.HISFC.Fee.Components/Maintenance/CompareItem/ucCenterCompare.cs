using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.CompareItem
{
    /// <summary>
    /// 根据医保类型对照
    /// </summary>
    public partial class ucCenterCompare : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCenterCompare()
        {
            InitializeComponent();
        }

        public delegate void ItemSelectedHandle(string strPacts);
        public event ItemSelectedHandle itemSelectedEvent;

        public delegate void SaveHandle();
        public event SaveHandle saveEvent;


        FS.SOC.HISFC.Fee.Components.Maintenance.CompareItem.ucCenterPact centerPact = null;
        FS.SOC.HISFC.Fee.Components.Maintenance.CompareItem.ucCompareForCenterType compareForCT = null;
        protected override void OnLoad(EventArgs e)
        {
            LoadUC();
            base.OnLoad(e);
        }

        private void LoadUC()
        {
            compareForCT = new ucCompareForCenterType();
            this.pnlCompareForCT.Controls.Clear();
            //加载界面
            compareForCT.Dock = DockStyle.Fill;
            this.pnlCompareForCT.Controls.Add(compareForCT);

                centerPact = new ucCenterPact();
                this.pnlCenterPact.Controls.Clear();
                //加载界面
                centerPact.Dock = DockStyle.Fill;
                centerPact.itemSelectedEvent += new ucCenterPact.ItemSelectedHandle(centerPact_itemSelectedEvent);
                centerPact.getCheckedPactEvent +=new ucCenterPact.QueryCheckedPactHandle(centerPact_getCheckedPactEvent);

                this.pnlCenterPact.Controls.Add(centerPact);

                this.strPacts = centerPact.StrPact;


                compareForCT.saveEvent += new ucCompareForCenterType.SaveHandle(compareForCT_saveEvent);

               

                

        }
        private string strPacts = "";
        public string StrPacts
        {
            get { return strPacts; }
            set { strPacts = value; }
        }
        private void centerPact_itemSelectedEvent(string strType,string strPacts)
        {
            this.compareForCT.SetCenterType(strType);
            StrPacts = strPacts;
            if (this.itemSelectedEvent!=null)
            {
                itemSelectedEvent(strPacts);
            }
 
        }
        private void centerPact_getCheckedPactEvent(Hashtable listPact)
        {
            compareForCT.ListPact = listPact;
            compareForCT.SetModifyLabel();

        }

        FS.SOC.HISFC.Fee.Models.CompareItemModel currentItem = null;
        public void SetLocalItem(FS.SOC.HISFC.Fee.Models.CompareItemModel item)
        {
            currentItem = item;
            compareForCT.SetLocalItem(item);
            
        }
        private void compareForCT_saveEvent()
        {
            if (this.saveEvent != null)
            {
                saveEvent();
            }
        }

        private void pnlCenterPact_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
