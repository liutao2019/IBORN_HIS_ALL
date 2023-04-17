using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucLocPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FarPoint.Win.Spread.SheetView sheetView = new FarPoint.Win.Spread.SheetView();
        private string title = "";

        public string SheetTitle
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public FarPoint.Win.Spread.SheetView SheetView
        {
            set
            {
                sheetView = value;
            }
        }

        public ucLocPrint()
        {
            InitializeComponent();
        }

        public void SetTitle()
        {
            sheetView.Models.ColumnHeaderSpan.Add(0, 0, 1, sheetView.Columns.Count);
            this.sheetView.ColumnHeader.Cells[0, 0].Text = title;
        }

        public void SetSheet()
        {
            FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1 = new FS.FrameWork.WinForms.Controls.NeuSpread();
            neuSpread1.Sheets.Add(sheetView);
            neuSpread1.Width = 150 * (sheetView.ColumnCount + 1);
            //neuSpread1.Dock = DockStyle.Fill;
            this.Controls.Add(neuSpread1);
            //sheetView.ColumnHeaderRowCount = 2;
            neuSpread1.BackColor = Color.White;

            try
            {
                for (int i = 0; i < sheetView.Columns.Count; i++)
                {
                    sheetView.ColumnHeader.Cells[0, i].BackColor = Color.White;
                    sheetView.ColumnHeader.Cells[1, i].BackColor = Color.White;                   
                }
                //sheetView.ColumnHeader.Columns[-1].Label = "BACK";//.BackColor = Color.White;
                //sheetView.ColumnHeader.Rows[0].BackColor = Color.White;
                sheetView.SheetCornerStyle.BackColor = Color.White;

                sheetView.ColumnHeader.Rows[0].Height = 35;
                
                sheetView.ColumnHeader.Rows[0].Font = new Font("新宋体", 14);
                sheetView.ColumnHeader.Rows[1].Height = 25;
                sheetView.ColumnHeader.Rows[0].Font = new Font("新宋体", 12);
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {
                    sheetView.RowHeader.Rows[i].BackColor = Color.White;                 
                }
                SetTitle();
            }
            catch
            { }
        }

        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize("A4",
            //p.SetPageSize(size);
            //p.PrintPreview(0, 0, this.Controls[0]);
            try
            {
                FS.FrameWork.WinForms.Controls.NeuSpread neuSpread1 = this.Controls[0] as FS.FrameWork.WinForms.Controls.NeuSpread;
                int width = neuSpread1.Width;
                sheetView.PrintInfo.Preview = true;
                sheetView.PrintInfo.Printer = "MZFEEDETAILPRINTER";
                neuSpread1.PrintSheet(0);
                //neuSpread1.PrintSheet(sheetView);
            }
            catch
            {

            }


        }
    }
}
