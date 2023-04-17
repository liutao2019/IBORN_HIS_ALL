using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;
using System.Management;
using System.Xml;
using FS.HISFC.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.IBeforeSaveOrder.FoShanOrderAudit
{
    /// <summary>
    /// 
    /// 医疗机构门诊/住院医保实时审核接口
    /// 2018-04-30
    /// gumzh
    /// 
    /// </summary>
    public class SiOrderAudit
    {
        #region 变量和属性

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 药品业务管理
        /// </summary>
        private static FS.HISFC.BizLogic.Pharmacy.Item myPha = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 人员业务管理类
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// 医保项目编码对照：0国标码；1自定义码；默认为0国标码
        /// </summary>
        private static string itemCodeCompareType = "-1";

        /// <summary>
        /// 社保对照码
        /// </summary>
        private static Dictionary<string, Const> dictSiItem = new Dictionary<string, Const>();

        /// <summary>
        /// 医嘱审核业务管理层
        /// </summary>
        private static SiOrderAuditBizlogic siMgr = new SiOrderAuditBizlogic();

        /// <summary>
        /// 医疗机构门诊/住院医保实时审核接口地址
        /// </summary>
        private static string foshanSiOrderAuditWebService = string.Empty;

        #endregion

        [DllImport("Audit4Hospital.dll")]
        private static extern string Audit4HospitalPortal(string args);//初始化

        /// <summary>
        /// 住院/门诊医嘱实时审核
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="doctor"></param>
        /// <param name="dept"></param>
        /// <param name="alOrder"></param>
        /// <param name="isOutpatient"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static int AuditOrder(FS.HISFC.Models.RADT.Patient patient, FS.FrameWork.Models.NeuObject doctor, FS.FrameWork.Models.NeuObject dept, ArrayList alOrder, bool isOutpatient, ref string error)
        {
            //初始化控制参数
            if (itemCodeCompareType == "-1")
            {
                itemCodeCompareType = controlParamIntegrate.GetControlParam<string>("FSMZ16", false, "0");
            }

            #region 参数化

            string header = "<?xml version='1.0' encoding='UTF-8'?>" +
                @"<MSG>
	                <HEAD>
                        <VER>1.0</VER>
                        <YLJGDM>{0}</YLJGDM>
                        <YLJGXZQH>{1}</YLJGXZQH>
                        <AGENTIP>{2}</AGENTIP>
                        <AGENTMAC>{3}</AGENTMAC>
                        <SBKH>{4}</SBKH>
                        <SBKSBH>{5}</SBKSBH>
                        <SFZH>{6}</SFZH>
                        <CBRXZQH>{7}</CBRXZQH>
                        <JZWYBH>{8}</JZWYBH>
                        <XXLXM>{9}</XXLXM>
                        <JYSJ>{10}</JYSJ>
	                </HEAD>
                    {11}
                </MSG>";

            string body = string.Empty;

            string claim = @"<PackHospital xmlns='http://schemas.datacontract.org/2004/07/BMI.Engine.Common.Hospital' xmlns:i='http://www.w3.org/2001/XMLSchema-instance'>
                              <HospitalClaim>
                                <BENEFIT_GROUP_ID>{0}</BENEFIT_GROUP_ID>
                                <BENEFIT_TYPE>{1}</BENEFIT_TYPE>
                                <BMI_CONVERED_AMOUNT>{2}</BMI_CONVERED_AMOUNT>
                                <CKC892>{3}</CKC892>
                                <DIAGNOSIS_IN>{4}</DIAGNOSIS_IN>
                                <DIAGNOSIS_OUT>{5}</DIAGNOSIS_OUT>
                                <DIAGNOSIS_TOTHER>{6}</DIAGNOSIS_TOTHER>
                                <GENDER>{7}</GENDER>
                                <HOSPITAL_ID>{8}</HOSPITAL_ID>
                                <HOSPITAL_LEVEL>{9}</HOSPITAL_LEVEL>
                                <HS_AREA_CODE>{10}</HS_AREA_CODE>
                                <HS_DIAGNOSIS_IN_NAME>{11}</HS_DIAGNOSIS_IN_NAME>
                                <HS_DIAGNOSIS_OUT_NAME>{12}</HS_DIAGNOSIS_OUT_NAME>
                                <HS_IN_NUMBER>{13}</HS_IN_NUMBER>
                                <HS_NUMBER>{14}</HS_NUMBER>
                                <HS_PATIENT_NAME>{15}</HS_PATIENT_NAME>
                                <HS_STATUS>{16}</HS_STATUS>
                                <HospitalType>{17}</HospitalType>
                                <ID>{18}</ID>
                                <IN_DATE>{19}</IN_DATE>
                                <IsLactation>{20}</IsLactation>
                                <IsPregnant>{21}</IsPregnant>
                                <MEDICAL_TYPE>{22}</MEDICAL_TYPE>
                                <OUT_DATE>{23}</OUT_DATE>
                                <PATIENT_BIRTH>{24}</PATIENT_BIRTH>
                                <PatientBenefitGroupCode>{25}</PatientBenefitGroupCode>
                                <Patient_IDStr>{26}</Patient_IDStr>
                                <SETTLE_DATE>{27}</SETTLE_DATE>
                                <TOTAL_COST>{28}</TOTAL_COST>
                                <UNUSUAL_FLAG>{29}</UNUSUAL_FLAG>
                                <Z_AACT007>{30}</Z_AACT007>
                                <Z_AACT008>{31}</Z_AACT008>
                                <Z_BAC021>{32}</Z_BAC021>
                              </HospitalClaim>
                              <HospitalClaimDetailSet>
                                {33}
                              </HospitalClaimDetailSet>
                            </PackHospital>";

            string detail = @"<ClaimDetailHospital>
                                      <AKF003>{0}</AKF003>
                                      <AKF005>{1}</AKF005>
                                      <ApprovalNumber>{2}</ApprovalNumber>
                                      <BKA609>{3}</BKA609>
                                      <BKC227>{4}</BKC227>
                                      <COSTS>{5}</COSTS>
                                      <DEPTNAME>{6}</DEPTNAME>
                                      <ELIGIBLE_AMOUNT>{7}</ELIGIBLE_AMOUNT>
                                      <FREQUENCY_INTERVAL>{8}</FREQUENCY_INTERVAL>
                                      <ID>{9}</ID>
                                      <ITEM_DATE>{10}</ITEM_DATE>
                                      <ITEM_ID>{11}</ITEM_ID>
                                      <ITEM_NAME>{12}</ITEM_NAME>
                                      <ITEM_TYPE>{13}</ITEM_TYPE>
                                      <NUMBERS>{14}</NUMBERS>
                                      <PRICE>{15}</PRICE>
                                      <PhysicianLevel>{16}</PhysicianLevel>
                                      <Specification>{17}</Specification>
                                      <USAGE>{18}</USAGE>
                                      <USAGE_DAYS>{19}</USAGE_DAYS>
                                      <USAGE_UNIT>{20}</USAGE_UNIT>
                                      <ZZZ_Flag>{21}</ZZZ_Flag>
                                      <Z_PhysicianAP>{22}</Z_PhysicianAP>
                              </ClaimDetailHospital>";

            #endregion

            //判断
            if (patient == null || doctor == null || dept == null || alOrder == null || alOrder.Count <= 0)
            {
                return 1;
            }

            /*
             * 1、医生级别 获取
             * 2、药品频次获取
             * 3、项目编码
             * 4、包装单位
             * 5、诊断信息
             * 
             * 其他对照
             * 1、人员类别
             * 2、参保类型
             * 3、就医方式
             * 4、参保人特殊保险类型组编码
             */
            FS.FrameWork.Models.NeuObject hosObj = GetAnditSetting();
            if (hosObj == null || string.IsNullOrEmpty(hosObj.ID))
            {
                error = "找不到配置文件/Setting/FoShanSiAudit.xml";
                return -1;
            }
            if (string.IsNullOrEmpty(foshanSiOrderAuditWebService))
            {
                foshanSiOrderAuditWebService = hosObj.User03;
            }
            if (string.IsNullOrEmpty(foshanSiOrderAuditWebService))
            {
                error = "没有维护医疗机构门诊/住院医保实时审核接口地址";
                return 1;
            }

            //当前时间
            DateTime dtNow = myPha.GetDateTimeFromSysDateTime();

            //业务变量
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            FS.HISFC.BizLogic.Manager.Person perMgr = new FS.HISFC.BizLogic.Manager.Person();
            FS.HISFC.BizLogic.Manager.Frequency freqMgr = new FS.HISFC.BizLogic.Manager.Frequency();
            FS.HISFC.Models.Base.Employee emp = perMgr.GetPersonByID(doctor.ID);

            //操作设备IP地址
            string strIP = "";
            //操作设备MAC地址
            string strMac = "unknown";
            try
            {
                //IP地址
                System.Net.IPAddress[] ipAddress = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
                strIP = ipAddress[0].ToString();

                //MAC地址
                System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_NetworkAdapterConfiguration");
                System.Management.ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                    }
                }
                moc = null;
                mc = null;
            }
            catch
            {
                strMac = "unknown";
            }

            decimal totCost = 0m;
            //明细信息
            StringBuilder sbOrdDetail = new StringBuilder();
            string OrdDetailInner = string.Empty;

            //入参信息
            string requestXML = string.Empty;

            if (isOutpatient)
            {
                #region 门诊医嘱

                FS.HISFC.Models.Registration.Register register = patient as FS.HISFC.Models.Registration.Register;

                #region 1、明细信息

                foreach (FS.HISFC.Models.Order.OutPatient.Order outOrd in alOrder)
                {
                    string akf003 = "0";         //帖数，默认0
                    string akf005 = doctor.ID;   //医师编码
                    string approvalNumber = "0"; //预留三，默认0, 非空
                    string bka609 = "1";  //预留一，默认1, 非空
                    string bkc227 = "0"; ; //预留二,默认0，非空
                    string deptName = dept.Name + "|" + doctor.Name;  //科室名称|医生姓名； 例如：本明细科室名称为心胸外科，医生姓名为李四，应该传值：心胸外科|李四
                   string frequencyInterval = outOrd.Frequency.ID;   //用药频次 【需要使用对照关系】 ??gmz??
                    string ordId = "O" + outOrd.ID;   //明细ID 唯一编码。非空
                    string itemDate = dtNow.ToString("yyyy-MM-dd"); //服务日期 yyyy-MM-dd，非空

                    string itemId = outOrd.Item.ID;  //项目编码
                    string itemName = outOrd.Item.Name;  //项目名称
                    string hisCompareCode = string.Empty;
                    if (outOrd.Item.ItemType == EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item drug = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrd.Item.ID);
                        if (drug != null)
                        {
                            outOrd.Item.SpecialPrice = drug.SpecialPrice;
                            if (itemCodeCompareType == "0")
                            {
                                hisCompareCode = drug.GBCode;
                            }
                            else
                            {
                                hisCompareCode = drug.UserCode;
                            }
                        }
                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(outOrd.Item.ID);
                        ;
                        if (undrug != null)
                        {
                            outOrd.Item.SpecialPrice = undrug.SpecialPrice;
                            if (itemCodeCompareType == "0")
                            {
                                hisCompareCode = undrug.GBCode;
                            }
                            else
                            {
                                hisCompareCode = undrug.UserCode;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(hisCompareCode))
                    {
                        if (dictSiItem.ContainsKey(hisCompareCode))
                        {
                            Const c = dictSiItem[hisCompareCode];
                            if (c != null && !string.IsNullOrEmpty(c.UserCode))
                            {
                                itemId = c.UserCode;
                                itemName = c.Memo;
                            }
                        }
                        else
                        {
                            Const c = siMgr.QuerySiCompare(hisCompareCode);
                            if (c != null && !string.IsNullOrEmpty(c.UserCode))
                            {
                                dictSiItem.Add(hisCompareCode, c);
                                itemId = c.UserCode;
                                itemName = c.Memo;
                            }
                        }
                    }

                    decimal costs = Math.Round(outOrd.Item.SpecialPrice * outOrd.Item.Qty / outOrd.Item.PackQty, 2);   //总费用
                    decimal eligibleAmount = costs;
                    string itemType = (outOrd.Item.ItemType == EnumItemType.Drug ? "1" : "0"); //项目类型：1药品；0诊疗、服务设施、耗材
                    decimal numbers = outOrd.Qty;   //数量
                    decimal price = outOrd.Item.SpecialPrice;   //单价
                    string physicianLevel = emp.Level.ID;     //医师级别 【有字典表】  ??gmz??
                    string specification = outOrd.Item.Specs;   //规格
                    string usage = outOrd.DoseOnce.ToString();//每次用量
                    string usageDays = outOrd.HerbalQty.ToString();   //用药天数
                    string usageUnit = outOrd.Unit;   //包装单位
                    if (outOrd.Item.ItemType == EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item drugItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrd.Item.ID);
                        if (drugItem != null)
                        {
                            usageUnit = drugItem.PackUnit;
                        }
                    }
                    string zzzFlag = "0";  //计费标记 默认0
                    string zPhysicianAP = string.Empty;   //医师行政职务

                    //总金额
                    totCost += costs;

                    OrdDetailInner = string.Format(detail, akf003, akf005, approvalNumber, bka609, bkc227,
                                                           costs.ToString(), deptName, eligibleAmount.ToString(), frequencyInterval, ordId,
                                                           itemDate, itemId, itemName, itemType, numbers.ToString(),
                                                           price.ToString(), physicianLevel, specification, usage, usageDays,
                                                           usageUnit, zzzFlag, zPhysicianAP);

                    sbOrdDetail.Append(OrdDetailInner);

                }

                #endregion

                #region 2、主单信息表

                //查询诊断信息
                ArrayList alDiag = diagMgr.QueryCaseDiagnoseForClinicByState(patient.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
                FS.FrameWork.Models.NeuObject firstDiag = new FS.FrameWork.Models.NeuObject();
                FS.FrameWork.Models.NeuObject secondDiag = new FS.FrameWork.Models.NeuObject();

                #region 获取诊断

                if (alDiag != null && alDiag.Count > 0)
                {
                    int i = 0;
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                    {
                        if (diag.IsValid && diag.PerssonType == ServiceTypes.C)  //患者类别 0 门诊患者 1 住院患者
                        {
                            i++;
                            if (i == 1)
                            {
                                firstDiag.ID = diag.DiagInfo.ICD10.ID;
                                firstDiag.Name = diag.DiagInfo.ICD10.Name;
                            }
                            else if (i >= 2)
                            {
                                secondDiag.ID = diag.DiagInfo.ICD10.ID + "|";
                                secondDiag.Name = diag.DiagInfo.ICD10.Name + "|";
                            }

                        }
                    }
                }

                #endregion

                string benefitGroupId = "D0C";   //人员类别 【字典：人员类别】D0C：发放
                string benefitType = "72";   //参保类型【字典：参保类型】 72：门诊医疗
                string bmiConveredAmount = totCost.ToString(); //医保内金额
                string ckc892 = register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd");   //登记时间 yyyy-MM-dd，非空
                string diagnosisIn = firstDiag.ID;   //入院诊断
                string diagnosisOut = firstDiag.ID;   //出院诊断
                string diagnosisOther = secondDiag.ID;   //其他副诊断
                string gender = (register.Sex.ID.ToString() == "M" ? "1" : "0");  //性别
                string hospitalId = hosObj.ID;  //医保分配的医疗机构ID，非空
                string hospitalLevel = hosObj.Memo;   //定点机构等级
                string hsAreaCode = string.Empty;   //病区
                string hsDiagnosisInName = firstDiag.Name;   //入院诊断中文名
                string hsDiagnosisOutName = firstDiag.Name;   //出院诊断中文名
                string hsInNumber = register.PID.CardNO;   //病案号
                string hsNumber = register.PID.CardNO;   //住院号\门诊号
                string hsPatientName = register.Name;   //参保人姓名
                string hsStatus = "0";   //住院状态 0未结算1已结算，默认1.
                string hospitalType = hosObj.User02;  //定点机构类型
                string id = register.ID;     //主单ID 唯一编码.非空
                string inDate = register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd");  //入院日期 yyyy-MM-dd，非空
                string isLactation = "0";   //是否哺乳期 0否1是，默认0
                string isPregnant = "0";    //是否孕期 0否1是，默认0
                string medicalType = "12";    //就医方式   12 特定门诊
                string outDate = register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd");   // 出院日期 yyyy-MM-dd，非空
                string patientBirth = register.Birthday.ToString("yyyy-MM-dd");   //出生日期 yyyy-MM-dd，非空
                string patientBenefitGroupCode = "-1";   //参保人特殊保险类型组编码 见参保人特殊保险类型组编码表。默认-1，非空
                string patientIdStr = register.ID;  //参保人唯一编码 非空
                string settleDate = dtNow.ToString("yyyy-MM-dd");  //门诊单据用实际结算日期；住院单据，如已经出院结算，则用结算日期，如尚未出院结算，用当天费用增量或医嘱增量日期
                //string totCost = totCost.ToString(); //总费用
                string unusualFlag = "0";  //是否异地就医 0否1是, 默认0
                string Z_AACT007 = "0";  //预留一 默认0，非空
                string Z_AACT008 = "0";   //预留二 默认0，非空
                string Z_BAC021 = "0";   //预留三 默认0，非空


                string bodyInfo = string.Format(claim, benefitGroupId, benefitType, bmiConveredAmount, ckc892, diagnosisIn,
                                                       diagnosisOut, diagnosisOther, gender, hospitalId, hospitalLevel,
                                                       hsAreaCode, hsDiagnosisInName, hsDiagnosisOutName, hsInNumber, hsNumber,
                                                       hsPatientName, hsStatus, hospitalType, id, inDate,
                                                       isLactation, isPregnant, medicalType, outDate, patientBirth,
                                                       patientBenefitGroupCode, patientIdStr, settleDate, totCost, unusualFlag,
                                                       Z_AACT007, Z_AACT008, Z_BAC021,
                                                       sbOrdDetail);

                body = "<BODY><ISINCREMENTAL>{0}</ISINCREMENTAL><YSBM>{1}</YSBM>{2}</BODY>";
                body = string.Format(body, "0", doctor.ID, bodyInfo);

                #endregion

                //3199	门诊诊间审核 
                requestXML = string.Format(header, hosObj.ID, hosObj.User01, strIP, strMac, "-",
                                                   "", patient.IDCard, hosObj.User01, patient.PID.CardNO, "3199",
                                                   dtNow.ToString("yyyyMMdd/HHmmss/"), body);

                #endregion
            }
            else
            {
                #region 住院医嘱

                FS.HISFC.Models.RADT.PatientInfo inPatientInfo = patient as FS.HISFC.Models.RADT.PatientInfo;

                ArrayList alNewOrder = new ArrayList();
                foreach (FS.HISFC.Models.Order.Inpatient.Order inOrd in alOrder)
                {
                    if (inOrd.Item.ID != "999")
                    {
                        alNewOrder.Add(inOrd);
                    }
                }
                alOrder = alNewOrder;
                if (alOrder.Count <= 0)
                {
                    return 1;
                }

                #region 1、明细信息

                foreach (FS.HISFC.Models.Order.Inpatient.Order inOrd in alOrder)
                {
                    string akf003 = "0";         //帖数，默认0
                    string akf005 = doctor.ID;   //医师编码
                    string approvalNumber = "0"; //预留三，默认0, 非空
                    string bka609 = "1";  //预留一，默认1, 非空
                    string bkc227 = "0"; ; //预留二,默认0，非空
                    string deptName = dept.Name + "|" + doctor.Name;  //科室名称|医生姓名； 例如：本明细科室名称为心胸外科，医生姓名为李四，应该传值：心胸外科|李四
                     string frequencyInterval = inOrd.Frequency.ID;   //用药频次 【需要使用对照关系】 ??gmz??
                    string ordId = "I" + inOrd.ID;   //明细ID 唯一编码。非空
                    string itemDate = dtNow.ToString("yyyy-MM-dd"); //服务日期 yyyy-MM-dd，非空

                    string itemId = inOrd.Item.ID;  //项目编码
                    string itemName = inOrd.Item.Name;  //项目名称
                    string hisCompareCode = string.Empty;
                    if (inOrd.Item.ItemType == EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item drug = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrd.Item.ID);
                        if (drug != null)
                        {
                            inOrd.Item.SpecialPrice = drug.SpecialPrice;
                            if (itemCodeCompareType == "0")
                            {
                                hisCompareCode = drug.GBCode;
                            }
                            else
                            {
                                hisCompareCode = drug.UserCode;
                            }
                        }
                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(inOrd.Item.ID);

                        inOrd.Item.SpecialPrice = undrug.SpecialPrice;
                        if (itemCodeCompareType == "0")
                        {
                            hisCompareCode = undrug.GBCode;
                        }
                        else
                        {
                            hisCompareCode = undrug.UserCode;
                        }
                    }
                    if (!string.IsNullOrEmpty(hisCompareCode))
                    {
                        if (dictSiItem.ContainsKey(hisCompareCode))
                        {
                            Const c = dictSiItem[hisCompareCode];
                            if (c != null && !string.IsNullOrEmpty(c.UserCode))
                            {
                                itemId = c.UserCode;
                                itemName = c.Memo;
                            }
                        }
                        else
                        {
                            Const c = siMgr.QuerySiCompare(hisCompareCode);
                            if (c != null && !string.IsNullOrEmpty(c.UserCode))
                            {
                                dictSiItem.Add(hisCompareCode, c);
                                itemId = c.UserCode;
                                itemName = c.Memo;
                            }
                        }
                    }
                    decimal costs = Math.Round(inOrd.Item.SpecialPrice * inOrd.Item.Qty / (inOrd.Item.PackQty == 0 ? 1 : inOrd.Item.PackQty), 2);   //总费用
                    decimal eligibleAmount = costs;
                   
                    string itemType = (inOrd.Item.ItemType == EnumItemType.Drug ? "1" : "0"); //项目类型：1药品；0诊疗、服务设施、耗材
                    decimal numbers = inOrd.Qty;   //数量
                    decimal price = inOrd.Item.SpecialPrice;   //单价
                    string physicianLevel = emp.Level.ID;     //医师级别 【有字典表】  ??gmz??
                    string specification = inOrd.Item.Specs;   //规格
                    string usage = inOrd.DoseOnce.ToString();//每次用量
                    string usageDays = inOrd.HerbalQty.ToString();   //用药天数
                    string usageUnit = inOrd.Unit;   //包装单位
                    if (inOrd.Item.ItemType == EnumItemType.Drug)
                    {
                        FS.HISFC.Models.Pharmacy.Item drugItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrd.Item.ID);
                        if (drugItem != null)
                        {
                            usageUnit = drugItem.PackUnit;
                        }
                    }
                    string zzzFlag = "0";  //计费标记 默认0
                    string zPhysicianAP = string.Empty;   //医师行政职务

                    //总金额
                    totCost += costs;

                    OrdDetailInner = string.Format(detail, akf003, akf005, approvalNumber, bka609, bkc227,
                                                           costs.ToString(), deptName, eligibleAmount.ToString(), frequencyInterval, ordId,
                                                           itemDate, itemId, itemName, itemType, numbers.ToString(),
                                                           price.ToString(), physicianLevel, specification, usage, usageDays,
                                                           usageUnit, zzzFlag, zPhysicianAP);

                    sbOrdDetail.Append(OrdDetailInner);

                }

                #endregion

                #region 2、主单信息表

                //查询诊断信息
                ArrayList alDiag = diagMgr.QueryCaseDiagnoseForClinicByState(patient.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
                FS.FrameWork.Models.NeuObject firstDiag = new FS.FrameWork.Models.NeuObject();
                FS.FrameWork.Models.NeuObject secondDiag = new FS.FrameWork.Models.NeuObject();

                #region 获取诊断

                if (alDiag != null && alDiag.Count > 0)
                {
                    int i = 0;
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                    {
                        if (diag.IsValid && diag.PerssonType == ServiceTypes.I)  //患者类别 0 门诊患者 1 住院患者
                        {
                            i++;
                            if (i == 1)
                            {
                                firstDiag.ID = diag.DiagInfo.ICD10.ID;
                                firstDiag.Name = diag.DiagInfo.ICD10.Name;
                            }
                            else if (i == 2)
                            {
                                secondDiag.ID = diag.DiagInfo.ICD10.ID;
                                secondDiag.Name = diag.DiagInfo.ICD10.Name;
                            }

                        }
                    }
                }

                #endregion

                string benefitGroupId = "D0C";   //人员类别 【字典：人员类别】D0C：发放
                string benefitType = "31";   //参保类型【字典：参保类型】 31：职工医疗
                string bmiConveredAmount = totCost.ToString(); //医保内金额
                string ckc892 = inPatientInfo.PVisit.InTime.ToString("yyyy-MM-dd");   //登记时间 yyyy-MM-dd，非空
                string diagnosisIn = firstDiag.ID;   //入院诊断
                string diagnosisOut = firstDiag.ID;   //出院诊断
                string diagnosisOther = secondDiag.ID;   //其他副诊断
                string gender = (inPatientInfo.Sex.ID.ToString() == "M" ? "1" : "0");  //性别
                string hospitalId = hosObj.ID;  //医保分配的医疗机构ID，非空
                string hospitalLevel = hosObj.Memo;   //定点机构等级
                string hsAreaCode = string.Empty;   //病区
                string hsDiagnosisInName = firstDiag.Name;   //入院诊断中文名
                string hsDiagnosisOutName = firstDiag.Name;   //出院诊断中文名

                string hsInNumber = inPatientInfo.PID.PatientNO;   //病案号
                string hsNumber = inPatientInfo.PID.PatientNO;   //住院号\门诊号

                string hsPatientName = inPatientInfo.Name;   //参保人姓名
                string hsStatus = "0";   //住院状态 0未结算1已结算，默认1.
                string hospitalType = hosObj.User02;  //定点机构类型
                string id = inPatientInfo.ID;     //主单ID 唯一编码.非空
                string inDate = inPatientInfo.PVisit.InTime.ToString("yyyy-MM-dd");  //入院日期 yyyy-MM-dd，非空
                string isLactation = "0";   //是否哺乳期 0否1是，默认0
                string isPregnant = "0";    //是否孕期 0否1是，默认0
                string medicalType = "21";    //就医方式   21	普通住院
                string outDate = inPatientInfo.PVisit.OutTime.ToString("yyyy-MM-dd");   // 出院日期 yyyy-MM-dd，非空
                string patientBirth = inPatientInfo.Birthday.ToString("yyyy-MM-dd");   //出生日期 yyyy-MM-dd，非空
                string patientBenefitGroupCode = "-1";   //参保人特殊保险类型组编码 见参保人特殊保险类型组编码表。默认-1，非空
                string patientIdStr = inPatientInfo.ID;  //参保人唯一编码 非空
                string settleDate = dtNow.ToString("yyyy-MM-dd");  //门诊单据用实际结算日期；住院单据，如已经出院结算，则用结算日期，如尚未出院结算，用当天费用增量或医嘱增量日期
                //string totCost = totCost.ToString(); //总费用
                string unusualFlag = "0";  //是否异地就医 0否1是, 默认0
                string Z_AACT007 = "0";  //预留一 默认0，非空
                string Z_AACT008 = "0";   //预留二 默认0，非空
                string Z_BAC021 = "0";   //预留三 默认0，非空


                string bodyInfo = string.Format(claim, benefitGroupId, benefitType, bmiConveredAmount, ckc892, diagnosisIn,
                                                       diagnosisOut, diagnosisOther, gender, hospitalId, hospitalLevel,
                                                       hsAreaCode, hsDiagnosisInName, hsDiagnosisOutName, hsInNumber, hsNumber,
                                                       hsPatientName, hsStatus, hospitalType, id, inDate,
                                                       isLactation, isPregnant, medicalType, outDate, patientBirth,
                                                       patientBenefitGroupCode, patientIdStr, settleDate, totCost, unusualFlag,
                                                       Z_AACT007, Z_AACT008, Z_BAC021,
                                                       sbOrdDetail);

                body = "<BODY><ISINCREMENTAL>{0}</ISINCREMENTAL><YSBM>{1}</YSBM>{2}</BODY>";
                body = string.Format(body, "0", doctor.ID, bodyInfo);

                #endregion

                //3201	住院审核 
                requestXML = string.Format(header, hosObj.ID, hosObj.User01, strIP, strMac, "-",
                                                   "", patient.IDCard, hosObj.User01, patient.PID.CardNO, "3201",
                                                   dtNow.ToString("yyyyMMdd/HHmmss/"), body);

                #endregion
            }

            //入参
            WriteLog(requestXML);

            #region 1、DLL调用

            //string result = Audit4HospitalPortal(requestXML);

            //string[] args = result.Split('|');
            ////0|错误信息	错误消息：调用接口失败1	无审核结果3	返回修改4	强制保存
            //if (args[0] == "0" || args[0] == "3")
            //{
            //    return -1;
            //}
            //else
            //{
            //    return 1;
            //}

            #endregion

            //2、webService调用
            string[] args = { requestXML };
            Object obj = WebAPIHelper.InvokeWebService(foshanSiOrderAuditWebService, "Audit4HospitalPortal", args, ref error);
            if (obj == null)
            {
                return -1;
            }
            string result = obj.ToString();

            //出参
            WriteLog(result);

            #region 错误信息弹出

            try
            {
                string strWarnInfo = string.Empty;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                XmlNode node = doc.SelectSingleNode("MSG/BODY");
                if (node != null && !string.IsNullOrEmpty(node.InnerXml))
                {
                    doc.LoadXml(node.InnerXml);

                    //添加命名空间，这一步一定要有，否则读取不了  
                    XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(doc.NameTable);
                    xmlNamespaceManager.AddNamespace("i", "http://www.w3.org/2001/XMLSchema-instance");

                    XmlNodeList xmlNodeList = doc.DocumentElement.ChildNodes;
                    if (xmlNodeList.Count > 3)
                    {
                        XmlNode cn = xmlNodeList[3];
                        if (cn.HasChildNodes && cn.ChildNodes.Count > 0)
                        {
                            foreach (XmlNode cn1 in cn.ChildNodes)
                            {
                                if (cn1.HasChildNodes && cn1.ChildNodes.Count > 11)
                                {
                                    strWarnInfo += cn1.ChildNodes[3].InnerText + "-" + cn1.ChildNodes[4].InnerText + "-" +
                                                   cn1.ChildNodes[6].InnerText + "-" +
                                                   cn1.ChildNodes[9].InnerText + "-" + cn1.ChildNodes[10].InnerText + "\r\n";
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(strWarnInfo))
                    {
                        strWarnInfo += "是否继续保存?\r\n";
                        System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show(strWarnInfo, "提示",
                                                                          System.Windows.Forms.MessageBoxButtons.YesNo,
                                                                          System.Windows.Forms.MessageBoxIcon.Question,
                                                                          System.Windows.Forms.MessageBoxDefaultButton.Button2);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }

                    #region 废弃

                    //XmlReaderSettings xs = new XmlReaderSettings();
                    //xs.XmlResolver = null;
                    //xs.ProhibitDtd = false;

                    //XmlReader xr = XmlReader.Create(node.InnerXml, xs);
                    //doc.Load(xr);

                    //XmlNamespaceManager nameSpace = new XmlNamespaceManager(doc.NameTable);
                    //nameSpace.AddNamespace("abc", "http://schemas.datacontract.org/2004/07/BMI.Engine.Common.Hospital");

                    //XmlElement resume = doc.DocumentElement;

                    ////读取节点内容(要在节点前加命名空间前缀,如本例"abc:")
                    //string str = resume.SelectSingleNode("abc:ViolationResult", nameSpace).InnerText.Trim();

                    #endregion


                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }

            #endregion

            return 1;

            #region 废弃

            //decimal itemTotCost = 0m;
            //FS.HISFC.Models.Order.Frequency freq = null;
            //FS.HISFC.Models.Pharmacy.Item pha = null;
            //decimal days = 1m;
            //string packUnit = string.Empty;


            //#region 明细处理

            //foreach (FS.HISFC.Models.Order.Order order in alOrder)
            //{
            //    if (order.Item.ItemType == EnumItemType.Drug)
            //    {
            //        pha = myPha.GetItem(order.Item.ID);

            //        packUnit = pha.PackUnit;

            //        //取天数
            //        if (order.Item.SysClass.ID.ToString() == "PCC")
            //        {
            //            days = order.HerbalQty;
            //        }
            //        else
            //        {
            //            //计算天数 【直接取字段】
            //            //if (!order.Item.IsMaterial && ! this.IsUsage(order.Usage.ID.ToString()) && order.Item.SysClass.ID.ToString() != "PCC" && order.Frequency.ID.ToString() != "遵医嘱")
            //            if (!order.Item.IsMaterial && order.Item.SysClass.ID.ToString() != "PCC" && order.Frequency.ID.ToString() != "遵医嘱")
            //            {
            //                freq = freqMgr.Get(order.Frequency, "Root") as FS.HISFC.Models.Order.Frequency;

            //                pha = myPha.GetItem(order.Item.ID);

            //                if (pha.DosageForm.ID == "43" || pha.DosageForm.ID == "029" || order.Item.Qty == 1)
            //                {
            //                    continue;
            //                }

            //                if (pha.MinUnit == order.DoseUnit)
            //                {
            //                    days = order.Item.Qty / order.DoseOnce / freq.Times.Length;
            //                }
            //                else
            //                {
            //                    days = order.Item.Qty * pha.BaseDose / order.DoseOnce / freq.Times.Length;
            //                }
            //            }
            //        }
            //    }


            //    itemTotCost = Math.Round(order.Item.Price / order.Item.PackQty * order.Item.Qty, 2);

            //    inner = string.Format(detail, "0", doctor.ID, "0", "1", "0", itemTotCost.ToString(), dept.Name + "|" + doctor.Name, itemTotCost.ToString(),
            //        order.Frequency.ID, order.ID, DateTime.Now.ToString("yyyy-MM-dd"), order.Item.ID, order.Item.Name, order.Item.ItemType == EnumItemType.Drug ? "1" : "0",
            //        order.Qty, order.Item.Price, emp.Level.ID, order.Item.Specs, order.DoseOnce, days, packUnit, "0", "");

            //    totCost += itemTotCost;

            //    sb.Append(inner);
            //}

            //#endregion

            //DateTime dtNow = DateTime.Now;


            //ArrayList alDiag = diagMgr.QueryCaseDiagnoseForClinic(patient.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);

            //if (alDiag == null || alDiag.Count <= 0)
            //{
            //    error = "诊断不能为空！";
            //    return -1;
            //}

            //if (isOutpatient)
            //{
            //    FS.HISFC.Models.Registration.Register register = patient as FS.HISFC.Models.Registration.Register;

            //    FS.HISFC.Models.HealthRecord.Diagnose mainDiag = null;
            //    StringBuilder sbOtherDiag = new StringBuilder();
            //    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
            //    {
            //        if (diag.Is30Disease == "1") //有效
            //        {
            //            if (diag.DiagInfo.DiagType.ID == "1") //主要诊断
            //            {
            //                mainDiag = diag;
            //            }
            //            else
            //            {
            //                sbOtherDiag.Append(diag.DiagInfo.ICD10.ID).Append("|");
            //            }
            //        }
            //    }

            //    string otherDiag = sbOtherDiag.ToString().TrimEnd('|');
            //    sbOtherDiag = null;

            //    bodyInfo = string.Format(claim, "人员类别", "参保类型", totCost.ToString(), register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd"),
            //                mainDiag.DiagInfo.ICD10.ID, mainDiag.DiagInfo.ICD10.ID, otherDiag, register.Sex.ID.ToString() == "M" ? "1" : "0",
            //                hosObj.ID, hosObj.Memo, "", mainDiag.DiagInfo.ICD10.Name, mainDiag.DiagInfo.ICD10.Name, register.PID.CardNO,
            //                register.PID.CardNO, register.Name, "0", hosObj.User02, register.ID, register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd"),
            //                "0", "0", "6", register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd"), register.Birthday.ToString("yyyy-MM-dd"),
            //                "-1", register.ID, dtNow.ToString("yyyy-MM-dd"), totCost.ToString(), "0", "0", "0", "0");

            //    body = "<BODY><YSBM>{0}</YSBM>{1}</BODY>";

            //    body = string.Format(body, doctor.ID, bodyInfo);
            //}
            //else
            //{
            //    FS.HISFC.Models.HealthRecord.Diagnose mainDiag = null;

            //    StringBuilder sbOtherDiag = new StringBuilder();
            //    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
            //    {
            //        if (diag.DiagInfo.DiagType.ID == "1") //主要诊断
            //        {
            //            mainDiag = diag;
            //        }
            //        else
            //        {
            //            sbOtherDiag.Append(diag.DiagInfo.ICD10.ID).Append("|");
            //        }
            //    }
            //    string otherDiag = sbOtherDiag.ToString().TrimEnd('|');
            //    sbOtherDiag = null;

            //    FS.HISFC.Models.RADT.PatientInfo patientInfo = patient as FS.HISFC.Models.RADT.PatientInfo;

            //    bodyInfo = string.Format(claim, "人员类别", "参保类型", totCost.ToString(), patientInfo.PVisit.InTime.ToString("yyyy-MM-dd"),
            //                mainDiag.DiagInfo.ICD10.ID, mainDiag.DiagInfo.ICD10.ID, otherDiag, patientInfo.Sex.ID.ToString() == "M" ? "1" : "0",
            //                hosObj.ID, hosObj.Memo, patientInfo.PVisit.PatientLocation.NurseCell.Name, mainDiag.DiagInfo.ICD10.Name, mainDiag.DiagInfo.ICD10.Name,
            //                patientInfo.PID.PatientNO, patientInfo.PID.PatientNO, patientInfo.Name, "0", hosObj.User02, patientInfo.ID,
            //                patientInfo.PVisit.InTime.ToString("yyyy-MM-dd"), "0", "0", "1", patientInfo.PVisit.OutTime.ToString("yyyy-MM-dd"),
            //                patientInfo.Birthday.ToString("yyyy-MM-dd"), "-1", patientInfo.ID, dtNow.ToString("yyyy-MM-dd"),
            //                totCost.ToString(), "0", "0", "0", "0");

            //    body = "<BODY><TOTALNUM>{0}</TOTALNUM><TOTALAMOUNT>{1}</TOTALAMOUNT><YSBM>{2}</YSBM>{3}</BODY>";

            //    body = string.Format(body, "", "", doctor.ID, bodyInfo);
            //}

            //string inxml = string.Format(header, hosObj.ID, hosObj.User01, strIP, strMac, patient.SSN, "", patient.IDCard, "", "", "", personMgr.GetDateTimeFromSysDateTime().ToString("yyyyMMhh/HHmmss/"), body);


            #endregion

        }

        /// <summary>
        /// 获取医嘱实时审核配置文件
        /// </summary>
        /// <returns></returns>
        private static FS.FrameWork.Models.NeuObject GetAnditSetting()
        {
            FS.FrameWork.Models.NeuObject hosObj = new FS.FrameWork.Models.NeuObject();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("./Setting/FoShanSiAudit.xml");

                hosObj.ID = doc.SelectSingleNode("SIConfig/hospitalCode").InnerText; //医院编码
                hosObj.Name = doc.SelectSingleNode("SIConfig/hospitalName").InnerText;//医院名称
                hosObj.Memo = doc.SelectSingleNode("SIConfig/hospitalLevel").InnerText;//医院等级
                hosObj.User01 = doc.SelectSingleNode("SIConfig/hospitalZone").InnerText; //医疗机构行政区划
                hosObj.User02 = doc.SelectSingleNode("SIConfig/hospitalKind").InnerText; //医疗机构行政类型
                hosObj.User03 = doc.SelectSingleNode("SIConfig/WebServies").InnerText;   //前置机webservice地址
            }
            catch (Exception ex) { }

            return hosObj;
        }
        /// <summary>
        /// 传递过去的参数或者返回来的参数
        /// </summary>
        /// <param name="p"></param>
        public static void WriteLog(string p)
        {
            //检查目录是否存在
            if (System.IO.Directory.Exists("./Plugins/SI/FoShanSiOrderAudit") == false)
            {
                System.IO.Directory.CreateDirectory("./Plugins/SI/FoShanSiOrderAudit");
            }

            //保存一周的日志
            System.IO.File.Delete("./Plugins/SI/FoShanSiOrderAudit/" + DateTime.Now.AddDays(-7).ToString("yyyyMMdd") + ".LOG");

            string name = DateTime.Now.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Plugins/SI/FoShanSiOrderAudit/" + name + ".LOG", true);
            w.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + p);
            w.Flush();
            w.Close();
        }


        /// <summary>
        /// 医嘱审核业务管理层
        /// </summary>
        public class SiOrderAuditBizlogic : FS.HISFC.BizLogic.Order.Order
        {
            /// <summary>
            /// 获取HIS与社保对照码
            /// </summary>
            /// <param name="hisCode"></param>
            /// <returns></returns>
            public Const QuerySiCompare(string hisCode)
            {
                string strSql = @"
                            SELECT t.HIS_CODE,t.HIS_NAME,t.CENTER_CODE,t.CENTER_NAME
                            FROM FIN_COM_COMPARE t
                            WHERE t.PACT_CODE = '99'
                            AND t.HIS_CODE = '{0}'
                            ";

                strSql = string.Format(strSql, hisCode);
                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }

                FS.HISFC.Models.Base.Const obj = null;
                try
                {
                    while (this.Reader.Read())
                    {
                        obj = new Const();

                        obj.ID = this.Reader[0].ToString().Trim();   //HIS编码
                        obj.Name = this.Reader[1].ToString().Trim();  //his名称
                        obj.UserCode = this.Reader[2].ToString();   //社保编码
                        obj.Memo = this.Reader[3].ToString();       //社保名称
                    }
                    return obj;
                }
                catch (Exception e)
                {
                    this.Err = e.Message;
                    this.ErrCode = "-1";
                    this.WriteErr();
                    return null;
                }
                finally
                {
                    this.Reader.Close();
                }

            }
        }


    }
}
