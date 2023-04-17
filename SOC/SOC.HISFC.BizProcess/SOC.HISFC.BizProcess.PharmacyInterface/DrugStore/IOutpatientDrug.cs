using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    /// <summary>
    /// [功能描述: 门诊药房配发药接口]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 功能说明：
    /// 1、实现本地化的打印
    /// 2、实现本地化配发药的补充程序
    /// </summary>
    public interface IOutpatientDrug
    {
        /// <summary>
        /// 门诊药房配发药自动打印调用
        /// 基于自动打印处方、配药清单、标签或者是其中几种一起打印的都在此函数中实现
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="type">类型0直接发药 1配药 2发药</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        int OnAutoPrint(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal);

        /// <summary>
        /// 门诊药房配发药处方格式打印
        /// 一般在手工打印或补打时调用
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        int PrintRecipe(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal);

        /// <summary>
        /// 门诊药房配发药清单格式打印
        /// 一般在手工打印或补打时调用
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns></returns>
        int PrintDrugBill(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal);

        /// <summary>
        /// 门诊药房配发药标签打印
        /// 一般在手工打印或补打时调用
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal);


        /// <summary>
        /// 门诊药房配发药药袋打印
        /// 一般在手工打印或补打时调用
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        int PrintDrugBag(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal);

        /// <summary>
        /// 门诊药房配发药保存完成后调用
        /// 基于自动打印处方、配药清单、标签或者是其中几种一起打印
        /// 保存后需要处理其他信息的都在此函数中实现
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugRecipe">处方调剂信息</param>
        /// <param name="type">类型0直接发药 1配药 2发药</param>
        /// <param name="drugTerminal">当前终端信息</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        int AfterSave(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, string type, FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal);
    }
}
