using System;
using System.Collections.Generic;
using System.Data;
using FS.FrameWork.Models;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Account;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Base;

namespace FS.HISFC.BizLogic.Fee
{
    /// <summary>
    /// ReturnApply<br></br>
    /// [功能描述: 帐户管理]<br></br>
    /// [创 建 者: 路志鹏]<br></br>
    /// [创建时间: 2007-10-01]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class Account : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Account()
        { }

        #region 变量
        /// <summary>
        /// 根据卡规则读取卡号和卡类型
        /// </summary>
        private static IReadMarkNO IreadMarkNO = null;
        #endregion

        #region 私有方法
        /// <summary>
        /// 提取卡信息
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <returns></returns>
        private FS.HISFC.Models.Account.AccountCard GetAccountCardInfo(string Sql)
        {
            FS.HISFC.Models.Account.AccountCard accountCard = null;
            try
            {
                if (this.ExecQuery(Sql) == -1) return null;
                while (this.Reader.Read())
                {
                    accountCard = new FS.HISFC.Models.Account.AccountCard();
                    accountCard.Patient.PID.CardNO = Reader[0].ToString();
                    accountCard.MarkNO = Reader[1].ToString();
                    accountCard.MarkType.ID = Reader[2].ToString();
                    accountCard.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[3]);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return accountCard;
        }

        /// <summary>
        /// 更新单表(update、insert)
        /// </summary>
        /// <param name="sqlIndex">sql索引</param>
        /// <param name="args">where条件参数</param>
        /// <returns>1成功 -1失败 0没有更新到记录</returns>
        private int UpdateSingTable(string sqlIndex, params string[] args)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql(sqlIndex, ref strSql) == -1)
            {
                this.Err = "查找索引为" + sqlIndex + "的Sql语句失败！";
                return -1;
            }
            return this.ExecNoQuery(strSql, args);
        }

        /// <summary>
        /// 预交金属性字符串数组
        /// </summary>
        /// <param name="prePay"></param>
        /// <returns></returns>
        private string[] GetPrePayArgs(PrePay prePay)
        {
            string[] args = new string[] {
                                            prePay.Patient.PID.CardNO,//病历卡号
                                            prePay.HappenNO.ToString(),//发生序号
                                            prePay.Patient.Name,//患者姓名
                                            prePay.InvoiceNO,//发票号
                                            prePay.PayType.ID.ToString(),//支付方式
                                            prePay.BaseCost.ToString(),//预交金额
                                            prePay.Bank.Name,//开户银行
                                            prePay.Bank.Account,//开户帐户
                                            prePay.Bank.InvoiceNO,//pos交易流水号或支票号或汇票号
                                            NConvert.ToInt32(prePay.IsValid).ToString(),//0未日结/1已日结
                                            prePay.BalanceNO,//日结序号
                                            prePay.BalanceOper.ID,//日结人
                                            prePay.BalanceOper.OperTime.ToString(),// 日结时间
                                            ((int)prePay.ValidState).ToString(),//预交金状态
                                            prePay.PrintTimes.ToString(),//重打次数
                                            prePay.OldInvoice,//原票据号
                                            prePay.PrePayOper.ID, //操作员
                                            prePay.AccountNO, //帐号
                                            NConvert.ToInt32(prePay.IsHostory).ToString(),//是否历史数据
                                            prePay.Bank.WorkName ,//开户单位
                                            prePay.DonateCost.ToString() //优惠金额
                                        };
            return args;
        }

        /// <summary>
        /// 预交金属性字符串数组NEW// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="prePay"></param>
        /// <returns></returns>
        private string[] GetPrePayArgsEX(PrePay prePay)
        {
            //{089AE7A4-C045-4782-9709-72F1E4B9A3FF}
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new FS.HISFC.Models.Base.Employee();
            }
            FS.HISFC.Models.Base.Department dept = employee.Dept as FS.HISFC.Models.Base.Department;
            if (dept == null)
            {
                dept = new FS.HISFC.Models.Base.Department();
            }
            if (string.IsNullOrEmpty(prePay.Hospital_id)) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                string hospitalid = dept.HospitalID;
                string hospitalname = dept.HospitalName;
                prePay.Hospital_id = hospitalid;
                prePay.Hospital_name = hospitalname;
            }

            string[] args = null;
            try
            {
                args = new string[] {
                                            prePay.Patient.PID.CardNO,//病历卡号
                                            prePay.HappenNO.ToString(),//发生序号
                                            prePay.Patient.Name,//患者姓名
                                            prePay.InvoiceNO,//发票号
                                            prePay.PayType.ID.ToString(),//支付方式
                                            prePay.BaseCost.ToString(),//预交金额
                                            prePay.Bank.Name,//开户银行
                                            prePay.Bank.Account,//开户帐户
                                            prePay.Bank.InvoiceNO,//pos交易流水号或支票号或汇票号
                                            NConvert.ToInt32(prePay.IsValid).ToString(),//0未日结/1已日结
                                            prePay.BalanceNO,//日结序号
                                            prePay.BalanceOper.ID,//日结人
                                            prePay.BalanceOper.OperTime.ToString(),// 日结时间
                                            ((int)prePay.ValidState).ToString(),//预交金状态
                                            prePay.PrintTimes.ToString(),//重打次数
                                            prePay.OldInvoice,//原票据号
                                            prePay.PrePayOper.ID, //操作员
                                            prePay.AccountNO, //帐号
                                            NConvert.ToInt32(prePay.IsHostory).ToString(),//是否历史数据
                                            prePay.Bank.WorkName ,//开户单位
                                            prePay.DonateCost.ToString(),//赠送金额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9} lfhm
                                            prePay.BaseVacancy.ToString(),//交易后的基本账户余额
                                            prePay.DonateVacancy.ToString(), //交易后的赠送余额
                                            prePay.PrintNo,//打印单据号
                                            prePay.AccountType.ID,//账户类型编码
                                            prePay.Hospital_id,
                                            prePay.Hospital_name// {3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
                                        };
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            return args;
        }
        /// <summary>
        /// 查找患者就诊卡
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private List<AccountCard> GetAccountMarkList(string whereIndex, params string[] args)
        {
            List<FS.HISFC.Models.Account.AccountCard> list = new List<FS.HISFC.Models.Account.AccountCard>();
            try
            {
                string Sql = string.Empty;
                string SqlWhere = string.Empty;
                if (this.Sql.GetCommonSql("Fee.Account.SelectAccountCard", ref Sql) == -1) return null;
                if (this.Sql.GetCommonSql(whereIndex, ref SqlWhere) == -1) return null;
                SqlWhere = string.Format(SqlWhere, args);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1) return null;
                FS.HISFC.Models.Account.AccountCard accountCard = null;

                while (this.Reader.Read())
                {
                    accountCard = new FS.HISFC.Models.Account.AccountCard();
                    accountCard.Patient.PID.CardNO = Reader[0].ToString();
                    accountCard.MarkNO = Reader[1].ToString();
                    accountCard.MarkType.ID = Reader[2].ToString();

                    accountCard.MarkStatus = (MarkOperateTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    if (!Reader.IsDBNull(4))
                    {
                        accountCard.AccountLevel.ID = Reader[4].ToString();// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                    }
                    if (!Reader.IsDBNull(5))
                    {
                        accountCard.BegTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5].ToString());

                    }
                    if (!Reader.IsDBNull(6))
                    {
                        accountCard.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
                    }
                    list.Add(accountCard);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// 初始化动态库
        /// </summary>
        /// <returns></returns>
        private bool InitReadMark()
        {
            //if (IreadMarkNO == null)
            //{
            try
            {
                IreadMarkNO = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(IReadMarkNO)) as IReadMarkNO;

                if (IreadMarkNO == null)
                {
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(@"./ReadMarkNO.dll");
                    if (assembly == null) return false;
                    Type[] vType = assembly.GetTypes();
                    foreach (Type type in vType)
                    {
                        if (type.GetInterface("IReadMarkNO") != null)
                        {
                            System.Runtime.Remoting.ObjectHandle obj = System.Activator.CreateInstance(type.Assembly.ToString(), type.FullName);
                            IreadMarkNO = obj.Unwrap() as IReadMarkNO;
                            break;
                        }
                    }
                }
            }
            catch
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(@"./ReadMarkNO.dll");
                if (assembly == null) return false;
                Type[] vType = assembly.GetTypes();
                foreach (Type type in vType)
                {
                    if (type.GetInterface("IReadMarkNO") != null)
                    {
                        System.Runtime.Remoting.ObjectHandle obj = System.Activator.CreateInstance(type.Assembly.ToString(), type.FullName);
                        IreadMarkNO = obj.Unwrap() as IReadMarkNO;
                        break;
                    }
                }
            }
            // }
            return true;
        }

        /// <summary>
        /// 查找患者信息
        /// </summary>
        /// <param name="Sql">WhereSql语句的索引</param>
        /// <param name="args">Where条件参数</param>
        /// <returns>null失败</returns>
        private List<FS.HISFC.Models.RADT.PatientInfo> GetPatient(string Sql)
        {
            try
            {
                if (this.ExecQuery(Sql) == -1) return null;
                List<FS.HISFC.Models.RADT.PatientInfo> list = new List<FS.HISFC.Models.RADT.PatientInfo>();
                FS.HISFC.Models.RADT.PatientInfo PatientInfo = null;
                while (this.Reader.Read())
                {
                    PatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    #region 患者信息
                    if (!Reader.IsDBNull(0)) PatientInfo.PID.CardNO = Reader[0].ToString(); //就诊卡号
                    if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //姓名
                    if (!Reader.IsDBNull(2)) PatientInfo.SpellCode = Reader[2].ToString(); //拼音码
                    if (!Reader.IsDBNull(3)) PatientInfo.WBCode = Reader[3].ToString(); //五笔
                    if (!Reader.IsDBNull(4)) PatientInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //出生日期
                    if (!Reader.IsDBNull(5)) PatientInfo.Sex.ID = Reader[5].ToString(); //性别
                    if (!Reader.IsDBNull(6)) PatientInfo.IDCard = Reader[6].ToString(); //身份证号
                    if (!Reader.IsDBNull(7)) PatientInfo.BloodType.ID = Reader[7].ToString(); //血型
                    if (!Reader.IsDBNull(8)) PatientInfo.Profession.ID = Reader[8].ToString(); //职业
                    if (!Reader.IsDBNull(9)) PatientInfo.CompanyName = Reader[9].ToString(); //工作单位
                    if (!Reader.IsDBNull(10)) PatientInfo.PhoneBusiness = Reader[10].ToString(); //单位电话
                    if (!Reader.IsDBNull(11)) PatientInfo.BusinessZip = Reader[11].ToString(); //单位邮编
                    if (!Reader.IsDBNull(12)) PatientInfo.AddressHome = Reader[12].ToString(); //户口或家庭所在
                    if (!Reader.IsDBNull(13)) PatientInfo.PhoneHome = Reader[13].ToString(); //家庭电话
                    if (!Reader.IsDBNull(14)) PatientInfo.HomeZip = Reader[14].ToString(); //户口或家庭邮政编码
                    if (!Reader.IsDBNull(15)) PatientInfo.DIST = Reader[15].ToString(); //籍贯
                    if (!Reader.IsDBNull(16)) PatientInfo.Nationality.ID = Reader[16].ToString(); //民族
                    if (!Reader.IsDBNull(17)) PatientInfo.Kin.Name = Reader[17].ToString(); //联系人姓名
                    if (!Reader.IsDBNull(18)) PatientInfo.Kin.RelationPhone = Reader[18].ToString(); //联系人电话
                    if (!Reader.IsDBNull(19)) PatientInfo.Kin.RelationAddress = Reader[19].ToString(); //联系人住址
                    if (!Reader.IsDBNull(20)) PatientInfo.Kin.Relation.ID = Reader[20].ToString(); //联系人关系
                    if (!Reader.IsDBNull(21)) PatientInfo.MaritalStatus.ID = Reader[21].ToString(); //婚姻状况
                    if (!Reader.IsDBNull(22)) PatientInfo.Country.ID = Reader[22].ToString(); //国籍
                    if (!Reader.IsDBNull(23)) PatientInfo.Pact.PayKind.ID = Reader[23].ToString(); //结算类别
                    if (!Reader.IsDBNull(24)) PatientInfo.Pact.PayKind.Name = Reader[24].ToString(); //结算类别名称
                    if (!Reader.IsDBNull(25)) PatientInfo.Pact.ID = Reader[25].ToString(); //合同代码
                    if (!Reader.IsDBNull(26)) PatientInfo.Pact.Name = Reader[26].ToString(); //合同单位名称
                    if (!Reader.IsDBNull(27)) PatientInfo.SSN = Reader[27].ToString(); //医疗证号
                    if (!Reader.IsDBNull(28)) PatientInfo.AreaCode = Reader[28].ToString(); //地区
                    if (!Reader.IsDBNull(29)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //医疗费用
                    if (!Reader.IsDBNull(30)) PatientInfo.Card.ICCard.ID = Reader[30].ToString(); //电脑号
                    if (!Reader.IsDBNull(31)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //药物过敏
                    if (!Reader.IsDBNull(32)) PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //重要疾病
                    if (!Reader.IsDBNull(33)) PatientInfo.Card.NewPassword = Reader[33].ToString(); //帐户密码
                    if (!Reader.IsDBNull(34)) PatientInfo.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //帐户总额
                    if (!Reader.IsDBNull(35)) PatientInfo.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //上期帐户余额
                    if (!Reader.IsDBNull(47)) PatientInfo.Memo = Reader[47].ToString(); //备注
                    if (!Reader.IsDBNull(48)) PatientInfo.User01 = Reader[48].ToString(); //操作员
                    if (!Reader.IsDBNull(49)) PatientInfo.User02 = Reader[49].ToString(); //操作日期
                    if (!Reader.IsDBNull(50)) PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());//是否加密
                    if (!Reader.IsDBNull(51)) PatientInfo.NormalName = Reader[51].ToString(); //密文
                    if (!Reader.IsDBNull(52)) PatientInfo.IDCardType.ID = Reader[52].ToString();//证件类型
                    if (!Reader.IsDBNull(53)) PatientInfo.CrmID = Reader[53].ToString();//crmid{67CE2526-5E7F-4c92-911F-56CA0077679A}
                    //if (!Reader.IsDBNull(53)) PatientInfo.VipFlag = NConvert.ToBoolean(Reader[53]);//vip标识
                    //if (!Reader.IsDBNull(54)) PatientInfo.MatherName = Reader[54].ToString();//母亲姓名
                    //if (!Reader.IsDBNull(55)) PatientInfo.IsTreatment = NConvert.ToBoolean(Reader[55]);//是否急诊
                    //{6036F4C6-9452-4f21-8634-940AACD4B296}
                    //if (!Reader.IsDBNull(56)) PatientInfo.PID.CaseNO = Reader[56].ToString();//病案号
                    #endregion
                    list.Add(PatientInfo);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }

        //{63F68506-F49D-4ed5-92BD-28A52AF54626}
        /// <summary>
        /// 查找患者信息
        /// </summary>
        /// <param name="Sql">WhereSql语句的索引</param>
        /// <returns>null失败</returns>
        private List<FS.HISFC.Models.Account.AccountCard> GetAccountCardList(string Sql)
        {
            try
            {
                if (this.ExecQuery(Sql) == -1) return null;
                List<FS.HISFC.Models.Account.AccountCard> list = new List<AccountCard>();
                FS.HISFC.Models.Account.AccountCard accountCard = null;
                while (this.Reader.Read())
                {
                    accountCard = new AccountCard();
                    #region 患者信息
                    if (!Reader.IsDBNull(0)) accountCard.Patient.PID.CardNO = Reader[0].ToString(); //就诊卡号
                    if (!Reader.IsDBNull(1)) accountCard.Patient.Name = Reader[1].ToString(); //姓名
                    if (!Reader.IsDBNull(2)) accountCard.Patient.SpellCode = Reader[2].ToString(); //拼音码
                    if (!Reader.IsDBNull(3)) accountCard.Patient.WBCode = Reader[3].ToString(); //五笔
                    if (!Reader.IsDBNull(4)) accountCard.Patient.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString()); //出生日期
                    if (!Reader.IsDBNull(5)) accountCard.Patient.Sex.ID = Reader[5].ToString(); //性别
                    if (!Reader.IsDBNull(6)) accountCard.Patient.IDCard = Reader[6].ToString(); //身份证号
                    if (!Reader.IsDBNull(7)) accountCard.Patient.BloodType.ID = Reader[7].ToString(); //血型
                    if (!Reader.IsDBNull(8)) accountCard.Patient.Profession.ID = Reader[8].ToString(); //职业
                    if (!Reader.IsDBNull(9)) accountCard.Patient.CompanyName = Reader[9].ToString(); //工作单位
                    if (!Reader.IsDBNull(10)) accountCard.Patient.PhoneBusiness = Reader[10].ToString(); //单位电话
                    if (!Reader.IsDBNull(11)) accountCard.Patient.BusinessZip = Reader[11].ToString(); //单位邮编
                    if (!Reader.IsDBNull(12)) accountCard.Patient.AddressHome = Reader[12].ToString(); //户口或家庭所在
                    if (!Reader.IsDBNull(13)) accountCard.Patient.PhoneHome = Reader[13].ToString(); //家庭电话
                    if (!Reader.IsDBNull(14)) accountCard.Patient.HomeZip = Reader[14].ToString(); //户口或家庭邮政编码
                    if (!Reader.IsDBNull(15)) accountCard.Patient.DIST = Reader[15].ToString(); //籍贯
                    if (!Reader.IsDBNull(16)) accountCard.Patient.Nationality.ID = Reader[16].ToString(); //民族
                    if (!Reader.IsDBNull(17)) accountCard.Patient.Kin.Name = Reader[17].ToString(); //联系人姓名
                    if (!Reader.IsDBNull(18)) accountCard.Patient.Kin.RelationPhone = Reader[18].ToString(); //联系人电话
                    if (!Reader.IsDBNull(19)) accountCard.Patient.Kin.RelationAddress = Reader[19].ToString(); //联系人住址
                    if (!Reader.IsDBNull(20)) accountCard.Patient.Kin.Relation.ID = Reader[20].ToString(); //联系人关系
                    if (!Reader.IsDBNull(21)) accountCard.Patient.MaritalStatus.ID = Reader[21].ToString(); //婚姻状况
                    if (!Reader.IsDBNull(22)) accountCard.Patient.Country.ID = Reader[22].ToString(); //国籍
                    if (!Reader.IsDBNull(23)) accountCard.Patient.Pact.PayKind.ID = Reader[23].ToString(); //结算类别
                    if (!Reader.IsDBNull(24)) accountCard.Patient.Pact.PayKind.Name = Reader[24].ToString(); //结算类别名称
                    if (!Reader.IsDBNull(25)) accountCard.Patient.Pact.ID = Reader[25].ToString(); //合同代码
                    if (!Reader.IsDBNull(26)) accountCard.Patient.Pact.Name = Reader[26].ToString(); //合同单位名称
                    if (!Reader.IsDBNull(27)) accountCard.Patient.SSN = Reader[27].ToString(); //医疗证号
                    if (!Reader.IsDBNull(28)) accountCard.Patient.AreaCode = Reader[28].ToString(); //地区
                    if (!Reader.IsDBNull(29)) accountCard.Patient.FT.TotCost = NConvert.ToDecimal(Reader[29].ToString()); //医疗费用
                    if (!Reader.IsDBNull(30)) accountCard.Patient.Card.ICCard.ID = Reader[30].ToString(); //电脑号
                    if (!Reader.IsDBNull(31)) accountCard.Patient.Disease.IsAlleray = NConvert.ToBoolean(Reader[31].ToString()); //药物过敏
                    if (!Reader.IsDBNull(32)) accountCard.Patient.Disease.IsMainDisease = NConvert.ToBoolean(Reader[32].ToString()); //重要疾病
                    if (!Reader.IsDBNull(33)) accountCard.Patient.Card.NewPassword = Reader[33].ToString(); //帐户密码
                    if (!Reader.IsDBNull(34)) accountCard.Patient.Card.NewAmount = NConvert.ToDecimal(Reader[34].ToString()); //帐户总额
                    if (!Reader.IsDBNull(35)) accountCard.Patient.Card.OldAmount = NConvert.ToDecimal(Reader[35].ToString()); //上期帐户余额
                    if (!Reader.IsDBNull(47)) accountCard.Patient.Memo = Reader[47].ToString(); //备注
                    if (!Reader.IsDBNull(48)) accountCard.Patient.User01 = Reader[48].ToString(); //操作员
                    if (!Reader.IsDBNull(49)) accountCard.Patient.User02 = Reader[49].ToString(); //操作日期
                    if (!Reader.IsDBNull(50)) accountCard.Patient.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[50].ToString());//是否加密
                    if (!Reader.IsDBNull(51)) accountCard.Patient.NormalName = Reader[51].ToString(); //密文
                    if (!Reader.IsDBNull(52)) accountCard.Patient.IDCardType.ID = Reader[52].ToString();//证件类型
                    //if (!Reader.IsDBNull(53)) accountCard.Patient.VipFlag = NConvert.ToBoolean(Reader[53]);//vip标识
                    //if (!Reader.IsDBNull(54)) accountCard.Patient.MatherName = Reader[54].ToString();//母亲姓名
                    //if (!Reader.IsDBNull(55)) accountCard.Patient.IsTreatment = NConvert.ToBoolean(Reader[55]);//是否急诊
                    if (!Reader.IsDBNull(56)) accountCard.Patient.PID.CaseNO = Reader[56].ToString();//病案号
                    if (!Reader.IsDBNull(57)) accountCard.MarkNO = this.Reader[57].ToString(); //就诊卡号
                    if (!Reader.IsDBNull(58)) accountCard.MarkType.ID = this.Reader[58].ToString(); //卡类型
                    if (!Reader.IsDBNull(59)) accountCard.AccountLevel.ID = this.Reader[59].ToString(); //会员等级
                    if (!Reader.IsDBNull(60)) accountCard.AccountLevel.Name = this.Reader[60].ToString(); //会员等级名称
                    if (!Reader.IsDBNull(61)) accountCard.BegTime = NConvert.ToDateTime(this.Reader[61].ToString()); //开始时间
                    if (!Reader.IsDBNull(62)) accountCard.EndTime = NConvert.ToDateTime(this.Reader[62].ToString()); //截止时间
                    if (!Reader.IsDBNull(63)) accountCard.Patient.User01 = this.Reader[63].ToString(); //现住址

                    #endregion
                    list.Add(accountCard);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }



        /// <summary>
        /// 获取当前院区的所有员工// {3218011F-CCDA-49f3-B468-06F25B3F7F72}
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Base.Employee> GetCurrDeptEmployee(string hospitalID)
        {
            try
            {

                string sqlStr = string.Empty;
                if (this.Sql.GetCommonSql("Fee.Account.GetCurrDeptEmployee", ref sqlStr) == -1)
                {
                    this.Err = "查找索引为Fee.Account.GetCurrDeptEmployee的sql语句失败！";
                    return null;
                }
                sqlStr = string.Format(sqlStr, hospitalID);
                if (this.ExecQuery(sqlStr) == -1) return null;
                List<FS.HISFC.Models.Base.Employee> list = new List<FS.HISFC.Models.Base.Employee>();
                FS.HISFC.Models.Base.Employee employee = null;
                while (this.Reader.Read())
                {
                    employee = new FS.HISFC.Models.Base.Employee();
                    #region 患者信息
                    if (!Reader.IsDBNull(0)) employee.ID = Reader[0].ToString();

                    #endregion
                    list.Add(employee);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }
        /// <summary>
        /// 查找帐户信息NEW lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="whereIndex">where条件索引</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        private FS.HISFC.Models.Account.Account GetAccountEX(string whereIndex, params string[] args)
        {
            string sqlStr = string.Empty;
            string sqlWhere = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccount.1", ref sqlStr) == -1)
            {
                this.Err = "查找索引为Fee.Account.SelectAccount.1的sql语句失败！";
                return null;
            }
            if (this.Sql.GetCommonSql(whereIndex, ref sqlWhere) == -1)
            {
                this.Err = "查找索引为" + whereIndex + "的sql语句失败！";
                return null;
            }
            sqlStr += " " + sqlWhere;
            FS.HISFC.Models.Account.Account account = null;
            try
            {
                sqlStr = string.Format(sqlStr, args);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "查找数据失败！";
                    return null; ;
                }
                while (this.Reader.Read())
                {
                    account = new FS.HISFC.Models.Account.Account();
                    account.CardNO = this.Reader[0].ToString();
                    if (this.Reader[1] != DBNull.Value) account.ValidState = (HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[1]));
                    if (this.Reader[2] != DBNull.Value) account.PassWord = HisDecrypt.Decrypt(this.Reader[2].ToString());
                    if (this.Reader[3] != DBNull.Value) account.DayLimit = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                    account.ID = this.Reader[4].ToString();
                    account.IsEmpower = NConvert.ToBoolean(this.Reader[5]);
                    account.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    account.DonateVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                    account.CouponVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                    account.Limit = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                    account.BaseAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                    account.DonateAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                    account.CouponAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                    account.AccountLevel.ID = this.Reader[13].ToString();// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                    account.AccountLevel.Name = this.Reader[14].ToString();
                    account.CreateEnvironment.ID = this.Reader[15].ToString();
                    account.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[16].ToString());
                    account.OperEnvironment.ID = this.Reader[17].ToString();
                    account.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
                }
                return account;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

        }

        /// <summary>
        /// 根据卡号查询普通账户余额
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public string GetAccountDetailPTYE(string cardNO)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetAccountDetailPTYE", ref sqlStr) == -1)
            {
                this.Err = "查找索引为Fee.Account.GetAccountDetailPTYE的sql语句失败！";
                return null;
            }

            try
            {
                string CKDonateAmout = string.Empty;
                sqlStr = string.Format(sqlStr, cardNO);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "查找数据失败！";
                    return null; ;
                }
                while (this.Reader.Read())
                {
                    CKDonateAmout = this.Reader[0].ToString();
                }
                return CKDonateAmout;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 根据卡号查询普通账户赠送余额
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public string GetAccountDetailPT(string cardNO)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetAccountDetailPT", ref sqlStr) == -1)
            {
                this.Err = "查找索引为Fee.Account.GetAccountDetailPT的sql语句失败！";
                return null;
            }

            try
            {
                string CKDonateAmout = string.Empty;
                sqlStr = string.Format(sqlStr, cardNO);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "查找数据失败！";
                    return null; ;
                }
                while (this.Reader.Read())
                {
                    CKDonateAmout = this.Reader[0].ToString();
                }
                return CKDonateAmout;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }


        /// <summary>
        /// 根据卡号查询产康账户余额
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public string GetAccountDetailCK(string cardNO)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetAccountDetailCK", ref sqlStr) == -1)
            {
                this.Err = "查找索引为Fee.Account.GetAccountDetailCK的sql语句失败！";
                return null;
            }

            try
            {
                string CKDonateAmout = string.Empty;
                sqlStr = string.Format(sqlStr, cardNO);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "查找数据失败！";
                    return null; ;
                }
                while (this.Reader.Read())
                {
                    CKDonateAmout = this.Reader[0].ToString();
                }
                return CKDonateAmout;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 查找帐户预交金信息
        /// </summary>
        /// <param name="whereIndex">WhereSql语句的索引</param>
        /// <param name="args">Where条件参数</param>
        /// <returns>null 失败</returns>
        private List<PrePay> GetPrePayList(string whereIndex, params string[] args)
        {
            string sqlstr = string.Empty;
            string sqlwhere = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetPrePayRecrod", ref sqlstr) < 0)
            {
                this.Err = "索引为Fee.Account.GetPrePayRecrod的SQL语句不存在！";
                return null;
            }
            if (this.Sql.GetCommonSql(whereIndex, ref sqlwhere) < 0)
            {
                this.Err = "索引为" + whereIndex + "的SQL语句不存在！";
                return null;
            }
            sqlstr += " " + sqlwhere;
            if (this.ExecQuery(sqlstr, args) < 0) return null;
            List<PrePay> list = new List<PrePay>();
            PrePay prepay = null;
            try
            {
                while (this.Reader.Read())
                {
                    prepay = new PrePay();
                    prepay.Patient.PID.CardNO = this.Reader[0].ToString(); //门诊卡号
                    prepay.HappenNO = NConvert.ToInt32(this.Reader[1]); //发生序号
                    prepay.Patient.Name = this.Reader[2].ToString(); //患者姓名
                    prepay.InvoiceNO = this.Reader[3].ToString();//票据号
                    prepay.PayType.ID = this.Reader[4].ToString();//支付方式
                    prepay.BaseCost = NConvert.ToDecimal(this.Reader[5]); //预交金
                    prepay.Bank.Name = this.Reader[6].ToString(); //银行
                    prepay.Bank.Account = this.Reader[7].ToString();//开户帐号
                    prepay.Bank.InvoiceNO = this.Reader[8].ToString();//开户帐号
                    prepay.IsValid = NConvert.ToBoolean(this.Reader[9]);//是否日结算
                    prepay.BalanceNO = this.Reader[10].ToString();//日结号
                    prepay.BalanceOper.ID = this.Reader[11].ToString(); //日结人
                    prepay.BalanceOper.OperTime = NConvert.ToDateTime(this.Reader[12]);//日结时间
                    prepay.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[13]); //状态
                    prepay.PrintTimes = NConvert.ToInt32(this.Reader[14]);//打印次数;
                    prepay.OldInvoice = this.Reader[15].ToString(); //原收据号
                    prepay.PrePayOper.ID = this.Reader[16].ToString();//操作员
                    prepay.PrePayOper.OperTime = NConvert.ToDateTime(this.Reader[17]);//操作时间
                    prepay.AccountNO = this.Reader[18].ToString();//账号
                    prepay.IsHostory = NConvert.ToBoolean(this.Reader[19].ToString());
                    prepay.Bank.WorkName = this.Reader[20].ToString();
                    list.Add(prepay);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// 医保审核
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cardNO"></param>
        /// <param name="operId"></param>
        /// <returns></returns>
        private int UpdateMedicalByCardNo(string sql, string cardNO, string operId)
        {
            string sqlstr = string.Empty;
            string sqlhead = string.Empty;

            if (this.Sql.GetCommonSql(sql, ref sqlstr) < 0)
            {
                this.Err = "索引为Fee.Account.UpdateMedicalInsurance的SQL语句不存在！";
                return 0;
            }

            if (this.Sql.GetCommonSql("Fee.Account.InsertMedicalInsuranceHead", ref sqlhead) < 0)
            {
                this.Err = "索引为Fee.Account.InsertMedicalInsuranceHead的SQL语句不存在！";
                return 0;
            }

            string headseq = GetMedicalInsuranceHeadSeq();

            sqlhead = string.Format(sqlhead, headseq, cardNO, "1", operId);
            sqlstr = string.Format(sqlstr, cardNO, operId, headseq);

            int row = this.ExecNoQuery(sqlhead);

            if (row > 0)
            {
                return this.ExecNoQuery(sqlstr);
            }
            else
            {
                return 0;
            }


        }

        /// <summary>
        /// 获取序列
        /// </summary>
        /// <returns></returns>
        private string GetMedicalInsuranceHeadSeq()
        {
            string sqlstr = @"select seq_gzsi_his_accountaudithead.Nextval from dual ";
            if (this.ExecQuery(sqlstr) < 0) return null;
            string headseq = "";
            try
            {
                while (this.Reader.Read())
                {
                    headseq = this.Reader[0].ToString();
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            return headseq;
        }

        /// <summary>
        /// 医保返账项目明细
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        private List<AccountMedicalInsurance> GetMedicalInsuranceEX(string sql, string cardNO)
        {
            string sqlstr = string.Empty;
            if (this.Sql.GetCommonSql(sql, ref sqlstr) < 0)
            {
                this.Err = "索引为Fee.Account.GetMedicalInsurance的SQL语句不存在！";
                return null;
            }
            sqlstr = string.Format(sqlstr, cardNO);

            if (this.ExecQuery(sqlstr) < 0) return null;

            List<AccountMedicalInsurance> list = new List<AccountMedicalInsurance>();
            AccountMedicalInsurance prepay = null;
            try
            {
                while (this.Reader.Read())
                {
                    prepay = new AccountMedicalInsurance();
                    prepay.Cardno = this.Reader[0].ToString(); //门诊卡号
                    prepay.Name = this.Reader[1].ToString();
                    prepay.Xmbh = this.Reader[2].ToString();
                    prepay.Xmmc = this.Reader[3].ToString();
                    prepay.Createtime = NConvert.ToDateTime(this.Reader[4]);
                    prepay.Cliniccode = this.Reader[5].ToString();
                    prepay.Je = NConvert.ToDecimal(this.Reader[6]);
                    prepay.Qty = NConvert.ToDecimal(this.Reader[7]);
                    prepay.State = this.Reader[8].ToString();
                    prepay.Operenvironment.ID = this.Reader[9].ToString();
                    prepay.Operenvironment.OperTime = NConvert.ToDateTime(this.Reader[10]);
                    list.Add(prepay);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return list;
        }


        /// <summary>
        /// 查找帐户预交金信息NEW lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="whereIndex">WhereSql语句的索引</param>
        /// <param name="args">Where条件参数</param>
        /// <returns>null 失败</returns>
        private List<PrePay> GetPrePayListEX(string whereIndex, params string[] args)
        {
            string sqlstr = string.Empty;
            string sqlwhere = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetPrePayRecrod.1", ref sqlstr) < 0)
            {
                this.Err = "索引为Fee.Account.GetPrePayRecrod.1的SQL语句不存在！";
                return null;
            }
            if (this.Sql.GetCommonSql(whereIndex, ref sqlwhere) < 0)
            {
                this.Err = "索引为" + whereIndex + "的SQL语句不存在！";
                return null;
            }
            sqlstr += " " + sqlwhere;
            if (this.ExecQuery(sqlstr, args) < 0) return null;
            List<PrePay> list = new List<PrePay>();
            PrePay prepay = null;
            try
            {
                while (this.Reader.Read())
                {
                    prepay = new PrePay();
                    prepay.Patient.PID.CardNO = this.Reader[0].ToString(); //门诊卡号
                    prepay.HappenNO = NConvert.ToInt32(this.Reader[1]); //发生序号
                    prepay.Patient.Name = this.Reader[2].ToString(); //患者姓名
                    prepay.InvoiceNO = this.Reader[3].ToString();//票据号
                    prepay.PayType.ID = this.Reader[4].ToString();//支付方式
                    prepay.BaseCost = NConvert.ToDecimal(this.Reader[5]); //预交金
                    prepay.Bank.Name = this.Reader[6].ToString(); //银行
                    prepay.Bank.Account = this.Reader[7].ToString();//开户帐号
                    prepay.Bank.InvoiceNO = this.Reader[8].ToString();//开户帐号
                    prepay.IsValid = NConvert.ToBoolean(this.Reader[9]);//是否日结算
                    prepay.BalanceNO = this.Reader[10].ToString();//日结号
                    prepay.BalanceOper.ID = this.Reader[11].ToString(); //日结人
                    prepay.BalanceOper.OperTime = NConvert.ToDateTime(this.Reader[12]);//日结时间
                    prepay.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[13]); //状态
                    prepay.PrintTimes = NConvert.ToInt32(this.Reader[14]);//打印次数;
                    prepay.OldInvoice = this.Reader[15].ToString(); //原收据号
                    prepay.PrePayOper.ID = this.Reader[16].ToString();//操作员
                    prepay.PrePayOper.OperTime = NConvert.ToDateTime(this.Reader[17]);//操作时间
                    prepay.AccountNO = this.Reader[18].ToString();//账号
                    prepay.IsHostory = NConvert.ToBoolean(this.Reader[19].ToString());
                    prepay.Bank.WorkName = this.Reader[20].ToString();

                    prepay.DonateCost = NConvert.ToDecimal(this.Reader[21]); //赠送金额
                    prepay.BaseVacancy = NConvert.ToDecimal(this.Reader[22]); //基本账户交易后余额
                    prepay.DonateVacancy = NConvert.ToDecimal(this.Reader[23]); //赠送账户交易后余额
                    prepay.PrintNo = this.Reader[24].ToString();//打印单据号
                    prepay.AccountType.ID = this.Reader[25].ToString();//账户类型
                    prepay.AccountType.Name = this.Reader[26].ToString();
                    prepay.Memo = this.Reader[27].ToString();//备注
                    list.Add(prepay);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return list;
        }



        /// <summary>
        /// 按编码和账户类型编码查找账户类型相关信息// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="accountType"></param>
        /// <returns></returns>
        private List<AccountDetail> GetAccountDetailSelect(string accountNo, string accountType, string vailFlag, string whereSql)
        {
            string sqlstr = string.Empty;
            string whereStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetAccountDetail.Select", ref sqlstr) < 0)
            {
                this.Err = "索引为Fee.Account.GetAccountDetail.Select的SQL语句不存在！";
                return null;
            }
            if (this.Sql.GetCommonSql(whereSql, ref whereStr) < 0)
            {
                this.Err = "索引为" + whereSql + "的SQL语句不存在！";
                return null;
            }
            sqlstr += "  " + whereStr;
            if (this.ExecQuery(sqlstr, accountNo, accountType, vailFlag) < 0) return null;
            List<AccountDetail> list = new List<AccountDetail>();
            AccountDetail accountDetail = null;
            try
            {
                while (this.Reader.Read())
                {
                    accountDetail = new AccountDetail();
                    accountDetail.ID = this.Reader[0].ToString(); //账号
                    accountDetail.AccountType.ID = this.Reader[1].ToString(); //账户类型编码
                    accountDetail.AccountType.Name = this.Reader[2].ToString(); //账户类型名称
                    accountDetail.CardNO = this.Reader[3].ToString(); //门诊卡号
                    if (this.Reader[4].ToString() == "0")//账户状态
                    {
                        accountDetail.ValidState = EnumValidState.Invalid;
                    }
                    else if (this.Reader[4].ToString() == "1")
                    {
                        accountDetail.ValidState = EnumValidState.Valid;
                    }
                    else if (this.Reader[4].ToString() == "2")
                    {
                        accountDetail.ValidState = EnumValidState.Ignore;
                    }
                    else if (this.Reader[4].ToString() == "3")
                    {
                        accountDetail.ValidState = EnumValidState.Extend;
                    }

                    accountDetail.DayLimit = NConvert.ToDecimal(this.Reader[5]); //单日消费限制
                    accountDetail.BaseVacancy = NConvert.ToDecimal(this.Reader[6]); //账户基本余额
                    accountDetail.DonateVacancy = NConvert.ToDecimal(this.Reader[7]);//账户赠送余额
                    accountDetail.CouponVacancy = NConvert.ToDecimal(this.Reader[8]);//账户积分剩余
                    accountDetail.BaseAccumulate = NConvert.ToDecimal(this.Reader[9]);//账户基本累计金额
                    accountDetail.DonateAccumulate = NConvert.ToDecimal(this.Reader[10]);//账户赠送累计金额
                    accountDetail.CouponAccumulate = NConvert.ToDecimal(this.Reader[11]);//账户积分累计
                    accountDetail.CreateEnvironment.ID = this.Reader[12].ToString();//创建人
                    accountDetail.CreateEnvironment.OperTime = NConvert.ToDateTime(this.Reader[13].ToString());//创建时间
                    accountDetail.OperEnvironment.ID = this.Reader[14].ToString();//操作人
                    accountDetail.OperEnvironment.OperTime = NConvert.ToDateTime(this.Reader[15].ToString()); //操作时间
                    list.Add(accountDetail);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return list;
        }
        /// <summary>
        /// 查找帐户交易操作流水信息
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private List<AccountRecord> GetAccountRecord(string whereIndex, params string[] args)
        {
            string Sql = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountRecord", ref Sql) == -1)
            {
                this.Err = "提取SQL语句出错！";
                return null;
            }
            if (this.Sql.GetCommonSql(whereIndex, ref SqlWhere) == -1)
            {
                this.Err = "提取SQL语句出错！";
                return null;
            }

            try
            {
                SqlWhere = string.Format(SqlWhere, args);
                Sql += " " + SqlWhere;
                if (this.ExecQuery(Sql) == -1)
                {
                    this.Err = "查找帐户交易数据失败！";
                    return null;
                }
                List<FS.HISFC.Models.Account.AccountRecord> list = new List<FS.HISFC.Models.Account.AccountRecord>();
                FS.HISFC.Models.Account.AccountRecord accountRecord = null;
                while (this.Reader.Read())
                {
                    accountRecord = new FS.HISFC.Models.Account.AccountRecord();
                    accountRecord.Patient.PID.CardNO = Reader[0].ToString();
                    accountRecord.AccountNO = Reader[1].ToString();
                    accountRecord.OperType.ID = Reader[2].ToString();
                    if (Reader[3] != DBNull.Value) accountRecord.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3]);
                    accountRecord.DonateCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[4]);
                    if (Reader[5] != DBNull.Value) accountRecord.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5]);
                    accountRecord.DonateVacancy = FS.FrameWork.Function.NConvert.ToDecimal(Reader[6]);
                    accountRecord.FeeDept.ID = Reader[7].ToString();
                    accountRecord.FeeDept.Name = Reader[8].ToString();
                    accountRecord.Oper.ID = Reader[9].ToString();
                    accountRecord.Oper.Name = Reader[10].ToString();
                    accountRecord.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[11]);
                    if (Reader[9] != DBNull.Value) accountRecord.ReMark = Reader[12].ToString();
                    accountRecord.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[13]);
                    if (Reader[14] != DBNull.Value) accountRecord.EmpowerPatient.PID.CardNO = this.Reader[14].ToString();
                    if (Reader[15] != DBNull.Value) accountRecord.EmpowerPatient.Name = this.Reader[15].ToString();
                    accountRecord.EmpowerCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                    accountRecord.InvoiceType.ID = this.Reader[17].ToString();
                    accountRecord.InvoiceNo = this.Reader[18].ToString();
                    accountRecord.PayType.ID = this.Reader[19].ToString();
                    accountRecord.PayInvoiceNo = this.Reader[20].ToString();
                    if (Reader[21] != DBNull.Value)
                        accountRecord.AccountType.ID = this.Reader[21].ToString();// {74621CF6-D26C-44aa-87DC-C68D0867BAC5}
                    list.Add(accountRecord);
                }
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 授权实体属性字符串数组
        /// </summary>
        /// <param name="accountEmpower"></param>
        /// <returns></returns>
        private string[] GetEmpowerArgs(AccountEmpower accountEmpower)
        {
            string[] args = new string[] {accountEmpower.AccountCard.Patient.PID.CardNO,
                                          accountEmpower.AccountCard.Patient.Name,  
                                          accountEmpower.AccountNO,
                                          accountEmpower.AccountCard.MarkNO,  
                                          accountEmpower.AccountCard.MarkType.ID,
                                          accountEmpower.EmpowerCard.Patient.PID.CardNO, 
                                          accountEmpower.EmpowerCard.Patient.Name,
                                          accountEmpower.EmpowerCard.MarkNO,  
                                          accountEmpower.EmpowerCard.MarkType.ID,
                                          accountEmpower.EmpowerLimit.ToString(),
                                          FS.HisDecrypt.Encrypt(accountEmpower.PassWord),
                                          accountEmpower.Oper.ID,
                                          accountEmpower.Vacancy.ToString(),
                                          (NConvert.ToInt32(accountEmpower.ValidState)).ToString()
                                          };
            return args;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<AccountEmpower> GetEmpowerList(string whereIndex, params string[] args)
        {
            string sql = string.Empty;
            string sqlwhere = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectEmpower", ref sql) < 0)
            {
                this.Err = "查找索引为Fee.Account.SelectEmpower的SQL语句失败！";
                return null;
            }
            if (this.Sql.GetCommonSql(whereIndex, ref sqlwhere) < 0)
            {
                this.Err = "查找索引为" + whereIndex + "的SQL语句失败！";
                return null;
            }
            sql += " " + string.Format(sqlwhere, args);
            if (this.ExecQuery(sql) < 0) return null;
            List<AccountEmpower> list = new List<AccountEmpower>();
            AccountEmpower obj = null;
            try
            {
                while (this.Reader.Read())
                {
                    obj = new AccountEmpower();
                    obj.AccountCard.Patient.PID.CardNO = this.Reader[0].ToString();
                    obj.AccountCard.Patient.Name = this.Reader[1].ToString();
                    obj.AccountNO = this.Reader[2].ToString();
                    obj.AccountCard.MarkNO = this.Reader[3].ToString();
                    obj.AccountCard.MarkType.ID = this.Reader[4].ToString();
                    obj.EmpowerCard.Patient.PID.CardNO = this.Reader[5].ToString();
                    obj.EmpowerCard.Patient.Name = this.Reader[6].ToString();
                    obj.EmpowerCard.MarkNO = this.Reader[7].ToString();
                    obj.EmpowerCard.MarkType.ID = this.Reader[8].ToString();
                    obj.EmpowerLimit = NConvert.ToDecimal(this.Reader[9]);
                    obj.PassWord = FS.HisDecrypt.Decrypt(this.Reader[10].ToString());
                    obj.Oper.ID = this.Reader[11].ToString();
                    obj.Oper.OperTime = NConvert.ToDateTime(this.Reader[12]);
                    obj.Vacancy = NConvert.ToDecimal(this.Reader[13]);
                    obj.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[14]);
                    list.Add(obj);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            return list;
        }

        /// <summary>
        /// 得到帐户余额
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="vacancy">帐户余额</param>
        /// <returns>-1 失败 0是没有帐户或帐户停用或帐户已被注销 1成功</returns>
        private int GetAccountVacancy(string cardNO, ref decimal vacancy)
        {
            string Sql = string.Empty;
            bool isHaveVacancy = false;
            if (this.Sql.GetCommonSql("Fee.Account.GetVacancy", ref Sql) == -1)
            {
                this.Err = "为找到SQL语句！";

                return -1;
            }
            try
            {
                if (this.ExecQuery(Sql, cardNO) == -1)
                {
                    return -1;
                }

                string state = string.Empty;

                while (this.Reader.Read())
                {
                    vacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                    state = Reader[1].ToString();
                    isHaveVacancy = true;
                }
                this.Reader.Close();
                if (isHaveVacancy)
                {
                    if (state == "0")
                    {
                        this.Err = "该帐户已经停用";
                        return 0;
                    }
                    return 1;
                }
                else
                {
                    this.Err = "该患者未建立帐户或帐户已注销";
                    return 0;
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得帐户余额失败！" + ex.Message;

                return -1;
            }
        }
        /// <summary>
        /// 得到帐户余额NEW// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="vacancy">帐户余额</param>
        /// <returns>-1 失败 0是没有帐户或帐户停用或帐户已被注销 1成功</returns>
        public FS.HISFC.Models.Account.Account GetAccountVacancyEX(string cardNO)
        {
            FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();
            string Sql = string.Empty;
            bool isHaveVacancy = false;
            if (this.Sql.GetCommonSql("Fee.Account.GetVacancy.1", ref Sql) == -1)
            {
                this.Err = "为找到SQL语句！";

                return null;
            }
            try
            {
                if (this.ExecQuery(Sql, cardNO) == -1)
                {
                    return null;
                }

                string state = string.Empty;

                while (this.Reader.Read())
                {
                    account.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
                    account.DonateVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                    account.CouponVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    account.Limit = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                    account.BaseAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    account.DonateAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    account.CouponAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    account.AccountLevel.ID = this.Reader[7].ToString();// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                    account.AccountLevel.Name = this.Reader[8].ToString();// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                    if (this.Reader[9].ToString() == "1")
                    {
                        account.ValidState = EnumValidState.Valid;
                    }
                    else
                    {
                        account.ValidState = EnumValidState.Invalid;
                    }
                    isHaveVacancy = true;
                }
                this.Reader.Close();
                if (isHaveVacancy)
                {
                    if (state == "0")
                    {
                        this.Err = "该帐户已经停用";
                        return null;
                    }
                    return account;
                }
                else
                {
                    this.Err = "该患者未建立帐户或帐户已注销";
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得帐户余额失败！" + ex.Message;

                return null;
            }
        }
        /// <summary>
        /// 获取被授权患者余额
        /// </summary>
        /// <param name="empowerCardNO">被授权门诊卡号</param>
        /// <returns>1成功 0不存在可用的授权信息　-1不存在被授权信息</returns>
        private int GetEmpowerVacancy(string empowerCardNO, ref decimal vacancy)
        {
            AccountEmpower accountEmpower = new AccountEmpower();
            int resultValue = QueryAccountEmpowerByEmpwoerCardNO(empowerCardNO, ref accountEmpower);
            if (resultValue == 1)
            {
                vacancy = accountEmpower.Vacancy;
            }
            return resultValue;
        }

        /// <summary>
        /// 查询患者证件信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList GetPatientIdenInfo(string sql, params string[] args)
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

        #endregion

        #region 查询患者信息
        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="markNO">就诊卡号</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string markNO)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatientByMarkNO", ref strSql) == -1) return null;
            strSql = string.Format(strSql, markNO);
            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);
            if (list == null || list.Count == 0) return null;
            return list[0];
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="IDCard">身份证号</param>
        /// <returns></returns>
        public int GetPatientInfoByIDCard(string IDCard)
        {
            try
            {
                string strSql = string.Empty;
                if (this.Sql.GetCommonSql("Fee.Account.SelectPatientByIDCard", ref strSql) == -1) return 0;
                strSql = string.Format(strSql, IDCard);
                int i = 0;
                if (this.ExecQuery(strSql) == -1)
                {
                    return -1;
                }
                while (this.Reader.Read())
                {
                    if (!Reader.IsDBNull(0)) i = NConvert.ToInt32(Reader[0]); //就诊卡号
                }
                return i;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 根据ID号查找门诊号
        /// </summary>
        /// <param name="IDCard">身份证</param>
        /// <returns></returns>
        public string GetCardNoByIDCard(string IDCard)
        {
            try
            {
                string strSql = string.Empty;
                if (this.Sql.GetCommonSql("Fee.Account.QeuryCardNoByIDCard", ref strSql) == -1) return string.Empty;
                strSql = string.Format(strSql, IDCard);
                string CardNo = string.Empty;
                if (this.ExecQuery(strSql) == -1)
                {
                    return string.Empty;
                }
                while (this.Reader.Read())
                {
                    if (!Reader.IsDBNull(0)) CardNo = Reader[0].ToString(); //就诊卡号
                }
                return CardNo;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return string.Empty;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }
        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="CardNO">就诊卡号</param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfoByCardNO(string CardNO)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatientByCardNO", ref strSql) == -1) return null;
            strSql = string.Format(strSql, CardNO);
            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);
            if (list == null || list.Count == 0) return null;
            return list[0];
        }

        /// <summary>
        /// 修改患者合同单位{}
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="pactName"></param>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public int UpdatePatientPactByCardNO(string pactCode, string pactName, string cardNO)
        {
            string strSql = @" update com_patientinfo set pact_code='{0}',pact_name='{1}' where card_no = '{2}' ";
            strSql = string.Format(strSql, pactCode, pactName, cardNO);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 查找患者信息
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCardInDays(string operCode, string days)
        {
            string sqlWhere = string.Empty;
            string strSql = string.Empty;
            if (!string.IsNullOrEmpty(operCode) && !string.IsNullOrEmpty(days))
            {// {3218011F-CCDA-49f3-B468-06F25B3F7F72}
                sqlWhere += " and (tt.createoper = '" + operCode + "' or 'ALL' = '" + operCode + "') and tt.createdate > sysdate - '" + days + "' ";
            }
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatient", ref strSql) == -1)
            {
                this.Err = "查找索引为Fee.Account.SelectPatient的Sql语句失败！";
                return null;
            }
            strSql += sqlWhere + "  and tt.state = 1 order by tt.createdate";
            List<FS.HISFC.Models.Account.AccountCard> list = GetAccountCardList(strSql);
            return list;


        }
        //{63F68506-F49D-4ed5-92BD-28A52AF54626}
        /// <summary>
        /// 查找患者信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="pact">合同单位</param>
        /// <param name="caseNO">病案号</param>
        /// <param name="idenType">证件类型</param>
        /// <param name="idenNo">证件号</param>
        /// <param name="ssNO">医疗证号</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCard(string name, string sex, string pact, string caseNO, string idenType, string idenNo, string ssNO)
        {

            return this.GetAccountCard(name, sex, pact, caseNO, idenType, idenNo, ssNO, "", "");
        }


        /// <summary>
        /// 查找患者信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="pact">合同单位</param>
        /// <param name="caseNO">病案号</param>
        /// <param name="idenType">证件类型</param>
        /// <param name="idenNo">证件号</param>
        /// <param name="ssNO">医疗证号</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCardEX(string name, string sex, string pact, string caseNO, string idenType, string idenNo, string ssNO, string phone, string cardNo)
        {

            return this.GetAccountCard(name, sex, pact, caseNO, idenType, idenNo, ssNO, phone, cardNo);
        }

        #region 20161118新增

        //{B062ABDC-7545-4e5d-A9F5-DCBF217052F9}
        /// <summary>
        /// 查找患者信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="pact">合同单位</param>
        /// <param name="caseNO">病案号</param>
        /// <param name="idenType">证件类型</param>
        /// <param name="idenNo">证件号</param>
        /// <param name="ssNO">医疗证号</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCard(string name, string homePhone, string mobile, string idenType, string idenNo, string ssNO)
        {

            string sqlWhere = string.Empty;
            bool isInput = false;
            if (name != null && name != string.Empty)
            {
                sqlWhere += " and t.NAME = '" + name + "' ";
                isInput = true;
            }

            if (idenNo != null && idenNo != string.Empty)
            {
                sqlWhere += " and t.IDENNO = '" + idenNo + "'";
                isInput = true;
            }

            if (!string.IsNullOrEmpty(ssNO))
            {
                sqlWhere += " and t.MCARD_NO = '" + ssNO + "'";
                isInput = true;
            }

            if (!string.IsNullOrEmpty(homePhone))
            {
                sqlWhere += " and t.HOME_TEL = '" + homePhone + "'";
                isInput = true;
            }

            if (!string.IsNullOrEmpty(mobile))
            {
                sqlWhere += " and t.MOBILE = '" + mobile + "'";
                isInput = true;
            }

            if (!isInput)
            {
                this.Err = "请输入患者信息！";
                return null;
            }
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatient", ref strSql) == -1)
            {
                this.Err = "查找索引为Fee.Account.SelectPatient的Sql语句失败！";
                return null;
            }
            strSql += sqlWhere + " order by t.card_no";
            List<FS.HISFC.Models.Account.AccountCard> list = GetAccountCardList(strSql);
            return list;
        }

        #endregion

        /// <summary>
        /// 查找患者信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="pact">合同单位</param>
        /// <param name="caseNO">病案号</param>
        /// <param name="idenType">证件类型</param>
        /// <param name="idenNo">证件号</param>
        /// <param name="ssNO">医疗证号</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCard(string name, string sex, string pact, string caseNO, string idenType, string idenNo, string ssNO, string phone, string cardNo)
        {
            string sqlWhere = string.Empty;
            bool isInput = false;

            //姓名
            if (!string.IsNullOrEmpty(name))
            {
                sqlWhere += " and t.NAME = '" + name + "' ";
                //sqlWhere += " and t.NAME like '%" + name + "%' ";
                isInput = true;
            }

            //性别
            if (!string.IsNullOrEmpty(sex))
            {
                sqlWhere += " and t.SEX_CODE  = '" + sex + "' ";
                isInput = true;
            }

            //去掉合同单条件
            //if (pact != null && pact != string.Empty)
            //{
            //    sqlWhere += " and t.PACT_CODE  = '" + pact + "' ";
            //    isInput = true;
            //}

            //病历号
            if (!string.IsNullOrEmpty(caseNO))
            {
                sqlWhere += " and t.CASE_NO = '" + caseNO + "' ";
                isInput = true;
            }

            //证件类型
            if (!string.IsNullOrEmpty(idenType))
            {
                sqlWhere += " and t.IDCARDTYPE = '" + idenType + "' ";
                isInput = true;
            }

            //证件号码
            if (!string.IsNullOrEmpty(idenNo))
            {
                sqlWhere += " and t.IDENNO = '" + idenNo + "'";
                isInput = true;
            }

            //医疗证号
            if (!string.IsNullOrEmpty(ssNO))
            {
                sqlWhere += " and t.MCARD_NO = '" + ssNO + "'";
                isInput = true;
            }

            //联系电话
            if (!string.IsNullOrEmpty(phone))
            {
                sqlWhere += " and t.HOME_TEL = '" + phone + "'";
                isInput = true;
            }

            if (!string.IsNullOrEmpty(cardNo))
            {
                sqlWhere += " and t.CARD_NO = '" + cardNo + "'";
                isInput = true;
            }
            if (!isInput)
            {
                this.Err = "请输入患者信息！";
                return null;
            }
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatient", ref strSql) == -1)
            {
                this.Err = "查找索引为Fee.Account.SelectPatient的Sql语句失败！";
                return null;
            }
            //sqlWhere = sqlWhere.Substring(0, sqlWhere.LastIndexOf("and") - 1);
            strSql += sqlWhere + " order by t.card_no";
            //List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);
            List<FS.HISFC.Models.Account.AccountCard> list = GetAccountCardList(strSql);
            return list;
        }
        /// <summary>
        /// 获取账户信息
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.Account.AccountCard> MyGetAccountCard(String whereSql, params string[] parms)
        {
            string selectSql = string.Empty;
            string sql = "";
            if (this.Sql.GetCommonSql("Fee.Account.SelectPatient", ref selectSql) == -1)
            {
                this.Err = "查找索引为Fee.Account.SelectPatient的Sql语句失败！";
                return null;
            }
            if (this.Sql.GetCommonSql(whereSql, ref whereSql) == -1)
            {
                this.Err = "查找索引为" + whereSql + "的Sql语句失败！";
                return null;
            }

            selectSql = selectSql + whereSql;

            sql = string.Format(selectSql, parms);

            return GetAccountCardList(sql);
        }

        /// <summary>
        /// 根据卡号（无特殊标识)查找卡记录
        /// </summary>
        /// <param name="normalNo"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetAccountCardByNormalNo(string normalNo)
        {
            return MyGetAccountCard("Fee.Account.QueryByNormalNo", normalNo);
        }

        /// <summary>
        /// 查找患者信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sex"></param>
        /// <param name="pact"></param>
        /// <param name="idenType"></param>
        /// <param name="idenNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> QueryPatient(string name, string sex, string pact, string idenType, string idenNo, string cardNo, string phone)
        {
            string sqlWhere = string.Empty;
            bool isInput = false;
            if (name != null && name != string.Empty)
            {
                sqlWhere += " and t.NAME like '%" + name + "%' ";
                isInput = true;
            }
            if (sex != null && sex != string.Empty && (sex == "M" || sex == "F"))
            {
                sqlWhere += " and t.SEX_CODE  = '" + sex + "' ";
                isInput = true;
            }
            if (pact != null && pact != string.Empty)
            {
                sqlWhere += " and t.PACT_CODE  = '" + pact + "' ";
                isInput = true;
            }

            if (idenType != null && idenType != string.Empty)
            {
                sqlWhere += " and t.IDCARDTYPE = '" + idenType + "' ";
                isInput = true;
            }
            if (idenNo != null && idenNo != string.Empty)
            {
                sqlWhere += " and t.IDENNO = '" + idenNo + "'";
                isInput = true;
            }
            if (cardNo != null && cardNo != string.Empty)
            {
                sqlWhere += " and t.CARD_NO = '" + cardNo + "'";
                isInput = true;
            }
            if (phone != null && phone != string.Empty)
            {
                sqlWhere += " and t.HOME_TEL = '" + phone + "'";
                isInput = true;
            }
            if (!isInput)
            {
                this.Err = "请输入患者信息！";
                return null;
            }
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.PatientInfo", ref strSql) == -1)
            {
                this.Err = "查找索引为 Fee.Account.PatientInfo 的Sql语句失败！";
                return null;
            }
            strSql += sqlWhere + " order by t.card_no";

            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);
            return list;
        }

        /// <summary>
        /// 查找丢失卡的患者信息
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="pact">合同单位</param>
        /// <param name="idenType">证件类型</param>
        /// <param name="idenNo">证件号</param>
        /// <param name="cardNo">门诊卡号</param>
        /// <param name="phone">医疗证号</param>
        /// <returns></returns>
        public List<AccountCard> GetLostAccountCard(string cardNo)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.PatientCard", ref strSql) == -1)
            {
                this.Err = "查找索引为 Fee.Account.SelectPatient 的Sql语句失败！";
                return null;
            }

            try
            {
                strSql = string.Format(strSql, cardNo);

                if (this.ExecQuery(strSql) == -1) return null;

                AccountCard card = null;
                List<AccountCard> lstCard = new List<AccountCard>();
                while (this.Reader.Read())
                {
                    card = new AccountCard();
                    card.Patient.PID.CardNO = this.Reader[0].ToString().Trim();
                    card.MarkNO = this.Reader[1].ToString().Trim();
                    card.MarkType.ID = this.Reader[2].ToString().Trim();
                    card.MarkStatus = (MarkOperateTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    card.ReFlag = this.Reader[4].ToString().Trim();
                    card.CreateOper.ID = this.Reader[5].ToString().Trim();
                    card.CreateOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
                    card.StopOper.ID = this.Reader[7].ToString().Trim();
                    card.StopOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                    card.BackOper.ID = this.Reader[9].ToString().Trim();
                    card.BackOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10]);
                    card.SecurityCode = this.Reader[11].ToString().Trim();

                    lstCard.Add(card);

                }

                return lstCard;

            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                return null;
            }
        }
        /// <summary>
        /// 获取指定合同单位
        /// </summary>
        /// <param name="lstPact"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> QueryPatientByPact(List<string> lstPact)
        {
            if (lstPact == null || lstPact.Count <= 0)
            {
                return null;
            }

            string sqlWhere = " and pact_code in ('";

            foreach (string pact in lstPact)
            {
                sqlWhere += pact + "', '";
            }
            sqlWhere = sqlWhere.Trim(new char[] { '\'' });
            sqlWhere = sqlWhere.Trim();
            sqlWhere = sqlWhere.Trim(new char[] { ',' });
            sqlWhere += ")";

            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.PatientInfo", ref strSql) == -1)
            {
                this.Err = "查找索引为 Fee.Account.PatientInfo 的Sql语句失败！";
                return null;
            }

            strSql = strSql + " " + sqlWhere + " order by card_no";

            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);
            return list;
        }
        /// <summary>
        /// 查询患者的基本信息// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
        /// </summary>
        /// <param name="IDCardNO"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <param name="SexCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> QueryPatientInfoByWhere(string IDCardNO, string Name, string Phone, string SexCode)
        {
            string strSql = @"
                    SELECT card_no,   --就诊卡号
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
                           ic_cardno, --电脑号
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
                           oper_date,   --操作日期
                           IS_ENCRYPTNAME, --是否加密姓名
                           NORMALNAME, --密文 
                           IDCARDTYPE, --证件类型
                           CRMID  --{67CE2526-5E7F-4c92-911F-56CA0077679A}
                      FROM com_patientinfo
                      where IS_VALID = '1' ";

            if (!string.IsNullOrEmpty(IDCardNO))
            {
                strSql += "  and IDENNO = '" + IDCardNO + "' ";
            }

            if (!string.IsNullOrEmpty(Name))
            {
                strSql += "  and NAME = '" + Name + "' ";
            }
            if (!string.IsNullOrEmpty(Phone))
            {
                strSql += "  and HOME_TEL = '" + Phone + "' ";
            }
            if (!string.IsNullOrEmpty(SexCode))
            {
                strSql += "  and SEX_CODE = '" + SexCode + "' ";
            }
            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);

            return list;
        }


        /// <summary>
        /// 查询患者的基本信息// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
        /// </summary>
        /// <param name="IDCardNO"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        /// <param name="SexCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.RADT.PatientInfo> QueryPatientInfoByLinkPhone(string Phone)
        {
            string strSql = @"
                    SELECT card_no,   --就诊卡号
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
                           ic_cardno, --电脑号
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
                           oper_date,   --操作日期
                           IS_ENCRYPTNAME, --是否加密姓名
                           NORMALNAME, --密文 
                           IDCARDTYPE, --证件类型
                           CRMID
                      FROM com_patientinfo
                      where IS_VALID = '1' ";

            if (!string.IsNullOrEmpty(Phone))
            {
                strSql += "  and linkman_tel = '" + Phone + "' ";
            }

            List<FS.HISFC.Models.RADT.PatientInfo> list = this.GetPatient(strSql);

            return list;
        }
        #endregion

        #region 帐户卡操作

        #region 插入卡操作记录
        /// <summary>
        /// 插入卡操作记录
        /// </summary>
        /// <param name="accountCardRecord">卡操作记录实体</param>
        /// <returns></returns>
        public int InsertAccountCardRecord(FS.HISFC.Models.Account.AccountCardRecord accountCardRecord)
        {
            string[] args = null;
            try
            {
                args = new string[] { accountCardRecord.MarkNO,
                                accountCardRecord.MarkType.ID.ToString(),
                                accountCardRecord.CardNO,
                                accountCardRecord.OperateTypes.ID.ToString(),
                                accountCardRecord.Oper.ID.ToString(),
                                accountCardRecord.CardMoney.ToString()};
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.UpdateSingTable("Fee.Account.InsetAccountCardRecord", args);
        }

        #endregion

        #region 根据患者门诊卡号查找卡信息

        /// <summary>
        /// 查找就诊卡信息
        /// </summary>
        /// <param name="markNO">物理卡号</param>
        /// <param name="markType">卡类型</param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.AccountCard GetAccountCard(string markNO, string markType)
        {
            //健康卡截取前16位
            if (markType.Trim() == "Health_CARD")
            {
                markNO = markNO.Substring(0, 16);
            }
            if (!string.IsNullOrEmpty(markNO))
            {
                string numStr = markNO.Substring(0, 1);// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                if (!System.Text.RegularExpressions.Regex.IsMatch(numStr, @"^\d+$"))
                {
                    markNO = markNO.Substring(1, markNO.Length - 1);
                }
            }
            List<AccountCard> list = this.GetAccountMarkList("Fee.Account.SelectAccountCardWhere3", markNO, markType);
            if (list == null || list.Count == 0) return null;
            return list[0];
        }
        /// <summary>
        /// 查找在用会员信息，仅返回一条// {9EE79BEB-608C-4bc1-991E-7F5E197A326C}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.AccountCard GetAccountCardForOne(string cardNo)
        {
            List<AccountCard> list = this.GetAccountMarkList("Fee.Account.SelectAccountCardWhere5", cardNo);
            if (list == null || list.Count == 0) return null;
            return list[0];
        }
        /// <summary>
        /// 查找就诊卡信息
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCard> GetMarkList(string cardNO)
        {
            return this.GetAccountMarkList("Fee.Account.SelectAccountCardWhere2", cardNO);

        }

        /// <summary>
        /// 查找就诊卡信息
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="state">状态 0停用　1在用</param>
        /// <returns></returns>
        public List<AccountCard> GetMarkList(string cardNO, bool state)
        {
            return this.GetAccountMarkList("Fee.Account.SelectAccountCardWhere4", cardNO, (NConvert.ToInt32(state)).ToString());
        }

        /// <summary>
        /// 查找就诊卡信息
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="markType">卡类型 All全部</param>
        /// <param name="state">状态 0停用 1在用 All全部</param>
        /// <returns></returns>
        public List<AccountCard> GetMarkList(string cardNO, string markType, string state)
        {
            //return this.GetMarkList("Fee.Account.SelectAccountCardWhere5", cardNO, markType, (NConvert.ToInt32(state)).ToString());
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountMarkNO", ref sqlStr) == -1)
            {
                this.Err = "查找索引为Fee.Account.SelectAccountMarkNO的SQL语句失败！";
                return null;
            }
            List<AccountCard> list = new List<AccountCard>();
            AccountCard tempCard = null;
            try
            {
                sqlStr = string.Format(sqlStr, cardNO, markType, state);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "查找数据失败！";
                    return null;
                }

                string strTemp = string.Empty;
                while (this.Reader.Read())
                {
                    tempCard = new AccountCard();
                    tempCard.Patient.PID.CardNO = this.Reader[0].ToString();
                    tempCard.MarkNO = this.Reader[1].ToString();
                    tempCard.MarkType.ID = this.Reader[2].ToString();

                    //{6036F4C6-9452-4f21-8634-940AACD4B296}
                    tempCard.AccountLevel.ID = this.Reader[12].ToString();

                    strTemp = this.Reader[3].ToString();
                    if (strTemp == "0")
                    {
                        tempCard.MarkStatus = MarkOperateTypes.Cancel;
                    }
                    else if (strTemp == "1")
                    {
                        tempCard.MarkStatus = MarkOperateTypes.Begin;
                    }
                    else if (strTemp == "2")
                    {
                        tempCard.MarkStatus = MarkOperateTypes.Stop;
                    }

                    tempCard.ReFlag = this.Reader[4].ToString().Trim();
                    tempCard.CreateOper.ID = this.Reader[5].ToString().Trim();
                    tempCard.CreateOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
                    tempCard.StopOper.ID = this.Reader[7].ToString().Trim();
                    tempCard.StopOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                    tempCard.BackOper.ID = this.Reader[9].ToString().Trim();
                    tempCard.BackOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10]);

                    tempCard.SecurityCode = this.Reader[11].ToString().Trim();

                    list.Add(tempCard);
                }
            }
            catch (Exception ex)
            {
                this.Err = "查找数据失败！" + ex.Message;
                return null;
            }
            return list;
        }

        /// <summary>
        /// 查找就诊卡信息
        /// </summary>
        /// <param name="idCardType"></param>
        /// <param name="idenno"></param>
        /// <param name="markType"></param>
        /// <returns></returns>
        public List<AccountCard> GetMarkListFromIdenno(string idCardType, string idenno, string markType)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountCardFromIdenno", ref sqlStr) == -1)
            {
                this.Err = "查找索引为Fee.Account.SelectAccountCardFromIdenno的SQL语句失败！";
                return null;
            }
            List<AccountCard> list = new List<AccountCard>();
            AccountCard tempCard = null;
            try
            {
                sqlStr = string.Format(sqlStr, idCardType, idenno, markType);
                if (this.ExecQuery(sqlStr) == -1)
                {
                    this.Err = "查找数据失败！";
                    return null;
                }
                while (this.Reader.Read())
                {
                    tempCard = new AccountCard();
                    tempCard.Patient.PID.CardNO = this.Reader[0].ToString();
                    tempCard.MarkNO = this.Reader[1].ToString();
                    tempCard.MarkType.ID = this.Reader[2].ToString();
                    if (this.Reader[3].ToString() == "1")
                    {
                        tempCard.IsValid = true;
                    }
                    else
                    {
                        tempCard.IsValid = false;
                    }
                    list.Add(tempCard);
                }
            }
            catch (Exception ex)
            {
                this.Err = "查找数据失败！" + ex.Message;
                return null;
            }
            return list;
        }

        #endregion

        #region 插入门诊帐户卡数据
        /// <summary>
        /// 插入门诊帐户卡数据
        /// </summary>
        /// <param name="accountCard"></param>
        /// <returns></returns>
        public int InsertAccountCard(FS.HISFC.Models.Account.AccountCard accountCard)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.InsertAccountCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql,
                                    accountCard.Patient.PID.CardNO, //门诊卡号
                                    accountCard.MarkNO,//身份标识卡号
                                    accountCard.MarkType.ID.ToString(),//身份标识卡类别 1磁卡 2IC卡 3保障卡
                                    FS.FrameWork.Function.NConvert.ToInt32(accountCard.MarkStatus).ToString(), //状态'1'正常'0'停用 
                                    accountCard.ReFlag,
                                    accountCard.CreateOper.ID,
                                    accountCard.CreateOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    accountCard.SecurityCode
                                    );
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
        /// 插入门诊帐户卡数据NEW // {AE74D7CC-B573-48a7-9EAD-60340E3F01C9}
        /// </summary>
        /// <param name="accountCard"></param>
        /// <returns></returns>
        public int InsertAccountCardEX(FS.HISFC.Models.Account.AccountCard accountCard)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.InsertAccountCard.1", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql,
                                    accountCard.Patient.PID.CardNO, //门诊卡号
                                    accountCard.MarkNO,//身份标识卡号
                                    accountCard.MarkType.ID.ToString(),//身份标识卡类别 1磁卡 2IC卡 3保障卡
                                    FS.FrameWork.Function.NConvert.ToInt32(accountCard.MarkStatus).ToString(), //状态'1'正常'0'停用 
                                    accountCard.ReFlag,
                                    accountCard.CreateOper.ID,
                                    accountCard.CreateOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                    accountCard.SecurityCode,
                                    accountCard.AccountLevel.ID,
                                    accountCard.BegTime.ToString(),
                                    accountCard.EndTime.ToString()
                                    );
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

        #region 更新卡状态
        /// <summary>
        /// 更新卡状态
        /// </summary>
        /// <param name="markNO">物理卡号</param>
        /// <param name="type">卡类型</param>
        /// <param name="valid">状态</param>
        /// <returns></returns>
        public int UpdateAccountCardState(string markNO, NeuObject markType, bool valid)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateAccountCardState", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, markNO, NConvert.ToInt32(valid).ToString(), markType.ID);
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
        /// 更新卡信息
        /// </summary>
        /// <param name="markNO"></param>
        /// <param name="markType"></param>
        /// <param name="levelCode"></param>
        /// <param name="begTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int UpdateAccountCardInfo(string markNO, NeuObject markType, string levelCode, DateTime begTime, DateTime endTime)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateAccountCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, markNO, markType.ID, levelCode, begTime.ToString(), endTime.ToString());
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
        /// 积分消费// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        public int UpdateCouponForPay(string cardNo, decimal money, string invoiceNo)
        {
            CounponOperTypes operType = CounponOperTypes.Pay;
            if (money < 0)
            {
                operType = CounponOperTypes.Quit;
            }
            else
            {
                operType = CounponOperTypes.Pay;
            }
            return this.UpdateCoupon(cardNo, money, invoiceNo, "", "", operType);
        }

        /// <summary>
        /// 更新积分信息// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="exchangeGoods"></param>
        /// <param name="mark"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        public int UpdateCoupon(string cardNo, decimal money, string invoiceNo, string exchangeGoods, string mark, CounponOperTypes operType)
        {
            //if (money == 0)
            //{
            //    this.Err = "金额为0！";
            //    return -1;
            //}
            if (string.IsNullOrEmpty(cardNo))
            {
                this.Err = "卡号为空！";
                return -1;
            }

            string couponOperType = "";
            decimal coupon = 0m;
            decimal couponAccumulate = 0m;
            if (operType == CounponOperTypes.Quit)
            {
                couponOperType = "0";
                if (money > 0)
                {
                    money = -money;
                }
                CardCoupon cardCoupon2 = new CardCoupon();
                cardCoupon2 = this.QueryCardCouponByCardNo(cardNo);
                if (string.IsNullOrEmpty(cardCoupon2.CardNo))
                {
                    this.Err = "没有该患者的积分账户信息！";
                    return -1;
                }
                if (cardCoupon2.CouponVacancy < -money)
                {
                    if (System.Windows.Forms.MessageBox.Show("积分不足！应退积分为：" + -money + "，可退积分为：" + cardCoupon2.CouponVacancy
                        + "\r\n是否继续？", "系统提示", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                    {

                        this.Err = "积分不足！";
                        return -1;
                    }
                    money = -cardCoupon2.CouponVacancy;
                }
                coupon = money;
                couponAccumulate = money;

            }
            else if (operType == CounponOperTypes.Pay)
            {
                couponOperType = "1";
                if (money < 0)
                {
                    money = -money;
                }
                coupon = money;
                couponAccumulate = money;
            }
            else if (operType == CounponOperTypes.Exc)
            {
                couponOperType = "2";
                couponAccumulate = 0m;
                if (money > 0)
                {
                    money = -money;
                }
                coupon = money;
                CardCoupon cardCoupon2 = new CardCoupon();
                cardCoupon2 = this.QueryCardCouponByCardNo(cardNo);
                if (string.IsNullOrEmpty(cardCoupon2.CardNo))
                {
                    this.Err = "没有该患者的积分账户信息！";
                    return -1;
                }

                if (cardCoupon2.CouponVacancy < -money)
                {
                    this.Err = "积分不足！";
                    return -1;
                }

                money = 0;
            }

            CardCoupon cardCoupon = new CardCoupon();

            cardCoupon.CardNo = cardNo;
            cardCoupon.Coupon = coupon;
            cardCoupon.CouponAccumulate = couponAccumulate;

            if (this.UpdateCardCoupon(cardCoupon) <= 0)
            {
                return -1;
            }
            CardCoupon cardCoupon1 = new CardCoupon();
            cardCoupon1 = this.QueryCardCouponByCardNo(cardCoupon.CardNo);

            CardCouponRecord cardCouponRecord = new CardCouponRecord();
            cardCouponRecord.CardNo = cardNo;
            cardCouponRecord.Money = money;
            cardCouponRecord.Coupon = coupon;
            cardCouponRecord.CouponVacancy = cardCoupon1.CouponVacancy;
            cardCouponRecord.InvoiceNo = invoiceNo;
            cardCouponRecord.ExchangeGoods = exchangeGoods;
            cardCouponRecord.Memo = mark;
            cardCouponRecord.OperEnvironment.ID = this.Operator.ID;
            cardCouponRecord.OperEnvironment.Name = this.Operator.Name;
            cardCouponRecord.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();
            cardCouponRecord.OperType = couponOperType;
            if (this.InsertCardCouponRecord(cardCouponRecord) <= 0)
            {
                return -1;
            }



            return 1;
        }


        #region 静默调用积分消费
        //{0EBA6A50-3F87-4e6a-AD8E-66062E90FDA0}

        /// <summary>
        /// 积分消费
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        public int CouponCostSilence(string cardNo, decimal money, string invoiceNo, string exchangeGoods, ref string vacancy)
        {
            if (string.IsNullOrEmpty(cardNo) || money < 0 || string.IsNullOrEmpty(invoiceNo) || string.IsNullOrEmpty(exchangeGoods))
            {
                this.Err = "病历号,发票号,商品详情不能为空,消费金额不能小于0!";
                return -1;
            }
            return this.UpdateCouponSilence(cardNo, money, invoiceNo, exchangeGoods, "积分兑换", CounponOperTypes.Exc, ref vacancy);
        }

        /// <summary>
        /// 积分退费
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        //public int CoupoQuitSilence(string cardNo, decimal money, string invoiceNo, string exchangeGoods,ref string vacancy)
        //{
        //    return this.UpdateCouponSilence(cardNo, money, invoiceNo, exchangeGoods, "", CounponOperTypes.Pay, ref vacancy);
        //}

        /// <summary>
        /// 更新积分信息// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="money"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="exchangeGoods"></param>
        /// <param name="mark"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        private int UpdateCouponSilence(string cardNo, decimal money, string invoiceNo, string exchangeGoods, string mark, CounponOperTypes operType, ref string vacancy)
        {
            if (string.IsNullOrEmpty(cardNo))
            {
                this.Err = "卡号为空！";
                return -1;
            }

            string couponOperType = "";
            decimal coupon = 0m;
            decimal couponAccumulate = 0m;
            if (operType == CounponOperTypes.Exc)
            {
                couponOperType = "2";
                couponAccumulate = 0m;
                coupon = -money;
                CardCoupon cardCoupon = new CardCoupon();
                cardCoupon = this.QueryCardCouponByCardNo(cardNo);
                if (string.IsNullOrEmpty(cardCoupon.CardNo))
                {
                    this.Err = "没有该患者的积分账户信息！";
                    return -1;
                }

                if (cardCoupon.CouponVacancy < money)
                {
                    this.Err = "积分不足！";
                    return -1;
                }

                vacancy = Math.Round((cardCoupon.CouponVacancy - money), 2).ToString();
            }
            else
            {
                this.Err = "不支持的操作类型！";
                return -1;
            }

            CardCoupon cardCouponForUpdate = new CardCoupon();

            cardCouponForUpdate.CardNo = cardNo;
            cardCouponForUpdate.Coupon = coupon;
            cardCouponForUpdate.CouponAccumulate = couponAccumulate;

            if (this.UpdateCardCoupon(cardCouponForUpdate) <= 0)
            {
                return -1;
            }

            CardCoupon cardCouponForRecord = new CardCoupon();
            cardCouponForRecord = this.QueryCardCouponByCardNo(cardCouponForUpdate.CardNo);

            CardCouponRecord cardCouponRecord = new CardCouponRecord();
            cardCouponRecord.CardNo = cardNo;
            cardCouponRecord.Money = -money;
            cardCouponRecord.Coupon = coupon;
            cardCouponRecord.CouponVacancy = cardCouponForRecord.CouponVacancy;
            cardCouponRecord.InvoiceNo = invoiceNo;
            cardCouponRecord.ExchangeGoods = exchangeGoods;
            cardCouponRecord.Memo = mark;
            cardCouponRecord.OperEnvironment.ID = this.Operator.ID;
            cardCouponRecord.OperEnvironment.Name = this.Operator.Name;
            cardCouponRecord.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();
            cardCouponRecord.OperType = couponOperType;
            if (this.InsertCardCouponRecord(cardCouponRecord) <= 0)
            {
                return -1;
            }

            return 1;
        }

        #endregion

        /// <summary>
        /// 更新积分// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="coupon"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        public int UpdateCardCoupon(CardCoupon cardCoupon)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateCoupon", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, cardCoupon.CardNo, cardCoupon.Coupon.ToString("F2"), cardCoupon.CouponAccumulate.ToString("F2"));
                int i = this.ExecNoQuery(Sql);
                if (i <= 0)
                {
                    return this.InsertCardCoupon(cardCoupon);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return 1;

        }
        #endregion

        #region 更新卡状态
        /// <summary>
        /// 更新卡状态
        /// </summary>
        /// <param name="markNO">物理卡号</param>
        /// <param name="markType">卡类型</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public int UpdateAccountCardState(string markNO, string markType, string state)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateAccountCardState", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, markNO, state, markType);
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
        /// 停用卡操作
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int StopAccountCard(AccountCard card)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.StopAccountCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, card.MarkNO, card.MarkType.ID, card.Patient.PID.CardNO, ((int)card.MarkStatus).ToString(), card.StopOper.ID, card.StopOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"));
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
        /// 退卡操作
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int BackAccountCard(AccountCard card)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.BackAccountCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, card.MarkNO, card.MarkType.ID, card.Patient.PID.CardNO, ((int)card.MarkStatus).ToString(), card.BackOper.ID, card.BackOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"));
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
        /// 停卡退卡启用操作
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int StopBackAccountCard(AccountCard card)
        {
            if (card == null)
            {
                this.Err = "参数为空！";
                return -1;
            }

            int iRes = 0;
            if (card.MarkStatus == MarkOperateTypes.Stop)
            {
                iRes = this.StopAccountCard(card);
            }
            else if (card.MarkStatus == MarkOperateTypes.Cancel)
            {
                iRes = this.BackAccountCard(card);
            }
            else if (card.MarkStatus == MarkOperateTypes.Begin)
            {
                iRes = this.RecoverdAccountCard(card);
            }
            if (iRes == -1)
            {
                return -1;
            }

            AccountCardRecord accountCardRecord = new FS.HISFC.Models.Account.AccountCardRecord();
            //插入卡的操作记录
            accountCardRecord.MarkNO = card.MarkNO;
            accountCardRecord.MarkType.ID = card.MarkType.ID;
            accountCardRecord.CardNO = card.Patient.PID.CardNO;
            accountCardRecord.OperateTypes.ID = (int)card.MarkStatus;
            accountCardRecord.Oper.ID = (this.Operator as FS.HISFC.Models.Base.Employee).ID;
            //是否收取卡成本费
            accountCardRecord.CardMoney = 0;

            if (this.InsertAccountCardRecord(accountCardRecord) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 启用卡操作
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public int RecoverdAccountCard(AccountCard card)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.RecoverCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, card.MarkNO, card.MarkType.ID, card.Patient.PID.CardNO, ((int)card.MarkStatus).ToString());
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

        #region 更新卡上传标记

        /// <summary>
        /// 更新卡上传标记
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="markNO"></param>
        /// <param name="markType"></param>
        /// <param name="upLoadFlag"></param>
        /// <returns></returns>
        public int UpdateAccountCardUploadFlag(string cardNo, string markNO, string markType, string upLoadFlag)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateAccountUpLoadFlag", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, cardNo, markNO, markType, upLoadFlag);
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

        #region 保存卡费用表
        /// <summary>
        /// 保存卡费用表
        /// </summary>
        /// <param name="cardFee"></param>
        /// <returns></returns>
        public int InsertAccountCardFee(AccountCardFee cardFee)
        {
            if (cardFee == null)
                return -1;

            if (string.IsNullOrEmpty(cardFee.InvoiceNo))
            {
                this.Err = "发票流水号为空！";

                return -1;
            }
            //默认支付方式
            if (string.IsNullOrEmpty(cardFee.PayType.ID)) cardFee.PayType.ID = "CA";

            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Insert2", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Insert 的Sql语句失败！";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql,
                    cardFee.InvoiceNo,
                    ((int)cardFee.TransType).ToString(),
                    cardFee.CardNo,
                    cardFee.MarkNO,
                    cardFee.MarkType.ID,
                    cardFee.Tot_cost,
                    cardFee.FeeOper.ID,
                    cardFee.FeeOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    cardFee.Oper.ID,
                    cardFee.Oper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    0,
                    "",
                    "",
                    "",
                    cardFee.IStatus,
                    cardFee.Print_InvoiceNo,
                    ((int)cardFee.FeeType).ToString(),
                    cardFee.ClinicNO,
                    cardFee.Remark,
                    cardFee.Own_cost,
                    cardFee.Pub_cost,
                    cardFee.Pay_cost,
                    cardFee.PayType.ID);
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

        #region 作废卡费用表
        /// <summary>
        /// 作废卡费用表数据
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="transType"></param>
        /// <param name="feeType"></param>
        /// <returns></returns>
        public int CancelAccountCardFee(string invoice, TransTypes transType, AccCardFeeType feeType)
        {
            if (string.IsNullOrEmpty(invoice))
            {
                this.Err = "发票流水号为空！";

                return -1;
            }

            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Cancel", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Cancel 的Sql语句失败！";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql,
                    invoice,
                    ((int)transType).ToString(),
                    ((int)feeType).ToString());
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
        /// 退费卡费用信息
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int CancelAccountCardFeeByInvoice(string invoice)
        {
            if (string.IsNullOrEmpty(invoice))
            {
                this.Err = "发票流水号为空！";

                return -1;
            }

            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Cancel.ByInvoice", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Cancel.ByInvoice 的Sql语句失败！";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql, invoice);
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
        /// 退费卡费用信息
        /// </summary>
        /// <param name="?"></param>
        /// <param name="flag">0：无效 1：有效 2:退费 3：作废</param>
        /// <returns></returns>
        public int CancelAccountCardFeeByInvoice(string invoice, int flag)
        {
            if (string.IsNullOrEmpty(invoice))
            {
                this.Err = "发票流水号为空！";

                return -1;
            }

            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Cancel.ByInvoice.1", ref Sql) == -1)
            {
                #region 默认sql
                Sql = @"update fin_opb_accountcardfee a
   set a.cancel_flag ={1}
 where a.invoice_no = '{0}'";
                #endregion
            }
            try
            {
                Sql = string.Format(Sql, invoice, flag);
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


        #region 查询卡费用信息
        /// <summary>
        /// 查询卡费用信息 -- 直接收费记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccCardFeeDirectory(string cardNo, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            if (string.IsNullOrEmpty(cardNo))
            {
                this.Err = "参数不对！";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Where.2", ref strWhere) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Where.2 的Sql语句失败！";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, cardNo);

                iRes = this.QueryAccountCardFeeSQL(strWhere, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;

        }
        /// <summary>
        /// 查询被关联人的家庭成员信息// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="cardNo">被关联人病历号</param>
        /// <param name="validState">查询成员的状态</param>
        /// <param name="accountFamilyInfoList"></param>
        /// <returns></returns>
        public int GetAccountFamilyInfo(string cardNo, string validState, out List<AccountFamilyInfo> accountFamilyInfoList)
        {
            accountFamilyInfoList = null;
            if (string.IsNullOrEmpty(cardNo))
            {
                this.Err = "参数不对！";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountFamilyInfo.Where.1", ref strWhere) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.SelectAccountFamilyInfo.Where.1的Sql语句失败！";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, cardNo, validState);

                iRes = this.QueryAccountFamilyInfo(strWhere, out accountFamilyInfoList);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;

        }

        /// <summary>
        /// 根据关联人的卡号查询关联人的信息
        /// </summary>
        /// <param name="linkedardNo">关联人病历号</param>
        /// <param name="validState">查询成员的状态</param>
        /// <param name="accountFamilyInfoList"></param>
        /// <returns></returns>
        public int GetLinkedFamilyInfo(string linkedardNo, string validState, out List<AccountFamilyInfo> accountFamilyInfoList)
        {
            accountFamilyInfoList = null;
            if (string.IsNullOrEmpty(linkedardNo))
            {
                this.Err = "参数不对！";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountFamilyInfo.Where.2", ref strWhere) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.SelectAccountFamilyInfo.Where.2的Sql语句失败！";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, linkedardNo, validState);

                iRes = this.QueryAccountFamilyInfo(strWhere, out accountFamilyInfoList);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;

        }

        /// <summary>
        /// 根据家庭号查询家庭成员信息
        /// {793CA9DB-FD85-460a-B8B4-971C31FFAD45}
        /// </summary>
        /// <param name="linkedardNo">关联人病历号</param>
        /// <param name="validState">查询成员的状态</param>
        /// <param name="accountFamilyInfoList"></param>
        /// <returns></returns>
        public int GetFamilyInfoByCode(string familyCode, string validState, out List<AccountFamilyInfo> accountFamilyInfoList)
        {
            accountFamilyInfoList = null;
            if (string.IsNullOrEmpty(familyCode))
            {
                this.Err = "参数不对！";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountFamilyInfo.Where.3", ref strWhere) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.SelectAccountFamilyInfo.Where.3的Sql语句失败！";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, familyCode, validState);

                iRes = this.QueryAccountFamilyInfo(strWhere, out accountFamilyInfoList);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;

        }
        /// <summary>
        /// 查询卡卷信息// {E0683A80-F23C-4848-8482-7257F9263221}
        /// </summary>
        /// <param name="begVolumeNo"></param>
        /// <param name="endVolumeNo"></param>
        /// <param name="begTime"></param>
        /// <param name="endTime"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="validState"></param>
        /// <param name="operCode"></param>
        /// <param name="cardNo"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public ArrayList QueryAccountCardVolumeList(string begVolumeNo, string endVolumeNo, DateTime begTime, DateTime endTime, string invoiceNo, string validState, string operCode, string cardNo, string memo)
        {
            ArrayList accountCardVolumeList = new ArrayList();
            begTime = FS.FrameWork.Function.NConvert.ToDateTime(begTime.ToLongDateString() + " 00:00:00");
            endTime = FS.FrameWork.Function.NConvert.ToDateTime(endTime.ToLongDateString() + " 23:59:59");
            string sqlWhere = "";
            if (string.IsNullOrEmpty(validState))
            {
                return null;
            }
            else
            {
                sqlWhere = " Where (VALID = '{0}' or '{0}' = 'ALL') ";
            }

            if (begTime > endTime)
            {
                return null;
            }
            else
            {
                sqlWhere += " And BEG_DATE >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')  And END_DATE <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')  ";
            }

            if (!string.IsNullOrEmpty(begVolumeNo) && string.IsNullOrEmpty(endVolumeNo))
            {
                sqlWhere += " And VOLUME_NO like '%{3}%' ";
            }
            else if (string.IsNullOrEmpty(begVolumeNo) && !string.IsNullOrEmpty(endVolumeNo))
            {
                sqlWhere += " And VOLUME_NO like '%{4}%' ";
            }
            else if (!string.IsNullOrEmpty(begVolumeNo) && !string.IsNullOrEmpty(endVolumeNo))
            {
                sqlWhere += " And VOLUME_NO >= '{3}' And  VOLUME_NO <= '{4}'";
            }

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                sqlWhere += " And INVOICE_NO like '%{5}%' ";
            }
            if (!string.IsNullOrEmpty(operCode))
            {
                sqlWhere += " And OPER_CODE like '%{6}%' ";
            }

            if (!string.IsNullOrEmpty(cardNo))
            {
                sqlWhere += " And CARD_NO like '%{7}%' ";
            }
            if (!string.IsNullOrEmpty(memo))
            {
                sqlWhere += " And MARK like '%{8}%' ";
            }
            sqlWhere += " Order By VOLUME_NO";
            sqlWhere = string.Format(sqlWhere, validState, begTime.ToString(), endTime.ToString(), begVolumeNo, endVolumeNo, invoiceNo, operCode, cardNo, memo);

            accountCardVolumeList = this.QueryAccountCardVolume(sqlWhere);


            return accountCardVolumeList;

        }
        /// <summary>
        /// 查询卡费用信息 -- 指定挂号记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="clinicNo"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccCardFeeByClinic(string cardNo, string clinicNo, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            if (string.IsNullOrEmpty(cardNo) || string.IsNullOrEmpty(clinicNo))
            {
                this.Err = "参数不对！";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Where.3", ref strWhere) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Where.3 的Sql语句失败！";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, cardNo, clinicNo);

                iRes = this.QueryAccountCardFeeSQL(strWhere, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }

        /// <summary>
        /// 查询有效卡费用信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="markNo"></param>
        /// <param name="cardType"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccountCardFee(string cardNo, string markNo, string cardType, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            if (string.IsNullOrEmpty(cardNo) || string.IsNullOrEmpty(markNo) || string.IsNullOrEmpty(cardType))
            {
                this.Err = "参数不对！";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Where.1", ref strWhere) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Where.1 的Sql语句失败！";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, cardNo, markNo, cardType);

                iRes = this.QueryAccountCardFeeSQL(strWhere, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }
        /// <summary>
        /// 查询卡费用信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccountCardFee(string cardNo, out List<AccountCardFee> lstCardFee)
        {
            return QueryAccountCardFee(cardNo, "ALL", "ALL", out lstCardFee);
        }

        /// <summary>
        /// 查询挂号费用信息-通过病历号和时间
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="dsResult"></param>
        /// <returns></returns>
        public int QueryAccountCardFeeByCardNOAndDate(string cardNo, string begin, string end, ref DataSet dsResult)
        {
            dsResult = new DataSet();

            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Select.4", ref strSql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Select.4 的Sql语句失败！";
                return -1;
            }

            int iRes = 0;
            try
            {
                strSql = string.Format(strSql, cardNo, begin, end);

                iRes = this.ExecQuery(strSql, ref dsResult);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }

        /// <summary>
        /// 通过发票号查询费用信息
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccountCardFeeByInvoiceNO(string invoiceNO, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;
            if (string.IsNullOrEmpty(invoiceNO) || string.IsNullOrEmpty(invoiceNO))
            {
                this.Err = "参数不对！";
                return -1;
            }

            string strWhere = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Where.4", ref strWhere) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Where.4 的Sql语句失败！";
                return -1;
            }

            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, invoiceNO);

                iRes = this.QueryAccountCardFeeSQL(strWhere, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }
        /// <summary>
        /// 根据Card_No查询有效卡的数量// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public string QueryCardAountByCardNo(string CardNo)
        {
            string sqlstr = "";
            if (this.Sql.GetCommonSql("Fee.Account.QueryCardSum", ref sqlstr) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.QueryCardSum 的Sql语句失败！";
                return "";
            }
            sqlstr = string.Format(sqlstr, CardNo);
            return this.ExecSqlReturnOne(sqlstr, "");

        }

        /// <summary>
        /// 查询卡费用信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        private int QueryAccountCardFeeSQL(string sql, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;

            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.CardFee.Select", ref strSql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Select 的Sql语句失败！";
                return -1;
            }

            try
            {
                strSql = strSql + sql;

                if (this.ExecQuery(strSql) == -1)
                {
                    return -1;
                }

                lstCardFee = new List<AccountCardFee>();
                AccountCardFee cardFee = null;
                while (this.Reader.Read())
                {
                    cardFee = new AccountCardFee();
                    cardFee.InvoiceNo = this.Reader[0].ToString().Trim();
                    cardFee.TransType = this.Reader[1].ToString().Trim() == "1" ? TransTypes.Positive : TransTypes.Negative;
                    cardFee.MarkNO = this.Reader[2].ToString().Trim();
                    cardFee.MarkType.ID = this.Reader[3].ToString().Trim();
                    cardFee.Tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
                    cardFee.FeeOper.ID = this.Reader[5].ToString().Trim();
                    cardFee.FeeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
                    cardFee.Oper.ID = this.Reader[7].ToString().Trim();
                    cardFee.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                    cardFee.IsBalance = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    cardFee.BalanceNo = this.Reader[10].ToString().Trim();
                    cardFee.BalnaceOper.ID = this.Reader[11].ToString().Trim();
                    cardFee.BalnaceOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12]);
                    cardFee.IStatus = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[13]);
                    cardFee.CardNo = this.Reader[14].ToString().Trim();

                    cardFee.Print_InvoiceNo = this.Reader[15].ToString().Trim();
                    switch (this.Reader[16].ToString().Trim())
                    {
                        case "1":
                            cardFee.FeeType = AccCardFeeType.CardFee;
                            break;
                        case "2":
                            cardFee.FeeType = AccCardFeeType.CaseFee;
                            break;
                        case "3":
                            cardFee.FeeType = AccCardFeeType.RegFee;
                            break;
                        case "4":
                            cardFee.FeeType = AccCardFeeType.DiaFee;
                            break;
                        case "5":
                            cardFee.FeeType = AccCardFeeType.ChkFee;
                            break;
                        case "6":
                            cardFee.FeeType = AccCardFeeType.AirConFee;
                            break;
                        case "7":
                            cardFee.FeeType = AccCardFeeType.OthFee;
                            break;
                    }
                    cardFee.ClinicNO = this.Reader[17].ToString().Trim();
                    cardFee.Remark = this.Reader[18].ToString().Trim();
                    cardFee.PayType.ID = this.Reader[19].ToString();
                    cardFee.Own_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                    cardFee.Pub_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21].ToString());
                    cardFee.Pay_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22].ToString());

                    cardFee.Oper.Name = this.Reader[23].ToString().Trim();
                    cardFee.MarkType.Name = this.Reader[24].ToString().Trim();

                    lstCardFee.Add(cardFee);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// 查询卡卷信息// {E0683A80-F23C-4848-8482-7257F9263221}
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        private ArrayList QueryAccountCardVolume(string sqlWhere)
        {
            ArrayList cardVolumeList = new ArrayList();
            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.AccountCardVolume.Select", ref strSql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Select 的Sql语句失败！";
                return null;
            }

            try
            {
                strSql = strSql + " " + sqlWhere;

                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }

                FS.HISFC.Models.Account.CardVolume cardVolume = null;
                while (this.Reader.Read())
                {
                    cardVolume = new CardVolume();

                    cardVolume.ID = this.Reader[0].ToString();
                    cardVolume.VolumeNo = this.Reader[1].ToString();
                    cardVolume.Money = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
                    cardVolume.BegTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3]);
                    cardVolume.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                    cardVolume.UseType.Name = this.Reader[5].ToString();
                    cardVolume.InvoiceNo = this.Reader[6].ToString();
                    cardVolume.Patient.PID.CardNO = this.Reader[7].ToString();
                    cardVolume.Mark = this.Reader[8].ToString();
                    string validState = this.Reader[9].ToString();
                    if (validState == "0")
                    {
                        cardVolume.ValidState = EnumValidState.Invalid;
                    }
                    else if (validState == "1")
                    {
                        cardVolume.ValidState = EnumValidState.Valid;
                    }
                    cardVolume.CreateEnvironment.ID = this.Reader[10].ToString();
                    cardVolume.CreateEnvironment.Name = this.Reader[11].ToString();
                    cardVolume.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12]);
                    cardVolume.OperEnvironment.ID = this.Reader[13].ToString();
                    cardVolume.OperEnvironment.Name = this.Reader[14].ToString();
                    cardVolume.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15]);

                    cardVolumeList.Add(cardVolume);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return cardVolumeList;
        }

        /// <summary>
        /// 查询积分账户// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.CardCoupon QueryCardCouponByCardNo(string cardNo)
        {
            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.QueryCoupon", ref strSql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.QueryCoupon 的Sql语句失败！";
                return null;
            }

            strSql = string.Format(strSql, cardNo);
            FS.HISFC.Models.Account.CardCoupon cardCoupon = new CardCoupon();
            try
            {
                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (this.Reader.Read())
                {
                    cardCoupon = new CardCoupon();

                    cardCoupon.ID = this.Reader[0].ToString();
                    cardCoupon.CardNo = this.Reader[1].ToString();
                    cardCoupon.CouponVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
                    cardCoupon.CouponAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());

                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return cardCoupon;
        }
        /// <summary>
        /// 查询兑换记录// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="exchangeGoods"></param>
        /// <param name="mark"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="operCode"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList QueryCardCouponRecord(string cardNo, string exchangeGoods, string mark, string startTime, string endTime, string operCode, string invoiceNo, string couponType)
        {
            string sqlWhere = "";
            if (!string.IsNullOrEmpty(cardNo))
            {
                sqlWhere += " and a.card_no = '" + cardNo + "'";
            }

            if (!string.IsNullOrEmpty(exchangeGoods))
            {
                sqlWhere += " and a.EXCHANGE_GOODS = '" + exchangeGoods + "'";
            }

            if (!string.IsNullOrEmpty(mark))
            {
                sqlWhere += " and a.REMARK = '" + mark + "'";
            }

            if (!string.IsNullOrEmpty(startTime))
            {
                sqlWhere += " and a.OPER_DATE >= to_date('" + startTime + "','yyyy-mm-dd hh24:mi:ss')";
            }

            if (!string.IsNullOrEmpty(endTime))
            {
                sqlWhere += " and a.OPER_DATE <= to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')";
            }

            if (!string.IsNullOrEmpty(operCode))
            {
                sqlWhere += " and a.OPER_CODE = '" + operCode + "' or 'ALL' = '" + operCode + "'";
            }

            if (!string.IsNullOrEmpty(invoiceNo))
            {
                sqlWhere += " and a.INVOICE_NO like '%" + invoiceNo + "'";
            }

            if (!string.IsNullOrEmpty(couponType))
            {
                sqlWhere += " and a.OPERTYPE = '" + couponType + "'";
            }
            return QueryCardCouponRecord(sqlWhere);
        }

        /// <summary>
        /// 查询积分账户// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        private ArrayList QueryCardCouponRecord(string sqlWhere)
        {
            ArrayList cardCouponRecordList = new ArrayList();
            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.QueryCouponRecord", ref strSql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.QueryCouponRecord 的Sql语句失败！";
                return null;
            }

            try
            {
                strSql = strSql + " " + sqlWhere;

                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }

                FS.HISFC.Models.Account.CardCouponRecord couponRecord = null;
                while (this.Reader.Read())
                {
                    couponRecord = new CardCouponRecord();

                    couponRecord.ID = this.Reader[0].ToString();
                    couponRecord.CardNo = this.Reader[1].ToString();
                    couponRecord.Name = this.Reader[2].ToString();
                    couponRecord.Money = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
                    couponRecord.Coupon = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
                    couponRecord.CouponVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                    couponRecord.InvoiceNo = this.Reader[6].ToString();
                    couponRecord.OperType = this.Reader[7].ToString();
                    couponRecord.ExchangeGoods = this.Reader[8].ToString();
                    couponRecord.Memo = this.Reader[9].ToString();
                    couponRecord.OperEnvironment.ID = this.Reader[10].ToString();
                    couponRecord.OperEnvironment.Name = this.Reader[11].ToString();
                    couponRecord.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12]);

                    cardCouponRecordList.Add(couponRecord);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return cardCouponRecordList;
        }
        /// <summary>
        /// 查询家庭成员信息// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="accountFamilyInfoList"></param>
        /// <returns></returns>
        private int QueryAccountFamilyInfo(string sqlWhere, out List<AccountFamilyInfo> accountFamilyInfoList)
        {
            accountFamilyInfoList = null;

            string strSql = string.Empty;

            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountFamilyInfo.Select", ref strSql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Select 的Sql语句失败！";
                return -1;
            }

            try
            {
                strSql = strSql + " " + sqlWhere;

                if (this.ExecQuery(strSql) == -1)
                {
                    return -1;
                }

                accountFamilyInfoList = new List<AccountFamilyInfo>();
                AccountFamilyInfo accountFamilyInfo = null;
                while (this.Reader.Read())
                {
                    accountFamilyInfo = new AccountFamilyInfo();
                    accountFamilyInfo.ID = this.Reader[0].ToString();
                    accountFamilyInfo.LinkedCardNO = this.Reader[1].ToString();
                    accountFamilyInfo.LinkedAccountNo = this.Reader[2].ToString();
                    accountFamilyInfo.Relation.ID = this.Reader[3].ToString();
                    accountFamilyInfo.Relation.Name = this.Reader[4].ToString();
                    accountFamilyInfo.Name = this.Reader[5].ToString();
                    accountFamilyInfo.Sex.ID = this.Reader[6].ToString();
                    string sexID = this.Reader[6].ToString();
                    string sexName = "";
                    if (sexID == "M")
                    {
                        sexName = "男";
                    }
                    else if (sexID == "F")
                    {
                        sexName = "女";
                    }
                    else
                    {
                        sexName = "保密";
                    }
                    accountFamilyInfo.Sex.Name = sexName;
                    accountFamilyInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7]);
                    accountFamilyInfo.CardType.ID = this.Reader[8].ToString();
                    accountFamilyInfo.CardType.Name = this.Reader[9].ToString();
                    accountFamilyInfo.IDCardNo = this.Reader[10].ToString();
                    accountFamilyInfo.Phone = this.Reader[11].ToString();
                    accountFamilyInfo.Address = this.Reader[12].ToString();
                    accountFamilyInfo.CardNO = this.Reader[13].ToString();
                    accountFamilyInfo.AccountNo = this.Reader[14].ToString();
                    string validState = this.Reader[15].ToString();
                    if (validState == "0")
                    {
                        accountFamilyInfo.ValidState = EnumValidState.Invalid;
                    }
                    else if (validState == "1")
                    {
                        accountFamilyInfo.ValidState = EnumValidState.Valid;
                    }

                    accountFamilyInfo.CreateEnvironment.ID = this.Reader[16].ToString();
                    accountFamilyInfo.CreateEnvironment.Name = this.Reader[17].ToString();
                    accountFamilyInfo.CreateEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18]);
                    accountFamilyInfo.OperEnvironment.ID = this.Reader[19].ToString();
                    accountFamilyInfo.OperEnvironment.Name = this.Reader[20].ToString();
                    accountFamilyInfo.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[21]);
                    accountFamilyInfo.FamilyCode = this.Reader[22].ToString();
                    accountFamilyInfo.FamilyName = this.Reader[23].ToString();
                    accountFamilyInfoList.Add(accountFamilyInfo);
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return 1;
        }
        #endregion


        #region 查找卡使用记录
        /// <summary>
        /// 查找卡使用记录
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCardRecord> GetAccountCardRecord(string cardNO, string begin, string end)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountCardRecord", ref Sql) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return null;
            }
            try
            {
                Sql = string.Format(Sql, cardNO, begin, end);
                if (this.ExecQuery(Sql) == -1)
                {
                    this.Err = "查找卡使用数据失败！";
                    return null;
                }
                List<FS.HISFC.Models.Account.AccountCardRecord> list = new List<FS.HISFC.Models.Account.AccountCardRecord>();
                FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;
                while (this.Reader.Read())
                {
                    accountCardRecord = new FS.HISFC.Models.Account.AccountCardRecord();
                    accountCardRecord.MarkNO = Reader[0].ToString();
                    accountCardRecord.MarkType.ID = Reader[1].ToString();
                    accountCardRecord.CardNO = Reader[2].ToString();
                    accountCardRecord.OperateTypes.ID = Reader[3];
                    accountCardRecord.Oper.ID = Reader[4].ToString();
                    accountCardRecord.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5]);
                    accountCardRecord.CardMoney = FS.FrameWork.Function.NConvert.ToDecimal(Reader[6]);
                    list.Add(accountCardRecord);
                }
                this.Reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        #endregion

        #region 删除卡数据
        /// <summary>
        /// 删除卡数据
        /// </summary>
        /// <param name="markNO">卡号</param>
        /// <param name="markType">卡类型</param>
        /// <returns></returns>
        public int DeleteAccoutCard(string markNO, string markType)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.DeleteAccountCard", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, markNO, markType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }

        #endregion

        #region 帐户换卡
        /// <summary>
        /// 帐户换卡
        /// </summary>
        /// <param name="newMark">新卡号</param>
        /// <param name="oldMark">原</param>
        /// <returns></returns>
        public int UpdateAccountCardMark(string newMark, string oldMark)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountCardMarkNo", newMark, oldMark);
        }

        #endregion

        #region 更新账户金额和写入记录// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm

        /// <summary>
        /// 更新账户金额和写入记录
        /// </summary>
        /// <param name="patient">授权人</param>
        /// <param name="accountNo">账号</param>
        /// <param name="acountTypeCode">账户类型</param>
        /// <param name="baseCost">基本金额</param>
        /// <param name="donateCost">赠送金额</param>
        /// <param name="payInvoiceNo">对应消费发票号</param>
        /// <param name="empowerPatient">被授权人</param>
        /// <param name="payWayTypes">消费类型：P购买套餐；R门诊挂号；C门诊消费；I住院结算;M套餐押金</param>
        /// <param name="aMod">0退费1消费</param>
        /// <returns></returns>
        public int UpdateAccountAndWriteRecord(HISFC.Models.RADT.Patient patient, string accountNo, string acountTypeCode, decimal baseCost, decimal donateCost, string payInvoiceNo, HISFC.Models.RADT.Patient empowerPatient, PayWayTypes payWayTypes, int aMod)
        {
            //消费时，判断余额
            if (aMod == 1)
            {
                // {0304EC3C-ECA4-4b90-8040-5EBEC93F2EA5}
                AccountDetail currAccountDetail = new AccountDetail();
                currAccountDetail = this.GetAccountDetail(accountNo, acountTypeCode, "1")[0] as AccountDetail;
                if (currAccountDetail.BaseVacancy < baseCost)
                {
                    this.Err = "基本账户余额不足！";
                    return -1;
                }
                if (currAccountDetail.DonateVacancy < donateCost)
                {
                    this.Err = "赠送账户余额不足！";
                    return -1;
                }
            }



            AccountDetail accountDetail = new AccountDetail();

            //消费对账户累计不做影响
            accountDetail.BaseAccumulate = 0;
            accountDetail.DonateAccumulate = 0;
            accountDetail.CouponAccumulate = 0;
            accountDetail.ID = accountNo;
            accountDetail.AccountType.ID = acountTypeCode;
            accountDetail.BaseCost = baseCost;
            accountDetail.DonateCost = donateCost;
            accountDetail.OperEnvironment.ID = this.Operator.ID;
            accountDetail.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();


            #region 更新账户类型明细表信息余额
            if (this.UpdateAccountDetail(accountDetail) <= 0)
            {
                this.Err = "更新账户明细表信息余额失败！";
                return -1;
            }

            #endregion

            #region 更新账户余额
            FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();
            account.ID = accountDetail.ID;
            account.BaseCost = accountDetail.BaseCost;
            account.DonateCost = accountDetail.DonateCost;
            account.CouponCost = 0;
            account.BaseAccumulate = 0;
            account.DonateAccumulate = 0;
            account.CouponAccumulate = 0;
            account.OperEnvironment.ID = this.Operator.ID;
            account.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();

            if (this.UpdateAccountVacancyEX(account) <= 0)
            {
                this.Err = "更新账户余额失败！";
                return -1;
            }
            #endregion

            #region 插入消费记录

            List<AccountDetail> accountDetailList1 = new List<AccountDetail>();
            accountDetailList1 = this.GetAccountDetail(accountDetail.ID, accountDetail.AccountType.ID, "1");
            if (accountDetailList1.Count <= 0)
            {
                this.Err = "获取账户明细表余额失败！";
                return -1;
            }
            AccountDetail accountDetail2 = accountDetailList1[0] as AccountDetail;
            FS.HISFC.Models.Account.AccountRecord accountRecord = new FS.HISFC.Models.Account.AccountRecord();
            accountRecord = accountDetail.AccountRecord.Clone();
            accountDetail.AccountRecord.Patient = patient;
            accountRecord.EmpowerPatient = empowerPatient;
            accountRecord.PayInvoiceNo = payInvoiceNo;
            if (aMod == 0)
            {
                accountRecord.OperType.ID = "5";
            }
            else if (aMod == 1)
            {
                accountRecord.OperType.ID = "4";
            }

            if (accountDetail.AccountRecord.Patient != null)
            {
                accountRecord.Patient = accountDetail.AccountRecord.Patient.Clone();
                accountRecord.CardNo = accountDetail.AccountRecord.Patient.PID.CardNO;
                accountRecord.Name = accountDetail.AccountRecord.Patient.Name;
            }
            else
            {
                this.Err = "授权人信息为空！";
                return -1;
            }
            if (accountRecord.EmpowerPatient == null)
            {
                this.Err = "被授权人信息为空！";
                return -1;
            }
            if (string.IsNullOrEmpty(accountRecord.PayInvoiceNo))
            {
                this.Err = "消费发票号不能为空！";
                return -1;
            }
            accountRecord.AccountNO = accountDetail.ID;
            accountRecord.ID = accountDetail.ID;
            accountRecord.AccountType = accountDetail.AccountType;
            accountRecord.BaseCost = accountDetail.BaseCost;//退费、消费
            accountRecord.DonateCost = accountDetail.DonateCost;//消费、退费
            accountRecord.BaseVacancy = accountDetail2.BaseVacancy;//余额
            accountRecord.DonateVacancy = accountDetail2.DonateVacancy;//余额
            accountRecord.IsValid = true;
            accountRecord.FeeDept.ID = ((FS.HISFC.Models.Base.Employee)this.Operator).Dept.ID; ////{1C42FA6C-C70A-4cd4-82C4-9FA1FCABD73B}
            accountRecord.Oper.ID = this.Operator.ID;
            accountRecord.OperTime = this.GetDateTimeFromSysDateTime();
            accountRecord.EmpowerCost = accountDetail.BaseVacancy + accountDetail.DonateVacancy;
            accountRecord.PayType.ID = payWayTypes;
            if (this.InsertAccountRecordEX(accountRecord) <= 0)
            {
                this.Err = "插入消费记录失败！";
                return -1;
            }
            #endregion

            return 1;
        }

        #endregion

        #region 卡卷状态更新
        /// <summary>
        /// 卡卷状态更新// {E0683A80-F23C-4848-8482-7257F9263221}
        /// </summary>
        /// <param name="volumeNo"></param>
        /// <param name="OldValidState"></param>
        /// <param name="NewValidState"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateCardVolumeState(string volumeNo, string OldValidState, string NewValidState, string operCode, DateTime operDate)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountCardVolumeState", volumeNo, OldValidState, NewValidState, operCode, operDate.ToString());
        }
        #endregion
        #region 家庭成员关系状态修改
        /// <summary>
        /// 家庭成员关系状态修改
        /// </summary>
        /// <param name="seqNo"></param>
        /// <param name="validState"></param>
        /// <returns></returns>
        public int UpdateAccountFamilyInfoState(string seqNo, string validState, string operCode, string date)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountFamilyInfo", seqNo, validState, operCode, date);
        }

        #endregion

        #region 根据卡号规则读出卡号
        ///// <summary>
        ///// 根据卡号规则读出卡号
        ///// </summary>
        ///// <param name="markNo">输入的卡号</param>
        ///// <param name="validedMarkNo"></param>
        ///// <returns></returns>
        //public int ValidMarkNO(string markNo, ref string validedMarkNo)
        //{
        //    string firstleter = markNo.Substring(0, 1);
        //    string lastleter = markNo.Substring(markNo.Length - 1, 1);
        //    if (firstleter != ";")
        //    {
        //        this.Err = "请输入正确的卡号！";
        //        return -1;
        //    }
        //    if (lastleter != "?")
        //    {
        //        this.Err = "请输入正确的卡号！";
        //        return -1;
        //    }
        //    validedMarkNo = markNo.Substring(1, markNo.Length - 2);
        //    if (!FS.FrameWork.Public.String.IsNumeric(validedMarkNo))
        //    {
        //        this.Err = "请输入正确的卡号！";
        //        return -1;
        //    }
        //    return 1;
        //}

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetMaxMcard(string type)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetMAXMCardNO", ref sql) == -1)
            {
                this.Err = "查询索引为Fee.Account.GetMAXMCardNO的SQL语句失败！";
                return null;
            }
            try
            {

                string maxid = ExecSqlReturnOne(sql);
                int maxnum = int.Parse(maxid);
                maxnum = maxnum + 1;
                maxid = maxnum.ToString().PadLeft(5, '0');
                return "T" + maxid;

            }
            catch
            {
                throw new Exception("错误");
            }
        }

        #endregion

        #endregion

        #region 帐户交易数据操作
        /// <summary>
        /// 帐户支付、退费管理、取现管理
        /// </summary>
        /// <param name="patient">患者</param>
        /// <param name="money">金额</param>
        /// <param name="reMark">标识</param>
        /// <param name="invoiceType">发票类型</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="aMod">0收费 1退费 2取现-授权帐户无法取现</param>
        /// <returns></returns>
        public bool AccountPayManager(HISFC.Models.RADT.Patient patient, decimal money, string reMark, string invoiceType, string deptCode, int aMod)
        {
            string strSeqNo = null;

            return AccountPayManager(patient, money, reMark, invoiceType, deptCode, aMod, out strSeqNo);
        }
        /// <summary>
        /// 帐户支付、退费管理、取现管理， 并返回交易流水号
        /// {48508EFF-7D63-42d4-AF73-87C5645B9D7E}
        /// </summary>
        /// <param name="patient">患者</param>
        /// <param name="money">金额</param>
        /// <param name="reMark">标识</param>
        /// <param name="invoiceType">发票类型</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="aMod">0收费 1退费 2取现-授权帐户无法取现</param>
        /// <param name="strSeqNo">返回交易流水号</param>
        /// <returns></returns>
        public bool AccountPayManager(HISFC.Models.RADT.Patient patient, decimal money, string reMark, string invoiceType, string deptCode, int aMod, out string strSeqNo)
        {
            strSeqNo = null;

            //帐户余额，如果是帐户则是帐户余额；如果是授权信息则返回授权信息的余额
            decimal vacancy = 0m;
            //授权信息
            HISFC.Models.Account.AccountEmpower accountEmpower = null;
            //帐户信息
            HISFC.Models.Account.Account account = null;

            HISFC.Models.RADT.Patient tempPaient = new FS.HISFC.Models.RADT.Patient();

            #region 查询余额
            //-1失败 0没有 帐户或授权信息 1帐户帐户 2授权信息
            int result = this.GetVacancy(patient.PID.CardNO, ref vacancy);
            if (result <= 0)
            {
                return false;
            }
            #endregion

            #region 查询帐户信息
            if (result == 1)
            {
                //tempCardNO = patient.PID.CardNO;
                tempPaient = patient;
            }
            else
            {
                // 取现
                // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                if (aMod == 2)
                {
                    this.Err = "授权帐户无法取现!";
                    return false;
                }
                //获得授权信息
                int resultValue = this.QueryAccountEmpowerByEmpwoerCardNO(patient.PID.CardNO, ref accountEmpower);
                if (resultValue <= 0)
                {

                    return false;
                }
                tempPaient = accountEmpower.AccountCard.Patient;
            }
            account = this.GetAccountByCardNoEX(tempPaient.PID.CardNO);//获得帐户信息
            if (account == null)
            {
                this.Err = "该患者不存在有效帐户！";
                return false;
            }
            #endregion

            #region 判断判断
            //在收费的时候判断
            if (aMod == 0)
            {
                #region 支付操作判断帐户余额是否够
                if (vacancy < money)
                {
                    this.Err = FS.FrameWork.Management.Language.Msg("余额：" + vacancy.ToString() + "元，不足本次扣费金额：" + money.ToString() + "！");
                    return false;
                }
                //授权信息
                if (result == 2)
                {
                    //在授权信息的余额大于费用金额，但授权的帐户余额小于费用的金额给出提示
                    if (account.BaseVacancy < money)
                    {
                        this.Err = "授权帐户的余额为：" + account.BaseVacancy.ToString() + "元，不足本次扣费金额：" + money.ToString() + "元";
                        return false;
                    }
                }
                #endregion
            }
            else if (aMod == 2)
            {
                // 取现
                // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                #region 取现操作判断帐户余额是否够
                if (vacancy < money)
                {
                    this.Err = FS.FrameWork.Management.Language.Msg("余额" + vacancy.ToString() + "不足" + money.ToString() + "！");
                    return false;
                }
                #endregion
            }

            #endregion
            try
            {
                #region 生成交易记录
                //生成交易记录
                FS.HISFC.Models.Account.AccountRecord accountRecord = new FS.HISFC.Models.Account.AccountRecord();
                //形成交易记录
                accountRecord.Patient = tempPaient;
                accountRecord.AccountNO = account.ID;//帐号
                if (result == 1)
                {
                    if (aMod == 0)
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.Pay;//操作类型
                    }
                    else if (aMod == 1)
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.CancelPay;//操作类型
                    }
                    else if (aMod == 2)
                    {
                        // 账户取现
                        // {4679504A-CEDA-44a8-8C67-DB7F847C6450}
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.AccountTaken;
                    }
                }
                else
                {
                    if (aMod == 0)
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.EmpowerPay;//操作类型
                    }
                    if (aMod == 1)
                    {
                        accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.EmpowerCancelPay;//操作类型
                    }
                    //被授权患者实体
                    accountRecord.EmpowerPatient = accountEmpower.EmpowerCard.Patient;
                }
                accountRecord.BaseCost = -money;//金额
                accountRecord.FeeDept.ID = deptCode;//科室
                accountRecord.Oper.ID = this.Operator.ID;//操作员
                accountRecord.OperTime = this.GetDateTimeFromSysDateTime();//操作时间
                accountRecord.ReMark = reMark;//发票号
                accountRecord.IsValid = true;//是否有效
                accountRecord.BaseVacancy = account.BaseVacancy - money;//本次交易余额
                accountRecord.InvoiceType.ID = invoiceType;
                if (!string.IsNullOrEmpty(patient.HomeZip))
                {
                    accountRecord.Patient.HomeZip = patient.HomeZip;
                }
                else
                {
                    accountRecord.Patient.HomeZip = "";
                }
                //保存帐户交易记录
                //strSeqNo = accountRecord.ID;
                if (this.InsertAccountRecordEX(accountRecord) == -1)
                {
                    this.Err = "插入交易数据失败！" + this.Err;
                    return false;
                }
                #endregion

                #region 更新余额
                //更新被是授权帐户的余额
                if (result == 2)
                {

                    if (UpdateEmpowerVacancy(account.ID, patient.PID.CardNO, money) <= 0)
                    {
                        this.Err = "更新授权信息余额失败！";
                        return false;
                    }
                }
                //更新帐户余额
                if (UpdateAccountVacancy(account.ID, money) <= 0)
                {
                    this.Err = "更新帐户余额失败！";
                    return false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return false;
            }

            return true;
        }

        #region 帐户预交金
        /// <summary>
        /// 帐户预交金
        /// </summary>
        /// <param name="accountRecord">交易实体</param>
        /// <param name="aMod">1收取 0反还</param>
        /// <returns></returns>
        public bool AccountPrePayManager(PrePay prePay, int aMode)
        {
            try
            {
                //返还
                if (aMode == 0)
                {
                    prePay.BaseCost = -prePay.BaseCost;
                    prePay.DonateCost = -prePay.DonateCost;
                    //prePay.OldInvoice = prePay.InvoiceNO;
                    //if (UpdatePrePayState(prePay) < 1)
                    //{
                    //    this.Err = this.Err + "该条记录已经进行过返还补打操作，更新状态出错!";
                    //    return false;
                    //}
                }

                if (this.InsertPrePay(prePay) < 0)
                {
                    this.Err = "插入预交金数据失败！";
                    return false;
                }

                #region 插入交易记录

                decimal vacancy = 0;
                int result = this.GetVacancy(prePay.Patient.PID.CardNO, ref vacancy);
                if (result <= 0)
                {
                    return false;
                }
                #region 交易实体

                AccountRecord accountRecord = new AccountRecord();
                accountRecord.Patient.PID.CardNO = prePay.Patient.PID.CardNO; //门诊卡号
                accountRecord.FeeDept.ID = (Operator as FS.HISFC.Models.Base.Employee).Dept.ID; //科室
                accountRecord.Oper.ID = this.Operator.ID; //操作员
                accountRecord.OperTime = prePay.PrePayOper.OperTime; //日期
                accountRecord.IsValid = true;//交易状态
                accountRecord.AccountNO = prePay.AccountNO;//帐号
                accountRecord.Name = prePay.Patient.Name;//姓名
                if (aMode == 0)
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.CancelPrePay;//操作类型

                }
                else
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.PrePay;//操作类型
                }
                accountRecord.BaseCost = prePay.BaseCost;//金额
                accountRecord.ReMark = prePay.InvoiceNO;//发票号
                accountRecord.BaseVacancy = prePay.BaseVacancy;//本次交易余额
                //accountRecord.Money = prePay.FT.PrepayCost;
                accountRecord.InvoiceType.ID = "A";
                #endregion

                if (this.InsertAccountRecord(accountRecord) < 0)
                {
                    return false;
                }
                #endregion

                #region 更新帐户余额
                //在计算帐户余额时是余额-本次交易的钱
                decimal consumeMoney = -accountRecord.BaseCost;

                if (this.UpdateAccountVacancy(accountRecord.AccountNO, consumeMoney) < 0)
                {
                    return false;
                }

                #endregion

                return true;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return false;
            }

        }
        /// <summary>
        /// 帐户预交金充值
        /// NEW// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="accountRecord">交易实体</param>
        /// <param name="aMod">1收取 0反还</param>
        /// <returns></returns>
        public bool AccountPrePayManagerEX(PrePay prePay, int aMode)
        {
            try
            {
                if (prePay == null)
                {
                    this.Err = "预交金数据为空！";
                    return false;
                }
                if (this.InsertPrePayEX(prePay) < 0)//lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                {
                    this.Err = "插入预交金数据失败！";
                    return false;
                }

                #region 插入交易记录

                #region 交易实体

                AccountRecord accountRecord = new AccountRecord();
                accountRecord.Patient.PID.CardNO = prePay.Patient.PID.CardNO; //门诊卡号
                accountRecord.FeeDept.ID = (Operator as FS.HISFC.Models.Base.Employee).Dept.ID; //科室
                accountRecord.Oper.ID = this.Operator.ID; //操作员
                accountRecord.OperTime = prePay.PrePayOper.OperTime; //日期
                accountRecord.IsValid = true;//交易状态
                accountRecord.AccountNO = prePay.AccountNO;//帐号
                accountRecord.Name = prePay.Patient.Name;//姓名
                if (aMode == 0)
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.CancelPrePay;//操作类型
                }
                else if (aMode == 10)// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.BalanceVacancy;//操作类型
                    if (UpdatePrePayHistory(prePay.AccountNO, false, true) < 0)// {74621CF6-D26C-44aa-87DC-C68D0867BAC5}
                    {
                        Err = "更新账户记录状态失败！" + Err;
                        return false;
                    }
                }
                else
                {
                    accountRecord.OperType.ID = (int)FS.HISFC.Models.Account.OperTypes.PrePay;//操作类型
                }
                // {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                accountRecord.BaseCost = prePay.BaseCost;//金额
                accountRecord.DonateCost = prePay.DonateCost;
                accountRecord.InvoiceType.ID = "A";
                accountRecord.BaseVacancy = prePay.BaseVacancy;//基本账户交易余额
                accountRecord.DonateVacancy = prePay.DonateVacancy;
                accountRecord.AccountType.ID = prePay.AccountType.ID;//账户类型
                accountRecord.InvoiceNo = prePay.InvoiceNO;//发票号
                accountRecord.ReMark = prePay.Memo;
                accountRecord.PayInvoiceNo = prePay.Bank.InvoiceNO;
                if (this.InsertAccountRecordEX(accountRecord) < 0)
                {
                    return false;
                }
                #endregion
                #endregion

                #region 更新帐户余额
                //在计算帐户余额时是余额-本次交易的钱
                //lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                //decimal consumeMoney = -accountRecord.Money;

                FS.HISFC.Models.Account.Account account2 = new FS.HISFC.Models.Account.Account();
                account2.ID = accountRecord.AccountNO;
                account2.BaseCost = prePay.BaseCost;
                account2.DonateCost = prePay.DonateCost;
                if (aMode == 0 || aMode == 10)
                {
                    account2.BaseAccumulate = 0;//返还累计金额不做处理
                    account2.DonateAccumulate = 0;
                    account2.CouponAccumulate = 0;
                }
                else
                {
                    account2.BaseAccumulate = prePay.BaseCost;
                    account2.DonateAccumulate = prePay.DonateCost;
                }
                account2.CouponCost = 0;//积分暂时不处理
                account2.Limit = 0;//警戒值暂时不处理
                account2.OperEnvironment.ID = this.Operator.ID;
                account2.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();
                if (this.UpdateAccountVacancyEX(account2) < 0)
                {
                    return false;
                }
                #endregion

                #region 更新对应帐户类型明细余额
                FS.HISFC.Models.Account.AccountDetail accountDetail = new AccountDetail();
                accountDetail.ID = prePay.AccountNO;//账号
                accountDetail.AccountType.ID = prePay.AccountType.ID;//账户类型
                accountDetail.CardNO = prePay.Patient.PID.CardNO;
                accountDetail.BaseCost = prePay.BaseCost;//充值金额
                accountDetail.DonateCost = prePay.DonateCost;//赠送金额
                if (aMode == 0 || aMode == 10)
                {
                    accountDetail.BaseAccumulate = 0;//返还累计金额不做处理
                    accountDetail.DonateAccumulate = 0;
                    accountDetail.CouponAccumulate = 0;
                }
                else
                {
                    accountDetail.BaseAccumulate = prePay.BaseCost;
                    accountDetail.DonateAccumulate = prePay.DonateCost;
                }
                accountDetail.OperEnvironment.ID = this.Operator.ID;
                accountDetail.OperEnvironment.OperTime = this.GetDateTimeFromSysDateTime();
                accountDetail.CouponCost = 0;//积分暂不处理
                if (this.UpdateAccountDetail(accountDetail) < 0)
                {
                    return false;
                }
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return false;
            }

        }
        /// <summary>
        /// 插入预交金数据
        /// </summary>
        /// <param name="prePay">预交金实体</param>
        /// <returns>1成功 -1失败</returns>
        public int InsertPrePay(PrePay prePay)
        {
            return this.UpdateSingTable("Fee.Account.InsertAccountPrePay", GetPrePayArgs(prePay));
        }
        /// <summary>
        /// 插入预交金数据NEW
        /// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="prePay">预交金实体</param>
        /// <returns>1成功 -1失败</returns>
        public int InsertPrePayEX(PrePay prePay)
        {
            return this.UpdateSingTable("Fee.Account.InsertAccountPrePay.1", GetPrePayArgsEX(prePay));
        }

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// 根据时间段查询预交金数据
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="isHistory">1历史数据 0当前数据 ALL全部数据</param>
        /// <returns>null失败</returns>
        public List<PrePay> GetPrepayByAccountNO(string accountNO, string isHistory)
        {
            return this.GetPrePayList("Fee.Account.GetPrePayWhere1", accountNO, isHistory);
        }

        /// <summary>
        /// 根据账号查询预交金数据// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
        /// </summary>
        /// <param name="accountNO">账号</param>
        /// <param name="isHistory">1历史数据 0当前数据 ALL全部数据</param>
        /// <returns>null失败</returns>
        public List<PrePay> GetPrepayByAccountNOEX(string accountNO, string isHistory)
        {
            return this.GetPrePayListEX("Fee.Account.GetPrePayWhere1", accountNO, isHistory);
        }
        /// <summary>
        /// 根据账号和账户类型查找预交金数据
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="isHistory">1历史数据 0当前数据 ALL全部数据</param>
        /// <returns>null失败</returns>
        public List<PrePay> GetPrepayByAccountNOAndType(string accountNO, string AccountType, string isHistory)
        {
            return this.GetPrePayListEX("Fee.Account.GetPrePayWhere.2", accountNO, AccountType, isHistory);
        }

        /// <summary>
        /// 根据就诊卡号查询医保项目明细
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public List<AccountMedicalInsurance> GetMedicalInsuranceByCardNo(string cardNO)
        {
            return this.GetMedicalInsuranceEX("Fee.Account.GetMedicalInsurance", cardNO);
        }

        /// <summary>
        /// 根据就诊卡号查询医保项目明细
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public int UpdateMedicalInsuranceByCardNo(string cardNO, string operId)
        {
            return this.UpdateMedicalByCardNo("Fee.Account.UpdateMedicalInsurance", cardNO, operId);
        }

        /// <summary>
        /// 更新预交金状态 --更新为作废或补打状态
        /// </summary>
        /// <param name="prePay">预交金实体</param>
        /// <returns>1成功 -1失败 0没有更新记录</returns>
        public int UpdatePrePayState(PrePay prePay)
        {
            return this.UpdateSingTable("Fee.Account.UpdatePrePayState", prePay.AccountNO, prePay.HappenNO.ToString(), ((int)prePay.ValidState).ToString());
        }

        /// <summary>
        /// 更新帐户预交金历史数据状态
        /// </summary>
        /// <returns></returns>
        public int UpdatePrePayHistory(string accountNO, bool currentState, bool updateState)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountPrePayHistoryState", accountNO, NConvert.ToInt32(currentState).ToString(), NConvert.ToInt32(updateState).ToString());
        }
        #endregion

        #region  通过物理卡号查找门诊卡号
        /// <summary>
        /// 通过物理卡号查找门诊卡号
        /// </summary>
        /// <param name="markNo">物理卡号</param>
        /// <param name="markType">卡类型</param>
        /// <param name="cardNo">门诊卡号</param>
        /// <returns>bool true 成功　false 失败</returns>
        public bool GetCardNoByMarkNo(string markNo, NeuObject markType, ref string cardNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectCardNoByMarkNo", ref Sql) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return false;
            }
            try
            {
                Sql = string.Format(Sql, markNo, markType.ID);
                if (this.ExecQuery(Sql) == -1)
                {
                    this.Err = "查找数据失败！";
                    return false;
                }
                #region Sql
                /*select b.card_no,
                           b.markno,
                           b.type,
                           b.state as cardstate,
                           a.state as accountstate,
                           a.vacancy 
                    from fin_opb_account a,fin_opb_accountcard b 
                    where a.card_no=b.card_no 
                      and b.markno='{0}' 
                      and type='{1}'*/
                #endregion
                FS.HISFC.Models.Account.Account account = null;
                while (this.Reader.Read())
                {
                    account = new FS.HISFC.Models.Account.Account();
                    account.AccountCard.Patient.PID.CardNO = this.Reader[0].ToString();
                    account.AccountCard.MarkNO = this.Reader[1].ToString();
                    account.AccountCard.MarkType.ID = Reader[2].ToString();
                    if (this.Reader[3].ToString() == "0")
                    {
                        account.AccountCard.IsValid = false;
                    }
                    else
                    {
                        account.AccountCard.IsValid = true;
                    }
                }
                this.Reader.Close();
                if (account == null)
                {
                    this.Err = "该卡" + markNo + "已被取消使用！";
                    return false;
                }
                if (!account.AccountCard.IsValid)
                {
                    this.Err = "该卡" + markNo + "已被停止使用！";
                    return false;
                }
                cardNo = account.AccountCard.Patient.PID.CardNO;

                return true;
            }
            catch (Exception ex)
            {
                this.Err = "查找门诊卡号失败，" + ex.Message;
                return false;
            }

        }

        /// <summary>
        /// 通过物理卡号查找门诊卡号
        /// </summary>
        /// <param name="markNo">物理卡号</param>
        /// <param name="cardNo">门诊卡号</param>
        /// <returns>bool true 成功　false 失败</returns>
        public bool GetCardNoByMarkNo(string markNo, ref string cardNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectCardNoByMarkNo1", ref Sql) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return false;
            }
            try
            {
                Sql = string.Format(Sql, markNo);
                if (this.ExecQuery(Sql) == -1)
                {
                    this.Err = "查找数据失败！";
                    return false;
                }
                #region Sql
                /*select b.card_no,
                           b.markno,
                           b.type,
                           b.state as cardstate,
                           a.state as accountstate,
                           a.vacancy 
                    from fin_opb_account a,fin_opb_accountcard b 
                    where a.card_no=b.card_no 
                      and b.markno='{0}' 
                      and type='{1}'*/
                #endregion
                FS.HISFC.Models.Account.Account account = null;
                while (this.Reader.Read())
                {
                    account = new FS.HISFC.Models.Account.Account();
                    account.AccountCard.Patient.PID.CardNO = this.Reader[0].ToString();
                    account.AccountCard.MarkNO = this.Reader[1].ToString();
                    account.AccountCard.MarkType.ID = Reader[2].ToString();
                    if (this.Reader[3].ToString() == "0")
                    {
                        account.AccountCard.IsValid = false;
                    }
                    else
                    {
                        account.AccountCard.IsValid = true;
                    }
                }
                this.Reader.Close();
                if (account == null)
                {
                    this.Err = "该卡" + markNo + "已被取消使用！";
                    return false;
                }
                if (!account.AccountCard.IsValid)
                {
                    this.Err = "该卡" + markNo + "已被停止使用！";
                    return false;
                }
                cardNo = account.AccountCard.Patient.PID.CardNO;

                return true;
            }
            catch (Exception ex)
            {
                this.Err = "查找门诊卡号失败，" + ex.Message;
                return false;
            }

        }

        #endregion

        #region {3613B73F-B8D6-487d-AEFE-D6C3B0C1968B}
        /// <summary>
        ///  获取新的预交金发票号码
        /// </summary>
        /// <returns></returns>
        public string GetNewInvoiceNO(string InvoiceNoType)
        {
            string invoiceno = "";
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetNewInvoiceNO", ref Sql) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return null;
            }
            try
            {
                Sql = string.Format(Sql, this.Operator.ID, InvoiceNoType);

                string result = this.ExecSqlReturnOne(Sql, "");
                if (string.IsNullOrEmpty(result))
                {
                    DateTime curr = this.GetDateTimeFromSysDateTime();
                    invoiceno = "A" + curr.ToString("yyMMdd") + this.Operator.ID + "0001";
                }
                else
                {
                    invoiceno = result.Substring(0, 13) + Convert.ToString((Convert.ToInt32(result.Substring(13, 4)) + 1)).PadLeft(4, '0');
                }
                return invoiceno;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

        }
        #endregion




        /// <summary>
        /// 根据门诊帐户、交易时间、操作状态查找帐户交易操作流水记录
        /// </summary>
        /// <param name="cardNO">门诊帐户</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="opertype">操作类型</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountRecord> GetAccountRecordList(string cardNO, string begin, string end, string opertype)
        {
            return this.GetAccountRecord("Fee.Account.SelectAccountRecordWhere1", cardNO, begin, end, opertype);
        }
        #endregion

        #region  根据门诊帐户、交易时间查找帐户交易操作流水记录
        /// <summary>
        /// 根据门诊帐户、交易时间查找帐户交易操作流水记录
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountRecord> GetAccountRecordList(string cardNO, string begin, string end)
        {
            return this.GetAccountRecord("Fee.Account.SelectAccountRecordWhere3", cardNO, begin, end);
        }

        #endregion


        #region 根据帐号以及操作类型查找帐户交易流水记录

        /// <summary>
        /// 根据帐号以及操作类型查找帐户交易流水记录
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="operType">操作记录</param>
        /// <returns></returns>
        public List<AccountRecord> GetAccountRecordList(string cardNO, string operType)
        {
            return this.GetAccountRecord("Fee.Account.SelectAccountRecordWhere4", cardNO, operType);
        }

        #endregion

        #region 根据交易流水号查找帐户交易流水记录
        /// <summary>
        /// 根据交易流水号查找帐户交易流水记录
        /// {48314E1F-72EC-4044-A41A-833C84687A40}
        /// </summary>
        /// <param name="strSeqNo">交易流水号</param>
        /// <returns></returns>
        public AccountRecord GetAccountRecord(string strSeqNo)
        {
            List<AccountRecord> accountList = this.GetAccountRecord("Fee.Account.SelectAccountRecordWhere5", strSeqNo, "");

            if (accountList == null)
            {
                return null;
            }
            return accountList[0];
        }
        /// <summary>
        /// 获取账户余额（ALL全部）所有有效的// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="accountTypeCode"></param>
        /// <param name="vailFlag">是否查有效ALL是全部</param>
        /// <returns></returns>
        public List<AccountDetail> GetAccountDetail(string accountNo, string accountTypeCode, string vailFlag)
        {
            return this.GetAccountDetailSelect(accountNo, accountTypeCode, vailFlag, "Fee.Account.GetAccountDetail.Where.1");
        }

        #endregion


        #region 根据门诊卡号、发票号查询交易记录
        /// <summary>
        /// 根据门诊卡号、发票号查询交易记录
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="invoiceNO">发票号</param>
        /// <returns>交易实体</returns>
        public FS.HISFC.Models.Account.AccountRecord GetAccountRecord(string cardNO, string invoiceNO)
        {
            List<AccountRecord> accountList = this.GetAccountRecord("Fee.Account.SelectAccountRecordWhere2", cardNO, invoiceNO);
            if (accountList.Count > 0)
            {
                return accountList[0];
            }
            return null;
        }

        #endregion


        #region 更新交易状态
        /// <summary>
        /// 更新交易状态
        /// </summary>
        /// <param name="valid">是否有效0有效1无效</param>
        /// <param name="cardNO">门诊帐号</param>
        /// <param name="operTime">操作时间</param>
        /// <returns></returns>
        public int UpdateAccountRecordState(string valid, string cardNO, string operTime, string remark)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateAccountRecordValid", ref Sql) == -1) return -1;
            try
            {
                Sql = string.Format(Sql, valid, cardNO, operTime, remark);
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

        #region 帐户交易记录
        /// <summary>
        /// 卡费用信息交易记录
        /// </summary>
        /// <returns></returns>
        public int InsertAccountRecord(FS.HISFC.Models.Account.AccountRecord accountRecord)
        {
            string[] args = new string[] {
                                  accountRecord.Patient.PID.CardNO, //门诊卡号
                                  accountRecord.OperType.ID.ToString(),//操作类型
                                  accountRecord.BaseCost.ToString(), //金额
                                  accountRecord.FeeDept.ID,//科室
                                  accountRecord.Oper.ID,//操作人
                                  accountRecord.OperTime.ToString(),//操作时间
                                  accountRecord.ReMark, //备注
                                  FS.FrameWork.Function.NConvert.ToInt32(accountRecord.IsValid).ToString(),//是否有效
                                  accountRecord.AccountNO,//帐号
                                  accountRecord.EmpowerPatient.PID.CardNO, //被授权卡号
                                  accountRecord.EmpowerPatient.Name, //被授权人姓名
                                  accountRecord.Patient.Name,//授权人姓名
                                  accountRecord.EmpowerCost.ToString(),//授权金额
                                  accountRecord.InvoiceType.ID,//发票类型
                                  accountRecord.AccountType.ID //账户类型// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
            };
            return this.UpdateSingTable("Fee.Account.InsertAccountRecord", args);
        }

        /// <summary>
        /// 卡费用信息交易记录
        /// </summary>
        /// <returns></returns>
        public int InsertAccountDetail(FS.HISFC.Models.Account.AccountDetail accountDetail)
        {
            string[] args = new string[] {
                                  accountDetail.ID, //门诊卡号
                                  accountDetail.AccountType.ID,//操作类型
                                  accountDetail.CardNO, //金额
                                  FS.FrameWork.Function.NConvert.ToInt32(accountDetail.IsValid).ToString(),
                                  accountDetail.CreateEnvironment.ID,//操作人
                                  accountDetail.CreateEnvironment.OperTime.ToString(),//操作时间
                                  accountDetail.OperEnvironment.ID,//操作人
                                  accountDetail.OperEnvironment.OperTime.ToString(),//操作时间
            };
            return this.UpdateSingTable("Fee.Account.InsertAccountDetail", args);
        }
        /// <summary>
        /// 卡费用信息交易记录NEW lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <returns></returns>
        public int InsertAccountRecordEX(FS.HISFC.Models.Account.AccountRecord accountRecord)
        {
            string[] args = new string[] {
                                  accountRecord.Patient.PID.CardNO, //门诊卡号
                                  accountRecord.OperType.ID.ToString(),//操作类型
                                  accountRecord.BaseCost.ToString(), //金额
                                  accountRecord.FeeDept.ID,//科室
                                  accountRecord.Oper.ID,//操作人
                                  accountRecord.OperTime.ToString(),//操作时间
                                  accountRecord.ReMark, //备注
                                  FS.FrameWork.Function.NConvert.ToInt32(accountRecord.IsValid).ToString(),//是否有效
                                  accountRecord.AccountNO,//帐号
                                  accountRecord.EmpowerPatient.PID.CardNO, //被授权卡号
                                  accountRecord.EmpowerPatient.Name, //被授权人姓名
                                  accountRecord.Patient.Name,//授权人姓名
                                  accountRecord.EmpowerCost.ToString(),//授权金额
                                  accountRecord.InvoiceType.ID,// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                                  accountRecord.DonateCost.ToString(),//赠送金额
                                  accountRecord.BaseVacancy.ToString(),//交易后基本账户余额
                                  accountRecord.DonateVacancy.ToString(),//交易后赠送账户余额
                                  accountRecord.AccountType.ID,//账户类型
                                  accountRecord.InvoiceNo,//发票号
                                  accountRecord.PayType.Name,//消费类型
                                  accountRecord.PayInvoiceNo//消费发票号
            };//发票类型
            return this.UpdateSingTable("Fee.Account.InsertAccountRecord.1", args);
        }

        #endregion

        #region 根据发票号查找费用明细
        /// <summary>
        /// 根据发票号查找费用明细
        /// </summary>
        /// <param name="invoiceNO">发票类型</param>
        /// <param name="isQuite">是否退费</param>
        /// <returns></returns>
        public DataSet GetFeeDetailByInvoiceNO(string invoiceNO, bool isQuite)
        {
            DataSet dsFeeDetail = new DataSet();
            string quiteFlg = isQuite ? "2" : "1";
            if (this.ExecQuery("Fee.Account.QueryFeeDetailByInvoiceForAccout", ref dsFeeDetail, invoiceNO, quiteFlg) < 0)
            {
                return null;
            }
            return dsFeeDetail;
        }
        #endregion

        #region 帐户数据操作

        #region 生成帐号
        /// <summary>
        /// 生成帐号
        /// </summary>
        /// <returns></returns>
        public string GetAccountNO()
        {
            return this.GetSequence("Fee.Account.GetAccountNO");
        }
        #endregion

        #region 得到帐户余额

        /// <summary>
        /// 查找帐户余额
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="vacancy">余额</param>
        /// <returns>-1查找失败 0不存在 1帐户余额 2授权余额</returns>
        public int GetVacancy(string cardNO, ref decimal vacancy)
        {
            //查找帐户余额
            int resultValue = this.GetAccountVacancy(cardNO, ref vacancy);
            //不存在帐户
            if (resultValue == 0)
            {
                //查找被授权余额
                resultValue = this.GetEmpowerVacancy(cardNO, ref vacancy);
                if (resultValue > 0)
                {
                    return 2;
                }
            }
            return resultValue;

        }
        #endregion

        #region 根据门诊卡号更新帐户余额
        /// <summary>
        /// 根据门诊卡号更新帐户余额
        /// </summary>
        /// <param name="cardNO">帐号</param>
        /// <param name="money">消费金额</param>
        /// <returns></returns>
        public int UpdateAccountVacancy(string accountNO, decimal money)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountVacancy", accountNO, money.ToString());
        }
        /// <summary>
        /// 根据门诊卡号更新帐户余额NEW lfhm// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="cardNO">帐号</param>
        /// <param name="money">消费金额</param>
        /// <returns></returns>
        public int UpdateAccountVacancyEX(FS.HISFC.Models.Account.Account account)
        {

            string[] args = new string[] {
                                  account.ID, //Account_No
                                  account.BaseCost.ToString(),//基本账户金额
                                  account.DonateCost.ToString(), //赠送金额
                                  account.CouponCost.ToString(),//积分
                                  account.Limit.ToString(),//警戒值
                                  account.BaseAccumulate.ToString(),//基本账户累计
                                  account.DonateAccumulate.ToString(),//赠送累计
                                  account.CouponAccumulate.ToString(),//积分累计
                                  account.OperEnvironment.ID,//操作人
                                  account.OperEnvironment.OperTime.ToString()//操作时间

            };
            return this.UpdateSingTable("Fee.Account.UpdateAccountVacancy.1", args);
        }
        /// <summary>
        /// 更新账户的会员卡等级
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="accountLevelCode"></param>
        /// <returns></returns>
        public int UpdateAccountLevel(string accountNo, string accountLevelCode)
        {
            string[] args = new string[] { accountNo, accountLevelCode };
            return this.UpdateSingTable("Fee.Account.UpdateAccountLevel", args);
        }
        /// <summary>
        /// 根据账号和账户类型更新金额// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <returns></returns>
        public int UpdateAccountDetail(FS.HISFC.Models.Account.AccountDetail accountDetail)
        {

            string[] args = new string[] {
                                  accountDetail.ID, //Account_No
                                  accountDetail.AccountType.ID,//账户类型
                                  accountDetail.BaseCost.ToString(),//基本账户金额
                                  accountDetail.DonateCost.ToString(), //赠送金额
                                  accountDetail.CouponCost.ToString(),//积分
                                  accountDetail.BaseAccumulate.ToString(),//基本账户累计
                                  accountDetail.DonateAccumulate.ToString(),//赠送累计
                                  accountDetail.CouponAccumulate.ToString(),//积分累计
                                  accountDetail.OperEnvironment.ID,//操作人
                                  accountDetail.OperEnvironment.OperTime.ToString()//操作时间

            };
            return this.UpdateSingTable("Fee.Account.UpdateAccountDetail", args);
        }
        #endregion

        #region 根据门诊卡号查找密码
        /// <summary>
        /// 根据门诊卡号查找密码
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <returns>用户密码</returns>
        public string GetPassWordByCardNO(string cardNO)
        {
            HISFC.Models.Account.Account account = GetAccountByCardNoEX(cardNO);
            if (account == null)
            {
                AccountEmpower accountEmpower = new AccountEmpower();
                int result = this.QueryAccountEmpowerByEmpwoerCardNO(cardNO, ref accountEmpower);
                if (result <= 0) return "-1";
                return accountEmpower.PassWord;
            }
            else
            {
                return account.PassWord;
            }
        }

        #endregion

        #region 根据门诊卡号更新用户密码
        /// <summary>
        /// 根据门诊卡号更新用户密码
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public int UpdatePassWordByCardNO(string accountNO, string passWord)
        {
            return this.UpdateSingTable("Fee.Account.UpdatePassWord", accountNO, HisDecrypt.Encrypt(passWord));
        }
        #endregion

        #region 更新帐户状态
        /// <summary>
        /// 更新帐户状态
        /// </summary>
        /// <param name="accountNO">帐号</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public int UpdateAccountState(string accountNO, string state)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountState", state, accountNO, this.Operator.ID, this.GetDateTimeFromSysDateTime().ToString());
        }
        #endregion
        #region 根据账号和账户类型更新帐户状态
        /// <summary>
        /// 更新帐户状态// {74621CF6-D26C-44aa-87DC-C68D0867BAC5}
        /// </summary>
        /// <param name="accountNO">帐号</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public int UpdateAccountDetailState(string accountNO, string accountType, string state)
        {
            return this.UpdateSingTable("Fee.Account.UpdateAccountDetailState", state, accountNO, accountType, this.Operator.ID, this.GetDateTimeFromSysDateTime().ToString());
        }
        #endregion

        #region 新建帐户
        /// <summary>
        /// 新建帐户
        /// </summary>
        /// <param name="account">帐户实体</param>
        /// <returns></returns>
        public int InsertAccount(FS.HISFC.Models.Account.Account account)
        {

            return this.UpdateSingTable("Fee.Account.InsertAccount", account.AccountCard.Patient.PID.CardNO, //门诊卡号
                                            FS.FrameWork.Function.NConvert.ToInt32(account.ValidState).ToString(), //帐户状态
                                            account.ID,//帐号
                                            HisDecrypt.Encrypt(account.PassWord),
                                            account.AccountLevel.ID,// {F3B649BB-FA16-49f8-BBDD-F9C6616C41E9}lfhm
                                            account.CreateEnvironment.ID,
                                            account.CreateEnvironment.OperTime.ToString(),
                                            account.OperEnvironment.ID,
                                            account.OperEnvironment.OperTime.ToString());//密码
        }
        #endregion

        #region 积分账户
        /// <summary>
        /// 积分账户// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int InsertCardCoupon(FS.HISFC.Models.Account.CardCoupon cardCoupon)
        {

            return this.UpdateSingTable("Fee.Account.InsertCoupon",
                                            cardCoupon.CardNo, //门诊卡号
                                            cardCoupon.Coupon.ToString("F2"), //积分
                                            cardCoupon.CouponAccumulate.ToString("F2"));//积分累计
        }
        #endregion


        #region 积分账户
        /// <summary>
        /// 积分账户记录// {71A043C7-ADEE-401e-81E2-FF32FBD39D78}
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int InsertCardCouponRecord(FS.HISFC.Models.Account.CardCouponRecord cardCouponRecord)
        {

            return this.UpdateSingTable("Fee.Account.InsertCouponRecord",
                                            cardCouponRecord.CardNo, //门诊卡号
                                            cardCouponRecord.Money.ToString("F2"),
                                            cardCouponRecord.Coupon.ToString("F2"),
                                            cardCouponRecord.CouponVacancy.ToString("F2"),
                                            cardCouponRecord.InvoiceNo,
                                            cardCouponRecord.OperType,
                                            cardCouponRecord.ExchangeGoods,
                                            cardCouponRecord.Memo,
                                            cardCouponRecord.OperEnvironment.ID,
                                            cardCouponRecord.OperEnvironment.OperTime.ToString()
                                            );//密码
        }
        #endregion
        #region 登记卡卷
        /// <登记卡卷>
        /// 新建帐户// {E0683A80-F23C-4848-8482-7257F9263221}
        /// </summary>
        /// <param name="account">帐户实体</param>
        /// <returns></returns>
        public int InsertAccountCardVolume(FS.HISFC.Models.Account.CardVolume cardVolume)
        {

            return this.UpdateSingTable("Fee.Account.InsertAccountCardVolume", cardVolume.VolumeNo,
                cardVolume.Money.ToString(),
                cardVolume.BegTime.ToString(),
                cardVolume.EndTime.ToString(),
                cardVolume.UseType.Name,
                cardVolume.InvoiceNo,
                cardVolume.Patient.PID.CardNO,
                cardVolume.Mark,
                FS.FrameWork.Function.NConvert.ToInt32(cardVolume.ValidState).ToString(),
                cardVolume.CreateEnvironment.ID,
                cardVolume.CreateEnvironment.OperTime.ToString(),
                cardVolume.OperEnvironment.ID,
                cardVolume.OperEnvironment.OperTime.ToString());//密码
        }
        #endregion
        #region 新建成员
        /// <summary>
        /// 新建家庭成员// {B6596A7A-E1DF-43da-ADB3-3CA267353E2C} lfhm
        /// </summary>
        /// <param name="accountFamilyInfo"></param>
        /// <returns></returns>
        public int InsertAccountFamilyInfo(FS.HISFC.Models.Account.AccountFamilyInfo accountFamilyInfo)
        {
            string validState = "1";
            if (accountFamilyInfo.ValidState == EnumValidState.Valid)
            {
                validState = "1";
            }
            else if (accountFamilyInfo.ValidState == EnumValidState.Invalid)
            {
                validState = "0";
            }

            return this.UpdateSingTable("Fee.Account.InsertAccountFamilyInfo",
                accountFamilyInfo.LinkedCardNO,
                accountFamilyInfo.LinkedAccountNo,
                accountFamilyInfo.Relation.ID,
                accountFamilyInfo.Name,
                accountFamilyInfo.Sex.ID,
                accountFamilyInfo.Birthday.ToString(),
                accountFamilyInfo.CardType.ID,
                accountFamilyInfo.IDCardNo,
                accountFamilyInfo.Phone,
                accountFamilyInfo.Address,
                accountFamilyInfo.CardNO,
                accountFamilyInfo.AccountNo,
                validState,
                accountFamilyInfo.CreateEnvironment.ID,
                accountFamilyInfo.CreateEnvironment.OperTime.ToString(),
                accountFamilyInfo.OperEnvironment.ID,
                accountFamilyInfo.OperEnvironment.OperTime.ToString(),
                accountFamilyInfo.FamilyCode,
                accountFamilyInfo.FamilyName);//
        }

        /// <summary>
        /// 修改基本信息的家庭号
        /// </summary>
        /// <param name="CardNo"></param>
        /// <param name="familyCode"></param>
        /// <param name="familyName"></param>
        /// <returns></returns>
        public int UpdatePatientFamilyCode(string CardNo, string familyCode, string familyName)
        {
            return this.UpdateSingTable("Fee.Account.UpdatePatientFamilyCode", CardNo, familyCode, familyName);//密码
        }
        #endregion

        #region 根据门诊卡号取帐户信息
        /// <summary>
        /// 根据门诊卡号取帐户信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.Account GetAccountByCardNoEX(string cardNO)
        {
            return this.GetAccountEX("Fee.Account.where1", cardNO);
        }
        #endregion

        #region 根据帐号获取取帐户信息
        /// <summary>
        /// 根据帐号取帐户信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.Account GetAccountByAccountNO(string accountNO)
        {
            return this.GetAccountEX("Fee.Account.where2", accountNO);
        }
        #endregion

        #region 根据物理卡号查找帐户数据
        /// <summary>
        /// 根据物理卡号查找帐户数据
        /// </summary>
        /// <param name="markNo">物理卡号</param>
        /// <returns></returns>
        public FS.HISFC.Models.Account.Account GetAccountByMarkNo(string markNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.AccountByMarkNo", ref Sql) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return null;
            }
            try
            {
                Sql = string.Format(Sql, markNo);
                if (this.ExecQuery(Sql) < 0)
                {
                    this.Err = "查找数据失败！";
                    return null;
                }
                FS.HISFC.Models.Account.Account account = null;
                //一个卡号只能对应一个帐户
                while (this.Reader.Read())
                {
                    account = new FS.HISFC.Models.Account.Account();
                    account.CardNO = this.Reader[0].ToString();
                    account.ValidState = (HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[1]));
                    account.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    account.DonateVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                    account.CouponVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    account.BaseAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    account.DonateAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    account.CouponAccumulate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                    account.PassWord = HisDecrypt.Decrypt(this.Reader[8].ToString());
                    account.DayLimit = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                    account.ID = this.Reader[10].ToString();
                }
                this.Reader.Close();
                return account;
            }
            catch (Exception ex)
            {
                this.Err = "查找数据失败！" + ex.Message;
                return null;
            }

        }

        #endregion

        #region 查找帐户密码
        /// <summary>
        /// 根据证件类型
        /// </summary>
        /// <param name="idCardNO">证件号</param>
        /// <param name="idCardType">证件类型</param>
        /// <returns>-1失败</returns>
        public ArrayList GetAccountByIdNO(string idCardNO, string idCardType)
        {
            string sqlstr = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.SelectAccountByIdNO", ref sqlstr) == -1)
            {
                this.Err = "查找索引为Fee.Account.SelectAccountByIdNO的Sql语句失败！";
                return null;
            }
            ArrayList al = new ArrayList();
            HISFC.Models.Account.Account account = null;
            try
            {
                sqlstr = string.Format(sqlstr, idCardNO, idCardType);
                if (this.ExecQuery(sqlstr) < 0) return null;

                while (this.Reader.Read())
                {
                    account = new FS.HISFC.Models.Account.Account();
                    account.CardNO = this.Reader[0].ToString();
                    if (this.Reader[1] != DBNull.Value) account.ValidState = (HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[1]));
                    if (this.Reader[2] != DBNull.Value) account.BaseVacancy = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    if (this.Reader[3] != DBNull.Value) account.PassWord = HisDecrypt.Decrypt(this.Reader[3].ToString());
                    if (this.Reader[4] != DBNull.Value) account.DayLimit = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    account.ID = this.Reader[5].ToString();
                    account.IsEmpower = NConvert.ToBoolean(this.Reader[6]);
                    al.Add(account);
                }
            }
            catch (Exception ex)
            {
                this.Err = "查询数据出错！" + ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                    this.Reader.Close();
            }
            return al;
        }
        #endregion

        #region 其他查询

        /// <summary>
        /// 获取当前收费员从当前时间开始计算前N张发票信息
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int GetAccountInvoiceByCount(string operCode, int count, ref DataSet dsResult)
        {
            dsResult = new DataSet();
            string sqlStr = "";

            if (this.Sql.GetCommonSql("FS.OutPatient.Account.Fee.GetInvoicesCountsInfosSinceNow.Select.1", ref sqlStr) == -1)
            {
                this.Err = "没有找到FS.OutPatient.Account.Fee.GetInvoicesCountsInfosSinceNow.Select.1索引sql语句!";
                return -1;
            }

            try
            {
                sqlStr = string.Format(sqlStr, operCode, count);
                if (this.ExecQuery(sqlStr, ref dsResult) == -1)
                {
                    this.Err += "执行FS.OutPatient.Account.Fee.GetInvoicesCountsInfosSinceNow.Select.1出错!";
                    return -1;
                }

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return 1;

        }

        #endregion

        #endregion

        #region 根据卡号规则读取卡号

        /// <summary>
        ///  根据卡号规则查找卡类型
        /// </summary>
        /// <param name="markNo">物理卡号</param>
        /// <param name="accountCard">卡实体</param>
        /// <returns>1:成功 0卡未发放 -1失败</returns>
        public int GetCardByRule(string markNo, ref FS.HISFC.Models.Account.AccountCard accountCard)
        {
            //markNo = FS.FrameWork.Public.String.TakeOffSpecialChar(markNo);
            if (string.IsNullOrEmpty(markNo))
            {
                this.Err = "请输入有效的就诊卡号！";
                return -1;
            }
            if (!InitReadMark())
            {
                this.Err = "初始化动态库失败！";
                return -1;
            }
            int resultValue = IreadMarkNO.ReadMarkNOByRule(markNo, ref accountCard);
            this.Err = IreadMarkNO.Error;
            return resultValue;
        }
        #endregion

        #region 授权
        /// <summary>
        /// 插入授权表
        /// </summary>
        /// <param name="accontEmpower">授权实体</param>
        /// <returns>1成功 -1失败</returns>
        public int InsertEmpower(AccountEmpower accontEmpower)
        {
            return this.UpdateSingTable("Fee.Account.InsertEmpower", GetEmpowerArgs(accontEmpower));
        }

        /// <summary>
        /// 更新授权表
        /// </summary>
        /// <param name="accountEmpower">授权实体</param>
        /// <returns>1成功 -1失败 0没有更新到记录</returns>
        public int UpdateEmpower(AccountEmpower accountEmpower)
        {
            return this.UpdateSingTable("Fee.Account.UpdateEmpower", GetEmpowerArgs(accountEmpower));
        }

        /// <summary>
        /// 更新帐户授权标识
        /// </summary>
        /// <param name="accountNO">帐号</param>
        /// <returns>1成功 -1失败、0帐户数据发生变化</returns>
        public int UpdateAccountEmpowerFlag(string accountNO)
        {
            return UpdateSingTable("Fee.Account.UpdateAccountEmpowerFlag", accountNO);
        }

        /// <summary>
        /// 根据授权门诊卡号查找被授权信息
        /// </summary>
        /// <param name="accountNO">授权帐号</param>
        /// <returns></returns>
        public List<AccountEmpower> QueryEmpowerByAccountNO(string accountNO)
        {
            return this.GetEmpowerList("Fee.Account.SelectEmpowerwhere2", accountNO);
        }

        /// <summary>
        /// 根据授权门诊卡号查找被授权信息
        /// </summary>
        /// <param name="accountNO"></param>
        /// <returns></returns>
        public List<AccountEmpower> QueryAllEmpowerByAccountNO(string accountNO)
        {
            return this.GetEmpowerList("Fee.Account.SelectEmpowerwhere3", accountNO);
        }

        /// <summary>
        /// 根据被授权门诊卡号查找授权信息
        /// </summary>
        /// <param name="empowerCardNO">被授权门诊卡号</param>
        /// <returns>-1失败 0不存在有效的授权信息 1成功</returns>
        public int QueryAccountEmpowerByEmpwoerCardNO(string empowerCardNO, ref AccountEmpower accountEmpower)
        {
            List<AccountEmpower> list = this.GetEmpowerList("Fee.Account.SelectEmpowerwhere1", empowerCardNO);
            if (list == null) return -1;
            if (list.Count == 0)
            {
                this.Err = "该卡不存在有效的授权信息！";
                return 0;
            }
            accountEmpower = list[0];
            return 1;
        }

        /// <summary>
        /// 根据授权帐号和被授权门诊卡号查找授权信息
        /// </summary>
        /// <param name="accountNO">授权帐号</param>
        /// <param name="empowerCardNO">门诊卡号</param>
        /// <param name="accountEmpower">授权信息</param>
        /// <returns>-1失败 0不存在授权信息 1成功</returns>
        public int QueryEmpower(string accountNO, string empowerCardNO, ref AccountEmpower accountEmpower)
        {
            List<AccountEmpower> list = this.GetEmpowerList("Fee.Account.SelectEmpowerwhere4", accountNO, empowerCardNO);
            if (list == null) return -1;
            if (list.Count == 0)
            {
                this.Err = "该卡不存在有效的授权信息！";
                return 0;
            }
            accountEmpower = list[0];
            return 1;
        }

        /// <summary>
        /// 更新授权信息余额
        /// </summary>
        /// <param name="accountNO">帐号</param>
        /// <param name="empowerCardNO">被授权门诊卡号</param>
        /// <param name="money">金额</param>
        /// <returns>1成功 -1失败</returns>
        public int UpdateEmpowerVacancy(string accountNO, string empowerCardNO, decimal money)
        {
            return this.UpdateSingTable("Fee.Account.UpdateEmpowerVacancy", accountNO, empowerCardNO, money.ToString());
        }

        /// <summary>
        /// 批量更新授权状态
        /// </summary>
        /// <param name="accountNO">帐号</param>
        /// <param name="validState">更新的状态</param>
        /// <param name="currentState">当前状态</param>
        /// <returns>1成功 -1失败</returns>
        public int UpdateEmpowerState(string accountNO, HISFC.Models.Base.EnumValidState validState, HISFC.Models.Base.EnumValidState currentState)
        {
            return this.UpdateSingTable("Fee.Account.UpdateEmpowerState", accountNO, ((int)validState).ToString(), ((int)currentState).ToString());
        }

        #endregion

        #region 证件信息
        /// <summary>
        /// 插入患者证件信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int InsertIdenInfo(FS.HISFC.Models.RADT.Patient p)
        {
            return this.UpdateSingTable("Fee.Account.InsertIdenInfo", p.PID.CardNO, p.IDCardType.ID, p.IDCardType.Name, p.IDCard);
        }

        /// <summary>
        /// 更新患者证件信息
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int UpdateIdenInfo(FS.HISFC.Models.RADT.Patient p)
        {
            return this.UpdateSingTable("Fee.Account.UpdateIdenInfo", p.PID.CardNO, p.IDCardType.ID, p.IDCardType.Name, p.IDCard);
        }

        /// <summary>
        /// 证件信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList QueryIdenInfo(string cardNO)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.QueryIdenInfo", ref sql) == -1)
            {
                this.Err = "查询索引为Fee.Account.QueryIdenInfo的SQL语句失败！";
                return null;
            }
            return this.GetPatientIdenInfo(sql, cardNO);

        }
        /// <summary>
        /// 根据CardNO和证件类型清空照片
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int DeletePhoto(FS.HISFC.Models.RADT.Patient p)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateIdenInfo.DeletePhoto", ref strSql) == -1)
            {
                this.Err = "查找索引为Fee.Account.UpdateIdenInfo.DeletePhoto的Sql语句失败！";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, p.PID.CardNO, p.IDCardType.ID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
            }
            return this.ExecNoQuery(strSql);
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
            if (this.Sql.GetCommonSql("Fee.Account.UpdateIdenInfo.Photo", ref strSql) == -1)
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
            if (this.Sql.GetCommonSql("Fee.Account.GetIdenInfo.Photo", ref sql) == -1)
            {
                this.Err = "查询索引为Fee.Account.GetIdenInfo.Photo的SQL语句失败！";
                return -1;
            }

            photo = this.OutputBlob(string.Format(sql, cardNO, cardType));

            return 1;
        }

        #endregion

        #region 多个合同单位

        /// <summary>
        /// 注册患者信息
        /// </summary>
        /// <param name="PatientInfo">登记的患者信息</param>
        /// <returns>0成功 -1失败</returns>
        public int InsertPatientPactInfo(Patient PatientInfo)
        {
            //先删除
            if (PatientInfo.MutiPactInfo == null || PatientInfo.MutiPactInfo.Count == 0)
            {
                return 1;
            }

            string strSql = string.Empty;
            if (this.ExecNoQueryByIndex("RADT.OutPatient.DeleteMutiPactInfo", PatientInfo.PID.CardNO) < 0)
            {
                return -1;
            }
            //删除所有

            strSql = string.Empty;
            if (Sql.GetSql("RADT.OutPatient.InsertMutiPactInfo", ref strSql) == -1) return -1;
            foreach (FS.HISFC.Models.Base.PactInfo pact in PatientInfo.MutiPactInfo)
            {
                try
                {
                    string[] s = new string[4];
                    try
                    {
                        s[0] = PatientInfo.PID.CardNO; //就诊卡号
                        s[1] = pact.ID; //合同单位
                        s[2] = pact.Name; //合同单位
                        s[3] = pact.ValidState; //状态
                    }
                    catch (Exception ex)
                    {
                        Err = ex.Message;
                        WriteErr();
                    }
                    if (ExecNoQuery(strSql, s) <= 0)
                    {
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    Err = "赋值时候出错！" + ex.Message;
                    WriteErr();
                    return -1;
                }
            }

            return 1;
        }

        public int GetPatientPactInfo(Patient PatientInfo)
        {
            //先删除
            if (PatientInfo == null)
            {
                return -1;
            }

            string strSql = string.Empty;
            if (Sql.GetSql("RADT.OutPatient.QueryMutiPactInfo", ref strSql) == -1) return -1;
            if (this.ExecQuery(strSql, PatientInfo.PID.CardNO) < 0)
            {
                return -1;
            }
            if (this.Reader != null)
            {
                try
                {
                    PatientInfo.MutiPactInfo = new System.Collections.Generic.List<PactInfo>();
                    while (this.Reader.Read())
                    {
                        FS.HISFC.Models.Base.PactInfo pact = new PactInfo();
                        pact.ID = this.Reader[0].ToString();
                        pact.Name = this.Reader[1].ToString();
                        pact.ValidState = this.Reader[2].ToString();
                        pact.Memo = pact.ValidState;//用于列表选择
                        if (Reader.FieldCount > 3)
                        {
                            pact.PayKind.ID = this.Reader[3].ToString();
                        }

                        PatientInfo.MutiPactInfo.Add(pact);
                    }
                }
                catch (Exception e)
                {
                    Err = "赋值时候出错！" + e.Message;
                    WriteErr();
                    return -1;
                }
                finally
                {
                    this.Reader.Close();
                }
            }

            return 1;
        }

        #endregion

        #region 通过病历号查询卡号

        /// <summary>
        /// 查找所有的物理卡列表
        /// </summary>
        /// <param name="cardNO">病历号</param>
        /// <param name="cardType">卡类型 ALL为全部</param>
        /// <param name="state">有效状态 ALL为全部</param>
        /// <returns></returns>
        public ArrayList GetMarkByCardNo(string cardNO, string cardType, string state)
        {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.GetMarkNoByCardNo", ref Sql) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return null;
            }
            try
            {
                Sql = string.Format(Sql, cardNO, cardType, state);
                if (this.ExecQuery(Sql) == -1)
                {
                    this.Err = "查找卡使用数据失败！";
                    return null;
                }
                ArrayList cardList = new ArrayList();
                FS.FrameWork.Models.NeuObject markCardObj = null;
                while (this.Reader.Read())
                {
                    markCardObj = new NeuObject();
                    //物理卡号
                    markCardObj.ID = Reader[0].ToString();
                    //物理卡类别
                    markCardObj.Name = Reader[1].ToString();
                    //卡状态：有效、无效
                    markCardObj.Memo = Reader[2].ToString();
                    //病历号
                    markCardObj.User01 = Reader[3].ToString();

                    cardList.Add(markCardObj);
                }
                return cardList;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 根据物理卡号获取收缴费信息
        /// </summary>
        /// <param name="markNO"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Account.AccountCardFee> QueryCardFeebyMCardNo(string markNO)
        {
            return null;
        }

        #endregion

        #region 停车票
        /// <summary>
        /// 插入停车票费用信息// {17D86AD6-A28C-4518-951C-EE0F3504598B}
        /// </summary>
        /// <param name="ParkingTicketFeeInfo"></param>
        /// <returns></returns>
        public int InsertParkingTicketInfo(ParkingTicketFeeInfo ParkingTicketFeeInfo)
        {
            // {23F37636-DC34-44a3-A13B-071376265450}
            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (employee == null)
            {
                employee = new FS.HISFC.Models.Base.Employee();
            }
            FS.HISFC.Models.Base.Department dept = employee.Dept as FS.HISFC.Models.Base.Department;
            if (dept == null)
            {
                dept = new FS.HISFC.Models.Base.Department();
            }
            if (string.IsNullOrEmpty(ParkingTicketFeeInfo.Hospital_id)) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                string hospitalid = dept.HospitalID;
                string hospitalname = dept.HospitalName;
                ParkingTicketFeeInfo.Hospital_id = hospitalid;
                ParkingTicketFeeInfo.Hospital_name = hospitalname;
            }
            string strSql = string.Empty;
            if (this.UpdateSingTable("Fee.Account.InsertParkingTicketInfo", ParkingTicketFeeInfo.InvoiceNo, ((int)ParkingTicketFeeInfo.TransType).ToString(),
                ParkingTicketFeeInfo.ItemCode, ParkingTicketFeeInfo.ItemName, ParkingTicketFeeInfo.Unit, ParkingTicketFeeInfo.UnitPrice.ToString(), ParkingTicketFeeInfo.Qty.ToString(),
                ParkingTicketFeeInfo.TotCost.ToString(), ParkingTicketFeeInfo.PayMode.ID, ParkingTicketFeeInfo.TicketNo, ParkingTicketFeeInfo.OldInvoiceNo, ParkingTicketFeeInfo.CancelDate.ToString(),
                ParkingTicketFeeInfo.OldTicketNo, ParkingTicketFeeInfo.Memo, ParkingTicketFeeInfo.Flag1, ParkingTicketFeeInfo.Flag2, ParkingTicketFeeInfo.Flag3, ((int)ParkingTicketFeeInfo.ValidState).ToString(),
                ParkingTicketFeeInfo.OperEnvironment.ID, ParkingTicketFeeInfo.OperEnvironment.OperTime.ToString(), FS.FrameWork.Function.NConvert.ToInt32(ParkingTicketFeeInfo.IsBalance).ToString(),
                ParkingTicketFeeInfo.BalanceNo, ParkingTicketFeeInfo.BalanceEnvironment.ID, ParkingTicketFeeInfo.BalanceEnvironment.OperTime.ToString(), FS.FrameWork.Function.NConvert.ToInt32(ParkingTicketFeeInfo.IsCheck).ToString(),
                ParkingTicketFeeInfo.CheckEnvironment.ID, ParkingTicketFeeInfo.CheckEnvironment.OperTime.ToString(), ParkingTicketFeeInfo.Hospital_id, ParkingTicketFeeInfo.Hospital_name) < 0) //{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
            {
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// 查询停车票费用信息// {17D86AD6-A28C-4518-951C-EE0F3504598B}
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="ticketNo"></param>
        /// <param name="memo"></param>
        /// <param name="ticketState"></param>
        /// <param name="operCode"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ArrayList QueryParkingTicketInfo(string itemCode, string ticketNo, string memo, string ticketState, string operCode, string invoiceNo, string beginTime, string endTime)
        {
            string sqlWhere = "";
            sqlWhere = " where oper_date >= to_date('" + beginTime + "','yyyy-mm-dd hh24:mi:ss') ";
            sqlWhere += " and oper_date <= to_date('" + endTime + "','yyyy-mm-dd hh24:mi:ss')";
            if (!string.IsNullOrEmpty(itemCode))
            {
                sqlWhere += " and item_code = '" + itemCode + "'";
            }
            if (!string.IsNullOrEmpty(ticketNo))
            {
                sqlWhere += " and ticket_no like '%" + ticketNo + "%'";
            }
            if (!string.IsNullOrEmpty(memo))
            {
                sqlWhere += " and remark = '%" + memo + "%'";
            }
            if (!string.IsNullOrEmpty(ticketState))
            {
                sqlWhere += " and (TRANS_TYPE = '" + ticketState + "' or 'ALL' = '" + ticketState + "')";
            }
            if (!string.IsNullOrEmpty(operCode))
            {
                sqlWhere += " and (oper_code = '" + operCode + "' or 'ALL' = '" + operCode + "')";
            }
            if (!string.IsNullOrEmpty(invoiceNo))
            {
                invoiceNo = invoiceNo.PadLeft(12, '0');
                sqlWhere += " and invoice_no = '" + invoiceNo + "'";
            }
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.QueryParkingTicketInfo", ref strSql) == -1)
            {
                this.Err = "查找索引为Fee.Account.QueryParkingTicketInfo的Sql语句失败！";
                return null;
            }
            strSql = strSql + " " + sqlWhere;

            ArrayList al = new ArrayList();
            try
            {
                if (this.ExecQuery(strSql) == -1) return null;
                while (this.Reader.Read())
                {
                    ParkingTicketFeeInfo parkingTicketFeeInfo = new ParkingTicketFeeInfo();
                    parkingTicketFeeInfo.InvoiceNo = Reader[0].ToString();
                    parkingTicketFeeInfo.TransType = Reader[1].ToString() == "1" ? TransTypes.Positive : TransTypes.Negative;
                    parkingTicketFeeInfo.ItemCode = Reader[2].ToString();
                    parkingTicketFeeInfo.ItemName = Reader[3].ToString();
                    parkingTicketFeeInfo.Unit = Reader[4].ToString();
                    parkingTicketFeeInfo.UnitPrice = NConvert.ToDecimal(Reader[5].ToString());
                    parkingTicketFeeInfo.Qty = NConvert.ToDecimal(Reader[6].ToString());
                    parkingTicketFeeInfo.TotCost = NConvert.ToDecimal(Reader[7].ToString());
                    parkingTicketFeeInfo.PayMode.ID = Reader[8].ToString();
                    parkingTicketFeeInfo.PayMode.Name = Reader[9].ToString();
                    parkingTicketFeeInfo.TicketNo = Reader[10].ToString();
                    parkingTicketFeeInfo.OldInvoiceNo = Reader[11].ToString();
                    parkingTicketFeeInfo.CancelDate = NConvert.ToDateTime(Reader[12].ToString());
                    parkingTicketFeeInfo.OldTicketNo = Reader[13].ToString();
                    parkingTicketFeeInfo.Memo = Reader[14].ToString();
                    parkingTicketFeeInfo.Flag1 = Reader[15].ToString();
                    parkingTicketFeeInfo.Flag2 = Reader[16].ToString();
                    parkingTicketFeeInfo.Flag3 = Reader[17].ToString();
                    parkingTicketFeeInfo.ValidState = Reader[18].ToString() == "1" ? EnumValidState.Valid : EnumValidState.Invalid;
                    parkingTicketFeeInfo.OperEnvironment.ID = Reader[19].ToString();
                    parkingTicketFeeInfo.OperEnvironment.Name = Reader[20].ToString();
                    parkingTicketFeeInfo.OperEnvironment.OperTime = NConvert.ToDateTime(Reader[21].ToString());
                    parkingTicketFeeInfo.IsBalance = NConvert.ToBoolean(Reader[22].ToString());
                    parkingTicketFeeInfo.BalanceNo = Reader[23].ToString();
                    parkingTicketFeeInfo.BalanceEnvironment.ID = Reader[24].ToString();
                    parkingTicketFeeInfo.BalanceEnvironment.Name = Reader[25].ToString();
                    parkingTicketFeeInfo.BalanceEnvironment.OperTime = NConvert.ToDateTime(Reader[26].ToString());
                    parkingTicketFeeInfo.IsCheck = NConvert.ToBoolean(Reader[27].ToString());
                    parkingTicketFeeInfo.CheckEnvironment.ID = Reader[28].ToString();
                    parkingTicketFeeInfo.CheckEnvironment.Name = Reader[29].ToString();
                    parkingTicketFeeInfo.CheckEnvironment.OperTime = NConvert.ToDateTime(Reader[30].ToString());

                    al.Add(parkingTicketFeeInfo);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }

            return al;
        }


        public int InsertExpItemMedical(FS.HISFC.Models.RADT.PatientInfo patient, List<ItemMedicalDetail> details)
        {

            string Sql = string.Empty;

            string headSql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.InsertExpItems", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.InsertExpItems 的Sql语句失败！";
                return -1;
            }

            if (this.Sql.GetSql("Fee.Account.ItemMedical.InsertHeadExpItems", ref headSql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.InsertHeadExpItems 的Sql语句失败！";
                return -1;
            }

            string sequece = this.GetSequence("Fee.Account.GetItemMedicalHeadNO");

            headSql = string.Format(headSql, sequece, patient.PID.CardNO, patient.Name, patient.ExtendFlag1, patient.ExtendFlag2, patient.Memo, (this.Operator as FS.HISFC.Models.Base.Employee).ID);

            if (this.ExecNoQuery(headSql) <= 0) { return -1; }


            try
            {
                foreach (ItemMedicalDetail item in details)
                {

                    string sqlstr = string.Format(Sql, patient.PID.CardNO,
                                                      patient.Name,//姓名
                                                      item.ItemCode,//项目编码
                                                      item.ItemName,//项目名称
                                                      item.ItemSubcode,//子项目编码
                                                      item.ItemSubname,
                                                      item.UnitPrice,
                                                      item.ItemNum,
                                                      item.ItemNum,
                                                      0,
                                                      item.UnitPrice * item.ItemNum,
                                                      patient.Memo,
                                                      item.OperEnvironment.ID,
                                                      item.OperEnvironment.OperTime.ToString(),
                                                      item.CreateEnvironment.ID,
                                                      item.CreateEnvironment.OperTime.ToString(),
                                                      item.ItemMediacl.PackageId,
                                                      item.ItemMediacl.PackageName,
                                                      sequece
                                                   );

                    if (this.ExecNoQuery(sqlstr) <= 0) { return -1; }
                }
            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }


            return 1;
        }



        public List<ExpItemMedical> QueryExpItemMedical(string sql)
        {
            if (this.ExecQuery(sql) < 0) return new List<ExpItemMedical>();

            List<ExpItemMedical> list = new List<ExpItemMedical>();
            ExpItemMedical item = null;

            try
            {
                while (this.Reader.Read())
                {
                    item = new ExpItemMedical();
                    item.ClinicCode = this.Reader[0].ToString();
                    item.CardNo = this.Reader[1].ToString();
                    item.PatientName = this.Reader[2].ToString();
                    item.ItemCode = this.Reader[3].ToString();
                    item.ItemName = this.Reader[4].ToString();
                    item.ItemSubcode = this.Reader[5].ToString();
                    item.ItemSubname = this.Reader[6].ToString();
                    item.UnitPrice = NConvert.ToDecimal(this.Reader[7].ToString());
                    item.Qty = int.Parse(this.Reader[8].ToString());
                    item.RtnQty = int.Parse(this.Reader[9].ToString());
                    item.ConfirmQty = int.Parse(this.Reader[10].ToString());
                    item.TotPrice = NConvert.ToDecimal(this.Reader[11]);
                    item.Memo = this.Reader[12].ToString();
                    item.OperEnvironment.ID = this.Reader[13].ToString();
                    item.OperEnvironment.OperTime = NConvert.ToDateTime(this.Reader[14]);
                    item.CreateEnvironment.ID = this.Reader[15].ToString();
                    item.CreateEnvironment.OperTime = NConvert.ToDateTime(this.Reader[16]);
                    item.PackageId = this.Reader[17].ToString();
                    item.PackageName = this.Reader[18].ToString();
                    item.CancelFlag = this.Reader[19].ToString();
                    item.CancelEnvironment.ID = this.Reader[20].ToString();
                    item.CancelEnvironment.OperTime = NConvert.ToDateTime(this.Reader[21]);
                    item.ItemMedicalHeadNo = this.Reader[22].ToString();

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return new List<ExpItemMedical>();
            }

            return list;


        }

        /// <summary>
        /// 查询未作废项目次数>0
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public List<ExpItemMedical> QueryExpItemMedicalByCardNo(string cardNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.QueryExpItemMedicalByCardNo", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.QueryExpItemMedicalByCardNo 的Sql语句失败！";
                return null;
            }
            Sql = string.Format(Sql, cardNo);
            return QueryExpItemMedical(Sql);
        }

        /// <summary>
        /// 查询次数包含0的项目
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public List<ExpItemMedical> QueryExpItemMedicalConsZeroByCardNo(string cardNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.QueryExpItemMedicalConsZeroByCardNo", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.QueryExpItemMedicalConsZeroByCardNo 的Sql语句失败！";
                return null;
            }
            Sql = string.Format(Sql, cardNo);
            return QueryExpItemMedical(Sql);

        }

        /// <summary>
        /// 查询所有项目
        /// </summary>
        /// <returns></returns>
        public List<ExpItemMedical> QueryExpItemMedicalALLByCardNo(string cardNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.SelectALL", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ExpItemMedical.SelectALL 的Sql语句失败！";
                return null;
            }
            string Where = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.Where1", ref Where) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ExpItemMedical.Where1 的Sql语句失败！";
                return null;
            }

            Sql = Sql + Where;

            Sql = string.Format(Sql, cardNo);
            return QueryExpItemMedical(Sql);

        }


        /// <summary>
        /// 根据就诊卡号创建时间范围查询所有项目
        /// </summary>
        /// <returns></returns>
        public List<ExpItemMedical> QueryExpItemMedicalALLByCardNoAndTime(string cardNo, string timeStar, string timeEnd)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.SelectALL", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ExpItemMedical.SelectALL 的Sql语句失败！";
                return null;
            }
            string Where = string.Empty;

            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.Where2", ref Where) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ExpItemMedical.Where2 的Sql语句失败！";
                return null;
            }

            Sql = Sql + Where;

            Sql = string.Format(Sql, cardNo, timeStar, timeEnd);
            return QueryExpItemMedical(Sql);

        }




        /// <summary>
        /// 作废套餐包明细
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="packageid"></param>
        /// <returns></returns>
        public bool UpdateCancelFlag(string cardNo, string packageid,string headno)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.UpdateCancelFlag", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ExpItemMedical.UpdateCancelFlag 的Sql语句失败！";
                return false;
            }

            string sqlhead = string.Empty;

            if (!string.IsNullOrEmpty(headno))
            {

                if (this.Sql.GetSql("Fee.Account.ExpItemMedical.UpdateHeadCancelFlag", ref sqlhead) == -1)
                {
                    this.Err = this.Err = "查找索引为 Fee.Account.ExpItemMedical.UpdateHeadCancelFlag 的Sql语句失败！";
                    return false;
                }

            }

            string ss = @" update exp_itemmedicalhead set UNIT_PRICE = UNIT_PRICE - '3001'  where CLINIC_CODE='1000000007' and UNIT_PRICE - '3001' >=0 ";

           
            Sql = string.Format(Sql, packageid, cardNo, FS.FrameWork.Management.Connection.Operator.ID);

            sqlhead = string.Format(sqlhead, cardNo, headno, FS.FrameWork.Management.Connection.Operator.ID);

            if (string.IsNullOrEmpty(sqlhead))
            {
                this.ExecNoQuery(ss);
                return this.ExecNoQuery(Sql) > 0;
            }
            else
            {


                if (this.ExecNoQuery(Sql) > 0) { return this.ExecNoQuery(sqlhead) > 0; }
                else
                {
                    return false;
                }
            }
           

            
        }


        public bool UpdateExpItemMedical(string cardNo, List<ExpItemMedical> details)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ExpItemMedical.UpdateExpItemMedical", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ExpItemMedical.UpdateExpItemMedical 的Sql语句失败！";
                return false;
            }

            try
            {
                foreach (ExpItemMedical item in details)
                {

                    string sqlstr = string.Format(Sql, cardNo,
                                                      item.ClinicCode,//姓名
                                                      item.RtnQty,//项目编码
                                                      item.ConfirmQty,//项目名称
                                                      item.Memo,
                                                      item.CreateEnvironment.ID
                                                   );

                    if (this.ExecNoQuery(sqlstr) <= 0) { return false; }
                }
            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return false;
            }

            return true;

        }


        /// <summary>
        /// 查询所有控费包(有效)
        /// </summary>
        /// <param name="accountNO"></param>
        /// <returns></returns>
        public List<ItemMedical> QueryAllItemMedical(string isvalid)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.SelectALL", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.SelectALL 的Sql语句失败！";
                return null;
            }

            string where = "";
            if (!string.IsNullOrEmpty(isvalid) && isvalid != "ALL")
            {
                where = "where VALID_FLAG =" + isvalid;
            }

            Sql = Sql + where;

            return QueryItemMedical(Sql);

        }


        public List<ItemMedical> QueryItemMedical(string sql)
        {

            if (this.ExecQuery(sql) < 0) return null;
            List<ItemMedical> list = new List<ItemMedical>();
            ItemMedical item = null;
            try
            {
                while (this.Reader.Read())
                {
                    item = new ItemMedical();
                    item.PackageId = this.Reader[0].ToString();
                    item.PackageName = this.Reader[1].ToString();
                    item.PackageCost = NConvert.ToDecimal(this.Reader[2]);
                    item.SpellCode = this.Reader[3].ToString();
                    item.InputCode = this.Reader[4].ToString();
                    item.SortId = this.Reader[5].ToString();
                    item.ValidState = this.Reader[6].ToString();
                    item.Memo = this.Reader[7].ToString();
                    item.OperEnvironment.ID = this.Reader[8].ToString();
                    item.OperEnvironment.OperTime = NConvert.ToDateTime(this.Reader[9]);
                    item.CreateEnvironment.ID = this.Reader[10].ToString();
                    item.CreateEnvironment.OperTime = NConvert.ToDateTime(this.Reader[11]);

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return list;

        }


        public ItemMedical QueryAllItemMedicalById(string packageid)
        {

            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.SelectALL", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.SelectALL 的Sql语句失败！";
                return null;
            }

            string sqlwhere = string.Empty;

            if (this.Sql.GetSql("Fee.Account.ItemMedical.Where1", ref sqlwhere) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.Where1 的Sql语句失败！";
                return null;
            }

            Sql += sqlwhere;

            if (this.ExecQuery(Sql, packageid) < 0) return null;
            List<ItemMedical> list = new List<ItemMedical>();
            ItemMedical item = null;
            try
            {
                while (this.Reader.Read())
                {
                    item = new ItemMedical();
                    item.PackageId = this.Reader[0].ToString();
                    item.PackageName = this.Reader[1].ToString();
                    item.PackageCost = NConvert.ToDecimal(this.Reader[2]);
                    item.SpellCode = this.Reader[3].ToString();
                    item.InputCode = this.Reader[4].ToString();
                    item.SortId = this.Reader[5].ToString();
                    item.ValidState = this.Reader[6].ToString();
                    item.Memo = this.Reader[7].ToString();
                    item.OperEnvironment.ID = this.Reader[8].ToString();
                    item.OperEnvironment.OperTime = NConvert.ToDateTime(this.Reader[9]);
                    item.CreateEnvironment.ID = this.Reader[10].ToString();
                    item.CreateEnvironment.OperTime = NConvert.ToDateTime(this.Reader[11]);

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return list[0] as ItemMedical;
        }

        /// <summary>
        /// 新增套包
        /// </summary>
        /// <param name="mediacl"></param>
        /// <returns></returns>
        public int AddMedicalPackage(ItemMedical mediacl)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.Insert", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.Insert 的Sql语句失败！";
                return 0;
            }


            Sql = string.Format(Sql, mediacl.PackageName, mediacl.PackageCost, mediacl.SpellCode, mediacl.InputCode, mediacl.ValidState, mediacl.Memo, mediacl.OperEnvironment.ID, mediacl.OperEnvironment.OperTime, mediacl.CreateEnvironment.ID, mediacl.CreateEnvironment.OperTime);

            return this.ExecNoQuery(Sql);

        }

        /// <summary>
        /// 修改套包
        /// </summary>
        /// <param name="mediacl"></param>
        /// <returns></returns>
        public int UpdateMedicalPackage(ItemMedical mediacl)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.Update", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.Update 的Sql语句失败！";
                return 0;
            }

            Sql = string.Format(Sql, mediacl.PackageId, mediacl.PackageName, mediacl.PackageCost, mediacl.SpellCode, mediacl.ValidState, mediacl.Memo, mediacl.OperEnvironment.ID, mediacl.OperEnvironment.OperTime);

            return this.ExecNoQuery(Sql);
        }





        /// <summary>
        /// 查询所有控费包明细
        /// </summary>
        /// <param name="packid"></param>
        /// <returns></returns>
        public List<ItemMedicalDetail> QueryItemMedicalDetailById(string packid)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.SelectDetailByID", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.SelectDetailByID 的Sql语句失败！";
                return null;
            }
            if (this.ExecQuery(Sql, packid) < 0) return null;
            List<ItemMedicalDetail> list = new List<ItemMedicalDetail>();
            ItemMedicalDetail itemdetail = null;
            try
            {
                while (this.Reader.Read())
                {
                    itemdetail = new ItemMedicalDetail();
                    itemdetail.PackageId = this.Reader[0].ToString();
                    itemdetail.SequenceNo = this.Reader[1].ToString();
                    itemdetail.ItemCode = this.Reader[2].ToString();
                    itemdetail.ItemName = this.Reader[3].ToString();
                    itemdetail.ItemNum = int.Parse(this.Reader[4].ToString());
                    itemdetail.ItemSubcode = this.Reader[5].ToString();
                    itemdetail.ItemSubname = this.Reader[6].ToString();
                    itemdetail.UnitPrice = NConvert.ToDecimal(this.Reader[7]);
                    itemdetail.Memo = this.Reader[8].ToString();
                    itemdetail.OperEnvironment.ID = this.Reader[9].ToString();
                    itemdetail.OperEnvironment.OperTime = NConvert.ToDateTime(this.Reader[10]);
                    itemdetail.CreateEnvironment.ID = this.Reader[11].ToString();
                    itemdetail.CreateEnvironment.OperTime = NConvert.ToDateTime(this.Reader[12]);
                    itemdetail.MedicalDetailId = this.Reader[13].ToString();
                    list.Add(itemdetail);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }

            return list;

        }

        public int UpdateItemMedicalDetail(ItemMedicalDetail detail)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.UpdateItemMedicalDetail", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.UpdateItemMedicalDetail 的Sql语句失败！";
                return -1;
            }
            Sql = string.Format(Sql, detail.MedicalDetailId, detail.SequenceNo, detail.ItemCode, detail.ItemName, detail.ItemNum, detail.ItemSubcode, detail.ItemSubname, detail.UnitPrice, detail.Memo, detail.OperEnvironment.ID, detail.OperEnvironment.OperTime);

            return this.ExecNoQuery(Sql);
        }

        public int InsertItemMedicalDetail(ItemMedicalDetail detail)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.InsertItemMedicalDetail", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.InsertItemMedicalDetail 的Sql语句失败！";
                return -1;
            }
            Sql = string.Format(Sql, detail.PackageId, detail.SequenceNo, detail.ItemCode, detail.ItemName, detail.ItemNum, detail.ItemSubcode, detail.ItemSubname, detail.UnitPrice, detail.Memo, detail.OperEnvironment.ID, detail.OperEnvironment.OperTime, detail.CreateEnvironment.ID, detail.CreateEnvironment.OperTime);

            return this.ExecNoQuery(Sql);
        }

        public int DeleteItemMedicalDetail(ItemMedicalDetail detail)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.ItemMedical.DeleteItemMedicalDetail", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.ItemMedical.DeleteItemMedicalDetail 的Sql语句失败！";
                return -1;
            }
            Sql = string.Format(Sql, detail.MedicalDetailId);

            return this.ExecNoQuery(Sql);
        }



        /// <summary>
        /// 更新停车票发票状态// {17D86AD6-A28C-4518-951C-EE0F3504598B}
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="state"></param>
        /// <param name="dtTime"></param>
        /// <returns></returns>
        public int UpdateTicketInfoState(string invoiceNo, string state, string dtTime)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateTicketInfoState", ref strSql) == -1)
            {
                this.Err = "查找索引为Fee.Account.UpdateTicketInfoState的Sql语句失败！";
                return -1;
            }
            strSql = string.Format(strSql, invoiceNo, state, dtTime);

            return this.ExecNoQuery(strSql);

        }
        /// <summary>
        /// 更新停车票剩余数量
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public int UpdateTicketTotalQty(string operCode, string itemCode, string qty, string getQty)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.UpdateTicketTotalQty", ref strSql) == -1)
            {
                this.Err = "查找索引为Fee.Account.UpdateTicketTotalQty的Sql语句失败！";
                return -1;
            }
            strSql = string.Format(strSql, operCode, itemCode, qty, getQty, FS.FrameWork.Management.Connection.Operator.ID);

            return this.ExecNoQuery(strSql);

        }
        /// <summary>
        /// 插入停车票汇总信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertTicketTotal(FS.HISFC.Models.Base.Const obj)
        {
            string strSql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.Account.InsertTicketTotal", ref strSql) == -1)
            {
                this.Err = "查找索引为Fee.Account.InsertTicketTotal的Sql语句失败！";
                return -1;
            }
            strSql = string.Format(strSql, obj.ID, obj.Name, obj.UserCode, obj.Memo, obj.SpellCode, obj.SpellCode, obj.User01, obj.User02, obj.User03, obj.OperEnvironment.ID, obj.OperEnvironment.OperTime.ToString());

            return this.ExecNoQuery(strSql);

        }
        #endregion

        #region 现金流积分[购买套餐、门诊结算、充值、住院结算的现金消费累计情况]
        //{F166B18B-62E3-4835-A729-4CA384F9ADEE}

        public string GetCashConponVacancySeq()
        {
            return this.GetSequence("Fee.Account.CashCouponRecord");
        }

        /// <summary>
        /// 插入现金流积分汇总信息
        /// </summary>
        /// <param name="cashCoupon"></param>
        /// <returns></returns>
        public int InsertCashCouponVacancy(HISFC.Models.Account.CashCoupon cashCoupon)
        {
            try
            {
                string strSql = string.Empty;

                if (this.Sql.GetCommonSql("Fee.Account.InsertCashCouponVacancy", ref strSql) == -1)
                {
                    this.Err = "查找索引为Fee.Account.InsertCashCouponVacancy的Sql语句失败！";
                    return -1;
                }

                string[] couponParams = this.getCouponParams(cashCoupon);

                strSql = string.Format(strSql, couponParams);

                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新现金流积分汇总信息
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public int UpdateCashCouponVacancy(string cardno, decimal coupon)
        {
            try
            {
                string strSql = string.Empty;

                if (this.Sql.GetCommonSql("Fee.Account.UpdateCashCouponVacancy", ref strSql) == -1)
                {
                    this.Err = "查找索引为Fee.Account.UpdateCashCouponVacancy的Sql语句失败！";
                    return -1;
                }

                strSql = string.Format(strSql, cardno, coupon.ToString(), coupon.ToString());

                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 插入现金流积分明细记录
        /// </summary>
        /// <param name="cashCouponRecord"></param>
        /// <returns></returns>
        public int InsertCashCouponRecord(HISFC.Models.Account.CashCouponRecord cashCouponRecord)
        {
            try
            {
                string strSql = string.Empty;

                if (this.Sql.GetCommonSql("Fee.Account.InsertCashCouponRecord", ref strSql) == -1)
                {
                    this.Err = "查找索引为Fee.Account.InsertCashCouponRecord的Sql语句失败！";
                    return -1;
                }

                string[] couponParams = this.getCouponParams(cashCouponRecord);

                strSql = string.Format(strSql, couponParams);

                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取现金流积分汇总参数
        /// </summary>
        /// <param name="cashCoupon"></param>
        /// <returns></returns>
        public string[] getCouponParams(HISFC.Models.Account.CashCoupon cashCoupon)
        {
            string[] couponParams = {
                                        cashCoupon.CardNo,
                                        cashCoupon.CouponVacancy.ToString(),
                                        cashCoupon.CouponAccumulate.ToString()
                                    };
            return couponParams;

        }

        /// <summary>
        /// 获取现金流积分明细参数
        /// </summary>
        /// <param name="cashCouponRecord"></param>
        /// <returns></returns>
        public string[] getCouponParams(HISFC.Models.Account.CashCouponRecord cashCouponRecord)
        {
            string[] couponRecordParams = {
                                        cashCouponRecord.ID,
                                        cashCouponRecord.CardNo,
                                        cashCouponRecord.Coupon.ToString(),
                                        cashCouponRecord.CouponVacancy.ToString(),
                                        cashCouponRecord.InvoiceNo,
                                        cashCouponRecord.CouponType,
                                        cashCouponRecord.Memo,
                                        cashCouponRecord.OperEnvironment.ID,
                                        cashCouponRecord.OperEnvironment.OperTime.ToString()
                                    };
            return couponRecordParams;

        }

        #endregion


        /// <summary>
        /// // {473865F9-C2E6-4f05-BEB3-7CD1F0349126} 佛山居民医保二次改造
        /// 查询某个患者某个时间段内的医院垫付的诊查费用
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="lstCardFee"></param>
        /// <returns></returns>
        public int QueryAccountCardFeeByPubAndCardNoAndDate(string cardNo, DateTime dtBegin, DateTime dtEnd, out List<AccountCardFee> lstCardFee)
        {
            lstCardFee = null;

            string strWhere = string.Empty;
            if (this.Sql.GetSql("Fee.Account.CardFee.Where.ByPubAndCardNoAndDate", ref strWhere) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.Where.ByPubAndCardNoAndDate 的Sql语句失败！";
                return -1;
            }
            int iRes = 0;
            try
            {
                strWhere = string.Format(strWhere, cardNo, dtBegin.ToString(), dtEnd.ToString());

                iRes = this.QueryAccountCardFeeSQL(strWhere, out lstCardFee);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return iRes;
        }

        /// <summary>
        /// 更新挂号诊查费的社保状态// {473865F9-C2E6-4f05-BEB3-7CD1F0349126}
        /// </summary>
        /// <param name="cardFee"></param>
        /// <returns></returns>
        public int UpdateAccountCardFeeSiState(AccountCardFee cardFee)
        {
            if (cardFee == null)
            {
                return -1;
            }

            if (string.IsNullOrEmpty(cardFee.InvoiceNo))
            {
                this.Err = "发票流水号为空！";
                return -1;
            }

            string Sql = string.Empty;
            if (this.Sql.GetSql("Fee.Account.CardFee.UpdateSiState", ref Sql) == -1)
            {
                this.Err = this.Err = "查找索引为 Fee.Account.CardFee.UpdateSiState 的Sql语句失败！";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql,
                    cardFee.SiFlag,
                    cardFee.SiBalanceNO,

                    cardFee.InvoiceNo,
                    ((int)cardFee.TransType).ToString(),
                    ((int)cardFee.FeeType).ToString()
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }


    }

    /// <summary>
    /// 读取就诊卡号接口，根据卡号规则读取卡号和卡类型
    /// </summary>
    public interface IReadMarkNO
    {
        /// <summary>
        /// 根据本地卡号规则读取卡实体
        /// </summary>
        /// <param name="markNO">卡号</param>
        /// <returns>-1 失败 0卡规则正确但还没有发放 1发放</returns>
        int ReadMarkNOByRule(string markNO, ref FS.HISFC.Models.Account.AccountCard accountCard);
        /// <summary>
        /// 错误
        /// </summary>
        string Error
        {
            get;
            set;
        }
    }
}
