using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neusoft.HISFC.Models.Base;
using System.Collections;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;
using System.Windows.Forms;
using System.Xml;

namespace Neusoft.SOC.Local.RADT.GuangZhou
{
    public  class Function
    {
        private static string err = "";
        public static string Err
        {
            get
            {
                return err;
            }
        }

        #region 综合函数

        /// <summary>
        /// 根据身份证号获取生日
        /// </summary>
        /// <param name="idNO">身份证号</param>
        /// <returns></returns>
        public static int GetBirthDayFromIdNO(string idNO, ref string datestr, ref Neusoft.FrameWork.Models.NeuObject sexobj, ref string err)
        {

            if (Neusoft.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref err) < 0)
            {
                return -1;
            }
            if (idNO.Length == 15)
            {
                idNO = Neusoft.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            datestr = idNO.Substring(6, 8);
            string year = datestr.Substring(0, 4);
            string month = datestr.Substring(4, 2);
            string day = datestr.Substring(6, 2);
            datestr = year + "-" + month + "-" + day;

            int flag = Neusoft.FrameWork.Function.NConvert.ToInt32((idNO.Substring(16, 1)));
            sexobj = new Neusoft.FrameWork.Models.NeuObject();
            Neusoft.HISFC.Models.Base.SexEnumService sexlist = new Neusoft.HISFC.Models.Base.SexEnumService();
            if (flag % 2 == 0)
            {
                sexobj.ID = Neusoft.HISFC.Models.Base.EnumSex.F.ToString();
                sexobj.Name = sexlist.GetName(Neusoft.HISFC.Models.Base.EnumSex.F);
            }
            else
            {
                sexobj.ID = Neusoft.HISFC.Models.Base.EnumSex.M.ToString();
                sexobj.Name = sexlist.GetName(Neusoft.HISFC.Models.Base.EnumSex.M);
            }

            return 1;
        }

        #region 控件的配置文件处理

        /// <summary>
        /// 从配置文件读取配置信息
        /// </summary>
        /// <param name="container"></param>
        /// <param name="fileName"></param>
        public static void ReadConfig(Control container, string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(new System.IO.StreamReader(fileName));
                System.Xml.Linq.XElement xelement = doc.Element("ControlConfig");
                readconfig(container, xelement);
            }
            else
            {
                //保存默认配置
                SaveConfig(container, fileName);
            }
        }

        /// <summary>
        /// 将信息保存到配置文件中
        /// </summary>
        /// <param name="container"></param>
        /// <param name="fileName"></param>
        private static void SaveConfig(Control container, string fileName)
        {
            System.Xml.Linq.XDocument doc = null;
            if (!System.IO.File.Exists(fileName))
            {
                System.IO.File.Create(fileName).Close();
                doc = new System.Xml.Linq.XDocument();
            }
            else
            {
                doc = System.Xml.Linq.XDocument.Load(new System.IO.StreamReader(fileName));

            }
            try
            {
                System.Xml.Linq.XElement xelement = new System.Xml.Linq.XElement("ControlConfig");
                saveconfig(container, xelement);
                doc.Add(xelement);
            }
            finally
            {
                doc.Save(fileName);
            }
        }

        /// <summary>
        /// 通过配置文件进行输入的验证
        /// </summary>
        /// <param name="container"></param>
        public static void ValidConfig(Control container)
        {
            //判断必须输入的控件是否都已经输入信息
            foreach (Control c in container.Controls)
            {
                if (c is Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)
                {
                    if (!((Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)c).IsValidInput())
                    {
                        c.Select();
                        throw new Exception(((Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)c).InputMsg);
                    }
                }

                if (c.Controls.Count > 0)
                {
                    ValidConfig(c);
                }
            }
        }

        /// <summary>
        /// 选择控件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void c_Click(object sender, EventArgs e)
        {
            if (sender is Control)
            {
                ((Control)sender).Select();
            }
        }

