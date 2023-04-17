using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    /// <summary>
    /// [功能描述: 住院药房发药接口]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 功能说明：
    /// 1、实现本地化的打印
    /// 2、实现本地化发药的补充程序
    /// </summary>
    public interface IInpatientDrug
    {
        /// <summary>
        /// 住院药房发药保存打印调用
        /// 基于自动打印处方、摆药单、标签、药袋或者是其中几种一起打印的都在此函数中实现
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugMessage">摆药通知</param>
        /// <param name="billNO">摆药单号</param>
        /// <param name="stockDept">实发科室</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        int OnSavePrint(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, string billNO, FS.FrameWork.Models.NeuObject stockDept);

        /// <summary>
        /// 摆药单格式打印，用于汇总和明细单据格式显示
        /// 能够展示界面，所以返回了控件
        /// 一般在手工打印或补打时调用
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">实发科室</param>
        /// <returns>显示单据的控件</returns>
        List<IInpatientBill> ShowDrugBill(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept);

        /// <summary>
        /// 标签打印
        /// 一般在手工打印或补打时调用
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">实发科室</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        int PrintDrugLabel(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept);

        /// <summary>
        /// 药袋打印
        /// 一般在手工打印或补打时调用
        /// </summary>
        /// <param name="alData">applyout出库申请实体数组</param>
        /// <param name="drugBillClass">摆药单分类</param>
        /// <param name="stockDept">实发科室</param>
        /// <returns>-1发生错误 0没有处理，1处理成功</returns>
        int PrintDrugBag(System.Collections.ArrayList alData, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass, FS.FrameWork.Models.NeuObject stockDept);
    
    }
}
