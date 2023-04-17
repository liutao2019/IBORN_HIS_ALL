using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.Data;

namespace FoShanYDSI.Business.InPatient
{
    public class InPatientService
    {
        FoShanYDSI.Funtion function = new Funtion();
        FoShanYDSI.FoShanYDSIDatabase SIMgr = new FoShanYDSI.FoShanYDSIDatabase();
        Log cpLog = new Log();
        #region by han-zf 2014-07-17 事务传递,若不传递，取消收费后SIMgr的操作记录不会回滚
        private System.Data.IDbTransaction trans = null;
        public System.Data.IDbTransaction Trans
        {
            set { this.trans = value; }
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
            this.SIMgr.SetTrans(t);
        }
        #endregion


        /// <summary>
        /// 获得结算类别实体
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Item GetBalanceType(string code)
        {
            ArrayList arrRet = new System.Collections.ArrayList();
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "00";
            obj1.Name = "平均定额";
            arrRet.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "01";
            obj2.Name = "专项定额";
            arrRet.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "02";
            obj3.Name = "按项目结算";
            arrRet.Add(obj3);
            FS.HISFC.Models.Base.Item obj4 = new FS.HISFC.Models.Base.Item();
            obj4.ID = "03";
            obj4.Name = "生育门诊";
            arrRet.Add(obj4);
            FS.HISFC.Models.Base.Item obj5 = new FS.HISFC.Models.Base.Item();
            obj5.ID = "04";
            obj5.Name = "生育住院";
            arrRet.Add(obj5);


            FS.HISFC.Models.Base.Item strRet = (from FS.HISFC.Models.Base.Item obj in arrRet
                                                     where obj.ID.Equals(code)
                                                     select obj).FirstOrDefault();
            return strRet;
        }

        /// <summary>
        /// 获得参保险种字典名称
        /// </summary>
        /// <returns></returns>
        private string GetSiType(string code)
        {
            ArrayList arrRet = new System.Collections.ArrayList();
            FS.HISFC.Models.Base.Item obj1 = new FS.HISFC.Models.Base.Item();
            obj1.ID = "1";
            obj1.Name = "未成年医保";
            arrRet.Add(obj1);
            FS.HISFC.Models.Base.Item obj2 = new FS.HISFC.Models.Base.Item();
            obj2.ID = "2";
            obj2.Name = "居民医保";
            arrRet.Add(obj2);
            FS.HISFC.Models.Base.Item obj3 = new FS.HISFC.Models.Base.Item();
            obj3.ID = "3";
            obj3.Name = "基本医疗";
            arrRet.Add(obj3);
            FS.HISFC.Models.Base.Item obj4 = new FS.HISFC.Models.Base.Item();
            obj4.ID = "4";
            obj4.Name = "基本医疗+补助";
            arrRet.Add(obj4);
            FS.HISFC.Models.Base.Item obj5 = new FS.HISFC.Models.Base.Item();
            obj5.ID = "5";
            obj5.Name = "大病医保";
            arrRet.Add(obj5);
            FS.HISFC.Models.Base.Item obj6 = new FS.HISFC.Models.Base.Item();
            obj6.ID = "6";
            obj6.Name = "生育医保";
            arrRet.Add(obj6);
            FS.HISFC.Models.Base.Item obj7 = new FS.HISFC.Models.Base.Item();
            obj7.ID = "7";
            obj7.Name = "工伤医保";
            arrRet.Add(obj7);
            FS.HISFC.Models.Base.Item obj8 = new FS.HISFC.Models.Base.Item();
            obj8.ID = "8";
            obj8.Name = "门诊统筹";
            arrRet.Add(obj8);

            string strRet = (from FS.HISFC.Models.Base.Item obj in arrRet
                             where obj.ID.Equals(code)
                             select obj.Name).FirstOrDefault();
            return strRet;
        }

        public string GetDocIDenno(string code)
        {
            return this.SIMgr.QueryDocIdenno(code);
        }


        /// <summary>
        /// 通过结算项目XML获取相应arr
        /// </summary>
        /// <param name="retXML"></param>
        /// <param name="arrRet"></param>
        /// <returns></returns>
        private int GetCenterItemArr(string retXML, ref ArrayList arrRet)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(retXML);
            }
            catch (Exception e)
            {
                return -1;
            }
            arrRet = new ArrayList();
            System.Xml.XmlNodeList nodeList = doc.SelectSingleNode("/XML").ChildNodes;

