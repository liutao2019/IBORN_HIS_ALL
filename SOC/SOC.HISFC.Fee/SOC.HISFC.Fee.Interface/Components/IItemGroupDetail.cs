using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Interface.Components
{
    /// <summary>
    /// [功能描述: 物价组套基本信息查询界面接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// 说明：
    /// </summary>
    public interface IItemGroupDetail:IDataDetail,IDisposable
    {
        /// <summary>
        /// 添加项目套餐
        /// </summary>
        /// <param name="alItemGroupDetials"></param>
        /// <returns></returns>
        int AddItemGroup(FS.SOC.HISFC.Fee.Models.Undrug itemGroup);

        /// <summary>
        /// 添加项目明细
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int AddItem(FS.SOC.HISFC.Fee.Models.Undrug item);

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
    }
}
