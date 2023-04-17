using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace Neusoft.SOC.Local.HISWebService.PE
{
    /// <summary>
    /// Account 的摘要说明。
    /// </summary>
    public class PEChargeService_Db : Neusoft.FrameWork.Management.Database
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PEChargeService_Db()
        { }

        //public IBM.Data.DB2.DB2Command command;

        #region 私有方法
        /// <summary>
        /// 提取卡信息
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <returns></returns>
        private Neusoft.HISFC.Models.Account.AccountCard GetAccountCardInfo(string Sql)
        {
            Neusoft.HISFC.Models.Account.AccountCard accountCard = null;
            //try
            //{
            //    if (this.ExecQuery(Sql) == -1) return null;
            //    while (this.Reader.Read())
            //    {
            //        accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
            //        accountCard.CardNO = Reader[0].ToString();
            //        accountCard.MarkNO = Reader[1].ToString();
            //        accountCard.MarkType.ID = Reader[2].ToString();
            //        accountCard.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[3]);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Err = ex.Message;
            //    this.ErrCode = ex.Message;
            //    return null;
            //}
            return accountCard;
        }

        #endregion


        #region 体检收费接口
        /// <summary>
        /// 用于查询
        /// </summary>
        /// <returns></returns>
        public int _dsExcuteQuery(string strSQL,ref DataSet objDs)
        {
            objDs=new DataSet();
            return this.ExecQuery(strSQL, ref objDs);
        }
        /// <summary>
        /// 用于执行sql
        /// </summary>
        /// <returns></returns>
        public int _ExcuteSQL(string strSQL)
        {       
            return this.ExecNoQuery(strSQL);
        }
        #region 插入登记信息
        /// <summary>
		/// 插入挂号记录表
		/// </summary>
		/// <param name="register"></param>
		/// <returns></returns>
		public int InsertPERegister(Neusoft.HISFC.Models.Registration.Register register)
		{
			string sql="";

			if(this.Sql.GetSql("Registration.Register.Insert.PeCharge",ref sql)==-1)return -1;
			
			try
			{
				sql = string.Format(sql,register.ID,    register.PID.CardNO,
                    register.DoctorInfo.SeeDate.ToString(),     register.DoctorInfo.Templet.Noon.ID,
					register.Name,  register.IDCard,  register.Sex.ID,  register.Birthday.ToString(),
					register.Pact.PayKind.ID,register.Pact.PayKind.Name,register.Pact.ID,register.Pact.Name,
					register.SSN,  register.DoctorInfo.Templet.RegLevel.ID,     register.DoctorInfo.Templet.RegLevel.Name,
                    register.DoctorInfo.Templet.Dept.ID,    register.DoctorInfo.Templet.Dept.Name,
                    register.DoctorInfo.SeeNO,  register.DoctorInfo.Templet.Doct.ID,
                    register.DoctorInfo.Templet.Doct.Name,	Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsFee),
                    (int)register.RegType,      Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsFirst),
					register.RegLvlFee.RegFee.ToString(),   register.RegLvlFee.ChkFee.ToString(),
                    register.RegLvlFee.OwnDigFee.ToString(),    register.RegLvlFee.OthFee.ToString(),
                    register.OwnCost.ToString(),    register.PubCost.ToString(),    register.PayCost.ToString(),
                    (int)register.Status,		register.InputOper.ID,  Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsSee),
					Neusoft.FrameWork.Function.NConvert.ToInt32(register.CheckOperStat.IsCheck),  register.PhoneHome,
					register.AddressHome,   (int)register.TranType,     register.CardType.ID,
                    register.DoctorInfo.Templet.Begin.ToString(),   register.DoctorInfo.Templet.End.ToString(),
					register.CancelOper.ID,     register.CancelOper.OperTime.ToString(),
                    register.InvoiceNO, register.RecipeNO,		Neusoft.FrameWork.Function.NConvert.ToInt32(register.DoctorInfo.Templet.IsAppend),
                    register.OrderNO,   register.DoctorInfo.Templet.ID,
					register.InputOper.OperTime.ToString(),     register.InSource.ID ,Neusoft.FrameWork.Function.NConvert.ToInt32(register.CaseState),
                    Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt),register.NormalName) ;

				return this.ExecNoQuery(sql);				
			}
			catch(Exception e)
			{
				this.Err="插入挂号主表类别表出错![Registration.Register.Insert.PeCharge]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}			
		}
        #endregion


        #endregion

        #region 患者数据操作

        #region 查询患者信息

        /// <summary>
        /// 查询患者信息(根据就诊卡号)
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Account.PatientAccount GetPatientInfo(string cardNO, string temp)
        {
            string Sql = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.Account.SelectPatient", ref Sql) == -1)
                return null;
            if (this.Sql.GetSql("Fee.Account.SelectPatientWhere10", ref SqlWhere) == -1)
                return null;
            Neusoft.HISFC.Models.Account.PatientAccount patientInfo = null;
            try
            {
                SqlWhere = string.Format(SqlWhere, cardNO);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1)
                    return null;
                #region SQL
                /*
                 SELECT a.card_no,   --就诊卡号
                   a.ic_cardno,   --电脑号
                   a.name,   --姓名
                   a.spell_code,   --拼音码
                   a.wb_code,   --五笔
                   a.birthday,   --出生日期
                   a.sex_code,   --性别
                   a.idenno,   --身份证号
                   a.blood_code,   --血型
                   a.prof_code,   --职业
                   a.work_home,   --工作单位
                   a.work_tel,   --单位电话
                   a.work_zip,   --单位邮编
                   a.home,   --户口或家庭所在
                   a.home_tel,   --家庭电话
                   a.home_zip,   --户口或家庭邮政编码
                   a.district,   --籍贯
                   a.nation_code,   --民族
                   a.linkman_name,   --联系人姓名
                   a.linkman_tel,   --联系人电话
                   a.linkman_add,   --联系人住址
                   a.rela_code,   --联系人关系
                   a.mari,   --婚姻状况
                   a.coun_code,   --国籍
                   a.paykind_code,   --结算类别
                   a.paykind_name,   --结算类别名称
                   a.pact_code,   --合同代码
                   a.pact_name,   --合同单位名称
                   a.mcard_no,   --医疗证号
                   a.area_code,   --出生地
                   a.framt,   --医疗费用
                   a.anaphy_flag,   --药物过敏
                   a.hepatitis_flag,   --重要疾病
                   a.act_code,   --帐户密码
                   a.act_amt,   --帐户总额
                   a.lact_sum,   --上期帐户余额
                   a.lbank_sum,   --上期银行余额
                   a.arrear_times,   --欠费次数
                   a.arrear_sum,   --欠费金额
                   a.inhos_source,   --住院来源
                   a.lihos_date,   --最近住院日期
                   a.inhos_times,   --住院次数
                   a.louthos_date,   --最近出院日期
                   a.fir_see_date,   --初诊日期
                   a.lreg_date,   --最近挂号日期
                   a.disoby_cnt,   --违约次数
                   a.end_date,   --结束日期
                   a.mark,   --备注
                   a.oper_code,   --操作员
                   a.oper_date,   --操作日期
                   a.is_valid,   --是否有效0有效1无效2作废
                   a.fee_kind,   --算法类别  0 全部
                   a.old_cardno,    --旧卡号,新老数据切换用
              FROM com_patientinfo a,   --病人基本信息表
              where and a.card_no='{0}'
             
                 */
                #endregion
                while (this.Reader.Read())
                {
                    patientInfo = new Neusoft.HISFC.Models.Account.PatientAccount();
                    #region 患者信息
                    if (Reader[0] != DBNull.Value)
                        patientInfo.PID.CardNO = Reader[0].ToString();//病历卡号
                    if (Reader[1] != DBNull.Value)
                        patientInfo.Card.ICCard.ID = Reader[1].ToString();
                    if (Reader[2] != DBNull.Value)
                        patientInfo.Name = Reader[2].ToString(); //姓名
                    if (Reader[4] != DBNull.Value)
                        patientInfo.WBCode = Reader[4].ToString(); //打卡时间
                    if (Reader[6] != DBNull.Value)
                        patientInfo.Sex.ID = Reader[6].ToString();//性别
                    if (Reader[0] != DBNull.Value)
                        patientInfo.PID.CardNO = Reader[0].ToString();//就诊卡号
                    if (Reader[26] != DBNull.Value)
                        patientInfo.Pact.ID = Reader[26].ToString();//结算类别code
                    if (Reader[27] != DBNull.Value)
                        patientInfo.Pact.Name = Reader[27].ToString();//结算类别名称
                    if (Reader[29] != DBNull.Value)
                        patientInfo.AreaCode = Reader[29].ToString();//出生地
                    if (Reader[23] != DBNull.Value)
                        patientInfo.Country.ID = Reader[23].ToString();//国籍
                    if (Reader[17] != DBNull.Value)
                        patientInfo.Nationality.ID = Reader[17].ToString();//民族
                    patientInfo.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);//出生日期
                    if (Reader[16] != DBNull.Value)
                        patientInfo.DIST = Reader[16].ToString();//籍贯
                    if (Reader[9] != DBNull.Value)
                        patientInfo.Profession.ID = Reader[9].ToString();//职业
                    if (Reader[7] != DBNull.Value)
                        patientInfo.IDCard = Reader[7].ToString();//身份证号
                    if (Reader[10] != DBNull.Value)
                        patientInfo.CompanyName = Reader[10].ToString();//工作单位
                    if (Reader[11] != DBNull.Value)
                        patientInfo.PhoneBusiness = Reader[11].ToString();//单位电话
                    if (Reader[22] != DBNull.Value)
                        patientInfo.MaritalStatus.ID = Reader[22].ToString();//婚姻状况
                    if (Reader[13] != DBNull.Value)
                        patientInfo.AddressHome = Reader[13].ToString();//家庭地址
                    if (Reader[14] != DBNull.Value)
                        patientInfo.PhoneHome = Reader[14].ToString();//家庭电话
                    if (Reader[18] != DBNull.Value)
                        patientInfo.Kin.Name = Reader[18].ToString();//联系人姓名
                    if (Reader[19] != DBNull.Value)
                        patientInfo.Kin.RelationPhone = Reader[19].ToString();//联系人电话
                    if (Reader[20] != DBNull.Value)
                        patientInfo.Kin.RelationAddress = Reader[20].ToString();//联系人地址
                    if (Reader[21] != DBNull.Value)
                        patientInfo.Kin.Relation.ID = Reader[21].ToString();//联系人关系
                    patientInfo.IsEncrypt = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[53]);
                    if (Reader[54] != DBNull.Value)
                        patientInfo.NormalName = Reader[54].ToString();
                    if (patientInfo.IsEncrypt)
                    {
                        //patientInfo.DecryptName = Neusoft.FrameWork.Interface.Classes.Function.Decrypt3DES(patientInfo.NormalName);
                    }
                    else
                    {
                        patientInfo.DecryptName = patientInfo.Name;
                    }
                    if (Reader[55] != DBNull.Value)
                        patientInfo.IDCardType.ID = Reader[55].ToString();
                    if (Reader[28] != DBNull.Value)
                        patientInfo.SSN = Reader[28].ToString();
                    if (Reader[15] != DBNull.Value)
                        patientInfo.HomeZip = Reader[15].ToString();
                    #endregion
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return patientInfo;
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Models.Account.PatientAccount GetPatientInfo(string cardNO)
        {
            string Sql = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.Account.SelectPatient", ref Sql) == -1) return null;
            if (this.Sql.GetSql("Fee.Account.SelectPatientWhere1", ref SqlWhere) == -1) return null;
            Neusoft.HISFC.Models.Account.PatientAccount patientInfo = null;
            try
            {
                SqlWhere = string.Format(SqlWhere, cardNO);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1) return null;
                #region SQL
                /*
                 SELECT a.card_no,   --就诊卡号
                   a.ic_cardno,   --电脑号
                   a.name,   --姓名
                   a.spell_code,   --拼音码
                   a.wb_code,   --五笔
                   a.birthday,   --出生日期
                   a.sex_code,   --性别
                   a.idenno,   --身份证号
                   a.blood_code,   --血型
                   a.prof_code,   --职业
                   a.work_home,   --工作单位
                   a.work_tel,   --单位电话
                   a.work_zip,   --单位邮编
                   a.home,   --户口或家庭所在
                   a.home_tel,   --家庭电话
                   a.home_zip,   --户口或家庭邮政编码
                   a.district,   --籍贯
                   a.nation_code,   --民族
                   a.linkman_name,   --联系人姓名
                   a.linkman_tel,   --联系人电话
                   a.linkman_add,   --联系人住址
                   a.rela_code,   --联系人关系
                   a.mari,   --婚姻状况
                   a.coun_code,   --国籍
                   a.paykind_code,   --结算类别
                   a.paykind_name,   --结算类别名称
                   a.pact_code,   --合同代码
                   a.pact_name,   --合同单位名称
                   a.mcard_no,   --医疗证号
                   a.area_code,   --出生地
                   a.framt,   --医疗费用
                   a.anaphy_flag,   --药物过敏
                   a.hepatitis_flag,   --重要疾病
                   a.act_code,   --帐户密码
                   a.act_amt,   --帐户总额
                   a.lact_sum,   --上期帐户余额
                   a.lbank_sum,   --上期银行余额
                   a.arrear_times,   --欠费次数
                   a.arrear_sum,   --欠费金额
                   a.inhos_source,   --住院来源
                   a.lihos_date,   --最近住院日期
                   a.inhos_times,   --住院次数
                   a.louthos_date,   --最近出院日期
                   a.fir_see_date,   --初诊日期
                   a.lreg_date,   --最近挂号日期
                   a.disoby_cnt,   --违约次数
                   a.end_date,   --结束日期
                   a.mark,   --备注
                   a.oper_code,   --操作员
                   a.oper_date,   --操作日期
                   a.is_valid,   --是否有效0有效1无效2作废
                   a.fee_kind,   --算法类别  0 全部
                   a.old_cardno,    --旧卡号,新老数据切换用
              FROM com_patientinfo a,   --病人基本信息表
              where and a.card_no='{0}'
             
                 */
                #endregion
                while (this.Reader.Read())
                {
                    patientInfo = new Neusoft.HISFC.Models.Account.PatientAccount();
                    #region 患者信息
                    if (Reader[0] != DBNull.Value) patientInfo.PID.CardNO = Reader[0].ToString();//病历卡号
                    if (Reader[1] != DBNull.Value)
                        patientInfo.Card.ICCard.ID = Reader[1].ToString();
                    if (Reader[2] != DBNull.Value) patientInfo.Name = Reader[2].ToString(); //姓名
                if (Reader[4] != DBNull.Value)
                    patientInfo.WBCode = Reader[4].ToString(); //打卡时间
                    if (Reader[6] != DBNull.Value) patientInfo.Sex.ID = Reader[6].ToString();//性别
                    if (Reader[0] != DBNull.Value) patientInfo.PID.CardNO = Reader[0].ToString();//就诊卡号
                    if (Reader[26] != DBNull.Value) patientInfo.Pact.ID = Reader[26].ToString();//结算类别code
                    if (Reader[27] != DBNull.Value) patientInfo.Pact.Name = Reader[27].ToString();//结算类别名称
                    if (Reader[29] != DBNull.Value) patientInfo.AreaCode = Reader[29].ToString();//出生地
                    if (Reader[23] != DBNull.Value) patientInfo.Country.ID = Reader[23].ToString();//国籍
                    if (Reader[17] != DBNull.Value) patientInfo.Nationality.ID = Reader[17].ToString();//民族
                    patientInfo.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);//出生日期
                    if (Reader[16] != DBNull.Value) patientInfo.DIST = Reader[16].ToString();//籍贯
                    if (Reader[9] != DBNull.Value) patientInfo.Profession.ID = Reader[9].ToString();//职业
                    if (Reader[7] != DBNull.Value) patientInfo.IDCard = Reader[7].ToString();//身份证号
                    if (Reader[10] != DBNull.Value) patientInfo.CompanyName = Reader[10].ToString();//工作单位
                    if (Reader[11] != DBNull.Value) patientInfo.PhoneBusiness = Reader[11].ToString();//单位电话
                    if (Reader[22] != DBNull.Value) patientInfo.MaritalStatus.ID = Reader[22].ToString();//婚姻状况
                    if (Reader[13] != DBNull.Value) patientInfo.AddressHome = Reader[13].ToString();//家庭地址
                    if (Reader[14] != DBNull.Value) patientInfo.PhoneHome = Reader[14].ToString();//家庭电话
                    if (Reader[18] != DBNull.Value) patientInfo.Kin.Name = Reader[18].ToString();//联系人姓名
                    if (Reader[19] != DBNull.Value) patientInfo.Kin.RelationPhone = Reader[19].ToString();//联系人电话
                    if (Reader[20] != DBNull.Value) patientInfo.Kin.RelationAddress = Reader[20].ToString();//联系人地址
                    if (Reader[21] != DBNull.Value) patientInfo.Kin.Relation.ID = Reader[21].ToString();//联系人关系
                    patientInfo.IsEncrypt = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[53]);
                    if (Reader[54] != DBNull.Value) patientInfo.NormalName = Reader[54].ToString();
                    if (patientInfo.IsEncrypt)
                    {
                        //patientInfo.DecryptName = Neusoft.Models.Interface.Classes.Function.Decrypt3DES(patientInfo.NormalName);
                    }
                    else
                    {
                        patientInfo.DecryptName = patientInfo.Name;
                    }
                    if (Reader[55] != DBNull.Value) patientInfo.IDCardType.ID=Reader[55].ToString();
                    if (Reader[28] != DBNull.Value)
                        patientInfo.SSN = Reader[28].ToString();
                    if (Reader[15] != DBNull.Value)
                        patientInfo.HomeZip = Reader[15].ToString();
                    #endregion
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return patientInfo;
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="idenNO">身份证号</param>
        /// <returns></returns>
        public List<Neusoft.HISFC.Models.Account.PatientAccount> GetPatientInfo(string name, string idenNO, string birthday, string homestr)
        {
            string Sql = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.Account.SelectPatient", ref Sql) == -1) return null;
            if (this.Sql.GetSql("Fee.Account.SelectPatientWhere2", ref SqlWhere) == -1) return null;
            Neusoft.HISFC.Models.Account.PatientAccount patientInfo = null;
            List<Neusoft.HISFC.Models.Account.PatientAccount> list = new List<Neusoft.HISFC.Models.Account.PatientAccount>();
            try
            {
                SqlWhere = string.Format(SqlWhere, name,birthday, idenNO,homestr);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1) return null;
                while (this.Reader.Read())
                {
                    patientInfo = new Neusoft.HISFC.Models.Account.PatientAccount();
                    #region 患者信息
                    if (Reader[0] != DBNull.Value) patientInfo.PID.CardNO = Reader[0].ToString();//病历卡号
                    if (Reader[1] != DBNull.Value)
                        patientInfo.Card.ICCard.ID = Reader[1].ToString();
                    if (Reader[2] != DBNull.Value) patientInfo.Name = Reader[2].ToString(); //姓名
                if (Reader[4] != DBNull.Value)
                    patientInfo.WBCode = Reader[4].ToString(); //打卡时间
                    if (Reader[6] != DBNull.Value) patientInfo.Sex.ID = Reader[6].ToString();//性别
                    if (Reader[0] != DBNull.Value) patientInfo.PID.CardNO = Reader[0].ToString();//就诊卡号
                    if (Reader[26] != DBNull.Value) patientInfo.Pact.ID = Reader[26].ToString();//结算类别code
                    if (Reader[27] != DBNull.Value) patientInfo.Pact.Name = Reader[27].ToString();//结算类别名称
                    if (Reader[29] != DBNull.Value) patientInfo.AreaCode = Reader[29].ToString();//出生地
                    if (Reader[23] != DBNull.Value) patientInfo.Country.ID = Reader[23].ToString();//国籍
                    if (Reader[17] != DBNull.Value) patientInfo.Nationality.ID = Reader[17].ToString();//民族
                    patientInfo.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);//出生日期
                    if (Reader[16] != DBNull.Value) patientInfo.DIST = Reader[16].ToString();//籍贯
                    if (Reader[9] != DBNull.Value) patientInfo.Profession.ID = Reader[9].ToString();//职业
                    if (Reader[7] != DBNull.Value) patientInfo.IDCard = Reader[7].ToString();//身份证号
                    if (Reader[10] != DBNull.Value) patientInfo.CompanyName = Reader[10].ToString();//工作单位
                    if (Reader[11] != DBNull.Value) patientInfo.PhoneBusiness = Reader[11].ToString();//单位电话
                    if (Reader[22] != DBNull.Value) patientInfo.MaritalStatus.ID = Reader[22].ToString();//婚姻状况
                    if (Reader[13] != DBNull.Value) patientInfo.AddressHome = Reader[13].ToString();//家庭地址
                    if (Reader[14] != DBNull.Value) patientInfo.PhoneHome = Reader[14].ToString();//家庭电话
                    if (Reader[18] != DBNull.Value) patientInfo.Kin.Name = Reader[18].ToString();//联系人姓名
                    if (Reader[19] != DBNull.Value) patientInfo.Kin.RelationPhone = Reader[19].ToString();//联系人电话
                    if (Reader[20] != DBNull.Value) patientInfo.Kin.RelationAddress = Reader[20].ToString();//联系人地址
                    if (Reader[21] != DBNull.Value) patientInfo.Kin.Relation.ID = Reader[21].ToString();//联系人关系
                    patientInfo.IsEncrypt = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[53]);//是否加密姓名
                    patientInfo.NormalName = Reader[54].ToString();//密文
                    if (patientInfo.IsEncrypt)
                    {
                        //patientInfo.DecryptName = Neusoft.FrameWork.Interface.Classes.Function.Decrypt3DES(patientInfo.NormalName);
                    }
                    else
                    {
                        patientInfo.DecryptName = patientInfo.Name;
                    }
                    if (this.Reader[55] != DBNull.Value) patientInfo.IDCardType.ID = this.Reader[55].ToString();
                    if (Reader[28] != DBNull.Value)
                        patientInfo.SSN = Reader[28].ToString();
                    if (Reader[15] != DBNull.Value)
                        patientInfo.HomeZip = Reader[15].ToString();
                    #endregion
                    list.Add(patientInfo);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return list;
        }
        #endregion

        #region 更新患者信息
        /// <summary>
        /// 更新患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        public int UpdatePatient(Neusoft.HISFC.Models.Account.PatientAccount patientInfo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.UpdatePatient", ref Sql) == -1) return -1;
            try
            {
                #region Sql
                /*UPDATE com_patientinfo   --病人基本信息表
                       SET name='{1}',   --姓名
                           birthday={2},   --出生日期
                           sex_code='{3}',   --性别
                           idenno='{4}',   --身份证号
                           prof_code='{5}',   --职业
                           work_home='{6}',   --工作单位
                           work_tel='{7}',   --单位电话
                           home='{8}',   --户口或家庭所在
                           home_tel='{9}',   --家庭电话
                           district='{10}',   --籍贯
                           nation_code='{11}',   --民族
                           linkman_name='{12}',   --联系人姓名
                           linkman_tel='{13}',   --联系人电话
                           linkman_add='{14}',   --联系人住址
                           rela_code='{15}',   --联系人关系
                           mari='{16}',   --婚姻状况
                           coun_code='{17}',   --国籍
                           pact_code='{18}',   --合同代码
                           pact_name='{19}',   --合同单位名称
                           area_code='{20}'    --出生地
                     WHERE card_no='{0}'
                  */
                #endregion

                #region 格式化SQL
                Sql = string.Format(Sql,
                                   patientInfo.PID.CardNO, //门诊卡号
                                   patientInfo.Name,//姓名
                                   //patientInfo.Birthday.ToShortDateString().ToString(),//出生日期
                                   patientInfo.Birthday.ToString(),//出生日期
                                   patientInfo.Sex.ID,//性别
                                   patientInfo.IDCard,//身份证号
                                   patientInfo.Profession.ID,//职业
                                   patientInfo.CompanyName,//工作单位
                                   patientInfo.PhoneBusiness,//单位电话
                                   patientInfo.AddressHome,//户口或家庭所在
                                   patientInfo.PhoneHome,//家庭电话
                                   patientInfo.DIST,//籍贯
                                   patientInfo.Nationality.ID,//民族
                                   patientInfo.Kin.Name,//联系人姓名
                                   patientInfo.Kin.RelationPhone,//联系人电话
                                   patientInfo.Kin.RelationAddress,//联系人住址
                                   patientInfo.Kin.Relation.ID,//联系人关系
                                   patientInfo.MaritalStatus.ID,//婚姻状况
                                   patientInfo.Country.ID,//国籍
                                   patientInfo.Pact.ID,//合同代码
                                   patientInfo.Pact.Name,//合同单位名称
                                   patientInfo.AreaCode,//出生地
                                   patientInfo.Oper.ID,//操作员
                                   patientInfo.Oper.OperTime, //操作时间
                                   Neusoft.FrameWork.Function.NConvert.ToInt32(patientInfo.IsEncrypt),//是否加密姓名
                                   patientInfo.NormalName, //密文
                                   patientInfo.IDCardType.ID //证件类型
                                   );
                #endregion
                
            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }
        /// <summary>
        /// 更新患者门诊、住院信息，写入改名记录
        /// </summary>
        /// <param name="patientInfo"></param>
        public int UpdatePatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientinfo, Neusoft.HISFC.Models.RADT.PatientInfo patientinfo_old)
        {
            //change_type代表修改类型，如果修改了名字，则修改类型为N，如果修改了性别，则修改类型为S~~~
            string change_type=string .Empty;
             if(patientinfo_old.Name!=patientinfo.Name)
                change_type+="N";
              if(patientinfo_old.Sex .ID!=patientinfo.Sex.ID)
                change_type+="S";
              if(patientinfo_old.Birthday!=patientinfo.Birthday)
                change_type+="B";

              Neusoft.HISFC.BizLogic.Manager.EmployeeRecord emp = new Neusoft.HISFC.BizLogic.Manager.EmployeeRecord();
             string empl_code = emp.Operator.ID;

            string Sql_ZSY = string.Empty;
            if (this.Sql.GetSql("Fee.Account.UpdateZSY", ref Sql_ZSY) == -1) return -1;

            string Sql_InmainInfo = string.Empty;
            if (this.Sql.GetSql("Fee.Account.UpdateInmainInfo", ref Sql_InmainInfo) == -1) return -1;
            
            string Sql_InsertChangeRecord = string.Empty;
            if (this.Sql.GetSql("Fee.Account.InsertModifyInfo", ref Sql_InsertChangeRecord) == -1) return -1;
           

            try
            {
                Sql_ZSY = string.Format(Sql_ZSY, patientinfo.Name, patientinfo.Sex.ID.ToString(), patientinfo.Birthday, patientinfo.PID.CardNO);
                Sql_InmainInfo = string.Format(Sql_InmainInfo, patientinfo.Name, patientinfo.Sex.ID.ToString(), patientinfo.Birthday, patientinfo.PID.CardNO);
                Sql_InsertChangeRecord = string.Format(Sql_InsertChangeRecord, patientinfo.Card.ICCard.ID, patientinfo.PID.CardNO,change_type, empl_code);
                int reuslt = 1;
                if (this.ExecNoQuery(Sql_ZSY) ==-1)
                    reuslt=-1;
                if (this.ExecNoQuery(Sql_InmainInfo) ==-1)
                    reuslt = -1;
                if (this.ExecNoQuery(Sql_InsertChangeRecord) == -1)
                    reuslt = -1;
                return reuslt;

            }
            catch(Exception e)
            {
                return -1;
            }
        }
        #endregion

        #region 插入患者信息
        /// <summary>
        /// 插入患者信息
        /// </summary>
        /// <param name="patientInfo">患者实体</param>
        /// <returns></returns>
        public int InsertPatient(Neusoft.HISFC.Models.Account.PatientAccount patientInfo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.InsertPatient", ref Sql) == -1) return -1;
            try
            {
                #region 格式化SQL
                Sql = string.Format(Sql,
                                   patientInfo.PID.CardNO, //门诊卡号
                                   patientInfo.Name,//姓名
                                   patientInfo.Birthday.ToShortDateString().ToString(),//出生日期
                                   patientInfo.Sex.ID,//性别
                                   patientInfo.IDCard,//身份证号
                                   patientInfo.Profession,//职业
                                   patientInfo.CompanyName,//工作单位
                                   patientInfo.PhoneBusiness,//单位电话
                                   patientInfo.AddressHome,//户口或家庭所在
                                   patientInfo.PhoneHome,//家庭电话
                                   patientInfo.DIST,//籍贯
                                   patientInfo.Nationality.ID,//民族
                                   patientInfo.Kin.Name,//联系人姓名
                                   patientInfo.Kin.RelationPhone,//联系人电话
                                   patientInfo.Kin.RelationAddress,//联系人住址
                                   patientInfo.Kin.Relation.ID,//联系人关系
                                   patientInfo.MaritalStatus.ID,//婚姻状况
                                   patientInfo.Country.ID,//国籍
                                   patientInfo.Pact.ID,//合同代码
                                   patientInfo.Pact.Name,//合同单位名称
                                   patientInfo.AreaCode,//出生地
                                   patientInfo.Oper.ID,//操作员
                                   patientInfo.Oper.OperTime, //操作时间
                                   Neusoft.FrameWork.Function.NConvert.ToInt32(patientInfo.IsEncrypt),//是否加密姓名
                                   patientInfo.NormalName, //密文
                                   patientInfo.IDCardType.ID
                                   );
                #endregion

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }
        #endregion

        #endregion

        #region 帐户卡操作

//        #region 根据物理卡号取卡信息
//        /// <summary>
//        /// 根据物理卡号取卡信息
//        /// </summary>
//        /// <param name="markNO">物理卡号</param>
//        /// <returns></returns>
//        public Neusoft.HISFC.Models.Account.AccountCard GetAccountCard(string markNO, string markType)
//        {
//            Neusoft.HISFC.Models.Account.AccountCard accountCard = null;
//            try
//            {
//                string Sql = string.Empty;
//                string SqlWhere = string.Empty;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCard", ref Sql) == -1) return null;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCardWhere3", ref SqlWhere) == -1) return null;
//                SqlWhere = string.Format(SqlWhere, markNO, markType);
//                Sql += " " + SqlWhere;
//                if (this.ExecQuery(Sql) == -1) return null;
//                while (this.Reader.Read())
//                {
//                    accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
//                    accountCard.CardNO = Reader[0].ToString();
//                    accountCard.MarkNO = Reader[1].ToString();
//                    accountCard.MarkType.ID = Reader[2].ToString();
//                    accountCard.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[3]);
//                }
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                this.ErrCode = ex.Message;
//                return null;
//            }
//            return accountCard;
//        }
//        #endregion

//        #region 插入卡操作记录
//        /// <summary>
//        /// 插入卡操作记录
//        /// </summary>
//        /// <param name="accountCardRecord">卡操作记录实体</param>
//        /// <returns></returns>
//        public int InsertAccountCardRecord(Neusoft.HISFC.Models.Account.AccountCardRecord accountCardRecord)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.InsetAccountCardRecord", ref Sql) == -1) return -1;
//            try
//            {
//                Sql = string.Format(Sql,
//                                accountCardRecord.MarkNO,
//                                accountCardRecord.MarkType.ID.ToString(),
//                                accountCardRecord.CardNO,
//                                accountCardRecord.OperateTypes.ID.ToString(),
//                                accountCardRecord.Oper.ID.ToString(),
//                                accountCardRecord.CardMoney);
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                return -1;
//            }
//            return this.ExecNoQuery(Sql);
//        }

//        #endregion

//        #region 根据患者门诊卡号查找卡信息
//        /// <summary>
//        /// 通过卡号查找卡信息
//        /// </summary>
//        /// <param name="cardNO"></param>
//        /// <returns></returns>
//        public Neusoft.HISFC.Models.Account.AccountCard GetMarkByCardNo(string cardNO, string markType)
//        {
//            Neusoft.HISFC.Models.Account.AccountCard accountCard = null;
//            try
//            {
//                string Sql = string.Empty;
//                string SqlWhere = string.Empty;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCard", ref Sql) == -1) return null;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCardWhere1", ref SqlWhere) == -1) return null;
//                SqlWhere = string.Format(SqlWhere, cardNO, markType);
//                Sql += " " + SqlWhere;
//                if (this.ExecQuery(Sql) == -1) return null;
//                while (this.Reader.Read())
//                {
//                    accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
//                    accountCard.CardNO = Reader[0].ToString();
//                    accountCard.MarkNO = Reader[1].ToString();
//                    accountCard.MarkType.ID = Reader[2].ToString();
//                    accountCard.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[3]);
//                }
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                this.ErrCode = ex.Message;
//                return null;
//            }
//            return accountCard;
//        }

//        /// <summary>
//        /// 通过卡号查找卡信息
//        /// </summary>
//        /// <param name="cardNO">门诊卡号</param>
//        /// <returns></returns>
//        public List<Neusoft.HISFC.Models.Account.AccountCard> GetMarkList(string cardNO)
//        {

//            List<Neusoft.HISFC.Models.Account.AccountCard> list = new List<Neusoft.HISFC.Models.Account.AccountCard>();
//            try
//            {
//                string Sql = string.Empty;
//                string SqlWhere = string.Empty;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCard", ref Sql) == -1) return null;
//                if (this.Sql.GetSql("Fee.Account.SelectAccountCardWhere2", ref SqlWhere) == -1) return null;
//                SqlWhere = string.Format(SqlWhere, cardNO);
//                Sql += " " + SqlWhere;
//                if (this.ExecQuery(Sql) == -1) return null;
//                Neusoft.HISFC.Models.Account.AccountCard accountCard = null;

//                while (this.Reader.Read())
//                {
//                    accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
//                    accountCard.CardNO = Reader[0].ToString();
//                    accountCard.MarkNO = Reader[1].ToString();
//                    accountCard.MarkType.ID = Reader[2].ToString();
//                    accountCard.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[3]);
//                    list.Add(accountCard);
//                }
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                this.ErrCode = ex.Message;
//                return null;
//            }
//            return list
//;

//        }
//        #endregion

//        #region 插入门诊帐户卡数据
//        /// <summary>
//        /// 插入门诊帐户卡数据
//        /// </summary>
//        /// <param name="accountCard"></param>
//        /// <returns></returns>
//        public int InsertAccountCard(Neusoft.HISFC.Models.Account.AccountCard accountCard)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.InsertAccountCard", ref Sql) == -1) return -1;
//            try
//            {
//                Sql = string.Format(Sql,
//                                    accountCard.CardNO, //门诊卡号
//                                    accountCard.MarkNO,//身份标识卡号
//                                    accountCard.MarkType.ID.ToString(),//身份标识卡类别 1磁卡 2IC卡 3保障卡
//                                    Neusoft.FrameWork.Function.NConvert.ToInt32(accountCard.IsValid).ToString() //状态'1'正常'0'停用 
//                                    );
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                this.ErrCode = ex.Message;
//                return -1;
//            }
//            return this.ExecNoQuery(Sql);
//        }
//        #endregion

//        #region 更新卡状态
//        /// <summary>
//        /// 更新卡状态
//        /// </summary>
//        /// <param name="markNO">物理卡号</param>
//        /// <param name="type">卡类型</param>
//        /// <param name="state">状态</param>
//        /// <returns></returns>
//        public int UpdateAccountCardState(string markNO, string type, string state)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.UpdateAccountCardState", ref Sql) == -1) return -1;
//            try
//            {
//                Sql = string.Format(Sql, markNO, state, type);
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                this.ErrCode = ex.Message;
//                return -1;
//            }
//            return this.ExecNoQuery(Sql);

//        }
//        #endregion

//        #region 查找卡使用记录
//        /// <summary>
//        /// 查找卡使用记录
//        /// </summary>
//        /// <param name="cardNO">门诊卡号</param>
//        /// <param name="begin">开始时间</param>
//        /// <param name="end">结束时间</param>
//        /// <returns></returns>
//        public List<Neusoft.HISFC.Models.Account.AccountCardRecord> GetAccountCardRecord(string cardNO, string begin, string end)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.SelectAccountCardRecord", ref Sql) == -1)
//            {
//                this.Err = "查找SQL语句失败！";
//                return null;
//            }
//            try
//            {
//                Sql = string.Format(Sql, cardNO, begin, end);
//                if (this.ExecQuery(Sql) == -1)
//                {
//                    this.Err = "查找卡使用数据失败！";
//                    return null;
//                }
//                List<Neusoft.HISFC.Models.Account.AccountCardRecord> list = new List<Neusoft.HISFC.Models.Account.AccountCardRecord>();
//                Neusoft.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;
//                while (this.Reader.Read())
//                {
//                    accountCardRecord = new Neusoft.HISFC.Models.Account.AccountCardRecord();
//                    accountCardRecord.MarkNO = Reader[0].ToString();
//                    accountCardRecord.MarkType.ID = Reader[1];
//                    accountCardRecord.CardNO = Reader[2].ToString();
//                    accountCardRecord.OperateTypes.ID = Reader[3];
//                    accountCardRecord.Oper.Name = Reader[4].ToString();
//                    accountCardRecord.Oper.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
//                    accountCardRecord.CardMoney = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[6]);
//                    list.Add(accountCardRecord);
//                }
//                return list;
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                return null;
//            }
//        }
//        #endregion

//        #region 删除卡数据
//        /// <summary>
//        /// 删除卡数据
//        /// </summary>
//        /// <param name="markNO">卡号</param>
//        /// <param name="markType">卡类型</param>
//        /// <returns></returns>
//        public int DeleteAccoutCard(string markNO, string markType)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.DeleteAccountCard", ref Sql) == -1) return -1;
//            try
//            {
//                Sql = string.Format(Sql, markNO, markType);
//            }
//            catch (Exception ex)
//            {
//                this.Err = ex.Message;
//                return -1;
//            }
//            return this.ExecNoQuery(Sql);
//        }

//        #endregion

//        #region 帐户换卡
//        /// <summary>
//        /// 帐户换卡
//        /// </summary>
//        /// <param name="newMark">新卡号</param>
//        /// <param name="oldMark">原</param>
//        /// <returns></returns>
//        public int UpdateAccountCardMark(string newMark, string oldMark)
//        {
//            string Sql = string.Empty;
//            if (this.Sql.GetSql("Fee.Account.UpdateAccountCardMarkNo", ref Sql) == -1)
//            {
//                this.Err = "查找SQL语句失败！";
//                return -1;
//            }
//            try
//            {
//                Sql = string.Format(Sql, newMark, oldMark);
//            }
//            catch (Exception ex)
//            {

//                this.Err = ex.Message;
//            }
//            return this.Sql.ExecNoQuery(Sql);
//        }

//        #endregion

//        #region 根据卡号规则读出卡号
//        /// <summary>
//        /// 根据卡号规则读出卡号
//        /// </summary>
//        /// <param name="markNo">输入的卡号</param>
//        /// <param name="validedMarkNo"></param>
//        /// <returns></returns>
//        public int ValidMarkNO(string markNo, ref string validedMarkNo)
//        {
//            string firstleter = markNo.Substring(0, 1);
//            string lastleter = markNo.Substring(markNo.Length - 1, 1);
//            if (firstleter != ";")
//            {
//                this.Err = "请输入正确的卡号！";
//                return -1;
//            }
//            if (lastleter != "?")
//            {
//                this.Err = "请输入正确的卡号！";
//                return -1;
//            }
//            validedMarkNo = markNo.Substring(1, markNo.Length - 2);
//            char[] charArray = validedMarkNo.ToCharArray();
//            foreach (char c in charArray)
//            {
//                if (!char.IsNumber(c))
//                {
//                    this.Err = "请输入正确的卡号！";
//                    return -1;
//                }
//            }
//            return 1;
//        }
//        #endregion

        #endregion

        #region 帐户交易数据操作

       // #region 帐户预交金
       ///// <summary>
       // /// 帐户预交金
       ///// </summary>
       ///// <param name="accountRecord">交易实体</param>
       ///// <returns></returns>
       // public bool AccountPrePay(Neusoft.HISFC.Models.Account.AccountRecord accountRecord)
       // {


       //     try
       //     {
       //         #region 插入交易记录
       //         if (this.InsertAccountRecord(accountRecord) < 0)
       //         {
       //             MessageBox.Show("交费失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }
       //         #endregion

       //         #region 更新帐户余额
       //         //在计算帐户余额时是余额-本次交易的钱，所以交费时的钱应该是负的
       //         decimal consumeMoney = -accountRecord.Money;
       //         if (this.UpdateAccountVacancy(accountRecord.CardNO, consumeMoney) < 0)
       //         {
       //             MessageBox.Show("交费失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }
       //         #endregion

       //         MessageBox.Show("交费 （" + accountRecord.Money.ToString() + "） 成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         return true;
       //     }
       //     catch
       //     {
       //         MessageBox.Show("交费失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         return false;
       //     }

       // }
       // #endregion

       // #region  通过物理卡号查找门诊卡号
       // /// <summary>
       // /// 通过物理卡号查找门诊卡号
       // /// </summary>
       // /// <param name="markNo">物理卡号</param>
       // /// <param name="markType">卡类型</param>
       // /// <param name="cardNo">门诊卡号</param>
       // /// <returns>bool true 成功　false 失败</returns>
       // public bool GetCardNoByMarkNo(string markNo, Neusoft.HISFC.Models.Account.MarkTypes markType, ref string cardNo)
       // {
       //     string Sql = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.SelectCardNoByMarkNo", ref Sql) == -1)
       //     {
       //         this.Err = "查找SQL语句失败！";
       //         return false;
       //     }
       //     try
       //     {
       //         Sql = string.Format(Sql, markNo, ((int)markType).ToString());
       //         if (this.ExecQuery(Sql) == -1)
       //         {
       //             this.Err = "查找数据失败！";
       //             return false;
       //         }
       //         #region Sql
       //         /*select b.card_no,
       //                    b.markno,
       //                    b.type,
       //                    b.state as cardstate,
       //                    a.state as accountstate,
       //                    a.vacancy 
       //             from fin_opb_account a,fin_opb_accountcard b 
       //             where a.card_no=b.card_no 
       //               and b.markno='{0}' 
       //               and type='{1}'*/
       //         #endregion
       //         Neusoft.HISFC.Models.Account.Account account = null;
       //         while (this.Reader.Read())
       //         {
       //             account = new Neusoft.HISFC.Models.Account.Account();
       //             account.AccountCard.CardNO = this.Reader[0].ToString();
       //             account.AccountCard.MarkNO = this.Reader[1].ToString();
       //             account.AccountCard.MarkType.ID = Neusoft.FrameWork.Function.NConvert.ToInt32(Reader[2]);
       //             account.AccountCard.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[3]);
       //         }
       //         if (account == null)
       //         {
       //             this.Err = "该卡" + markNo + "已被取消使用！";
       //             return false;
       //         }
       //         if (!account.AccountCard.IsValid)
       //         {
       //             this.Err = "该卡"+ markNo +"已被停止使用！";
       //             return false;
       //         }
       //         cardNo = account.AccountCard.CardNO;
       //         return true;
       //     }
       //     catch (Exception ex)
       //     {
       //         this.Err = "查找门诊卡号失败，" + ex.Message;
       //         return false;
       //     }

       // }

       // #endregion

       // #region 帐户支付
       // /// <summary>
       // /// 帐户支付
       // /// </summary>
       // /// <param name="cardNO">门诊卡号</param>
       // /// <param name="money">金额（支付正值）</param>
       // /// <param name="reMark">标识</param>
       // /// <param name="deptCode">科室编码</param>
       // /// <returns>True成功False失败</returns>
       // public bool AccountPay(string cardNO, decimal money, string reMark, string deptCode)
       // {
       //     try
       //     {
       //         #region 得到帐户余额
       //         decimal accountVacancy = 0;
       //         int result = this.GetVacancy(cardNO, ref accountVacancy);
       //         if (result <= 0)
       //         {
       //             MessageBox.Show(this.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
       //             return false;
       //         }
       //         #endregion

       //         #region 支付操作判断帐户余额是否够

       //         if (accountVacancy < money)
       //         {
       //             MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("帐户余额" + accountVacancy.ToString() + "不足" + money.ToString() + "！"), Neusoft.FrameWork.Management.Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
       //             return false;
       //         }

       //         #endregion

       //         #region 插入交易记录
       //         Neusoft.HISFC.Models.Account.AccountRecord accountRecord = new Neusoft.HISFC.Models.Account.AccountRecord();
       //         accountRecord.CardNO = cardNO; //病历号   
       //         accountRecord.OperType.ID = (int)Neusoft.HISFC.Models.Account.OperTypes.Pay;//操作类型
       //         accountRecord.Money = -money;//金额
       //         accountRecord.DeptCode = deptCode;//科室
       //         accountRecord.Oper = (this.Operator as Neusoft.HISFC.Models.Base.Employee).ID;//操作员
       //         accountRecord.OperTime = this.GetDateTimeFromSysDateTime();//操作时间
       //         accountRecord.ReMark = reMark;//发票号
       //         accountRecord.IsValid = true;//是否有效
       //         accountRecord.Vacancy = accountVacancy - money;//本次交易余额
       //         if (this.InsertAccountRecord(accountRecord) == -1)
       //         {
       //             MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("插入交易数据失败！"), Neusoft.FrameWork.Management.Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }
       //         #endregion

       //         #region 更新帐户余额
       //         if (this.UpdateAccountVacancy(cardNO, money) == -1)
       //         {
       //             MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("更新帐户余额失败！"), Neusoft.FrameWork.Management.Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }

       //         #endregion

       //         MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("支付费用" + money.ToString() + "成功！"), Neusoft.FrameWork.Management.Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         return true;
       //     }
       //     catch
       //     {
       //         MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("支付费用失败！"), Neusoft.FrameWork.Management.Language.Msg("错误"), MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         return false;
       //     }

       // }
       // #endregion

       // #region 退费入户
       // /// <summary>
       // /// 退费入户
       // /// </summary>
       // /// <param name="cardNO">门诊卡号</param>
       // /// <param name="money">金额(退费时负值)</param>
       // /// <param name="reMark">标识</param>
       // /// <param name="deptCode">科室编码</param>
       // /// <returns></returns>
       // public bool AccountCancelPay(string cardNO, decimal money, string reMark, string deptCode)
       // {
       //     try
       //     {
       //         #region 更新帐户余额
       //         //帐户余额
       //         decimal vacancy = 0;
       //         int resullt = this.GetVacancy(cardNO, ref vacancy);
       //         if (resullt <= 0)
       //         {
       //             MessageBox.Show(this.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }
       //         //在更新帐户余额是用原余额-money所以在退费入户时应传入负数来增加余额
       //         if (this.UpdateAccountVacancy(cardNO, money) == -1)
       //         {
       //             MessageBox.Show("更新帐户余额失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }

       //         #endregion

       //         #region 插入一条新交易记录

       //         Neusoft.HISFC.Models.Account.AccountRecord accountRecord = new Neusoft.HISFC.Models.Account.AccountRecord();
       //         accountRecord.CardNO = cardNO;//门诊卡号
       //         accountRecord.DeptCode = deptCode;//科室编码
       //         accountRecord.IsValid = true;//交易状态
       //         accountRecord.Money = -money;//金额
       //         accountRecord.Oper = (this.Operator as Neusoft.HISFC.Models.Base.Employee).ID;//操作员
       //         accountRecord.OperType.ID = ((int)Neusoft.HISFC.Models.Account.OperTypes.CancelPay).ToString();//操作类型
       //         accountRecord.Vacancy = vacancy - money;//本次操作后余额
       //         accountRecord.ReMark = reMark;//发票号
       //         accountRecord.OperTime = this.GetDateTimeFromSysDateTime();//操作时间
       //         if (this.InsertAccountRecord(accountRecord) == -1)
       //         {
       //             MessageBox.Show("增加退费入户数据失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //             return false;
       //         }

       //         #region 废弃
       //         //Neusoft.HISFC.Models.Account.AccountRecord accountRecord = this.GetAccountRecord(cardNO, reMark);
       //         //if (accountRecord == null)
       //         //{
       //         //    MessageBox.Show("获取支付记录失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         //    return false;
       //         //}
       //         //if (!accountRecord.IsValid)
       //         //{
       //         //    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("该费用已退，不可在退！"), Neusoft.FrameWork.Management.Language.Msg("提示"), MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         //    return false;
       //         //}
       //         //Neusoft.HISFC.Models.Account.AccountRecord oldRecord = accountRecord.Clone();
       //         //accountRecord.Vacancy = vacancy + money;
       //         //accountRecord.OperType.ID = ((int)Neusoft.HISFC.Models.Account.OperTypes.CancelPay).ToString();
       //         //accountRecord.OperTime = this.GetDateTimeFromSysDateTime();
       //         //accountRecord.DeptCode = deptCode;
       //         //accountRecord.IsValid = true;
       //         //accountRecord.Money = money;
       //         //accountRecord.Oper = (this.Operator as Neusoft.HISFC.Models.Base.Employee).ID;

       //         //if (this.InsertAccountRecord(accountRecord) == -1)
       //         //{
       //         //    MessageBox.Show("增加退费入户数据失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         //    return false;
       //         //}
       //         #endregion

       //         #endregion

       //         #region 更新原交易记录状态 （废弃）
       //         ////int result = this.UpdateAccountRecordState(((int)Neusoft.FrameWork.Function.NConvert.ToInt32(false)).ToString(), //状态
       //         ////                                             cardNO,//门诊卡号
       //         ////                                             oldRecord.OperTime.ToString(),//操作时间
       //         ////                                             oldRecord.ReMark);//发票号
       //         ////if (result == -1)
       //         ////{
       //         ////    MessageBox.Show("更改支付数据标志失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         ////    return false;
       //         ////}
       //         #endregion

       //         MessageBox.Show("退费入户成功 （" + (-money).ToString() + "） ！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         return true;
       //     }
       //     catch (Exception ex)
       //     {
       //         MessageBox.Show("退费入户失败！" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
       //         return false;
       //     }
       // }

       // #endregion

       // #region 根据门诊帐户、交易时间、操作状态查找交易记录
       // /// <summary>
       // /// 根据门诊帐户、交易时间、操作状态查找交易记录
       // /// </summary>
       // /// <param name="cardNO">门诊帐户</param>
       // /// <param name="begin">开始时间</param>
       // /// <param name="end">结束时间</param>
       // /// <param name="opertype">操作类型</param>
       // /// <returns></returns>
       // public List<Neusoft.HISFC.Models.Account.AccountRecord> GetAccountRecordList(string cardNO, string begin, string end, string opertype)
       // {
       //     string Sql = string.Empty;
       //     string SqlWhere = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecord", ref Sql) == -1)
       //     {
       //         this.Err = "提取SQL语句出错！";
       //         return null;
       //     }
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecordWhere1", ref SqlWhere) == -1)
       //     {
       //         this.Err = "提取SQL语句出错！";
       //         return null;
       //     }

       //     try
       //     {
       //         SqlWhere = string.Format(SqlWhere, cardNO, begin, end, opertype);
       //         Sql += " " + SqlWhere;
       //         if (this.ExecQuery(Sql) == -1)
       //         {
       //             this.Err = "查找帐户交易数据失败！";
       //             return null;
       //         }
       //         List<Neusoft.HISFC.Models.Account.AccountRecord> list = new List<Neusoft.HISFC.Models.Account.AccountRecord>();
       //         Neusoft.HISFC.Models.Account.AccountRecord accountRecord = null;
       //         while (this.Reader.Read())
       //         {
       //             accountRecord = new Neusoft.HISFC.Models.Account.AccountRecord();
       //             accountRecord.CardNO = Reader[0].ToString();
       //             accountRecord.OperType.ID = Reader[1].ToString();
       //             if (Reader[2] != DBNull.Value) accountRecord.Money = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[2]);
       //             accountRecord.DeptCode = Reader[3].ToString();
       //             accountRecord.Oper = Reader[4].ToString();
       //             accountRecord.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
       //             if (Reader[5] != DBNull.Value) accountRecord.ReMark = Reader[6].ToString();
       //             accountRecord.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[7]);
       //             if (Reader[8] != DBNull.Value) accountRecord.Vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[8]);
       //             list.Add(accountRecord);
       //         }
       //         return list;
       //     }
       //     catch (Exception ex)
       //     {
       //         this.Err = ex.Message;
       //         return null;
       //     }
       // }
       // #endregion

       // #region  根据门诊帐户、交易时间查找交易记录
       // /// <summary>
       // /// 根据门诊帐户、交易时间查找交易记录
       // /// </summary>
       // /// <param name="cardNO">门诊卡号</param>
       // /// <param name="begin">开始时间</param>
       // /// <param name="end">结束时间</param>
       // /// <returns></returns>
       // public List<Neusoft.HISFC.Models.Account.AccountRecord> GetAccountRecordList(string cardNO, string begin, string end)
       // {
       //     string Sql = string.Empty;
       //     string SqlWhere = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecord", ref Sql) == -1)
       //     {
       //         this.Err = "提取SQL语句出错！";
       //         return null;
       //     }
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecordWhere3", ref SqlWhere) == -1)
       //     {
       //         this.Err = "提取SQL语句出错！";
       //         return null;
       //     }

       //     try
       //     {
       //         SqlWhere = string.Format(SqlWhere, cardNO, begin, end);
       //         Sql += " " + SqlWhere;
       //         if (this.ExecQuery(Sql) == -1)
       //         {
       //             this.Err = "查找帐户交易数据失败！";
       //             return null;
       //         }
       //         List<Neusoft.HISFC.Models.Account.AccountRecord> list = new List<Neusoft.HISFC.Models.Account.AccountRecord>();
       //         Neusoft.HISFC.Models.Account.AccountRecord accountRecord = null;
       //         while (this.Reader.Read())
       //         {
       //             accountRecord = new Neusoft.HISFC.Models.Account.AccountRecord();
       //             accountRecord.CardNO = Reader[0].ToString();
       //             accountRecord.OperType.ID = Reader[1].ToString();
       //             if (Reader[2] != DBNull.Value) accountRecord.Money = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[2]);
       //             accountRecord.DeptCode = Reader[3].ToString();
       //             accountRecord.Oper = Reader[4].ToString();
       //             accountRecord.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
       //             if (Reader[5] != DBNull.Value) accountRecord.ReMark = Reader[6].ToString();
       //             accountRecord.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[7]);
       //             if (Reader[8] != DBNull.Value) accountRecord.Vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[8]);
       //             list.Add(accountRecord);
       //         }
       //         return list;
       //     }
       //     catch (Exception ex)
       //     {
       //         this.Err = ex.Message;
       //         return null;
       //     }
       // }

       // #endregion

       // #region 根据门诊卡号、发票号查询交易记录
       // /// <summary>
       // /// 根据门诊卡号、发票号查询交易记录
       // /// </summary>
       // /// <param name="cardNO">门诊卡号</param>
       // /// <param name="invoiceNO">发票号</param>
       // /// <returns>交易实体</returns>
       // private Neusoft.HISFC.Models.Account.AccountRecord GetAccountRecord(string cardNO, string invoiceNO)
       // {
       //     string Sql = string.Empty;
       //     string SqlWhere = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecord", ref Sql) == -1)
       //     {
       //         this.Err = "提取SQL语句出错！";
       //         return null;
       //     }
       //     if (this.Sql.GetSql("Fee.Account.SelectAccountRecordWhere2", ref SqlWhere) == -1)
       //     {
       //         this.Err = "提取SQL语句出错！";
       //         return null;
       //     }

       //     try
       //     {
       //         SqlWhere = string.Format(SqlWhere, cardNO, invoiceNO);
       //         Sql += " " + SqlWhere;
       //         if (this.ExecQuery(Sql) == -1)
       //         {
       //             this.Err = "查找帐户交易数据失败！";
       //             return null;
       //         }
       //         Neusoft.HISFC.Models.Account.AccountRecord accountRecord = null;
       //         while (this.Reader.Read())
       //         {
       //             accountRecord = new Neusoft.HISFC.Models.Account.AccountRecord();
       //             accountRecord.CardNO = Reader[0].ToString();
       //             accountRecord.OperType.ID = Reader[1].ToString();
       //             if (Reader[2] != DBNull.Value) accountRecord.Money = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[2]);
       //             accountRecord.DeptCode = Reader[3].ToString();
       //             accountRecord.Oper = Reader[4].ToString();
       //             accountRecord.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
       //             if (Reader[5] != DBNull.Value) accountRecord.ReMark = Reader[6].ToString();
       //             accountRecord.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[7]);
       //             if (Reader[8] != DBNull.Value) accountRecord.Vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(Reader[8]);
       //         }
       //         return accountRecord;
       //     }
       //     catch (Exception ex)
       //     {
       //         this.Err = ex.Message;
       //         return null;
       //     }
       // }
       // #endregion

       // #region 更新交易状态
       // /// <summary>
       // /// 更新交易状态
       // /// </summary>
       // /// <param name="valid">是否有效0有效1无效</param>
       // /// <param name="cardNO">门诊帐号</param>
       // /// <param name="operTime">操作时间</param>
       // /// <returns></returns>
       // public int UpdateAccountRecordState(string valid, string cardNO, string operTime, string remark)
       // {
       //     string Sql = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.UpdateAccountRecordValid", ref Sql) == -1) return -1;
       //     try
       //     {
       //         Sql = string.Format(Sql, valid, cardNO, operTime, remark);
       //     }
       //     catch (Exception ex)
       //     {
       //         this.Err = ex.Message;
       //         this.ErrCode = ex.Message;
       //         return -1;
       //     }
       //     return this.ExecNoQuery(Sql);
       // }
       // #endregion

       // #region 卡费用信息交易记录
       // /// <summary>
       // /// 卡费用信息交易记录
       // /// </summary>
       // /// <returns></returns>
       // public int InsertAccountRecord(Neusoft.HISFC.Models.Account.AccountRecord accountRecord)
       // {
       //     string Sql = string.Empty;
       //     if (this.Sql.GetSql("Fee.Account.InsertAccountRecord", ref Sql) == -1) return -1;
       //     try
       //     {
       //         Sql = string.Format(Sql,
       //                           accountRecord.CardNO,
       //                           accountRecord.OperType.ID,
       //                           accountRecord.Money,
       //                           accountRecord.DeptCode,
       //                           accountRecord.Oper,
       //                           accountRecord.OperTime.ToString(),
       //                           accountRecord.ReMark,
       //                           Neusoft.FrameWork.Function.NConvert.ToInt32(accountRecord.IsValid),
       //                           accountRecord.Vacancy);
       //     }
       //     catch (Exception ex)
       //     {

       //         this.Err = ex.Message;
       //         this.ErrCode = ex.Message;
       //         return -1;
       //     }
       //     return this.ExecNoQuery(Sql);

       // }
       // #endregion

        #endregion

        #region 帐户数据操作

        //#region 得到帐户余额
        ///// <summary>
        ///// 得到帐户余额
        ///// </summary>
        ///// <param name="cardNO">门诊卡号</param>
        ///// <param name="vacancy">帐户余额</param>
        ///// <returns>-1 失败 0是没有帐户或帐户停用 1成功</returns>
        //public int GetVacancy(string cardNO, ref decimal vacancy)
        //{
        //    string Sql = string.Empty;
        //    bool isHaveVacancy = false;
        //    if (this.Sql.GetSql("Fee.Account.GetVacancy", ref Sql) == -1)
        //    {
        //        this.Err = "为找到SQL语句！";

        //        return -1;
        //    }
        //    try
        //    {
        //        if (this.ExecQuery(Sql, cardNO) == -1)
        //        {
        //            return -1;
        //        }

        //        string state = string.Empty;

        //        while (this.Reader.Read())
        //        {
        //            vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
        //            state = Reader[1].ToString();
        //            isHaveVacancy = true;
        //        }
        //        if (isHaveVacancy)
        //        {
        //            if (state == "0")
        //            {
        //                this.Err = "该帐户已停用";
        //                return 0;

        //            }
        //            return 1;
        //        }
        //        else
        //        {
        //            this.Err = "该帐户不存在！";
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = "获得帐户余额失败！" + ex.Message;

        //        return -1;
        //    }
        //}
        //#endregion

        //#region 根据门诊卡号更新帐户余额
        ///// <summary>
        ///// 根据门诊卡号更新帐户余额
        ///// </summary>
        ///// <param name="cardNO">门诊卡好</param>
        ///// <param name="money">消费金额</param>
        ///// <returns></returns>
        //public int UpdateAccountVacancy(string cardNO, decimal money)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.UpdateAccountVacancy", ref Sql) == -1) return -1;
        //    try
        //    {
        //        Sql = string.Format(Sql, cardNO, money);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}
        //#endregion

        //#region 根据门诊卡号查找密码
        ///// <summary>
        ///// 根据门诊卡号查找密码
        ///// </summary>
        ///// <param name="cardNO">门诊卡号</param>
        ///// <returns>用户密码</returns>
        //public string GetPassWordByCardNO(string cardNO)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.SelectPassWordByCardNo", ref Sql) == -1)
        //    {
        //        this.Err = "查找SQL语句失败！";
        //        return "-1";
        //    }
        //    try
        //    {
        //        Sql = string.Format(Sql, cardNO);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return "-1";
        //    }
        //    return this.ExecSqlReturnOne(Sql);
        //}
        //#endregion

        //#region 根据门诊卡号更新用户密码
        ///// <summary>
        ///// 根据门诊卡号更新用户密码
        ///// </summary>
        ///// <param name="cardNO">门诊卡号</param>
        ///// <param name="passWord">密码</param>
        ///// <returns></returns>
        //public int UpdatePassWordByCardNO(string cardNO, string passWord)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.UpdatePassWord", ref Sql) == -1) return -1;
        //    try
        //    {
        //        Sql = string.Format(Sql, cardNO, passWord);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}
        //#endregion

        //#region 根据门诊卡号更新用户每日消费限额
        ///// <summary>
        ///// 根据门诊卡号更新用户每日消费限额
        ///// </summary>
        ///// <param name="cardNO"></param>
        ///// <param name="dayLimit"></param>
        ///// <returns></returns>
        //public int UpdateDaylimitByCardNO(string cardNO, decimal dayLimit)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.UpdateDaylimit", ref Sql) == -1) return -1;
        //    try
        //    {
        //        Sql = string.Format(Sql, cardNO, dayLimit);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}
        //#endregion

        //#region 更新帐户状态
        ///// <summary>
        ///// 更新帐户状态
        ///// </summary>
        ///// <param name="cardNO"></param>
        ///// <param name="state"></param>
        ///// <returns></returns>
        //public int UpdateAccountState(string cardNO, string state)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.UpdateAccountState", ref Sql) == -1) return -1;
        //    try
        //    {
        //        Sql = string.Format(Sql, state, cardNO);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}
        //#endregion

        //#region 新建帐户
        ///// <summary>
        ///// 新建帐户
        ///// </summary>
        ///// <param name="account">帐户实体</param>
        ///// <returns></returns>
        //public int InsertAccount(Neusoft.HISFC.Models.Account.Account account)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.InsertAccount", ref Sql) == -1) return -1;
        //    try
        //    {
        //        Sql = string.Format(Sql,
        //                            account.AccountCard.CardNO, //门诊卡号
        //                            Neusoft.FrameWork.Function.NConvert.ToInt32(account.IsValid).ToString() //是否有效
        //                            );

        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return -1;
        //    }
        //    return this.ExecNoQuery(Sql);
        //}
        //#endregion

        //#region 根据门诊卡号取帐户信息
        ///// <summary>
        ///// 根据门诊卡号取帐户信息
        ///// </summary>
        ///// <param name="cardNO"></param>
        ///// <returns></returns>
        //public Neusoft.HISFC.Models.Account.Account GetAccount(string cardNO)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.SelectAccount", ref Sql) == -1) return null;
        //    Neusoft.HISFC.Models.Account.Account account = null;
        //    try
        //    {
        //        Sql = string.Format(Sql, cardNO);
        //        if (this.ExecQuery(Sql) == -1) return null; ;
        //        while (this.Reader.Read())
        //        {
        //            account = new Neusoft.HISFC.Models.Account.Account();
        //            if (this.Reader[1] != DBNull.Value) account.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[1]);
        //            if (this.Reader[2] != DBNull.Value) account.Vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
        //            if (this.Reader[3] != DBNull.Value) account.PassWord = this.Reader[3].ToString();
        //            if (this.Reader[4] != DBNull.Value) account.DayLimit = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        this.ErrCode = ex.Message;
        //        return null;
        //    }
        //    return account;
        //}
        //#endregion

        //#region 根据物理卡号查找帐户数据
        ///// <summary>
        ///// 根据物理卡号查找帐户数据
        ///// </summary>
        ///// <param name="markNo">物理卡号</param>
        ///// <returns></returns>
        //public Neusoft.HISFC.Models.Account.Account GetAccountByMarkNo(string markNo)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.AccountByMarkNo", ref Sql) == -1)
        //    {
        //        this.Err = "查找SQL语句失败！";
        //        return null;
        //    }
        //    try
        //    {
        //        Sql = string.Format(Sql, markNo);
        //        if (this.ExecQuery(Sql) < 0)
        //        {
        //            this.Err = "查找数据失败！";
        //            return null;
        //        }
        //        Neusoft.HISFC.Models.Account.Account account = new Neusoft.HISFC.Models.Account.Account();
        //        //一个卡号只能对应一个帐户
        //        while (this.Reader.Read())
        //        {
        //            account.AccountCard.CardNO = this.Reader[0].ToString();
        //            account.IsValid = Neusoft.FrameWork.Function.NConvert.ToBoolean(this.Reader[1]);
        //            account.Vacancy = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
        //            account.PassWord = this.Reader[3].ToString();
        //            account.DayLimit = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
        //        }
        //        return account;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Err = "查找数据失败！" + ex.Message;
        //        return null;
        //    }

        //}

        //#endregion

        //#region 根据证件号查找帐户密码
        ///// <summary>
        ///// 根据证件号查找帐户密码
        ///// </summary>
        ///// <param name="idenno">证件号</param>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public int SelectAccountPassWord(string idenno, ref List<Neusoft.FrameWork.Models.NeuObject> list)
        //{
        //    string Sql = string.Empty;
        //    if (this.Sql.GetSql("Fee.Account.SelectAccountPassWord", ref Sql) == -1)
        //    {
        //        this.Err = "查找SQL语句失败！";
        //        return -1;
        //    }
        //    try
        //    {
        //        Sql = string.Format(Sql, idenno);
        //        if (this.ExecQuery(Sql) == -1)
        //        {
        //            this.Err = "查找数据失败！";
        //            return -1;
        //        }
        //        list = new List<Neusoft.FrameWork.Models.NeuObject>();
        //        while (this.Reader.Read())
        //        {
        //            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
        //            obj.ID = this.Reader[0].ToString();
        //            obj.Name = this.Reader[1].ToString();
        //            list.Add(obj);
        //        }
        //        return 1;
        //    }
        //    catch(Exception ex)
        //    {
        //        this.Err = ex.Message;
        //        return -1;
        //    }
        //}
        //#endregion
        #endregion

        #region 处理中大肿瘤初诊  2007-11-30

        /// <summary>
        /// 插入患者信息 多保存一个打印条码流水号
        /// </summary>
        /// <param name="patientInfo">患者实体</param>
        /// <returns></returns>
        public int InsertPatientZDZL(Neusoft.HISFC.Models.Account.PatientAccount patientInfo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.InsertPatientZDZL", ref Sql) == -1)
                return -1;
            try
            {
                #region 格式化SQL
                Sql = string.Format(Sql,
                                   patientInfo.PID.CardNO, //门诊卡号
                                   
                                   patientInfo.Name,//姓名
                                   patientInfo.Birthday.ToShortDateString().ToString(),//出生日期
                                   patientInfo.Sex.ID,//性别
                                   patientInfo.IDCard,//身份证号
                                   patientInfo.Profession,//职业
                                   patientInfo.CompanyName,//工作单位
                                   patientInfo.PhoneBusiness,//单位电话
                                   patientInfo.AddressHome,//户口或家庭所在
                                   patientInfo.PhoneHome,//家庭电话
                                   patientInfo.DIST,//籍贯
                                   patientInfo.Nationality.ID,//民族
                                   patientInfo.Kin.Name,//联系人姓名
                                   patientInfo.Kin.RelationPhone,//联系人电话
                                   patientInfo.Kin.RelationAddress,//联系人住址
                                   patientInfo.Kin.Relation.ID,//联系人关系
                                   patientInfo.MaritalStatus.ID,//婚姻状况
                                   patientInfo.Country.ID,//国籍
                                   patientInfo.Pact.ID,//合同代码
                                   patientInfo.Pact.Name,//合同单位名称
                                   patientInfo.AreaCode,//出生地
                                   patientInfo.Oper.ID,//操作员
                                   patientInfo.Oper.OperTime, //操作时间
                                   Neusoft.FrameWork.Function.NConvert.ToInt32(patientInfo.IsEncrypt),//是否加密姓名
                                   patientInfo.NormalName, //密文
                                   patientInfo.IDCardType.ID,
                                   patientInfo.PID.ID //条码打印号
                                   );
                #endregion

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        /// <summary>
        /// 获取打印条码流水号
        /// </summary>
        /// <returns></returns>
        public string GetPrintCardID()
        {
            return this.GetSequence("Fee.Account.GetPrintCardID");
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <returns></returns>
        public System.Collections.ArrayList GetPatientInfo(DateTime operTime)
        {
            string Sql = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.Account.SelectPatient", ref Sql) == -1)
                return null;
            if (this.Sql.GetSql("Fee.Account.SelectPatientWhere11", ref SqlWhere) == -1)
                return null;
            Neusoft.HISFC.Models.Account.PatientAccount patientInfo = null;
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            try
            {
                SqlWhere = string.Format(SqlWhere, operTime);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1)
                    return null;
                while (this.Reader.Read())
                {
                    patientInfo = new Neusoft.HISFC.Models.Account.PatientAccount();
                    #region 患者信息
                    if (Reader[0] != DBNull.Value)
                        patientInfo.PID.CardNO = Reader[0].ToString();//病历卡号
                    if (Reader[1] != DBNull.Value)
                        patientInfo.Card.ICCard.ID = Reader[1].ToString(); //条码号
                    if (Reader[2] != DBNull.Value)
                        patientInfo.Name = Reader[2].ToString(); //姓名
                    if (Reader[6] != DBNull.Value)
                        patientInfo.Sex.ID = Reader[6].ToString();//性别
                    if (Reader[0] != DBNull.Value)
                        patientInfo.PID.CardNO = Reader[0].ToString();//就诊卡号
                    if (Reader[26] != DBNull.Value)
                        patientInfo.Pact.ID = Reader[26].ToString();//结算类别code
                    if (Reader[27] != DBNull.Value)
                        patientInfo.Pact.Name = Reader[27].ToString();//结算类别名称
                    if (Reader[29] != DBNull.Value)
                        patientInfo.AreaCode = Reader[29].ToString();//出生地
                    if (Reader[23] != DBNull.Value)
                        patientInfo.Country.ID = Reader[23].ToString();//国籍
                    if (Reader[17] != DBNull.Value)
                        patientInfo.Nationality.ID = Reader[17].ToString();//民族
                    patientInfo.Birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(Reader[5]);//出生日期
                    if (Reader[16] != DBNull.Value)
                        patientInfo.DIST = Reader[16].ToString();//籍贯
                    if (Reader[9] != DBNull.Value)
                        patientInfo.Profession.ID = Reader[9].ToString();//职业
                    if (Reader[7] != DBNull.Value)
                        patientInfo.IDCard = Reader[7].ToString();//身份证号
                    if (Reader[10] != DBNull.Value)
                        patientInfo.CompanyName = Reader[10].ToString();//工作单位
                    if (Reader[11] != DBNull.Value)
                        patientInfo.PhoneBusiness = Reader[11].ToString();//单位电话
                    if (Reader[22] != DBNull.Value)
                        patientInfo.MaritalStatus.ID = Reader[22].ToString();//婚姻状况
                    if (Reader[13] != DBNull.Value)
                        patientInfo.AddressHome = Reader[13].ToString();//家庭地址
                    if (Reader[14] != DBNull.Value)
                        patientInfo.PhoneHome = Reader[14].ToString();//家庭电话
                    if (Reader[18] != DBNull.Value)
                        patientInfo.Kin.Name = Reader[18].ToString();//联系人姓名
                    if (Reader[19] != DBNull.Value)
                        patientInfo.Kin.RelationPhone = Reader[19].ToString();//联系人电话
                    if (Reader[20] != DBNull.Value)
                        patientInfo.Kin.RelationAddress = Reader[20].ToString();//联系人地址
                    if (Reader[21] != DBNull.Value)
                        patientInfo.Kin.Relation.ID = Reader[21].ToString();//联系人关系
                    patientInfo.IsEncrypt = Neusoft.FrameWork.Function.NConvert.ToBoolean(Reader[53]);//是否加密姓名
                    patientInfo.NormalName = Reader[54].ToString();//密文
                    if (patientInfo.IsEncrypt)
                    {
                        //patientInfo.DecryptName = Neusoft.FrameWork.Interface.Classes.Function.Decrypt3DES(patientInfo.NormalName);
                    }
                    else
                    {
                        patientInfo.DecryptName = patientInfo.Name;
                    }
                    if (this.Reader[55] != DBNull.Value)
                        patientInfo.IDCardType.ID = this.Reader[55].ToString();
                    #endregion
                    list.Add(patientInfo);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return list;
        }

        /// <summary>
        /// 添加条码号
        /// </summary>
        /// <param name="icCard"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int UpdatePrintID(string icCard, string cardNo)
        {
            string strSQL = "";

            if (this.Sql.GetSql("Neusoft.HISFC.Management.Fee.UpdatePrintID", ref strSQL) == -1)
            {
                this.Err = "没有找到Neusoft.HISFC.Management.Fee.UpdatePrintID字段！";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, icCard, cardNo);
            }
            catch(Exception ex)
            {
                this.Err = ex.Message;

                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// 添加条码号,病历表
        /// </summary>
        /// <param name="icCard"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int UpdatePrintIDCase(string icCard, string cardNo)
        {
            string strSQL = "";

            if (this.Sql.GetSql("Neusoft.HISFC.Management.Fee.UpdatePrintIDCase", ref strSQL) == -1)
            {
                this.Err = "Neusoft.HISFC.Management.Fee.UpdatePrintIDCase字段！";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL, icCard, cardNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 添加条码费用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oper"></param>
        /// <param name="operTime"></param>
        /// <param name="newFlag"></param>
        /// <param name="cost"></param>
        /// <param name="printNo"></param>
        /// <param name="patientNo"></param>
        /// <param name="patientName"></param>
        /// <param name="extend1"></param>
        /// <param name="extend2"></param>
        /// <param name="extend3"></param>
        /// <param name="validstat"></param>
        /// <returns></returns>
        public int InsertCardFee(string id, string oper, DateTime operTime, string newFlag, decimal cost, string printNo, string patientNo, string patientName, string extend1, string extend2, string extend3, string validstat)
        {
            string strSQL = "";

            if (this.Sql.GetSql("Neusoft.HISFC.Management.Fee.InsertCardFee", ref strSQL) == -1)
            {
                this.Err = "没有找到Neusoft.HISFC.Management.Fee.InsertCardFee字段！";

                return -1;
            }

            try
            {
                strSQL = string.Format(strSQL,  id,  oper,  operTime.ToString(),  newFlag,  cost.ToString(),  printNo,  patientNo,  patientName,  extend1,  extend2, extend3,  validstat);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 插入新病人图片
        /// </summary>
        /// <param name="icCard"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public int InsertPatientPhoto(Neusoft.FrameWork.Management.Transaction trans, string icCard ,Byte[] photo)
        {
            //try
            //{
            //    this.command = new IBM.Data.DB2.DB2Command ( );   
            //    this.command.Transaction = trans.Trans as IBM.Data.DB2.DB2Transaction;
            //    this.command.Connection = this.con as IBM.Data.DB2.DB2Connection;
            //    //this.command.Connection = Neusoft.FrameWork.Management.Connection.Instance;
            //    this.command.CommandText = "Insert into COM_PATIENTINFO_PHOTO (photo,ic_cardno) values(?,?) ";
            //    IBM.Data.DB2.DB2Parameter parPhoto = new IBM.Data.DB2.DB2Parameter ( "PHOTO", IBM.Data.DB2.DB2Type.Binary, photo.Length );
            //    parPhoto.Value = photo;
            //    IBM.Data.DB2.DB2Parameter parCard = new IBM.Data.DB2.DB2Parameter ( "CARD", IBM.Data.DB2.DB2Type.VarChar, icCard.Length );
            //    parCard.Value = icCard;
            //    this.command.Parameters.Add ( parPhoto );
            //    this.command.Parameters.Add ( parCard );
            //}
            //catch ( Exception ex )
            //{
            //    this.Err = ex.Message;

            //    return -1;
            //}
            //return this.command.ExecuteNonQuery ( );

            return -1;

            
        }

        /// <summary>
        /// 查询患者的图片
        /// </summary>
        /// <returns></returns>
        public int QueryPatinePhoto ( string ic_CardNo, ref Byte[] Photo )
        {
            string Sql = string.Empty;
            
            if ( this.Sql.GetSql ( "Fee.Account.SelectPatientPhoto", ref Sql ) == -1 )
                return -1;


            Photo = new Byte [204800];
            try
            {
                 Sql= string.Format ( Sql,ic_CardNo);
                 System.Data.DataSet ds =new DataSet();
                if ( this.ExecQuery ( Sql,ref  ds ) == -1 )
                    return -1;
                Photo = ds.Tables[0].Rows[0][0] as Byte [ ];
                //while ( this.Reader.Read ( ) )
                //{

                //    if ( Reader [0] != DBNull.Value )
                //        Photo =  Reader [0] as Byte[];
                    
                //}
                //this.Reader.Close ( );
            }
            catch ( Exception ex )
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        ///  删除患者图片
        /// </summary>
        /// <param name="icCard"></param>
        /// <returns></returns>
        public int DeletePatinePhoto ( string icCard )
        {
            string strSQL = "";

            if ( this.Sql.GetSql ( "Fee.Account.DeletePatientPhoto", ref strSQL ) == -1 )
            {
                this.Err = "Fee.Account.DeletePatientPhoto字段！";

                return -1;
            }

            try
            {
                strSQL = string.Format ( strSQL, icCard);
            }
            catch ( Exception ex )
            {
                this.Err = ex.Message;

                return -1;
            }

            return this.ExecNoQuery ( strSQL );
        }

        #endregion

        #region 插入登记信息
        /// <summary>
        /// 插入挂号记录表
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdatePERegister(Neusoft.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            if (this.Sql.GetSql("Registration.Register.Update.PeCharge", ref sql) == -1)
                return -1;

            try
            {
                sql = string.Format(sql, register.ID, register.PID.CardNO,
                     register.DoctorInfo.SeeDate.ToString(), register.DoctorInfo.Templet.Noon.ID,
                     register.Name, register.IDCard, register.Sex.ID, register.Birthday.ToString(),
                     register.Pact.PayKind.ID, register.Pact.PayKind.Name, register.Pact.ID, register.Pact.Name,
                     register.SSN, register.DoctorInfo.Templet.RegLevel.ID, register.DoctorInfo.Templet.RegLevel.Name,
                     register.DoctorInfo.Templet.Dept.ID, register.DoctorInfo.Templet.Dept.Name,
                     register.DoctorInfo.SeeNO, register.DoctorInfo.Templet.Doct.ID,
                     register.DoctorInfo.Templet.Doct.Name, Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsFee),
                     (int)register.RegType, Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsFirst),
                     register.RegLvlFee.RegFee.ToString(), register.RegLvlFee.ChkFee.ToString(),
                     register.RegLvlFee.OwnDigFee.ToString(), register.RegLvlFee.OthFee.ToString(),
                     register.OwnCost.ToString(), register.PubCost.ToString(), register.PayCost.ToString(),
                     (int)register.Status, register.InputOper.ID, Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsSee),
                     Neusoft.FrameWork.Function.NConvert.ToInt32(register.CheckOperStat.IsCheck), register.PhoneHome,
                     register.AddressHome, (int)register.TranType, register.CardType.ID,
                     register.DoctorInfo.Templet.Begin.ToString(), register.DoctorInfo.Templet.End.ToString(),
                     register.CancelOper.ID, register.CancelOper.OperTime.ToString(),
                     register.InvoiceNO, register.RecipeNO, Neusoft.FrameWork.Function.NConvert.ToInt32(register.DoctorInfo.Templet.IsAppend),
                     register.OrderNO, register.DoctorInfo.Templet.ID,
                     register.InputOper.OperTime.ToString(), register.InSource.ID, Neusoft.FrameWork.Function.NConvert.ToInt32(register.CaseState),
                     Neusoft.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt), register.NormalName);

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新挂号主表类别表出错![Registration.Register.Update.PeCharge]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion
    }
}
