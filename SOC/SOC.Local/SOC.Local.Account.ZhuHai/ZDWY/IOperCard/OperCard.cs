using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;

//36D05C87-8E9B-4f63-8E9A-77BF1C0D18A1
namespace FS.SOC.Local.Account.ZhuHai.ZDWY.IOperCard
{
    public class OperCard:FS.HISFC.BizProcess.Interface.Account.IOperCard
    {

        #region 加载射频卡DLL中的方法 作废
        /*
        ////取当前设备端口
        //[DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        //private static extern int PiccMemGetPort(ref int iCom);

        ////打卡COM口
        //[DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        //private static extern int PiccMemOpenCOM(int iCom);

        ////关闭COM口
        //[DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        //private static extern int PiccMemCloseCOM();

        ////射频卡激活
        //[DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        //private static extern int PiccMemActive(ushort usDelayTimes, ref byte ucCardType, byte[] UID);

        ////射频卡扇区认证
        //[DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        //private static extern int PiccMemAuthSector(byte ucSectorNo, byte ucKeyType, byte[] Key);

        ////写块
        //[DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        //private static extern int PiccMemWriteBlock(byte ucBlockNo, byte[] BlockData);


        ////读块
        //[DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        //private static extern int PiccMemReadBlock(byte ucBlockNo, byte[] BlockData);


        ////射频卡停用
        //[DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        //private static extern int PiccMemHalt();

        //[DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        //private static extern int PiccMemReadRecord(byte ucBlockNo, byte[] BlockData, int ReadLen);

        //[DllImport("\\MarkDll\\GWIPICC.dll", CharSet = CharSet.Auto)]
        //private static extern int PiccMemWriteRecord(byte ucBlockNo, byte[] BlockData, int WriteLen);
     #region 写入病历号
        /// <summary>
        /// 写入病历号
        /// </summary>
        /// <param name="WriteMsg">要写入的信息</param>
        /// <param name="Msg">错误信息</param>
        /// <returns>-1:失败,0:成功</returns>
        public int WriterCardNO(string CardNo, ref string Msg)
        {
        
            string Key="ffffffffffff";
            int sBlockNo=2;
            int sSectorNo=0;

            string iPort = "";
            int nResult = 0;

            int iPortId = 0;

            bool openComFlage = false;
            try
            {
               
                nResult = PiccMemGetPort(ref iPortId);

                if (nResult == -1)
                {
                    Msg = "取当前设备端口失败";
                    return -1;
                }
                iPort = "COM" + iPortId.ToString();
                nResult = PiccMemOpenCOM(iPortId);

                if (nResult == -1)
                {
                    Msg = "接口打开失败";
                    return -1;
                }

                openComFlage = true;

                //激活操作功能
                ushort usDelayTimes = 0x00;
                byte ucCardType = 0;
                byte[] UID = new byte[4];
                nResult = PiccMemActive(usDelayTimes, ref ucCardType, UID);

                if (nResult != 0)
                {
                    Msg = "卡类型错误";
                    return -1;
                }
                else
                {
                    byte ucSectorNo = Convert.ToByte(sSectorNo);
                    byte ucKeyType = 0x60;
                    // byte[] Key = { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

                    byte[] HxKey = new byte[Key.Length / 2];
                    for (int i = 0; i < Key.Length; )
                    {
                        int n = (Int32.Parse(Key.Substring(i, 1), NumberStyles.AllowHexSpecifier) * 16 + Int32.Parse(Key.Substring(i + 1, 1), NumberStyles.AllowHexSpecifier));
                        //int n = Convert.ToInt32(sInput.Substring(i, 2), 16);
                        HxKey[i / 2] = (byte)n;
                        i = i + 2;
                    }


                    nResult = PiccMemAuthSector(ucSectorNo, ucKeyType, HxKey);
                    if (nResult >= 0)
                    {
                        byte ucBlockNo = Convert.ToByte(sBlockNo);
                        CardNo = CardNo.PadRight(32, 'E');
                        byte[] w = Encoding.ASCII.GetBytes(CardNo);
                        //nResult = PiccMemWriteBlock(ucBlockNo, w);
                        nResult = PiccMemWriteRecord(ucBlockNo, w, 32);

                        if (nResult == -1)
                        {
                            Msg = "写卡失败";
                            return -1;
                        }
                    }
                    else
                    {
                        Msg = "认证区密码错误";
                        return -1;
                    }
                }

            }
            catch (Exception err)
            {
                nResult = -1;
                Msg = err.Message;
            }
            finally
            {
                //PiccMemHalt();
                if (openComFlage)
                {
                    PiccMemHalt();
                    PiccMemCloseCOM();
                }
            }
            return nResult;
        }
        #endregion 

        #region 获取病历号
        /// <summary>
        /// 获取卡病历号
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="sSectorNo">射频卡扇区</param>
        /// <param name="sBlockNo">射频卡块区</param>
        /// <param name="Key">射频卡扇区认证密码</param>
        /// <param name="CardNo">卡号</param>
        /// <param name="Msg">错误信息</param>
        /// <returns>-1:失败,0:成功</returns>
        public int ReadCardNO(ref string CardNo, ref string Msg)
        {
            int sSectorNo=0;
            int sBlockNo=2;
            string Key = "ffffffffffff";
            int nResult = 0;

            int iPortId = 0;
            string iPort = "";

            bool openComFlage = false;
            try
            {
                //取当前设备端口
                nResult = PiccMemGetPort(ref iPortId);

                if (nResult == -1)
                {
                    Msg = "取当前设备端口失败";
                    return -1;
                }

                nResult = PiccMemOpenCOM(iPortId);

                if (nResult == -1)
                {
                    Msg = "接口打开失败";
                    return -1;
                }

                openComFlage = true;

                //激活操作功能
                ushort usDelayTimes = 0x00;
                byte ucCardType = 0;
                byte[] UID = new byte[4];
                nResult = PiccMemActive(usDelayTimes, ref ucCardType, UID);

                if ((nResult != 0) || (ucCardType == 0x0b))
                {
                    if (ucCardType == 0x0b)
                    {
                        PiccMemHalt();
                    }
                    PiccMemCloseCOM();
                    nResult = ReadIDCardNo(iPort, ref  CardNo, ref Msg);
                    openComFlage = false;
                    return nResult;
                }
                else
                {


                    byte ucSectorNo = Convert.ToByte(sSectorNo);
                    byte ucKeyType = 0x60;
                    // byte[] Key = { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

                    byte[] HxKey = new byte[Key.Length / 2];
                    for (int i = 0; i < Key.Length; )
                    {
                        int n = (Int32.Parse(Key.Substring(i, 1), NumberStyles.AllowHexSpecifier) * 16 + Int32.Parse(Key.Substring(i + 1, 1), NumberStyles.AllowHexSpecifier));
                        //int n = Convert.ToInt32(sInput.Substring(i, 2), 16);
                        HxKey[i / 2] = (byte)n;
                        i = i + 2;
                    }


                    nResult = PiccMemAuthSector(ucSectorNo, ucKeyType, HxKey);
                    if (nResult >= 0)
                    {
                        byte ucBlockNo = Convert.ToByte(sBlockNo);
                        byte[] sb = new byte[32];
                        //nResult = PiccMemReadBlock(ucBlockNo, sb);
                        nResult = PiccMemReadRecord(ucBlockNo, sb, 32);
                        if (nResult >= 0)
                        {
                            CardNo = Encoding.ASCII.GetString(sb);
                            CardNo = CardNo.IndexOf('E') > 0 ? CardNo.Substring(0, CardNo.IndexOf('E')) : CardNo;
                            CardNo = CardNo.Replace("\0", "");
                            CardNo = CardNo.Substring(0, 10);
                            if (CardNo == "0000000000")
                            {
                               // Msg = "该卡还未发放！";
                               // return -1;
                            }

                            return 1;
                        }
                    }
                    else
                    {
                        Msg = "认证区密码错误";
                        return -1;
                    }
                }

            }
            catch (Exception err)
            {
                nResult = -1;
                Msg = err.Message;
            }
            finally
            {
                //PiccMemHalt();
                if (openComFlage)
                {
                    PiccMemHalt();
                    PiccMemCloseCOM();
                }
            }
            return nResult;
        }
        #endregion 

        #region 获取卡号
        /// <summary>
        /// 获取卡号
        /// </summary>
        /// <param name="iPort">端口号</param>
        /// <param name="sSectorNo">射频卡扇区</param>
        /// <param name="sBlockNo">射频卡块区</param>
        /// <param name="Key">射频卡扇区认证密码</param>
        /// <param name="CardNo">卡号</param>
        /// <param name="Msg">错误信息</param>
        /// <returns>-1:失败,0:成功</returns>
        public int ReadMCardNO(ref string MCardNo, ref string Msg)
        {
            int nResult = 0;
            int iPortId = 0;
            bool openComFlage = false;

            string iPort="COM2";
            int sSectorNo=0;
            int sBlockNo=1;
            string Key = "ffffffffffff";

            try
            {
                try
                {
                    iPortId = Convert.ToInt32(iPort.ToUpper().Replace("COM", ""));
                }
                catch
                {
                    Msg = "接口参数不正确";
                    return -1;
                }
                nResult = PiccMemGetPort(ref iPortId);
                if (nResult == -1)
                {
                    Msg = "取当前设备端口失败";
                    return -1;
                }
                nResult = PiccMemOpenCOM(iPortId);
                if (nResult == -1)
                {
                    Msg = "接口打开失败";
                    return -1;
                }
                openComFlage = true;
                ushort usDelayTimes = 0;
                byte ucCardType = 0;
                byte[] UID = new byte[4];
                nResult = PiccMemActive(usDelayTimes, ref ucCardType, UID);
                if ((nResult != 0) || (ucCardType == 11))
                {
                    if (ucCardType == 11)
                    {
                        PiccMemHalt();
                    }
                    PiccMemCloseCOM();
                    nResult = this.ReadIDCardNo(iPort, ref MCardNo, ref Msg);
                    openComFlage = false;
                    return nResult;
                }
                byte ucSectorNo = Convert.ToByte(sSectorNo);
                byte ucKeyType = 0x60;
                byte[] HxKey = new byte[Key.Length / 2];
                for (int i = 0; i < Key.Length; i += 2)
                {
                    int n = (int.Parse(Key.Substring(i, 1), NumberStyles.AllowHexSpecifier) * 0x10) + int.Parse(Key.Substring(i + 1, 1), NumberStyles.AllowHexSpecifier);
                    HxKey[i / 2] = (byte)n;
                }
                nResult = PiccMemAuthSector(ucSectorNo, ucKeyType, HxKey);
                if (nResult >= 0)
                {
                    byte ucBlockNo = Convert.ToByte(sBlockNo);
                    byte[] sb = new byte[0x20];
                    nResult = PiccMemReadRecord(ucBlockNo, sb, 0x20);
                    if (nResult != 0)
                    {
                        return nResult;
                    }
                    MCardNo = Encoding.ASCII.GetString(sb);
                    MCardNo = (MCardNo.IndexOf("E") > 0) ? MCardNo.Substring(0, MCardNo.IndexOf("E")) : MCardNo;
                    MCardNo = MCardNo.Substring(0, 12);
                    return 1;
                }
                Msg = "认证区密码错误";
                return -1;
            }
            catch (Exception err)
            {
                nResult = -1;
                Msg = err.Message;
            }
            finally
            {
                if (openComFlage)
                {
                    PiccMemHalt();
                    PiccMemCloseCOM();
                }
            }
            return nResult;

        }
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

       

        #region 读卡
        private int ReadIDCardNo(string iPort, ref string CardNo, ref string Msg)
        {
            int nResult = 0;
            int iPortId = 0;
            int iIfOpen = 0;
            bool flag = false;
            int result = 0;
            byte[] pucManaInfo = new byte[4];
            byte[] pucManaMsg = new byte[8];
            byte[] pucCHMsg = new byte[0x200];
            byte[] pucPHMsg = new byte[0x400];
            uint uiCHMsgLen = 0;
            uint uiPHMsgLen = 0;
            try
            {
                nResult = PiccMemGetPort(ref iPortId);
                if (nResult == -1)
                {
                    Msg = "取当前设备端口失败";
                    return -1;
                }
                if (SDT_OpenPort(iPortId) != 0x90)
                {
                    result = -1;
                    Msg = "端口打开错误";
                    return result;
                }
                flag = true;
                bool findflag = true;
                for (int i = 0; i < 10; i++)
                {
                    if (SDT_StartFindIDCard(iPortId, ref pucManaInfo, iIfOpen) == 0x9f)
                    {
                        findflag = true;
                        break;
                    }
                }
                if (!findflag)
                {
                    result = -1;
                    Msg = "没找到身份证";
                    return result;
                }
                if (SDT_SelectIDCard(iPortId, ref pucManaMsg, iIfOpen) == 0x90)
                {
                    if (SDT_ReadBaseMsg(iPortId, pucCHMsg, ref uiCHMsgLen, pucPHMsg, ref uiPHMsgLen, iIfOpen) == 0x90)
                    {
                        CardNo = Encoding.Unicode.GetString(pucCHMsg).Substring(0x3d, 0x12);
                        return nResult;
                    }
                    Msg = "读取身份证信息失败";
                    return -1;
                }
                result = -1;
                Msg = "选卡失败";
                return result;
            }
            catch (Exception ea)
            {
                result = -1;
                Msg = ea.Message;
            }
            finally
            {
                if (flag)
                {
                    SDT_ClosePort(iPortId);
                }
            }
            return nResult;

        }
        #endregion

       
         */
        #endregion


