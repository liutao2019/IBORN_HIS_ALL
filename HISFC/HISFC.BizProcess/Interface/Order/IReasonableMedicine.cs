using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Registration;
using System.Drawing;
using System.Collections;

namespace FS.HISFC.BizProcess.Interface.Order
{
    /// <summary>
    /// 合理用药接口定义
    /// </summary>
    public interface IReasonableMedicine
    {
        /* 基本功能描述
         * 1、输入药品后，显示要点提示
         * 2、点击药品名称列或者双击行，再次显示要点提示
         * 3、每输入一个药品都提交合理用药进行审查
         * 4、保存时进行统一审查
         * 5、右键可以查看合理用药的功能信息菜单
         * 
         * 其他功能
         * 1、儿童、孕妇、老年人用药提示
         * 2、过敏史提示
         * 
         * */

        /// <summary>
        /// 工作站类别
        /// </summary>
        FS.HISFC.Models.Base.ServiceTypes StationType
        {
            get;
            set;
        }

        /// <summary>
        /// 合理用药功能是否可用
        /// </summary>
        bool PassEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        string Err
        {
            get;
            set;
        }

        /// <summary>
        /// 合理用药系统初始化
        /// </summary>
        /// <param name="logEmpl">登陆人员</param>
        /// <param name="logDept">登陆科室</param>
        /// <param name="workStationType">工作站类型</param>
        /// <returns>0 初始化失败 1 初始化成功</returns>
        int PassInit(FS.FrameWork.Models.NeuObject logEmpl, FS.FrameWork.Models.NeuObject logDept, string workStationType);

        /// <summary>
        /// 设置传入患者基本信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        int PassSetPatientInfo(FS.HISFC.Models.RADT.Patient patient, FS.FrameWork.Models.NeuObject recipeDoct);

        /// <summary>
        /// 显示单个药品要点提示
        /// </summary>
        /// <param name="order">处方（医嘱）信息</param>
        /// <param name="LeftTop">左上角坐标</param>
        /// <param name="RightButton">右下角坐标</param>
        /// <returns></returns>
        int PassShowSingleDrugInfo(FS.HISFC.Models.Order.Order order, System.Drawing.Point LeftTop, Point RightButton, bool isFirst);

        /// <summary>
        /// 设置是否显示要点提示框
        /// </summary>
        /// <param name="isShow"></param>
        /// <returns></returns>
        int PassShowFloatWindow(bool isShow);

        /// <summary>
        /// 药品统一审查
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <param name="alOrder">处方（医嘱）列表</param>
        /// <param name="isSave">是否保存</param>
        /// <returns></returns>
        int PassDrugCheck(ArrayList alOrder, bool isSave);

        /// <summary>
        /// 合理用药功能初始化刷新
        /// </summary>
        /// <returns></returns>
        int PassRefresh();

        /// <summary>
        /// 合理用药功能关闭
        /// </summary>
        /// <returns></returns>
        int PassClose();

        /// <summary>
        /// 获取查询功能
        /// </summary>
        /// <param name="order">医嘱</param>
        /// <param name="queryType">查询功能类别</param>
        /// <param name="alShowMemu">查询菜单列表</param>
        /// <returns></returns>
        int PassShowOtherInfo(FS.HISFC.Models.Order.Order order, FS.FrameWork.Models.NeuObject queryType, ref ArrayList alShowMemu);

        /// <summary>
        /// 获取药品警告信息
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        int PassShowWarnDrug(FS.HISFC.Models.Order.Order order);

        /// <summary>
        /// 设置诊断信息
        /// </summary>
        /// <param name="diagnoseList"></param>
        /// <returns></returns>
        int PassSetDiagnoses(ArrayList diagnoseList);

        #region 旧的作废

        ///// <summary>
        ///// PASS功能调用
        ///// </summary>
        ///// <param name="commandType"></param>
        ///// <returns></returns>
        //int DoCommand(int commandType);

        ///// <summary>
        ///// PASS系统功能是否有效性检验
        ///// </summary>
        ///// <param name="queryItemNo"></param>
        ///// <returns></returns>
        //int PassGetStateIn(string queryItemNo);


        //int PassGetWarnFlag(string orderId);
        //int PassGetWarnInfo(string orderId, string flag);
        //int PassInit(string userID, string userName, string deptID, string deptName, int stationType, bool isJudgeLocalSetting);
        //void PassQueryDrug(string drugCode, string drugName, string doseUnit, string useName, int left, int top, int right, int bottom);
        //int PassQuitIn();
        //int PassSaveCheck(PatientInfo patient, List<FS.HISFC.Models.Order.Inpatient.Order> listOrder, int checkType);
        //int PassSaveCheck(Register patient, List<FS.HISFC.Models.Order.OutPatient.Order> listOrder, int checkType);
        //int PassSetDrug(string drugCode, string drugName, string doseUnit, string routeName);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="patient"></param>
        ///// <param name="docID"></param>
        ///// <param name="docName"></param>
        //void PassSetPatientInfo(PatientInfo patient, string docID, string docName);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="patient"></param>
        ///// <param name="docID"></param>
        ///// <param name="docName"></param>
        //void PassSetPatientInfo(Register patient, string docID, string docName);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="order"></param>
        //void PassSetRecipeInfo(FS.HISFC.Models.Order.Inpatient.Order order);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="order"></param>
        //void PassSetRecipeInfo(FS.HISFC.Models.Order.OutPatient.Order order);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="isShow"></param>
        //void ShowFloatWin(bool isShow);

        ///// <summary>
        ///// 错误信息
        ///// </summary>
        //string Err
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 合理用药功能是否可用
        ///// </summary>
        //bool PassEnabled
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 0 初始化PASS失败 1初始化PASS正常通过
        ///// </summary>
        //int StationType
        //{
        //    get;
        //    set;
        //}

        #endregion
    }
}
