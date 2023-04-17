using System;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FS.FrameWork.Public;

namespace FS.HISFC.BizLogic.Privilege.Model
{

    [Serializable]
    public enum OrganizationType
    {
        //isDepTrue代表可以添加相同的科室，isDepFalse代表不可以添加相同的科室

         /// <summary>
        /// clinic门诊
        /// </summary>
        [Description("门诊|isDepTrue")]
        C = 1,
        /// <summary>
        /// Inhospital住院
        /// </summary>
        [Description("住院|isDepFalse")]
        I = 2,
    
    }

}
