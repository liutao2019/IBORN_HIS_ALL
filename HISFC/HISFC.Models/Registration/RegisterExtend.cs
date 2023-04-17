using System;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Account;
using System.Collections.Generic;

namespace FS.HISFC.Models.Registration
{
    /// <summary>
    /// Register<br></br>
    /// [功能描述: 挂号扩展信息实体]<br></br>
    /// <summary>
    [Serializable]
    public class RegisterExtend : Patient
    {
        /// <summary>
        /// 
        /// </summary>
        public RegisterExtend()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            // 
        }

        #region 变量
        /// <summary>
        /// 预约挂号类型ID
        /// </summary>
        private string bookingTypeId = string.Empty;

        /// <summary>
        /// 预约挂号类型名称
        /// </summary>
        private string bookingTypeName = string.Empty;
        #endregion

        #region 属性
        /// <summary>
        /// 预约挂号类型ID
        /// </summary>
        public string BookingTypeId
        {
            get
            {
                return bookingTypeId;
            }
            set
            {
                bookingTypeId = value;
            }
        }

        /// <summary>
        /// 预约挂号类型名称
        /// </summary>
        public string BookingTypeName
        {
            get
            {
                return bookingTypeName;
            }
            set
            {
                bookingTypeName = value;
            }
        }
        #endregion

        #region 方法
        ///// <summary>
        /////  挂号的副本
        ///// </summary>
        ///// <returns></returns>
        public new RegisterExtend Clone()
        {
            RegisterExtend regExtend = base.Clone() as RegisterExtend;
            return regExtend;
        }
        #endregion
    }
}
