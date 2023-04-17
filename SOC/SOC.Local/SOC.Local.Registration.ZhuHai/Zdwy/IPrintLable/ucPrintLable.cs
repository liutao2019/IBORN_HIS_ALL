using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Registration.ZhuHai.Zdwy.IPrintLable
{
    public partial class ucPrintLable : UserControl
    {

        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        public ucPrintLable()
        {
            InitializeComponent();
        }

        public int SetValue(FS.HISFC.Models.Account.AccountCard card)
        {
            try 
            {
                this.pictureBox1.Image = SOC.Public.Function.CreateBarCode(card.MarkNO, this.pictureBox1.Width, this.pictureBox1.Height);
            }
            catch
            {
                return -1;
            }

            return 1;
        }

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
                ps = psManager.GetPageSize("XXDJ");

                if (ps == null || string.IsNullOrEmpty(ps.Printer))
                {
                    //没有维护，就采取默认设置
                    //ps = new FS.HISFC.Models.Base.PageSize("XXDJ", 200, 100);
                    //ps.Top = 0;
                    //ps.Left = 0;
                    MessageBox.Show("没有找到打印机【XXDJ】");
                    return -1;
                }

                print.SetPageSize(ps);

                if (ps != null && !string.IsNullOrEmpty(ps.Printer))
                {
                    print.PrintDocument.PrinterSettings.PrinterName = ps.Printer;
                }

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.IsCanCancel = false;

                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                {
                    print.PrintPreview(ps.Left, ps.Top, this);
                }
                else
                {
                    print.PrintPage(ps.Left, ps.Top, this);
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
