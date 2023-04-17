using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FoShanSI.Management
{
    /// <summary>
    /// DataAccess层
    /// 张琦 2010-7
    /// </summary>
    public class SIBizProcess:SIDataBase
    {
        public SIBizProcess() { }

        #region 门诊医保结算

        /// <summary>
        /// 插入门诊项目明细表
        /// </summary>
        /// <returns></returns>
        public int InsertOutPatientFeeItemDetail(FS.HISFC.Models.Registration.Register r,ArrayList feeDetails ,
            DateTime operDate)
        {
            // {7D2DF202-5493-40a6-8338-D9ED41D994B9}
            if (feeDetails == null || feeDetails.Count <= 0)
            {
                this.Err = "没有医保可报销的项目，请自费！";
                return -1;
            }
            string sqlInsert=@"insert into masmzhxm(hos_code,billno,xmcode,ass_sign,xmdes,
                            xmunt,xmqnt,xmprc,xmamt,trndate,trnflag,memoa,
                            u_version) values('{0}','{1}','{2}','{3}','{4}','{5}',{6},
                        {7},{8},date(timestamp('{9}')),'{10}','{11}','{12}')";
            string sqlInsertStr = "";
            bool isInit = false;
            this.ClearErrMsg();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
            {
                //数量为0不上传
                if (f.Item.Qty == 0)
                {
                    continue;
                }
                string itemName = f.Item.Name.Clone().ToString();
                if (f.Item.Name.Length > 10)
                {
                    itemName = f.Item.Name.Substring(0, 10);
                }

                string itemPriceUnit = f.Item.PriceUnit.Clone().ToString();
                if (f.Item.PriceUnit.Length > 1)
                {
                    itemPriceUnit = f.Item.PriceUnit.Substring(0, 1);
                }
                string strSpecs = f.Item.Specs;
                if (r.SIMainInfo.TransNo == "1")
                {
                    strSpecs = r.SIMainInfo.InvoiceNo;
                }
                else
                {
                    if (!string.IsNullOrEmpty(strSpecs))
                    {
                        if (strSpecs.Length > 15)
                        {
                            strSpecs = strSpecs.Substring(0, 15);
                        }
                    }
                    else
                    {
                        strSpecs = itemPriceUnit;
                    }
                }

                if (!isInit)
                {
                    sqlInsertStr = string.Format(sqlInsert, Function.Function.HospitalCode, r.SIMainInfo.RegNo,
                        //不知道为什么此处需要传入医院的自定义码？？
                        f.Item.UserCode,
                        // f.Item.Memo,
                        strSpecs, itemName, itemPriceUnit,
                        f.Item.Qty, f.Item.SpecialPrice,
                        (f.Item.Qty * f.Item.SpecialPrice).ToString("F2"),// {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20 
                        operDate, "-", "-", "-");

                    isInit = true;//初始化SQL结束
                }
                else
                {
                    sqlInsertStr += ",("
                        + Function.Function.AddMark(Function.Function.HospitalCode, "'", FoShanSI.Function.FillWay.BOTH)
                        + Function.Function.AddMark(r.SIMainInfo.RegNo, "'", FoShanSI.Function.FillWay.BOTH)
                        + Function.Function.AddMark(f.Item.UserCode, "'", FoShanSI.Function.FillWay.BOTH)
                        + Function.Function.AddMark(strSpecs, "'", FoShanSI.Function.FillWay.BOTH)
                        + Function.Function.AddMark(itemName, "'", FoShanSI.Function.FillWay.BOTH)   //2010-9-6
                        + Function.Function.AddMark(itemPriceUnit, "'", FoShanSI.Function.FillWay.BOTH)
                        + f.Item.Qty.ToString() + ","
                        + f.Item.SpecialPrice.ToString() + ","
                        + (f.Item.Qty * f.Item.SpecialPrice).ToString() + ","
                        + "date(timestamp('" + operDate.ToString() + "')),"
                        + Function.Function.AddMark("-", "'", FoShanSI.Function.FillWay.BOTH)
                        + Function.Function.AddMark("-", "'", FoShanSI.Function.FillWay.BOTH)
                        + Function.Function.AddMark("-", "'", FoShanSI.Function.FillWay.BOTH).Replace(",", "")
                        + ")";
                }
            }

            try
            {
                if (!string.IsNullOrEmpty(sqlInsertStr))
                {
                    return this.ExecNoQuery(sqlInsertStr);
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
            }
            return 1; 
        }

        /// <summary>
        /// 获取医保前台结算结果信息
        /// </summary>
        public string GetOutPatientBalanceHead(FS.HISFC.Models.Registration.Register register)
        {
            string sqlBalance = @"select medNo,hos_code,billNo,ass_sign,cbcwf,zfyp,ylyp,
                            zcyp,zcyf,gxylf,qtfy,lxqjf,lxfqjf,lxjkcl,
                            ylfyze,tczfje,
                            ylxma,ylxmb,ylxmc,ylxmd,
                            ylxme,trnflg,trndate,u_version from MASMZHZFJS where hos_code='{0}' and billno='{1}'
                                ";
            string balanceHead = string.Empty;
            this.ClearErrMsg();
            try
            {
                sqlBalance = string.Format(sqlBalance,Function.Function.HospitalCode, register.SIMainInfo.RegNo);
                if (this.ExecQuery(sqlBalance) == -1)
                {
                    return null;
                }
                while (siReader.Read())
                {
                    balanceHead = this.siReader.IsDBNull(0) ? "" : this.siReader[0].ToString()+"|"
                        + (this.siReader.IsDBNull(1) ? "" : this.siReader[1].ToString()) + "|"
                        + (this.siReader.IsDBNull(2) ? "" : this.siReader[2].ToString()) + "|"
                        + (this.siReader.IsDBNull(3) ? "" : this.siReader[3].ToString()) + "|"
                        + (this.siReader.IsDBNull(4) ? "" : this.siReader[4].ToString()) + "|"
                        + (this.siReader.IsDBNull(5) ? "" : this.siReader[5].ToString()) + "|"
                        + (this.siReader.IsDBNull(6) ? "" : this.siReader[6].ToString()) + "|"
                        + (this.siReader.IsDBNull(7) ? "" : this.siReader[7].ToString()) + "|"
                        + (this.siReader.IsDBNull(8) ? "" : this.siReader[8].ToString()) + "|"
                        + (this.siReader.IsDBNull(9) ? "" : this.siReader[9].ToString()) + "|"
                        + (this.siReader.IsDBNull(10) ? "" : this.siReader[10].ToString()) + "|"
                        + (this.siReader.IsDBNull(11) ? "" : this.siReader[11].ToString()) + "|"
                        + (this.siReader.IsDBNull(12) ? " " : this.siReader[12].ToString()) + "|"
                        + (this.siReader.IsDBNull(13) ? " " : this.siReader[13].ToString()) + "|"
                        + (this.siReader.IsDBNull(14) ? " " : this.siReader[14].ToString()) + "|"
                        + (this.siReader.IsDBNull(15) ? " " : this.siReader[15].ToString()) + "|"
                        + (this.siReader.IsDBNull(16) ? " " : this.siReader[16].ToString()) + "|"
                        + (this.siReader.IsDBNull(17) ? " " : this.siReader[17].ToString()) + "|"
                        + (this.siReader.IsDBNull(18) ? " " : this.siReader[18].ToString()) + "|"
                        + (this.siReader.IsDBNull(19) ? " " : this.siReader[19].ToString()) + "|"
                        + (this.siReader.IsDBNull(20) ? " " : this.siReader[20].ToString()) + "|"
                        + (this.siReader.IsDBNull(21) ? " " : this.siReader[21].ToString()) + "|"
                        + (this.siReader.IsDBNull(22) ? " " : this.siReader[22].ToString()) + "|"
                        + (this.siReader.IsDBNull(23) ? " " : this.siReader[23].ToString());
                }
            }
            catch (Exception ex)
            {
                balanceHead = Function.Function.errorText;
                this.ErrCode = "-1";
                this.Err = ex.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
                return null;
            }
            finally
            {
                if (this.siReader != null && !this.siReader.IsClosed)
                {
                    this.siReader.Close();
                }
            }
			
            return balanceHead;
        }

        /// <summary>
        /// 撤销明细上传
        /// </summary>
        /// <param name="objDel"></param>
        /// <returns></returns>
        public int DeleteOutPaientFeeItemDetail(string outPatientClinicCode)
        {
            string sqlDelete = @"delete from masmzhxm where hos_code='{0}' and billno='{1}'";
            this.ClearErrMsg();
            try
            {
                sqlDelete = string.Format(sqlDelete, Function.Function.HospitalCode, outPatientClinicCode);
                return this.ExecNoQuery(sqlDelete);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
            }

            return 1; 
        }

        /// <summary>
        /// 医保门诊患者是否已经结算
        /// </summary>
        /// <param name="outPatientClinicCode"></param>
        /// <returns></returns>
        public int GetOutPatientBalanceCount(string outPatientClinicCode)
        {
            string sqlBalance = @"select count(*) from MASMZHZFJS where hos_code='{0}' and billno='{1}'";
            this.ClearErrMsg();
            try
            {
                sqlBalance = string.Format(sqlBalance, Function.Function.HospitalCode, outPatientClinicCode);
                this.ExecQuery(sqlBalance);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
            }
            try
            {
                while (this.siReader.Read())
                {
                    string count = this.siReader.IsDBNull(0) ? "" : this.siReader[0].ToString();
                    return FS.FrameWork.Function.NConvert.ToInt32(count);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
            }
            finally
            {
                if (this.siReader != null && !this.siReader.IsClosed)
                {
                    this.siReader.Close();
                }
            }

            return 1;
        }

        #endregion

        #region 住院医保结算
        /// <summary>
        /// 撤销住院明细上传
        /// </summary>
        /// <param name="strInpatientNO"></param>
        /// <param name="InPatientTimes"></param>
        /// <returns></returns>
        public int DeleteInPaientFeeItemDetail(string strInpatientNO, int InPatientTimes)
        {
            string strSQL = @"delete from mashxm
 where hos_code = '{0}'
   and zyno = '{1}'
   and zysno = {2}";

            this.ClearErrMsg();
            try
            {
                strSQL = string.Format(strSQL, Function.Function.HospitalCode, strInpatientNO, InPatientTimes);
                return this.ExecNoQuery(strSQL);
            }
            catch (Exception objEx)
            {
                this.ErrCode = "-1";
                this.Err = objEx.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 查询是否已结算
        /// </summary>
        /// <param name="strInpatientNO"></param>
        /// <param name="InPatientTimes"></param>
        /// <returns>-1查询失败； 0 未结算 1 已结算</returns>
        public int QueryIsBalance(string strInpatientNO, int InPatientTimes)
        {
            string strSQL = @"SELECT a.TRNFLG FROM mashzfjs a WHERE a.HOS_CODE = '{0}' and a.ZYNO = '{1}' AND a.ZYSNO = {2}";

            this.ClearErrMsg();
            int iRes = 0;
            try
            {
                strSQL = string.Format(strSQL, Function.Function.HospitalCode, strInpatientNO, InPatientTimes);
                this.ExecNoQuery(strSQL);
                while (this.siReader.Read())
                {
                    string str = this.siReader[0].ToString();
                    if (str == "T")
                    {
                        iRes = 1;
                    }
                    else
                    {
                        iRes = 0;
                    }
                }

            }
            catch (Exception objEx)
            {
                this.ErrCode = "-1";
                this.Err = objEx.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ": " + this.Err + this.ErrCode);
                return -1;
            }
            finally
            {
                if (this.siReader != null && !this.siReader.IsClosed)
                {
                    this.siReader.Close();
                }
            }
            return iRes;
        }

        /// <summary>
        /// 插入住院项目明细表
        /// </summary>
        /// <returns></returns>
        public int InsertInPatientFeeItemDetail(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f,DateTime operTime)
        {
            string sqlInsert = @"insert into mashxm(HOS_CODE,ZYNO,ZYSNO,XMCODE,
 XMDES,XMUNT,XMQNT,XMPRC,XMAMT,TRNDATE,TRNFLAG, MEMOA,U_VERSION) values('{0}',
'{1}',{2},'{3}','{4}','{5}',{6},{7},{8},'{9}','{10}','{11}','{12}')";
            this.ClearErrMsg();
            try
            {
                string strUnit = f.Item.PriceUnit;
                if (!string.IsNullOrEmpty(strUnit) && strUnit.Length > 4)
                {
                    strUnit = strUnit.Substring(0, 4);
                }
                decimal totCost = f.Item.SpecialPrice * f.Item.Qty;
                sqlInsert = string.Format(sqlInsert, Function.Function.HospitalCode,patient.PID.PatientNO,
                    patient.InTimes, f.Item.UserCode, f.Item.Name, strUnit, f.Item.Qty, f.Item.SpecialPrice, totCost,
                    operTime,"F","","");
                return this.ExecNoQuery(sqlInsert);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
            }
            return 1; 
        }

        public string GetInPatientBalanceHead1(FS.HISFC.Models.RADT.PatientInfo siPatient)
        {
            string sqlBalance = @"select medno,hos_code,zyno,zysno,cbcwf,zfyp,ylyp,zcyp,
                                    zcyf,gxylf,qtfy,qfbzf,grfd2,grfd3,grfd3s,grfdcg,
                                    ylxma,ylxmb,ylxmc,grblzf,ylfyze,jbyltczf,bcyltczf,
                                    gwytczf,lxjjzf,gsjjzf,syjjzf,ylxmd,ylxme,
                                    ylxmf,ylxmg,ylxmh,trnflg,trndate,u_version from mashzfjs
                                    where hos_code= '{0}' and zyno= '{1}' and zysno= {2}";

//            sqlBalance = @"select medno,hos_code,zyno,zysno,cbcwf,zfyp,ylyp,zcyp,
//                                    zcyf,gxylf,qtfy,qfbzf,grfd2,grfd3,grfd3s,grfdcg,
//                                    ylxma,ylxmb,ylxmc,
//                                    ylxmd,ylxme,
//                                    trnflg,trndate,u_version from mashzfjs
//                                    where hos_code= '{0}' and zyno= '{1}' and zysno= {2}";

            this.ClearErrMsg();
            string balanceHead = string.Empty;
            try
            {
                sqlBalance = string.Format(sqlBalance, Function.Function.HospitalCode, siPatient.PID.PatientNO, siPatient.InTimes);
                if (this.ExecQuery(sqlBalance) == -1)
                {
                    return null;
                }
                while (siReader.Read())
                {
                    balanceHead = this.siReader.IsDBNull(0) ? "" : this.siReader[0].ToString() + "|"
                        + (this.siReader.IsDBNull(1) ? "" : this.siReader[1].ToString()) + "|"
                        + (this.siReader.IsDBNull(2) ? "" : this.siReader[2].ToString()) + "|"
                        + (this.siReader.IsDBNull(3) ? "" : this.siReader[3].ToString()) + "|"
                        + (this.siReader.IsDBNull(4) ? "" : this.siReader[4].ToString()) + "|"
                        + (this.siReader.IsDBNull(5) ? "" : this.siReader[5].ToString()) + "|"
                        + (this.siReader.IsDBNull(6) ? "" : this.siReader[6].ToString()) + "|"
                        + (this.siReader.IsDBNull(7) ? "" : this.siReader[7].ToString()) + "|"
                        + (this.siReader.IsDBNull(8) ? "" : this.siReader[8].ToString()) + "|"
                        + (this.siReader.IsDBNull(9) ? "" : this.siReader[9].ToString()) + "|"
                        + (this.siReader.IsDBNull(10) ? "" : this.siReader[10].ToString()) + "|"
                        + (this.siReader.IsDBNull(11) ? "" : this.siReader[11].ToString()) + "|"
                        + (this.siReader.IsDBNull(12) ? " " : this.siReader[12].ToString()) + "|"
                        + (this.siReader.IsDBNull(13) ? " " : this.siReader[13].ToString()) + "|"
                        + (this.siReader.IsDBNull(14) ? " " : this.siReader[14].ToString()) + "|"
                        + (this.siReader.IsDBNull(15) ? " " : this.siReader[15].ToString()) + "|"
                        + (this.siReader.IsDBNull(16) ? " " : this.siReader[16].ToString()) + "|"
                        + (this.siReader.IsDBNull(17) ? " " : this.siReader[17].ToString()) + "|"
                        + (this.siReader.IsDBNull(18) ? " " : this.siReader[18].ToString()) + "|"
                        + (this.siReader.IsDBNull(19) ? " " : this.siReader[19].ToString()) + "|"
                        + (this.siReader.IsDBNull(20) ? " " : this.siReader[20].ToString()) + "|"
                        + (this.siReader.IsDBNull(21) ? " " : this.siReader[21].ToString()) + "|"
                        + (this.siReader.IsDBNull(22) ? " " : this.siReader[22].ToString()) + "|"
                        + (this.siReader.IsDBNull(23) ? " " : this.siReader[23].ToString()) + "|"
                        + (this.siReader.IsDBNull(24) ? " " : this.siReader[24].ToString()) + "|"
                        + (this.siReader.IsDBNull(25) ? " " : this.siReader[25].ToString()) + "|"
                        + (this.siReader.IsDBNull(26) ? " " : this.siReader[26].ToString()) + "|"
                        + (this.siReader.IsDBNull(27) ? " " : this.siReader[27].ToString()) + "|"
                        + (this.siReader.IsDBNull(28) ? " " : this.siReader[28].ToString()) + "|"
                        + (this.siReader.IsDBNull(29) ? " " : this.siReader[29].ToString()) + "|"
                        + (this.siReader.IsDBNull(30) ? " " : this.siReader[30].ToString()) + "|"
                        + (this.siReader.IsDBNull(31) ? " " : this.siReader[31].ToString()) + "|"
                        + (this.siReader.IsDBNull(32) ? " " : this.siReader[32].ToString()) + "|"
                        + (this.siReader.IsDBNull(33) ? " " : this.siReader[33].ToString()) + "|"
                        + (this.siReader.IsDBNull(34) ? " " : this.siReader[33].ToString())
                        ;
                }
            }
            catch (Exception ex)
            {
                balanceHead = Function.Function.errorText;
                this.ErrCode = "-1";
                this.Err = ex.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
                return null;
            }
            finally
            {
                if (this.siReader != null && !this.siReader.IsClosed)
                {
                    this.siReader.Close();
                }
            }

            return balanceHead;
        }

        /// <summary>
        /// 获取医保前台结算结果信息
        /// </summary>
        public int GetInPatientBalanceHead(FS.HISFC.Models.RADT.PatientInfo siPatient)
        {
            string sqlBalance = @"select medno,hos_code,zyno,zysno,cbcwf,zfyp,ylyp,zcyp,
                                    zcyf,gxylf,qtfy,qfbzf,grfd2,grfd3,grfd3s,grfdcg,
                                    ylmna,ylxmb,ylxmc,grblzf,ylfyze,jbyltczf,bcyltczf,
                                    gwytczf,lxjjzf,gsjjzf,syjjzf,ylxmd,ylxme,
                                    ylxmf,ylxmg,ylxmh,trnflg,trndate,u_version from mashzfjs
                                    where hos_code='{0}' and zyno='{1}' and zysno='{2}'";
            this.ClearErrMsg();
            try
            {
                sqlBalance = string.Format(sqlBalance, Function.Function.HospitalCode, siPatient.PID.PatientNO,siPatient.InTimes);
                if (this.ExecQuery(sqlBalance) == -1)
                {
                    return -1;
                }
                while (this.siReader.Read())
                {
                    //（医疗费用总额-基本医疗统筹支付-补充医疗统筹支付-公务员统筹支付-
                    //离休基金支付-工伤基金支付-生育基金支付）
                    int index = 0;
                    decimal basePubCost = 0;//基本医疗统筹支付
                    decimal supplyPubCost = 0;//补充医疗统筹支付
                    decimal officePubCost = 0;//公务员统筹支付
                    decimal leftPubCost = 0;//离休基金支付
                    decimal hurtPubcost = 0m;//工伤基金支付
                    decimal babyPubCost = 0m;//生育基金支付


                    //个人自付部分金额
                    decimal overBedFee=0;
                    decimal ownDrugFee=0;
                    decimal classBDrugFee=0;
                    decimal chinesePatentDrugFee=0;
                    decimal chineseHerbalDrugFee=0;
                    decimal highNewFee=0;
                    decimal otherFee=0;
                    decimal startPayFee=0;
                    decimal overLimitTotFee=0;
                    decimal leaveNotInRescueFee=0;
                    decimal leaveNotInNoRescueFee=0;
                    decimal leaveImportFee=0;
                    decimal ratioByOwnPay=0;


                    #region 统筹支付部分

                        index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("基本医疗统筹支付"));
                    basePubCost = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);

                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("补充医疗统筹支付"));
                    supplyPubCost = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);

                    index = FS.FrameWork.Function.NConvert.ToInt32(
                            Function.Function.BalanceHelper.GetID("公务员统筹支付"));
                    officePubCost = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);

                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("离休基金支付"));
                    leftPubCost = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);

                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("工伤基金支付"));
                    hurtPubcost= siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);

                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("生育基金支付"));
                    babyPubCost = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);

                    #endregion

                    #region 个人自付部分金额

                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("超标床位费"));
                    overBedFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);

                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("自费药品费"));
                    ownDrugFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("乙类药品费"));
                    classBDrugFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("中成药品费"));
                    chinesePatentDrugFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("中草药费"));
                    chineseHerbalDrugFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("高新仪器、治疗费"));
                    highNewFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("其他费用"));
                    otherFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("起付标准费"));
                    startPayFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("超最高限额费用"));
                    overLimitTotFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("离休不纳入抢救费用"));
                    leaveNotInRescueFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("离休不纳入非抢救费用"));
                    leaveNotInNoRescueFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("离休进口材料"));
                    leaveImportFee = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                      index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("按比例个人自付费用"));
                    ratioByOwnPay = siReader.IsDBNull(index) ? 
                        0 : FS.FrameWork.Function.NConvert.ToDecimal(siReader[index]);
                    #endregion

                    #region 总额

                    index = FS.FrameWork.Function.NConvert.ToInt32(
                        Function.Function.BalanceHelper.GetID("医疗费用总额"));
                    siPatient.SIMainInfo.TotCost = this.siReader.IsDBNull(index) ?
                    0 : FS.FrameWork.Function.NConvert.ToDecimal(this.siReader[index]);

                    siPatient.SIMainInfo.PubCost = basePubCost + supplyPubCost + officePubCost + leftPubCost + hurtPubcost + babyPubCost;

                    siPatient.SIMainInfo.PayCost = overBedFee + ownDrugFee+classBDrugFee+chinesePatentDrugFee
                    +chineseHerbalDrugFee+highNewFee+otherFee+startPayFee+overLimitTotFee+leaveNotInRescueFee
                    +leaveNotInNoRescueFee+leaveImportFee+ratioByOwnPay;

                    #endregion
                    
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
            }
            finally
            {
                if (this.siReader != null && !this.siReader.IsClosed)
                {
                    this.siReader.Close();
                }
            }

            return 1;
        }

        /// <summary>
        /// 获取医保前台结算结果信息
        /// </summary>
        public string GetInPatientBalanceDetail()
        {
            string sqlBalance = @"select medno,xmsno,xmqnt,ylfamt,nrzamt,xmcdea,xmcdeb,xmjsfl,
xmclsc,hos_Code,xmcode,u_version from mashjsmx ";
            string balanceHead = string.Empty;
            this.ClearErrMsg();
            try
            {
                //sqlBalance = string.Format(sqlBalance, Function.Function.HospitalCode, register.ID);
                if (this.ExecQuery(sqlBalance) == -1)
                {
                    return null;
                }
                while (siReader.Read())
                {
                    balanceHead = this.siReader.IsDBNull(0) ? "" : this.siReader[0].ToString() + "|"
                        + (this.siReader.IsDBNull(1) ? "" : this.siReader[1].ToString()) + "|"
                        + (this.siReader.IsDBNull(2) ? "" : this.siReader[2].ToString()) + "|"
                        + (this.siReader.IsDBNull(3) ? "" : this.siReader[3].ToString()) + "|"
                        + (this.siReader.IsDBNull(4) ? "" : this.siReader[4].ToString()) + "|"
                        + (this.siReader.IsDBNull(5) ? "" : this.siReader[5].ToString()) + "|"
                        + (this.siReader.IsDBNull(6) ? "" : this.siReader[6].ToString()) + "|"
                        + (this.siReader.IsDBNull(7) ? "" : this.siReader[7].ToString()) + "|"
                        + (this.siReader.IsDBNull(8) ? "" : this.siReader[8].ToString()) + "|"
                        + (this.siReader.IsDBNull(9) ? "" : this.siReader[9].ToString()) + "|"
                        + (this.siReader.IsDBNull(10) ? "" : this.siReader[10].ToString()) + "|"
                        + (this.siReader.IsDBNull(11) ? "" : this.siReader[11].ToString()) + "|"
                        + (this.siReader.IsDBNull(12) ? " " : this.siReader[12].ToString());
                }
            }
            catch (Exception ex)
            {
                balanceHead = Function.Function.errorText;
                this.ErrCode = "-1";
                this.Err = ex.Message;
                Function.Function.WriteErr(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
                return null;
            }
            finally
            {
                if (this.siReader != null && !this.siReader.IsClosed)
                {
                    this.siReader.Close();
                }
            }

            return balanceHead;
        }

        #endregion

        public void ClearErrMsg()
        {
            this.Err = string.Empty;//清空错误码信息
            this.ErrCode = string.Empty;
        }
    }
}
