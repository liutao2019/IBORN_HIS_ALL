using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls
{

    public partial class ucEMRNEWQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        //传递患者信息类
        private FS.HISFC.Models.RADT.PatientInfo patient;
        /// <summary>
        /// 页面属性，接收传过来的患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
                if (patient != null)
                {
                    this.ShowData(patient);
                }
            }
        }

        FS.HISFC.Models.Registration.Register register;
        /// <summary>
        /// 页面属性，接收传过来的患者信息
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.register;
            }
            set
            {
                this.register = value;
                if (register != null)
                {
                    this.ShowData(register);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ucEMRNEWQuery()
        {
            InitializeComponent();
        }

       

        /// <summary>
        /// 传递病人实体
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (this.tv != null)
            {
                if (this.tv.CheckBoxes == true)
                {
                    this.tv.CheckBoxes = false;
                }
                this.tv.ExpandAll();
                if (neuObject is FS.HISFC.Models.RADT.PatientInfo)
                {
                    this.Patient = neuObject as FS.HISFC.Models.RADT.PatientInfo;
                }
                else if (neuObject is FS.HISFC.Models.Registration.Register)
                {
                    this.Register = neuObject as FS.HISFC.Models.Registration.Register;
                }
                else
                {

                }
            }
            return base.OnSetValue(neuObject, e);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void ShowData(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            string url = this.GetEMRUrl();
            url += getDepUrl();
            url += "&cardNo=" + patient.PID.CardNO;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(url);
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void ShowData(FS.HISFC.Models.Registration.Register register)
        {
            string url = this.GetEMRUrl();
            url += getDepUrl();
            url += "&cardNo=" + register.PID.CardNO;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(url);
        }


        private string GetEMRUrl()
        {
            string strUrl = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\EmrNewInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\EmrNewInterface.xml");
                System.Xml.XmlNode node = file.SelectSingleNode("Config/Url");
                if (node != null)
                {
                    strUrl = node.InnerText;
                }
            }
            return strUrl;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string url = this.GetEMRUrl();
            url += getDepUrl();
            url += "&cardNo=" + register.PID.CardNO;

            OpenBrowserUrl(url);

        }

        public static void OpenBrowserUrl(string url)
        {
            var result = System.Diagnostics.Process.Start("chrome.exe", url);
        }


        private string getDepUrl()
        {
            FS.HISFC.Models.Base.Employee employee = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;
            FS.HISFC.Models.Base.Department currDept = (FS.HISFC.Models.Base.Department)(employee.Dept);
            string urls = "";
            if (currDept.HospitalName.Contains("顺德"))
            {
                urls = "hosCode=PDY01165944060617A5182&userCode=00000";
            }
            else
            {
                urls = "hosCode=914401013047761900&userCode=666666";
            }
            return urls;
        }
    }
}
