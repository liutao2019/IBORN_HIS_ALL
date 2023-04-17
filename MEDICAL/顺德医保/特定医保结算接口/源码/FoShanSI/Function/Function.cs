using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;

namespace FoShanSI.Function
{
    /// <summary>
    /// 功能函数
    /// 张琦 2010-7
    /// </summary>
    public class Function
    {
        /// <summary>
        /// 医保数据库连接设置
        /// </summary>
        protected static string profileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Plugins\SI\FoShanSiDataBase.xml";//医保数据库连接设置;

        /// <summary>
        /// 日志类
        /// </summary>
        protected static FS.FrameWork.Models.NeuLog log = new FS.FrameWork.Models.NeuLog();//日志类

        /// <summary>
        /// 医院代码--南方医科大学北滘医院
        /// 提出来到配置文件比较号，暂不管了
        /// </summary>
        private static string hospitalCode = string.Empty;

        /// <summary>
        /// 业务单据号填充规则
        /// </summary>
        //private string fillBillNoRule = "";

        public static string HospitalCode
        {
            get
            {
                if (string.IsNullOrEmpty(hospitalCode))
                {
                    FS.HISFC.BizProcess.Integrate.Common.ControlParam ctlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                    hospitalCode = ctlParam.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.HosCode);
                }
                return hospitalCode;
            }
        }

        /// <summary>
        /// 门诊费用结算表
        /// </summary>
        static Hashtable hsOutPatientFeeItemTable = new Hashtable();

        /// <summary>
        /// 门诊费用结算表
        /// </summary>
        public static Hashtable HsOutPatientFeeItemTable
        {
            get 
            {
                //N为Name显示列；%为自付明细显示项;T医疗费用总额;P统筹支付金额
                if (hsOutPatientFeeItemTable.Count == 0)
                {
                    hsOutPatientFeeItemTable.Add(0, "结算业务号");
                    hsOutPatientFeeItemTable.Add(1, "医院代码");
                    hsOutPatientFeeItemTable.Add(2, "N门诊标识号");
                    hsOutPatientFeeItemTable.Add(3, "医院辅助标识号");
                    hsOutPatientFeeItemTable.Add(4, "%超标床位费");
                    hsOutPatientFeeItemTable.Add(5, "%自费药品费");
                    hsOutPatientFeeItemTable.Add(6, "%乙类药品费");
                    hsOutPatientFeeItemTable.Add(7, "%中成药品费");
                    hsOutPatientFeeItemTable.Add(8, "%中草药费");
                    hsOutPatientFeeItemTable.Add(9, "%高新仪器、治疗费");
                    hsOutPatientFeeItemTable.Add(10, "%其他费用");
                    hsOutPatientFeeItemTable.Add(11, "%离休不纳入抢救费用");
                    hsOutPatientFeeItemTable.Add(12, "%离休不纳入非抢救费用");
                    hsOutPatientFeeItemTable.Add(13, "%离休进口材料");
                    hsOutPatientFeeItemTable.Add(14, "T医疗费用总额");
                    hsOutPatientFeeItemTable.Add(15, "P统筹支付金额");
                    hsOutPatientFeeItemTable.Add(16, "预留项目金额1");
                    hsOutPatientFeeItemTable.Add(17, "预留项目金额2");   //民政低保救助金额的负值，例如民政低保救助金额100元，则这里保存-100【只是另外说明，已经包含在第15列】
                    hsOutPatientFeeItemTable.Add(18, "预留项目金额3");
                    hsOutPatientFeeItemTable.Add(19, "预留项目金额4");
                    hsOutPatientFeeItemTable.Add(20, "预留项目金额5");
                    hsOutPatientFeeItemTable.Add(21, "处理状态");
                    hsOutPatientFeeItemTable.Add(22, "处理日期");
                    hsOutPatientFeeItemTable.Add(23, "USYS系统字段");
                }
                return Function.hsOutPatientFeeItemTable; 
            }
        }

        /// <summary>
        /// 住院费用结算表
        /// </summary>
        static Hashtable hsInPatientFeeItemTable = new Hashtable();

