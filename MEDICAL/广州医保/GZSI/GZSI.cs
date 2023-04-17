using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using GZSI.Controls;
using System.Data;
using GZSI.ApiControls;
using GZSI.ApiClass;
using GZSI.ApiModel;
using System.Xml;

namespace GZSI
{
    class GZSI : FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare
    {
        #region 变量

        private static Management.SIConnect conn = null;
        private string errMsg = "";
        private string errCode = string.Empty;
        private Controls.ucInputRegNo inputRegNo = new Controls.ucInputRegNo();
        private Controls.ucGetSiInHosInfo queryMany = new Controls.ucGetSiInHosInfo();
        private Controls.usSetConnectSqlServer setSql = new Controls.usSetConnectSqlServer();
        private System.Data.IDbTransaction trans = null;
        private DateTime operDate;

        /// <summary>
        /// 医院编码
        /// </summary>
        public const string HosCode = "006181";

        /// <summary>
        /// 判断是否特殊医保系统服务器
        /// </summary>
        private bool isSpeseSiServer = false;

        /// <summary>
        /// 业务日期
        /// </summary>
        DateTime bizDate = new DateTime();

        /// <summary>
        /// 本院医保默认基准合同单位
        /// </summary>
        private string gzybPact_code = "2";

        /// <summary>
        /// 合同单位
        /// </summary>
        string pactCode = string.Empty;

        /// <summary>
        /// 医保Api调用开关设置
        /// </summary>
        private string filePath = @".\profile\SiApiSetting.xml";

        /// <summary>
        /// 是否调用API
        /// </summary>
        private bool isCallApi = false;

        /// <summary>
        /// 是否已载入API配置
        /// </summary>
        bool isCallApiSetting = false;

        /// <summary>
        /// 是否调用API
        /// </summary>
        private bool IsCallApi
        {
            get
            {
                if (!isCallApi && !isCallApiSetting)
                {
                    isCallApi = this.GetApiXmlSetting(this.filePath);
                    isCallApiSetting = true;
                }
                return isCallApi;
            }
        }

        /// <summary>
        /// 合同单位是否可以调用API
        /// </summary>
        private bool isPactCanCallApi = false;

        /// <summary>
        /// 是否特殊合同单位
        /// </summary>
        private bool isSpecailPact = false;

        /// <summary>
        /// 是否需要住院处理的合同单位（从化医保）
        /// </summary>
        private bool isInPatientDealPact = false;

        /// <summary>
        /// API业务类型
        /// </summary>
        private string bizType = "";

        /// <summary>
        /// 是否已经查询 特殊合同单位、api 维护
        /// </summary>
        private bool isSearch = false;

        /// <summary>
        /// 门诊结算时回滚
        /// </summary>
        bool isRoll = false;

        /// <summary>
        /// 是否取消挂号登记
        /// </summary>
        bool isCancelReg = true;

        /// <summary>
        /// API应用服务器地址
        /// </summary>
        private string server = "";

        /// <summary>
        /// API 医院编码
        /// </summary>
        private string hospital_id = "";

        /// <summary>
        /// 挂号临时变量
        /// </summary>
        FS.HISFC.Models.Registration.Register register;

        /// <summary>
        /// 患者信息
        /// </summary>
        private PersonInfo pInfo = new PersonInfo();
        private PersonInfo _pInfo = new PersonInfo();
        private BizInfo bizInfo = new BizInfo();

        /// <summary>
        /// icd字典
        /// </summary>
        ArrayList icdList = new ArrayList();

        public ArrayList IcdList
        {
            get 
            {
                if (icdList.Count == 0)
                {
                    icdList = localMgr.QuerySiDisease();
                }
                return icdList; 
            }
        }

        /// <summary>
        /// icd诊断显示
        /// </summary>
        frmDiagnose frmdiag = null;

        /// <summary>
        /// 未匹配费用总金额
        /// </summary>
        decimal totNoCompareCost = 0;

        /// <summary>
        /// 未匹配提示
        /// </summary>
        StringBuilder sbNoCompare = new StringBuilder();

        /// <summary>
        /// 上传费费用列表
        /// </summary>
        ArrayList alFeeUploads = new ArrayList();

        /// <summary>
        /// 最小费用分类字典
        /// </summary>
        ArrayList alMinFee = new ArrayList();

        /// <summary>
        /// API业务类
        /// </summary>
        GzsiApiBizProcess GzsiApiMgr = new GzsiApiBizProcess();

        /// <summary>
        /// API操作HIS类
        /// </summary>
        ApiManager.LocalManager localMgr = new ApiManager.LocalManager();

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();//常数 

        /// <summary>
        /// 医保接口业务管理类
        /// </summary>
        Management.SILocalManager mySILocalManager = new global::GZSI.Management.SILocalManager();

        /// <summary>
        /// 患者信息管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 是否正式结算，爱博恩非正式结算的时候，不计算医保金额
        /// </summary>
        private bool isBalance = false;

        #endregion

        #region 私有

