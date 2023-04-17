using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GJLocal.HISFC.Components.OpGuide.OutPatient.Classes
{
    public class LocalManager : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获取一天的看诊记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ArrayList GetRegSeeList(string cardNo,string clinicCode, DateTime beginDate)
        {
            ArrayList alReg = new ArrayList();

            string sql = @"select r.reg_date,--挂号时间
                                   r.see_dpcd,
                                   r.see_docd,
                                   r.see_date
                        from fin_opr_register r
                        where r.card_no='{0}'
                        and r.clinic_code!='{1}'
                        and r.ynsee='1'
                        and r.reg_date>to_date('{2}','yyyy-mm-dd hh24:mi:ss')";

            try
            {
                sql = string.Format(sql, cardNo, clinicCode, beginDate.ToString());
                this.ExecQuery(sql);

                while (Reader.Read())
                {
                    FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();

                    regObj.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[0]);
                    regObj.SeeDoct.Dept.ID = Reader[1].ToString();
                    regObj.SeeDoct.Dept.Name = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(regObj.SeeDoct.Dept.ID);
                    regObj.SeeDoct.ID = Reader[2].ToString();
                    regObj.SeeDoct.Name = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(regObj.SeeDoct.ID);
                    regObj.SeeDoct.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[3]);

                    alReg.Add(regObj);
                }
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return null;
            }

            return alReg;
        }
    }
}
