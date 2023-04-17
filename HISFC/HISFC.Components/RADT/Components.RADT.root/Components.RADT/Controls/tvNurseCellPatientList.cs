using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;

namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: ��ʿվ�����б�]<br></br>
    /// ��ʿվͨ�ã��������ߣ������������ﻼ�ߡ�ת�뻼�ߡ�ת�����ߡ���Ժ���ߣ�
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class tvNurseCellPatientList : tvNursePatientList
    {
        public tvNurseCellPatientList():base("0")
        {
            InitializeComponent();
        }

        public tvNurseCellPatientList(IContainer container)
            : this()
        {
            container.Add(this);
        }

        /// <summary>
        /// ͷ������
        /// </summary>
        /// <param name="alInpatientNO"></param>
        public void RefreshImage(ArrayList alInpatientNO)
        {
            if (alInpatientNO != null && alInpatientNO.Count > 0)
            {
                bool isExist = false;
                Hashtable htInpatient = new Hashtable();
                foreach (string inpatienNO in alInpatientNO)
                {
                    Neusoft.HISFC.Models.RADT.Patient patientInfo = new Neusoft.HISFC.Models.RADT.Patient();
                    foreach (System.Windows.Forms.TreeNode treeNode in this.Nodes)
                    {
                        isExist = false;
                        foreach (System.Windows.Forms.TreeNode subTreeNode in treeNode.Nodes)
                        {
                            if (subTreeNode.Tag is Neusoft.HISFC.Models.RADT.PatientInfo)
                            {
                                patientInfo = subTreeNode.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;

                                if (patientInfo != null && patientInfo.ID == inpatienNO)
                                {
                                    if (subTreeNode.ImageIndex == this.BlankImageIndex)
                                    {
                                        switch (patientInfo.Sex.ID.ToString())
                                        {
                                            case "F":
                                                //��
                                                if (patientInfo.ID.IndexOf("B") > 0)
                                                    subTreeNode.ImageIndex = this.GirlImageIndex;	//Ӥ��Ů
                                                else
                                                    subTreeNode.ImageIndex = this.FemaleImageIndex;	//����Ů
                                                break;
                                            case "M":
                                                if (patientInfo.ID.IndexOf("B") > 0)
                                                    subTreeNode.ImageIndex = this.BabyImageIndex;	//Ӥ����
                                                else
                                                    subTreeNode.ImageIndex = this.MaleImageIndex;	//������
                                                break;
                                            default:
                                                subTreeNode.ImageIndex = this.MaleImageIndex;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        subTreeNode.ImageIndex = this.BlankImageIndex;
                                    }
                                    isExist = true;
                                    break;
                                }
                            }

                        }

                        if (isExist)
                        {
                            break;
                        }
                    }
                }
            }
        }

        #region �ɵ�����
        /*
        public tvNurseCellPatientList()
        {
            InitializeComponent();
            this.ShowType = enuShowType.Bed;
            this.Direction = enuShowDirection.Ahead;

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
            {
                return;
            }

            this.Refresh();
        }

        public tvNurseCellPatientList(IContainer container)
            : this()
        {
            container.Add(this);
        }

        Neusoft.HISFC.BizProcess.Integrate.RADT manager = null;
        Neusoft.HISFC.BizLogic.RADT.InPatient radtManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        private ArrayList depts = null;

        /// <summary>
        /// ���ݲ�����ȡ����
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        private ArrayList GetDepts(string nurseCode)
        {
            if (depts == null)
            {
                Neusoft.HISFC.BizProcess.Integrate.Manager m = new Neusoft.HISFC.BizProcess.Integrate.Manager();
                depts = m.QueryDepartment(nurseCode);
            }
            return depts;
        }

        /// <summary>
        /// ˢ��
        /// </summary>
        public new void Refresh()
        {
            this.BeginUpdate();
            this.Nodes.Clear();
            if (manager == null)
            {
                manager = new Neusoft.HISFC.BizProcess.Integrate.RADT();
            }

            ArrayList al = new ArrayList();//�����б�

            addPatientList(al, Neusoft.HISFC.Models.Base.EnumInState.I, EnumPatientType.In);

            //��ʾ���л����б�
            this.SetPatient(al);

            this.EndUpdate();
        }

        /// <summary>
        /// ���ݲ���վ�õ�����
        /// </summary>
        /// <param name="al"></param>
        private void addPatientList(ArrayList al, Neusoft.HISFC.Models.Base.EnumInState Status, EnumPatientType patientType)
        {
            ArrayList al1 = new ArrayList();

            Neusoft.HISFC.Models.Base.Employee employee = Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee;

            if (employee == null) 
                return;

            if (patientType == EnumPatientType.In) //������Ժ����
            {
                //��½�Ŀ����Ƿ��ǲ���
                //����һ�����Ҷ�Ӧ�������ʱ��employee.Nurse.ID����ǵ�½������Ϣ
                bool isNureseDept = true;

                al1 = this.radtManager.PatientQueryByNurseCell(employee.Nurse.ID, Status);

                ArrayList alDept = this.GetDepts(employee.Nurse.ID);

                //����һ�����Ҷ�Ӧ�������ʱ��employee.Nurse.ID����ǵ�½������Ϣ
                //��ʱ�����½���ң������ǲ��������ѯ��������
                if (al1.Count == 0)
                {
                    isNureseDept = false;
                    foreach (Neusoft.FrameWork.Models.NeuObject objdept in alDept)
                    {
                        al1.AddRange(this.radtManager.PatientQueryByNurseCell(objdept.ID, Status));
                    }
                }

                if (alDept == null || alDept.Count < 2)
                {
                    al.Add("��������|" + EnumPatientType.In.ToString());

                    al.AddRange(al1);
                }
                else
                {
                    Neusoft.FrameWork.Models.NeuObject objdept = null;
                    Neusoft.HISFC.Models.RADT.PatientInfo patientTemp = null;

                    for (int i = 0; i < alDept.Count; i++)
                    {
                        objdept = alDept[i] as Neusoft.FrameWork.Models.NeuObject;
                        al.Add(objdept.Name + "|" + EnumPatientType.In.ToString());

                        for (int j = 0; j < al1.Count; j++)
                        {
                            patientTemp = al1[j] as Neusoft.HISFC.Models.RADT.PatientInfo;

                            if (isNureseDept)
                            {
                                if (patientTemp.PVisit.PatientLocation.Dept.ID.Trim() == objdept.ID.Trim())
                                {
                                    al.Add(patientTemp);
                                }
                            }
                            else
                            {
                                if (patientTemp.PVisit.PatientLocation.NurseCell.ID.Trim() == objdept.ID.Trim())
                                {
                                    al.Add(patientTemp);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ArrayList alDept = this.GetDepts(employee.Nurse.ID);
                foreach (Neusoft.FrameWork.Models.NeuObject objDept in alDept)
                {
                    if (patientType == EnumPatientType.Arrive)
                    {
                        al1 = this.manager.QueryPatient(objDept.ID, Status);				//�����ҽ���
                    }
                    else if (patientType == EnumPatientType.ShiftOut)
                    {
                        al1 = this.radtManager.QueryPatientShiftOutApply(objDept.ID, "1");				//�����Ҳ�ת�������
                    }
                    else if (patientType == EnumPatientType.ShiftIn)
                    {
                        al1 = this.radtManager.QueryPatientShiftInApply(objDept.ID, "1");				//�����Ҳ�ת�������
                    }
                    else if (patientType == EnumPatientType.Out)
                    {
                        al1 = this.manager.QueryPatient(objDept.ID, Status);				//�����Ҳ��Ժ�Ǽǵ�
                    }
                    al.AddRange(al1);
                }
            }
        }

        /// <summary>
        /// ˢ��MQ�ļ�ʱ��Ϣ
        /// </summary>
        /// <param name="alInpatientNO"></param>
        public void RefreshMQ(ArrayList alInpatientNO)
        {
            if (alInpatientNO != null && alInpatientNO.Count > 0)
            {
                bool isExist = false;
                foreach (string inpatienNO in alInpatientNO)
                {
                    foreach (System.Windows.Forms.TreeNode treeNode in this.Nodes)
                    {
                        isExist = false;
                        foreach (System.Windows.Forms.TreeNode subTreeNode in treeNode.Nodes)
                        {
                            if (subTreeNode.Tag is Neusoft.HISFC.Models.RADT.PatientInfo)
                            {
                                if (((Neusoft.HISFC.Models.RADT.PatientInfo)subTreeNode.Tag).ID == inpatienNO)
                                {
                                    subTreeNode.BackColor = System.Drawing.Color.Chocolate;
                                    isExist = true;
                                    break;
                                }
                            }
                        }
                        if (isExist)
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ͷ������
        /// </summary>
        /// <param name="alInpatientNO"></param>
        public void RefreshImage(ArrayList alInpatientNO)
        {
            if (alInpatientNO != null && alInpatientNO.Count > 0)
            {
                bool isExist = false;
                Hashtable htInpatient = new Hashtable();
                foreach (string inpatienNO in alInpatientNO)
                {
                    Neusoft.HISFC.Models.RADT.Patient patientInfo = new Neusoft.HISFC.Models.RADT.Patient();
                    foreach (System.Windows.Forms.TreeNode treeNode in this.Nodes)
                    {
                        isExist = false;
                        foreach (System.Windows.Forms.TreeNode subTreeNode in treeNode.Nodes)
                        {
                            if (subTreeNode.Tag is Neusoft.HISFC.Models.RADT.PatientInfo)
                            {
                                patientInfo = subTreeNode.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;

                                if (patientInfo != null && patientInfo.ID == inpatienNO)
                                {
                                    if (subTreeNode.ImageIndex == this.BlankImageIndex)
                                    {
                                        switch (patientInfo.Sex.ID.ToString())
                                        {
                                            case "F":
                                                //��
                                                if (patientInfo.ID.IndexOf("B") > 0)
                                                    subTreeNode.ImageIndex = this.GirlImageIndex;	//Ӥ��Ů
                                                else
                                                    subTreeNode.ImageIndex = this.FemaleImageIndex;	//����Ů
                                                break;
                                            case "M":
                                                if (patientInfo.ID.IndexOf("B") > 0)
                                                    subTreeNode.ImageIndex = this.BabyImageIndex;	//Ӥ����
                                                else
                                                    subTreeNode.ImageIndex = this.MaleImageIndex;	//������
                                                break;
                                            default:
                                                subTreeNode.ImageIndex = this.MaleImageIndex;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        subTreeNode.ImageIndex = this.BlankImageIndex;
                                    }
                                    isExist = true;
                                    break;
                                }
                            }

                        }

                        if (isExist)
                        {
                            break;
                        }
                    }
                }
            }
        }
         * */
        #endregion
    }
}
