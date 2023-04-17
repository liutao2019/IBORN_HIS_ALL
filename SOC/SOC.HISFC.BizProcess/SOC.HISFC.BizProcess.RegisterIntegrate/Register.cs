using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.RegisterIntegrate
{
    /// <summary>
    /// 华南本地挂号管理
    /// </summary>
    public class Register : FS.HISFC.BizProcess.Integrate.IntegrateBase
    {
        FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 按照挂号科室查询一段时间内有效挂号信息
        /// 只查询必要信息：门诊流水号、结算类别、合同单位、姓名、性别、出生日期
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee">0 否；1 是；ALL 全部</param>
        /// <param name="isValid">0 退费；1 有效；2 作废；ALL 全部</param>
        /// <returns></returns>
        public ArrayList QuerySimpleRegByDept(string deptID, DateTime beginDate, DateTime endDate, string isSee, string isValid)
        {
            this.SetDB(regMgr);
            return this.regMgr.QuerySimpleRegByDept(deptID, beginDate, endDate, isSee, isValid);
        }

    }
}
