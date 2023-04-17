using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Xml;

namespace FS.SOC.Local.Registration.FoSi
{
    /// <summary>
    /// 挂号发票打印
    /// </summary>
    public partial class ucRegPrint : UserControl, FS.HISFC.BizProcess.Interface.Registration.IRegPrint
    {
        public ucRegPrint()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 打印纸张设置类
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();

        private FS.HISFC.BizProcess.Integrate.Manager manageIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        /// <summary>
        /// 打印用
        /// </summary>
        //private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();


        #endregion

        #region IRegPrint 成员

        public int Clear()
        {
            return 0;
        }

                /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            if (lstPrint == null || lstPrint.Count <= 0)
            {
                return 1;
            }

            try
            {
                string strMsg = "";
                int iRes = this.GetCOMParams(out portName, out baudRate, out parity, out stopBit, out dataBit, out strMsg);
                if (iRes <= 0)
                {
                    return iRes;
                }
                this.OpenCOM();
                this.Send("@"); // 初始化
                //设置打印字体
                int it1 = 27;
                int it2 = 156;
                string s1 =((char)it1).ToString();
                string s2 =((char)it2).ToString();
                this.Send(s1 + "!" + s2);//设置英文字体
                int it3 = 28;
                int it4 = 40;
                string s3 = ((char)it3).ToString();
                string s4 = ((char)it4).ToString();
                this.Send(s3 + "!" + s4);//设置中文字体
                foreach (string str in lstPrint)
                {
                    this.Send(str + "\r\n");
                }

                this.Send("\r\n");
                this.Send("\r\n");
                this.Send("i");

                this.CloseCOM();

            }
            catch (Exception objEx)
            {
                MessageBox.Show(objEx.Message);
                return -1;
            }
            finally
            {
                this.CloseCOM();
            }
            return 1;

            //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            //FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            //FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("GHPZ");//挂号凭证
            //System.Drawing.Printing.PaperSize curPaperSize = new System.Drawing.Printing.PaperSize();
            //if (pSize == null)
            //{
            //    pSize = new FS.HISFC.Models.Base.PageSize("ghpz", 300, 200);
            //    pSize.Top = 0;
            //    pSize.Left = 0;
            //}

            //curPaperSize.PaperName = pSize.Name;
            //curPaperSize.Height = pSize.Height;
            //curPaperSize.Width = pSize.Width;
            //print.SaveAsFile(this, "D:\\挂号票\\GHPZ.jpg", pSize.Width, pSize.Height);
            //MessageBox.Show("保存图片完毕");

            //print.SetPageSize(pSize);
            //if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            //{
            //    this.printPicture();
            //    this.PrintView();
            //    return 1;
            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(pSize.Printer))
            //    {
            //        this.PrintDocument.PrinterSettings.PrinterName = pSize.Printer;
            //    }

