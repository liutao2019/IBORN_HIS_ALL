using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace FSDataWindow.Controls
{
    /// <summary>
    /// [功能描述: 数据窗口控件]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2007-01-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class FSDataWindowExtend : Sybase.DataWindow.DataWindowControl
    {
        private Sybase.DataWindow.Transaction SQLCA;
        private Sybase.DataWindow.AdoTransaction trans;
        private string error;

        public string Error
        {
            get
            {
                return this.error;
            }
        }

        #region 连接串

        public static string ConnectStr
        {
            get
            {
                if (DataSource.Trim() == string.Empty)
                {
                    GetSetting();
                }
                return FSDataWindowExtend.DataSource;
            }
        }
        const string UrlFileName = "url.xml";
        static string DataSource = "";

        /// <summary>
        /// 
        /// </summary>
        static string ServerPath = "";

        /// <summary>
        /// 获得配置文件
        /// </summary>
        /// <returns></returns>
        internal static int GetSetting()
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                doc.Load(FS.FrameWork.WinForms.Classes.Function.CurrentPath + UrlFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("装载url失败！\n" + ex.Message);
                return -1;
            }
            System.Xml.XmlNode node;
            #region 改成读一个地址列表，这样可以实现双机效果  {A5B6BD9E-68A1-45f5-BFE2-7EF0604AAAED}
            bool isUseUrlList = false;
            try
            {
                //校验用的node
                System.Xml.XmlNode nodeForCheck;
                nodeForCheck = doc.SelectSingleNode("//root/dir");
                if (nodeForCheck == null)
                {
                    isUseUrlList = false;
                }
                else
                {
                    isUseUrlList = true;
                }
            }
            catch (Exception ex)
            {
                isUseUrlList = false;
            }
            if (isUseUrlList == false)
            {
            #endregion
                #region 原有读单一路径的代码，为了兼容保留
                node = doc.SelectSingleNode("//dir");

                if (node == null)
                {
                    MessageBox.Show("url中找dir结点出错！");
                    return -1;
                }

                ServerPath = node.InnerText;
                //TemplateDesignerHost.Function.SystemPath = ServerPath;//为电子病历使用的服务器路径


                string serverSettingFileName = "HisProfile.xml"; //服务器文件名

                try
                {
                    doc.Load(ServerPath + serverSettingFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("装载HisProfile.xml失败！\n" + ex.Message);

                }
                #endregion
                #region 改成读一个地址列表，这样可以实现双机效果 {A5B6BD9E-68A1-45f5-BFE2-7EF0604AAAED}
            }
            else
            {
                System.Xml.XmlNodeList xnl = doc.SelectNodes("//root/dir");
                if (xnl == null || xnl.Count == 0)
                {
                    MessageBox.Show("url中找dir结点出错！");
                    return -1;
                }

                int xnIdx = 0;
                foreach (System.Xml.XmlNode xn in xnl)
                {
                    ServerPath = xn.InnerText;
                    //TemplateDesignerHost.Function.SystemPath = ServerPath;//为电子病历使用的服务器路径

                    string serverSettingFileName = "HisProfile.xml"; //服务器文件名

                    try
                    {

                        doc.Load(ServerPath + serverSettingFileName);
                        break;
                    }
                    catch (Exception ex)
                    {

                        if (xnIdx == xnl.Count - 1)
                        {
                            MessageBox.Show("装载HisProfile.xml失败！\n" + ex.Message);
                            return -1;
                        }
                        else
                        {
                            xnIdx++;
                            continue;
                        }
                    }
                }

            }
                #endregion
            node = doc.SelectSingleNode("/设置/报表数据库设置");

            if (node == null)
            {
                MessageBox.Show("没有找到报表数据库设置!");
                return -1;
            }

            string strDataSource = node.Attributes[0].Value;

            //判断连接串是否加密{2480BEE8-92D0-484e-8D7E-2E24CC41C0C1}
            //node = doc.SelectSingleNode("/设置/加密");
            //if (node == null)
            //{
            //    MessageBox.Show("没有找到是否加密信息!");
            //    return -1;
            //}
            //string strCrypto = node.Attributes[0].Value;
            //if (strCrypto.Trim().Equals("1"))
            //{
            //    //strDataSource = FS.HisDecrypt.Decrypt(strDataSource);
            //}
            //END

            #region 数据库类型
            //node = doc.SelectSingleNode("/设置/数据库类别");
            //if (node != null)
            //{
            //    //strDataType = node.Attributes[0].Value;//这个是在界面上显示用的,没用//{712E982E-B9B5-4063-9736-F97FE734C3BB}

            //    DBType = GetDBType(node.Attributes[0].Value);//数据库类型判断.//{08F955BE-6313-47cc-AB3A-14897F4147B8}
            //}


            #endregion
            DataSource = strDataSource;

            //node = doc.SelectSingleNode("/设置/设置");

            //if (node == null)
            //{
            //    MessageBox.Show("没有找到SQL设置!");
            //    return -1;
            //}

            //if (node.Attributes[0].Value == "1")//Sql.xml
            //{
            //    IsSqlInDB = false;
            //}
            //else//数据库
            //{
            //    IsSqlInDB = true;
            //}

            //node = doc.SelectSingleNode("/设置/管理员");


            //if (node == null)
            //{
            //    MessageBox.Show("没有找到管理员密码!");
            //    return -1;
            //}

            //ManagerPWD = node.Attributes[0].Value;

            //node = doc.SelectSingleNode("/设置/正式库");
            //if (node != null)
            //{
            //    if (node.Attributes[0].Value == "0")
            //    {
            //        IsTestDB = true;
            //    }
            //    else
            //    {
            //        IsTestDB = false;
            //    }
            //}

            return 0;


        }

        #endregion
        protected int Connect()
        {
            #region DB2  //{2C89BBBC-10FB-4f7e-B080-712A6C228719}

            if (FS.FrameWork.Management.Connection.Instance.GetType().ToString().IndexOf("IBM") >= 0)
            {
                //if (DBConfig.Init() == -1)
                //{
                //    return -1;
                //}

                try
                {
                    this.trans = new Sybase.DataWindow.AdoTransaction();
                    System.Data.OleDb.OleDbConnection olecon = new System.Data.OleDb.OleDbConnection();
                    olecon.ConnectionString = ConnectStr;
                    olecon.Open();
                    this.trans = new Sybase.DataWindow.AdoTransaction(olecon);

                    this.trans.BindConnection();
                    this.SetTransaction(this.trans);
                }
                catch (Exception e)
                {
                    this.trans = null;
                    this.error = e.Message;
                    return -1;
                }

                return 1;
            }

            #endregion

            if (this.SQLCA == null && trans == null)
            {
                if (DBConfig.Init() == -1)
                    return -1;

                if (DBConfig.DbType.StartsWith("Oracle"))        //Oracle数据库
                {

                    this.SQLCA = new Sybase.DataWindow.Transaction();
                    System.Data.OracleClient.OracleConnectionStringBuilder ocs =
                        new System.Data.OracleClient.OracleConnectionStringBuilder(/*"data source=CHCC;password=his;persist security info=True;user id=HIS45"*/FS.FrameWork.Management.Connection.Instance.ConnectionString);

                    SQLCA.Password = ocs.Password;
                    SQLCA.ServerName = ocs.DataSource;
                    SQLCA.UserId = ocs.UserID;

                    try
                    {
                        SQLCA.Dbms = (Sybase.DataWindow.DbmsType)Enum.Parse(typeof(Sybase.DataWindow.DbmsType), DBConfig.DbType);
                    }
                    catch
                    {
                        SQLCA.Dbms = Sybase.DataWindow.DbmsType.Oracle8i;
                    }

                    SQLCA.AutoCommit = false;
                    SQLCA.DbParameter = DBConfig.DbParameter;

                    try
                    {
                        SQLCA.Connect();
                        this.SetTransaction(this.SQLCA);
                    }
                    catch (Exception e)
                    {
                        this.error = e.Message;
                        return -1;
                    }

                    return 0;

                }
                else if (DBConfig.DbType.StartsWith("Sql"))
                {
                    System.Data.SqlClient.SqlConnection oleCn = new System.Data.SqlClient.SqlConnection();
                    oleCn.ConnectionString = DBConfig.DbParameter;
                    try
                    {
                        oleCn.Open();
                        this.SetTransaction(this.trans);
                    }
                    catch (Exception e)
                    {
                        this.error = e.Message;
                        return -1;
                    }

                    this.trans = new Sybase.DataWindow.AdoTransaction(oleCn);
                    this.trans.BindConnection();
                    return 0;
                }

            }
            else
            {
                System.Data.OleDb.OleDbConnection oleCn = new System.Data.OleDb.OleDbConnection();
                oleCn.ConnectionString = FS.FrameWork.Management.Connection.Instance.ConnectionString;
                try
                {
                    oleCn.Open();
                    this.SetTransaction(this.trans);
                }
                catch (Exception e)
                {
                    this.error = e.Message;
                    return -1;
                }

                this.trans = new Sybase.DataWindow.AdoTransaction(oleCn);
                this.trans.BindConnection();
                return 0;
            }

            try
            {
                if (this.SQLCA != null)
                {
                    this.SQLCA.Connect();
                    this.SetTransaction(this.SQLCA);
                }
            }
            catch (Exception e)
            {
                this.error = e.Message;
                return -1;
            }
            try
            {
                if (this.trans != null)
                {
                    this.trans.Connection.Open();
                    this.SetTransaction(this.trans);
                }
            }
            catch (Exception e)
            {
                this.error = e.Message;
                return -1;
            }
            return 0;
        }

        protected void Disconnect()
        {
            try
            {
                if (this.SQLCA != null)
                {
                    this.SQLCA.Disconnect();
                }

                if (this.trans != null && this.trans.Connection != null)
                {
                    this.trans.Connection.Close();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Retrieve数据
        /// </summary>
        /// <param name="objects"></param>
        /// <returns>大于等于0 记录数，-1 失败</returns>
        public new int Retrieve(params object[] objects)
        {
            if (this.Connect() == -1)
                return -1;

            try
            {
                int ret = base.Retrieve(objects);

                return ret;
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
                return -1;
            }
            finally
            {
                this.Disconnect();
            }
        }

        /// <summary>
        /// Retrieve数据
        /// </summary>
        /// <param name="objects"></param>
        /// <returns>大于等于0 记录数，-1 失败</returns>
        public  int RetrieveDataTable(System.Data.DataTable dt)
        {
            try
            {
                int ret = base.Retrieve(dt);

                return ret;
            }
            catch (Exception ex)
            {
                this.error = ex.Message;

                return -1;
            }
        }

        /// <summary>
        /// [功能描述: DB配置]<br></br>
        /// [创 建 者: 王铁全]<br></br>
        /// [创建时间: 2007-01-30]<br></br>
        /// <修改记录
        ///		修改人=''
        ///		修改时间='yyyy-mm-dd'
        ///		修改目的=''
        ///		修改描述=''
        ///  />
        /// </summary>
        class DBConfig
        {
            public static string DbType;
            public static string DbParameter;

            public static int Init()
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                try
                {
                    if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\FSDataWindow.xml"))
                    {
                        doc.Load(System.Windows.Forms.Application.StartupPath + "\\FSDataWindow.xml");
                        System.Xml.XmlNode node = doc.SelectSingleNode("/Config/DB");
                        DbType = node.Attributes[0].Value;
                        DbParameter = node.Attributes[1].Value;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("文件FSDataWindow.xml在" + System.Windows.Forms.Application.StartupPath + " 下没有找到");
                        return -1;
                    }
                }
                catch
                {

                    System.Windows.Forms.MessageBox.Show("文件FSDataWindow.xml错误!");
                    return -1;
                }

                return 0;
            }
        };

        /// <summary>
        /// 数据窗口打印
        /// </summary>
        public class DataWindowPrint
        {
            /// <summary>
            /// 将数据窗口的内容打印到指定名称的打印机
            /// </summary>
            /// <param name="printerName">打印机名称</param>
            /// <param name="dwc">DataWindowControl</param>
            /// <param name="error">错误信息</param>
            /// <returns>1－成功；－1－失败</returns>
            public static int PrintDataWindow(string printerName, Sybase.DataWindow.DataWindowControl dwc, ref string error)
            {
                try
                {
                    dwc.SetProperty("DataWindow.Printer", printerName);

                    dwc.Print();
                }
                catch (Exception exception)
                {
                    error = exception.Message;

                    return -1;
                }

                return 1;
            }

            /// <summary>
            /// 将数据窗口的内容打印到指定名称的打印机，同时指定纸张的大小
            /// </summary>
            /// <param name="printerName">打印机名称</param>
            /// <param name="width">纸张的宽度</param>
            /// <param name="length">纸张的长度</param>
            /// <param name="dwc">DataWindowControl</param>
            /// <param name="error">错误信息</param>
            /// <returns>1－成功；－1－失败</returns>
            public static int PrintDataWindow(string printerName, int width, int length, Sybase.DataWindow.DataWindowControl dwc, ref string error)
            {
                try
                {
                    dwc.SetProperty("DataWindow.Printer", printerName);

                    dwc.Modify("DataWindow.Print.Paper.Size=256");
                    dwc.Modify("DataWindow.Print.CustomPage.Length=" + length.ToString());
                    dwc.Modify("DataWindow.Print.CustomPage.Width=" + width.ToString());

                    dwc.Print();
                }
                catch (Exception exception)
                {
                    error = exception.Message;

                    return -1;
                }

                return 1;
            }

            /// <summary>
            /// 将数据窗口的内容打印到指定名称的打印机，同时指定纸张的大小
            /// </summary>
            /// <param name="printerName">打印机名称</param>
            /// <param name="width">纸张的宽度</param>
            /// <param name="length">纸张的长度</param>
            /// <param name="showPrinterDiag">打印时是否显示打印设置对话框</param>
            /// <param name="showCancelDiag">打印时是否显示取消打印对话框</param>
            /// <param name="dwc">DataWindowControl</param>
            /// <param name="error">错误信息</param>
            /// <returns>1－成功；－1－失败</returns>
            public static int PrintDataWindow(string printerName, int width, int length, Sybase.DataWindow.DataWindowControl dwc, bool showPrinterDiag, bool showCancelDiag, ref string error)
            {
                try
                {
                    dwc.SetProperty("DataWindow.Printer", printerName);

                    dwc.Modify("DataWindow.Print.Paper.Size=256");
                    dwc.Modify("DataWindow.Print.CustomPage.Length=" + length.ToString());
                    dwc.Modify("DataWindow.Print.CustomPage.Width=" + width.ToString());

                    dwc.Print(showCancelDiag, showPrinterDiag);
                }
                catch (Exception exception)
                {
                    error = exception.Message;

                    return -1;
                }

                return 1;
            }
        }
    }
}
