using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy
{
    /// <summary>
    /// [功能描述: 药库业务扩展接口]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-03]<br></br>
    /// 说明：
    /// 1、扩展提供在入库保存成后处理的业务，如：采购对比，货位号更新等
    /// </summary>
    public interface IPharmacyBizExtend
    {
        /// <summary>
        /// 保存后需要扩展的本地化业务
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3code">三级用户自定义权限</param>
        /// <param name="alData">业务数据</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>-1 处理失败</returns>
        int AfterSave(string class2Code, string class3code, ArrayList alData, ref string errInfo);

        /// <summary>
        /// 获取金额小数位数
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3MeaningCode">三级用户自定义权限</param>
        /// <param name="type">0零售 1 购入 2批发</param>
        /// <returns></returns>
        uint GetCostDecimals(string class2Code, string class3MeaningCode, string type);
        
        /// <summary>
        /// 获取和业务相关的数据选择列表属性集合
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3MeaningCode">三级系统权限</param>
        /// <param name="class3Code">三级用户自定义权限</param>
        /// <param name="listType">单据类型 0药品清单 1入库单 2出库单 3申请单 4采购单</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>null 发生错误</returns>
        ChooseDataSetting GetChooseDataSetting(string class2Code, string class3MeaningCode, string class3Code, string listType, ref string errInfo);

        /// <summary>
        /// 创建本地化要求的内部入库申请表
        /// </summary>
        /// <param name="applyDeptNO">申请科室</param>
        /// <param name="stockDeptNO">库存科室</param>
        /// <param name="alData">原数据列表</param>
        /// <returns></returns>
        ArrayList SetInnerInputApply(string applyDeptNO, string stockDeptNO, ArrayList alData);

        /// <summary>
        /// 创建本地化要求的入库申请表
        /// </summary>
        /// <param name="stockDeptNO">库存科室</param>
        /// <param name="alData">原数据列表</param>
        /// <returns></returns>
        ArrayList SetInputPlan(string stockDeptNO, ArrayList alData);

        /// <summary>
        /// 创建本地化要求的盘点表
        /// </summary>
        /// <param name="stockDeptNO">库存科室</param>
        /// <returns></returns>
        ArrayList SetCheckDetail(string stockDeptNO);

        /// <summary>
        /// 获取本地化规则的单单号
        /// </summary>
        /// <param name="stockDeptNO">库存科室</param>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3code">三级用户自定义权限</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>"-1"表示获取单据失败</returns>
        string GetBillNO(string stockDeptNO, string class2Code, string class3code, ref string errInfo);

        /// <summary>
        /// 获取入库信息录入控件
        /// </summary>
        /// <param name="class3code">三级自定义权限</param>
        /// <param name="isSpecial">是否特殊入库</param>
        /// <returns></returns>
        IInputInfoControl GetInputInfoControl(string class3code,bool isSpecial,IInputInfoControl defaultInputInfoControl);

        /// <summary>
        /// 获取药品基本信息扩展信息维护的本地化实现控件
        /// </summary>
        /// <returns></returns>
        IItemExtendControl GetItemExtendControl(IItemExtendControl defaultItemExtendControl);
    }
}
