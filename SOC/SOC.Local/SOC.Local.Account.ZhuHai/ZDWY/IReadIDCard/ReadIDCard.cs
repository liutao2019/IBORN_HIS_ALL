using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using System.Globalization;
using System.Security.Cryptography;
using System.Collections;

//F4AEBA6C-370E-4855-B15E-F87C17A10BEC
namespace FS.SOC.Local.Account.ZhuHai.ZDWY.IReadIDCard
{
    public enum s
    {
        汉族 = 1,
        蒙古族,
        回族,
        藏族,
        维吾尔族,
        苗族,
        彝族,
        壮族,
        布依族,
        朝鲜族,
        满族,
        侗族,
        瑶族,
        白族,
        土家族,
        哈尼族,
        哈萨克族,
        傣族,
        黎族,
        傈僳族,
        佤族,
        畲族,
        高山族,
        拉祜族,
        水族,
        东乡族,
        纳西族,
        景颇族,
        柯尔克孜族,
        土族,
        达斡尔族,
        仫佬族,
        羌族,
        布朗族,
        撒拉族,
        毛南族,
        仡佬族,
        锡伯族,
        阿昌族,
        普米族,
        塔吉克族,
        怒族,
        乌孜别克族,
        俄罗斯族,
        鄂温克族,
        德昂族,
        保安族,
        裕固族,
        京族,
        塔塔尔族,
        独龙族,
        鄂伦春族,
        赫哲族,
        门巴族,
        珞巴族,
        基诺族,
        其他,
        外国血统
    }


    public class ReadIDCard : FS.HISFC.BizProcess.Interface.Account.IReadIDCard
    {
        #region 
        /*
        #region 加载射频卡DLL中的方法
        //取当前设备端口
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int PiccMemGetPort(ref int iCom);

        //打卡COM口
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int PiccMemOpenCOM(int iCom);

        //关闭COM口
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int PiccMemCloseCOM();

        //射频卡激活
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int PiccMemActive(ushort usDelayTimes, ref byte ucCardType, byte[] UID);

        //射频卡扇区认证
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int PiccMemAuthSector(byte ucSectorNo, byte ucKeyType, byte[] Key);

        //写块
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int PiccMemWriteBlock(byte ucBlockNo, byte[] BlockData);


        //读块
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int PiccMemReadBlock(byte ucBlockNo, byte[] BlockData);


        //射频卡停用
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int PiccMemHalt();

        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int PiccMemReadRecord(byte ucBlockNo, byte[] BlockData, int ReadLen);

        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int PiccMemWriteRecord(byte ucBlockNo, byte[] BlockData, int WriteLen);


        #endregion

        #region 加载读身份证DLL中的方法
        //打开COM口
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int SDT_OpenPort(int iPort);//1001 USB,1-16 com口

        //找卡
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int SDT_StartFindIDCard(int iPort, ref byte[] pucManaInfo, int iIfOpen);

        //选卡
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern int SDT_SelectIDCard(int iPort, ref byte[] pucManaInfo, int iIfOpen);

        //读取
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SDT_ReadBaseMsg(int iPort, byte[] pucCHMsg, ref uint puiCHMsgLen,
             byte[] pucPHMsg, ref uint puiPHMsgLen, int iIfOpen);

        //关闭COM口
        [DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        private static extern int SDT_ClosePort(int iPort);


        #endregion

        #region IReadIDCard 成员

        public int GetIDInfo(ref string IDCode, ref string Name, ref string Sex, ref DateTime BirthDay, ref string Nation, ref string Adress, ref string Agency, ref DateTime ExpireStart, ref DateTime ExpireEnd, ref string PhotoFileName, ref string Message)
        {
            int nResult = 0;
            int iPortId = 0;

            int iIfOpen = 0;//标识
            bool flag = false;//记录COM口是否打开
            int result = 1;//记录结果
            byte[] pucManaInfo = new byte[4];
            byte[] pucManaMsg = new byte[8];
            byte[] pucCHMsg = new byte[512]; //文字信息
            byte[] pucPHMsg = new byte[1024]; //照片信息
            byte[] pucFPMsg = new byte[1024]; //指纹信息
            uint uiCHMsgLen = 0;
            uint uiPHMsgLen = 0;

            try
            {


                nResult = PiccMemGetPort(ref iPortId);

                if (nResult == -1)
                {
                    Message = "取当前设备端口失败";
                    return -1;
                }
                //iPort = "COM" + iPortId.ToString();
                //try
                //{
                //    SerialPort myserialPort = new SerialPort();
                //    myserialPort.PortName = iPort; //定义为COM1
                //    myserialPort.BaudRate = 9600; //波特率为9600
                //    myserialPort.Open();
                //    byte[] b = { 0x1B, 0x25, 0x53, 0x31 };
                //    myserialPort.Write(b, 0, 4);
                //    myserialPort.Close();
                //}
                //catch
                //{
                //    Msg = "接口参数不正确";
                //    return -1;
                //}

                result = SDT_OpenPort(iPortId);//打开COM
                if (result != 0x90)
                {
                    result = -1;
                    Message = "端口打开错误";
                    return result;
                }
                else//成功
                {
                    flag = true;
                    bool findflag = true;
                    #region 循环找卡
                    for (int i = 0; i < 10; i++)
                    {
                        result = SDT_StartFindIDCard(iPortId, ref pucManaInfo, iIfOpen);
                        if (result == 0x9f)
                        {
                            findflag = true;
                            break;
                        }
                    }
                    #endregion

                    if (!findflag)
                    {
                        result = -1;
                        Message = "没找到身份证";
                        return result;
                    }
                    #region 选卡
                    result = SDT_SelectIDCard(iPortId, ref pucManaMsg, iIfOpen);
                    if (result == 0x90)//选卡成功
                    {
                        #region 读卡
                        result = SDT_ReadBaseMsg(iPortId, pucCHMsg, ref uiCHMsgLen, pucPHMsg, ref uiPHMsgLen, iIfOpen);
                        if (result == 0x90)
                        {
                            string userMsg = Encoding.Unicode.GetString(pucCHMsg);
                            IDCode = userMsg.Substring(61, 18);
                            Nation = ((s)Convert.ToInt32(userMsg.Substring(16, 2))).ToString();
                            Name = userMsg.Substring(0, 15).Trim();
                            Sex = userMsg.Substring(15, 1) == "1" ? "男" : "女";
                            Adress = userMsg.Substring(26, 35).Trim();
                            //  BirthDay = userMsg.Substring(18, 8);
                            result = 1;
                        }
                        else
                        {
                            Message = "读取身份证信息失败";
                            return -1;
                        }
                        #endregion
                    }
                    else
                    {
                        result = -1;
                        Message = "选卡失败";
                        return result;
                    }
                    #endregion
                }

            }
            catch (Exception ea)
            {
                result = -1;
                Message = ea.Message;

            }
            finally
            {
                if (flag)
                {
                    SDT_ClosePort(iPortId);//关闭com口 
                }
                //SerialPort myserialPort = new SerialPort();
                //myserialPort.PortName = iPort; //定义为COM1
                //myserialPort.BaudRate = 115200; //波特率为115200
                //myserialPort.Open();
                //byte[] CloseS = { 0x1B, 0x25, 0x42 };
                //myserialPort.Write(CloseS, 0, 3);
                //myserialPort.Close();

            }

            return result;
        }
        */
#endregion


