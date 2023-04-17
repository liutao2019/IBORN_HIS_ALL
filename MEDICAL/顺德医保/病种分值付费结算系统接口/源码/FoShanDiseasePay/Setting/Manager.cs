using System;
using System.Xml;
using System.Collections;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using FoShanDiseasePay.Base;
using FoShanDiseasePay.Setting;

namespace FoShanDiseasePay.DataBase
{
    /// <summary>
    /// Main 的摘要说明。
    /// </summary>
    public class Manager
    {
        public Manager()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            if (setObj == null)
            {
                setObj = new SettingObject();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static SettingObject setObj;

        private OracleConnection pacscon = null;

        private OracleConnection hiscon = null;

        private SqlConnection liscon = null;

        private System.Data.Odbc.OdbcConnection fssiConnect = null;

        //错误信息
        public string Err = String.Empty;

        //错误代码
        public string ErrCode = String.Empty;

        #region 读取配置相关
        public int Init()
        {
            if (GetSetting(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Setting\FoshanSiUploadSetting.xml") != 0)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 获得配置
        /// </summary>
        /// <param name="FileName">HisProfile.xml文件名</param>
        /// <returns>0正确 -1错误</returns>
        private int GetSetting(string FileName)
        {
            XmlDocument doc;
            FS.FrameWork.Xml.XML manageXml = new FS.FrameWork.Xml.XML();
            doc = manageXml.LoadXml(FileName);
            if (doc == null)
            {
                this.Err = "无法打开文件！" + manageXml.Err;
                this.ErrCode = "-1";
                return -1;
            }
            XmlNode node;
            try
            {
                //HIS数据库设置
                node = doc.SelectSingleNode(@"/设置/HIS数据库设置");
                setObj.SQLConnectionString = node.Attributes[0].Value;

                //DAP数据库设置
                //node = doc.SelectSingleNode(@"/设置/医保前置机地址");
                //setObj.FoShanSIConnectionString = node.Attributes[0].Value;

                //pacs数据库
                node = doc.SelectSingleNode(@"/设置/Pacs数据库设置");
                setObj.PacsConnectionString = node.Attributes[0].Value;

                node = doc.SelectSingleNode(@"/设置/B超数据库设置");
                setObj.RadioConnectionString = node.Attributes[0].Value;

                //LIS数据库
                node = doc.SelectSingleNode(@"/设置/LIS数据库设置");
                setObj.LISConnectionString = node.Attributes[0].Value;

                //ftp设置
                node = doc.SelectSingleNode(@"/设置/设置");
                setObj.SQLByXMl = FS.FrameWork.Function.NConvert.ToBoolean(node.Attributes["sql开关"].Value);

                //his数据库最大连接数
                node = doc.SelectSingleNode(@"/设置/MaxCount设置");
                setObj.MaxConCount = FS.FrameWork.Function.NConvert.ToInt32(node.Attributes[0].Value);

                //医院常数相关
                node = doc.SelectSingleNode(@"/设置/医院设置");
                setObj.HospitalID = node.Attributes["HospitalId"].Value;
                setObj.HospitalName = node.Attributes["HospitalName"].Value;

                node = doc.SelectSingleNode(@"/设置/参数设置");
                setObj.OrgCode = node.Attributes["OrgCode"].Value;
                setObj.UserID = node.Attributes["UserID"].Value;
                setObj.UserCode = node.Attributes["UserCode"].Value;
                setObj.Token = node.Attributes["Token"].Value;

                #region 新接口
                node = doc.SelectSingleNode(@"/设置/新参数设置");
                setObj.NewOrgCode = node.Attributes["OrgCode"].Value;
                setObj.NewUserID = node.Attributes["UserID"].Value;
                setObj.NewUserCode = node.Attributes["UserCode"].Value;
                setObj.NewToken = node.Attributes["Token"].Value;
                setObj.NewSign = FoShanDiseasePay.Common.CommonHelp.GetMD5(setObj.NewToken + setObj.NewUserID);//.ToLower();
                setObj.NewWebServiceAddress = doc.SelectSingleNode(@"/设置/WebServiesDRGS").InnerText;
                #endregion
                //System.Net.IPAddress[] IPAddresses = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
                //setObj.IPAddress = IPAddresses[0].ToString();
                string ipAddress = string.Empty;

                try
                {
                    System.Net.IPHostEntry IpEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                    for (int i = 0; i < IpEntry.AddressList.Length; i++)
                    {
                        if (IpEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddress = IpEntry.AddressList[i].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                setObj.IPAddress = ipAddress;
                setObj.Sign = FoShanDiseasePay.Common.CommonHelp.GetMD5(setObj.Token + setObj.UserID);
                setObj.WebServiceAddress = doc.SelectSingleNode(@"/设置/WebServies").InnerText;
            }
            catch (Exception ex)
            {
                this.Err = "初始化错误!" + ex.ToString();
                this.ErrCode = "-1";
                return -1;
            }
            return 0;
        }
        #endregion

        #region 打开/关闭 数据库相关

        /// <summary>
        /// 获取his数据库连接
        /// </summary>
        /// <returns></returns>
        public OracleConnection GetHisCon()
        {
            if (setObj == null || string.IsNullOrEmpty(setObj.SQLConnectionString))
            {
                return null;
            }

            hiscon = new OracleConnection();
            hiscon.ConnectionString = setObj.SQLConnectionString;
            try
            {
                hiscon.Open();
                return hiscon;
            }
            catch (Exception ex)
            {
                this.Err = "打开his数据库失败！" + ex.ToString();
                this.ErrCode = "-1";
                hiscon.Dispose();
                hiscon = null;
                return null;
            }
        }

        /// <summary>
        /// 获取PACS数据库连接
        /// </summary>
        /// <returns></returns>
        public OracleConnection GetPacsCon()
        {
            if (setObj == null || string.IsNullOrEmpty(setObj.PacsConnectionString))
            {
                return null;
            }

            pacscon = new OracleConnection();
            pacscon.ConnectionString = setObj.PacsConnectionString;
            try
            {
                pacscon.Open();
                return pacscon;
            }
            catch (Exception ex)
            {
                this.Err = "打开PACS数据库失败！" + ex.ToString();
                this.ErrCode = "-1";
                pacscon.Dispose();
                pacscon = null;
                return null;
            }
        }

        /// <summary>
        /// 获取PACS数据库连接
        /// </summary>
        /// <returns></returns>
        public OracleConnection GetRadioCon()
        {
            if (setObj == null || string.IsNullOrEmpty(setObj.RadioConnectionString))
            {
                return null;
            }

            pacscon = new OracleConnection();
            pacscon.ConnectionString = setObj.RadioConnectionString;
            try
            {
                pacscon.Open();
                return pacscon;
            }
            catch (Exception ex)
            {
                this.Err = "打开PACS数据库失败！" + ex.ToString();
                this.ErrCode = "-1";
                pacscon.Dispose();
                pacscon = null;
                return null;
            }
        }


        /// <summary>
        /// 获取LIS数据库连接
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetLisCon()
        {
            if (setObj == null || string.IsNullOrEmpty(setObj.LISConnectionString))
            {
                return null;
            }

            liscon = new SqlConnection();

            liscon.ConnectionString = setObj.LISConnectionString;

            try
            {
                liscon.Open();
                return liscon;
            }
            catch (Exception ex)
            {
                this.Err = "打开LIS数据库失败！" + ex.ToString();
                this.ErrCode = "-1";
                liscon.Dispose();
                liscon = null;
                return null;
            }
        }

        public System.Data.Odbc.OdbcConnection GetFoshanSICon()
        {
            if (setObj == null || string.IsNullOrEmpty(setObj.FoShanSIConnectionString))
            {
                return null;
            }

            fssiConnect = new System.Data.Odbc.OdbcConnection();

            fssiConnect.ConnectionString = setObj.FoShanSIConnectionString;

            try
            {
                fssiConnect.Open();
                return fssiConnect;
            }
            catch (Exception ex)
            {
                this.Err = "打开佛山医保前置机数据库失败！" + ex.ToString();
                this.ErrCode = "-1";
                fssiConnect.Dispose();
                fssiConnect = null;
                return null;
            }
        }

        ///// <summary>
        ///// 获取前置机数据库连接
        ///// </summary>
        ///// <returns></returns>
        //public SqlConnection GetDapCon()
        //{
        //    if(objMassSetting==null || objMassSetting.DAPConnectionString==null && objMassSetting.DAPConnectionString!="")
        //    {
        //        return null;
        //    }
        //    dapcon=new SqlConnection();
        //    dapcon.ConnectionString=objMassSetting.DAPConnectionString;
        //    try
        //    {
        //        dapcon.Open();
        //        return dapcon;
        //    }
        //    catch(Exception ex)
        //    {
        //        this.Err="打开前置机数据库失败！"+ex.ToString();
        //        this.ErrCode="-1";
        //        dapcon.Dispose();
        //        dapcon=null;
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 关闭sql并释放连接
        ///// </summary>
        ///// <param name="Sqlcon"></param>
        //public void CloseDisposeSql(SqlConnection Sqlcon)
        //{
        //    if(Sqlcon!=null)
        //    {
        //        try
        //        {
        //            Sqlcon.Close();
        //            Sqlcon.Dispose();
        //        }
        //        catch
        //        {
        //        }	
        //    }
        //}

        /// <summary>
        /// 关闭oracle并释放连接
        /// </summary>
        /// <param name="Sqlcon"></param>
        public void CloseDisposeOrc(OracleConnection Orccon)
        {
            if (Orccon != null)
            {
                try
                {
                    Orccon.Close();
                    Orccon.Dispose();
                }
                catch
                {
                }
            }
        }

        public void CloseDisposeSql(SqlConnection sqlcon)
        {
            if (sqlcon != null)
            {
                try
                {
                    sqlcon.Close();
                    sqlcon.Dispose();
                }
                catch
                {
                }
            }
        }

        public void CloseDisposeDB2(System.Data.Odbc.OdbcConnection db2Conn)
        {
            if (db2Conn != null)
            {
                try
                {
                    db2Conn.Close();
                    db2Conn.Dispose();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        ///关闭所有的数据库库连接
        /// </summary>
        public void CloseDisposeAll()
        {
            //CloseDisposeSql(this.dapcon);
            CloseDisposeOrc(this.hiscon);
            CloseDisposeOrc(this.pacscon);
            CloseDisposeSql(this.liscon);
            //CloseDisposeDB2(this.fssiConnect);
        }
        #endregion

        #region 查询数据库相关
        /// <summary>
        /// 查询his数据库
        /// </summary>
        /// <param name="Orccon"></param>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public DataSet ExecSql(OracleConnection Orccon, string sqlstr)
        {
            try
            {
                DataSet ds = new DataSet();
                OracleDataAdapter dp = new OracleDataAdapter(sqlstr, Orccon);
                dp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = "执行sql出错;" + ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// 获取sql语句列表
        /// </summary>
        /// <returns></returns>
        public int GetSqlList()
        {
            if (this.hiscon == null)
            {
                if (GetHisCon() == null)
                {
                    this.Err = "打开his数据库失败！";
                    return -1;
                }
            }
            try
            {
                string strsql = "select  id,name,type from com_sql  where team like '%佛山医保%'";
                DataSet ds = ExecSql(this.hiscon, strsql);

                setObj.SqlList = new ArrayList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        FS.FrameWork.Models.NeuObject temp = new FS.FrameWork.Models.NeuObject();
                        temp.ID = dr["id"].ToString();
                        temp.Memo = dr["name"].ToString();
                        temp.Name = dr["type"].ToString();
                        setObj.SqlList.Add(temp);
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                this.Err = "获取sql语句列表失败！" + ex.ToString();
                return -1;
            }
        }


        /// <summary>
        /// 获取常数装换列表
        /// </summary>
        /// <returns></returns>
        public int GetContentList()
        {
            if (this.hiscon == null)
            {
                if (GetHisCon() == null)
                {
                    this.Err = "打开his数据库失败！";
                    return -1;
                }
            }
            try
            {
                string strsql = @"select c.type,c.name,c.input_code from com_dictionary c where c.type like 'FoShanSI%'";
                //string strsql = "select c.type,c.name,c.input_code from com_dictionary c where c.type in "
                //            + "(select a.parameter from priv_com_role_resource a,priv_com_role b "
                //            + "where a.role_id = b.roleid and b.rolename = '社保监控系统')";
                DataSet ds = ExecSql(this.hiscon, strsql);
                setObj.ContentList = new ArrayList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        FS.FrameWork.Models.NeuObject temp = new FS.FrameWork.Models.NeuObject();
                        temp.ID = dr["type"].ToString();
                        temp.Name = dr["name"].ToString();
                        temp.Memo = dr["input_code"].ToString();
                        setObj.ContentList.Add(temp);
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                this.Err = "获取sql语句列表失败！" + ex.ToString();
                return -1;
            }
        }

        /// <summary>
        /// 获取job实体
        /// </summary>
        /// <returns></returns>
        public ArrayList GetJobList()
        {
            ArrayList result = new ArrayList();
            if (this.hiscon == null)
            {
                if (GetHisCon() == null)
                {
                    this.Err = "打开his数据库失败！";
                    return null;
                }
            }
            try
            {
                string strsql = "select JOB_CODE,JOB_NAME,JOB_STATE,LAST_DTIME,NEXT_DTIME,INTERVAL,MARK,DLL_NAME,CLASS_NAME,JOB_STARTTIME,JOB_ENDTIME,OPER_CODE,OPER_DATE  from com_foshanSI_job";
                DataSet ds = ExecSql(this.hiscon, strsql);
                setObj.SqlList = new ArrayList();
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        JobInfo tempinfo = new JobInfo();
                        tempinfo.JOBCODE = dr["JOB_CODE"].ToString();
                        tempinfo.JOBNAME = dr["JOB_NAME"].ToString();
                        tempinfo.JOBSTATE = dr["JOB_STATE"].ToString();
                        tempinfo.LASTDTIME = FS.FrameWork.Function.NConvert.ToDateTime(dr["LAST_DTIME"].ToString());
                        tempinfo.NEXTDTIME = FS.FrameWork.Function.NConvert.ToDateTime(dr["NEXT_DTIME"].ToString());
                        tempinfo.INTERVAL = FS.FrameWork.Function.NConvert.ToInt32(dr["INTERVAL"].ToString());
                        tempinfo.OPERCODE = dr["OPER_CODE"].ToString();
                        tempinfo.OPERDATE = FS.FrameWork.Function.NConvert.ToDateTime(dr["OPER_DATE"].ToString());
                        tempinfo.MARK = dr["MARK"].ToString();
                        tempinfo.DLLNAME = dr["DLL_NAME"].ToString();
                        tempinfo.CLASSNAME = dr["CLASS_NAME"].ToString();
                        tempinfo.JOBSTARTTIME = dr["JOB_STARTTIME"].ToString();
                        tempinfo.JOBENDTIME = dr["JOB_ENDTIME"].ToString();
                        result.Add(tempinfo);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                this.Err = "获取Job列表失败！" + ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// 根据ID获取sql语句数据
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Str"></param>
        public void GetsqlByID(string ID, ref string Str)
        {
            if (setObj.SqlList == null || setObj.SqlList.Count == 0)
            {
                GetSqlList();
            }

            foreach (FS.FrameWork.Models.NeuObject temp in setObj.SqlList)
            {
                if (temp.ID == ID)
                {
                    Str = temp.Memo;
                    return;
                }
            }
            Str = "";
        }

        /// <summary>
        /// 获得常数列的一个实体
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetConstant(string type, string ID)
        {
            if (this.hiscon == null)
            {
                if (GetHisCon() == null)
                {
                    this.Err = "打开his数据库失败！";
                    return null;
                }
            }
            string strSql = @"SELECT TYPE,
                                   CODE,
                                   NAME,
                                   MARK,
                                   SPELL_CODE,
                                   WB_CODE,
                                   INPUT_CODE,
                                   SORT_ID,
                                   VALID_STATE,
                                   OPER_CODE,
                                   OPER_DATE
                              FROM COM_DICTIONARY
                             WHERE TYPE = '{0}'
                               AND CODE = '{1}'
                             ORDER BY SORT_ID";

            strSql = string.Format(strSql, type, ID);

            DataSet ds = ExecSql(this.hiscon, strSql);
            Const cons = new Const();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    cons.ID = dr[1].ToString();
                    cons.Name = dr[2].ToString();
                    cons.Memo = dr[3].ToString();
                    cons.SpellCode = dr[4].ToString();
                    cons.WBCode = dr[5].ToString();
                    cons.UserCode = dr[6].ToString();
                    if (!Convert.IsDBNull(dr[7]))
                        cons.SortID = Convert.ToInt32(dr[7].ToString());
                    cons.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(dr[8].ToString());
                    cons.OperEnvironment.ID = dr[9].ToString();
                    if (!Convert.IsDBNull(dr[10]))
                        cons.OperEnvironment.OperTime = Convert.ToDateTime(dr[10].ToString());
                }
            }
            return cons;
        }
        #endregion
    }
}
