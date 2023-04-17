using System;
using System.Data;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FoShanDiseasePay.Jobs
{

    /*
        *注意：
        *1、挂号的诊查费；
        *2、组合项目上传(分拆) - 禅城区没有
        *
        *3、电子病历内容上传 - 出院小结【新版电子病历和旧版电子病历】
        *
        *4、检查信息上传/检验信息上传
        *5、HIS与平台参数对照 【参考 明细上传的对照关系】
        * 
        * */

    /// <summary>
    /// OutpatientJob 的摘要说明。
    /// 门诊业务信息
    /// </summary>
    public class PayDiseaseJob : BaseJob
    {
        public PayDiseaseJob()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

            ArrayList alDept = this.dataBaseHelp.getConstDict("SporadicDept");
            fsDeptHelper.ArrayObject = alDept;
        }

        string SessionNO = string.Empty;

        //private DataBaseHelp dbHelp = new DataBaseHelp();

        Neusoft.FrameWork.Public.ObjectHelper LisHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        Neusoft.FrameWork.Public.ObjectHelper fsDeptHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        //Neusoft.FrameWork.Public.ObjectHelper diagHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        public override void Start()
        {
            //初始化log文件名
            this.logFileName = @".\log\Detail" + this.endTime + ".log";
            try
            {
                DateTime dtBegin = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.startTime).AddDays(-180);
                DateTime dtEnd = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.endTime).AddDays(-1);
                StringBuilder sb = new StringBuilder();

                //ArrayList alDiagNoses  = dbHelp.QueryDiagNoseInfoToday(dtBegin.ToString("yyyy-MM-dd"), dtEnd.ToString("yyyy-MM-dd"));
                //diagHelper.ArrayObject = alDiagNoses;

                #region 门诊上传
                WriteLog("******************************门诊信息开始上传**************************\n");

                if (false)
                {
                    ArrayList alOutpatient = dbHelp.QueryOutpatientInvoiceInfo4Pay(dtBegin.ToString("yyyy-MM-dd"), dtEnd.ToString("yyyy-MM-dd"));

                    WriteLog("查询到门诊患者发票信息" + alOutpatient.Count + "条记录\n");

                    foreach (Neusoft.HISFC.Models.Fee.Outpatient.Balance balance in alOutpatient)
                    {
                        if (string.IsNullOrEmpty(balance.Patient.IDCard))
                        {
                            WriteLog(balance.Patient.ID + "没有上传@身份证号码为空！");
                            continue;
                        }

                        if (string.IsNullOrEmpty(balance.Memo))
                        {
                            WriteLog(balance.Patient.ID + "没有上传@社保单号为空！");
                            continue;
                        }

                        int result = 0;

                        if (balance.Invoice.ValidState == "0")
                        {
                            result = this.UploadCancelSIInfo(balance.ID, balance.Memo);
                        }
                        else
                        {
                            result = this.QueryUploadedInfo(balance.ID, balance.Memo);

                            if (result < 0) //表示没有查询到 进行上传操作
                            {
                                result = this.UploadSIInfo(balance.Patient.ID, balance.Patient.PID.CardNO, balance.ID, balance.Memo, true);

                                if (result <= 0)
                                {
                                    if (result == 0)
                                    {
                                        WriteLog(balance.Patient.ID + "诊断为自定义诊断不上传！");
                                        continue;
                                    }
                                    WriteLog(balance.Patient.ID + " " + balance.ID + "上传出错@" + errMsg);
                                    continue;
                                }
                            }
                        }
                    }

                }

                WriteLog("******************************门诊信息上传完成**************************\n");

                #endregion

                #region 住院上传

                WriteLog("******************************住院信息开始上传**************************\n");

                ArrayList alInpatient = dbHelp.QueryInpatientInvoiceInfo4PayNew(dtBegin.ToString("yyyy-MM-dd"), dtEnd.ToString("yyyy-MM-dd"));

                #region 废弃
                //ArrayList alInpatient = new ArrayList();
                //Neusoft.HISFC.Models.Fee.Inpatient.Balance balancec = new Neusoft.HISFC.Models.Fee.Inpatient.Balance();
                //balancec.ID = "101879100934";
                //balancec.Patient.ID = "ZY020000150194";
                //balancec.Patient.Name = "2";
                //balancec.FT.TotCost = 3457.07m;
                //balancec.FT.PubCost = 2544.96m;
                //balancec.FT.PayCost = 912.11m;
                //balancec.Patient.IDCard = "429001198211074990";
                //balancec.Memo = "70829473";
                //alInpatient.Add(balancec);
                #endregion

                if (alInpatient != null && alInpatient.Count > 0)
                {

                    WriteLog("查询到住院患者发票信息" + alInpatient.Count + "条记录\n");

                    foreach (Neusoft.HISFC.Models.Fee.Inpatient.Balance invoice in alInpatient)
                    {
                        if (string.IsNullOrEmpty(invoice.Patient.IDCard))
                        {
                            WriteLog(invoice.Patient.ID + "没有上传@身份证号码为空！");
                            continue;
                        }
                        int result = 0;

                        //查询住院患者的主表
                        Neusoft.HISFC.Models.RADT.PatientInfo inPatientInfo = dbHelp.QueryPatientInfoByInpatientNO(invoice.Patient.ID);
                        if (inPatientInfo == null || string.IsNullOrEmpty(inPatientInfo.ID))
                        {
                            WriteLog("未找到【" + invoice.Patient.ID + "】的住院信息!");
                            continue;
                        }

                        //查询住院患者的发票信息
                        ArrayList balanceList = dbHelp.QueryBalances(invoice.Invoice.ID);
                        if (balanceList == null || balanceList.Count <= 0)
                        {
                            WriteLog("发票号【" + invoice.Invoice.ID + "】，未找到对应的结算信息!");
                            continue;
                        }
                        if (balanceList.Count > 1)
                        {
                            WriteLog("发票号【" + invoice.Invoice.ID + "】，已经退费!");
                            continue;
                        }

                        Neusoft.HISFC.Models.Fee.Inpatient.Balance balance = balanceList[0] as Neusoft.HISFC.Models.Fee.Inpatient.Balance;
                        if (balance == null || string.IsNullOrEmpty(balance.Invoice.ID))
                        {
                            WriteLog("发票号【" + balance.Invoice.ID + "】，未找到对应的结算信息!");
                            continue;
                        }

                        //发票对应的社保返回信息
                        string siMemo = dbHelp.GetSiInfo(balance.Patient.ID, balance.Invoice.ID, "2");
                        string siReturnID = string.Empty;
                        if (!string.IsNullOrEmpty(siMemo))
                        {
                            string[] arr = siMemo.Split('|');
                            if (arr.Length > 0)
                            {
                                siReturnID = arr[0];
                            }
                        }
                        string wylsh = Manager.setObj.HospitalID + "-2-" + balance.Invoice.ID + "-" + siReturnID;  //主键

                        //查询诊断信息
                        ArrayList alDiag = dbHelp.QueryCaseDiagnoseForClinicByState(inPatientInfo.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
                        string firstDiag = string.Empty;
                        string secondDiag = string.Empty;

                        #region 获取诊断

                        if (alDiag != null && alDiag.Count > 0)
                        {
                            int i = 0;
                            foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                            {
                                if (diag.IsValid && diag.PerssonType == Neusoft.HISFC.Models.Base.ServiceTypes.I)  //患者类别 0 门诊患者 1 住院患者
                                {
                                    i++;
                                    if (i == 1)
                                    {
                                        firstDiag = diag.DiagInfo.ICD10.ID;
                                    }
                                    else if (i == 2)
                                    {
                                        secondDiag = diag.DiagInfo.ICD10.ID;
                                    }

                                }
                            }
                        }

                        #endregion


                        //5.1 就诊单据信息上传(HJ001)
                        if (balance.CancelType != Neusoft.HISFC.Models.Base.CancelTypes.Valid)
                        {
                            result = this.UploadCancelSIInfo(wylsh, siReturnID);
                        }
                        else
                        {
                            result = this.QueryUploadedInfo(wylsh, siReturnID);
                            if (result < 0) //表示没有查询到 进行上传操作
                            {
                                result = this.UploadSIInfo(balance.Patient.ID, balance.Patient.PID.PatientNO, balance.Invoice.ID, siReturnID, false);
                                if (result <= 0)
                                {
                                    if (result == 0)
                                    {
                                        WriteLog(balance.Patient.ID + "诊断为自定义诊断不上传！");
                                        continue;
                                    }
                                    WriteLog(balance.Patient.ID + " " + balance.ID + "上传出错@" + errMsg);
                                    continue;
                                }
                            }
                        }

                        //5.7 病案信息上传(HB5001)
                        result = this.QueryCaseInfo(balance.Patient.PID.CardNO);
                        if (result < 0) //表示没有查询到 进行上传操作
                        {
                            result = this.UploadCaseInfo(balance.Patient.ID, balance.Patient.IDCard, balance.Memo, "", DateTime.Now, DateTime.Now);
                            if (result <= 0)
                            {
                                WriteLog(balance.Patient.ID + "上传病案信息出错@" + errMsg);
                                continue;
                            }
                        }
                    }
                }

                WriteLog("******************************住院信息上传完成**************************\n");

                #endregion

            }
            catch (Exception ex)
            {
                this.WriteLog(ex.Message);
            }
            finally
            {

            }
        }

        #region 佛山分值付费系统

        //5.1	就诊单据信息上传(HJ001)
        private int UploadSIInfo(string patientID, string patientNO, string invoiceNO, string balanceBusNo, bool isOutpatient)
        {
            string json = @"'wylsh':'{0}','sfdjh':'{1}','djjsrq':'{2}','ddjgbm':'{3}','ddjgmc':'{4}','fyfsjgbm':'{5}',
                            'fyfsjgmc':'{6}','zwjgmc':'{7}','gmsfhm':'{8}','cbrmc':'{9}','cbrxb':'{10}','cbrcsrq':'{11}',
                            'rylbbm':'{12}','jzfsbm':'{13}','sfydjy':'{14}','ryzdbm':'{15}','cyzdbm':'{16}','fzd1':'{17}',
                            'fzd2':'{18}','fzd3':'{19}','fzd4':'{20}','fzd5':'{21}','fzd6':'{22}','fzd7':'{23}','fzd8':'{24}',
                            'fzd9':'{25}','fzd10':'{26}','fzd11':'{27}','fzd12':'{28}','fzd13':'{29}','fzd14':'{30}','fzd15':'{31}',
                            'fzd16':'{32}','sftbmbdjbz':'{33}','mbtbdm':'{34}','ryrq':'{35}','cyrq':'{36}','jzrq':'{37}','sfhy':'{38}',
                            'sfbrq':'{39}','sg':'{40}','tz':'{41}','xy':'{42}','kfxt':'{43}','chxt':'{44}','tw':'{45}','xl':'{46}',
                            'sfzry':'{47}','ybzxdm':'{48}','ydrylb':'{49}','ydcbxzqydm':'{50}','dylx':'{51}','cblx':'{52}','zje':'{53}',
                            'ybnzje':'{54}','ybnzfje':'{55}','zfbl':'{56}','zhfbl':'{57}','xyf':'{58}','zchyf':'{59}','zcyf':'{60}',
                            'ybylfwf':'{61}','ybzlczf':'{62}','hlf':'{63}','blzdf':'{64}','syszdf':'{65}','yxxzdf':'{66}','lczdxmf':'{67}',
                            'sszlf':'{68}','mjf':'{69}','fsszlxmf':'{70}','lcwlzlf':'{71}','kff':'{72}','xf':'{73}','bdblzpf':'{74}',
                            'qdblzpf':'{75}','nxyzlzpf':'{76}','xbyzlzpf':'{77}','jcyclf':'{78}','zlyclf':'{79}','ssyclf':'{80}',
                            'mzzyh':'{81}','cjrbs':'{82}','yylb':'{83}','cjdjh':'{84}','jjlx':'{85}','ssdm':'{86}','ssqkfl':'{87}',
                            'cyzry':'{88}',detail: [{89}]";

            string innerJson = @"'sfdjh':'{0}','wylsh':'{1}','fwrq':'{2}','xmbm':'{3}','xmmc':'{4}','xmlb':'{5}',
                            'dj':'{6}','sl':'{7}','zj':'{8}','ysbm':'{9}','ysmc':'{10}','ksbm':'{11}',
                            'ksmc':'{12}','yf':'{13}','gytj':'{14}','yl':'{15}','pc':'{16}','yyts':'{17}',
                            'ybnje':'{18}','jylx':'{19}','ypgg':'{20}','cydybs':'{21}','zyh':'{22}',
                            'yzzdid':'{23}','yzmxid':'{24}'";

            if (isOutpatient)
            {
                #region 门诊

                #region 明细项目处理

                ArrayList comFeeItemLists = dbHelp.QueryFeeItemListsByInvoiceNO4Pay(invoiceNO);

                ArrayList tempFeeDetails = new ArrayList();
                tempFeeDetails = new ArrayList();
                Hashtable hsUpLoadFeeDetails = new Hashtable();
                Neusoft.HISFC.Models.Fee.FeeItemBase feeItemList = null;

                foreach (Neusoft.HISFC.Models.Fee.FeeItemBase f in comFeeItemLists)
                {
                    this.DealFeeItemList(f);

                    if (string.IsNullOrEmpty(f.Item.UserCode) || f.Item.UserCode == "0") //2010-9-1
                    {
                        this.WriteLog("项目" + f.Item.Name + "没有对照码");
                        tempFeeDetails = new ArrayList();
                        return -1;
                    }

                    if (hsUpLoadFeeDetails.ContainsKey(f.Item.UserCode))
                    {
                        feeItemList = hsUpLoadFeeDetails[f.Item.UserCode] as Neusoft.HISFC.Models.Fee.FeeItemBase;

                        feeItemList.Item.Qty += f.Item.Qty;//数量累加
                        feeItemList.FT.TotCost += f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost;//金额累加
                    }
                    else
                    {
                        f.FT.TotCost = f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                        hsUpLoadFeeDetails.Add(f.Item.UserCode, f);
                        tempFeeDetails.Add(f);
                    }
                }

                hsUpLoadFeeDetails = null;
                comFeeItemLists = null;

                int days = 0;

                string type = string.Empty;

                StringBuilder sb = new StringBuilder();

                Neusoft.FrameWork.Models.NeuObject deptObj = null;

                int seqNo = 1;
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList f in tempFeeDetails)
                {
                    if (f.Item.Qty == 0)//数量为0的不上传
                        continue;

                    if (f.TransType == Neusoft.HISFC.Models.Base.TransTypes.Positive)
                    {
                        type = "1";
                    }
                    else
                    {
                        type = "2";
                    }

                    if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.UnDrug)
                    {
                        f.Item.PriceUnit = "0";
                    }

                    deptObj = fsDeptHelper.GetObjectFromID(f.ConfirmOper.Dept.ID);

                    sb.Append(string.Format(innerJson,
                                  balanceBusNo,
                                  seqNo++,
                        //htDetail[f.Item.UserCode].ToString(),
                                  f.FeeOper.OperTime.ToString("yyyyMMdd"),
                                  f.Item.UserCode,
                                  f.Item.Name,
                                  f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug ? "1" : "0",
                                  f.Item.Price.ToString(),
                                  f.Item.Qty.ToString(),
                                  f.FT.TotCost.ToString(),
                                  f.ConfirmOper.ID,
                                  f.RecipeOper.Name,
                        //f.ConfirmOper.Dept.ID,
                                  deptObj.Memo,//编码 
                                  deptObj.Name, //名称
                        //f.RecipeOper.Dept.Name,
                                  string.IsNullOrEmpty(f.Item.PriceUnit) ? "0" : f.Item.PriceUnit,
                                  string.IsNullOrEmpty(f.Order.Usage.Name) ? "900" : f.Order.Usage.Name,
                                  f.Item.Qty.ToString(),
                                  string.IsNullOrEmpty(f.Order.Frequency.ID) ? "62" : f.Order.Frequency.ID,
                                  days.ToString(),
                                  "0",
                                  type,
                                  string.IsNullOrEmpty(f.Item.Specs) ? "0" : f.Item.Specs,
                                  "0",
                                  f.Patient.PID.CardNO,
                                  string.IsNullOrEmpty(f.Order.ID) ? f.RecipeNO + f.SequenceNO.ToString() + type : f.Order.ID,
                                  string.IsNullOrEmpty(f.Order.ID) ? f.RecipeNO + f.SequenceNO.ToString() + type : f.Order.ID));
                    sb.Append(",");
                }
                #endregion

                Neusoft.HISFC.Models.Registration.Register register = dbHelp.QuerySIOutpatient4Pay(patientID, invoiceNO);

                json = string.Format(json, Manager.setObj.HospitalID + "-" + register.InvoiceNO + "-" + balanceBusNo, balanceBusNo, register.InputOper.OperTime.ToString("yyyyMMdd"),
                    Manager.setObj.HospitalID, Manager.setObj.HospitalName, Manager.setObj.HospitalID, Manager.setObj.HospitalName,
                    "", register.SSN, register.Name, register.Sex.ID.ToString(), register.Birthday.ToString("yyyyMMdd"), register.Pact.ID, register.Pact.Name,
                    "", register.ClinicDiagnose, register.ClinicDiagnose,//当类型为12时，可以不用传次要诊断
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", register.DoctorInfo.SeeDate.ToString("yyyyMMdd"), register.DoctorInfo.SeeDate.ToString("yyyyMMdd"),
                    register.DoctorInfo.SeeDate.ToString("yyyyMMdd"), "", "", "", "", "", "", "", "", "",
                    "", "440606", "", "", "", register.Pact.PayKind.ID, register.PayCost.ToString(), register.PubCost.ToString(), "0", register.OwnCost.ToString(), register.SIMainInfo.TotCost.ToString(),
                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", register.PID.CardNO, "", register.DIST, register.InputOper.Memo, register.InputOper.Oper.Memo,
                    "", "0", "", sb.ToString().TrimEnd(','));

                #endregion
            }
            else
            {
                #region 住院

                string wylsh = Manager.setObj.HospitalID + "-2-" + invoiceNO + "-" + balanceBusNo;  //主键

                #region 明细处理

                //获取非药品明细
                ArrayList feeDetails = dbHelp.QueryFeeItemListsByInpatientNOAndInvoiceNO("FoShanSI.SIUploadJob.PayDisease.QueryFeeItemListsByInpatientNOAndInvoiceNO", patientID, invoiceNO);
                //获取药品明细
                ArrayList feeDetailsDrug = dbHelp.QueryMedItemListsByInpatientNOAndInvoiceNO("FoShanSI.SIUploadJob.PayDisease.QueryMedItemListsByInvoiceNO", patientID, invoiceNO);
                if (feeDetailsDrug != null)
                {
                    feeDetails.AddRange(feeDetailsDrug);
                }

                Hashtable hsUpLoadFeeDetails = new Hashtable();

                ArrayList tempFeeDetails = new ArrayList();

                ArrayList alFeeDetails = this.SplitFeeItemList(feeDetails);
                string keyCode = string.Empty;

                foreach (Neusoft.HISFC.Models.Fee.FeeItemBase f in alFeeDetails)
                {
                    this.DealFeeItemList(f);

                    if (string.IsNullOrEmpty(f.Item.UserCode) || f.Item.UserCode == "0") //2010-9-1
                    {
                        this.WriteLog("项目" + f.Item.Name + "没有对照码");
                        tempFeeDetails = new ArrayList();
                        break;
                    }

                    keyCode = f.Item.UserCode + f.FeeOper.OperTime.ToString("yyyyMMdd");
                    if (hsUpLoadFeeDetails.ContainsKey(keyCode))
                    {
                        Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = hsUpLoadFeeDetails[keyCode] as
                            Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                        feeItemList.Item.Qty += f.Item.Qty;//数量累加
                        feeItemList.FT.TotCost += f.FT.TotCost;//金额累加
                    }
                    else
                    {
                        hsUpLoadFeeDetails.Add(keyCode, f);
                        tempFeeDetails.Add(f);
                    }
                }

                int days = 0;

                string type = string.Empty;

                StringBuilder sb = new StringBuilder();

                Neusoft.FrameWork.Models.NeuObject deptObj = null;

                int seqNo = 1;

                foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f in tempFeeDetails)
                {
                    if (f.Item.Qty == 0)//数量为0的不上传
                        continue;

                    if (f.TransType == Neusoft.HISFC.Models.Base.TransTypes.Positive)
                    {
                        type = "1";
                    }
                    else
                    {
                        type = "2";
                    }

                    if (string.IsNullOrEmpty(f.RecipeOper.Dept.ID))
                    {
                        f.RecipeOper.Dept.ID = ((Neusoft.HISFC.Models.RADT.PatientInfo)f.Patient).PVisit.PatientLocation.Dept.ID;
                    }

                    deptObj = fsDeptHelper.GetObjectFromID(f.RecipeOper.Dept.ID);

                    if (deptObj == null)
                    {
                        deptObj = fsDeptHelper.GetObjectFromID(((Neusoft.HISFC.Models.RADT.PatientInfo)f.Patient).PVisit.PatientLocation.Dept.ID);
                    }

                    if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.UnDrug)
                    {
                        f.Item.PriceUnit = "0";
                    }

                    sb.Append(string.Format(innerJson,
                                  balanceBusNo,
                        //htDetail[f.Item.UserCode].ToString(),
                                  seqNo++,
                                  f.FeeOper.OperTime.ToString("yyyyMMdd"),
                                  f.Item.UserCode,
                                  f.Item.Name,
                                  f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug ? "1" : "0",
                                  f.Item.Price.ToString(),
                                  f.Item.Qty.ToString(),
                                  f.FT.TotCost.ToString(),
                                  f.ConfirmOper.ID,
                                  f.RecipeOper.Name,
                        //f.RecipeOper.Dept.ID,
                        //f.MachineNO,
                                  deptObj.Memo,
                                  deptObj.Name,
                                  string.IsNullOrEmpty(f.Item.PriceUnit) ? "0" : f.Item.PriceUnit,
                                  string.IsNullOrEmpty(f.Order.Usage.ID) ? "900" : f.Order.Usage.ID,
                                  f.Item.Qty.ToString(),
                                  string.IsNullOrEmpty(f.Order.Frequency.ID) ? "62" : f.Order.Frequency.ID,
                                  days.ToString(),
                                  "0",
                                  "0",
                                  string.IsNullOrEmpty(f.Item.Specs) ? "0" : f.Item.Specs,
                                  f.ChargeOper.ID == "CD" ? "1" : "0",
                                  patientNO,
                                  string.IsNullOrEmpty(f.Order.ID) ? f.RecipeNO + f.SequenceNO.ToString() + type : f.Order.ID,
                                  string.IsNullOrEmpty(f.Order.ID) ? f.RecipeNO + f.SequenceNO.ToString() + type : f.Order.ID));

                    sb.Append(",");
                }
                #endregion

                Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = dbHelp.QuerySIInpatient4Pay(patientID, invoiceNO);
                Neusoft.HISFC.Models.HealthRecord.Fee inpatientFee = dbHelp.QuerySIInpatientFee4Pay(patientID, invoiceNO);

                json = string.Format(json, wylsh, balanceBusNo, patientInfo.SIMainInfo.OperDate.ToString("yyyyMMdd"),
                    Manager.setObj.HospitalID, Manager.setObj.HospitalName, Manager.setObj.HospitalID, Manager.setObj.HospitalName, "",
                    patientInfo.SSN, patientInfo.Name, patientInfo.Sex.ID, patientInfo.Birthday.ToString("yyyyMMdd"), patientInfo.Pact.ID, patientInfo.PayKind.ID,
                    "", string.IsNullOrEmpty(patientInfo.ClinicDiagnose) ? "无" : patientInfo.ClinicDiagnose, string.IsNullOrEmpty(patientInfo.Disease.Name) ? "无" : patientInfo.Disease.Name, patientInfo.ExtendFlag, patientInfo.ExtendFlag1, patientInfo.ExtendFlag2,
                    patientInfo.User01, patientInfo.User02, patientInfo.User03, patientInfo.Pact.PayKind.User01, patientInfo.Pact.PayKind.User02, patientInfo.Pact.PayKind.User03,
                    patientInfo.PID.User01, patientInfo.PID.User02, patientInfo.PID.User03, patientInfo.Profession.User01, patientInfo.Profession.User02, patientInfo.Profession.User03,
                    patientInfo.SpellCode, "", "", patientInfo.PVisit.InTime.ToString("yyyyMMdd"), patientInfo.PVisit.OutTime.ToString("yyyyMMdd"),
                    patientInfo.PVisit.InTime.ToString("yyyyMMdd"), "", "", "", "", "", "", "", "", "",
                    "", "440606", "", "", "", patientInfo.PayKind.Name, patientInfo.SIMainInfo.TotCost.ToString(), patientInfo.SIMainInfo.PubCost.ToString(),
                    "0", patientInfo.SIMainInfo.OwnCost.ToString(), patientInfo.SIMainInfo.PayCost.ToString(), inpatientFee.User01, inpatientFee.User02, inpatientFee.User03, inpatientFee.DeptInfo.User01,
                    inpatientFee.DeptInfo.User02, inpatientFee.DeptInfo.User03, inpatientFee.FeeInfo.User01, inpatientFee.FeeInfo.User02, inpatientFee.FeeInfo.User03, inpatientFee.MainOutICD.User01,
                    inpatientFee.MainOutICD.User02, inpatientFee.MainOutICD.User03, inpatientFee.OperInfo.User01, inpatientFee.OperInfo.User02, inpatientFee.OperInfo.User03, inpatientFee.ID, inpatientFee.Name,
                    inpatientFee.Memo, inpatientFee.DeptInfo.ID, inpatientFee.DeptInfo.Name, inpatientFee.DeptInfo.Memo, inpatientFee.FeeInfo.ID, inpatientFee.FeeInfo.Name, patientInfo.PID.PatientNO, "",
                    patientInfo.ProCreateNO, patientInfo.AddressHome, patientInfo.AddressBusiness, "", "0", "", sb.ToString().TrimEnd(','));

                #endregion
            }

            string error = string.Empty;
            int result = base.UploadInfo("HJ001", json, ref error);
            return result;

        }

        //5.2	就诊信息作废(HJ002)
        private int UploadCancelSIInfo(string serialNO, string billNO)
        {
            string json = @"'wylsh': '{0}','sfdjh': '{1}'";

            string seqNO = Manager.setObj.HospitalID + "-" + serialNO + "-" + billNO;

            json = string.Format(json, seqNO, billNO);

            string error = string.Empty;

            int result = base.UploadInfo("HJ002", json, ref error);

            return result;
        }

        //5.3	就诊信息查询(HJ003)
        private int QueryUploadedInfo(string serialNO, string billNO)
        {
            string json = @"'wylsh': '{0}','sfdjh': '{1}'";

            string seqNO = Manager.setObj.HospitalID + "-" + serialNO + "-" + billNO;

            json = string.Format(json, seqNO, billNO);

            string error = string.Empty;

            int result = base.UploadInfo("HJ003", json, ref error);

            return result;
        }

        //5.7	病案信息上传(HB5001)
        private int UploadCaseInfo(string inpatientNO, string idenno, string balanceBusNO,string inTimes,DateTime dtBegin,DateTime dtEnd)
        {
            string json = @"'Medical': {
                                    'HospitalId':'{0}','AdmissionNo':'{1}','SciCardNo':'{2}','SciCardIdentified':'{3}',
		                            'OutBedNum':'{4}','AdmissionDate':'{5}','DischargeDate':'{6}','DoctorCode':'{7}',
		                            'DoctorName':'{8}','IsDrugAllergy':'{9}','AllergyDrugCode':'{10}','AllergyDrugName':'{11}',
		                            'IsPathologicalExamination':'{12}','PathologyCode':'{13}','IsHospitalInfected':'{14}',
		                            'HospitalInfectedCode':'{15}','BloodTypeS':'{16}','BloodTypeE':'{17}','LeaveHospitalType':'{18}',
		                            'ChiefComplaint':'{19}','MedicalHistory':'{20}','SurgeryHistory':'{21}','BloodTransHistory':'{22}',
		                            'Marriage':'{23}','Height':'{24}','Weight':'{25}','NewbornDate':'{26}','NewbornWeight':'{27}',
		                            'NewbornCurrentWeight':'{28}','BearPregnancy':'{29}','BearYie':'{30}','AdmissionDiseaseId':'{31}',
		                            'AdmissionDiseaseName':'{32}','DiagnosePosition1':'{33}','DischargeDiseaseId':'{34}',
		                            'DischargeDiseaseName':'{35}','DiagnosePosition2':'{36}','Tsblbs':'{37}',{38}
                                },
                                'ListCheck': [{39}],
                                'ListOperation': [{40}],
                                'LeaveHospital': {41}
                            ";

            string checkJson = @"'CheckId': '{0}','DepartmentCode': '{1}','DepartmentName': '{2}','ApplyProjectCode': '{3}',
                            'ApplyProjectName': '{4}','ApplyDoctor': '{5}','ApplyDoctorName': '{6}','ApplyDatetime': '{7}',
                            'ReportDatetime': '{8}','CheckPositionCode': '{9}','Abnormal': '{10}','CheckResult': '{11}'";

            string operationRecordlJson = @"'OperationRecordNo': '{0}','OperationNo': '{1}','OperationCode': '{2}','OperationName': '{3}','OperationLevel': '{4}',
                            'OperationIncisionClass': '{5}','OperationHealClass': '{6}','IsMajorIden': '{7}','IsIatrogenic': '{8}'";

            string operationDetailJson = @"'OperationRecordNo': '{0}','OperationDoctorCode': '{1}','OperationDoctorName': '{2}','FirstOperdoctorcode': '{3}',
                            'FirstOperdoctorname': '{4}','SecondOperdoctorcode': '{5}','SecondOperdoctorname': '{6}','AnesthesiologistCode': '{7}',
                            'AnesthesiologistName': '{8}','OperationDate': '{9}','OperationFinishDate': '{10}','AnaesthesiaType': '{11}',
                            'IsComplication': '{12}','ComplicationCode': '{13}','ComplicationName': '{14}','OperationRecord': '{15}',
                            'RecordDoctorCode': '{16}','RecordDoctorName': '{17}','ListOperationDetail': [{18}]";

            string leaveHos = @"'DischargeOutcome': '{0}','HospitalizationSituation': '{1}','DtProcess': '{2}','LeaveHospitalStatus': '{3}','LeaveDoctorAdvice': '{4}'";

            string error = string.Empty;

            StringBuilder sb = null;

            #region 检验检查部分 返回checkInfo 

            ArrayList al = dbHelp.QueryPacsResults(inpatientNO, dtBegin, dtEnd);

            sb = new StringBuilder();
            foreach (Neusoft.HISFC.Models.RADT.PatientInfo patient in al)
            {
                sb.Append(string.Format(checkJson,patient.ID,patient.PVisit.PatientLocation.Dept.ID,patient.PVisit.PatientLocation.Dept.Name,
                                    patient.Surety.ID,patient.Surety.Name,patient.DoctorReceiver.ID,patient.DoctorReceiver.Name,patient.PVisit.InTime.ToString("yyyy-MM-dd"),
                                    patient.PVisit.OutTime.ToString("yyyy-MM-dd"),patient.AreaCode,patient.Disease.Memo,patient.Disease.Name));
                sb.Append(',');
            }

            al = dbHelp.QueryLisResults(inpatientNO, inTimes);
            foreach (Neusoft.HISFC.Models.RADT.PatientInfo patient in al)
            {
                sb.Append(string.Format(checkJson, patient.ID, patient.PVisit.PatientLocation.Dept.ID, patient.PVisit.PatientLocation.Dept.Name,
                                    patient.Surety.ID, patient.Surety.Name, patient.DoctorReceiver.ID, patient.DoctorReceiver.Name, patient.PVisit.InTime.ToString("yyyy-MM-dd"),
                                    patient.PVisit.OutTime.ToString("yyyy-MM-dd"), patient.AreaCode, patient.Disease.Memo, patient.Disease.Name));
            }


            string checkInfo = sb.ToString().TrimEnd(',');
            #endregion
                
            #region 手术部分处理 返回sbRecord.toString();
            al = dbHelp.QueryOperationInfo4Pay(inpatientNO);

            //首先进行分组
            Hashtable ht = new Hashtable();
            ArrayList alOper = null;
            foreach (Neusoft.HISFC.Models.HealthRecord.OperationDetail info in al)
            {
                sb = new StringBuilder();
                sb.Append(info.FirDoctInfo.ID).Append(info.SecDoctInfo.ID).Append(info.ThrDoctInfo.ID).Append(info.NarcDoctInfo.ID).Append(info.OperationDate.ToString("yyyyMMdd"));
                string key = sb.ToString();

                if (ht.ContainsKey(key))
                {
                    alOper = ht[key] as ArrayList;
                    alOper.Add(info);
                }
                else
                {
                    alOper = new ArrayList();
                    alOper.Add(info);
                    ht.Add(key, alOper);
                }
            }

            sb = null;
            Neusoft.HISFC.Models.HealthRecord.OperationDetail opTmp = null;

            int recordNum = 1;

            StringBuilder sbRecord = null;
            StringBuilder sbDetail = null;

            foreach (string key in ht.Keys)
            {
                sbRecord = new StringBuilder();
                al = ht[key] as ArrayList;

                opTmp = al[0] as Neusoft.HISFC.Models.HealthRecord.OperationDetail;

                sbDetail = new StringBuilder();

                foreach (Neusoft.HISFC.Models.HealthRecord.OperationDetail opDetail in al)
                { 
                    sbDetail.Append(string.Format(operationDetailJson,recordNum.ToString(),opDetail.HappenNO,opDetail.OperationInfo.ID,
                        opDetail.OperationInfo.Name,"等级",opDetail.NickKind,opDetail.CicaKind,opDetail.HappenNO =="1" ?"1":"0","")).Append(",");
                }

                sbRecord.Append(string.Format(operationRecordlJson, recordNum.ToString(), opTmp.FirDoctInfo.ID, opTmp.FirDoctInfo.Name, opTmp.SecDoctInfo.ID,
                            opTmp.SecDoctInfo.Name, opTmp.ThrDoctInfo.ID, opTmp.ThrDoctInfo.Name, opTmp.NarcDoctInfo.ID, opTmp.NarcDoctInfo.Name,
                            opTmp.OperDate.ToString(), opTmp.OperDate.ToString(), opTmp.CicaKind, "0", "", "", "", "", "", sbDetail.ToString().TrimEnd(','))).Append(",");

                recordNum++;
            }

            sbDetail = null;

            #endregion

            #region 出院小结 leaveHos
            Neusoft.FrameWork.Models.NeuObject obj = null;
            string doct = string.Empty;

            dbHelp.GetEmrOutResult(inpatientNO,out obj);

            leaveHos = string.Format(leaveHos, obj.ID, obj.Memo, obj.User01, obj.User02, obj.User03);

            #endregion

            #region 其他诊断信息

            ArrayList alDiag = dbHelp.QueryDiagNoseInfo(inpatientNO,"2");

            sb = new StringBuilder();
            obj = null;
            for (int i = 1; i < 17; i++)
            {
                if (i <= al.Count)
                {
                    obj = al[i - 1] as Neusoft.FrameWork.Models.NeuObject;
                    sb.Append("'DiagnosisCode").Append(i.ToString()).Append("':'").Append(obj.ID).Append("','DiagnosisName").Append(i.ToString()).Append("':'").Append(obj.Name).Append("',");
                }
                else
                {
                    sb.Append("'DiagnosisCode").Append(i.ToString()).Append("':'','DiagnosisName").Append(i.ToString()).Append("':'',");
                }
            }

            string diagInfo = sb.ToString().TrimEnd(',');
            sb = null;
            obj = null;

            #endregion 

            #region 病案相关信息
            Neusoft.HISFC.Models.HealthRecord.Base metCase = dbHelp.QueryMetCase4Pay(inpatientNO);

            if (metCase == null)
            {
                this.WriteLog("没有查询到患者的病案信息");
                return 1;
            }
            #endregion

            json = string.Format(json, "44061104", metCase.PatientInfo.PID.PatientNO, metCase.PatientInfo.IDCard,"", metCase.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(5),
                metCase.PatientInfo.PVisit.InTime.ToString("yyyy-MM-dd"), metCase.PatientInfo.PVisit.OutTime.ToString("yyyy-MM-dd"), metCase.PatientInfo.DoctorReceiver.ID, metCase.PatientInfo.DoctorReceiver.Name, 
                metCase.AnaphyFlag, metCase.FirstAnaphyPharmacy.ID, metCase.FirstAnaphyPharmacy.Name, metCase.YnFirst, metCase.PathNum,"0","", metCase.ReactionBlood, 
                metCase.RhBlood, metCase.Out_Type,"","","","", metCase.PatientInfo.MaritalStatus.ID, metCase.PatientInfo.Height, metCase.PatientInfo.Weight,"","","","","",
                "入院诊断1", "入院诊断2", "入院诊断3", "出院诊断1", "出院诊断2", "出院诊断3", "", diagInfo, checkInfo, sbRecord.ToString(), leaveHos); 

            int result = base.UploadInfo("HB5001", json, ref error);
            return result;
        }

        //5.8	病案信息查询(HB5002)
        private int QueryCaseInfo(string patientNO)
        {
            string json = @"'AdmissionNo':'{0}','HospitalId':'{1}'";

            string error = string.Empty;

            json = string.Format(json, patientNO, Manager.setObj.HospitalID).Replace("'", "\"");

            int result = base.UploadInfo("HB5002", json, ref error);

            return result;
        }

        //5.9	病案信息作废(HB5003)
        private int CancelCaseInfo(string patientNO)
        {
            string json = @"AdmissionNo:'{0}',HospitalId:'{1}'";

            string error = string.Empty;

            json = string.Format(json, patientNO, Manager.setObj.HospitalID).Replace("'", "\"");

            int result = base.UploadInfo("HB5003", json, ref error);

            return result;
        }

        #endregion

        #region  明细处理

        /// <summary>
        /// 处理费用明细
        /// </summary>
        /// <param name="f"></param>
        private void DealFeeItemList(Neusoft.HISFC.Models.Fee.FeeItemBase f)
        {
            bool isReplace = false;
            if (f.Item.Name.Contains("诊查费") && f.Item.Qty != 0)
            {
                //diagObj = conMgr.GetConstant("DiagSICompare", fitem.Item.ID);

                Neusoft.FrameWork.Models.NeuObject diagObj = dbHelp.QueryItemInfo("FoShanSI.SIUploadJob.QueryDictionaryInfo", f.Item.ID);

                if (diagObj != null && !string.IsNullOrEmpty(diagObj.ID))
                {
                    f.Item.ID = diagObj.ID;
                    isReplace = true;
                }
            }

            string sqlIndex = string.Empty;
            if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
            {
                sqlIndex = "FoShanSI.SIUploadJob.QueryPharmacyInfo";
            }
            else if (f.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                sqlIndex = "FoShanSI.SIUploadJob.QueryUndrugInfo";
            }

            Neusoft.FrameWork.Models.NeuObject obj = dbHelp.QueryItemInfo(sqlIndex, f.Item.ID);

            f.Item.UserCode = obj.Memo;
            f.Memo = "0";
            f.Item.Memo = obj.User01; //剂型
            f.Item.MinFee.ID = obj.User02; //分类编码
            f.Item.MinFee.Name = obj.User03; //分类名称

            if (isReplace)
            {
                f.Item.Name = obj.Name;
                f.Item.Price = Neusoft.FrameWork.Function.NConvert.ToDecimal(obj.User01);
                f.FT.OwnCost = f.Item.Price * f.Item.Qty;
                f.FT.PubCost = 0;
                f.FT.PayCost = 0;
            }

            if (!string.IsNullOrEmpty(f.Compare.CenterItem.ID.Trim()) && f.Compare.CenterItem.ID.Trim().Length > 1) //2010-9-1
            {
                if (f.Compare.CenterItem.ID.Trim().Contains(f.Item.UserCode.Trim()))
                {
                    f.Memo = "1";
                    f.Item.UserCode = f.Compare.CenterItem.ID.Trim();
                }
            }
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        public ArrayList SplitFeeItemList(ArrayList feeItemLists)
        {
            ArrayList al = dbHelp.GetList("FoShanSI.SIUploadJob.QueryConst", "SIPack");

            if (al == null || al.Count == 0)
                return feeItemLists;

            Hashtable htPackage = new Hashtable();

            foreach (Neusoft.HISFC.Models.Base.Const con in al)
            {
                htPackage.Add(con.ID, con);
            }

            ArrayList unPackList = new ArrayList();
            ArrayList packList = new ArrayList();

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList f in feeItemLists)
            {
                if (string.IsNullOrEmpty(f.UndrugComb.ID))
                {
                    unPackList.Add(f);
                }
                else
                {
                    if (htPackage.ContainsKey(f.UndrugComb.ID))
                    {
                        packList.Add(f);
                    }
                    else
                    {
                        unPackList.Add(f);
                    }
                }
            }

            ArrayList alPackList = SetGroupItems(packList);

            unPackList.AddRange(alPackList);

            return unPackList;
        }

        /// <summary>
        /// 对特定套餐项目进行处理
        /// </summary>
        /// <param name="feeItemLists"></param>
        /// <returns></returns>
        public ArrayList SetGroupItems(ArrayList feeItemLists)
        {
            if (feeItemLists.Count == 0)
                return feeItemLists;

            Hashtable htFeeItemList = new Hashtable();
            string key = string.Empty;

            Hashtable htPackAge = new Hashtable();

            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList itemList in feeItemLists)
            {
                if (!string.IsNullOrEmpty(itemList.UndrugComb.ID))
                {
                    if (!htPackAge.Contains(itemList.UndrugComb.ID))
                    {
                        htPackAge.Add(itemList.UndrugComb.ID, itemList);
                    };

                    key = itemList.UndrugComb.ID + "|" + itemList.Item.ID;
                    if (htFeeItemList.ContainsKey(key))
                    {
                        Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList item = htFeeItemList[key] as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                        item.Item.Qty += itemList.Item.Qty;//数量累加
                        item.FT.TotCost += itemList.FT.TotCost;//金额累加
                    }
                    else
                    {
                        htFeeItemList.Add(key, itemList);
                    }
                }
            }

            ArrayList alAfter = new ArrayList();

            foreach (string skey in htFeeItemList.Keys)
            {
                string[] keys = skey.Split('|');

                string packageCode = keys[0];

                if (htPackAge.ContainsKey(packageCode))
                {
                    htPackAge.Remove(packageCode);
                }
                else
                {
                    continue;
                }

                List<Neusoft.HISFC.Models.Fee.Item.UndrugComb> alUndrugs = new List<Neusoft.HISFC.Models.Fee.Item.UndrugComb>();

                alUndrugs = dbHelp.QueryUnDrugztDetail("FoShanSI.SIUploadJob.QueryUnDrugztDetail", packageCode);

                decimal packQty = 0;

                foreach (Neusoft.HISFC.Models.Fee.Item.UndrugComb undrug in alUndrugs)
                {
                    if (undrug.ValidState == "无效")
                    {
                        continue;
                    }

                    decimal packageQty = undrug.Qty;

                    string s = packageCode + "|" + undrug.ID;
                    Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = htFeeItemList[s] as Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList;

                    packQty = feeItem.Item.Qty / packageQty;

                    break;
                }

                if (packQty == 0)
                {
                    continue;
                }

                Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList packFeeItemList = new Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList();

                Neusoft.HISFC.Models.Fee.Item.Undrug item = dbHelp.GetValidItemByUndrugCode("FoShanSI.SIUploadJob.QueryValidUndrugInfo", packageCode);

                packFeeItemList.Item = item;

                packFeeItemList.Item.Qty = packQty;

                packFeeItemList.FT.TotCost = item.Price * packQty;

                alAfter.Add(packFeeItemList);
            }

            return alAfter;
        }

        #endregion

        
    }
}
