using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// Web Service服务类
    /// </summary>
    public class WSHelper
    {

        /// <summary>
        /// 患者信息业务类
        /// </summary>
        private static FS.HISFC.BizProcess.Integrate.RADT radtProcess = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 参数控制类
        /// </summary>
        private static FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 常数控制类
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 调用web代理类
        /// </summary>
        private static IbornMobileService ibornMobileService = new IbornMobileService();

        /// < summary> 
        /// 动态调用web服务 （不含有SoapHeader）
        /// < /summary> 
        /// < param name="url">WSDL服务地址< /param> 
        /// < param name="methodname">方法名< /param> 
        /// < param name="args">参数< /param> 
        /// < returns>< /returns> 
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return WSHelper.InvokeWebService(url, null, methodname, null, args);
        }
        /// <summary>
        /// 动态调用web服务（含有SoapHeader）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="methodname"></param>
        /// <param name="soapHeader"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object InvokeWebService(string url, string methodname, SoapHeader soapHeader, object[] args)
        {
            return WSHelper.InvokeWebService(url, null, methodname, soapHeader, args);
        }
        /// < summary> 
        /// 动态调用web服务 
        /// < /summary> 
        /// < param name="url">WSDL服务地址< /param> 
        /// < param name="classname">类名< /param> 
        /// < param name="methodname">方法名< /param> 
        /// < param name="args">参数< /param> 
        /// < returns>< /returns> 
        public static object InvokeWebService(string url, string classname, string methodname, SoapHeader soapHeader, object[] args)
        {
            string @namespace = "FS.HISFC.BizProcess.Integrate";
            if ((classname == null) || (classname == ""))
            {
                classname = WSHelper.GetWsClassName(url);
            }
            try
            {
                //获取WSDL 
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);

                //生成客户端代理类代码 
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider icc = new CSharpCodeProvider();

                //设定编译参数 
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                //编译代理类 
                CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                //保存生产的代理类，默认是保存在bin目录下面，调试的时候需要  
                //TextWriter writer = File.CreateText("MyWebServices.cs");
                //icc.GenerateCodeFromCompileUnit(ccu, writer, null);
                //writer.Flush();
                //writer.Close();

                //生成代理实例 
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);

                #region 设置SoapHeader
                FieldInfo client = null;
                object clientkey = null;
                if (soapHeader != null)
                {
                    client = t.GetField(soapHeader.ClassName + "Value");
                    //获取客户端验证对象    soap类  
                    Type typeClient = assembly.GetType(@namespace + "." + soapHeader.ClassName);
                    //为验证对象赋值  soap实例    
                    clientkey = Activator.CreateInstance(typeClient);
                    //遍历属性  
                    foreach (KeyValuePair<string, object> property in soapHeader.Properties)
                    {
                        typeClient.GetField(property.Key).SetValue(clientkey, property.Value);
                        // typeClient.GetProperty(property.Key).SetValue(clientkey, property.Value, null);  
                    }
                }
                #endregion

                if (soapHeader != null)
                {
                    //设置Soap头  
                    client.SetValue(obj, clientkey);
                    //pro.SetValue(obj, soapHeader, null);  
                }

                //调用指定的方法
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);
                //方法名错误（找不到方法），给出提示
                if (null == mi)
                {
                    return "方法名不存在，请检查方法名是否正确！";
                }
                return mi.Invoke(obj, args);
                // PropertyInfo propertyInfo = type.GetProperty(propertyname); 
                //return propertyInfo.GetValue(obj, null); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }
        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }

        /// <summary>  
        /// 构建SOAP头，用于SoapHeader验证  
        /// </summary>  
        public class SoapHeader
        {
            /// <summary>  
            /// 构造一个SOAP头  
            /// </summary>  
            public SoapHeader()
            {
                this.Properties = new Dictionary<string, object>();
            }
            /// <summary>  
            /// 构造一个SOAP头  
            /// </summary>  
            /// <param name="className">SOAP头的类名</param>  
            public SoapHeader(string className)
            {
                this.ClassName = className;
                this.Properties = new Dictionary<string, object>();
            }
            /// <summary>  
            /// 构造一个SOAP头  
            /// </summary>  
            /// <param name="className">SOAP头的类名</param>  
            /// <param name="properties">SOAP头的类属性名及属性值</param>  
            public SoapHeader(string className, Dictionary<string, object> properties)
            {
                this.ClassName = className;
                this.Properties = properties;
            }
            /// <summary>  
            /// SOAP头的类名  
            /// </summary>  
            public string ClassName { get; set; }
            /// <summary>  
            /// SOAP头的类属性名及属性值  
            /// </summary>  
            public Dictionary<string, object> Properties { get; set; }
            /// <summary>  
            /// 为SOAP头增加一个属性及值  
            /// </summary>  
            /// <param name="name">SOAP头的类属性名</param>  
            /// <param name="value">SOAP头的类属性值</param>  
            public void AddProperty(string name, object value)
            {
                if (this.Properties == null)
                {
                    this.Properties = new Dictionary<string, object>();
                }
                Properties.Add(name, value);
            }
        }


        #region 辅助函数

        /// <summary>
        /// 获取url{D6F8B61E-4738-47ec-A840-DBDB3AD5EFE8}private改成了public，外面借用一下
        /// </summary>
        /// <param name="hospitalID"></param>
        /// <param name="urlType"></param>
        /// <returns></returns>
        public static string GetUrl(string hospitalID, URLTYPE urlType)
        {
            string url = string.Empty;

            try
            {
                //{09B08BE4-9AC8-4094-9D25-0CA5B8C615AB}
                FrameWork.Models.NeuObject cst = constManager.GetConstant("URLLIST", "1");
                url = cst.Name;
            }
            catch (Exception ex)
            {
                url = string.Empty;
            }

            return url;

            //if (hospitalID == "IBORNGZ" || hospitalID == "IBORNCLINIC")
            //{
            //    url = @"http://192.168.34.9:8020/IbornMobileService.asmx";
            //}
            //else if (hospitalID == "IBORNSD")
            //{
            //    url = @"http://10.20.10.220:8020/IbornMobileService.asmx";
            //}

        }

        /// <summary>
        /// 获取函数名
        /// </summary>
        /// <param name="methodType"></param>
        /// <returns></returns>
        private static string GetMethod(string methodType)
        {
            if (string.IsNullOrEmpty(methodType))
            {
                return "";
            }

            string rtn = "";

            switch (methodType)
            {
                case "0":
                    rtn = "GetCouponAccount";
                    break;
                case "1":
                    rtn = "OperateCoupon";
                    break;
                case "2":
                    rtn = "CostCoupon";
                    break;
                case "3":
                    rtn = "GetCouponNum";
                    break;
                default:
                    break;
            }

            return rtn;
        }

        /// <summary>
        /// 获取查询账户报文格式
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        private static string ConstructAccountInfoReq(string patientID)
        {
            string req = @"<?xml version='1.0' encoding='UTF-8'?>
                             <req>
                                <patientId>{0}</patientId>              
                             </req>";

            req = string.Format(req, patientID);
            return req;
        }

        /// <summary>
        /// {6481187A-826A-40d7-8548-026C8C501B3E}
        /// 获取查询账户报文格式
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        private static string ConstructAccountInfoPersonalReq(string patientID)
        {
            string req = @"<?xml version='1.0' encoding='UTF-8'?>
                             <req>
                                <patientId>{0}</patientId>  
                                <personalFlag>1</personalFlag>               
                             </req>";

            req = string.Format(req, patientID);
            return req;
        }

        /// <summary>
        /// 积分倍数报文
        /// </summary>
        /// <param name="hospitalId"></param>
        /// <param name="invoiceNO"></param>
        /// <returns></returns>
        private static string GetCouponReq(string hospitalId, string invoiceNO, string meth)
        {
            string req = @"<?xml version='1.0' encoding='UTF-8'?>
                            <relay>
                             <req>
                                <hospitalId>{0}</hospitalId>  
                                <invoiceNO>{1}</invoiceNO>               
                             </req>
                             <method>{2}</method>
                            </relay>";
            req = string.Format(req, hospitalId, invoiceNO, meth);
            return req;
        }

        /// <summary>
        /// 获取积分报文格式
        /// </summary>
        /// <param name="hospitalID"></param>
        /// <param name="hospitalName"></param>
        /// <param name="crmID"></param>
        /// <param name="patientName"></param>
        /// <param name="couponType"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="amout"></param>
        /// <param name="operCode"></param>
        /// <param name="isFirst"></param>
        /// <returns></returns>
        private static string ConstructReq(string hospitalID,
            string hospitalName,
            string patientId,
            string patientName,
            string couponType,
            string invoiceNO,
            decimal amount,
            decimal delegateAmount,
            string operCode, string oldinvoiceno, string iscouponnum)
        {
            //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
            string req = @"<?xml version='1.0' encoding='UTF-8'?>
                             <req>　                                 
                               <hospitalId>{0}</hospitalId>      
                               <hospitalName>{1}</hospitalName>  
                               <patientId>{2}</patientId>              
                               <patientName>{3}</patientName>     
                               <couponType>{4}</couponType>     
                               <invoiceNO>{5}</invoiceNO>        
                               <amount>{6}</amount>
                               <donateAmount>0</donateAmount>      
                               <delegateAmount>{7}</delegateAmount>                          
                               <operCode>{8}</operCode>   
                               <oldinvoiceNO>{9}</oldinvoiceNO>    
                               <iscouponNum>{10}</iscouponNum>         
                             </req>";

            req = string.Format(req, hospitalID, hospitalName, patientId, patientName, couponType, invoiceNO, amount.ToString(), delegateAmount.ToString(), operCode, oldinvoiceno, iscouponnum);
            return req;
        }

        /// <summary>
        /// 获取积分消费报文格式
        /// </summary>
        /// <param name="hospitalID"></param>
        /// <param name="hospitalName"></param>
        /// <param name="patientId"></param>
        /// <param name="patientName"></param>
        /// <param name="costPatientId"></param>
        /// <param name="costPatientName"></param>
        /// <param name="costType"></param>
        /// <param name="costAmount"></param>
        /// <param name="costDonateAmount"></param>
        /// <param name="costInvoiceNO"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        private static string ConstructCostReq(string hospitalID,
            string hospitalName,
            string patientId,
            string patientName,
            string costPatientId,
            string costPatientName,
            string costType,
            decimal costAmount,
            decimal costDonateAmount,
            string costInvoiceNO,
            string operCode)
        {
            string req = @"<?xml version='1.0' encoding='UTF-8'?>
                            <req>　                                 
                              <hospitalId>{0}</hospitalId>      
                              <hospitalName>{1}</hospitalName>  
                              <patientId>{2}</patientId>                
                              <patientName>{3}</patientName>            
                              <costPatientId>{4}</costPatientId>        
                              <costPatientName>{5}</costPatientName>    
                              <costType>{6}</costType>                  
                              <costAmount>{7}</costAmount>              
                              <costDonateAmount>{8}</costDonateAmount>  
                              <costInvoiceNO>{9}</costInvoiceNO>        
                              <operCode>{10}</operCode>                  
                            </req>";

            req = string.Format(req, hospitalID, hospitalName, patientId, patientName, costPatientId, costPatientName,
                                costType, costAmount.ToString(), costDonateAmount.ToString(), costInvoiceNO, operCode);

            return req;
        }


        /// <summary>
        /// 解析普通请求返回报文
        /// </summary>
        /// <param name="resultXml"></param>
        /// <param name="ResultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        private static int ParseXml(string resultXml, out string ResultCode, out string ErrorMsg)
        {
            try
            {
                System.Xml.XmlDocument docbind = new System.Xml.XmlDocument();
                docbind.LoadXml(resultXml);
                System.Xml.XmlNode ResultCodeNode = docbind.SelectSingleNode("/res/resultCode");
                System.Xml.XmlNode ResultDescNode = docbind.SelectSingleNode("/res/resultDesc");

                if (!string.IsNullOrEmpty(ResultCodeNode.InnerText))
                {
                    ResultCode = ResultCodeNode.InnerText;
                    ErrorMsg = ResultDescNode.InnerText;
                }
                else
                {
                    ResultCode = "1";
                    ErrorMsg = "返回状态代码为空！";
                }
            }
            catch (Exception ex)
            {
                ResultCode = "1";
                ErrorMsg = "解析返回结果发生异常！";
            }

            if (ResultCode != "0")
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 解析账户返回报文
        /// </summary>
        /// <param name="resultXml"></param>
        /// <param name="ResultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        private static Dictionary<string, string> ParseAccountInfoXml(string resultXml, out string ResultCode, out string ErrorMsg, out string AccountInfo)
        {
            try
            {
                System.Xml.XmlDocument docbind = new System.Xml.XmlDocument();
                docbind.LoadXml(resultXml);
                System.Xml.XmlNode ResultCodeNode = docbind.SelectSingleNode("/res/resultCode");
                System.Xml.XmlNode ResultDescNode = docbind.SelectSingleNode("/res/resultDesc");
                System.Xml.XmlNode AccountInfoNode = docbind.SelectSingleNode("/res/accountInfo");

                if (!string.IsNullOrEmpty(ResultCodeNode.InnerText))
                {
                    ResultCode = ResultCodeNode.InnerText;
                    ErrorMsg = ResultDescNode.InnerText;
                    AccountInfo = AccountInfoNode.InnerXml;

                    Dictionary<string, string> pDic = new Dictionary<string, string>();
                    foreach (System.Xml.XmlNode node in docbind.SelectNodes("/res/accountInfo/*"))
                    {
                        pDic.Add(node.Name, node.InnerText);
                    }

                    return pDic;
                }
                else
                {
                    ResultCode = "1";
                    ErrorMsg = "返回状态代码为空！";
                    AccountInfo = "";
                    return null;
                }
            }
            catch (Exception ex)
            {
                ResultCode = "1";
                ErrorMsg = "解析返回结果发生异常！";
                AccountInfo = "";
                return null;
            }
        }

        #endregion

        /// <summary>
        /// 查询账户
        /// </summary>
        /// <param name="CardNO"></param>
        /// <param name="resultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public static Dictionary<string, string> QueryAccountInfo(string cardNO, out string resultCode, out string errorMsg)
        {
            try
            {
                //积分模块是否启用控制参数
                //未启用时正常返回，不影响正常流程
                bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";
                if (!IsCouponModuleInUse)
                {
                    resultCode = "0";
                    errorMsg = "";
                    Dictionary<string, string> tmp = new Dictionary<string, string>();
                    tmp.Add("couponvacancy", "0");
                    return tmp;
                }

                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                FS.HISFC.Models.RADT.PatientInfo patient = radtProcess.QueryComPatientInfo(cardNO);
                string accountInfo = "";
                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);
                string req = FS.HISFC.BizProcess.Integrate.WSHelper.ConstructAccountInfoReq(patient.CrmID);
                string method = FS.HISFC.BizProcess.Integrate.WSHelper.GetMethod("0");
                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, method, new string[] { req }) as string;
                Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.ParseAccountInfoXml(resultXml, out resultCode, out errorMsg, out accountInfo);

                if (resultCode != "0")
                {
                    return null;
                }

                return dic;
            }
            catch (Exception ex)
            {
                resultCode = "1";
                errorMsg = ex.Message;
                return null;
            }
        }

        //{1093FFDD-31BD-45c1-9810-3D8AD28BFE89}
        /// <summary>
        ///更新预产期
        /// </summary>
        /// <param name="CardNO"></param>
        /// <param name="resultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public static string updatePatientInfoByExceptedTime(string reqInfo)
        {
            try
            {
                bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";
                if (!IsCouponModuleInUse)
                {
                    return "";
                }

                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);
                string req = reqInfo;// FS.HISFC.BizProcess.Integrate.WSHelper.ConstructAccountInfoReq(patient.CrmID);
                string method = "updatePatientInfoByExceptedTime";// FS.HISFC.BizProcess.Integrate.WSHelper.GetMethod("0");
                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, method, new string[] { req }) as string;
                return resultXml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取CRM预产期
        /// </summary>
        /// <param name="reqInfo"></param>
        /// <returns></returns>
        public static string GetPatientInfoExceptedTime(string reqInfo)
        {
            try
            {

                bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";
                if (!IsCouponModuleInUse)
                {
                    return "";
                }

                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);
                string method = "GetPatientInfoExceptedTime";
                string req = reqInfo;
                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, method, new string[] { req }) as string;


                System.Xml.XmlDocument docbind = new System.Xml.XmlDocument();
                docbind.LoadXml(resultXml);
                System.Xml.XmlNode ResultCodeNode = docbind.SelectSingleNode("/res/resultCode");
                System.Xml.XmlNode ResultDescNode = docbind.SelectSingleNode("/res/resultDesc");


                return ResultDescNode.InnerText;
            }
            catch (Exception ex)
            {
                return "";
            }

        }


        /// <summary>
        ///航天发票
        /// </summary>
        /// <param name="CardNO"></param>
        /// <param name="resultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public static string HanTianInvoice(string reqInfo, string methodname, string crmMethodName)
        {
            try
            {

                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);
                string req = reqInfo;// FS.HISFC.BizProcess.Integrate.WSHelper.ConstructAccountInfoReq(patient.CrmID);
                string method = methodname;// FS.HISFC.BizProcess.Integrate.WSHelper.GetMethod("0");
                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, method, new string[] { req, crmMethodName }) as string;
                return resultXml;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// {6481187A-826A-40d7-8548-026C8C501B3E}
        /// 查询账户
        /// </summary>
        /// <param name="CardNO"></param>
        /// <param name="resultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public static Dictionary<string, string> QueryAccountInfoPersonal(string cardNO, out string resultCode, out string errorMsg)
        {
            try
            {
                //积分模块是否启用控制参数
                //未启用时正常返回，不影响正常流程
                bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";
                if (!IsCouponModuleInUse)
                {
                    resultCode = "0";
                    errorMsg = "";
                    Dictionary<string, string> tmp = new Dictionary<string, string>();
                    tmp.Add("couponvacancy", "0");
                    return tmp;
                }

                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                FS.HISFC.Models.RADT.PatientInfo patient = radtProcess.QueryComPatientInfo(cardNO);
                string accountInfo = "";
                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);
                string req = FS.HISFC.BizProcess.Integrate.WSHelper.ConstructAccountInfoPersonalReq(patient.CrmID);
                string method = FS.HISFC.BizProcess.Integrate.WSHelper.GetMethod("0");
                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, method, new string[] { req }) as string;
                Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.ParseAccountInfoXml(resultXml, out resultCode, out errorMsg, out accountInfo);

                if (resultCode != "0")
                {
                    return null;
                }

                return dic;
            }
            catch (Exception ex)
            {
                resultCode = "1";
                errorMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取积分倍数    {c2d43b9d-eda4-4f87-92c1-f6fdb08d9d04}
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="resultCode"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>

        public static Dictionary<string, string> QueryCouponNum(string invoiceNO, out string resultCode, out string errorMsg)
        {
            try
            {
                //积分模块是否启用控制参数
                //未启用时正常返回，不影响正常流程
                bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";
                if (!IsCouponModuleInUse)
                {
                    resultCode = "0";
                    errorMsg = "";
                    Dictionary<string, string> tmp = new Dictionary<string, string>();
                    tmp.Add("couponNum", "1");
                    return tmp;
                }
                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }
                string accountInfo = "";
                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);
                string method = FS.HISFC.BizProcess.Integrate.WSHelper.GetMethod("3");
                //{f291dfdf-ad8d-427d-8c84-81218afefdd0}
                string req = FS.HISFC.BizProcess.Integrate.WSHelper.GetCouponReq(hospitalCode, invoiceNO, method);
                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "crmRelay", new string[] { req }) as string;
                Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.ParseAccountInfoXml(resultXml, out resultCode, out errorMsg, out accountInfo);

                if (resultCode != "0")
                {
                    return null;
                }
                if (dic != null && dic.ContainsKey("couponNum") && string.IsNullOrEmpty(dic["couponNum"]))
                {
                    dic["couponNum"] = "1";
                }
                return dic;


            }
            catch (Exception ex)
            {

                resultCode = "1";
                errorMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 适配门诊折扣方面
        /// </summary>
        /// <returns></returns>

        public static int QueryAccountDiscount(string cardNO, string depts, out decimal discount, out string levelID, out string levelName, out string resultCode, out string errorMsg)
        {
            try
            {
                discount = 1m;
                levelID = "0";
                levelName = "普通会员";
                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;

                //积分模块是否启用控制参数
                //未启用时正常返回，不影响正常流程
                //{09B08BE4-9AC8-4094-9D25-0CA5B8C615AB}
                bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";
                if (!IsCouponModuleInUse)
                {
                    resultCode = "0";
                    errorMsg = "";
                    return 1;
                }

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                FS.HISFC.Models.RADT.PatientInfo patient = radtProcess.QueryComPatientInfo(cardNO);

                if (string.IsNullOrEmpty(patient.CrmID))
                {
                    resultCode = "1";
                    errorMsg = "该患者未绑定会员系统编码";
                    discount = 1m;
                    levelID = "0";
                    levelName = "普通会员";
                    return -1;
                }

                string accountInfo = "";
                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);
                string req = FS.HISFC.BizProcess.Integrate.WSHelper.ConstructAccountInfoReq(patient.CrmID);
                string method = FS.HISFC.BizProcess.Integrate.WSHelper.GetMethod("0");

                //string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, method, new string[] { req }) as string;

                //{09B08BE4-9AC8-4094-9D25-0CA5B8C615AB}
                ibornMobileService.Url = url;
                string resultXml = ibornMobileService.GetCouponAccount(req);

                Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.ParseAccountInfoXml(resultXml, out resultCode, out errorMsg, out accountInfo);

                if (dic.ContainsKey("patientlevel"))
                {
                    levelID = dic["patientlevel"].ToString();
                }
                else
                {
                    return -1;
                }

                if (string.IsNullOrEmpty(levelID))
                {
                    levelID = "0";
                }


                FS.FrameWork.Models.NeuObject obj = constManager.GetConstant("MEMBERLEVEL", levelID);

                if (levelID == "2")
                {
                    FS.FrameWork.Models.NeuObject objdept = constManager.GetConstant("MEMBERLEVELDEPT", levelID);


                    if (!string.IsNullOrEmpty(depts) && objdept.Name.Contains(depts))
                    {
                        levelName = obj.Name;
                        discount = decimal.Parse(objdept.Memo);
                    }
                    else
                    {
                        levelName = obj.Name;
                        discount = decimal.Parse(obj.Memo);
                    }

                }
                else
                {
                    levelName = obj.Name;
                    discount = decimal.Parse(obj.Memo);
                }

                return 1;
            }
            catch (Exception ex)
            {
                resultCode = "1";
                errorMsg = ex.Message;
                discount = 1m;
                levelID = "0";
                levelName = "普通会员";
                return -1;
            }

        }

        /// <summary>
        /// 查询账户
        /// </summary>
        /// <param name="CardNO"></param>
        /// <param name="resultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public static int QueryAccountDiscount(string cardNO, out decimal discount, out string levelID, out string levelName, out string resultCode, out string errorMsg)
        {
            try
            {
                discount = 1m;
                levelID = "0";
                levelName = "普通会员";
                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;

                //积分模块是否启用控制参数
                //未启用时正常返回，不影响正常流程
                //{09B08BE4-9AC8-4094-9D25-0CA5B8C615AB}
                bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";
                if (!IsCouponModuleInUse)
                {
                    resultCode = "0";
                    errorMsg = "";
                    return 1;
                }

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                FS.HISFC.Models.RADT.PatientInfo patient = radtProcess.QueryComPatientInfo(cardNO);

                if (string.IsNullOrEmpty(patient.CrmID))
                {
                    resultCode = "1";
                    errorMsg = "该患者未绑定会员系统编码";
                    discount = 1m;
                    levelID = "0";
                    levelName = "普通会员";
                    return -1;
                }

                string accountInfo = "";
                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);
                string req = FS.HISFC.BizProcess.Integrate.WSHelper.ConstructAccountInfoReq(patient.CrmID);
                string method = FS.HISFC.BizProcess.Integrate.WSHelper.GetMethod("0");

                //string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, method, new string[] { req }) as string;

                //{09B08BE4-9AC8-4094-9D25-0CA5B8C615AB}
                ibornMobileService.Url = url;
                string resultXml = ibornMobileService.GetCouponAccount(req);

                Dictionary<string, string> dic = FS.HISFC.BizProcess.Integrate.WSHelper.ParseAccountInfoXml(resultXml, out resultCode, out errorMsg, out accountInfo);

                if (dic.ContainsKey("patientlevel"))
                {
                    levelID = dic["patientlevel"].ToString();
                }
                else
                {
                    return -1;
                }

                if (string.IsNullOrEmpty(levelID))
                {
                    levelID = "0";
                }

                //{BFB97C9E-18C2-41e3-9827-595DD947756D}
                FS.FrameWork.Models.NeuObject obj = constManager.GetConstant("MEMBERLEVEL", levelID);
                levelName = obj.Name;
                discount = decimal.Parse(obj.Memo);

                //switch (levelID)
                //{
                //    case "0":
                //        discount = 1;
                //        levelName = "普通会员";
                //        break;
                //    case "1":
                //        discount = 0.98m;
                //        levelName = "银卡会员";
                //        break;
                //    case "2":
                //        discount = 0.97m;
                //        levelName = "金卡会员";
                //        break;
                //    case "3":
                //        discount = 0.94m;
                //        levelName = "白金卡会员";
                //        break;
                //    default:
                //        discount = 1;
                //        break;

                //}

                return 1;
            }
            catch (Exception ex)
            {
                resultCode = "1";
                errorMsg = ex.Message;
                discount = 1m;
                levelID = "0";
                levelName = "普通会员";
                return -1;
            }
        }

        /// <summary>
        /// 在各模块消费或者退费时产生积分
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="hospitalID"></param>
        /// <param name="hospitalName"></param>
        /// <param name="patientId"></param>
        /// <param name="patientName"></param>
        /// <param name="couponType"></param>
        /// <param name="invoiceNO"></param>
        /// <param name="amount"></param>
        /// <param name="delegateAmount"></param>
        /// <param name="resultCode"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static int OperateCoupon(string cardNO,
            string patientName,
            string couponType,
            string invoiceNO,
            decimal amount,
            decimal delegateAmount,
            out string resultCode,
            out string errorMsg)
        {
            try
            {
                //积分模块是否启用控制参数
                //未启用时正常返回，不影响正常流程
                bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";
                if (!IsCouponModuleInUse)
                {
                    resultCode = "0";
                    errorMsg = "";
                    return 1;
                }

                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;
                HISFC.Models.RADT.PatientInfo patientInfo = radtProcess.QueryComPatientInfo(cardNO);

                if (patientInfo == null || string.IsNullOrEmpty(patientInfo.CrmID))
                {
                    resultCode = "1";
                    errorMsg = "在会员系统中找不到对应的患者信息！";
                    return -1;
                }

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, URLTYPE.HIS);
                //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
                string req = FS.HISFC.BizProcess.Integrate.WSHelper.ConstructReq(hospitalCode, dept.HospitalName, patientInfo.CrmID, patientName, couponType, invoiceNO, amount, delegateAmount, empl.ID, null, null);
                string method = FS.HISFC.BizProcess.Integrate.WSHelper.GetMethod("1");
                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, method, new string[] { req }) as string;
                return FS.HISFC.BizProcess.Integrate.WSHelper.ParseXml(resultXml, out resultCode, out errorMsg);
            }
            catch (Exception ex)
            {
                resultCode = "1";
                errorMsg = ex.Message;
                return -1;
            }
        }


        /// <summary>
        /// 在各模块消费或者退费时产生积分(存在多倍积分退费重收时使用)
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="hospitalID"></param>
        /// <param name="hospitalName"></param>
        /// <param name="patientId"></param>
        /// <param name="patientName"></param>
        /// <param name="couponType"></param>
        /// <param name="invoiceNO">现发票</param>
        /// <param name="amount"></param>
        /// <param name="delegateAmount"></param>
        /// <param name="oldinvoiceno">原发票</param>
        /// <param name="resultCode"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static int OperateCoupon(string cardNO,
            string patientName,
            string couponType,
            string invoiceNO,
            decimal amount,
            decimal delegateAmount,
            string oldinvoiceno,
            string iscoupon,
            out string resultCode,
            out string errorMsg)
        {
            try
            {
                //积分模块是否启用控制参数
                //未启用时正常返回，不影响正常流程
                bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";
                if (!IsCouponModuleInUse)
                {
                    resultCode = "0";
                    errorMsg = "";
                    return 1;
                }

                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;
                HISFC.Models.RADT.PatientInfo patientInfo = radtProcess.QueryComPatientInfo(cardNO);

                if (patientInfo == null || string.IsNullOrEmpty(patientInfo.CrmID))
                {
                    resultCode = "1";
                    errorMsg = "在会员系统中找不到对应的患者信息！";
                    return -1;
                }

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, URLTYPE.HIS);
                //{3B9D0100-377E-48ac-AF31-4412CDCBF4B0}
                string req = FS.HISFC.BizProcess.Integrate.WSHelper.ConstructReq(hospitalCode, dept.HospitalName, patientInfo.CrmID, patientName, couponType, invoiceNO, amount, delegateAmount, empl.ID, oldinvoiceno, iscoupon);
                string method = FS.HISFC.BizProcess.Integrate.WSHelper.GetMethod("1");
                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, method, new string[] { req }) as string;
                return FS.HISFC.BizProcess.Integrate.WSHelper.ParseXml(resultXml, out resultCode, out errorMsg);
            }
            catch (Exception ex)
            {
                resultCode = "1";
                errorMsg = ex.Message;
                return -1;
            }
        }




        /// <summary>
        /// 在各模块使用消费或者退费时使用积分{c2d43b9d-eda4-4f87-92c1-f6fdb08d9d04}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="patientId"></param>
        /// <param name="patientName"></param>
        /// <param name="costPatientId"></param>
        /// <param name="costPatientName"></param>
        /// <param name="costType"></param>
        /// <param name="costInvoiceNO"></param>
        /// <param name="costAmount"></param>
        /// <param name="costDonateAmount"></param>
        /// <param name="resultCode"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static int CostCoupon(string cardNO,
            string patientName,
            string costCardNO,
            string costPatientName,
            string costType,
            string costInvoiceNO,
            decimal costAmount,
            decimal costDonateAmount,
            out string resultCode,
            out string errorMsg)
        {
            try
            {
                //积分模块是否启用控制参数
                //未启用时正常返回，不影响正常流程
                bool IsCouponModuleInUse = ctlMgr.QueryControlerInfo("CP0001") == "1";
                if (!IsCouponModuleInUse)
                {
                    resultCode = "0";
                    errorMsg = "";
                    return 1;
                }


                string year = "20" + costInvoiceNO.Substring(0, 2);
                string month = costInvoiceNO.Substring(2, 2);
                string day = costInvoiceNO.Substring(4, 2);

                DateTime date = DateTime.Parse(year + "-" + month + "-" + day);
                DateTime beginDate = DateTime.Parse("2019-06-05");

                if (date < beginDate)
                {
                    resultCode = "0";
                    errorMsg = "";
                    return 1;
                }

                HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;
                HISFC.Models.RADT.PatientInfo patientInfo = radtProcess.QueryComPatientInfo(cardNO);
                HISFC.Models.RADT.PatientInfo costPatientInfo = radtProcess.QueryComPatientInfo(costCardNO);

                if (patientInfo == null || costPatientInfo == null || string.IsNullOrEmpty(patientInfo.CrmID) || string.IsNullOrEmpty(costPatientInfo.CrmID))
                {
                    resultCode = "1";
                    errorMsg = "在会员系统中找不到对应的患者信息！";
                    return -1;
                }

                string hospitalCode = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                }

                string url = FS.HISFC.BizProcess.Integrate.WSHelper.GetUrl(hospitalCode, FS.HISFC.BizProcess.Integrate.URLTYPE.HIS);
                string req = FS.HISFC.BizProcess.Integrate.WSHelper.ConstructCostReq(hospitalCode, dept.HospitalName, patientInfo.CrmID, patientInfo.Name,
                                                                                          costPatientInfo.CrmID, costPatientName, costType, costAmount * 100, costDonateAmount * 100,
                                                                                          costInvoiceNO, empl.ID);
                string method = FS.HISFC.BizProcess.Integrate.WSHelper.GetMethod("2");
                string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, method, new string[] { req }) as string;
                return FS.HISFC.BizProcess.Integrate.WSHelper.ParseXml(resultXml, out resultCode, out errorMsg);
            }
            catch (Exception ex)
            {
                resultCode = "1";
                errorMsg = ex.Message;
                return -1;
            }
        }

    }

    /// <summary>
    /// URL类型
    /// </summary>
    public enum URLTYPE
    {
        HIS = 0,                   //HIS
        CRM,                       //CRM
        MSG,                       //短信平台
        SJZ                        //时间轴
    }

}
