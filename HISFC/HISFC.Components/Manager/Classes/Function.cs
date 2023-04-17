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

            switch (obj.MenuWin)//���ⴰ��
            {
                case "Test":

                    return;
                case "Register":

                    return;
                case "Help":

                    return;
                case "ChangePWD":
                    /*Ӧ��ʵ�ִ���*/
                    return;
                case "Exit":

                    return;
                default: //��������
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
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����������\n") + formName);
                            return;
                        }
                        System.Object objHandle = System.Activator.CreateInstance(type, objParam);
                        form = objHandle as Control;
                        form.Tag = tag;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����������\n") + ex.Message);
                        return;
                    }

                    break;
            }

            FS.FrameWork.WinForms.Forms.IMaintenanceControlable iQueryControl = form as FS.FrameWork.WinForms.Forms.IMaintenanceControlable;
            if (iQueryControl != null) //ά����ѯ����
            {
                form = new FS.FrameWork.WinForms.Forms.frmQuery(iQueryControl);
                //���˵������Ƹ������ڵ�����
                form.Text = obj.MenuName;
            }

            FS.FrameWork.WinForms.Forms.IControlable iControlable = form as FS.FrameWork.WinForms.Forms.IControlable;
            if (iControlable != null) //���ܴ���
            {
                //�����
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
                //���˵������Ƹ������ڵ�����
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

        #region ϵͳ���

        #region ���ݿ� ����COM_IPADDRESS Sql ���
        static string sqlInsert = @"INSERT INTO COM_IPADDRESS T
            ( LOG_ADDRESS,      --IP��ַ
			  LOGIN_DATE,		--���һ�ε�¼����
			  EMPL_CODE,		--���һ�ε�½Ա������
			  VERSION_NUMBER,	--����汾
              DEPT_NAME,        --��������
              GROUP_NAME,       --ϵͳ��
              FLAG,             --��־λ ���� ����չ
              EXT_CODE,         --err.log �ļ���С
              EXT_CODE1         --debugsql.log�ļ���С
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

        #region ���ݿ� ����COM_HOSPITALINFO Select��� �ȵ���д �Ժ��������ʱ�����ҵ���
        static string versionSelect = @"SELECT T.VERSION_NUMBER
										FROM   COM_HOSPITALINFO T
										WHERE  ROWNUM = 1";
        static string ftpSelect = @"SELECT T.Number_Value
										FROM   COM_HOSPITALINFO T
										WHERE  ROWNUM = 1";
        #endregion

        #region Login Log

        /// <summary>
        /// ϵͳ��¼ID
        /// </summary>
        static string LoginSessionID = "NULL";

        #endregion

        /// <summary>
        /// ����汾��
        /// </summary>
        static string VersionNumber = "20070101";

        /// <summary>
        /// ��Ϣ��ʾ�ӿ�
        /// </summary>
        private static System.Collections.Hashtable hsNoticeInterface = null;

        /// <summary>
        /// ������Ϣ��ʾ�ӿڼ���
        /// 
        /// {5BE03DF2-25DE-4e7a-9B47-85CE92911277}  ��������
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
                            //{89381D97-6630-4fb5-8ACE-7CAF578207F5} �޶��ӿڵĶ���
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
        /// HISϵͳ���Ӻ���
        /// </summary>
        /// <param name="employee">��½��Ա��Ϣ</param>
        /// <returns>�޴��󷵻�1 ������-1</returns>
        public static int HISMonitor()
        {
            #region �汾��½

            //{89381D97-6630-4fb5-8ACE-7CAF578207F5} ���ӶԷ���ֵ�Ĵ���
            if (JudgeVersionNumber() == -1)
            {
                return -1;
            }

            #endregion

            #region �Զ�����

            //{5BE03DF2-25DE-4e7a-9B47-85CE92911277}  �޸�ԭ�ӿڻ�ȡ��ʽ
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
                            //{89381D97-6630-4fb5-8ACE-7CAF578207F5} ���ӶԷ���ֵ�Ĵ���
                            if (noticeInterface.Notice() == -1)
                            {
                                return -1;
                            }
                            //{89381D97-6630-4fb5-8ACE-7CAF578207F5} ���ӶԷ���ֵ�Ĵ���
                            if (noticeInterface.Warn() == -1)
                            {
                                return -1;
                            }
                            //{89381D97-6630-4fb5-8ACE-7CAF578207F5} ���ӶԷ���ֵ�Ĵ���
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

            #region ��ʾ��Ϣ

            ShowNotice();

            #endregion

            #region ����Ա��½Log

            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            FS.HISFC.Models.Base.Employee oper = (FS.HISFC.Models.Base.Employee)dataManager.Operator;
            SaveLoginInfo(oper);

            NoteLog(oper, true, true, true, 1024);

            #endregion
           
            return 1;
        }

        /// <summary>
        /// HISϵͳ���Ӻ���
        /// 
        /// {5BE03DF2-25DE-4e7a-9B47-85CE92911277} ����HISϵͳע��ʱ�ĺ���
        /// </summary>
        /// <returns>�޴��󷵻�1 ������-1</returns>
        public static int HISLogout()
        {
            #region �Զ�����

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

            //ϵͳע�� {DEA84BD8-882A-440c-AF5B-3C244D16211D}
            SaveLogoutInfo();

            return 1;
        }

        /// <summary>
        /// �жϳ���汾�Ƿ�Ϊ���°汾
        /// </summary>
        /// <returns>�°汾����1 �ɰ汾����-1</returns>
        public static int JudgeVersionNumber()
        {
            FS.FrameWork.Xml.XML xmlManager = new FS.FrameWork.Xml.XML();
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            System.Xml.XmlNode root;

            #region ��ȡ���������ļ���HISϵͳ�汾

            string hosVersionNumber = "";
            //string hosFilePath = Application.StartupPath + "\\HIS.config";
            //if (System.IO.File.Exists(hosFilePath))			//���������ļ�
            //{
                //doc.Load(hosFilePath);
                //root = doc.SelectSingleNode("//VersionNumber");
                hosVersionNumber = System.Configuration.ConfigurationManager.AppSettings["VersionNumber"];
                
                if (string.IsNullOrEmpty(hosVersionNumber))
                {
                    MessageBox.Show("��ȡ����XML�����ļ�ʱ δ�ҵ��汾�Ž��");
                    return -1;
                }
                //hosVersionNumber = obj.ToString(); //root.Attributes[0].Value.ToString();
            //}
            //else
            //{
            //    #region �Զ�����

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

            //��ȡ���Ʋ��� ���δ�ҵ���Ӧ���Ʋ��������Ϊ0 �����а汾У��
            FS.FrameWork.Management.ControlParam controlerManagment = new FS.FrameWork.Management.ControlParam();

            string control = controlerManagment.QueryControlerInfo(FS.HISFC.BizProcess.Integrate.SysConst.Use_Judge_VersionNum);
            if (control == null || control == "0" || control == "-1")
            {
                return 1;
            }

            #region ��ȡ�������汾��

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
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ǰ���õ���Ϣ����ϵͳ�����°汾 ������ϵͳ��������������Ϣ����ϵ"));
                return -1;
            }

            return 1;
        }

        #region ϵͳ��¼��־

        /// <summary>
        /// �������Ա��½��Ϣ
        /// 
        /// {DEA84BD8-882A-440c-AF5B-3C244D16211D}
        /// </summary>
        public static int SaveLoginInfo(FS.HISFC.Models.Base.Employee oper)
        {
            if (LoginSessionID == "NULL")       //�״ε�½
            {
                //{70BC3C2C-823D-4c56-A06A-3E584EA13B2B}  ��ȡ��ǰ��¼IP����ʾ�ڽ�����
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
        /// �������Աע����Ϣ
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
        /// ��¼debugsql.log �� err.log ��־�ļ���С��Ϣ ���Դ���ɾ����׼���ļ�����ɾ��
        /// </summary>
        /// <param name="var">��½��Ϣ</param>
        /// <param name="isUpdate">���Ѳ������Ϣ�Ƿ���и��²���</param>
        /// <param name="delSqlLog">�Ƿ��debugSql.log����ɾ��</param>
        /// <param name="delErrLog">�Ƿ��err.log����ɾ��</param>
        /// <param name="delSize">��ɾ���ļ���С��׼ ��KΪ��λ</param>
        public static int NoteLog(FS.HISFC.Models.Base.Employee oper, bool isUpdate, bool delSqlLog, bool delErrLog, int delSize)
        {
            if (oper != null && oper.ID != "")
            {
                string sql = "";
                int parm = -1;

                //{70BC3C2C-823D-4c56-A06A-3E584EA13B2B}  ��ȡ��ǰ��¼IP����ʾ�ڽ�����
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
                    //AddressFamily ���� IPv4������ InterNetwork������ IPv6������ InterNetworkV6
                    else if (info.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(firstIP))
                    {
                        firstIP = info.ToString();
                    }

                    PingReply pr = userping.Send(info.ToString());
                    if (pr.Status == IPStatus.Success)//����������ö��״̬���Լ�ȥ�� 
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

                #region ���ݿ��¼

                //�Զ��Ʒѳ����־
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
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ��HISTIMEJOB�ļ�ʧ�� ������Ϣ����ϵ" + ex.Message));
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
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ɾ��HISTIMEJOB�ļ�ʧ�� ������Ϣ����ϵ" + ex.Message));
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
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ļ���Ϣ�����ݽ��в���ʧ��\n" + dataBase.Err));
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
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ļ���Ϣ�����ݽ��и���ʧ��\n" + dataBase.Err));
                    return -1;
                }
                //FS.FrameWork.Management.PublicTrans.Commit();;

                #endregion
            }

            return 1;
        }

        #endregion

        /// <summary>
        /// ��������ʾ������Ϣ
        /// </summary>
        /// <param name="alAddNotice">���ӵķ�����Ϣ</param>
        public static void ShowNotice(ArrayList alAddNotice)
        {
            //���� using �������ھ�̬������
            using (Manager.Notice.frmNoticeManager frm = new Manager.Notice.frmNoticeManager())
            {
                frm.OnlyShow = true;
                frm.ShowData(alAddNotice);
                if (frm.NoticeInfo.Count > 0)
                    frm.ShowDialog();
            }
        }

        /// <summary>
        /// ��������ʾ������Ϣ
        /// </summary>
        public static void ShowNotice()
        {
            ShowNotice(null);
        }

        #endregion

        #region {619F3CBF-7954-4d5e-B815-C66987E15C60}   ��λ���

        public static decimal LicenceBedQty = -1;

        /// <summary>
        /// ��λУ��
        /// </summary>
        /// <returns>True У��ͨ�� False  У��ʧ��</returns>
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
                    MessageBox.Show( "ά���ܴ�λ���ѳ���ϵͳ��Ȩ���ƣ����빩Ӧ����ϵ  \n\n��Ȩ��λ����" + LicenceBedQty.ToString() + "  ��ǰ��λ����" + bedQty.ToString() ,"��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
