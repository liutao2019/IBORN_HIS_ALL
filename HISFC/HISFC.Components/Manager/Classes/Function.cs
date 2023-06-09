using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Net.NetworkInformation;

namespace FS.HISFC.Components.Manager.Classes
{
    public class Function
    {

        public static void ShowForm(FS.HISFC.Models.Admin.SysMenu obj)
        {
            if (obj == null) return;

            string dllName = obj.ModelFuntion.DllName + ".dll";
            string formName = obj.ModelFuntion.WinName.TrimStart().TrimEnd();
            string tag = obj.MenuParm;
            string param = "";
            string showType = obj.ModelFuntion.FormShowType;
            string tree = obj.ModelFuntion.TreeControl.WinName;
            string treeDll = obj.ModelFuntion.TreeControl.DllName + ".dll";
            string treeTag = obj.ModelFuntion.TreeControl.Param;


            if (formName == "") return;

            if (formName.IndexOf(" ") >= 0)
            {
                param = formName.Substring(formName.IndexOf(" ") + 1).TrimStart();
                formName = formName.Substring(0, formName.IndexOf(" "));
            }

            System.Windows.Forms.Control form = null;
            System.Reflection.Assembly assembly = null;

            switch (obj.MenuWin)//特殊窗口
            {
                case "Test":

                    return;
                case "Register":

                    return;
                case "Help":

                    return;
                case "ChangePWD":
                    /*应该实现代码*/
                    return;
                case "Exit":

                    return;
                default: //其它窗口
                    Object[] objParam = null;
                    if (param != "")
                    {
                        objParam = new object[0];
                        objParam[0] = param;
                    }
                    try
                    {
                        assembly = System.Reflection.Assembly.LoadFrom(dllName);
                        Type type = assembly.GetType(formName);
                        if (type == null)
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("创建窗体出错！\n") + formName);
                            return;
                        }
                        System.Object objHandle = System.Activator.CreateInstance(type, objParam);
                        form = objHandle as Control;
                        form.Tag = tag;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("创建窗体出错！\n") + ex.Message);
                        return;
                    }

