using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Net;
using System.IO;
using System.Web.Services.Description;

namespace FS.SOC.Public.WebService
{
    /// <summary>
    /// [功能描述: WebService调用相关]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-9]<br></br>
    /// </summary>
    public class Scheduler
    {
        #region InvokeWebService

        /// <summary>
        /// 动态调用web服务
        /// </summary>
        /// <param name="url">WSDL服务地址</param>
        /// <param name="methodname">方法名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object Invoke(string url, string methodname, object[] args, out string errInfo)
        {
            return Invoke(url, null, methodname, args, out errInfo);
        }

        /// <summary>
        /// 动态调用web服务
        /// </summary>
        /// <param name="url">WSDL服务地址</param>
        /// <param name="classname">类名</param>
        /// <param name="methodname">方法名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object Invoke(string url, string classname, string methodname, object[] args, out string errInfo)
        {
            errInfo = "";
            try
            {
                object obj = GetObject(url, classname, out errInfo);
                Type t = obj.GetType();
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);
                return mi.Invoke(obj, args);
            }
            catch (Exception ex)
            {
                errInfo = "反射调用WebService出错," + ex.Message;
                return null;
            }
        }

        public static object Invoke(object classInstance, string methodname, object[] args, out string errInfo)
        {
            errInfo = "";
            try
            {
                Type t = classInstance.GetType();
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);
                return mi.Invoke(classInstance, args);
            }
            catch (Exception ex)
            {
                errInfo = "反射调用WebService出错," + ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 动态调用web服务
        /// </summary>
        /// <param name="url">WSDL服务地址</param>
        /// <param name="classname">类名</param>
        /// <returns>返回WebSever提供的类的实例</returns>
        public static object GetObject(string url, string classname, out string errInfo)
        {
            errInfo = "";
            string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";
            if ((classname == null) || (classname == ""))
            {
                classname = GetWsClassName(url);
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
                CompilerResults cr = icc.CreateCompiler().CompileAssemblyFromDom(cplist, ccu);
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
                Type t = assembly.GetType(@namespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);

                return obj;

                /*
                 PropertyInfo propertyInfo = type.GetProperty(propertyname);
                 return propertyInfo.GetValue(obj, null);
                 */
            }
            catch (Exception ex)
            {
                errInfo = "反射调用WebService出错," + ex.Message;
                return null;
            }
        }

        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');

            return pps[0];
        }

        #endregion

    }
}
