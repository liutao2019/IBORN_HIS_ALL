using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface
{
    public interface ITopQueryCondition
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        int Init();

        /// <summary>
        /// 增加控件
        /// </summary>
        /// <param name="list"></param>
        void AddControls(List<QueryControl> list);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="common"></param>
        /// <returns></returns>
        Object GetValue(QueryControl common);

        /// <summary>
        /// 获取文本值
        /// </summary>
        /// <param name="common"></param>
        /// <returns></returns>
        Object GetText(QueryControl common);

        /// <summary>
        /// 过滤事件
        /// </summary>
        event FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface.ICommonReportController.FilterHandler OnFilterHandler;
    }
}
