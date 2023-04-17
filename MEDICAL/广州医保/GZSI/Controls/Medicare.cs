using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.BizLogic.Fee;
using FS.FrameWork.Management;
using System.Collections;
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Fee.Outpatient;
using System.Windows.Forms;
using GZSI.Management;

namespace GZSI.Controls
{
    /// <summary>
    /// 广州医保
    /// </summary>
    class Medicare : FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxyBase
    {
        #region 变量
        private SIConnect conn = null;
        private string errMsg = "";
        private Controls.ucInputRegNo inputRegNo = new Controls.ucInputRegNo();
        private ucGetSiInHosInfo queryMany = new ucGetSiInHosInfo();
        private usSetConnectSqlServer setSql = new usSetConnectSqlServer();
        private System.Data.IDbTransaction trans = null;
        private DateTime operDate;
        #endregion

        /// <summary>
        /// 本地数据连接
        /// </summary>
        public System.Data.IDbTransaction Trans
        {
            set
            {
                this.trans = value;
            }
        }
        /// <summary>
        /// 设置本地数据库连接
        /// </summary>
        /// <param name="t"></param>
        public void SetTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
        }

        /// <summary>
        /// 错误编码
        /// </summary>
        public string ErrCode
        {
            get
            {
                return errMsg;
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return errMsg;
            }
        }

        /// <summary>
        /// 控件描述，最好填写。
        /// </summary>
        public string Description
        {
            get
            {
                return "广州医保接口实现";
            }
        }

        #region 公有

        /// <summary>
        /// 获得黑名单信息
        /// </summary>
        /// <param name="blackLists">黑名单信息</param>
        /// <returns>成功 >= 1 失败 -1 没有获得数据 0</returns>
        public int QueryBlackLists(ref ArrayList blackLists)
        {
            return 1;
        }

