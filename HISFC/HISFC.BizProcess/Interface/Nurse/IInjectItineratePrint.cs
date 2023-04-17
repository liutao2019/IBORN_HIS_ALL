﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizProcess.Interface.Nurse
{
    /// <summary>
    /// IInjectItineratePrint<br></br>
    /// <Font color='#FF1111'>[功能描述:门诊注射签名卡打印接口{637EDB0D-3F39-4fde-8686-F3CD87B64581}]</Font><br></br>
    /// [创 建 者: 耿晓雷]<br></br>
    /// [创建时间: 2010-7-29]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///		/>
    /// </summary>
    public interface IInjectItineratePrint
    {
        /// <summary>
        /// 是否补打
        /// </summary>
        bool IsReprint
        {
            get;
            set;
        }

        void Init(ArrayList alPrintData);
    }
}