        /// <summary>
        /// 住院费用结算表
        /// </summary>
        public static Hashtable HsInPatientFeeItemTable
        {
            get 
            {
                if (hsInPatientFeeItemTable.Count == 0)
                {
                    hsInPatientFeeItemTable.Add(0, "结算业务号");
                    hsInPatientFeeItemTable.Add(1, "医院代码");
                    hsInPatientFeeItemTable.Add(2, "医院住院号");
                    hsInPatientFeeItemTable.Add(3, "医院住院次数");
                    hsInPatientFeeItemTable.Add(4, "%超标床位费");
                    hsInPatientFeeItemTable.Add(5, "%自费药品费");
                    hsInPatientFeeItemTable.Add(6, "%乙类药品费");
                    hsInPatientFeeItemTable.Add(7, "%中成药品费");
                    hsInPatientFeeItemTable.Add(8, "%中草药费");
                    hsInPatientFeeItemTable.Add(9, "%高新仪器、治疗费");
                    hsInPatientFeeItemTable.Add(10, "%其他费用");
                    hsInPatientFeeItemTable.Add(11, "%起付标准费");
                    hsInPatientFeeItemTable.Add(12, "年工资2倍段负担费用");
                    hsInPatientFeeItemTable.Add(13, "年工资3倍段负担费用");
                    hsInPatientFeeItemTable.Add(14, "年工资3倍以上负担费用");
                    hsInPatientFeeItemTable.Add(15, "超最高限额费用");
                    hsInPatientFeeItemTable.Add(16, "%离休不纳入抢救费用");
                    hsInPatientFeeItemTable.Add(17, "%离休不纳入非抢救费用");
                    hsInPatientFeeItemTable.Add(18, "%离休进口材料");
                    hsInPatientFeeItemTable.Add(19, "按比例个人自付费用");
                    hsInPatientFeeItemTable.Add(20, "T医疗费用总额");
                    hsInPatientFeeItemTable.Add(21, "P基本医疗统筹支付");
                    hsInPatientFeeItemTable.Add(22, "P补充医疗统筹支付");
                    hsInPatientFeeItemTable.Add(23, "P公务员统筹支付");
                    hsInPatientFeeItemTable.Add(24, "P离休基金支付");
                    hsInPatientFeeItemTable.Add(25, "P工伤基金支付");
                    hsInPatientFeeItemTable.Add(26, "P生育基金支付");
                    hsInPatientFeeItemTable.Add(27, "预留项目金额4");
                    hsInPatientFeeItemTable.Add(28, "预留项目金额5");
                    hsInPatientFeeItemTable.Add(29, "P预留项目金额6");   //所有民政支付金额=民政优抚补助金额+民政低保救助金额+民政低保二次救助金额
                    hsInPatientFeeItemTable.Add(30, "预留项目金额7");
                    hsInPatientFeeItemTable.Add(31, "预留项目金额8");
                    hsInPatientFeeItemTable.Add(32, "处理状态");
                    hsInPatientFeeItemTable.Add(33, "处理日期");
                    hsInPatientFeeItemTable.Add(34, "USYS系统字段");
                }
                return Function.hsInPatientFeeItemTable;
            }
        }

        static FS.FrameWork.Public.ObjectHelper balanceHelper = new FS.FrameWork.Public.ObjectHelper();