        /// <summary>
        /// 获取API开关标志
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool GetApiXmlSetting(string filePath)
        {
            //filePath=@".\profile\SiApiSetting.xml";
            if (!System.IO.File.Exists(filePath))
            {
                return false;
            }
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                System.IO.StreamReader sr = new System.IO.StreamReader(filePath, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                xmlDoc.LoadXml(cleandown);
                sr.Close();
                XmlNode rootNode = xmlDoc.SelectSingleNode("//API开关");
                string flag = rootNode.Attributes["开关标志"].Value.Trim().ToString();
                this.server = rootNode.Attributes["应用服务器地址"].Value.Trim().ToString();
                if (this.server != null && this.server.Trim() != "")
                {
                    this.GzsiApiMgr.Server = this.server;
                }
                else
                {
                    this.errMsg = "未配置API应用服务器地址，请联系信息科！";
                }
                this.hospital_id = rootNode.Attributes["医院编码"].Value.Trim().ToString();
                if (this.hospital_id != null && this.hospital_id.Trim() != "")
                {
                    this.GzsiApiMgr.HospitalID = this.hospital_id;
                }
                else
                {
                    this.errMsg = "未配置API医院编码，请联系信息科！";
                }

                if ("1" == flag)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                this.errMsg = "错误：获取API开关标志失败！" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 根据合同单位获取待遇类型
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        private ArrayList GetApiTreatmenttype(string pactCode)
        {
            ArrayList alObj = new ArrayList();

            if (this.trans != null)
            {
                localMgr.SetTrans(this.trans);
            }
            string biz_type = localMgr.GetApiBizType(pactCode);

            if (!string.IsNullOrEmpty(biz_type))
            {
                alObj = localMgr.GetApiTreatmenttype(biz_type);
            }
            return alObj;
        }

        /// <summary>
        /// 设置支付信息
        /// </summary>
        /// <param name="payInfo"></param>
        /// <param name="r"></param>
        private void SetPayInfo(PayInfo payInfo, ref FS.HISFC.Models.Registration.Register r)
        {
            r.SIMainInfo.RegNo = payInfo.serial_no;
            //  r.SIMainInfo.FeeTimes = FS.NFC.Function.NConvert.ToInt32(Reader[1].ToString());
            r.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(payInfo.pay_date);
            r.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.zyzje);
            r.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.sbzfje);
            //r.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.zhzfje);
            r.SIMainInfo.PayCost = 0;
            r.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.bfxmzfje);
            r.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.qfje);
            r.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.grzfje1);
            r.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.grzfje2);
            //r.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.grzfje3);
            r.SIMainInfo.OwnCost = r.SIMainInfo.TotCost - r.SIMainInfo.PubCost;
            r.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.cxzfje);
            r.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.yyfdje);
            r.SIMainInfo.BalanceDate = this.localMgr.GetDateTimeFromSysDateTime();
            r.SIMainInfo.User01 = this.localMgr.GetSysDateTime();

            r.SIMainInfo.TotCost += totNoCompareCost;
            r.SIMainInfo.OwnCost += totNoCompareCost;

            r.SIMainInfo.MedicalType.ID = "3";   //就诊类型 1-住院，2-特定门诊，3-门诊
            r.SIMainInfo.ClinicDiagNose = r.ClinicDiagnose;
            r.SIMainInfo.InDiagnose.ID = r.ClinicDiagnose;
            r.SIMainInfo.TypeCode = "1"; //1-门诊，2-住院
        }

        #endregion

        #region IMedcare 成员

        /// <summary>
        /// 类描述
        /// </summary>
        public string Description
        {
            get
            {
                return "广州医保接口实现";
            }
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrCode
        {
            get
            {
                return errCode;
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
        /// 住院预结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
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
            if (-1 == this.CheckTotalUploadEqualBalance(patient))
            {
                this.errMsg = "总金额不一致，请检查是否漏上传项目或者重复上传项目，谢谢！";
                return -1;
            }
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
        /// 住院结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            FS.HISFC.Models.RADT.PatientInfo balancePatient = patient.Clone();
            FS.HISFC.Models.RADT.PatientInfo siPInfo = new FS.HISFC.Models.RADT.PatientInfo();

            #region 检查参数

            if (balancePatient.SIMainInfo.RegNo == null || balancePatient.SIMainInfo.RegNo == "")
            {
                this.errMsg = "没有该患者的医保信息";
                return -1;
            }

            if (balancePatient.SIMainInfo.InvoiceNo == null || balancePatient.SIMainInfo.InvoiceNo == "")
            {
                // myInterface = null;
                this.errMsg = "主发票号(patient.SIMainInfo.InvoiceNo)没有赋值";
                return -1;
            }

            if (balancePatient.SIMainInfo.BalNo == null || balancePatient.SIMainInfo.BalNo == "")
            {
                //myInterface = null;
                this.errMsg = "结算序号（patient.SIMainInfo.BalNo）没有赋值";
                return -1;
            }
            #endregion

            #region  更新到数据库

            pactCode = patient.Pact.ID;
            if (MyConnect() == -1)
            {
                this.errMsg = "连接数据库出错";
                return -1;
            }
            // myInterface.SetTrans(this.trans);

            balancePatient.SIMainInfo.IsBalanced = true;
            balancePatient.SIMainInfo.IsValid = true;
            balancePatient.SIMainInfo.BalanceDate = this.operDate;

            int iReturn = this.UploadRegInfoInpatient(balancePatient);   // myInterface.UpdateSiMainInfo(balancePatient);
            if (iReturn <= 0)
            {
                MyDisconnect();
                errMsg = "更新医保信息失败!";// +myInterface.Err;
                //myInterface = null;
                return -1;
            }
            // myInterface = null;
            if (conn == null)
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
        /// 门诊预结算
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int PreBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            isBalance = false;

            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }

            if (!isSearch)
            {
                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                    this.localMgr.SetTrans(this.trans);
                }
                isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
                if (!isSpecailPact)
                {
                    isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
                }
                string biz_type = localMgr.GetApiBizType(r.Pact.ID);
                if (!string.IsNullOrEmpty(biz_type))
                {
                    isPactCanCallApi = true;
                    bizType = biz_type;
                }
                this.isSearch = true;//已经查询过

            }

            register = null;
            if (!IsCallApi || !isPactCanCallApi || isSpecailPact || isInPatientDealPact)
            {
                #region 正常流程

                if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
                {
                    this.errMsg = "没有找到患者的就医登记号";
                    return -1;
                }
                pactCode = r.Pact.ID;
                MyConnect();

                if (conn == null)
                {
                    this.errMsg = "没有设置数据库连接!";
                    return -1;
                }
                string invoiceNo = "";
                if (r.SIMainInfo.Memo != null && r.SIMainInfo.Memo != "")
                {
                    //发票号
                    r.SIMainInfo.InvoiceNo = r.SIMainInfo.Memo;
                    invoiceNo = r.SIMainInfo.InvoiceNo;
                }

                #region 医保预结算


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
                    //return 1;
                }

                Management.SILocalManager myInterface = new Management.SILocalManager();
                myInterface.SetTrans(this.trans);
                //爱博恩医院医保报销金额处理为医保账户金额
                #region 医保账户
                decimal amount = r.SIMainInfo.PubCost + r.SIMainInfo.PayCost;
                if (myInterface.SaveAccount(r.PID.CardNO, amount) == -1)
                {
                    return -1;
                }
                #endregion

                #region 本地存储医保收费明细

                DateTime dtNow = myInterface.GetDateTimeFromSysDateTime();
                int iReturn = myInterface.InsertShareData(r, feeDetails, dtNow);
                if (iReturn == -1)
                {
                    this.errMsg = myInterface.Err;
                    return -1;
                }

                r.SIMainInfo.TypeCode = "1";
                r.SIMainInfo.IsBalanced = true;
                iReturn = mySILocalManager.UpdateSiMainInfo(r);
                if (iReturn == -1)
                {
                    this.errMsg = "插入医保登记出错!" + mySILocalManager.Err;
                    return -1;
                }
                if (iReturn == 0)
                {
                    iReturn = mySILocalManager.InsertSIMainInfo(r);
                    if (iReturn <= 0)
                    {
                        this.errMsg = "插入医保登记出错!" + mySILocalManager.Err;
                        return -1;
                    }
                }

                //更新费用明细，结算表与费用明细表关联
                //iReturn = mySILocalManager.UpdateShareData(r);
                //if (iReturn <= 0)
                //{
                //    this.errMsg = "更新医保费用明细信息出错!" + mySILocalManager.Err;
                //    return -1;
                //}

                #endregion

                #endregion
                #endregion
            }
            else
            {
                #region API处理

                if (this.trans != null)
                {
                    localMgr.SetTrans(this.trans);
                    GzsiApiMgr.SetTrans(this.trans);
                }

                //退费
                if (string.IsNullOrEmpty(r.SIMainInfo.Memo))
                {

                    if (r.SIMainInfo.InvoiceNo != "" && r.SIMainInfo.InvoiceNo != null)
                    {
                        FS.FrameWork.Models.NeuObject obj = localMgr.GetSiRegNo(r.ID, r.SIMainInfo.InvoiceNo);
                        if (obj != null && obj.ID != "" && obj.ID != null && obj.Name != null && obj.Name != "")
                        {
                            r.SIMainInfo.RegNo = obj.ID;
                            r.SIMainInfo.ApplyType.ID = obj.Name;
                        }
                        if (r.SIMainInfo.RegNo != null && r.SIMainInfo.RegNo != "")
                        {
                            this.errMsg = "找不到医保主表的登记信息!";
                            return -1;
                        }
                        return 1;
                    }
                }

                //收费
                // 初始化
                r.SIMainInfo.RegNo = "";

                #region 公费处理
                if (bizType == "61")
                {
                    if (r.SSN != "" && r.SSN != null)
                    {
                        r.IDCard = "4|" + r.SSN;
                    }
                    //  r.IDCard = !string.IsNullOrEmpty(r.IDCard) ? r.IDCard : localMgr.GetIDCardByCardNo(r.PID.CardNO);
                    if (conn == null)
                    {
                        this.errMsg = "没有设置数据库连接!";
                        return -1;
                    }
                    // ArrayList alReginfo = conn.GetLastSIRegNo(r.DoctorInfo.SeeDate, r.IDCard);
                    ArrayList alReginfo = new ArrayList();
                    if (alReginfo == null || alReginfo.Count == 0)
                    {
                        #region 选择市公医

                        //C9CEC819-B764-41ae-AA88-6AD05D23A4F9 tang.ll 2015-6-3 广州中医药大学第三附属医院要求单价超过500的项目才弹出是否审批确认界面
                        bool isNeedApprove = false;
                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList tempFeeItemList in feeDetails)
                        {
                            if (tempFeeItemList.Item.Price > 500)
                            {
                                isNeedApprove = true;
                                break;
                            }
                        }

                        if (isNeedApprove)
                        {
                            if (DialogResult.Yes == MessageBox.Show("该市公医是否存在特批项目？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                int iReturn = -1;
                                queryMany.IsSpecialPact = isSpecailPact;
                                queryMany.RegName = r.Name;
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

                                r.ClinicDiagnose = r.SIMainInfo.InDiagnose.ID;

                            }
                            else
                            {

                                r.SIMainInfo.RegNo = "";

                            }
                        }
                        else
                        {
                            r.SIMainInfo.RegNo = "";
                        }
                        #endregion

                    }
                    else
                    {
                        FS.HISFC.Models.Registration.Register regObj = alReginfo[0] as FS.HISFC.Models.Registration.Register;
                        r.SIMainInfo.RegNo = regObj.SIMainInfo.RegNo;
                        r.ClinicDiagnose = regObj.SIMainInfo.InDiagnose.ID;
                    }
                }
                #endregion

                totNoCompareCost = 0;
                alFeeUploads = new ArrayList();
                if (-1 == this.GetUserCode(feeDetails, out totNoCompareCost, out alFeeUploads))
                {
                    this.errMsg = "转换医保码失败!";
                    return -1;
                }
                if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "" || r.SIMainInfo.RegNo.Length <= 10)
                {
                    #region  PJ3系统未挂号
                    //输入诊断编号 HIS诊断与医保的诊断码不一致，先屏蔽
                    if (string.IsNullOrEmpty(r.ClinicDiagnose)&& bizType!="13")
                    {
                        ArrayList alDiay = null;
                        FS.HISFC.Models.HealthRecord.Diagnose diagnose = null;
                        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                        try
                        {
                            alDiay = diagManager.QueryCaseDiagnoseForClinic(r.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                            if (alDiay.Count > 0)
                            {
                                //优先找主要诊断
                                foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiay)
                                {
                                    if (diag.MainFlag == "1")
                                    {
                                        diagnose = diag;
                                        break;
                                    }
                                }

                                if (diagnose == null || diagnose.DiagInfo == null && string.IsNullOrEmpty(diagnose.DiagInfo.ICD10.ID))
                                {
                                    diagnose = alDiay[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                                }

                                r.ClinicDiagnose = diagnose.DiagInfo.ICD10.ID;
                                //判断是否是标准ICD10,暂时简单判断
                                if (r.ClinicDiagnose == "MS999")
                                {
                                    r.ClinicDiagnose = "";

                                    //生育门诊默认为【产前筛查】
                                    if (bizType == "51")
                                    {
                                        r.ClinicDiagnose = "Z36.900";
                                    }
                                    else
                                    {
                                        if (frmdiag == null)
                                        {
                                            frmdiag = new frmDiagnose();
                                            frmdiag.AlIcdlist = IcdList;
                                        }
                                        frmdiag.RegObj = r;
                                        frmdiag.ShowDialog();

                                        if (string.IsNullOrEmpty(r.ClinicDiagnose))
                                        {
                                            this.errMsg = "主要诊断不是标准ICD10码，请输入门诊诊断！";
                                            return -1;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                r.ClinicDiagnose = "";

                                //生育门诊默认为【产前筛查】
                                if (bizType == "51")
                                {
                                    r.ClinicDiagnose = "Z36.900";
                                }
                                else
                                {
                                    if (frmdiag == null)
                                    {
                                        frmdiag = new frmDiagnose();
                                        frmdiag.AlIcdlist = IcdList;
                                    }
                                    frmdiag.RegObj = r;
                                    frmdiag.ShowDialog();

                                    if (string.IsNullOrEmpty(r.ClinicDiagnose))
                                    {
                                        this.errMsg = "主要诊断不是标准ICD10码，请输入门诊诊断！";
                                        return -1;
                                    }
                                }

                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("获得患者的诊断信息出错！" + ex.Message, "提示");
                            return -1;
                        }

                    }

                    pInfo = new PersonInfo();

                    r.IDCard = !string.IsNullOrEmpty(r.IDCard) ? r.IDCard : localMgr.GetIDCardByCardNo(r.PID.CardNO);

                    //身份证为空
                    if (string.IsNullOrEmpty(r.IDCard))
                    {
                        frmSearch frmsea = new frmSearch();
                        frmsea.RegInfo = r;
                        frmsea.ShowDialog();
                    }

                    //系统为挂号，按当前日期作为API的业务时间
                    bizDate = localMgr.GetDateTimeFromSysDateTime();
                    ArrayList alPersonInfo = GzsiApiMgr.GetSiPersonInfo(r.IDCard, bizType, bizDate);
                    if (alPersonInfo == null || alPersonInfo.Count == 0)
                    {

                        MyDisconnect();
                        this.errMsg = this.GetLocalPerInfo(r.IDCard) + "\n医保API的返回信息!" + GzsiApiMgr.ErrMsg;

                        if (DialogResult.Yes == MessageBox.Show(this.errMsg + "\n 是否重新查询该患者的医保人员信息？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                        {
                            frmSearch frmsea = new frmSearch();
                            frmsea.RegInfo = r;
                            frmsea.ShowDialog();

                            alPersonInfo = GzsiApiMgr.GetSiPersonInfo(r.IDCard, bizType, bizDate);

                            if (alPersonInfo == null || alPersonInfo.Count == 0)
                            {
                                MyDisconnect();
                                this.errMsg = "医保API获取医保患者信息出错!" + GzsiApiMgr.ErrMsg;
                                return -1;
                            }
                            _pInfo = alPersonInfo[0] as PersonInfo;
                            if (_pInfo != null)
                            {
                                if (_pInfo.Api_name.Trim() != r.Name.Trim())
                                {
                                    this.errMsg = "获取医保患者信息姓名与his系统患者挂号姓名不同，请查对!" + GzsiApiMgr.ErrMsg;
                                    return -1;
                                }
                            }
                        }
                        else
                        {
                            this.errMsg = this.GetLocalPerInfo(r.IDCard) + "\n 未找到人员信息！";
                            return -1;
                        }


                    }

                    //直接在后台注册判断患者哪一条信息为正常参保，不让操作人员选择。 pengc 2017-11-03 {BFE1E4D1-AFF1-42bd-98AB-99225CAE395D}
                    if (alPersonInfo.Count > 1)
                    {
                        //单个人有多条信息
                        ArrayList alMyPersonInfo = alPersonInfo;
                        bool flag = false;
                        foreach (PersonInfo personInfo in alMyPersonInfo)
                        {
                            r.IDCard = "2|" + personInfo.Api_indi_id;
                            alPersonInfo = GzsiApiMgr.GetSiPersonInfo(r.IDCard, bizType, bizDate);
                            if (alPersonInfo != null && alPersonInfo.Count != 0)
                            {
                                pInfo = alPersonInfo[0] as PersonInfo;
                                if (pInfo.Api_indi_freeze_status.Equals("0"))
                                {
                                    flag = true;
                                    break;
                                }

                            }
                        }
                        if (!flag)
                        {
                            MessageBox.Show("医保API获取医保患者信息出错!" + GzsiApiMgr.ErrMsg);
                            return -1;
                        }
                        //if (objfrm.Person != null && objfrm.Person.Api_indi_id != "" && objfrm.Person.Api_indi_id != null)
                        //{
                        //    PersonInfo personInfo = objfrm.Person as PersonInfo;
                        //    r.IDCard = "2|" + personInfo.Api_indi_id;
                        //    alPersonInfo = GzsiApiMgr.GetSiPersonInfo(r.IDCard, bizType, bizDate);
                        //    if (alPersonInfo == null || alPersonInfo.Count == 0)
                        //    {
                        //       // MessageBox.Show("医保API获取医保患者信息出错!" + GzsiApiMgr.ErrMsg);
                        //        //goto ShowPersonInfo;

                        //    }
                        //    // pInfo = alPersonInfo[0] as PersonInfo;
                        //}
                        //else
                        //{
                        //    this.errMsg = "未选择患者参保信息!";
                        //    return -1;
                        //}

                    }

                    PayInfo payInfo = new PayInfo();
                    if (alPersonInfo.Count == 1)
                    {
                        pInfo = alPersonInfo[0] as PersonInfo;

                        FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                        obj = conMgr.GetConstant("PrintAbbreviation", r.PID.CardNO.ToString());
                        string regname = "";
                        regname = r.Name;
                        if (obj != null && obj.Memo.Length > 20)
                        {
                            r.Name = obj.Memo;
                        }
                        //判断姓名是否一致
                        if (pInfo.Api_name != r.Name)
                        {
                            this.errMsg = "挂号患者姓名: " + r.Name + " API返回医保患者姓名: " + pInfo.Api_name + " 不一致！";
                            pInfo = null;
                            return -1;
                        }
                        if (!string.IsNullOrEmpty(regname) && regname.Trim() != "")
                        {
                            r.Name = regname;
                        }

                        if (string.IsNullOrEmpty(r.ClinicDiagnose))
                        {
                            this.errMsg = "入院诊断为空！";
                            return -1;
                        }
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正上传并计算费用，请稍等...");
                        Application.DoEvents();


                        if (-1 == GzsiApiMgr.SiRegisterAndClinicFee(bizType, pInfo, ref r, alFeeUploads, ref payInfo, "1"))
                        {
                            MyDisconnect();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.errMsg = "医保API的挂号并收费出错!\n 错误信息：" + GzsiApiMgr.ErrMsg;
                            return -1;
                        }

                        this.SetPayInfo(payInfo, ref r);
                        register = r;

                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    }
                    else
                    {
                        MessageBox.Show("本次收费未走API，请联系信息科！谢谢");
                    }

                    #endregion
                }
                else
                {
                    #region PJ3系统已经挂号，医保主表不存在数据
                    bizInfo = new BizInfo();
                    //直接计算（API函数调用）
                    if (this.trans != null)
                    {
                        GzsiApiMgr.SetTrans(this.trans);
                    }
                    if (bizType == "61")
                    {
                        r.SIMainInfo.ApplyType.ID = "610";//暂时写死
                    }

                    if (-1 == GzsiApiMgr.GetSiRegisterInfo(r.SIMainInfo.RegNo, r.SIMainInfo.ApplyType.ID, ref bizInfo))
                    {
                        this.errMsg = "API调用，获取门诊挂号业务信息错误!\n 错误信息：" + GzsiApiMgr.ErrMsg;
                        return -1;
                    }
                    PayInfo payInfo = new PayInfo();
                    if (r.SIMainInfo.ApplyType.ID.Substring(0, 2) == "61")
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正上传并计算费用，请稍等...");
                        Application.DoEvents();

                        //公医需审批的患者，先PJ3系统先挂号，再收费
                        bizInfo.in_dept = r.DoctorInfo.Templet.Dept.ID;
                        bizInfo.in_dept_name = r.DoctorInfo.Templet.Dept.Name;
                        bizInfo.reg_date = r.DoctorInfo.SeeDate.ToString();
                        if (-1 == GzsiApiMgr.SiClinicFee(bizInfo, alFeeUploads, ref payInfo, "1"))
                        {
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            this.errMsg = "API调用，校验计算并保存费用信息错误! \n 错误信息：" + GzsiApiMgr.ErrMsg;
                            return -1;
                        }
                    }

                    this.SetPayInfo(payInfo, ref r);
                    r.SIMainInfo.ApplyType.User03 = "1";//已经登记挂号
                    register = r;

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                    #endregion
                }

                #endregion
            }
            return 1;
        }

        /// <summary>
        /// 门诊结算
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            if (!isSearch)
            {
                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                    this.localMgr.SetTrans(this.trans);
                }
                isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
                if (!isSpecailPact)
                {
                    isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
                }
                string biz_type = localMgr.GetApiBizType(r.Pact.ID);
                if (!string.IsNullOrEmpty(biz_type))
                {
                    isPactCanCallApi = true;
                    bizType = biz_type;
                }
                this.isSearch = true;//已经查询过 
            }

            if (!IsCallApi ||!isPactCanCallApi|| isSpecailPact || isInPatientDealPact)
            {
                return 1;
            }
            else
            {
                #region API处理
                //在prebalanceoutpatient里面已经进行了结算，此处不再结算

                //if (alFeeUploads ==null || alFeeUploads.Count == 0)
                //{
                //    totNoCompareCost = 0;
                //    alFeeUploads = new ArrayList();
                //    if (-1 == GetUserCode(feeDetails, out totNoCompareCost, out alFeeUploads))
                //    {
                //        this.errMsg = "转换医保码失败!";
                //        return -1;
                //    }
                //}


                //PayInfo payInfo = new PayInfo();
                //isCancelReg = true;

                //if (this.bizInfo != null && !string.IsNullOrEmpty(bizInfo.serial_no))
                //{
                //    //门诊收费
                //    if (r.SIMainInfo.ApplyType.ID.Substring(0, 2) == "61")
                //    {
                //        isCancelReg = false;
                //        //公医需审批的患者，先PJ3系统先挂号，再收费

                //        bizInfo.in_dept = r.DoctorInfo.Templet.Dept.ID;
                //        bizInfo.in_dept_name = r.DoctorInfo.Templet.Dept.Name;
                //        bizInfo.reg_date = r.DoctorInfo.SeeDate.ToString();
                //        if (-1 == GzsiApiMgr.SiClinicFee(bizInfo, alFeeUploads, ref payInfo, "1"))
                //        {
                //            this.errMsg = "API调用，校验计算并保存费用信息错误! \n 错误信息：" + GzsiApiMgr.ErrMsg;
                //            return -1;
                //        }
                //    }

                //}
                //else
                //{
                //    //门诊挂号并收费
                //    if (pInfo == null || string.IsNullOrEmpty(pInfo.Api_indi_id))
                //    {
                //        ArrayList alPersonInfo = GzsiApiMgr.GetSiPersonInfo(r.IDCard, bizType, (bizDate==DateTime.MinValue?r.DoctorInfo.SeeDate:bizDate));
                //        if (alPersonInfo.Count == 1)
                //        {
                //            pInfo = alPersonInfo[0] as PersonInfo;

                //        }
                //        else
                //        {
                //            // 暂时不处理
                //        }
                //    }

                //    if (-1 == GzsiApiMgr.SiRegisterAndClinicFee(bizType, pInfo, ref r, alFeeUploads, ref payInfo, "1"))
                //    {
                //        MyDisconnect();
                //        this.errMsg = "医保API的挂号函数出错!\n 错误信息：" + GzsiApiMgr.ErrMsg;
                //        return -1;
                //    }
                //}

                //r.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.zyzje);
                //r.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(payInfo.sbzfje);
                //r.SIMainInfo.OwnCost = r.SIMainInfo.TotCost - r.SIMainInfo.PubCost;
                //r.SIMainInfo.PayCost = 0;

                #endregion

                #region 操作HIS 医保主表库

                string[] idcards = r.IDCard.Split('|');
                if (idcards.Length > 1)
                {
                    r.IDCard = idcards[1];
                }
                r.SSN = r.SIMainInfo.RegNo;
                r.SIMainInfo.IsValid = true;
                r.SIMainInfo.IsBalanced = true;

                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                }
                int iReturn = mySILocalManager.InsertSIMainInfo(r);
                if (iReturn <= 0)
                {
                    MyDisconnect();
                    isRoll = true;
                    CancelBalanceOutpatient(r, ref feeDetails);
                    this.errMsg = "插入医保登记出错!" + mySILocalManager.Err;
                    return -1;
                }

                //为了不影响已稳定版本
                //直接更新 待遇类型
                if (!string.IsNullOrEmpty(r.SIMainInfo.ApplyType.ID))
                {
                    if (-1 == localMgr.UpdateApiTreamType(r.ID, r.SIMainInfo.RegNo, r.SIMainInfo.ApplyType.ID))
                    {
                        MyDisconnect();
                        isRoll = true;
                        CancelBalanceOutpatient(r, ref feeDetails);
                        this.errMsg = "更新 待遇类型出错!" + localMgr.Err;
                        return -1;
                    }
                }
                //为了不影响已稳定版本 xhl 2017-04-29 字段后期再维护
                //{249DC040-7C55-4a7e-BAB8-6FF56FF985E5}
                //直接更新工伤就医凭证号
                if (!string.IsNullOrEmpty(r.SIMainInfo.User02)) 
                {
                    if (-1 == localMgr.UpdateApiIDNO_GS(r.ID, r.SIMainInfo.RegNo, r.SIMainInfo.User02.ToString()))
                    {
                        MyDisconnect();
                        isRoll = true;
                        CancelBalanceOutpatient(r, ref feeDetails);
                        this.errMsg = "更新工伤就医凭证号出错!" + localMgr.Err;
                        return -1;
                    }
                }

                string jsid = string.Empty;
                if ( localMgr.InsertMZJSData(r, ref jsid) < 1)
                {
                    MyDisconnect();
                    isRoll = true;
                    CancelBalanceOutpatient(r, ref feeDetails);
                    this.errMsg = "插入本地结算信息出错!" + localMgr.Err;
                    return -1;
                }

                DateTime dtNow = localMgr.GetDateTimeFromSysDateTime();
                //插入上传的费用
                iReturn = localMgr.InsertMZXMData(r, this.alFeeUploads, dtNow, jsid);
                if (iReturn == -1)
                {
                    MyDisconnect();
                    isRoll = true;
                    CancelBalanceOutpatient(r, ref feeDetails);
                    this.errMsg = "插入本地结算明细信息出错!" + localMgr.Err;
                    return -1;
                }

                #endregion
            }
            return 1;
        }

        /// <summary>
        /// 取消住院结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CancelBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            //Management.SILocalManager interfaceMgr = new Management.SILocalManager();
            //if (this.trans != null)
            //{
            //    interfaceMgr.SetTrans(this.trans);
            //}
            //FS.HISFC.Models.RADT.PatientInfo tempPatient = new FS.HISFC.Models.RADT.PatientInfo();
            //tempPatient = interfaceMgr.GetSIPersonInfo(patient.ID, patient.SIMainInfo.InvoiceNo);
            //if (tempPatient == null || tempPatient.ID == "")
            //{
            //    return -1;
            //}

            return 1;
        }

        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            if (!isSearch)
            {
                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                    this.localMgr.SetTrans(this.trans);
                }
                isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
                if (!isSpecailPact)
                {
                    isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
                }
                string biz_type = localMgr.GetApiBizType(r.Pact.ID);
                if (!string.IsNullOrEmpty(biz_type))
                {
                    isPactCanCallApi = true;
                    bizType = biz_type;
                }
                this.isSearch = true;//已经查询过
            }

            //可以做取消结算
            if (IsCallApi && isPactCanCallApi && !isSpecailPact && !isInPatientDealPact)
            {
                if (this.trans != null)
                {
                    this.GzsiApiMgr.SetTrans(this.trans);
                }
                //DateTime dtNow = GzsiApiMgr.GetDateTimeFromSysDateTime();
                //if (r.DoctorInfo.SeeDate.Date.AddDays(3) < dtNow.Date)
                //{
                //    if (DialogResult.No == MessageBox.Show("医保PJ3已经超过3天有效期，请确保医保局已经开锁，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                //    {
                //        this.errMsg = "PJ3系统费用信息已过有效期，医保局尚未开锁";
                //        return -1;
                //    }
                //}
                //if (r.DoctorInfo.SeeDate.Month != dtNow.Month)
                //{
                //    this.errMsg = "PJ3系统费用信息已经跨月，不允许退费操作，请进行手工退费！";
                //    return -1;
                //}
                if (register != null && !string.IsNullOrEmpty(register.ID))
                {
                    //API收费时回滚
                    isRoll = true;
                }

                if (!isRoll)
                {

                    FS.FrameWork.Models.NeuObject obj = localMgr.GetSiRegNo(r.ID, r.SIMainInfo.InvoiceNo);
                    if (obj != null && obj.ID != null && obj.ID != "" && obj.Name != null && obj.Memo != null && obj.Memo != "")
                    {
                        r.SIMainInfo.RegNo = obj.ID;
                        r.SIMainInfo.ApplyType.ID = obj.Name;
                        r.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(obj.Memo);
                    }
                    if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.errMsg = "获取医保主表信息出错！" + localMgr.Err;
                        return -1;
                    }

                }

                if (this.localMgr.DisableMZJSData(r.SIMainInfo.RegNo) < 0)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.errMsg = "更新本地医保结算表信息出错！" + localMgr.Err;
                    return -1;
                }

                if (r.SIMainInfo.ApplyType.ID == null || r.SIMainInfo.ApplyType.ID == "")
                {
                    if (DialogResult.No == MessageBox.Show("非API退费,医保患者是否已经取消了医保客户端的结算信息？", "非API退费", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
                    {
                        MessageBox.Show("请先在医保客户端取消结算信息!");
                        return -1;
                    }
                    return 1;
                }

                if (isRoll)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在回滚API结算信息，请稍等...");
                    Application.DoEvents();
                }
                else
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行API退费处理，请稍等...");
                    Application.DoEvents();

                }

                long feeTimes = 0;
                BizInfo bizInfo = new BizInfo();
                // ArrayList alFeeInfo = null;

                FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
                obj1 = conMgr.GetConstant("PrintAbbreviation", r.PID.CardNO.ToString());
                string regnamenew = "";
                regnamenew = r.Name;
                if (obj1 != null && obj1.Memo.Length > 20)
                {
                    r.Name = obj1.Memo;
                }

                ArrayList al = GzsiApiMgr.GetSiClinicFeeInfo(r, r.SIMainInfo.ApplyType.ID, "0", ref feeTimes, ref bizInfo);
                if (al == null || al.Count == 0)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.errMsg = "广州医保API退费操作中，获取费用信息出错！" + GzsiApiMgr.ErrMsg;
                    return -1;
                }
                if (!string.IsNullOrEmpty(regnamenew) && regnamenew.Trim() != "")
                {
                    r.Name = regnamenew;
                }
                //if (feeTimes > 1)
                //{
                //    //多个费用批次

                //    //弹出费用批次
                //    ucFeeBatch ucfeeBatch = new ucFeeBatch();
                //    ucfeeBatch.AlFeeBatch = al;
                //    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucfeeBatch);
                //    if (ucfeeBatch.FeeBatch != null && !string.IsNullOrEmpty(ucfeeBatch.FeeBatch.fee_batch))
                //    {
                //        FeeBatchInfo feeBath = ucfeeBatch.FeeBatch;
                //        alFeeInfo = GzsiApiMgr.GetSiClinicFeeInfo(r, r.SIMainInfo.ApplyType.ID, feeBath.fee_batch, ref feeTimes, ref bizInfo);

                //    }
                //    else
                //    {
                //        this.errMsg = "没有选择费用批次";
                //        return -1;
                //    }

                //}
                if (feeTimes > 1)
                {
                    //多个费用批次
                    for (int i = al.Count - 1; i >= 0; --i)
                    {
                        FeeBatchInfo feeBath = al[i] as FeeBatchInfo;
                        if (FS.FrameWork.Function.NConvert.ToDecimal(feeBath.sum_fee) == r.SIMainInfo.TotCost)
                        {
                            al = null;
                            al = GzsiApiMgr.GetSiClinicFeeInfo(r, r.SIMainInfo.ApplyType.ID, feeBath.fee_batch, ref feeTimes, ref bizInfo);
                            break;
                        }
                    }

                }

                //费用信息列表
                if (al == null || al.Count == 0)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    this.errMsg = GzsiApiMgr.ErrMsg;
                    return -1;
                }
                PayInfo payInfo = new PayInfo();
                string bizType = r.SIMainInfo.ApplyType.ID.Substring(0, 2);
                if (bizType == "11" || bizType == "61")
                {
                    if (-1 == GzsiApiMgr.CancleSiClinicFee(bizInfo, al, ref payInfo))
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.errMsg = "广州医保API退费操作中,门诊退费(普通、门诊)出错！" + GzsiApiMgr.ErrMsg;
                        return -1;
                    }
                    if (!isRoll)
                    {
                        MessageBox.Show("医保客户端已经退费，该过程不可逆！", "已退费提示");
                    }
                }
                else if (bizType == "41")//取消工伤收费 xhl 2017-04-29  //{249DC040-7C55-4a7e-BAB8-6FF56FF985E5}
                {
                    if (-1 == GzsiApiMgr.CancleSiClinicFeeGS(bizInfo, al, ref payInfo))
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.errMsg = "广州医保API退费操作中,门诊退费(工伤门诊)出错！" + GzsiApiMgr.ErrMsg;
                        return -1;
                    }
                    if (!isRoll)
                    {
                        MessageBox.Show("医保客户端已经退费，该过程不可逆！", "已退费提示");
                    }
                }
                else
                {
                    if (-1 == GzsiApiMgr.CancleTsSiClinicFee(bizInfo, al, ref payInfo))
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.errMsg = "广州医保API退费操作中,门诊退费（工伤、生育、门慢）出错！" + GzsiApiMgr.ErrMsg;
                        return -1;
                    }
                }
                //退费完成，置空
                bizInfo = null;

                if (isCancelReg)
                {
                    if (bizType == "61")
                    {
                        if (isRoll && r.SIMainInfo.ApplyType.User03 == "1")
                        {
                            //存在特批项目的直接不取消公医挂号
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            return 1;
                        }
                        if (!isRoll)
                        {
                            if (DialogResult.No == MessageBox.Show("取消公医挂号，将取消该就医登记号存在的特批项目 \n\n 是否取消该市公医挂号记录？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                return 1;
                            }
                        }

                    }
                    if (-1 == GzsiApiMgr.CancleSiRegister(r))
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.errMsg = "广州医保API取消门诊挂号出错！" + GzsiApiMgr.ErrMsg;
                        return -1;
                    }
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            #region 前置机模式
            else
            {
                //取消账户金额
                //暂时采用手工取现处理
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 半退操作
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            //
            return 1;
        }

        /// <summary>
        /// 取消住院登记信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        /// <summary>
        /// 取消门诊登记信息
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            if (!isSearch)
            {
                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                }
                isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
                if (!isSpecailPact)
                {
                    isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
                }
                string biz_type = localMgr.GetApiBizType(r.Pact.ID);
                if (!string.IsNullOrEmpty(biz_type))
                {
                    isPactCanCallApi = true;
                    bizType = biz_type;
                }
                this.isSearch = true;//已经查询过
            }


            if (IsCallApi &&isPactCanCallApi&& !isSpecailPact && !isInPatientDealPact)
            {
                if (this.trans != null)
                {
                    GzsiApiMgr.SetTrans(this.trans);
                }
                if (-1 == GzsiApiMgr.CancleSiRegister(r))
                {
                    this.errMsg += " 广州医保API取消门诊挂号出错！" + GzsiApiMgr.ErrMsg;
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 删除住院上传费用明细
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        /// <summary>
        /// 删除门诊上传费用明细
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
          
            pactCode = r.Pact.ID;

            if (!isSearch)
            {
                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                    this.localMgr.SetTrans(this.trans);
                }
                isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
                if (!isSpecailPact)
                {
                    isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
                }
                string biz_type = localMgr.GetApiBizType(r.Pact.ID);
                if (!string.IsNullOrEmpty(biz_type))
                {
                    isPactCanCallApi = true;
                    bizType = biz_type;
                }
                this.isSearch = true;//已经查询过
            }

            if (!IsCallApi ||!isPactCanCallApi|| isSpecailPact || isInPatientDealPact)
            {
                if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
                {
                    this.errMsg = "没有找到患者的就医登记号";
                    return -1;
                }
                MyConnect();
                if (conn == null)
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
                if (!isSpecailPact && !isInPatientDealPact)
                {
                    if (conn.DeleteItemListClinic(r.SIMainInfo.RegNo) == -1)
                    {
                        Disconnect();
                        return -1;
                    }
                    if (conn.DeleteItemListIndicationsClinic(r.SIMainInfo.RegNo) == -1)
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
            return 1;
        }

        /// <summary>
        /// 删除患者住院所有上传费用明细
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        /// <summary>
        /// 删除患者门诊所有上传费用明细
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
           
            pactCode = r.Pact.ID;

            if (!isSearch)
            {
                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                    this.localMgr.SetTrans(this.trans);
                }
                isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
                if (!isSpecailPact)
                {
                    isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
                }
                string biz_type = localMgr.GetApiBizType(r.Pact.ID);
                if (!string.IsNullOrEmpty(biz_type))
                {
                    isPactCanCallApi = true;
                    bizType = biz_type;
                }
                this.isSearch = true;//已经查询过
            }
            if (!IsCallApi ||!isPactCanCallApi|| isSpecailPact || isInPatientDealPact)
            {
                if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
                {
                    this.errMsg = "没有找到患者的就医登记号";
                    return -1;
                }

                MyConnect();

                if (conn == null)
                {
                    this.errMsg = "没有设置数据库连接!";
                    return -1;
                }

                if (!isSpecailPact && !isInPatientDealPact)
                {
                    if (conn.DeleteItemListClinic(r.SIMainInfo.RegNo) == -1)
                    {
                        MyDisconnect();
                        return -1;
                    }
                    if (conn.DeleteItemListIndicationsClinic(r.SIMainInfo.RegNo) == -1)
                    {
                        MyDisconnect();
                        return -1;
                    }
                    // MyDisconnect();
                    return 1;
                }
                else
                {
                    if (conn.DeleteItemList(r.SIMainInfo.RegNo) == -1)
                    {
                        MyDisconnect();
                        return -1;
                    }
                    // MyDisconnect();
                    return 1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 删除部分住院上传费用明细
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        /// <summary>
        /// 删除部分门诊上传费用明细
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            if (r.ID == null || r.ID == "")
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
            pactCode = r.Pact.ID;

            if (!isSearch)
            {
                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                    this.localMgr.SetTrans(this.trans);
                }
                isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
                if (!isSpecailPact)
                {
                    isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
                }
                string biz_type = localMgr.GetApiBizType(r.Pact.ID);
                if (!string.IsNullOrEmpty(biz_type))
                {
                    isPactCanCallApi = true;
                    bizType = biz_type;
                }
                this.isSearch = true;//已经查询过
            }
            if (!IsCallApi ||!isPactCanCallApi|| isSpecailPact || isInPatientDealPact)
            {
                if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
                {
                    this.errMsg = "没有找到患者的就医登记号";
                    return -1;
                }

                MyConnect();

                if (conn == null)
                {
                    this.errMsg = "没有设置数据库连接!";
                    return -1;
                }
                if (!isSpecailPact && !isInPatientDealPact)
                {
                    if (conn.DeleteItemListClinic(r.SIMainInfo.RegNo) == -1)
                    {
                        this.MyDisconnect();
                        return -1;
                    }
                    if (conn.DeleteItemListIndicationsClinic(r.SIMainInfo.RegNo) == -1)
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
            return 1;
        }

        /// <summary>
        /// 获取住院登记信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (string.IsNullOrEmpty(patient.SIMainInfo.RegNo) || patient.Insurance.Memo.Trim() == "重选患者")
            {
                ////医保登记号为空
                pactCode = patient.Pact.ID;
                this.MyConnect();
                if (conn == null)
                {
                    this.errMsg = "没有设置数据库连接!";
                    return -1;
                }

                Management.SILocalManager myInterface = new Management.SILocalManager();
                if (this.trans != null)
                {
                    myInterface.SetTrans(this.trans);
                }
                //queryMany.Init(Controls.ucGetSiClinicInfocs.PatientType.OutPatient);
                queryMany.IsSpecialPact = myInterface.IsPactDealByInpatient(patient.Pact.ID);
                //queryMany.RegInfos = null;
                DateTime dtDefault = new DateTime(1900, 1, 1, 0, 0, 0);
                if (patient.PVisit.InTime.Date > dtDefault)
                {
                    queryMany.DtPatientIn = patient.PVisit.InTime.Date;
                }
                queryMany.QueryPatientType = ucGetSiInHosInfo.PatientType.InPatient;
                queryMany.RegName = patient.Name;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(queryMany);
                if (queryMany.RegNo == null)
                {
                    this.errMsg = "没有选取患者医保挂号信息";
                    MyDisconnect();
                    return -1;
                }
                //--------------------------------------------------
                //这个赋值在5.0中不适用，改成明细赋值 2012年7月23日16:39:10
                //patient.SIMainInfo = queryMany.PersonInfo.SIMainInfo;
                patient.SIMainInfo.RegNo = queryMany.PersonInfo.SIMainInfo.RegNo;
                patient.SIMainInfo.HosNo = queryMany.PersonInfo.SIMainInfo.HosNo;
                patient.SIMainInfo.EmplType = queryMany.PersonInfo.SIMainInfo.EmplType;
                patient.SIMainInfo.InDiagnose.Name = FS.FrameWork.Public.String.TakeOffSpecialChar(queryMany.PersonInfo.SIMainInfo.InDiagnose.Name, "'", "(", ")", "%", ";");
                patient.SIMainInfo.InDiagnose.ID = queryMany.PersonInfo.SIMainInfo.InDiagnose.ID;
                patient.SIMainInfo.AppNo = queryMany.PersonInfo.SIMainInfo.AppNo;
                patient.SIMainInfo.IsValid = true;
                patient.SIMainInfo.TypeCode = "2";
                //----------------------------------------------------
                patient.IDCard = queryMany.PersonInfo.IDCard;
                string priorName = patient.Name;
                patient.Name = queryMany.PersonInfo.Name;
                //patient.Pact.ID = "2";
                patient.CompanyName = queryMany.PersonInfo.CompanyName;
                //patient.PVisit = queryMany.PersonInfo.PVisit;
                patient.Birthday = queryMany.PersonInfo.Birthday;
                patient.Sex = queryMany.PersonInfo.Sex;

                if (!string.IsNullOrEmpty(queryMany.RegName) && priorName != null && priorName != "" && priorName != queryMany.PersonInfo.Name)
                {
                   if (MessageBox.Show("所选医保患者名字和在院登记名字不一致,请检查选择是否正确并继续?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return -1;
                    }
                }
                if (!string.IsNullOrEmpty(patient.ID))
                {
                    patient.SIMainInfo.IsBalanced = false;
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
                }

            }
            return 1;
        }

        /// <summary>
        /// 获取门诊登记信息
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {

            pactCode = r.Pact.ID;

            int iReturn = 0;
            if (this.trans != null)
            {
                mySILocalManager.SetTrans(this.trans);
                localMgr.SetTrans(this.trans);
            }
            this.isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
            if (!isSpecailPact)
            {
                this.isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
            }
            string biz_type = localMgr.GetApiBizType(r.Pact.ID);
            if (!string.IsNullOrEmpty(biz_type))
            {
                isPactCanCallApi = true;
                bizType = biz_type;
            }
            this.isSearch = true;//已经查询过

            /////////////////////////

            if (!IsCallApi ||!isPactCanCallApi|| isSpecailPact || isInPatientDealPact)
            {
                #region 正常流程
                //退费
                if (!string.IsNullOrEmpty(r.SIMainInfo.InvoiceNo))
                {
                    r.SIMainInfo.RegNo = localMgr.GetSiRegNo(r.ID);
                    if (string.IsNullOrEmpty(r.SIMainInfo.RegNo))
                    {
                        this.errMsg = "找不到医保主表的登记信息!";
                        return -1;
                    }
                    if (DialogResult.No == MessageBox.Show("医保患者是否已经取消了医保客户端的结算信息？", "是否继续", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        MessageBox.Show("请先在医保客户端取消结算信息!");
                        return -1;
                    }
                    return 1;
                }

                this.MyConnect();

                if (conn == null)
                {
                    this.errMsg = "没有设置数据库连接!";
                    return -1;
                }

                queryMany.IsSpecialSiServer = this.isSpeseSiServer;

                queryMany.RegName = r.Name;

                queryMany.QueryPatientType = ucGetSiInHosInfo.PatientType.OutPatient;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(queryMany);
                if (string.IsNullOrEmpty(queryMany.RegNo))
                {
                    MyDisconnect();
                    this.ClearRegInfoOutpatient();
                    this.errMsg = "没有选取患者医保挂号信息";
                    return -1;
                }
                if (!queryMany.IsSpecialPact && !queryMany.IsInPatientDealPact)
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
                    this.ClearRegInfoOutpatient();
                    this.errMsg = "查询挂号信息出错!" + conn.Err;
                    return -1;
                }

                r.SSN = r.SIMainInfo.RegNo;
                r.SIMainInfo.IsValid = true;
                r.SIMainInfo.IsBalanced = false;

                string priorName = r.Name.Trim();
                if (priorName != null && priorName != "" && priorName != queryMany.PersonInfo.Name.Trim())
                {
                    this.ClearRegInfoOutpatient();
                    MessageBox.Show("所选医保患者名字和挂号登记名字不一致,请检查选择是否正确!");
                    return -1;
                }
            
                MyDisconnect();

                #endregion
            }
            else
            {
                return 1;
            }
            string[] idcards = r.IDCard.Split('|');
            if (idcards.Length > 1)
            {
                r.IDCard = idcards[1];
            }
            r.SSN = r.SIMainInfo.RegNo;
            r.SIMainInfo.IsValid = true;
            r.SIMainInfo.IsBalanced = false;

            return 1;
        }

        /// <summary>
        /// 清空原有已挂医保患者信息
        /// </summary>
        public void ClearRegInfoOutpatient()
        {
            if (queryMany != null)
            {
                queryMany.ClearPersonInfo();
            }
        }

        /// <summary>
        /// 判断患者是否在黑名单中
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool IsInBlackList(FS.HISFC.Models.Registration.Register r)
        {
            return false;
        }

        /// <summary>
        /// 判断患者是否在黑名单中
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public bool IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return false;
        }

        /// <summary>
        /// 是否上传所有费用
        /// </summary>
        public bool IsUploadAllFeeDetailsOutpatient
        {
            get
            {
                return true;
            }
        }

        public int LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //住院取取医保总额
            patient.SIMainInfo.TotCost = patient.FT.TotCost;
            return this.CheckTotalUploadEqualBalance(patient);
        }

        public int MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            int state = this.GetInpatientMidBalanceInfo(patient);
            if (state == -1)
            {
                return -1;
            }
            return this.BalanceInpatient(patient, ref feeDetails);
        }

        public int ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            this.errMsg = "不允许修改上传明细";
            return 1;
        }

        public int ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            this.errMsg = "不允许修改上传明细";
            return 1;
        }

        public int QueryBlackLists(ref System.Collections.ArrayList blackLists)
        {
            return 1;
        }

        public int QueryCanMedicare(FS.HISFC.Models.Registration.Register r)
        {
            throw new NotImplementedException();
        }

        public int QueryDrugLists(ref System.Collections.ArrayList drugLists)
        {
            return 1;
        }

        public int QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            //计算费用的总额
            // 广州医保 按照项目比例试算
            FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();
            if (feeDetails != null)
            {
                string sql = @"select t.his_code,t.center_rate from fin_com_compare t where t.pact_code='{0}' and t.his_code in ({1})";

                StringBuilder itemCodes = new StringBuilder(256);
                itemCodes.Append("'A'");
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeDetails)
                {
                    itemCodes.Append(",'" + feeItemList.Item.ID + "'");
                }

                FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
                DataSet ds = new DataSet();
                dbMgr.ExecQuery(string.Format(sql, patient.Pact.ID, itemCodes.ToString()), ref ds);

                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeDetails)
                {

                    foreach (FS.HISFC.Models.Base.Const con in alMinFee)
                    {
                        if (feeItemList.Item.MinFee.ID == con.ID)
                        {
                            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                            feeItemList.FT.PubCost = 0;
                            feeItemList.FT.PayCost = 0;
                            feeItemList.FT.DerateCost = 0;
                            return 0;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    DataRow[] drs = ds.Tables[0].Select(string.Format("HIS_CODE='{0}'", feeItemList.Item.ID));
                    decimal rate = 1M;
                    if (drs != null && drs.Length > 0)
                    {
                        rate = FS.FrameWork.Function.NConvert.ToDecimal(drs[0][1]);
                    }

                    if (rate < 0 || rate > 1)
                    {
                        rate = 1;
                    }
                    decimal decDefCost = feeItemList.FT.DefTotCost;
                    if (feeItemList.Item.DefPrice > 0 && decDefCost != feeItemList.FT.TotCost)
                    {
                        //怡康计算费用
                        feeItemList.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(decDefCost * rate, 2);
                        feeItemList.FT.PubCost = decDefCost - feeItemList.FT.OwnCost;
                        feeItemList.FT.OwnCost = feeItemList.FT.TotCost - feeItemList.FT.PubCost;
                        feeItemList.FT.PayCost = 0;
                        feeItemList.FT.DerateCost = 0;//重算不考虑减免金额

                    }
                    else if (feeItemList.SplitFeeFlag)
                    {
                        feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                        feeItemList.FT.PubCost = 0;
                        feeItemList.FT.PayCost = 0;
                        feeItemList.FT.DerateCost = 0;
                    }
                    else
                    {
                        //正常计算费用
                        feeItemList.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost * rate, 2);
                        feeItemList.FT.PubCost = feeItemList.FT.TotCost - feeItemList.FT.OwnCost;
                        feeItemList.FT.PayCost = 0;
                        feeItemList.FT.DerateCost = 0;//重算不考虑减免金额
                    }
                    //feeItemList.FT.OwnCost = feeItemList.FT.TotCost * rate;
                    //feeItemList.FT.PubCost = feeItemList.FT.TotCost - feeItemList.FT.OwnCost;
                    //feeItemList.FT.PayCost = 0;
                    feeItemList.FT.DerateCost = 0;
                    feeItemList.FT.RebateCost = 0;
                    feeItemList.FT.DefTotCost = 0;

                    ft.TotCost += feeItemList.FT.TotCost;
                    ft.OwnCost += feeItemList.FT.OwnCost;
                    ft.PayCost += feeItemList.FT.PayCost;
                    ft.PubCost += feeItemList.FT.PubCost;
                    ft.RebateCost += feeItemList.FT.RebateCost;
                    ft.DefTotCost += feeItemList.FT.DefTotCost;
                    ft.DerateCost += feeItemList.FT.DerateCost;
                }

                ft.TotCost = FS.FrameWork.Public.String.FormatNumber(ft.TotCost, 2);
                ft.OwnCost = FS.FrameWork.Public.String.FormatNumber(ft.OwnCost, 2);
                ft.PayCost = 0;
                ft.PubCost = ft.TotCost - ft.OwnCost;
                ft.RebateCost = FS.FrameWork.Public.String.FormatNumber(ft.RebateCost, 2);
                ft.DefTotCost = FS.FrameWork.Public.String.FormatNumber(ft.DefTotCost, 2);
                ft.DerateCost = FS.FrameWork.Public.String.FormatNumber(ft.DerateCost, 2);

                //起付线，把患者的所有费用都加起来
                FS.HISFC.Models.Base.FT ftInsurancedeal = ft.Clone();
                ftInsurancedeal.TotCost += patient.FT.TotCost;
                ftInsurancedeal.PubCost += patient.FT.PubCost;
                ftInsurancedeal.PayCost += patient.FT.PayCost;
                ftInsurancedeal.OwnCost += patient.FT.OwnCost;
                ftInsurancedeal.DefTotCost += patient.FT.DefTotCost;

                //暂时用广州医保（2） 来查询起伏线
                ArrayList alInsurancedeal = this.inpatientManager.QueryInsurancedeal("2", patient.PVisit.MedicalType.ID);
                if (alInsurancedeal != null && alInsurancedeal.Count > 0)
                {
                    foreach (FS.HISFC.Models.SIInterface.Insurance insurance in alInsurancedeal)
                    {
                        //满足区间条件
                        if (ftInsurancedeal.PubCost > insurance.BeginCost && ftInsurancedeal.PubCost <= insurance.EndCost)
                        {
                            //按区间比例
                            decimal dtKJZtot = ftInsurancedeal.PubCost - insurance.BeginCost;
                            ftInsurancedeal.FTRate.PayRate = insurance.Rate;
                            ftInsurancedeal.SupplyCost = FS.FrameWork.Public.String.FormatNumber(dtKJZtot * insurance.Rate, 2);//个人自付部分= （总费用-自费-乙类自付-起伏线）* 自付比 
                            ftInsurancedeal.PubCost = ftInsurancedeal.TotCost - ftInsurancedeal.OwnCost - ftInsurancedeal.PayCost - ftInsurancedeal.SupplyCost;//记账金额
                            break;
                        }
                    }

                    //重新计算本次收费的记账金额
                    //记账金额
                    ft.PubCost = ftInsurancedeal.PubCost - patient.FT.PubCost;
                    if (ft.PubCost < 0)
                    {
                        ft.PubCost = 0;
                    }
                    //自费金额
                    ft.OwnCost = ft.TotCost - ft.PayCost - ft.PubCost;
                }
            }

            patient.FT = ft;
            return 1;
        }

        public int QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            throw new NotImplementedException();
        }

        public int QueryUndrugLists(ref System.Collections.ArrayList undrugLists)
        {
            return 1;
        }

        public int RecallRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            FS.HISFC.BizLogic.Fee.Interface FeeInterface = new FS.HISFC.BizLogic.Fee.Interface();
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            FeeInterface.SetTrans(this.trans);
            controlParamIntegrate.SetTrans(this.trans);
            FeeInterface.IsSplitFee = controlParamIntegrate.GetControlParam<bool>("I00020", true, false);
            return FeeInterface.ComputeRate(patient.Pact.ID, ref feeItemList);
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
        }

        public System.Data.IDbTransaction Trans
        {
            set
            {
                this.trans = value;
            }
        }

        public int UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return this.LocalComputeInpatient(patient, false);
            return 1;
        }

        public int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
          
            if (f == null)
            {
                this.errMsg = "明细为空!";
                return -1;
            }
            pactCode = r.Pact.ID;
            if (!isSearch)
            {
                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                    this.localMgr.SetTrans(this.trans);
                }
                isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
                if (!isSpecailPact)
                {
                    isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
                }
                string biz_type = localMgr.GetApiBizType(r.Pact.ID);
                if (!string.IsNullOrEmpty(biz_type))
                {
                    isPactCanCallApi = true;
                    bizType = biz_type;
                }
                this.isSearch = true;//已经查询过
            }
            if (!IsCallApi ||!isPactCanCallApi|| isSpecailPact || isInPatientDealPact)
            {
                if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
                {
                    this.errMsg = "没有找到患者的就医登记号";
                    return -1;
                }
                this.MyConnect();
                if (conn == null)
                {
                    this.errMsg = "没有设置数据库连接!";
                    return -1;
                }
                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                }
                operDate = mySILocalManager.GetDateTimeFromSysDateTime();

                if (!isSpecailPact && !isInPatientDealPact)
                {
                    if (conn.InsertShareData(r, f, 0, operDate) == -1)
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
            return 1;
        }

        public int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            if (r.ID == null || r.ID == "")
            {
                this.errMsg = "没有找到患者的挂号流水号!";
                return -1;
            }
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                if (r.SIMainInfo.RegNo.Length > 6)
                { 
                    //如果没有，就去就医登记号前6位，待调整后修改
                    r.SIMainInfo.HosNo = r.SIMainInfo.RegNo.Substring(0, 6);
                }
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
            pactCode = r.Pact.ID;


            int iReturn = -1;
            if (!isSearch)
            {
                if (this.trans != null)
                {
                    this.mySILocalManager.SetTrans(this.trans);
                    this.localMgr.SetTrans(this.trans);
                }
                isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
                if (!isSpecailPact)
                {
                    isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
                }
                string biz_type = localMgr.GetApiBizType(r.Pact.ID);
                if (!string.IsNullOrEmpty(biz_type))
                {
                    isPactCanCallApi = true;
                    bizType = biz_type;
                }
                this.isSearch = true;//已经查询过
            }

            if (!IsCallApi ||!isPactCanCallApi|| isSpecailPact || isInPatientDealPact)
            {
                if (r.SIMainInfo.RegNo == null || r.SIMainInfo.RegNo == "")
                {
                    this.errMsg = "没有找到患者的就医登记号";
                    return -1;
                }
                this.MyConnect();
                if (conn == null)
                {
                    this.errMsg = "没有设置医保数据库连接!";
                    MyDisconnect();
                    return -1;
                }
                decimal txCost = 0m;

                FS.HISFC.BizLogic.Fee.Item myItem = new FS.HISFC.BizLogic.Fee.Item();
                FS.HISFC.BizLogic.Pharmacy.Item myPItem = new FS.HISFC.BizLogic.Pharmacy.Item();
                Management.SILocalManager myInterface = new Management.SILocalManager();
                if (this.trans != null)
                {
                    myItem.SetTrans(trans);
                    myPItem.SetTrans(trans);
                    myInterface.SetTrans(trans);
                }
                foreach (FeeItemList f in feeDetails)
                {
                    if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = myPItem.GetItem(f.Item.ID);
                        if (phaItem == null)
                        {
                            this.errMsg = "获得药品信息出错!";
                            MyDisconnect();
                            return -1;
                        }
                        f.Item.UserCode = phaItem.UserCode;

                        string indicationsFlag = myInterface.IsIndicationsItemForOut(r.ID, f.Order.ID);
                        f.Item.User03 = indicationsFlag;
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

                if (this.trans != null)
                {
                    mySILocalManager.SetTrans(this.trans);
                }
                operDate = mySILocalManager.GetDateTimeFromSysDateTime();

                ArrayList alTempFeeDetails = (feeDetails.Clone() as ArrayList);

                for (int i = alTempFeeDetails.Count - 1; i >= 0; i--)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList temp = alTempFeeDetails[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                    if ((temp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug))
                    {
                        //if (mySILocalManager.GetCompareSingleItem(gzybPact_code, temp.Item.ID, ref temp.Compare) == -2) //没有维护对照的药品
                        //{
                            //txCost += temp.FT.TotCost;
                            //alTempFeeDetails.Remove(temp);
                        //}
                    }
                }

                r.SIMainInfo.OperInfo.User03 = txCost.ToString();  //存特需服务费费用

                if (!isSpecailPact && !isInPatientDealPact)
                {
                    //插入明细
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
                if (conn != null)
                {
                    try
                    {
                        conn.Commit();//上传后提交事务便于预结算时读取
                    }
                    catch (Exception ex)
                    {
                    }//没办法，没事务也要提交oyh，可能要改；第二次上传，没开始事务也这样，主代码有问题
                }
                MyDisconnect();

                #region HIS本地存储医保明细

                //if (!isSpecailPact && !isInPatientDealPact)
                //{
                //    //插入明细
                //    iReturn = this.mySILocalManager.InsertShareData(r, alTempFeeDetails, operDate);
                //}
                //else
                //{
                //    iReturn = mySILocalManager.InsertShareDataInpatient(r, alTempFeeDetails, operDate);
                //}
                //if (iReturn < 0)
                //{
                //    this.errMsg = "上传明细失败!" + conn.Err;
                //    MyDisconnect();
                //    return -1;
                //}
                //if (iReturn == 0)
                //{
                //    this.errMsg = "没有可上传的费用,请将合同单位设为自费或者其他";
                //    MyDisconnect();
                //    return -1;
                //}

                #endregion
            }
            return 1;
        }

        public int UploadRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            patient.SIMainInfo.IsBalanced = false;
            Management.SILocalManager myInterface = new Management.SILocalManager();
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

        public int UploadRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {

            if (this.trans != null)
            {
                mySILocalManager.SetTrans(this.trans);
                localMgr.SetTrans(this.trans);
            }
            if (!isSearch)
            {
                this.isSpecailPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID);
                if (!isSpecailPact)
                {
                    this.isInPatientDealPact = mySILocalManager.IsPactDealByInpatient(r.Pact.ID, "INPATIENT");
                }
                string biz_type = localMgr.GetApiBizType(r.Pact.ID);
                if (!string.IsNullOrEmpty(biz_type))
                {
                    isPactCanCallApi = true;
                    bizType = biz_type;
                }
                this.isSearch = true;//已经查询过


            }
            if (!IsCallApi ||!isPactCanCallApi|| isSpecailPact || isInPatientDealPact)
            {
                r.SIMainInfo.TypeCode = "1";
                int iReturn = mySILocalManager.UpdateSiMainInfo(r);
                if (iReturn == -1)
                {
                    this.errMsg = "插入医保登记出错!" + mySILocalManager.Err;
                    return -1;
                }
                if (iReturn == 0)
                {
                    iReturn = mySILocalManager.InsertSIMainInfo(r);
                    if (iReturn <= 0)
                    {
                        this.errMsg = "插入医保登记出错!" + mySILocalManager.Err;
                        return -1;
                    }
                }
            }

            ////为了不影响已稳定版本
            ////直接更新 待遇类型
            //if (!string.IsNullOrEmpty(r.SIMainInfo.ApplyType.ID))
            //{
            //    if (-1 == localMgr.UpdateApiTreamType(r.ID, r.SIMainInfo.RegNo, r.SIMainInfo.ApplyType.ID))
            //    {
            //        MyDisconnect();
            //        this.errMsg = "更新 待遇类型出错!" + localMgr.Err;

            //        if (CancelRegInfoOutpatient(r) < 0)
            //        {
            //            this.errMsg += "\n 医保API取消门诊登记失败，请手工取消医保门诊登记！";
            //        }
            //        return -1;
            //    }
            //}
            return 1;
        }

        #endregion

        #region IMedcareTranscation 成员

        private int MyConnect()
        {
            //判断是否特殊医保系统服务器
            isSpeseSiServer = this.mySILocalManager.IsPactDealByOtherServer(pactCode);
            //为本地的数据库连接设置事务
            //为了适应两个服务器，暂时屏蔽
            if (conn == null || 
                (conn != null  && this.isSpeseSiServer && !conn.SiConnectFile.Contains("SiDataBaseYBMM.xml")) || 
                (conn != null && !this.isSpeseSiServer && conn.SiConnectFile.Contains("SiDataBaseYBMM.xml")))
            // if(conn==null)
            {
            LabelTwo:
                try
                {
                    if (!this.isSpeseSiServer)
                    {
                        conn = new Management.SIConnect();
                    }
                    else
                    {
                        conn = new global::GZSI.Management.SIConnect(System.Windows.Forms.Application.StartupPath + @".\profile\SiDataBaseYBMM.xml");
                    }
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

        private int MyDisconnect()
        {
            //   conn.Close();
            return 1;
        }

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

        public long Commit()
        {
            //conn = null;
            return 1;
        }

        public long Connect()
        {
            //为了适应两个服务器，暂时屏蔽
            if (conn == null 
                || (conn != null 
                && this.isSpeseSiServer
                && !conn.SiConnectFile.Contains("SiDataBaseYBMM.xml"))
                || (conn != null 
                && !this.isSpeseSiServer 
                && conn.SiConnectFile.Contains("SiDataBaseYBMM.xml")))
            {
            LabelTwo:
                try
                {
                    if (!this.isSpeseSiServer)
                    {
                        //默认医保服务器
                        conn = new Management.SIConnect();
                    }
                    else
                    {
                        //门慢服务器
                        conn = new global::GZSI.Management.SIConnect(System.Windows.Forms.Application.StartupPath + @".\profile\SiDataBaseYBMM.xml");
                    }
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

        public long Disconnect()
        {
            if (conn == null)
            {
                this.errMsg = "数据库已断开连接，不能断开!";
                return 1;
            }
            try
            {
                this.ClearRegInfoOutpatient();
                conn.Close();
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
            if (register != null && !string.IsNullOrEmpty(register.ID))
            {
                if (!isSearch)
                {
                    if (this.trans != null)
                    {
                        this.localMgr.SetTrans(this.trans);
                    }

                    string biz_type = localMgr.GetApiBizType(register.Pact.ID);
                    if (!string.IsNullOrEmpty(biz_type))
                    {
                        isPactCanCallApi = true;
                        bizType = biz_type;
                    }
                    this.isSearch = true;//已经查询过

                }
                if (-1 == CancelBalanceOutpatient(register, ref alFeeUploads))
                {
                    MessageBox.Show("API结算回滚出错！请手工（PJ3系统）进行退费！");
                    return -1;
                }
               
                
            }
            else
            {
                if (conn == null)
                {
                    //this.errMsg = "数据库已断开连接，不能断开!";
                    return 1;
                }
                try
                {
                    conn.RollBack();
                    conn = null;
                }
                catch (Exception e)
                {
                    //this.errMsg = e.Message;
                    //return -1;
                }
                
            }
            return 1;

        }

        #endregion

        public GZSI()
        {
            isCallApi = this.GetApiXmlSetting(this.filePath);
            alMinFee = conMgr.GetList("NOUPMINFEE");
            if (alMinFee == null || alMinFee.Count == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("获取常数出错，请维护不需要上传的项目", "提示");
                return;
            }

            gzybPact_code = mySILocalManager.GetGzSiParamPactCode("gzyb", "param");
            //gzybPact_code = "2";
        }

        private int CheckTotalUploadEqualBalance(FS.HISFC.Models.RADT.PatientInfo balancePatient)
        {
            FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            if (this.trans != null)
            {
                feeInpatient.SetTrans(this.trans);
            }
            int iReturn;
            decimal tempTotCost = 0;
            if (!string.IsNullOrEmpty(balancePatient.Insurance.User01) && !string.IsNullOrEmpty(balancePatient.Insurance.User02))
            {
                //中结
                try
                {
                    DateTime dtBegin = FS.FrameWork.Function.NConvert.ToDateTime(balancePatient.Insurance.User01);
                    DateTime dtEnd = FS.FrameWork.Function.NConvert.ToDateTime(balancePatient.Insurance.User02);
                    if (feeInpatient.GetUploadTotCost(balancePatient.ID, dtBegin, dtEnd, ref tempTotCost) == -1)
                    {
                        this.errMsg = "错误：获取上传总费用出错！" + feeInpatient.Err;
                        return -1;
                    }
                }
                catch (Exception)
                {
                    this.errMsg = "异常：获取上传总费用出错！";
                    return -1;
                }

            }
            else
            {
                // 出院
                if (feeInpatient.GetUploadTotCost(balancePatient.ID, ref tempTotCost) == -1)
                {
                    this.errMsg = "获取上传总费用出错！" + feeInpatient.Err;
                    return -1;
                }
            }

            //则认为有多条结算信息，弹出输入医保流水号的对话框，如果选择取消，则退出。
            while (tempTotCost != balancePatient.SIMainInfo.TotCost)
            {

                FS.HISFC.Models.RADT.PatientInfo SIPatient = new FS.HISFC.Models.RADT.PatientInfo();

                Controls.frmTextInput ucTextInput1 = new Controls.frmTextInput();
                ucTextInput1.OkButtonClickEvent += new frmTextInput.OkButtonClink(ucTextInput1_OkButtonClickEvent);

                ucTextInput1.StartPosition = FormStartPosition.CenterParent;
                ucTextInput1.ShowDialog();

                if (string.IsNullOrEmpty(this.regNo))
                {
                    this.errMsg = "如果本地费用与结算费用不符,请查明原因";
                    return -1;
                }

                SIPatient.SIMainInfo.RegNo = ucTextInput1.TextRegNO;
                if (balancePatient.SIMainInfo.RegNo == SIPatient.SIMainInfo.RegNo)
                {
                    errMsg = "该医保登记号的费用信息已经包含，请重新输入!";
                    continue;
                }
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
                // balancePatient.SIMainInfo.ItemPayCost += SIPatient.SIMainInfo.ItemPayCost;
                balancePatient.SIMainInfo.ItemPayCost += SIPatient.SIMainInfo.PayCost;//存医保自付金额
                balancePatient.SIMainInfo.PubOwnCost += SIPatient.SIMainInfo.PubOwnCost;
                balancePatient.SIMainInfo.OverTakeOwnCost += SIPatient.SIMainInfo.OverTakeOwnCost;
                balancePatient.SIMainInfo.HosCost += SIPatient.SIMainInfo.HosCost;
            }
            return 1;
        }

        string regNo = string.Empty;

        private void ucTextInput1_OkButtonClickEvent(string regNo)
        {
            this.regNo = regNo;
        }

        #region 本地预结算，调整费用明细

        /// <summary>
        /// 计算费用项目的费用金额（包括 TOT_COST,PUB_COST,PAY_COST,OWN_COST）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        private int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref FS.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {

            int returnValue = 0;

            foreach (FS.HISFC.Models.Base.Const con in alMinFee)
            {
                if (item.Item.MinFee.ID == con.ID)
                {
                    item.FT.OwnCost = item.FT.TotCost;
                    item.FT.PubCost = 0;
                    item.FT.PayCost = 0;
                    item.FT.DerateCost = 0;
                    return 0;
                }
                else
                {
                    continue;
                }
            }

            FS.HISFC.Models.SIInterface.Compare objCompare = new FS.HISFC.Models.SIInterface.Compare();

            returnValue = mySILocalManager.GetCompareSingleItemNew(patient.Pact.ID, item.Item.ID, ref objCompare);

            if (returnValue == -1)
            {
                this.errMsg = mySILocalManager.Err;
                return returnValue;
            }
            if (returnValue == -2)
            {
                objCompare.CenterItem.Rate = 1;
            }

            if (item.Item.DefPrice > 0 && item.Item.DefPrice * item.Item.Qty != item.FT.TotCost)
            {
                //怡康计算费用
                decimal decDefCost = item.FT.DefTotCost;
                item.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(decDefCost * objCompare.CenterItem.Rate, 2);
                item.FT.PubCost = decDefCost - item.FT.OwnCost;
                item.FT.OwnCost = item.FT.TotCost - item.FT.PubCost;
                item.FT.PayCost = 0;
                item.FT.DerateCost = 0;//重算不考虑减免金额

            }
            else if (item.Item.DefPrice == 0 && item.SplitFeeFlag)
            {
                item.FT.OwnCost = item.FT.TotCost;
                item.FT.PubCost = 0;
                item.FT.PayCost = 0;
                item.FT.DerateCost = 0;
            }
            else
            {
                //正常计算费用
                item.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(item.FT.TotCost * objCompare.CenterItem.Rate, 2);
                item.FT.PubCost = item.FT.TotCost - item.FT.OwnCost;
                item.FT.PayCost = 0;
                item.FT.DerateCost = 0;//重算不考虑减免金额
            }

            return 0;
        }

        /// <summary>
        /// 计算门诊费用项目的费用金额
        /// </summary>
        /// <param name="register"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private int RecomputeFeeItemListOutPatient(FS.HISFC.Models.Registration.Register register, ref FS.HISFC.Models.Fee.Outpatient.FeeItemList item)
        {

            int returnValue = 0;

            FS.HISFC.Models.SIInterface.Compare objCompare = new FS.HISFC.Models.SIInterface.Compare();

            returnValue = mySILocalManager.GetCompareSingleItemNew(register.Pact.ID, item.Item.ID, ref objCompare);

            if (returnValue == -1)
            {
                this.errMsg = mySILocalManager.Err;
                return returnValue;
            }
            if (returnValue == -2)
            {
                objCompare.CenterItem.Rate = 1;
            }

            item.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(item.FT.TotCost * objCompare.CenterItem.Rate, 2);
            item.FT.PubCost = item.FT.TotCost - item.FT.OwnCost;
            item.FT.PayCost = 0;
            item.FT.DerateCost = 0;//重算不考虑减免金额

            return 0;
        }

        /// <summary>
        /// 本地预结算
        /// 预计农保预结算Job执行时间晚上 2:00
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="isChangePact">身份变更 true 不是身份变更 false</param>
        /// <returns></returns>
        public int LocalComputeInpatient(FS.HISFC.Models.RADT.PatientInfo patient, bool isChangePact)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在本地预结算...");
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            mySILocalManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //查询农保预结算JOB 的上次执行时间
            DateTime dtLast = DateTime.MinValue;
            string lastTime = mySILocalManager.GetJobLastExecDate(patient.ID, patient.Pact.ID);
            if (string.IsNullOrEmpty(lastTime))
            {
                dtLast = patient.PVisit.InTime;
            }
            else
            {
                dtLast = FS.FrameWork.Function.NConvert.ToDateTime(lastTime);
            }

            DateTime dtNow = mySILocalManager.GetDateTimeFromSysDateTime();

            if (isChangePact)
            {
                //获取该患者所有在院未结费用，按每天费用处理
            }
            else
            {
                //非身份变更预结算
                if (patient.PVisit.InState.ID.ToString() == "C" || patient.PVisit.InState.ID.ToString() == "B")
                {
                    #region 出院预结算
                    DateTime dtFirst = dtLast.Date;
                    if (dtLast.AddDays(1) > dtNow)
                    {
                        //出院当天费用的预结算
                        if (-1 == ExtcutePreBalance(patient, dtFirst))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            return -1;
                        }
                    }
                    else
                    {

                        try
                        {
                            //出院前几天为跑农保预结算处理
                            System.TimeSpan interval = dtNow.Date - dtLast.Date;
                            if (interval.Days >= 1)
                            {
                                //多天未执行农保预结算，按每天来预结算费用                           
                                for (int i = 0; i <= interval.Days; i++)
                                {

                                    if (-1 == ExtcutePreBalance(patient, dtFirst))
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                        return -1;
                                    }
                                    dtFirst = dtFirst.AddDays(1);

                                }


                            }

                        }
                        catch { }
                    }

                    #endregion //出院预结算

                    #region 更新新农合预结算时间
                    //更新新农合预结算时间
                    if (-1 == mySILocalManager.InsertOrUpdateLocalBalanceTime(patient, mySILocalManager.GetDateTimeFromSysDateTime()))
                    {
                        this.errMsg = mySILocalManager.Err;
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    #region 在院预结算
                    //在院预结算
                    if (dtLast.AddDays(1) > dtNow)
                    {
                        //已经预结算
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return 1;
                    }
                    else
                    {
                        try
                        {
                            DateTime dtFirst = dtLast.Date;
                            System.TimeSpan interval = dtNow.Date - dtLast.Date;
                            if (interval.Days >= 1)
                            {
                                //多天未执行农保预结算，按每天来预结算费用

                                for (int i = 0; i < interval.Days; i++)
                                {

                                    if (-1 == ExtcutePreBalance(patient, dtFirst))
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                        return -1;
                                    }
                                    dtFirst = dtFirst.AddDays(1);

                                }

                                #region 更新新农合预结算时间
                                //更新新农合预结算时间
                                DateTime dt = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 2, 0, 0);
                                if (-1 == mySILocalManager.InsertOrUpdateLocalBalanceTime(patient, dt))
                                {
                                    this.errMsg = mySILocalManager.Err;
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                    return -1;
                                }
                                #endregion

                            }

                        }
                        catch { }

                    }

                    #endregion //end 在院预结算
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// 按时间执行当日本地预结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private int ExtcutePreBalance(FS.HISFC.Models.RADT.PatientInfo patient, DateTime date)
        {
            DateTime dtBegin = date.Date;
            DateTime dtEnd = new DateTime(dtBegin.Year, dtBegin.Month, dtBegin.Day, 23, 59, 59);

            ArrayList alFeeItemList = new ArrayList();
            alFeeItemList = inpatientManager.QueryMedicineListsForBalance(patient.ID, dtBegin, dtEnd);

            alFeeItemList.AddRange(inpatientManager.QueryItemListsForBalance(patient.ID, dtBegin, dtEnd));

            if (alFeeItemList != null && alFeeItemList.Count > 0)
            {
                return this.ComputeFeeCostInpatient(patient, ref alFeeItemList);
            }
            return 1;
        }

        /// <summary>
        /// 计算所有的费用，并更新费用明细（fin_ipb_medicinelist，fin_ipb_itemlist,fin_ipb_feeinfo、fin_ipr_inmaininfo）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        public int ComputeFeeCostInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeItemLists)
        {
            for (int i = 0; i <= feeItemLists.Count - 1; ++i)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList item = feeItemLists[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

                FS.HISFC.Models.Fee.Inpatient.FeeItemList itemOld = item.Clone();

                #region 重新计算费用

                //判断身份变更费用明细
                if (item.Patient.Pact.ID != patient.Pact.ID) continue;

                if (RecomputeFeeItemListInpatient(patient, ref item) < 0)
                {
                    return -1;
                }
                #endregion

                #region 更新费用明细

                //更新费用明细表（fin_ipb_itemlist,fin_ipb_medicinelist）
                if (item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //更新药品费用明细表（fin_ipb_medicinelist）
                    if (mySILocalManager.UpdateMedItemList(item) < 0)
                    {
                        return -1;
                    }
                }
                else
                {
                    //更新非药品费用明细表（fin_ipb_itemlist）
                    if (mySILocalManager.UpdateItemList(item) < 0)
                    {
                        return -1;
                    }
                }
                #endregion

                #region 更新费用汇总
                //根据处方号、最小费用、执行科室 获取费用汇总信息
                FS.HISFC.Models.Base.FT ft = mySILocalManager.QueryFeeInfo(itemOld.RecipeNO, itemOld.Item.MinFee.ID, itemOld.ExecOper.Dept.ID);
                if (ft == null)
                {
                    return -1;
                }

                //更新费用汇总
                //增加重新计算的费用
                ft.TotCost = ft.TotCost - itemOld.FT.TotCost + item.FT.TotCost;
                ft.OwnCost = ft.OwnCost - itemOld.FT.OwnCost + item.FT.OwnCost;
                ft.PubCost = ft.PubCost - itemOld.FT.PubCost + item.FT.PubCost;
                ft.PayCost = ft.PayCost - itemOld.FT.PayCost + item.FT.PayCost;


                //更新费用汇总操作
                if (-1 == mySILocalManager.UpdateFeeInfo(ft, itemOld.RecipeNO, itemOld.Item.MinFee.ID, itemOld.ExecOper.Dept.ID))
                {
                    return -1;

                }
                #endregion

                #region 更新住院主表
                //根据住院流水号，获取住院主表费用
                FS.HISFC.Models.Base.FT ftMain = mySILocalManager.QueryInMainInfo(patient.ID);
                if (ftMain == null) return -1;


                //更新费用汇总
                //增加重新计算的费用
                ftMain.TotCost = ftMain.TotCost - itemOld.FT.TotCost + item.FT.TotCost;
                ftMain.OwnCost = ftMain.OwnCost - itemOld.FT.OwnCost + item.FT.OwnCost;
                ftMain.PubCost = ftMain.PubCost - itemOld.FT.PubCost + item.FT.PubCost;
                ftMain.PayCost = ftMain.PayCost - itemOld.FT.PayCost + item.FT.PayCost;


                //更新费用汇总操作
                if (-1 == mySILocalManager.UpdateInMainInfo(ftMain, patient.ID))
                {
                    return -1;
                }
                #endregion
            }
            return 1;
        }

        #endregion

        private int GetInpatientMidBalanceInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            FS.HISFC.Models.RADT.PatientInfo balancePatient = patient.Clone();
            FS.HISFC.Models.RADT.PatientInfo siPInfo = new FS.HISFC.Models.RADT.PatientInfo();

            FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();

            if (this.trans != null)
            {
                myInterface.SetTrans(this.trans);
                feeInpatient.SetTrans(this.trans);
            }
            pactCode = patient.Pact.ID;
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

        private int GetUserCode( ArrayList feeDetails,out decimal totNoCompareCost,out ArrayList feeUploads)
        {

            sbNoCompare = new StringBuilder();
           

            FS.HISFC.BizLogic.Fee.Item myItem = new FS.HISFC.BizLogic.Fee.Item();
            FS.HISFC.BizLogic.Pharmacy.Item myPItem = new FS.HISFC.BizLogic.Pharmacy.Item();
            if (this.trans != null)
            {
                myPItem.SetTrans(this.trans);
                myItem.SetTrans(this.trans);
                mySILocalManager.SetTrans(this.trans);
            }
            totNoCompareCost = 0;
            feeUploads = new ArrayList();
            ArrayList feeDetailsTemp = (feeDetails.Clone() as ArrayList);

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetailsTemp)
            {
                if (mySILocalManager.GetCompareSingleItem(gzybPact_code, f.Item.ID, ref f.Compare) == -2) //没有维护对照的药品
                {
                    if (sbNoCompare.Length==0)
                    {
                         sbNoCompare.Append("未对照项目：\n");
                    }
                    totNoCompareCost += f.FT.TotCost;
                    sbNoCompare.Append("项目代码：" + f.Item.ID + "项目名称：" + f.Item.Name.Trim() +"\n");
                  //  feeUploads.Remove(f);
                    continue;
                }
                else if (string.IsNullOrEmpty(f.Compare.CenterItem.ID) || string.IsNullOrEmpty(f.Compare.SpellCode.UserCode))
                {
                    if (sbNoCompare.Length == 0)
                    {
                        sbNoCompare.Append("未对照项目：\n");
                    }
                    totNoCompareCost += f.FT.TotCost;
                    sbNoCompare.Append("项目代码：" + f.Item.ID + "项目名称：" + f.Item.Name.Trim() + "\n");
                 //   feeUploads.Remove(f);
                    continue;
                }
                else
                {
                    f.Item.UserCode = f.Compare.SpellCode.UserCode;
                    feeUploads.Add(f);
                }
                //if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                //{
                //    FS.HISFC.Models.Pharmacy.Item phaItem = myPItem.GetItem(f.Item.ID);
                //    if (phaItem == null)
                //    {
                //        this.errMsg = "获得药品信息出错!";
                //        MyDisconnect();
                //        return -1;
                //    }
                //    f.Item.UserCode = phaItem.NameCollection.FormalSpell.UserCode;
                  
                //}
                //else
                //{
                //    FS.HISFC.Models.Base.Item baseItem = new FS.HISFC.Models.Base.Item();
                //    baseItem = myItem.GetValidItemByUndrugCode(f.Item.ID);
                //    if (baseItem == null)
                //    {
                //        this.errMsg = "获得非药品信息出错!";
                //        MyDisconnect();
                //        return -1;
                //    }
                //    f.Item.UserCode = baseItem.UserCode;
                //}
            }

            if (sbNoCompare.Length > 0)
            {
                if (DialogResult.No == MessageBox.Show(sbNoCompare.ToString() +"\n\n 是否自费处理？","未对照项目",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2))
                {
                    return -1;
                }
            }
            return 1;
        }

        private string GetLocalPerInfo(string idcard)
        {
            string[] info = idcard.Split('|');
            if (info.Length > 1)
            {
                string type = info[0];

                switch (type)
                {
                    case "1":
                        return "身份证：" + info[1];
                    case "2":
                        return "个人电脑号：" + info[1];
                    case "3":
                        return "姓名：" + info[1];
                    case "4":
                        return "医疗证号：" + info[1];
                    default:
                        return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}
