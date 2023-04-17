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
    public partial class frmShowNewEMR : FS.FrameWork.WinForms.Forms.frmBaseForm, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public frmShowNewEMR()
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
            FS.HISFC.Models.Base.Department currDept = (FS.HISFC.Models.Base.Department)(employee.Dept);

            
            string opercode = FS.FrameWork.Management.Connection.Operator.ID.ToString();
            string opername = FS.FrameWork.Management.Connection.Operator.Name.ToString();

            string s = "mtlemr://HosCode=914401013047761900&HosName=广州爱博恩妇产医院";
            if (currDept.HospitalName.Contains("顺德"))
            {
                s = "mtlemr://HosCode=PDY01165944060617A5182&HosName=顺德爱博恩妇产医院";
            }
           
            s += "&DeptCode=" + currDept.ID;
            s += "&DeptName=" + currDept.Name;
            s += "&UserCode=" + opercode;
            s += "&UserName=" + opername;
            s += "&PatientId=";
            s += "&AppType=" + currDept.Name == "妇产科" ? "In" : "Out";


            string loadPath = GetEMRProcessPath();

            if (string.IsNullOrEmpty(loadPath))
            {
                loadPath = "D:\\新版电子病历\\Mandala.AutoUpdate.exe";
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
        
        private string GetEMRProcessPath()
        {
            string strProcessPath = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\EmrNewInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\EmrNewInterface.xml");
                System.Xml.XmlNode node = file.SelectSingleNode("Config/Path");
                if (node != null)
                {
                    strProcessPath = node.InnerText;
                }
            }
            return strProcessPath;
        }
       
        private string GetEMRServerPath()
        {
            string strServerPath = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\EmrNewInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\EmrNewInterface.xml");
                System.Xml.XmlNode node = file.SelectSingleNode("Config/Url");
                if (node != null)
                {
                    strServerPath = node.InnerText;
                }
            }
            return strServerPath;
        }
      
        private string GetCodeLength()
        {
            string strServerPath = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\EmrNewInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\EmrNewInterface.xml");
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
