using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Print.BinHai
{
    /// <summary>
    /// [��������: ҩƷ��ⵥ]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2012-06]<br></br>
    /// ˵����
    /// 1����ݸ������ҽԺ�����ϸĵ�
    /// 2�����������ҩƷ��𣬱����ʵ��ʹ���п��ܲ�һ�£�����SetPrintData�е�siwtch����ڸ���
    /// 3������ͨ����ͷ��������ָ�cell��ֵ����ͷ����ʱ�����SetPrintData�еĸ�ֵ���
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
            public decimal PurchaseCost;
            public decimal RetailCost;
            public decimal WholesaleCost;
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
                this.lbTitle.Text = string.Format(this.lbTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {
                if (title.IndexOf("[������]") != -1)
                {
                    this.lbTitle.Text = title.Replace("[������]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                }
                else
                {
                    this.lbTitle.Text = title;
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
            this.lbCompany.Text = "�ͻ���λ:" + info.Company.ID + " " + company;
            
            this.lbBillNO.Text = "��ⵥ��:" + info.InListNO;
            this.lblInputDate.Text = "�������:" + info.InDate.ToShortDateString();
            this.lbMadeBillOper.Text = "�Ʊ���:"; //+ FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "ҳ:"+inow.ToString() + "/" + icount.ToString();
            //this.lbPlanOper.Text = "�ɹ�Ա:";// +BillPrintFun.GetStockPlanPersonName(info.StockDept.ID);
            //this.lbStockOper.Text = "�ֹ�Ա:";// +BillPrintFun.GetStockManagerName(info.StockDept.ID);//BillPrintFun.GetEmplName(info.Operation.ExamOper.ID);

            #endregion

            #region farpoint��ֵ
            //��ҳ�ܼƽ��
            TotCost curPageCost = new TotCost();
            //��ҳҩƷ���
            TotCost curPagePCZCost = new TotCost();
            //��ҳ�������Ͻ��
            TotCost curPageBCost = new TotCost();
            //��ҳԭ���Ͻ��
            TotCost curPageMCost = new TotCost();

            this.socFpSpread1_Sheet1.RowCount = 0;

            FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
            n.DecimalPlaces = 4;
            this.socFpSpread1.SetColumnCellType(0, "���뵥��", n);
            this.socFpSpread1.SetColumnCellType(0, "������", n);
            //this.socFpSpread1.SetColumnCellType(0, "���۵���", n);

            for (int index = 0; index < al.Count; index++)
            {
                this.socFpSpread1_Sheet1.AddRows(index, 1);
                FS.HISFC.Models.Pharmacy.Input input = al[index] as FS.HISFC.Models.Pharmacy.Input;
                this.socFpSpread1.SetCellValue(0, index, "��ƷID", FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID).GBCode); //ҩƷ�Զ�����
                this.socFpSpread1.SetCellValue(0, index, "����", input.Item.Name);//ҩƷ����
                this.socFpSpread1.SetCellValue(0, index, "���", input.Item.Specs);//���	

                if (input.Item.PackQty == 0)
                {
                    input.Item.PackQty = 1;
                }
                decimal count = 0;
                count = input.Quantity;

                if (input.ShowState == "0")
                {
                    this.socFpSpread1.SetCellValue(0, index, "��λ", input.Item.MinUnit);//��λ
                    this.socFpSpread1.SetCellValue(0, index, "����", count);//����				
                    this.socFpSpread1.SetCellValue(0, index, "���뵥��", (input.Item.PriceCollection.PurchasePrice / input.Item.PackQty).ToString("F2"));
                    this.socFpSpread1.SetCellValue(0, index, "��Ч��", input.ValidTime.ToShortDateString());
                }
                else
                {
                    this.socFpSpread1.SetCellValue(0, index, "��λ", input.Item.PackUnit);//��λ
                    this.socFpSpread1.SetCellValue(0, index, "����", (count / input.Item.PackQty).ToString("F2").TrimEnd('0').TrimEnd('.'));//����				
                    this.socFpSpread1.SetCellValue(0, index, "���뵥��", input.Item.PriceCollection.PurchasePrice);
                    this.socFpSpread1.SetCellValue(0, index, "��Ч��", input.ValidTime.ToShortDateString());
                }

                this.socFpSpread1.SetCellValue(0, index, "����", FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(input.Producer.ID));
                this.socFpSpread1.SetCellValue(0, index, "����", input.BatchNO);
                this.socFpSpread1.SetCellValue(0, index, "������", input.PurchaseCost);
                this.socFpSpread1.SetCellValue(0, index, "��Ʊ��", input.InvoiceNO);


                switch (input.Item.Type.ID)
                {
                    case "B":
                        curPageBCost.WholesaleCost += input.WholeSaleCost;
                        curPageBCost.PurchaseCost += input.PurchaseCost;
                        curPageBCost.RetailCost += input.RetailCost;
                        break;
                    case "M":
                        curPageMCost.WholesaleCost += input.WholeSaleCost;
                        curPageMCost.PurchaseCost += input.PurchaseCost;
                        curPageMCost.RetailCost += input.RetailCost;
                        break;
                    case "C":
                    case "P":
                    case "Z":
                    default:
                        curPagePCZCost.WholesaleCost += input.WholeSaleCost;
                        curPagePCZCost.PurchaseCost += input.PurchaseCost;
                        curPagePCZCost.RetailCost += input.RetailCost;
                        break;
                }

                curPageCost.WholesaleCost += input.WholeSaleCost;
                curPageCost.PurchaseCost += input.PurchaseCost;
                curPageCost.RetailCost += input.RetailCost;

            }
            //��ǰҳ����
            this.lbCurPageRetailCost.Text = "��ҳ���۽��:" + curPageCost.RetailCost.ToString("F2");
            this.lbCurPagePurchaseCost.Text = "��ҳ������:" + curPageCost.PurchaseCost.ToString("F2");
            this.lbCurPageSubCost.Text = "��ҳ�㹺��:" + (curPageCost.RetailCost - curPageCost.PurchaseCost).ToString("F2");
            
            //ҩƷ����
            this.lbDrugRetailCost.Text = "ҩƷ���:" + curPagePCZCost.RetailCost.ToString("F2");
            this.lbDrugPurchaseCost.Text = "ҩƷ����:" + curPagePCZCost.PurchaseCost.ToString("F2");
            this.lbDrugSubCost.Text = "ҩƷ�㹺��:" + (curPagePCZCost.RetailCost - curPagePCZCost.PurchaseCost).ToString("F2");

            //ԭ���ϡ����������ù����
            //this.lbBCost.Text = "��������:" + curPageBCost.PurchaseCost.ToString("F4");
            //this.lbMCost.Text = "ԭ����:" + curPageBCost.PurchaseCost.ToString("F4");

            //������
            this.lbTotPurchaseCost.Text = "�ܹ����" + ((TotCost)hsTotCost[info.InListNO]).PurchaseCost.ToString("F2");
            this.lbTotRetailCost.Text = "�����۽�" + ((TotCost)hsTotCost[info.InListNO]).RetailCost.ToString("F2");
            this.lbTotSubCost.Text = "���㹺��" + (((TotCost)hsTotCost[info.InListNO]).RetailCost - ((TotCost)hsTotCost[info.InListNO]).PurchaseCost).ToString("F2");
            FS.FrameWork.Management.DataBaseManger obj = new FS.FrameWork.Management.DataBaseManger();
            DateTime date = obj.GetDateTimeFromSysDateTime();
            label3.Text = "�Ƶ����ڣ�" + date.ToShortDateString();
            #endregion

            this.ResetTitleLocation();

        }

        /// <summary>
        /// �������ñ���λ��
        /// </summary>
        private void ResetTitleLocation()
        {
            this.neuPanel4.Controls.Remove(this.lbTitle);
            int with = 0;
            for (int col = 0; col < this.socFpSpread1_Sheet1.ColumnCount; col++)
            {
                if (this.socFpSpread1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.socFpSpread1_Sheet1.Columns[col].Width;
                }
            }
            if (with > this.neuPanel4.Width)
            {
                with = this.neuPanel4.Width;
            }
            this.lbTitle.Location = new Point((with - this.lbTitle.Size.Width) / 2, this.lbTitle.Location.Y);
            this.neuPanel4.Controls.Add(this.lbTitle);

        }
        #endregion

        #region IPharmacyBill ��Ա

        private Base.PrintBill printBill = new FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill();

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
            float rowHeight = this.socFpSpread1_Sheet1.Rows[0].Height;
            
            //�����ʱ������ж��ŵ��ݣ��ֿ�
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Input input in alPrintData)
            {
                if (hs.Contains(input.InListNO))
                {

                    ArrayList al = (ArrayList)hs[input.InListNO];
                    al.Add(input);

                    TotCost tc = (TotCost)hsTotCost[input.InListNO];
                    tc.PurchaseCost += input.PurchaseCost;
                    tc.RetailCost += input.RetailCost;
                    tc.WholesaleCost += input.WholeSaleCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(input);
                    hs.Add(input.InListNO, al);

                    TotCost tc = new TotCost();
                    tc.PurchaseCost += input.PurchaseCost;
                    tc.RetailCost += input.RetailCost;
                    tc.WholesaleCost += input.WholeSaleCost;
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
                        p.PrintPreview(0, 0, this.neuPanel1);
                    }
                    else
                    {
                        p.PrintPage(0, 0, this.neuPanel1);
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
