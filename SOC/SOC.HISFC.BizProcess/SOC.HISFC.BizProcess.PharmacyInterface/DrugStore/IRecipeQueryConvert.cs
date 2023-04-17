using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    /// <summary>
    /// [功能描述: 门诊药房处方查询各种单号转换接口：便于简写录入]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
    public interface IRecipeQueryConvert
    {
        /// <summary>
        /// 处方号转换
        /// </summary>
        /// <param name="sampleRecipeNO">简写处方号</param>
        /// <returns>完整处方号</returns>
        string ConvertRecipeNO(string sampleRecipeNO);

        /// <summary>
        /// 发票号转换
        /// </summary>
        /// <param name="sampleInvoiceNO">简写发票号</param>
        /// <returns>完整发票号</returns>
        string ConvertInvoiceNO(string sampleInvoiceNO);

        /// <summary>
        /// 病例号转换
        /// </summary>
        /// <param name="sampleCardNO">简写病例号</param>
        /// <returns>完整病例号</returns>
        string ConvertCardNO(string sampleCardNO);

        /// <summary>
        /// 看诊号转换       
        /// </summary>
        /// <param name="sampleClinicNO">简写看诊号</param>
        /// <returns>完整看诊号</returns>
        string ConvertClinicNO(string sampleClinicNO);

        /// <summary>
        /// 将看诊号转换成当日流水号
        /// 东莞启用挂号时给病人一个当日流水号的概念
        /// 在此保留将看诊号转换成当日流水号的功能，建议在接口实现中缓存此号
        /// </summary>
        /// <param name="curDayNO"></param>
        /// <returns></returns>
        string ConvertToClinicNO(string curDayNO);

        /// <summary>
        /// 将当日流水号转换成看诊号
        /// 东莞启用挂号时给病人一个当日流水号的概念
        /// 在此保留将看诊号转换成当日流水号的功能，建议在接口实现中缓存此号
        /// </summary>
        /// <param name="curDayNO"></param>
        /// <returns></returns>
        string ConvertToCurDayNO(string clinicNO);

        ///// <summary>
        ///// 健康卡
        ///// </summary>
        ///// <param name="sampleMarkNO"></param>
        ///// <returns></returns>
        //string ConvertMarkNO(string sampleMarkNO);
    }
}
