using System;
using System.Collections.Generic;
using System.Text;

namespace GZSI.ApiModel
{
    /// <summary>
    /// 个人基本信息
    /// </summary>
    public class PersonInfo
    {
        private string indi_id=string.Empty;
        /// <summary>
        /// 个人电脑号
        /// </summary>
        public string Api_indi_id
        {
            get
            {
                return indi_id;
            }
            set { indi_id = value; }
        }
        private string name = string.Empty;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Api_name
        {
            get
            {
                return name;
            }
            set { name = value; }
        }

        private string sex=string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        public string Api_sex
        {
            get
            {
                return sex;
            }
            set { sex = value; }
        }


        private string pers_identity=string.Empty;
        /// <summary>
        /// 人员类别
        /// </summary>
        public string Api_pers_identity
        {
            get
            {
                return pers_identity;
            }
            set { pers_identity = value; }
        }

        private string pers_status=string.Empty;
        /// <summary>
        /// 人员状态
        /// </summary>
        public string Api_pers_status
        {
            get
            {
                return pers_status;
            }
            set { pers_status = value; }
        }

        private string office_grade=string.Empty;
        /// <summary>
        /// 级别
        /// </summary>
        public string Api_office_grade
        {
            get
            {
                return office_grade;
            }
            set { office_grade = value; }
        }

        private string idcard=string.Empty;
        /// <summary>
        /// 公民身份号码
        /// </summary>
        public string Api_idcard
        {
            get
            {
                return idcard;
            }
            set { idcard = value; }
        }

        private string telephone=string.Empty;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Api_telephone
        {
            get
            {
                return telephone;
            }
            set { telephone = value; }
        }

        private string birthday=string.Empty;
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Api_birthday
        {
            get
            {
                return birthday;
            }
            set { birthday = value; }
        }

        private string post_code=string.Empty;
        /// <summary>
        /// 地区编码
        /// </summary>
        public string Api_post_code
        {
            get
            {
                return post_code;
            }
            set { post_code = value; }
        }


        private string corp_id=string.Empty;
        /// <summary>
        /// 单位编码
        /// </summary>
        public string Api_corp_id
        {
            get
            {
                return corp_id;
            }
            set { corp_id = value; }
        }

        private string corp_name=string.Empty;
        /// <summary>
        /// 单位名称
        /// </summary>
        public string Api_corp_name
        {
            get
            {
                return corp_name;
            }
            set { corp_name = value; }
        }

        private string last_balance = string.Empty;
        /// <summary>
        /// 个人帐户余额
        /// </summary>
        public string Api_last_balance
        {
            get
            {
                return last_balance;
            }
            set { last_balance = value; }
        }



        ///////////////////////////////////////////////
        //个人帐户冻结信息
        //////////////////////////////////////////////

        private string fund_id = string.Empty;
        /// <summary>
        /// 基金编号 max = 3
        /// </summary>
        public string Api_fund_id
        {
            get
            {
                return fund_id;
            }
            set { fund_id = value; }
        }

        private string fund_name = string.Empty;
        /// <summary>
        /// 基金名称 max = 30
        /// </summary>
        public string Api_fund_name
        {
            get
            {
                return fund_name;
            }
            set { fund_name = value; }
        }

        private string indi_freeze_status = string.Empty;
        /// <summary>
        /// 基金状态标志 max = 1
        /// </summary>
        public string Api_indi_freeze_status
        {
            get
            {
                //switch (indi_freeze_status)
                //{
                //    case "0":
                //        return "正常";
                //    case "1":
                //        return "冻结";
                //    case "2":
                //        return "暂停参保";
                //    case "3":
                //        return "中止参保";
                //    case "4":
                //        return "未参保";
                //    default:
                //        return "";
                //}
                return indi_freeze_status;
            }
            set 
            {
                indi_freeze_status = value; 
            }
        }


        //////////////////////
        //门诊选点信息clinicapplyinfo
        /////////////////////
        private string serial_apply = string.Empty;

        /// <summary>
        /// 门诊选点申请序列号(门慢申请序列号)
        /// </summary>
        public string Api_serial_apply
        {
            get { return serial_apply; }
            set
            {
                serial_apply = value;
            }
        }


        /////////////////////////////
        //生育认定信息injuryorbirthinfo（工伤认定信息）
        /////////////////////////////
        private string serial_mn = string.Empty;
        /// <summary>
        /// 生育医疗凭证号
        /// </summary>
        public string Api_serial_mn
        {
            get { return serial_mn; }
            set { serial_mn = value; }
        }

        private string serial_wi = string.Empty;
        /// <summary>
        /// 工伤就医凭证号(保存工伤门诊业务信息时，该值作为参数“injuryorbirth_serial”的值送入)
        /// </summary>
        public string Api_serial_wi
        {
            get { return serial_wi; }
            set { serial_wi = value; }
        }

        private string begin_date = string.Empty;
        /// <summary>
        /// 医疗期开始时间(格式：“yyyy-mm-dd”)
        /// </summary>
        public string Api_begin_date
        {
            get { return begin_date; }
            set { begin_date = value; }
        }

        private string end_date = string.Empty;
        /// <summary>
        /// 医疗期结束时间(格式：“yyyy-mm-dd”)
        /// </summary>
        public string Api_end_date
        {
            get { return end_date; }
            set { end_date = value; }
        }

        /////////////////////////////////////
        //门慢申请信息
        /////////////////////////////////////
        private string treatment_type = string.Empty;
        /// <summary>
        /// 待遇类型
        /// </summary>
        public string Api_treatment_type
        {
            get { return treatment_type; }
            set { treatment_type = value; }
        }


        private string treatment_name = string.Empty;
        /// <summary>
        /// 待遇类型名称
        /// </summary>
        public string Api_treatment_name
        {
            get { return treatment_name; }
            set { treatment_name = value; }
        }


        private string biz_type = string.Empty;
        /// <summary>
        /// 业务类型
        /// </summary>
        public string Api_biz_type
        {
            get { return biz_type; }
            set { biz_type = value; }
        }


        private string icd = string.Empty;
        /// <summary>
        /// 疾病编码
        /// </summary>
        public string Api_icd
        {
            get { return icd; }
            set { icd = value; }
        }

        private string disease = string.Empty;
        /// <summary>
        /// 疾病名称
        /// </summary>
        public string Api_disease
        {
            get { return disease; }
            set { disease = value; }
        }

        private string admit_effect = string.Empty;
        /// <summary>
        /// 申请生效时间
        /// </summary>
        public string Api_admit_effect
        {
            get { return admit_effect; }
            set { admit_effect = value; }
        }

        private string admit_date = string.Empty;
        /// <summary>
        /// 申请到期时间
        /// </summary>
        public string Api_admit_date
        {
            get { return admit_date; }
            set { admit_date = value; }
        }


    }
}
