using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Material.Print.GYSY
{
    public partial class ucMatOutputBill : UserControl, FS.HISFC.Interface.Material.Print.IBillPrint
    {
        public ucMatOutputBill()
        {
            InitializeComponent();
        }

        FS.HISFC.InterfaceArchieve.Material.Manager.ManagerAchieve matManager = new FS.HISFC.InterfaceArchieve.Material.Manager.ManagerAchieve();
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
        FS.HISFC.BizLogic.Material.BizLogic.Store.StockLogic stockManager = new FS.HISFC.BizLogic.Material.BizLogic.Store.StockLogic();

        FS.HISFC.BizProcess.Material.Base.BaseProcess baseStock = new FS.HISFC.BizProcess.Material.Base.BaseProcess();


        #region 变量

        /// <summary>
        /// 所有待打印数据
        /// </summary>
        private ArrayList alPrintData = new ArrayList();

        private int rowcount = 8;

        /// <summary>
        /// 总金额
        /// </summary>
        private decimal totCost = 0M;

        /// <summary>
        /// 总零售金额
        /// </summary>
        private decimal totSaleCost = 0M;
        #endregion

        int totRows = 0;

        #region IBillPrint 成员

        public bool IsRePrint
        {
            get
            {
                return true;
            }
            set
            {
                
            }
        }

        public int  Prieview()
        {
            return this.Print();
        }

        public int Print()
        {
            #region 打印信息设置 

            int pageHeight = this.matManager.GetControlParam<int>("MATS33", true, 100); //纸张高度
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            //this.printBill.PageSize.HeightMM = pageHeight;
            //p.SetPageSize(this.printBill.PageSize);
            p.SetPageSize(new FS.HISFC.Models.Base.PageSize("MAT_OUTPUT", 850, pageHeight));

            #endregion

            #region 分页打印
            int height = this.neuPanel4.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

            this.neuPanel4.Height = this.neuPanel4.Height + (int)rowHeight * alPrintData.Count;
            this.Height += (int)rowHeight * alPrintData.Count;

            p.PrintPage(5, 0, this.neuPanel1);

            this.neuPanel4.Height = height;
            this.Height = ucHeight;
            
            #endregion

            return 1;
        }

        public int PrintRowCount
        {
            get
            {
                return this.matManager.GetControlParam<int>("MATS32", true, 8);
            }
        }

        public int SetPrintData(List<FS.FrameWork.Models.NeuObject> DataList, int pageCount, int totPageCount)
        {
            if (DataList == null || DataList.Count == 0)
            {
                return 1;
            }

            decimal sumRetail = 0;
            decimal sumPurchase = 0;

            FarPoint.Win.Spread.CellType.NumberCellType nct = new FarPoint.Win.Spread.CellType.NumberCellType();
            nct.DecimalPlaces = 4;
            this.neuFpEnter1_Sheet1.Columns[7].CellType = nct;
            this.neuFpEnter1_Sheet1.Columns[8].CellType = nct;
            this.neuFpEnter1_Sheet1.Columns[9].CellType = nct;
            this.neuFpEnter1_Sheet1.RowCount = 0;
            alPrintData.Clear();

            FS.HISFC.BizLogic.Material.Object.Output info = DataList[0] as FS.HISFC.BizLogic.Material.Object.Output;
            this.lblTitle.Text = deptManager.Hospital.Name + "物资出库单"; //本地化


            if(string.IsNullOrEmpty(info.Storage.ID))
            {
                this.lblStorage.Text = "发出仓库:" + deptManager.GetDeptmentById(info.StockHeadInfo.Storage.ID).Name; //本地化
            }
            else
            {
                this.lblStorage.Text = "发出仓库:" + deptManager.GetDeptmentById(info.Storage.ID).Name; //本地化
            }
            this.lblCompany.Text = "领用部门: (" + info.TargetDept.ID + ")" + deptManager.GetDeptmentById(info.TargetDept.ID).Name;
            this.lblBillID.Text = "单号: " + info.OutListCode;
            this.lblInputDate.Text = "领用日期: " + info.OutDate.ToShortDateString();

            this.lblOper.Text = "制单:" + personManager.GetPersonByID(info.Oper.ID).Name; 
          
            this.lblPage.Text = "页：" + pageCount.ToString() + "/" + totPageCount.ToString();

            int i = 0;
            foreach(FS.HISFC.BizLogic.Material.Object.Output output in DataList)
            {
                alPrintData.Add(output);
                this.neuFpEnter1_Sheet1.AddRows(i, 1);
                this.neuFpEnter1_Sheet1.Columns[2].Width = 81F;

                FS.HISFC.BizLogic.Material.Object.StockDetail stockDetail = stockManager.GetStockDetail(output.StockCode);
                if (stockDetail != null && string.IsNullOrEmpty(stockDetail.StockNo) == false)
                {
                    output.OutPrice = stockDetail.SourceInPrice;
                }
                else
                {
                    output.OutPrice = output.StockHeadInfo.StorePrice;
                }


                //取自定码
                if (string.IsNullOrEmpty(output.BaseInfo.UserCode))
                {
                    output.BaseInfo = baseStock.GetBaseInfoByID(output.BaseInfo.ID, false);
                }


                this.neuFpEnter1_Sheet1.Cells[i, 0].Text = output.BaseInfo.UserCode; //药品自定义码
                this.neuFpEnter1_Sheet1.Cells[i, 1].Text = output.BaseInfo.Name;//药品名称
                this.neuFpEnter1_Sheet1.Cells[i, 2].Text = output.BaseInfo.Specs;//规格		
                if (output.BaseInfo.PackQty == 0)
                    output.BaseInfo.PackQty = 1;

                this.neuFpEnter1_Sheet1.Cells[i, 4].Text = output.BaseInfo.MinUnit;//单位
                this.neuFpEnter1_Sheet1.Cells[i, 3].Text = output.OutNum.ToString();//数量				
                this.neuFpEnter1_Sheet1.Cells[i, 6].Value = output.OutPrice;// output.BaseInfo.InPrice;
                this.neuFpEnter1_Sheet1.Cells[i, 8].Value = output.OutSalePrice;


                this.neuFpEnter1_Sheet1.Cells[i, 5].Text =output.StockBatchNo;

                this.neuFpEnter1_Sheet1.Cells[i, 7].Value = (output.OutPrice * output.OutNum).ToString("F4");// 买入金额 (output.BaseInfo.InPrice * output.OutNum).ToString("F4");
                this.neuFpEnter1_Sheet1.Cells[i, 9].Value = output.OutSaleCost.ToString("F4"); //零售金额

                sumRetail = sumRetail + output.OutSaleCost;
                sumPurchase += output.OutPrice * output.OutNum;//output.OutNum * output.BaseInfo.InPrice;
                i++;
            }

            this.lblCurRet.Text = sumRetail.ToString("F2");
            this.lblCurPur.Text = sumPurchase.ToString("F2");


            return 1;
        }

        #endregion

        public int SetPrintDataTotal(List<FS.FrameWork.Models.NeuObject> DataList)
        {
            totCost = 0;
            totSaleCost = 0;
            if (DataList != null && DataList.Count > 0)
            {
                foreach (FS.HISFC.BizLogic.Material.Object.Output outPut in DataList)
                {
                    totCost += outPut.OutNum * outPut.OutPrice;
                    totSaleCost += outPut.OutSaleCost;
                }
            }
            return 1;
        }

        private void neuPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
