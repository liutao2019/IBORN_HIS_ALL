using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.Models.Order
{
    public class OrderST
    {

        //{23F37636-DC34-44a3-A13B-071376265450}
        /// <summary>
        /// 院区id
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// 院区名
        /// </summary>
        private string hospital_name;

        /// <summary>
        ///院区id //{23F37636-DC34-44a3-A13B-071376265450}
        /// </summary>
        public string Hospital_id
        {
            get
            {
                return this.hospital_id;
            }
            set
            {
                this.hospital_id = value;
            }
        }

        /// <summary>
        /// 院区名
        /// </summary>
        public string Hospital_name
        {
            get
            {
                return this.hospital_name;
            }
            set
            {
                this.hospital_name = value;
            }
        }

        /// <summary>
        /// 门诊/住院流水号
        /// </summary>
        private string clinic_no = "";
        /// <summary>
        /// 门诊/住院流水号
        /// </summary>
        public string Clinic_no
        {
            get { return clinic_no; }
            set { clinic_no = value; }
        }
        /// <summary>
        /// 是否打印
        /// </summary>
        bool is_prine = false;
        /// <summary>
        /// 是否打印
        /// </summary>
        public bool Is_prine
        {
            get { return is_prine; }
            set { is_prine = value; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        private string name = "";
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// O门诊/ I住院
        /// </summary>
        private string inouttype = "";
        /// <summary>
        /// O门诊/ I住院
        /// </summary>
        public string Inouttype
        {
            get { return inouttype; }
            set { inouttype = value; }
        }
        /// <summary>
        /// 门诊号/住院号
        /// </summary>
        private string card_no = "";
        /// <summary>
        /// 门诊号/住院号
        /// </summary>
        public string Card_no
        {
            get { return card_no; }
            set { card_no = value; }
        }
        /// <summary>
        /// 处方号(住院为mo_order ,门诊为SEQUENCE_NO)
        /// </summary>
        private string recipe_no = "";
        /// <summary>
        /// 处方号(住院为mo_order ,门诊为SEQUENCE_NO)
        /// </summary>
        public string Recipe_no
        {
            get { return recipe_no; }
            set { recipe_no = value; }
        }
        /// <summary>
        /// 项目编码
        /// </summary>
        private string item_code = "";
        /// <summary>
        /// 项目编码
        /// </summary>
        public string Item_code
        {
            get { return item_code; }
            set { item_code = value; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        private string item_name = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Item_name
        {
            get { return item_name; }
            set { item_name = value; }
        }
        /// <summary>
        /// 用法编码
        /// </summary>
        private string usage_code = "";
        /// <summary>
        /// 用法编码
        /// </summary>
        public string Usage_code
        {
            get { return usage_code; }
            set { usage_code = value; }
        }
        /// <summary>
        /// 用法名称
        /// </summary>
        private string usage_name = "";
        /// <summary>
        /// 用法名称
        /// </summary>
        public string Usage_name
        {
            get { return usage_name; }
            set { usage_name = value; }
        }
        /// <summary>
        /// 用量
        /// </summary>
        private decimal once_dose = 0;
        /// <summary>
        /// 用量
        /// </summary>
        public decimal Once_dose
        {
            get { return once_dose; }
            set { once_dose = value; }
        }
        /// <summary>
        /// 用量单位
        /// </summary>
        private string dose_unit = "";
        /// <summary>
        /// 用量单位
        /// </summary>
        public string Dose_unit
        {
            get { return dose_unit; }
            set { dose_unit = value; }
        }
        /// <summary>
        /// 频次编码
        /// </summary>
        private string fre_code = "";
        /// <summary>
        /// 频次编码
        /// </summary>
        public string Fre_code
        {
            get { return fre_code; }
            set { fre_code = value; }
        }
        /// <summary>
        /// 频次名称
        /// </summary>
        private string fre_name = "";

        /// <summary>
        /// 频次名称
        /// </summary>
        public string Fre_name
        {
            get { return fre_name; }
            set { fre_name = value; }
        }
        /// <summary>
        /// 天数
        /// </summary>
        private decimal days = 0;
        /// <summary>
        /// 天数
        /// </summary>
        public decimal Days
        {
            get { return days; }
            set { days = value; }
        }
        /// <summary>
        /// 开立医生编码
        /// </summary>
        private string recipe_doc_code = "";
        /// <summary>
        /// 开立医生编码
        /// </summary>
        public string Recipe_doc_code
        {
            get { return recipe_doc_code; }
            set { recipe_doc_code = value; }
        }
        /// <summary>
        /// 开立医生名称
        /// </summary>
        private string recipe_doc_name = "";

        /// <summary>
        /// 开立医生名称
        /// </summary>
        public string Recipe_doc_name
        {
            get { return recipe_doc_name; }
            set { recipe_doc_name = value; }
        }
        /// <summary>
        /// 开立科室编码
        /// </summary>
        private string recipe_dept_code = "";
        /// <summary>
        /// 开立科室编码
        /// </summary>
        public string Recipe_dept_code
        {
            get { return recipe_dept_code; }
            set { recipe_dept_code = value; }
        }
        /// <summary>
        /// 开立科室名称
        /// </summary>
        private string recipe_dept_name = "";
        /// <summary>
        /// 开立科室名称
        /// </summary>
        public string Recipe_dept_name
        {
            get { return recipe_dept_name; }
            set { recipe_dept_name = value; }
        }
        /// <summary>
        /// 丢弃量
        /// </summary>
        private decimal discarded_dose = 0;
        /// <summary>
        /// 丢弃量
        /// </summary>
        public decimal Discarded_dose
        {
            get { return discarded_dose; }
            set { discarded_dose = value; }
        }
        /// <summary>
        /// 审核者编码
        /// </summary>
        private string audit_doc_code = "";
        /// <summary>
        /// 审核者编码
        /// </summary>
        public string Audit_doc_code
        {
            get { return audit_doc_code; }
            set { audit_doc_code = value; }
        }
        /// <summary>
        /// 审核者名称
        /// </summary>
        private string audit_doc_name = "";
        /// <summary>
        /// 审核者名称
        /// </summary>
        public string Audit_doc_name
        {
            get { return audit_doc_name; }
            set { audit_doc_name = value; }
        }
        /// <summary>
        /// 执行者编码
        /// </summary>
        private string exec_doc_code = "";
        /// <summary>
        /// 执行者编码
        /// </summary>
        public string Exec_doc_code
        {
            get { return exec_doc_code; }
            set { exec_doc_code = value; }
        }
        /// <summary>
        /// 执行者名称
        /// </summary>
        private string exec_doc_name = "";
        /// <summary>
        /// 执行者名称
        /// </summary>
        public string Exec_doc_name
        {
            get { return exec_doc_name; }
            set { exec_doc_name = value; }
        }
        /// <summary>
        /// 执行时间
        /// </summary>
        private DateTime exec_date = new DateTime();
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime Exec_date
        {
            get { return exec_date; }
            set { exec_date = value; }
        }
        /// <summary>
        /// 组合号
        /// </summary>
        private string comb_no = "";
        /// <summary>
        /// 组合号
        /// </summary>
        public string Comb_no
        {
            get { return comb_no; }
            set { comb_no = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        private string memo = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
        /// <summary>
        /// 扩展字段
        /// </summary>
        private string ext_memo = "";
        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Ext_memo
        {
            get { return ext_memo; }
            set { ext_memo = value; }
        }
        /// <summary>
        /// 扩展字段1
        /// </summary>
        private string ext_memo1 = "";
        /// <summary>
        /// 扩展字段1
        /// </summary>
        public string Ext_memo1
        {
            get { return ext_memo1; }
            set { ext_memo1 = value; }
        }
        /// <summary>
        /// 扩展字段2
        /// </summary>
        private string ext_memo2 = "";
        /// <summary>
        /// 扩展字段2
        /// </summary>
        public string Ext_memo2
        {
            get { return ext_memo2; }
            set { ext_memo2 = value; }
        }
        /// <summary>
        /// 看诊序号（门诊用）
        /// </summary>
        private string see_no = "";
        /// <summary>
        /// 看诊序号（门诊用）
        /// </summary>
        public string See_no
        {
            get { return see_no; }
            set { see_no = value; }
        }

        /// <summary>
        /// 是否已经导入（数据判断用）
        /// </summary>
        private bool isInput = false;
        /// <summary>
        /// 是否已经导入（数据判断用）
        /// </summary>
        public bool IsInput
        {
            get { return isInput; }
            set { isInput = value; }
        }
    }
}
