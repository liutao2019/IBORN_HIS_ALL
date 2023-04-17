using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Configuration;

namespace FS.SOC.HISFC.CallQueue.InterfaceImplement.ZDLY
{
    /// <summary>
    /// 灵信LED显示动态链接库声明
    /// </summary>
    class LEDDLL
    {
        /// <summary>
        /// 设置通讯方式
        /// </summary>
        /// <param name="TransMode"></param>
        /// <param name="ConType"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetTransMode")]
        public static extern int SetTransMode(int TransMode, int ConType);

        /// <summary>
        /// 设置网络通讯参数
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetNetworkPara")]
        public static extern int SetNetworkPara(int pno, char[] ip);

        /// <summary>
        /// 设置串口通讯参数
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="port"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetSerialPortPara")]
        public static extern int SetSerialPortPara(int pno, int port, int rate);


        /// <summary>
        /// 屏参设置
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="DBColor"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SendScreenPara")]
        public static extern int SendScreenPara(int pno, int DBColor, int width, int height);

        /// <summary>
        /// </summary>
        /// <param name="pno">屏号</param>
        /// <param name="power">0 为开屏   1 为关屏</param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetPower")]
        public static extern int SetPower(int pno, int power);

        /// <summary>
        /// 设置控制器定时开关屏
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="flag"></param>
        /// <param name="startHour1"></param>
        /// <param name="startMinute1"></param>
        /// <param name="endHour1"></param>
        /// <param name="endMinute1"></param>
        /// <param name="startHour2"></param>
        /// <param name="startMinute2"></param>
        /// <param name="endHour2"></param>
        /// <param name="endMinute2"></param>
        /// <param name="startHour3"></param>
        /// <param name="startMinute3"></param>
        /// <param name="endHour3"></param>
        /// <param name="endMinute3"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetPowerTimer")]
        public static extern int SetPowerTimer(int pno, int flag, int startHour1, int startMinute1, int endHour1, int endMinute1,
                                                int startHour2, int startMinute2, int endHour2, int endMinute2,
                                                int startHour3, int startMinute3, int endHour3, int endMinute3);

        /// <summary>
        /// 校时
        /// </summary>
        /// <param name="pno"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AdjustTime")]
        public static extern int AdjustTime(int pno);

        /// <summary>
        /// 设置控制器网络参数
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="macAddress"></param>
        /// <param name="ip"></param>
        /// <param name="gateway"></param>
        /// <param name="subnetmask"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetRemoteNetwork")]
        public static extern int SetRemoteNetwork(int pno, string macAddress, string ip, string gateway, string subnetmask);


        /// <summary>
        /// 调节亮度
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetBrightness")]
        public static extern int SetBrightness(int pno, int value);

        /// <summary>
        /// 设置控制器定时亮度调节
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="flag"></param>
        /// <param name="startHour1"></param>
        /// <param name="startMinute1"></param>
        /// <param name="endHour1"></param>
        /// <param name="endMinute1"></param>
        /// <param name="brightness1"></param>
        /// <param name="startHour2"></param>
        /// <param name="startMinute2"></param>
        /// <param name="endHour2"></param>
        /// <param name="endMinute2"></param>
        /// <param name="brightness2"></param>
        /// <param name="startHour3"></param>
        /// <param name="startMinute3"></param>
        /// <param name="endHour3"></param>
        /// <param name="endMinute3"></param>
        /// <param name="brightness3"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetBrightnessTimer")]
        public static extern int SetBrightnessTimer(int pno, int flag,
                                         int startHour1, int startMinute1, int endHour1, int endMinute1, int brightness1,
                                         int startHour2, int startMinute2, int endHour2, int endMinute2, int brightness2,
                                         int startHour3, int startMinute3, int endHour3, int endMinute3, int brightness3);

        /// <summary>
        /// 时间限制
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="FSecond"></param>
        /// <param name="FMinute"></param>
        /// <param name="FHour"></param>
        /// <param name="FDay"></param>
        /// <param name="FMonth"></param>
        /// <param name="FWeek"></param>
        /// <param name="FYear"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetTimingLimit")]
        public static extern int SetTimingLimit(int pno, int FSecond, int FMinute, int FHour, int FDay, int FMonth, int FWeek, int FYear);
        
        /// <summary>
        /// 取消限制
        /// </summary>
        /// <param name="pno"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "CancelTimingLimit")]
        public static extern int CancelTimingLimit(int pno);


        //查询//

        /// <summary>
        /// 网络搜索控制器
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SearchController")]
        public static extern int SearchController(string filePath);
        
