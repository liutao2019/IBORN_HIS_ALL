using BarcodeLib;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Model;
using Neusoft.FrameWork.Function;
using Neusoft.FrameWork.WinForms.Classes;
using Neusoft.FrameWork.WinForms.Controls;
using Neusoft.HISFC.BizLogic.Nurse;
using Neusoft.HISFC.BizProcess.Interface.Nurse;
using Neusoft.HISFC.Components.Common.Classes;
using Neusoft.HISFC.Models.Nurse;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;

namespace Neusoft.SOC.Local.Nurse.ShenZhen
{
    
    partial class ucPrintCure : UserControl, IInjectCurePrint
    {
        private ArrayList alPrint = new ArrayList();

        private Neusoft.HISFC.BizLogic.Nurse.Inject injectMgr = new Neusoft.HISFC.BizLogic.Nurse.Inject();
      

        public ucPrintCure()
        {
            this.InitializeComponent();
        }

 

        private void lbAge_Click(object sender, EventArgs e)
        {
        }

        private void lbName_Click(object sender, EventArgs e)
        {
        }

        private void lbSex_Click(object sender, EventArgs e)
        {
        }

        private void neuPictureBox2_Click(object sender, EventArgs e)
        {
        }

        void IInjectCurePrint.Init(ArrayList al)
        {
            try
            {
                ArrayList list = new ArrayList();
                while (al.Count > 0)
                {
                    Neusoft.HISFC.Models.Nurse.Inject inject = al[0] as Neusoft.HISFC.Models.Nurse.Inject;
                    ArrayList list2 = new ArrayList();
                    foreach (Neusoft.HISFC.Models.Nurse.Inject inject2 in al)
                    {
                        if ((inject2.InjectOrder == null) || (inject2.InjectOrder == ""))
                        {
                            list2.Add(inject2);
                            break;
                        }
                        if (inject2.InjectOrder == inject.InjectOrder)
                        {
                            list2.Add(inject2);
                        }
                    }
                    list.Add(list2);
                    foreach (Neusoft.HISFC.Models.Nurse.Inject inject2 in list2)
                    {
                        al.Remove(inject2);
                    }
                }
                foreach (ArrayList list3 in list)
                {
                    if (this.neuSpread1_Sheet1.RowCount > 0)
                    {
                        this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
                    }
                    Neusoft.HISFC.Models.Nurse.Inject inject3 = null;
                    Barcode barcode = new Barcode();
                    barcode.IncludeLabel = true;
                    int num = 1;
                    for (int i = 0; i < list3.Count; i++)
                    {
                        inject3 = (Neusoft.HISFC.Models.Nurse.Inject)list3[i];
                        this.neuSpread1_Sheet1.Rows.Add(0, 1);
                        num = NConvert.ToInt32(inject3.Memo);
                        if (num == 0)
                        {
                            num = 1;
                        }
                        if ((inject3.Item.Item.Name != null) && (inject3.Item.Item.Name != ""))
                        {
                            this.neuSpread1_Sheet1.Cells[i, 0].Text = inject3.Item.Item.Name;
                            this.neuSpread1_Sheet1.Cells[i, 1].Text = Math.Round((decimal)(inject3.Item.Order.DoseOnce / num), 3).ToString() + inject3.Item.Order.DoseUnit;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells[i, 0].Text = inject3.Item.Name;
                            this.neuSpread1_Sheet1.Cells[i, 1].Text = Math.Round((decimal)(inject3.Item.Order.DoseOnce / num), 3).ToString() + inject3.Item.Order.DoseUnit;
                        }
                    }
                    inject3 = (Neusoft.HISFC.Models.Nurse.Inject)list3[0];
                    this.lbName.Text = inject3.Patient.Name;
                    this.lbTime.Text = DateTime.Now.ToShortDateString();
                    this.lbdept.Text = inject3.Item.Order.DoctorDept.Name;
                    this.lbAge.Text = this.injectMgr.GetAge(inject3.Patient.Birthday, DateTime.Now);
                    this.neuPictureBox1.Image = barcode.Encode(TYPE.CODE128, inject3.Item.Patient.PID.CardNO, 140, 50);
                    QRCodeEncoder encoder = new QRCodeEncoder();
                    encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    encoder.QRCodeScale = 4;
                    encoder.QRCodeVersion = 7;
                    encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    this.neuPictureBox2.Image = encoder.Encode(inject3.Item.Patient.PID.CardNO);
                    if (inject3.Patient.Sex.ID.ToString() == "M")
                    {
                        this.lbSex.Text = "男";
                    }
                    else if (inject3.Patient.Sex.ID.ToString() == "F")
                    {
                        this.lbSex.Text = "女";
                    }
                    else
                    {
                        this.lbSex.Text = "";
                    }
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 3);
                    this.neuSpread1_Sheet1.GrayAreaBackColor = Color.Black;
                    this.Print();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Print()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = null;
            if (print == null)
            {
                print = new Neusoft.FrameWork.WinForms.Classes.Print();
                Neusoft.HISFC.Components.Common.Classes.Function.GetPageSize("Inject", ref print);
            }
            Control control = this;
            print.ControlBorder = enuControlBorder.None;
            control.Width = base.Width;
            control.Height = base.Height;
            print.PrintPage(12, 1, new Control[] { control });
        }

        private void ucPrintCure_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.GrayAreaBackColor = Color.White;
        }
    }
}

