using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Pharmacy
{
    public partial class ucCheckBillByFp1 : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint
    {
        public ucCheckBillByFp1()
        {
            InitializeComponent();
        }

        #region IBillPrint 成员

        public int Prieview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.PrintPreview(40, 10, this);

            return 1;

        }

        public int SetData(System.Collections.ArrayList alPrintData, FS.HISFC.BizProcess.Interface.Pharmacy.BillType billType)
        {
            this.neuSpread1_Sheet1.Rows.Count = alPrintData.Count;

            int iIndex = 0;
            foreach (FS.HISFC.Models.Pharmacy.Check info in alPrintData)
            {
                if (iIndex == 0)
                {
                    this.lbInfo.Text = string.Format("科室：{0}   日期：{1}    盘点单号：{2}   盘点人：",info.StockDept.Name,info.Operation.Oper.OperTime.ToString(),info.CheckNO);
                }
                this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = info.PlaceNO;
                this.neuSpread1_Sheet1.Cells[iIndex, 1].Text = info.Item.Name + "[" + info.Item.Specs + "]";
                this.neuSpread1_Sheet1.Cells[iIndex, 2].Text = info.BatchNO;
                this.neuSpread1_Sheet1.Cells[iIndex, 3].Text = ""; //info.PackQty.ToString();
                this.neuSpread1_Sheet1.Cells[iIndex, 4].Text = info.Item.PackUnit;
                this.neuSpread1_Sheet1.Cells[iIndex, 5].Text = ""; //info.MinQty.ToString();
                this.neuSpread1_Sheet1.Cells[iIndex, 6].Text = info.Item.MinUnit;
                this.neuSpread1_Sheet1.Cells[iIndex, 7].Text = info.Memo;

                iIndex++;
            }

            return 1;
        }

        public int SetData(System.Collections.ArrayList alPrintData, string privType)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int SetData(string billNO)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
