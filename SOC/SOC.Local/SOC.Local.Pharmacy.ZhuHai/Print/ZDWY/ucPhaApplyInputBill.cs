using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Print.ZDWY
{
    /// <summary>
    /// ��ݸҩƷ��ⵥ�ݴ�ӡ
    /// </summary>
    public partial class ucPhaApplyInputBill : ucBaseControl, Base.IPharmacyBillPrint
    {
        /// <summary>
        /// ҩƷ����ӡ��
        /// </summary>
        public ucPhaApplyInputBill()
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

        private FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();

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

            FS.HISFC.Models.Pharmacy.ApplyOut info = (FS.HISFC.Models.Pharmacy.ApplyOut)al[0];

            #region label��ֵ
            if (string.IsNullOrEmpty(title))
            {
                this.lblTitle.Text = string.Format(this.lblTitle.Text, FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID));
            }
            else
            {
                if (title.IndexOf("[������]") != -1)
                {


                    this.lblTitle.Text = title.Replace("[������]", FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.ApplyDept.ID));

                }
                else
                {
                    this.lblTitle.Text = title;
                }
            }

            if (info.State == "2")
            {
                this.lblTitle.Text = "(����)" + this.lblTitle.Text;
            }

            this.lblApplyDept.Text = "����ҩ�� " + info.ApplyDept.ID;
            this.lblTargetDept.Text = "����ҩ�� " + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(info.StockDept.ID);
            this.lblBillID.Text = "���쵥�� " + info.BillNO;
            this.lblInputDate.Text = "�������� " + info.Operation.ApplyOper.OperTime.ToShortDateString() + " " + info.Operation.ApplyOper.OperTime.Hour.ToString().PadLeft(2, '0') +"��" + info.Operation.ApplyOper.OperTime.Minute.ToString().PadLeft(2, '0');
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

            this.neuFpEnter1_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = al[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.Item itemInfo = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = itemInfo.NameCollection.UserCode;
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = itemInfo.Name;
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = itemInfo.Specs;
                decimal packqty = applyOut.Operation.ApplyQty / applyOut.Item.PackQty;
                if (packqty == Math.Ceiling(applyOut.Operation.ApplyQty / applyOut.Item.PackQty))
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = (applyOut.Operation.ApplyQty / applyOut.Item.PackQty).ToString();
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = (applyOut.Operation.ApplyQty / applyOut.Item.PackQty).ToString("F2");
                }

                try 
                {
                    FS.HISFC.Models.Pharmacy.Storage storageInfo = this.storageMgr.GetStockInfoByDrugCode(applyOut.StockDept.ID, applyOut.Item.ID);
                    decimal packqty1 = (storageInfo.StoreQty / applyOut.Item.PackQty);
                    if (packqty1 == (storageInfo.StoreQty / applyOut.Item.PackQty))
                    {
                        this.neuFpEnter1_Sheet1.Cells[i, 5].Text = (storageInfo.StoreQty / applyOut.Item.PackQty).ToString();
                    }
                    else
                    {
                        this.neuFpEnter1_Sheet1.Cells[i, 5].Text = (storageInfo.StoreQty / applyOut.Item.PackQty).ToString("F2");
                    }
                }
                catch (Exception e) 
                {
                
                }
                this.neuFpEnter1_Sheet1.Cells[i, 6].Text = itemInfo.PackUnit;
                this.neuFpEnter1_Sheet1.Cells[i, 7].Text = applyOut.State;
                this.neuFpEnter1_Sheet1.Cells[i, 8].Text = applyOut.ID;
              


            }

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
            int height = this.neuPanel5.Height;
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
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut input in alPrintData)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut i = input.Clone();
                if (hs.Contains(i.BillNO))
                {

                    ArrayList al = (ArrayList)hs[i.BillNO];
                    al.Add(i);
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(i);
                    hs.Add(i.BillNO, al);
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
            this.alPrintData = alPrintData;
            this.printBill = printBill;
            return this.Print();
        }

        public int Preview()
        {
            this.printBill.IsNeedPreview = true;
            return this.Print();
        }

        #endregion

        private void lblOper_Click(object sender, EventArgs e)
        {

        }


    }
}
