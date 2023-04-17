using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Neusoft.FrameWork.Function;

namespace FoShanDiseasePay.BizLogic
{
    /// <summary>
    /// 医用耗材业务管理类
    /// </summary>
    public class MaterialManager : Neusoft.HISFC.BizLogic.Material.BizLogic.Base.BaseLogic
    {

        /**
         * 
         * 佛山市第三人民医院：药库管理模式；
         * 佛山市第四人民医院：新版物资管理模式；  【根据不同医院，取值不同医用耗材库，库房编码 2006】
         * 
         * */

        /// <summary>
        /// 查询医用耗材基本信息
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryMaterialBaseInfoByQuery(string queryCode, DateTime begTime, DateTime endTime)
        {
            string strSql = string.Empty;
            string deptCode = "";
            // {385E49F2-947B-43e3-931D-BE89481BA68C}
            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                #region 佛山市第三人民医院 佛山市中医院禅城高新区医院 

                strSql = @"SELECT t.DRUG_CODE 医用耗材编码,
                                       t.TRADE_NAME 医用耗材名称,
                                       t.CUSTOM_CODE 自定义码,
                                       t.GB_CODE 国标码,
                                       t.SPECS 规格,
                                       t.RETAIL_PRICE 零售价,
                                       t.APPROVE_INFO 批文号,
                                       (SELECT p.FAC_NAME FROM PHA_COM_COMPANY p WHERE p.FAC_CODE = i.producer_code) 厂家,
                                        (SELECT p.FAC_NAME FROM PHA_COM_COMPANY p WHERE p.FAC_CODE = i.company_code) 供货商,
                                       (SELECT sum(s.STORE_SUM) FROM PHA_COM_STOCKINFO s WHERE s.DRUG_CODE = t.DRUG_CODE) 库存,
                                       i.invoice_no 发票号,
                                       t.upload_type,
                                       t.sun_registerno,
                                       t.cg_productid,
                                       t.yg_productid
                                FROM PHA_COM_BASEINFO t,pha_com_input i
                                WHERE t.VALID_STATE = '1'
                                  and t.drug_code = i.drug_code
                                  AND t.FEE_CODE NOT IN ('001','002','003')                      
                                  and (t.drug_code like '%{0}%' or t.trade_name like '%{0}%'
                                  or t.CUSTOM_CODE like '%{0}%' or t.spell_code like '%{0}%'
                                  or t.wb_code like '%{0}%'or t.GB_CODE like '%{0}%')
                                  and i.in_date>= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
                                  and i.in_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                  and i.in_type = '01'
                                  and i.class3_meaning_code = '11'  
                                  and t.is_upload = '1'  
                                ORDER BY t.DRUG_CODE
                                ";

                #endregion
            }
                
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "南庄社区卫生服务中心")
            {
                #region 南庄社区卫生服务中心

                strSql = @"
                            SELECT t.ITEM_CODE 医用耗材编码,
                                   t.ITEM_NAME 医用耗材名称,
                                   t.CUSTOM_CODE 自定义码,
                                   t.GB_CODE 国标码,
                                   t.SPECS 规格,
                                   t.SALE_PRICE 零售价,
                                   t.APPROVE_INFO 批文号,
                                   (SELECT m.COMPANY_NAME FROM MAT_COM_COMPANY@nzhis m WHERE m.COMPANY_CODE=t.FACTORY_CODE AND m.COMPANY_TYPE='0') 厂家,
                                   (SELECT m.COMPANY_NAME FROM MAT_COM_COMPANY@nzhis m WHERE m.COMPANY_CODE=t.COMPANY_CODE AND m.COMPANY_TYPE='1') 供货商,
                                   (SELECT sum(s.STORE_NUM) FROM MAT_COM_STOCKDETAIL@nzhis  s WHERE s.ITEM_CODE=t.ITEM_CODE and s.STORAGE_CODE='7012') 库存,
                                    '' 发票号,
                                   t.upload_type,
                                   (select a.register_code from mat_com_basereginfo@nzhis a where a.item_code = t.item_code and a.valid_flag = '1' and rownum = 1) regNo,
                                   t.cg_productid,
                                   t.yg_productid
                           
                            FROM MAT_COM_BASEINFO@nzhis  t
                            WHERE t.STORAGE_CODE = '7012'   --根据不同医院，取值不同医用耗材库
                             AND t.VALID_FLAG = '1'
                             and (t.ITEM_CODE like '%{0}%' or t.ITEM_NAME like '%{0}%' or
                                   t.CUSTOM_CODE like '%{0}%' or t.spell_code like '%{0}%' or
                                   t.wb_code like '%{0}%'or
                                   t.GB_CODE like '%{0}%')
                             ORDER BY t.ITEM_CODE
                            ";

                #endregion
            }
            else
            {
                strSql = @"SELECT t.ITEM_CODE 医用耗材编码,
                                   t.ITEM_NAME 医用耗材名称,
                                   t.CUSTOM_CODE 自定义码,
                                   t.GB_CODE 国标码,
                                   t.SPECS 规格,
                                   t.SALE_PRICE 零售价,
                                   t.APPROVE_INFO 批文号,
                                   (SELECT m.COMPANY_NAME FROM MAT_COM_COMPANY m WHERE m.COMPANY_CODE=y.FACTORY_CODE AND m.COMPANY_TYPE='0') 厂家,
                                   (SELECT m.COMPANY_NAME FROM MAT_COM_COMPANY m WHERE m.COMPANY_CODE=y.company_code AND m.COMPANY_TYPE='1') 供货商,
                                   (SELECT sum(s.STORE_NUM) FROM MAT_COM_STOCKDETAIL s WHERE s.ITEM_CODE=t.ITEM_CODE and s.STORAGE_CODE='2006') 库存,
                                    y.invoice_no 发票号,
                                   t.upload_type,
                                   (select a.register_code from mat_com_basereginfo a where a.item_code = t.item_code and a.valid_flag = '1' and rownum = 1) regNo,
                                   t.cg_productid,
                                   t.yg_productid

                            FROM MAT_COM_BASEINFO t,mat_com_input y
                            WHERE t.STORAGE_CODE = '{3}'   --根据不同医院，取值不同医用耗材库
                             and t.item_code = y.item_code
                             AND t.VALID_FLAG = '1'
                             and (t.ITEM_CODE like '%{0}%' or t.ITEM_NAME like '%{0}%' or
                                   t.CUSTOM_CODE like '%{0}%' or t.spell_code like '%{0}%' or
                                   t.wb_code like '%{0}%'or
                                   t.GB_CODE like '%{0}%')
                             and y.in_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
                             and y.in_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                             and y.in_num > 0
                             ORDER BY y.company_code,t.ITEM_CODE  
                             
                            ";
            }
            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                #region 佛山市第四人民医院
                deptCode = "2006";              

