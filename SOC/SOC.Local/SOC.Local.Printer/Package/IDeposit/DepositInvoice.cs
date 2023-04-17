using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Local.Printer.Package.IDeposit
{
    public class DepositInvoice : FS.HISFC.BizProcess.Interface.MedicalPackage.IDepositInvoice
    {
        //{E7A07F8A-FD47-43cc-A8D0-B0962CB835FB}
        ucDepositInvoiceIborn ucdepositInvoiceIborn = null;
        ucDepositInvoiceBel ucdepositInvoiceBel = null;

        public int Clear()
        {
            return 1;
        }

        public int Print()
        {
            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;

            //{0DEFA2E1-2454-4695-B7BB-89DE2C06B8C9}
            //if (curDepartment.HospitalID == "IBORN")
            //{
            //    if (ucdepositInvoiceIborn != null)
            //    {
            //        return ucdepositInvoiceIborn.Print();
            //    }
            //}

            if (ucdepositInvoiceIborn != null)
            {
                return ucdepositInvoiceIborn.Print();
            }

            //if (curDepartment.HospitalID == "BELLAIRE")
            //{
            //    if (ucdepositInvoiceBel != null)
            //    {
            //        return ucdepositInvoiceBel.Print();
            //    }
            //}

            return 1;
        }

        public int PrintView()
        {
            return 1;
        }

        public int SetPrintValue(System.Collections.ArrayList invoiceNO)
        {
            if (invoiceNO == null)
            {
                return -1;
            }

            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;


            //{0DEFA2E1-2454-4695-B7BB-89DE2C06B8C9}
            //if (curDepartment.HospitalID == "IBORN")
            //{
            //    ucdepositInvoiceIborn = new ucDepositInvoiceIborn();
            //    ucdepositInvoiceIborn.SetPrintValue(invoiceNO);
            //}

            ucdepositInvoiceIborn = new ucDepositInvoiceIborn();
            ucdepositInvoiceIborn.SetPrintValue(invoiceNO);

            //if (curDepartment.HospitalID == "BELLAIRE")
            //{
            //    ucdepositInvoiceBel = new ucDepositInvoiceBel();
            //    ucdepositInvoiceBel.SetPrintValue(invoiceNO);
            //}

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
    }
}
