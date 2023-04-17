using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models.Outpatient
{
    /// <summary>
    /// 门诊单据时结算返回
    /// </summary>
    public class Detlcutinfo
    {
        public List<DetlcutinfoRows> rows { get; set; }
    }
    public class DetlcutinfoRows
    {
        /// <summary>
        /// 费用明细流水号  Y     
        /// <summary>
        public string feedetl_sn { get; set; }

        /// <summary>
        /// 明细项目费用总额  Y   
        /// <summary>
        public string det_item_fee_sumamt { get; set; }

        /// <summary>
        /// 数量  Y     
        /// <summary>
        public string cnt { get; set; }

        /// <summary>
        /// 单价  Y     
        /// <summary>
        public string pric { get; set; }

        /// <summary>
        /// 定价上限金额  Y     
        /// <summary>
        public string pric_uplmt_amt { get; set; }

        /// <summary>
        /// 自付比例         
        /// <summary>
        public string selfpay_prop { get; set; }

        /// <summary>
        /// 全自费金额         
        /// <summary>
        public string fulamt_ownpay_amt { get; set; }

        /// <summary>
        /// 超限价金额         
        /// <summary>
        public string overlmt_amt { get; set; }

        /// <summary>
        /// 先行自付金额         
        /// <summary>
        public string preselfpay_amt { get; set; }

        /// <summary>
        /// 符合政策范围金额         
        /// <summary>
        public string inscp_scp_amt { get; set; }

        /// <summary>
        /// 收费项目等级  Y     
        /// <summary>
        public string chrgitm_lv { get; set; }

        /// <summary>
        /// 医疗收费项目类别  Y     
        /// <summary>
        public string med_chrgitm_type { get; set; }

        /// <summary>
        /// 基本药物标志         
        /// <summary>
        public string bas_medn_flag { get; set; }

        /// <summary>
        /// 医保谈判药品标志         
        /// <summary>
        public string hi_nego_drug_flag { get; set; }

        /// <summary>
        /// 儿童用药标志         
        /// <summary>
        public string chld_medc_flag { get; set; }

        /// <summary>
        /// 目录特项标志      特检特治项目或特殊药品 
        /// <summary>
        public string list_sp_item_flag { get; set; }

        /// <summary>
        /// 直报标志       
        /// <summary>
        public string drt_reim_flag { get; set; }

        /// <summary>
        /// 备注      明细分割错误信息 
        /// <summary>
        public string memo { get; set; }
    }
}
