using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Assign.Interface.Components
{
    /// <summary>
    /// [功能描述: 门诊待分诊或已分诊患者列表显示接口]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public interface ITriaggingPatient:ILifecycle,ILoadable,IClearable
    {
        /// <summary>
        /// 添加患者
        /// </summary>
        /// <param name="alPatient"></param>
        /// <returns></returns>
        int AddRange(ArrayList alPatient);

        /// <summary>
        /// 患者选择发生变化
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Registration.Register> SelectedItemChange
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
        FS.HISFC.Models.Registration.Register SelectItem
        {
            get;
        }

        /// <summary>
        /// 第一个节点
        /// </summary>
        FS.HISFC.Models.Registration.Register FirstItem
        {
            get;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        int Remove(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        int Add(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        /// 当拖拽结束后触发
        /// </summary>
        FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Assign> DragDrop
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
