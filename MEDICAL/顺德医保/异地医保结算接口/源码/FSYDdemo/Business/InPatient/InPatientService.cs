using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Xml;

namespace ZhuHaiYDSI.Business.InPatient
{
    public class InPatientService
    {
        ZhuHaiYDSI.Function function = new ZhuHaiYDSI.Function();
        ZhuHaiYDSI.ZhuHaiYDSIDatabase SIMgr = new ZhuHaiYDSI.ZhuHaiYDSIDatabase();

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
        private Neusoft.HISFC.Models.Base.Item GetBalanceType(string code)
        {
            ArrayList arrRet = new System.Collections.ArrayList();
            Neusoft.HISFC.Models.Base.Item obj1 = new Neusoft.HISFC.Models.Base.Item();
            obj1.ID = "00";
            obj1.Name = "平均定额";
            arrRet.Add(obj1);
            Neusoft.HISFC.Models.Base.Item obj2 = new Neusoft.HISFC.Models.Base.Item();
            obj2.ID = "01";
            obj2.Name = "专项定额";
            arrRet.Add(obj2);
            Neusoft.HISFC.Models.Base.Item obj3 = new Neusoft.HISFC.Models.Base.Item();
            obj3.ID = "02";
            obj3.Name = "按项目结算";
            arrRet.Add(obj3);
            Neusoft.HISFC.Models.Base.Item obj4 = new Neusoft.HISFC.Models.Base.Item();
            obj4.ID = "03";
            obj4.Name = "生育门诊";
            arrRet.Add(obj4);
            Neusoft.HISFC.Models.Base.Item obj5 = new Neusoft.HISFC.Models.Base.Item();
            obj5.ID = "04";
            obj5.Name = "生育住院";
            arrRet.Add(obj5);


            Neusoft.HISFC.Models.Base.Item strRet = (from Neusoft.HISFC.Models.Base.Item obj in arrRet
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
            Neusoft.HISFC.Models.Base.Item obj1 = new Neusoft.HISFC.Models.Base.Item();
            obj1.ID = "1";
            obj1.Name = "未成年医保";
            arrRet.Add(obj1);
            Neusoft.HISFC.Models.Base.Item obj2 = new Neusoft.HISFC.Models.Base.Item();
            obj2.ID = "2";
            obj2.Name = "居民医保";
            arrRet.Add(obj2);
            Neusoft.HISFC.Models.Base.Item obj3 = new Neusoft.HISFC.Models.Base.Item();
            obj3.ID = "3";
            obj3.Name = "基本医疗";
            arrRet.Add(obj3);
            Neusoft.HISFC.Models.Base.Item obj4 = new Neusoft.HISFC.Models.Base.Item();
            obj4.ID = "4";
            obj4.Name = "基本医疗+补助";
            arrRet.Add(obj4);
            Neusoft.HISFC.Models.Base.Item obj5 = new Neusoft.HISFC.Models.Base.Item();
            obj5.ID = "5";
            obj5.Name = "大病医保";
            arrRet.Add(obj5);
            Neusoft.HISFC.Models.Base.Item obj6 = new Neusoft.HISFC.Models.Base.Item();
            obj6.ID = "6";
            obj6.Name = "生育医保";
            arrRet.Add(obj6);
            Neusoft.HISFC.Models.Base.Item obj7 = new Neusoft.HISFC.Models.Base.Item();
            obj7.ID = "7";
            obj7.Name = "工伤医保";
            arrRet.Add(obj7);
            Neusoft.HISFC.Models.Base.Item obj8 = new Neusoft.HISFC.Models.Base.Item();
            obj8.ID = "8";
            obj8.Name = "门诊统筹";
            arrRet.Add(obj8);

            string strRet = (from Neusoft.HISFC.Models.Base.Item obj in arrRet
                             where obj.ID.Equals(code)
                             select obj.Name).FirstOrDefault();
            return strRet;
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
            ZhuHaiYDSI.Objects.SICenterBalanceItem obj;
            for (int i = 0; i < nodeList.Count; i++)
            {
                xn = nodeList[i];
                if (xn.Name == "结算项目")
                {
                    obj = new ZhuHaiYDSI.Objects.SICenterBalanceItem();

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
   

        /// <summary>
        /// 结算项目下载
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="arrRet"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string DownLoadCenterItem(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref ArrayList arrRet, ref string msg)
        {
            string status = "3";
            string ser = "医院联网结算";
            string cas = "住院结算项目下载";

            string parXML = string.Empty;//生成XML
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(xml.CreateXmlDeclaration("1.0", "GB18030", null));
            XmlElement root = xml.CreateElement("XML");
            xml.AppendChild(root);
            //出院时间
            XmlElement OutDateNode = xml.CreateElement("出院时间");
            OutDateNode.InnerText = patient.PVisit.OutTime.ToString("yyyyMMdd");
            root.AppendChild(OutDateNode);
            parXML = xml.InnerXml.ToString();


            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "3";
                msg = "未能读取电子签名";
                return status;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            function.GetResultStatusAndMessage(retXML, ref status, ref msg);

            if (status == null || string.IsNullOrEmpty(status) || status.Equals("3") || status.Equals("2"))
            {
                //msg在获取返回值时已经赋值
                return status;
            }

            //将XML转换到arrRet中
            this.GetCenterItemArr(retXML, ref arrRet);

            int ret = this.SIMgr.InsertBalanceType(arrRet);
            if (ret < 1)
            {
                status = "3";
                msg = "住院结算项目下载成功，但插入数据库失败，请重试！";
            }
            return status;
        }

        /// <summary>
        /// 查询患者费用明细
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryfeeDetails(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails, ref string msg)
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
        public int QueryInpatienRegInfo(string inpatientID, ref ZhuHaiYDSI.Objects.SIPersonInfo personInfo)
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
        public string YDInPatientAccreditation(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref ZhuHaiYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            status = "1";

            string ser = "异地联网结算";
            string cas = "身份识别";
            string parXML = function.GetYdInPatientAccreditationXML(patient, personInfo);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "3";
                msg = "未能读取电子签名";
                return status;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果
            function.GetYdRegInpationResult(retXML, ref status, ref msg);
            
            //function.GetResultStatusAndMessage(retXML, ref status, ref msg);

            //if (status == null || string.IsNullOrEmpty(status) || status.Equals("3"))
            //{
            //    return "3";
            //}

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
        public string YDInpatientRegCancel(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref ZhuHaiYDSI.Objects.SIPersonInfo personInfo,ref string msg)
        {
            string status = "1";

            string ser = "异地联网结算";
            string cas = "就诊登记回退";
            string parXML = function.GetCancelYdInPatientRegXML(patient, personInfo);
            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "未能读取电子签名";
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果
            if (function.GetInpatientCancelRegResult(retXML,ref msg) > 0)
            {
                status = "取消登记成功！";
            }
            else
            {
                status = "取消住院登记失败！";
            }
            return status;
 
        }


        /// <summary>
        /// 【异地】住院登记
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int YDInPatientReg(Neusoft.HISFC.Models.RADT.PatientInfo patient, ZhuHaiYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            status = "1";

            string ser = "异地联网结算";
            string cas = "就诊登记";


            string parXML = function.GetYdInPatientRegXML(patient, personInfo);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return -1;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            function.GetYDResultStatusAndMessage(retXML, ref status, ref msg);

            if (status == null || string.IsNullOrEmpty(status) || status.Equals("3") || status.Equals("-1"))
            {
                return -1;
            }

            function.GetYdInPatientRegResult(retXML, patient,ref personInfo);

            //TODO:插入数据库。
            if (this.SIMgr.InsertYDInPatientReg(patient, personInfo) < 1)
            {
                status = "-1";
                msg = "\n插入医保主表失败！请手工取消中心住院登记记录";
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
        public int YDGetInpatientFeeDetail(Neusoft.HISFC.Models.RADT.PatientInfo patient,ref ZhuHaiYDSI.Objects.SIPersonInfo personInfo,ref ArrayList feeDetails,ref string msg)
        {
            string status = "1";

            string ser = "异地联网结算";
            string cas = "费用明细处理";


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
            string parXML = function.GetYDInpatientFeeDetialXML2(personInfo, feeDetails);
            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return -1;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果
            function.GetResultStatusAndMessage(retXML, ref status, ref msg);
            if (status != "1")
            {
                status = "-1";
                return -1;
            }

            //费用明细处理的返回状态有待商讨，先这样写着
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
        public int YDInPatientBalanceReg(ref Neusoft.HISFC.Models.RADT.PatientInfo patient, ref ZhuHaiYDSI.Objects.SIPersonInfo personInfo, ref string status, ref string msg)
        {
            int state = 1;
            string ser = "异地联网结算";
            string cas = "出院登记";


            string parXML = function.GetYDInpatientOutRegXML(ref patient,ref personInfo);

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return -1;
            }
            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果
            function.GetResultStatusAndMessage(retXML, ref status, ref msg);
            if (status != "1")
            {
                status = "-1";
                return -1;
            }
            return state;//之后需要修改
        }

        /// <summary>
        /// 住院登记取消
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int CancelInPatientReg(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref string status, ref string msg)
        {
            ZhuHaiYDSI.Objects.SIPersonInfo personInfo = new ZhuHaiYDSI.Objects.SIPersonInfo();
            if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 1)
            {
                msg = "\n珠海医保" + "\n查询患者入院登记信息出错" + "\n" + this.SIMgr.Err;
                return -1;
            }

            status = "1";

            string ser = "医院联网结算";
            string cas = "住院登记取消";

            string parXML = function.GetCancelInPatientRegXML(patient, personInfo);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return -1;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            function.GetResultStatusAndMessage(retXML, ref status, ref msg);

            if (status == null || string.IsNullOrEmpty(status) || status.Equals("3"))
            {
                //msg在获取返回值时已经赋值
                return -1;
            }

            //TODO:更新数据库。
            if (this.SIMgr.UpdateSiinmaininfoValidFlag(patient, personInfo, "0") < 1)
            {
                msg = "\n珠海医保" + "\n更新医保主表出错" + "\n" + this.SIMgr.Err;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 住院登记查询【结算前查询】
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int QueryRegInfo(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref ZhuHaiYDSI.Objects.SIPersonInfo personInfo, ref string msg)
        {
            //查询参保险种
            if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 1)
            {
                msg = "\n珠海医保" + "\n查询患者入院登记信息出错" + "\n" + this.SIMgr.Err;
                return -1;
            }

            string status = "3";

            string ser = "医院联网结算";
            string cas = "住院登记查询";

            string parXML = function.GetQueryRegInfoXML(patient, personInfo);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "3";
                msg = "未能读取电子签名";
                return -1;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            function.GetResultStatusAndMessage(retXML, ref status, ref msg);

            if (status == null || string.IsNullOrEmpty(status) || status.Equals("3") || status.Equals("2"))
            {
                //msg在获取返回值时已经赋值
                return -1;
            }

            function.GetQueryRegInfoResult(retXML, patient, ref personInfo);
            

            return 1;
        }

        /// <summary>
        /// 普通住院结算
        /// </summary>
        /// <param name="patient">患者信息</param>
        /// <param name="personInfo">医保信息</param>
        /// <param name="feeDetails">费用明细</param>
        /// <param name="msg"></param>
        /// <param name="arrParm">住院结算界面相关参数</param>
        /// <returns></returns>
        public int YDInpatientFeeBalance(Neusoft.HISFC.Models.RADT.PatientInfo patient, ZhuHaiYDSI.Objects.SIPersonInfo personInfo,ref string msg)
        {

            string status = "1";

            string ser = "异地联网结算";
            string cas = "费用结算";

            string parXML = function.GetYDInpatientFeeBalanceXML(personInfo,patient);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return -1;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果
           
            function.GetResultStatusAndMessage(retXML, ref status, ref msg);

            if (status != "1")
            {
                //msg在获取返回值时已经赋值
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

        /// <summary>
        /// 工伤住院结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="personInfo"></param>
        /// <param name="feeDetails"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int BalanceGS(Neusoft.HISFC.Models.RADT.PatientInfo patient, ZhuHaiYDSI.Objects.SIPersonInfo personInfo, ref ArrayList feeDetails, ref string msg, ArrayList arrParm)
        {
            string status = "1";

            string ser = "医院联网结算";
            string cas = "工伤住院结算";

            string parXML = function.GetBalanceGsXML(patient, personInfo, feeDetails, arrParm);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return -1;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            function.GetResultStatusAndMessage(retXML, ref status, ref msg);

            if (status == null || string.IsNullOrEmpty(status) || status.Equals("3"))
            {
                //msg在获取返回值时已经赋值
                return -1;
            }

            ZhuHaiYDSI.Objects.OutBalanceInfo outBalanceInfo = new ZhuHaiYDSI.Objects.OutBalanceInfo();
            //TODO:获取返回值         
            function.GetInPatientBalanceGsResult(retXML, patient, ref personInfo, ref outBalanceInfo);

            //TODO:操作数据库
            if (this.SIMgr.InsertSIMainInfoInpatient(patient, personInfo) < 0)
            {
                status = "-1";
                msg = SIMgr.Err;
                return -1;
            }

            return 1;
        }


        /// <summary>
        /// 【异地】住院结算取消
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CancelYDBalanceInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref string msg)
        {
            ZhuHaiYDSI.Objects.SIPersonInfo personInfo = new ZhuHaiYDSI.Objects.SIPersonInfo();
            //查询参保险种
            if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 1)
            {
                msg = "\n异地医保" + "\n查询患者入院登记信息出错" + "\n" + this.SIMgr.Err;
                return -1;
            }


            string status = "1";

            string ser = "异地联网结算";
            string cas = "费用结算回退";

            string parXML = function.GetCancelBalanceXML(patient, personInfo);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return -1;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            if (function.GetResultStatusAndMessage(retXML, ref status, ref msg) < 0) ;
            {
                return -1;
            }


            //TODO:操作数据库
            //更新医保结算表有效位         
            if (this.SIMgr.UpdateSiinmaininfoValidFlag(patient, personInfo, "0") < 1)
            {
                msg = "\n珠海异地医保" + "\n更新医保主表出错" + "\n" + this.SIMgr.Err;
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
        public int CancelUploadBalanceItem(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref string msg)
        {
            ZhuHaiYDSI.Objects.SIPersonInfo personInfo = new ZhuHaiYDSI.Objects.SIPersonInfo();
            //查询参保险种
            if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 1)
            {
                msg = "\n异地医保" + "\n查询患者入院登记信息出错" + "\n" + this.SIMgr.Err;
                return -1;
            }


            string status = "1";

            string ser = "异地联网结算";
            string cas = "费用明细回退";

            string parXML = function.GetCancelBalanceItemXML(patient, personInfo);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return -1;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            if (function.GetResultStatusAndMessage(retXML, ref status, ref msg) < 1)
            {
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
        public int CancelOutReg(Neusoft.HISFC.Models.RADT.PatientInfo patient, ref string msg)
        {
            ZhuHaiYDSI.Objects.SIPersonInfo personInfo = new ZhuHaiYDSI.Objects.SIPersonInfo();
            //查询参保险种
            if (this.SIMgr.QueryInPatientRegInfo(patient.ID, ref personInfo) < 1)
            {
                msg = "\n异地医保" + "\n查询患者入院登记信息出错" + "\n" + this.SIMgr.Err;
                return -1;
            }


            string status = "1";

            string ser = "异地联网结算";
            string cas = "出院登记回退";

            string parXML = function.GetCancelBalanceItemXML(patient, personInfo);//生成XML

            string sig = function.GetSig(ser, function.DEP, cas, parXML);

            if ("-1".Equals(sig))
            {
                status = "-1";
                msg = "未能读取电子签名";
                return -1;
            }

            string retXML = function.Provider(ser, function.DEP, cas, parXML, function.CER, sig);//获得返回结果

            if (function.GetResultStatusAndMessage(retXML, ref status, ref msg) < 1)
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
        public int QuerySiBalanceInfo(ref Neusoft.HISFC.Models.RADT.PatientInfo patient, ref ZhuHaiYDSI.Objects.SIPersonInfo personInfo, ref string msg)
        {
            //查询参保险种
            if (this.SIMgr.QuerySiBalanceInfo(ref patient, ref personInfo) < 1)
            {
                msg = "\n珠海医保" + "\n查询珠海医保结算信息失败！" + "\n" + this.SIMgr.Err;
                return -1;
            }

            if (!personInfo.BalanceFlag.Equals("1"))
            {
                msg = "\n珠海医保" + "\n未查询到医保结算信息，请先进行珠海医保医保结算！";
                return -1;
            }
            return 1;
        }

    }
}
