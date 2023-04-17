using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.OutPatientOrder.Controls.IOutPateintTree
{
    /// <summary>
    /// 门诊患者树列表
    /// </summary>
    partial class tvOutOrderPatientList : FS.FrameWork.WinForms.Controls.NeuTreeView
    {
        #region 变量

        protected ArrayList myPatients = new ArrayList();

        #endregion

        #region 方法

        /// <summary>
        /// 刷新患者列表
        /// </summary>
        /// <param name="isSee">是否已诊</param>
        /// <returns></returns>
        public int Fresh(bool isSee)
        {
            DateTime dtNow = CacheManager.ConManager.GetDateTimeFromSysDateTime();
            DateTime dtEnd = dtNow;

            TreeNode rootNode = new TreeNode();
            rootNode.ImageIndex = 3;
            rootNode.SelectedImageIndex = 2;

            ArrayList alRegList = new ArrayList();

            #region 未诊

            if (!isSee)
            {
                #region 患者列表

                ////分诊状态
                //string assignFlag = "";

                //if (isOnlyShowAssignedPatient)
                //{
                //    assignFlag = "'" + ((Int32)EnuTriageStatus.In).ToString() + "','" + ((Int32)EnuTriageStatus.Delay).ToString() + "'";
                //}
                //else
                //{
                //    assignFlag = "'" + ((Int32)EnuTriageStatus.Triage).ToString() + "','" + ((Int32)EnuTriageStatus.In).ToString() + "','" + ((Int32)EnuTriageStatus.Delay).ToString() + "'";
                //}

                ////科室未指定医师的患者列表
                //ArrayList alDept = assignIntegrate.QuerySimpleAssignPatientByDeptID(dtNow.Date, dtNow.AddDays(1), assignFlag, this.employee.Dept.ID);

                ////被指定为诊疗医师的患者列表
                //ArrayList alPersonal = assignIntegrate.QuerySimpleAssignPatientByDoctID(dtNow.Date, dtNow.AddDays(1), assignFlag, this.employee.ID, this.employee.Dept.ID);

                #endregion

                rootNode.Text = "待诊患者 (" + alRegList.Count.ToString() + "人)";

            }

            #endregion

            #region 已诊

            else
            {
                #region 患者列表

                ////按时间段查询已诊患者列表
                //if (interval != null && interval.Length == 2)
                //{
                //    al = regManagement.QueryBySeeDocAndSeeDate(this.employee.ID, interval[0], interval[1], true);
                //}
                //else if (isShow24HoursPatient)//此处修改为查询最近24小时看诊的所有患者
                //{
                //    al = regManagement.QueryBySeeDocAndSeeDate(this.employee.ID, dt.AddDays(-1), dt.AddDays(2), true);
                //}
                //else
                //{
                //    //凌晨时间段显示最近五小时内已诊患者
                //    if (dt.Hour >= 0 && dt.Hour <= 3)
                //    {
                //        al = regManagement.QueryBySeeDocAndSeeDate(this.employee.ID, dt.AddHours(-5), dt.AddDays(2), true);
                //    }
                //    else
                //    {
                //        al = regManagement.QueryBySeeDocAndSeeDate(this.employee.ID, dt.Date, dt.AddDays(2), true);
                //    }
                //}

                #endregion

                rootNode.Text = "已诊患者 (" + alRegList.Count.ToString() + "人)";
            }

            #endregion

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载患者列表，请稍等...");
            Application.DoEvents();

            for (int i = 0; i < alRegList.Count; i++)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(i + 1, alRegList.Count);
                Application.DoEvents();

                FS.HISFC.Models.Registration.Register reg = alRegList[i] as FS.HISFC.Models.Registration.Register;

                AddToPatientRoot(rootNode, reg);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.Nodes.Clear();
            Nodes.Add(rootNode);

            return 1;
        }

        /// <summary>
        /// 加载树列表
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="regObj"></param>
        private void AddToPatientRoot(TreeNode parentNode, FS.HISFC.Models.Registration.Register regObj)
        {
            TreeNode patientNode = new TreeNode();

            #region 设置显示图标

            int image = 0;

            if (regObj.Sex.ID.ToString() == "F")//女
            {
                if (regObj.IsBaby)
                {
                    //小女孩
                    image = 7;
                }
                else
                {
                    //女人
                    image = 5;
                }
            }
            else //男
            {
                if (regObj.IsBaby)
                {
                    //小男孩
                    image = 6;
                }
                else
                {
                    //男人
                    image = 4;
                }
            }
            patientNode.ImageIndex = image;
            patientNode.SelectedImageIndex = image;

            #endregion

            #region 显示名称

            string preFlag = string.Empty;

            if (!regObj.IsSee
                && (regObj.RegType == FS.HISFC.Models.Base.EnumRegType.Pre
                || regObj.DoctorInfo.SeeDate.Date > regObj.InputOper.OperTime.Date))
            {
                preFlag = "预";
            }

            //assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Delay;

            patientNode.Text = regObj.Name
                            + "【" + preFlag + regObj.OrderNO.ToString()
                            + "(" + regObj.DoctorInfo.SeeNO.ToString() + ")】"
                            + "*" + regObj.DoctorInfo.Templet.RegLevel.Name;

            #endregion

            patientNode.Tag = regObj;

            parentNode.Nodes.Add(patientNode);
        }

        #endregion
    }
}
