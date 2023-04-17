using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Assign.Interface.Components
{
    /// <summary>
    /// [功能描述: 门诊分诊队列列表显示接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public interface ITriageQueue : ILifecycle, ILoadable, IClearable
    {
        /// <summary>
        /// 添加队列信息
        /// </summary>
        /// <param name="alQueue"></param>
        /// <returns></returns>
        int AddQueue(ArrayList alQueue);

        /// <summary>
        /// 添加队列信息（人数）
        /// </summary>
        /// <param name="alQueue"></param>
        /// <returns></returns>
        int AddQueueNum(ArrayList alQueue);

        /// <summary>
        /// 添加队列
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        int Add(FS.SOC.HISFC.Assign.Models.Queue queue);

        /// <summary>
        /// 队列选择发生变化
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue> SelectedQueueChange
        {
            get;
            set;
        }

        /// <summary>
        /// 队列选择发生变化
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Queue> DoubleClickItem
        {
            get;
            set;
        }

        /// <summary>
        /// 选择的队列
        /// </summary>
        FS.SOC.HISFC.Assign.Models.Queue Queue
        {
            get;
            set;
        }

        /// <summary>
        /// 当拖拽结束后触发
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> DragDropAssign
        {
            get;
            set;
        }

        /// <summary>
        /// 当拖拽结束后触发
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> DragDropRegister
        {
            get;
            set;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        int Remove(FS.SOC.HISFC.Assign.Models.Queue queue);
    }
}
