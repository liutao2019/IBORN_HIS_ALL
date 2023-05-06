using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Neusoft.HISFC.Models.Fee.Outpatient;
using System.Windows.Forms;
using Neusoft.HISFC.BizProcess.Interface.Common;

namespace HISTIMEJOB
{
    /// <summary>
    /// 医保费用上传
    /// </summary>
    public class SIPactUpdateLoad : Neusoft.FrameWork.Management.Database, IJob
    {
        #region IJob 成员

        public string Message
        {
            get { throw new NotImplementedException(); }
        }

        public int Start()
        {
            return this.StartUpLoad();
        }

        #endregion

        #region 变量
        //服务器时间
        private DateTime dtServerDateTime;
        private DateTime dtBeginDateTime = new DateTime(2000, 1, 1);
        #endregion




        #region 处理函数

        public int StartUpLoad()
        {
            int iRes = 1;

            dtServerDateTime = this.regManage.GetDateTimeFromSysDateTime();
            DateTime dtEndDateTime = dtServerDateTime.Date.AddSeconds(-1);

            #region 调试时间
            ////2012-04-04 10:48:16 调试
           // dtBeginDateTime = new DateTime(2012, 4, 1, 0, 0, 0);
           // dtEndDateTime = new DateTime(2012, 4, 26, 23, 59, 59);
            dtEndDateTime = dtServerDateTime;
            #endregion


            ArrayList regInfoList = regManage.QueryYNSeeRegister(dtBeginDateTime, dtEndDateTime);
            if (regInfoList == null || regInfoList.Count <= 0)
            {
                this.Err = dtBeginDateTime + "~" + dtEndDateTime + "的医保待上传人次 : "+regInfoList.Count + this.regManage.Err;
                this.WriteErr();
                return iRes;
            }


            ArrayList arlFeeItemList = null;
            ArrayList arlFeeItemUpdateload = null;
            foreach (Neusoft.HISFC.Models.Registration.Register regInfo in regInfoList)
            {

                //防止一人多卡，患者挂号表为居民医保，结算时特定医保结算，晚上居民医保重复上传的问题
                ArrayList al =fun.GetYBMEDNOByClinic(regInfo.ID);
                if (al!=null && al.Count >0)
                {
                    //更新挂号表的挂号的合同单位
                    Neusoft.HISFC.Models.Base.PactInfo pact=al[0] as Neusoft.HISFC.Models.Base.PactInfo;
                    if (pact.ID==regInfo.Pact.ID)
                    {
                        continue;
                    }
                    if (-1 == fun.UpdateRegister(regInfo.ID, pact))
                    {
                        this.Err = regInfo.ID + "：更新挂号表出错（更新特点医保合同单位） " + fun.Err;
                        this.WriteErr();
                    }
                    continue;
                }

                arlFeeItemList = outpatientManage.QueryFeeItemByClinicCode(regInfo.ID, regInfo.Pact.ID);
                if (arlFeeItemList == null || arlFeeItemList.Count <= 0)
                {
                    this.Err = regInfo.ID+"：无费用记录 " + this.outpatientManage.Err;
                    this.WriteErr();
                    continue;
                }
                System.Collections.Hashtable hsItems = new Hashtable();
                bool isReture = false;//出现同处方、同医嘱号，不上传
                arlFeeItemUpdateload = new ArrayList();
                foreach (FeeItemList item in arlFeeItemList)
                {
                    if (item.PayType == Neusoft.HISFC.Models.Base.PayTypes.Charged)
                    {
                        this.Err = regInfo.ID+ item.Item.ID + "--" + item.Item.Name + "：未收费 ";
                        this.WriteErr();
                        continue;
                    }

                    if (item.User03 == "1")
                    {
                        this.Err = regInfo.ID + item.Item.ID + "--" + item.Item.Name + "：已上传 ";
                        this.WriteErr();
                        // // 已上传
                        continue;
                    }

                    if (hsItems.Contains(item.Item.ID))
                    {
                        FeeItemList obj = hsItems[item.Item.ID] as FeeItemList;
                        if (obj.RecipeNO == item.RecipeNO && obj.Order.ID == item.Order.ID)
                        {
                            isReture = true;
                            this.Err="存在 项目代码：" + item.Item.ID + " 项目名称：" + item.Item.Name + " 的处方号、医嘱号一致，违反fs_local_masmzjsmx表的唯一性";
                            this.WriteErr();
                            break;
                        }
                        else
                        {
                            arlFeeItemUpdateload.Add(item);
                        }
                    }
                    else
                    {
                        hsItems.Add(item.Item.ID, item);
                        arlFeeItemUpdateload.Add(item);
                    }
                }
                //不上传，直接下一个患者
                if (isReture || arlFeeItemUpdateload.Count <= 0)
                {             
                    continue;
                }
                
  

                // 标识自动上传，医保接口处理
                regInfo.User03 = "自动上传";

                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                medcareProxy.SetPactCode(regInfo.Pact.ID);
                medcareProxy.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                medcareProxy.BeginTranscation();
               // medcareProxy.SetPactCode(regInfo.Pact.ID);
                medcareProxy.IsLocalProcess = false;

                //连接待遇接口
                long returnValue = this.medcareProxy.Connect();
                if (returnValue == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    //医保回滚可能出错，此处提示
                    this.medcareProxy.Rollback();

                    this.Err = "社保连接 Connect " + this.medcareProxy.ErrMsg;
                    this.WriteErr();

                    continue;
                }

                regInfo.UpFlag = "1";//补单标记(接口中判断)
                // 判断是否允许报销
                if (this.medcareProxy.IsInBlackList(regInfo))
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    //医保回滚可能出错，此处提示
                    //this.medcareProxy.Rollback();

                    this.Err = regInfo.ID+"医保记账次数 IsInBlackList " + this.medcareProxy.ErrMsg + " 【" + regInfo.Name + " --- " + regInfo.PID.CardNO +  " --- " + regInfo.ID + "】";
                    this.WriteErr();

                    continue;
                }

                //删除本次因为错误或者其他原因上传的明细
                returnValue = this.medcareProxy.DeleteUploadedFeeDetailsAllOutpatient(regInfo);
                if (returnValue == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    //医保回滚可能出错，此处提示
                    this.medcareProxy.Rollback();

                    this.Err = regInfo.ID +"删除社保已经上传费用明细  DeleteUploadedFeeDetailsAllOutpatient " + this.medcareProxy.ErrMsg;
                    this.WriteErr();

                    continue;
                }

                
                //重新上传所有明细
                returnValue = this.medcareProxy.UploadFeeDetailsOutpatient(regInfo, ref arlFeeItemUpdateload);
                if (returnValue == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    //医保回滚可能出错，此处提示
                    this.medcareProxy.Rollback();

                    this.Err = regInfo.ID +"UploadFeeDetailsOutpatient " + this.medcareProxy.ErrMsg;
                    this.WriteErr();


                    continue;
                }
                
                // 因居民医保比较特殊,结算时并未进行结算,只是取数据而已,所以这里要调用预结算方法
              //  returnValue = this.medcareProxy.PreBalanceOutpatient(regInfo, ref arlFeeItemUpdateload);
                returnValue = this.medcareProxy.BdBalanceOutpatient(regInfo, ref arlFeeItemUpdateload);
                if (returnValue == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    //医保回滚可能出错，此处提示k
                    this.medcareProxy.Rollback();

                    this.Err = regInfo.ID+"BdBalanceOutpatient " + this.medcareProxy.ErrMsg;
                    this.WriteErr();

                    continue;
                }
                #region 补单结算后，不需再结算
                //returnValue = this.medcareProxy.BalanceOutpatient(regInfo, ref arlFeeItemUpdateload);
                //if (returnValue != 1)
                //{
                //    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                //    this.medcareProxy.Rollback();

                //    this.Err = "BalanceOutpatient " + this.medcareProxy.ErrMsg;
                //    this.WriteErr();

                //    continue;
                //}
                #endregion
                Neusoft.FrameWork.Management.PublicTrans.Commit();
                this.Err = regInfo.ID + "补单结算成功 " ;
                this.WriteErr();

                Application.DoEvents();
                System.Threading.Thread.Sleep(100);
            }

            return iRes;
        }


        #endregion


        #region 业务层
        /// <summary>
        /// 门诊挂号业务管理
        /// </summary>
        Neusoft.HISFC.BizLogic.Registration.Register regManage = new Neusoft.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 门诊费用业务管理
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.Outpatient outpatientManage = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// 医疗待遇接口
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareProxy = new Neusoft.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        Function fun = new Function();
        #endregion
    }
}
