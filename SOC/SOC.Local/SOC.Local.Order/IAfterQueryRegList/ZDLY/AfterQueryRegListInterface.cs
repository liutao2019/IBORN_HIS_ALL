using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.ZDLY.IAfterQueryRegList
{
    /// <summary>
    /// 用于门诊医生站查询挂号列表、自助挂号的操作接口
    /// </summary>
    class AfterQueryRegListInterface : FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList
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
        /// 整合管理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager interManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 开立科室
        /// </summary>
        private FS.FrameWork.Models.NeuObject reciptDept = null;

        /// <summary>
        /// 医生排版信息
        /// </summary>
        private FS.HISFC.Models.Registration.Schema schema = null;

        private FS.HISFC.Models.Base.Employee reciptDoct = null;

        ArrayList YkDept;

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

                    this.reciptDept = ((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Dept.Clone(); //开立科室

                    this.reciptDoct = (FS.HISFC.Models.Base.Employee)this.contrlManager.Operator;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return this.reciptDept;
        }


        /// <summary>
        /// 未看诊之前,根据挂号科室 和挂号专家过滤
        /// </summary>
        /// <param name="alReginfo"></param>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int OnAfterQueryRegList(System.Collections.ArrayList alReginfo, FS.FrameWork.Models.NeuObject dept)
        {

            DateTime sysTime = this.constantManager.GetDateTimeFromSysDateTime();


            //登入
            bool LoginDept = false;
            try
            {
                if (YkDept == null)
                {
                    YkDept = this.interManager.GetConstantList("YkDept");
                }
                IList<string> YkDeptList = new List<string>();

                foreach (FS.FrameWork.Models.NeuObject deptuObj in YkDept)
                {
                    if (deptuObj.ID.Equals(GetReciptDept().ID))
                    {
                        LoginDept = true;
                    }
                    YkDeptList.Add(deptuObj.ID);
                }

                ArrayList tempDept = alReginfo.Clone() as ArrayList;

                alReginfo.Clear();

                foreach (FS.HISFC.Models.Registration.Register regObj in tempDept)
                {
                    //预约挂号的不让看到
                    //if (regObj.DoctorInfo.SeeDate > sysTime)
                    //{
                    //    continue;
                    //}

                    regObj.DoctorInfo.Templet.RegLevel = this.regInterMgr.QueryRegLevelByCode(regObj.DoctorInfo.Templet.RegLevel.ID);

                    if (LoginDept)
                    {
                        if (YkDeptList.Contains(regObj.DoctorInfo.Templet.Dept.ID))
                        {
                            alReginfo.Add(regObj);
                        }
                    }
                    else
                    {
                        if (!YkDeptList.Contains(regObj.DoctorInfo.Templet.Dept.ID))
                        {
                            //    FS.HISFC.Models.Registration.RegLevel doctRegLevel = this.regInterMgr.GetRegLevl(this.GetReciptDept().ID, reciptDoct.ID, (((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Level.ID));
                            //    if (doctRegLevel != null)
                            //    {
                            //        if (doctRegLevel.UserCode.CompareTo(regObj.DoctorInfo.Templet.RegLevel.UserCode) >= 0)
                            //        {
                            //            if (this.GetReciptDept().ID.Equals(regObj.DoctorInfo.Templet.Dept.ID))
                            //            {
                            //                alReginfo.Add(regObj);
                            //            }
                            //            else
                            //            {
                            //                if (MessageBox.Show("患者有效挂号科室为《" + regObj.DoctorInfo.Templet.Dept.Name + "》是否继续看诊？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            //                {
                            alReginfo.Add(regObj);
                            //                }
                            //            }
                            //        }
                            //    }
                            //    else
                            //    {
                            //        alReginfo.Add(regObj);
                            //    }
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }




            ////未看诊的，挂号级别不一致的给出提示
            //if (!regObj.IsSee)
            //{
            //    FS.HISFC.Models.Registration.Schema schema = this.regInterMgr.GetSchema(this.contrlManager.Operator.ID, this.contrlManager.GetDateTimeFromSysDateTime());
            //    if(schema!=null && !string.IsNullOrEmpty(schema.Templet.RegLevel.ID))
            //    {
            //        FS.HISFC.Models.Registration.RegLevel regLevelObj = this.regInterMgr.QueryRegLevelByCode(regObj.DoctorInfo.Templet.RegLevel.ID);
            //        FS.HISFC.Models.Registration.RegLevel curLevelObj = this.regInterMgr.QueryRegLevelByCode(schema.Templet.RegLevel.ID);
            //        if (!regLevelObj.IsExpert&&curLevelObj.IsExpert)
            //        {
            //            MessageBox.Show("当前患者挂号信息为普通门诊，您的排班信息为专家！");
            //        }
            //    }
            //} 
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
