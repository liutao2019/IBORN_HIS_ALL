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
    public partial class ucPacsResQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

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
                    this.ShowData(patient);
                }
                else
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
        /// 初始化
        /// </summary>
        public ucPacsResQuery()
        {
            InitializeComponent();
        }

        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            MessageBox.Show("页面已载入，但网页上存在错误！");
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
            try
            {
                webBrowser1.ScriptErrorsSuppressed = true;
                //string url = @"http://192.168.34.6:82/xds/index.php?appuser=1&type=healthcarefacility&value=123";
                //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
                string url = this.GetPacsUrl();
                //url += @"&type1=PatientType&value1=住院";
                url += @"&type1=HISCODE1&value1=" + patient.PID.CardNO;
                //url += @"&type2=HISCODE3&value2=" + patient.ID;
                webBrowser1.Navigate(url);

                //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
                //string s = key.GetValue("").ToString();

                //s就是你的默认浏览器，不过后面带了参数，把它截去，不过需要注意的是：不同的浏览器后面的参数不一样！  
                //"D:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"  
                //System.Diagnostics.Process.Start(s.Substring(0, s.Length - 8),url);
            }
            catch(Exception ex)
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
                webBrowser1.ScriptErrorsSuppressed = true;
                //string url = @"http://192.168.34.6:82/xds/index.php?appuser=1&type=healthcarefacility&value=123";
                //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
                string url = this.GetPacsUrl();
                //url += @"&type1=PatientType&value1=住院";
                url += @"&type1=HISCODE1&value1=" + register.PID.CardNO;
                //url += @"&type2=HISCODE3&value2=" + register.ID;
                webBrowser1.Navigate(url);

                //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
                //string s = key.GetValue("").ToString();

                //s就是你的默认浏览器，不过后面带了参数，把它截去，不过需要注意的是：不同的浏览器后面的参数不一样！  
                //"D:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"  
                //System.Diagnostics.Process.Start(s.Substring(0, s.Length - 8),url);
            }
            catch (Exception ex)
            {

            }
        }

        //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
        private string GetPacsUrl()
        {
            //@"http://192.168.34.6:82/xds/index.php?appuser=1&type=healthcarefacility&value=123"
            string strUrl = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\PacsInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\PacsInterface.xml");
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
            string url = this.GetPacsUrl();
            url += @"&type1=HISCODE1&value1=" + register.PID.CardNO;

            OpenBrowserUrl(url);
        }

        public static void OpenBrowserUrl(string url)
        {
            var result = System.Diagnostics.Process.Start("chrome.exe", url);
        }

    }
}