        #region 读取身份证
        [DllImport("sdtapi.dll")]
        protected static extern int InitComm(int iPort);


      
        [DllImport("sdtapi.dll")]
        protected static extern  int Authenticate ();

        //[DllImport("sdtapi.dll")]
        //protected static extern int SDT_SelectIDCard(int iPort, byte[] pucSN, int iIfOpen);


        //[DllImport("sdtapi.dll")]
        //protected static extern int ReadBaseMsgW(ref byte[] pucCHMsg, ref int puiCHMsgLen);

        [DllImport("sdtapi.dll")]
        private static extern int ReadBaseMsg(byte[] reCode, ref int len);
        [DllImport("sdtapi.dll")]
        private static extern int ReadBaseMsg(StringBuilder reCode, ref int len);


        [DllImport("sdtapi.dll")]
        private static extern int ReadIINSNDN(StringBuilder reCode);
 



        [DllImport("sdtapi.dll")]
        protected static extern int CloseComm();

        #endregion

        private static void ClosePort(int port)
        {
            try
            {
                CloseComm();
            }
            catch
            { }
        }

        /// <summary>
        /// 读取身份证
        /// </summary>
        /// <returns></returns>
        // public int GetIDInfo(ref string IDCode, ref string Name, ref string Sex, ref DateTime BirthDay, ref string Nation, ref string Adress, ref string Agency, ref DateTime ExpireStart, ref DateTime ExpireEnd, ref string PhotoFileName, ref string Message)
        //{
        //public int  ReadIDCard(ref string IdNo, ref string nameReadBaseMsg,ref string sex, ref DateTime birthday, ref string nation, ref string address,, , ref string error)
      //  public int GetIDInfo(ref string IDCode, ref string Name, ref string Sex, ref string BirthDay, ref string nation, ref string address, ref string Agency, ref DateTime ExpireStart, ref DateTime ExpireEnd, ref string PhotoFileName, ref string Message)

