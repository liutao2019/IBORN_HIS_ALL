using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;

namespace FS.SOC.HISFC.InpatientFee.BizProcess
{
    /// <summary>
    /// [功能描述: 费用调用外部患者入出转逻辑业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class RADT : AbstractBizProcess
    {
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 获取患者信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string inpatientNO)
        {
            this.SetDB(inpatientManager);
            return inpatientManager.QueryPatientInfoByInpatientNO(inpatientNO);
        }

        /// <summary>
        /// 保存变更记录
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="shiftType"></param>
        /// <param name="shiftText"></param>
        /// <param name="oldShift"></param>
        /// <param name="newShift"></param>
        /// <returns></returns>
        public int SaveShiftData(string inpatientNO, FS.HISFC.Models.Base.EnumShiftType shiftType, string shiftText, FS.FrameWork.Models.NeuObject oldShift, FS.FrameWork.Models.NeuObject newShift)
        {
            this.SetDB(inpatientManager);
            return this.inpatientManager.SetShiftData(inpatientNO, shiftType, shiftText, oldShift, newShift, false);
        }

        /// <summary>
        /// 根据母亲流水号查询婴儿
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryBabies(string inpatientNO)
        {
            this.SetDB(inpatientManager);
            return  inpatientManager.QueryBabiesByMother(inpatientNO);
        }

        /// <summary>
        /// 获取住院天数
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public int GetInDays(string inpatientNO)
        {
            FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();
            return radtManager.GetInDays(inpatientNO);
        }

    }

}
