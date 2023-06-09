
using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using FS.HISFC.BizProcess.Interface.FeeInterface;
namespace FS.HISFC.BizProcess.Integrate.FeeInterface
{
    public class MedcareInterfaceProxy : FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare, FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend, FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan
    {
        public MedcareInterfaceProxy()
        {

        }


        /// <summary>
        /// 直接付值合同单位
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        public MedcareInterfaceProxy(string pactCode, System.Data.IDbTransaction t)
        {
            this.pactCode = pactCode;
            this.SetPactCode(pactCode);
            this.trans = t;
        }

        /// <summary>
        /// 当该类结束的时候,释放接口实例
        /// </summary>
        ~MedcareInterfaceProxy()
        {
            this.medcaredInterface = null;
            //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
            interfaceHash.Clear();
        }

        /// <summary>
        /// 设置合同单位
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        /// <returns>成功 1 失败 -1</returns>
        public int SetPactCode(string pactCode)
        {
            this.pactCode = pactCode;

            return this.GetInterfaceFromPact(pactCode);
        }

        /// <summary>
        /// 验证挂号信息是否合法
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <returns>符合: true 不符合 false</returns>
        private bool IsValid(FS.HISFC.Models.Registration.Register r)
        {
            if (this.medcaredInterface == null)
            {
                if (r == null)
                {
                    this.errMsg = "挂号信息为空";

                    return false;
                }
                if (r.Pact == null || r.Pact.ID == null || r.Pact.ID == string.Empty)
                {
                    this.errMsg = "合同单位为空!";

                    return false;
                }

                this.GetInterfaceFromPact(r.Pact.ID);

                if (this.medcaredInterface == null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 验证登记信息是否合法
        /// </summary>
        /// <param name="patient">挂号信息</param>
        /// <returns>符合: true 不符合 false</returns>
        private bool IsValid(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (this.medcaredInterface == null)
            {
                if (patient == null)
                {
                    this.errMsg = "住院患者基本信息信息为空";

                    return false;
                }
                if (patient.Pact == null || patient.Pact.ID == null || patient.Pact.ID == string.Empty)
                {
                    this.errMsg = "合同单位为空!";

                    return false;
                }

                this.GetInterfaceFromPact(patient.Pact.ID);

                if (this.medcaredInterface == null)
                {
                    return false;
                }
            }

            return true;
        }

        #region 变量

        /// <summary>
        /// 医保接口实例
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare medcaredInterface = null;

        /// <summary>
        /// 本地数据库连接
        /// </summary>
        private System.Data.IDbTransaction trans = null;

        /// <summary>
        /// 当前错误信息
        /// </summary>
        private string errMsg = string.Empty;

        /// <summary>
        /// 合同单位编码
        /// </summary>
        private string pactCode = null;//合同单位编码

        /// <summary>
        /// 合同单位管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// 当前载入的接口类型
        /// </summary>
        private static Hashtable interfaceHash = new Hashtable();

        #endregion

        #region 属性

        /// <summary>
        /// 合同单位编码
        /// </summary>
        public string PactCode
        {
            set
            {
                this.pactCode = value;
            }
            get
            {
                return this.pactCode;
            }
        }

        /// <summary>
        /// 当前接口实例
        /// </summary>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare NowMedcaredInterface
        {
            get
            {
                return this.medcaredInterface;
            }
        }

        #endregion


        #region IMedcare 成员

        /// <summary>
        /// 本地数据库事务
        /// </summary>
        public System.Data.IDbTransaction Trans
        {
            set
            {
                this.SetTrans(value);
            }
        }

        /// <summary>
        /// 错误编码
        /// </summary>
        public string ErrCode
        {
            get
            {
                if (this.medcaredInterface != null)
                {
                    return this.medcaredInterface.ErrCode;
                }
                else
                {
                    return "实例为空!";
                }
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }

        }

        /// <summary>
        /// 接口描述信息
        /// </summary>
        public string Description
        {
            get
            {
                if (this.medcaredInterface != null)
                {
                    return this.medcaredInterface.Description;
                }

                return "实例为空!";
            }
        }

        /// <summary>
        /// 通过合同单位编码获得
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        /// <returns>成功 1 失败 -1</returns>
        public int GetInterfaceFromPact(string pactCode)
        {
            //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
            if (MedcareInterfaceProxy.interfaceHash.ContainsKey(pactCode))
            {
                //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                this.medcaredInterface = (IMedcare)MedcareInterfaceProxy.interfaceHash[pactCode];
                if (this.medcaredInterface != null)
                {
                    return 1;
                }
            }

            if (this.trans != null)
            {
                this.pactManager.SetTrans(this.trans);
            }

            FS.HISFC.Models.Base.PactInfo pactInfo = this.pactManager.GetPactUnitInfoByPactCode(pactCode);
            if (pactInfo == null)
            {
                this.errMsg = "获得患者合同单位出错!(接口)" + this.pactManager.Err;

                return -1;
            }
            if (pactInfo.PactDllName == null || pactInfo.PactDllName == string.Empty)
            {
                this.errMsg = "编号为: " + pactCode + "名称为: " + pactInfo.Name + "的合同单位没有维护待遇算法!";

                return -1;
            }

            try
            {
                // Assembly a = Assembly.LoadFrom(FS.FrameWork.WinForms.Classes.Function.PluginPath + "\\SI\\" + pactInfo.PactDllName);
                Assembly a = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "\\" + FS.FrameWork.WinForms.Classes.Function.PluginPath + "\\SI\\" + pactInfo.PactDllName);


                System.Type[] types = a.GetTypes();
                foreach (System.Type type in types)
                {
                    if (type.GetInterface("IMedcare") != null)
                    {
                        this.medcaredInterface = (IMedcare)System.Activator.CreateInstance(type);
                    }
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }

            //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
            MedcareInterfaceProxy.interfaceHash.Add(pactCode, this.medcaredInterface);

            return 1;
        }

        /// <summary>
        /// 设置本地数据库连接
        /// </summary>
        /// <param name="t">当前数据库连接</param>
        public void SetTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
            try
            {
                if (this.medcaredInterface == null)
                {
                    //如果当前的实例为null,重新获得医保对象实例
                    this.GetInterfaceFromPact(pactCode);
                    if (medcaredInterface == null)
                    {
                        return;
                    }
                }

                medcaredInterface.SetTrans(t);
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return;
            }
        }

        public void SetPactTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
            this.pactManager.SetTrans(t);
            return;
        }
        /// <summary>
        /// 获得黑名单信息
        /// </summary>
        /// <param name="blackLists">黑名单信息</param>
        /// <returns>成功 1 失败 -1</returns>
        public int QueryBlackLists(ref ArrayList blackLists)
        {
            try
            {
                if (this.medcaredInterface == null)
                {
                    this.GetInterfaceFromPact(this.pactCode);

                    if (this.medcaredInterface == null)
                    {
                        this.errMsg = this.medcaredInterface.ErrMsg;
                        return -1;
                    }
                }

                int iReturn = 0;

                iReturn = this.medcaredInterface.QueryBlackLists(ref blackLists);

                if (iReturn <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 住院患者是否再黑名单中
        /// </summary>
        /// <param name="patient">住院患者基本信息</param>
        /// <returns>在 true 不在 false</returns>
        public bool IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return true;
                }

                bool returnValue = this.medcaredInterface.IsInBlackList(patient);
                if (returnValue)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;

            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return true;
            }
        }

        /// <summary>
        /// 门诊患者是否再黑名单中
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <returns>在 true 不在 false</returns>
        public bool IsInBlackList(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return false;
                }

                bool returnValue = this.medcaredInterface.IsInBlackList(r);
                if (returnValue == true)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;

            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return false;
            }
        }

