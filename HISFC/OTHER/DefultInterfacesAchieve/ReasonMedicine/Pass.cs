using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;
using System.Collections;
using Neusoft.HISFC.BizProcess.Interface.Order;
using Neusoft.HISFC.Models.RADT;
using Neusoft.HISFC.Models.Registration;
using Neusoft.FrameWork.Function;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.DefultInterfacesAchieve
{
    /// <summary>
    /// 美康合理用药接口实现
    /// </summary>
    public class Pass : IReasonableMedicine
    {
        #region 接口调用

        /// <summary>
        /// 注册服务器（PASS客户端共享应用摸式注册是否成功）
        /// </summary>
        /// <returns>0	注册成功，否则失败；255  PASS服务器物理连接失败</returns>
        [DllImport("ShellRunAs.dll")]
        public static extern int RegisterServer();

        /// <summary>
        /// 初始化PASS是否正确
        /// </summary>
        /// <param name="userInfo">用户工号/登录用户</param>
        /// <param name="deptInfo">科室代码/科室中文名称</param>
        /// <param name="stationType">必须传值，10--指住院医生站、门诊医生站、护士站、PIVA静脉药物配置中心；20-指临床药学工作站</param>
        /// <returns>0 初始化PASS失败 1初始化PASS正常通过</returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassInit(string userInfo, string deptInfo, int stationType);

        /// <summary>
        /// PASS运行模式设置
        /// </summary>
        /// <param name="saveCheckResult">是否进行采集，取值： 0-不采集 1-依赖系统设置  （必须传值）</param>
        /// <param name="allowAllegen">是否管理病人过敏史/病生状态，取值：0-不管理1-由用户传入 2-PASS管理 3-PASS强制管理（必须传值）</param>
        /// <param name="checkMode">审查模式，取值：0-普通模式 1-IV模式 （必须传值）</param>
        /// <param name="disqMode">调用药研究时是否传入医嘱信息，取值：0-不传 1-要传 2-提示 （必须传值）</param>
        /// <param name="useDiposeIden">是否使用处理意见，取值：0-不使用处理 1-根据设置（必须传值）</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetControlParam(int saveCheckResult, int allowAllegen, int checkMode, int disqMode, int useDiposeIden);

        /// <summary>
        /// 传病人基本信息
        /// </summary>
        /// <param name="PatientID">病人病案编号（必须传值）</param>
        /// <param name="VisitID">当前就诊次数（必须传值）</param>
        /// <param name="Name">病人姓名    （必须传值）</param>
        /// <param name="Sex">病人性别    （必须传值）如：男、女，其他值传：未知</param>
        /// <param name="Birthday">出生日期    （必须传值）格式：2005-09-20</param>
        /// <param name="Weight">体重        （可以不传值）单位：KG</param>
        /// <param name="cHeight">身高        （可以不传值）单位：CM</param>
        /// <param name="DepartMentName"> 医嘱科室ID/医嘱科室名称  （可以不传值）</param>
        /// <param name="Doctor">主治医生ID/主治医生姓名 （可以不传值）</param>
        /// <param name="LeaveHospitalDate">出院日期 （可以不传值）</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetPatientInfo(string PatientID, string VisitID, string Name, string Sex, string Birthday, string Weight, string cHeight, string DepartMentName, string Doctor, string LeaveHospitalDate);

        /// <summary>
        /// 传入当前病人用药信息接口
        /// 如果当前病人有多条用药医嘱时，循环传入。传入的医嘱为用药医嘱，对于工作站类型为10即时性审查时(如：住院医生站或门诊医生站)，用药结束日期可以不用传值，默认为当天；而对于工作站类型为20回顾性审查时(如：临床药学工作或查询统计)，用药结束日期必须传值。
        /// </summary>
        /// <param name="OrderUniqueCode">医嘱唯一码（必须传值）</param>
        /// <param name="DrugCode">药品编码  （必须传值）</param>
        /// <param name="DrugName">药品名称  （必须传值）</param>
        /// <param name="SingleDose">每次用量  （必须传值）</param>
        /// <param name="DoseUnit">剂量单位  （必须传值）</param>
        /// <param name="Frequency">用药频率(次/天)（必须传值）</param>
        /// <param name="StartOrderDate">用药开始日期，格式：yyyy-mm-dd  （必须传值）</param>
        /// <param name="StopOrderDate">用药结束日期，格式：yyyy-mm-dd  （可以不传值），默认值为当天</param>
        /// <param name="RouteName">给药途径中文名称 （必须传值）</param>
        /// <param name="GroupTag">成组医嘱标志  （必须传值）</param>
        /// <param name="OrderType">是否为临时医嘱 1-是临时医嘱 0或空 长期医嘱 （必须传值）</param>
        /// <param name="OrderDoctor">下嘱医生ID/下嘱医生姓名 （必须传值）</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetRecipeInfo(string OrderUniqueCode, string DrugCode, string DrugName, string SingleDose, string DoseUnit, string Frequency, string StartOrderDate, string StopOrderDate, string RouteName, string GroupTag, string OrderType, string OrderDoctor);

        /// <summary>
        /// 当病人过敏史信息由HIS系统管理，并传入到PASS系统进行审查时，调用该接口
        /// </summary>
        /// <param name="allergenIndex">过敏原在医嘱中的顺序编号（必须传值）</param>
        /// <param name="allergenCode">过敏原编码（必须传值）</param>
        /// <param name="allergenDesc">过敏原名称（必须传值）</param>
        /// <param name="allergenType">过敏原类型（必须传值）取值如下(判断不分大小写)：
        ///     AllergenGroup  PASS过敏组
        ///     USER_Drug   用户药品
        ///     DrugName     PASS药物名称</param>
        /// <param name="reaction">过敏症状  （可以不传值）</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetAllergenInfo(string allergenIndex, string allergenCode, string allergenDesc, string allergenType, string reaction);

        /// <summary>
        /// 传入病生状态
        /// </summary>
        /// <param name="medCondIndex">医疗条件在医嘱中的顺序编号（必须传值）</param>
        /// <param name="medCondCode">医疗条件编码（必须传值）</param>
        /// <param name="medCondDesc">医疗条件名称（必须传值）</param>
        /// <param name="medCondType">医疗条件类型（必须传值）传值如下(判断不分大小写)
        /// USER_MedCond	用户医疗条件
        /// ICD				ICD-9CM编码</param>
        /// <param name="startDate">开始日期  格式：yyyy-mm-dd（可以不传值）</param>
        /// <param name="endDate">结束日期  格式：yyyy-mm-dd（可以不传值）</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetMedCond(string medCondIndex, string medCondCode, string medCondDesc, string medCondType, string startDate, string endDate);

        /// <summary>
        /// 传入当前药品查询信息
        /// </summary>
        /// <param name="drugCode">药品编码（必须传值）</param>
        /// <param name="drugName">药品名称（必须传值）</param>
        /// <param name="doseUnit">剂量单位（必须传值）</param>
        /// <param name="routeName">用药途径中文名称（必须传值）</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetQueryDrug(string drugCode, string drugName, string doseUnit, string routeName);

        /// <summary>
        /// PASS系统功能是否有效性
        /// 在刷新界面、切换病人、弹出右键菜单，总之是需要刷新查询功能时，都根据此函数的返回值来设置相应控件Enabled属性
        /// </summary>
        /// <param name="queryItemNo">查询的项目标识或项目编号，其中带”/”的部分，表示两上标识都可以用来查询此项目的状态(项目标识不区分大小写)，见下表</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassGetState(string queryItemNo);
        /*
        项目编号	项目标识	项目描述	说明
            0	PASSENABLE	PASS连接是否可用	审查相关命令状态

            11	SYS-SET	系统参数设置	合理用药辅助功能
            12	DISQUISITION	用药研究	
            13	MATCH-DRUG	药品配对信息查询	
            14	MATCH-ROUTE	药品给药途径信息查询	

            24	AlleyEnable	病生状态/过敏史管理	过敏史/病生状态管理

            101	CPRRes/CPR	临床用药指南查询	一级菜单查询状态
            102	Directions	药品说明书查询	
            103	CPERes/CPE	病人用药教育查询	
            104	CheckRes/CHECKINFO	药物检验值查询	
            105	HisDrugInfo	医院药品信息查询	
            106	MEDInfo	药物信息查询中心	
            107	Chp	中国药典	
            501	DISPOSE	处理意见设置	
            220	LMIM	临床检验信息参考查询	

            201	DDIM	药物与药物相互作用查询	二级菜单查询状态(专项信息)
            202	DFIM	药物与食物相互作用查询	
            203	MatchRes/IV	国内注射剂体外配伍查询	
            204	TriessRes/IVM	国外注射剂体外配伍查询	
            205	DDCM	禁忌症查询	
            206	SIDE	副作用查询	
            207	GERI	老年人用药警告查询	
            208	PEDI	儿童用药警告查询	
            209	PREG	妊娠期用药警告查询	
            210	LACT	哺乳期用药警告查询	

            301	HELP	Pass系统帮助	合理用药帮助系统状态
         * */

        /// <summary>
        /// PASS功能调用
        /// </summary>
        /// <param name="commandNo">CommandNo命令号，表示调用PASS系统功能命令号</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassDoCommand(int commandNo);
        /*
         * 审查类
         * 
         * 1	住院医生工作站保存自动审查	1-审查正常通过            0-存在未处理问题
            2	住院医生工作站提交自动审查	1-审查正常通过            0-存在未处理问题
            33	门诊医生工作站保存自动审查	1-审查正常通过            0-存在未处理问题
            34	门诊医生工作站提交自动审查	1-审查正常通过            0-存在未处理问题
            3	手工审查	暂无意义
            4	临床药学单病人审查	暂无意义
            5	临床药学多病人审查	暂无意义
            6	单药警告	暂无意义
            7	手动审查,不显结果界面	暂无意义
         * 
            查询类
         * 
            13	药品配对信息查询	暂无意义
            14	药品给药途径信息查询	暂无意义
            101	临床用药指南查询	暂无意义
            102	药品说明书查询	暂无意义
            103	病人用药教育查询	暂无意义
            104	药物检验值查询	暂无意义
            105	医院药品信息查询	暂无意义
            106	药物信息查询中心	暂无意义
            107	中国药典	暂无意义
            201	药物与药物相互作用查询	暂无意义
            202	药物与食物相互作用查询	暂无意义
            203	国内注射剂体外配伍查询	暂无意义
            204	国外注射剂体外配伍查询	暂无意义
            205	禁忌症查询	暂无意义
            206	副作用查询	暂无意义
            207	老年人用药警告查询	暂无意义
            208	儿童用药警告查询	暂无意义
            209	妊娠期用药警告查询	暂无意义
            210	哺乳期用药警告查询	暂无意义
            220	临床检验信息参考查询	暂无意义
         * 
         * 窗口控制类
         * 
            401	显示浮动窗口	暂无意义
            402	关闭所有浮动窗口	暂无意义
            403	显示单药最近一次审查结果浮动提示窗口	暂无意义
         * 
         * 综合类
         * 
            11	系统参数设置	暂无意义
            12	用药研究	暂无意义
            21	病生状态/过敏史管理(只读)	暂无意义
            22	病生状态/过敏史管理(编辑)	病人过敏史/病生状态是否发生了变化。2-发生了变化，1-未发生变化，<=0-出现程序错误。
            23	病生状态/过敏史管理(强制)	病人过敏史/病生状态是否发生了变化。2-发生了变化，1-未发生变化，<=0-出现程序错误。

         * */

        /// <summary>
        /// 获取PASS审查结果警示级别
        /// 嵌套时用户可以根据审查返回警告值，进行对医嘱是否需要保存或提交控制，同时还可以将该警告值保存到HIS系统数据库中，便于其它工作站查看等。如果一条用药医嘱经过PASS审查发现可能存在多个潜在用药问题，系统将以其中警示级别最高的警示色标示该条医嘱 。需要注意的是，审查返回警告值不是最高代表最严重，而是警示级别最高的警示色标代表最严重
        /// </summary>
        /// <param name="drugUniqeCode">医嘱唯一编码</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassGetWarn(string drugUniqeCode);
        /*
         * 审查返回警告值	警示色	说明
            -3	无灯	表示PASS出现错误，未进行审查
            -2	无灯	表示该药品在处方传送时被过滤
            -1	无灯	表示未经过PASS系统的监测
            0	蓝灯	PASS监测未提示相关用药问题
            1	黄灯	危害较低或尚不明确，适度关注
            2	红灯	不推荐或较严重危害，高度关注
            4	橙灯	慎用或有一定危害，较高度关注
            3	黑灯	绝对禁忌、错误或致死性危害，严重关注
         * */

        /// <summary>
        /// 设置药品浮动窗口位置
        /// 传入当前药品浮动窗口显示位置时，调用本接口，但是调用本接口之前首先调用PassSetQueryDrug() 函数，传入当前药品查询信息
        /// </summary>
        /// <param name="left">左上角x</param>
        /// <param name="top">左上角y</param>
        /// <param name="right">右下角x</param>
        /// <param name="bottom">右下角y</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetFloatWinPos(int left, int top, int right, int bottom);

        /// <summary>
        /// 显示警告浮动窗口或单药警告
        /// </summary>
        /// <param name="drugUniqueCode">医嘱唯一编码</param>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassSetWarnDrug(string drugUniqueCode);

        /// <summary>
        /// PASS退出
        /// </summary>
        /// <returns></returns>
        [DllImport("DIFPassDll.dll")]
        public static extern int PassQuit();

        #endregion

        #region 变量

        /// <summary>
        /// 错误信息
        /// </summary>
        private string err = "";

        /// <summary>
        /// 是否启用合理用药审查
        /// </summary>
        private bool passEnabled = false;

        /// <summary>
        /// 0 初始化PASS失败 1初始化PASS正常通过
        /// </summary>
        private int stationType = 0;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Err
        {
            get
            {
                return this.err;
            }
            set
            {
                this.err = value;
            }
        }

        /// <summary>
        /// 是否启用合理用药审查
        /// </summary>
        public bool PassEnabled
        {
            get
            {
                return this.passEnabled;
            }
            set
            {
                this.passEnabled = value;
            }
        }

        /// <summary>
        /// 0 初始化PASS失败 1初始化PASS正常通过
        /// </summary>
        public int StationType
        {
            get
            {
                return this.stationType;
            }
            set
            {
                this.stationType = value;
            }
        }

        /// <summary>
        /// 暂存药品基本信息
        /// </summary>
        Neusoft.HISFC.Models.Pharmacy.Item phaItem = null;

        #endregion

        #region 业务层函数

        /// <summary>
        /// 药品业务
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        #endregion

        #region 方法

        /// <summary>
        /// PASS功能调用
        /// </summary>
        /// <param name="commandType">PASS功能命令号</param>
        /// <returns></returns>
        public int DoCommand(int commandType)
        {
            //判断连接是否可用
            if (this.PassEnabled && (PassGetState("0") != 0))
            {
                return PassDoCommand(commandType);
            }
            return -4;
        }

        /// <summary>
        /// PASS系统功能是否有效性检验
        /// </summary>
        /// <param name="queryItemNo"></param>
        /// <returns></returns>
        public int PassGetStateIn(string queryItemNo)
        {
            return PassGetState(queryItemNo);
        }

        /// <summary>
        /// 获取PASS审查结果警示级别
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int PassGetWarnFlag(string orderId)
        {
            if (this.PassEnabled && (PassGetState("0") != 0))
            {
                return PassGetWarn(orderId);
            }
            return -4;
        }

        /// <summary>
        /// 显示警告浮动窗口或单药警告
        /// </summary>
        /// <param name="orderId">医嘱唯一编码</param>
        /// <param name="flag">暂时无用</param>
        /// <returns></returns>
        public int PassGetWarnInfo(string orderId, string flag)
        {
            if (this.PassEnabled && (PassGetState("0") != 0))
            {
                PassSetWarnDrug(orderId);
                if (flag == "0")
                {
                    PassDoCommand(0x193);
                }
                else
                {
                    PassDoCommand(6);
                }
            }
            return 1;
        }

        /// <summary>
        /// PASS初始化
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="deptID">科室ID</param>
        /// <param name="deptName">科室名称</param>
        /// <param name="stationType">工作站类型(10-医生工作站 20-药学工作站)</param>
        /// <returns></returns>
        public int PassInit(string userID, string userName, string deptID, string deptName, int stationType)
        {
            int rev = this.PassInit(userID, userName, deptID, deptName, stationType, false);

            if (rev != 1)
            {
                try
                {
                    if (System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "/MedcomPassClient.exe"))
                    {
                        System.Diagnostics.Process.Start(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "/MedcomPassClient.exe");
                    }
                }
                catch
                {
                    return rev;
                }
                rev = this.PassInit(userID, userName, deptID, deptName, stationType, true);
            }
            return rev;
        }

        /// <summary>
        /// PASS初始化
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="deptID">科室ID</param>
        /// <param name="deptName">科室名称</param>
        /// <param name="stationType">工作站类型(10-医生工作站 20-药学工作站)</param>
        /// <param name="isJudgeLocalSetting">是否判断本地xml配置文件（5.0 无用）</param>
        /// <returns></returns>
        public int PassInit(string userID, string userName, string deptID, string deptName, int stationType, bool isJudgeLocalSetting)
        {
            Exception exception;
            Neusoft.FrameWork.Management.ControlParam controler = new Neusoft.FrameWork.Management.ControlParam();

            bool isEnablePass = false;
            try
            {
                isEnablePass = NConvert.ToBoolean(controler.QueryControlerInfo("200014"));
            }
            catch (Exception exception1)
            {
                exception = exception1;
                this.err = exception.Message;
                this.PassEnabled = false;
                return -1;
            }
            if (!isEnablePass)
            {
                this.PassEnabled = false;
                return 0;
            }
            if (isJudgeLocalSetting)
            {
                try
                {
                    ArrayList defaultValue = Neusoft.FrameWork.WinForms.Classes.Function.GetDefaultValue("PassSetting", out this.err);
                    if ((defaultValue == null) || (defaultValue.Count == 0))
                    {
                        this.PassEnabled = false;
                    }
                    else if ((defaultValue[0] as string) == "1")
                    {
                        this.PassEnabled = true;
                    }
                    else
                    {
                        this.PassEnabled = false;
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    this.err = exception.Message;
                    this.PassEnabled = false;
                    return -1;
                }
                if (!this.PassEnabled)
                {
                    this.PassEnabled = false;
                    return 0;
                }
            }
            try
            {
                int rev = PassInit(userID + "/" + userName, deptID + "/" + deptName, stationType);

                if (rev != 1)
                {
                    try
                    {
                        if (System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe"))
                        {
                            System.Diagnostics.Process.Start(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe");
                            DateTime time = DateTime.Now;
                            while (DateTime.Now < time.AddSeconds(3))
                            {
                            }
                        }
                        rev = PassInit(userID + "/" + userName, deptID + "/" + deptName, stationType);
                    }
                    catch
                    {
                        this.PassEnabled = false;
                        return rev;
                    }
                }

                if (rev == 0)
                {
                    this.PassEnabled = false;
                    this.err = "合理用药系统 初始化失败! 请与管理员联系";
                    return -1;
                }
                if (PassGetState("0") != 0)
                {
                    PassSetControlParam(1, 2, 0, 2, 1);
                    this.passEnabled = true;
                }
            }
            catch (DllNotFoundException)
            {
                try
                {
                    if (System.IO.File.Exists(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe"))
                    {
                        System.Diagnostics.Process.Start(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "MedicomSoftWare.exe");
                        DateTime time = DateTime.Now;
                        while (DateTime.Now < time.AddSeconds(3))
                        {
                        }
                    }
                    int rev = PassInit(userID + "/" + userName, deptID + "/" + deptName, stationType);

                    if (rev > 0)
                    {
                        return rev;
                    }
                }
                catch
                {
                }

                this.err = "未找到合理用药系统正常运行所需Dll 合理用药系统将无法正常启动！\n                 请与管理员联系";
                this.PassEnabled = false;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 7.2.8	传入当前查询药品信息
        /// 当需要实现药物信息查询或浮动窗口功能，调用该接口
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="drugName"></param>
        /// <param name="doseUnit"></param>
        /// <param name="useName"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        public void PassQueryDrug(string drugCode, string drugName, string doseUnit, string useName, int left, int top, int right, int bottom)
        {
            if (this.PassEnabled && (PassGetState("0") != 0))
            {
                int rev = 0;
                rev = PassSetQueryDrug(drugCode, drugName, doseUnit, useName);
                rev = PassSetFloatWinPos(left, top, right, bottom);
                //rev = PassSetFloatWinPos(10, 20, 300, 400);
                this.ShowFloatWin(true);
            }
        }

        /// <summary>
        /// 退出PASS
        /// </summary>
        /// <returns></returns>
        public int PassQuitIn()
        {
            if (this.passEnabled && (PassGetState("0") != 0))
            {
                return PassQuit();
            }
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient">患者信息</param>
        /// <param name="alOrder">医嘱列表</param>
        /// <param name="checkType">PASS功能编码</param>
        /// <returns></returns>
        public int PassSaveCheck(PatientInfo patient, List<Neusoft.HISFC.Models.Order.Inpatient.Order> alOrder, int checkType)
        {
            if (!this.PassEnabled)
            {
                return 0;
            }
            if ((alOrder == null) || (alOrder.Count == 0))
            {
                return -1;
            }

            foreach (Neusoft.HISFC.Models.Order.Inpatient.Order order in alOrder)
            {
                if (order == null)
                {
                    this.err = "执行医嘱用药审查时出错! 发生类型转换错误";
                    return -1;
                }
                if ((order.Item.SysClass.ID.ToString().Substring(0, 1) == "P") && (order.Status != 3))
                {
                    try
                    {
                        this.PassSetRecipeInfo(order);
                    }
                    catch (Exception exception)
                    {
                        this.err = "合理用药审查传送医嘱数据过程中发生错误! " + exception.Message;
                        return -1;
                    }
                }
            }
            //PASS功能调用
            return PassDoCommand(checkType);
            return 1;
        }

        /// <summary>
        /// 用药信息审查
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alOrder"></param>
        /// <param name="checkType"></param>
        /// <returns></returns>
        public int PassSaveCheck(Register patient, List<Neusoft.HISFC.Models.Order.OutPatient.Order> alOrder, int checkType)
        {
            if (!this.PassEnabled)
            {
                return 0;
            }
            if ((alOrder == null) || (alOrder.Count == 0))
            {
                return -1;
            }

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                if (order == null)
                {
                    this.err = "执行医嘱用药审查时出错! 发生类型转换错误";
                    return -1;
                }
                if ((order.Item.ItemType == EnumItemType.Drug) && (order.Status != 3))
                {
                    try
                    {
                        this.PassSetRecipeInfo(order);
                    }
                    catch (Exception exception)
                    {
                        this.err = "合理用药审查传送医嘱数据过程中发生错误! " + exception.Message;
                        return -1;
                    }
                }
            }
            int rev = PassDoCommand(checkType);

            return rev;
            return 1;
        }

        /// <summary>
        /// 设置当前用药信息
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="drugName"></param>
        /// <param name="doseUnit"></param>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public int PassSetDrug(string drugCode, string drugName, string doseUnit, string routeName)
        {
            return PassSetQueryDrug(drugCode, drugName, doseUnit, routeName);
        }

        /// <summary>
        /// 传入住院病人信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="docID"></param>
        /// <param name="docName"></param>
        public void PassSetPatientInfo(PatientInfo patient, string docID, string docName)
        {
            if (((patient != null) && this.PassEnabled) && (PassGetState("0") != 0))
            {
                string str2;
                string patientNO = patient.PID.PatientNO;
                try
                {
                    str2 = NConvert.ToInt32(patient.ID.Substring(2, 2)).ToString();
                }
                catch
                {
                    str2 = "1";
                }
                string name = patient.Name;
                string sex = "";
                if (patient.Sex.ID.ToString() == "F")
                {
                    sex = "男";
                }
                else if (patient.Sex.ID.ToString() == "M")
                {
                    sex = "女";
                }
                else
                {
                    sex = "未知";
                }
                string birthday = patient.Birthday.ToString("yyyy-MM-dd");
                string weight = patient.Weight;
                string height = patient.Height;
                string departMentName = patient.PVisit.PatientLocation.Dept.ID + "/" + patient.PVisit.PatientLocation.Dept.Name;
                string doctor = patient.PVisit.AdmittingDoctor.ID + "/" + patient.PVisit.AdmittingDoctor.Name;
                string leaveHospitalDate = "";
                PassDoCommand(0x192);
                PassSetPatientInfo(patientNO, str2, name, sex, birthday, weight, height, departMentName, doctor, leaveHospitalDate);
            }
        }

        /// <summary>
        /// 传入门诊病人信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="docID"></param>
        /// <param name="docName"></param>
        public void PassSetPatientInfo(Register patient, string docID, string docName)
        {
            if (((patient != null) && this.PassEnabled) && (PassGetState("0") != 0))
            {
                string str2;
                string patientNO = patient.PID.CardNO;
                try
                {
                    str2 = NConvert.ToInt32(patient.ID.Substring(2, 2)).ToString();
                }
                catch
                {
                    str2 = "1";
                }
                string name = patient.Name;
                string sex = "";
                if (patient.Sex.ID.ToString() == "F")
                {
                    sex = "男";
                }
                else if (patient.Sex.ID.ToString() == "M")
                {
                    sex = "女";
                }
                else
                {
                    sex = "未知";
                }
                string birthday = patient.Birthday.ToString("yyyy-MM-dd");
                string weight = patient.Weight;
                string height = patient.Height;
                string departMentName = patient.DoctorInfo.Templet.Dept.ID + "/" + patient.DoctorInfo.Templet.Dept.Name;

                if (!string.IsNullOrEmpty(patient.SeeDoct.Dept.ID))
                {
                    departMentName = patient.SeeDoct.Dept.ID + "/" + patient.SeeDoct.Dept.Name;
                }

                //看诊/挂号医生
                string doctor = patient.DoctorInfo.Templet.Doct.ID + "/" + patient.DoctorInfo.Templet.Doct.Name;

                if (!string.IsNullOrEmpty(patient.SeeDoct.ID))
                {
                    doctor = patient.SeeDoct.ID + "/" + patient.SeeDoct.Name;
                }

                string leaveHospitalDate = "";
                PassDoCommand(0x192);
                PassSetPatientInfo(patientNO, str2, name, sex, birthday, weight, height, departMentName, doctor, leaveHospitalDate);
            }
        }

        /// <summary>
        /// 传入住院医嘱信息
        /// </summary>
        /// <param name="order"></param>
        public void PassSetRecipeInfo(Neusoft.HISFC.Models.Order.Inpatient.Order order)
        {
            if (this.PassEnabled && ((order != null) && (order.Item.ItemType.ToString() != ItemTypes.Undrug.ToString())))
            {
                string applyNO = order.ApplyNo;
                string iD = order.Item.UserCode;

                phaItem = this.phaIntegrate.GetItem(iD);
                if (phaItem != null)
                {
                    iD = phaItem.UserCode;
                }

                //string iD = "Y00000001077";
                string name = order.Item.Name;
                string singleDose = order.DoseOnce.ToString();
                string doseUnit = order.DoseUnit;
                int length = 0;
                string str7 = "";
                if ((order.Frequency != null) && (order.Usage != null))
                {
                    if (order.Frequency.Time == null)
                    {
                        Neusoft.HISFC.BizLogic.Manager.Frequency frequency = new Neusoft.HISFC.BizLogic.Manager.Frequency();
                        order.Frequency = (Neusoft.HISFC.Models.Order.Frequency)frequency.Get(order.Frequency, "ROOT");
                        if (order.Frequency == null)
                        {
                            return;
                        }
                    }
                    if (order.Frequency.Time == null)
                    {
                        length = 1;
                    }
                    else
                    {
                        length = order.Frequency.Times.Length;
                    }
                    if (order.Frequency.Days == null)
                    {
                        str7 = "1";
                    }
                    else
                    {
                        str7 = order.Frequency.Days[0];
                    }
                    string str6 = length.ToString() + "/" + str7.ToString();
                    string startOrderDate = order.BeginTime.ToString("yyyy-MM-dd");
                    string stopOrderDate = "";
                    string routeName = order.Usage.Name;
                    string groupTag = order.Combo.ID;
                    string orderType = order.OrderType.ID;
                    string orderDoctor = order.Doctor.ID + "/" + order.Doctor.Name;
                    PassSetRecipeInfo(applyNO, iD, name, singleDose, doseUnit, str6, startOrderDate, stopOrderDate, routeName, groupTag, orderType, orderDoctor);
                }
            }
        }

        /// <summary>
        /// 传入门诊处方信息
        /// </summary>
        /// <param name="order"></param>
        public void PassSetRecipeInfo(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            if (this.PassEnabled && ((order != null) && (order.Item.ItemType == EnumItemType.Drug)))
            {
                //医嘱唯一码（必须传值）
                string applyNO = order.ApplyNo;

                // 药品编码  （必须传值）
                string iD = order.Item.ID;

                //药品名称  （必须传值）
                string name = order.Item.Name;

                //每次用量  （必须传值）
                string singleDose = order.DoseOnce.ToString();

                //剂量单位  （必须传值）
                string doseUnit = order.DoseUnit;

                #region 用药频率(次/天)（必须传值）
                int length = 0;
                string str7 = "";
                string str6 = "";
                if ((order.Frequency != null) && (order.Usage != null))
                {
                    if (order.Frequency.Time == null)
                    {
                        Neusoft.HISFC.BizLogic.Manager.Frequency frequency = new Neusoft.HISFC.BizLogic.Manager.Frequency();
                        order.Frequency = (Neusoft.HISFC.Models.Order.Frequency)frequency.Get(order.Frequency, "ROOT");
                        if (order.Frequency == null)
                        {
                            return;
                        }
                    }
                    if (order.Frequency.Time == null)
                    {
                        length = 1;
                    }
                    else
                    {
                        length = order.Frequency.Times.Length;
                    }
                    if (order.Frequency.Days == null)
                    {
                        str7 = "1";
                    }
                    else
                    {
                        str7 = order.Frequency.Days[0];
                    }
                    str6 = length.ToString() + "/" + str7.ToString();
                }
                #endregion

                //用药开始日期，格式：yyyy-mm-dd  （必须传值）
                string startOrderDate = order.MOTime.ToString("yyyy-MM-dd");

                //用药结束日期，格式：yyyy-mm-dd  （可以不传值），默认值为当天
                string stopOrderDate = "";

                //给药途径中文名称 （必须传值）
                string routeName = order.Usage.Name;

                //成组医嘱标志  （必须传值）
                string groupTag = order.Combo.ID;

                //是否为临时医嘱 1-是临时医嘱 0或空 长期医嘱 （必须传值）
                string orderType = "1";

                //下嘱医生ID/下嘱医生姓名 （必须传值）
                string orderDoctor = order.ReciptDoctor.ID + "/" + order.ReciptDoctor.Name;

                PassSetRecipeInfo(applyNO, iD, name, singleDose, doseUnit, str6, startOrderDate, stopOrderDate, routeName, groupTag, orderType, orderDoctor);
            }
        }

        /// <summary>
        /// 设置浮动窗口是否显示
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowFloatWin(bool isShow)
        {
            if (this.PassEnabled && (PassGetState("0") != 0))
            {
                if (isShow)
                {
                    PassDoCommand(401);
                }
                else
                {
                    PassDoCommand(402);
                }
            }
        }

        #endregion

        #region IReasonableMedicine 成员


        public int PassClose()
        {
            return 1;
        }

        public int PassDrugCheck(ArrayList alOrder, bool isSave)
        {
            return 1;
        }

        public int PassInit(Neusoft.FrameWork.Models.NeuObject logEmpl, Neusoft.FrameWork.Models.NeuObject logDept, string workStationType)
        {
            return 1;
        }

        public int PassRefresh()
        {
            return 1;
        }

        public int PassSetPatientInfo(Patient patient, Neusoft.FrameWork.Models.NeuObject recipeDoct)
        {
            return 1;
        }

        public int PassShowFloatWindow(bool isShow)
        {
            return 1;
        }

        public int PassShowOtherInfo(Neusoft.HISFC.Models.Order.Order order, Neusoft.FrameWork.Models.NeuObject queryType, ref ArrayList alShowMemu)
        {
            return 1;
        }

        public int PassShowSingleDrugInfo(Neusoft.HISFC.Models.Order.Order order, System.Drawing.Point LeftTop, System.Drawing.Point RightButton, bool isFirst)
        {
            return 1;
        }

        public int PassShowWarnDrug(Neusoft.HISFC.Models.Order.Order order)
        {
            return 1;
        }

        ServiceTypes station = ServiceTypes.C;

        ServiceTypes IReasonableMedicine.StationType
        {
            get
            {
                return station;
            }
            set
            {
                station = value;
            }
        }

        #endregion
    }
}
