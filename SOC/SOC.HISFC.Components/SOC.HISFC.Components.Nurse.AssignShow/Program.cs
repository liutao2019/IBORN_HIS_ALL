using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using System.Collections;

namespace SOC.HISFC.Components.Nurse.AssignShow
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            /*
            System.Diagnostics.Process[] sysProcess = System.Diagnostics.Process.GetProcessesByName("HISTIMEJOB");

            //{814E89EB-9361-425b-BA77-3C3D1D4631B5} by niuxy 判断进程数，防止多次打开应用
            if (sysProcess.Length > 1)
            {
                MessageBox.Show("当前系统中有正在运行的固定费用程序，请关掉后再启动！");
                Application.Exit();
                return;
            }
            */ 

            if (Application.StartupPath.Substring(Application.StartupPath.Length - 1, 1) == @"\")
            {
                FS.FrameWork.WinForms.Classes.Function.CurrentPath = Application.StartupPath;
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.CurrentPath = Application.StartupPath + @"\";
            }

            Application.DoEvents();

            if (GetSetting() == -1)
            {
                Application.Exit();
            }

            if (ConnectDB() == -1)
            {
                return;
            }

            if (ConnectSQL() == -1)
            {
                return;
            }

            try
            {
                if (System.IO.File.Exists("DebugSql.log"))
                {
                    System.IO.File.Delete("DebugSql.log");
                    System.IO.File.CreateText("DebugSql.log");
                }
            }
            catch { }


            #region 获取护士站信息

            FS.HISFC.BizProcess.Integrate.Manager constManagr = new FS.HISFC.BizProcess.Integrate.Manager();


            FS.HISFC.BizLogic.Nurse.Queue queue=new FS.HISFC.BizLogic.Nurse.Queue();

            FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();


            ArrayList   deptList=constManagr.GetConstantList("AssignNurseIP");

            string ip = GetMyIP();

            foreach (FS.HISFC.Models.Base.Const dept in deptList)
            {
                if (dept.Memo.Trim() == ip.Trim() || dept.UserCode.Trim() == ip.Trim())
                {
                    employee.Nurse = dept;
                    employee.Dept = dept;
                    break;
                }
            }

            if (string.IsNullOrEmpty(employee.Nurse.ID))
            {
                MessageBox.Show("未维护显示屏对应的护士站！");
            }

            FS.FrameWork.Management.Connection.Operator = employee;

            FS.FrameWork.Management.Connection.Hospital.ID = queue.ExecSqlReturnOne("SELECT HOS_CODE FROM COM_HOSPITALINFO", "CORE_HIS50");

            #endregion


            Application.Run(new frmDisplay1());
        }

        #region 函数
        /// <summary>
        /// 获取本地IP
        /// </summary>
        /// <returns></returns>
        static string GetMyIP()
        {
            IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
            return IpEntry.AddressList[0].ToString();
        }

        #endregion


        #region  数据库操作

        const string UrlFileName = "url.xml";
        static string DataSource = "";
        static string ServerPath = "";

        static bool IsSqlInDB = true;

        static string ManagerPWD = "his";

        /// <summary>
        /// 获得配置文件
        /// </summary>
        /// <returns></returns>
        internal static int GetSetting()
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                doc.Load(FS.FrameWork.WinForms.Classes.Function.CurrentPath + UrlFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("装载url失败！\n" + ex.Message);
                return -1;
            }
            System.Xml.XmlNode node = doc.SelectSingleNode("//dir");
            if (node == null)
            {
                MessageBox.Show("url中找dir结点出错！");
                return -1;
            }

            ServerPath = node.InnerText;
            string serverSettingFileName = "HisProfile.xml"; //服务器文件名

            try
            {
                if (ServerPath.ToLower().Contains("http"))
                {
                    doc.Load(ServerPath + serverSettingFileName);
                }
                else 
                {
                    doc.Load(FS.FrameWork.WinForms.Classes.Function.CurrentPath + ServerPath + serverSettingFileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("装载HisProfile.xml失败！\n" + ex.Message);
                return -1;
            }

            node = doc.SelectSingleNode("/设置/数据库设置");

            if (node == null)
            {
                MessageBox.Show("没有找到数据库设置!");
                return -1;
            }

            string strDataSource = node.Attributes[0].Value;

            if (strDataSource.ToUpper().IndexOf("PASSWORD") > 0)
            {

            }
            else //需要解密
            {
                //
                //strDataSource = FS.HisDecrypt.Decrypt(strDataSource);
                strDataSource = FS.HisCrypto.DESCryptoService.DESDecrypt(strDataSource, FS.FrameWork.Management.Connection.DESKey);
            }

            DataSource = strDataSource;

            node = doc.SelectSingleNode("/设置/设置");

            if (node == null)
            {
                MessageBox.Show("没有找到SQL设置!");
                return -1;
            }

            if (node.Attributes[0].Value == "1")//Sql.xml
            {
                IsSqlInDB = false;
            }
            else//数据库
            {
                IsSqlInDB = true;
            }

            node = doc.SelectSingleNode("/设置/管理员");


            if (node == null)
            {
                MessageBox.Show("没有找到管理员密码!");
                return -1;
            }

            ManagerPWD = node.Attributes[0].Value;

            return 0;


        }

        internal static int ConnectDB()
        {
            //不存在Remote.config则按照2层结构走，读取配置文件
            string err = string.Empty;
            if (!System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + @"\" + "Remote.config"))
            {
                if (FS.FrameWork.Management.Connection.GetSetting(out err) == -1)
                {
                    MessageBox.Show(err);
                    Application.Exit();
                }
            }
            return 0;
        }

        internal static int ConnectSQL()
        {
            if (IsSqlInDB)
            {
                FS.FrameWork.Management.Connection.Sql = new FS.FrameWork.Management.Sql(FS.FrameWork.Management.Connection.Instance);
            }
            else
            {
                FS.FrameWork.Management.Connection.Sql = new FS.FrameWork.Management.Sql(ServerPath + "SQL.xml");
            }

            return 0;
        }

        #endregion

        
    }
}