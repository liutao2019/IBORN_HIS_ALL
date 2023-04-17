using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using FS.HISFC.Components.Common.Controls;
using FS.HISFC.Models.HealthRecord.EnumServer;
using FarPoint.Win.Spread;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// {4097D044-6360-4895-BFFB-D6E4BBF5AE74}
    /// </summary>
    public partial class ucOutPatientCaseQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOutPatientCaseQuery()
        {
            InitializeComponent();
            this.Clear();
            InitEvents();
        }

        private void ucModifyOutPatientHealthInfo1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 诊断类别
        /// </summary>
        private ArrayList diagnoseType = new ArrayList();

        /// <summary>
        /// 诊断列表
        /// </summary>
        public ArrayList alDiag = new ArrayList();

        /// <summary>
        /// 科室常用诊断
        /// </summary>
        ArrayList alDeptDiag = new ArrayList();

        /// <summary>
        /// 全部诊断
        /// </summary>
        ArrayList alAllDiag = new ArrayList();

        /// <summary>
        /// 患者住院号或门诊号
        /// </summary>
        private string patientId = "";



        /// <summary>
        /// 诊断类别
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper diagnoseTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 诊断级别帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper levelHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 诊断分期帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper periodHelper = new FS.FrameWork.Public.ObjectHelper();


        /// <summary>
        /// 历史病历实体
        /// </summary>
        private FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;

        /// <summary>
        /// 数据库管理类
        /// </summary>
        private FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();


        /// <summary>
        /// 患者住院号或门诊号
        /// </summary>
        public string PatientId
        {
            get { return patientId; }

            set
            {
                this.patientId = value;
                if (!string.IsNullOrEmpty(value) && this.regObj != null)
                {
                    FS.HISFC.Models.Registration.Register regTemp = FS.HISFC.Components.Order.CacheManager.RegMgr.GetByClinic(value);

                    if (regTemp != null && !string.IsNullOrEmpty(regTemp.ID))
                    {
                        this.regObj = regTemp;
                    }

                    ShowPatientInfo();
                }
            }
        }

        /// <summary>
        /// 当前患者信息
        /// </summary>
        FS.HISFC.Models.Registration.Register regObj = null;

        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 患者挂号时实体
        /// </summary>
        public FS.HISFC.Models.Registration.Register RegObj
        {
            get
            {
                return regObj;
            }
            set
            {
                regObj = value;
                this.PatientId = regObj.ID;
            }
        }

        #region 事件处理
        void InitEvents()
        {
            this.tvHistoryCase.AfterSelect += new TreeViewEventHandler(tvHistoryCase_AfterSelect);

        }

        #endregion

        /// <summary>
        /// 选择患者赋值
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.Clear();

            if (neuObject != null && neuObject.GetType() == typeof(FS.HISFC.Models.Registration.Register))
            {
                this.regObj = neuObject as FS.HISFC.Models.Registration.Register;
                this.PatientId = this.regObj.ID;
            }
            return 1;
        }


        private void tvHistoryCase_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvHistoryCase.SelectedNode != null && this.tvHistoryCase.SelectedNode.Tag != null)
            {
                FS.FrameWork.Models.NeuObject obj = this.tvHistoryCase.SelectedNode.Tag as FS.FrameWork.Models.NeuObject;
                if (obj == null)
                {
                    return;
                }

                try
                {
                    this.alDiag = FS.HISFC.Components.Order.CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(obj.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("获得患者的诊断信息出错！" + ex.Message, "提示");
                    return;
                }
                if (this.alDiag == null)
                {
                    return;
                }

                //如果为空，重取，否则下面出异常
                if (this.diagnoseTypeHelper.ArrayObject.Count <= 0)
                {
                    this.diagnoseTypeHelper.ArrayObject = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
                }

                //清空
                if (this.fpDiagnose_Sheet1.Rows.Count > 0)
                {
                    this.fpDiagnose_Sheet1.Rows.Remove(0, this.fpDiagnose_Sheet1.Rows.Count);
                }
                foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in this.alDiag)
                {
                    this.fpDiagnose_Sheet1.Rows.Add(0, 1);
                    if (!diag.IsValid)
                    {
                        this.fpDiagnose_Sheet1.Rows[0].ForeColor = Color.Red;
                    }

                    this.fpDiagnose_Sheet1.Cells[0, 0].Text = this.diagnoseTypeHelper.GetObjectFromID(diag.DiagInfo.DiagType.ID).Name;
                    this.fpDiagnose_Sheet1.Cells[0, 1].Value = diag.DiagInfo.IsMain;//是否描述
                    this.fpDiagnose_Sheet1.Cells[0, 2].Text = diag.DiagInfo.ICD10.ID;//icd码
                    this.fpDiagnose_Sheet1.Cells[0, 3].Text = diag.DiagInfo.ICD10.Name;//icd名称
                    if (diag.DiagInfo.ICD10.ID == "MS999")
                    {
                        this.fpDiagnose_Sheet1.Cells[0, 3].Locked = false;
                        this.fpDiagnose_Sheet1.Cells[0, 2].Locked = true;
                    }
                    else
                    {
                        this.fpDiagnose_Sheet1.Cells[0, 2].Locked = false;
                        this.fpDiagnose_Sheet1.Cells[0, 3].Locked = true;
                    }
                    this.fpDiagnose_Sheet1.Cells[0, 4].Value = FS.FrameWork.Function.NConvert.ToBoolean(diag.DubDiagFlag);//是否疑诊
                    this.fpDiagnose_Sheet1.Cells[0, 5].Value = FS.FrameWork.Function.NConvert.ToBoolean(diag.Diagnosis_flag);//是否初诊
                    this.fpDiagnose_Sheet1.Cells[0, 6].Text = diag.DiagInfo.DiagDate.Date.ToShortDateString();//日期
                    this.fpDiagnose_Sheet1.Cells[0, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.fpDiagnose_Sheet1.Cells[0, 7].Text = diag.DiagInfo.Doctor.ID;//代码
                    this.fpDiagnose_Sheet1.Cells[0, 8].Text = diag.DiagInfo.Doctor.Name;//诊断医生
                    if (this.periodHelper.GetObjectFromID(diag.PeriorCode) != null)
                    {
                        this.fpDiagnose_Sheet1.Cells[0, 9].Text = this.periodHelper.GetObjectFromID(diag.PeriorCode).Name;
                    }
                    if (this.levelHelper.GetObjectFromID(diag.LevelCode) != null)
                    {
                        this.fpDiagnose_Sheet1.Cells[0, 10].Text = this.levelHelper.GetObjectFromID(diag.LevelCode).Name;
                    }
                    this.fpDiagnose_Sheet1.Rows[0].Tag = diag;

                    this.fpDiagnose_Sheet1.Rows[0].Locked = true;
                    this.fpDiagnose_Sheet1.Cells[0, 3].Locked = true;
                }

                this.SetCaseHistory(obj.ID, obj.User01);
            }
        }

        private void SetCaseHistory(string regId, string operTime)
        {
            FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistorytmp = FS.HISFC.Components.Order.CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(regId, operTime);//.SelectCaseHistory(regId);
            if (caseHistorytmp != null)
            {
                this.tbChiefComplaint.Text = caseHistorytmp.CaseMain;//主诉
                this.tbPresentIllness.Text = caseHistorytmp.CaseNow;//现病史
                this.tbPastHistory.Text = caseHistorytmp.CaseOld;//既往史
                this.tbPhysicalExam.Text = caseHistorytmp.CheckBody;//查体
                this.tbMemo.Text = caseHistorytmp.Memo;//备注
                this.tbAllergicHistory.Text = caseHistorytmp.CaseAllery;//过敏史
                this.tbTreatment.Text = caseHistorytmp.User01;//处理
                this.lblDept.Text = caseHistorytmp.DeptID.PadRight(10, '_');//科室

                this.tbmemo2.Text = caseHistorytmp.Memo2;//


            }

            this.tbChiefComplaint.Focus();
        }


        #region 信息显示
        private void ShowPatientInfo()
        {
            this.lblCardNO.Text = this.regObj.PID.CardNO.PadRight(10, '_');
            this.lblName.Text = this.regObj.Name.PadRight(10, '_');
            this.lblSex.Text = Language.Msg(this.regObj.Sex.Name).PadRight(10, '_');
            this.lblAge.Text = regMgr.GetAge(this.regObj.Birthday);
            this.lblDept.Text = this.regObj.DoctorInfo.Templet.Dept.Name.PadRight(10, '_');

            try
            {
                this.alDiag = FS.HISFC.Components.Order.CacheManager.DiagMgr.QueryCaseDiagnoseForClinicByState(this.patientId, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("获得患者的诊断信息出错！" + ex.Message, "提示");
                return;
            }
            if (this.alDiag == null)
            {
                return;
            }

            //如果为空，重取，否则下面出异常
            if (this.diagnoseTypeHelper.ArrayObject.Count <= 0)
            {
                this.diagnoseTypeHelper.ArrayObject = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
            }

            //清空
            if (this.fpDiagnose_Sheet1.Rows.Count > 0)
            {
                this.fpDiagnose_Sheet1.Rows.Remove(0, this.fpDiagnose_Sheet1.Rows.Count);
            }
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in this.alDiag)
            {
                this.fpDiagnose_Sheet1.Rows.Add(0, 1);
                if (!diag.IsValid)
                {
                    this.fpDiagnose_Sheet1.Rows[0].ForeColor = Color.Red;
                }

                this.fpDiagnose_Sheet1.Cells[0, 0].Text = this.diagnoseTypeHelper.GetObjectFromID(diag.DiagInfo.DiagType.ID).Name;
                this.fpDiagnose_Sheet1.Cells[0, 1].Value = diag.DiagInfo.IsMain;//是否描述
                this.fpDiagnose_Sheet1.Cells[0, 2].Text = diag.DiagInfo.ICD10.ID;//icd码
                this.fpDiagnose_Sheet1.Cells[0, 3].Text = diag.DiagInfo.ICD10.Name;//icd名称
                if (diag.DiagInfo.ICD10.ID == "MS999")
                {
                    this.fpDiagnose_Sheet1.Cells[0, 3].Locked = false;
                    this.fpDiagnose_Sheet1.Cells[0, 2].Locked = true;
                }
                else
                {
                    this.fpDiagnose_Sheet1.Cells[0, 2].Locked = false;
                    this.fpDiagnose_Sheet1.Cells[0, 3].Locked = true;
                }
                this.fpDiagnose_Sheet1.Cells[0, 4].Value = FS.FrameWork.Function.NConvert.ToBoolean(diag.DubDiagFlag);//是否疑诊
                this.fpDiagnose_Sheet1.Cells[0, 5].Value = FS.FrameWork.Function.NConvert.ToBoolean(diag.Diagnosis_flag);//是否初诊
                this.fpDiagnose_Sheet1.Cells[0, 6].Text = diag.DiagInfo.DiagDate.Date.ToShortDateString();//日期
                this.fpDiagnose_Sheet1.Cells[0, 7].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.fpDiagnose_Sheet1.Cells[0, 7].Text = diag.DiagInfo.Doctor.ID;//代码
                this.fpDiagnose_Sheet1.Cells[0, 8].Text = diag.DiagInfo.Doctor.Name;//诊断医生
                if (this.periodHelper.GetObjectFromID(diag.PeriorCode) != null)
                {
                    this.fpDiagnose_Sheet1.Cells[0, 9].Text = this.periodHelper.GetObjectFromID(diag.PeriorCode).Name;
                }
                if (this.levelHelper.GetObjectFromID(diag.LevelCode) != null)
                {
                    this.fpDiagnose_Sheet1.Cells[0, 10].Text = this.levelHelper.GetObjectFromID(diag.LevelCode).Name;
                }
                this.fpDiagnose_Sheet1.Rows[0].Tag = diag;

                this.fpDiagnose_Sheet1.Rows[0].Locked = true;
                this.fpDiagnose_Sheet1.Cells[0, 3].Locked = true;
            }

            //加载历史病历
            InitTreeCase();

        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            this.regObj = null;
            this.lblCardNO.Text = "";
            this.lblName.Text = "";
            this.lblSex.Text = "";
            this.lblAge.Text = "";
            this.lblDept.Text = "";
            this.tbChiefComplaint.Text = "";
            this.tbPresentIllness.Text = "";
            this.tbPastHistory.Text = "";
            this.tbAllergicHistory.Text = "";
            this.tbPhysicalExam.Text = "";
            this.tbTreatment.Text = "";
            this.tbMemo.Text = "";
            this.fpDiagnose_Sheet1.RowCount = 1;// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}

            this.tvHistoryCase.Nodes.Clear();
            this.tbmemo2.Text = "";
            this.fpDiagnose_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 初始化历史病历
        /// </summary>
        public void InitTreeCase()
        {

            ArrayList al = new ArrayList();
            try
            {
                if (this.regObj == null || string.IsNullOrEmpty(this.regObj.PID.CardNO))
                {
                    return;
                }

                if (this.tvHistoryCase.Nodes.Count > 0)
                {
                    this.tvHistoryCase.Nodes.Clear();
                }

                TreeNode root = new TreeNode();
                root.Text = Language.Msg("历史病历");
                root.ImageIndex = 1;
                root.SelectedImageIndex = 1;
                root.Tag = null;
                this.tvHistoryCase.Nodes.Add(root);
                al = CacheManager.OutOrderMgr.QueryAllCaseHistory(this.regObj.PID.CardNO);
                //al.Sort(new myRegCompareByRegTime());
                if (al == null || al.Count < 0)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < al.Count; i++)
                    {

                        FS.FrameWork.Models.NeuObject obj = al[i] as FS.FrameWork.Models.NeuObject;
                        if (obj == null)
                        {
                            continue;
                        }

                        TreeNode node = new TreeNode();
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 4;
                        node.Tag = obj;
                        if (obj.Memo != null)
                        {
                            node.Text = obj.Name + "[" + FS.FrameWork.Function.NConvert.ToDateTime(obj.User01).ToString() + "]";
                        }
                        else
                        {
                            node.Text = obj.Name + "[" + obj.ID + "]";
                        }
                        root.Nodes.Add(node);
                    }

                    //获取当前id最新的病历
                    this.SetCaseHistory(this.regObj.ID);

                    this.tvHistoryCase.ExpandAll();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "获得历史模板出错！");
                return;
            }
        }

        /// <summary>
        /// 设置显示
        /// </summary>
        /// <param name="regId"></param>
        private void SetCaseHistory(string regId)
        {
            this.caseHistory = CacheManager.OutOrderMgr.QueryCaseHistoryByClinicCode(regId);
            if (this.caseHistory != null)
            {
                this.tbChiefComplaint.Text = caseHistory.CaseMain;//主诉
                this.tbPresentIllness.Text = caseHistory.CaseNow;//现病史
                this.tbPastHistory.Text = caseHistory.CaseOld;//既往史
                this.tbPhysicalExam.Text = caseHistory.CheckBody;//查体
                this.tbMemo.Text = caseHistory.Memo;//备注
                this.tbAllergicHistory.Text = caseHistory.CaseAllery;//过敏史
                this.tbTreatment.Text = caseHistory.User01;// 
                this.lblDept.Text = caseHistory.DeptID.PadRight(10, '_');//科室

                this.tbmemo2.Text = caseHistory.Memo2;// //
            }
            else
            {
                this.tbChiefComplaint.Text = "";//主诉
                this.tbPresentIllness.Text = "";//现病史
                this.tbPastHistory.Text = "";//既往史
                this.tbPhysicalExam.Text = "";//查体
                this.tbMemo.Text = "";//备注
                this.tbAllergicHistory.Text = "";//过敏史
                this.tbTreatment.Text = "";//处理//
            }
            this.tbChiefComplaint.Focus();
        }


        public class myRegCompareByRegTime : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                DateTime reg1 = Convert.ToDateTime(((FS.FrameWork.Models.NeuObject)x).Memo);
                DateTime reg2 = Convert.ToDateTime(((FS.FrameWork.Models.NeuObject)y).Memo);
                return reg1.CompareTo(reg2);

                //if(reg1>=reg2)
                //{
                //return 1;
                //}
                //else
                //{
                //return 0;
                //}
                //return ((FS.HISFC.Models.Registration.Register)x).DoctorInfo.SeeDate.CompareTo(((FS.HISFC.Models.Registration.Register)y).DoctorInfo.SeeDate);

            }
        }
        #endregion

    }
}



