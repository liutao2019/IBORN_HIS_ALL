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
    public class FSDataWindow : Sybase.DataWindow.DataWindowControl
    {
        private Sybase.DataWindow.Transaction SQLCA;
        private Sybase.DataWindow.AdoTransaction trans;
        private string error;

        private System.Data.DataSet ds = null;
        private Function myfun = new Function();
        private System.Data.DataView dv = null;

        public System.Data.DataView Dv
        {
            get { return dv; }
        }

        public string Error
        {
            get
            {
                return this.error;
            }
        }

        protected int Connect()
        {
            //if (this.SQLCA == null && trans == null)
            //{
            //    if (DBConfig.Init() == -1)
            //        return -1;

            //    if (DBConfig.DbType.StartsWith("Oracle"))        //Oracle数据库
            //    {
            //        #region Oracle 数据库

            //        this.SQLCA = new Sybase.DataWindow.Transaction();
            //        System.Data.OracleClient.OracleConnectionStringBuilder ocs =
            //            new System.Data.OracleClient.OracleConnectionStringBuilder(/*"data source=CHCC;password=his;persist security info=True;user id=HIS45"*/FS.FrameWork.Management.Connection.Instance.ConnectionString);



            //        SQLCA.Password = ocs.Password;
            //        SQLCA.ServerName = ocs.DataSource;
            //        SQLCA.UserId = ocs.UserID;

            //        try
            //        {
            //            SQLCA.Dbms = (Sybase.DataWindow.DbmsType)Enum.Parse(typeof(Sybase.DataWindow.DbmsType), DBConfig.DbType);
            //        }
            //        catch
            //        {
            //            SQLCA.Dbms = Sybase.DataWindow.DbmsType.Oracle8i;
            //        }

            //        SQLCA.AutoCommit = false;
            //        SQLCA.DbParameter = DBConfig.DbParameter;

            //        try
            //        {
            //            SQLCA.Connect();
            //            this.SetTransaction(this.SQLCA);
            //        }
            //        catch (Exception e)
            //        {
            //            this.error = e.Message;
            //            return -1;
            //        }

            //        #endregion

            //        return 0;

            //    }
            //    else if (DBConfig.DbType.StartsWith("Sql"))
            //    {
            //        #region SqlServer数据库

            //        System.Data.SqlClient.SqlConnection oleCn = new System.Data.SqlClient.SqlConnection();
            //        oleCn.ConnectionString = DBConfig.DbParameter;

            //        try
            //        {
            //            oleCn.Open();

            //            this.trans = new Sybase.DataWindow.AdoTransaction(oleCn);
            //            this.trans.BindConnection();

            //            this.SetTransaction(this.trans);
            //        }
            //        catch (Exception e)
            //        {
            //            this.error = e.Message;
            //            return -1;
            //        }

            //        #endregion

            //        return 0;
            //    }
            //    else
            //    {
            //        #region OleDB

            //        System.Data.OleDb.OleDbConnection oleCn = new System.Data.OleDb.OleDbConnection();
            //        oleCn.ConnectionString = FS.FrameWork.Management.Connection.Instance.ConnectionString;
            //        try
            //        {
            //            oleCn.Open();

            //            this.trans = new Sybase.DataWindow.AdoTransaction(oleCn);
            //            this.trans.BindConnection();

            //            this.SetTransaction(this.trans);
            //        }
            //        catch (Exception e)
            //        {
            //            this.error = e.Message;
            //            return -1;
            //        }

            //        #endregion

            //        return 0;
            //    }
            //}

            //try
            //{
            //    if(this.SQLCA!=null)
            //    {
            //        this.SQLCA.Connect();
            //        this.SetTransaction(this.SQLCA);
            //    }
            //}
            //catch (Exception e)
            //{
            //    this.error = e.Message;
            //    return -1;
            //}
            //try
            //{
            //    if (this.trans != null)
            //    {
            //        if (this.trans.Connection.State == System.Data.ConnectionState.Closed)
            //        {
            //            this.trans.Connection.Open();
            //            this.trans.BindConnection();

            //            this.SetTransaction(this.trans);
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    this.error = e.Message;
            //    return -1;
            //}
            return 0;
        }

        protected void Disconnect()
        {
            //if (this.SQLCA != null)
            //{
            //    this.SQLCA.Disconnect();                
            //}

            //if(this.trans!=null && this.trans.Connection != null)
            //{
            //    this.trans.Connection.Close();

            //    this.trans = null;
            //}
        }

        /// <summary>
        /// Retrieve数据
        /// </summary>
        /// <param name="objects"></param>
        /// <returns>大于等于0 记录数，-1 失败</returns>
        public new int Retrieve(params object[] objects)
        {
            //if (this.Connect() == -1)
            //    return -1;
            //int ret = base.Retrieve(objects);
            //this.Disconnect();

            //return ret;

            if (ds == null)
            {
                ds = new System.Data.DataSet();
            }
            else
            {
                ds.Clear();
            }
            this.Reset();
            string sql = string.Empty;
            sql = this.GetSqlSelect().Replace("\"", "");
            //sql.ToUpper();
            //if (this.Connect() == -1)
            //    return -1;
            //int ret = base.Retrieve(objects);
            //this.Disconnect();
            string argument = string.Empty;
            argument = this.Describe("DataWindow.Table.Arguments");
            string[] s1 = argument.Split('\n');
            string[] s2 = new string[2];
            for (int i = 0; i < s1.Length; i++)
            {
                s2 = s1[i].Split('\t');
                this.ReplaceString(ref sql, s2, i);
            }
            if (this.Style == Sybase.DataWindow.DataWindowStyle.Crosstab)
            {
                System.Windows.Forms.MessageBox.Show("暂时不支持交叉报表");
                return 1;
            }

            ///// <summary>
            /////传入参数是数组的情况
            ///// </summary>
            ///// <param name="objects"></param>
            ///// <returns>大于等于0 记录数，-1 失败</returns>

            //for (int i = 0; i < objects.Length; i++)
            //{
            //    object obj = objects[i];
            //    string[] array;
            //    try
            //    {
            //        array = (string[])obj;
            //    }
            //    catch
            //    {
            //        continue;
            //    }
            //    string str = "";
            //    for (int j = 0; j < array.Length; j++)
            //    {
            //        str += "'" + array[j] + "'";
            //        if (j != array.Length - 1)
            //        {
            //            str += ",";
            //        }
            //    }
            //    objects[i] = str;
            //}

            object[] newObjs = new object[objects.Length];
            int j = 0;
            foreach (object var in objects)
            {
                if (var.GetType().ToString() == "System.String[]")
                {
                    string tmp = string.Empty;
                    if ((var as Array).Length == 1)
                    {
                        tmp = (var as Array).GetValue(0).ToString();
                    }
                    else
                    {
                        foreach (string aa in var as Array)
                        {
                            tmp = "'" + aa + "'," + tmp;
                        }
                        tmp = tmp.Substring(0, tmp.Length - 1);
                        tmp = tmp.Substring(1, tmp.Length - 2);
                    }


                    newObjs.SetValue(tmp, j);
                }
                else
                {

                    newObjs.SetValue(var, j);
                }
                j++;
            }

            //sql = string.Format(sql, objects);
            sql = string.Format(sql, newObjs);
            if (this.myfun.ExecQuery(sql, ref ds) >= 0)
            {
                //this.BindAdoDataTable(ds.Tables[0]);
                this.BindDataView(ds.Tables[0].DefaultView);
                this.dv = ds.Tables[0].DefaultView;
                this.CalculateGroups();
                //this.BindAdoDataTable(ds.Tables[0]);
            }
            return 1;
        }

        /// <summary>
        /// 分解字符串  现在加了两个类型 还有其他类型没有增加
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="s2"></param>
        /// <param name="sx"></param>
        private void ReplaceString(ref string sql, string[] s2, int sx)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(":[ ]*" + s2[0], System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            while (rex.IsMatch(sql))
            {
                System.Text.RegularExpressions.Match match = rex.Match(sql);
                if (match.Success)
                {
                    string replaceKey = "";
                    switch (s2[1].ToUpper())
                    {
                        case "DATETIME":
                        case "DATE":
                            replaceKey = "to_date('{" + sx + "}','yyyy-mm-dd hh24:mi:ss')";
                            break;
                        case "STRING":
                            replaceKey = "'{" + sx + "}'";
                            break;
                        case "NUMBER":
                            replaceKey = "'{" + sx + "}'";
                            break;
                        case "STRINGLIST":
                            replaceKey = "'{" + sx + "}'";
                            break;
                    }

                    if (match.Index + match.Length >= sql.Length)
                    {
                        sql = sql.Substring(0, match.Index) + replaceKey;
                    }
                    else
                    {
                        sql = sql.Substring(0, match.Index) + replaceKey + sql.Substring(match.Index + match.Length);
                    }
                }
            }

            //switch (s2[1].ToUpper())
            //{
            //    case "DATETIME":
            //    case "DATE":
            //        sql = sql.Replace(":" + s2[0], "to_date('{" + sx + "}','YYYY-MM-DD HH24:MI:SS')");
            //        break;
            //    case "STRING":
            //        sql = sql.Replace(":" + s2[0], "'{" + sx + "}'");
            //        break;
            //    case "INT":
            //        sql = sql.Replace(":" + s2[0], "{" + sx + "}");
            //        break;
            //    case "NUMBER":
            //        sql = sql.Replace(":" + s2[0], "{" + sx + "}");
            //        break;
            //    case "DECIMAL":
            //        sql = sql.Replace(":" + s2[0], "{" + sx + "}");
            //        break;
            //    case "STRINGLIST":
            //        //sql = sql.Replace(":" + s2[0], "'{" + sx + "}'");
            //        sql = sql.Replace(":" + s2[0], "{" + sx + "}");
            //        break;
            //}

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
                    //防止启动路径改变而找不到配置文件 {83920EC5-57A6-4179-8BF3-B8EC78D503BC}
                    if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\FSDataWindow.xml"))
                    {
                        doc.Load(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\FSDataWindow.xml");
                        System.Xml.XmlNode node = doc.SelectSingleNode("/Config/DB");
                        DbType = node.Attributes[0].Value;
                        DbParameter = node.Attributes[1].Value;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("文件FSDataWindow.xml在" + FS.FrameWork.WinForms.Classes.Function.CurrentPath + " 下没有找到");
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
    }
}
