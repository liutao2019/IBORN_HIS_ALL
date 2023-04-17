using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Interface
{
    public interface IMainReportForm
    {
        /// <summary>
        /// 初始化报表设置信息
        /// </summary>
        /// <param name="reportQueryInfo"></param>
        /// <returns></returns>
        int Init(ReportQueryInfo reportQueryInfo);

        /// <summary>
        /// DataWindow对象
        /// </summary>
        string DataWindowObject
        {
            get;
            set;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="objects"></param>
        int Retrieve(params object[] objects);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        int Retrieve(System.Data.DataTable dt);

        /// <summary>
        /// 根据变量名称代替字符串中的值
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        int Retrieve(Dictionary<String, Object> map);

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        int Export();

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        int Print(PrintInfo printInfo);

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns></returns>
        int PrintPreview(bool isPreview, PrintInfo printInfo);

        /// <summary>
        /// 打印所有数据
        /// </summary>
        /// <returns></returns>
        int PrintAll(PrintInfo printInfo);

        /// <summary>
        /// 过滤
        /// </summary>
        void OnFilter(string filter);

        /// <summary>
        /// 选择行事件
        /// </summary>
        event ICommonReportController.SelectRowHanlder OnSelectRowHandler;

        /// <summary>
        /// 选择单元格事件发生
        /// </summary>
        event ICommonReportController.SelectCellHanlder OnCellClickHandler;

        /// <summary>
        /// 选择单元格事件发生
        /// </summary>
        event ICommonReportController.SelectCellHanlder OnDoubleCellClickHandler;

        /// <summary>
        /// 获取鼠标选择数据
        /// </summary>
        /// <returns></returns>
        DataRow[] GetCellDataRow();

        /// <summary>
        /// 根据行和列名称获取相应的值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        string GetItemString(int row, string columnName);
    }
}
