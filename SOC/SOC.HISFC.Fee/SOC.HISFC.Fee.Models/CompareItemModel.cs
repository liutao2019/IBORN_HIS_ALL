using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.Models
{
    /// <summary>
    /// [功能描述: 医保对照实体类]<br></br>
    /// [创 建 者: ma_d]<br></br>
    /// [创建时间: 2012-4]<br></br>
    /// </summary>
    [System.ComponentModel.DisplayName("医保对照信息")]
    [Serializable]
    public class CompareItemModel:FS.HISFC.Models.Base.Spell
    {
        string pact_code = string.Empty;
        /// <summary>
        /// 医保合同单位编号
        /// </summary>
        public string PactCode
        {
            get { return pact_code; }
            set 
            {
                pact_code = value;
            }
        }
        string pact_name = string.Empty;
        public string PactName
        {
            get { return pact_name; }
            set
            {
                pact_name = value;
            }
        }

        string his_code = string.Empty;
        /// <summary>
        /// HIS系统项目编号
        /// </summary>
        public string HisCode
        {
            get { return his_code; }
            set
            {
                his_code = value;
            }
        }

        string his_name = string.Empty;
        /// <summary>
        /// HIS系统项目名称
        /// </summary>
        public string HisName
        {
            get { return his_name; }
            set
            {
                his_name = value;
            }
        }

        string his_user_code = string.Empty;
        /// <summary>
        /// HIS系统项目自定义码
        /// </summary>
        public string HisUserCode
        {
            get { return his_user_code; }
            set
            {
                his_user_code = value;
            }
        }


        string center_code = string.Empty;
        /// <summary>
        /// 医保中心项目编码
        /// </summary>
        public string CenterCode
        {
            get { return center_code; }
            set
            {
                center_code = value;
            }
        }

        string center_name = string.Empty;
        /// <summary>
        /// 医保中心项目名称
        /// </summary>
        public string CenterName
        {
            get { return center_name; }
            set
            {
                center_name = value;
            }
        }

        string center_item_type = string.Empty;
        /// <summary>
        /// 医保中心项目类型
        /// </summary>
        public string CenterItemType
        {
            get 
            { 
                return center_item_type; 
            }
            set
            {
                center_item_type = value;
                //switch (value)
                //{
                //    case "P":
                //        center_item_type = "X";
                //        break;
                //    case "Z":
                //        center_item_type = "Z";
                //        break;
                //    case "C":
                //        center_item_type = "C";
                //        break;
                //    default:
                //        center_item_type = "L";
                //        break;
                //}
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


        public new CompareItemModel Clone()
        {
            CompareItemModel u = base.Clone() as CompareItemModel;
            return u;
        }

    }
}
