using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml;
using FS.HISFC.Models.Base;
using log4net;

namespace SOC.Local.HISWebService.PE
{
    /// <summary>
    /// PhysicalExaminationService 的摘要说明
    /// </summary>
    //[WebService(Namespace = "http://HIS.org/")]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class PhysicalExaminationService : System.Web.Services.WebService
    {
        private SOC.Local.HISWebService.PE.PhysicalExaminationConfirm bank = new SOC.Local.HISWebService.PE.PhysicalExaminationConfirm();
        private FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
        //{014680EC-6381-408b-98FB-A549DAA49B82}
        //private ILog log = LogManager.GetLogger();
        private ILog log = LogManager.GetLogger("");

        #region 获取体检接口病人基本资料信息
        /// <summary>
        /// 获取体检接口病人基本资料信息
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Register getPatientInfValue(string strXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);
            XmlNode objXn = doc.SelectSingleNode("/Request/PatInfo");
            FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register(); ;

            regObj.User01 = objXn.SelectSingleNode("Flag") == null ? "" : objXn.SelectSingleNode("Flag").InnerText.Trim();//记录是否新增标识,0-不改，1-新增，2-修改
            regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//正交易
            regObj.ID = objXn.SelectSingleNode("RegisterNo") == null ? "" : objXn.SelectSingleNode("RegisterNo").InnerText.Trim();//记录就诊号
            regObj.PID.CardNO = objXn.SelectSingleNode("PatientId") == null ? "" : objXn.SelectSingleNode("PatientId").InnerText.Trim();//记录卡号
            //如果卡号为空，默认新增
            if (string.IsNullOrEmpty(regObj.PID.CardNO))
            {
                regObj.User01 = "1";
            }
            if (regObj.User01 == "1")
            {
                // string strCardNo = bank._strGetSequence("select nextval for seq_pe_autocardno from dual");//"Fee.OutPatient.GetPECardNo.Select");
                string strCardNo = bank._strGetSequence("select  SEQ_FIN_REGCARDNO.nextval from dual");
                //strCardNo = "M" + strCardNo;
                strCardNo = strCardNo.PadLeft(10, '0');
                if (string.IsNullOrEmpty(strCardNo))
                {
                    return regObj = null;
                }
                else
                {
                    regObj.PID.CardNO = strCardNo;//"TJ" + strCardNo.Substring(1,strCardNo.Length-1);
                }

            }
            if (string.IsNullOrEmpty(regObj.ID))
            {
                //获取就诊号
                // string strClinic = bank._strGetSequence(" select nextval for SEQ_PE_CLINICCODE from dual");//"Fee.OutPatient.GetPEClinic.Select");
                string strClinic = bank._strGetSequence(" select  seq_fin_clinicno.nextval from dual");
                if (string.IsNullOrEmpty(strClinic))
                {
                    return regObj = null;
                }
                else
                {
                    regObj.ID = strClinic;//"TJ" + strClinic;
                }
                regObj.User02 = "1";//需要登记
            }
            regObj.DoctorInfo.Templet.RegLevel.ID = "";// doc.SelectSingleNode("/Request/CardNo").InnerText.Trim();
            regObj.DoctorInfo.Templet.RegLevel.Name = "";// doc.SelectSingleNode("/Request/CardNo").InnerText.Trim();

            regObj.DoctorInfo.Templet.Dept.ID = objXn.SelectSingleNode("Dept") == null ? "" : objXn.SelectSingleNode("Dept").InnerText.Trim();
            regObj.DoctorInfo.Templet.Dept.Name = objXn.SelectSingleNode("DeptName") == null ? "" : objXn.SelectSingleNode("DeptName").InnerText.Trim();

            regObj.DoctorInfo.Templet.Doct.ID = objXn.SelectSingleNode("Doct") == null ? "" : objXn.SelectSingleNode("Doct").InnerText.Trim();
            regObj.DoctorInfo.Templet.Doct.Name = objXn.SelectSingleNode("DoctName") == null ? "" : objXn.SelectSingleNode("DoctName").InnerText.Trim();

            regObj.Name = objXn.SelectSingleNode("PatientName") == null ? "" : objXn.SelectSingleNode("PatientName").InnerText.Trim();//患者姓名
            regObj.Sex.ID = objXn.SelectSingleNode("Sex") == null ? "" : objXn.SelectSingleNode("Sex").InnerText.Trim();//性别
            string strr = objXn.SelectSingleNode("BirthDay") == null ? "" : objXn.SelectSingleNode("BirthDay").InnerText.Trim();
            regObj.Birthday = string.IsNullOrEmpty(strr) ? System.DateTime.Now : DateTime.Parse(strr);//出生日期		
            regObj.IDCard = objXn.SelectSingleNode("IdentityCardNo") == null ? "" : objXn.SelectSingleNode("IdentityCardNo").InnerText.Trim();//身份证号	

            // FS.HISFC.Models.Registration.RegLevel level = (FS.HISFC.Models.Registration.RegLevel)this.cmbRegLevel.SelectedItem;
            regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;



            regObj.Pact.ID = objXn.SelectSingleNode("Pact") == null ? "" : objXn.SelectSingleNode("Pact").InnerText.Trim();//合同单位
            regObj.Pact.Name = objXn.SelectSingleNode("PactName") == null ? "" : objXn.SelectSingleNode("PactName").InnerText.Trim();
            if (string.IsNullOrEmpty(regObj.Pact.ID))
            {
                regObj.Pact.ID = "1";
                regObj.Pact.Name = "自费";
            }
            string strTmp1 = objXn.SelectSingleNode("PayNo") == null ? "" : objXn.SelectSingleNode("PayNo").InnerText.Trim();
            if (string.IsNullOrEmpty(strTmp1))
            {
                regObj.Pact.PayKind.ID = "01";// doc.SelectSingleNode("/CardNo").InnerText.Trim();
                regObj.Pact.PayKind.Name = "自费";// doc.SelectSingleNode("/CardNo").InnerText.Trim();
            }
            else
            {
                regObj.Pact.PayKind.Name = objXn.SelectSingleNode("PayName") == null ? "" : objXn.SelectSingleNode("PayName").InnerText.Trim();
                regObj.Pact.PayKind.ID = objXn.SelectSingleNode("PayNo") == null ? "" : objXn.SelectSingleNode("PayNo").InnerText.Trim();
            }
            regObj.SSN = string.Empty;//医疗证号

            regObj.PhoneHome = objXn.SelectSingleNode("PhoneHome") == null ? "" : objXn.SelectSingleNode("PhoneHome").InnerText.Trim();//联系电话
            regObj.AddressHome = objXn.SelectSingleNode("AddressHome") == null ? "" : objXn.SelectSingleNode("AddressHome").InnerText.Trim();//联系地址

            regObj.IsFee = false;
            regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            regObj.IsSee = false;
            regObj.InputOper.ID = objXn.SelectSingleNode("Operator") == null ? "" : objXn.SelectSingleNode("Operator").InnerText.Trim();
            regObj.InputOper.OperTime = System.DateTime.Now;
            //add by niuxinyuan
            regObj.DoctorInfo.SeeDate = System.DateTime.Now;//doc.SelectSingleNode("/CardNo").InnerText.Trim(); ;
            // regObj.DoctorInfo.Templet.Noon.Name = this.QeryNoonName(this.regObj.DoctorInfo.Templet.Noon.ID);
            regObj.CancelOper.ID = "";
            regObj.CancelOper.OperTime = DateTime.MinValue;
            return regObj;
        }
        #endregion

