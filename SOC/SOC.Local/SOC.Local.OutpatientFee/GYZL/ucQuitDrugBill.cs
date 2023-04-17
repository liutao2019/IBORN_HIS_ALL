using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.GYZL
{
    public partial class ucQuitDrugBill : UserControl, FS.HISFC.BizProcess.Interface.FeeInterface.IBackFeeRecipePrint
    {
        public ucQuitDrugBill()
        {
            InitializeComponent();
        }
        FS.HISFC.Models.Registration.Register patient;
        #region IBackFeeRecipePrint 成员

        public FS.HISFC.Models.Registration.Register Patient
        {
            set {
                patient=value;
            }
            
        }

        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("OutPatientDrugBill", 860, 400));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            print.PrintPreview(5, 5, this);
        }

        public int SetData(System.Collections.ArrayList alBackFee)
        {
                FS.HISFC.BizLogic.Registration.Register registerIntegrate = new FS.HISFC.BizLogic.Registration.Register();
              
                patient = registerIntegrate.GetByClinic(this.patient.ID);
                this.lbName.Text = "姓名：" + patient.Name;
                this.lbCardNo.Text = "病历号：" + patient.PID.CardNO;
                this.lbSex.Text = "性别：" + patient.Sex.Name;                
                this.lbDeptName.Text = "科室：" + patient.DoctorInfo.Templet.Dept.Name;                
                //this.lbInvoice.Text = "发票号：" + patient.InvoiceNO;
                decimal sum = 0;
                for (int i=0;i<alBackFee.Count;i++)
                {  
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = (FS.HISFC.Models.Fee.Outpatient.FeeItemList)alBackFee[i];
                    
                    //this.lbCardNo.Text = "病历号：" + feeItemList.Patient.PID.CardNO;
                    //this.lbDeptName.Text = "科室：" + ((Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID;
                    this.lbInvoice.Text = "发票号：" + feeItemList.Invoice.ID;

                    this.neuSpread1_Sheet1.Cells[i, 0].Text = feeItemList.Item.Name.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = feeItemList.Item.Specs.ToString();
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
