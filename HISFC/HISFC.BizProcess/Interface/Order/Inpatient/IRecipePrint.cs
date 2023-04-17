using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Models;
using FS.HISFC.Models.Registration;

namespace FS.HISFC.BizProcess.Interface.Order.Inpatient
{
    #region 虚方法实现

    ///// <summary>
    ///// 处方打印接口，虚方法
    ///// </summary>
    //public abstract class IRecipePrint : IRecipePrintBase
    //{
    //    #region IRecipePrintBase 成员

    //    public virtual void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
    //    {
    //        return;
    //    }

    //    public virtual string RecipeNO
    //    {
    //        get
    //        {
    //            return null;
    //        }
    //        set
    //        {
    //        }
    //    }

    //    public void ppp()
    //    {
    //    }

    //    public virtual void PrintRecipe()
    //    {
    //        return;
    //    }

    //    public virtual void PrintRecipeView()
    //    {
    //        return;
    //    }

    //    public virtual string Printer
    //    {
    //        get
    //        {
    //            return null;
    //        }
    //        set
    //        {
    //        }
    //    }

    //    #endregion
    //}

    #endregion

    #region 接口定义

    /// <summary>
    /// 门诊处方打印接口
    /// </summary>
    public interface IRecipePrint
    {

        /// <summary>
        /// 错误信息
        /// </summary>
        string ErrInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo);
        /// <summary>
        /// 打印接口
        /// </summary>
        /// <param name="regObj">挂号实体</param>
        /// <param name="reciptDept">处方开立科室</param>
        /// <param name="reciptDoct">处方开立医生</param>
        /// <param name="orderList">处方List</param>
        /// <param name="orderSelectList">选中的List</param>
        /// <param name="IsReprint">是否补打</param>
        /// <param name="IsPreview">是否预览</param>
        /// <param name="printType">打印类型</param>
        /// <param name="obj">预留字段</param>
        /// <returns></returns>
        int OnInPatientPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, NeuObject reciptDept, NeuObject reciptDoct, ArrayList orderPrintList, ArrayList orderSelectList, bool IsReprint, bool IsPreview, string printType, object obj);

    }

    #endregion
}
