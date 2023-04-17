using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FS.SOC.Local.OutpatientFee.FoSan
{
    public class Function
    {
        #region 佛三显示屏

        #region RECT

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        #endregion

        [DllImport("user32.dll", EntryPoint = "MessageBoxA")]
        static extern int MsgBox(int hWnd, string msg, string caption, int type);
        //清屏
        [DllImport("cts4r.dll", EntryPoint = "Cls")]
        static extern int Cls(ref RECT lpRect);
        //清屏重载				
        [DllImport("cts4r.dll", EntryPoint = "Cls")]
        static extern int Cls(int lp);
        //设置字体								
        [DllImport("cts4r.dll", EntryPoint = "SetFont")]
        static extern int SetFont(byte fontSize, string fontName, byte fbold);
        //图形模式文本输出
        [DllImport("cts4r.dll", EntryPoint = "DrawText")]
        static extern int DrawText(int X, int Y, string text);
        //文本模式文本输出				
        [DllImport("cts4r.dll", EntryPoint = "PrintText")]
        static extern int PrintText(ushort Col, ushort Row, string text);
        //画直线
        [DllImport("cts4r.dll", EntryPoint = "DrawLine")]
        static extern int DrawLine(int X1, int Y1, int X2, int Y2);
        //图像输出	
        [DllImport("cts4r.dll", EntryPoint = "DrawImage")]
        static extern int DrawImage(int X, int Y, string filename, ref RECT lpRect);
        //图像输出重载
        [DllImport("cts4r.dll", EntryPoint = "DrawImage")]
        static extern int DrawImage(int X, int Y, string filename, int lp);
        //滚屏		
        [DllImport("cts4r.dll", EntryPoint = "Scroll")]
        static extern int Scroll(byte loop, byte orient, ushort scale);
        //语音播放
        [DllImport("cts4r.dll", EntryPoint = "SoundOut")]
        static extern int SoundOut(string cmdstr);
        //复位							
        [DllImport("cts4r.dll", EntryPoint = "Reset")]
        static extern int Reset();
        //设置显示坐标原点位置								
        [DllImport("cts4r.dll", EntryPoint = "SetOrigin")]
        static extern int SetOrigin(ushort X, ushort Y);    						
        [DllImport("cts4r.dll", EntryPoint = "NJFSpeak")]
        static extern long NJFSpeak(string cmdstr);

        //[DllImport("cts4r.dll", EntryPoint = "SoftSoundOut")]
        //static extern long SoftSoundOut(string cmdstr,char flag ); 
        
        /// <summary>
        /// 清屏
        /// </summary>
        public void SetClear()
        {
            Cls(0);
        }

        /// <summary>
        /// 字体
        /// </summary>
        /// <param name="flag"></param>
        public void SetFont(byte flag)
        {
            //SetFont(flag, "楷体_GB2312", 1);
            SetFont(flag, "宋体", 1);
        }

        /// <summary>
        /// 位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="str"></param>
        public void SetPrintText(int x, int y, string str)
        {
            //SetFont(30, "楷体_GB2312", 1);
            //PrintText(1, 2, str);
            //PrintText(x, y, str);
            DrawText(x, y, str);
        }

        /// <summary>
        ///声音
        /// </summary>
        /// <param name="str"></param>
        public void SetSoundWait(string str)
        {
            //SoundOut("_H");
            //NJFSpeak(str + "请您稍等");
        }

        /// <summary>
        /// 声音
       /// </summary>
       /// <param name="str"></param>
       /// <param name="t"></param>
        public void SetSoundOut(string str, int t)
        {
            switch (t)
            {
                case 0:
                    SoundOut(str + "_P");
                    break;
                case 1:
                    SoundOut(str + "_k");
                    break;
                case 2:
                    SoundOut(str + "_B");
                    break;
                case 3:
                    NJFSpeak(str);
                    break;
                default:
                    break;
            }

        }
       
        /// <summary>
        ///  显示患者姓名
        /// </summary>
        /// <param name="name"></param>
        public void GetName(string name)
        {
            SetClear();
            SetFont(23);
            SetPrintText(20, 1, "姓名：" + name);
            SetSoundWait("\"" + name + "\"");
        }

        /// <summary>
        /// 显示应收金额
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cost"></param>
        public void GetPayCost(string name, decimal cost)
        {
            try
            {
                cost = Decimal.Round(cost, 1, MidpointRounding.AwayFromZero);
                SetClear();
                SetFont(12);
                SetPrintText(40, 1, "姓名：      " + name);
                SetPrintText(40, 21, "应收：      " + cost + "元");
                //setPrintText(1, 4, "姓名：      " + name);
                //setPrintText(1, 4,"应收：      " + cost + "元");
                SetSoundOut(cost + "", 0);
            }
            catch
            {
                SetClear();
                SetFont(12);
                SetPrintText(40, 1, " 姓名：    " + name);
                SetPrintText(40, 16, "应收：     " + cost + "元");
                SetSoundOut(cost + "", 0);
            }
        }

        /// <summary>
        /// 显示实收应收找零
        /// </summary>
        /// <param name="name"></param>
        /// <param name="TotOwnCost"></param>
        /// <param name="RealCost"></param>
        /// <param name="Least"></param>
        public void GetAllCount(string name, decimal TotOwnCost, decimal RealCost, decimal Least)
        {
            try
            {
                TotOwnCost = Decimal.Round(TotOwnCost, 1, MidpointRounding.AwayFromZero);
                RealCost = Decimal.Round(RealCost, 1, MidpointRounding.AwayFromZero);
                Least = Decimal.Round(RealCost - TotOwnCost, 1, MidpointRounding.AwayFromZero);
                SetClear();
                SetFont(12);
                SetPrintText(1, 1, "姓名：" + name);
                SetPrintText(115, 1, " 应收：" + TotOwnCost + "元");
                SetPrintText(1, 21, "实收：" + RealCost + "元");
                if (Least > 0)
                {
                    SetPrintText(115, 21, " 找零：" + Least + "元");
                    //SetSoundOut(Least + "", 2);
                }
                SetSoundOut(RealCost + "", 1);
                if (Least > 0)
                {
                    //SetPrintText(115, 21, " 找零：" + Least + "元");
                    SetSoundOut(Least + "", 2);
                }
            }
            catch
            {
                SetClear();
                SetFont(12);
                SetPrintText(1, 1, "姓名：" + name);
                SetPrintText(115, 1, "应收：" + TotOwnCost + "元");
                SetPrintText(1, 21, "实收：" + RealCost + "元");
                SetSoundOut(RealCost + "", 1);
                if (Least > 0)
                {
                    SetPrintText(115, 21, "找零：" + Least + "元");
                    SetSoundOut(Least + "", 2);
                }
            }
        }
       
        #endregion
    }
}
