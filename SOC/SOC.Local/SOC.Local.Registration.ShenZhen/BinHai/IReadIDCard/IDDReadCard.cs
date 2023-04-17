using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;

namespace FS.SOC.Local.Registration.ShenZhen.BinHai.IReadIDCard
{
    public class IDDReadCard : FS.HISFC.BizProcess.Interface.Account.IReadIDCard
    {

        #region 精伦电子 -居民身份证读卡器

        [DllImport(@"sdtapi.dll")]
        public extern static int InitComm(int iPort);

        [DllImport(@"sdtapi.dll")]
        public extern static int CloseComm();

        [DllImport(@"sdtapi.dll")]
        public extern static int Authenticate();

        [DllImport(@"sdtapi.dll")]
        public extern static int ReadBaseMsg(StringBuilder pMsg, int len);

        [DllImport(@"sdtapi.dll")]
        public extern static int ReadBaseMsgPhoto(StringBuilder pMsg, int len, StringBuilder directory);

        [DllImport(@"sdtapi.dll")]
        public extern static int ReadBaseInfos(StringBuilder Name, StringBuilder Gender, StringBuilder Folk,
StringBuilder BirthDay, StringBuilder Code, StringBuilder Address, StringBuilder Agency, StringBuilder ExpireStart, StringBuilder ExpireEnd);

        [DllImport(@"sdtapi.dll")]
        public extern static int ReadBaseInfosPhoto(StringBuilder Name, StringBuilder Gender, StringBuilder Folk,
StringBuilder BirthDay, StringBuilder Code, StringBuilder Address, StringBuilder Agency, StringBuilder ExpireStart, StringBuilder ExpireEnd, StringBuilder directory);

        [DllImport(@"sdtapi.dll")]
        public extern static int ReadBaseMsgW(StringBuilder pMsg, int len);

        [DllImport(@"sdtapi.dll")]
        public extern static int ReadBaseMsgWPhoto(StringBuilder pMsg, int len, StringBuilder directory);

        [DllImport("kernel32.dll")]
        private static extern int Beep(int dwFreq, int dwDuration);//用来大吼一声

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDCode"></param>
        /// <param name="Name"></param>
        /// <param name="Sex"></param>
        /// <param name="BirthDay"></param>
        /// <param name="Nation"></param>
        /// <param name="Adress"></param>
        /// <param name="Agency"></param>
        /// <param name="ExpireStart"></param>
        /// <param name="ExpireEnd"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static int GetIDInfoOther(ref string IDCode, ref string Name, ref string Sex, ref DateTime BirthDay, ref string Nation, ref string Adress, ref string Agency, ref DateTime ExpireStart, ref DateTime ExpireEnd, ref string photoFileName, ref string Message)
        {

            bool bUsbPort = false;
            int iReturn = 0;
            //检测usb口的机具连接，必须先检测usb
            for (int iPort = 1001; iPort <= 1016; iPort++)
            {
                iReturn = InitComm(iPort);
                if (iReturn == 1)
                {
                    CurPort = iPort;
                    bUsbPort = true;
                    break;
                }
            }
            //检测串口的机具连接
            //if (!bUsbPort)
            //{
            //    for (int iPort = 1; iPort <= 4; iPort++)
            //    {
            //        iReturn = InitComm(iPort);
            //        if (iReturn == 1)
            //        {
            //            CurPort = iPort;
            //            bUsbPort = false;
            //            break;
            //        }
            //    }
            //}
            if (iReturn != 1)
            {
                Message = "端口打开失败，请检测相应的端口或者重新连接读卡器！";
                return -1;
            }

            //定义读取内存
            StringBuilder sbName = new StringBuilder(31);
            StringBuilder sbSex = new StringBuilder(3);
            StringBuilder sbNation = new StringBuilder(10);
            StringBuilder sbBirthDay = new StringBuilder(9);
            StringBuilder sbIDCode = new StringBuilder(19);
            StringBuilder sbAddress = new StringBuilder(71);
            StringBuilder sbAgency = new StringBuilder(31);
            StringBuilder sbExpireStart = new StringBuilder(9);
            StringBuilder sbExpireEnd = new StringBuilder(9);
            StringBuilder sbDirectory = new StringBuilder(100);

            //新建目录
            if (!Directory.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Photo"))
            {
                Directory.CreateDirectory(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Photo");
            }
            sbDirectory.Append(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Photo");
            //卡认证
            iReturn = Authenticate();
            if (iReturn != 1)
            {
                Message = "未放卡或者卡未放好，请重新放卡！";
                CloseComm();
                return 0;
            }
            //   ReadBaseInfos(推荐使用)
            iReturn = ReadBaseInfosPhoto(sbName, sbSex, sbNation, sbBirthDay, sbIDCode, sbAddress, sbAgency, sbExpireStart, sbExpireEnd, sbDirectory);
            if (iReturn != 1)
            {
                Message = "读卡失败！";
                CloseComm();
                return 0;
            }

            Beep(2047, 200);
            IDCode = sbIDCode.ToString().Trim();
            Name = sbName.ToString().Trim();
            Sex = sbSex.ToString();
            Nation = sbNation.ToString();
            BirthDay = FS.FrameWork.Function.NConvert.ToDateTime(sbBirthDay.ToString());
            Adress = sbAddress.ToString();
            Agency = sbAgency.ToString();
            ExpireStart = FS.FrameWork.Function.NConvert.ToDateTime(sbExpireStart.ToString());
            ExpireEnd = FS.FrameWork.Function.NConvert.ToDateTime(sbExpireEnd.ToString());

            //先屏蔽掉，避免数据量过大
            //photoFileName = sbDirectory.ToString() + "\\photo.bmp";
            //if (File.Exists(photoFileName))
            //{
            //    if (File.Exists(sbDirectory.ToString() + "\\" + IDCode + ".bmp"))
            //    {
            //        File.Delete(sbDirectory.ToString() + "\\" + IDCode + ".bmp");
            //    }

            //    File.Copy(photoFileName, sbDirectory.ToString() + "\\" + IDCode + ".bmp");

            //    File.Delete(photoFileName);

            //    photoFileName = sbDirectory.ToString() + "\\" + IDCode + ".bmp";
            //}
            //arrys[9]=System.IO.Directory.GetCurrentDirectory()+"\\photo.bmp"；
            Message = " 读卡成功";


            //SetImage("photo.bmp", pictureBox1);
            CloseComm();
            return 1;


        }
        #endregion

        #region 精伦-身份证读取接口

        private static int CurPort = 0;

        #endregion

        #region IReadIDCard 成员

        public int GetIDInfo(ref string IDCode, ref string Name, ref string Sex, ref DateTime BirthDay, ref string Nation, ref string Adress, ref string Agency, ref DateTime ExpireStart, ref DateTime ExpireEnd, ref string PhotoFileName, ref string Message)
        {
            return GetIDInfoOther(ref IDCode, ref Name, ref Sex, ref BirthDay, ref Nation, ref Adress, ref Agency, ref ExpireStart, ref ExpireEnd, ref PhotoFileName, ref Message);
        }

        #endregion
    }
}