                    break;
            }

            FS.FrameWork.WinForms.Forms.IMaintenanceControlable iQueryControl = form as FS.FrameWork.WinForms.Forms.IMaintenanceControlable;
            if (iQueryControl != null) //维护查询窗口
            {
                form = new FS.FrameWork.WinForms.Forms.frmQuery(iQueryControl);
                //将菜单的名称付给窗口的名称
                form.Text = obj.MenuName;
            }

            FS.FrameWork.WinForms.Forms.IControlable iControlable = form as FS.FrameWork.WinForms.Forms.IControlable;
            if (iControlable != null) //功能窗口
            {
                //添加树
                System.Windows.Forms.TreeView tv = null;
                if (tree.Trim() != "")
                {
                    assembly = System.Reflection.Assembly.LoadFrom(treeDll);
                    tv = AddTree(tree, assembly, tv);
                }
                if (tv == null)
                    form = new FS.FrameWork.WinForms.Forms.frmBaseForm(form);
                else
                {
                    tv.Tag = treeTag;
                    form = new FS.FrameWork.WinForms.Forms.frmBaseForm(form, tv);
                }
                //将菜单的名称付给窗口的名称
                form.Text = obj.MenuName;
            }

            Type typeSender = form.GetType();
            if (typeSender.IsSubclassOf(typeof(FS.FrameWork.WinForms.Forms.frmBaseForm)) || typeSender == typeof(FS.FrameWork.WinForms.Forms.frmBaseForm))
            {
                ((FS.FrameWork.WinForms.Forms.frmBaseForm)form).SetFormID(obj.MenuWin);
            }

            switch (showType)
            {
                case "FormDialog":
                    ((Form)form).ShowDialog();
                    break;
                case "Web":
                    try
                    {
                        System.Diagnostics.Process.Start("iexplore.exe", formName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                default:
                    ((Form)form).ShowDialog();
                    break;
            }
        }

        private static System.Windows.Forms.TreeView AddTree(string tree, System.Reflection.Assembly assembly, System.Windows.Forms.TreeView tv)
        {
            Type type = assembly.GetType(tree);
            if (type != null)
            {
                try
                {

                    tv = System.Activator.CreateInstance(type) as System.Windows.Forms.TreeView;
                }
                catch { }

            }
            return tv;
        }

        #region 系统监控

        #region 数据库 操作COM_IPADDRESS Sql 语句
        static string sqlInsert = @"INSERT INTO COM_IPADDRESS T
            ( LOG_ADDRESS,      --IP地址
			  LOGIN_DATE,		--最近一次登录日期
			  EMPL_CODE,		--最近一次登陆员工代码
			  VERSION_NUMBER,	--程序版本
              DEPT_NAME,        --科室名称
              GROUP_NAME,       --系统组
              FLAG,             --标志位 功能 待扩展
              EXT_CODE,         --err.log 文件大小
              EXT_CODE1         --debugsql.log文件大小
            )
      VALUES
            ( '{0}',
              to_date('{1}','yyyy-mm-dd HH24:mi:ss'),
              '{2}',
              '{3}',
              '{4}',
              '{5}',
			  '{6}',
			  '{7}',
			  '{8}'
              )";
        static string sqlUpdate = @"UPDATE COM_IPADDRESS T
				SET    T.LOGIN_DATE = to_date('{1}','yyyy-mm-dd HH24:mi:ss'),
					   T.EMPL_CODE = '{2}',
					   T.VERSION_NUMBER = '{3}',
					   T.DEPT_NAME = '{4}',
				       T.GROUP_NAME = '{5}',
				       T.FLAG = '{6}',
					   T.EXT_CODE = '{7}',
				       T.EXT_CODE1 = '{8}'
				WHERE  T.LOG_ADDRESS = '{0}'";
        #endregion

        #region 数据库 操作COM_HOSPITALINFO Select语句 先单独写 以后程序整合时再添加业务层
        static string versionSelect = @"SELECT T.VERSION_NUMBER
										FROM   COM_HOSPITALINFO T
										WHERE  ROWNUM = 1";
        static string ftpSelect = @"SELECT T.Number_Value
										FROM   COM_HOSPITALINFO T
										WHERE  ROWNUM = 1";
        #endregion

        #region Login Log

        /// <summary>
        /// 系统登录ID
        /// </summary>
        static string LoginSessionID = "NULL";

        #endregion

        /// <summary>
        /// 程序版本号
        /// </summary>
        static string VersionNumber = "20070101";

        /// <summary>
        /// 信息提示接口
        /// </summary>
        private static System.Collections.Hashtable hsNoticeInterface = null;

        /// <summary>
        /// 创建信息提示接口集合
        /// 
        /// {5BE03DF2-25DE-4e7a-9B47-85CE92911277}  新增方法
        /// </summary>
        /// <returns></returns>
        private static int CreateNoticeInterfaceCollection()
        {
            hsNoticeInterface = new Hashtable();

            FS.HISFC.BizProcess.Interface.Common.INoticeManager noticeInterface = null;
            try
            {
                string directoryPath = Application.StartupPath + "\\" + FS.FrameWork.WinForms.Classes.Function.PluginPath + "Notice";

                if (System.IO.Directory.Exists(directoryPath))
                {
                    string[] strNoticeDll = System.IO.Directory.GetFiles(directoryPath);

                    int tempKey = 1;
                    foreach (string str in strNoticeDll)
                    {
                        if (str.IndexOf(".dll") == -1)
                        {
                            continue;
                        }
                        Assembly a = Assembly.LoadFrom(str);

                        System.Type[] types = a.GetTypes();
                        foreach (System.Type type in types)
                        {
                            //{89381D97-6630-4fb5-8ACE-7CAF578207F5} 修订接口的定义
                            if (type.GetInterface( "FS.HISFC.BizProcess.Interface.Common.INoticeManager" ) != null)
                            {
                                noticeInterface = (FS.HISFC.BizProcess.Interface.Common.INoticeManager)System.Activator.CreateInstance(type);

                                if (noticeInterface != null)
                                {
                                    hsNoticeInterface.Add(tempKey, noticeInterface);
                                    tempKey++;
                                }

                            }
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// HIS系统监视函数
        /// </summary>
        /// <param name="employee">登陆人员信息</param>
        /// <returns>无错误返回1 出错返回-1</returns>
        public static int HISMonitor()
        {
            #region 版本登陆

            //{89381D97-6630-4fb5-8ACE-7CAF578207F5} 增加对返回值的处理
            if (JudgeVersionNumber() == -1)
            {
                return -1;
            }

            #endregion

            #region 自动运行

            //{5BE03DF2-25DE-4e7a-9B47-85CE92911277}  修改原接口获取方式
            try
            {
                if (hsNoticeInterface == null)
                {
                    CreateNoticeInterfaceCollection();
                }
                if (hsNoticeInterface != null)
                {
                    foreach (FS.HISFC.BizProcess.Interface.Common.INoticeManager noticeInterface in hsNoticeInterface.Values)
                    {
                        if (noticeInterface != null)
                        {
                            //{89381D97-6630-4fb5-8ACE-7CAF578207F5} 增加对返回值的处理
                            if (noticeInterface.Notice() == -1)
                            {
                                return -1;
                            }
                            //{89381D97-6630-4fb5-8ACE-7CAF578207F5} 增加对返回值的处理
                            if (noticeInterface.Warn() == -1)
                            {
                                return -1;
                            }
                            //{89381D97-6630-4fb5-8ACE-7CAF578207F5} 增加对返回值的处理
                            if (noticeInterface.Schedule() == -1)
                            {
                                return -1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            #endregion

            #region 提示信息

            ShowNotice();

            #endregion

            #region 操作员登陆Log

            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            FS.HISFC.Models.Base.Employee oper = (FS.HISFC.Models.Base.Employee)dataManager.Operator;
            SaveLoginInfo(oper);

            NoteLog(oper, true, true, true, 1024);

            #endregion
           
            return 1;
        }

        /// <summary>
        /// HIS系统监视函数
        /// 
        /// {5BE03DF2-25DE-4e7a-9B47-85CE92911277} 增加HIS系统注销时的函数
        /// </summary>
        /// <returns>无错误返回1 出错返回-1</returns>
        public static int HISLogout()
        {
            #region 自动运行

            try
            {
                if (hsNoticeInterface == null)
                {
                    CreateNoticeInterfaceCollection();
                }
                if (hsNoticeInterface != null)
                {
                    foreach (FS.HISFC.BizProcess.Interface.Common.INoticeManager noticeInterface in hsNoticeInterface.Values)
                    {
                        if (noticeInterface != null)
                        {
                            noticeInterface.MsgOnLogout();
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }

            #endregion

            //系统注销 {DEA84BD8-882A-440c-AF5B-3C244D16211D}
            SaveLogoutInfo();

            return 1;
        }

        /// <summary>
        /// 判断程序版本是否为最新版本
        /// </summary>
        /// <returns>新版本返回1 旧版本返回-1</returns>
        public static int JudgeVersionNumber()
        {
            FS.FrameWork.Xml.XML xmlManager = new FS.FrameWork.Xml.XML();
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlNode root;

            #region 获取本地配置文件内HIS系统版本

            string hosVersionNumber = "";
            //string hosFilePath = Application.StartupPath + "\\HIS.config";
            //if (System.IO.File.Exists(hosFilePath))			//存在配置文件
            //{
                //doc.Load(hosFilePath);
                //root = doc.SelectSingleNode("//VersionNumber");
                hosVersionNumber = System.Configuration.ConfigurationManager.AppSettings["VersionNumber"];
                
                if (string.IsNullOrEmpty(hosVersionNumber))
                {
                    MessageBox.Show("读取本地XML配置文件时 未找到版本号结点");
                    return -1;
                }
                //hosVersionNumber = obj.ToString(); //root.Attributes[0].Value.ToString();
            //}
            //else
            //{
            //    #region 自动建立

            //    System.Xml.XmlDocument newDoc = new System.Xml.XmlDocument();
            //    System.Xml.XmlDeclaration declarationNode = newDoc.CreateXmlDeclaration("1.0", "GB2312", null);
            //    newDoc.AppendChild(declarationNode);

            //    System.Xml.XmlElement configNode = newDoc.CreateElement("Config");
            //    configNode.SetAttribute("Version", "4.5");
            //    newDoc.AppendChild(configNode);

            //    System.Xml.XmlElement versionNumNode = newDoc.CreateElement("VersionNumber");
            //    versionNumNode.SetAttribute("Number", "20070101");
            //    configNode.AppendChild(versionNumNode);

            //    newDoc.Save(Application.StartupPath + "\\HIS.config");

            //    #endregion

            //    return 1;
            //}
            Function.VersionNumber = hosVersionNumber;
            #endregion

            //读取控制参数 如果未找到对应控制参数或参数为0 不进行版本校验
            FS.FrameWork.Management.ControlParam controlerManagment = new FS.FrameWork.Management.ControlParam();

            string control = controlerManagment.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.SysConst.Use_Judge_VersionNum);
            if (control == null || control == "0" || control == "-1")
            {
                return 1;
            }

            #region 获取服务器版本号

            string serverVersionNumber = "";
            FS.FrameWork.Management.DataBaseManger dataBase = new FS.FrameWork.Management.DataBaseManger();
            dataBase.ExecQuery(Function.versionSelect);
            try
            {
                if (dataBase.Reader.Read())
                {
                    serverVersionNumber = dataBase.Reader[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dataBase.Reader.Close();
            }
            #endregion

            if (hosVersionNumber != serverVersionNumber)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("您当前所用的信息管理系统非最新版本 请重启系统进行升级或与信息科联系"));
                return -1;
            }

            return 1;
        }

        #region 系统登录日志

        /// <summary>
        /// 保存操作员登陆信息
        /// 
        /// {DEA84BD8-882A-440c-AF5B-3C244D16211D}
        /// </summary>
        public static int SaveLoginInfo(FS.HISFC.Models.Base.Employee oper)
        {
            if (LoginSessionID == "NULL")       //首次登陆
            {
                //{70BC3C2C-823D-4c56-A06A-3E584EA13B2B}  获取当前登录IP并显示在界面上
                string hosName = System.Net.Dns.GetHostName();
                string ip = string.Empty;

                foreach (System.Net.IPAddress info in System.Net.Dns.GetHostAddresses( hosName ))
                {
                    if (info.IsIPv6LinkLocal == true)
                    {
                        continue;
                    }

                    ip = info.ToString();
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();               

                FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();

                LoginSessionID = userManager.GetLoginSessionID();

                if (userManager.InsertLoginLog(oper, LoginSessionID, ip, hosName) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    return -1;
                }                

                FS.FrameWork.Management.PublicTrans.Commit();

                return 1;
            }

            return 1;
        }

        /// <summary>
        /// 保存操作员注销信息
        /// 
        /// {DEA84BD8-882A-440c-AF5B-3C244D16211D}
        /// </summary>
        public static int SaveLogoutInfo()
        {
            if (LoginSessionID != "NULL")
            {
                try
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    FS.HISFC.BizLogic.Manager.UserManager userManager = new FS.HISFC.BizLogic.Manager.UserManager();

                    if (userManager.UpdateLogoutLog(LoginSessionID) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();

                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                }
                catch { }
                return 1;
            }

            return 1;
        }

        /// <summary>
        /// 记录debugsql.log 与 err.log 日志文件大小信息 并对大于删除标准的文件进行删除
        /// </summary>
        /// <param name="var">登陆信息</param>
        /// <param name="isUpdate">对已插入的信息是否进行更新操作</param>
        /// <param name="delSqlLog">是否对debugSql.log进行删除</param>
        /// <param name="delErrLog">是否对err.log进行删除</param>
        /// <param name="delSize">需删除文件大小标准 以K为单位</param>
        public static int NoteLog(FS.HISFC.Models.Base.Employee oper, bool isUpdate, bool delSqlLog, bool delErrLog, int delSize)
        {
            if (oper != null && oper.ID != "")
            {
                string sql = "";
                int parm = -1;

                //{70BC3C2C-823D-4c56-A06A-3E584EA13B2B}  获取当前登录IP并显示在界面上
                string hosName = System.Net.Dns.GetHostName();
                string hosIP = string.Empty;
                string firstIP = string.Empty;
                Ping userping = new Ping();

                //foreach (System.Net.IPAddress info in System.Net.Dns.GetHostAddresses( hosName ))
                //{
                //    if (info.IsIPv6LinkLocal == true)
                //    {
                //        continue;
                //    }

                //    hosIP = info.ToString();
                //}
                System.Net.IPAddress[] ipa = System.Net.Dns.GetHostAddresses(hosName);
                foreach (System.Net.IPAddress info in System.Net.Dns.GetHostAddresses(hosName))
                {
                    if (info.IsIPv6LinkLocal == true)
                    {
                        continue;
                    }
                    //AddressFamily 对于 IPv4，返回 InterNetwork；对于 IPv6，返回 InterNetworkV6
                    else if (info.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(firstIP))
                    {
                        firstIP = info.ToString();
                    }

                    PingReply pr = userping.Send(info.ToString());
                    if (pr.Status == IPStatus.Success)//还有其他的枚举状态，自己去看 
                    {
                        hosIP = info.ToString();
                        break;
                    }
                }

                if (string.IsNullOrEmpty(hosIP))
                {
                    hosIP = firstIP;
                }

                FS.FrameWork.Management.DataBaseManger dataBase = new FS.FrameWork.Management.DataBaseManger();              

                #region 数据库记录

                //自动计费程序标志
                string fixFeeFlag = "1";
                if (System.IO.File.Exists(Application.StartupPath + "\\HISTIMEJOB.exe"))
                {
                    fixFeeFlag = "F";
                    try
                    {
                        System.IO.FileInfo fixFeeFs = new System.IO.FileInfo(Application.StartupPath + "\\FixFee.exe");
                        fixFeeFs.Delete();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除HISTIMEJOB文件失败 请与信息科联系" + ex.Message));
                        return -1;
                    }
                }
                if (System.IO.File.Exists(Application.StartupPath + "\\HISTIMEJOB.exe"))
                {
                    fixFeeFlag = "F";
                    try
                    {
                        System.IO.FileInfo fixFeeFs = new System.IO.FileInfo(Application.StartupPath + "\\FIXFEE.exe");
                        fixFeeFs.Delete();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除HISTIMEJOB文件失败 请与信息科联系" + ex.Message));
                        return -1;
                    }
                }

                //FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(dataBase.Connection);
                //t.BeginTransaction();

                //dataBase.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (isUpdate)
                {
                    sql = string.Format(sqlUpdate, hosIP, dataBase.GetDateTimeFromSysDateTime().ToString(), oper.ID,
                        Function.VersionNumber, oper.Dept.Name, oper.CurrentGroup.Name, fixFeeFlag, "", "");
                    parm = dataBase.ExecNoQuery(sql);
                    if (parm == -1)
                    {
                        //FS.FrameWork.Management.PublicTrans.RollBack();;
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("根据文件信息对数据进行插入失败\n" + dataBase.Err));
                        return -1;
                    }
                    if (parm > 0)
                    {
                        //FS.FrameWork.Management.PublicTrans.Commit();;
                        return 1;
                    }
                }
                sql = string.Format(sqlInsert, hosIP, dataBase.GetDateTimeFromSysDateTime().ToString(), oper.ID,
                    Function.VersionNumber, oper.Dept.Name, oper.CurrentGroup.Name, fixFeeFlag, "", "");
                parm = dataBase.ExecNoQuery(sql);
                if (parm < 0 && dataBase.DBErrCode != 1)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("根据文件信息对数据进行更新失败\n" + dataBase.Err));
                    return -1;
                }
                //FS.FrameWork.Management.PublicTrans.Commit();;

                #endregion
            }

            return 1;
        }

        #endregion

        /// <summary>
        /// 检索并显示发布信息
        /// </summary>
        /// <param name="alAddNotice">附加的发布信息</param>
        public static void ShowNotice(ArrayList alAddNotice)
        {
            //郁闷 using 不能用于静态函数内
            using (Manager.Notice.frmNoticeManager frm = new Manager.Notice.frmNoticeManager())
            {
                frm.OnlyShow = true;
                frm.ShowData(alAddNotice);
                if (frm.NoticeInfo.Count > 0)
                    frm.ShowDialog();
            }
        }

        /// <summary>
        /// 检索并显示发布信息
        /// </summary>
        public static void ShowNotice()
        {
            ShowNotice(null);
        }

        #endregion

        #region {619F3CBF-7954-4d5e-B815-C66987E15C60}   床位监控

        public static decimal LicenceBedQty = -1;

        /// <summary>
        /// 床位校验
        /// </summary>
        /// <returns>True 校验通过 False  校验失败</returns>
        internal static bool BedVerify()
        {
            if (LicenceBedQty != -1)
            {
                FS.FrameWork.Management.DataBaseManger dataBase = new FS.FrameWork.Management.DataBaseManger();

                string bedNumSql = @"select count(*) from com_bedinfo";
                decimal bedQty = -1;
                try
                {
                    bedQty = FS.FrameWork.Function.NConvert.ToDecimal( dataBase.ExecSqlReturnOne( bedNumSql ) );
                }
                catch (Exception ex)
                {
                    MessageBox.Show( ex.Message );
                    return false;
                }

                if (LicenceBedQty < bedQty)
                {
                    MessageBox.Show( "维护总床位数已超过系统授权限制，请与供应商联系  \n\n授权床位数：" + LicenceBedQty.ToString() + "  当前床位数：" + bedQty.ToString() ,"提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
