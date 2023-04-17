using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Registration.GuangZhou.Zdly.IPrintBar
{
    public partial class ucBarCode : UserControl
    {
        public ucBarCode()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 200, 50);
        }


        public int SetPrintValue(FS.HISFC.Models.Registration.Register register,int i)
        {
            Image img = CreateBarCode(register.PID.CardNO);
            pictureBox1.Image = img;
            lblname.Text = register.Name;
            lblsex.Text = register.Sex.Name;
            return 1;
        }


        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = null;
                try
                {
                    print = new FS.FrameWork.WinForms.Classes.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("初始化打印机失败!" + ex.Message);
                    return -1;
                }

                FS.HISFC.Models.Base.PageSize ps = null;
                //ps = psManager.GetPageSize("BLTM");

                //if (ps == null)
                //{
                //    //没有维护，就采取默认设置
                //ps = new FS.HISFC.Models.Base.PageSize("BLTM", 200, 120);
                //    ps.Top = 0;
                //    ps.Left = 0;
                //}

                ps = new FS.HISFC.Models.Base.PageSize();
                ps.Printer = "BLTM";
                ps.Name = "BLTM";
                ps.ID = "BLTM";
                ps.WidthMM = 50f;
                ps.HeightMM = 30f;

                ps.Top = 0;
                ps.Left = 0;
                print.SetPageSize(ps);
               
              

                //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //print.IsCanCancel = false;
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(0,0, this);
                }
                else
                {
                    print.PrintPage(0,0, this);
                }

                //打印文档的左侧硬页边距的X坐标，这个硬边距和打印机有关，如果硬边距>0，可以设置打印控件的边距值为负数
                //print.PrintDocument.PrinterSettings.DefaultPageSettings.HardMarginX;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }

            return 1;
        }



    }
}
