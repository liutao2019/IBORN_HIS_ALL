using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.Print.BeiJiao
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

        private string drugType="";

        /// <summary>
        /// ��ӡ�������ֵ
        /// </summary>
        private Base.PrintBill printBill = new Base.PrintBill();

        #endregion

        /// <summary>
        /// 1�ⲿ���� 
        /// </summary>
        /// <param name="alPrintData"></param>
        /// <param name="printBill"></param>
        /// <returns></returns>
        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            if (alPrintData != null && alPrintData.Count > 0)
            {
                string bill = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).InListNO;
                string dept = (alPrintData[0] as FS.HISFC.Models.Pharmacy.Input).StockDept.ID;
                FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
                ArrayList al = itemMgr.QueryInputInfoByListID(dept, bill, "AAAA", "AAAA");
               
                Base.PrintBill.SortByOtherSpell(ref al);
               
                this.alPrintData = al;
                this.printBill = printBill;
                return this.Print();
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 2��ӡ����
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

                    if (hsTotCost.Contains(i.InListNO))
                    {
                        TotCost tc = (TotCost)hsTotCost[i.InListNO];
                        tc.purchaseCost += i.PurchaseCost;
                        tc.retailCost += i.RetailCost;
                    }
                    else
                    {
                        TotCost tc = new TotCost();
                        tc.purchaseCost = i.PurchaseCost;
                        tc.retailCost = i.RetailCost;
                        hsTotCost.Add(i.InListNO, tc);
                    }
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(i);
                    hs.Add(i.InListNO, al);

                    if (hsTotCost.Contains(i.InListNO))
                    {
                        TotCost tc = (TotCost)hsTotCost[i.InListNO];
                        tc.purchaseCost += i.PurchaseCost;
                        tc.retailCost += i.RetailCost;
                    }
                    else
                    {
                        TotCost tc = new TotCost();
                        tc.purchaseCost = i.PurchaseCost;
                        tc.retailCost = i.RetailCost;
                        hsTotCost.Add(i.InListNO, tc);
                    }
                }
            }

            //�ֵ��ݴ�ӡ
            foreach (ArrayList alPrintList in hs.Values)
            {
                int pageTotNum = alPrintList.Count / this.printBill.RowCount;
                if (alPrintList.Count != this.printBill.RowCount * pageTotNum)
                {
                    pageTotNum++;
                }

                ArrayList alPrint = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.Input item in alPrintList)
                {
                    alPrint.Add(item);
                }

                Base.PrintBill.SortByOtherSpell(ref alPrint);

                //��ҳ��ӡ
                for (int pageNow = 0; pageNow < pageTotNum; pageNow++)
                {
                    ArrayList al = new ArrayList();

                    for (int index = pageNow * this.printBill.RowCount; index < alPrint.Count && index < (pageNow + 1) * this.printBill.RowCount; index++)
                    {
                        al.Add(alPrint[index]);
                    }
                    this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

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

        /// <summary>
        /// 3��ֵ
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <param name="i">�ڼ�ҳ</param>
        /// <param name="count">��ҳ��</param>
        /// <param name="title">����</param>
        private void SetPrintData(ArrayList al, int inow, int icount, string title)
        {
            this.lblTitle.Text = "{0}ҩƷ��ⵥ({1})";
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
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID), drugType);
            }
            else
            {
                if (title.IndexOf("[������]") != -1)
                {
                    title = title.Replace("[������]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                }
                if (title.IndexOf("[ҩƷ����]") != -1)
                {
                    string tmpDrugType = "(" + this.drugType + ")";
                    title = title.Replace("[ҩƷ����]", tmpDrugType);
                }                
                    this.lblTitle.Text = title; 
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
            this.lblOper.Text = "�Ʊ���:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(FS.FrameWork.Management.Connection.Operator.ID);
            this.lblPage.Text = "ҳ:" + inow.ToString() + "/" + icount.ToString();
            
          
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

            //��Ʊ��
            string invoiceNO="";
            Hashtable hsInvoiceNO = new Hashtable();

            this.neuFpEnter1_Sheet1.RowCount = 0;

            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Input input = al[i] as FS.HISFC.Models.Pharmacy.Input;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID).NameCollection.OtherSpell.SpellCode; //ҩƷ�Զ�����
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = input.Item.Name;//ҩƷ����
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = input.Item.Specs;//���		
                if (input.Item.PackQty == 0)
                    input.Item.PackQty = 1;
                decimal count = 0;
                count = input.Quantity;

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;
                this.neuFpEnter1_Sheet1.Columns[6].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = n;

                if (input.ShowState == "0")
                {

                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.Item.MinUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count).ToString();//����				
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = (input.Item.PriceCollection.PurchasePrice / input.Item.PackQty).ToString("F4");
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = (input.Item.PriceCollection.RetailPrice / input.Item.PackQty).ToString("F4");

                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.Item.PackUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = (count / input.Item.PackQty).ToString("F2");//����			


                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = input.Item.PriceCollection.PurchasePrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.Item.PriceCollection.RetailPrice;
                }

                this.neuFpEnter1_Sheet1.Cells[i, 5].Text = input.BatchNO;
                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.PurchaseCost;
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.RetailCost;
                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = input.ValidTime.ToShortDateString();
                this.neuFpEnter1_Sheet1.Cells[i, 11].Value = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(input.Producer.ID);


                switch (input.Item.Type.ID)
                {
                    case "B":
                        sumBM += input.RetailCost;
                        break;
                    case "M":
                        sumM += input.PurchaseCost;
                        break;
                    case "C":
                    case "P":
                    case "Z":
                    default:
                        sumPCZret += input.RetailCost;
                        sumPCZpur += input.PurchaseCost;
                        break;
                }

                sumRetail = sumRetail + input.RetailCost;
                sumPurchase = sumPurchase + input.PurchaseCost;
                sumDif = sumRetail - sumPurchase;

                if (!hsInvoiceNO.Contains(input.InvoiceNO))
                {
                    hsInvoiceNO.Add(input.InvoiceNO, input.InvoiceNO);
                }
            }
            //��ǰҳ����
            this.lblCurRet.Text = "��ҳ���۽��:"+sumRetail.ToString("F4");
            this.lblCurPur.Text = "��ҳ������:" + sumPurchase.ToString("F4");
            this.lblCurDif.Text = "��ҳ�����:" + sumDif.ToString("F4");
         
            //������
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString("F4");
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.InListNO]).retailCost.ToString("F4");
            this.lblTotDif.Text = (((TotCost)hsTotCost[info.InListNO]).retailCost - ((TotCost)hsTotCost[info.InListNO]).purchaseCost).ToString("F4");
    
            //��Ʊ��
            foreach (object item in hsInvoiceNO.Keys)
            {
                if (string.IsNullOrEmpty(invoiceNO))
                {
                    invoiceNO +=  item.ToString();
                }
                else
                {
                    invoiceNO += "," + item.ToString();
                }
            }
                       
            this.lblInvoiceNO.Text = "��Ʊ�ţ�" + invoiceNO;
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

        /// <summary>
        /// Ԥ��
        /// </summary>
        /// <returns></returns>
        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

    }
}
