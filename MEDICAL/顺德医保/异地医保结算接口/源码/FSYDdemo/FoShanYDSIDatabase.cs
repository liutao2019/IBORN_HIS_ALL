using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace FoShanYDSI
{
    public class FoShanYDSIDatabase : FS.FrameWork.Management.Database
    {
        
        //先不考虑
        //#region han-zf 事务传递
        //private new System.Data.IDbTransaction trans = null;
        //public new System.Data.IDbTransaction Trans
        //{
        //    set { this.trans = value; }
        //}

        //public new void SetTrans(System.Data.IDbTransaction t)
        //{
        //    this.trans = t;
        //}
        //#endregion

        /// <summary>
        /// 获取门诊结算流水号
        /// </summary>
        /// <returns></returns>
        public string GetBalanceSeq()
        {
            return this.GetSequence("SI.ZhuHai.GetBalanceSeq.Select");
        }

        /// <summary>
        /// 保存诊断
        /// </summary>
        /// <returns></returns>
        public int SaveDiag(string id, string diag1, string diag2, string diag3, ref string err)
        {
            string sqlStr = "update fin_ipr_siinmaininfo_yd set DIAGN1 = '{1}',DIAGN2 = '{2}',DIAGN3 = '{3}' where INPATIENT_NO = '{0}' ";
            sqlStr = string.Format(sqlStr, id, diag1, diag2, diag3);
            try
            {                
                if (this.ExecNoQuery(sqlStr) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
                return 1;
            }
            catch(Exception e)
            {
                err = e.ToString();
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            
        }

        /// <summary>
        /// 查询ICD10
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryICD()
        {
            ArrayList al = new ArrayList();
            string sqlStr = "select icd_code, icd_name from met_com_icd10";
            try
            {
                this.ExecQuery(sqlStr);
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject Obj = new FS.FrameWork.Models.NeuObject();
                    Obj.ID = Reader[0].ToString();
                    Obj.Name = Reader[1].ToString();                    
                    al.Add(Obj);
                }
                return al;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询ICD10
        /// </summary>
        /// <returns></returns>
        public string QueryICDName(string ICD)
        {
            string sqlStr = "select  icd_name from met_com_icd10 where icd_code='{0}'";
            sqlStr = string.Format(sqlStr, ICD);
            try
            {
                this.ExecQuery(sqlStr);
                string icdName = "";
                while (this.Reader.Read())
                {
                    icdName = Reader[0].ToString();
                }
                return icdName;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询自动上传失败的发票上传状态
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns>1已经上传,其他未上传</returns>
        public int QueryInvoiceUpState(FS.HISFC.Models.Registration.Register patientInfo)
        {
            string strSql = @"select U.BALANCE_SEQ
  FROM foshan_INVOICE_NEEDUPLOAD U
 WHERE U.INPATIENT_NO = '{0}'
   AND U.INVOICE_NO = '{1}'
";
            strSql = string.Format(strSql, patientInfo.ID, patientInfo.SIMainInfo.InvoiceNo);

            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    //结算流水号先从上传失败发票表取，避免上传失败时后一个SQL查询不到
                    patientInfo.SIMainInfo.RegNo = Reader[0].ToString();
                }
            }
            catch (Exception e)
            {
            }


            strSql = @"select s.name,
                                     s.FOSHANSITYPE,
                                     s.reg_no,
                                     s.tot_cost
                                from fin_ipr_siinmaininfo s
                               where s.inpatient_no = '{0}'
                                 and s.pact_code = '{1}'
                                 and s.invoice_no = '{2}'--未上传发票号成功就退费会造成报错
                                 and s.valid_flag = '1'
                                 --and s.REG_NO IS NOT NULL";
            strSql = string.Format(strSql, patientInfo.ID, patientInfo.Pact.ID, patientInfo.SIMainInfo.InvoiceNo);

            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    patientInfo.Name = Reader[0].ToString();
                    patientInfo.SIMainInfo.AnotherCity.User01 = Reader[1].ToString();
                    patientInfo.SIMainInfo.RegNo = Reader[2].ToString();
                    patientInfo.SIMainInfo.TotCost = Decimal.Parse(Reader[3].ToString());
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取门诊结算基本信息(用于取消结算)
        /// SIMainInfo.InvoiceNo非空
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int QueryOutBalanceBaseInfo(FS.HISFC.Models.Registration.Register patientInfo)
        {
            string strSql = @"select s.name,
                                     s.FOSHANSITYPE,
                                     s.reg_no,
                                     s.tot_cost
                                from fin_ipr_siinmaininfo s
                               where s.inpatient_no = '{0}'
                                 and s.pact_code = '{1}'
                                 and s.invoice_no = '{2}'--未上传发票号成功就退费会造成报错
                                 and s.valid_flag = '1'
                                 --and s.REG_NO IS NOT NULL";
            strSql = string.Format(strSql, patientInfo.ID, patientInfo.Pact.ID, patientInfo.SIMainInfo.InvoiceNo);

            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    patientInfo.Name = Reader[0].ToString();
                    patientInfo.SIMainInfo.AnotherCity.User01 = Reader[1].ToString();
                    patientInfo.SIMainInfo.RegNo = Reader[2].ToString();
                    patientInfo.SIMainInfo.TotCost = Decimal.Parse(Reader[3].ToString());
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
            return 1;
        }

        public string QueryDocIdenno(string code)
        {
            string sql = @"select e.idenno from com_employee e where e.empl_code = '{0}'";
            sql = string.Format(sql, code);
            try
            {
                return this.ExecSqlReturnOne(sql);

            }
            catch(Exception e)
            {
                return "";
            }
        }

        public ArrayList QueryTYPEFormComDictionary(string strType)
        {
            ArrayList al=new ArrayList();
            string strSql = @"select c.code,
                               c.name,
                               c.mark,
                               c.spell_code,
                               c.wb_code,
                               c.input_code,
                               c.sort_id,
                               c.oper_code,
                               c.oper_date,
                               c.is_common,
                               c.kind_id
                        from com_dictionary c
                        where c.type='{0}'
                        order by c.sort_id desc";
            strSql = string.Format(strSql, strType);
            try
            {
                int intFlag = this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject Obj = new FS.FrameWork.Models.NeuObject();
                    Obj.ID=Reader[0].ToString();
                    Obj.Name=Reader[1].ToString();
                    Obj.Memo=Reader[2].ToString();
                    Obj.User01=Reader[3].ToString();
                    Obj.User02=Reader[4].ToString();
                    Obj.User03=Reader[5].ToString();
                    al.Add(Obj);
                }
                return al;
            }
            catch (Exception e)
            {
                return null;
            }

        }


        /// <summary>
        /// 获得参保险种字典名称
        /// </summary>
        /// <returns></returns>
        public ArrayList GetSiTypeDic()
        {
            //有时间再挪到常数
            ArrayList arrRet = new System.Collections.ArrayList();
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "1";
            obj1.Name = "未成年医保";
            arrRet.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "2";
            obj2.Name = "居民医保";
            arrRet.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "3";
            obj3.Name = "基本医疗";
            arrRet.Add(obj3);
            FS.HISFC.Models.Base.Item obj4 = new FS.HISFC.Models.Base.Item();
            obj4.ID = "4";
            obj4.Name = "基本医疗+补助";
            arrRet.Add(obj4);
            FS.HISFC.Models.Base.Item obj5 = new FS.HISFC.Models.Base.Item();
            obj5.ID = "5";
            obj5.Name = "大病医保";
            arrRet.Add(obj5);
            FS.HISFC.Models.Base.Item obj6 = new FS.HISFC.Models.Base.Item();
            obj6.ID = "6";
            obj6.Name = "生育医保";
            arrRet.Add(obj6);
            FS.HISFC.Models.Base.Item obj7 = new FS.HISFC.Models.Base.Item();
            obj7.ID = "7";
            obj7.Name = "工伤医保";
            arrRet.Add(obj7);
            FS.HISFC.Models.Base.Item obj8 = new FS.HISFC.Models.Base.Item();
            obj8.ID = "8";
            obj8.Name = "门诊统筹";
            arrRet.Add(obj8);

            return arrRet;
            //string strRet = (from FS.HISFC.Models.Base.Item obj in arrRet
            //                 where obj.ID.Equals(code)
            //                 select obj.Name).FirstOrDefault();
            //return strRet;
        }

        /// <summary>
        /// 获得人员类别字典
        /// </summary>
        /// <returns></returns>
        public ArrayList GetPersonTypeDic()
        {
            //有时间再挪到常数
            ArrayList arrRet = new System.Collections.ArrayList();
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "11";
            obj1.Name = "在职";
            arrRet.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "21";
            obj2.Name = "退休";
            arrRet.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "31";
            obj3.Name = "离休";
            arrRet.Add(obj3);
            FS.HISFC.Models.Base.Item obj4 = new FS.HISFC.Models.Base.Item();
            obj4.ID = "32";
            obj4.Name = "老红军";
            arrRet.Add(obj4);
            FS.HISFC.Models.Base.Item obj5 = new FS.HISFC.Models.Base.Item();
            obj5.ID = "33";
            obj5.Name = "二等乙级以上革命残伤军人";
            arrRet.Add(obj5);
            FS.HISFC.Models.Base.Item obj6 = new FS.HISFC.Models.Base.Item();
            obj6.ID = "41";
            obj6.Name = "失业";
            arrRet.Add(obj6);
            FS.HISFC.Models.Base.Item obj7 = new FS.HISFC.Models.Base.Item();
            obj7.ID = "91";
            obj7.Name = "其他";
            arrRet.Add(obj7);
            FS.HISFC.Models.Base.Item obj8 = new FS.HISFC.Models.Base.Item();
            obj8.ID = "100";
            obj8.Name = "居民普通人群";
            arrRet.Add(obj8);
            FS.HISFC.Models.Base.Item obj9 = new FS.HISFC.Models.Base.Item();
            obj9.ID = "101";
            obj9.Name = "居民特殊人群";
            arrRet.Add(obj9);
            FS.HISFC.Models.Base.Item obj10 = new FS.HISFC.Models.Base.Item();
            obj10.ID = "102";
            obj10.Name = "未成年";
            arrRet.Add(obj10);

            return arrRet;
            //string strRet = (from FS.HISFC.Models.Base.Item obj in arrRet
            //                 where obj.ID.Equals(code)
            //                 select obj.Name).FirstOrDefault();
            //return strRet;
        }

        /// <summary>
        /// 获得转归字典
        /// </summary>
        /// <returns></returns>
        public ArrayList GetZGDic()
        {
            //有时间再挪到常数
            ArrayList arrRet = new System.Collections.ArrayList();
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "01";
            obj1.Name = "治愈";
            arrRet.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "02";
            obj2.Name = "好转";
            arrRet.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "03";
            obj3.Name = "未愈";
            arrRet.Add(obj3);
            FS.HISFC.Models.Base.Item obj4 = new FS.HISFC.Models.Base.Item();
            obj4.ID = "04";
            obj4.Name = "死亡";
            arrRet.Add(obj4);
            FS.HISFC.Models.Base.Item obj5 = new FS.HISFC.Models.Base.Item();
            obj5.ID = "05";
            obj5.Name = "其他";
            arrRet.Add(obj5);

            return arrRet;
        }


        /// <summary>
        /// 查询门诊生育结算项目
        /// </summary>
        /// <param name="arrOutBalanceItemSY"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int QueryOutBalanceItemSY(ref ArrayList arrOutBalanceItemSY, ref DataTable dt)
        {
            DataSet ds = new DataSet();
            string strSql = @"SELECT B.ITEM_CODE AS 项目代码,
       B.ITEM_NAME AS 项目名称,
       fun_get_querycode(B.ITEM_NAME, '1') AS SPEL_CODE,
       fun_get_querycode(B.ITEM_NAME, '0') AS WB_CODE
  FROM FOSHAN_BALANCE_TYPE B
 WHERE B.BALANCE_TYPE = '03'
";
            int intFlag = this.ExecQuery(strSql, ref ds);
            if (intFlag < 0)
            {
                this.Err = "";
                return -1;
            }
            dt = ds.Tables[0].Copy();

            FS.HISFC.Models.Base.Item obj;
            while (this.Reader.Read())
            {
                obj = new FS.HISFC.Models.Base.Item();
                obj.ID = Reader[0].ToString();
                obj.Name = Reader[1].ToString();
                obj.SpellCode = Reader[2].ToString();
                obj.WBCode = Reader[3].ToString();

                arrOutBalanceItemSY.Add(obj);
            }
            return 1;
        }


        /// <summary>
        /// 获取单个对照项目
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int GetCompareSingleItem(string pactCode, string itemCode, ref FS.HISFC.Models.SIInterface.Compare compareItem)
        {
            int result = 0;

            #region 查询赋值
            string strSql = @"SELECT
pact_code,         his_code,           center_code,     center_sys_class,  center_name,         --#1
center_ename,      center_specs,       center_dose,     center_spell,      center_fee_code,     --#2
center_item_type,  center_item_grade,  center_rate,     center_price,      center_memo,         --#3
his_spell,         his_wb_code,        his_user_code,   his_specs,         his_price,           --#4
his_dose,          oper_code,          oper_date,       regularname,       center_property_code,--#5
center_property_name, fda_code  --#6
                                FROM fin_com_compare c
                               WHERE pact_code = '{0}'
                                 AND his_code = '{1}'";

            strSql = string.Format(strSql, pactCode, itemCode);

            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    if (compareItem == null)
                    {
                        compareItem = new FS.HISFC.Models.SIInterface.Compare();
                    }
                    //pact_code,         his_code,           center_code,     center_sys_class,  center_name,         --#1
                    compareItem.CenterItem.PactCode = Reader[0].ToString();
                    compareItem.HisCode = Reader[1].ToString();
                    compareItem.CenterItem.ID = Reader[2].ToString();
                    compareItem.CenterItem.SysClass = Reader[3].ToString();
                    compareItem.CenterItem.Name = Reader[4].ToString();

                    //center_ename,      center_specs,       center_dose,     center_spell,      center_fee_code,     --#2
                    compareItem.CenterItem.EnglishName = Reader[5].ToString();
                    compareItem.CenterItem.Specs = Reader[6].ToString();
                    compareItem.CenterItem.DoseCode = Reader[7].ToString();
                    compareItem.CenterItem.SpellCode = Reader[8].ToString();
                    compareItem.CenterItem.FeeCode = Reader[9].ToString();//消费类别

                    //center_item_type,  center_item_grade,  center_rate,     center_price,      center_memo,         --#3
                    compareItem.CenterItem.ItemType = Reader[10].ToString();
                    compareItem.CenterItem.ItemGrade = Reader[11].ToString();//医保类型
                    compareItem.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    compareItem.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    compareItem.CenterItem.Memo = Reader[14].ToString();

                    //his_spell,         his_wb_code,        his_user_code,   his_specs,         his_price,           --#4
                    compareItem.SpellCode.SpellCode = Reader[15].ToString();
                    compareItem.SpellCode.WBCode = Reader[16].ToString();
                    compareItem.SpellCode.UserCode = Reader[17].ToString();
                    compareItem.Specs = Reader[18].ToString();
                    compareItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());

                    //his_dose,          oper_code,          oper_date,       regularname,       center_property_code,--#5
                    compareItem.DoseCode = Reader[20].ToString();
                    compareItem.CenterItem.OperCode = Reader[21].ToString();
                    compareItem.CenterItem.OperDate = Convert.ToDateTime(Reader[22].ToString());
                    compareItem.RegularName = Reader[23].ToString();//通用名

                    #region 后面处理 佛山
                    //compareItem.FeeProperty = new FS.FrameWork.Models.NeuObject();
                    //compareItem.FeeProperty.ID = Reader[24].ToString();//费用属性

                    ////center_property_name, fda_code  --#6
                    //compareItem.FeeProperty.Name = Reader[25].ToString();
                    //compareItem.FdaDrguCode = Reader[26].ToString();//药监局药品编码
                    #endregion
                    result = 1;
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception e)
            {
            }
            #endregion


            return result;
        }

        /// <summary>
        /// 获取本地HIS与佛山医保的对照码
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public ArrayList GetCompareItems(string pactCode, ref string errInfo)
        {
            ArrayList alCompareItems = new ArrayList();
            string strSql = @"SELECT
                                pact_code,         his_code,           center_code,     center_sys_class,  center_name,         --#1
                                center_ename,      center_specs,       center_dose,     center_spell,      center_fee_code,     --#2
                                center_item_type,  center_item_grade,  center_rate,     center_price,      center_memo,         --#3
                                his_spell,         his_wb_code,        his_user_code,   his_specs,         his_price,           --#4
                                his_dose,          oper_code,          oper_date,       regularname,       center_property_code,--#5
                                center_property_name, fda_code  --#6
                                FROM fin_com_compare c
                                WHERE pact_code = '{0}'";

            try
            {
                strSql = string.Format(strSql, pactCode);
                if (this.ExecQuery(strSql) == -1)
                {
                    errInfo += this.Err;
                    return null;
                }

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Compare compareItem = new FS.HISFC.Models.SIInterface.Compare();

                    #region 实体赋值

                    //pact_code,         his_code,           center_code,     center_sys_class,  center_name,         --#1
                    compareItem.CenterItem.PactCode = Reader[0].ToString();
                    compareItem.HisCode = Reader[1].ToString();
                    compareItem.CenterItem.ID = Reader[2].ToString();
                    compareItem.CenterItem.SysClass = Reader[3].ToString();
                    compareItem.CenterItem.Name = Reader[4].ToString();

                    //center_ename,      center_specs,       center_dose,     center_spell,      center_fee_code,     --#2
                    compareItem.CenterItem.EnglishName = Reader[5].ToString();
                    compareItem.CenterItem.Specs = Reader[6].ToString();
                    compareItem.CenterItem.DoseCode = Reader[7].ToString();
                    compareItem.CenterItem.SpellCode = Reader[8].ToString();
                    compareItem.CenterItem.FeeCode = Reader[9].ToString();//消费类别

                    //center_item_type,  center_item_grade,  center_rate,     center_price,      center_memo,         --#3
                    compareItem.CenterItem.ItemType = Reader[10].ToString();
                    compareItem.CenterItem.ItemGrade = Reader[11].ToString();//医保类型
                    compareItem.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    compareItem.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    compareItem.CenterItem.Memo = Reader[14].ToString();

                    //his_spell,         his_wb_code,        his_user_code,   his_specs,         his_price,           --#4
                    compareItem.SpellCode.SpellCode = Reader[15].ToString();
                    compareItem.SpellCode.WBCode = Reader[16].ToString();
                    compareItem.SpellCode.UserCode = Reader[17].ToString();
                    compareItem.Specs = Reader[18].ToString();
                    compareItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());

                    //his_dose,          oper_code,          oper_date,       regularname,       center_property_code,--#5
                    compareItem.DoseCode = Reader[20].ToString();
                    compareItem.CenterItem.OperCode = Reader[21].ToString();
                    compareItem.CenterItem.OperDate = (string.IsNullOrEmpty(Reader[22].ToString()) ? DateTime.Now : Convert.ToDateTime(Reader[22].ToString()));
                    compareItem.RegularName = Reader[23].ToString();//通用名
                    #region 佛山后面处理
                    //compareItem.FeeProperty = new FS.FrameWork.Models.NeuObject();
                    //compareItem.FeeProperty.ID = Reader[24].ToString();//费用属性

                    ////center_property_name, fda_code  --#6
                    //compareItem.FeeProperty.Name = Reader[25].ToString();
                    //compareItem.FdaDrguCode = Reader[26].ToString();//药监局药品编码
                    #endregion 

                    #endregion

                    alCompareItems.Add(compareItem);
                }

                return alCompareItems;
            }
            catch (Exception e)
            {
                errInfo += e.Message;
                return null;
            }

        }

        /// <summary>
        /// 插入医保信息(门诊)
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <param name="outBalanceInfo"></param>
        /// <returns></returns>
        public int InsertSIMainInfo(FS.HISFC.Models.Registration.Register patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo, FoShanYDSI.Objects.OutBalanceInfo outBalanceInfo)
        {
            //医保基本信息保存到表fin_ipr_siinmaininfo
            //佛山医保扩展信息需要另外保存
            string balanceNo = this.GetBalanceNo(patientInfo.ID);
            if (string.IsNullOrEmpty(balanceNo))
            {
                balanceNo = "0";
            }
            balanceNo = (int.Parse(balanceNo) + 1).ToString();

            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region sql
            string strSql = @"INSERT INTO fin_ipr_siinmaininfo f
                                (
                                inpatient_no,    reg_no,      balance_no,    patient_no,
                                card_no,         mcard_no,    name,          idenno,
                                clinic_diagnose, paykind_code,pact_code,     pact_name,
                                oper_code,       oper_date,   TOT_COST,      PUB_COST, 
                                OWN_COST,        VALID_FLAG,  FEE_TIMES,     SEX_CODE,
                                DEPT_CODE,       IN_DATE,     BALANCE_DATE,  FOSHANSITYPE,
                                TYPE_CODE
                                )
                                Values
                                (
                                '{0}',          '{1}',        '{2}',        '{3}', 
                                '{4}',          '{5}',        '{6}',        '{7}', 
                                '{8}',          '{9}',        '{10}',       '{11}',
                                '{12}',    to_date('{13}','YYYY-MM-DD hh24:mi:ss'),   '{14}', '{15}', 
                                '{16}',       '{17}',          0,           '{18}',
                                '{19}',  to_date('{20}','YYYY-MM-DD hh24:mi:ss'),  to_date('{21}','YYYY-MM-DD hh24:mi:ss'),'{22}',
                                '{23}'
                                )";
            strSql = string.Format(strSql,
                patientInfo.ID, patientInfo.SIMainInfo.RegNo, balanceNo, patientInfo.PID.PatientNO,
                patientInfo.PID.CardNO, personInfo.MCardNo, personInfo.Name, personInfo.IdenNo,
                personInfo.OutDiseaseType, patientInfo.Pact.PayKind.ID, patientInfo.Pact.ID, patientInfo.Pact.Name,
                this.Operator.ID, patientInfo.SIMainInfo.BalanceDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                                                  patientInfo.SIMainInfo.TotCost, patientInfo.SIMainInfo.PubCost,
                patientInfo.SIMainInfo.OwnCost, "1", patientInfo.Sex.ID, patientInfo.DoctorInfo.Templet.Dept.ID,
                patientInfo.SIMainInfo.BalanceDate.ToString("yyyy-MM-dd HH:mm:ss"),
                patientInfo.SIMainInfo.BalanceDate.ToString("yyyy-MM-dd HH:mm:ss"),
                personInfo.SIType, "1"
                );


            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                //FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            #region sql
            strSql = @"insert into fin_ipr_siinmaininfo_zhuhai
                      (
                      INPATIENT_NO,      BALANCE_NO,       MCARD_NO,          IDENNO,
                      NAME,              GUARD_1_NAME,     GUARD_1_IDENNO,    GUARD_2_NAME,
                      GUARD_2_IDENNO,    YEAR,             SI_TYPE,           SI_MAIN_TYPE, 
                      PERSON_TYPE,       OUT_DISEASE_TYPE, LIMIT_COST,        EVALUATE_TYPE, 
                      ACCEPT_NO,         ACCIDENT_DATE,    ACCIDENT_REASON,   OVER_DISEASE_TYPE,
                      OVER_OWNCOST_ITEM, OVER_OWNCOST,     BANK,              ACOUNT, 
                      BALANCE_SEQ,       OPER_DATE,        DETAIL_ROWCOUNT,   TOT_COST,
                      APPROVAL_COST,     PUB_RATE,         PUB_COST,          REST_LIMIT_COST,
                      BILL_NO,           SUBSIDY,          OWNITEM_SUPPLY,    FUND_PAY_COST,
                      PAY_COST,          PAY_RATE,         ALLOWANCE
                      )
                    values
                      (
                      '{0}',             '{1}',             '{2}',           '{3}',
                      '{4}',             '{5}',             '{6}',           '{7}', 
                      '{8}',             '{9}',             '{10}',          '{11}', 
                      '{12}',            '{13}',             {14},           '{15}',  
                      '{16}',            '{17}',            '{18}',          '{19}',   
                      '{20}',             {21},             '{22}',          '{23}', 
                      '{24}',            '{25}',            '{26}',           {27},
                       {28},              {29},              {30},            {31},  
                      '{32}',             {33},              {34},            {35},  
                       {36},              {37},              {38}
                      )";


            strSql = string.Format(strSql,
                patientInfo.ID, balanceNo, personInfo.MCardNo, personInfo.IdenNo,    //#1
                personInfo.Name, personInfo.Guard1Name, personInfo.Guard1IdenNo, personInfo.Guard2Name,//#2
                personInfo.Guard2IdenNo, personInfo.Year, personInfo.SIType, personInfo.SIMainType,//#3
                personInfo.PersonType, personInfo.OutDiseaseType, personInfo.LimitCost, personInfo.EvaluateType,//#4
                personInfo.AcceptNo, personInfo.AccidentDate, personInfo.AccidentReason, personInfo.OverDiseaseType,//#5
                personInfo.OverOwnCostItem, personInfo.OverOwnCost, outBalanceInfo.Bank, outBalanceInfo.Acount,//#6
                outBalanceInfo.BalanceSeq, outBalanceInfo.OperDate, outBalanceInfo.DetailRowCount, outBalanceInfo.TotCost, //#7
                outBalanceInfo.ApprovalCost, personInfo.PubRate, outBalanceInfo.PubCost, outBalanceInfo.RestLimitCost, //#8
                outBalanceInfo.BillNo, outBalanceInfo.Subsidy, outBalanceInfo.OwnItemSupply, outBalanceInfo.FundPayCost,  //#9
                outBalanceInfo.PayCost, outBalanceInfo.PayRate, outBalanceInfo.Allowance                               //#10
                );
            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                //FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            patientInfo.SIMainInfo.BalNo = balanceNo;
            //FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        /// <summary>
        /// 更新医保信息状态, false无效, true有效(暂时不提供)
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateSIMainInfo(FS.HISFC.Models.Registration.Register patientInfo, bool status)
        {
            if (status == true)
            {
                return -1;
            }
            else
            {
                string strSql = @"update fin_ipr_siinmaininfo
                                 set valid_flag = '{0}'
                               where inpatient_no = '{1}'
                                 and reg_no = '{2}'
                                 and tot_cost = '{3}'";
                strSql = string.Format(strSql, "0", patientInfo.ID, patientInfo.SIMainInfo.RegNo, patientInfo.SIMainInfo.TotCost);
                try
                {
                    if (this.ExecNoQuery(strSql) < 0)
                    {
                        this.Err = this.ErrorException;
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    this.Err = e.Message;
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 门诊确认结算更新医保主表发票
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int UpdateSIMainInfoInvoiceNo(FS.HISFC.Models.Registration.Register patientInfo)
        {
            if (string.IsNullOrEmpty(patientInfo.SIMainInfo.InvoiceNo))
            {
                return -1;
            }
            else
            {
                string strSql = @"update fin_ipr_siinmaininfo
                                 set INVOICE_NO = '{0}'
                               where inpatient_no = '{1}'
                                 and BALANCE_NO = '{2}'";
                strSql = string.Format(strSql, patientInfo.SIMainInfo.InvoiceNo, patientInfo.ID, patientInfo.SIMainInfo.BalNo);
                try
                {
                    if (this.ExecNoQuery(strSql) < 0)
                    {
                        this.Err = this.ErrorException;
                        return -1;
                    }
                    return 1;
                }
                catch (Exception e)
                {
                    this.Err = e.Message;
                    return -1;
                }
            }
        }


        #region 项目对照

        /// <summary>
        /// 查询本地费用字典
        /// </summary>
        /// <param name="ds"></param>  
        /// <returns></returns>
        public int QueryDicLocal(ref DataSet ds)
        {
            string strSql = @"SELECT B.DRUG_CODE,       B.TRADE_NAME,       B.SPECS,       B.SPELL_CODE,       B.WB_CODE,
       B.CUSTOM_CODE,     B.RETAIL_PRICE,     B.PURCHASE_PRICE,      '1' AS FEE_TYPE
  FROM PHA_COM_BASEINFO B
 WHERE B.VALID_STATE = '1'
UNION ALL
SELECT U.ITEM_CODE,       U.ITEM_NAME,       U.SPECS,       U.SPELL_CODE,       U.WB_CODE,
       U.INPUT_CODE,      U.UNIT_PRICE,      U.UNIT_PRICE,  '2' AS FEE_TYPE
  FROM FIN_COM_UNDRUGINFO U
 WHERE U.VALID_STATE = '1'
";
            strSql = string.Format(strSql);
            return this.ExecQuery(strSql, ref ds);
        }

        /// <summary>
        /// 查询中心费用字典
        /// </summary>
        /// <param name="ds"></param>   
        /// <returns></returns>
        public int QueryDicCenter(ref DataSet ds)
        {
            string strSql = @"";

            strSql = string.Format(strSql);
            return this.ExecQuery(strSql, ref ds);
        }

        /// <summary>
        /// 查询对照字典
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="pactCode">合同单位编码</param>
        /// <param name="userCode">自定义码</param>
        /// <param name="itemCode">项目编码</param>
        /// <returns></returns>
        public int QueryDicComp(ref DataSet ds, string pactCode, string userCode, string itemCode)
        {
            string strSql = @"";
            strSql = string.Format(strSql);
            return this.ExecQuery(strSql, ref ds);
        }

        public int DeleteComp(string pactCode, string userCode, string itemCode)
        {
            return 1;
        }

        public int InsertCom()
        {
            return 1;
        }
        #endregion

        #region 门诊发票补传
        /// <summary>
        /// 查询门诊未自动上传成功的发票
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="dtBgn"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public int QueryNeedUpLoadInvoiceNo(ref DataSet ds, DateTime dtBgn, DateTime dtEnd)
        {
            string strSql = @"SELECT U.UP_FALG,
       DECODE(U.UP_FALG, '1', '已上传', '未上传') AS 上传标识,
       TO_CHAR(U.BALANCE_DATE, 'YYYY-MM-DD HH24:MI:SS') AS 收费日期,
       U.INVOICE_NO AS 发票流水号,
       U.INVOICE_NO_PRINT AS 发票印刷号,
       U.NAME AS 姓名,
       U.IDNO AS 身份证号码,
       U.MCAR_NO AS 社保卡号,
       U.BALANCE_SEQ AS 结算流水号,
       --结算单号
       --人员类别
       U.PACT_ID,
       U.PACT_NAME AS 合同单位,
       U.SI_TYPE,
       U.SI_TYPE_NAME AS 参保险种,
       fun_get_employee_name(U.BALANCE_OPER_CODE) AS 收费员,
       fun_get_employee_name(U.OPER_CODE) AS 发票上传人员,
        U.INPATIENT_NO,U.BALANCE_NO
--门诊病种
--明细行数
--医保科室
--医生姓名
  FROM FOSHAN_INVOICE_NEEDUPLOAD U
 WHERE U.Balance_Date >= TO_DATE('{0}', 'YYYY-MM-DD HH24:MI:SS')
   AND U.BALANCE_DATE <= TO_DATE('{1}', 'YYYY-MM-DD HH24:MI:SS')
   AND NVL(U.CANCEL_FLAG,'1') !='0'"
   ;

            strSql = string.Format(strSql, dtBgn.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"));
            return this.ExecQuery(strSql, ref ds);
        }

        /// <summary>
        /// 更新门诊未自动上传成功的发票上传标识
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="upLoadFlag"></param>
        /// <returns></returns>
        public int UpdateNeedUpLoadInvoiceNo(FS.HISFC.Models.Registration.Register patientInfo, string upLoadFlag)
        {
            string strSql = @"UPDATE FOSHAN_INVOICE_NEEDUPLOAD U
   SET U.UP_FALG = '{2}',U.OPER_DATE=SYSDATE,U.OPER_CODE='{3}'
 WHERE U.INPATIENT_NO = '{0}'
   AND U.BALANCE_SEQ = '{1}'";
            strSql = string.Format(strSql, patientInfo.ID, patientInfo.SIMainInfo.RegNo, upLoadFlag, this.Operator.ID);
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    this.Err = this.ErrorException;
                    return -1;
                }
                return 1;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 插入门诊未自动上传成功的发票
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int InsertNeedUpLoadInvoiceNo(FS.HISFC.Models.Registration.Register patientInfo)
        {
            string strSql = @"  INSERT INTO FOSHAN_INVOICE_NEEDUPLOAD
     (INPATIENT_NO,      UP_FALG,      BALANCE_NO,      BALANCE_DATE,      SI_TYPE,
      INVOICE_NO_PRINT,  INVOICE_NO,   SI_TYPE_NAME,    IDNO,              MCAR_NO,
      NAME,              OPER_DATE,    OPER_CODE,       BALANCE_OPER_CODE, PACT_ID,
      PACT_NAME,         BALANCE_SEQ)
   VALUES
     ('{0}','{1}','{2}',TO_DATE('{3}','YYYY-MM-DD HH24:MI:SS'),'{4}',
      '{5}','{6}','{7}','{8}','{9}',
      '{10}',TO_DATE('{11}','YYYY-MM-DD HH24:MI:SS'),'{12}','{13}','{14}',
      '{15}','{16}'
     )";
            string siTypeName = (from FS.HISFC.Models.Base.Item obj in this.GetSiTypeDic()
                                 where obj.ID.Equals(patientInfo.SIMainInfo.AnotherCity.User01)
                                 select obj.Name).FirstOrDefault();

            strSql = string.Format(strSql, patientInfo.ID, "0", patientInfo.SIMainInfo.BalNo, patientInfo.SIMainInfo.BalanceDate.ToString("yyyy-MM-dd HH:mm:ss"), patientInfo.SIMainInfo.AnotherCity.User01,
                patientInfo.SIMainInfo.User01, patientInfo.SIMainInfo.InvoiceNo, siTypeName, patientInfo.IDCard, patientInfo.SSN,
                patientInfo.Name, patientInfo.SIMainInfo.BalanceDate.ToString("yyyy-MM-dd HH:mm:ss"), "", this.Operator.ID, patientInfo.Pact.ID,
                patientInfo.Pact.Name, patientInfo.SIMainInfo.RegNo);
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    this.Err = this.ErrorException;
                    return -1;
                }
                return 1;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }
            return 1;
        }
        #endregion

        #region 佛山医保住院

        /// <summary>
        /// 查询患者出院诊断
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="diagn"></param>
        /// <returns></returns>
        public int QueryDign(string inpatientNo, ref  FS.FrameWork.Models.NeuObject diagn, ref string msg)
        {
            diagn = new FS.FrameWork.Models.NeuObject();

            int result = 0;

            #region 查询赋值
            string strSql = @"SELECT D.ICD_CODE, D.DIAG_NAME
  FROM MET_CAS_DIAGNOSE D
 WHERE D.DIAG_KIND = '14'
 --D.MAIN_FLAG = '1'
 AND D.VALID_FLAG = '1'
 AND D.INPATIENT_NO = '{0}'
 ORDER BY D.OPER_DATE DESC
";
            strSql = string.Format(strSql, inpatientNo);

            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    diagn.ID = Reader[0].ToString();
                    diagn.Name = Reader[1].ToString();

                    result = 1;
                }
                else
                {
                    msg = "住院医师未给该患者未填写出院诊断！";
                    result = -1;
                }
            }
            catch (Exception e)
            {
                msg = "查询出院诊断出错，请联系信息科！";
                result = -1;
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 查询患者费用明细
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int QueryfeeDetails(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            feeDetails.Clear();
            int result = 0;

            #region 查询赋值

            #region 废弃

            //            string strSql = @"SELECT I.ITEM_CODE,
            //       I.ITEM_NAME,
            //       I.UNIT_PRICE,
            //       I.QTY,
            //       I.TOT_COST,
            //       decode(E.EMPL_TYPE,'D',E.EMPL_NAME,(SELECT c.EMPL_NAME FROM COM_EMPLOYEE c WHERE c.EMPL_CODE = r.HOUSE_DOC_CODE)) EMPL_NAME,
            //       decode(E.EMPL_TYPE,'D',E.IDENNO,(SELECT c.IDENNO FROM COM_EMPLOYEE c WHERE c.EMPL_CODE = r.HOUSE_DOC_CODE)) IDENNO,
            //       decode(E.EMPL_TYPE,'D',E.EMPL_CODE,r.HOUSE_DOC_CODE) EMPL_CODE
            //  FROM FIN_IPB_ITEMLIST I, COM_EMPLOYEE E, FIN_IPR_INMAININFO R
            // WHERE E.EMPL_CODE = I.RECIPE_DOCCODE
            //   AND I.INPATIENT_NO =  R.INPATIENT_NO
            //   AND I.BALANCE_STATE = '0'
            //   AND I.NOBACK_NUM > 0
            //  -- AND I.INPATIENT_NO='25246'
            //   AND I.TRANS_TYPE = '1'
            //   AND R.INPATIENT_NO = '{0}'
            // 
            //UNION ALL
            //SELECT M.DRUG_CODE,
            //       M.DRUG_NAME,
            //       ROUND(M.UNIT_PRICE / M.PACK_QTY, 2) AS UNIT_PRICE,
            //       M.QTY,
            //       M.TOT_COST,
            //       decode(EE.EMPL_TYPE,'D',EE.EMPL_NAME,(SELECT c.EMPL_NAME FROM COM_EMPLOYEE c WHERE c.EMPL_CODE = RR.HOUSE_DOC_CODE)) EMPL_NAME,
            //       decode(EE.EMPL_TYPE,'D',EE.IDENNO,(SELECT c.IDENNO FROM COM_EMPLOYEE c WHERE c.EMPL_CODE = RR.HOUSE_DOC_CODE)) IDENNO,
            //       decode(EE.EMPL_TYPE,'D',EE.EMPL_CODE,RR.HOUSE_DOC_CODE) EMPL_CODE
            //  FROM FIN_IPB_MEDICINELIST M, COM_EMPLOYEE EE, FIN_IPR_INMAININFO RR
            // WHERE EE.EMPL_CODE =  M.RECIPE_DOCCODE
            //   AND M.INPATIENT_NO = RR.INPATIENT_NO
            //   AND M.BALANCE_STATE = '0'
            //   AND M.NOBACK_NUM > 0
            //   --AND M.INPATIENT_NO='25246'
            //   AND M.TRANS_TYPE = '1'
            //   AND RR.INPATIENT_NO = '{0}'";

            #endregion

            string strSql = @"SELECT I.ITEM_CODE,
                   I.ITEM_NAME,
                   round(u.unit_price2, 4),
                   sum(I.QTY) QTY,
                   round(sum(u.unit_price2 * I.qty), 2) TOT_COST,
                   decode(E.EMPL_TYPE,'D',E.EMPL_NAME,(SELECT c.EMPL_NAME FROM COM_EMPLOYEE c WHERE c.EMPL_CODE = r.HOUSE_DOC_CODE)) EMPL_NAME,
                   decode(E.EMPL_TYPE,'D',E.IDENNO,(SELECT c.IDENNO FROM COM_EMPLOYEE c WHERE c.EMPL_CODE = r.HOUSE_DOC_CODE)) IDENNO,
                   decode(E.EMPL_TYPE,'D',E.EMPL_CODE,r.HOUSE_DOC_CODE) EMPL_CODE,
                   
                   i.item_code||to_char(i.charge_date,'yyyyMMdd'),
                   d2.mark,
                   d2.spell_code,
                   u.input_code,
                   u.gb_code,
                   trunc(i.charge_date) fee_date
              FROM FIN_IPB_ITEMLIST I, COM_EMPLOYEE E, FIN_IPR_INMAININFO R,fin_com_feecodestat fee,fin_com_undruginfo u,com_dictionary d2
             WHERE E.EMPL_CODE = I.RECIPE_DOCCODE
               AND I.INPATIENT_NO =  R.INPATIENT_NO
               and fee.report_code='ZY01'  --YDYB
               and fee.fee_code=i.fee_code
               --AND I.BALANCE_STATE = '0'
               AND R.INPATIENT_NO = '{0}'
               and u.item_code=i.item_code
               and d2.type='YD_CLASS_COMPARE'
               and d2.code=fee.fee_stat_cate
               and I.upload_flag<>'2'
             GROUP BY I.INPATIENT_NO,i.ITEM_CODE,i.ITEM_NAME,
                      u.unit_price2,r.HOUSE_DOC_CODE,e.EMPL_CODE,
                      e.EMPL_NAME,e.IDENNO,e.EMPL_TYPE ,i.charge_date,d2.mark,
                      d2.spell_code,u.input_code,
                   u.gb_code
             HAVING round(sum(u.unit_price2 * I.qty), 2) != 0
             
            UNION ALL
            SELECT M.DRUG_CODE,
                   M.DRUG_NAME,
                   round(b.wholesale_price / b.pack_qty, 4) AS UNIT_PRICE,
                   sum(M.QTY) QTY,
                   round(sum(b.wholesale_price / b.pack_qty * M.qty), 2) TOT_COST,
                   decode(EE.EMPL_TYPE,'D',EE.EMPL_NAME,(SELECT c.EMPL_NAME FROM COM_EMPLOYEE c WHERE c.EMPL_CODE = RR.HOUSE_DOC_CODE)) EMPL_NAME,
                   decode(EE.EMPL_TYPE,'D',EE.IDENNO,(SELECT c.IDENNO FROM COM_EMPLOYEE c WHERE c.EMPL_CODE = RR.HOUSE_DOC_CODE)) IDENNO,
                   decode(EE.EMPL_TYPE,'D',EE.EMPL_CODE,RR.HOUSE_DOC_CODE) EMPL_CODE,
                  
                   m.drug_code||to_char(m.charge_date,'yyyyMMdd'),
                   d2.mark,
                   d2.spell_code,
                   b.custom_code,
                   b.gb_code,
                   trunc(m.charge_date) fee_date
              FROM FIN_IPB_MEDICINELIST M, COM_EMPLOYEE EE, FIN_IPR_INMAININFO RR,fin_com_feecodestat fee,pha_com_baseinfo b ,com_dictionary d2
             WHERE EE.EMPL_CODE =  M.RECIPE_DOCCODE
               AND M.INPATIENT_NO = RR.INPATIENT_NO
               and fee.report_code='ZY01' --YDYB
               and fee.fee_code=M.fee_code
               --AND M.BALANCE_STATE = '0'
               AND RR.INPATIENT_NO = '{0}'
               and m.drug_code=b.drug_code
               and d2.type='YD_CLASS_COMPARE'
               and d2.code=fee.fee_stat_cate
               and M.upload_flag<>'2'
               GROUP BY M.INPATIENT_NO,m.DRUG_CODE,m.DRUG_NAME,
                        b.wholesale_price,b.pack_qty,rr.HOUSE_DOC_CODE,
                        ee.EMPL_CODE,ee.EMPL_NAME,ee.IDENNO,
                        ee.EMPL_TYPE,m.charge_date,d2.mark,
                        d2.spell_code,b.custom_code,
                   b.gb_code
               HAVING round(b.wholesale_price / b.pack_qty, 4) != 0                   
               ";

            strSql = string.Format(strSql, patient.ID);

            try
            {
                int intFlag = this.ExecQuery(strSql);
                patient.SIMainInfo.TotCost = 0;
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();

                    feeItem.Item.ID = this.Reader[0].ToString();
                    feeItem.Item.Name = this.Reader[1].ToString();
                    feeItem.Item.Price = Convert.ToDecimal(this.Reader[2].ToString());
                    feeItem.Item.Qty = Convert.ToDecimal(this.Reader[3].ToString());
                    feeItem.FT.TotCost = Convert.ToDecimal(this.Reader[4].ToString());
                    patient.SIMainInfo.TotCost += feeItem.FT.TotCost;
                    //医生信息
                    feeItem.RecipeOper.Name = this.Reader[5].ToString();     //医生姓名
                    feeItem.RecipeOper.Memo = this.Reader[6].ToString();     //医生身份证号
                    feeItem.RecipeOper.ID = this.Reader[7].ToString();       //医生工号

                    feeItem.RecipeNO = this.Reader[8].ToString();//异地医保临时用的处方号
                    feeItem.Item.MinFee.ID = this.Reader[9].ToString();
                    feeItem.Item.MinFee.Name = this.Reader[10].ToString();
                    feeItem.Item.UserCode = this.Reader[11].ToString();
                    feeItem.Item.GBCode = this.Reader[12].ToString();
                    feeItem.ExecOrder.ExecOper.OperTime = DateTime.Parse(this.Reader[13].ToString());
                    feeDetails.Add(feeItem);
                    result = 1;
                }

            }
            catch (Exception e)
            {
                result = -1;
            }
            #endregion

            return result;
        }


        /// <summary>
        /// 插入住院登记信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>    
        /// <returns></returns>
        public int InsertYDInPatientReg(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            //医保基本信息保存到表fin_ipr_siinmaininfo
            //佛山医保扩展信息需要另外保存
            string balanceNo = this.GetBalanceNo(patientInfo.ID);
            if (string.IsNullOrEmpty(balanceNo))
            {
                balanceNo = "0";
            }
            balanceNo = (int.Parse(balanceNo) + 1).ToString();

            #region sql
            string strSqlV = @"update fin_ipr_siinmaininfo f
                              set f.VALID_FLAG='0'
                              where f.inpatient_no='{0}'";

            strSqlV = string.Format(strSqlV, patientInfo.ID);
            #endregion

            try
            {
                if (this.ExecNoQuery(strSqlV) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            #region sql
            string strSql = @"INSERT INTO fin_ipr_siinmaininfo f
                                (
                                inpatient_no,
                                reg_no,
                                balance_no,
                                patient_no,
                                card_no,
                                mcard_no,
                                name,
                                idenno,
                                clinic_diagnose,
                                paykind_code,
                                pact_code,
                                pact_name,
                                oper_code,
                                oper_date,
                                TOT_COST,
                                PUB_COST,
                                OWN_COST,
                                VALID_FLAG,
                                FEE_TIMES,
                                SEX_CODE,
                                DEPT_CODE,
                                IN_DATE,
                                BALANCE_DATE,
                                FOSHANSITYPE,
                                TYPE_CODE
                                --SY_FLAG,
                                --GS_FLAG                                
                                )
                                Values
                                (
                                '{0}',
                                '{1}',
                                '{2}',
                                '{3}',
                                '{4}',
                                '{5}',
                                '{6}',
                                '{7}',
                                '{8}',
                                '{9}',
                                '{10}',
                                '{11}',
                                '{12}',
                                to_date('{13}','YYYY-MM-DD hh24:mi:ss'),
                                '{14}',
                                '{15}',
                                '{16}',
                                '{17}',
                                0,
                                '{18}',
                                '{19}',
                                to_date('{20}','YYYY-MM-DD hh24:mi:ss'),
                                to_date('{21}','YYYY-MM-DD hh24:mi:ss'),
                                '{22}','{23}'
                            
                             
                                )";
            strSql = string.Format(strSql,
                patientInfo.ID,
                patientInfo.SIMainInfo.RegNo,
                balanceNo,
                patientInfo.PID.PatientNO,
                patientInfo.PID.CardNO,
                personInfo.MCardNo,
                personInfo.Name,
                personInfo.IdenNo,
                patientInfo.ClinicDiagnose,
                patientInfo.Pact.PayKind.ID,
                patientInfo.Pact.ID,
                patientInfo.Pact.Name,
                this.Operator.ID,
                patientInfo.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss"),
                patientInfo.SIMainInfo.TotCost,
                patientInfo.SIMainInfo.PubCost,
                patientInfo.SIMainInfo.OwnCost,
                "1",
                patientInfo.Sex.ID,
                patientInfo.PVisit.PatientLocation.Dept.ID,
                patientInfo.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss"),
                patientInfo.SIMainInfo.BalanceDate.ToString("yyyy-MM-dd HH:mm:ss"),
                personInfo.SIType, "2"

                );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }


            #region sql
            strSql = @"insert into fin_ipr_siinmaininfo_yd 
                        (INPATIENT_NO,   BALANCE_NO,              JZDJH,      DLDM,           DLMC, 
                        --'住院流水号'0; '结算序号'1;'就诊登记号'2; '大类代码'3; '大类名称'4;

                         MCARD_NO,       INSUREDCENTERAREACODE,   NAME,       SEX,            BIRTHDAY, 
                        --'医保编号'5; '参保地分中心编号'6; '姓名'7;'性别'8; '出生日期'9;

                         IDNO,           YLRYLB,                  AGE,        WORKPLACECODE,  WORKPLACENAME, 
                        --'证件号码'10; '医疗人员类别'11; '实际年龄'12;'单位编码'13;'单位名称'14;

                         JJLX,           DWLX,                    DSGX,       JFNX,           ZHYE, 
                        --'经济类型'15; '单位类型'16;'隶属关系'17; '缴费年限'18;'帐户余额'19;

                         QILJ,           BCYFQFX,                 JBYLBCZFXE, DBYLBCZFXE,     GWYBCZFXE, 
                        --'起付线累计'20; '本次应付起付线'21;'基本医疗本次支付限额'22; '大病医疗本次支付限额'23;'公务员本次支付限额'24;

                         JBYLTCLJ,       DBYLTCLJ,                IN_TIMES,   IN_STATE,       ECFYSPBZ, 
                        --'基本医疗统筹累计'25; '大病医疗统筹累计'26;'住院次数'27; '住院状态'28; '二次返院审批标志'29;

                         ZYBZ,           ZCJZDJH,                 ZCYYMC,     DJZRYYMC,       ZYLB, 
                        --'转院标志'30;'转出就诊登记号'31; '转出医院名称'32; '登记转入医院名称'33; '转院类别'34;

                         ZC_DATE,        ZZZD,                    ZZZYSPJG,   ZZYY,           InsuredAreaCode,
                        --'转出日期'35;'转诊诊断'36; '转诊转院审批结果'37;'转诊原因'38;参保地统筹区编码39;
                         nation,         ZJLX,                    xzlx,       GWYflag,        ZZJGDM,
                        --民族40;证件类型41;险种类型42;公务员标识43;组织机构代码44;
                         YDJYBAH,                person_type ,regTransID ,balanceTransID,out_transid,
                        --异地就医备案号45;      人员类型46   登记流水号47 结算流水号48   出院登记号49
                        seedoctype,oper_date,BNDYLJZLJJE,      BCYLJZZFXE
                        --就诊类别50  操作时间   本年度医疗救助累计金额51   本次医疗救助支付限额52
                        )
                       values 
                        ('{0}', '{1}',  '{2}',  '{3}',  '{4}',  '{5}',  '{6}',  '{7}',  '{8}',  to_date('{9}','yyyy-MM-dd'), '{10}', 
                        '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', 
                        '{21}', '{22}', '{23}', '{24}', '{25}', '{26}', '{27}', '{28}', '{29}', '{30}', 
                        '{31}', '{32}', '{33}', '{34}', to_date('{35}','yyyy-MM-dd'), '{36}', '{37}', '{38}','{39}','{40}',
                        '{41}', '{42}', '{43}', '{44}', '{45}', '{46}','{47}','{48}','{49}','{50}',sysdate,'{51}','{52}')

";

            if (personInfo.PubRate.Trim().Length == 0)
            {
                personInfo.PubRate = "0";
            }
            if (personInfo.RQtype.Length < 2)
            {
                personInfo.RQtype = "0" + personInfo.RQtype;
            }
            strSql = string.Format(strSql, patientInfo.ID, balanceNo, personInfo.ClinicNo, "", "",
                //医保编号5,参保地分中心编号6,姓名7,性别8，出生日期9
                personInfo.MCardNo, personInfo.InsuredCenterAreaCode, personInfo.Name, personInfo.Sex, personInfo.Birth,
                //证件号码10
                personInfo.IdenNo,
                //医疗人员类别11
                personInfo.PersonType,
                //人群类别
                //personInfo.RQtype ,
                //实际年龄12
                personInfo.Age ,
                //单位编码13
                personInfo.CompanyCode,
                //单位名称14
                personInfo.CompanyName,
                //经济类型15
                personInfo.EconomicType,
                //单位类型16  隶属关系17
                personInfo.CompanyType,"",
                //缴费年限18
                personInfo.PayYears,
                //帐户余额19
                personInfo.RestAccount,
                //起付线累计20
                personInfo.SumCostLine,
                //本次应付起付线21
                personInfo.CostLine,
                //基本医疗本次支付限额22
                personInfo.LimitCostJBYL,
                //大病医疗本次支付限额23
                personInfo.LimitCostDBYL,
                //公务员本次支付限额24
                personInfo.LimitCostGWY,
                //基本医疗统筹累计25
                personInfo.SumCostJBYL,
                //大病医疗统筹累计26
                personInfo.SumCostDBYL,
                //住院次数27
                personInfo.InTimes,
                //住院状态28
                personInfo.InState,
                //二次返院审批标志29
                personInfo.ReturnsFlag,
                //转院标志30
                personInfo.ChangeOutFlag,
                //转出就诊登记号31
                personInfo.ChangeOutClinicNo,
                //转出医院名称32
                personInfo.ChangeOutHosName,
                //登记转入医院名称33
                personInfo.ChangeInHosName,
                //转院类别34
                personInfo.ChangeType,
                //转出日期35
                personInfo.ChangeDate,
                //转诊诊断36
                personInfo.ChangeDiagn,
                //转诊转院审批结果37
                personInfo.ChangePassFlag,
                //转诊原因38
                personInfo.ChangeReason,
                //参保地统筹区编码39
                personInfo.InsuredAreaCode,
                //民族
                personInfo.nation,
                //证件类型
                personInfo.ZJLX,
                //xzlx
                personInfo.xzlx,
                //公务员标识
                personInfo.GWYflag,
                //组织机构代码
                personInfo.ZZJGDM,
                //异地就医备案号
                personInfo.YDJYBAH,
                //人员类型
                personInfo.PersonType,
                //登记流水号
                personInfo.regTransID,
                //结算流水号
                personInfo.balanceTransID,
                //出院登记流水号
                personInfo.Out_TransID,
                //就诊类别
                personInfo.SeeDocType,
                //本年度医疗救助累计金额
                personInfo.BNDYLJZLJJE,
                //本次医疗救助支付限额
                personInfo.BCYLJZZFXE

                );
            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            strSql = @"UPDATE FIN_IPR_INMAININFO I
   SET I.MCARD_NO = '{1}',I.PAYKIND_CODE='02'
 WHERE I.INPATIENT_NO = '{0}'";
            strSql = string.Format(strSql, patientInfo.ID, patientInfo.SSN);
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }


            patientInfo.SIMainInfo.BalNo = balanceNo;

            return 1;
        }


        /// <summary>
        /// 【异地】查询入院登记信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public int QueryInPatientRegInfo(string inpatientNo, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            int result = 0;

            #region 查询赋值
            string strSql = @"select 
                                mcard_no,
                                name,
                                idenno,
                                TYPE_CODE
                         from fin_ipr_siinmaininfo f
                         where f.inpatient_no='{0}'
                         and f.balance_no = (select max(f.balance_no)
                                            from fin_ipr_siinmaininfo f
                                            where f.inpatient_no='{0}')

                             ";
            strSql = string.Format(strSql, inpatientNo);

            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    if (personInfo == null)
                    {
                        personInfo = new FoShanYDSI.Objects.SIPersonInfo();
                    }
                    personInfo.MCardNo = this.Reader[0].ToString();
                    personInfo.Name = this.Reader[1].ToString();
                    personInfo.IdenNo = this.Reader[2].ToString();
                    personInfo.SIType = this.Reader[3].ToString();
                    result = 1;
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception e)
            {
            }

            string strSql2 = @"select JZDJH, --就诊登记号';       
                           MCARD_NO, --'医保编号'; 
                           INSUREDCENTERAREACODE, --'参保地分中心编号';
                           NAME, --'姓名';
                           SEX, --'性别'; 
                           BIRTHDAY, -- '出生日期';       
                           IDNO, --'证件号码'; 
                           YLRYLB, --'医疗人员类别'; 
                           AGE, --'实际年龄';
                           WORKPLACECODE, --'单位编码';
                           WORKPLACENAME, --'单位名称';    10   
                           JJLX, --'经济类型'; 
                           DWLX, --'单位类型';
                           JFNX, --'单位类型';
                           ZHYE, --'帐户余额';       
                           QILJ, --'起付线累计';
                           BCYFQFX, -- '本次应付起付线';
                           JBYLBCZFXE, -- '基本医疗本次支付限额';
                           DBYLBCZFXE, -- '大病医疗本次支付限额';'
                           GWYBCZFXE, --   '公务员本次支付限额';       
                           JBYLTCLJ, --'基本医疗统筹累计'; 
                           DBYLTCLJ, -- '大病医疗统筹累计';
                           IN_TIMES, --  '住院次数';
                           IN_STATE, -- '住院状态'; 23
                           ECFYSPBZ, --  '二次返院审批标志';
                           ZYBZ, --'转院标志';
                           ZCJZDJH, -- '转出就诊登记号';
                           ZCYYMC, --  '转出医院名称';
                           DJZRYYMC, --'登记转入医院名称'; 
                           ZYLB, --  '转院类别';
                           ZC_DATE, --'转出日期';'  30
                           ZZZD, --转诊诊断';'
                           ZZZYSPJG, -- 转诊转院审批结果';
                           ZZYY, --  '转诊原因';
                           TOT_COST, --费用总额
                           OWN_COST, --自费部分;
                           PAY_COST, -- 自付部分;
                           PUB_COST, --允许报销部分;
                           GRZF_COST, -- 个人支付金额;
                           LIMIT_COST, --起付标准;            
                           GZZF_COST, --个人账户支付金额;  40 
                           CBXXEZF_COST, --超报销限额自付金额;
                           YBTCZF_COST, --医保统筹支付总额
                           TCJJZF_COST, --;统筹基金支付;
                           JBYLTCZF_COST, --基本医疗统筹自付部分 
                           DBYLTCZF_COST, --大病医疗统筹支付部分;
                           DBYLTCZIFU_COST, --大病医疗统筹自付部分;
                           GWYBZ_COST, --公务员补助;
                           GWYDB_COST, --公务员大病;
                           YLBZBF_COST, --医疗补助部分;       
                           GWYCXBZ_COST, --公务员超限补助部分;
                           QTBZZF_COST, --其它补助支付; 51
                           JSYWH, --结算业务号;
                           MEMO, --自费原因;
                           SUMCOSTJBYL, --统筹累计已支付;
                           BCYLLJYZF_COST, --补充医疗累计已支付;
                           GWYBCLJYZF_COST, --公务员补助累计已支付;
                           GFDGRZF_COST, --共付段个人支付
                           INSUREDAREACODE, --参保地统筹区编码,
                           NATION, --民族       
                           ZJLX, --证件类型;
                           XZLX, --险种类型
                           GWYFLAG, --公务员标识 62
                           ZZJGDM, --组织机构代码
                           YDJYBAH, --异地就医备案号
                           BALANCE_FLAG, --结算标志
                           JYLSH,--交易流水号  65
                           Balance_No, --67
                           PERSON_TYPE,
                           DIAGN1,
                           DIAGN2, --70
                           DIAGN3,
                           GRZIFUJE, --个人自负金额,其他支付,  
                           QTZF, --
                           ZYFYJSFS, --主要费用结算方式
                           QTLJ, --其他累计
                           DBBXYLTCLJ,
                           GWYBZZF_COST,
                           regTransID, --登记流水号
                           balanceTransID, --结算流水号
                           out_transid, --出院登记流水号  80
                           INPATIENT_NO, --住院流水号
                           seedoctype, --就诊类别
                           person_type, --人员类别
                           oper_date, -- 84
                           bndyljzljje, --本年度医疗救助累计金额 
                           bcyljzzfxe, --本次医疗救助支付限额 
                           oper_sate, --操作类型，0登记，1费用上传，2出院登记，3出院结算,-1取消住院登记 
                           akb068, --统筹支付金额合计 
                           ykc751, --救助对象类型 89
                           YKC641,--医疗救助金额  90
                           ykc642, --二次救助金额  91
                           ykc752, --一至六级残疾军人等医疗补助  92
                           ykc753 --本年度医疗救助累计金额 
                      from fin_ipr_siinmaininfo_yd
                     where inpatient_no = '{0}'
                       and balance_no = (select max(f.balance_no)
                                           from fin_ipr_siinmaininfo_yd f
                                          where f.inpatient_no = '{0}')

              
                           ";
            strSql = string.Format(strSql2, inpatientNo);
             try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    personInfo.ClinicNo = this.Reader[0].ToString();
                    //医保编号5,参保地分中心编号6,姓名7,性别8，出生日期9
                    personInfo.SIcode = this.Reader[1].ToString();
                    personInfo.InsuredCenterAreaCode = this.Reader[2].ToString();
                    personInfo.Name = this.Reader[3].ToString();
                    personInfo.Sex = this.Reader[4].ToString();
                    personInfo.Birth = this.Reader[5].ToString();
                    //证件号码10
                    personInfo.IdenNo = this.Reader[6].ToString();
                    //人群类别
                    personInfo.RQtype = this.Reader[7].ToString();
                    //实际年龄12
                    personInfo.Age = this.Reader[8].ToString();
                    //单位编码13
                    personInfo.CompanyCode = this.Reader[9].ToString();
                    //单位名称14
                    personInfo.CompanyName = this.Reader[10].ToString();
                    //经济类型15
                    personInfo.EconomicType = this.Reader[11].ToString();
                    //单位类型16  隶属关系17
                    personInfo.CompanyType = this.Reader[12].ToString();
                    //缴费年限18
                    personInfo.PayYears = this.Reader[13].ToString();
                    //帐户余额19
                    personInfo.RestAccount = this.Reader[14].ToString();
                    //起付线累计20
                    personInfo.SumCostLine = this.Reader[15].ToString();
                    //本次应付起付线21
                    personInfo.CostLine = this.Reader[16].ToString();
                    //基本医疗本次支付限额22
                    personInfo.LimitCostJBYL = this.Reader[17].ToString();
                    //大病医疗本次支付限额23
                    personInfo.LimitCostDBYL = this.Reader[18].ToString();
                    //公务员本次支付限额24
                    personInfo.LimitCostGWY = this.Reader[19].ToString();
                    //基本医疗统筹累计25
                    personInfo.SumCostJBYL = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                    //大病医疗统筹累计26
                    personInfo.SumCostDBYL = this.Reader[21].ToString();
                    //住院次数27
                    personInfo.InTimes = this.Reader[22].ToString();
                    //住院状态28
                    personInfo.InState = this.Reader[23].ToString();
                    //二次返院审批标志29
                    personInfo.ReturnsFlag = this.Reader[24].ToString();
                    //转院标志30
                    personInfo.ChangeOutFlag = this.Reader[25].ToString();
                    //转出就诊登记号31
                    personInfo.ChangeOutClinicNo = this.Reader[26].ToString();
                    //转出医院名称32
                    personInfo.ChangeOutHosName = this.Reader[27].ToString();
                    //登记转入医院名称33
                    personInfo.ChangeInHosName = this.Reader[28].ToString();
                    //转院类别34
                    personInfo.ChangeType = this.Reader[29].ToString();
                    //转出日期35
                    personInfo.ChangeDate = this.Reader[30].ToString();
                    //转诊诊断36
                    personInfo.ChangeDiagn = this.Reader[31].ToString();
                    //转诊转院审批结果37
                    personInfo.ChangePassFlag = this.Reader[32].ToString();
                    //转诊原因38
                    personInfo.ChangeReason = this.Reader[33].ToString();
                    //费用总额
                    personInfo.tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[34].ToString());
                    //自费部分;自付部分;允许报销部分;个人支付金额;起付标准; 
                    personInfo.own_cost_part = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[35].ToString());
                    personInfo.pay_cost_part = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());
                    personInfo.pub_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[37].ToString());
                    personInfo.GRZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[38].ToString());
                    personInfo.limit_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[39].ToString());
                    //个人账户支付金额;超报销限额自付金额;医保统筹支付总额;统筹基金支付;基本医疗统筹自付部分
                    personInfo.GZZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[40].ToString());
                    personInfo.CBXXEZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[41].ToString());
                    personInfo.YBTCZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[42].ToString());
                    personInfo.TCJJZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[43].ToString());
                    personInfo.JBYLTCZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[44].ToString());
                    //大病医疗统筹支付部分;大病医疗统筹自付部分;公务员补助;公务员大病;医疗补助部分;
                    personInfo.DBYLTCZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[45].ToString());
                    personInfo.DBYLTCZIFU_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[46].ToString());
                    personInfo.GWYBZ_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[47].ToString());
                    personInfo.GWYDB_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[48].ToString());
                    personInfo.YLBZBF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[49].ToString());
                    //公务员超限补助部分;其它补助支付;结算业务号;自费原因;统筹累计已支付;
                    personInfo.GWYCXBZ_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[50].ToString());
                    personInfo.QTBZZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[51].ToString());
                    personInfo.JSYWH = this.Reader[52].ToString();
                    personInfo.Memo = this.Reader[53].ToString();
                    personInfo.SumCostJBYL = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[54].ToString());
                    //补充医疗累计已支付;公务员补助累计已支付;共付段个人支付,参保地统筹区编码,民族,
                    personInfo.BCYLLJYZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[55].ToString());
                    personInfo.GWYBCLJYZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[56].ToString());
                    personInfo.GFDGRZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[57].ToString());
                    personInfo.InsuredAreaCode = this.Reader[58].ToString();
                    personInfo.nation = this.Reader[59].ToString();
                    //证件类型;险种类型;公务员标识;组织机构代码;异地就医备案号;
                    personInfo.ZJLX = this.Reader[60].ToString();
                    personInfo.xzlx = this.Reader[61].ToString();
                    personInfo.GWYflag = this.Reader[62].ToString();
                    personInfo.ZZJGDM = this.Reader[63].ToString();
                    personInfo.YDJYBAH = this.Reader[64].ToString();
                    //结算标志,交易流水号,结算序号
                    personInfo.BalanceFlag = this.Reader[65].ToString();
                    personInfo.jylsh = this.Reader[66].ToString();
                    personInfo.BalanceNo = this.Reader[67].ToString();
                    personInfo.PersonType = this.Reader[68].ToString();
                    personInfo.Diagn1 = this.Reader[69].ToString();
                    personInfo.Diagn2 = this.Reader[70].ToString();
                    personInfo.Diagn3 = this.Reader[71].ToString();
                    personInfo.GRZIFUJE = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[72].ToString());
                    personInfo.QTZF = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[73].ToString());
                    personInfo.ZYFYJSFS = this.Reader[74].ToString();
                    personInfo.qtlj = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[75].ToString());
                    personInfo.DBBXYLTCLJ = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[76].ToString());
                    personInfo.GWYBZZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[77].ToString());
                    personInfo.regTransID = this.Reader[78].ToString();
                    personInfo.balanceTransID = this.Reader[79].ToString();
                    personInfo.Out_TransID = this.Reader[80].ToString();
                    personInfo.InPatient_No = this.Reader[81].ToString();
                    personInfo.SeeDocType = this.Reader[82].ToString();
                    //人员类别 ykc021
                    personInfo.PersonType = this.Reader[83].ToString();
                    //
                    personInfo.oper_date = this.Reader[84].ToString();
                    personInfo.BNDYLJZLJJE = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[85].ToString());
                    personInfo.BCYLJZZFXE = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[86].ToString());
                    personInfo.oper_State = this.Reader[87].ToString();
                    personInfo.akb068 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[88].ToString());
                    personInfo.ykc751 = this.Reader[89].ToString();
                    personInfo.JZDXLX = this.Reader[89].ToString();
                    personInfo.ykc641 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[90].ToString());
                    personInfo.ykc642 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[91].ToString());
                    personInfo.ykc752 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[92].ToString());
                    personInfo.ykc753 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[93].ToString());
                    result = 1;
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception e)
            {
            }
            #endregion

            return result;
        }
        /// <summary>
        /// 更新医保主表有效
        /// 取消住院登记用
        /// 取消住院结算用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <param name="validFlag"></param>
        /// <returns></returns>
        public int UpdateSiinmaininfoYDout_transid(FS.HISFC.Models.RADT.PatientInfo patientInfo, string out_transid)
        {
            string strSql2 = @"update FIN_IPR_SIINMAININFO_YD
                               set out_transid = '{0}'
                              where inpatient_no = '{1}' ";

            strSql2 = string.Format(strSql2, out_transid, patientInfo.ID);
            try
            {
                if (this.ExecNoQuery(strSql2) < 0)
                {
                    this.Err = this.ErrorException;
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }
            return 1;
        }
       
        /// <summary>
        /// 更新医保主表有效
        /// 取消住院登记用
        /// 取消住院结算用
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <param name="validFlag"></param>
        /// <returns></returns>
        public int UpdateSiinmaininfoValidFlag(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo, string validFlag)
        {
            string strSql2 = @"update FIN_IPR_SIINMAININFO_YD
                               set balance_flag = '{0}'
                              where inpatient_no = '{1}' ";

            string strSql = @"update fin_ipr_siinmaininfo
                                 set valid_flag = '{0}',BALANCE_STATE = '{0}', OPER_DATE = SYSDATE
                               where inpatient_no = '{1}'
                                -- and BALANCE_NO='{3}'
                                 --and reg_no = '{2}'                              
                               ";
            //string q = patientInfo.SIMainInfo.BalNo;// BALANCE_NO没有取到
            strSql = string.Format(strSql, validFlag, patientInfo.ID, patientInfo.SIMainInfo.RegNo, personInfo.BalanceNo);
            strSql2 = string.Format(strSql2, validFlag, patientInfo.ID);
            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    this.Err = this.ErrorException;
                    return -1;
                }
                if (this.ExecNoQuery(strSql2) < 0)
                {
                    this.Err = this.ErrorException;
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// 插入医保信息【住院】
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <param name="outBalanceInfo"></param>
        /// <returns></returns>
        public int InsertSIMainInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            //医保基本信息保存到表fin_ipr_siinmaininfo
            //佛山医保扩展信息需要另外保存
            string balanceNo = this.GetBalanceNo(patientInfo.ID);
            if (string.IsNullOrEmpty(balanceNo))
            {
                balanceNo = "0";
            }
            balanceNo = (int.Parse(balanceNo) + 1).ToString();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region sql
            string strSql = @"INSERT INTO fin_ipr_siinmaininfo f
                                (
                                inpatient_no,
                                reg_no,
                                balance_no,
                                patient_no,
                                card_no,
                                mcard_no,
                                name,
                                idenno,
                                clinic_diagnose,
                                paykind_code,
                                pact_code,
                                pact_name,
                                oper_code,
                                oper_date,
                                TOT_COST,
                                PUB_COST,
                                OWN_COST,
                                VALID_FLAG,
                                FEE_TIMES,
                                SEX_CODE,
                                DEPT_CODE,
                                IN_DATE,
                                BALANCE_DATE,
                                FOSHANSITYPE, BALANCE_STATE,TYPE_CODE                                                             
                                )
                                Values
                                (
                                '{0}',
                                '{1}',
                                '{2}',
                                '{3}',
                                '{4}',
                                '{5}',
                                '{6}',
                                '{7}',
                                '{8}',
                                '{9}',
                                '{10}',
                                '{11}',
                                '{12}',
                                to_date('{13}','YYYY-MM-DD hh24:mi:ss'),
                                '{14}',
                                '{15}',
                                '{16}',
                                '{17}',
                                0,
                                '{18}',
                                '{19}',
                                to_date('{20}','YYYY-MM-DD hh24:mi:ss'),
                                to_date('{21}','YYYY-MM-DD hh24:mi:ss'),
                                '{22}' , '{23}', '{24}'
                                                                  
                                )";
            strSql = string.Format(strSql,
                patientInfo.ID,
                patientInfo.SIMainInfo.RegNo,
                balanceNo,
                patientInfo.PID.PatientNO,
                patientInfo.PID.CardNO,
                personInfo.MCardNo,
                personInfo.Name,
                personInfo.IdenNo,
                personInfo.OutDiseaseType,
                patientInfo.Pact.PayKind.ID,
                patientInfo.Pact.ID,
                patientInfo.Pact.Name,
                this.Operator.ID,
                patientInfo.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss"),
                patientInfo.SIMainInfo.TotCost,
                patientInfo.SIMainInfo.PubCost,
                patientInfo.SIMainInfo.OwnCost,
                "1",
                patientInfo.Sex.ID,
                patientInfo.PVisit.PatientLocation.Dept.ID,
                patientInfo.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss"),
                patientInfo.SIMainInfo.BalanceDate.ToString("yyyy-MM-dd HH:mm:ss"),
                personInfo.SIType, "1", "2"
                );

            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            #region sql
            strSql = @"select distinct person_type,DIAGN1,DIAGN2,DIAGN3  from fin_ipr_siinmaininfo_yd d 
                       where d.INPATIENT_NO = '{0}' ";
            strSql = string.Format(strSql, patientInfo.ID);
            #endregion
            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    personInfo.PersonType = this.Reader[0].ToString();
                    if (!string.IsNullOrEmpty(this.Reader[1].ToString()))
                    {
                        personInfo.Diagn1 = this.Reader[1].ToString();
                    }
                    if (!string.IsNullOrEmpty(this.Reader[2].ToString()))
                    {
                        personInfo.Diagn2 = this.Reader[2].ToString();
                    }
                    if (!string.IsNullOrEmpty(this.Reader[3].ToString()))
                    {
                        personInfo.Diagn3 = this.Reader[3].ToString();
                    }
                }
            }
            catch (Exception e)
            {
            }
            

            #region sql
            strSql = @"insert into fin_ipr_siinmaininfo_yd 
                        (
                         INPATIENT_NO,      BALANCE_NO,         JZDJH,       DLDM,        DLMC,
                         MCARD_NO,     INSUREDCENTERAREACODE,   NAME,        SEX,         BIRTHDAY, 
                         IDNO,              YLRYLB,             AGE,     WORKPLACECODE,   WORKPLACENAME, 
                         JJLX,              DWLX,               DSGX,       JFNX,         ZHYE, 
                         QILJ,              BCYFQFX,          JBYLBCZFXE,   DBYLBCZFXE,   GWYBCZFXE, 
                         JBYLTCLJ,          DBYLTCLJ,          IN_TIMES,    IN_STATE,     ECFYSPBZ, 
                         ZYBZ,              ZCJZDJH,           ZCYYMC,      DJZRYYMC,     ZYLB, 
                         ZC_DATE,           ZZZD,              ZZZYSPJG,    ZZYY,         TOT_COST,
                         OWN_COST,         PAY_COST,           PUB_COST,    GRZF_COST,    LIMIT_COST,  
                         GZZF_COST,        CBXXEZF_COST,       YBTCZF_COST, TCJJZF_COST,  JBYLTCZF_COST, 
                         DBYLTCZF_COST,    DBYLTCZIFU_COST,    GWYBZ_COST,  GWYDB_COST,   YLBZBF_COST, 
                         GWYCXBZ_COST,     QTBZZF_COST,        JSYWH,       MEMO,         SUMCOSTJBYL, 
                         BCYLLJYZF_COST,   GWYBCLJYZF_COST,    GFDGRZF_COST,BALANCE_FLAG, InsuredAreaCode,
                         nation,           ZJLX,               xzlx,        GWYflag,      ZZJGDM,
                         YDJYBAH,          GRZIFUJE,           QTZF,        ZYFYJSFS,     QTLJ,
                         DBBXYLTCLJ,       GWYBZZF_COST,       person_type, Diagn1,       Diagn2,
                         Diagn3   ,        regTransID,         balanceTransID     ,out_transid , SEEDOCTYPE,oper_date,
                         Oper_Sate,akb068,ykc751,ykc641,ykc642,ykc752,ykc753)
                        values  
                        ( 
                         '{0}', '{1}', '{2}', '{3}', '{4}',
                         '{5}', '{6}', '{7}', '{8}', to_date('{9}','yyyy-MM-dd hh24:mi:ss'),
                         '{10}', '{11}', '{12}', '{13}', '{14}',
                         '{15}', '{16}', '{17}', '{18}', '{19}',
                         '{20}', '{21}', '{22}', '{23}', '{24}',
                         '{25}', '{26}', '{27}', '{28}', '{29}',
                         '{30}', '{31}', '{32}', '{33}', '{34}',
                         to_date('{35}','yyyy-MM-dd hh24:mi:ss'), '{36}', '{37}', '{38}', '{39}',
                         '{40}', '{41}', '{42}', '{43}', '{44}',
                         '{45}', '{46}', '{47}', '{48}', '{49}',
                         '{50}', '{51}', '{52}', '{53}', '{54}',
                         '{55}', '{56}', '{57}', '{58}', '{59}',
                         '{60}', '{61}', '{62}', '{63}', '{64}',
                         '{65}', '{66}', '{67}', '{68}', '{69}',
                         '{70}', '{71}', '{72}', '{73}', '{74}', 
                         '{75}', '{76}', '{77}', '{78}', '{79}',
                         '{80}', '{81}', '{82}', '{83}', '{84}',sysdate,
                         '{85}', '{86}', '{87}', '{88}' ,'{89}','{90}','{91}'
                         )
                        ";
            personInfo.OverOwnCost = string.IsNullOrEmpty(personInfo.OverOwnCost) ? "0" : personInfo.OverOwnCost;
            //personInfo.GRZIFUJE = string.IsNullOrEmpty(personInfo.GRZIFUJE) ? "0" : personInfo.GRZIFUJE;
            personInfo.RestAccount = string.IsNullOrEmpty(personInfo.RestAccount) ? "0" : personInfo.RestAccount;
            personInfo.SumCostLine = string.IsNullOrEmpty(personInfo.SumCostLine) ? "0" : personInfo.SumCostLine;
            personInfo.CostLine = string.IsNullOrEmpty(personInfo.CostLine) ? "0" : personInfo.CostLine;
            personInfo.LimitCostJBYL = string.IsNullOrEmpty(personInfo.LimitCostJBYL) ? "0" : personInfo.LimitCostJBYL;
            personInfo.LimitCostDBYL = string.IsNullOrEmpty(personInfo.LimitCostDBYL) ? "0" : personInfo.LimitCostDBYL;
            personInfo.LimitCostGWY = string.IsNullOrEmpty(personInfo.LimitCostGWY) ? "0" : personInfo.LimitCostGWY;
            personInfo.SumCostDBYL = string.IsNullOrEmpty(personInfo.SumCostDBYL) ? "0" : personInfo.SumCostDBYL;
            personInfo.BalanceFlag = (personInfo.BalanceFlag=="1") ? "1" : "0";
            
            strSql = string.Format(strSql, patientInfo.ID, balanceNo, personInfo.ClinicNo,"","",
                                   personInfo.SIcode, personInfo.InsuredCenterAreaCode, personInfo.Name, personInfo.Sex, personInfo.Birth,
                                   personInfo.IdenNo, personInfo.RQtype, personInfo.Age, personInfo.CompanyCode, personInfo.CompanyName,
                                   personInfo.EconomicType, personInfo.CompanyType, "", personInfo.PayYears, personInfo.RestAccount,
                                   personInfo.SumCostLine, personInfo.CostLine, personInfo.LimitCostJBYL, personInfo.LimitCostDBYL, personInfo.LimitCostGWY,
                                   personInfo.SumCostJBYL, personInfo.SumCostDBYL, personInfo.InTimes, personInfo.InState, personInfo.ReturnsFlag,
                                   personInfo.ChangeOutFlag, personInfo.ChangeOutClinicNo, personInfo.ChangeOutHosName, personInfo.ChangeInHosName, personInfo.ChangeType,
                                   personInfo.ChangeDate, personInfo.ChangeDiagn, personInfo.ChangePassFlag, personInfo.ChangeReason,personInfo.tot_cost,
                                   personInfo.own_cost_part, personInfo.pay_cost_part, personInfo.pub_cost, personInfo.GRZF_cost, personInfo.limit_cost,
                                   personInfo.GZZF_cost, personInfo.CBXXEZF_cost, personInfo.YBTCZF_cost, personInfo.TCJJZF_cost, personInfo.YLBZBF_cost,
                                   personInfo.DBYLTCZF_cost, personInfo.DBYLTCZIFU_cost, personInfo.GWYBZ_cost, personInfo.GWYDB_cost, personInfo.YLBZBF_cost,
                                   personInfo.GWYCXBZ_cost, personInfo.QTBZZF_cost, personInfo.JSYWH, personInfo.Memo, personInfo.SumCostJBYL,
                                   personInfo.BCYLLJYZF_cost, personInfo.GWYBCLJYZF_cost, personInfo.GFDGRZF_cost,personInfo.BalanceFlag,personInfo.InsuredAreaCode,
                                   //民族//证件类型 //xzlx//公务员标识//组织机构代码
                                   personInfo.nation, personInfo.ZJLX, personInfo.xzlx, personInfo.GWYflag, personInfo.ZZJGDM,
                                   //异地就医备案号
                                   personInfo.YDJYBAH, personInfo.GRZIFUJE, personInfo.QTZF,personInfo.ZYFYJSFS,personInfo.qtlj,
                                   personInfo.DBBXYLTCLJ, personInfo.GWYBZZF_cost,personInfo.PersonType,personInfo.Diagn1, personInfo.Diagn2,
                                   personInfo.Diagn3, personInfo.regTransID, personInfo.balanceTransID, personInfo.Out_TransID,personInfo.SeeDocType,
                                   "3", personInfo.akb068, personInfo.ykc751, personInfo.ykc641, personInfo.ykc642, personInfo.ykc752, personInfo.ykc753
                );
            
            #endregion

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            patientInfo.SIMainInfo.BalNo = balanceNo;
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }


        /// <summary>
        /// 查询结算信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public int QuerySiBalanceInfo(ref FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            int result = 0;

            #region 查询赋值
            string strSql = @"SELECT I.FOSHANSITYPE, I.BALANCE_NO,Z.BALANCE_FLAG,Z.BLANCETRANSID,Z.OUT_TRANSID
  FROM FIN_IPR_SIINMAININFO_YD Z, FIN_IPR_SIINMAININFO I
 WHERE Z.BALANCE_NO = I.BALANCE_NO
   AND Z.INPATIENT_NO = I.INPATIENT_NO
   AND I.FOSHANSITYPE IS NOT NULL
   AND I.BALANCE_NO = (SELECT TO_CHAR(MAX(TO_NUMBER(SI.BALANCE_NO)))
                         FROM FIN_IPR_SIINMAININFO SI
                        WHERE SI.VALID_FLAG = '1'
                          AND SI.INPATIENT_NO = '{0}')
   AND I.VALID_FLAG = '1'
   AND I.INPATIENT_NO = '{0}'
  ";
            strSql = string.Format(strSql, patientInfo.ID);

            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    if (personInfo == null)
                    {
                        personInfo = new FoShanYDSI.Objects.SIPersonInfo();
                    }

                    personInfo.SIType = Reader[0].ToString();
                    personInfo.BalanceNo = Reader[1].ToString();
                    personInfo.BalanceFlag = Reader[2].ToString();
                    personInfo.balanceTransID = Reader[3].ToString();
                    personInfo.Out_TransID = Reader[4].ToString();
                    result = 1;
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception e)
            {
                return result;
            }
            if (!personInfo.BalanceFlag.Equals("1"))
            {
                return result;
            }
            #endregion

            #region 查询结算信息
            strSql = @"SELECT I.TOT_COST, I.PUB_COST, I.PAY_COST, I.OWN_COST
  FROM FIN_IPR_SIINMAININFO I
 WHERE I.INPATIENT_NO = '{0}'
   AND I.BALANCE_NO = '{1}'
  ";
            strSql = string.Format(strSql, patientInfo.ID, personInfo.BalanceNo);
            result = -1;
            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    if (personInfo == null)
                    {
                        personInfo = new FoShanYDSI.Objects.SIPersonInfo();
                    }

                    if (Reader[0].ToString().Trim().Length > 0)
                    {
                        patientInfo.SIMainInfo.TotCost = Convert.ToDecimal(Reader[0].ToString());
                    }
                    if (Reader[1].ToString().Trim().Length > 0)
                    {
                        patientInfo.SIMainInfo.PubCost = Convert.ToDecimal(Reader[1].ToString());
                    }
                    if (Reader[2].ToString().Trim().Length > 0)
                    {
                        patientInfo.SIMainInfo.PayCost = Convert.ToDecimal(Reader[2].ToString());
                    }
                    if (Reader[3].ToString().Trim().Length > 0)
                    {
                        patientInfo.SIMainInfo.OwnCost = Convert.ToDecimal(Reader[3].ToString());
                    }

                    result = 1;
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception e)
            {
                return result;
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 插入住院结算项目表
        /// </summary>
        /// <param name="arrCenterBalanceItem"></param>
        /// <returns></returns>
        public int InsertBalanceType(ArrayList arrCenterBalanceItem)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (FoShanYDSI.Objects.SICenterBalanceItem obj in arrCenterBalanceItem)
            {
                string strSql = @"INSERT INTO FOSHAN_BALANCE_TYPE
  (SI_TYPE, ITEM_NAME, ITEM_CODE, BGN_TIME, END_TIME, BALANCE_TYPE,BALANCE_TYPE_NAME,OPER_DATE,SI_NAME)
VALUES
  ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}',SYSDATE,'{7}')
";
                strSql = string.Format(strSql, obj.SiType, obj.ItemName, obj.ItemCode, obj.BgnTime, obj.EndTime, obj.BalanceType, obj.BalanceTypeName, obj.SiName);

                try
                {
                    if (this.ExecNoQuery(strSql) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
                catch (Exception e)
                {
                    this.Err = e.Message;
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 删除住院结算项目表所有条目
        /// </summary>
        /// <param name="arrCenterBalanceItem"></param>
        /// <returns></returns>
        public int DeleteBalanceType()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            string strSql = @"DELETE FOSHAN_BALANCE_TYPE";

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 查询住院结算项目表
        /// </summary>
        /// <param name="arrCenterBalanceItem"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int QueryBalanceType(ref ArrayList arrCenterBalanceItem, ref DataTable dt)
        {
            DataSet ds = new DataSet();
            string strSql = @"SELECT B.SI_TYPE      AS 参保险种,
       B.SI_NAME AS 参保险种名称,
       B.ITEM_NAME AS 项目名称,
       B.ITEM_CODE AS 项目代码,
       B.BGN_TIME AS 有效开始日期,
       B.END_TIME AS 有效结束日期,
       B.BALANCE_TYPE AS 结算类型,
       B.BALANCE_TYPE_NAME AS 结算类型名称,
       TO_CHAR(B.OPER_DATE,'YYYY-MM-DD HH24:MI:SS') AS 下载时间
  FROM FOSHAN_BALANCE_TYPE B
";
            int intFlag = this.ExecQuery(strSql, ref ds);
            if (intFlag < 0)
            {
                this.Err = "";
                return -1;
            }
            dt = ds.Tables[0].Copy();

            arrCenterBalanceItem.Clear();
            FoShanYDSI.Objects.SICenterBalanceItem obj;
            while (this.Reader.Read())
            {
                obj = new FoShanYDSI.Objects.SICenterBalanceItem();
                obj.SiType = Reader[0].ToString();
                obj.SiName = Reader[1].ToString();
                obj.ItemName = Reader[2].ToString();
                obj.ItemCode = Reader[3].ToString();
                obj.BgnTime = Reader[4].ToString();
                obj.EndTime = Reader[5].ToString();
                obj.BalanceType = Reader[6].ToString();
                obj.BalanceTypeName = Reader[7].ToString();

                arrCenterBalanceItem.Add(obj);
            }
            return 1;
        }
        
        /// <summary>
        /// 查询住院病案首页内容
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="inTimes"></param>
        /// <returns></returns>
        public DataTable QueryInpatientCase(string patientNo, string inTimes)
        {
            string strSql = @"
                            SELECT yzy001, --  病案号
                                   yzy002, --  住院次数
                                   yzy003, --  ICD版本
                                   yzy004, --  住院流水号
                                   akc023, --  年龄
                                   aac003, --  病人姓名
                                   aac004, --  性别编号
                                   yzy008, --  性别
                                   aac006, --  出生日期
                                   yzy010, --  出生地
                                   yzy011, --  身份证号
                                   aac161, --  国籍编号
                                   yzy013, --  国籍
                                   aac005, --  民族编号
                                   yzy015, --  民族
                                   yzy016, --  职业
                                   aac017, --  婚姻状况编号
                                   yzy018, --  婚姻状况
                                   aab004, --  单位名称
                                   yzy020, --  单位地址
                                   yzy021, --  单位电话
                                   yzy022, --  单位邮编
                                   aac010, --  户口地址
                                   yzy024, --  户口邮编
                                   aae004, --  联系人
                                   yzy026, --  与病人关系
                                   yzy027, --  联系人地址
                                   yzy028, --  联系人电话
                                   yzy029, --  健康卡号
                                   ykc701, --  入院日期
                                   yzy032, --  入院统一科号
                                   yzy033, --  入院科别
                                   yzy034, --  入院病室
                                   ykc702, --  出院日期
                                   yzy037, --  出院统一科号
                                   yzy038, --  出院科别
                                   yzy039, --  出院病室
                                   akb063, --  实际住院天数
                                   akc193, --  门（急）诊诊断编码
                                   akc050, --  门（急）诊诊断疾病名
                                   yzy043, --  门、急诊医生编号
                                   ake022, --  门、急诊医生
                                   yzy045, --  病理诊断
                                   yzy046, --  过敏药物
                                   yzy047, --  抢救次数
                                   yzy048, --  抢救成功次数
                                   yzy049, --  科主任编号
                                   yzy050, --  科主任
                                   yzy051, --  主（副主）任医生编号
                                   yzy052, --  主（副主）任医生
                                   yzy053, --  主治医生编号
                                   yzy054, --  主治医生
                                   yzy055, --  住院医生编号
                                   yzy056, --  住院医生
                                   yzy057, --  进修医师编号
                                   yzy058, --  进修医师
                                   yzy059, --  实习医师编号
                                   yzy060, --  实习医师
                                   yzy061, --  编码员编号
                                   yzy062, --  编码员
                                   yzy063, --  病案质量编号
                                   yzy064, --  病案质量
                                   yzy065, --  质控医师编号
                                   yzy066, --  质控医师
                                   yzy067, --  质控护士编号
                                   yzy068, --  质控护士
                                   yzy069, --  质控日期
                                   akc264, --  总费用
                                   ake047, --  西药费
                                   yzy072, --  中药费
                                   ake050, --  中成药费
                                   ake049, --  中草药费
                                   ake044, --  其他费
                                   yzy076, --  是否尸检编号
                                   yzy077, --  是否尸检
                                   yzy078, --  血型编号
                                   yzy079, --  血型
                                   yzy080, --  RH编号
                                   yzy081, --  RH
                                   yzy082, --  首次转科统一科号
                                   yzy083, --  首次转科科别
                                   yzy084, --  首次转科日期
                                   yzy085, --  首次转科时间
                                   yzy086, --  疾病分型编号
                                   yzy087, --  疾病分型
                                   yzy088, --  籍贯
                                   yzy089, --  现住址
                                   yzy090, --  现电话
                                   yzy091, --  现邮编
                                   aca111, --  职业编号
                                   yzy093, --  新生儿出生体重
                                   yzy094, --  新生儿入院体重
                                   yzy095, --  入院途径编号
                                   yzy096, --  入院途径
                                   yzy097, --  临床路径病例编号
                                   yzy098, --  临床路径病例
                                   yzy099, --  病理疾病编码
                                   yzy100, --  病理号
                                   yzy101, --  是否药物过敏编号
                                   yzy102, --  是否药物过敏
                                   yzy103, --  责任护士编号
                                   yzy104, --  责任护士
                                   yzy105, --  离院方式编号
                                   yzy106, --  离院方式
                                   yzy107, --  离院方式为医嘱转院，拟接收医疗机构名称
                                   yzy108, --  离院方式为转社区卫生服务器机构/乡镇卫生院，拟接收医疗机构名称
                                   yzy109, --  是否有出院31天内再住院计划编号
                                   yzy110, --  是否有出院31天内再住院计划
                                   yzy111, --  再住院目的
                                   yzy112, --  颅脑损伤患者昏迷时间：入院前 天
                                   yzy113, --  颅脑损伤患者昏迷时间：入院前 小时
                                   yzy114, --  颅脑损伤患者昏迷时间：入院前 分钟
                                   yzy115, --  入院前昏迷总分钟
                                   yzy116, --  颅脑损伤患者昏迷时间：入院后 天
                                   yzy117, --  颅脑损伤患者昏迷时间：入院后 小时
                                   yzy118, --  颅脑损伤患者昏迷时间：入院后 分钟
                                   yzy119, --  入院后昏迷总分钟
                                   yzy120, --  付款方式编号
                                   yzy121, --  付款方式
                                   yzy122, --  住院总费用：自费金额
                                   yzy123, --  综合医疗服务类：（1）一般医疗服务费
                                   yzy124, --  综合医疗服务类：（2）一般治疗操作费
                                   yzy125, --  综合医疗服务类：（3）护理费
                                   yzy126, --  综合医疗服务类：（4）其他费用
                                   yzy127, --  诊断类：(5) 病理诊断费
                                   yzy128, --  诊断类：(6) 实验室诊断费
                                   yzy129, --  诊断类：(7) 影像学诊断费
                                   yzy130, --  诊断类：(8) 临床诊断项目费
                                   yzy131, --  治疗类：(9) 非手术治疗项目费
                                   yzy132, --  治疗类：非手术治疗项目费 其中临床物理治疗费
                                   yzy133, --  治疗类：(10) 手术治疗费
                                   yzy134, --  治疗类：手术治疗费 其中麻醉费
                                   yzy135, --  治疗类：手术治疗费 其中手术费
                                   yzy136, --  康复类：(11) 康复费
                                   yzy137, --  中医类：中医治疗类
                                   yzy138, --  西药类： 西药费 其中抗菌药物费用
                                   yzy139, --  血液和血液制品类： 血费
                                   yzy140, --  血液和血液制品类： 白蛋白类制品费
                                   yzy141, --  血液和血液制品类： 球蛋白制品费
                                   yzy142, --  血液和血液制品类：凝血因子类制品费
                                   yzy143, --  血液和血液制品类： 细胞因子类费
                                   yzy144, --  耗材类：检查用一次性医用材料费
                                   yzy145, --  耗材类：治疗用一次性医用材料费
                                   yzy146, --  耗材类：手术用一次性医用材料费
                                   yzy147, --  综合医疗服务类：一般医疗服务费 其中中医辨证论治费（中医）
                                   yzy148, --  综合医疗服务类：一般医疗服务费 其中中医辨证论治会诊费（中医）
                                   yzy149, --  中医类：诊断（中医）
                                   yzy150, --  中医类：治疗（中医）
                                   yzy151, --  中医类：治疗 其中外治（中医）
                                   yzy152, --  中医类：治疗 其中骨伤（中医）
                                   yzy153, --  中医类：治疗 其中针刺与灸法（中医）
                                   yzy154, --  中医类：治疗推拿治疗（中医）
                                   yzy155, --  中医类：治疗 其中肛肠治疗（中医）
                                   yzy156, --  中医类：治疗 其中特殊治疗（中医）
                                   yzy157, --  中医类：其他（中医）
                                   yzy158, --  中医类：其他 其中中药特殊调配加工（中医）
                                   yzy159, --  中医类：其他 其中辨证施膳（中医）
                                   yzy160, --  中药类：中成药费 其中医疗机构中药制剂费（中医）
                                   yzy161, --  治疗类别编号（中医类）
                                   yzy162, --  治疗类别（中医类）
                                   yzy163, --  门（急）诊中医诊断编码（中医类）
                                   yzy164, --  门（急）诊中医诊断（中医类）
                                   yzy165, --  实施临床路径编号（中医类）
                                   yzy166, --  实施临床路径（中医类）
                                   yzy167, --  使用医疗机构中药制剂编号（中医类）
                                   yzy168, --  使用医疗机构中药制剂（中医类）
                                   yzy169, --  使用中医诊疗设备编号（中医类）
                                   yzy170, --  使用中医诊疗设备（中医类）
                                   yzy171, --  使用中医诊疗技术编号（中医类）
                                   yzy172, --  使用中医诊疗技术（中医类）
                                   yzy173, --  辨证施护编号（中医类）
                                   yzy174 --  辨证施护（中医类）

                              FROM bagl
                             WHERE yzy001 = '{0}'
                               AND yzy002 = '{1}'";

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int i = FunctionSql.ExecuteQuery_DRGS(string.Format(strSql, patientNo, inTimes), ref ds, "CaseList");
            if (ds.Tables["CaseList"].Rows.Count > 0)
            {
                dt = ds.Tables["CaseList"];
            }
            else
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 查询住院病案诊断内容
        /// </summary>
        /// <param name="inPatientNo">住院流水号</param>
        /// <returns></returns>
        public DataTable GetCaseDiagnoseByBASY(string patientNo, string inTimes)
        {
            string strSql = @"SELECT yzy201, -- 排序
                               yzy202, --  诊断类型
                               akc185, --  疾病名称
                               akc196, --  ICD码
                               yzy205, --  入院病情编号
                               yzy206, -- 入院病情
                               yzy003 --ICD版本
                          FROM bazd
                         WHERE 住院号 = '{0}'
                         AND 住院次数 = '{1}'";

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int i = FunctionSql.ExecuteQuery_DRGS(string.Format(strSql, patientNo, inTimes), ref ds, "CaseDiagnoseList");
            if (ds.Tables["CaseDiagnoseList"].Rows.Count > 0)
            {
                dt = ds.Tables["CaseDiagnoseList"];
            }
            else
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// 住院病人产科分娩婴儿信息（病案首页）录入
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="inTimes"></param>
        /// <returns></returns>
        public DataTable GetCaseBabyByBASY(string patientNo,string inTimes)
        {
            string strSql = @" SELECT  yzy201	,--	排序
                            aac004	,--	婴儿性别编号
                            yzy230	,--	婴儿性别
                            yzy231	,--	婴儿体重
                            yzy232	,--	分娩结果编号
                            yzy233	,--	分娩结果
                            yzy234	,--	转归编号
                            yzy235	,--	转归
                            yzy236	,--	婴儿抢救成功次数
                            yzy237	,--	呼吸编号
                            yzy238 --	呼吸
                             FROM bafy
                             WHERE [住院号] = '{0}'
                             AND [住院次数] = '{1}' ";
            strSql = string.Format(strSql, patientNo, inTimes);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int i = FunctionSql.ExecuteQuery_DRGS(strSql, ref ds, "BabyList");
            if (ds.Tables["BabyList"].Rows.Count > 0)
            {
                dt = ds.Tables["BabyList"];
            }
            else
            {
                return null;
            }

            return dt;

        }

        /// <summary>
        /// 取出院记录-【入院情况；诊疗过程；出院情况；出院医嘱】
        /// </summary>
        /// <param name="inPatientNo">住院流水号</param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Const QueryPatientOutRecord(string inPatientNo)
        {
            string strSql = @"select YZY301, YZY302, YZY303, YZY304, YZY305, YZY306, AKC273
                          from VIEW_HIS_SNYD_CYXJ@emr1_dblink
                         where HIS_outside_id = '{0}'";

            strSql = string.Format(strSql, inPatientNo);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            FS.HISFC.Models.Base.Const obj = null;
            try
            {
                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Base.Const();

                    obj.OperEnvironment.ID = this.Reader[0].ToString();       //入院诊断描述
                    obj.OperEnvironment.Name = this.Reader[1].ToString();       //出院诊断描述
                    obj.ID = this.Reader[2].ToString().Trim();   //入院情况
                    obj.Name = this.Reader[3].ToString().Trim();  //诊疗过程
                    obj.UserCode = this.Reader[4].ToString();   //出院情况
                    obj.Memo = this.Reader[5].ToString();       //出院医嘱
                    obj.SpellCode = this.Reader[6].ToString();  //医生
                }
                return obj;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 查询住院手术内容
        /// </summary>
        /// <param name="registerId">住院流水号</param>
        /// <returns></returns>
        public DataTable QueryOperationInfo(string patientNo, string inTimes)
        {
            string strSql = @"SELECT yzy201, -- 排序
                                   yzy207, --  手术码
                                   yzy208, --  手术码对应名称
                                   yzy209, --  手术日期
                                   yzy210, --  切口编号
                                   yzy211, --  切口
                                   yzy212, --  愈合编号
                                   yzy213, --  愈合
                                   yzy214, --  手术医生编号
                                   yzy215, --  手术医生
                                   yzy216, --  麻醉方式编号
                                   yzy217, --  麻醉方式
                                   yzy218, --  是否附加手术
                                   yzy219, --  I助编号
                                   yzy220, --  I助姓名
                                   yzy221, --  II助编号
                                   yzy222, --  II助姓名
                                   yzy223, --  麻醉医生编号
                                   yzy224, --  麻醉医生
                                   yzy225, --  择期手术编号
                                   yzy226, --  择期手术
                                   yzy227, --  手术级别编号
                                   yzy228 --  手术级别
                              FROM bass
                             WHERE 住院号 = '{0}'
                               AND 住院次数 = '{1}'
                            ";

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            int i = FunctionSql.ExecuteQuery_DRGS(string.Format(strSql, patientNo, inTimes), ref ds, "OperationList");
            if (ds.Tables["OperationList"].Rows.Count > 0)
            {
                dt = ds.Tables["OperationList"];
            }
            else
            {
                return null;
            }

            return dt;
        }


        /// <summary>
        /// 获取对照码
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="objStr"></param>
        /// <returns></returns>
        public string GetDictionaryCode(string typeName, string objStr)
        {
            string sql = @"select y.mark from com_dictionary y where y.type = '{0}' and y.code = '{1}' and y.valid_state = '1'";
            sql = string.Format(sql, typeName, objStr);
            string res = this.ExecSqlReturnOne(sql);
            if (res == "-1")
            {
                return "";
            }
            return res;
        }
        /// <summary>
        /// 查询出院结算单
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="outBalanceInfo"></param>
        /// <returns></returns>
        public int QueryBalancePrint(FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref FoShanYDSI.Objects.OutBalanceInfo outBalanceInfo)
        {
            int result = 0;

            #region 查询赋值
            string strSql = @"SELECT Z.INPATIENT_NO,      Z.BALANCE_NO,       Z.MCARD_NO,          Z.IDENNO,
                      Z.NAME,              Z.GUARD_1_NAME,     Z.GUARD_1_IDENNO,    Z.GUARD_2_NAME,
                      Z.GUARD_2_IDENNO,    Z.YEAR,             Z.SI_TYPE,           Z.SI_MAIN_TYPE, 
                      Z.PERSON_TYPE,       Z.OUT_DISEASE_TYPE, Z.LIMIT_COST,        Z.EVALUATE_TYPE, 
                      Z.ACCEPT_NO,         Z.ACCIDENT_DATE,    Z.ACCIDENT_REASON,   Z.OVER_DISEASE_TYPE,
                      Z.OVER_OWNCOST_ITEM, Z.OVER_OWNCOST,     Z.BANK,              Z.ACOUNT, 
                      Z.BALANCE_SEQ,       Z.OPER_DATE,        Z.DETAIL_ROWCOUNT,   Z.TOT_COST,
                      Z.APPROVAL_COST,     Z.PUB_RATE,         Z.PUB_COST,          Z.REST_LIMIT_COST,
                      Z.BILL_NO,           Z.SUBSIDY,          Z.OWNITEM_SUPPLY,    Z.FUND_PAY_COST,
                      Z.PAY_COST,          Z.PAY_RATE,         Z.ALLOWANCE,         Z.SY_FLAG,
                      Z.GS_FLAG,           Z.BALANCE_FLAG,     Z.PATIENT_NO,        Z.BALANCE_TYPE,
                      Z.BALANCE_TYPE_NAME, Z.BALANCE_ITEM,     Z.BALANCE_ITEM_NAME, Z.DIAGN1,
               I.OPER_CODE,fun_get_employee_name(I.OPER_CODE), Z.DEDUCTIBLE_LINE
  FROM FIN_IPR_SIINMAININFO_ZHUHAI Z, FIN_IPR_SIINMAININFO I
 WHERE Z.BALANCE_NO = I.BALANCE_NO
   AND Z.BALANCE_FLAG='1'
   AND Z.INPATIENT_NO = I.INPATIENT_NO
   AND I.ZhuHaiYDSITYPE IS NOT NULL
   AND I.BALANCE_NO = (SELECT TO_CHAR(MAX(TO_NUMBER(SI.BALANCE_NO)))
                         FROM FIN_IPR_SIINMAININFO SI
                        WHERE SI.VALID_FLAG = '1'
                          AND SI.INPATIENT_NO = '{0}')
   AND I.VALID_FLAG = '1'
   AND I.INPATIENT_NO = '{0}'
  ";
            strSql = string.Format(strSql, patient.ID);

            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    if (personInfo == null)
                    {
                        personInfo = new FoShanYDSI.Objects.SIPersonInfo();
                    }
                    if (outBalanceInfo == null)
                    {
                        outBalanceInfo = new FoShanYDSI.Objects.OutBalanceInfo();
                    }

                    //patientInfo.ID, balanceNo, personInfo.MCardNo, personInfo.IdenNo,    //#1
                    string patientID = Reader[0].ToString();
                    personInfo.BalanceNo = Reader[1].ToString();
                    personInfo.MCardNo = Reader[2].ToString();
                    personInfo.IdenNo = Reader[3].ToString();
                    //personInfo.Name, personInfo.Guard1Name, personInfo.Guard1IdenNo, personInfo.Guard2Name,//#2
                    personInfo.Name = Reader[4].ToString();
                    personInfo.Guard1Name = Reader[5].ToString();
                    personInfo.Guard1IdenNo = Reader[6].ToString();
                    personInfo.Guard2Name = Reader[7].ToString();
                    //personInfo.Guard2IdenNo, personInfo.Year, personInfo.SIType, personInfo.SIMainType,//#3
                    personInfo.Guard2IdenNo = Reader[8].ToString();
                    personInfo.Year = Reader[9].ToString();
                    personInfo.SIType = Reader[10].ToString();
                    personInfo.SIMainType = Reader[11].ToString();
                    //personInfo.PersonType, personInfo.OutDiseaseType, personInfo.LimitCost, personInfo.EvaluateType,//#4
                    personInfo.PersonType = Reader[12].ToString();
                    personInfo.OutDiseaseType = Reader[13].ToString();
                    personInfo.LimitCost = Reader[14].ToString();
                    personInfo.EvaluateType = Reader[15].ToString();
                    //personInfo.AcceptNo, personInfo.AccidentDate, personInfo.AccidentReason, personInfo.OverDiseaseType,//#5
                    personInfo.AcceptNo = Reader[16].ToString();
                    personInfo.AccidentDate = Reader[17].ToString();
                    personInfo.AccidentReason = Reader[18].ToString();
                    personInfo.OverDiseaseType = Reader[19].ToString();
                    //personInfo.OverOwnCostItem, "0", outBalanceInfo.Bank, outBalanceInfo.Acount,//#6
                    personInfo.OverOwnCostItem = Reader[20].ToString();
                    personInfo.OverOwnCost = Reader[21].ToString();
                    outBalanceInfo.Bank = Reader[22].ToString();
                    outBalanceInfo.Acount = Reader[23].ToString();
                    //outBalanceInfo.BalanceSeq, outBalanceInfo.OperDate, outBalanceInfo.DetailRowCount, outBalanceInfo.TotCost, //#7
                    outBalanceInfo.BalanceSeq = Reader[24].ToString();
                    outBalanceInfo.OperDate = Reader[25].ToString();
                    outBalanceInfo.DetailRowCount = Reader[26].ToString();
                    outBalanceInfo.TotCost = Reader[27].ToString();
                    //outBalanceInfo.ApprovalCost, outBalanceInfo.PubRate, outBalanceInfo.PubCost, outBalanceInfo.RestLimitCost, //#8
                    outBalanceInfo.ApprovalCost = Reader[28].ToString();
                    outBalanceInfo.PubRate = Reader[29].ToString();
                    outBalanceInfo.PubCost = Reader[30].ToString();
                    outBalanceInfo.RestLimitCost = Reader[31].ToString();
                    //outBalanceInfo.BillNo, "0", outBalanceInfo.OwnItemSupply, outBalanceInfo.FundPayCost,   //#9
                    outBalanceInfo.BillNo = Reader[32].ToString();
                    outBalanceInfo.Subsidy = Reader[33].ToString();
                    outBalanceInfo.OwnItemSupply = Reader[34].ToString();
                    outBalanceInfo.FundPayCost = Reader[35].ToString();
                    //outBalanceInfo.PayCost, "0", outBalanceInfo.Allowance, personInfo.SYFlag,               //#10
                    outBalanceInfo.PayCost = Reader[36].ToString();
                    outBalanceInfo.PayRateInPatient = Reader[37].ToString();
                    outBalanceInfo.Allowance = Reader[38].ToString();
                    personInfo.SYFlag = Reader[39].ToString();
                    //personInfo.GSFlag, "1", patientInfo.PID.PatientNO, personInfo.BalanceType,              //#11"
                    personInfo.GSFlag = Reader[40].ToString();
                    personInfo.BalanceFlag = Reader[41].ToString();
                    personInfo.PatientNO = Reader[42].ToString();
                    personInfo.BalanceType = Reader[43].ToString();
                    //personInfo.BalanceTypeName, personInfo.BalanceItem, personInfo.BalanceItemName.Split('-')[1], personInfo.Diagn1 //#12
                    personInfo.BalanceTypeName = Reader[44].ToString();
                    personInfo.BalanceItem = Reader[45].ToString();
                    personInfo.BalanceItemName = Reader[46].ToString();
                    personInfo.Diagn1 = Reader[47].ToString();
                    // I.OPER_CODE,fun_get_employee_name(I.OPER_CODE),DEDUCTIBLE_LINE //#13
                    outBalanceInfo.OperCode = Reader[48].ToString();
                    outBalanceInfo.OperName = Reader[49].ToString();
                    outBalanceInfo.DeductibleLine = Reader[50].ToString();


                    result = 1;
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception e)
            {
                return result;
            }
            //if (!personInfo.BalanceFlag.Equals("1"))
            //{
            //    return result;
            //}
            return result;
            #endregion
        }

        #endregion

        public string GetComparePactCode(string siType, string personType, string type)
        {
            string strSql = @"select pact_code||','||pact_name
                                from fin_com_sicompare_foshan
                               where si_type = '{0}'
                                 and person_type = '{1}'
                                 and type_code = '{2}'";

            strSql = string.Format(strSql, siType, personType, type);

            try
            {
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public string GetYDRegionName(string regionID)
        {
            string strSql = @"SELECT su.region_name FROM SI_YD_REGIONALISM SU 
                            WHERE SU.Region_Id='{0}'";
            strSql = string.Format(strSql, regionID);
            try
            {
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        /// <summary>
        /// 查询医生身份证
        /// </summary>
        /// <param name="doc_code"></param>
        /// <returns></returns>
        public string GetDocIDNo(string doc_code)
        {
            string strSql = @"SELECT e.idenno from com_employee e where e.empl_code='{0}'";
            strSql = string.Format(strSql, doc_code);
            try
            {
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        /// <summary>
        /// 获得对照科室
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
//        public string GetCompareDeptCodeYD(string deptCode)
//        {
//            string strSql = @"select COMPARE_DEPT_CODE
//                                from fin_com_sideptcompare_zhuhai_yd
//                               where DEPT_CODE = '{0}'
//                                 and VALID_STATE = '1'";

//            strSql = string.Format(strSql, deptCode);

//            try
//            {
//                return this.ExecSqlReturnOne(strSql);
//            }
//            catch (Exception e)
//            {
//                return "";
//            }
//        }

        /// <summary>
        /// 获得对照科室
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string GetCompareDeptCode(string deptCode)
        {
            string strSql = @"select COMPARE_DEPT_CODE
                                from fin_com_sideptcompare_yd
                               where DEPT_CODE = '{0}'
                                 and VALID_STATE = '1'";

            strSql = string.Format(strSql, deptCode);

            try
            {
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        /// <summary>
        /// 获得异地对照科室
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string GetCompareDeptCodeYD(string deptCode)
        {
            string strSql = @"select COMPARE_DEPT_CODE
                                from fin_com_sideptcompare_YD
                               where DEPT_CODE = '{0}'
                                 and VALID_STATE = '1'";

            strSql = string.Format(strSql, deptCode);

            try
            {
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        /// <summary>
        /// 获得医保科室名称
        /// </summary>
        /// <param name="cmpDeptCode">医保科室编码</param>
        /// <returns></returns>
        public string GetCompareDeptName(string cmpDeptCode)
        {
            string strSql = @"select compare_dept_name
                                from fin_com_sideptcompare_yd
                               where compare_dept_code = '{0}'
                                 and VALID_STATE = '1'";

            strSql = string.Format(strSql, cmpDeptCode);

            try
            {
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        private string GetBalanceNo(string patientNo)
        {
            string strSql = @"select max(to_number(BALANCE_NO))
                                from fin_ipr_siinmaininfo
                               where inpatient_no = '{0}'";
            strSql = string.Format(strSql, patientNo);

            try
            {
                return this.ExecSqlReturnOne(strSql);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        /// <summary>
        /// 获取异地医保费用信息
        /// </summary>
        /// <param name="inpatienNo"></param>
        /// <returns></returns>
        public ArrayList GetYDFeeList(string inpatienNo)
        {
            string strSql = @"SELECT FEE.FEE_STAT_NAME,
                                       SUM(ML.TOT_COST) 
                                FROM FIN_IPB_MEDICINELIST ML,fin_com_feecodestat fee 
                                WHERE ML.FEE_CODE=FEE.FEE_CODE
                                and fee.report_code='ZY01'
                                AND ML.INPATIENT_NO='{0}'
                                GROUP BY FEE.FEE_STAT_NAME
                                UNION ALL

                                SELECT FEE.FEE_STAT_NAME,
                                       SUM(IL.TOT_COST) 
                                FROM FIN_IPB_ITEMLIST IL,fin_com_feecodestat fee 
                                WHERE IL.FEE_CODE=FEE.FEE_CODE
                                 and fee.report_code='ZY01'
                                AND IL.INPATIENT_NO='{0}'
                                GROUP BY FEE.FEE_STAT_NAME";
            try
            {
                ArrayList al = new ArrayList();
                int intFlag = this.ExecQuery(strSql,inpatienNo);
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject Obj = new FS.FrameWork.Models.NeuObject();
                    Obj.Name = Reader[0].ToString();
                    Obj.Memo = Reader[1].ToString();
                    al.Add(Obj);
                }
                return al;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 患者入出转业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 根据就诊登记号和身份证号码获取住院流水号
        /// </summary>
        /// <param name="jzdjh"></param>
        /// <param name="idno"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfoByJZDJH(string jzdjh, string idno)
        {
            if (string.IsNullOrEmpty(jzdjh) || string.IsNullOrEmpty(idno))
            {
                return null;
            }
            string sql = @"select d.inpatient_no from fin_ipr_siinmaininfo_yd d where d.jzdjh = '{0}' and d.idno = '{1}'";
            sql = string.Format(sql, jzdjh, idno);
            string inpatientNo = this.ExecSqlReturnOne(sql);
            if (string.IsNullOrEmpty(inpatientNo) || inpatientNo == "-1")
            {
                return null;
            }

            FS.HISFC.Models.RADT.PatientInfo patientTemp = this.radtIntegrate.GetPatientInfomation(inpatientNo);

            return patientTemp;
        }

        /// <summary>
        /// 更新异地结算状态
        /// </summary>
        /// <param name="inPatintNo"></param>
        /// <returns></returns>
        public int UpdateSiState(string inPatintNo, string state)
        {
            if (string.IsNullOrEmpty(inPatintNo))
            {
                this.Err = "住院流水号为空！";
                return -1;
            }
            string strSql = @"update fin_ipr_siinmaininfo_yd d
                            set d.oper_sate = '{1}'
                            where d.inpatient_no = '{0}'";
            strSql = string.Format(strSql, inPatintNo, state);

            try
            {
                if (this.ExecNoQuery(strSql) < 0)
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                return -1;
            }
            return 1;

        }

        /// <summary>
        /// 获取当前状态
        /// </summary>
        /// <param name="inPatintNo"></param>
        /// <returns></returns>
        public string GetSiState(string inPatintNo)
        {
            string sql = @"select d.oper_sate from fin_ipr_siinmaininfo_yd d
                        where d.inpatient_no = '{0}'
                        and rownum = 1";
            return this.ExecSqlReturnOne(sql);
        }


        /// <summary>
        /// 查询患者住院信息列表
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public ArrayList QueryInPatientInfo(string patientNo)
        {
            int result = 0;

            string strSql = @"select a.inpatient_no,
                           a.name,
                           a.in_state,
                           a.dept_name,
                           a.in_date,
                           a.dept_code
                      from fin_ipr_inmaininfo a
                     where a.patient_no = '{0}'";
            strSql = string.Format(strSql, patientNo);

            ArrayList al = new ArrayList();
            FS.HISFC.Models.Base.Spell pObj = null;
            try
            {
                this.ExecQuery(strSql);
                while (this.Reader.Read())
                {
                    pObj = new FS.HISFC.Models.Base.Spell();

                    pObj.ID = this.Reader[0].ToString();//序号
                    pObj.Name = this.Reader[1].ToString();
                    pObj.Memo = this.Reader[2].ToString();//住院状态
                    pObj.SpellCode = this.Reader[3].ToString();//科室
                    pObj.WBCode = this.Reader[4].ToString();//入院时间
                    //pObj.UserCode = this.Reader[5].ToString();//挂号时间

                    al.Add(pObj);
                }
            }
            catch (Exception e)
            {
            }

            return al;
        }
    }
}
