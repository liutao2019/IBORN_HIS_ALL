using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OADeal
{
    public class FormInfo
    {
        /// <summary>
        /// 表单id
        /// </summary>
        public string FormID { set; get; }

        /// <summary>
        /// 审批流程类型
        /// </summary>
        public string FormType { set; get; }

        public string FormTypeID { set; get; }

        /// <summary>
        /// 所属院区
        /// </summary>
        public string HospitalID { set; get; }


    }
}
