using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Pharmacy.Print.NanZhuang;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.Print.HuanShi
{
    /// <summary>
    /// ��ݸҩƷ���ⵥ�ݴ�ӡ
    /// </summary>
    public partial class ucPhaOutputBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// ҩƷ����ӡ��
        /// </summary>
        public ucPhaOutputBill()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���д���ӡ����
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        FS.SOC.HISFC.BizLogic.Pharmacy.InOut item = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

        /// <summary>
        /// ���ݵ��ܽ��
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        public decimal storageSum;

        /// <summary>
        /// 
        /// </summary>
        private class TotCost
        {
            public decimal purchaseCost;
            public decimal retailCost;
        }

        #endregion

        #region ��ӡ������


        #region ��ⵥ��ӡ
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <param name="i">�ڼ�ҳ</param>
        /// <param name="count">��ҳ��</param>
        /// <param name="operCode">����</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
           
            if (al.Count <= 0)
            {
                MessageBox.Show("û�д�ӡ������!");
                return;
            }
            FS.HISFC.Models.Pharmacy.Output info = (FS.HISFC.Models.Pharmacy.Output)al[0];

            #region label��ֵ
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {
                if (title.IndexOf("[������]") != -1)
                {
                    this.lblTitle.Text = title.Replace("[������]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                }
                else
                {
                    this.lblTitle.Text = title;
                }
            }
            //������ע
            if (string.IsNullOrEmpty(info.Memo)||"���뼴��"== info.Memo)
            {
                if (!string.IsNullOrEmpty(info.GetPerson))
                {
                    this.lblMemo.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.GetPerson) + "   "+ info.Memo;
                }
            }
            else
            {
                this.lblMemo.Text = info.Memo.ToString();
            }

            this.lblCompany.Text = "���ϵ�λ: " + info.TargetDept.ID + " " + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
            this.lblBillID.Text = "���Ϻ�: " + info.OutListNO;
            this.lblInputDate.Text = "��������: " + info.OutDate.ToShortDateString();
            this.lblOper.Text = "�Ʊ���:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID) + "   " + DateTime.Now.ToString(); ;
            this.lblPage.Text = "ҳ��" + inow.ToString() + "/" + icount.ToString();
            this.neuLabel10.Visible = true;
           
            #endregion

            #region farpoint��ֵ
            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;
            decimal sumDrugWholeCost = 0;
            decimal sumDrugPurCost = 0;
            
            decimal sumBWholeCost = 0;
            decimal sumBPurCost = 0;
            decimal sumPPurCost = 0;
            decimal sumCPurCost = 0;
            decimal sumZPurCost = 0;
            decimal sumYPurCost = 0;
            decimal sumSPurCost = 0;
            decimal sumMPurCost = 0;

            this.neuFpEnter1_Sheet1.RowCount = 0;
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut inOutMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            ArrayList allData = inOutMgr.QueryOutputInfo(info.StockDept.ID,info.OutListNO,"A");
            if (allData.Count != 0 && allData != null)
            {
                for (int i = 0; i < allData.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.Output outPutModels = allData[i] as FS.HISFC.Models.Pharmacy.Output;

                    switch (outPutModels.Item.Type.ID)
                    {
                        case "C":
                            sumCPurCost += outPutModels.PurchaseCost;
                            break;
                        case "Z":
                            sumZPurCost += outPutModels.PurchaseCost;
                            break;
                        case "Y":
                            sumYPurCost += outPutModels.PurchaseCost;
                            break;
                        case "S":
                            sumSPurCost += outPutModels.PurchaseCost;
                            break;
                        case "P":
                            sumPPurCost += outPutModels.PurchaseCost;
                            break;
                        case "M":
                            sumMPurCost += outPutModels.PurchaseCost;
                            break;

                    }
                
                }
            }
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);


                //this.neuFpEnter1_Sheet1.Columns[2].Width = 81F;

                FS.HISFC.Models.Pharmacy.Output output = al[i] as FS.HISFC.Models.Pharmacy.Output;
                item.GetStorageNum(output.StockDept.ID, output.Item.ID, out  storageSum);
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID).UserCode; //ҩƷ�Զ�����
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = output.Item.Name;//ҩƷ����
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = SOC.HISFC.BizProcess.Cache.Common.GetDrugTypeName(output.Item.Type.ID);//ҩƷ���
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = output.Item.Specs;//���		
                if (output.Item.PackQty == 0)
                    output.Item.PackQty = 1;
                decimal count = 0;
                count = output.Quantity;
                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 2;
                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = Function.GetCostDecimal();

                this.neuFpEnter1_Sheet1.Columns[6].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[10].CellType = n;
                if (output.ShowState=="0")
                {

                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = output.Item.PackUnit; ;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count / output.Item.PackQty).ToString();//����				
                  
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = output.Item.PackUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count / output.Item.PackQty).ToString("F2");//����				
            
                }
               
                switch(output.Item.Type.ID)
                {
                    case "B":
                        sumBPurCost += output.PurchaseCost;
                        sumBWholeCost += output.RetailCost;
                        break;

                    default:
                        sumDrugPurCost += output.PurchaseCost;
                        sumDrugWholeCost += output.RetailCost;
                        break;

                }
            
                this.neuFpEnter1_Sheet1.Cells[i, 6].Value = output.Item.PriceCollection.PurchasePrice;
                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = output.PurchaseCost;

                //if (output.Item.Type.ID.ToString() == "P" ||output.Item.Type.ID.ToString() == "Z")
                //{
                //    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.Item.PriceCollection.RetailPrice;
                //    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = output.RetailCost;
                //    this.neuFpEnter1_Sheet1.Cells[i, 10].Value = (FS.FrameWork.Function.NConvert.ToDecimal(output.PurchaseCost) * FS.FrameWork.Function.NConvert.ToDecimal(0.15)).ToString();
                //    this.neuFpEnter1_Sheet1.Cells[i, 11].Value = (FS.FrameWork.Function.NConvert.ToDecimal(output.RetailCost) / FS.FrameWork.Function.NConvert.ToDecimal(output.PurchaseCost)).ToString("F2");
                //}
                //else if (output.Item.Type.ID.ToString() == "C")
                //{
                //    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = (FS.FrameWork.Function.NConvert.ToDecimal(output.Item.PriceCollection.PurchasePrice) * FS.FrameWork.Function.NConvert.ToDecimal(1.25)).ToString();
                //    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = (FS.FrameWork.Function.NConvert.ToDecimal(output.PurchaseCost) * FS.FrameWork.Function.NConvert.ToDecimal(1.25)).ToString();
                //    this.neuFpEnter1_Sheet1.Cells[i, 10].Value = (FS.FrameWork.Function.NConvert.ToDecimal(output.PurchaseCost) * FS.FrameWork.Function.NConvert.ToDecimal(0.25)).ToString();
                //    this.neuFpEnter1_Sheet1.Cells[i, 11].Value = 1.25;
                //}
                //else
                //{
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.Item.PriceCollection.RetailPrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = output.RetailCost;
                    this.neuFpEnter1_Sheet1.Cells[i, 10].Value = output.RetailCost - output.PurchaseCost;
                    this.neuFpEnter1_Sheet1.Cells[i, 11].Value = (FS.FrameWork.Function.NConvert.ToDecimal(output.RetailCost) / FS.FrameWork.Function.NConvert.ToDecimal(output.PurchaseCost)).ToString("F2");
                //}
                this.neuFpEnter1_Sheet1.Cells[i, 12].Value = (FS.FrameWork.Function.NConvert.ToDecimal(output.StoreQty) / FS.FrameWork.Function.NConvert.ToDecimal(output.Item.PackQty)).ToString("F0");
                

                if (output.ValidTime > DateTime.MinValue)
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 13].Text = output.ValidTime.Date.ToString("yyyy-MM-dd");
                }

                if (output.Item.Type.ID.ToString() == "P" || output.Item.Type.ID.ToString() == "Z")
                {
                    sumRetail = sumRetail + FS.FrameWork.Function.NConvert.ToDecimal(output.RetailCost);
                }
                else if (output.Item.Type.ID.ToString() == "C")
                {
                    sumRetail = sumRetail + FS.FrameWork.Function.NConvert.ToDecimal(output.PurchaseCost) * FS.FrameWork.Function.NConvert.ToDecimal(1.25);
                }
                else
                {
                    sumRetail = sumRetail + output.RetailCost;
                }
                sumPurchase += output.PurchaseCost;
                sumDif = sumRetail - sumPurchase;
            }
            //��ǰҳ����
            this.lblCurRet.Text = "��ҳ���۽��  ��" + sumRetail.ToString(Function.GetCostDecimalString());
            this.lblCurPur.Text = "��ҳ�����" + sumPurchase.ToString(Function.GetCostDecimalString());
            this.lblCurDif.Text = "��ҳ����" + sumDif.ToString(Function.GetCostDecimalString());

            string sumByDrugType = "";
            if (sumPPurCost != 0)
            {
                sumByDrugType += "��ҩ:" + sumPPurCost.ToString("F2") + "\n";
            }
            if (sumZPurCost != 0)
            {
                sumByDrugType += "��ҩ:" + sumZPurCost.ToString("F2") + "\n";
            }
            if (sumCPurCost != 0)
            {
                sumByDrugType += "��ҩ:" + sumCPurCost.ToString("F2") + "\n";
            }
            if (sumYPurCost != 0)
            {
                sumByDrugType += "����:" + sumYPurCost.ToString("F2") + "\n";
            }
            if (sumSPurCost != 0)
            {
                sumByDrugType += "����:" + sumSPurCost.ToString("F2") + "\n";
            }
            if (sumMPurCost != 0)
            {
                sumByDrugType += "�Ĳ�:" + sumMPurCost.ToString("F2") + "\n";
            }
            this.neuLabel3.Text = sumByDrugType;
           
            //������
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.OutListNO]).purchaseCost.ToString(Function.GetCostDecimalString());
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.OutListNO]).retailCost.ToString(Function.GetCostDecimalString());
            //this.lblTotDif.Text = (((TotCost)hsTotCost[info.OutListNO]).retailCost - ((TotCost)hsTotCost[info.OutListNO]).purchaseCost).ToString(Function.GetCostDecimalString());
            this.neuLabel2.Text = "��ë��: " + (((TotCost)hsTotCost[info.OutListNO]).retailCost - ((TotCost)hsTotCost[info.OutListNO]).purchaseCost).ToString(Function.GetCostDecimalString());

            #endregion

            this.resetTitleLocation();

        }

        /// <summary>
        /// �������ñ���λ��
        /// </summary>
        private void resetTitleLocation()
        {
            this.neuPanel3.Controls.Remove(this.lblTitle);
            int with = 0;
            for (int col = 0; col < this.neuFpEnter1_Sheet1.ColumnCount; col++)
            {
                if (this.neuFpEnter1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.neuFpEnter1_Sheet1.Columns[col].Width;
                }
            }
            if (with > this.neuPanel3.Width)
            {
                with = this.neuPanel3.Width;
            }
            this.lblTitle.Location = new Point((with - this.lblTitle.Size.Width) / 2, this.lblTitle.Location.Y);
            this.neuPanel3.Controls.Add(this.lblTitle);

        }

        #endregion

        #endregion

        #region IBillPrint ��Ա

        #region IPharmacyBill ��Ա

        private Base.PrintBill printBill = new Base.PrintBill();

        /// <summary>
        /// IBillPrint��ԱPrint
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            #region ��ӡ��Ϣ����
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region ��ҳ��ӡ
            int height = this.neuPanel4.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.��λ��)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.Base.PrintBill.SortType.����˳��)
            {
                 Base.PrintBill.SortByPlaceCodeAndCustomCode(ref alPrintData);
            }

            //�����ʱ������ж��ŵ��ݣ��ֿ�
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Output output in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.Output o = output.Clone();
                if (hs.Contains(o.OutListNO))
                {

                    ArrayList al = (ArrayList)hs[o.OutListNO];
                    al.Add(o);

                    TotCost tc = (TotCost)hsTotCost[o.OutListNO];
                    tc.purchaseCost += o.PurchaseCost;
                    if (o.Item.Type.ID.ToString() == "P" || o.Item.Type.ID.ToString() == "Z")
                    {
                        tc.retailCost += FS.FrameWork.Function.NConvert.ToDecimal(o.RetailCost);
                    }
                    else if (o.Item.Type.ID.ToString() == "C")
                    {
                        tc.retailCost += FS.FrameWork.Function.NConvert.ToDecimal(o.PurchaseCost) * FS.FrameWork.Function.NConvert.ToDecimal(1.25);
                    }
                    else
                    { 
                        tc.retailCost += o.RetailCost;
                    }
                    }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(o);
                    hs.Add(o.OutListNO, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = o.PurchaseCost;
                    if (o.Item.Type.ID.ToString() == "P" || o.Item.Type.ID.ToString() == "Z")
                    {
                        tc.retailCost += FS.FrameWork.Function.NConvert.ToDecimal(o.RetailCost);
                    }
                    else if (o.Item.Type.ID.ToString() == "C")
                    {
                        tc.retailCost += FS.FrameWork.Function.NConvert.ToDecimal(o.PurchaseCost) * FS.FrameWork.Function.NConvert.ToDecimal(1.25);
                    }
                    else
                    {
                        tc.retailCost = o.RetailCost;
                    }
                    hsTotCost.Add(o.OutListNO, tc);
                }
            }

            //�ֵ��ݴ�ӡ
            foreach (ArrayList alPrint in hs.Values)
            {
                int pageTotNum = alPrint.Count / this.printBill.RowCount;
                if (alPrint.Count != this.printBill.RowCount * pageTotNum)
                {
                    pageTotNum++;
                }

                //��ҳ��ӡ
                for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                {
                    ArrayList al = new ArrayList();

                    for (int index = pageNow * this.printBill.RowCount; index < alPrint.Count && index < (pageNow + 1) * this.printBill.RowCount; index++)
                    {
                        al.Add(alPrint[index]);
                    }
                   
                    this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

                    this.neuPanel4.Height = this.neuPanel4.Height + (int)rowHeight * al.Count;
                    this.Height += (int)rowHeight * al.Count;

                    if (this.printBill.IsNeedPreview)
                    {
                        p.PrintPreview(5, 0, this.neuPanel1);
                    }
                    else
                    {
                        p.PrintPage(5, 0, this.neuPanel1);
                    }

                    this.neuPanel4.Height = height;
                    this.Height = ucHeight;
                }
            }
            #endregion

            return 1;
        }

        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            if (this.alPrintData == null || alPrintData.Count == 0)
            {
                return 1;
            }
            FS.HISFC.Models.Pharmacy.Output output = alPrintData[0] as FS.HISFC.Models.Pharmacy.Output;
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            ArrayList al = itemMgr.QueryOutputInfo(output.StockDept.ID, output.OutListNO, output.State);
            if (al == null || al.Count == 0)
            {
                return 1;
            }
            this.alPrintData = al;
            this.printBill = printBill;
            return this.Print();
        }

        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

        #endregion

    

        #endregion
    }
}
