using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace SOC.Local.PacsInterface.SDFY
{
    /// <summary>
    /// 检查申请单打印工厂类
    /// </summary>
    public class PacsReportPrintFactory : FS.HISFC.BizProcess.Interface.Order.IPacsReportPrint
    {
        public PacsReportPrintFactory()
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
            htPrintBill.Clear();
            foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in alOrder)
            {
                if (orderObj.Item.SysClass.ID.ToString() == "UC" ||
                    (orderObj.Item.SysClass.ID.ToString() == "UZ" 
                    //&& reciptDept.ID == "0409"
                    ))
                {
                    alPacsOrder.Add(orderObj);
                }
            }

            alCons = this.consMgr.GetList("PACSREPORTTYPE");
            consHelper.ArrayObject = alCons;
            if (alCons == null)
            {
                this.errInfo = "常数类型PACSREPORTTYPE没有维护！";
                return -1;
            }
            foreach (FS.FrameWork.Models.NeuObject obj in alCons)
            {
                ArrayList alTemp = new ArrayList();
                foreach (FS.HISFC.Models.Order.OutPatient.Order o in alPacsOrder)
                {
                    if (this.consMgr.GetConstant("REPORTTYPE_ITEM", o.Item.ID).Name == obj.ID)
                    {
                        alTemp.Add(o);
                    }
                }
                if (!htPrintBill.ContainsKey(obj.ID))
                {
                    htPrintBill.Add(obj.ID, alTemp);
                }
            }
            ArrayList alReport = null;
            string strReportType = string.Empty;
            foreach (DictionaryEntry d in htPrintBill)
            {
                alReport = d.Value as ArrayList;
                if (alReport != null && alReport.Count > 0)
                {
                    strReportType = this.consHelper.GetObjectFromID(d.Key.ToString()).Name;
                    if (MessageBox.Show("是否打印" + strReportType + "申请单？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        continue;
                    }
                    switch (d.Key.ToString())
                    {
                        //超声申请单
                        case "1":
                            ucUltrasonicReportPrint uc1 = new ucUltrasonicReportPrint();
                            if (uc1.PacsReportPrintForOutPatient(regObj, reciptDept, reciptDoct, alReport, strReportType) == -1)
                            {
                                this.errInfo = "打印失败！" + uc1.ErrInfo;
                                return -1;
                            }
                            break;
                        //心电图申请单
                        case "2":
                            ucECGReportPrint uc2 = new ucECGReportPrint();
                            if (uc2.PacsReportPrintForOutPatient(regObj, reciptDept, reciptDoct, alReport, strReportType) == -1)
                            {
                                this.errInfo = "打印失败！" + uc2.ErrInfo;
                                return -1;
                            }
                            break;
                        //病理申请单
                        case "3":
                            ucPathologyReportPrint uc3 = new ucPathologyReportPrint();
                            if (uc3.PacsReportPrintForOutPatient(regObj, reciptDept, reciptDoct, alReport, strReportType) == -1)
                            {
                                this.errInfo = "打印失败！" + uc3.ErrInfo;
                                return -1;
                            }
                            break;
                        //X光申请单
                        case "4":
                            ucXRayReportPrint uc4 = new ucXRayReportPrint();
                            if (uc4.PacsReportPrintForOutPatient(regObj, reciptDept, reciptDoct, alReport, strReportType) == -1)
                            {
                                this.errInfo = "打印失败！" + uc4.ErrInfo;
                                return -1;
                            }
                            break;
                        //CT申请单
                        case "5":
                            ucCTReportPrint uc5 = new ucCTReportPrint();
                            if (uc5.PacsReportPrintForOutPatient(regObj, reciptDept, reciptDoct, alReport, strReportType) == -1)
                            {
                                this.errInfo = "打印失败！" + uc5.ErrInfo;
                                return -1;
                            }
                            break;
                        //男科治疗处理单
                        case "6":
                            ucTreatmentReportPrint uc6 = new ucTreatmentReportPrint();
                            if (uc6.PacsReportPrintForOutPatient(regObj, reciptDept, reciptDoct, alReport, strReportType) == -1)
                            {
                                this.errInfo = "打印失败！" + uc6.ErrInfo;
                                return -1;
                            }
                            break;
                        //男科检查申请单
                        case "8":
                            ucAndrologyCheckReportPrint uc8 = new ucAndrologyCheckReportPrint();
                            if (uc8.PacsReportPrintForOutPatient(regObj, reciptDept, reciptDoct, alReport, strReportType) == -1)
                            {
                                this.errInfo = "打印失败！" + uc8.ErrInfo;
                                return -1;
                            }
                            break;
                        //其他申请单
                        default:
                            ucPacsReportPrint ucDefault = new ucPacsReportPrint();
                            if (ucDefault.PacsReportPrintForOutPatient(regObj, reciptDept, reciptDoct, alReport, strReportType) == -1)
                            {
                                this.errInfo = "打印失败！" + ucDefault.ErrInfo;
                                return -1;
                            }
                            break;
                    }
                }
            }

            return 1;
        }

        #endregion
    }
}
