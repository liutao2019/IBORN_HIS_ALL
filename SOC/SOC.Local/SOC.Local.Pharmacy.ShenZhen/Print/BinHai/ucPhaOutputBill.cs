using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Pharmacy.ShenZhen.Print.BinHai;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Print.BinHai
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

            this.lblCompany.Text = "��ҩ����: " + info.TargetDept.ID + " " + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
            this.lblBillID.Text = "�����: " + info.OutListNO;
            this.lblInputDate.Text = "��������: " + info.OutDate.ToShortDateString();
            this.lblOper.Text = "�Ƶ���:" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "ҳ��" + inow.ToString() + "/" + icount.ToString();
            //this.neuLabel10.Text = "�ֹ�Ա��";// +BillPrintFun.GetStockManagerName(info.StockDept.ID);// BillPrintFun.GetEmplName(info.Operation.ExamOper.ID);
            //this.neuLabel10.Visible = true;
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
           // this.socFpSpread1.SetColumnCellType(0, "������", n);
            this.socFpSpread1.SetColumnCellType(0, "���۵���", n);

            for (int index = 0; index < al.Count; index++)
            {
                this.socFpSpread1_Sheet1.AddRows(index, 1);
                FS.HISFC.Models.Pharmacy.Output output = al[index] as FS.HISFC.Models.Pharmacy.Output;
                this.socFpSpread1.SetCellValue(0, index, "��ƷID", FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID).GBCode); //ҩƷ�Զ�����
                this.socFpSpread1.SetCellValue(0, index, "����", output.Item.Name);//ҩƷ����
                this.socFpSpread1.SetCellValue(0, index, "���", output.Item.Specs);//���	

                if (output.Item.PackQty == 0)
                {
                    output.Item.PackQty = 1;
                }
                decimal count = 0;
                count = output.Quantity;

                if (output.ShowState == "0")
                {
                    this.socFpSpread1.SetCellValue(0, index, "��λ", output.Item.MinUnit);//��λ
                    this.socFpSpread1.SetCellValue(0, index, "����", count);//����				
                    this.socFpSpread1.SetCellValue(0, index, "���뵥��", (output.Item.PriceCollection.PurchasePrice / output.Item.PackQty).ToString("F2"));
                    this.socFpSpread1.SetCellValue(0, index, "���۵���", (output.Item.PriceCollection.RetailPrice / output.Item.PackQty).ToString("F2"));
                }
                else
                {
                    this.socFpSpread1.SetCellValue(0, index, "��λ", output.Item.PackUnit);//��λ
                    this.socFpSpread1.SetCellValue(0, index, "����", (count / output.Item.PackQty).ToString("F2").TrimEnd('0').TrimEnd('.'));//����				
                    this.socFpSpread1.SetCellValue(0, index, "���뵥��", output.Item.PriceCollection.PurchasePrice);
                    this.socFpSpread1.SetCellValue(0, index, "���۵���", output.Item.PriceCollection.RetailPrice);
                }
                this.socFpSpread1.SetCellValue(0, index, "��Ч��", output.ValidTime.ToShortDateString());//��Ч��
                this.socFpSpread1.SetCellValue(0, index, "����", FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(output.Producer.ID));
                this.socFpSpread1.SetCellValue(0, index, "����", output.BatchNO);
                this.socFpSpread1.SetCellValue(0, index, "������", output.PurchaseCost);
                this.socFpSpread1.SetCellValue(0, index, "���۽��", output.RetailCost);


                switch (output.Item.Type.ID)
                {
                    case "B":
                        curPageBCost.WholesaleCost += output.WholeSaleCost;
                        curPageBCost.PurchaseCost += output.PurchaseCost;
                        curPageBCost.RetailCost += output.RetailCost;
                        break;
                    case "M":
                        curPageMCost.WholesaleCost += output.WholeSaleCost;
                        curPageMCost.PurchaseCost += output.PurchaseCost;
                        curPageMCost.RetailCost += output.RetailCost;
                        break;
                    case "C":
                    case "P":
                    case "Z":
                    default:
                        curPagePCZCost.WholesaleCost += output.WholeSaleCost;
                        curPagePCZCost.PurchaseCost += output.PurchaseCost;
                        curPagePCZCost.RetailCost += output.RetailCost;
                        break;
                }

                curPageCost.WholesaleCost += output.WholeSaleCost;
                curPageCost.PurchaseCost += output.PurchaseCost;
                curPageCost.RetailCost += output.RetailCost;

            }

            //��ǰҳ����
            //this.lblCurRet.Text = "��ҳ���۽�" + sumRetail.ToString("F4");
            //this.lblCurPur.Text = "��ҳ�����" + sumPurchase.ToString("F4");
            //this.lblCurDif.Text = "��ҳ����" + sumDif.ToString("F4");

            //lblBDiff.Text = "�������ϲ" + (sumBWholeCost - sumBPurCost).ToString("F4");
            //lblBPurCost.Text = "�������Ϲ��" + sumBPurCost.ToString("F4");
            //lblBWholeCost.Text = "����������"+sumBWholeCost.ToString("F4");

            //lblDrugDiff.Text = "ҩƷ��"+(sumDrugWholeCost-sumDrugPurCost).ToString("F4");
            //lblDrugPurCost.Text = "ҩƷ���" + sumDrugPurCost.ToString("F4");
            //lblDrugWholeCost.Text = "ҩƷ��"+sumDrugWholeCost.ToString("F4");
           
            ////������
            //this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.OutListNO]).purchaseCost.ToString("F4");
            //this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.OutListNO]).retailCost.ToString("F4");
            //this.lblTotDif.Text = (((TotCost)hsTotCost[info.OutListNO]).retailCost - ((TotCost)hsTotCost[info.OutListNO]).purchaseCost).ToString("F4");

            //��ǰҳ����
            this.lblCurRet.Text = "���۽��:" + curPageCost.RetailCost.ToString("F2");
            this.lblCurPur.Text = "��ҳ�ϼ�  ������:" + curPageCost.PurchaseCost.ToString("F2");
            this.lblCurDif.Text = "�㹺��:" + (curPageCost.RetailCost - curPageCost.PurchaseCost).ToString("F2");

            //ҩƷ����
            //this.lblDrugWholeCost.Text = "ҩƷ���:" + curPagePCZCost.RetailCost.ToString("F4");
            //this.lblDrugPurCost.Text = "ҩƷ����:" + curPagePCZCost.PurchaseCost.ToString("F4");
            //this.lblDrugDiff.Text = "ҩƷ�㹺��:" + (curPagePCZCost.RetailCost - curPagePCZCost.PurchaseCost).ToString("F4");

            //ԭ���ϡ����������ù����
            //this.lblBDiff.Text = "�������ϲ" + (curPageBCost.RetailCost-curPageBCost.PurchaseCost).ToString("F4");
            //this.lblBPurCost.Text = "�������Ϲ��" + curPageBCost.PurchaseCost.ToString("F4");
            //this.lblBWholeCost.Text = "����������" + curPageBCost.RetailCost.ToString("F4");
            
            //������
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.OutListNO]).PurchaseCost.ToString("F2");
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.OutListNO]).RetailCost.ToString("F2");
            this.lblTotDif.Text = (((TotCost)hsTotCost[info.OutListNO]).RetailCost - ((TotCost)hsTotCost[info.OutListNO]).PurchaseCost).ToString("F2");

            FS.FrameWork.Management.DataBaseManger obj = new FS.FrameWork.Management.DataBaseManger();
            DateTime date = obj.GetDateTimeFromSysDateTime();
            neuLabel5.Text = "�Ƶ����ڣ�" + date.ToShortDateString();
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
            for (int col = 0; col < this.socFpSpread1_Sheet1.ColumnCount; col++)
            {
                if (this.socFpSpread1_Sheet1.Columns[col].Visible)
                {
                    with += (int)this.socFpSpread1_Sheet1.Columns[col].Width;
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
            float rowHeight = this.socFpSpread1_Sheet1.Rows[0].Height;
            this.socFpSpread1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            //�����ʱ������ж��ŵ��ݣ��ֿ�
            System.Collections.Hashtable hs = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Output output in alPrintData)
            {
                if (hs.Contains(output.OutListNO))
                {

                    ArrayList al = (ArrayList)hs[output.OutListNO];
                    al.Add(output);

                    TotCost tc = (TotCost)hsTotCost[output.OutListNO];
                    tc.PurchaseCost += output.PurchaseCost;
                    tc.RetailCost += output.RetailCost;
                    tc.WholesaleCost += output.WholeSaleCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(output);
                    hs.Add(output.OutListNO, al);

                    TotCost tc = new TotCost();
                    tc.PurchaseCost += output.PurchaseCost;
                    tc.RetailCost += output.RetailCost;
                    tc.WholesaleCost += output.WholeSaleCost;
                    hsTotCost.Add(output.OutListNO, tc);
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
                    if(this.printBill.Sort == FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.��λ��)
                    {
                        Base.PrintBill.SortByPlaceNO(ref al);
                    }
                    else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.����˳��)
                    {
                        Base.PrintBill.SortByBillNO(ref al);
                    }
                    this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

                    this.neuPanel4.Height = this.neuPanel4.Height + (int)rowHeight * al.Count;
                    this.Height += (int)rowHeight * al.Count;

                    if (this.printBill.IsNeedPreview)
                    {
                        p.PrintPreview(0, 0, this.neuPanel1);
                    }
                    else
                    {
                        p.PrintPage(0, 0, this.neuPanel1);
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
