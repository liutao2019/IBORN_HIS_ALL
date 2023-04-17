using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.Data
{
    /// <summary>
    /// [功能描述: 数据库类型特性类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class DataBaseAttribute : Attribute
    {
        private FS.FrameWork.Management.Connection.EnumDBType dbType = FS.FrameWork.Management.Connection.EnumDBType.ORACLE;
        public DataBaseAttribute(FS.FrameWork.Management.Connection.EnumDBType dbType)
            : base()
        {
            this.dbType = dbType;
        }

        public DataBaseAttribute()
            : base()
        {
        }

        public FS.FrameWork.Management.Connection.EnumDBType DbType
        {
            get
            {
                return this.dbType;
            }
        }
    }

}
