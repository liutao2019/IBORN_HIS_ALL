using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ShenZhen.BizLogic
{
    public class CompareSI: FS.FrameWork.Management.Database
    {

        /// <summary>
        /// 直接获取未对照的医保药品信息 
        /// </summary>
        /// <returns></returns>
        public ArrayList GetUnCompareDrug(string drugcode)
        {
            string strSql = "";
            /*
             *             0  '2' pact_code,         
                           1   a.drug_code his_code,
                           2   a.trade_name his_name,
                           3   a.regular_name regularname,
                           4   c.ybtybm center_code,
                           5  'X' center_sys_class,
                           6  b.name center_name ,
                           7  b.english_name center_ename  ,
                           8  '' center_specs,
                           9  b.dose_mode center_dose,
                           10  b.spell_code center_spell,
                           11  b.fee_code center_fee_code,
                           12  b.drug_type center_item_type,
                           13  b.drug_quality center_item_grade,
                           14  '1' center_rate,
                           15  '0' center_price,
                           16  b.memo center_memo,
                           17  b.spell_code his_spell,
                           18  b.wb_code his_wb_code,
                           19  c.yljgxmbm his_user_code,
                           20  a.specs his_specs,
                           21   a.retail_price his_price,
                           22  a.dose_model_code his_dose,
                           23  a.oper_code oper_code,
                           24  a.oper_date oper_date, 
                           25  ''special_limit_flag,
                           26  ''special_limit_content 
             */
            strSql = @"select '2' pact_code,         
                        a.drug_code his_code,
                        a.trade_name his_name,
                        a.regular_name regularname,
                        c.ybtybm center_code,
                        'X' center_sys_class,
                        b.name center_name ,
                        b.english_name center_ename  ,
                        '' center_specs,
                        b.dose_mode center_dose,
                        b.spell_code center_spell,
                        b.fee_code center_fee_code,
                        b.drug_type center_item_type,
                         decode(length(c.jsxm),'2','3','1','0') center_item_grade,
                        '1' center_rate,
                        '0' center_price,
                        b.memo center_memo,
                        a.spell_code his_spell,
                        a.wb_code his_wb_code,
                        c.yljgxmbm  his_user_code,
                        a.specs his_specs,
                        a.retail_price his_price,
                        a.dose_model_code his_dose,
                        a.oper_code oper_code,
                        a.oper_date oper_date, 
                        ''special_limit_flag,
                        ''special_limit_content 
                  from pha_com_baseinfo a ,
                     si_shenzhensi_drug b,
                   si_shenzhensi_compareappr c
                  where a.custom_code= c.yljgxmbm 
                    and b.center_code= c.ybtybm 
                    and a.special_flag2 <>'1'
                    and a.drug_code ='{0}'
                  union all
                  select '2' pact_code,         
                        a.drug_code his_code,
                        a.trade_name his_name,
                        a.regular_name regularname,
                        c.ybtybm center_code,
                        'X' center_sys_class,
                        b.name center_name ,
                        b.english_name center_ename  ,
                        '' center_specs,
                        b.dose_mode center_dose,
                        b.spell_code center_spell,
                        b.fee_code center_fee_code,
                        b.drug_type center_item_type,
                        decode(length(c.jsxm),'2','3','1','0') center_item_grade,
                        '1' center_rate,
                        '0' center_price,
                        d.compare_mark center_memo,
                        a.spell_code his_spell,
                        a.wb_code his_wb_code,
                        c.yljgxmbm  his_user_code,
                        a.specs his_specs,
                        a.retail_price his_price,
                        a.dose_model_code his_dose,
                        a.oper_code oper_code,
                        a.oper_date oper_date, 
                        ''special_limit_flag,
                        ''special_limit_content 
                  from pha_com_baseinfo a ,
                     si_shenzhensi_drug b,
                   si_shenzhensi_compareappr c,
                   si_shenzhen_drugcompare d
                  where a.drug_code =d.drug_code
                  and b.center_code= c.ybtybm 
                  and c.yljgxmbm =d.compare_item_code
                  and a.special_flag2 ='1'
                  and  a.drug_code ='{0}'";
            ArrayList DrugCompareList = new ArrayList();
            try
            {
                strSql = string.Format(strSql, drugcode);

                if (ExecQuery(strSql) == -1)
                {
                    this.Err = "执行SQL语句出错";
                    return null;
                }

                while (this.Reader.Read())
                {

                    //自定义码为空不对照
                    if (this.Reader[19].ToString() == "" || this.Reader[1].ToString() == "")
                    {
                        continue;
                    }
                    FS.HISFC.Models.SIInterface.Compare compare = new FS.HISFC.Models.SIInterface.Compare();
                    compare.CenterItem.FeeCode = this.Reader[11].ToString();  //结算项目
                    compare.CenterItem.ID = this.Reader[4].ToString();  //中心代码

                    compare.CenterItem.ItemGrade = this.Reader[13].ToString();  //医保目录等级（自费）

                    compare.CenterItem.Memo = this.Reader[16].ToString();  //说明
                    compare.CenterItem.Name = this.Reader[6].ToString();  //名称
                    compare.CenterItem.OperCode = FS.FrameWork.Management.Connection.Operator.ID;
                    compare.CenterItem.OperDate = DateTime.Now;
                    compare.CenterItem.PactCode = this.Reader[0].ToString();  //合同单位
                    compare.CenterItem.SysClass = this.Reader[5].ToString();  //医保项目类别
                    compare.HisCode = this.Reader[1].ToString();   //本地项目代码
                    compare.ID = this.Reader[19].ToString();  //本地项目自定义代码
                    compare.Name = this.Reader[2].ToString();   //本地项目名称
                    compare.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21].ToString()); //默认价this.Reader[21].ToString();  //本地项目单价
                    compare.Specs = this.Reader[20].ToString();   //本地项目规格
                    compare.SpellCode.SpellCode = this.Reader[17].ToString();   //本地项目拼音码
                    compare.SpellCode.WBCode = this.Reader[18].ToString();
                    compare.SpellCode.UserCode = this.Reader[19].ToString();//审批编码
                    DrugCompareList.Add(compare);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            return DrugCompareList;
        }

    }
}
