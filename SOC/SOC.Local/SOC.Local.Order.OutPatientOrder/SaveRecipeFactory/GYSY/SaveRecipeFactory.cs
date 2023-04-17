using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.SaveRecipeFactory.GYSY
{
    /// <summary>
    /// 医嘱保存后工厂类
    /// </summary>
    class SaveRecipeFactory : Neusoft.HISFC.BizProcess.Interface.Order.ISaveOrder
    {

        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = string.Empty;

        /// <summary>
        /// 实现门诊院注单打印接口
        /// </summary>
        private Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCInjectBillPrint = null;

        /// <summary>
        /// 实现门诊诊疗记录打印接口
        /// </summary>
        private Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCMedicalRecordPrint = null;

        
        
        #region ISaveOrder 成员
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
        /// 实现住院医嘱保存完的后续操作
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
        /// 实现门诊处方保存完的后续操作
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnSaveOrderForOutPatient(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            //转换为Ilist,ArrayList效率低下，不安全应该摒弃
            IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IOrderList = alOrder.Cast<Neusoft.HISFC.Models.Order.OutPatient.Order>().ToList();


            #region 实现门诊处方打印





            #endregion


            #region 实现院注射单接口
            if (object.Equals(ISOCInjectBillPrint, null))
            {
                ISOCInjectBillPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.SaveOrderFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 1) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }

            if (!object.Equals(ISOCInjectBillPrint, null))
            {
                ISOCInjectBillPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alOrder);
            }
            #endregion


            #region 实现门诊诊疗记录
            if (object.Equals(ISOCMedicalRecordPrint, null))
            {
                ISOCMedicalRecordPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.SaveOrderFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 2) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }

            if (!object.Equals(ISOCMedicalRecordPrint, null))
            {
                ISOCMedicalRecordPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, IOrderList);
            }
            #endregion
            return 1;
        }

        #endregion
    }
}
