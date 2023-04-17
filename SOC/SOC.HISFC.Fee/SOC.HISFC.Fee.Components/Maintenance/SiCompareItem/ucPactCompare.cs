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
    public partial class ucPactCompare : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPactCompare()
        {
            InitializeComponent();
        }

        public delegate void SaveHandle();
        public event SaveHandle saveEvent;

        public delegate void ItemPactCompareSelectedHandle(FS.SOC.HISFC.Fee.Models.CompareItemModel pactInfo);
        public event ItemPactCompareSelectedHandle itemPactCompareSelectedHandle;


        FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem.ucComparedItem comparedItem = null;
        FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem.ucCompareForPact compareForP = null;
        private void LoadUC()
        {
            comparedItem = new  ucComparedItem();
            this.pnlComparedItem.Controls.Clear();
            //加载界面
            comparedItem.Dock = DockStyle.Fill;
            comparedItem.itemComparedSelectedHandle +=new ucComparedItem.ItemComparedSelectedHandle(comparedItem_itemComparedSelectedHandle);
            
            this.pnlComparedItem.Controls.Add(comparedItem);

            compareForP = new  ucCompareForPact();
            this.pnlCompareForP.Controls.Clear();
            //加载界面
            compareForP.Dock = DockStyle.Fill;
            this.pnlCompareForP.Controls.Add(compareForP);
            pnlCompareForP.Visible = false;

 
        }

        private void comparedItem_itemComparedSelectedHandle(FS.SOC.HISFC.Fee.Models.CompareItemModel pactInfo)
        {


            if (this.itemPactCompareSelectedHandle != null)
            {
                try
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载医保中心数据...");
                    Application.DoEvents();
                    this.itemPactCompareSelectedHandle(pactInfo);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }


        
        }


        protected override void OnLoad(EventArgs e)
        {
            LoadUC();
            base.OnLoad(e);
        }
        FS.SOC.HISFC.Fee.Models.CompareItemModel currentItem = null;
        public void SetLocalItem(FS.SOC.HISFC.Fee.Models.CompareItemModel item)
        {
            comparedItem.SetLocalItem(item);
            if (StrPacts!=null&&StrPacts!=string.Empty)
            {
                 comparedItem.SetComparedItem(StrPacts);
            }
        }
        string strPacts = string.Empty;
        public string StrPacts
        {
            get { return strPacts; }
            set { strPacts = value; }
        }
        public void SetComparedItem(string strPacts)
        {
            strPacts = strPacts;
            comparedItem.SetComparedItem(strPacts);
            if (currentItem!=null )
            {
                comparedItem.SetLocalItem(currentItem);
            }
        }
    }
}
