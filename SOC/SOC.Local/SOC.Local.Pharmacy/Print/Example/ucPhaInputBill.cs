using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.Print.Example
{
    /// <summary>
    /// ��ݸҩƷ��ⵥ�ݴ�ӡ
    /// </summary>
    public partial class ucPhaInputBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// ҩƷ����ӡ��
        /// </summary>
        public ucPhaInputBill()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���д���ӡ����
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        /// <summary>
        /// ���ݵ��ܽ��
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        /// <summary>
        /// 
        /// </summary>
        private class TotCost
        {
            public decimal purchaseCost;
            public decimal retailCost;
        }

        #endregion


        #region ��ⵥ��ӡ
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <param name="i">�ڼ�ҳ</param>
        /// <param name="count">��ҳ��</param>
        /// <param name="title">����</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
            if (al.Count <= 0)
            {
                MessageBox.Show("û�д�ӡ������!");
                return;
            }
            if (icount <= 0)
            {
                icount = 1;
            }
            FS.HISFC.Models.Pharmacy.Input info = (FS.HISFC.Models.Pharmacy.Input)al[0];

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

            string company = "";
            if (info.SourceCompanyType == "1")
            {
                company = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.Company.ID);
            }
            else
            {
                company = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(info.Company.ID);
            }
            this.lblCompany.Text = "�ͻ���λ:" + info.Company.ID + " " + company;
            
            this.lblBillID.Text = "���ֺ�:" + info.InListNO;
            this.lblInputDate.Text = "��������:" + info.InDate.ToShortDateString();
            this.lblOper.Text = "�Ʊ���:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "ҳ:"+inow.ToString() + "/" + icount.ToString();
            this.neuLabel9.Text = "�ɹ�Ա:";// +BillPrintFun.GetStockPlanPersonName(info.StockDept.ID);
            this.neuLabel10.Text = "�ֹ�Ա:";// +BillPrintFun.GetStockManagerName(info.StockDept.ID);//BillPrintFun.GetEmplName(info.Operation.ExamOper.ID);

            #endregion

            #region farpoint��ֵ
            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;
            //������
            decimal sumPCZret = 0;
            decimal sumPCZpur = 0;
            //��������
            decimal sumBM = 0;
            //ԭ����
            decimal sumM = 0;

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Input input = al[i] as FS.HISFC.Models.Pharmacy.Input;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID).UserCode; //ҩƷ�Զ�����
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = input.Item.Name;//ҩƷ����
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = input.Item.Specs;//���		
                if (input.Item.PackQty == 0)
                    input.Item.PackQty = 1;
                decimal count = 0;
                count = input.Quantity;

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = n;

                if (input.ShowState == "0")
                {
                   
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.Item.MinUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count).ToString();//����				
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (input.Item.PriceCollection.PurchasePrice / input.Item.PackQty).ToString("F4");
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = (input.Item.PriceCollection.WholeSalePrice / input.Item.PackQty).ToString("F4");
                    // input.PriceCollection.RetailPrice;//���ۼ۸���Ϊ�������ֶ�
            
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.Item.PackUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count / input.Item.PackQty).ToString("F2");//����			
	

                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.Item.PriceCollection.PurchasePrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.Item.PriceCollection.WholeSalePrice;// input.PriceCollection.RetailPrice;//���ۼ۸���Ϊ�������ֶ�
                }
                //this.neuFpEnter1_Sheet1.Cells[i, 5].Text = BillPrintFun.GetItem(input.Item.ID).Product.ProducingArea;
                               
                if (string.IsNullOrEmpty(input.Producer.ID))
                { 
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = ""; 
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(input.Producer.ID);
                }

                this.neuFpEnter1_Sheet1.Cells[i, 6].Text = input.BatchNO;
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.PurchaseCost;


                switch (input.Item.Type.ID)
                {
                    case "B":
                        sumBM += input.WholeSaleCost;
                        break;
                    case "M":
                        sumM += input.PurchaseCost;
                        break;
                    case "C":
                    case "P":
                    case "Z":
                    default:
                        sumPCZret += input.WholeSaleCost;
                        sumPCZpur += input.PurchaseCost;
                        break;
                }

                sumRetail = sumRetail + input.WholeSaleCost;
                sumPurchase = sumPurchase + input.PurchaseCost;
                sumDif = sumRetail - sumPurchase;

          }
            //��ǰҳ����
            this.lblCurRet.Text = "��ҳ���۽��:"+sumRetail.ToString("F4");
            this.lblCurPur.Text = "��ҳ������:" + sumPurchase.ToString("F4");
            this.lblCurDif.Text = "��ҳ�����:" + sumDif.ToString("F4");
            //ҩƷ����
            this.lblRetailCost.Text = "ҩƷ���:" + sumPCZret.ToString("F4");
            this.lblPurchaseCost.Text = "ҩƷ����:" + sumPCZpur.ToString("F4");
            this.lblDifference.Text = "ҩƷ��:" + (sumPCZret - sumPCZpur).ToString("F4");
            this.lblStuff.Text = "��������:" + sumBM.ToString("F4");
            this.lblBaseStuff.Text = "ԭ����:" + sumM.ToString("F4");

            //������
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString("F4");
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.InListNO]).retailCost.ToString("F4");
            this.lblTotDif.Text = (((TotCost)hsTotCost[info.InListNO]).retailCost - ((TotCost)hsTotCost[info.InListNO]).purchaseCost).ToString("F4");
    
            #endregion

            this.resetTitleLocation();

        }

        /// <summary>
        /// �������ñ���λ��
        /// </summary>
        private void resetTitleLocation()
        {
            this.neuPanel4.Controls.Remove(this.lblTitle);
            int with = 0;
            for (int col = 0; col < this.neuFpEnter1_Sheet1.ColumnCount; col++)
            {
                if (this.neuFpEnter1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.neuFpEnter1_Sheet1.Columns[col].Width;
                }
            }
            if (with > this.neuPanel4.Width)
            {
                with = this.neuPanel4.Width;
            }
            this.lblTitle.Location = new Point((with - this.lblTitle.Size.Width) / 2, this.lblTitle.Location.Y);
            this.neuPanel4.Controls.Add(this.lblTitle);

        }
        #endregion

        #region IPharmacyBill ��Ա

        private Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.Base.PrintBill();

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
            int height = this.neuPanel5.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            
            //�����ʱ������ж��ŵ��ݣ��ֿ�
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Input input in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.Input i = input.Clone();
                if (hs.Contains(i.InListNO))
                {

                    ArrayList al = (ArrayList)hs[i.InListNO];
                    al.Add(i);

                    TotCost tc = (TotCost)hsTotCost[i.InListNO];
                    tc.purchaseCost += i.PurchaseCost;
                    tc.retailCost += i.WholeSaleCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(i);
                    hs.Add(i.InListNO, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = i.PurchaseCost;
                    tc.retailCost = i.WholeSaleCost;
                    hsTotCost.Add(input.InListNO, tc);
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
                    this.SetPrintData(al, pageNow+1, pageTotNum, printBill.Title);

                    this.neuPanel5.Height += (int)rowHeight * al.Count;
                    this.Height += (int)rowHeight * al.Count;

                    if (this.printBill.IsNeedPreview)
                    {
                        p.PrintPreview(5, 0, this.neuPanel1);
                    }
                    else
                    {
                        p.PrintPage(5, 0, this.neuPanel1);
                    }

                    this.neuPanel5.Height = height;
                    this.Height = ucHeight;
                }
            }
            #endregion

            return 1;
        }

        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            if (alPrintData != null && alPrintData.Count > 0)
            {
                string bill = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).InListNO;
                string dept = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).StockDept.ID;
                FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
                ArrayList al = itemMgr.QueryInputInfoByListID(dept, bill, "AAAA", "AAAA");
                this.alPrintData = al;
            }
            //this.alPrintData = alPrintData;
            this.printBill = printBill;
            return this.Print();
        }

        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

        #endregion
    }
}
