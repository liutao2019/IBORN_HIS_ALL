using System;
using System.Collections.Generic;
using System.Text;

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
    public class NeuDataWindow45 : Sybase.DataWindow.DataWindowControl
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
        protected int Connect()
        {
            if(this.SQLCA==null && trans==null)
            {
                if(DBConfig.Init()==-1)
                    return -1;                

                if(DBConfig.DbType.StartsWith("Oracle"))        //Oracle数据库
                {
                    
                    this.SQLCA = new Sybase.DataWindow.Transaction();
                    System.Data.OracleClient.OracleConnectionStringBuilder ocs =
                        new System.Data.OracleClient.OracleConnectionStringBuilder(/*"data source=CHCC;password=his;persist security info=True;user id=HIS45"*/FS.FrameWork.Management.Connection.DataSouceString);                
                    
                    SQLCA.Password = ocs.Password;
                    SQLCA.ServerName = ocs.DataSource;
                    SQLCA.UserId = ocs.UserID;
                    
                    try
                    {
                        SQLCA.Dbms = (Sybase.DataWindow.DbmsType)Enum.Parse(typeof(Sybase.DataWindow.DbmsType), DBConfig.DbType);
                    }catch
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
            
                }else
                {
                    System.Data.SqlClient.SqlConnection oleCn = new System.Data.SqlClient.SqlConnection();
                    oleCn.ConnectionString=DBConfig.DbParameter;
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
                    
                    this.trans=new Sybase.DataWindow.AdoTransaction(oleCn);
                    this.trans.BindConnection();
                    return 0;
                }                
                
            }

            try
            {
                if(this.SQLCA!=null)
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
            if (this.SQLCA != null)
            {
                this.SQLCA.Disconnect();
            }

            if(this.trans!=null && this.trans.Connection != null)
            {
                this.trans.Connection.Close();
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
            int ret = base.Retrieve(objects);
            this.Disconnect();

            return ret;
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
                    doc.Load(".\\FSDataWindow.xml");
                    System.Xml.XmlNode node = doc.SelectSingleNode("/Config/DB");
                    DbType=node.Attributes[0].Value;
                    DbParameter=node.Attributes[1].Value;                    
                }
                catch
                {
                    
                    System.Windows.Forms.MessageBox.Show("文件FSDataWindow.xml错误!");
                    return -1;
                }

                return 0;
            }
        };
    }
}
