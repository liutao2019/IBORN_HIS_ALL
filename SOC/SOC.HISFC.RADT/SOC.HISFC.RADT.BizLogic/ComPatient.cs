using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Collections;
using FS.FrameWork.Models;
using FS.HISFC.Models.RADT;

namespace FS.SOC.HISFC.RADT.BizLogic
{
    /// <summary>
    /// 患者基本信息表业务类
    /// 重新SOC-赵景
    /// </summary>
    public class ComPatient : Database
    {
        /// <summary>
        /// 查询门诊患者信息(com_Patient)
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        private ArrayList queryComPatient(string whereIndex, params string[] parms)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("RADT.Inpatient.PatientQuery", ref sql) == -1)
            {
                this.Err = "查询索引为RADT.Inpatient.PatientQuery的SQL语句失败！";
                return null;
            }
            string sqlWhere = string.Empty;
            if (this.Sql.GetCommonSql(whereIndex, ref sqlWhere) == -1)
            {
                this.Err = "查找索引为" + whereIndex + "的SQL语句失败！";
                return null;
            }
            sqlWhere = string.Format(sqlWhere, parms);
            sql += sqlWhere;
            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            Patient Patient = null;
            ArrayList alPatient = new ArrayList();
            while (Reader.Read())
            {
                try
                {
                    Patient = new Patient();
                    if (!Reader.IsDBNull(0)) Patient.PID.CardNO = Reader[0].ToString(); //就诊卡号
                    if (!Reader.IsDBNull(1)) Patient.Name = Reader[1].ToString(); //姓名
                    if (!Reader.IsDBNull(2)) Patient.SpellCode = Reader[2].ToString(); //拼音码
                    if (!Reader.IsDBNull(3)) Patient.WBCode = Reader[3].ToString(); //五笔
                    if (!Reader.IsDBNull(4)) Patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //出生日期
                    if (!Reader.IsDBNull(5)) Patient.Sex.ID = Reader[5].ToString(); //性别
                    if (!Reader.IsDBNull(6)) Patient.IDCard = Reader[6].ToString(); //身份证号
                    if (!Reader.IsDBNull(7)) Patient.BloodType.ID = Reader[7].ToString(); //血型
                    if (!Reader.IsDBNull(8)) Patient.Profession.ID = Reader[8].ToString(); //职业
                    if (!Reader.IsDBNull(9)) Patient.CompanyName = Reader[9].ToString(); //工作单位
                    if (!Reader.IsDBNull(10)) Patient.PhoneBusiness = Reader[10].ToString(); //单位电话
                    if (!Reader.IsDBNull(11)) Patient.BusinessZip = Reader[11].ToString(); //单位邮编
                    if (!Reader.IsDBNull(12)) Patient.AddressHome = Reader[12].ToString(); //户口或家庭所在
                    if (!Reader.IsDBNull(13)) Patient.PhoneHome = Reader[13].ToString(); //家庭电话
                    if (!Reader.IsDBNull(14)) Patient.HomeZip = Reader[14].ToString(); //户口或家庭邮政编码
                    if (!Reader.IsDBNull(15)) Patient.DIST = Reader[15].ToString(); //籍贯
                    if (!Reader.IsDBNull(16)) Patient.Nationality.ID = Reader[16].ToString(); //民族
                    if (!Reader.IsDBNull(17)) Patient.Kin.Name = Reader[17].ToString(); //联系人姓名
                    if (!Reader.IsDBNull(18)) Patient.Kin.RelationPhone = Reader[18].ToString(); //联系人电话
                    if (!Reader.IsDBNull(19)) Patient.Kin.RelationAddress = Reader[19].ToString(); //联系人住址
                    if (!Reader.IsDBNull(20)) Patient.Kin.Relation.ID = Reader[20].ToString(); //联系人关系
                    if (!Reader.IsDBNull(21)) Patient.MaritalStatus.ID = Reader[21].ToString(); //婚姻状况
                    if (!Reader.IsDBNull(22)) Patient.Country.ID = Reader[22].ToString(); //国籍
                    if (!Reader.IsDBNull(23)) Patient.Pact.PayKind.ID = Reader[23].ToString(); //结算类别
                    if (!Reader.IsDBNull(24)) Patient.Pact.PayKind.Name = Reader[24].ToString(); //结算类别名称
                    if (!Reader.IsDBNull(25)) Patient.Pact.ID = Reader[25].ToString(); //合同代码
                    if (!Reader.IsDBNull(26)) Patient.Pact.Name = Reader[26].ToString(); //合同单位名称
                    if (!Reader.IsDBNull(27)) Patient.SSN = Reader[27].ToString(); //医疗证号
                    if (!Reader.IsDBNull(28)) Patient.AreaCode = Reader[28].ToString(); //地区
                    //if (!Reader.IsDBNull(29)) Patient.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //医疗费用
                    if (!Reader.IsDBNull(30)) Patient.Card.ICCard.ID = Reader[30].ToString(); //电脑号
                    if (!Reader.IsDBNull(31)) Patient.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //药物过敏
                    if (!Reader.IsDBNull(32)) Patient.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //重要疾病
                    if (!Reader.IsDBNull(33)) Patient.Card.NewPassword = Reader[33].ToString(); //帐户密码
                    if (!Reader.IsDBNull(34)) Patient.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //帐户总额
                    if (!Reader.IsDBNull(35)) Patient.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //上期帐户余额
                    //					if (!this.Reader.IsDBNull(36)) Patient=this.Reader[36].ToString();//上期银行余额
                    //					if (!this.Reader.IsDBNull(37)) Patient=this.Reader[37].ToString();//欠费次数
                    //					if (!this.Reader.IsDBNull(38)) Patient=this.Reader[38].ToString();//欠费金额
                    //					if (!this.Reader.IsDBNull(39)) Patient=this.Reader[39].ToString();//住院来源
                    //					if (!this.Reader.IsDBNull(40)) Patient=this.Reader[40].ToString();//最近住院日期
                    //					if (!this.Reader.IsDBNull(41)) Patient=this.Reader[41].ToString();//住院次数
                    //					if (!this.Reader.IsDBNull(42)) Patient=this.Reader[42].ToString();//最近出院日期
                    //					if (!this.Reader.IsDBNull(43)) Patient=this.Reader[43].ToString();//初诊日期
                    //					if (!this.Reader.IsDBNull(44)) Patient=this.Reader[44].ToString();//最近挂号日期
                    //					if (!this.Reader.IsDBNull(45)) Patient=this.Reader[45].ToString();//违约次数
                    //					if (!this.Reader.IsDBNull(46)) Patient=this.Reader[46].ToString();//结束日期
                    if (!Reader.IsDBNull(47)) Patient.Memo = Reader[47].ToString(); //备注
                    if (!Reader.IsDBNull(48)) Patient.User01 = Reader[48].ToString(); //操作员
                    if (!Reader.IsDBNull(49)) Patient.User02 = Reader[49].ToString(); //操作日期
                    if (!Reader.IsDBNull(50)) Patient.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) Patient.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) Patient.IDCardType.ID = Reader[52].ToString();//证件类型
                    if (!Reader.IsDBNull(53)) Patient.VipFlag = NConvert.ToBoolean(Reader[53]);//vip标识
                    if (!Reader.IsDBNull(54)) Patient.MatherName = Reader[54].ToString();//母亲姓名
                    if (!Reader.IsDBNull(55)) Patient.IsTreatment = NConvert.ToBoolean(Reader[55]);//是否急诊
                    if (!Reader.IsDBNull(56)) Patient.PID.CaseNO = Reader[56].ToString();//病案号
                    if (Patient.IsEncrypt && Patient.NormalName != string.Empty)
                    {
                        Patient.DecryptName = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(Patient.NormalName);
                    }
                    if (!Reader.IsDBNull(57)) Patient.Insurance.ID = Reader[57].ToString(); //保险公司编码
                    if (!Reader.IsDBNull(58)) Patient.Insurance.Name = Reader[58].ToString(); //保险公司名称
                    if (!Reader.IsDBNull(59)) Patient.Kin.RelationDoorNo = Reader[59].ToString(); //联系人地址门牌号
                    if (!Reader.IsDBNull(60)) Patient.AddressHomeDoorNo = Reader[60].ToString(); //家庭住址门牌号
                    if (!Reader.IsDBNull(61)) Patient.Email = Reader[61].ToString(); //email
                    alPatient.Add(Patient);
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();
            return alPatient;

        }

        /// <summary>
        /// 获取患者数组
        /// </summary>
        /// <param name="Patient"></param>
        /// <returns></returns>
        private string[] getMyComPatient(Patient Patient)
        {
            string[] s = new string[62];
            s[0] = Patient.PID.CardNO; //就诊卡号
            s[1] = Patient.Name; //姓名
            s[2] = Patient.SpellCode; //拼音码
            s[3] = Patient.WBCode; //五笔
            s[4] = Patient.Birthday.ToString(); //出生日期
            s[5] = Patient.Sex.ID.ToString(); //性别
            s[6] = Patient.IDCard; //身份证号
            s[7] = Patient.BloodType.ID.ToString(); //血型
            s[8] = Patient.Profession.ID; //职业
            s[9] = Patient.CompanyName; //工作单位
            s[10] = Patient.PhoneBusiness; //单位电话
            s[11] = Patient.BusinessZip; //单位邮编
            s[12] = Patient.AddressHome; //户口或家庭所在
            s[13] = Patient.PhoneHome; //家庭电话
            s[14] = Patient.HomeZip; //户口或家庭邮政编码
            s[15] = Patient.DIST; //籍贯
            s[16] = Patient.Nationality.ID; //民族
            s[17] = Patient.Kin.Name; //联系人姓名
            s[18] = Patient.Kin.RelationPhone; //联系人电话
            s[19] = Patient.Kin.RelationAddress; //联系人住址
            s[20] = Patient.Kin.Relation.ID; //联系人关系
            s[21] = Patient.MaritalStatus.ID.ToString(); //婚姻状况
            s[22] = Patient.Country.ID; //国籍
            s[23] = Patient.Pact.PayKind.ID; //结算类别
            s[24] = Patient.Pact.PayKind.Name; //结算类别名称
            s[25] = Patient.Pact.ID; //合同代码
            s[26] = Patient.Pact.Name; //合同单位名称
            s[27] = Patient.SSN; //医疗证号
            s[28] = Patient.AreaCode; //出生地
            //s[29] = Patient.FT.TotCost.ToString(); //医疗费用
            s[31] = FS.FrameWork.Function.NConvert.ToInt32(Patient.Disease.IsAlleray).ToString(); //药物过敏
            s[30] = string.Empty; //电脑号
            s[32] = FS.FrameWork.Function.NConvert.ToInt32(Patient.Disease.IsMainDisease).ToString(); //重要疾病
            s[33] = string.Empty; //帐户密码
            s[34] = "0"; //帐户总额
            s[35] = "0"; //上期帐户余额
            s[36] = "0"; //上期银行余额
            s[37] = "0"; //欠费次数
            s[38] = "0"; //欠费金额
            s[39] = string.Empty; //住院来源
            s[40] = GetSysDateTime(); //最近住院日期
            s[41] = Patient.InTimes.ToString(); //住院次数
            s[42] = GetSysDateTime(); //最近出院日期
            s[43] = GetSysDateTime().ToString(); //初诊日期
            s[44] = GetSysDateTime(); //最近挂号日期
            s[45] = "0"; //违约次数
            s[46] = GetSysDateTime(); //结束日期
            s[47] = Patient.Memo; //备注
            s[48] = Operator.ID; //操作员
            s[49] = GetSysDateTime().ToString(); //操作日期
            s[50] = FS.FrameWork.Function.NConvert.ToInt32(Patient.IsEncrypt).ToString();
            s[51] = Patient.NormalName;

            s[52] = Patient.IDCardType.ID;//证件类型
            s[53] = NConvert.ToInt32(Patient.VipFlag).ToString(); //是否Vip
            s[54] = Patient.MatherName;//母亲姓名
            s[55] = NConvert.ToInt32(Patient.IsTreatment).ToString(); //是否急诊患者
            //s[56] = Patient.CaseNO;//病案号
            s[56] = Patient.PID.CaseNO;//病案号
            //{112F6B96-DC1D-4e20-8290-0403A25B443C}
            s[57] = Patient.Insurance.ID;//保险公司编码
            s[58] = Patient.Insurance.Name;//保险公司名称
            s[59] = Patient.Kin.RelationDoorNo;//联系人地址门牌号
            s[60] = Patient.AddressHomeDoorNo;//家庭住址门牌号
            s[61] = Patient.Email; //email地址
            return s;
        }

        /// <summary>
        /// 获取患者数组59E9D317-38C8-4603-B963-4FB04A522F63
        /// </summary>
        /// <param name="Patient"></param>
        /// <returns></returns>
        private string[] getMyComPatient(PatientInfo Patient)
        {
            string[] s = new string[63];
            s[0] = Patient.PID.CardNO; //就诊卡号
            s[1] = Patient.Name; //姓名
            s[2] = Patient.SpellCode; //拼音码
            s[3] = Patient.WBCode; //五笔
            s[4] = Patient.Birthday.ToString(); //出生日期
            s[5] = Patient.Sex.ID.ToString(); //性别
            s[6] = Patient.IDCard; //身份证号
            s[7] = Patient.BloodType.ID.ToString(); //血型
            s[8] = Patient.Profession.ID; //职业
            s[9] = Patient.CompanyName; //工作单位
            s[10] = Patient.PhoneBusiness; //单位电话
            s[11] = Patient.BusinessZip; //单位邮编
            s[12] = Patient.AddressHome; //户口或家庭所在
            s[13] = Patient.PhoneHome; //家庭电话
            s[14] = Patient.HomeZip; //户口或家庭邮政编码
            s[15] = Patient.DIST; //籍贯
            s[16] = Patient.Nationality.ID; //民族
            s[17] = Patient.Kin.Name; //联系人姓名
            s[18] = Patient.Kin.RelationPhone; //联系人电话
            s[19] = Patient.Kin.RelationAddress; //联系人住址
            s[20] = Patient.Kin.Relation.ID; //联系人关系
            s[21] = Patient.MaritalStatus.ID.ToString(); //婚姻状况
            s[22] = Patient.Country.ID; //国籍
            s[23] = Patient.Pact.PayKind.ID; //结算类别
            s[24] = Patient.Pact.PayKind.Name; //结算类别名称
            s[25] = Patient.Pact.ID; //合同代码
            s[26] = Patient.Pact.Name; //合同单位名称
            s[27] = Patient.SSN; //医疗证号
            s[28] = Patient.AreaCode; //出生地
            //s[29] = Patient.FT.TotCost.ToString(); //医疗费用
            s[31] = FS.FrameWork.Function.NConvert.ToInt32(Patient.Disease.IsAlleray).ToString(); //药物过敏
            s[30] = string.Empty; //电脑号
            s[32] = FS.FrameWork.Function.NConvert.ToInt32(Patient.Disease.IsMainDisease).ToString(); //重要疾病
            s[33] = string.Empty; //帐户密码
            s[34] = "0"; //帐户总额
            s[35] = "0"; //上期帐户余额
            s[36] = "0"; //上期银行余额
            s[37] = "0"; //欠费次数
            s[38] = "0"; //欠费金额
            s[39] = string.Empty; //住院来源
            s[40] = GetSysDateTime(); //最近住院日期
            s[41] = Patient.InTimes.ToString(); //住院次数
            s[42] = GetSysDateTime(); //最近出院日期
            s[43] = GetSysDateTime().ToString(); //初诊日期
            s[44] = GetSysDateTime(); //最近挂号日期
            s[45] = "0"; //违约次数
            s[46] = GetSysDateTime(); //结束日期
            s[47] = Patient.Memo; //备注
            s[48] = Operator.ID; //操作员
            s[49] = GetSysDateTime().ToString(); //操作日期
            s[50] = FS.FrameWork.Function.NConvert.ToInt32(Patient.IsEncrypt).ToString();
            s[51] = Patient.NormalName;

            s[52] = Patient.IDCardType.ID;//证件类型
            s[53] = NConvert.ToInt32(Patient.VipFlag).ToString(); //是否Vip
            s[54] = Patient.MatherName;//母亲姓名
            s[55] = NConvert.ToInt32(Patient.IsTreatment).ToString(); //是否急诊患者
            //s[56] = Patient.CaseNO;//病案号
            s[56] = Patient.PID.CaseNO;//病案号
            //{112F6B96-DC1D-4e20-8290-0403A25B443C}
            s[57] = Patient.Insurance.ID;//保险公司编码
            s[58] = Patient.Insurance.Name;//保险公司名称
            s[59] = Patient.Kin.RelationDoorNo;//联系人地址门牌号
            s[60] = Patient.AddressHomeDoorNo;//家庭住址门牌号
            s[61] = Patient.Email; //email地址
            s[62] = Patient.ChildFlag;
            return s;
        }

        /// <summary>
        /// 获得自动生成的卡号， 主要为收费直接输入患者信息时生成。
        /// </summary>
        /// <returns>成功:自动生成的卡号 失败:null </returns>
        public string GetAutoCardNO()
        {
            string cardNO = this.GetSequence("Fee.OutPatient.GetAutoCardNo.Select");

            return cardNO.PadLeft(10, '0');
        }

        #region 患者基本信息

        /// <summary>
        /// 插入病人基本信息表-不是患者主表 表名：com_Patient 
        /// </summary>
        /// <param name="Patient">患者基本信息</param>
        /// <returns>成功标志 0 失败，1 成功</returns>
        /// <returns></returns>
        public int InsertPatient(PatientInfo Patientinfo)
        //修复住院登记信息缺失bug，区别就是patientinfo和patient的父子类应用的问题
        {
            //{DBB05E6E-156D-417f-AAC5-ABFB78DFBA31}
            Patient Patient = (Patient) Patientinfo;

            #region "接口"

            //			0 就诊卡号,          1 姓名,              2 拼音码,             3 五笔,
            //			4 出生日期,          5 性别,              6 身份证号,           7 血型,
            //			8 职业,              9 工作单位,         10 单位电话,          11 单位邮编,
            //			12 户口或家庭所在,   13 家庭电话,         14 户口或家庭邮政编码,15 籍贯,
            //			16 民族,	     17 联系人姓名,       18 联系人电话,        19 联系人住址,
            //			20 联系人关系,       21 婚姻状况,         22 国籍,              23 结算类别,
            //			24 结算类别名称,     25 合同代码,         26 合同单位名称,      27 医疗证号,
            //			28 地区,             29 医疗费用,         30 电脑号,            31 药物过敏,
            //			32 重要疾病,         33 帐户密码,         34 帐户总额,          35 上期帐户余额,
            //			36 上期银行余额,     37 欠费次数,         38 欠费金额,          39 住院来源,
            //			40 最近住院日期,     41 住院次数,         42 最近出院日期,      43 初诊日期,
            //			44 最近挂号日期,     45 违约次数,         46 结束日期,          47 备注,
            //			48 操作员,           49 操作日期

            #endregion

            string strSql = string.Empty;
            if (Sql.GetSql("RADT.InPatient.CreatePatientInfo.1", ref strSql) == -1) return -1;
            try
            {
                string[] s = new string[72];
                try
                {
                    s[0] = Patient.PID.CardNO; //就诊卡号
                    s[1] = Patient.Name; //姓名
                    s[2] = Patient.SpellCode; //拼音码
                    s[3] = Patient.WBCode; //五笔
                    s[4] = Patient.Birthday.ToString(); //出生日期
                    s[5] = Patient.Sex.ID.ToString(); //性别
                    s[6] = Patient.IDCard; //身份证号
                    s[7] = Patient.BloodType.ID.ToString(); //血型
                    s[8] = Patient.Profession.ID; //职业
                    s[9] = Patient.CompanyName; //工作单位
                    s[10] = Patient.PhoneBusiness; //单位电话
                    s[11] = Patient.BusinessZip; //单位邮编
                    s[12] = Patient.AddressHome; //户口或家庭所在
                    s[13] = Patient.PhoneHome; //家庭电话
                    s[14] = Patient.HomeZip; //户口或家庭邮政编码
                    s[15] = Patient.DIST; //籍贯
                    s[16] = Patient.Nationality.ID; //民族
                    s[17] = Patientinfo.Kin.Name; //联系人姓名
                    s[18] = Patientinfo.Kin.RelationPhone; //联系人电话
                    s[19] = Patientinfo.Kin.RelationAddress; //联系人住址
                    s[20] = Patientinfo.Kin.Relation.ID; //联系人关系
                    s[21] = Patient.MaritalStatus.ID.ToString(); //婚姻状况
                    s[22] = Patient.Country.ID; //国籍
                    s[23] = Patient.Pact.PayKind.ID; //结算类别
                    s[24] = Patient.Pact.PayKind.Name; //结算类别名称
                    s[25] = Patient.Pact.ID; //合同代码
                    s[26] = Patient.Pact.Name; //合同单位名称
                    s[27] = Patient.SSN; //医疗证号
                    s[28] = Patient.AreaCode; //出生地
                    //s[29] = Patient.FT.TotCost.ToString(); //医疗费用
                    s[31] = FS.FrameWork.Function.NConvert.ToInt32(Patient.Disease.IsAlleray).ToString(); //药物过敏
                    s[30] = string.Empty; //电脑号
                    s[32] = FS.FrameWork.Function.NConvert.ToInt32(Patient.Disease.IsMainDisease).ToString(); //重要疾病
                    s[33] = string.Empty; //帐户密码
                    s[34] = "0"; //帐户总额
                    s[35] = "0"; //上期帐户余额
                    s[36] = "0"; //上期银行余额
                    s[37] = "0"; //欠费次数
                    s[38] = "0"; //欠费金额
                    s[39] = string.Empty; //住院来源
                    s[40] = string.Empty; //最近住院日期
                    s[41] = Patient.InTimes.ToString(); //住院次数
                    s[42] = string.Empty; //最近出院日期
                    s[43] = GetSysDateTime().ToString(); //初诊日期
                    s[44] = string.Empty; //最近挂号日期
                    s[45] = "0"; //违约次数
                    s[46] = string.Empty; //结束日期
                    s[47] = Patient.Memo; //备注
                    s[48] = Operator.ID; //操作员
                    s[49] = GetSysDateTime().ToString(); //操作日期
                    s[50] = FS.FrameWork.Function.NConvert.ToInt32(Patient.IsEncrypt).ToString();
                    s[51] = Patient.NormalName;

                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    s[52] = Patient.IDCardType.ID;//证件类型
                    s[53] = NConvert.ToInt32(Patient.VipFlag).ToString(); //是否Vip
                    s[54] = Patient.MatherName;//母亲姓名
                    s[55] = NConvert.ToInt32(Patient.IsTreatment).ToString(); //是否急诊患者
                    //s[56] = Patient.CaseNO;//病案号
                    s[56] = Patient.PID.CaseNO;//病案号
                    //{112F6B96-DC1D-4e20-8290-0403A25B443C}
                    s[57] = Patient.Insurance.ID; //保险公司编码
                    s[58] = Patient.Insurance.Name; //保险公司名称
                    s[59] = Patient.Kin.RelationDoorNo;//联系人地址门牌号
                    s[60] = Patient.AddressHomeDoorNo;//家庭住址门牌号
                    s[61] = Patient.Email;//email地址


                    s[62] = "";//现住址// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                    s[63] = "";//家庭号
                    s[64] = "";//其他卡号
                    s[65] = "";//客服专员
                    s[66] = "";//
                    s[67] = "";//患者来源
                    s[68] = "";//转诊人
                    s[69] = "";//开发渠道

                    s[70] = "";//家庭号

                    //{05024624-3FF4-44B5-92BF-BD4C6FAF6897}添加儿童标签 儿童的必须填写联系人电话 非儿童则必须填写联系电话 且联系电话不能重复
                    s[71] = Patient.ChildFlag;


                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                }
                //strSql = string.Format(strSql, s);
                //{11E484F5-B92A-4903-8C8A-4920908B4D0A}
                int result =  ExecNoQuery(strSql, s);

                /*
                if (result >= 0)
                {
                    //FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService();

                }
                 * */


                return result;
            }
            catch (Exception ex)
            {
                Err = "赋值时候出错！" + ex.Message;
                WriteErr();
                return -1;
            }

        }

        /// <summary>
        /// 更新基本信息表－不是患者主表  表名：com_Patient
        /// </summary>
        /// <param name="Patient"></param>
        /// <returns></returns>
        public int UpdatePatient(Patient Patient)
        {
            return base.UpdateSingleTable("RADT.InPatient.UpdatePatientInfo.1", this.getMyComPatient(Patient));

        }

        /// <summary>
        /// 更新患者信息（给住院用）
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int UpdatePatientForInpatient(Patient patient)
        {
            return base.UpdateSingleTable("RADT.InPatient.UpdatePatientInfo.2", this.getMyComPatient(patient));
        }

        /// <summary>
        /// 更新患者信息（给住院用）//{59E9D317-38C8-4603-B963-4FB04A522F63} 有父类接收参数导致很多数据为空
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int UpdatePatientForInpatient1(PatientInfo patient)
        {
            return base.UpdateSingleTable("RADT.InPatient.UpdatePatientInfo.2", this.getMyComPatient(patient));
        }

        /// <summary>
        /// 获取患者姓名
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.Patient GetPatient(string CardNO)
        {
            FS.HISFC.Models.RADT.Patient obj = new Patient();
            string strSql = "";
            if (this.Sql.GetSql("RADT.InPatient.GetPatient.1", ref strSql) == -1) return null;
            strSql = string.Format(strSql, CardNO);
            if (this.ExecQuery(strSql) == -1) return null;
            while (this.Reader.Read())
            {
                obj.PID.CardNO = Reader[0].ToString(); //卡号 
                obj.Name = Reader[1].ToString();//姓名
            }
            this.Reader.Close();
            return obj;
        }

        /// <summary>
        /// 患者基本信息查询  com_Patient
        /// </summary>
        /// <param name="cardNO">卡号</param>
        /// <returns></returns>
        public Patient QueryComPatient(string cardNO)
        {
            Patient Patient = new Patient();
            string sql = string.Empty;
            if (Sql.GetSql("RADT.Inpatient.PatientInfoQuery.1", ref sql) == -1)
            #region SQL
            /*SELECT card_no,
						   name,   --姓名
								   spell_code,   --拼音码

								   wb_code,   --五笔
								   birthday,   --出生日期
								   sex_code,   --性别
								   idenno,   --身份证号
								   blood_code,   --血型

								   prof_code,   --职业
								   work_home,   --工作单位
								   work_tel,   --单位电话
								   work_zip,   --单位邮编
								   home,   --户口或家庭所在

								   home_tel,   --家庭电话
								   home_zip,   --户口或家庭邮政编码

								   district,   --籍贯
								   nation_code,   --民族
								   linkman_name,   --联系人姓名

								   linkman_tel,   --联系人电话

								   linkman_add,   --联系人住址
								   rela_code,   --联系人关系

								   mari,   --婚姻状况
								   coun_code,   --国籍
								   paykind_code,   --结算类别
								   paykind_name,   --结算类别名称
								   pact_code,   --合同代码
								   pact_name,   --合同单位名称
								   mcard_no,   --医疗证号
								   area_code,   --出生地

								   framt,   --医疗费用
								   ic_cardno,   --电脑号

								   anaphy_flag,   --药物过敏
								   hepatitis_flag,   --重要疾病
								   act_code,   --帐户密码
								   act_amt,   --帐户总额
								   lact_sum,   --上期帐户余额
								   lbank_sum,   --上期银行余额
								   arrear_times,   --欠费次数
								   arrear_sum,   --欠费金额
								   inhos_source,   --住院来源
								   lihos_date,   --最近住院日期

								   inhos_times,   --住院次数
								   louthos_date,   --最近出院日期

								   fir_see_date,   --初诊日期
								   lreg_date,   --最近挂号日期

								   disoby_cnt,   --违约次数
								   end_date,   --结束日期
								   mark,   --备注
								   oper_code,   --操作员

								   oper_date    --操作日期
							  FROM com_Patient   --病人基本信息表
							 WHERE PARENT_CODE='[父级编码]'  and 
								   CURRENT_CODE='[本级编码]' and 
								   card_no='{0}'
								   */
            #endregion
            {
                return null;
            }
            sql = string.Format(sql, cardNO);

            if (ExecQuery(sql) < 0)
            {
                return null;
            }

            if (Reader.Read())
            {
                try
                {
                    if (!Reader.IsDBNull(0)) Patient.PID.CardNO = Reader[0].ToString(); //就诊卡号
                    if (!Reader.IsDBNull(1)) Patient.Name = Reader[1].ToString(); //姓名
                    if (!Reader.IsDBNull(2)) Patient.SpellCode = Reader[2].ToString(); //拼音码
                    if (!Reader.IsDBNull(3)) Patient.WBCode = Reader[3].ToString(); //五笔
                    if (!Reader.IsDBNull(4)) Patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //出生日期
                    if (!Reader.IsDBNull(5)) Patient.Sex.ID = Reader[5].ToString(); //性别
                    if (!Reader.IsDBNull(6)) Patient.IDCard = Reader[6].ToString(); //身份证号
                    if (!Reader.IsDBNull(7)) Patient.BloodType.ID = Reader[7].ToString(); //血型
                    if (!Reader.IsDBNull(8)) Patient.Profession.ID = Reader[8].ToString(); //职业
                    if (!Reader.IsDBNull(9)) Patient.CompanyName = Reader[9].ToString(); //工作单位
                    if (!Reader.IsDBNull(10)) Patient.PhoneBusiness = Reader[10].ToString(); //单位电话
                    if (!Reader.IsDBNull(11)) Patient.BusinessZip = Reader[11].ToString(); //单位邮编
                    if (!Reader.IsDBNull(12)) Patient.AddressHome = Reader[12].ToString(); //户口或家庭所在
                    if (!Reader.IsDBNull(13)) Patient.PhoneHome = Reader[13].ToString(); //家庭电话
                    if (!Reader.IsDBNull(14)) Patient.HomeZip = Reader[14].ToString(); //户口或家庭邮政编码
                    if (!Reader.IsDBNull(15)) Patient.DIST = Reader[15].ToString(); //籍贯
                    if (!Reader.IsDBNull(16)) Patient.Nationality.ID = Reader[16].ToString(); //民族
                    if (!Reader.IsDBNull(17)) Patient.Kin.Name = Reader[17].ToString(); //联系人姓名
                    if (!Reader.IsDBNull(18)) Patient.Kin.RelationPhone = Reader[18].ToString(); //联系人电话
                    if (!Reader.IsDBNull(19)) Patient.Kin.RelationAddress = Reader[19].ToString(); //联系人住址
                    if (!Reader.IsDBNull(20)) Patient.Kin.Relation.ID = Reader[20].ToString(); //联系人关系
                    if (!Reader.IsDBNull(21)) Patient.MaritalStatus.ID = Reader[21].ToString(); //婚姻状况
                    if (!Reader.IsDBNull(22)) Patient.Country.ID = Reader[22].ToString(); //国籍
                    if (!Reader.IsDBNull(23)) Patient.Pact.PayKind.ID = Reader[23].ToString(); //结算类别
                    if (!Reader.IsDBNull(24)) Patient.Pact.PayKind.Name = Reader[24].ToString(); //结算类别名称
                    if (!Reader.IsDBNull(25)) Patient.Pact.ID = Reader[25].ToString(); //合同代码
                    if (!Reader.IsDBNull(26)) Patient.Pact.Name = Reader[26].ToString(); //合同单位名称
                    if (!Reader.IsDBNull(27)) Patient.SSN = Reader[27].ToString(); //医疗证号
                    if (!Reader.IsDBNull(28)) Patient.AreaCode = Reader[28].ToString(); //地区
                    //if (!Reader.IsDBNull(29)) Patient.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //医疗费用
                    if (!Reader.IsDBNull(30)) Patient.Card.ICCard.ID = Reader[30].ToString(); //电脑号
                    if (!Reader.IsDBNull(31)) Patient.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //药物过敏
                    if (!Reader.IsDBNull(32)) Patient.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //重要疾病
                    if (!Reader.IsDBNull(33)) Patient.Card.NewPassword = Reader[33].ToString(); //帐户密码
                    if (!Reader.IsDBNull(34)) Patient.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //帐户总额
                    if (!Reader.IsDBNull(35)) Patient.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //上期帐户余额
                    //					if (!this.Reader.IsDBNull(36)) Patient=this.Reader[36].ToString();//上期银行余额
                    //					if (!this.Reader.IsDBNull(37)) Patient=this.Reader[37].ToString();//欠费次数
                    //					if (!this.Reader.IsDBNull(38)) Patient=this.Reader[38].ToString();//欠费金额
                    //					if (!this.Reader.IsDBNull(39)) Patient=this.Reader[39].ToString();//住院来源
                    //					if (!this.Reader.IsDBNull(40)) Patient=this.Reader[40].ToString();//最近住院日期
                    //					if (!this.Reader.IsDBNull(41)) Patient=this.Reader[41].ToString();//住院次数
                    //					if (!this.Reader.IsDBNull(42)) Patient=this.Reader[42].ToString();//最近出院日期
                    //					if (!this.Reader.IsDBNull(43)) Patient=this.Reader[43].ToString();//初诊日期
                    //					if (!this.Reader.IsDBNull(44)) Patient=this.Reader[44].ToString();//最近挂号日期
                    //					if (!this.Reader.IsDBNull(45)) Patient=this.Reader[45].ToString();//违约次数
                    //					if (!this.Reader.IsDBNull(46)) Patient=this.Reader[46].ToString();//结束日期
                    if (!Reader.IsDBNull(47)) Patient.Memo = Reader[47].ToString(); //备注
                    if (!Reader.IsDBNull(48)) Patient.User01 = Reader[48].ToString(); //操作员
                    if (!Reader.IsDBNull(49)) Patient.User02 = Reader[49].ToString(); //操作日期
                    if (!Reader.IsDBNull(50)) Patient.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) Patient.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) Patient.IDCardType.ID = Reader[52].ToString();//证件类型
                    if (!Reader.IsDBNull(53)) Patient.VipFlag = NConvert.ToBoolean(Reader[53]);//vip标识
                    if (!Reader.IsDBNull(54)) Patient.MatherName = Reader[54].ToString();//母亲姓名
                    if (!Reader.IsDBNull(55)) Patient.IsTreatment = NConvert.ToBoolean(Reader[55]);//是否急诊
                    if (!Reader.IsDBNull(56)) Patient.PID.CaseNO = Reader[56].ToString();//病案号
                    if (Patient.IsEncrypt && Patient.NormalName != string.Empty)
                    {
                        Patient.DecryptName = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(Patient.NormalName);
                    }
                    if (!Reader.IsDBNull(57)) Patient.Insurance.ID = Reader[57].ToString(); //保险公司编码
                    if (!Reader.IsDBNull(58)) Patient.Insurance.Name = Reader[58].ToString(); //保险公司名称
                    if (!Reader.IsDBNull(59)) Patient.AddressHomeDoorNo = Reader[59].ToString(); //家庭住址门牌号
                    if (!Reader.IsDBNull(60)) Patient.Kin.RelationDoorNo = Reader[60].ToString(); //联系人地址门牌号
                    if (!Reader.IsDBNull(61)) Patient.Email = Reader[61].ToString(); //email
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();

            return Patient;
        }

        /// <summary>
        /// 根据身份证号获取患者信息
        /// </summary>
        /// <param name="idNO">身份证号</param>
        /// <returns></returns>
        public Patient QueryComPatientByIDNO(string IDNO)
        {
            Patient Patient = new Patient();
            string sql = string.Empty;
            if (Sql.GetSql("RADT.Inpatient.PatientInfoQuerybyIDNO", ref sql) == -1)
            #region SQL
            /*SELECT card_no,
						   name,   --姓名
								   spell_code,   --拼音码

								   wb_code,   --五笔
								   birthday,   --出生日期
								   sex_code,   --性别
								   idenno,   --身份证号
								   blood_code,   --血型

								   prof_code,   --职业
								   work_home,   --工作单位
								   work_tel,   --单位电话
								   work_zip,   --单位邮编
								   home,   --户口或家庭所在

								   home_tel,   --家庭电话
								   home_zip,   --户口或家庭邮政编码

								   district,   --籍贯
								   nation_code,   --民族
								   linkman_name,   --联系人姓名

								   linkman_tel,   --联系人电话

								   linkman_add,   --联系人住址
								   rela_code,   --联系人关系

								   mari,   --婚姻状况
								   coun_code,   --国籍
								   paykind_code,   --结算类别
								   paykind_name,   --结算类别名称
								   pact_code,   --合同代码
								   pact_name,   --合同单位名称
								   mcard_no,   --医疗证号
								   area_code,   --出生地

								   framt,   --医疗费用
								   ic_cardno,   --电脑号

								   anaphy_flag,   --药物过敏
								   hepatitis_flag,   --重要疾病
								   act_code,   --帐户密码
								   act_amt,   --帐户总额
								   lact_sum,   --上期帐户余额
								   lbank_sum,   --上期银行余额
								   arrear_times,   --欠费次数
								   arrear_sum,   --欠费金额
								   inhos_source,   --住院来源
								   lihos_date,   --最近住院日期

								   inhos_times,   --住院次数
								   louthos_date,   --最近出院日期

								   fir_see_date,   --初诊日期
								   lreg_date,   --最近挂号日期

								   disoby_cnt,   --违约次数
								   end_date,   --结束日期
								   mark,   --备注
								   oper_code,   --操作员

								   oper_date    --操作日期
							  FROM com_Patient   --病人基本信息表
							 WHERE PARENT_CODE='[父级编码]'  and 
								   CURRENT_CODE='[本级编码]' and 
								   card_no='{0}'
								   */
            #endregion
            {
                return null;
            }
            sql = string.Format(sql, IDNO);

            if (ExecQuery(sql) < 0)
            {
                return null;
            }

            if (Reader.Read())
            {
                try
                {
                    if (!Reader.IsDBNull(0)) Patient.PID.CardNO = Reader[0].ToString(); //就诊卡号
                    if (!Reader.IsDBNull(1)) Patient.Name = Reader[1].ToString(); //姓名
                    if (!Reader.IsDBNull(2)) Patient.SpellCode = Reader[2].ToString(); //拼音码
                    if (!Reader.IsDBNull(3)) Patient.WBCode = Reader[3].ToString(); //五笔
                    if (!Reader.IsDBNull(4)) Patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //出生日期
                    if (!Reader.IsDBNull(5)) Patient.Sex.ID = Reader[5].ToString(); //性别
                    if (!Reader.IsDBNull(6)) Patient.IDCard = Reader[6].ToString(); //身份证号
                    if (!Reader.IsDBNull(7)) Patient.BloodType.ID = Reader[7].ToString(); //血型
                    if (!Reader.IsDBNull(8)) Patient.Profession.ID = Reader[8].ToString(); //职业
                    if (!Reader.IsDBNull(9)) Patient.CompanyName = Reader[9].ToString(); //工作单位
                    if (!Reader.IsDBNull(10)) Patient.PhoneBusiness = Reader[10].ToString(); //单位电话
                    if (!Reader.IsDBNull(11)) Patient.BusinessZip = Reader[11].ToString(); //单位邮编
                    if (!Reader.IsDBNull(12)) Patient.AddressHome = Reader[12].ToString(); //户口或家庭所在
                    if (!Reader.IsDBNull(13)) Patient.PhoneHome = Reader[13].ToString(); //家庭电话
                    if (!Reader.IsDBNull(14)) Patient.HomeZip = Reader[14].ToString(); //户口或家庭邮政编码
                    if (!Reader.IsDBNull(15)) Patient.DIST = Reader[15].ToString(); //籍贯
                    if (!Reader.IsDBNull(16)) Patient.Nationality.ID = Reader[16].ToString(); //民族
                    if (!Reader.IsDBNull(17)) Patient.Kin.Name = Reader[17].ToString(); //联系人姓名
                    if (!Reader.IsDBNull(18)) Patient.Kin.RelationPhone = Reader[18].ToString(); //联系人电话
                    if (!Reader.IsDBNull(19)) Patient.Kin.RelationAddress = Reader[19].ToString(); //联系人住址
                    if (!Reader.IsDBNull(20)) Patient.Kin.Relation.ID = Reader[20].ToString(); //联系人关系
                    if (!Reader.IsDBNull(21)) Patient.MaritalStatus.ID = Reader[21].ToString(); //婚姻状况
                    if (!Reader.IsDBNull(22)) Patient.Country.ID = Reader[22].ToString(); //国籍
                    if (!Reader.IsDBNull(23)) Patient.Pact.PayKind.ID = Reader[23].ToString(); //结算类别
                    if (!Reader.IsDBNull(24)) Patient.Pact.PayKind.Name = Reader[24].ToString(); //结算类别名称
                    if (!Reader.IsDBNull(25)) Patient.Pact.ID = Reader[25].ToString(); //合同代码
                    if (!Reader.IsDBNull(26)) Patient.Pact.Name = Reader[26].ToString(); //合同单位名称
                    if (!Reader.IsDBNull(27)) Patient.SSN = Reader[27].ToString(); //医疗证号
                    if (!Reader.IsDBNull(28)) Patient.AreaCode = Reader[28].ToString(); //地区
                    //if (!Reader.IsDBNull(29)) Patient.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //医疗费用
                    if (!Reader.IsDBNull(30)) Patient.Card.ICCard.ID = Reader[30].ToString(); //电脑号
                    if (!Reader.IsDBNull(31)) Patient.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //药物过敏
                    if (!Reader.IsDBNull(32)) Patient.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //重要疾病
                    if (!Reader.IsDBNull(33)) Patient.Card.NewPassword = Reader[33].ToString(); //帐户密码
                    if (!Reader.IsDBNull(34)) Patient.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //帐户总额
                    if (!Reader.IsDBNull(35)) Patient.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //上期帐户余额
                    //					if (!this.Reader.IsDBNull(36)) Patient=this.Reader[36].ToString();//上期银行余额
                    //					if (!this.Reader.IsDBNull(37)) Patient=this.Reader[37].ToString();//欠费次数
                    //					if (!this.Reader.IsDBNull(38)) Patient=this.Reader[38].ToString();//欠费金额
                    //					if (!this.Reader.IsDBNull(39)) Patient=this.Reader[39].ToString();//住院来源
                    //					if (!this.Reader.IsDBNull(40)) Patient=this.Reader[40].ToString();//最近住院日期
                    //					if (!this.Reader.IsDBNull(41)) Patient=this.Reader[41].ToString();//住院次数
                    //					if (!this.Reader.IsDBNull(42)) Patient=this.Reader[42].ToString();//最近出院日期
                    //					if (!this.Reader.IsDBNull(43)) Patient=this.Reader[43].ToString();//初诊日期
                    //					if (!this.Reader.IsDBNull(44)) Patient=this.Reader[44].ToString();//最近挂号日期
                    //					if (!this.Reader.IsDBNull(45)) Patient=this.Reader[45].ToString();//违约次数
                    //					if (!this.Reader.IsDBNull(46)) Patient=this.Reader[46].ToString();//结束日期
                    if (!Reader.IsDBNull(47)) Patient.Memo = Reader[47].ToString(); //备注
                    if (!Reader.IsDBNull(48)) Patient.User01 = Reader[48].ToString(); //操作员
                    if (!Reader.IsDBNull(49)) Patient.User02 = Reader[49].ToString(); //操作日期
                    if (!Reader.IsDBNull(50)) Patient.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) Patient.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) Patient.IDCardType.ID = Reader[52].ToString();//证件类型
                    if (!Reader.IsDBNull(53)) Patient.VipFlag = NConvert.ToBoolean(Reader[53]);//vip标识
                    if (!Reader.IsDBNull(54)) Patient.MatherName = Reader[54].ToString();//母亲姓名
                    if (!Reader.IsDBNull(55)) Patient.IsTreatment = NConvert.ToBoolean(Reader[55]);//是否急诊
                    if (!Reader.IsDBNull(56)) Patient.PID.CaseNO = Reader[56].ToString();//病案号
                    if (Patient.IsEncrypt && Patient.NormalName != string.Empty)
                    {
                        Patient.DecryptName = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(Patient.NormalName);
                    }
                    if (!Reader.IsDBNull(57)) Patient.Insurance.ID = Reader[57].ToString(); //保险公司编码
                    if (!Reader.IsDBNull(58)) Patient.Insurance.Name = Reader[58].ToString(); //保险公司名称
                    if (!Reader.IsDBNull(59)) Patient.Kin.RelationDoorNo = Reader[59].ToString(); //联系人地址门牌号
                    if (!Reader.IsDBNull(60)) Patient.AddressHomeDoorNo = Reader[60].ToString(); //家庭住址门牌号
                    if (!Reader.IsDBNull(61)) Patient.Email = Reader[61].ToString(); //email
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();

            return Patient;
        }

        /// <summary>
        /// 根据医保编号查询患者基本信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public Patient QueryComPatientByMcardNO(string cardNO)
        {
            Patient Patient = new Patient();
            string sql = string.Empty;
            if (Sql.GetSql("RADT.Inpatient.PatientInfoQuery.96", ref sql) == -1)
            {
                return null;
            }
            sql = string.Format(sql, cardNO);

            if (ExecQuery(sql) < 0)
            {
                return null;
            }

            if (Reader.Read())
            {
                try
                {
                    if (!Reader.IsDBNull(0)) Patient.PID.CardNO = Reader[0].ToString(); //就诊卡号
                    if (!Reader.IsDBNull(1)) Patient.Name = Reader[1].ToString(); //姓名
                    if (!Reader.IsDBNull(2)) Patient.SpellCode = Reader[2].ToString(); //拼音码
                    if (!Reader.IsDBNull(3)) Patient.WBCode = Reader[3].ToString(); //五笔
                    if (!Reader.IsDBNull(4)) Patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //出生日期
                    if (!Reader.IsDBNull(5)) Patient.Sex.ID = Reader[5].ToString(); //性别
                    if (!Reader.IsDBNull(6)) Patient.IDCard = Reader[6].ToString(); //身份证号
                    if (!Reader.IsDBNull(7)) Patient.BloodType.ID = Reader[7].ToString(); //血型
                    if (!Reader.IsDBNull(8)) Patient.Profession.ID = Reader[8].ToString(); //职业
                    if (!Reader.IsDBNull(9)) Patient.CompanyName = Reader[9].ToString(); //工作单位
                    if (!Reader.IsDBNull(10)) Patient.PhoneBusiness = Reader[10].ToString(); //单位电话
                    if (!Reader.IsDBNull(11)) Patient.BusinessZip = Reader[11].ToString(); //单位邮编
                    if (!Reader.IsDBNull(12)) Patient.AddressHome = Reader[12].ToString(); //户口或家庭所在
                    if (!Reader.IsDBNull(13)) Patient.PhoneHome = Reader[13].ToString(); //家庭电话
                    if (!Reader.IsDBNull(14)) Patient.HomeZip = Reader[14].ToString(); //户口或家庭邮政编码
                    if (!Reader.IsDBNull(15)) Patient.DIST = Reader[15].ToString(); //籍贯
                    if (!Reader.IsDBNull(16)) Patient.Nationality.ID = Reader[16].ToString(); //民族
                    if (!Reader.IsDBNull(17)) Patient.Kin.Name = Reader[17].ToString(); //联系人姓名
                    if (!Reader.IsDBNull(18)) Patient.Kin.RelationPhone = Reader[18].ToString(); //联系人电话
                    if (!Reader.IsDBNull(19)) Patient.Kin.RelationAddress = Reader[19].ToString(); //联系人住址
                    if (!Reader.IsDBNull(20)) Patient.Kin.Relation.ID = Reader[20].ToString(); //联系人关系
                    if (!Reader.IsDBNull(21)) Patient.MaritalStatus.ID = Reader[21].ToString(); //婚姻状况
                    if (!Reader.IsDBNull(22)) Patient.Country.ID = Reader[22].ToString(); //国籍
                    if (!Reader.IsDBNull(23)) Patient.Pact.PayKind.ID = Reader[23].ToString(); //结算类别
                    if (!Reader.IsDBNull(24)) Patient.Pact.PayKind.Name = Reader[24].ToString(); //结算类别名称
                    if (!Reader.IsDBNull(25)) Patient.Pact.ID = Reader[25].ToString(); //合同代码
                    if (!Reader.IsDBNull(26)) Patient.Pact.Name = Reader[26].ToString(); //合同单位名称
                    if (!Reader.IsDBNull(27)) Patient.SSN = Reader[27].ToString(); //医疗证号
                    if (!Reader.IsDBNull(28)) Patient.AreaCode = Reader[28].ToString(); //地区
                    //if (!Reader.IsDBNull(29)) Patient.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //医疗费用
                    if (!Reader.IsDBNull(30)) Patient.Card.ICCard.ID = Reader[30].ToString(); //电脑号
                    if (!Reader.IsDBNull(31)) Patient.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //药物过敏
                    if (!Reader.IsDBNull(32)) Patient.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //重要疾病
                    if (!Reader.IsDBNull(33)) Patient.Card.NewPassword = Reader[33].ToString(); //帐户密码
                    if (!Reader.IsDBNull(34)) Patient.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //帐户总额
                    if (!Reader.IsDBNull(35)) Patient.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //上期帐户余额
                    //					if (!this.Reader.IsDBNull(36)) Patient=this.Reader[36].ToString();//上期银行余额
                    //					if (!this.Reader.IsDBNull(37)) Patient=this.Reader[37].ToString();//欠费次数
                    //					if (!this.Reader.IsDBNull(38)) Patient=this.Reader[38].ToString();//欠费金额
                    //					if (!this.Reader.IsDBNull(39)) Patient=this.Reader[39].ToString();//住院来源
                    //					if (!this.Reader.IsDBNull(40)) Patient=this.Reader[40].ToString();//最近住院日期
                    //					if (!this.Reader.IsDBNull(41)) Patient=this.Reader[41].ToString();//住院次数
                    //					if (!this.Reader.IsDBNull(42)) Patient=this.Reader[42].ToString();//最近出院日期
                    //					if (!this.Reader.IsDBNull(43)) Patient=this.Reader[43].ToString();//初诊日期
                    //					if (!this.Reader.IsDBNull(44)) Patient=this.Reader[44].ToString();//最近挂号日期
                    //					if (!this.Reader.IsDBNull(45)) Patient=this.Reader[45].ToString();//违约次数
                    //					if (!this.Reader.IsDBNull(46)) Patient=this.Reader[46].ToString();//结束日期
                    if (!Reader.IsDBNull(47)) Patient.Memo = Reader[47].ToString(); //备注
                    if (!Reader.IsDBNull(48)) Patient.User01 = Reader[48].ToString(); //操作员
                    if (!Reader.IsDBNull(49)) Patient.User02 = Reader[49].ToString(); //操作日期
                    if (!Reader.IsDBNull(50)) Patient.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());
                    if (!Reader.IsDBNull(51)) Patient.NormalName = Reader[51].ToString();
                    //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
                    if (!Reader.IsDBNull(52)) Patient.IDCardType.ID = Reader[52].ToString();//证件类型
                    if (!Reader.IsDBNull(53)) Patient.VipFlag = NConvert.ToBoolean(Reader[53]);//vip标识
                    if (!Reader.IsDBNull(54)) Patient.MatherName = Reader[54].ToString();//母亲姓名
                    if (!Reader.IsDBNull(55)) Patient.IsTreatment = NConvert.ToBoolean(Reader[55]);//是否急诊
                    if (!Reader.IsDBNull(56)) Patient.PID.CaseNO = Reader[56].ToString();//病案号
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    WriteErr();
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }
                    return null;
                }
            }

            Reader.Close();

            return Patient;
        }

        /// <summary>
        /// 按照姓名查询患者信息
        /// </summary>
        /// <param name="name">患者姓名</param>
        /// <returns></returns>
        public ArrayList QueryComPatientByName(string name)
        {
            return this.queryComPatient("RADT.Inpatient.PatientInfoQuery.Where1", name);
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="idenType">证件类型</param>
        /// <param name="idenNO">证件号</param>
        /// <returns></returns>
        public ArrayList QueryComPatientByIdenInfo(string idenType, string idenNO)
        {
            return this.queryComPatient("RADT.Inpatient.PatientInfoQuery.Where2", idenType, idenNO);
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="idenType">证件类型</param>
        /// <param name="idenNO">证件号</param>
        /// <returns></returns>
        public ArrayList QueryComPatientByIdenInfoAndName(string idenType, string idenNO, string name)
        {
            return this.queryComPatient("RADT.Inpatient.PatientInfoQuery.Where3", idenType, idenNO, name);
        }

        #endregion

        #region 患者证件信息

        /// <summary>
        /// 插入患者证件信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int InsertIdenInfo(FS.HISFC.Models.RADT.Patient p)
        {
            return this.updateSingTable("Fee.Account.InsertIdenInfo", p.PID.CardNO, p.IDCardType.ID, p.IDCardType.Name, p.IDCard);
        }

        /// <summary>
        /// 更新患者证件信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int UpdateIdenInfo(FS.HISFC.Models.RADT.Patient p)
        {
            return this.updateSingTable("Fee.Account.UpdateIdenInfo", p.PID.CardNO, p.IDCardType.ID, p.IDCardType.Name, p.IDCard);
        }

        /// <summary>
        /// 证件信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList QueryIdenInfo(string cardNO)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.QueryIdenInfo", ref sql) == -1)
            {
                this.Err = "查询索引为Fee.Account.QueryIdenInfo的SQL语句失败！";
                return null;
            }
            return this.getPatientIdenInfo(sql, cardNO);

        }

        /// <summary>
        /// 查询患者证件信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList getPatientIdenInfo(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }
            ArrayList al = new ArrayList();
            NeuObject obj = null;
            while (this.Reader.Read())
            {
                obj = new NeuObject();
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
                obj.User01 = this.Reader[2].ToString();
                obj.User02 = this.Reader[3].ToString();
                al.Add(obj);
            }
            return al;
        }

        /// <summary>
        /// 更新单表(update、insert)
        /// </summary>
        /// <param name="sqlIndex">sql索引</param>
        /// <param name="args">where条件参数</param>
        /// <returns>1成功 -1失败 0没有更新到记录</returns>
        private int updateSingTable(string sqlIndex, params string[] args)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql(sqlIndex, ref strSql) == -1)
            {
                this.Err = "查找索引为" + sqlIndex + "的Sql语句失败！";
                return -1;
            }
            return this.ExecNoQuery(strSql, args);
        }

        /// <summary>
        /// 根据CardNO和证件类型更新照片
        /// </summary>
        /// <param name="p"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public int UpdatePhoto(FS.HISFC.Models.RADT.Patient p, byte[] photo)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.UpdateIdenInfo.Photo", ref strSql) == -1)
            {
                this.Err = "查找索引为Fee.Account.UpdateIdenInfo.Photo的Sql语句失败！";
                return -1;
            }
            return this.InputBlob(string.Format(strSql, p.PID.CardNO, p.IDCardType.ID), photo);
        }

        /// <summary>
        /// 根据CardNO和证件类型获取照片
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="cardType"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        public int GetIdenInfoPhoto(string cardNO, string cardType, ref byte[] photo)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.GetIdenInfo.Photo", ref sql) == -1)
            {
                this.Err = "查询索引为Fee.Account.GetIdenInfo.Photo的SQL语句失败！";
                return -1;
            }

            photo = this.OutputBlob(string.Format(sql, cardNO, cardType));

            return 1;
        }

        #endregion
    }
}
