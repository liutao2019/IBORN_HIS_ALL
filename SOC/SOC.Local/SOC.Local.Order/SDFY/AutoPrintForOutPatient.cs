using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Local.Order.SDFY
{
    public class AutoPrintForOutPatient : Neusoft.HISFC.BizProcess.Interface.Order.ISaveOrder
    {
        public AutoPrintForOutPatient()
        {
            this.ILisReportPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint), 0) as Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint;
            this.IPacsReportPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint), 1) as Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint;
        }

        /// <summary>
        /// 检验申请单打印
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint ILisReportPrint = null;
        /// <summary>
        /// 检查申请单打印
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint IPacsReportPrint = null;
        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = string.Empty;

        #region ISaveOrder 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return this.errInfo;
            }
            set
            {
                this.errInfo = value;
            }
        }

        /// <summary>
        /// 住院医嘱保存后自动打印
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnSaveOrderForInPatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            return 1;
        }

        /// <summary>
        /// 门诊医嘱保存后自动打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnSaveOrderForOutPatient(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            #region 检验申请单打印
            if (ILisReportPrint == null)
            {
                ILisReportPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint), 0) as Neusoft.HISFC.BizProcess.Interface.Order.ILisReportPrint;
            }
            if (ILisReportPrint == null)
            {
                this.errInfo = "检验申请单打印接口初始化失败！";
                return -1;
            }
            if (ILisReportPrint.LisReportPrintForOutPatient(regObj, reciptDept, reciptDoct, alOrder) == -1)
            {
                return -1;
            }
            #endregion

            #region 检查申请单打印
            if (IPacsReportPrint == null)
            {
                IPacsReportPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint), 1) as Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint;
            }
            if (IPacsReportPrint == null)
            {
                this.errInfo = "检查申请单打印接口初始化失败！";
                return -1;
            }
            if (IPacsReportPrint.PacsReportPrintForOutPatient(regObj, reciptDept, reciptDoct, alOrder) == -1)
            {
                return -1;
            }
            #endregion

            return 1;
        }

        #endregion
    }
}
