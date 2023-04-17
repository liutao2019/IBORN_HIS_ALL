using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.GuangZhou.InPatient
{
    public class UpLoadFeeDetail : IBorn.SI.GuangZhou.AbstractService<FS.HISFC.Models.RADT.PatientInfo, string>
    {
        public override bool Transcation
        {
            get { return true; }
        }

        protected override int Excute(FS.HISFC.Models.RADT.PatientInfo r, ref System.Data.DataTable dt, params object[] appendParams)
        {
            if (appendParams == null || appendParams.Length == 0)
            {
                this.ErrorMsg = "门诊费用上传没有数据，请检查是否正确";
                return -1;
            }
            else if (appendParams[0] is System.Data.DataTable)
            {
                if (appendParams.Length < 2)
                {
                    this.ErrorMsg = "门诊费用上传参数错误，请传入操作时间。";
                    return -1;
                }
                string sql = string.Format("delete from HIS_CFXM where JYDJH = '{0}'", r.SIMainInfo.RegNo);
                int ret = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(sql);
                if (ret < 0)
                {
                    this.ErrorMsg = "删除已上传的住院费用明细失败";
                    return -1;
                }

                sql = string.Format("delete from HIS_CFXM_JKSYBZ where JYDJH = '{0}'", r.SIMainInfo.RegNo);
                ret = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(sql);
                if (ret < 0)
                {
                    this.ErrorMsg = "删除已上传的限制性用药明细失败";
                    return -1;
                }

                try
                {
                    DateTime operDate = FS.FrameWork.Function.NConvert.ToDateTime(appendParams[1]);
                    System.Data.DataTable dtFeeDetail = appendParams[0] as System.Data.DataTable;
                    int res = 1;
                    foreach (System.Data.DataRow dr in dtFeeDetail.Rows)
                    {
                        string strSQL = string.Format(@"INSERT INTO HIS_CFXM (
JYDJH, YYBH, GMSFHM, ZYH, RYRQ, FYRQ, XMXH, XMBH, XMMC,FLDM, YPGG, YPJX, JG, MCYL, JE, BZ1, BZ2, BZ3, DRBZ, YPLY)
VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},{14},'{15}','{16}','{17}',{18},'{19}')",
                        r.SIMainInfo.RegNo,                         //0 就诊登记号
                        r.SIMainInfo.HosNo,                         //1 医院编码
                        r.IDCard,                                   //2 证件号
                        r.PID.PatientNO,                            //3 住院号(门诊这里是门诊号)
                        operDate.ToString("yyyy-MM-dd HH:mm:ss"),   //4 操作时间
                        dr["FYRQ"].ToString(),                      //5 费用记账时间
                        dr["XMXH"].ToString(),                      //6 序号
                        dr["XMBH"].ToString(),                      //7 自定义码  
                        dr["XMMC"].ToString(),                      //8 项目名称
                        dr["FLDM"].ToString(),                     //9 分类编码
                        dr["YPGG"].ToString(),                     //10 规格
                        dr["YPJX"].ToString(),                     //11 剂型
                        dr["JG"].ToString(),                       //12 价格
                        dr["MCYL"].ToString(),                     //13 数量
                        dr["JE"].ToString(),                       //14 总金额
                        dr["BZ1"].ToString(),                      //15 备注1 存放记录保存到医保系统的时间，格式为YYYY-MM-DD hh:mm:ss
                        dr["BZ2"].ToString(),                       //16 备注2
                        dr["BZ3"].ToString(),                       //17 备注3 存放费用明细读入时有效性检查的处理结果代码
                        dr["DRBZ"].ToString(),                  //18 读入标志
                        dr["YPLY"].ToString()                  //19 药品来源 1-国产 2-进口 3-合资
                     );
                        res = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(strSQL);
                        if (res < 0)
                        {
                            return -1;
                        }

                        //{B2DB4B07-CF78-47ed-95DA-55713658A710}
                        //判断限制性用药
                        int limitRet = this.judgeLimitedMedicine(dr["XMBH"].ToString());
                        if (limitRet < 0)
                        {
                            return -1;
                        }

                        //限制性用药数据插入
                        if (limitRet > 0)
                        {
                            //{E543FFCE-DA98-4e63-BE11-12E0888ED4F7}
                            string indications = "0";

                            if (dtFeeDetail.Columns.Contains("indications") && dr["indications"] != null && !string.IsNullOrEmpty(dr["indications"].ToString()))
                            {
                                indications = dr["indications"].ToString();
                            }

                            string strLimitSQL = @"INSERT INTO HIS_CFXM_JKSYBZ (JYDJH, YYBH, GMSFHM, ZYH,  XMXH,
                                                                                 XMBH, XMMC, MCYL, JE,  AKA185,
                                                                                 BZ1, BZ2, BZ3)
                                                                         VALUES('{0}','{1}','{2}','{3}','{4}',
                                                                                '{5}','{6}','{7}','{8}','{9}',
                                                                                '{10}','{11}','{12}')";
                            strLimitSQL = string.Format(strLimitSQL,
                                                        r.SIMainInfo.RegNo,                         //0 就诊登记号
                                                        r.SIMainInfo.HosNo,                         //1 医院编码
                                                        r.IDCard,                                   //2 证件号
                                                        r.PID.PatientNO,                            //3 住院号(门诊这里是门诊号)
                                                        dr["XMXH"].ToString(),                      //4 序号
                                                        dr["XMBH"].ToString(),                      //5 自定义码  
                                                        dr["XMMC"].ToString(),                      //6 项目名称
                                                        dr["MCYL"].ToString(),                      //7 数量
                                                        dr["JE"].ToString(),                        //8 总金额
                                                        indications,                                //9 限制性用药标记
                                                        dr["BZ1"].ToString(),                       //10 备注1 
                                                        dr["BZ2"].ToString(),                       //11 备注2
                                                        dr["BZ3"].ToString()                        //12 备注3 
                                                        );
                            res = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(strLimitSQL);
                            if (res < 0)
                            {
                                return -1;
                            }
                        }
                    }
                }
                catch 
                {
                    throw;
                }
            }
            return 1;
        }

        private int judgeLimitedMedicine(string xmbh)
        {
            try
            {
                string strQuerySql = string.Format(@"select count(*)
                                                       from view_medi t, view_match t2
                                                      where t.medi_code = t2.item_code
                                                        and t.valid_flag = '1'
                                                        and t.range_flag = '1'
                                                        and t2.hosp_code = '{0}'", xmbh);
                System.Data.DataTable dt = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteDataTable(strQuerySql);

                if (dt == null)
                {
                    return -1;
                }
                else
                {
                    return Int32.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
