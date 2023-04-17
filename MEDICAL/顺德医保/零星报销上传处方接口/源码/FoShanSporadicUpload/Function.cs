using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace FoShanSporadicUpload
{
    /// <summary>
    /// ********************************************************
    /// 功能描述：佛山市基本医疗保险医院上传零星报销处方接口
    /// 创建日期：2016-04-15
    /// 创 建 人：gumaozhu
    /// 修改日期：
    /// 修 改 人：
    /// 修改内容：
    /// ********************************************************
    /// </summary>
    public class Function
    {
        #region 变量和属性

        /// <summary>
        /// 配置文件
        /// </summary>
        private static string profileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\Setting\SporadicUploadSIConfig.xml";

        /// <summary>
        /// 医院代码  如：A-09
        /// </summary>
        private static string hospitalCode;

        /// <summary>
        /// 医院代码
        /// </summary>
        public static string HospitalCode
        {
            get
            {
                if (string.IsNullOrEmpty(Function.hospitalCode))
                {
                    Function.GetSetting();
                }
                return Function.hospitalCode;
            }
        }


        /// <summary>
        /// 账户
        /// </summary>
        private static string userID;

        /// <summary>
        /// 账户
        /// </summary>
        public static string UserID
        {
            get
            {
                if (string.IsNullOrEmpty(Function.userID))
                {
                    Function.GetSetting();
                }
                return Function.userID;
            }
        }


        /// <summary>
        /// 密码
        /// </summary>
        private static string passWord;

        /// <summary>
        /// 密码
        /// </summary>
        public static string PassWord
        {
            get
            {
                if (string.IsNullOrEmpty(Function.passWord))
                {
                    Function.GetSetting();
                }
                return Function.passWord;
            }
        }

        /// <summary>
        /// 代理路径
        /// </summary>
        private static string webServiceAddress = string.Empty;

        /// <summary>
        /// 代理路径
        /// </summary>
        public static string WebServiceAddress
        {
            get
            {
                if (string.IsNullOrEmpty(Function.webServiceAddress))
                {
                    Function.GetSetting();
                }
                return Function.webServiceAddress;
            }
        }
        /// <summary>
        /// 登录交易号[100]
        /// </summary>
        /// <returns></returns>
        public static string LoginTransNO
        {
            get
            {
                return "100";
            }
        }

        /// <summary>
        /// 医院口令修改交易号[101]
        /// </summary>
        public static string ChangePwTransNO
        {
            get
            {
                return "101";
            }
        }

        /// <summary>
        /// 待上传处方查询交易号[31]
        /// </summary>
        public static string NeedUploadRecipeTransNO
        {
            get
            {
                return "31";
            }
        }

        /// <summary>
        /// 已上传处方明细汇总信息查询交易号[32]
        /// </summary>
        public static string HaveUploadedRecipeTransNO
        {
            get
            {
                return "32";
            }
        }

        /// <summary>
        /// 零报处方项目上传交易号[33]
        /// </summary>
        public static string UploadRecipeTransNO
        {
            get
            {
                return "33";
            }
        }

        /// <summary>
        /// 零报处方项目回退交易号[34]
        /// </summary>
        public static string CancelUploadRecipeTransNO
        {
            get
            {
                return "34";
            }
        }

        #endregion

        #region 零星报销-入参数封装XML

        /// <summary>
        /// 医院登录[100]
        /// </summary>
        public static string LoginXML
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
	                                <!-- 0 医院编码  ckz543  varchar2(8) 不可为空 -->
	                                <ckz543>{0}</ckz543>
	                                <!-- 1 用户id  operid  varchar2(20) 不可为空 -->
	                                <operid>{1}</operid>
	                                <!-- 2 口令  password  varchar2(32) 不可为空 -->
	                                <password>{2}</password>
	                                <!-- 3 经办人类型  opertype  varchar2(3) (1普通经办人 2门诊收费员) 不可为空 -->
	                                <opertype>{3}</opertype>
	                                <!-- 4 经办人类型  clienttype  varchar2(20) (预留，传入“1”) 不可为空 -->
	                                <clienttype>{4}</clienttype>
                                </input>";

                return str;
            }
        }

        /// <summary>
        /// 医院口令修改[101]
        /// </summary>
        public static string ChangePwXML
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
                                    <!-- 0 医院编码  ckz543  varchar2(8) 不可为空 -->
                                    <ckz543>{0}</ckz543>
                                    <!-- 1 用户id  operid  varchar2(20) 不可为空 -->
                                    <operid>{1}</operid>
                                    <!-- 2 原口令  oldpwd  varchar2(32) 不可为空 -->
                                    <oldpwd>{2}</oldpwd>
                                    <!-- 3 新口令  newpwd  varchar2(32) 不可为空 -->
                                    <newpwd>{3}</newpwd>
                                </input>";
                return str;
            }
        }

        /// <summary>
        /// 待上传处方查询[31]
        /// </summary>
        public static string NeedUploadRecipeXML
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
	                                <!-- 0 医院编号  ckz543  varchar2(8) 不可为空 -->
                                    <ckz543>{0}</ckz543>
                                    <!-- 1 经办人  aae011  varchar2(20) 不可为空 (操作员id) -->
                                    <aae011>{1}</aae011>
	                                <!-- 2 医院登陆的sessionid  sessionid  varchar2(100) 不可为空 (操作员id) -->
                                    <sessionid>{2}</sessionid>
                                    <!-- 3 开始日期  aae030  date 可为空 (按社保发起时间：格式yyyy-mm-dd) -->
                                    <aae030>{3}</aae030>
                                    <!-- 4 终止日期  aae031  date 可为空 (按社保发起时间：格式yyyy-mm-dd) -->
                                    <aae031>{4}</aae031>
                                </input>";
                return str;
            }
        }

        /// <summary>
        /// 已上传处方明细汇总信息查询[32]
        /// </summary>
        public static string HaveUploadedRecipeXML
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
	                                <!-- 0 医院编号  ckz543  varchar2(8) 不可为空  -->
	                                <ckz543>{0}</ckz543>
	                                <!-- 1 经办人  aae011  varchar2(20) 不可为空 (操作员id) -->
	                                <aae011>{1}</aae011>
	                                <!-- 2 医院登陆的sessionid  sessionid  varchar2(100) 不可为空 (操作员id) -->
	                                <sessionid>{2}</sessionid>
                                    <!-- 3 证件号码  aac002  varchar2(20) 不可为空 -->
                                    <aac002>{3}</aac002>
                                    <!-- 4 医保编号  aac001  varchar2(18) 不可为空 个人编号-->
                                    <aac001>{4}</aac001>
                                    <!-- 5 发票号  akc332  varchar2(15) 可为空 (当“发票号”为空时：“开始日期”，“结束日期”为必录)-->
                                    <akc332>{5}</akc332>
                                    <!-- 6 开始日期  aae301  date 可为空 (对应上传处方入参的‘费用开始日期’格式yyyy-mm-dd) -->
                                    <aae301>{6}</aae301>
                                    <!-- 7 结束日期  aae302  date 可为空 (对应上传处方入参的‘费用开始日期’格式yyyy-mm-dd) -->
                                    <aae302>{7}</aae302>
                                </input>";
                return str;
            }
        }

        /// <summary>
        /// 零报处方项目上传-头表[33]
        /// </summary>
        public static string UploadRecipeHeadXML
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
	                                <!-- 0 医院编号  ckz543  varchar2(8) 不可为空  -->
	                                <ckz543>{0}</ckz543>
	                                <!-- 1 经办人  aae011  varchar2(20) 不可为空 (操作员id) -->
	                                <aae011>{1}</aae011>
	                                <!-- 2 医院登陆的sessionid  sessionid  varchar2(100) 不可为空 (操作员id) -->
	                                <sessionid>{2}</sessionid>
                                    <!-- 3 证件号码  aac002  varchar2(20) 不可为空 -->
                                    <aac002>{3}</aac002>
                                    <!-- 4 医保编号  aac001  varchar2(18) 不可为空 个人编号-->
                                    <aac001>{4}</aac001>
                                    <!-- 5 门诊住院标志  Akc331  varchar2(3) 不可为空 (1-门诊、2-住院)-->
                                    <Akc331>{5}</Akc331>
                                    <!-- 6 处方号  akc220  varchar2(20) 不可为空 (门诊时，则为门诊号，住院时，则为住院号) -->
                                    <akc220>{6}</akc220>
                                    <!-- 7 住院次数  akc330  number(12,0) 可为空 (如果是住院，则必录) -->
                                    <akc330>{7}</akc330>
                                    <!-- 8 发票号  akc332  varchar2(15) 不可为空  -->
                                    <akc332>{8}</akc332>
                                    <!-- 9 发票总金额  akc228  number(12,2) 不可为空 (当明细总金额=发票总金额时，表示已上传完成；明细总金额>发票总金额时，返回错误提示信息；) -->
                                    <akc228>{9}</akc228>
                                    <!-- 10 明细 -->
                                    <dataset>{10}</dataset>
                                </input>";
                return str;
            }
        }

        /// <summary>
        /// 零报处方项目上传-明细[33]
        /// </summary>
        public static string UploadRecipeDetailXML
        {
            get
            {
                string str = @"<row> 
                                 <!-- 0 项目序号  ykc610  varchar2(12) 不可为空 (明细回退时序号不变) -->
                                 <ykc610>{0}</ykc610>
                                 <!-- 1 大类代码(结算项目分类)  aka111  varchar2(3) 不可为空 (字典fldm) -->
                                 <aka111>{1}</aka111>
                                 <!-- 2 大类名称  aka112  varchar2(40) -->
                                 <aka112>{2}</aka112>
                                 <!-- 3 项目代码  akc222y  varchar2(20) 不可为空 (医院本地对应库项目编码) -->
                                 <akc222y>{3}</akc222y>
                                 <!-- 4 项目名称  akc223y  varchar2(40) 不可为空 (医院本地对应库项目名称) -->
                                 <akc223y>{4}</akc223y>
                                 <!-- 5 药监局药品编码  akc224  varchar2(30) 可为空 -->
                                 <akc224>{5}</akc224>
                                 <!-- 6 限制用药标记  akc229  varchar2(3) 不可为空 (0代表非限制性用药1代表限制性用药 相当于医保系统中医院前台对限制性用药的勾选)-->
                                 <akc229>{6}</akc229>
                                 <!-- 7 医用材料的注册证产品名称  akc230  varchar2(50) 可为空 -->
                                 <akc230>{7}</akc230>
                                 <!-- 8 医用材料的食药监注册号  akc231  varchar2(20) 可为空 -->
                                 <akc231>{8}</akc231>
                                 <!-- 9 数量  akc226  number(8,2) 不可为空 -->
                                 <akc226>{9}</akc226>
                                 <!-- 10 单价  akc225  number(12,5) 不可为空 -->
                                 <akc225>{10}</akc225>
                                 <!-- 11 费用总额  akc227  number(12,2) 不可为空 (数量*单价不等于费用总额时，以费用总额为准)-->
                                 <akc227>{11}</akc227>
                                 <!-- 12 产地  ykc611  varchar2(128) 可为空 -->
                                 <ykc611>{12}</ykc611>
                                 <!-- 13 规格型号  aka074  varchar2(50) 可为空 -->
                                 <aka074>{13}</aka074>
                                 <!-- 14 计价单位  aka067  varchar2(30) 可为空 -->
                                 <aka067>{14}</aka067>
                                 <!-- 15 剂型  aka070  varchar2(50) 可为空 -->
                                 <aka070>{15}</aka070>
                                 <!-- 16 使用情况  cke522  varchar2(20) 可为空 -->
                                 <cke522>{16}</cke522>
                                 <!-- 17 费用开始日期  aae030  date 可为空 (yyyy-mm-dd)-->
                                 <aae030>{17}</aae030>
                                 <!-- 18 费用终止日期  aae031  date 可为空 (yyyy-mm-dd)-->
                                 <aae031>{18}</aae031>
                                 <!-- 19 处方医生姓名  ykc613  varchar2(20) 可为空 -->
                                 <ykc613>{19}</ykc613>
                                 <!-- 20 科室名称  ykc011 varchar2(60)  可为空 (字典bqdm) -->
                                 <ykc011>{20}</ykc011>
                                 <!-- 21 收费时间  akc221 date  不可为空 (yyyy-mm-dd) -->
                                 <akc221>{21}</akc221>
                            </row>";
                return str;
            }
        }

        /// <summary>
        /// 零报处方项目回退[34]
        /// </summary>
        public static string CancelUploadRecipeXML
        {
            get
            {
                string str = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
                                <input>
	                                <!-- 0 医院编号  ckz543  varchar2(8) 不可为空 -->
	                                <ckz543>{0}</ckz543>
	                                <!-- 1 经办人  aae011  varchar2(20) 不可为空 (操作员id) -->
	                                <aae011>{1}</aae011>
	                                <!-- 2 医院登陆的sessionid  sessionid  varchar2(100) 不可为空 (操作员id) -->
	                                <sessionid>{2}</sessionid>
                                    <!-- 3 证件号码  aac002  varchar2(20) 不可为空 -->
                                    <aac002>{3}</aac002>
                                    <!-- 4 医保编号  aac001  varchar2(18) 不可为空 个人编号-->
                                    <aac001>{4}</aac001>
                                    <!-- 5 处方号  akc220  varchar2(20) 不可为空 (门诊时，则为门诊号，住院时，则为住院号) -->
                                    <akc220>{5}</akc220>
                                    <!-- 6 住院次数  akc330  number(12,0) 可为空 (如果是住院，则必录) -->
                                    <akc330>{6}</akc330>
                                    <!-- 7 明细序号  ykc610  varchar2(18) 可为空 (明细序号为空时回退全部处方明细记录；否则，回退对应明细序号的单条处方明细记录；) -->
                                    <ykc610>{7}</ykc610>
                                    <!-- 8 发票号  akc332  varchar2(15) 不可为空 -->
                                    <akc332>{8}</akc332>
                                </input>";
                return str;
            }
        }

        #endregion

        #region 零星报销-出参数解析XML

        /// <summary>
        /// 公共出参实体
        /// </summary>
        /// <param name="transNO"></param>
        /// <param name="resultXML"></param>
        /// <returns></returns>
        public static Model.ResultHead GetResultHead(string transNO, string resultXML)
        {
            #region 参数封装形式

            //<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
            //    <result>
            //        <code>返回值 1:执行成功；否则:失败</code>
            //        <message>返回信息</message>
            //        <output>
            //            <SESSIONID>会话ID</SESSIONID>
            //        </output>
            //    </result>

            #endregion

            Model.ResultHead resultHead = new FoShanSporadicUpload.Model.ResultHead();
            if (!string.IsNullOrEmpty(resultXML))
            {
                //节点-值
                Dictionary<string, string> dicNodeValue = new Dictionary<string, string>();
                //节点
                List<string> listNode = new List<string>();
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(resultXML);

                    //返回值 1:执行成功；否则:失败
                    listNode.Add("code");
                    listNode.Add("message");
                    listNode.Add("output");

                    string errorMsg = string.Empty; //错误信息
                    int rev = GetNodeValue(doc, listNode, "result", ref dicNodeValue, out errorMsg);
                    if (rev > 0)
                    {
                        #region 成功

                        //公共出参
                        if (dicNodeValue.ContainsKey("code"))
                        {
                            resultHead.Code = dicNodeValue["code"];
                        }
                        if (dicNodeValue.ContainsKey("message"))
                        {
                            resultHead.Message = dicNodeValue["message"];
                        }

                        //个性化output
                        if (dicNodeValue.ContainsKey("output"))
                        {
                            string outputXML = dicNodeValue["output"];

                            if (!string.IsNullOrEmpty(outputXML))
                            {
                                doc = new XmlDocument();
                                doc.LoadXml(outputXML);

                                if (transNO == "100")
                                {
                                    #region 医院登录[100]

                                    resultHead.SessionId = GetLoginOut(doc, ref errorMsg);

                                    #endregion
                                }
                                else if (transNO == "101")
                                {
                                    #region 医院口令修改[101]

                                    #endregion
                                }
                                else if (transNO == "31")
                                {
                                    #region 待上传处方查询[31]

                                    resultHead.OutPutXML = outputXML;

                                    #endregion
                                }
                                else if (transNO == "33")
                                {
                                    #region 零报处方项目上传[33]

                                    resultHead.OutPutXML = outputXML;

                                    #endregion
                                }
                                else if (transNO == "32")
                                {
                                    #region 已上传处方明细汇总信息查询[32]

                                    resultHead.OutPutXML = outputXML;

                                    #endregion
                                }
                                else if (transNO == "34")
                                {
                                    #region 零报处方项目回退[34]

                                    resultHead.OutPutXML = outputXML;

                                    #endregion
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region 失败

                        resultHead.Code = "-1";
                        resultHead.Message = errorMsg;

                        #endregion
                    }

                }
                catch (Exception ex)
                {
                    resultHead.Code = "-1";
                    resultHead.Message = ex.Message;
                }

            }

            return resultHead;
        }

        /// <summary>
        /// 医院登录的Output
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        private static string GetLoginOut(XmlDocument doc, ref string errorMsg)
        {
            int rev = -1;
            string nodeValue = string.Empty;
            //获取值
            rev = GetParamValue(doc, "output/SESSIONID", out nodeValue, out errorMsg);
            if (rev <= 0)
            {
                errorMsg = "无节点【output/SESSIONID】!请检查返回参数!";
                return "";
            }
            return nodeValue;
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 获取节点值
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="listNode"></param>
        /// <param name="root"></param>
        /// <param name="dicNodeValue"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static int GetNodeValue(XmlDocument doc, List<string> listNode, string root, ref Dictionary<string, string> dicNodeValue, out string errorMsg)
        {
            errorMsg = string.Empty;
            //无元素值
            if (listNode.Count <= 0)
            {
                return 1;
            }
            int rev = -1;
            string nodeValue = string.Empty;
            foreach (string key in listNode)
            {
                //获取值
                rev = GetParamValue(doc, root + "/" + key, out nodeValue, out errorMsg);
                if (rev <= 0)
                {
                    errorMsg = "无节点【" + key + "】!请检查输入参数!";
                    return rev;
                }

                //添加到Dictionary中
                if (!dicNodeValue.ContainsKey(key))
                {
                    dicNodeValue.Add(key, nodeValue);
                }
            }

            return rev;
        }

        /// <summary>
        /// 获取节点值
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="strNode"></param>
        /// <param name="Value"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static int GetParamValue(XmlDocument doc, string strNode, out string Value, out string errorMsg)
        {
            Value = string.Empty;
            errorMsg = string.Empty;
            if (doc == null || string.IsNullOrEmpty(strNode))
            {
                errorMsg = "内部参数调用异常！";
                return -1;
            }

            XmlNode node = doc.SelectSingleNode(strNode);
            if (node == null)
            {
                errorMsg = "获取参数异常,不存在 【" + strNode + "】节点。";
                return -1;
            }

            if (node.HasChildNodes)
            {
                if (node.ChildNodes[0].HasChildNodes)
                {
                    Value = node.OuterXml.Trim();
                }
                else
                {
                    Value = node.InnerText.Trim();
                }
            }
            else
            {
                Value = node.InnerText.Trim();
            }

            return 1;
        }

        /// <summary>
        /// 1-门诊、2-住院
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static string GetServiceName(string serviceType)
        {
            string serviceName = string.Empty;
            switch (serviceType)
            {
                case "1":
                    serviceName = "门诊";
                    break;
                case "2":
                    serviceName = "住院";
                    break;
            }

            return serviceName ;
        }

        /// <summary>
        /// 读取社保配置文件
        /// </summary>
        private static void GetSetting()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(profileName);

            XmlNode node = doc.SelectSingleNode(@"/SIConfig/hospitalCode");
            Function.hospitalCode = node.InnerText.Trim();

            node = doc.SelectSingleNode(@"/SIConfig/webServiceAddress");
            Function.webServiceAddress = node.InnerText.Trim();


            node = doc.SelectSingleNode(@"/SIConfig/userID");
            Function.userID = node.InnerText.Trim();

            node = doc.SelectSingleNode(@"/SIConfig/passWord");
            Function.passWord = node.InnerText.Trim();
        }
        #endregion
    }
}
