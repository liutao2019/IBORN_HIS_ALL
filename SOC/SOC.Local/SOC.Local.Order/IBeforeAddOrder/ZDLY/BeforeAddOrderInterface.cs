using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.IBeforeAddOrder.ZDLY
{
    /// <summary>
    /// 开立医嘱（处方）前操作接口
    /// 1、不同合同单位的给出提示
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

        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

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
                if (regObj.IsSee)
                {
                    return 1;
                }
                string reglevlCode = "";
                string regDiagItemCode = "";
                
                DateTime sysTime=regIntegrate.GetDateTimeFromSysDateTime();
                
                FS.HISFC.Models.Base.Employee doct = FS.SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(reciptDoct.ID);


                FS.HISFC.Models.Registration.Schema schema = regIntegrate.GetSchema(reciptDoct.ID, sysTime);

                if (schema == null)
                {
                    if (regIntegrate.GetSupplyRegInfo(reciptDoct.ID, doct.Level.ID, reciptDept.ID, ref reglevlCode, ref regDiagItemCode) == -1)
                    {
                        errInfo = regIntegrate.Err;
                        return -1;
                    }

                }
                else 
                {
                    reglevlCode = schema.Templet.RegLevel.ID;
                }

                FS.HISFC.Models.Registration.RegLevel doctRegLevl = FS.SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(reglevlCode);
                if (regObj.DoctorInfo.Templet.RegLevel.ID != doctRegLevl.ID)
                {
                    if (MessageBox.Show("您看诊的挂号级别为:【" + doctRegLevl.Name + "】，\r\n而患者的挂号级别为：【" + regObj.DoctorInfo.Templet.RegLevel.Name + "】\r\n\r\n是否继续看诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        errInfo = "您已经取消操作！";
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