        #region 获取体检接口收费项目信息
        /// <summary>
        /// 获取体检接口收费项目信息
        /// </summary>
        /// <returns></returns>
        private ArrayList getItemListInfValue(string strXml, ref FS.HISFC.Models.Registration.Register Patient)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alFeeItem = new ArrayList();//保存费用
            FS.HISFC.Models.Order.OutPatient.Order order;
            // 非药品组合项目业务
            FS.HISFC.BizProcess.Integrate.Fee undrugztManager = new FS.HISFC.BizProcess.Integrate.Fee();
            // 费用业务层
            FS.HISFC.BizProcess.Integrate.Fee feeManagement = new FS.HISFC.BizProcess.Integrate.Fee();
            if (Patient == null)
            {
                Patient = new FS.HISFC.Models.Registration.Register();
            }
            string reciptNo = "";//处方号
            string strID = "";
            #region 添加判断如果有医嘱流水号，则不需要重新获取，否则获取新的流水号
            //2008-1-23

            XmlNodeList objXNL = doc.SelectNodes("/Request/Item");
            if (objXNL == null || objXNL.Count == 0)
            {
                return alFeeItem;
            }
            XmlNode objXn;
            for (int intI = 0; intI < objXNL.Count; intI++)
            {
                objXn = objXNL[intI];
                order = new FS.HISFC.Models.Order.OutPatient.Order();
                if (order.ID == "")
                {

                    strID = bank._strGetSequence("select seq_met_order_id.nextval from dual");//Management.Order.GetNewOrderID");
                    if (strID == "")
                    {
                        /*2008-1-23*/
                        return alFeeItem;
                    }
                    order.ID = strID;    //赋值医嘱流水号
                }

            #endregion
                //order.IsNewOrder = true;
                order.ReciptNO = reciptNo;
                order.SequenceNO = 0;
                order.ReciptSequence = "";

                #region 赋值
                order.Item.ItemType = EnumItemType.UnDrug;


                order.ApplyNo = "";//feeitemlist.ApplyNo = order.ApplyNo;

                order.Patient.ID = objXn.SelectSingleNode("RegisterNo") == null ? "" : objXn.SelectSingleNode("RegisterNo").InnerText.Trim();// feeitemlist.Patient.ID = order.Patient.ID;//门诊流水号
                order.Patient.PID.CardNO = objXn.SelectSingleNode("PatientId") == null ? "" : objXn.SelectSingleNode("PatientId").InnerText.Trim(); // feeitemlist.Patient.PID.CardNO = order.Patient.PID.CardNO;//门诊卡号 
                order.ID = ""; // feeitemlist.Order.ID = order.ID;//医嘱流水号
                Patient.PID.CardNO = order.Patient.PID.CardNO;
                Patient.ID = order.Patient.ID;
                order.CheckPartRecord = "";
                order.Combo.ID = "";
                order.User01 = "";

                string itemCode = objXn.SelectSingleNode("ItemCode") == null ? "" : objXn.SelectSingleNode("ItemCode").InnerText.Trim();// feeitemlist.Item.ID = order.Item.ID;
                order.ExeDept.ID = objXn.SelectSingleNode("ExcDept") == null ? "" : objXn.SelectSingleNode("ExcDept").InnerText.Trim();
                string strMinFee = string.Empty;
                string strSysCode = string.Empty;
                bool isPharmacy = false;
                int intII = bank._QueryItemFeeIDInf(itemCode, ref strMinFee, ref strSysCode, ref isPharmacy);

                if (isPharmacy)
                {
                    order.Item = new FS.HISFC.Models.Pharmacy.Item();
                    //order.ExeDept.ID = "90P1";//暂时写死吧
                }
                order.Qty = objXn.SelectSingleNode("Qty") == null ? 0 : int.Parse(objXn.SelectSingleNode("Qty").InnerText.Trim());// feeitemlist.Item.Qty = order.Qty;
                order.Item.ItemType = EnumItemType.UnDrug;
                order.Item.MinFee.ID = strMinFee;// feeitemlist.Item.MinFee = order.Item.MinFee;//最小费用
                order.Item.SysClass.ID = strSysCode;



                FS.HISFC.Models.Base.Department dept = deptMgr.GetDeptmentById(order.ExeDept.ID);
                if (dept != null)
                {
                    order.ExeDept.Name = dept.Name;
                }
                if (order.Item.PackQty <= 0)
                {
                    order.Item.PackQty = 1;
                }
                order.FT.OwnCost = order.Qty * order.Item.Price;

                if (order.HerbalQty == 0)
                {
                    order.HerbalQty = 1;
                }

                order.HerbalQty = 0;//草药付数
                order.ReciptDept.ID = objXn.SelectSingleNode("Dept") == null ? "" : objXn.SelectSingleNode("Dept").InnerText.Trim(); // feeitemlist.RecipeOper.Dept = order.ReciptDept;//开方科室信息
                order.ReciptDoctor.ID = objXn.SelectSingleNode("Doct") == null ? "" : objXn.SelectSingleNode("Doct").InnerText.Trim();// feeitemlist.RecipeOper.ID = order.ReciptDoctor.ID;
                order.DoseUnit = "";//用量单位
                order.Frequency.ID = "";//频次信息

                order.Combo.IsMainDrug = false; ;//是否主药
                order.Item.ID = objXn.SelectSingleNode("ItemCode") == null ? "" : objXn.SelectSingleNode("ItemCode").InnerText.Trim();// feeitemlist.Item.ID = order.Item.ID;
                order.Item.Name = objXn.SelectSingleNode("ItemName") == null ? "" : objXn.SelectSingleNode("ItemName").InnerText.Trim();// feeitemlist.Item.Name = order.Item.Name;
                order.IsEmergency = false;// feeitemlist.IsUrgent = order.IsEmergency;//是否加急
                order.Sample.ID = "";// feeitemlist.Order.Sample = order.Sample;//样本信息
                order.Memo = "";// feeitemlist.Memo = order.Memo;//备注


                order.Item.Price = objXn.SelectSingleNode("UnitPrice") == null ? 0 : decimal.Parse(objXn.SelectSingleNode("UnitPrice").InnerText.Trim());// feeitemlist.Item.Price = order.Item.Price;//价格


                order.Item.PriceUnit = objXn.SelectSingleNode("Unit") == null ? "" : objXn.SelectSingleNode("Unit").InnerText.Trim();// feeitemlist.Item.PriceUnit = order.Item.PriceUnit;//价格单位
                order.FT.TotCost = objXn.SelectSingleNode("UnitTotal") == null ? 0 : decimal.Parse(objXn.SelectSingleNode("UnitTotal").InnerText.Trim());//新加赋值 xiaohf
                order.FT.TotCost = order.FT.TotCost;
                order.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
                order.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
                order.RegTime = objXn.SelectSingleNode("RegisterDate") == null ? DateTime.MinValue : DateTime.Parse(objXn.SelectSingleNode("RegisterDate").InnerText);
                Patient.DoctorInfo.SeeDate = order.RegTime;
                Patient.DoctorInfo.Templet.Dept.ID = order.ReciptDept.ID;
                Patient.User01 = objXn.SelectSingleNode("Operator") == null ? "tjoper" : objXn.SelectSingleNode("Operator").InnerText.Trim();
                order.ReciptSequence = "";// feeitemlist.RecipeSequence = order.ReciptSequence;//收费序列
                order.ReciptNO = "";// feeitemlist.RecipeNO = order.ReciptNO;//处方号
                order.SequenceNO = 0;// feeitemlist.SequenceNO = order.SequenceNO;//处方流水号
                #endregion

                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitemo = this.ChangeToFeeItemList(order);

                if (feeitemo == null)
                {
                    /*2008-1-23*/
                    //order.IsNewOrder = true;
                    return null;
                }
                ///fuhe项目先获取处方号
                ///2008-10-21
                ArrayList temptemp = new ArrayList();
                string errText = string.Empty;
                if (feeitemo.Item.MinFee.ID == "fh")
                {
                    temptemp.Add(feeitemo);

                    bool rtnValue = false;//feeManagement.SetRecipeNOOutpatient(temptemp, ref errText);
                    if (!rtnValue)
                    {
                        errText = "获取组套处方号出错！" + feeManagement.Err;
                        return null;
                    }
                    feeitemo = (temptemp[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).Clone();
                }

                alFeeItem.Add(feeitemo);
            }
            return alFeeItem;
        }
        #endregion

