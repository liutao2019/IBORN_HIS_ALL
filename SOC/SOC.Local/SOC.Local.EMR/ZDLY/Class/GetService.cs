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
    /// ��̬����webservice��
    /// </summary>
    public class GetService
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public GetService()
        { }

        /// <summary>
        /// ����ָ������Ϣ������Զ��WebService����
        /// </summary>
        /// <param name="url">WebService��http��ʽ�ĵ�ַ</param>
        /// <param name="classname">�����õ�WebService�������������������ռ�ǰ׺��</param>
        /// <param name="methodname">�����õ�WebService�ķ�����</param>
        /// <param name="args">�����б�</param>
        /// <returns>WebService��ִ�н��</returns>
        /// <remarks>
        /// �������ʧ�ܣ������׳�Exception������õ�ʱ���ʵ��ػ��쳣��
        /// �쳣��Ϣ���ܻᷢ���������ط���
        /// 1����̬����WebService��ʱ��CompileAssemblyʧ�ܡ�
        /// 2��WebService����ִ��ʧ�ܡ�
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