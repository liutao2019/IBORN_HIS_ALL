using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Windows.Forms;
using System.Xml;

namespace FS.SOC.Local.RADT.GuangZhou
{
    public class Function
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
        public static int GetBirthDayFromIdNO(string idNO, ref string datestr, ref FS.FrameWork.Models.NeuObject sexobj, ref string err)
        {

            if (FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNO, ref err) < 0)
            {
                return -1;
            }
            if (idNO.Length == 15)
            {
                idNO = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            datestr = idNO.Substring(6, 8);
            string year = datestr.Substring(0, 4);
            string month = datestr.Substring(4, 2);
            string day = datestr.Substring(6, 2);
            datestr = year + "-" + month + "-" + day;

            int flag = FS.FrameWork.Function.NConvert.ToInt32((idNO.Substring(16, 1)));
            sexobj = new FS.FrameWork.Models.NeuObject();
            FS.HISFC.Models.Base.SexEnumService sexlist = new FS.HISFC.Models.Base.SexEnumService();
            if (flag % 2 == 0)
            {
                sexobj.ID = FS.HISFC.Models.Base.EnumSex.F.ToString();
                sexobj.Name = sexlist.GetName(FS.HISFC.Models.Base.EnumSex.F);
            }
            else
            {
                sexobj.ID = FS.HISFC.Models.Base.EnumSex.M.ToString();
                sexobj.Name = sexlist.GetName(FS.HISFC.Models.Base.EnumSex.M);
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
                if (c is FS.SOC.HISFC.RADT.Interface.Common.IInputControl)
                {
                    if (!((FS.SOC.HISFC.RADT.Interface.Common.IInputControl)c).IsValidInput())
                    {
                        c.Select();
                        throw new Exception(((FS.SOC.HISFC.RADT.Interface.Common.IInputControl)c).InputMsg);
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
                if (c is FS.SOC.HISFC.RADT.Interface.Common.IInputControl)
                {
                    ((FS.SOC.HISFC.RADT.Interface.Common.IInputControl)c).ReadConfig(doc);
                    if (((FS.SOC.HISFC.RADT.Interface.Common.IInputControl)c).IsDefaultCHInput)
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
                if (c is FS.SOC.HISFC.RADT.Interface.Common.IInputControl)
                {
                    ((FS.SOC.HISFC.RADT.Interface.Common.IInputControl)c).SaveConfig(doc);
                    if (((FS.SOC.HISFC.RADT.Interface.Common.IInputControl)c).IsDefaultCHInput)
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
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
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
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                System.IO.File.Create(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            if (chInput == null)
                return;

            XmlDocument doc = new XmlDocument();
            doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
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

            doc.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
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
            if (IsContainYKDept())
            {
                sqlwhere += " and card_no like '%V%'";
            }
            else
            {
                sqlwhere += " and card_no not like '%V%'";
            }
            FS.SOC.HISFC.RADT.BizLogic.ComOldPatient comOldPatientMgr = new FS.SOC.HISFC.RADT.BizLogic.ComOldPatient();
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
            if (IsContainYKDept())
            {
                sqlwhere += " and dept_code in (select code from com_dictionary a where a.type='YkDept')";
            }
            else
            {
                sqlwhere += " and dept_code not in (select code from com_dictionary a where a.type='YkDept')";
            }

            FS.SOC.HISFC.RADT.BizLogic.Inpatient inpatientMgr = new FS.SOC.HISFC.RADT.BizLogic.Inpatient();

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
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

            try
            {
                if (IsContainYKDept())
                {
                    return GetAutoPatientNOByYk(ref patientNO, ref isRecycle);
                }
                else
                {
                    return radtIntegrate.GetAutoPatientNO(ref patientNO, ref isRecycle);
                }
            }
            finally
            {
                err = radtIntegrate.Err;
            }
        }

        private static Dictionary<string, string> dictionaryYKDept = new Dictionary<string, string>();
        /// <summary>
        ///  判断是否是宜康科室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool IsContainYKDept()
        {

            string dept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;

            return IsContainYKDept(dept);
        }

        /// <summary>
        ///  判断是否是宜康科室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool IsContainYKDept(string dept)
        {
            if (dictionaryYKDept == null || dictionaryYKDept.Count == 0)
            {
                ArrayList al = CommonController.Instance.QueryConstant("YkDept");
                if (al != null)
                {
                    foreach (FS.FrameWork.Models.NeuObject obj in al)
                    {
                        dictionaryYKDept[obj.ID] = obj.Name;
                    }
                }
            }

            return dictionaryYKDept.ContainsKey(dept);
        }

        /// <summary>
        /// 自动获取住院号方法（用于宜康）
        /// </summary>
        /// <param name="patientNO"></param>
        /// <param name="isRecycle"></param>
        /// <returns></returns>
        public static int GetAutoPatientNOByYk(ref string patientNO, ref bool isRecycle)
        {
            //从号码表中获取住院号
            string usedPatientNO = string.Empty;
            //isRecycle = GetNoUsedPatientNO(ref patientNO, ref usedPatientNO);
            //if (!isRecycle)
            {
                //如果号码表中没有数据，则获取新的下一个号码。
                //获取新的住院号和住院流水号
                try
                {
                    string MaxPatientNo = string.Empty;
                    MaxPatientNo = GetMaxPatientNO("0008");
                    MaxPatientNo = FS.FrameWork.Public.String.AddNumber(MaxPatientNo, 1);
                    MaxPatientNo = MaxPatientNo.TrimStart('0').PadLeft(10, '0');
                    patientNO = MaxPatientNo;
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 取出最大的住院号---wangrc
        /// </summary>
        /// <param name="parm"></param>
        /// <returns>string -最住院号</returns>
        private static string GetMaxPatientNO(string parm)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string strSql = @"
                select nvl(max(t.patient_no),'00000000V0') from fin_ipr_inmaininfo t
				where t.patient_no not like '{0}%'
				and t.patient_no like '%V%'
				and t.patient_no not like '%B%'
				and t.patient_no not like '%L%'
				and t.patient_no not like '%C%'
                and t.in_state <> 'N' 
                and t.dept_code in (select code from com_dictionary a where a.type='YkDept')";

            strSql = String.Format(strSql, parm);
            return dbMgr.ExecSqlReturnOne(strSql, string.Empty);
        }

        /// <summary>
        /// 获取临时住院号
        /// </summary>
        /// <param name="tempPatientNO"></param>
        /// <returns></returns>
        public static int GetAutoTempPatientNO(ref string tempPatientNO)
        {
            FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
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
        /// 获取生殖住院号
        /// </summary>
        /// <param name="FertilityPatientNO"></param>
        /// <returns></returns>
        public static int GetNewFertilityPatientNo(ref string fertilityPatientNO)
        {
            FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            fertilityPatientNO = inpatientMgr.GetNewFertilityPatientNo();
            if (string.IsNullOrEmpty(fertilityPatientNO) || fertilityPatientNO == "-1")
            {
                err = "获取生殖住院号失败";
                return -1;
            }

            fertilityPatientNO = fertilityPatientNO.PadLeft(10, '0');

            return 1;
        }

        /// <summary>
        ///  根据住院号获取住院信息
        /// </summary>
        /// <param name="patientNO"></param>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public static int GetInputPatientNO(string patientNO, ref FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
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
        public static FS.HISFC.Models.RADT.Patient GetPatient(string cardNO)
        {
            FS.SOC.HISFC.RADT.BizLogic.ComPatient compatientMgr = new FS.SOC.HISFC.RADT.BizLogic.ComPatient();
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

            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            int resultValue = feeIntegrate.ValidMarkNO(cardNO, ref accountCard);
            if (resultValue <= 0)
            {
                cardNO = FS.FrameWork.Public.String.FillString(cardNO);
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

            if (string.IsNullOrEmpty(cardNO) || FS.FrameWork.Public.String.IsNumeric(cardNO) == false)
            {
                //if (FS.FrameWork.Public.String.IsNumeric(patientNO))
                //{
                //自动生成病历号
                return new FS.SOC.HISFC.RADT.BizLogic.ComPatient().GetAutoCardNO();

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
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

            ArrayList alNurse = new ArrayList();
            ArrayList al = deptStatMgr.LoadByParent("01", deptCode);
            if (al == null || al.Count == 0)
            {
                al = deptStatMgr.LoadByChildren("01", deptCode);
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in al)
                    {
                        alNurse.Add(new FS.FrameWork.Models.NeuObject(deptStat.PardepCode, deptStat.PardepName, ""));
                    }
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in al)
                {
                    alNurse.Add(new FS.FrameWork.Models.NeuObject(deptStat.DeptCode, deptStat.DeptName, ""));
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
            FS.HISFC.BizLogic.Manager.Bed managerBed = new FS.HISFC.BizLogic.Manager.Bed();
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
        public static int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, ref string errText)
        {
            FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);

            if (medcareInterfaceProxy.Connect() == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                errText = "连接医保出错!" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            //获取医保登记信息
            if (medcareInterfaceProxy.GetRegInfoInpatient(patientInfo) != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            //断开连接
            if (medcareInterfaceProxy.Disconnect() != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                errText = medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            medcareInterfaceProxy.Commit();
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }


        #endregion

        #region 根据住院号获取最新的住院次数

        public static int GetMaxIntimes(string PatientNo)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

            string strSql = @"
               
                    select nvl(max(in_times),0)+1  from (
                    select i.in_times as  in_times from fin_ipr_inmaininfo i 
                    where i.patient_no ='{0}'
                    and i.in_state<>'N'
                    union all
                    select od.inhos_times  as in_times from com_patientinfo_old od where lpad(od.card_no ,10,'0')='{0}'
                    and od.IS_VALID='1'
                    )";

            strSql = String.Format(strSql, PatientNo);
            string maxstr = dbMgr.ExecSqlReturnOne(strSql, string.Empty);
            if (string.IsNullOrEmpty(maxstr))
            {
                return -1;
            }
            return int.Parse(maxstr);
        }

        /// <summary>
        /// 锁住院号
        /// </summary>
        /// <param name="PatientNO"></param>
        /// <returns></returns>
        public static int LockPatientNO(string PatientNO, ref string errorInfo)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

            string strSql = @"insert into com_patientno_list
                                            select '0000000000','0000000000','1',sysdate,'009999',sysdate,'009999' from dual a 
                                            where not exists (select 1 from com_patientno_list a where a.patient_no ='0000000000' )";

            if (dbMgr.ExecNoQuery(strSql) < 0)
            {
                errorInfo = "锁住院号失败，" + dbMgr.Err;
                return -1;
            }
            else
            {
                strSql = @"update com_patientno_list set use_date=sysdate where patient_no='0000000000'";
                if (dbMgr.ExecNoQuery(strSql) <= 0)
                {
                    errorInfo = "锁住院号失败，" + dbMgr.Err;
                    return -1;
                }
            }

            return 1;
        }
        #endregion

        #region 读诊疗卡
        /// <summary>
        /// 读卡接口
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperCard(ref string cardNO, ref string errInfo)
        {
            if (FS.SOC.Local.RADT.GuangZhou.ZDLY.Interface.InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = FS.SOC.Local.RADT.GuangZhou.ZDLY.Interface.InterfaceManager.GetIOperCard().ReadCardNO(ref cardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }
        #endregion
    }
}
