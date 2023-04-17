using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml;

namespace FS.SOC.Local.HISWebService.GuangZhou
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class TerminalService : System.Web.Services.WebService
    {
        string errXml = @"<Response><ResultCode>{0}</ResultCode><ErrorMsg>{1}</ErrorMsg></Response>";
        TerminalConfirm terminalConfirmMgr = new TerminalConfirm();

        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private string GetReturn()
        {
            return string.Format(errXml, 1, "成功");
        }

        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private string GetReturn(string errInfo)
        {
            return string.Format(errXml, -1, errInfo);
        }

        /// <summary>
        /// SaveConfirm
        /// </summary>
        /// <returns>0－成功、1－失败</returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "住院Pacs系统、超声系统、心电图系统费用确认接口")]
        public string SaveConfirm(string xml)
        {
            //解析xml
            #region 解析
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            //申请单号
            XmlNode node= xmlDoc.SelectSingleNode("/Request/AppNo");
            if (node == null)
            {
                return GetReturn("申请单号AppNo节点为空！");
            }
            string applyNO = node.InnerText.Trim();
            if (string.IsNullOrEmpty(applyNO))
            {
                return GetReturn( "申请单号AppNo节点内容为空！");
            }

            //住院流水号
            node = xmlDoc.SelectSingleNode("/Request/InpatientNo");
            if (node == null)
            {
                return GetReturn( "住院流水号InpatientNo节点为空！");
            }
            string inpatientNO = node.InnerText.Trim();
            if (string.IsNullOrEmpty(inpatientNO))
            {
                return GetReturn("住院流水号InpatientNo节点内容为空！");
            }

            //确认人
            node = xmlDoc.SelectSingleNode("/Request/ConfirmDoctId");
            if (node == null)
            {
                return GetReturn("确认医生编码ConfirmDoctId节点为空！");
            }
            string confirmDoctCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(confirmDoctCode))
            {
                return GetReturn("确认医生编码ConfirmDoctId节点内容为空！");
            }

            //确认科室
            node = xmlDoc.SelectSingleNode("/Request/ConfirmDept");
            if (node == null)
            {
                return GetReturn("确认科室编码ConfirmDept节点为空！");
            }
            string confirmDeptCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(confirmDeptCode))
            {
                return GetReturn( "确认科室编码ConfirmDept节点内容为空！");
            }
            #endregion
            string errInfo=string.Empty;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            terminalConfirmMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (terminalConfirmMgr.ComfirmFee(inpatientNO, applyNO, confirmDeptCode, confirmDoctCode, ref errInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return GetReturn( errInfo);
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return GetReturn();
        }

        /// <summary>
        /// SaveConfirm
        /// </summary>
        /// <returns>0－成功、1－失败</returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "住院Pacs系统、超声系统、心电图系统取消费用确认接口")]
        public string CancelConfirm(string xml)
        {
            //解析xml
            #region 解析
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            //申请单号
            XmlNode node = xmlDoc.SelectSingleNode("/Request/AppNo");
            if (node == null)
            {
                return GetReturn("申请单号AppNo节点为空！");
            }
            string applyNO = node.InnerText.Trim();
            if (string.IsNullOrEmpty(applyNO))
            {
                return GetReturn("申请单号AppNo节点内容为空！");
            }

            //住院流水号
            node = xmlDoc.SelectSingleNode("/Request/InpatientNo");
            if (node == null)
            {
                return GetReturn("住院流水号InpatientNo节点为空！");
            }
            string inpatientNO = node.InnerText.Trim();
            if (string.IsNullOrEmpty(inpatientNO))
            {
                return GetReturn("住院流水号InpatientNo节点内容为空！");
            }

            //确认人
            node = xmlDoc.SelectSingleNode("/Request/ConfirmDoctId");
            if (node == null)
            {
                return GetReturn("确认医生编码ConfirmDoctId节点为空！");
            }
            string confirmDoctCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(confirmDoctCode))
            {
                return GetReturn("确认医生编码ConfirmDoctId节点内容为空！");
            }

            //确认科室
            node = xmlDoc.SelectSingleNode("/Request/ConfirmDept");
            if (node == null)
            {
                return GetReturn("确认科室编码ConfirmDept节点为空！");
            }
            string confirmDeptCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(confirmDeptCode))
            {
                return GetReturn("确认科室编码ConfirmDept节点内容为空！");
            }
            #endregion
            string errInfo = string.Empty;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            terminalConfirmMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (terminalConfirmMgr.CancelConfirmFee(inpatientNO, applyNO, confirmDeptCode, confirmDoctCode, ref errInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return GetReturn(errInfo);
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return GetReturn();
        }

        [WebMethod(EnableSession = true, BufferResponse = true, Description = "住院Pacs系统、超声系统、心电图系统获取接口xml测试字符")]
        public string GetConfirmTestString()
        {
            return @"<Request><AppNo>16295</AppNo><InpatientNo>21736</InpatientNo><ConfirmDoctId>009999</ConfirmDoctId><ConfirmDept>6064</ConfirmDept></Request>";
        }

    }
}