        /// <summary>
        /// 验证在院患者是否属于黑名单
        /// </summary>
        /// <param name="patient">在院患者基本信息</param>
        /// <returns>在黑名单内 true 不在和名单内false</returns>
        public bool IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return false;
        }

        /// <summary>
        /// 验证门诊患者是否属于黑名单
        /// </summary>
        /// <param name="r">门诊患者基本信息</param>
        /// <returns>在黑名单内 true 不在和名单内false</returns>
        public bool IsInBlackList(FS.HISFC.Models.Registration.Register r)
        {
            return false;
        }

        /// <summary>
        /// 获得非药品信息目录

        /// </summary>
        /// <param name="undrugLists">非药品信息目录</param>
        /// <returns>成功 >= 1 失败: -1 没有获得数据 0</returns>
        public int QueryUndrugLists(ref ArrayList undrugLists)
        {
            return 1;
        }

        /// <summary>
        /// 获得药品信息目录
        /// </summary>
        /// <param name="drugLists">药品信息目录</param>
        /// <returns>成功 >= 1 失败: -1 没有获得数据 0</returns>
        public int QueryDrugLists(ref ArrayList drugLists)
        {
            return 1;
        }

        #endregion

        #region 门诊

        /// <summary>
        /// 门诊挂号函数
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int UploadRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            #region 不用
            //if (r.ID == null || r.ID == "")
            //{
            //    this.errMsg = "患者的挂号流水号没有付值!";
            //    return -1;
            //}
            //if (r.IDCard == null || r.IDCard == "")
            //{
            //    this.errMsg = "患者身份证号码为空";
            //    return -1;
            //}
            //this.MyConnect();
            //if (this.conn == null)
            //{
            //    this.errMsg = "没有设置数据库连接!" + errMsg;
            //    return -1;
            //}
            //FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            //if (this.trans != null)
            //{
            //    myInterface.SetTrans(this.trans);
            //}
            ////好像有问题  等测试时候看看修改


            ////FS.HISFC.Models.RADT.PatientInfo p = myInterface.GetSIPersonInfo(r.ID);
            ////if (p == null)
            ////{
            ////    this.errMsg = "查询广州医保患者基本信息出错!" + myInterface.Err;
            ////}
            ////if (p == null)
            ////{
            ////    p = new FS.HISFC.Models.RADT.PatientInfo();
            ////}
            ////if (p.ID == null || p.ID == "")
            ////{
            ////    this.errMsg = "没有查找到挂号流水号为: " + r.ID + "的患者医保挂号信息";
            ////}
            ////r.SIMainInfo = p.SIMainInfo;

            //string tmpRegNo = "";
            //bool isSpecailPact = false;//是否特殊合同单位

            //isSpecailPact = myInterface.IsPactDealByInpatient(r.Pact.ID);
            //if (!isSpecailPact)
            //{
            //    tmpRegNo = conn.GetNewRegNo(r.IDCard);
            //}
            //else
            //{
            //    tmpRegNo = conn.GetNewRegNoInpatient(r.IDCard);
            //}
            //if (tmpRegNo == null || tmpRegNo == "")
            //{
            //    MyDisconnect();
            //    this.errMsg = "获得患者就医登记号出错,可能患者没有在医保端登记,请登记!";
            //    return -1;
            //}
            //r.SIMainInfo.RegNo = tmpRegNo;

            //this.MyDisconnect();
            #endregion
            return 1;
        }

        /// <summary>
        /// 取消入院登记方法
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <returns>成功 1 失败 -1 没有记录 0</returns>
        public int CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }
        /// <summary>
        /// 获得医保挂号信息
        /// </summary>
        /// <param name="r">门诊挂号实体</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            this.MyConnect();

            if (conn == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }
            int iReturn = 0;

            Management.SILocalManager myInterface = new  SILocalManager();
            if (this.trans != null)
            {
                myInterface.SetTrans(this.trans);
            }
            //queryMany.Init(Controls.ucGetSiClinicInfocs.PatientType.OutPatient);
            queryMany.IsSpecialPact = myInterface.IsPactDealByInpatient(r.Pact.ID);
            //queryMany.RegInfos = null;
            queryMany.QueryPatientType = ucGetSiInHosInfo.PatientType.OutPatient;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(queryMany);
            if (queryMany.RegNo == null)
            {
                MyDisconnect();
                this.errMsg = "没有选取患者医保挂号信息";
                return -1;
            }
            if (!queryMany.IsSpecialPact)
            {
                iReturn = conn.GetRegInfo(queryMany.RegNo, ref r);
            }
            else
            {
                iReturn = conn.GetRegInfoFromInpatient(queryMany.RegNo, ref r);
            }
            if (iReturn == -1)
            {
                MyDisconnect();
                this.errMsg = "查询挂号信息出错!" + conn.Err;
                return -1;
            }
            //Interface myInterface = new Interface();
            //myInterface.SetTrans(this.trans.Trans);
            //if (r.Name != regFromSI.Name)
            //{
            //    DialogResult rs = MessageBox.Show("选择的患者名字与挂号时不一致,是否重新选择", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //    if (rs == DialogResult.Yes)
            //    {
            //        isChooseOk = false;
            //    }
            //    else
            //    {
            //        isChooseOk = true;
            //    }
            //}
            //else
            //{
            //    isChooseOk = true;
            //}

            //r.SSN = r.SIMainInfo.RegNo;
            r.SIMainInfo.IsValid = true;
            r.SIMainInfo.IsBalanced = false;
            iReturn = myInterface.InsertSIMainInfo(r);
            if (iReturn <= 0)
            {
                MyDisconnect();
                this.errMsg = "插入医保登记出错!" + myInterface.Err;
                return -1;
            }
            //}
            MyDisconnect();
            return 1;
        }

        /// <summary>
        /// 单条上传费用明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="f">门诊费用明细</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, int seq)
        {
            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            {
                this.errMsg = "没有找到患者的就医登记号";
                return -1;
            }
            if (f == null)
            {
                this.errMsg = "明细为空!";
                return -1;
            }
            this.MyConnect();
            if (this.conn == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }
            bool isSpecailPact = false;//是否特殊合同单位
            SILocalManager myInterface = new  SILocalManager();
            operDate = myInterface.GetDateTimeFromSysDateTime();
            if (this.trans != null)
            {
                myInterface.SetTrans(this.trans);
            }
            isSpecailPact = myInterface.IsPactDealByInpatient(r.Pact.ID);
            bool isInPatientDealPact = myInterface.IsPactDealByInpatient(r.Pact.ID, "INPATIENT"); 
            if (!isSpecailPact && !isInPatientDealPact)
            {
                if (conn.InsertShareData(r, f, seq, operDate) == -1)
                {
                    MyDisconnect();
                    return -1;
                }
                else
                {
                    MyDisconnect();
                    return 1;
                }
            }
            else
            {
                if (conn.InsertShareDataInpatient(r, f, operDate) == -1)
                {
                    MyDisconnect();
                    return -1;
                }
                else
                {
                    MyDisconnect();
                    return 1;
                }
            }
        }

        /// <summary>
        /// 多条上传费用明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="feeDetails">费用明细实体集合</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {

            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            {
                this.errMsg = "没有找到患者的就医登记号";
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

            this.MyConnect();
            if (this.conn == null)
            {
                this.errMsg = "没有设置医保数据库连接!";
                MyDisconnect();
                return -1;
            }
            decimal txCost = 0m;

            Item myItem = new Item();
            FS.HISFC.BizLogic.Pharmacy.Item myPItem = new FS.HISFC.BizLogic.Pharmacy.Item();
            if (this.trans != null)
            {
                myItem.SetTrans(trans);
                myPItem.SetTrans(trans);
            }
            foreach (FeeItemList f in feeDetails)
            {
                if (f.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item phaItem = myPItem.GetItem(f.Item.ID);
                    if (phaItem == null)
                    {
                        this.errMsg = "获得药品信息出错!";
                        MyDisconnect();
                        return -1;
                    }
                    f.Item.UserCode = phaItem.UserCode;
                }
                else
                {
                    FS.HISFC.Models.Base.Item baseItem = new FS.HISFC.Models.Base.Item();
                    baseItem = myItem.GetValidItemByUndrugCode(f.Item.ID);
                    if (baseItem == null)
                    {
                        this.errMsg = "获得非药品信息出错!";
                        MyDisconnect();
                        return -1;
                    }
                    f.Item.UserCode = baseItem.UserCode;
                }

            }

            int iReturn = -1;
            SILocalManager myInterface = new  SILocalManager();
            if (this.trans != null)
            {
                myInterface.SetTrans(this.trans);
            }
            operDate = myInterface.GetDateTimeFromSysDateTime();

            ArrayList alTempFeeDetails = (feeDetails.Clone() as ArrayList);

            for (int i = alTempFeeDetails.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList temp = alTempFeeDetails[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                //if ((temp.Compare == null || temp.Compare.HisCode == null || temp.Compare.HisCode == "") && !temp.IsGroup)
                //{
                //    myInterface.GetCompareSingleItem("2", temp.Item.ID, ref temp.Compare);
                //}

                //if (temp.Item.MinFee.ID == "025" || temp.Item.UserCode == null || temp.Item.UserCode == "" || /*((temp.Compare == null || temp.Compare.HisCode == null || temp.Compare.HisCode == "") && */temp.IsGroup) //特需服务费费用不上传 未对照费用也不上传 
                //{
                //    txCost += temp.FT.TotCost;
                //    alTempFeeDetails.Remove(temp);
                //}

                if (r.Pact.ID == "201" && temp.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (myInterface.GetCompareSingleItem("201", temp.Item.ID, ref temp.Compare) == -2) //没有维护对照的药品
                    {
                        txCost += temp.FT.TotCost;
                        alTempFeeDetails.Remove(temp);
                    }
                }
                else if (r.Pact.ID == "202" && temp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (myInterface.GetCompareSingleItem("202", temp.Item.ID, ref temp.Compare) == -2) //没有维护对照的药品
                    {
                        txCost += temp.FT.TotCost;
                        alTempFeeDetails.Remove(temp);
                    }
                }
                //add by liuyi 合同单位是慢性再生障碍性贫血
                else if (r.Pact.ID == "203")
                {
                    if (myInterface.GetCompareSingleItem("203", temp.Item.ID, ref temp.Compare) == -2) //没有维护对照的药品
                    {
                        txCost += temp.FT.TotCost;
                        alTempFeeDetails.Remove(temp);
                    }
                }
                //end
            }

            r.SIMainInfo.OperInfo.User03 = txCost.ToString();  //存特需服务费费用


            bool isSpecailPact = myInterface.IsPactDealByInpatient(r.Pact.ID);
            bool isInPatientDealPact = myInterface.IsPactDealByInpatient(r.Pact.ID, "INPATIENT"); 
            if (!isSpecailPact && !isInPatientDealPact)
            {
                iReturn = conn.InsertShareData(r, alTempFeeDetails, operDate);
            }
            else
            {
                iReturn = conn.InsertShareDataInpatient(r, alTempFeeDetails, operDate);
            }
            if (iReturn < 0)
            {
                this.errMsg = "上传明细失败!" + conn.Err;
                MyDisconnect();
                return -1;
            }
            if (iReturn == 0)
            {
                this.errMsg = "没有可上传的费用,请将合同单位设为自费或者其他";
                MyDisconnect();
                return -1;
            }
            MyDisconnect();
            return 1;
        }

        /// <summary>
        /// 删除单条已经上传明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="f">费用明细信息</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            {
                this.errMsg = "没有找到患者的就医登记号";
                return -1;
            }
            MyConnect();
            if (this.conn == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }
            if (f == null)
            {
                MyDisconnect();
                this.errMsg = "明细为空!";
                return -1;
            }
            bool isSpecailPact = false;//是否特殊合同单位
            SILocalManager myInterface = new  SILocalManager();
            if (this.trans != null)
            {
                myInterface.SetTrans(this.trans);
            }
            isSpecailPact = myInterface.IsPactDealByInpatient(r.Pact.ID);
            bool isInPatientDealPact = myInterface.IsPactDealByInpatient(r.Pact.ID, "INPATIENT"); 
            if (!isSpecailPact && !isInPatientDealPact)
            {
                if (conn.DeleteItemListClinic(r.SIMainInfo.RegNo) == -1)
                {
                    Disconnect();
                    return -1;
                }
                Disconnect();
                return 1;
            }
            else
            {
                if (conn.DeleteItemList(r.SIMainInfo.RegNo) == -1)
                {
                    Disconnect();
                    return -1;
                }
                Disconnect();
                return 1;
            }
        }

        /// <summary>
        /// 删除患者的所有费用上传明细

        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            {
                this.errMsg = "没有找到患者的就医登记号";
                return -1;
            }

            MyConnect();

            if (this.conn == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }

            //if(feeDetails == null)
            //{
            //    this.errMsg = "明细为空!";
            //    return -1;
            //}
            //if(feeDetails.Count == 0)
            //{
            //    this.errMsg = "明细数量为 0";
            //    return -1;
            //}

            bool isSpecailPact = false;//是否特殊合同单位
            SILocalManager myInterface = new  SILocalManager();
            if (this.trans != null)
            {
                myInterface.SetTrans(this.trans);
            }
            isSpecailPact = myInterface.IsPactDealByInpatient(r.Pact.ID);
            bool isInPatientDealPact = myInterface.IsPactDealByInpatient(r.Pact.ID, "INPATIENT"); 
            if (!isSpecailPact && !isInPatientDealPact)
            {
                if (conn.DeleteItemListClinic(r.SIMainInfo.RegNo) == -1)
                {
                    MyDisconnect();
                    return -1;
                }
                MyDisconnect();
                return 1;
            }
            else
            {
                if (conn.DeleteItemList(r.SIMainInfo.RegNo) == -1)
                {
                    MyDisconnect();
                    return -1;
                }
                MyDisconnect();
                return 1;
            }

        }

        /// <summary>
        /// 删除指定数据集的明细
        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="feeDetails">要删除的费用实体明细</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            {
                this.errMsg = "没有找到患者的就医登记号";
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

            MyConnect();

            if (this.conn == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }
            bool isSpecailPact = false;//是否特殊合同单位
            SILocalManager myInterface = new  SILocalManager();
            if (this.trans != null)
            {
                myInterface.SetTrans(this.trans);
            }
            isSpecailPact = myInterface.IsPactDealByInpatient(r.Pact.ID);
            bool isInPatientDealPact = myInterface.IsPactDealByInpatient(r.Pact.ID, "INPATIENT"); 
            if (!isSpecailPact && !isInPatientDealPact)
            {
                if (conn.DeleteItemListClinic(r.SIMainInfo.RegNo) == -1)
                {
                    this.MyDisconnect();
                    return -1;
                }
                MyDisconnect();
                return 1;
            }
            else
            {
                if (conn.DeleteItemList(r.SIMainInfo.RegNo) == -1)
                {
                    MyDisconnect();
                    return -1;
                }
                MyDisconnect();
                return 1;
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
            this.errMsg = "不允许修改上传明细";
            return 1;
        }

        /// <summary>
        /// 修改多条门诊已上传明细

        /// </summary>
        /// <param name="r">挂号信息</param>
        /// <param name="feeDetails">要修改的费用实体明细集合</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            this.errMsg = "不允许修改上传明细";
            return 1;
        }

        /// <summary>
        /// 医保预结算

        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int PreBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            {
                this.errMsg = "没有找到患者的就医登记号";
                return -1;
            }
            FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();
            if (this.trans != null)
            {
                interfaceMgr.SetTrans(this.trans);
            }
            //foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in feeDetails)
            //{
            //    if (feeItem.Item.MinFee.ID != "025" &&( feeItem.Item.UserCode == null || feeItem.Item.UserCode == "" || feeItem.Compare == null || feeItem.Compare.HisCode == null || feeItem.Compare.HisCode == ""))
            //    {
            //        DialogResult result = MessageBox.Show("上传的费用中包含未对照的项目,未对照项目作自费处理,\n 是否继续上传", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //        if (result == DialogResult.No)
            //        {
            //            this.DeleteUploadedFeeDetailsAllOutpatient(r);
            //            return -1;
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }
            //}

            MyConnect();

            if (this.conn == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }
            Controls.frmBalance frmPopBalance = new Controls.frmBalance();
            frmPopBalance.ucBalanceClinic1.RInfo = r;
            frmPopBalance.ucBalanceClinic1.Conn = conn;

            frmPopBalance.ShowDialog();
            if (frmPopBalance.ucBalanceClinic1.IsCorrect == false)
            {
                MyDisconnect();
                return -1;
            }
            else
            {
                r = frmPopBalance.ucBalanceClinic1.RInfo;
                MyDisconnect();
                return 1;
            }
        }

        /// <summary>
        /// 医保结算
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            //if (r.ID == null || r.ID == "")
            //{
            //    this.errMsg = "没有找到患者的挂号流水号!";
            //    return -1;
            //}
            //if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
            //{
            //    this.errMsg = "没有找到患者的就医登记号";
            //    return -1;
            //}

            //MyConnect();

            //if (this.conn == null)
            //{
            //    this.errMsg = "没有设置数据库连接!";
            //    return -1;
            //}
            //Controls.frmBalance frmPopBalance = new Controls.frmBalance();
            //frmPopBalance.ucBalanceClinic1.RInfo = r;
            //frmPopBalance.ucBalanceClinic1.Conn = conn;

            //frmPopBalance.ShowDialog();
            //if (frmPopBalance.ucBalanceClinic1.IsCorrect == false)
            //{
            //    MyDisconnect();
            //    return -1;
            //}
            //else
            //{
            //    r = frmPopBalance.ucBalanceClinic1.RInfo;
            //    MyDisconnect();
            return 1;

        }

        /// <summary>
        /// 取消结算
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">要取消结算的患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            this.errMsg = "广州医保暂时不支持取消结算!";
            return 1;
        }

        /// <summary>
        /// 取消结算(半退)
        /// </summary>
        /// <param name="r">患者挂号信息</param>
        /// <param name="feeDetails">要取消结算的患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            this.errMsg = "广州医保暂时不支持取消结算!";
            return 1;
        }

        #endregion

        #region 住院

        /// <summary>
        /// 更新费用信息
        /// /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="f">费用明细</param>
        /// <returns>成功 1 失败 -1 没有更新到数据 0</returns>
        public int UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        /// <summary>
        /// 重新计算费用明细
        /// </summary>
        /// <param name="patient">住院患者基本信息</param>
        /// <param name="feeItemList">住院费用单条明细</param>
        /// <returns>成功 1 失败 -1</returns>
        public int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            FS.HISFC.BizLogic.Fee.Interface FeeInterface = new FS.HISFC.BizLogic.Fee.Interface();
            FeeInterface.SetTrans(this.trans);
            return FeeInterface.ComputeRate(patient.Pact.ID, ref feeItemList);
        }

        /// <summary>
        /// 住院登记函数
        /// </summary>
        /// <param name="patient">住院登记患者基本信息</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int UploadRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //if (conn == null)
            //{
            //    this.errMsg = "没有设置数据库连接!";
            //    return -1;
            //}
            //int iReturn = 0;

            //FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            //if (this.trans != null)
            //{
            //    myInterface.SetTrans(this.trans);
            //}
            //queryMany.QueryPatientType = ucGetSiInHosInfo.PatientType.InPatient;
            //FS.FrameWork.WinForms.Classes.Function.PopShowControl(queryMany);
            //if (queryMany.PersonInfo == null||queryMany.RegNo == null)
            //{
            //    this.errMsg = "没有选取患者医保挂号信息";
            //    return -1;
            //}

            //patient = queryMany.PersonInfo.Clone();

            //if (iReturn == -1)
            //{
            //    this.errMsg = "查询挂号信息出错!" + conn.Err;
            //    return -1;
            //}
            //if (this.trans == null)
            //{
            //    this.errMsg = "没有传递TransCation!";
            //    return -1;
            //}
            ////Interface myInterface = new Interface();
            ////myInterface.SetTrans(this.trans.Trans);
            //r.SIMainInfo.IsValid = true;
            patient.SIMainInfo.IsBalanced = false;
            FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            myInterface.SetTrans(this.trans);
            int iReturn = myInterface.UpdateSiMainInfo(patient);
            if (iReturn == -1)
            {
                this.errMsg = "插入医保登记出错!" + myInterface.Err;
                return -1;
            }
            if (iReturn == 0)
            {
                iReturn = myInterface.InsertSIMainInfo(patient);
                if (iReturn <= 0)
                {
                    this.errMsg = "插入医保登记出错!" + myInterface.Err;
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 取消入院登记方法
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <returns>成功 1 失败 -1 没有记录 0</returns>
        public int CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        /// <summary>
        /// 出院召回方法
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <returns>成功 1 失败 -1 没有记录 0</returns>
        public int RecallRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        /// <summary>
        /// 获得医保住院登记信息
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.MyConnect();

            if (conn == null)
            {
                this.errMsg = "没有设置数据库连接!";
                return -1;
            }

            SILocalManager myInterface = new  SILocalManager();
            if (this.trans != null)
            {
                myInterface.SetTrans(this.trans);
            }
            //queryMany.Init(Controls.ucGetSiClinicInfocs.PatientType.OutPatient);
            queryMany.IsSpecialPact = myInterface.IsPactDealByInpatient(patient.Pact.ID);
            //queryMany.RegInfos = null;
            queryMany.QueryPatientType = ucGetSiInHosInfo.PatientType.InPatient;
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(queryMany);
            if (queryMany.RegNo == null)
            {
                this.errMsg = "没有选取患者医保挂号信息";
                MyDisconnect();
                return -1;
            }
            patient.SIMainInfo = queryMany.PersonInfo.SIMainInfo;
            patient.IDCard = queryMany.PersonInfo.IDCard;
            string priorName = patient.Name;
            patient.Name = queryMany.PersonInfo.Name;
            patient.Pact.ID = "2";
            patient.CompanyName = queryMany.PersonInfo.CompanyName;
            patient.PVisit = queryMany.PersonInfo.PVisit;
            patient.Birthday = queryMany.PersonInfo.Birthday;
            patient.Sex = queryMany.PersonInfo.Sex;

            MyDisconnect();
            if (priorName != null && priorName != "" && priorName != queryMany.PersonInfo.Name)
            {
                //DialogResult r = MessageBox.Show("所选医保患者名字和在院登记名字不一致,是否继续", "提示", MessageBoxButtons.YesNo);
                MessageBox.Show("所选医保患者名字和在院登记名字不一致,请检查选择是否正确!");
                // if (r == DialogResult.Yes)
                //{
                //   return 1;
                //  }
                //else
                // {
                return -1;
                // }
            }
            return 1;
        }

        /// <summary>
        /// 出院登记方法
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <returns>成功 1 失败 -1 没有记录 0</returns>
        public int LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return this.CheckTotalUploadEqualBalance(patient);
        }

        /// <summary>
        /// 单条上传费用明细(不实现)
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="f">住院费用明细</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        /// <summary>
        /// 多条上传费用明细(没有实现)
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">费用明细实体集合</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            return 1;
        }

        /// <summary>
        /// 上传住院患者费用费用
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int MyUploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, DateTime beginTime, DateTime endTime,ref ArrayList feeDetails)
        {
            MyConnect();
            FS.HISFC.BizLogic.Pharmacy.Item phaItemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
            FS.HISFC.BizLogic.Fee.Item feeItemMgr = new FS.HISFC.BizLogic.Fee.Item();
            SILocalManager siLocalManager = new SILocalManager();
            //设置事务
            phaItemMgr.SetTrans(trans);
            feeItemMgr.SetTrans(trans);
            siLocalManager.SetTrans(trans);

            //循环提交单条明细
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeDetails)
            {
                #region 处理只有对照过的才上传
                if (!conn.IsUpLoad(f.Item))
                {
                    continue;
                }
                #endregion

                //更新明细表,已上传标志									
                if (siLocalManager.UpdateUploadFlag(patient.ID, beginTime, endTime, "1", f) <= 0)//错误
                {
                    this.errMsg = "更新上传标志出错!" + siLocalManager.Err;
                    return -1;
                }
                //上传的费用，本次存储一份
                if (siLocalManager.InsertItemList(patient, f) == -1)
                {
                    this.errMsg = "医保上传明细出错!" + siLocalManager.Err;
                    return -1;
                }

                //上传医保服务器
                if (!string.IsNullOrEmpty(f.Item.Specs) && f.Item.Specs.Length > 14)
                {
                    f.Item.Specs = f.Item.Specs.Substring(0, 14);
                }

                if (conn.InsertItemList(patient, f) == -1)
                {
                    this.errMsg = "医保上传明细出错!" + conn.Err;
                    MyDisconnect();
                    return -1;
                }
            }

            MyDisconnect();
            return 1;
        }

        /// <summary>
        /// 上传费用
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int MyUploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            MyConnect();

            FS.HISFC.BizLogic.Pharmacy.Item phaItemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();//药品
            FS.HISFC.BizLogic.Fee.Item feeItemMgr = new FS.HISFC.BizLogic.Fee.Item();//非药品


            FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            //设置事务
            myInterface.SetTrans(trans);
            phaItemMgr.SetTrans(trans);
            feeItemMgr.SetTrans(trans);
            int iReturn;  //函数返回值

            iReturn = UploadFeeDetailInpatient(patient, f, phaItemMgr, feeItemMgr, myInterface);
            if (iReturn == -1)
            {
                this.errMsg = "单条上传费用明细失败";
                MyDisconnect();
                return -1;
            }
            MyDisconnect();
            return 1;
        }

        private int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f, FS.HISFC.BizLogic.Pharmacy.Item phaItemMgr, FS.HISFC.BizLogic.Fee.Item feeItemMgr, FS.HISFC.BizLogic.Fee.Interface myInterface)
        {
            int iReturn;  //内部函数返回值


            if (f.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                #region 药品
                //更新药品明细表,已上传标志									
                iReturn = myInterface.UpdateUploadedDetailFlag(f.RecipeNO, f.SequenceNO, "1");

                 if (iReturn <=0)//错误
                {
                    this.errMsg = "更新上传标志出错!" + myInterface.Err;
                    return -1;
                }

                #endregion
            }
            else
            {
                #region 非药品

                //更新非药品明细表,已上传标志

                iReturn = myInterface.UpdateUploadedDetailFlag(f.RecipeNO, f.SequenceNO, "2");

                 if (iReturn <=0)//错误
                {
                    this.errMsg = "更新上传标志出错!" + myInterface.Err;
                    return -1;
                }
          
                #endregion
            }
            //上传医保服务器
            if (!string.IsNullOrEmpty(f.Item.Specs)&&f.Item.Specs.Length>14)
            {
                f.Item.Specs = f.Item.Specs.Substring(0, 14); 
            }
           
            iReturn = conn.InsertItemList(patient, f);
            if (iReturn == -1)
            {
                this.errMsg = "医保上传明细出错!" + conn.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 删除单条已经上传明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="f">费用明细信息</param>
        /// <returns>-1 失败 0 没有记录 1 成功</returns>
        public int DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        /// <summary>
        /// 删除患者的所有费用上传明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        /// <summary>
        /// 删除指定数据集的明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">要删除的费用实体明细</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            return 1;
        }

        /// <summary>
        /// 修改单条住院已上传明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="f">要修改的费用实体明细</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        /// <summary>
        /// 修改多条住院已上传明细
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">要修改的费用实体明细集合</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            return 1;
        }

        /// <summary>
        /// 住院医保预结算
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            pactCode = patient.Pact.ID;
            MyConnect();
            int i = conn.GetBalanceInfoInpatient(patient);
            if (i == -1)
            {
                this.errMsg = "获取结算信息出错";
                return -1;
            }
            if (i == 0)
            {
                this.errMsg = "没有结算信息,请先去医保客户端进行结算，谢谢！";
                return -1;
            }
            //检查上传费用金额是否等于本地费用金额
            //if (-1 == this.CheckTotalUploadEqualBalance(patient))
            //{
            //    this.errMsg = "总金额不一致，请检查是否漏上传项目或者重复上传项目，谢谢！";
            //    return -1;
            //}
            //更新住院主表
            if (-1 == UploadRegInfoInpatient(patient))
            {
                this.errMsg = "更新医保住院主表出错！";
                return -1;
            }

            //累加不上传医保费用信息
            for (int m = 0; m <= feeDetails.Count - 1; ++m)
            {
                if (feeDetails[m] is FS.HISFC.Models.Fee.Inpatient.FeeItemList)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList item = feeDetails[m] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                    if (item.User03 == "3")//3为不上传费用
                    {
                        patient.SIMainInfo.PubCost += item.FT.PubCost;
                        patient.SIMainInfo.PayCost += item.FT.PayCost;
                        patient.SIMainInfo.OwnCost += item.FT.OwnCost;
                    }
                }
            }

            MyDisconnect();
            return i;
        }

        /// <summary>
        /// 住院医保中途结算
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            int state = this.GetInpatientMidBalanceInfo(patient);
            if (state == -1)
            {
                return -1;
            }
            return this.BalanceInpatient(patient, ref feeDetails);
        }

        /// <summary>
        /// 医保结算
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            FS.HISFC.Models.RADT.PatientInfo balancePatient = patient.Clone();
            FS.HISFC.Models.RADT.PatientInfo siPInfo = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new InPatient();

            #region 检查参数

            if (balancePatient.SIMainInfo.RegNo == null || balancePatient.SIMainInfo.RegNo == "")
            {
                this.errMsg = "没有该患者的医保信息";
                return -1;
            }

            if (balancePatient.SIMainInfo.InvoiceNo == null || balancePatient.SIMainInfo.InvoiceNo == "")
            {
                myInterface = null;
                this.errMsg = "主发票号(patient.SIMainInfo.InvoiceNo)没有赋值";
                return -1;
            }

            if (balancePatient.SIMainInfo.BalNo == null || balancePatient.SIMainInfo.BalNo == "")
            {
                myInterface = null;
                this.errMsg = "结算序号（patient.SIMainInfo.BalNo）没有赋值";
                return -1;
            }
            #endregion

            #region  更新到数据库
            if (MyConnect() == -1)
            {
                this.errMsg = "连接数据库出错";
                return -1;
            }
            myInterface.SetTrans(this.trans);

            balancePatient.SIMainInfo.IsBalanced = true;
            balancePatient.SIMainInfo.IsValid = true;
            balancePatient.SIMainInfo.BalanceDate = this.operDate;

            int iReturn = this.UploadRegInfoInpatient(balancePatient);   // myInterface.UpdateSiMainInfo(balancePatient);
            if (iReturn <= 0)
            {
                MyDisconnect();
                errMsg = "更新医保信息失败!" + myInterface.Err;
                myInterface = null;
                return -1;
            }
            myInterface = null;
            if (this.conn == null)
            {
                MyDisconnect();
                errMsg = "没有医保数据库的连接";
                return -1;
            }
            if (conn.UpdateBalaceReadFlag(balancePatient.SIMainInfo.RegNo, 1) != 0)
            {
                MyDisconnect();
                errMsg = "更新医保结算信息读入标志失败!" + conn.Err;
                return -1;
            }
            MyDisconnect();
            return 1;
            #endregion

        }

        /// <summary>
        /// 取消结算
        /// </summary>
        /// <param name="patient">住院登记基本信息</param>
        /// <param name="feeDetails">要取消结算的患者费用信息</param>
        /// <returns>-1 失败 0 没有记录 >=1 成功</returns>
        public int CancelBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            SILocalManager interfaceMgr = new  SILocalManager();
            if (this.trans != null)
            {
                interfaceMgr.SetTrans(this.trans);
            }
            FS.HISFC.Models.RADT.PatientInfo tempPatient = new FS.HISFC.Models.RADT.PatientInfo();
            tempPatient = interfaceMgr.GetSIPersonInfo(patient.ID, patient.SIMainInfo.InvoiceNo);
            if (tempPatient == null || tempPatient.ID == "")
            {
                return -1;
            }

            return interfaceMgr.InsertSIMainInfo(tempPatient);

        }

        #endregion

        #region IMedcareTranscation 成员

        /// <summary>
        /// 接口连接
        /// </summary>
        /// <returns></returns>
        private int MyConnect()
        {
            //为本地的数据库连接设置事务
            if (conn == null)
            {
            LabelTwo:
                try
                {
                    conn = new SIConnect();
                }
                catch (Exception e)
                {
                    DialogResult result = MessageBox.Show("数据库连接失败是否重新设置?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(setSql);
                        goto LabelTwo;
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
                conn.Open();
            }
            return 1;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <returns></returns>
        private int MyDisconnect()
        {
            //if (conn == null)
            //{
            //    this.errMsg = "数据库已连接，不能断开!";
            //    return 1;
            //}
            //try
            //{
            //    conn.Close();
            //}
            //catch (Exception e)
            //{
            //    this.errMsg = e.Message;
            //    return -1;
            //}
            return 1;
        }
        /// <summary>
        /// {48AFF8FF-8F83-4f2a-8464-85BA8F5A0F1E}广州医保上传报内存溢出错误，不知道谁将断开连接注释，可能是因为没有释放连接，导致。FangW,2016-07-01
        /// </summary>
        /// <returns></returns>
        public int MyDisconnectSI()
        {
            if (conn == null)
            {
                this.errMsg = "数据库已连接，不能断开!";
                return 1;
            }
            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 接口连接,初始化方法
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Connect()
        {
            //数据库的连接放在函数内部,在myConnect中实现

            return 1;
        }

        /// <summary>
        /// 关闭接口连接 清空方法
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Disconnect()
        {
            if (conn == null)
            {
                this.errMsg = "数据库已断开连接，不能断开!";
                return 1;
            }
            try
            {
                conn.Close();
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;
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
            if (conn == null)
            {
                this.errMsg = "没有创建数据库连接，不能提交!";
                return -1;
            }
            try
            {
                conn.Commit();
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 接口回滚方法
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public long Rollback()
        {
            try
            {
                conn.RollBack();
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 接口开始数据事务函数
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public void BeginTranscation()
        {
            if (conn == null)
            {
                this.errMsg = "没有创建数据库连接，创建事务!";
                return;
            }
            try
            {
                conn.BeginTranscation();
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;
                return;
            }
        }

        #endregion




        public interface IFeeExtend
        {
            /// <summary>
            /// 特殊验证合法性

            /// </summary>
            /// <param name="feeItemList">当前收费项目信息</param>
            /// <param name="errText">错误信息</param>
            /// <returns>true合法 false不合法</returns>
            bool IsValid(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList, ref string errText);
        }


        #region IMedcare 成员


        public int GetLocalRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            FS.HISFC.BizLogic.Fee.Interface interfaceMgr = new FS.HISFC.BizLogic.Fee.Interface();
            if (this.trans != null)
            {
                interfaceMgr.SetTrans(this.trans);
            }
            FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
            p = interfaceMgr.GetSIPersonInfo(patient.ID);
            if (p != null)
            {
                patient.SIMainInfo = p.SIMainInfo;
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public int GetLocalRegInfoOutpatient(Register r)
        {
            return 1;
        }

        #endregion

        #region IMedcare 成员
        /// <summary>
        /// 获取医保患者在医保的结算信息
        /// </summary>
        /// <param name="patient">患者实体</param>
        /// <returns></returns>
        public int GetInpatientBalanceInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            FS.HISFC.Models.RADT.PatientInfo balancePatient = patient.Clone();
            FS.HISFC.Models.RADT.PatientInfo siPInfo = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new InPatient();

            if (MyConnect() == -1)
            {
                this.errMsg = "连接数据库出错";
                return -1;
            }

            #region 获取医保结算信息
            siPInfo = myInterface.GetSIPersonInfo(balancePatient.ID);
            if (siPInfo == null)
            {
                MyDisconnect();
                this.errMsg = "获得医保基本信息失败";
                return -1;
            }
            balancePatient.SIMainInfo = siPInfo.SIMainInfo;
            int iReturn = 0;
            iReturn = conn.GetBalanceInfo(balancePatient);
            if (iReturn == 0)
            {
                MyDisconnect();
                this.errMsg = "请先在医保客户端进行结算!";
                return -1;
            }
            if (iReturn == -1)
            {
                MyDisconnect();
                this.errMsg = "获得医保患者结算信息出错!" + conn.Err;
                return -1;
            }
            #endregion

            #region 上传的数据与医保数据比较
            //decimal tempTotCost = 0;
            //if (feeInpatient.GetUploadTotCost(balancePatient.ID, ref tempTotCost) == -1)
            //{
            //    MyDisconnect();
            //    this.errMsg = "获取上传总费用出错！" + feeInpatient.Err;
            //    return -1;
            //}

            ////如果本地费用 与结算费用不符


            ////则认为有多条结算信息，弹出输入医保流水号的对话框，如果选择取消，则退出。


            //while (tempTotCost != balancePatient.SIMainInfo.TotCost)
            //{

            //    FS.HISFC.Models.RADT.PatientInfo SIPatient = new FS.HISFC.Models.RADT.PatientInfo();

            //    Controls.ucTextInput ucTextInput1 = new UFC.InterfaceSI.SiClinic.Controls.ucTextInput();
            //    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucTextInput1);

            //    if (!ucTextInput1.IsOK)
            //    {
            //        this.MyDisconnect();
            //        this.errMsg = "如果本地费用与结算费用不符,请查明原因";
            //        return -1;
            //    }

            //    SIPatient.SIMainInfo.RegNo = ucTextInput1.TextRegNO;
            //    iReturn = conn.GetBalanceInfo(SIPatient);
            //    if (iReturn == 0)
            //    {
            //        MyDisconnect();
            //        myInterface = null;
            //        errMsg = "请先在医保客户端进行结算!";
            //        return -1;
            //    }
            //    if (iReturn == -1)
            //    {
            //        MyDisconnect();
            //        myInterface = null;
            //        errMsg = "获得医保患者结算信息出错!" + conn.Err;
            //        return -1;
            //    }
            //    balancePatient.SIMainInfo.TotCost += SIPatient.SIMainInfo.TotCost;
            //    balancePatient.SIMainInfo.PubCost += SIPatient.SIMainInfo.PubCost;
            //    balancePatient.SIMainInfo.PayCost += SIPatient.SIMainInfo.PayCost;
            //    balancePatient.SIMainInfo.OwnCost += SIPatient.SIMainInfo.OwnCost;
            //    balancePatient.SIMainInfo.ItemYLCost += SIPatient.SIMainInfo.ItemYLCost;
            //    balancePatient.SIMainInfo.BaseCost += SIPatient.SIMainInfo.BaseCost;
            //    balancePatient.SIMainInfo.ItemPayCost += SIPatient.SIMainInfo.ItemPayCost;
            //    balancePatient.SIMainInfo.PubOwnCost += SIPatient.SIMainInfo.PubOwnCost;
            //    balancePatient.SIMainInfo.OverTakeOwnCost += SIPatient.SIMainInfo.OverTakeOwnCost;
            //    balancePatient.SIMainInfo.HosCost += SIPatient.SIMainInfo.HosCost;
            //}
            #endregion
            //结算结果返回
            patient.SIMainInfo = balancePatient.SIMainInfo;
            return 1;
        }

        private int GetInpatientMidBalanceInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            FS.HISFC.Models.RADT.PatientInfo balancePatient = patient.Clone();
            FS.HISFC.Models.RADT.PatientInfo siPInfo = new FS.HISFC.Models.RADT.PatientInfo();

            FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new InPatient();

            if (this.trans != null)
            {
                myInterface.SetTrans(this.trans);
                feeInpatient.SetTrans(this.trans);
            }

            if (MyConnect() == -1)
            {
                this.errMsg = "连接数据库出错";
                return -1;
            }

            #region 获取医保结算信息
            siPInfo = myInterface.GetSIPersonInfo(balancePatient.ID);
            if (siPInfo == null)
            {
                MyDisconnect();
                this.errMsg = "获得医保基本信息失败";
                return -1;
            }
            balancePatient.SIMainInfo = siPInfo.SIMainInfo;
            int iReturn = 0;
            iReturn = conn.GetBalanceInfo(balancePatient);
            if (iReturn == 0)
            {
                MyDisconnect();
                this.errMsg = "请先在医保客户端进行结算!";
                return -1;
            }
            if (iReturn == -1)
            {
                MyDisconnect();
                this.errMsg = "获得医保患者结算信息出错!" + conn.Err;
                return -1;
            }
            #endregion

            //结算结果返回
            string invoiceNo = patient.SIMainInfo.InvoiceNo;
            string balanceNo = patient.SIMainInfo.BalNo;
            patient.SIMainInfo = balancePatient.SIMainInfo;
            patient.SIMainInfo.InvoiceNo = invoiceNo;
            patient.SIMainInfo.BalNo = balanceNo;
            return 1;
        }

        /// <summary>
        /// 检查金额
        /// </summary>
        private int CheckTotalUploadEqualBalance(FS.HISFC.Models.RADT.PatientInfo balancePatient)
        {
            FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new InPatient();
            if (this.trans != null)
            {
                feeInpatient.SetTrans(this.trans);
            }
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
                FS.HISFC.Models.RADT.PatientInfo SIPatient = new FS.HISFC.Models.RADT.PatientInfo();

                Controls.frmTextInput ucTextInput1 = new frmTextInput();
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucTextInput1);

                if (!ucTextInput1.IsOK)
                {
                    this.errMsg = "如果本地费用与结算费用不符,请查明原因";
                    return -1;
                }

                SIPatient.SIMainInfo.RegNo = ucTextInput1.TextRegNO;
                iReturn = conn.GetBalanceInfo(SIPatient);
                if (iReturn == 0)
                {
                    errMsg = "请先在医保客户端进行结算!";
                    return -1;
                }
                if (iReturn == -1)
                {
                    errMsg = "获得医保患者结算信息出错!" + conn.Err;
                    return -1;
                }
                balancePatient.SIMainInfo.TotCost += SIPatient.SIMainInfo.TotCost;
                balancePatient.SIMainInfo.PubCost += SIPatient.SIMainInfo.PubCost;
                balancePatient.SIMainInfo.PayCost += SIPatient.SIMainInfo.PayCost;
                balancePatient.SIMainInfo.OwnCost += SIPatient.SIMainInfo.OwnCost;
                balancePatient.SIMainInfo.ItemYLCost += SIPatient.SIMainInfo.ItemYLCost;
                balancePatient.SIMainInfo.BaseCost += SIPatient.SIMainInfo.BaseCost;
                balancePatient.SIMainInfo.ItemPayCost += SIPatient.SIMainInfo.ItemPayCost;
                balancePatient.SIMainInfo.PubOwnCost += SIPatient.SIMainInfo.PubOwnCost;
                balancePatient.SIMainInfo.OverTakeOwnCost += SIPatient.SIMainInfo.OverTakeOwnCost;
                balancePatient.SIMainInfo.HosCost += SIPatient.SIMainInfo.HosCost;
            }
            return 1;
        }

        #endregion
    }
}