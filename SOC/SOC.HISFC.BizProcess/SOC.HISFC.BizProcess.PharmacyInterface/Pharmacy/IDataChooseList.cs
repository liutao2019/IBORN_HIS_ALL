using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy
{
    public interface IDataChooseList
    {
        #region 代理
        

        /// <summary>
        /// 录入信息完成事件（函数指针定义）
        /// 目前外部实现：FS.SOC.HISFC.Components.Pharmacy.Common.Input.CommonInput
        /// </summary>
        Delegate.ChooseCompletedHander ChooseCompletedEvent
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 配置文件
        /// </summary>
        string SettingFileName
        {
            get;
            set;
        }

        #region 函数

        void Dispose();

        /// <summary>
        /// 清空数据
        /// </summary>
        void Clear();

        /// <summary>
        /// 初始化设置
        /// </summary>
        void Init();

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="title">TabPages[0]标题</param>
        /// <param name="tabPageText">供用户选择的数据集,这个数据集不需要区(药品)分类别</param>
        /// <param name="filter">过滤字符串</param>
        /// <returns>-1 发生错误</returns>
        int ShowChooseList(string tabPageText, System.Data.DataSet dataSet, string filter);

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="tabPageText">TabPages[0]标题</param>
        /// <param name="SQL">获取供用户选择的数据集的SQL</param>
        /// <param name="isFormatDrugType">是否Format药品类别</param>
        /// <param name="filter">过滤字符串</param>
        /// <returns></returns>
        int ShowChooseList(string tabPageText, string SQL, bool isFormatDrugType, string filter);

        /// <summary>
        /// 根据上次使用的SQL重新获取数据集，用于用户改变药品类别后或者需要刷新数据时
        /// </summary>
        /// <returns></returns>
        int ReShowChooseList();

        /// <summary>
        /// 设置显示格式(格式化FarPoint)
        /// </summary>
        /// <param name="cellTypes">列的单元格显示类型</param>
        /// <param name="columnLabels">列的标题</param>
        /// <param name="columnWiths">列的宽度</param>
        /// <returns>-1 发生错误</returns>
        int SetFormat(FarPoint.Win.Spread.CellType.BaseCellType[] cellTypes, string[] columnLabels, float[] columnWiths);

        /// <summary>
        /// 根据上次使用的CellType,Label,With集合重新设置FarPoint，用于用户改变药品类别后或者需要刷新数据时
        /// </summary>
        /// <returns></returns>
        int ReSetFormat();

        /// <summary>
        /// 获取选择的数据
        /// </summary>
        /// <param name="columns">需要获取数据的列索引</param>
        /// <returns></returns>
        string[] GetChooseData(int[] columns);

        /// <summary>
        /// 将焦点地位到过滤框
        /// </summary>
        void SetFocusToFilter();

        #endregion
    }
}
