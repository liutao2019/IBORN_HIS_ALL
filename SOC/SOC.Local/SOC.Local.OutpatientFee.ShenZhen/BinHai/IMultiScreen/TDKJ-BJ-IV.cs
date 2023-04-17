using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.ShenZhen.BinHai.IMultiScreen
{
    class TDKJ_BJ_IV
    {
        /// <summary>
        /// 外屏报价显示接口
        /// </summary>
        /// <param name="Comport"></param>
        /// <param name="Outstring"></param>
        /// <returns></returns>
        [DllImport("Tdbjq.dll", EntryPoint = "dsbdll")]
        private static extern int dsbdll(int Comport, string Outstring);

        /// <summary>
        /// ini文件路径
        /// </summary>
        public static string FilePath = Application.StartupPath + "\\setting.ini";//ini文件路径

        /// <summary>
        /// 串口端口映射
        /// </summary>
        public static string port = string.Empty;
        /// <summary>
        /// 首行显示的礼貌用语
        /// </summary>
        public static string text = string.Empty;

        /// <summary>
        /// 使用串口号 
        /// </summary>
        private static int Comport = 0;

        /// <summary>
        /// 清屏命令
        /// </summary>
        public const string ClearScreen = "&Sc$";

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="Outstring"></param>
        /// <returns></returns>
        public static int ShowInfo(string Outstring)
        {
            ReadSetting();
            Comport = Convert.ToInt32(port);
            return TDKJ_BJ_IV.dsbdll(TDKJ_BJ_IV.Comport, Outstring);
        }

        /// <summary>
        /// 获取设置信息
        /// </summary>
        /// <returns></returns>
        public static int ReadSetting()
        {
            string section = "Setting";
            string ident = "YsComPort";//串口端口映射
            string str = "TopLineText";//首行显示的礼貌用语
            FS.SOC.Public.Ini.IniFilesUtil objIni = new FS.SOC.Public.Ini.IniFilesUtil(FilePath);
            port = objIni.ReadString(section, ident, "");
            text = objIni.ReadString(section, str, "");
            return 1;
        }
        /// <summary>
        /// 显示及语音　J 请付款：****.**元  Y 收您：****.**元 Z 找您：****.**元
        /// </summary>
        public static int SayMoney(string str)
        {
            return TDKJ_BJ_IV.dsbdll(TDKJ_BJ_IV.Comport, str);
        }

        /// <summary>
        /// 显示医院信息
        /// </summary>
        public static int ShowHospital()
        {
            string[] tt = text.Split('|');
            string str = "&C21" + tt[0] + "$";
            return TDKJ_BJ_IV.dsbdll(TDKJ_BJ_IV.Comport, str);
        }

        /// <summary>
        /// 显示欢迎信息
        /// </summary>
        public static int ShowWelCome()
        {
            string[] tt = text.Split('|');
            string str = string.Empty;
            if (tt.Length > 1)
            {  str = "&C33" + tt[1].Substring(0, 6) + "$"; }
            return TDKJ_BJ_IV.dsbdll(TDKJ_BJ_IV.Comport, str);
        }
    }
}
