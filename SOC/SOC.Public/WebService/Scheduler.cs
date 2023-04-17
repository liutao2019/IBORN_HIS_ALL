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
    /// [��������: WebService�������]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-9]<br></br>
    /// </summary>
    public class Scheduler
    {
        #region InvokeWebService

        /// <summary>
        /// ��̬����web����
        /// </summary>
        /// <param name="url">WSDL�����ַ</param>
        /// <param name="methodname">������</param>
        /// <param name="args">����</param>
        /// <returns></returns>
        public static object Invoke(string url, string methodname, object[] args, out string errInfo)
        {
            return Invoke(url, null, methodname, args, out errInfo);
        }

        /// <summary>
        /// ��̬����web����
        /// </summary>
        /// <param name="url">WSDL�����ַ</param>
        /// <param name="classname">����</param>
        /// <param name="methodname">������</param>
        /// <param name="args">����</param>
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
                errInfo = "�������WebService����," + ex.Message;
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
                errInfo = "�������WebService����," + ex.Message;
                return null;
            }
        }
        /// <summary>
        /// ��̬����web����
        /// </summary>
        /// <param name="url">WSDL�����ַ</param>
        /// <param name="classname">����</param>
        /// <returns>����WebSever�ṩ�����ʵ��</returns>
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
                //��ȡWSDL
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);

                //���ɿͻ��˴��������
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider icc = new CSharpCodeProvider();

                //�趨�������
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                //���������
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

                //���ɴ���ʵ���������÷���
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
                errInfo = "�������WebService����," + ex.Message;
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
