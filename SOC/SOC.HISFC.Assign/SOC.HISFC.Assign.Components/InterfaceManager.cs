using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.Assign.Components.Common.Display;
using FS.SOC.HISFC.Assign.Components.Interface;
using FS.SOC.HISFC.Assign.Interface.Components;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Assign.Components
{
    /// <summary>
    /// [功能描述: 接口管理类]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class InterfaceManager
    {
        /// <summary>
        /// 获取待分诊患者信息接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Interface.Components.ITriaggingPatient GetITriaggingPatient()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Assign.Interface.Components.ITriaggingPatient>(typeof(InterfaceManager), new Common.Triage.ucTriaggingPatient());
        }

        /// <summary>
        /// 获取已分诊患者信息接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient GetITriageWaiting()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient>(typeof(InterfaceManager), new Common.Triage.ucTriaggedPatient());
        }

        /// <summary>
        /// 获取已进诊患者信息接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient GetITriageCalling()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient>(typeof(InterfaceManager), new Common.Triage.ucTriaggedPatient());
        }

        /// <summary>
        /// 获取已到诊患者信息接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient GetITriageArrive()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient>(typeof(InterfaceManager), new Common.Triage.ucTriaggedPatient());
        }

        /// <summary>
        /// 获取已看诊患者信息接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient GetITriageSee()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient>(typeof(InterfaceManager), new Common.Triage.ucTriaggedPatient());
        }

        /// <summary>
        /// 获取未看诊患者信息接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient GetITriageNoSee()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Assign.Interface.Components.ITriaggedPatient>(typeof(InterfaceManager), new Common.Triage.ucTriaggedPatient());
        }

        /// <summary>
        /// 获取患者信息接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Interface.Components.IPatientInfo GetITriagePatientInfo()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Assign.Interface.Components.IPatientInfo>(typeof(InterfaceManager), new Common.Patient.ucPatientInfo());
        }

        /// <summary>
        /// 获取分诊队列接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Interface.Components.ITriageQueue GetITriageQueue()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Assign.Interface.Components.ITriageQueue>(typeof(InterfaceManager), new Common.Triage.ucTriageQueue());
        }

        /// <summary>
        /// 获取分诊队列接口实现
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay GetIAssignDisplay()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay>(typeof(InterfaceManager), new DefualtDisplay());
        }

        /// <summary>
        /// 获取挂号信息显示接口
        /// </summary>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Interface.Components.IMaintenance<FS.HISFC.Models.Registration.Register> GetIRegPatient()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.Assign.Interface.Components.IMaintenance<FS.HISFC.Models.Registration.Register>>(typeof(InterfaceManager), new Common.Patient.frmRegPatientInfo());
        }

        /// <summary>
        /// 获取护士站列表的接口
        /// </summary>
        /// <returns></returns>
        public static IDataList GetINurseStations()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<IDataList>(typeof(InterfaceManager), new  Base.ucAssignNurse());
        }

        /// <summary>
        /// 获取护士站诊室列表的接口
        /// </summary>
        /// <returns></returns>
        public static IDataList GetINurseRoomsManager()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<IDataList>(typeof(InterfaceManager), new Maintenance.Room.ucRoomManager());
        }

        /// <summary>
        /// 获取护士站诊室的接口
        /// </summary>
        /// <returns></returns>
        public static IMaintenance<FS.SOC.HISFC.Assign.Models.Room> GetINurseRoom()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<IMaintenance<FS.SOC.HISFC.Assign.Models.Room>>(typeof(InterfaceManager), new Maintenance.Room.ucRoom());
        }

        /// <summary>
        /// 获取护士站诊室的接口
        /// </summary>
        /// <returns></returns>
        public static IMaintenance<KeyValuePair< FS.SOC.HISFC.Assign.Models.Queue,System.Windows.Forms.Day>> GetIQueue()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<IMaintenance<KeyValuePair< FS.SOC.HISFC.Assign.Models.Queue,Day>>>(typeof(InterfaceManager), new Maintenance.Queue.ucQueue());
        }

        /// <summary>
        /// 获取护士站诊台列表的接口
        /// </summary>
        /// <returns></returns>
        public static IDataList GetINurseConsolesManager()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<IDataList>(typeof(InterfaceManager), new Maintenance.Room.ucConsoleManager());
        }

        /// <summary>
        /// 获取护士站诊台的接口
        /// </summary>
        /// <returns></returns>
        public static IMaintenance<FS.HISFC.Models.Nurse.Seat> GetINurseConsole()
        {
            return ControllerFactroy.CreateFactory().CreateInferface<IMaintenance<FS.HISFC.Models.Nurse.Seat>>(typeof(InterfaceManager), new Maintenance.Room.ucConsole());
        }

        ///// <summary>
        ///// 获取护理评估接口实现
        ///// </summary>
        ///// <returns></returns>
        //public static FS.SOC.HISFC.BizProcess.EMRInterface.Interface.INurseCase GetINurseCase()
        //{
        //    return ControllerFactroy.CreateFactory().CreateInferface<FS.SOC.HISFC.BizProcess.EMRInterface.Interface.INurseCase>(typeof(InterfaceManager),new FS.SOC.HISFC.BizProcess.EMRInterface.Implement.INurseCaseImplement());
        //}
    }
}
