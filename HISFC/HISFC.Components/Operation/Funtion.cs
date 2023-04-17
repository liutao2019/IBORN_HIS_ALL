using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.Operation
{
    class Funtion
    {
        /// <summary>
        /// 常数业务类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant conBizLogic = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 判断是否能够申请：择期的正台手术函数
        /// </summary>
        /// <param name="dtBegin">手术预约开始日期</param>
        /// <param name="dtEnd">手术预约结束日期</param>
        /// <param name="ExeDept">执行手术室</param>
        /// <param name="AppDept">申请科室</param>
        /// <param name="Type"></param>
        /// <param name="Error">返回错误信息</param>
        /// <returns>-1无正台 -2 未维护总台数常数 1 可以申请正台 </returns>
        public static int CheckLimitedLostNumber(DateTime dtBegin, DateTime dtEnd, string ExeDept, string AppDept, string Type,ref string Error)
        {
            int divideLostNum = 0;//分配数量
            //执行科室的分配给申请科室的正台剩余数量 ： 分配数 - 已申请正台数
            FS.HISFC.Models.Base.Department deptInfo = new FS.HISFC.Models.Base.Department();
            deptInfo.ID = ExeDept;
            divideLostNum = Environment.OperationManager.GetEnableTableNum(deptInfo, AppDept, dtBegin);
            if (divideLostNum <= 0)//手术室分配给科室数小于等于0
            {
                Error = "科室在该"+dtBegin.ToShortDateString()+"内已无正台,请修改手术台类型!";
                return -1;
            }

            //科室手术申请单
            int TotNum = 0;//全院每天的正台数量
            FS.HISFC.BizLogic.Manager.Constant conBizLogic = new FS.HISFC.BizLogic.Manager.Constant();

            FS.FrameWork.Models.NeuObject TotObj = conBizLogic.GetConstant("OPFORMALNUM", ExeDept);
            if (TotObj != null && TotObj.ID != "")
            {
                try
                {
                    TotNum = FS.FrameWork.Function.NConvert.ToInt32(TotObj.Memo);//执行科室的总正台数
                }
                catch
                {
                }
                if (TotNum == 0)
                {
                    Error = "请维护手术室的每天允许的申请总正台数：type=“OPFORMALNUM”；code=“手术室编码” name=“手术室名称”mark=“正台总数”";
                    return -1;
                }
            }
            else
            {
                Error = "请维护手术室的每天允许的申请总正台数常数：type=“OPFORMALNUM”；code=“手术室编码” name=“手术室名称”mark=“正台总数”";
                return -2;
            }

            int arrangeNum = 0;//执行科室已安排数量
            ArrayList arrangeList = new ArrayList();
            arrangeList = Environment.OperationManager.GetOpsAppList(ExeDept, dtBegin.ToString(), dtEnd.ToString(), false);
            if (arrangeList != null)
            {
                foreach (FS.HISFC.Models.Operation.OperationAppllication opsInfo in arrangeList)
                {
                    if (opsInfo.ExecStatus == "3" &&opsInfo.OperateKind=="1" && opsInfo.TableType=="1")//已安排的数量
                    {
                        arrangeNum++;//执行科室的已安排数量
                    }
                }
            }
            if (TotNum - arrangeNum <= 0)//手术每天总量减去已安排数小于等于0
            {
                Error = "手术室在" + dtBegin.ToShortDateString() + "号，已无正台,请修改手术台类型!";
                return -1;
            }
            return 1;
        }
    }
}