        public int GetIDInfo(ref string IDCode, ref string Name, ref string Sex, ref DateTime BirthDay, ref string nation, ref string address, ref string Agency, ref DateTime ExpireStart, ref DateTime ExpireEnd, ref string PhotoFileName, ref string Message)
        {

            //if (File.Exists(Environment.CurrentDirectory + @"\photo.bmp"))
            //{
            //    File.Delete(Environment.CurrentDirectory + @"\photo.bmp");
            //    //this.pictureBox1.ImageLocation = "";
            //}
            //int k = 0;
            //for (int i = 0x3e9; i <= 0x3f8; i++)
            //{
            //    k = InitComm(i);
            //    if (k == 1)
            //    {
            //        break;
            //    }
            //}
            //if (k == 1)
            //{
            // //   this.label1.Text = "初始化读卡器成功";
            //    StringBuilder sb = new StringBuilder(0x400);
            //    byte[] bytes = new byte[0x7de];
            //    int len = 0;
            //    if (ReadBaseMsg(sb, ref len) == 1)
            //    {
            //        string msg = sb.ToString();
            //         string ds="";

            //         ds = "身份证信息：\n\r\n\r姓名：" + Encoding.Default.GetString(bytes, 0, 0x1f);
            //         ds = ds + "\n\r性别：" + Encoding.Default.GetString(bytes, 0x1f, 3);
            //         ds = ds + "\n\r民族：" + Encoding.Default.GetString(bytes, 0x22, 10);
            //         ds = ds + "\n\r出生日期：" + Encoding.Default.GetString(bytes, 0x2c, 9);
            //         ds = ds + "\n\r住址：" + Encoding.Default.GetString(bytes, 0x35, 0x47);
            //         ds = ds + "\n\r身份证号码：" + Encoding.Default.GetString(bytes, 0x7c, 0x13);
            //         ds = ds + "\n\r签发机关：" + Encoding.Default.GetString(bytes, 0x8f, 0x1f);
            //         ds = ds + "\n\r有效起始日期：" + Encoding.Default.GetString(bytes, 0xae, 9);
            //         ds = ds + "\n\r有效截至日期：" + Encoding.Default.GetString(bytes, 0xb7, 9);
            //       // this.pictureBox1.ImageLocation = Environment.CurrentDirectory + @"\photo.bmp";
            //    }
            //    else
            //    {
            //       // MessageBox.Show("获取身份证信息失败");
            //    }
            //    CloseComm();
            //}
            //else
            //{
            // //   MessageBox.Show("初始化设备失败");
            //}
            //return 0;



            string error = "";
            Agency = "";
            ExpireStart = DateTime.Now;
            int port = 1001;
            byte[] pucIIN = new byte[0x7de];
            byte[] pucSN = new byte[0x7de];
            byte[] bytes = new byte[1024];
            byte[] pucPHMsg = new byte[0x7de];
            int uiCHMsgLen = 0;
            int uiPHMsgLen = 0;
            int result;

            try
            {
                for (int iPort = 1001; iPort <= 1016; iPort++)
                {
                    int intOpenPortRtn = InitComm(iPort);
                    if (intOpenPortRtn == 1)
                    {
                        port = iPort;
                        break;
                    }
                }

                ClosePort(port);
                result = InitComm(port);
                if (result != 1)
                {
                    error = "打开USB口失败";
                    ClosePort(port);
                    return -1;
                }

                StringBuilder sb = new StringBuilder();
                int j = ReadIINSNDN(sb);
                result = InitComm(port);
                if (result != 1)
                {
                    error = "打开USB口失败";
                    ClosePort(port);
                    return -1;
                }

                //result = Authenticate();
                //if (result != 1)
                //{
                //    result = Authenticate();
                //    if (result != 1)
                //    {
                //        ClosePort(port);
                //        error = "未放卡或者卡未放好，请重新放卡！";
                //        return -1;
                //    }
                //}

                //result = SDT_SelectIDCard(port, pucSN, 0);
                //if (result != 144)
                //{
                //    result = SDT_SelectIDCard(port, pucSN, 0);
                //    if (result != 144)
                //    {
                //        error = "此卡不是标准身份证";
                //        ClosePort(port);
                //        return -1;
                //    }

                //}
                int ddd = 0;
                try
                {
                   // StringBuilder sb = new StringBuilder(200);

                    result = ReadBaseMsg(bytes, ref ddd);
                }
                catch (Exception ex)
                {
                    ClosePort(port);

                    return -1;
                }
                if (result != 1)
                {
                    error = "读取身份证错误！";
                    ClosePort(port);
                    return -1;
                }

               // string messge = Encoding.Default.GetString(pucCHMsg);
                Name = Encoding.Default.GetString(bytes, 0, 0x1f).Replace("\0","");
                //messge = messge.Substring(31);

                //string sex = messge.Substring(0,3);
                //if (sex == "1")
                //{
                //    sex = "男";
                //}
                //else
                //{
                //    sex = "女";
                //}
                Sex = Encoding.Default.GetString(bytes, 0x1f, 3).Replace("\0", "");
              //  messge = messge.Substring(1);
                loadNation();
                nation = Encoding.Default.GetString(bytes, 0x22, 10).Replace("\0", "") + "族";
                //if (lstMZ.ContainsKey(nation))
                //{
                //    nation = lstMZ[nation].ToString();
                //}
                //else
                //{
                //    nation = "未知名族";
                //}

               // messge = messge.Substring(2);

                string birthday = Encoding.Default.GetString(bytes, 0x2c, 9).Replace("\0", "");

                BirthDay = new DateTime(
                    int.Parse(birthday.Substring(0, 4))
                    , int.Parse(birthday.Substring(4, 2))
                    , int.Parse(birthday.Substring(6, 2))

                    );
               // messge = messge.Substring(8);

                address = Encoding.Default.GetString(bytes, 0x35, 0x47).Replace("\0", "");
               // messge = messge.Substring(35);

                IDCode = Encoding.Default.GetString(bytes, 0x7c, 0x13).Replace("\0", "");
              //  messge = messge.Substring(18);

              //  messge = messge.Substring(15);

             //   messge = messge.Substring(8);
//
          //      messge = messge.Substring(8);

                ClosePort(port);
                return 0;
            }
            catch
            {
                ClosePort(port);
                return -1;
            }

        }


