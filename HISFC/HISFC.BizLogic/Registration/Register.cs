using System;
using System.Collections;
using FS.HISFC.Models.RADT;
using FS.FrameWork.Function;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.Registration
{
    /// <summary>
    /// 挂号管理类
    /// </summary>
    public class Register : FS.FrameWork.Management.Database
    {
        /// <summary>
        ///  挂号管理类
        /// </summary>
        public Register()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        //private ArrayList al = new ArrayList();
        private FS.HISFC.Models.Registration.Register reg;
       
        #region 增、删、改

        //账户流程 医生站收挂号费，置挂号费收费状态 {6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// 置已收挂号费标志
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="operID"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateAccountFeeState(string clinicID, string operID, string dept, DateTime operDate)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateAccountFeeState", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, operID, dept, operDate.ToString());
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "置患者收费标志出错![Registration.Register.UpdateAccountFeeState]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// 更新挂号记录费用信息
        /// </summary>
        /// <param name="objRegister"></param>
        /// <returns></returns>
        public int UpdateRegFeeCost(FS.HISFC.Models.Registration.Register objRegister)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateRegFeeCost", ref sql) == -1)
            {
                this.Err = "查找索引为 Registration.Register.UpdateRegFeeCost 的Sql语句失败！";
                return -1;
            }

            try
            {
                sql = string.Format(sql,
                    objRegister.ID,
                    objRegister.InvoiceNO,
                    objRegister.RegLvlFee.RegFee,
                    objRegister.RegLvlFee.ChkFee,
                    objRegister.RegLvlFee.OwnDigFee,
                    objRegister.RegLvlFee.OthFee,
                    objRegister.OwnCost,
                    objRegister.PubCost,
                    objRegister.PayCost);

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "置患者收费标志出错![Registration.Register.UpdateRegFeeCost]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 删除挂号记录表{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int DeleteByClinic(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Delete", ref sql) == -1) return -1;

            try
            {
                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                sql = string.Format(sql, register.ID);
            }
            catch
            {
                return -1;
            }

            return this.ExecNoQuery(sql);

        }


        /// <summary>
        /// 插入挂号记录表{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Registration.Register register)
        {
            //{C8C76028-D071-41ce-8276-C7FA91F9F0C0}
            FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
            try
            {
                dept =this.GetDeptmentById(register.DoctorInfo.Templet.Dept.ID.ToString());
            }
            catch (Exception)
            {
               
            }
            string sql = "";
            if (register.TranType == FS.HISFC.Models.Base.TransTypes.Positive)
            {
                if (this.Sql.GetCommonSql("Registration.Register.GetInTimes", ref sql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }

                //先获取登记次数
                string inTimes = this.ExecSqlReturnOne(string.Format(sql, register.PID.CardNO));
                if (string.IsNullOrEmpty(inTimes) || inTimes.Equals("-1"))
                {
                    return -1;
                }

                register.InTimes = FS.FrameWork.Function.NConvert.ToInt32(inTimes);
            }

            if (this.Sql.GetCommonSql("Registration.Register.Insert.1", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {



                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                sql = string.Format(sql,
                    register.ID,                                                        //门诊号/发票号
                    register.PID.CardNO,                                                //就诊卡号
                    register.DoctorInfo.SeeDate.ToString(),                             // --挂号日期
                    register.DoctorInfo.Templet.Noon.ID,                                //午别
                    register.Name,                                                      //姓名
                    register.IDCard,                                                    //身份证号
                    register.Sex.ID,                                                    //性别
                    register.Birthday.ToString(),                                       //出生日
                    register.Pact.PayKind.ID,                                           //结算类别号
                    register.Pact.PayKind.Name,                                         //结算类别名称
                    register.Pact.ID,                                                   //合同号
                    register.Pact.Name,                                                 //合同单位名称
                    register.SSN,                                                       //医疗证号
                    register.DoctorInfo.Templet.RegLevel.ID,                            //挂号级别
                    register.DoctorInfo.Templet.RegLevel.Name,                          //挂号级别名称
                    register.DoctorInfo.Templet.Dept.ID,                                //科室号
                    register.DoctorInfo.Templet.Dept.Name,                              //科室名称
                    register.DoctorInfo.SeeNO,                                          //看诊序号
                    register.DoctorInfo.Templet.Doct.ID,                                //医师代号
                    register.DoctorInfo.Templet.Doct.Name,                              //医师姓名
                    FS.FrameWork.Function.NConvert.ToInt32(register.IsFee),                       //挂号收费标志  
                    (int)register.RegType,                                                             //是否预约   
                    FS.FrameWork.Function.NConvert.ToInt32(register.IsFirst),                     //1初诊/2复诊   
                    register.RegLvlFee.RegFee.ToString(),                                              //挂号费   
                    register.RegLvlFee.ChkFee.ToString(),                                              //检查费   
                    register.RegLvlFee.OwnDigFee.ToString(),                                           //诊察费   
                    register.RegLvlFee.OthFee.ToString(),                                              //附加费   
                    register.OwnCost.ToString(),                                                       //自费金额   
                    register.PubCost.ToString(),                                                       //报销金额   
                    register.PayCost.ToString(),                                                       //自付金额   
                    (int)register.Status,                                                              //有效标志   
                    register.InputOper.ID,                                                             //操作员代码   
                    FS.FrameWork.Function.NConvert.ToInt32(register.IsSee),                       //是否看诊   
                    FS.FrameWork.Function.NConvert.ToInt32(register.CheckOperStat.IsCheck),       //1未核查/2已核查   
                    register.PhoneHome,                                                                //联系电话   
                    register.AddressHome,                                                              //地址   
                    (int)register.TranType,                                                            //交易类型   
                    register.CardType.ID,                                                              //证件类型   
                    register.DoctorInfo.Templet.Begin.ToString(),                                      //开始时间   
                    register.DoctorInfo.Templet.End.ToString(),                                        //开始时间
                    register.CancelOper.ID,                                                             //作废人
                    register.CancelOper.OperTime.ToString(),                                          //作废时间
                    register.InvoiceNO,                                                               //发票号
                    register.RecipeNO,                                                                //处方号
                    FS.FrameWork.Function.NConvert.ToInt32(register.DoctorInfo.Templet.IsAppend),
                    register.OrderNO,
                    register.DoctorInfo.Templet.ID,
                    register.InputOper.OperTime.ToString(),
                    register.InSource.ID,
                    FS.FrameWork.Function.NConvert.ToInt32(register.CaseState),
                    FS.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt),
                    register.NormalName,
                    register.EcoCost,
                    NConvert.ToInt32(register.IsAccount).ToString(),
                    NConvert.ToInt32(register.DoctorInfo.Templet.RegLevel.IsEmergency), /*{156C449B-60A9-4536-B4FB-D00BC6F476A1}*/
                    register.Mark1,
                    register.Card.ID,
                    register.Card.CardType.ID,
                    register.InTimes.ToString(),
                    register.PatientType,
                    register.Class1Desease, //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
                    register.Class2Desease,
                    register.User01,  //{91E7755E-E0D6-405d-92F3-A0585C0C1F2C}
                    register.RealDoctorID,//{AE399953-4F87-4199-8060-EFDC16AFAAF3}
                    register.RealDoctorName,
                    register.HospitalFirstVisit,  //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33} 
                    register.RootDeptFirstVisit,  //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33} 
                    register.DoctFirstVist,        //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33} 
                    register.AssignFlag,        //分诊标识 0-未分诊 1-分诊
                    register.AssignStatus,      //分诊状态 0-未分诊 1-待诊 2-进诊 3-诊出 4-过号 
                    register.FirstSeeFlag,      //首诊标识 1-是 0-否 
                    register.PreferentialFlag,  //优先标识 1-是 0-否
                    register.SequenceNO  ,      //看诊序号 
                    dept.HospitalID,
                    dept.HospitalName //{3515892E-1541-47de-8E0B-E306798A358C}
                    );
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "插入挂号主表类别表出错![Registration.Register.Insert.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新挂号信息,作废(注销)、退号、取消作废、换科、修改患者信息
        /// </summary>
        /// <param name="status"></param>
        /// <param name="register"></param>
        /// <returns></returns>
        public int Update(EnumUpdateStatus status, FS.HISFC.Models.Registration.Register register)
        {
            if (status == EnumUpdateStatus.Cancel)
            {
                int i = this.CancelRegUnSeeDoctor(register.ID, register.CancelOper.ID, register.CancelOper.OperTime, status);
                return i;
            }
            else if (status == EnumUpdateStatus.Return)
            {
                return this.CancelReg(register.ID, register.CancelOper.ID, register.CancelOper.OperTime, status);
            }
            else if (status == EnumUpdateStatus.ChangeDept)
            {
                return this.ChangeDept(register);
            }
            else if (status == EnumUpdateStatus.PatientInfo)
            {
                return this.UpdatePatientInfoForNewClinicFee(register);//{69C503A2-4C1C-44D4-82A3-174ABDAC34C1}不能更改基本信息
                // return this.UpdatePatientInfo(register);
            }
            else if (status == EnumUpdateStatus.Uncancel)
            {
                return this.Uncancel(register.ID);
            }
            else if (status == EnumUpdateStatus.Bad)
            {
                return this.CancelReg(register.ID, register.CancelOper.ID, register.CancelOper.OperTime, status);
            }
            return 0;
        }

        /// <summary>
        /// 置已分诊标志
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="operID"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int Update(string clinicID, string operID, DateTime operDate)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateTriage", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, operID, operDate.ToString());
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "置患者分诊标志出错![Registration.Register.UpdateTriage]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 作废原有挂号记录
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="cancelID"></param>
        /// <param name="cancelDate"></param>
        /// <param name="cancelFlag"></param>
        /// <returns></returns>
        private int CancelReg(string clinicID, string cancelID, DateTime cancelDate, EnumUpdateStatus cancelFlag)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.CancelReg", ref sql) == -1) return -1;

            try
            {
                int flag = (int)cancelFlag;
                if (cancelFlag == EnumUpdateStatus.Bad)
                {
                    flag = 3;
                }
                sql = string.Format(sql, clinicID, cancelID, cancelDate.ToString(), flag);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "作废挂号记录出错![Registration.Register.CancelReg]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 作废原有挂号记录 {A2E63BDD-4FC3-488A-85AE-EC9791F820D9}
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="cancelID"></param>
        /// <param name="cancelDate"></param>
        /// <param name="cancelFlag"></param>
        /// <returns></returns>
        private int CancelRegUnSeeDoctor(string clinicID, string cancelID, DateTime cancelDate, EnumUpdateStatus cancelFlag)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.CancelRegUnSeeDoctor", ref sql) == -1) return -1;

            try
            {
                int flag = (int)cancelFlag;
                if (cancelFlag == EnumUpdateStatus.Bad)
                {
                    flag = 3;
                }
                sql = string.Format(sql, clinicID, cancelID, cancelDate.ToString(), flag);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "作废挂号记录出错![Registration.Register.CancelRegUnSeeDoctor]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// 换科(无用，暂无该需求)
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        private int ChangeDept(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.ChangeDept", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.ID, register.DoctorInfo.Templet.Dept.ID, register.DoctorInfo.Templet.Dept.Name,
                    register.DoctorInfo.SeeNO, register.DoctorInfo.Templet.Doct.ID, register.DoctorInfo.Templet.Doct.Name,
                    register.RegLvlFee.RegFee, register.RegLvlFee.ChkFee, register.RegLvlFee.OwnDigFee, register.RegLvlFee.OthFee,
                    register.OwnCost, register.PubCost, register.PayCost);

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新挂号记录出错![Registration.Register.ChangeDept]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// 取消作废(注销)
        /// </summary>
        /// <param name="clinicID"></param>
        /// <returns></returns>
        private int Uncancel(string clinicID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Uncancel", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "作废挂号记录出错![Registration.Register.Uncancel]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// 取消分诊状态
        /// </summary>
        /// <param name="clinicID"></param>
        /// <returns></returns>
        public int CancelTriage(string clinicID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.CancelTriage", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "取消挂号信息的分诊状态出错![Registration.Register.CancelTriage]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #region 更新患者信息（门诊收费）
        /// <summary>
        /// 更新患者基本信息（门诊收费）
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdatePatientInfoForClinicFee(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            #region SQL
            /* UPDATE com_patientinfo   --病人基本信息表
               SET name='{0}',   --姓名
                   birthday=to_date('{1}','yyyy-mm-dd hh24:mi:ss'),   --出生日期
                   sex_code='{2}',   --性别
                   home='{3}',   --户口或家庭所在
                   home_tel='{4}',   --家庭电话       
                   mark ='{6}',
                   inhos_source='{7}',
                   paykind_code='{8}',
                   pact_code='{9}',
                   pact_name='{10}',
                   mcard_no='{11}',
                   is_encryptname = '{12}',
                   normalname = '{13}'
             WHERE card_no = '{5}'*/
            #endregion

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.2", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.Name, register.Birthday.ToString(), register.Sex.ID,
                                        register.AddressHome, register.PhoneHome, register.PID.CardNO, register.CardType.ID,
                                        register.InSource.ID, register.Pact.PayKind.ID, register.Pact.ID, register.Pact.Name,
                                        register.SSN, FS.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt), register.NormalName);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新患者信息出错![Registration.Register.Update.PatientInfo.2]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 修改患者基本信息（门诊收费只更改结算类型）//{69C503A2-4C1C-44D4-82A3-174ABDAC34C1}更新基本信息只更改付款种类
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdatePatientInfoForNewClinicFee(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            #region SQL
            /* UPDATE com_patientinfo   --病人基本信息表
               SET name='{0}',   --姓名
                   birthday=to_date('{1}','yyyy-mm-dd hh24:mi:ss'),   --出生日期
                   sex_code='{2}',   --性别
                   home='{3}',   --户口或家庭所在
                   home_tel='{4}',   --家庭电话       
                   mark ='{6}',
                   inhos_source='{7}',
                   paykind_code='{8}',
                   pact_code='{9}',
                   pact_name='{10}',
                   mcard_no='{11}',
                   is_encryptname = '{12}',
                   normalname = '{13}'
             WHERE card_no = '{5}'*/
            #endregion

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.4", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.Pact.PayKind.ID, register.Pact.ID, register.Pact.Name,
                                        register.PID.CardNO);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新患者信息出错![Registration.Register.Update.PatientInfo.4]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新挂号表中的患者信息（门诊收费）
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdateRegInfoForClinicFee(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            #region SQL
            /* UPDATE fin_opr_register   --挂号主表
                SET name='{0}',   --姓名
                    birthday=to_date('{1}','yyyy-mm-dd hh24:mi:ss'),   --出生日期
                    sex_code='{2}',   --性别
                    address='{3}',   --地址
                    rela_phone ='{4}' --联系电话
               WHERE clinic_code='{5}' and trans_type='1'*/
            #endregion

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.3", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.Name,
                                        register.Birthday.ToString(),
                                        register.Sex.ID,
                                        register.AddressHome,
                                        register.PhoneHome,
                                        register.ID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新患者信息出错![Registration.Register.Update.PatientInfo.3]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

        }
        #endregion
        /// <summary>
        /// 更新患者基本信息
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        private int UpdatePatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            //{D944AF1A-3BDE-4d51-BBA3-EB0FE779C7FC}增加身份证号
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql,
                    register.Name,
                    register.Birthday.ToString(),
                    register.Sex.ID,
                    register.AddressHome,
                    register.PhoneHome,
                    register.PID.CardNO,
                    register.CardType.ID,
                    register.InSource.ID,
                    register.Pact.PayKind.ID,
                    register.Pact.ID,
                    register.Pact.Name,
                    register.SSN,
                    NConvert.ToInt32(register.IsEncrypt),
                    register.NormalName,
                    //{58B76445-C6F0-4492-921E-6407AAE9901A}增加备注信息更改
                    register.IDCard,
                    register.Memo
                    );
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新患者信息出错![Registration.Register.Update.PatientInfo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        #region {FCEC42B4-DF78-45c2-8D1A-EDAB94AA56DD} 分诊时修改患者基本信息

        /// <summary>
        /// 更新挂号表中的患者信息
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdateRegInfo(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.Name,
                                                        register.Birthday.ToString(),
                                                        register.Sex.ID,
                                                        register.AddressHome,
                                                        register.PhoneHome,
                    //register.CardType.ID,
                    //register.InSource.ID,
                    //register.Pact.PayKind.ID,
                    //register.Pact.PayKind.Name,
                    //register.Pact.ID, 
                    //register.Pact.Name, 
                    //register.Pact.Name,
                    //register.SSN,
                    //FS.FrameWork.Function.NConvert.ToInt32(register.IsEncrypt),
                    //register.NormalName,
                                                        register.IDCard,
                                                        register.ID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新患者信息出错![Registration.Register.Update.PatientInfo.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

        }

        /// <summary>
        /// 修改信息更新挂号表急诊标志与温度
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdateRegInfoAdd(FS.HISFC.Models.Registration.Register register)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo.2", ref sql) == -1)
            {
                sql = "UPDATE fin_opr_register   SET is_emergency='{0}',  temperature='{1}'  WHERE clinic_code='{2}' and trans_type='1'";
            }
            string isEmergency = "";
            if (register.DoctorInfo.Templet.RegLevel.IsEmergency == true)
            { isEmergency = "1"; }
            else
            { isEmergency = "0"; }
            try
            {
                sql = string.Format(sql, isEmergency,
                                                        register.Temperature,
                                                        register.ID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新患者信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

        }

        /// <summary>
        /// 分诊时更新患者基本信息
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int UpdatePatientForNurse(FS.HISFC.Models.Registration.Register register)
        {
            //{D944AF1A-3BDE-4d51-BBA3-EB0FE779C7FC}增加身份证号
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.PatientInfo", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, register.Name, register.Birthday.ToString(), register.Sex.ID,
                                        register.AddressHome, register.PhoneHome, register.PID.CardNO, register.IDCard);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新患者信息出错![Registration.Register.Update.PatientInfo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        /// <summary>
        /// 换科{87C56F02-B81A-4fac-BA4D-654C8E56C500}
        /// </summary>
        /// <param name="clinicNO">挂号流水号</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="deptName">科室名称</param>
        /// <param name="doctCode">医生编码</param>
        /// <param name="doctName">医生名称</param>
        /// <param name="dtReg">挂号时间</param>
        /// <returns></returns>
        public int UpdateDeptAndDoct(string clinicNO, string deptCode, string deptName, string doctCode, string doctName, string dtReg)
        {
            string strSql = string.Empty;
            int returnValue = this.Sql.GetCommonSql("Registration.Register.UpdateDeptAndDoct", ref  strSql);
            if (returnValue < 0)
            {
                this.Err = "没有Registration.Register.UpdateDeptAndDoct对应的sql语句";
                return -1;
            }
            strSql = string.Format(strSql, clinicNO, deptCode, deptName, doctCode, doctName, dtReg);
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region 更新

        #region 挂号更新限额
        /// <summary>
        /// 更新看诊序号
        /// </summary>
        /// <param name="Type">1医生 2科室 4全院</param>
        /// <param name="seeDate">看诊日期</param>
        /// <param name="Subject">Type=1时,医生代码;Type=2,科室代码;Type=4,ALL</param>
        /// <param name="noonID">午别</param>
        /// <returns></returns>
        public int UpdateSeeNo(string Type, DateTime seeDate, string Subject, string noonID)
        {
            string sql = "";

            #region 更新看诊序号

            if (this.Sql.GetCommonSql("Registration.Register.UpdateSeeSequence", ref sql) == -1) return -1;
            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), Type, Subject, noonID);
                int rtn = this.ExecNoQuery(sql);

                if (rtn == -1) return -1;

                //没有更新记录,插入一条新记录
                if (rtn == 0)
                {
                    if (this.Sql.GetCommonSql("Registration.Register.InsertSeeSequence", ref sql) == -1) return -1;

                    sql = string.Format(sql, seeDate.Date.ToString(), Type, Subject, "", 1, noonID);

                    if (this.ExecNoQuery(sql) == -1) return -1;
                }
            }
            catch (Exception e)
            {
                this.Err = "更新看诊序号出错" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
            #endregion
            return 0;
        }


        /// <summary>
        /// 获得患者看诊序号
        /// </summary>
        /// <param name="Type">Type:1专家序号、2科室序号、4全院序号</param>
        /// <param name="current">看诊日期</param>
        /// <param name="subject">Type=1时,医生代码;Type=2,科室代码;Type=4,ALL</param>
        /// <param name="noonID">午别</param>
        /// <param name="seeNo">当前看诊号</param>
        /// <returns></returns>
        public int GetSeeNo(string Type, DateTime current, string subject, string noonID, ref int seeNo)
        {
            string sql = "", rtn = "";

            if (this.Sql.GetCommonSql("Registration.Register.getSeeNo", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, current.Date.ToString(), Type, subject, noonID);

                rtn = this.ExecSqlReturnOne(sql, "0");

                seeNo = FS.FrameWork.Function.NConvert.ToInt32(rtn);

                return 0;
            }
            catch (Exception e)
            {
                this.Err = "查询看诊序号出错![Registration.Register.getSeeNo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        #endregion

        #region 更新日结数据
        /// <summary>
        /// 根据操作员、时间段更新日结信息
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="OperID"></param>
        /// <param name="BalanceID"></param>
        /// <returns></returns>
        public int Update(DateTime begin, DateTime end, string OperID, string BalanceID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.DayBalance", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, begin.ToString(), end.ToString(), OperID, BalanceID);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "置挂号信息日结标志出错![Registration.Register.Update.DayBalance]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #region 更新已看诊、已收费标记

        /// <summary>
        /// 更新已看诊、已收费标记
        /// </summary>
        /// <param name="Type">1医生 2科室 4全院</param>
        /// <param name="seeDate">看诊日期</param>
        /// <param name="Subject">Type=1时,医生代码;Type=2,科室代码;Type=4,ALL</param>
        /// <param name="noonID">午别</param>
        /// <returns></returns>
        public int UpdateYNSeeAndCharge(string clinicCode)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateYNFlag", ref sql) == -1)
                return -1;
            try
            {
                sql = string.Format(sql, clinicCode);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新已看诊、已收费标记" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return 0;
        }

        #endregion

        #region 更新com_patientinfo时更新挂号表

        /// <summary>
        /// 修改患者基本信息时，更新挂号部分信息 根据clinicCode
        /// </summary>
        /// <param name="patientInfo">患者基本信息实体</param>
        /// <returns></returns>
        public int UpdateRegInfoByClinicCode(FS.HISFC.Models.Registration.Register patientInfo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateRegByClinicNo", ref sql) == -1)
                return -1;
            try
            {
                sql = string.Format(sql,
                                patientInfo.ID,
                                patientInfo.Name,
                                patientInfo.Sex.ID,
                                patientInfo.Birthday,
                                patientInfo.IDCard,
                                patientInfo.Pact.PayKind.ID,
                                patientInfo.Pact.PayKind.Name,
                                patientInfo.Pact.ID,
                                patientInfo.Pact.Name
                                );
                this.ExecNoQuery(sql);
                return 1;
            }
            catch (Exception e)
            {
                this.Err = "挂号信息失败：" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 修改患者基本信息时，更新挂号相关信息
        /// </summary>
        /// <param name="patientInfo">患者基本信息实体</param>
        /// <returns></returns>
        public int UpdateRegByPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.UpdateRegByPatientInfo", ref sql) == -1)
                return -1;
            try
            {
                sql = string.Format(sql,

                                patientInfo.PID.CardNO,
                                patientInfo.Name, patientInfo.Sex.ID,
                                patientInfo.Birthday,
                                patientInfo.IDCard,
                                patientInfo.Pact.PayKind.ID,
                                patientInfo.Pact.PayKind.Name,
                                patientInfo.Pact.ID,
                                patientInfo.Pact.Name
                                );

                this.ExecNoQuery(sql);
                return 1;
            }
            catch (Exception e)
            {
                this.Err = "挂号信息失败：" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return 0;
        }

        #endregion

        #region 更新CRM的分诊信息到挂号表
        /// <summary>
        /// 更新CRM的分诊信息到挂号表
        /// </summary>        
        /// <param name="register">挂号信息</param>
        /// <returns></returns>
        public int UpdateCRMAssign(FS.HISFC.Models.Registration.Register register)
        {
            if (string.IsNullOrEmpty(register.ID))
            {
                this.Err = "参数错误";
                return -1;
            }
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.UpdateCRMAssign", ref sql) == -1)
                return -1;
            try
            {
                sql = string.Format(sql, register.ID, register.AssignFlag, register.AssignStatus, register.FirstSeeFlag, register.PreferentialFlag, register.SequenceNO);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新CRM的分诊信息失败" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #endregion

        #region 自动取卡号
        /// <summary>
        /// 取数据库序列值来作为就诊卡号
        /// </summary>
        /// <returns>序列值</returns>
        public int AutoGetCardNO()
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.GetNewCardNo", ref sql) == -1) return -1;

            try
            {
                return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
            }
            catch (Exception e)
            {
                this.Err = "自动取卡号出错![Registration.Register.GetNewCardNo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        /// <summary>
        /// 取数据库序列值来作为就诊卡号（用于自助挂号）
        /// </summary>
        /// <returns>序列值</returns>
        public long AutoGetCardNOForSelfHelpReg()
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.GetNewCardNo.SelfHelpReg", ref sql) == -1) return -1;

            try
            {
                return Convert.ToInt64(this.ExecSqlReturnOne(sql));
            }
            catch (Exception e)
            {
                this.Err = "自动取卡号出错![Registration.Register.GetNewCardNo.SelfHelpReg]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #region 诊间使用
        #region 更新已经看诊

        /// <summary>
        ///  更新已经看诊－－根据门诊流水号
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public int UpdateSeeDone(string clinicNo)
        {
            string sql = "Registration.Register.Update.SeeDone";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            return this.ExecNoQuery(sql, clinicNo);
        }

        #endregion

        #region 更新看诊科室
        /// <summary>
        /// 更新看诊科室
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="seeDeptID"></param>
        /// <param name="seeDoctID"></param>
        /// <returns></returns>
        public int UpdateDept(string clinicID, string seeDeptID, string seeDoctID)
        {
            string sql = "";
            string[] parm = new string[] { clinicID, seeDeptID, seeDoctID };

            if (this.Sql.GetCommonSql("Registration.Register.Query.17", ref sql) == -1) return -1;

            return this.ExecNoQuery(sql, parm);
        }
        #endregion



        #endregion


        #region 按病历号查询一条最近的挂号信息,屏蔽

        /// <summary>
        /// 根据病历号查询患者最近一次挂号信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Register Query(string cardNo)
        {
            ArrayList al = this.QueryRegListBase("Registration.Register.Query.2", cardNo);

            if (al == null)
            {
                return null;
            }
            else if (al.Count == 0)
            {
                return new FS.HISFC.Models.Registration.Register();
            }
            else
            {
                return (FS.HISFC.Models.Registration.Register)al[0];
            }
        }

        #endregion

        #region 按患者姓名从挂号表查询患者信息
        public ArrayList QueryRegisterByName(string name)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryByName", ref sql) == -1) return null;

            sql = string.Format(sql, name);

            if (this.ExecQuery(sql) == -1) return null;

            ArrayList al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    this.reg = new FS.HISFC.Models.Registration.Register();

                    reg.PID.CardNO = this.Reader[0].ToString();
                    reg.Name = this.Reader[1].ToString();
                    reg.IDCard = this.Reader[2].ToString();
                    reg.Sex.ID = this.Reader[3].ToString();
                    reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());
                    reg.PhoneHome = this.Reader[5].ToString();
                    reg.AddressHome = this.Reader[6].ToString();
                    reg.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());

                    al.Add(reg);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "检索患者基本信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return al;

        }
        #endregion
        #region 按患者名称查询患者基本信息
        /// <summary>
        /// 根据患者姓名查询
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ArrayList QueryByName(string Name)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.10", ref sql) == -1) return null;

            sql = string.Format(sql, Name);

            if (this.ExecQuery(sql) == -1) return null;

            ArrayList al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    this.reg = new FS.HISFC.Models.Registration.Register();

                    reg.PID.CardNO = this.Reader[0].ToString();
                    reg.Name = this.Reader[1].ToString();
                    reg.IDCard = this.Reader[2].ToString();
                    reg.Sex.ID = this.Reader[3].ToString();
                    reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());
                    reg.PhoneHome = this.Reader[5].ToString();
                    reg.AddressHome = this.Reader[6].ToString();

                    al.Add(reg);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "检索患者基本信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return al;
        }
        #endregion

        public ArrayList GetByIDCard(string IDCard)
        {
            return this.QueryRegListBase("Registration.Register.Query.IDCard", IDCard);
        }

        /// <summary>
        /// 根据病历号 午别 医生 部门 挂号时间 看是否已经挂过号D8F6425B-1CFD-4b3f-921E-03B1ECA0F95E
        /// </summary>
        /// <param name="card_no"></param>
        /// <param name="noon"></param>
        /// <param name="doctorId"></param>
        /// <param name="depteCode"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public bool QueryRegByNoonDoctor(string card_no, string noon, string doctorId, string depteCode, DateTime regDate)
        {
            string[] arr = new string[] { card_no, noon, doctorId, depteCode, regDate.ToString("yyyy-MM-dd") };
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.QueryRegByNoodDoctor", ref sql) == -1)
            {
                return false;
            }
            sql = string.Format(sql, arr);
            int result = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
            return result > 0;
        }

        #region 按门诊号查询一条挂号信息
        /// <summary>
        /// 按门诊流水号查询挂号信息
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Register GetByClinic(string clinicNo)
        {
            ArrayList al = this.QueryRegListBase("Registration.Register.Query.4", clinicNo);

            if (al == null)
            {
                return null;
            }
            else if (al.Count == 0)
            {
                return new FS.HISFC.Models.Registration.Register();
            }
            else
            {
                return (FS.HISFC.Models.Registration.Register)al[0];
            }
        }

        #endregion

        #region 按处方号查询一条挂号信息
        /// <summary>
        /// 按处方号查询
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <returns></returns>
        public ArrayList QueryByRecipe(string recipeNo)
        {
            string sql = "", where = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.14", ref where) == -1) return null;

            try
            {
                where = string.Format(where, recipeNo);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.14]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }
        #endregion

        //{B6E76F4C-1D79-4fa2-ABAD-4A22DE89A6F7}
        #region 根据发票号查询挂号信息
        /// <summary>
        /// 根据发票号查询挂号信息
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <returns></returns>
        public ArrayList QueryByRegInvoice(string invoiceNo)
        {
            string sql = "", where = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.22", ref where) == -1) return null;

            try
            {
                where = string.Format(where, invoiceNo);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.22]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }

        /// <summary>
        /// add by lijp 2012-08-24
        /// 根据发票号查询一段时间内患者的有效挂号信息
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <returns></returns>
        public ArrayList QueryByRegInvoice(string invoiceNo, DateTime limitDate)
        {
            string sql = "", where = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.24", ref where) == -1)
            {
                this.Err = "SQL语句没有找到：Registration.Register.Query.24";
                return null;
            }

            try
            {
                where = string.Format(where, invoiceNo, limitDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.24]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }

        #endregion

        #region 按照病历号，医保类别（大类），时间有效查询挂号信息
        /// <summary>
        ///  按照病历号，医保类别（大类），时间有效查询挂号信息{46F865E4-9B79-4cc6-814D-3847DDBC85F9}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="beginDateTime"></param>
        /// <param name="EndDateTime"></param>
        /// <param name="payKindCode"></param>
        /// <returns></returns>
        public ArrayList QueryRegInfo(string cardNO, string beginDateTime, string EndDateTime, string payKindCode)
        {
            string sql = "", where = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.23", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNO, beginDateTime, EndDateTime, payKindCode);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.23]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }
        #endregion

        #region 按病历号、开始时间查询患者的挂号信息

        public ArrayList QueryRegListBase(string whereSQL)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1)
            {
                return null;
            }

            sql = sql + "\r\n" + whereSQL;

            return this.QueryRegister(sql);
        }

        private ArrayList QueryRegListBase(string whereSQLIndex, params string[] args)
        {

            string where = "";

            if (this.Sql.GetCommonSql(whereSQLIndex, ref where) == -1)
            {
                return null;
            }

            try
            {
                where = string.Format(where, args);
            }
            catch (Exception e)
            {
                this.Err = "[" + whereSQLIndex + "]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return QueryRegListBase(where);
        }

        /// <summary>
        /// 按照病历号查询一段时间内的挂号记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">截止时间</param>
        /// <param name="valide">是否有效 1 有效；0 退费；2 作废； 其他 全部记录</param>
        /// <returns></returns>
        public ArrayList QueryRegList(string cardNo, DateTime beginDate, DateTime endDate, string valide)
        {
            if (valide != "1" && valide != "0" && valide != "2")
            {
                valide = "All";
            }

            return this.QueryRegListBase("Registration.Register.Query.ByDateAndState", cardNo, beginDate.ToString(), endDate.ToString(), valide);
        }

        /// <summary>
        /// 查询患者一段时间内挂的有效号
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList Query(string cardNo, DateTime limitDate)
        {
            return this.QueryRegListBase("Registration.Register.Query.3", cardNo, limitDate.ToString());
        }

        /// <summary>
        /// 查询患者一段时间内挂的有效号
        /// </summary>
        /// <param name="name"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryName(string name, DateTime limitDate)
        {
            return this.QueryRegListBase("Registration.Register.Query.25", name, limitDate.ToString());
        }

        ///查询当天是否存在一条同医生同科室的挂号信息 {A2E63BDD-4FC3-488A-85AE-EC9791F820D9}
        public bool ExixtRegList(string cardNo, string deptcode, string doccode)
        {
            ArrayList list = this.QueryRegByDeptDocAndDay( cardNo,  deptcode,  doccode);//("Registration.Register.Query.ByDeptDocAndDay", cardNo, deptcode, doccode);//Registration.Register.Query.ByDeptDocAndDay
            if (list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //{A2E63BDD-4FC3-488A-85AE-EC9791F820D9}
        public ArrayList QueryRegByDeptDocAndDay(string cardNo, string deptcode, string doccode)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.ByDeptDocAndDay", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNo, deptcode,doccode);
            }
            catch (Exception e)
            {
                this.Err = "Registration.Register.Query.ByDeptDocAndDay" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryUnionNurse(string cardNo, DateTime limitDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.20", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNo, limitDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.20]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        /// <summary>
        /// 查询一段时间内作废挂号信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryCancel(string cardNo, DateTime limitDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.16", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNo, limitDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.16]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        /// <summary>
        /// 根据病历号查询已看诊的有效挂号信息
        /// </summary>
        /// <param name="cardNO">病历号</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结算时间</param>
        public ArrayList GetRegisterByCardNODate(string cardNO, DateTime beginDate, DateTime endDate)
        {
            //Registration.Register.Query.Where
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.Where", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNO, beginDate.ToString(), endDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.Where]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        #region 按患者费用执行科室查询挂号信息
        /// <summary>
        /// 按患者费用执行科室查询挂号信息
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByFeeExcuDept(string excuDeptID, string beginDate, string endDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByFeeExcuDept", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, excuDeptID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByFeeExcuDept]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        #region 按患者费用执行科室查询挂号信息--按挂号时间
        /// <summary>
        /// 按患者费用执行科室查询挂号信息
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByFeeExcuDeptOrderByRegDate(string excuDeptID, string beginDate, string endDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByFeeExcuDeptOrderByRegDate", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, excuDeptID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByFeeExcuDept]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        /// <summary>
        /// 按患者费用最小费用挂号信息
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByMinFeeOrderByRegDate(string minFee, string beginDate, string endDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByMinFeeOrderByRegDate", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, minFee);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByMinFeeOrderByRegDate]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        #region 按患者费用执行科室和卡号查询挂号信息
        /// <summary>
        /// 按患者费用执行科室和卡号查询挂号信息
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByFeeExcuDeptAndCardNo(string excuDeptID, string beginDate, string endDate, string CardNo)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByFeeExcuDeptAndCardNo", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, excuDeptID, CardNo);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByFeeExcuDeptAndCardNo]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 按患者费用执行科室和卡号查询挂号信息--按挂号时间
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByFeeExcuDeptAndCardNoOrderByRegDate(string excuDeptID, string beginDate, string endDate, string CardNo)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByFeeExcuDeptAndCardNoOrderByRegDate", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, excuDeptID, CardNo);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByFeeExcuDeptAndCardNoOrderByFeeDate]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 按患者最小费用和卡号查询挂号信息--按挂号时间
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByMinFeeAndCardNoOrderByRegDate(string excuDeptID, string beginDate, string endDate, string CardNo)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.QueryRegisterByMinFeeAndCardNoOrderByRegDate", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate, endDate, excuDeptID, CardNo);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.QueryRegisterByMinFeeAndCardNoOrderByRegDate]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        #endregion



        #region 按看诊序号查询患者挂号信息 门诊收费使用
        /// <summary>
        /// 按看诊序号、开始时间查询挂号信息
        /// </summary>
        /// <param name="seeNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeNo(string seeNo, DateTime limitDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.18", ref where) == -1) return null;

            try
            {
                where = string.Format(where, seeNo, limitDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.18]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        /// <summary>
        /// 检验是否院内职工，根据身份证号码
        /// </summary>
        /// <param name="IdenNO">身份者号码</param>
        /// <returns></returns>
        public bool CheckIsEmployee(string IdenNO)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Registration.Register.CheckIsEmployee", ref sql) == -1)
            {
                this.Err += "没有找到索引为:Registration.Register.CheckIsEmployee 的SQL语句";
                return false;
            }
            try
            {
                sql = string.Format(sql, IdenNO);
            }
            catch (Exception e)
            {
                this.Err = "查找sql语句失败[Registration.Register.CheckIsEmployee]" + e.Message;
                this.ErrCode = e.Message;
                return false;
            }

            int count = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));

            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检验是否院内职工，根据身份证号码
        /// </summary>
        /// <param name="IdenNO">身份者号码</param>
        /// <returns></returns>
        public bool CheckIsEmployee(FS.HISFC.Models.Registration.Register register)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Registration.Register.CheckIsEmployeeByClinicNO", ref sql) == -1)
            {
                return this.CheckIsEmployee(register.IDCard);
            }
            try
            {
                sql = string.Format(sql, register.ID);
            }
            catch (Exception e)
            {
                this.Err = "查找sql语句失败[Registration.Register.CheckIsEmployeeByClinicNO]" + e.Message;
                this.ErrCode = e.Message;
                return false;
            }

            int count = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));

            if (count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 按时间段统计查询挂号员的有效挂号数
        /// </summary>
        /// <param name="operID">挂号员id</param>
        /// <param name="beginDateTime">起始时间</param>
        /// <param name="endDateTime">截至时间</param>
        /// <returns></returns>
        public string QueryValidRegNumByOperAndOperDT(string operID, string beginDateTime, string endDateTime)
        {
            string sql = string.Empty;
            if (this.Sql.GetCommonSql("Registration.QueryValidRegNumByOperAndOperDT.Select1", ref sql) == -1)
            {
                this.Err += "没有找到索引为:Registration.QueryValidRegNumByOperAndOperDT.Select1 的SQL语句";
                return "-1";
            }
            try
            {
                sql = string.Format(sql, operID, beginDateTime, endDateTime);
            }
            catch (Exception e)
            {
                this.Err = "组成sql语句失败[Registration.QueryValidRegNumByOperAndOperDT.Select1]" + e.Message;
                this.ErrCode = e.Message;
            }

            return this.ExecSqlReturnOne(sql);
        }

        #region 按操作员、时间段查询挂号信息
        /// <summary>
        /// 按操作员、时间段查询挂号信息
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="operID"></param>
        /// <returns></returns>
        public ArrayList Query(DateTime beginDate, DateTime endDate, string operID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.9", ref where) == -1) return null;

            try
            {
                where = string.Format(where, beginDate.ToString(), endDate.ToString(), operID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.9]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        /// <summary>
        /// 查询复诊记录
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public int QueryRegiterByCardNO(string cardNO)
        {
            string sql = string.Empty;
            int returnValue = Sql.GetCommonSql("Registration.QueryRegiterByCardNO.Select.1", ref sql);
            if (returnValue == -1)
            {
                return -1;
            }
            try
            {
                sql = string.Format(sql, cardNO);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.QueryRegiterByCardNO.Select.1]出错" + e.Message;
                return -1;

            }


            int result = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));

            return result;
        }


        #region 四级初诊判断
        //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33} 

        /// <summary>
        /// 判断初诊
        /// </summary>
        /// <param name="type">初诊类型 1-院级 2-大科级 3-科级 4-医生</param>
        /// <param name="cardNO"></param>
        /// <param name="dept"></param>
        /// <param name="doct"></param>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        public int IsFirstRegister(string type, string cardNO, string dept, string doct, DateTime beginTime)
        {
            int ret = 0;
            string sql = string.Empty;

            switch (type)
            {
                case "1"://院级
                    ret = Sql.GetCommonSql("Registration.QueryRegiterByCardNODeptTime.Select.2", ref sql);
                    break;
                case "2"://大科级
                    ret = Sql.GetCommonSql("Registration.QueryRegiterByCardNODeptTime.Select.3", ref sql);
                    break;
                case "3"://科级
                    ret = Sql.GetCommonSql("Registration.QueryRegiterByCardNODeptTime.Select.1", ref sql);
                    break;
                case "4"://医生级
                    ret = Sql.GetCommonSql("Registration.QueryRegiterByCardNODeptTime.Select.4", ref sql);
                    break;
                default:
                    ret = -1;
                    break;
            }

            if (ret == -1)
            {
                return ret;
            }

            try
            {
                if (type == "1")
                {
                    sql = string.Format(sql, cardNO, beginTime.ToString());
                }
                else if (type == "4")
                {
                    sql = string.Format(sql, cardNO, doct, beginTime.ToString());
                }
                else  //大科级和科级
                {
                    sql = string.Format(sql, cardNO, dept, beginTime.ToString());
                }
            }
            catch (Exception e)
            {
                this.Err = "获取初诊判断SQL语句出错" + e.Message;
                return -1;
            }

            int result = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));

            return result;
        }

        /// <summary>
        /// {496701C2-CCAE-4a8d-B3DB-7D528CFF7025}
        /// 根据卡号时间科室查询挂号数量
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="Dept"></param>
        /// <param name="BeginTime"></param>
        /// <returns></returns>
        public int QueryRegisterByCardNOTimeDept(string cardNO, string Dept, DateTime BeginTime)
        {
            string sql = string.Empty;
            int returnValue = Sql.GetCommonSql("Registration.QueryRegiterByCardNODeptTime.Select.1", ref sql);
            if (returnValue == -1)
            {
                return -1;
            }
            try
            {
                sql = string.Format(sql, cardNO, Dept, BeginTime.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.QueryRegiterByCardNODeptTime.Select.1]出错" + e.Message;
                return -1;

            }
            int result = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
            return result;
        }


        /// <summary>
        /// {2888444F-50BA-4956-A5F7-D71F0C6448BB}
        /// 根据卡号时间医生查询挂号数量
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="Dept"></param>
        /// <param name="BeginTime"></param>
        /// <returns></returns>
        public int QueryRegisterByCardNODoctTime(string cardNO, string deptID, string Doct, DateTime BeginTime)
        {
            string sql = string.Empty;
            int returnValue = Sql.GetCommonSql("Registration.QueryRegiterByCardNODoctTime.Select.1", ref sql);
            if (returnValue == -1)
            {
                return -1;
            }
            try
            {
                sql = string.Format(sql, cardNO, deptID, Doct, BeginTime.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.QueryRegiterByCardNODoctTime.Select.1]出错" + e.Message;
                return -1;

            }
            int result = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
            return result;
        }

        #endregion

        #region 查询一段时间内未分诊的挂号患者 门诊护士使用
        /// <summary>
        /// 查询一段时间内未分诊的挂号患者
        /// </summary>
        /// <param name="begin"></param>
        /// <returns></returns>
        public ArrayList QueryNoTriage(DateTime begin)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.5", ref where) == -1) return null;

            try
            {
                where = string.Format(where, begin.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.5]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }
        #endregion

        #region 分诊
        /// <summary>
        /// 通过一段时间内 某护理站对应科室的挂号患者 addby sunxh
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="myNurseDept">护理站代码</param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyDept(DateTime begin, string myNurseDept)
        {

            string sql = ""; string where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byNurseDept", ref where) == -1) return null;

            where = string.Format(where, begin.ToString(), myNurseDept);

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 通过一段时间内 某护理站的挂号患者{F044FCF3-6736-4aaa-AA04-4088BB194C20}
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="myNurseDept">护理站代码</param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyNurse(DateTime begin, string NurseID)
        {
            string sql = ""; string where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byNurseID", ref where) == -1) return null;

            where = string.Format(where, begin.ToString(), NurseID);

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 通过一段时间内 某护理站对应科室的挂号患者未看诊 addby niuxy
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="myNurseDept">护理站代码</param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyDeptUnSee(DateTime begin, string myNurseDept)
        {
            string sql = ""; string where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byNurseDept1", ref where) == -1) return null;

            where = string.Format(where, begin.ToString(), myNurseDept);

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 根据门诊号判断挂号信息是否分诊
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public bool QueryIsTriage(string clinicNo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.IsTriage", ref sql) == -1) return false;

            try
            {
                sql = string.Format(sql, clinicNo);

                string rtn = this.ExecSqlReturnOne(sql, "0");

                // return FS.FrameWork.Function.NConvert.ToBoolean(rtn) ;
                if (rtn == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.IsTriage]" + e.Message;
                this.ErrCode = e.Message;
                return false;
            }
        }

        /// <summary>
        /// 根据门诊号判断挂号信息是否作废
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public bool QueryIsCancel(string clinicNo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.IsCancel", ref sql) == -1) return false;

            try
            {
                sql = string.Format(sql, clinicNo);

                string rtn = this.ExecSqlReturnOne(sql, "0");

                if (rtn == "1")
                {
                    return false;//有效,未作废
                }
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.IsCancel]" + e.Message;
                this.ErrCode = e.Message;
                return false;
            }
        }

        /// <summary>
        /// 查询患者有效挂号记录
        /// 不包括进诊和诊出状态
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList QueryUnionNurseTriage(string cardNo, DateTime limitDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.ByInTriage", ref where) == -1) return null;

            try
            {
                where = string.Format(where, cardNo, limitDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.ByInTriage]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 获得患者看诊序号
        /// </summary>
        /// <param name="Type">Type:1专家序号、2科室序号、4全院序号</param>
        /// <param name="current">看诊日期</param>
        /// <param name="subject">Type=1时,医生代码;Type=2,科室代码;Type=4,ALL</param>
        /// <param name="noonID">午别</param>
        /// <param name="seeNo">当前看诊号</param>
        /// <returns></returns>
        public int GetSeeNo(string Type, DateTime current, string subject, string noonID, ref string seeNo)
        {
            string sql = "", rtn = "";

            if (this.Sql.GetCommonSql("Registration.Register.getSeeNo", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, current.Date.ToString(), Type, subject, noonID);

                rtn = this.ExecSqlReturnOne(sql, "0");

                seeNo = rtn;

                return 0;
            }
            catch (Exception e)
            {
                this.Err = "查询看诊序号出错![Registration.Register.getSeeNo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        #endregion

        #region 查询公费患者某日挂号数量
        /// <summary>
        /// 查询公费患者某日挂号数量
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="regDate"></param>
        /// <returns></returns>
        public int QuerySeeNum(string cardNo, DateTime regDate)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.12", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, cardNo, regDate.Date.ToString(), regDate.Date.AddDays(1).ToString());
                string Cnt = this.ExecSqlReturnOne(sql, "0");

                return FS.FrameWork.Function.NConvert.ToInt32(Cnt);
            }
            catch (Exception e)
            {
                this.Err = "获得患者挂号数量出错![Registration.Register.Query.12]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #region 按门诊号查询已打印发票数量
        /// <summary>
        /// 按门诊号查询已打印发票数量
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public int QueryPrintedInvoiceCnt(string clinicNo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.15", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicNo);
                string Cnt = this.ExecSqlReturnOne(sql, "0");

                return FS.FrameWork.Function.NConvert.ToInt32(Cnt);
            }
            catch (Exception e)
            {
                this.Err = "获得患者打印发票数量出错![Registration.Register.Query.15]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// 按门诊号更新已打印发票数量
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public int UpdatePrintInvoiceCnt(string clinicNo)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Update.InvoiceCnt", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicNo);

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "更新患者打印发票数量出错![Registration.Register.Update.InvoiceCnt]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }
        #endregion

        #region 共有查询

        /// <summary>
        /// 挂号查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList QueryRegister(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;

            ArrayList al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    this.reg = new FS.HISFC.Models.Registration.Register();

                    this.reg.ID = this.Reader[0].ToString();//序号
                    this.reg.PID.CardNO = this.Reader[1].ToString();//病历号
                    this.reg.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[2].ToString());//挂号日期
                    this.reg.DoctorInfo.Templet.Noon.ID = this.Reader[3].ToString();
                    this.reg.Name = this.Reader[4].ToString();
                    this.reg.IDCard = this.Reader[5].ToString();
                    this.reg.Sex.ID = this.Reader[6].ToString();

                    this.reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());//出生日期

                    this.reg.Pact.PayKind.ID = this.Reader[8].ToString();//结算类别
                    this.reg.Pact.PayKind.Name = this.Reader[9].ToString();

                    this.reg.Pact.ID = this.Reader[10].ToString();//合同单位
                    this.reg.Pact.Name = this.Reader[11].ToString();
                    this.reg.SSN = this.Reader[12].ToString();
                    this.reg.SIMainInfo.RegNo = this.reg.SSN;

                    this.reg.DoctorInfo.Templet.RegLevel.ID = this.Reader[13].ToString();//挂号级别
                    this.reg.DoctorInfo.Templet.RegLevel.Name = this.Reader[14].ToString();

                    this.reg.DoctorInfo.Templet.Dept.ID = this.Reader[15].ToString();//挂号科室
                    this.reg.DoctorInfo.Templet.Dept.Name = this.Reader[16].ToString();

                    this.reg.DoctorInfo.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[17].ToString());

                    this.reg.DoctorInfo.Templet.Doct.ID = this.Reader[18].ToString();//看诊医生
                    this.reg.DoctorInfo.Templet.Doct.Name = this.Reader[19].ToString();

                    this.reg.RegType = (FS.HISFC.Models.Base.EnumRegType)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[20].ToString());
                    this.reg.IsFirst = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[21].ToString());

                    this.reg.RegLvlFee.RegFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22].ToString());
                    this.reg.RegLvlFee.ChkFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23].ToString());
                    this.reg.RegLvlFee.OwnDigFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24].ToString());
                    this.reg.RegLvlFee.OthFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25].ToString());

                    this.reg.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString());
                    this.reg.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString());
                    this.reg.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[28].ToString());

                    this.reg.Status = (FS.HISFC.Models.Base.EnumRegisterStatus)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());

                    this.reg.InputOper.ID = this.Reader[30].ToString();
                    this.reg.IsSee = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[31].ToString());
                    this.reg.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[32].ToString());
                    this.reg.TranType = (FS.HISFC.Models.Base.TransTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[33].ToString());
                    this.reg.BalanceOperStat.IsCheck = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[34]);//日结
                    this.reg.BalanceOperStat.CheckNO = this.Reader[35].ToString();
                    this.reg.BalanceOperStat.Oper.ID = this.Reader[36].ToString();

                    if (!this.Reader.IsDBNull(37))
                        this.reg.BalanceOperStat.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[37].ToString());

                    this.reg.PhoneHome = this.Reader[38].ToString();//联系电话
                    this.reg.AddressHome = this.Reader[39].ToString();//地址
                    this.reg.IsFee = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[40].ToString());
                    //作废人信息
                    this.reg.CancelOper.ID = this.Reader[41].ToString();
                    this.reg.CancelOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[42].ToString());
                    this.reg.CardType.ID = this.Reader[43].ToString();//证件类型
                    this.reg.DoctorInfo.Templet.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[44].ToString());
                    this.reg.DoctorInfo.Templet.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[45].ToString());
                    //this.reg.InvoiceNo = this.Reader[50].ToString() ;
                    //this.reg.InvoiceNO = this.Reader[51].ToString() ; by niuxinyuan
                    this.reg.InvoiceNO = this.Reader[50].ToString();
                    this.reg.RecipeNO = this.Reader[51].ToString();

                    this.reg.DoctorInfo.Templet.IsAppend = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[52].ToString());
                    this.reg.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[53].ToString());
                    this.reg.DoctorInfo.Templet.ID = this.Reader[54].ToString();
                    this.reg.InSource.ID = this.Reader[55].ToString();
                    this.reg.PVisit.InState.ID = this.Reader[56].ToString();
                    this.reg.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[57].ToString());
                    this.reg.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[58].ToString());
                    this.reg.PVisit.ZG.ID = this.Reader[59].ToString();
                    this.reg.PVisit.PatientLocation.Bed.ID = this.Reader[60].ToString();

                    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                    //标识是否是账户流程挂号 1代表是
                    this.reg.IsAccount = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[61].ToString());

                    //{E26C3EE9-D480-421e-9FD3-7094D8E4E1D0}
                    this.reg.SeeDoct.Dept.ID = this.Reader[62].ToString(); //看诊科室
                    this.reg.SeeDoct.ID = this.Reader[63].ToString();//看诊医生
                    //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                    this.reg.DoctorInfo.Templet.RegLevel.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[64].ToString());
                    //{921FBFCA-3D0D-4bc6-8EEA-A9BBE152E69A}
                    this.reg.Mark1 = this.Reader[65].ToString();
                    // this.reg.PID.CaseNO =this.q;

                    // {531B6C65-1DF5-4f16-94EC-F7D87287966F}
                    this.reg.SeeDoct.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[46].ToString());
                    //患者是否已经分诊
                    this.reg.IsTriage = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[47].ToString());
                    //{4AC12996-BC4B-4272-9FA4-E06DB8326330}
                    if (this.Reader.FieldCount >= 67)
                    {
                        this.reg.NormalName = this.Reader[66].ToString();

                    }
                    if (this.Reader.FieldCount > 67)
                    {
                        this.reg.Card.ID = this.Reader[67].ToString();
                        this.reg.Card.CardType.ID = this.Reader[68].ToString();
                        this.reg.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[69].ToString());
                    }
                    if (this.Reader.FieldCount > 70)
                    {
                        this.reg.Temperature = this.Reader[70].ToString();
                    }
                    if (Reader.FieldCount > 71)
                    {
                        reg.PatientType = Reader[71].ToString();
                    }

                    if (Reader.FieldCount > 72)//实付金额// {F53BD032-1D92-4447-8E20-6C38033AA607}
                    {
                        reg.EcoCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[72].ToString());
                    }

                    if (Reader.FieldCount > 73)//一级病种  //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
                    {
                        reg.Class1Desease = this.Reader[73].ToString();
                    }

                    if (Reader.FieldCount > 74)//二级病种  //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
                    {
                        reg.Class2Desease = this.Reader[74].ToString();
                    }

                    if (Reader.FieldCount > 75)//直接收费标记 {91E7755E-E0D6-405d-92F3-A0585C0C1F2C}
                    {
                        reg.User01 = this.Reader[75].ToString();
                    }
                    if (Reader.FieldCount > 76)//分诊标识 0-未分诊 1-分诊
                    {
                        reg.AssignFlag = this.Reader[76].ToString();
                    }
                    if (Reader.FieldCount > 77)//分诊状态 0-未分诊 1-待诊 2-进诊 3-诊出 4-过号
                    {
                        reg.AssignStatus = this.Reader[77].ToString();
                    }
                    if (Reader.FieldCount > 78)//首诊标识1-是 0-否
                    {
                        reg.FirstSeeFlag = this.Reader[78].ToString();
                    }
                    if (Reader.FieldCount > 79)//优先标识1-是 0-否
                    {
                        reg.PreferentialFlag = this.Reader[79].ToString();
                    }
                    if (Reader.FieldCount > 80)//看诊序号
                    {
                        reg.SequenceNO = NConvert.ToInt32(this.Reader[80].ToString());
                    }
                    if (Reader.FieldCount > 81)//看诊序号
                    {
                        reg.Hospital_id = this.Reader[81].ToString();
                    }
                    if (Reader.FieldCount > 82)//看诊序号
                    {
                        reg.Hospital_name = this.Reader[82].ToString();
                    }
                    al.Add(this.reg);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "检索挂号信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }

        /// <summary>
        /// 查询医保上传日志表中的合同单位代码
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public string GetPactCodeFoMedcare(string clinicCode)
        {
            string defaultsql = "select pact_code from fin_ipr_sirecord where clinic_code='{0}'";
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.GetPactCodeFoMedcare.1", ref sql) == -1)
            {
                sql = defaultsql;
            }
            try
            {
                sql = string.Format(sql, clinicCode);
            }
            catch (Exception e)
            {
                this.Err = "Registration.Register.GetPactCodeFoMedcare.1" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            if (this.ExecQuery(sql) == -1) return null;
            return ExecSqlReturnOne(sql);
        }
        #endregion

        #region 门诊医生站使用查询

        /// <summary>
        /// 按挂号医生查询某一段时间内挂的有效号
        /// </summary>
        /// <param name="doctID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryByDoct(string doctID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.7", ref where) == -1) return null;

            try
            {
                where = string.Format(where, doctID, beginDate.ToString(), endDate.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isSee));
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.7]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 按挂号科室查询某一段时间内挂的有效号
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryByDept(string deptID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.8", ref where) == -1) return null;

            try
            {
                where = string.Format(where, deptID, beginDate.ToString(), endDate.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isSee));
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.8]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 按看诊医生查询某一段时间内挂的有效号
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDoc(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.19", ref where) == -1) return null;

            try
            {
                where = string.Format(where, docID, beginDate.ToString(), endDate.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isSee));
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.19]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 按看诊医生查询某一段时间内已经看诊的有效号{A448C42B-AEA2-4a36-889C-C5AB97C38A6B}
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDocAndSeeDate(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.21", ref where) == -1) return null;

            try
            {
                where = string.Format(where, docID, beginDate.ToString(), endDate.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isSee));
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.21]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 按拟手术时间一段时间内已经看诊的有效号
        /// </summary>
        /// <param name="docID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDocAndSeeDate2(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.opsApply", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.opsApplyWhere", ref where) == -1) return null;

            try
            {
                where = string.Format(where, docID, beginDate.ToString(), endDate.ToString(), FS.FrameWork.Function.NConvert.ToInt32(isSee));
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.21]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        #endregion

        #region
        /// <summary>
        /// 查询注射室患者信息
        /// </summary>
        /// <param name="cardNo">卡号，为空时表示查询全部</param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="isPrint">是否打印</param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryInject(string cardNo, DateTime beginTime, DateTime endTime, bool isPrint, string dept, string unDrugUsage, string drugUsage, ref System.Data.DataSet ds)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.Inject", ref sql) == -1)
            {
                ds = null;
                return -1;
            }

            try
            {
                if (isPrint)
                {
                    //已打印的限定  999999>打印次数>=1
                    sql = string.Format(sql, beginTime.ToString(), endTime.ToString(), cardNo.Trim(), 1, 9999999, dept, unDrugUsage, drugUsage);
                }
                else
                {
                    //未打印的限定  1>打印次数>=0
                    sql = string.Format(sql, beginTime.ToString(), endTime.ToString(), cardNo.Trim(), 0, 1, dept, unDrugUsage, drugUsage);
                }
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Register.Query.Inject]" + e.Message;
                this.ErrCode = e.Message;
                ds = null;
                return -1;
            }

            return this.ExecQuery(sql, ref ds);
        }

        #endregion



        #region 按照姓名查询具有划价信息的患者
        /// <summary>
        /// 按照姓名查询具有划价信息的患者
        /// </summary>
        /// <param name="name" >姓名</param>
        /// <param name="days ">有效天数</param>
        /// <returns></returns>
        public ArrayList QueryRegHaveChargedInfo(string name, int days)
        {
            string strSql = "";

            ArrayList al = new ArrayList();

            if (this.Sql.GetCommonSql("Registration.Register.Query.HaveChargedInfo", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Registration.Register.Query.HaveChargedInfo";
                return null;
            }
            strSql = System.String.Format(strSql, name, days);
            if (this.ExecQuery(strSql) < 0)
            {
                this.Err = "Execute Err;";
                return null;
            }

            while (this.Reader.Read())
            {
                this.reg = new FS.HISFC.Models.Registration.Register();

                reg.ID = this.Reader[0].ToString();//流水号
                reg.PID.CardNO = this.Reader[1].ToString();//病利号
                reg.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());//方号
                reg.Name = this.Reader[3].ToString();//姓名
                reg.DoctorInfo.Templet.Dept.ID = this.Reader[4].ToString();
                reg.DoctorInfo.Templet.Dept.Name = this.Reader[5].ToString();//挂号科室
                reg.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
                reg.Sex.ID = this.Reader[7].ToString();
                reg.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());//出生日期
                reg.Pact.ID = this.Reader[9].ToString();
                reg.Pact.Name = this.Reader[10].ToString();//合同单位
                reg.DoctorInfo.Templet.Doct.ID = this.Reader[11].ToString();
                reg.DoctorInfo.Templet.Doct.Name = this.Reader[12].ToString();//挂号医生
                reg.SSN = this.Reader[13].ToString();//医疗证号
                reg.DoctorInfo.Templet.RegLevel.ID = this.Reader[14].ToString();
                reg.DoctorInfo.Templet.RegLevel.Name = this.Reader[15].ToString();

                al.Add(reg);
            }
            this.Reader.Close();
            return al;
        }
        #endregion


        #region 按护士站和急诊留观状态查询患者列表
        /// <summary>
        /// 按护士站和急诊留观状态查询患者列表
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByNurseCell(string nurseCellCode, string status)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byNurseCellCode", ref where) == -1) return null;

            where = string.Format(where, nurseCellCode, status);

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        /// <summary>
        /// 医生站加载留观患者信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByNurseCell(string deptCode)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.QueryEnEmergencyPatient.byDeptCode", ref where) == -1) return null;

            where = string.Format(where, deptCode);

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }


        //{FC2B9551-0246-4375-8667-8EFF39A5CC6C}
        /// <summary>
        /// 加载患者信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByNameOrPhone(string code)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.QueryEnEmergencyPatient.byNameOrCardNo", ref where) == -1) return null;

            where = string.Format(where, code);

            sql = sql + " " + where;

            return this.QueryRegister(sql);
        }

        #endregion

        #region 按护士站和急诊留观状态查询患者列表

        /// <summary>
        /// 按科室查询和急诊留观状态查询患者列表
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(string deptcode, string status)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byDeptCode", ref where) == -1) return null;

            where = string.Format(where, deptcode, status);

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }

        /// <summary>
        /// 急诊留观查询当前护理站的不同状态的病人信息(出观)
        /// </summary>
        /// <param name="deptcode">科室编码</param>
        /// <param name="status">状态</param>
        /// <param name="fromDate">出观起始时间</param>
        /// <param name="toDate">出观截至时间</param>
        /// <returns></returns>
        public ArrayList QueryPatient(string deptcode, string status, string fromDate, string toDate)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Register.Query.byDeptCodeAndOutDate", ref where) == -1) return null;

            where = string.Format(where, deptcode, status, fromDate, toDate);

            sql = sql + " " + where;

            return this.QueryRegister(sql);

        }

        /// <summary>
        /// 根据门诊号去有效的挂号信息
        /// </summary>
        /// <param name="clinicNO">门诊号</param>
        /// <returns></returns>
        public ArrayList QueryPatient(string clinicNO)
        {
            string sql = string.Empty;
            string whereSql = string.Empty;

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1)
            {
                this.Err = "未能找到索引为[Registration.Register.Query.1]的sql语句";
                return null;
            }

            if (this.Sql.GetCommonSql("Registration.Register.Query.WhereByClinic", ref whereSql) == -1)
            {
                this.Err = "未能找到索引为[Registration.Register.Query.WhereByClinic]的sql语句";
                return null;
            }

            try
            {
                whereSql = string.Format(whereSql, clinicNO);
                sql = sql + "  " + whereSql;
            }
            catch (Exception ex)
            {

                this.Err = "设置参数出错" + ex.Message;
                return null;
            }

            return this.QueryRegister(sql);
        }

        /// <summary>
        /// 根据门诊流水号查询挂号记录
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="state">0 无效；1 有效；其他 全部</param>
        /// <returns></returns>
        public ArrayList QueryPatientByState(string clinicNO, string state)
        {
            string sql = string.Empty;
            string whereSql = string.Empty;

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1)
            {
                this.Err = "未能找到索引为[Registration.Register.Query.1]的sql语句";
                return null;
            }

            if (this.Sql.GetCommonSql("Registration.Register.Query.WhereByClinicAndState", ref whereSql) == -1)
            {
                this.Err = "未能找到索引为[Registration.Register.Query.WhereByClinicAndState]的sql语句";
                return null;
            }

            try
            {
                whereSql = string.Format(whereSql, clinicNO, state);
                sql = sql + "  " + whereSql;
            }
            catch (Exception ex)
            {

                this.Err = "设置参数出错" + ex.Message;
                return null;
            }

            return this.QueryRegister(sql);
        }

        #endregion

        #region 根据职称获取诊查费项目

        /// <summary>
        /// 根据医生职级获取对应的诊查费项目
        /// </summary>
        /// <param name="doctRank"></param>
        /// <returns></returns>
        [Obsolete("作废", true)]
        public string GetDiagItemCodeByDoctRank(string doctRank)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1)
                return null;

            try
            {
                sql = string.Format(sql, doctRank);

                return this.ExecSqlReturnOne(sql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
        }

        #endregion

        #region 急诊判断

        #endregion

        #region 查询指定合单位已看诊患者信息
        /// <summary>
        /// 查询指定合单位已看诊患者信息
        /// {4C5542EA-E90E-4831-B430-3D3DBDE12066}
        /// </summary>
        /// <param name="strPactArr"></param>
        /// <param name="dtSeeDateBeg"></param>
        /// <param name="dtSeeDateEnd"></param>
        /// <returns></returns>
        public ArrayList QueryYNSeeRegister(DateTime dtSeeDateBeg, DateTime dtSeeDateEnd)
        {
            string sql = ""; string where = "";

            try
            {
                if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1) return null;
                if (this.Sql.GetCommonSql("Registration.Register.Query.24", ref where) == -1) return null;

                where = string.Format(where, dtSeeDateBeg.ToString(), dtSeeDateEnd.ToString());

                sql = sql + " " + where;

                return this.QueryRegister(sql);
            }
            catch (Exception objEx)
            {
                this.Err = objEx.Message;
                return null;
            }
        }


        #endregion

        #region 顺德医保用来存储医保返回的门诊流水号2010-9-16
        /// <summary>
        /// 更新判断是否为30种病种
        /// 顺德医保用来存储医保返回的门诊流水号
        /// {2C4A235D-390F-41d5-92DE-B59E87448BDE}
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="seeDeptID"></param>
        /// <param name="seeDoctID"></param>
        /// <returns></returns>
        public int UpdateDiagnose(FS.HISFC.Models.Registration.Register reg)
        {
            string sql = "";

            string[] parm = new string[] { reg.ID, reg.NormalName };

            if (this.Sql.GetCommonSql("Registration.Register.Update.Diagnose", ref sql) == -1) return -1;

            return this.ExecNoQuery(sql, parm);
        }

        public string QueryDiagnose(FS.HISFC.Models.Registration.Register reg)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.Register.Query.Diagnose", ref sql) == -1)
            {
                return "";
            }

            sql = string.Format(sql, reg.ID);
            return this.ExecSqlReturnOne(sql);
        }
        #endregion

        #region 补挂号相关查询

        /// <summary>
        /// 根据医生职级获取对于的挂号级别和诊查费
        /// </summary>
        /// <param name="doctCode">医生编码</param>
        /// <param name="doctLevl">医生职级编码</param>
        /// <param name="deptCode">科室编码</param>
        /// <param name="regLevl">挂号级别编码</param>
        /// <param name="diagItemCode">诊查费项目</param>
        /// <returns></returns>
        public int GetSupplyRegInfo(string doctCode, string doctLevl, string deptCode, ref string regLevl, ref string diagItemCode)
        {
            string sql = "";
            #region 先按照排班获取排班的挂号级别及诊查费项目

            sql = @"select f.reglevl_code,
                       (select t.item_code from fin_com_regfeeset t
                       where t.reglevl_code=f.reglevl_code
                               and t.dept_code='ALL'
                               and rownum=1) item_code,
                               1 sort
                        from fin_opr_schema f
                        where f.doct_code='{0}'
                        and f.dept_code='{1}'
                        --and f.noon_code='{2}'
                        and f.begin_time<=to_date('{3}','yyyy-mm-dd hh24:mi:ss')
                        and f.end_time>=to_date('{3}','yyyy-mm-dd hh24:mi:ss')

                        union all

                        select f.reglevl_code,
                               (select t.item_code from fin_com_regfeeset t
                               where t.reglevl_code=f.reglevl_code
                               and t.dept_code='{1}'
                               and rownum=1) item_code,
                               2 sort
                        from fin_opr_schema f
                        where f.doct_code='{0}'
                        and f.dept_code='{1}'
                        --and f.noon_code='{2}'
                        and f.begin_time<=to_date('{3}','yyyy-mm-dd hh24:mi:ss')
                        and f.end_time>=to_date('{3}','yyyy-mm-dd hh24:mi:ss')
                        order by sort";

            sql = string.Format(sql, doctCode, deptCode, "", this.GetDateTimeFromSysDateTime().ToString());

            try
            {
                if (this.ExecQuery(sql) == -1)
                {
                    return -1;
                }
                while (this.Reader.Read())
                {
                    regLevl = Reader[0].ToString();
                    diagItemCode = Reader[1].ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
            }
            #endregion

            #region 如果没有排班时，则按照职级获取

            //{A1BAF267-6053-44e3-B96E-36E2C48DE4BD}
            if (string.IsNullOrEmpty(regLevl) || string.IsNullOrEmpty(diagItemCode))
            {
                sql = @"select t.reglevl_code,--挂号级别
                               t.item_code, --诊查费项目
                               1 sort
                        from fin_com_regfeeset t
                        where t.levl_code='{0}'
                        and t.dept_code='{1}'

                        union
                        
                        select t.reglevl_code, --挂号级别
                               t.item_code, --诊查费项目
                               2 sort
                        from fin_com_regfeeset t
                        where t.dept_code = '{1}'
                        and t.reglevl_code = '{2}'

                        union

                        select t.reglevl_code,--挂号级别
                               t.item_code, --诊查费项目
                               3 sort
                        from fin_com_regfeeset t
                        where t.levl_code='{0}'
                        and t.dept_code='ALL'
                       
                        union

                        select t.reglevl_code, --挂号级别
                                t.item_code, --诊查费项目
                                4 sort
                        from fin_com_regfeeset t
                        where t.levl_code = 'ALL'
                        and t.dept_code = 'ALL'
   
                        order by sort --按照序号排序";

                //{61CC27CE-6E87-4412-8CCF-051A3862DDBD}

                sql = string.Format(sql, doctLevl, deptCode, regLevl);
                try
                {
                    if (this.ExecQuery(sql) == -1)
                    {
                        return -1;
                    }
                    while (this.Reader.Read())
                    {
                        regLevl = Reader[0].ToString();
                        diagItemCode = Reader[1].ToString();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    return -1;
                }
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 根据挂号级别获取诊查费项目
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="regLevl">挂号级别编码</param>
        /// <param name="diagItemCode">诊查费项目</param>
        /// <returns></returns>
        public int GetSupplyRegInfo(string deptCode, string regLevl, ref string diagItemCode)
        {
            string sql = @"select t.item_code, --诊查费项目
                               1 sort
                        from fin_com_regfeeset t
                        where t.reglevl_code='{0}'
                        and t.dept_code='{1}'

                        union

                        select t.item_code, --诊查费项目
                               2 sort
                        from fin_com_regfeeset t
                        where t.reglevl_code='{0}'
                        and t.dept_code='ALL'
   
                        union

                        select --t.reglevl_code, --挂号级别
                        t.item_code, --诊查费项目
                        3 sort
                        from fin_com_regfeeset t
                        where t.levl_code = 'ALL'
                        and t.dept_code = 'ALL'
   
                        order by sort --按照序号排序";

            try
            {
                if (this.ExecQuery(sql, regLevl, deptCode) == -1)
                {
                    Err = this.Sql.Err;
                    return -1;
                }
                while (this.Reader.Read())
                {
                    diagItemCode = Reader[0].ToString();
                    break;
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// 根据挂号级别获取诊查费项目
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="doctLevl">医生职级编码</param>
        /// <param name="regLevl">挂号级别编码</param>
        /// <param name="diagItemCode">诊查费项目</param>
        /// <returns></returns>
        public int GetSupplyRegInfo(string deptCode, string operLevel, string regLevl, ref string diagItemCode)
        {
            string sql = @"select t.item_code, --诊查费项目
                               1 sort
                        from fin_com_regfeeset t
                        where t.reglevl_code='{0}'
                        and t.dept_code='{1}'
                        and t.levl_code='{2}'

                        union

                        select t.item_code, --诊查费项目
                               2 sort
                        from fin_com_regfeeset t
                        where t.reglevl_code='{0}'
                        and t.dept_code='ALL'
                        --and t.levl_code='{2}'
   
                        union

                        select --t.reglevl_code, --挂号级别
                        t.item_code, --诊查费项目
                        3 sort
                        from fin_com_regfeeset t
                        where t.levl_code = 'ALL'
                        and t.dept_code = 'ALL'
   
                        order by sort --按照序号排序";

            try
            {
                if (this.ExecQuery(sql, regLevl, deptCode, operLevel) == -1)
                {
                    Err = this.Sql.Err;
                    return -1;
                }
                while (this.Reader.Read())
                {
                    diagItemCode = Reader[0].ToString();
                    break;
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
            }
            return 1;
        }

        #endregion

        #region 优化查询

        /// <summary>
        /// 查询挂号信息 
        /// 精简查询：门诊流水号、结算类别、合同单位、姓名、性别、出生日期
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QuerySimpleRegInfo(string whereIndex, params string[] args)
        {
            //查询主SQL
            string sql = @"select clinic_code,--门诊流水号
                                   name,--姓名
                                   sex_code,--性别
                                   birthday,--生日
                                   paykind_code,--结算类别
                                   pact_code,--合同单位
                                   seeno 看诊序号,
                                   card_no ,--病历号
                                   reg_date ,--挂号时间
                                   dept_code, --挂号科室
                                   doct_code, --挂号医生
                                   reglevl_code,    --挂号级别
                                   reglevl_name,
                                   order_no, --每日序号
                                   reglevl_code,
                                   reglevl_name,
                                    oper_date,
                                    assign_flag,
                                    assign_status,
                                    first_see_flag,
                                    preferential_flag,
                                    sequence_no
                            from fin_opr_register
                            ";
            if (this.Sql.GetCommonSql(whereIndex, ref whereIndex) == -1)
            {
                this.Err = Sql.Err;
                this.ErrCode = Sql.ErrCode;
                return null;
            }

            try
            {
                sql = sql + "\r\n" + whereIndex;

                sql = string.Format(sql, args);

                if (this.ExecQuery(sql) == -1)
                {
                }

                ArrayList al = new ArrayList();

                FS.HISFC.Models.Registration.Register regObj = null;
                while (this.Reader.Read())
                {
                    regObj = new FS.HISFC.Models.Registration.Register();
                    regObj.ID = this.Reader[0].ToString();//门诊流水号
                    regObj.Name = this.Reader[1].ToString();//姓名
                    regObj.Sex.ID = this.Reader[2].ToString();//性别
                    regObj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[3]);//生日
                    regObj.Pact.PayKind.ID = Reader[4].ToString();//结算类别
                    regObj.Pact.ID = this.Reader[5].ToString();//合同单位
                    regObj.DoctorInfo.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString()); //看诊序号
                    regObj.PID.CardNO = this.Reader[7].ToString();
                    regObj.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[8]);
                    regObj.DoctorInfo.Templet.Dept.ID = Reader[9].ToString();
                    regObj.DoctorInfo.Templet.Doct.ID = Reader[10].ToString();
                    regObj.DoctorInfo.Templet.RegLevel.ID = Reader[11].ToString();
                    regObj.DoctorInfo.Templet.RegLevel.Name = Reader[12].ToString();
                    regObj.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[13].ToString());
                    regObj.DoctorInfo.Templet.RegLevel.ID = Reader[14].ToString();
                    regObj.DoctorInfo.Templet.RegLevel.Name = Reader[15].ToString();
                    regObj.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[16]);

                    //{8FE4C905-279D-48c7-9D1B-D0742556A102}
                    regObj.AssignFlag = Reader[17].ToString();
                    regObj.AssignStatus = Reader[18].ToString();
                    regObj.FirstSeeFlag = Reader[19].ToString();
                    regObj.PreferentialFlag = Reader[20].ToString();

                    if (Reader[21] == null || string.IsNullOrEmpty(Reader[21].ToString()))
                    {
                        regObj.SequenceNO = 0;
                    }
                    else
                    {
                        regObj.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[21].ToString());
                    }

                    al.Add(regObj);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;

                return null;
            }
            finally
            {
                if (this.Reader != null && !Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 按照挂号科室查询一段时间内有效挂号信息
        /// 只查询必要信息：门诊流水号、结算类别、合同单位、姓名、性别、出生日期
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee">0 否；1 是；ALL 全部</param>
        /// <param name="isValid">0 退费；1 有效；2 作废；ALL 全部</param>
        /// <returns></returns>
        public ArrayList QuerySimpleRegByDept(string deptID, DateTime beginDate, DateTime endDate, string isSee, string isValid)
        {
            return this.QuerySimpleRegInfo("Registration.Register.QuerySimple.ByDept", deptID, beginDate.ToString(), endDate.ToString(), isSee, isValid);
        }

        #endregion

        /// <summary>
        /// 得到当前操作员从当前开始计算前N张发票的信息
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns>成功: 结算信息数组 失败: null</returns>
        public ArrayList QueryRegistersByCount(string operCode, int count)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Register.Query.1", ref sql) == -1)
            {
                return null;
            }
            if (this.Sql.GetCommonSql("Registration.Register.Query.ByOperAndCount", ref where) == -1)
            {
                where = @" where ROWNUM <= {1}
                                       and  oper_date > trunc(sysdate)
	                                   and  oper_code = '{0}'
	                                   order by   OPER_DATE DESC";
            }

            try
            {
                where = string.Format(where, operCode, count);
            }
            catch (Exception e)
            {
                this.Err = "[" + where + "]" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;
            return this.QueryRegister(sql);
        }
        public FS.HISFC.Models.Base.Department GetDeptmentById(string id)
        {

            string sql = "";
            if (this.Sql.GetCommonSql("Department.SelectDepartmentByID", ref sql) == -1)
                return null;

            try
            {
                sql = string.Format(sql, id);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "接口错误！" + ex.Message;
                this.WriteErr();
                return null;
            }

            if (this.ExecQuery(sql) == -1) return null;

            if (this.Reader.Read())
            {
                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();
                dept.ID = (string)this.Reader[0];
                dept.Name = (string)this.Reader[1];

                if (!(this.Reader.IsDBNull(2)))
                    dept.SpellCode = this.Reader.GetString(2);
                if (!(this.Reader.IsDBNull(3)))
                    dept.WBCode = this.Reader.GetString(3);
                if (!(this.Reader.IsDBNull(4)))
                    dept.EnglishName = this.Reader.GetString(4);

                //dept.DeptType =	 new DepartmentType();
                dept.DeptType.ID = (string)this.Reader[5];

                if (!(this.Reader.IsDBNull(6)))
                {
                    if (this.Reader[6].ToString() == "0")
                        dept.IsRegDept = false;//Convert.ToBoolean(this.Reader[6]);
                    else
                        dept.IsRegDept = true;
                }
                if (!(this.Reader.IsDBNull(7)))
                {
                    //	dept.IsRegDept = Convert.ToBoolean(this.Reader[7]);
                    if (this.Reader[7].ToString() == "0")
                        dept.IsStatDept = false;
                    else
                        dept.IsStatDept = true;
                }

                dept.SpecialFlag = this.Reader[8].ToString();
                dept.ValidState = (FS.HISFC.Models.Base.EnumValidState)Convert.ToInt32(this.Reader[9]);

                if (!(this.Reader.IsDBNull(10)))
                    dept.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10].ToString());
                //自定义码
                dept.UserCode = this.Reader[11].ToString();
                dept.ShortName = this.Reader[12].ToString();

                //{335827AE-42F6-4cbd-A73F-1B900B070E74}
                try
                {
                    dept.HospitalID = this.Reader[13].ToString();
                    dept.HospitalName = this.Reader[14].ToString();
                }
                catch
                {
                }

                this.Reader.Close();
                return dept;

            }

            return null;


        }

        //public void GetPhyDataByPhone(string phone,Register r)
        //{
        //    string sql = "";

        //    if (this.Sql.GetCommonSql("HISFC.BizLogic.Registration.Register.GetPhyDataByPhone", ref sql) == -1)
        //    {
        //        return ;
        //    }
        //    sql = string.Format(sql, phone);

        //    try
        //    {
        //        if (this.ExecQuery(sql) == -1)
        //        {
        //            return ;
        //        }
        //        while (this.Reader.Read())
        //        {
        //            regLevl = Reader[0].ToString();
        //            diagItemCode = Reader[1].ToString();
        //            break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Err = ex.Message;
        //        return ;
        //    }
        //    //HISFC.BizLogic.Registration.Register.GetPhyDataByPhone
        //}

    }

    /// <summary>
    /// 挂号操作的类型
    /// </summary>
    public enum EnumUpdateStatus
    {
        /// <summary>
        /// 退号
        /// </summary>
        Return,
        /// <summary>
        /// 换科
        /// </summary>
        ChangeDept,
        /// <summary>
        /// 作废
        /// </summary>
        Cancel,
        /// <summary>
        /// 患者信息
        /// </summary>
        PatientInfo,
        /// <summary>
        /// 取消作废
        /// </summary>
        Uncancel,
        /// <summary>
        /// 废号
        /// </summary>
        Bad
    }

    /// <summary>
    /// 挂号打印接口
    /// </summary>
    public interface IRegPrint
    {
        /// <summary>
        /// 患者挂号信息
        /// </summary>
        FS.HISFC.Models.Registration.Register RegInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 打印函数
        /// </summary>
        /// <returns></returns>
        int Print();
    }


}
