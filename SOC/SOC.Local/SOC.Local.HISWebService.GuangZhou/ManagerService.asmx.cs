using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace FS.SOC.Local.HISWebService.GuangZhou
{
    /// <summary>
    /// ManagerService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ManagerService : System.Web.Services.WebService
    {
        /// <summary>
        /// NetTest
        /// </summary>
        /// <returns>0－成功、1－失败</returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "测试")]
        public string NetTest()
        {
            return FS.FrameWork.Server.PoolingNew.CreateInstance().GetConnection(FS.FrameWork.Management.Connection.ApplicationID).State.ToString();
        }

        /// <summary>
        /// PoolingNum
        /// </summary>
        /// <returns>0－成功、1－失败</returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "连接池数量")]
        public string PoolingNum()
        {
            return FS.FrameWork.Server.PoolingNew.CreateInstance().ToString();
        }

        /// <summary>
        /// ClearPoolingNum
        /// </summary>
        /// <returns>0－成功、1－失败</returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "清空连接池")]
        public int ClearPoolingNum()
        {
            FS.FrameWork.Server.PoolingNew.CreateInstance().ClearDB();

            return 0;
        }
    }
}
