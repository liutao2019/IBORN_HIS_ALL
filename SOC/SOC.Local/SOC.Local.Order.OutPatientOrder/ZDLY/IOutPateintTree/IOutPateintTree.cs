using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.OutPatientOrder.ZDLY.IOutPateintTree
{
    /// <summary>
    /// 查找卡号加载到医生未诊列表接口
    /// </summary>
    public class IOutPateintTree : SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPateintTree
    {
        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntergrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        #region IOutPateintTree 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = "";

        public string Err
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
        /// 添加到未诊列表树节点之前
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns></returns>
        public int AfterAddToTree(FS.HISFC.Models.Registration.Register regObj)
        {
            return 1;
        }

        /// <summary>
        /// 添加到未诊列表树节点之后
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns></returns>
        public int BeforeAddToTree(FS.HISFC.Models.Registration.Register regObj)
        {
            try
            {
                #region 提前看诊给出提示

                //是否提前看诊
                bool isPreSee = false;

                //隔日提前看诊
                if (regObj.DoctorInfo.SeeDate.Date > this.constantManager.GetDateTimeFromSysDateTime().Date)
                {
                    //if (MessageBox.Show("患者【" + regObj.Name + "】挂号时间为 " + regObj.DoctorInfo.SeeDate.ToString() + ",\r\n\r\n午别为：" + SOC.HISFC.BizProcess.Cache.Common.GetNoonByTime(regObj.DoctorInfo.SeeDate).Name + ",\r\n\r\n是否提前看诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    //{
                    //    return 0;
                    //}
                    isPreSee = true;
                }
                //上午看下午午别的挂号
                else if (regObj.DoctorInfo.SeeDate.Date == this.constantManager.GetDateTimeFromSysDateTime().Date
                    && SOC.HISFC.BizProcess.Cache.Common.GetNoonByTime(regObj.DoctorInfo.SeeDate).StartTime.TimeOfDay >
                    SOC.HISFC.BizProcess.Cache.Common.GetNoonByTime(constantManager.GetDateTimeFromSysDateTime()).StartTime.TimeOfDay)
                {
                    //if (MessageBox.Show("患者【" + regObj.Name + "】挂号时间为 " + regObj.DoctorInfo.SeeDate.ToString() + ",\r\n\r\n午别为：" + SOC.HISFC.BizProcess.Cache.Common.GetNoonByTime(regObj.DoctorInfo.SeeDate).Name + ",\r\n\r\n是否提前看诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    //{
                    //    return 0;
                    //}
                    isPreSee = true;
                }

                if (isPreSee)
                {
                    if (MessageBox.Show("患者【" + regObj.Name + "】挂号时间为 " + regObj.DoctorInfo.SeeDate.ToString() + ",\r\n\r\n午别为：" + SOC.HISFC.BizProcess.Cache.Common.GetNoonByTime(regObj.DoctorInfo.SeeDate).Name + ",\r\n\r\n是否提前看诊？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        return 0;
                    }
                }

                #endregion

                #region 处理挂号级别限制问题

                //挂号级别限制
                bool isReglevlLimit = false;

                FS.HISFC.Models.Registration.RegLevel doctRegLevel = this.regIntergrate.GetRegLevl(this.GetReciptDept().ID, reciptDoct.ID, (((FS.HISFC.Models.Base.Employee)reciptDoct).Level.ID));
                if (doctRegLevel != null)
                {
                    if (regObj.DoctorInfo.Templet.Doct.ID != reciptDoct.ID)
                    {
                        //不同挂号级别看诊限制
                        if (doctRegLevel.UserCode.CompareTo(regObj.DoctorInfo.Templet.RegLevel.UserCode) < 0)
                        {
                            MessageBox.Show("患者【" + regObj.Name + "】的挂号级别为【" + regObj.DoctorInfo.Templet.RegLevel.Name + "】,\r\n您没有权限看诊！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return 0;
                        }
                        else
                        {
                            isReglevlLimit = true;
                            //if (MessageBox.Show("患者【" + regObj.Name + "】的挂号级别为【" + regObj.DoctorInfo.Templet.RegLevel.Name + "】,\r\n是否继续看诊？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            //{
                            //    return 0;
                            //}
                        }
                    }
                }
                #endregion

                #region 处理不同科室看诊问题

                //是否不同科室提示
                bool isDiffDept = false;

                //不同科室看诊提示
                if (!this.GetReciptDept().ID.Equals(regObj.DoctorInfo.Templet.Dept.ID))
                {
                    //if (MessageBox.Show("患者【" + regObj.Name + "】的挂号科室为【" + regObj.DoctorInfo.Templet.Dept.Name + "】\r\n\r\n是否继续看诊？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    //{
                    //    return 0;
                    //}
                    isDiffDept = true;
                }
                #endregion

                if (isReglevlLimit && isDiffDept)
                {
                    if (MessageBox.Show("患者【" + regObj.Name + "】的挂号级别为【" + regObj.DoctorInfo.Templet.RegLevel.Name + "】,\r\n\r\n挂号科室为【" + regObj.DoctorInfo.Templet.Dept.Name + "】,\r\n\r\n是否继续看诊？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        return 0;
                    }
                }
                else if (isReglevlLimit)
                {
                    if (MessageBox.Show("患者【" + regObj.Name + "】的挂号级别为【" + regObj.DoctorInfo.Templet.RegLevel.Name + "】,\r\n\r\n是否继续看诊？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        return 0;
                    }
                }
                else if (isDiffDept)
                {
                    if (MessageBox.Show("患者【" + regObj.Name + "】的挂号科室为【" + regObj.DoctorInfo.Templet.Dept.Name + "】\r\n\r\n是否继续看诊？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        return 0;
                    }
                }
            }
            catch
            {
            }
            return 1;
        }

        #endregion

        #region IAfterQueryRegList 成员

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

                    this.reciptDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Clone(); //开立科室

                    this.reciptDoct = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return this.reciptDept;
        }

        #endregion
    }
}
