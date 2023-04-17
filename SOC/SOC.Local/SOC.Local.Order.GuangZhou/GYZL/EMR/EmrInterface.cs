using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace FS.SOC.Local.Order.GuangZhou.GYZL.EMR
{
    class EmrInterface : FS.HISFC.BizProcess.Interface.EMR.IEMR
    {
        private string errMsg = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }
        }

        #region IEMR成员
        public int EMRRegister(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, FS.HISFC.Models.Base.ServiceTypes patientType)
        {
            try
            {
                string loadPath = "";
                if (System.IO.File.Exists(Application.StartupPath + "\\EmrInterface.xml"))
                {
                    XmlDocument file = new XmlDocument();
                    file.Load(Application.StartupPath + "\\EmrInterface.xml");
                    XmlNode node = file.SelectSingleNode("Config/Path");
                    if (node != null)
                    {
                        loadPath = node.InnerText;
                    }
                }

                //{E3ED6553-0A5D-43e9-8D0C-DD24E42E8258}
                if (string.IsNullOrEmpty(loadPath))
                {
                    this.errMsg = "请维护EMR程序的启动目录，配置文件为根目录“EmrInterface.xml”";
                    return -1;
                }
                System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(loadPath);//"PatientEls.dll");

                if (patient == null)
                {
                    this.errMsg = "请选择患者！";
                    return -1;
                }
                //{E3ED6553-0A5D-43e9-8D0C-DD24E42E8258}
                //Type[] t = asm.GetTypes();
                //foreach (Type tmp in t)
                //{
                //    if (tmp.Name == "HisExe")
                //    {
                //        object o = asm.GetType();
                //        System.Reflection.MethodInfo method = tmp.GetMethod("Show");
                //        object[] parms = new object[] { patient.PID.CardNO };
                //        method.Invoke(o, parms);
                //    }
                //}

                //D:\MandalaT DoqLei\LordInterop.exe http://192.168.34.7/ORCService 0127 emr HOSPITAL 244230 Name:赵旭|Sex:男|Birthday:1993-07-19
                //|Age:23|HIS内部标识:244230|HIS外部标识:201619511|住院号:201619511|Department:普外科

                string opercode = FS.FrameWork.Management.Connection.Operator.ID;
                FS.HISFC.Models.Base.Employee employee = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;

                if(opercode.Length == 6 )
                {
                    opercode = FS.FrameWork.Management.Connection.Operator.ID.Substring(2, 4);
                }
                string name = patient.Name;
                string sex = patient.Sex.Name;
                string birthday = patient.Birthday.ToShortDateString();
                string age = patient.Age;
                string hisinside = patient.ID;
                string hisoutside = patient.PID.PatientNO;
                string patientno = patient.PID.PatientNO;
                string department = ((FS.HISFC.Models.RADT.PatientInfo)patient).PVisit.PatientLocation.Dept.Name;
                string bedno = ((FS.HISFC.Models.RADT.PatientInfo)patient).PVisit.PatientLocation.Bed.ID;
                string department2 = ((FS.HISFC.Models.RADT.PatientInfo)patient).PVisit.PatientLocation.Dept.Name;
                string nursecell = ((FS.HISFC.Models.RADT.PatientInfo)patient).PVisit.PatientLocation.NurseCell.Name;
                string bedno2 = ((FS.HISFC.Models.RADT.PatientInfo)patient).PVisit.PatientLocation.Bed.ID;
                string nursecell2 = ((FS.HISFC.Models.RADT.PatientInfo)patient).PVisit.PatientLocation.NurseCell.Name;
                string indate = ((FS.HISFC.Models.RADT.PatientInfo)patient).PVisit.InTime.ToString();
                string intime = ((FS.HISFC.Models.RADT.PatientInfo)patient).PVisit.InTime.ToString();
                string indate2 = ((FS.HISFC.Models.RADT.PatientInfo)patient).PVisit.InTime.ToString();


                string s = " http://192.168.34.7/ORCService";
                s += " " + opercode + " emr";
                if (employee.EmployeeType.ID.ToString() == "D")
                {
                    s += " HOSPITAL " + hisinside;
                }
                else
                {
                    s += " NURSE " + hisinside;
                }
                s += " Name:" + name;
                s += "|Sex:" + sex;
                s += "|Birthday:" + birthday;
                s += "|Age:" + age;
                s += "|HIS内部标识:" + hisinside;
                s += "|HIS外部标识:" + hisoutside;
                s += "|住院号:" + patientno;
                s += "|Department:" + department;
                s += "|床号:" + bedno;
                s += "|科别:" + department2;
                s += "|病区:" + nursecell;
                s += "|入院床号:" + bedno2;
                s += "|入院病区:" + nursecell2;
                s += "|入院日期:\"" + indate + "\"";
                s += "|入院时间:\"" + intime + "\"";
                s += "|入区日期:\"" + indate2 + "\"";

                System.Diagnostics.Process myprocess = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(loadPath, s);
                myprocess.StartInfo = startInfo;
                myprocess.StartInfo.UseShellExecute = false;
                myprocess.Start();
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "提示", "\r\n正在启动电子病历功能，请稍后！", ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return -1;
            }

            return 1;
        }
        #endregion
    }
}
