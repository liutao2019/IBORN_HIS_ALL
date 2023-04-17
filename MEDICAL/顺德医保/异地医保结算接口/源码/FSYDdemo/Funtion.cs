using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace FoShanYDSI
{
    class Funtion
    {

        public FoShanYDSI.FoShanYDSIDatabase SIMgr = new FoShanYDSIDatabase();

        /// <summary>
        /// 配置文件
        /// </summary>
        private static string profileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\Plugins\SI\FoShanYDSIConfig.xml";

        /// <summary>
        /// Web服务
        /// </summary>
        private static string webServiceAddress = string.Empty;

        private FS.HISFC.BizLogic.Fee.Account ucMgr = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 
        /// </summary>
        public static string WebServiceAddress
        {
            get
            {
                if (string.IsNullOrEmpty(webServiceAddress))
                {
                    GetSetting();
                }
                return webServiceAddress;
            }
        }

        /// <summary>
        /// 日志记录实体
        /// </summary>
        Log lg = new Log();

        /// <summary>
        /// 读取社保配置文件
        /// </summary>
        private static void GetSetting()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(profileName);

            XmlNode node = doc.SelectSingleNode(@"/SIConfig/webServiceAddress");
            webServiceAddress = node.InnerText.Trim();
        }


        /// <summary>
        /// 医保的本地HIS项目编码与医保中的项目编码对照(合同单位为14)//合同单位需按实际修改
        /// </summary>
        Hashtable hsCompareItems = new Hashtable();

        FoShanYDSI.WebReference.Service1 myWebService = new FoShanYDSI.WebReference.Service1();

        /// <summary>
        /// webSevice调用
        /// </summary>
        /// <param name="TransNo">交易类别代码</param>
        /// <param name="Inxml">交易输入xml</param>
        /// <param name="Outxml">交易输出xml</param>
        /// <param name="appCode">错误代码</param>
        /// <param name="ErrorMsg">错误信息</param>
        public void Provider(FS.HISFC.Models.RADT.PatientInfo patient,string TransNo, string Inxml, ref string Outxml, ref string appCode, ref  string ErrorMsg)
        {
            #region old
            //lg.path = @".\Plugins\SI\FoShanYDLog\";

            //lg.WriteInPut(TransNo, "  ", " ", Inxml, "  ", " ");
            //Outxml = myWebService.CallService(TransNo, Inxml);

            //lg.WriteOutPut(Outxml);
            
            //XmlDocument doc = new XmlDocument();

            //doc.LoadXml(Outxml);
            //appCode = doc.SelectSingleNode("result/errorcode").InnerText.Trim();

            //if (FS.FrameWork.Function.NConvert.ToInt32(appCode) < 0) //失败
            //{
            //    ErrorMsg = doc.SelectSingleNode("result/errormsg").InnerText.Trim();
            //}
            #endregion 

            #region New
            //入参
            lg.path = @".\Plugins\SI\FoShanYDLog\";

            lg.WriteInPut(TransNo, "  ", " ", Inxml, "  ", " ");

            string[] args = { TransNo, Inxml };
            Object obj = FoShanYDSI.Business.Common.WebServiceHelper.InvokeWebService(WebServiceAddress, "CallService", args, ref ErrorMsg);

            if (obj == null)
            {
                MessageBox.Show("返回结果为空！");
                appCode = "-1";
                return ;
            }

            Outxml = obj.ToString();
            // {855F4C4E-041E-467d-8757-20B1EF9F7406}
            if (TransNo == "28" || TransNo == "0301" || TransNo == "0707")//费用明细不插入记录
            {
            }
            else 
            {
                if (patient != null)
                {
                    this.InsertFoShanYDLOG(patient.ID, TransNo, patient.User01, Inxml, Outxml);
                }
            }

            //出参
            lg.WriteOutPut(Outxml);

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(Outxml);
            appCode = doc.SelectSingleNode("result/errorcode").InnerText.Trim();

            if (FS.FrameWork.Function.NConvert.ToInt32(appCode) < 0) //失败
            {
                ErrorMsg = doc.SelectSingleNode("result/errormsg").InnerText.Trim();
            }
            else
            {
                appCode = "1";
            }
            #endregion 
        }
        // {855F4C4E-041E-467d-8757-20B1EF9F7406}
        public int InsertFoShanYDLOG(string inpatientNo,string transNo,string remark,string inXML,string outXML)
        {
            string sql = @"insert into COM_FoShanYD_LOG
                                  (SEQ,
                                   INPATIENT_NO,
                                   TYPE,
                                   TRANSNO,
                                   REMARK,
                                   INXML,
                                   OUTXML,
                                   OPER_CODE,
                                   OPER_DATE)
                                values
                                  (SEQ_COM_FoShanYD_LOG.Nextval,
                                   '{0}',
                                   '{1}',
                                   '{2}',
                                   '{3}',
                                   '{4}',
                                   '{5}',
                                   '{6}',
                                   sysdate )
                                ";
            string operCode = FS.FrameWork.Management.Connection.Operator.ID;
            if (inXML.Length > 3890)
            {
                inXML = inXML.Substring(0, 3890);
            }
            if (outXML.Length > 3890)
            {
                outXML = outXML.Substring(0, 3890);
            }
            sql = string.Format(sql, inpatientNo, "1", transNo, remark, inXML, outXML, operCode);
            return this.ucMgr.ExecNoQuery(sql);
        }
        /// <summary>
        /// 更新出院登记流水号
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="outTrandID"></param>
        /// <returns></returns>
        public int UpdateOutTrandID(string inPatientNo, string outTrandID)
        {
            if (string.IsNullOrEmpty(inPatientNo) || string.IsNullOrEmpty(outTrandID))
            {
                return 0;
            }
            string sql = @"update fin_ipr_siinmaininfo_yd d
                    set d.out_transid = '{1}'
                    and d.inpatient_no = '{0}'";
            sql = string.Format(sql, inPatientNo, outTrandID);

            return this.ucMgr.ExecNoQuery(sql);
        }
        public void GetHosCodeAndRegionCode(ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            #region old
            //personInfo.InsuredAreaCode = "440500";// "440400"; // 
            //personInfo.HospitalizeCenterAreaCode = "";//北窖 :440606  禅城：440605
            //personInfo.HospitalizeAreaCode = "440600";//佛山:"440400";  北窖：440600
            //personInfo.HospitalCode = "45607420044060411A5201";//北窖测试 ：4400001059984 佛山三院测试：4400001059978
            //personInfo.HospitalName = "佛山市第三人民医院";  //佛山市顺德区北滘医院 ；佛山市第三人民医院;佛山市第四人民医院(佛山市结核病防治所)
            //三院测试 :"4400001059978";
            //三院正式： "45592698544040211A1001";//"359292440402313931";//"73759012144011111A1001";//
            //personInfo.ZJLX = "01";
            #endregion 

            #region New
            XmlDocument doc = new XmlDocument();
            doc.Load(profileName);

            XmlNode node = doc.SelectSingleNode(@"/SIConfig/HospitalizeCenterAreaCode");
            personInfo.HospitalizeCenterAreaCode = node.InnerText.Trim();

            node = doc.SelectSingleNode(@"/SIConfig/HospitalizeAreaCode");
            personInfo.HospitalizeAreaCode = node.InnerText.Trim();

            node = doc.SelectSingleNode(@"/SIConfig/HospitalCode");
            personInfo.HospitalCode = node.InnerText.Trim();

            node = doc.SelectSingleNode(@"/SIConfig/HospitalName");
            personInfo.HospitalName = node.InnerText.Trim();
            #endregion
        }


        /// <summary>
        /// 获取对照项目
        /// </summary>
        /// <param name="pactCode">合同单位代码</param>
        /// <param name="alFeeDetail">门诊费用明细</param>
        /// <param name="noCompareTotCost">未对照项目总金额</param>
        /// <param name="alFeeUpload">已对照项目</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public int GetCompareItem(string pactCode, ArrayList alFeeDetail, out decimal noCompareTotCost, out ArrayList alFeeUpload, ref string errMsg)
        {

            string sbNoCompare = "";
            FS.HISFC.BizProcess.Integrate.Fee Fee = new FS.HISFC.BizProcess.Integrate.Fee();
            string itemCodeCompareType = Fee.GetControlValue("FSMZ16", "0");
            alFeeUpload = new ArrayList();
            if (itemCodeCompareType == "0")
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo f in alFeeDetail)
                {
                    if (string.IsNullOrEmpty(f.Item.GBCode) || f.Item.GBCode == "0")
                    {
                        sbNoCompare  = sbNoCompare + "/n/r"+"项目【" + f.Item.Name + "】没有维护医保对照码(国标码为空)，请先进行维护！";
                    }
                    else
                    {
                        f.Item.ID = f.Item.GBCode;
                    }
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo f in alFeeDetail)
                {
                    if (string.IsNullOrEmpty(f.Item.UserCode) || f.Item.UserCode == "0")
                    {
                        sbNoCompare  =sbNoCompare + "/n/r"+ "项目【" + f.Item.Name + "】没有维护医保对照码(自定义码为空)，请先进行维护！";
                    }
                    else
                    {
                        f.Item.ID = f.Item.UserCode;
                    }
                }
            }
            noCompareTotCost = 0;
            
            #region 作废佛山有独立方法
            //经像信息科确定佛山医保所有结算类型使用的对照完全一致
            //pactCode = "14";
            //try
            //{
            //    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeDetail)
            //    {
            //        if (this.hsCompareItems.Contains(pactCode + feeItem.Item.ID))
            //        {
            //            feeItem.Compare = this.hsCompareItems[pactCode + feeItem.Item.ID] as FS.HISFC.Models.SIInterface.Compare;

            //            feeItem.Item.UserCode = feeItem.Compare.SpellCode.UserCode;
            //            alFeeUpload.Add(feeItem);
            //        }
            //        else
            //        {
            //            if (SIMgr.GetCompareSingleItem(pactCode, feeItem.Item.ID, ref feeItem.Compare) < 0)
            //            {
            //                //未找到对照项目
            //                if (sbNoCompare.Length == 0)
            //                {
            //                    sbNoCompare.Append("未对照项目(" + pactCode + ")：\n");
            //                }
            //                noCompareTotCost += feeItem.FT.TotCost;

            //                string perTip = "项目代码：" + feeItem.Item.ID + "项目名称：" + feeItem.Item.Name.Trim() + "\n";
            //                if (!sbNoCompare.ToString().Contains(perTip))
            //                {
            //                    sbNoCompare.Append(perTip);
            //                }

            //                continue;
            //            }
            //            else
            //            {
            //                feeItem.Item.UserCode = feeItem.Compare.SpellCode.UserCode;
            //                alFeeUpload.Add(feeItem);
            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeItem in alFeeDetail)
            //    {
            //        if (this.hsCompareItems.Contains(pactCode + feeItem.Item.ID))
            //        {
            //            feeItem.Compare = this.hsCompareItems[pactCode + feeItem.Item.ID] as FS.HISFC.Models.SIInterface.Compare;

            //            feeItem.Item.UserCode = feeItem.Compare.SpellCode.UserCode;
            //            alFeeUpload.Add(feeItem);
            //        }
            //        else
            //        {
            //            if (SIMgr.GetCompareSingleItem(pactCode, feeItem.Item.ID, ref feeItem.Compare) < 0)
            //            {
            //                //未找到对照项目
            //                if (sbNoCompare.Length == 0)
            //                {
            //                    sbNoCompare.Append("未对照项目(" + pactCode + ")：\n");
            //                }
            //                noCompareTotCost += feeItem.FT.TotCost;
            //                //sbNoCompare.Append("项目代码：" + feeItem.Item.ID + "项目名称：" + feeItem.Item.Name.Trim() + "\n");
            //                string perTip = "项目代码：" + feeItem.Item.ID + "项目名称：" + feeItem.Item.Name.Trim() + "\n";
            //                if (!sbNoCompare.ToString().Contains(perTip))
            //                {
            //                    sbNoCompare.Append(perTip);
            //                }
            //                continue;
            //            }
            //            else
            //            {
            //                feeItem.Item.UserCode = feeItem.Compare.SpellCode.UserCode;
            //                alFeeUpload.Add(feeItem);
            //            }
            //        }
            //    }
            //}
            #endregion 

            if (sbNoCompare.Length != 0)
            {
                errMsg = sbNoCompare;
                return -1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 【异地】设置住院身份确认信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        private void SetYdInPatientAccreditationInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            patientInfo.SIMainInfo.AnotherCity.User01 = personInfo.SIType;
            patientInfo.SIMainInfo.RegNo = personInfo.ClinicNo;
            //其他直接存医保主表
        }

        /// <summary>
        /// 【异地】2.2.1.1 身份识别 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetYdInPatientAccreditationXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            this.GetHosCodeAndRegionCode(ref personInfo);

            #region 患者参数
            //数字签名
            XmlElement sign = xml.CreateElement("sign");//数字签名
            sign.InnerText = "";
            root.AppendChild(sign);
            //交易流水号
            XmlElement transid = xml.CreateElement("transid");//交易流水号
            transid.InnerText = "";
            root.AppendChild(transid);
            //行政区划代码（就医地）
            XmlElement node8 = xml.CreateElement("aab299");//行政区划代码（就医地）
            node8.InnerText = "440600";
            root.AppendChild(node8);
            //就医地分中心编码
            XmlElement node3 = xml.CreateElement("yab600");//就医地分中心编码
            node3.InnerText =  personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);
            //医疗机构执业许可证登记号
            XmlElement node9 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            node9.InnerText = personInfo.HospitalCode;
            root.AppendChild(node9);
            //行政区划代码（参保地）
            XmlElement node10 = xml.CreateElement("aab301");//行政区划代码（参保地）
            node10.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(node10);

            //参保地社保分支机构代码
            XmlElement node11 = xml.CreateElement("yab060");//参保地社保分支机构代码
            node11.InnerText = "";
            root.AppendChild(node11);

            //社会保障号码
            XmlElement node6 = xml.CreateElement("aac002");//社会保障号码
            node6.InnerText = (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(node6);

            //证件号码
            XmlElement node12 = xml.CreateElement("aac043");//证件号码
            node12.InnerText = personInfo.ZJLX;
            root.AppendChild(node12);

            //证件号码
            XmlElement node20 = xml.CreateElement("aac044");//证件号码
            node20.InnerText = patientInfo.IDCard;
            root.AppendChild(node20);

            //发生日期
            XmlElement node7 = xml.CreateElement("ake007");//发生日期
            node7.InnerText = patientInfo.PVisit.InTime.ToString("yyyyMMdd");
            root.AppendChild(node7);
            #endregion

            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 【异地】2.2.1.1 身份识别 获取返回实体
        /// </summary>
        /// <param name="retXML"></param>
        /// <param name="patientInfo"></param>
        /// <param name="?"></param>
        public void GetYdInPatientAccreditationResult(string retXML, FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(retXML);
            }
            catch (Exception e)
            {
                return;
            }

            #region 作废
            //XmlNode stateNode = doc.SelectSingleNode("result/output/状态");
            //if (stateNode != null)
            //{
            //    status = stateNode.InnerText;
            //}
            //else
            //{
            //    status = "";
            //}

            //XmlNode memoNode = doc.SelectSingleNode("XML/备注");
            //if (memoNode != null)
            //{
            //    msg = memoNode.InnerText;
            //}
            //else
            //{
            //    msg = "";
            //}
            #endregion

            XmlNode NameNode = doc.SelectSingleNode("result/output/aac003");//姓名
            if (NameNode != null)
            {
                personInfo.Name = NameNode.InnerText;
            }
            else
            {
                personInfo.Name = "";
            }

            XmlNode IdenNoNode = doc.SelectSingleNode("result/output/aac002");//社会保障号码
            if (IdenNoNode != null)
            {
                personInfo.MCardNo = IdenNoNode.InnerText;
                patientInfo.SSN = IdenNoNode.InnerText;
            }
            else
            {
                personInfo.MCardNo = "";
                personInfo.IdenNo = "";
            }

            XmlNode IdenNoNode2 = doc.SelectSingleNode("result/output/aac044");//证件号码
            if (IdenNoNode2 != null && IdenNoNode2.InnerText != patientInfo.SSN)
            {
                personInfo.IdenNo = IdenNoNode2.InnerText;
            }
            else
            {
                personInfo.IdenNo = "";
            }


            XmlNode MCardNoNode = doc.SelectSingleNode("result/output/aac001");//医保编号
            if (MCardNoNode != null)
            {
                personInfo.MCardNo = MCardNoNode.InnerText;
            }
            else
            {
                personInfo.MCardNo = "";
            }


            XmlNode SexNode = doc.SelectSingleNode("result/output/aac004");//性别
            if (SexNode != null)
            {
                personInfo.Sex = SexNode.InnerText;
            }
            else
            {
                personInfo.Sex = "";
            }

            XmlNode BirthNode = doc.SelectSingleNode("result/output/aac006");//出生日期
            if (BirthNode != null)
            {
                personInfo.Birth = BirthNode.InnerText;
            }
            else
            {
                personInfo.Birth = "0";
            }

            XmlNode PersonTypeNode = doc.SelectSingleNode("result/output/akc021");//人员类别
            if (PersonTypeNode != null)
            {
                personInfo.PersonType = PersonTypeNode.InnerText;
            }
            else
            {
                personInfo.PersonType = "";
            }

            XmlNode RQtypeNode = doc.SelectSingleNode("result/output/akc300");//人群类别
            if (RQtypeNode != null)
            {
                personInfo.RQtype = RQtypeNode.InnerText;
            }
            else
            {
                personInfo.RQtype = "";
            }

            XmlNode GWYflagNode = doc.SelectSingleNode("result/output/akc026");//公务员标识
            if (GWYflagNode != null)
            {
                personInfo.GWYflag = GWYflagNode.InnerText;
            }
            else
            {
                personInfo.GWYflag = "";
            }

            XmlNode AgeNode = doc.SelectSingleNode("result/output/akc023");//实际年龄
            if (AgeNode != null)
            {
                personInfo.Age = AgeNode.InnerText;
            }
            else
            {
                personInfo.Age = "";
            }

            XmlNode PayYearsNode = doc.SelectSingleNode("result/output/aae379");//缴费年限
            if (PayYearsNode != null)
            {
                personInfo.PayYears = PayYearsNode.InnerText;
            }
            else
            {
                personInfo.PayYears = "0";
            }

            XmlNode RestAccountNode = doc.SelectSingleNode("result/output/akc252");//帐户余额
            if (RestAccountNode != null)
            {
                personInfo.RestAccount = RestAccountNode.InnerText;
            }
            else
            {
                personInfo.RestAccount = "0";
            }

            XmlNode CompanyCodeNode = doc.SelectSingleNode("result/output/aab001");//单位编码
            if (CompanyCodeNode != null)
            {
                personInfo.CompanyCode = CompanyCodeNode.InnerText;
            }
            else
            {
                personInfo.CompanyCode = "";
            }

            XmlNode CompanyNameNode = doc.SelectSingleNode("result/output/aab004");//单位名称
            if (CompanyNameNode != null)
            {
                personInfo.CompanyName = CompanyNameNode.InnerText;
            }
            else
            {
                personInfo.CompanyName = "";
            }

            //XmlNode InsuredCenterAreaCodeNode = doc.SelectSingleNode("result/output/yab060");//参保地分中心编号
            //if (InsuredCenterAreaCodeNode != null)
            //{
            //    personInfo.InsuredCenterAreaCode = InsuredCenterAreaCodeNode.InnerText;
            //}
            //else
            //{
            //    personInfo.InsuredCenterAreaCode = "";
            //}

            XmlNode SumCostLineNode = doc.SelectSingleNode("result/output/yka116");//起付线累计
            if (SumCostLineNode != null)
            {
                personInfo.SumCostLine = SumCostLineNode.InnerText;
            }
            else
            {
                personInfo.SumCostLine = "0";
            }

            XmlNode LimitCostJBYLNode = doc.SelectSingleNode("result/output/yka119");//基本医疗本次支付限额
            if (LimitCostJBYLNode != null)
            {
                personInfo.LimitCostJBYL = LimitCostJBYLNode.InnerText;
            }
            else
            {
                personInfo.LimitCostJBYL = "0";
            }

            XmlNode LimitCostDBYLNode = doc.SelectSingleNode("result/output/yka121");//大病医疗本次支付限额
            if (LimitCostDBYLNode != null)
            {
                personInfo.LimitCostDBYL = LimitCostDBYLNode.InnerText;
            }
            else
            {
                personInfo.LimitCostDBYL = "0";
            }

            XmlNode LimitCostGWYNode = doc.SelectSingleNode("result/output/yka123");//公务员本次支付限额
            if (LimitCostGWYNode != null)
            {
                personInfo.LimitCostGWY = LimitCostGWYNode.InnerText;
            }
            else
            {
                personInfo.LimitCostGWY = "0";
            }

            XmlNode SumCostJBYLNode = doc.SelectSingleNode("result/output/ake092");//本年度基本医疗保险统筹基金累计支付金额
            if (SumCostJBYLNode != null)
            {
                personInfo.SumCostJBYL = FS.FrameWork.Function.NConvert.ToDecimal(SumCostJBYLNode.InnerText);
            }
            else
            {
                personInfo.SumCostJBYL = 0;
            }

            XmlNode SumCostDBYLNode = doc.SelectSingleNode("result/output/yka437");//大病医疗统筹累计
            if (SumCostDBYLNode != null)
            {
                personInfo.SumCostDBYL = SumCostDBYLNode.InnerText;
            }
            else
            {
                personInfo.SumCostDBYL = "0";
            }


            XmlNode InTimesNode = doc.SelectSingleNode("result/output/akc200");//本年度住院次数
            if (InTimesNode != null)
            {
                personInfo.InTimes = InTimesNode.InnerText;
            }
            else
            {
                personInfo.InTimes = "";
            }

            XmlNode ReturnsFlagNode = doc.SelectSingleNode("result/output/ykc667");//二次返院审批标志
            if (ReturnsFlagNode != null)
            {
                personInfo.ReturnsFlag = ReturnsFlagNode.InnerText;
            }
            else
            {
                personInfo.ReturnsFlag = "";
            }

            XmlNode ChangeOutFlagNode = doc.SelectSingleNode("result/output/ake132");//转院标志
            if (ChangeOutFlagNode != null)
            {
                personInfo.ChangeOutFlag = ChangeOutFlagNode.InnerText;
            }
            else
            {
                personInfo.ChangeOutFlag = "";
            }

            XmlNode ChangeOutClinicNoNode = doc.SelectSingleNode("result/output/ykc669");//转出就诊登记号
            if (ChangeOutClinicNoNode != null)
            {
                personInfo.ChangeOutClinicNo = ChangeOutClinicNoNode.InnerText;
            }
            else
            {
                personInfo.ChangeOutClinicNo = "";
            }

            XmlNode ChangeOutHosNameNode = doc.SelectSingleNode("result/output/ykc678");//转出医院名称");
            if (ChangeOutHosNameNode != null)
            {
                personInfo.ChangeOutHosName = ChangeOutHosNameNode.InnerText;
            }
            else
            {
                personInfo.ChangeOutHosName = "";
            }


            XmlNode ChangeInHosNameNode = doc.SelectSingleNode("result/output/ykc670");//登记转入医院名称");
            if (ChangeInHosNameNode != null)
            {
                personInfo.ChangeInHosName = ChangeInHosNameNode.InnerText;
            }
            else
            {
                personInfo.ChangeInHosName = "";
            }

            XmlNode SeeDocTypeNode = doc.SelectSingleNode("result/output/aka130");//就诊类别");
            if (SeeDocTypeNode != null)
            {
                personInfo.SeeDocType = SeeDocTypeNode.InnerText;
            }
            else
            {
                personInfo.SeeDocType = "";
            }

            XmlNode ChangeTypeNode = doc.SelectSingleNode("result/output/ykc682");//转院类别");
            if (ChangeTypeNode != null)
            {
                personInfo.ChangeType = ChangeTypeNode.InnerText;
            }
            else
            {
                personInfo.ChangeType = "";
            }

            XmlNode ChangeDateNode = doc.SelectSingleNode("result/output/ake014");//转出日期");
            if (ChangeDateNode != null)
            {
                personInfo.ChangeDate = ChangeDateNode.InnerText;
            }
            else
            {
                personInfo.ChangeDate = "0";
            }

            XmlNode ChangeDiagnNode = doc.SelectSingleNode("result/output/ykc672");//转诊诊断");
            if (ChangeDiagnNode != null)
            {
                personInfo.ChangeDiagn = ChangeDiagnNode.InnerText;
            }
            else
            {
                personInfo.ChangeDiagn = "";
            }

            XmlNode ChangePassFlagNode = doc.SelectSingleNode("result/output/ykc673");//转诊转院审批结果");
            if (ChangePassFlagNode != null)
            {
                personInfo.ChangePassFlag = ChangePassFlagNode.InnerText;
            }
            else
            {
                personInfo.ChangePassFlag = "";
            }

            XmlNode ChangeReasonNode = doc.SelectSingleNode("result/output/ykc674");//转诊原因");
            if (ChangeReasonNode != null)
            {
                personInfo.ChangeReason = ChangeReasonNode.InnerText;
            }
            else
            {
                personInfo.ChangeReason = "";
            }

            XmlNode ZJLXNode = doc.SelectSingleNode("result/output/aac043");//证件类型");
            if (ZJLXNode != null)
            {
                personInfo.ZJLX = ZJLXNode.InnerText;
            }
            else
            {
                personInfo.ZJLX = "";
            }

            XmlNode nationNode = doc.SelectSingleNode("result/output/aac005");//民族");
            if (nationNode != null)
            {
                personInfo.nation = nationNode.InnerText;
            }
            else
            {
                personInfo.nation = "";
            }

            XmlNode ZZJGDMNode = doc.SelectSingleNode("result/output/aab003");//组织机构代码");
            if (ZZJGDMNode != null)
            {
                personInfo.ZZJGDM = ZZJGDMNode.InnerText;
            }
            else
            {
                personInfo.ZZJGDM = "";
            }

            XmlNode YDJYBAHNode = doc.SelectSingleNode("result/output/yzz014");//异地就医备案号");
            if (YDJYBAHNode != null)
            {
                personInfo.YDJYBAH = YDJYBAHNode.InnerText;
            }
            else
            {
                personInfo.YDJYBAH = "";
            }


            //4.0修订
            XmlNode ZYZTNode = doc.SelectSingleNode("result/output/akc068");//在院状态
            if (ZYZTNode != null)
            {
                personInfo.ZYZT = ZYZTNode.InnerText;
            }
            else
            {
                personInfo.ZYZT = "";
            }

            XmlNode JZDXLXNode = doc.SelectSingleNode("result/output/ykc751");//救助对象类型
            if (JZDXLXNode != null)
            {
                personInfo.JZDXLX = JZDXLXNode.InnerText;
            }
            else
            {
                personInfo.JZDXLX = "";
            }

            XmlNode BNDYLJZLJJENode = doc.SelectSingleNode("result/output/ykc753");//本年度医疗救助累计金额
            if (BNDYLJZLJJENode != null)
            {
                personInfo.BNDYLJZLJJE = FS.FrameWork.Function.NConvert.ToDecimal(BNDYLJZLJJENode.InnerText);
            }
            else
            {
                personInfo.BNDYLJZLJJE = 0;
            }
            XmlNode BCYLJZZFXENode = doc.SelectSingleNode("result/output/ykc754");//本次医疗救助支付限额
            if (BCYLJZZFXENode != null)
            {
                personInfo.BCYLJZZFXE = FS.FrameWork.Function.NConvert.ToDecimal(BCYLJZZFXENode.InnerText);
            }
            else
            {
                personInfo.BCYLJZZFXE = 0;
            }
            this.SetYdInPatientAccreditationInfo(patientInfo, personInfo);
        }

        /// <summary>
        /// 【异地】2.2.1.2 就诊登记 获取XML
        /// </summary>
        /// <param name="patientInfo">住院患者信息实体</param>
        /// <param name="IsSY">是否生育结算</param>
        /// <param name="IsGS">是否工伤结算</param>
        /// <returns></returns>
        public string SetYdInPatientRegXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            //工伤的入院时间格式是yyyyMMddHHmm
            //其他的入院时间格式是yyyyMMddHH
            string inDateStyle = string.Empty;
            if (personInfo.GSFlag.Equals("1"))
            {
                inDateStyle = "yyyyMMddHHmm";
            }
            else
            {
                inDateStyle = "yyyyMMdd";
            }


            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            #region 患者参数
            //数字签名
            XmlElement sign = xml.CreateElement("sign");//数字签名
            sign.InnerText = "";
            root.AppendChild(sign);
            //交易流水号
            XmlElement transid = xml.CreateElement("transid");//交易流水号
            transid.InnerText = "";
            root.AppendChild(transid);
            ////参保地统筹区编码
            //XmlElement node1 = xml.CreateElement("yab003");//参保地统筹区编码");
            //node1.InnerText = personInfo.InsuredAreaCode;
            //root.AppendChild(node1);

            ////参保地统筹区编码
            //XmlElement cbdfzxbmNode = xml.CreateElement("参保地分中心编码");
            //cbdfzxbmNode.InnerText = personInfo.InsuredCenterAreaCode;
            //root.AppendChild(cbdfzxbmNode);

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            if (personInfo.ZJLX != "90")
            {
                patientInfo.SSN = "-";
            }
            aac002.InnerText = (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(aac002);

            //证件类型
            XmlElement aac043 = xml.CreateElement("aac043");//证件类型
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = patientInfo.IDCard;
            root.AppendChild(aac044);

            //病历号
            XmlElement node8 = xml.CreateElement("ykc009");//病历号");
            node8.InnerText = patientInfo.ID;
            root.AppendChild(node8);

            //病历号
            XmlElement akc190 = xml.CreateElement("akc190");//病历号");
            akc190.InnerText = patientInfo.PID.PatientNO;
            root.AppendChild(akc190);

            //医疗类别
            XmlElement aka130 = xml.CreateElement("aka130");//医疗类别");
            aka130.InnerText = personInfo.SeeDocType;
            root.AppendChild(aka130);

            //科室
            string compareDeptCode = this.SIMgr.GetCompareDeptCodeYD(patientInfo.PVisit.PatientLocation.Dept.ID);
            if (string.IsNullOrEmpty(compareDeptCode))
            {
                //需要增加科室选择(未完成)
                //compareDeptCode = patientInfo.PVisit.PatientLocation.Dept.Name.ToString();
                MessageBox.Show("当前入院科室未对照，请在FIN_COM_SIDEPTCOMPARE_YD表中维护对照科室", "跨省异地入院登记");
                return "";
            }
            XmlElement node10 = xml.CreateElement("akf001");//入院科室");
            node10.InnerText = compareDeptCode;
            root.AppendChild(node10);

            //入院病区号
            XmlElement node29 = xml.CreateElement("yzz018");//入院病区号");
            node29.InnerText = "";// personInfo.InNurseDeptCode;
            root.AppendChild(node29);

            //入院病区名称
            XmlElement node30 = xml.CreateElement("yzz019");//入院病区名称");
            node30.InnerText = "";// personInfo.InNurseDeptName;
            root.AppendChild(node30);

            //入院床位
            XmlElement node11 = xml.CreateElement("ykc012");//入院床位");
            node11.InnerText = patientInfo.PVisit.PatientLocation.Bed.ID; //"1";//
            root.AppendChild(node11);

            //入院诊断
            XmlElement node12 = xml.CreateElement("akc050");//入院诊断");
            node12.InnerText = patientInfo.ClinicDiagnose; //暂时使用门诊诊断
            root.AppendChild(node12);

            //住院原因
            XmlElement node14 = xml.CreateElement("ykc679");//住院原因");
            node14.InnerText = personInfo.in_reason;// "5";//暂时不处理
            root.AppendChild(node14);

            //补助类型字段
            XmlElement node15 = xml.CreateElement("ykc680");//补助类型字段");
            node15.InnerText = personInfo.bzlx;// "1";//暂时不处理
            root.AppendChild(node15);

            //入院诊断疾病编码1_ICD10
            XmlElement node16 = xml.CreateElement("akc193");//入院诊断疾病编码1_ICD10");
            node16.InnerText = personInfo.InDiagn;//暂时不处理
            root.AppendChild(node16);

            //入院诊断编码2_ICD10
            XmlElement node17 = xml.CreateElement("ykc601");//入院诊断编码2_ICD10");
            node17.InnerText = "";//暂时不处理
            root.AppendChild(node17);

            //入院诊断编码3_ICD10
            XmlElement node19 = xml.CreateElement("ykc602");//入院诊断编码3_ICD10");
            node19.InnerText = "";//暂时不处理
            root.AppendChild(node19);

            //医师执业证编码
            XmlElement akc056 = xml.CreateElement("akc056");//医师执业证编码");
            akc056.InnerText = "";//可为空且没有这个东西。"52010319870919482X";//personInfo.yszyzbm;
            root.AppendChild(akc056);

            //入院诊断医生
            XmlElement ake022 = xml.CreateElement("ake022");//入院诊断医生");
            ake022.InnerText = "";
            root.AppendChild(ake022);

            //入院时间,不能为空
            XmlElement node21 = xml.CreateElement("ykc701");//入院时间");
            node21.InnerText = patientInfo.PVisit.InTime.ToString("yyyyMMdd");//yyyyMMdd");
            root.AppendChild(node21);


            //入院经办人
            XmlElement node22 = xml.CreateElement("aae011");//入院经办人");
            node22.InnerText = FS.FrameWork.Management.Connection.Operator.Name;
            root.AppendChild(node22);

            //经办时间,不能为空
            XmlElement node23 = xml.CreateElement("aae036");//经办时间");//("xxxxxxx");//就诊登记_经办时间");
            //node23.InnerText = patientInfo.PVisit.InTime.ToString("yyyyMMdd");//yyyyMMdd");
            node23.InnerText = DateTime.Now.ToString("yyyyMMdd");
            root.AppendChild(node23);

            //患者联系人
            XmlElement node24 = xml.CreateElement("aae004");//患者联系人");
            node24.InnerText = patientInfo.Kin.Name;
            root.AppendChild(node24);

            //患者联系电话
            XmlElement node25 = xml.CreateElement("aae005");//患者联系电话");
            node25.InnerText = patientInfo.Kin.RelationPhone;
            root.AppendChild(node25);

            //备注
            XmlElement node26 = xml.CreateElement("aae013");//备注");
            node26.InnerText = personInfo.Memo;
            root.AppendChild(node26);

            //4.0修订
            //救助对象类型
            XmlElement node27 = xml.CreateElement("ykc751");//救助对象类型");
            node27.InnerText = personInfo.JZDXLX;
            root.AppendChild(node27);
            ////医保编号
            //XmlElement node4 = xml.CreateElement("aac001");//医保编号");
            //node4.InnerText = (patientInfo.SSN == "" ? "-" : patientInfo.SSN);
            //root.AppendChild(node4);

            ////医院编码
            //XmlElement node5 = xml.CreateElement("akb020");//医院编码");
            //node5.InnerText = personInfo.HospitalCode;
            //root.AppendChild(node5);

            ////医院名称
            //XmlElement node6 = xml.CreateElement("akb021");//医院名称");
            //node6.InnerText = FS.FrameWork.Management.Connection.Hospital.Name;
            //root.AppendChild(node6);

            ////就诊类别
            //XmlElement node7 = xml.CreateElement("aka130");//就诊类别");
            //node7.InnerText = personInfo.SeeDocType;
            //root.AppendChild(node7);

           

            ////住院号
            //XmlNode patientNoNode = xml.CreateElement("ykc010");//住院号");
            //patientNoNode.InnerText = patientInfo.PID.PatientNO;
            //root.AppendChild(patientNoNode);

          

            ////病种编码
            //XmlElement node13 = xml.CreateElement("aka120");//病种编码");
            //node13.InnerText = "";//暂时不处理
            //root.AppendChild(node13);

            ////入院诊断医生
            //XmlElement node20 = xml.CreateElement("ykc008");//入院诊断医生");
            //node20.InnerText = personInfo.docIdenno;//"52010319870919482X";//暂时不处理
            //root.AppendChild(node20);


            #region
          
            #endregion
            #endregion

            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 【异地】2.2.1.2 就诊登记 获取返回实体
        /// </summary>
        /// <param name="retXML"></param>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        public void GetYdInPatientRegResult(string retXML, FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(retXML);
            }
            catch (Exception e)
            {
                return;
            }

            //XmlNode MCardNoNode = doc.SelectSingleNode("result/output/aac001");//医保编号");
            //if (MCardNoNode != null)
            //{
            //    personInfo.MCardNo = MCardNoNode.InnerText;
            //}
            //else
            //{
            //    personInfo.MCardNo = "";
            //}

            XmlNode transid = doc.SelectSingleNode("result/transid");//交易流水号");
            if (transid != null)
            {
                personInfo.regTransID = transid.InnerText;
            }
            else
            {
                personInfo.regTransID = "";
            }

            XmlNode ClinicNoNode = doc.SelectSingleNode("result/output/ykc700");//就诊登记号");
            if (ClinicNoNode != null)
            {
                personInfo.ClinicNo = ClinicNoNode.InnerText;
            }
            else
            {
                personInfo.ClinicNo = "";
            }

            XmlNode InsuredAreaCode = doc.SelectSingleNode("result/output/aab301");//参保地分中心编号");
            if (InsuredAreaCode != null)
            {
                personInfo.InsuredAreaCode = InsuredAreaCode.InnerText;
            }
            else
            {
                personInfo.InsuredAreaCode = "";
            }

            XmlNode InsuredCenterAreaCodeNode = doc.SelectSingleNode("result/output/yab060");//参保地分中心编号");
            if (InsuredCenterAreaCodeNode != null)
            {
                personInfo.InsuredCenterAreaCode = InsuredCenterAreaCodeNode.InnerText;
            }
            else
            {
                personInfo.InsuredCenterAreaCode = "";
            }

            XmlNode SSN = doc.SelectSingleNode("result/output/aac002");//社会保障号码");
            if (SSN != null)
            {
                patientInfo.SSN = SSN.InnerText;
                personInfo.MCardNo = SSN.InnerText;
            }
            else
            {
                patientInfo.SSN = "";
                personInfo.MCardNo = "";
            }

            XmlNode ZJLX = doc.SelectSingleNode("result/output/aac043");//证件类型");
            if (ZJLX != null)
            {
                personInfo.ZJLX = ZJLX.InnerText;
            }
            else
            {
                personInfo.ZJLX = "";
            }

            XmlNode IdenNoNode = doc.SelectSingleNode("result/output/aac044");//证件号码");
            if (IdenNoNode != null )
            {
                personInfo.IdenNo = IdenNoNode.InnerText;
            }
            else
            {
                personInfo.IdenNo = "";
            }

            XmlNode NameNode = doc.SelectSingleNode("result/output/aac003");//姓名");
            if (NameNode != null)
            {
                personInfo.Name = NameNode.InnerText;
            }
            else
            {
                personInfo.Name = "";
            }

            XmlNode SexNode = doc.SelectSingleNode("result/output/aac004");//性别");
            if (SexNode != null)
            {
                personInfo.Sex = SexNode.InnerText;
            }
            else
            {
                personInfo.Sex = "";
            }

            XmlNode nation = doc.SelectSingleNode("result/output/aac005");//民族");
            if (nation != null)
            {
                personInfo.nation = nation.InnerText;
            }
            else
            {
                personInfo.nation = "";
            }


            XmlNode BirthNode = doc.SelectSingleNode("result/output/aac006");//出生日期");
            if (BirthNode != null)
            {
                personInfo.Birth = BirthNode.InnerText;
            }
            else
            {
                personInfo.Birth = "0001-01-01";
            }

            //XmlNode PersonTypeNode = doc.SelectSingleNode("result/output/xxxxxxx");//人员类别");
            XmlNode PersonTypeNode = doc.SelectSingleNode("result/output/ykc021");//人员类别");
            if (PersonTypeNode != null)
            {
                personInfo.PersonType = PersonTypeNode.InnerText;
            }
            else
            {
                personInfo.PersonType = "";
            }

            XmlNode RQtypeNode = doc.SelectSingleNode("result/output/ykc300");//人群类别");
            if (RQtypeNode != null)
            {
                personInfo.PersonType = RQtypeNode.InnerText;
            }
            else
            {
                personInfo.PersonType = "";
            }

            XmlNode xzlxNode = doc.SelectSingleNode("result/output/aae140");//险种类型");
            if (xzlxNode != null)
            {
                personInfo.xzlx = xzlxNode.InnerText;
            }
            else
            {
                personInfo.xzlx = "";
            }

            XmlNode GWYflagNode = doc.SelectSingleNode("result/output/akc026");//公务员标识");
            if (GWYflagNode != null)
            {
                personInfo.GWYflag = GWYflagNode.InnerText;
            }
            else
            {
                personInfo.GWYflag = "";
            }

            XmlNode AgeNode = doc.SelectSingleNode("result/output/akc023");///实足年龄");
            if (AgeNode != null)
            {
                personInfo.Age = AgeNode.InnerText;
            }
            else
            {
                personInfo.Age = "";
            }

            XmlNode CompanyCodeNode = doc.SelectSingleNode("result/output/aab001");//单位编码");
            if (CompanyCodeNode != null)
            {
                personInfo.CompanyCode = CompanyCodeNode.InnerText;
            }
            else
            {
                personInfo.CompanyCode = "";
            }

            XmlNode ZZJGDMNode = doc.SelectSingleNode("result/output/aab003");//组织机构代码");
            if (ZZJGDMNode != null)
            {
                personInfo.ZZJGDM = ZZJGDMNode.InnerText;
            }
            else
            {
                personInfo.ZZJGDM = "";
            }

            XmlNode CompanyNameNode = doc.SelectSingleNode("result/output/aab004");//单位名称");
            if (CompanyNameNode != null)
            {
                personInfo.CompanyName = CompanyNameNode.InnerText;
            }
            else
            {
                personInfo.CompanyName = "";
            }

            XmlNode EconomicTypeNode = doc.SelectSingleNode("result/output/aab020");//经济类型");
            if (EconomicTypeNode != null)
            {
                personInfo.EconomicType = EconomicTypeNode.InnerText;
            }
            else
            {
                personInfo.EconomicType = "";
            }

            XmlNode CompanyTypeNode = doc.SelectSingleNode("result/output/aab019");//单位类型");
            if (CompanyTypeNode != null)
            {
                personInfo.CompanyType = CompanyTypeNode.InnerText;
            }
            else
            {
                personInfo.CompanyType = "";
            }

            //隶属关系,不处理

            XmlNode PayYearsNode = doc.SelectSingleNode("result/output/aae379");//缴费年限");
            if (PayYearsNode != null)
            {
                personInfo.PayYears = PayYearsNode.InnerText;
            }
            else
            {
                personInfo.PayYears = "0";
            }

            XmlNode RestAccountNode = doc.SelectSingleNode("result/output/akc252");//帐户余额");
            if (RestAccountNode != null)
            {
                personInfo.RestAccount = RestAccountNode.InnerText;
            }
            else
            {
                personInfo.RestAccount = "0";
            }

            XmlNode SumCostLineNode = doc.SelectSingleNode("result/output/yka116");//起付线累计");
            if (SumCostLineNode != null)
            {
                personInfo.SumCostLine = SumCostLineNode.InnerText;
            }
            else
            {
                personInfo.SumCostLine = "0";
            }

            //XmlNode CostLineNode = doc.SelectSingleNode("result/output/yka115");//本次应付起付线");
            //if (CostLineNode != null)
            //{
            //    personInfo.CostLine = CostLineNode.InnerText;
            //}
            //else
            //{
            //    personInfo.CostLine = "0";
            //}

            XmlNode LimitCostJBYLNode = doc.SelectSingleNode("result/output/yka119");//基本医疗本次支付限额");
            if (LimitCostJBYLNode != null)
            {
                personInfo.LimitCostJBYL = LimitCostJBYLNode.InnerText;
            }
            else
            {
                personInfo.LimitCostJBYL = "0";
            }

            XmlNode LimitCostDBYLNode = doc.SelectSingleNode("result/output/yka121");//大病医疗本次支付限额");
            if (LimitCostDBYLNode != null)
            {
                personInfo.LimitCostDBYL = LimitCostDBYLNode.InnerText;
            }
            else
            {
                personInfo.LimitCostDBYL = "0";
            }

            XmlNode LimitCostGWYNode = doc.SelectSingleNode("result/output/yka123");//公务员本次支付限额");
            if (LimitCostGWYNode != null)
            {
                personInfo.LimitCostGWY = LimitCostGWYNode.InnerText;
            }
            else
            {
                personInfo.LimitCostGWY = "0";
            }

            XmlNode SumCostJBYLNode = doc.SelectSingleNode("result/output/ake092");//基本医疗统筹累计");
            if (SumCostJBYLNode != null)
            {
                personInfo.SumCostJBYL = FS.FrameWork.Function.NConvert.ToDecimal(SumCostJBYLNode.InnerText);
            }
            else
            {
                personInfo.SumCostJBYL = 0;
            }

            XmlNode SumCostDBYLNode = doc.SelectSingleNode("result/output/yka437");//大病医疗统筹累计");
            if (SumCostDBYLNode != null)
            {
                personInfo.SumCostDBYL = SumCostDBYLNode.InnerText;
            }
            else
            {
                personInfo.SumCostDBYL = "0";
            }

            XmlNode InTimesNode = doc.SelectSingleNode("result/output/akc200");//本年度住院次数");
            if (InTimesNode != null)
            {
                personInfo.InTimes = InTimesNode.InnerText;
            }
            else
            {
                personInfo.InTimes = "";
            }

            XmlNode InStateNode = doc.SelectSingleNode("result/output/ykc023");//住院状态");
            if (InStateNode != null)
            {
                personInfo.InState = InStateNode.InnerText;
            }
            else
            {
                personInfo.InState = "";
            }

            XmlNode ReturnsFlagNode = doc.SelectSingleNode("result/output/ykc667");//二次返院审批标志");
            if (ReturnsFlagNode != null)
            {
                personInfo.ReturnsFlag = ReturnsFlagNode.InnerText;
            }
            else
            {
                personInfo.ReturnsFlag = "";
            }


            XmlNode YDJYBAHNode = doc.SelectSingleNode("result/output/yzz014");//异地就医备案号");
            if (YDJYBAHNode != null)
            {
                personInfo.YDJYBAH = YDJYBAHNode.InnerText;
            }
            else
            {
                personInfo.YDJYBAH = "";
            }

            XmlNode ChangeOutFlagNode = doc.SelectSingleNode("result/output/ake132");//转院标志");
            if (ChangeOutFlagNode != null)
            {
                personInfo.ChangeOutFlag = ChangeOutFlagNode.InnerText;
            }
            else
            {
                personInfo.ChangeOutFlag = "";
            }

            XmlNode ChangeOutClinicNoNode = doc.SelectSingleNode("result/output/ykc669");//转出就诊登记号");
            if (ChangeOutClinicNoNode != null)
            {
                personInfo.ChangeOutClinicNo = ChangeOutClinicNoNode.InnerText;
            }
            else
            {
                personInfo.ChangeOutClinicNo = "";
            }

            XmlNode ChangeOutHosNameNode = doc.SelectSingleNode("result/output/ykc678");//转出医院名称");
            if (ChangeOutHosNameNode != null)
            {
                personInfo.ChangeOutHosName = ChangeOutHosNameNode.InnerText;
            }
            else
            {
                personInfo.ChangeOutHosName = "";
            }


            XmlNode ChangeInHosNameNode = doc.SelectSingleNode("result/output/ykc670");//转入医院名称");
            if (ChangeInHosNameNode != null)
            {
                personInfo.ChangeInHosName = ChangeInHosNameNode.InnerText;
            }
            else
            {
                personInfo.ChangeInHosName = "";
            }

            XmlNode ChangeTypeNode = doc.SelectSingleNode("result/output/ykc682");//转院类别");
            if (ChangeTypeNode != null)
            {
                personInfo.ChangeType = ChangeTypeNode.InnerText;
            }
            else
            {
                personInfo.ChangeType = "";
            }

            XmlNode ChangeDateNode = doc.SelectSingleNode("result/output/ake014");//转诊日期");
            if (ChangeDateNode != null)
            {
                personInfo.ChangeDate = ChangeDateNode.InnerText;
            }
            else
            {
                personInfo.ChangeDate = "0001-01-01";
            }

            XmlNode ChangeDiagnNode = doc.SelectSingleNode("result/output/ykc672");//转诊诊断");
            if (ChangeDiagnNode != null)
            {
                personInfo.ChangeDiagn = ChangeDiagnNode.InnerText;
            }
            else
            {
                personInfo.ChangeDiagn = "";
            }

            XmlNode ChangePassFlagNode = doc.SelectSingleNode("result/output/ykc673");//转诊审批标志");
            if (ChangePassFlagNode != null)
            {
                personInfo.ChangePassFlag = ChangePassFlagNode.InnerText;
            }
            else
            {
                personInfo.ChangePassFlag = "";
            }

            XmlNode ChangeReasonNode = doc.SelectSingleNode("result/output/ykc674");//转诊原因");
            if (ChangeReasonNode != null)
            {
                personInfo.ChangeReason = ChangeReasonNode.InnerText;
            }
            else
            {
                personInfo.ChangeReason = "";
            }


            //4.0修订

            XmlNode BNDYLJZLJJENode = doc.SelectSingleNode("result/output/ykc753");//本年度医疗救助累计金额
            if (BNDYLJZLJJENode != null)
            {
                personInfo.BNDYLJZLJJE = FS.FrameWork.Function.NConvert.ToDecimal(BNDYLJZLJJENode.InnerText);
            }
            else
            {
                personInfo.BNDYLJZLJJE = 0;
            }


            XmlNode BCYLJZZFXENode = doc.SelectSingleNode("result/output/ykc754");//本次医疗救助支付限额

            if (BCYLJZZFXENode != null)
            {
                personInfo.BCYLJZZFXE = FS.FrameWork.Function.NConvert.ToDecimal(BCYLJZZFXENode.InnerText);
            }
            else
            {
                personInfo.BCYLJZZFXE = 0;
            }

            #region 作废
            #endregion 

            this.SetYdInPatientAccreditationInfo(patientInfo, personInfo);
        }

        /// <summary>
        /// 【异地】2.2.1.3 费用明细处理 获取XML
        /// </summary>
        /// <param name="personInfo"></param>
        /// <param name="al"></param>
        /// <returns></returns>
        public string[] SetYDInpatientFeeDetialXML3(FoShanYDSI.Objects.SIPersonInfo personInfo, ArrayList al)
        {
            this.GetHosCodeAndRegionCode(ref personInfo);
            int maxTime = (int)Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(al.Count / 100));
            string[] arr = new string[maxTime + 1];
            int num = 0;
            int Mtemp = 0;
            for (int i = 0; i <= maxTime; i++)
            {
                XmlDocument xml = new XmlDocument();
                xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
                XmlElement rootroot = xml.CreateElement("xml");
                xml.AppendChild(rootroot);
                XmlElement root = xml.CreateElement("input");
                rootroot.AppendChild(root);

                XmlNode InsuredAreaCode = xml.CreateElement("yab003");//参保地统筹区编码");
                InsuredAreaCode.InnerText = personInfo.InsuredAreaCode;
                root.AppendChild(InsuredAreaCode);

                //XmlNode InsuredCenterAreaCode = xml.CreateElement("xxxxxx");//参保地分中心编码");
                //InsuredCenterAreaCode.InnerText = personInfo.InsuredCenterAreaCode;
                //root.AppendChild(InsuredCenterAreaCode);

                XmlNode HospitalizeAreaCode = xml.CreateElement("yab300");//就医地统筹区编码");
                HospitalizeAreaCode.InnerText = personInfo.HospitalizeAreaCode;
                root.AppendChild(HospitalizeAreaCode);

                XmlNode HospitalizeCenterAreaCode = xml.CreateElement("yab600");//就医地分中心编号");
                HospitalizeCenterAreaCode.InnerText = personInfo.HospitalizeCenterAreaCode;
                root.AppendChild(HospitalizeCenterAreaCode);

                XmlNode HospitalCode = xml.CreateElement("akb020");//医院编码");
                HospitalCode.InnerText = personInfo.HospitalCode;
                root.AppendChild(HospitalCode);

                XmlNode SIcode = xml.CreateElement("aac001");//医保编号");
                SIcode.InnerText = personInfo.SIcode;
                root.AppendChild(SIcode);

                //XmlNode zjlx = xml.CreateElement("xxxxxx");//证件类型");
                //zjlx.InnerText = (string.IsNullOrEmpty(personInfo.ZJLX) ? "01" : personInfo.ZJLX);
                //root.AppendChild(zjlx);

                //XmlNode IdenNo = xml.CreateElement("xxxxxx");//证件号码");
                //IdenNo.InnerText = personInfo.IdenNo;
                //root.AppendChild(IdenNo);

                XmlNode ClinicNo = xml.CreateElement("akc190");//就诊登记号");
                ClinicNo.InnerText = personInfo.ClinicNo;
                root.AppendChild(ClinicNo);

                XmlElement row1 = xml.CreateElement("dataset");//费用明细列表");
               

                for (int M = 0; M < 100; M++)
                //foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeDetail in al)
                {
                    #region 费用信息明细
                    if (!(num < al.Count))
                    {
                        break;
                    }
                    //XmlElement row1 = xml.CreateElement("row");//费用明细列表");

                    FS.HISFC.Models.Fee.Inpatient.FeeInfo feeDetail = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                    feeDetail = al[num] as FS.HISFC.Models.Fee.Inpatient.FeeInfo;
                    XmlElement row2 = xml.CreateElement("row");//费用明细");

                    XmlNode recipe_no = xml.CreateElement("akc220");//处方号");
                    recipe_no.InnerText = System.DateTime.Now.ToString("yyyyMMddHHmmss") + (M + Mtemp).ToString();//feeDetail.RecipeNO;
                    row2.AppendChild(recipe_no);

                    XmlNode balance_no = xml.CreateElement("ykc610");//明细序号");
                    balance_no.InnerText = System.DateTime.Now.ToString("MMddHHmmss") + (M + Mtemp).ToString();// feeDetail.RecipeNO;
                    row2.AppendChild(balance_no);

                    XmlNode class_code = xml.CreateElement("aka111");//大类代码");
                    class_code.InnerText = feeDetail.Item.MinFee.ID;//"XV06DC";//
                    row2.AppendChild(class_code);

                    XmlNode class_name = xml.CreateElement("aka112");//大类名称");
                    class_name.InnerText = feeDetail.Item.MinFee.Name;//"碳水化合物";//
                    row2.AppendChild(class_name);

                    XmlNode SI_item_code = xml.CreateElement("akc222");//就医地项目代码");
                    SI_item_code.InnerText = feeDetail.Compare.CenterItem.ID;
                    row2.AppendChild(SI_item_code);

                    XmlNode SI_item_name = xml.CreateElement("akc223");//就医地项目名称");
                    SI_item_name.InnerText = feeDetail.Compare.CenterItem.Name;
                    row2.AppendChild(SI_item_name);

                    XmlNode FDA_code = xml.CreateElement("akc224");//药监局药品编码");
                    FDA_code.InnerText = "";//feeDetail.Compare.FdaDrguCode;//后面处理 佛山
                    row2.AppendChild(FDA_code);

                    XmlNode limit_flag = xml.CreateElement("akc229");//限制用药标记");
                    limit_flag.InnerText = "0";
                    row2.AppendChild(limit_flag);

                    XmlNode item_reg_name = xml.CreateElement("akc230");//医用材料的注册证产品名称");
                    item_reg_name.InnerText = "";
                    row2.AppendChild(item_reg_name);

                    XmlNode item_reg_code = xml.CreateElement("akc231");//医用材料的食药监注册号");
                    item_reg_code.InnerText = "";
                    row2.AppendChild(item_reg_code);

                    XmlNode item_code = xml.CreateElement("akc222y");//就医地医院项目编码");
                    item_code.InnerText = feeDetail.Item.ID; //feeDetail.Compare.CenterItem.ID; //
                    row2.AppendChild(item_code);

                    XmlNode item_name = xml.CreateElement("akc223y");//就医地医院项目名称");
                    item_name.InnerText = feeDetail.Item.Name; //feeDetail.Compare.CenterItem.Name; //
                    row2.AppendChild(item_name);

                    XmlNode qty = xml.CreateElement("akc226");//数量");
                    qty.InnerText = feeDetail.Item.Qty.ToString();
                    row2.AppendChild(qty);

                    XmlNode price = xml.CreateElement("akc225");//单价");
                    price.InnerText = feeDetail.Item.Price.ToString();
                    row2.AppendChild(price);

                    XmlNode tot_cost = xml.CreateElement("akc227");//费用总额");
                    tot_cost.InnerText = feeDetail.FT.TotCost.ToString();
                    row2.AppendChild(tot_cost);

                    XmlNode origin = xml.CreateElement("ykc611");//产地");
                    origin.InnerText = "";
                    row2.AppendChild(origin);

                    XmlNode pecial_flag = xml.CreateElement("ykc615");//特项标志");
                    pecial_flag.InnerText = "0";
                    row2.AppendChild(pecial_flag);

                    XmlNode specs = xml.CreateElement("aka074");//规格");
                    specs.InnerText = "";
                    row2.AppendChild(specs);

                    XmlNode unit = xml.CreateElement("aka067");//单位");
                    unit.InnerText = "";
                    row2.AppendChild(unit);

                    XmlNode drug_type = xml.CreateElement("aka070");//剂型");
                    drug_type.InnerText = "";
                    row2.AppendChild(drug_type);


                    XmlNode recipe_doc_name = xml.CreateElement("ykc613");//处方医生姓名");
                    recipe_doc_name.InnerText = feeDetail.RecipeOper.Memo;//"52010319870919482X"; //feeDetail.RecipeOper.Name;
                    row2.AppendChild(recipe_doc_name);

                    XmlNode dept_name = xml.CreateElement("ykc011");//科室");
                    dept_name.InnerText = "";
                    row2.AppendChild(dept_name);

                    XmlNode fee_date = xml.CreateElement("akc221");//收费时间");
                    fee_date.InnerText = feeDetail.ExecOrder.ExecOper.OperTime.ToString("yyyyMMdd");
                    row2.AppendChild(fee_date);

                    XmlNode operater = xml.CreateElement("aae011");//经办人");
                    operater.InnerText = feeDetail.RecipeOper.Name;
                    row2.AppendChild(operater);

                    XmlNode oper_date = xml.CreateElement("aae036");//经办时间");
                    oper_date.InnerText = feeDetail.ExecOrder.ExecOper.OperTime.ToString("yyyyMMdd");
                    row2.AppendChild(oper_date);

                    row1.AppendChild(row2);
                    num++;
                    #endregion
                }
                Mtemp = Mtemp + 100;
                root.AppendChild(row1);
                xml.AppendChild(root);
                arr[i] = xml.InnerXml.ToString();
            }

            return arr;
        }

        /// <summary>
        /// 【异地】28 佛山专用 ，费用明细上传前调用，OUTXML直接作为[0301]入参 获取XML
        /// </summary>
        /// <param name="personInfo"></param>
        /// <param name="al"></param>
        /// <returns></returns>
        public string[] SetYDInpatientFeeDetialXML4(FoShanYDSI.Objects.SIPersonInfo personInfo, ArrayList al)
        {
            //工伤的入院时间格式是yyyyMMddHHmm
            //其他的入院时间格式是yyyyMMddHH
            string inDateStyle = string.Empty;
            if (personInfo.GSFlag.Equals("1"))
            {
                inDateStyle = "yyyyMMddHHmm";
            }
            else
            {
                inDateStyle = "yyyyMMdd";
            }

            decimal totCost = 0;
            int totRow = 0;
            this.GetHosCodeAndRegionCode(ref personInfo);
            int maxTime = (int)Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(al.Count / 100));
            string[] arr = null;//new string[maxTime + 1];
            if ((al.Count % 100) > 0)
            {
                 arr = new string[maxTime + 1];
            }
            else
            {
                 arr = new string[maxTime];
                 maxTime = maxTime - 1;
            }
            int num = 0;
            int Mtemp = 0;
            for (int i = 0; i <= maxTime; i++)
            {

            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            #region 患者参数

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//就诊登记号
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(personInfo.MCardNo) ? "-" : personInfo.MCardNo);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = personInfo.IdenNo;
            root.AppendChild(aac044);

            #endregion

            XmlElement row1 = xml.CreateElement("detail");//费用明细列表");


            for (int M = 0; M < 100; M++)
            //foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeDetail in al)
            {
                #region 费用信息明细
                if (!(num < al.Count))
                {
                    break;
                }
                //XmlElement row1 = xml.CreateElement("row");//费用明细列表");

                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeDetail = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                feeDetail = al[num] as FS.HISFC.Models.Fee.Inpatient.FeeInfo;
                XmlElement row2 = xml.CreateElement("row");//费用明细");
                totRow++;
                XmlNode recipe_no = xml.CreateElement("akc220");//处方号");
                recipe_no.InnerText = "";//System.DateTime.Now.ToString("yyyyMMddHHmmss") + (M + Mtemp).ToString();//feeDetail.RecipeNO;
                row2.AppendChild(recipe_no);

                XmlNode balance_no = xml.CreateElement("ykc610");//明细序号");
                balance_no.InnerText = System.DateTime.Now.ToString("yyyyMMddHHmmss") + (M + Mtemp).ToString();// feeDetail.RecipeNO;
                row2.AppendChild(balance_no);

                XmlNode class_code = xml.CreateElement("yka111");//大类代码");
                class_code.InnerText = feeDetail.Item.MinFee.ID;//"XV06DC";//
                row2.AppendChild(class_code);

                XmlNode class_name = xml.CreateElement("yka112");//大类名称");
                class_name.InnerText = feeDetail.Item.MinFee.Name;//"碳水化合物";//
                row2.AppendChild(class_name);

                XmlNode SI_item_code = xml.CreateElement("ake001");//就医地项目代码");//佛山为：医院内部HIS系统的项目编号
                SI_item_code.InnerText = feeDetail.Item.ID;//feeDetail.Compare.CenterItem.ID;
                row2.AppendChild(SI_item_code);

                XmlNode SI_item_name = xml.CreateElement("ake002");//就医地项目名称");//佛山为：医院内部HIS系统的项目名称。包括药品、医用材料、诊疗项目名称。
                SI_item_name.InnerText = feeDetail.Item.Name;//feeDetail.Compare.CenterItem.Name;
                row2.AppendChild(SI_item_name);

                XmlNode FDA_code = xml.CreateElement("ake114");//药监局药品编码");
                FDA_code.InnerText = "";//feeDetail.Compare.FdaDrguCode;//后面处理 佛山
                row2.AppendChild(FDA_code);

                XmlNode limit_flag = xml.CreateElement("aka185");//限制用药标记");
                limit_flag.InnerText = "0";
                row2.AppendChild(limit_flag);

                XmlNode item_reg_name = xml.CreateElement("yke230");//医用材料的注册证产品名称");
                item_reg_name.InnerText = "";
                row2.AppendChild(item_reg_name);

                XmlNode item_reg_code = xml.CreateElement("yke231");//医用材料的食药监注册号");
                item_reg_code.InnerText = "";
                row2.AppendChild(item_reg_code);

                XmlNode item_code = xml.CreateElement("ake005");//就医地医院项目编码");
                item_code.InnerText = feeDetail.Item.ID; //feeDetail.Compare.CenterItem.ID; //
                row2.AppendChild(item_code);

                XmlNode item_name = xml.CreateElement("ake006");//就医地医院项目名称");
                item_name.InnerText = feeDetail.Item.Name; //feeDetail.Compare.CenterItem.Name; //
                row2.AppendChild(item_name);

                XmlNode qty = xml.CreateElement("akc226");//数量");
                qty.InnerText = feeDetail.Item.Qty.ToString();
                row2.AppendChild(qty);

                XmlNode price = xml.CreateElement("akc225");//单价");
                price.InnerText = feeDetail.Item.Price.ToString();
                row2.AppendChild(price);

                XmlNode tot_cost = xml.CreateElement("akc264");//费用总额");
                tot_cost.InnerText = feeDetail.FT.TotCost.ToString();
                row2.AppendChild(tot_cost);
                totCost += feeDetail.FT.TotCost;

                XmlNode origin = xml.CreateElement("ykc611");//产地");
                origin.InnerText = "";
                row2.AppendChild(origin);

                XmlNode pecial_flag = xml.CreateElement("ykc615");//特项标志");
                pecial_flag.InnerText = "0";
                row2.AppendChild(pecial_flag);

                XmlNode specs = xml.CreateElement("aka074");//规格");
                specs.InnerText = "";
                row2.AppendChild(specs);

                XmlNode unit = xml.CreateElement("aka067");//单位");
                unit.InnerText = "";
                row2.AppendChild(unit);

                XmlNode drug_type = xml.CreateElement("aka070");//剂型");
                drug_type.InnerText = "";
                row2.AppendChild(drug_type);

                XmlNode akc056 = xml.CreateElement("akc056");//医师执业证编码");
                akc056.InnerText = "";//"52010319870919482X"; //feeDetail.RecipeOper.Name;
                row2.AppendChild(akc056);


                XmlNode recipe_doc_name = xml.CreateElement("akc273");//处方医生姓名");
                recipe_doc_name.InnerText = feeDetail.RecipeOper.Memo;//"52010319870919482X"; //feeDetail.RecipeOper.Name;
                row2.AppendChild(recipe_doc_name);

                XmlNode dept_name = xml.CreateElement("aae386");//科室");
                dept_name.InnerText = "";
                row2.AppendChild(dept_name);

                XmlNode fee_date = xml.CreateElement("akc221");//收费时间");
                fee_date.InnerText = feeDetail.ExecOrder.ExecOper.OperTime.ToString("yyyyMMdd");
                row2.AppendChild(fee_date);

                XmlNode operater = xml.CreateElement("aae011");//经办人");
                operater.InnerText = feeDetail.RecipeOper.Name;
                row2.AppendChild(operater);

                XmlNode oper_date = xml.CreateElement("aae036");//经办时间");
                oper_date.InnerText = feeDetail.ExecOrder.ExecOper.OperTime.ToString("yyyyMMdd");
                row2.AppendChild(oper_date);

                row1.AppendChild(row2);
                num++;
                #endregion
            }
            Mtemp = Mtemp + 100;
            root.AppendChild(row1);
            xml.AppendChild(root);
            arr[i] = xml.InnerXml.ToString();
            }
            //MessageBox.Show("明细总金额：" + totCost.ToString() + "   总行数：" + totRow.ToString());
            return arr;

        }

        /// <summary>
        /// 【异地】2.2.1.3 费用明细处理 获取返回实体
        /// 先放着，不定要不要存明细返回记录
        /// </summary>
        /// <param name="personInfo"></param>
        /// <param name="al"></param>
        /// <returns></returns>
        public void GetYDInpatientFeeDetailResult()
        { }

        /// <summary>
        /// 【异地】2.2.1.5 费用结算 获取XML
        /// </summary>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetYDInpatientFeeBalanceXML(FoShanYDSI.Objects.SIPersonInfo personInfo, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            this.GetHosCodeAndRegionCode(ref personInfo);

            #region 费用结算参数
            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//就诊登记号
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(personInfo.MCardNo) ? "-" : personInfo.MCardNo);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = personInfo.IdenNo;
            root.AppendChild(aac044);

            //结算类型
            XmlElement ykc675 = xml.CreateElement("ykc675");//结算类型
            ykc675.InnerText = "1";
            root.AppendChild(ykc675);

            XmlNode tot_cost = xml.CreateElement("akc264");//费用总额");
            tot_cost.InnerText = patient.SIMainInfo.TotCost.ToString();
            root.AppendChild(tot_cost);

            XmlNode operater = xml.CreateElement("aae011");//经办人");
            operater.InnerText = FS.FrameWork.Management.Connection.Operator.Name;
            root.AppendChild(operater);

            XmlNode PatientNO = xml.CreateElement("yzz021");//医院结算业务序列号");//佛山：医院发票号
            PatientNO.InnerText = patient.ID;//后面需要整合
            root.AppendChild(PatientNO);

            XmlNode oper_date = xml.CreateElement("aae036");//经办时间");
            oper_date.InnerText = DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
            root.AppendChild(oper_date);


            XmlNode balance_date = xml.CreateElement("akc194");//结算时间");
            balance_date.InnerText = DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
            root.AppendChild(balance_date);

            XmlNode Memo = xml.CreateElement("aae013");//备注");
            Memo.InnerText = personInfo.Memo;
            root.AppendChild(Memo);

            //XmlNode IdenNo = xml.CreateElement("xxxxxx");//证件号码");
            //IdenNo.InnerText = personInfo.IdenNo;
            //root.AppendChild(IdenNo);

            //XmlNode ZJLX = xml.CreateElement("xxxxxx");//证件类型");
            //ZJLX.InnerText = (string.IsNullOrEmpty(personInfo.ZJLX) ? "01" : personInfo.ZJLX);
            //root.AppendChild(ZJLX);

            #endregion

            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 【异地】2.2.1.5 费用结算[普通] 获取返回实体
        /// </summary>
        /// <param name="retXML"></param>
        /// <param name="patientInfo"></param>

        public void GetYdInPatientBalanceResult(string retXML, FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(retXML);
            }
            catch (Exception e)
            {
                return;
            }

            XmlNode transid = doc.SelectSingleNode("result/transid");
            if (transid != null)
            {
                personInfo.balanceTransID = transid.InnerText;
            }
            else
            {
                personInfo.balanceTransID = "";
            }

            XmlNode JSYWH = doc.SelectSingleNode("result/output/ykc618");
            if (JSYWH != null)
            {
                personInfo.JSYWH = JSYWH.InnerText;
            }
            else
            {
                personInfo.JSYWH = "";
            }


            XmlNode ZYFYJSFS = doc.SelectSingleNode("result/output/aka105");
            if (ZYFYJSFS != null)
            {
                personInfo.ZYFYJSFS = ZYFYJSFS.InnerText;
            }
            else
            {
                personInfo.ZYFYJSFS = "";
            }

            XmlNode balance_type = doc.SelectSingleNode("result/output/ykc675");
            if (balance_type != null)
            {
                personInfo.balance_type = balance_type.InnerText;
            }
            else
            {
                personInfo.balance_type = "";
            }

            XmlNode tot_cost = doc.SelectSingleNode("result/output/akc264");
            if (tot_cost != null)
            {
                personInfo.tot_cost = FS.FrameWork.Function.NConvert.ToDecimal(tot_cost.InnerText);
            }
            else
            {
                personInfo.tot_cost = 0;
            }


            XmlNode own_cost_part = doc.SelectSingleNode("result/output/akc253");
            if (own_cost_part != null)
            {
                personInfo.own_cost_part = FS.FrameWork.Function.NConvert.ToDecimal(own_cost_part.InnerText);
            }
            else
            {
                personInfo.own_cost_part = 0;
            }

            XmlNode first_pay_cost = doc.SelectSingleNode("result/output/akc254");
            if (first_pay_cost != null)
            {
                personInfo.pay_cost_part = FS.FrameWork.Function.NConvert.ToDecimal(first_pay_cost.InnerText);
            }
            else
            {
                personInfo.pay_cost_part = 0;
            }

            XmlNode pub_cost = doc.SelectSingleNode("result/output/yka319");
            if (pub_cost != null)
            {
                personInfo.pub_cost = FS.FrameWork.Function.NConvert.ToDecimal(pub_cost.InnerText);
            }
            else
            {
                personInfo.pub_cost = 0;
            }

            XmlNode GRZF_cost = doc.SelectSingleNode("result/output/ykc624");
            if (GRZF_cost != null)
            {
                personInfo.GRZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(GRZF_cost.InnerText);
            }
            else
            {
                personInfo.GRZF_cost = 0;
            }


            XmlNode limit_cost = doc.SelectSingleNode("result/output/aka151");
            if (limit_cost != null)
            {
                personInfo.limit_cost = FS.FrameWork.Function.NConvert.ToDecimal(limit_cost.InnerText);
            }
            else
            {
                personInfo.limit_cost = 0;
            }


            XmlNode GZZF_cost = doc.SelectSingleNode("result/output/akb066");
            if (GZZF_cost != null)
            {
                personInfo.GZZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(GZZF_cost.InnerText);
            }
            else
            {
                personInfo.GZZF_cost = 0;
            }

            XmlNode CBXXEZF_cost = doc.SelectSingleNode("result/output/ykc631");
            if (CBXXEZF_cost != null)
            {
                personInfo.CBXXEZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(CBXXEZF_cost.InnerText);
            }
            else
            {
                personInfo.CBXXEZF_cost = 0;
            }

            XmlNode YBTCZF_cost = doc.SelectSingleNode("result/output/akb068");
            if (YBTCZF_cost != null)
            {
                personInfo.YBTCZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(YBTCZF_cost.InnerText);
            }
            else
            {
                personInfo.YBTCZF_cost = 0;
            }

            XmlNode TCJJZF_cost = doc.SelectSingleNode("result/output/ake039");
            if (TCJJZF_cost != null)
            {
                personInfo.TCJJZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(TCJJZF_cost.InnerText);
            }
            else
            {
                personInfo.TCJJZF_cost = 0;
            }

            XmlNode JBYLTCZF_cost = doc.SelectSingleNode("result/output/ykc627");
            if (JBYLTCZF_cost != null)
            {
                personInfo.JBYLTCZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(JBYLTCZF_cost.InnerText);
            }
            else
            {
                personInfo.JBYLTCZF_cost = 0;
            }

            XmlNode DBYLTCZF_cost = doc.SelectSingleNode("result/output/ykc630");
            if (DBYLTCZF_cost != null)
            {
                personInfo.DBYLTCZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(DBYLTCZF_cost.InnerText);
            }
            else
            {
                personInfo.DBYLTCZF_cost = 0;
            }

            XmlNode DBYLTCZIFU_cost = doc.SelectSingleNode("result/output/ykc629");
            if (DBYLTCZIFU_cost != null)
            {
                personInfo.DBYLTCZIFU_cost = FS.FrameWork.Function.NConvert.ToDecimal(DBYLTCZIFU_cost.InnerText);
            }
            else
            {
                personInfo.DBYLTCZIFU_cost = 0;
            }

            XmlNode GWYBZ_cost = doc.SelectSingleNode("result/output/ake035");
            if (GWYBZ_cost != null)
            {
                personInfo.GWYBZ_cost = FS.FrameWork.Function.NConvert.ToDecimal(GWYBZ_cost.InnerText);
            }
            else
            {
                personInfo.GWYBZ_cost = 0;
            }

            XmlNode GWYDB_cost = doc.SelectSingleNode("result/output/ykc635");
            if (GWYDB_cost != null)
            {
                personInfo.GWYDB_cost = FS.FrameWork.Function.NConvert.ToDecimal(GWYDB_cost.InnerText);
            }
            else
            {
                personInfo.GWYDB_cost = 0;
            }

            XmlNode GWYCXBZ_cost = doc.SelectSingleNode("result/output/ykc636");
            if (GWYCXBZ_cost != null)
            {
                personInfo.GWYCXBZ_cost = FS.FrameWork.Function.NConvert.ToDecimal(GWYCXBZ_cost.InnerText);
            }
            else
            {
                personInfo.GWYCXBZ_cost = 0;
            }

            XmlNode QTBZZF_cost = doc.SelectSingleNode("result/output/ykc637");
            if (QTBZZF_cost != null)
            {
                personInfo.QTBZZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(QTBZZF_cost.InnerText);
            }
            else
            {
                personInfo.QTBZZF_cost = 0;
            }

            XmlNode QTZF = doc.SelectSingleNode("result/output/ykc639");//其它支付");
            if (QTZF != null)
            {
                personInfo.QTZF = FS.FrameWork.Function.NConvert.ToDecimal(QTZF.InnerText);
            }
            else
            {
                personInfo.QTZF = 0;
            }

            XmlNode balance_bed_day = doc.SelectSingleNode("result/output/akb063");
            if (balance_bed_day != null)
            {
                personInfo.balance_bed_day = FS.FrameWork.Function.NConvert.ToDecimal(balance_bed_day.InnerText);
            }
            else
            {
                personInfo.balance_bed_day = 0;
            }

            XmlNode Memo = doc.SelectSingleNode("result/output/ykc666");
            if (Memo != null)
            {
                personInfo.Memo = Memo.InnerText;
            }
            else
            {
                personInfo.Memo = "";
            }

            //本年度住院次数
            XmlNode InTimes = doc.SelectSingleNode("result/output/akc200");
            if (InTimes != null)
            {
                personInfo.InTimes = InTimes.InnerText;
            }
            else
            {
                personInfo.SumCostJBYL = 0;
            }


            XmlNode SumCostJBYL = doc.SelectSingleNode("result/output/yka430");
            if (SumCostJBYL != null)
            {
                personInfo.SumCostJBYL = FS.FrameWork.Function.NConvert.ToDecimal(SumCostJBYL.InnerText);
            }
            else
            {
                personInfo.SumCostJBYL = 0;
            }

            XmlNode BCYLLJYZF_cost = doc.SelectSingleNode("result/output/yka431");//补充医疗累计已支付");
            if (BCYLLJYZF_cost != null)
            {
                personInfo.BCYLLJYZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(BCYLLJYZF_cost.InnerText);
            }
            else
            {
                personInfo.BCYLLJYZF_cost = 0;
            }


            XmlNode GWYBCLJYZF_cost = doc.SelectSingleNode("result/output/yka432");//公务员补助累计已支付");
            if (GWYBCLJYZF_cost != null)
            {
                personInfo.GWYBCLJYZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(GWYBCLJYZF_cost.InnerText);
            }
            else
            {
                personInfo.GWYBCLJYZF_cost = 0;
            }

            XmlNode DBBXYLTCLJ = doc.SelectSingleNode("result/output/yka433");//重大疾病或大病保险累计支付");
            if (DBBXYLTCLJ != null)
            {
                personInfo.DBBXYLTCLJ = FS.FrameWork.Function.NConvert.ToDecimal(DBBXYLTCLJ.InnerText);
            }
            else
            {
                personInfo.DBBXYLTCLJ = 0;
            }

            XmlNode qtlj = doc.SelectSingleNode("result/output/yka434");//其他累计支付");
            if (qtlj != null)
            {
                personInfo.qtlj = FS.FrameWork.Function.NConvert.ToDecimal(qtlj.InnerText);
            }
            else
            {
                personInfo.qtlj = 0;
            }

            XmlNode xzlx = doc.SelectSingleNode("result/output/aae140");//险种类型");
            if (xzlx != null)
            {
                personInfo.xzlx = xzlx.InnerText;
            }
            else
            {
                personInfo.xzlx = "";
            }

            //XmlNode GFDGRZF_cost = doc.SelectSingleNode("result/output/共付段个人支付");
            //if (GFDGRZF_cost != null)
            //{
            //    personInfo.GFDGRZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(GFDGRZF_cost.InnerText);
            //}
            //else
            //{
            //    personInfo.GFDGRZF_cost = 0;
            //}

            #region 作废
            XmlNode GRZIFUJE = doc.SelectSingleNode("result/output/yzz139");//个人自负金额");
            if (GRZIFUJE != null)
            {
                personInfo.GRZIFUJE = FS.FrameWork.Function.NConvert.ToDecimal(GRZIFUJE.InnerText);
            }
            else
            {
                personInfo.YLBZBF_cost = 0;
            }

            XmlNode GWYBZZF_cost = doc.SelectSingleNode("result/output/ykc640");//公务员补助支付费用");
            if (GWYBZZF_cost != null)
            {
                personInfo.GWYBZZF_cost = FS.FrameWork.Function.NConvert.ToDecimal(GWYBZZF_cost.InnerText);
            }
            else
            {
                personInfo.GWYBZZF_cost = 0;
            }


            //4.0修订
            XmlNode akb068 = doc.SelectSingleNode("result/output/akb068");
            if (akb068 != null)
            {
                personInfo.akb068 = FS.FrameWork.Function.NConvert.ToDecimal(akb068.InnerText);
            }
            else
            {
                personInfo.akb068 = 0;
            }

            XmlNode ykc751 = doc.SelectSingleNode("result/output/ykc751");
            if (ykc751 != null)
            {
                personInfo.ykc751 = ykc751.InnerText.ToString();
            }
            else
            {
                personInfo.ykc751 = "";
            }

            XmlNode ykc641 = doc.SelectSingleNode("result/output/ykc641");
            if (ykc641 != null)
            {
                personInfo.ykc641 = FS.FrameWork.Function.NConvert.ToDecimal(ykc641.InnerText);
            }
            else
            {
                personInfo.ykc641 = 0;
            }

            XmlNode ykc642 = doc.SelectSingleNode("result/output/ykc642");
            if (ykc642 != null)
            {
                personInfo.ykc642 = FS.FrameWork.Function.NConvert.ToDecimal(ykc642.InnerText);
            }
            else
            {
                personInfo.ykc642 = 0;
            }

            XmlNode ykc752 = doc.SelectSingleNode("result/output/ykc752");
            if (ykc752 != null)
            {
                personInfo.ykc752 = FS.FrameWork.Function.NConvert.ToDecimal(ykc752.InnerText);
            }
            else
            {
                personInfo.ykc752 = 0;
            }

            XmlNode ykc753 = doc.SelectSingleNode("result/output/ykc753");
            if (ykc753 != null)
            {
                personInfo.ykc753 = FS.FrameWork.Function.NConvert.ToDecimal(ykc753.InnerText);
            }
            else
            {
                personInfo.ykc753 = 0;
            }

            #endregion

            this.SetOutPatientBalanceInfo(patientInfo, personInfo);
        }

        private void SetOutPatientBalanceInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            //设置基础费用信息以供校验
            patientInfo.SIMainInfo.BalanceDate = DateTime.Now;
            patientInfo.SIMainInfo.TotCost = personInfo.tot_cost;
            //patientInfo.SIMainInfo.PubCost = personInfo.pub_cost;
            //patientInfo.SIMainInfo.OwnCost = personInfo.own_cost_part + personInfo.pay_cost_part;
            //patientInfo.SIMainInfo.PubCost=personInfo.tot_cost-(personInfo.own_cost_part + personInfo.pay_cost_part);

            patientInfo.SIMainInfo.OwnCost = personInfo.GRZF_cost;
            //patientInfo.SIMainInfo.PubCost = personInfo.TCJJZF_cost + personInfo.DBBXYLTCLJ + personInfo.BCYLLJYZF_cost + personInfo.DBYLTCZF_cost
            //                                + personInfo.GWYBZ_cost + personInfo.GWYBZZF_cost + personInfo.GWYDB_cost + personInfo.QTZF ;
            //patientInfo.SIMainInfo.PubCost = personInfo.TCJJZF_cost + personInfo.DBYLTCZF_cost + personInfo.GWYBZ_cost + personInfo.GWYBZZF_cost + personInfo.GWYDB_cost + personInfo.QTZF + personInfo.QTBZZF_cost;//2016.4.29 lu.jsh代改
            patientInfo.SIMainInfo.PubCost = personInfo.YBTCZF_cost;//personInfo.TCJJZF_cost + personInfo.DBYLTCZF_cost + personInfo.GWYBZZF_cost + personInfo.GWYDB_cost + personInfo.QTZF + personInfo.QTBZZF_cost;
        }

        /// <summary>
        /// 【异地】2.2.1.6 更新就诊登记信息
        /// 暂时不用，先放着
        /// </summary>
        public void SetUpdateRegInfoXML()
        { }

        /// <summary>
        /// 【异地】2.2.1.9 出院登记回退 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetCancelYdInPatientRegXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            //工伤的入院时间格式是yyyyMMddHHmm
            //其他的入院时间格式是yyyyMMddHH
            string inDateStyle = string.Empty;
            if (personInfo.GSFlag.Equals("1"))
            {
                inDateStyle = "yyyyMMddHHmm";
            }
            else
            {
                inDateStyle = "yyyyMMdd";
            }


            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            #region 患者参数

            //参保地统筹区编码
            XmlElement otransid = xml.CreateElement("otransid");//参保地统筹区编码");
            otransid.InnerText = personInfo.regTransID;
            root.AppendChild(otransid);


            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = patientInfo.IDCard;
            root.AppendChild(aac044);

            //入院经办人
            XmlElement node7 = xml.CreateElement("aae011");//经办人");
            node7.InnerText = FS.FrameWork.Management.Connection.Operator.Name;
            root.AppendChild(node7);

            //经办时间,不能为空
            XmlElement node8 = xml.CreateElement("aae036");//经办时间");
            node8.InnerText = System.DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
            root.AppendChild(node8);

            #endregion

            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 【异地】2.2.1.7 就诊登记回退 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetCancelYdInPatientInRegXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            //工伤的入院时间格式是yyyyMMddHHmm
            //其他的入院时间格式是yyyyMMddHH
            string inDateStyle = string.Empty;
            if (personInfo.GSFlag.Equals("1"))
            {
                inDateStyle = "yyyyMMddHHmm";
            }
            else
            {
                inDateStyle = "yyyyMMdd";
            }


            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            #region 患者参数

            //参保地统筹区编码
            XmlElement otransid = xml.CreateElement("otransid");//参保地统筹区编码");
            otransid.InnerText = personInfo.regTransID;
            root.AppendChild(otransid);


            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//医疗服务机构名称
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = patientInfo.IDCard;
            root.AppendChild(aac044);

            //入院经办人
            XmlElement node7 = xml.CreateElement("aae011");//经办人");
            node7.InnerText = FS.FrameWork.Management.Connection.Operator.Name;
            root.AppendChild(node7);

            //经办时间,不能为空
            XmlElement node8 = xml.CreateElement("aae036");//经办时间");
            node8.InnerText = System.DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
            root.AppendChild(node8);

            #endregion

            return xml.InnerXml.ToString();
        }


        /// <summary>
        /// 4.40	提交月度申报（0521）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="alPatientSI"></param>
        /// <param name="hsPatienct"></param>
        /// <returns></returns>
        public string SetMonthlyReportSubmitXML(string year, string month,ref FS.FrameWork.Models.NeuObject commObj,
                        FS.FrameWork.Models.NeuObject neuObj, ArrayList alPatientSI, Hashtable hsPatienct)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //结算年度
            XmlElement yzz060 = xml.CreateElement("yzz060");//结算年度
            yzz060.InnerText = year;
            root.AppendChild(yzz060);

            //结算月份
            XmlElement yzz061 = xml.CreateElement("yzz061");//结算月份
            yzz061.InnerText = month.PadLeft(2, '0');
            root.AppendChild(yzz061);

            //起始日期
            XmlElement yzz041 = xml.CreateElement("yzz041");//起始日期
            yzz041.InnerText = year+month.PadLeft(2,'0')+"01";
            root.AppendChild(yzz041);

            //截止日期
            XmlElement yzz042 = xml.CreateElement("yzz042");//截止日期
            DateTime dtEnd = DateTime.ParseExact(yzz041.InnerText, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            yzz042.InnerText = (dtEnd.AddMonths(1).AddDays(-1)).ToString("yyyyMMdd");
            root.AppendChild(yzz042);

             //结算申报业务号
            XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            yzz062.InnerText = "B" + personInfo.HospitalizeAreaCode + year + month.PadLeft(2, '0') + commObj.ID;
            commObj.ID = "B" + personInfo.HospitalizeAreaCode + year + month.PadLeft(2, '0') + commObj.ID;
            root.AppendChild(yzz062);

            //险种类型
            XmlElement aae140 = xml.CreateElement("aae140");//险种类型
            aae140.InnerText = commObj.Name;
            root.AppendChild(aae140);

            //负责人
            XmlElement yzz134 = xml.CreateElement("yzz134");//负责人
            yzz134.InnerText = commObj.User01;
            root.AppendChild(yzz134);

            //复核人
            XmlElement yzz135 = xml.CreateElement("yzz135");//复核人
            yzz135.InnerText = commObj.User02;
            root.AppendChild(yzz135);

            //填表人
            XmlElement yzz136 = xml.CreateElement("yzz136");//填表人
            yzz136.InnerText = commObj.User03;
            root.AppendChild(yzz136);

            //填报日期
            XmlElement yzz137 = xml.CreateElement("yzz137");//填报日期
            yzz137.InnerText = DateTime.Now.ToString("yyyyMMdd");
            root.AppendChild(yzz137);

            //联系电话
            XmlElement yzz138 = xml.CreateElement("yzz138");//联系电话
            yzz138.InnerText = commObj.Memo;
            root.AppendChild(yzz138);

            //申报费用笔数
            XmlElement yzz063 = xml.CreateElement("yzz063");//申报费用笔数
            yzz063.InnerText = alPatientSI.Count.ToString();
            root.AppendChild(yzz063);

            decimal totCost = 0;

             XmlElement row1 = xml.CreateElement("detail");//费用明细列表");
             foreach (FoShanYDSI.Objects.SIPersonInfo personInfoTemp in alPatientSI)
             {
                 FS.HISFC.Models.RADT.PatientInfo patientTemp = hsPatienct[personInfoTemp.InPatient_No] as FS.HISFC.Models.RADT.PatientInfo;
                 XmlElement row2 = xml.CreateElement("row");

                 XmlNode aac002 = xml.CreateElement("aac002");//社会保障号码");
                 aac002.InnerText = personInfoTemp.SIcode;
                 row2.AppendChild(aac002);

                 XmlNode aac043 = xml.CreateElement("aac043");//证件类型");
                 aac043.InnerText = personInfoTemp.ZJLX;
                 row2.AppendChild(aac043);

                 XmlNode aac044 = xml.CreateElement("aac044");//证件号码");
                 aac044.InnerText = personInfoTemp.IdenNo;
                 row2.AppendChild(aac044);

                 XmlNode aac003 = xml.CreateElement("aac003");//姓名");
                 aac003.InnerText = personInfoTemp.Name;
                 row2.AppendChild(aac003);

                 XmlNode aab301 = xml.CreateElement("aab301");//姓名");
                 aab301.InnerText = personInfoTemp.InsuredAreaCode;
                 row2.AppendChild(aab301);

                 XmlNode ykc700 = xml.CreateElement("ykc700");//就诊登记号");
                 ykc700.InnerText = personInfoTemp.ClinicNo;
                 row2.AppendChild(ykc700);

                 XmlNode ykc618 = xml.CreateElement("ykc618");//结算业务号");
                 ykc618.InnerText = personInfoTemp.JSYWH;
                 row2.AppendChild(ykc618);

                 XmlNode ykc701 = xml.CreateElement("ykc701");//入院日期");
                 ykc701.InnerText = patientTemp.PVisit.InTime.ToString("yyyyMMdd");
                 row2.AppendChild(ykc701);

                 XmlNode ykc702 = xml.CreateElement("ykc702");//出院日期");
                 ykc702.InnerText = patientTemp.PVisit.OutTime.ToString("yyyyMMdd");
                 row2.AppendChild(ykc702);

                 XmlElement indays = xml.CreateElement("akb063");//住院床日");
                 TimeSpan ts = patientTemp.PVisit.OutTime - patientTemp.PVisit.InTime;
                 indays.InnerText = (ts.Days > 0) ? ts.Days.ToString() : "1";
                 row2.AppendChild(indays);

                 XmlNode akc194 = xml.CreateElement("akc194");//就诊结算日期");
                 akc194.InnerText = patientTemp.BalanceDate.ToString("yyyyMMdd");
                 row2.AppendChild(akc194);

                 XmlNode akc050 = xml.CreateElement("akc050");//入院疾病诊断名称");
                 akc050.InnerText = patientTemp.ClinicDiagnose;
                 row2.AppendChild(akc050);

                 XmlNode akc185 = xml.CreateElement("akc185");//出院疾病诊断名称");
                 akc185.InnerText = this.SIMgr.QueryICDName(personInfoTemp.Diagn1);
                 row2.AppendChild(akc185);

                 XmlNode akc264 = xml.CreateElement("akc264");//出院疾病诊断名称");
                 akc264.InnerText = personInfoTemp.tot_cost.ToString();
                 row2.AppendChild(akc264);

                 totCost = totCost + personInfoTemp.tot_cost;

                 XmlNode akb068 = xml.CreateElement("akb068");//统筹支付金额合计");
                 akb068.InnerText = personInfoTemp.YBTCZF_cost.ToString();
                 row2.AppendChild(akb068);

                 XmlNode ykc630 = xml.CreateElement("ykc630");//大病医疗统筹支付部分");
                 ykc630.InnerText = personInfoTemp.DBYLTCZF_cost.ToString();
                 row2.AppendChild(ykc630);

                 XmlNode aka130 = xml.CreateElement("aka130");//医疗类别");
                 aka130.InnerText = personInfoTemp.SeeDocType;
                 row2.AppendChild(aka130);

                 XmlNode aae140_2 = xml.CreateElement("aae140");//险种类型");
                 aae140_2.InnerText = personInfoTemp.xzlx;
                 row2.AppendChild(aae140_2);

                 XmlNode yzz139 = xml.CreateElement("yzz139");//个人自负金额");
                 yzz139.InnerText = personInfoTemp.GRZIFUJE.ToString();
                 row2.AppendChild(yzz139);

                 XmlNode aae013 = xml.CreateElement("aae013");//备注");
                 aae013.InnerText = personInfoTemp.Memo;
                 row2.AppendChild(aae013);

                 XmlNode akc190 = xml.CreateElement("akc190");//住院号(门诊号)
                 akc190.InnerText = patientTemp.PID.PatientNO;
                 row2.AppendChild(akc190);
                 row1.AppendChild(row2);
             }
             root.AppendChild(row1);

             //申报费用笔数
             XmlElement yzz064 = xml.CreateElement("yzz064");//申报费用笔数
             yzz064.InnerText = totCost.ToString();
             root.AppendChild(yzz064);
             xml.AppendChild(root);
             return xml.InnerXml.ToString();
        }
        /// <summary>
        /// 提交月度申报汇总表回退
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="alPatientSI"></param>
        /// <param name="hsPatienct"></param>
        /// <returns></returns>
        public string SetMonthlyReportCancelXML(string year, string month, ref FS.FrameWork.Models.NeuObject commObj,
                        FS.FrameWork.Models.NeuObject neuObj)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            //原交易流水号
            XmlElement otransid = xml.CreateElement("otransid");//原交易流水号
            otransid.InnerText =  commObj.Memo;
            root.AppendChild(otransid);

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);


            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);


            //结算年度
            XmlElement yzz060 = xml.CreateElement("yzz060");//结算年度
            yzz060.InnerText = year;
            root.AppendChild(yzz060);

            //结算月份
            XmlElement yzz061 = xml.CreateElement("yzz061");//结算月份
            yzz061.InnerText = month.PadLeft(2, '0');
            root.AppendChild(yzz061);

            //结算申报业务号

            XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            yzz062.InnerText = commObj.ID;
            root.AppendChild(yzz062);

            //XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            //yzz062.InnerText = "B" + personInfo.HospitalizeAreaCode + year + month.PadLeft(2, '0') + commObj.ID;
            //commObj.ID = "B" + personInfo.HospitalizeAreaCode + year + month.PadLeft(2, '0') + commObj.ID;
            //root.AppendChild(yzz062);

            //险种类型
            //XmlElement aae140 = xml.CreateElement("aae140");//险种类型
            //aae140.InnerText = commObj.Name;
            //root.AppendChild(aae140);

            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 月度结算申报回退0522
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="alPatientSI"></param>
        /// <param name="hsPatienct"></param>
        /// <returns></returns>
        public string SetMonthlyReportCancelByCustomTypeXML(string year, string month, ref FS.FrameWork.Models.NeuObject commObj,
                        FS.FrameWork.Models.NeuObject neuObj)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            //原交易流水号
            XmlElement otransid = xml.CreateElement("otransid");//原交易流水号
            otransid.InnerText = commObj.Memo;
            root.AppendChild(otransid);

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);


            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);


            //结算年度
            XmlElement yzz060 = xml.CreateElement("yzz060");//结算年度
            yzz060.InnerText = year;
            root.AppendChild(yzz060);

            //结算月份
            XmlElement yzz061 = xml.CreateElement("yzz061");//结算月份
            yzz061.InnerText = month.PadLeft(2, '0');
            root.AppendChild(yzz061);

            //结算申报业务号

            XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            yzz062.InnerText = commObj.ID;
            root.AppendChild(yzz062);

            //XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            //yzz062.InnerText = "B" + personInfo.HospitalizeAreaCode + year + month.PadLeft(2, '0') + commObj.ID;
            //commObj.ID = "B" + personInfo.HospitalizeAreaCode + year + month.PadLeft(2, '0') + commObj.ID;
            //root.AppendChild(yzz062);

            //险种类型
            XmlElement aae140 = xml.CreateElement("aae140");//险种类型
            aae140.InnerText = commObj.Name;
            root.AppendChild(aae140);

            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 月度结算申报回退0522
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="alPatientSI"></param>
        /// <param name="hsPatienct"></param>
        /// <returns></returns>
        public string SetMonthlyReportCancelByCustomType0537XML(string year, string month, ref FS.FrameWork.Models.NeuObject commObj,
                        FS.FrameWork.Models.NeuObject neuObj)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            //原交易流水号
            XmlElement otransid = xml.CreateElement("otransid");//原交易流水号
            otransid.InnerText = commObj.Memo;
            root.AppendChild(otransid);

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);


            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);


            //结算年度
            XmlElement yzz060 = xml.CreateElement("yzz060");//结算年度
            yzz060.InnerText = year;
            root.AppendChild(yzz060);

            //结算月份
            XmlElement yzz061 = xml.CreateElement("yzz061");//结算月份
            yzz061.InnerText = month.PadLeft(2, '0');
            root.AppendChild(yzz061);

            //结算申报业务号

            XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            yzz062.InnerText = commObj.ID;
            root.AppendChild(yzz062);

            //XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            //yzz062.InnerText = "B" + personInfo.HospitalizeAreaCode + year + month.PadLeft(2, '0') + commObj.ID;
            //commObj.ID = "B" + personInfo.HospitalizeAreaCode + year + month.PadLeft(2, '0') + commObj.ID;
            //root.AppendChild(yzz062);

            //险种类型
            XmlElement aae140 = xml.CreateElement("aae140");//险种类型
            aae140.InnerText = commObj.Name;
            root.AppendChild(aae140);

            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }
        /// <summary>
        ///  提交审核支付明细回退0524
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <returns></returns>
        public string SetMonthlyReportCancelDetailXML(string year, string month, ref FS.FrameWork.Models.NeuObject commObj,
                        FS.FrameWork.Models.NeuObject neuObj)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            //原交易流水号
            XmlElement otransid = xml.CreateElement("otransid");//原交易流水号
            otransid.InnerText = commObj.Memo;
            root.AppendChild(otransid);

            XmlNode aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）");
            aab301.InnerText = neuObj.User01;
            root.AppendChild(aab301);

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);


            //医疗机构执业许可证登记号
            //XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            //akb026.InnerText = personInfo.HospitalCode;
            //root.AppendChild(akb026);

            //医疗服务机构名称
            //XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            //akb021.InnerText = personInfo.HospitalName;
            //root.AppendChild(akb021);


            //结算年度
            XmlElement yzz060 = xml.CreateElement("yzz060");//结算年度
            yzz060.InnerText = year;
            root.AppendChild(yzz060);

            //结算月份
            XmlElement yzz061 = xml.CreateElement("yzz061");//结算月份
            yzz061.InnerText = month.PadLeft(2, '0');
            root.AppendChild(yzz061);


            //结算申报业务号

            //XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            //yzz062.InnerText = commObj.ID;
            //root.AppendChild(yzz062);

            //审核支付业务号
            XmlElement yzz065 = xml.CreateElement("yzz065");//审核支付业务号
            yzz065.InnerText = "P" + neuObj.User01 + year + month.PadLeft(2, '0') ;
            root.AppendChild(yzz065);

            //险种类型
            XmlElement aae140 = xml.CreateElement("aae140");//险种类型
            aae140.InnerText = commObj.Name;
            root.AppendChild(aae140);

            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }
        /// <summary>
        /// 4.53	提交月度申报汇总表(0534)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="alPatientSI">这里填汇总实体，借用一下</param>
        /// <returns></returns>
        public string SetMonthlyReportTotXML(string year, string month, ref FS.FrameWork.Models.NeuObject commObj,ArrayList alPatientSI)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //结算年度
            XmlElement yzz060 = xml.CreateElement("yzz060");//结算年度
            yzz060.InnerText = year;
            root.AppendChild(yzz060);

            //结算月份
            XmlElement yzz061 = xml.CreateElement("yzz061");//结算月份
            yzz061.InnerText = month.PadLeft(2, '0');
            root.AppendChild(yzz061);

            //起始日期
            XmlElement yzz041 = xml.CreateElement("yzz041");//起始日期
            yzz041.InnerText = year + month.PadLeft(2, '0') + "01";
            root.AppendChild(yzz041);

            //截止日期
            XmlElement yzz042 = xml.CreateElement("yzz042");//截止日期
            DateTime dtEnd = DateTime.ParseExact(yzz041.InnerText, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            yzz042.InnerText = (dtEnd.AddMonths(1).AddDays(-1)).ToString("yyyyMMdd");
            root.AppendChild(yzz042);

            //结算申报业务号
            XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            yzz062.InnerText = commObj.ID;//"B" + personInfo.HospitalizeAreaCode + year + month + commObj.ID;
            //commObj.ID = "B" + personInfo.HospitalizeAreaCode + year + month + commObj.ID;
            root.AppendChild(yzz062);

            //负责人
            XmlElement yzz134 = xml.CreateElement("yzz134");//负责人
            yzz134.InnerText = commObj.User01;
            root.AppendChild(yzz134);

            //复核人
            XmlElement yzz135 = xml.CreateElement("yzz135");//复核人
            yzz135.InnerText = commObj.User02;
            root.AppendChild(yzz135);

            //填表人
            XmlElement yzz136 = xml.CreateElement("yzz136");//填表人
            yzz136.InnerText = commObj.User03;
            root.AppendChild(yzz136);

            //填报日期
            XmlElement yzz137 = xml.CreateElement("yzz137");//填报日期
            yzz137.InnerText = DateTime.Now.ToString("yyyyMMdd");
            root.AppendChild(yzz137);

            //联系电话
            XmlElement yzz138 = xml.CreateElement("yzz138");//联系电话
            yzz138.InnerText = commObj.Memo;
            root.AppendChild(yzz138);

            decimal totCost = 0;

            XmlElement row1 = xml.CreateElement("detail");//费用明细列表");
            foreach (FoShanYDSI.Objects.SIPersonInfo personInfoTemp in alPatientSI)
            {
                //FS.HISFC.Models.RADT.PatientInfo patientTemp = hsPatienct[personInfoTemp.InPatient_No] as FS.HISFC.Models.RADT.PatientInfo;
                XmlElement row2 = xml.CreateElement("row");

                XmlNode aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）");
                aab301.InnerText = personInfoTemp.InsuredAreaCode.ToString();
                row2.AppendChild(aab301);

                XmlNode ake096 = xml.CreateElement("ake096");//就医人数");
                ake096.InnerText = personInfoTemp.Diagn1.ToString();//借用一下
                row2.AppendChild(ake096);

                XmlNode ake098 = xml.CreateElement("ake098");//就医人次");
                ake098.InnerText = personInfoTemp.Diagn2.ToString();//借用一下
                row2.AppendChild(ake098);

                XmlNode akc264 = xml.CreateElement("akc264");//医疗费总额");
                akc264.InnerText = personInfoTemp.tot_cost.ToString();
                row2.AppendChild(akc264);

                XmlNode akb068 = xml.CreateElement("akb068");//统筹支付金额合计");
                akb068.InnerText = personInfoTemp.YBTCZF_cost.ToString();
                row2.AppendChild(akb068);

                XmlNode ykc630 = xml.CreateElement("ykc630");//大病医疗统筹支付部分");
                ykc630.InnerText = personInfoTemp.DBYLTCZF_cost.ToString();
                row2.AppendChild(ykc630);


                XmlNode yzz139 = xml.CreateElement("yzz139");//个人自负金额");
                yzz139.InnerText = personInfoTemp.GRZIFUJE.ToString();
                row2.AppendChild(yzz139);

                XmlNode aae013 = xml.CreateElement("aae013");//备注");
                aae013.InnerText = personInfoTemp.Memo;
                row2.AppendChild(aae013);

                row1.AppendChild(row2);
            }
            root.AppendChild(row1);
            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }


        /// <summary>
        /// 4.55	提交月度申报分险种汇总表(0536)（新增）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="alPatientSI"></param>
        /// <param name="hsPatienct"></param>
        /// <returns></returns>
        public string SetMonthlyReportSubmitXZXML(string year, string month,string areaCount,
            ref FS.FrameWork.Models.NeuObject commObj,ArrayList alPatientSI)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //结算年度
            XmlElement yzz060 = xml.CreateElement("yzz060");//结算年度
            yzz060.InnerText = year;
            root.AppendChild(yzz060);

            //结算月份
            XmlElement yzz061 = xml.CreateElement("yzz061");//结算月份
            yzz061.InnerText = month.PadLeft(2, '0');
            root.AppendChild(yzz061);

            //起始日期
            XmlElement yzz041 = xml.CreateElement("yzz041");//起始日期
            yzz041.InnerText = year + month.PadLeft(2, '0') + "01";
            root.AppendChild(yzz041);

            //截止日期
            XmlElement yzz042 = xml.CreateElement("yzz042");//截止日期
            DateTime dtEnd = DateTime.ParseExact(yzz041.InnerText, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            yzz042.InnerText = (dtEnd.AddMonths(1).AddDays(-1)).ToString("yyyyMMdd");
            root.AppendChild(yzz042);

            //结算申报业务号
            XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            yzz062.InnerText = commObj.ID;//"B" + personInfo.HospitalizeAreaCode + year + month + commObj.ID;
            //commObj.ID = "B" + personInfo.HospitalizeAreaCode + year + month + commObj.ID;
            root.AppendChild(yzz062);

            //险种类型
            XmlElement aae140 = xml.CreateElement("aae140");//险种类型
            aae140.InnerText = commObj.Name;
            root.AppendChild(aae140);

            //负责人
            XmlElement yzz133 = xml.CreateElement("yzz133");//合计参保地数量
            yzz133.InnerText = areaCount;
            root.AppendChild(yzz133);

            //负责人
            XmlElement yzz134 = xml.CreateElement("yzz134");//负责人
            yzz134.InnerText = commObj.User01;
            root.AppendChild(yzz134);

            //复核人
            XmlElement yzz135 = xml.CreateElement("yzz135");//复核人
            yzz135.InnerText = commObj.User02;
            root.AppendChild(yzz135);

            //填表人
            XmlElement yzz136 = xml.CreateElement("yzz136");//填表人
            yzz136.InnerText = commObj.User03;
            root.AppendChild(yzz136);

            //填报日期
            XmlElement yzz137 = xml.CreateElement("yzz137");//填报日期
            yzz137.InnerText = DateTime.Now.ToString("yyyyMMdd");
            root.AppendChild(yzz137);

            //联系电话
            XmlElement yzz138 = xml.CreateElement("yzz138");//联系电话
            yzz138.InnerText = commObj.Memo;
            root.AppendChild(yzz138);

            XmlElement row1 = xml.CreateElement("detail");//费用明细列表");
            foreach (FoShanYDSI.Objects.SIPersonInfo personInfoTemp in alPatientSI)
            {
                //FS.HISFC.Models.RADT.PatientInfo patientTemp = hsPatienct[personInfoTemp.InPatient_No] as FS.HISFC.Models.RADT.PatientInfo;
                XmlElement row2 = xml.CreateElement("row");

                XmlNode aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）");
                aab301.InnerText = personInfoTemp.InsuredAreaCode.ToString();
                row2.AppendChild(aab301);

                XmlNode ake096 = xml.CreateElement("ake096");//就医人数");
                ake096.InnerText = personInfoTemp.Diagn1.ToString();//借用一下
                row2.AppendChild(ake096);

                XmlNode ake098 = xml.CreateElement("ake098");//就医人次");
                ake098.InnerText = personInfoTemp.Diagn2.ToString();//借用一下
                row2.AppendChild(ake098);

                XmlNode akc264 = xml.CreateElement("akc264");//医疗费总额");
                akc264.InnerText = personInfoTemp.tot_cost.ToString();
                row2.AppendChild(akc264);

                XmlNode akb068 = xml.CreateElement("akb068");//统筹支付金额合计");
                akb068.InnerText = personInfoTemp.YBTCZF_cost.ToString();
                row2.AppendChild(akb068);

                XmlNode ykc630 = xml.CreateElement("ykc630");//大病医疗统筹支付部分");
                ykc630.InnerText = personInfoTemp.DBYLTCZF_cost.ToString();
                row2.AppendChild(ykc630);


                XmlNode yzz139 = xml.CreateElement("yzz139");//个人自负金额");
                yzz139.InnerText = personInfoTemp.GRZIFUJE.ToString();
                row2.AppendChild(yzz139);

                XmlNode aae013 = xml.CreateElement("aae013");//备注");
                aae013.InnerText = personInfoTemp.Memo;
                row2.AppendChild(aae013);

                row1.AppendChild(row2);
            }
            root.AppendChild(row1);
            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 【异地】2.2.1.9 出院登记回退 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetCancelYdInPatientOutRegXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            //工伤的入院时间格式是yyyyMMddHHmm
            //其他的入院时间格式是yyyyMMddHH
            string inDateStyle = string.Empty;
            if (personInfo.GSFlag.Equals("1"))
            {
                inDateStyle = "yyyyMMddHHmm";
            }
            else
            {
                inDateStyle = "yyyyMMdd";
            }


            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            #region 患者参数

            //参保地统筹区编码
            XmlElement otransid = xml.CreateElement("otransid");//参保地统筹区编码");
            otransid.InnerText = personInfo.Out_TransID;
            root.AppendChild(otransid);


            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //行政区划代码（参保地）
            XmlElement ykc700 = xml.CreateElement("ykc700");//行政区划代码（参保地）
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = patientInfo.IDCard;
            root.AppendChild(aac044);


            //入院经办人
            XmlElement node7 = xml.CreateElement("aae011");//经办人");
            node7.InnerText = FS.FrameWork.Management.Connection.Operator.Name;
            root.AppendChild(node7);

            //经办时间,不能为空
            XmlElement node8 = xml.CreateElement("aae036");//经办时间");
            node8.InnerText = System.DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
            root.AppendChild(node8);


            #endregion

            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 【异地】4.29	费用结算回退（0305） 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetCancelYdInPatientBalanceXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            //工伤的入院时间格式是yyyyMMddHHmm
            //其他的入院时间格式是yyyyMMddHH
            string inDateStyle = string.Empty;
            if (personInfo.GSFlag.Equals("1"))
            {
                inDateStyle = "yyyyMMddHHmm";
            }
            else
            {
                inDateStyle = "yyyyMMdd";
            }


            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            #region 患者参数

            //原交易流水号
            XmlElement otransid = xml.CreateElement("otransid");//原交易流水号");
            otransid.InnerText = personInfo.balanceTransID;
            root.AppendChild(otransid);


            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//就诊登记号
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = patientInfo.IDCard;
            root.AppendChild(aac044);

            //结算业务号
            XmlElement ykc618 = xml.CreateElement("ykc618");//结算业务号
            ykc618.InnerText = personInfo.JSYWH;
            root.AppendChild(ykc618);

            //入院经办人
            XmlElement node7 = xml.CreateElement("aae011");//经办人");
            node7.InnerText = FS.FrameWork.Management.Connection.Operator.Name;
            root.AppendChild(node7);

            //经办时间,不能为空
            XmlElement node8 = xml.CreateElement("aae036");//经办时间");
            node8.InnerText = System.DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
            root.AppendChild(node8);

            //医院结算业务序列号
            XmlElement yzz021 = xml.CreateElement("yzz021");//医院结算业务序列号
            yzz021.InnerText = patientInfo.ID;
            root.AppendChild(yzz021);

            #endregion

            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 【异地】2.2.1.8 出院登记 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetYDInpatientOutRegXML(ref FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            this.GetHosCodeAndRegionCode(ref personInfo);
            #region 出院登记参数
            #region 患者参数

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//就诊登记号
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(personInfo.MCardNo) ? "-" : personInfo.MCardNo);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = personInfo.IdenNo;
            root.AppendChild(aac044);

            #endregion

            string OutCompareDeptCode = this.SIMgr.GetCompareDeptCode(patientInfo.PVisit.PatientLocation.Dept.ID);
            XmlNode OutCompareDeptCodeNode = xml.CreateElement("akf002");//出院科室");
            OutCompareDeptCodeNode.InnerText = OutCompareDeptCode;// "0400";//OutCompareDeptCode;
            root.AppendChild(OutCompareDeptCodeNode);

            XmlNode yzz088 = xml.CreateElement("yzz088");//出院病区号");
            yzz088.InnerText = "";
            root.AppendChild(yzz088);

            XmlNode yzz089 = xml.CreateElement("yzz089");//出院病区名称");
            yzz089.InnerText = "";
            root.AppendChild(yzz089);

            XmlNode bed_no = xml.CreateElement("ykc016");//出院床位");
            bed_no.InnerText = patientInfo.PVisit.PatientLocation.Bed.ToString();
            root.AppendChild(bed_no);

            XmlElement DignNode1 = xml.CreateElement("akc185");//出院诊断");
            DignNode1.InnerText = personInfo.DiagnName;// "1";//patientInfo.Diagnoses[0].ToString();
            root.AppendChild(DignNode1);

            XmlNode ICD10_1 = xml.CreateElement("akc196");//出院诊断疾病编码1_ICD10");
            ICD10_1.InnerText = personInfo.Diagn1;//"J10.101";//暂空personInfo.Diagn1;
            root.AppendChild(ICD10_1);

            XmlNode ICD10_2 = xml.CreateElement("akc188");//出院诊断编码2_ICD10");
            ICD10_2.InnerText = personInfo.Diagn2; // "";//暂空personInfo.Diagn2;
            root.AppendChild(ICD10_2);

            XmlNode ICD10_3 = xml.CreateElement("akc189");//出院诊断编码3_ICD10");
            ICD10_3.InnerText = personInfo.Diagn3; //"";//暂空personInfo.Diagn3;
            root.AppendChild(ICD10_3);

            XmlNode akc056 = xml.CreateElement("akc056");//医师执业证编码
            akc056.InnerText = ""; //"";//暂空personInfo.Diagn3;
            root.AppendChild(akc056);

            XmlNode DiagnosesDoc = xml.CreateElement("ake021");//出院诊断医生");
            DiagnosesDoc.InnerText = this.SIMgr.GetDocIDNo(patientInfo.PVisit.AdmittingDoctor.ID); //"52010319870919482X";//this.SIMgr.GetDocIDNo(patientInfo.PVisit.AdmittingDoctor.ID); //patientInfo.PVisit.AdmittingDoctor.Name.ToString();
            root.AppendChild(DiagnosesDoc);

            XmlNode out_Reason = xml.CreateElement("ykc195");//出院原因");
            out_Reason.InnerText = "2";//personInfo.out_reason;//有相关规定，看接口问题，后续完善
            root.AppendChild(out_Reason);

            XmlNode SSMC = xml.CreateElement("ykc683");//手术名称");
            SSMC.InnerText = "";//暂空
            root.AppendChild(SSMC);

            XmlElement OutDateNode = xml.CreateElement("ykc702");//出院时间");
            OutDateNode.InnerText = patientInfo.PVisit.OutTime.ToString("yyyyMMdd");//yyyyMMdd");
            root.AppendChild(OutDateNode);

            XmlElement indays = xml.CreateElement("akb063");//住院床日");
            TimeSpan ts = patientInfo.PVisit.OutTime.Date - patientInfo.PVisit.InTime.Date;
            indays.InnerText = (ts.Days > 0) ? ts.Days.ToString() : "1";
            root.AppendChild(indays);

            XmlElement nodeOperater = xml.CreateElement("aae011");//出院经办人");
            nodeOperater.InnerText = FS.FrameWork.Management.Connection.Operator.Name;
            root.AppendChild(nodeOperater);

            XmlElement nodeOper_date = xml.CreateElement("ykc018");//出院经办时间");
            nodeOper_date.InnerText = System.DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
            root.AppendChild(nodeOper_date);

            XmlElement outDrugDate = xml.CreateElement("yzz020");//出院带药天数");
            outDrugDate.InnerText = "";
            root.AppendChild(outDrugDate);
            #endregion
            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 【异地】2.2.1.10 费用明细回退 获取XML
        /// 都是全退，重传
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetCancelYdFeeDetailXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            //工伤的入院时间格式是yyyyMMddHHmm
            //其他的入院时间格式是yyyyMMddHH
            string inDateStyle = string.Empty;
            if (personInfo.GSFlag.Equals("1"))
            {
                inDateStyle = "yyyyMMddHHmm";
            }
            else
            {
                inDateStyle = "yyyyMMdd";
            }


            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            #region 患者参数

            //原交易流水号
            XmlElement otransid = xml.CreateElement("otransid");//原交易流水号 , 0 为回退所有费用信息;
            otransid.InnerText = "0";
            root.AppendChild(otransid);


            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//就诊登记号
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(personInfo.MCardNo) ? "-" : personInfo.MCardNo);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //医院费用序列号
            XmlElement ykc610 = xml.CreateElement("ykc610");//医院费用序列号
            ykc610.InnerText = "";
            root.AppendChild(ykc610);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = personInfo.IdenNo;
            root.AppendChild(aac044);

            #endregion

            //入院经办人
            XmlElement node9 = xml.CreateElement("aae011");//经办人");
            node9.InnerText = FS.FrameWork.Management.Connection.Operator.Name;
            root.AppendChild(node9);

            //经办时间,不能为空
            XmlElement node8 = xml.CreateElement("aae036");//经办时间");
            node8.InnerText = System.DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
            root.AppendChild(node8);

            return xml.InnerXml.ToString();
        }

        #region 作废
        ///// <summary>
        ///// 【异地】2.2.1.11 费用结算回退 获取XML
        ///// </summary>
        ///// <param name="patientInfo"></param>
        ///// <param name="personInfo"></param>
        ///// <returns></returns>
        //public string SetCancelYdInPatientBalanceXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        //{
        //    //工伤的入院时间格式是yyyyMMddHHmm
        //    //其他的入院时间格式是yyyyMMddHH
        //    string inDateStyle = string.Empty;
        //    if (personInfo.GSFlag.Equals("1"))
        //    {
        //        inDateStyle = "yyyyMMddHHmm";
        //    }
        //    else
        //    {
        //        inDateStyle = "yyyyMMdd";
        //    }

        //    this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

        //    XmlDocument xml = new XmlDocument();
        //    xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
        //    XmlElement root = xml.CreateElement("XML");
        //    xml.AppendChild(root);

        //    #region 患者参数
        //    //参保地统筹区编码
        //    XmlElement node1 = xml.CreateElement("yab003");//参保地统筹区编码");
        //    node1.InnerText = personInfo.InsuredAreaCode;
        //    root.AppendChild(node1);

        //    //就医地统筹区编码
        //    XmlElement node2 = xml.CreateElement("yab300");//就医地统筹区编码");
        //    node2.InnerText = personInfo.HospitalizeAreaCode;
        //    root.AppendChild(node2);

        //    //就医地分中心编码
        //    XmlElement node3 = xml.CreateElement("yab600");//就医地分中心编号");
        //    node3.InnerText = personInfo.HospitalizeCenterAreaCode;
        //    root.AppendChild(node3);

        //    //医院编码
        //    XmlElement node4 = xml.CreateElement("akb020");//医院编码");
        //    node4.InnerText = personInfo.HospitalCode;
        //    root.AppendChild(node4);

        //    //医保编号
        //    XmlElement node5 = xml.CreateElement("aac001");//医保编号");
        //    node5.InnerText = (string.IsNullOrEmpty(personInfo.SIcode) ? "01" : personInfo.SIcode);
        //    root.AppendChild(node5);

        //    XmlElement node6 = xml.CreateElement("akc190");//就诊登记号");
        //    node6.InnerText = personInfo.ClinicNo;
        //    root.AppendChild(node6);

        //    //结算业务号
        //    XmlElement node7 = xml.CreateElement("ykc618");//结算业务号");
        //    node7.InnerText = personInfo.JSYWH;
        //    root.AppendChild(node7);

        //    //入院经办人
        //    XmlElement node9 = xml.CreateElement("aae011");//经办人");
        //    node9.InnerText = FS.FrameWork.Management.Connection.Operator.Name;
        //    root.AppendChild(node9);

        //    //经办时间,不能为空
        //    XmlElement node8 = xml.CreateElement("aae036");//经办时间");
        //    node8.InnerText = System.DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
        //    root.AppendChild(node8);

        //    #endregion

        //    return xml.InnerXml.ToString();
        //}
        #endregion

        /// <summary>
        /// 【异地】2.2.1.12 查询个人就医资料信息 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetQueryYdInPatientInfoXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            //工伤的入院时间格式是yyyyMMddHHmm
            //其他的入院时间格式是yyyyMMddHH
            string inDateStyle = string.Empty;
            if (personInfo.GSFlag.Equals("1"))
            {
                inDateStyle = "yyyyMMddHHmm";
            }
            else
            {
                inDateStyle = "yyyyMMdd";
            }

            this.GetHosCodeAndRegionCode(ref personInfo);//调测用，先写死，后需对照

            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("XML");
            xml.AppendChild(root);

            #region 患者参数
            //参保地统筹区编码
            XmlElement node1 = xml.CreateElement("yab003");//参保地统筹区编码");
            node1.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(node1);

            //就医地统筹区编码
            XmlElement node2 = xml.CreateElement("yab300");//就医地统筹区编码");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地分中心编码
            XmlElement node3 = xml.CreateElement("yab600");//就医地分中心编号");
            node3.InnerText = personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医院编码
            XmlElement node4 = xml.CreateElement("akb020");//医院编码");
            node4.InnerText = personInfo.HospitalCode;
            root.AppendChild(node4);

            //医保编号
            XmlElement node5 = xml.CreateElement("aac001");//医保编号");
            node5.InnerText = (string.IsNullOrEmpty(personInfo.SIcode) ? "01" : personInfo.SIcode);
            root.AppendChild(node5);

            //证件号码,不能为空
            XmlElement node9 = xml.CreateElement("xxxxxx");//证件号码");
            node9.InnerText = personInfo.IdenNo;
            root.AppendChild(node9);

            XmlElement node6 = xml.CreateElement("akc190");//就诊登记号");
            node6.InnerText = personInfo.ClinicNo;
            root.AppendChild(node6);

            #endregion

            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 4.74 已上传的费用明细信息查询XML(0707)
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetQueryInPatientUpFeeDetailXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            this.GetHosCodeAndRegionCode(ref personInfo);

            #region 患者参数
            //数字签名
            XmlElement sign = xml.CreateElement("sign");//数字签名
            sign.InnerText = "";
            root.AppendChild(sign);
            //交易流水号
            XmlElement transid = xml.CreateElement("transid");//交易流水号
            transid.InnerText = "";
            root.AppendChild(transid);
            //行政区划代码（就医地）
            XmlElement node1 = xml.CreateElement("aab299");//行政区划代码（就医地）
            node1.InnerText = "440600";
            root.AppendChild(node1);
            //就医地分中心编码
            XmlElement node2 = xml.CreateElement("yab600");//就医地分中心编码
            node2.InnerText = personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node2);
            //医疗机构执业许可证登记号
            XmlElement node3 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            node3.InnerText = personInfo.HospitalCode;
            root.AppendChild(node3);
            //行政区划代码（参保地）
            XmlElement node4 = xml.CreateElement("aab301");//行政区划代码（参保地）
            node4.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(node4);

            //社会保障号码
            XmlElement node5 = xml.CreateElement("aac002");//社会保障号码
            node5.InnerText = (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(node5);

            //证件号码
            XmlElement node6 = xml.CreateElement("aac043");//证件号码
            node6.InnerText = personInfo.ZJLX;
            root.AppendChild(node6);

            //证件号码
            XmlElement node7 = xml.CreateElement("aac044");//证件号码
            node7.InnerText = patientInfo.IDCard;
            root.AppendChild(node7);

            //就诊登记号
            XmlElement node8 = xml.CreateElement("ykc700");//就诊登记号
            node8.InnerText = personInfo.ClinicNo;
            root.AppendChild(node8);


            //结算业务号
            XmlElement node9 = xml.CreateElement("ykc618");//结算业务号
            node9.InnerText = personInfo.JSYWH;
            root.AppendChild(node9);

            //开始时间
            XmlElement node10 = xml.CreateElement("aae310");//开始时间
            node10.InnerText = patientInfo.PVisit.InTime.Date.ToString("yyyyMMdd");
            root.AppendChild(node10);

            //终止时间
            XmlElement node11 = xml.CreateElement("aae311");//终止时间
            node11.InnerText = DateTime.Now.Date.AddDays(1).ToString("yyyyMMdd");
            root.AppendChild(node11);

            //开始行数
            XmlElement node12 = xml.CreateElement("startrow");//开始行数
            node12.InnerText = "0";
            root.AppendChild(node12);


            #endregion

            return xml.InnerXml.ToString();
        }


        /// <summary>
        /// 4.74 已上传的费用明细信息查询(0707)  返回实体
        /// </summary>
        /// <param name="retXML"></param>
        /// <param name="patientInfo"></param>
        /// <param name="?"></param>
        public void GetQueryInPatientUpFeeDetailResult(string retXML, FS.HISFC.Models.RADT.PatientInfo patientInfo, ref ArrayList alPersonInfo, ref string status, ref string msg)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(retXML);
            }
            catch (Exception e)
            {
                return;
            }
            string totalrowStr = "";
            XmlNode totalrow = doc.SelectSingleNode("result/output/totalrow");//数量
            if (totalrow != null)
            {
                totalrowStr = totalrow.InnerText;
            }
            else
            {
                totalrowStr = "0";
            }
            FoShanYDSI.Objects.SIPersonFeeInfo personInfo = null;
            XmlNodeList nodes = doc.GetElementsByTagName("row");
            if (nodes.Count != 0)
            {
                alPersonInfo = new ArrayList();
                for (int i = 0; i < nodes.Count; i++)
                {
                    personInfo = new FoShanYDSI.Objects.SIPersonFeeInfo();
                    personInfo.totalrow = totalrowStr;
                    XmlNode seqno = nodes[i].SelectSingleNode("seqno");
                    if (seqno != null)
                    {
                        personInfo.seqno = seqno.InnerText;
                    }
                    XmlNode ykc700 = nodes[i].SelectSingleNode("ykc700");
                    if (ykc700 != null)
                    {
                        personInfo.ykc700 = ykc700.InnerText;
                    }
                    XmlNode ykc618 = nodes[i].SelectSingleNode("ykc618");
                    if (ykc618 != null)
                    {
                        personInfo.ykc618 = ykc618.InnerText;
                    }
                    XmlNode yka111 = nodes[i].SelectSingleNode("yka111");
                    if (yka111 != null)
                    {
                        personInfo.yka111 = yka111.InnerText;
                    }
                    XmlNode yka112 = nodes[i].SelectSingleNode("yka112");
                    if (yka112 != null)
                    {
                        personInfo.yka112 = yka112.InnerText;
                    }
                    XmlNode ake001 = nodes[i].SelectSingleNode("ake001");
                    if (ake001 != null)
                    {
                        personInfo.ake001 = ake001.InnerText;
                    }
                    XmlNode ake002 = nodes[i].SelectSingleNode("ake002");
                    if (ake002 != null)
                    {
                        personInfo.ake002 = ake002.InnerText;
                    }
                    XmlNode ake114 = nodes[i].SelectSingleNode("ake114");
                    if (ake114 != null)
                    {
                        personInfo.ake114 = ake114.InnerText;
                    }
                    XmlNode aka185 = nodes[i].SelectSingleNode("aka185");
                    if (aka185 != null)
                    {
                        personInfo.aka185 = aka185.InnerText;
                    }
                    XmlNode yke230 = nodes[i].SelectSingleNode("yke230");
                    if (yke230 != null)
                    {
                        personInfo.yke230 = yke230.InnerText;
                    }
                    XmlNode yke231 = nodes[i].SelectSingleNode("yke231");
                    if (yke231 != null)
                    {
                        personInfo.yke231 = yke231.InnerText;
                    }
                    XmlNode ake005 = nodes[i].SelectSingleNode("ake005");
                    if (ake005 != null)
                    {
                        personInfo.ake005 = ake005.InnerText;
                    }
                    XmlNode ake006 = nodes[i].SelectSingleNode("ake006");
                    if (ake006 != null)
                    {
                        personInfo.ake006 = ake006.InnerText;
                    }
                    XmlNode ykc610 = nodes[i].SelectSingleNode("ykc610");
                    if (ykc610 != null)
                    {
                        personInfo.ykc610 = ykc610.InnerText;
                    }
                    XmlNode akc264 = nodes[i].SelectSingleNode("akc264");
                    if (akc264 != null)
                    {
                        personInfo.akc264 = akc264.InnerText;
                    }
                    XmlNode aka069 = nodes[i].SelectSingleNode("aka069");
                    if (aka069 != null)
                    {
                        personInfo.aka069 = aka069.InnerText;
                    }
                    XmlNode akc228 = nodes[i].SelectSingleNode("akc228");
                    if (akc228 != null)
                    {
                        personInfo.akc228 = akc228.InnerText;
                    }
                    XmlNode akc253 = nodes[i].SelectSingleNode("akc253");
                    if (akc253 != null)
                    {
                        personInfo.akc253 = akc253.InnerText;
                    }
                    XmlNode akc254 = nodes[i].SelectSingleNode("akc254");
                    if (akc254 != null)
                    {
                        personInfo.akc254 = akc254.InnerText;
                    }
                    XmlNode yka319 = nodes[i].SelectSingleNode("yka319");
                    if (yka319 != null)
                    {
                        personInfo.yka319 = yka319.InnerText;
                    }
                    XmlNode aka065 = nodes[i].SelectSingleNode("aka065");
                    if (aka065 != null)
                    {
                        personInfo.aka065 = aka065.InnerText;
                    }
                    XmlNode akc226 = nodes[i].SelectSingleNode("akc226");
                    if (akc226 != null)
                    {
                        personInfo.akc226 = akc226.InnerText;
                    }
                    XmlNode akc225 = nodes[i].SelectSingleNode("akc225");
                    if (akc225 != null)
                    {
                        personInfo.akc225 = akc225.InnerText;
                    }
                    alPersonInfo.Add(personInfo);
                }

            }
        }
        /// <summary>
        /// 删除医保上传的费用明细
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int DeletePatientUploadFeeDetail(string lsh, string regNo,ref string errInfo)
        {
            if (string.IsNullOrEmpty(lsh))
            {
                errInfo = "删除费用明细失败，患者流水号为空！";
                return -1;
            }

            if (string.IsNullOrEmpty(regNo))
            {
                errInfo = "删除费用明细失败，就诊登记号为空！";
                return -1;
            }

            string strSql = "";

            if (this.ucMgr.Sql.GetSql("Fee.Interface.DeleteInPatientUploadFeeDetail", ref strSql) == -1)
            {
                errInfo = "获得[Fee.Interface.DeleteInPatientUploadFeeDetail]对应sql语句出错";
                return -1;
            }
            strSql = string.Format(strSql, lsh, regNo);

            try
            {
                return this.ucMgr.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                //this.ErrCode = ex.Message;
                errInfo = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 插入医保上传的费用明细信息
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int InsertInPatientUploadFeeDetail(FS.HISFC.Models.RADT.PatientInfo obj, FS.HISFC.Models.Fee.Inpatient.FeeItemList f, ref string errInfo)
        {
            if (f == null)
            {
                errInfo = "插入医保费用明细失败，费用为空！";
                return -1;
            }
            if (obj == null)
            {
                errInfo = "插入医保费用明细失败，患者信息为空！";
                return -1;
            }

            string strSql = "";

            if (this.ucMgr.Sql.GetSql("Fee.Interface.InsertInPatientUploadFeeDetail.1", ref strSql) == -1)
            {
                errInfo = "获得[Fee.Interface.InsertInPatientUploadFeeDetail.1]对应sql语句出错";
                return -1;
            }
            string[] memoList = obj.SIMainInfo.Memo.Split('|');


            string strBalanceNo = "";
            if (memoList != null && memoList.Length > 0)
            {
                strBalanceNo = memoList[0];
            }

            try
            {
                //strSql = string.Format(strSql, obj.SIMainInfo.RegNo,
                //    string.IsNullOrEmpty(obj.SIMainInfo.HosNo) ? Function.Function.HospitalCode : obj.SIMainInfo.HosNo,
                //    obj.IDCard,
                //    obj.PID.CardNO,
                //    obj.PVisit.InTime.ToString(),
                //    f.ChargeOper.OperTime.ToString(),//5
                //    f.SequenceNO,
                //    f.Item.UserCode,
                //    f.Item.Name,
                //    f.Item.MinFee.ID,
                //    f.Item.Specs,
                //    f.Item.SpecialFlag1,//剂型  11
                //    f.Item.SpecialPrice.ToString(),
                //    f.Item.Qty,
                //    f.FT.TotCost.ToString(),//14
                //    "",
                //    "",
                //    "",
                //    "",
                //    "",//19
                //    obj.PID.PatientNO,// 20 
                //    obj.ID,
                //    obj.SIMainInfo.InvoiceNo,
                //    this.Operator.ID,
                //    f.Item.MinFee.ID,//24
                //    strBalanceNo);
            }
            catch (Exception ex)
            {
                //this.ErrCode = ex.Message;
                errInfo = ex.Message;
                return -1;
            }

            try
            {
                return this.ucMgr.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                //this.ErrCode = ex.Message;
                errInfo = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 作废医保结算
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateBlanceSIPatient(string lsh, string regNo, ref string errInfo)
        {
            if (string.IsNullOrEmpty(lsh))
            {
                errInfo = "删除费用明细失败，患者流水号为空！";
                return -1;
            }

            if (string.IsNullOrEmpty(regNo))
            {
                errInfo = "删除费用明细失败，就诊登记号为空！";
                return -1;
            }
            string strSql = "";

            if (this.ucMgr.Sql.GetSql("Fee.Interface.UpdateBlanceSIInPatient", ref strSql) == -1)
            {
                errInfo = "获得[Fee.Interface.UpdateBlanceSIInPatient]对应sql语句出错";
                return -1;
            }
            strSql = string.Format(strSql, lsh, regNo);

            try
            {
                return this.ucMgr.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                //this.ErrCode = ex.Message;
                errInfo = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 插入结算后的信息
        /// </summary>
        /// <param name="balanceStr"></param>
        /// <returns></returns>
        public int SaveBlanceSIInPatient(FS.HISFC.Models.RADT.PatientInfo obj, ref string errInfo)
        {
            if (obj == null)
            {
                errInfo = "插入医保结算失败，患者信息为空！";
                return -1;
            }
            if (string.IsNullOrEmpty(obj.SIMainInfo.Memo))
            {
                errInfo = "获取医保结算信息失败！";
                return -1;
            }
            string[] balanceInfo = obj.SIMainInfo.Memo.Split('|');

            string strSql = "";
            //if (balanceInfo != null && balanceInfo.Length > 0)
            {
                string strBalanceNo = "";

                if (balanceInfo != null && balanceInfo.Length > 0)
                {
                    strBalanceNo = balanceInfo[0];
                }
                if (string.IsNullOrEmpty(strBalanceNo))
                {
                    errInfo = "插入医保结算失败，结算单号为空！";
                    return -1;
                }

                string delSQL = "delete from gzsi_his_fyjs where JSID = '{0}'";
                delSQL = string.Format(delSQL, strBalanceNo);
                this.ucMgr.ExecNoQuery(delSQL);


                if (this.ucMgr.Sql.GetSql("Fee.Interface.SaveBlanceSIInPatient.1", ref strSql) == -1)
                {
                    errInfo = "获得[Fee.Interface.SaveBlanceSIInPatient.1]对应sql语句出错";
                    return -1;
                }

                //strSql = string.Format(strSql, obj.ID,//住院流水号
                //    obj.SIMainInfo.InvoiceNo,//结算发票号
                //    obj.SIMainInfo.RegNo,//就医登记号
                //    obj.SIMainInfo.FeeTimes,//费用批次
                //    string.IsNullOrEmpty(obj.SIMainInfo.HosNo) ? Function.Function.HospitalCode : obj.SIMainInfo.HosNo,//医院编号
                //    obj.IDCard,//5身份证号
                //    obj.PID.PatientNO,// 门诊号/住院号
                //    obj.PVisit.InTime.ToString(),//入院日期
                //    obj.BalanceDate.ToString(),//结算日期
                //    obj.SIMainInfo.TotCost.ToString(),//总金额
                //    obj.SIMainInfo.PubCost.ToString(),//社保支付金额
                //    obj.SIMainInfo.PayCost.ToString(),// 11账户支付金额
                //    "0",//部分项目自付金额
                //    "0",//个人起付金额
                //    obj.SIMainInfo.OwnCost.ToString(),//14个人自费项目金额
                //    obj.SIMainInfo.PayCost.ToString(),//个人自付金额  15
                //    obj.SIMainInfo.PayCost.ToString(),//个人自负金额
                //    "0",//超统筹支付限额个人自付金额
                //    "",//自费原因
                //    "0",//19医药机构分单金额
                //    "",// 20 备注1,记录产生时间
                //    "",//备注2
                //    "",//备注3
                //    "",//读入标志
                //    this.Operator.ID,//操作员 24
                //    strBalanceNo)// 25
                    ;

            }
            //else
            //{
            //    this.Err = "结算信息不正确！";
            //    return -1;
            //}

            return this.ucMgr.ExecNoQuery(strSql);

        }


        /// <summary>
        /// 【异地】住院病人信息（病案首页）录入（0801） 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetYDInPatientUploadDRGSXML(DataTable dt, ref FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            this.GetHosCodeAndRegionCode(ref personInfo);
            #region 住院病人信息（病案首页）录入参数
            #region 患者参数

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//就诊登记号
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(personInfo.MCardNo) ? "-" : personInfo.MCardNo);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = personInfo.IdenNo;
            root.AppendChild(aac044);

            #endregion

            if (dt != null)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    XmlNode yzy001 = xml.CreateElement("yzy001"); //病案号
                    XmlNode yzy002 = xml.CreateElement("yzy002"); //住院次数
                    XmlNode yzy003 = xml.CreateElement("yzy003"); //ICD版本
                    XmlNode yzy004 = xml.CreateElement("yzy004"); //住院流水号
                    XmlNode akc023 = xml.CreateElement("akc023"); //年龄
                    XmlNode aac003 = xml.CreateElement("aac003"); //病人姓名
                    XmlNode aac004 = xml.CreateElement("aac004"); //性别编号
                    XmlNode yzy008 = xml.CreateElement("yzy008"); //性别
                    XmlNode aac006 = xml.CreateElement("aac006"); //出生日期
                    XmlNode yzy010 = xml.CreateElement("yzy010"); //出生地
                    XmlNode yzy011 = xml.CreateElement("yzy011"); //身份证号
                    XmlNode aac161 = xml.CreateElement("aac161"); //国籍编号
                    XmlNode yzy013 = xml.CreateElement("yzy013"); //国籍
                    XmlNode aac005 = xml.CreateElement("aac005"); //民族编号
                    XmlNode yzy015 = xml.CreateElement("yzy015"); //民族
                    XmlNode yzy016 = xml.CreateElement("yzy016"); //职业
                    XmlNode aac017 = xml.CreateElement("aac017"); //婚姻状况编号
                    XmlNode yzy018 = xml.CreateElement("yzy018"); //婚姻状况
                    XmlNode aab004 = xml.CreateElement("aab004"); //单位名称
                    XmlNode yzy020 = xml.CreateElement("yzy020"); //单位地址
                    XmlNode yzy021 = xml.CreateElement("yzy021"); //单位电话
                    XmlNode yzy022 = xml.CreateElement("yzy022"); //单位邮编
                    XmlNode aac010 = xml.CreateElement("aac010"); //户口地址
                    XmlNode yzy024 = xml.CreateElement("yzy024"); //户口邮编
                    XmlNode aae004 = xml.CreateElement("aae004"); //联系人
                    XmlNode yzy026 = xml.CreateElement("yzy026"); //与病人关系
                    XmlNode yzy027 = xml.CreateElement("yzy027"); //联系人地址
                    XmlNode yzy028 = xml.CreateElement("yzy028"); //联系人电话
                    XmlNode yzy029 = xml.CreateElement("yzy029"); //健康卡号
                    XmlNode ykc701 = xml.CreateElement("ykc701"); //入院日期
                    XmlNode yzy032 = xml.CreateElement("yzy032"); //入院统一科号
                    XmlNode yzy033 = xml.CreateElement("yzy033"); //入院科别
                    XmlNode yzy034 = xml.CreateElement("yzy034"); //入院病室
                    XmlNode ykc702 = xml.CreateElement("ykc702"); //出院日期
                    XmlNode yzy037 = xml.CreateElement("yzy037"); //出院统一科号
                    XmlNode yzy038 = xml.CreateElement("yzy038"); //出院科别
                    XmlNode yzy039 = xml.CreateElement("yzy039"); //出院病室
                    XmlNode akb063 = xml.CreateElement("akb063"); //实际住院天数
                    XmlNode akc193 = xml.CreateElement("akc193"); //门（急）诊诊断编码
                    XmlNode akc050 = xml.CreateElement("akc050"); //门（急）诊诊断疾病名
                    XmlNode yzy043 = xml.CreateElement("yzy043"); //门、急诊医生编号
                    XmlNode ake022 = xml.CreateElement("ake022"); //门、急诊医生
                    XmlNode yzy045 = xml.CreateElement("yzy045"); //病理诊断
                    XmlNode yzy046 = xml.CreateElement("yzy046"); //过敏药物
                    XmlNode yzy047 = xml.CreateElement("yzy047"); //抢救次数
                    XmlNode yzy048 = xml.CreateElement("yzy048"); //抢救成功次数
                    XmlNode yzy049 = xml.CreateElement("yzy049"); //科主任编号
                    XmlNode yzy050 = xml.CreateElement("yzy050"); //科主任
                    XmlNode yzy051 = xml.CreateElement("yzy051"); //主（副主）任医生编号
                    XmlNode yzy052 = xml.CreateElement("yzy052"); //主（副主）任医生
                    XmlNode yzy053 = xml.CreateElement("yzy053"); //主治医生编号
                    XmlNode yzy054 = xml.CreateElement("yzy054"); //主治医生
                    XmlNode yzy055 = xml.CreateElement("yzy055"); //住院医生编号
                    XmlNode yzy056 = xml.CreateElement("yzy056"); //住院医生
                    XmlNode yzy057 = xml.CreateElement("yzy057"); //进修医师编号
                    XmlNode yzy058 = xml.CreateElement("yzy058"); //进修医师
                    XmlNode yzy059 = xml.CreateElement("yzy059"); //实习医师编号
                    XmlNode yzy060 = xml.CreateElement("yzy060"); //实习医师
                    XmlNode yzy061 = xml.CreateElement("yzy061"); //编码员编号
                    XmlNode yzy062 = xml.CreateElement("yzy062"); //编码员
                    XmlNode yzy063 = xml.CreateElement("yzy063"); //病案质量编号
                    XmlNode yzy064 = xml.CreateElement("yzy064"); //病案质量
                    XmlNode yzy065 = xml.CreateElement("yzy065"); //质控医师编号
                    XmlNode yzy066 = xml.CreateElement("yzy066"); //质控医师
                    XmlNode yzy067 = xml.CreateElement("yzy067"); //质控护士编号
                    XmlNode yzy068 = xml.CreateElement("yzy068"); //质控护士
                    XmlNode yzy069 = xml.CreateElement("yzy069"); //质控日期
                    XmlNode akc264 = xml.CreateElement("akc264"); //总费用
                    XmlNode ake047 = xml.CreateElement("ake047"); //西药费
                    XmlNode yzy072 = xml.CreateElement("yzy072"); //中药费
                    XmlNode ake050 = xml.CreateElement("ake050"); //中成药费
                    XmlNode ake049 = xml.CreateElement("ake049"); //中草药费
                    XmlNode ake044 = xml.CreateElement("ake044"); //其他费
                    XmlNode yzy076 = xml.CreateElement("yzy076"); //是否尸检编号
                    XmlNode yzy077 = xml.CreateElement("yzy077"); //是否尸检
                    XmlNode yzy078 = xml.CreateElement("yzy078"); //血型编号
                    XmlNode yzy079 = xml.CreateElement("yzy079"); //血型
                    XmlNode yzy080 = xml.CreateElement("yzy080"); //RH编号
                    XmlNode yzy081 = xml.CreateElement("yzy081"); //RH
                    XmlNode yzy082 = xml.CreateElement("yzy082"); //首次转科统一科号
                    XmlNode yzy083 = xml.CreateElement("yzy083"); //首次转科科别
                    XmlNode yzy084 = xml.CreateElement("yzy084"); //首次转科日期
                    XmlNode yzy085 = xml.CreateElement("yzy085"); //首次转科时间
                    XmlNode yzy086 = xml.CreateElement("yzy086"); //疾病分型编号
                    XmlNode yzy087 = xml.CreateElement("yzy087"); //疾病分型
                    XmlNode yzy088 = xml.CreateElement("yzy088"); //籍贯
                    XmlNode yzy089 = xml.CreateElement("yzy089"); //现住址
                    XmlNode yzy090 = xml.CreateElement("yzy090"); //现电话
                    XmlNode yzy091 = xml.CreateElement("yzy091"); //现邮编
                    XmlNode aca111 = xml.CreateElement("aca111"); //职业编号
                    XmlNode yzy093 = xml.CreateElement("yzy093"); //新生儿出生体重
                    XmlNode yzy094 = xml.CreateElement("yzy094"); //新生儿入院体重
                    XmlNode yzy095 = xml.CreateElement("yzy095"); //入院途径编号
                    XmlNode yzy096 = xml.CreateElement("yzy096"); //入院途径
                    XmlNode yzy097 = xml.CreateElement("yzy097"); //临床路径病例编号
                    XmlNode yzy098 = xml.CreateElement("yzy098"); //临床路径病例
                    XmlNode yzy099 = xml.CreateElement("yzy099"); //病理疾病编码
                    XmlNode yzy100 = xml.CreateElement("yzy100"); //病理号
                    XmlNode yzy101 = xml.CreateElement("yzy101"); //是否药物过敏编号
                    XmlNode yzy102 = xml.CreateElement("yzy102"); //是否药物过敏
                    XmlNode yzy103 = xml.CreateElement("yzy103"); //责任护士编号
                    XmlNode yzy104 = xml.CreateElement("yzy104"); //责任护士
                    XmlNode yzy105 = xml.CreateElement("yzy105"); //离院方式编号
                    XmlNode yzy106 = xml.CreateElement("yzy106"); //离院方式
                    XmlNode yzy107 = xml.CreateElement("yzy107"); //离院方式为医嘱转院，拟接收医疗机构名称
                    XmlNode yzy108 = xml.CreateElement("yzy108"); //离院方式为转社区卫生服务器机构/乡镇卫生院，拟接收医疗机构名称
                    XmlNode yzy109 = xml.CreateElement("yzy109"); //是否有出院31天内再住院计划编号
                    XmlNode yzy110 = xml.CreateElement("yzy110"); //是否有出院31天内再住院计划
                    XmlNode yzy111 = xml.CreateElement("yzy111"); //再住院目的
                    XmlNode yzy112 = xml.CreateElement("yzy112"); //颅脑损伤患者昏迷时间：入院前 天
                    XmlNode yzy113 = xml.CreateElement("yzy113"); //颅脑损伤患者昏迷时间：入院前 小时
                    XmlNode yzy114 = xml.CreateElement("yzy114"); //颅脑损伤患者昏迷时间：入院前 分钟
                    XmlNode yzy115 = xml.CreateElement("yzy115"); //入院前昏迷总分钟
                    XmlNode yzy116 = xml.CreateElement("yzy116"); //颅脑损伤患者昏迷时间：入院后 天
                    XmlNode yzy117 = xml.CreateElement("yzy117"); //颅脑损伤患者昏迷时间：入院后 小时
                    XmlNode yzy118 = xml.CreateElement("yzy118"); //颅脑损伤患者昏迷时间：入院后 分钟
                    XmlNode yzy119 = xml.CreateElement("yzy119"); //入院后昏迷总分钟
                    XmlNode yzy120 = xml.CreateElement("yzy120"); //付款方式编号
                    XmlNode yzy121 = xml.CreateElement("yzy121"); //付款方式
                    XmlNode yzy122 = xml.CreateElement("yzy122"); //住院总费用：自费金额
                    XmlNode yzy123 = xml.CreateElement("yzy123"); //综合医疗服务类：（1）一般医疗服务费
                    XmlNode yzy124 = xml.CreateElement("yzy124"); //综合医疗服务类：（2）一般治疗操作费
                    XmlNode yzy125 = xml.CreateElement("yzy125"); //综合医疗服务类：（3）护理费
                    XmlNode yzy126 = xml.CreateElement("yzy126"); //综合医疗服务类：（4）其他费用
                    XmlNode yzy127 = xml.CreateElement("yzy127"); //诊断类：(5) 病理诊断费
                    XmlNode yzy128 = xml.CreateElement("yzy128"); //诊断类：(6) 实验室诊断费
                    XmlNode yzy129 = xml.CreateElement("yzy129"); //诊断类：(7) 影像学诊断费
                    XmlNode yzy130 = xml.CreateElement("yzy130"); //诊断类：(8) 临床诊断项目费
                    XmlNode yzy131 = xml.CreateElement("yzy131"); //治疗类：(9) 非手术治疗项目费
                    XmlNode yzy132 = xml.CreateElement("yzy132"); //治疗类：非手术治疗项目费 其中临床物理治疗费
                    XmlNode yzy133 = xml.CreateElement("yzy133"); //治疗类：(10) 手术治疗费
                    XmlNode yzy134 = xml.CreateElement("yzy134"); //治疗类：手术治疗费 其中麻醉费
                    XmlNode yzy135 = xml.CreateElement("yzy135"); //治疗类：手术治疗费 其中手术费
                    XmlNode yzy136 = xml.CreateElement("yzy136"); //康复类：(11) 康复费
                    XmlNode yzy137 = xml.CreateElement("yzy137"); //中医类：中医治疗类
                    XmlNode yzy138 = xml.CreateElement("yzy138"); //西药类： 西药费 其中抗菌药物费用
                    XmlNode yzy139 = xml.CreateElement("yzy139"); //血液和血液制品类： 血费
                    XmlNode yzy140 = xml.CreateElement("yzy140"); //血液和血液制品类： 白蛋白类制品费
                    XmlNode yzy141 = xml.CreateElement("yzy141"); //血液和血液制品类： 球蛋白制品费
                    XmlNode yzy142 = xml.CreateElement("yzy142"); //血液和血液制品类：凝血因子类制品费
                    XmlNode yzy143 = xml.CreateElement("yzy143"); //血液和血液制品类： 细胞因子类费
                    XmlNode yzy144 = xml.CreateElement("yzy144"); //耗材类：检查用一次性医用材料费
                    XmlNode yzy145 = xml.CreateElement("yzy145"); //耗材类：治疗用一次性医用材料费
                    XmlNode yzy146 = xml.CreateElement("yzy146"); //耗材类：手术用一次性医用材料费
                    XmlNode yzy147 = xml.CreateElement("yzy147"); //综合医疗服务类：一般医疗服务费 其中中医辨证论治费（中医）
                    XmlNode yzy148 = xml.CreateElement("yzy148"); //综合医疗服务类：一般医疗服务费 其中中医辨证论治会诊费（中医）
                    XmlNode yzy149 = xml.CreateElement("yzy149"); //中医类：诊断（中医）
                    XmlNode yzy150 = xml.CreateElement("yzy150"); //中医类：治疗（中医）
                    XmlNode yzy151 = xml.CreateElement("yzy151"); //中医类：治疗 其中外治（中医）
                    XmlNode yzy152 = xml.CreateElement("yzy152"); //中医类：治疗 其中骨伤（中医）
                    XmlNode yzy153 = xml.CreateElement("yzy153"); //中医类：治疗 其中针刺与灸法（中医）
                    XmlNode yzy154 = xml.CreateElement("yzy154"); //中医类：治疗推拿治疗（中医）
                    XmlNode yzy155 = xml.CreateElement("yzy155"); //中医类：治疗 其中肛肠治疗（中医）
                    XmlNode yzy156 = xml.CreateElement("yzy156"); //中医类：治疗 其中特殊治疗（中医）
                    XmlNode yzy157 = xml.CreateElement("yzy157"); //中医类：其他（中医）
                    XmlNode yzy158 = xml.CreateElement("yzy158"); //中医类：其他 其中中药特殊调配加工（中医）
                    XmlNode yzy159 = xml.CreateElement("yzy159"); //中医类：其他 其中辨证施膳（中医）
                    XmlNode yzy160 = xml.CreateElement("yzy160"); //中药类：中成药费 其中医疗机构中药制剂费（中医）
                    XmlNode yzy161 = xml.CreateElement("yzy161"); //治疗类别编号（中医类）
                    XmlNode yzy162 = xml.CreateElement("yzy162"); //治疗类别（中医类）
                    XmlNode yzy163 = xml.CreateElement("yzy163"); //门（急）诊中医诊断编码（中医类）
                    XmlNode yzy164 = xml.CreateElement("yzy164"); //门（急）诊中医诊断（中医类）
                    XmlNode yzy165 = xml.CreateElement("yzy165"); //实施临床路径编号（中医类）
                    XmlNode yzy166 = xml.CreateElement("yzy166"); //实施临床路径（中医类）
                    XmlNode yzy167 = xml.CreateElement("yzy167"); //使用医疗机构中药制剂编号（中医类）
                    XmlNode yzy168 = xml.CreateElement("yzy168"); //使用医疗机构中药制剂（中医类）
                    XmlNode yzy169 = xml.CreateElement("yzy169"); //使用中医诊疗设备编号（中医类）
                    XmlNode yzy170 = xml.CreateElement("yzy170"); //使用中医诊疗设备（中医类）
                    XmlNode yzy171 = xml.CreateElement("yzy171"); //使用中医诊疗技术编号（中医类）
                    XmlNode yzy172 = xml.CreateElement("yzy172"); //使用中医诊疗技术（中医类）
                    XmlNode yzy173 = xml.CreateElement("yzy173"); //辨证施护编号（中医类）
                    XmlNode yzy174 = xml.CreateElement("yzy174"); //辨证施护（中医类）

                    yzy001.InnerText = dRow["yzy001"].ToString();//病案号
                    yzy002.InnerText = dRow["yzy002"].ToString();//住院次数
                    yzy003.InnerText = dRow["yzy003"].ToString();//ICD版本
                    yzy004.InnerText = dRow["yzy004"].ToString();//住院流水号
                    akc023.InnerText = dRow["akc023"].ToString();//年龄
                    aac003.InnerText = dRow["aac003"].ToString();//病人姓名
                    aac004.InnerText = dRow["aac004"].ToString();//性别编号
                    yzy008.InnerText = dRow["yzy008"].ToString();//性别
                    aac006.InnerText = FS.FrameWork.Function.NConvert.ToDateTime(dRow["aac006"].ToString()).ToString("yyyyMMdd");//出生日期
                    yzy010.InnerText = dRow["yzy010"].ToString();//出生地
                    yzy011.InnerText = dRow["yzy011"].ToString();//身份证号
                    aac161.InnerText = dRow["aac161"].ToString();//国籍编号
                    yzy013.InnerText = dRow["yzy013"].ToString();//国籍
                    aac005.InnerText = dRow["aac005"].ToString();//民族编号
                    yzy015.InnerText = dRow["yzy015"].ToString();//民族
                    yzy016.InnerText = dRow["yzy016"].ToString();//职业
                    aac017.InnerText = dRow["aac017"].ToString();//婚姻状况编号
                    yzy018.InnerText = dRow["yzy018"].ToString();//婚姻状况
                    aab004.InnerText = dRow["aab004"].ToString();//单位名称
                    yzy020.InnerText = dRow["yzy020"].ToString();//单位地址
                    yzy021.InnerText = dRow["yzy021"].ToString();//单位电话
                    yzy022.InnerText = dRow["yzy022"].ToString();//单位邮编
                    aac010.InnerText = dRow["aac010"].ToString();//户口地址
                    yzy024.InnerText = dRow["yzy024"].ToString();//户口邮编
                    aae004.InnerText = dRow["aae004"].ToString();//联系人
                    yzy026.InnerText = dRow["yzy026"].ToString();//与病人关系
                    yzy027.InnerText = dRow["yzy027"].ToString();//联系人地址
                    yzy028.InnerText = dRow["yzy028"].ToString();//联系人电话
                    yzy029.InnerText = dRow["yzy029"].ToString();//健康卡号
                    ykc701.InnerText = FS.FrameWork.Function.NConvert.ToDateTime(dRow["ykc701"].ToString()).ToString("yyyyMMdd");//入院日期
                    yzy032.InnerText = dRow["yzy032"].ToString();//入院统一科号
                    yzy033.InnerText = dRow["yzy033"].ToString();//入院科别
                    yzy034.InnerText = dRow["yzy034"].ToString();//入院病室
                    ykc702.InnerText = FS.FrameWork.Function.NConvert.ToDateTime(dRow["ykc702"].ToString()).ToString("yyyyMMdd");//出院日期
                    yzy037.InnerText = dRow["yzy037"].ToString();//出院统一科号
                    yzy038.InnerText = dRow["yzy038"].ToString();//出院科别
                    yzy039.InnerText = dRow["yzy039"].ToString();//出院病室
                    akb063.InnerText = dRow["akb063"].ToString();//实际住院天数
                    akc193.InnerText = dRow["akc193"].ToString();//门（急）诊诊断编码
                    akc050.InnerText = dRow["akc050"].ToString();//门（急）诊诊断疾病名
                    yzy043.InnerText = dRow["yzy043"].ToString();//门、急诊医生编号
                    ake022.InnerText = dRow["ake022"].ToString();//门、急诊医生
                    yzy045.InnerText = dRow["yzy045"].ToString();//病理诊断
                    yzy046.InnerText = dRow["yzy046"].ToString();//过敏药物
                    yzy047.InnerText = dRow["yzy047"].ToString();//抢救次数
                    yzy048.InnerText = dRow["yzy048"].ToString();//抢救成功次数
                    yzy049.InnerText = dRow["yzy049"].ToString();//科主任编号
                    yzy050.InnerText = dRow["yzy050"].ToString();//科主任
                    yzy051.InnerText = dRow["yzy051"].ToString();//主（副主）任医生编号
                    yzy052.InnerText = dRow["yzy052"].ToString();//主（副主）任医生
                    yzy053.InnerText = dRow["yzy053"].ToString();//主治医生编号
                    yzy054.InnerText = dRow["yzy054"].ToString();//主治医生
                    yzy055.InnerText = dRow["yzy055"].ToString();//住院医生编号
                    yzy056.InnerText = dRow["yzy056"].ToString();//住院医生
                    yzy057.InnerText = dRow["yzy057"].ToString();//进修医师编号
                    yzy058.InnerText = dRow["yzy058"].ToString();//进修医师
                    yzy059.InnerText = dRow["yzy059"].ToString();//实习医师编号
                    yzy060.InnerText = dRow["yzy060"].ToString();//实习医师
                    yzy061.InnerText = dRow["yzy061"].ToString();//编码员编号
                    yzy062.InnerText = dRow["yzy062"].ToString();//编码员
                    yzy063.InnerText = dRow["yzy063"].ToString();//病案质量编号
                    yzy064.InnerText = dRow["yzy064"].ToString();//病案质量
                    yzy065.InnerText = dRow["yzy065"].ToString();//质控医师编号
                    yzy066.InnerText = dRow["yzy066"].ToString();//质控医师
                    yzy067.InnerText = dRow["yzy067"].ToString();//质控护士编号
                    yzy068.InnerText = dRow["yzy068"].ToString();//质控护士
                    yzy069.InnerText = FS.FrameWork.Function.NConvert.ToDateTime(dRow["yzy069"].ToString()).ToString("yyyyMMdd");//质控日期
                    akc264.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["akc264"].ToString()).ToString("F2");//总费用
                    ake047.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["ake047"].ToString()).ToString("F2");//西药费
                    yzy072.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy072"].ToString()).ToString("F2");//中药费
                    ake050.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["ake050"].ToString()).ToString("F2");//中成药费
                    ake049.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["ake049"].ToString()).ToString("F2");//中草药费
                    ake044.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["ake044"].ToString()).ToString("F2"); //其他费
                    yzy076.InnerText = dRow["yzy076"].ToString();//是否尸检编号
                    yzy077.InnerText = dRow["yzy077"].ToString();//是否尸检
                    yzy078.InnerText = dRow["yzy078"].ToString();//血型编号
                    yzy079.InnerText = dRow["yzy079"].ToString();//血型
                    yzy080.InnerText = dRow["yzy080"].ToString();//RH编号
                    yzy081.InnerText = dRow["yzy081"].ToString();//RH
                    yzy082.InnerText = dRow["yzy082"].ToString();//首次转科统一科号
                    yzy083.InnerText = dRow["yzy083"].ToString();//首次转科科别
                    yzy084.InnerText = string.IsNullOrEmpty(dRow["yzy084"].ToString())?"":FS.FrameWork.Function.NConvert.ToDateTime(dRow["yzy084"].ToString()).ToString("yyyyMMdd");//首次转科日期
                    yzy085.InnerText = dRow["yzy085"].ToString();//首次转科时间
                    yzy086.InnerText = dRow["yzy086"].ToString();//疾病分型编号
                    yzy087.InnerText = dRow["yzy087"].ToString();//疾病分型
                    yzy088.InnerText = dRow["yzy088"].ToString();//籍贯
                    yzy089.InnerText = dRow["yzy089"].ToString();//现住址
                    yzy090.InnerText = dRow["yzy090"].ToString();//现电话
                    yzy091.InnerText = dRow["yzy091"].ToString();//现邮编
                    aca111.InnerText = dRow["aca111"].ToString();//职业编号
                    yzy093.InnerText = dRow["yzy093"].ToString();//新生儿出生体重
                    yzy094.InnerText = dRow["yzy094"].ToString();//新生儿入院体重
                    yzy095.InnerText = dRow["yzy095"].ToString();//入院途径编号
                    yzy096.InnerText = dRow["yzy096"].ToString();//入院途径
                    yzy097.InnerText = dRow["yzy097"].ToString();//临床路径病例编号
                    yzy098.InnerText = dRow["yzy098"].ToString();//临床路径病例
                    yzy099.InnerText = dRow["yzy099"].ToString();//病理疾病编码
                    yzy100.InnerText = dRow["yzy100"].ToString();//病理号
                    yzy101.InnerText = dRow["yzy101"].ToString();//是否药物过敏编号
                    yzy102.InnerText = dRow["yzy102"].ToString();//是否药物过敏
                    yzy103.InnerText = dRow["yzy103"].ToString();//责任护士编号
                    yzy104.InnerText = dRow["yzy104"].ToString();//责任护士
                    yzy105.InnerText = dRow["yzy105"].ToString();//离院方式编号
                    yzy106.InnerText = dRow["yzy106"].ToString();//离院方式
                    yzy107.InnerText = dRow["yzy107"].ToString();//离院方式为医嘱转院，拟接收医疗机构名称
                    yzy108.InnerText = dRow["yzy108"].ToString();//离院方式为转社区卫生服务器机构/乡镇卫生院，拟接收医疗机构名称
                    yzy109.InnerText = dRow["yzy109"].ToString();//是否有出院31天内再住院计划编号
                    yzy110.InnerText = dRow["yzy110"].ToString();//是否有出院31天内再住院计划
                    yzy111.InnerText = dRow["yzy111"].ToString();//再住院目的
                    yzy112.InnerText = dRow["yzy112"].ToString();//颅脑损伤患者昏迷时间：入院前 天
                    yzy113.InnerText = dRow["yzy113"].ToString();//颅脑损伤患者昏迷时间：入院前 小时
                    yzy114.InnerText = dRow["yzy114"].ToString();//颅脑损伤患者昏迷时间：入院前 分钟
                    yzy115.InnerText = dRow["yzy115"].ToString();//入院前昏迷总分钟
                    yzy116.InnerText = dRow["yzy116"].ToString();//颅脑损伤患者昏迷时间：入院后 天
                    yzy117.InnerText = dRow["yzy117"].ToString();//颅脑损伤患者昏迷时间：入院后 小时
                    yzy118.InnerText = dRow["yzy118"].ToString();//颅脑损伤患者昏迷时间：入院后 分钟
                    yzy119.InnerText = dRow["yzy119"].ToString();//入院后昏迷总分钟
                    yzy120.InnerText = dRow["yzy120"].ToString();//付款方式编号
                    yzy121.InnerText = dRow["yzy121"].ToString();//付款方式

                    yzy122.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy122"].ToString()).ToString("F2");//住院总费用：自费金额
                    yzy123.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy123"].ToString()).ToString("F2");//综合医疗服务类：（1）一般医疗服务费
                    yzy124.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy124"].ToString()).ToString("F2");//综合医疗服务类：（2）一般治疗操作费
                    yzy125.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy125"].ToString()).ToString("F2");//综合医疗服务类：（3）护理费
                    yzy126.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy126"].ToString()).ToString("F2");//综合医疗服务类：（4）其他费用
                    yzy127.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy127"].ToString()).ToString("F2");//诊断类：(5) 病理诊断费
                    yzy128.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy128"].ToString()).ToString("F2");//诊断类：(6) 实验室诊断费
                    yzy129.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy129"].ToString()).ToString("F2");//诊断类：(7) 影像学诊断费
                    yzy130.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy130"].ToString()).ToString("F2");//诊断类：(8) 临床诊断项目费
                    yzy131.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy131"].ToString()).ToString("F2");//治疗类：(9) 非手术治疗项目费
                    yzy132.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy132"].ToString()).ToString("F2");//治疗类：非手术治疗项目费 其中临床物理治疗费
                    yzy133.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy133"].ToString()).ToString("F2");//治疗类：(10) 手术治疗费
                    yzy134.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy134"].ToString()).ToString("F2");//治疗类：手术治疗费 其中麻醉费
                    yzy135.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy135"].ToString()).ToString("F2");//治疗类：手术治疗费 其中手术费
                    yzy136.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy136"].ToString()).ToString("F2");//康复类：(11) 康复费
                    yzy137.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy137"].ToString()).ToString("F2");//中医类：中医治疗类
                    yzy138.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy138"].ToString()).ToString("F2");//西药类： 西药费 其中抗菌药物费用
                    yzy139.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy139"].ToString()).ToString("F2");//血液和血液制品类： 血费
                    yzy140.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy140"].ToString()).ToString("F2");//血液和血液制品类： 白蛋白类制品费
                    yzy141.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy141"].ToString()).ToString("F2");//血液和血液制品类： 球蛋白制品费
                    yzy142.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy142"].ToString()).ToString("F2");//血液和血液制品类：凝血因子类制品费
                    yzy143.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy143"].ToString()).ToString("F2");//血液和血液制品类： 细胞因子类费
                    yzy144.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy144"].ToString()).ToString("F2");//耗材类：检查用一次性医用材料费
                    yzy145.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy145"].ToString()).ToString("F2");//耗材类：治疗用一次性医用材料费
                    yzy146.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy146"].ToString()).ToString("F2");//耗材类：手术用一次性医用材料费
                    yzy147.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy147"].ToString()).ToString("F2");//综合医疗服务类：一般医疗服务费 其中中医辨证论治费（中医）
                    yzy148.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy148"].ToString()).ToString("F2");//综合医疗服务类：一般医疗服务费 其中中医辨证论治会诊费（中医）
                    yzy149.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy149"].ToString()).ToString("F2");//中医类：诊断（中医）
                    yzy150.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy150"].ToString()).ToString("F2");//中医类：治疗（中医）
                    yzy151.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy151"].ToString()).ToString("F2");//中医类：治疗 其中外治（中医）
                    yzy152.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy152"].ToString()).ToString("F2");//中医类：治疗 其中骨伤（中医）
                    yzy153.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy153"].ToString()).ToString("F2");//中医类：治疗 其中针刺与灸法（中医）
                    yzy154.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy154"].ToString()).ToString("F2");//中医类：治疗推拿治疗（中医）
                    yzy155.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy155"].ToString()).ToString("F2");//中医类：治疗 其中肛肠治疗（中医）
                    yzy156.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy156"].ToString()).ToString("F2");//中医类：治疗 其中特殊治疗（中医）
                    yzy157.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy157"].ToString()).ToString("F2");//中医类：其他（中医）
                    yzy158.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy158"].ToString()).ToString("F2");//中医类：其他 其中中药特殊调配加工（中医）
                    yzy159.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy159"].ToString()).ToString("F2");//中医类：其他 其中辨证施膳（中医）
                    yzy160.InnerText = FS.FrameWork.Function.NConvert.ToDecimal(dRow["yzy160"].ToString()).ToString("F2");//中药类：中成药费 其中医疗机构中药制剂费（中医）

                    yzy161.InnerText = dRow["yzy161"].ToString();//治疗类别编号（中医类）
                    yzy162.InnerText = dRow["yzy162"].ToString();//治疗类别（中医类）
                    yzy163.InnerText = dRow["yzy163"].ToString();//门（急）诊中医诊断编码（中医类）
                    yzy164.InnerText = dRow["yzy164"].ToString();//门（急）诊中医诊断（中医类）
                    yzy165.InnerText = dRow["yzy165"].ToString();//实施临床路径编号（中医类）
                    yzy166.InnerText = dRow["yzy166"].ToString();//实施临床路径（中医类）
                    yzy167.InnerText = dRow["yzy167"].ToString();//使用医疗机构中药制剂编号（中医类）
                    yzy168.InnerText = dRow["yzy168"].ToString();//使用医疗机构中药制剂（中医类）
                    yzy169.InnerText = dRow["yzy169"].ToString();//使用中医诊疗设备编号（中医类）
                    yzy170.InnerText = dRow["yzy170"].ToString();//使用中医诊疗设备（中医类）
                    yzy171.InnerText = dRow["yzy171"].ToString();//使用中医诊疗技术编号（中医类）
                    yzy172.InnerText = dRow["yzy172"].ToString();//使用中医诊疗技术（中医类）
                    yzy173.InnerText = dRow["yzy173"].ToString();//辨证施护编号（中医类）
                    yzy174.InnerText = dRow["yzy174"].ToString();//辨证施护（中医类）

                    root.AppendChild(yzy001);//病案号
                    root.AppendChild(yzy002);//住院次数
                    root.AppendChild(yzy003);//ICD版本
                    root.AppendChild(yzy004);//住院流水号
                    root.AppendChild(akc023);//年龄
                    root.AppendChild(aac003);//病人姓名
                    root.AppendChild(aac004);//性别编号
                    root.AppendChild(yzy008);//性别
                    root.AppendChild(aac006);//出生日期
                    root.AppendChild(yzy010);//出生地
                    root.AppendChild(yzy011);//身份证号
                    root.AppendChild(aac161);//国籍编号
                    root.AppendChild(yzy013);//国籍
                    root.AppendChild(aac005);//民族编号
                    root.AppendChild(yzy015);//民族
                    root.AppendChild(yzy016);//职业
                    root.AppendChild(aac017);//婚姻状况编号
                    root.AppendChild(yzy018);//婚姻状况
                    root.AppendChild(aab004);//单位名称
                    root.AppendChild(yzy020);//单位地址
                    root.AppendChild(yzy021);//单位电话
                    root.AppendChild(yzy022);//单位邮编
                    root.AppendChild(aac010);//户口地址
                    root.AppendChild(yzy024);//户口邮编
                    root.AppendChild(aae004);//联系人
                    root.AppendChild(yzy026);//与病人关系
                    root.AppendChild(yzy027);//联系人地址
                    root.AppendChild(yzy028);//联系人电话
                    root.AppendChild(yzy029);//健康卡号
                    root.AppendChild(ykc701);//入院日期
                    root.AppendChild(yzy032);//入院统一科号
                    root.AppendChild(yzy033);//入院科别
                    root.AppendChild(yzy034);//入院病室
                    root.AppendChild(ykc702);//出院日期
                    root.AppendChild(yzy037);//出院统一科号
                    root.AppendChild(yzy038);//出院科别
                    root.AppendChild(yzy039);//出院病室
                    root.AppendChild(akb063);//实际住院天数
                    root.AppendChild(akc193);//门（急）诊诊断编码
                    root.AppendChild(akc050);//门（急）诊诊断疾病名
                    root.AppendChild(yzy043);//门、急诊医生编号
                    root.AppendChild(ake022);//门、急诊医生
                    root.AppendChild(yzy045);//病理诊断
                    root.AppendChild(yzy046);//过敏药物
                    root.AppendChild(yzy047);//抢救次数
                    root.AppendChild(yzy048);//抢救成功次数
                    root.AppendChild(yzy049);//科主任编号
                    root.AppendChild(yzy050);//科主任
                    root.AppendChild(yzy051);//主（副主）任医生编号
                    root.AppendChild(yzy052);//主（副主）任医生
                    root.AppendChild(yzy053);//主治医生编号
                    root.AppendChild(yzy054);//主治医生
                    root.AppendChild(yzy055);//住院医生编号
                    root.AppendChild(yzy056);//住院医生
                    root.AppendChild(yzy057);//进修医师编号
                    root.AppendChild(yzy058);//进修医师
                    root.AppendChild(yzy059);//实习医师编号
                    root.AppendChild(yzy060);//实习医师
                    root.AppendChild(yzy061);//编码员编号
                    root.AppendChild(yzy062);//编码员
                    root.AppendChild(yzy063);//病案质量编号
                    root.AppendChild(yzy064);//病案质量
                    root.AppendChild(yzy065);//质控医师编号
                    root.AppendChild(yzy066);//质控医师
                    root.AppendChild(yzy067);//质控护士编号
                    root.AppendChild(yzy068);//质控护士
                    root.AppendChild(yzy069);//质控日期
                    root.AppendChild(akc264);//总费用
                    root.AppendChild(ake047);//西药费
                    root.AppendChild(yzy072);//中药费
                    root.AppendChild(ake050);//中成药费
                    root.AppendChild(ake049);//中草药费
                    root.AppendChild(ake044);//其他费
                    root.AppendChild(yzy076);//是否尸检编号
                    root.AppendChild(yzy077);//是否尸检
                    root.AppendChild(yzy078);//血型编号
                    root.AppendChild(yzy079);//血型
                    root.AppendChild(yzy080);//RH编号
                    root.AppendChild(yzy081);//RH
                    root.AppendChild(yzy082);//首次转科统一科号
                    root.AppendChild(yzy083);//首次转科科别
                    root.AppendChild(yzy084);//首次转科日期
                    root.AppendChild(yzy085);//首次转科时间
                    root.AppendChild(yzy086);//疾病分型编号
                    root.AppendChild(yzy087);//疾病分型
                    root.AppendChild(yzy088);//籍贯
                    root.AppendChild(yzy089);//现住址
                    root.AppendChild(yzy090);//现电话
                    root.AppendChild(yzy091);//现邮编
                    root.AppendChild(aca111);//职业编号
                    root.AppendChild(yzy093);//新生儿出生体重
                    root.AppendChild(yzy094);//新生儿入院体重
                    root.AppendChild(yzy095);//入院途径编号
                    root.AppendChild(yzy096);//入院途径
                    root.AppendChild(yzy097);//临床路径病例编号
                    root.AppendChild(yzy098);//临床路径病例
                    root.AppendChild(yzy099);//病理疾病编码
                    root.AppendChild(yzy100);//病理号
                    root.AppendChild(yzy101);//是否药物过敏编号
                    root.AppendChild(yzy102);//是否药物过敏
                    root.AppendChild(yzy103);//责任护士编号
                    root.AppendChild(yzy104);//责任护士
                    root.AppendChild(yzy105);//离院方式编号
                    root.AppendChild(yzy106);//离院方式
                    root.AppendChild(yzy107);//离院方式为医嘱转院，拟接收医疗机构名称
                    root.AppendChild(yzy108);//离院方式为转社区卫生服务器机构/乡镇卫生院，拟接收医疗机构名称
                    root.AppendChild(yzy109);//是否有出院31天内再住院计划编号
                    root.AppendChild(yzy110);//是否有出院31天内再住院计划
                    root.AppendChild(yzy111);//再住院目的
                    root.AppendChild(yzy112);//颅脑损伤患者昏迷时间：入院前 天
                    root.AppendChild(yzy113);//颅脑损伤患者昏迷时间：入院前 小时
                    root.AppendChild(yzy114);//颅脑损伤患者昏迷时间：入院前 分钟
                    root.AppendChild(yzy115);//入院前昏迷总分钟
                    root.AppendChild(yzy116);//颅脑损伤患者昏迷时间：入院后 天
                    root.AppendChild(yzy117);//颅脑损伤患者昏迷时间：入院后 小时
                    root.AppendChild(yzy118);//颅脑损伤患者昏迷时间：入院后 分钟
                    root.AppendChild(yzy119);//入院后昏迷总分钟
                    root.AppendChild(yzy120);//付款方式编号
                    root.AppendChild(yzy121);//付款方式
                    root.AppendChild(yzy122);//住院总费用：自费金额
                    root.AppendChild(yzy123);//综合医疗服务类：（1）一般医疗服务费
                    root.AppendChild(yzy124);//综合医疗服务类：（2）一般治疗操作费
                    root.AppendChild(yzy125);//综合医疗服务类：（3）护理费
                    root.AppendChild(yzy126);//综合医疗服务类：（4）其他费用
                    root.AppendChild(yzy127);//诊断类：(5) 病理诊断费
                    root.AppendChild(yzy128);//诊断类：(6) 实验室诊断费
                    root.AppendChild(yzy129);//诊断类：(7) 影像学诊断费
                    root.AppendChild(yzy130);//诊断类：(8) 临床诊断项目费
                    root.AppendChild(yzy131);//治疗类：(9) 非手术治疗项目费
                    root.AppendChild(yzy132);//治疗类：非手术治疗项目费 其中临床物理治疗费
                    root.AppendChild(yzy133);//治疗类：(10) 手术治疗费
                    root.AppendChild(yzy134);//治疗类：手术治疗费 其中麻醉费
                    root.AppendChild(yzy135);//治疗类：手术治疗费 其中手术费
                    root.AppendChild(yzy136);//康复类：(11) 康复费
                    root.AppendChild(yzy137);//中医类：中医治疗类
                    root.AppendChild(yzy138);//西药类： 西药费 其中抗菌药物费用
                    root.AppendChild(yzy139);//血液和血液制品类： 血费
                    root.AppendChild(yzy140);//血液和血液制品类： 白蛋白类制品费
                    root.AppendChild(yzy141);//血液和血液制品类： 球蛋白制品费
                    root.AppendChild(yzy142);//血液和血液制品类：凝血因子类制品费
                    root.AppendChild(yzy143);//血液和血液制品类： 细胞因子类费
                    root.AppendChild(yzy144);//耗材类：检查用一次性医用材料费
                    root.AppendChild(yzy145);//耗材类：治疗用一次性医用材料费
                    root.AppendChild(yzy146);//耗材类：手术用一次性医用材料费
                    root.AppendChild(yzy147);//综合医疗服务类：一般医疗服务费 其中中医辨证论治费（中医）
                    root.AppendChild(yzy148);//综合医疗服务类：一般医疗服务费 其中中医辨证论治会诊费（中医）
                    root.AppendChild(yzy149);//中医类：诊断（中医）
                    root.AppendChild(yzy150);//中医类：治疗（中医）
                    root.AppendChild(yzy151);//中医类：治疗 其中外治（中医）
                    root.AppendChild(yzy152);//中医类：治疗 其中骨伤（中医）
                    root.AppendChild(yzy153);//中医类：治疗 其中针刺与灸法（中医）
                    root.AppendChild(yzy154);//中医类：治疗推拿治疗（中医）
                    root.AppendChild(yzy155);//中医类：治疗 其中肛肠治疗（中医）
                    root.AppendChild(yzy156);//中医类：治疗 其中特殊治疗（中医）
                    root.AppendChild(yzy157);//中医类：其他（中医）
                    root.AppendChild(yzy158);//中医类：其他 其中中药特殊调配加工（中医）
                    root.AppendChild(yzy159);//中医类：其他 其中辨证施膳（中医）
                    root.AppendChild(yzy160);//中药类：中成药费 其中医疗机构中药制剂费（中医）
                    root.AppendChild(yzy161);//治疗类别编号（中医类）
                    root.AppendChild(yzy162);//治疗类别（中医类）
                    root.AppendChild(yzy163);//门（急）诊中医诊断编码（中医类）
                    root.AppendChild(yzy164);//门（急）诊中医诊断（中医类）
                    root.AppendChild(yzy165);//实施临床路径编号（中医类）
                    root.AppendChild(yzy166);//实施临床路径（中医类）
                    root.AppendChild(yzy167);//使用医疗机构中药制剂编号（中医类）
                    root.AppendChild(yzy168);//使用医疗机构中药制剂（中医类）
                    root.AppendChild(yzy169);//使用中医诊疗设备编号（中医类）
                    root.AppendChild(yzy170);//使用中医诊疗设备（中医类）
                    root.AppendChild(yzy171);//使用中医诊疗技术编号（中医类）
                    root.AppendChild(yzy172);//使用中医诊疗技术（中医类）
                    root.AppendChild(yzy173);//辨证施护编号（中医类）
                    root.AppendChild(yzy174);//辨证施护（中医类）
                }
            }
            #endregion

            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }
        
        /// <summary>
        /// 【异地】住院病人诊断信息（病案首页）录入（0802） 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetYDInPatientCaseDiagnoseXML(DataTable dt, ref FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            this.GetHosCodeAndRegionCode(ref personInfo);
            #region 诊断信息参数
            #region 患者参数

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//就诊登记号
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(personInfo.MCardNo) ? "-" : personInfo.MCardNo);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = personInfo.IdenNo;
            root.AppendChild(aac044);


            #endregion

            //ICD版本
            XmlElement yzy003 = xml.CreateElement("yzy003");//ICD版本

            string strYZY003 = "";
            XmlElement row1 = xml.CreateElement("detail");//明细列表");
            if (dt != null)
            {

                foreach (DataRow dRow in dt.Rows)
                {
                    XmlElement row2 = xml.CreateElement("row");//费用明细");

                    XmlNode yzy201 = xml.CreateElement("yzy201");//排序
                    XmlNode yzy202 = xml.CreateElement("yzy202");//诊断类型
                    XmlNode akc185 = xml.CreateElement("akc185");//疾病名称
                    XmlNode akc196 = xml.CreateElement("akc196");//ICD码
                    XmlNode yzy205 = xml.CreateElement("yzy205");//入院病情编号
                    XmlNode yzy206 = xml.CreateElement("yzy206");//入院病情
                    strYZY003 = dRow["yzy003"].ToString();//ICD版本
                    yzy201.InnerText = dRow["yzy201"].ToString();//排序
                    yzy202.InnerText = dRow["yzy202"].ToString();//诊断类型
                    akc185.InnerText = dRow["akc185"].ToString();//疾病名称
                    akc196.InnerText = dRow["akc196"].ToString();//ICD码
                    yzy205.InnerText = dRow["yzy205"].ToString();//入院病情编号
                    yzy206.InnerText = dRow["yzy206"].ToString();//入院病情

                    row2.AppendChild(yzy201);//排序
                    row2.AppendChild(yzy202);//诊断类型
                    row2.AppendChild(akc185);//疾病名称
                    row2.AppendChild(akc196);//ICD码
                    row2.AppendChild(yzy205);//入院病情编号
                    row2.AppendChild(yzy206);//入院病情

                    row1.AppendChild(row2);
                }
            }
            #endregion

            yzy003.InnerText = strYZY003;
            root.AppendChild(yzy003);
            root.AppendChild(row1);
            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }
        /// <summary>
        /// 【异地】住院病人手术信息（病案首页）录入（0803） 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetYDInPatientOperationDetailXML(DataTable dt, ref FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            this.GetHosCodeAndRegionCode(ref personInfo);
            #region 手术信息参数
            #region 患者参数

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//就诊登记号
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(personInfo.MCardNo) ? "-" : personInfo.MCardNo);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = personInfo.IdenNo;
            root.AppendChild(aac044);

            #endregion
            XmlElement row1 = xml.CreateElement("detail");//明细列表");
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dRow in dt.Rows)
                {
                    XmlElement row2 = xml.CreateElement("row");//明细");

                    XmlNode yzy201 = xml.CreateElement("yzy201");//排序
                    XmlNode yzy207 = xml.CreateElement("yzy207");//手术码
                    XmlNode yzy208 = xml.CreateElement("yzy208");//手术码对应名称
                    XmlNode yzy209 = xml.CreateElement("yzy209");//手术日期
                    XmlNode yzy210 = xml.CreateElement("yzy210");//切口编号
                    XmlNode yzy211 = xml.CreateElement("yzy211");//切口
                    XmlNode yzy212 = xml.CreateElement("yzy212");//愈合编号
                    XmlNode yzy213 = xml.CreateElement("yzy213");//愈合
                    XmlNode yzy214 = xml.CreateElement("yzy214");//手术医生编号
                    XmlNode yzy215 = xml.CreateElement("yzy215");//手术医生
                    XmlNode yzy216 = xml.CreateElement("yzy216");//麻醉方式编号
                    XmlNode yzy217 = xml.CreateElement("yzy217");//麻醉方式
                    XmlNode yzy218 = xml.CreateElement("yzy218");//是否附加手术
                    XmlNode yzy219 = xml.CreateElement("yzy219");//I助编号
                    XmlNode yzy220 = xml.CreateElement("yzy220");//I助姓名
                    XmlNode yzy221 = xml.CreateElement("yzy221");//II助编号
                    XmlNode yzy222 = xml.CreateElement("yzy222");//II助姓名
                    XmlNode yzy223 = xml.CreateElement("yzy223");//麻醉医生编号
                    XmlNode yzy224 = xml.CreateElement("yzy224");//麻醉医生
                    XmlNode yzy225 = xml.CreateElement("yzy225");//择期手术编号
                    XmlNode yzy226 = xml.CreateElement("yzy226");//择期手术
                    XmlNode yzy227 = xml.CreateElement("yzy227");//手术级别编号
                    XmlNode yzy228 = xml.CreateElement("yzy228");//手术级别

                    yzy201.InnerText = dRow["yzy201"].ToString();//排序
                    yzy207.InnerText = dRow["yzy207"].ToString();//手术码
                    yzy208.InnerText = dRow["yzy208"].ToString();//手术码对应名称
                    yzy209.InnerText = FS.FrameWork.Function.NConvert.ToDateTime(dRow["yzy209"].ToString()).ToString("yyyyMMdd");//手术日期
                    yzy210.InnerText = dRow["yzy210"].ToString();//切口编号
                    yzy211.InnerText = dRow["yzy211"].ToString();//切口
                    yzy212.InnerText = dRow["yzy212"].ToString();//愈合编号
                    yzy213.InnerText = dRow["yzy213"].ToString();//愈合
                    yzy214.InnerText = dRow["yzy214"].ToString();//手术医生编号
                    yzy215.InnerText = dRow["yzy215"].ToString();//手术医生
                    yzy216.InnerText = dRow["yzy216"].ToString();//麻醉方式编号
                    yzy217.InnerText = dRow["yzy217"].ToString();//麻醉方式
                    yzy218.InnerText = FS.FrameWork.Function.NConvert.ToInt32(dRow["yzy218"].ToString()).ToString();//是否附加手术
                    yzy219.InnerText = string.IsNullOrEmpty(dRow["yzy219"].ToString()) ? "-" : dRow["yzy219"].ToString();//I助编号
                    yzy220.InnerText = string.IsNullOrEmpty(dRow["yzy220"].ToString()) ? "-" : dRow["yzy220"].ToString();//I助姓名
                    yzy221.InnerText = string.IsNullOrEmpty(dRow["yzy221"].ToString()) ? "-" : dRow["yzy221"].ToString();//II助编号
                    yzy222.InnerText = string.IsNullOrEmpty(dRow["yzy222"].ToString()) ? "-" : dRow["yzy222"].ToString();//II助姓名
                    yzy223.InnerText = string.IsNullOrEmpty(dRow["yzy223"].ToString()) ? "-" : dRow["yzy223"].ToString();//麻醉医生编号
                    yzy224.InnerText = string.IsNullOrEmpty(dRow["yzy224"].ToString()) ? "-" : dRow["yzy224"].ToString();//麻醉医生
                    yzy225.InnerText = dRow["yzy225"].ToString();//择期手术编号
                    yzy226.InnerText = dRow["yzy226"].ToString();//择期手术
                    yzy227.InnerText = dRow["yzy227"].ToString();//手术级别编号
                    yzy228.InnerText = dRow["yzy228"].ToString();//手术级别

                    row2.AppendChild(yzy201);//排序
                    row2.AppendChild(yzy207);//手术码
                    row2.AppendChild(yzy208);//手术码对应名称
                    row2.AppendChild(yzy209);//手术日期
                    row2.AppendChild(yzy210);//切口编号
                    row2.AppendChild(yzy211);//切口
                    row2.AppendChild(yzy212);//愈合编号
                    row2.AppendChild(yzy213);//愈合
                    row2.AppendChild(yzy214);//手术医生编号
                    row2.AppendChild(yzy215);//手术医生
                    row2.AppendChild(yzy216);//麻醉方式编号
                    row2.AppendChild(yzy217);//麻醉方式
                    row2.AppendChild(yzy218);//是否附加手术
                    row2.AppendChild(yzy219);//I助编号
                    row2.AppendChild(yzy220);//I助姓名
                    row2.AppendChild(yzy221);//II助编号
                    row2.AppendChild(yzy222);//II助姓名
                    row2.AppendChild(yzy223);//麻醉医生编号
                    row2.AppendChild(yzy224);//麻醉医生
                    row2.AppendChild(yzy225);//择期手术编号
                    row2.AppendChild(yzy226);//择期手术
                    row2.AppendChild(yzy227);//手术级别编号
                    row2.AppendChild(yzy228);//手术级别

                    row1.AppendChild(row2);
                }
            }
            #endregion
            root.AppendChild(row1);
            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// 【异地】住院病人产科分娩婴儿信息（病案首页）录入（0804） 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetYDInPatientBabyInfoXML(DataTable dt, ref FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            this.GetHosCodeAndRegionCode(ref personInfo);
            #region 住院病人产科分娩婴儿信息参数
            #region 患者参数

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//就诊登记号
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(personInfo.MCardNo) ? "-" : personInfo.MCardNo);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = personInfo.IdenNo;
            root.AppendChild(aac044);

            #endregion
            XmlElement row1 = xml.CreateElement("detail");//明细列表");

            if (dt != null)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    XmlElement row2 = xml.CreateElement("row");//费用明细");


                    XmlNode yzy201 = xml.CreateElement("yzy201");//排序
                    XmlNode aac004 = xml.CreateElement("aac004");//婴儿性别编号
                    XmlNode yzy230 = xml.CreateElement("yzy230");//婴儿性别
                    XmlNode yzy231 = xml.CreateElement("yzy231");//婴儿体重
                    XmlNode yzy232 = xml.CreateElement("yzy232");//分娩结果编号
                    XmlNode yzy233 = xml.CreateElement("yzy233");//分娩结果
                    XmlNode yzy234 = xml.CreateElement("yzy234");//转归编号
                    XmlNode yzy235 = xml.CreateElement("yzy235");//转归
                    XmlNode yzy236 = xml.CreateElement("yzy236");//婴儿抢救成功次数
                    XmlNode yzy237 = xml.CreateElement("yzy237");//呼吸编号
                    XmlNode yzy238 = xml.CreateElement("yzy238");//呼吸

                    yzy201.InnerText = dRow["yzy201"].ToString();//排序
                    aac004.InnerText = dRow["aac004"].ToString();//婴儿性别编号
                    yzy230.InnerText = dRow["yzy230"].ToString();//婴儿性别
                    yzy231.InnerText = dRow["yzy231"].ToString();//婴儿体重
                    yzy232.InnerText = dRow["yzy232"].ToString();//分娩结果编号
                    yzy233.InnerText = dRow["yzy233"].ToString();//分娩结果
                    yzy234.InnerText = dRow["yzy234"].ToString();//转归编号
                    yzy235.InnerText = dRow["yzy235"].ToString();//转归
                    yzy236.InnerText = dRow["yzy236"].ToString();//婴儿抢救成功次数
                    yzy237.InnerText = dRow["yzy237"].ToString();//呼吸编号
                    yzy238.InnerText = dRow["yzy238"].ToString();//呼吸

                    row2.AppendChild(yzy201);//排序
                    row2.AppendChild(aac004);//婴儿性别编号
                    row2.AppendChild(yzy230);//婴儿性别
                    row2.AppendChild(yzy231);//婴儿体重
                    row2.AppendChild(yzy232);//分娩结果编号
                    row2.AppendChild(yzy233);//分娩结果
                    row2.AppendChild(yzy234);//转归编号
                    row2.AppendChild(yzy235);//转归
                    row2.AppendChild(yzy236);//婴儿抢救成功次数
                    row2.AppendChild(yzy237);//呼吸编号
                    row2.AppendChild(yzy238);//呼吸

                    row1.AppendChild(row2);
                }
            }

            #endregion
            root.AppendChild(row1);
            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }



        /// <summary>
        /// 【异地】出院小结（出院记录）录入（0806） 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetYDInPatientOutRecordXML(FS.HISFC.Models.Base.Const obj, ref FS.HISFC.Models.RADT.PatientInfo patientInfo, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GBK", "yes"));
            XmlElement root = xml.CreateElement("input");
            xml.AppendChild(root);

            this.GetHosCodeAndRegionCode(ref personInfo);
            #region 出院小结参数
            #region 患者参数

            //行政区划代码（就医地）
            XmlElement node2 = xml.CreateElement("aab299");//行政区划代码（就医地）");
            node2.InnerText = personInfo.HospitalizeAreaCode;
            root.AppendChild(node2);

            //就医地社保分支机构代码
            XmlElement node3 = xml.CreateElement("yab600");//就医地社保分支机构代码");
            node3.InnerText = "";// personInfo.HospitalizeCenterAreaCode;
            root.AppendChild(node3);

            //医疗机构执业许可证登记号
            XmlElement akb026 = xml.CreateElement("akb026");//医疗机构执业许可证登记号
            akb026.InnerText = personInfo.HospitalCode;
            root.AppendChild(akb026);

            //医疗服务机构名称
            XmlElement akb021 = xml.CreateElement("akb021");//医疗服务机构名称
            akb021.InnerText = personInfo.HospitalName;
            root.AppendChild(akb021);

            //就诊登记号
            XmlElement ykc700 = xml.CreateElement("ykc700");//就诊登记号
            ykc700.InnerText = personInfo.ClinicNo;
            root.AppendChild(ykc700);

            //行政区划代码（参保地）
            XmlElement aab301 = xml.CreateElement("aab301");//行政区划代码（参保地）
            aab301.InnerText = personInfo.InsuredAreaCode;
            root.AppendChild(aab301);

            //参保地社保分支机构代码
            XmlElement yab060 = xml.CreateElement("yab060");//参保地社保分支机构代码
            yab060.InnerText = personInfo.InsuredCenterAreaCode;
            root.AppendChild(yab060);


            //社会保障号码
            XmlElement aac002 = xml.CreateElement("aac002");//社会保障号码
            aac002.InnerText = (string.IsNullOrEmpty(personInfo.MCardNo) ? "-" : personInfo.MCardNo);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = personInfo.IdenNo;
            root.AppendChild(aac044);

            #endregion

            if (obj != null)
            {
                XmlNode yzy301 = xml.CreateElement("yzy301");//入院诊断描述
                XmlNode yzy302 = xml.CreateElement("yzy302");//出院诊断描述
                XmlNode yzy303 = xml.CreateElement("yzy303");//入院时主要症状，体征
                XmlNode yzy304 = xml.CreateElement("yzy304");//入院后处理及经过（包含实验室和其他特殊检查）
                XmlNode yzy305 = xml.CreateElement("yzy305");//出院时情况
                XmlNode yzy306 = xml.CreateElement("yzy306");//出院时医嘱
                XmlNode akc273 = xml.CreateElement("akc273");//医师

                yzy301.InnerText = obj.OperEnvironment.ID;//入院诊断描述
                yzy302.InnerText = obj.OperEnvironment.Name;//出院诊断描述
                yzy303.InnerText = obj.ID;//入院时主要症状，体征
                yzy304.InnerText = obj.Name;//入院后处理及经过（包含实验室和其他特殊检查）
                yzy305.InnerText = obj.UserCode;//出院时情况
                yzy306.InnerText = obj.Memo;//出院时医嘱
                akc273.InnerText = obj.SpellCode;//医师

                root.AppendChild(yzy301);//入院诊断描述
                root.AppendChild(yzy302);//出院诊断描述
                root.AppendChild(yzy303);//入院时主要症状，体征
                root.AppendChild(yzy304);//入院后处理及经过（包含实验室和其他特殊检查）
                root.AppendChild(yzy305);//出院时情况
                root.AppendChild(yzy306);//出院时医嘱
                root.AppendChild(akc273);//医师
            }
            

            #endregion

            xml.AppendChild(root);
            return xml.InnerXml.ToString();
        }



    }
}