        private static Hashtable lstMZ = new Hashtable();

        public static void loadNation()
        {
            lstMZ.Clear();
            lstMZ.Add("01", "汉族");
            lstMZ.Add("02", "蒙古族");
            lstMZ.Add("03", "回族");
            lstMZ.Add("04", "藏族");
            lstMZ.Add("05", "维吾尔族");
            lstMZ.Add("06", "苗族");
            lstMZ.Add("07", "彝族");
            lstMZ.Add("08", "壮族");
            lstMZ.Add("09", "布依族");
            lstMZ.Add("10", "朝鲜族");
            lstMZ.Add("11", "满族");
            lstMZ.Add("12", "侗族");
            lstMZ.Add("13", "瑶族");
            lstMZ.Add("14", "白族");
            lstMZ.Add("15", "土家族");
            lstMZ.Add("16", "哈尼族");
            lstMZ.Add("17", "哈萨克族");
            lstMZ.Add("18", "傣族");
            lstMZ.Add("19", "黎族");
            lstMZ.Add("20", "傈僳族");
            lstMZ.Add("21", "佤族");
            lstMZ.Add("22", "畲族");
            lstMZ.Add("23", "高山族");
            lstMZ.Add("24", "拉祜族");
            lstMZ.Add("25", "水族");
            lstMZ.Add("26", "东乡族");
            lstMZ.Add("27", "纳西族");
            lstMZ.Add("28", "景颇族");
            lstMZ.Add("29", "柯尔克孜族");
            lstMZ.Add("30", "土族");
            lstMZ.Add("31", "达翰尔族");
            lstMZ.Add("32", "仫佬族");
            lstMZ.Add("33", "羌族");
            lstMZ.Add("34", "布朗族");
            lstMZ.Add("35", "撒拉族");
            lstMZ.Add("36", "毛南族");
            lstMZ.Add("37", "仡佬族");
            lstMZ.Add("38", "锡伯族");
            lstMZ.Add("39", "阿昌族");
            lstMZ.Add("40", "普米族");
            lstMZ.Add("41", "塔吉克族");
            lstMZ.Add("42", "怒族");
            lstMZ.Add("43", "乌孜别克族");
            lstMZ.Add("44", "俄罗斯族");
            lstMZ.Add("45", "鄂温克族");
            lstMZ.Add("46", "德昂族");
            lstMZ.Add("47", "保安族");
            lstMZ.Add("48", "裕固族");
            lstMZ.Add("49", "京族");
            lstMZ.Add("50", "塔塔尔族");
            lstMZ.Add("51", "独龙族");
            lstMZ.Add("52", "鄂伦春族");
            lstMZ.Add("53", "赫哲族");
            lstMZ.Add("54", "门巴族");
            lstMZ.Add("55", "珞巴族");
            lstMZ.Add("56", "基诺族");
            lstMZ.Add("57", "其它");
            lstMZ.Add("98", "外国人入籍");
        }


      


    }
}
       