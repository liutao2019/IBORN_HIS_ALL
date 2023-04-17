using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.Components.DCP.Classes;
using System.Collections;

namespace FS.SOC.HISFC.Components.DCP
{
    /// <summary>
    /// UnionManager<br></br>
    /// [功能描述: 报卡接口实现]<br></br>
    /// [创 建 者: zengft]<br></br>
    /// [创建时间: 2008-8-20]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class UnionManager : FS.SOC.HISFC.BizProcess.DCPInterface.IUnionManager
    {
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        public UnionManager()
        {
        }

        private FS.SOC.HISFC.Components.DCP.frmReportManager frmReportManager = new frmReportManager();
        ///<summary>
        /// 疾病控制预防管理接口函数
        /// 新建报卡上报疾病到预防保健科
        /// </summary>
        /// <param name="patientType">患者类型</param>
        /// <returns></returns>
        public FS.SOC.HISFC.DCP.Enum.ReportOperResult RegisterReport(System.Windows.Forms.IWin32Window owner, FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            frmReportManager.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            frmReportManager.Init(patientType);
            frmReportManager.SetControlProperty(null, patientType, "");
            frmReportManager.ReportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.OK;
            if (patientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                frmReportManager.QueryDoctReportInfo();
            }
            else if (patientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
            {
                frmReportManager.QueryDeptReportInfo();
            }
            frmReportManager.BringToFront();

            frmReportManager.Show(owner);
            return frmReportManager.ReportOperResult;
        }

        /// <summary>
        /// 疾病控制预防管理接口报卡函数
        /// 上报某患者疾病情况到预防保健科
        /// </summary>
        /// <param name="patient">患者实体</param>
        /// <param name="patientType">患者类型</param>
        /// <returns></returns>
        public FS.SOC.HISFC.DCP.Enum.ReportOperResult RegisterReport(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            frmReportManager.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            frmReportManager.Init(patientType);
            frmReportManager.SetControlProperty(patient, patientType, "");
            frmReportManager.ReportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.OK;
            if (patientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                frmReportManager.QueryDoctReportInfo();
            }
            else if (patientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
            {
                frmReportManager.QueryDeptReportInfo();
            }
            frmReportManager.BringToFront();
            frmReportManager.Show(owner);

            return frmReportManager.ReportOperResult;
        }

        /// <summary>
        /// 疾病控制预防管理接口函数
        /// 根据诊断名称确定是否上报疾病到预防保健科
        /// </summary>
        /// <param name="owner">所有者</param>
        /// <param name="patient">患者实体</param>
        /// <param name="patientType">患者类型</param>
        /// <param name="diagName">诊断名称</param>
        /// <param name="msg">提示信息</param>
        /// <returns>FS.SOC.HISFC.DCP.Enum.ReportOperResult</returns>
        public FS.SOC.HISFC.DCP.Enum.ReportOperResult CheckDisease(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, FS.SOC.HISFC.DCP.Enum.PatientType patientType, string diagName, out string msg)
        {
            msg = "";
            string diseaseCode = "";

            if (FS.SOC.HISFC.Components.DCP.Classes.Function.CheckDiagNose(diagName, out diseaseCode))
            {
                //需要判断是否已经填写传染病报告卡
               // System.Windows.Forms.MessageBox.Show("诊断：" + diagName + "为传染病诊断，请填写传染病报告卡！", "提示", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
               
                // {79888F16-C9EE-4eee-931D-86A8838DDF2D}增加判断传染病时弹出窗口
               DialogResult result = System.Windows.Forms.MessageBox.Show("诊断：" + diagName + "为传染病诊断，请填写传染病报告卡！", "提示", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);

             // {79888F16-C9EE-4eee-931D-86A8838DDF2D}增加判断传染病时弹出窗口

               if (result == DialogResult.No)
               {
                   bool isReport1 = true;
                   //System.Windows.Forms.MessageBox.Show("请填写不报卡理由！", "提示", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                   if (FS.SOC.HISFC.Components.DCP.Classes.Function.CheckPatientNeedResonOfNot(patient, diagName, ref isReport1))
                   {
                       Controls.ucReasonOfNot ucReasonofnot = new Controls.ucReasonOfNot(patient, diagName);
                       DialogResult dia = FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucReasonofnot,FormBorderStyle.None);
                       if (dia == DialogResult.OK)
                       {

                       }

                       else
                       {

                           return FS.SOC.HISFC.DCP.Enum.ReportOperResult.Other;
                       }
                   }
                   
                   
               }
               // {79888F16-C9EE-4eee-931D-86A8838DDF2D}增加判断传染病时弹出窗口
                
                if (result == DialogResult.Yes)
               {

                   bool isReport = true;
                   //弹出报卡窗口
                   if (FS.SOC.HISFC.Components.DCP.Classes.Function.CheckPatientNeedReport(patient, diseaseCode, ref isReport))
                   {
                       //如果强制报卡,则一定要求保存成功后才能继续开方
                       frmReportManager.Init(patientType);
                       frmReportManager.SetControlProperty(patient, patientType, diseaseCode);
                       frmReportManager.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                       frmReportManager.BringToFront();
                       string s = frmReportManager.Text;
                       bool isMustReport = controlParamIntegrate.GetControlParam<bool>("DCP001", false, false);
                       if (isReport == false || !isMustReport)
                       {
                           frmReportManager.ReportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.OK;
                       }
                       else
                       {
                           frmReportManager.ReportOperResult = FS.SOC.HISFC.DCP.Enum.ReportOperResult.Cancel;
                       }
                       frmReportManager.Text = frmReportManager.Text + "－" + diagName;
                       if (owner == null)
                       {
                           frmReportManager.ShowDialog();
                       }
                       else
                       {
                           frmReportManager.ShowDialog(owner);
                       }
                       frmReportManager.Text = s;
                       return frmReportManager.ReportOperResult;

                   }
               }

            }
            return FS.SOC.HISFC.DCP.Enum.ReportOperResult.Other;
        }

        /// <summary>
        /// 获取疾病控制预防的反馈信息
        /// </summary>
        /// <returns></returns>
        public int GetDCPNotice(System.Windows.Forms.IWin32Window owner, FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            //提示不合格的项目
            //住院提示科室，门诊提示医生
            bool isNoticeUnEligible = controlParamIntegrate.GetControlParam<bool>("DCP002", false, false);
            if (isNoticeUnEligible)
            {
                FS.SOC.HISFC.BizLogic.DCP.DiseaseReport diseaseMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();
                ArrayList al = null;
                string msg = "";
                if (patientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
                {
                    al = diseaseMgr.GetReportListByStateAndDoctor(Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible), diseaseMgr.Operator.ID);
                    if (al != null && al.Count > 0)
                    {
                        FS.HISFC.DCP.Object.CommonReport report = al[0] as FS.HISFC.DCP.Object.CommonReport;
                        if (report == null)
                        {
                            return 0;
                        }

                        msg = "您有不合格的传染病报告卡，请查看不合格的【退卡原因】栏进行相应修改\n" +
                                           "                  例    报告编号：" + report.ReportNO + "\n" +
                                           "                          患者姓名：" + report.Patient.Name + "\n" +
                                           "                          疾病名称：" + report.Disease.Name + "\n" +
                                           "                          报告日期：" + report.ReportTime.ToShortDateString() + "\n" +
                                           "\n" +
                                           "-----------------------------  不合格报告修改步骤  -------------------------------\n" +
                                           "1、在医嘱开立界面点击【传染病报告】按钮\n" +
                                           "2、选择列表中的不合格报告卡，查看“退卡原因”\n" +
                                           "3、参照“退卡原因”进行修改并【保存】";

                        MessageBox.Show(owner, msg, "不合格报告卡提示：（例）退卡原因【" + report.OperCase + "】", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    al = diseaseMgr.GetCommonReportListByMore("report_date", DateTime.MinValue.ToString(), DateTime.MaxValue.ToString(), Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible), ((FS.HISFC.Models.Base.Employee)diseaseMgr.Operator).Dept.ID);

                    if (al != null && al.Count > 0)
                    {
                        FS.HISFC.DCP.Object.CommonReport report = al[0] as FS.HISFC.DCP.Object.CommonReport;
                        if (report == null)
                        {
                            return 0;
                        }
                        //查找医生姓名
                        if (string.IsNullOrEmpty(report.ReportDoctor.Name))
                        {
                            report.ReportDoctor = managerIntegrate.GetEmployeeInfo(report.ReportDoctor.ID);
                        }

                        if (report.ReportDoctor == null)
                        {
                            return 0;
                        }

                        msg = "您有不合格的传染病报告卡，请查看不合格的【退卡原因】栏进行相应修改\n" +
                                           "                  例    报告编号：" + report.ReportNO + "\n" +
                                           "                          患者姓名：" + report.Patient.Name + "\n" +
                                           "                          疾病名称：" + report.Disease.Name + "\n" +
                                           "                          报告日期：" + report.ReportTime.ToShortDateString() + "\n" +
                                           "                          报告医生：" + report.ReportDoctor.Name + "(" + report.ReportDoctor.ID + ")" + "\n" +
                                           "\n" +
                                           "-----------------------------  不合格报告修改步骤  -------------------------------\n" +
                                           "1、在医嘱开立界面点击【传染病报告】按钮\n" +
                                           "2、选择列表中的不合格报告卡，查看“退卡原因”\n" +
                                           "3、参照“退卡原因”进行修改并【保存】";

                        MessageBox.Show(owner, msg, "不合格报告卡提示：（例）退卡原因【" + report.OperCase + "】", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }


            }

            return 0;
        }

        #region 肿瘤报卡
        /// <summary>
        ///  肿瘤报卡登记
        /// </summary>
        /// <param name="register">门诊挂号信息</param>
        /// <param name="owner">父窗口</param>
        public void RegisterCancerReport(FS.HISFC.Models.Registration.Register register, System.Windows.Forms.IWin32Window owner)
        {
            if (!this.AllowReport())
            {
                return;
            }
            if (register != null)
            {
                FS.SOC.HISFC.Components.DCP.CancerReport.frmReportCancerRegister frm = new FS.SOC.HISFC.Components.DCP.CancerReport.frmReportCancerRegister(register, "");
                if (owner == null)
                {
                    frm.ShowDialog();
                }
                else
                {
                    frm.ShowDialog(owner);
                }
            }
            else
            {
                FS.SOC.HISFC.Components.DCP.CancerReport.frmReportCancerRegister frm = new FS.SOC.HISFC.Components.DCP.CancerReport.frmReportCancerRegister();
                if (owner == null)
                {
                    frm.ShowDialog();
                }
                else
                {
                    frm.ShowDialog(owner);
                }
            }
        }

        /// <summary>
        ///  肿瘤报卡登记
        /// </summary>
        /// <param name="register">住院登记信息</param>
        /// <param name="owner">父窗口</param>
        public void RegisterCancerReport(FS.HISFC.Models.RADT.PatientInfo patientInfo, System.Windows.Forms.IWin32Window owner)
        {
            if (!this.AllowReport())
            {
                return;
            }
            if (patientInfo != null)
            {
                FS.SOC.HISFC.Components.DCP.CancerReport.frmReportCancerRegister frm = new FS.SOC.HISFC.Components.DCP.CancerReport.frmReportCancerRegister(patientInfo);
                if (owner == null)
                {
                    frm.ShowDialog();
                }
                else
                {
                    frm.ShowDialog(owner);
                }
            }
            else
            {
                FS.SOC.HISFC.Components.DCP.CancerReport.frmReportCancerRegister frm = new FS.SOC.HISFC.Components.DCP.CancerReport.frmReportCancerRegister();
                if (owner == null)
                {
                    frm.ShowDialog();
                }
                else
                {
                    frm.ShowDialog(owner);
                }
            }
        }

        /// <summary>
        /// 判断是否允许报卡
        /// </summary>
        /// <returns>True 允许报卡</returns>
        public bool AllowReport()
        {
            FS.SOC.HISFC.BizProcess.DCP.Common conMgr = new FS.SOC.HISFC.BizProcess.DCP.Common();
            FS.HISFC.Models.Base.Employee emplInfo = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            //特权用户
            if (emplInfo.ID == "001406" || emplInfo.ID == "009999")
            {
                return true;
            }
            else
            {
                //授权科室
                string deptCode = emplInfo.Dept.ID;
                ArrayList al = conMgr.QueryConstantList("NEEDREPORTDEPT");

                //开始试用时少数科室需要维护常数，全院上线后可以不维护
                if (al == null || al.Count == 0)
                {
                    return true;
                }

                foreach (FS.FrameWork.Models.NeuObject dept in al)
                {
                    if (dept.ID == deptCode)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