            System.Xml.XmlNode xn;
            FoShanYDSI.Objects.SICenterBalanceItem obj;
            for (int i = 0; i < nodeList.Count; i++)
            {
                xn = nodeList[i];
                if (xn.Name == "结算项目")
                {
                    obj = new FoShanYDSI.Objects.SICenterBalanceItem();

                    obj.SiType = (from System.Xml.XmlNode childNode in xn.ChildNodes
                                  where childNode.Name == "参保险种"
                                  select childNode.InnerText).FirstOrDefault();
                    obj.ItemName = (from System.Xml.XmlNode childNode in xn.ChildNodes
                                    where childNode.Name == "项目名称"
                                    select childNode.InnerText).FirstOrDefault();
                    obj.ItemCode = (from System.Xml.XmlNode childNode in xn.ChildNodes
                                    where childNode.Name == "项目代码"
                                    select childNode.InnerText).FirstOrDefault();
                    obj.BgnTime = (from System.Xml.XmlNode childNode in xn.ChildNodes
                                   where childNode.Name == "有效开始日期"
                                   select childNode.InnerText).FirstOrDefault();
                    obj.EndTime = (from System.Xml.XmlNode childNode in xn.ChildNodes
                                   where childNode.Name == "有效结束日期"
                                   select childNode.InnerText).FirstOrDefault();
                    obj.BalanceType = (from System.Xml.XmlNode childNode in xn.ChildNodes
                                       where childNode.Name == "结算类型"
                                       select childNode.InnerText).FirstOrDefault();

                    obj.BalanceTypeName = this.GetBalanceType(obj.BalanceType).Name;
                    obj.SiName = this.GetSiType(obj.SiType);
                    arrRet.Add(obj);
                }
            }

