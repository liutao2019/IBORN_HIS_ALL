using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Speciment
{
    public class Constant
    {
        public static int BarCodeLength = 10;
        /// <summary>
        /// 肿物类型
        /// </summary>
        public enum TumorType
        {
             肿物 =1,
             子瘤=2,
             癌旁=3,
             正常,
             癌栓=5,
             癌变=6,
             正常血,
             淋巴结 = 8
        }

        public enum TumorTypeCode
        {
            T=1,
            S=2,
            P=3,
            N,
            E=5,
            L=8
        }

        /// <summary>
        /// 肿物性质
        /// </summary>
        public enum TumorPro
        {
            原发癌=1,
            复发癌,
            转移癌,
            其它
        }

        /// <summary>
        /// 治疗阶段
        /// </summary>
        public enum GetPeriod
        {
            手术前=1,
            手术后,//2
            放疗前,//3
            放疗后,//4
            化疗前,//5
            化疗后,//6
            无,//7
            其它//8
        }
    }
}
