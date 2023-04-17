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
    public partial class ucCenterControl : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCenterControl()
        {
            InitializeComponent();

        }

       private string strPacts = "";
       public string StrPacts
       {
           get { return strPacts; }
           set { strPacts = value; }
       }

       string centerType = "";
       public string CenterType
       {
           get { return centerType; }
           set { centerType = value; }
       }
        public delegate void ItemSelectedHandle(string strPacts);
        public event ItemSelectedHandle itemSelectedEvent;
        public delegate void SaveHandle();
        public event SaveHandle saveEvent;



        FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem.ucCenterCompare centerCompare = null;
        FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem.ucPactCompare pactCompare = null;
        protected override void OnLoad(EventArgs e)
        {
            LoadUC();
            base.OnLoad(e);
        }

        private void LoadUC()
        {
            pactCompare = new ucPactCompare();
            this.splitContainer1.Panel2.Controls.Clear();
            //加载界面
            pactCompare.Dock = DockStyle.Fill;
            pactCompare.itemPactCompareSelectedHandle+=new ucPactCompare.ItemPactCompareSelectedHandle(pactCompare_itemPactCompareSelectedHandle);
            this.splitContainer1.Panel2.Controls.Add(pactCompare);


            centerCompare = new  ucCenterCompare();
            this.splitContainer1.Panel1.Controls.Clear();
            //加载界面
            centerCompare.Dock = DockStyle.Fill;
            centerCompare.itemSelectedEvent += new ucCenterCompare.ItemSelectedHandle(centerCompare_itemSelectedEvent);


            this.splitContainer1.Panel1.Controls.Add(centerCompare);

            this.strPacts = centerCompare.StrPacts;
            this.CenterType = centerCompare.CenterType;

            centerCompare.saveEvent += new ucCenterCompare.SaveHandle(centerCompare_saveEvent);
           
        }

        FS.SOC.HISFC.Fee.Models.CompareItemModel currentItem = null;
        public void SetLocalItem(FS.SOC.HISFC.Fee.Models.CompareItemModel item)
        {
            currentItem = item;
            centerCompare.SetLocalItem(item);

            if ("1" == item.User01)
            {
                pactCompare.StrPacts = this.StrPacts;
                pactCompare.SetLocalItem(item);
            }
            
        }

        public void centerCompare_itemSelectedEvent(string strPacts)
        {
            StrPacts = strPacts;
            this.CenterType = centerCompare.CenterType;
            if (currentItem !=null)
            {
                pactCompare.SetComparedItem(strPacts); 
            }
            
            if (this.itemSelectedEvent != null)
            {
                itemSelectedEvent(strPacts);
            }


 
        }
        private void centerCompare_saveEvent()
        {
            pactCompare.SetComparedItem(strPacts); 
        }


        private void pactCompare_itemPactCompareSelectedHandle(FS.SOC.HISFC.Fee.Models.CompareItemModel pactInfo)
        {


            centerCompare.SetLocalItem(pactInfo);
        }
    }
}
