using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY.GJ
{
    /// <summary>
    /// ��ݸҩƷ���ⵥ�ݴ�ӡ
    /// </summary>
    public partial class ucPhaOutputBillIBORN : ucBaseControl, Base.IPharmacyBillPrint
    {
        private string curStockDept = string.Empty;

        /// <summary>
        /// ҩƷ����ӡ��
        /// </summary>
        public ucPhaOutputBillIBORN()
        {
            InitializeComponent();
            PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            PrintDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_BeginPrint);
            PrintDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(PrintDocument_EndPrint);
        }

        void PrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.PrintDocument.PrintController.IsPreview == false)
            {
                printPreviewDialog.Close();
                printPreviewDialog.Dispose();
            }
        }

        void PrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            curPageNO = 1;
        }


        #region ����
        private bool isReprint = false;

        /// <summary>
        /// ���д���ӡ����
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        System.Drawing.Printing.PrintPageEventArgs printPageEventArgs = null;

        /// <summary>
        /// ���ݵ��ܽ��
        /// </summary>
        private Hashtable hsTotCost = new Hashtable();

        private Hashtable hsTotCostByPage = new Hashtable();

        /// <summary>
        /// �Ƿ��ۼ�
        /// </summary>
        private bool isRetailPrice = false;


        /// <summary>
        /// �Ƿ���Ҫ��ע
        /// </summary>
        private bool isNeedMemo = false;
        /// <summary>
        /// 
        /// </summary>
        private class TotCost
        {
            public decimal purchaseCost;
            public decimal wholeSaleCost;
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
            curStockDept = info.StockDept.ID;

            #region ����ͷ���͵ײ���ʾ����// {DE780BCE-98B8-4d1d-8AE6-205981C1AAE6}
            //Ĭ��
            this.lblOper.Text = "  �Ʊ��ˣ�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID) + " ";
            this.lblPrintTime.Text = "�Ƶ����ڣ�" + DateTime.Now.ToString();
            this.lblPage.Text = "ҳ��" + inow.ToString() + "/" + icount.ToString();

            if (this.printBill.Class2Code == "0320" && this.printBill.Class3Code == "09")//��������
            {
                //�Ƿ���Ҫ��ע
                this.isNeedMemo = true;
                //ͷ��
                this.lblReciveDept.Text = "";
                this.lblBillID.Text = "���ⵥ�ţ� " + info.OutListNO;
                this.lblInputDate.Text = "�������ڣ�" + info.OutDate.ToString();
                //�ײ�
                this.lblGetOper.Text = "��ҩ�ˣ�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.GetPerson);
                this.lblExportOper.Text = "�����ˣ�";
                this.neuLabel9.Text = "��׼�ˣ�";
                //�ۼ�
                this.isRetailPrice = true;

            }
            else if (this.printBill.Class2Code == "0320" && this.printBill.Class3Code == "12")//������ҩ
            {
                //�Ƿ���Ҫ��ע
                this.isNeedMemo = true;
                //ͷ��
                this.lblReciveDept.Text = "��ҩ���ң�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
                this.lblBillID.Text = "���ⵥ�ţ� " + info.OutListNO;
                this.lblInputDate.Text = "�������ڣ�" + info.OutDate.ToString();
                //�ײ�
                this.lblGetOper.Text = "��ҩ�ˣ�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.GetPerson);
                this.lblExportOper.Text = "�����ˣ�";
                this.neuLabel9.Text = "�����ˣ�";
                //�ۼ�
                this.isRetailPrice = true;
            }
            //{2277CF4C-D5FB-4d53-9265-907AAA21B195}
            else if (this.printBill.Class2Code == "0320" && this.printBill.Class3Code == "02")   //�����˿�
            {
                //�Ƿ���Ҫ��ע
                this.isNeedMemo = false;
                //ͷ��
                this.lblReciveDept.Text = "�˿���ң�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
                this.lblBillID.Text = "�˿ⵥ�ţ� " + info.OutListNO;
                this.lblInputDate.Text = "�˿����ڣ�" + info.OutDate.ToString();
                //�ײ�
                //{09150DE9-4D02-4446-BA6F-5C8562183945}
                //�����˿ⲻ��ʾ��ҩ��
                //this.lblGetOper.Text = "��ҩ�ˣ�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.GetPerson);
                this.lblGetOper.Text = "";
                this.lblExportOper.Text = "�˻��ˣ�";
                this.neuLabel9.Text = "�����ˣ�";
                //�ۼ�
                this.isRetailPrice = true;
            }
            //{DFFB011F-3756-4f0d-B1AC-EBFB8BDFF72F}
            else if ((this.printBill.Class2Code == "0320" && this.printBill.Class3Code == "04")//��������
                  ||(this.printBill.Class2Code == "0320" && this.printBill.Class3Code == "10"))//����һ�����
            {
                //�Ƿ���Ҫ��ע
                this.isNeedMemo = true;
                //ͷ��
                this.lblReciveDept.Text = "Ŀ����ң�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
                this.lblBillID.Text = "���ⵥ�ţ� " + info.OutListNO;
                this.lblInputDate.Text = "�������ڣ�" + info.OutDate.ToString();
                //�ײ�
                this.lblGetOper.Text = "��ҩ�ˣ�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.GetPerson);
                this.lblExportOper.Text = "�����ˣ�";
                this.neuLabel9.Text = "�����ˣ�";
                //�ۼ�
                this.isRetailPrice = true;
            }
            else
            {
                //�Ƿ���Ҫ��ע
                this.isNeedMemo = true;
                //ͷ��
                this.lblReciveDept.Text = "Ŀ����ң�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
                this.lblBillID.Text = "���ⵥ�ţ� " + info.OutListNO;
                this.lblInputDate.Text = "�������ڣ�" + info.OutDate.ToString();
                //�ײ�
                this.lblGetOper.Text = "��ҩ�ˣ�" + FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.GetPerson);
                this.lblExportOper.Text = "�����ˣ�";
                this.neuLabel9.Text = "��׼�ˣ�";
                //�ۼ�
                this.isRetailPrice = true;
            }
            #endregion




            #region label��ֵ
            //{AE43CB99-F241-4aee-84E1-67C3B3505DF0}
            FS.HISFC.Models.Base.Department deptInfo = FS.SOC.HISFC.BizProcess.Cache.Common.GetDept(info.StockDept.ID);

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

                //{AE43CB99-F241-4aee-84E1-67C3B3505DF0}
                if (title.IndexOf("[Ժ��]") != -1)
                {
                    this.lblTitle.Text = this.lblTitle.Text.Replace("[Ժ��]", deptInfo.HospitalName);
                }
            }    
            #endregion


            #region farpoint��ֵ
            decimal sumRetail = 0;
            decimal sumWholeSaleCost = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;
            decimal sumDrugWholeCost = 0;
            decimal sumDrugPurCost = 0;

            decimal sumBWholeCost = 0;
            decimal sumBPurCost = 0;
            int pageNo = 1;

            this.neuFpEnter1_Sheet1.RowCount = 0;


            //{38837A76-F219-4b18-AADB-9F606D61D52D}
            if (!this.isNeedMemo)//����Ҫ
            {
                this.neuFpEnter1_Sheet1.Columns.Get(7).Width += 10;
                this.neuFpEnter1_Sheet1.Columns.Get(8).Width += 10;
                this.neuFpEnter1_Sheet1.Columns.Get(9).Width = 0F;

            }

            for (int i = 0; i < al.Count; i++)
            {
                if((i != 0) &&(i% printBill.RowCount == 0))
                {
                    pageNo++;
                }
                this.neuFpEnter1_Sheet1.AddRows(i, 1);

                //this.neuFpEnter1_Sheet1.Columns[2].Width = 81F;

                FS.HISFC.Models.Pharmacy.Output output = al[i] as FS.HISFC.Models.Pharmacy.Output;
                //this.neuFpEnter1_Sheet1.Cells[i, 0].Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID).UserCode; //ҩƷ�Զ�����
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = (i + 1).ToString();
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = output.Item.Name;//ҩƷ����
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = output.Item.Specs;//���	 
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = output.BatchNO;
                //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
                this.neuFpEnter1_Sheet1.Cells[i, 4].Text = output.ValidTime.ToShortDateString();
                if (output.Item.PackQty == 0)
                    output.Item.PackQty = 1;
                decimal count = 0;
                count = output.Quantity;
                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;
                FarPoint.Win.Spread.CellType.NumberCellType n2 = new FarPoint.Win.Spread.CellType.NumberCellType();
                n2.DecimalPlaces = 2;

                //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
                //FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                //nCost.DecimalPlaces = Function.GetCostDecimal();
                //this.neuFpEnter1_Sheet1.Columns[5].CellType = nCost;

                this.neuFpEnter1_Sheet1.Columns[7].CellType = n;
                //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
                this.neuFpEnter1_Sheet1.Columns[8].CellType = n2;

                if (isRetailPrice)
                {

                    this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "�ۼ�(Ԫ)";
                    this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "�ۼ۽��(Ԫ)";
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = (count / output.Item.PackQty).ToString();//����	
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = output.Item.PackUnit;//��λ		
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = output.PriceCollection.RetailPrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.RetailCost.ToString(Function.GetCostDecimalString());
                }
                else
                {

                    this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "���뵥��(Ԫ)";
                    this.neuFpEnter1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "������(Ԫ)";
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = (count / output.Item.PackQty).ToString();//����	
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = output.Item.PackUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = output.PriceCollection.PurchasePrice;
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.PurchaseCost.ToString(Function.GetCostDecimalString());
                }
                
                this.neuFpEnter1_Sheet1.Cells[i, 9].Text = output.Memo;

                //{38837A76-F219-4b18-AADB-9F606D61D52D}
                //��Ȼ�ŵ�ѭ����ж��ٸ��ͼӶ��ٴΣ�����������
                //�Ƶ�ѭ����
                //if (!this.isNeedMemo)//����Ҫ
                //{
                //    this.neuFpEnter1_Sheet1.Columns.Get(7).Width += 10;
                //    this.neuFpEnter1_Sheet1.Columns.Get(8).Width += 10;
                //    this.neuFpEnter1_Sheet1.Columns.Get(9).Width = 0F;

                //}

                if(hsTotCostByPage.Contains(pageNo))
                {
                     TotCost totCostByPage  = (TotCost)hsTotCostByPage[pageNo];
                     totCostByPage.purchaseCost += output.PurchaseCost;
                     totCostByPage.wholeSaleCost += output.WholeSaleCost;
                     totCostByPage.retailCost += output.RetailCost;
                }
                else
                {
                    TotCost totCostByPage = new TotCost();
                    totCostByPage.retailCost += output.RetailCost;
                    totCostByPage.wholeSaleCost += output.WholeSaleCost;
                    totCostByPage.purchaseCost += output.PurchaseCost;
                    hsTotCostByPage.Add(pageNo, totCostByPage);
                }

                sumRetail = sumRetail + output.RetailCost;
                sumWholeSaleCost = sumWholeSaleCost + output.WholeSaleCost;
                sumPurchase += output.PurchaseCost;
                sumDif = sumRetail - sumPurchase;
            }
            //��ǰҳ����
            this.lblCurRet.Text = "��ҳ�ϼƣ��ۼ۽�" + sumRetail.ToString("F2");
            this.lblCurPur.Text = "��ҳ�ϼƣ������" + sumPurchase.ToString("F2");
            this.lblCurDif.Text = "��ҳ����" + sumDif.ToString("F2");

            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(info.StockDept.ID).DeptType.ID.ToString() != "PI" && isReprint)
            {
                this.lblTitle.Text = "(����)" + this.lblTitle.Text; ;
            }

            //������
            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            //this.lblTotPurCost.Text = "������ϼƣ�" + ((TotCost)hsTotCost[info.OutListNO]).purchaseCost.ToString(Function.GetCostDecimalString());
            this.lblTotPurCost.Text = "�ܺϼƣ������ܽ�" + ((TotCost)hsTotCost[info.OutListNO]).purchaseCost.ToString("F2");
            this.lblTotRetailCost.Text = "�ܺϼƣ��ۼ��ܽ�" + ((TotCost)hsTotCost[info.OutListNO]).retailCost.ToString("F2");
            this.lblTotDif.Text = "������ϼƣ�" + (((TotCost)hsTotCost[info.OutListNO]).retailCost - ((TotCost)hsTotCost[info.OutListNO]).purchaseCost).ToString("F2");

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

        #region ��ӡ���
        /// <summary>
        /// ��ǰ��ӡҳ��ҳ��
        /// �����Զ������
        /// </summary>
        private int curPageNO = 1;

        /// <summary>
        /// ���δ�ӡ���ҳ��
        /// �����Զ������
        /// </summary>
        private int maxPageNO = 1;

        private System.Drawing.Printing.Margins DrawingMargins = new System.Drawing.Printing.Margins(10, 10, 5, 15);

        SOC.Windows.Forms.PrintPageSelectDialog socPrintPageSelectDialog = new FS.SOC.Windows.Forms.PrintPageSelectDialog();

        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();


        private void PrintDocumentPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            printPageEventArgs = e;
            //����ѡ���ӡ��Χ�������
            while (this.curPageNO < this.socPrintPageSelectDialog.FromPageNO && this.curPageNO < this.maxPageNO)
            {
                curPageNO++;
            }

            if (this.curPageNO > this.maxPageNO || this.curPageNO > socPrintPageSelectDialog.ToPageNO)
            {
                this.curPageNO = 1;
                //this.maxPageNO = 1;
                e.HasMorePages = true;
                return;
            }

            Graphics graphics = e.Graphics;

            #region �������
            int mainTitleLocalX = this.DrawingMargins.Left + this.lblTitle.Location.X;
            int mainTitleLoaclY = this.DrawingMargins.Top + this.lblTitle.Location.Y;
            graphics.DrawString(this.lblTitle.Text, this.lblTitle.Font, new SolidBrush(this.lblTitle.ForeColor), mainTitleLocalX, mainTitleLoaclY);

            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            //int additionTitleLocalX = this.DrawingMargins.Left + this.lblCompany.Location.X;
            //int additionTitleLocalY = this.DrawingMargins.Top + this.lblCompany.Location.Y;
            //graphics.DrawString(this.lblCompany.Text, this.lblCompany.Font, new SolidBrush(this.lblCompany.ForeColor), additionTitleLocalX, additionTitleLocalY);

            int additionTitleLocalX = this.DrawingMargins.Left + this.lblBillID.Location.X;
            int additionTitleLocalY = this.DrawingMargins.Top + this.lblBillID.Location.Y;
            graphics.DrawString(this.lblBillID.Text, this.lblBillID.Font, new SolidBrush(this.lblBillID.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblInputDate.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblInputDate.Location.Y;
            graphics.DrawString(this.lblInputDate.Text, this.lblInputDate.Font, new SolidBrush(this.lblInputDate.ForeColor), additionTitleLocalX, additionTitleLocalY);

            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            additionTitleLocalX = this.DrawingMargins.Left + this.lblPrintTime.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPrintTime.Location.Y;
            graphics.DrawString(this.lblPrintTime.Text, this.lblPrintTime.Font, new SolidBrush(this.lblPrintTime.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblReciveDept.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblReciveDept.Location.Y;
            graphics.DrawString(this.lblReciveDept.Text, this.lblReciveDept.Font, new SolidBrush(this.lblReciveDept.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region ҳ�����

            additionTitleLocalX = this.DrawingMargins.Left + this.lblPage.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblPage.Location.Y;
            graphics.DrawString("ҳ�룺" + this.curPageNO.ToString() + "/" + this.maxPageNO.ToString(), this.lblPage.Font, new SolidBrush(this.lblPage.ForeColor), additionTitleLocalX, additionTitleLocalY);

            #endregion

            #region Farpoint����
            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            //int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel3.Height - neuPanel2.Height; 
            int drawingHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount);
            this.neuFpEnter1.OwnerPrintDraw(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel3.Height, drawingWidth, drawingHeight), 0, this.curPageNO);
            int FarpointHeight = 0;
            if (curPageNO == maxPageNO)
            {
                if (alPrintData.Count % printBill.RowCount == 0)
                {
                    FarpointHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount + 5);
                }
                else
                {
                    FarpointHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * (alPrintData.Count % printBill.RowCount) + 5);
                }
            }
            else
            {
                FarpointHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount + 5);
            }
            #endregion

            #region ҳβ����
            additionTitleLocalX = this.DrawingMargins.Left + this.lblOper.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblOper.Location.Y + this.neuPanel3.Height + FarpointHeight;
            graphics.DrawString(this.lblOper.Text, this.lblOper.Font, new SolidBrush(this.lblOper.ForeColor), additionTitleLocalX, additionTitleLocalY);

            additionTitleLocalX = this.DrawingMargins.Left + this.lblGetOper.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblGetOper.Location.Y + this.neuPanel3.Height + FarpointHeight;
            graphics.DrawString(this.lblGetOper.Text, this.lblGetOper.Font, new SolidBrush(this.lblGetOper.ForeColor), additionTitleLocalX, additionTitleLocalY);

            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            additionTitleLocalX = this.DrawingMargins.Left + this.lblExportOper.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.lblExportOper.Location.Y + this.neuPanel3.Height + FarpointHeight;
            graphics.DrawString(this.lblExportOper.Text, this.lblReciveDept.Font, new SolidBrush(this.lblExportOper.ForeColor), additionTitleLocalX, additionTitleLocalY);
            
            additionTitleLocalX = this.DrawingMargins.Left + this.neuLabel9.Location.X;
            additionTitleLocalY = this.DrawingMargins.Top + this.neuLabel9.Location.Y + this.neuPanel3.Height + FarpointHeight;
            graphics.DrawString(this.neuLabel9.Text, this.neuLabel9.Font, new SolidBrush(this.neuLabel9.ForeColor), additionTitleLocalX, additionTitleLocalY);

            if (this.curPageNO == this.maxPageNO)
            {
                if (this.isRetailPrice)
                {
                    additionTitleLocalX = this.DrawingMargins.Left + this.lblTotRetailCost.Location.X;
                    additionTitleLocalY = this.DrawingMargins.Top + this.lblTotRetailCost.Location.Y + this.neuPanel3.Height + FarpointHeight;
                    graphics.DrawString(this.lblTotRetailCost.Text, this.lblTotRetailCost.Font, new SolidBrush(this.lblTotRetailCost.ForeColor), additionTitleLocalX, additionTitleLocalY);
                }
                else
                {
                    additionTitleLocalX = this.DrawingMargins.Left + this.lblTotPurCost.Location.X;
                    additionTitleLocalY = this.DrawingMargins.Top + this.lblTotPurCost.Location.Y + this.neuPanel3.Height + FarpointHeight;
                    graphics.DrawString(this.lblTotPurCost.Text, this.lblTotPurCost.Font, new SolidBrush(this.lblTotPurCost.ForeColor), additionTitleLocalX, additionTitleLocalY);
                }
                
                //additionTitleLocalX = this.DrawingMargins.Left + this.lblTotDif.Location.X;
                //additionTitleLocalY = this.DrawingMargins.Top + this.lblTotDif.Location.Y + this.neuPanel3.Height + FarpointHeight;
                //graphics.DrawString(this.lblTotDif.Text, this.lblTotDif.Font, new SolidBrush(this.lblTotDif.ForeColor), additionTitleLocalX, additionTitleLocalY);
            }
            if (this.isRetailPrice)
            {
                additionTitleLocalX = this.DrawingMargins.Left + this.lblCurRet.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblCurRet.Location.Y + this.neuPanel3.Height + FarpointHeight;
                TotCost totCostByPage = (TotCost)hsTotCostByPage[curPageNO];
                graphics.DrawString("��ҳ�ϼƣ��ۼ۽�" + totCostByPage.retailCost.ToString("F2"), this.lblCurPur.Font, new SolidBrush(this.lblCurPur.ForeColor), additionTitleLocalX, additionTitleLocalY);
     
            }
            else
            {
                additionTitleLocalX = this.DrawingMargins.Left + this.lblCurPur.Location.X;
                additionTitleLocalY = this.DrawingMargins.Top + this.lblCurPur.Location.Y + this.neuPanel3.Height + FarpointHeight;
                TotCost totCostByPage = (TotCost)hsTotCostByPage[curPageNO];
                graphics.DrawString("��ҳ�ϼƣ������" + totCostByPage.purchaseCost.ToString("F2"), this.lblCurPur.Font, new SolidBrush(this.lblCurPur.ForeColor), additionTitleLocalX, additionTitleLocalY);
            }
           
            //additionTitleLocalX = this.DrawingMargins.Left + this.lblCurDif.Location.X;
            //additionTitleLocalY = this.DrawingMargins.Top + this.lblCurDif.Location.Y + this.neuPanel3.Height + FarpointHeight;
            //graphics.DrawString("����" + (totCostByPage.retailCost - totCostByPage.purchaseCost).ToString(Function.GetCostDecimalString()), this.lblCurDif.Font, new SolidBrush(this.lblCurDif.ForeColor), additionTitleLocalX, additionTitleLocalY);
            #endregion

            #region ��ҳ
            if (this.curPageNO < this.socPrintPageSelectDialog.ToPageNO && this.curPageNO < maxPageNO)
            {
                e.HasMorePages = true;
                curPageNO++;
            }
            else
            {
                curPageNO = 1;
                //maxPageNO = 1;
                e.HasMorePages = false;
            }
            #endregion
        }

        /// <summary>
        /// ��ӡҳ��ѡ��
        /// </summary>
        private bool ChoosePrintPageNO(Graphics graphics)
        {

            int drawingWidth = this.printBill.PageSize.Width - this.DrawingMargins.Left - this.DrawingMargins.Right;
            //{F450BE06-02D2-4314-8AF8-3DC481D44BC3}
            //int drawingHeight = this.printBill.PageSize.Height - this.DrawingMargins.Top - this.DrawingMargins.Bottom - this.neuPanel3.Height - this.neuPanel2.Height;
            int drawingHeight = (int)(this.neuFpEnter1.Height + this.neuFpEnter1.Sheets[0].Rows.Default.Height * printBill.RowCount);

            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = true;
            printInfo.PrintType = FarPoint.Win.Spread.PrintType.All;
            printInfo.ShowRowHeaders = this.neuFpEnter1_Sheet1.RowHeader.Visible;
            this.neuFpEnter1_Sheet1.PrintInfo = printInfo;
            this.maxPageNO = neuFpEnter1.GetOwnerPrintPageCount(graphics, new Rectangle(this.DrawingMargins.Left, this.DrawingMargins.Top + this.neuPanel3.Height, drawingWidth, drawingHeight), 0);

            socPrintPageSelectDialog.MaxPageNO = this.maxPageNO;
            if (this.maxPageNO > 1)
            {
                socPrintPageSelectDialog.StartPosition = FormStartPosition.CenterScreen;
                socPrintPageSelectDialog.ShowDialog();
                if (socPrintPageSelectDialog.ToPageNO == 0)
                {
                    return false;
                }
            }

            return true;
        }

        protected void PrintView(System.Drawing.Printing.PaperSize paperSize)
        {
            this.SetPaperSize(paperSize);
            this.myPrintView();
        }

        protected void PrintView()
        {
            this.SetPaperSize(null);
            this.myPrintView();
        }

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
            if (paperSize == null)
            {
                paperSize = new System.Drawing.Printing.PaperSize(printBill.PageSize.ID, printBill.PageSize.Width, printBill.PageSize.Height);
            }

            this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
            this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        PrintPreviewDialog printPreviewDialog = null;

        private void myPrintView()
        {
            printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = this.PrintDocument;
            try
            {
                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
            }
            catch { }
            try
            {
                printPreviewDialog.ShowDialog();
                printPreviewDialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ӡ������" + ex.Message);
            }
        }

        protected void PrintPage(System.Drawing.Printing.PaperSize paperSize)
        {
            this.SetPaperSize(paperSize);
            this.PrintDocument.Print();
        }

        protected void PrintPage()
        {
            this.SetPaperSize(null);
            this.PrintDocument.Print();
        }

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
            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.IsDataAutoExtend = false;
            //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            //p.IsHaveGrid = true;
            //p.SetPageSize(this.printBill.PageSize);
            #endregion

            #region ��ҳ��ӡ
            int height = this.neuPanel3.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.��λ��)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.����˳��)
            {
                Base.PrintBill.SortByBillNO(ref alPrintData);
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
                    tc.wholeSaleCost += o.WholeSaleCost;
                    tc.retailCost += o.RetailCost;
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(o);
                    hs.Add(o.OutListNO, al);

                    TotCost tc = new TotCost();
                    tc.purchaseCost = o.PurchaseCost;
                    tc.wholeSaleCost = o.WholeSaleCost;
                    tc.retailCost = o.RetailCost;
                    hsTotCost.Add(o.OutListNO, tc);
                }
            }

            //�ֵ��ݴ�ӡ
            foreach (ArrayList alPrint in hs.Values)
            {
                this.SetPrintData(alPrint, 1, 1, printBill.Title);
                alPrintData = alPrint.Clone() as ArrayList;
                if (this.printBill.IsNeedPreview)
                {
                    if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                    {
                        this.PrintView(null);
                    }
                }
                else
                {
                    if (this.ChoosePrintPageNO(Graphics.FromHwnd(this.Handle)))
                    {
                        this.PrintPage(null);
                    }
                }
            }
            #endregion

            return 1;
        }

        public int SetPrintData(ArrayList alPrintData, Base.PrintBill printBill)
        {
            isReprint = false;
            if (this.alPrintData == null || alPrintData.Count == 0)
            {
                return 1;
            }

            foreach (FS.HISFC.Models.Pharmacy.Output info in alPrintData)
            {
                if (!string.IsNullOrEmpty(info.SpecialFlag) && info.SpecialFlag.Contains("����"))
                {
                    isReprint = true;
                }
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
