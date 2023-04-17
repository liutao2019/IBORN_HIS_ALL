using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy
{
    /// <summary>
    /// [功能描述: 入出库明细数据显示控件规范]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// </summary>
    public interface IDataDetail
    {
        /// <summary>
        /// 过滤字符串
        /// </summary>
        string Filter
        { set; }

        /// <summary>
        /// 显示数据的SheetView
        /// </summary>
        SOC.Windows.Forms.FpSpread FpSpread
        { get; }

        /// <summary>
        /// 显示数据的SheetView
        /// </summary>
        //FarPoint.Win.Spread.FpSpread FpSpread
        //{ get; }

        /// <summary>
        /// 对应配置文件的名称（包括全路径）
        /// </summary>
        string SettingFileName { set; }

        /// <summary>
        /// 信息（金额合计）
        /// </summary>
        string Info
        {
            set;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        int Init();

        /// <summary>
        /// 清空数据
        /// </summary>
        /// <returns></returns>
        int Clear();

        /// <summary>
        /// 获得输入焦点
        /// </summary>
        /// <returns></returns>
        int SetFocus();

        /// <summary>
        /// 获得输入焦点
        /// </summary>
        /// <returns></returns>
        int SetFocusToFilter();

        /// <summary>
        /// 是否有焦点
        /// </summary>
        bool IsContainsFocus
        {
            get;
        }

        Delegate.FilterTextChangeHander FilterTextChanged
        { get; set; }
    }
}
