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
    //{9231D008-9415-40ce-9D4D-C14AD3DCED16}
    public partial class ucLisResQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
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

        FS.HISFC.Models.Registration.Register register;/// <summary>
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
        public ucLisResQuery()
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
            //{1AC2C273-460D-4f2f-B121-4492731DBC0D}
            string url = @"http://192.168.34.8:8089/web/frmreportselect.aspx?p_id=123456&name=1&sDate=2017-09-01&eDate=2017-09-18&depcode=5001&ShowSearchbar=false";
            //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
            url = this.GetLisUrl();
            url += "&p_id=" + patient.PID.PatientNO;
            //url += "&name=" + patient.Name;
            url += "&sDate=" + patient.PVisit.InTime.Date.AddMonths(-6).ToShortDateString();
            url += "&eDate=" + patient.PVisit.InTime.Date.AddYears(1).ToShortDateString();
            //url += "&depcode=" + patient.PVisit.PatientLocation.Dept.ID;

            //{A0C660BA-1983-49f7-9F07-A3313684B707}
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(url);
        }


        /// <summary>
        /// 查询数据
        /// {204ED852-A592-4a15-9BFE-0E7C1E47BB87}
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void ShowData(FS.HISFC.Models.Registration.Register register)
        {
            string url = @"http://192.168.34.8:8089/web/frmreportselect.aspx?p_id=123456&name=1&sDate=2017-09-01&eDate=2017-09-18&depcode=5001&ShowSearchbar=false";
            //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
            url = this.GetLisUrl();
            url += "&p_id=" + register.PID.CardNO;
            //url += "&name=" + patient.Name;
            url += "&sDate=" + register.DoctorInfo.SeeDate.Date.AddMonths(-6).ToShortDateString();
            url += "&eDate=" + register.DoctorInfo.SeeDate.Date.AddYears(1).ToShortDateString();
            //url += "&depcode=" + register.DoctorInfo.Templet.Dept.ID;

            //{A0C660BA-1983-49f7-9F07-A3313684B707}
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(url);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void ShowData(FS.HISFC.Models.RADT.PatientInfo patient,DateTime beginTime,DateTime endTime)
        {
            string url = @"http://192.168.34.8:8089/web/frmreportselect.aspx?p_id=123456&name=1&sDate=2017-09-01&eDate=2017-09-18&depcode=5001&ShowSearchbar=false";
            //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
            url = this.GetLisUrl();
            url += "&p_id=" + patient.PID.PatientNO;
            //url += "&name=" + patient.Name;
            url += "&sDate=" + beginTime.ToShortDateString();
            url += "&eDate=" + endTime.ToShortDateString();
            //url += "&depcode=" + patient.PVisit.PatientLocation.Dept.ID;

            //{A0C660BA-1983-49f7-9F07-A3313684B707}
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(url);
        }


        /// <summary>
        /// 查询数据
        /// {204ED852-A592-4a15-9BFE-0E7C1E47BB87}
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void ShowData(FS.HISFC.Models.Registration.Register register, DateTime beginTime, DateTime endTime)
        {
            string url = @"http://192.168.34.8:8089/web/frmreportselect.aspx?p_id=123456&name=1&sDate=2017-09-01&eDate=2017-09-18&depcode=5001&ShowSearchbar=false";
            //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
            url = this.GetLisUrl();
            url += "&p_id=" + register.PID.CardNO;
            //url += "&name=" + patient.Name;
            url += "&sDate=" + beginTime.ToShortDateString();
            url += "&eDate=" + endTime.ToShortDateString();
            //url += "&depcode=" + register.DoctorInfo.Templet.Dept.ID;

            //{A0C660BA-1983-49f7-9F07-A3313684B707}
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(url);
        }

        //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
        private string GetLisUrl()
        {
            // @"http://192.168.34.8:8089/web/frmreportselect.aspx?ShowSearchbar=true";
            string strUrl = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\LisGeneralInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\LisGeneralInterface.xml");
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
            string url = this.GetLisUrl();
            url += "&p_id=" + register.PID.CardNO;
            url += "&sDate=" + register.DoctorInfo.SeeDate.Date.AddMonths(-6).ToShortDateString();
            url += "&eDate=" + register.DoctorInfo.SeeDate.Date.AddYears(1).ToShortDateString();

            OpenBrowserUrl(url);
        }


        public static void OpenBrowserUrl(string url)
        {
            var result = System.Diagnostics.Process.Start("chrome.exe", url);
        }
    }
}
