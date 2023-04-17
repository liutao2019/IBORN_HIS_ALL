using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy
{
    public interface IInputInfoControl
    {
        /// <summary>
        /// 清空显示数据
        /// </summary>
        /// <returns></returns>
        int Clear();

        /// <summary>
        /// 清空信息
        /// </summary>
        /// <param name="isClearInvoiceInfo">是否清空发票信息</param>
        /// <param name="isClearDeliveryInfo">是否清空送货单号信息</param>
        /// <returns></returns>
        int Clear(bool isClearInvoiceInfo, bool isClearDeliveryInfo);

        /// <summary>
        /// 初始化
        /// 控件Enabled属性
        /// </summary>
        /// <returns></returns>
        int Init();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="unitShowState">单位显示方式</param>
        /// <returns></returns>
        int Init(string unitShowState);

        /// <summary>
        /// 设置焦点
        /// </summary>
        /// <returns></returns>
        int SetFocusToInputQty();

        /// <summary>
        /// 将入库数据显示为药品基本信息实体
        /// 用于药品列表选中药品后
        /// </summary>
        /// <param name="item">药品</param>
        /// <param name="storage">最新库存信息</param>
        /// <param name="isUseBatchNO">是否使用库存信息的批号</param>
        /// <param name="defaultValidDate">默认有效期</param>
        /// <returns>-1失败</returns>
        int SetInputObject(FS.HISFC.Models.Pharmacy.Item item, FS.HISFC.Models.Pharmacy.Storage storage, bool isUseBatchNO, DateTime defaultValidDate);

        /// <summary>
        /// 将入库数据显示为药品基本信息实体
        /// 用于药品列表选中药品后
        /// </summary>
        /// <param name="item">药品</param>
        /// <returns>-1失败</returns>
        int SetInputObject(FS.HISFC.Models.Pharmacy.Item item);

        /// <summary>
        /// 设置入库信息，双击明细数据FarPoint后修改入库信息调用
        /// </summary>
        /// <param name="input">入库实体</param>
        /// <param name="item">药品基本信息，重新查询数据后的基本信息</param>
        /// <returns>-1 失败</returns>
        int SetInputObject(FS.HISFC.Models.Pharmacy.Input input, FS.HISFC.Models.Pharmacy.Item item);

        /// <summary>
        /// 获取入库信息（实体）
        /// </summary>
        /// <returns></returns>
        FS.HISFC.Models.Pharmacy.Input GetInputObject();

        /// <summary>
        /// 数据录入完成
        /// </summary>
        Delegate.InputCompletedHander InputCompletedEven
        {
            get;
            set;
        }
    }
}
