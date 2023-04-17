using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucSpecDetInBox : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private SpecBoxManage boxMgr;
        private SubSpecManage subSpecMgr;
        private SpecBox box;
        private int boxId;
        private string sheetTitle;

        public int BoxId
        {
            set
            {
                boxId = value;
            }
        }

        public ucSpecDetInBox()
        {
            box = new SpecBox();
            boxMgr = new SpecBoxManage();
            subSpecMgr = new SubSpecManage();
            sheetTitle = "";
            InitializeComponent();
        }

        private void InitSheet()
        {
            BoxSpecManage bsMgr = new BoxSpecManage();
            BoxSpec bs = bsMgr.GetSpecByBoxId(boxId.ToString()); ;
            if (bs == null || bs.BoxSpecID <= 0)
            {
                return;
            }

            neuSpread1_Sheet1.DataAutoCellTypes = false;
            neuSpread1_Sheet1.DataAutoHeadings = false;
            neuSpread1_Sheet1.DataAutoSizeColumns = false;
            neuSpread1_Sheet1.AutoGenerateColumns = false;
            neuSpread1_Sheet1.RowCount = bs.Row;
            neuSpread1_Sheet1.ColumnCount = bs.Col;

            neuSpread1_Sheet1.ColumnHeaderRowCount = 2;
            neuSpread1.BackColor = Color.White;

            try
            {
                for (int i = 0; i < neuSpread1_Sheet1.Columns.Count; i++)
                {
                    neuSpread1_Sheet1.ColumnHeader.Cells[0, i].BackColor = Color.White;
                    neuSpread1_Sheet1.ColumnHeader.Cells[1, i].BackColor = Color.White;
                }
                neuSpread1_Sheet1.SheetCornerStyle.BackColor = Color.White;

                neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 35;
                neuSpread1_Sheet1.ColumnHeader.Rows[0].Font = new Font("新宋体", 14);
                neuSpread1_Sheet1.ColumnHeader.Rows[1].Height = 25;
                neuSpread1_Sheet1.ColumnHeader.Rows[0].Font = new Font("新宋体", 12);
                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    neuSpread1_Sheet1.RowHeader.Rows[i].BackColor = Color.White;
                }
                neuSpread1_Sheet1.Models.ColumnHeaderSpan.Add(0, 0, 1, neuSpread1_Sheet1.Columns.Count);
                this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = sheetTitle;

                for (int i = 0; i < neuSpread1_Sheet1.ColumnCount; i++)
                {
                    neuSpread1_Sheet1.ColumnHeader.Cells[1, i].Text = (i + 1).ToString();
                    neuSpread1_Sheet1.Columns[i].Width = 100;
                    neuSpread1_Sheet1.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                }
                for (int k = 0; k < neuSpread1_Sheet1.RowCount; k++)
                {
                    neuSpread1_Sheet1.RowHeader.Cells[k, 0].Text = (Convert.ToChar('A' + k)).ToString();
                    neuSpread1_Sheet1.Rows[k].Height = 25;
                }

                Size size = new Size();
                size.Height = (bs.Row + 1) * 25 + 100;
                size.Width = (bs.Col) * 100 + 50;                
                this.ParentForm.Size = size;
            }
            catch
            { }
        }

        private void SetSheetValue()
        {
            try
            {
                System.Collections.ArrayList arr = new System.Collections.ArrayList();
                arr = subSpecMgr.GetSubSpecInOneBox(boxId.ToString());
                if (arr == null)
                    return;
                foreach (SubSpec sub in arr)
                {
                    neuSpread1_Sheet1.Cells[sub.BoxEndRow - 1, sub.BoxEndCol - 1].Text = sub.SubBarCode;
                }
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                { 
                    for (int k = 0; k < neuSpread1_Sheet1.ColumnCount; k++)
                    {
                        if (neuSpread1_Sheet1.Cells[i, k].Text == "")
                        {
                            neuSpread1_Sheet1.Cells[i, k].Text = "空";

                        } 
                    }
                }
            }
            catch
            { }
        }

        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            try
            {
                neuSpread1_Sheet1.PrintInfo.Preview = true;
                neuSpread1_Sheet1.PrintInfo.Printer = "pro factory";
                neuSpread1.PrintSheet(neuSpread1_Sheet1);
            }
            catch
            {

            }
        }

        public void Export()
        {

            string path = string.Empty;
            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "盒内标本详情，选择Excel文件保存位置";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = sheetTitle;
            DialogResult dr = saveFileDiaglog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);

            }
        }

        public void LoadSetting()
        {
            if (boxId > 0)
            {
                box = boxMgr.GetBoxById(boxId.ToString());
            }
            try
            {
                sheetTitle = box.BoxBarCode + "(" + ParseLocation.ParseSpecBox(box.BoxBarCode) + ")";
                this.ParentForm.Text = sheetTitle;
            }
            catch
            { }
            if (box != null)
            {
                InitSheet();
                SetSheetValue();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadSetting();
            //string s = this.Height.ToString();
            //string h = this.Width.ToString();
            base.OnLoad(e);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export();
        }
    }
}
