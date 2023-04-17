using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;
using System.Runtime.InteropServices;

namespace FS.SOC.Local.CallQueue.IBORN.INurseAssign
{
    public class NurseAssign : FS.SOC.HISFC.CallQueue.Interface.INurseAssign
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public NurseAssign()
        {

            #region 初始化

            try
            {
                //判断是否不存在着路径配置文件
                string strXMLPath = ".\\Setting\\OutDoctScreen.xml";
                if (!System.IO.File.Exists(strXMLPath))
                {
                    return;
                }

                //判断节点是否存在
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(strXMLPath);
                System.Xml.XmlNode node = doc.SelectSingleNode("Config/Enable");
                if (node == null)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(node.InnerText))
                {
                    this.isEnableScreen = FS.FrameWork.Function.NConvert.ToBoolean(node.InnerText);
                }

                //{8225C046-D7AE-4228-9BFE-1D933C731A04}
                System.Xml.XmlNode node1 = doc.SelectSingleNode("Config/Region");
                if (node1 == null)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(node1.InnerText))
                {
                    try
                    {
                        this.hospitalRegion = FS.FrameWork.Function.NConvert.ToInt32(node1.InnerText);
                    }
                    catch
                    {
                        this.hospitalRegion = 1;
                    }
                }

                //{8225C046-D7AE-4228-9BFE-1D933C731A04}
                System.Xml.XmlNode node2 = doc.SelectSingleNode("Config/Room");
                if (node2 == null)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(node2.InnerText))
                {
                    try
                    {
                        this.RoomName = node2.InnerText.ToString();
                    }
                    catch
                    {
                        this.RoomName = "";
                    }
                }
            }
            catch (Exception ex)
            {
                //{8225C046-D7AE-4228-9BFE-1D933C731A04}

                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "启用排队叫号失败:" + ex.Message, System.Windows.Forms.ToolTipIcon.Info);

                this.isEnableScreen = false;
                this.hospitalRegion = 1;
                this.RoomName = "";
            }

            #endregion
        }

        /// <summary>
        /// 是否启用外屏
        /// </summary>
        private bool isEnableScreen = false;


        //{8225C046-D7AE-4228-9BFE-1D933C731A04}
        /// <summary>
        /// 院区 1-爱博恩，2-顺德
        /// </summary>
        private int hospitalRegion = 1;

        /// <summary>
        /// 诊室名
        /// </summary>
        private string RoomName = "";

        FS.SOC.HISFC.CallQueue.BizLogic.NurseAssign nurseAssignMgr = new FS.SOC.HISFC.CallQueue.BizLogic.NurseAssign();
        FS.SOC.HISFC.Assign.BizLogic.Assign assignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();

        FS.HISFC.BizLogic.Nurse.Assign assignManager = new FS.HISFC.BizLogic.Nurse.Assign();

        #region INurseAssign 成员

        /// <summary>
        /// 叫号（根据护士站进行叫号）
        /// 获取所有需要叫号的申请信息
        /// </summary>
        public void Call(string nurseCode, string noonID)
        {
            //判断是否启用门诊医生站外屏
            if (!this.isEnableScreen)
            {
                return;
            }

            //只叫号本护士站的，当前午别的病人
            FS.SOC.HISFC.CallQueue.BizProcess.NurseAssign.CreateInstance().CallAssign(nurseCode, CommonController.CreateInstance().GetNoon(noonID));
        }


        /// <summary>
        /// 插入叫号申请信息
        /// </summary>
        /// <param name="register"></param>
        /// <param name="dept"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Registration.Register register, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
        {
            //判断是否启用门诊医生站外屏
            if (!this.isEnableScreen)
            {
                return 1;
            }

            if (register == null || string.IsNullOrEmpty(register.ID))
            {
                return 1;
            }

            DateTime dtNow = nurseAssignMgr.GetDateTimeFromSysDateTime();
            if (noon == null || string.IsNullOrEmpty(noon.ID))
            {
                noon = new FS.HISFC.Models.Base.Noon();
                noon.ID = CommonController.CreateInstance().GetNoonID(dtNow);//午别
            }
            string roomID = "";
            if (room is FS.HISFC.Models.Nurse.Seat)
            {
                roomID = ((FS.HISFC.Models.Nurse.Seat)room).PRoom.ID;
            }
            else
            {
                roomID = room.ID;
            }
            string errinfo = string.Empty;

            //选择患者叫号
            {
                //整合NurseAssign实体
                FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign = new FS.SOC.HISFC.CallQueue.Models.NurseAssign();
                nurseAssign.PatientID = register.ID;
                nurseAssign.PatientSeeNO = register.DoctorInfo.SeeNO.ToString();
                nurseAssign.PatientName = register.Name;
                nurseAssign.PatientCardNO = register.PID.CardNO;
                nurseAssign.PatientSex = register.Sex.ID.ToString();
                nurseAssign.Room.ID = roomID;
                nurseAssign.Room.Name = room.Name;
                nurseAssign.Dept.ID = dept.ID;
                nurseAssign.Dept.Name = dept.Name;
                nurseAssign.Nurse.ID = nurse.ID;
                nurseAssign.Nurse.Name = nurse.Name;
                nurseAssign.Console.ID = console.ID;
                nurseAssign.Console.Name = console.Name;
                nurseAssign.Noon.ID = noon.ID;
                nurseAssign.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;

                ////插入叫号申请
                //int i = nurseAssignMgr.Insert(nurseAssign);
                //if (i <= 0)
                //{
                //    err = errinfo;
                //    return -1;
                //}

                //{8225C046-D7AE-4228-9BFE-1D933C731A04}
                if (this.hospitalRegion == 1)
                {
                    //{33E1E503-7C43-452b-AABF-FAE7D481B298}
                    // Queue_CallNext(register.ID, register.Name, register.DoctorInfo.SeeNO, "", "", 0);
                    string resultCode = "";
                    string resultDesc = "";
                    //{E12342D9-42FE-4169-99F8-A54BD713D581}
                    if (WSHelper.Call(register.ID, register.PID.CardNO, register.Name, this.RoomName, out resultCode, out resultDesc) > 0)
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "患者【" + register.Name + "】叫号成功！", System.Windows.Forms.ToolTipIcon.Info);
                    }
                    else
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "患者【" + register.Name + "】叫号失败！", System.Windows.Forms.ToolTipIcon.Info);
                    }

                }
                else if (this.hospitalRegion == 2)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("DoctorID", FS.FrameWork.Management.Connection.Operator.ID.ToString());
                    ht.Add("DoctorName", ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name);
                    ht.Add("ClinicName", RoomName);
                    ht.Add("PatientID", register.ID);
                    ht.Add("PatientName", register.Name);
                    ht.Add("IPaddress", RoomName);
                    ht.Add("Jiaohao", "叫号");
                    bool success = this.PostWebServiceByJson("http://10.20.20.251:81/WebServiceTerminalCall.asmx", "Call", ht, false);
                    if (success)
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "患者【" + register.Name + "】叫号成功！", System.Windows.Forms.ToolTipIcon.Info);
                    }
                    else
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "患者【" + register.Name + "】叫号失败！", System.Windows.Forms.ToolTipIcon.Info);
                    }
                }

                return 1;
            }
        }

        /// <summary>
        /// 延迟就诊
        /// </summary>
        /// <param name="register"></param>
        /// <param name="dept"></param>
        /// <param name="nurse"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="noon"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DelayCall(FS.HISFC.Models.Registration.Register register, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
        {
            //判断是否启用门诊医生站外屏
            if (!this.isEnableScreen)
            {
                return 1;
            }

            if (register == null || string.IsNullOrEmpty(register.ID))
            {
                return 1;
            }

            //{8225C046-D7AE-4228-9BFE-1D933C731A04}
            if (this.hospitalRegion == 1)
            {
                //return Queue_NoCall(register.ID, register.Name, register.DoctorInfo.SeeNO);
                //{33E1E503-7C43-452b-AABF-FAE7D481B298}
                string resultCode = "";
                string resultDesc = "";
                if (WSHelper.DelayCall(register.ID, out resultCode, out resultDesc) < 0)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "更新患者【" + register.Name + "】会员系统的过号信息失败！", System.Windows.Forms.ToolTipIcon.Info);
                }

                return 1;
            }
            else if (this.hospitalRegion == 2)
            {
                return 1;
            }
            else
            { 
                return 1;
            }
        }

        /// <summary>
        /// 结束就诊
        /// </summary>
        /// <param name="register"></param>
        /// <param name="dept"></param>
        /// <param name="nurse"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="noon"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int DiagOut(FS.HISFC.Models.Registration.Register register, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
        {
            //判断是否启用门诊医生站外屏
            if (!this.isEnableScreen)
            {
                return 1;
            }

            if (register == null || string.IsNullOrEmpty(register.ID))
            {
                return 1;
            }

            //{8225C046-D7AE-4228-9BFE-1D933C731A04}
            if (this.hospitalRegion == 1)
            {
                //return Queue_CallEndcall(register.ID);
                //{33E1E503-7C43-452b-AABF-FAE7D481B298}
                string resultCode = "";
                string resultDesc = "";
                if (WSHelper.DiagOut(register.ID, out resultCode, out resultDesc) < 0)
                { 
                    FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "更新患者【" + register.Name + "】会员系统的排队信息失败！", System.Windows.Forms.ToolTipIcon.Info); 
                }

                return 1;
            }
            else if (this.hospitalRegion == 2)
            {
                return 1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="doct"></param>
        /// <param name="dept"></param>
        /// <param name="nurse"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="noon"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int Init(FS.FrameWork.Models.NeuObject doct, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
        {
            //判断是否启用门诊医生站外屏
            if (!this.isEnableScreen)
            {
                return 1;
            }
            //{8225C046-D7AE-4228-9BFE-1D933C731A04}
            if (this.hospitalRegion == 1)
            {
                return Queue_Login(doct.ID);
            }
            else if (this.hospitalRegion == 2)
            {
                try
                {
                    //{390EA9BE-1A9C-43da-B26B-08533FC00415}
                    Hashtable ht = new Hashtable();
                    ht.Add("DoctorID", FrameWork.Management.Connection.Operator.ID.ToString());
                    ht.Add("ClinicName", this.RoomName);
                    ht.Add("type", "0");
                    bool success = this.PostWebServiceByJson("http://10.20.20.251:81/HQSWcfServicePush.asmx", "updateD2C", ht, true);

                    if (!success)
                    {
                        FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "诊室屏幕初始化失败！", System.Windows.Forms.ToolTipIcon.Info);
                    }

                    return 1;
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="doct"></param>
        /// <param name="dept"></param>
        /// <param name="nurse"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="noon"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int Close(FS.FrameWork.Models.NeuObject doct, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err)
        {
            //判断是否启用门诊医生站外屏
            if (!this.isEnableScreen)
            {
                return 1;
            }
            //{8225C046-D7AE-4228-9BFE-1D933C731A04}
            if (this.hospitalRegion == 1)
            {
                return Queue_Logout();
            }
            else if (this.hospitalRegion == 2)
            {
                try
                {
                    //{390EA9BE-1A9C-43da-B26B-08533FC00415}
                    Hashtable ht = new Hashtable();
                    ht.Add("DoctorID", FrameWork.Management.Connection.Operator.ID.ToString());
                    ht.Add("ClinicName", this.RoomName);
                    ht.Add("type", "1");
                    this.PostWebServiceByJson("http://10.20.20.251:81/HQSWcfServicePush.asmx", "updateD2C", ht,true);
                    return 1;
                }
                catch (Exception ex)
                {
                    return -1;
                }

            }
            else
            {
                return 1;
            }
        }

        //{DF69D955-220E-462f-BB65-04EB39BB0AF5}
        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="clinic_code"></param>
        /// <returns></returns>
        public int CancelCall(string clinic_code)
        {
            //{8225C046-D7AE-4228-9BFE-1D933C731A04}
            if (this.hospitalRegion == 1)
            {
                return Queue_Cancel(clinic_code);
            }
            else if (this.hospitalRegion == 2)
            {
                return 1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 重新叫号
        /// </summary>
        /// <param name="clinic_code"></param>
        /// <returns></returns>
        public int ReCall(string clinic_code)
        {
            //return Queue_Update(clinic_code);
            //判断是否启用门诊医生站外屏
            if (!this.isEnableScreen)
            {
                return 1;
            }

            if (string.IsNullOrEmpty(clinic_code))
            {
                return 1;
            }

            //{8225C046-D7AE-4228-9BFE-1D933C731A04}
            if (this.hospitalRegion == 1)
            {
                //return Queue_CallEndcall(register.ID);
                //{33E1E503-7C43-452b-AABF-FAE7D481B298}
                string resultCode = "";
                string resultDesc = "";
                if (WSHelper.CancelDiagOut(clinic_code, out resultCode, out resultDesc) < 0)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "更新患者会员系统的排队信息失败！", System.Windows.Forms.ToolTipIcon.Info);
                }

                return 1;
            }
            else if (this.hospitalRegion == 2)
            {
                return 1;
            }
            else
            {
                return 1;
            }
        }

        #endregion


        #region DLL嵌套调用

        /// <summary>
        /// 登录成功，如果诊室屏是开启的，将显示该诊室对应的医生个人信息介绍，具体的信息在后台管理里的医生管理进行
        /// </summary>
        /// <param name="DOCTOR_ID"></param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Login", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_Login(String DOCTOR_ID);

        /// <summary>
        /// 成功会清除当前对应的诊室屏的医生相关个人以及排队信息
        /// </summary>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Logout", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_Logout();

        /// <summary>
        /// 医生呼叫功能
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <param name="CURR_NAME">就诊人员姓名</param>
        /// <param name="QUEUE_NUM">就诊序号</param>
        /// <param name="NEXT_ID"></param>
        /// <param name="NEXT_NAME"></param>
        /// <param name="NEXT_QUEUE_NUM"></param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_CallNext", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_CallNext(String CURR_ID, String CURR_NAME, int QUEUE_NUM, String NEXT_ID, String NEXT_NAME, int NEXT_QUEUE_NUM);

        /// <summary>
        /// 医生结诊功能
        /// </summary>
        /// <param name="CURR_ID">Int Queue_CallEndcall(String CURR_ID)</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_CallEndcall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_CallEndcall(String CURR_ID);

        /// <summary>
        /// 过号呼叫标识
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <param name="CURR_NAME">就诊人员姓名</param>
        /// <param name="QUEUE_NUM">就诊序号</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_NoCall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_NoCall(String CURR_ID, String CURR_NAME, int QUEUE_NUM);

        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Cancel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_Cancel(String CURR_ID);

        /// <summary>
        /// 重新待诊
        /// </summary>
        /// <param name="CURR_ID">就诊ID---这个对应V_CALL_REGISTER里的ID字段</param>
        /// <returns></returns>
        [DllImport("Queue.dll", EntryPoint = "Queue_Update", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern int Queue_Update(String CURR_ID);

        #endregion

        #region 聚点排队叫号webservice
        /// <summary>
        /// 需要WebService支持Post调用
        /// </summary>
        public bool PostWebServiceByJson(String URL, String MethodName, Hashtable Pars,bool IsLoginOrOut)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/soap+xml; charset=utf-8";

            // 凭证
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //超时时间
            request.Timeout = 10000;
            byte[] data = HashtableToSoap12(Pars, "http://tempuri.org/", MethodName, IsLoginOrOut);
            request.ContentLength = data.Length;
            System.IO.Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
            var response = request.GetResponse();
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            String retXml = sr.ReadToEnd();
            sr.Close();
            doc.LoadXml(retXml);
            System.Xml.XmlNamespaceManager mgr = new System.Xml.XmlNamespaceManager(doc.NameTable);
            mgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
            String xmlStr = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;

            if (xmlStr == "Call Success")
            {
                return true;
            }

            return false;
        }

        private static byte[] HashtableToSoap12(Hashtable ht, String XmlNs, String MethodName,bool isLoginOrOut)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml("<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\"></soap12:Envelope>");
            System.Xml.XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);
            System.Xml.XmlElement soapBody = doc.CreateElement("soap12", "Body", "http://www.w3.org/2003/05/soap-envelope");

            System.Xml.XmlElement soapMethod = doc.CreateElement(MethodName);
            soapMethod.SetAttribute("xmlns", XmlNs);
            System.Xml.XmlElement soapPar = doc.CreateElement("Call_PatientInfo");
            soapPar.InnerXml = ObjectToSoapXml(HashtableToJson(ht, 0, isLoginOrOut));
            soapMethod.AppendChild(soapPar);
            soapBody.AppendChild(soapMethod);
            doc.DocumentElement.AppendChild(soapBody);
            return Encoding.UTF8.GetBytes(doc.OuterXml);
        }

        private static string ObjectToSoapXml(object o)
        {
            System.Xml.Serialization.XmlSerializer mySerializer = new System.Xml.Serialization.XmlSerializer(o.GetType());
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            mySerializer.Serialize(ms, o);
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));
            if (doc.DocumentElement != null)
            {
                return doc.DocumentElement.InnerXml;
            }
            else
            {
                return o.ToString();
            }
        }

        public static string HashtableToJson(Hashtable hr, int readcount, bool isLoginOrOut)
        {
            string json = string.Empty;
            if (isLoginOrOut)
            {
                json = "<Request>";
            }
            else
            {
                json = "<patient_info>";
            }

            foreach (DictionaryEntry row in hr)
            {
                try
                {
                    string keyStart = "<" + row.Key + ">";
                    string keyEnd = "</" + row.Key + ">";
                    if (row.Value is Hashtable)
                    {
                        Hashtable t = (Hashtable)row.Value;
                        if (t.Count > 0)
                        {
                            json += keyStart + HashtableToJson(t, readcount++, isLoginOrOut) + keyEnd + ",";
                        }
                        else { json += keyStart + "{}," + keyEnd; }
                    }
                    else
                    {
                        string value = "" + row.Value.ToString() + "";
                        json += keyStart + value + keyEnd;
                    }
                }
                catch { }
            }
            if (isLoginOrOut)
            {
                json = json + "</Request>";
            }
            else
            {
                json = json + "</patient_info>";
            }
            return json;
        }

        #endregion 
    }
}
