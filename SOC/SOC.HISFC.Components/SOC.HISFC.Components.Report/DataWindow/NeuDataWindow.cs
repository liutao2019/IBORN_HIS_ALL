using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.Report.DataWindow
{
    using FS.FrameWork.WinForms.Classes;
    using Sybase.DataWindow;
    using System;
    using System.Data;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class NeuDataWindow : DataWindowControl
    {
        //private DataSet ds = null;
        //private DataView dv = null;
        private string error;
        private FS.FrameWork.Management.DataBaseManger myfun = new FS.FrameWork.Management.DataBaseManger();
        private Transaction SQLCA;
        private AdoTransaction trans;

        protected int Connect()
        {
            if (this.SQLCA == null)
            {
                this.SQLCA = new Sybase.DataWindow.Transaction();
                System.Data.OracleClient.OracleConnectionStringBuilder ocs =
                    new System.Data.OracleClient.OracleConnectionStringBuilder(FS.FrameWork.Management.Connection.DataSouceString);
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
            }
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
            try
            {
                if (this.trans != null)
                {
                    if (this.trans.Connection.State == System.Data.ConnectionState.Closed)
                    {
                        this.trans.Connection.Open();
                        this.trans.BindConnection();

                        this.SetTransaction(this.trans);
                    }
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

            if (this.trans != null && this.trans.Connection != null)
            {
                this.trans.Connection.Close();

                this.trans = null;
            }
        }

        //private void ReplaceString(ref string sql, string[] s2, int sx)
        //{
        //    if (s2 != null && s2.Length > 1)
        //    {
        //        string str = s2[1].ToUpper();
        //        if (str != null)
        //        {
        //            if (!(str == "DATETIME"))
        //            {
        //                if (str == "STRING")
        //                {
        //                    sql = sql.Replace(":" + s2[0], "'{" + sx + "}'");
        //                }
        //                else if (str == "INT")
        //                {
        //                    sql = sql.Replace(":" + s2[0], "{" + sx + "}");
        //                }
        //                else if (str == "NUMBER")
        //                {
        //                    sql = sql.Replace(":" + s2[0], "{" + sx + "}");
        //                }
        //                else if (str == "DECIMAL")
        //                {
        //                    sql = sql.Replace(":" + s2[0], "{" + sx + "}");
        //                }
        //                else if (str == "STRINGLIST")
        //                {
        //                    sql = sql.Replace(":" + s2[0], "{" + sx + "}");
        //                }
        //            }
        //            else
        //            {
        //                sql = sql.Replace(":" + s2[0], "to_date('{" + sx + "}','YYYY-MM-DD HH24:MI:SS')");
        //            }
        //        }
        //    }
        //}

        public int Retrieve(params object[] objects)
        {
            if (true||base.Style == DataWindowStyle.Crosstab)
            {
                Connect();
                base.Retrieve(objects);
                Disconnect();

            }
            //else
            //{
            //    int num;
            //    if (this.ds == null)
            //    {
            //        this.ds = new DataSet();
            //    }
            //    else
            //    {
            //        this.ds.Clear();
            //    }
            //    base.Reset();
            //    string sql = string.Empty;
            //    sql = base.GetSqlSelect().Replace("\"", "");
            //    string[] strArray = base.Describe("DataWindow.Table.Arguments").Split(new char[] { '\n' });
            //    string[] strArray2 = new string[2];
            //    for (num = 0; num < strArray.Length; num++)
            //    {
            //        strArray2 = strArray[num].Split(new char[] { '\t' });
            //        this.ReplaceString(ref sql, strArray2, num);
            //    }

            //    for (num = 0; num < objects.Length; num++)
            //    {
            //        string[] strArray3;
            //        object obj2 = objects[num];
            //        try
            //        {
            //            strArray3 = (string[])obj2;
            //        }
            //        catch
            //        {
            //            continue;
            //        }
            //        string str3 = "";
            //        for (int i = 0; i < strArray3.Length; i++)
            //        {
            //            str3 = str3 + "'" + strArray3[i] + "'";
            //            if (i != (strArray3.Length - 1))
            //            {
            //                str3 = str3 + ",";
            //            }
            //        }
            //        objects[num] = str3;
            //    }
            //    sql = string.Format(sql, objects);
            //    if (this.myfun.ExecQuery(sql, ref this.ds) >= 0)
            //    {
            //        base.BindDataView(this.ds.Tables[0].DefaultView);
            //        this.dv = this.ds.Tables[0].DefaultView;
            //    }
            //}
            return 1;
        }

        //public DataView Dv
        //{
        //    get
        //    {
        //        return this.dv;
        //    }
        ////}

        public string Error
        {
            get
            {
                return this.error;
            }
        }

        private class DBConfig
        {
            static DBConfig()
            {
                Init();
            }
            public static string DbParameter;
            public static string DbType;

            public static int Init()
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    if (File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\NeuDataWindow.xml"))
                    {
                        document.Load(FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\NeuDataWindow.xml");
                        XmlNode node = document.SelectSingleNode("/Config/DB");
                        DbType = node.Attributes[0].Value;
                        DbParameter = node.Attributes[1].Value;
                    }
                    else
                    {
                        MessageBox.Show("文件NeuDataWindow.xml在" + FS.FrameWork.WinForms.Classes.Function.CurrentPath + " 下没有找到");
                        return -1;
                    }
                }
                catch
                {
                    MessageBox.Show("文件NeuDataWindow.xml错误!");
                    return -1;
                }
                return 0;
            }
        }
    }
}
