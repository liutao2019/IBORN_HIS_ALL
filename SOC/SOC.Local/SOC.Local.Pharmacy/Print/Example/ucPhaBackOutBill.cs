using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.Print.Example
{
    /// <summary>
    /// ��ݸҩ������˿ⵥ�ݴ�ӡ
    /// </summary>
    public partial class ucPhaBackOutBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// ҩ������˿�(����)��ӡ��
        /// </summary>
        public ucPhaBackOutBill()
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

        #region ��ӡ������


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
            FS.HISFC.Models.Pharmacy.Output info = (FS.HISFC.Models.Pharmacy.Output)al[0];

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

            this.lblCompany.Text = "�˻���λ: " + info.TargetDept.ID +" "+ SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
            this.lblBillID.Text = "���ⵥ��: " + info.OutListNO;
            this.lblInputDate.Text = "�˻�����: " + info.OutDate.ToShortDateString();
            this.lblOper.Text = "�Ƶ���:" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID); //BillPrintFun.GetEmplName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "ҳ��" + inow.ToString() + "/" + icount.ToString();
            this.neuLabel10.Text = "�ֹ�Ա��";// +BillPrintFun.GetStockManagerName(info.StockDept.ID);
            this.neuLabel10.Visible = true;
            #endregion

            #region farpoint��ֵ
            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Output output = al[i] as FS.HISFC.Models.Pharmacy.Output;
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text =   SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID).UserCode; //ҩƷ�Զ�����
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = output.Item.Name;//ҩƷ����
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = output.Item.Specs;//���		
                if (output.Item.PackQty == 0)
                    output.Item.PackQty = 1;
                decimal count = 0;
                count = Math.Abs(output.Quantity);//�����˿������Ǹ���  ������Ҫ��ӡ������

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;
                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[9].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n;
                this.neuFpEnter1_Sheet1.Columns[10].CellType = n;
                if (SOC.HISFC.BizProcess.Cache.Common.GetDept(output.StockDept.ID).DeptType.ID.ToString() == "P")
                {
                   
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = output.Item.MinUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = (count).ToString();//����				
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (output.Item.PriceCollection.PurchasePrice / output.Item.PackQty).ToString("F4");//�����
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = (output.Item.PriceCollection.WholeSalePrice / output.Item.PackQty).ToString("F4");//���ۼ�
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = output.Item.PackUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = (count / output.Item.PackQty).ToString("F2");//����				
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = output.Item.PriceCollection.PurchasePrice;//�����
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = output.Item.PriceCollection.WholeSalePrice;//���ۼ�
               
                }
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = SOC.HISFC.BizProcess.Cache.Pharmacy.GetProducerName(output.Producer.ID);//��������
                this.neuFpEnter1_Sheet1.Cells[i, 4].Text = output.BatchNO;
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.PurchaseCost;//������
                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = output.WholeSaleCost;//�˿���

                sumRetail = sumRetail + output.WholeSaleCost;
                sumPurchase = sumPurchase + output.PurchaseCost;
                sumDif = sumRetail - sumPurchase;
            }
            //��ǰҳ����
            this.lblCurRet.Text = "��ҳ���۽�" + sumRetail.ToString("F4");
            this.lblCurPur.Text = "��ҳ�����" + sumPurchase.ToString("F4");
            this.lblCurDif.Text = "��ҳ����" + sumDif.ToString("F4");

            //������
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.OutListNO]).purchaseCost.ToString("F4");
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.OutListNO]).retailCost.ToString("F4");

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
                    tc.retailCost += o.WholeSaleCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(o);
                    hs.Add(o.OutListNO, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = o.PurchaseCost;
                    tc.retailCost = o.WholeSaleCost;
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

        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            if (this.alPrintData == null || alPrintData.Count == 0)
            {
                return 1;
            }
            FS.HISFC.Models.Pharmacy.Output output = alPrintData[0] as FS.HISFC.Models.Pharmacy.Output;
            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            ArrayList al = itemMgr.QueryOutputInfo(output.StockDept.ID, output.OutListNO, "2");
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
