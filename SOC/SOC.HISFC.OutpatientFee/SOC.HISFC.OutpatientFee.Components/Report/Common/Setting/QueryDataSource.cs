using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting
{
    [Serializable]
    public class QueryDataSource
    {
        private string name = string.Empty;
        /// <summary>
        /// 数据源名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string sql = string.Empty;
        /// <summary>
        /// 数据源sql
        /// </summary>
        public string Sql
        {
            get
            {
                return sql;
            }
            set
            {
                sql = value;
            }
        }

        private bool addMapRow = false;
        /// <summary>
        /// 是否将数据内容添加到字典中
        /// </summary>
        public bool AddMapRow
        {
            get
            {
                return addMapRow;
            }
            set
            {
                addMapRow = value;
            }
        }

        private bool addMapColumn = false;
        /// <summary>
        /// 是否将数据内容添加到字典中
        /// </summary>
        public bool AddMapColumn
        {
            get
            {
                return addMapColumn;
            }
            set
            {
                addMapColumn = value;
            }
        }

        private bool addMapData = false;
        /// <summary>
        /// 是否将数据内容添加到字典中
        /// </summary>
        public bool AddMapData
        {
            get
            {
                return addMapData;
            }
            set
            {
                addMapData = value;
            }
        }

        private bool addMapSourceData = false;
        /// <summary>
        /// 加入数据源字典
        /// </summary>
        public bool AddMapSourceData
        {
            get
            {
                return addMapSourceData;
            }
            set
            {
                addMapSourceData = value;
            }
        }

        private bool isCross = false;
        /// <summary>
        /// 是否交叉报表
        /// </summary>
        public bool IsCross
        {
            get
            {
                return isCross;
            }
            set
            {
                isCross = value;
            }
        }

        private Enum.EnumSqlType sqlType =  Enum.EnumSqlType.MainReportUsing;
        /// <summary>
        /// 是否明细使用的Sql
        /// </summary>
        public Enum.EnumSqlType SqlType
        {
            get
            {
                return sqlType;
            }
            set
            {
                sqlType = value;
            }
        }

        private string crossRows = string.Empty;
        private string crossColumns = string.Empty;
        private string crossValues = string.Empty;
        /// <summary>
        /// 交叉行
        /// </summary>
        public string CrossRows
        {
            get { return crossRows; }
            set { crossRows = value; }
        }
        /// <summary>
        /// 交叉列
        /// </summary>
        public string CrossColumns
        {
            get { return crossColumns; }
            set { crossColumns = value; }
        }
        /// <summary>
        /// 交叉值
        /// </summary>
        public string CrossValues
        {
            get { return crossValues; }
            set { crossValues = value; }
        }

        private string crossCombinColumns = string.Empty;
        /// <summary>
        /// 交叉合并列
        /// </summary>
        public string CrossCombinColumns
        {
            get
            {
                return crossCombinColumns;
            }
            set
            {
                crossCombinColumns = value;
            }
        }

        private string sumColumns = string.Empty;
        /// <summary>
        /// 交叉合计列
        /// </summary>
        public string SumColumns
        {
            get
            {
                return sumColumns;
            }
            set
            {
                sumColumns = value;
            }
        }

        private string crossGroupColumns = string.Empty;
        /// <summary>
        /// 分组列
        /// </summary>
        public string CrossGroupColumns
        {
            get
            {
                return crossGroupColumns;
            }
            set
            {
                crossGroupColumns = value;
            }
        }

        /// <summary>
        /// 行合计？
        /// </summary>
        private bool isSumRow = true;

        /// <summary>
        /// 行合计？
        /// </summary>
        public bool IsSumRow
        {
            get
            {
                return isSumRow;
            }
            set
            {
                isSumRow = value;
            }
        }

        private string sumRows = string.Empty;
        /// <summary>
        /// 行合计
        /// </summary>
        public string SumRows
        {
            get
            {
                return sumRows;
            }
            set
            {
                sumRows = value;
            }
        }

        #region 以下新加

        #region 内部成员

        /// <summary>
        /// 行分组
        /// </summary>
        private List<RowGroupInfo> rowGroup = new List<RowGroupInfo>();

        #endregion

        #region 外部访问

        /// <summary>
        /// 行分组
        /// </summary>
        public List<RowGroupInfo> RowGroup
        {
            get
            {
                return rowGroup;
            }
            set
            {
                rowGroup = value;
            }
        }
        #endregion

        /// <summary>
        /// 序列化使用
        /// </summary>
        /// <param name="mainObj"></param>
        /// <param name="genericValue"></param>
        /// <param name="p"></param>
        public void SetGenericTypeValue(Object mainObj, Object genericValue, System.Reflection.PropertyInfo p)
        {
            if (p.Name == "RowGroup" && genericValue != null)
            {
                List<RowGroupInfo> list = new List<RowGroupInfo>();
                foreach (RowGroupInfo step in (System.Collections.ICollection)genericValue)
                {
                    list.Add(step);
                }

                p.SetValue(mainObj, list, null);
            }
        
        }

        #endregion

    }
}
