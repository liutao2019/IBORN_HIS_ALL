using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.FoSi
{
    /// <summary>
    /// 外接屏显示接口---通过COM口显示
    /// </summary>
    public class MiltScreenByCom : FS.HISFC.BizProcess.Interface.FeeInterface.IMultiScreen
    {

        #region IMultiScreen 成员

        string DefaultMsg = "    祝 您 健 康     " + "                    ";
        string ClearMsg = "                    " + "                    ";

        public int CloseScreen()
        {
            this.Close();
            return 1;
        }

        public List<object> ListInfo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                if (value == null || value.Count != 5)
                    return;

                string[] strTempArr = value[4] as string[];
                if (strTempArr == null && strTempArr.Length <= 0)
                {
                    return;
                }

                string strType = strTempArr[0];
                if (strType == "1" || strType == "2")
                    return;

                this.Send(ClearMsg);
                string strMsg = "";

                if (strType == "3")
                {
                    FS.HISFC.Models.Registration.Register register = value[0] as FS.HISFC.Models.Registration.Register;
                    if (register == null)
                    {
                        return;
                    }

                    ArrayList feeItemLists  = value[2] as ArrayList;
                    decimal SumOwnCost = 0;
                    decimal SumRebateCost = 0;
     
                    if (feeItemLists != null && feeItemLists.Count > 0)
                    {
                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeItemLists)
                        {
                            SumRebateCost += f.FT.RebateCost;
                            SumOwnCost += f.FT.OwnCost - f.FT.RebateCost;
                        }
                      

                        strMsg = "姓名: " + register.Name + "\r\n    金额: " + SumOwnCost.ToString() + "元\r\n";
                        if (register.SIMainInfo.PubCost != 0 )
                        {
                            decimal temp = register.SIMainInfo.TotCost - register.SIMainInfo.PubCost;
                            if (temp > SumRebateCost)
                            {
                                strMsg = "姓名: " + register.Name + "\r\n    金额: " + (temp - SumRebateCost).ToString() + "元\r\n";
                            }
                        }

                    }
                }
                else if (strType == "4")
                {
                    strMsg = DefaultMsg;
                }
                if (string.IsNullOrEmpty(strMsg))
                    return;

                this.Send(strMsg);
            }
        }

        SerialPort port = null;

        public int ShowScreen()
        {
            string errMsg = "";
            int iRes = GetCOMParams(out portName, out baudRate, out parity, out stopBit, out dataBit, out errMsg);
            if (iRes <= 0)
            {
                MessageBox.Show(errMsg);
                return -1;
            }
            //string com = portName.Replace("COM", "");
            //int iCom = 0;
            //int.TryParse(com, out iCom);

            ////blnComInit = com_init(iCom, baudRate);
            ////if (!blnComInit)
            ////{
            ////    MessageBox.Show("收费外接屏，串口初始化失败！");
            ////}
            //if (port == null)
            //{
            //    port = new SerialPort();
            //}

            //if (port.IsOpen)
            //{
            //    port.Close();
            //}
            //port.PortName = portName;
            //port.BaudRate = baudRate;
            //port.Parity = parity;
            //port.StopBits = stopBit;
            //port.DataBits = 8;
            //if (!port.IsOpen)
            //{
            //    port.Open();
            //}

            this.Open();

            this.Send(DefaultMsg);
            return 1;
        }

        #endregion

        #region 获取参数
        private string portName = string.Empty;
        private int baudRate = 0;
        private Parity parity = Parity.None;
        private StopBits stopBit = StopBits.None;
        private int dataBit = 0;

        private string proFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"Profile\佛四收费外接屏参数.xml";

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
            catch(Exception objEx)
            {
                errMsg = "加载收费外接屏参数失败！ " + objEx.Message;
                return -1;
            }

            XmlNode paramNode = doc.SelectSingleNode("/收费外接屏");
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
                errMsg = "读取外接屏参数失败！" + objEx.Message;
                return -1;
            }
            return 1;
        }

        #endregion

        #region 串口操作

        SerialPort objSerialPort = null;
        /// <summary>
        /// 打开一个新的串行端口连接，并开始接收数据。
        /// </summary>
        private int Open()
        {
            if (objSerialPort == null)
                objSerialPort = new SerialPort();

            if (!objSerialPort.IsOpen)
            {
                try
                {
                    objSerialPort.PortName = portName;
                    objSerialPort.BaudRate = baudRate;
                    objSerialPort.DataBits = dataBit;
                    objSerialPort.StopBits = stopBit;
                    objSerialPort.Parity = parity;
                    objSerialPort.WriteBufferSize = 2048;
                    objSerialPort.ReadBufferSize = 2048;

                    objSerialPort.Open();
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
            if (objSerialPort == null)
                return -1;
            if (!objSerialPort.IsOpen)
            {
                return -1;
            }

            if(string.IsNullOrEmpty(sendMsg))
            {
                return 1;
            }

            byte[] buff = Encoding.Default.GetBytes(sendMsg);

            objSerialPort.Write(buff, 0, buff.Length);
            return 1;
        }

        /// <summary>
        /// 关闭端口连接，将 IsOpen 属性设置为 false，并释放内存。
        /// </summary>
        public void Close()
        {
            if (objSerialPort == null)
            {
                return;
            }
            if (objSerialPort.IsOpen)
            {
                objSerialPort.Close();
            }
            objSerialPort.Dispose();
        }
        #endregion

        [DllImport("API_COM.dll")]
        public static extern bool com_init(int com, int baud);
        [DllImport("API_COM.dll")]
        public static extern bool com_rest();
        [DllImport("API_COM.dll")]
        public static extern bool com_send(string buf, int len);
    }


    
}