        #region
        [DllImport("rfid.dll", EntryPoint = "LinkCard")]
        protected static extern bool LinkCard();
        [DllImport("rfid.dll")]
        public static extern int HexReadCardID(byte[] CardNo);
        [DllImport("rfid.dll", EntryPoint = "UnlinkCard")]
        protected static extern bool UnlinkCard();

        [DllImport("rfid.dll", EntryPoint = "ReadCardID")]
        protected static extern int ReadCardID(byte[] sCardId);

        [DllImport("rfid.dll", EntryPoint = "WriteCardData")]
        protected static extern int WriteCardData(int nBlock, byte[] sData, int sPassType, string sPassWord);

        [DllImport("rfid.dll", EntryPoint = "HexWriteCardData")]
        protected static extern int HexWriteCardData(int nBlock, string sData, int sPassType, string sPassWord);

        [DllImport("rfid.dll", EntryPoint = "HexReadCardData")]
        protected static extern int HexReadCardData(byte[] sData, int nBlock, int sPassType, string sPassWord);

        [DllImport("rfid.dll", EntryPoint = "ReadCardData")]
        protected static extern bool ReadCardData(byte[] sData, int nBlock, int sPassType, string sPassWord);

        [DllImport("rfid.dll", EntryPoint = "GetErr")]
        protected static extern void GetErr(StringBuilder ErrStr);
        #endregion 

