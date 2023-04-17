using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.OrderInterface.Common;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.IMedicalResult
{
    /// <summary>
    /// 中大五院医疗结果查询
    /// 整合所有的医疗结果：检验、PACS、心电、超声、内镜、院感等
    /// </summary>
    public class MedicalResult : FS.SOC.HISFC.BizProcess.OrderInterface.Common.IMedicalResult
    {
        #region IMedicalResult 成员

        string errInfo = "";

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

        EnumResultType resultType = EnumResultType.LIS;

        public EnumResultType ResultType
        {
            get
            {
                return resultType;
            }
            set
            {
                resultType = value;
            }
        }

        public int ShowResult(FS.HISFC.Models.RADT.Patient patient, ArrayList alOrder)
        {
            switch(resultType)
            {
                case EnumResultType.LIS:
                    return ShowLisResult(patient, alOrder);
                    break;
                default:
                    break;
            }
            return 1;
        }

        #endregion


        private LIS.frmLisResultShow lisResultShow = null;

        private int ShowLisResult(FS.HISFC.Models.RADT.Patient patient, ArrayList alOrder)
        {
            if (lisResultShow == null || lisResultShow.IsDisposed)
            {
                lisResultShow = new LIS.frmLisResultShow();
            }

            if (lisResultShow.SetPatient(patient) == -1)
            {
                this.errInfo = lisResultShow.ErrMsg;
                return -1;
            }
            if (lisResultShow.ShowResultByPatient() == -1)
            {
                this.errInfo = lisResultShow.ErrMsg;
                return -1;
            }
            lisResultShow.Show();

            return 1;
        }
    }
}
