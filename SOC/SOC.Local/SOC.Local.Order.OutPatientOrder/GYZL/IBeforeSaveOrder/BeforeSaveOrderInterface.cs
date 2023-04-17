using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.IBeforeSaveOrder
{
    /// <summary>
    /// 保存医嘱（处方）前操作接口
    /// 1、限制检验不需录入样本类型
    /// </summary>
    class BeforeSaveOrderInterface : FS.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder
    {
        #region IBeforeSaveOrder 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        private string errInfo = "";

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
        public int BeforeSaveOrderForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            return 1;
        }

        /// <summary>
        /// 门诊
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int BeforeSaveOrderForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            int rev = 1;
            string errItem = "";
            foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in alOrder)
            {
                if (outOrder.Item.SysClass.ID.ToString() == "UL")
                {
                    if (string.IsNullOrEmpty(outOrder.Sample.Name))
                    {
                        //errInfo = "项目【" + outOrder.Item.Name + "】送检样本为空，请重新录入！";
                        //return -1;
                        errItem += "\r\n" + outOrder.Item.Name;
                        rev = -1;
                    }
                }
            }
            if (rev == -1)
            {
                errInfo = "以下项目送检样本为空，请录入后继续！\r\n";
            }

            return rev;
        }

        #endregion
    }
}
