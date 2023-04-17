using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using Neusoft.HISFC.Models;
using Neusoft.FrameWork.Models;
using Neusoft.FrameWork.Function;
using Neusoft.HISFC.Models.Base;

namespace FoShanDiseasePay.BizLogic
{
    /// <summary>
    /// 药品业务管理类
    /// </summary>
    public class DrugManager : Neusoft.HISFC.BizLogic.Pharmacy.Item
    {
        /// <summary>
        /// 根据入库时间查询药品入库信息
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryDrugBaseInfoByDate(string queryCode,DateTime begTime,DateTime endTime,string deptCode)
        {
            string strSql = @"SELECT   t.DRUG_CODE 药品编码,
                                       t.TRADE_NAME 药品名称,
                                       t.CUSTOM_CODE 自定义码,
                                       t.GB_CODE 国标码,
                                       t.SPECS 规格,
                                       t.RETAIL_PRICE 零售价,
                                       t.APPROVE_INFO 批文号,
                                       (SELECT p.FAC_NAME FROM PHA_COM_COMPANY p WHERE p.FAC_CODE = t.PRODUCER_CODE) 厂家,
                                       (SELECT p.FAC_NAME FROM PHA_COM_COMPANY p WHERE p.FAC_CODE = t.company_code) 供货商,
                                       (SELECT sum(s.STORE_SUM) FROM PHA_COM_STOCKINFO s WHERE s.DRUG_CODE = t.DRUG_CODE) 库存,
                                       i.invoice_no 发票号,
                                       '9999'
                                FROM PHA_COM_BASEINFO t,pha_com_input i
                                WHERE t.VALID_STATE = '1'
                                  AND t.FEE_CODE IN ('001','002')                                  
                                  and (t.drug_code like '%{0}%' or t.trade_name like '%{0}%'
                                  or t.CUSTOM_CODE like '%{0}%' or t.spell_code like '%{0}%'
                                  or t.wb_code like '%{0}%' or t.GB_CODE like '%{0}%')
                                  and t.drug_code = i.drug_code
                                  and i.in_date>= to_date('{1}','yyyy-mm-dd hh24:mi:ss') 
                                  and i.in_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                  and (i.drug_dept_code = '{3}' or '{3}' is null)
                                  and i.in_type in ('01','20')--一般入库、即入即出
                                  --and i.class3_meaning_code = '11'
                                  and t.is_upload = '1'
                                ORDER BY i.company_code,t.drug_code
                                ";
            strSql = string.Format(strSql, queryCode, begTime.ToString(), endTime.ToString(), deptCode);// {385E49F2-947B-43e3-931D-BE89481BA68C}
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            ArrayList alDrug = new ArrayList();
            Neusoft.HISFC.Models.Pharmacy.Item drug = null;
            try
            {
                while (this.Reader.Read())
                {
                    drug = new Neusoft.HISFC.Models.Pharmacy.Item();

                    drug.ID = this.Reader[0].ToString().Trim();
                    drug.Name = this.Reader[1].ToString().Trim();
                    drug.UserCode = this.Reader[2].ToString();
                    drug.NameCollection.GbCode = this.Reader[3].ToString();
                    drug.Specs = this.Reader[4].ToString();
                    drug.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[5].ToString());
                    drug.Product.ApprovalInfo = this.Reader[6].ToString();
                    drug.Product.Producer.Name = this.Reader[7].ToString();
                    drug.Product.Company.Name = this.Reader[8].ToString();
                    drug.Memo = this.Reader[9].ToString();   //库存信息
                    drug.Product.Memo = this.Reader[10].ToString();   //发票号
                    drug.Product.User01 = this.Reader[11].ToString();   //上传标志

                    alDrug.Add(drug);
                }
                return alDrug;
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
        /// 查询药品的基本信息
        /// </summary>
        /// <param name="queryCode"></param>
        /// <returns></returns>
        public ArrayList QueryDrugBaseInfoByAll(string queryCode,string upType)
        {
            string strSql = @"SELECT   t.DRUG_CODE 药品编码,
                                       t.TRADE_NAME 药品名称,
                                       t.CUSTOM_CODE 自定义码,
                                       t.GB_CODE 国标码,
                                       t.SPECS 规格,
                                       t.RETAIL_PRICE 零售价,
                                       t.APPROVE_INFO 批文号,
                                       (SELECT p.FAC_NAME FROM PHA_COM_COMPANY p WHERE p.FAC_CODE = t.PRODUCER_CODE) 厂家,
                                       (SELECT p.FAC_NAME FROM PHA_COM_COMPANY p WHERE p.FAC_CODE = t.company_code) 供货商,
                                       (SELECT sum(s.STORE_SUM) FROM PHA_COM_STOCKINFO s WHERE s.DRUG_CODE = t.DRUG_CODE) 库存,
                                       '' 发票号,
                                       t.upload_type
                                FROM PHA_COM_BASEINFO t
                                WHERE t.VALID_STATE = '1'
                                  AND t.FEE_CODE IN ('001','002')                                  
                                  and (t.drug_code like '%{0}%' or t.trade_name like '%{0}%'
                                  or t.CUSTOM_CODE like '%{0}%' or t.spell_code like '%{0}%'
                                  or t.wb_code like '%{0}%' or t.GB_CODE like '%{0}%')
                                  and t.is_upload = '1'
                                  and (t.upload_type = '{1}' or 'ALL' = '{1}')
                                ORDER BY t.drug_code
                                ";
            strSql = string.Format(strSql, queryCode, upType);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            ArrayList alDrug = new ArrayList();
            Neusoft.HISFC.Models.Pharmacy.Item drug = null;
            try
            {
                while (this.Reader.Read())
                {
                    drug = new Neusoft.HISFC.Models.Pharmacy.Item();

                    drug.ID = this.Reader[0].ToString().Trim();
                    drug.Name = this.Reader[1].ToString().Trim();
                    drug.UserCode = this.Reader[2].ToString();
                    drug.NameCollection.GbCode = this.Reader[3].ToString();
                    drug.Specs = this.Reader[4].ToString();
                    drug.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[5].ToString());
                    drug.Product.ApprovalInfo = this.Reader[6].ToString();
                    drug.Product.Producer.Name = this.Reader[7].ToString();
                    drug.Product.Company.Name = this.Reader[8].ToString();
                    drug.Memo = this.Reader[9].ToString();   //库存信息
                    drug.Product.Memo = this.Reader[10].ToString();   //发票号
                    drug.Product.User01 = this.Reader[11].ToString();   //上传标志

                    alDrug.Add(drug);
                }
                return alDrug;
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
        /// 更新药品上传标志
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="upType"></param>
        /// <returns></returns>
        public int UpdateDrugUploadFlat(string drugCode,string upType)
        {
            string sql = @"update pha_com_baseinfo a
                        set a.upload_type = '{0}'
                        where a.drug_code = '{1}'";
            sql = string.Format(sql, upType, drugCode);

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 查询药品基本信息
        /// </summary>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Pharmacy.Item QueryDrugBaseInfo(string drugId)
        {
            string strSql = @"SELECT P.DRUG_CODE,
                                        P.TRADE_NAME,
                                         (select y.fac_name from pha_com_company y
                                        where y.fac_code = P.PRODUCER_CODE and rownum = 1) PRODUCER_NAME,
                                        (select y.fac_name from pha_com_company y
                                        where y.fac_code = P.COMPANY_CODE and rownum = 1) COMPANY_NAME,
                                        '0',
                                        '',
                                        P.SPELL_CODE,
                                        P.SPECS,
                                        P.DOSE_MODEL_CODE,
                                        nvl(P.WRAPPER,p.pack_unit)wrapper,
                                        P.WRAPPER_DESC,
                                        P.DOSE_UNIT,
                                        P.MIN_UNIT,
                                        P.BASE_DOSE,
                                        P.PACK_QTY,
                                        P.PACK_UNIT,
                                        P.APPROVE_INFO,
                                        P.OLD_APPROVEINFO,
                                        P.APPROVE_DATE,
                                        P.APPROVE_EXPDATE,
                                        P.DRUG_SOURCE,
                                        P.IMPORTINFO,
                                        P.OLD_IMPORTINFO,
                                        P.IMPORT_EXPDATE,
                                        P.GMP_FLAG,
                                        P.GMP_EXPDATE,
                                        nvl(P.DRUG_TYPEFLAG,'1') as DRUG_TYPEFLAG,
                                        P.QUALITY_TYPE,
                                        P.QUALITY_CODE,
                                        P.QUALITY_EXPDATE,
                                        DECODE(P.OCT_FLAG, '1', P.OCT_GRADE, '0') OCT_GRADE,
                                        DECODE(P.ITEM_GRADE, '3', 0, P.ITEM_GRADE),
                                        (SELECT T.CENTER_CODE
                                          FROM FIN_COM_COMPARE T
                                         WHERE T.HIS_CODE = P.OTHER_SPELL) CENTCODE,
                                        DECODE(P.SPECIAL_FLAG2, '1', '1', '0'),
                                        P.PURCHASE_PRICE,
                                        P.PACK_UNIT
                                        FROM PHA_COM_BASEINFO P
                                        WHERE P.VALID_STATE = '1'
                                        AND p.DRUG_CODE = '{0}'
                                ";

            strSql = string.Format(strSql, drugId);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Neusoft.HISFC.Models.Pharmacy.Item drug = null;
            try
            {
                while (this.Reader.Read())
                {
                    drug = new Neusoft.HISFC.Models.Pharmacy.Item();

                    drug.ID = Reader[0].ToString();
                    drug.Name = Reader[1].ToString();
                    drug.Product.Producer.Name = Reader[2].ToString();
                    drug.Product.Company.Name = Reader[3].ToString();
                    drug.IsOTC = NConvert.ToBoolean(Reader[4].ToString());
                    drug.OTCInfo = Reader[5].ToString();
                    drug.SpellCode = Reader[6].ToString();
                    drug.Specs = Reader[7].ToString();
                    drug.DosageForm.ID = Reader[8].ToString();
                    drug.Wrapper = Reader[9].ToString();
                    drug.WrapperDesc = Reader[10].ToString();
                    drug.DoseUnit = Reader[11].ToString();
                    drug.MinUnit = Reader[12].ToString();
                    drug.BaseDose = NConvert.ToDecimal(Reader[13].ToString());
                    drug.PackQty = NConvert.ToDecimal(Reader[14].ToString());
                    drug.PackUnit = Reader[15].ToString();
                    drug.Product.ApprovalInfo = Reader[16].ToString();
                    drug.OldApproveInfo = Reader[17].ToString();
                    drug.ApproveDate = NConvert.ToDateTime(Reader[18]);
                    drug.ApproveExpDate = NConvert.ToDateTime(Reader[19]);
                    drug.DrugSource = Reader[20].ToString();
                    drug.ImportInfo = Reader[21].ToString();
                    drug.OldImportInfo = Reader[22].ToString();
                    drug.ImportExpDate = NConvert.ToDateTime(Reader[23]);
                    drug.IsGMP = NConvert.ToBoolean(Reader[24].ToString());
                    drug.GmpExpdate = NConvert.ToDateTime(Reader[25]);
                    drug.DrugTypeFlag = Reader[26].ToString();
                    drug.QualityType = Reader[27].ToString();
                    drug.QualityCode = Reader[28].ToString();
                    drug.QualityExpDate = NConvert.ToDateTime(Reader[29]);
                    drug.OTCInfo = Reader[30].ToString();
                    drug.Grade = Reader[31].ToString();
                    drug.UserCode = Reader[32].ToString();
                    drug.SpecialFlag2 = Reader[33].ToString();
                    drug.PriceCollection.PurchasePrice = NConvert.ToDecimal(Reader[34].ToString());
                    drug.Memo = Reader[35].ToString();
                }

                return drug;
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
        /// 查询药品消耗明细信息
        /// </summary>
        /// <param name="drugId"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public ArrayList QueryDrugUseDetail(string drugId, DateTime dtBegin, DateTime dtEnd)
        {
            #region SQL

            string strSql = @"SELECT (SELECT P.SUN_DRUGCODE
                                  FROM PHA_COM_BASEINFO P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE) usercode,
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
                           AND T.OUT_DATE >=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                           AND T.OUT_DATE <=to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                           
                        UNION ALL

                        SELECT (SELECT P.SUN_DRUGCODE
                                  FROM PHA_COM_BASEINFO P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE) usercode,
                               T.DRUG_CODE,
                               (SELECT TK.RECIPE_DEPTCODE
                                  FROM FIN_IPB_MEDICINELIST TK
                                 WHERE TK.INPATIENT_NO = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO) deptCOde,
                               (SELECT TK.RECIPE_DOCCODE
                                  FROM FIN_IPB_MEDICINELIST TK
                                 WHERE TK.INPATIENT_NO = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO) doctCode,
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
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO) deptname,
                               (SELECT FUN_GET_EMPLOYEE_NAME(TK.RECIPE_DOCCODE)
                                  FROM FIN_IPB_MEDICINELIST TK
                                 WHERE TK.INPATIENT_NO = T.GET_PERSON
                                   AND TK.RECIPE_NO = T.RECIPE_NO
                                   AND TK.SEQUENCE_NO = T.SEQUENCE_NO) doctname
                          FROM PHA_COM_OUTPUT T
                         WHERE T.OUT_TYPE IN ('Z1', 'Z2')
                           AND T.DRUG_CODE = '{0}'
                           AND T.OUT_DATE >=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                           AND T.OUT_DATE <=to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                        ";

            #endregion

            strSql = string.Format(strSql, drugId, dtBegin.ToString(),dtEnd.ToString());
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Neusoft.HISFC.Models.Pharmacy.Output outPut = null;
            ArrayList alOutPut = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    outPut = new Neusoft.HISFC.Models.Pharmacy.Output();

                    outPut.ID = Reader[0].ToString(); //阳光用药编码
                    outPut.Item.ID = Reader[1].ToString();//项目编码
                    outPut.Operation.ApproveOper.Dept.ID = Reader[2].ToString();//科室
                    outPut.Operation.ApproveOper.ID = Reader[3].ToString();//医生
                    outPut.Operation.Oper.ID = Reader[4].ToString();//患者号
                    outPut.Operation.Oper.Memo = Reader[5].ToString();//医保号
                    outPut.Operation.Oper.Name = Reader[6].ToString();//姓名
                    outPut.OutDate = Convert.ToDateTime(Reader[7]);//时间
                    outPut.BatchNO = Reader[8].ToString();//批号
                    outPut.Producer.Name = Reader[9].ToString();//生产时间
                    outPut.ValidTime = Convert.ToDateTime(Reader[10]);//有效期
                    outPut.Operation.ExamQty = Convert.ToDecimal(Reader[11]);//数量
                    outPut.Operation.ApproveOper.Dept.Name = Reader[12].ToString();//科室
                    outPut.Operation.ApproveOper.Name = Reader[13].ToString();//医生

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
        /// 查询药品的入库信息
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public ArrayList QueryDrugInputList(DateTime dtBegin, DateTime dtEnd, string drugId)
        {
            #region SQL

            string strSql = @" SELECT T.DELIVERY_NO,
                                    T.IN_BILL_CODE,
                                    (select m.fac_name
                                       from pha_com_company m
                                      where m.fac_code = T.Company_Code) Comp,
                                    (select m.fac_name
                                       from pha_com_company m
                                      where m.fac_code = T.Company_Code) senderName,
                                     (select t.dept_name from com_department t where t.dept_code = T.DRUG_DEPT_CODE) deptName,
                                    T.MED_ID,
                                    T.IN_LIST_CODE,
                                    (SELECT P.SUN_DRUGCODE
                                       FROM PHA_COM_BASEINFO P
                                      WHERE P.DRUG_CODE = T.DRUG_CODE) SUNDRUG,
                                    T.DRUG_CODE,
                                    T.PURCHASE_PRICE,
                                    T.APPLY_NUM,
                                    t.ext_code,
                                    t.ext_code1,
                                    T.EXT_CODE2,
                                    T.IN_NUM,
                                    T.IN_DATE,
                                    T.BATCH_NO,
                                    T.VALID_DATE,
                                    t.purchase_cost,
                                    t.wholesale_cost
                               FROM PHA_COM_INPUT T 
                              WHERE T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                AND T.DRUG_CODE = '{2}'
                                AND T.IN_NUM > 0   --入库
                                AND (SELECT c.DEPT_TYPE FROM COM_DEPARTMENT c WHERE c.DEPT_CODE=T.DRUG_DEPT_CODE)='PI'
                                AND T.SOURCE_COMPANY_TYPE = '2'";

            #endregion

            strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString(), drugId);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Neusoft.HISFC.Models.Pharmacy.Input input = null;
            ArrayList alInput = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    input = new Neusoft.HISFC.Models.Pharmacy.Input();

                    input.DeliveryNO = Reader[0].ToString(); //订单号
                    input.InListNO = Reader[1].ToString();//院内单据号
                    input.Company.Name = Reader[2].ToString();//供货公司
                    input.TenderNO = Reader[3].ToString();//送货公司
                    input.StockDept.Name = Reader[4].ToString();//库房
                    input.MedNO = Reader[5].ToString();//订单明细号
                    input.ID = Reader[6].ToString();//院内订单明细号
                    input.Item.UserCode = Reader[7].ToString();//阳光用药编码
                    input.Item.ID = Reader[8].ToString();//院内编码
                    input.PriceCollection.PurchasePrice = Convert.ToDecimal(Reader[9].ToString());//采购价格
                    input.Operation.ApproveQty = Convert.ToDecimal(Reader[10].ToString());//采购数量

                    input.User01 = Reader[11].ToString();//发货日期 ext_code  【与入库日期一致】
                    input.User02 = Reader[12].ToString();//生产日期 ext_code1 
                    input.User03 = Reader[13].ToString();//采购日期 EXT_CODE2 【与入库日期一致】

                    input.Quantity = Convert.ToDecimal(Reader[14].ToString());//数量
                    input.InDate = Convert.ToDateTime(Reader[15].ToString());//数量
                    input.BatchNO = Reader[16].ToString();//采购日期
                    input.ValidTime = Convert.ToDateTime(Reader[17]);//有效期
                    input.RetailCost = Convert.ToDecimal(Reader[18].ToString());//采购金额
                    input.PurchaseCost = Convert.ToDecimal(Reader[19].ToString());//到货金额

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
        /// 查询药品的退库信息
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public ArrayList QueryDrugReturnList(DateTime dtBegin, DateTime dtEnd, string drugId)
        {
            #region SQL

            string strSql = @" SELECT T.DELIVERY_NO,
                                    T.IN_BILL_CODE,
                                    (select m.fac_name
                                       from pha_com_company m
                                      where m.fac_code = T.Company_Code) Comp,
                                    (select m.fac_name
                                       from pha_com_company m
                                      where m.fac_code = T.Company_Code) senderName,
                                     (select t.dept_name from com_department t where t.dept_code = T.DRUG_DEPT_CODE) deptName,
                                    T.MED_ID,
                                    T.IN_LIST_CODE,
                                    (SELECT P.SUN_DRUGCODE
                                       FROM PHA_COM_BASEINFO P
                                      WHERE P.DRUG_CODE = T.DRUG_CODE) SUNDRUG,
                                    T.DRUG_CODE,
                                    T.PURCHASE_PRICE,
                                    T.APPLY_NUM,
                                    t.ext_code,
                                    t.ext_code1,
                                    T.EXT_CODE2,
                                    T.IN_NUM,
                                    T.IN_DATE,
                                    T.BATCH_NO,
                                    T.VALID_DATE,
                                    t.purchase_cost,
                                    t.wholesale_cost,
                                    (
                                    SELECT max(p.IN_BILL_CODE) FROM PHA_COM_INPUT p
                                    WHERE p.DRUG_CODE = T.DRUG_CODE
                                    AND p.BATCH_NO = T.BATCH_NO
                                    AND p.GROUP_CODE = T.GROUP_CODE
                                    AND (p.PRODUCER_CODE IS NULL OR p.PRODUCER_CODE=t.PRODUCER_CODE)
                                    AND p.COMPANY_CODE=t.COMPANY_CODE
                                    AND p.SOURCE_COMPANY_TYPE = '2'
                                    AND p.IN_NUM > 0
                                    ) RTNORDERID,
                                    (
                                    SELECT max(p.IN_LIST_CODE) FROM PHA_COM_INPUT p
                                    WHERE p.DRUG_CODE = T.DRUG_CODE
                                    AND p.BATCH_NO = T.BATCH_NO
                                    AND p.GROUP_CODE = T.GROUP_CODE
                                    AND (p.PRODUCER_CODE IS NULL OR p.PRODUCER_CODE=t.PRODUCER_CODE)
                                    AND p.COMPANY_CODE=t.COMPANY_CODE
                                    AND p.SOURCE_COMPANY_TYPE = '2'
                                    AND p.IN_NUM > 0
                                    ) RTNDETAILID
                              FROM PHA_COM_INPUT T 
                              WHERE T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                AND T.DRUG_CODE = '{2}'
                                AND T.IN_NUM < 0   --退库
                                AND (SELECT c.DEPT_TYPE FROM COM_DEPARTMENT c WHERE c.DEPT_CODE=T.DRUG_DEPT_CODE)='PI'
                                AND T.SOURCE_COMPANY_TYPE = '2'";

            #endregion

            strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString(), drugId);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Neusoft.HISFC.Models.Pharmacy.Input input = null;
            ArrayList alInput = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    input = new Neusoft.HISFC.Models.Pharmacy.Input();

                    input.DeliveryNO = Reader[0].ToString(); //订单号
                    input.InListNO = Reader[1].ToString();//院内单据号
                    input.Company.Name = Reader[2].ToString();//供货公司
                    input.TenderNO = Reader[3].ToString();//送货公司
                    input.StockDept.Name = Reader[4].ToString();//库房
                    input.MedNO = Reader[5].ToString();//订单明细号
                    input.ID = Reader[6].ToString();//院内订单明细号
                    input.Item.UserCode = Reader[7].ToString();//阳光用药编码
                    input.Item.ID = Reader[8].ToString();//院内编码
                    input.PriceCollection.PurchasePrice = Convert.ToDecimal(Reader[9].ToString());//生产时间
                    input.Operation.ApproveQty = Convert.ToDecimal(Reader[10].ToString());//采购数量

                    input.User01 = Reader[11].ToString();//发货日期 ext_code  【与入库日期一致】
                    input.User02 = Reader[12].ToString();//生产日期 ext_code1 
                    input.User03 = Reader[13].ToString();//采购日期 EXT_CODE2 【与入库日期一致】

                    input.Quantity = Convert.ToDecimal(Reader[14].ToString());//数量
                    input.InDate = Convert.ToDateTime(Reader[15].ToString());//数量
                    input.BatchNO = Reader[16].ToString();//采购日期
                    input.ValidTime = Convert.ToDateTime(Reader[17]);//有效期
                    input.RetailCost = Convert.ToDecimal(Reader[18].ToString());//采购金额
                    input.PurchaseCost = Convert.ToDecimal(Reader[19].ToString());//到货金额

                    input.Operation.User01 = Reader[20].ToString();   //正记录的订单号
                    input.Operation.User02 = Reader[21].ToString();   //正记录的订单明细号

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
        /// 药品发票信息
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="drugId"></param>
        /// <returns></returns>
        public ArrayList QueryDrugInvoiceInfo(DateTime dtBegin, DateTime dtEnd, string drugId)
        {
            #region SQL

            string strSql = @"SELECT '',
                                       T.INVOICE_NO,
                                        (select m.fac_name
                                           from pha_com_company m
                                          where m.fac_code = T.Company_Code) Comp,
                                       T.PURCHASE_COST,
                                       T.INVOICE_DATE,
                                       DECODE(T.IN_TYPE, '06', '20', '10') INVOICETYPE,
                                       '采购发票' INVOICE_NAME,
                                       '退回原因',
                                       '',
                                       T.DELIVERY_NO,
                                       T.IN_LIST_CODE,
                                       T.MED_ID,
                                       T.IN_BILL_CODE
                                  FROM PHA_COM_INPUT T
                                 WHERE T.IN_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')    --应该使用INVOICE_DATE
                                   AND T.IN_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')    --应该使用INVOICE_DATE
                                   AND T.DRUG_CODE = '{2}'
                                   AND T.INVOICE_NO IS NOT NULL  ";

            #endregion

            strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString(), drugId);
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            Neusoft.HISFC.Models.Pharmacy.Input input = null;
            ArrayList alInput = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    input = new Neusoft.HISFC.Models.Pharmacy.Input();

                    input.Operation.ID = Reader[0].ToString(); //采购平台发票
                    input.InvoiceNO = Reader[1].ToString(); //院内发票
                    input.Company.Name = Reader[2].ToString(); //配送企业
                    input.PurchaseCost = Convert.ToDecimal(Reader[3].ToString());//明细金额
                    input.InvoiceDate = Convert.ToDateTime(Reader[4].ToString()); //发票时间
                    input.InvoiceType = Reader[5].ToString();//发票类型
                    input.Name = Reader[6].ToString();//发票名称
                    input.Memo = Reader[7].ToString();//
                    input.Operation.Memo = Reader[8].ToString();
                    input.DeliveryNO = Reader[9].ToString();//平台订单号
                    input.ID = Reader[10].ToString();//院内订单编号
                    input.MedNO = Reader[11].ToString();//阳光采购平台明细编号
                    input.InListNO = Reader[12].ToString();//院内订单明细好

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
        /// 查询某段时间内的药品和非药品的结算信息
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public ArrayList QueryBalanceInfo(DateTime dtBegin, DateTime dtEnd)
        {
            #region SQL

            string strSql = @"SELECT K.PAYDETAIL_NO,
                                   K.PAYHEAD_NO,
                                   K.COMPANY_CODE,
                                   DECODE(T.DRUG_FLAG,
                                          '1',
                                          (SELECT L.FAC_NAME
                                             FROM PHA_COM_COMPANY L
                                            WHERE L.FAC_CODE = K.COMPANY_CODE),
                                          (SELECT C.COMPANY_NAME
                                             FROM MAT_COM_COMPANY C
                                            WHERE C.COMPANY_CODE = K.COMPANY_CODE)) COMPNAME,
                                   T.PAY_COST,
                                   T.DRUG_FLAG,
                                   T.INVOICE_NO,
                                   K.OPER_DATE
                              FROM COM_SUN_PAYHEAD T, COM_SUN_PAYDETAIL K
                             WHERE T.PAYHEAD_NO = K.PAYHEAD_NO
                               AND K.OPER_DATE BETWEEN TO_DATE('{0}', 'yyyy-mm-dd hh24:mi:ss') AND
                                   TO_DATE('{1}', 'yyyy-mm-dd hh24:mi:ss')
                             ";

            #endregion

            strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString());
            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            DiseasePay.Models.BalanceHead balance = new DiseasePay.Models.BalanceHead();
            ArrayList alBalanceInfo = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    balance = new DiseasePay.Models.BalanceHead();

                    balance.PayDetailNo = Reader[0].ToString();
                    balance.PayHeadNo = Reader[1].ToString();
                    balance.Company.ID = Reader[2].ToString();
                    balance.Company.Name = Reader[3].ToString();
                    balance.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[4]);
                    balance.PayState = Reader[5].ToString();
                    balance.InvoiceNo = Reader[6].ToString();
                    balance.OperDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[7]);

                    alBalanceInfo.Add(balance);
                }

                return alBalanceInfo;
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
