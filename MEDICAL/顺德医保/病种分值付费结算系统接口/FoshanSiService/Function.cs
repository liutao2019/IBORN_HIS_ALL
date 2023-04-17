using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Runtime.InteropServices;

namespace FoshanSiService
{
    public class Function
    {
        [DllImport("Audit4Hospital.dll")]
        public static extern string Audit4HospitalPortal(string args);//初始化
    }
}
