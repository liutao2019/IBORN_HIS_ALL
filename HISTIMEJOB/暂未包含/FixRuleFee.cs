using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.FrameWork.Function;
using System.Collections;
using Neusoft.HISFC.BizProcess.Interface.Common;

namespace HISTIMEJOB
{
    public class FixRuleFee : Neusoft.FrameWork.Management.Database, IJob
    {

        #region 变量
        //患者管理类
        private Neusoft.HISFC.BizLogic.RADT.InPatient radtInpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        private Neusoft.HISFC.BizLogic.Fee.InPatient feeInpatient = new Neusoft.HISFC.BizLogic.Fee.InPatient();
        string messErr = string.Empty;
        private Neusoft.HISFC.BizProcess.Integrate.FeeInterface.InpatientRuleFee inpatientRuleFee = new Neusoft.HISFC.BizProcess.Integrate.FeeInterface.InpatientRuleFee();
        private DateTime feeTime;
        #endregion

        #region 属性
        /// <summary>
        /// 收费时间
        /// </summary>
        public DateTime FeeTime
        {
            get
            {
                return feeTime;
            }
            set
            {
                feeTime = value;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 按规则收取患者费用
        /// </summary>
        /// <returns></returns>
        public int RuleFee()
        {
            ArrayList alAllInpatient = this.radtInpatient.QueryPatient(Neusoft.HISFC.Models.Base.EnumInState.I);

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            inpatientRuleFee.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            foreach (Neusoft.HISFC.Models.RADT.PatientInfo p in alAllInpatient)
            {
                //考虑到有可能某天job没有启动，此处需要修改为收取前几天的，这样当天医嘱收费数量就得有限制，或者不收费的需要更新标记

                DateTime beginTime = NConvert.ToDateTime(feeTime.Date.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:00");
                DateTime endTime = feeTime;

                if (inpatientRuleFee.DoRuleFee(p, new Neusoft.HISFC.Models.Fee.Inpatient.FTSource("200"), beginTime, endTime) < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = p.ID + p.Name + "按规则收取费用失败！" + inpatientRuleFee.Err;
                    WriteErr();
                    return -1;
                }
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            foreach (Neusoft.HISFC.Models.RADT.PatientInfo p in alAllInpatient)
            {
                try
                {
                    this.feeInpatient.UpdateInMainInfoCost(p.ID);
                }
                catch { }
            }


            return 1;
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        public override void WriteErr()
        {
            this.messErr = this.Err;
            base.WriteErr();
        }
        #endregion

        #region IJob 成员

        public string Message
        {
            get
            {
                return messErr;
            }
        }

        public int Start()
        {

            return RuleFee();
        }

        #endregion
    }
}
