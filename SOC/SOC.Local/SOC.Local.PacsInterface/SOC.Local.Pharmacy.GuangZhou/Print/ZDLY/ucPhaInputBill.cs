using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.WinForms.Controls;

namespace Neusoft.SOC.Local.Pharmacy.Print.ZDLY
{
    /// <summary>
    /// ��ݸҩƷ���ⵥ�ݴ�ӡ
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

        Neusoft.SOC.HISFC.BizLogic.Pharmacy.InOut item = new Neusoft.SOC.HISFC.BizLogic.Pharmacy.InOut();
        Neusoft.HISFC.BizLogic.Manager.PowerLevelManager powerLevelManager = new Neusoft.HISFC.BizLogic.Manager.PowerLevelManager();
        /// <summary>
        /// ����������
        /// </summary>
        protected Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

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
            Neusoft.HISFC.Models.Pharmacy.Input info = (Neusoft.HISFC.Models.Pharmacy.Input)al[0];

            #region label��ֵ
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {

                Neusoft.HISFC.Models.Admin.PowerLevelClass3 privClass3 = powerLevelManager.LoadLevel3ByPrimaryKey("0310", info.PrivType);

                title = title.Replace("[������]", Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                if (privClass3 != null)
                {
                    title = title.Replace("[��������]", privClass3.Class3Name);
                }

                this.lblTitle.Text = title;
            }

            this.lblCompany.Text = "������˾: (" + info.Company.ID + ")" + Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(info.Company.ID);
            this.lblBillID.Text = "���ݺ�: " + info.InListNO;
            this.lblInputTime.Text = "�������: " + info.InDate.ToShortDateString();
            this.lblRecord.Text = "¼��:" + Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ApplyOper.ID);
            this.lblOper.Text = "�Ʊ�:" + Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ApplyOper.ID);

            lblStorageDept.Text = "������: (" + info.StockDept.ID + ")" +Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID);
            
            lblPrintDate.Text = "��ӡ���ڣ� "+System.DateTime.Now.ToShortDateString();
            this.lblPage.Text = "ҳ��" + inow.ToString() + "/" + icount.ToString();

            this.lblTotPurCost.Text = "�����" + ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString(Function.GetCostDecimalString());
            this.lblTotRetail.Text = "���۽�" + ((TotCost)hsTotCost[info.InListNO]).retailCost.ToString(Function.GetCostDecimalString());

            this.lblTotDiff.Text = "������" + (((TotCost)hsTotCost[info.InListNO]).retailCost - 
                ((TotCost)hsTotCost[info.InListNO]).purchaseCost).ToString(Function.GetCostDecimalString());
          
            #endregion

            #region farpoint��ֵ
            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;


            this.neuFpEnter1_Sheet1.RowCount = 0;


            FarPoint.Win.Spread.CellType.NumberCellType nPrice = new FarPoint.Win.Spread.CellType.NumberCellType();
            nPrice.DecimalPlaces = Function.GetPriceDecimal();
            FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
            nCost.DecimalPlaces = Function.GetCostDecimal();
            FarPoint.Win.Spread.CellType.NumberCellType nQty = new FarPoint.Win.Spread.CellType.NumberCellType();
            nQty.DecimalPlaces = Function.GetQtyDecimal();

            this.neuFpEnter1_Sheet1.Columns[4].CellType = nQty;
            this.neuFpEnter1_Sheet1.Columns[6].CellType = nPrice;
            this.neuFpEnter1_Sheet1.Columns[7].CellType = nPrice;
            this.neuFpEnter1_Sheet1.Columns[8].CellType = nCost;
            this.neuFpEnter1_Sheet1.Columns[9].CellType = nCost;

            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);


                Neusoft.HISFC.Models.Pharmacy.Input input = al[i] as Neusoft.HISFC.Models.Pharmacy.Input;
                item.GetStorageNum(input.StockDept.ID, input.Item.ID, out  storageSum);

                Neusoft.HISFC.Models.Pharmacy.Item itemObj = Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(input.Item.ID);

                this.neuFpEnter1_Sheet1.Cells[i, 0].Value = itemObj.UserCode;
                this.neuFpEnter1_Sheet1.Cells[i, 1].Value = itemObj.Name; //ҩƷ����   
                this.neuFpEnter1_Sheet1.Cells[i, 2].Value = itemObj.Specs;//���	
                this.neuFpEnter1_Sheet1.Cells[i, 3].Value = input.BatchNO;//����

                if (input.Item.PackQty == 0)
                {
                    input.Item.PackQty = 1;
                }
                decimal count = input.Quantity;
                
                if (input.ShowState=="0")
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Value = (count).ToString();//����
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Value = input.Item.MinUnit;//��λ	 
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = input.PriceCollection.PurchasePrice / input.Item.PackQty;//�����
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.PriceCollection.RetailPrice / input.Item.PackQty;//���ۼ�
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Value = (count / input.Item.PackQty).ToString("F2");//����
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Value = input.Item.PackUnit;//��λ		
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = input.PriceCollection.PurchasePrice.ToString();//�����
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.PriceCollection.RetailPrice.ToString();//���ۼ�          
                }

                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.PurchaseCost.ToString(Function.GetCostDecimalString());//������
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.RetailCost.ToString(Function.GetCostDecimalString());//���۽��

                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = input.ValidTime.ToShortDateString();//��Ч��

                sumRetail = sumRetail + input.RetailCost;
                sumPurchase += input.PurchaseCost;
                sumDif = sumRetail - sumPurchase;
            }


            #region ����ҳ
            if (inow == icount)
            {
                int rowCount = this.neuFpEnter1_Sheet1.RowCount;
                this.neuFpEnter1_Sheet1.AddRows(rowCount, 1);
                this.neuFpEnter1_Sheet1.Cells[rowCount, 0].Value = "�ϼ�";
                this.neuFpEnter1_Sheet1.Cells[rowCount, 8].Value = ((TotCost)hsTotCost[info.InListNO]).purchaseCost.ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[rowCount, 9].Value = ((TotCost)hsTotCost[info.InListNO]).retailCost.ToString(Function.GetCostDecimalString());
           
            }
            #endregion
           
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
            Neusoft.FrameWork.WinForms.Classes.Print p = new Neusoft.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region ��ҳ��ӡ
            int height = this.neuPanel4.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            if (this.printBill.Sort == Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.��λ��)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.����˳��)
            {
                Base.PrintBill.SortByCustomerCode(ref alPrintData);
            }

            //�����ʱ������ж��ŵ��ݣ��ֿ�
            System.Collections.Hashtable hs = new Hashtable();
            foreach (Neusoft.HISFC.Models.Pharmacy.Input input in alPrintData)
            {
                Neusoft.HISFC.Models.Pharmacy.Input o = input.Clone();
                if (hs.Contains(o.InListNO))
                {

                    ArrayList al = (ArrayList)hs[o.InListNO];
                    al.Add(o);

                    TotCost tc = (TotCost)hsTotCost[o.InListNO];
                    tc.purchaseCost += o.PurchaseCost;
                    tc.retailCost += o.RetailCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(o);
                    hs.Add(o.InListNO, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = o.PurchaseCost;
                    tc.retailCost = o.RetailCost;
                    hsTotCost.Add(o.InListNO, tc);
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

                
                int fromPage = 0;
                int toPage = 0;
                frmSelectPages frmSelect = new frmSelectPages();
                frmSelect.PageCount = pageTotNum;
                frmSelect.SetPages();
                DialogResult dRsult = frmSelect.ShowDialog();
                if (dRsult == DialogResult.OK)
                {
                    fromPage = frmSelect.FromPage - 1;
                    toPage = frmSelect.ToPage;
                }
                else
                {
                    return 0;
                }

                //��ҳ��ӡ
                for (int pageNow = fromPage; pageNow < toPage; pageNow++)
                {
                    ArrayList al = new ArrayList();

                    for (int index = pageNow * this.printBill.RowCount; index < alPrint.Count && index < (pageNow + 1) * this.printBill.RowCount; index++)
                    {
                        al.Add(alPrint[index]);
                    }
                    this.SetPrintData(al, pageNow + 1, pageTotNum, printBill.Title);

                    if (pageNow + 1 == pageTotNum)
                    {
                        this.neuPanel4.Height += (int)rowHeight * (al.Count+1);
                        this.Height += (int)rowHeight * (al.Count+1);
                    }
                    else
                    {
                        this.neuPanel4.Height += (int)rowHeight * al.Count;
                        this.Height += (int)rowHeight * al.Count;
                    }

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
            
            if (alPrintData != null && alPrintData.Count > 0)
            {
                string bill = (alPrintData[0] as Neusoft.HISFC.Models.Pharmacy.Input).InListNO;
                string dept = (alPrintData[0] as Neusoft.HISFC.Models.Pharmacy.Input).StockDept.ID;
                Neusoft.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new Neusoft.SOC.HISFC.BizLogic.Pharmacy.InOut();
                ArrayList al = itemMgr.QueryInputInfoByListID(dept, bill, "AAAA", "AAAA");
                this.alPrintData = al;
            }
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
