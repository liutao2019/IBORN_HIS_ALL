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

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
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

        private bool isReprint = false;

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
            public decimal wholeSaleCost;
            public decimal retailCost;
        }

        FS.HISFC.BizLogic.Manager.PowerLevelManager powerLevelManager = new FS.HISFC.BizLogic.Manager.PowerLevelManager();
        /// <summary>
        /// ����������
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
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

                FS.HISFC.Models.Admin.PowerLevelClass3 privClass3 = powerLevelManager.LoadLevel3ByPrimaryKey("0320", info.PrivType);

                title = title.Replace("[������]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
                if (privClass3 != null)
                {
                    title = title.Replace("[��������]", privClass3.Class3Name);
                }

                this.lblTitle.Text = title;
            }

            FS.HISFC.Models.Base.Department deptInfo = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(info.StockDept.ID);
            this.lblCompany.Text = "ҩƷȥ��: " + info.TargetDept.ID +" "+ SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.TargetDept.ID);
            this.lblBillID.Text = "���˵���: " + info.OutListNO;
            this.lblInputDate.Text = "��������: " + info.OutDate.ToShortDateString() + " " +info.OutDate.Hour.ToString().PadLeft(2,'0') + "��" + info.OutDate.Minute.ToString().PadLeft(2,'0');
            this.lblOper.Text = "�Ƶ���:" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(info.Operation.ExamOper.ID); //BillPrintFun.GetEmplName(info.Operation.ExamOper.ID);
            this.lblPage.Text = "ҳ��" + inow.ToString() + "/" + icount.ToString();
            this.neuLabel10.Visible = true;
            #endregion

            #region farpoint��ֵ
            decimal sumRetail = 0;
            decimal sumWholeSaleCost = 0;
            decimal sumPurchase = 0;
            decimal sumDif = 0;

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.Output output = al[i] as FS.HISFC.Models.Pharmacy.Output;

                FS.HISFC.Models.Pharmacy.Item itemObj = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(output.Item.ID);
                if (ctrlIntegrate.GetControlParam<bool>("HNPHA2", false, true))
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text =  itemObj.NameCollection.RegularName; //ҩƷ�Զ�����                    
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text =  itemObj.Name; //ҩƷ�Զ�����                    
                }
                
                //this.neuFpEnter1_Sheet1.Cells[i, 1].Text = output.Item.Name;//ҩƷ����

                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = itemObj.UserCode; //ҩƷ�Զ�����
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = output.Item.Specs;//���		
                if (output.Item.PackQty == 0)
                    output.Item.PackQty = 1;
                decimal count = 0;
                count = Math.Abs(output.Quantity);//�����˿������Ǹ���  ������Ҫ��ӡ������

                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = 4;
                FarPoint.Win.Spread.CellType.NumberCellType nCost = new FarPoint.Win.Spread.CellType.NumberCellType();
                nCost.DecimalPlaces = Function.GetCostDecimal();

                if (SOC.HISFC.BizProcess.Cache.Common.GetDept(output.StockDept.ID).DeptType.ID.ToString() == "P")
                {
                   
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = output.Item.MinUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = (count).ToString();//����				
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = (count).ToString();//����				
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = (output.Item.PriceCollection.PurchasePrice / output.Item.PackQty).ToString(Function.GetCostDecimalString());//�����
                    //this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (output.Item.PriceCollection.RetailPrice / output.Item.PackQty).ToString(Function.GetCostDecimalString());//���ۼ�
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (output.Item.PriceCollection.WholeSalePrice / output.Item.PackQty).ToString(Function.GetCostDecimalString());//���ۼ�
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = output.Item.PackUnit;//��λ
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = (count / output.Item.PackQty).ToString(Function.GetCostDecimalString());//����	
                    this.neuFpEnter1_Sheet1.Cells[i, 5].Text = (count / output.Item.PackQty).ToString(Function.GetCostDecimalString());//����		
                    this.neuFpEnter1_Sheet1.Cells[i, 6].Value = output.Item.PriceCollection.PurchasePrice;//�����
                    //this.neuFpEnter1_Sheet1.Cells[i, 7].Value = output.Item.PriceCollection.RetailPrice;//���ۼ�
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = output.Item.PriceCollection.WholeSalePrice;//���ۼ�
               
                }
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.PurchaseCost.ToString(Function.GetCostDecimalString()); //������
                //this.neuFpEnter1_Sheet1.Cells[i, 9].Value = output.RetailCost.ToString(Function.GetCostDecimalString()); //�˿���
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = output.WholeSaleCost.ToString(Function.GetCostDecimalString());
                this.neuFpEnter1_Sheet1.Cells[i, 10].Value = (output.RetailCost - output.PurchaseCost).ToString(Function.GetCostDecimalString());//������
                

                //sumRetail = sumRetail + output.RetailCost;
                //sumPurchase = sumPurchase + output.PurchaseCost;
                //sumDif = sumRetail - sumPurchase;
                sumRetail = sumRetail + output.RetailCost;
                sumWholeSaleCost = sumWholeSaleCost + output.WholeSaleCost;
                sumPurchase = sumPurchase + output.PurchaseCost;
                sumDif = sumWholeSaleCost - sumPurchase;
            }
            //��ǰҳ����
            this.lblCurRet.Text = sumWholeSaleCost.ToString(Function.GetCostDecimalString());
            this.lblCurPur.Text = sumPurchase.ToString(Function.GetCostDecimalString());
            this.lblCurDif.Text = sumDif.ToString(Function.GetCostDecimalString());

            if (deptInfo.DeptType.Name.ToString().Contains("ҩ��"))
            {
                this.lblTitle.Text = this.lblTitle.Text.Replace('��', '��');
                this.lblCompany.Text = this.lblCompany.Text.Replace("ҩƷȥ��", "ҩƷ��Դ");
                this.lblBillID.Text = this.lblBillID.Text.Replace("���˵���", "���˵���");
                this.lblInputDate.Text = this.lblInputDate.Text.Replace("��������", "��������");
            }
            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(info.StockDept.ID).DeptType.ID.ToString() != "PI" && isReprint)
            {
                this.lblTitle.Text = "(����)" + this.lblTitle.Text; ;
            }

            //������
            this.lblTotPurCost.Text = ((TotCost)hsTotCost[info.OutListNO]).purchaseCost.ToString(Function.GetCostDecimalString());
            this.lblTotRetailCost.Text = ((TotCost)hsTotCost[info.OutListNO]).wholeSaleCost.ToString(Function.GetCostDecimalString());

            #endregion

            if (SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(info.StockDept.ID).DeptType.ID.ToString() == "PI")
            {
                this.neuLabel10.Visible = true;
                this.neuLabel2.Visible = true;

            }
            else
            {
                this.neuLabel10.Visible = false;
                this.neuLabel2.Visible = false;
            }

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

            if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.��λ��)
            {
                Base.PrintBill.SortByPlaceNO(ref alPrintData);
            }
            else if (this.printBill.Sort == FS.SOC.Local.Pharmacy.ZhuHai.Base.PrintBill.SortType.����˳��)
            {
                //Base.PrintBill.SortByBillNO(ref alPrintData);
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
                int pageTotNum = alPrint.Count / this.printBill.RowCount;
                if (alPrint.Count != this.printBill.RowCount * pageTotNum)
                {
                    pageTotNum++;
                }


                int fromPage = 0;
                int toPage = 0;
                int copyCount = 1;
                frmSelectPages frmSelect = new frmSelectPages();
                frmSelect.PageCount = pageTotNum;
                frmSelect.SetPages();
                DialogResult dRsult = frmSelect.ShowDialog();
                if (dRsult == DialogResult.OK)
                {
                    fromPage = frmSelect.FromPage - 1;
                    toPage = frmSelect.ToPage;
                    copyCount = frmSelect.CopyCount;
                }
                else
                {
                    return 0;
                }

                for (int i = 0; i < copyCount; i++)
                {
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
