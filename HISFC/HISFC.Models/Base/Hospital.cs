﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Base
{
    /// <summary>
    /// [功能描述: 医院信息实体]<br></br>
    /// [创 建 者: 周雪松]<br></br>
    /// [创建时间: 2006-12-19]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// 
    /// </summary>
    [System.Serializable]
    public class Hospital : FS.FrameWork.Models.NeuObject
    {

        /// <summary>
        /// 构造函数

        /// </summary>
        public Hospital()
        {
        }

        /// <summary>
        /// 医院Logo图片
        /// </summary>
        private byte[] hosLogoImage;

        /// <summary>
        /// 医院Logo图片
        /// </summary>
        public byte[] HosLogoImage
        {
            get
            {
                return hosLogoImage;
            }
            set
            {
                hosLogoImage = value;
            }
        }
    }
    
}
