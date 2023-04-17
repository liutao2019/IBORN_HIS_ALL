using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Windows.Forms;

namespace ProvinceAcrossSI
{
    class Funtion
    {

        public ProvinceAcrossSI.ProvinceAcrossSIDatabase SIMgr = new ProvinceAcrossSIDatabase();

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
        /// 日志记录实体
        /// </summary>
        Log lg = new Log();


        /// <summary>
        /// 医保的本地HIS项目编码与医保中的项目编码对照(合同单位为14)//合同单位需按实际修改
        /// </summary>
        Hashtable hsCompareItems = new Hashtable();

        ProvinceAcrossSI.WebReference.Service1 myWebService = new ProvinceAcrossSI.WebReference.Service1();

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
                return;
            }
            Outxml = obj.ToString();
            // {855F4C4E-041E-467d-8757-20B1EF9F7406}
            if (TransNo == "28" || TransNo == "0301")//费用明细不插入记录
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
        public int InsertFoShanYDLOG(string inpatientNo, string transNo, string remark, string inXML, string outXML)
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
            sql = string.Format(sql, inpatientNo, "2", transNo, remark, inXML, outXML, operCode);
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
        public void GetHosCodeAndRegionCode(ref ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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
        private void SetYdInPatientAccreditationInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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
        public string SetYdInPatientAccreditationXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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
            node6.InnerText = patientInfo.SSN;// (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(node6);

            //证件类型
            XmlElement node12 = xml.CreateElement("aac043");//证件类型
            node12.InnerText = "90";//personInfo.ZJLX; 
            root.AppendChild(node12);

            //证件号码
            XmlElement node20 = xml.CreateElement("aac044");//证件号码,4.0修订省平台验证以证件号码为主
            node20.InnerText = patientInfo.IDCard;//patientInfo.SSN; 
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
        public void GetYdInPatientAccreditationResult(string retXML, FS.HISFC.Models.RADT.PatientInfo patientInfo, ref ProvinceAcrossSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
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

            XmlNode aab301 = doc.SelectSingleNode("result/output/aab301");//行政区划代码（参保地）
            if (aab301 != null)
            {
                personInfo.InsuredAreaCode = aab301.InnerText;
            }
            else
            {
                personInfo.InsuredAreaCode = "";
            }

            XmlNode yab060 = doc.SelectSingleNode("result/output/yab060");//参保地社保分支机构代码
            if (yab060 != null)
            {
                personInfo.InsuredCenterAreaCode = yab060.InnerText;
            }
            else
            {
                personInfo.InsuredCenterAreaCode = "";
            }


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
                personInfo.ZJLX = "90";
            }
            else
            {
                personInfo.MCardNo = "";
                personInfo.IdenNo = "";
            }

            XmlNode IdenNoNode2 = doc.SelectSingleNode("result/output/aac044");//证件号码
            if (IdenNoNode2 != null && IdenNoNode2.InnerText != patientInfo.SSN)
            {
                personInfo.MCardNo = IdenNoNode.InnerText;
                patientInfo.SSN = IdenNoNode.InnerText;
            }
            else
            {
                personInfo.MCardNo = "";
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

            XmlNode PersonTypeNode = doc.SelectSingleNode("result/output/ykc021");//人员类别
            if (PersonTypeNode != null)
            {
                personInfo.PersonType = PersonTypeNode.InnerText;
            }
            else
            {
                personInfo.PersonType = "";
            }

            XmlNode RQtypeNode = doc.SelectSingleNode("result/output/ykc300");//人群类别
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
        public string SetYdInPatientRegXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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
            aac002.InnerText = patientInfo.SSN;// (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(aac002);

            //证件类型
            XmlElement aac043 = xml.CreateElement("aac043");//证件类型
            aac043.InnerText = "90";// personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = patientInfo.SSN; //patientInfo.IDCard;
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
        public void GetYdInPatientRegResult(string retXML, FS.HISFC.Models.RADT.PatientInfo patientInfo, ref ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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
        public string[] SetYDInpatientFeeDetialXML3(ProvinceAcrossSI.Objects.SIPersonInfo personInfo, ArrayList al)
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
        public string[] SetYDInpatientFeeDetialXML4(ProvinceAcrossSI.Objects.SIPersonInfo personInfo, ArrayList al)
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

            this.GetHosCodeAndRegionCode(ref personInfo);
            int maxTime = (int)Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(al.Count / 100));

            string[] arr = new string[maxTime + 1];
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
        public string SetYDInpatientFeeBalanceXML(ProvinceAcrossSI.Objects.SIPersonInfo personInfo, FS.HISFC.Models.RADT.PatientInfo patient)
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

            //就诊登记号
            XmlElement aae072 = xml.CreateElement("aae072");//单据号
            aae072.InnerText = "1111";
            root.AppendChild(aae072);

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

            //医疗类别
            XmlElement aka130 = xml.CreateElement("aka130");//医疗类别
            aka130.InnerText = personInfo.SeeDocType;
            root.AppendChild(aka130);


            ////证件号码
            //XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            //aac043.InnerText = personInfo.ZJLX;
            //root.AppendChild(aac043);

            ////证件号码
            //XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            //aac044.InnerText = personInfo.IdenNo;
            //root.AppendChild(aac044);

            ////结算类型
            //XmlElement ykc675 = xml.CreateElement("ykc675");//结算类型
            //ykc675.InnerText = "1";
            //root.AppendChild(ykc675);

            XmlNode tot_cost = xml.CreateElement("akc264");//费用总额");
            tot_cost.InnerText = patient.SIMainInfo.TotCost.ToString();
            root.AppendChild(tot_cost);

            XmlNode ake171 = xml.CreateElement("ake171");//统筹基金支付范围内费用");
            ake171.InnerText = "0";
            root.AppendChild(ake171);

            XmlNode akc018 = xml.CreateElement("akc018");//统筹基金支付范围内费用-其中甲类费用合计");
            akc018.InnerText = "0";
            root.AppendChild(akc018);

            XmlNode akc232 = xml.CreateElement("akc232");//乙类先自付费用-其中药品费用合计);
            akc232.InnerText = "0";
            root.AppendChild(akc232);

            XmlNode akc228 = xml.CreateElement("akc228");//乙类先自付费用合计);
            akc228.InnerText = "0";
            root.AppendChild(akc228);

            XmlNode akc233 = xml.CreateElement("akc233");//乙类先自付费用-其中诊疗费用合计);
            akc233.InnerText = "0";
            root.AppendChild(akc233);

            XmlNode akc234 = xml.CreateElement("akc234");//乙类先自付费用-其中材料费用合计);
            akc234.InnerText = "0";
            root.AppendChild(akc234);

            XmlNode akc235 = xml.CreateElement("akc235");//乙类先自付费用-其中其他费用合计);
            akc235.InnerText = "0";
            root.AppendChild(akc235);

            XmlNode akc253 = xml.CreateElement("akc253");//个人自费费用);
            akc253.InnerText = "0";
            root.AppendChild(akc253);

            XmlNode akc268 = xml.CreateElement("akc268");//超限价自付费用);
            akc268.InnerText = "0";
            root.AppendChild(akc268);

            XmlNode akc236 = xml.CreateElement("akc236");//超限价自付费用-其中床位费费用);
            akc236.InnerText = "0";
            root.AppendChild(akc236);

            XmlNode ake170 = xml.CreateElement("ake170");//中途结算标志);
            ake170.InnerText = "0";
            root.AppendChild(ake170);

            XmlNode ake122 = xml.CreateElement("ake122");//账户使用标志);
            ake122.InnerText = "0";
            root.AppendChild(ake122);

            XmlNode aae554 = xml.CreateElement("aae554");//TAC);
            aae554.InnerText = "";
            root.AppendChild(aae554);

            XmlNode operater = xml.CreateElement("aae011");//经办人");
            operater.InnerText = FS.FrameWork.Management.Connection.Operator.Name;
            root.AppendChild(operater);

            //XmlNode PatientNO = xml.CreateElement("yzz021");//医院结算业务序列号");//佛山：医院发票号
            //PatientNO.InnerText = patient.ID;//后面需要整合
            //root.AppendChild(PatientNO);

            //XmlNode oper_date = xml.CreateElement("aae036");//经办时间");
            //oper_date.InnerText = DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
            //root.AppendChild(oper_date);


            //XmlNode balance_date = xml.CreateElement("akc194");//结算时间");
            //balance_date.InnerText = DateTime.Now.ToString("yyyyMMdd");//yyyyMMdd");
            //root.AppendChild(balance_date);

            //XmlNode Memo = xml.CreateElement("aae013");//备注");
            //Memo.InnerText = personInfo.Memo;
            //root.AppendChild(Memo);

            XmlNode akc063 = xml.CreateElement("akc063");//输入附加信息1");
            akc063.InnerText = "";
            root.AppendChild(akc063);

            XmlNode akc064 = xml.CreateElement("akc064");//输入附加信息2");
            akc064.InnerText = "";
            root.AppendChild(akc064);

            XmlNode akc065 = xml.CreateElement("akc065");//输入附加信息3");
            akc065.InnerText = "";
            root.AppendChild(akc065);


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

        public void GetYdInPatientBalanceResult(string retXML, FS.HISFC.Models.RADT.PatientInfo patientInfo,
            ref ProvinceAcrossSI.Objects.SIPersonInfo personInfo, ref ArrayList list_SIJiJinclass)
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

            #region
            XmlNode transid = doc.SelectSingleNode("result/transid");
            if (transid != null)
            {
                personInfo.balanceTransID = transid.InnerText;
            }
            else
            {
                personInfo.balanceTransID = "";
            }

            XmlNode JSYWH = doc.SelectSingleNode("result/output/aaz216");
            if (JSYWH != null)
            {
                personInfo.JSYWH = JSYWH.InnerText;
            }
            else
            {
                personInfo.JSYWH = "";
            }

            XmlNode akc194 = doc.SelectSingleNode("result/output/akc194");
            if (akc194 != null)
            {
                personInfo.balance_date = akc194.InnerText;
            }
            else
            {
                personInfo.balance_date = "";
            }

            XmlNode ykc706 = doc.SelectSingleNode("result/output/ykc706");
            if (ykc706 != null)
            {
                personInfo.dzrq_date = ykc706.InnerText;
            }
            else
            {
                personInfo.dzrq_date = "";
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

            XmlNode ake149 = doc.SelectSingleNode("result/output/ake149");
            if (ake149 != null)
            {
                personInfo.ake149 = FS.FrameWork.Function.NConvert.ToDecimal(ake149.InnerText);
            }
            else
            {
                personInfo.ake149 = 0;
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

            XmlNode GWYBZ_cost = doc.SelectSingleNode("result/output/ake035");
            if (GWYBZ_cost != null)
            {
                personInfo.GWYBZ_cost = FS.FrameWork.Function.NConvert.ToDecimal(GWYBZ_cost.InnerText);
            }
            else
            {
                personInfo.GWYBZ_cost = 0;
            }

            XmlNode ake026 = doc.SelectSingleNode("result/output/ake026");
            if (ake026 != null)
            {
                personInfo.ake026 = FS.FrameWork.Function.NConvert.ToDecimal(ake026.InnerText);
            }
            else
            {
                personInfo.ake026 = 0;
            }

            XmlNode aae045 = doc.SelectSingleNode("result/output/aae045");
            if (aae045 != null)
            {
                personInfo.DBYLTCZHIF_cost = FS.FrameWork.Function.NConvert.ToDecimal(aae045.InnerText);
            }
            else
            {
                personInfo.DBYLTCZHIF_cost = 0;
            }

            XmlNode ake032 = doc.SelectSingleNode("result/output/ake032");
            if (ake032 != null)
            {
                personInfo.ake032 = FS.FrameWork.Function.NConvert.ToDecimal(ake032.InnerText);
            }
            else
            {
                personInfo.ake032 = 0;
            }

            XmlNode ake181 = doc.SelectSingleNode("result/output/ake181");
            if (ake181 != null)
            {
                personInfo.ake181 = FS.FrameWork.Function.NConvert.ToDecimal(ake181.InnerText);
            }
            else
            {
                personInfo.ake181 = 0;
            }

            XmlNode ake173 = doc.SelectSingleNode("result/output/ake173");
            if (ake173 != null)
            {
                personInfo.ake173 = FS.FrameWork.Function.NConvert.ToDecimal(ake173.InnerText);
            }
            else
            {
                personInfo.ake173 = 0;
            }

            XmlNode akb067 = doc.SelectSingleNode("result/output/akb067");
            if (akb067 != null)
            {
                personInfo.akb067 = FS.FrameWork.Function.NConvert.ToDecimal(akb067.InnerText);
            }
            else
            {
                personInfo.akb067 = 0;
            }

            XmlNode ake038 = doc.SelectSingleNode("result/output/ake038");
            if (ake038 != null)
            {
                personInfo.ake038 = FS.FrameWork.Function.NConvert.ToDecimal(ake038.InnerText);
            }
            else
            {
                personInfo.ake038 = 0;
            }

            XmlNode aae240 = doc.SelectSingleNode("result/output/aae240");
            if (aae240 != null)
            {
                personInfo.aae240 = FS.FrameWork.Function.NConvert.ToDecimal(ake038.InnerText);
            }
            else
            {
                personInfo.aae240 = 0;
            }

            XmlNode aac002 = doc.SelectSingleNode("result/output/aac002");
            if (aac002 != null)
            {
                personInfo.MCardNo = aac002.InnerText;
            }
            else
            {
                personInfo.MCardNo = "";
            }

            XmlNode aac003 = doc.SelectSingleNode("result/output/aac003");
            if (aac003 != null)
            {
                personInfo.Name = aac003.InnerText;
            }
            else
            {
                personInfo.Name = "";
            }

            XmlNode aac004 = doc.SelectSingleNode("result/output/aac004");
            if (aac004 != null)
            {
                personInfo.Sex = aac004.InnerText;
            }
            else
            {
                personInfo.Sex = "";
            }

            XmlNode aac006 = doc.SelectSingleNode("result/output/aac006");
            if (aac006 != null)
            {
                personInfo.Birth = aac006.InnerText;
            }
            else
            {
                personInfo.Birth = "";
            }

            XmlNode aab001 = doc.SelectSingleNode("result/output/aab001");
            if (aab001 != null)
            {
                personInfo.CompanyCode = aab001.InnerText;
            }
            else
            {
                personInfo.CompanyCode = "";
            }

            XmlNode aab004 = doc.SelectSingleNode("result/output/aab004");
            if (aab004 != null)
            {
                personInfo.CompanyName = aab004.InnerText;
            }
            else
            {
                personInfo.CompanyName = "";
            }

            XmlNode aab019 = doc.SelectSingleNode("result/output/aab019");
            if (aab019 != null)
            {
                personInfo.CompanyType = aab019.InnerText;
            }
            else
            {
                personInfo.CompanyType = "";
            }

            XmlNode aab020 = doc.SelectSingleNode("result/output/aab020");
            if (aab020 != null)
            {
                personInfo.EconomicType = aab020.InnerText;
            }
            else
            {
                personInfo.EconomicType = "";
            }

             XmlNode aab021 = doc.SelectSingleNode("result/output/aab021");
            if (aab021 != null)
            {
                personInfo.aab021 = aab021.InnerText.ToString();
            }
            else
            {
                personInfo.aab021 = "";
            }

             XmlNode ykc021 = doc.SelectSingleNode("result/output/ykc021");
            if (ykc021 != null)
            {
                personInfo.PersonType = ykc021.InnerText;
            }
            else
            {
                personInfo.PersonType = "";
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


            XmlNode ake105 = doc.SelectSingleNode("result/output/ake105");
            if (ake105 != null)
            {
                personInfo.ake105 = ake105.InnerText;
            }
            else
            {
                personInfo.ake105 = "";
            }


            XmlNode akc070 = doc.SelectSingleNode("result/output/akc070");
            if (akc070 != null)
            {
                personInfo.akc070 = akc070.InnerText.ToString();
            }
            else
            {
                personInfo.akc070 = "";
            }

            XmlNode akc071 = doc.SelectSingleNode("result/output/akc071");
            if (akc071 != null)
            {
                personInfo.akc071 = akc071.InnerText.ToString();
            }
            else
            {
                personInfo.akc071 = "";
            }

            XmlNode akc072 = doc.SelectSingleNode("result/output/akc072");
            if (akc072 != null)
            {
                personInfo.akc072 = akc072.InnerText.ToString();
            }
            else
            {
                personInfo.akc072 = "";
            }

            XmlNode ake171 = doc.SelectSingleNode("result/output/ake171");
            if (ake171 != null)
            {
                personInfo.ake171 = FS.FrameWork.Function.NConvert.ToDecimal(ake171.InnerText.ToString());
            }
            else
            {
                personInfo.ake171 = 0;
            }

            XmlNode akc253 = doc.SelectSingleNode("result/output/akc253");
            if (akc253 != null)
            {
                personInfo.own_cost_part = FS.FrameWork.Function.NConvert.ToDecimal(akc253.InnerText.ToString());
            }
            else
            {
                personInfo.own_cost_part = 0;
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


            XmlNode detail = doc.SelectSingleNode("result/output/detail");
            if (detail != null)
            {
                ProvinceAcrossSI.Objects.SIJiJinclass siJiJin = new ProvinceAcrossSI.Objects.SIJiJinclass();
                foreach (XmlNode rows in detail.SelectNodes("row"))
                {
                    siJiJin = new ProvinceAcrossSI.Objects.SIJiJinclass();
                    siJiJin.inpatient_no = patientInfo.ID;
                    siJiJin.is_valid = "1";
                    XmlNode aaa157 = rows.SelectSingleNode("aaa157");
                    if (aaa157 != null)
                    {
                        siJiJin.aaa157 = aaa157.InnerText.ToString();
                    }
                    else
                    {
                        siJiJin.aaa157 = "";
                    }
                    XmlNode aaa160 = rows.SelectSingleNode("aaa160");
                    if (aaa160 != null)
                    {
                        siJiJin.aaa160 = aaa160.InnerText.ToString();
                    }
                    else
                    {
                        siJiJin.aaa160 = "";
                    }
                    XmlNode aad006 = rows.SelectSingleNode("aad006");
                    if (aad006 != null)
                    {
                        siJiJin.aad006 = aad006.InnerText.ToString();
                    }
                    else
                    {
                        siJiJin.aad006 = "";
                    }
                    XmlNode aae187 = rows.SelectSingleNode("aae187");
                    if (aae187 != null)
                    {
                        siJiJin.aae187 = FS.FrameWork.Function.NConvert.ToDecimal(aae187.InnerText.ToString());
                    }
                    else
                    {
                        siJiJin.aae187 = 0;
                    }

                    list_SIJiJinclass.Add(siJiJin);
                }

            }
            #endregion

            this.SetOutPatientBalanceInfo(patientInfo, personInfo);
        }


        private void SetOutPatientBalanceInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
        {
            //设置基础费用信息以供校验
            //patientInfo.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(personInfo.balance_date);
            //personInfo.tot_cost = patientInfo.SIMainInfo.TotCost;
            //patientInfo.SIMainInfo.OwnCost = personInfo.tot_cost - personInfo.ake149;// GRZF_cost;
            //patientInfo.SIMainInfo.PubCost = personInfo.ake149;
            //personInfo.pub_cost = personInfo.ake149;

            //设置基础费用信息以供校验
            patientInfo.SIMainInfo.BalanceDate = DateTime.Now;
            patientInfo.SIMainInfo.TotCost = personInfo.tot_cost;
            patientInfo.SIMainInfo.OwnCost = personInfo.GRZF_cost;
            patientInfo.SIMainInfo.PubCost = personInfo.YBTCZF_cost;
        }

        /// <summary>
        /// 【异地】2.2.1.6 更新就诊登记信息
        /// 暂时不用，先放着
        /// </summary>
        public void SetUpdateRegInfoXML()
        { }

        /// <summary>
        /// 【异地】2.2.1.7 就诊登记回退 获取XML
        /// 【异地】2.2.1.9 出院登记回退 获取XML
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        public string SetCancelYdInPatientRegXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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

            //证件号码
            XmlElement ykc700 = xml.CreateElement("ykc700");//证件号码
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
            aac002.InnerText = patientInfo.SSN;// (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = "90";// personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = patientInfo.SSN;// patientInfo.IDCard;
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
            ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
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
            yzz061.InnerText = month;
            root.AppendChild(yzz061);

            //起始日期
            XmlElement yzz041 = xml.CreateElement("yzz041");//起始日期
            yzz041.InnerText = year+month+"01";
            root.AppendChild(yzz041);

            //截止日期
            XmlElement yzz042 = xml.CreateElement("yzz042");//截止日期
            DateTime dtEnd = DateTime.ParseExact(yzz041.InnerText, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            yzz042.InnerText = (dtEnd.AddMonths(1).AddDays(-1)).ToString("yyyyMMdd");
            root.AppendChild(yzz042);

             //结算申报业务号
            XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            yzz062.InnerText = "B" + personInfo.HospitalizeAreaCode + year + month + commObj.ID;
            commObj.ID = "B" + personInfo.HospitalizeAreaCode + year + month + commObj.ID;
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
             foreach (ProvinceAcrossSI.Objects.SIPersonInfo personInfoTemp in alPatientSI)
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
                 TimeSpan ts = patientTemp.PVisit.OutTime.Date - patientTemp.PVisit.InTime.Date;
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
            ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
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
            yzz061.InnerText = month;
            root.AppendChild(yzz061);

            //起始日期
            XmlElement yzz041 = xml.CreateElement("yzz041");//起始日期
            yzz041.InnerText = year + month + "01";
            root.AppendChild(yzz041);

            //截止日期
            XmlElement yzz042 = xml.CreateElement("yzz042");//截止日期
            DateTime dtEnd = DateTime.ParseExact(yzz041.InnerText, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            yzz042.InnerText = (dtEnd.AddMonths(1).AddDays(-1)).ToString("yyyyMMdd");
            root.AppendChild(yzz042);

            //结算申报业务号
            XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            yzz062.InnerText = "B" + personInfo.HospitalizeAreaCode + year + month + commObj.ID;
            commObj.ID = "B" + personInfo.HospitalizeAreaCode + year + month + commObj.ID;
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
            foreach (ProvinceAcrossSI.Objects.SIPersonInfo personInfoTemp in alPatientSI)
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
            ProvinceAcrossSI.Objects.SIPersonInfo personInfo = new ProvinceAcrossSI.Objects.SIPersonInfo();
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
            yzz061.InnerText = month;
            root.AppendChild(yzz061);

            //起始日期
            XmlElement yzz041 = xml.CreateElement("yzz041");//起始日期
            yzz041.InnerText = year + month + "01";
            root.AppendChild(yzz041);

            //截止日期
            XmlElement yzz042 = xml.CreateElement("yzz042");//截止日期
            DateTime dtEnd = DateTime.ParseExact(yzz041.InnerText, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            yzz042.InnerText = (dtEnd.AddMonths(1).AddDays(-1)).ToString("yyyyMMdd");
            root.AppendChild(yzz042);

            //结算申报业务号
            XmlElement yzz062 = xml.CreateElement("yzz062");//结算申报业务号
            yzz062.InnerText = "B" + personInfo.HospitalizeAreaCode + year + month + commObj.ID;
            commObj.ID = "B" + personInfo.HospitalizeAreaCode + year + month + commObj.ID;
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
            foreach (ProvinceAcrossSI.Objects.SIPersonInfo personInfoTemp in alPatientSI)
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
        public string SetCancelYdInPatientOutRegXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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
            aac002.InnerText = patientInfo.SSN;// (string.IsNullOrEmpty(patientInfo.SSN) ? "-" : patientInfo.SSN);
            root.AppendChild(aac002);

            //证件号码
            XmlElement aac043 = xml.CreateElement("aac043");//证件号码
            aac043.InnerText = "90";// personInfo.ZJLX;
            root.AppendChild(aac043);

            //证件号码
            XmlElement aac044 = xml.CreateElement("aac044");//证件号码
            aac044.InnerText = patientInfo.SSN;// patientInfo.IDCard;
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
        public string SetCancelYdInPatientBalanceXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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

            #region 新
            //结算业务号
            XmlElement aaz216 = xml.CreateElement("aaz216");//结算业务号
            aaz216.InnerText = personInfo.JSYWH;
            root.AppendChild(aaz216);

            //结算业务号
            XmlElement akc194 = xml.CreateElement("akc194");//结算业务号
            akc194.InnerText = patientInfo.PVisit.OutTime.ToString("yyyyMMddHH");
            root.AppendChild(akc194);

            //结算业务号
            XmlElement ykc706 = xml.CreateElement("ykc706");//结算业务号
            ykc706.InnerText = personInfo.balance_date;
            root.AppendChild(ykc706);

            #endregion

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
        public string SetYDInpatientOutRegXML(ref FS.HISFC.Models.RADT.PatientInfo patientInfo, ref ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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
        public string SetCancelYdFeeDetailXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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
        //public string SetCancelYdInPatientBalanceXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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
        public string SetQueryYdInPatientInfoXML(FS.HISFC.Models.RADT.PatientInfo patientInfo, ProvinceAcrossSI.Objects.SIPersonInfo personInfo)
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
    }
}
