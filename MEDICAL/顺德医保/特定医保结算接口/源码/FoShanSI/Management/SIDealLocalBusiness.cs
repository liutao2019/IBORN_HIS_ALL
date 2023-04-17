using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoShanSI.Management
{
    /// <summary>
    /// 本地HIS业务处理层
    /// 
    /// {1A3FD005-392E-418e-998B-BD98B5E4CB49}
    /// </summary>
    public class SIDealLocalBusiness:FS.FrameWork.Management.Database
    {
        public SIDealLocalBusiness()
        { }


        #region 住院插入医保表

        /// <summary>
        /// 住院插入医保表
        /// </summary>
        /// <param name="obj">FS.HISFC.Models.RADT.PatientInfo实体</param>
        /// <returns></returns>
        public int InsertSIMainInfo(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertSIMainInfo_AnShan.1", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.InsertSIMainInfo_AnShan.1]对应sql语句出错";
                return -1;
            }

            int balNo = FS.FrameWork.Function.NConvert.ToInt32(this.GetBalNo(obj.ID)) + 1;
            obj.SIMainInfo.BalNo = balNo.ToString();
            try
            {
                strSql = string.Format(strSql, obj.ID, obj.SIMainInfo.BalNo, obj.SIMainInfo.InvoiceNo, obj.SIMainInfo.MedicalType.ID, obj.PID.PatientNO,
                     obj.PID.CardNO, obj.SSN, obj.SIMainInfo.AppNo, obj.SIMainInfo.ProceatePcNo,
                     obj.SIMainInfo.SiBegionDate.ToString(), obj.SIMainInfo.SiState, obj.Name, obj.Sex.ID.ToString(),
                     obj.IDCard, "", obj.Birthday.ToString(), obj.SIMainInfo.EmplType, obj.CompanyName,
                     obj.SIMainInfo.InDiagnose.Name, obj.PVisit.PatientLocation.Dept.ID, obj.PVisit.PatientLocation.Dept.Name,
                     obj.Pact.PayKind.ID, obj.Pact.ID, obj.Pact.Name, obj.PVisit.PatientLocation.Bed.ID,
                     obj.PVisit.InTime.ToString(), obj.PVisit.InTime.ToString(), obj.SIMainInfo.InDiagnose.ID,
                     obj.SIMainInfo.InDiagnose.Name, this.Operator.ID, obj.SIMainInfo.HosNo, obj.SIMainInfo.RegNo,
                     obj.SIMainInfo.FeeTimes, obj.SIMainInfo.HosCost, obj.SIMainInfo.YearCost, obj.PVisit.OutTime.ToString(),
                     obj.SIMainInfo.OutDiagnose.ID, obj.SIMainInfo.OutDiagnose.Name, obj.SIMainInfo.BalanceDate.ToString(),
                     obj.SIMainInfo.TotCost, obj.SIMainInfo.PayCost, obj.SIMainInfo.PubCost, obj.SIMainInfo.ItemPayCost,
                     obj.SIMainInfo.BaseCost, obj.SIMainInfo.ItemYLCost, obj.SIMainInfo.PubOwnCost, obj.SIMainInfo.OwnCost,
                     obj.SIMainInfo.OverCost, 1,
                     FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced), obj.SIMainInfo.Memo, obj.SIMainInfo.OfficalCost, obj.SIMainInfo.PersonType.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
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
        /// 更新医保结算主表信息
        /// </summary>
        /// <param name="obj">住院患者基本信息类</param>
        /// <returns></returns>
        public int UpdateSiMainInfo(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            string strSql = "";
            //string balNo = this.GetBalNo(obj.ID);
            if (this.Sql.GetSql("Fee.Interface.UpdateSiMainInfo.Update.1", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.UpdateSiMainInfo.Update.1]对应sql语句出错";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, obj.ID, obj.SIMainInfo.BalNo, obj.SIMainInfo.InvoiceNo, obj.SIMainInfo.MedicalType.ID, obj.PID.PatientNO,
                    obj.PID.CardNO, obj.SSN, obj.SIMainInfo.AppNo, obj.SIMainInfo.ProceatePcNo,
                    obj.SIMainInfo.SiBegionDate.ToString(), obj.SIMainInfo.SiState, obj.Name, obj.Sex.ID.ToString(),
                    obj.IDCard, "", obj.Birthday.ToString(), obj.SIMainInfo.EmplType, obj.CompanyName,
                    obj.SIMainInfo.InDiagnose.Name, obj.PVisit.PatientLocation.Dept.ID, obj.PVisit.PatientLocation.Dept.Name,
                    obj.Pact.PayKind.ID, obj.Pact.ID, obj.Pact.Name, obj.PVisit.PatientLocation.Bed.ID,
                    obj.PVisit.InTime.ToString(), obj.PVisit.InTime.ToString(), obj.SIMainInfo.InDiagnose.ID,
                    obj.SIMainInfo.InDiagnose.Name, obj.PVisit.OutTime, obj.SIMainInfo.OutDiagnose.ID, obj.SIMainInfo.OutDiagnose.Name,
                    obj.SIMainInfo.BalanceDate.ToString(), obj.SIMainInfo.TotCost, obj.SIMainInfo.PayCost, obj.SIMainInfo.PubCost,
                    obj.SIMainInfo.ItemPayCost, obj.SIMainInfo.BaseCost, obj.SIMainInfo.PubOwnCost, obj.SIMainInfo.ItemYLCost,
                    obj.SIMainInfo.OwnCost, obj.SIMainInfo.OverTakeOwnCost, "", this.Operator.ID,
                    obj.SIMainInfo.RegNo, obj.SIMainInfo.FeeTimes, obj.SIMainInfo.HosCost, obj.SIMainInfo.YearCost,
                    FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid), FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced), obj.SIMainInfo.OverCost, obj.SIMainInfo.OfficalCost, obj.SIMainInfo.Memo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);

        }

        /// <summary>
        /// 更新医保结算状态
        /// </summary>
        /// <param name="inPatienNo"></param>
        /// <param name="validType"></param>
        /// <returns></returns>
        public int UpdateSiMainInfoValidType(string inPatienNo, string validType,string personType)
        {
            string sql = @"update fin_ipr_siinmaininfo
                        set valid_flag = '{2}',
                            oper_date = sysdate,
                            oper_Code = '{3}'
                        where Inpatient_No = '{0}'
                        and type_code = '{1}'";
            sql = string.Format(sql, inPatienNo, personType, validType, this.Operator.ID);

            return this.ExecNoQuery(sql);
        }
        #endregion

        #region 门诊插入医保表

        /// <summary>
        /// 门诊插入医保表
        /// </summary>
        /// <param name="obj">FS.HISFC.Models.Registration.Register实体</param>
        /// <returns></returns>
        public int InsertSIMainInfo(FS.HISFC.Models.Registration.Register obj)
        {
            string strSql = "";
            int balNo = FS.FrameWork.Function.NConvert.ToInt32(this.GetBalNo(obj.ID))+1;
            obj.SIMainInfo.BalNo = balNo.ToString();
            if (this.Sql.GetSql("Fee.Interface.InsertSIMainInfo.OutPatient", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.InsertSIMainInfo.OutPatient]对应sql语句出错";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, obj.ID, obj.SIMainInfo.BalNo, obj.SIMainInfo.InvoiceNo, obj.SIMainInfo.MedicalType.ID, obj.PID.PatientNO,
                    obj.PID.CardNO, obj.SSN, obj.SIMainInfo.AppNo, obj.SIMainInfo.ProceatePcNo,
                    obj.SIMainInfo.SiBegionDate.ToString(), obj.SIMainInfo.SiState, obj.Name, obj.Sex.ID.ToString(),
                    obj.IDCard, "", obj.Birthday.ToString(), obj.SIMainInfo.EmplType, obj.CompanyName,
                    obj.SIMainInfo.InDiagnose.Name, obj.DoctorInfo.Templet.Dept.ID, obj.DoctorInfo.Templet.Dept.Name,
                    obj.Pact.PayKind.ID, obj.Pact.ID, obj.Pact.Name, "",
                    obj.DoctorInfo.SeeDate, obj.DoctorInfo.SeeDate, obj.SIMainInfo.InDiagnose.ID,
                    obj.SIMainInfo.InDiagnose.Name, this.Operator.ID, obj.SIMainInfo.HosNo, obj.SIMainInfo.RegNo,
                    obj.SIMainInfo.FeeTimes, obj.SIMainInfo.HosCost, obj.SIMainInfo.YearCost, obj.DoctorInfo.SeeDate,
                    obj.SIMainInfo.OutDiagnose.ID, obj.SIMainInfo.OutDiagnose.Name, obj.SIMainInfo.BalanceDate.ToString(),
                    obj.SIMainInfo.TotCost, obj.SIMainInfo.PayCost, obj.SIMainInfo.PubCost, obj.SIMainInfo.ItemPayCost,
                    obj.SIMainInfo.BaseCost, obj.SIMainInfo.ItemYLCost, obj.SIMainInfo.PubOwnCost, obj.SIMainInfo.OwnCost,
                    obj.SIMainInfo.OverCost, FS.FrameWork.Function.NConvert.ToInt32(true),
                    FS.FrameWork.Function.NConvert.ToInt32(true), obj.SIMainInfo.Memo, obj.SIMainInfo.OfficalCost, "1", 0);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        #endregion

        #region 得到结算序号
        /// <summary>
        /// 得到结算序号
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns></returns>
        public string GetBalNo(string patientSeqNo)
        {
            string strSql = "";
            string balNo = "";
            if (this.Sql.GetSql("Fee.Interface.GetBalNo.1", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.GetBalNo.1]对应sql语句出错";

                return "";
            }
            try
            {
                strSql = string.Format(strSql, patientSeqNo);
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
        
        public string GetRegNo(string inpatienNo, string invoceNo)
        {
            string sql = "select a.reg_no from fin_ipr_siinmaininfo a where a.inpatient_no = '{0}' and a.invoice_no = '{1}'";
            sql = string.Format(sql, inpatienNo, invoceNo);
            string res = this.ExecSqlReturnOne(sql);
            if (res == "-1")
            {
                res = "";
            }
            return res;
        }
        #endregion

        #region 判断是否已结算
        /// <summary>
        /// 判断是否已结算
        /// 
        /// {1A3FD005-392E-418e-998B-BD98B5E4CB49}
        /// </summary>
        /// <param name="strInpatientID"></param>
        /// <returns>-1查询失败；0未结算；1已结算</returns>
        public int IsInPatientBalance(string strInpatientID)
        {
            string strSQL = "";
            if (this.Sql.GetSql("Fee.Interface.QueryIsBalance.1", ref strSQL) == -1)
            {
                this.Err = "获得[Fee.Interface.QueryIsBalance.1]对应sql语句出错";

                return -1;
            }

            int iRes = -1;
            try
            {
                strSQL = string.Format(strSQL, strInpatientID);

                this.ExecQuery(strSQL);
                iRes = 0;
                while (this.Reader.Read())
                {
                    iRes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString());
                    break;
                }

                Reader.Close();
                return iRes;
            }
            catch (Exception objEx)
            {
                this.ErrCode = objEx.Message;
                this.Err = objEx.Message;

                return -1;
            }
        }

        #endregion

        // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
        #region 根据广州模式插入医保费用信息 -- 住院 
        /// <summary>
        /// 获取结算ID
        /// </summary>
        /// <returns></returns>
        public string GetJSID()
        {
            string getIDSql = "select  SEQ_GZSI_HIS_FYJS.Nextval from dual";
            string jsid =  this.ExecSqlReturnOne(getIDSql);
            if(jsid == "-1")
            {
                return "";
            }
            return jsid;
        }



        /// <summary>
        /// 插入医保上传的费用明细信息
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int InsertInPatientUploadFeeDetail(FS.HISFC.Models.RADT.PatientInfo obj, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            if (f == null)
            {
                this.Err = "插入医保费用明细失败，费用为空！";
                return -1;
            }
            if (obj == null)
            {
                this.Err = "插入医保费用明细失败，患者信息为空！";
                return -1;
            }

            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertInPatientUploadFeeDetail.1", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.InsertInPatientUploadFeeDetail.1]对应sql语句出错";
                return -1;
            }
            string[] memoList = obj.SIMainInfo.Memo.Split('|');


            string strBalanceNo = "";
            if (memoList != null && memoList.Length > 0)
            {
                strBalanceNo = memoList[0];
            }

            try
            {
                strSql = string.Format(strSql, obj.SIMainInfo.RegNo,
                    string.IsNullOrEmpty(obj.SIMainInfo.HosNo) ? Function.Function.HospitalCode : obj.SIMainInfo.HosNo,
                    obj.IDCard,
                    obj.PID.CardNO,
                    obj.PVisit.InTime.ToString(),
                    f.ChargeOper.OperTime.ToString(),//5
                    f.SequenceNO,
                    f.Item.UserCode,
                    f.Item.Name,
                    f.Item.MinFee.ID,
                    f.Item.Specs,
                    f.Item.SpecialFlag1,//剂型  11
                    f.Item.SpecialPrice.ToString(),
                    f.Item.Qty,
                    f.FT.TotCost.ToString(),//14
                    "",
                    "",
                    "",
                    "",
                    "",//19
                    obj.PID.PatientNO,// 20 
                    obj.ID,
                    obj.SIMainInfo.InvoiceNo,
                    this.Operator.ID,
                    f.Item.MinFee.ID,//24
                    strBalanceNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
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
        /// 删除医保上传的费用明细
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int DeletePatientUploadFeeDetail(string lsh,string regNo)
        {
            if (string.IsNullOrEmpty(lsh))
            {
                this.Err = "删除费用明细失败，患者流水号为空！";
                return -1;
            }

            if (string.IsNullOrEmpty(regNo))
            {
                this.Err = "删除费用明细失败，就诊登记号为空！";
                return -1;
            }

            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.DeleteInPatientUploadFeeDetail", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.DeleteInPatientUploadFeeDetail]对应sql语句出错";
                return -1;
            }
            strSql = string.Format(strSql, lsh, regNo);

            try
            {
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
        /// 插入结算后的信息
        /// </summary>
        /// <param name="balanceStr"></param>
        /// <returns></returns>
        public int SaveBlanceSIInPatient(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            if (obj == null)
            {
                this.Err = "插入医保结算失败，患者信息为空！";
                return -1;
            }
            if (string.IsNullOrEmpty(obj.SIMainInfo.Memo))
            {
                this.Err = "获取医保结算信息失败！";
                return -1;
            }
            string[] balanceInfo = obj.SIMainInfo.Memo.Split('|');

            string strSql = "";
            //if (balanceInfo != null && balanceInfo.Length > 0)
            {
                string strBalanceNo = "";

                if (balanceInfo != null && balanceInfo.Length > 0)
                {                    
                    strBalanceNo = balanceInfo[0];
                }
                if (string.IsNullOrEmpty(strBalanceNo))
                {
                    this.Err = "插入医保结算失败，结算单号为空！";
                    return -1;
                }

                string delSQL = "delete from gzsi_his_fyjs where JSID = '{0}'";
                delSQL = string.Format(delSQL, strBalanceNo);
                this.ExecNoQuery(delSQL);


                if (this.Sql.GetSql("Fee.Interface.SaveBlanceSIInPatient.1", ref strSql) == -1)
                {
                    this.Err = "获得[Fee.Interface.SaveBlanceSIInPatient.1]对应sql语句出错";
                    return -1;
                }

                strSql = string.Format(strSql, obj.ID,//住院流水号
                    obj.SIMainInfo.InvoiceNo,//结算发票号
                    obj.SIMainInfo.RegNo,//就医登记号
                    obj.SIMainInfo.FeeTimes,//费用批次
                    string.IsNullOrEmpty(obj.SIMainInfo.HosNo) ? Function.Function.HospitalCode : obj.SIMainInfo.HosNo,//医院编号
                    obj.IDCard,//5身份证号
                    obj.PID.PatientNO,// 门诊号/住院号
                    obj.PVisit.InTime.ToString(),//入院日期
                    obj.BalanceDate.ToString(),//结算日期
                    obj.SIMainInfo.TotCost.ToString(),//总金额
                    obj.SIMainInfo.PubCost.ToString(),//社保支付金额
                    obj.SIMainInfo.PayCost.ToString(),// 11账户支付金额
                    "0",//部分项目自付金额
                    "0",//个人起付金额
                    obj.SIMainInfo.OwnCost.ToString(),//14个人自费项目金额
                    obj.SIMainInfo.PayCost.ToString(),//个人自付金额  15
                    obj.SIMainInfo.PayCost.ToString(),//个人自负金额
                    "0",//超统筹支付限额个人自付金额
                    "",//自费原因
                    "0",//19医药机构分单金额
                    "",// 20 备注1,记录产生时间
                    "",//备注2
                    "",//备注3
                    "",//读入标志
                    this.Operator.ID,//操作员 24
                    strBalanceNo)// 25
                    ;

            }
            //else
            //{
            //    this.Err = "结算信息不正确！";
            //    return -1;
            //}

            return this.ExecNoQuery(strSql);

        }


        /// <summary>
        /// 作废医保结算
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateBlanceSIPatient(string lsh, string regNo)
        {
            if (string.IsNullOrEmpty(lsh))
            {
                this.Err = "删除费用明细失败，患者流水号为空！";
                return -1;
            }

            if (string.IsNullOrEmpty(regNo))
            {
                this.Err = "删除费用明细失败，就诊登记号为空！";
                return -1;
            }
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.UpdateBlanceSIInPatient", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.UpdateBlanceSIInPatient]对应sql语句出错";
                return -1;
            }
            strSql = string.Format(strSql, lsh, regNo);

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        #endregion

        // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
        #region 根据广州模式插入医保费用信息 -- 门诊

        /// <summary>
        /// 插入医保上传的费用明细信息
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int InsertIOutPatientUploadFeeDetail(FS.HISFC.Models.Registration.Register obj, FS.HISFC.Models.Fee.FeeItemBase f)
        {
            if (f == null)
            {
                this.Err = "插入医保费用明细失败，费用为空！";
                return -1;
            }
            if (obj == null)
            {
                this.Err = "插入医保费用明细失败，患者信息为空！";
                return -1;
            }
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertInPatientUploadFeeDetail.1", ref strSql) == -1)
            {
                this.Err = "获得[Fee.Interface.InsertInPatientUploadFeeDetail.1]对应sql语句出错";
                return -1;
            }
            string[] memoList = obj.SIMainInfo.Memo.Split('|');


            string strBalanceNo = "";
            if (memoList != null && memoList.Length > 0)
            {
                strBalanceNo = memoList[0];
            }

            try
            {
                strSql = string.Format(strSql, obj.SIMainInfo.RegNo,
                    obj.SIMainInfo.HosNo,
                    obj.IDCard,
                    obj.PID.CardNO,
                    obj.PVisit.InTime.ToString(),
                    f.ChargeOper.OperTime.ToString(),//5
                    f.SequenceNO,
                    f.Item.UserCode,
                    f.Item.Name,
                    f.Item.MinFee.ID,
                    f.Item.Specs,
                    f.Item.SpecialFlag1,//剂型  11
                    f.Item.SpecialPrice.ToString(),
                    f.Item.Qty,
                    (f.Item.Qty * f.Item.SpecialPrice).ToString("F2"),//14
                    "",
                    "",
                    "",
                    "",
                    "",//19
                    obj.PID.PatientNO,// 20 
                    obj.ID,
                    obj.SIMainInfo.InvoiceNo,
                    this.Operator.ID,
                    f.Item.MinFee.ID,//24
                    strBalanceNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
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
        /// 插入结算后的信息
        /// </summary>
        /// <param name="balanceStr"></param>
        /// <returns></returns>
        public int SaveBlanceSIOutPatient(FS.HISFC.Models.Registration.Register obj)
        {
            if (string.IsNullOrEmpty(obj.SIMainInfo.Memo))
            {
                this.Err = "获取医保结算信息失败！";
                return -1;
            }
            string[] balanceInfo = obj.SIMainInfo.Memo.Split('|');
            //if (balanceInfo != null && balanceInfo.Length > 0)
            {
                string strBalanceNo = "";

                if (balanceInfo != null && balanceInfo.Length > 0)
                {
                    strBalanceNo = balanceInfo[0];
                }

                string strSql = "";

                if (this.Sql.GetSql("Fee.Interface.SaveBlanceSIInPatient.1", ref strSql) == -1)
                {
                    this.Err = "获得[Fee.Interface.SaveBlanceSIInPatient.1]对应sql语句出错";
                    return -1;
                }

                strSql = string.Format(strSql, obj.ID,//住院流水号
                    obj.SIMainInfo.InvoiceNo,//结算发票号
                    obj.SIMainInfo.RegNo,//就医登记号
                    obj.SIMainInfo.FeeTimes,//费用批次
                    string.IsNullOrEmpty(obj.SIMainInfo.HosNo) ? Function.Function.HospitalCode : obj.SIMainInfo.HosNo,//医院编号
                    obj.IDCard,//5身份证号
                    obj.PID.PatientNO,// 门诊号/住院号
                    obj.DoctorInfo.SeeDate.ToString(),//入院日期
                    obj.DoctorInfo.SeeDate.ToString(),//结算日期
                    obj.SIMainInfo.TotCost.ToString(),//总金额
                    obj.SIMainInfo.PubCost.ToString(),//社保支付金额
                    obj.SIMainInfo.PayCost.ToString(),// 11账户支付金额
                    "0",//部分项目自付金额
                    "0",//个人起付金额
                    obj.SIMainInfo.OwnCost.ToString(),//14个人自费项目金额
                    obj.SIMainInfo.PayCost.ToString(),//个人自付金额  15
                    obj.SIMainInfo.PayCost.ToString(),//个人自负金额
                    "0",//超统筹支付限额个人自付金额
                    "",//自费原因
                    "0",//19医药机构分单金额
                    "",// 20 备注1,记录产生时间
                    "",//备注2
                    "",//备注3
                    "",//读入标志
                    this.Operator.ID,//操作员 24
                    strBalanceNo)// 25
                    ;

                try
                {
                    return this.ExecNoQuery(strSql);
                }
                catch (Exception ex)
                {
                    this.ErrCode = ex.Message;
                    this.Err = ex.Message;
                    return -1;
                }
            }
            //else
            //{
            //    this.Err = "结算信息不正确！";
            //    return -1;
            //}

            return 1;

        }


        /// <summary>
        /// 获取HIS与社保对照码// {405B92DC-4786-4c78-9476-8841F28FF5FE}
        /// </summary>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Const QuerySiCompare(string hisCode)
        {
            string strSql = @"
                            SELECT t.HIS_CODE,t.HIS_NAME,t.CENTER_CODE,t.CENTER_NAME
                            FROM FIN_COM_COMPARE t
                            WHERE t.PACT_CODE = '99'
                            AND t.HIS_CODE = '{0}'
                            ";

            strSql = string.Format(strSql, hisCode);
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

                    obj.ID = this.Reader[0].ToString().Trim();   //HIS编码
                    obj.Name = this.Reader[1].ToString().Trim();  //his名称
                    obj.UserCode = this.Reader[2].ToString();   //社保编码
                    obj.Memo = this.Reader[3].ToString();       //社保名称
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
        /// 获取HIS与社保自费的对照码
        /// </summary>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        public string QuerySiCompareByOwnFee(string hisCode)
        {
            string strSql = @"
                            SELECT t.CENTER_CODE
                            FROM FIN_COM_COMPARE t
                            WHERE t.PACT_CODE = '98'
                            AND t.HIS_CODE = '{0}'
                            and t.CENTER_CODE is not null
                            ";

            strSql = string.Format(strSql, hisCode);

            string res = this.ExecSqlReturnOne(strSql);
            if (res == "-1" || res == "0")
            {
                res = "";
            }

            return res;
        }
        /// <summary>
        /// 查询是自费或报销编码// {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
        /// </summary>
        /// <param name="moOrder"></param>
        /// <param name="personType">1门诊 2住院</param>
        /// <returns></returns>
        public string QueryFeeOwnOrPut(string moOrder,string itemCode, string personType)
        {
            string sql = "";
            if (personType == "1")
            {
                sql = @"select d.indications from met_ord_orderextend d,fin_opb_feedetail f
                        where d.mo_order = f.mo_order
                        and d.clinic_code = f.clinic_code
                        and f.mo_order = '{0}'";
                sql = string.Format(sql, moOrder);

                string res = this.ExecSqlReturnOne(sql);
                if (res == "0")
                {
                    string ownCode = this.QuerySiCompareByOwnFee(itemCode);
                    return ownCode;

                }
                else
                {
                    return "";
                }
            }
            else
            {
                sql = @"select d1.indications from met_ipm_orderextend d1, met_ipm_order o
                        where d1.mo_order = o.mo_order
                        and d1.inpatient_no = o.inpatient_no
                        and o.mo_order = '{0}' ";
                sql = string.Format(sql, moOrder);

                string res = this.ExecSqlReturnOne(sql);
                if (res == "0")
                {
                    string ownCode = this.QuerySiCompareByOwnFee(itemCode);
                    return ownCode;

                }
                else
                {
                    return "";
                }
            }

            return "";
        }

        /// <summary>
        /// 获取HIS与社保自费的对照码（住院）
        /// </summary>
        /// <param name="hisCode"></param>
        /// <returns></returns>
        public string QuerySiCompareByInOwnFee(string hisCode)
        {
            string strSql = @" SELECT t.CENTER_CODE
                            FROM FIN_COM_COMPARE t,fin_com_undruginfo u
                            WHERE t.his_code = u.item_code
                            and t.PACT_CODE = '98'
                            AND u.input_code = '{0}'
                            and t.CENTER_CODE is not null
                            union all
                             SELECT t.CENTER_CODE
                            FROM FIN_COM_COMPARE t,pha_com_baseinfo b
                            WHERE t.his_code = b.drug_code
                            and t.PACT_CODE = '98'
                            AND b.custom_code = '{0}'
                            and t.CENTER_CODE is not null
                            ";

            strSql = string.Format(strSql, hisCode);

            string res = this.ExecSqlReturnOne(strSql);
            if (res == "-1" || res == "0")
            {
                res = "";
            }

            return res;
        }
        #endregion
    }
}
