using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Assign.Interface.Components
{
    /// <summary>
    /// [功能描述: 门诊分诊护士站列表接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public  interface IDataList:ILifecycle,ILoadable,IClearable
    {
        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="alNurseStation"></param>
        /// <returns></returns>
        int AddRange(ArrayList alInfo);

        /// <summary>
        /// 添加单条数据
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        int Add(Object o);

        /// <summary>
        /// 选择的节点
        /// </summary>
        Object SelectItem
        {
            get;
        }

        /// <summary>
        /// 第一个节点
        /// </summary>
        Object FirstItem
        {
            get;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        int Remove(Object info);

        /// <summary>
        /// 科室选择发生变化
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.FrameWork.Models.NeuObject,ArrayList> SelectedDeptChange
        {
            get;
            set;
        }

        /// <summary>
        /// 科室选择发生变化
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<Object> DoubleClickItem
        {
            get;
            set;
        }
    }
}
