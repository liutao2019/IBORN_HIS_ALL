using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ҽ��վ�����б�]<br></br>
    /// [�� �� ��: wolf]<br></br>{9C8C5614-96E4-49d6-A3BF-74996194A63F}
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class tvDoctorPatientList : FS.HISFC.Components.Common.Controls.tvPatientListByDoc
    {
        public tvDoctorPatientList()
        {
            if (DesignMode) return;
            //if (FS.FrameWork.Management.Connection.Instance == null) return;

            //if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
            //{
            //    return;
            //}

            try
            {
                this.ShowType = enuShowType.Bed;
                this.Direction = enuShowDirection.Ahead;

                GetShowList();

                this.RefreshInfo();

            }
            catch (Exception ex)
            {
                MessageBox.Show("tvDoctorPatientList" + ex.Message);
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�ֹܻ����б�
        /// </summary>
        private bool isShowInChargePatient = true;

        /// <summary>
        /// �Ƿ���ʾ�����һ����б�
        /// </summary>
        private bool isShowDeptPatient = true;

        /// <summary>
        /// �Ƿ���ʾ���ﻼ���б�
        /// </summary>
        private bool isShowConsultPatient = true;

        /// <summary>
        /// �Ƿ���ʾ��Ȩ�����б�
        /// </summary>
        private bool isShowAuthorizationPatient = true;

        /// <summary>
        /// �Ƿ���ʾ���һ����б�
        /// </summary>
        private bool isShowFindedPatient = true;

        /// <summary>
        /// �Ƿ���ʾҽ���黼���б�
        /// </summary>
        private bool isShowMedicalTeamsPatient = true;

        /// <summary>
        /// ��ʾ�Ļ������
        /// </summary>
        private string strShowPatientType = "-1";

        /// <summary>
        /// �ٴ�·��
        /// </summary>
        FS.HISFC.BizProcess.Interface.Common.IClinicPath iClinicPath = null;

        private int GetShowList()
        {
            if (strShowPatientType == "-1")
            {
                strShowPatientType = CacheManager.ContrlManager.GetControlParam<string>("HNZY36", false, "111111");
            }
            if (strShowPatientType.Length == 6)
            {
                isShowInChargePatient = FS.FrameWork.Function.NConvert.ToBoolean(strShowPatientType.Substring(0, 1));
                isShowDeptPatient = FS.FrameWork.Function.NConvert.ToBoolean(strShowPatientType.Substring(1, 1));
                isShowConsultPatient = FS.FrameWork.Function.NConvert.ToBoolean(strShowPatientType.Substring(2, 1));
                isShowAuthorizationPatient = FS.FrameWork.Function.NConvert.ToBoolean(strShowPatientType.Substring(3, 1));
                isShowFindedPatient = FS.FrameWork.Function.NConvert.ToBoolean(strShowPatientType.Substring(4, 1));
                isShowMedicalTeamsPatient = FS.FrameWork.Function.NConvert.ToBoolean(strShowPatientType.Substring(5, 1));
            }

            return 1;
        }


        /// <summary>
        /// ����ҽ���鴦��
        /// </summary> 
        public FS.HISFC.BizLogic.Order.MedicalTeamForDoct medicalTeamForDoctBizlogic = null;

        FS.HISFC.BizProcess.Integrate.RADT manager = null;

        public virtual void RefreshInfo()
        {
            FS.HISFC.Models.Base.Employee per = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            RefreshInfo(per);
        }

        public virtual void RefreshInfo(FS.HISFC.Models.Base.Employee empl)
        {
            this.Nodes.Clear();

            ArrayList al = new ArrayList();

            //�ڵ�˵��

            #region �ֹܻ���

            if (isShowInChargePatient)
            {
                al.Add("�ֹܻ���|patient");
                try
                {
                    ArrayList al1 = new ArrayList();

                    al1 = CacheManager.RadtIntegrate.QueryPatientByHouseDoc(empl, FS.HISFC.Models.Base.EnumInState.I, empl.Dept.ID);

                    foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
                    {
                        PatientInfo1.Memo = "����";
                        al.Add(PatientInfo1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("���ҷֹܻ��߳���\n��" + ex.Message + CacheManager.RadtIntegrate.Err);

                }
            }

            #endregion

            #region �����һ���
            if (isShowDeptPatient)
            {
                al.Add("�����һ���|DeptPatient");
                addPatientList(al, FS.HISFC.Models.Base.EnumInState.I);
            }
            #endregion

            #region ���ﻼ��

            if (isShowConsultPatient)
            {
                al.Add("���ﻼ��|ConsultationPatient");

                try
                {
                    ArrayList al1 = new ArrayList();
                    FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();
                    DateTime dt = dbManager.GetDateTimeFromSysDateTime();
                    DateTime dt1 = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
                    DateTime dt2 = new DateTime(dt.Year, dt.AddDays(1).Month, dt.AddDays(1).Day, 0, 0, 0, 0);
                    al1 = CacheManager.RadtIntegrate.QueryPatientByConsultation(dbManager.Operator, dt1, dt2, empl.Dept.ID);
                    foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
                    {
                        PatientInfo1.Memo = "����";
                        al.Add(PatientInfo1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("���һ��ﻼ�߳���\n��" + ex.Message + CacheManager.RadtIntegrate.Err);
                }
            }
            #endregion

            #region ��Ȩ����

            if (isShowAuthorizationPatient)
            {
                al.Add("��Ȩ����|PermissionPatient");

                try
                {
                    ArrayList al1 = new ArrayList();
                    al1 = CacheManager.RadtIntegrate.QueryPatientByPermission(FS.FrameWork.Management.Connection.Operator);
                    foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
                    {
                        PatientInfo1.Memo = "����";
                        al.Add(PatientInfo1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("������Ȩ���߳���\n��" + ex.Message + CacheManager.RadtIntegrate.Err);
                }
            }
            #endregion

            #region ���һ���

            if (isShowFindedPatient)
            {
                try
                {
                    al.Add("���һ���|QueryPatient");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("���һ��߳���\n��" + ex.Message + CacheManager.InterMgr.Err);
                }
            }

            #endregion

            #region ҽ���黼��

            if (isShowMedicalTeamsPatient)
            {
                //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
                al.Add("ҽ�����ڻ���|TeamPatient");

                if (this.medicalTeamForDoctBizlogic == null)
                {
                    this.medicalTeamForDoctBizlogic = new FS.HISFC.BizLogic.Order.MedicalTeamForDoct();
                }

                List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct> medicalTeamForDoctList =
                    this.medicalTeamForDoctBizlogic.QueryQueryMedicalTeamForDoctInfo(empl.Dept.ID, empl.ID);
                if (medicalTeamForDoctList == null)
                {
                    MessageBox.Show("��ѯҽ����ʧ��!\n" + this.medicalTeamForDoctBizlogic.Err);
                }

                if (medicalTeamForDoctList.Count > 0)
                {
                    FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct medcialObj = medicalTeamForDoctList[0];

                    addPatientListMedialTeam(al, FS.HISFC.Models.Base.EnumInState.I, medcialObj.MedcicalTeam.ID);
                }
            }

            #endregion

            this.SetPatient(al);

            this.iClinicPathPatient();
        }

        /// <summary>
        /// ����ҽ������վ�õ�����
        /// </summary>
        /// <param name="al"></param>
        public virtual void addPatientList(ArrayList al, FS.HISFC.Models.Base.EnumInState Status, FS.HISFC.Models.Base.Employee empl)
        {
            ArrayList al1 = new ArrayList();
            try
            {
                al1 = this.manager.QueryPatient(empl.Dept.ID, Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show("���ҿ��һ��߳���\n��" + ex.Message + this.manager.Err);
            }
            foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
            {
                PatientInfo1.Memo = "����";
            }
            al.AddRange(al1);
        }

        /// <summary>
        /// ����ҽ������վ�õ�����
        /// </summary>
        /// <param name="al"></param>
        public virtual void addPatientList(ArrayList al, FS.HISFC.Models.Base.EnumInState Status)
        {
            ArrayList al1 = new ArrayList();
            try
            {
                FS.HISFC.Models.Base.Employee per = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                al1 = CacheManager.RadtIntegrate.QueryPatient(per.Dept.ID, Status);
            }
            catch (Exception ex)
            {
                MessageBox.Show("���ҿ��һ��߳���\n��" + ex.Message + CacheManager.RadtIntegrate.Err);
            }
            foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
            {
                PatientInfo1.Memo = "����";
            }
            al.AddRange(al1);
        }

        //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
        public virtual void addPatientListMedialTeam(ArrayList al, FS.HISFC.Models.Base.EnumInState Status, string medcialTeamCode)
        {
            ArrayList al1 = new ArrayList();
            try
            {
                FS.HISFC.Models.Base.Employee per = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                al1 = CacheManager.RadtIntegrate.PatientQueryByMedicalTeam(medcialTeamCode, Status, per.Dept.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("���ҿ��һ��߳���\n��" + ex.Message + CacheManager.RadtIntegrate.Err);
            }
            foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
            {
                PatientInfo1.Memo = "����";
            }
            al.AddRange(al1);
        }

        private void iClinicPathPatient()
        {
            //�Ȳ�������ʾ�Ƿ���·���У�������������˵�ɣ�����ˢ�»ᵼ�±Ƚ���
            if (this.iClinicPath == null)
            {
                iClinicPath = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IClinicPath))
               as FS.HISFC.BizProcess.Interface.Common.IClinicPath;
            } 
            if (iClinicPath != null)
            {
                foreach (TreeNode nodes in this.Nodes)
                {
                    foreach (TreeNode childNode in nodes.Nodes)
                    {
                        try
                        {
                            FS.HISFC.Models.RADT.PatientInfo patient = childNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                            if (iClinicPath.PatientIsSelectedPath(patient.ID))
                            {
                                childNode.NodeFont = new System.Drawing.Font("���� ", 9, System.Drawing.FontStyle.Bold);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }


        #region ����Ȩ�޿����Ƿ��ܲ鿴ȫԺ����
        //addby xuewj 2009-8-24 ��ӻ��߲�ѯ���ܣ�����Ȩ�޿����Ƿ��ܲ鿴ȫԺ���� {8B4B8C49-2181-4aeb-95D4-DADFDE26DBC2}

        /// <summary>
        /// ����סԺ��ˮ�Ų�ѯ������Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        public void QueryPaitent(string inpatientNO, FS.HISFC.Models.Base.Employee empl)
        {
            if (inpatientNO == "")
            {
                return;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = CacheManager.RadtIntegrate.QueryPatientInfoByInpatientNO(inpatientNO);

            if (patientInfo == null)
            {
                MessageBox.Show("��ѯ���߻�����Ϣʧ��!\r\n" + CacheManager.RadtIntegrate.Err);
                return;
            }

            int returnValue = this.PreArrange(empl);

            int branch = -1;
            branch = GetBrach("QueryPatient");

            if (returnValue == -1)
            {
                //ֻ�ܲ鿴���Ƴ�Ժ����
                if (patientInfo.PVisit.PatientLocation.Dept.ID == empl.Dept.ID)
                //&& (patientInfo.PVisit.InState.ID.ToString() == "P"
                //|| patientInfo.PVisit.InState.ID.ToString() == "D"
                //|| patientInfo.PVisit.InState.ID.ToString() == "T"))
                {
                    this.Nodes[branch].Nodes.Clear();
                    this.AddTreeNode(branch, patientInfo);
                    this.SelectedNode = this.Nodes[branch].Nodes[0];
                }
                else
                {
                    MessageBox.Show("��û��Ȩ�޲鿴�������ҵĻ���ҽ��");
                }
            }
            else
            {
                this.Nodes[branch].Nodes.Clear();
                this.AddTreeNode(branch, patientInfo);
                this.SelectedNode = this.Nodes[branch].Nodes[0];
            }
        }

        ///// <summary>
        ///// ����סԺ��ˮ�Ų�ѯ������Ϣ,���ڴ������ն˷��� add by zhy
        ///// </summary>
        ///// <param name="patientInfo"></param>
        //public void QueryPaitentTerminalFee(string inpatientNO)
        //{
        //    if (inpatientNO == "")
        //    {
        //        return;
        //    }

        //    FS.HISFC.Models.RADT.PatientInfo patientInfo = CacheManager.InterMgr.QueryPatientTerminalFeeByInpatientNO(inpatientNO);

        //    //if (patientInfo == null)
        //    //{
        //    //    MessageBox.Show("��ѯ���߻�����Ϣʧ��!");
        //    //    return;
        //    //}
        //}

        /// <summary>
        /// Ȩ���ж�
        /// </summary>
        /// <returns></returns>
        private int PreArrange(FS.HISFC.Models.Base.Employee empl)
        {
            if (FS.HISFC.Components.Common.Classes.Function.ChoosePiv("0001") == false)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// get selectedNode's index
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public int GetBrach(string tag)
        {
            int branch = -1;
            foreach (TreeNode treeNode in this.Nodes)
            {
                if (treeNode.Tag != null && treeNode.Tag.ToString() == tag)
                {
                    branch = treeNode.Index;
                    break;
                }
            }
            return branch;
        }

        #endregion

    }
}
