using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Management;
using System.Collections;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.RADT.BizLogic
{
    /// <summary>
    /// 住院ADT类，用于住院主表信息
    /// 重新SOC- 赵景
    /// </summary>
    public class Inpatient:Database
    {
        private string patientQuerySelect()
        {
            string sql = string.Empty;
            if (Sql.GetSql("RADT.Inpatient.PatientQuery.Select.1", ref sql) == -1)
            {
                return null;
            }
            return sql;
        }

        private ArrayList myPatientQuery(string SQLPatient)
        {
            ArrayList al = new ArrayList();
            PatientInfo PatientInfo;
            ProgressBarText = "正在查询患者...";
            ProgressBarValue = 0;

            if (ExecQuery(SQLPatient) == -1)
            {
                return null;
            }
            //取系统时间,用来得到年龄字符串
            DateTime sysDate = GetDateTimeFromSysDateTime();

            try
            {
                while (Reader.Read())
                {
                    PatientInfo = new PatientInfo();

                    #region "接口说明"

                    //<!-- 0  住院流水号,1 姓名 ,2   住院号   ,3 就诊卡号  ,4  病历号, 5  医疗证号
                    //,6    医疗类别,   7   性别   ,8   身份证号  ,9   拼音     ,10  生日
                    //,11   职业代码     ,12 职业名称,13   工作单位    ,14   工作单位电话      ,15   单位邮编
                    //,16   户口或家庭地址     ,17   家庭电话   ,18   户口或家庭邮编   , 19  籍贯id,20  籍贯name
                    //,21   出生地代码    , 22 出生地名称   ,23   民族id    ,24  民族name    ,25   联系人id
                    //,26   联系人姓名    ,27   联系人电话       ,28   联系人地址     ,29   联系人关系id , 30   联系人关系name
                    //,31   婚姻状况id    ,32  婚姻状况name  ,33   国籍id    , 34 国籍名称
                    //,35   身高           ,36   体重         ,37   血压      ,38   ABO血型
                    //,39   重大疾病标志    ,40   过敏标志            
                    //,41   入院日期      ,42   科室代码   , 43  科室名称  , 44  结算类别id 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
                    //,45   结算类别名称   , 46 合同代码   , 47  合同单位名称  , 48  床号
                    //, 49 护理单元代码  , 50  护理单元名称, 51 医师代码(住院), 52 医师姓名(住院)
                    //, 53 医师代码(主治) , 54 医师姓名(主治) , 55 医师代码(主任) , 56 医师姓名(主任)
                    //, 57 医师代码(实习) , 58 医师姓名(实习), 59  护士代码(责任), 60  护士姓名(责任)
                    //, 61  入院情况id  , 62  入院情况name   , 63  入院途径id    , 64  入院途径name      
                    //, 65  入院来源id 1 -门诊 2 -急诊 3 -转科 4 -转院    , 66  入院来源name
                    //, 67  在院状态 住院登记  i-病房接诊 -出院登记 o-出院结算 p-预约出院 n-无费退院
                    //,  68  出院日期(预约)  , 69  出院日期 , 70  是否在ICU 0 no 1 yes,71 icu code,72 icu name
                    //,73 楼 ,74 层,75 屋 
                    //,76 总共金额TotCost ,77 自费金额 OwnCost,	78 自付金额 PayCost,79 公费金额 PubCost
                    //,80 剩余金额 LeftCost,81 优惠金额
                    //,82  预交金额 ，83    费用金额(已结)，84    预交金额(已结) ， 85 结算日期(上次)     
                    //，86 警戒线, 87 转归代号,88 TransferPrepayCost 转入预交金额（未结)  ,89 转入费用金额(未结),90 病床状态91公费日限额超标部分
                    //,92 特注93公费日限额94血滞纳金95住院次数96床位上限97空调上限98门诊诊断99收住医师100生育保险电脑号
                    //-->

                    #endregion

                    try
                    {
                        if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString(); // 住院流水号
                        if (!Reader.IsDBNull(0)) PatientInfo.ID = Reader[0].ToString(); // 住院流水号
                        if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //姓名
                        if (!Reader.IsDBNull(1)) PatientInfo.Name = Reader[1].ToString(); //姓名
                        if (!Reader.IsDBNull(2)) PatientInfo.PID.PatientNO = Reader[2].ToString(); //  住院号						
                        if (!Reader.IsDBNull(3)) PatientInfo.PID.CardNO = Reader[3].ToString(); //就诊卡号
                        if (!Reader.IsDBNull(4)) PatientInfo.PID.CaseNO = Reader[4].ToString(); // 病历号
                        if (!Reader.IsDBNull(5)) PatientInfo.SSN = Reader[5].ToString(); // 医疗证号
                        if (!Reader.IsDBNull(6)) PatientInfo.PVisit.MedicalType.ID = Reader[6].ToString(); //   医疗类别id
                        if (!Reader.IsDBNull(7)) PatientInfo.Sex.ID = Reader[7].ToString(); //  性别
                        if (!Reader.IsDBNull(8)) PatientInfo.IDCard = Reader[8].ToString(); //  身份证号
                        if (!Reader.IsDBNull(9)) PatientInfo.Memo = Reader[9].ToString(); //  拼音
                        if (!Reader.IsDBNull(10)) PatientInfo.Birthday = NConvert.ToDateTime(Reader[10]); // 生日
                        if (!Reader.IsDBNull(11)) PatientInfo.Profession.ID = Reader[11].ToString(); //  职业代码
                        if (!Reader.IsDBNull(12)) PatientInfo.Profession.Name = Reader[12].ToString(); //职业名称
                        if (!Reader.IsDBNull(13)) PatientInfo.CompanyName = Reader[13].ToString(); //  工作单位
                        if (!Reader.IsDBNull(14)) PatientInfo.PhoneBusiness = Reader[14].ToString(); //  工作单位电话
                        if (!Reader.IsDBNull(15)) PatientInfo.User01 = Reader[15].ToString(); //  单位邮编
                        if (!Reader.IsDBNull(16)) PatientInfo.AddressHome = Reader[16].ToString(); //  户口或家庭地址
                        if (!Reader.IsDBNull(17)) PatientInfo.PhoneHome = Reader[17].ToString(); //  家庭电话
                        if (!Reader.IsDBNull(18)) PatientInfo.User02 = Reader[18].ToString(); //  户口或家庭邮编
                        if (!Reader.IsDBNull(20)) PatientInfo.DIST = Reader[20].ToString(); // 籍贯name
                        if (!Reader.IsDBNull(21)) PatientInfo.AreaCode = Reader[21].ToString(); //  出生地代码
                        if (!Reader.IsDBNull(23)) PatientInfo.Nationality.ID = Reader[23].ToString(); //  民族id
                        if (!Reader.IsDBNull(24)) PatientInfo.Nationality.Name = Reader[24].ToString(); // 民族name
                        if (!Reader.IsDBNull(25)) PatientInfo.Kin.ID = Reader[25].ToString(); //  联系人id
                        if (!Reader.IsDBNull(26)) PatientInfo.Kin.Name = Reader[26].ToString(); //  联系人姓名
                        if (!Reader.IsDBNull(27)) PatientInfo.Kin.RelationPhone = Reader[27].ToString(); //  联系人电话
                        if (!Reader.IsDBNull(28)) PatientInfo.Kin.RelationAddress = Reader[28].ToString(); //  联系人地址
                        if (!Reader.IsDBNull(29)) PatientInfo.Kin.Relation.ID = Reader[29].ToString(); //  联系人关系id
                        if (!Reader.IsDBNull(30)) PatientInfo.Kin.Relation.Name = Reader[30].ToString(); //  联系人关系name
                        if (!Reader.IsDBNull(31)) PatientInfo.MaritalStatus.ID = Reader[31].ToString(); //  婚姻状况id
                        if (!Reader.IsDBNull(33)) PatientInfo.Country.ID = Reader[33].ToString(); //  国籍id
                        if (!Reader.IsDBNull(34)) PatientInfo.Country.Name = Reader[34].ToString(); //国籍名称
                        if (!Reader.IsDBNull(35)) PatientInfo.Height = Reader[35].ToString(); //  身高
                        if (!Reader.IsDBNull(36)) PatientInfo.Weight = Reader[36].ToString(); //  体重
                        if (!Reader.IsDBNull(38)) PatientInfo.BloodType.ID = Reader[38].ToString(); //  ABO血型
                        if (!Reader.IsDBNull(39)) PatientInfo.Disease.IsMainDisease = NConvert.ToBoolean(Reader[39]); //  重大疾病标志
                        if (!Reader.IsDBNull(40)) PatientInfo.Disease.IsAlleray = NConvert.ToBoolean(Reader[40]); //  过敏标志
                        if (!Reader.IsDBNull(41)) PatientInfo.PVisit.InTime = NConvert.ToDateTime(Reader[41]); //  入院日期
                        if (!Reader.IsDBNull(42)) PatientInfo.PVisit.PatientLocation.Dept.ID = Reader[42].ToString(); //  科室代码
                        if (!Reader.IsDBNull(43)) PatientInfo.PVisit.PatientLocation.Dept.Name = Reader[43].ToString(); // 科室名称
                        if (!Reader.IsDBNull(44)) PatientInfo.Pact.PayKind.ID = Reader[44].ToString();
                        // 结算类别id 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
                        if (!Reader.IsDBNull(45)) PatientInfo.Pact.PayKind.Name = Reader[45].ToString(); //  结算类别名称
                        if (!Reader.IsDBNull(46)) PatientInfo.Pact.ID = Reader[46].ToString(); //合同代码
                        if (!Reader.IsDBNull(47)) PatientInfo.Pact.Name = Reader[47].ToString(); // 合同单位名称
                        if (!Reader.IsDBNull(48)) PatientInfo.PVisit.PatientLocation.Bed.ID = Reader[48].ToString(); // 床号
                        if (!Reader.IsDBNull(48))
                            PatientInfo.PVisit.PatientLocation.Bed.Name = PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4
                                                                            ? PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4)
                                                                            : PatientInfo.PVisit.PatientLocation.Bed.ID; // 床号
                        if (!Reader.IsDBNull(90)) PatientInfo.PVisit.PatientLocation.Bed.Status.ID = Reader[90].ToString(); //床位状态
                        if (!Reader.IsDBNull(90)) PatientInfo.PVisit.PatientLocation.Bed.InpatientNO = Reader[0].ToString(); // 住院流水号
                        if (!Reader.IsDBNull(49)) PatientInfo.PVisit.PatientLocation.NurseCell.ID = Reader[49].ToString(); //护理单元代码
                        if (!Reader.IsDBNull(50)) PatientInfo.PVisit.PatientLocation.NurseCell.Name = Reader[50].ToString(); // 护理单元名称
                        if (!Reader.IsDBNull(51)) PatientInfo.PVisit.AdmittingDoctor.ID = Reader[51].ToString(); //医师代码(住院)
                        if (!Reader.IsDBNull(52)) PatientInfo.PVisit.AdmittingDoctor.Name = Reader[52].ToString(); //医师姓名(住院)
                        if (!Reader.IsDBNull(53)) PatientInfo.PVisit.AttendingDoctor.ID = Reader[53].ToString(); //医师代码(主治)
                        if (!Reader.IsDBNull(54)) PatientInfo.PVisit.AttendingDoctor.Name = Reader[54].ToString(); //医师姓名(主治)
                        if (!Reader.IsDBNull(55)) PatientInfo.PVisit.ConsultingDoctor.ID = Reader[55].ToString(); //医师代码(主任)
                        if (!Reader.IsDBNull(56)) PatientInfo.PVisit.ConsultingDoctor.Name = Reader[56].ToString(); //医师姓名(主任)
                        if (!Reader.IsDBNull(57)) PatientInfo.PVisit.TempDoctor.ID = Reader[57].ToString(); //医师代码(实习)
                        if (!Reader.IsDBNull(58)) PatientInfo.PVisit.TempDoctor.Name = Reader[58].ToString(); //医师姓名(实习)
                        if (!Reader.IsDBNull(59)) PatientInfo.PVisit.AdmittingNurse.ID = Reader[59].ToString(); // 护士代码(责任)
                        if (!Reader.IsDBNull(60)) PatientInfo.PVisit.AdmittingNurse.Name = Reader[60].ToString(); // 护士姓名(责任)
                        if (!Reader.IsDBNull(61)) PatientInfo.PVisit.Circs.ID = Reader[61].ToString(); // 入院情况id
                        if (!Reader.IsDBNull(62)) PatientInfo.PVisit.Circs.Name = Reader[62].ToString(); // 入院情况name
                        if (!Reader.IsDBNull(63)) PatientInfo.PVisit.AdmitSource.ID = Reader[63].ToString(); // 入院途径id
                        if (!Reader.IsDBNull(64)) PatientInfo.PVisit.AdmitSource.Name = Reader[64].ToString(); // 入院途径name
                        if (!Reader.IsDBNull(65)) PatientInfo.PVisit.InSource.ID = Reader[65].ToString();
                        // 入院来源id 1 -门诊 2 -急诊 3 -转科 4 -转院
                        if (!Reader.IsDBNull(66)) PatientInfo.PVisit.InSource.Name = Reader[66].ToString(); // 入院来源name
                        if (!Reader.IsDBNull(67)) PatientInfo.PVisit.InState.ID = Reader[67].ToString();
                        // 在院状态 住院登记  i-病房接诊 -出院登记 o-出院结算 p-预约出院 n-无费退院
                        if (!Reader.IsDBNull(68)) PatientInfo.PVisit.PreOutTime = NConvert.ToDateTime(Reader[68]); // 出院日期(预约)
                        #region {8D72F2C7-624C-41e4-9922-7A5556B9D82E}
                        if (!Reader.IsDBNull(69))
                        {
                            if (NConvert.ToDateTime(Reader[69]) < NConvert.ToDateTime("1000-01-01"))
                            {
                                PatientInfo.PVisit.OutTime = DateTime.MinValue;
                            }
                            else//{3D0766DE-A5AA-409f-8A04-C56F4C9D53DA}
                            {
                                PatientInfo.PVisit.OutTime = NConvert.ToDateTime(Reader[69]);
                            }

                        }
                        #endregion
                        if (!Reader.IsDBNull(71)) PatientInfo.PVisit.ICULocation.ID = Reader[71].ToString(); //icu code
                        if (!Reader.IsDBNull(72)) PatientInfo.PVisit.ICULocation.Name = Reader[72].ToString(); //icu name
                        if (!Reader.IsDBNull(73)) PatientInfo.PVisit.PatientLocation.Building = Reader[73].ToString(); //楼
                        if (!Reader.IsDBNull(74)) PatientInfo.PVisit.PatientLocation.Floor = Reader[74].ToString(); //层
                        if (!Reader.IsDBNull(75)) PatientInfo.PVisit.PatientLocation.Room = Reader[75].ToString(); //屋
                        if (!Reader.IsDBNull(76)) PatientInfo.FT.TotCost = NConvert.ToDecimal(Reader[76]); //总共金额TotCost
                        if (!Reader.IsDBNull(77)) PatientInfo.FT.OwnCost = NConvert.ToDecimal(Reader[77]); //自费金额 OwnCost
                        if (!Reader.IsDBNull(78)) PatientInfo.FT.PayCost = NConvert.ToDecimal(Reader[78]); //自付金额 PayCost
                        if (!Reader.IsDBNull(79)) PatientInfo.FT.PubCost = NConvert.ToDecimal(Reader[79]); //公费金额 PubCost
                        if (!Reader.IsDBNull(80)) PatientInfo.FT.LeftCost = NConvert.ToDecimal(Reader[80]); //剩余金额 LeftCost
                        if (!Reader.IsDBNull(81)) PatientInfo.FT.RebateCost = NConvert.ToDecimal(Reader[81]); //优惠金额
                        if (!Reader.IsDBNull(82)) PatientInfo.FT.PrepayCost = NConvert.ToDecimal(Reader[82]); // 预交金额
                        if (!Reader.IsDBNull(83)) PatientInfo.FT.BalancedCost = NConvert.ToDecimal(Reader[83]); //   费用金额(已结)
                        if (!Reader.IsDBNull(84)) PatientInfo.FT.BalancedPrepayCost = NConvert.ToDecimal(Reader[84]); //   预交金额(已结)
                        if (!Reader.IsDBNull(85))
                        {
                            try
                            {
                                PatientInfo.BalanceDate = NConvert.ToDateTime(Reader[85]); //结算时间
                            }
                            catch { }
                        }
                        if (!Reader.IsDBNull(86)) PatientInfo.PVisit.MoneyAlert = NConvert.ToDecimal(Reader[86]); //警戒线
                        if (!Reader.IsDBNull(87)) PatientInfo.PVisit.ZG.ID = Reader[87].ToString(); //  转归代码
                        if (!Reader.IsDBNull(88)) PatientInfo.FT.TransferPrepayCost = NConvert.ToDecimal(Reader[88]); // 转入预交金额（未结) 
                        if (!Reader.IsDBNull(89)) PatientInfo.FT.TransferTotCost = NConvert.ToDecimal(Reader[89]); //  转入费用金额（未结) 
                        if (!Reader.IsDBNull(91)) PatientInfo.FT.OvertopCost = NConvert.ToDecimal(Reader[91]); //   公费超标金额
                        if (!Reader.IsDBNull(92)) PatientInfo.Memo = Reader[92].ToString(); //特注
                        if (!Reader.IsDBNull(93)) PatientInfo.FT.DayLimitCost = NConvert.ToDecimal(Reader[93]); //   公费日限额
                        if (!Reader.IsDBNull(94)) PatientInfo.FT.BloodLateFeeCost = NConvert.ToDecimal(Reader[94]); //  血滞纳金
                        if (!Reader.IsDBNull(95)) PatientInfo.InTimes = NConvert.ToInt32(Reader[95]); //住院次数
                        if (!Reader.IsDBNull(96)) PatientInfo.FT.BedLimitCost = NConvert.ToDecimal(Reader[96].ToString()); //床位上限
                        if (!Reader.IsDBNull(97)) PatientInfo.FT.AirLimitCost = NConvert.ToDecimal(Reader[97].ToString()); //空调上限
                        if (!Reader.IsDBNull(99)) PatientInfo.DoctorReceiver.ID = Reader[99].ToString();
                        if (!Reader.IsDBNull(98)) PatientInfo.ClinicDiagnose = Reader[98].ToString();
                        if (!Reader.IsDBNull(100)) PatientInfo.ProCreateNO = Reader[100].ToString(); //生育保险电脑号
                        PatientInfo.IsHasBaby = NConvert.ToBoolean(Reader[101].ToString()); //是否有婴儿
                        PatientInfo.Disease.Tend.Name = Reader[102].ToString(); //护理级别
                        //费用收取间隔
                        PatientInfo.FT.FixFeeInterval = NConvert.ToInt32(Reader[103].ToString());
                        //上次收取时间
                        PatientInfo.FT.PreFixFeeDateTime = NConvert.ToDateTime(Reader[104].ToString());
                        //床费超标处理 0超标不限1超标自理
                        PatientInfo.FT.BedOverDeal = Reader[105].ToString();
                        //日限额累计
                        PatientInfo.FT.DayLimitTotCost = NConvert.ToDecimal(Reader[106].ToString());
                        //病案状态: 0 无需病案 1 需要病案 2 医生站形成病案 3 病案室形成病案 4病案封存
                        PatientInfo.CaseState = Reader[107].ToString();

                        PatientInfo.ExtendFlag = Reader[108].ToString(); //是否允许日限额超标 0 不同意 1 同意 中山一院需求
                        PatientInfo.ExtendFlag1 = Reader[109].ToString(); //扩展标记1
                        PatientInfo.ExtendFlag2 = Reader[110].ToString(); //扩展标记2
                        PatientInfo.FT.BoardCost = NConvert.ToDecimal(Reader[111]); //膳食花费总额
                        PatientInfo.FT.BoardPrepayCost = NConvert.ToDecimal(Reader[112]); //膳食预交金额
                        PatientInfo.PVisit.BoardState = Reader[113].ToString(); //膳食结算状态：0在院 1出院
                        PatientInfo.FT.FTRate.OwnRate = NConvert.ToDecimal(Reader[114].ToString()); //自费比例
                        PatientInfo.FT.FTRate.PayRate = NConvert.ToDecimal(Reader[115].ToString()); //自付比例
                        PatientInfo.FT.DrugFeeTotCost = NConvert.ToDecimal(Reader[116].ToString()); //公费患者公费药品累计(日限额)
                        PatientInfo.MainDiagnose = Reader[117].ToString(); //患者住院主诊断

                        PatientInfo.Age = GetAge(PatientInfo.Birthday, sysDate); //根据出生日期取患者年龄
                        PatientInfo.IsEncrypt = FS.FrameWork.Function.NConvert.ToBoolean(Reader[118].ToString());
                        PatientInfo.NormalName = Reader[119].ToString();
                        if (!Reader.IsDBNull(120)) PatientInfo.BalanceNO = NConvert.ToInt32(Reader[120].ToString());//结算次数
                        //{F0C48258-8EFB-4356-B730-E852EE4888A0}
                        PatientInfo.ExtendFlag1 = Reader[121].ToString();//病情


                        if (PatientInfo.PID.PatientNO.StartsWith("B"))
                        {
                            //是否婴儿
                            PatientInfo.IsBaby = true;
                        }
                        else
                        {
                            PatientInfo.IsBaby = false;
                        }

                        //{2FA0D4CE-E2EB-4bc7-975A-3693B71C62CF}
                        if (!Reader.IsDBNull(122))
                        {
                            PatientInfo.IsStopAcount = FS.FrameWork.Function.NConvert.ToBoolean(Reader[122].ToString());
                        }
                        PatientInfo.PVisit.AlertType.ID = this.Reader[123].ToString();
                        PatientInfo.PVisit.BeginDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[124]);
                        PatientInfo.PVisit.EndDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[125]);
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
                    //获得变更信息

                    #region "获得变更信息"

                    //deleted by cuipeng 2005-5 不知道此功能为啥用,而且有问题
                    //this.myGetTempLocation(PatientInfo);

                    #endregion

                    ProgressBarValue++;
                    al.Add(PatientInfo);
                }
            } //抛出错误
            catch (Exception ex)
            {
                Err = "获得患者基本信息出错！" + ex.Message;
                ErrCode = "-1";
                WriteErr();
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return al;
            }
            Reader.Close();

            ProgressBarValue = -1;
            return al;
        }

        /// <summary>
        /// 查询病人基本信息 适用于 用组合查询框 生成查询条件的情况。
        /// </summary>
        /// <param name="strWhere"> 传入的where 条件</param>
        /// <returns> 成功返回数组，失败返回null</returns>
        public ArrayList QueryPatientInfo(string strWhere)
        {
            //定义要返回的数组
            ArrayList al = new ArrayList();
            try
            {
                //获取检索主SQL语句
                string strSql = patientQuerySelect();
                strSql = strSql + " " + strWhere;
                //查询患者信息
                al = myPatientQuery(strSql);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                WriteErr();
                return null;
            }
            return al;
        }

        /// <summary>
        /// 根据住院流水号获取最新病房接诊时间
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public string GetMaxKDate(string clinicNO)
        {
            string sql = string.Empty;
            if (Sql.GetSql("RADT.Inpatient.GetMaxKDate", ref sql) == -1)
            {
                return "";
            }
            sql = string.Format(sql, clinicNO);
            return this.ExecSqlReturnOne(sql);
        }

        #region 身份变更

        /// <summary>
        /// 身份变更（包括公费信息）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdatePactInfo(PatientInfo patient, FS.HISFC.Models.Base.PactInfo obj)
        {
            string strSql = FS.SOC.HISFC.RADT.Data.AbstractPatientInfo.Current.UpdatePactInfo;

            try
            {
                strSql = string.Format(strSql,
                    patient.ID,	//0 		住院号	
                    obj.ID,	//1 合同单位代码
                    obj.Name,		//2 	合同单位名称	
                    obj.PayKind.ID,	//3 结算类别代码
                    obj.User03,//4卡号
                    patient.FT.FixFeeInterval.ToString(),//5固定费用周期
                    patient.FT.BedLimitCost.ToString(),//6床位标准
                    patient.FT.BedOverDeal,//7床位处理
                    patient.FT.DayLimitCost.ToString(),//8日限额
                    patient.FT.DayLimitTotCost.ToString(),//9日限总额
                    patient.FT.OvertopCost.ToString(),//10超标金额
                    patient.FT.AirLimitCost.ToString()//11监护床									
                    );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSql);

        }

        #endregion

        #region 插入住院主表--2012-07-17
         /// <summary>
        /// 插入住院主表 
        /// </summary>
        /// <param name="PatientInfo">插入住院主表</param>
        /// <returns>0成功 -1失败</returns>患者基本信息
        public int InsertPatient(PatientInfo PatientInfo)
        {
            string strSql = string.Empty;
            if (Sql.GetSql("RADT.InPatient.RegisterPatient.SOC.1", ref strSql) == -1)
            {
                return -1;
            }

            try
            {
                string[] s = new string[81];
                try
                {
                    s[0] = PatientInfo.ID; // --住院流水号
                    s[1] = PatientInfo.Name; //--姓名
                    s[2] = PatientInfo.PID.PatientNO; //  --住院号
                    s[3] = PatientInfo.PID.CardNO; //  --就诊卡号
                    s[4] = PatientInfo.SSN; //  --医疗证号
                    s[5] = PatientInfo.PVisit.MedicalType.ID; //    --医疗类别id zhouxs
                    s[6] = PatientInfo.Sex.ID.ToString(); //  --性别
                    s[7] = PatientInfo.IDCard; //  --身份证号
                    s[8] = PatientInfo.Memo; //  --拼音
                    s[9] = PatientInfo.Birthday.ToString(); //  --生日
                    s[10] = PatientInfo.Profession.ID; //  --职业名称
                    s[11] = PatientInfo.CompanyName; //  --工作单位
                    s[12] = PatientInfo.PhoneBusiness; //  --工作单位电话
                    s[13] = PatientInfo.BusinessZip; //  --单位邮编
                    s[14] = PatientInfo.AddressHome; //  --户口或家庭地址
                    s[15] = PatientInfo.PhoneHome; //  --家庭电话
                    s[16] = PatientInfo.HomeZip; //  --户口或家庭邮编
                    s[17] = PatientInfo.DIST; // --籍贯name
                    s[18] = PatientInfo.AreaCode; //  --出生地代码---地区
                    s[19] = PatientInfo.Nationality.ID; //  --民族id
                    s[20] = PatientInfo.Nationality.Name; // --民族name
                    s[21] = PatientInfo.Kin.Name; //  --联系人姓名
                    s[22] = PatientInfo.Kin.RelationPhone; //  --联系人电话
                    s[23] = PatientInfo.Kin.RelationAddress; //  --联系人地址
                    s[24] = PatientInfo.Kin.Relation.ID; //  --联系人关系id
                    s[25] = PatientInfo.Kin.Relation.Name; //  --联系人关系name
                    s[26] = PatientInfo.MaritalStatus.ID.ToString(); //  --婚姻状况id
                    s[27] = PatientInfo.MaritalStatus.Name; // --婚姻状况name
                    s[28] = PatientInfo.Country.ID; //  --国籍id
                    s[29] = PatientInfo.Country.Name; //--国籍名称
                    s[30] = PatientInfo.Height; //  --身高
                    s[31] = PatientInfo.Weight; //  --体重
                    s[32] = PatientInfo.Profession.ID; //  --职业id
                    s[33] = PatientInfo.BloodType.ID.ToString(); //  --ABO血型
                    s[34] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsMainDisease).ToString(); //  --重大疾病标志
                    s[35] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.Disease.IsAlleray).ToString(); //  --过敏标志
                    s[36] = PatientInfo.PVisit.InTime.ToString(); //  --入院日期
                    s[37] = PatientInfo.PVisit.PatientLocation.Dept.ID; //  --科室代码
                    s[38] = PatientInfo.PVisit.PatientLocation.Dept.Name; // --科室名称
                    s[39] = PatientInfo.Pact.PayKind.ID; // --结算类别id 1-自费  2-保险 3-公费在职 4-公费退休 5-公费高干
                    s[40] = PatientInfo.Pact.PayKind.Name; //  --结算类别名称
                    s[41] = PatientInfo.Pact.ID; // --合同代码
                    s[42] = PatientInfo.Pact.Name; // --合同单位名称
                    s[43] = PatientInfo.PVisit.PatientLocation.Bed.ID; // --床号
                    s[44] = PatientInfo.PVisit.PatientLocation.NurseCell.ID; //--护理单元代码
                    s[45] = PatientInfo.PVisit.PatientLocation.NurseCell.Name; // --护理单元名称
                    s[46] = PatientInfo.PVisit.AdmittingDoctor.ID; //--医师代码(住院)
                    s[47] = PatientInfo.PVisit.AdmittingDoctor.Name; //--医师姓名(住院)
                    s[48] = PatientInfo.PVisit.AttendingDoctor.ID; // --医师代码(主治)
                    s[49] = PatientInfo.PVisit.AttendingDoctor.Name; //--医师姓名(主治)
                    s[50] = PatientInfo.PVisit.ConsultingDoctor.ID; // --医师代码(主任)
                    s[51] = PatientInfo.PVisit.ConsultingDoctor.Name; //--医师姓名(主任)
                    s[52] = PatientInfo.PVisit.TempDoctor.ID; //--医师代码(实习)
                    s[53] = PatientInfo.PVisit.TempDoctor.Name; //--医师姓名(实习)
                    s[54] = PatientInfo.PVisit.AdmittingNurse.ID; // --护士代码(责任)
                    s[55] = PatientInfo.PVisit.AdmittingNurse.Name; // --护士姓名(责任)
                    s[56] = PatientInfo.PVisit.Circs.ID; // --入院情况id
                    s[57] = PatientInfo.PVisit.Circs.Name; // --入院情况name
                    s[58] = PatientInfo.PVisit.AdmitSource.ID; // --入院途径id
                    s[59] = PatientInfo.PVisit.AdmitSource.Name; // --入院途径name
                    s[60] = PatientInfo.PVisit.InSource.ID; // --入院来源id 1 -门诊 2 -急诊 3 -转科 4 -转院
                    s[61] = PatientInfo.PVisit.InSource.Name; // --入院来源name
                    s[62] = PatientInfo.PVisit.InState.ID.ToString(); // --住院登记  i-病房接诊 -出院登记 o-出院结算 p-预约出院 n-无费退院
                    s[63] = PatientInfo.PVisit.PreOutTime.ToString(); // --出院日期(预约)
                    s[64] = PatientInfo.PVisit.OutTime.ToString(); // --出院日期
                    if (PatientInfo.PVisit.ICULocation == null)
                    {
                        s[65] = "0";
                    }
                    else
                    {
                        s[65] = "1"; // --是否在ICU
                    }
                    s[66] = Operator.ID;
                    s[67] = PatientInfo.DoctorReceiver.ID; //收住医师
                    s[68] = PatientInfo.InTimes.ToString(); //住院次数
                    s[69] = PatientInfo.FT.FixFeeInterval.ToString(); //床费间隔
                    s[70] = PatientInfo.ClinicDiagnose; //门诊诊断

                    try
                    {
                        //-----------Add-----------
                        s[71] = PatientInfo.FT.DayLimitCost.ToString(); //公费日限
                        s[72] = PatientInfo.FT.BedLimitCost.ToString();//普通标准(床位上限)
                        s[73] = PatientInfo.FT.AirLimitCost.ToString();//监护床(空调上限)
                        s[74] = PatientInfo.FT.FTRate.PayRate.ToString(); //自付比例
                        s[75] = PatientInfo.FT.BedOverDeal; //超标处理
                        //------------EedAdd---------------
                        s[76] = PatientInfo.ExtendFlag; //是否允许日限额超标 0 不同意 1 同意
                        s[77] = PatientInfo.ExtendFlag1;
                        s[78] = PatientInfo.ExtendFlag2;
                    }
                    catch
                    {
                    }
                    s[79] = FS.FrameWork.Function.NConvert.ToInt32(PatientInfo.IsEncrypt).ToString();
                    s[80] = PatientInfo.NormalName;
                }
                catch (Exception ex)
                {
                    Err = "赋值时候出错！" + ex.Message;
                    WriteErr();
                    return -1;
                }
                strSql = string.Format(strSql, s);
            }
            catch (Exception ex)
            {
                Err = "赋值时候出错！" + ex.Message;
                WriteErr();
                return -1;
            }

            return ExecNoQuery(strSql);
        }
        #endregion

    }
}
