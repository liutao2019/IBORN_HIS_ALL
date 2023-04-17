using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Controls;

namespace FS.SOC.Local.Material.Print.FoSi
{
    public partial class ucMatInputBill : UserControl, FS.HISFC.Interface.Material.Print.IBillPrint
    {
        FS.HISFC.InterfaceArchieve.Material.Manager.ManagerAchieve matManager = new FS.HISFC.InterfaceArchieve.Material.Manager.ManagerAchieve();
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

        FS.HISFC.BizProcess.Material.Base.BaseProcess baseStock = new FS.HISFC.BizProcess.Material.Base.BaseProcess();

        public ucMatInputBill()
        {
            InitializeComponent();
        }


        ArrayList alPrintData = new ArrayList();


        /// <summary>
        /// 总金额
        /// </summary>
        private decimal totCost = 0M;

        /// <summary>
        /// 总零售金额
        /// </summary>
        private decimal totSaleCost = 0M;


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

        public int Prieview()
        {
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

            alPrintData.Clear();
            FarPoint.Win.Spread.CellType.NumberCellType nct = new FarPoint.Win.Spread.CellType.NumberCellType();
            nct.DecimalPlaces = 3;
            this.neuFpEnter1_Sheet1.Columns[7].CellType = nct;
            this.neuFpEnter1_Sheet1.Columns[8].CellType = nct;
            this.neuFpEnter1_Sheet1.Columns[9].CellType = nct;
            this.neuFpEnter1_Sheet1.RowCount = 0;


            decimal sumRetail = 0;
            decimal sumPurchase = 0;
            if (DataList != null && DataList.Count > 0)
            {
                string titleIn = this.matManager.GetControlParam<string>("MATS31", true, "");
                FS.HISFC.BizLogic.Material.Object.Input info = DataList[0] as FS.HISFC.BizLogic.Material.Object.Input;

                string type = string.Empty;

                switch (info.InClass3)
                {
                    case "01":
                        type = "一般入库";
                        break;
                    case "04":
                        type = "入库退库";
                        break;
                    case "09":
                        type = "特殊入库";
                        break;
                    case "14":
                        type = "赠送入库";
                        break;
                    default:
                        type = "一般入库";
                        break;

                }

                this.lblTitle.Text = titleIn + deptManager.GetDeptmentById(info.StockInfo.Storage.ID).Name + type+ "单"; //本地化

                string company = "";
                this.lblCompany.Text = "供货公司:" + info.StockInfo.Company.ID + " " + info.StockInfo.Company.Name;

                this.lblBillID.Text = "事务单号:" + info.InListCode;
                this.lblInputDate.Text = "事务日期:" + info.InDate.ToShortDateString();
                this.lblOper.Text = "入库人:" + personManager.GetPersonByID(FS.FrameWork.Management.Connection.Operator.ID).Name;
                this.neuLabel10.Text = "仓库负责人";
                this.lblPage.Text = "页:" + pageCount.ToString() + "/" + totPageCount.ToString();

                int i = 0;
                foreach (FS.HISFC.BizLogic.Material.Object.Input input in DataList)
                {
                    //取自定码
                    if (string.IsNullOrEmpty(input.StockInfo.BaseInfo.UserCode))
                    {
                        input.StockInfo.BaseInfo = baseStock.GetBaseInfoByID(input.StockInfo.BaseInfo.ID, false);
                    }


                    this.alPrintData.Add(input);
                    this.neuFpEnter1_Sheet1.AddRows(i, 1);
                    this.neuFpEnter1_Sheet1.Cells[i, 0].Text = input.StockInfo.BaseInfo.UserCode; //自定义码
                    this.neuFpEnter1_Sheet1.Cells[i, 1].Text = input.StockInfo.BaseInfo.Name;//名称
                    this.neuFpEnter1_Sheet1.Cells[i, 2].Text = input.StockInfo.BaseInfo.Specs;//规格		
                    if (input.StockInfo.BaseInfo.PackQty == 0)
                        input.StockInfo.BaseInfo.PackQty = 1;

                    this.neuFpEnter1_Sheet1.Cells[i, 3].Text = input.StockInfo.MinUnit;//单位
                    this.neuFpEnter1_Sheet1.Cells[i, 4].Text = input.InNum.ToString();//数量			

                    this.neuFpEnter1_Sheet1.Cells[i, 6].Text = input.StockInfo.BatchNo; //批次号
                    this.neuFpEnter1_Sheet1.Cells[i, 7].Value = input.InPrice;  //购入单价
                    this.neuFpEnter1_Sheet1.Cells[i, 8].Value = input.InNum * input.InPrice / input.StockInfo.BaseInfo.PackQty;//购入金额
                    this.neuFpEnter1_Sheet1.Cells[i, 9].Value = input.InSaleCost; //零售金额

                    sumRetail = sumRetail + input.InSaleCost;
                    sumPurchase = sumPurchase + input.InNum * input.InPrice / input.StockInfo.BaseInfo.PackQty;
                    i++;
                }

                this.lblCurRet.Text =  sumRetail.ToString("F2");
                this.lblCurPur.Text =  sumPurchase.ToString("F2");
            }
            return 1;
        }

        public int SetPrintDataTotal(List<FS.FrameWork.Models.NeuObject> DataList)
        {
            totCost = 0;
            totSaleCost = 0;
            if (DataList != null && DataList.Count > 0)
            {
                foreach (FS.HISFC.BizLogic.Material.Object.Input inPut in DataList)
                {
                    totCost += inPut.InNum * inPut.InPrice;
                    totSaleCost += inPut.InSaleCost;
                }
            }
            return 1;
        }

       

        public int Print()
        {
            int pageHeight = this.matManager.GetControlParam<int>("MATS33", true, 100); //纸张高度

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            p.IsHaveGrid = true;
            p.SetPageSize(new FS.HISFC.Models.Base.PageSize("MAT_INPUT",850,pageHeight));
            
            int height = this.neuPanel5.Height;
            int ucHeight = this.Height;
            float rowHeight = this.neuFpEnter1_Sheet1.Rows[0].Height;
            this.neuFpEnter1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.neuPanel5.Height += (int)rowHeight * alPrintData.Count;
            this.Height += (int)rowHeight * alPrintData.Count;
            p.PrintPage(5, 0, this.neuPanel1);

            this.neuPanel5.Height = height;
            this.Height = ucHeight;

            return 1;
        }
    }
}
