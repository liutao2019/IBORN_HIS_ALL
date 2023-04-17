using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;


namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucOBISBrowsertest : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //{1954741E-939E-4c1e-913E-13533E8E7004}
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly = false;

        WebView browser1;



        public ucOBISBrowsertest()
        {
            InitializeComponent();
            //browser1 = new WebView("http://192.168.34.9:8090/obis/desk/index?id=0000500003&doctorId=009011&doctor=李萍", new CefSharp.BrowserSettings());
            //this.Controls.Add(browser1);
            //browser1.Dock = DockStyle.Fill;
            //browser1.Visible = true;
            //browser1.TabIndex = 1;
        }

        //{9231D008-9415-40ce-9D4D-C14AD3DCED16}
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
                    //this.ShowData(patient);
                }
                else
                {
                    //this.ShowData(patient);
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
                    //this.ShowData(register);
                }
            }
        }

        /// <summary>
        /// 传递病人实体   {39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
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
                    if (this.Patient == neuObject as FS.HISFC.Models.RADT.PatientInfo)
                    {
                        return base.OnSetValue(neuObject, e);
                    }
                    this.Patient = neuObject as FS.HISFC.Models.RADT.PatientInfo;
                }
                else if (neuObject is FS.HISFC.Models.Registration.Register)
                {
                    if (this.Register == (neuObject as FS.HISFC.Models.Registration.Register))
                    {
                        return base.OnSetValue(neuObject, e);
                    }
                    this.Register = neuObject as FS.HISFC.Models.Registration.Register;
                }
                else
                {

                }
            }
            else
            {
                if (neuObject != null)
                {
                    if (neuObject is FS.HISFC.Models.RADT.PatientInfo)
                    {
                        if (this.Patient == neuObject as FS.HISFC.Models.RADT.PatientInfo)
                        {
                            return base.OnSetValue(neuObject, e);
                        }
                        this.Patient = neuObject as FS.HISFC.Models.RADT.PatientInfo;
                    }
                    else if (neuObject is FS.HISFC.Models.Registration.Register)
                    {
                        if (this.Register == (neuObject as FS.HISFC.Models.Registration.Register))
                        {
                            return base.OnSetValue(neuObject, e);
                        }
                        this.Register = neuObject as FS.HISFC.Models.Registration.Register;
                    }
                    else
                    {

                    }
                }
            }
            return base.OnSetValue(neuObject, e);
        }

        /// <summary>
        /// 查询数据 {39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void ShowData(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                browser1 = new WebView();
                //webBrowser1.ScriptErrorsSuppressed = true;
                //string url = @"http://192.168.34.9:8090/obis/desk/index?id=0000500003&doctorId=009011&doctor=李萍";
                //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}

                //http://192.168.34.18:8900/single/labour-record?empid=admin&patId=&inpatientNO=22222&inpatientSum=2&adminDate=&idType=&idNO=


                string info = CacheManager.InPatientMgr.GetInDateByPatientNo(patient.PID.ID);
                string url = this.GetOBISUrl("3");
                url += "&empId=" + FS.FrameWork.Management.Connection.Operator.ID;
                url += "&patId=";
                url += "&inpatientNO=" + patient.PID.ID;
                url += "&inpatientSum=";
                url += "&adminDate=" + info;
                url += "&idType=" + patient.IDCardType;
                url += "&idNO=" + patient.IDCard;

                if (string.IsNullOrEmpty(patient.PID.CardNO))
                {
                    url = "";
                }

                OpenBrowserUrl(url);
                //System.Diagnostics.Process.Start("chrome", url);

                //browser1 = new WebView(url, new CefSharp.BrowserSettings());
                //this.Controls.Add(browser1);
                //browser1.Dock = DockStyle.Fill;
                //browser1.Visible = true;
                // browser1.TabIndex = 1;
                //  browser1 .
                //webBrowser1.Navigate(url);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 查询数据
        /// {204ED852-A592-4a15-9BFE-0E7C1E47BB87}
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void ShowData(FS.HISFC.Models.Registration.Register register)
        {
            try
            {
                //webBrowser1.ScriptErrorsSuppressed = true;
                ////string url = @"http://192.168.34.9:8090/obis/desk/index?";
                ////{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
                string url = this.GetOBISUrl("2");
                //url += "id=" + register.PID.CardNO;  //id=99999 患者卡号
                //url += "&doctor=" + FS.FrameWork.Management.Connection.Operator.Name; //&doctor=kjajga
                //url += "&doctorID=" + FS.FrameWork.Management.Connection.Operator.ID; //&doctorId=ffdd";

                url += "moduleName=prenatalVisit";
                url += "&patId=" + register.PID.CardNO;
                url += "&empId=" + FS.FrameWork.Management.Connection.Operator.ID;
                url += "&serialNo=" + register.ID;
                url += "&idNo=" + register.IDCard;

                if (string.IsNullOrEmpty(register.PID.CardNO))
                {
                    url = "";
                }

                FS.HISFC.Components.OutpatientFee.Class.LogManager.Write("URL跳转:  " + url + "    "+ DateTime.Now.ToString());

                OpenBrowserUrl(url);

            }
            catch (Exception ex)
            {

            }
        }

        //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
        private string GetOBISUrl(string num)
        {
            // @"http://192.168.34.9:8090/obis/desk/index?";
            string strUrl = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\OBISInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\OBISInterface.xml");
                System.Xml.XmlNode node = null;
                if (num == "2")
                {
                    node = file.SelectSingleNode("Config/Url2");
                }
                else
                {
                    node = file.SelectSingleNode("Config/Url3");
                }

                if (node != null)
                {
                    strUrl = node.InnerText;
                }
            }

            return strUrl;
        }

        public static void OpenBrowserUrl(string url)
        {
            var result = System.Diagnostics.Process.Start("chrome.exe", url);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (Register != null)
            {

                ShowData(Register);
            }
            else
            {
                if (Patient != null)
                {
                    ShowData(Patient);
                }
            }



        }
    }
}