        #region 读卡器操作

        public bool LinkCard(StringBuilder errMsg)
        {
            try
            {
                bool flag = LinkCard();
                if (flag == true)
                {

                }
                else
                {
                    GetErr(errMsg);
                }

                return flag;
            }
            catch (Exception ex)
            {
                errMsg.Append(ex.Message);
                return false;
            }
        }
        public bool UnlinkCard(StringBuilder errMsg)
        {
            try
            {
                bool flag = UnlinkCard();
                if (flag == true)
                {

                }
                else
                {
                    GetErr(errMsg);
                }

                return flag;
            }
            catch (Exception ex)
            {
                errMsg.Append(ex.Message);
                return false;
            }
        }

        private static int WriteCardData1(int nBlock, string sData, int sPassType, string sPassWord, StringBuilder errMsg)
        {
            try
            {
                int recode = 0;

                bool re = LinkCard();
                if (re == true)
                {
                    string pass = "";

                    for (int i = 0; i < sPassWord.Length; i += 2)
                    {
                        pass += " " + sPassWord.Substring(i, 2);
                    }

                    byte[] data = new byte[128];
                    for (int i = 0; i < sData.Length / 2; i++)
                    {
                        int c = Int32.Parse(sData.Substring(i * 2, 2));
                        string da = (c / 10 * 16 + c % 10).ToString();
                        data[i] = byte.Parse(da);
                    }

                    //byte[] data = new byte[128]; 
                    //for (int i = 0; i < sData.Length / 2; i++)
                    //{
                    //    int c=Int32.Parse(sData.Substring(i*2,2));
                    //    string da = (c / 10 * 16 + c % 10).ToString();
                    //    data[i] = byte.Parse(da);
                    //}


                    recode = WriteCardData(nBlock, data, sPassType, pass);

                    if (recode < 0)
                    {
                        GetErr(errMsg);
                    }
                }
                else
                {
                    GetErr(errMsg);
                }
                UnlinkCard();
                return recode;
            }
            catch (Exception ex)
            {
                errMsg.Append(ex.Message);
                UnlinkCard();
                return -1;
            }
        }

