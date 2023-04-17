using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace FS.SOC.HISFC.BizProcess.Cache
{
    /// <summary>
    /// [功能描述: 缓存常数、科室、人员等]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
    public class Common
    {
        #region 科室缓存
        /// <summary>
        /// 暂存科室信息
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper deptHelper;

        /// <summary>
        ///  暂存有效科室信息
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper deptValidHelper;

        private static Hashtable hsDept;

        /// <summary>
        /// 根据科室编码获取科室名称
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <returns>科室名称</returns>
        public static string GetDeptName(string deptNO)
        {
            FS.HISFC.Models.Base.Department dept = GetDept(deptNO);
            if (dept == null)
            {
                return "";
            }
            return dept.Name;            
        }

        /// <summary>
        /// 根据科室编码获取科室信息
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <returns>科室名称</returns>
        public static FS.HISFC.Models.Base.Department GetDeptInfo(string deptNO)
        {
            return GetDept(deptNO);
        }

        /// <summary>
        /// 获取所有科室
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetDept()
        {
            InitDept();
            return deptHelper.ArrayObject;
        }

        /// <summary>
        /// 获取有效科室
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetValidDept()
        {
            InitValidDept();
            return deptValidHelper.ArrayObject;
        }

        /// <summary>
        /// 初始化有效科室
        /// </summary>
        public static void InitValidDept()
        {
            if (deptValidHelper == null||deptValidHelper.ArrayObject.Count == 0)
            {
                deptValidHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
                deptValidHelper.ArrayObject = deptMgr.GetDeptmentAll();
            }
        }

        /// <summary>
        /// 初始化科室列表
        /// </summary>
        public static void InitDept()
        {
            if (deptHelper == null)
            {
                deptHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
                deptHelper.ArrayObject = deptMgr.GetDeptAllUserStopDisuse();
            }
        }

        /// <summary>
        /// 根据科室编码获取科室名称
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <returns>科室名称</returns>
        public static FS.HISFC.Models.Base.Department GetDept(string deptNO)
        {
            if (string.IsNullOrEmpty(deptNO))
            {
                return null;
            }

            InitDept();

            if (deptHelper == null || deptHelper.ArrayObject == null)
            {
                return null;
            }
            if (hsDept == null)
            {
                hsDept = new Hashtable();
            }

            FS.HISFC.Models.Base.Department dept = null;
            if (hsDept.Contains(deptNO))
            {
                dept = (FS.HISFC.Models.Base.Department)hsDept[deptNO];
                if (dept != null)
                {
                    return dept.Clone();
                }
                return dept;
            }
            FS.FrameWork.Models.NeuObject obj = deptHelper.GetObjectFromID(deptNO);
            if (obj is FS.HISFC.Models.Base.Department)
            {
                if (!hsDept.Contains(deptNO))
                {
                    hsDept.Add(deptNO, obj);
                }
                dept = (FS.HISFC.Models.Base.Department)obj;
                if (dept != null)
                {
                    return dept.Clone();
                }
                return dept;
            }
            return null;
        }

        #endregion

        #region 人员缓存
        /// <summary>
        /// 暂存员工信息
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper employeeHelper;

        public static FS.FrameWork.Public.ObjectHelper EmployeeHelper
        {
            get
            {
                if (employeeHelper == null)
                {
                    employeeHelper = new FS.FrameWork.Public.ObjectHelper();
                    FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
                    employeeHelper.ArrayObject = personMgr.GetEmployeeAll();
                }
                return Common.employeeHelper;
            }
        }

        /// <summary>
        /// 根据工号获取姓名
        /// </summary>
        /// <param name="employeeNO">工号</param>
        /// <returns>姓名</returns>
        public static string GetEmployeeName(string employeeNO)
        {
            if (string.IsNullOrEmpty(employeeNO))
            {
                return "";
            }
            if (EmployeeHelper == null || EmployeeHelper.ArrayObject == null)
            {
                return "获取员工信息发生错误";
            }
            return EmployeeHelper.GetName(employeeNO);
        }

        /// <summary>
        /// 根据工号获取人员信息
        /// </summary>
        /// <param name="employeeNO">工号</param>
        /// <returns>姓名</returns>
        public static FS.HISFC.Models.Base.Employee GetEmployeeInfo(string employeeNO)
        {
            if (string.IsNullOrEmpty(employeeNO))
            {
                return null;
            }
            if (EmployeeHelper == null || EmployeeHelper.ArrayObject == null)
            {
                return null;
            }
            return EmployeeHelper.GetObjectFromID(employeeNO) as FS.HISFC.Models.Base.Employee;
        }

        /// <summary>
        /// 查询所有人员信息
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetEmployee()
        {
            return EmployeeHelper.ArrayObject;
        }

        #endregion

        #region 药品性质
        /// <summary>
        /// 暂存药品性质
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper drugQualityHelper;

        /// <summary>
        /// 获取药品性质系统类别
        /// </summary>
        /// <param name="drugQuality">药品性质编码</param>
        /// <returns>null 没有找到对应的常数维护</returns>
        public static string GetDrugQualitySystem(string drugQualityNO)
        {
            if (string.IsNullOrEmpty(drugQualityNO))
            {
                return "";
            }

            if (drugQualityHelper == null)
            {
                drugQualityHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                drugQualityHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            }
            if (drugQualityHelper == null || drugQualityHelper.ArrayObject == null)
            {
                return "";
            }
            FS.FrameWork.Models.NeuObject neuObject = drugQualityHelper.GetObjectFromID(drugQualityNO);
            if (neuObject == null)
            {
                return "";
            }
            FS.HISFC.Models.Base.Const drugQuality = (FS.HISFC.Models.Base.Const)neuObject;
            if (drugQuality == null)
            {
                return "";
            }

            return drugQuality.UserCode;
        }

        /// <summary>
        /// 获取药品性质名称
        /// </summary>
        /// <param name="drugQuality">药品性质编码</param>
        /// <returns>null 没有找到对应的常数维护</returns>
        public static string GetDrugQualityName(string drugQualityNO)
        {
            if (string.IsNullOrEmpty(drugQualityNO))
            {
                return "";
            }

            if (drugQualityHelper == null)
            {
                drugQualityHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                drugQualityHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            }
            if (drugQualityHelper == null || drugQualityHelper.ArrayObject == null)
            {
                return "";
            }
            FS.FrameWork.Models.NeuObject neuObject = drugQualityHelper.GetObjectFromID(drugQualityNO);
            if (neuObject == null)
            {
                return "";
            }
            FS.HISFC.Models.Base.Const drugQuality = (FS.HISFC.Models.Base.Const)neuObject;
            if (drugQuality == null)
            {
                return "";
            }

            return drugQuality.Name;
        }

        #endregion

        #region 药品剂型
        /// <summary>
        /// 暂存药品性质
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper drugDoseModualHelper;

        /// <summary>
        /// 获取药品剂型名称
        /// </summary>
        /// <param name="drugDoseModualNO">药品剂型编码</param>
        /// <returns>null 没有找到对应的常数维护</returns>
        public static string GetDrugDoseModualName(string drugDoseModualNO)
        {
            return GetDrugDoseModual(drugDoseModualNO).Name;
        }

        /// <summary>
        /// 获取药品剂型
        /// </summary>
        /// <param name="drugDoseModualNO">药品剂型编码</param>
        /// <returns>null 没有找到对应的常数维护</returns>
        public static FS.HISFC.Models.Base.Const GetDrugDoseModual(string drugDoseModualNO)
        {
            if (string.IsNullOrEmpty(drugDoseModualNO))
            {
                return new FS.HISFC.Models.Base.Const();
            }

            if (drugDoseModualHelper == null)
            {
                drugDoseModualHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                drugDoseModualHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM);
            }
            if (drugDoseModualHelper == null || drugDoseModualHelper.ArrayObject == null)
            {
                return new FS.HISFC.Models.Base.Const();
                
            }
            FS.FrameWork.Models.NeuObject neuObject = drugDoseModualHelper.GetObjectFromID(drugDoseModualNO);
            if (neuObject == null)
            {
                return new FS.HISFC.Models.Base.Const();
               
            }
            FS.HISFC.Models.Base.Const drugDoseModual = (FS.HISFC.Models.Base.Const)neuObject;
            if (drugDoseModual == null)
            {
                return new FS.HISFC.Models.Base.Const();
                
            }

            return drugDoseModual;
        }

      

      
        #endregion

        #region 药品类别
        /// <summary>
        /// 暂存药品类别
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper drugTypeHelper;
        /// <summary>
        /// 获取药品类别名称
        /// </summary>
        /// <param name="drugQuality">药品类别码</param>
        /// <returns>null 没有找到对应的常数维护</returns>
        public static string GetDrugTypeName(string drugTypeNO)
        {
            if (string.IsNullOrEmpty(drugTypeNO))
            {
                return "";
            }

            if (drugTypeHelper == null)
            {
                drugTypeHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                drugTypeHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            }
            if (drugTypeHelper == null || drugTypeHelper.ArrayObject == null)
            {
                return "";
            }
            FS.FrameWork.Models.NeuObject neuObject = drugTypeHelper.GetObjectFromID(drugTypeNO);
            if (neuObject == null)
            {
                return "";
            }
            FS.HISFC.Models.Base.Const drugType = (FS.HISFC.Models.Base.Const)neuObject;
            if (drugType == null)
            {
                return "";
            }

            return drugType.Name;
        }

        public static void InitDrugType()
        {
            if (drugTypeHelper == null)
            {
                drugTypeHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                drugTypeHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            }
        }
        #endregion

        #region 用法缓存

        /// <summary>
        /// 缓存用法
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper usageHelper;

        /// <summary>
        /// 用法
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper UsageHelper
        {
            get
            {
                if (usageHelper == null)
                {
                    usageHelper = new FS.FrameWork.Public.ObjectHelper();
                    FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                    usageHelper.ArrayObject = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.USAGE);
                }

                return usageHelper;
            }
        }

        /// <summary>
        /// 缓存院内注射用法
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper innerInjectUsageHelper;

        /// <summary>
        /// 根据用法编码获取用法中文名称，即备注说明
        /// </summary>
        /// <param name="usageNO">用法编码</param>
        /// <returns>用法名称</returns>
        public static string GetUsageName(string usageNO)
        {
            if (string.IsNullOrEmpty(usageNO))
            {
                return "";
            }
            if (UsageHelper == null || UsageHelper.ArrayObject == null)
            {
                return "获取用法信息发生错误";
            }
            FS.FrameWork.Models.NeuObject neuObject = UsageHelper.GetObjectFromID(usageNO);
            if (neuObject == null)
            {
                return "没有找到该用法";
            }
            FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
            if (usage == null)
            {
                return "";
            }
            if (usage.Memo.ToLower() == "true" ||　string.IsNullOrEmpty(usage.Memo))
            {
                return usage.Name;
            }
            else
            {
                return usage.Memo;
            }
        }

        /// <summary>
        /// 根据用法编码获取用法系统类别
        /// </summary>
        /// <param name="usageNO">用法编码</param>
        /// <returns>用法系统类别</returns>
        public static string GetUsageSystemType(string usageNO)
        {
            if (string.IsNullOrEmpty(usageNO))
            {
                return "";
            }
            if (UsageHelper == null || UsageHelper.ArrayObject == null)
            {
                return "";
            }
            FS.FrameWork.Models.NeuObject neuObject = UsageHelper.GetObjectFromID(usageNO);
            if (neuObject == null)
            {
                return "";
            }
            FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
            if (usage == null)
            {
                return "";
            }

            return usage.UserCode;
        }

        /// <summary>
        /// 获取用法常数实体
        /// </summary>
        /// <param name="usageNO">用法常数编码</param>
        /// <returns>null 没有找到对应的常数维护</returns>
        public static FS.HISFC.Models.Base.Const GetUsage(string usageNO)
        {
            if (string.IsNullOrEmpty(usageNO))
            {
                return null;
            }
            if (UsageHelper == null || UsageHelper.ArrayObject == null)
            {
                return null;
            }
            FS.FrameWork.Models.NeuObject neuObject = UsageHelper.GetObjectFromID(usageNO);
            if (neuObject == null)
            {
                return null;
            }
            FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;

            return usage.Clone();
        }
        #endregion

        #region 注射类用法

        /// <summary>
        /// 判断用法是否为注射类用法
        /// </summary>
        /// <param name="usageNO">用法代码</param>
        /// <returns></returns>
        public static bool IsInjectUsage(string usageNO)
        {
            bool isInjectUsage = false;

            if (string.IsNullOrEmpty(usageNO))
            {
                return isInjectUsage;
            }
            if (UsageHelper == null || UsageHelper.ArrayObject == null)
            {
                return isInjectUsage;
            }
            FS.FrameWork.Models.NeuObject neuObject = UsageHelper.GetObjectFromID(usageNO);
            if (neuObject == null)
            {
                return isInjectUsage;
            }
            FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
            if (usage == null)
            {
                return isInjectUsage;
            }
            switch (usage.UserCode)
            {
                case "IAST"://皮试
                    isInjectUsage = true;
                    break;
                case "IH"://皮下注射
                    isInjectUsage = true;
                    break;
                case "IM"://肌注
                    isInjectUsage = true;
                    break;
                case "IMO"://肌注其他
                    isInjectUsage = true;
                    break;
                case "IV"://静注
                    isInjectUsage = true;
                    break;
                case "IVO"://静注其他
                    isInjectUsage = true;
                    break;
                case "IVD"://静滴
                    isInjectUsage = true;
                    break;
                case "IVDO"://静滴其他
                    isInjectUsage = true;
                    break;
                case "IZ"://肿瘤注射
                    isInjectUsage = true;
                    break;
                case "IO"://其它注射
                    isInjectUsage = true;
                    break;
                default:
                    isInjectUsage = false;
                    break;
            }

            return isInjectUsage;
        }


        /// <summary>
        /// 判断用法是否为院注类用法（一般用于门诊）
        /// </summary>
        /// <param name="usageNO">用法代码</param>
        /// <returns></returns>
        public static bool IsInnerInjectUsage(string usageNO)
        {
            bool isInnerInjectUsage = false;

            if (string.IsNullOrEmpty(usageNO))
            {
                return isInnerInjectUsage;
            }

            if (innerInjectUsageHelper == null)
            {
                innerInjectUsageHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                innerInjectUsageHelper.ArrayObject = constantMgr.GetAllList("InjectUsage");
            }
            if (innerInjectUsageHelper == null || innerInjectUsageHelper.ArrayObject == null)
            {
                return isInnerInjectUsage;
            }
            FS.FrameWork.Models.NeuObject neuObject = innerInjectUsageHelper.GetObjectFromID(usageNO);
            if (neuObject == null)
            {
                return isInnerInjectUsage;
            }
            FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
            if (usage == null)
            {
                return isInnerInjectUsage;
            }

            return usage.IsValid;

        }

        #endregion

        #region 口服用法

        /// <summary>
        /// 判断用法是否为口服用法
        /// </summary>
        /// <param name="usageNO">用法代码</param>
        /// <returns></returns>
        public static bool IsPOUsage(string usageNO)
        {
            bool isPOUsage = false;

            if (string.IsNullOrEmpty(usageNO))
            {
                return isPOUsage;
            }
            if (UsageHelper == null || UsageHelper.ArrayObject == null)
            {
                return isPOUsage;
            }
            FS.FrameWork.Models.NeuObject neuObject = UsageHelper.GetObjectFromID(usageNO);
            if (neuObject == null)
            {
                return isPOUsage;
            }
            FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
            if (usage == null)
            {
                return isPOUsage;
            }
            //用法常数的自定义码作为系统类别
            switch (usage.UserCode)
            {
                case "PO"://口服
                    isPOUsage = true;
                    break;
                case "POF"://口服磨粉
                    isPOUsage = true;
                    break;
                case "PON"://内服(含、冲、嚼、漱等)
                    isPOUsage = true;
                    break;
                default:
                    isPOUsage = false;
                    break;
            }

            return isPOUsage;
        }



        public static FS.FrameWork.Public.ObjectHelper herbalUsageHelper;

        /// <summary>
        /// 获取草药用法
        /// </summary>
        /// <returns></returns>
        public static FS.FrameWork.Public.ObjectHelper GetHerbalUsage()
        {
            bool isHerbalUsage = false;

            if (UsageHelper == null || UsageHelper.ArrayObject == null)
            {
                return null;
            }

            foreach (FS.FrameWork.Models.NeuObject neuObject in UsageHelper.ArrayObject)
            {
                FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
                if (usage == null)
                {
                    return null;
                }
                //判断以H开头的都为草药用法
                if (usage.UserCode.StartsWith("H"))
                {
                    herbalUsageHelper.ArrayObject.Add(usage);
                }
            }
            return herbalUsageHelper;
        }
        #endregion

        #region 注射类用法（用于判断是否属于针剂摆药单）
        /// <summary>
        /// 判断是否注射类用法 {E81700F1-6DAB-4664-AFC5-9175D601BE02}
        /// </summary>
        /// <param name="usageNO"></param>
        /// <returns></returns>
        public static bool IsInjectionUsage(string usageNO)
        {
            bool isInjectionUsage = false;

            if (string.IsNullOrEmpty(usageNO))
            {
                return isInjectionUsage;
            }
            if (UsageHelper == null || UsageHelper.ArrayObject == null)
            {
                return isInjectionUsage;
            }
            FS.FrameWork.Models.NeuObject neuObject = UsageHelper.GetObjectFromID(usageNO);
            if (neuObject == null)
            {
                return isInjectionUsage;
            }
            FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
            if (usage == null)
            {
                return isInjectionUsage;
            }
            //用法常数的自定义码作为系统类别
            switch (usage.UserCode)
            {
                case "IM"://肌注
                    isInjectionUsage = true;
                    break;
                case "IMO"://肌注其他
                    isInjectionUsage = true;
                    break;
                case "IVD"://静注
                    isInjectionUsage = true;
                    break;
                case "IVDO"://静注其他
                    isInjectionUsage = true;
                    break;
                case "IV"://静滴
                    isInjectionUsage = true;
                    break;
                case "IVO"://静滴其他
                    isInjectionUsage = true;
                    break;
                case "IH"://皮下注射
                    isInjectionUsage = true;
                    break;
                case "IAST"://皮试
                    isInjectionUsage = true;
                    break;
                case "IO"://其他注射
                    isInjectionUsage = true;
                    break;
                case "IX"://穴位注射
                    isInjectionUsage = true;
                    break;
                case "IZ"://肿瘤注射
                    isInjectionUsage = true;
                    break;
                default:
                    isInjectionUsage = false;
                    break;
            }

            return isInjectionUsage;
        }
        #endregion

        #region 外用用法（用于判断是否属于外用药摆药单） 
        /// <summary>
        /// 判断是否外用用法 {E81700F1-6DAB-4664-AFC5-9175D601BE02}
        /// </summary>
        /// <param name="usageNO"></param>
        /// <returns></returns>
        public static bool IsExternalUsage(string usageNO)
        {
            bool isExternalUsage = false;

            if (string.IsNullOrEmpty(usageNO))
            {
                return isExternalUsage;
            }
            if (UsageHelper == null || UsageHelper.ArrayObject == null)
            {
                return isExternalUsage;
            }
            FS.FrameWork.Models.NeuObject neuObject = UsageHelper.GetObjectFromID(usageNO);
            if (neuObject == null)
            {
                return isExternalUsage;
            }
            FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
            if (usage == null)
            {
                return isExternalUsage;
            }
            //用法常数的自定义码作为系统类别
            switch (usage.UserCode)
            {
                case "WY"://外用
                    isExternalUsage = true;
                    break;
                default:
                    isExternalUsage = false;
                    break;
            }

            return isExternalUsage;
        }
        #endregion

        #region 重点精二摆药单药品

        /// <summary>
        /// 暂存重点精二摆药单药品 {E81700F1-6DAB-4664-AFC5-9175D601BE02}
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper stressP2DrguHelper;

        public static string GetP2DrugUserCode(string drugCode)
        {
            if (string.IsNullOrEmpty(drugCode))
            {
                return "";
            }

            if (stressP2DrguHelper == null)
            {
                stressP2DrguHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                stressP2DrguHelper.ArrayObject = constantMgr.GetAllList("STRESSP2SUBSTANCE");
            }
            if (stressP2DrguHelper == null || stressP2DrguHelper.ArrayObject == null)
            {
                return "";
            }
            FS.FrameWork.Models.NeuObject neuObject = stressP2DrguHelper.GetObjectFromID(drugCode);
            if (neuObject == null)
            {
                return "";
            }
            FS.HISFC.Models.Base.Const drug = (FS.HISFC.Models.Base.Const)neuObject;
            if (drug == null)
            {
                return "";
            }

            return drug.UserCode;
        }
        #endregion

        #region 全身麻醉剂摆药单药品

        /// <summary>
        /// 暂存全身麻醉剂摆药单药品 {E81700F1-6DAB-4664-AFC5-9175D601BE02}
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper qsAnesthesiaDrguHelper;

        public static string GetQsAnesthesiaDrugUserCode(string drugCode)
        {
            if (string.IsNullOrEmpty(drugCode))
            {
                return "";
            }

            if (qsAnesthesiaDrguHelper == null)
            {
                qsAnesthesiaDrguHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
                qsAnesthesiaDrguHelper.ArrayObject = constantMgr.GetAllList("AllANESTHESIA");
            }
            if (qsAnesthesiaDrguHelper == null || qsAnesthesiaDrguHelper.ArrayObject == null)
            {
                return "";
            }
            FS.FrameWork.Models.NeuObject neuObject = qsAnesthesiaDrguHelper.GetObjectFromID(drugCode);
            if (neuObject == null)
            {
                return "";
            }
            FS.HISFC.Models.Base.Const drug = (FS.HISFC.Models.Base.Const)neuObject;
            if (drug == null)
            {
                return "";
            }

            return drug.UserCode;
        }
        #endregion

        #region 治疗用法
        /// <summary>
        /// 治疗用法
        /// </summary>
        /// <param name="usageNO"></param>
        /// <returns></returns>
        public static bool IsCure(string usageNO)
        {
            bool isCure = false;
            if (string.IsNullOrEmpty(usageNO))
            {
                return isCure;
            }
            if (UsageHelper == null || UsageHelper.ArrayObject == null)
            {
                return isCure;
            }
            FS.FrameWork.Models.NeuObject neuObject = UsageHelper.GetObjectFromID(usageNO);
            if (neuObject == null)
            {
                return isCure;
            }
            FS.HISFC.Models.Base.Const usage = (FS.HISFC.Models.Base.Const)neuObject;
            if (usage == null)
            {
                return isCure;
            }           
            //用法常数的自定义码作为系统类别
            switch (usage.UserCode)
            {
                case "UZL"://治疗
                    isCure = true;
                    break;
                default:
                    isCure = false;
                    break;
            }             
            return isCure;
        }        
        #endregion

        #region 午别

        /// <summary>
        /// 午别帮助类
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper noonHelper = null;

         /// <summary>
        /// 根据午别编码获取午别信息
        /// </summary>
        /// <param name="noonCode">午别编码</param>
        /// <returns>午别</returns>
        public static FS.HISFC.Models.Base.Noon GetNoonInfo(string noonCode)
        {
            if (noonHelper == null)
            {
                FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

                ArrayList alNoon = noonMgr.Query();
                noonHelper = new FS.FrameWork.Public.ObjectHelper(alNoon);
            }
            return noonHelper.GetObjectFromID(noonCode) as FS.HISFC.Models.Base.Noon;
        }

        /// <summary>
        /// 获取所有午别
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetNoon()
        {
            if (noonHelper == null)
            {
                FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

                ArrayList alNoon = noonMgr.Query();
                noonHelper = new FS.FrameWork.Public.ObjectHelper(alNoon);
            }
            return noonHelper.ArrayObject;
        }

        /// <summary>
        /// 根据时间获取对应的午别
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Base.Noon GetNoonByTime(DateTime dt)
        {
            if (noonHelper == null)
            {
                FS.HISFC.BizLogic.Registration.Noon noonMgr = new FS.HISFC.BizLogic.Registration.Noon();

                ArrayList alNoon = noonMgr.Query();
                noonHelper = new FS.FrameWork.Public.ObjectHelper(alNoon);
            }

            if (noonHelper.ArrayObject != null)
            {
                foreach (FS.HISFC.Models.Base.Noon noon in noonHelper.ArrayObject)
                {
                    if (dt.TimeOfDay >= noon.StartTime.TimeOfDay
                        && dt.TimeOfDay <= noon.EndTime.TimeOfDay)
                    {
                        return noon;
                    }
                }
            }
            return null;
        }

        #endregion

        #region 床位信息

        /// <summary>
        /// 床位列表
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper bedHelper = null;

        /// <summary>
        /// 获取所有床位信息
        /// </summary>
        /// <returns></returns>
        public static ArrayList GetBedList()
        {
            InitBed();
            return bedHelper.ArrayObject;
        }

        /// <summary>
        /// 初始化床位列表
        /// </summary>
        public static void InitBed()
        {
            if (bedHelper == null)
            {
                bedHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Manager.Bed bedMgr = new FS.HISFC.BizLogic.Manager.Bed();
                bedHelper.ArrayObject = bedMgr.GetBedList("ALL");
            }
        }

        /// <summary>
        /// 根据床位编码获取床位信息
        /// </summary>
        /// <param name="deptNO">科室编码</param>
        /// <returns>科室名称</returns>
        public static FS.HISFC.Models.Base.Bed GetBedInfo(string bedNo)
        {
            if (string.IsNullOrEmpty(bedNo))
            {
                return null;
            }

            if (bedHelper == null)
            {
                InitBed();
            }
            if (bedHelper == null || bedHelper.ArrayObject == null)
            {
                return null;
            }

            FS.FrameWork.Models.NeuObject obj = bedHelper.GetObjectFromID(bedNo);
            if (obj is FS.HISFC.Models.Base.Bed)
            {
                return (FS.HISFC.Models.Base.Bed)obj;
            }
            return null;
        }

        #endregion

        private static Dictionary<string, string> dictionaryYKDept = new Dictionary<string, string>();

        /// <summary>
        ///  判断是否是宜康科室
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool IsContainYKDept(string dept)
        {
            if (dictionaryYKDept == null || dictionaryYKDept.Count == 0)
            {
                FS.HISFC.BizLogic.Manager.Constant managerIntegrate = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList al = managerIntegrate.GetList("YkDept");
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

        #region 执行科室
        /// <summary>
        /// 获取执行科室信息
        /// </summary>
        /// <param name="reciptDept">开立科室</param>
        /// <param name="itemCode">项目编码</param>
        /// <param name="defaultExecDept">默认执行科室</param>
        /// <param name="alExecDept">执行科室列表</param>
        /// <returns></returns>
        public static int SetExecDept(bool isOut, string reciptDept, string itemCode, ref string defaultExecDept, ref ArrayList alExecDept)
        {
            FS.SOC.HISFC.Fee.Models.Undrug item = GetUndrugExecInfo(itemCode);

            if (item == null)
            {
                alExecDept = null;
                defaultExecDept = reciptDept;
            }
            else
            {
                if (string.IsNullOrEmpty(item.ExecDept)
                    || item.ExecDept == "ALL"
                    || item.ExecDept == "ALL|")
                {
                    alExecDept = null;
                    defaultExecDept = isOut ? item.DefaultExecDeptForOut : item.DefaultExecDeptForIn;
                }
                else
                {
                    string[] depts = item.ExecDept.Split('|');
                    alExecDept = new ArrayList();
                    string firstDept = "";
                    for (int i = 0; i < depts.Length; i++)
                    {
                        FS.HISFC.Models.Base.Department deptObj = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(depts[i]);
                        if (deptObj != null)
                        {
                            alExecDept.Add(deptObj);
                            if (string.IsNullOrEmpty(firstDept))
                            {
                                firstDept = deptObj.ID;
                            }
                        }
                    }

                    if (item.ExecDept.Contains(reciptDept))
                    {
                        defaultExecDept = reciptDept;
                    }
                    else
                    {
                        defaultExecDept = isOut ? item.DefaultExecDeptForOut : item.DefaultExecDeptForIn;
                    }

                    if (string.IsNullOrEmpty(defaultExecDept)
                        && !string.IsNullOrEmpty(firstDept))
                    {
                        defaultExecDept = firstDept;
                    }
                }

                if (string.IsNullOrEmpty(defaultExecDept))
                {
                    defaultExecDept = reciptDept;
                }
                if (alExecDept == null || alExecDept.Count == 0)
                {
                    if (deptValidHelper == null || deptValidHelper.ArrayObject.Count == 0)
                    {
                        alExecDept = SOC.HISFC.BizProcess.Cache.Common.GetValidDept();
                    }
                    else
                    {
                        alExecDept = deptValidHelper.ArrayObject;
                    }
                    
                }
            }

            return 1;
        }

        private static Dictionary<string, FS.SOC.HISFC.Fee.Models.Undrug> dicUndrugExec = new Dictionary<string, FS.SOC.HISFC.Fee.Models.Undrug>();

        /// <summary>
        /// 只获取项目的执行科室信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        private static FS.SOC.HISFC.Fee.Models.Undrug GetUndrugExecInfo(string itemCode)
        {
            FS.SOC.HISFC.Fee.Models.Undrug item = null;
            if (dicUndrugExec.ContainsKey(itemCode))
            {
                item = dicUndrugExec[itemCode];
            }
            else
            {
                FS.SOC.HISFC.Fee.BizLogic.Undrug undrugMgr = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
                item = undrugMgr.GetExecInfo(itemCode);

                dicUndrugExec.Add(itemCode, item);
            }

            return item;
        }
        #endregion

        public static string GetExecDept(bool isOut, string reciptDept, string itemCode)
        {
            string execDept = "";
            ArrayList alExecDept = null;

            SetExecDept(isOut, reciptDept, itemCode, ref execDept, ref alExecDept);

            return execDept;
        }

    }
}
