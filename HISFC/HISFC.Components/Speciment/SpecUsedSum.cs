using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Speciment
{
    public class SpecUsedSum
    {
        private int storeCount = 0;
        private int colCount = 0;
        private string dept = "";
        private DateTime dtStartTime = DateTime.Now;
        private DateTime dtEndTime = DateTime.Now;
        private int usedCount = 0;
        private string userName = "";
        private string disType = "";
        private string specType="";

        /// <summary>
        /// 病种类型
        /// </summary>
        public string DisType
        {
            get
            {
                return disType;
            }
            set
            {
                disType = value;
            }
        }

        /// <summary>
        /// 标本类型
        /// </summary>
        public string SpecType
        {
            get
            {
                return specType;
            }
            set
            {
                specType = value;
            }
        }

        /// <summary>
        /// 现有数量
        /// </summary>
        public int StoreCount
        {
            get
            {
                return storeCount;
            }
            set
            {
                storeCount = value;
            }
        }

        /// <summary>
        /// 收集的数量
        /// </summary>
        public int ColCount
        {
            get
            {
                return colCount;
            }
            set
            {
                colCount = value;
            }
        }

        /// <summary>
        /// 部门
        /// </summary>
        public string Dept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return dtStartTime;
            }
            set
            {
                dtStartTime = value;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return dtEndTime;
            }
            set
            {
                dtEndTime = value;
            }
        }

        /// <summary>
        /// 用去的数量
        /// </summary>
        public int UsedCount
        {
            get
            {
                return usedCount;
            }
            set
            {
                usedCount = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }
    }
}
