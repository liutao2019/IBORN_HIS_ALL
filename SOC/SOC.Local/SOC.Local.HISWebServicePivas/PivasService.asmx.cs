using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Collections;
using FS.FrameWork.Function;

namespace SOC.Local.HISWebServiceTest
{
    /// <summary>
    /// PivasService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class PivasService : System.Web.Services.WebService
    {

        string errXml = @"<Response><ResultCode>{0}</ResultCode><ErrorMsg>{1}</ErrorMsg></Response>";
        
        #region 变量

        /// <summary>
        /// 退费、收费业务层
        /// </summary>
        PivasConfirm PivasConfirmMgr = new PivasConfirm();
        /// <summary>
        /// 人员管理业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Person employeeManager = new FS.HISFC.BizLogic.Manager.Person();

        #endregion 

        #region 返回错误信息
        /// <summary>
        /// 返回正确信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private string GetSucReturn(string errInfo)
        {
            return string.Format(errXml, 1, errInfo);
        }
        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private string GetFailReturn(string errInfo)
        {
            return string.Format(errXml, -1, errInfo);
        }
        #endregion

        #region 申请退费接口
        /// <summary>
        /// 申请退费确认接口
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "申请退费确认接口")]
        public string ApplyConfirm(string xml)
        {
            #region 解析xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            //操作员编号
            XmlNode node = xmlDoc.SelectSingleNode("/Request/EmpCode");
            if (node == null)
            {
                return GetFailReturn("确认操作员编号EmpCode节点为空！");
            }
            string empCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(empCode))
            {
                return GetFailReturn("确认操作员编号EmpCode节点内容为空！");
            }
            //住院流水号
            node = xmlDoc.SelectSingleNode("/Request/InPatientNo");
            if (node == null)
            {
                return GetFailReturn("确认住院流水号InPatientNo节点为空！");
            }
            string inPatientNo = node.InnerText.Trim();
            if (string.IsNullOrEmpty(inPatientNo))
            {
                return GetFailReturn("确认住院流水号InPatientNo节点内容为空！");
            }
            //类型 1药品 2非药品
            node = xmlDoc.SelectSingleNode("/Request/ItemType");
            if (node == null)
            {
                return GetFailReturn("确认住院类型ItemType节点为空！");
            }
            string itemType = node.InnerText.Trim();
            if (string.IsNullOrEmpty(itemType))
            {
                return GetFailReturn("确认住院类型ItemType节点内容为空！");
            }
            //医嘱执行档单号
            node = xmlDoc.SelectSingleNode("/Request/ExecOrderID");
            if (node == null)
            {
                return GetFailReturn("确认医嘱执行档单号ExecOrderID节点为空！");
            }
            string execOrderID = node.InnerText.Trim();
            if (string.IsNullOrEmpty(execOrderID))
            {
                return GetFailReturn("确认医嘱执行档单号ExecOrderID节点内容为空！");
            }
            //退费数量
            node = xmlDoc.SelectSingleNode("/Request/NoBackQty");
            if (node == null)
            {
                return GetFailReturn("确认退费数量NoBackQty节点为空！");
            }
            string noBackQty = node.InnerText.Trim();
            Decimal noBackQtyDecimal = 0;
            if (string.IsNullOrEmpty(noBackQty))
            {
                return GetFailReturn("确认退费数量NoBackQty节点内容为空！");
            }
            else
            {
                noBackQtyDecimal = Convert.ToDecimal(noBackQty);
            }
            #endregion

            string errInfo = string.Empty;
            //当前操作员
            FS.HISFC.Models.Base.Employee empoyee = employeeManager.GetPersonByID(empCode);
            if (!string.IsNullOrEmpty(empoyee.ID))
            {
                FS.FrameWork.Management.Connection.Operator = empoyee;
            }
            else
            {
                return GetFailReturn("编号为“" + empCode + "”的操作员信息在HIS系统中不存在，请验证后输入！");
            }
            //退费
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            PivasConfirmMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (PivasConfirmMgr.ApplyQuit(inPatientNo, itemType, execOrderID, noBackQtyDecimal, ref errInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return GetFailReturn(errInfo);
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return GetSucReturn(errInfo);
        }

        /// <summary>
        /// 申请退费系统获取接口xml测试字符
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "申请退费系统获取接口xml测试字符")]
        public string GetApplyConfirmTestString()
        {
            /*
             * 操作员编号           EmpCode
             * 住院流水号           InPatientNo
             * 类型（药品1/非药品2）ItemType
             * 医嘱执行单号         ExecOrderID
             * 退费数量             NoBackQty
             */
            return @"<Request><EmpCode>009999</EmpCode><InPatientNo>930609</InPatientNo><ItemType>2</ItemType><ExecOrderID>27204043</ExecOrderID><NoBackQty>1</NoBackQty></Request>";
        }
        #endregion

        #region 确认收费接口
        /// <summary>
        /// 收费系统确认接口
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
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
                return GetFailReturn("确认操作员编号EmpCode节点为空！");
            }
            string empCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(empCode))
            {
                return GetFailReturn("确认操作员编号EmpCode节点内容为空！");
            }
            #endregion

            #region 药品医嘱执行号
            node = xmlDoc.SelectSingleNode("/Request/ExecOrderID");
            if (node == null)
            {
                return GetFailReturn("确认住院流水号ExecOrderID节点为空！");
            }
            string execOrderID = node.InnerText.Trim();
            if (string.IsNullOrEmpty(execOrderID))
            {
                return GetFailReturn("确认住院流水号ExecOrderID节点内容为空！");
            }
            #endregion

            #region 非药品项目编号
            node = xmlDoc.SelectSingleNode("/Request/ItemCode");
            if (node == null)
            {
                return GetFailReturn("确认非药品项目编号ItemCode节点为空！");
            }
            string itemCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(itemCode))
            {
                return GetFailReturn("确认非药品项目编号ItemCode节点内容为空！");
            }
            #endregion

            #region 非药品数量
            node = xmlDoc.SelectSingleNode("/Request/ItemQty");
            if (node == null)
            {
                return GetFailReturn("确认非药品数量ItemQty节点为空！");
            }
            string itemQty = node.InnerText.Trim();
            decimal itemQtyDecimal = 0;
            if (string.IsNullOrEmpty(itemQty))
            {
                return GetFailReturn("确认非药品数量ItemQty节点内容为空！");
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
                return GetFailReturn("确认开立医生编号DoctCode节点为空！");
            }
            string doctCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(doctCode))
            {
                return GetFailReturn("确认开立医生编号DoctCode节点内容为空！");
            }
            #endregion

            #region 执行科室编码
            node = xmlDoc.SelectSingleNode("/Request/ExecDeptCode");
            if (node == null)
            {
                return GetFailReturn("确认执行科室编码ExecDeptCode节点为空！");
            }
            string execDeptCode = node.InnerText.Trim();
            if (string.IsNullOrEmpty(execDeptCode))
            {
                return GetFailReturn("确认执行科室编码ExecDeptCode节点内容为空！");
            }
            #endregion

            #region 组合序号
            node = xmlDoc.SelectSingleNode("/Request/ComBoNo");
            if (node == null)
            {
                return GetFailReturn("确认组合序号ComBoNo节点为空！");
            }
            string comBoNo = node.InnerText.Trim();
            #endregion

            #endregion

            string errInfo = string.Empty;
            //当前操作员
            FS.HISFC.Models.Base.Employee empoyee = employeeManager.GetPersonByID(empCode);
            if (!string.IsNullOrEmpty(empoyee.ID))
            {
                FS.FrameWork.Management.Connection.Operator = empoyee;
            }
            else
            {
                return GetFailReturn("编号为“" + empCode + "”的操作员信息在HIS系统中不存在，请验证后输入！");
            }
            //退费
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            PivasConfirmMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (PivasConfirmMgr.SaveChargeInfo(execOrderID, itemCode, itemQtyDecimal, doctCode, execDeptCode, comBoNo, ref errInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return GetFailReturn(errInfo);
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return GetSucReturn(errInfo);
        }
        /// <summary>
        /// 收费系统获取接口xml测试字符
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true, BufferResponse = true, Description = "收费系统获取接口xml测试字符")]
        public string GetSaveConfirmTestString()
        {
            /*
             * 操作员编号           EmpCode
             * 药品医嘱执行单号     ExecOrderID
             * 非药品项目编码       ItemCode
             * 非药品项目数量       ItemQty
             * 开立医生编号         DoctCode
             * 执行科室编号         ExecDeptCode
             * 组合序号             ComBoNo
             */
            return @"<Request><EmpCode>009999</EmpCode><ExecOrderID>27204041</ExecOrderID><ItemCode>F00002245598</ItemCode><ItemQty>1</ItemQty><DoctCode>02311L</DoctCode><ExecDeptCode>0611</ExecDeptCode><ComBoNo>1</ComBoNo></Request>";//非药品
        }
        #endregion
    }
}
