using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Print
{
    public partial class ucIceBoxPrint : UserControl
    {
        public ucIceBoxPrint()
        {
            InitializeComponent();
        }

        private string iceBoxId = "";
        public string IceBoxId
        {
            set
            {
                iceBoxId = value;
            }
        }

        private string iceBoxName = "";

        private string GetSql()
        {
            return @"
            select d.DISEASENAME ||' '|| max(SPECIMENTNAME)||' '||min(sr.SPEC_NO)||'-'||max(sr.SPEC_NO)
            from SPEC_SHELF s join SPEC_box b on s.SHELFID = b.DESCAPID and b.DESCAPTYPE = 'J'
            join SPEC_SUBSPEC ss on b.BOXID = ss.BOXID join spec_source sr on ss.SPECID = sr.SPECID
            join SPEC_DISEASETYPE d on sr.DISEASETYPEID = d.DISEASETYPEID 
            join SPEC_TYPE st on st.SPECIMENTTYPEID = b.SPECTYPEID
            where s.SHELFID = {0}
            group by d.DISEASENAME
            ";
        }        

        private void GetIceBoxInfo()
        {
            int shelfCount = 0;

            try
            {
            IceBox iceBox = (new IceBoxManage()).GetIceBoxById(iceBoxId);
            if(iceBox==null || iceBox.IceBoxId==0)
            {
                return;
            }

            this.iceBoxName = iceBox.IceBoxName;
            ArrayList arrLayer = (new IceBoxLayerManage()).GetLayerInOneBox(iceBoxId);

            if(arrLayer==null||arrLayer.Count==0)
            {
                return;
            }

           this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.Columns.Count=0;
           //this.neuSpread1_Sheet1.Models.ColumnHeaderSpan.Add(0, 0, 1, sheetView.Columns.Count);
           //this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = iceBox.IceBoxName + "   位置图";
            
            bool initShelf = false;

            foreach(IceBoxLayer l in arrLayer)
            {
                neuSpread1_Sheet1.Rows.Add(neuSpread1_Sheet1.RowCount,1);

                int index = neuSpread1_Sheet1.RowCount -1;
                neuSpread1_Sheet1.Rows[index].Height = 60;
                
               
                neuSpread1_Sheet1.RowHeader.Cells[index,0].Text = "第 " +  l.LayerNum +" 层";
                neuSpread1_Sheet1.RowHeader.Columns[0].Width = 100;
                ArrayList arrShelf = (new ShelfManage()).GetShelfByLayerID(l.LayerId.ToString());

                foreach(Shelf sf in arrShelf)
                {
                    if(!initShelf)
                    {
                        shelfCount = l.Col;
                        neuSpread1_Sheet1.Columns.Count = l.Capacity;
                        this.neuSpread1_Sheet1.ColumnHeader.RowCount = 2;
                        this.neuSpread1_Sheet1.Models.ColumnHeaderSpan.Add(0,0,1,neuSpread1_Sheet1.ColumnCount);
                        initShelf=true;
                    }
                    neuSpread1_Sheet1.ColumnHeader.Cells[1,sf.IceBoxLayer.Col-1].Text = "第 " + sf.IceBoxLayer.Col + " 架";

                    string tmpSql = string.Format(this.GetSql(),sf.ShelfID.ToString());
                    string text =  (new IceBoxLayerManage()).ExecSqlReturnOne(tmpSql);

                    if (text == "-1")
                    {
                        text = "";
                    }
                    int width = 100;
                    if (l.Col <= 3)
                    {
                        width = 250;
                    }
                    if (l.Col == 4)
                    {
                        width = 200;
                    }
                    if (l.Col == 5)
                    {
                        width = 180;
                    }
                    if (l.Col == 6)
                    {
                        width = 160;
                    }

                     FarPoint.Win.ComplexBorderSide bs = new FarPoint.Win.ComplexBorderSide(Color.Black,1);
                    neuSpread1_Sheet1.Columns[sf.IceBoxLayer.Col - 1].Width = width;

                    FarPoint.Win.ComplexBorder bord = new FarPoint.Win.ComplexBorder(bs, bs, bs, bs);

                     //neuSpread1_Sheet1.Columns[sf.IceBoxLayer.Col - 1].Border = bord; 

                    neuSpread1_Sheet1.Cells[index, sf.IceBoxLayer.Col - 1].Border = bord;

                    neuSpread1_Sheet1.Cells[index, sf.IceBoxLayer.Col - 1].Text = text;
                    neuSpread1_Sheet1.Cells[index, sf.IceBoxLayer.Col - 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    neuSpread1_Sheet1.Cells[index, sf.IceBoxLayer.Col - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                }
            }

            this.neuSpread1_Sheet1.Models.ColumnHeaderSpan.Add(0, 0, 1, neuSpread1_Sheet1.Columns.Count);
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = iceBox.IceBoxName + "   位置图";
            }
            catch
            {
                return;
            }

            int size = 10;
            if (shelfCount >= 6)
            {
                size = 9;
            }
            Font f = new Font("宋体",size);
            this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 50;
            this.neuSpread1_Sheet1.ColumnHeader.Rows[1].Height = 40;
            this.neuSpread1_Sheet1.ColumnHeader.Rows[0].Font = new Font("宋体", 15);
            this.neuSpread1.Font = f;
            this.neuSpread1_Sheet1.PrintInfo.Printer = "";
            this.neuSpread1_Sheet1.PrintInfo.Centering = FarPoint.Win.Spread.Centering.Both;           
            this.neuSpread1_Sheet1.PrintInfo.ShowGrid = true;            
            this.neuSpread1_Sheet1.PrintInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
            this.neuSpread1_Sheet1.PrintInfo.Preview = true;          
            
            
        }


        public void Print()
        {            
            if(iceBoxId=="")
            {
                return;
            }
            this.GetIceBoxInfo();
            this.neuSpread1.PrintSheet(this.neuSpread1_Sheet1);
        }

        public void Export()
        {

            if (iceBoxId == "")
            {
                return;
            }
            this.GetIceBoxInfo();

            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "位置图导出，选择Excel文件保存位置";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = iceBoxName;
            DialogResult dr = saveFileDiaglog.ShowDialog();
            this.neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            //this.neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.NoFormulas);
        }

        private void ucIceBoxPrint_Load(object sender, EventArgs e)
        {

        }
    }
}
