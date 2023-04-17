using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace FSDataWindow.Controls
{
    /// <summary>
    /// [��������: ���ݴ��ڿؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
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

        #region ���Ӵ�

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
        /// ��������ļ�
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
                MessageBox.Show("װ��urlʧ�ܣ�\n" + ex.Message);
                return -1;
            }
            System.Xml.XmlNode node;
            #region �ĳɶ�һ����ַ�б���������ʵ��˫��Ч��  {A5B6BD9E-68A1-45f5-BFE2-7EF0604AAAED}
            bool isUseUrlList = false;
            try
            {
                //У���õ�node
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
                #region ԭ�ж���һ·���Ĵ��룬Ϊ�˼��ݱ���
                node = doc.SelectSingleNode("//dir");

                if (node == null)
                {
                    MessageBox.Show("url����dir������");
                    return -1;
                }

                ServerPath = node.InnerText;
                //TemplateDesignerHost.Function.SystemPath = ServerPath;//Ϊ���Ӳ���ʹ�õķ�����·��


                string serverSettingFileName = "HisProfile.xml"; //�������ļ���

                try
                {
                    doc.Load(ServerPath + serverSettingFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("װ��HisProfile.xmlʧ�ܣ�\n" + ex.Message);

                }
                #endregion
                #region �ĳɶ�һ����ַ�б���������ʵ��˫��Ч�� {A5B6BD9E-68A1-45f5-BFE2-7EF0604AAAED}
            }
            else
            {
                System.Xml.XmlNodeList xnl = doc.SelectNodes("//root/dir");
                if (xnl == null || xnl.Count == 0)
                {
                    MessageBox.Show("url����dir������");
                    return -1;
                }

                int xnIdx = 0;
                foreach (System.Xml.XmlNode xn in xnl)
                {
                    ServerPath = xn.InnerText;
                    //TemplateDesignerHost.Function.SystemPath = ServerPath;//Ϊ���Ӳ���ʹ�õķ�����·��

                    string serverSettingFileName = "HisProfile.xml"; //�������ļ���

                    try
                    {

                        doc.Load(ServerPath + serverSettingFileName);
                        break;
                    }
                    catch (Exception ex)
                    {

                        if (xnIdx == xnl.Count - 1)
                        {
                            MessageBox.Show("װ��HisProfile.xmlʧ�ܣ�\n" + ex.Message);
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
            node = doc.SelectSingleNode("/����/�������ݿ�����");

            if (node == null)
            {
                MessageBox.Show("û���ҵ��������ݿ�����!");
                return -1;
            }

            string strDataSource = node.Attributes[0].Value;

            //�ж����Ӵ��Ƿ����{2480BEE8-92D0-484e-8D7E-2E24CC41C0C1}
            //node = doc.SelectSingleNode("/����/����");
            //if (node == null)
            //{
            //    MessageBox.Show("û���ҵ��Ƿ������Ϣ!");
            //    return -1;
            //}
            //string strCrypto = node.Attributes[0].Value;
            //if (strCrypto.Trim().Equals("1"))
            //{
            //    //strDataSource = FS.HisDecrypt.Decrypt(strDataSource);
            //}
            //END

            #region ���ݿ�����
            //node = doc.SelectSingleNode("/����/���ݿ����");
            //if (node != null)
            //{
            //    //strDataType = node.Attributes[0].Value;//������ڽ�������ʾ�õ�,û��//{712E982E-B9B5-4063-9736-F97FE734C3BB}

            //    DBType = GetDBType(node.Attributes[0].Value);//���ݿ������ж�.//{08F955BE-6313-47cc-AB3A-14897F4147B8}
            //}


            #endregion
            DataSource = strDataSource;

            //node = doc.SelectSingleNode("/����/����");

            //if (node == null)
            //{
            //    MessageBox.Show("û���ҵ�SQL����!");
            //    return -1;
            //}

            //if (node.Attributes[0].Value == "1")//Sql.xml
            //{
            //    IsSqlInDB = false;
            //}
            //else//���ݿ�
            //{
            //    IsSqlInDB = true;
            //}

            //node = doc.SelectSingleNode("/����/����Ա");


            //if (node == null)
            //{
            //    MessageBox.Show("û���ҵ�����Ա����!");
            //    return -1;
            //}

            //ManagerPWD = node.Attributes[0].Value;

            //node = doc.SelectSingleNode("/����/��ʽ��");
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

                if (DBConfig.DbType.StartsWith("Oracle"))        //Oracle���ݿ�
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
        /// Retrieve����
        /// </summary>
        /// <param name="objects"></param>
        /// <returns>���ڵ���0 ��¼����-1 ʧ��</returns>
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
        /// Retrieve����
        /// </summary>
        /// <param name="objects"></param>
        /// <returns>���ڵ���0 ��¼����-1 ʧ��</returns>
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
        /// [��������: DB����]<br></br>
        /// [�� �� ��: ����ȫ]<br></br>
        /// [����ʱ��: 2007-01-30]<br></br>
        /// <�޸ļ�¼
        ///		�޸���=''
        ///		�޸�ʱ��='yyyy-mm-dd'
        ///		�޸�Ŀ��=''
        ///		�޸�����=''
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
                        System.Windows.Forms.MessageBox.Show("�ļ�FSDataWindow.xml��" + System.Windows.Forms.Application.StartupPath + " ��û���ҵ�");
                        return -1;
                    }
                }
                catch
                {

                    System.Windows.Forms.MessageBox.Show("�ļ�FSDataWindow.xml����!");
                    return -1;
                }

                return 0;
            }
        };

        /// <summary>
        /// ���ݴ��ڴ�ӡ
        /// </summary>
        public class DataWindowPrint
        {
            /// <summary>
            /// �����ݴ��ڵ����ݴ�ӡ��ָ�����ƵĴ�ӡ��
            /// </summary>
            /// <param name="printerName">��ӡ������</param>
            /// <param name="dwc">DataWindowControl</param>
            /// <param name="error">������Ϣ</param>
            /// <returns>1���ɹ�����1��ʧ��</returns>
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
            /// �����ݴ��ڵ����ݴ�ӡ��ָ�����ƵĴ�ӡ����ͬʱָ��ֽ�ŵĴ�С
            /// </summary>
            /// <param name="printerName">��ӡ������</param>
            /// <param name="width">ֽ�ŵĿ��</param>
            /// <param name="length">ֽ�ŵĳ���</param>
            /// <param name="dwc">DataWindowControl</param>
            /// <param name="error">������Ϣ</param>
            /// <returns>1���ɹ�����1��ʧ��</returns>
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
            /// �����ݴ��ڵ����ݴ�ӡ��ָ�����ƵĴ�ӡ����ͬʱָ��ֽ�ŵĴ�С
            /// </summary>
            /// <param name="printerName">��ӡ������</param>
            /// <param name="width">ֽ�ŵĿ��</param>
            /// <param name="length">ֽ�ŵĳ���</param>
            /// <param name="showPrinterDiag">��ӡʱ�Ƿ���ʾ��ӡ���öԻ���</param>
            /// <param name="showCancelDiag">��ӡʱ�Ƿ���ʾȡ����ӡ�Ի���</param>
            /// <param name="dwc">DataWindowControl</param>
            /// <param name="error">������Ϣ</param>
            /// <returns>1���ɹ�����1��ʧ��</returns>
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
