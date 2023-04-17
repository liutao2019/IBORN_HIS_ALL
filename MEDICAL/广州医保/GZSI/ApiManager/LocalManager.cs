using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace GZSI.ApiManager
{
    /// <summary>
    /// API操作HIS类
    /// </summary>
    public class LocalManager : FS.FrameWork.Management.Database 
    {
        /// <summary>
        /// 医院编码
        /// </summary>
        public string HosCode = string.Empty;

        /// <summary>
        /// 获取合同单位对应的业务类型 
        /// 常数类型： biz_type  
        /// code:合同单位代码 
        /// name:合同单位名称
        /// mark:业务类型代码
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public string GetApiBizType(string pactCode)
        {
            string strSql = "";
            string biz_type = "";
            if (this.Sql.GetSql("gzsi.GetApiBizType", ref strSql) == -1)
            {
                strSql = @"select d.mark from com_dictionary d where d.type='biz_type' and d.code='{0}'";
            }
            try
            {
                strSql = string.Format(strSql, pactCode);
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    biz_type = Reader[0].ToString();
                }
                Reader.Close();
                return biz_type;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
 
        }

        /// <summary>
        /// 获取API中的待遇类型 
        /// 常数：treatment_type 
        /// code:待遇类型代码 
        /// name:待遇类型名称 
        /// mark:业务类型代码
        /// </summary>
        /// <param name="biz_type"></param>
        /// <returns></returns>
        public ArrayList GetApiTreatmenttype(string biz_type)
        {

            string strSql = "";
            ArrayList alTreatmenttype = new ArrayList();
            if (this.Sql.GetSql("gzsi.GetApiTreatmenttype", ref strSql) == -1)
            {
                strSql = @"select d.code,d.name,d.mark from com_dictionary d where d.type='treatment_type' and d.mark='{0}'";
            }
            try
            {
                strSql = string.Format(strSql, biz_type);
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new  FS.FrameWork.Models.NeuObject();
                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.Memo = Reader[2].ToString();
                    alTreatmenttype.Add(obj);
                }
                Reader.Close();
                return alTreatmenttype;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 更新待遇类型
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="regNo"></param>
        /// <param name="treamType"></param>
        /// <returns></returns>
        public int UpdateApiTreamType(string clinicNo, string regNo,string treamType)
        {
            string strSql = "";
            if (this.Sql.GetSql("gzsi.UpdateApiTreamType", ref strSql) == -1)
            {
                strSql = @"update fin_ipr_siinmaininfo si set si.applytypeid='{2}' where si.inpatient_no='{0}' and si.reg_no='{1}'";
            }
            try
            {
                strSql = string.Format(strSql, clinicNo, regNo, treamType);
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新工伤就医凭证号
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="regNo"></param>
        /// <param name="treamType"></param>
        /// <returns></returns>
        public int UpdateApiIDNO_GS(string clinicNo, string regNo, string idNoGS)
        {
            string strSql = "";
            if (this.Sql.GetSql("gzsi.UpdateApiidNoGS", ref strSql) == -1)
            {
                strSql = @"update fin_ipr_siinmaininfo si set si.IDNO_GS='{2}' where si.inpatient_no='{0}' and si.reg_no='{1}'";
            }
            try
            {
                strSql = string.Format(strSql, clinicNo, regNo, idNoGS);
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取待遇类型
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="regNo"></param>
        /// <returns></returns>
        public string GetHisApiTreamType(string clinicNo, string regNo)
        {
            string strSql = "",treamType="";
            if (this.Sql.GetSql("gzsi.UpdateApiTreamType", ref strSql) == -1)
            {
                strSql = @"select si.applytypeid from  fin_ipr_siinmaininfo si  where si.inpatient_no='{0}' and si.reg_no='{1}' and si.valid_flag='1' order by si.balance_no";
            }
            try
            {
                strSql = string.Format(strSql, clinicNo, regNo);
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    treamType = Reader[0].ToString();
                }
                Reader.Close();
                return treamType;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 获取医保登记号
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public string GetSiRegNo(string clinicNo)
        {
            string balNo = this.GetBalNo(clinicNo);
            if (balNo == "")
                return null;
            string strSql = "", regNo = "";
            if (this.Sql.GetSql("gzsi.GetSiRegNo", ref strSql) == -1)
            {
                strSql = @"select si.reg_no from  fin_ipr_siinmaininfo si where si.inpatient_no='{0}' and si.balance_no='{1}' and si.valid_flag='1'";
            }
            try
            {
                strSql = string.Format(strSql, clinicNo, balNo);
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    regNo = Reader[0].ToString();
                }
                Reader.Close();
                return regNo;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
 
        }

        /// <summary>
        /// 根据流水号和发票号
        /// 获取等级好,待遇类型和费用
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetSiRegNo(string clinicNo, string invoiceNo)
        {
            FS.FrameWork.Models.NeuObject obj = null;

            string strSql = @"select si.reg_no,si.APPLYTYPEID,si.tot_cost from  fin_ipr_siinmaininfo si  where si.inpatient_no='{0}' and si.INVOICE_NO='{1}' and si.valid_flag='1'";

            try
            {
                strSql = string.Format(strSql, clinicNo, invoiceNo);
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.Memo = Reader[2].ToString();
                }
                Reader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

        }

        /// <summary>
        /// 根据卡号获取身份证号码
        /// </summary>
        /// <param name="card_no"></param>
        /// <returns></returns>
        public string GetIDCardByCardNo(string card_no)
        {
            string idcard = "";
            string strSql = @"SELECT  IDENNO FROM COM_PATIENTINFO WHERE CARD_NO='{0}'";
            try
            {
                strSql = string.Format(strSql, card_no);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {
                    idcard = Reader[0].ToString();
                }
                Reader.Close();
                return idcard;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 得到结算序号
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public string GetBalNo(string inpatientNo)
        {
            string strSql = "";
            string balNo = "";
            if (this.Sql.GetCommonSql("Fee.Interface.GetBalNo.1", ref strSql) == -1)
                return "";
            try
            {
                strSql = string.Format(strSql, inpatientNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {
                    balNo = Reader[0].ToString();
                }
                Reader.Close();
                return balNo;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 获取医保诊断字典
        /// </summary>
        /// <returns></returns>
        public ArrayList QuerySiDisease()
        {
            string strSql = "";
            ArrayList alDisease = new ArrayList();
            if (this.Sql.GetSql("gzsi.QuerySiDisease", ref strSql) == -1)
            {   
                //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                //strSql = @"SELECT * FROM MET_COM_SIDIAGNOSE";
                strSql = @"SELECT * FROM MET_COM_ICD10";
            }
            try
            {
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.ICD icd = null;
                while (Reader.Read())
                {
                    icd = new FS.HISFC.Models.HealthRecord.ICD();
                    //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                    //icd.ID = Reader["ICD"].ToString();
                    //icd.Name = Reader["DISEASE"].ToString();
                    //icd.SpellCode = Reader["SPELL_CODE"].ToString();
                    //icd.WBCode = Reader["WB_CODE"].ToString();
                    //icd.Memo = Reader["MEMO"].ToString();

                    icd.ID = Reader["ICD_CODE"].ToString();
                    icd.Name = Reader["ICD_NAME"].ToString();
                    icd.SpellCode = Reader["SPELL_CODE"].ToString();
                    icd.WBCode = Reader["WB_CODE"].ToString();

                    alDisease.Add(icd); //填充数据
                    icd = null;
                }
                Reader.Close();
                return alDisease;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据医嘱编号获取限制用药标识 
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public string GetLimitByCard_No(string recipe_no, string sequence_no)
        {
            string sql = @"select recipe_memo from fin_opb_feedetail where recipe_no='{0}' and sequence_no='{1}'";
            sql = string.Format(sql, recipe_no, sequence_no);

            //FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string num = this.ExecSqlReturnOne(sql);
            if (num == null || num.Equals("-1"))
            {
                return "";
            }
            else
            {
                return num;
            }
        }

        #region 本地医保结算表

        /// <summary>
        /// 插入门诊结算信息
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="jsid"></param>
        /// <returns></returns>
        public int InsertMZJSData(FS.HISFC.Models.Registration.Register reg, ref string jsid)
        {

            try
            {
                string getIDSql = "select SEQ_GZSI_HIS_MZJS.Nextval from dual";
                this.ExecQuery(getIDSql);
                if (Reader.Read())
                {
                    jsid = Reader[0].ToString();
                }
                Reader.Close();
                if (string.IsNullOrEmpty(jsid))
                {
                    this.Err = "获取主键ID(SEQ_GZSI_HIS_MZJS)失败";
                    return -1;
                }

                #region 插入语句
                string insertSql = @"INSERT INTO GZSI_HIS_MZJS(JYDJH
                                                             , FYPC
                                                             , YYBH
                                                             , GMSFHM
                                                             , ZYH
                                                             , RYRQ
                                                             , JSRQ
                                                             , ZYZJE
                                                             , SBZFJE
                                                             , ZHZFJE
                                                             , BFXMZFJE
                                                             , QFJE
                                                             , GRZFJE1
                                                             , GRZFJE2
                                                             , GRZFJE3
                                                             , CXZFJE
                                                             , ZFYY
                                                             , YYFDJE
                                                             , BZ1
                                                             , BZ2
                                                             , BZ3
                                                             , DRBZ
                                                             , CLINIC_CODE
                                                             , CARD_NO
                                                             , INVOICE_NO
                                                             , OPER_CODE
                                                             , OPER_DATE
                                                             ,VALID_FLAG
                                                             ,JSID)
                                                        VALUES
                                                            ('{0}'      --就医登记号
                                                            ,'{1}'     --费用批次
                                                            ,'{2}'     --医院编号
                                                            ,'{3}'     --身份证号
                                                            ,'{4}'     --门诊号/住院号
                                                            ,to_date('{5}','yyyy-mm-dd hh24:mi:ss')     --就诊日期
                                                            ,to_date('{6}','yyyy-mm-dd hh24:mi:ss')     --结算日期
                                                            ,'{7}'     --总金额
                                                            ,'{8}'     --社保支付金额
                                                            ,'{9}'     --账户支付金额
                                                            ,'{10}'    --部分项目自付金额
                                                            ,'{11}'    --个人起付金额
                                                            ,'{12}'    --个人自费项目金额
                                                            ,'{13}'    --个人自付金额
                                                            ,'{14}'    --个人自负金额
                                                            ,'{15}'    --超统筹支付限额个人自付金额
                                                            ,'{16}'    --自费原因
                                                            ,'{17}'    --医药机构分单金额
                                                            ,'{18}'    --备注1,记录产生时间
                                                            ,'{19}'    --备注2
                                                            ,'{20}'    --备注3
                                                            ,'{21}'    --读入标志
                                                            ,'{22}'    --门诊流水号
                                                            ,'{23}'    --门诊号
                                                            ,'{24}'    --发票号
                                                            ,'{25}'    --操作员
                                                            ,sysdate    --操作时间
                                                            ,'1'
                                                            ,'{26}'    --结算ID
                                                            )";
                #endregion

                //写入
                insertSql = string.Format(insertSql, reg.SIMainInfo.RegNo, reg.SIMainInfo.FeeTimes, reg.SIMainInfo.HosNo, reg.IDCard, reg.PID.CardNO,
                                                     reg.SeeDoct.OperTime, reg.SIMainInfo.BalanceDate, reg.SIMainInfo.TotCost, reg.SIMainInfo.PubCost, reg.SIMainInfo.PayCost,
                                                     reg.SIMainInfo.ItemYLCost, reg.SIMainInfo.BaseCost, reg.SIMainInfo.ItemPayCost, reg.SIMainInfo.PubOwnCost, reg.SIMainInfo.OwnCost,
                                                     reg.SIMainInfo.OverTakeOwnCost, reg.SIMainInfo.Memo, reg.SIMainInfo.HosCost, reg.SIMainInfo.User01, reg.SIMainInfo.User02,
                                                     reg.SIMainInfo.User03, reg.SIMainInfo.ReadFlag, reg.ID, reg.PID.CardNO, reg.SIMainInfo.InvoiceNo,
                                                     reg.SIMainInfo.OperInfo.ID,/*操作时间,有效标记,*/ jsid);
                return this.ExecNoQuery(insertSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新医保结算信息为无效
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public int DisableMZJSData(string clinicCode, string invoiceNo)
        {
            if (string.IsNullOrEmpty(clinicCode) || string.IsNullOrEmpty(invoiceNo))
            {
                this.Err = "DisableMZJSData方法参数错误。";
                return -1;
            }
            string strSql = string.Format("update GZSI_HIS_MZJS t set t.valid_flag='0' where t.clinic_code='{0}' and t.invoice_no='{1}'", clinicCode, invoiceNo);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新医保结算信息为无效
        /// </summary>
        /// <param name="jsid"></param>
        /// <returns></returns>
        public int DisableMZJSData(string regNO)
        {
            if (string.IsNullOrEmpty(regNO))
            {
                this.Err = "DisableMZJSData方法参数错误。";
                return -1;
            }
            string strSql = string.Format("update GZSI_HIS_MZJS t set t.valid_flag='0' where t.jydjh='{0}' and t.valid_flag='1'", regNO);
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region 本地医保结算明细表

        /// <summary>
        /// 插入多条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="feeDetails">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertMZXMData(FS.HISFC.Models.Registration.Register r, ArrayList feeDetails, DateTime operDate, string jsid)
        {
            int iReturn = 0;
            int uploadRows = 0;
            try
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    iReturn = DeleteMZXMData(r, f);
                    if (iReturn < 0)
                    {
                        this.Err += "删除历史费用明细失败!";
                        return -1;
                    }

                    iReturn = InsertMZXMData(r, f, operDate, jsid);
                    if (iReturn <= 0)
                    {
                        this.Err += "插入明细失败!";
                        return -1;
                    }

                    uploadRows += iReturn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return uploadRows;
        }

        /// <summary>
        /// 插入单条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertMZXMData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, DateTime operDate, string jsid)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            #region 插入语句

            string strSQL = @"INSERT INTO GZSI_HIS_MZXM     --广州医保费用明细信息表
                                      ( JYDJH,   --就医登记号
                                        YYBH,   --医院编号
                                        GMSFHM,   --公民身份证号
                                        ZYH,   --住院号/门诊号
                                        RYRQ,   --挂号/入院时间
                                        FYRQ,   --收费时间
                                        XMXH,   --项目序号
                                        XMBH,   --医院的项目编号
                                        XMMC,   --医院的项目名称
                                        FLDM,   --分类代码
                                        YPGG,   --规格
                                        YPJX,   --剂型
                                        JG,   --单价
                                        MCYL,   --数量
                                        JE,   --金额
                                        BZ1,   --备注1，存放记录产生时间
                                        BZ2,   --备注2
                                        BZ3,   --备注3,存放费用明细读入时有效性检查的处理结果代码
                                        DRBZ,   --读入标志
                                        YPLY,   --1-国产 2-进口 3-合资
                                        CLINIC_CODE,   --门诊就诊流水号
                                        CARD_NO,   --门诊号
                                        OPER_CODE,   --操作员
                                        OPER_DATE,   --操作时间
                                        INVOICE_NO,   --发票号
                                        FYPC,   --费用批次
                                        fee_code,
                                        jsid
                                        ) 
                                        VALUES
                                        (
                                        '{0}',   --就医登记号
                                        '{1}',   --医院编号
                                        '{2}',   --公民身份证号
                                        '{3}',   --住院号/门诊号
                                        TO_DATE('{4}','YYYY-MM-DD HH24:MI:SS'),   --挂号/入院时间
                                        TO_DATE('{5}','YYYY-MM-DD HH24:MI:SS'),   --收费时间
                                        '{6}',   --项目序号
                                        '{7}',   --医院的项目编号
                                        '{8}',   --医院的项目名称
                                        '{9}',   --分类代码
                                        '{10}',   --规格
                                        '{11}',   --剂型
                                        '{12}',   --单价
                                        '{13}',   --数量
                                        '{14}',   --金额
                                        '{15}',   --备注1，存放记录产生时间
                                        '{16}',   --备注2
                                        '{17}',   --备注3,存放费用明细读入时有效性检查的处理结果代码
                                        '{18}',   --读入标志
                                        '{19}',   --1-国产   2-进口3-合资
                                        '{20}',   --门诊就诊流水号
                                        '{21}',   --门诊号
                                        '{22}',   --操作员
                                        sysdate,   --操作时间
                                        '{23}',   --发票号
                                        '{24}',   --费用批次
                                        '{25}',  --HIS费用类别
                                        '{26}') ";
            #endregion 

            string name = "";
            name = f.Item.Name;
            if (name.Length > 20)
            {
                name = name.Substring(0, 20);
            }
            try
            {
                decimal price = f.SIPrice; //this.GetPrice(f.Item);
                decimal totCost = f.SIft.OwnCost + f.SIft.PubCost + f.SIft.PayCost;

                strSQL = string.Format(strSQL,
                    r.SIMainInfo.RegNo,
                    r.SIMainInfo.HosNo,
                    r.IDCard,
                    r.PID.CardNO,
                    r.DoctorInfo.SeeDate.ToString(),
                    operDate.ToString(),
                    f.RecipeNO+f.SequenceNO,//f.Order.SequenceNO.ToString(),
                    f.Item.UserCode,
                    name,
                    "0",
                    f.Item.Specs,
                    r.MainDiagnose, //诊断
                    FS.FrameWork.Public.String.FormatNumber(price / f.Item.PackQty, 4),
                    f.Item.Qty,                 //总量Amount
                    totCost,//(f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString(),
                    operDate.ToString(),
                    "",
                    "",
                    "0",
                    "",
                    r.ID,
                    r.PID.CardNO,
                    this.Operator.ID,
                    r.SIMainInfo.InvoiceNo,
                    r.SIMainInfo.FeeTimes.ToString(),
                    f.Item.MinFee.ID,
                    jsid
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// 删除单条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int DeleteMZXMData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            string strSQL = "delete from gzsi_his_mzxm t where t.clinic_code='{0}' and t.jydjh='{1}' and t.xmbh='{2}' and t.xmxh='{3}' ";
            try
            {
                strSQL = string.Format(strSQL,
                    r.ID,
                    r.SIMainInfo.RegNo,
                    f.Item.UserCode,
                    f.RecipeNO + f.SequenceNO
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        #endregion 

        #region 处理价格

        /// <summary>
        /// 获得上传的价格
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private decimal GetPrice(FS.HISFC.Models.Base.Item item)
        {
            decimal price = item.Price;
            if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
                //if (undrug.SpecialPrice > 0)
                //{
                price = undrug.SpecialPrice;
                //}
            }
            else if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                if (phaItem.SpecialPrice > 0)
                {
                    price = phaItem.SpecialPrice;
                }
            }
            if (price == 0)
            {
                price = item.Price;
            }
            return price;
        }

        #endregion
    }
}
