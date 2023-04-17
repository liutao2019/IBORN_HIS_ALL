using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Models
{
    /// <summary>
    /// [功能描述: 基础项目属性扩展实体]<br></br>
    /// [创 建 者: xiangf]<br></br>
    /// [创建时间: 2012-05]<br></br>
    /// </summary>
    [System.ComponentModel.DisplayName("基础项目属性扩展信息")]
    [Serializable]
    public class ComItemExtend : FS.FrameWork.Models.NeuObject
    {
        string item_code = string.Empty;
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ItemCode
        {
            get { return item_code; }
            set
            {
                item_code = value;
            }
        }
        string item_name = string.Empty;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName
        {
            get { return item_name; }
            set
            {
                item_name = value;
            }
        }
        string type_code = string.Empty;
        /// <summary>
        /// 项目类型 1：药品 2：非药品
        /// </summary>
        public string TypeCode
        {
            get { return type_code; }
            set
            {
                type_code = value;
            }
        }

        string item_grade = string.Empty;
        /// <summary>
        /// 甲乙类标志 1:甲类 2:乙类 3:丙类 
        /// </summary>
        public string ItemGrade
        {
            get
            {
                return item_grade;
            }
            set
            {
                item_grade = value;
            }
        }

        string province_flag = string.Empty;
        /// <summary>
        /// 省限制 0:不限制 1:限制
        /// </summary>
        public string ProvinceFlag
        {
            get
            {
                return province_flag;
            }
            set
            {
                province_flag = value;
            }
        }

        string city_flag = string.Empty;
        /// <summary>
        /// 市限制  0:不限制 1:限制
        /// </summary>
        public string CityFlag
        {
            get
            {
                return city_flag;
            }
            set
            {
                city_flag = value;
            }
        }

        string area_flag = string.Empty;
        /// <summary>
        /// 区限制  0:不限制 1:限制
        /// </summary>
        public string AreaFlag
        {
            get
            {
                return area_flag;
            }
            set
            {
                area_flag = value;
            }
        }

        string spepact_flag = string.Empty;
        /// <summary>
        /// 特约单位限制  0假 1真
        /// </summary>
        public string SpePactFlag
        {
            get
            {
                return spepact_flag;
            }
            set
            {
                spepact_flag = value;
            }
        }

        string zf_flag = string.Empty;
        /// <summary>
        /// 特约单位限制  0假 1真
        /// </summary>
        public string ZFFlag
        {
            get
            {
                return zf_flag;
            }
            set
            {
                zf_flag = value;
            }
        }

        string syn_flag = string.Empty;
        /// <summary>
        /// 医保对照同步药品甲乙类标记 0不同步 1同步
        /// </summary>
        public string SynFlag
        {
            get
            {
                return syn_flag;
            }
            set
            {
                syn_flag = value;
            }
        }

        string mlg_flag = string.Empty;
        /// <summary>
        /// 医保对照同步药品甲乙类标记 0不同步 1同步
        /// </summary>
        public string MlgFlag
        {
            get
            {
                return mlg_flag;
            }
            set
            {
                mlg_flag = value;
            }
        }

        string oper_code = string.Empty;
        /// <summary>
        /// 医保对照同步药品甲乙类标记 0不同步 1同步
        /// </summary>
        public string OperCode
        {
            get
            {
                return oper_code;
            }
            set
            {
                oper_code = value;
            }
        }

        string oper_date = string.Empty;
        /// <summary>
        /// 医保对照同步药品甲乙类标记 0不同步 1同步
        /// </summary>
        public string OperDate
        {
            get
            {
                return oper_date;
            }
            set
            {
                oper_date = value;
            }
        }

        string spell_code = string.Empty;
        /// <summary>
        /// 拼音码
        /// </summary>
        public string Spell_code
        {
            get { return spell_code; }
            set { spell_code = value; }
        }

        string wb_code = string.Empty;
        /// <summary>
        /// 五笔码
        /// </summary>
        public string Wb_code
        {
            get { return wb_code; }
            set { wb_code = value; }
        }

        string specs = string.Empty;
        /// <summary>
        /// 规格
        /// </summary>
        public string Specs
        {
            get { return specs; }
            set { specs = value; }
        }

        public new ComItemExtend Clone()
        {
            ComItemExtend u = base.Clone() as ComItemExtend;
            return u;
        }

    }
}
