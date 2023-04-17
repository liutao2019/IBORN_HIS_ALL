using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.DrugStore
{
    public partial class frmChooseDrugLableLangue : Form
    {
        public frmChooseDrugLableLangue()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 语言选择 1 英文 2 日语
        /// </summary>
        private string langue = "1";

        /// <summary>
        /// 语言选择 1 英文 2 日语
        /// </summary>
        public string Langue
        {
            get { return langue; }
            set { langue = value; }
        }

        //{5980AEF4-5140-41ab-8064-D17B6EB6F4A7}
        public void SetInfo( string infotext)
        {
            this.lblPatientInfo.Text = infotext;
        }

        private bool isCanContine = false;

        private void btEnglish_Click(object sender, EventArgs e)
        {
            this.langue = "1";
            this.isCanContine = true;
            this.Close();
        }

        private void btJapaness_Click(object sender, EventArgs e)
        {
            this.langue = "2";
            this.isCanContine = true;
            this.Close();
        }

        public bool IsCanContine()
        {
            return this.isCanContine;
        }
    }
}
