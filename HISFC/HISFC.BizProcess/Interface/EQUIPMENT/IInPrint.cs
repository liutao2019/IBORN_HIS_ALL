﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Equipment
{
    /// <summary>
    /// IInPrint<br></br>
    /// [功能描述: 设备入库单打印接口]<br></br>
    /// [创 建 者: 许超]<br></br>
    /// [创建时间: 2007-12-16]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public interface IInPrint
    {
        void SetPrintData(List<FS.HISFC.Models.Equipment.Input> inputList);

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <returns>>成功 1 失败 -1</returns>
        int PrintView();

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        int Print();
    }
}
