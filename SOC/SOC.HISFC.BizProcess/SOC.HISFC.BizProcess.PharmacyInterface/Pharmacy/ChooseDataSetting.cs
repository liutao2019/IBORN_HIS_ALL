using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy
{
    /// <summary>
    /// [功能描述: 入出库等FarPoint选择数据的uc关键属性]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-3]<br></br>
    /// </summary>
    public class ChooseDataSetting
    {
        /// <summary>
        /// 是否使用系统默认值
        /// </summary>
        bool isDefault = false;

        /// <summary>
        /// 是否使用系统默认值
        /// </summary>
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        /// <summary>
        /// 列表TabPage名称
        /// </summary>
        private string listTile = "";

        /// <summary>
        /// 列表TabPage名称
        /// </summary>
        public string ListTile
        {
            get { return listTile; }
            set { listTile = value; }
        }

        /// <summary>
        /// 获取选择数据的SQL
        /// </summary>
        private string mySQL = "";

        /// <summary>
        /// 配置文件名称
        /// </summary>
        private string settingFileName = "";

        /// <summary>
        /// 配置文件名称
        /// </summary>
        public string SettingFileName
        {
            get { return settingFileName; }
            set { settingFileName = value; }
        }

        /// <summary>
        /// 获取选择数据的SQL
        /// </summary>
        public string SQL
        {
            get { return mySQL; }
            set { mySQL = value; }
        }

        /// <summary>
        /// FarPoint中获取选择row中的列，便于定位cell的值
        /// </summary>
        private int[] columnIndexs;

        /// <summary>
        /// FarPoint中获取选择row中的列，便于定位cell的值
        /// </summary>
        public int[] ColumnIndexs
        {
            get { return columnIndexs; }
            set { columnIndexs = value; }
        }

        /// <summary>
        /// FarPoint CellType
        /// </summary>
        private FarPoint.Win.Spread.CellType.BaseCellType[] cellTypes;

        /// <summary>
        /// FarPoint CellType
        /// </summary>
        public FarPoint.Win.Spread.CellType.BaseCellType[] CellTypes
        {
            get { return cellTypes; }
            set { cellTypes = value; }
        }

        /// <summary>
        /// FarPoint ColumnLabel
        /// </summary>
        private string[] columnLabels;

        /// <summary>
        /// FarPoint ColumnLabel
        /// </summary>
        public string[] ColumnLabels
        {
            get { return columnLabels; }
            set { columnLabels = value; }
        }

        /// <summary>
        /// FarPoint ColumnWith
        /// </summary>
        private float[] columnWiths;

        /// <summary>
        /// FarPoint ColumnWith
        /// </summary>
        public float[] ColumnWiths
        {
            get { return columnWiths; }
            set { columnWiths = value; }
        }

        /// <summary>
        /// 是否需要药品类别选择
        /// </summary>
        private bool isNeedDrugType = false;

        /// <summary>
        /// 是否需要药品类别选择
        /// </summary>
        public bool IsNeedDrugType
        {
            get { return isNeedDrugType; }
            set { isNeedDrugType = value; }
        }

        /// <summary>
        /// 过滤字符串
        /// </summary>
        private string filter = "";

        /// <summary>
        /// 过滤字符串
        /// </summary>
        public string Filter
        {
            get { return filter; }
            set { filter = value; }
        }
    }
}