        /// <summary>
        /// 串口搜素
        /// </summary>
        /// <param name="PNO"></param>
        /// <param name="ComNo"></param>
        /// <param name="BaudRate"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "ComSearchController")]
        public static extern int ComSearchController(int PNO, int ComNo, int BaudRate, string filePath);


        ////////
        //内容//

        /// <summary>
        /// 初始化数据结构
        /// </summary>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "StartSend")]
        public static extern int StartSend();

        /// <summary>
        /// 添加控制器资源
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="DBColor"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddControl")]
        public static extern int AddControl(int pno, int DBColor);

        /// <summary>
        /// 添加节目
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="playTime"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddProgram")]
        public static extern int AddProgram(int pno, int jno, int playTime);

        /// <summary>
        /// 设置节目定时播放属性
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="TimingModel"></param>
        /// <param name="WeekSelect"></param>
        /// <param name="startSecond"></param>
        /// <param name="startMinute"></param>
        /// <param name="startHour"></param>
        /// <param name="startDay"></param>
        /// <param name="startMonth"></param>
        /// <param name="startWeek"></param>
        /// <param name="startYear"></param>
        /// <param name="endSecond"></param>
        /// <param name="endMinute"></param>
        /// <param name="endHour"></param>
        /// <param name="endDay"></param>
        /// <param name="endMonth"></param>
        /// <param name="endWeek"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetProgramTimer")]
        public static extern int SetProgramTimer(int pno, int jno, int TimingModel, int WeekSelect,
                                                 int startSecond, int startMinute, int startHour, int startDay, int startMonth, int startWeek, int startYear,
                                                 int endSecond, int endMinute, int endHour, int endDay, int endMonth, int endWeek, int endYear);

        //转换rgb值为颜色值
        //int _stdcall GetRGB( int r, int g, int b);

        /// <summary>
        /// 添加单行文本区域
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="LnFileName"></param>
        /// <param name="PlayStyle"></param>
        /// <param name="Playspeed"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddLnTxtArea")]
        public static extern int AddLnTxtArea(int pno, int jno, int qno, int left, int top, int width, int height, string LnFileName, int PlayStyle, int Playspeed, int times);

        /// <summary>
        /// 添加静止文本区域
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="FontColor"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontBold"></param>
        /// <param name="Italic"></param>
        /// <param name="Underline"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddQuitText")]
        public static extern int AddQuitText(int pno, int jno, int qno, int left, int top, int width, int height, int FontColor, string fontName, int fontSize, int fontBold, int Italic, int Underline, string text);

        /// <summary>
        /// 添加图文区域
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddFileArea")]
        public static extern int AddFileArea(int pno, int jno, int qno, int left, int top, int width, int height);

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="mno"></param>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="playstyle"></param>
        /// <param name="QuitStyle"></param>
        /// <param name="playspeed"></param>
        /// <param name="delay"></param>
        /// <param name="MidText"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddFile")]
        public static extern int AddFile(int pno, int jno, int qno, int mno, string fileName, int width, int height, int playstyle, int QuitStyle, int playspeed, int delay, int MidText);

        /// <summary>
        /// 添加动画区域
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddFlashArea")]
        public static extern int AddFlashArea(int pno, int jno, int qno, int left, int top, int width, int height);

        /// <summary>
        /// 添加动画帧图片
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="mno"></param>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="playspeed"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddFlashBmpFile")]
        public static extern int AddFlashBmpFile(int pno, int jno, int qno, int mno, string fileName, int width, int height, int playspeed);

        /// <summary>
        /// 添加数字时钟区域
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fontColor"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontBold"></param>
        /// <param name="Italic"></param>
        /// <param name="Underline"></param>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="TwoOrFourYear"></param>
        /// <param name="HourShow"></param>
        /// <param name="format"></param>
        /// <param name="spanMode"></param>
        /// <param name="Advacehour"></param>
        /// <param name="Advaceminute"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddDClockArea")]
        public static extern int AddDClockArea(int pno, int jno, int qno, int left, int top, int width, int height,
                                               int fontColor, string fontName, int fontSize, int fontBold, int Italic, int Underline,
                                               int year, int week, int month, int day, int hour, int minute, int second, int TwoOrFourYear,
                                               int HourShow, int format, int spanMode, int Advacehour, int Advaceminute);


