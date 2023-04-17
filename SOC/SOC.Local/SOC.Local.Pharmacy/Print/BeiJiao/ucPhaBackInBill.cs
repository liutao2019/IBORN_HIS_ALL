using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Pharmacy.Print.BeiJiao
{
    /// <summary>
    /// ��ݸҩ������˿ⵥ�ݴ�ӡ
    /// </summary>
    public partial class ucPhaBackInBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// ҩ������˿�(����)��ӡ��
        /// </summary>
        public ucPhaBackInBill()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ���ݵ��ܽ��
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        /// <summary>
        /// �ܽ��
        /// </summary>
        private class TotCost
        {
            public decimal purchaseCost;
            public decimal retailCost;
        }

        private Base.PrintBill printBill = new Base.PrintBill();

        ArrayList alPrintData = new ArrayList();
        #endregion 

        #region ����
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
            int height = this.neuPanel4.Height;
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

                    this.neuPanel4.Height += (int)rowHeight * al.Count;
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

        /// <summary>
        /// 3��ֵ
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
            FS.HISFC.Models.Pharmacy.Input info = (FS.HISFC.Models.Pharmacy.Input)al[0];

            #region label��ֵ

            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {
                if (title.IndexOf("[������]") != -1)
                {
                    title = title.Replace("[������]", SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                }

                if (title.IndexOf("[�˿�]") != -1)
                {
                    title = title.Replace("[�˿�]", "����");
                }

                this.lblTitle.Text = title;
            }

            this.lblCompany.Text = "������λ: " + info.Company.ID + " " + SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(info.Company.ID);
            this.lblBillID.Text = "����: " + info.InListNO;
            this.lblInputDate.Text = "�˻�����: " + info.InDate.ToShortDateString();
            this.lblOper.Text = "�Ƶ���:" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "ҳ:" + inow.ToString() + "/" + icount.ToString();
            #endregion

            #region farpoint��ֵ
            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Input input = al[i] as FS.HISFC.Models.Pharmacy.Input;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID).NameCollection.OtherSpell.SpellCode; //ҩƷ�Զ�����
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = input.Item.Name;//ҩƷ����
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = input.Item.Specs;//���		
                if (input.Item.PackQty == 0)
                    input.Item.PackQty = 1;
                decimal count = 0;
                count = Math.Abs(input.Quantity);

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[10].CellType = n;
                if (SOC.HISFC.BizProcess.Cache.Common.GetDept(input.StockDept.ID).DeptType.ID.ToString() == "P")
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = input.Item.MinUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = (count).ToString();//����
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (input.Item.PriceCollection.PurchasePrice / input.Item.PackQty).ToString("F4");
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = (input.Item.PriceCollection.RetailPrice / input.Item.PackQty).ToString("F4"); ;
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = input.Item.PackUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = (count / input.Item.PackQty).ToString("F4");//����
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.Item.PriceCollection.PurchasePrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.Item.PriceCollection.RetailPrice;
                }
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(input.Producer.ID);//��������
                this.neuFpEnter1_Sheet1.Cells[i, 4].Text = input.BatchNO;
                this.neuFpEnter1_Sheet1.Cells[i, 8].Text = input.PurchaseCost.ToString("F4");
                this.neuFpEnter1_Sheet1.Cells[i, 10].Text = input.PurchaseCost.ToString("F4");

                sumRetail = sumRetail + input.RetailCost;
                sumPurchase = sumPurchase + input.PurchaseCost;
                sumDif = sumRetail - sumPurchase;
            }

            //������
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString("F4");
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.InListNO]).retailCost.ToString("F4");

            #endregion
            this.resetTitleLocation();

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
    }
}