            return 1;
        }


        #region 作废
        ///// <summary>
        ///// 结算项目下载
        ///// </summary>
        ///// <param name="patient"></param>
        ///// <param name="arrRet"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public string DownLoadCenterItem(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList arrRet, ref string msg)
        //{
        //    string status = "3";
        //    string ser = "医院联网结算";
        //    string cas = "住院结算项目下载";

        //    string parXML = string.Empty;//生成XML
        //    XmlDocument xml = new XmlDocument();
        //    xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GB18030", null));
        //    XmlElement root = xml.CreateElement("XML");
        //    xml.AppendChild(root);
        //    //出院时间
        //    XmlElement OutDateNode = xml.CreateElement("出院时间");
        //    OutDateNode.InnerText = patient.PVisit.OutTime.ToString("yyyyMMdd");
        //    root.AppendChild(OutDateNode);
        //    parXML = xml.InnerXml.ToString();


        //    string sig = function.GetSig(ser, function.DEP, cas, parXML);

        //    if ("-1".Equals(sig))
        //    {
        //        status = "3";
        //        msg = "未能读取电子签名";
        //        return status;
        //    }

        //    string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

        //    function.GetResultStatusAndMessage(retXML, ref status, ref msg);

        //    if (status == null || string.IsNullOrEmpty(status) || status.Equals("3") || status.Equals("2"))
        //    {
        //        //msg在获取返回值时已经赋值
        //        return status;
        //    }

        //    //将XML转换到arrRet中
        //    this.GetCenterItemArr(retXML, ref arrRet);

        //    int ret = this.SIMgr.InsertBalanceType(arrRet);
        //    if (ret < 1)
        //    {
        //        status = "3";
        //        msg = "住院结算项目下载成功，但插入数据库失败，请重试！";
        //    }
        //    return status;
        //}
        #endregion

        /// <summary>
        /// 查询患者费用明细
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryfeeDetails(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails, ref string msg)
        {
            int flag = this.SIMgr.QueryfeeDetails(patient, ref feeDetails);
            if (flag < 1)
            {
                msg = this.SIMgr.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 【异地】查询患者异地医保信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryInpatienRegInfo(string inpatientID, ref FoShanYDSI.Objects.SIPersonInfo personInfo)
        {
            int flag = this.SIMgr.QueryInPatientRegInfo(inpatientID, ref personInfo);
            if (flag < 1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 【异地】住院资格确认  
        /// </summary>
        /// <param name="patientInfo">患者实体</param>
        /// <param name="status">返回状态</param>
        /// <param name="msg">返回信息</param>
        /// <returns>返回交易retXML或者错误状态"3"</returns>
        public string YDInPatientAccreditation(FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            status = "-1";

            string ser = "异地联网结算";
            string cas = "身份识别";
            string parXML = function.SetYdInPatientAccreditationXML(patient, personInfo);//生成XML


            string retXML = "";
            patient.User01 = "住院资格确认YDInPatientAccreditation";
            function.Provider(patient,"0211", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                //MessageBox.Show(status);
                return status;
            }

            #region 生成医保数据

            function.GetYdInPatientAccreditationResult(retXML, patient, ref personInfo,ref  status, ref msg);

            #endregion

            return status;
        }

        /// <summary>
        /// 【异地】就诊登记回退
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string YDInpatientRegCancel(FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string msg)
        {
            string status = "-1";

            string ser = "异地联网结算";
            string cas = "就诊登记回退";
            string parXML = function.SetCancelYdInPatientInRegXML(patient, personInfo);

            string retXML = "";
            patient.User01 = "就诊登记回退YDInpatientRegCancel";
            function.Provider(patient, "0214", parXML, ref retXML, ref status, ref msg);//获得返回结果
            if (FS.FrameWork.Function.NConvert.ToDecimal(status) >= 0)
            {
                if (this.SIMgr.UpdateSiState(patient.ID, "-1") < 0)
                {
                    msg = "取消登记成功，但更新【fin_ipr_siinmaininfo_yd】状态为【取消住院登记】失败！" + this.SIMgr.Err;
                }
                else
                {
                    msg = "取消登记成功！";
                }
            }
            else
            {
                msg = "取消住院登记失败！" + msg;
            }
            return msg;
 
        }


        /// <summary>
        /// 【异地】住院登记
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int YDInPatientReg(FS.HISFC.Models.RADT.PatientInfo patient, FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            status = "-1";

            string ser = "异地联网结算";
            string cas = "就诊登记";


            string parXML = function.SetYdInPatientRegXML(patient, personInfo);//生成XML
            cpLog.WirteCpLog("\n住院登记：" + parXML);

            string retXML = "";
            patient.User01 = "住院登记YDInPatientReg";
            function.Provider(patient, "0212", parXML, ref retXML, ref status, ref msg);//获得返回结果
            cpLog.WirteCpLog("\n住院登记返回值\n" + retXML);
            
            if (status == null || string.IsNullOrEmpty(status) || FS.FrameWork.Function.NConvert.ToDecimal(status)<0)
            {
                MessageBox.Show("住院登记失败！TransNo：0212   " + msg);
                return -1;
            }
            function.GetYdInPatientRegResult(retXML, patient,ref personInfo);
            if (string.IsNullOrEmpty(personInfo.Name))
            {
                personInfo.Name = patient.Name;
            }

            if (string.IsNullOrEmpty(personInfo.IdenNo))
            {
                personInfo.IdenNo = patient.IDCard;
            }

            if (string.IsNullOrEmpty(personInfo.ClinicNo))
            {
                MessageBox.Show("返回的就诊登记号为空！");
                //return -1;
            }
            //TODO:插入数据库。
            if (this.SIMgr.InsertYDInPatientReg(patient, personInfo) < 0)
            {
                MessageBox.Show("插入医保主表失败！请手工取消中心住院登记记录！");
                status = "-1";
                msg = "\n插入医保主表失败！请手工取消中心住院登记记录";
                cpLog.WirteCpLog("\n插入医保主表失败\n" + retXML);
                return -1;
            }
            //更新状态为住院登记
            if ((this.SIMgr.UpdateSiState(patient.ID, "0") < 0))
            {
                status = "-1";
                msg = "\n更新为住院登记状态失败！";
                cpLog.WirteCpLog("\n更新为住院登记状态失败\n" + retXML);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 【异地】费用明细处理 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public int YDGetInpatientFeeDetail(FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref ArrayList feeDetails, ref string msg)
        {
            string status = "-1";

            string ser = "异地联网结算";
            string cas = "费用明细处理";


            #region 方法二
            //string[] listXML = function.GetYDInpatientFeeDetialXML(personInfo, feeDetails);

            //foreach (string parXML in listXML)
            //{
            //    string sig = function.GetSig(ser, function.DEP, cas, parXML);

            //    if ("-1".Equals(sig))
            //    {
            //        status = "-1";
            //        msg = "未能读取电子签名";
            //        return -1;
            //    }

            //    string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果
            //    function.GetResultStatusAndMessage(retXML, ref status, ref msg);
            //    if (status != "1")
            //    {
            //        status = "-1";
            //        return -1;
            //    }
            //}
            #endregion 

            #region 方法一
            //string parXML = function.GetYDInpatientFeeDetialXML2(personInfo, feeDetails);
            //string sig = function.GetSig(ser, function.DEP, cas, parXML);

            //if ("-1".Equals(sig))
            //{
            //    status = "-1";
            //    msg = "未能读取电子签名";
            //    return -1;
            //}

            //string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果
            //if (function.GetResultStatusAndMessage(retXML, ref status, ref msg) < 1)
            //{
            //    return -1;
            //}
            #endregion

            //费用明细处理的返回状态有待商讨，先这样写着 
            //不用商讨了，我就这么干
            #region 上面两个方法没什么卵用
            //string[] listXML = function.SetYDInpatientFeeDetialXML3(personInfo, feeDetails);
            //foreach (string parXML in listXML)
            //{
            //    string retXML = "";
            //    function.Provider("0301", parXML, ref retXML, ref status, ref msg);//获得返回结果
            //    //function.GetResultStatusAndMessage(retXML, ref status, ref msg);
            //    if (status != "1")
            //    {
            //        status = "-1";
            //        return -1;
            //    }
            //}
            #endregion

            #region 佛山专用，有个本地的接口[28]需要先调用
            string[] listXML = function.SetYDInpatientFeeDetialXML4(personInfo, feeDetails);
            foreach (string parXML in listXML)
            {
                if (!parXML.Contains("row"))
                {
                    continue;
                }
                string retXML = "";
                patient.User01 = "费用明细处理YDGetInpatientFeeDetail";
                function.Provider(patient, "28", parXML, ref retXML, ref status, ref msg);//获得返回结果
                //function.GetResultStatusAndMessage(retXML, ref status, ref msg);
                if (status != "1")
                {
                    status = "-1";
                    return -1;
                }
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(retXML);
                string inxml = string.Empty;
                inxml = doc.SelectSingleNode("result/output").InnerXml.ToString().Trim();

                function.Provider(patient, "0301", inxml, ref retXML, ref status, ref msg);//获得返回结果
                if (status != "1")
                {
                    status = "-1";
                    return -1;
                }
            }
            #endregion 
            return 1;
        }

        /// <summary>
        /// 【异地】出院登记
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int YDInPatientBalanceReg(ref FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            int state = 1;
            string ser = "异地联网结算";
            string cas = "出院登记";


            string parXML = function.SetYDInpatientOutRegXML(ref patient, ref personInfo);

            string retXML = "";
            patient.User01 = "出院登记YDInPatientBalanceReg";
            function.Provider(patient, "0215", parXML, ref retXML, ref status, ref msg); //获得返回结果
            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXML);
            string inxml = string.Empty;
            inxml = doc.SelectSingleNode("result/transid").InnerXml.ToString().Trim();
            personInfo.Out_TransID = inxml;
            int ret = function.UpdateOutTrandID(patient.ID, personInfo.Out_TransID);
            //TODO:插入数据库。
            if (this.SIMgr.UpdateSiinmaininfoYDout_transid(patient, personInfo.Out_TransID) < 1)
            {
                status = "-1";
                msg = "\n更新医保主表失败！请联系信息科";
                cpLog.WirteCpLog("\n更新医保主表失败\n" + retXML);
                return -1;
            }
            return 1;//之后需要修改
        }

        /// <summary>
        /// 住院登记取消
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int CancelInPatientReg(FS.HISFC.Models.RADT.PatientInfo patient, ref string status, ref string msg)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            
            if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 1)
            {
                msg = "\n佛山医保" + "\n查询患者入院登记信息出错" + "\n" + this.SIMgr.Err;
                cpLog.WirteCpLog("\n住院登记取消："+msg);
                return -1;
            }

            status = "-1";

            string ser = "异地联网结算";
            string cas = "就诊登记回退";
            string parXML = function.SetCancelYdInPatientInRegXML(patient, personInfo);

            string retXML = "";
            patient.User01 = "住院登记取消CancelInPatientReg";
            function.Provider(patient, "2014", parXML, ref retXML, ref status, ref msg);//获得返回结果
            if (FS.FrameWork.Function.NConvert.ToDecimal(status) > 0)
            {
                if (this.SIMgr.UpdateSiinmaininfoValidFlag(patient, personInfo, "0") < 1)
                {
                    msg = "\n佛山异地医保" + "\n更新医保主表出错" + "\n" + this.SIMgr.Err;
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(retXML);
                string errormsg = string.Empty;
                errormsg = doc.SelectSingleNode("result/errormsg").InnerXml.ToString().Trim();
                msg = "取消住院登记失败！" + errormsg;
                return -1;
            }
        }

        #region 佛山没有预留接口，直接查表
        ///// <summary>
        ///// 住院登记查询【结算前查询】
        ///// </summary>
        ///// <param name="patient"></param>
        ///// <param name="personInfo"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public int QueryRegInfo(FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string msg)
        //{
        //    //查询参保险种
        //    if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 1)
        //    {
        //        msg = "\n佛山医保" + "\n查询患者入院登记信息出错" + "\n" + this.SIMgr.Err;
        //        return -1;
        //    }

        //    string status = "3";

        //    string ser = "医院联网结算";
        //    string cas = "住院登记查询";

        //    string parXML = function.GetQueryRegInfoXML(patient, personInfo);//生成XML

        //    string sig = function.GetSig(ser, function.DEP, cas, parXML);

        //    if ("-1".Equals(sig))
        //    {
        //        status = "3";
        //        msg = "未能读取电子签名";
        //        return -1;
        //    }

        //    string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

        //    function.GetResultStatusAndMessage(retXML, ref status, ref msg);

        //    if (status == null || string.IsNullOrEmpty(status) || status.Equals("3") || status.Equals("2"))
        //    {
        //        //msg在获取返回值时已经赋值
        //        return -1;
        //    }

        //    function.GetQueryRegInfoResult(retXML, patient, ref personInfo);
            

        //    return 1;
        //}
        #endregion

        /// <summary>
        /// [异地] 住院结算
        /// </summary>
        /// <param name="patient">患者信息</param>
        /// <param name="personInfo">医保信息</param>
        /// <param name="feeDetails">费用明细</param>
        /// <param name="msg"></param>
        /// <param name="arrParm">住院结算界面相关参数</param>
        /// <returns></returns>
        public int YDInpatientFeeBalance(FS.HISFC.Models.RADT.PatientInfo patient, FoShanYDSI.Objects.SIPersonInfo personInfo, ref string msg)
        {

            string status = "-1";

            string ser = "异地联网结算";
            string cas = "费用结算";

            string parXML = function.SetYDInpatientFeeBalanceXML(personInfo, patient);//生成XML
            string retXML = "";
            patient.User01 = "住院结算YDInpatientFeeBalance";
            function.Provider(patient, "0304", parXML, ref retXML, ref status, ref msg);//获得返回结果
            
            if(FS.FrameWork.Function.NConvert.ToDecimal(status)<0)
            {
                return -1;
            }
            else
            {
                personInfo.BalanceFlag = "1";
            }

            //TODO:获取返回值         
            function.GetYdInPatientBalanceResult(retXML, patient, ref personInfo);

            //TODO:操作数据库
            if (this.SIMgr.InsertSIMainInfoInpatient(patient, personInfo) < 0)
            {
                status = "-1";
                msg = SIMgr.Err;
                return -1;
            }

            return 1;
        }

        #region 佛山没有分
        ///// <summary>
        ///// 工伤住院结算
        ///// </summary>
        ///// <param name="patient"></param>
        ///// <param name="personInfo"></param>
        ///// <param name="feeDetails"></param>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public int BalanceGS(FS.HISFC.Models.RADT.PatientInfo patient, FoShanYDSI.Objects.SIPersonInfo personInfo, ref ArrayList feeDetails, ref string msg, ArrayList arrParm)
        //{
        //    string status = "-1";

        //    string ser = "医院联网结算";
        //    string cas = "工伤住院结算";

        //    string parXML = function.GetBalanceGsXML(patient, personInfo, feeDetails, arrParm);//生成XML

        //    string sig = function.GetSig(ser, function.DEP, cas, parXML);

        //    if ("-1".Equals(sig))
        //    {
        //        status = "-1";
        //        msg = "未能读取电子签名";
        //        return -1;
        //    }

        //    string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

        //    function.GetResultStatusAndMessage(retXML, ref status, ref msg);

        //    if (status == null || string.IsNullOrEmpty(status) || status.Equals("3"))
        //    {
        //        //msg在获取返回值时已经赋值
        //        return -1;
        //    }

        //    ZhuHaiYDSI.Objects.OutBalanceInfo outBalanceInfo = new ZhuHaiYDSI.Objects.OutBalanceInfo();
        //    //TODO:获取返回值         
        //    function.GetInPatientBalanceGsResult(retXML, patient, ref personInfo, ref outBalanceInfo);

        //    //TODO:操作数据库
        //    if (this.SIMgr.InsertSIMainInfoInpatient(patient, personInfo) < 0)
        //    {
        //        status = "-1";
        //        msg = SIMgr.Err;
        //        return -1;
        //    }

        //    return 1;
        //}
        #endregion


        /// <summary>
        /// 【异地】住院结算取消
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CancelYDBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref string msg)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            //查询参保险种
            if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 1)
            {
                msg = "\n异地医保" + "\n查询患者入院登记信息出错" + "\n" + this.SIMgr.Err;
                return -1;
            }


            string status = "-1";

            string ser = "异地联网结算";
            string cas = "费用结算回退";

            string parXML = function.SetCancelYdInPatientBalanceXML(patient, personInfo);//生成XML
            string retXML = "";
            patient.User01 = "住院结算取消CancelYDBalanceInpatient";
            function.Provider(patient, "0305", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status)< 0)
            {
                return -1;
            }


            //TODO:操作数据库
            //更新医保结算表有效位         
            if (this.SIMgr.UpdateSiinmaininfoValidFlag(patient, personInfo, "0") < 1)
            {
                msg = "\n佛山异地医保" + "\n更新医保主表出错" + "\n" + this.SIMgr.Err;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 取消住院费用信息上传
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int CancelUploadBalanceItem(FS.HISFC.Models.RADT.PatientInfo patient, ref string msg)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            //查询参保险种
            if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 1)
            {
                msg = "\n异地医保" + "\n查询患者入院登记信息出错" + "\n" + this.SIMgr.Err;
                return -1;
            }


            string status = "-1";

            string ser = "异地联网结算";
            string cas = "费用明细回退";

            string parXML = function.SetCancelYdFeeDetailXML(patient, personInfo);//生成XML

            string retXML = "";
            patient.User01 = "取消住院费用信息上传CancelUploadBalanceItem";
            function.Provider(patient, "0302", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// [异地] 出院登记回退
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int CancelOutReg(FS.HISFC.Models.RADT.PatientInfo patient, ref string msg)
        {
            FoShanYDSI.Objects.SIPersonInfo personInfo = new FoShanYDSI.Objects.SIPersonInfo();
            //查询参保险种
            if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 1)
            {
                msg = "\n异地医保" + "\n查询患者入院登记信息出错" + "\n" + this.SIMgr.Err;
                return -1;
            }


            string status = "-1";

            string ser = "异地联网结算";
            string cas = "出院登记回退";

            string parXML = function.SetCancelYdInPatientOutRegXML(patient, personInfo);//生成XML
            string retXML = "";
            patient.User01 = "出院登记回退CancelOutReg";
            function.Provider(patient, "0216", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status)< 1)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 住院结算查询
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QuerySiBalanceInfo(ref FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string msg)
        {
            ////查询参保险种
            //if (this.SIMgr.QuerySiBalanceInfo(ref patient, ref personInfo) < 1)
            //{
            //    msg = "\n佛山医保" + "\n查询佛山医保结算信息失败！" + "\n" + this.SIMgr.Err;
            //    return -1;
            //}

            //if (!personInfo.BalanceFlag.Equals("1"))
            //{
            //    msg = "\n佛山医保" + "\n未查询到医保结算信息，请先进行佛山医保医保结算！";
            //    return -1;
            //}

            if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 0)
            {
                msg = "\n佛山医保" + "\n查询佛山医保结算信息失败！" + "\n" + this.SIMgr.Err;
                return -1;
            }
            
            return 1;
        }

        public int MonthlyReportCancel(FS.FrameWork.Models.NeuObject obj)
        {
            return 1;
        }

        public ArrayList ExtractMonthlySettlementResults(FS.FrameWork.Models.NeuObject obj)
        {
            ArrayList alTemp = new ArrayList();
            return alTemp;
        }

        public ArrayList ExtractAuditInstructions(FS.FrameWork.Models.NeuObject obj, FoShanYDSI.Objects.SIPersonInfo siPerson, ref FS.FrameWork.Models.NeuObject objt)
        {
            ArrayList alTemp = new ArrayList();
            return alTemp;
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
        public int MonthlyReportSubmit(string year, string month,ref FS.FrameWork.Models.NeuObject commObj,
                        FS.FrameWork.Models.NeuObject neuObj, ArrayList alPatientSI, Hashtable hsPatienct, ref string msg)
        {
            string status = "-1";

            string parXML = function.SetMonthlyReportSubmitXML(year, month,ref commObj,neuObj,alPatientSI, hsPatienct);//生成XML
            string retXML = "";
            function.Provider(null, "0521", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXML);
            commObj.ID = doc.SelectSingleNode("result/transid").InnerXml.ToString().Trim();
            commObj.User01 = doc.SelectSingleNode("result/yzz062").InnerXml.ToString().Trim();

            return 1;
        }

        /// <summary>
        /// 提交月度申报汇总表回退0535
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="alPatientSI"></param>
        /// <param name="hsPatienct"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int MonthlyReportCancel(string year, string month, ref FS.FrameWork.Models.NeuObject commObj,
                       FS.FrameWork.Models.NeuObject neuObj, ref string msg)
        {
            string status = "-1";

            string parXML = function.SetMonthlyReportCancelXML(year, month, ref commObj, neuObj);//生成XML
            string retXML = "";
            function.Provider(null, "0535", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXML);
            commObj.ID = doc.SelectSingleNode("result/transid").InnerXml.ToString().Trim();

            return 1;
        }
        /// <summary>
        /// 月度结算申报回退0522
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int MonthlyReportCancelByCustomType(string year, string month, ref FS.FrameWork.Models.NeuObject commObj,
                      FS.FrameWork.Models.NeuObject neuObj, ref string msg)
        {
            string status = "-1";

            string parXML = function.SetMonthlyReportCancelByCustomTypeXML(year, month, ref commObj, neuObj);//生成XML
            string retXML = "";
            function.Provider(null, "0522", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXML);
            commObj.ID = doc.SelectSingleNode("result/transid").InnerXml.ToString().Trim();

            return 1;
        }

        /// <summary>
        /// 月度结算申报回退0522
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int MonthlyReportCancelByCustomType0537(string year, string month, ref FS.FrameWork.Models.NeuObject commObj,
                      FS.FrameWork.Models.NeuObject neuObj, ref string msg)
        {
            string status = "-1";

            string parXML = function.SetMonthlyReportCancelByCustomType0537XML(year, month, ref commObj, neuObj);//生成XML
            string retXML = "";
            function.Provider(null, "0537", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXML);
            commObj.ID = doc.SelectSingleNode("result/transid").InnerXml.ToString().Trim();

            return 1;
        }
        /// <summary>
        /// 提交审核支付明细回退0524
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int MonthlyReportCancelDetail(string year, string month, ref FS.FrameWork.Models.NeuObject commObj,
                     FS.FrameWork.Models.NeuObject neuObj, ref string msg)
        {
            string status = "-1";

            string parXML = function.SetMonthlyReportCancelDetailXML(year, month, ref commObj, neuObj);//生成XML
            string retXML = "";
            function.Provider(null, "0524", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(retXML);
            commObj.ID = doc.SelectSingleNode("result/transid").InnerXml.ToString().Trim();

            return 1;
        }
        /// <summary>
        /// 4.53	提交月度申报汇总表(0534)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="alPatientSI"></param>
        /// <param name="hsPatienct"></param>
        /// <returns></returns>
        public int MonthlyReportTot(string year, string month, ref FS.FrameWork.Models.NeuObject commObj,
            ArrayList alPatientSI, ref string msg)
        {
            string status = "-1";

            string parXML = function.SetMonthlyReportTotXML(year, month, ref commObj, alPatientSI);//生成XML
            string retXML = "";
            function.Provider(null, "0534", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 4.55	提交月度申报分险种汇总表(0536)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="commObj"></param>
        /// <param name="neuObj"></param>
        /// <param name="alPatientSI"></param>
        /// <param name="hsPatienct"></param>
        /// <returns></returns>
        public int MonthlyReportSubmitXZ(string year, string month, string areaCount,
            ref FS.FrameWork.Models.NeuObject commObj, ArrayList alPatientSI,  ref string msg)
        {
            string status = "-1";

            string parXML = function.SetMonthlyReportSubmitXZXML(year, month, areaCount, ref commObj , alPatientSI);//生成XML
            string retXML = "";
            function.Provider(null, "0536", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 1)
            {
                return -1;
            }
            return 1;
        }

        //1301 冲正交易
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SICode">参保地</param>
        /// <param name="transNO">冲正交易编号0212 0304 0305 3105 3202 </param>
        /// <param name="transID">交易返回的事物流水号</param>
        /// <returns></returns>
        public int CorrectionTrans(string SICode, string transNO, string transID, ref string msg)
        {
            string inxml = @"<?xml version='1.0' encoding='GBK' ?><input><sign>{0}</sign><transid>{1}</transid>
                                <aab299>{2}</aab299><aab301>{3}</aab301><otransno>{4}</otransno><otransid>{5}</otransid></input>";
            
            FoShanYDSI.Objects.SIPersonInfo personInfo= new FoShanYDSI.Objects.SIPersonInfo();

            function.GetHosCodeAndRegionCode(ref personInfo);

            inxml = string.Format(inxml, "", "", personInfo.HospitalizeAreaCode, SICode, transNO, transID);
            string outxml = "";
            string status = "-1";
            function.Provider(null, "1301", inxml, ref outxml, ref status, ref msg);//获得返回结果
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(outxml);
            string code = doc.SelectSingleNode("result/errorcode").InnerText.Trim();

            if (FS.FrameWork.Function.NConvert.ToInt32(code) < 0) //失败
            {
                return -1;
            }

            return 1;
        }


        /// <summary>
        /// 已上传的费用明细信息查询
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDeatils"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string QueryInPatientUpFeeDetail(FS.HISFC.Models.RADT.PatientInfo patient, FoShanYDSI.Objects.SIPersonInfo personInfo,ref ArrayList feeDeatils, ref string msg)
        {
            string status = "-1";

            string parXML = function.SetQueryInPatientUpFeeDetailXML(patient, personInfo);//生成XML


            string retXML = "";
            patient.User01 = "已上传的费用明细信息查询QueryInPatientUpFeeDetail";
            function.Provider(patient, "0707", parXML, ref retXML, ref status, ref msg);//获得返回结果

            if (status != "1")
            {
                return "-1";
            }

            #region 生成医保数据

            function.GetQueryInPatientUpFeeDetailResult(retXML, patient, ref feeDeatils, ref  status, ref msg);

            #endregion

            return status;
        }



        /// <summary>
        /// 查询患者住院信息列表
        /// </summary>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public ArrayList QueryInpatienInfo(string patientNo)
        {
            ArrayList al = this.SIMgr.QueryInPatientInfo(patientNo);
            if (al == null || al.Count <= 0)
            {
                return null;
            }
            return al;
        }
        /// <summary>
        /// 【异地】住院病人信息（病案首页）录入（0801）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int YDInPatientUploadDRGS(DataTable dt, ref FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                msg = "住院病案信息为空！";
                return -1;
            }
            int state = 1;
            string ser = "异地联网结算";
            string cas = "住院病人信息（病案首页）录入";


            string parXML = function.SetYDInPatientUploadDRGSXML(dt, ref patient, ref personInfo);

            string retXML = "";
            patient.User01 = "住院病人信息（病案首页）录入YDInPatientUploadDRGS";
            function.Provider(patient, "0801", parXML, ref retXML, ref status, ref msg); //获得返回结果
            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }

            return 1;//
        }


        /// <summary>
        /// 【异地】住院病人诊断信息（病案首页）录入（0802）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int YDInPatientUploadCaseDiagnose(DataTable dt, ref FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            if (dt == null)
            {
                msg = "住院病案信息为空！";
                return -1;
            }
            int state = 1;
            string ser = "异地联网结算";
            string cas = "住院病人诊断信息（病案首页）录入";


            string parXML = function.SetYDInPatientCaseDiagnoseXML(dt, ref patient, ref personInfo);

            string retXML = "";
            patient.User01 = "住院病人诊断信息（病案首页）录入YDInPatientUploadCaseDiagnose";
            function.Provider(patient, "0802", parXML, ref retXML, ref status, ref msg); //获得返回结果
            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }

            return 1;//
        }



        /// <summary>
        /// 【异地】住院病人手术信息（病案首页）录入（0803）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int YDInPatientOperationDetail(DataTable dt, ref FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            if (dt == null)
            {
                msg = "住院病人手术信息为空！";
                return -1;
            }
            int state = 1;
            string ser = "异地联网结算";
            string cas = "住院病人手术信息（病案首页）录入";


            string parXML = function.SetYDInPatientOperationDetailXML(dt, ref patient, ref personInfo);

            string retXML = "";
            patient.User01 = "住院病人手术信息（病案首页）录入YDInPatientOperationDetail";
            function.Provider(patient, "0803", parXML, ref retXML, ref status, ref msg); //获得返回结果
            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }

            return 1;//
        }



        /// <summary>
        /// 【异地】住院病人产科分娩婴儿信息（病案首页）录入（0804）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int YDInPatientBabyInfo(DataTable dt, ref FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            if (dt == null)
            {
                msg = "分娩婴儿信息为空！";
                return -1;
            }
            int state = 1;
            string ser = "异地联网结算";
            string cas = "住院病人产科分娩婴儿信息（病案首页）录入";


            string parXML = function.SetYDInPatientBabyInfoXML(dt, ref patient, ref personInfo);

            string retXML = "";
            patient.User01 = "住院病人产科分娩婴儿信息（病案首页）录入YDInPatientBabyInfo";
            function.Provider(patient, "0804", parXML, ref retXML, ref status, ref msg); //获得返回结果
            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }

            return 1;//
        }



        /// <summary>
        /// 【异地】出院小结（出院记录）录入（0806）
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int YDInPatientOutRecord(FS.HISFC.Models.Base.Const dt, ref FS.HISFC.Models.RADT.PatientInfo patient, ref FoShanYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            if (dt == null)
            {
                msg = "出院小结信息为空！";
                return -1;
            }
            int state = 1;
            string ser = "异地联网结算";
            string cas = "出院小结（出院记录）录入";


            string parXML = function.SetYDInPatientOutRecordXML(dt, ref patient, ref personInfo);

            string retXML = "";
            patient.User01 = "出院小结（出院记录）录入YDInPatientOutRecord";
            function.Provider(patient, "0806", parXML, ref retXML, ref status, ref msg); //获得返回结果
            if (FS.FrameWork.Function.NConvert.ToDecimal(status) < 0)
            {
                return -1;
            }

            return 1;//
        }




    }
}
