using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using Microsoft.CSharp;
using System.Net;
using System.IO;
using System.CodeDom.Compiler;
using System.Web.Services.Description;

namespace FoShanDiseasePay.Common
{
    public class WebServiceHelper
    {
        #region  动态调用WebServices

        public static object InvokeWebService(string url, string methodname, object[] args, ref string error)
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
        public static object InvokeService(string url, string json, ref string error)
        {
            try
            {
                //postData = postData.Substring(0, postData.Length - 1);

                //string postData = GZipHelper.Compress(json);

                string postData = json;

                System.Net.HttpWebRequest request = System.Net.HttpWebRequest.Create(url) as System.Net.HttpWebRequest;
                request.Method = "POST";
                request.Accept = "text/html,application/xhtml+xml,*/*";
                byte[] byte1 = Encoding.UTF8.GetBytes(postData);
                // 设置请求的参数形式  
                request.ContentType = "application/json;charset=UTF-8";

                // 设置请求参数的长度.  
                request.ContentLength = byte1.Length;
                // 取得发向服务器的流  
                Stream newStream = request.GetRequestStream();

                // 使用 POST 方法请求的时候，实际的参数通过请求的 Body 部分以流的形式传送  
                newStream.Write(byte1, 0, byte1.Length);

                // 完成后，关闭请求流.  
                newStream.Close();
                // GetResponse 方法才真的发送请求，等待服务器返回  
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.Stream receiveStream = response.GetResponseStream();
                // 还可以将字节流包装为高级的字符流，以便于读取文本内容  
                // 需要注意编码  
                System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, Encoding.UTF8);
                string msg = readStream.ReadToEnd();
                //Console.WriteLine(msg);  
                //注：上边大体都是百度的，吸取了某位大牛的博文，当时没记下来，如果大牛看到，请联系我，我添加来源说明  
                //这边收到的是json对象数据和整夜html页面代码，所以需要正则抓取  
                //string patten = "({.+})+";
                //Regex reg = new Regex(patten);
                //Match matches = reg.Match(msg);
                // 完成后要关闭字符流，字符流底层的字节流将会自动关闭  
                response.Close();
                readStream.Close();

                return msg;
            }
            catch (Exception e)
            {
                error = e.Message;
                return "{\"Code\":0,\"Msg\":\"" + error + "\",\"Result\":\"\"}";
            }
        }
        #endregion
    }
}
