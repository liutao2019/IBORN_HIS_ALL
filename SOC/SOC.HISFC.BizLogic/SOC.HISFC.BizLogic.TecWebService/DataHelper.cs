using System;
using System.Xml;
using System.Reflection;

/// <summary>
/// 连接his数据库
/// </summary>
public class DataHelper 
{

    #region  数据库操作
    
    public static string  error = string.Empty;


    #endregion

    public static  void InitConnection()
    {

        //string serverPath = System.Web.Configuration.WebConfigurationManager.AppSettings["URL"]+ "HisProfile.xml";

        //System.IO.Stream file = typeof(DataHelper).Assembly.GetManifestResourceStream(Assembly.GetExecutingAssembly().FullName.Split(',')[0] + ".Code.xml");

        if (FS.FrameWork.Management.Connection.GetSettingPB(System.AppDomain.CurrentDomain.BaseDirectory, out error) == -1)
        {
            error = "连接数据库出错！";
        }

        FS.FrameWork.Management.Connection.Sql = new FS.FrameWork.Management.Sql(FS.FrameWork.Management.Connection.Instance);


        try
        {
            if (System.IO.File.Exists("DebugSql.log"))
            {
                System.IO.File.Delete("DebugSql.log");
                System.IO.File.CreateText("DebugSql.log");
            }
        }
        catch { }
    }


}