using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.Models.Account
{
    
    [Serializable]
    public class EnumPayTypesService : FS.HISFC.Models.Base.EnumServiceBase
    {
        public EnumPayTypesService() 
        {
            this.Items[PayWayTypes.P] = "P";
            this.Items[PayWayTypes.R] = "R";
            this.Items[PayWayTypes.C] = "C";
            this.Items[PayWayTypes.I] = "I";
            this.Items[PayWayTypes.M] = "M";
            this.Items[PayWayTypes.Y] = "Y";
        }

        #region 变量
        /// <summary>
        /// 消费类型
        /// </summary>
        PayWayTypes payWayTypes;
        /// <summary>
        /// 存储枚举
        /// </summary>
        protected static Hashtable items = new Hashtable();
        #endregion

        #region 属性
        /// <summary>
        /// 存贮枚举
        /// </summary>
        protected override Hashtable Items
        {
            get 
            { 
                return items; 
            }
        }
        /// <summary>
        /// 枚举项目
        /// </summary>
        protected override System.Enum EnumItem
        {
            get 
            {
                return payWayTypes; 
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 得到枚举的NeuObject数组
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
        #endregion  

      
    }
    #region 操作类型枚举
    /// <summary>
    /// 消费类型：P购买套餐；R门诊挂号；C门诊消费；I住院结算；M套餐押金
    /// </summary>
    public enum PayWayTypes
    {
        /// <summary>
        /// 购买套餐
        /// </summary>
        P = 0,
        /// <summary>
        /// 门诊挂号
        /// </summary>
        R= 1,
        /// <summary>
        /// 门诊消费
        /// </summary>
        C=2,
        /// <summary>
        /// 住院结算
        /// </summary>
        I = 3,
        
        /// <summary>
        /// 套餐押金
        /// </summary>
        M = 4,

        /// <summary>
        /// 医美消费{1C42FA6C-C70A-4cd4-82C4-9FA1FCABD73B}
        /// </summary>
        Y = 5,

    };
    #endregion
}
