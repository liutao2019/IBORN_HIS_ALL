using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.PrintOrderBillFactory.GYSY
{
    class PrintOrderBillFactory:FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint
    {


        /// <summary>
        /// PACS申请单打印接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint IPacsReportPrint = null;

        #region IInPatientOrderPrint 成员


        /// <summary>
        /// 错误信息
        /// </summary>
        string err = "";
        public string ErrInfo
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="type"></param>
        /// <param name="deptNeuObject"></param>
        /// <param name="doctNeuObject"></param>
        /// <param name="IList"></param>
        /// <param name="IsReprint"></param>
        /// <returns></returns>
        public int PrintInPatientOrderBill(FS.HISFC.Models.RADT.PatientInfo patientInfo, string type, FS.FrameWork.Models.NeuObject deptNeuObject, FS.FrameWork.Models.NeuObject doctNeuObject, IList<FS.HISFC.Models.Order.Order> IList, bool IsReprint)
        {



            return 1;
        }

        /// <summary>
        /// 实现打印
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="type"></param>
        /// <param name="deptNeuObject"></param>
        /// <param name="doctNeuObject"></param>
        /// <param name="alOrder"></param>
        /// <param name="IsReprint"></param>
        /// <returns></returns>
        public int PrintInPatientOrderBill(FS.HISFC.Models.RADT.PatientInfo patientInfo, string type, FS.FrameWork.Models.NeuObject deptNeuObject, FS.FrameWork.Models.NeuObject doctNeuObject, System.Collections.ArrayList alOrder, bool IsReprint)
        {
            this.ErrInfo = string.Empty;
            switch (type)
            {
                case "检查单":
                    if (!PacsReportPrint(patientInfo, deptNeuObject, doctNeuObject, alOrder)) 
                    {
                        return -1;
                    }
                    break;
            }
            return 1;
        }

        #endregion






        #region 检查申请单打印

        /// <summary>
        /// PACS申请单打印
        /// </summary>
        public bool PacsReportPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList allOrder)
        {

            if (IPacsReportPrint == null)
            {
                this.IPacsReportPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint)) as FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint;
            }

            if (IPacsReportPrint == null)
            {
                this.ErrInfo += "PACS打印接口未实现！请联系信息科！\r\n" + FS.FrameWork.WinForms.Classes.UtilInterface.Err + "\n";
                return false;
            }

            if (IPacsReportPrint.PacsReportPrintForInPatient(patientInfo, reciptDept, reciptDoct, allOrder) == -1)
            {
                this.ErrInfo += IPacsReportPrint.ErrInfo + "\n";
                return false;
            }
            return true;
        }

        #endregion

    }
}
