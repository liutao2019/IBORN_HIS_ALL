using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.HISFC.Models.Account
{
    
    [Serializable]
    public class EnumOperTypesCoupon : FS.HISFC.Models.Base.EnumServiceBase
    {
        public EnumOperTypesCoupon() 
        {
            this.Items[CounponOperTypes.Pay] = "消费";
            this.Items[CounponOperTypes.Quit] = "退费";
            this.Items[CounponOperTypes.Exc] = "兑换";
        }

        #region 变量
        /// <summary>
        /// 操作类别
        /// </summary>
        CounponOperTypes operTypes;
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
                return operTypes; 
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
    /// 操作类型1消费2兑换
    /// </summary>
    public enum CounponOperTypes
    {

        /// <summary>
        /// 退费
        /// </summary>
        Quit = 1,
        /// <summary>
        /// 消费
        /// </summary>
        Pay=2,
        /// <summary>
        /// 兑换
        /// </summary>
        Exc=3
    };
    #endregion
}