                #endregion
            }

            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "禅城区石湾社区卫生服务中心")
            {
                #region 禅城区石湾社区卫生服务中心

                deptCode = "2004";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                #region 佛山市第一人民医院禅城医院 

                deptCode = "7012";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "南庄社区卫生服务中心")
            {
                #region 南庄社区卫生服务中心

                deptCode = "7012";

                #endregion
            }

            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "张槎街道社区卫生服务中心")
            {
                #region 张槎街道社区卫生服务中心

                deptCode = "9007";

                #endregion
            }
            strSql = string.Format(strSql, queryCode,begTime.ToString(),endTime.ToString(),deptCode);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            ArrayList alMat = new ArrayList();
            Neusoft.HISFC.BizLogic.Material.Object.MatBase matBase = null;
            try
            {
                while (this.Reader.Read())
                {
                    matBase = new Neusoft.HISFC.BizLogic.Material.Object.MatBase();

                    matBase.ID = this.Reader[0].ToString().Trim();
                    matBase.Name = this.Reader[1].ToString().Trim();
                    matBase.UserCode = this.Reader[2].ToString();
                    matBase.GBCode = this.Reader[3].ToString();
                    matBase.Specs = this.Reader[4].ToString();
                    matBase.Price = NConvert.ToDecimal(this.Reader[5].ToString());
                    matBase.ApproveInfo = this.Reader[6].ToString();
                    matBase.Factory.Name = this.Reader[7].ToString();
                    matBase.Company.Name = this.Reader[8].ToString();
                    matBase.Memo = this.Reader[9].ToString();   //库存信息
                    matBase.User01 = this.Reader[10].ToString();   //发票号
                    matBase.User02 = this.Reader[11].ToString();   //上传标志
                    matBase.RegisterNo = this.Reader[12].ToString();   //注册号
                    matBase.PlatMatCode = this.Reader[13].ToString();
                    matBase.SunMatCode = this.Reader[14].ToString();//阳光编码
                    alMat.Add(matBase);
                }
                return alMat;
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
        /// 查询所有耗材基本信息
        /// </summary>
        /// <param name="queryCode"></param>
        /// <returns></returns>
        public ArrayList QueryMaterialBaseInfoByALL(string queryCode,string upType)
        {
            string strSql = string.Empty;
            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                #region 佛山市第三人民医院 佛山市中医院禅城高新区医院

                strSql = @"SELECT t.DRUG_CODE 医用耗材编码,
                                       t.TRADE_NAME 医用耗材名称,
                                       t.CUSTOM_CODE 自定义码,
                                       t.GB_CODE 国标码,
                                       t.SPECS 规格,
                                       t.RETAIL_PRICE 零售价,
                                       t.APPROVE_INFO 批文号,
                                       (SELECT p.FAC_NAME FROM PHA_COM_COMPANY p WHERE p.FAC_CODE = t.producer_code) 厂家,
                                        (SELECT p.FAC_NAME FROM PHA_COM_COMPANY p WHERE p.FAC_CODE = t.company_code) 供货商,
                                       (SELECT sum(s.STORE_SUM) FROM PHA_COM_STOCKINFO s WHERE s.DRUG_CODE = t.DRUG_CODE) 库存,
                                       '' 发票号,
                                       t.upload_type,
                                       t.SUN_REGISTERNO,
                                       t.CG_ProductID,
                                       t.YG_ProductID
                                FROM PHA_COM_BASEINFO t
                                WHERE t.VALID_STATE = '1'
                                  AND t.FEE_CODE NOT IN ('001','002','003')                      
                                  and (t.drug_code like '%{0}%' or t.trade_name like '%{0}%'
                                  or t.CUSTOM_CODE like '%{0}%' or t.spell_code like '%{0}%'
                                  or t.wb_code like '%{0}%'or t.GB_CODE like '%{0}%') 
                                  and (t.upload_type = '{2}' or 'ALL' = '{2}')
                                  and t.is_upload = '1'                                 
                                ORDER BY t.DRUG_CODE
                                ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "禅城区石湾社区卫生服务中心"
            || Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "张槎街道社区卫生服务中心")
            {
                #region 

                strSql = @" SELECT t.ITEM_CODE 医用耗材编码,
                                   t.ITEM_NAME 医用耗材名称,
                                   t.CUSTOM_CODE 自定义码,
                                   t.GB_CODE 国标码,
                                   t.SPECS 规格,
                                   t.SALE_PRICE 零售价,
                                   t.APPROVE_INFO 批文号,
                                   (SELECT m.COMPANY_NAME FROM MAT_COM_COMPANY m WHERE m.COMPANY_CODE=t.FACTORY_CODE AND m.COMPANY_TYPE='0') 厂家,
                                   (SELECT m.COMPANY_NAME FROM MAT_COM_COMPANY m WHERE m.COMPANY_CODE=t.company_code AND m.COMPANY_TYPE='1') 供货商,
                                   (SELECT sum(s.STORE_NUM) FROM MAT_COM_STOCKDETAIL s WHERE s.ITEM_CODE=t.ITEM_CODE and s.STORAGE_CODE='2006') 库存,
                                    '' 发票号,
                                   t.upload_type,
                                   (select a.register_code from mat_com_basereginfo a where a.item_code = t.item_code and a.valid_flag = '1' and rownum = 1) regNo,
                                   t.cg_productid,
                                   t.yg_productid

                            FROM MAT_COM_BASEINFO t
                            WHERE t.STORAGE_CODE = '{1}'   --根据不同医院，取值不同医用耗材库
                             AND t.VALID_FLAG = '1'
                             and (t.ITEM_CODE like '%{0}%' or t.ITEM_NAME like '%{0}%' or
                                   t.CUSTOM_CODE like '%{0}%' or t.spell_code like '%{0}%' or
                                   t.wb_code like '%{0}%'or
                                   t.GB_CODE like '%{0}%')
                             and (t.upload_type = '{2}' or 'ALL' = '{2}')
                             and t.is_upload = '1'
                             ORDER BY t.ITEM_CODE
                             
                            ";

                #endregion
            }

            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "南庄社区卫生服务中心")
            {
                #region 南庄社区卫生服务中心

                strSql = @"
                            SELECT t.ITEM_CODE 医用耗材编码,
                                   t.ITEM_NAME 医用耗材名称,
                                   t.CUSTOM_CODE 自定义码,
                                   t.GB_CODE 国标码,
                                   t.SPECS 规格,
                                   t.SALE_PRICE 零售价,
                                   t.APPROVE_INFO 批文号,
                                   (SELECT m.COMPANY_NAME FROM MAT_COM_COMPANY@nzhis m WHERE m.COMPANY_CODE=t.FACTORY_CODE AND m.COMPANY_TYPE='0') 厂家,
                                   (SELECT m.COMPANY_NAME FROM MAT_COM_COMPANY@nzhis m WHERE m.COMPANY_CODE=t.COMPANY_CODE AND m.COMPANY_TYPE='1') 供货商,
                                   (SELECT sum(s.STORE_NUM) FROM MAT_COM_STOCKDETAIL@nzhis  s WHERE s.ITEM_CODE=t.ITEM_CODE and s.STORAGE_CODE='7012') 库存,
                                    '' 发票号,
                                   t.upload_type,
                                   (select a.register_code from mat_com_basereginfo@nzhis a where a.item_code = t.item_code and a.valid_flag = '1' and rownum = 1) regNo,
                                   t.cg_productid,
                                   t.yg_productid
                            FROM MAT_COM_BASEINFO@nzhis  t
                            WHERE t.STORAGE_CODE = '7012'   --根据不同医院，取值不同医用耗材库
                             AND t.VALID_FLAG = '1'
                             and (t.ITEM_CODE like '%{0}%' or t.ITEM_NAME like '%{0}%' or
                                   t.CUSTOM_CODE like '%{0}%' or t.spell_code like '%{0}%' or
                                   t.wb_code like '%{0}%'or
                                   t.GB_CODE like '%{0}%')
                             and (t.upload_type = '{2}' or 'ALL' = '{2}')
                             and t.is_upload = '1'
                             ORDER BY t.ITEM_CODE
                            ";

                #endregion
            }
            string deptCode = "";
            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "禅城区石湾社区卫生服务中心")
            {
                #region 禅城区石湾社区卫生服务中心
                deptCode = "2004";
                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                #region 佛山市第一人民医院禅城医院
                deptCode = "7012";
                #endregion
            }

            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "张槎街道社区卫生服务中心")
            {
                #region 张槎街道社区卫生服务中心
                deptCode = "9007";
                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                #region 佛山市第四人民医院
                deptCode = "2006";
                #endregion
            }
            strSql = string.Format(strSql, queryCode, deptCode, upType);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            ArrayList alMat = new ArrayList();
            Neusoft.HISFC.BizLogic.Material.Object.MatBase matBase = null;
            try
            {
                while (this.Reader.Read())
                {
                    matBase = new Neusoft.HISFC.BizLogic.Material.Object.MatBase();

                    matBase.ID = this.Reader[0].ToString().Trim();
                    matBase.Name = this.Reader[1].ToString().Trim();
                    matBase.UserCode = this.Reader[2].ToString();
                    matBase.GBCode = this.Reader[3].ToString();
                    matBase.Specs = this.Reader[4].ToString();
                    matBase.Price = NConvert.ToDecimal(this.Reader[5].ToString());
                    matBase.ApproveInfo = this.Reader[6].ToString();
                    matBase.Factory.Name = this.Reader[7].ToString();
                    matBase.Company.Name = this.Reader[8].ToString();
                    matBase.Memo = this.Reader[9].ToString();   //库存信息
                    matBase.User01 = this.Reader[10].ToString();   //发票号
                    matBase.User02 = this.Reader[11].ToString();   //上传标志
                    matBase.RegisterNo = this.Reader[12].ToString();   //注册证号
                    matBase.PlatMatCode = this.Reader[13].ToString();
                    matBase.SunMatCode = this.Reader[14].ToString(); //阳光编码

                    alMat.Add(matBase);
                }
                return alMat;
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
        /// 更新耗材上传标志
        /// </summary>
        /// <param name="matCode"></param>
        /// <param name="upType"></param>
        /// <returns></returns>
        public int UpdateMatUploadFlat(string matCode, string upType)
        {
            string sql = @"update MAT_COM_BASEINFO t
                        set t.upload_type = '{0}'
                        where t.item_code = '{1}'";
            sql = string.Format(sql, upType, matCode);

            return this.ExecNoQuery(sql);
        }
        /// <summary>
        /// 更新药品表的上传标志
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="upType"></param>
        /// <returns></returns>
        public int UpdatBaseInfoFlat(string drugCode, string upType)
        {
            string sql = @"update pha_com_baseinfo t
                        set t.upload_type = '{0}'
                        where t.drug_code = '{1}'";
            sql = string.Format(sql, upType, drugCode);

            return this.ExecNoQuery(sql);
        }
        /// <summary>
        /// 查询医用耗材基本信息
        /// </summary>
        /// <param name="matId"></param>
        /// <returns></returns>
        public Neusoft.HISFC.BizLogic.Material.Object.MatBase QueryMaterialBaseInfo(string matId)
        {
            string strSql = string.Empty;

            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                #region 佛山市第三人民医院 佛山市中医院禅城高新区医院

                strSql = @"SELECT t.cg_productid  PLATCODE,
                                   t.yg_productid  SUNCODE,
                                   t.DRUG_CODE ITEM_CODE,
                                   t.SUN_DRUGCODE CENTERCODE,
                                   t.TRADE_NAME ITEM_NAME,
                                   t.sun_drugname REGNAME,
                                   t.PRODUCER_CODE FACTORY_CODE,
                                   FUN_GET_COMPANY_NAME(t.PRODUCER_CODE) FACTORY_NAME,
                                   t.COMPANY_CODE,
                                   FUN_GET_COMPANY_NAME(t.COMPANY_CODE) COM_NAME,
                                   t.LABEL BRAND,
                                   t.DOSE_UNIT,
                                   t.MIN_UNIT,
                                   t.BASE_DOSE,
                                   t.PACK_UNIT,
                                   t.PACK_QTY,
                                   t.SPECS WrapSpecs,--包装规格
                                   t.WRAPPER Wrapper,--包装材质
                                   t.INGREDIENT PERFORMGROUP,--18
                                   t.APPROVE_INFO REGISTERID,
                                   t.sun_registerno REGISTERNO,
                                   t.sun_drugspecs REGSPECS,--注册证规格21
                                   t.sun_drugtype REGMODEL,--注册证型号22
                                   '' OVER_DATE,
                                   t.SPECS,--24
                                   t.sun_drugtype MODEL,--25
                                   '' USAGE,
                                   '' MEMO,
                                   t.RETAIL_PRICE
                            FROM PHA_COM_BASEINFO t
                            WHERE t.VALID_STATE = '1'
                            AND t.DRUG_CODE = '{0}'
                            ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "禅城区石湾社区卫生服务中心"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "张槎街道社区卫生服务中心")
            {
                #region 佛山市第四人民医院 禅城区石湾社区卫生服务中心 佛山市第一人民医院禅城医院 张槎街道社区卫生服务中心

                strSql = @"SELECT  T.CG_ProductID,
                                   T.YG_ProductID,
                                   T.ITEM_CODE,
                                   T.CENTERCODE,
                                   T.ITEM_NAME,
                                   T.Sun_Drugname REGNAME,
                                   T.FACTORY_CODE,
                                   (SELECT C.COMPANY_NAME
                                      FROM MAT_COM_COMPANY  C
                                     WHERE C.COMPANY_CODE = T.FACTORY_CODE
                                     AND c.COMPANY_TYPE='0') FAC_NAME,
                                   T.COMPANY_CODE,
                                   (SELECT C.COMPANY_NAME
                                      FROM MAT_COM_COMPANY  C
                                     WHERE C.COMPANY_CODE = T.COMPANY_CODE
                                    AND c.COMPANY_TYPE='1') COM_NAME,
                                   T.BRAND,
                                   T.MIN_UNIT,
                                   T.MIN_UNIT,
                                   '1',
                                   T.PACK_UNIT,
                                   T.PACK_QTY,
                                   T.WRAPSPEC,
                                   T.WRAPPER,
                                   T.PERFORMGROUP,
                                   T.REGISTER_CODE REGISTERCODE,
                                   (select a.register_code from mat_com_basereginfo a where a.item_code = t.item_code and a.valid_flag = '1' and rownum = 1) REGISTERCODE,--20                               
                                   T.Sun_Drugspecs REGSPECS,
                                   (select a.register_model from mat_com_basereginfo a where a.item_code = t.item_code and a.valid_flag = '1' and rownum = 1) REGMODEL,--20                               
                                   (select a.over_date from mat_com_basereginfo a where a.item_code = t.item_code and a.valid_flag = '1' and rownum = 1)   OVER_DATE,                          
                                   T.SPECS,
                                   T.MODEL,
                                   T.USAGE,
                                   T.MEMO,
                                   T.IN_PRICE
                              FROM MAT_COM_BASEINFO  T
                             WHERE T.VALID_FLAG = '1'
                               AND T.ITEM_CODE = '{0}'
  ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "南庄社区卫生服务中心")
            {
                #region 南庄社区卫生服务中心

                strSql = @"SELECT  T.CG_ProductID,
                                   T.YG_ProductID,
                                   T.ITEM_CODE,
                                   T.CENTERCODE,
                                   T.ITEM_NAME,
                                   T.Sun_Drugname REGNAME,
                                   T.FACTORY_CODE,
                                   (SELECT C.COMPANY_NAME
                                      FROM MAT_COM_COMPANY  C
                                     WHERE C.COMPANY_CODE = T.FACTORY_CODE
                                     AND c.COMPANY_TYPE='0') FAC_NAME,--7
                                   T.COMPANY_CODE,--8
                                   (SELECT C.COMPANY_NAME
                                      FROM MAT_COM_COMPANY  C
                                     WHERE C.COMPANY_CODE = T.COMPANY_CODE
                                    AND c.COMPANY_TYPE='1') COM_NAME,
                                   T.BRAND,--10
                                   T.MIN_UNIT,
                                   T.MIN_UNIT,
                                   '1',
                                   T.PACK_UNIT,
                                   T.PACK_QTY,
                                   T.WRAPSPEC,
                                   T.WRAPPER,
                                   T.PERFORMGROUP,--18
                                   T.REGISTER_CODE REGISTERCODE,--19
                                   (select a.register_code from mat_com_basereginfo@nzhis a where a.item_code = t.item_code and a.valid_flag = '1' and rownum = 1) REGISTERCODE,--20                               
                                   T.Sun_Drugspecs REGSPECS,
                                   (select a.register_model from mat_com_basereginfo a where a.item_code = t.item_code and a.valid_flag = '1' and rownum = 1) REGMODEL,--20                               
                                   (select a.over_date from mat_com_basereginfo a where a.item_code = t.item_code and a.valid_flag = '1' and rownum = 1)   OVER_DATE,                          
                                   
                                   T.SPECS,
                                   T.MODEL,
                                   T.USAGE,
                                   T.MEMO,
                                   T.IN_PRICE
                              FROM MAT_COM_BASEINFO@nzhis  T
                             WHERE T.VALID_FLAG = '1'
                               AND T.ITEM_CODE = '{0}'
  ";

                #endregion
            }

            strSql = string.Format(strSql, matId);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Neusoft.HISFC.BizLogic.Material.Object.MatBase mat = null;
            try
            {
                while (this.Reader.Read())
                {
                    mat = new Neusoft.HISFC.BizLogic.Material.Object.MatBase();

                    mat.PlatMatCode = Reader[0].ToString();
                    mat.SunMatCode = Reader[1].ToString();
                    mat.ID = Reader[2].ToString();
                    mat.CenterCode = Reader[3].ToString();
                    mat.Name = Reader[4].ToString();
                    mat.User03 = Reader[5].ToString();
                    mat.Factory.ID = Reader[6].ToString();
                    mat.Factory.Name = Reader[7].ToString();
                    mat.Company.ID = Reader[8].ToString();
                    mat.Company.Name = Reader[9].ToString();
                    mat.Brand = Reader[10].ToString();
                    mat.MinUnit = Reader[11].ToString();
                    mat.User01 = Reader[12].ToString();
                    mat.User02 = Reader[13].ToString();
                    mat.PackUnit = Reader[14].ToString();
                    mat.PackQty = NConvert.ToDecimal(Reader[15].ToString());
                    mat.WrapSpecs = Reader[16].ToString();
                    mat.Wrapper = Reader[17].ToString();
                    mat.Perform = Reader[18].ToString();
                    mat.RegisterCode = Reader[19].ToString();//对应佛山市耗材采购平台注册证编号
                    mat.UserCode = Reader[20].ToString();//注册证号
                    mat.SpellCode = Reader[21].ToString();//注册证规格
                    mat.Storage.Memo = Reader[22].ToString();//注册证型号
                    mat.RegisterDate = NConvert.ToDateTime(Reader[23].ToString());
                    mat.Specs = Reader[24].ToString();
                    mat.Model = Reader[25].ToString();
                    mat.Usage = Reader[26].ToString();
                    mat.Memo = Reader[27].ToString();
                    mat.InPrice = NConvert.ToDecimal(Reader[28]);
                }

                return mat;
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
        /// 查询医用耗材消耗明细信息
        /// </summary>
        /// <param name="matId"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public ArrayList QueryMaterialUseDetail(string matId, DateTime dtBegin, DateTime dtEnd)
        {

            string strSql = string.Empty;

            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                #region 佛山市第三人民医院

                strSql = @"SELECT (SELECT P.Cg_Productid
                                  FROM PHA_COM_BASEINFO P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE
                                   AND ROWNUM = 1) platCode, 
                                 (SELECT P.Yg_Productid
                                  FROM PHA_COM_BASEINFO P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE
                                   AND ROWNUM = 1) sunCode,
                               T.DRUG_CODE,
                               (SELECT TK.DOCT_DEPT
                                  FROM FIN_OPB_FEEDETAIL TK
                                 WHERE TK.CLINIC_CODE = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO
                                   AND ROWNUM = 1) deptCode,
                               (SELECT TK.DOCT_CODE
                                  FROM FIN_OPB_FEEDETAIL TK
                                 WHERE TK.CLINIC_CODE = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO
                                   AND ROWNUM = 1) doctCode,
                               (SELECT R.CARD_NO
                                  FROM FIN_OPR_REGISTER R
                                 WHERE R.CLINIC_CODE = T.GET_PERSON
                                   AND ROWNUM = 1) cardNO,
                               (SELECT R.MCARD_NO
                                  FROM FIN_OPR_REGISTER R
                                 WHERE R.CLINIC_CODE = T.GET_PERSON
                                   AND ROWNUM = 1) ssn,
                               (SELECT R.NAME
                                  FROM FIN_OPR_REGISTER R
                                 WHERE R.CLINIC_CODE = T.GET_PERSON
                                   AND ROWNUM = 1) name,
                               T.OUT_DATE,
                               T.BATCH_NO,
                               '' PRO_DATE,
                               T.VALID_DATE,
                               T.OUT_NUM,
                               (SELECT fun_get_dept_name(TK.DOCT_DEPT)
                                  FROM FIN_OPB_FEEDETAIL TK
                                 WHERE TK.CLINIC_CODE = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO
                                   AND ROWNUM = 1) deptname,
                               (SELECT FUN_GET_EMPLOYEE_NAME(TK.DOCT_CODE)
                                  FROM FIN_OPB_FEEDETAIL TK
                                 WHERE TK.CLINIC_CODE = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO
                                   AND ROWNUM = 1) doctname
                          FROM PHA_COM_OUTPUT T
                         WHERE T.OUT_TYPE IN ('M1', 'M2')
                           AND T.DRUG_CODE = '{0}'
                           AND T.OUT_DATE >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                           AND T.OUT_DATE <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                           
                        UNION ALL

                        SELECT  (SELECT P.Cg_Productid
                                  FROM PHA_COM_BASEINFO P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE
                                   AND ROWNUM = 1) platCode, 
                                 (SELECT P.Yg_Productid
                                  FROM PHA_COM_BASEINFO P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE
                                   AND ROWNUM = 1) sunCode,
                               T.DRUG_CODE,
                               (SELECT TK.RECIPE_DEPTCODE
                                  FROM FIN_IPB_MEDICINELIST TK
                                 WHERE TK.INPATIENT_NO = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO
                                   AND ROWNUM = 1) deptCOde,
                               (SELECT TK.RECIPE_DOCCODE
                                  FROM FIN_IPB_MEDICINELIST TK
                                 WHERE TK.INPATIENT_NO = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO
                                   AND ROWNUM = 1) doctCode,
                               (SELECT R.PATIENT_NO
                                  FROM FIN_IPR_INMAININFO R
                                 WHERE R.INPATIENT_NO = T.GET_PERSON
                                   AND ROWNUM = 1) cardNO,
                               (SELECT R.MCARD_NO
                                  FROM FIN_IPR_INMAININFO R
                                 WHERE R.INPATIENT_NO = T.GET_PERSON
                                   AND ROWNUM = 1) ssn,
                               (SELECT R.NAME
                                  FROM FIN_IPR_INMAININFO R
                                 WHERE R.INPATIENT_NO = T.GET_PERSON
                                   AND ROWNUM = 1) name,
                               T.OUT_DATE,
                               T.BATCH_NO,
                               '' PRO_DATE,
                               T.VALID_DATE,
                               T.OUT_NUM,
                               (SELECT fun_get_dept_name(TK.RECIPE_DEPTCODE)
                                  FROM FIN_IPB_MEDICINELIST TK
                                 WHERE TK.INPATIENT_NO = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO
                                   AND ROWNUM = 1) deptname,
                               (SELECT FUN_GET_EMPLOYEE_NAME(TK.RECIPE_DOCCODE)
                                  FROM FIN_IPB_MEDICINELIST TK
                                 WHERE TK.INPATIENT_NO = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO
                                   AND ROWNUM = 1) doctname
                          FROM PHA_COM_OUTPUT T
                         WHERE T.OUT_TYPE IN ('Z1', 'Z2')
                           AND T.DRUG_CODE = '{0}'
                           AND T.OUT_DATE >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                           AND T.OUT_DATE <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                        ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "禅城区石湾社区卫生服务中心"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "南庄社区卫生服务中心"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "张槎街道社区卫生服务中心")
            {
                /**
                 * 将物资系统 与 非药品 对照起来
                 * 
                 * FIN_COM_UNDRUGINFO.OTHER_CUSTOM 对应的是物资系统 MAT_COM_BASEINFO.ITEM_CODE
                 * 
                 * */

                #region 佛山市第四人民医院 禅城区石湾社区卫生服务中心 佛山市第一人民医院禅城医院 南庄社区卫生服务中心 张槎街道社区卫生服务中心

                strSql = @"SELECT FSCMMDID, --  佛山耗材采购平台商品编号
                                   YGCMMDID, --  佛山阳光采购平台商品编号
                                   HCMMDID, --  医院耗材采购商品编号
                                   DEPTID,  --使用科室编码
                                   DOCTORCODE, --  医师编码
                                   HISNUMBER, --  住院号/门诊号
                                   PATIENTINSURANCECODE, --  患者社保编号
                                   PATIENTNAME, --  患者姓名
                                   DIAGNOSISDATE, --  就诊时间
                                   LOTNO, --  生产批号
                                   MANUDATE, --  生产日期
                                   EXPIREDDATE, --  有效日期
                                   SUM(USEAMOUNT) QTY, --  使用量
                                   DEPTNAME, --  使用科室名称
                                   DOCTORNAME --  医师姓名
                                   
                              FROM (SELECT K.CG_ProductID  FSCMMDID,
                                           K.YG_ProductID YGCMMDID,
                                           K.ITEM_CODE HCMMDID,
                                           D.DOCT_DEPT DEPTID,
                                           FUN_GET_DEPT_NAME(D.DOCT_DEPT) DEPTNAME,
                                           D.DOCT_CODE DOCTORCODE,
                                           FUN_GET_EMPLOYEE_NAME(D.DOCT_CODE) DOCTORNAME,
                                           R.CARD_NO HISNUMBER,
                                           R.MCARD_NO PATIENTINSURANCECODE,
                                           R.NAME PATIENTNAME,
                                           D.FEE_DATE DIAGNOSISDATE,
                                           '' LOTNO,
                                           '' MANUDATE,
                                           '' EXPIREDDATE,
                                           D.QTY USEAMOUNT
                                      FROM FIN_OPB_FEEDETAIL  D,
                                           FIN_COM_UNDRUGINFO T,
                                           MAT_COM_BASEINFO   K,
                                           FIN_OPR_REGISTER   R
                                     WHERE D.ITEM_CODE = T.ITEM_CODE
                                       AND D.CLINIC_CODE = R.CLINIC_CODE
                                       AND T.OTHER_CUSTOM = K.ITEM_CODE
                                       AND D.PAY_FLAG = '1'
                                       AND K.ITEM_CODE = '{0}'
                                       AND D.FEE_DATE >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                       AND D.FEE_DATE <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')

                                       
                                    UNION ALL
                                    
                                    SELECT K.CG_ProductID  FSCMMDID,
                                           K.YG_ProductID YGCMMDID,
                                           K.ITEM_CODE HCMMDID,
                                           D.RECIPE_DEPTCODE DEPTID,
                                           FUN_GET_DEPT_NAME(D.RECIPE_DEPTCODE) DEPTNAME,
                                           D.RECIPE_DOCCODE DOCTORCODE,
                                           FUN_GET_EMPLOYEE_NAME(D.RECIPE_DOCCODE) DOCTORNAME,
                                           R.PATIENT_NO HISNUMBER,
                                           R.MCARD_NO PATIENTINSURANCECODE,
                                           R.NAME PATIENTNAME,
                                           D.FEE_DATE DIAGNOSISDATE,
                                           '' LOTNO,
                                           '' MANUDATE,
                                           '' EXPIREDDATE,
                                           D.QTY USEAMOUNT
                                      FROM FIN_IPB_ITEMLIST   D,
                                           FIN_COM_UNDRUGINFO T,
                                           MAT_COM_BASEINFO   K,
                                           FIN_IPR_INMAININFO R
                                     WHERE D.ITEM_CODE = T.ITEM_CODE
                                       AND D.INPATIENT_NO = R.INPATIENT_NO
                                       AND T.OTHER_CUSTOM = K.ITEM_CODE
                                       AND K.ITEM_CODE = '{0}'
                                       AND D.FEE_DATE >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                       AND D.FEE_DATE <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                             )
                             GROUP BY FSCMMDID,
                                      YGCMMDID,
                                      HCMMDID,
                                      DEPTID,
                                      DEPTNAME,
                                      DOCTORCODE,
                                      DOCTORNAME,
                                      HISNUMBER,
                                      PATIENTINSURANCECODE,
                                      PATIENTNAME,
                                      DIAGNOSISDATE,
                                      LOTNO,
                                      MANUDATE,
                                      EXPIREDDATE
                                ";

                #endregion
            }

            strSql = string.Format(strSql, matId, dtBegin.ToString(),dtEnd.ToString());
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Neusoft.HISFC.BizLogic.Material.Object.Output outPut = null;
            ArrayList alOutPut = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    outPut = new Neusoft.HISFC.BizLogic.Material.Object.Output();

                    outPut.BaseInfo.UserCode = Reader[0].ToString(); //佛山耗材采购平台商品编号
                    outPut.BaseInfo.Memo = Reader[1].ToString();//佛山阳光采购平台商品编号

                    outPut.BaseInfo.ID = Reader[2].ToString();//医院耗材采购商品编号

                    outPut.ApproveOper.Dept.ID = Reader[3].ToString();//科室
                    outPut.ApproveOper.Oper.ID = Reader[4].ToString();//医生

                    outPut.Oper.ID = Reader[5].ToString();//患者号
                    outPut.Oper.Memo = Reader[6].ToString();//医保号
                    outPut.Oper.Name = Reader[7].ToString();//姓名
                    outPut.OutDate = Convert.ToDateTime(Reader[8]);//时间
                    outPut.StockBatchNo = Reader[9].ToString();//批号

                    if (!string.IsNullOrEmpty(Reader[10].ToString()))
                    {
                        outPut.ApplyOper.OperTime = Convert.ToDateTime(Reader[10].ToString());//生产时间
                    }
                    if (!string.IsNullOrEmpty(Reader[11].ToString()))
                    {
                        outPut.StockValidDate = Convert.ToDateTime(Reader[11]);//有效期
                    }

                    outPut.OutNum = Convert.ToDecimal(Reader[12]);//数量

                    outPut.ApproveOper.Dept.Name = Reader[13].ToString();//科室
                    outPut.ApproveOper.Oper.Name = Reader[14].ToString();//医生

                    alOutPut.Add(outPut);
                }

                return alOutPut;
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
        /// 查询医用耗材的入库信息
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="matId"></param>
        /// <returns></returns>
        public ArrayList QueryMaterialInputList(DateTime dtBegin, DateTime dtEnd, string matId)
        {
            string strSql = string.Empty;

            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                #region 佛山市第三人民医院

                strSql = @" SELECT T.IN_BILL_CODE,
                                    T.IN_LIST_CODE,
                                    (select m.fac_name
                                       from pha_com_company m
                                      where m.fac_code = T.Company_Code) Comp,
                                    (select m.fac_name
                                       from pha_com_company m
                                      where m.fac_code = T.Company_Code) senderName,
                                     T.DRUG_DEPT_CODE,
                                     (select t.dept_name from com_department t where t.dept_code = T.DRUG_DEPT_CODE) deptName,
                                    (SELECT P.Cg_Productid
                                  FROM PHA_COM_BASEINFO P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE
                                   AND ROWNUM = 1) platCode, 
                                 (SELECT P.Yg_Productid
                                  FROM PHA_COM_BASEINFO P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE
                                   AND ROWNUM = 1) sunCode,
                                    T.DRUG_CODE,
                                    T.PURCHASE_PRICE,
                                    T.APPLY_NUM,
                                    T.APPLY_DATE,
                                    T.IN_NUM,
                                    T.IN_DATE,
                                    T.BATCH_NO,
                                    T.VALID_DATE,
                                    t.purchase_cost
                               FROM PHA_COM_INPUT T 
                              WHERE T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                AND T.DRUG_CODE = '{2}'
                                AND T.IN_NUM > 0   --入库
                                AND (SELECT c.DEPT_TYPE FROM COM_DEPARTMENT c WHERE c.DEPT_CODE=T.DRUG_DEPT_CODE)='PI'
                                AND T.SOURCE_COMPANY_TYPE = '2'";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                #region 佛山市第四人民医院

                strSql = @"SELECT T.IN_NO,
                                   T.IN_LIST_CODE,
                                   (SELECT c.COMPANY_NAME FROM MAT_COM_COMPANY c WHERE c.COMPANY_CODE=T.FACTORY_CODE AND c.COMPANY_TYPE='0') facName,
                                   T.COMPANY_NAME,
                                   T.STORAGE_CODE,
                                   FUN_GET_DEPT_NAME(T.STORAGE_CODE) STOR,
                                   
                                   (SELECT K.CG_ProductID  
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') PLATCODE,
                                    (SELECT K.YG_ProductID
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') SUNCODE,   
                                   T.ITEM_CODE,
                                   T.IN_PRICE,
                                   T.IN_NUM, --采购数量
                                   T.IN_DATE, --采购日期
                                   T.IN_NUM,
                                   T.IN_DATE,
                                   T.BATCH_NO,
                                   T.VALID_DATE,
                                   T.IN_COST
                              FROM MAT_COM_INPUT T 
                             WHERE T.STORAGE_CODE = '2006'
                               AND T.IN_NUM > 0 --入库
                               AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                               AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                               AND T.ITEM_CODE = '{2}'
                                ";

                #endregion
            }

            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "禅城区石湾社区卫生服务中心")
            {
                #region 禅城区石湾社区卫生服务中心

                strSql = @"SELECT T.IN_NO,
                                   T.IN_LIST_CODE,
                                   (SELECT c.COMPANY_NAME FROM MAT_COM_COMPANY c WHERE c.COMPANY_CODE=T.FACTORY_CODE AND c.COMPANY_TYPE='0') facName,
                                   T.COMPANY_NAME,
                                   T.STORAGE_CODE,
                                   FUN_GET_DEPT_NAME(T.STORAGE_CODE) STOR,
                                   
                                   (SELECT K.CG_ProductID  
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') PLATCODE,
                                    (SELECT K.YG_ProductID
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') SUNCODE,   
                                   T.ITEM_CODE,
                                   T.IN_PRICE,
                                   T.IN_NUM, --采购数量
                                   T.IN_DATE, --采购日期
                                   T.IN_NUM,
                                   T.IN_DATE,
                                   T.BATCH_NO,
                                   T.VALID_DATE,
                                   T.IN_COST
                              FROM MAT_COM_INPUT T 
                             WHERE T.STORAGE_CODE = '2004'
                               AND T.IN_NUM > 0 --入库
                               AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                               AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                               AND T.ITEM_CODE = '{2}'
                                ";

                #endregion
            }

            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                #region 佛山市第一人民医院禅城医院 

                strSql = @"SELECT T.IN_NO,
                                   T.IN_LIST_CODE,
                                   (SELECT c.COMPANY_NAME FROM MAT_COM_COMPANY  c WHERE c.COMPANY_CODE=T.FACTORY_CODE AND c.COMPANY_TYPE='0') facName,
                                   T.COMPANY_NAME,
                                   T.STORAGE_CODE,
                                   FUN_GET_DEPT_NAME(T.STORAGE_CODE) STOR,
                                   
                                   (SELECT K.CG_ProductID  
                                      FROM MAT_COM_BASEINFO  K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') PLATCODE,
                                    (SELECT K.YG_ProductID
                                      FROM MAT_COM_BASEINFO  K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') SUNCODE,   
                                   T.ITEM_CODE,
                                   T.IN_PRICE,
                                   T.IN_NUM, --采购数量
                                   T.IN_DATE, --采购日期
                                   T.IN_NUM,
                                   T.IN_DATE,
                                   T.BATCH_NO,
                                   T.VALID_DATE,
                                   T.IN_COST
                              FROM MAT_COM_INPUT  T 
                             WHERE T.STORAGE_CODE = '7012'
                               AND T.IN_NUM > 0 --入库
                               AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                               AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                               AND T.ITEM_CODE = '{2}'
                                ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "张槎街道社区卫生服务中心")
            {
                #region 张槎街道社区卫生服务中心

                strSql = @"SELECT T.IN_NO,
                                   T.IN_LIST_CODE,
                                   (SELECT c.COMPANY_NAME FROM MAT_COM_COMPANY c WHERE c.COMPANY_CODE=T.FACTORY_CODE AND c.COMPANY_TYPE='0') facName,
                                   T.COMPANY_NAME,
                                   T.STORAGE_CODE,
                                   FUN_GET_DEPT_NAME(T.STORAGE_CODE) STOR,
                                   
                                   (SELECT K.CG_ProductID 
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') PLATCODE,
                                    (SELECT K.YG_ProductID 
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') SUNCODE,   
                                   T.ITEM_CODE,
                                   T.IN_PRICE,
                                   T.IN_NUM, --采购数量
                                   T.IN_DATE, --采购日期
                                   T.IN_NUM,
                                   T.IN_DATE,
                                   T.BATCH_NO,
                                   T.VALID_DATE,
                                   T.IN_COST
                              FROM MAT_COM_INPUT T 
                             WHERE T.STORAGE_CODE = '9007'
                               AND T.IN_NUM > 0 --入库
                               AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                               AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                               AND T.ITEM_CODE = '{2}'
                                ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "南庄社区卫生服务中心")
            {
                #region 南庄社区卫生服务中心

                strSql = @"SELECT T.IN_NO,
                                   T.IN_LIST_CODE,
                                   (SELECT c.COMPANY_NAME FROM MAT_COM_COMPANY@nzhis  c WHERE c.COMPANY_CODE=T.FACTORY_CODE AND c.COMPANY_TYPE='0') facName,
                                   T.COMPANY_NAME,
                                   T.STORAGE_CODE,
                                   FUN_GET_DEPT_NAME(T.STORAGE_CODE) STOR,
                                   
                                   (SELECT K.CG_ProductID
                                      FROM MAT_COM_BASEINFO@nzhis  K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') PLATCODE,
                                    (SELECT K.YG_ProductID 
                                      FROM MAT_COM_BASEINFO@nzhis  K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') SUNCODE,   
                                   T.ITEM_CODE,
                                   T.IN_PRICE,
                                   T.IN_NUM, --采购数量
                                   T.IN_DATE, --采购日期
                                   T.IN_NUM,
                                   T.IN_DATE,
                                   T.BATCH_NO,
                                   T.VALID_DATE,
                                   T.IN_COST
                              FROM MAT_COM_INPUT@nzhis  T 
                             WHERE T.STORAGE_CODE = '7012'
                               AND T.IN_NUM > 0 --入库
                               AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                               AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                               AND T.ITEM_CODE = '{2}'
                                ";

                #endregion
            }

            strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString(), matId);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Neusoft.HISFC.BizLogic.Material.Object.Input input = null;
            ArrayList alInput = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    input = new Neusoft.HISFC.BizLogic.Material.Object.Input();

                    input.InListCode = Reader[0].ToString(); //订单号【流水号】
                    input.OutNo = Reader[1].ToString();//院内单据号

                    input.StockInfo.Company.Memo = Reader[2].ToString();//供货公司
                    input.StockInfo.Company.Name = Reader[3].ToString();//送货公司

                    input.StockInfo.Storage.ID = Reader[4].ToString();  //库房编码
                    input.StockInfo.Storage.Name = Reader[5].ToString();//库房名称

                    input.StockInfo.BaseInfo.PlatMatCode = Reader[6].ToString(); //佛山耗材采购平台商品编号
                    input.StockInfo.BaseInfo.SunMatCode = Reader[7].ToString(); //佛山阳光采购平台商品编号
                    input.StockInfo.BaseInfo.ID = Reader[8].ToString();//院内编码

                    input.InPrice = Convert.ToDecimal(Reader[9].ToString());//采购价格
                    input.StockInfo.LowNum = Convert.ToDecimal(Reader[10].ToString());//采购数量
                    input.ApplyOper.OperTime = Convert.ToDateTime(Reader[11].ToString());  //采购日期


                    input.InNum = Convert.ToDecimal(Reader[12].ToString());//入库数量
                    input.InDate = Convert.ToDateTime(Reader[13].ToString());//入库日期
                    input.StockInfo.BatchNo = Reader[14].ToString();//批号
                    input.StockInfo.ValidDate = Convert.ToDateTime(Reader[15]);//有效期

                    input.InCost = Convert.ToDecimal(Reader[16].ToString());//采购金额

                    alInput.Add(input);
                }

                return alInput;
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
        /// 查询医用耗材的退库信息
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="matId"></param>
        /// <returns></returns>
        public ArrayList QueryMaterialReturnList(DateTime dtBegin, DateTime dtEnd, string matId)
        {
            string strSql = string.Empty;

            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                #region 佛山市第三人民医院

                strSql = @" SELECT T.IN_BILL_CODE,
                                    T.IN_LIST_CODE,
                                    (select m.fac_name
                                       from pha_com_company m
                                      where m.fac_code = T.Company_Code) Comp,
                                    (select m.fac_name
                                       from pha_com_company m
                                      where m.fac_code = T.Company_Code) senderName,
                                     T.DRUG_DEPT_CODE,
                                     (select t.dept_name from com_department t where t.dept_code = T.DRUG_DEPT_CODE) deptName,
                                    (SELECT P.Cg_Productid
                                  FROM PHA_COM_BASEINFO P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE
                                   AND ROWNUM = 1) platCode, 
                                 (SELECT P.Yg_Productid
                                  FROM PHA_COM_BASEINFO P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE
                                   AND ROWNUM = 1) sunCode,
                                    T.DRUG_CODE,
                                    T.PURCHASE_PRICE,
                                    T.APPLY_NUM,
                                    T.APPLY_DATE,
                                    T.IN_NUM,
                                    T.IN_DATE,
                                    T.BATCH_NO,
                                    T.VALID_DATE,
                                    t.purchase_cost,
                                    (
                                    SELECT max(p.IN_BILL_CODE) FROM PHA_COM_INPUT p
                                    WHERE p.DRUG_CODE = T.DRUG_CODE
                                    AND p.BATCH_NO = T.BATCH_NO
                                    AND p.GROUP_CODE = T.GROUP_CODE
                                    AND (p.PRODUCER_CODE IS NULL OR p.PRODUCER_CODE=t.PRODUCER_CODE)
                                    AND p.COMPANY_CODE=t.COMPANY_CODE
                                    AND p.SOURCE_COMPANY_TYPE = '2'
                                    AND p.IN_NUM > 0
                                    )  RTNLISTCODE,
                                    (
                                    SELECT max(p.IN_LIST_CODE) FROM PHA_COM_INPUT p
                                    WHERE p.DRUG_CODE = T.DRUG_CODE
                                    AND p.BATCH_NO = T.BATCH_NO
                                    AND p.GROUP_CODE = T.GROUP_CODE
                                    AND (p.PRODUCER_CODE IS NULL OR p.PRODUCER_CODE=t.PRODUCER_CODE)
                                    AND p.COMPANY_CODE=t.COMPANY_CODE
                                    AND p.SOURCE_COMPANY_TYPE = '2'
                                    AND p.IN_NUM > 0
                                    )  RTNSERIALCODE
                               FROM PHA_COM_INPUT T 
                              WHERE T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                AND T.DRUG_CODE = '{2}'
                                AND T.IN_NUM < 0   --退库
                                AND (SELECT c.DEPT_TYPE FROM COM_DEPARTMENT c WHERE c.DEPT_CODE=T.DRUG_DEPT_CODE)='PI'
                                AND T.SOURCE_COMPANY_TYPE = '2'";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                #region 佛山市第四人民医院

                strSql = @"SELECT T.IN_NO,
                                   T.IN_LIST_CODE,
                                   (SELECT c.COMPANY_NAME FROM MAT_COM_COMPANY c WHERE c.COMPANY_CODE=T.FACTORY_CODE AND c.COMPANY_TYPE='0') facName,
                                   T.COMPANY_NAME,
                                   T.STORAGE_CODE,
                                   FUN_GET_DEPT_NAME(T.STORAGE_CODE) STOR,
                                   
                                   (SELECT K.CG_ProductID 
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') PLATCODE,
                                    (SELECT K.YG_ProductID 
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') SUNCODE,   
                                   T.ITEM_CODE,
                                   T.IN_PRICE,
                                   T.IN_NUM, --采购数量
                                   T.IN_DATE, --采购日期
                                   T.IN_NUM,
                                   T.IN_DATE,
                                   T.BATCH_NO,
                                   T.VALID_DATE,
                                   T.IN_COST,
                                   (SELECT K.IN_NO
                                      FROM MAT_COM_INPUT K
                                     WHERE K.STORAGE_CODE = '2006'
                                       AND K.IN_NO = T.RETURN_IN_NO) RTNLISTCODE,
                                   (SELECT K.IN_LIST_CODE
                                      FROM MAT_COM_INPUT K
                                     WHERE K.STORAGE_CODE = '2006'
                                       AND K.IN_NO = T.RETURN_IN_NO) RTNSERIALCODE

                              FROM MAT_COM_INPUT T 
                             WHERE T.STORAGE_CODE = '2006'
                               AND T.IN_NUM < 0 --退库
                               AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                               AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                               AND T.ITEM_CODE = '{2}'
                                ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "禅城区石湾社区卫生服务中心")
            {
                #region 禅城区石湾社区卫生服务中心

                strSql = @"SELECT T.IN_NO,
                                   T.IN_LIST_CODE,
                                   (SELECT c.COMPANY_NAME FROM MAT_COM_COMPANY c WHERE c.COMPANY_CODE=T.FACTORY_CODE AND c.COMPANY_TYPE='0') facName,
                                   T.COMPANY_NAME,
                                   T.STORAGE_CODE,
                                   FUN_GET_DEPT_NAME(T.STORAGE_CODE) STOR,
                                   
                                   (SELECT K.CG_ProductID 
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') PLATCODE,
                                    (SELECT K.YG_ProductID 
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') SUNCODE,   
                                   T.ITEM_CODE,
                                   T.IN_PRICE,
                                   T.IN_NUM, --采购数量
                                   T.IN_DATE, --采购日期
                                   T.IN_NUM,
                                   T.IN_DATE,
                                   T.BATCH_NO,
                                   T.VALID_DATE,
                                   T.IN_COST,
                                   (SELECT K.IN_NO
                                      FROM MAT_COM_INPUT K
                                     WHERE K.STORAGE_CODE = '2004'
                                       AND K.IN_NO = T.RETURN_IN_NO) RTNLISTCODE,
                                   (SELECT K.IN_LIST_CODE
                                      FROM MAT_COM_INPUT K
                                     WHERE K.STORAGE_CODE = '2004'
                                       AND K.IN_NO = T.RETURN_IN_NO) RTNSERIALCODE

                              FROM MAT_COM_INPUT T 
                             WHERE T.STORAGE_CODE = '2004'
                               AND T.IN_NUM < 0 --退库
                               AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                               AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                               AND T.ITEM_CODE = '{2}'
                                ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                #region 佛山市第一人民医院禅城医院

                strSql = @"SELECT T.IN_NO,
                                   T.IN_LIST_CODE,
                                   (SELECT c.COMPANY_NAME FROM MAT_COM_COMPANY  c WHERE c.COMPANY_CODE=T.FACTORY_CODE AND c.COMPANY_TYPE='0') facName,
                                   T.COMPANY_NAME,
                                   T.STORAGE_CODE,
                                   FUN_GET_DEPT_NAME(T.STORAGE_CODE) STOR,
                                   
                                   (SELECT K.CG_ProductID
                                      FROM MAT_COM_BASEINFO  K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') PLATCODE,
                                    (SELECT K.YG_ProductID  
                                      FROM MAT_COM_BASEINFO  K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') SUNCODE,   
                                   T.ITEM_CODE,
                                   T.IN_PRICE,
                                   T.IN_NUM, --采购数量
                                   T.IN_DATE, --采购日期
                                   T.IN_NUM,
                                   T.IN_DATE,
                                   T.BATCH_NO,
                                   T.VALID_DATE,
                                   T.IN_COST,
                                   (SELECT K.IN_NO
                                      FROM MAT_COM_INPUT  K
                                     WHERE K.STORAGE_CODE = '7012'
                                       AND K.IN_NO = T.RETURN_IN_NO) RTNLISTCODE,
                                   (SELECT K.IN_LIST_CODE
                                      FROM MAT_COM_INPUT  K
                                     WHERE K.STORAGE_CODE = '7012'
                                       AND K.IN_NO = T.RETURN_IN_NO) RTNSERIALCODE

                              FROM MAT_COM_INPUT  T 
                             WHERE T.STORAGE_CODE = '7012'
                               AND T.IN_NUM < 0 --退库
                               AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                               AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                               AND T.ITEM_CODE = '{2}'
                                ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "南庄社区卫生服务中心")
            {
                #region 南庄社区卫生服务中心

                strSql = @"SELECT T.IN_NO,
                                   T.IN_LIST_CODE,
                                   (SELECT c.COMPANY_NAME FROM MAT_COM_COMPANY@nzhis  c WHERE c.COMPANY_CODE=T.FACTORY_CODE AND c.COMPANY_TYPE='0') facName,
                                   T.COMPANY_NAME,
                                   T.STORAGE_CODE,
                                   FUN_GET_DEPT_NAME(T.STORAGE_CODE) STOR,
                                   
                                   (SELECT K.CG_ProductID  
                                      FROM MAT_COM_BASEINFO@nzhis  K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') PLATCODE,
                                    (SELECT K.YG_ProductID
                                      FROM MAT_COM_BASEINFO@nzhis  K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') SUNCODE,   
                                   T.ITEM_CODE,
                                   T.IN_PRICE,
                                   T.IN_NUM, --采购数量
                                   T.IN_DATE, --采购日期
                                   T.IN_NUM,
                                   T.IN_DATE,
                                   T.BATCH_NO,
                                   T.VALID_DATE,
                                   T.IN_COST,
                                   (SELECT K.IN_NO
                                      FROM MAT_COM_INPUT@nzhis  K
                                     WHERE K.STORAGE_CODE = '7012'
                                       AND K.IN_NO = T.RETURN_IN_NO) RTNLISTCODE,
                                   (SELECT K.IN_LIST_CODE
                                      FROM MAT_COM_INPUT@nzhis  K
                                     WHERE K.STORAGE_CODE = '7012'
                                       AND K.IN_NO = T.RETURN_IN_NO) RTNSERIALCODE

                              FROM MAT_COM_INPUT@nzhis  T 
                             WHERE T.STORAGE_CODE = '7012'
                               AND T.IN_NUM < 0 --退库
                               AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                               AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                               AND T.ITEM_CODE = '{2}'
                                ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "张槎街道社区卫生服务中心")
            {
                #region 张槎街道社区卫生服务中心

                strSql = @"SELECT T.IN_NO,
                                   T.IN_LIST_CODE,
                                   (SELECT c.COMPANY_NAME FROM MAT_COM_COMPANY c WHERE c.COMPANY_CODE=T.FACTORY_CODE AND c.COMPANY_TYPE='0') facName,
                                   T.COMPANY_NAME,
                                   T.STORAGE_CODE,
                                   FUN_GET_DEPT_NAME(T.STORAGE_CODE) STOR,
                                   
                                   (SELECT K.CG_ProductID 
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') PLATCODE,
                                    (SELECT K.YG_ProductID 
                                      FROM MAT_COM_BASEINFO K
                                     WHERE K.ITEM_CODE = T.ITEM_CODE
                                       AND K.STORAGE_CODE = '0048') SUNCODE,   
                                   T.ITEM_CODE,
                                   T.IN_PRICE,
                                   T.IN_NUM, --采购数量
                                   T.IN_DATE, --采购日期
                                   T.IN_NUM,
                                   T.IN_DATE,
                                   T.BATCH_NO,
                                   T.VALID_DATE,
                                   T.IN_COST,
                                   (SELECT K.IN_NO
                                      FROM MAT_COM_INPUT K
                                     WHERE K.STORAGE_CODE = '9007'
                                       AND K.IN_NO = T.RETURN_IN_NO) RTNLISTCODE,
                                   (SELECT K.IN_LIST_CODE
                                      FROM MAT_COM_INPUT K
                                     WHERE K.STORAGE_CODE = '9007'
                                       AND K.IN_NO = T.RETURN_IN_NO) RTNSERIALCODE

                              FROM MAT_COM_INPUT T 
                             WHERE T.STORAGE_CODE = '9007'
                               AND T.IN_NUM < 0 --退库
                               AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                               AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                               AND T.ITEM_CODE = '{2}'
                                ";

                #endregion
            }

            strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString(), matId);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Neusoft.HISFC.BizLogic.Material.Object.Input input = null;
            ArrayList alInput = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    input = new Neusoft.HISFC.BizLogic.Material.Object.Input();

                    input.InListCode = Reader[0].ToString(); //订单号【流水号】
                    input.OutNo = Reader[1].ToString();//院内单据号

                    input.StockInfo.Company.Memo = Reader[2].ToString();//供货公司
                    input.StockInfo.Company.Name = Reader[3].ToString();//送货公司

                    input.StockInfo.Storage.ID = Reader[4].ToString();  //库房编码
                    input.StockInfo.Storage.Name = Reader[5].ToString();//库房名称

                    input.StockInfo.BaseInfo.PlatMatCode = Reader[6].ToString(); //佛山耗材采购平台商品编号
                    input.StockInfo.BaseInfo.SunMatCode = Reader[7].ToString(); //佛山阳光采购平台商品编号
                    input.StockInfo.BaseInfo.ID = Reader[8].ToString();//院内编码

                    input.InPrice = Convert.ToDecimal(Reader[9].ToString());//采购价格
                    input.StockInfo.LowNum = Convert.ToDecimal(Reader[10].ToString());//采购数量
                    input.ApplyOper.OperTime = Convert.ToDateTime(Reader[11].ToString());  //采购日期


                    input.InNum = Convert.ToDecimal(Reader[12].ToString());//入库数量
                    input.InDate = Convert.ToDateTime(Reader[13].ToString());//入库日期
                    input.StockInfo.BatchNo = Reader[14].ToString();//批号
                    input.StockInfo.ValidDate = Convert.ToDateTime(Reader[15]);//有效期

                    input.InCost = Convert.ToDecimal(Reader[16].ToString());//采购金额

                    input.ReturnInNo = Reader[17].ToString();   //入库单号
                    input.Memo = Reader[18].ToString();         //入库单号

                    alInput.Add(input);
                }

                return alInput;
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
        /// 查询医用耗材的发票信息
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public ArrayList QueryMaterialInvoiceInfo(DateTime dtBegin, DateTime dtEnd, string matId)
        {
            string strSql = string.Empty;

            if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院"
                || Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市中医院禅城高新区医院")
            {
                #region 佛山市第三人民医院

                strSql = @"SELECT '',
                                       T.INVOICE_NO,
                                        (select m.fac_name
                                           from pha_com_company m
                                          where m.fac_code = T.Company_Code) Comp,
                                       T.PURCHASE_COST,
                                       T.INVOICE_DATE,
                                       DECODE(T.IN_TYPE, '06', '20', '10') INVOICETYPE,
                                       '采购发票' INVOICE_NAME,
                                       '',
                                       T.IN_LIST_CODE,
                                       T.IN_BILL_CODE
                                  FROM PHA_COM_INPUT T
                                 WHERE T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')    --应该使用INVOICE_DATE
                                   AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')    --应该使用INVOICE_DATE
                                   AND T.DRUG_CODE = '{2}'
                                   AND T.INVOICE_NO IS NOT NULL  ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第四人民医院")
            {
                #region 佛山市第四人民医院

                strSql = @"
                            SELECT '', 
                            T.INVOICE_NO,
                            T.COMPANY_NAME,
                            T.IN_COST,
                            T.INVOICE_DATE,
                            DECODE(T.TRANS_TYPE, '2', '20', '10') itype,
                            '采购发票',
                            '',
                            T.IN_LIST_CODE,
                            T.IN_NO
                            FROM MAT_COM_INPUT T
                            WHERE T.STORAGE_CODE = '2006'
                            AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                            AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                            AND T.ITEM_CODE = '{2}'
                            AND T.INVOICE_NO IS NOT NULL ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "禅城区石湾社区卫生服务中心")
            {
                #region 禅城区石湾社区卫生服务中心

                strSql = @"
                            SELECT '', 
                            T.INVOICE_NO,
                            T.COMPANY_NAME,
                            T.IN_COST,
                            T.INVOICE_DATE,
                            DECODE(T.TRANS_TYPE, '2', '20', '10') itype,
                            '采购发票',
                            '',
                            T.IN_LIST_CODE,
                            T.IN_NO
                            FROM MAT_COM_INPUT T
                            WHERE T.STORAGE_CODE = '2004'
                            AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                            AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                            AND T.ITEM_CODE = '{2}'
                            AND T.INVOICE_NO IS NOT NULL ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "佛山市第一人民医院禅城医院")
            {
                #region 佛山市第一人民医院禅城医院

                strSql = @"
                            SELECT '', 
                            T.INVOICE_NO,
                            T.COMPANY_NAME,
                            T.IN_COST,
                            T.INVOICE_DATE,
                            DECODE(T.TRANS_TYPE, '2', '20', '10') itype,
                            '采购发票',
                            '',
                            T.IN_LIST_CODE,
                            T.IN_NO
                            FROM MAT_COM_INPUT  T
                            WHERE T.STORAGE_CODE = '7012'
                            AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                            AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                            AND T.ITEM_CODE = '{2}'
                            AND T.INVOICE_NO IS NOT NULL ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "南庄社区卫生服务中心")
            {
                #region 南庄社区卫生服务中心

                strSql = @"
                            SELECT '', 
                            T.INVOICE_NO,
                            T.COMPANY_NAME,
                            T.IN_COST,
                            T.INVOICE_DATE,
                            DECODE(T.TRANS_TYPE, '2', '20', '10') itype,
                            '采购发票',
                            '',
                            T.IN_LIST_CODE,
                            T.IN_NO
                            FROM MAT_COM_INPUT@nzhis  T
                            WHERE T.STORAGE_CODE = '7012'
                            AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                            AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                            AND T.ITEM_CODE = '{2}'
                            AND T.INVOICE_NO IS NOT NULL ";

                #endregion
            }
            else if (Neusoft.FrameWork.Management.Connection.Hospital.Name == "张槎街道社区卫生服务中心")
            {
                #region 张槎街道社区卫生服务中心

                strSql = @"
                            SELECT '', 
                            T.INVOICE_NO,
                            T.COMPANY_NAME,
                            T.IN_COST,
                            T.INVOICE_DATE,
                            DECODE(T.TRANS_TYPE, '2', '20', '10') itype,
                            '采购发票',
                            '',
                            T.IN_LIST_CODE,
                            T.IN_NO
                            FROM MAT_COM_INPUT T
                            WHERE T.STORAGE_CODE = '9007'
                            AND T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                            AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                            AND T.ITEM_CODE = '{2}'
                            AND T.INVOICE_NO IS NOT NULL ";

                #endregion
            }


            strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString(), matId);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Neusoft.HISFC.BizLogic.Material.Object.Input input = null;
            ArrayList alInput = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    input = new Neusoft.HISFC.BizLogic.Material.Object.Input();

                    input.Oper.ID = Reader[0].ToString(); //采购平台发票
                    input.InvoiceNo = Reader[1].ToString(); //院内发票
                    input.StockInfo.Company.Name = Reader[2].ToString(); //配送企业
                    input.InCost = Convert.ToDecimal(Reader[3].ToString());//明细金额

                    input.InvoiceDate = Convert.ToDateTime(Reader[4].ToString()); //发票时间
                    input.Oper.Memo = Reader[5].ToString();//发票类型
                    input.Name = Reader[6].ToString();//发票名称

                    input.BuyOper.Memo = Reader[7].ToString();

                    input.OutNo = Reader[8].ToString();//院内订单编明细号
                    input.InListCode = Reader[9].ToString();//院内订单号【流水号】

                    alInput.Add(input);
                }

                return alInput;
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

    }
}