        /// <summary>
        /// 输入法设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void c_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = CHInput;
        }

        private static void readconfig(Control container, System.Xml.Linq.XElement doc)
        {
            foreach (Control c in container.Controls)
            {
                if (c is Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)
                {
                    ((Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)c).ReadConfig(doc);
                    if (((Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)c).IsDefaultCHInput)
                    {
                        c.Enter += new EventHandler(c_Enter);
                    }
                }

                if (c is Label || c is Panel)
                {
                    c.Click += new EventHandler(c_Click);
                }

                if (c.Controls.Count > 0)
                {
                    readconfig(c, doc);
                }
            }
        }

        private static void saveconfig(Control container, System.Xml.Linq.XElement doc)
        {
            for (int i = container.Controls.Count - 1; i >= 0; i--)
            {
                Control c = container.Controls[i];
                if (c is Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)
                {
                    ((Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)c).SaveConfig(doc);
                    if (((Neusoft.SOC.HISFC.RADT.Interface.Common.IInputControl)c).IsDefaultCHInput)
                    {
                    }
                }

                if (c is Label || c is Panel)
                {
                    c.Click += new EventHandler(c_Click);
                }

                if (c.Controls.Count > 0)
                {
                    saveconfig(c, doc);
                }
            }
        }

        #endregion

        #region 输入法

        private static InputLanguage chInput = null;

        /// <summary>
        /// 默认的中文输入法
        /// </summary>
        public static InputLanguage CHInput
        {
            get
            {
                if (chInput == null)
                {
                    ReadInputLanguage();
                }

                return chInput;
            }
            set
            {
                chInput = value;
                SaveInputLanguage();
            }
        }

        /// <summary>
        /// 读取当前默认输入法
        /// </summary>
        private static void ReadInputLanguage()
        {
            if (!System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                System.IO.File.Create(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            XmlNode node = doc.SelectSingleNode("//IME");
            if (node != null)
            {
                chInput = getInputLanguage(node.Attributes["currentmodel"].Value);
            }
        }

        /// <summary>
        /// 根据输入法名称获取输入法
        /// </summary>
        /// <param name="LanName"></param>
        /// <returns></returns>
        private static InputLanguage getInputLanguage(string LanName)
        {
            foreach (InputLanguage input in InputLanguage.InstalledInputLanguages)
            {
                if (input.LayoutName == LanName)
                {
                    return input;
                }
            }
            return null;
        }

        /// <summary>
        /// 保存当前输入法
        /// </summary>
        private static void SaveInputLanguage()
        {
            if (!System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                System.IO.File.Create(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            if (chInput == null)
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            XmlNode node = doc.SelectSingleNode("//IME");
            if (node == null)
            {
                node = doc.CreateElement("IME");
            }
            if (node.Attributes["currentmodel"] != null)
            {
                node.Attributes["currentmodel"].Value = chInput.LayoutName;
            }
            else
            {
                XmlAttribute attribute = doc.CreateAttribute("currentmodel");
                attribute.Value = chInput.LayoutName;
                node.AppendChild(attribute);
            }

            doc.Save(Neusoft.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
        }

        #endregion

        #endregion

        #region 业务层综合函数

        /// <summary>
        /// 根据sql条件查询旧系统数据
        /// </summary>
        /// <param name="sqlwhere"></param>
        /// <returns></returns>
        public static ArrayList QueryOldSysPatientInfo(string sqlwhere)
        {
            Neusoft.SOC.HISFC.RADT.BizLogic.ComOldPatient comOldPatientMgr = new Neusoft.SOC.HISFC.RADT.BizLogic.ComOldPatient();
            ArrayList alOldPatient = comOldPatientMgr.GetOldSysPatientInfo(sqlwhere);

            if (alOldPatient == null)
            {
                CommonController.CreateInstance().MessageBox("查询旧系统数据失败！" + comOldPatientMgr.Err, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            return alOldPatient;
        }

        /// <summary>
        /// 根据sql条件查询患者信息
        /// </summary>
        /// <param name="sqlwhere"></param>
        /// <returns></returns>
        public static ArrayList QueryPatientInfo(string sqlwhere)
        {
            Neusoft.SOC.HISFC.RADT.BizLogic.Inpatient inpatientMgr = new Neusoft.SOC.HISFC.RADT.BizLogic.Inpatient();

            ArrayList alPatient = inpatientMgr.QueryPatientInfo(sqlwhere);

            if (alPatient == null)
            {
                CommonController.CreateInstance().MessageBox("查询患者信息失败！" + inpatientMgr.Err, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            return alPatient;
        }

        /// <summary>
        /// 自动获取住院号方法
        /// </summary>
        /// <param name="patientNO"></param>
        /// <param name="isRecycle"></param>
        /// <returns></returns>
        public static int GetAutoPatientNO(ref string patientNO, ref bool isRecycle)
        {
            Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();

            try
            {
                return radtIntegrate.GetAutoPatientNO(ref patientNO, ref isRecycle);
            }
            finally
            {
                err = radtIntegrate.Err;
            }
        }

        /// <summary>
        /// 获取临时住院号
        /// </summary>
        /// <param name="tempPatientNO"></param>
        /// <returns></returns>
        public static int GetAutoTempPatientNO(ref string tempPatientNO)
        {
            Neusoft.HISFC.BizLogic.RADT.InPatient inpatientMgr = new Neusoft.HISFC.BizLogic.RADT.InPatient();
            tempPatientNO = inpatientMgr.GetNewTempPatientNo();
            if (string.IsNullOrEmpty(tempPatientNO) || tempPatientNO == "-1")
            {
                err = "获取临时住院号失败";
                return -1;
            }

            tempPatientNO = tempPatientNO.PadLeft(10, '0');

            return 1;
        }

        /// <summary>
        ///  根据住院号获取住院信息
        /// </summary>
        /// <param name="patientNO"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public static int GetInputPatientNO(string patientNO, ref Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();
            try
            {
                return radtIntegrate.GetInputPatientNO(patientNO, ref patientInfo);
            }
            finally
            {
                err = radtIntegrate.Err;
            }

        }

        /// <summary>
        /// 根据病历号查询患者信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public static Neusoft.HISFC.Models.RADT.Patient GetPatient(string cardNO)
        {
            Neusoft.SOC.HISFC.RADT.BizLogic.ComPatient compatientMgr = new Neusoft.SOC.HISFC.RADT.BizLogic.ComPatient();
            try
            {
                return compatientMgr.QueryComPatient(cardNO);
            }
            finally
            {
                err = compatientMgr.Err;
            }
        }

        /// <summary>
        /// 根据卡号/病历号获取病历号
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetCardNO(string text)
        {
            string cardNO = text.Trim();

            Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();
            Neusoft.HISFC.Models.Account.AccountCard accountCard = new Neusoft.HISFC.Models.Account.AccountCard();
            int resultValue = feeIntegrate.ValidMarkNO(cardNO, ref accountCard);
            if (resultValue <= 0)
            {
                cardNO = Neusoft.FrameWork.Public.String.FillString(cardNO);
            }
            else
            {
                cardNO = accountCard.Patient.PID.CardNO;
            }

            return cardNO;
        }

        /// <summary>
        /// 根据住院号自动生成病历号
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public static string GetCardNOByPatientNO(string cardNO, string patientNO)
        {
            if (string.IsNullOrEmpty(patientNO))
            {
                return cardNO;
            }

            if (string.IsNullOrEmpty(cardNO))
            {
                //if (Neusoft.FrameWork.Public.String.IsNumeric(patientNO))
                //{
                //自动生成病历号
                return new Neusoft.SOC.HISFC.RADT.BizLogic.ComPatient().GetAutoCardNO();

                //return "T" + patientNO.PadLeft(10, '0').Substring(1);
                //}
                //else
                //{
                //    return patientNO;
                //}
            }
            else
            {
                return cardNO;
            }
        }

        /// <summary>
        /// 根据科室找对应的病区
        /// </summary>
        /// <param name="deptStatCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public static ArrayList QueryNurseByDept(string deptCode)
        {
            Neusoft.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new Neusoft.HISFC.BizLogic.Manager.DepartmentStatManager();

            ArrayList alNurse = new ArrayList();
            ArrayList al = deptStatMgr.LoadByParent("01", deptCode);
            if (al == null || al.Count == 0)
            {
                al = deptStatMgr.LoadByChildren("01", deptCode);
                if (al != null)
                {
                    foreach (Neusoft.HISFC.Models.Base.DepartmentStat deptStat in al)
                    {
                        alNurse.Add(new Neusoft.FrameWork.Models.NeuObject(deptStat.PardepCode, deptStat.PardepName, ""));
                    }
                }
            }
            else
            {
                foreach (Neusoft.HISFC.Models.Base.DepartmentStat deptStat in al)
                {
                    alNurse.Add(new Neusoft.FrameWork.Models.NeuObject(deptStat.DeptCode, deptStat.DeptName, ""));
                }
            }
            return alNurse;
        }

        /// <summary>
        /// 根据病区找对应的床位
        /// </summary>
        /// <param name="deptStatCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public static ArrayList QueryBedByNurse(string nurseCode)
        {
            Neusoft.HISFC.BizLogic.Manager.Bed managerBed = new Neusoft.HISFC.BizLogic.Manager.Bed();
            return managerBed.GetUnoccupiedBed(nurseCode);
        }

        #endregion

        #region 医保

        /// <summary>
        /// 获取住院登记信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        public static int GetRegInfoInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, ref string errText)
        {
            Neusoft.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new Neusoft.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            medcareInterfaceProxy.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);

            if (medcareInterfaceProxy.Connect() == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                errText = "连接医保出错!" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            //获取医保登记信息
            if (medcareInterfaceProxy.GetRegInfoInpatient(patientInfo) != 1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            //断开连接
            if (medcareInterfaceProxy.Disconnect() != 1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            medcareInterfaceProxy.Commit();
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }


        #endregion
    }
}
