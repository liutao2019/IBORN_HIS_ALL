using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Pharmacy;

namespace Neusoft.SOC.Local.OutpatientFee.NanZhuang
{
    public partial class ucHerbalDrugList : UserControl
    {
        public ucHerbalDrugList()
        {
            InitializeComponent();
        }

        #region ����

        Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();
        #endregion

        /// <summary>
        /// ���������뷽��
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("OutPatientDrugBill", 450, 400));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        /// <summary>
        /// ����ؼ�����
        /// </summary>
        private void Clear()
        {
            lbName.Text = "";
            lbRecipe.Text = "";
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        public void ShowData(ArrayList alData, string diagnose, Neusoft.HISFC.Models.Registration.Register register, string hospitalName, DateTime printTime, int pageNO, int maxPageNO)
        {

            #region ��ϸ��Ϣ
            decimal totCost = 0;

            decimal days = 0;
            this.neuSpread1_Sheet1.RowCount = 0;

            //����������Ҫ���ӣ�������Ҫ��ҳ������ʱע�����һ�еĻ�����Ϣ
            this.neuSpread1_Sheet1.RowCount = 13;
            int index = 0;
            string memo = "";
            for (int rowIndex = 0; rowIndex < alData.Count && index < alData.Count; rowIndex++)
            {
                //��һ��ҩ
                feeItemObj = alData[index] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                
                this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text = feeItemObj.Item.Name;
                this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text = (feeItemObj.FeePack == "1" ? (feeItemObj.Item.Qty / feeItemObj.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') : feeItemObj.Item.Qty.ToString("F4").TrimEnd('0').TrimEnd('.')) + feeItemObj.Item.PriceUnit;
                this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsage(feeItemObj.Order.Usage.ID).Name;

                Neusoft.HISFC.Models.Order.OutPatient.Order order = SOC.Local.DrugStore.Common.Function.GetOrder(feeItemObj.Order.ID);
                if (order != null && (order.Memo.Contains("�Լ�") || order.Memo.Contains("����") || order.Memo.Contains("��")))
                {
                    if (string.IsNullOrEmpty(memo))
                    {
                        memo = order.Memo;
                    }
                    else if (!memo.Contains(order.Memo))
                    {
                        if (order.Memo.Contains(memo))
                        {
                            memo = order.Memo;
                        }
                        else
                        {
                            memo = memo.TrimEnd(',') + "," + order.Memo;
                        }
                    }
                }
                days = feeItemObj.Days;
                if (feeItemObj.Item.PackQty == 0)
                {
                    feeItemObj.Item.PackQty = 1;
                }
                feeItemObj = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();
                index++;
                //�ڶ���ҩ
                if (index < alData.Count)
                {
                    feeItemObj = alData[index] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;

                    this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text = feeItemObj.Item.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text = (feeItemObj.FeePack == "1"?(feeItemObj.Item.Qty/feeItemObj.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.'):feeItemObj.Item.Qty.ToString("F4").TrimEnd('0').TrimEnd('.'))+ feeItemObj.Item.PriceUnit;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 5].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsage(feeItemObj.Order.Usage.ID).Name;

                    order = SOC.Local.DrugStore.Common.Function.GetOrder(feeItemObj.Order.ID);
                    if (order != null && (order.Memo.Contains("�Լ�") || order.Memo.Contains("����") || order.Memo.Contains("��")))
                    {
                        if (string.IsNullOrEmpty(memo))
                        {
                            memo = order.Memo;
                        }
                        else if (!memo.Contains(order.Memo))
                        {
                            if (order.Memo.Contains(memo))
                            {
                                memo = order.Memo;
                            }
                            else
                            {
                                memo = memo.TrimEnd(',') + "," + order.Memo;
                            }
                        }
                    }

                    if (feeItemObj.Item.PackQty == 0)
                    {
                        feeItemObj.Item.PackQty = 1;
                    }
                    feeItemObj = new Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList();
                    index++;
                }
            }

            //���һ��
            if (maxPageNO > 1)
            {
                this.lbRecipe.Text = "�����ţ�" + register.RecipeNO + "  " + pageNO.ToString() + "/" + maxPageNO.ToString();
                //��ҳ����ʾ
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).RowSpan = this.neuSpread1_Sheet1.ColumnCount;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Value = "ע�⣺�������� " + maxPageNO.ToString() + " ҳ" + "  �� " + days.ToString("F4").TrimEnd('0').TrimEnd('.') + " ��  " + memo + "      ";
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Font = new Font("����", 11F, FontStyle.Bold);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            }
            else
            {
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Value = "�� " + days.ToString("F4").TrimEnd('0').TrimEnd('.') + " ��  " + memo + "      ";
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
                this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Font = new Font("����", 11F, FontStyle.Bold);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            }
            Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemObj1 = alData[0] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
            this.nlbPrintTime.Text = "ҽ����" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(feeItemObj1.RecipeOper.ID) +"              "+printTime.ToString();
            #endregion
        }

        public int Print(ArrayList alData, string diagnose, Neusoft.HISFC.Models.Registration.Register register, string hospitalName, DateTime printTime,string isReprint)
        {
            this.Clear();
            if (isReprint=="0")
            {
                this.lbReprint.Visible = false;
            }
            else
            {
                this.lbReprint.Visible = true;
            }
            if (alData == null || register == null)
            {
                return -1;
            }

            #region ������Ϣ
            //����
            lbName.Text = register.Name + "  " + register.Sex.Name + "  " + (new Neusoft.FrameWork.Management.DataBaseManger()).GetAge(register.Birthday);
            feeItemObj = (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList)alData[0];
            //������
            this.lbRecipe.Text = "�����ţ�" + feeItemObj.RecipeNO;

            #endregion

            //��ҳ
            decimal maxPageNO = Math.Ceiling((decimal)alData.Count / 25);
            for (int pageNO = 0; pageNO < maxPageNO; pageNO++)
            {
                int count = 25;
                if (count + pageNO * 25 > alData.Count)
                {
                    count = alData.Count - pageNO * 25;
                }
                ArrayList alOnePage = new ArrayList();
                alOnePage = alData.GetRange(pageNO * 25, count);
                this.ShowData(alOnePage, diagnose, register, hospitalName, printTime, pageNO + 1, (int)maxPageNO);

                this.PrintPage();
            }


            return 0;
        }
    }
   
    
}
