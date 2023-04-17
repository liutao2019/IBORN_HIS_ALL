using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Outpatient
{
    public partial class ucHerbalDrugList : UserControl
    {
        public ucHerbalDrugList()
        {
            InitializeComponent();
        }

        #region ����


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
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 800, 550));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        /// <summary>
        /// ����ؼ�����
        /// </summary>
        private void Clear()
        {
            lbName.Text = "";
            lbRecipe.Text = "";
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        public void ShowData(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime, int pageNO, int maxPageNO)
        {
            SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

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

                FS.HISFC.Models.Pharmacy.Item item1 = new Item();

                
                item1 = itemMgr.GetItem(applyOut1.Item.ID);

                if (item1.DosageForm.ID == "26") //��Ƭ��
                {
                    this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text = applyOut1.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut1.Item.DoseUnit;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text = item1.BaseDose.ToString() + applyOut1.Item.DoseUnit + "*" + Math.Round(applyOut1.DoseOnce / item1.BaseDose, 2).ToString() + applyOut1.Item.MinUnit;
                }
                //this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut1.Usage.ID);   
                FS.HISFC.Models.Order.OutPatient.Order order = Common.Function.GetOrder(applyOut1.OrderNO);
                //if (order != null && (order.Memo.Contains("�Լ�") || order.Memo.Contains("����") || order.Memo.Contains("��")))
                //{
                //    memo = order.Memo;                   
                //}
                if (order != null)
                {
                    this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = order.Memo;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 2].Font = new Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                days = applyOut1.Days;
                if (applyOut1.Item.PackQty == 0)
                {
                    applyOut1.Item.PackQty = 1;
                }
                totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut1.Item.PriceCollection.RetailPrice * (applyOut1.Operation.ApplyQty  / applyOut1.Item.PackQty)).ToString("F2"));
                index++;

                //�ڶ���ҩ
                if (index < alData.Count)
                {
                    ApplyOut applyOut2 = alData[index] as ApplyOut;


                    FS.HISFC.Models.Pharmacy.Item item2 = new Item();


                    item2 = itemMgr.GetItem(applyOut2.Item.ID);

                    this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text = applyOut2.Item.Name;
                    //this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text = applyOut2.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut2.Item.DoseUnit;

                    this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text = item2.BaseDose.ToString() + applyOut2.Item.DoseUnit + "*" + Math.Round(applyOut2.DoseOnce / item2.BaseDose, 2).ToString() + applyOut2.Item.MinUnit;

                    //this.neuSpread1_Sheet1.Cells[rowIndex, 5].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut2.Usage.ID);  
                    FS.HISFC.Models.Order.OutPatient.Order order2 = Common.Function.GetOrder(applyOut2.OrderNO);
                    if (order != null)
                    {
                        this.neuSpread1_Sheet1.Cells[rowIndex, 5].Text = order2.Memo;
                        this.neuSpread1_Sheet1.Cells[rowIndex, 5].Font = new Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    }
                    if (applyOut2.Item.PackQty == 0)
                    {
                        applyOut2.Item.PackQty = 1;
                    }                    
                    totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut2.Item.PriceCollection.RetailPrice * (applyOut2.Operation.ApplyQty  / applyOut2.Item.PackQty)).ToString("F2"));

                    index++;
                }
            }

            if (maxPageNO > 1)
            {
                this.lblPageNO.Visible = true;
            }
            else
            {
                this.lblPageNO.Visible = false;
            }

            //���һ��
            ApplyOut applyOut3 = alData[0] as ApplyOut;
            this.lblPageNO.Text = "��" + pageNO.ToString() + "ҳ/��" + maxPageNO.ToString() + " ҳ";
            string lastRowValue = "��ҩ�ۣ�" + totCost.ToString() + "Ԫ       �� " + days.ToString("F4").TrimEnd('0').TrimEnd('.') + " ��     �� " + alData.Count.ToString() + " ζ       " + " �÷���" + SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut3.Usage.ID)+"                ";

            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;            
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Value = lastRowValue;
            this.neuSpread1_Sheet1.Cells.Get(this.neuSpread1_Sheet1.RowCount - 1, 0).Font = new Font("����", 11F, FontStyle.Bold);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Border = new FarPoint.Win.LineBorder(System.Drawing.Color.Black, 1, false, true, false, false);
            
            #endregion
        }

        public int Print(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime,int isAuto)
        {
            this.Clear();

            if (alData == null || drugRecipe == null)
            {
                return -1;
            }

            #region ������Ϣ
            //����
            lbName.Text = "������"+drugRecipe.PatientName + "  �Ա�" + drugRecipe.Sex.Name + "  ���䣺" + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
            //������
            this.lbRecipe.Text = "�����ţ�" + drugRecipe.RecipeNO;
            this.lblCardNO.Text = "�����ţ�" + drugRecipe.CardNO;
            this.nlbDoct.Text = "ҽ  ����" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.lblDiagNose.Text = "��ϣ�" + diagnose;
            this.lblPrintTime.Text = "��ӡʱ�䣺" + printTime.ToString();
            
            if (isAuto == 2){
                this.lbReprint.Text = "����";
            }else if (isAuto == 1){
                this.lbReprint.Text = "";
            }

            #endregion 

            //��ҳ
            decimal maxPageNO = Math.Ceiling((decimal)alData.Count / 25);
            for (int pageNO = 0; pageNO < maxPageNO; pageNO++)
            {
                int count = 25;
                if (count + pageNO * 25 > alData.Count)
                {
                    count = alData.Count - pageNO * 25;
                }
                ArrayList alOnePage = new ArrayList(); 
                alOnePage=alData.GetRange(pageNO * 25, count);
                this.ShowData(alOnePage, diagnose, drugRecipe, drugTerminal, hospitalName, printTime, pageNO + 1, (int)maxPageNO);

                this.PrintPage();
            }
           
           
            return 0;
        }

        private void neuPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblPageNO_Click(object sender, EventArgs e)
        {

        }

    }
}
