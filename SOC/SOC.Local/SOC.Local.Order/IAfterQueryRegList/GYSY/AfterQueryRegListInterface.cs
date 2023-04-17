using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.GYSY.IAfterQueryRegList
{
    /// <summary>
    /// 用于门诊医生站查询挂号列表、自助挂号的操作接口
    /// </summary>
    class AfterQueryRegListInterface:FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList
    {

        #region IAfterQueryRegList 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
            set
            {
                errInfo = value;
            }
        }

        /// <summary>
        /// 患者基本信息
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Registration.Registration regInterMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();


        /// <summary>
        /// 控制参数管理
        /// </summary>
        FS.FrameWork.Management.ControlParam contrlManager = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 开立科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject reciptDept = null;


        /*
        /// <summary>
        /// 获取开方科室
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDept()
        {
            try
            {
                if (this.reciptDept == null)
                {
                    FS.HISFC.Models.Registration.Schema schema = this.regInterMgr.GetSchema(this.contrlManager.Operator.ID, this.contrlManager.GetDateTimeFromSysDateTime());
                    if (schema != null && schema.Templet.Dept.ID != "")
                    {
                        this.reciptDept = schema.Templet.Dept.Clone();
                    }
                    //没有排版取登陆科室作为开立科室
                    else
                    {
                        this.reciptDept = ((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Dept.Clone(); //开立科室
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return this.reciptDept;
        }
        */ 


        /// <summary>
        /// 未看诊之前,根据挂号科室 和挂号专家过滤
        /// </summary>
        /// <param name="alReginfo"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int OnAfterQueryRegList( System.Collections.ArrayList alReginfo, FS.FrameWork.Models.NeuObject dept)
        {
            try
            {
                ArrayList alTemp = new ArrayList();


                foreach (FS.HISFC.Models.Registration.Register regObj in alReginfo)
                {

                    //看诊医生不一致
                    if (!regObj.IsSee && !string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID) && regObj.DoctorInfo.Templet.Doct.ID != this.contrlManager.Operator.ID)
                    {
                        continue;
                    }
                    //看诊科室不一致
                    //else if (!regObj.IsSee && this.GetReciptDept().ID != regObj.DoctorInfo.Templet.Dept.ID)
                    //{
                    //    alReginfo.Remove(regObj);
                    //    continue;
                    //}
                    else
                    {
                        alTemp.Add(regObj);
                    }
                }


                /*
                FS.HISFC.Models.Registration.Register regObj = null;
                while (alReginfo != null && alReginfo.Count > 0)
                {

                    regObj = alReginfo[0] as FS.HISFC.Models.Registration.Register;

                    //看诊医生不一致
                    if (!regObj.IsSee && !string.IsNullOrEmpty(regObj.DoctorInfo.Templet.Doct.ID) && regObj.DoctorInfo.Templet.Doct.ID != this.contrlManager.Operator.ID)
                    {
                        alReginfo.Remove(regObj);
                        continue;

                    }
                    //看诊科室不一致
                    //else if (!regObj.IsSee && this.GetReciptDept().ID != regObj.DoctorInfo.Templet.Dept.ID)
                    //{
                    //    alReginfo.Remove(regObj);
                    //    continue;
                    //}
                    else 
                    {
                        alTemp.Add(regObj);
                        alReginfo.Remove(regObj);
                    }
                  
                }
                */

                alReginfo.Clear();
                alReginfo.AddRange(alTemp);
            }
            catch
            {

            }
            return 1;
        }

        /// <summary>
        /// 医生站自助挂号后操作
        /// </summary>
        /// <param name="regInfo"></param>
        /// <returns></returns>
        public int OnConfirmRegInfo(FS.HISFC.Models.Registration.Register regInfo)
        {
            return 1;
        }

        #endregion
    }
}
