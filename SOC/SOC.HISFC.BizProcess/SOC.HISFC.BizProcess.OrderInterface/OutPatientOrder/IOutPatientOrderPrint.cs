using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Order.OutPatient;
using System.Windows.Forms;

namespace FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutPatientOrderPrint
    {
        #region 实现该接口的类有 通过index区分
        //1.FS.SOC.Local.Order.OutPatientOrder.InjectBillPrint.ucOrderInjectBill 院注单打印
        //2.FS.SOC.Local.Order.OutPatientOrder.MedicalRecord.ucMedicalRecord     门诊诊疗记录打印




        #endregion

        /// <summary>
        /// 门诊医生站本地化单据打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview);








        /// <summary>
        /// 门诊医生站本地化单据打印<泛型>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IList"></param>
        /// <returns></returns>
        int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview);



        void PreviewOutPatientOrderBill(Register regObj, NeuObject reciptDept, NeuObject reciptDoct, IList<Order> List, bool isPreview);

        /// <summary>
        /// 设置页码
        /// </summary>
        /// <param name="pageStr"></param>
        void SetPage(string pageStr);
    }
}
