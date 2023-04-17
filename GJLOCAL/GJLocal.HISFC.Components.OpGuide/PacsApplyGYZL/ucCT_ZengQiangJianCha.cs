using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.PacsApplyGYZL
{
    public partial class ucCT_ZengQiangJianCha : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {
        public ucCT_ZengQiangJianCha()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

         private void Clear()
        {
            lblName.Text = "";
            //lblAddress.Text = "";
            lblAge.Text = "";
            lblBedno.Text = "";
            lblDept.Text = "";
            //lblPhone.Text = "";
            lblSex.Text = "";
            //lblDate.Text = "";
            lblTime.Text = "";
        }

         #region IOutPatientOrderPrint 成员

         public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
         {
             this.Clear();

             this.SetPatientInfo(regObj);
             this.SetPrintValue(orderList, isPreview);

             return 1;
         }

         public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
         {
             return 1;
         }

         public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
         {
         }

         public void SetPage(string pageStr)
         {
         }

         #endregion

         /// <summary>
         /// 设置患者基本信息
         /// </summary>
         /// <param name="register"></param>
         public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
         {
             if (register == null) return;

             this.lblName.Text = register.Name;
             this.lblAge.Text = this.diagManager.GetAge(register.Birthday, false);
             this.lblSex.Text = register.Sex.Name;
             //lblDate.Text = diagManager.GetDateTimeFromSysDateTime().ToString("yyyy.MM.dd");
             //this.lblDate.Text = DateTime.Now.ToString.ToString("yyyy 年 MM 月 dd 日");
         }

         /// <summary>
         /// 设置打印
         /// </summary>
         /// <param name="IList"></param>
         public void SetPrintValue(IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
         {
             if (orderList == null || orderList.Count <= 0)
             {
                 return;
             }

             this.lblDept.Text = orderList[0].ReciptDept.Name;

             if (!isPreview)
             {
                 this.PrintPage();
             }
         }

         /// <summary>
         /// 打印
         /// </summary>
         private void PrintPage()
         {
             FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
             print.SetPageSize(FS.SOC.Local.Order.ZhuHai.ZDWY.Function.GetPrintPage(false));
             print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
             print.IsDataAutoExtend = false;
             //print.IsLandScape = true;
             if (FS.SOC.Local.Order.ZhuHai.ZDWY.Function.IsPreview())
             {
                 print.PrintPreview(5, 5, this);
             }
             else
             {
                 print.PrintPage(5, 5, this);
             }
         }

    }
}
