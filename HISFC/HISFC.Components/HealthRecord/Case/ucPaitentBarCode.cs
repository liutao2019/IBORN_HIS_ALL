using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Case
{
    public partial class ucPaitentBarCode : UserControl
    {
        public ucPaitentBarCode()
        {
            InitializeComponent();
        }

        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                BarcodeLib.Barcode b = new BarcodeLib.Barcode();
                BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
                //===== Encoding performed here =====
                b.IncludeLabel = true;
                this.pictureBox1.Image = b.Encode(type, value.PID.PatientNO.TrimStart(new char[] { '0' }).PadLeft(6, '0').ToString() + "-" + value.InTimes.ToString(), Color.Black, Color.White, this.pictureBox1.Width, this.pictureBox1.Height);
                this.lblpatientNoAndInTimes.Text = value.PID.PatientNO.TrimStart('0').PadLeft(6, '0') + "-" + value.InTimes.ToString();
                this.lblDeptName.Text = value.PVisit.PatientLocation.Name;
                this.lblPatientName.Text = value.Name;
            }
        }
    }
}
