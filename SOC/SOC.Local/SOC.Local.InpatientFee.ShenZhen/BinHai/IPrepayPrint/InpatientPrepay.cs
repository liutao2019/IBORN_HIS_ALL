using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.IPrepayPrint
{
    public partial class InpatientPrepay : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.FeeInterface.IPrepayPrint
    {
        public InpatientPrepay()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();

        #region IPrepayPrint 成员


        ////{014680EC-6381-408b-98FB-A549DAA49B82}
        // 摘要:
        //     设置押金发票打印参数
        //
        // 参数:
        //   patient:
        //     住院患者基本信息实体
        //
        //   alPrepay:
        //     预交金打印实体
        //
        // 返回结果:
        //     成功 1 失败 -1
        public int SetValue(FS.HISFC.Models.RADT.PatientInfo patient, System.Collections.ArrayList alPrepay)
        {
            throw new NotImplementedException();
        }

        public int Clear()
        {
            return 1;
        }

        public int Print()
        {

            FS.HISFC.Models.Base.PageSize pageSize = pageSizeManager.GetPageSize("ZYYJ");
            //使用FS默认打印方式
            if (pageSize == null)
            {
               // pageSize = new FS.HISFC.Models.Base.PageSize("ZYYJ", 550, 365);//使用默认的A4纸张
                pageSize = new FS.HISFC.Models.Base.PageSize("ZYYJ", 491, 365);//使用默认的A4纸张
            }
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(pageSize);
            //print.IsLandScape = true;
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (employee != null && employee.IsManager)
            {
                print.PrintPreview(pageSize.Left, pageSize.Top, this);
            }
            else
            {
                print.PrintPage(pageSize.Left, pageSize.Top, this);
            }
          
            return 1;
        }

        public void SetTrans(IDbTransaction trans)
        {
            return;
        }

        public int SetValue(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.Prepay prepay)
        {
            this.lblBetNo.Text = "床号:" + patient.PVisit.PatientLocation.Bed.ToString();
            this.lblInpatientName.Text = "姓名:" + patient.Name;
            this.lblInpatientNo.Text = "住院号:" + patient.PID.ID;
            this.lblOper.Text = prepay.PrepayOper.ID.ToString();
            this.lblOperTime.Text = prepay.PrepayOper.OperTime.ToString();
            this.lblPrepay.Text = prepay.FT.PrepayCost.ToString();
            this.lblPrepayCaptal.Text = FS.FrameWork.Function.NConvert.ToCapital(prepay.FT.PrepayCost);
            this.lblInvoiceNO.Text = "NO." + prepay.RecipeNO.ToString();
            this.lblroomno.Text = "病区:" + patient.PVisit.PatientLocation.Dept.Name.ToString();
            FS.HISFC.BizLogic.Manager.Department departmentManager = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.Models.Base.Department deptObj = departmentManager.GetDeptmentById(patient.PVisit.PatientLocation.Dept.ID);
            if (deptObj != null)
            {
                this.lblroomno.Text += deptObj.EnglishName == null ? "" : deptObj.EnglishName;
            }

            if (string.IsNullOrEmpty(prepay.PayType.Name))
            {
                prepay.PayType.Name = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.GetConstantName(FS.HISFC.Models.Base.EnumConstant.PAYMODES, prepay.PayType.ID);
            }
            this.lbprepaytype.Text = "(" + prepay.PayType.Name + ")";

            return 1;
        }

        public IDbTransaction Trans
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        #endregion
    }
}
