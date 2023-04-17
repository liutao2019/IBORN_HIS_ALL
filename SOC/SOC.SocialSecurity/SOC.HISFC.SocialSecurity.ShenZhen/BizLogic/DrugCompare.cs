using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.SocialSecurity.ShenZhen.BizLogic
{
    public class DrugCompare : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获取对照信息的明细数据
        /// </summary>
        /// <param name="ds">System.Data.DataSet</param>
        /// <returns></returns>
        public int QueryDetail(ref System.Data.DataSet ds)
        {
            string SQL = @"
                           select t.drug_code 药品编码,
                           y.custom_code 自定义码,
                           y.trade_name 药品名称,
                           y.specs 规格,
                           decode(y.valid_state,'1','否','是') 药品停用,
                           t.compare_item_code 对照编码,
                           t.compare_mark 对照说明,
                           t.oper_code 操作人,
                           t.oper_date 操作时间,
                           y.spell_code 拼音码,
                           y.wb_code 五笔码,       
                           t.extend1 扩展,
                           '' key
                           
                      from si_shenzhen_drugcompare t left outer join pha_com_baseinfo y
                      on   t.drug_code = y.drug_code
            ";

            return this.ExecQuery(SQL, ref ds);
        }

        /// <summary>
        /// 设置对照信息，先插入，然后更新
        /// </summary>
        /// <param name="drugNO">HIS药品编码</param>
        /// <param name="compareItemNO">对照项目编码</param>
        /// <param name="compareCommont">对照说明</param>
        /// <param name="extendInfo">扩展信息</param>
        /// <returns></returns>
        public int InsertDetail(string drugNO, string compareItemNO, string compareCommont, string extendInfo)
        {
            string insertSQL = @"
                            insert into si_shenzhen_drugcompare
                                   ( 
                                   drug_code,
                                   compare_item_code,
                                   compare_mark,
                                   oper_code,
                                   oper_date,     
                                   extend1
                                   )
                            values
                                  (
                                   '{0}',--t.drug_code,
                                   '{1}',--t.compare_item_code,
                                   '{2}',--t.compare_mark,
                                   '{3}',--t.oper_code,
                                   to_date('{4}','yyyy-mm-dd hh24:mi:ss'),--t.oper_date,     
                                   '{5}'--t.extend1
                                  )
           ";

            insertSQL = string.Format(insertSQL, drugNO, compareItemNO, compareCommont, this.Operator.ID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), extendInfo);
            int param = this.ExecNoQuery(insertSQL);
            return param;
        }

     /// <summary>
        /// 设置对照信息，先插入，然后更新
        /// </summary>
        /// <param name="drugNO">HIS药品编码</param>
        /// <param name="compareItemNO">对照项目编码</param>
        /// <param name="compareCommont">对照说明</param>
        /// <param name="extendInfo">扩展信息</param>
        /// <returns></returns>
        public int UpdateDetail(string drugNO, string compareItemNO, string compareCommont, string extendInfo)
        {
            string updateSQL = @"
                                update si_shenzhen_drugcompare set 
                                       drug_code = '{0}',
                                       compare_item_code = '{1}',
                                       compare_mark = '{2}',
                                       oper_code = '{3}',
                                       oper_date = to_date('{4}','yyyy-mm-dd hh24:mi:ss'),     
                                       extend1= '{5}'
                                ";
            updateSQL = string.Format(updateSQL, drugNO, compareItemNO, compareCommont, this.Operator.ID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), extendInfo);
            return this.ExecNoQuery(updateSQL);
        }

        /// <summary>
        /// 删除对照信息
        /// </summary>
        /// <param name="drugNO">HIS系统编码</param>
        /// <param name="compareItemNO">对照项目编码</param>
        /// <returns></returns>
        public int DeleteDetail(string drugNO,string compareItemNO)
        {
            string SQL = @"delete from si_shenzhen_drugcompare where drug_code = '{0}' and compare_item_code = '{1}'";
            return this.ExecNoQuery(SQL, drugNO, compareItemNO);
        }

    }
}
