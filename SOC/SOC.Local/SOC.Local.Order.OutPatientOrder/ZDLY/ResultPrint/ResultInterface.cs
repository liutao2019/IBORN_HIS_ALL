using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace FS.SOC.Local.Order.OutPatientOrder.ZDLY
{
    class ResultInterface : FS.HISFC.BizProcess.Interface.Common.ILis
    {
        /// <summary>
        /// 患者类别
        /// </summary>
        private FS.HISFC.Models.RADT.EnumPatientType patientType = FS.HISFC.Models.RADT.EnumPatientType.O;

        private string errMsg = "";

        #region ILis 成员

        public bool CheckOrder(FS.HISFC.Models.Order.Order order)
        {
            return false;
        }

        public int Commit()
        {
            return 1;
        }

        public int Connect()
        {
            return 1;
        }

        public int Disconnect()
        {
            return 1;
        }

        public string ErrCode
        {
            get
            {
                return "";
            }
        }
        
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }
        }

        public bool IsReportValid(string id)
        {
            return false;
        }

        public int PlaceOrder(ICollection<FS.HISFC.Models.Order.Order> orders)
        {
            return 1;
        }

        public int PlaceOrder(FS.HISFC.Models.Order.Order order)
        {
            return 1;
        }

        public string[] QueryResult()
        {
            return new string[] { };
        }

        public int Rollback()
        {
            return 1;
        }

        public void SetTrans(System.Data.IDbTransaction t)
        {
            return;
        }

        public int ShowResult(string id)
        {
            return 1;
        }

        /// <summary>
        /// 显示Lis结果
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public int ShowResultByPatient()
        {
            try
            {
                //string loadPath = "";
                //if (System.IO.File.Exists(Application.StartupPath + "\\LisInterface.xml"))
                //{
                //    XmlDocument file = new XmlDocument();
                //    file.Load(Application.StartupPath + "\\LisInterface.xml");
                //    XmlNode node = file.SelectSingleNode("Config/Path");
                //    if (node != null)
                //    {
                //        loadPath = node.InnerText;
                //    }
                //}

                //if (string.IsNullOrEmpty(loadPath))
                //{
                //    this.errMsg = "请维护LIS程序的启动目录，配置文件为根目录“LisInteface.xml”";
                //    return -1;
                //}
                //System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(Application.StartupPath + loadPath);//"PatientEls.dll");

                //Type[] t = asm.GetTypes();
                //foreach (Type tmp in t)
                //{
                //    if (tmp.Name == "HisExe")
                //    {
                //        object o = asm.GetType();
                //        System.Reflection.MethodInfo method = tmp.GetMethod("Show");
                //        object[] parms = new object[] { myPatientInfo.PID.CardNO };
                //        method.Invoke(o, parms);
                //    }
                //}

                ////PatientEls.HisExe.Show(myPatientInfo.PID.CardNO);
 

                switch (ResultType)
                {
                    case "1":  //心电图

                        ECGNET();
                        break;
                    case "2": // 检验
                        Lis();
                        break;
                    case "3": //华海PACS
                        Pacs();
                        break;
                    case "4": //B超
                        Efilm();
                        break;
                    case "5": //B超
                        EUS();
                        break;
                    case "tbInfectionReport"://院感上报
                        HICSCRB();
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 患者类别
        /// </summary>
        public FS.HISFC.Models.RADT.EnumPatientType PatientType
        {
            get
            {
                return this.patientType;
            }
            set
            {
                this.patientType = value;
            }
        }

        /// <summary>
        /// 结果类型
        /// </summary>
        string resultType;

        public string ResultType
        {
            get 
            { 
                return resultType; 
            }
            set 
            { 
                resultType = value; 
            }
        }

        /// <summary>
        /// 当前患者
        /// </summary>
        FS.HISFC.Models.RADT.Patient myPatientInfo = null;

        public int SetPatient(FS.HISFC.Models.RADT.Patient patient)
        {
            myPatientInfo = patient;
            return 1;
        }

        #endregion

        #region 华海pacs
        private void Pacs()
        {
            try
            {
                string errMsg = string.Empty;
                string ttemp = "NoStart";

                Process[] arrP = processMrg.ListProcesses();
                foreach (Process p in arrP)
                {
                    try
                    {
                        if (p.MainWindowHandle != null && !string.IsNullOrEmpty(p.MainWindowTitle))
                        {
                            if (p.MainWindowTitle.Contains("ClinicalPACS.xbap"))
                            {
                                ttemp = "START";
                                break;
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                if (ttemp == "NoStart")
                {
                    System.Diagnostics.Process.Start("http://192.168.250.210/ClinicalPACS/ClinicalPACS.xbap?P=" + this.myPatientInfo.PID.CardNO);
                }
                else
                {
                    HuaHaiServices.ConnectHISServiceClient CHS = new HuaHaiServices.ConnectHISServiceClient();
                    CHS.InputParameter("P", this.myPatientInfo.PID.CardNO);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 心电图
        /// <summary>
        /// 心电图系统进程名
        /// add by lijp 2011-04-25
        /// </summary>
        private const string ECGPROCESSNAME = @"ECGRptView.exe";

        /// <summary>
        /// 进程管理类
        /// </summary>
        private static FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager processMrg = new FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager();

        /// <summary>
        /// 心电图调用
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="appFolderPath"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public void ECGNET()
        {
            try
            {
                string patientNo = this.myPatientInfo.PID.CardNO;

                if (string.IsNullOrEmpty(patientNo))
                {
                    return;
                }

                processMrg.KillProcess(ECGPROCESSNAME);
                string appFolderPath = Application.StartupPath;
                string fullFilePath = appFolderPath + "\\" + ECGPROCESSNAME;

                if (Directory.Exists(appFolderPath))
                {
                    if (!System.IO.File.Exists(fullFilePath))
                    {
                        System.Windows.Forms.MessageBox.Show("找不到心电图系统客户端：ECGRptView.exe，请检查是否已安装。");
                        return;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("找不到目录 " + appFolderPath + "，请确认安装在正确的路径。");
                    return;
                }
                System.Windows.Forms.Application.DoEvents();

                FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager.ShellExecute(new IntPtr(), "open", fullFilePath, " -R" + patientNo + " -T0", null, (int)FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager.ShowWindowCommands.SW_SHOW);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 超声
        private void Efilm()
        {
            string patientNo = this.myPatientInfo.PID.CardNO;
            try
            {
                if (string.IsNullOrEmpty(patientNo))
                {
                    return;
                }

                processMrg.KillProcess("SuperSoundReport.exe");
                string appFolderPath = Application.StartupPath;
                string fullFilePath = appFolderPath + "\\csview\\SuperSoundReport.exe";

                if (Directory.Exists(appFolderPath))
                {
                    if (!System.IO.File.Exists(fullFilePath))
                    {
                        System.Windows.Forms.MessageBox.Show("找不到超声系统客户端，请检查是否已安装。");
                        return;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("找不到目录 " + appFolderPath + "，请确认安装在正确的路径。");
                    return;
                }
                System.Windows.Forms.Application.DoEvents();

                FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager.ShellExecute(new IntPtr(), "open", fullFilePath,
                    "," + patientNo + ",,200", null, (int)FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager.ShowWindowCommands.SW_SHOW);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Lis

   
       // [DllImport("kernel32")]
　　   //private static extern long GetPrivateProfileString(string section,string key,string def,StringBuilder retVal,int size,string filePath);




        private void Lis()
        {
            string patientNo = this.myPatientInfo.PID.CardNO;
            try
            {
                if (string.IsNullOrEmpty(patientNo))
                {
                    return;
                }
                string LisPath = string.Empty;

                //System.IO.StreamReader sr = new System.IO.StreamReader(@"C:\Program Files\hope\his\lis.ini"); 
                //while(sr.ReadLine()!=null) 
                //{ 

                //    LisPath = sr.ReadLine();
                //    //放入第一个 list list2 = arr[1]; 
                //}



                //StringBuilder Buffer = new StringBuilder(500);

                //long bufLen = GetPrivateProfileString("", "", "", Buffer, 500, @"C:\Program Files\hope\his\lis.ini");
                ////必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
                //string s = Buffer.ToString();
                //LisPath = s.Trim();


                FileStream fs1 = new FileStream(@"C:\Program Files\hope\his\lis.ini", FileMode.Open); 
                StreamReader sr = new StreamReader(fs1); 
                string str1 = sr.ReadToEnd();
                LisPath = str1.Trim(); 
                sr.Close(); 
                fs1.Close();

                processMrg.KillProcess("lis.client.barcodeclient.exe");
                string fullFilePath = LisPath;


                if (!System.IO.File.Exists(fullFilePath))
                {
                    System.Windows.Forms.MessageBox.Show("找不到检验系统客户端，请检查是否已安装。");
                    return;
                }

                System.Windows.Forms.Application.DoEvents();

                FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager.ShellExecute(new IntPtr(), "open", fullFilePath,
                    "report;-1;" + patientNo + ";-1;-1;-1;107;", null, (int)FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager.ShowWindowCommands.SW_SHOW);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 内镜信息系统
        private void EUS()
        {
            System.Diagnostics.Process.Start("http://192.168.19.30/Report.aspx?InPatNo=&OutPatNo=" + this.myPatientInfo.PID.CardNO + "&PatientID=");
        }

        #endregion

        #region 院感上报
        /// <summary>
        /// 院感上报系统进程名
        /// </summary>
        private const string HICSCRBNAME = @"InfectionReport"+"\\"+"HICS_crb.exe";
        /// <summary>
        /// 院感上报调用
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="appFolderPath"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public void HICSCRB()
        {
            try
            {
                string patientNo = this.myPatientInfo.PID.CardNO;

                if (string.IsNullOrEmpty(patientNo))
                {
                    return;
                }
                processMrg.KillProcess(HICSCRBNAME);
                string appFolderPath = Application.StartupPath;
                string fullFilePath = appFolderPath + "\\" + HICSCRBNAME;

                if (Directory.Exists(appFolderPath))
                {
                    if (!System.IO.File.Exists(fullFilePath))
                    {
                        System.Windows.Forms.MessageBox.Show("找不到报卡系统客户端：HICS_crb.exe，请检查是否已安装。");
                        return;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("找不到目录 " + appFolderPath + "，请确认安装在正确的路径。");
                    return;
                }
                System.Windows.Forms.Application.DoEvents();

                //1- 住院 2- 门诊
                if ("1".Equals(this.myPatientInfo.Memo))
                {
                    FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager.ShellExecute(new IntPtr(), "open", fullFilePath, this.myPatientInfo.Memo + ",0," + this.myPatientInfo.PID.PatientNO + "," + this.myPatientInfo.InTimes, null, (int)FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager.ShowWindowCommands.SW_SHOW);
                }
                else
                {
                    FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager.ShellExecute(new IntPtr(), "open", fullFilePath, this.myPatientInfo.Memo + ",0," + this.myPatientInfo.PID.CardNO + ",1", null, (int)FS.SOC.Local.Order.OutPatientOrder.ZDLY.ResultPrint.ProcessesManager.ShowWindowCommands.SW_SHOW);
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
