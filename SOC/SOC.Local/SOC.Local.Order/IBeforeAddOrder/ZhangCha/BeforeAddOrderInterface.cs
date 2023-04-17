using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.IBeforeAddOrder.ZhangCha
{
    /// <summary>
    /// 开立医嘱（处方）前操作接口
    /// 1、不允许给自己开立处方
    /// </summary>
    public class BeforeAddOrderInterface : FS.HISFC.BizProcess.Interface.Order.IBeforeAddOrder
    {
        #region IBeforeAddOrder 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
            set
            {
                errInfo = value;
            }
        }

        /// <summary>
        /// 住院
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnBeforeAddOrderForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct)
        {
            return 1;
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        Hashtable hsPatientInfo = new Hashtable();

        /// <summary>
        /// 操作员
        /// </summary>
        FS.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// 门诊
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnBeforeAddOrderForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct)
        {
            /* 处方管理办法：
             * 医师取得麻醉药品和第一类精神药品处方权后，方可在本机构开具麻醉药品和第一类精神药品处方，
             * 但不得为自己开具该类药品处方。
             * */

            //按照张槎的要求，所有药都不能给自己开立
            try
            {
                FS.HISFC.Models.RADT.Patient patient = null;
                if (oper == null)
                {
                    FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    oper = inteMgr.GetEmployeeInfo(FS.FrameWork.Management.Connection.Operator.ID);
                }

                if (patient == null || !hsPatientInfo.Contains(patient.PID.CardNO))
                {
                    FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
                    patient = inpatientMgr.QueryComPatientInfo(regObj.PID.CardNO);
                }
                else
                {
                    patient = hsPatientInfo[patient.PID.CardNO] as FS.HISFC.Models.RADT.Patient;
                }

                if (patient != null && oper != null)
                {
                    if (patient.IDCard.Trim() == oper.IDCard.Trim()) 
                    {
                        errInfo = "您不能给自己开立处方，如有疑问请联系医务科！\r\n\r\n原因：您和患者的身份证号相同！";
                        //MessageBox.Show("您不能给自己开立处方，如有疑问请联系医务科！\r\n\r\n原因：您和患者的身份证号相同！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return 1;
        }

        #endregion
    }
}
