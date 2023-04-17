using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting
{
    /// <summary>
    /// 表分组信息
    /// </summary>
    public class TableGroupInfo
    {
        #region 内部成员

        /// <summary>
        /// 分组条件
        /// </summary>
        private string groupCondition = string.Empty;

        /// <summary>
        /// 分组时是否按照内容分页
        /// </summary>
        private bool groupByPage = true;

        /// <summary>
        /// 分组数据源
        /// </summary>
        private QueryDataSource datasource = new QueryDataSource();

        #endregion

        #region 外部访问

        /// <summary>
        /// 分组条件
        /// </summary>
        public string GroupCondition
        {
            get
            {
                return groupCondition;
            }
            set
            {
                groupCondition = value;
            }
        }

        /// <summary>
        /// 分组数据源
        /// </summary>
        public QueryDataSource QueryDataSource
        {
            get
            {
                return datasource;
            }
            set
            {
                datasource = value;
            }
        }

        /// <summary>
        /// 按页分组
        /// </summary>
        public bool GroupByPage
        {
            get
            {
                return groupByPage;
            }
            set
            {
                groupByPage = value;
            }
        }

        #endregion
    }
}
