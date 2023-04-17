using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.GuangZhou.OutPatient
{
    public class UpLoadFeeDetail : IBorn.SI.GuangZhou.AbstractService<FS.HISFC.Models.Registration.Register, string>
    {
        public override bool Transcation
        {
            get { return true; }
        }

        protected override int Excute(FS.HISFC.Models.Registration.Register r, ref System.Data.DataTable dt, params object[] appendParams)
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
                string sql = string.Format("delete from HIS_MZXM where JYDJH = '{0}'", r.SIMainInfo.RegNo);
                int ret = IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(sql);
                if (ret < 0)
                {
                    this.ErrorMsg = "删除已上传的门诊费用明细失败";
                    return -1;
                }
                DateTime operDate = FS.FrameWork.Function.NConvert.ToDateTime(appendParams[1]);
                System.Data.DataTable dtFeeDetail = appendParams[0] as System.Data.DataTable;
                int res = 1;
                foreach (System.Data.DataRow dr in dtFeeDetail.Rows)
                {
                    string strSQL = string.Format(@"INSERT INTO HIS_MZXM (
JYDJH, YYBH, GMSFHM, ZYH, RYRQ, FYRQ, XMXH, XMBH, XMMC,FLDM, YPGG, YPJX, JG, MCYL, JE, BZ1, BZ2, BZ3, DRBZ, YPLY)
VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},{14},'{15}','{16}','{17}',{18},'{19}')",
                    r.SIMainInfo.RegNo,                         //0 就诊登记号
                    r.SIMainInfo.HosNo,                         //1 医院编码
                    r.IDCard,                                   //2 证件号
                    r.PID.CardNO,                               //3 住院号(门诊这里是门诊号)
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
                }
            }
            return 1;
        }

        #region 屏蔽不用
        /*
        /// <summary>
        /// 插入单条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertShareData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, int seq,
            DateTime operDate)
        {
            r.SIMainInfo.HosNo = string.IsNullOrEmpty(r.SIMainInfo.HosNo) ? r.SIMainInfo.RegNo.Substring(0, 6) : r.SIMainInfo.HosNo;
            string strSQL = "INSERT INTO HIS_MZXM (JYDJH, YYBH, GMSFHM, ZYH, RYRQ, FYRQ, XMXH, XMBH, XMMC, " +
                "FLDM, YPGG, YPJX, JG, MCYL, JE, BZ1, BZ2, BZ3, DRBZ, YPLY)" +
                "VALUES('{0}','{1}','{2}','{3}',CONVERT(datetime,'{4}'),CONVERT(datetime,'{5}'), " +
                "{6},'{7}','{8}',{9},'{10}','{11}',{12},{13},'{14}','{15}','{16}','{17}',{18},'{19}')";
            string name = "";
            name = f.Item.Name;
            if (name.Length > 20)
            {
                name = name.Substring(0, 20);
            }
            try
            {
                decimal price = this.GetPrice(f.Item);
                decimal totCost = f.Item.Qty * price / f.Item.PackQty;

                if (!string.IsNullOrEmpty(f.Order.ID))
                {
                    seq = FS.FrameWork.Function.NConvert.ToInt32(f.Order.ID);
                }
                if (seq == 0)
                {
                    seq = FS.FrameWork.Function.NConvert.ToInt32(f.Order.SequenceNO);
                }

                strSQL = string.Format(strSQL,
                    r.SIMainInfo.RegNo,
                    r.SIMainInfo.HosNo,
                    r.IDCard,
                    r.PID.CardNO,
                    r.DoctorInfo.SeeDate.ToString(),
                    operDate.ToString(),
                    seq.ToString(),
                    f.Item.UserCode,
                    name,
                    "000",
                    f.Item.Specs,
                    r.MainDiagnose, //诊断
                    FS.FrameWork.Public.String.FormatNumber(price / f.Item.PackQty, 4),
                    f.Item.Qty,                 //总量Amount
                    totCost,//(f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString(),
                    "", "", "", "0", ""
                    );
            }
            catch (Exception ex)
            {
                this.ErrorMsg = ex.Message;
                return -1;
            }
            return IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(strSQL);
        }

        /// <summary>
        /// 插入单条适应症信息
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertIndicationsShareData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f,
            DateTime operDate)
        {
            decimal price = this.GetPrice(f.Item);
            string sql1 = "insert into HIS_CFXM_JKSYBZ values('" + r.SIMainInfo.RegNo + "','" +
                                                              r.SIMainInfo.HosNo + "','" +
                                                              r.IDCard + "','" +
                                                                "','" +
                                                              f.Order.ID.ToString() + "','" +
                                                              f.Item.UserCode + "','" +
                                                              f.Item.Name + "'," +
                                                              f.Item.Qty.ToString() + "," +
                                                              f.FT.TotCost.ToString() + ",'" +
                                                              f.Item.User03.ToString() + "','" +
                                                              "" + "','" + "" + "','" +
                                                              "" + "','" + "" + "')";
            if (IBorn.SI.GuangZhou.Base.SIDataBase.ExecuteNonQuery(sql1) == -1)
            {
                return -1;
            }
            return 1;
        }
        */
        #endregion
    }
}
