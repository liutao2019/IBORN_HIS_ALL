using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy
{
    /// <summary>
    /// [功能描述: 药库单据本地化接口]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-03]<br></br>
    /// 说明：
    /// 1、单据打印
    /// </summary>
    public interface IPharmacyBill
    {
        /// <summary>
        /// 打印单据
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3code">三级用户自定义权限</param>
        /// <param name="alPrintData">所有需要打印的数据</param>
        /// <returns>-1 打印错误</returns>
        int PrintBill(string class2Code, string class3code, ArrayList alPrintData);
       
    }
}
