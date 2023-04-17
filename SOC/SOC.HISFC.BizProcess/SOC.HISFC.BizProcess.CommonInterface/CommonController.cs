using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.HISFC.BizProcess.CommonInterface
{
    /// <summary>
    /// 常用类控制类
    /// </summary>
    public abstract class CommonController : Controller
    {
        private static CommonController factory;

        private static object lockHelper = new object();

        public static CommonController Instance
        {
            get
            {
                return CreateInstance();
            }
        }

        public static CommonController CreateInstance()
        {
            if (factory == null)
            {
                lock (lockHelper)
                {
                    //factory = factory ?? FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(CommonController), typeof(CommonController)) as CommonController;

                    if (factory == null)
                    {
                        factory = factory ?? new DefaultCommonController();
                    }
                }
            }
            return factory;
        }

        /// <summary>
        /// 缓存字典信息
        /// </summary>
        private static Dictionary<string, FS.FrameWork.Public.ObjectHelper> dictionary = new Dictionary<string, FS.FrameWork.Public.ObjectHelper>();

        #region 常数

        public virtual ArrayList QueryConstant(string type)
        {
            if (dictionary.ContainsKey(type))
            {
                return dictionary[type].ArrayObject.Clone() as ArrayList;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Constant managerIntegrate = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList al = managerIntegrate.GetList(type);
                if (al != null)
                {
                    FS.FrameWork.Public.ObjectHelper objectHepler = new FS.FrameWork.Public.ObjectHelper();
                    objectHepler.ArrayObject = al.Clone() as ArrayList;
                    dictionary[type] = objectHepler;
                }

                return al;
            }
        }

        //{1A1ECA02-C14B-4c25-9962-7797FDE2F7E2}通过指定sql构造用于下拉框框架的字典
        public virtual ArrayList QueryConstantBySql(string sql,string type)
        {
            if (dictionary.ContainsKey(type))
            {
                return dictionary[type].ArrayObject.Clone() as ArrayList;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Constant managerIntegrate = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList al = managerIntegrate.GetListAsSql(sql, type);
                if (al != null)
                {
                    FS.FrameWork.Public.ObjectHelper objectHepler = new FS.FrameWork.Public.ObjectHelper();
                    objectHepler.ArrayObject = al.Clone() as ArrayList;
                    dictionary[type] = objectHepler;
                }

                return al;
            }
        }

        public virtual ArrayList QueryConstant(FS.HISFC.Models.Base.EnumConstant type)
        {
            return QueryConstant(type.ToString());
        }

        public virtual FS.FrameWork.Models.NeuObject GetConstant(FS.HISFC.Models.Base.EnumConstant type, string code)
        {
            return GetConstant(type.ToString(), code);
        }

        public virtual FS.FrameWork.Models.NeuObject GetConstant(string type, string code)
        {
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(code))
            {
                return null;
            }

            FS.FrameWork.Models.NeuObject constant = null;
            if (!dictionary.ContainsKey(type))
            {
                QueryConstant(type);
            }

            constant = dictionary[type].GetObjectFromID(code);

            if (constant == null)
            {
                FS.HISFC.BizLogic.Manager.Constant managerIntegrate = new FS.HISFC.BizLogic.Manager.Constant();
                constant = managerIntegrate.GetConstant(type, code);
                if (constant == null)
                {
                    return null;
                }
                else if (string.IsNullOrEmpty(constant.ID) == false)
                {
                    dictionary[type].ArrayObject.Add(constant);
                }
            }

            return constant.Clone();

        }

        public virtual string GetConstantName(FS.HISFC.Models.Base.EnumConstant type, string code)
        {
            return GetConstantName(type.ToString(), code);
        }

        public virtual string GetConstantName(string type, string code)
        {
            FS.FrameWork.Models.NeuObject constant = GetConstant(type, code);
            return constant == null ? "" : constant.Name;
        }

        #endregion

        #region 频次

        /// <summary>
        /// 获取所有频次
        /// </summary>
        /// <returns></returns>
        public virtual ArrayList QueryFrequency()
        {
            return Cache.Order.QueryFrequency();
        }

        /// <summary>
        /// 根据编码获取频次
        /// </summary>
        /// <param name="frequencyCode"></param>
        /// <returns></returns>
        public virtual FS.HISFC.Models.Order.Frequency GetFrequency(string frequencyCode)
        {
            return Cache.Order.GetFrequency(frequencyCode);
        }

        /// <summary>
        /// 获取频次名称
        /// </summary>
        /// <param name="frequencyCode"></param>
        /// <returns></returns>
        public virtual string GetFrequencyName(string frequencyCode)
        {
            return Cache.Order.GetFrequencyName(frequencyCode);
        }

        #endregion

        #region 科室

        /// <summary>
        /// 获取所有科室（缓存）
        /// </summary>
        /// <returns></returns>
        public virtual ArrayList QueryDepartment()
        {
            if (dictionary.ContainsKey("Department"))
            {
                return dictionary["Department"].ArrayObject.Clone() as ArrayList;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Department managerIntegrate = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList al = managerIntegrate.GetDeptmentAll();
                if (al != null)
                {
                    FS.FrameWork.Public.ObjectHelper objectHepler = new FS.FrameWork.Public.ObjectHelper();
                    objectHepler.ArrayObject = al.Clone() as ArrayList;
                    dictionary["Department"] = objectHepler;
                }

                return al;
            }
        }

        /// <summary>
        /// 根据类型获取科室信息（缓存）
        /// </summary>
        /// <param name="deptType">科室类型</param>
        /// <returns></returns>
        public virtual ArrayList QueryDepartment(FS.HISFC.Models.Base.EnumDepartmentType deptType)
        {
            if (dictionary.ContainsKey("DepartmentByType"))
            {
                return dictionary["DepartmentByType"].ArrayObject.Clone() as ArrayList;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Department managerIntegrate = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList al = managerIntegrate.GetDeptment(deptType);
                if (al != null)
                {
                    FS.FrameWork.Public.ObjectHelper objectHepler = new FS.FrameWork.Public.ObjectHelper();
                    objectHepler.ArrayObject = al.Clone() as ArrayList;
                    dictionary["DepartmentByType"] = objectHepler;
                }

                return al;
            }
        }

        /// <summary>
        /// 获取住院科室（缓存）
        /// </summary>
        /// <param name="isInHos"></param>
        /// <returns></returns>
        public virtual ArrayList QueryDepartment(bool isInHos)
        {
            if (dictionary.ContainsKey("DepartmentInHos"))
            {
                return dictionary["DepartmentInHos"].ArrayObject.Clone() as ArrayList;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Department managerIntegrate = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList al = managerIntegrate.GetInHosDepartment(isInHos);
                if (al != null)
                {
                    FS.FrameWork.Public.ObjectHelper objectHepler = new FS.FrameWork.Public.ObjectHelper();
                    objectHepler.ArrayObject = al.Clone() as ArrayList;
                    dictionary["DepartmentInHos"] = objectHepler;
                }

                return al;
            }
        }

        /// <summary>
        /// 根据科室编码获取科室（缓存）
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns></returns>
        public virtual FS.HISFC.Models.Base.Department GetDepartment(string deptCode)
        {
            if (string.IsNullOrEmpty(deptCode))
            {
                return null;
            }
            FS.HISFC.Models.Base.Department dept = null;
            if (!dictionary.ContainsKey("Department"))
            {
                QueryDepartment();
            }

            dept = dictionary["Department"].GetObjectFromID(deptCode) as FS.HISFC.Models.Base.Department;

            if (dept == null)
            {
                //从数据库查找
                FS.HISFC.BizLogic.Manager.Department managerIntegrate = new FS.HISFC.BizLogic.Manager.Department();
                dept = managerIntegrate.GetDeptmentById(deptCode);
                if (dept == null)
                {
                    return null;
                }
                else if (!string.IsNullOrEmpty(dept.ID))
                {
                    dictionary["Department"].ArrayObject.Add(dept);
                }

            }


            return dept.Clone();
        }

        /// <summary>
        /// 根据科室编码获取名称（缓存）
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns></returns>
        public virtual string GetDepartmentName(string deptCode)
        {
            FS.HISFC.Models.Base.Department dept = GetDepartment(deptCode);
            return dept == null ? "" : dept.Name;
        }

        #endregion

        #region 人员

        /// <summary>
        /// 获取所有人员（缓存）
        /// </summary>
        /// <returns></returns>
        public virtual ArrayList QueryEmployee()
        {
            if (dictionary.ContainsKey("Employee"))
            {
                return dictionary["Employee"].ArrayObject.Clone() as ArrayList;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Person managerIntegrate = new FS.HISFC.BizLogic.Manager.Person();
                ArrayList al = managerIntegrate.GetEmployeeAll();
                if (al != null)
                {
                    FS.FrameWork.Public.ObjectHelper objectHepler = new FS.FrameWork.Public.ObjectHelper();
                    objectHepler.ArrayObject = al.Clone() as ArrayList;
                    dictionary["Employee"] = objectHepler;
                }

                return al;
            }
        }

        /// <summary>
        /// 根据人员类型获取人员（缓存）
        /// </summary>
        /// <param name="emplType"></param>
        /// <returns></returns>
        public virtual ArrayList QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType emplType)
        {
            if (dictionary.ContainsKey("EmployeeByType"))
            {
                return dictionary["EmployeeByType"].ArrayObject.Clone() as ArrayList;
            }
            else
            {
                FS.HISFC.BizLogic.Manager.Person managerIntegrate = new FS.HISFC.BizLogic.Manager.Person();
                ArrayList al = managerIntegrate.GetEmployee(emplType);
                if (al != null)
                {
                    FS.FrameWork.Public.ObjectHelper objectHepler = new FS.FrameWork.Public.ObjectHelper();
                    objectHepler.ArrayObject = al.Clone() as ArrayList;
                    dictionary["EmployeeByType"] = objectHepler;
                }

                return al;
            }
        }

        /// <summary>
        /// 根据人员编码获取人员信息（缓存）
        /// </summary>
        /// <param name="emplCode"></param>
        /// <returns></returns>
        public virtual FS.HISFC.Models.Base.Employee GetEmployee(string emplCode)
        {
            if (string.IsNullOrEmpty(emplCode))
            {
                return null;
            }
            FS.HISFC.Models.Base.Employee employee = null;
            if (!dictionary.ContainsKey("Employee"))
            {
                QueryEmployee();
            }

            employee = dictionary["Employee"].GetObjectFromID(emplCode) as FS.HISFC.Models.Base.Employee;

            //从数据库查找
            if (employee == null)
            {
                FS.HISFC.BizLogic.Manager.Person managerIntegrate = new FS.HISFC.BizLogic.Manager.Person();
                employee = managerIntegrate.GetPersonByID(emplCode);

                if (employee == null)
                {
                    return null;
                }
                else if (string.IsNullOrEmpty(employee.ID) == false)
                {
                    dictionary["Employee"].ArrayObject.Add(employee);
                }
            }

            return employee.Clone();

        }

        /// <summary>
        /// 根据人员编码获取人员名称（缓存）
        /// </summary>
        /// <param name="emplCode"></param>
        /// <returns></returns>
        public virtual string GetEmployeeName(string emplCode)
        {
            FS.HISFC.Models.Base.Employee emplyee = GetEmployee(emplCode);
            return emplyee == null ? "" : emplyee.Name;
        }

        #endregion

        #region 床位

        public virtual ArrayList QueryBedInfoByDept(FS.FrameWork.Models.NeuObject dept)
        {
            FS.HISFC.BizLogic.Manager.Department managerDepartment = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.BizLogic.Manager.Bed managerBed = new FS.HISFC.BizLogic.Manager.Bed();
            ArrayList alNurseCell = managerDepartment.GetNurseStationFromDept(dept);
            ArrayList alBed = null;
            if (alNurseCell != null)
            {
                alBed = new ArrayList();
                foreach (FS.FrameWork.Models.NeuObject obj in alNurseCell)
                {
                    ArrayList temp = managerBed.GetUnoccupiedBed(obj.ID);
                    if (temp != null && temp.Count > 0)
                    {
                        alBed.AddRange(temp);
                    }
                }
            }

            return alBed;
        }

        #endregion

        #region 午别

        public virtual ArrayList QueryNoon()
        {
            if (dictionary.ContainsKey("Noon"))
            {
                return dictionary["Noon"].ArrayObject.Clone() as ArrayList;
            }
            else
            {
                FS.HISFC.BizLogic.Registration.Noon registerIntegrate = new FS.HISFC.BizLogic.Registration.Noon();
                ArrayList al = registerIntegrate.Query();
                if (al != null)
                {
                    FS.FrameWork.Public.ObjectHelper objectHepler = new FS.FrameWork.Public.ObjectHelper();
                    objectHepler.ArrayObject = al.Clone() as ArrayList;
                    dictionary["Noon"] = objectHepler;
                }

                return al;
            }
        }

        public virtual FS.HISFC.Models.Base.Noon GetNoon(DateTime currentTime)
        {
            if (QueryNoon() == null)
            {
                return null;
            }

            //int[,] zones = new int[,] { { 0, 140000 }, { 140000, 180000 }, { 180000, 235959 } };
            int time = int.Parse(currentTime.ToString("HHmmss"));
            //int begin = 0, end = 0;

            //for (int i = 0; i < 3; i++)
            //{
            //    if (zones[i, 0] <= time && zones[i, 1] > time)
            //    {
            //        begin = zones[i, 0];
            //        end = zones[i, 1];
            //        break;
            //    }
            //}

            foreach (FS.HISFC.Models.Base.Noon obj in QueryNoon())
            {
                if (time>=int.Parse(obj.StartTime.ToString("HHmmss"))  &&
                   time<= int.Parse(obj.EndTime.ToString("HHmmss")))
                {
                    return obj;
                }
            }

            return null;
        }

        public virtual string GetNoonID(DateTime currentTime)
        {
            FS.HISFC.Models.Base.Noon noon = GetNoon(currentTime);
            if (noon == null)
            {
                return "";
            }
            else
            {
                return noon.ID;
            }
        }

        public virtual FS.HISFC.Models.Base.Noon GetNoon(string noonID)
        {
            FS.HISFC.Models.Base.Noon noon = null;
            if (dictionary.ContainsKey("Noon"))
            {
                noon = dictionary["Noon"].GetObjectFromID(noonID) as FS.HISFC.Models.Base.Noon;
            }

            if (noon == null)
            {
                dictionary.Remove("Noon");
                QueryNoon();
                noon = dictionary["Noon"].GetObjectFromID(noonID) as FS.HISFC.Models.Base.Noon;
            }

            if (noon == null)
            {
                return null;
            }

            return noon.Clone();
        }

        public virtual string GetNoonName(string noonID)
        {
            FS.HISFC.Models.Base.Noon noon = GetNoon(noonID);
            return noon == null ? "" : noon.Name;
        }

        #endregion

        #region 系统时间

        public DateTime GetSystemTime()
        {
            FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger();
            return db.GetDateTimeFromSysDateTime();
        }

        #endregion

        #region 出生日期和年龄

        public string GetAge(DateTime birthday)
        {
            FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger();
            return db.GetAge(birthday);
        }

        public string GetAge(DateTime birthday, ref int year, ref int month, ref int day)
        {
            FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger();
            return db.GetAge(birthday, db.GetDateTimeFromSysDateTime(), ref year, ref month, ref day);
        }

        public DateTime GetBirthday(int year,int month,int day)
        {
            FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger();
            return db.GetDateFromAge(db.GetDateTimeFromSysDateTime(), year, month, day);
        }

        #endregion

        #region 提示窗口

        /// <summary>
        /// 提示方法
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgIcon"></param>
        public DialogResult MessageBox(IWin32Window owner, string msg, MessageBoxButtons msgBtn, MessageBoxIcon msgIcon)
        {
            return System.Windows.Forms.MessageBox.Show(owner, msg, "提示>>", msgBtn, msgIcon);
        }

        /// <summary>
        /// 提示方法
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgIcon"></param>
        public DialogResult MessageBox(string msg, MessageBoxButtons msgBtn, MessageBoxIcon msgIcon)
        {
            return System.Windows.Forms.MessageBox.Show(msg, "提示>>", msgBtn, msgIcon);
        }

        /// <summary>
        /// 提示方法
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgIcon"></param>
        public DialogResult MessageBox(IWin32Window owner, string msg, MessageBoxIcon msgIcon)
        {
            return System.Windows.Forms.MessageBox.Show(owner, msg, "提示>>", MessageBoxButtons.OK, msgIcon);
        }

        /// <summary>
        /// 提示方法
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgIcon"></param>
        public DialogResult MessageBox(string msg, MessageBoxIcon msgIcon)
        {
            return System.Windows.Forms.MessageBox.Show(msg, "提示>>", MessageBoxButtons.OK, msgIcon);
        }
        #endregion

        #region  合同单位

        public virtual FS.HISFC.Models.Base.PayKind GetPayKind(string pactCode)
        {
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfo = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            //合同单位不做缓存
            FS.HISFC.Models.Base.PactInfo pact = pactUnitInfo.GetPactUnitInfoByPactCode(pactCode);
            if (pact == null)
            {
                return null;
            }

            return pact.PayKind;
        }

        public virtual FS.HISFC.Models.Base.Pact GetPact(string pactCode)
        {
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfo = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            //合同单位不做缓存
            FS.HISFC.Models.Base.PactInfo pact = pactUnitInfo.GetPactUnitInfoByPactCode(pactCode);
            if (pact == null)
            {
                return null;
            }

            return pact;
        }

        public virtual ArrayList QueryPactInfo()
        {
            //合同单位不做缓存
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfo = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            return pactUnitInfo.QueryPactUnitAll();
        }

        public virtual ArrayList QueryInPatientPactInfo()
        {
            FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfo = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            return pactUnitInfo.QueryPactUnitInPatient();
        }

        #endregion

        #region 挂号级别

        /// <summary>
        /// 获取挂号级别（缓存）
        /// </summary>
        /// <returns></returns>
        public virtual ArrayList QueryRegLevel()
        {
            if (dictionary.ContainsKey("RegLevel"))
            {
                return dictionary["RegLevel"].ArrayObject.Clone() as ArrayList;
            }
            else
            {
                FS.HISFC.BizLogic.Registration.RegLevel regLevelMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
                ArrayList al = regLevelMgr.Query();
                if (al != null)
                {
                    FS.FrameWork.Public.ObjectHelper objectHepler = new FS.FrameWork.Public.ObjectHelper();
                    objectHepler.ArrayObject = al.Clone() as ArrayList;
                    dictionary["RegLevel"] = objectHepler;
                }

                return al;
            }
        }

        /// <summary>
        /// 根据挂号级别编码获取挂号级别
        /// </summary>
        /// <param name="emplCode"></param>
        /// <returns></returns>
        public virtual FS.HISFC.Models.Registration.RegLevel GetRegLevel(string regLevelID)
        {
            if (string.IsNullOrEmpty(regLevelID))
            {
                return null;
            }
            FS.HISFC.Models.Registration.RegLevel regLevel = null;
            if (!dictionary.ContainsKey("RegLevel"))
            {
                QueryRegLevel();
            }

            regLevel = dictionary["RegLevel"].GetObjectFromID(regLevelID) as FS.HISFC.Models.Registration.RegLevel;

            //从数据库查找
            if (regLevel == null)
            {
                FS.HISFC.BizLogic.Registration.RegLevel regLevelMgr = new FS.HISFC.BizLogic.Registration.RegLevel();
                regLevel = regLevelMgr.Query(regLevelID);

                if (regLevel == null)
                {
                    return null;
                }
                else if (string.IsNullOrEmpty(regLevel.ID) == false)
                {
                    dictionary["RegLevel"].ArrayObject.Add(regLevel);
                }
            }

            return regLevel.Clone();

        }

        /// <summary>
        /// 根据挂号级别编码获取名称
        /// </summary>
        /// <param name="regLevelID"></param>
        /// <returns></returns>
        public virtual string GetRegLevelName(string regLevelID)
        {
            FS.HISFC.Models.Registration.RegLevel regLevel = GetRegLevel(regLevelID);
            return regLevel == null ? "" : regLevel.Name;
        }
        #endregion

        #region 项目信息


        /// <summary>
        /// 根据挂号级别编码获取挂号级别
        /// </summary>
        /// <param name="emplCode"></param>
        /// <returns></returns>
        public virtual FS.HISFC.Models.Fee.Item.Undrug GetItem(string itemCode)
        {
            if (string.IsNullOrEmpty(itemCode))
            {
                return null;
            }
            FS.HISFC.Models.Fee.Item.Undrug undrug = null;
            if (!dictionary.ContainsKey("Undrug"))
            {
                //查找
                dictionary["Undrug"] = new FS.FrameWork.Public.ObjectHelper();
            }

            undrug = dictionary["Undrug"].GetObjectFromID(itemCode) as FS.HISFC.Models.Fee.Item.Undrug;

            //从数据库查找
            if (undrug == null)
            {
                FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
                undrug = itemMgr.GetUndrugByCode(itemCode);

                if (undrug == null)
                {
                    return null;
                }
                else if (string.IsNullOrEmpty(undrug.ID) == false)
                {
                    dictionary["Undrug"].ArrayObject.Add(undrug);
                }
            }

            return undrug;

        }

        #endregion

        #region 权限管理

        /// <summary>
        /// 判断是否有二级权限
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <returns>true有，false无</returns>
        public bool JugePrive(string class2Code)
        {
            if (string.IsNullOrEmpty(class2Code))
            {
                return false;
            }
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> listPrive = powerDetailManager.QueryUserPriv(powerDetailManager.Operator.ID, class2Code);
            if (listPrive == null)
            {
                return false;
            }

            return listPrive.Count > 0;
        }

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3Code">三级权限</param>
        /// <returns>true有，false无</returns>
        public  bool JugePrive(string class2Code, string class3Code)
        {
            if (string.IsNullOrEmpty(class2Code) || string.IsNullOrEmpty(class3Code))
            {
                return false;
            }
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> listPrive = powerDetailManager.QueryUserPriv(powerDetailManager.Operator.ID, class2Code, class3Code);
            if (listPrive == null)
            {
                return false;
            }

            return listPrive.Count > 0;
        }

        #endregion
    }

    public class DefaultCommonController : CommonController
    {
    }
}
