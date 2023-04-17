using System;
using System.Collections.Generic;
using System.Text;

namespace GZSI.ApiModel
{
    public class PayInfo
    {
        /// <summary>
        /// 医院编号
        /// </summary>
        public string hospital_id = string.Empty;
        /// <summary>
        /// 就医登记号
        /// </summary>
        public string serial_no = string.Empty;
        /// <summary>
        /// 总金额
        /// </summary>
        public string zyzje = string.Empty;
        /// <summary>
        /// 社保支付金额
        /// </summary>
        public string sbzfje = string.Empty;
        /// <summary>
        /// 帐户支付金额
        /// </summary>
        public string zhzfje = string.Empty;
        /// <summary>
        /// 部分项目自付金额
        /// </summary>
        public string bfxmzfje = string.Empty;
        /// <summary>
        /// 个人起付金额
        /// </summary>
        public string qfje = string.Empty;
        /// <summary>
        /// 个人自费项目金额
        /// </summary>
        public string grzfje1 = string.Empty;
        /// <summary>
        /// 个人自付金额
        /// </summary>
        public string grzfje2 = string.Empty;
        /// <summary>
        /// 个人自负金额
        /// </summary>
        public string grzfje3 = string.Empty;
        /// <summary>
        /// 超统筹支付限额个人自付金额
        /// </summary>
        public string cxzfje = string.Empty;
        /// <summary>
        /// 医院垫付金额
        /// </summary>
        public string yyfdje = string.Empty;
        /// <summary>
        /// 个人自付现金部分
        /// </summary>
        public string cash_pay_com = string.Empty;
        /// <summary>
        /// 个人自付个人帐户部分
        /// </summary>
        public string acct_pay_com = string.Empty;
        /// <summary>
        /// 个人自费现金部分
        /// </summary>
        public string cash_pay_own = string.Empty;
        /// <summary>
        /// 个人自费个人帐户部分
        /// </summary>
        public string acct_pay_own = string.Empty;

        /// <summary>
        /// 收费序号
        /// </summary>
        public string pay_no = string.Empty;

        /// <summary>
        /// 收费日期
        /// </summary>
        public string pay_date = string.Empty;
 
    }
}
