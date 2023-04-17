using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.SDFY.IAfterQueryRegList
{
    /// <summary>
    /// 用于门诊医生站查询挂号列表、自助挂号的操作接口
    /// </summary>
    class AfterQueryRegListInterface:FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList
    {

        #region IAfterQueryRegList 成员

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


        public int OnAfterQueryRegList( System.Collections.ArrayList alReginfo, FS.FrameWork.Models.NeuObject dept)
        {
            return 1;
            try
            {
                FS.HISFC.Models.Registration.Register regObj = null;
                ArrayList alTemp = new ArrayList();
                FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();
                while (alReginfo != null && alReginfo.Count > 0)
                {
                    regObj = alReginfo[0] as FS.HISFC.Models.Registration.Register;
                    if ("2,3".Contains(regObj.Pact.ID.Trim()) &&
                        regObj.DoctorInfo.SeeDate.Date != conManager.GetDateTimeFromSysDateTime().Date)
                    {
                        alReginfo.Remove(regObj);
                    }
                    else
                    {
                        alTemp.Add(regObj);
                        alReginfo.Remove(regObj);
                    }
                }
                alReginfo = alTemp;
            }
            catch
            {
            }
            return 1;
        }

        /// <summary>
        /// 医生站自助挂号后操作
        /// </summary>
        /// <param name="regInfo"></param>
        /// <returns></returns>
        public int OnConfirmRegInfo(FS.HISFC.Models.Registration.Register regInfo)
        {
            if (regInfo == null || regInfo.Pact == null)
            {
                return 1;
            }

            if (string.IsNullOrEmpty(regInfo.Pact.ID))
            {
                errInfo = "合同单位为空，请选择合同单位！";
                return -1;
            }
            if ("2,3".Contains(regInfo.Pact.ID))
            {
                errInfo = "顺德医保患者请到挂号处挂号后，再到医生处看诊！";
                return -1;
            }

            return 1;
        }

        #endregion
    }
}
