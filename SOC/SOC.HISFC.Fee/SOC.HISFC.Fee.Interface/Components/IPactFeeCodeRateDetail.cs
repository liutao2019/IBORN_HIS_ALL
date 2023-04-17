using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.Interface.Components
{
    /// <summary>
    /// [功能描述: 合同单位与项目对照维护界面接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// 说明：
    /// </summary>
    public interface IPactFeeCodeRateDetail : IDataDetail, IDisposable
    {

        /// <summary>
        /// 添加合同单位信息
        /// </summary>
        /// <param name="pactInfo"></param>
        void AddItemRange(List<FS.HISFC.Models.Base.PactInfo> pactInfo);

        /// <summary>
        /// 添加项目套餐
        /// </summary>
        /// <param name="alItemGroupDetials"></param>
        /// <returns></returns>
        void AddItemRange(List<FS.HISFC.Models.Base.PactItemRate> pactInfoList);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <returns></returns>
        int DeleteItem();

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        int Save();

        /// <summary>
        /// 有效性验证
        /// </summary>
        /// <returns></returns>
        bool Valid();

        /// <summary>
        /// 项目选择变化
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<List<FS.HISFC.Models.Base.PactItemRate>> SelectedPactItemRateChange
        {
            get;
            set;
        }
    }
}
