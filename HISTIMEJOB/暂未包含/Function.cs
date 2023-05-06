using System;
using System.Collections;
//using Neusoft.FrameWork.Function;

namespace HISTIMEJOB
{
    /// <summary>
    /// 功能管理类
    /// </summary>
    public class Function : Neusoft.FrameWork.Management.Database
    {
        /// <summary>
        ///  功能管理类
        /// </summary>
        public Function()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 医保上传处理
        /// <summary>
        /// 按门诊流水号查询门诊特点医保有效的结算号
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public ArrayList GetYBMEDNOByClinic(string clinicNo)
        {
            ArrayList al = null;
            string sql = string.Empty;
            if (this.Sql.GetSql("Registration.GetYBMEDNOByClinic", ref sql) == -1)
            {
                sql = @"select pact_code,pact_name from fin_ipr_siinmaininfo  
where inpatient_no='{0}'  and type_code='1' and valid_flag='1'  and ybmedno ='0'";
            }
            try
            {
                sql = string.Format(sql, clinicNo);
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
            }

            if (this.ExecQuery(sql) < 0)
            {
                this.Err = "Execute Err;";
                return null;
            }

            while (this.Reader.Read())
            {
                al = new ArrayList();
                Neusoft.HISFC.Models.Base.PactInfo pact=new Neusoft.HISFC.Models.Base.PactInfo();

                pact.ID = this.Reader[0].ToString();
                pact.Name = this.Reader[1].ToString();
                al.Add(pact);
            }
            return al;
        }

        /// <summary>
        /// 更新挂号表的挂号的合同单位
        /// </summary>
        /// <param name="clinic_code"></param>
        /// <param name="pact"></param>
        /// <returns></returns>
        public int UpdateRegister(string clinic_code,Neusoft.HISFC.Models.Base.PactInfo pact)
        {
            string sql =  @"update fin_opr_register r set r.pact_code='{1}' , r.pact_name ='{2}' where r.clinic_code='{0}' ";

            try
            {
                sql = string.Format(sql, clinic_code,pact.ID,pact.Name);
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
            }

            return this.ExecNoQuery(sql);

 
        }

      

        #endregion

    }
}
