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

namespace FS.SOC.Local.Registration.FoSan
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
            return 1;
        }

        protected void PrintPage(System.Drawing.Printing.PaperSize paperSize)
        {
        }

        private void SetPaperSize(System.Drawing.Printing.PaperSize paperSize)
        {
        
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns></returns>
        public int PrintView()
        {
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

                strTemp = "门诊号：" + register.PID.CardNO.TrimStart('0') + "   " + "顺序号：" + register.OrderNO.ToString();
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

    }
}
