using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Registration
{
    /// <summary>
    /// 挂号处理接口{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
    /// </summary>
    public interface IProcessRegiter
    {
        /// <summary>
        /// 保存开始时处理
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        int SaveBegin(ref FS.HISFC.Models.Registration.Register regObj, ref string errText);

        /// <summary>
        /// 保存结束时处理
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        int SaveEnd(ref FS.HISFC.Models.Registration.Register regObj, ref string errText);
    }

    /// <summary>
    /// 挂号票打印
    /// </summary>
    public interface IRegPrint
    {
        ///<summary>
        ///数据库连接
        ///</summary>
        System.Data.IDbTransaction Trans
        {
            get;
            set;
        }
        /// <summary>
        /// 添值
        /// </summary>
        /// <param name="register"></param>
        /// <param name="reglvlfee"></param>
        /// <returns></returns>

        int SetPrintValue(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns>>成功 1 失败 -1</returns>
        int PrintView();
        /// <summary>
        /// 打印
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>

        int Print();

        /// <summary>
        /// 清空当前信息
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        int Clear();

        /// <summary>
        /// 设置本地数据库连接
        /// </summary>
        /// <param name="trans">数据库连接</param>
        void SetTrans(System.Data.IDbTransaction trans);
    }

    /// <summary>
    /// 腕带打印// {AD405AA0-3101-46c0-A0B6-846DC3AAB10A}
    /// </summary>
    public interface IBraceletPrint
    {
        /// <summary>
        /// 添值
        /// </summary>
        /// <param name="register"></param>
        /// <param name="reglvlfee"></param>
        /// <returns></returns>

        int SetPrintValue(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        void Print();

    }
    /// <summary>
    /// 挂号票打印
    /// </summary>
    public interface IShowLED
    {
        ///<summary>
        ///数据库连接
        ///</summary>
        //System.Data.IDbTransaction Trans
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="register"></param>
        /// <param name="reglvlfee"></param>
        /// <returns></returns>

        string Query();


        /// <summary>
        /// 显示farpoint格式
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>

        int SetFPFormat();

        /// <summary>
        ///  调用LED 接口 组成显示串给LED
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        int CreateString();

        /// <summary>
        /// 设置本地数据库连接
        /// </summary>
        /// <param name="trans">数据库连接</param>
        //void SetTrans(System.Data.IDbTransaction trans);
    }

    /// <summary>
    /// 查找卡号或者病历号失败处理
    /// </summary>
    public interface ISearchCardNoFalse
    {
        /// <summary>
        /// 传递卡号
        /// </summary>
        /// <returns></returns>
        int PostMarkNo(ref string markNo);
    }
}
