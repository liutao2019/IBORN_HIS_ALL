using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Forms
{
    public partial class frmShowEMR : FS.FrameWork.WinForms.Forms.frmBaseForm, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public frmShowEMR()
        {
            InitializeComponent();
        }

        #region IPreArrange 成员

        /// <summary>
        /// 界面显示前的各种判断
        /// </summary>
        /// <returns></returns>
        public int PreArrange()
        {
            Classes.LogManager.Write("【开始启动电子病历界面前 动作】");

            FS.HISFC.Models.Base.Employee employee = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;

            //{860BDCF8-A6AC-443a-B48E-517F78F18193}
            string opercode = FS.FrameWork.Management.Connection.Operator.ID.ToString().TrimStart('0');
            //string operLength = this.GetCodeLength();
            //if (operLength == "4")
            //{
            //    opercode = FS.FrameWork.Management.Connection.Operator.ID.Substring(2, 4);
            //}
            //{9838CF3F-7C89-4e10-89C5-38C3554CAEE4}
            //string s = " http://192.168.34.7/ORCService";
            string s = this.GetEMRServerPath();
            s += " " + opercode + " emr";

            string loadPath = GetEMRProcessPath();

            loadPath = "D:\\MandalaT DoqLei\\DoqLei.exe";

            if (string.IsNullOrEmpty(loadPath))
            {
                MessageBox.Show("请维护电子病历的启动路径！");
                return -1;
            }
            System.Diagnostics.ProcessStartInfo pInfo = new System.Diagnostics.ProcessStartInfo(loadPath, s);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = pInfo;
            pInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            process.Start();
            FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "\r\n正在启动电子病历功能，请稍后！", ToolTipIcon.Info);

            Classes.LogManager.Write("【结束启动电子病历界面前 动作】");
            return -1;
        }

        #endregion

        //{C826392C-D6A8-4ba5-95C9-7591460BE490}
        private string GetEMRProcessPath()
        {
            string strProcessPath = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\EmrInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\EmrInterface.xml");
                System.Xml.XmlNode node = file.SelectSingleNode("Config/Path");
                if (node != null)
                {
                    strProcessPath = node.InnerText;
                }
            }
            return strProcessPath;
        }

        //{9838CF3F-7C89-4e10-89C5-38C3554CAEE4}
        private string GetEMRServerPath()
        {
            string strServerPath = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\EmrInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\EmrInterface.xml");
                System.Xml.XmlNode node = file.SelectSingleNode("Config/Url1");
                if (node != null)
                {
                    strServerPath = node.InnerText;
                }
            }
            return strServerPath;
        }

        //{9838CF3F-7C89-4e10-89C5-38C3554CAEE4}
        private string GetCodeLength()
        {
            string strServerPath = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\EmrInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\EmrInterface.xml");
                System.Xml.XmlNode node = file.SelectSingleNode("Config/CodeLength");
                if (node != null)
                {
                    strServerPath = node.InnerText;
                }
            }
            return strServerPath;
        }

    }
}