            //    this.PrintPage(curPaperSize);
            //    return 1;
            //}
        }

        protected void PrintPage(System.Drawing.Printing.PaperSize paperSize)
        {
            //this.SetPaperSize(paperSize);
            //this.PrintDocument.Print();
        }

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
        //    if (paperSize == null)
        //    {
        //        paperSize = new System.Drawing.Printing.PaperSize("xsk", 300, 200);
        //    }

        //    this.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        //    this.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(paperSize.PaperName, paperSize.Width, paperSize.Height);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns></returns>
        public int PrintView()
        {
            //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.PrintPreview(0, 0, this);
            return 0;
        }

        List<string> lstPrint = new List<string>();
        public int SetPrintValue(FS.HISFC.Models.Registration.Register register)
        {
            lstPrint.Clear();
            try
            {
                string strTemp = "佛山市第四人民医院";
                lstPrint.Add(strTemp);
                lstPrint.Add(" "); // 空行
                // 打印日期：
                strTemp = "打印日期：" + register.DoctorInfo.SeeDate.ToString();
                lstPrint.Add(strTemp);

                lstPrint.Add(" ");


                string pSex = "";
                if (register.Sex.ID.ToString() == "M")
                {
                    pSex = "男";
                }
                else if (register.Sex.ID.ToString() == "F")
                {
                    pSex = "女";
                }
                else
                {
                    pSex = "";
                }
                string pAge = this.psManager.GetAge(register.Birthday, System.DateTime.Now);
                strTemp = register.Name + " " + pSex + "    " + pAge + "    " + register.Pact.Name;
                lstPrint.Add(strTemp);
                lstPrint.Add(" ");

                strTemp = "门诊号：" + register.PID.CardNO.TrimStart('0');// +"   " + "顺序号：" + register.OrderNO.ToString();
                lstPrint.Add(strTemp);
                lstPrint.Add(" ");
                lstPrint.Add(" ");
                lstPrint.Add(" ");
                lstPrint.Add(" ");

                ////门诊号
                //this.lblCardNo.Text = register.PID.CardNO.TrimStart('0');

                ////姓名
                //this.lblPatientName.Text = register.Name;

                ////挂号日期
                //this.lblRegDate.Text = register.DoctorInfo.SeeDate.ToString();

                ////合同单位类别
                //this.lblPactName.Text = register.Pact.Name;

                //string pAge = this.psManager.GetAge(register.Birthday, System.DateTime.Now);
                //this.lblAge.Text = pAge.ToString();
                
                //this.lblSex.Text = pSex.ToString();
                //this.lblOrderNo.Text = register.OrderNO.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 0;
        }

        public void SetTrans(IDbTransaction trans)
        {
            this.trans.Trans = trans;
        }

        public IDbTransaction Trans
        {
            get
            {
                return this.trans.Trans;
            }
            set
            {
                this.trans.Trans = value;
            }
        }

        #endregion

        #region 获取参数
        private string portName = string.Empty;
        private int baudRate = 0;
        private Parity parity = Parity.None;
        private StopBits stopBit = StopBits.None;
        private int dataBit = 0;

        private string proFileName = Application.StartupPath + @"\Profile\佛四票据打印配置参数.xml";

        /// <summary>
        /// 获得连接医保数据库参数
        /// </summary>
        /// <param name="strDbName"></param>
        /// <param name="strDbUser"></param>
        /// <param name="strDbPwd"></param>
        /// <returns></returns>
        private int GetCOMParams(out string strPortName, out int iBaudRate, out Parity parity, out StopBits stopBit, out int dataBit, out string errMsg)
        {
            strPortName = "";
            iBaudRate = 0;
            parity = Parity.None;
            stopBit = StopBits.None;
            errMsg = "";
            dataBit = 8;

            XmlDocument doc = new XmlDocument();

            try
            {
                StreamReader sr = new StreamReader(proFileName, System.Text.Encoding.Default);
                string cleanDown = sr.ReadToEnd();
                doc.LoadXml(cleanDown);
                sr.Close();
            }
            catch (Exception objEx)
            {
                errMsg = "加载票据打印配置参数失败！ " + objEx.Message;
                return -1;
            }

            XmlNode paramNode = doc.SelectSingleNode("/票据打印");
            try
            {
                strPortName = paramNode.ChildNodes[0].Attributes["PortName"].Value.Trim();
                int.TryParse(paramNode.ChildNodes[0].Attributes["BaudRate"].Value, out iBaudRate);
                string strParity = paramNode.ChildNodes[0].Attributes["Parity"].Value;
                string strstopBit = paramNode.ChildNodes[0].Attributes["StopBit"].Value;
                int.TryParse(paramNode.ChildNodes[0].Attributes["DataBit"].Value, out dataBit);

                strParity = strParity.ToUpper();
                switch (strParity)
                {
                    case "NONE":
                        parity = Parity.None;
                        break;
                    case "ODD":
                        parity = Parity.Odd;
                        break;
                    case "EVEN":
                        parity = Parity.Even;
                        break;
                    case "MARK":
                        parity = Parity.Mark;
                        break;
                    case "SPACE":
                        parity = Parity.Space;
                        break;
                }

                strstopBit = strstopBit.ToUpper();
                switch (strstopBit)
                {
                    case "NONE":
                        stopBit = StopBits.None;
                        break;
                    case "ONE":
                        stopBit = StopBits.One;
                        break;
                    case "TWO":
                        stopBit = StopBits.Two;
                        break;
                    case "ONEPOINTFIVE":
                        stopBit = StopBits.OnePointFive;
                        break;
                }
            }
            catch (Exception objEx)
            {
                errMsg = "读取票据打印配置参数失败！" + objEx.Message;
                return -1;
            }
            return 1;
        }

        #endregion
        #region 串口操作

        /// <summary>
        /// 打开一个新的串行端口连接，并开始接收数据。
        /// </summary>
        private int OpenCOM()
        {
            if (serialPortObj == null)
                serialPortObj = new SerialPort();

            if (!serialPortObj.IsOpen)
            {
                try
                {
                    serialPortObj.PortName = portName;
                    serialPortObj.BaudRate = baudRate;
                    serialPortObj.DataBits = dataBit;
                    serialPortObj.StopBits = stopBit;
                    serialPortObj.Parity = parity;
                    serialPortObj.WriteBufferSize = 2048;
                    serialPortObj.ReadBufferSize = 2048;

                    serialPortObj.Open();
                }
                catch (Exception objEx)
                {
                    MessageBox.Show("打开串口资源失败！" + objEx.Message);
                    return -1;
                }
            }
            return 1;
        }

        private int Send(string sendMsg)
        {
            if (string.IsNullOrEmpty(sendMsg))
            {
                return 1;
            }

            byte[] buff = Encoding.Default.GetBytes(sendMsg);

            return this.Send(buff);
        }

        private int Send(byte[] buffer)
        {
            if (buffer == null || buffer.Length <= 0)
                return -1;

            if (serialPortObj == null)
                return -1;
            if (!serialPortObj.IsOpen)
            {
                return -1;
            }

            serialPortObj.Write(buffer, 0, buffer.Length);
            return 1;
        }

        /// <summary>
        /// 关闭端口连接，将 IsOpen 属性设置为 false，并释放内存。
        /// </summary>
        public void CloseCOM()
        {
            if (serialPortObj == null)
            {
                return;
            }
            if (serialPortObj.IsOpen)
            {
                serialPortObj.Close();
            }
            serialPortObj.Dispose();
        }
        #endregion

        /// <summary>
        /// 串口
        /// </summary>
        SerialPort serialPortObj = null;

        ///// <summary>
        ///// 串口打印图片
        ///// </summary>
        ///// <returns></returns>
        //private int printPicture()
        //{
        //    try
        //    {
        //        if (pictureBox1.Image == null)
        //        {
        //            this.pictureBox1.Load("D:\\挂号票\\GHPZ.jpg");
        //        }
        //        this.pictureBox1.Size = this.Size;

        //        string errMsg = "";
        //        int iRes = GetCOMParams(out portName, out baudRate, out parity, out stopBit, out dataBit, out errMsg);
        //        if (iRes <= 0)
        //        {
        //            MessageBox.Show(errMsg);
        //            return -1;
        //        }

        //        this.Open();

        //        Bitmap btm = new Bitmap(pictureBox1.Image);

        //        int height, width, row, col;
        //        height = btm.Height;
        //        width = btm.Width;
        //        row = (height + 23) / 24;   //字符行
        //        col = (width > 576) ? 576 : width;

        //        //根据图象大小计算数据量
        //        int PicDataLen = 3 * col;
        //        int DataLen = 5 + PicDataLen + 2;   //5字节命令头 + 图象数据 + 2字节回车符立即打印
        //        byte[] aCmdBuf = new byte[DataLen];

        //        //设定图形打印模式
        //        aCmdBuf[0] = 27;
        //        aCmdBuf[1] = 42;
        //        aCmdBuf[2] = 33;
        //        aCmdBuf[3] = (byte)(col % 256);
        //        aCmdBuf[4] = (byte)(col / 256);

        //        //回车
        //        aCmdBuf[DataLen - 2] = 0x0d;
        //        //换行
        //        aCmdBuf[DataLen - 1] = 0x0a;

        //        for (int rowi = 0; rowi < row; rowi++)
        //        {
        //            int DataOffset = 5;

        //            for (int coli = 0; coli < col; coli++)
        //            {
        //                for (int i = 0; i < 3; i++)
        //                {
        //                    byte temp = 0;

        //                    for (int j = 0; j < 8; j++)
        //                    {
        //                        int dotrow = rowi * 24 + i * 8 + j;

        //                        try
        //                        {
        //                            if (btm.GetPixel(coli, dotrow).ToArgb() == Color.Black.ToArgb())
        //                                temp += (byte)(Math.Pow(2, (7 - j)));
        //                        }
        //                        catch
        //                        {
        //                            continue;
        //                        }
        //                    }

        //                    aCmdBuf[DataOffset++] = temp;
        //                }
        //            }
        //        }
        //        //byte[] b;
        //        //using (FileStream fs = File.OpenRead("D:\\挂号票\\GHPZ.jpg"))
        //        //{
        //        //    b = new byte[fs.Length];
        //        //    fs.Read(b, 0, b.Length);
        //        //    //byte[] b = new byte[1024];
        //        //    //UTF8Encoding temp = new UTF8Encoding(true);
        //        //    //while (fs.Read(b, 0, b.Length) > 0)
        //        //    //{
        //        //    //    Console.WriteLine(temp.GetString(b));
        //        //    //}
        //        //}


        //        //初始化打印机
        //        this.serialPortObj.Write("@");
        //        //发送图片
        //        this.serialPortObj.Write("&");
        //        //this.serialPortObj.Write(b, 0, b.Length);

        //        this.serialPortObj.Write(aCmdBuf, 0, aCmdBuf.Length);
        //        //打印并走纸
        //        //this.serialPortObj.Write("d@");

        //        //切纸
        //        this.serialPortObj.Write("i");
        //    }
        //    catch (ExecutionEngineException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return -1;
        //    }
        //    catch (ExternalException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return -1;
        //    }
        //    finally
        //    {
        //        this.Close();
        //    }

        //    return 1;
        //}


        //#endregion

        //class ClsPrintLPT
        //{
        //    private IntPtr iHandle;
        //    private System.IO.FileStream fs;
        //    private System.IO.StreamWriter sw;

        //    private string prnPort = "LPT1";   //打印机端口

        //    public ClsPrintLPT()
        //    {

        //    }

        //    private const uint GENERIC_READ = 0x80000000;
        //    private const uint GENERIC_WRITE = 0x40000000;
        //    private const int OPEN_EXISTING = 3;

        //    /// <summary>
        //    /// 打开一个vxd(设备)
        //    /// </summary>
        //    [System.Runtime.InteropServices.DllImport("kernel32.dll", EntryPoint = "CreateFile", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //    private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, int lpSecurityAttributes,
        //                                            int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        //    /// <summary>
        //    /// 开始连接打印机
        //    /// </summary>
        //    private bool PrintOpen()
        //    {
        //        iHandle = CreateFile(prnPort, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);

        //        if (iHandle.ToInt32() == -1)
        //        {
        //            MessageBox.Show("没有连接打印机或者打印机端口不是LPT1！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return false;
        //        }
        //        else
        //        {
        //            fs = new FileStream(iHandle, FileAccess.ReadWrite);
        //            sw = new StreamWriter(fs, System.Text.Encoding.Default);   //写数据
        //            return true;
        //        }
        //    }

        //    /// <summary>
        //    /// 打印字符串
        //    /// </summary>
        //    /// <param name="str">要打印的字符串</param>
        //    private void PrintLine(string str)
        //    {
        //        sw.WriteLine(str); ;
        //    }

        //    /// <summary>
        //    /// 关闭打印连接
        //    /// </summary>
        //    private void PrintEnd()
        //    {
        //        sw.Close();
        //        fs.Close();
        //    }

        //    /// <summary>
        //    /// 打印票据
        //    /// </summary>
        //    /// <param name="ds">tb_Temp 全部字段数据集合</param>
        //    /// <returns>true：打印成功 false：打印失败</returns>
        //    public bool PrintDataSet(DataSet dsPrint)
        //    {
        //        try
        //        {
        //            if (PrintOpen())
        //            {
        //                PrintLine(" ");
        //                PrintLine("[XXXXXXXXXXXXXXXXXX超市]");
        //                PrintLine("NO :      " + dsPrint.Tables[0].Rows[0][1].ToString());
        //                PrintLine("XXXXXX: " + dsPrint.Tables[0].Rows[0][2].ToString());
        //                PrintLine("XXXXXX: " + dsPrint.Tables[0].Rows[0][3].ToString());
        //                PrintLine("XXXXXX: " + dsPrint.Tables[0].Rows[0][4].ToString());
        //                PrintLine("XXXXXX: " + dsPrint.Tables[0].Rows[0][5].ToString());
        //                PrintLine("操 作 员: " + dsPrint.Tables[0].Rows[0][6].ToString() + " " + dsPrint.Tables[0].Rows[0][7].ToString());
        //                PrintLine("-------------------------------------------");
        //            }
        //            PrintEnd();

        //            return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }

        //    /// <summary>
        //    /// ESC/P 指令
        //    /// </summary>
        //    /// <param name="iSelect">0：退纸命令 1：进纸命令 2：换行命令</param>
        //    public void PrintESC(int iSelect)
        //    {
        //        string send;

        //        iHandle = CreateFile(prnPort, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);

        //        if (iHandle.ToInt32() == -1)
        //        {
        //            MessageBox.Show("没有连接打印机或者打印机端口不是LPT1！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            fs = new FileStream(iHandle, FileAccess.ReadWrite);
        //        }

        //        byte[] buf = new byte[80];

        //        switch (iSelect)
        //        {
        //            case 0:
        //                send = "" + (char)(27) + (char)(64) + (char)(27) + 'j' + (char)(255);    //退纸1 255 为半张纸长
        //                send = send + (char)(27) + 'j' + (char)(125);    //退纸2
        //                break;
        //            case 1:
        //                send = "" + (char)(27) + (char)(64) + (char)(27) + 'J' + (char)(255);    //进纸
        //                break;
        //            case 2:
        //                send = "" + (char)(27) + (char)(64) + (char)(12);   //换行
        //                break;
        //            default:
        //                send = "" + (char)(27) + (char)(64) + (char)(12);   //换行
        //                break;
        //        }

        //        for (int i = 0; i < send.Length; i++)
        //        {
        //            buf[i] = (byte)send[i];
        //        }

        //        fs.Write(buf, 0, buf.Length);
        //        fs.Close();
        //    }
        //}
    }
}
