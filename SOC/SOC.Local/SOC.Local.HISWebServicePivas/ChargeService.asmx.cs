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
using Neusoft.FrameWork.Function;

namespace SOC.Local.HISWebServiceTest
{
    /// <summary>
    /// ChargeService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ChargeService : System.Web.Services.WebService
    {
        string errXml = @"<Response><ResultCode>{0}</ResultCode><ErrorMsg>{1}</ErrorMsg></Response>";

        #region 变量

        SOC.Local.HISWebServiceTest.ChargeConfirm chargeConfirmMgr = new SOC.Local.HISWebServiceTest.ChargeConfirm();
        /// <summary>
        /// 人员管理业务层
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.Person employeeManager = new Neusoft.HISFC.BizLogic.Manager.Person();

        #endregion

        #region 返回错误信息
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
        #endregion

        /// <summary>
        /// 收费系统确认接口
        /// </summary>
        /// <returns>-1 失败,1 成功</returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "收费系统确认接口")]
        public string SaveConfirm(string xml)
        {
            #region 解析xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            #region 操作员编号
            XmlNode node = xmlDoc.SelectSingleNode("/Request/EmpCode");
            if (node == null)
            {
                return GetReturn("确认操作员编号EmpCode节点为空！");
            }
            string empCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(empCode))
            {
                return GetReturn("确认操作员编号EmpCode节点内容为空！");
            }
            #endregion

            #region 药品医嘱执行号
            node = xmlDoc.SelectSingleNode("/Request/ExecOrderID");
            if (node == null)
            {
                return GetReturn("确认住院流水号ExecOrderID节点为空！");
            }
            string execOrderID = node.InnerText.Trim();
            if (string.IsNullOrEmpty(execOrderID))
            {
                return GetReturn("确认住院流水号ExecOrderID节点内容为空！");
            }
            #endregion

            #region 非药品项目编号
            node = xmlDoc.SelectSingleNode("/Request/ItemCode");
            if (node == null)
            {
                return GetReturn("确认非药品项目编号ItemCode节点为空！");
            }
            string itemCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(itemCode))
            {
                return GetReturn("确认非药品项目编号ItemCode节点内容为空！");
            }
            #endregion

            #region 非药品数量
            node = xmlDoc.SelectSingleNode("/Request/ItemQty");
            if (node == null)
            {
                return GetReturn("确认非药品数量ItemQty节点为空！");
            }
            string itemQty = node.InnerText.Trim();
            decimal itemQtyDecimal = 0;
            if (string.IsNullOrEmpty(itemQty))
            {
                return GetReturn("确认非药品数量ItemQty节点内容为空！");
            }
            else
            {
                itemQtyDecimal = NConvert.ToDecimal(itemQty);
            }
            #endregion

            #region 开立医生编号
            node = xmlDoc.SelectSingleNode("/Request/DoctCode");
            if (node == null)
            {
                return GetReturn("确认开立医生编号DoctCode节点为空！");
            }
            string doctCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(doctCode))
            {
                return GetReturn("确认开立医生编号DoctCode节点内容为空！");
            }
            #endregion

            #region 执行科室编码
            node = xmlDoc.SelectSingleNode("/Request/ExecDeptCode");
            if (node == null)
            {
                return GetReturn("确认执行科室编码ExecDeptCode节点为空！");
            }
            string execDeptCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(execDeptCode))
            {
                return GetReturn("确认执行科室编码ExecDeptCode节点内容为空！");
            }
            #endregion

            #region 组合序号
            node = xmlDoc.SelectSingleNode("/Request/ComBoNo");
            if (node == null)
            {
                return GetReturn("确认组合序号ComBoNo节点为空！");
            }
            string comBoNo = node.InnerText.Trim();
            #endregion

            #endregion

            string errInfo = string.Empty;
            //当前操作员
            Neusoft.HISFC.Models.Base.Employee empoyee = employeeManager.GetPersonByID(empCode);
            if (!string.IsNullOrEmpty(empoyee.ID))
            {
                Neusoft.FrameWork.Management.Connection.Operator = empoyee;
            }
            else
            {
                return GetReturn("编号为“" + empCode + "”的操作员信息在HIS系统中不存在，请验证后输入！");
            }
            //退费
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            chargeConfirmMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            if (chargeConfirmMgr.SaveChargeInfo(execOrderID, itemCode, itemQtyDecimal, doctCode, execDeptCode,comBoNo, ref errInfo) < 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                return GetReturn(errInfo);
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            return GetReturn(errInfo);
        }

        /// <summary>
        /// 收费系统获取接口xml测试字符
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "收费系统获取接口xml测试字符")]
        public string GetConfirmTestString()
        {
            /*
             * 操作员编号           EmpCode
             * 药品医嘱执行号       execOrderID
             * 非药品项目编码       ItemCode
             * 非药品项目数量       ItemQty
             * 开立医生编号         DoctCode
             * 执行科室编号         ExecDeptCode
             * 组合序号             ComBoNo
             */
            return @"<Request><EmpCode>009999</EmpCode><ExecOrderID>27203972</ExecOrderID><ItemCode>F00002238116</ItemCode><ItemQty>1</ItemQty><DoctCode>02311L</DoctCode><ExecDeptCode>3272</ExecDeptCode><ComBoNo>1</ComBoNo></Request>";//非药品
        }
    }
}
