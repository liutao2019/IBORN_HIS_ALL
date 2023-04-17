using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.AdjustPrice
{
    public partial class frmRetailHistoryAdjust : Form
    {
        public frmRetailHistoryAdjust(System.Collections.ArrayList alAdjustPriceInfo, string drugInfo)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmRetialHistoryAdjust_Load);

            if (alAdjustPriceInfo != null)
            {
                this.alAdjustPriceInfo.AddRange(alAdjustPriceInfo);
            }

            this.nlbInfo.Text = drugInfo;
        }

        System.Collections.ArrayList alAdjustPriceInfo = new System.Collections.ArrayList();        

        void frmRetialHistoryAdjust_Load(object sender, EventArgs e)
        {
            this.nbtOK.Click += new EventHandler(nbtOK_Click);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);

            this.neuDataListSpread_Sheet1.Rows.Count = alAdjustPriceInfo.Count;
            int rowIndex = 0;
            foreach (FS.HISFC.Models.Pharmacy.AdjustPrice info in this.alAdjustPriceInfo)
            {
                this.neuDataListSpread_Sheet1.Cells[rowIndex, 0].Value = info.ID;
                this.neuDataListSpread_Sheet1.Cells[rowIndex, 1].Value = info.InureTime;
                this.neuDataListSpread_Sheet1.Cells[rowIndex, 2].Value = info.Operation.ID;
                this.neuDataListSpread_Sheet1.Cells[rowIndex, 3].Value = info.Operation.Oper.OperTime;
                rowIndex++;
            }
        }

        void nbtCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        void nbtOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

     
    }
}
