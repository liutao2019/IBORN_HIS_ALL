using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace FS.SOC.Local.PubReport.BizLogic
{
    /// <summary>
    /// PublicReport 的摘要说明。
    /// </summary>
    public class PubReport : FS.FrameWork.Management.Database
    {
        public PubReport()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        private ArrayList myPubReport(string SQLString)
        {
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            SOC.Local.PubReport.Models.PubReport info;            //返回数组中的摆药单分类信息
            this.ProgressBarText = "正在检索药品信息...";
            this.ProgressBarValue = 0;
            #region
            //ID           VARCHAR2(12)                  序号                                 
            //STATIC_MONTH DATE                          统计月份                             
            //LSH          VARCHAR2(10)                  门诊费用记帐号 或者 住院费用托收单号 
            //BH           VARCHAR2(3)                   公费类别代码                         
            //ZZS          NUMBER(5)    Y                门诊患者人数                         
            //FY1          NUMBER(10,2) Y        0       01床位费                             
            //FY2          NUMBER(10,2) Y        0       02药品费                             
            //FY3          NUMBER(10,2) Y        0       03成药费                             
            //FY4          NUMBER(10,2) Y        0       04化验费                             
            //FY5          NUMBER(10,2) Y        0       05检查费                             
            //FY6          NUMBER(10,2) Y        0       06放射费                             
            //FY7          NUMBER(10,2) Y        0       07治疗费                             
            //FY8          NUMBER(10,2) Y        0       08手术费                             
            //FY9          NUMBER(10,2) Y        0       09输血费                             
            //FY10         NUMBER(10,2) Y        0       10输氧费                             
            //FY11         NUMBER(10,2) Y        0       11接生费                             
            //FY12         NUMBER(10,2) Y        0       12高氧费                             
            //FY13         NUMBER(10,2) Y        0       13MR费                               
            //FY14         NUMBER(10,2) Y        0       14CT费                               
            //FY15         NUMBER(10,2) Y        0       15血透费                             
            //FY16         NUMBER(10,2) Y        0       16诊金费                             
            //FY17         NUMBER(10,2) Y        0       17草药费                             
            //FY18         NUMBER(10,2) Y        0       18特检费                             
            //FY19         NUMBER(10,2) Y        0       19审药费                             
            //FY20         NUMBER(10,2) Y        0       20监护费                             
            //FY51         NUMBER(10,2) Y        0       51省诊费                             
            //JZZJE        NUMBER(10,2) Y        0       合计金额                             
            //SJZJE        NUMBER(10,2) Y        0       实际金额                             
            //FEE_TYPE     VARCHAR2(1)                   1 住院费用 2 门诊费用                
            //ZFBL         NUMBER(10,2) Y        0       自付比例           
            #endregion
            if (this.ExecQuery(SQLString) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    info = new SOC.Local.PubReport.Models.PubReport();
                    try
                    {
                        info.ID = this.Reader[0].ToString();
                        info.Static_Month = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString());
                        info.Bill_No = this.Reader[2].ToString();
                        info.Pact.ID = this.Reader[3].ToString();
                        info.Amount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);
                        info.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                        info.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                        info.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                        info.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                        info.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                        info.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                        info.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                        info.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        info.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                        info.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14]);
                        info.TeZhi = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        info.TeZhiRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);
                        info.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17]);
                        info.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18]);
                        info.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);
                        info.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                        info.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21]);
                        info.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22]);
                        info.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23]);
                        info.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24]);
                        info.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25]);
                        info.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26]);
                        info.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27]);
                        info.Fee_Type = this.Reader[28].ToString();
                        info.Pay_Rate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[30]);
                        info.IsValid = this.Reader[31].ToString();
                        info.Pact.Name = this.Reader[32].ToString();
                        info.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33].ToString());
                        info.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());
                        info.MCardNo = this.Reader[35].ToString();
                        info.SpDrugFeeTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());
                        info.IsInHos = this.Reader[37].ToString();
                        info.OperCode = this.Reader[38].ToString();
                        info.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39]);

                        info.EXT_FLAG = this.Reader[42].ToString();
                        info.EXT_FLAG2 = this.Reader[43].ToString();
                        info.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[51]);
                        info.InvoiceNo = this.Reader[57].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    this.ProgressBarValue++;
                    al.Add(info);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            this.ProgressBarValue = -1;
            return al;
        }

        /// <summary>
        /// 获取托收单集合（住院用）
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        public ArrayList GetPubReportArray(string InpatientNo, string begin, string enddate)
        {
            string strSql = "select * from gfhz_main where inpatient_no = '" + InpatientNo + "' and end_date > to_date('" + begin + "', 'YYYY-MM-DD HH24:MI:SS') and end_date <= to_date('" + enddate + "', 'YYYY-MM-DD HH24:MI:SS')";

            ArrayList al = this.GetPubReportArray(strSql);
            if (al == null)
                return null;
            if (al.Count == 0)
                return null;
            return al;
        }
        /// <summary>
        /// 获取托收单集合（住院用）
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        private ArrayList GetPubReportArray(string SQLString)
        {
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            SOC.Local.PubReport.Models.PubReport info;            //返回数组中的摆药单分类信息
            this.ProgressBarText = "正在检索药品信息...";
            this.ProgressBarValue = 0;
            #region
            //ID           VARCHAR2(12)                  序号                                 
            //STATIC_MONTH DATE                          统计月份                             
            //LSH          VARCHAR2(10)                  门诊费用记帐号 或者 住院费用托收单号 
            //BH           VARCHAR2(3)                   公费类别代码                         
            //ZZS          NUMBER(5)    Y                门诊患者人数                         
            //FY1          NUMBER(10,2) Y        0       01床位费                             
            //FY2          NUMBER(10,2) Y        0       02药品费                             
            //FY3          NUMBER(10,2) Y        0       03成药费                             
            //FY4          NUMBER(10,2) Y        0       04化验费                             
            //FY5          NUMBER(10,2) Y        0       05检查费                             
            //FY6          NUMBER(10,2) Y        0       06放射费                             
            //FY7          NUMBER(10,2) Y        0       07治疗费                             
            //FY8          NUMBER(10,2) Y        0       08手术费                             
            //FY9          NUMBER(10,2) Y        0       09输血费                             
            //FY10         NUMBER(10,2) Y        0       10输氧费                             
            //FY11         NUMBER(10,2) Y        0       11接生费                             
            //FY12         NUMBER(10,2) Y        0       12高氧费                             
            //FY13         NUMBER(10,2) Y        0       13MR费                               
            //FY14         NUMBER(10,2) Y        0       14CT费                               
            //FY15         NUMBER(10,2) Y        0       15血透费                             
            //FY16         NUMBER(10,2) Y        0       16诊金费                             
            //FY17         NUMBER(10,2) Y        0       17草药费                             
            //FY18         NUMBER(10,2) Y        0       18特检费                             
            //FY19         NUMBER(10,2) Y        0       19审药费                             
            //FY20         NUMBER(10,2) Y        0       20监护费                             
            //FY51         NUMBER(10,2) Y        0       51省诊费                             
            //JZZJE        NUMBER(10,2) Y        0       合计金额                             
            //SJZJE        NUMBER(10,2) Y        0       实际金额                             
            //FEE_TYPE     VARCHAR2(1)                   1 住院费用 2 门诊费用                
            //ZFBL         NUMBER(10,2) Y        0       自付比例           
            #endregion
            if (this.ExecQuery(SQLString) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    info = new SOC.Local.PubReport.Models.PubReport();
                    try
                    {
                        info.ID = this.Reader[0].ToString();
                        info.Static_Month = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString());
                        info.Pact.ID = this.Reader[2].ToString();
                        info.Pact.Memo = this.Reader[3].ToString();
                        info.Amount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);
                        info.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                        info.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                        info.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                        info.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                        info.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                        info.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                        info.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                        info.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        info.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                        info.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14]);
                        info.TeZhi = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        info.TeZhiRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);
                        info.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17]);
                        info.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18]);
                        info.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);
                        info.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                        info.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21]);
                        info.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22]);
                        info.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23]);
                        info.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24]);
                        info.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25]);
                        info.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26]);
                        info.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27]);
                        info.Fee_Type = this.Reader[28].ToString();
                        info.TeYaoRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[29].ToString());
                        info.Pay_Rate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[30]);
                        info.IsValid = this.Reader[31].ToString();
                        info.Pact.Name = this.Reader[32].ToString();//住院流水号
                        //info.InpatientNo = this.Reader[32].ToString();
                        info.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33].ToString());
                        info.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());
                        info.MCardNo = this.Reader[35].ToString();
                        info.SpDrugFeeTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());
                        info.IsInHos = this.Reader[37].ToString();
                        info.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[38]);
                        info.OperCode = this.Reader[39].ToString();
                        info.Seq = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[44].ToString());
                        info.SortId = this.Reader[45].ToString();
                        info.Name = this.Reader[46].ToString();//姓名
                        info.WorkName = this.Reader[47].ToString();
                        info.WorkCode = this.Reader[48].ToString();
                        info.PatientNO = this.Reader[49].ToString();
                        info.OverLimitDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[50]);
                        info.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[51]);
                        info.BedFee_JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[52]);
                        info.BedFee_CengLiu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[53]);
                        info.CompanyPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[54]);
                        info.SelfPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[55]);
                        info.TotalFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[56]);
                        info.InvoiceNo = this.Reader[57].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    this.ProgressBarValue++;
                    al.Add(info);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            this.ProgressBarValue = -1;
            return al;
        }

        private ArrayList myPubReport_other(string SQLString)
        {
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            SOC.Local.PubReport.Models.PubReport info;            //返回数组中的摆药单分类信息
            this.ProgressBarText = "正在检索药品信息...";
            this.ProgressBarValue = 0;
            #region
            //ID           VARCHAR2(12)                  序号                                 
            //STATIC_MONTH DATE                          统计月份                             
            //LSH          VARCHAR2(10)                  门诊费用记帐号 或者 住院费用托收单号 
            //BH           VARCHAR2(3)                   公费类别代码                         
            //ZZS          NUMBER(5)    Y                门诊患者人数                         
            //FY1          NUMBER(10,2) Y        0       01床位费                             
            //FY2          NUMBER(10,2) Y        0       02药品费                             
            //FY3          NUMBER(10,2) Y        0       03成药费                             
            //FY4          NUMBER(10,2) Y        0       04化验费                             
            //FY5          NUMBER(10,2) Y        0       05检查费                             
            //FY6          NUMBER(10,2) Y        0       06放射费                             
            //FY7          NUMBER(10,2) Y        0       07治疗费                             
            //FY8          NUMBER(10,2) Y        0       08手术费                             
            //FY9          NUMBER(10,2) Y        0       09输血费                             
            //FY10         NUMBER(10,2) Y        0       10输氧费                             
            //FY11         NUMBER(10,2) Y        0       11接生费                             
            //FY12         NUMBER(10,2) Y        0       12高氧费                             
            //FY13         NUMBER(10,2) Y        0       13MR费                               
            //FY14         NUMBER(10,2) Y        0       14CT费                               
            //FY15         NUMBER(10,2) Y        0       15血透费                             
            //FY16         NUMBER(10,2) Y        0       16诊金费                             
            //FY17         NUMBER(10,2) Y        0       17草药费                             
            //FY18         NUMBER(10,2) Y        0       18特检费                             
            //FY19         NUMBER(10,2) Y        0       19审药费                             
            //FY20         NUMBER(10,2) Y        0       20监护费                             
            //FY51         NUMBER(10,2) Y        0       51省诊费                             
            //JZZJE        NUMBER(10,2) Y        0       合计金额                             
            //SJZJE        NUMBER(10,2) Y        0       实际金额                             
            //FEE_TYPE     VARCHAR2(1)                   1 住院费用 2 门诊费用                
            //ZFBL         NUMBER(10,2) Y        0       自付比例           
            #endregion
            if (this.ExecQuery(SQLString) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    info = new SOC.Local.PubReport.Models.PubReport();
                    try
                    {
                        info.ID = this.Reader[0].ToString();
                        info.Static_Month = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString());
                        info.Pact.ID = this.Reader[2].ToString();
                        info.Pact.Memo = this.Reader[3].ToString();
                        info.Amount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);
                        info.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                        info.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                        info.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                        info.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                        info.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                        info.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                        info.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                        info.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        info.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                        info.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14]);
                        info.TeZhi = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        info.TeZhiRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);
                        info.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17]);
                        info.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18]);
                        info.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);
                        info.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                        info.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21]);
                        info.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22]);
                        info.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23]);
                        info.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24]);
                        info.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25]);
                        info.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26]);
                        info.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27]);
                        info.Fee_Type = this.Reader[28].ToString();
                        info.TeYaoRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[29].ToString());
                        info.Pay_Rate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[30]);
                        info.IsValid = this.Reader[31].ToString();
                        //info.Pact.Name = this.Reader[32].ToString();
                        info.InpatientNo = this.Reader[32].ToString();
                        info.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33].ToString());
                        info.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());
                        info.MCardNo = this.Reader[35].ToString();
                        info.SpDrugFeeTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());
                        info.IsInHos = this.Reader[37].ToString();
                        info.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[38]);
                        info.OperCode = this.Reader[39].ToString();
                        info.Seq = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[44].ToString());
                        info.SortId = this.Reader[45].ToString();
                        info.Pact.Name = this.Reader[46].ToString();
                        info.Name = this.Reader[47].ToString();//姓名
                        info.User02 = this.Reader[48].ToString();//
                        info.User03 = this.Reader[49].ToString();//结算方式
                        info.WorkName = this.Reader[50].ToString();
                        info.WorkCode = this.Reader[51].ToString();
                        info.PatientNO = this.Reader[52].ToString();
                        //info.TeYaoRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[53].ToString());
                        #region [2010-01-27] zhaozf 修改公医报表添加
                        info.OverLimitDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[53]);
                        info.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[54]);
                        info.BedFee_JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[55]);
                        info.BedFee_CengLiu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[56]);
                        info.CompanyPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[57]);
                        info.SelfPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[58]);
                        info.TotalFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[59]);
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    this.ProgressBarValue++;
                    al.Add(info);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            this.ProgressBarValue = -1;
            return al;
        }

        private ArrayList myPubReport_other_a(string SQLString)
        {
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            SOC.Local.PubReport.Models.PubReport info;            //返回数组中的摆药单分类信息
            this.ProgressBarText = "正在检索药品信息...";
            this.ProgressBarValue = 0;
            #region
            //ID           VARCHAR2(12)                  序号                                 
            //STATIC_MONTH DATE                          统计月份                             
            //LSH          VARCHAR2(10)                  门诊费用记帐号 或者 住院费用托收单号 
            //BH           VARCHAR2(3)                   公费类别代码                         
            //ZZS          NUMBER(5)    Y                门诊患者人数                         
            //FY1          NUMBER(10,2) Y        0       01床位费                             
            //FY2          NUMBER(10,2) Y        0       02药品费                             
            //FY3          NUMBER(10,2) Y        0       03成药费                             
            //FY4          NUMBER(10,2) Y        0       04化验费                             
            //FY5          NUMBER(10,2) Y        0       05检查费                             
            //FY6          NUMBER(10,2) Y        0       06放射费                             
            //FY7          NUMBER(10,2) Y        0       07治疗费                             
            //FY8          NUMBER(10,2) Y        0       08手术费                             
            //FY9          NUMBER(10,2) Y        0       09输血费                             
            //FY10         NUMBER(10,2) Y        0       10输氧费                             
            //FY11         NUMBER(10,2) Y        0       11接生费                             
            //FY12         NUMBER(10,2) Y        0       12高氧费                             
            //FY13         NUMBER(10,2) Y        0       13MR费                               
            //FY14         NUMBER(10,2) Y        0       14CT费                               
            //FY15         NUMBER(10,2) Y        0       15血透费                             
            //FY16         NUMBER(10,2) Y        0       16诊金费                             
            //FY17         NUMBER(10,2) Y        0       17草药费                             
            //FY18         NUMBER(10,2) Y        0       18特检费                             
            //FY19         NUMBER(10,2) Y        0       19审药费                             
            //FY20         NUMBER(10,2) Y        0       20监护费                             
            //FY51         NUMBER(10,2) Y        0       51省诊费                             
            //JZZJE        NUMBER(10,2) Y        0       合计金额                             
            //SJZJE        NUMBER(10,2) Y        0       实际金额                             
            //FEE_TYPE     VARCHAR2(1)                   1 住院费用 2 门诊费用                
            //ZFBL         NUMBER(10,2) Y        0       自付比例           
            #endregion
            if (this.ExecQuery(SQLString) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    info = new SOC.Local.PubReport.Models.PubReport();
                    try
                    {
                        info.ID = this.Reader[0].ToString();
                        info.Static_Month = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString());
                        info.Bill_No = this.Reader[2].ToString();
                        info.Pact.ID = this.Reader[3].ToString();
                        info.Amount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);
                        info.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                        info.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                        info.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                        info.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                        info.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                        info.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                        info.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                        info.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        info.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                        info.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14]);
                        info.TeZhi = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        info.TeZhiRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);
                        info.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17]);
                        info.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18]);
                        info.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);
                        info.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                        info.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21]);
                        info.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22]);
                        info.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23]);
                        info.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24]);
                        info.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25]);
                        info.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26]);
                        info.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27]);
                        info.Fee_Type = this.Reader[28].ToString();
                        info.Pay_Rate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[30]);
                        info.IsValid = this.Reader[31].ToString();
                        info.Pact.Name = this.Reader[32].ToString();
                        info.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33].ToString());
                        info.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());
                        info.MCardNo = this.Reader[35].ToString();
                        info.SpDrugFeeTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());
                        info.IsInHos = this.Reader[37].ToString();
                        info.OperCode = this.Reader[38].ToString();
                        info.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39]);
                        info.User01 = this.Reader[40].ToString();
                        //info.User02=this.Reader[51].ToString();
                        //info.User03=this.Reader[54].ToString();

                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    this.ProgressBarValue++;
                    al.Add(info);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            this.ProgressBarValue = -1;
            return al;
        }

        private ArrayList myPubReport_other_clinic(string SQLString)
        {
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            SOC.Local.PubReport.Models.PubReport info;            //返回数组中的摆药单分类信息
            this.ProgressBarText = "正在检索药品信息...";
            this.ProgressBarValue = 0;
            #region
            //ID           VARCHAR2(12)                  序号                                 
            //STATIC_MONTH DATE                          统计月份                             
            //LSH          VARCHAR2(10)                  门诊费用记帐号 或者 住院费用托收单号 
            //BH           VARCHAR2(3)                   公费类别代码                         
            //ZZS          NUMBER(5)    Y                门诊患者人数                         
            //FY1          NUMBER(10,2) Y        0       01床位费                             
            //FY2          NUMBER(10,2) Y        0       02药品费                             
            //FY3          NUMBER(10,2) Y        0       03成药费                             
            //FY4          NUMBER(10,2) Y        0       04化验费                             
            //FY5          NUMBER(10,2) Y        0       05检查费                             
            //FY6          NUMBER(10,2) Y        0       06放射费                             
            //FY7          NUMBER(10,2) Y        0       07治疗费                             
            //FY8          NUMBER(10,2) Y        0       08手术费                             
            //FY9          NUMBER(10,2) Y        0       09输血费                             
            //FY10         NUMBER(10,2) Y        0       10输氧费                             
            //FY11         NUMBER(10,2) Y        0       11接生费                             
            //FY12         NUMBER(10,2) Y        0       12高氧费                             
            //FY13         NUMBER(10,2) Y        0       13MR费                               
            //FY14         NUMBER(10,2) Y        0       14CT费                               
            //FY15         NUMBER(10,2) Y        0       15血透费                             
            //FY16         NUMBER(10,2) Y        0       16诊金费                             
            //FY17         NUMBER(10,2) Y        0       17草药费                             
            //FY18         NUMBER(10,2) Y        0       18特检费                             
            //FY19         NUMBER(10,2) Y        0       19审药费                             
            //FY20         NUMBER(10,2) Y        0       20监护费                             
            //FY51         NUMBER(10,2) Y        0       51省诊费                             
            //JZZJE        NUMBER(10,2) Y        0       合计金额                             
            //SJZJE        NUMBER(10,2) Y        0       实际金额                             
            //FEE_TYPE     VARCHAR2(1)                   1 住院费用 2 门诊费用                
            //ZFBL         NUMBER(10,2) Y        0       自付比例           
            #endregion
            if (this.ExecQuery(SQLString) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    info = new SOC.Local.PubReport.Models.PubReport();
                    try
                    {
                        info.ID = this.Reader[0].ToString();
                        info.Static_Month = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString());
                        info.Pact.ID = this.Reader[2].ToString();
                        info.User01 = this.Reader[3].ToString();
                        info.Amount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);
                        info.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                        info.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                        info.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                        info.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                        info.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                        info.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                        info.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                        info.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        info.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                        info.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14]);
                        info.TeZhi = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        info.TeZhiRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);
                        info.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17]);
                        info.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18]);
                        info.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);
                        info.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                        info.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21]);
                        info.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22]);
                        info.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23]);
                        info.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24]);
                        info.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25]);
                        info.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26]);
                        info.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27]);
                        info.Fee_Type = this.Reader[28].ToString();
                        info.TeYaoRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[29].ToString());
                        info.Pay_Rate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[30]);
                        info.IsValid = this.Reader[31].ToString();
                        //info.Pact.Name = this.Reader[32].ToString();
                        info.InpatientNo = this.Reader[32].ToString();
                        info.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33].ToString());
                        info.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());
                        info.MCardNo = this.Reader[35].ToString();
                        info.SpDrugFeeTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());
                        info.IsInHos = this.Reader[37].ToString();
                        info.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[38]);
                        info.OperCode = this.Reader[39].ToString();
                        info.Seq = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[44].ToString());
                        info.SortId = this.Reader[45].ToString();
                        info.Pact.Name = this.Reader[46].ToString();
                        info.Name = this.Reader[47].ToString();//姓名
                        info.User02 = this.Reader[48].ToString();//
                        info.User03 = this.Reader[49].ToString();//结算方式
                        info.WorkName = this.Reader[50].ToString();
                        info.WorkCode = this.Reader[51].ToString();
                        info.PatientNO = this.Reader[52].ToString();
                        //info.TeYaoRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[53].ToString());
                        #region [2010-01-27] zhaozf 修改公医报表添加
                        info.OverLimitDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[53]);
                        info.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[54]);
                        info.BedFee_JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[55]);
                        info.BedFee_CengLiu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[56]);
                        info.CompanyPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[57]);
                        info.SelfPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[58]);
                        info.TotalFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[59]);
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    this.ProgressBarValue++;
                    al.Add(info);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            this.ProgressBarValue = -1;
            return al;
 
        }


        private ArrayList myPubReport_other_clinicnew(string SQLString)
        {
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            SOC.Local.PubReport.Models.DuiZhangObj info;           //返回数组中的摆药单分类信息
            this.ProgressBarText = "正在检索药品信息...";
            this.ProgressBarValue = 0;

            if (this.ExecQuery(SQLString) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    info = new SOC.Local.PubReport.Models.DuiZhangObj();
                    try
                    {
                        info.GYInvoiceNo = this.Reader[0].ToString();
                        info.GYOperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1]);
                        info.GYOperCode = this.Reader[2].ToString();
                        info.GYPubFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                        info.GYTotFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);

                        info.SFInvoiceNo = this.Reader[5].ToString();
                        info.SFOperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6]);
                        info.SFOperCode = this.Reader[7].ToString();
                        info.SFPubFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                        info.SFTotFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);

                        info.ComparePubFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                        info.CompareTotFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);

                        info.Pact_Code = this.Reader[12].ToString();
                        info.Pact_Name = this.Reader[13].ToString();
                        info.MCardNo = this.Reader[14].ToString();
                        info.Name = this.Reader[15].ToString();
                        info.Memo = this.Reader[16].ToString();


                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    this.ProgressBarValue++;
                    al.Add(info);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            this.ProgressBarValue = -1;
            return al;


 
        }

        /// <summary>
        /// 时间段内是否已经核对过
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="PatientType">类型 I住院 C门诊</param>
        /// <returns></returns>
        public bool HasStaticBefor(DateTime dtBegin, DateTime dtEnd,string patientType)
        {
            string sql = "select count(*) from FS.GFHZ_MAIN s where s.OPER_DATE between '{0}'  and '{1}' and s.FEE_TYPE = '{2}'";
            sql = string.Format(sql, dtBegin.ToString(), dtEnd.ToString(),patientType);
            string iReturn = this.ExecSqlReturnOne(sql);
            int i = 0;
            try
            {
                i = FS.FrameWork.Function.NConvert.ToInt32(iReturn);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return false;
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SOC.Local.PubReport.Models.PubReport GetPubReport(string InpatientNo, string begin, string enddate)
        {
            string strSql = "select * from gfhz_main where inpatient_no = '" + InpatientNo + "' and end_date > to_date('" + begin + "', 'YYYY-MM-DD HH24:MI:SS') and end_date <= to_date('" + enddate + "', 'YYYY-MM-DD HH24:MI:SS')";

            ArrayList al = this.myPubReport(strSql);
            if (al == null)
                return null;
            if (al.Count == 0)
                return null;
            return al[0] as SOC.Local.PubReport.Models.PubReport;
        }

        public SOC.Local.PubReport.Models.PubReport GetPubReport(string InpatientNo, string staticMonth)
        {
            string strSql = "select * from gfhz_main where inpatient_no = '" + InpatientNo + "' and static_month = to_date('" + staticMonth + "', 'YYYY-MM-DD HH24:MI:SS')";// and end_date <= to_date('"+ enddate+"', 'YYYY-MM-DD HH24:MI:SS')";

            ArrayList al = this.myPubReport(strSql);
            if (al == null)
                return null;
            if (al.Count == 0)
                return null;
            return al[0] as SOC.Local.PubReport.Models.PubReport;
        }

        //		
        //		public SOC.Local.PubReport.Models.PubReport GetPubReport(string InpatientNo,string staticMonth)
        //		{
        //			string strSql = "select * from gfhz_main where inpatient_no = '"+InpatientNo+"' and static_month = to_date('"+ staticMonth+"', 'YYYY-MM-DD HH24:MI:SS')";// and end_date <= to_date('"+ enddate+"', 'YYYY-MM-DD HH24:MI:SS')";
        //
        //			ArrayList al = this.myPubReport(strSql);
        //			if(al==null)return null;
        //			if(al.Count==0)return null;
        //			return  al[0] as SOC.Local.PubReport.Models.PubReport;
        //		}

        //		public ArrayList PubReportList() 
        //		{
        //			string strSelect = "SELECT id as 序号,   static_month as 统计月份,      lsh as 门诊费用记帐号,       bh as 公费类别代码,       zzs as 门诊患者人数,       fy1 as 床位费,       fy2 as 药品费,       fy3 as 成药费,       fy4 as 化验费,       fy5 as 检查费,       fy6 as 放射费,       fy7 as 治疗费,       fy8 as 手术费,       fy9 as 输血费,       fy10 as 输氧费,       fy11 as 接生费,       fy12 as 高氧费,       fy13 as MR费,       fy14 as CT费,       fy15 as 血透费,       fy16 as 诊金费,       fy17 as 草药费,       fy18 as 特检费,       fy19 as 审药费,       fy20 as 监护费,       fy51 as 省诊费,       jzzje as 合计金额,       sjzje as 实际金额,       fee_type as 住院费用,       zfbl   as 自付比例  FROM gfhz_main  ";
        // 
        //			//根据SQL语句取摆药单分类数组并返回数组
        //			return this.myPubReport(strSelect);
        //		}

        /// <summary>
        /// 检索发票
        /// </summary>
        /// <param name="statflag">2未确认，1已确认，0已作废</param>
        /// <returns></returns>



        /// <summary>
        /// 插入一条
        /// </summary>
        /// <param name="info">摆药单分类实体</param>
        /// <returns></returns>
        public int InsertPubReport(SOC.Local.PubReport.Models.PubReport info)
        {
            string strSql = "";

            strSql = @"INSERT INTO gfhz_main   ( id, static_month,lsh, bh, zzs,
            fy1,fy2,fy3, fy4,fy5,fy6,fy7, fy8,fy9, fy10,fy11,fy12,fy13,fy14,fy15,fy16,fy17,fy18, fy19,fy20,fy51,
            jzzje,sjzje,fee_type,pay_rate,is_valid,inpatient_no,
            begin_date,end_date,MCARD_NO,SP_TOT,IN_STATE,
                FEE_FLAG,REP_FLAG,EXT_FLAG,EXT_FLAG2,oper_code,oper_date,seq,sortid,name,work_name,work_code,patientNO,zfbl,
                OVERLIMITDRUGFEE,CANCERDRUGFEE,BEDFEE_JIANHU,BEDFEE_CENGLIU,COMPANYFAY,SELFPAY,TOTALFEE,INVOICE_NO)
                VALUES(  '{0}',to_date('{1}','YYYY-MM-DD HH24:MI:SS'),'{2}', '{3}', {4}, {5},{6},{7},{8},{9},    {10},   {11},   {12},    {13},
                {14}, {15},     {16},      {17},  {18},   {19},  {20},{21},    {22},    {23},   {24},    {25},
                {26},  {27},   '{28}',    {29},    '{30}',   '{31}',  
                to_date('{32}','YYYY-MM-DD HH24:MI:SS'),
                to_date('{33}','YYYY-MM-DD HH24:MI:SS'),
               '{34}',{35},'{36}',
                '{37}','{38}','{39}','{40}','{41}', to_date('{42}','YYYY-MM-DD HH24:MI:SS'),{43},'{44}','{45}','{46}','{47}','{48}',{49},
                {50},{51},{52},{53},{54},{55},{56},'{57}')";//[2010-01-27]50-56为修改公医报表添加
            try
            {

                string[] strParm = myGetParmPubReport(info);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        public string GetSeq(string seq)
        {
            string strSql = "";

            //strSql = " select nextval for " + seq + " from dual ";
            strSql = " select  " + seq + ".nextval  from dual ";

            return this.ExecSqlReturnOne(strSql);

        }

        public int GetPubReortSeq(string invoiceNo)
        {
            string sql = "select max(m.SEQ) from gfhz_main m where m.ID = '{0}'";
            try
            {
                sql = string.Format(sql, invoiceNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            string i = ExecSqlReturnOne(sql);
            int seq = 0;
            try
            {
                seq = FS.FrameWork.Function.NConvert.ToInt32(i);
            }
            catch
            {
                return 1;
            }
            return seq + 1;

        }


        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdatePubReport(SOC.Local.PubReport.Models.PubReport info)
        {
            string strSql = "";
            strSql = "UPDATE gfhz_main    SET     static_month=to_date('{1}','yyyy-mm-dd hh24:mi:ss'),          lsh='{2}',         bh='{3}',       zzs={4},    fy1={5},       fy2={6},        fy3={7},        fy4={8},         fy5={9},         fy6={10}    ,     fy7={11},       fy8={12},        fy9={13},        fy10={14},      fy11={15}," +
                "fy12={16},         fy13={17},         fy14={18},        fy15={19},        fy16={20},      fy17={21},        fy18={22},        fy19={23},         fy20={24},        fy51={25},         jzzje={26},         sjzje={27},         fee_type='{28}',       Pay_Rate={29} ,is_valid = '{30}' , MCARD_NO = '{34}'," +
               " BEGIN_DATE = to_date('{32}','yyyy-mm-dd hh24:mi:ss'), end_date = to_date('{33}','yyyy-mm-dd hh24:mi:ss'),SP_TOT = {35}, IN_STATE = '{36}', FEE_FLAG='{37}', REP_FLAG='{38}', EXT_FLAG='{39}',  EXT_FLAG2='{40}' ,sortid = '{44}',name = '{45}',work_name = '{46}',work_code = '{47}',patientno='{48}',zfbl = {49},"+
               "OVERLIMITDRUGFEE = {50},CANCERDRUGFEE = {51},BEDFEE_JIANHU = {52},BEDFEE_CENGLIU = {53},COMPANYFAY = {54},SELFPAY = {55},TOTALFEE = {56},INVOICE_NO ='{57}'  WHERE id ='{0}' and seq = {43}";

            try
            {
                string[] strParm = myGetParmPubReport(info);  //取参数列表
                strSql = string.Format(strSql, strParm);

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
        /// 更新30%药品费用
        /// </summary>
        /// <param name="ID">序号</param>
        /// <param name="Cost">金额</param>
        /// <returns></returns>
        public int UpdatePubReportForSp(string ID, decimal Cost)
        {
            string strSql = "";
            strSql = "UPDATE gfhz_main     SET    SP_TOT = '{1}' WHERE id ='{0}'";

            try
            {

                strSql = string.Format(strSql, ID, Cost.ToString());

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        public int DeletePubReport(string ID,int seq)
        {
            string strSql = "DELETE FROM gfhz_main   WHERE id = '{0}' and seq = {1}";

            try
            {
                strSql = string.Format(strSql, ID,seq.ToString());

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.PubReport";
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }

        /// <summary>
        /// 根据摆药单分类编码,删除一条记录
        /// </summary>
        /// <param name="ID">摆药单分类编码</param>
        /// <returns></returns>
        public int DeletePubReport(string ID)
        {
            if (IsExist(ID, "1") > 0)
            {
                this.Err = "托收单已出报表,不允许删除.";
                return -1;
            }
            string strSql = "DELETE FROM gfhz_main   WHERE id = '{0}'";

            try
            {
                strSql = string.Format(strSql, ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.PubReport";
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }

        public int DeletePubReport(string Inpatient_no, string StaticMonth, string beginDate)
        {
            if (IsExist(Inpatient_no, StaticMonth, beginDate, "1") > 0)
            {
                this.Err = "托收单已出报表,不允许删除.";
                return -1;
            }

            string strSql = "DELETE FROM gfhz_main   WHERE inpatient_no = '{0}'" +
                " and static_month = to_date('{1}','yyyy-mm-dd') " +
                " and begin_date = to_date('{2}','yyyy-mm-dd hh24:mi:ss')";

            try
            {
                strSql = string.Format(strSql, Inpatient_no, StaticMonth, beginDate);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.PubReport";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        private string[] myGetParmPubReport(SOC.Local.PubReport.Models.PubReport info)
        {

            string[] strParm ={   info.ID,
								 info.Static_Month.ToString(),
								 info.Bill_No,   // 托收单
								 //info.Pact.Memo,  //字头
                                 info.Pact.ID,
								 info.Amount.ToString(),
								 info.Bed_Fee.ToString(),
								 info.YaoPin.ToString() , 
								 info.ChengYao.ToString() ,
								 info.HuaYan.ToString(),
								 info.JianCha.ToString(),
								 info.FangShe.ToString() , 
								 info.ZhiLiao.ToString() , 
								 info.ShouShu.ToString() , 
								 info.ShuXue.ToString() , 
								 info.ShuYang.ToString() ,
								 info.TeZhi.ToString() ,
								 info.TeZhiRate.ToString() , 
								 info.MR.ToString() , 
								 info.CT.ToString() , 
								 info.XueTou.ToString(), 
								 info.ZhenJin.ToString()  , 
								 info.CaoYao.ToString() , 
								 info.TeJian.ToString()  , 
								 info.ShenYao.ToString()  ,
								 info.JianHu.ToString() , 
								 info.ShengZhen.ToString()  , 
								 info.Tot_Cost.ToString()  ,
								 info.Pub_Cost.ToString(),
								 info.Fee_Type  , 
								 info.Pay_Rate.ToString()  , 
								 info.IsValid,
								 info.Pact.Name,  //住院流水号
								 info.Begin.ToString(),
								 info.End.ToString(),
								 info.MCardNo,
                                 info.SpDrugFeeTot.ToString(),
								 info.IsInHos,
				                 info.FEE_FLAG,
				                 info.REP_FLAG,
				                 info.EXT_FLAG,
				                 info.EXT_FLAG2,
                                 info.OperCode,
                                 info.OperDate.ToString(),
                                 info.Seq.ToString(),
                                 info.SortId,
                                 info.Name,
                                 info.WorkName,
                                 info.WorkCode,
                                 info.PatientNO,
                                 info.TeYaoRate.ToString(),
                                 #region [2010-01-27] zhaozf 修改公医报表添加                 
                                 info.OverLimitDrugFee.ToString(),
                                 info.CancerDrugFee.ToString(),
                                 info.BedFee_JianHu.ToString(),
                                 info.BedFee_CengLiu.ToString(),
                                 info.CompanyPay.ToString(),
                                 info.SelfPay.ToString(),
                                 info.TotalFee.ToString(),
                                 info.InvoiceNo.ToString()
                                 #endregion
							 };

            return strParm;
        }

        #region  托收单新增函数
        /// <summary>
        /// 获得患者的最后一次统计时间
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns>null or "" 代表患者没有统计过, DateTime表示最后一次统计时间</returns>
        public string GetLastStaticTime(string inpatientNo, string dtEnd)
        {
            string strSql = "select nvl(max(a.end_date),to_date('1900-01-01 00:00:00','yyyy-mm-dd hh24:mi:ss')) from gfhz_main a where a.inpatient_no = '{0}' and " +
                "static_month <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";

            try
            {
                strSql = string.Format(strSql, inpatientNo, dtEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            return this.ExecSqlReturnOne(strSql);
        }

        /// <summary>
        /// 查找患者在统计月份中托收单是否已经统计或出报表
        /// </summary>
        /// <param name="id">gfhz_main.id</param>		
        /// <param name="rep_flag">报表标志,0未出报表,1已出报表</param>
        /// <returns>null执行错误 "0"没有纪录 >=1 已经有纪录</returns>
        public int IsExist(string id, string rep_flag)
        {
            string strSql = "select count(*)  from gfhz_main a where a.id = '{0}' " +
                "and a.rep_flag='{1}'";
            try
            {
                strSql = string.Format(strSql, id, rep_flag);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            string temp = this.ExecSqlReturnOne(strSql);
            if (temp == null)
            {
                return -1;
            }
            return FS.FrameWork.Function.NConvert.ToInt32(temp);
        }
        /// <summary>
        /// 查找患者再统计月份中是否已经存在.
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始统计时间</param>
        /// <param name="dtEnd">结束统计时间</param>
        /// <returns>null执行错误 "0"没有纪录 >=1 已经有纪录</returns>
        public string IsExist(string inpatientNo, DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = "select count(*)  from gfhz_main a where a.inpatient_no = '{2}' " +
                "and a.static_month >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                "and a.static_month <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString(), inpatientNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            string temp = this.ExecSqlReturnOne(strSql);
            if (temp == null)
            {
                return null;
            }
            return temp;
        }
        /// <summary>
        /// 查找患者在统计月份中托收单是否已经统计或出报表
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始统计时间</param>
        /// <param name="dtEnd">结束统计时间</param>
        /// <param name="rep_flag">报表标志,0未出报表,1已出报表</param>
        /// <returns>null执行错误 "0"没有纪录 >=1 已经有纪录</returns>
        public int IsExist(string inpatientNo, string dtBegin, string dtEnd, string rep_flag)
        {
            string strSql = "select count(*)  from gfhz_main a where a.inpatient_no = '{2}' " +
                "and a.static_month >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                "and a.static_month <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')" +
                "and a.rep_flag='{3}'";
            try
            {
                strSql = string.Format(strSql, dtBegin, dtEnd, inpatientNo, rep_flag);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            string temp = this.ExecSqlReturnOne(strSql);
            if (temp == null)
            {
                return -1;
            }
            return FS.FrameWork.Function.NConvert.ToInt32(temp);
        }
        /// <summary>
        /// 获得患者的本期托收信息
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public DataSet GetPatientFee(string inpatientNo, DateTime dtBegin, DateTime dtEnd)
        {
            //string strSql = "select b.sort_id, a.fee_code, sum(tot_cost),sum(own_cost),sum(pay_cost), sum(pub_cost) " +
            //    "from fin_ipb_feeinfo a, com_dictionary b where a.parent_code = '[父级编码]' and  a.current_code = '[本级编码]' " +
            //    "and a.parent_code = b.parent_code and a.current_code = b.current_code and type = 'MINFEE' and a.fee_code = b.code  " +
            //    "and a.charge_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss') and a.charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss') and a.inpatient_no = '{0}' " +
            //    "group by b.sort_id,a.fee_code order by b.sort_id,a.fee_code";

            string strSql = "select b.sort_id, a.fee_code, sum(tot_cost),sum(own_cost),sum(pay_cost), sum(pub_cost) " +
       "from fin_ipb_feeinfo a, com_dictionary b where  type = 'MINFEE' and a.fee_code = b.code  " +
       "and a.charge_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss') and a.charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss') and a.inpatient_no = '{0}' " +
       "group by b.sort_id,a.fee_code order by b.sort_id,a.fee_code";

            try
            {
                strSql = string.Format(strSql, inpatientNo, dtBegin.ToString(), dtEnd.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }
        /// <summary>
        /// 获得指定月份的查询日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetStaticTime(string year, string month)
        {

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            string strSql = "select * from gfhz_static where year = '" + year + "'" +
                " and month = '" + month + "'";

            if (this.ExecQuery(strSql) == -1)
                return null;

            while (this.Reader.Read())
            {
                obj.ID = this.Reader[0].ToString();
                obj.Memo = this.Reader[1].ToString();
                obj.User01 = this.Reader[2].ToString();
                obj.User02 = this.Reader[3].ToString();
            }
            this.Reader.Close();

            return obj;
        }

        /// <summary>
        /// 获得最近的统计月份
        /// </summary>
        public FS.FrameWork.Models.NeuObject GetLastStaticTime()
        {

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

//////            string strSql = @"select YEAR,MONTH,BEGIN_DATE,END_DATE from gfhz_static g
//////	order by g.YEAR desc,g.MONTH desc 
//////	fetch first 1 rows only ";

            string strSql = @"select * from (select YEAR,MONTH,BEGIN_DATE,END_DATE from gfhz_static g
                            order by g.YEAR desc,g.MONTH desc) where rownum = 1 ";

            if (this.ExecQuery(strSql) == -1)
                return null;

            while (this.Reader.Read())
            {
                obj.ID = this.Reader[0].ToString();
                obj.Memo = this.Reader[1].ToString();
                obj.User01 = this.Reader[2].ToString();
                obj.User02 = this.Reader[3].ToString();
            }
            this.Reader.Close();

            return obj;
        }


        /// <summary>
        /// 插入的统计月份
        /// </summary>
        public int  InsertStaticTime(FS.FrameWork.Models.NeuObject obj)
        {

            string strSql = @"insert into gfhz_static 
	(YEAR,MONTH,BEGIN_DATE,END_DATE)
	values
	('{0}','{1}',to_date('{2}' ,'yyyy-mm-dd hh24:mi:ss'),to_date('{3}' ,'yyyy-mm-dd hh24:mi:ss'))
	 ";
            strSql = string.Format(strSql, obj.ID, obj.Memo, obj.User01, obj.User02);

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新的统计月份
        /// </summary>
        public int UpdateStaticTime(FS.FrameWork.Models.NeuObject obj)
        {

            string strSql = @"	update gfhz_static s
	set
	s.BEGIN_DATE = to_date('{2}' ,'yyyy-mm-dd hh24:mi:ss'),
	s.END_DATE = to_date('{3}' ,'yyyy-mm-dd hh24:mi:ss')
	where  s.YEAR = '{0}'
	and s.MONTH = '{1}'";
            strSql = string.Format(strSql, obj.ID, obj.Memo, obj.User01, obj.User02);

            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// 获得患者的特殊比例药品费用，暂时都为30%
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT GetSpDrugFee(string inpatientNo, DateTime dtBegin, DateTime dtEnd)
        {

            FS.HISFC.Models.Base.FT ft = null;

            string strSql =
                " select sum(tot_cost), sum(own_cost), sum(pub_cost), sum(pay_cost) " +
                " from ( SELECT tot_cost, own_cost,pub_cost,pay_cost,round((pub_cost+pay_cost)*0.30,2) as spPay  " +
                " FROM Fin_Ipb_Feeinfo        WHERE  " +
                " inpatient_no = '{0}'  " +
                " and fee_code in ('001','002','003')   and pay_cost<>0 " +
                " and charge_date>=to_date('{1}','yyyy-mm-dd hh24:mi:ss') " +
                " and charge_date <= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')  " +
                "  )t     where   (spPay=pay_cost)";

            strSql = string.Format(strSql, inpatientNo, dtBegin.ToString(), dtEnd.ToString());

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }
            while (this.Reader.Read())
            {
                ft = new FS.HISFC.Models.Base.FT();
                ft.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
                ft.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1].ToString());
                ft.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
                ft.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
            }

            return ft;
        }

        /// <summary>
        /// 获得当前月份的显示时间和查询时间.
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetStaticTime()
        {
            //获得当前时间
            DateTime now = this.GetDateTimeFromSysDateTime();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            string strSql = "select * from gfhz_static where year = '" + now.Year.ToString() + "'" +
                " and month = '" + now.Month.ToString() + "'";

            if (this.ExecQuery(strSql) == -1)
                return null;

            while (this.Reader.Read())
            {
                obj.ID = this.Reader[0].ToString();
                obj.Memo = this.Reader[1].ToString();
                obj.User01 = this.Reader[2].ToString();
                obj.User02 = this.Reader[3].ToString();
            }
            this.Reader.Close();

            return obj;
        }
        #endregion

        #region 处理公费托收单
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="flag"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public int InsertStaticOLD(FS.HISFC.Models.RADT.PatientInfo p, string flag, DateTime dtBegin, DateTime dtEnd)
        {
            FS.HISFC.BizLogic.Fee.InPatient myFee = new FS.HISFC.BizLogic.Fee.InPatient();
            //Local.Function myFun = new Function();
            SOC.Local.PubReport.BizLogic.PubReport rp = new PubReport();
            FS.HISFC.Models.Base.PactInfo pact = new FS.HISFC.Models.Base.PactInfo();
            FS.HISFC.BizLogic.Fee.PactUnitInfo myPact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            SOC.Local.PubReport.BizLogic.Fee locFee = new Fee();
            locFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            rp.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            myPact.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            myFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isExist = false; //患者的托收信息是否已经存在
            DateTime lastStatic; //患者最后一次统计时间

            DateTime pBeginDate;
            DateTime pEndDate;
            int iReturn = 0;

            ArrayList alFee = new ArrayList();//患者的费用信息;

            if (flag == "0")//月统计
            {
                #region 月统计
                //如果患者的统计信息已经存在那么提示
                string temp = rp.IsExist(p.ID, dtBegin, dtEnd);
                if (temp == null)
                {
                    this.Err = "查询患者是否存在托收单出错!";
                    return -1;
                }
                if (FS.FrameWork.Function.NConvert.ToInt32(temp) > 1)
                {
                    this.Err ="该患者在统计时间段内存在多条托收信息，请合并后再统计!";
                    return -1;
                }
                isExist = FS.FrameWork.Function.NConvert.ToBoolean(temp);

                //判断代码
                if (isExist)
                {
                    SOC.Local.PubReport.Models.PubReport tempReport = new SOC.Local.PubReport.Models.PubReport();
                    tempReport = rp.GetPubReport(p.ID, dtBegin.ToString(), dtEnd.ToString());
                    if (tempReport == null)
                    {
                        this.Err ="获得患者的托收单信息出错!";
                        return -1;
                    }
                    /*
                    DialogResult r = this.Err =p.Name + "的托收信息已经存在，单号为: " + tempReport.Bill_No + "是否重新统计并作废原来的托收单?",
                        "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.No)
                    {
                        return 0;
                    }
                    else
                    {

                        删除作废信息
                        iReturn = rp.DeletePubReport(tempReport.ID);
                        if (iReturn <= 0)
                        {
                            this.Err ="删除作废托收信息出错!" + rp.Err);
                            return -1;
                        }

                    }
                     */
                    this.Err =p.Name + "的托收信息已经存在，单号为: " + tempReport.Bill_No ;
                    return -1;
                }
                //查找患者的最后一次统计时间，如果没有，那么说明患者一致没有打印
                //托说单，开始查询的时间设置为患者的入院日期.
                temp = rp.GetLastStaticTime(p.ID, dtEnd.Date.ToString());
                if (temp == null)
                {
                    this.Err ="查找患者的最后统计时间出错!" + rp.Err;
                    return -1;
                }
                lastStatic = FS.FrameWork.Function.NConvert.ToDateTime(temp);
                if (lastStatic.Year == 1900)//该患者没有统计过,患者的统计时间为患者的入院时间
                {
                    if (p.PVisit.InTime < dtBegin)
                    {
                        if (p.PVisit.InTime < new DateTime(2005, 12, 21, 0, 0, 0))
                        {
                            pBeginDate = new DateTime(2005, 12, 21, 0, 0, 0);
                        }
                        else
                        {
                            pBeginDate = p.PVisit.InTime;
                        }
                    }
                    else
                    {
                        pBeginDate = p.PVisit.InTime;
                    }
                }
                else if (lastStatic < dtBegin)//该患者统计过，但是不是本期统计，那么患者的开始统计时间为上次统计时间
                {
                    pBeginDate = lastStatic.AddSeconds(1);
                }
                else //患者的统计时间为统一的开始时间
                {
                    pBeginDate = dtBegin;
                }
                pEndDate = dtEnd; //如果是月统计，患者的结束统计时间为统一的结束时间.
                #endregion
            }
            else //结算拖收单
            {
                #region 结算拖收单
                string temp = "";
                temp = rp.IsExist(p.ID, dtBegin, dtEnd);
                if (temp == null)
                {
                    this.Err ="查询患者是否存在托收单出错!";
                    return -1;
                }
                if (FS.FrameWork.Function.NConvert.ToInt32(temp) > 1)
                {
                    this.Err ="该患者在统计时间段内存在多条托收信息，请合并后再统计!";
                    return -1;
                }
                isExist = FS.FrameWork.Function.NConvert.ToBoolean(temp);

                //判断代码
                if (isExist)
                {
                   SOC.Local.PubReport.Models.PubReport tempReport = new SOC.Local.PubReport.Models.PubReport();
                    tempReport = rp.GetPubReport(p.ID, dtEnd.Date.ToString());
                    if (tempReport == null)
                    {
                        this.Err ="获得患者的托收单信息出错!";
                        return -1;
                    }

                    /*
                    DialogResult r = this.Err =p.Name + "的托收信息已经存在，单号为: " + tempReport.Bill_No + "是否重新统计并作废原来的托收单?",
                        "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.No)
                    {
                        return 0;
                    }
                    else
                    {
                        //删除作废信息
                        iReturn = rp.DeletePubReport(tempReport.ID);
                        if (iReturn <= 0)
                        {
                            this.Err ="删除作废托收信息出错!" + rp.Err);
                            return -1;
                        }
                    }
                    */
                }

                temp = rp.GetLastStaticTime(p.ID, dtEnd.ToString());
                if (temp == null)
                {
                    this.Err ="查找患者的最后统计时间出错!" + rp.Err;
                    return -1;
                }
                lastStatic = FS.FrameWork.Function.NConvert.ToDateTime(temp);
                if (lastStatic.Year == 1900)//该患者没有统计过,患者的统计时间为患者的入院时间
                {
                    //					if(p.PVisit.InTime < dtBegin)
                    //					{
                    //						pBeginDate = new DateTime(2005,12,21,0,0,0);
                    //					}
                    //					else
                    //					{
                    pBeginDate = p.PVisit.InTime;
                    //					}
                }
                else if (lastStatic < dtBegin)//该患者统计过，但是不是本期统计，那么患者的开始统计时间为上次统计时间
                {
                    pBeginDate = lastStatic.AddSeconds(1);
                }
                else
                {
                    pBeginDate = dtBegin;
                }

                pEndDate = rp.GetDateTimeFromSysDateTime();//统计最末日期为当前时间;
                #endregion
            }

            if (flag == "0")
            {
                //				//重新调整日限定额
                //				if(myFee.AdjustOverLimitFee(p)==-1)
                //				{
                //					t.RollBack();
                //					MessageBox.Show("调整日限额出错"+myFee.Err,"提示");
                //					return -1;
                //				}				
            }
            //在获得本期费用之前调用调整费用函数，插入调整数据 By Maokb 06-03-13
            if (locFee.AdjustPubFeeTial(p, pBeginDate.ToString(), pEndDate.ToString()) == -1)
            {
                this.Err = "调整数据出错!" + locFee.Err;
                return -1;
            }
            //if(pBeginDate < new DateTime(2006,1,22
            //获得患者的本期费用信息;
            DataSet ds = rp.GetPatientFee(p.ID, pBeginDate, pEndDate);

            if (ds == null)
            {
                this.Err ="获得患者费用信息出错!" + rp.Err;
                return -1;
            }

            FS.HISFC.Models.Base.FT ft = rp.GetSpDrugFee(p.ID, pBeginDate, pEndDate);

            SOC.Local.PubReport.Models.PubReport report = new SOC.Local.PubReport.Models.PubReport();
            decimal totPubCost = 0;//实付总额 pub
            decimal totReportCost = 0;//费用总额 pay + pub

            pact = myPact.GetPactUnitInfoByPactCode(p.Pact.ID);
            if (p.ExtendFlag1 == "1")
            {
                pact.Rate.PayRate = p.FT.FTRate.PayRate;
            }
            if (pact == null)
            {
                this.Err ="获得患者费用比例出错!" + myPact.Err;
                return -1;
            }

            if (flag == "0")//月统计则统计月份为 dtEnd的整数日期
            {
                report.Static_Month = dtEnd.Date;//统计月份为最后统计时间.
            }
            else //结算统计
            {
                report.Static_Month = dtEnd.Date;
            }
            report.Bill_No = rp.GetSeq("SEQ_GFHZ_BILLNO");
            if (p.SSN == null || p.SSN == "")
            {
                this.Err =p.Name + "患者(" + p.PID.PatientNO + ")的公费卡号为空，这个患者暂时不处理，请处理卡号后再处理此患者";
                return 0;
            }
            report.Pact = this.GetCardNo(p);
            if (report.Pact.ID == null || report.Pact.ID == "")
            {
                this.Err =p.Name + "患者(" + p.PID.PatientNO + ")的公费卡号不符合规则，这个患者暂时不处理，请处理卡号后再处理此患者";
                return 0;
            }
            report.Fee_Type = "1";//住院费用

            report.IsValid = "1";
            report.Begin = pBeginDate;
            if (p.PVisit.InState.ID.ToString() == "O" || flag == "1" || p.PVisit.InState.ID.ToString() == "B")//结算患者
            {
                #region 结算患者
                report.IsInHos = "0";//出院

                ArrayList alBalance = myFee.QueryBalancesByInpatientNO(p.ID);
                if (alBalance == null)
                {
                    this.Err ="获得结算患者信息出错!";
                    return -1;
                }
                DateTime dtTemp = DateTime.MinValue;

                foreach (FS.HISFC.Models.Fee.Inpatient.Balance b in alBalance)
                {
                    if (b.BalanceType.ID.ToString() == "O")
                    {
                        if (b.BalanceOper.OperTime > dtTemp)
                        {
                            dtTemp = b.BalanceOper.OperTime;
                        }
                    }
                }
                if (dtTemp == DateTime.MinValue)//出院登记打印托收单，没有结算时间
                {
                    pEndDate = p.PVisit.OutTime;
                    report.Amount = (p.PVisit.OutTime.Date - pBeginDate.Date).Days;
                }
                else
                {
                    if (dtTemp > dtEnd && flag == "0")//本期结算患者
                    {

                        pEndDate = dtEnd;//相当于在院
                        report.Amount = (pEndDate.Date - pBeginDate.Date).Days + 1;
                    }
                    else
                    {
                        pEndDate = dtTemp;
                        report.Amount = (p.PVisit.OutTime.Date - pBeginDate.Date).Days;
                    }
                }
                #endregion
            }
            else
            {
                report.Amount = (pEndDate.Date - pBeginDate.Date).Days + 1;
                report.IsInHos = "1"; //在院
            }
            report.End = pEndDate;


            report.ID = rp.GetSeq("SEQ_GFHZ");
            report.Pact.Name = p.ID;
            report.Pay_Rate = pact.Rate.PayRate;

            #region 计算费用明细
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string sortId = "";
                string feeCode = "";
                decimal totCost = 0, ownCost = 0, payCost = 0, pubCost = 0;

                sortId = row[0].ToString();
                feeCode = row[1].ToString();
                totCost = FS.FrameWork.Function.NConvert.ToDecimal(row[2].ToString());
                ownCost = FS.FrameWork.Function.NConvert.ToDecimal(row[3].ToString());
                payCost = FS.FrameWork.Function.NConvert.ToDecimal(row[4].ToString());
                pubCost = FS.FrameWork.Function.NConvert.ToDecimal(row[5].ToString());
                //省诊和本院患者诊金算省诊
                //				if(((p.Pact.ID.ToString().Length == 2 && p.Pact.ID.ToString().Substring(0,1) == "1") ||
                //					p.Pact.ID.Substring(0,1) == "7")&& feeCode == "016")
                if ((report.Pact.ID == "80" || report.Pact.ID == "81" || report.Pact.ID == "82" ||
                    report.Pact.ID == "83" || report.Pact.ID == "90" || report.Pact.ID == "J80" ||
                    report.Pact.ID == "J81" || report.Pact.ID == "J82" || report.Pact.ID == "J83" ||
                    report.Pact.ID == "Y1" || report.Pact.ID == "Y2" || report.Pact.ID == "Y3" || report.Pact.ID == "L2" ||
                    report.Pact.ID == "80" || report.Pact.ID == "90" || report.Pact.ID.Length >= 4) && feeCode == "016")
                {
                    totPubCost += pubCost;
                    totReportCost += pubCost;
                }
                else
                {
                    totPubCost += pubCost;
                    totReportCost += payCost + pubCost;
                }


                switch (sortId)
                {
                    case "1": //床位费
                        report.Bed_Fee += payCost + pubCost;
                        break;
                    case "2": //药品费
                        report.YaoPin += payCost + pubCost;
                        break;
                    case "3": //成药费
                        report.ChengYao += payCost + pubCost;
                        break;
                    case "4": //化验
                        report.HuaYan += payCost + pubCost;
                        break;
                    case "5": //检查
                        report.JianCha += payCost + pubCost;
                        break;
                    case "6": //放射
                        report.FangShe += payCost + pubCost;
                        break;
                    case "7": //治疗
                        report.ZhiLiao += payCost + pubCost;
                        break;
                    case "8"://手术
                        report.ShouShu += payCost + pubCost;
                        break;
                    case "9"://输血
                        report.ShuXue += payCost + pubCost;
                        break;
                    case "10"://输氧
                        report.ShuYang += payCost + pubCost;
                        break;
                    case "11"://接生
                        report.TeZhi += payCost + pubCost;
                        break;
                    case "12"://高氧
                        report.TeZhiRate += payCost + pubCost;
                        break;
                    case "13"://MR
                        report.MR += payCost + pubCost;
                        break;
                    case "14"://CT
                        report.CT += payCost + pubCost;
                        break;
                    case "15"://血透
                        report.XueTou += payCost + pubCost;
                        break;
                    case "16"://诊金
                        if ((report.Pact.ID == "80" || report.Pact.ID == "81" || report.Pact.ID == "82" ||
                            report.Pact.ID == "83" || report.Pact.ID == "90" || report.Pact.ID == "J80" ||
                            report.Pact.ID == "J81" || report.Pact.ID == "J82" || report.Pact.ID == "J83" ||
                            report.Pact.ID == "Y1" || report.Pact.ID == "Y2" || report.Pact.ID == "Y3" || report.Pact.ID == "L2" ||
                            report.Pact.ID == "80" || report.Pact.ID == "90" || report.Pact.ID.Length >= 4) && feeCode == "016")
                        {
                            report.ShengZhen += pubCost;
                        }
                        else
                        {
                            report.ZhenJin += payCost + pubCost;
                        }
                        break;
                    case "17"://草药
                        report.CaoYao += payCost + pubCost;
                        break;
                    case "18"://特检
                        report.TeJian += payCost + pubCost;
                        break;
                    case "19"://审药
                        report.ShenYao += payCost + pubCost;
                        break;
                    case "20"://监护
                        report.JianHu += payCost + pubCost;
                        break;
                    case "41"://省诊
                        report.ShengZhen += payCost + pubCost;//Modify by Maokb
                        break;
                    case "36"://造影剂
                        report.JianCha += payCost + pubCost;
                        break;
                    case "42":
                        report.JianCha += payCost + pubCost;
                        break;
                    case "43":
                        report.JianCha += payCost + pubCost;
                        break;
                    case "48":
                        report.JianCha += payCost + pubCost;
                        break;
                    case "47":
                        report.ZhiLiao += payCost + pubCost;
                        break;
                }
            }
            #endregion


            report.Tot_Cost = totReportCost - report.ShengZhen;
            report.Pub_Cost = totPubCost - report.ShengZhen;
            report.MCardNo = p.SSN;
            report.FEE_FLAG = "0";//正常普通帐
            report.REP_FLAG = "0";//未统计报表

            if (ft != null)//有30%费用
            {

                report.SpDrugFeeTot = ft.PubCost + ft.PayCost;//Edit By Maokb

            }

            if (report.Pub_Cost == 0)
            {
                return 0;
            }

            iReturn = rp.InsertPubReport(report);
            if (iReturn <= 0)
            {
                this.Err ="插入托收信息出错!" + rp.Err;
                return -1;
            }

            return 0;
        }

        public int InsertStatic(FS.HISFC.Models.RADT.PatientInfo p, string flag, DateTime dtBegin, DateTime dtEnd)
        {
            FS.HISFC.BizLogic.Fee.InPatient myFee = new FS.HISFC.BizLogic.Fee.InPatient();
            //Local.Function myFun = new Function();
            SOC.Local.PubReport.BizLogic.PubReport rp = new PubReport();
            FS.HISFC.Models.Base.PactInfo pact = new FS.HISFC.Models.Base.PactInfo();
            FS.HISFC.BizLogic.Fee.PactUnitInfo myPact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            SOC.Local.PubReport.BizLogic.Fee locFee = new Fee();
            locFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            rp.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            myPact.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            myFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isExist = false; //患者的托收信息是否已经存在
            DateTime lastStatic; //患者最后一次统计时间

            DateTime pBeginDate;
            DateTime pEndDate;
            int iReturn = 0;

            ArrayList alFee = new ArrayList();//患者的费用信息;

            if (flag == "0")//月统计
            {
                #region 月统计
                //如果患者的统计信息已经存在那么提示
                string temp = rp.IsExist(p.ID, dtBegin, dtEnd);
                if (temp == null)
                {
                    this.Err = "查询患者是否存在托收单出错!";
                    return -1;
                }
                if (FS.FrameWork.Function.NConvert.ToInt32(temp) > 1)
                {
                    this.Err = "该患者在统计时间段内存在多条托收信息，请合并后再统计!";
                    return -1;
                }
                isExist = FS.FrameWork.Function.NConvert.ToBoolean(temp);

                //判断代码
                if (isExist)
                {
                    SOC.Local.PubReport.Models.PubReport tempReport = new SOC.Local.PubReport.Models.PubReport();
                    tempReport = rp.GetPubReport(p.ID, dtBegin.ToString(), dtEnd.ToString());
                    if (tempReport == null)
                    {
                        this.Err = "获得患者的托收单信息出错!";
                        return -1;
                    }
                    DialogResult r = MessageBox.Show(p.Name + "的托收信息已经存在，单号为: " + tempReport.Bill_No + "是否重新统计并作废原来的托收单?",
                           "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.No)
                    {
                        return 0;
                    }
                    else
                    {

                        //删除作废信息
                        iReturn = rp.DeletePubReport(tempReport.ID);
                        if (iReturn <= 0)
                        {
                            MessageBox.Show("删除作废托收信息出错!" + rp.Err);
                            return -1;
                        }

                    }
                }
                //查找患者的最后一次统计时间，如果没有，那么说明患者一致没有打印
                //托说单，开始查询的时间设置为患者的入院日期.
                temp = rp.GetLastStaticTime(p.ID, dtEnd.Date.ToString());
                if (temp == null)
                {
                    this.Err = "查找患者的最后统计时间出错!" + rp.Err;
                    return -1;
                }
                lastStatic = FS.FrameWork.Function.NConvert.ToDateTime(temp);
                if (lastStatic.Year == 1900)//该患者没有统计过,患者的统计时间为患者的入院时间
                {
                    if (p.PVisit.InTime < dtBegin)
                    {
                        if (p.PVisit.InTime < new DateTime(2005, 12, 21, 0, 0, 0))
                        {
                            pBeginDate = new DateTime(2005, 12, 21, 0, 0, 0);
                        }
                        else
                        {
                            pBeginDate = p.PVisit.InTime;
                        }
                    }
                    else
                    {
                        pBeginDate = p.PVisit.InTime;
                    }
                }
                else if (lastStatic < dtBegin)//该患者统计过，但是不是本期统计，那么患者的开始统计时间为上次统计时间
                {
                    pBeginDate = lastStatic.AddSeconds(1);
                }
                else //患者的统计时间为统一的开始时间
                {
                    pBeginDate = dtBegin;
                }
                pEndDate = dtEnd; //如果是月统计，患者的结束统计时间为统一的结束时间.
                #endregion
            }
            else //结算拖收单
            {
                #region 结算拖收单
                string temp = "";
                temp = rp.IsExist(p.ID, dtBegin, dtEnd);
                if (temp == null)
                {
                    this.Err = "查询患者是否存在托收单出错!";
                    return -1;
                }
                if (FS.FrameWork.Function.NConvert.ToInt32(temp) > 1)
                {
                    this.Err = "该患者在统计时间段内存在多条托收信息，请合并后再统计!";
                    return -1;
                }
                isExist = FS.FrameWork.Function.NConvert.ToBoolean(temp);

                //判断代码
                if (isExist)
                {
                    SOC.Local.PubReport.Models.PubReport tempReport = new SOC.Local.PubReport.Models.PubReport();
                    tempReport = rp.GetPubReport(p.ID, dtEnd.Date.ToString());
                    if (tempReport == null)
                    {
                        this.Err = "获得患者的托收单信息出错!";
                        return -1;
                    }

                    DialogResult r = MessageBox.Show(p.Name + "的托收信息已经存在，单号为: " + tempReport.Bill_No + "是否重新统计并作废原来的托收单?",
                        "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.No)
                    {
                        return 0;
                    }
                    else
                    {
                        //删除作废信息
                        iReturn = rp.DeletePubReport(tempReport.ID);
                        if (iReturn <= 0)
                        {
                            MessageBox.Show("删除作废托收信息出错!" + rp.Err);
                            return -1;
                        }
                    }
                }

                temp = rp.GetLastStaticTime(p.ID, dtEnd.ToString());
                if (temp == null)
                {
                    this.Err = "查找患者的最后统计时间出错!" + rp.Err;
                    return -1;
                }
                lastStatic = FS.FrameWork.Function.NConvert.ToDateTime(temp);
                if (lastStatic.Year == 1900)//该患者没有统计过,患者的统计时间为患者的入院时间
                {
                    if (p.PVisit.InTime < new DateTime(2006, 12, 21, 0, 0, 0))
                    {
                        pBeginDate = new DateTime(2006, 12, 21, 0, 0, 0);//new DateTime(2006,12,21,0,0,0);
                    }
                    else
                    {
                        pBeginDate = p.PVisit.InTime;
                    }

                }
                else if (lastStatic < dtBegin)//该患者统计过，但是不是本期统计，那么患者的开始统计时间为上次统计时间
                {
                    pBeginDate = lastStatic.AddSeconds(1);
                }
                else
                {
                    pBeginDate = dtBegin;
                }

                pEndDate = rp.GetDateTimeFromSysDateTime();//统计最末日期为当前时间;
                #endregion
            }

            if (flag == "0")
            {
                //				//重新调整日限定额
                //				if(myFee.AdjustOverLimitFee(p)==-1)
                //				{
                //					t.RollBack();
                //					MessageBox.Show("调整日限额出错"+myFee.Err,"提示");
                //					return -1;
                //				}				
            }
            //在获得本期费用之前调用调整费用函数，插入调整数据 By Maokb 06-03-13
            if (locFee.AdjustPubFeeTial(p, pBeginDate.ToString(), pEndDate.ToString()) == -1)
            {
                this.Err = "调整数据出错!" + locFee.Err;
                return -1;
            }
            //if(pBeginDate < new DateTime(2006,1,22
            //获得患者的本期费用信息;
            DataSet ds = rp.GetPatientFee(p.ID, pBeginDate, pEndDate);

            if (ds == null)
            {
                this.Err = "获得患者费用信息出错!" + rp.Err;
                return -1;
            }

            FS.HISFC.Models.Base.FT ft = rp.GetSpDrugFee(p.ID, pBeginDate, pEndDate);

            SOC.Local.PubReport.Models.PubReport report = new SOC.Local.PubReport.Models.PubReport();
            decimal totPubCost = 0;//实付总额 pub
            decimal totReportCost = 0;//费用总额 pay + pub

            pact = myPact.GetPactUnitInfoByPactCode(p.Pact.ID);
            if (p.ExtendFlag1 == "1")
            {
                pact.Rate.PayRate = p.FT.FTRate.PayRate;
            }
            if (pact == null)
            {
                this.Err = "获得患者费用比例出错!" + myPact.Err;
                return -1;
            }

            if (flag == "0")//月统计则统计月份为 dtEnd的整数日期
            {
                report.Static_Month = dtEnd.Date;//统计月份为最后统计时间.
            }
            else //结算统计
            {
                report.Static_Month = dtEnd.Date;
            }
            report.Bill_No = rp.GetSeq("SEQ_GFHZ_BILLNO");
            if (p.SSN == null || p.SSN == "")
            {
                this.Err = p.Name + "患者(" + p.PID.PatientNO + ")的公费卡号为空，这个患者暂时不处理，请处理卡号后再处理此患者";
                return 0;
            }
            report.Pact = this.GetCardNo(p);
            if (report.Pact.ID == null || report.Pact.ID == "")
            {
                this.Err = p.Name + "患者(" + p.PID.PatientNO + ")的公费卡号不符合规则，这个患者暂时不处理，请处理卡号后再处理此患者";
                return 0;
            }
            report.Fee_Type = "1";//住院费用

            report.IsValid = "1";
            report.Begin = pBeginDate;
            if (p.PVisit.InState.ID.ToString() == "O" || flag == "1" || p.PVisit.InState.ID.ToString() == "B")//结算患者
            {
                #region 结算患者
                report.IsInHos = "0";//出院

                ArrayList alBalance = myFee.QueryBalancesByInpatientNO(p.ID);
                if (alBalance == null)
                {
                    this.Err = "获得结算患者信息出错!";
                    return -1;
                }
                DateTime dtTemp = DateTime.MinValue;

                foreach (FS.HISFC.Models.Fee.Inpatient.Balance b in alBalance)
                {
                    if (b.BalanceType.ID.ToString() == "O")
                    {
                        if (b.BalanceOper.OperTime > dtTemp)
                        {
                            dtTemp = b.BalanceOper.OperTime;
                        }
                    }
                }
                if (dtTemp == DateTime.MinValue)//出院登记打印托收单，没有结算时间
                {
                    pEndDate = p.PVisit.OutTime;
                    report.Amount = (p.PVisit.OutTime.Date - pBeginDate.Date).Days + 1;
                }
                else
                {
                    if (dtTemp > dtEnd && flag == "0")//本期结算患者
                    {

                        pEndDate = dtEnd;//相当于在院
                        report.Amount = (pEndDate.Date - pBeginDate.Date).Days + 1;
                    }
                    else
                    {
                        pEndDate = dtTemp;
                        report.Amount = (p.PVisit.OutTime.Date - pBeginDate.Date).Days + 1;
                    }
                }
                #endregion
            }
            else
            {
                report.Amount = (pEndDate.Date - pBeginDate.Date).Days + 1;
                report.IsInHos = "1"; //在院
            }
            report.End = pEndDate;


            report.ID = rp.GetSeq("SEQ_GFHZ");
            report.Pact.Name = p.ID;
            report.Pay_Rate = pact.Rate.PayRate;

            #region 计算费用明细
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string sortId = "";
                string feeCode = "";
                decimal totCost = 0, ownCost = 0, payCost = 0, pubCost = 0;

                sortId = row[0].ToString();
                feeCode = row[1].ToString();
                totCost = FS.FrameWork.Function.NConvert.ToDecimal(row[2].ToString());
                ownCost = FS.FrameWork.Function.NConvert.ToDecimal(row[3].ToString());
                payCost = FS.FrameWork.Function.NConvert.ToDecimal(row[4].ToString());
                pubCost = FS.FrameWork.Function.NConvert.ToDecimal(row[5].ToString());
                //省诊和本院患者诊金算省诊
                //				if(((p.Pact.ID.ToString().Length == 2 && p.Pact.ID.ToString().Substring(0,1) == "1") ||
                //					p.Pact.ID.Substring(0,1) == "7")&& feeCode == "016")
                if ((report.Pact.ID == "80" || report.Pact.ID == "81" || report.Pact.ID == "82" ||
                    report.Pact.ID == "83" || report.Pact.ID == "90" || report.Pact.ID == "J80" ||
                    report.Pact.ID == "J81" || report.Pact.ID == "J82" || report.Pact.ID == "J83" ||
                    report.Pact.ID == "Y1" || report.Pact.ID == "Y2" || report.Pact.ID == "Y3" || report.Pact.ID == "L2" ||
                    report.Pact.ID == "80" || report.Pact.ID == "90" || report.Pact.ID.Length >= 4) && feeCode == "008")
                {
                    totPubCost += pubCost;
                    totReportCost += pubCost;
                }
                else
                {
                    totPubCost += pubCost;
                    totReportCost += payCost + pubCost;
                }


                switch (feeCode)
                {
                    case "004": //床位费
                        report.Bed_Fee += payCost + pubCost;
                        break;
                    case "006"://审药
                    case "001": //药品费
                        report.YaoPin += payCost + pubCost;
                        break;
                    case "002": //成药费
                        report.ChengYao += payCost + pubCost;
                        break;
                    case "026": //化验
                        report.HuaYan += payCost + pubCost;
                        break;
                    case "016": //检查					
                    case "021"://核医学检查	
                    case "046":
                    case "047":
                        report.JianCha += payCost + pubCost;
                        break;
                    case "014"://x光					
                        report.FangShe += payCost + pubCost;
                        break;
                    case "005":
                    case "011":
                    case "022":
                    case "012":
                    case "015":
                    case "034":
                    case "053":
                    case "055":
                    case "040":
                    case "013": //治疗
                        report.ZhiLiao += payCost + pubCost;
                        break;
                    case "030":
                    case "028"://手术
                        report.ShouShu += payCost + pubCost;
                        break;
                    case "037"://输血
                        report.ShuXue += payCost + pubCost;
                        break;
                    case "038"://输氧
                        report.ShuYang += payCost + pubCost;
                        break;
                    //					case "11"://接生
                    //						report.JieSheng += payCost + pubCost;
                    //						break;
                    //					case "12"://高氧
                    //						report.GaoYang += payCost + pubCost;
                    //						break;
                    case "023"://MR
                        report.MR += payCost + pubCost;
                        break;
                    case "024"://CT
                        report.CT += payCost + pubCost;
                        break;
                    case "039"://血透
                        report.XueTou += payCost + pubCost;
                        break;
                    case "008"://诊金
                        if ((report.Pact.ID == "80" || report.Pact.ID == "81" || report.Pact.ID == "82" ||
                            report.Pact.ID == "83" || report.Pact.ID == "90" || report.Pact.ID == "J80" ||
                            report.Pact.ID == "J81" || report.Pact.ID == "J82" || report.Pact.ID == "J83" || report.Pact.ID == "84" ||
                            report.Pact.ID == "Y1" || report.Pact.ID == "Y2" || report.Pact.ID == "Y3" || report.Pact.ID == "L2" ||
                            report.Pact.ID == "80" || report.Pact.ID == "90" || report.Pact.ID.Length >= 4) && feeCode == "008")
                        {
                            report.ShengZhen += pubCost;
                        }
                        else
                        {
                            report.ZhenJin += payCost + pubCost;
                        }
                        break;
                    case "003"://草药
                        report.CaoYao += payCost + pubCost;
                        break;//					
                    //					case "006"://审药
                    case "009": //护理费 呵呵偷点懒，原本report.ShenYao 统计的数据是：006自付药+009护理费；现在把自付药费归入西药费中，ShenYao仅仅包含护理费
                        report.ShenYao += payCost + pubCost;
                        break;
                    case "010"://监护
                        report.JianHu += payCost + pubCost;
                        break;
                    case "41"://省诊
                        report.ShengZhen += payCost + pubCost;//Modify by Maokb
                        break;
                    default://其他
                        report.TeJian += payCost + pubCost;
                        break;
                    //					case "36"://造影剂
                    //						report.JianCha += payCost + pubCost;
                    //						break;
                    //					case "42":
                    //						report.JianCha += payCost + pubCost;
                    //						break;
                    //					case "43":
                    //						report.JianCha += payCost + pubCost;
                    //						break;
                    //					case "48":
                    //						report.JianCha += payCost + pubCost;
                    //						break;
                    //					case "47":
                    //						report.ZhiLiao += payCost + pubCost;
                    //						break;
                }
            }
            #endregion


            report.Tot_Cost = totReportCost - report.ShengZhen;
            report.Pub_Cost = totPubCost - report.ShengZhen;
            report.MCardNo = p.SSN;
            report.FEE_FLAG = "0";//正常普通帐
            report.REP_FLAG = "0";//未统计报表

            if (ft != null)//有30%费用
            {

                report.SpDrugFeeTot = ft.PubCost + ft.PayCost;//Edit By Maokb

            }

            if (report.Pub_Cost == 0)
            {
                return 0;
            }
            report.EXT_FLAG = p.Patient.Sex.User01;  //高检
            report.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(p.Patient.Sex.User02);  //肿瘤
            iReturn = rp.InsertPubReport(report);
            if (iReturn <= 0)
            {
                this.Err = "插入托收信息出错!" + rp.Err;
                return -1;
            }

            return 0;
        }


        #endregion
        #region 公费报表从托收单中取数据函数
        /// <summary>
        ///  根据卡号和是否退休选择省公医患者
        /// </summary>
        /// <param name="begin">本月结束时间</param>
        /// <param name="end">本月开始时间</param>
        /// <param name="CardNO">卡号</param>
        /// <param name="IsRetire">1在职2退休</param>
        /// <returns></returns>
        public DataSet GetDetailByCardForPro(string month, string CardNO, string IsRetire)
        {
            DataSet ds = new DataSet();
            string strSql1 = "";
            //省单位
                strSql1 = @"select g.id,g.static_month,g.lsh,g.bh,g.zzs,g.fy1,g.fy2,g.fy3,g.fy4,g.fy5,g.fy6,
g.fy7,g.fy8,g.fy9,g.fy10,g.fy11,g.fy12,g.fy13,g.fy14,g.fy15,g.fy16,g.fy17,
g.fy18,g.fy19,g.fy20,g.fy51,g.jzzje,g.sjzje,g.fee_type,g.zfbl,g.pay_rate,
g.is_valid,g.inpatient_no,g.begin_date,g.end_date,g.mcard_no,g.sp_tot,'0',
g.NAME,g.WORK_NAME,g.PATIENTNO
from gfhz_main g
where  Fee_type='2' and BH='{1}' 
and g.static_month=to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
order by PATIENTNO";
            //else if (IsRetire == "2")
            //{//省离退休
            //    strSql1 = " select g.id,g.static_month,g.lsh,g.bh,g.zzs,g.fy1,g.fy2,g.fy3,g.fy4,g.fy5,g.fy6,g.fy7,g.fy8,g.fy9,g.fy10,g.fy11,g.fy12,g.fy13,g.fy14,g.fy15,g.fy16,g.fy17,g.fy18,g.fy19,g.fy20,g.fy51,g.jzzje,g.sjzje,g.fee_type,g.zfbl,g.pay_rate,g.is_valid,g.inpatient_no,g.begin_date,g.end_date,g.mcard_no,g.sp_tot,g.in_state,i.name,i.work_name,i.patient_no  " +//i.in_state,
            //        " from gfhz_main g,fin_ipr_inmaininfo i where i.inpatient_no=g.inpatient_no " +
            //        " and i.pact_code in('13','8000','8100','8200','8300','J8000','J8300','J8100','J8200','SQ80','SQ81','SQ82','SQ83') and Fee_type='2' and BH='" + CardNO + "'" +
            //        " and g.static_month=to_date('" + month + "','yyyy-mm-dd hh24:mi:ss')" +
                
            //        " order by i.patient_no";
            //}
            //else if (IsRetire == "3")
            //{//省特殊帐
            //    strSql1 = " select g.id,g.static_month,g.lsh,g.bh,g.zzs,g.fy1,g.fy2,g.fy3,g.fy4,g.fy5,g.fy6,g.fy7,g.fy8,g.fy9,g.fy10,g.fy11,g.fy12,g.fy13,g.fy14,g.fy15,g.fy16,g.fy17,g.fy18,g.fy19,g.fy20,g.fy51,g.jzzje,g.sjzje,g.fee_type,g.zfbl,g.pay_rate,g.is_valid,g.inpatient_no,g.begin_date,g.end_date,g.mcard_no,g.sp_tot,g.in_state,i.name,i.work_name,i.patient_no  " +//i.in_state,
            //        " from gfhz_main g,fin_ipr_inmaininfo i where i.inpatient_no=g.inpatient_no " +
            //        " and Fee_type='2' and BH in (select distinct(e.pact_head) from com_pactcompare e where e.paykind_code='03' and e.parent_pact='PP' and e.pact_head<>'NF')" +
            //        " and fee_flag='1'" +
            //        " and g.static_month=to_date('" + month + "','yyyy-mm-dd hh24:mi:ss')" +
            //        " order by i.patient_no";
            //}
            strSql1 = string.Format(strSql1, month, CardNO);

            if (this.ExecQuery(strSql1, ref ds) < 0)
            {
                this.Err = "查询出错";
                return null;
            }
            return ds;

        }

        /// <summary>
        /// 本院职工
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="pact">合同单位</param>
        /// <param name="memo">A上半年B下半年，F全年</param>
        /// <returns></returns>
        public DataSet GetDetailForThisHos(string month, string pact, string memo)
        {
            DataSet ds = new DataSet();
            string strSql1 = "";
            //本院职工，本院离退休
            if (memo == "MMM" || memo == "T" || memo == "R")
            {
                strSql1 = " select g.id,g.static_month,g.lsh,g.bh,g.zzs,g.fy1,g.fy2,g.fy3,g.fy4,g.fy5,g.fy6,g.fy7,g.fy8,g.fy9,g.fy10,g.fy11,g.fy12,g.fy13,g.fy14,g.fy15,g.fy16,g.fy17,g.fy18,g.fy19,g.fy20,g.fy51,g.jzzje,g.sjzje,g.fee_type,g.zfbl,g.pay_rate,g.is_valid,g.inpatient_no,g.begin_date,g.end_date,g.mcard_no,g.sp_tot,g.in_state,i.name,i.work_name,i.patient_no  " +//,i.in_state
                    " from gfhz_main g,fin_ipr_inmaininfo i where i.inpatient_no=g.inpatient_no " +
                    " and i.pact_code in ('" + pact + "') and Fee_type='1' " +
                    " and g.static_month=to_date('" + month + "','yyyy-mm-dd hh24:mi:ss')" +
                 
                    " order by i.patient_no";
            }
            else
            {//本院家属
                strSql1 = " select g.id,g.static_month,g.lsh,g.bh,g.zzs,g.fy1,g.fy2,g.fy3,g.fy4,g.fy5,g.fy6,g.fy7,g.fy8,g.fy9,g.fy10,g.fy11,g.fy12,g.fy13,g.fy14,g.fy15,g.fy16,g.fy17,g.fy18,g.fy19,g.fy20,g.fy51,g.jzzje,g.sjzje,g.fee_type,g.zfbl,g.pay_rate,g.is_valid,g.inpatient_no,g.begin_date,g.end_date,g.mcard_no,g.sp_tot,g.in_state,i.name,i.work_name,i.patient_no  " +//i.in_state
                    " from gfhz_main g,fin_ipr_inmaininfo i where i.inpatient_no=g.inpatient_no " +
                    " and i.pact_code in('" + pact + "') and Fee_type='1' " +
                    " and substr(g.mcard_no,1,1)='" + memo + "'" +
                    " and g.static_month=to_date('" + month + "','yyyy-mm-dd hh24:mi:ss')" +
                    " order by i.patient_no";
            }
            if (this.ExecQuery(strSql1, ref ds) < 0)
            {
                this.Err = "查询出错";
                return null;
            }
            return ds;
        }
        /// <summary>
        /// 查找特约单位费用
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataSet GetDetailForTT(string month)
        {
            DataSet ds = new DataSet();
            string strSql = "";
            strSql = "select g.id,g.static_month,g.lsh,g.bh,g.zzs,g.fy1,g.fy2,g.fy3,g.fy4,g.fy5,g.fy6,g.fy7,g.fy8,g.fy9,g.fy10,g.fy11,g.fy12,g.fy13,g.fy14,g.fy15,g.fy16,g.fy17,g.fy18,g.fy19,g.fy20,g.fy51,g.jzzje,g.sjzje,g.fee_type,g.zfbl,g.pay_rate,g.is_valid,g.inpatient_no,g.begin_date,g.end_date,g.mcard_no,g.sp_tot,g.in_state,i.name,i.work_name,i.patient_no,d.simple_name " +//i.in_state
                  " from gfhz_main g,fin_ipr_inmaininfo i,com_department d where i.inpatient_no=g.inpatient_no  " +
                  "  and d.dept_code=i.dept_code " +
                  " and g.bh=g.mcard_no  and length(g.bh)=3 and Fee_type='1' " +
                  " and g.static_month=to_date('" + month + "','yyyy-mm-dd hh24:mi:ss')" +
                  " order by i.patient_no";
            if (this.ExecQuery(strSql, ref ds) < 0)
            {
                this.Err = "查询出错";
                return null;
            }
            return ds;
        }
        /// <summary>
        /// 市公医
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public DataSet GetDetailByCardForCity(string static_month, string CardNO)
        {
            DataSet ds = new DataSet();
            string strSql = @"select g.id,g.static_month,g.lsh,g.bh,g.zzs,g.fy1,g.fy2,g.fy3,g.fy4,g.fy5,g.fy6,g.fy7,g.fy8,g.fy9,g.fy10,g.fy11,g.fy12,g.fy13,g.fy14,g.fy15,g.fy16,g.fy17,g.fy18,g.fy19,g.fy20,g.fy51,g.jzzje,g.sjzje,g.fee_type,
g.zfbl,g.pay_rate,g.is_valid,g.inpatient_no,g.begin_date,g.end_date,g.mcard_no,g.sp_tot,g.in_state,g.name,g.work_name,g.PATIENTNO,   
case i.day_limit
when 200 then 'N'
else 'Y' end as ISS,
i.diag_name  
from gfhz_main g left join fin_ipr_inmaininfo i on i.inpatient_no=g.inpatient_no   
where g.fee_type='2' and BH='{1}' 
and g.IS_VALID = '1'
and g.static_month=to_date('{0}','yyyy-mm-dd hh24:mi:ss')
order by i.patient_no";

            strSql = string.Format(strSql, static_month, CardNO);
            if (this.ExecQuery(strSql, ref ds) < 0)
            {
                this.Err = "查询出错";
                return null;
            }
            return ds;
        }
        /// <summary>
        /// 区公医
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public DataSet GetDetailForArea(string staticMonth, string statID)
        {
            DataSet ds = new DataSet();
            string strSql = "";
            strSql = "";
            strSql = @"select g.id,g.static_month,g.lsh,g.bh,g.zzs,g.fy1,g.fy2,g.fy3,g.fy4,g.fy5,g.fy6,g.fy7,g.fy8,g.fy9,g.fy10,g.fy11,g.fy12,g.fy13,g.fy14,g.fy15,g.fy16,g.fy17,g.fy18,g.fy19,g.fy20,g.fy51,g.jzzje,g.sjzje,g.fee_type,g.zfbl,g.pay_rate,g.is_valid,g.inpatient_no,g.begin_date,g.end_date,g.mcard_no,g.sp_tot,
g.in_state,g.name,g.work_name,g.patientno, s.memo  
from gfhz_main g,com_statlevel s
where  s.report_code='4'
and s.stat_code = '{1}' 
and BH=s.card_no   
and Fee_type='2' 
and is_valid = '1'  
and g.static_month = to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
order by g.PATIENTNO";
            strSql = string.Format(strSql, staticMonth, statID);
            if (this.ExecQuery(strSql, ref ds) < 0)
            {
                this.Err = "查询出错";
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 本院职工
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public DataSet GetDetailForLocal(string staticMonth, string statID)
        {
            DataSet ds = new DataSet();
            string strSql = "";
            strSql = "";
            strSql = @"select g.id,g.static_month,g.lsh,g.bh,g.zzs,g.fy1,g.fy2,g.fy3,g.fy4,g.fy5,g.fy6,g.fy7,g.fy8,g.fy9,g.fy10,g.fy11,g.fy12,g.fy13,g.fy14,g.fy15,g.fy16,g.fy17,g.fy18,g.fy19,g.fy20,g.fy51,g.jzzje,g.sjzje,g.fee_type,g.zfbl,g.pay_rate,g.is_valid,g.inpatient_no,g.begin_date,g.end_date,g.mcard_no,g.sp_tot,
g.in_state,g.name,g.work_name,g.patientno, s.memo  
from gfhz_main g,com_statlevel s
where  s.report_code='6'
and s.stat_code = '{1}' 
and BH=s.card_no   
and Fee_type='2' 
and is_valid = '1'  
and g.static_month = to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
order by g.PATIENTNO";
            strSql = string.Format(strSql, staticMonth, statID);
            if (this.ExecQuery(strSql, ref ds) < 0)
            {
                this.Err = "查询出错";
                return null;
            }
            return ds;
        }

        #endregion

        #region 公费总表(包含门诊费用)相关
        public DataSet GetSumPubFee(string repDate)
        {
            DataSet ds = new DataSet();
            string strSql = "";
            try
            {
                strSql = "select  g.bh as bh, count(1) as ZZS ,sum(g.fy1)as fy1,sum(fy2)as fy2, sum(fy3) as fy3,sum(fy4) as fy4,sum(fy5) as fy5," +
                    " sum(fy6) as fy6,sum(fy7) as fy7, sum(fy8) as fy8,sum(fy9) as fy9,sum(fy10)as fy10,sum(fy11) as fy11,sum(fy12) as fy12,sum(fy13) as fy13," +
                    " sum(fy14) as fy14,sum(fy15) as fy15,sum(fy16) as fy16,sum(fy17)as fy2,sum(fy18) as fy18,sum(fy19) as fy19,sum(fy20)as fy20,sum(fy51) as fy51, " +
                    " sum(jzzje) as jzzje,sum(sjzje)as sjzje,sum(g.zzs) ts,sum(sp_tot) as fy_b30" +
                    " from gfhz_main g " +
                    " where g.static_month=to_date('{0}','yyyy-mm-dd') " +
                    " and g.fee_type='1' and length(g.bh)<=3 " +
                    " group by bh";
                strSql = string.Format(strSql, repDate);
                if (this.ExecQuery(strSql, ref ds) == -1)
                {
                    return null;
                }
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 离休
        /// </summary>
        /// <param name="repDate"></param>
        /// <returns></returns>
        public DataSet GetSumPubFeeLiXiu(string repDate)
        {
            DataSet ds = new DataSet();
            string strSql = "";
            try
            {
                strSql = "select  'L'||g.bh as bh, count(1) as ZZS ,sum(g.fy1)as fy1,sum(fy2)as fy2, sum(fy3) as fy3,sum(fy4) as fy4,sum(fy5) as fy5," +
                    " sum(fy6) as fy6,sum(fy7) as fy7, sum(fy8) as fy8,sum(fy9) as fy9,sum(fy10)as fy10,sum(fy11) as fy11,sum(fy12) as fy12,sum(fy13) as fy13," +
                    " sum(fy14) as fy14,sum(fy15) as fy15,sum(fy16) as fy16,sum(fy17)as fy2,sum(fy18) as fy18,sum(fy19) as fy19,sum(fy20)as fy20,sum(fy51) as fy51, " +
                    " sum(jzzje) as jzzje,sum(sjzje)as sjzje,sum(g.zzs) ts,sum(sp_tot) as fy_b30" +
                    " from gfhz_main g ,fin_ipr_inmainInfo i " +
                    " where g.inpatient_no = i.inpatient_no and (i.pact_code='13' or i.pact_code='8100' or i.pact_code='8200' or i.pact_code='8300' or i.pact_code='J8300' or i.pact_code='J8100' or i.pact_code='J8200') " +
                    " and g.static_month=to_date('{0}','yyyy-mm-dd') " +
                    " and g.fee_type='1' and length(g.bh)<=3 " +
                    " group by bh";
                strSql = string.Format(strSql, repDate);
                if (this.ExecQuery(strSql, ref ds) == -1)
                {
                    return null;
                }
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 本院
        /// </summary>
        /// <param name="repDate"></param>
        /// <returns></returns>
        public DataSet GetSumPubFeeForBY(string repDate)
        {
            DataSet ds = new DataSet();
            string strSql = "";
            try
            {
                strSql = "select  g.DW as bh, count(1) as zzs ,sum(g.fy1) as fy1, sum(fy2) as fy2,sum(fy3) as f3, sum(fy4) as fy4 ,sum(fy5) as fy5," +
                    " sum(fy6) as fy6,sum(fy7) as fy7, sum(fy8) as fy8,sum(fy9) as fy9,sum(fy10)as fy10,sum(fy11) as fy11, sum(fy12) as fy12,sum(fy13) as fy13," +
                    " sum(fy14) as fy14,sum(fy15) as fy15,sum(fy16) as fy16,sum(fy17) as fy17, sum(fy18) as fy18,sum(fy19) as fy19,sum(fy20) as fy20,sum(fy51) as fy51," +
                    " sum(jzzje) as jzzje,sum(sjzje)as sjzje ,sum(g.zzs) ts,sum(sp_tot) as fy_b30" +
                    " from (select decode(substr(upper(t.bh),0,1),'T','BY_T','V','BY_T','A','BY_A','B','BY_B','L','BY_R','F','BY_F','BY_0') as DW,t.*" +
                    " from    gfhz_main t " +
                    " where length(t.bh)>3 " +
                    " and t.static_month=to_date('{0}','yyyy-mm-dd') " +
                    " and t.fee_type='1')g " +
                    " group by g.DW ";
                strSql = string.Format(strSql, repDate);
                if (this.ExecQuery(strSql, ref ds) == -1)
                {
                    return null;
                }
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 检查时否已经导入过费用
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public int GetExistMonth(string day)
        {
            string temp;
            string strSql = "select count(1) from gfhz_main " +
                " where static_month = to_date('{0}','yyyy-mm-dd hh24:mi:ss')" +
                " and fee_type='2'";
            try
            {
                strSql = string.Format(strSql, day);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            temp = this.ExecSqlReturnOne(strSql);
            if (temp != null || temp != "")
            {
                return FS.FrameWork.Function.NConvert.ToInt32(temp);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 删除已导入的数据
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public int DeleteMonthData(string day)
        {
            string strSql = "delete from gfhz_main " +
                " where static_month = to_date('{0}','yyyy-mm-dd hh24:mi:ss')" +
                " and fee_type='2'";
            try
            {
                strSql = string.Format(strSql, day);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// 获取省公医门诊住院费用
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="IsRetired"></param>
        /// <param name="IsMenZhen"></param>
        /// <returns></returns>
        public DataSet GetSumForSheng(string begin, string end, bool IsRetired, string IsMenZhen)
        {
            string strSql = "";
            DataSet ds = new DataSet();
            if (IsRetired)//省离休
            {
                if (IsMenZhen == "2")//门诊费用
                {
                    strSql = "select g.bh, sum(zzs) as num, sum(g.fy2+g.fy3+g.fy17+g.sp_tot) as yaofei," +
                        "sum(g.fy4+g.fy5+g.fy6+ g.fy7+g.fy8+g.fy9)as zhiliao," +
                        "sum(g.fy10+g.fy11+g.fy12+g.fy13+g.fy14+g.fy15+g.fy18+g.fy19+g.fy20)as jiancha," +
                        "sum(g.fy1) as bedfee, sum(g.fy16+51) as zhenjin, sum(g.jzzje) as jzzje," +
                        "sum(jzzje-sjzje)as zifu, sum(g.sjzje) as sjje      from gfhz_main g " +
                        " where g.static_month>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') and g.static_month<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') " +
                        " and g.bh in('80','81','82','83','J80','J81','J82','J83','90') " +
                        " and  g.jzzje=g.sjzje " +
                        " and g.fee_type='{2}'  group by g.bh";
                }
                else
                {
                    strSql = "select g.bh, count(1) as num,sum(zzs) as days ,sum(g.fy2+g.fy3+g.fy17+g.sp_tot) as yaofei," +
                        "sum(g.fy4+g.fy5+g.fy6+ g.fy7+g.fy8+g.fy9)as zhiliao," +
                        "sum(g.fy10+g.fy11+g.fy12+g.fy13+g.fy14+g.fy15+g.fy18+g.fy19+g.fy20)as jiancha," +
                        "sum(g.fy1) as bedfee, sum(g.fy16+51) as zhenjin, sum(g.jzzje) as jzzje," +
                        "sum(jzzje-sjzje)as zifu, sum(g.sjzje) as sjje      from gfhz_main g,fin_ipb_inmaininfo i" +
                        " where g.inpatient_no=i.inpatient_no and i.pact_code='13' " +
                        " and g.static_month>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') and g.static_month<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') " +
                        " and g.bh in('80','81','82','83','J80','J81','J82','J83','90') " +
                        " and  g.jzzje=g.sjzje " +
                        " and g.fee_type='{2}'  group by g.bh";
                }
            }
            else
            {
                if (IsMenZhen == "2")
                {
                    strSql = "select g.bh, count(1) as num, sum(g.fy2+g.fy3+g.fy17+g.sp_tot) as yaofei," +
                        "sum(g.fy4+g.fy5+g.fy6+ g.fy7+g.fy8+g.fy9)as zhiliao," +
                        "sum(g.fy10+g.fy11+g.fy12+g.fy13+g.fy14+g.fy15+g.fy18+g.fy19+g.fy20)as jiancha," +
                        "sum(g.fy1) as bedfee, sum(g.fy16+51) as zhenjin, sum(g.jzzje) as jzzje," +
                        "sum(jzzje-sjzje)as zifu, sum(g.sjzje) as sjje      from gfhz_main g " +
                        " where g.static_month>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') and g.static_month<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') " +
                        " and g.bh in('80','81','82','83','J80','J81','J82','J83','90') " +
                        " and g.fee_type='{2}'  group by g.bh";
                }
                else
                {
                    strSql = "select g.bh, count(1) as num, sum(zzs) as days,sum(g.fy2+g.fy3+g.fy17+g.sp_tot) as yaofei," +
                        "sum(g.fy4+g.fy5+g.fy6+ g.fy7+g.fy8+g.fy9)as zhiliao," +
                        "sum(g.fy10+g.fy11+g.fy12+g.fy13+g.fy14+g.fy15+g.fy18+g.fy19+g.fy20)as jiancha," +
                        "sum(g.fy1) as bedfee, sum(g.fy16+51) as zhenjin, sum(g.jzzje) as jzzje," +
                        "sum(jzzje-sjzje)as zifu, sum(g.sjzje) as sjje      from gfhz_main g " +
                        " where g.static_month>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') and g.static_month<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') " +
                        " and g.bh in('80','81','82','83','J80','J81','J82','J83','90') " +
                        " and g.fee_type='{2}'  group by g.bh";
                }
            }
            try
            {
                strSql = string.Format(strSql, begin, end, IsMenZhen);
                if (this.ExecQuery(strSql, ref ds) < 0)
                {
                    this.Err = "查询出错";
                    return null;
                }
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        public DataSet GetGfhzReportCity(string month, string ReportTag)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select r.PACT_HEAD,r.mzrs,r.mzfy1,r.mzfy2,r.mzfy3,r.mzfy4,r.mzfy5,r.mzfy6,
 r.mzfy7,r.mzfy8,r.mzfy9,r.mzfy10,r.mzfy11,r.mzfy12,r.mzfy13,r.mzfy14,r.mzfy15,
 r.mzfy16,r.mzfy17,r.mzfy18,r.mzfy19,r.mzfy20,r.mzfy51,r.mzjzzj,r.mzsjjz,
 r.zyrs,r.zyts,r.zyfy1,r.zyfy2,r.zyfy3,r.zyfy4,r.zyfy5,r.zyfy6,r.zyfy7,r.zyfy8,
 r.zyfy9,r.zyfy10,r.zyfy11,r.zyfy12,r.zyfy13,r.zyfy14,r.zyfy15,r.zyfy16,r.zyfy17,
 r.zyfy18,r.zyfy19,r.zyfy20,r.zyfy51,r.zyjzzj,r.zysjjz,r.tjyf,r.PACT_HEAD as SORT_ID
 
 from gfhz_report r,com_dictionary c
 where c.type = 'GFBBNZT' 
 and c.CODE = r.PACT_HEAD 
 and c.MARK= '{1}'
 and r.tjyf = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
 order by SORT_ID";
                strSql = string.Format(strSql, month, ReportTag);
                this.ExecQuery(strSql, ref ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }


        public DataSet GetGfhzReport(string month, string ReportTag)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select '干部', sum(r.mzrs),sum(r.mzfy1),sum(r.mzfy2),sum(r.mzfy3),sum(r.mzfy4),sum(r.mzfy5),sum(r.mzfy6),
 sum(r.mzfy7),sum(r.mzfy8),sum(r.mzfy9),sum(r.mzfy10),sum(r.mzfy11),sum(r.mzfy12),sum(r.mzfy13),sum(r.mzfy14),
 sum(r.mzfy15),sum(r.mzfy16),sum(r.mzfy17),sum(r.mzfy18),sum(r.mzfy19),sum(r.mzfy20),sum(r.mzfy51),sum(r.mzjzzj),sum(r.mzsjjz),
 sum(r.zyrs),sum(r.zyts),sum(r.zyfy1),sum(r.zyfy2),sum(r.zyfy3),sum(r.zyfy4),sum(r.zyfy5),sum(r.zyfy6),sum(r.zyfy7),sum(r.zyfy8),
 sum(r.zyfy9),sum(r.zyfy10),sum(r.zyfy11),sum(r.zyfy12),sum(r.zyfy13),sum(r.zyfy14),sum(r.zyfy15),sum(r.zyfy16),sum(r.zyfy17),
 sum(r.zyfy18),sum(r.zyfy19),sum(r.zyfy20),sum(r.zyfy51),sum(r.zyjzzj),sum(r.zysjjz),r.tjyf ,'' as sort_id
 from gfhz_report r,com_dictionary c
 where c.type = 'GFBBNZT' 
 and c.CODE = r.PACT_HEAD 
 and c.MARK= '{1}'
 and r.tjyf = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
 and r.PACT_HEAD not like '%2'
 group by r.tjyf  
 union 
select r.PACT_HEAD,r.mzrs,r.mzfy1,r.mzfy2,r.mzfy3,r.mzfy4,r.mzfy5,r.mzfy6,
 r.mzfy7,r.mzfy8,r.mzfy9,r.mzfy10,r.mzfy11,r.mzfy12,r.mzfy13,r.mzfy14,r.mzfy15,
 r.mzfy16,r.mzfy17,r.mzfy18,r.mzfy19,r.mzfy20,r.mzfy51,r.mzjzzj,r.mzsjjz,
 r.zyrs,r.zyts,r.zyfy1,r.zyfy2,r.zyfy3,r.zyfy4,r.zyfy5,r.zyfy6,r.zyfy7,r.zyfy8,
 r.zyfy9,r.zyfy10,r.zyfy11,r.zyfy12,r.zyfy13,r.zyfy14,r.zyfy15,r.zyfy16,r.zyfy17,
 r.zyfy18,r.zyfy19,r.zyfy20,r.zyfy51,r.zyjzzj,r.zysjjz,r.tjyf,r.PACT_HEAD as SORT_ID
 from gfhz_report r,com_dictionary c
 where c.type = 'GFBBNZT' 
 and c.CODE = r.PACT_HEAD 
 and c.MARK= '{1}'
 and r.tjyf = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
   union
 select '合计', sum(r.mzrs),sum(r.mzfy1),sum(r.mzfy2),sum(r.mzfy3),sum(r.mzfy4),sum(r.mzfy5),sum(r.mzfy6),
 sum(r.mzfy7),sum(r.mzfy8),sum(r.mzfy9),sum(r.mzfy10),sum(r.mzfy11),sum(r.mzfy12),sum(r.mzfy13),sum(r.mzfy14),
 sum(r.mzfy15),sum(r.mzfy16),sum(r.mzfy17),sum(r.mzfy18),sum(r.mzfy19),sum(r.mzfy20),sum(r.mzfy51),sum(r.mzjzzj),sum(r.mzsjjz),
 sum(r.zyrs),sum(r.zyts),sum(r.zyfy1),sum(r.zyfy2),sum(r.zyfy3),sum(r.zyfy4),sum(r.zyfy5),sum(r.zyfy6),sum(r.zyfy7),sum(r.zyfy8),
 sum(r.zyfy9),sum(r.zyfy10),sum(r.zyfy11),sum(r.zyfy12),sum(r.zyfy13),sum(r.zyfy14),sum(r.zyfy15),sum(r.zyfy16),sum(r.zyfy17),
 sum(r.zyfy18),sum(r.zyfy19),sum(r.zyfy20),sum(r.zyfy51),sum(r.zyjzzj),sum(r.zysjjz),r.tjyf ,'合计' as sort_id
 from gfhz_report r,com_dictionary c
 where c.type = 'GFBBNZT' 
 and c.CODE = r.PACT_HEAD 
 and c.MARK= '{1}'
 and r.tjyf = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
 group by r.tjyf 
 order by SORT_ID"
;

                strSql = string.Format(strSql, month, ReportTag);
                this.ExecQuery(strSql, ref ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 查询市公医报表
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public DataSet GetGFReportNew(DateTime month)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select  PACT_HEAD as 类别,
                                    sum(r.mzrs) as 门诊人数,
                                    sum(r.MZTOTALFEE) as 门诊医疗费总金额,
                                    sum(r.MZSELFPAY) as 门诊自费金额,
                                    sum(r.MZOVERLIMITDRUG) as 门诊药费超标金额,
                                    sum(r.MZJZZJ-r.MZSJJZ) as 门诊自负金额,
                                    sum(r.MZSJJZ) as 实际记账金额,
                                    sum(r.ZYRS) as 住院人数,
                                    sum(r.ZYTS) as 住院天数,
                                    sum(r.ZYTOTALFEE) as 住院医疗费总金额,
                                    sum(r.ZYSELFPAY) as 住院自费金额,
                                    sum(r.ZYOVERLIMITDRUG) as 住院药费超标金额,
                                    sum(r.ZYCOMPANYPAY) as 住院单位支付金额,
                                    sum(r.ZYJZZJ-r.ZYSJJZ) as 住院自负金额,
                                    sum(r.ZYSJJZ) as 住院实际记账金额,
                                    sum(r.MZSJJZ+r.ZYSJJZ) as 实际记账金额合计
                                    from gfhz_report r,com_dictionary c
                                    where c.type = 'GFBBNZT' 
                                    and c.CODE = r.PACT_HEAD 
                                    and c.MARK in ('SQGY','SSGY')
                                    and r.tjyf = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                    group by r.PACT_HEAD ";
                strSql = string.Format(strSql, month);
                this.ExecQuery(strSql, ref ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 查询各区的公费情况（按区代码）
        /// </summary>
        /// <param name="month"></param>
        /// <param name="ReportType"></param>
        /// <returns></returns>
        public DataSet GetGFReportNewbyReportType(DateTime month, String ReportType)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select  PACT_HEAD as 类别,
                                    sum(r.mzrs) as 门诊人数,
                                    sum(r.MZTOTALFEE) as 门诊医疗费总金额,
                                    sum(r.MZSELFPAY) as 门诊自费金额,
                                    sum(r.MZOVERLIMITDRUG) as 门诊药费超标金额,
                                    sum(r.MZJZZJ-r.MZSJJZ) as 门诊自负金额,
                                    sum(r.MZSJJZ) as 实际记账金额,
                                    sum(r.ZYRS) as 住院人数,
                                    sum(r.ZYTS) as 住院天数,
                                    sum(r.ZYTOTALFEE) as 住院医疗费总金额,
                                    sum(r.ZYSELFPAY) as 住院自费金额,
                                    sum(r.ZYOVERLIMITDRUG) as 住院药费超标金额,
                                    sum(r.ZYCOMPANYPAY) as 住院单位支付金额,
                                    sum(r.ZYJZZJ-r.ZYSJJZ) as 住院自负金额,
                                    sum(r.ZYSJJZ) as 住院实际记账金额,
                                    sum(r.MZSJJZ+r.ZYSJJZ) as 实际记账金额合计
                                    from gfhz_report r,com_dictionary c
                                    where c.type = 'GFBBNZT' 
                                    and c.CODE = r.PACT_HEAD 
                                    and c.MARK in ('{1}')
                                    and r.tjyf = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                    group by r.PACT_HEAD ";
                strSql = string.Format(strSql, month, ReportType);
                this.ExecQuery(strSql, ref ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }


 
        }

        public DataSet GetShengGfhzReport(string month, string ReportTag)
        {
            try
            {
                DataSet ds = new DataSet();
               /* string strSql = @"with wc
(PACT_HEAD,mzrs,mzfy1,mzfy2,mzfy3,mzfy4,mzfy5,mzfy6,
 mzfy7,mzfy8,mzfy9,mzfy10,mzfy11,mzfy12,mzfy13,mzfy14,mzfy15,
 mzfy16,mzfy17,mzfy18,mzfy19,mzfy20,mzfy51,mzjzzj,mzsjjz,
 zyrs,zyts,zyfy1,zyfy2,zyfy3,zyfy4,zyfy5,zyfy6,zyfy7,zyfy8,
 zyfy9,zyfy10,zyfy11,zyfy12,zyfy13,zyfy14,zyfy15,zyfy16,zyfy17,
 zyfy18,zyfy19,zyfy20,zyfy51,zyjzzj,zysjjz,tjyf,sortid)
 as  (select case left(r.PACT_HEAD,2)
        when 'J8' then '交'
				when '82' then '8283'
				when '83' then '8283'
				when '90' then '家属' 
				else left(r.PACT_HEAD,2)  end,				
r.mzrs,r.mzfy1,r.mzfy2,r.mzfy3,r.mzfy4,r.mzfy5,r.mzfy6,
 r.mzfy7,r.mzfy8,r.mzfy9,r.mzfy10,r.mzfy11,r.mzfy12,r.mzfy13,r.mzfy14,r.mzfy15,
 r.mzfy16,r.mzfy17,r.mzfy18,r.mzfy19,r.mzfy20,r.mzfy51,r.mzjzzj,r.mzsjjz,
 r.zyrs,r.zyts,r.zyfy1,r.zyfy2,r.zyfy3,r.zyfy4,r.zyfy5,r.zyfy6,r.zyfy7,r.zyfy8,
 r.zyfy9,r.zyfy10,r.zyfy11,r.zyfy12,r.zyfy13,r.zyfy14,r.zyfy15,r.zyfy16,r.zyfy17,
 r.zyfy18,r.zyfy19,r.zyfy20,r.zyfy51,r.zyjzzj,r.zysjjz,r.tjyf,case left(r.PACT_HEAD,2)
				when '80' then '1'
				when '81' then '2'
                when 'J8' then '4'
				when '82' then '3'
				when '83' then '3'
				when '90' then '5' 
				end sortid
 from gfhz_report r,com_dictionary c
 where c.type = 'GFBBNZT' 
 and c.CODE = r.PACT_HEAD 
 and c.MARK=  '{1}'
 and r.tjyf = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
 order by c.SORT_ID) 
  select '干部',sum(mzrs),sum(mzfy1),sum(mzfy2),sum(mzfy3),sum(mzfy4),sum(mzfy5),sum(mzfy6),
 sum(mzfy7),sum(mzfy8),sum(mzfy9),sum(mzfy10),sum(mzfy11),sum(mzfy12),sum(mzfy13),sum(mzfy14),sum(mzfy15),
 sum(mzfy16),sum(mzfy17),sum(mzfy18),sum(mzfy19),sum(mzfy20),sum(mzfy51),sum(mzjzzj)-sum(mzfy16),sum(mzsjjz),
 sum(zyrs),sum(zyts),sum(zyfy1),sum(zyfy2),sum(zyfy3),sum(zyfy4),sum(zyfy5),sum(zyfy6),sum(zyfy7),sum(zyfy8),
 sum(zyfy9),sum(zyfy10),sum(zyfy11),sum(zyfy12),sum(zyfy13),sum(zyfy14),sum(zyfy15),sum(zyfy16),sum(zyfy17),
 sum(zyfy18),sum(zyfy19),sum(zyfy20),sum(zyfy51),sum(zyjzzj)-sum(zyfy16),sum(zysjjz),tjyf,'0' sortid
 from wc where pact_head in ('80','81','8283')
 group by tjyf
 union
 select PACT_HEAD,sum(mzrs),sum(mzfy1),sum(mzfy2),sum(mzfy3),sum(mzfy4),sum(mzfy5),sum(mzfy6),
 sum(mzfy7),sum(mzfy8),sum(mzfy9),sum(mzfy10),sum(mzfy11),sum(mzfy12),sum(mzfy13),sum(mzfy14),sum(mzfy15),
 sum(mzfy16),sum(mzfy17),sum(mzfy18),sum(mzfy19),sum(mzfy20),sum(mzfy51),sum(mzjzzj)-sum(mzfy16),sum(mzsjjz),
 sum(zyrs),sum(zyts),sum(zyfy1),sum(zyfy2),sum(zyfy3),sum(zyfy4),sum(zyfy5),sum(zyfy6),sum(zyfy7),sum(zyfy8),
 sum(zyfy9),sum(zyfy10),sum(zyfy11),sum(zyfy12),sum(zyfy13),sum(zyfy14),sum(zyfy15),sum(zyfy16),sum(zyfy17),
 sum(zyfy18),sum(zyfy19),sum(zyfy20),sum(zyfy51),sum(zyjzzj)-sum(zyfy16),sum(zysjjz),tjyf,sortid
 from wc
 group by pact_head,tjyf,sortid
 UNION
  select '合计:',sum(mzrs),sum(mzfy1),sum(mzfy2),sum(mzfy3),sum(mzfy4),sum(mzfy5),sum(mzfy6),
 sum(mzfy7),sum(mzfy8),sum(mzfy9),sum(mzfy10),sum(mzfy11),sum(mzfy12),sum(mzfy13),sum(mzfy14),sum(mzfy15),
 sum(mzfy16),sum(mzfy17),sum(mzfy18),sum(mzfy19),sum(mzfy20),sum(mzfy51),sum(mzjzzj)-sum(mzfy16),sum(mzsjjz),
 sum(zyrs),sum(zyts),sum(zyfy1),sum(zyfy2),sum(zyfy3),sum(zyfy4),sum(zyfy5),sum(zyfy6),sum(zyfy7),sum(zyfy8),
 sum(zyfy9),sum(zyfy10),sum(zyfy11),sum(zyfy12),sum(zyfy13),sum(zyfy14),sum(zyfy15),sum(zyfy16),sum(zyfy17),
 sum(zyfy18),sum(zyfy19),sum(zyfy20),sum(zyfy51),sum(zyjzzj)-sum(zyfy16),sum(zysjjz),tjyf,'9' sortid
 from wc 
 group by tjyf 
 order by sortid

";*/
                string strSql = @"with wc
 as  (select (case substr(r.PACT_HEAD,2)
        when 'J8' then '交'
				when '82' then '8283'
				when '83' then '8283'
				when '90' then '家属' 
				else substr(r.PACT_HEAD,0,2)  end) PACT_HEAD,				
r.mzrs,r.mzfy1,r.mzfy2,r.mzfy3,r.mzfy4,r.mzfy5,r.mzfy6,
 r.mzfy7,r.mzfy8,r.mzfy9,r.mzfy10,r.mzfy11,r.mzfy12,r.mzfy13,r.mzfy14,r.mzfy15,
 r.mzfy16,r.mzfy17,r.mzfy18,r.mzfy19,r.mzfy20,r.mzfy51,r.mzjzzj,r.mzsjjz,
 r.zyrs,r.zyts,r.zyfy1,r.zyfy2,r.zyfy3,r.zyfy4,r.zyfy5,r.zyfy6,r.zyfy7,r.zyfy8,
 r.zyfy9,r.zyfy10,r.zyfy11,r.zyfy12,r.zyfy13,r.zyfy14,r.zyfy15,r.zyfy16,r.zyfy17,
 r.zyfy18,r.zyfy19,r.zyfy20,r.zyfy51,r.zyjzzj,r.zysjjz,r.tjyf,(case substr(r.PACT_HEAD,0,2)
				when '80' then '1'
				when '81' then '2'
                when 'J8' then '4'
				when '82' then '3'
				when '83' then '3'
				when '90' then '5' 
				end )sortid
 from gfhz_report r,com_dictionary c
 where c.type = 'GFBBNZT' 
 and c.CODE = r.PACT_HEAD 
 and c.MARK=  '{1}'
 and r.tjyf = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
 order by c.SORT_ID) 
  select '干部',sum(mzrs),sum(mzfy1),sum(mzfy2),sum(mzfy3),sum(mzfy4),sum(mzfy5),sum(mzfy6),
 sum(mzfy7),sum(mzfy8),sum(mzfy9),sum(mzfy10),sum(mzfy11),sum(mzfy12),sum(mzfy13),sum(mzfy14),sum(mzfy15),
 sum(mzfy16),sum(mzfy17),sum(mzfy18),sum(mzfy19),sum(mzfy20),sum(mzfy51),sum(mzjzzj)-sum(mzfy16),sum(mzsjjz),
 sum(zyrs),sum(zyts),sum(zyfy1),sum(zyfy2),sum(zyfy3),sum(zyfy4),sum(zyfy5),sum(zyfy6),sum(zyfy7),sum(zyfy8),
 sum(zyfy9),sum(zyfy10),sum(zyfy11),sum(zyfy12),sum(zyfy13),sum(zyfy14),sum(zyfy15),sum(zyfy16),sum(zyfy17),
 sum(zyfy18),sum(zyfy19),sum(zyfy20),sum(zyfy51),sum(zyjzzj)-sum(zyfy16),sum(zysjjz),tjyf,'0' sortid
 from wc where pact_head in ('80','81','8283')
 group by tjyf
 union
 select PACT_HEAD,sum(mzrs),sum(mzfy1),sum(mzfy2),sum(mzfy3),sum(mzfy4),sum(mzfy5),sum(mzfy6),
 sum(mzfy7),sum(mzfy8),sum(mzfy9),sum(mzfy10),sum(mzfy11),sum(mzfy12),sum(mzfy13),sum(mzfy14),sum(mzfy15),
 sum(mzfy16),sum(mzfy17),sum(mzfy18),sum(mzfy19),sum(mzfy20),sum(mzfy51),sum(mzjzzj)-sum(mzfy16),sum(mzsjjz),
 sum(zyrs),sum(zyts),sum(zyfy1),sum(zyfy2),sum(zyfy3),sum(zyfy4),sum(zyfy5),sum(zyfy6),sum(zyfy7),sum(zyfy8),
 sum(zyfy9),sum(zyfy10),sum(zyfy11),sum(zyfy12),sum(zyfy13),sum(zyfy14),sum(zyfy15),sum(zyfy16),sum(zyfy17),
 sum(zyfy18),sum(zyfy19),sum(zyfy20),sum(zyfy51),sum(zyjzzj)-sum(zyfy16),sum(zysjjz),tjyf,sortid
 from wc
 group by pact_head,tjyf,sortid
 UNION
  select '合计:',sum(mzrs),sum(mzfy1),sum(mzfy2),sum(mzfy3),sum(mzfy4),sum(mzfy5),sum(mzfy6),
 sum(mzfy7),sum(mzfy8),sum(mzfy9),sum(mzfy10),sum(mzfy11),sum(mzfy12),sum(mzfy13),sum(mzfy14),sum(mzfy15),
 sum(mzfy16),sum(mzfy17),sum(mzfy18),sum(mzfy19),sum(mzfy20),sum(mzfy51),sum(mzjzzj)-sum(mzfy16),sum(mzsjjz),
 sum(zyrs),sum(zyts),sum(zyfy1),sum(zyfy2),sum(zyfy3),sum(zyfy4),sum(zyfy5),sum(zyfy6),sum(zyfy7),sum(zyfy8),
 sum(zyfy9),sum(zyfy10),sum(zyfy11),sum(zyfy12),sum(zyfy13),sum(zyfy14),sum(zyfy15),sum(zyfy16),sum(zyfy17),
 sum(zyfy18),sum(zyfy19),sum(zyfy20),sum(zyfy51),sum(zyjzzj)-sum(zyfy16),sum(zysjjz),tjyf,'9' sortid
 from wc 
 group by tjyf 
 order by sortid

";
                strSql = string.Format(strSql, month, ReportTag);
                this.ExecQuery(strSql, ref ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
       

        public DataSet GetShiGfDetail(string dtBegin, string dtEnd, string ReportTag)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select i.PATIENT_NO,
i.WORK_NAME,
i.NAME,
g.MCARD_NO,
g.BEGIN_DATE,
g.END_DATE,
0 天数,
g.FY17 ,
g.FY2,
0 特殊药,
g.FY5 检查费,
g.FY18 特检费,
g.FY7 治疗费,
g.FY8 手术费,
g.FY1 床位费,
g.FY20 监护费,
0,
0,
g.JZZJE 合计金额,
g.ZFBL 自负比例,
g.JZZJE - g.SJZJE 自负金额,
g.SJZJE 实际记账
from gfhz_main g ,fin_ipr_inmaininfo i 
where g.INPATIENT_NO = i.INPATIENT_NO
and g.FEE_TYPE = '2'
and g.OPER_DATE >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
and g.OPER_DATE <= to_date('{1}','yyyy-mm-dd hh24:mi:ss') ";

                strSql = string.Format(strSql, dtBegin, dtEnd, ReportTag);
                this.ExecQuery(strSql, ref ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        public DataSet GetGfhzSpecialReport(string dtBegin, string dtEnd)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select d.name,d.code,r.mzrs+r.zyrs as rs,r.mzsjjz+r.zysjjz as sjjz,'' as upper,r.tjyf
									from gfhz_report r,com_dictionary d
									where r.report_label=d.code
									and r.report_kind='D'
									and r.is_lx='T'
									and d.type='TUNIT'
									and r.tjyf >to_date('{0}','YYYY-MM-dd HH24:mi:ss')
									and r.tjyf <= to_date('{1}','YYYY-MM-dd HH24:mi:ss')";
                strSql = string.Format(strSql, dtBegin, dtEnd);
                this.ExecQuery(strSql, ref ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /*
        /// <summary>
        /// 获取省公医门诊住院汇总费用
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>		
        /// <returns></returns>
        public DataSet GetGfhzReport(string dtBegin, string dtEnd)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select d.mark,r.mzrs,r.mzfy1,r.mzfy2,r.mzfy3,r.mzfy4,r.mzfy5,r.mzfy6,
                                 r.mzfy7,r.mzfy8,r.mzfy9,r.mzfy10,r.mzfy11,r.mzfy12,r.mzfy13,r.mzfy14,r.mzfy15,
                                 r.mzfy16,r.mzfy17,r.mzfy18,r.mzfy19,r.mzfy20,r.mzfy51,r.mzjzzj,r.mzsjjz,
                                 r.zyrs,r.zyts,r.zyfy1,r.zyfy2,r.zyfy3,r.zyfy4,r.zyfy5,r.zyfy6,r.zyfy7,r.zyfy8,
					             r.zyfy9,r.zyfy10,r.zyfy11,r.zyfy12,r.zyfy13,r.zyfy14,r.zyfy15,r.zyfy16,r.zyfy17,
					             r.zyfy18,r.zyfy19,r.zyfy20,r.zyfy51,r.zyjzzj,r.zysjjz,r.tjyf
								from gfhz_report r,com_dictionary d
								where r.pact_head=d.code
								and r.report_kind='T'
								and d.type='GFINDEX'
								and d.wb_code='T'
								and r.tjyf >to_date('{0}','YYYY-MM-dd HH24:mi:ss')
								and r.tjyf <= to_date('{1}','YYYY-MM-dd HH24:mi:ss')
								order by d.sort_id";
                strSql = string.Format(strSql, dtBegin, dtEnd);
                this.ExecQuery(strSql, ref ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }
         */ 

        /// <summary>
        /// 获取省公医门诊住院汇总费用,区别GetGfhzReport 于财务科要把在各个区与特约单位之间插入“小计”
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>		
        /// <returns></returns>
        
        public DataSet GetGfhzReportDetail(string dtBegin, string dtEnd)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = "";
                if (this.Sql.GetSql("Local.Pub.GetGfhzReportDetail.1", ref strSql) == -1)
                    return null;

                strSql = string.Format(strSql, dtBegin, dtEnd);
                this.ExecQuery(strSql, ref ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        #endregion

        #region 门诊统计相关
        #region com_pactcompare 相关，合同单位 卡号 比例 等
        /// <summary>
        /// 根据合同单位取卡号
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetCardNo(FS.HISFC.Models.RADT.PatientInfo info)
        {
            if (info == null)
                return null;
            if (info.Pact.ID.Length < 2)
                return null;
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.Name = info.ID;
            try
            {
                string PactHead = this.GetPactHeadByPact(info.Pact.ID);
                if (PactHead == "DS")
                {
                    return null;
                }
                else if (PactHead != "NF")
                {
                    obj.ID = PactHead;
                }
                else if (info.Pact.ID.Substring(0, 1) == "1")/*省*/
                {

                    if (info.SSN.Substring(0, 1).ToUpper() == "J")/*缴款单位*/
                    {
                        obj.ID = info.SSN.Substring(0, 3).ToUpper();
                    }
                    else/*享受单位*/
                    {
                        obj.ID = info.SSN.Substring(0, 2).ToUpper();
                    }
                }
                else if (info.Pact.ID.Substring(0, 1) == "6")/*开发区*/
                {
                    if (info.SSN.Substring(0, 2).ToUpper() == "K7")/*K70,K71,K72,K73*/
                    {
                        obj.ID = info.SSN.Substring(0, 3).ToUpper();
                    }
                    else
                    {
                        obj.ID = info.SSN.Substring(0, 2).ToUpper();/*K1,K2*/
                    }
                }
                else if (info.Pact.ID.Substring(0, 1) == "7"
                    || info.Pact.ID.ToUpper() == "L3" || info.Pact.ID.ToUpper() == "Y110"
                    || info.Pact.ID.ToUpper() == "Y210" || info.Pact.ID.ToUpper() == "Y305")/*本院*/
                {
                    obj.ID = info.SSN.ToUpper();/*本院职工用工号 大于等于 4位*/
                }
                else if (info.Pact.ID.Substring(0, 1) == "5" || info.Pact.ID.Substring(0, 1) == "4")/*特约 3位*/
                {

                    obj.ID = info.SSN.ToUpper();
                }
                else/*其他所有取两位卡号的合同单位*/
                {
                    obj.ID = info.SSN.Substring(0, 2).ToUpper();
                }

                return obj;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 根据合同单位代码获取字头
        /// </summary>
        /// <param name="PactID"></param>
        /// <returns></returns>
        public string GetPactHeadByPact(string PactCode)
        {
            string rtn = "";
            string strSql = "select pact_head from com_pactcompare where pact_code = '{0}'";
            try
            {
                strSql = string.Format(strSql, PactCode);
                rtn = this.ExecSqlReturnOne(strSql, "NF");
                if (rtn == "-1")
                {
                    return "NF";
                }
                else
                {
                    return rtn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return "NF";
            }
        }

        public SOC.Local.PubReport.Models.PactInfo GetPactInfo(string PactCode)
        {
            string strSql = "select pact_code,pact_head,pact_name,parent_pact,parent_name,pact_flag,paykind_code,pay_rate " +
                           " from com_pactcompare where pact_code = '{0}'";
            strSql = string.Format(strSql, PactCode);
            ArrayList al = this.GetpactInfos(strSql);
            if (al == null)
            {
                return null;
            }
            if (al.Count == 0)
            {
                return new SOC.Local.PubReport.Models.PactInfo();
            }
            else
            {
                return al[0] as SOC.Local.PubReport.Models.PactInfo;
            }
        }
        /// <summary>
        /// 获取公费字头
        /// </summary>
        /// <returns></returns>
        public ArrayList GetPactHeadList()
        {
            string strSql = "select distinct pact_head,pact_name " +
                           " from com_pactcompare where paykind_code = '03' and Pact_HEAD <> 'NF' order by pact_name";
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();
                pinfo.Name = this.Reader[1].ToString();
                al.Add(pinfo);
            }
            return al;
        }

        /// <summary>
        /// 统计门诊收费员公费所有记录
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>Local.Pub.GetAllOper</returns>
        public ArrayList GetAllPactHeadByDateHistory(string begin, string end,string feeType,bool isChecked)
        {
            string valid = "";
            if (isChecked)
            {
                valid = "1";
            }
            else
            {
                valid = "0";
            }
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetAllOper.History", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, begin, end,feeType,valid);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.User01 = this.Reader[1].ToString();//工号
                pinfo.Name = this.Reader[2].ToString();//姓名
                pinfo.User02 = this.Reader[3].ToString();//条数				
                al.Add(pinfo);
            }
            return al;
        }


        /// <summary>
        /// 根据统计月份获取 统计门诊收费员公费所有记录 
        /// </summary>
        public ArrayList GetAllPactHeadByStaticMonth(string staticMonth, string feeType)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetAllOper.static", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, staticMonth, feeType);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.User01 = this.Reader[1].ToString();//工号
                pinfo.Name = this.Reader[2].ToString();//姓名
                pinfo.User02 = this.Reader[3].ToString();//条数				
                al.Add(pinfo);
            }
            return al;
        }

        /// <summary>
        /// 根据统计月份获取 统计门诊收费员公费所有记录 
        /// </summary>
        public ArrayList GetAllOper(string staticMonth, string feeType)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetOper.static", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, staticMonth, feeType);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.User01 = this.Reader[1].ToString();//工号
                pinfo.Name = this.Reader[2].ToString();//姓名
                pinfo.User02 = this.Reader[3].ToString();//条数				
                al.Add(pinfo);
            }
            return al;
        }


        /// <summary>
        /// 根据统计月份获取 统计门诊收费员公费所有记录 
        /// </summary>
        public ArrayList GetAllOper(string begin, string end, string feeType, bool isChecked)
        {
            string valid = "";
            if (isChecked)
            {
                valid = "1";
            }
            else
            {
                valid = "0";
            }
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetOper", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, begin,end, feeType,valid);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.User01 = this.Reader[1].ToString();//工号
                pinfo.Name = this.Reader[2].ToString();//姓名
                pinfo.User02 = this.Reader[3].ToString();//条数				
                al.Add(pinfo);
            }
            return al;
        }


        /// <summary>
        /// 统计门诊收费员公费所有记录
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>Local.Pub.GetAllOper</returns>
        public ArrayList GetAllOperByDate(string begin, string end)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetAllOper", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, begin, end);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.User01 = this.Reader[1].ToString();//工号
                pinfo.Name = this.Reader[2].ToString();//姓名
                pinfo.User02 = this.Reader[3].ToString();//条数				
                al.Add(pinfo);
            }
            return al;
        }

        /// <summary>
        /// 统计住院处收费员公费所有记录
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>Local.Pub.GetAllOper</returns>
        public ArrayList GetAllInPactHeadByDateHistory(string begin, string end,bool isChecked)
        {
            string valid = "";
            if (isChecked)
            {
                valid = "1";
            }
            else
            {
                valid = "0";
            }
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetAllOper.In.History", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, begin, end,valid);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.User01 = this.Reader[1].ToString();//工号
                pinfo.Name = this.Reader[2].ToString();//姓名
                pinfo.User02 = this.Reader[3].ToString();//条数				
                al.Add(pinfo);
            }
            return al;
        }

        /// <summary>
        /// 统计住院处收费员公费所有记录
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>Local.Pub.GetAllOper</returns>
        public ArrayList GetAllInOperByDate(string begin, string end)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetAllOper.In", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, begin, end);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.User01 = this.Reader[1].ToString();//工号
                pinfo.Name = this.Reader[2].ToString();//姓名
                pinfo.User02 = this.Reader[3].ToString();//条数				
                al.Add(pinfo);
            }
            return al;
        }

        /// <summary>
        /// 统计门诊收费员gfhz_main没有打印小单所有记录
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>Local.Pub.GetAllOper</returns>
        public ArrayList GetAllOperByDateNotPrint(string month)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetAllOper.2", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, month);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//字头
                pinfo.Name = this.Reader[1].ToString();//合同单位名称
                pinfo.User01 = this.Reader[2].ToString();//''暂时无用				
                pinfo.User02 = this.Reader[3].ToString();//条数				
                al.Add(pinfo);
            }
            return al;
        }


        /// <summary>
        /// 统计住院公医的所有单据
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>Local.Pub.GetAllOper</returns>
        public ArrayList GetAllOperByDateZY(string month)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetAllOper.static", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, month,"2");

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//字头
                pinfo.Name = this.Reader[1].ToString();//合同单位名称
                pinfo.User01 = this.Reader[2].ToString();//''暂时无用				
                pinfo.User02 = this.Reader[3].ToString();//条数				
                al.Add(pinfo);
            }
            return al;
        }

        public FS.FrameWork.Models.NeuObject GetWorkHome(string PatientNO)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            string sql = @" select p.WORK_HOME,p.spell_code from com_patientinfo p 
 where p.CARD_NO = '{0}'";
            sql = string.Format(sql, PatientNO);
            this.ExecQuery(sql);
            while (this.Reader.Read())
            {
                obj.Name = this.Reader[0].ToString();
                obj.ID = this.Reader[1].ToString();
            }
            return obj;
        }

        /// <summary>
        /// 获取门诊公费明细记录
        /// </summary>
        /// <param name="UserCode">章号</param>
        /// <param name="PactHead">字头</param>
        /// <returns></returns>
        public ArrayList QueryPubFeeInfoByOperAndPactheadHistory(string UserCode, string PactHead, DateTime begin, DateTime end,string feeType,bool isValid)
        {
            string valid = "";
            if (isValid)
            {
                valid = "1";
            }
            else
            {
                valid = "0";
            }
            DataSet ds = new DataSet();
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetGfDetail.History", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, UserCode, PactHead, begin.ToString(), end.ToString(),feeType,valid);
            return this.myPubReport_other(strSql);

        }

        /// <summary>
        /// 获取门诊公费明细记录
        /// </summary>
        /// <param name="UserCode">章号</param>
        /// <param name="PactHead">字头</param>
        /// <returns></returns>
        public ArrayList QueryClinicPubFeeInfoByOperAndPactheadHistory(string UserCode, string PactHead, DateTime begin, DateTime end, string feeType,bool isValid)
        {
            string valid;
            if (isValid)
            {
                valid = "1";
            }
            else
            {
                valid = "0";
            }
            DataSet ds = new DataSet();
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetGfDetail.History.Clinic", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, UserCode, PactHead, begin.ToString(), end.ToString(), feeType,valid);
            return this.myPubReport_other(strSql);

        }

        /// <summary>
        /// 根据统计月份获取已经审核的记账单信息
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="PactHead"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="feeType"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public ArrayList QueryPubFeeInfoByOperAndPactheadStaticMonth(string UserCode, string PactHead, DateTime staticMonth, string feeType)
        {
            
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetGfDetail.byStaticMonth", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, UserCode, PactHead, staticMonth.ToString(), feeType);
            return this.myPubReport_other(strSql);

        }

      
        public ArrayList QueryPubFeeInfoByMcardNo(string mcardNo,DateTime dtBegin,DateTime dtEnd,string feeType)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetGfDetail.mcardNo", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql,mcardNo,dtBegin.ToString(),dtEnd.ToString(),feeType);
            return this.myPubReport_other(strSql);

        }

        public ArrayList QueryPubFeeInfoByMcardNo(string mcardNo, DateTime staticMonth,string feeType)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetGfDetail.mcardNoandstatic", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, mcardNo, staticMonth.ToString(), feeType);
            return this.myPubReport_other(strSql);

        }


        /// <summary>
        /// 获取门诊公费明细记录
        /// </summary>
        /// <param name="PactHead">字头</param>
        /// <returns></returns>
        public ArrayList QueryClinicPubFeeInfoPactheadHistory(string PactHead, DateTime month, string feeType, bool isValid)
        {
            string valid;
            if (isValid)
            {
                valid = "1";
            }
            else
            {
                valid = "0";
            }
            DataSet ds = new DataSet();
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetGfDetail.History.ClinicHead", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, PactHead, month.ToString(), feeType, valid);
            return this.myPubReport_other(strSql);

        }



        //public ArrayList QueryInvoiceByNo(string InvoiceNo)
        //{

 
        //}



        /// <summary>
        /// 获取公费明细记录对帐用。小何
        /// </summary>
        /// <param name="PactHead"></param>
        /// <param name="date"></param>
        /// <param name="feeType"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        /// 


        public ArrayList QueryPubFeeInfoPactheadHistoryByTimeandDiQu(string DiQu, DateTime date,DateTime date2,string feeType, bool isValid)
        {
            string valid;
            if (isValid)
            {
                valid = "1";
            }
            else
            {
                valid = "0";
            }
            DataSet ds = new DataSet();
            string strSql = "";
            if (DiQu == "ALL")
            {
                if (this.Sql.GetSql("Local.Pub.GetGfDetail.History.ClinicHead.ByTime.DiQu.NewAll", ref strSql) == -1)
                    return null;
            }
            else
            {
                if (this.Sql.GetSql("Local.Pub.GetGfDetail.History.ClinicHead.ByTime.DiQu", ref strSql) == -1)
                    return null;
            }
                strSql = string.Format(strSql, DiQu, date.ToString(),date2.ToString(),feeType, valid);
                return this.myPubReport_other_clinicnew(strSql);

        }


        /// <summary>
        /// 获取公费明细记录 - 收费处 - 对帐用。小何 feetype 1-门诊 2-住院
        /// </summary>
        /// <param name="PactHead"></param>
        /// <param name="date"></param>
        /// <param name="feeType"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        /// 

        public ArrayList QueryPubFeeInfoPactheadFromInvoice(string DiQu, DateTime date, DateTime date2, string feeType)
        {
            string valid;
            DataSet ds = new DataSet();
            string strSql = "";
            if (feeType == "2")//住院
            {
                if (DiQu == "ALL")
                {
                    if (this.Sql.GetSql("Local.Pub.GetGfDetail.History.Fee.Invoice.All", ref strSql) == -1)
                        return null;
                }
                else
                {
                    if (this.Sql.GetSql("Local.Pub.GetGfDetail.History.Fee.Invoice.DiQu", ref strSql) == -1)
                        return null;
                }
            }
            else if (feeType == "1")//门诊
            {
                if (DiQu == "ALL")
                {
                    if (this.Sql.GetSql("Local.Pub.GetGfDetail.History.Fee.Invoice.Menzen.All", ref strSql) == -1)
                        return null;
                }
                else
                {
                    if (this.Sql.GetSql("Local.Pub.GetGfDetail.History.Fee.Invoice.Menzen.DiQu", ref strSql) == -1)
                        return null;
                }
 
            }
            strSql = string.Format(strSql, DiQu, date.ToString(), date2.ToString());
            return this.myPubReport_Invoice(strSql);

        }


       // private bool Invoice




        /// <summary>
        /// 
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="feeType"></param>
        /// <returns></returns>
        private ArrayList myPubReport_Invoice(string SQLString)
        {
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            SOC.Local.PubReport.Models.PubReport info;            //返回数组中的摆药单分类信息
            this.ProgressBarText = "正在检索药品信息...";
            this.ProgressBarValue = 0;
            #region
            #endregion
            if (this.ExecQuery(SQLString) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    info = new SOC.Local.PubReport.Models.PubReport();
                    try
                    {
                        
                        info.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[0].ToString());//日期
                        info.Bill_No = this.Reader[1].ToString();//发票号
                        info.OperCode = this.Reader[2].ToString(); //收费员号
                        info.User01 = this.Reader[3].ToString();//收费员名称
                        info.Tot_Cost = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);
                        info.Pay_Rate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);//pay_cost
                        info.SelfPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                        info.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                        info.ID = this.Reader[8].ToString();//地区
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    this.ProgressBarValue++;
                    al.Add(info);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            this.ProgressBarValue = -1;
            return al;

        }




        public int GetPubReportCount(DateTime begin, DateTime end,string feeType)
        {
////            string strSql = @"select count(*) from gfhz_main g where 
////g.OPER_DATE between '{0}' and '{1}'
////and g.fee_type = '{2}'
////";
            string strSql = @"select count(*) from gfhz_main g where 
            g.OPER_DATE between to_date('{0}','yyyy-mm-dd hh24:mi:ss') and to_date('{1}','yyyy-mm-dd hh24:mi:ss')
            and g.fee_type = '{2}'
            ";
            try
            {
                strSql = string.Format(strSql, begin.ToString(), end.ToString(), feeType);
            }
            catch
            {
                return -1;
            }
            string strCount = this.ExecSqlReturnOne(strSql);
            int count = 0;
            try
            {
                count = FS.FrameWork.Function.NConvert.ToInt32(strCount);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return count;
        }


        /// <summary>
        /// 获取门诊公费明细记录
        /// </summary>
        /// <param name="UserCode">章号</param>
        /// <param name="PactHead">字头</param>
        /// <returns></returns>
        public ArrayList GetGfDetail(string UserCode, string PactHead, string begin, string end)
        {

            DataSet ds = new DataSet();
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetGfDetail", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, UserCode, PactHead, begin, end);
            return this.myPubReport_other(strSql);

        }

        /// <summary>
        /// 获取门诊公费明细记录,未打印
        /// </summary>
        /// <param name="UserCode">章号</param>
        /// <param name="PactHead">字头</param>
        /// <returns></returns>
        public ArrayList GetGfDetailNotPrint(string UserCode, string PactHead, string begin, string end)
        {

            DataSet ds = new DataSet();
            string strSql = "";
            if (this.Sql.GetSql("Local.Pub.GetGfDetail.2", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, UserCode, PactHead, begin, end);
            return this.myPubReport_other(strSql);

        }

        private ArrayList GetpactInfos(string strSql)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                SOC.Local.PubReport.Models.PactInfo pinfo = new SOC.Local.PubReport.Models.PactInfo();
                pinfo.ID = this.Reader[0].ToString();
                pinfo.Name = this.Reader[2].ToString();
                pinfo.Pact.ID = this.Reader[0].ToString();
                pinfo.Pact.Name = this.Reader[2].ToString();
                pinfo.PactHead = this.Reader[1].ToString();
                pinfo.ParentPact.ID = this.Reader[3].ToString();
                pinfo.ParentPact.Name = this.Reader[4].ToString();
                pinfo.PactFlag = this.Reader[5].ToString();
                pinfo.PayKind = this.Reader[6].ToString();
                pinfo.PactRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
                al.Add(pinfo);
            }
            return al;
        }

        #endregion


        /// <summary>
        /// 更新发票统计状态
        /// </summary>
        /// <param name="inv">ID</param>
        /// <param name="state">1有效0无效2未复核 状态</param>
        /// <returns>1 成功</returns>
        public int UpdateConfirmflag(string BillId, string state)
        {
            string strSql = "Update gfhz_main set IS_VALID = '{1}' where  inPatient_no='{0}'";
            strSql = string.Format(strSql, BillId, state);
            if (this.ExecNoQuery(strSql) != 1)
            {
                return -1;
            }
            return 1;
        }


        public int UpdateBillNo(string BillNo, string id,int seq)
        {
            string strSql = " update gfhz_main g set g.EXT_FLAG = '{2}' where g.ID = '{0}' and g.SEQ = {1}";
            strSql = string.Format(strSql, id,seq.ToString(),BillNo);
            if (this.ExecNoQuery(strSql) != 1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更新发票及账单号及状态
        /// </summary>
        /// <param name="BillNO"></param>
        ///  <param name="state">状态:1有效 0无效 2未复核</param>
        /// <param name="type">类型: 1小单号，2流水号，3 发票号</param>
        /// <returns></returns>
        public int UpdateBillNO(string BillNO, string state, int type)
        {
            string strSql = "";
            if (type == 1)
                return -1;
            else if (type == 2)
            {
                strSql = @"update gfhz_main tt set tt.is_valid='{1}',oper_code='{2}',oper_date=sysdate where tt.inpatient_no in 
				   (select t.inpatient_no from gfhz_main t,fin_opb_gfinvoice g where t.id=g.gfhz_id and g.gf_seq = '{0}')";
            }
            else if (type == 3)
            {
                strSql = "update gfhz_main t set t.is_valid='{1}',oper_code='{2}',oper_date=sysdate where t.inpatient_no='{0}'";
            }
            strSql = string.Format(strSql, BillNO, state, this.Operator.ID);
            if (this.ExecNoQuery(strSql) != 1)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 更新发票及账单号及状态
        /// </summary>
        /// <param name="BillID"></param>
        /// <param name="BillNo"></param>
        /// <param name="state">1有效0无效2未复核</param>
        /// <returns></returns>
        public int UpdateBillNO(string BillID, string BillNo, string state)
        {
            string strSql = "Update gfhz_main set LSH = '{1}' ,IS_VALID = '{2}',oper_code='{3}',oper_date=sysdate where  ID='{0}'";
            strSql = string.Format(strSql, BillID, BillNo, state, this.Operator.ID);
            if (this.ExecNoQuery(strSql) != 1)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 更新发票及账单号及状态
        /// </summary>
        /// <param name="BillID"></param>
        /// <param name="BillNo"></param>
        /// <param name="state">1有效0无效2未复核</param>
        /// <param name="pactcode">字头</param>
        /// <param name="pactcode">医疗证号</param>
        /// <returns></returns>
        public int UpdateBillNO(string BillID, string BillNo, string state, string pactcode, string mcard_no)
        {
            string strSql = "Update gfhz_main set LSH = '{1}' ,IS_VALID = '{2}',oper_code='{3}',BH='{4}',mcard_no='{5}',oper_date=sysdate where  ID='{0}'";
            strSql = string.Format(strSql, BillID, BillNo, state, this.Operator.ID, pactcode, mcard_no);
            if (this.ExecNoQuery(strSql) != 1)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 更新发票及账单号及状态
        /// </summary>
        /// <param name="BillID"></param>
        /// <param name="BillNo"></param>
        /// <param name="state">1有效0无效2未复核</param>
        /// <returns></returns>
        public int UpdateBillNO(string BillID, string BillNo)
        {
            string strSql = "Update gfhz_main set LSH = '{1}',oper_code='{2}',oper_date=sysdate where  ID='{0}'";
            strSql = string.Format(strSql, BillID, BillNo, this.Operator.ID);
            if (this.ExecNoQuery(strSql) != 1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更新发票及账单号及状态
        /// </summary>
        /// <param name="BillID"></param>
        /// <param name="BillNo"></param>
        /// <param name="state">1有效0无效2未复核</param>
        /// <returns></returns>
        public int UpdateBillNOPrintBill(string BillID, string BillNo, string pactcode, string mcard_no)
        {
            string strSql = "Update gfhz_main set LSH = '{1}',oper_code='{2}',BH='{3}',mcard_no='{4}',oper_date=sysdate where  ID='{0}'";
            strSql = string.Format(strSql, BillID, BillNo, this.Operator.ID, pactcode, mcard_no);
            if (this.ExecNoQuery(strSql) != 1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更新帐单流水号及状态
        /// </summary>
        /// <param name="BillID"></param>
        /// <param name="BillNo"></param>
        /// <param name="state">1有效0无效2未复核</param>
        /// <returns></returns>
        public int UpdateBillNO(string BillNo)
        {
            string strSql = "Update gfhz_main set LSH = '{1}',oper_code='{2}',oper_date=sysdate where  LSH='{0}'";
            strSql = string.Format(strSql, BillNo, '0', this.Operator.ID);
            if (this.ExecNoQuery(strSql) != 1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 统计记账单信息
        /// </summary>
        /// <param name="BillNO">记账单单号</param>
        /// <returns></returns>
        public ArrayList GetDetailForJZD(string BillNO)
        {
            string strSql = "select * from gfhz_main t where IS_VALID = '1' ";
            strSql = string.Format(strSql, BillNO);
            //return this.myPubReport(strSql);		
            return this.myPubReport_other_a(strSql);
        }

        /// <summary>
        /// 统计记账单信息
        /// </summary>
        /// <param name="BillNO">记账单单号</param>
        ///  <param name="type">类型: 1小单号，2流水号，3 发票号</param>
        /// <returns></returns>
        public ArrayList GetDetailForJZD(string BillNO, int type)
        {
            string strSql = "";
            if (type == 1)
                strSql = "select * from gfhz_main t,fin_opb_gfinvoice g where t.id=g.gfhz_id and LSh = '{0}' and IS_VALID = '1' order by g.gf_seq";
            else if (type == 2)
                strSql = "select * from gfhz_main t,fin_opb_gfinvoice g where t.id=g.gfhz_id and g.gf_seq = '{0}' and IS_VALID = '1' order by g.gf_seq";
            else
                strSql = "select * from gfhz_main t,fin_opb_gfinvoice g where t.id=g.gfhz_id and g.invoice_no = '{0}' and IS_VALID = '1' order by g.gf_seq";
            strSql = string.Format(strSql, BillNO);
            //return this.myPubReport(strSql);		
            return this.myPubReport_other_a(strSql);
        }
        /// <summary>
        /// 根据字头,状态获取发票
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="PactHead">"BY"本院，其他为合同单位字头</param>
        /// <param name="state">0作废1正常2未审核</param>
        /// <returns></returns>
        public ArrayList GetInvinfoByPact(string begin, string end, string PactHead, string state)
        {
            string strSql;
            if (PactHead == "BY")
            {
                strSql = "select * from gfhz_main where begin_date>to_date('{0}','yyyy-mm-dd hh24:mi:ss') and begin_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') and fee_type = '2' and  length('{2}')>=4 and mcardNO = '{2}' and IS_valid = '{3}' " +
                    " order by inpatient_no ";
            }
            else
            {
                strSql = "select * from gfhz_main where begin_date>to_date('{0}','yyyy-mm-dd hh24:mi:ss')  and begin_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') and fee_type = '2' and  bh='{2}' and IS_valid = '{3}' " +
                    " order by inpatient_no ";
            }
            strSql = string.Format(strSql, begin, end, PactHead, state);
            return this.myPubReport(strSql);

        }
        /// <summary>
        ///  根据小单号,状态获取发票详细信息
        /// </summary>
        /// <param name="LSH"></param>
        /// <returns></returns>
        public ArrayList GetInvinfoByPact(string LSH, string state)
        {
            string strSql;
            strSql = "select * from gfhz_main where LSH='{0}' and IS_valid = '{1}' ";
            strSql = string.Format(strSql, LSH, state);
            return this.myPubReport(strSql);

        }
        #region
        //		select distinct(LSH),  /*小单号*/
        //		BH    /*字头号*/        
        //		from gfhz_main 
        //		where is_valid='1' /*已经审核*/
        //		and   fee_type='2' /*门诊*/
        //		and   BH='01'
        #endregion
        /// <summary>
        /// 根据字头，查询已经复核之后的小单
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="PactHead">字头</param>
        /// <param name="state">0作废1已审核2未审核</param>
        /// <returns></returns>		
        public ArrayList GetInvoice_LSH_ByPact(string begin, string end, string PactHead, string state)
        {
            string strSql = "select distinct(LSH),BH from gfhz_main where begin_date>to_date('{0}','yyyy-mm-dd hh24:mi:ss')  and begin_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') and fee_type = '2' and  bh='{2}' and IS_valid = '{3}' ";
            strSql = string.Format(strSql, begin, end, PactHead, state);
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();
                pinfo.Name = this.Reader[1].ToString();
                al.Add(pinfo);
            }
            return al;
        }

        /// <summary>
        /// 根据章号，统计各个字头
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="EmplCode">工号</param>
        /// <returns></returns>		
        public ArrayList GetInvoiceSumByUserCode(string begin, string end, string EmplCode,string feeType,bool isValid)
        {
            //if(c.ExecQuery("Local.Clinic.GetOperInvoiceList",ref ds,Begin,End,c.Operator.ID,this.parm)==-1)
            string valid = "";
            if (isValid)
            {
                valid = "1";
            }
            else
            {
                valid = "0";
            }
            string strSql = "";
            //if (this.Sql.GetSql("Local.Clinic.GetOperInvoiceList",ref strSql)==-1) return null;
            if (this.Sql.GetSql("Local.Pub.GetOperInvoiceList", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, begin, end, EmplCode, feeType,valid);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.Name = this.Reader[1].ToString();//姓名
                pinfo.User01 = this.Reader[2].ToString();//字头
                pinfo.User02 = this.Reader[3].ToString();//条数
                al.Add(pinfo);
            }
            return al;
        }

        /// <summary>
        /// 根据章号统计月份，统计各个字头
        /// </summary>
        public ArrayList GetInvoiceSumByUserCode(string staticMonth, string EmplCode, string feeType, bool isValid)
        {
            //if(c.ExecQuery("Local.Clinic.GetOperInvoiceList",ref ds,Begin,End,c.Operator.ID,this.parm)==-1)
            string valid = "";
            if (isValid)
            {
                valid = "1";
            }
            else
            {
                valid = "0";
            }
            string strSql = "";
            //if (this.Sql.GetSql("Local.Clinic.GetOperInvoiceList",ref strSql)==-1) return null;
            if (this.Sql.GetSql("Local.Pub.GetOperInvoiceList.static", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql,  staticMonth, EmplCode, feeType, valid);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.Name = this.Reader[1].ToString();//姓名
                pinfo.User01 = this.Reader[2].ToString();//字头
                pinfo.User02 = this.Reader[3].ToString();//条数
                al.Add(pinfo);
            }
            return al;
        }

        /// <summary>
        /// 根据章号，统计各个字头
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="EmplCode">工号</param>
        /// <returns></returns>		
        public ArrayList GetInvoiceSumInByUserCode(string begin, string end, string EmplCode,bool isValid)
        {
            //if(c.ExecQuery("Local.Clinic.GetOperInvoiceList",ref ds,Begin,End,c.Operator.ID,this.parm)==-1)
            string valid = "";
            if (isValid)
            {
                valid = "1";
            }
            else
            {
                valid = "0";
            }
            string strSql = "";
            //if (this.Sql.GetSql("Local.Clinic.GetOperInvoiceList",ref strSql)==-1) return null;
            if (this.Sql.GetSql("Local.Pub.GetInOperInvoiceList", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, begin, end, EmplCode, "Personal",valid);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.Name = this.Reader[1].ToString();//姓名
                pinfo.User01 = this.Reader[2].ToString();//字头
                pinfo.User02 = this.Reader[3].ToString();//条数
                al.Add(pinfo);
            }
            return al;
        }

        /// <summary>
        /// 根据章号，统计各个字头,未打印
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="EmplCode">工号</param>
        /// <returns></returns>		
        public ArrayList GetInvoiceSumByUserCodeNotPrint(string begin, string end, string EmplCode)
        {
            //if(c.ExecQuery("Local.Clinic.GetOperInvoiceList",ref ds,Begin,End,c.Operator.ID,this.parm)==-1)

            string strSql = "";
            //if (this.Sql.GetSql("Local.Clinic.GetOperInvoiceList",ref strSql)==-1) return null;
            if (this.Sql.GetSql("Local.Pub.GetOperInvoiceList.2", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, begin, end, EmplCode, "Personal");

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.Name = this.Reader[1].ToString();//姓名
                pinfo.User01 = this.Reader[2].ToString();//字头
                pinfo.User02 = this.Reader[3].ToString();//条数
                al.Add(pinfo);
            }
            return al;
        }

        /// <summary>
        /// 根据章号，统计各个字头,未打印
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="EmplCode">工号</param>
        /// <returns></returns>		
        public ArrayList GetInvoiceSumByPactHead(string begin, string end, string PactHead)
        {
            //if(c.ExecQuery("Local.Clinic.GetOperInvoiceList",ref ds,Begin,End,c.Operator.ID,this.parm)==-1)

            string strSql = "";
            //if (this.Sql.GetSql("Local.Clinic.GetOperInvoiceList",ref strSql)==-1) return null;
            if (this.Sql.GetSql("Local.Pub.GetOperInvoiceList.3", ref strSql) == -1)
                return null;
            strSql = string.Format(strSql, begin, end, PactHead);

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.ID = this.Reader[0].ToString();//章号
                pinfo.Name = this.Reader[1].ToString();//姓名
                pinfo.Memo = this.Reader[2].ToString();
                pinfo.User01 = this.Reader[3].ToString();//字头
                pinfo.User02 = this.Reader[4].ToString();//条数
                al.Add(pinfo);
            }
            return al;
        }
        /// <summary>
        /// 根据字头，获得收费员对应发票数
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="PactHead"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        #region  SQL
        //		select e.user_code,bh,count(1)
        //		from gfhz_main g,               /*公费汇总表*/
        //		fin_opb_invoiceinfo f,/*发票信息表*/
        //			com_employee e       /*员工代码表*/
        //     
        //			where g.inpatient_no=f.invoice_no
        //									 and f.oper_code=e.empl_code
        //														 and begin_date>to_date('2006-1-17 0:00:00','yyyy-mm-dd hh24:mi:ss')  
        //		and begin_date<=to_date('2007-1-18 23:59:59','yyyy-mm-dd hh24:mi:ss') 
        //		and fee_type = '2' 
        //			and  bh='01' 
        //				and IS_valid = '2'
        //					group by e.user_code,bh		

        #endregion
        public ArrayList GetInvoiceNum_ByPact(string begin, string end, string PactHead, string state)
        {
            string strSql = "select e.user_code,bh,count(1) from gfhz_main g,fin_opb_invoiceinfo f,com_employee e " +      //发票信息表 员工代码表		             
                        "where g.inpatient_no=f.invoice_no and f.oper_code=e.empl_code " +
                        "and begin_date>to_date('{0}','yyyy-mm-dd hh24:mi:ss') and begin_date<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') " +
                        "and fee_type = '2' " +
                        "and  bh='{2}' " +
                        "and IS_valid = '{3}' group by e.user_code,bh";

            strSql = string.Format(strSql, begin, end, PactHead, state);
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.User01 = this.Reader[0].ToString();//工号
                pinfo.User02 = this.Reader[1].ToString();//字头
                pinfo.User03 = this.Reader[2].ToString();//数量
                al.Add(pinfo);
            }
            return al;
        }

        public ArrayList PubReportList(string statflag)
        {
            string strSelect = string.Format("select * from gfhz_main where is_valid = '{0}'", statflag);

            return this.myPubReport(strSelect);
        }
        /// <summary>
        /// 根据费用类型查询所有gfhz_main
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="fee_type">费用类型,1住院,2门诊</param>
        /// <returns></returns>
        public ArrayList GetGfhzByFeeType(string begin, string end, string fee_type)
        {

            string strSql = "select * from gfhz_main where static_month>to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                "and static_month<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') " +
                "and fee_type = '{2}' ";
            strSql = string.Format(strSql, begin, end, fee_type);

            return this.myPubReport(strSql);
        }
        /// <summary>
        /// 根据费用类型查询单独gfhz_main
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="fee_type">费用类型,1住院,2门诊</param>
        /// <returns></returns>
        public ArrayList GetGfhzByFeeType(string begin, string end, string fee_type, string InpationNo)
        {

            string strSql = "select * from gfhz_main where static_month>to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                "and static_month<=to_date('{1}','yyyy-mm-dd hh24:mi:ss') " +
                "and fee_type = '{2}' and inpatient_no='{3}'";
            strSql = string.Format(strSql, begin, end, fee_type, InpationNo);

            return this.myPubReport(strSql);
        }

        //地区信息-PAT
        public ArrayList GetGfDiQu()
        {
            string strSql = "SELECT distinct y.INPUT_CODE, y.Mark FROM COM_DICTIONARY Y WHERE Y.TYPE='GFBBNZT'";
          //  strSql = string.Format(strSql, begin, end, fee_type, InpationNo);
            ArrayList al = new ArrayList();
             if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.Name = this.Reader[0].ToString();//地区名字
                pinfo.ID = this.Reader[1].ToString();//地区注用
               // pinfo.User03 = this.Reader[2].ToString();//数量
                al.Add(pinfo);
            }
            return al;
 
        }


        //收费人员信息-PAT
        public ArrayList GetGfPeople(string type)
        {
            string strSql = "SELECT distinct e.empl_code,e.empl_name FROM COM_EMPLOYEE e ";
            string sqlwhere= string.Empty;
            if(type=="1")//门诊
            {
                sqlwhere = "where e.Dept_code='70F2'";
            }
            else if (type == "2")
            {
                sqlwhere = "where e.Dept_code='70F3'";
            }
            strSql += sqlwhere;
            //  strSql = string.Format(strSql, begin, end, fee_type, InpationNo);
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject pinfo = new FS.FrameWork.Models.NeuObject();
                pinfo.Name = this.Reader[1].ToString();//人员名字
                pinfo.ID = this.Reader[0].ToString();//人员注用
                // pinfo.User03 = this.Reader[2].ToString();//数量
                al.Add(pinfo);
            }
            return al;

        }






        //取收费处的信息
        public ArrayList GetInvoide(string feetype)
        {
//            string strSql = "select Y.INPUT_CODE as Typenew, sum(value(pay_cost,0)) as PayCost,
//sum(value(pub_cost,0)) as PubCost ,
//count(case cancel_flag when '1' 
// then invoice_no end) as CountNum
//from fin_opb_invoiceinfo 
//where paykind_code='03'
//    and oper_code='{0}'
//      and balance_flag='{1}'
//      and (oper_date between to_date('2011-03-11 00:00:00','yyyy-MM-dd hh24:mi:ss') 
//      and to_date('2011-03-12 00:00:00','yyyy-MM-dd hh24:mi:ss'))
//            and cancel_flag<>'3'
//            --and cancel_flag<>'2'
//            and substring(mcard_no,1,2) in
//            (
//      SELECT distinct y.INPUT_CODE,y.MARK FROM COM_DICTIONARY Y WHERE Y.TYPE='GFBBNZT' group by y.INPUT_CODE AND y.MARK not in ('ZGYL','TS','YT') ORDER BY Y.MARK
//            --'80','81','82','83'
//            )'";
            //  strSql = string.Format(strSql, begin, end, fee_type, InpationNo);
            ArrayList al = new ArrayList();
            //if (this.ExecQuery(strSql) == -1)
            //    return null;
            return null;
 
        }


        /// <summary>
        /// 该月是否已经封账
        /// </summary>
        /// <param name="staticMonth"></param>
        /// <returns></returns>
        public bool MonthLocked(DateTime staticMonth)
        {
            string sql = "select s.LOCKED from gfhz_static s where  s.YEAR = '{0}'  and s.MONTH = '{1}'";
            try
            {
                sql = string.Format(sql, staticMonth.Year.ToString(), staticMonth.Month.ToString().PadLeft(2, '0'));
            }
            catch
            {}
            string strR = this.ExecSqlReturnOne(sql);
            if (strR == "1")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 对该月封账
        /// </summary>
        /// <param name="staticMonth"></param>
        /// <returns></returns>
        public int MonthLock(DateTime staticMonth)
        {
            string sql = @"	update gfhz_static s
								set s.LOCKED = '1'
								where s.YEAR = '{0}'
								and s.MONTH = '{1}'";
            try
            {
                sql = string.Format(sql, staticMonth.Year.ToString(), staticMonth.Month.ToString().PadLeft(2,'0'));
            }
            catch
            {
            }
            return this.ExecNoQuery(sql);

        }

        /// <summary>
        /// 对该月解封
        /// </summary>
        /// <param name="staticMonth"></param>
        /// <returns></returns>
        public int MonthUnLock(DateTime staticMonth)
        {
            string sql = @"	update gfhz_static s
								set s.LOCKED = '0'
								where s.YEAR = '{0}'
								and s.MONTH = '{1}'";
            try
            {
                sql = string.Format(sql, staticMonth.Year.ToString(), staticMonth.Month.ToString().PadLeft(2, '0'));
            }
            catch
            {
            }
           return this.ExecNoQuery(sql);
        }

        #endregion

        #region 托收单维护
        /// <summary>
        /// 保存SetUpdateGfhz扩展属性数据－－先执行更新操作，如果没有找到可以更新的数据，则插入一条新记录
        /// </summary>
        /// <param name="departmentExt">科室扩展属性实体</param>
        /// <returns>1成功 -1失败</returns>
        public int SetUpdateGfhz(SOC.Local.PubReport.Models.PubReport item)
        {
            int parm;
            //执行更新操作
            parm = UpdatePubReport(item);

            //如果没有找到可以更新的数据，则插入一条新记录
            if (parm == 0)
            {
                parm = InsertPubReport(item);
            }
            return parm;
        }
        #endregion

        #region 生成公费报表统计


        public ArrayList  AnasysClinicPubFeeStat(DateTime month)
        {
            #region sql
////            string sql = @"  with gf ( BH,
////	           co,
////       fy1,
////       fy2,
////       fy3,
////       fy4,
////       fy5,
////       fy6,
////       fy7,
////       fy8,
////       fy9,
////       fy10,
////       fy11,
////       fy12,
////       fy13,
////       fy14,
////       fy15,
////       fy16,
////       fy17,
////       fy18,
////       fy19,
////       fy20,
////       fy51,
////       sp_tot,
////       jzzje,
////       sjzje,
////       cancerDrug,
////       overLimitDrug,
////       selfPay,
////       totalFee) as 
////			 (select
////      g.BH,
////	    count(*),
////       sum(g.fy1),
////       sum(g.fy2),
////       sum(g.fy3),
////       sum(g.fy4),
////       sum(g.fy5),
////       sum(g.fy6),
////       sum(g.fy7),
////       sum(g.fy8),
////       sum(g.fy9),
////       sum(g.fy10),
////       sum(g.fy11),
////       sum(g.fy12),
////       sum(g.fy13),
////       sum(g.fy14),
////       sum(g.fy15),
////       sum(g.fy16),
////       sum(g.fy17),
////       sum(g.fy18),
////       sum(g.fy19),
////       sum(g.fy20),
////       sum(g.fy51),
////       sum(g.sp_tot),
////       sum(g.jzzje),
////       sum(g.sjzje),
////       sum(g.CANCERDRUGFEE),
////       sum(g.OVERLIMITDRUGFEE),
////       sum(g.SELFPAY),
////       sum(g.TOTALFEE)
//// from gfhz_main g 
//// where g.static_month = '{0}'
//// and g.FEE_TYPE = '1'
//// and g.IS_VALID = '1'
//// group by g.BH) 
//// select 
////   a.code,
////	    value(co,0),
////        value(fy1,0),
////        value(fy2,0),
////        value(fy3,0),
////        value(fy4,0),
////        value(fy5,0),
////        value(fy6,0),
////        value(fy7,0),
////        value(fy8,0),
////        value(fy9,0),
////        value(fy10,0),
////        value(fy11,0),
////        value(fy12,0),
////        value(fy13,0),
////        value(fy14,0),
////        value(fy15,0),
////        value(fy16,0),
////        value(fy17,0),
////        value(fy18,0),
////        value(fy19,0),
////        value(fy20,0),
////        value(fy51,0),
////        value(sp_tot,0),
////        value(jzzje,0),
////        value(sjzje,0),
////        value(cancerDrug,0),
////        value(overLimitDrug,0),
////        value(selfPay,0),
////        value(totalFee,0)
//// from gf right join (select * from com_dictionary where type = 'GFBBNZT')  a
//// on gf.bh = a.code
////
////union 
//// 
//// select
////       g.BH||'M',
////	   count(*),
////       sum(g.fy1),
////       sum(g.fy2),
////       sum(g.fy3),
////       sum(g.fy4),
////       sum(g.fy5),
////       sum(g.fy6),
////       sum(g.fy7),
////       sum(g.fy8),
////       sum(g.fy9),
////       sum(g.fy10),
////       sum(g.fy11),
////       sum(g.fy12),
////       sum(g.fy13),
////       sum(g.fy14),
////       sum(g.fy15),
////       sum(g.fy16),
////       sum(g.fy17),
////       sum(g.fy18),
////       sum(g.fy19),
////       sum(g.fy20),
////       sum(g.fy51),
////       sum(g.sp_tot),
////       sum(g.jzzje),
////       sum(g.sjzje),
////       sum(g.CANCERDRUGFEE),
////       sum(g.OVERLIMITDRUGFEE),
////       sum(g.SELFPAY),
////       sum(g.TOTALFEE)
//// from gfhz_main g
//// where g.static_month = '{0}'
//// and g.FEE_TYPE = '1'
//// and g.IS_VALID = '1'
//// and g.PAY_RATE = 0
//// and left(g.BH,1) in ('8','9','J') 
//// group by g.BH
////";
            string sql = @"  with gf as 
			 (select
      g.BH BH,
	    count(*) CO,
       sum(g.fy1) fy1,
       sum(g.fy2) fy2,
       sum(g.fy3) fy3,
       sum(g.fy4) fy4,
       sum(g.fy5) fy5,
       sum(g.fy6) fy6,
       sum(g.fy7) fy7,
       sum(g.fy8) fy8,
       sum(g.fy9) fy9,
       sum(g.fy10) fy10,
       sum(g.fy11) fy11,
       sum(g.fy12) fy12,
       sum(g.fy13) fy13,
       sum(g.fy14) fy14,
       sum(g.fy15) fy15,
       sum(g.fy16) fy16,
       sum(g.fy17) fy17,
       sum(g.fy18) fy18,
       sum(g.fy19) fy19,
       sum(g.fy20) fy20,
       sum(g.fy51) fy51,
       sum(g.sp_tot) sp_tot,
       sum(g.jzzje) jzzje,
       sum(g.sjzje) sjzje,
       sum(g.CANCERDRUGFEE) CANCERDRUG,
       sum(g.OVERLIMITDRUGFEE) OVERLIMITDRUG,
       sum(g.SELFPAY)  SELFPAY ,
       sum(g.TOTALFEE)  TOTALFEE
 from gfhz_main g 
 where g.static_month = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
 and g.FEE_TYPE = '1'
 and g.IS_VALID = '1'
 group by g.BH) 
 select 
  a.code,
      NVL(co,0),
        NVL(fy1,0),
        NVL(fy2,0),
        NVL(fy3,0),
        NVL(fy4,0),
        NVL(fy5,0),
        NVL(fy6,0),
        NVL(fy7,0),
        NVL(fy8,0),
        NVL(fy9,0),
        NVL(fy10,0),
        NVL(fy11,0),
        NVL(fy12,0),
        NVL(fy13,0),
        NVL(fy14,0),
        NVL(fy15,0),
        NVL(fy16,0),
        NVL(fy17,0),
        NVL(fy18,0),
        NVL(fy19,0),
        NVL(fy20,0),
        NVL(fy51,0),
        NVL(sp_tot,0),
        NVL(jzzje,0),
        NVL(sjzje,0),
        NVL(CANCERDRUG,0),
        NVL(OVERLIMITDRUG,0),
        NVL(selfPay,0),
        NVL(TOTALFEE,0)
 from gf right join (select * from com_dictionary where type = 'GFBBNZT')  a
 on gf.bh = a.code

union 
 
 select
       g.BH||'M',
	   count(*),
       sum(g.fy1),
       sum(g.fy2),
       sum(g.fy3),
       sum(g.fy4),
       sum(g.fy5),
       sum(g.fy6),
       sum(g.fy7),
       sum(g.fy8),
       sum(g.fy9),
       sum(g.fy10),
       sum(g.fy11),
       sum(g.fy12),
       sum(g.fy13),
       sum(g.fy14),
       sum(g.fy15),
       sum(g.fy16),
       sum(g.fy17),
       sum(g.fy18),
       sum(g.fy19),
       sum(g.fy20),
       sum(g.fy51),
       sum(g.sp_tot),
       sum(g.jzzje),
       sum(g.sjzje),
       sum(g.CANCERDRUGFEE),
       sum(g.OVERLIMITDRUGFEE),
       sum(g.SELFPAY),
       sum(g.TOTALFEE)
 from gfhz_main g
 where g.static_month = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
 and g.FEE_TYPE = '1'
 and g.IS_VALID = '1'
 and g.PAY_RATE = 0
 and SUBSTR(g.BH,0,1) in ('8','9','J') 
 group by g.BH
";
#endregion
            try
            {
                sql = string.Format(sql, month.ToString());
            }
            catch(Exception exp)
            {
                this.Err = exp.Message;
                return null;
            }
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            SOC.Local.PubReport.Models.PubReportStatic stat;            //返回数组中的摆药单分类信息
            if (this.ExecQuery(sql) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    stat = new SOC.Local.PubReport.Models.PubReportStatic();
                    try
                    {
                        int i = 0;
                        //stat.ClinicPub.Static_Month = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[i++].ToString());
                        //stat.ClinicPub.Bill_No = this.Reader[2].ToString();
                        stat.ClinicPub.Pact.Memo = this.Reader[i++].ToString();
                        stat.ClinicPub.Amount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[i++]);
                        stat.ClinicPub.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.TeZhi = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.TeZhiRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.SpDrugFeeSj = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);

                        #region [2010-02-02] zhaozf 修改公医报表添加
                        stat.ClinicPub.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.OverLimitDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.SelfPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.ClinicPub.TotalFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(stat);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            return al;
        }

        public ArrayList AnasysInPubFeeStat(DateTime month)
        {
            #region sql
            string sql = @"select
       g.BH,
			 count(*),
       sum(g.fy1),
       sum(g.fy2),
       sum(g.fy3),
       sum(g.fy4),
       sum(g.fy5),
       sum(g.fy6),
       sum(g.fy7),
       sum(g.fy8),
       sum(g.fy9),
       sum(g.fy10),
       sum(g.fy11),
       sum(g.fy12),
       sum(g.fy13),
       sum(g.fy14),
       sum(g.fy15),
       sum(g.fy16),
       sum(g.fy17),
       sum(g.fy18),
       sum(g.fy19),
       sum(g.fy20),
       sum(g.fy51),
       sum(g.sp_tot),
       sum(g.jzzje),
       sum(g.sjzje),
       sum(g.ZZS),
       sum(OVERLIMITDRUGFEE),
       sum(g.CANCERDRUGFEE),
       sum(g.BEDFEE_JIANHU),
       sum(g.BEDFEE_CENGLIU),
       sum(g.COMPANYFAY),
       sum(g.SELFPAY),
       sum(g.TOTALFEE)
 from gfhz_main g
 where g.static_month = '{0}'
 and g.FEE_TYPE = '2'
 and g.IS_VALID = '1'
 group by g.BH

union 
 
 select
       g.BH||'M',
	   count(*),
       sum(g.fy1),
       sum(g.fy2),
       sum(g.fy3),
       sum(g.fy4),
       sum(g.fy5),
       sum(g.fy6),
       sum(g.fy7),
       sum(g.fy8),
       sum(g.fy9),
       sum(g.fy10),
       sum(g.fy11),
       sum(g.fy12),
       sum(g.fy13),
       sum(g.fy14),
       sum(g.fy15),
       sum(g.fy16),
       sum(g.fy17),
       sum(g.fy18),
       sum(g.fy19),
       sum(g.fy20),
       sum(g.fy51),
       sum(g.sp_tot),
       sum(g.jzzje),
       sum(g.sjzje),
       sum(g.ZZS),
       sum(OVERLIMITDRUGFEE),
       sum(g.CANCERDRUGFEE),
       sum(g.BEDFEE_JIANHU),
       sum(g.BEDFEE_CENGLIU),
       sum(g.COMPANYFAY),
       sum(g.SELFPAY),
       sum(g.TOTALFEE)
 from gfhz_main g
 where g.static_month = '{0}'
 and g.FEE_TYPE = '2'
 and g.IS_VALID = '1'
 and g.PAY_RATE = 0
 and substr(g.BH,0,1) in ('8','9','J') 
 group by g.BH
";

            #endregion

            try
            {
                sql = string.Format(sql, month.ToString());
            }
            catch (Exception exp)
            {
                this.Err = exp.Message;
                return null;
            }

            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            SOC.Local.PubReport.Models.PubReportStatic stat;            //返回数组中的摆药单分类信息
            if (this.ExecQuery(sql) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    stat = new SOC.Local.PubReport.Models.PubReportStatic();
                    try
                    {
                        #region 住院部分
                        int i = 0;
                        //stat.InPub.Static_Month = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[i++].ToString());
                        //stat.ClinicPub.Bill_No = this.Reader[2].ToString();
                        stat.InPub.Pact.Memo = this.Reader[i++].ToString();
                        stat.InPub.Amount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[i++]);
                        stat.InPub.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.TeZhi = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.TeZhiRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.SpDrugFeeSj = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        
                        stat.InPub.User01 = this.Reader[i++].ToString();  //住院天数

                        #region [2010-02-02] zhaozf 修改公医报表添加
                        stat.InPub.OverLimitDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.BedFee_JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.BedFee_CengLiu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.CompanyPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.SelfPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        stat.InPub.TotalFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++]);
                        #endregion

                        #endregion

                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(stat);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            return al;

        }

        public ArrayList AnasysPubFeeStat(DateTime month)
        {
            ArrayList alPubFeeStat = new ArrayList();
            ArrayList alClinicPubFeeStat = this.AnasysClinicPubFeeStat(month);
            ArrayList alInPubFeeStat = this.AnasysInPubFeeStat(month);
            Hashtable hsPubFeeStat = new Hashtable();  //加载已经出来的统计项
            foreach (SOC.Local.PubReport.Models.PubReportStatic clinicStat in alClinicPubFeeStat)
            {
                if (!hsPubFeeStat.Contains(clinicStat.ClinicPub.Pact.Memo))
                {
                    hsPubFeeStat.Add(clinicStat.ClinicPub.Pact.Memo, clinicStat);
                }
                else
                {
                    hsPubFeeStat[clinicStat.ClinicPub.Pact.Memo] = clinicStat;
                }
            }
            foreach (SOC.Local.PubReport.Models.PubReportStatic inStat in alInPubFeeStat)
            {
                if (hsPubFeeStat.Contains(inStat.InPub.Pact.Memo))
                {
                    SOC.Local.PubReport.Models.PubReportStatic stat = hsPubFeeStat[inStat.InPub.Pact.Memo] as SOC.Local.PubReport.Models.PubReportStatic;
                    stat.InPub = inStat.InPub;
                }
                else
                {
                    inStat.ClinicPub.Pact.Memo = inStat.InPub.Pact.Memo;      //字头
                    inStat.ClinicPub.Pact.User01 = inStat.InPub.Pact.User01;  //公费大类
                    hsPubFeeStat.Add(inStat.InPub.Pact.Memo, inStat);
                }
            }
            foreach (SOC.Local.PubReport.Models.PubReportStatic stat in hsPubFeeStat.Values)
            {
                alPubFeeStat.Add(stat);
            }
            return alPubFeeStat;
        }

        private string[] GetParms(SOC.Local.PubReport.Models.PubReportStatic stat)
        {
            string[] strParm ={   
								 stat.ClinicPub.Static_Month.ToString(),
								 stat.ClinicPub.Pact.Memo,   // 字头
								 stat.ClinicPub.Amount.ToString(),
								 stat.ClinicPub.Bed_Fee.ToString(),
								 stat.ClinicPub.YaoPin.ToString() , 
								 stat.ClinicPub.ChengYao.ToString() ,
								 stat.ClinicPub.HuaYan.ToString(),
								 stat.ClinicPub.JianCha.ToString(),
								 stat.ClinicPub.FangShe.ToString() , 
								 stat.ClinicPub.ZhiLiao.ToString() , 
								 stat.ClinicPub.ShouShu.ToString() , 
								 stat.ClinicPub.ShuXue.ToString() , 
								 stat.ClinicPub.ShuYang.ToString() ,
								 stat.ClinicPub.TeZhi.ToString() ,
								 stat.ClinicPub.SpDrugFeeSj.ToString() , 
								 stat.ClinicPub.MR.ToString() , 
								 stat.ClinicPub.CT.ToString() , 
								 stat.ClinicPub.XueTou.ToString(), 
								 stat.ClinicPub.ZhenJin.ToString()  , 
								 stat.ClinicPub.CaoYao.ToString() , 
								 stat.ClinicPub.TeJian.ToString()  , 
								 stat.ClinicPub.ShenYao.ToString()  ,
								 stat.ClinicPub.JianHu.ToString() , 
								 stat.ClinicPub.ShengZhen.ToString()  , 
								 stat.ClinicPub.Tot_Cost.ToString()  ,
								 stat.ClinicPub.Pub_Cost.ToString(),
                                  
								 stat.InPub.Amount.ToString(),
								 stat.InPub.Bed_Fee.ToString(),
								 stat.InPub.YaoPin.ToString() , 
								 stat.InPub.ChengYao.ToString() ,
								 stat.InPub.HuaYan.ToString(),
								 stat.InPub.JianCha.ToString(),
								 stat.InPub.FangShe.ToString() , 
								 stat.InPub.ZhiLiao.ToString() , 
								 stat.InPub.ShouShu.ToString() , 
								 stat.InPub.ShuXue.ToString() , 
								 stat.InPub.ShuYang.ToString() ,
								 stat.InPub.TeZhi.ToString() ,
								 stat.InPub.SpDrugFeeSj.ToString() , 
								 stat.InPub.MR.ToString() , 
								 stat.InPub.CT.ToString() , 
								 stat.InPub.XueTou.ToString(), 
								 stat.InPub.ZhenJin.ToString()  , 
								 stat.InPub.CaoYao.ToString() , 
								 stat.InPub.TeJian.ToString()  , 
								 stat.InPub.ShenYao.ToString()  ,
								 stat.InPub.JianHu.ToString() , 
								 stat.InPub.ShengZhen.ToString()  , 
								 stat.InPub.Tot_Cost.ToString()  ,
								 stat.InPub.Pub_Cost.ToString(),
                                 stat.InPub.User01,
                                 stat.ClinicPub.CancerDrugFee.ToString(),      //门诊审批肿瘤药费
                                 stat.ClinicPub.OverLimitDrugFee.ToString(),   //门诊药费超标金额
                                stat.ClinicPub.SelfPay.ToString(),             //门诊自费金额
                                stat.ClinicPub.TotalFee.ToString(),            //门诊医疗费总金额
                                stat.InPub.CancerDrugFee.ToString(),           //住院审批肿瘤药费
                                stat.InPub.BedFee_JianHu.ToString(),           //住院监护床位费
                                stat.InPub.BedFee_CengLiu.ToString(),          //住院层流床位费
                                stat.InPub.OverLimitDrugFee.ToString(),        //住院药费超标金额
                                stat.InPub.CompanyPay.ToString(),              //住院单位支付金额
                                stat.InPub.SelfPay.ToString(),                 //住院自费金额
                                stat.InPub.TotalFee.ToString()                 //住院医疗费总额                                 
							 };

            return strParm;
        }

        public int InsertPubFeeStat(SOC.Local.PubReport.Models.PubReportStatic stat)
        {
            string strSql = "";

            strSql = @"insert into gfhz_report 
(TJYF,PACT_HEAD,MZRS,MZFY1,MZFY2,MZFY3,MZFY4,MZFY5,MZFY6,MZFY7,MZFY8,MZFY9,MZFY10,MZFY11,MZFY12,MZFY13,MZFY14,MZFY15,MZFY16,MZFY17,MZFY18,MZFY19,MZFY20,MZFY51,MZJZZJ,MZSJJZ,ZYRS,ZYFY1,ZYFY2,ZYFY3,ZYFY4,ZYFY5,ZYFY6,ZYFY7,ZYFY8,ZYFY9,ZYFY10,ZYFY11,ZYFY12,ZYFY13,ZYFY14,ZYFY15,ZYFY16,ZYFY17,ZYFY18,ZYFY19,ZYFY20,ZYFY51,ZYJZZJ,ZYSJJZ,ZYTS,IS_LX,REPORT_KIND,REPORT_LABEL,SORT_ID,
            MZCANCERDRUG,       --门诊审批肿瘤药费            
            MZOVERLIMITDRUG,    --门诊药费超标金额
            MZSELFPAY,          --门诊自费金额
            MZTOTALFEE,         --门诊医疗费总金额
            ZYCANCERDRUG,       --住院审批肿瘤药费
            ZYBEDFEE_JIANHU,    --住院监护床位费
            ZYBEDFEE_CENGLIU,   --住院层流床位费
            ZYOVERLIMITDRUG,    --住院药费超标金额
            ZYCOMPANYPAY,       --住院单位支付金额
            ZYSELFPAY,          --住院自费金额
            ZYTOTALFEE)         --住院医疗费总额
values
(to_date('{0}','yyyy-mm-dd hh24:mi:ss'),'{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47},{48},{49},{50},'','','','',
{51},{52},{53},{54},{55},{56},{57},{58},{59},{60},{61})";
           
            try
            {

                string[] strParm = GetParms(stat);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        public int UpdatePubFeeStat(SOC.Local.PubReport.Models.PubReportStatic stat)
        {
            string strSql = "";

            strSql = @"update  gfhz_report g 
set 
MZRS = {2},
MZFY1 = {3},
MZFY2 = {4},
MZFY3 = {5},
MZFY4 = {6},
MZFY5 = {7},
MZFY6 = {8},
MZFY7 = {9},
MZFY8 = {10},
MZFY9 = {11},
MZFY10 = {12},
MZFY11 = {13},
MZFY12 = {14},
MZFY13 = {15},
MZFY14 = {16},
MZFY15 = {17},
MZFY16 = {18},
MZFY17 = {19},
MZFY18 = {20},
MZFY19 = {21},
MZFY20 = {22},
MZFY51 = {23},
MZJZZJ = {24},
MZSJJZ = {25},
ZYRS = {26},
ZYFY1 = {27},
ZYFY2 = {28},
ZYFY3 = {29},
ZYFY4 = {30},
ZYFY5 = {31},
ZYFY6 = {32},
ZYFY7 = {33},
ZYFY8 = {34},
ZYFY9 = {35},
ZYFY10 = {36},
ZYFY11 = {37},
ZYFY12 = {38},
ZYFY13 = {39},
ZYFY14 = {40},
ZYFY15 = {41},
ZYFY16 = {42},
ZYFY17 = {43},
ZYFY18 = {44},
ZYFY19 = {45},
ZYFY20 = {46},
ZYFY51 = {47},
ZYJZZJ = {48},
ZYSJJZ = {49},
ZYTS = {50},
IS_LX = '',
REPORT_KIND = '',
REPORT_LABEL = '',
SORT_ID = '',
MZCANCERDRUG = {51},       --门诊审批肿瘤药费            
MZOVERLIMITDRUG = {52},    --门诊药费超标金额
MZSELFPAY = {53},          --门诊自费金额
MZTOTALFEE = {54},         --门诊医疗费总金额
ZYCANCERDRUG = {55},       --住院审批肿瘤药费
ZYBEDFEE_JIANHU = {56},    --住院监护床位费
ZYBEDFEE_CENGLIU = {57},   --住院层流床位费
ZYOVERLIMITDRUG = {58},    --住院药费超标金额
ZYCOMPANYPAY = {59},       --住院单位支付金额
ZYSELFPAY = {60},          --住院自费金额
ZYTOTALFEE = {61}         --住院医疗费总额
where TJYF = to_date('{0}','yyyy-mm-dd hh24:mi:ss') and PACT_HEAD = '{1}'";

            try
            {

                string[] strParm = GetParms(stat);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region 特殊检查管理

        public ArrayList QuerySpecialCheck(string sqlWhere,params string[] parm)
        {
            string sqlSelect = "select s.CHECK_DATE, s.CHECK_RESULT, s.CT, s.DIAGNOZE, s.GAOFEN, s.INPATIENT_NO, s.ITEM_NAME, s.MCARD_NO, s.NAME, s.PATIENT_NO, s.PAY_RATE, s.PIANFEI, s.PUB_COST, s.SEQ, s.TOT_COST, s.WORK_PLACE, s.XIANGJI, s.XIANYIN, s.ZHUSHI,s.invoiceNo,s.static_month from gfhz_specialcheck s ";
            string sql = sqlSelect + sqlWhere;
            try
            {
                sql = string.Format(sql, parm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            SOC.Local.PubReport.Models.SpecialCheck obj;
           
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                try
                {
                    int i = 0;
                    obj = new SOC.Local.PubReport.Models.SpecialCheck();
                    obj.CheckDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[i++].ToString());
                    obj.CheckResult = this.Reader[i++].ToString();
                    obj.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++].ToString());
                    obj.Diagnoze = this.Reader[i++].ToString();
                    obj.Gaofen = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++].ToString());
                    obj.InpatientNo = this.Reader[i++].ToString();
                    obj.ItemName = this.Reader[i++].ToString();
                    //s.MCARD_NO, s.NAME, s.PATIENT_NO, s.PAY_RATE, s.PIANFEI, s.PUB_COST, s.SEQ, s.TOT_COST, s.WORK_PLACE, s.XIANGJI, s.XIANYIN, s.ZHUSHI
                    obj.McardNo = this.Reader[i++].ToString();
                    obj.Name = this.Reader[i++].ToString();
                    obj.PatientNo = this.Reader[i++].ToString();
                    obj.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++].ToString());
                    obj.Pianfei = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++].ToString());
                    obj.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++].ToString());
                    obj.Seq = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[i++].ToString());
                    obj.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++].ToString());
                    obj.WorkPlace = this.Reader[i++].ToString();
                    obj.Xiangji = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++].ToString());
                    obj.Xianyin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++].ToString());
                    obj.Zhushi = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[i++].ToString());
                    obj.InvoiceNo = this.Reader[i++].ToString();
                    obj.StaticMonth = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[i++].ToString());
                    al.Add(obj);
                }
                catch (Exception ex)
                {
                    this.Reader.Close();
                    this.Err = this.Err + ex.Message;
                    return null;
                }
            }
            this.Reader.Close();
            return al;
        }

        public ArrayList QuerySpecailCheck(string inpatientNo,string invoNo)
        {
            string where = " where inpatient_no = '{0}' and invoiceno = '{1}'";
            return QuerySpecialCheck(where, inpatientNo,invoNo);
        }

        public int GetSpecialCheckSeq(string inpatientNo)
        {
            string sql = "select  max(g.SEQ) from gfhz_specialcheck g where inpatient_no = '{0}'";
            try
            {
                sql = string.Format(sql, inpatientNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            string i = ExecSqlReturnOne(sql);
            int seq = 0;
            try
            {
                seq = FS.FrameWork.Function.NConvert.ToInt32(i);
            }
            catch 
            {
                return 1;
            }
            return seq + 1;

        }

        private string[] GetParmFromSpecialCheck(SOC.Local.PubReport.Models.SpecialCheck obj)
        {
            string[] s = new string[21];
            s[0] = obj.CheckDate.ToString() ;
            s[1] = obj.CheckResult ;
            s[2] = obj.CT.ToString() ;
            s[3] = obj.Diagnoze ;
            s[4] = obj.Gaofen.ToString() ;
            s[5] = obj.InpatientNo ;
            s[6] = obj.ItemName;
            s[7] = obj.McardNo ;
            s[8] = obj.Name ;
            s[9] = obj.PatientNo ;
            s[10] = obj.PayRate.ToString() ;
            s[11] = obj.Pianfei.ToString() ;
            s[12] = obj.PubCost.ToString() ;
            s[13] = obj.Seq.ToString();
            s[14] = obj.TotCost.ToString() ;
            s[15] = obj.WorkPlace ;
            s[16] = obj.Xiangji.ToString() ;
            s[17] = obj.Xianyin.ToString() ;
            s[18] = obj.Zhushi.ToString() ;
            s[19] = obj.InvoiceNo;
            s[20] = obj.StaticMonth.ToString();
            return s;
        }

        public int InsertSpecialCheck(SOC.Local.PubReport.Models.SpecialCheck obj)
        {
            string sql = @"	insert into GFHZ_SPECIALCHECK (CHECK_DATE, CHECK_RESULT, CT, DIAGNOZE, GAOFEN, INPATIENT_NO, ITEM_NAME, MCARD_NO, NAME, PATIENT_NO, PAY_RATE, PIANFEI, PUB_COST, SEQ, TOT_COST, WORK_PLACE, XIANGJI, XIANYIN, ZHUSHI,invoiceno,static_month) 
	values (to_Date('{0}','yyyy-mm-dd hh24:mi:ss'),'{1}',{2},'{3}',{4},'{5}','{6}','{7}','{8}','{9}',{10},{11},{12},{13},{14},'{15}',{16},{17},{18},'{19}','{20}')";
            try
            {
                sql = string.Format(sql, GetParmFromSpecialCheck(obj));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return ExecNoQuery(sql);
        }

        public int UpdateSpecialCheck(SOC.Local.PubReport.Models.SpecialCheck obj)
        {
            string sql = @"	  update GFHZ_SPECIALCHECK 
	set CHECK_DATE = to_Date('{0}','yyyy-mm-dd hh24:mi:ss'),
	CHECK_RESULT = '{1}',
	CT = {2}, 
	DIAGNOZE = '{3}',
	GAOFEN = {4},  
	ITEM_NAME = '{6}', 
	MCARD_NO = '{7}',  
	NAME = '{8}', 
	PATIENT_NO = '{9}',
	PAY_RATE = {10}, 
	PIANFEI = {11}, 
	PUB_COST = {12}, 
	TOT_COST = {14}, 
	WORK_PLACE = '{15}', 
	XIANGJI = {16}, 
	XIANYIN = {17}, 
	ZHUSHI = {18},
    invoiceno = '{19}',
    static_month = '{20}'
	where INPATIENT_NO = '{5}'
	and SEQ = {13}";
            try
            {
                sql = string.Format(sql, GetParmFromSpecialCheck(obj));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return ExecNoQuery(sql);
        }

        public int DeleteSpecialCheck(SOC.Local.PubReport.Models.SpecialCheck obj)
        {
            string sql = @"	  delete from GFHZ_SPECIALCHECK
	where INPATIENT_NO = '{5}'
	and SEQ = {13}";
            try
            {
                sql = string.Format(sql, GetParmFromSpecialCheck(obj));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return ExecNoQuery(sql);
        }

        
        public DataSet GetSpecialCheck(DateTime month,string eare)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = "";
                if (this.Sql.GetSql("Local.Pub.GetSpecialCheck.1", ref strSql) == -1)
                    return null;

                strSql = string.Format(strSql, month, eare);
                this.ExecQuery(strSql, ref ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        #endregion
    }
}
