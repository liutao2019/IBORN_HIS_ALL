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
    public partial class ucPactCompare : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPactCompare()
        {
            InitializeComponent();
        }

        public delegate void SaveHandle();
        public event SaveHandle saveEvent;

        FS.SOC.HISFC.Fee.Components.Maintenance.PYCompareItem.ucComparedItem comparedItem = null;
        FS.SOC.HISFC.Fee.Components.Maintenance.PYCompareItem.ucCompareForPact compareForP = null;
        private void LoadUC()
        {
            comparedItem = new  ucComparedItem();
            this.pnlComparedItem.Controls.Clear();
            //加载界面
            comparedItem.Dock = DockStyle.Fill;
            this.pnlComparedItem.Controls.Add(comparedItem);

            compareForP = new  ucCompareForPact();
            this.pnlCompareForP.Controls.Clear();
            //加载界面
            compareForP.Dock = DockStyle.Fill;
            this.pnlCompareForP.Controls.Add(compareForP);
            pnlCompareForP.Visible = false;

 
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
