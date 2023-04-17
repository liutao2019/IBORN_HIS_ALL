using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Assign.Interface.Components
{
    /// <summary>
    /// [功能描述: 门诊叫号中或到诊患者列表显示接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public interface ITriaggedPatient : ILifecycle, ILoadable, IClearable
    {
        /// <summary>
        /// 添加已分诊患者
        /// </summary>
        /// <param name="alPatient"></param>
        /// <returns></returns>
        int AddRange(ArrayList alPatient);

        /// <summary>
        /// 患者选择发生变化
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> SelectedItemChange
        {
            get;
            set;
        }

        /// <summary>
        /// 分诊状态
        /// </summary>
        FS.HISFC.Models.Nurse.EnuTriageStatus TriageStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 选择的患者
        /// </summary>
        FS.SOC.HISFC.Assign.Models.Assign SelectItem
        {
            get;
        }

        /// <summary>
        /// 第一个节点
        /// </summary>
        FS.SOC.HISFC.Assign.Models.Assign FirstItem
        {
            get;
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
        /// 删除节点
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        int Remove(FS.SOC.HISFC.Assign.Models.Assign assign);

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        int Add(FS.SOC.HISFC.Assign.Models.Assign assign);

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
    }
}
