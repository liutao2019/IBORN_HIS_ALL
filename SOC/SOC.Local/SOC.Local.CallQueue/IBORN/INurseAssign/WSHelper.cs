﻿using System;
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

namespace FS.SOC.Local.CallQueue.IBORN.INurseAssign
{
    /// <summary>
    /// Web Service服务类
    /// </summary>
    public class WSHelper
    {
        /// <summary>
        /// 参数控制类
        /// </summary>
        private static FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

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
            string @namespace = "FS.SOC.Local.CallQueue.IBORN.INurseAssign";
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

        /// <summary>
        /// 获取url
        /// </summary>
        /// <param name="hospitalID"></param>
        /// <param name="urlType"></param>
        /// <returns></returns>
        private static string GetUrl(string hospitalID,URLTYPE urlType)
        {
            string url = string.Empty;

            if (hospitalID == "IBORNGZ" || hospitalID == "IBORNCLINIC")
            {
                url = @"http://192.168.34.9:8020/IbornMobileService.asmx";
            }
            else if (hospitalID == "IBORNSD")
            {
                url = @"http://10.20.10.220:8020/IbornMobileService.asmx";
            }

            return url;

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
        /// 叫号
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="patientID"></param>
        /// <param name="patientName"></param>
        /// <param name="ResultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public static int Call(string clinicNO,
                               string patientID,
                               string patientName,
                               string roomName,
                               out string ResultCode,
                               out string ErrorMsg)
        {
            ResultCode = "1";
            ErrorMsg = "";

            try
            {

                FS.HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.Models.Base.Department dept = empl.Dept as FS.HISFC.Models.Base.Department;

                //{E12342D9-42FE-4169-99F8-A54BD713D581}
                string req = @"<?xml version='1.0' encoding='UTF-8'?>
                            <req>　                              
                              <clinicNO>{0}</clinicNO>              
                              <patientId>{1}</patientId>            
                              <patientName>{2}</patientName>       
                              <deptName>{3}</deptName>               
                              <doctName>{4}</doctName>                
                              <roomName>{5}</roomName>              
                              <visitingTime>{6}</visitingTime>       
                              <operCode>{7}</operCode>              
                              <operName>{8}</operName>               
                              <operTime>{9}</operTime>               
                              <hospitalID>{10}</hospitalID>           
                              <hospitalName>{11}</hospitalName>      
                             </req>";

                string hospitalCode = string.Empty;
                string hospitalName = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                    hospitalName = "顺德爱博恩妇产医院";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                    hospitalName = "广州爱博恩妇产医院";
                }

                string timeNow = ctlMgr.GetDateTimeFromSysDateTime().ToString();

                string url = WSHelper.GetUrl(hospitalCode, URLTYPE.HIS);
                req = string.Format(req, clinicNO, patientID, patientName, dept.Name, empl.Name, roomName, timeNow, empl.ID, empl.Name, timeNow, hospitalCode, hospitalName);
                string resultXml = WSHelper.InvokeWebService(url, "MedicalReminder", new string[] { req }) as string;

                int rev = WSHelper.ParseXml(resultXml, out ResultCode, out ErrorMsg);

                if (ResultCode == "1")
                {
                    ErrorMsg = "叫号失败";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 诊出
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="patientID"></param>
        /// <param name="patientName"></param>
        /// <param name="ResultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public static int DiagOut(string clinicNO, out string ResultCode, out string ErrorMsg)
        {
            ResultCode = "1";
            ErrorMsg = "";

            try
            {

                FS.HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.Models.Base.Department dept = empl.Dept as FS.HISFC.Models.Base.Department;

                string req = @"<?xml version='1.0' encoding='UTF-8'?>
                                <req>　                   
                                  <clinicNO>{0}</clinicNO>            
                                  <operCode>{1}</operCode>             
                                  <operName>{2}</operName>             
                                  <operTime>{3}</operTime>            
                                  <hospitalID>{4}</hospitalID>        
                                  <hospitalName>{5}</hospitalName>     
                                </req>";

                string hospitalCode = string.Empty;
                string hospitalName = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                    hospitalName = "顺德爱博恩妇产医院";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                    hospitalName = "广州爱博恩妇产医院";
                }

                string timeNow = ctlMgr.GetDateTimeFromSysDateTime().ToString();

                string url = WSHelper.GetUrl(hospitalCode, URLTYPE.HIS);
                req = string.Format(req, clinicNO, empl.ID, empl.Name, timeNow, hospitalCode, hospitalName);
                string resultXml = WSHelper.InvokeWebService(url, "AssignSee", new string[] { req }) as string;

                int rev = WSHelper.ParseXml(resultXml, out ResultCode, out ErrorMsg);

                if (ResultCode == "1")
                {
                    ErrorMsg = "更新会员系统诊出信息失败";
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 取消诊出
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="patientID"></param>
        /// <param name="patientName"></param>
        /// <param name="ResultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public static int CancelDiagOut(string clinicNO, out string ResultCode, out string ErrorMsg)
        {
            ResultCode = "1";
            ErrorMsg = "";

            try
            {

                FS.HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.Models.Base.Department dept = empl.Dept as FS.HISFC.Models.Base.Department;

                string req = @"<?xml version='1.0' encoding='UTF-8'?>
                                <req>　                   
                                  <clinicNO>{0}</clinicNO>            
                                  <operCode>{1}</operCode>             
                                  <operName>{2}</operName>             
                                  <operTime>{3}</operTime>            
                                  <hospitalID>{4}</hospitalID>        
                                  <hospitalName>{5}</hospitalName>     
                                </req>";

                string hospitalCode = string.Empty;
                string hospitalName = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                    hospitalName = "顺德爱博恩妇产医院";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                    hospitalName = "广州爱博恩妇产医院";
                }

                string timeNow = ctlMgr.GetDateTimeFromSysDateTime().ToString();

                string url = WSHelper.GetUrl(hospitalCode, URLTYPE.HIS);
                req = string.Format(req, clinicNO, empl.ID, empl.Name, timeNow, hospitalCode, hospitalName);
                string resultXml = WSHelper.InvokeWebService(url, "CancelAssignSee", new string[] { req }) as string;

                int rev = WSHelper.ParseXml(resultXml, out ResultCode, out ErrorMsg);

                if (ResultCode == "1")
                {
                    ErrorMsg = "更新会员系统诊出信息失败";
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 延迟叫号
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="patientID"></param>
        /// <param name="patientName"></param>
        /// <param name="ResultCode"></param>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public static int DelayCall(string clinicNO, out string ResultCode, out string ErrorMsg)
        {
            ResultCode = "1";
            ErrorMsg = "";

            try
            {

                FS.HISFC.Models.Base.Employee empl = FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.Models.Base.Department dept = empl.Dept as FS.HISFC.Models.Base.Department;

                string req = @"<?xml version='1.0' encoding='UTF-8'?>
                                <req>
                                  <clinicNO>{0}</clinicNO>
                                  <operCode>{1}</operCode>
                                  <operName>{2}</operName>
                                  <operTime>{3}</operTime>
                                  <hospitalID>{4}</hospitalID>
                                  <hospitalName>{5}</hospitalName>
                                </req>";

                string hospitalCode = string.Empty;
                string hospitalName = string.Empty;

                if (dept.HospitalName.Contains("顺德"))
                {
                    hospitalCode = "IBORNSD";
                    hospitalName = "顺德爱博恩妇产医院";
                }
                else
                {
                    hospitalCode = "IBORNGZ";
                    hospitalName = "广州爱博恩妇产医院";
                }

                string timeNow = ctlMgr.GetDateTimeFromSysDateTime().ToString();

                string url = WSHelper.GetUrl(hospitalCode, URLTYPE.HIS);
                req = string.Format(req, clinicNO, empl.ID, empl.Name, timeNow, hospitalCode, hospitalName);
                string resultXml = WSHelper.InvokeWebService(url, "AssignPass", new string[] { req }) as string;

                int rev = WSHelper.ParseXml(resultXml, out ResultCode, out ErrorMsg);

                if (ResultCode == "1")
                {
                    ErrorMsg = "更新会员系统过号信息失败";
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }

            return 1;
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