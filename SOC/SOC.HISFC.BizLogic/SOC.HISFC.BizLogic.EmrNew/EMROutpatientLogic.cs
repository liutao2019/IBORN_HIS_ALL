using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizLogic.EmrNew
{
    /// <summary>
    /// 门诊接口业务逻辑处理
    /// </summary>
    public class EMROutpatientLogic : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 取EMR挂号流水号
        /// </summary>
        /// <param name="clinicNo">His挂号流水号</param>
        /// <param name="emrRegId">EMR挂号流水号</param>
        /// <returns>成功返回影响行数，失败返回-1</returns>
        public int GetEmrRegId(string clinicNo, ref long emrRegId)
        {

            string sql = "";
            if (this.Sql.GetSql("EMR.ORD.GetClinicID", ref sql) == -1)
            {
                this.Err = "EMR.ORD.GetClinicID不存在！";
                this.WriteErr();
                return -1;
            }
            sql = string.Format(sql, clinicNo);
            string result = this.ExecSqlReturnOne(sql);
            try
            {
                emrRegId = Convert.ToInt64(result);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        #region 作废

       // /// <summary>
       // /// 门诊 -- 更新收费状态
       // /// </summary>
       // /// <param name="emrOrdMoOrder">医嘱流水号</param>
       // /// <returns>成功返回更新行数，失败返回-1</returns>
       // public int UpdateOrderFeeFlag(long emrOrdMoOrder)
       // {
       //     return this.UpdateSingleTable("EMR.ORD.UpdateOrderFeeFlag",emrOrdMoOrder.ToString());
       // }

       // /// <summary>
       // /// 门诊 -- 更新发药状态
       // /// </summary>
       // /// <param name="emrOrdMoOrder">医嘱流水号</param>
       // /// <returns>成功返回更新行数，失败返回-1</returns>
       // public int UpdateOrderDrugedFlag(long emrOrdMoOrder)
       // {
       //     return this.UpdateSingleTable("EMR.ORD.UpdateOrderDrugedFlag",emrOrdMoOrder.ToString());
       // }

       // /// <summary>
       // /// 门诊 -- 更新终端确认状态
       // /// </summary>
       // /// <param name="emrOrdMoOrder">医嘱流水号</param>
       // /// <returns>成功返回更新行数，失败返回-1</returns>
       // public int UpdateOrderTerminalExecFlag(long emrOrdMoOrder)
       // {
       //     return this.UpdateSingleTable("EMR.ORD.UpdateOrderTerminalExecFlag",emrOrdMoOrder.ToString());
       // }

       // /// <summary>
       // /// 门诊 -- 更新非药品术语在HIS中对照通过标志
       // /// </summary>
       // /// <param name="emrUnrugID">术语主键</param>
       // /// <returns>成功返回更新行数，失败返回-1</returns>
       // public int UpdateUndrugTermApproveFlag(long emrUnrugID)
       // {
       //     return this.UpdateSingleTable("EMR.COM.UpdateUndrugTermApproveFlag",emrUnrugID.ToString());
       // }

       // /// <summary>
       // /// 门诊 -- 更新非药品术语在HIS中对照未通过标志
       // /// </summary>
       // /// <param name="emrUnrugID">术语主键</param>
       // /// <returns>成功返回更新行数，失败返回-1</returns>
       // public int UpdateUndrugTermUnApproveFlag(long emrUnrugID)
       // {
       //     return this.UpdateSingleTable("EMR.COM.UpdateUndrugTermUnApproveFlag", emrUnrugID.ToString());
       // }

       // /// <summary>
       // /// 门诊--更新皮试结果
       // /// </summary>
       // /// <param name="emrOrdMoOrder">门诊医嘱流水号</param>
       // /// <param name="needHypo">是否试敏</param>
       // /// <param name="hypotest">结果是否阴性</param>
       // /// <returns>成功返回更新行数，失败返回-1</returns>
       // public int UpdateHypoTestResult(long emrOrdMoOrder, long needHypo, long hypotest)
       // {
       //     return this.UpdateSingleTable("EMR.ORD.UpdateHypoTestResult", emrOrdMoOrder.ToString(), needHypo.ToString(), hypotest.ToString());
       // }

       // /// <summary>
       // /// 门诊皮试双签名
       // /// </summary>
       // /// <param name="orderId">EMR医嘱ID</param>
       // /// <param name="hypoTestFlag">阴性 阳性标记</param>
       // /// <param name="operNurseCode">第一次签名护士</param>
       // /// <param name="operTime">第一次签名时间</param>
       // /// <param name="approveOperNurseCode">第二次签名护士</param>
       // /// <param name="approveOperTime">第二次签名时间</param>
       // /// <returns>成功返回更新行数，失败返回-1</returns>
       // public int HypoTestOpmSignFirst(long orderId, string hypoTestFlag, string operNurseCode, string operTime, string approveOperNurseCode, string approveOperTime)
       // {
       //     string operNrsid = DoctCodeToId(operNurseCode);
       //     if (string.IsNullOrEmpty(operNrsid))
       //     {
       //         this.Err = string.Format("取得emr护士Id失败！His 人员Code：{0}", operNurseCode);
       //         this.WriteErr();
       //         return -1;

       //     }

       //     string approveOperNurseId = DoctCodeToId(approveOperNurseCode);
       //     if (string.IsNullOrEmpty(approveOperNurseId))
       //     {
       //         this.Err = string.Format("取得emr护士Id失败！His 人员Code：{0}", approveOperNurseId);
       //         this.WriteErr();
       //         return -1;
       //     }
       //     return this.UpdateSingleTable("EMR.ORD.HypoTestOpmSignFirst", orderId.ToString(), hypoTestFlag, operNurseCode.ToString(), operTime, approveOperNurseCode.ToString(), approveOperTime);
       // }



       // /// <summary>
       // /// 门诊-患者门诊挂号
       // /// </summary>
       // /// <param name="emrRegID">EMR挂号流水号</param>
       // /// <param name="clinicNo">His挂号流水号</param>
       // /// <param name="cardNo">就诊卡号</param>
       // /// <param name="IdenNo">身份证号</param>
       // /// <param name="mcardNo">医保卡号</param>
       // /// <returns>成功返回更新行数，失败返回-1</returns>
       // public int Register(long emrRegID, string clinicNo, string cardNo, string IdenNo, string mcardNo)
       // {
       //     string sql = "";
       //     if (this.Sql.GetSql("EMR.COM.QueryPatientInfo", ref sql) == -1)
       //     {
       //         this.Err = "EMR.COM.QueryPatientInfo不存在！";
       //         this.WriteErr();
       //         return -1;
       //     }
       //     sql = string.Format(sql, cardNo);
       //     if (this.ExecQuery(sql) == -1)
       //     {
       //         this.Err = "执行EMR.COM.QueryPatientInfo失败！";
       //         this.WriteErr();
       //         return -1;
       //     }
       //     try
       //     {
       //         long emrPatId = 0;//    emr_patid ,--患者流水号 
       //         string name = string.Empty;//  name,  --姓名 string
       //         string sexCode = string.Empty;//sex_code, --性别编码 string 
       //         DateTime birthday = new DateTime();//  birthday --生日 date
       //         if (this.Reader.Read())
       //         {
       //             try
       //             {
       //                 emrPatId = Convert.ToInt64(this.Reader[0]);//患者流水号
       //             }
       //             catch { }


       //             try
       //             {
       //                 name = Convert.ToString(this.Reader[1]);//姓名
       //             }
       //             catch { }

       //             try
       //             {
       //                 sexCode = Convert.ToString(this.Reader[2]);//性别编码
       //             }
       //             catch { }

       //             try
       //             {
       //                 birthday = Convert.ToDateTime(this.Reader[3]);//生日
       //             }
       //             catch { }

       //         }
       //         else
       //         {
       //             this.Err = string.Format("CardNo:{0}的就诊信息取得失败！", cardNo);
       //             this.WriteErr();
       //             return -1;
       //         }

       //         int result = this.UpdateSingleTable("EMR.COM.InsertPatientInfo", emrPatId.ToString(), name, sexCode, birthday.ToString("yyyy-MM-dd"));

       //         if (result == -1)
       //         {
       //             return -1;
       //         }
       //         else
       //         {
       //             return this.UpdateSingleTable("EMR.ORD.InsertPatient", emrRegID.ToString(), emrPatId.ToString(), clinicNo);
       //         }
                
       //     }
       //     catch
       //     {
       //         this.Err = "数据取得失败！";
       //         this.WriteErr();
       //         return -1;
       //     }
            

       // }


       // /// <summary>
       // /// 取EMR挂号流水号
       // /// </summary>
       // /// <param name="clinicNo">His挂号流水号</param>
       // /// <param name="emrRegId">EMR挂号流水号</param>
       // /// <returns>成功返回影响行数，失败返回-1</returns>
       // public  int GetEmrRegId(string clinicNo, ref long emrRegId)
       // {

       //     string sql = "";
       //     if (this.Sql.GetSql("EMR.ORD.GetClinicID", ref sql) == -1)
       //     {
       //         this.Err = "EMR.ORD.GetClinicID不存在！";
       //         this.WriteErr();
       //         return -1;
       //     }
       //     sql = string.Format(sql, clinicNo);
       //     string result = this.ExecSqlReturnOne(sql);
       //     try
       //     {
       //         emrRegId = Convert.ToInt64(result);
       //         return 1;
       //     }
       //     catch
       //     {
       //         return -1;
       //     }
       // }


       // /// <summary>yushuai
       // /// 过敏登记
       // /// </summary>
       // /// <param name="cardNo">就诊卡号</param>
       // /// <param name="funCode">药理作用编码</param>
       // /// <param name="drugCode">药品编码</param>
       // /// <param name="errText">错误信息</param>
       // /// <returns>成功返回影响行数，失败返回-1</returns>
       // public int AllergenReg(string cardNo, string funCode, string drugCode, out string errText)
       // {
       //     string patientId = this.CardNoToPatientId(cardNo);
       //     string drugId = this.DrugCodeToDrugId(drugCode);
       //     string drugName = this.DrugCodeToDrugName(drugCode);
       //     string funName = this.FunCodeToName(funCode);
       //     try
       //     {
       //         errText = string.Empty;
       //        return  this.UpdateSingleTable("EMR.IPR.AllergenReg", patientId, funCode, funName, drugId, drugName);
       //     }
       //     catch (Exception e)
       //     {
       //         errText = "过敏登记错误";
       //         return -1;
       //     }


       // }

       // /// <summary>
       // /// 更新门诊申请单打印标志
       // /// </summary>
       // /// <param name="emrOrderCombID">EMROrderCombID</param>
       // /// <returns>功返回更新或插入行数，失败返回-1</returns
       // public int UpdateOpmApplyPrintState(long emrOrderCombID)
       // {
       //     return this.UpdateSingleTable("EMR.OPD.UpdateOpmApplyPrintState", emrOrderCombID.ToString());
       // }

       // /// <summary>
       // /// 将 指定患者门诊流水号、指定医嘱流水号 医嘱， 更新成 执行状态，并同时更新执行人、执行时间、执行备注
       // /// </summary>
       // /// <param name="orderId">Emr医嘱ID</param>
       // /// <param name="execOperCode">执行人Code</param>
       // /// <param name="execOperTime">执行操作时间</param>
       // /// <param name="execMemo">执行备注</param>
       // /// <returns>1成功；其他报异常</returns>      
       // public int HisUpdateOrderExecState(long orderId, string execOperCode, DateTime execOperTime, string execMemo)
       // {
       //     string operId = this.DoctCodeToId(execOperCode);
       //     if (string.IsNullOrEmpty(operId))
       //     {
       //         this.Err = "取得EMR医生ID失败！DoctCode：" + execOperCode;
       //         this.WriteErr();
       //         return -1;
       //     }

       //     return this.UpdateSingleTable("EMR.ORD.UpdateOrderExecState", orderId.ToString(),operId,execOperTime.ToString("yyyy-MM-dd HH:mm:ss"),execMemo);

       // }

       // /// <summary>
       // /// 更新门诊医嘱退费后回写总量
       // /// </summary>
       // /// <param name="orderId">Emr医嘱ID</param>
       // /// <param name="numerator">分子</param>
       // /// <param name="denominator">分母</param>
       // /// <returns>成功返回影响行数，失败返回-1</returns>
       // public int UpdateOpmOrderForRefundQty(long orderId, int numerator, int denominator)
       // {
       //     if (denominator <= 0)
       //     {
       //         this.Err = "回写门诊医嘱退费时，分母不能为零！";
       //         this.WriteErr();
       //         return -1;
       //     }
       //     return this.UpdateSingleTable("EMR.ORD.UpdateOrderForRefundQty", orderId.ToString(), numerator.ToString(), denominator.ToString());
       // }

       // /// <summary>
       // /// 更新单表操作
       // /// </summary>
       // /// <param name="sqlIndex">SQL语句索引</param>
       // /// <param name="args">参数</param>
       // /// <returns>成功: >= 1 失败 -1 没有更新到数据 0</returns>
       // public int UpdateSingleTable(string sqlIndex, params string[] args)
       // {
       //     string sql = string.Empty;//Update语句

       //     //获得Where语句
       //     if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
       //     {
       //         this.Err = "没有找到索引为:" + sqlIndex + "的SQL语句";

       //         return -1;
       //     }

       //     return this.ExecNoQuery(sql, args);
       // }

      
       // #region 私有方法
       // /// <summary>yushuai
       // /// 根据就诊卡号取得患者ID
       // /// </summary>
       // /// <param name="cardNo">就诊卡号</param>
       // /// <returns>患者ID</returns>
       //private string CardNoToPatientId(string cardNo)
       //{
       //     string sql = string.Format("SELECT a.emr_patid FROM com_patientinfo a WHERE a.card_no = '{0}'", cardNo);
       //     return this.ExecSqlReturnOne(sql);
       //}
       // /// <summary>yushuai
       // /// 根据药品编码取得药品ID
       // /// </summary>
       // /// <param name="drugCode">药品编码</param>
       // /// <returns>药品ID</returns>
       //private string DrugCodeToDrugId(string drugCode)
       //{
       //    string sql = string.Format("SELECT b.idFROM ordt_drg b WHERE b.former_code = '{0}'", drugCode);
       //    return this.ExecSqlReturnOne(sql);
       //}
       // /// <summary>yushuai
       // /// 根据药品编码取得药品名称
       // /// </summary>
       // /// <param name="drugCode">药品编码</param>
       // /// <returns>药品名称</returns>
       // private string DrugCodeToDrugName(string drugCode)
       // {
       //     string sql = string.Format("SELECT b.trade_name FROM ordt_drg b WHERE b.former_code = '{0}'", drugCode);
       //     return this.ExecSqlReturnOne(sql);
       // }
       // /// <summary>yushuai
       // /// 根据药理作用编码取得药理作用名称
       // /// </summary>
       // /// <param name="funCode">药理作用编码</param>
       // /// <returns>药理作用名称</returns>
       // private string FunCodeToName(string funCode)
       // {
       //     string sql = string.Format("SELECT c.node_name FROM pha_com_function c WHERE c.node_code = '{0}'", funCode);
       //      return this.ExecSqlReturnOne(sql);
       // }

       // /// <summary>
       // /// 根据医生Code（His）取医生ID（emr）
       // /// </summary>
       // /// <param name="doctCode">医生Code（His）</param>
       // /// <returns>医生ID（emr）</returns>
       // private string DoctCodeToId(string doctCode)
       // {
       //     string sql = string.Format("select id from vemr_emp where empl_code = '{0}'", doctCode);
       //     return this.ExecSqlReturnOne(sql);

       // }

       // #endregion


        #endregion

    }
}
