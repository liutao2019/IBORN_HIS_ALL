using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Print
{
    public partial class ucPreOutBillPrint : UserControl
    {
        public ucPreOutBillPrint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 出库类型
        /// </summary>
        private string outType = string.Empty;
        /// <summary>
        /// 出库类型
        /// </summary>
        public string OutType
        {
            get
            {
                return this.outType;
            }
            set
            {
                this.outType = value;
            }
        }

        /// <summary>
        /// 预出库数组
        /// </summary>
        private List<OutInfo> alPreOutList = new List<OutInfo>();

        /// <summary>
        /// 预出库数组
        /// </summary>
        public List<OutInfo> AlPreOutList
        {
            get
            {
                return this.alPreOutList;
            }
            set
            {
                this.alPreOutList = value;
            }
        }

        /// <summary>
        /// 申请信息
        /// </summary>
        private FS.HISFC.Models.Speciment.ApplyTable appTab = new FS.HISFC.Models.Speciment.ApplyTable();
        /// <summary>
        /// 申请信息
        /// </summary>
        public FS.HISFC.Models.Speciment.ApplyTable AppTab
        {
            set
            {
                this.appTab = value;
            }
            get
            {
                return this.appTab;
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="al"></param>
        public void AddData()
        {
            if (this.AlPreOutList.Count <= 0)
            {
                return;
            }
            else
            {
                ArrayList alList = new ArrayList();
                foreach (FS.HISFC.Components.Speciment.OutInfo ot in AlPreOutList)
                {
                    alList.Add(ot);
                }
                alList.Sort(new sortOutputByBarCode());
                int i = 0;
                this.neuSpread1_Sheet1.RowCount = alList.Count;
                foreach (FS.HISFC.Components.Speciment.OutInfo outObj in alList)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.barCode].Text = outObj.SpecBarCode;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.barCode].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.specid].Text = outObj.SpecId;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.specid].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.qty].Text = outObj.Count.ToString();
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.qty].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    if (outObj.ReturnAble == "1")
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)Cols.rt].Text = "归还";
                    }
                    else if (outObj.ReturnAble == "0")
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)Cols.rt].Text = "不归还";
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)Cols.rt].Text = "多次出库";
                    }
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.rt].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    //this.neuSpread1_Sheet1.Cells[i, 3].Text = outObj.ReturnAble;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.position].Text = outObj.Position;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.row].Text = outObj.SubRow;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.row].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.column].Text = outObj.SubCol;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.column].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.qly].Text = outObj.SubQly;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.qly].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.dis].Text = outObj.SubDis;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.dis].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.type].Text = outObj.SubType;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.type].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    this.neuSpread1_Sheet1.Cells[i, (int)Cols.memo].Text = "    ";
                    i++;
                }
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 11;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "取标本人：                   出库：                    审核：";
            }
            this.SetTitle();
        }

        /// <summary>
        /// 设置抬头
        /// </summary>
        private void SetTitle()
        {
            if (this.AppTab != null)
            {
                this.lbTargetDept.Text = "申请科室/部门：" + this.AppTab.DeptName.ToString();
                this.lbOperDate.Text = "申请日期：" + this.AppTab.OutPutTime.ToString();
                this.lbOutListNO.Text = "申请单号：" + this.AppTab.ApplyId.ToString();
                this.lbDrugType.Text = "申请人：" + this.AppTab.ApplyUserName.ToString();
                if (OutType == "OUT")
                {
                    this.lbTitle.Text = "标本库出库单";
                    this.lbDemand.Visible = true;
                    if (string.IsNullOrEmpty(this.AppTab.Comment))
                    {
                        this.AppTab.Comment = "无";
                    }
                    this.lbDemand.Text = "出库说明：" + this.AppTab.Comment.ToString();
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[0].Width = 100;  
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[1].Width = 51;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[5].Visible = true;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[9].Visible = true;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[5].Width = 37;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[9].Width = 65;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[4].Width = 254;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[2].Width = 30;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[3].Width = 30;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[8].Visible = false;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[7].Width = 50;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[8].Width = 50;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[10].Width = 60;
                }
                else if (OutType == "PREOUT")
                {
                    this.lbDemand.Visible = false;
                    this.lbTitle.Text = "标本库预出库单(取标本单)";                    
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[0].Width = 100;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[1].Width = 53;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[5].Visible = false;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[9].Visible = false;                   
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[4].Width = 254;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[2].Width = 48;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[3].Width = 49;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[8].Visible = false;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[6].Width = 62;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[7].Width = 74;
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[10].Width = 89;
                }
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsResetPage = true;
            System.Drawing.Printing.PaperSize pageSize = this.getPaperSizeForOutput();
            p.SetPageSize(pageSize);

            p.PrintPreview(15, 10, this);
        }

        private System.Drawing.Printing.PaperSize getPaperSizeForOutput()
        {
            System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize();
            paperSize.PaperName = "ckd" + System.DateTime.Now.ToString();
            try
            {
                int width = 800;
                int curHeight = this.Height;
                int addHeight = 1;
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    addHeight = (this.neuSpread1_Sheet1.RowCount - 1) * (int)this.neuSpread1_Sheet1.Rows[0].Height;
                }
                //Epson 300K+ 额外增加高度 = 三行farPoint高度
                int additionAddHeight = 3 * (int)this.neuSpread1_Sheet1.Rows[0].Height;

                paperSize.Width = width;
                paperSize.Height = (addHeight + curHeight + additionAddHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置出库打印纸张出错>>" + ex.Message);
            }
            return paperSize;
        }

        class sortOutputByBarCode : System.Collections.IComparer
        {
            public sortOutputByBarCode()
            {
            }
            public int Compare(object x1, object x2)
            {
                FS.HISFC.Components.Speciment.OutInfo o1 = x1 as FS.HISFC.Components.Speciment.OutInfo;
                FS.HISFC.Components.Speciment.OutInfo o2 = x2 as FS.HISFC.Components.Speciment.OutInfo;
                if (o1 == null || o2 == null)
                {
                    throw new Exception("类型不符合Output");
                }
                string oX = o1.Position + o1.SubRow + o1.SubCol;
                string oY = o2.Position + o2.SubRow + o2.SubCol;
                int result = string.Compare(oX, oY);
                return result;
            }
        }
    }

    public enum Cols
    {
        /// <summary>
        /// 0条码号
        /// </summary>
        barCode,
        /// <summary>
        /// 1标本号
        /// </summary>
        specid,
        /// <summary>
        /// 2行
        /// </summary>
        row,
        /// <summary>
        /// 3列
        /// </summary>
        column,
        /// <summary>
        /// 4位置
        /// </summary>
        position,
        /// <summary>
        /// 5数量
        /// </summary>
        qty,
        /// <summary>
        /// 6病种
        /// </summary>
        dis,
        /// <summary>
        /// 7类型
        /// </summary>
        type,
        /// <summary>
        /// 8性质
        /// </summary>
        qly,
        /// <summary>
        /// 9是否归还
        /// </summary>
        rt,
        /// <summary>
        /// 10备注
        /// </summary>
        memo

    }
}
