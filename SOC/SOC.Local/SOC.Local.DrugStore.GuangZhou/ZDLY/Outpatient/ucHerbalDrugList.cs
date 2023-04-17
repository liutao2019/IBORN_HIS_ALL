using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;

namespace FS.SOC.Local.DrugStore.GuangZhou.ZDLY.Outpatient
{
    public partial class ucHerbalDrugList : UserControl
    {
        public ucHerbalDrugList()
        {
            InitializeComponent();
        }

        #region ����
        private FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();

        #endregion

        /// <summary>
        /// ���������뷽��
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, 150, 50);
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 583, 800));
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 583, 850));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 0, this);
            }
            else
            {
                print.PrintPage(5, 0, this);
            }
        }

        /// <summary>
        /// ����ؼ�����
        /// </summary>
        private void Clear()
        {
            lbName.Text = "";
            lbRecipe.Text = "";
            this.lblSeeNo.Text = "";
            this.lblAge.Text = "";
            this.lblSex.Text = "";
            this.lblCardNO.Text = "";
            this.lblSeeNo.Text = "";
            this.lblDeptName.Text = "";
            this.lblDiagNose.Text = "";
            this.lbRegisterDate.Text = "";
            this.nlbDoct.Text = "";
            this.lbRecipe.Text = "";
            this.lblPhone.Text = "";
            this.lbTotCost.Text = "";
            this.lblFeeOper.Text = "";
            this.lblPageNO.Text = "";
            this.lblPrintTime.Text = "";
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        public void ShowData(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime, int pageNO, int maxPageNO)
        {

            #region ��ϸ��Ϣ
            decimal totCost = 0;

            decimal days = 0;
            this.neuSpread1_Sheet1.RowCount = 0;

            //����������Ҫ���ӣ�������Ҫ��ҳ������ʱע�����һ�еĻ�����Ϣ
            this.neuSpread1_Sheet1.RowCount = (alData.Count + 1) / 2 + 1;
            int index = 0;
            //string memo = "";
            for (int rowIndex = 0; rowIndex < alData.Count && index < alData.Count; rowIndex++)
            {
                //��һ��ҩ
                ApplyOut applyOut1 = alData[index] as ApplyOut;

                this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text = "  " + applyOut1.Item.Name;
                this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text = (applyOut1.DoseOnce).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut1.Item.DoseUnit;
                FS.HISFC.Models.Order.OutPatient.Order order = Common.Function.GetOrder(applyOut1.OrderNO);
                if (order != null)
                {
                    this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = order.Memo;
                }
                days = applyOut1.Days;
                if (applyOut1.Item.PackQty == 0)
                {
                    applyOut1.Item.PackQty = 1;
                }
                totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut1.Item.PriceCollection.RetailPrice * (applyOut1.Operation.ApplyQty / applyOut1.Item.PackQty)).ToString("F2"));
                index++;

                //�ڶ���ҩ
                if (index < alData.Count)
                {
                    ApplyOut applyOut2 = alData[index] as ApplyOut;

                    this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text = applyOut2.Item.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text = applyOut2.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut2.Item.DoseUnit;
                    FS.HISFC.Models.Order.OutPatient.Order order2 = Common.Function.GetOrder(applyOut2.OrderNO);
                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[rowIndex, 5].Text = order2.Memo;
                    }
                    if (applyOut2.Item.PackQty == 0)
                    {
                        applyOut2.Item.PackQty = 1;
                    }
                    totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut2.Item.PriceCollection.RetailPrice * (applyOut2.Operation.ApplyQty / applyOut2.Item.PackQty)).ToString("F2"));

                    index++;
                }
            }

            this.lbTotCost.Text =" ���ϼƣ�" + totCost.ToString() + "Ԫ";

            //���һ��
            ApplyOut applyOut3 = alData[0] as ApplyOut;
            this.lblPageNO.Text = "��" + pageNO.ToString() + "ҳ/��" + maxPageNO.ToString() + "ҳ";
            string lastRowValue = "�÷�����" + days.ToString("F4").TrimEnd('0').TrimEnd('.') + " ��     �� " + alData.Count.ToString() + " ζ       " + "" + SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut3.Usage.ID) + "                ";

            this.lblTotal.Text = lastRowValue;
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);

            #endregion
        }

        public int Print(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime)
        {
            this.Clear();

            if (alData == null || drugRecipe == null)
            {
                return -1;
            }
            FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
            register = registerMgr.GetByClinic(drugRecipe.ClinicNO);

            #region ������Ϣ
            //����
            lbName.Text = "������" + drugRecipe.PatientName;
            //������
            this.lbRecipe.Text = "�����ţ�" + drugRecipe.RecipeNO;
            this.lblSex.Text = "�Ա�" + drugRecipe.Sex.Name;
            this.lblAge.Text = "���䣺" + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
            this.lblCardNO.Text = "�����ţ�" + drugRecipe.CardNO;
            this.nlbDoct.Text = "ҽ����" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.lblDiagNose.Text = "��ϣ�" + diagnose;
            this.lblPrintTime.Text = "��ӡʱ�䣺" + printTime.ToString("yyyy-MM-dd hh:mm");
            this.lbRegisterDate.Text = "�Һ����ڣ�" + drugRecipe.RegTime.ToString("yyyy-MM-dd");
            if (register != null)
            {
                this.lbName.Text += "   " + register.Pact.Name.ToString();
                this.lblPhone.Text = "�绰��"+ register.PhoneHome.ToString();
                this.lblSeeNo.Text = "��ˮ�ţ�" + register.OrderNO.ToString();
            }
            this.lblFeeOper.Text = "�շѣ�" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.FeeOper.ID);

            #endregion

            //��ҳ
            decimal maxPageNO = Math.Ceiling((decimal)alData.Count / 50);
            for (int pageNO = 0; pageNO < maxPageNO; pageNO++)
            {
                int count = 50;
                if (count + pageNO * 50 > alData.Count)
                {
                    count = alData.Count - pageNO * 50;
                }
                ArrayList alOnePage = new ArrayList();
                alOnePage = alData.GetRange(pageNO * 50, count);
                this.ShowData(alOnePage, diagnose, drugRecipe, drugTerminal, hospitalName, printTime, pageNO + 1, (int)maxPageNO);

                this.PrintPage();
            }


            return 0;
        }


    }
}
