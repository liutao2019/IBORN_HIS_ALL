using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDWY.Array
{
    public class LocalManager : FS.HISFC.BizLogic.Manager.DataBase
    {
        /// <summary>
        /// 按护士站/分诊日期/午别查询分诊队列信息
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <param name="noonID"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, DateTime queueDate, string noonID)
        {
            string sql = @"
                            SELECT '{0}', --门诊护士站代码
                                   f.id, --队列代码
                                   case when f.doct_name is null then f.room_name
                                     else f.room_name||'('||f.doct_name||')' end , --队列名称
                                   noon_code, --午别
                                   nvl((select 1 from fin_opr_reglevel l
                                   where l.reglevl_code=f.reglevl_code),2), --1医生队列/2自定义队列
                                   0, --显示顺序
                                   valid_flag, --1有效/0无效
                                   remark, --备注
                                   oper_code, --操作员
                                   oper_date, --操作时间
                                   f.see_date, --队列日期
                                   doct_code, --看诊医生
                                   ROOM_ID,
                                   ROOM_NAME,
                                   CONSOLE_CODE,
                                   CONSOLE_NAME,
                                   nvl((select l.is_expert from fin_opr_reglevel l
                                   where l.reglevl_code=f.reglevl_code),0),--是否专家
                                   dept_code,
                                   dept_name,
                                   0 --候诊人数
                              FROM fin_opr_schema f --门诊护士站分诊队列表
                             where f.room_id in
                                   (select f.room_id from met_nuo_room f where f.dept_code = '{0}')";

            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "基本sql出错!" + sql;
                this.ErrCode = "基本sql出错!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();

                    //所属护士站
                    queue.Dept.ID = this.Reader[0].ToString();
                    //队列代码
                    queue.ID = this.Reader[1].ToString();
                    //队列名称
                    queue.Name = this.Reader[2].ToString();
                    //午别代码
                    queue.Noon.ID = this.Reader[3].ToString();

                    //队列类型[2007/03/27]
                    queue.User01 = this.Reader[4].ToString();

                    //显示顺序
                    if (!this.Reader.IsDBNull(5))
                    {
                        queue.Order = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());
                    }
                    //是否有效
                    queue.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[6].ToString());
                    //备注
                    queue.Memo = this.Reader[7].ToString();
                    //操作员
                    queue.Oper.ID = this.Reader[8].ToString();
                    //操作时间
                    queue.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());
                    //队列日期
                    queue.QueueDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());
                    //看诊医生
                    queue.Doctor.ID = this.Reader[11].ToString();
                    //诊室
                    queue.SRoom.ID = this.Reader[12].ToString();
                    queue.SRoom.Name = this.Reader[13].ToString();
                    //诊台
                    queue.Console.ID = this.Reader[14].ToString();
                    queue.Console.Name = this.Reader[15].ToString();
                    //专家标志
                    queue.ExpertFlag = this.Reader[16].ToString();
                    //分诊科室
                    queue.AssignDept.ID = this.Reader[17].ToString();
                    queue.AssignDept.Name = this.Reader[18].ToString();
                    queue.WaitingCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19]);

                    al.Add(queue);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "查询门诊护士站分诊队列信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }
    }
}
