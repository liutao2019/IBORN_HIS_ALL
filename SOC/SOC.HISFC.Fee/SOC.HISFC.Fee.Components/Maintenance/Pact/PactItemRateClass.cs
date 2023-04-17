using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    public class PactItemRateClass
    {
        #region 构造函数

        public PactItemRateClass()
            : this(null)
        {
        }

        public PactItemRateClass(FS.HISFC.Models.Base.PactItemRate pactInfo)
        {
            this.pactInfo = pactInfo ?? new FS.HISFC.Models.Base.PactItemRate();
        }

        #endregion

        #region 变量

        private FS.HISFC.Models.Base.PactItemRate pactInfo = null;

        #endregion

        #region 属性

        #region 基本信息

        [CategoryAttribute("A基本信息"),
        DescriptionAttribute("合同单位的代码"),
        DefaultValueAttribute("")]
        [ReadOnly(true)]
        public string 代码
        {
            get
            {
                return this.pactInfo.ID;
            }
        }

        [CategoryAttribute("A基本信息"),
        DescriptionAttribute("合同单位的名称"),
        DefaultValueAttribute("")]
        [ReadOnly(true)]
        public string 名称
        {
            get
            {
                return this.pactInfo.Name;
            }
        }

        [CategoryAttribute("A基本信息"),
        DescriptionAttribute("项目的代码"),
        DefaultValueAttribute("")]
        [ReadOnly(true)]
        public string 项目代码
        {
            get
            {
                return this.pactInfo.PactItem.ID;
            }
        }

        [CategoryAttribute("A基本信息"),
        DescriptionAttribute("项目的名称"),
        DefaultValueAttribute("")]
        [ReadOnly(true)]
        public string 项目名称
        {
            get
            {
                return this.pactInfo.PactItem.Name;
            }
        }

        #endregion

        #region 费用相关

        [CategoryAttribute("B费用相关"),
        DescriptionAttribute("公费报销比例"),
        DefaultValueAttribute(0.0)]
        public decimal 公费比例
        {
            get
            {
                return this.pactInfo.Rate.PubRate;
            }
            set
            {
                this.pactInfo.Rate.PubRate = value;
            }
        }

        [CategoryAttribute("B费用相关"),
        DescriptionAttribute("自付报销比例"),
        DefaultValueAttribute(0.0)]
        public decimal 自付比例
        {
            get
            {
                return this.pactInfo.Rate.PayRate;
            }
            set
            {
                this.pactInfo.Rate.PayRate = value;
            }
        }

        [CategoryAttribute("B费用相关"),
        DescriptionAttribute("自费报销比例"),
        DefaultValueAttribute(1.0)]
        public decimal 自费比例
        {
            get
            {
                return this.pactInfo.Rate.OwnRate;
            }
            set
            {
                this.pactInfo.Rate.OwnRate = value;
            }
        }

        [CategoryAttribute("B费用相关"),
        DescriptionAttribute("优惠报销比例"),
        DefaultValueAttribute(0.0)]
        public decimal 优惠比例
        {
            get
            {
                return this.pactInfo.Rate.RebateRate;
            }
            set
            {
                this.pactInfo.Rate.RebateRate = value;
            }
        }

        [CategoryAttribute("B费用相关"),
        DescriptionAttribute("欠费报销比例"),
        DefaultValueAttribute(0.0)]
        public decimal 欠费比例
        {
            get
            {
                return this.pactInfo.Rate.ArrearageRate;
            }
            set
            {
                this.pactInfo.Rate.ArrearageRate = value;
            }
        }

        #endregion

        #region 限额相关

        [CategoryAttribute("C限额相关"),
        DescriptionAttribute("一次限额"),
        DefaultValueAttribute(0.0)]
        public decimal 一次限额
        {
            get
            {
                return this.pactInfo.Rate.Quota;
            }
            set
            {
                this.pactInfo.Rate.Quota = value;
            }
        }

        #endregion

        #endregion

        #region 方法

        public new FS.HISFC.Models.Base.PactItemRate ToString()
        {
            return this.pactInfo;
        }

        #endregion

    }

}
