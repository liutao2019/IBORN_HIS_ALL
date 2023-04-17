using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Web.Services.Description;

namespace FoshanSiService
{
    /// <summary>
    /// SiService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SiService : System.Web.Services.WebService
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public SiService()
        {
        }

        /// <summary>
        /// 3、 医疗机构门诊/住院医保实时审核接口
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "3、 医疗机构门诊/住院医保实时审核接口")]
        public string Audit4HospitalPortalOLD(string req)
        {
            string result = Function.Audit4HospitalPortal(req);
            return result;
        }


        public string postXmlData(string url, string requestXml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            byte[] bytes;
            bytes = System.Text.Encoding.UTF8.GetBytes(requestXml);
            request.ContentType = "application/xml";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }

        /// <summary>
        /// 药品和医用耗材阳光采购平台接口、分值付费系统接口
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "药品和医用耗材阳光采购平台接口、分值付费系统接口")]
        public string SendReveForHospital(string requstXML)
        {
            string responseXml = "";
            responseXml = postXmlData("http://200.10.20.34:8000/fshrssHisApi/openApi/sendReveForHospital", requstXML);
            return responseXml;
        }


        /// <summary>
        /// 医疗机构门诊/住院医保实时审核接口
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "医疗机构门诊/住院医保实时审核接口")]
        public string Audit4HospitalPortal(string requestXML)
        {
            //string responseXml = "";
            string error = "";
            string[] args = { requestXML };
            object responseXml = InvokeWebService("http://200.10.20.33/CisWsHospital.asmx", "CisWsHospitalPortal", args, ref error);
            return responseXml.ToString();
        }

        public object InvokeWebService(string url, string methodname, object[] args, ref string error)
        {
            //这里的namespace是需引用的webservices的命名空间，在这里是写死的，大家可以加一个参数从外面传进来。
            try
            {
                //获取WSDL
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                string classname = sd.Services[0].Name;

                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.ProtocolName = "Soap"; // 指定访问协议。
                sdi.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。
                //sdi.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync; 
                sdi.AddServiceDescription(sd, "", "");

                CodeNamespace cn = new CodeNamespace();
                //cn.Name = "AAAA";

                //生成客户端代理类代码
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider csc = new CSharpCodeProvider();
                ICodeCompiler icc = csc.CreateCompiler();

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
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                //生成代理实例，并调用方法
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);

                return mi.Invoke(obj, args);
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
        }

        /// <summary>
        /// 分值付费系统接口（新）
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "新分值付费系统接口")]
        public string SendDRGS(string requstXML)
        {
            string responseXml = "";
            string err = "";
            responseXml = postXmlDataJSon("http://200.10.20.42:8088/crhms/ybjsqd", requstXML, ref err);
            if (string.IsNullOrEmpty(responseXml))
            {
                return err;
            }
            return responseXml;
        }


        public static string postXmlDataJSon(string url, string requestXml, ref string error)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                byte[] bytes;
                bytes = System.Text.Encoding.UTF8.GetBytes(requestXml);
                request.ContentType = "application/json;charset=UTF-8";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return responseStr;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
            return null;
        }


    }
}