        /// <summary>
        /// 获得医保或者公费非药品列表
        /// </summary>
        /// <param name="undrugLists">非药品列表</param>
        /// <returns>成功 1 失败 -1</returns>
        public int QueryUndrugLists(ref ArrayList undrugLists)
        {
            try
            {
                if (this.medcaredInterface == null)
                {
                    this.GetInterfaceFromPact(this.pactCode);

                    if (this.medcaredInterface == null)
                    {
                        this.errMsg = this.medcaredInterface.ErrMsg;
                        return -1;
                    }
                }

                int iReturn = 0;

                iReturn = this.medcaredInterface.QueryUndrugLists(ref undrugLists);

                if (iReturn <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 获得医保或者公费药品列表
        /// </summary>
        /// <param name="drugLists">药品列表</param>
        /// <returns>成功 1 失败 -1</returns>
        public int QueryDrugLists(ref ArrayList drugLists)
        {
            try
            {
                if (this.medcaredInterface == null)
                {
                    this.GetInterfaceFromPact(this.pactCode);

                    if (this.medcaredInterface == null)
                    {
                        this.errMsg = this.medcaredInterface.ErrMsg;
                        return -1;
                    }
                }

                int iReturn = 0;

                iReturn = this.medcaredInterface.QueryDrugLists(ref drugLists);

                if (iReturn <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 门诊医保或者公费登记函数
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int UploadRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UploadRegInfoOutpatient(r);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 获得门诊医保公费登记信息
        /// </summary>
        /// <param name="r">门诊挂号实体</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.GetRegInfoOutpatient(r);
                //{187A73EB-008A-4A25-A6CB-28CAE0E629A7}查询其他个人信息添加备注信息
                FS.HISFC.BizLogic.RADT.InPatient patientManger = new FS.HISFC.BizLogic.RADT.InPatient();
                r.Memo = patientManger.QueryComPatientInfo(r.PID.CardNO).Memo;
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 获得门诊医保公费登记信息
        /// </summary>
        /// <param name="r">门诊挂号实体</param>
        /// <param name="isReadMCard">是否从医保卡中读卡</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r, bool isReadMCard)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.GetRegInfoOutpatient(r);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }


        /// <summary>
        /// 门诊单条上传费用明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="f">门诊费用明细</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UploadFeeDetailOutpatient(r, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 门诊多条上传费用明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="feeDetails">费用明细实体集合</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UploadFeeDetailsOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 门诊删除单条已经上传明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="f">费用明细信息</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailOutpatient(r, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 门诊删除患者的所有费用上传明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailsAllOutpatient(r);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        ///门诊 删除指定数据集的明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="feeDetails">要删除的费用实体明细</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailsOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 修改单条门诊已上传明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="f">要修改的费用实体明细</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int ModifyUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.ModifyUploadedFeeDetailOutpatient(r, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 修改多条门诊已上传明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="feeDetails">要修改的费用实体明细集合</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.ModifyUploadedFeeDetailsOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 门诊医保预结算
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int PreBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.PreBalanceOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 门诊医保结算
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.BalanceOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }


        /// <summary>
        /// 门诊取消结算
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">要取消结算的患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.CancelBalanceOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 门诊取消结算（半退）
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">要取消结算的患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.CancelBalanceOutpatientHalf(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院更新费用信息
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="f">费用明细</param>
        /// <returns>成功 1 失败 -1 没有更新到数据 0</returns>
        public int UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UpdateFeeItemListInpatient(patient, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院重新计算费用明细
        /// </summary>
        /// <param name="patient">住院患者基本信息</param>
        /// <param name="f">住院费用单条明细</param>
        /// <returns>成功 1 失败 -1</returns>
        public int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.RecomputeFeeItemListInpatient(patient, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院登记函数
        /// </summary>
        /// <param name="patient">住院登记患者基本信息</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int UploadRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }
                int returnValue = this.medcaredInterface.UploadRegInfoInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 获得医保住院登记信息
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.GetRegInfoInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院单条上传费用明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="f">住院费用明细</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UploadFeeDetailInpatient(patient, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院多条上传费用明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">费用明细实体集合</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UploadFeeDetailsInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院删除单条已经上传明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="f">费用明细信息</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailInpatient(patient, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院删除患者的所有费用上传明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailsAllInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院删除指定数据集的明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">要删除的费用实体明细</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailsInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 修改单条住院已上传明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="f">要修改的费用实体明细</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.ModifyUploadedFeeDetailInpatient(patient, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 修改多条住院已上传明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">要修改的费用实体明细集合</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.ModifyUploadedFeeDetailsInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院医保预结算
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.PreBalanceInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院医保中途结算
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.MidBalanceInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院医保结算
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.BalanceInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 住院取消结算
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">要取消结算的患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int CancelBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.CancelBalanceInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        #endregion

        #region IMedcareTranscation 成员

        /// <summary>
        /// 接口连接,初始化方法
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Connect()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "合同单位没有赋值";

                return -1;
            }
            if (this.medcaredInterface == null)
            {
                int returnValue = this.GetInterfaceFromPact(this.pactCode);
                if (returnValue == -1)
                {
                    //this.errMsg = this.medcaredInterface.ErrMsg;
                    return -1;
                }
            }
            //{87DE75DB-BF2E-4f68-9C28-15D043C1D49E}
            //return this.medcaredInterface.Connect();
            long returnV = this.medcaredInterface.Connect();
            if (returnV < 0)
            {
                this.errMsg = this.ErrMsg;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 关闭接口连接 清空方法
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Disconnect()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "合同单位没有赋值";

                return -1;
            }
            if (this.medcaredInterface == null)
            {
                int returnValue = this.GetInterfaceFromPact(this.pactCode);
                if (returnValue == -1)
                {
                    // this.errMsg = this.medcaredInterface.ErrMsg;
                    return -1;
                }
            }

            //{87DE75DB-BF2E-4f68-9C28-15D043C1D49E}
            //return this.medcaredInterface.Disconnect();
            long returnV = this.medcaredInterface.Disconnect();
            if (returnV < 0)
            {
                this.errMsg = this.ErrMsg;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 接口提交方法
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Commit()
        {
            //{7E721E47-0F64-4b43-8B42-36926A96D9CB}
            #region 旧代码-屏蔽
            //if (this.pactCode == null)
            //{
            //    this.errMsg = "合同单位没有赋值";

            //    return -1;
            //}
            //if (this.medcaredInterface == null)
            //{
            //    int returnValue = this.GetInterfaceFromPact(this.pactCode);
            //    if (returnValue == -1)
            //    {
            //        this.errMsg = this.medcaredInterface.ErrMsg;
            //        return -1;
            //    }
            //}

            //return this.medcaredInterface.Commit();
            #endregion

            ArrayList pactList = this.pactManager.QueryPactUnitAll();
            if (pactList == null)
            {
                this.errMsg = "查找合同单位失败";
                return -1;
            }
            if (pactList.Count == 0)
            {
                this.errMsg = "合同单位未维护";
                return -1;
            }
            for (int i = 0; i < pactList.Count; i++)
            {
                FS.HISFC.Models.Base.PactInfo nowPactCode = pactList[i] as FS.HISFC.Models.Base.PactInfo;

                IMedcare myMedcare = null;

                //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                if (MedcareInterfaceProxy.interfaceHash.ContainsKey(nowPactCode.ID))
                {
                    myMedcare = (IMedcare)MedcareInterfaceProxy.interfaceHash[nowPactCode.ID];
                    if (myMedcare != null)
                    {
                        if (myMedcare.Commit() < 0)
                        {
                            return -1;
                        }
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 接口回滚方法
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Rollback()
        {
            //{21105AAF-1A77-4614-9D13-C3F3233E8DE9}
            #region 旧代码-屏蔽
            //if (this.pactCode == null)
            //{
            //    this.errMsg = "合同单位没有赋值";

            //    return -1;
            //}
            //if (this.medcaredInterface == null)
            //{
            //    int returnValue = this.GetInterfaceFromPact(this.pactCode);
            //    if (returnValue == -1)
            //    {
            //        this.errMsg = this.medcaredInterface.ErrMsg;
            //        return -1;
            //    }
            //}

            //return this.medcaredInterface.Rollback();
            #endregion

            ArrayList pactList = this.pactManager.QueryPactUnitAll();
            if (pactList == null)
            {
                this.errMsg = "查找合同单位失败";
                return -1;
            }
            if (pactList.Count == 0)
            {
                this.errMsg = "合同单位未维护";
                return -1;
            }
            int i = 0;
            for (i = 0; i < pactList.Count; i++)
            {
                FS.HISFC.Models.Base.PactInfo nowPactCode = pactList[i] as FS.HISFC.Models.Base.PactInfo;

                if (this.pactCode != nowPactCode.ID) continue;

                IMedcare myMedcare = null;

                //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                if (MedcareInterfaceProxy.interfaceHash.ContainsKey(nowPactCode.ID))
                {
                    myMedcare = (IMedcare)MedcareInterfaceProxy.interfaceHash[nowPactCode.ID];
                    if (myMedcare != null)
                    {
                        if (myMedcare.Rollback() < 0)
                        {
                            this.errMsg = myMedcare.ErrMsg;
                            continue;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// 接口开始数据事务函数
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public void BeginTranscation()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "合同单位没有赋值";

                return;
            }
            if (this.medcaredInterface == null)
            {
                int returnValue = this.GetInterfaceFromPact(this.pactCode);
                if (returnValue == -1)
                {
                    //this.errMsg = this.medcaredInterface.ErrMsg;
                    return;
                }
            }

            this.medcaredInterface.BeginTranscation();
        }

        #endregion

        public int OpenAll()
        {
            //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
            foreach (DictionaryEntry d in MedcareInterfaceProxy.interfaceHash)
            {
                ((IMedcare)d.Value).Connect();
            }

            return 0;
        }

        public int CloseAll()
        {
            //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
            foreach (DictionaryEntry d in MedcareInterfaceProxy.interfaceHash)
            {
                ((IMedcare)d.Value).Disconnect();
            }

            return 0;
        }

        #region IMedcare 成员

        /// <summary>
        /// 取消住院登记方法
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <returns>成功 1 失败 -1</returns>
        public int CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.CancelRegInfoInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }
        /// <summary>
        /// 出院召回
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <returns>成功 1 失败 -1</returns>
        public int RecallRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.RecallRegInfoInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// 出院登记方法
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns>成功 1 失败 -1</returns>
        public int LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.LogoutInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        #endregion

        #region IMedcare 成员

        /// <summary>
        /// 门诊退号
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.CancelRegInfoOutpatient(r);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        #endregion



        #region IMedcare 成员

        /// <summary>
        /// 门诊结算时是否整体上传
        /// </summary>
        /// <remarks>true 整体上传 false 部分上传</remarks>       
        public bool IsUploadAllFeeDetailsOutpatient
        {
            get
            {
                if (this.medcaredInterface != null)
                {
                    return this.medcaredInterface.IsUploadAllFeeDetailsOutpatient;
                }

                return true;
            }
        }

        #endregion

        #region IMedcare 成员

        #region {AD6E49F9-7409-48b1-A297-73610F0072C7}

        public int QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.QueryFeeDetailsInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        public int QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.QueryFeeDetailsOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        #endregion
        #endregion

        #region IMedcareExtend 成员
        // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
        public bool IsLocalProcess
        {
            get
            {
                if (this.medcaredInterface != null)
                {
                    if (this.medcaredInterface is FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend)
                    {
                        return (this.medcaredInterface as FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend).IsLocalProcess;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.medcaredInterface != null)
                {
                    if (this.medcaredInterface is FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend)
                    {
                        (this.medcaredInterface as FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend).IsLocalProcess = value;
                    }
                }
            }
        }

        /// <summary>
        /// HIS内部医保结算，如果未实现此方法，默认处理成功
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <param name="arlOther">其他信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int LocalBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails, ArrayList arlOther)
        {
            int iRes = 1;
            this.errMsg = "";

            if (this.medcaredInterface != null)
            {
                if (this.medcaredInterface is FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend)
                {
                    iRes = (this.medcaredInterface as FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend).LocalBalanceOutpatient(r, ref feeDetails, arlOther);
                    //if (iRes <= 0)
                    //{
                    //    this.errMsg = this.medcaredInterface.ErrMsg;
                    //}

                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                else
                {
                    //iRes = -1;
                    //this.errMsg = this.Description + "  不支持本地结算，请到收费处结算！";
                }
            }

            return iRes;
        }
        #endregion

        #region IMedcareBuDan 成员

        /// <summary>
        /// HIS内部医保补单结算，
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int BdBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            int iRes = 1;
            this.errMsg = "";

            if (this.medcaredInterface != null)
            {
                if (this.medcaredInterface is FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan)
                {
                    iRes = (this.medcaredInterface as FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan).BdBalanceOutpatient(r, ref feeDetails);
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
            }

            return iRes;
        }
        /// <summary>
        ///预结算后记录社保的结算信息
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int BalanceOutpatientAfterPreBalance(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            int iRes = 1;
            this.errMsg = "";

            if (this.medcaredInterface != null)
            {
                if (this.medcaredInterface is FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan)
                {
                    iRes = (this.medcaredInterface as FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan).BalanceOutpatientAfterPreBalance(r, ref feeDetails);
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
            }

            return iRes;
        }

        /// <summary>
        /// 扫医保码
        /// </summary>
        /// <param name="r"></param>
        /// <param name="codeNum"></param>
        /// <returns></returns>
        public string GetCodeScanningVerification(FS.HISFC.Models.Registration.Register r, string codeNum)
        {

            string idnum = "";

            idnum = this.medcaredInterface.GetCodeScanningVerification(r, codeNum);

            return idnum;
        }
        #endregion


        #region IMedcare 成员

        /// <summary>
        /// 判断指定门诊病人是否享受此医保
        /// {199EF4E9-EF21-4067-97A7-9AA97AF74CDE}
        /// </summary>
        /// <param name="r"></param>
        /// <returns>
        /// 0：可以享受居民门诊医保报销并且当日无报销记录
        /// -1：不可以享受居民门诊医保报销
        /// -2：失败
        /// 其它值：可以享受居民门诊医保报销并且当日有报销记录（返回值为当日报销次数）
        /// </returns>
        public int QueryCanMedicare(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -2;
                }

                int returnValue = this.medcaredInterface.QueryCanMedicare(r);
                if (returnValue == -2 || returnValue == -1)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;

            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -2;
            }
        }

        #endregion

        #region IMedcare 成员


        //public int CancelSIBalance(FS.HISFC.Models.RADT.PatientInfo patient, ref string error)
        //{
        //    return this.medcaredInterface.CancelSIBalance(patient, ref error);
        //}

        //public int CancelSIBeforeBalance(FS.HISFC.Models.RADT.PatientInfo patient, ref string error)
        //{
        //    return this.medcaredInterface.CancelSIBeforeBalance(patient, ref error);
        //}

        #endregion
    }
}


