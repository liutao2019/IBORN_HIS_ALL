using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SOC.Local.LisInterface.ZDWY
{
    public partial class frmLisResultShow : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmLisResultShow()
        {
            InitializeComponent();
        }

        string url = string.Empty;
        #region ILis 成员

        string errMsg = string.Empty;
        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }
        }

        /// <summary>
        /// 是否门诊系统
        /// </summary>
        private bool isOutPatient = true;

        /// <summary>
        /// 当前患者基本信息
        /// </summary>
        private FS.HISFC.Models.RADT.Patient myPatient = null;

        /// <summary>
        /// 类别  0 - 门诊号；1 - 住院号
        /// </summary>
        private string patientType = "";

        /// <summary>
        /// 姓名
        /// </summary>
        string patientName = "";

        /// <summary>
        /// 住院号、门诊号
        /// </summary>
        string patientNo = "";

        /// <summary>
        /// 住院、门诊流水号
        /// </summary>
        string clinicNo = "";

        /// <summary>
        /// 科室
        /// </summary>
        string deptCode = "";

        ///// <summary>
        ///// 是否已释放
        ///// </summary>
        //private bool isDespose = false;

        //public bool IsDespose
        //{
        //    get
        //    {
        //        return isDespose;
        //    }
        //    set
        //    {
        //        isDespose = value;
        //    }
        //}

        #region 接口说明

        /**
         * Lis查询接口说明

            地址：http://172.16.92.203:7777/transaction/search_for_his/list.asp

            参数列表：

            ptnt_no_type : 病历号类型   0 - 门诊号；1 - 住院号
            ptnt_name : 姓名
            ptnt_no : 病历号		门诊为就诊卡号，住院为住院号
            sqnc_no : 就诊流水号
            dept_key : 科室编码		仅对住院查询有用。


            ptnt_no_type,sqnc_no,ptnt_name为必选项。必须传递

            dept_key仅在住院查询时为必选项，门诊查询时可不传。

            当传递ptnt_no参数时将返回该病历号的所有检验报告，不论是否当次就诊。

            例子：

            http://172.16.92.203:7777/transaction/search_for_his/list.asp?ptnt_no_type=1&ptnt_name=陈渭泉&sqnc_no=&ptnt_no=117379&dept_key=204



            其中sqnc_no暂不支持,调试可直接传空值,需要等到上线联调之后才会用到.
         * 
         * */
        #endregion

        
        private string GetURL()
        {
            string strURL = "";
            if (System.IO.File.Exists(Application.StartupPath + "\\LisInterface.xml"))
            {
                XmlDocument file = new XmlDocument();
                file.Load(Application.StartupPath + "\\LisInterface.xml");
                XmlNode node = file.SelectSingleNode("Config/ResultURL");
                if (node != null)
                {
                    strURL = node.InnerText;
                }
            }

            if (string.IsNullOrEmpty(strURL))
            {
                strURL = "http://172.16.92.203:7777/transaction/search_for_his/list.asp?";
            }
            return strURL;
        }

        public int ShowResultByPatient()
        {
            if (this.wbResultShow.IsDisposed)
            {
                this.wbResultShow = new WebBrowser();
            }
            if (string.IsNullOrEmpty(this.myPatient.ID))
            {
                return 1;
            }

            string strURL = GetURL();

            //测试数据
            patientType = "1";
            patientName = "陈渭泉";
            clinicNo = "";
            patientNo = "11737";
            deptCode = "204";

            //查看全部
            if (this.cbxIsAll.Checked)
            {
                this.lblDeptInfo.Visible = false;
                this.lblDeptAll.Visible = true;

                url = strURL + "ptnt_no_type=" + patientType + "&ptnt_name=" + patientName + "&sqnc_no=" + clinicNo + "&ptnt_no=" + patientNo + "&dept_key=" + "";
            }
            else
            {
                this.lblDeptInfo.Visible = true;
                this.lblDeptAll.Visible = false;

                url = strURL + "ptnt_no_type=" + patientType + "&ptnt_name=" + patientName + "&sqnc_no=" + clinicNo + "&ptnt_no=" + patientNo + "&dept_key=" + deptCode;
            }

            this.wbResultShow.Navigate(url);
            this.WindowState = FormWindowState.Maximized;
            //this.ShowDialog();
            return 1;
        }

        #endregion

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowResultByPatient();
        }

        #region ILis 成员

        public int SetPatient(FS.HISFC.Models.RADT.Patient patient)
        {
            if (patient == null)
            {
                errMsg = "患者信息为空！";
                return -1;
            }

            this.wbResultShow.Refresh(WebBrowserRefreshOption.Normal);

            myPatient = patient;
            this.patientName = myPatient.Name;

            FS.HISFC.BizLogic.Admin.FunSetting funMgr = new FS.HISFC.BizLogic.Admin.FunSetting();
            try
            {
                if (myPatient.GetType() == typeof(FS.HISFC.Models.Registration.Register))
                {
                    FS.HISFC.Models.Registration.Register regObj = (FS.HISFC.Models.Registration.Register)myPatient;
                    isOutPatient = true;
                    patientNo = regObj.PID.CardNO.TrimStart('0');
                    this.patientType = "0";
                    this.clinicNo = regObj.ID;
                    this.deptCode = "";

                    this.lblDeptInfo.Text = "  科室：" + regObj.SeeDoct.Dept.Name;
                    this.lblPatientInfo.Text = "类型：门诊" + "  门诊号：" + myPatient.PID.PatientNO.TrimStart('0') + "  姓名：" + myPatient.Name + "  性别：" + myPatient.Sex.Name + "  年龄：" + funMgr.GetAge(myPatient.Birthday, regObj.DoctorInfo.SeeDate);
                }
                else
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = (FS.HISFC.Models.RADT.PatientInfo)myPatient;
                    isOutPatient = false;
                    patientNo = patientInfo.PID.PatientNO.TrimStart('0');
                    this.patientType = "1";
                    this.clinicNo = patientInfo.ID;
                    this.deptCode = patientInfo.PVisit.PatientLocation.Dept.ID;

                    this.lblDeptInfo.Text = "  科室：" + patientInfo.PVisit.PatientLocation.Dept.Name;
                    this.lblPatientInfo.Text = "类型：住院" + "  住院号：" + myPatient.PID.PatientNO.TrimStart('0') + "  姓名：" + myPatient.Name + "  性别：" + myPatient.Sex.Name + "  年龄：" + funMgr.GetAge(myPatient.Birthday, patientInfo.PVisit.InTime);
                }
            }
            catch
            {
                FS.HISFC.Models.RADT.PatientInfo patientInfo = (FS.HISFC.Models.RADT.PatientInfo)myPatient;
                if (patientInfo == null)
                {
                    errMsg = "患者信息为空！";
                    return -1;
                }

                isOutPatient = false;
                patientNo = patientInfo.PID.PatientNO.TrimStart('0');
                this.patientType = "1";
                this.clinicNo = patientInfo.ID;
                this.deptCode = patientInfo.PVisit.PatientLocation.Dept.ID;

                this.lblDeptInfo.Text = "  科室：" + patientInfo.PVisit.PatientLocation.Dept.Name;
                this.lblPatientInfo.Text = "类型：住院" + "  住院号：" + myPatient.PID.PatientNO.TrimStart('0') + "  姓名：" + myPatient.Name + "  性别：" + myPatient.Sex.Name + "  年龄：" + funMgr.GetAge(myPatient.Birthday, patientInfo.PVisit.InTime);
            }
            return 1;
        }

        #endregion
    }
}
