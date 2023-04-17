using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Registration.ZhuHai.Zdwy.IRegPrint
{
    public class RegPrint : FS.HISFC.BizProcess.Interface.Registration.IRegPrint
    {
        //{82AC7ED8-CF2C-4662-974B-1F0CEE43E5FD}
        ucRegInvoicePrint ucRegPrint = null;
        ucRegInvoicePrintBel ucRegPrintBel = null;

        #region IRegPrint 成员

        public int Clear()
        {
            return 1;
        }

        public int Print()
        {
            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;

            if (curDepartment.HospitalID == "IBORN")
            {
                if (ucRegPrint != null)
                {
                    return ucRegPrint.Print();
                }
            }

            if (curDepartment.HospitalID == "BELLAIRE")
            {
                if (ucRegPrintBel != null)
                {
                    return ucRegPrintBel.Print();
                }
            }

            return 1;
        }

        public int PrintView()
        {
            return 1;
        }

        public int SetPrintValue(FS.HISFC.Models.Registration.Register register)
        {
            

            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;

            if (curDepartment.HospitalID == "IBORN")
            {
                if (register.PrintInvoiceCnt == 0 || register.PrintInvoiceCnt == 1)
                {
                    ucRegPrint = new ucRegInvoicePrint();
                    ucRegPrint.SetPrintValue(register);
                }
            }

            if (curDepartment.HospitalID == "BELLAIRE")
            {
                if (register.PrintInvoiceCnt == 0 || register.PrintInvoiceCnt == 1)
                {
                    ucRegPrintBel = new ucRegInvoicePrintBel();
                    ucRegPrintBel.SetPrintValue(register);
                }
            }

            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction trans)
        {
        }

        public System.Data.IDbTransaction Trans
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