        #region 插入登记信息
        /// <summary>
        /// 插入登记信息，如果新增，则插入基本信息
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "新增体检人员登记资料")]
        public string _AddRegisterInf(string strXml)
        {
            log.Debug(string.Format("方法名：{0} \r\n 传入参数：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, strXml));
            string head = "<Response><ResultCode>";
            string res = "";
            if (string.IsNullOrEmpty(strXml))
            {
                head += "-1</ResultCode><ErrorMsg>" + "XML为空" + "</ErrorMsg><PatientId></PatientId><RegisterNo></RegisterNo>";
                Session.Abandon();
                string result = head + "</Response>";
                log.Debug(string.Format("方法名：{0} \r\n 传出参数：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, result));
                return result;
            }
            FS.HISFC.Models.Registration.Register regObj;
            try
            {
                regObj = this.getPatientInfValue(strXml);
                if (regObj == null)
                {
                    head += "-1</ResultCode><ErrorMsg>" + "XML无法解析" + "</ErrorMsg><PatientId></PatientId><RegisterNo></RegisterNo>";
                    Session.Abandon();
                    string result = head + "</Response>";
                    log.Debug(string.Format("方法名：{0} \r\n 传出参数：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, result));
                    return result;
                }
            }
            catch(Exception ex)
            {
                head += "-1</ResultCode><ErrorMsg>" + "XML无法解析" + "</ErrorMsg><PatientId></PatientId><RegisterNo></RegisterNo>";
                Session.Abandon();
                string result =  head + "</Response>";
                log.Debug(string.Format("方法名：{0} \r\n 传出参数：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, result));
                log.Error(string.Format("方法名：{0} \r\n 抛出异常：", System.Reflection.MethodBase.GetCurrentMethod().Name),ex);
                return result;
            }
            try
            {
                res = bank._intAddRegisterInf(regObj);//成功则返回1，失败-1
                if (res.Trim() == "1")
                {
                    head += "1</ResultCode><ErrorMsg></ErrorMsg><PatientId>" + regObj.PID.CardNO + "</PatientId><RegisterNo>" + regObj.ID + "</RegisterNo>";
                }
                else
                {
                    head += "-1</ResultCode><ErrorMsg>" + bank.ErrMsg + "</ErrorMsg><PatientId></PatientId><RegisterNo></RegisterNo>";
                }
            }
            catch(Exception ex)
            {
                head += "-1</ResultCode><ErrorMsg>" + bank.ErrMsg + "</ErrorMsg><PatientId></PatientId><RegisterNo></RegisterNo>";
                log.Error(string.Format("方法名：{0} \r\n 抛出异常：", System.Reflection.MethodBase.GetCurrentMethod().Name), ex);
            }
            Session.Abandon();
            string returnResult = head + "</Response>";
            log.Debug(string.Format("方法名：{0} \r\n 传出参数：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, returnResult));
            return returnResult;
        }

        /// <summary>
        /// 获取新增体检人员登记资料XML
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "获取新增体检人员登记资料XML")]
        public string _GetAddRegisterInfXml()
        {
            return "<Request><PatInfo><Dept>1110</Dept><DeptName>体检科</DeptName><Doct>000018</Doct><DoctName>黄福章</DoctName><Operator>009999</Operator><OperatorName>系统员</OperatorName><PatientId></PatientId><RegisterNo></RegisterNo><PatientName>体检测试</PatientName><Sex>M</Sex><BirthDay>2000-01-01 00:00:00</BirthDay><IdentityCardNo>430512200001011111</IdentityCardNo><PayNo>01</PayNo><PayName>现金</PayName><PhoneHome>12345677</PhoneHome><AddressHome>中山六院</AddressHome><Flag>1</Flag><RegFlag>1</RegFlag><Pact>1</Pact><PactName>自费</PactName></PatInfo></Request>";
        }
        #endregion

        #region 插入收费信息
        /// <summary>
        /// 插入收费信息，修改的话是全部删除全部未收费的项目，然后插入新的项目。
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "新增体检人员收费项目信息")]
        public string _AddItemListInf(string strXml)
        {
            log.Debug(string.Format("方法名：{0} \r\n 传入参数：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, strXml));
            string head = "<Response><ResultCode>";
            DateTime now = System.DateTime.Now;
            if (string.IsNullOrEmpty(strXml))
            {
                head += "-1</ResultCode><ErrorMsg>" + "XML为空" + "</ErrorMsg>";
                Session.Abandon();
                string result =head + "</Response>";
                log.Debug(string.Format("方法名：{0} \r\n 传出参数：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, result));
                return result;
            }
            ArrayList temptemp = new ArrayList();
            FS.HISFC.Models.Registration.Register Patient = new FS.HISFC.Models.Registration.Register();
            string errText = string.Empty;
            try
            {
                temptemp = this.getItemListInfValue(strXml, ref Patient);
                if (temptemp == null || temptemp.Count == 0)
                {
                    head += "-1</ResultCode><ErrorMsg>" + "XML无法解析" + "</ErrorMsg>";
                    Session.Abandon();
                    string result = head + "</Response>";
                    log.Debug(string.Format("方法名：{0} \r\n 传出参数：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, result));
                    return result;
                }
                //if()
                //{

                //}
            }
            catch(Exception ex)
            {
                head += "-1</ResultCode><ErrorMsg>" + "XML无法解析" + "</ErrorMsg>";
                Session.Abandon();
                string result =  head + "</Response>";
                log.Debug(string.Format("方法名：{0} \r\n 传出参数：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, result));
                log.Error(string.Format("方法名：{0} \r\n 抛出异常：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex));
                return result;
            }
            try
            {
                bool bln = bank.SetChargeInfo(Patient, temptemp, now, ref errText);//成功则返回1，失败-1
                if (bln)
                {
                    head += "1</ResultCode><ErrorMsg></ErrorMsg>";
                }
                else
                {
                    head += "-1</ResultCode><ErrorMsg>" + errText + "</ErrorMsg>";
                }
            }
            catch(Exception ex)
            {
                head += "-1</ResultCode><ErrorMsg>" + errText + "</ErrorMsg>";
                log.Error(string.Format("方法名：{0} \r\n 抛出异常：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, ex));
            }
            Session.Abandon();
            string logResult = head + "</Response>";
            log.Debug(string.Format("方法名：{0} \r\n 传出参数：{1}", System.Reflection.MethodBase.GetCurrentMethod().Name, logResult));
            return logResult;
        }

        /// <summary>
        /// 获取新增项目信息XML
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "获取新增体检人员收费项目信息XML")]
        public string _GetAddItemListInfXml()
        {
            return "<Request><Item><Dept>1110</Dept><Doct>000018</Doct><Operator>009999</Operator><PatientId></PatientId><RegisterNo></RegisterNo><RegisterDate>2013-12-19 00:00:00</RegisterDate><ItemCode>F00000073119</ItemCode><ItemName>腹部(肝胆胰脾)彩色多普勒超声常规检查</ItemName><Price>120</Price><UnitPrice>120</UnitPrice><Unit>次</Unit><Qty>1</Qty><UnitTotal>1</UnitTotal><PayRate>1</PayRate><ExcDept>4001</ExcDept><ExcDeptName>B超室</ExcDeptName></Item></Request>";
        }
        #endregion

        #region 删除未收费信息
        /// <summary>
        /// 删除未收费信息。
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "删除体检人全部未收费信息")]
        public string _DeleteItemListInf(string strXml)
        {
            string head = "<Response><ResultCode>";
            DateTime now = System.DateTime.Now;
            if (string.IsNullOrEmpty(strXml))
            {
                head += "-1</ResultCode><ErrorMsg>" + "XML为空" + "</ErrorMsg>";
                Session.Abandon();
                return head + "</Response>";
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);
            string PatientID = doc.SelectSingleNode("/Request/PatientId").InnerText.Trim();
            string RegisterNo = doc.SelectSingleNode("/Request/RegisterNo").InnerText.Trim();
            if (string.IsNullOrEmpty(RegisterNo))
            {
                head += "-1</ResultCode><ErrorMsg>" + "门诊流水号为空" + "</ErrorMsg>";
                Session.Abandon();
                return head + "</Response>";
            }
            string errText = string.Empty;
            try
            {
                int intI = bank._DeleteUnChargeInfo(PatientID, RegisterNo, ref errText);
                head += intI.ToString() + "</ResultCode><ErrorMsg>" + errText + "</ErrorMsg>";
            }
            catch
            {
                head += "-1</ResultCode><ErrorMsg>" + errText + "</ErrorMsg>";
                Session.Abandon();
                return head + "</Response>";
            }

            Session.Abandon();
            return head + "</Response>";
        }

        /// <summary>
        /// 获取删除未收费信息XML
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "获取删除体检人全部未收费信息XML")]
        public string _GetDeleteItemListInfXml()
        {
            return "<Request><PatientId></PatientId><RegisterNo></RegisterNo></Request>";
        }
        #endregion

        #region 查询全部的有效的科室编码、名称信息
        /// <summary>
        /// 查询全部的有效的科室编码、名称信息。
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "查询全部的有效的科室编码、名称信息")]
        public string _QueryDeptInf()
        {
            string resErrXml = @"<Response><Item><DeptID></DeptID><DeptName></DeptName></Item></Response>";
            string resXml = @"<Response>";

            DataSet ds = new DataSet();
            bank._QueryDeptInf(ref ds);
            if (ds == null || ds.Tables.Count == 0)
            {
                return resErrXml;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tmpXml = @"<Item><DeptID></DeptID><DeptName></DeptName></Item>";
                XmlDocument xt = new XmlDocument();
                xt.LoadXml(tmpXml);
                xt.SelectSingleNode("/Item/DeptID").InnerText = dr["DEPT_CODE"].ToString();
                xt.SelectSingleNode("/Item/DeptName").InnerText = dr["DEPT_NAME"].ToString();

                resXml += xt.InnerXml.ToString();
            }

            string res = resXml + "</Response>";
            Session.Abandon();
            return res;
        }
        #endregion

        #region 用于查询全部的有效的操作人员编码、姓名，性别
        /// <summary>
        /// 用于查询全部的有效的操作人员编码、姓名，性别信息。
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "查询全部的有效的操作人员编码、姓名，性别信息")]
        public string _QueryEmpInf()
        {
            string resErrXml = @"<Response><Item><EmpID></EmpID><EmpName></EmpName><EmpSex></EmpSex></Item></Response>";
            string resXml = @"<Response>";

            DataSet ds = new DataSet();
            bank._QueryEmpInf(ref ds);
            if (ds == null || ds.Tables.Count == 0)
            {
                return resErrXml;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tmpXml = @"<Item><EmpID></EmpID><EmpName></EmpName><EmpSex></EmpSex></Item>";
                XmlDocument xt = new XmlDocument();
                xt.LoadXml(tmpXml);
                xt.SelectSingleNode("/Item/EmpID").InnerText = dr["EMPL_CODE"].ToString();
                xt.SelectSingleNode("/Item/EmpName").InnerText = dr["EMPL_NAME"].ToString();
                xt.SelectSingleNode("/Item/EmpSex").InnerText = dr["SEX_CODE"].ToString();
                resXml += xt.InnerXml.ToString();
            }

            string res = resXml + "</Response>";
            Session.Abandon();
            return res;
        }
        #endregion

        #region 用于查询全部的收费组套项目ID、名称、价格
        /// <summary>
        /// 用于查询全部的收费组套项目ID、名称、价格信息。
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "用于查询全部的收费组套项目ID、名称、价格")]
        public string _QueryItemInf()
        {
            string resErrXml = @"<Response><Item><ItemID></ItemID><ItemName></ItemName><ItemUnit></ItemUnit><ItemPrice></ItemPrice ><ItemPrice2></ItemPrice2><ItemQty></ItemQty ></Item></Response>";
            string resXml = @"<Response>";

            DataSet ds = new DataSet();
            bank._QueryItemInf(ref ds);
            if (ds == null || ds.Tables.Count == 0)
            {
                return resErrXml;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tmpXml = @"<Item><ItemID></ItemID><ItemName></ItemName><ItemUnit></ItemUnit><ItemPrice></ItemPrice ><ItemPrice2></ItemPrice2><ItemQty></ItemQty ></Item>";
                XmlDocument xt = new XmlDocument();
                xt.LoadXml(tmpXml);
                xt.SelectSingleNode("/Item/ItemID").InnerText = dr["item_code"].ToString();
                xt.SelectSingleNode("/Item/ItemName").InnerText = dr["item_name"].ToString();
                xt.SelectSingleNode("/Item/ItemUnit").InnerText = dr["stock_unit"].ToString();
                xt.SelectSingleNode("/Item/ItemPrice").InnerText = dr["unit_price"].ToString();
                xt.SelectSingleNode("/Item/ItemPrice2").InnerText = dr["unit_price2"].ToString();
                xt.SelectSingleNode("/Item/ItemQty").InnerText = dr["pack_qty"].ToString();
                resXml += xt.InnerXml.ToString();
            }

            string res = resXml + "</Response>";
            Session.Abandon();
            return res;
        }
        #endregion

        #region 将医嘱实体转成费用实体
        /// <summary>
        /// 将医嘱实体转成费用实体
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Outpatient.FeeItemList ChangeToFeeItemList(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            try
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitemlist = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

                if (order.Item.ItemType==EnumItemType.Drug)
                {
                    feeitemlist.Item = itemMgr.GetItem(order.Item.ID);

                }
                else
                {
                    feeitemlist.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                }

                //feeitemlist.ApplyNo = order.ApplyNo;
                feeitemlist.Item.Qty = order.Qty;
                feeitemlist.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                feeitemlist.Patient.ID = order.Patient.ID;//门诊流水号
                feeitemlist.Patient.PID.CardNO = order.Patient.PID.CardNO;//门诊卡号 
                feeitemlist.Order.ID = order.ID;//医嘱流水号

                feeitemlist.ChargeOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                feeitemlist.Order.CheckPartRecord = order.CheckPartRecord;//检体 
                feeitemlist.Order.Combo.ID = order.Combo.ID;//组合号
                if (order.Unit == "[复合项]")
                {
                    feeitemlist.IsGroup = true;
                    feeitemlist.UndrugComb.ID = order.User01;
                    feeitemlist.UndrugComb.Name = order.User02;
                }

                //if (order.Item.IsPharmacy && !((FS.HISFC.Models.Pharmacy.Item)order.Item).IsSubtbl)
                //{
                //    feeitemlist.ExecOper.Dept.ID = order.StockDept.Clone().ID;//传扣库科室
                //    feeitemlist.ExecOper.Dept.Name = order.StockDept.Clone().Name;
                //}
                //else
                //{
                feeitemlist.ExecOper.Dept.ID = order.ExeDept.Clone().ID;
                feeitemlist.ExecOper.Dept.Name = order.ExeDept.Clone().Name;
                //}
                feeitemlist.InjectCount = order.InjectCount;//院内次数

                if (order.Item.PackQty <= 0)
                {
                    order.Item.PackQty = 1;
                }
                //自批价项目
                ////if (order.Item.Price == 0)
                ////{
                ////    order.Item.Price = order.Item.Price;
                ////}
                //by zuowy 根据收费是否是最小单位 确定收费 改时慎重
                if (order.Item.ItemType==EnumItemType.Drug)
                {
                    if (order.NurseStation.User03 == "")//user03为空,说明不知道开立的什么单位 默认为最小单位
                    {
                        order.NurseStation.User03 = "1";//默认
                    }
                    if (order.NurseStation.User03 != "1")//开立最小单位 !=((FS.HISFC.Object.Pharmacy.Item)order.Item).MinUnit)
                    {
                        //feeitemlist.Item.Qty = order.Item.PackQty * order.Qty;
                        //order.FT.OwnCost = order.Qty * order.Item.Price;

                        //order.Item.PriceUnit = order.Unit;
                        //feeitemlist.FeePack = "1";//开立单位 1:包装单位 0:最小单位
                    }
                    else
                    {
                        if (order.Item.PackQty == 0)
                        {
                            order.Item.PackQty = 1;
                        }
                        order.FT.OwnCost = order.Qty * order.Item.Price / order.Item.PackQty;

                        //order.Item.PriceUnit = order.Unit;
                        feeitemlist.FeePack = "0";//开立单位 1:包装单位 0:最小单位
                    }
                }
                else
                {
                    order.FT.OwnCost = order.Qty * order.Item.Price;
                    feeitemlist.FeePack = "1";
                }

                if (order.HerbalQty == 0)
                {
                    order.HerbalQty = 1;
                }

                feeitemlist.Days = order.HerbalQty;//草药付数
                feeitemlist.RecipeOper.Dept = order.ReciptDept;//开方科室信息
                feeitemlist.RecipeOper.Name = order.ReciptDoctor.Name;//开方医生信息
                feeitemlist.RecipeOper.ID = order.ReciptDoctor.ID;
                //feeitemlist.Order.DoseUnit = order.DoseUnit;//用量单位
                if (order.Item.ItemType==EnumItemType.Drug)
                {
                    //if (((FS.HISFC.Models.Pharmacy.Item)order.Item).SysClass.ID.ToString() == "PCC")
                    //{
                    //    if (order.HerbalQty == 0)
                    //    {
                    //        order.HerbalQty = 1;
                    //    }

                    //    feeitemlist.Order.DoseOnce = order.DoseOnce;

                    //}
                    //else
                    //{
                    //    feeitemlist.Order.DoseOnce = order.DoseOnce;//每次用量
                    //}

                    feeitemlist.Order.DoseOnce = 1m;
                    feeitemlist.Order.DoseUnit = ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).PackUnit;

                }
                feeitemlist.Item.PriceUnit = order.Item.PriceUnit;
                feeitemlist.Order.Item.PriceUnit = order.Item.PriceUnit;


                feeitemlist.Order.Combo.IsMainDrug = order.Combo.IsMainDrug;//是否主药
                feeitemlist.Item.ID = order.Item.ID;
                feeitemlist.Item.Name = order.Item.Name;
                if (order.Item.ItemType==EnumItemType.Drug)//是否药品
                {
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).BaseDose = ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose;//基本计量
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).Quality = ((FS.HISFC.Models.Pharmacy.Item)order.Item).Quality;//药品性质
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).DosageForm = ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm;//剂型

                    feeitemlist.Order.Usage.ID = "1002";
                    feeitemlist.Order.Frequency.ID = "QD";
                    feeitemlist.IsConfirmed = false;//是否终端确认
                    feeitemlist.Item.PackQty = order.Item.PackQty;//包装数量
                }
                else
                {
                    if (order.ReTidyInfo != "SUBTBL")
                    {
                        feeitemlist.IsConfirmed = false;
                        feeitemlist.Item.PackQty = 1;//包装数量
                    }
                    else//附材中的复合项目
                    {
                        feeitemlist.IsConfirmed = false;//FS.neuFC.Function.NConvert.ToBoolean(order.Mark2);
                        feeitemlist.Item.PackQty = 1;
                    }
                }

                feeitemlist.Order.Item.ItemType = order.Item.ItemType;//是否药品

                feeitemlist.IsUrgent = order.IsEmergency;//是否加急
                feeitemlist.Order.Sample = order.Sample;//样本信息
                feeitemlist.Memo = order.Memo;//备注

                //[2007/12/20]组套项目最小费用固定
                //if (!order.Item.IsPharmacy)
                //{
                //    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).UnitFlag == "1")//组套
                //    {
                //        feeitemlist.Item.MinFee.ID = "fh";
                //    }
                //    else//明细
                //    {
                //        feeitemlist.Item.MinFee = order.Item.MinFee;//最小费用
                //    }
                //}
                //else
                //{
                feeitemlist.Item.MinFee = order.Item.MinFee;//最小费用
                //}
                //feeitemlist.Item.MinFee = order.Item.MinFee;//最小费用
                //原来只是这行代码[2007/12/20]END

                feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//划价状态
                feeitemlist.Item.Price = order.Item.Price;//价格


                feeitemlist.Item.PriceUnit = order.Item.PriceUnit;//价格单位
                if (order.Item.SysClass.ID.ToString() == "PCC" && order.HerbalQty > 0)
                {

                }
                order.FT.TotCost = order.FT.TotCost;
                order.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
                order.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
                feeitemlist.FT = Round(order, 2);//取两位				
                ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.SeeDate = order.RegTime;//登记日期
                ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.Templet.Dept = order.ReciptDept;//登记科室
                feeitemlist.Item.SysClass = order.Item.SysClass;//系统类别
                feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//交易类型
                //feeitemlist.Order.Usage = order.Usage;//用法
                feeitemlist.RecipeSequence = order.ReciptSequence;//收费序列
                feeitemlist.RecipeNO = order.ReciptNO;//处方号
                feeitemlist.SequenceNO = order.SequenceNO;//处方流水号
                feeitemlist.FTSource = "3";//来自医嘱
                if (order.IsSubtbl)
                {
                    feeitemlist.Item.IsMaterial = true;//是附材
                }
                return feeitemlist;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 为费用取整
        /// </summary>
        /// <param name="order"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static FS.HISFC.Models.Base.FT Round(FS.HISFC.Models.Order.OutPatient.Order order, int i)
        {
            FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();
            //为NULL返回新实体
            if (order == null || order.FT == null)
            {
                return ft;
            }

            ft.AdjustOvertopCost = FS.FrameWork.Public.String.FormatNumber(order.FT.AdjustOvertopCost, i);
            ft.AirLimitCost = FS.FrameWork.Public.String.FormatNumber(order.FT.AirLimitCost, i);
            ft.BalancedCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BalancedCost, i);
            ft.BalancedPrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BalancedPrepayCost, i);
            ft.BedLimitCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BedLimitCost, i);
            ft.BedOverDeal = order.FT.BedOverDeal;
            ft.BloodLateFeeCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BloodLateFeeCost, i);
            ft.BoardCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BoardCost, i);
            ft.BoardPrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BoardPrepayCost, i);
            ft.DrugFeeTotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DrugFeeTotCost, i);
            ft.TransferPrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TransferPrepayCost, i);
            ft.TransferTotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TransferTotCost, i);
            ft.DayLimitCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DayLimitCost, i);
            ft.DerateCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DerateCost, i);
            ft.FixFeeInterval = order.FT.FixFeeInterval;
            ft.ID = order.FT.ID;
            ft.LeftCost = FS.FrameWork.Public.String.FormatNumber(order.FT.LeftCost, i);
            ft.OvertopCost = FS.FrameWork.Public.String.FormatNumber(order.FT.OvertopCost, i);
            ft.DayLimitTotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DayLimitTotCost, i);
            ft.Memo = order.FT.Memo;
            ft.Name = order.FT.Name;
            ft.OwnCost = FS.FrameWork.Public.String.FormatNumber(order.FT.OwnCost, i);
            ft.FTRate.OwnRate = order.FT.FTRate.OwnRate;
            ft.PayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.PayCost, i);
            ft.FTRate.PayRate = order.FT.FTRate.PayRate;
            ft.PreFixFeeDateTime = order.FT.PreFixFeeDateTime;
            ft.PrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.PrepayCost, i);
            ft.PubCost = FS.FrameWork.Public.String.FormatNumber(order.FT.PubCost, i);
            ft.RebateCost = FS.FrameWork.Public.String.FormatNumber(order.FT.RebateCost, i);
            ft.ReturnCost = FS.FrameWork.Public.String.FormatNumber(order.FT.ReturnCost, i);
            ft.SupplyCost = FS.FrameWork.Public.String.FormatNumber(order.FT.SupplyCost, i);
            ft.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, i);

            ft.User01 = order.FT.User01;
            ft.User02 = order.FT.User02;
            ft.User03 = order.FT.User03;
            return ft;
        }

        #endregion
    }
}
