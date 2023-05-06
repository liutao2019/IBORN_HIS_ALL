using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.BizProcess.Interface.Common;

namespace HISTIMEJOB
{
    /// <summary>
    /// FixedFee<br></br>
    /// [功能描述: 固定费用收取类]<br></br>
    /// [创 建 者: 王儒超]<br></br>
    /// [创建时间: 2007-1-9]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class FixedFee : FS.FrameWork.Management.Database, IJob
    {
        #region "变量"
        //服务器时间


        public DateTime dtServerDateTime;

        //上次固定费用收取时间
        public DateTime dtPreFixFeeDateTime;
        //占床包床挂床数据集合
        public ArrayList alBedInfo = new ArrayList();
        //显示的文本框,引用主窗口的文本框


        public FS.FrameWork.WinForms.Controls.NeuRichTextBox rtbLogo = null;
        //费用管理类		
        public FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
        //患者管理类
        private FS.HISFC.BizLogic.RADT.InPatient radtInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        //床位费管理类
        private FS.HISFC.BizLogic.Fee.BedFeeItem feeBedFeeItem = new FS.HISFC.BizLogic.Fee.BedFeeItem();
        //床位管理类
        private FS.HISFC.BizLogic.Manager.Bed managerBed = new FS.HISFC.BizLogic.Manager.Bed();

        private FS.HISFC.BizProcess.Integrate.Fee interageFee = new FS.HISFC.BizProcess.Integrate.Fee();
        #endregion

        #region "函数"

        /// <summary>
        /// 获取占用床位列表
        /// </summary>
        /// <returns>null失败</returns>
        public ArrayList GetUsedBeds()
        {
            ArrayList ALLBedInfo = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("FixFee.GetUsedBeds", ref strSql) == -1)
            {
                //内部已经记录错误日志
                //this.Err = "获得占用床位列表失败fixFee.GetUsedBeds出错!";
                return null;
            }

            if (Reader != null && Reader.IsClosed == false)
                Reader.Close();

            if (this.ExecQuery(strSql) == -1) return null;
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.Bed bedInfo = new FS.HISFC.Models.Base.Bed();
                bedInfo.ID = Reader[0].ToString();
                bedInfo.BedGrade.ID = Reader[1].ToString();
                bedInfo.InpatientNO = Reader[2].ToString();
                bedInfo.Status.ID = Reader[3].ToString();
                ALLBedInfo.Add(bedInfo);
            }
            this.Reader.Close();
            return ALLBedInfo;
        }

        /// <summary>
        /// 固定费用收取
        /// </summary>
        /// <returns>1 成功 －1 失败</returns>
        public int BedFeeStart()
        {
            try
            {
                //服务器时间
                dtServerDateTime = this.GetDateTimeFromSysDateTime();

                if (dtServerDateTime == DateTime.MinValue)
                {
                    return -1;
                }

                //获取占床全部数据集合
                //获取 O占床,W包床,R请假
                this.alBedInfo = this.GetUsedBeds();
                if (alBedInfo == null)
                {
                    WriteErr();
                    return -1;//如果没有取到占床列表则返回
                }

                FS.HISFC.Models.Base.Bed bed = null;
                for (int i = 0; i < alBedInfo.Count; i++)
                {
                    bed = (FS.HISFC.Models.Base.Bed)alBedInfo[i];

                    //验证数据合法性
                    if (bed.ID == null || bed.ID == "")
                    {
                        //床应该有效
                        this.Err = "床号为空!请到在占床表中查!";
                        WriteErr();
                        continue;
                    }

                    //判断患者的住院流水号					
                    if (bed.InpatientNO == null || bed.InpatientNO == "")
                    {
                        this.Err = bed.ID + "床的患者住院流水号为空!";
                        this.WriteErr();
                        continue;
                    }

                    //判断床位等级
                    if (bed.BedGrade.ID == null || bed.BedGrade.ID == "")
                    {
                        this.Err = bed.ID + "床的床位等级为空!";
                        this.WriteErr();
                        continue;
                    }

                    //这里不处理包床，等处理患者的主床的时候处理包床
                    if (bed.Status.ID.ToString() == "W")//包床
                    {
                        continue;
                    }

                    //处理每张床患者费用
                    try
                    {
                        if (SetFeeByPerson(bed, dtServerDateTime) == -1)
                        {
                            this.Err = "床位：" + bed.ID + "的床位费收取异常，原因：" + this.Err;
                            this.WriteErr();
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Err = "床位：" + bed.ID + "的床位费收取异常，原因：" + ex.Message;
                        this.WriteErr();
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = "收取固定床位费错误!" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获得最大接诊时间
        /// </summary>
        /// <param name="clinicNo">住院号</param>
        /// <returns></returns>
        private string GetMaxOperDateTime(string clinicNo)
        {
            string Sql = string.Empty;
            if (this.Sql.GetSql("FixFee.GetMaxDateTime", ref Sql) == -1)
            {
                this.Err = "查找SQL语句失败！";
                return "-1";
            }
            try
            {
                Sql = string.Format(Sql, clinicNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return "-1";
            }
            return this.Sql.ExecSqlReturnOne(Sql);
        }

        /// <summary>
        /// 按患者收取固定费用
        /// </summary>
        /// <param name="bed">床位实体</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>1成功 －1失败</returns>
        public int SetFeeByPerson(FS.HISFC.Models.Base.Bed bed, DateTime operDate)
        {
            //获取患者基本信息
            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtInpatient.QueryPatientInfoByInpatientNO(bed.InpatientNO);
            if (patientInfo == null)
            {
                this.Err = bed.ID + "获取患者基本信息失败："  + this.radtInpatient.Err;
                WriteErr();
                return -1;
            }
            //如果是婴儿，不收床位费
            if (patientInfo.IsBaby)
            {
                this.Err = "婴儿不收床位费" + patientInfo.ID;
                this.WriteErr();
                return 1;
            }

            //如果患者已出院,则不收取床位费.并提示有错误存在.可能在包床上出的问题.
            if (patientInfo.PVisit.InState.ID.ToString() != "I")
            {
                this.Err = "患者不是在院状态" + patientInfo.ID;
                this.WriteErr();
                return 1;
            }

            ArrayList alBed = new ArrayList();

            alBed.Add(bed);

            //获得请包床信息，请假床算作主床
            ArrayList otherBed = managerBed.GetOtherBedList(patientInfo.ID);
            if (otherBed == null)
            {
                this.Err = "获得包床信息出错!" + managerBed.Err;
                WriteErr();
                return -1;
            }
            alBed.AddRange(otherBed);

            //入院没有收过床费,先收一个
            string resultValue = this.GetMaxOperDateTime(patientInfo.ID);
            if (resultValue == "-1")
            {
                this.Err = "获取最大接诊时间出错" + resultValue;
                return -1;
            }
            DateTime date = FS.FrameWork.Function.NConvert.ToDateTime(resultValue);
            //{8DEDFDE0-5137-4f33-9ADD-52E69F83E2E1}
            if (patientInfo.FT.PreFixFeeDateTime == DateTime.MinValue)
            {
                patientInfo.FT.PreFixFeeDateTime = date;
            }

            if (patientInfo.FT.PreFixFeeDateTime == date)
            {
                //patientInfo.FT.PreFixFeeDateTime = DateTime.Parse(patientInfo.PVisit.InTime.Date.ToString("D") + " 23:30:00");
                //如果入院时间和接诊时间不在一天，则会多收床位费
                //按照接诊时间收取，此处上次收费时间按照接诊时间获取 houwb 2011-8-16 10:43:36
                patientInfo.FT.PreFixFeeDateTime = DateTime.Parse(patientInfo.FT.PreFixFeeDateTime.Date.ToString("yyyy-MM-dd") + " 23:30:00");
                //收取1天的费用
                ArrayList itemsSupply = null;
                foreach (FS.HISFC.Models.Base.Bed objBed in alBed)
                {

                    itemsSupply = this.feeBedFeeItem.QueryBedFeeItemByMinFeeCode(objBed.BedGrade.ID);
                    if (itemsSupply == null)
                    {
                        this.Err = objBed.ID + "床未维护收费项目!";
                        WriteErr();
                        return -1;
                    }
                    if (itemsSupply.Count == 0)
                    {
                        continue;
                    }
                    ArrayList alNormalItemSupply = new ArrayList();   //正常床位费
                    ArrayList alBabyItemSupply = new ArrayList();    //婴儿床位费
                    foreach (FS.HISFC.Models.Fee.BedFeeItem b in itemsSupply)
                    {
                        if (b.IsBabyRelation)
                        {
                            //婴儿相关
                            alBabyItemSupply.Add(b);
                        }
                        else
                        {
                            alNormalItemSupply.Add(b);
                        }
                    }

                    //收取1天的费用-正常床位费用
                    if (interageFee.DoBedItemFee(alNormalItemSupply, patientInfo, 1, operDate, patientInfo.FT.PreFixFeeDateTime, objBed) == -1)
                    {
                        return -1;
                    }

                    //收取1天的费用-婴儿床位费用（包床，不需要收取婴儿的床位费用）
                    if (patientInfo.IsHasBaby && alBabyItemSupply.Count > 0 && objBed.Status.ID.ToString() != "W")
                    {
                        #region 婴儿床位费处理

                        try
                        {
                            //获得该母亲的所有的有效婴儿-返回的是婴儿在主表的信息
                            ArrayList alBaby = this.radtInpatient.QueryBabiesByMother(patientInfo.ID);
                            if (alBaby != null && alBaby.Count > 0)
                            {
                                for (int i = 0; i < alBaby.Count; i++)
                                {
                                    FS.HISFC.Models.RADT.PatientInfo babyInfo = alBaby[i] as FS.HISFC.Models.RADT.PatientInfo;
                                    if (babyInfo == null)
                                    {
                                        continue;
                                    }
                                    if (babyInfo.PVisit.InState.ID.ToString() != "I")
                                    {
                                        continue;   //不在院的情况，就不应该收取床位费
                                    }

                                    if (babyInfo.PVisit.InTime.Date <= patientInfo.FT.PreFixFeeDateTime.Date)  //在婴儿出生日期之前的床位费用不应该收取
                                    {
                                        babyInfo.FT.PreFixFeeDateTime = patientInfo.FT.PreFixFeeDateTime;
                                        if (this.interageFee.DoBedItemFee(alBabyItemSupply, babyInfo, 1, operDate, babyInfo.FT.PreFixFeeDateTime, objBed) == -1)
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { }

                        #endregion
                    }

                }
            }

            //判断固定费用收取的天数

            //{34522F2E-59B9-4916-A0BB-4CD2E093AD5D}防止时间不同步
            //System.TimeSpan interval = operDate.Subtract(patientInfo.FT.PreFixFeeDateTime);	//当前时间跟上次收取时间的间隔天数
            //System.TimeSpan interval = operDate.AddHours(0.5).Subtract(patientInfo.FT.PreFixFeeDateTime);	//当前时间跟上次收取时间的间隔天数

            System.TimeSpan interval = operDate.Date - patientInfo.FT.PreFixFeeDateTime.Date;
            if (interval.Days == 0) 
                return 1;

            //实际收取天数 = 间隔天数 / 计费间隔(有可能0.5天收一次)
            int days = interval.Days / patientInfo.FT.FixFeeInterval;
            //跟踪病区床位费十天收一次,固days可能等于0 --Modify by Maokb 060227
            if (days == 0) 
                return 1;

            //记录本次收取固定费用的收取时间


            for (int i = 0; i < days; i++)
            {
                patientInfo.FT.PreFixFeeDateTime = patientInfo.FT.PreFixFeeDateTime.AddDays(patientInfo.FT.FixFeeInterval);
                //大于当前时间点 不收取，等晚上在收取
                if (patientInfo.FT.PreFixFeeDateTime > operDate)
                {
                    continue;
                }

                ArrayList items = null;

                foreach (FS.HISFC.Models.Base.Bed objBed in alBed)
                {

                    items = this.feeBedFeeItem.QueryBedFeeItemByMinFeeCode(objBed.BedGrade.ID);
                    if (items == null)
                    {
                        this.Err = objBed.ID + "床未维护收费项目!";
                        WriteErr();
                        return -1;
                    }
                    if (items.Count == 0)
                    {
                        continue;//没有维护项目
                    }
                    ArrayList alNormalItemSupply = new ArrayList();   //正常床位费
                    ArrayList alBabyItemSupply = new ArrayList();    //婴儿床位费
                    foreach (FS.HISFC.Models.Fee.BedFeeItem b in items)
                    {
                        if (b.IsBabyRelation)
                        {
                            //婴儿相关
                            alBabyItemSupply.Add(b);
                        }
                        else
                        {
                            alNormalItemSupply.Add(b);
                        }
                    }

                    //收取days天的费用,每次收1个，划价时间为固定费用时间
                    if (interageFee.DoBedItemFee(alNormalItemSupply, patientInfo, 1, operDate, patientInfo.FT.PreFixFeeDateTime, objBed) == -1)
                    {
                        return -1;
                    }

                    //收取days天的费用,每次收1个，划价时间为固定费用时间-婴儿床位费用（包床，不需要收取婴儿的床位费用）
                    if (patientInfo.IsHasBaby && alBabyItemSupply.Count > 0 && objBed.Status.ID.ToString() != "W")
                    {
                        #region 婴儿床位费处理

                        try
                        {
                            //获得该母亲的所有的有效婴儿-返回的是婴儿在主表的信息
                            ArrayList alBaby = this.radtInpatient.QueryBabiesByMother(patientInfo.ID);
                            if (alBaby != null && alBaby.Count > 0)
                            {
                                for (int k = 0; k < alBaby.Count; k++)
                                {
                                    FS.HISFC.Models.RADT.PatientInfo babyInfo = alBaby[k] as FS.HISFC.Models.RADT.PatientInfo;
                                    if (babyInfo == null)
                                    {
                                        continue;
                                    }
                                    if (babyInfo.PVisit.InState.ID.ToString() != "I")
                                    {
                                        continue;   //不在院的情况，就不应该收取床位费
                                    }

                                    if (babyInfo.FT.PreFixFeeDateTime == null || babyInfo.FT.PreFixFeeDateTime == DateTime.MinValue
                                        || babyInfo.FT.PreFixFeeDateTime.Date <= new DateTime(2000, 1, 1, 0, 0, 0))
                                    {
                                        //如果没有上次收取固定费用收取时间，则通过本次收取的固定时间收取
                                        DateTime dtBabyReg = babyInfo.PVisit.InTime.Date;//婴儿登记日期，本次收取固定费用日期
                                        babyInfo.FT.PreFixFeeDateTime = new DateTime(dtBabyReg.Year, dtBabyReg.Month, dtBabyReg.Day, 23, 30, 0);
                                    }
                                    else
                                    {
                                        //如果有上次收取固定费用收取时间，则+1，收取固定费用日期，同时取时间点为23:30:00
                                        DateTime dtBabyLast = babyInfo.FT.PreFixFeeDateTime.Date.AddDays(1);
                                        babyInfo.FT.PreFixFeeDateTime = new DateTime(dtBabyLast.Year, dtBabyLast.Month, dtBabyLast.Day, 23, 30, 0);
                                    }

                                    //在婴儿出生日期之前的床位费用不应该收取,同时之前没有收取的也需要补收取
                                    System.TimeSpan spanDay = patientInfo.FT.PreFixFeeDateTime.Date - babyInfo.FT.PreFixFeeDateTime.Date;
                                    int addDays = spanDay.Days;
                                    for (int d = addDays; addDays >= 0; addDays--)
                                    {
                                        if (this.interageFee.DoBedItemFee(alBabyItemSupply, babyInfo, 1, operDate, babyInfo.FT.PreFixFeeDateTime, objBed) == -1)
                                        {
                                            continue;
                                        }
                                        babyInfo.FT.PreFixFeeDateTime = babyInfo.FT.PreFixFeeDateTime.AddDays(1);
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { }

                        #endregion
                    }

                }
            }
            return 1;
        }

        /// <summary>
        /// 写错误日志


        /// </summary>
        public override void WriteErr()
        {
            this.myMessage = this.Err;
            base.WriteErr();
        }

        #endregion

        #region IJob 成员
        private string myMessage = ""; //传递消息

        public string Message
        {
            // TODO:  添加 calculateFee.Con setter 实现
            get
            {
                return this.myMessage;
            }
        }

        public int Start()
        {
            // TODO:  添加 calculateFee.Start 实现
            return this.BedFeeStart();
        }

        #endregion

    }
}
