using System;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using System.Web.Services.Description;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text;

namespace API.GZSI.Common
{
    public static class Function
    {
        #region 序/反列化

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="xml">xml</param>
        /// <returns>实体</returns>
        public static T Deserialize<T>(string xml)
        {
            try
            {
                XmlSerializer xx = new XmlSerializer(typeof(T));
                StringReader sr = new StringReader(xml);
                T obj = (T)xx.Deserialize(sr);
                sr.Close();
                sr.Dispose();

                return obj;
            }
            catch
            {
                return default(T);
            }
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">实体</param>
        /// <returns></returns>
        public static string Serializer<T>(T t)
        {
            try
            {
                XmlSerializerNamespaces sn = new XmlSerializerNamespaces();
                sn.Add(string.Empty, string.Empty);
                XmlSerializer xs = new XmlSerializer(typeof(T));
                StringWriter sw = new StringWriter();
                xs.Serialize(sw, t, sn);
                string ss = sw.ToString();
                sw.Close();
                sw.Dispose();
                return ss;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion 序/反列化

        #region datarow转化为实体

        /// <summary>
        /// 将DataRow转换为对象实体：T为数据类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T DataRowToObject<T>(System.Data.DataRow dr)
        {
            T obj = Activator.CreateInstance<T>(); //先创建一个实体实例

            //获取实体实例的属性信息
            ArrayList propertyInfoList = new ArrayList();
            foreach (PropertyInfo propInfo in obj.GetType().GetProperties())
            {
                propertyInfoList.Add(propInfo);
            }

            //遍历各个属性
            for (int i = 0; i <= propertyInfoList.Count - 1; i++)
            {
                PropertyInfo propertyInfo = (PropertyInfo)propertyInfoList[i];
                if (propertyInfo.CanWrite)
                {
                    if (dr.Table.Columns.Contains(propertyInfo.Name))
                    {
                        //if (dr[propertyInfo.Name] != DBNull.Value)
                        //{
                            try
                            {
                                object dataValue = dr[propertyInfo.Name]; //数据行中的字段的值
                                object value = ConvertToPropertyValue(propertyInfo, dataValue); //将字段的值转换为属性值
                                propertyInfo.SetValue(obj, value, null);
                            }
                            catch
                            {

                            }
                        //}
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// 将数据行中的数据值转换为属性值
        /// </summary>
        /// <param name="propInfo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object ConvertToPropertyValue(PropertyInfo propInfo, object value)
        {
            try
            {
                return Convert.ChangeType(value, propInfo.PropertyType);
            }
            catch
            {

            }
            return null;
        }

        #endregion


        #region MD5加密

        /// <summary>
        /// MD5密码
        /// </summary>
        /// <param name="myString"></param>
        /// <returns></returns>
        public static string GetMD5(string myString)
        {
            string md5String = string.Empty;
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(myString));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。 X 表示大写， x 表示小写， X2和x2表示不省略首位为0的十六进制数字；
                // 比如：ox0A, 使用X== 0xA，  使用X2==0x0A
                md5String = md5String + s[i].ToString("X2");

            }
            return md5String;
        }
        #endregion MD5加密

        #region Sha256加密

        /// <summary>
        /// Sha256加密
        /// </summary>
        /// <param name="myString"></param>
        /// <returns></returns>
        public static string GetSha256(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            try
            {
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                byte[] retVal = sha256.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetSHA256HashFromString() fail,error:" + ex.Message);
            }

        }
        #endregion Sha256加密

        #region 日志

        public static void WriteLog(string logText)
        {
            string logPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到窗体
            string logFullName = logPath +
                "\\interface_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            if (!File.Exists(logFullName))
            {
                FileStream fs1 = new FileStream(logFullName, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(logText);//开始写入值
                sw.Close();
                fs1.Close();

            }
            else
            {
                System.IO.File.AppendAllText(logFullName,"【" + DateTime.Now.ToString() + "】--> \r\n" + logText + "\r\n");
            }
        }

        #endregion 日志

        #region POST


        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="strURL"></param>
        /// <param name="postData"></param>
        /// <param name="ContentType">
        ///   text/html ： HTML格式
        ///   text/plain ：纯文本格式      
        ///   text/xml ：  XML格式
        ///   image/gif ：gif图片格式    
        ///   image/jpeg ：jpg图片格式 
        ///   image/png：png图片格式
        /// 以application开头的媒体格式类型： 
        ///   application/xhtml+xml ：XHTML格式
        ///   application/xml     ： XML数据格式
        ///   application/atom+xml  ：Atom XML聚合格式    
        ///   application/json    ： JSON数据格式
        ///   application/pdf       ：pdf格式  
        ///   application/msword  ： Word文档格式
        ///   application/octet-stream ： 二进制流数据（如常见的文件下载）
        ///   application/x-www-form-urlencoded ： <form encType=””>中默认的encType，form表单数据被编码为key/value格式发送到服务器（表单默认的提交数据的格式）
        /// </param>
        /// <returns></returns>
        public static string Post(string strURL, string postData, string ContentType)
        {
            if (string.IsNullOrEmpty(ContentType))
            {
                ContentType = "application/json";
            }
            // 日志
            //FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger();
            //db.WriteDebug("Post Url:" + strURL + "\r\nPost Data:" + postData);
            WriteLog("Post Url:" + strURL + "\r\nContentType:" + ContentType + "\r\nPost Data:" + postData);
            try
            {

                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strURL);
                //Post请求方式
                request.Method = "POST";
                // 内容类型
                request.ContentType = ContentType; //"application/json";
                // 参数经过URL编码
                //string paraUrlCoded = System.Web.HttpUtility.UrlEncode(postData);
                byte[] payload;
                //将URL编码后的字符串转化为字节
                payload = System.Text.Encoding.UTF8.GetBytes(postData);
                //设置请求的 ContentLength 
                request.ContentLength = payload.Length;
                //获得请 求流
                System.IO.Stream writer = request.GetRequestStream();
                //将请求参数写入流
                writer.Write(payload, 0, payload.Length);
                // 关闭请求流
                writer.Close();
                System.Net.HttpWebResponse response;
                // 获得响应流
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
                string responseText = myreader.ReadToEnd();
                myreader.Close();
                //db.WriteDebug("Response Data:" + responseText);
                WriteLog("Response Data:" + responseText);

                return responseText;

            }
            catch (Exception ex)
            {
                WriteLog("Exception:" + ex.ToString());
                return ex.ToString();
            }
        }

        public static string PostWebRequest(string postUrl, string paramData)
        {
            string paasid = "rzGz8JZs3M9Q0zKBHAz4RC1eXmjA6IDWcQbEZ4";//应用编码
            string secreKey = "SKKwZvvjzrVdFhXBXhAMOclKA310Mndv1cZWAgYA";//私钥（secreKey）
            long timestamp = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string nonce = timestamp.ToString();
            string signature = GetSha256(timestamp + secreKey + nonce + timestamp);//签名
            Encoding dataEncode = Encoding.UTF8;
            string ret = string.Empty;
            try
            {
                if (dataEncode == null)
                    dataEncode = Encoding.UTF8;
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/json";
                webReq.Headers.Add("x-tif-paasid", paasid);
                webReq.Headers.Add("x-tif-signature", signature);
                webReq.Headers.Add("x-tif-timestamp", timestamp.ToString());
                webReq.Headers.Add("x-tif-nonce", nonce);

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                var c = response.GetResponseStream();
                StreamReader sr = new StreamReader(c, Encoding.UTF8);
                Char[] read = new Char[4000];
                int count = sr.Read(read, 0, read.Length);
                while (count > 0)
                { 
                    String str = new String(read, 0, count);
                    ret += str;

                    count = sr.Read(read, 0, read.Length);
                }

                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return ret;
        }

        #endregion POST


        #region  显示返回实体到列表

        /// <summary>
        /// 显示返回实体到列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fp"></param>
        /// <param name="lsQpr"></param>
        public static void ShowOutDateToFarpoint<T>(FarPoint.Win.Spread.SheetView fp, List<T> lsData)
        {
            if (lsData == null)
            {
                return;
            }
            fp.RowCount = 0;
            fp.ColumnCount = 0;

            // 设置属性默认值
            PropertyInfo[] propertys = typeof(T).GetProperties();
            //动态设置列头
            FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
            foreach (var property in propertys)
            {
                fp.ColumnCount++;
                fp.Columns[fp.ColumnCount - 1].CellType = txtCellType;

                // 通过资源字符串反序列头
                string title = "";
                try
                {
                    System.Reflection.PropertyInfo fieldPropertyInfo = typeof(T).GetProperty(property.Name);
                    DisplayAttribute display = (DisplayAttribute)Attribute.GetCustomAttribute(fieldPropertyInfo, typeof(DisplayAttribute));

                    if (display != null)
                    {
                        title = display.Name;
                    }
                    else
                    {
                        title = Properties.Resources.ResourceManager.GetString(property.Name);
                    }
                }
                catch
                {
                    title = Properties.Resources.ResourceManager.GetString(property.Name);
                }

                fp.Columns[fp.ColumnCount - 1].Label = !string.IsNullOrEmpty(title) ? title : property.Name;
            }

            foreach (var data in lsData)
            {
                fp.RowCount++;
                int colIndex = 0;
                fp.Rows[fp.RowCount - 1].Tag = data;
                //动态加载行
                foreach (var property in propertys)
                {
                    object obj = property.GetValue(data, null);
                    fp.Cells[fp.RowCount - 1, colIndex].Text = obj == null ? string.Empty : obj.ToString();
                    colIndex++;
                }
            }

            // 列自动适应宽度
            for (int i = 0; i < fp.ColumnCount; i++)
            {
                //自动行宽
                fp.Columns[i].Width = fp.Columns[i].GetPreferredWidth();
            }

        }

        #endregion 显示返回实体到列表

        #region WebServices调用 
        /// <summary>
        /// 实例化WebServices
        /// </summary>
        /// <param name="url">WebServices地址</param>
        /// <param name="methodname">调用的方法</param>
        /// <param name="args">把webservices里需要的参数按顺序放到这个object[]里</param>
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            //这里的namespace是需引用的webservices的命名空间，我没有改过，也可以使用。也可以加一个参数从外面传进来。
            string @namespace = "client";

            try
            {
                //获取WSDL
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                string classname = sd.Services[0].Name;
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);

                //生成客户端代理类代码
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider csc = new CSharpCodeProvider();
                //ICodeCompiler icc = csc.CreateCompiler();

                //设定编译参数
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;//动态编译后的程序集不生成可执行文件
                cplist.GenerateInMemory = true;//动态编译后的程序集只存在于内存中，不在硬盘的文件上
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                //编译代理类
                CompilerResults cr = csc.CompileAssemblyFromDom(cplist, ccu);
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
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);

                //注：method.Invoke(o, null)返回的是一个Object,如果你服务端返回的是DataSet,这里也是用(DataSet)method.Invoke(o, null)转一下就行了,method.Invoke(0,null)这里的null可以传调用方法需要的参数,string[]形式的
                return mi.Invoke(obj, args);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 日期转换处理

        /// <summary>
        /// 字符串年月日不分隔转日期，示例：19750303
        /// </summary>
        /// <param name="strBirthday">字符串年月日</param>
        /// <returns></returns>
        public static DateTime StringYYYYMMDDToDateTime(string strBirthday)
        {
            if (!string.IsNullOrEmpty(strBirthday) && strBirthday.Length >= 6)
            {
                strBirthday = strBirthday.Substring(0, 4) + "-"
                                   + strBirthday.Substring(4, 2) + "-"
                                   + strBirthday.Substring(6, 2);

                return FS.FrameWork.Function.NConvert.ToDateTime(strBirthday);
            }

            return DateTime.MinValue;
        }

        #endregion

    }
}
