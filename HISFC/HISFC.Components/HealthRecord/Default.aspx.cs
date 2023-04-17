using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    Neusoft.WebPrint.BLL.CaseWebPrint CaseWebPrint = new Neusoft.WebPrint.BLL.CaseWebPrint();
    
    protected void lbtnFirstPage_Click(object sender, EventArgs e)
    {
        string hisInpatientID =  string.Empty;

        if (Request.QueryString["hisInpatientID"] != null)
        {
            hisInpatientID = Request.QueryString["hisInpatientID"].ToString();
        }
        //string inpatientNo = "21630";//string.Empty;
        string fileName = Page.Server.MapPath("~/images/ucCaseFirstPrint.jpg");

        if (!string.IsNullOrEmpty(hisInpatientID) && CaseWebPrint.PrintFirstPage(hisInpatientID, fileName) > 0)
        {
            this.Image1.ImageUrl = "~/images/ucCaseFirstPrint.jpg" + "?temp=" + DateTime.Now.Millisecond.ToString();
        }
        else
        {
            this.Image1.ImageUrl = "~/images/ucCaseFirstPrint_Empty.jpg";
        }
    }
    protected void lbtnSecondPage_Click(object sender, EventArgs e)
    {
        string hisInpatientID = string.Empty;

        if (Request.QueryString["hisInpatientID"] != null)
        {
            hisInpatientID = Request.QueryString["hisInpatientID"].ToString();
        }
        //string inpatientNo = "21630";//string.Empty;
        string fileName = Page.Server.MapPath("~/images/ucCaseBackPrint.jpg");

        if (!string.IsNullOrEmpty(hisInpatientID) && CaseWebPrint.PrintSecondPage(hisInpatientID, fileName) > 0)
        {
            this.Image1.ImageUrl = "~/images/ucCaseBackPrint.jpg" + "?temp=" + DateTime.Now.Millisecond.ToString();
        }
        else
        {
            this.Image1.ImageUrl = "~/images/ucCaseBackPrint_Empty.jpg";
        }
    }
}
