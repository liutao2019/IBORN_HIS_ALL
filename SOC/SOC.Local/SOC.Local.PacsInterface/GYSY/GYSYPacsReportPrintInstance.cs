using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace SOC.Local.PacsInterface.GYSY
{
    /// <summary>
    /// 检查申请单打印工厂类
    /// </summary>
    public class GYSYPacsReportPrintInstance : FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint
    {
        public GYSYPacsReportPrintInstance()
        {
        }

        #region 变量

        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = string.Empty;

        /// <summary>
        /// 单据类型集合
        /// </summary>
        ArrayList alCons = new ArrayList();

        /// <summary>
        /// 常数业务类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 非药品项目业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 打印单据集合
        /// </summary>
        Hashtable htPrintBill = new Hashtable();

        /// <summary>
        /// 帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper consHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        #region IPacsReportPrint 成员

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
        /// 住院检查申请单打印
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alPacsOrder"></param>
        /// <returns></returns>
        public int PacsReportPrintForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            ArrayList alPacsOrder = new ArrayList();

            foreach (FS.HISFC.Models.Order.Order orderObj in alOrder)
            {
                if (orderObj.Item.SysClass.ID.ToString() == "UC")
                {
                    alPacsOrder.Add(orderObj);
                }
            }

            if (alPacsOrder.Count <= 0) //没有选择项目信息
            {
                this.errInfo = "请选择开立的检查信息!";
                return -1;
            }

            SOC.Local.PacsInterface.GYSY.InPaitent.frmPacsApply f = new SOC.Local.PacsInterface.GYSY.InPaitent.frmPacsApply(alPacsOrder, patientInfo);
            f.ShowDialog();
            return 1;
        }

        /// <summary>
        /// 门诊检查申请单打印
        /// </summary>
        /// <param name="regObj">患者挂号信息</param>
        /// <param name="reciptDept">开单科室</param>
        /// <param name="reciptDoct">开单医生</param>
        /// <param name="alPacsOrder">医嘱信息</param>
        /// <returns></returns>
        public int PacsReportPrintForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            ArrayList alPacsOrder = new ArrayList();

            foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
            {
                if (orderObj.Item.SysClass.ID.ToString() == "UC" && (orderObj.Status == 0 || orderObj.Status == 1))
                {
                    alPacsOrder.Add(orderObj);
                }
            }

            if (alPacsOrder.Count <= 0) //没有选择项目信息
            {
                this.errInfo = "请选择开立的检查信息!";
                return -1;
            }

            SOC.Local.PacsInterface.GYSY.frmPacsApplyForClinic f = new SOC.Local.PacsInterface.GYSY.frmPacsApplyForClinic(alPacsOrder, regObj);
            f.ShowDialog();
            return 1;
        }

        #endregion
    }
}
