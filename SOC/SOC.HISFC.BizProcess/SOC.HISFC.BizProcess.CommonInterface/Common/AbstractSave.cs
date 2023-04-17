using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.CommonInterface.Common
{
    /// <summary>
    /// [功能描述: 保存信息后调用接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public abstract class AbstractSave<T> : ISave<T>
    {
        #region ISave<T> 成员

        public virtual int Saved(EnumSaveType saveType, T t)
        {
            return 1;
        }

        public virtual int SaveCommitting(EnumSaveType saveType, T t)
        {
            return 1;
        }

        public virtual int Saving(EnumSaveType saveType, T t)
        {
            return 1;
        }

        #endregion

        #region IErr 成员
        protected string errInfo = string.Empty;
        public string Err
        {
            get
            {
                return this.errInfo;
            }
            set
            {
                this.errInfo = value;
            }
        }

        #endregion
    }
}
