using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;


namespace Neusoft.SOC.Local.EMR.ZDLY.Class
{
    /// <summary>
    /// add by lijp 2011-06-02
    /// 动态调用webservice类
    /// </summary>
    public class GetService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GetService()
        { }

        /// <summary>
        /// 根据指定的信息，调用远程WebService方法
        /// </summary>
        /// <param name="url">WebService的http形式的地址</param>
        /// <param name="classname">欲调用的WebService的类名（不包括命名空间前缀）</param>
        /// <param name="methodname">欲调用的WebService的方法名</param>
        /// <param name="args">参数列表</param>
        /// <returns>WebService的执行结果</returns>
        /// <remarks>
        /// 如果调用失败，将会抛出Exception。请调用的时候，适当截获异常。
        /// 异常信息可能会发生在两个地方：
        /// 1、动态构造WebService的时候，CompileAssembly失败。
        /// 2、WebService本身执行失败。
        /// </remarks>
        /// <example>
        /// <code>
        /// object obj = InvokeWebservice("http://localhost/GSP_WorkflowWebservice/common.asmx",
        ///                               "Genersoft.Platform.Service.Workflow",
        ///                               "Common",
        ///                               "GetToolType",
        ///                               new object[]{"1"});
        /// </code>
        /// </example>
        public static object InvokeWebservice(string wsdlURL, string classname,
                                              string methodname, object[] args)
        {
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                System.IO.Stream stream = wc.OpenRead(wsdlURL);
                System.Web.Services.Description.ServiceDescription sd
                    = System.Web.Services.Description.ServiceDescription.Read(stream);
                System.Web.Services.Description.ServiceDescriptionImporter sdi
                    = new System.Web.Services.Description.ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                System.CodeDom.CodeNamespace cn = new System.CodeDom.CodeNamespace();
                System.CodeDom.CodeCompileUnit ccu = new System.CodeDom.CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);

                Microsoft.CSharp.CSharpCodeProvider csc = new Microsoft.CSharp.CSharpCodeProvider();
                System.CodeDom.Compiler.ICodeCompiler icc = csc.CreateCompiler();

                System.CodeDom.Compiler.CompilerParameters cplist
                    = new System.CodeDom.Compiler.CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                System.CodeDom.Compiler.CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
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
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);
                return mi.Invoke(obj, args);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }
    }
}