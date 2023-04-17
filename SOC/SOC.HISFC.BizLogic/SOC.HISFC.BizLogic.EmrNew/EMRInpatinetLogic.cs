using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizLogic.EmrNew
{
    /// <summary>
    /// 住院接口业务逻辑处理
    /// </summary>
    public class EMRInpatinetLogic : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 根据HIS患者流水号取EMR患者流水号
        /// </summary>
        /// <param name="inpatientNo">His患者流水号</param>
        /// <param name="inpatientId">EMR患者流水号</param>
        /// <returns>成功返回更新行数，失败返回-1</returns>
        public int GetPatientId(string inpatientNo, ref long inpatientId)
        {
            string sql = "";
            if (this.Sql.GetSql("EMR.IPM.GetPatientId", ref sql) == -1)
            {
                this.Err = "EMR.IPM.GetPatientId不存在！";
                this.WriteErr();
                return -1;
            }
            sql = string.Format(sql, inpatientNo);

            string result = this.ExecSqlReturnOne(sql);
            try
            {
                inpatientId = Convert.ToInt64(result);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>yushuai
        /// 出院召回录入医师，如果住院医师变更则插入新纪录，否则更新状态和结束时间
        /// </summary>
        /// <param name="inpatientNo">住院患者流水号</param>
        /// <param name="directDoctCode">主任医师code </param>
        /// <param name="chargeDoctCode">主治医师code </param>
        /// <param name="residencyCode">住院医师code string</param>
        ///  <param name="deptCode">科室编码</param>
        /// <returns>成功返回更新或插入行数，失败返回-1</returns>
        public int ChangePTDocReCall(long inpatientNo, string directDoctCode, string chargeDoctCode, string residencyCode, string deptCode)
        {
            string directDoctId = this.DoctCodeToId(directDoctCode);
            string chargeDoctId = this.DoctCodeToId(chargeDoctCode);
            string residencyId = this.DoctCodeToId(residencyCode);
            string sql = "";
            if (this.Sql.GetSql("EMR.IPR.SelectPTDoc", ref sql) == -1)
            {
                this.Err = "EMR.IPR.SelectPTDoc不存在！";
                this.WriteErr();
                return -1;
            }
            sql = string.Format(sql, inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId);

            string result = this.ExecSqlReturnOne(sql);
            try
            {
                int qty = Convert.ToInt32(result);
                if (qty > 0)
                {
                    return this.UpdateSingleTable("EMR.IPR.ChangePTDocBack", inpatientNo.ToString());
                }
                else
                {
                    if (this.UpdateSingleTable("EMR.IPR.UpdatePTDocBack", inpatientNo.ToString()) == -1)
                    {
                        this.Err = "EMR.IPR.UpdatePTDocBack执行失败！";
                        this.WriteErr();
                        return -1;
                    }

                    return this.UpdateSingleTable("EMR.IPR.InsertPTDoc", inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId, deptCode);
                }
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 出院登记，结束医师信息
        /// </summary>
        /// <param name="inpatientNo">住院患者流水号</param>
        /// <returns>成功返回更新行数，失败返回-1</returns>
        public int OutPTDoc(long inpatientNo)
        {
            return this.UpdateSingleTable("EMR.IPR.OutPTDoc", inpatientNo.ToString());
        }


        /// <summary>
        /// 医师变更，先更新再插入
        /// </summary>
        /// <param name="inpatientNo">住院患者流水号</param>
        /// <param name="directDoctCode">主任医师code </param>
        /// <param name="chargeDoctCode">主治医师code </param>
        /// <param name="residencyCode">住院医师code string</param>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功返回更新行数，失败返回-1</returns>
        public int ChangePTDoc(long inpatientNo, string directDoctCode, string chargeDoctCode, string residencyCode, string deptCode)
        {
            string directDoctId = this.DoctCodeToId(directDoctCode);
            string chargeDoctId = this.DoctCodeToId(chargeDoctCode);
            string residencyId = this.DoctCodeToId(residencyCode);
            //modify by zhutonghao @ 20110906
            //int resultChange = this.UpdateSingleTable("EMR.IPR.ChangePTDoc", inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId);
            //int resultInsert = this.UpdateSingleTable("EMR.IPR.InsertPTDoc", inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId, deptCode);
            // if (resultChange == -1)
            //{
            //    return -1;
            //}
            //else if (resultInsert == -1)
            //{
            //    return -1;
            //}
            //else
            //{
            //    return resultChange + resultInsert;
            //}
            return this.UpdateSingleTable("EMR.IPR.ChangePTDoc", inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId);
        }


        /// <summary>
        /// 添加住院医师、主治医师、主任医师
        /// </summary>
        /// <param name="inpatientNo">住院患者流水号</param>
        /// <param name="directDoctCode">主任医师code </param>
        /// <param name="chargeDoctCode">主治医师code </param>
        /// <param name="residencyCode">住院医师code </param>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功返回更新行数，失败返回-1</returns>
        public int InsertPTDoc(long inpatientNo, string directDoctCode, string chargeDoctCode, string residencyCode, string deptCode)
        {
            string directDoctId = this.DoctCodeToId(directDoctCode);
            string chargeDoctId = this.DoctCodeToId(chargeDoctCode);
            string residencyId = this.DoctCodeToId(residencyCode);
            return this.UpdateSingleTable("EMR.IPR.InsertPTDoc", inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId, deptCode);
        }

        #region 作废

        ///// <summary>
        ///// 更新住院医嘱的收费状态
        ///// </summary>
        ///// <param name="emrIpmMoOrder">医嘱流水号</param>
        ///// <param name="payeeCode">收费人Code</param>
        ///// <param name="chargeTime">收费时间</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int ChargeOrder( long emrIpmMoOrder, string payeeCode, DateTime chargeTime)
        //{
        //    return this.UpdateSingleTable("EMR.IPM.ChargeOrder", emrIpmMoOrder.ToString(), payeeCode, chargeTime.ToString("yyyy-MM-dd HH:mm:ss"));
        //}

        ///// <summary>
        ///// 更新住院医嘱的审核状态
        ///// </summary>
        ///// <param name="emrIpmMoOrder">医嘱流水号</param>
        ///// <param name="checkOperCode">审核人Code</param>
        ///// <param name="checkTime">审核时间</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int ConfirmOrder(long emrIpmMoOrder, string checkOperCode, DateTime checkTime)
        //{
        //    return this.UpdateSingleTable("EMR.IPM.ConfirmOrder", checkOperCode,checkTime.ToString("yyyy-MM-dd HH:mm:ss"),emrIpmMoOrder.ToString());
        //}

        ///// <summary>shaungyan
        ///// 更新停止的住院医嘱的审核状态
        ///// </summary>
        ///// <param name="emrIpmMoOrder">医嘱流水号</param>
        ///// <param name="checkOperCode">审核人Code</param>
        ///// <param name="checkTime">审核时间</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int DcConfirmOrder(long emrIpmMoOrder, string checkOperCode, DateTime checkTime)
        //{
        //    string checkOperId = this.DoctCodeToId(checkOperCode);
        //    return this.UpdateSingleTable("EMR.IPM.DcConfirmOrder", emrIpmMoOrder.ToString(), checkOperId.ToString(), checkTime.ToString("yyyy-MM-dd HH:mm:ss"));
        //}

        ///// <summary>
        ///// 住院 -- 更新住院预停止医嘱的停止状态
        ///// </summary>
        ///// <param name="emrIpmMoOrder">医嘱流水号</param>
        ///// <param name="combNo">组合号</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int DcOrder( long emrIpmMoOrder, string combNo)
        //{
        //    //医嘱术语类型校验

        //    string sql = "";
        //    if (this.Sql.GetSql("EMR.IPM.OrderCheck", ref sql) == -1)
        //    {
        //        this.Err = "EMR.IPM.OrderCheck不存在！";
        //        this.WriteErr();
        //        return -1;
        //    }
        //    sql = string.Format(sql, emrIpmMoOrder.ToString());

        //    string strOrder = this.ExecSqlReturnOne(sql);



        //    if (!string.IsNullOrEmpty(strOrder) && strOrder.ToLower() == "uc")
        //    {
        //        //住院检查申请单更新
        //        int result = this.UpdateSingleTable("EMR.IPM.DcExam",combNo);
        //    }
        //    return this.UpdateSingleTable("EMR.IPM.DcOrder", combNo);
            
        //}

        ///// <summary>
        ///// 更新住院医嘱的发药状态
        ///// </summary>
        ///// <param name="emrIpmMoOrder">医嘱流水号</param>
        ///// <param name="drOperCode">发药人Code</param>
        ///// <param name="drTime">发药时间</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int DrugedOrder( long emrIpmMoOrder, string drOperCode, DateTime drTime)
        //{
        //    return this.UpdateSingleTable("EMR.IPM.DrugedOrder", emrIpmMoOrder.ToString(), drOperCode,drTime.ToString("yyyy-MM-dd HH:mm:ss"));
        //}

        ///// <summary>
        ///// 更新住院医嘱的执行状态
        ///// </summary>
        ///// <param name="emrIpmMoOrder">医嘱流水号</param>
        ///// <param name="execOperCode">执行人Code</param>
        ///// <param name="execTime">执行时间</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int ExecOrder(long emrIpmMoOrder, string execOperCode, DateTime execTime)
        //{
        //    string existsLabTestOrder = this.ExecSqlReturnOne("EMR.IPM.ExistsLabTestOrder",emrIpmMoOrder.ToString());
        //    if (!string.IsNullOrEmpty(existsLabTestOrder) && Convert.ToInt32(existsLabTestOrder) > 0)
        //    {//判断医嘱是否是检验医嘱，如果是插入一条新的数据到Doc_Lab_Data
        //        this.UpdateSingleTable("EMR.IPM.InsertDocLabData", emrIpmMoOrder.ToString());
        //    }

        //    return this.UpdateSingleTable("EMR.IPM.ExecOrder", emrIpmMoOrder.ToString(), execOperCode, execTime.ToString("yyyy-MM-dd HH:mm:ss"));
        //}

        ///// <summary>yushuai
        ///// 更新手术患者状态
        ///// </summary>
        ///// <param name="inpatientID">EMR住院流水号 </param>
        ///// <param name="operationState">是否手术患者 默认0非手术患者，1手术患者</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int UpdateImpOperationState(long inpatientID, int operationState)
        //{
        //    return this.UpdateSingleTable("EMR.IPR.UpdateInpatientOpera", inpatientID.ToString(), operationState.ToString());
        //}

        ///// <summary>yushuai
        ///// 更新抢救患者状态
        ///// </summary>
        ///// <param name="inpatientID">EMR住院流水号</param>
        ///// <param name="rescueState">是否抢救患者 默认 0非抢救患者，1抢救患者</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int UpdateImpRescueState(long inpatientID, int rescueState)
        //{
        //    return this.UpdateSingleTable("EMR.IPR.UpdateInpatientRescue", inpatientID.ToString(), rescueState.ToString());
        //}

        ///// <summary>
        /////  住院更新皮试结果
        ///// </summary>
        ///// <param name="emrIpmMoOrder">医嘱流水号</param>
        ///// <param name="needHypo">是否试敏</param>
        ///// <param name="hypotest">结果是否阴性</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int UpdateHypoTestResult(long emrIpmMoOrder, long needHypo, long hypotest)
        //{
        //    return this.UpdateSingleTable("EMR.IPM.UpdateHypoTestResult", emrIpmMoOrder.ToString(), needHypo.ToString(), hypotest.ToString());
        //}

        ///// <summary>
        ///// 住院皮试双签名
        ///// </summary>
        ///// <param name="orderId">EMR医嘱ID</param>
        ///// <param name="hypoTestFlag">阴性 阳性标志</param>
        ///// <param name="operNurseCode">第一次签名护士</param>
        ///// <param name="operTime">第一次签名时间</param>
        ///// <param name="approveOperNurseCode">第二次签名护士</param>
        ///// <param name="approveOperTime">第二次签名时间</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int HypoTestIpmSignFirst(long orderId, string hypoTestFlag, string operNurseCode, string operTime, string approveOperNurseCode, string approveOperTime)
        //{
        //    string operNrsid = DoctCodeToId(operNurseCode);
        //    if (string.IsNullOrEmpty(operNrsid))
        //    {
        //        this.Err = string.Format("取得emr护士Id失败！His 人员Code：{0}",operNurseCode);
        //        this.WriteErr();
        //        return -1;
        //    }

        //    string approveOperNurseId = DoctCodeToId(approveOperNurseCode);
        //    if (string.IsNullOrEmpty(approveOperNurseId))
        //    {
        //        this.Err = string.Format("取得emr护士Id失败！His 人员Code：{0}", approveOperNurseId);
        //        this.WriteErr();
        //        return -1;
        //    }

        //    return this.UpdateSingleTable("EMR.IPM.HypoTestOpmSignFirst", orderId.ToString(), hypoTestFlag, operNrsid, operTime, approveOperNurseId, approveOperTime);
        //}


        ///// <summary>
        ///// 住院登记
        ///// </summary>
        ///// <param name="emrInpatientID">EMR 住院流水号</param>
        ///// <param name="inpatientNo">HIS住院流水号</param>
        ///// <param name="patientNo">住院号</param>
        ///// <param name="cardNo">就诊卡号</param>
        ///// <param name="IdenNo">身份证号</param>
        ///// <param name="mcardNo">医保卡号</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int Register(long emrInpatientID, string inpatientNo, string patientNo, string cardNo, string IdenNo, string mcardNo)
        //{
        //    string sql = "";
        //    if (this.Sql.GetSql("EMR.COM.QueryPatientInfo", ref sql) == -1)
        //    {
        //        this.Err = "EMR.COM.QueryPatientInfo不存在！";
        //        this.WriteErr();
        //        return -1;
        //    }
        //    sql = string.Format(sql, cardNo);
        //    if (this.ExecQuery(sql) == -1)
        //    {
        //        this.Err = "执行EMR.COM.QueryPatientInfo失败！";
        //        this.WriteErr();
        //        return -1;
        //    }
        //    try
        //    {
        //        long emrPatId = 0;//    emr_patid ,--患者流水号 
        //        string name = string.Empty;//  name,  --姓名 string
        //        string sexCode = string.Empty;//sex_code, --性别编码 string 
        //        DateTime birthday = new DateTime();//  birthday --生日 date
        //        if (this.Reader.Read())
        //        {
        //            try
        //            {
        //                emrPatId = Convert.ToInt64(this.Reader[0]);//患者流水号
        //            }
        //            catch { }


        //            try
        //            {
        //                name = Convert.ToString(this.Reader[1]);//姓名
        //            }
        //            catch { }

        //            try
        //            {
        //                sexCode = Convert.ToString(this.Reader[2]);//性别编码
        //            }
        //            catch { }

        //            try
        //            {
        //                birthday = Convert.ToDateTime(this.Reader[3]);//生日
        //            }
        //            catch { }

        //        }
        //        else
        //        {
        //            this.Err = string.Format("CardNo:{0}的就诊信息取得失败！", cardNo);
        //            this.WriteErr();
        //            return -1;
        //        }

        //        int result = this.UpdateSingleTable("EMR.COM.InsertPatientInfo", emrPatId.ToString(), name, sexCode, birthday.ToString("yyyy-MM-dd"));

        //        if (result == -1)
        //        {
        //            return -1;
        //        }
        //        else
        //        {
        //            return this.UpdateSingleTable("EMR.IPM.InsertPatient", emrInpatientID.ToString(), emrPatId.ToString(), inpatientNo);
        //        }

        //    }
        //    catch
        //    {
        //        this.Err = "数据取得失败！";
        //        this.WriteErr();
        //        return -1;
        //    }
        //}

        ///// <summary>
        ///// 根据HIS患者流水号取EMR患者流水号
        ///// </summary>
        ///// <param name="inpatientNo">His患者流水号</param>
        ///// <param name="inpatientId">EMR患者流水号</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public  int GetPatientId(string inpatientNo, ref long inpatientId)
        //{
        //    string sql = "";
        //    if (this.Sql.GetSql("EMR.IPM.GetPatientId", ref sql) == -1)
        //    {
        //        this.Err = "EMR.IPM.GetPatientId不存在！";
        //        this.WriteErr();
        //        return -1;
        //    }
        //    sql = string.Format(sql, inpatientNo);

        //    string result = this.ExecSqlReturnOne(sql);
        //    try
        //    {
        //        inpatientId = Convert.ToInt64(result);
        //        return 1;
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //}

        ///// <summary>
        ///// 添加住院医师、主治医师、主任医师
        ///// </summary>
        ///// <param name="inpatientNo">住院患者流水号</param>
        ///// <param name="directDoctCode">主任医师code </param>
        ///// <param name="chargeDoctCode">主治医师code </param>
        ///// <param name="residencyCode">住院医师code </param>
        ///// <param name="deptCode">科室编码</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int InsertPTDoc(long inpatientNo, string directDoctCode, string chargeDoctCode, string residencyCode, string deptCode)
        //{
        //    string directDoctId = this.DoctCodeToId(directDoctCode);
        //    string chargeDoctId = this.DoctCodeToId(chargeDoctCode);
        //    string residencyId = this.DoctCodeToId(residencyCode);
        //    return this.UpdateSingleTable("EMR.IPR.InsertPTDoc", inpatientNo.ToString(), directDoctId, chargeDoctId,residencyId,deptCode);
        //}
        ///// <summary>
        ///// 医师变更，先更新再插入
        ///// </summary>
        ///// <param name="inpatientNo">住院患者流水号</param>
        ///// <param name="directDoctCode">主任医师code </param>
        ///// <param name="chargeDoctCode">主治医师code </param>
        ///// <param name="residencyCode">住院医师code string</param>
        ///// <param name="deptCode">科室编码</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public int ChangePTDoc(long inpatientNo, string directDoctCode, string chargeDoctCode, string residencyCode, string deptCode)
        //{
        //    string directDoctId = this.DoctCodeToId(directDoctCode);     
        //    string chargeDoctId = this.DoctCodeToId(chargeDoctCode);
        //    string residencyId = this.DoctCodeToId(residencyCode);
        //    //modify by zhutonghao @ 20110906
        //    //int resultChange = this.UpdateSingleTable("EMR.IPR.ChangePTDoc", inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId);
        //    //int resultInsert = this.UpdateSingleTable("EMR.IPR.InsertPTDoc", inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId, deptCode);
        //    // if (resultChange == -1)
        //    //{
        //    //    return -1;
        //    //}
        //    //else if (resultInsert == -1)
        //    //{
        //    //    return -1;
        //    //}
        //    //else
        //    //{
        //    //    return resultChange + resultInsert;
        //    //}
        //    return this.UpdateSingleTable("EMR.IPR.ChangePTDoc", inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId);
            
            
        //}
        ///// <summary>
        ///// 出院登记，结束医师信息
        ///// </summary>
        ///// <param name="inpatientNo">住院患者流水号</param>
        ///// <returns>成功返回更新行数，失败返回-1</returns>
        //public  int OutPTDoc(long inpatientNo)
        //{
        //    return this.UpdateSingleTable("EMR.IPR.OutPTDoc", inpatientNo.ToString());
        //}

        ///// <summary>yushuai
        ///// 出院召回录入医师，如果住院医师变更则插入新纪录，否则更新状态和结束时间
        ///// </summary>
        ///// <param name="inpatientNo">住院患者流水号</param>
        ///// <param name="directDoctCode">主任医师code </param>
        ///// <param name="chargeDoctCode">主治医师code </param>
        ///// <param name="residencyCode">住院医师code string</param>
        /////  <param name="deptCode">科室编码</param>
        ///// <returns>成功返回更新或插入行数，失败返回-1</returns>
        //public int ChangePTDocReCall(long inpatientNo, string directDoctCode, string chargeDoctCode, string residencyCode, string deptCode)
        //{
        //    string directDoctId = this.DoctCodeToId(directDoctCode);
        //    string chargeDoctId = this.DoctCodeToId(chargeDoctCode);
        //    string residencyId = this.DoctCodeToId(residencyCode);
        //    string sql = "";
        //    if (this.Sql.GetSql("EMR.IPR.SelectPTDoc", ref sql) == -1)
        //    {
        //        this.Err = "EMR.IPR.SelectPTDoc不存在！";
        //        this.WriteErr();
        //        return -1;
        //    }
        //    sql = string.Format(sql, inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId);

        //    string result = this.ExecSqlReturnOne(sql);
        //    try
        //    {
        //        int qty = Convert.ToInt32(result);
        //        if (qty > 0)
        //        {
        //            return this.UpdateSingleTable("EMR.IPR.ChangePTDocBack", inpatientNo.ToString());
        //        }
        //        else
        //        {
        //            if (this.UpdateSingleTable("EMR.IPR.UpdatePTDocBack", inpatientNo.ToString()) == -1)
        //            {
        //                this.Err = "EMR.IPR.UpdatePTDocBack执行失败！";
        //                this.WriteErr();
        //                return -1;
        //            }

        //            return this.UpdateSingleTable("EMR.IPR.InsertPTDoc", inpatientNo.ToString(), directDoctId, chargeDoctId, residencyId, deptCode);
        //        }
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //}

        ///// <summary>
        ///// 更新住院申请单打印标志
        ///// </summary>
        ///// <param name="emrOrderCombID">EMROrderCombID</param>
        ///// <returns>功返回更新或插入行数，失败返回-1</returns>
        //public int UpdateIpmApplyPrintState(long emrOrderCombID)
        //{
        //    return this.UpdateSingleTable("EMR.IPR.UpdateIpmApplyPrintState", emrOrderCombID.ToString());
           
        //}

        ///// <summary>
        ///// 更新路径执行状态
        ///// </summary>
        ///// <param name="inpatientId">Emr住院流水号</param>
        ///// <returns>功返回更新或插入行数，失败返回-1</returns>
        //public int UpdatePathState(long inpatientId)
        //{
        //    //路径状态-2 强制退出
        //    //         1 正常终了
        //    string StateCodeQuit = "-2";
        //    string StateNameQuit = "强制退出";

        //    string StateCodeFinish = "1";
        //    string StateNameFinish = "正常终了";


        //    string existsNotExecElem = this.ExecSqlReturnOne("EMR.IPM.ExistsNotExecElem", inpatientId.ToString());

        //    if (!string.IsNullOrEmpty(existsNotExecElem) && Convert.ToInt32(existsNotExecElem) > 0)
        //    {//不为空 并且大于零 表示 有未执行完的必须执行元素
        //        //路径状态为强制退出
        //        int state = this.UpdateSingleTable("EMR.IPM.UpdatePathState", inpatientId.ToString(), StateCodeQuit, StateNameQuit);
        //        if (state <= 0)
        //        {
        //            this.Err = "为找到路径注册信息！InpatientId："+inpatientId.ToString();
        //            this.WriteErr();
        //            return -1;
        //        }
        //        return state;
        //    }

        //    string isInPath = this.ExecSqlReturnOne("EMR.IPM.IsInPath", inpatientId.ToString());
        //    if (!string.IsNullOrEmpty(isInPath) && Convert.ToInt32(isInPath) > 0)
        //    {// 返回值不为空 并且 大于零 表示 当前日期在最后一个阶段之前
        //        //路径状态为强制退出
        //        int state = this.UpdateSingleTable("EMR.IPM.UpdatePathState", inpatientId.ToString(), StateCodeQuit, StateNameQuit);
        //        if (state <= 0)
        //        {
        //            this.Err = "为找到路径注册信息！InpatientId：" + inpatientId.ToString();
        //            this.WriteErr();
        //            return -1;
        //        }
        //        return state;
        //    }
        //    //其他情况路径正常终了
        //    int result = this.UpdateSingleTable("EMR.IPM.UpdatePathState", inpatientId.ToString(), StateCodeFinish, StateNameFinish);
        //    if (result <= 0)
        //    {
        //        this.Err = "为找到路径注册信息！InpatientId：" + inpatientId.ToString();
        //        this.WriteErr();
        //        return -1;
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 更新单表操作
        ///// </summary>
        ///// <param name="sqlIndex">SQL语句索引</param>
        ///// <param name="args">参数</param>
        ///// <returns>成功: >= 1 失败 -1 没有更新到数据 0</returns>
        //public int UpdateSingleTable(string sqlIndex, params string[] args)
        //{
        //    string sql = string.Empty;//Update语句

        //    //获得Where语句
        //    if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
        //    {
        //        this.Err = "没有找到索引为:" + sqlIndex + "的SQL语句";

        //        return -1;
        //    }

        //    return this.ExecNoQuery(sql, args);
        //}


        #region 私有方法
        /// <summary>
        /// 根据医生Code（His）取医生ID（emr）
        /// </summary>
        /// <param name="doctCode">医生Code（His）</param>
        /// <returns>医生ID（emr）</returns>
        private string DoctCodeToId(string doctCode)
        {
            string sql = string.Format("select id from vemr_emp where empl_code = '{0}'", doctCode);
            return this.ExecSqlReturnOne(sql);

        }
        #endregion

        #endregion
    }
}
