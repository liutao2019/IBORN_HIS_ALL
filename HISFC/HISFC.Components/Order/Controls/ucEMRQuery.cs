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
    public partial class ucEMRQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucEMRQuery()
        {
            InitializeComponent();
            //{4067547D-0A7E-4dd4-9EEE-6372E03963E0}
            this.fpPatientNO.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpPatientNO_CellClick);
        }

        HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();

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
                this.fpPatientNO_Sheet1.RowCount = 0;
                webBrowser1.ScriptErrorsSuppressed = true;
                string url = this.GetEMRUrl();

                if (string.IsNullOrEmpty(patient.PID.CardNO))
                {
                    url += "0";
                }
                else
                {
                    List<string> patientList = new List<string>();
                    System.Collections.ArrayList inpatientList = inpatientMgr.GetPatientInfoByCardNO(patient.PID.CardNO);

                    if (inpatientList != null && inpatientList.Count > 0)
                    {
                        //{4067547D-0A7E-4dd4-9EEE-6372E03963E0}
                        foreach (FS.HISFC.Models.RADT.PatientInfo pat in inpatientList)
                        {
                            if (!patientList.Contains(pat.PID.PatientNO))
                            {
                                patientList.Add(pat.PID.PatientNO);
                                this.fpPatientNO_Sheet1.AddRows(this.fpPatientNO_Sheet1.RowCount, 1);
                                this.fpPatientNO_Sheet1.Cells[this.fpPatientNO_Sheet1.RowCount - 1, 0].Text = pat.PID.PatientNO;
                            }
                        }

                        FS.HISFC.Models.RADT.PatientInfo inpatient = inpatientList[0] as FS.HISFC.Models.RADT.PatientInfo;
                        if (inpatient != null && !string.IsNullOrEmpty(inpatient.PID.PatientNO))
                        {
                            url += inpatient.PID.PatientNO;
                        }
                    }
                    else
                    {
                        url += "0";
                    }
                }

                webBrowser1.Navigate(url);
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
                this.fpPatientNO_Sheet1.RowCount = 0;
                webBrowser1.ScriptErrorsSuppressed = true;
                string url = this.GetEMRUrl();

                if (string.IsNullOrEmpty(register.PID.CardNO))
                {
                    url += "0";
                }
                else
                {
                    List<string> patientList = new List<string>();
                    System.Collections.ArrayList inpatientList = inpatientMgr.GetPatientInfoByCardNO(register.PID.CardNO);

                    if (inpatientList != null && inpatientList.Count > 0)
                    {
                        //{4067547D-0A7E-4dd4-9EEE-6372E03963E0}
                        foreach (FS.HISFC.Models.RADT.PatientInfo pat in inpatientList)
                        {
                            if (!patientList.Contains(pat.PID.PatientNO))
                            {
                                patientList.Add(pat.PID.PatientNO);
                                this.fpPatientNO_Sheet1.AddRows(this.fpPatientNO_Sheet1.RowCount, 1);
                                this.fpPatientNO_Sheet1.Cells[this.fpPatientNO_Sheet1.RowCount - 1, 0].Text = pat.PID.PatientNO;
                            }
                        }

                        FS.HISFC.Models.RADT.PatientInfo inpatient = inpatientList[0] as FS.HISFC.Models.RADT.PatientInfo;
                        if (inpatient != null && !string.IsNullOrEmpty(inpatient.PID.PatientNO))
                        {
                            url += inpatient.PID.PatientNO;
                        }
                        else
                        {
                            url += "0";
                        }
                    }
                    else
                    {
                        url += "0";
                    }
                }

                webBrowser1.Navigate(url);
            }
            catch (Exception ex)
            {

            }
        }

        private void fpPatientNO_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                webBrowser1.ScriptErrorsSuppressed = true;
                string url = this.GetEMRUrl();
                string patientNO = this.fpPatientNO_Sheet1.Cells[e.Row, e.Column].Text;

                if (!string.IsNullOrEmpty(patientNO))
                {
                    url += patientNO;
                }
                else
                {
                    url += "0";
                }

                webBrowser1.Navigate(url);
            }
            catch (Exception ex)
            {

            }


        }

        //{3D56F769-FF06-4c5d-A45A-6A5C33AB550C}
        private string GetEMRUrl()
        {
            string strUrl = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\EmrInterface.xml"))
            {
                System.Xml.XmlDocument file = new System.Xml.XmlDocument();
                file.Load(Application.StartupPath + "\\EmrInterface.xml");
                System.Xml.XmlNode node = file.SelectSingleNode("Config/Url");
                if (node != null)
                {
                    strUrl = node.InnerText;
                }
            }
            return strUrl;
        }



        private string GetNewEMRUrl()
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


        public static void OpenBrowserUrl(string url)
        {
            var result = System.Diagnostics.Process.Start("chrome.exe", url);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string cardno = "";
            if (register == null && patient == null)
            {
                return;
            }
            else
            {
                if (register != null)
                {
                    cardno = register.PID.CardNO;
                }
                else
                {
                    cardno = patient.PID.CardNO;
                }
            }

            string url = this.GetNewEMRUrl();
            url += getDepUrl();
            url += "&cardNo=" + cardno;

            OpenBrowserUrl(url);
        }
    }
}
