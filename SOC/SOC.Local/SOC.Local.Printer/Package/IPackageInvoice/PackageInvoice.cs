using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Local.Printer.Package.IPackageInvoice
{
    public class PackageInvoice:FS.HISFC.BizProcess.Interface.MedicalPackage.IPackageInvoice
    {
        //{E7A07F8A-FD47-43cc-A8D0-B0962CB835FB}
        ucPackageInvoiceIborn ucpackageInvoiceIborn = null;
        ucPackageInvoiceBel ucpackageInvoiceBel = null;

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
            //    if (ucpackageInvoiceIborn != null)
            //    {
            //        return ucpackageInvoiceIborn.Print();
            //    }
            //}

            //if (curDepartment.HospitalID == "BELLAIRE")
            //{
            //    if (ucpackageInvoiceBel != null)
            //    {
            //        return ucpackageInvoiceBel.Print();
            //    }
            //}

            if (ucpackageInvoiceIborn != null)
            {
                return ucpackageInvoiceIborn.Print();
            }

            return 1;
        }

        public int PrintView()
        {
            return 1;
        }

        public int SetPrintValue(string invoiceNO)
        {
            if (string.IsNullOrEmpty(invoiceNO))
            {
                return -1;
            }

            FS.HISFC.Models.Base.Employee curEmployee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department curDepartment = curEmployee.Dept as FS.HISFC.Models.Base.Department;

            //{0DEFA2E1-2454-4695-B7BB-89DE2C06B8C9}
            //if (curDepartment.HospitalID == "IBORN")
            //{
            //    ucpackageInvoiceIborn = new ucPackageInvoiceIborn();
            //    ucpackageInvoiceIborn.SetPrintValue(invoiceNO);
            //}

            //if (curDepartment.HospitalID == "BELLAIRE")
            //{
            //    ucpackageInvoiceBel = new ucPackageInvoiceBel();
            //    ucpackageInvoiceBel.SetPrintValue(invoiceNO);
            //}

            ucpackageInvoiceIborn = new ucPackageInvoiceIborn();
            ucpackageInvoiceIborn.SetPrintValue(invoiceNO);

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
