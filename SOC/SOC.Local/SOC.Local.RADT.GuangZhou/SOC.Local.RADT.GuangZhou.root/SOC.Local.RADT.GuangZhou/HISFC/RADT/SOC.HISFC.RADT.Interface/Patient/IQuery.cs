using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.HISFC.RADT.Interface.Patient
{

    /// <summary>
    /// 查询类别
    /// </summary>
    public enum EnumQueryType
    {
        /// <summary>
        /// 住院号
        /// </summary>
        PatientNo,

        /// <summary>
        /// 姓名和性别
        /// </summary>
        NameSex,

        /// <summary>
        /// 科室和床号
        /// </summary>
        DeptBedNo
    }

    /// <summary>
    /// [功能描述: 查询患者信息]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// 患者信息
        /// </summary>
        Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 是否选择
        /// </summary>
        bool IsSelect
        {
            get;
        }

        /// <summary>
        /// 是否旧系统数据
        /// </summary>
        bool IsOldSystem
        {
            get;
        }

        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="enumQueryType"></param>
        /// <returns></returns>
        int Query(EnumQueryType enumQueryType);

        /// <summary>
        /// 显示界面
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        void Show(System.Windows.Forms.IWin32Window owner);

        /// <summary>
        /// 清空界面
        /// </summary>
        void Clear();
    }
}
