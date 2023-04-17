using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.CallQueue.Interface
{
    /// <summary>
    /// 接收叫号信息
    /// </summary>
    public interface INurseAssign
    {
        /// <summary>
        ///  门诊分诊接收叫号信息
        /// </summary>
        /// <param name="register">挂号实体</param>
        /// <param name="dept">看诊科室信息</param>
        /// <param name="room">看诊诊室</param>
        /// <param name="console">看诊诊台</param>
        /// <returns></returns>
        int Insert(FS.HISFC.Models.Registration.Register register, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err);

        /// <summary>
        /// 门诊分诊叫号
        /// </summary>
        /// <param name="nurseCode">护士站</param>
        /// <param name="noon">午别</param>
        void Call(string nurseCode, string noonID);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="doct"></param>
        /// <param name="dept"></param>
        /// <param name="nurse"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="noon"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        int Init(FS.FrameWork.Models.NeuObject doct, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err);

        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="doct"></param>
        /// <param name="dept"></param>
        /// <param name="nurse"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="noon"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        int Close(FS.FrameWork.Models.NeuObject doct, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err);

        /// <summary>
        /// 诊出
        /// </summary>
        /// <param name="register"></param>
        /// <param name="dept"></param>
        /// <param name="nurse"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="noon"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        int DiagOut(FS.HISFC.Models.Registration.Register register, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err);

        /// <summary>
        /// 过号呼叫
        /// </summary>
        /// <param name="register"></param>
        /// <param name="dept"></param>
        /// <param name="nurse"></param>
        /// <param name="room"></param>
        /// <param name="console"></param>
        /// <param name="noon"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        int DelayCall(FS.HISFC.Models.Registration.Register register, FS.FrameWork.Models.NeuObject dept, FS.FrameWork.Models.NeuObject nurse, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, FS.HISFC.Models.Base.Noon noon, ref string err);

        //{DF69D955-220E-462f-BB65-04EB39BB0AF5}
        /// <summary>
        /// 取消分诊
        /// </summary>
        /// <param name="clinic_code"></param>
        /// <returns></returns>
        int CancelCall(string clinic_code);

        /// <summary>
        /// 重新叫号
        /// </summary>
        /// <param name="clinic_code"></param>
        /// <returns></returns>
        int ReCall(string clinic_code);

    }
}
