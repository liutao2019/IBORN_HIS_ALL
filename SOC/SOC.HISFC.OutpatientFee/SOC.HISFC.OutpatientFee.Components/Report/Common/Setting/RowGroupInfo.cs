using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Enum;
using System.Drawing;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting
{
    /// <summary>
    /// 行分组信息
    /// </summary>
    public class RowGroupInfo
    {
        #region 内部成员

        /// <summary>
        /// 分组条件
        /// </summary>
        private string groupConditionStr = string.Empty;

        /// <summary>
        /// 是否使用表格线
        /// </summary>
        private bool isUseTableLine = false;

        /// <summary>
        /// 分组内容显示位置
        /// </summary>
        private EnumGroupShowLocation groupShowLocation = EnumGroupShowLocation.Header;

        /// <summary>
        /// 组显示依赖列
        /// </summary>
        private string groupDependColumn = string.Empty;

        /// <summary>
        /// 组字体
        /// </summary>
        private Font groupFont = new Font("宋体", 10.5F, FontStyle.Bold);

        /// <summary>
        /// 分组显示内容类型
        /// </summary>
        private EnumGroupShowInfoType showInfoType = EnumGroupShowInfoType.GroupColumn;
        /// <summary>
        /// 自定义显示内容
        /// </summary>
        private string customShowInfo = string.Empty;

        #endregion

        #region 外部访问

        /// <summary>
        /// 分组条件
        /// </summary>
        public string GroupConditionStr
        {
            get
            {
                return groupConditionStr;
            }
            set
            {
                groupConditionStr = value;
            }
        }


        /// <summary>
        /// 分组条件
        /// </summary>
        public string[] GroupCondition
        {
            get
            {
                return groupConditionStr.Split(new char[]{'|'}, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        /// 是否使用表格线
        /// </summary>
        public bool IsUseTableLine
        {
            get
            {
                return isUseTableLine;
            }
            set
            {
                isUseTableLine = value;
            }
        }

        /// <summary>
        /// 分组显示内容类型
        /// </summary>
        public EnumGroupShowInfoType ShowInfoType
        {
            get
            {
                return showInfoType;
            }
            set
            {
                showInfoType = value;
            }
        }

        /// <summary>
        /// 分组内容显示位置
        /// </summary>
        public EnumGroupShowLocation GroupShowLocation
        {
            get
            {
                return groupShowLocation;
            }
            set
            {
                groupShowLocation = value;
            }
        }

        /// <summary>
        /// 组显示依赖列
        /// </summary>
        public string GroupDependColumn
        {
            get
            {
                return groupDependColumn;
            }
            set
            {
                groupDependColumn = value;
            }
        }

        /// <summary>
        /// 组字体
        /// </summary>
        public Font GroupFont
        {
            get
            {
                return groupFont;
            }
            set
            {
                groupFont = value;
            }
        }

        /// <summary>
        /// 自定义显示内容
        /// </summary>
        public string CustomShowInfo
        {
            get
            {
                return customShowInfo;
            }
            set
            {
                customShowInfo = value;
            }
        }

        #endregion
    }
}