        public int WriterCardNO(int sec,string CardNo, ref string Msg)
        {
            Msg = "";
            return WriteCardNo(CardNo);
        }


        private static int ReadCardData1(StringBuilder sData, int nBlock, int sPassType, string sPassWord, StringBuilder errMsg)
        {
            try
            {
                int recode = 0;
                //  sData = new StringBuilder();
                errMsg = new StringBuilder();

                bool re = LinkCard();
                byte[] linkData = new byte[128];
                int rre = HexReadCardID(linkData);
                if (re == true&&rre>0)
                {
                    byte[] data = new byte[128];
                    string pass = "";
                    for (int i = 0; i < sPassWord.Length; i += 2)
                    {
                        pass += " " + sPassWord.Substring(i, 2);
                    }
                    bool flag = ReadCardData(data, nBlock, sPassType, pass);

                    if (flag == true)
                    {

                        string str = BitConverter.ToString(data).Replace("-", "");
                        //string str = System.Text.Encoding.Default.GetString(data).Replace("-","");
                        if (nBlock == 0)
                        {
                            sData.Append(str.Substring(0, 32));
                        }
                        else if (nBlock == 1)
                        {
                            //if (str.Substring(10, 10) == "0000000000") //诊疗卡
                            //{
                            //    sData.Append(str.Substring(0, 8));
                            //}
                            //else
                            //{
                            //    sData.Append(str.Substring(1, 19));
                            //}
                            sData.Append(str.Substring(0, 12));
                        }
                        else if (nBlock ==2)
                        {
                            sData.Append(str.Substring(0, 10));
                            //sData.Append(str);
                        }

                        recode = 1;

                    }
                    else
                    {
                        GetErr(errMsg);
                        UnlinkCard();
                        recode = -1;
                    }
                }
                else
                {
                    GetErr(errMsg);
                    UnlinkCard();
                    recode = -1;
                }

                UnlinkCard();
                return recode;
            }
            catch (Exception ex)
            {
                sData = new StringBuilder();
                errMsg.Append(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 读取卡号 安全吗
        /// </summary>
        /// <param name="MCard"></param>
        /// <param name="Security"></param>
        /// <returns></returns>
        public static int ReadMCard(ref string MCard, ref string Security)
        {
            StringBuilder MCard1 = new StringBuilder();
            StringBuilder errMsg = new StringBuilder();
            StringBuilder Security1 = new StringBuilder();
            int cc = ReadCardData1(Security1, 0, 0, "FFFFFFFFFFFF", errMsg);
            if (cc < 0)
            {
                return -1;
            }

            cc = ReadCardData1(MCard1, 1, 0, "FFFFFFFFFFFF", errMsg);
            if (cc < 0)
            {
                return -1;
            }
            Security = Security1.ToString();
            MCard = MCard1.ToString();
            return 1;
        }

        /// <summary>
        /// 写入病历号(暂时不行)
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public static int WriteCardNo(string CardNO)
        {
            StringBuilder errMsg = new StringBuilder();

            int cc = WriteCardData1(2, CardNO, 0, "FFFFFFFFFFFF", errMsg);

            if (cc < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        private static int ReadCardData2(StringBuilder sData, int nBlock, int sPassType, string sPassWord, StringBuilder errMsg)
        {
            try
            {
                int recode = 0;
                //  sData = new StringBuilder();
                errMsg = new StringBuilder();

                bool re = LinkCard();
                if (re == true)
                {
                    byte[] data = new byte[128];
                    string pass = "";
                    for (int i = 0; i < sPassWord.Length; i += 2)
                    {
                        pass += " " + sPassWord.Substring(i, 2);
                    }
                    int flag = HexReadCardData(data, nBlock, sPassType, pass);

                    if (flag == 1)
                    {
                        string str = BitConverter.ToString(data).Replace("-", "");
                        if (nBlock == 0)
                        {
                            sData.Append(str.Substring(0, 32));
                        }
                        else if (nBlock == 1)
                        {
                            if (str.Substring(10, 10) == "0000000000") //诊疗卡
                            {
                                sData.Append(str.Substring(0, 8));
                            }
                            else
                            {
                                sData.Append(str.Substring(1, 19));
                            }
                        }
                        else if (nBlock == 5)
                        {
                            sData.Append(str.Substring(0, 10));
                            //sData.Append(str);
                        }

                        recode = 1;
                    }
                    else
                    {
                        GetErr(errMsg);
                        recode = -1;
                    }
                }
                else
                {
                    GetErr(errMsg);
                    recode = -1;
                }

                return recode;
            }
            catch (Exception ex)
            {
                sData = new StringBuilder();
                errMsg.Append(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 读取病历号（暂时不行）
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public  int ReadCardNO(ref string CardNo,ref string msg)
        {
            msg = "";
            StringBuilder MCard1 = new StringBuilder();
            StringBuilder errMsg = new StringBuilder();
            int cc = ReadCardData1(MCard1, 2, 0, "FFFFFFFFFFFF", errMsg);
            if (cc < 0)
            {
                return -1;
            }

            CardNo = MCard1.ToString();
            return 1;
        }

        public int ReadMCardNO(ref string MCardNo, ref string Msg)
        {
            Msg = "";
            StringBuilder MCard1 = new StringBuilder();
            StringBuilder errMsg = new StringBuilder();
            int cc = ReadCardData1(MCard1,1, 0, "FFFFFFFFFFFF", errMsg);
            if (cc < 0)
            {
                return -1;
            }

            MCardNo = MCard1.ToString();
            return 1;
        }

      
        #endregion


        #region 读取身份证
        [DllImport("sdtapi.dll")]
        protected static extern int SDT_OpenPort(int iPort);

        [DllImport("sdtapi.dll")]
        protected static extern int SDT_StartFindIDCard(int iPort, byte[] pucIIN, int iIfOpen);

        [DllImport("sdtapi.dll")]
        protected static extern int SDT_SelectIDCard(int iPort, byte[] pucSN, int iIfOpen);


        [DllImport("sdtapi.dll")]
        protected static extern int SDT_ReadBaseMsg(int iPort, byte[] pucCHMsg, ref int puiCHMsgLen, byte[] pucPHMsg, ref int puiPHMsgLen, int iIfOpen);

        [DllImport("sdtapi.dll")]
        protected static extern int SDT_ClosePort(int iPort);

        #endregion

        private static void ClosePort(int port)
        {
            try
            {
                SDT_ClosePort(port);
            }
            catch
            { }
        }

        /// <summary>
        /// 读取身份证
        /// </summary>
        /// <returns></returns>
        public static bool ReadIDCard(ref string IdNo, ref string name, ref string nation, ref string address, ref string birthday, ref string sex, ref string error)
        {
            int port = 1001;
            byte[] pucIIN = new byte[255];
            byte[] pucSN = new byte[255];
            byte[] pucCHMsg = new byte[1024];
            byte[] pucPHMsg = new byte[1024];
            int uiCHMsgLen = 0;
            int uiPHMsgLen = 0;
            int result;

            try
            {
                for (int iPort = 1001; iPort <= 1016; iPort++)
                {
                    int intOpenPortRtn = SDT_OpenPort(iPort);
                    if (intOpenPortRtn == 144)
                    {
                        port = iPort;
                        break;
                    }
                }

                ClosePort(port);
                result = SDT_OpenPort(port);
                if (result != 144)
                {
                    error = "打开USB口失败";
                    ClosePort(port);
                    return false;
                }
                result = SDT_StartFindIDCard(port, pucIIN, 0);
                if (result != 159)
                {
                    result = SDT_StartFindIDCard(port, pucIIN, 0);
                    if (result != 159)
                    {
                        ClosePort(port);
                        error = "未放卡或者卡未放好，请重新放卡！";
                        return false;
                    }
                }

                result = SDT_SelectIDCard(port, pucSN, 0);
                if (result != 144)
                {
                    result = SDT_SelectIDCard(port, pucSN, 0);
                    if (result != 144)
                    {
                        error = "此卡不是标准身份证";
                        ClosePort(port);
                        return false;
                    }

                }

                result = SDT_ReadBaseMsg(port, pucCHMsg, ref uiCHMsgLen, pucPHMsg, ref uiPHMsgLen, 0);
                if (result != 144)
                {
                    error = "读取身份证错误！";
                    ClosePort(port);
                    return false;
                }

                string messge = System.Text.ASCIIEncoding.Unicode.GetString(pucCHMsg);
                name = messge.Substring(0, 15).Trim();
                messge = messge.Substring(15);

                sex = messge.Substring(0, 1);
                if (sex == "1")
                {
                    sex = "男";
                }
                else
                {
                    sex = "女";
                }
                messge = messge.Substring(1);
                loadNation();
                nation = messge.Substring(0, 2).Trim();
                if (lstMZ.ContainsKey(nation))
                {
                    nation = lstMZ[nation].ToString();
                }
                else
                {
                    nation = "未知名族";
                }
                messge = messge.Substring(2);

                birthday = messge.Substring(0, 8).Trim();
                messge = messge.Substring(8);

                address = messge.Substring(0, 35).Trim();
                messge = messge.Substring(35);

                IdNo = messge.Substring(0, 18).Trim();
                messge = messge.Substring(18);

                messge = messge.Substring(15);

                messge = messge.Substring(8);

                messge = messge.Substring(8);

                ClosePort(port);
                return true;
            }
            catch
            {
                ClosePort(port);
                return false;
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
