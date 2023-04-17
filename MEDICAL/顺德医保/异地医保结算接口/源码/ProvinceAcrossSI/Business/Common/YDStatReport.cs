using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ProvinceAcrossSI.Business.Common
{
    public class YDStatReport : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 查询需要申报的患者明细信息
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns>险种类型</returns>
        public ArrayList QueryYDLiquidation(DateTime dtBegin,DateTime dtEnd,string type)
        {

            string sqlStr = @"
                            
                    select JZDJH,     
                        --就诊登记号'; 

                         MCARD_NO,       INSUREDCENTERAREACODE,   NAME,       SEX,            BIRTHDAY, 
                        --'医保编号'; '参保地分中心编号'; '姓名';'性别'; '出生日期';

                         IDNO,           YLRYLB,                  AGE,        WORKPLACECODE,  WORKPLACENAME, 
                        --'证件号码'; '医疗人员类别'; '实际年龄';'单位编码';'单位名称';

                         JJLX,           DWLX,                           JFNX,           ZHYE, 
                        --'经济类型'; '单位类型'; '缴费年限';'帐户余额';

                         QILJ,           BCYFQFX,                 JBYLBCZFXE, DBYLBCZFXE,     GWYBCZFXE, 
                        --'起付线累计'; '本次应付起付线';'基本医疗本次支付限额'; '大病医疗本次支付限额';'公务员本次支付限额';

                         JBYLTCLJ,       DBYLTCLJ,                IN_TIMES,   IN_STATE,       ECFYSPBZ, 
                        --'基本医疗统筹累计'; '大病医疗统筹累计';'住院次数'; '住院状态'; '二次返院审批标志';

                         ZYBZ,           ZCJZDJH,                 ZCYYMC,     DJZRYYMC,       ZYLB, 
                        --'转院标志';'转出就诊登记号'; '转出医院名称'; '登记转入医院名称'; '转院类别';

                         ZC_DATE,        ZZZD,                    ZZZYSPJG,   ZZYY,         yd.TOT_COST,
                        --'转出日期';'转诊诊断'; '转诊转院审批结果';'转诊原因'; 费用总额

                         yd.OWN_COST,         yd.PAY_COST,           yd.PUB_COST,    GRZF_COST,    LIMIT_COST, 
                        --自费部分;自付部分;允许报销部分;个人支付金额;起付标准; 

                         GZZF_COST,        CBXXEZF_COST,       YBTCZF_COST, TCJJZF_COST,  JBYLTCZF_COST, 
                        --个人账户支付金额;超报销限额自付金额;医保统筹支付总额;统筹基金支付;基本医疗统筹自付部分

                         DBYLTCZF_COST,    DBYLTCZIFU_COST,    GWYBZ_COST,  GWYDB_COST,   YLBZBF_COST, 
                        --大病医疗统筹支付部分;大病医疗统筹自付部分;公务员补助;公务员大病;医疗补助部分;

                         GWYCXBZ_COST,     QTBZZF_COST,        JSYWH,       MEMO,         SUMCOSTJBYL, 
                        --公务员超限补助部分;其它补助支付;结算业务号;自费原因;统筹累计已支付;

                         BCYLLJYZF_COST,   GWYBCLJYZF_COST,    GFDGRZF_COST,INSUREDAREACODE,NATION,
                        --补充医疗累计已支付;公务员补助累计已支付;共付段个人支付,参保地统筹区编码,民族,

                        ZJLX,              XZLX,               GWYFLAG,     ZZJGDM,         YDJYBAH,
                        --证件类型;险种类型;公务员标识;组织机构代码;异地就医备案号;

                        BALANCE_FLAG,      JYLSH,              yd.Balance_No,   yd.PERSON_TYPE, DIAGN1,DIAGN2,DIAGN3,
                        --结算标志,交易流水号
                        GRZIFUJE,          QTZF,      ZYFYJSFS,       QTLJ,      DBBXYLTCLJ,    
                        --个人自负金额,其他支付,  主要费用结算方式,  其他累计
                        GWYBZZF_COST , regTransID, balanceTransID,out_transid,            yd.inpatient_no,
                        --              登记流水号，结算流水号,   出院登记流水号,           住院流水号
                        seedoctype
                        --就诊类别
                    from  fin_ipr_siinmaininfo_yd yd 
                    where yd.xzlx='{2}'
                    and yd.balance_flag='1'
                    and yd.is_month_balance='1'
                    and yd.inpatient_no in 
                    (
                    select i.inpatient_no from  fin_ipr_siinmaininfo i 
                    where i.balance_date between to_date('{0}','yyyy-MM-dd hh24:mi:ss') and to_date('{1}','yyyy-MM-dd hh24:mi:ss') 
                    and i.valid_flag ='1'
                    and i.balance_state='1'
                    )
            ";
            sqlStr = string.Format(sqlStr, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), type);
            ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
            ArrayList alTemp = new ArrayList();
            try
            {
                this.ExecQuery(sqlStr);
                 while (this.Reader.Read())
                {
                    personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
                    personInfo.ClinicNo = this.Reader[0].ToString();
                    //医保编号5,参保地分中心编号6,姓名7,性别8，出生日期9
                    personInfo.SIcode = this.Reader[1].ToString();
                    personInfo.InsuredCenterAreaCode = this.Reader[2].ToString();
                    personInfo.Name = this.Reader[3].ToString();
                    personInfo.Sex = this.Reader[4].ToString();
                    personInfo.Birth = this.Reader[5].ToString();
                    //证件号码10
                    personInfo.IdenNo = this.Reader[6].ToString();
                    //医疗人员类别11
                    personInfo.PersonType = this.Reader[7].ToString();
                    //人群类别
                    //personInfo.RQtype ,
                    //实际年龄12
                    personInfo.Age = this.Reader[8].ToString();
                    //单位编码13
                    personInfo.CompanyCode = this.Reader[9].ToString();
                    //单位名称14
                    personInfo.CompanyName = this.Reader[10].ToString();
                    //经济类型15
                    personInfo.EconomicType = this.Reader[11].ToString();
                    //单位类型16  隶属关系17
                    personInfo.CompanyType = this.Reader[12].ToString();
                    //缴费年限18
                    personInfo.PayYears = this.Reader[13].ToString();
                    //帐户余额19
                    personInfo.RestAccount = this.Reader[14].ToString();
                    //起付线累计20
                    personInfo.SumCostLine = this.Reader[15].ToString();
                    //本次应付起付线21
                    personInfo.CostLine = this.Reader[16].ToString();
                    //基本医疗本次支付限额22
                    personInfo.LimitCostJBYL = this.Reader[17].ToString();
                    //大病医疗本次支付限额23
                    personInfo.LimitCostDBYL = this.Reader[18].ToString();
                    //公务员本次支付限额24
                    personInfo.LimitCostGWY = this.Reader[19].ToString();
                    //基本医疗统筹累计25
                    personInfo.SumCostJBYL = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                    //大病医疗统筹累计26
                    personInfo.SumCostDBYL = this.Reader[21].ToString();
                    //住院次数27
                    personInfo.InTimes = this.Reader[22].ToString();
                    //住院状态28
                    personInfo.InState = this.Reader[23].ToString();
                    //二次返院审批标志29
                    personInfo.ReturnsFlag = this.Reader[24].ToString();
                    //转院标志30
                    personInfo.ChangeOutFlag = this.Reader[25].ToString();
                    //转出就诊登记号31
                    personInfo.ChangeOutClinicNo = this.Reader[26].ToString();
                    //转出医院名称32
                    personInfo.ChangeOutHosName = this.Reader[27].ToString();
                    //登记转入医院名称33
                    personInfo.ChangeInHosName = this.Reader[28].ToString();
                    //转院类别34
                    personInfo.ChangeType = this.Reader[29].ToString();
                    //转出日期35
                    personInfo.ChangeDate = this.Reader[30].ToString();
                    //转诊诊断36
                    personInfo.ChangeDiagn = this.Reader[31].ToString();
                    //转诊转院审批结果37
                    personInfo.ChangePassFlag = this.Reader[32].ToString();
                    //转诊原因38
                    personInfo.ChangeReason = this.Reader[33].ToString();
                    //费用总额
                    personInfo.tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[34].ToString());
                    //自费部分;自付部分;允许报销部分;个人支付金额;起付标准; 
                    personInfo.own_cost_part = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[35].ToString());
                    personInfo.pay_cost_part = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());
                    personInfo.pub_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[37].ToString());
                    personInfo.GRZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[38].ToString());
                    personInfo.limit_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[39].ToString());
                    //个人账户支付金额;超报销限额自付金额;医保统筹支付总额;统筹基金支付;基本医疗统筹自付部分
                    personInfo.GZZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[40].ToString());
                    personInfo.CBXXEZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[41].ToString());
                    personInfo.YBTCZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[42].ToString());
                    personInfo.TCJJZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[43].ToString());
                    personInfo.JBYLTCZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[44].ToString());
                    //大病医疗统筹支付部分;大病医疗统筹自付部分;公务员补助;公务员大病;医疗补助部分;
                    personInfo.DBYLTCZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[45].ToString());
                    personInfo.DBYLTCZIFU_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[46].ToString());
                    personInfo.GWYBZ_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[47].ToString());
                    personInfo.GWYDB_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[48].ToString());
                    personInfo.YLBZBF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[49].ToString());
                    //公务员超限补助部分;其它补助支付;结算业务号;自费原因;统筹累计已支付;
                    personInfo.GWYCXBZ_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[50].ToString());
                    personInfo.QTBZZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[51].ToString());
                    personInfo.JSYWH = this.Reader[52].ToString();
                    personInfo.Memo = this.Reader[53].ToString();
                    personInfo.SumCostJBYL = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[54].ToString());
                    //补充医疗累计已支付;公务员补助累计已支付;共付段个人支付,参保地统筹区编码,民族,
                    personInfo.BCYLLJYZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[55].ToString());
                    personInfo.GWYBCLJYZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[56].ToString());
                    personInfo.GFDGRZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[57].ToString());
                    personInfo.InsuredAreaCode = this.Reader[58].ToString();
                    personInfo.nation = this.Reader[59].ToString();
                    //证件类型;险种类型;公务员标识;组织机构代码;异地就医备案号;
                    personInfo.ZJLX = this.Reader[60].ToString();
                    personInfo.xzlx = this.Reader[61].ToString();
                    personInfo.GWYflag = this.Reader[62].ToString();
                    personInfo.ZZJGDM = this.Reader[63].ToString();
                    personInfo.YDJYBAH = this.Reader[64].ToString();
                    //结算标志,交易流水号,结算序号
                    personInfo.BalanceFlag = this.Reader[65].ToString();
                    personInfo.jylsh = this.Reader[66].ToString();
                    personInfo.BalanceNo = this.Reader[67].ToString();
                    personInfo.PersonType = this.Reader[68].ToString();
                    personInfo.Diagn1 = this.Reader[69].ToString();
                    personInfo.Diagn2 = this.Reader[70].ToString();
                    personInfo.Diagn3 = this.Reader[71].ToString();
                    personInfo.GRZIFUJE = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[72].ToString());
                    personInfo.QTZF = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[73].ToString());
                    personInfo.ZYFYJSFS = this.Reader[74].ToString();
                    personInfo.qtlj = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[75].ToString());
                    personInfo.DBBXYLTCLJ = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[76].ToString());
                    personInfo.GWYBZZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[77].ToString());
                    personInfo.regTransID = this.Reader[78].ToString();
                    personInfo.balanceTransID = this.Reader[79].ToString();
                    personInfo.Out_TransID = this.Reader[80].ToString();
                    personInfo.InPatient_No = this.Reader[81].ToString();
                    personInfo.SeeDocType = this.Reader[82].ToString();
                    alTemp.Add(personInfo);
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return alTemp;
        }

        /// <summary>
        /// 获取申报的业务流水号
        /// </summary>
        /// <returns></returns>
        public string GetLiquidationBusNO()
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("SOC.Local.BeiJiao.YD.Liquidation.2", ref sql) == -1)
            {
                this.Err = "获得非药品流水号查询字段SOC.Local.BeiJiao.YD.Liquidation.2出错!";

                return null;
            }

            return this.ExecSqlReturnOne(sql);
        }

        /// <summary>
        /// 插入申报情况
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertLiquidInfo(FS.FrameWork.Models.NeuObject neuObj,FS.FrameWork.Models.NeuObject commObj)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("SOC.Local.BeiJiao.YD.Liquidation.3", ref sql) == -1)
            {
                this.Err = "获得异地结算 插入结算申报表SOC.Local.BeiJiao.YD.Liquidation.3出错!";

                return -1;
            }
            //valid_flag : 1 oper_date: sysdate
            sql = string.Format(sql, neuObj.ID, neuObj.Name, neuObj.Memo, neuObj.User01, commObj.User01,
                                commObj.User02,commObj.User03,commObj.Memo,neuObj.User02);

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 查询申报情况
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="type">险种类别 All</param>
        /// <returns></returns>
        public ArrayList QueryLiquidInfo(string year, string month, string type)
        {
            string sql = string.Empty;
            ArrayList al = new ArrayList();

            if (this.Sql.GetSql("SOC.Local.BeiJiao.YD.Liquidation.4", ref sql) == -1)
            {
                this.Err = "没有找到索引为:SOC.Local.BeiJiao.YD.Liquidation.4的Sql语句!";
                return null;
            }

            sql = string.Format(sql, year, month, type);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }

            try
            {
                FS.FrameWork.Models.NeuFileInfo obj = null;
                al = new ArrayList();
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuFileInfo();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.Memo = this.Reader[2].ToString();
                    obj.User01 = this.Reader[3].ToString();
                    obj.User02 = this.Reader[4].ToString();
                    obj.User03 = this.Reader[5].ToString();
                    obj.FilePath = this.Reader[6].ToString();
                    obj.FileFullPath = this.Reader[7].ToString();
                    al.Add(obj);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                al = null;
                return null;
            }

            return al;
        }

        public int UpdateCancelLiquidInfo(FS.FrameWork.Models.NeuObject obj)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("SOC.Local.BeiJiao.YD.Liquidation.5", ref sql) == -1)
            {
                this.Err = "获得非药品流水号查询字段SOC.Local.BeiJiao.YD.Liquidation.5出错!";

                return -1;
            }

            sql = string.Format(sql, obj.ID, obj.Name, obj.Memo, obj.User01, obj.User02);

            return this.ExecNoQuery(sql);
        }

        public int UpdateLiquidInfoTransID(FS.FrameWork.Models.NeuObject obj, FS.FrameWork.Models.NeuObject commObj)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("SOC.Local.BeiJiao.YD.Liquidation.6", ref sql) == -1)
            {
                this.Err = "获得非药品流水号查询字段SOC.Local.BeiJiao.YD.Liquidation.6出错!";

                return -1;
            }

            sql = string.Format(sql, obj.ID, obj.Name, obj.Memo, commObj.ID, commObj.Name);

            return this.ExecNoQuery(sql);
        }

        
    }
}
