using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.DrugStore.IBORN
{
    public partial class ucRecipeInfoLabelHerbalIBORN : UserControl
    {

        FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
        FS.HISFC.Models.Base.PageSize pageSize;
        FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        FS.HISFC.BizLogic.Order.OutPatient.Order orderOutpatientManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();


        public ucRecipeInfoLabelHerbalIBORN()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 清除显示信息
        /// </summary>
        private void Clear()
        {
            foreach (Control c in this.Controls)
            {
                if ((c is Label))
                {
                    if (c.Name.Substring(0,2) != "lb")
                    {
                        continue;
                    }

                    (c as Label).Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="alData"></param>
        /// <param name="drugRecipe"></param>
        /// <param name="drugTerminal"></param>
        /// <returns></returns>
        public int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal, bool isPrintRegularName, string hospitalName, bool isPrintMemo, string diagnose, DateTime printTime)
        {
            if (alData == null || drugRecipe == null || drugTerminal == null)
            {
                return -1;
            }

            Dictionary<string, int> varietyDic = new Dictionary<string, int>();
            Dictionary<string, decimal> moneyDic = new Dictionary<string, decimal>();
            Dictionary<string, string> printedDic = new Dictionary<string, string>();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
            {
                if (varietyDic.Keys.Contains(applyInfo.RecipeNO))
                {
                    varietyDic[applyInfo.RecipeNO] +=   1;
                }
                else
                {
                    varietyDic.Add(applyInfo.RecipeNO,1);
                }

                //decimal money = Math.Round((applyInfo.Item.PriceCollection.RetailPrice / applyInfo.Item.PackQty) * applyInfo.Operation.ApplyQty,2);
                decimal money = FS.FrameWork.Public.String.TruncateNumber(applyInfo.Item.PriceCollection.RetailPrice * (applyInfo.Operation.ApplyQty / applyInfo.Item.PackQty), 2);
                    

                if (moneyDic.Keys.Contains(applyInfo.RecipeNO))
                {
                    moneyDic[applyInfo.RecipeNO] += money;
                }
                else
                {
                    moneyDic.Add(applyInfo.RecipeNO,money);
                }
            }


            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyInfo in alData)
            {

                if(printedDic.Keys.Contains(applyInfo.RecipeNO))
                {
                    continue;
                }
                else
                {
                    printedDic.Add(applyInfo.RecipeNO,applyInfo.RecipeNO);
                }

                this.Clear();
                FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyInfo.Item.ID);
                FS.HISFC.Models.Registration.Register regInfo = regMgr.GetByClinic(drugRecipe.ClinicNO);
                FS.HISFC.Models.Order.Order recipeInfo = new FS.HISFC.Models.Order.Order();
                recipeInfo = this.orderOutpatientManager.QueryOneOrder(applyInfo.OrderNO);

                FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;


                FS.FrameWork.Models.NeuObject hos = constantMgr.GetConstant("HOSPITALLIST", curDepartment.HospitalID);

                this.lbHospitalName.Text = hos.Name;
                this.lbRecipeNO.Text = drugRecipe.RecipeNO;

                this.lbName.Text = drugRecipe.PatientName + "   " + drugRecipe.Sex.Name + "   " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
                this.lbAddressPhone.Text = regInfo.PhoneHome + " " + regInfo.AddressHome;
                this.lbNum.Text = applyInfo.Days.ToString() ;
                this.lbUseWay.Text = applyInfo.Usage.Name + " " + Common.Function.GetFrequenceName(applyInfo.Frequency); ;
                this.lbVarietyNum.Text = varietyDic[applyInfo.RecipeNO].ToString();

                this.lbDoctor.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(recipeInfo.ReciptDoctor.ID);
                this.lbMoney.Text = moneyDic[applyInfo.RecipeNO].ToString();

                this.lbPhamacyName.Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyInfo.StockDept.ID);
                this.lbPrintTime.Text = this.orderOutpatientManager.GetSysDateTime();

                //this.nlbRePrint.Visible = !(drugRecipe.RecipeState == "0");
                //this.nlbPlaceNo.Text = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemPlaceNo(applyInfo.StockDept.ID, applyInfo.Item.ID);//货位号
                this.Print();

            }

            return 1;
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            if (pageSize == null)
            {
                pageSize = pageSizeMgr.GetPageSize("OutPatientDrugLabel");
                if (pageSize != null && pageSize.Printer.ToLower() == "default")
                {
                    pageSize.Printer = "";
                }
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize("OutPatientDrugLabel", 315, 138);
                }
            }
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
            }
        }
    }
}
