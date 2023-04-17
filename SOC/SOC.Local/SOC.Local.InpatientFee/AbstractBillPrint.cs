using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.InpatientFee.Interface;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee
{
    public abstract class AbstractBillPrint<TObject> : IBillPrint
    {
        protected string errorInfo = string.Empty;
        protected FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();

        #region 抽象方法

        public abstract int SetData(FS.HISFC.Models.RADT.PatientInfo patientInfo, EnumPrintType printType, TObject t, params object[] appendParams);

        public abstract FS.HISFC.Models.Base.PageSize GetPageSize();

        public abstract Control[] GetPrintControls();

        #endregion

        #region IBillPrint 成员

        public int SetData(FS.HISFC.Models.RADT.PatientInfo patientInfo, object t, ref string errInfo, params object[] appendParams)
        {
            return this.SetData(patientInfo, EnumPrintType.Normal, t, ref errInfo, appendParams);
        }

        public int SetData(FS.HISFC.Models.RADT.PatientInfo patientInfo, EnumPrintType printType, object t, ref string errInfo, params object[] appendParams)
        {
            TObject tObject = default(TObject);
            if (t is TObject)
            {
                tObject = (TObject)t;
            }

            int i = this.SetData(patientInfo, printType, tObject, appendParams);
            errInfo = this.errorInfo;

            return i;
        }

        public virtual void Print()
        {
            FS.HISFC.Models.Base.PageSize pageSize = this.GetPageSize();
            //使用FS默认打印方式
            if (pageSize == null)
            {
                pageSize = new FS.HISFC.Models.Base.PageSize();//使用默认的A4纸张
            }
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(pageSize);
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (employee != null && employee.IsManager)
            {
                print.PrintPreview(pageSize.Left, pageSize.Top, this.GetPrintControls());
            }
            else
            {
                print.PrintPage(pageSize.Left, pageSize.Top, this.GetPrintControls());
            }
        }

        #endregion
    }
}