        /// <summary>
        /// 添加模拟时钟区域
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="BeforOrDelay"></param>
        /// <param name="TimeHour"></param>
        /// <param name="TimeMin"></param>
        /// <param name="HourColor"></param>
        /// <param name="MinColor"></param>
        /// <param name="SecColor"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddModelClock")]
        public static extern int AddModelClock(int pno, int jno, int qno, int left, int top, int width, int height, int BeforOrDelay,
                                               int TimeHour, int TimeMin, int HourColor, int MinColor, int SecColor);

        /// <summary>
        /// 添加计时区域
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fontColor"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontBold"></param>
        /// <param name="Italic"></param>
        /// <param name="Underline"></param>
        /// <param name="mode"></param>
        /// <param name="DayShow"></param>
        /// <param name="CulWeek"></param>
        /// <param name="CulDay"></param>
        /// <param name="CulHour"></param>
        /// <param name="CulMin"></param>
        /// <param name="CulSec"></param>
        /// <param name="year"></param>
        /// <param name="week"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddTimerArea")]
        public static extern int AddTimerArea(int pno, int jno, int qno, int left, int top, int width, int height,
                                              int fontColor, string fontName, int fontSize, int fontBold, int Italic, int Underline,
                                              int mode, int DayShow, int CulWeek, int CulDay, int CulHour, int CulMin, int CulSec,
                                              int year, int week, int month, int day, int hour, int minute, int second);


        /// <summary>
        /// 添加温度区域
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fontColor"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontBold"></param>
        /// <param name="Italic"></param>
        /// <param name="Underline"></param>
        /// <param name="Temp_Mode"></param>
        /// <param name="Temp_digit"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddTemperature")]
        public static extern int AddTemperature(int pno, int jno, int qno, int left, int top, int width, int height,
                                                int fontColor, string fontName, int fontSize, int fontBold, int Italic, int Underline,
                                                int Temp_Mode, int Temp_digit);

        /// <summary>
        /// 添加噪声区
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fontColor"></param>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontBold"></param>
        /// <param name="Italic"></param>
        /// <param name="Underline"></param>
        /// <param name="Noise_digit"></param>
        /// <param name="NAdjust"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddNoise")]
        public static extern int AddNoise(int pno, int jno, int qno, int left, int top, int width, int height,
                                          int fontColor, string fontName, int fontSize, int fontBold, int Italic, int Underline,
                                          int Noise_digit, int NAdjust);


        /// <summary>
        /// 发送内容
        /// </summary>
        /// <param name="PNO"></param>
        /// <param name="SendType"></param>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "SendControl")]
        public static extern int SendControl(int PNO, int SendType, IntPtr hwnd);



        //////////////////
        //户外配置与查询//
        //////////////////

        //设置硬件参数
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetHardPara")]
        public static extern int SetHardPara(int PNO, int Sign, int Mirror, int ScanStyle, int LineOrder, int cls, int RGChange, int zhangKong);
        //查询硬件参数
        [DllImport("ListenPlayDll.dll", EntryPoint = "GetHardPara")]
        public static extern int GetHardPara(int PNO, string FilePath);


        //显示测试
        [DllImport("ListenPlayDll.dll", EntryPoint = "SetTest")]
        public static extern int SetTest(int pno, int value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pno"></param>
        /// <param name="jno"></param>
        /// <param name="qno"></param>
        /// <param name="mno"></param>
        /// <param name="str"></param>
        /// <param name="fontname"></param>
        /// <param name="fontsize"></param>
        /// <param name="fontcolor"></param>
        /// <param name="bold"></param>
        /// <param name="italic"></param>
        /// <param name="underline"></param>
        /// <param name="align"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="playstyle"></param>
        /// <param name="QuitStyle"></param>
        /// <param name="playspeed"></param>
        /// <param name="delay"></param>
        /// <param name="MidText"></param>
        /// <returns></returns>
        [DllImport("ListenPlayDll.dll", EntryPoint = "AddFileString")]
        public static extern int AddFileString(int pno, int jno, int qno, int mno, string str, string fontname, int fontsize, int fontcolor, bool bold, bool italic, bool underline,int align, int width, int height, int playstyle, int QuitStyle, int playspeed, int delay, int MidText);

        [DllImport("ListenPlayDll.dll", EntryPoint = "AddLnTxtString")]
        public static extern int AddLnTxtString(int pno, int jno, int qno, int left, int top, int width, int height,
                string text,
                string fontname,
                int fontsize,
                int fontcolor,
                bool bold,
                bool italic,
                bool underline,
                int PlayStyle,
                int Playspeed,
                int times);



    }
}





