using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [功能描述: 医生站患者列表]<br></br>
    /// [创 建 者: wolf]<br></br>{9C8C5614-96E4-49d6-A3BF-74996194A63F}
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
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
        /// 是否显示分管患者列表
        /// </summary>
        private bool isShowInChargePatient = true;

        /// <summary>
        /// 是否显示本科室患者列表
        /// </summary>
        private bool isShowDeptPatient = true;

        /// <summary>
        /// 是否显示会诊患者列表
        /// </summary>
        private bool isShowConsultPatient = true;

        /// <summary>
        /// 是否显示授权患者列表
        /// </summary>
        private bool isShowAuthorizationPatient = true;

        /// <summary>
        /// 是否显示查找患者列表
        /// </summary>
        private bool isShowFindedPatient = true;

        /// <summary>
        /// 是否显示医疗组患者列表
        /// </summary>
        private bool isShowMedicalTeamsPatient = true;

        /// <summary>
        /// 显示的患者类别
        /// </summary>
        private string strShowPatientType = "-1";

        /// <summary>
        /// 临床路径
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
        /// 增加医疗组处理
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

            //节点说明

            #region 分管患者

            if (isShowInChargePatient)
            {
                al.Add("分管患者|patient");
                try
                {
                    ArrayList al1 = new ArrayList();

                    al1 = CacheManager.RadtIntegrate.QueryPatientByHouseDoc(empl, FS.HISFC.Models.Base.EnumInState.I, empl.Dept.ID);

                    foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
                    {
                        PatientInfo1.Memo = "科室";
                        al.Add(PatientInfo1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查找分管患者出错\n！" + ex.Message + CacheManager.RadtIntegrate.Err);

                }
            }

            #endregion

            #region 本科室患者
            if (isShowDeptPatient)
            {
                al.Add("本科室患者|DeptPatient");
                addPatientList(al, FS.HISFC.Models.Base.EnumInState.I);
            }
            #endregion

            #region 会诊患者

            if (isShowConsultPatient)
            {
                al.Add("会诊患者|ConsultationPatient");

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
                        PatientInfo1.Memo = "会诊";
                        al.Add(PatientInfo1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查找会诊患者出错\n！" + ex.Message + CacheManager.RadtIntegrate.Err);
                }
            }
            #endregion

            #region 授权患者

            if (isShowAuthorizationPatient)
            {
                al.Add("授权患者|PermissionPatient");

                try
                {
                    ArrayList al1 = new ArrayList();
                    al1 = CacheManager.RadtIntegrate.QueryPatientByPermission(FS.FrameWork.Management.Connection.Operator);
                    foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
                    {
                        PatientInfo1.Memo = "科室";
                        al.Add(PatientInfo1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查找授权患者出错\n！" + ex.Message + CacheManager.RadtIntegrate.Err);
                }
            }
            #endregion

            #region 查找患者

            if (isShowFindedPatient)
            {
                try
                {
                    al.Add("查找患者|QueryPatient");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查找患者出错\n！" + ex.Message + CacheManager.InterMgr.Err);
                }
            }

            #endregion

            #region 医疗组患者

            if (isShowMedicalTeamsPatient)
            {
                //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} 增加医疗组处理
                al.Add("医疗组内患者|TeamPatient");

                if (this.medicalTeamForDoctBizlogic == null)
                {
                    this.medicalTeamForDoctBizlogic = new FS.HISFC.BizLogic.Order.MedicalTeamForDoct();
                }

                List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct> medicalTeamForDoctList =
                    this.medicalTeamForDoctBizlogic.QueryQueryMedicalTeamForDoctInfo(empl.Dept.ID, empl.ID);
                if (medicalTeamForDoctList == null)
                {
                    MessageBox.Show("查询医疗组失败!\n" + this.medicalTeamForDoctBizlogic.Err);
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
        /// 根据医生工作站得到患者
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
                MessageBox.Show("查找科室患者出错\n！" + ex.Message + this.manager.Err);
            }
            foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
            {
                PatientInfo1.Memo = "科室";
            }
            al.AddRange(al1);
        }

        /// <summary>
        /// 根据医生工作站得到患者
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
                MessageBox.Show("查找科室患者出错\n！" + ex.Message + CacheManager.RadtIntegrate.Err);
            }
            foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
            {
                PatientInfo1.Memo = "科室";
            }
            al.AddRange(al1);
        }

        //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} 增加医疗组处理
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
                MessageBox.Show("查找科室患者出错\n！" + ex.Message + CacheManager.RadtIntegrate.Err);
            }
            foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo1 in al1)
            {
                PatientInfo1.Memo = "科室";
            }
            al.AddRange(al1);
        }

        private void iClinicPathPatient()
        {
            //先不考虑显示是否在路径中，后续有需求再说吧，这里刷新会导致比较慢
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
                                childNode.NodeFont = new System.Drawing.Font("宋体 ", 9, System.Drawing.FontStyle.Bold);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }


        #region 根据权限控制是否能查看全院患者
        //addby xuewj 2009-8-24 添加患者查询功能，根据权限控制是否能查看全院患者 {8B4B8C49-2181-4aeb-95D4-DADFDE26DBC2}

        /// <summary>
        /// 根据住院流水号查询患者信息
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
                MessageBox.Show("查询患者基本信息失败!\r\n" + CacheManager.RadtIntegrate.Err);
                return;
            }

            int returnValue = this.PreArrange(empl);

            int branch = -1;
            branch = GetBrach("QueryPatient");

            if (returnValue == -1)
            {
                //只能查看本科出院患者
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
                    MessageBox.Show("您没有权限查看其他科室的患者医嘱");
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
        ///// 根据住院流水号查询患者信息,用于处理患者终端费用 add by zhy
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
        //    //    MessageBox.Show("查询患者基本信息失败!");
        //    //    return;
        //    //}
        //}

        /// <summary>
        /// 权限判断
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
