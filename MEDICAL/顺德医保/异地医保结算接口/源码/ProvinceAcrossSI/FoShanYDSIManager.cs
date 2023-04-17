using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace ProvinceAcrossSI
{
    public class ProvinceAcrossSIManager : FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare
    {
        private System.Data.IDbTransaction trans = null;
        //private ProvinceAcrossSI.Business.OutPatient.OutPatientService outPatientService = new ProvinceAcrossSI.Business.OutPatient.OutPatientService();
        private ProvinceAcrossSI.Business.Common.CommonService comService = new ProvinceAcrossSI.Business.Common.CommonService();
        private ProvinceAcrossSI.Business.InPatient.InPatientService inPatientService = new ProvinceAcrossSI.Business.InPatient.InPatientService();


        #region 佛山医保特殊记录

        //门诊标记,门诊业务操作为true|住院业务操作为false
        private bool outFlag = true;
        //用于记录当前正在处理状态(方法),用于rollback方法判断
        private string callStatus = "";

        #region 用于错误回滚时取消门诊结算使用数据
        //门诊患者姓名
        private string curOutPatientName;
        //门诊参保险种
        private string curOutPatientSIType;
        //门诊结算流水号
        private string curOutPatientBalanceSeq;
        //门诊结算费用总额
        private string curOutPatientFeeTot;
        //结算日期
        private DateTime curOutPatientBalanceDate;
        //合同单位信息
        FS.HISFC.Models.Base.PactInfo pact = new FS.HISFC.Models.Base.PactInfo();
        #endregion

        #endregion


        #region IMedcare 成员

        /// <summary>
        /// 出院结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            //ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
            //string msg = string.Empty;
            //string status = string.Empty;
            //if (true)
            //{
            //    //TODO:查询佛山医保住院结算信息
            //    //最大序号的有效结算信息                 
            //    if (this.inPatientService.QuerySiBalanceInfo(ref patient, ref personInfo, ref msg) < 1)
            //    {
            //        this.ErrMsg = msg;
            //        return -1;
            //    }
            //    patient.FT.OwnCost = personInfo.akb067;
            //    patient.FT.PubCost = personInfo.ake149;
            //    patient.FT.TotCost = personInfo.ake149 + personInfo.akb067;
            //    patient.Pact.PayKind.ID = "02";
            //    patient.SIMainInfo.OwnCost = personInfo.akb067;
            //    patient.SIMainInfo.PayCost = 0;
            //    patient.SIMainInfo.PubCost = personInfo.ake149;
            //    patient.SIMainInfo.TotCost = personInfo.ake149 + personInfo.akb067;

            //    return 1;
            //}
            //else
            //{
            //    //TODO:查询异地医保住院结算信息
            //}
            ProvinceAcrossSI.Controls.YDInpatientBalance YDInpatientBalance = new ProvinceAcrossSI.Controls.YDInpatientBalance();
            YDInpatientBalance.SetPatient(patient);
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(YDInpatientBalance);
            return 1;
        }

        /// <summary>
        /// 门诊结算确认
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            //this.callStatus = "EnterBalanceOutpatient";

            ////结算时更新发票号并上传发票号
            //if (string.IsNullOrEmpty(r.SIMainInfo.BalNo))
            //{
            //    return -1;
            //}
            //else
            //{
            //    string status = "";
            //    string msg = "";
            //    this.outPatientService.OutPatientUploadInvoiceNo(r, ref status, ref msg);
            //    if (status == null || int.Parse(status) != 1)
            //    {
            //        //上传发票不能影响主要流程,所以需要提示并正常进行
            //        MessageBox.Show("佛山医保结算上传发票出错:\n" + msg + "请使用发票补传功能上传发票！");
            //        this.errMsg = msg;
            //        //TODO:插入发票补传表
            //        this.outPatientService.InsertNeedUpLoadInvoiceNo(r);
            //        return 1;
            //    }
            //}
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
            //根据信息科提出，避免断网时不能重新出发票，故该处不再查询中心住院登记信息
            return 1;

            //中心住院登记信息查询
            string msg = string.Empty;
            ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
            //if (this.inPatientService.QueryRegInfo(patient, ref personInfo, ref msg) < 1)
            //{
            //    this.ErrMsg = "佛山医保\n" + msg;
            //    return -1;
            //}

            //流程：中心结算————本地结算，本地结算取消————中心结算取消；
            //if (!personInfo.BalanceFlag.Equals("1"))
            //{
            //    this.ErrMsg = "佛山医保\n" + "请先取消中心结算！";
            //    return -1;
            //}
            return 1;

            if (this.inPatientService.CancelYDBalanceInpatient(patient, ref msg) < 1)
            {
                this.ErrMsg = msg;
                return -1;
            }
            return 1;
        }

        public int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            ////门诊结算取消
            //this.errMsg = "";
            //string status = "";
            //string msg = "";

            //outPatientService.OutPatientCancelBalance(r, ref status, ref msg);

            //if (status == null || "".Equals(status) || int.Parse(status) == 3 || int.Parse(status) < 0)
            //{
            //    //返回1是结算成功,2是重复取消结算,3是取消结算失败
            //    this.errMsg = "取消结算失败,错误信息:\n" + msg + "\n请到佛山医保网核对结算信息";
            //    return -1;
            //}

            return 1;
        }

        public int CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        /// <summary>
        /// 住院登记取消
        /// 使用：无费退院
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            string status = string.Empty;
            string msg = string.Empty;

            //住院号8位处理
            FS.HISFC.Models.RADT.PatientInfo patientTemp = new FS.HISFC.Models.RADT.PatientInfo();
            patientTemp = patient.Clone();
            int pnLength = patientTemp.PID.PatientNO.Length;
            int needLength = 8;//需要自右向左几位
            patientTemp.PID.PatientNO = patientTemp.PID.PatientNO.Substring(pnLength - needLength, needLength);

            if (this.inPatientService.CancelInPatientReg(patientTemp, ref status, ref msg) < 1)
            {
                this.ErrMsg = msg;
                return -1;
            }
            return 1;
        }

        public int CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            //门诊结算不存在单独上传明细
            return 1;
        }

        public int DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            //门诊结算不存在单独上传明细
            return 1;
        }

        public string Description
        {
            get { return "佛山医保接口"; }
        }

        public string ErrCode
        {
            get { return ""; }
        }

        private string errMsg;

        public string ErrMsg
        {
            get { return this.errMsg; }
            set { this.errMsg = value; }
        }

        public int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        /// <summary>
        /// 佛山医保读卡方法
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            ////重新获取相应信息,用于退费取消结算
            //if (outPatientService.GetOutPatientBalanceBaseInfo(r) < 0)
            //{
            //    this.errMsg = "医保结算数据缺失";
            //    return -1;
            //}

            //this.GetRegInfoOutpatient(r, true);
            return 1;
        }

        /// <summary>
        /// 获得医保挂号信息【由于门诊收费时也会调用，所以重载】
        /// </summary>
        /// <param name="r">门诊挂号实体</param>
        /// <param name="isReadCard">是否读卡</param>
        /// <returns></returns>
        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r, bool isReadMCard)
        {
            return 1;
            //if (isReadMCard == false)
            //{
            //    ////重新获取相应信息,用于退费取消结算
            //    //if (outPatientService.GetOutPatientBalanceBaseInfo(r) < 0)
            //    //{
            //    //    this.errMsg = "医保结算数据缺失";
            //    //    return -1;
            //    //}
            //    return 1;
            //}


            //FS.HISFC.BizProcess.Interface.Account.IReadMCard iReadMCard = null;
            ////iReadMCard = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(ProvinceAcrossSI.ProvinceAcrossSIManager), typeof(FS.HISFC.BizProcess.Interface.Account.IReadMCard)) as FS.HISFC.BizProcess.Interface.Account.IReadMCard;
            //iReadMCard = new ProvinceAcrossSI.ReadMCardFoShan();

            //if (iReadMCard != null)
            //{
            //    string cardId = string.Empty;
            //    string cardNo = string.Empty;
            //    DateTime cardIssueDate = new DateTime();
            //    string userId = string.Empty;
            //    string userName = string.Empty;
            //    string userSex = string.Empty;
            //    string userPhoneNumber = string.Empty;
            //    string errInfo = string.Empty;
            //    DateTime dtBirth = new DateTime();

            //    int rtn = iReadMCard.GetMCardInfo(ref cardId, ref cardNo, ref cardIssueDate,
            //                                      ref userId, ref userName, ref userSex,
            //                                      ref userPhoneNumber, ref dtBirth, ref errInfo);

            //    //收费界面也会调用,需要判断要不要读卡赋值
            //    if (r.Name.Trim().Length != 0 && userName != r.Name)
            //    {
            //        return -1;
            //    }

            //    if (rtn == 1)
            //    {
            //        r.SSN = cardNo;
            //        r.IDCard = userId;
            //        r.Name = userName;
            //        r.Sex.ID = (userSex == "1") ? "M" : "F";
            //        r.Sex.Name = (userSex == "1") ? "男" : "女";
            //        r.Birthday = dtBirth;
            //        r.PhoneHome = userPhoneNumber;
            //        return 1;
            //    }
            //    else
            //    {
            //        MessageBox.Show(errInfo, "提示");
            //        return -1;
            //    }
            //}
            //else
            //{
            //    this.errMsg = "医保卡读卡接口未实现！";
            //    return -1;
            //}
        }

        /// <summary>
        /// 用于门诊资格确认
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool IsInBlackList(FS.HISFC.Models.Registration.Register r)
        {
            //this.errMsg = "";
            //string status = "";
            //string msg = "";

            //if (string.IsNullOrEmpty(r.PhoneHome.Trim()))
            //{
            //    this.ErrMsg = "联系电话不能为空";
            //    return false;
            //}

            //ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();

            //outPatientService.OutPatientAccreditation(r, ref personInfo, ref status, ref msg);

            //this.callStatus = "AccreditationOutPatient";

            //#region 本地合同单位对照

            //string retPactCode = "";
            //if (this.comService.CheckPactCode(r, personInfo.SIType, personInfo.PersonType, "C", out retPactCode) < 0)
            //{
            //    this.errMsg = "本地结算方式选择错误,应选择编号:" + retPactCode;
            //    return true;//相当于在黑名单,不通过
            //}

            //#endregion

            //if ("-1".Equals(status))
            //{
            //    this.ErrMsg = msg;
            //    return true;//相当于在黑名单,不通过
            //}

            return false;
        }

        public bool IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return false;
        }

        public bool IsUploadAllFeeDetailsOutpatient
        {
            get { return true; }
        }

        public int LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        public int MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }


        public int PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
            string msg = string.Empty;
            string status = string.Empty;
            if (true)
            {
                //TODO:查询佛山医保住院结算信息
                //最大序号的有效结算信息                 
                if (this.inPatientService.QuerySiBalanceInfo(ref patient, ref personInfo, ref msg) < 1)
                {
                    this.ErrMsg = msg;
                    return -1;
                }
                patient.FT.OwnCost = personInfo.akb067;
                patient.FT.PubCost = personInfo.ake149;
                patient.FT.TotCost = personInfo.ake149 + personInfo.akb067;
                patient.Pact.PayKind.ID = "02";
                patient.SIMainInfo.OwnCost = personInfo.akb067;
                patient.SIMainInfo.PayCost = 0;
                patient.SIMainInfo.PubCost = personInfo.ake149;

                return 1;
            }
            else
            {
                //TODO:查询异地医保住院结算信息
            }
            return 1;
        }


        public int PreBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
           
            return 1;
            ////初始化回滚备份信息
            //this.curOutPatientName = "";
            //this.curOutPatientSIType = "";
            //this.curOutPatientBalanceSeq = "";
            //this.curOutPatientFeeTot = "";
            //this.curOutPatientBalanceDate = r.SIMainInfo.BalanceDate;
            //this.pact = null;


            //if (string.IsNullOrEmpty(r.Kin.RelationPhone) || (string.IsNullOrEmpty(r.SSN) && string.IsNullOrEmpty(r.IDCard)))
            //{
            //    this.UploadRegInfoOutpatient(r);
            //}

            ////直接门诊结算
            //this.errMsg = "";
            //string status = "";
            //string msg = "";
            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在联网结算,请稍候...");
            //outPatientService.OutPatientBalance(r, feeDetails, ref status, ref msg);
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //#region 回滚数据备份

            //this.curOutPatientName = r.Name;
            //this.curOutPatientSIType = r.SIMainInfo.SiType;
            //this.curOutPatientBalanceSeq = r.SIMainInfo.RegNo;
            //this.curOutPatientFeeTot = r.SIMainInfo.TotCost.ToString();
            //this.curOutPatientBalanceDate = r.SIMainInfo.BalanceDate;
            //this.pact = r.Pact.Clone();
            ////this.callStatus = "BalanceOutpatient";

            //#endregion
            //if (status == null || "".Equals(status) || int.Parse(status) != 1)
            //{
            //    this.errMsg = msg;

            //    //中心结算成功，本地插入数据库失败
            //    if (status.Equals("4"))
            //    {
            //        //需要保存相关信息,用于回滚时取消结算                  
            //        this.callStatus = "BalanceOutpatient";
            //    }
            //    return -1;
            //}
            //else
            //{
            //    //需要保存相关信息,用于回滚时取消结算
            //    this.callStatus = "BalanceOutpatient";
            //    return 1;
            //}
        }

        public int QueryBlackLists(ref System.Collections.ArrayList blackLists)
        {
            return 1;
        }

        public int QueryCanMedicare(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        public int QueryDrugLists(ref System.Collections.ArrayList drugLists)
        {
            return 1;
        }

        /// <summary>
        /// 用于计算费用-用于住院审核临时医嘱和分解长期医嘱的控费
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            //计算费用的总额-有待完善
            //公费计算
            FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();
            if (feeDetails != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in feeDetails)
                {
                    ft.TotCost += feeItemList.FT.TotCost;
                    ft.OwnCost += feeItemList.FT.OwnCost;
                    ft.PayCost += feeItemList.FT.PayCost;
                    ft.PubCost += feeItemList.FT.PubCost;
                    ft.RebateCost += feeItemList.FT.RebateCost;
                    ft.DefTotCost += feeItemList.FT.DefTotCost;
                }

                ft.TotCost = FS.FrameWork.Public.String.FormatNumber(ft.TotCost, 2);
                ft.OwnCost = FS.FrameWork.Public.String.FormatNumber(ft.TotCost, 2);
                ft.PayCost = 0;
                ft.PubCost = ft.TotCost - ft.OwnCost;
                ft.RebateCost = FS.FrameWork.Public.String.FormatNumber(ft.RebateCost, 2);
                ft.DefTotCost = FS.FrameWork.Public.String.FormatNumber(ft.DefTotCost, 2);
            }

            patient.FT = ft;
            return 1;
        }

        public int QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
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
            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
            //this.outPatientService.SetTrans(t);//by han-zf
            this.inPatientService.SetTrans(t);//by han-zf
        }

        public System.Data.IDbTransaction Trans
        {
            set { this.trans = value; }
        }

        public int UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        public int UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        public int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        /// <summary>
        /// 住院登记
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int UploadRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            FS.HISFC.Models.RADT.PatientInfo patientTemp = new FS.HISFC.Models.RADT.PatientInfo();
            patientTemp = patient.Clone();

            //TODO:异地医保
            ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
            string msg = string.Empty;
            string status = string.Empty;

            ProvinceAcrossSI.Controls.frmInpatientRegYiDi frm = new ProvinceAcrossSI.Controls.frmInpatientRegYiDi();
            int iniFlag = frm.InitControl(patient);
            if (iniFlag < 1)
            {
                this.ErrMsg = frm.ErrMsg;
                return -1;
            }
            frm.inpateintReg += this.inPatientService.YDInPatientReg;
            frm.ShowDialog();


            if (frm.ErrCode < 0)
            {
                this.ErrMsg = "\n" + frm.ErrMsg;
                return -1;
            }

            if (!frm.isClickBtnEnter)
            {
                this.ErrMsg = "\n查询住院资格完成后，必须点击确定按钮以进行中心登记！";
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
            return 1;
        }

        #endregion

        #region IMedcareTranscation 成员

        public void BeginTranscation()
        {

        }

        public long Commit()
        {
            return 1;
        }

        public long Connect()
        {
            ////通过实例化服务对象来判断连接是否成功
            //this.errMsg = "";
            //try
            //{
            //    ProvinceAcrossSIService.outwebserviceClient client = new ProvinceAcrossSI.ProvinceAcrossSIService.outwebserviceClient(new System.ServiceModel.BasicHttpBinding(), new System.ServiceModel.EndpointAddress("http://172.16.0.6/outservice/services/outwebservice"));
            //    if (client == null)
            //    {
            //        this.errMsg = "连接佛山医保服务失败!";
            //        return -1;
            //    }
            //}
            //catch (Exception e)
            //{
            //    this.errMsg = "连接佛山医保服务失败!\n错误信息：" + e.Message;
            //    return -1;
            //}

            return 1;
        }

        public long Disconnect()
        {
            return 1;
        }

        public long Rollback()
        {
            //this.errMsg = "";
            //string status = "";
            //string msg = "";

            //if (this.outFlag && "BalanceOutpatient".Equals(callStatus))
            //{
            //    //门诊,由于调用预结算时是直接结算,所以需要根据相关参数取消结算
            //    FS.HISFC.Models.Registration.Register patientInfo = new FS.HISFC.Models.Registration.Register();
            //    patientInfo.Name = this.curOutPatientName;
            //    patientInfo.SIMainInfo.SiType = this.curOutPatientSIType;
            //    patientInfo.SIMainInfo.RegNo = this.curOutPatientBalanceSeq;
            //    patientInfo.SIMainInfo.TotCost = Decimal.Parse(this.curOutPatientFeeTot);
            //    patientInfo.SIMainInfo.BalanceDate = this.curOutPatientBalanceDate;
            //    patientInfo.Pact = this.pact.Clone();

            //    outPatientService.OutPatientCancelBalance(patientInfo, ref status, ref msg);

            //    if (status == null || "".Equals(status) || int.Parse(status) > 1)
            //    {
            //        this.errMsg = "取消结算失败,请在医保网站手工取消结算!\n错误信息:\n" + msg;
            //        return -1;
            //    }
            //    else
            //    {
            //        //取消结算成功,清空结算相关参数
            //        this.curOutPatientName = string.Empty;
            //        this.curOutPatientSIType = string.Empty;
            //        this.curOutPatientBalanceSeq = string.Empty;
            //        this.curOutPatientFeeTot = string.Empty;
            //        MessageBox.Show("医保中心结算回滚成功！", "佛山医保");
            //    }
            //}
            //else
            //{
            //    //住院
            //}

            return 1;
        }


        #endregion

        #region IMedcare 成员

        //int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        //{
        //    return 1;
        //}

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.CancelBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        string FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.Description
        {
            get { return ""; }
        }

        //string FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.ErrCode
        //{
        //    get { return 1; }
        //}

        //int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        //{
        //    return 1;
        //}

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        bool FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.IsInBlackList(FS.HISFC.Models.Registration.Register r)
        {
            return true;
        }

        bool FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return true;
        }

        bool FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.IsUploadAllFeeDetailsOutpatient
        {
            get { return true; }
        }

        //int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        //{
        //    return 1;
        //}

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.ModifyUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        //int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        //{
        //    return 1;
        //}

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.PreBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.QueryBlackLists(ref System.Collections.ArrayList blackLists)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.QueryCanMedicare(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.QueryDrugLists(ref System.Collections.ArrayList drugLists)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.QueryUndrugLists(ref System.Collections.ArrayList undrugLists)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.RecallRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            return 1;
        }

        //void FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.SetTrans(IDbTransaction t)
        //{
        //    return 1;
        //}

        //IDbTransaction FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.Trans
        //{
        //    set { return 1; }
        //}

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        int FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare.UploadRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        #endregion

        #region IMedcareTranscation 成员


        long FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareTranscation.Connect()
        {
            return 1;
        }

        long FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareTranscation.Disconnect()
        {
            return 1;
        }

        long FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareTranscation.Rollback()
        {
            return 1;
        }

        #endregion
    }
}
