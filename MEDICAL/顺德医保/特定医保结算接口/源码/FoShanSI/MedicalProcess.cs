using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.BizProcess.FeeInterface.Local;
using FS.HISFC.Models.Fee.Outpatient;

namespace FoShanSI
{
    /// <summary>
    /// 广东佛山医保接口
    /// 张琦
    /// 2010-7月
    /// </summary>
    public class MedicalProcess : FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare, FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan
    {
        FS.FrameWork.Models.NeuLog log = null;
        public MedicalProcess()
        {
            log = new FS.FrameWork.Models.NeuLog(string.Concat(FS.FrameWork.WinForms.Classes.Function.CurrentPath, "\\sidebug.log"));
            this.isUploadInvoiceNo = controlParamIntegrate.GetControlParam<bool>("I00018", false);
        }

        //public MedicalProcess()
        //{
        //    this.isUploadInvoiceNo = controlParamIntegrate.GetControlParam<bool>("I00018", false);
        //}

        #region 变量

        #region 是否需要上传发票号
        /// <summary>
        /// 是否需要上传发票号
        /// </summary>
        private bool isUploadInvoiceNo = false;
        #endregion

        /// <summary>
        /// 本地HIS业务处理层
        /// </summary>
        Management.SIDealLocalBusiness siLocalBusiness = new FoShanSI.Management.SIDealLocalBusiness();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 医保业务访问层
        /// </summary>
        static Management.SIBizProcess siBizProcess = null;

        /// <summary>
        /// 错误编码
        /// </summary>
        protected string errCode = string.Empty;

        /// <summary>
        /// 错误描述
        /// </summary>
        protected string errMsg = string.Empty;

        /// <summary>
        /// 事物信息
        /// </summary>
        private System.Data.IDbTransaction trans = null;

        /// <summary>
        /// 设置DB2连接界面
        /// </summary>
        Components.ucConnectServer ucConnectDB2Server = new FoShanSI.Components.ucConnectServer();


        ///药品业务层
        FS.HISFC.BizLogic.Pharmacy.Item itemPharmacyManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        //非药品业务层
        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 结算次数
        /// </summary>
        int siPatientBalanceCount = 0;
        /// <summary>
        /// 是否已上传
        /// </summary>
        bool IsUpdateLoad = false;
        /// <summary>
        /// 减免计算
        /// {484CCBB6-81F6-4268-A4BF-D66033A6B187}
        /// </summary>
        private clsPublicLocalSI objPublicLocalSI = new clsPublicLocalSI();

        /// <summary>
        /// 挂号费用业务 {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accMgr = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 医保接口代理【居民医保】
        /// {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        #endregion

        #region 属性

        /// <summary>
        /// 待遇接口描述
        /// </summary>
        public string Description
        {
            get { return "广东省佛山市医保接口"; }
        }

        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrCode
        {
            get { return errCode; }
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrMsg
        {
            get { return errMsg; }
        }

        /// <summary>
        /// 事物
        /// </summary>
        /// <param name="t"></param>
        public void SetTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
        }

        public System.Data.IDbTransaction Trans
        {
            set { this.trans = value; }
        }

        #endregion

        #region IMedcare 成员

        #region 门诊

        #region 挂号

        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            //医保患者不需要挂号，不访问医保前置机
            //连接医保接口
            //siLocalBusiness.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //int iReturn = -1;
            //iReturn = siLocalBusiness.InsertSIMainInfo(r);
            //if (iReturn == -1)
            //{
            //    this.errMsg = "插入医保登记出错!" + siLocalBusiness.Err;
            //    return -1;
            //}
            return 1;
        }

        /// <summary>
        /// 挂号诊查费用(挂号) {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
        /// </summary>
        List<FS.HISFC.Models.Account.AccountCardFee> accFeeList = null;

        /// <summary>
        ///  项目主键对应项目 {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
        /// </summary>
        private Dictionary<string, FS.HISFC.Models.Fee.Item.Undrug> dictDiagItem = null;

        /// <summary>
        /// 上传挂号信息
        /// {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int UploadRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            //判断患者一天一诊次 {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
            DateTime dtNow = this.accMgr.GetDateTimeFromSysDateTime();
            List<FS.HISFC.Models.Account.AccountCardFee> lstPubFee = new List<FS.HISFC.Models.Account.AccountCardFee>();
            //爱博恩目前挂号不收费，该SQL语句未维护，先注销
            //if (this.accMgr.QueryAccountCardFeeByPubAndCardNoAndDate(r.PID.CardNO, dtNow.Date, dtNow.Date.AddDays(1).AddSeconds(-1), out lstPubFee) == -1)
            //{
            //    this.errMsg = "查找患者的医院垫付诊查费用失败!" + this.accMgr.Err;
            //    return -1;
            //}
            if (lstPubFee != null && lstPubFee.Count > 0)
            {
                FS.HISFC.Models.Account.AccountCardFee acf = lstPubFee[0];
                this.errMsg = string.Format("该患者当天存在医保支付的挂号!挂号发票为：{0}\r\n请选择非医保合同单位挂号", acf.Print_InvoiceNo);
                return -1;
            }

            #region 计算统筹金额和优惠金额

            //{6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
            if (this.dictDiagItem == null)
            {
                #region 初始化

                this.dictDiagItem = new Dictionary<string, FS.HISFC.Models.Fee.Item.Undrug>();

                FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

                ArrayList al = conMgr.GetConstantList("REGLEVEL_DIAGFEE");
                if (al != null && al.Count > 0)
                {
                    foreach (FS.HISFC.Models.Base.Const con in al)
                    {
                        if (!string.IsNullOrEmpty(con.Name))
                        {
                            FS.HISFC.Models.Fee.Item.Undrug itemObj = feeMgr.GetItem(con.Name);
                            if (itemObj != null && !string.IsNullOrEmpty(itemObj.ID) && itemObj.IsValid)
                            {
                                if (!this.dictDiagItem.ContainsKey(itemObj.ID))
                                {
                                    this.dictDiagItem.Add(itemObj.ID, itemObj);
                                }
                            }
                        }
                    }
                }

                #endregion
            }

            decimal pubCost = 0;
            decimal ecoCost = 0;
            if (r.UsualObject != null && r.UsualObject is List<FS.HISFC.Models.Account.AccountCardFee>)
            {
                List<FS.HISFC.Models.Account.AccountCardFee> lstAccFee = r.UsualObject as List<FS.HISFC.Models.Account.AccountCardFee>;
                foreach (FS.HISFC.Models.Account.AccountCardFee cardFee in lstAccFee)
                {
                    if ((cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost) > 0 &&
                         !string.IsNullOrEmpty(cardFee.ItemCode))
                    {
                        if (this.dictDiagItem.ContainsKey(cardFee.ItemCode))
                        {
                            cardFee.Tot_cost = cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost;
                            if (cardFee.Tot_cost < 7)
                            {
                                continue;
                            }

                            cardFee.Pay_cost = 0;
                            cardFee.Pub_cost = 9;   //报销金额统一为 7 
                            cardFee.Own_cost = cardFee.Tot_cost - cardFee.Pay_cost - cardFee.Pub_cost;

                            #region 计算减免金额

                            ArrayList alFeeDetail = new ArrayList();
                            FeeItemList f = new FeeItemList();
                            f.Item.ID = cardFee.ItemCode;
                            f.FT.TotCost = cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost;
                            f.FT.OwnCost = cardFee.Own_cost;
                            f.FT.PubCost = cardFee.Pub_cost;
                            f.FT.PayCost = cardFee.Pay_cost;
                            f.FT.RebateCost = cardFee.Eco_cost;
                            alFeeDetail.Add(f);

                            this.objPublicLocalSI.ComputeFeeCostByUpLoad(r.Pact.ID, ref alFeeDetail);
                            f = alFeeDetail[0] as FeeItemList;

                            f.FT.RebateCost = Math.Round(f.FT.RebateCost, 2);
                            cardFee.Eco_cost = f.FT.RebateCost;

                            #endregion

                            pubCost += cardFee.Pub_cost;
                            ecoCost += cardFee.Eco_cost;
                        }
                    }
                }

            }

