using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.GuangZhou
{
    public class SILocalManager : FS.FrameWork.Management.Database
    {
        #region 医保登记相关

        /// <summary>
        /// 保存医保登记信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int SaveSIMainInfo(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            if (string.IsNullOrEmpty((obj.SIMainInfo.RegNo)))
            {
                this.Err = "参数不正确。";
                return -1;
            }
            if (ISRegistered(obj.SIMainInfo.RegNo))
            {
                return UpdateSiMainInfo(obj);
            }
            string balNo = GetBalNo(obj.ID);
            if (balNo == null || balNo == "")
            {
                balNo = "0";
            }
            balNo = (Convert.ToInt32(balNo) + 1).ToString();
            string strSql = "";
            if (this.Sql.GetSql("Fee.Interface.InsertSIMainInfo.1.new", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, obj.ID, balNo, obj.SIMainInfo.InvoiceNo, obj.PVisit.MedicalType.ID, obj.PID.PatientNO,
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
                    obj.SIMainInfo.OverTakeOwnCost, FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid),
                    FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced), "2");
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
        /// 保存医保登记信息(门诊部分)
        /// </summary>
        /// <param name="obj">挂号信息</param>
        /// <returns>-1 失败 1成功</returns>
        public int SaveSIMainInfo(FS.HISFC.Models.Registration.Register obj)
        {
            if (string.IsNullOrEmpty((obj.SIMainInfo.RegNo)))
            {
                this.Err = "参数不正确。";
                return -1;
            }
            if (ISRegistered(obj.SIMainInfo.RegNo))
            {
                return UpdateSiMainInfo(obj);
            }
            string strSql = "";
            if (this.Sql.GetSql("Fee.Interface.InsertSIMainInfo.1.new", ref strSql) == -1)
                return -1;
            string balNo = GetBalNo(obj.ID);
            if (balNo == null || balNo == "")
            {
                balNo = "0";
            }
            balNo = (Convert.ToInt32(balNo) + 1).ToString();
            try
            {
                string[] str = new string[]{
                                                obj.ID, //0
                                                balNo, 
                                                obj.SIMainInfo.InvoiceNo, 
                                                obj.SIMainInfo.MedicalType.ID, 
                                                obj.PID.CardNO,
                                                obj.PID.CardNO, 
                                                obj.SSN, 
                                                obj.SIMainInfo.AppNo.ToString(), 
                                                obj.SIMainInfo.ProceatePcNo,
											   obj.SIMainInfo.SiBegionDate.ToString(), 
                                               obj.SIMainInfo.SiState, //10
                                               obj.Name,
                                               obj.Sex.ID.ToString(),
											   obj.IDCard, 
                                               "", 
                                               obj.Birthday.ToString(), 
                                               obj.SIMainInfo.EmplType,
                                               obj.CompanyName,
											   obj.SIMainInfo.ClinicDiagNose, 
                                               obj.DoctorInfo.Templet.Dept.ID, 
                                               obj.DoctorInfo.Templet.Dept.Name,//20 
											   obj.Pact.PayKind.ID,
                                               obj.Pact.ID,
                                               obj.Pact.Name, 
                                               "", 
											   obj.DoctorInfo.SeeDate.ToString(), 
                                               obj.DoctorInfo.SeeDate.ToString(), 
                                               obj.SIMainInfo.InDiagnose.ID,
											   obj.SIMainInfo.InDiagnose.Name, 
                                               this.Operator.ID, 
                                               obj.SIMainInfo.HosNo,
                                               obj.SIMainInfo.RegNo,
											   obj.SIMainInfo.FeeTimes.ToString(), 
                                               obj.SIMainInfo.HosCost.ToString(), 
                                               obj.SIMainInfo.YearCost.ToString(),
                                               obj.DoctorInfo.Templet.End.ToString(),
											   obj.SIMainInfo.OutDiagnose.ID, 
                                               obj.SIMainInfo.OutDiagnose.Name, 
                                               obj.SIMainInfo.BalanceDate.ToString(),
											   obj.SIMainInfo.TotCost.ToString(),
                                               obj.SIMainInfo.PayCost.ToString(), //40 
                                               obj.SIMainInfo.PubCost.ToString(),
                                               obj.SIMainInfo.ItemPayCost.ToString(),
											   obj.SIMainInfo.BaseCost.ToString(), 
                                               obj.SIMainInfo.ItemYLCost.ToString(), 
                                               obj.SIMainInfo.PubOwnCost.ToString(), 
                                               obj.SIMainInfo.OwnCost.ToString(),
											   obj.SIMainInfo.OverTakeOwnCost.ToString(),
                                               FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid).ToString(),
											   FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced).ToString(), 
                                               "1"//50                                               
										   };
                return this.ExecNoQuery(strSql, str);
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
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateSiMainInfo(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            if (string.IsNullOrEmpty(obj.SIMainInfo.RegNo))
            {
                this.Err = "参数错误";
                return -1;
            }
            string strSql = "";
            string balNo = this.GetBalNo(obj.ID);
            if (balNo == null || balNo == "")
            {
                balNo = "0";
            }
            if (this.Sql.GetSql("Fee.Interface.UpdateSiMainInfo.Update", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql,
                                                    obj.SIMainInfo.RegNo,
                                                    balNo,
                                                    obj.SIMainInfo.InvoiceNo,
                                                    obj.PVisit.MedicalType.ID,
                                                    obj.PID.PatientNO,
                                                    obj.PID.CardNO,
                                                    obj.SSN,
                                                    obj.SIMainInfo.AppNo,
                                                    obj.SIMainInfo.ProceatePcNo,
                                                    obj.SIMainInfo.SiBegionDate.ToString(),
                                                    obj.SIMainInfo.SiState,
                                                    obj.Name,
                                                    obj.Sex.ID.ToString(),
                                                    obj.IDCard,
                                                    "",
                                                    obj.Birthday.ToString(),
                                                    obj.SIMainInfo.EmplType,
                                                    obj.CompanyName,
                                                    obj.SIMainInfo.InDiagnose.Name,
                                                    obj.PVisit.PatientLocation.Dept.ID,
                                                    obj.PVisit.PatientLocation.Dept.Name,
                                                    obj.Pact.PayKind.ID, obj.Pact.ID,
                                                    obj.Pact.Name,
                                                    obj.PVisit.PatientLocation.Bed.ID,
                                                    obj.PVisit.InTime.ToString(),
                                                    obj.PVisit.InTime.ToString(),
                                                    obj.SIMainInfo.InDiagnose.ID,
                                                    obj.SIMainInfo.InDiagnose.Name,
                                                    obj.PVisit.OutTime,
                                                    obj.SIMainInfo.OutDiagnose.ID,
                                                    obj.SIMainInfo.OutDiagnose.Name,
                                                    obj.SIMainInfo.BalanceDate.ToString(),
                                                    obj.SIMainInfo.TotCost,
                                                    obj.SIMainInfo.PayCost,
                                                    obj.SIMainInfo.PubCost,
                                                    obj.SIMainInfo.ItemPayCost,
                                                    obj.SIMainInfo.BaseCost,
                                                    obj.SIMainInfo.PubOwnCost,
                                                    obj.SIMainInfo.ItemYLCost,
                                                    obj.SIMainInfo.OwnCost,
                                                    obj.SIMainInfo.OverTakeOwnCost,
                                                    obj.SIMainInfo.Memo,
                                                    this.Operator.ID,
                                                    obj.SIMainInfo.RegNo,
                                                    obj.SIMainInfo.FeeTimes,
                                                    obj.SIMainInfo.HosCost,
                                                    obj.SIMainInfo.YearCost,
                                                    FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid),
                                                    FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced),
                                                    0,
                                                    0,
                                                    obj.SIMainInfo.TypeCode
                                                    );
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
        /// 更新医保结算表(门诊部分)
        /// </summary>
        /// <param name="obj">挂号信息</param>
        /// <returns>-1 失败 1成功</returns>
        public int UpdateSiMainInfo(FS.HISFC.Models.Registration.Register obj)
        {
            if (string.IsNullOrEmpty(obj.SIMainInfo.RegNo))
            {
                this.Err = "参数错误";
                return -1;
            }
            string strSql = "";
            if (this.Sql.GetSql("Fee.Interface.UpdateSiMainInfo.Update", ref strSql) == -1)
                return -1;
            string balNo = GetBalNo(obj.ID);
            if (balNo == null || balNo == "")
            {
                balNo = "0";
            }
            try
            {
                strSql = string.Format(strSql,
                                            obj.SIMainInfo.RegNo,
                                            balNo,
                                            obj.SIMainInfo.InvoiceNo,
                                            obj.SIMainInfo.MedicalType.ID,
                                            obj.PID.CardNO,
                                            obj.PID.CardNO,
                                            obj.SSN,
                                            obj.SIMainInfo.AppNo.ToString(),
                                            obj.SIMainInfo.ProceatePcNo,
                                            obj.SIMainInfo.SiBegionDate.ToString(),
                                            obj.SIMainInfo.SiState,
                                            obj.Name,
                                            obj.Sex.ID.ToString(),
                                            obj.IDCard,
                                            "",//14      
                                            obj.Birthday.ToString(),
                                            obj.SIMainInfo.EmplType,
                                            obj.CompanyName,
                                            obj.SIMainInfo.ClinicDiagNose,
                                            obj.DoctorInfo.Templet.Dept.ID,
                                            obj.DoctorInfo.Templet.Dept.Name,
                                            obj.Pact.PayKind.ID,
                                            obj.Pact.ID,
                                            obj.Pact.Name,
                                            "",
                                            obj.DoctorInfo.SeeDate.ToString(), //25
                                            obj.DoctorInfo.SeeDate.ToString(),
                                            obj.SIMainInfo.InDiagnose.ID,
                                            obj.SIMainInfo.InDiagnose.Name,
                                            obj.DoctorInfo.Templet.End.ToString(),
                                            obj.SIMainInfo.OutDiagnose.ID,
                                            obj.SIMainInfo.OutDiagnose.Name,
                                            obj.SIMainInfo.BalanceDate.ToString(),
                                            obj.SIMainInfo.TotCost.ToString(),
                                            obj.SIMainInfo.PayCost.ToString(),
                                            obj.SIMainInfo.PubCost,
                                            obj.SIMainInfo.ItemPayCost,
                                            obj.SIMainInfo.BaseCost,
                                            obj.SIMainInfo.PubOwnCost,
                                            obj.SIMainInfo.ItemYLCost,
                                            obj.SIMainInfo.OwnCost, //40
                                            obj.SIMainInfo.OverTakeOwnCost,
                                            obj.SIMainInfo.Memo,
                                            this.Operator.ID,
                                            obj.SIMainInfo.RegNo,
                                            obj.SIMainInfo.FeeTimes,
                                            obj.SIMainInfo.HosCost,
                                            obj.SIMainInfo.YearCost.ToString(),
                                            FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid),
                                            FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced),
                                            0, //50
                                            0,
                                            obj.SIMainInfo.TypeCode
                                          );
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
        /// 更新医保登记信息为无效
        /// </summary>
        /// <param name="inpatient_no"></param>
        /// <param name="reg_no"></param>
        /// <returns></returns>
        public int UpdateSiMainInfoDisable(string inpatient_no, string reg_no)
        {
            string strSql = "";
            if (this.Sql.GetSql("Fee.Interface.UpdateSiMainInfo.Update.Disable", ref strSql) == -1)
            {
                return -1;
            }
            strSql = string.Format(strSql, inpatient_no, reg_no);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新医保登记信息为无效
        /// </summary>
        /// <param name="inpatient_no"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public int UpdateSiMainInfoDisableByInvoiceNO(string inpatient_no, string invoiceNO)
        {
            if (string.IsNullOrEmpty(inpatient_no) || string.IsNullOrEmpty(invoiceNO))
            {
                this.Err = "UpdateSiMainInfoDisableByInvoiceNO方法参数错误。";
                return -1;
            }
            string strSql = string.Format("update fin_ipr_siinmaininfo i set i.valid_flag='0' where  i.inpatient_no='{0}' and i.invoice_no='{1}'", inpatient_no, invoiceNO);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新医保登记信息为无效
        /// </summary>
        /// <param name="siRegNO"></param>
        /// <param name="jsid"></param>
        /// <returns></returns>
        public int UpdateInPatientSiMainInfoDisable(string inpatient_no, string siRegNO)
        {
            if (string.IsNullOrEmpty(inpatient_no) || string.IsNullOrEmpty(siRegNO))
            {
                this.Err = "UpdateSiMainInfoDisableByInvoiceNO方法参数错误。";
                return -1;
            }
            string strSql = string.Format(@"update fin_ipr_siinmaininfo i set i.valid_flag='0' 
where not exists (select 1 from gzsi_his_fyjs t where t.inpatient_no=i.inpatient_no and t.valid_flag='1')
and i.inpatient_no='{0}' and i.reg_no='{1}'", inpatient_no, siRegNO);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新医保登记信息为无效
        /// </summary>
        /// <param name="siRegNO"></param>
        /// <param name="jsid"></param>
        /// <returns></returns>
        public int UpdateOutPatientSiMainInfoDisable(string inpatient_no, string siRegNO)
        {
            if (string.IsNullOrEmpty(inpatient_no) || string.IsNullOrEmpty(siRegNO))
            {
                this.Err = "UpdateSiMainInfoDisableByInvoiceNO方法参数错误。";
                return -1;
            }
            string strSql = string.Format(@"update fin_ipr_siinmaininfo i set i.valid_flag='0' 
where not exists (select 1 from gzsi_his_mzjs t where t.clinic_code=i.inpatient_no and t.valid_flag='1')
and i.inpatient_no='{0}' and i.reg_no='{1}'", inpatient_no, siRegNO);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 获取有效的医保登记号
        /// </summary>
        /// <param name="registerID"></param>
        /// <returns></returns>
        public string GetPatientLastSIRegNO(string registerID, string registerType)
        {
            string medical_type = "1";
            switch (registerType)
            {
                case "I": medical_type = "1"; break;
                case "S": medical_type = "2"; break;
                case "O": medical_type = "3"; break;
                default: medical_type = "1";
                    break;
            }
            string sql1 = string.Format(@"select max(i.reg_no) from fin_ipr_siinmaininfo i where  i.inpatient_no='{0}' and valid_flag='1' and i.medical_type='{1}'", registerID, medical_type);
            this.ExecQuery(sql1);
            string regNO = string.Empty;
            try
            {
                while (Reader.Read())
                {
                    regNO = Reader[0].ToString();
                }
                return regNO;
            }
            catch
            {
                throw;
            }
            finally
            {
                Reader.Close();
            }
        }

        /// <summary>
        /// 判断是否有有效的医保登记信息
        /// </summary>
        /// <param name="registerID"></param>
        /// <returns>已有则返回true，没有则返回false</returns>
        public bool ISRegistered(string siRegNO)
        {
            string sql1 = string.Format(@"select max(i.reg_no) from fin_ipr_siinmaininfo i where  i.reg_no='{0}' and valid_flag='1'", siRegNO);
            this.ExecQuery(sql1);
            string regNO = string.Empty;
            try
            {
                while (Reader.Read())
                {
                    regNO = Reader[0].ToString();
                }
                if (string.IsNullOrEmpty(regNO))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Reader.Close();
            }
        }

        /// <summary>
        /// 得到医保登记信息;
        /// </summary>
        /// <param name="siRegNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Register GetOutPatientSIRegInfoBySIRegNO(string siRegNO)
        {
            FS.HISFC.Models.Registration.Register obj = new FS.HISFC.Models.Registration.Register();
            string strSql = "";
            if (this.Sql.GetSql("Fee.Interface.GetSIPersonInfo.Select", ref strSql) == -1)
                return null;
            try
            {
                strSql = string.Format(strSql, siRegNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {
                    obj.SIMainInfo.HosNo = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SIMainInfo.BalNo = Reader[2].ToString();
                    obj.SIMainInfo.InvoiceNo = Reader[3].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[4].ToString();
                    if (obj.SIMainInfo.MedicalType.ID == "1")
                    { obj.SIMainInfo.MedicalType.Name = "住院"; }
                    else if (obj.SIMainInfo.MedicalType.ID == "2")
                    { obj.SIMainInfo.MedicalType.Name = "门诊特定项目"; }
                    else if (obj.SIMainInfo.MedicalType.ID == "2")
                    {
                        obj.SIMainInfo.MedicalType.Name = "门诊";
                    }
                    obj.PID.PatientNO = Reader[5].ToString();
                    obj.PID.CardNO = Reader[6].ToString();
                    obj.SSN = Reader[7].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[8].ToString());
                    obj.SIMainInfo.ProceatePcNo = Reader[9].ToString();
                    obj.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    obj.SIMainInfo.SiState = Reader[11].ToString();
                    obj.Name = Reader[12].ToString();
                    obj.Sex.ID = Reader[13].ToString();
                    obj.IDCard = Reader[14].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                    obj.SIMainInfo.EmplType = Reader[16].ToString();
                    obj.CompanyName = Reader[17].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[18].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[19].ToString();
                    obj.PVisit.PatientLocation.Dept.Name = Reader[20].ToString();
                    obj.Pact.PayKind.ID = Reader[21].ToString();
                    obj.Pact.ID = Reader[22].ToString();
                    obj.Pact.Name = Reader[23].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[24].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnose.ID = Reader[26].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[27].ToString();
                    if (!Reader.IsDBNull(28))
                        obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());
                    obj.SIMainInfo.OutDiagnose.ID = Reader[29].ToString();
                    obj.SIMainInfo.OutDiagnose.Name = Reader[30].ToString();
                    if (!Reader.IsDBNull(31))
                        obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString());
                    obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());
                    obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());
                    obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());
                    obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());
                    obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());
                    obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());
                    obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString());
                    obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString());
                    obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString());
                    obj.SIMainInfo.Memo = Reader[41].ToString();
                    obj.SIMainInfo.OperInfo.ID = Reader[42].ToString();
                    obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[43].ToString());
                    obj.SIMainInfo.RegNo = Reader[44].ToString();
                    obj.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[45].ToString());
                    obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[46].ToString());
                    obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[47].ToString());
                    obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[48].ToString());
                    obj.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString());
                }
                if (string.IsNullOrEmpty(obj.SIMainInfo.HosNo) && obj.SIMainInfo.RegNo.Length > 6)
                {
                    obj.SIMainInfo.HosNo = obj.SIMainInfo.RegNo.Substring(0, 6);
                }
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                Reader.Close();
            }
        }

        /// <summary>
        /// 得到医保登记信息;
        /// </summary>
        /// <param name="siRegNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetInPatientSIRegInfoBySIRegNO(string siRegNO)
        {
            FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.GetSIPersonInfo.Select", ref strSql) == -1)
            {
                return null;
            }

            try
            {
                strSql = string.Format(strSql, siRegNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            
            List<FS.HISFC.Models.RADT.PatientInfo> patientList = getSIRegisterInfo(strSql);

            if (patientList == null || patientList.Count == 0)
            {
                return null;
            }
            else
            {
                obj = patientList[0];
                return obj;
            }
        }

        ///{EC30FC86-C175-46e7-A757-A66A0854568E}
        /// <summary>
        /// 通过时间，住院号查询患者信息
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="patientNO"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> GetInPatientByDatePatientNO(DateTime beginDate, DateTime endDate, string patientNO)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.GetSIPersonInfo.Select.2", ref strSql) == -1)
            {
                return null;
            }

            try
            {
                strSql = string.Format(strSql, patientNO,beginDate.ToShortDateString(), endDate.ToShortDateString());
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            
            List<FS.HISFC.Models.RADT.PatientInfo> patientList = getSIRegisterInfo(strSql);

            return patientList;
        }

        /// <summary>
        /// 获取医保患者登记信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.RADT.PatientInfo> getSIRegisterInfo(string sql)
        {
            List<FS.HISFC.Models.RADT.PatientInfo> patientList = new List<FS.HISFC.Models.RADT.PatientInfo>();

            this.ExecQuery(sql);
            try
            {
                while (Reader.Read())
                {
                    FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
                    obj.SIMainInfo.HosNo = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SIMainInfo.BalNo = Reader[2].ToString();
                    obj.SIMainInfo.InvoiceNo = Reader[3].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[4].ToString();
                    if (obj.SIMainInfo.MedicalType.ID == "1") { obj.SIMainInfo.MedicalType.Name = "住院"; }
                    else if (obj.SIMainInfo.MedicalType.ID == "2") { obj.SIMainInfo.MedicalType.Name = "门诊特定项目"; }
                    else if (obj.SIMainInfo.MedicalType.ID == "2") { obj.SIMainInfo.MedicalType.Name = "门诊"; }
                    obj.PID.PatientNO = Reader[5].ToString();
                    obj.PID.CardNO = Reader[6].ToString();
                    obj.SSN = Reader[7].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[8].ToString());
                    obj.SIMainInfo.ProceatePcNo = Reader[9].ToString();
                    obj.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    obj.SIMainInfo.SiState = Reader[11].ToString();
                    obj.Name = Reader[12].ToString();
                    obj.Sex.ID = Reader[13].ToString();
                    obj.IDCard = Reader[14].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                    obj.SIMainInfo.EmplType = Reader[16].ToString();
                    obj.CompanyName = Reader[17].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[18].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[19].ToString();
                    obj.PVisit.PatientLocation.Dept.Name = Reader[20].ToString();
                    obj.Pact.PayKind.ID = Reader[21].ToString();
                    obj.Pact.ID = Reader[22].ToString();
                    obj.Pact.Name = Reader[23].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[24].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnose.ID = Reader[26].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[27].ToString();
                    if (!Reader.IsDBNull(28))
                        obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());
                    obj.SIMainInfo.OutDiagnose.ID = Reader[29].ToString();
                    obj.SIMainInfo.OutDiagnose.Name = Reader[30].ToString();
                    if (!Reader.IsDBNull(31))
                        obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString());
                    obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());
                    obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());
                    obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());
                    obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());
                    obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());
                    obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());
                    obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString());
                    obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString());
                    obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString());
                    obj.SIMainInfo.Memo = Reader[41].ToString();
                    obj.SIMainInfo.OperInfo.ID = Reader[42].ToString();
                    obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[43].ToString());
                    obj.SIMainInfo.RegNo = Reader[44].ToString();
                    obj.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[45].ToString());
                    obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[46].ToString());
                    obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[47].ToString());
                    obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[48].ToString());
                    obj.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString());

                    if (string.IsNullOrEmpty(obj.SIMainInfo.HosNo) && obj.SIMainInfo.RegNo.Length > 6)
                    {
                        obj.SIMainInfo.HosNo = obj.SIMainInfo.RegNo.Substring(0, 6);
                    }

                    patientList.Add(obj);
                }

                return patientList;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                Reader.Close();
            }
        }

        #endregion

        #region 医保费用明细相关


        /// <summary>
        /// 得到结算序号
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public string GetBalNo(string inpatientNo)
        {
            string strSql = "";
            string balNo = "";
            if (this.Sql.GetSql("Fee.Interface.GetBalNo.1", ref strSql) == -1)
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
        /// 获取门诊需要上传的费用明细
        /// </summary>
        /// <param name="registerID"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryOutPatientNeedUploadFeeDetail(string registerID, string invoiceNO)
        {
            string sql = string.Format(@"select * from v_fin_opb_fee_gzsi t where t.clinic_code='{0}' and t.invoice_no='{1}'", registerID, invoiceNO);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取门诊需要上传的费用明细
        /// </summary>
        /// <param name="registerID"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryOutPatientNeedUploadFeeDetail(string registerID)
        {
            string sql = string.Format(@"select * from v_fin_opb_fee_gzsi t where t.clinic_code='{0}'", registerID);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 删除本地保存的已上传已结算的费用明细信息
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public int DeleteOutPatientUploadFeeDetail(string clinicCode, string invoiceNO)
        {
            if (string.IsNullOrEmpty(clinicCode) || string.IsNullOrEmpty(invoiceNO))
            {
                this.Err = "DeleteOutPatientUploadFeeDetail方法参数错误。";
                return -1;
            }
            string strSQL = string.Format("delete from gzsi_his_mzxm t where t.clinic_code='{0}' and t.invoice_no='{1}'", clinicCode, invoiceNO);
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除本地保存的已上传已结算的费用明细信息
        /// </summary>
        /// <param name="jsid"></param>
        /// <returns></returns>
        public int DeleteOutPatientUploadFeeDetail(string jsid)
        {
            if (string.IsNullOrEmpty(jsid))
            {
                this.Err = "DeleteOutPatientUploadFeeDetail方法参数错误。";
                return -1;
            }
            string strSQL = string.Format("delete from gzsi_his_mzxm t where t.jsid='{0}'", jsid);
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除本地保存的已上传已结算的费用明细信息
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public int DeleteInPatientUploadFeeDetail(string inPatientNO, string invoiceNO)
        {
            if (string.IsNullOrEmpty(inPatientNO) || string.IsNullOrEmpty(invoiceNO))
            {
                this.Err = "DeleteInPatientUploadFeeDetail方法参数错误。";
                return -1;
            }
            string strSQL = string.Format("delete from GZSI_HIS_CFXM t where t.inpatient_no='{0}' and t.invoice_no='{1}'", inPatientNO, invoiceNO);
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除本地保存的已上传已结算的费用明细信息
        /// </summary>
        /// <param name="jsid"></param>
        /// <returns></returns>
        public int DeleteInPatientUploadFeeDetail(string jsid)
        {
            if (string.IsNullOrEmpty(jsid))
            {
                this.Err = "DeleteInPatientUploadFeeDetail方法参数错误。";
                return -1;
            }
            string strSQL = string.Format("delete from GZSI_HIS_CFXM t where t.jsid='{0}'", jsid);
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 把已上传已结算的费用明细信息保存到HIS本地
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="jsid">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertOutPatientUploadFeeDetail(FS.HISFC.Models.Registration.Register r, System.Data.DataTable dtFee, string jsid)
        {
            if (string.IsNullOrEmpty(jsid) || r == null || dtFee == null)
            {
                this.Err = "InsertOutPatientUploadFeeDetail方法参数错误。";
                return -1;
            }
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = r.SIMainInfo.RegNo.Substring(0, 6);
            }
            //保存前应该先删除
            this.DeleteOutPatientUploadFeeDetail(jsid);
            string strSQL = @"INSERT INTO GZSI_HIS_MZXM     --广州医保费用明细信息表
(
JYDJH,   --就医登记号
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
FYPC   --费用批次
,fee_code
,jsid
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
'{24}'   --费用批次
,'{25}'  --HIS费用类别
,'{26}'
) ";
            try
            {
                for (int i = 0; i < dtFee.Rows.Count; i++)
                {
                    string sql = string.Format(strSQL,
                        r.SIMainInfo.RegNo,
                        r.SIMainInfo.HosNo,
                        r.IDCard,
                        r.PID.CardNO,
                        r.DoctorInfo.SeeDate.ToString(),
                        dtFee.Rows[i]["FYRQ"],   //收费时间                                           
                        dtFee.Rows[i]["XMXH"],   //项目序号                                           
                        dtFee.Rows[i]["XMBH"],   //医院的项目编号                                     
                        dtFee.Rows[i]["XMMC"],   //医院的项目名称                                     
                        dtFee.Rows[i]["FLDM"],   //分类代码                                           
                        dtFee.Rows[i]["YPGG"],   //规格                                               
                        dtFee.Rows[i]["YPJX"],   //剂型                                               
                        dtFee.Rows[i]["JG"],	//单价                                                 
                        dtFee.Rows[i]["MCYL"],   //数量                                               
                        dtFee.Rows[i]["JE"],	//金额                                                 
                        dtFee.Rows[i]["BZ1"],	//备注1，存放记录产生时间                             
                        dtFee.Rows[i]["BZ2"],	//备注2                                               
                        dtFee.Rows[i]["BZ3"],	//备注3,存放费用明细读入时有效性检查的处理结果代码    
                        dtFee.Rows[i]["DRBZ"],   //读入标志                                           
                        dtFee.Rows[i]["YPLY"],   //1-国产 2-进口 3-合资 
                        r.ID,
                        r.PID.CardNO,
                        this.Operator.ID,
                        r.SIMainInfo.InvoiceNo,
                        r.SIMainInfo.FeeTimes.ToString(),
                        dtFee.Rows[i]["fee_code"],
                        jsid
                        );
                    if (this.ExecNoQuery(sql) < 0)
                    {
                        return -1;
                    }
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }

        public int InsertInPatientUploadFeeDetail(FS.HISFC.Models.RADT.PatientInfo r, System.Data.DataTable dtFee, string jsid)
        {
            if (string.IsNullOrEmpty(jsid) || r == null || dtFee == null)
            {
                this.Err = "InsertInPatientUploadFeeDetail方法参数错误。";
                return -1;
            }
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = r.SIMainInfo.RegNo.Substring(0, 6);
            }
            //保存前应该先删除
            this.DeleteInPatientUploadFeeDetail(jsid);
            string strSQL = @"INSERT INTO GZSI_HIS_CFXM     
(
JYDJH,   --就医登记号
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
PATIENT_NO,  --住院号
INPATIENT_NO,  --住院流水号
INVOICE_NO,     --结算发票号
OPER_CODE,
OPER_DATE,
FEE_CODE,
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
'{20}',   --住院号
'{21}',   --住院流水号
'{22}',   --结算发票号
'{23}',   --操作员
sysdate   --操作时间
,'{24}'
,'{25}'
) ";
            try
            {
                for (int i = 0; i < dtFee.Rows.Count; i++)
                {
                    string sql = string.Format(strSQL,
                        r.SIMainInfo.RegNo,
                        r.SIMainInfo.HosNo,
                        r.IDCard,
                        r.PID.PatientNO,
                        r.SIMainInfo.OperDate.ToString(),
                        dtFee.Rows[i]["FYRQ"],   //收费时间                                           
                        dtFee.Rows[i]["XMXH"],   //项目序号                                           
                        dtFee.Rows[i]["XMBH"],   //医院的项目编号                                     
                        dtFee.Rows[i]["XMMC"],   //医院的项目名称                                     
                        dtFee.Rows[i]["FLDM"],   //分类代码                                           
                        dtFee.Rows[i]["YPGG"],   //规格                                               
                        dtFee.Rows[i]["YPJX"],   //剂型                                               
                        dtFee.Rows[i]["JG"],	//单价                                                 
                        dtFee.Rows[i]["MCYL"],   //数量                                               
                        dtFee.Rows[i]["JE"],	//金额                                                 
                        dtFee.Rows[i]["BZ1"],	//备注1，存放记录产生时间                             
                        dtFee.Rows[i]["BZ2"],	//备注2                                               
                        dtFee.Rows[i]["BZ3"],	//备注3,存放费用明细读入时有效性检查的处理结果代码    
                        dtFee.Rows[i]["DRBZ"],   //读入标志                                           
                        dtFee.Rows[i]["YPLY"],   //1-国产 2-进口 3-合资 
                        r.PID.PatientNO,
                        r.ID,
                        dtFee.Rows[i]["invoice_no"],
                        this.Operator.ID,
                        dtFee.Rows[i]["fee_code"],
                        jsid
                        );
                    if (this.ExecNoQuery(sql) < 0)
                    {
                        return -1;
                    }
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 获取住院需要上传的费用明细
        /// </summary>
        /// <param name="registerID"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryInPatientNeedUploadFeeDetail(string registerID, string invoiceNO)
        {
            string sql = string.Format(@"select * from v_fin_ipb_fee_gzsi t where t.inpatient_no='{0}' and t.invoice_no='{1}'  and t.upload_flag<>'2' ", registerID, invoiceNO);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取住院需要上传的费用明细
        /// </summary>
        /// <param name="registerID"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryInPatientNeedUploadFeeDetail(string registerID)
        {
            string sql = string.Format(@"select * from v_fin_ipb_fee_gzsi t where t.inpatient_no='{0}'  and t.upload_flag<>'2' ", registerID);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取有匹配但手工选择不上传医保的项目
        /// </summary>
        /// <param name="registerID"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryInPatientNotUploadFeeDetail(string registerID)
        {
            string sql = string.Format(@"select * from v_fin_ipb_fee_gzsi t where t.inpatient_no='{0}'  and t.upload_flag='2' ", registerID);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }
        
        #endregion

        #region 医保结算信息相关

        /// <summary>
        /// 根据就诊流水号获取结算信息
        /// </summary>
        /// <param name="registerID"></param>
        /// <returns></returns>
        public System.Data.DataTable QueryOutPatientInvoiceinfo(string registerID)
        {
            string sql = string.Format(@"select i.invoice_no,i.pact_name 结算类型,i.name 姓名,i.card_no 门诊号,i.print_invoiceno 真实票据号,i.oper_date 操作时间,fun_get_employee_name(i.oper_code) 操作人
from fin_opb_invoiceinfo i where  i.cancel_flag='1' and  i.clinic_code='{0}'", registerID);
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sql, ref ds);
            return ds.Tables[0];
        }

        public int SaveBlanceSIOutPatient(FS.HISFC.Models.Registration.Register reg, ref string jsid)
        {
            //todo:存储结算结果可以考虑同时需要调用存储过程生成发票明细和发票信息
            //string sql1 = string.Format(@"select fypc from GZSI_HIS_MZJS where jydjh='{0}' and valid_flag='1'", reg.SIMainInfo.RegNo);
            //int fypc = 0;
            //this.ExecQuery(sql1);
            try
            {
                //while (Reader.Read())
                //{
                //    fypc = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                //}
                //Reader.Close();
                //fypc += 1;  //费用批次加1
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
                //写入
                string insertSql = string.Format(@"INSERT INTO GZSI_HIS_MZJS
	(
	JYDJH
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
    ,JSID
	)
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
	)", reg.SIMainInfo.RegNo,
                reg.SIMainInfo.FeeTimes,
                reg.SIMainInfo.HosNo,
                reg.IDCard,
                reg.PID.CardNO,
                reg.SeeDoct.OperTime,
                reg.SIMainInfo.BalanceDate,
                reg.SIMainInfo.TotCost,
                reg.SIMainInfo.PubCost,
                reg.SIMainInfo.PayCost,
                reg.SIMainInfo.ItemYLCost,
                reg.SIMainInfo.BaseCost,
                reg.SIMainInfo.ItemPayCost,
                reg.SIMainInfo.PubOwnCost,
                reg.SIMainInfo.OwnCost,
                reg.SIMainInfo.OverTakeOwnCost,
                reg.SIMainInfo.Memo,
                reg.SIMainInfo.HosCost,
                reg.SIMainInfo.User01,
                reg.SIMainInfo.User02,
                reg.SIMainInfo.User03,
                reg.SIMainInfo.ReadFlag,
                reg.ID,
                reg.PID.CardNO,
                reg.SIMainInfo.InvoiceNo,
                reg.SIMainInfo.OperInfo.ID,
                jsid
                );
                return this.ExecNoQuery(insertSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }

        public int SaveBlanceSIInPatient(FS.HISFC.Models.RADT.PatientInfo reg, ref string jsid)
        {
            //todo:存储结算结果同时需要调用存储过程生成发票明细和发票信息
            //string sql1 = string.Format(@"select fypc from GZSI_HIS_FYJS where jydjh='{0}' and invoice_no='{1}' and valid_flag='1'", reg.SIMainInfo.RegNo, reg.SIMainInfo.InvoiceNo);
            //string fypc = string.Empty;
            //this.ExecQuery(sql1);
            try
            {
                //while (Reader.Read())
                //{
                //    fypc = Reader[0].ToString();
                //}
                //Reader.Close();
                //if (string.IsNullOrEmpty(fypc))
                //{}

                string getIDSql = "select  SEQ_GZSI_HIS_FYJS.Nextval from dual";
                this.ExecQuery(getIDSql);
                if (Reader.Read())
                {
                    jsid = Reader[0].ToString();
                }
                Reader.Close();
                if (string.IsNullOrEmpty(jsid))
                {
                    this.Err = "获取主键ID(SEQ_GZSI_HIS_FYJS)失败";
                    return -1;
                }
                //写入
                string insertSql = string.Format(@"INSERT INTO GZSI_HIS_FYJS
	(
    inpatient_no
    ,invoice_no
	,JYDJH
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
	, OPER_CODE
	, OPER_DATE
    ,VALID_FLAG
    ,JSID
	)
VALUES
	(   
	'{0}'                                       --住院流水号
	,'{1}'                                      --结算发票号
	,'{2}'                                      --就医登记号
	,'{3}'                                      --费用批次
	,'{4}'                                      --医院编号
	,'{5}'                                      --身份证号
	,'{6}'                                      --门诊号/住院号
	,to_date('{7}','yyyy-mm-dd hh24:mi:ss')     --入院日期
	,to_date('{8}','yyyy-mm-dd hh24:mi:ss')     --结算日期
	,'{9}'                                      --总金额
	,'{10}'                                     --社保支付金额
	,'{11}'                                     --账户支付金额
	,'{12}'                                     --部分项目自付金额
	,'{13}'                                     --个人起付金额
	,'{14}'                                     --个人自费项目金额
	,'{15}'                                     --个人自付金额
	,'{16}'                                     --个人自负金额
	,'{17}'                                     --超统筹支付限额个人自付金额
	,'{18}'                                     --自费原因
	,'{19}'                                     --医药机构分单金额
	,'{20}'                                     --备注1,记录产生时间
	,'{21}'                                     --备注2
	,'{22}'                                     --备注3
	,'{23}'                                     --读入标志
	,'{24}'                                     --操作员	
	,sysdate                                    --操作时间
    ,'1'                                        --有效标记
    ,'{25}' 
	)", reg.ID,
                reg.SIMainInfo.InvoiceNo,
                reg.SIMainInfo.RegNo,
                reg.SIMainInfo.FeeTimes,
                reg.SIMainInfo.HosNo,
                reg.IDCard,
                reg.PID.CardNO,
                reg.SIMainInfo.OperDate,
                reg.SIMainInfo.BalanceDate,
                reg.SIMainInfo.TotCost,
                reg.SIMainInfo.PubCost,
                reg.SIMainInfo.PayCost,
                reg.SIMainInfo.ItemYLCost,
                reg.SIMainInfo.BaseCost,
                reg.SIMainInfo.ItemPayCost,
                reg.SIMainInfo.PubOwnCost,
                reg.SIMainInfo.OwnCost,
                reg.SIMainInfo.OverTakeOwnCost,
                reg.SIMainInfo.Memo,
                reg.SIMainInfo.HosCost,
                reg.SIMainInfo.User01,
                reg.SIMainInfo.User02,
                reg.SIMainInfo.User03,
                reg.SIMainInfo.ReadFlag,
                reg.SIMainInfo.OperInfo.ID,
                jsid
                );
                return this.ExecNoQuery(insertSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 判断门诊是否已经结算
        /// </summary>
        /// <param name="jydjh"></param>
        /// <param name="invoiceNo"></param>
        /// <returns>未结算 false，已结算 true</returns>
        public bool IsBalanceOutPatient(string jydjh, string invoiceNo)
        {
            string sql1 = string.Format(@"select fypc from GZSI_HIS_MZJS where jydjh='{0}' and invoice_no='{1}' and valid_flag='1'", jydjh, invoiceNo);
            this.ExecQuery(sql1);
            string fypc = string.Empty;
            try
            {
                while (Reader.Read())
                {
                    fypc = Reader[0].ToString();
                }
                if (string.IsNullOrEmpty(fypc))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Reader.Close();
            }
        }

        /// <summary>
        /// 更新医保结算信息为无效
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public int UpdateOutPatientSiBalanceDisable(string invoiceNo, string clinicCode)
        {
            if (string.IsNullOrEmpty(clinicCode) || string.IsNullOrEmpty(invoiceNo))
            {
                this.Err = "UpdateOutPatientSiBalanceDisable方法参数错误。";
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
        public int UpdateOutPatientSiBalanceDisable(string jsid)
        {
            if (string.IsNullOrEmpty(jsid))
            {
                this.Err = "UpdateOutPatientSiBalanceDisable方法参数错误。";
                return -1;
            }
            string strSql = string.Format("update GZSI_HIS_MZJS t set t.valid_flag='0' where t.jsid='{0}' and t.valid_flag='1'", jsid);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新医保结算信息为无效
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public int UpdateInPatientSiBalanceDisable(string invoiceNo, string inPatientNO)
        {
            if (string.IsNullOrEmpty(inPatientNO) || string.IsNullOrEmpty(invoiceNo))
            {
                this.Err = "UpdateOutPatientSiBalanceDisable方法参数错误。";
                return -1;
            }
            string strSql = string.Format("update GZSI_HIS_FYJS t set t.valid_flag='0' where t.inpatient_no='{0}' and t.invoice_no='{1}'", inPatientNO, invoiceNo);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新医保结算信息为无效
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        public int UpdateInPatientSiBalanceDisable(string jsid)
        {
            if (string.IsNullOrEmpty(jsid))
            {
                this.Err = "UpdateOutPatientSiBalanceDisable方法参数错误。";
                return -1;
            }
            string strSql = string.Format("update GZSI_HIS_FYJS t set t.valid_flag='0' where t.jsid='{0}' and t.valid_flag='1'", jsid);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 通过住院就医登记号获取医保结算信息，获取可用于构成fee实体的datatable，用于医保结算清单
        /// {DB2F2D38-B825-436d-B4C2-6BBC8BB720DB}
        /// </summary>
        /// <param name="jydjh"></param>
        /// <returns></returns>
        public System.Data.DataTable GetInpatientSiBalanceListAsFee(string jydjh)
        {
            string sqlfeelist = @"select fii.inpatient_no,
                                    fii.name,
                                    ghc.jydjh,
                                    ghc.xmmc,
                                    ghc.ypgg ,
                                    ghc.jg,
                                    ghc.mcyl,
                                    ghc.je,
                                    ghf.invoice_no,
                                    ghf.zyzje,
                                    ghf.sbzfje,
                                    ghf.grzfje3 grzfje1
                                    from fin_ipr_inmaininfo fii
                                    left join gzsi_his_cfxm ghc on fii.inpatient_no = ghc.inpatient_no
                                    left join gzsi_his_fyjs ghf on ghc.jydjh = ghf.jydjh
                                    left join fin_ipr_siinmaininfo fis on ghc.jydjh=fis.reg_no
                                    where ghc.jydjh ='{0}'
                                    and fis.valid_flag='1'
                                    and ghf.valid_flag='1'
                                    ";

            sqlfeelist = @"
             select t1.inpatient_no,
                       t1.name,
                       t3.jydjh,
                       t3.xmmc,
                       t3.ypgg ,
                       t3.jg,
                       t3.mcyl,
                       t3.je,
                       t1.invoice_no,
                       t1.medfee_sumamt zyzje,
                       t1.fund_pay_sumamt sbzfje,
                       nvl(t1.medfee_sumamt,0) - nvl(t1.fund_pay_sumamt,0) grzfje1,
                       (select a.name from com_dictionary a where a.type = 'GZSI_med_chrgitm_type' and a.code = t2.med_chrgitm_type) chrgitm_type
                  from fin_ipr_siinmaininfo t1, gzsi_feedetail t2, gzsi_his_cfxm t3
                 where t1.mdtrt_id = t2.mdtrt_id
                   and t1.setl_id = t2.setl_id
                   and t2.mdtrt_id = t3.jydjh
                   and t2.feedetl_sn = t3.xmxh
                   and t1.valid_flag = '1'
                   and t1.balance_state = '1'
                   and t2.memo is null    
                   and t1.mdtrt_id = '{0}'";


            sqlfeelist = string.Format(sqlfeelist, jydjh);

            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sqlfeelist, ref ds);

            System.Data.DataTable dt = ds.Tables[0];

            return dt;
        }

        /// <summary>
        /// 通过门诊就医登记号获取医保结算信息，获取可用于构成fee实体的datatable，用于医保结算清单
        /// {DB2F2D38-B825-436d-B4C2-6BBC8BB720DB}
        /// </summary>
        /// <param name="jydjh"></param>
        /// <returns></returns>
        public System.Data.DataTable GetOutpatientSiBalanceListAsFee(string jydjh)
        {
            string sqlfeelist = @"select cp.card_no,
                                    cp.name,
                                    ghc.jydjh,
                                    ghc.xmmc,
                                    ghc.ypgg ,
                                    ghc.jg,
                                    ghc.mcyl,
                                    ghc.je,
                                    ghf.invoice_no,
                                    ghf.zyzje,
                                    ghf.sbzfje,
                                    ghf.grzfje3 grzfje1
                                    from com_patientinfo cp
                                    left join GZSI_HIS_MZXM ghc on cp.card_no = ghc.card_no
                                    left join GZSI_HIS_MZJS ghf on ghc.jydjh = ghf.jydjh
                                    left join fin_ipr_siinmaininfo fis on ghc.jydjh=fis.reg_no
                                    where ghc.jydjh ='{0}'
                                    and fis.valid_flag='1'
                                    --and ghf.valid_flag='1'
                                    ";

            sqlfeelist = string.Format(sqlfeelist, jydjh);

            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sqlfeelist, ref ds);

            System.Data.DataTable dt = ds.Tables[0];

            return dt;
        }

        /// <summary>
        /// 通过门诊号获取零星医保结算信息，获取可用于构成fee实体的datatable，{B5C35BD4-A8AD-43e0-A1F5-062F1F87FC7D}
        /// {130C7CAA-7CB1-49b9-84DC-BE0775140BF3}
        /// </summary>
        /// <param name="clincode"></param>
        /// <returns></returns>
        public System.Data.DataTable GetOutpatientBalanceListAsFee(string cardno,string begin,string  end,string deptid)
        {
            string sqlfeelist = @" select  * from 
                                 (--药品
                                 select 
                                 a.dept_name 挂号科室,
                                 to_char(b.fee_date,'yyyy-MM-dd') 费用时间,
                                 c.name 费用类别,
                                 b.item_name 名称,
                                 b.specs  规格 ,
                                 --b.price_unit 单位,
                                 e.min_unit 单位,
                                 b.unit_price 本地价,
                                 b.qty         数量,
                                 e.retail_price 医保价  ,
                                 --b.qty*e.retail_price 金额
                                 b.own_cost 金额
                                 from fin_opr_register a 
                                 inner join   fin_opb_feedetail b on a.clinic_code=b.clinic_code
                                 --inner join  (select * from com_dictionary where type = 'MINFEE') c  on c.code = b.fee_code
                                 inner join   (select g.fee_stat_name name,g.fee_code 
                                 from fin_com_feecodestat g
                                 where g.report_code = 'ZY01') c on  c.fee_code = b.fee_code
                                 inner  join pha_com_baseinfo e on b.item_code=e.drug_code
                                 left  join fin_com_undruginfo f on b.item_code=f.item_code 
                                 where  f.item_name is null  and  b.trans_type = 1  and b.cancel_flag=1 and a.dept_code='{3}'  and a.card_no='{0}' 
                                 and b.fee_date between to_date('{1}','yyyy-MM-dd hh24:mi:ss')  and to_date('{2}','yyyy-MM-dd hh24:mi:ss') 
                                 union --耗材
                                 select
                                  a.dept_name 挂号科室,
                                  to_char(b.fee_date,'yyyy-MM-dd')  费用时间,
                                 c.name 费用类别,
                                 b.item_name 名称,
                                 b.specs  规格,
                                 b.price_unit 单位,
                                 b.unit_price 本地价,
                                 b.qty         数量,
                                 f.unit_price2  医保价  ,
                                 b.qty*f.unit_price2  金额  from fin_opr_register a 
                                 inner join   fin_opb_feedetail b on a.clinic_code=b.clinic_code
                                  --inner join  (select * from com_dictionary where type = 'MINFEE') c  on c.code = b.fee_code
                                 inner join   (select g.fee_stat_name name,g.fee_code 
                                 from fin_com_feecodestat g
                                 where g.report_code = 'ZY01') c on  c.fee_code = b.fee_code
                                  inner  join fin_com_undruginfo f on  b.item_code=f.item_code
                                  where b.trans_type = 1  and b.cancel_flag=1  and a.card_no='{0}'   and a.dept_code='{3}'  
                                  and  b.fee_date between to_date('{1}','yyyy-MM-dd hh24:mi:ss')  and to_date('{2}','yyyy-MM-dd hh24:mi:ss') 
                                  ) a order by 挂号科室, 费用时间,费用类别,名称,费用时间";

            sqlfeelist = string.Format(sqlfeelist, cardno,begin,end,deptid);

            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sqlfeelist, ref ds);

            System.Data.DataTable dt = ds.Tables[0];

            return dt;
        }

        /// <summary>
        /// 通过医保就医登记号获取住院号
        /// </summary>
        /// <returns></returns>
        public string GetInpatientNoByJYDJH(string jydjh)
        {
            string sqlinpatient = "select inpatient_no from gzsi_his_fyjs where JYDJH = '{0}' and rownum=1";
            sqlinpatient = string.Format(sqlinpatient, jydjh);

            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sqlinpatient, ref ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            return ds.Tables[0].Rows[0]["INPATIENT_NO"].ToString();
        }

        /// <summary>
        /// 通过医保就医登记号获取HIS卡号
        /// {8AA24CF1-D42B-4978-9D60-7083330080E5}
        /// </summary>
        /// <returns></returns>
        public string GetCardNoByJYDJH(string jydjh)
        {
            //{7784630A-37D7-4eb9-95C5-8D2CD37AC2FB}
            string sqlinpatient = "select card_no from GZSI_HIS_MZXM where JYDJH = '{0}' and rownum=1";
            sqlinpatient = string.Format(sqlinpatient, jydjh);

            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery(sqlinpatient, ref ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            return ds.Tables[0].Rows[0]["CARD_NO"].ToString();
        }

        #endregion

    }
}
