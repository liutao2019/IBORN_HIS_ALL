using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.PubReport.Models
{
    /// <summary>
    /// InvoiceTypeEnumService<br></br>
    /// [功能描述: 收据(发票)类型枚举服务类]<br></br>
    /// [创 建 者: 王宇]<br></br>
    /// [创建时间: 2006-09-01]<br></br>
    /// <修改记录
    ///		修改人='徐伟哲'
    ///		修改时间='2007-10-01'
    ///		修改目的='肿瘤医院本地化开发'
    ///		修改描述='增加一种新的发票类型:营养膳食发票'
    ///  />
    /// </summary>
    public class InvoiceTypeEnumService : FS.HISFC.Models.Base.EnumServiceBase
    {
        static InvoiceTypeEnumService()
        {
            items[EnumInvoiceType.R] = "挂号收据";
            items[EnumInvoiceType.C] = "门诊收据";
            items[EnumInvoiceType.I] = "住院收据";
            items[EnumInvoiceType.P] = "预交收据";
            items[EnumInvoiceType.A] = "门诊帐户";
            items[EnumInvoiceType.N] = "营养膳食发票";//新增加的发票类型
        }

        #region 变量

        /// <summary>
        /// 存贮枚举名称
        /// </summary>
        protected static Hashtable items = new Hashtable();

        /// <summary>
        /// 收据(发票)分类
        /// </summary>
        private EnumInvoiceType enumInvoiceType;

        #endregion

        #region 属性

        /// <summary>
        /// 存贮枚举名称
        /// </summary>
        protected override Hashtable Items
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// 收据(发票)分类
        /// </summary>
        protected override Enum EnumItem
        {
            get
            {
                return this.enumInvoiceType;
            }
        }

        #endregion

        #region 方法

        #region 克隆

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>当前对象的实例副本</returns>
        public new InvoiceTypeEnumService Clone()
        {
            return base.Clone() as InvoiceTypeEnumService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// 收据(发票)分类
    /// </summary>
    public enum EnumInvoiceType
    {
        /// <summary>
        /// 挂号收据
        /// </summary>
        R = 0,

        /// <summary>
        /// 门诊收据
        /// </summary>
        C = 1,

        /// <summary>
        /// 住院收据
        /// </summary>
        I = 2,

        /// <summary>
        /// 预交收据
        /// </summary>
        P = 4,

        /// <summary>
        /// 门诊帐户
        /// </summary>
        A = 5,

        /// <summary>
        /// 营养膳食发票(新增)
        /// </summary>
        N = 6
    }
}