        public static FS.FrameWork.Public.ObjectHelper BalanceHelper
        {
            get 
            {
                ArrayList balanceList = new ArrayList();
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "0","结算业务号",""));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "1", "医院代码", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "2", "医院住院号", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "3", "医院住院次数", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "4", "超标床位费", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "5", "自费药品费", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "6", "乙类药品费", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "7", "中成药品费", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "8", "中草药费", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "9", "高新仪器、治疗费", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "10", "其他费用", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "11", "起付标准费", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "12", "年工资2倍段负担费用", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "13", "年工资3倍段负担费用", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "14", "年工资3倍以上负担费用", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "15", "超最高限额费用", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "16", "离休不纳入抢救费用", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "17", "离休不纳入非抢救费用", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "18", "离休进口材料", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "19", "按比例个人自付费用", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "20", "医疗费用总额", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "21", "基本医疗统筹支付", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "22", "补充医疗统筹支付", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "23", "公务员统筹支付", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "24", "离休基金支付", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "25", "工伤基金支付", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "26", "生育基金支付", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "27", "预留项目金额4", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "28", "预留项目金额5", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "29", "预留项目金额6", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "30", "预留项目金额7", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "31", "预留项目金额8", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "32", "处理状态", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "33", "处理日期", "" ));
                balanceList.Add(new FS.FrameWork.Models.NeuObject ( "34", "USYS系统字段", "" ));
                balanceHelper.ArrayObject = balanceList;
                return Function.balanceHelper; 
            }
        }


        /// <summary>
        /// 错误消息返回码
        /// </summary>
        public static string errorText = "error";

        #region 日志管理

        /// <summary>
        /// 写入错误信息
        /// </summary>
        public static void WriteErr(string errText)
        {
            log.WriteLog("Error:" + errText);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logString"></param>
        [System.Diagnostics.Conditional("DEBUG")]
        public static void WriteLog(string logString,string logObject)
        {
            log.WriteLog("Log: " + logObject + " ： " + logString);
        }

        #endregion

        #region 读取配置函数

        /// <summary>
        /// 获得连接串
        /// </summary>
        /// <returns></returns>
        public static string GetConnectString()
        {
            string dbInstance = "";
            string dataBaseName = "";
            string userName = "";
            string passWord = "";
            string connString = "";

            if (!CreatXmlDoc())
            {
                log.WriteLog("创建连接医保配置文件失败!");
                return "";
            }
            XmlNode profileNode = GetUsedProfileNode();
            try
            {
                dbInstance = profileNode.Attributes["数据库实例名"].Value.ToString();
                dataBaseName = profileNode.Attributes["数据库名"].Value.ToString();
                userName = profileNode.Attributes["用户名"].Value.ToString();
                passWord = profileNode.Attributes["密码"].Value.ToString();
            }
            catch
            {
                log.WriteLog("获取连接字符串出错!");
                return "";
            }

            //             //Driver={IBM DB2 ODBC 
            //DRIVER};Server=localhost;DSN=TESTDB;UID=username;PWD=pwd;Protocol=TCPIP");

            //driver={IBM   DB2   ODBC   DRIVER};Database=db;hostname=computername;port=50000;protocol=TCPIP;uid=db2admin;pwd=pwd

            //connString = "server=" + dbInstance + ";database=" + dataBaseName + ";uid=" + userName + ";pwd=" + passWord;
            connString = "DSN=" + dataBaseName + ";uid=" + userName + ";pwd=" + passWord;
            return connString;
        }

        /// <summary>
        /// 为数据库配置创建xml文件 成功创建或文件已存在true
        /// </summary>
        /// <returns></returns>
        public static bool CreatXmlDoc()
        {
            if (!System.IO.File.Exists(profileName))
            {
                FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
                XmlDocument doc = new XmlDocument();
                XmlElement root;
                root = myXml.CreateRootElement(doc, "DB2ConnectToFoShanSI", "1.0");
                XmlElement dbName = myXml.AddXmlNode(doc, root, "设置", "");

                myXml.AddNodeAttibute(dbName, "数据库实例名", "");
                myXml.AddNodeAttibute(dbName, "数据库名", "");
                myXml.AddNodeAttibute(dbName, "用户名", "");
                myXml.AddNodeAttibute(dbName, "密码", "");

                try
                {
                    StreamWriter sr = new StreamWriter(profileName, false, System.Text.Encoding.Default);
                    string cleandown = doc.OuterXml;
                    sr.Write(cleandown);
                    sr.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("无法保存！" + ex.Message);
                    return false;
                }
                return true;
            }
            return true;
        }

        /// <summary>
        /// 获取XML
        /// </summary>
        /// <param name="profileName"></param>
        /// <returns></returns>
        public static XmlDocument GetXmlDoc()
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                StreamReader sr = new StreamReader(profileName, System.Text.Encoding.Default);
                string cleanDown = sr.ReadToEnd();
                doc.LoadXml(cleanDown);
                sr.Close();
                return doc;
            }
            catch
            {
                log.WriteLog("获取xml配置文件失败 SIDatabase.cs-->creatXmlDoc");
                return null;
            }
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        public static ArrayList GetArrayListFromXmlNodes(XmlNodeList nodeList)
        {
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            for (int i = 0; i < nodeList.Count; i++)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj.Name = nodeList.Item(i).Name;
                obj.Memo = nodeList.Item(i).Attributes["当前使用"].Value.ToString();
                obj.ID = i.ToString();              //节点的下标

                al.Add(obj);
            }
            return al;
        }

        /// <summary>
        ///如果profileName的配置文件存在 获取数据库当前设置的结点
        /// </summary>
        /// <returns></returns>
        public static XmlNode GetUsedProfileNode()
        {
            XmlDocument xmlDoc = GetXmlDoc();
            XmlNode rootNode = xmlDoc.SelectSingleNode("//设置");
            ArrayList al = GetArrayListFromXmlNodes(rootNode.ChildNodes);
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.Memo == "1")         //节点的["当前使用"]属性
                {
                    int usedNodeIndex = FS.FrameWork.Function.NConvert.ToInt32(obj.ID);
                    return rootNode.ChildNodes.Item(usedNodeIndex);
                }
            }
            return null;
        }

        /// <summary>
        /// 保存XML
        /// </summary>
        public static bool SaveXmlDoc(int changedIndex,FS.FrameWork.Models.NeuObject objSave,ref string errText)
        {
            XmlDocument xmlDoc = GetXmlDoc();

            XmlNode rootNode = xmlDoc.SelectSingleNode("//设置");
            ArrayList alPact = GetArrayListFromXmlNodes(rootNode.ChildNodes);
            //将节点的["当前使用"]属性都设为0
            foreach (FS.FrameWork.Models.NeuObject obj in alPact)
            {
                int index = FS.FrameWork.Function.NConvert.ToInt32(obj.ID);
                XmlNode node = rootNode.ChildNodes.Item(index);
                node.Attributes["当前使用"].Value = "0";
            }

            //修改当前操作的节点属性 
            XmlNode selectnode = rootNode.ChildNodes.Item(changedIndex);

            if (selectnode == null)
            {
                errText="获得节点错误 " + selectnode.Name;
                return false;
            }
            try
            {

                selectnode.Attributes["数据库实例名"].Value = objSave.ID;
                selectnode.Attributes["数据库名"].Value = objSave.Name;
                selectnode.Attributes["用户名"].Value = objSave.Memo;
                selectnode.Attributes["密码"].Value = objSave.User01;
                selectnode.Attributes["当前使用"].Value = "1";
            }
            catch(Exception ex) {
                errText = ex.Message;
                return false; }

            xmlDoc.Save(profileName);
            return true;
        }
        #endregion

        #region 拆分字符串

        public static string[] splitStr(string orgStr, char splitChar)
        {
            try
            {
                if (string.IsNullOrEmpty(orgStr))
                    return null;
                return orgStr.Split(splitChar);
            }
            catch { return null; }
        }

        #endregion

        #region 列索引转置列名

        public static string GetColumnNameByIndex(Hashtable hsTable,int index)
        {
            if (hsTable != null)
            {
                return hsTable[index] as string;
            }
            else
            {
                return errorText;
            }
        }

        #endregion

        #region 补充单引号

        public static string AddMark(string orgStr, string markStr,FillWay fillWay)
        {
            string markedStr = string.Empty;
            switch (fillWay)
            {
                case FillWay.BEFORE:
                    markedStr= markStr + orgStr;
                    break;
                case FillWay.AFTER:
                    markedStr= orgStr + markStr;
                    break;
                case FillWay.BOTH:
                    markedStr= markStr + orgStr + markStr+",";
                    break;
            }
            return markedStr;
        }

        #endregion

        #region 填充业务单据号 防止重复需要时再调用


        public static string FillUpLoadBillNo(string billNo, string fillBillNoRule)
        {
            return billNo + fillBillNoRule;//暂时不处理用门诊流水号
        }

        #endregion

        #region 复制到操作系统粘贴板

        public static void Copy(string contents)
        {
            try
            {
                System.Windows.Forms.Clipboard.Clear();
                System.Windows.Forms.Clipboard.SetDataObject(contents);
            }
            catch { }
        }

        #endregion

    }

    /// <summary>
    /// 填充方式
    /// </summary>
    public enum FillWay
    {
        BEFORE=0,//前面填充
        AFTER=1,//后面填充
        BOTH//前后填充
    }

    /// <summary>
    /// 患者类型
    /// </summary>
    public enum patientType
    {
        OUTPATIENT=0,//门诊患者
        INPATIENT=1,//住院患者
    }
}
