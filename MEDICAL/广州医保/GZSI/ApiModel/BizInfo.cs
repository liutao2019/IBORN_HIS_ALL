using System;
using System.Collections.Generic;
using System.Text;

namespace GZSI.ApiModel
{
    /// <summary>
    /// 门诊业务信息
    /// </summary>
    public class BizInfo
    {

        /// <summary>
        /// 就医登记号
        /// </summary>
        public string serial_no = string.Empty;
        /// <summary>
        /// 人员信息
        /// </summary>
        public PersonInfo personInfo = new PersonInfo();
        /// <summary>
        /// 医院编号
        /// </summary>
        public string hospital_id = string.Empty;
        /// <summary>
        /// 业务类型
        /// </summary>
        public string biz_type = string.Empty;
        /// <summary>
        /// 中心编码
        /// </summary>
        public string center_id = string.Empty;

        /// <summary>
        /// 医保卡号(25位)
        /// </summary>
        public string ic_no = string.Empty;


        /// <summary>
        /// 待遇类别
        /// </summary>
        public string treatment_type = string.Empty;


        /// <summary>
        /// 业务登记日期(格式：YYYY-MM-DD hh:mi:ss)
        /// </summary>
        public string reg_date = string.Empty;

        /// <summary>
        /// 发生日期(格式：YYYY-MM-DD hh:mi:ss)
        /// </summary>
        public string diagnose_date = String.Empty;
        /// <summary>
        /// 登记人工号
        /// </summary>
        public string reg_staff = string.Empty;
        /// <summary>
        /// 登记人
        /// </summary>
        public string reg_man = string.Empty;
        /// <summary>
        /// 登记标志
        /// </summary>
        public string reg_flag = string.Empty;
        /// <summary>
        /// 业务开始时间(日期格式YYYY-MM-DD)
        /// </summary>
        public string begin_date = string.Empty;
        /// <summary>
        /// 业务开始情况
        /// </summary>
        public string reg_info = string.Empty;
        /// <summary>
        /// 入院科室
        /// </summary>
        public string in_dept = string.Empty;
        /// <summary>
        /// 入院科室名称
        /// </summary>
        public string in_dept_name = string.Empty;
        /// <summary>
        /// 入院病区
        /// </summary>
        public string in_area = string.Empty;
        /// <summary>
        /// 入院病区名称
        /// </summary>
        public string in_area_name = string.Empty;
        /// <summary>
        /// 入院床位号
        /// </summary>
        public string in_bed = string.Empty;

        /// <summary>
        /// 医院业务号(挂号)
        /// </summary>
        public string patient_id = string.Empty;
        /// <summary>
        /// 入院疾病诊断（icd码）
        /// </summary>
        public string in_disease = string.Empty;
        /// <summary>
        /// 疾病名称
        /// </summary>
        public string disease = string.Empty;
        /// <summary>
        /// 用卡标志
        /// </summary>
        public string ic_flag = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string remark = string.Empty;


        /// <summary>
        /// 总费用
        /// </summary>
        public decimal total_fee = 0m;

        /// <summary>
        /// 费用批次
        /// </summary>
        public string fee_batch = string.Empty;

        /// <summary>
        /// 工伤凭证号（生育就医凭证号）
        /// </summary>
        public string injury_borth_sn = string.Empty;

        /// <summary>
        /// 门慢申请序号
        /// </summary>
        public string serial_apply = string.Empty;


    }
}
