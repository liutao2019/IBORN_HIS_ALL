using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;

namespace SOC.Local.HISWebService.PE
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            string errInfo = string.Empty;
            FS.FrameWork.Management.Connection.GetSetting(out errInfo);
            FS.FrameWork.Management.Connection.Hospital.ID = FS.FrameWork.Management.Connection.CoreHospatialCode;
            //启动晚上定时器
            FS.FrameWork.Server.PoolingNew.CreateInstance().RunTimer();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            FS.FrameWork.Server.PoolingNew.CreateInstance().CloseDB(FS.FrameWork.Management.Connection.ApplicationID);
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}