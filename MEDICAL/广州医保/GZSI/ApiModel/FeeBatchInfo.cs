using System;
using System.Collections.Generic;
using System.Text;

namespace GZSI.ApiModel
{
    /// <summary>
    /// 费用批次类
    /// </summary>
    public class FeeBatchInfo
    {
        /// <summary>
        /// 医院编码
        /// </summary>
        public string hospital_id = string.Empty;
        /// <summary>
        /// 就医登记号
        /// </summary>
        public string serial_no = string.Empty;
        /// <summary>
        /// 个人电脑号
        /// </summary>
        public string Indi_id = string.Empty;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name = string.Empty;
        /// <summary>
        /// 批次
        /// </summary>
        public string fee_batch = string.Empty;
        /// <summary>
        /// 批次费用
        /// </summary>
        public string sum_fee = string.Empty;

    }
}
