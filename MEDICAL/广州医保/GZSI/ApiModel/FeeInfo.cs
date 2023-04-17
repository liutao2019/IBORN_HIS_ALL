using System;
using System.Collections.Generic;
using System.Text;

namespace GZSI.ApiModel
{
    /// <summary>
    /// 费用信息
    /// </summary>
    public  class FeeInfo
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
        /// 费用批次
        /// </summary>
        public string fee_batch = string.Empty;
        /// <summary>
        /// 费用序号
        /// </summary>
        public string serial_fee = string.Empty;
        /// <summary>
        /// 统计类别
        /// </summary>
        public string Stat_type = string.Empty;
        /// <summary>
        /// 项目药品类型
        /// </summary>
        public string medi_item_type = string.Empty;
        /// <summary>
        /// 中心药品项目编码
        /// </summary>
        public string Item_code = string.Empty;
        /// <summary>
        /// 中心药品项目名称
        /// </summary>
        public string Item_name = string.Empty;
        /// <summary>
        /// 医院药品项目编码
        /// </summary>
        public string his_item_code = string.Empty;
        /// <summary>
        /// 医院药品项目名称
        /// </summary>
        public string his_item_name = string.Empty;
        /// <summary>
        /// 费用发生日期
        /// </summary>
        public string fee_date = string.Empty;
        /// <summary>
        /// 剂型
        /// </summary>
        public string model = string.Empty;
        /// <summary>
        /// 厂商
        /// </summary>
        public string factory = string.Empty;
        /// <summary>
        /// 规格
        /// </summary>
        public string standard = string.Empty;
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit = string.Empty;
        /// <summary>
        /// 单价
        /// </summary>
        public string Price = string.Empty;

        /// <summary>
        /// 用量
        /// </summary>
        public string dosage = string.Empty;
        /// <summary>
        /// 金额
        /// </summary>
        public string Money = string.Empty;
        /// <summary>
        /// 可退金额
        /// </summary>
        public string reduce_money = string.Empty;
        /// <summary>
        /// 用药标志
        /// </summary>
        public string usage_flag = string.Empty;
        /// <summary>
        /// 对应费用序列号
        /// </summary>
        public string opp_serial_fee = string.Empty;
        /// <summary>
        /// 处方号
        /// </summary>
        public string recipe_no = string.Empty;

        /// <summary>
        /// 医生编号
        /// </summary>
        public string doctor_no = string.Empty;
        /// <summary>
        /// 医生名称
        /// </summary>
        public string doctor_name = string.Empty;
        /// <summary>
        /// 费用录入日期(格式：yyyy-mm-dd)
        /// </summary>
        public string input_date = string.Empty;
        /// <summary>
        /// 录入人工号
        /// </summary>
        public string input_staff = string.Empty;
        /// <summary>
        /// 录入人姓名
        /// </summary>
        public string input_name = string.Empty;

        /// <summary>
        /// 医院费用序列号(医院费用的唯一标识)
        /// </summary>
        public string hos_serial = string.Empty;

    }
}
