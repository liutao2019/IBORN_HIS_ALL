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
using System.Web;
using Neusoft.HisCrypto;


namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucFetalSupervisionBrowser : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //{1954741E-939E-4c1e-913E-13533E8E7004}
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly = false;

        WebView browser1;



        public ucFetalSupervisionBrowser()
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

                if (string.IsNullOrEmpty(patient.PID.CardNO))
                {
                   string url = "";
                }
                
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
                var userName = "";
                var passWord = "";
                string url = GetOBISUrl(out userName, out passWord);

                //var userName = "18613016720";//医生账号 16626435247,18613016720,18529428144
                //var passWord = "123456";//医生密码

                var loginType = "users";//类型，users为医生端
                var clinicNo = register.PID.CardNO;//门诊号

                var obj = new
                {
                    userName,
                    passWord,
                    loginType,
                    clinicNo
                };
                var doc = Newtonsoft.Json.JsonConvert.SerializeObject(obj);//序列化对象为字符串
                var doctor = HttpUtility.UrlEncode(doc, System.Text.Encoding.GetEncoding(65001));//对字符串UrlEncode

                

                url += "?doctor=" + doctor;

                //拼接带参跳转
                //string url = string.Format("http://120.24.108.131/website?doctor={0}", doctor);
              

                OpenBrowserUrl(url);

            }
            catch (Exception ex)
            {

            }
        }

        //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
        private string GetOBISUrl(out string name,out string pwd)
        {
            // @"http://120.24.108.131/website";
            string strUrl = "";
            name = "";
            pwd = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\FetalSuperInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\FetalSuperInterface.xml");
                System.Xml.XmlNode node = file.SelectSingleNode("Config/Url");
                System.Xml.XmlNode node1 = file.SelectSingleNode("Config/Pid");
                System.Xml.XmlNode node2 = file.SelectSingleNode("Config/Pwd");

                if (node != null)
                {
                    strUrl = node.InnerText;
                }
                if (node1 != null)
                {
                    name = node1.InnerText;
                }
                if (node2 != null)
                {
                    pwd = node2.InnerText;
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