            decimal totCost = r.OwnCost + r.PubCost + r.PayCost;
            r.PubCost = pubCost;
            r.PayCost = 0;
            r.OwnCost = totCost - r.PubCost - r.PayCost;
            r.EcoCost = ecoCost;

            #endregion

            r.SIMainInfo.OwnCost = r.OwnCost;  //自费金额 
            r.SIMainInfo.PubCost = r.PubCost;  //统筹金额 
            r.SIMainInfo.PayCost = r.PayCost;  //帐户金额 

            return 1;
        }

        public int CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            this.DB2Connect(); //2010-11-01
            return 1;
        }

        #endregion

        #region 明细上传

        public int UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            ClearErrMsg();

            if (this.IsUpdateLoad)
            {
                return 1;
            }
            log.WriteLog("特定医保明细上传开始" + r.ID + r.Name);
            string temp = null;
            if (this.isUploadInvoiceNo)
            {
                temp = r.SIMainInfo.TransNo;//保存该字段原先的值
                r.SIMainInfo.TransNo = "1";
            }

            #region 校验

            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            //if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            //{
            //    this.errMsg = "没有找到患者的就医登记号";
            //    return -1;
            //}

            if (feeDetails == null)
            {
                this.errMsg = "明细为空!";
                return -1;
            }
            if (feeDetails.Count == 0)
            {
                this.errMsg = "明细数量为 0";
                return -1;
            }

            #endregion

            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置医保数据库连接!";
                //出错情况下，外面需要事物的处理，处理完事物后再释放连接
                //Disconnect();
                return -1;
            }

            #endregion

            #region 项目对照及明细上传处理

            //1、存在本地一个项目对照医保中心两个项目的情况，及依照病种进行判断
            //2、医保要求相同的项目汇总后进行上传
            //3、医院本地是否允许某些项目不进行上传依照自费项目进行结算？在医保前置机限制
            //4、医保端：门诊号+医院号+项目编号为主键，限制一次门诊流水号只能一次收取一次相同的项目，
            //如果再补收，则必须在召回，重新上传重新结算
            //5、没有对照上的项目如何处理？【待处理】
            ArrayList tempFeeDetails = new ArrayList();
            tempFeeDetails = new ArrayList();
            //存放项目汇总信息
            Hashtable hsUpLoadFeeDetails = new Hashtable();

            ArrayList feeDetailsClone = new ArrayList();

            //foreach (FS.HISFC.Models.Fee.FeeItemBase f in feeDetails)
            foreach (FS.HISFC.Models.Fee.FeeItemBase f in feeDetails)
            {
                if (string.IsNullOrEmpty(f.Item.UserCode) || f.Item.UserCode == "0" || f.Item.SpecialPrice <= 0)
                {
                    this.DealFeeItemList(f);
                }

                if (string.IsNullOrEmpty(f.Item.UserCode) || f.Item.UserCode == "0") //2010-9-1
                {
                    MessageBox.Show("项目" + f.Item.Name + "没有维护医保对照码，请先进行维护！");
                    return -1;
                }

                FS.HISFC.Models.Fee.FeeItemBase tmpf = f.Clone();

                // {405B92DC-4786-4c78-9476-8841F28FF5FE}
                // {2742B814-EFFB-42b4-83DE-E3E92F648B71}
                FS.HISFC.Models.Base.Const obj = siLocalBusiness.QuerySiCompare(tmpf.Item.UserCode);
                if (obj != null && !string.IsNullOrEmpty(obj.UserCode))
                {
                    // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                    string ownFeeInputCode = siLocalBusiness.QueryFeeOwnOrPut(tmpf.Order.ID, tmpf.Item.ID, "1");
                    if (!string.IsNullOrEmpty(ownFeeInputCode))
                    {
                        tmpf.Item.UserCode = ownFeeInputCode;
                    }
                    //tmpf.Item.UserCode = obj.UserCode;//转换成社保编码，根据维护，是否使用自费编码
                    //一次循环处理完上传明细相关
                    //相同的项目进行累加汇总后再进行上传
                    if (hsUpLoadFeeDetails.ContainsKey(tmpf.Item.UserCode))
                    {
                        FS.HISFC.Models.Fee.FeeItemBase feeItemList = hsUpLoadFeeDetails[tmpf.Item.UserCode] as
                            FS.HISFC.Models.Fee.FeeItemBase;

                        feeItemList.Item.Qty += tmpf.Item.Qty;//数量累加
                        feeItemList.FT.TotCost += tmpf.FT.TotCost;//金额累加
                    }
                    else
                    {
                        //hsUpLoadFeeDetails.Add(f.Item.UserCode, f);
                        hsUpLoadFeeDetails.Add(tmpf.Item.UserCode, tmpf);
                        tempFeeDetails.Add(tmpf);
                    }
                }
            }


            #region 挂号诊查费处理

            /*
            * {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
            * */

            //清空
            this.accFeeList = new List<FS.HISFC.Models.Account.AccountCardFee>();

            List<FS.HISFC.Models.Account.AccountCardFee> lstAccFee = new List<FS.HISFC.Models.Account.AccountCardFee>();
            //爱博恩不处理挂号费
            //if (this.accMgr.QueryAccCardFeeByClinic(r.PID.CardNO, r.ID, out lstAccFee) == -1)
            //{
            //    this.errMsg = "查找患者的诊查费用失败!" + this.accMgr.Err;
            //    return -1;
            //}
            foreach (FS.HISFC.Models.Account.AccountCardFee cardFee in lstAccFee)
            {
                if (cardFee.Pub_cost > 0 && !string.IsNullOrEmpty(cardFee.ItemCode) && cardFee.SiFlag == "0")
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList fItem = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
                    fItem.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                    fItem.Item.ID = cardFee.ItemCode;
                    fItem.Item.Name = cardFee.ItemName;
                    fItem.Item.Qty = cardFee.ItemQty;
                    fItem.Item.PriceUnit = cardFee.ItemUnit;
                    fItem.Item.Price = cardFee.ItemPrice;
                    fItem.Item.PackQty = 1;
                    fItem.Days = 1;
                    fItem.FT.TotCost = cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost;

                    #region 自定义码处理

                    this.DealFeeItemList(fItem);

                    if (string.IsNullOrEmpty(fItem.Item.UserCode) || fItem.Item.UserCode == "0")
                    {
                        string strTemp = "项目【" + fItem.Item.Name + "】没有维护医保对照码(自定义码为空)，请先进行维护！";
                        if (r.User03 == "自动上传")
                        {
                            this.errMsg = strTemp;
                            return -1;
                        }
                        else
                        {
                            MessageBox.Show(strTemp);
                            return -1;
                        }
                    }
                    // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                    string ownFeeInputCode = siLocalBusiness.QueryFeeOwnOrPut(fItem.Order.ID, fItem.Item.ID, "1");
                    if (!string.IsNullOrEmpty(ownFeeInputCode))
                    {
                        fItem.Item.UserCode = ownFeeInputCode;
                    }
                    //一次循环处理完上传明细相关；相同的项目进行累加汇总后再进行上传
                    if (hsUpLoadFeeDetails.ContainsKey(fItem.Item.UserCode))
                    {
                        FS.HISFC.Models.Fee.FeeItemBase feeItemList = hsUpLoadFeeDetails[fItem.Item.UserCode] as FS.HISFC.Models.Fee.FeeItemBase;

                        feeItemList.Item.Qty += fItem.Item.Qty;      //数量累加
                        feeItemList.FT.TotCost += fItem.FT.TotCost;  //金额累加
                    }
                    else
                    {
                        hsUpLoadFeeDetails.Add(fItem.Item.UserCode, fItem);
                        tempFeeDetails.Add(fItem);
                    }

                    #endregion

                    this.accFeeList.Add(cardFee);

                }
            }


            #endregion

            #endregion

            log.WriteLog("特定医保明细上传项目对照及明细上传处理");

            #region 明细上传

            DateTime operTime = itemManager.GetDateTimeFromSysDateTime();

            //上传到医保前置机
            int iReturn = siBizProcess.InsertOutPatientFeeItemDetail(r, tempFeeDetails, operTime);

            if (iReturn < 0)
            {
                this.errMsg = "上传明细失败!" + siBizProcess.Err;
                //出错情况下，外面需要事物的处理，处理完事物后再释放连接
                //Disconnect();
                return -1;
            }
            if (iReturn == 0)
            {
                this.errMsg = "没有可上传的费用,请将合同单位设为自费或者其他";
                //出错情况下，外面需要事物的处理，处理完事物后再释放连接
                //Disconnect();
                return -1;
            }
            #endregion

            if (r.SIMainInfo.TransNo != null)
            {
                r.SIMainInfo.TransNo = temp;//还原原先的值
            }


            #region 断开连接
            //多事物不能断开连接 否则会数据丢失
            //Disconnect();
            //siBizProcess.Commit();

            #endregion

            log.WriteLog("特定医保明细上传结束" + r.ID + r.Name);
            return 1;
        }

        public int ModifyUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            this.errMsg = "不允许修改上传明细";
            return 1;
        }

        public int ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            this.errMsg = "不允许修改上传明细";
            return 1;
        }

        public int DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            ClearErrMsg();
            #region 校验

            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            //if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            //{
            //    this.errMsg = "没有找到患者的就医登记号";
            //    return -1;
            //}
            if (f == null)
            {
                this.errMsg = "明细为空!";
                return -1;
            }

            #endregion

            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }

            #endregion

            #region 删除不掉，另起一个事物--特殊情况下再使用

            try
            {
                siBizProcess.BeginTranscation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }


            #endregion

            #region 删除上传明细
            string errText = string.Empty;
            if (IsOutPatientBalance(r, ref errText))
            {
                //已经结算
                MessageBox.Show("门诊号:" + r.ID + "患者在医保终端已经结算，请先取消结算，才能删除明细重新结算！");
                //siBizProcess.RollBack();
                return -1;
            }
            else if (!string.IsNullOrEmpty(errText))
            {
                MessageBox.Show(errText);
                //siBizProcess.RollBack();
                return -1;
            }


            //生成就医登记号
            MakeUpLoadRegNo(r);

            int iReturn = siBizProcess.DeleteOutPaientFeeItemDetail(r.SIMainInfo.RegNo);
            if (iReturn == -1)
            {
                //siBizProcess.RollBack();
                return -1;
            }
            //siBizProcess.Commit();

            //Disconnect();

            this.IsUpdateLoad = false;

            return 1;

            #endregion
        }

        public int DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            ClearErrMsg();
            #region 校验

            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            //if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            //{
            //    this.errMsg = "没有找到患者的就医登记号";
            //    return -1;
            //}

            #endregion

            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }

            #endregion


            #region 删除不掉，另起一个事物

            try
            {
                siBizProcess.BeginTranscation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }


            #endregion

            #region 删除上传明细

            string errText = string.Empty;
            if (IsOutPatientBalance(r, ref errText))
            {
                //已经结算
                MessageBox.Show("门诊号:" + r.ID + "患者在医保终端已经结算，请先取消结算，才能删除明细重新结算！");
                //出错情况下，外面需要事物的处理，处理完事物后再释放连接
                //Disconnect();
                //siBizProcess.RollBack();
                return -1;
            }
            else if (!string.IsNullOrEmpty(errText))
            {
                //出错情况下，外面需要事物的处理，处理完事物后再释放连接
                //Disconnect();
                MessageBox.Show(errText);
                //siBizProcess.RollBack();
                return -1;
            }


            //生成就医登记号
            MakeUpLoadRegNo(r);

            int iReturn = siBizProcess.DeleteOutPaientFeeItemDetail(r.SIMainInfo.RegNo);
            if (iReturn == -1)
            {
                //siBizProcess.RollBack();
                //return -1;
            }
            //siBizProcess.Commit();
            //执行成功时暂释放连接
            //Disconnect();

            this.IsUpdateLoad = false;

            return 1;

            #endregion
        }

        public int DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            ClearErrMsg();
            #region 校验

            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            //if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            //{
            //    this.errMsg = "没有找到患者的就医登记号";
            //    return -1;
            //}
            if (feeDetails == null)
            {
                this.errMsg = "明细为空!";
                return -1;
            }
            if (feeDetails.Count == 0)
            {
                this.errMsg = "明细数量为 0";
                return -1;
            }

            #endregion

            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }

            #endregion

            #region 删除不掉，另起一个事物

            try
            {
                siBizProcess.BeginTranscation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }


            #endregion

            #region 删除上传明细

            string errText = string.Empty;
            if (IsOutPatientBalance(r, ref errText))
            {
                //已经结算
                MessageBox.Show("门诊号:" + r.ID + "患者在医保终端已经结算，请先取消结算，才能删除明细重新结算！");
                //出错情况下，外面需要事物的处理，处理完事物后再释放连接
                //Disconnect();
                //siBizProcess.RollBack();
                return -1;
            }
            else if (!string.IsNullOrEmpty(errText))
            {
                //出错情况下，外面需要事物的处理，处理完事物后再释放连接
                //Disconnect();
                MessageBox.Show(errText);
                //siBizProcess.RollBack();
                return -1;
            }


            //生成就医登记号
            MakeUpLoadRegNo(r);

            int iReturn = siBizProcess.DeleteOutPaientFeeItemDetail(r.SIMainInfo.RegNo);
            if (iReturn == -1)
            {
                //siBizProcess.RollBack();
                return -1;
            }

            //siBizProcess.Commit();
            //Disconnect();

            this.IsUpdateLoad = false;

            return 1;

            #endregion
        }

        public bool IsUploadAllFeeDetailsOutpatient
        {
            get { return true; }
        }

        /// <summary>
        /// 判断门诊医保患者是否已经结算
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool IsOutPatientBalance(FS.HISFC.Models.Registration.Register r, ref string errText)
        {
            siPatientBalanceCount = 0;
            // siPatientBalanceCount = siBizProcess.GetOutPatientBalanceCount(r.ID);
            //if (siPatientBalanceCount < 0)
            //{
            //    errText = "获取医保结算次数失败！" + siBizProcess.Err;
            //    Disconnect();
            //    return false;
            //}
            //if (siPatientBalanceCount < 0)
            //{
            //    errText = "获取医保本地结算次数失败！" + siLocalBusiness.Err;
            //    Disconnect();
            //    return false;
            //}
            //else if (siPatientBalanceCount > 0)//说明已经结算过，先取消结算
            //{
            //    return true;
            //}
            siPatientBalanceCount = FS.FrameWork.Function.NConvert.ToInt32(siLocalBusiness.GetBalNo(r.ID));
            if (siPatientBalanceCount < 0)
            {
                errText = "获取医保本地结算次数失败！" + siLocalBusiness.Err;
                // Disconnect();
            }
            return false;
        }

        /// <summary>
        /// 填充就医登记信息
        /// </summary>
        /// <param name="r"></param>
        public void MakeUpLoadRegNo(FS.HISFC.Models.Registration.Register r)
        {
            //避免和就系统重复，此处增加字符显示
            r.SIMainInfo.RegNo = "R" + r.ID + (siPatientBalanceCount + 1).ToString();
        }

        #endregion

        #region 结算

        public int PreBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {

            #region 校验

            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            //if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            //{
            //    this.errMsg = "没有找到患者的就医登记号";
            //    return -1;
            //}

            #endregion

            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }

            #endregion

            //此处先提交前置机数据
            siBizProcess.Commit();
            this.IsUpdateLoad = true;

            log.WriteLog("特定医保预结算开始" + r.ID + r.Name);
            #region 结果返回

            OutPatient.frmOutPatientBalanceHead frmPopBalance = new FoShanSI.OutPatient.frmOutPatientBalanceHead();
            OutPatient.ucOutPatientBalanceHead ucBalance = new FoShanSI.OutPatient.ucOutPatientBalanceHead();
            frmPopBalance.Controls.Add(ucBalance);
            frmPopBalance.ucBalance = ucBalance;
            ucBalance.Dock = DockStyle.Fill;
            ucBalance.Register = r;
            ucBalance.AccFeeList = this.accFeeList;  // {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造

            ucBalance.SiDealBusiness = siBizProcess;
            //将上传的号码复制到操作系统粘贴板
            FoShanSI.Function.Function.Copy(r.SIMainInfo.RegNo);
            frmPopBalance.StartPosition = FormStartPosition.CenterScreen;
            frmPopBalance.ShowDialog();
            log.WriteLog("特定医保预结算完成");
            if (ucBalance.IsSuccessBalance == false)
            {
                Disconnect();
                this.errMsg = "非正常途径取消结算";
                return -1;
            }
            else
            {
                r = ucBalance.Register;
                Disconnect();

                // {484CCBB6-81F6-4268-A4BF-D66033A6B187}
                // 正常预结算，处理减免信息
                // 计算减免金额

                #region 计算统筹金额和优惠金额

                //{6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造，特定重新计算诊查费
                if (this.dictDiagItem == null)
                {
                    #region 初始化

                    this.dictDiagItem = new Dictionary<string, FS.HISFC.Models.Fee.Item.Undrug>();

                    FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

                    ArrayList al = conMgr.GetConstantList("REGLEVEL_DIAGFEE");
                    if (al != null && al.Count > 0)
                    {
                        foreach (FS.HISFC.Models.Base.Const con in al)
                        {
                            if (!string.IsNullOrEmpty(con.Name))
                            {
                                FS.HISFC.Models.Fee.Item.Undrug itemObj = feeMgr.GetItem(con.Name);
                                if (itemObj != null && !string.IsNullOrEmpty(itemObj.ID) && itemObj.IsValid)
                                {
                                    if (!this.dictDiagItem.ContainsKey(itemObj.ID))
                                    {
                                        this.dictDiagItem.Add(itemObj.ID, itemObj);
                                    }
                                }
                            }
                        }
                    }

                    #endregion
                }
                foreach (FS.HISFC.Models.Fee.FeeItemBase f in feeDetails)
                {
                    if (!string.IsNullOrEmpty(f.Item.ID))
                    {
                        if (this.dictDiagItem.ContainsKey(f.Item.ID))
                        {
                            f.FT.TotCost = f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                            if (FS.FrameWork.Management.Connection.Hospital.Name == "佛山市第三人民医院")
                            {
                                if (f.FT.TotCost < 9)
                                {
                                    continue;
                                }
                                f.FT.PayCost = 0;
                                f.FT.RebateCost = 0;
                                f.FT.PubCost = 9;   //佛山市第三人民医院报销金额为 9
                                f.FT.OwnCost = f.FT.TotCost - f.FT.PayCost - f.FT.PubCost;

                            }
                            else
                            {
                                if (f.FT.TotCost < 7)
                                {
                                    continue;
                                }
                                f.FT.PayCost = 0;
                                f.FT.RebateCost = 0;
                                f.FT.PubCost = 7;   //报销金额为 7
                                f.FT.OwnCost = f.FT.TotCost - f.FT.PayCost - f.FT.PubCost;
                            }

                        }
                    }
                }


                #endregion
                objPublicLocalSI.ComputeFeeCostByUpLoad(r.Pact.ID, ref feeDetails);


                return 1;
            }

            #endregion
            log.WriteLog("特定医保预结算结束" + r.ID + r.Name);
        }

        public int BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {

            log.WriteLog("特定医保结算开始" + r.ID + r.Name);
            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }
            else
            {
                try
                {
                    siBizProcess.BeginTranscation();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }
            }

            #endregion

            //结算时将结算信息插入到中间表
            if (siLocalBusiness != null)
            {
                siLocalBusiness.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }

            // {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
            decimal regTotCost = 0m;
            decimal regPubCost = 0m;
            #region 挂号诊查费除外

            //爱博恩不处理挂号费
            //if (this.accFeeList != null && this.accFeeList.Count > 0)
            //{
            //    foreach (FS.HISFC.Models.Account.AccountCardFee cardFee in this.accFeeList)
            //    {
            //        regTotCost += (cardFee.Own_cost + cardFee.Pub_cost + cardFee.Pay_cost);
            //        regPubCost += (cardFee.Pub_cost);
            //    }
            //}

            #endregion
            //先加上
            r.SIMainInfo.TotCost += regTotCost;
            r.SIMainInfo.PubCost += regPubCost;
            r.SIMainInfo.OwnCost = r.SIMainInfo.TotCost - r.SIMainInfo.PubCost;

            int iReturn = siLocalBusiness.InsertSIMainInfo(r);
            // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
            if (iReturn > 0)
            {
                iReturn = siLocalBusiness.DeletePatientUploadFeeDetail(r.ID, r.SIMainInfo.RegNo);
                if (feeDetails != null && feeDetails.Count > 0)
                {
                    foreach (FS.HISFC.Models.Fee.FeeItemBase fTemp in feeDetails)
                    {
                        FS.HISFC.Models.Fee.FeeItemBase f = fTemp.Clone();
                        // {405B92DC-4786-4c78-9476-8841F28FF5FE}
                        FS.HISFC.Models.Base.Const obj = siLocalBusiness.QuerySiCompare(f.Item.UserCode);
                        if (obj != null && !string.IsNullOrEmpty(obj.UserCode))
                        {
                            // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                            string ownFeeInputCode = siLocalBusiness.QueryFeeOwnOrPut(f.Order.ID,f.Item.ID,"1");
                            if (!string.IsNullOrEmpty(ownFeeInputCode))
                            {
                                f.Item.UserCode = ownFeeInputCode;
                            }
                            if (f.Item.SpecialPrice <= 0)
                            {
                                this.DealFeeItemList(f);
                            }
                            iReturn = siLocalBusiness.InsertIOutPatientUploadFeeDetail(r, f);
                        }
                    }
                }

            }
            // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
            if (iReturn > 0)
            {
                iReturn = siLocalBusiness.UpdateBlanceSIPatient(r.ID, r.SIMainInfo.RegNo);
                iReturn = siLocalBusiness.SaveBlanceSIOutPatient(r);
            }

            //再减掉 {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
            r.SIMainInfo.TotCost -= regTotCost;
            r.SIMainInfo.PubCost -= regPubCost;
            r.SIMainInfo.OwnCost = r.SIMainInfo.TotCost - r.SIMainInfo.PubCost;

            if (iReturn <= 0)
            {
                this.errCode = siLocalBusiness.ErrCode;
                this.errMsg = siLocalBusiness.Err;
                Disconnect();
                return iReturn;
            }

            #region 挂号诊查费更新

            // {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
            //爱博恩不处理挂号费
            //if (this.accFeeList != null && this.accFeeList.Count > 0)
            //{
            //    foreach (FS.HISFC.Models.Account.AccountCardFee cardFee in this.accFeeList)
            //    {
            //        cardFee.SiFlag = "1";
            //        cardFee.SiBalanceNO = r.SIMainInfo.InvoiceNo;

            //        iReturn = this.accMgr.UpdateAccountCardFeeSiState(cardFee);

            //        if (iReturn <= 0)
            //        {
            //            this.errCode = this.accMgr.ErrCode;
            //            this.errMsg = "更新垫付的挂号诊查费用失败!\r\n" + this.accMgr.Err;
            //            return iReturn;
            //        }
            //    }
            //}


            #endregion

            //Disconnect();
            log.WriteLog("特定医保预结算结算" + r.ID + r.Name);
            return 1;
        }

        public int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            #region 挂号诊查费用状态

            // {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造

            List<FS.HISFC.Models.Account.AccountCardFee> lstAccFee = new List<FS.HISFC.Models.Account.AccountCardFee>();
            if (this.accMgr.QueryAccCardFeeByClinic(r.PID.CardNO, r.ID, out lstAccFee) == -1)
            {
                this.errMsg = "查找医院垫付的诊查费失败!\r\n注销医院垫付的诊查费失败!" + this.accMgr.Err;
                return -1;
            }
            foreach (FS.HISFC.Models.Account.AccountCardFee cardFee in lstAccFee)
            {
                if (cardFee.Pub_cost > 0 && !string.IsNullOrEmpty(cardFee.ItemCode) && cardFee.SiFlag == "1" &&
                    cardFee.SiBalanceNO == r.SIMainInfo.InvoiceNo)
                {
                    cardFee.SiFlag = "0";
                    cardFee.SiBalanceNO = string.Empty;

                    if (this.accMgr.UpdateAccountCardFeeSiState(cardFee) <= 0)
                    {
                        this.errCode = this.accMgr.ErrCode;
                        this.errMsg = "注销医院垫付的诊查费失败!\r\n" + this.accMgr.Err;
                        return -1;
                    }
                }
            }

            int res = 1;
            if (r != null)
            {
                res = this.siLocalBusiness.UpdateSiMainInfoValidType(r.ID, "0", "1");
            }
            // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
            r.SIMainInfo.RegNo = this.siLocalBusiness.GetRegNo(r.ID, r.SIMainInfo.InvoiceNo);//{405B92DC-4786-4c78-9476-8841F28FF5FE}
            if (res > 0)
            {
                res = this.siLocalBusiness.DeletePatientUploadFeeDetail(r.ID, r.SIMainInfo.RegNo);
                res = this.siLocalBusiness.UpdateBlanceSIPatient(r.ID, r.SIMainInfo.RegNo);
            }
            #endregion

            //this.errMsg = "佛山医保HIS端暂时不支持取消结算!";
            return res;
        }

        public int CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            this.errMsg = "佛山医保暂时不支持取消结算!";
            return 1;
        }

        #endregion

        #region 黑名单

        public bool IsInBlackList(FS.HISFC.Models.Registration.Register r)
        {
            if (r.User03 == "自动上传")
            {
                return true;
            }

            #region 门特门慢、生育医保

            //{6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造

            this.MedcareInterfaceProxy.SetPactCode("2");   //佛山三院，2 居民医保；

            this.MedcareInterfaceProxy.SetTrans(this.trans);
            this.MedcareInterfaceProxy.BeginTranscation();

            //连接待遇接口
            long returnValue = this.MedcareInterfaceProxy.Connect();
            if (returnValue == -1)
            {
                //医保回滚可能出错，此处提示
                if (this.MedcareInterfaceProxy.Rollback() == -1)
                {
                    r.User03 = this.MedcareInterfaceProxy.ErrMsg;
                    return false;
                }

                r.User03 = "医疗待遇接口连接失败!" + this.MedcareInterfaceProxy.ErrMsg;
                return false;
            }

            //黑名单判断，南庄用于判断当日报销次数
            if (this.MedcareInterfaceProxy.IsInBlackList(r))
            {
                // 医保回滚可能出错，此处提示
                if (this.MedcareInterfaceProxy.Rollback() == -1)
                {
                    r.User03 = this.MedcareInterfaceProxy.ErrMsg;
                    return false;
                }

                this.MedcareInterfaceProxy.Disconnect();
                r.User03 = this.MedcareInterfaceProxy.ErrMsg;
                return false;
            }

            #endregion

            return false;
        }

        #endregion

        #endregion

        #region 住院

        #region 登记

        public int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int UploadRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //int iReturn = siLocalBusiness.UpdateSiMainInfo(patient);
            //if (iReturn == -1)
            //{
            //    this.errMsg = "插入医保登记出错!" + siLocalBusiness.Err;
            //    return -1;
            //}
            //if (iReturn == 0)
            //{
            //    iReturn = siLocalBusiness.InsertSIMainInfo(patient);
            //    if (iReturn <= 0)
            //    {
            //        this.errMsg = "插入医保登记出错!" + siLocalBusiness.Err;
            //        return -1;
            //    }
            //}
            return 1;
        }

        public int LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int RecallRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }
        #endregion

        #region 明细上传

        public int UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            this.errMsg = "收费时不上传数据，在结算时整体上传";

            // {1A3FD005-392E-418e-998B-BD98B5E4CB49}
            // 佛山特定医保不支持单条费用上传，必须在结算时整体上传
            return 1;

            #region  校验
            if (patient == null || patient.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            if (f == null)
            {
                this.errMsg = "明细为空!";
                return -1;
            }
            #endregion

            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置医保数据库连接!";
                Disconnect();
                return -1;
            }

            #endregion

            #region 对照处理

            this.DealFeeItemList(f);

            #endregion

            #region 明细上传


            DateTime operTime = f.ChargeOper.OperTime;// {0F456794-6735-43f7-B220-92FC4CE638D2}
            if (f.ChargeOper.OperTime == DateTime.MaxValue || f.ChargeOper.OperTime == DateTime.MinValue)
            {
                operTime = itemManager.GetDateTimeFromSysDateTime();
            }
            //FS.HISFC.Models.Base.Const obj = siLocalBusiness.QuerySiCompare(f.Item.UserCode);
            //if (obj != null && !string.IsNullOrEmpty(obj.UserCode))
            //{
            //    f.Item.UserCode = obj.UserCode;
            //}
            // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
            string ownFeeInputCode = siLocalBusiness.QueryFeeOwnOrPut(f.Order.ID, f.Item.ID, "2");
            if (!string.IsNullOrEmpty(ownFeeInputCode))
            {
                f.Item.UserCode = ownFeeInputCode;
            }
            //上传到医保前置机
            int iReturn = siBizProcess.InsertInPatientFeeItemDetail(patient, f, operTime);

            if (iReturn < 0)
            {
                this.errMsg = "上传明细失败!" + siBizProcess.Err;
                Disconnect();
                return -1;
            }
            if (iReturn == 0)
            {
                this.errMsg = "没有可上传的费用,请将合同单位设为自费或者其他";
                Disconnect();
                return -1;
            }
            #endregion

            #region 断开连接

            Disconnect();
            return 1;

            #endregion

        }

        /// <summary>
        /// 住院费用上传
        /// 
        /// {1A3FD005-392E-418e-998B-BD98B5E4CB49}
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            this.errMsg = "收费时不上传数据，在结算时整体上传";

            // {1A3FD005-392E-418e-998B-BD98B5E4CB49}
            // 佛山特定医保不支持单条费用上传，必须在结算时整体上传
            return 1;

            #region  校验
            if (patient == null || patient.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            if (f == null)
            {
                this.errMsg = "明细为空!";
                return -1;
            }
            #endregion

            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置医保数据库连接!";
                Disconnect();
                return -1;
            }

            #endregion

            #region 对照处理

            this.DealFeeItemList(f);

            #endregion

            #region 明细上传

            DateTime operTime = f.ChargeOper.OperTime;// {0F456794-6735-43f7-B220-92FC4CE638D2}
            if (f.ChargeOper.OperTime == DateTime.MaxValue || f.ChargeOper.OperTime == DateTime.MinValue)
            {
                operTime = itemManager.GetDateTimeFromSysDateTime();
            }
            //FS.HISFC.Models.Base.Const obj = siLocalBusiness.QuerySiCompare(f.Item.UserCode);
            //if (obj != null && !string.IsNullOrEmpty(obj.UserCode))
            //{
            //    f.Item.UserCode = obj.UserCode;
            //}
            string ownFeeInputCode = siLocalBusiness.QuerySiCompareByInOwnFee(f.Item.UserCode);
            if (!string.IsNullOrEmpty(ownFeeInputCode))
            {
                f.Item.UserCode = ownFeeInputCode;
            }
            //上传到医保前置机
            int iReturn = siBizProcess.InsertInPatientFeeItemDetail(patient, f, operTime);

            if (iReturn < 0)
            {
                this.errMsg = "上传明细失败!" + siBizProcess.Err;
                Disconnect();
                return -1;
            }
            if (iReturn == 0)
            {
                this.errMsg = "没有可上传的费用,请将合同单位设为自费或者其他";
                Disconnect();
                return -1;
            }
            #endregion

            #region 断开连接

            Disconnect();
            return 1;

            #endregion
        }
        /// <summary>
        /// 住院费用明细上传
        /// 
        /// {1A3FD005-392E-418e-998B-BD98B5E4CB49}
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {

            #region  校验
            if (patient == null || patient.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            if (feeDetails == null)
            {
                this.errMsg = "明细为空!";
                return -1;
            }
            if (feeDetails.Count == 0)
            {
                this.errMsg = "明细数量为 0";
                return -1;
            }
            #endregion

            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置医保数据库连接!";
                Disconnect();
                return -1;
            }

            #endregion

            #region 对照与汇总处理 -- {1A3FD005-392E-418e-998B-BD98B5E4CB49}

            //1、存在本地一个项目对照医保中心两个项目的情况，及依照病种进行判断
            //2、医保要求相同的项目汇总后进行上传
            //3、医院本地是否允许某些项目不进行上传依照自费项目进行结算？在医保前置机限制
            //4、医保端：医院号 + 住院号 + 住院次数 + 项目编号为主键，限制一次住院只能一次收取一个相同的项目，
            //   如果再补收，则必须在召回，重新上传重新结算
            ArrayList tempFeeDetails = new ArrayList();
            //存放项目汇总信息
            Hashtable hsUpLoadFeeDetails = new Hashtable();

            ArrayList feeDetailsClone = new ArrayList();

            foreach (FS.HISFC.Models.Fee.FeeItemBase f in feeDetails)
            {
                feeDetailsClone.Add(f.Clone());
            }

            //foreach (FS.HISFC.Models.Fee.FeeItemBase f in feeDetails)
            foreach (FS.HISFC.Models.Fee.FeeItemBase f in feeDetailsClone)
            {
                if (string.IsNullOrEmpty(f.Item.UserCode) || f.Item.UserCode == "0" || f.Item.SpecialPrice <= 0)
                {
                    this.DealFeeItemList(f);
                }

                if (string.IsNullOrEmpty(f.Item.UserCode) || f.Item.UserCode == "0") //2010-9-1
                {
                    this.errMsg = "项目" + f.Item.Name + "没有维护医保对照码，请先进行维护！";
                    return -1;
                }
                
                //FS.HISFC.Models.Base.Const obj = siLocalBusiness.QuerySiCompare(f.Item.UserCode);
                //if (obj != null && !string.IsNullOrEmpty(obj.UserCode))
                //{
                //    f.Item.UserCode = obj.UserCode;
                //}
                //一次循环处理完上传明细相关
                //相同的项目进行累加汇总后再进行上传
                if (hsUpLoadFeeDetails.ContainsKey(f.Item.UserCode))
                {
                    FS.HISFC.Models.Fee.FeeItemBase feeItemList = hsUpLoadFeeDetails[f.Item.UserCode] as
                        FS.HISFC.Models.Fee.FeeItemBase;

                    feeItemList.Item.Qty += f.Item.Qty;//数量累加
                    feeItemList.FT.TotCost += f.FT.TotCost;//金额累加
                }
                else
                {
                    hsUpLoadFeeDetails.Add(f.Item.UserCode, f);
                    tempFeeDetails.Add(f);
                }
            }

            #endregion

            DateTime operTime = itemManager.GetDateTimeFromSysDateTime();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in tempFeeDetails)
            {
                #region 明细上传
                if (f.Item.Qty == 0)
                {
                    continue;
                }
                if (f.Item.SpecialPrice == 0)//特诊价格为0 先不做上传
                {
                    continue;
                }
                DateTime operTime1 = f.ChargeOper.OperTime;// {0F456794-6735-43f7-B220-92FC4CE638D2}
                if (f.ChargeOper.OperTime == DateTime.MaxValue || f.ChargeOper.OperTime == DateTime.MinValue)
                {
                    operTime1 = operTime;
                }
                string ownFeeInputCode = siLocalBusiness.QuerySiCompareByInOwnFee(f.Item.UserCode);
                if (!string.IsNullOrEmpty(ownFeeInputCode))
                {
                    f.Item.UserCode = ownFeeInputCode;
                }
                //上传到医保前置机
                int iReturn = siBizProcess.InsertInPatientFeeItemDetail(patient, f, operTime1);
                if (iReturn < 0)
                {
                    this.errMsg = "上传明细失败!" + siBizProcess.Err;
                    Disconnect();
                    return -1;
                }
                if (iReturn == 0)
                {
                    this.errMsg = "没有可上传的费用,请将合同单位设为自费或者其他";
                    Disconnect();
                    return -1;
                }
                #endregion
            }

            return 1;
        }

        public int ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            this.errMsg = "不允许修改上传明细";
            return 1;
        }

        public int ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            this.errMsg = "不允许修改上传明细";
            return 1;
        }

        public int DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            this.errMsg = "不允许修改上传明细";
            return 1;
        }

        /// <summary>
        /// 删除已上传费用明细
        /// 
        /// {1A3FD005-392E-418e-998B-BD98B5E4CB49}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            ClearErrMsg();

            #region 校验

            if (patient == null || patient.PID == null || string.IsNullOrEmpty(patient.PID.ID))
            {
                this.errMsg = "没找到串者信息！";
                return -1;
            }

            #endregion

            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }

            #endregion

            int iReturn = this.siLocalBusiness.IsInPatientBalance(patient.ID);
            if (iReturn == -1)
            {
                this.errMsg = "查询此病人结算状态失败！";
                return -1;
            }
            else if (iReturn > 0)
            {
                this.errMsg = "此患者在医保终端已经结算，请先取消结算，才能删除明细重新结算！";
                return -1;
            }


            #region 另起一个事物

            try
            {
                siBizProcess.BeginTranscation();
            }
            catch (Exception ex)
            {
                this.errMsg = ex.Message;
                return -1;
            }


            #endregion

            #region 删除上传明细

            iReturn = siBizProcess.DeleteInPaientFeeItemDetail(patient.PID.PatientNO, patient.InTimes);
            if (iReturn == -1)
            {
                //siBizProcess.RollBack();
                return -1;
            }

            this.IsUpdateLoad = false;

            #endregion
            return 1;
        }
        /// <summary>
        /// 删除已上传费用明细
        /// 
        /// {1A3FD005-392E-418e-998B-BD98B5E4CB49}
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return DeleteUploadedFeeDetailsAllInpatient(patient);
        }

        #endregion

        #region 费用结算
        /// <summary>
        /// 医保预结算
        /// 
        /// {1A3FD005-392E-418e-998B-BD98B5E4CB49}
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            this.ClearErrMsg();

            #region 校验

            if (patient == null || patient.PID == null || string.IsNullOrEmpty(patient.PID.ID))
            {
                this.errMsg = "没找到串者信息！";
                return -1;
            }

            #endregion

            #region 连接

            DB2Connect();

            if (siBizProcess == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }

            #endregion

            int iReturnValue = 0;
            #region 删除已上传费用信息

            iReturnValue = this.DeleteUploadedFeeDetailsAllInpatient(patient);
            if (iReturnValue == -1)
            {
                return -1;
            }
            #endregion

            #region 上传费用明细

            iReturnValue = this.UploadFeeDetailsInpatient(patient, ref feeDetails);
            if (iReturnValue == -1)
            {
                //siBizProcess.RollBack();
                return -1;
            }

            #endregion

            //此处先提交前置机数据
            siBizProcess.Commit();
            this.IsUpdateLoad = true;

            #region 结果返回

            InPatient.frmInPatientBalanceHead frmPopBalance = new FoShanSI.InPatient.frmInPatientBalanceHead();
            InPatient.ucInPatientBalanceHead ucBalance = new FoShanSI.InPatient.ucInPatientBalanceHead();
            frmPopBalance.Controls.Add(ucBalance);
            frmPopBalance.ucBalance = ucBalance;
            ucBalance.Dock = DockStyle.Fill;
            ucBalance.Patientinfo = patient;
            ucBalance.SiDealBusiness = siBizProcess;
            //将上传的号码复制到操作系统粘贴板
            FoShanSI.Function.Function.Copy(patient.SIMainInfo.RegNo);
            frmPopBalance.StartPosition = FormStartPosition.CenterScreen;
            frmPopBalance.ShowDialog();
            if (ucBalance.IsSuccessBalance == false)
            {
                Disconnect();
                this.errMsg = "非正常途径取消结算";
                return -1;
            }
            else
            {
                patient = ucBalance.Patientinfo;
                Disconnect();

                // {484CCBB6-81F6-4268-A4BF-D66033A6B187}
                // 正常预结算，处理减免信息
                // 计算减免金额
                // 住院减免？

                //objPublicLocalSI.ComputeFeeCostByUpLoad(patient.Pact.ID, ref feeDetails);

                return 1;
            }

            #endregion
            return 1;
        }
        /// <summary>
        /// 中途结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            // 不支持中途结算
            return -1;
        }

        /// <summary>
        /// 住院结算
        /// 
        /// {1A3FD005-392E-418e-998B-BD98B5E4CB49}
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            #region 连接

            DB2Connect();

            //if (siBizProcess == null)
            //{
            //    this.errMsg = "没有设置数据库连接!";
            //    return -1;
            //}
            //else
            //{
            //    try
            //    {
            //        siBizProcess.BeginTranscation();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        return -1;
            //    }
            //}

            #endregion

            //结算时将结算信息插入到中间表
            if (siLocalBusiness != null)
            {
                siLocalBusiness.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }
            // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
            patient.SIMainInfo.RegNo = siLocalBusiness.GetJSID();

            patient.SIMainInfo.IsValid = true;
            int iReturn = siLocalBusiness.UpdateSiMainInfo(patient);
            if (iReturn <= 0)
            {
                iReturn = siLocalBusiness.InsertSIMainInfo(patient);
            }
            // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
            if (iReturn > 0)
            {             
                if (feeDetails != null && feeDetails.Count >= 0)
                {
                    iReturn = siLocalBusiness.DeletePatientUploadFeeDetail(patient.ID, patient.SIMainInfo.RegNo);

                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList fTemp in feeDetails)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList f = fTemp.Clone();
                        // {EBC9E80A-CFAD-4e22-9AED-3C0628A788AE}
                        string ownFeeInputCode = siLocalBusiness.QueryFeeOwnOrPut(f.Order.ID, f.Item.ID, "2");
                        if (!string.IsNullOrEmpty(ownFeeInputCode))
                        {
                            f.Item.UserCode = ownFeeInputCode;
                        }
                        iReturn = siLocalBusiness.InsertInPatientUploadFeeDetail(patient,f);
                    }
                }
            }
            // {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
            if (iReturn > 0)
            {
                iReturn = siLocalBusiness.UpdateBlanceSIPatient(patient.ID, patient.SIMainInfo.RegNo);
                iReturn = siLocalBusiness.SaveBlanceSIInPatient(patient);

            }
            if (iReturn <= 0)
            {
                this.errCode = siLocalBusiness.ErrCode;
                this.errMsg = siLocalBusiness.Err;
                Disconnect();
                return iReturn;
            }

            return 1;
        }

        public int CancelBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            //this.DB2Connect();
            int res = 1;
            if (patient != null && !string.IsNullOrEmpty(patient.SIMainInfo.RegNo))// {72C3B6AE-A1FF-4239-A9AB-A07FE869636B} lfhm 2020-1-20
            {
                res = this.siLocalBusiness.UpdateSiMainInfoValidType(patient.ID, "0", "2");
                if (res > 0)
                {
                    res = this.siLocalBusiness.DeletePatientUploadFeeDetail(patient.ID, patient.SIMainInfo.RegNo);
                    res = this.siLocalBusiness.UpdateBlanceSIPatient(patient.ID, patient.SIMainInfo.RegNo);
                }

            }
            return res;
        }

        public int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            //this.DB2Connect();
           
            return 1;
        }
        #endregion

        #region 黑名单管理

        public bool IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return false;
        }

        public int QueryBlackLists(ref System.Collections.ArrayList blackLists)
        {
            return 1;
        }

        public int QueryDrugLists(ref System.Collections.ArrayList drugLists)
        {
            return 1;
        }

        public int QueryUndrugLists(ref System.Collections.ArrayList undrugLists)
        {
            return 1;
        }
        #endregion

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private int CheckTotalUploadEqualBalance(FS.HISFC.Models.RADT.PatientInfo balancePatient)
        {
            FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            feeInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int iReturn;
            decimal tempTotCost = 0;
            if (feeInpatient.GetUploadTotCost(balancePatient.ID, ref tempTotCost) == -1)
            {
                this.errMsg = "获取上传总费用出错！" + feeInpatient.Err;
                return -1;
            }

            //如果本地费用 与结算费用不符
            //则认为有多条结算信息，弹出输入医保流水号的对话框，如果选择取消，则退出。
            while (tempTotCost != balancePatient.SIMainInfo.TotCost)
            {
                FS.HISFC.Models.RADT.PatientInfo siPatient = new FS.HISFC.Models.RADT.PatientInfo();
                iReturn = siBizProcess.GetInPatientBalanceHead(siPatient);
                if (iReturn == 0)
                {
                    errMsg = "请先在医保客户端进行结算!";
                    return -1;
                }
                if (iReturn == -1)
                {
                    errMsg = "获得医保患者结算信息出错!" + siBizProcess.Err;
                    return -1;
                }
                balancePatient.SIMainInfo.TotCost += siPatient.SIMainInfo.TotCost;
                balancePatient.SIMainInfo.PubCost += siPatient.SIMainInfo.PubCost;
                balancePatient.SIMainInfo.PayCost += siPatient.SIMainInfo.PayCost;
                balancePatient.SIMainInfo.OwnCost += siPatient.SIMainInfo.OwnCost;
                balancePatient.SIMainInfo.ItemYLCost += siPatient.SIMainInfo.ItemYLCost;
                balancePatient.SIMainInfo.BaseCost += siPatient.SIMainInfo.BaseCost;
                balancePatient.SIMainInfo.ItemPayCost += siPatient.SIMainInfo.ItemPayCost;
                balancePatient.SIMainInfo.PubOwnCost += siPatient.SIMainInfo.PubOwnCost;
                balancePatient.SIMainInfo.OverTakeOwnCost += siPatient.SIMainInfo.OverTakeOwnCost;
                balancePatient.SIMainInfo.HosCost += siPatient.SIMainInfo.HosCost;
            }
            return 1;
        }

        ///// <summary>
        ///// 处理项目对照
        ///// </summary>
        ///// <param name="feeItemList"></param>
        ///// <param name="patientType"></param>
        ///// <returns></returns>
        //private void DealFeeItemList(FS.HISFC.Models.Fee.FeeItemBase f)
        //{
        //    itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
        //    itemPharmacyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

        //    //收费前台对本地一条对医保多条的项目进行了处理
        //    if (!string.IsNullOrEmpty(f.Compare.CenterItem.ID)&&f.Compare.CenterItem.ID.Length>1) //2010-9-1
        //    {
        //        f.Item.Memo = f.Item.UserCode;
        //        f.Item.UserCode = f.Compare.CenterItem.ID;
        //    }
        //    else
        //    {
        //        if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
        //        {
        //            FS.HISFC.Models.Pharmacy.Item phaItem = itemPharmacyManager.GetItem(f.Item.ID);
        //            if (phaItem == null)
        //            {
        //                this.errMsg = "获得药品信息出错!";
        //                Disconnect();
        //            }
        //            #region 2010-8-18
        //            //药品使用的是国际码进行对照 2010-9-1
        //            //药品对照使用其他码进行对照 2010-9-25

        //            f.Item.Memo = f.Item.UserCode;
        //            f.Item.UserCode = phaItem.NameCollection.OtherSpell.SpellCode.ToString(); 

        //            #endregion
        //        }
        //        else if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
        //        {
        //            FS.HISFC.Models.Base.Item baseItem = new FS.HISFC.Models.Base.Item();
        //            baseItem = itemManager.GetValidItemByUndrugCode(f.Item.ID);  //2010-9-1
        //            //baseItem = itemManager.GetValidItemByUndrugCode(f.ID);
        //            if (baseItem == null)
        //            {
        //                this.errMsg = "获得非药品信息出错!";
        //                Disconnect();
        //            }
        //            f.Item.Memo = f.Item.UserCode;
        //            f.Item.UserCode = baseItem.NationCode;  
        //            //国际码和自定义码进行互换(收款员习惯使用国际码进行搜索) 2010-9-1
        //        }
        //    }
        //}
        /// <summary>
        /// 处理项目对照 -- 按项目上传
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <param name="patientType"></param>
        /// <returns></returns>
        private void DealFeeItemList(FS.HISFC.Models.Fee.FeeItemBase f)
        {
            itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            itemPharmacyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            ////收费前台对本地一条对医保多条的项目进行了处理
            //if (!string.IsNullOrEmpty(f.Compare.CenterItem.ID) && f.Compare.CenterItem.ID != "0") //2010-9-1
            //{
            //    // 不处理，医保定上传自定义编码
            //    //f.Item.UserCode = f.Compare.CenterItem.ID;

            //}
            //else
            {
                if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {

                    FS.HISFC.Models.Pharmacy.Item phaItem = itemPharmacyManager.GetItem(f.Item.ID);
                    if (phaItem == null)
                    {
                        this.errMsg = "获得药品信息出错!";
                        Disconnect();
                    }
                    #region 2010-12-27
                    f.Item.UserCode = phaItem.UserCode;
                    f.Item.SpecialPrice = phaItem.SpecialPrice;
                    #endregion
                }
                else if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    FS.HISFC.Models.Base.Item baseItem = new FS.HISFC.Models.Base.Item();
                    baseItem = itemManager.GetItemByUndrugCode(f.Item.ID);
                    //baseItem = itemManager.GetValidItemByUndrugCode(f.Item.ID);  //2010-9-1
                    //baseItem = itemManager.GetValidItemByUndrugCode(f.ID);
                    if (baseItem == null)
                    {
                        this.errMsg = "获得非药品信息出错!";
                        Disconnect();
                    }
                    f.Item.UserCode = baseItem.UserCode;
                    f.Item.SpecialPrice = baseItem.SpecialPrice;
                    //f.Item.UserCode = baseItem.NationCode;  //国际码和自定义码进行互换(收款员习惯使用国际码进行搜索) 2010-9-1
                }
            }
        }
        #endregion

        #region IMedcareTranscation 成员

        /// <summary>
        /// 接口连接
        /// </summary>
        /// <returns></returns>
        private int DB2Connect()
        {
        //为本地的数据库连接设置事务

            ReConnect:
            if (siBizProcess == null)
            {
                try
                {
                    siBizProcess = new FoShanSI.Management.SIBizProcess();
                }
                catch (Exception e)
                {
                    DialogResult result = MessageBox.Show("数据库连接失败是否重新设置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucConnectDB2Server);
                        goto ReConnect;
                    }
                    else
                    {
                        this.errMsg = "数据连接失败!" + e.Message;
                        return -1;
                    }
                }
            }
            else
            {
                siBizProcess.Open();
            }
            return 1;
        }

        public void BeginTranscation()
        {
            // {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
            this.accFeeList = new List<FS.HISFC.Models.Account.AccountCardFee>();

            if (siBizProcess == null)
            {
                this.errMsg = "没有数据库连接！";
                return;
            }
            try
            {
                siBizProcess.BeginTranscation();
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;
                return;
            }
        }

        public long Commit()
        {
            // {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
            this.accFeeList = new List<FS.HISFC.Models.Account.AccountCardFee>();

            if (siBizProcess == null)
            {
                return 1;
            }
            try
            {
                if (siBizProcess != null)
                {
                    siBizProcess.Commit();
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        public long Connect()
        {
            return 1;
        }

        public long Disconnect()
        {
            if (siBizProcess == null)
            {
                this.errMsg = "数据库已断开连接，不能断开!";
                return 1;
            }
            try
            {
                siBizProcess.Close();
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        public long Rollback()
        {
            // {6E8955EE-09C2-40b5-89B7-B31326EDD753} 佛山居民医保二次改造
            this.accFeeList = new List<FS.HISFC.Models.Account.AccountCardFee>();

            if (siBizProcess == null)
            {
                return 1;
            }
            try
            {
                siBizProcess.RollBack();
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        #endregion

        /// <summary>
        /// 清空错误显示信息--防止历史错误信息没有清空返回到前台情况
        /// </summary>
        public void ClearErrMsg()
        {
            this.errMsg = string.Empty;//清空错误码信息
            this.errCode = string.Empty;
        }

        #region IMedcare 成员


        public int QueryCanMedicare(FS.HISFC.Models.Registration.Register r)
        {
            return this.objPublicLocalSI.ValidHospitalStaff(r, ref this.errMsg);
        }

        public int QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            return -1;
        }

        public int QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            return -1;
        }

        #endregion

        #region IMedcareBuDan成员

        public int BdBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;

        }
        /// <summary>
        ///预结算后记录社保的结算信息
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int BalanceOutpatientAfterPreBalance(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return BalanceOutpatient(r, ref feeDetails);
        }
        #endregion
    }
}
