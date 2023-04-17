using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;

namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [功能描述: 护士站患者列表]<br></br>
    /// 护士站通用：本区患者，不含（待接诊患者、转入患者、转出患者、出院患者）
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
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
        /// 头像闪动
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
                                                //男
                                                if (patientInfo.ID.IndexOf("B") > 0)
                                                    subTreeNode.ImageIndex = this.GirlImageIndex;	//婴儿女
                                                else
                                                    subTreeNode.ImageIndex = this.FemaleImageIndex;	//成年女
                                                break;
                                            case "M":
                                                if (patientInfo.ID.IndexOf("B") > 0)
                                                    subTreeNode.ImageIndex = this.BabyImageIndex;	//婴儿男
                                                else
                                                    subTreeNode.ImageIndex = this.MaleImageIndex;	//成年男
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

        #region 旧的作废
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
        /// 根据病区获取科室
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
        /// 刷新
        /// </summary>
        public new void Refresh()
        {
            this.BeginUpdate();
            this.Nodes.Clear();
            if (manager == null)
            {
                manager = new Neusoft.HISFC.BizProcess.Integrate.RADT();
            }

            ArrayList al = new ArrayList();//患者列表

            addPatientList(al, Neusoft.HISFC.Models.Base.EnumInState.I, EnumPatientType.In);

            //显示所有患者列表
            this.SetPatient(al);

            this.EndUpdate();
        }

        /// <summary>
        /// 根据病区站得到患者
        /// </summary>
        /// <param name="al"></param>
        private void addPatientList(ArrayList al, Neusoft.HISFC.Models.Base.EnumInState Status, EnumPatientType patientType)
        {
            ArrayList al1 = new ArrayList();

            Neusoft.HISFC.Models.Base.Employee employee = Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee;

            if (employee == null) 
                return;

            if (patientType == EnumPatientType.In) //本区在院患者
            {
                //登陆的科室是否是病区
                //存在一个科室对应多个病区时，employee.Nurse.ID存的是登陆科室信息
                bool isNureseDept = true;

                al1 = this.radtManager.PatientQueryByNurseCell(employee.Nurse.ID, Status);

                ArrayList alDept = this.GetDepts(employee.Nurse.ID);

                //存在一个科室对应多个病区时，employee.Nurse.ID存的是登陆科室信息
                //此时如果登陆科室，而不是病区，则查询不到患者
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
                    al.Add("本区患者|" + EnumPatientType.In.ToString());

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
                        al1 = this.manager.QueryPatient(objDept.ID, Status);				//按科室接珍
                    }
                    else if (patientType == EnumPatientType.ShiftOut)
                    {
                        al1 = this.radtManager.QueryPatientShiftOutApply(objDept.ID, "1");				//按科室查转出申请的
                    }
                    else if (patientType == EnumPatientType.ShiftIn)
                    {
                        al1 = this.radtManager.QueryPatientShiftInApply(objDept.ID, "1");				//按科室查转入申请的
                    }
                    else if (patientType == EnumPatientType.Out)
                    {
                        al1 = this.manager.QueryPatient(objDept.ID, Status);				//按科室查出院登记的
                    }
                    al.AddRange(al1);
                }
            }
        }

        /// <summary>
        /// 刷新MQ的即时消息
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
        /// 头像闪动
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
                                                //男
                                                if (patientInfo.ID.IndexOf("B") > 0)
                                                    subTreeNode.ImageIndex = this.GirlImageIndex;	//婴儿女
                                                else
                                                    subTreeNode.ImageIndex = this.FemaleImageIndex;	//成年女
                                                break;
                                            case "M":
                                                if (patientInfo.ID.IndexOf("B") > 0)
                                                    subTreeNode.ImageIndex = this.BabyImageIndex;	//婴儿男
                                                else
                                                    subTreeNode.ImageIndex = this.MaleImageIndex;	//成年男
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
