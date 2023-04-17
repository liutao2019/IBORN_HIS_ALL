using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.Extend.FuYou
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
                                   u.day_consume * {4} - s.store_sum,
                                   consume_qty
                            from 
                            (
                                select o.drug_dept_code,
                                       o.drug_code,
                                       sum(o.out_num) consume_qty,
                                       round(sum(o.out_num)/{5},2) day_consume
                                from   pha_com_output o
                                where  o.drug_dept_code = '{0}'
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
                                    select drug_code from pha_com_drugopen where  stencil_code = '{7}'))";

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

        public ArrayList GetAllStockinfoAsPlan(string deptCode,string drugType)
        {
            string SQL = @"select s.drug_code,
       s.store_sum,
       (select sum(i.store_sum)
          from pha_com_stockinfo i
         where i.drug_code = b.drug_code),
       b.pack_qty,
       b.pack_unit,
       b.retail_price,
       b.purchase_price,
       b.specs,
       b.spell_code,
       b.wb_code,
       b.custom_code,
       b.company_code,
       (select c.fac_name
          from pha_com_company c
         where c.fac_code = b.company_code
           and c.company_type = '1') company,
       b.producer_code,
       (select c.fac_name
          from pha_com_company c
         where c.fac_code = b.producer_code
           and c.company_type = '0') producer,
        b.trade_name

  from pha_com_stockinfo s, pha_com_baseinfo b
 where s.drug_code = b.drug_code
   and b.valid_state = '1'
   and s.valid_state = '1'
   and s.drug_dept_code = '8202'
   and (s.drug_type = 'All' or 'All' = 'All')
 order by b.custom_code
";

            SQL = string.Format(SQL, deptCode, drugType);

            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            //FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            dbMgr.ExecQuery(SQL);
            ArrayList alInPlan = new ArrayList();
            while (dbMgr.Reader.Read())
            {
                FS.HISFC.Models.Pharmacy.InPlan inPlan = new FS.HISFC.Models.Pharmacy.InPlan();
                inPlan.Item.ID = dbMgr.Reader[0].ToString();
                inPlan.StoreQty = FS.FrameWork.Function.NConvert.ToDecimal(dbMgr.Reader[1]);
                inPlan.StoreTotQty = FS.FrameWork.Function.NConvert.ToDecimal(dbMgr.Reader[2]);
                inPlan.Item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(dbMgr.Reader[3]);
                inPlan.Item.PackUnit = dbMgr.Reader[4].ToString();
                inPlan.Item.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(dbMgr.Reader[5]);
                inPlan.Item.PriceCollection.PurchasePrice = FS.FrameWork.Function.NConvert.ToDecimal(dbMgr.Reader[6]);
                inPlan.Item.Specs = dbMgr.Reader[7].ToString();
                inPlan.Item.SpellCode = dbMgr.Reader[8].ToString();
                inPlan.Item.WBCode = dbMgr.Reader[9].ToString();
                inPlan.Item.UserCode = dbMgr.Reader[10].ToString();
                inPlan.Item.Product.Company.ID = dbMgr.Reader[11].ToString();
                inPlan.Item.Product.Company.Name = dbMgr.Reader[12].ToString();
                inPlan.Item.Product.Producer.ID = dbMgr.Reader[13].ToString();
                inPlan.Item.Product.Producer.Name = dbMgr.Reader[14].ToString();
                inPlan.Item.Name = dbMgr.Reader[15].ToString();

                //inPlan.Formula = "警戒线";
                //inPlan.Item = itemMgr.GetItem(inPlan.Item.ID);
                inPlan.Dept.ID = deptCode;
                alInPlan.Add(inPlan);
            }

            dbMgr.Reader.Close();

            return alInPlan;
        }
   
    }
}
