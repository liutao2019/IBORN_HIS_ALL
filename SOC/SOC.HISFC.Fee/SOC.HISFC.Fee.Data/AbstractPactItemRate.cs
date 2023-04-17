using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Data
{
    /// <summary>
    /// [功能描述: 合同单位明细对照数据抽象类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractPactItemRate:AbstractSql<AbstractPactItemRate>
    {
        /// <summary>
        /// 根据项目分组获取所有项目的对照信息（所有的）
        /// </summary>
        public abstract String SelectItemGroup
        {
            get;
        }

        /// <summary>
        /// 根据项目分组获取所有项目的对照信息（合同单位过滤）
        /// </summary>
        public abstract String SelectItemGroupByPact
        {
            get;
        }

        /// <summary>
        /// 根据项目类型和项目Code查找对应的合同单位信息
        /// </summary>
        public abstract String SelectByItemTypeAndCode
        {
            get;
        }
    }
}
