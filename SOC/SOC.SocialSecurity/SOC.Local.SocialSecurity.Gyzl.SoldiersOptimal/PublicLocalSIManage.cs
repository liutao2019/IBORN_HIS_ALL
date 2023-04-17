using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FS.SOC.Local.SocialSecurity.Gyzl.SoldiersOptimal
{
    /// <summary>
    /// HIS待遇接口本地结算，数据管理类
    /// </summary>
    public class PublicLocalSIManage : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获取指定待遇类别，本地预结算费用比例 -- 最小费用类别
        /// </summary>
        /// <param name="strPactCode"></param>
        /// <param name="dtFeeRate"></param>
        /// <returns></returns>
        public long QueryLocalFeeRate(string strPactCode, out DataTable dtFeeRate)
        {
            long lngRes = 1;
            dtFeeRate = null;

            string strSQL = @"select a.pact_code,
       a.type_code,
       a.fee_code,
       a.pub_ratio,
       a.own_ratio,
       a.pay_ratio,
       a.eco_ratio,
       a.arr_ratio
  from fin_com_pactunitfeecoderate a
  where a.type_code = 0
  and a.pact_code = '{0}'";

            strSQL = string.Format(strSQL, strPactCode);

            try
            {
                DataSet ds = new DataSet();
                this.ExecQuery(strSQL, ref ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dtFeeRate = ds.Tables[0];
                }
            }
            catch (Exception objEx)
            {
                this.ErrCode = objEx.Message;
                this.Err = objEx.Message;
                return -1;
            }

            return lngRes;
        }
        /// <summary>
        /// 获取指定待遇类别，本地预结算费用比例 -- 项目
        /// </summary>
        /// <param name="strPactCode"></param>
        /// <param name="dtItemRate"></param>
        /// <returns></returns>
        public long QueryLocalItemRate(string strPactCode, out DataTable dtItemRate)
        {
            long lngRes = 0;
            dtItemRate = null;

            string strSQL = @"select a.pact_code,
       a.type_code,
       a.fee_code,
       a.pub_ratio,
       a.own_ratio,
       a.pay_ratio,
       a.eco_ratio,
       a.arr_ratio
  from fin_com_pactunitfeecoderate a
  where a.type_code <> 0
  and a.pact_code = '{0}'";

            strSQL = string.Format(strSQL, strPactCode);

            try
            {
                DataSet ds = new DataSet();
                this.ExecQuery(strSQL, ref ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dtItemRate = ds.Tables[0];
                }
            }
            catch (Exception objEx)
            {
                this.ErrCode = objEx.Message;
                this.Err = objEx.Message;
                return -1;
            }

            return lngRes;

        }
    }
}
