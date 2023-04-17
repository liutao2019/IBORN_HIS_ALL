using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Extend.ZDWY
{
    /// <summary>
    /// [功能描述: 计划生成本地化]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-6]<br></br>
    /// </summary>
    public class SetPlan
    {
        /// <summary>
        /// 日消耗获取计划
        /// </summary>
        /// <param name="deptNO">计划科室编码</param>
        /// <param name="consumeBeginTime">消耗开始时间</param>
        /// <param name="consumeEndTime">消耗结束时间</param>
        /// <param name="lowDays">下限天数</param>
        /// <param name="upDays">上限天数</param>
        /// <param name="drugType">药品类别</param>
        /// <param name="stencilNO">模板编码</param>
        /// <returns></returns>
        public ArrayList GetPlan(string deptNO, DateTime consumeBeginTime, DateTime consumeEndTime, int lowDays, int upDays, string drugType, string stencilNO)
        {
            if (consumeBeginTime >= consumeEndTime)
            {
                return new ArrayList();
            }
            if (string.IsNullOrEmpty(drugType))
            {
                drugType = "All";
            }
            if (string.IsNullOrEmpty(stencilNO))
            {
                stencilNO = "All";
            }

            //天数
            int spanDays = (consumeEndTime - consumeBeginTime).Days;

            string SQL = @"select  s.drug_code,
                                   (u.day_consume * {4} - s.store_sum),
                                   consume_qty,
                                   sum(s.store_sum),
                                   (select sum(sk.store_sum) from pha_com_stockinfo sk where sk.drug_code=s.drug_code and sk.valid_state='1')
                            from 
                            (
                                select o.drug_dept_code,
                                       o.drug_code,
                                       sum(o.out_num) consume_qty,
                                       round(sum(o.out_num)/{5},2) day_consume
                                from   pha_com_output o
                                where  o.drug_dept_code = decode('{0}','4003','4001','4103','4101','{0}')
                                and    o.out_date > to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                and    o.out_date < to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                group  by o.drug_dept_code,o.drug_code
                            ) u,pha_com_stockinfo s,pha_com_baseinfo b
                            where  s.drug_code = b.drug_code
                            and    b.valid_state = '1'
                            and    s.valid_state = '1'
                            and    s.drug_dept_code = u.drug_dept_code
                            and    s.drug_code = u.drug_code
                            and    u.day_consume * {3} > s.store_sum
                            and    (s.drug_type = '{6}' or '{6}' = 'All')
                            and    ('{7}' = 'All' or s.drug_code in (
                                    select drug_code from pha_com_drugopen where  stencil_code = '{7}'))
                            group by s.drug_code,b.custom_code,
                                     (u.day_consume * {4} - s.store_sum),
                                     consume_qty
                            order by b.custom_code";

            SQL = string.Format(SQL, 
                deptNO, 
                consumeBeginTime.ToString(), 
                consumeEndTime.ToString(), 
                lowDays.ToString(), 
                upDays.ToString(), 
                spanDays.ToString(),
                drugType,
                stencilNO);

            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            dbMgr.ExecQuery(SQL);
            ArrayList alInPlan = new ArrayList();
            while (dbMgr.Reader.Read())
            {
                FS.HISFC.Models.Pharmacy.InPlan inPlan = new FS.HISFC.Models.Pharmacy.InPlan();
                inPlan.Item.ID = dbMgr.Reader[0].ToString();
                inPlan.Extend = dbMgr.Reader[1].ToString();
                inPlan.OutputQty = FS.FrameWork.Function.NConvert.ToDecimal(dbMgr.Reader[2]);
                inPlan.Formula = "日消耗";
                inPlan.StoreQty = FS.FrameWork.Function.NConvert.ToDecimal(dbMgr.Reader[3]);
                inPlan.StoreTotQty = FS.FrameWork.Function.NConvert.ToDecimal(dbMgr.Reader[4]);
                alInPlan.Add(inPlan);
            }

            dbMgr.Reader.Close();

            return alInPlan;
        }

        /// <summary>
        /// 自定义公式获取计划
        /// </summary>
        /// <param name="deptNO"></param>
        /// <param name="consumeDays"></param>
        /// <param name="rate"></param>
        /// <param name="planDays"></param>
        /// <returns></returns>
        public ArrayList GetPlan(string deptNO, string consumeDays, string rate, string planDays)
        {
            string strSql = @" select 
                                    s.drug_code,
                                    u.day_consume * {3} * {2} - s.store_sum,
                                    consume_qty 
                                    from
                                    (
                                    select o.drug_dept_code,
                                       o.drug_code,
                                       sum(o.out_num) consume_qty,
                                       round(sum(o.out_num)/{1},2) day_consume
                                       from   pha_com_output o
                                       where  o.drug_dept_code = decode('{0}','4003','4001','4103','4101','{0}')
                                       and    o.out_date > trunc(sysdate) - {1}
                                       and    o.out_date < trunc(sysdate)
                                       group  by o.drug_dept_code,o.drug_code)u,pha_com_baseinfo b,pha_com_stockinfo s
                            where  s.drug_code = b.drug_code
                            and    b.valid_state = '1'
                            and    s.valid_state = '1'
                            and    s.drug_dept_code = u.drug_dept_code
                            and    s.drug_code = u.drug_code
                            and    u.day_consume * {3} > s.store_sum";
            strSql = string.Format(strSql,deptNO, consumeDays, rate, planDays);
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            dbMgr.ExecQuery(strSql);
            ArrayList alInPlan = new ArrayList();
            while (dbMgr.Reader.Read())
            {
                FS.HISFC.Models.Pharmacy.InPlan inPlan = new FS.HISFC.Models.Pharmacy.InPlan();
                inPlan.Item.ID = dbMgr.Reader[0].ToString();
                inPlan.Extend = dbMgr.Reader[1].ToString();
                inPlan.OutputQty = FS.FrameWork.Function.NConvert.ToDecimal(dbMgr.Reader[2]);
                inPlan.Formula = "日消耗";

                alInPlan.Add(inPlan);
            }

            dbMgr.Reader.Close();

            return alInPlan;
        }

        /// <summary>
        /// 警戒线获取计划
        /// </summary>
        /// <param name="deptNO">计划科室编码</param>
        /// <param name="drugType">药品类别</param>
        /// <param name="stencilNO">模板编码</param>
        /// <returns></returns>
        public ArrayList GetPlan(string deptNO, string drugType, string stencilNO)
        {
            if (string.IsNullOrEmpty(drugType))
            {
                drugType = "All";
            }
            if (string.IsNullOrEmpty(stencilNO))
            {
                stencilNO = "All";
            }

            string SQL = @"select  s.drug_code,
                                   s.top_sum-s.store_sum 
                            from   pha_com_stockinfo s,pha_com_baseinfo b
                            where  s.drug_code = b.drug_code
                            and    b.valid_state = '1'
                            and    s.valid_state = '1'
                            and    s.store_sum < s.low_sum
                            and    s.drug_dept_code = '{0}'
                            and    (s.drug_type = '{1}' or '{1}' = 'All')
                            and    ('{2}' = 'All' or s.drug_code in (
                            select drug_code from pha_com_drugopen where  stencil_code = '{2}'))";

            SQL = string.Format(SQL, deptNO, drugType, stencilNO);

            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            dbMgr.ExecQuery(SQL);
            ArrayList alInPlan = new ArrayList();
            while (dbMgr.Reader.Read())
            {
                FS.HISFC.Models.Pharmacy.InPlan inPlan = new FS.HISFC.Models.Pharmacy.InPlan();
                inPlan.Item.ID = dbMgr.Reader[0].ToString();
                inPlan.Extend = dbMgr.Reader[1].ToString();
                inPlan.Formula = "警戒线";
                alInPlan.Add(inPlan);
            }

            dbMgr.Reader.Close();

            return alInPlan;
        }
   
    }
}
