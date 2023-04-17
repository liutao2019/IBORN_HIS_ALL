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
    /// [��������: �����ӿ�ʵ��]<br></br>
    /// [�� �� ��: zengft]<br></br>
    /// [����ʱ��: 2008-8-20]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
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
        /// ��������Ԥ������ӿں���
        /// �½������ϱ�������Ԥ��������
        /// </summary>
        /// <param name="patientType">��������</param>
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
        /// ��������Ԥ������ӿڱ�������
        /// �ϱ�ĳ���߼��������Ԥ��������
        /// </summary>
        /// <param name="patient">����ʵ��</param>
        /// <param name="patientType">��������</param>
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
        /// ��������Ԥ������ӿں���
        /// �����������ȷ���Ƿ��ϱ�������Ԥ��������
        /// </summary>
        /// <param name="owner">������</param>
        /// <param name="patient">����ʵ��</param>
        /// <param name="patientType">��������</param>
        /// <param name="diagName">�������</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <returns>FS.SOC.HISFC.DCP.Enum.ReportOperResult</returns>
        public FS.SOC.HISFC.DCP.Enum.ReportOperResult CheckDisease(System.Windows.Forms.IWin32Window owner, FS.HISFC.Models.RADT.Patient patient, FS.SOC.HISFC.DCP.Enum.PatientType patientType, string diagName, out string msg)
        {
            msg = "";
            string diseaseCode = "";

            if (FS.SOC.HISFC.Components.DCP.Classes.Function.CheckDiagNose(diagName, out diseaseCode))
            {
                //��Ҫ�ж��Ƿ��Ѿ���д��Ⱦ�����濨
               // System.Windows.Forms.MessageBox.Show("��ϣ�" + diagName + "Ϊ��Ⱦ����ϣ�����д��Ⱦ�����濨��", "��ʾ", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
               
                // {79888F16-C9EE-4eee-931D-86A8838DDF2D}�����жϴ�Ⱦ��ʱ��������
               DialogResult result = System.Windows.Forms.MessageBox.Show("��ϣ�" + diagName + "Ϊ��Ⱦ����ϣ�����д��Ⱦ�����濨��", "��ʾ", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);

             // {79888F16-C9EE-4eee-931D-86A8838DDF2D}�����жϴ�Ⱦ��ʱ��������

               if (result == DialogResult.No)
               {
                   bool isReport1 = true;
                   //System.Windows.Forms.MessageBox.Show("����д���������ɣ�", "��ʾ", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
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
               // {79888F16-C9EE-4eee-931D-86A8838DDF2D}�����жϴ�Ⱦ��ʱ��������
                
                if (result == DialogResult.Yes)
               {

                   bool isReport = true;
                   //������������
                   if (FS.SOC.HISFC.Components.DCP.Classes.Function.CheckPatientNeedReport(patient, diseaseCode, ref isReport))
                   {
                       //���ǿ�Ʊ���,��һ��Ҫ�󱣴�ɹ�����ܼ�������
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
                       frmReportManager.Text = frmReportManager.Text + "��" + diagName;
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
        /// ��ȡ��������Ԥ���ķ�����Ϣ
        /// </summary>
        /// <returns></returns>
        public int GetDCPNotice(System.Windows.Forms.IWin32Window owner, FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            //��ʾ���ϸ����Ŀ
            //סԺ��ʾ���ң�������ʾҽ��
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

                        msg = "���в��ϸ�Ĵ�Ⱦ�����濨����鿴���ϸ�ġ��˿�ԭ����������Ӧ�޸�\n" +
                                           "                  ��    �����ţ�" + report.ReportNO + "\n" +
                                           "                          ����������" + report.Patient.Name + "\n" +
                                           "                          �������ƣ�" + report.Disease.Name + "\n" +
                                           "                          �������ڣ�" + report.ReportTime.ToShortDateString() + "\n" +
                                           "\n" +
                                           "-----------------------------  ���ϸ񱨸��޸Ĳ���  -------------------------------\n" +
                                           "1����ҽ����������������Ⱦ�����桿��ť\n" +
                                           "2��ѡ���б��еĲ��ϸ񱨸濨���鿴���˿�ԭ��\n" +
                                           "3�����ա��˿�ԭ�򡱽����޸Ĳ������桿";

                        MessageBox.Show(owner, msg, "���ϸ񱨸濨��ʾ���������˿�ԭ��" + report.OperCase + "��", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                        //����ҽ������
                        if (string.IsNullOrEmpty(report.ReportDoctor.Name))
                        {
                            report.ReportDoctor = managerIntegrate.GetEmployeeInfo(report.ReportDoctor.ID);
                        }

                        if (report.ReportDoctor == null)
                        {
                            return 0;
                        }

                        msg = "���в��ϸ�Ĵ�Ⱦ�����濨����鿴���ϸ�ġ��˿�ԭ����������Ӧ�޸�\n" +
                                           "                  ��    �����ţ�" + report.ReportNO + "\n" +
                                           "                          ����������" + report.Patient.Name + "\n" +
                                           "                          �������ƣ�" + report.Disease.Name + "\n" +
                                           "                          �������ڣ�" + report.ReportTime.ToShortDateString() + "\n" +
                                           "                          ����ҽ����" + report.ReportDoctor.Name + "(" + report.ReportDoctor.ID + ")" + "\n" +
                                           "\n" +
                                           "-----------------------------  ���ϸ񱨸��޸Ĳ���  -------------------------------\n" +
                                           "1����ҽ����������������Ⱦ�����桿��ť\n" +
                                           "2��ѡ���б��еĲ��ϸ񱨸濨���鿴���˿�ԭ��\n" +
                                           "3�����ա��˿�ԭ�򡱽����޸Ĳ������桿";

                        MessageBox.Show(owner, msg, "���ϸ񱨸濨��ʾ���������˿�ԭ��" + report.OperCase + "��", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }


            }

            return 0;
        }

        #region ��������
        /// <summary>
        ///  ���������Ǽ�
        /// </summary>
        /// <param name="register">����Һ���Ϣ</param>
        /// <param name="owner">������</param>
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
        ///  ���������Ǽ�
        /// </summary>
        /// <param name="register">סԺ�Ǽ���Ϣ</param>
        /// <param name="owner">������</param>
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
        /// �ж��Ƿ�������
        /// </summary>
        /// <returns>True ������</returns>
        public bool AllowReport()
        {
            FS.SOC.HISFC.BizProcess.DCP.Common conMgr = new FS.SOC.HISFC.BizProcess.DCP.Common();
            FS.HISFC.Models.Base.Employee emplInfo = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            //��Ȩ�û�
            if (emplInfo.ID == "001406" || emplInfo.ID == "009999")
            {
                return true;
            }
            else
            {
                //��Ȩ����
                string deptCode = emplInfo.Dept.ID;
                ArrayList al = conMgr.QueryConstantList("NEEDREPORTDEPT");

                //��ʼ����ʱ����������Ҫά��������ȫԺ���ߺ���Բ�ά��
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
