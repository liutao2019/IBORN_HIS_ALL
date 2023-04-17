using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    public partial class ucQuitDrugBill : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeApplyPrint
    {
        public ucQuitDrugBill()
        {
            InitializeComponent();
        }
        FS.HISFC.Models.RADT.PatientInfo patient;
        #region IBackFeeRecipePrint 成员

        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                patient = value;
            }

        }

        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 860, 400));
            FS.HISFC.BizLogic.Manager.PageSize pageSet = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize ps = pageSet.GetPageSize("QuitDrugBill");
            if (ps == null)
            {
                //默认大小
                ps = new FS.HISFC.Models.Base.PageSize("PrepayPrint", 830, 450);
            }
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.SetPageSize(ps);
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(ps.Left, ps.Top, this);
            }
            else
            {
                print.PrintPage(ps.Left, ps.Top, this);
            }
        }

        public int SetData(System.Collections.ArrayList alBackFee)
        {
            //FS.HISFC.BizLogic.Registration.Register registerIntegrate = new FS.HISFC.BizLogic.Registration.Register();

            //patient = registerIntegrate.GetByClinic(this.patient.ID);
            this.lbName.Text = "姓名：" + patient.Name;
            this.lbCardNo.Text = "住院号：" + patient.PID.PatientNO;
            this.lbSex.Text = "性别：" + patient.Sex.Name;
            this.lbDeptName.Text = "科室：" + patient.PVisit.PatientLocation.Dept.Name;
            this.labArea.Text += patient.PVisit.PatientLocation.NurseCell;   //病区
            this.labPrintDate.Text += DateTime.Now.ToString();  //打印时间

            decimal sum = 0;
            this.neuSpread1_Sheet1.Rows.Count = 0;

            this.neuSpread1_Sheet1.Rows.Add(0, alBackFee.Count + 1);



            for (int i = 0; i < alBackFee.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = (FS.HISFC.Models.Fee.Inpatient.FeeItemList)alBackFee[i];


                this.neuSpread1_Sheet1.Cells[i, 0].Text = feeItemList.Item.Name.ToString();
                if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = feeItemList.Item.Specs.ToString();
                }
                if (feeItemList.Order.DoseUnit == feeItemList.Item.PriceUnit)
                {
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = feeItemList.Item.Qty.ToString();
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, 2].Text = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Qty / feeItemList.Item.PackQty, 2).ToString();
                }
                this.neuSpread1_Sheet1.Cells[i, 3].Text = feeItemList.Item.PriceUnit;
                this.neuSpread1_Sheet1.Cells[i, 4].Text = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Qty / feeItemList.Item.PackQty * feeItemList.Item.Price, 2).ToString();
                sum += FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Qty / feeItemList.Item.PackQty * feeItemList.Item.Price, 2);
            }
            this.neuSpread1_Sheet1.Cells[alBackFee.Count, 0].Text = "合计：";
            this.neuSpread1_Sheet1.Cells[alBackFee.Count, 4].Text = sum.ToString();
            return 1;
        }

        #endregion
    }
}
