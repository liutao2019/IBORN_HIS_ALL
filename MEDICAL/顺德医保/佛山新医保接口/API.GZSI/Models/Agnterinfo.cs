using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Models
{
    /// <summary>
    /// 代办信息（公共）
    /// </summary>
    public class Agnterinfo
    {
        #region 代办人信息agnterinfo
        /// <summary>
        /// 代办人姓名
        /// </summary>
        public string agnter_name { get; set; }
        /// <summary>
        /// 代办人关系
        /// </summary>
        public string agnter_rlts { get; set; }
        /// <summary>
        /// 代办人证件类型
        /// </summary>
        public string agnter_cert_type { get; set; }
        /// <summary>
        /// 代办人证件号码
        /// </summary>
        public string agnter_certno { get; set; }
        /// <summary>
        /// 代办人联系电话
        /// </summary>
        public string agnter_tel { get; set; }
        /// <summary>
        /// 代办人联系地址
        /// </summary>
        public string agnter_addr { get; set; }
        /// <summary>
        /// 代办人照片
        /// </summary>
        public string agnter_photo { get; set; }
        #endregion
    }
}
