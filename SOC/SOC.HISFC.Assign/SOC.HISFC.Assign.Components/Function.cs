using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Assign.Components
{
    public class Function
    {
        #region 分诊相关

        /// <summary>
        /// 获取护士站对应的分诊控制
        /// </summary>
        /// <param name="nurse"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Base.DepartmentStat GetNurseDepartmentStat(FS.FrameWork.Models.NeuObject nurse)
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager statManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            statManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            ArrayList al = statManager.LoadByChildren("14", nurse.ID);
            FS.HISFC.Models.Base.DepartmentStat temp = null;
            if (al != null)
            {
                foreach (FS.HISFC.Models.Base.DepartmentStat stat in al)
                {
                    if (stat.PardepCode.Equals("AAAA"))
                    {
                        temp = stat;
                        break;
                    }
                }
            }

            return temp;
        }
        
        /// <summary>
        /// 门诊分诊操作状态
        /// 避免同步操作引起不可预料的问题
        /// </summary>
        public enum EnumAssignOperType
        {
            手工分诊,
            自动分诊,
            队列显示,
            刷新,
            叫号,
            大屏显示处理,
            空闲
        }

        #endregion

        #region 自动注册相关

        /// <summary>
        /// 注册护士站自动分诊
        /// </summary>
        /// <param name="nurse">护士站</param>
        public static bool RegisterNurse(FS.FrameWork.Models.NeuObject nurse, ref string info)
        {
            if (nurse == null)
            {
                return false;
            }

            string myIP = "";
            try
            {
                System.Net.IPAddress[] IPS = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
                foreach (System.Net.IPAddress IP in IPS)
                {
                    if (IP.IsIPv6LinkLocal == true)
                    {
                        continue;
                    }

                    myIP = IP.ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                info = "注册自动发生错误：" + ex.Message;
                return false;
            }

            if (string.IsNullOrEmpty(myIP))
            {
                info = "注册自动发生错误：没有获取到本机IP地址";
                return false;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizLogic.Manager.DepartmentStatManager statManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            statManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.Models.Base.DepartmentStat temp = Function.GetNurseDepartmentStat(nurse);
            if (temp == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                info = "护士站关闭，不可以注册自动分诊！";
                return false;
            }
            else if (!string.IsNullOrEmpty(temp.Memo))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                info = "护士站已经注册IP：" + temp.Memo + "不可以重复注册自动分诊！";
                return false;
            }

            temp.Memo = myIP;

            if (statManager.UpdateDepartmentStat(temp) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                info = "注册自动打印发生错误：" + statManager.Err;
                return false;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            info = "注册自动分诊成功，程序可以自动分诊！";
            return true;
        }

        /// <summary>
        /// 取消进诊
        /// </summary>
        /// <param name="nurse"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool CancelRegisterNurse(FS.FrameWork.Models.NeuObject nurse, ref string info)
        {
            if (nurse == null)
            {
                return false;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizLogic.Manager.DepartmentStatManager statManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            statManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.HISFC.Models.Base.DepartmentStat temp = Function.GetNurseDepartmentStat(nurse);
            if (temp == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                info = "护士站关闭，不需要取消注册自动分诊！";
                return true;
            }
            else if (string.IsNullOrEmpty(temp.Memo))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                info = "护士站已经取消注册，不需要取消自动分诊！";
                return true;
            }

            temp.Memo = "";

            if (statManager.UpdateDepartmentStat(temp) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                info = "取消注册自动分诊发生错误：" + statManager.Err;
                return false;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            info = "取消注册自动分诊成功，程序需要手工分诊！";
            return true;
        }

        /// <summary>
        /// 门诊分诊自动分诊控制
        /// </summary>
        /// <param name="drugTeminalMemo"></param>
        /// <returns>true 可以启用自动分诊</returns>
        public static bool CheckAutoAssignPrive(FS.FrameWork.Models.NeuObject nurse)
        {
            try
            {
                FS.HISFC.Models.Base.DepartmentStat temp = Function.GetNurseDepartmentStat(nurse);

                if (temp == null)
                {
                    return false;
                }
                else if (string.IsNullOrEmpty(temp.Memo))
                {
                    return false;
                }

                System.Net.IPAddress[] IPS = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
                foreach (System.Net.IPAddress IP in IPS)
                {
                    if (IP.IsIPv6LinkLocal == true)
                    {
                        continue;
                    }


                    if (IP.ToString() == temp.Memo)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        #endregion
        
        #region 权限相关

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3Code">三级权限</param>
        /// <returns>true有，false无</returns>
        public static bool JugePrive(string class2Code, string class3Code)
        {
            if (string.IsNullOrEmpty(class2Code) || string.IsNullOrEmpty(class3Code))
            {
                return false;
            }
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager powerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            List<FS.FrameWork.Models.NeuObject> listPrive = powerDetailManager.QueryUserPriv(powerDetailManager.Operator.ID, class2Code, class3Code);
            if (listPrive == null)
            {
                return false;
            }

            return listPrive.Count > 0;
        }

        /// <summary>
        /// 根据护士站查找对应的科室
        /// </summary>
        /// <param name="nurseID"></param>
        /// <returns></returns>
        public static ArrayList GetNurseDept(string nurseID, ref string error)
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager statManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            //查找护士站下面的科室
            ArrayList al = statManager.LoadByParent("14", nurseID);
            error = statManager.Err;
            return al;

        }

        #endregion

        #region 患者相关

        /// <summary>
        /// 检查是否当前护士站
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public static bool CheckPatientRegDept(FS.FrameWork.Models.NeuObject nurse, FS.HISFC.Models.Registration.Register register)
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager statManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();
            string error="";
            ArrayList al = Function.GetNurseDept(nurse.ID, ref error);
            if (al != null)
            {
                foreach (FS.HISFC.Models.Base.DepartmentStat stat in al)
                {
                    if (stat.DeptCode.Equals(register.DoctorInfo.Templet.Dept.ID))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region 队列相关

        /// <summary>
        /// 队列与队列模板的转换
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public static FS.SOC.HISFC.Assign.Models.QueueTemplate Convert(FS.SOC.HISFC.Assign.Models.Queue queue)
        {
            FS.SOC.HISFC.Assign.Models.QueueTemplate queueTemplate = new FS.SOC.HISFC.Assign.Models.QueueTemplate();
            queueTemplate.AssignNurse = queue.AssignNurse;
            queueTemplate.ID = queue.ID;
            queueTemplate.Name = queue.Name;
            queueTemplate.AssignDept = queue.AssignDept;
            queueTemplate.Doctor = queue.Doctor;
            queueTemplate.IsExpert = queue.IsExpert;
            queueTemplate.IsValid = queue.IsValid;
            queueTemplate.Memo = queue.Memo;
            queueTemplate.Oper = queue.Oper;
            queueTemplate.QueueType = queue.QueueType;
            queueTemplate.QueueDate = queue.QueueDate;
            queueTemplate.RegLevel = queue.RegLevel;
            queueTemplate.SRoom = queue.SRoom;
            queueTemplate.WaitingCount = queue.WaitingCount;
            queueTemplate.Order = queue.Order;
            queueTemplate.Noon = queue.Noon;
            queueTemplate.Console = queue.Console;
            queueTemplate.PatientConditionType = queue.PatientConditionType;

            return queueTemplate;
        }

        #endregion
    }
}
