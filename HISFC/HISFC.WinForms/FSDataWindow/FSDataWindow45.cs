using System;
using System.Collections.Generic;
using System.Text;

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

                if(DBConfig.DbType.StartsWith("Oracle"))        //Oracle���ݿ�
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
        /// Retrieve����
        /// </summary>
        /// <param name="objects"></param>
        /// <returns>���ڵ���0 ��¼����-1 ʧ��</returns>
        public new int Retrieve(params object[] objects)
        {
            if (this.Connect() == -1)
                return -1;
            int ret = base.Retrieve(objects);
            this.Disconnect();

            return ret;
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
                    doc.Load(".\\FSDataWindow.xml");
                    System.Xml.XmlNode node = doc.SelectSingleNode("/Config/DB");
                    DbType=node.Attributes[0].Value;
                    DbParameter=node.Attributes[1].Value;                    
                }
                catch
                {
                    
                    System.Windows.Forms.MessageBox.Show("�ļ�FSDataWindow.xml����!");
                    return -1;
                }

                return 0;
            }
        };
    }
}
