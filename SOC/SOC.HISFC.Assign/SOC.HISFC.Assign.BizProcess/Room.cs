using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Assign.BizProcess
{
    /// <summary>
    /// [功能描述: SOC诊室综合类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class Room : FS.SOC.HISFC.BizProcess.CommonInterface.Common.AbstractBizProcess
    {
        /// <summary>
        /// 保存诊室
        /// </summary>
        /// <param name="room"></param>
        /// <param name="saveType"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int SaveRoom(FS.SOC.HISFC.Assign.Models.Room room, FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType saveType, ref string error)
        {
            if (room == null)
            {
                error = "诊室为空！";
                return -1;
            }

            this.BeginTransaction();
            FS.SOC.HISFC.Assign.BizLogic.Room roomMgr = new FS.SOC.HISFC.Assign.BizLogic.Room();
            FS.SOC.HISFC.Assign.BizLogic.Console consoleMgr = new FS.SOC.HISFC.Assign.BizLogic.Console();
            consoleMgr.SetTrans(this.Trans);
            roomMgr.SetTrans(this.Trans);
            if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert)
            {
                if (roomMgr.Insert(room) <= 0)
                {
                    this.RollBack();
                    error = roomMgr.Err;
                    return -1;
                }
            }
            else if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update)
            {
                if (roomMgr.Update(room) <= 0)
                {
                    this.RollBack();
                    error = roomMgr.Err;
                    return -1;
                }
            }
            else if (saveType == FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Delete)
            {
                //判断诊室是否被排班
                int result = roomMgr.IsExistUsed(room.ID);
                if (result < 0)
                {
                    this.RollBack();
                    error = "查询诊室失败" + roomMgr.Err;
                    return -1;
                }
                else if (result >= 1)
                {
                    this.RollBack();
                    error = "在以后的时间里，该诊室在队列维护中已经被维护，不能删除";
                    return -1;
                }

                if (roomMgr.Delete(room.ID) == -1)
                {
                    this.RollBack();
                    error = "删除诊室失败！\n请与系统管理员联系。" + roomMgr.Err;
                    return -1;
                }

                if (consoleMgr.DeleteByRoom(room.ID) == -1)
                {
                    this.RollBack();
                    error = "删除诊台失败！\n请与系统管理员联系。" + consoleMgr.Err;
                    return -1;
                }
            }

            if (InterfaceManager.GetISaveRoom().SaveCommitting(saveType, room) <= 0)
            {
                this.RollBack();
                error = InterfaceManager.GetISaveRoom().Err;
                return -1;
            }
            this.Commit();

            return 1;
        }

        /// <summary>
        /// 根据护士站和科室获取诊室信息
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList GetRooms(string nurseID, string deptID, ref string error)
        {
            FS.SOC.HISFC.Assign.BizLogic.Room roomMgr = new FS.SOC.HISFC.Assign.BizLogic.Room();
            ArrayList al = roomMgr.QueryRoomsByDept(nurseID, deptID);
            error = roomMgr.Err;
            return al;
        }

        /// <summary>
        /// 获取护士站下其他诊室信息（排除本id）
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="deptID"></param>
        /// <param name="roomid"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public ArrayList GetOtherRooms(string nurseID, string deptID,string roomid, ref string error)
        {
            FS.SOC.HISFC.Assign.BizLogic.Room roomMgr = new FS.SOC.HISFC.Assign.BizLogic.Room();
            ArrayList al = roomMgr.QueryOtherRoomsByDept(nurseID, deptID, roomid);
            error = roomMgr.Err;
            return al;
        }
    }
}
