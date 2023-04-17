using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;

namespace FS.SOC.Local.DrugStore.FuYou.Outpatient
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
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 550, 800));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            print.PrintDocument.DefaultPageSettings.Landscape = true;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 0, this);
            }
            else
            {
                print.PrintPage(0, 0, this);
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

        public void ShowData(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName)
        {

            #region ��ϸ��Ϣ
            decimal totCost = 0;

            decimal days = 0;

            FarPoint.Win.IBorder lineBorder = new FarPoint.Win.LineBorder(Color.Black, 1, false, true, false, true);
            FarPoint.Win.IBorder buttomBorder = new FarPoint.Win.LineBorder(Color.Black, 1, false,false, false, true);

            //����������Ҫ���ӣ�������Ҫ��ҳ������ʱע�����һ�еĻ�����Ϣ
            this.neuSpread1_Sheet1.RowCount = 0;
            for (int i = 0; i < alData.Count; i++)
            {
                //��һ��ҩ
                ApplyOut applyOut1 = alData[i] as ApplyOut;
                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count,1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count-1, 0].Text = applyOut1.Item.Name;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count-1, 1].Text = applyOut1.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut1.Item.MinUnit;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count-1, 2].Text = SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut1.Usage.ID);
                days = applyOut1.Days;
                if (applyOut1.Item.PackQty == 0)
                {
                    applyOut1.Item.PackQty = 1;
                }
                totCost += FS.FrameWork.Function.NConvert.ToDecimal((applyOut1.Item.PriceCollection.RetailPrice * (applyOut1.Operation.ApplyQty * applyOut1.Days / applyOut1.Item.PackQty)).ToString("F2"));
           
            }

            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 3;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "��  " + days.ToString() + "  ��";
            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = lineBorder;

            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = 3;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "�ܼƣ�" + totCost.ToString() + "Ԫ   ˾ҩԱ��      �˶ԣ�  ";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = buttomBorder;
            
            #endregion
        }

        public int Print(ArrayList alData, string diagnose, DrugRecipe drugRecipe, DrugTerminal drugTerminal, string hospitalName, DateTime printTime)
        {
            this.Clear();
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            this.neuSpread1_Sheet1.Rows.Default.Font = new Font("����", 15F, FontStyle.Bold);

            if (alData == null || drugRecipe == null)
            {
                return -1;
            }

            #region ������Ϣ
            //����
            lbName.Text = drugRecipe.PatientName + " " + drugRecipe.Sex.Name + " " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(drugRecipe.Age);
            //������
            this.lbRecipe.Text = drugRecipe.RecipeNO;

            this.lbDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugRecipe.DoctDept.ID);
            this.lbDoct.Text = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(drugRecipe.Doct.ID);
            this.nlbPrintTime.Text = "��ӡʱ�䣺" + printTime.ToString();

            ArrayList diagList = diagMgr.QueryCaseDiagnoseForClinic(drugRecipe.ClinicNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (diagList != null && diagList.Count > 0)
            {
                FS.HISFC.Models.HealthRecord.Diagnose diag = diagList[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                this.lblDiagnose.Text = diag.DiagInfo.ICD10.Name;
            }


            #endregion

            this.ShowData(alData, diagnose, drugRecipe, drugTerminal, hospitalName);

            this.PrintPage();


            return 0;
        }
    }
}
