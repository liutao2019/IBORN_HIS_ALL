using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    public partial class ucNurseCellSendQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucNurseCellSendQuery()
        {
            InitializeComponent();
            this.ShowNurseSend();
        }

        private FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

        private void ShowNurseSend()
        {
            ArrayList allDeptInfo = this.drugStoreMgr.QueryNurseDruged();
            if (allDeptInfo == null || allDeptInfo.Count == 0)
            {
                return;
            }
            this.SetData(allDeptInfo);
        }

        /// <summary>
        /// 设置数据显示
        /// </summary>
        /// <param name="alStorage"></param>
        private void SetData(ArrayList allDeptInfo)
        {
            if (allDeptInfo == null || allDeptInfo.Count == 0)
            {
                return;
            }
            this.SetFarpoint();
            this.SetFarpointDetail(allDeptInfo);
        }

     
        private void SetFarpoint()
        {
            this.SetFarpointColumns();
            this.SetFarpontDataType();
        }

        private void SetFarpontDataType()
        {
            for(int index = 0;index < this.neuSpreadDetail_Sheet1.Columns.Count;index++)
            {
                this.neuSpreadDetail_Sheet1.Columns[index].Locked = true;
            }
        }

        private void SetFarpointDetail(ArrayList allDeptInfo)
        {
            int index = 0;
            foreach (FS.FrameWork.Models.NeuObject deptInfo in allDeptInfo)
            {
                string WarningType = "0";
                if (string.IsNullOrEmpty(deptInfo.User02)||string.IsNullOrEmpty(deptInfo.User03))
                {
                    WarningType = "2";
                }
                if (string.IsNullOrEmpty(deptInfo.User01))
                {
                    WarningType = "1";
                }
                if (index % 2 == 0)
                {
                    this.neuSpreadDetail_Sheet1.Rows.Add(this.neuSpreadDetail_Sheet1.Rows.Count, 1);

                    this.neuSpreadDetail_Sheet1.Rows[this.neuSpreadDetail_Sheet1.Rows.Count - 1].BackColor = System.Drawing.Color.White;

                    this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 0].Text = deptInfo.Name;
                    this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 1].Text = string.IsNullOrEmpty(deptInfo.User01) ? "" : FS.FrameWork.Function.NConvert.ToDateTime(deptInfo.User01).ToString("HH:mm:ss");
                    this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 2].Text = string.IsNullOrEmpty(deptInfo.User02) ? "" : FS.FrameWork.Function.NConvert.ToDateTime(deptInfo.User02).ToString("HH:mm:ss");
                    this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 3].Text = string.IsNullOrEmpty(deptInfo.User03) ? "" : FS.FrameWork.Function.NConvert.ToDateTime(deptInfo.User03).ToString("HH:mm:ss");
                    if (WarningType == "2")
                    {
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 0].BackColor = System.Drawing.Color.Red;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 1].BackColor = System.Drawing.Color.Red;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 2].BackColor = System.Drawing.Color.Red;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 3].BackColor = System.Drawing.Color.Red;
                    }
                    else if (WarningType == "1")
                    {
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 0].BackColor = System.Drawing.Color.Yellow;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 1].BackColor = System.Drawing.Color.Yellow;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 2].BackColor = System.Drawing.Color.Yellow;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 3].BackColor = System.Drawing.Color.Yellow;
                    }
                }
                else
                {
                    this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 4].Text = deptInfo.Name;
                    this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 5].Text = string.IsNullOrEmpty(deptInfo.User01)?"":FS.FrameWork.Function.NConvert.ToDateTime(deptInfo.User01).ToString("HH:mm:ss");
                    this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 6].Text = string.IsNullOrEmpty(deptInfo.User02) ? "" : FS.FrameWork.Function.NConvert.ToDateTime(deptInfo.User02).ToString("HH:mm:ss");
                    this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 7].Text = string.IsNullOrEmpty(deptInfo.User03) ? "" : FS.FrameWork.Function.NConvert.ToDateTime(deptInfo.User03).ToString("HH:mm:ss");
                    if (WarningType == "2")
                    {
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 4].BackColor = System.Drawing.Color.Red;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 5].BackColor = System.Drawing.Color.Red;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 6].BackColor = System.Drawing.Color.Red;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 7].BackColor = System.Drawing.Color.Red;
                    }
                    else if (WarningType == "1")
                    {
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 4].BackColor = System.Drawing.Color.Yellow;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 5].BackColor = System.Drawing.Color.Yellow;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 6].BackColor = System.Drawing.Color.Yellow;
                        this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, 7].BackColor = System.Drawing.Color.Yellow;
                    }
                }
               
                index++;
            }
        }

        private void SetFarpointColumns()
        {
            FarPoint.Win.Spread.CellType.NumberCellType n1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            n1.DecimalPlaces = 2;
            FarPoint.Win.Spread.CellType.TextCellType t1 = new FarPoint.Win.Spread.CellType.TextCellType();
            t1.WordWrap = true;
            //FarPoint.Win.Spread.CellType.DateTimeCellType t1 = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            //t1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            //t1.UserDefinedFormat = "HH:mm:ss";
            System.Drawing.Font f1 = new Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            System.Drawing.Font f2 = new Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpreadDetail_Sheet1.Columns.Count = (int)EnumColumnSet.口服配药时间1 + 1;
            this.neuSpreadDetail_Sheet1.Rows.Add(0, 1);
            this.neuSpreadDetail_Sheet1.Rows[0].Height = 45F;

            //#region 设置赋值
         
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区名称].Width = 150F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区名称].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区名称].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区名称].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.病区名称].Text = "病区名称";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.病区名称].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区发送时间].Width = 80;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区发送时间].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区发送时间].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区发送时间].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区发送时间].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.病区发送时间].Text = "病区发送时间";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.病区发送时间].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.针剂配药时间].Width = 80;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.针剂配药时间].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.针剂配药时间].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.针剂配药时间].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.针剂配药时间].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.针剂配药时间].Text = "针剂配药时间";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.针剂配药时间].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.口服配药时间].Width = 80;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.口服配药时间].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.口服配药时间].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.口服配药时间].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.口服配药时间].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.口服配药时间].Text = "口服配药时间";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.口服配药时间].Font = f1;



            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区名称1].Width = 150F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区名称1].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区名称1].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区名称1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区名称1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.病区名称1].Text = "病区名称";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.病区名称1].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区发送时间1].Width = 80;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区发送时间1].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区发送时间1].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区发送时间1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.病区发送时间1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.病区发送时间1].Text = "病区发送时间";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.病区发送时间1].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.针剂配药时间1].Width = 80;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.针剂配药时间1].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.针剂配药时间1].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.针剂配药时间1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.针剂配药时间1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.针剂配药时间1].Text = "针剂配药时间";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.针剂配药时间1].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.口服配药时间1].Width = 80;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.口服配药时间1].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.口服配药时间1].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.口服配药时间1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.口服配药时间1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.口服配药时间1].Text = "口服配药时间";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.口服配药时间1].Font = f1;
            
            //#endregion
        }

        private void Clear()
        {
            this.neuSpreadDetail_Sheet1.RowCount = 0;
        }
      
    }

    enum EnumColumnSet
    { 
        病区名称,
        病区发送时间,
        针剂配药时间,
        口服配药时间,
        病区名称1,
        病区发送时间1,
        针剂配药时间1,
        口服配药时间1,
    }
}
