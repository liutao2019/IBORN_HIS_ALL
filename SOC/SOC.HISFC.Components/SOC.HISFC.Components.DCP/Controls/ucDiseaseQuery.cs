using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.Components.DCP.Classes;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// ucPatientInfo<br></br>
    /// [功能描述: 查询面板uc]<br></br>
    /// [创 建 者: zj]<br></br>
    /// [创建时间: 2008-09-17]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucDiseaseQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDiseaseQuery()
        {
            InitializeComponent();
            this.tvReport.ImageList = this.tvReport.groupImageList;
            this.tvReport.AfterSelect+=new TreeViewEventHandler(tvReport_AfterSelect);
        }

        #region 域变量

        

        /// <summary>
        /// 查询患者信息的类型（默认为门诊卡号）
        /// </summary>
        private FS.SOC.HISFC.DCP.Enum.PatientType llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;

        /// <summary>
        /// 报告卡管理类
        /// </summary>
        private FS.SOC.HISFC.BizLogic.DCP.DiseaseReport diseaseMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();

        /// <summary>
        /// 住院患者管理类
        /// </summary>
        private FS.SOC.HISFC.BizProcess.DCP.Patient patientProcess = new FS.SOC.HISFC.BizProcess.DCP.Patient();

        /// <summary>
        /// 选择节点委托
        /// </summary>
        /// <param name="patient"></param>
        public delegate void SelectNode(FS.FrameWork.Models.NeuObject obj);

        /// <summary>
        /// 显示事件
        /// </summary>
        public event SelectNode ShowInfo;

        /// <summary>
        /// 报告集合
        /// </summary>
        private ArrayList alReport = new ArrayList();

        #endregion

        #region 属性

        /// <summary>
        /// 操作患者的类型（默认为其他）
        /// </summary>
        private FS.SOC.HISFC.DCP.Enum.PatientType patientType = FS.SOC.HISFC.DCP.Enum.PatientType.O;
        /// <summary>
        /// 操作患者的类型
        /// </summary>
        public FS.SOC.HISFC.DCP.Enum.PatientType PatientType
        {
            get
            {
                return this.patientType;
            }
            set
            {
                this.patientType = value;
            }
        }

        /// <summary>
        /// 报告卡[在保健科审核时窗口show之前赋值]
        /// </summary>
        public ArrayList AlReport
        {
            get
            {
                return alReport;
            }
            set
            {
                alReport = value;
            }
        }

        /// <summary>
        /// 设置患者查询的默认天数
        /// </summary>
        private int days = 5;
        public int Days
        {
            get
            {
                return days;
            }
            set
            {
                this.days = value;
            }
        }


        /// <summary>
        /// 患者实体
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = null;

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        private FS.HISFC.DCP.Object.CommonReport commonReport =null;

        public FS.HISFC.DCP.Object.CommonReport CommonReport
        {
            get
            {
                return this.commonReport;
            }
            set
            {
                this.commonReport = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询内容初始化
        /// </summary>
        private void InitQueryContent()
        {
            //初始化查询内容
            ArrayList al = new ArrayList();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            if (this.patientType == FS.SOC.HISFC.DCP.Enum.PatientType.O)
            {
                obj.ID = "ReportInfo";
                obj.Name = "全院报卡信息查询";
                al.Add(obj);
            }
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "PatientInfo";
            obj.Name = "患者查询";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "DeptReport";
            obj.Name = "本科室报卡信息查询";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "DeptUnReport";
            obj.Name = "本科室不合格报卡查询";
            al.Add(obj);

            //obj = new FS.FrameWork.Models.NeuObject();
            //obj.ID = "deptLisResult";
            //obj.Name = "本科室实验室检测阳性报卡";
            //al.Add(obj);

            //obj = new FS.FrameWork.Models.NeuObject();
            //obj.ID = "FeedBack";
            //obj.Name = "本科室传染病漏报查询";
            //al.Add(obj);

            this.cmbQueryContent.AddItems(al);
            this.cmbQueryContent.SelectedIndex = 0;
            this.cmbQueryContent.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            this.txtDocterNO.Text = "";
            this.txtPatientName.Text = "";
            this.txtPatientNO.Text = "";
            this.txtReportNO.Text = "";
            this.patient = null;
            //不清空界面打开加载的报告卡信息
            //this.alReport.Clear();
            if (this.AlReport == null || this.AlReport.Count==0)
            {
                this.commonReport = null;
 
            }
            //时间
        }

        public void ClearSelected()
        {
            this.tvReport.SelectedNode = null;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.InitQueryContent();
            this.dtpBeginTime.Value = this.diseaseMgr.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value=this.dtpBeginTime.Value.AddDays(-days).Date;
            this.SetEnablellb(this.PatientType);
            this.Clear();
            this.tvReport.Nodes.Clear();

            this.TreeViewAddReportsIgnorState(this.alReport);

            return 1;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns>-1 失败 1成功</returns>
        public int Query()
        {
            this.tvReport.Nodes.Clear();
            this.tvReport.Select();
            switch (this.cmbQueryContent.Tag.ToString())
            {
                case "ReportInfo":
                    return this.QueryReportInfo();
                case "PatientInfo":
                    return this.QueryPatientInfo();
                case "DeptReport":
                    return this.QueryDeptReport();
                    break;
                case "DeptUnReport":
                    return this.QueryDeptReport(FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible);
                    break;
                case "FeedBack":
                    break;
            }
            return 1;
        }

        /// <summary>
        /// 根据
        /// </summary>
        private int QueryReportInfo()
        {
            if (this.txtReportNO.Text.Trim() != "")
            {
                this.QueryByReportNO();
            }
            else if (this.txtPatientNO.Text.Trim() != "")
            {
                this.QueryByPatientNO();
            }
            else if (this.txtPatientName.Text != "")
            {
                this.QueryByPatientName();
            }
            else if (this.txtDocterNO.Text.Trim() != "")
            {
                this.QueryByDoctorNO();
            }
            else
            {
                this.QueryOldReport();
            }
            
            return 1;
        }

        /// <summary>
        /// 报告卡虚拟编号查询
        /// </summary>
        private void QueryByReportNO()
        {
            //报告卡虚拟编号查询

            this.tvReport.Nodes.Clear();
            ArrayList al = new ArrayList();
            FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
            report = this.diseaseMgr.GetCommonReportByNO(this.txtReportNO.Text);
            if (report != null)
            {
                if (report.ReportTime > this.dtpBeginTime.Value.Date && report.ReportTime < this.dtpEndTime.Value.AddDays(1).Date)
                {
                    al.Add(report);
                }

                this.TreeViewAddReportsIgnorState(al);
            }
        }

        /// <summary>
        /// 查找历史报告[科室新增 删除 作废报告后或按下历史按钮后显示已填写报告列表]
        /// </summary>
        public void QueryOldReport()
        {
            if (this.PatientType == FS.SOC.HISFC.DCP.Enum.PatientType.O)//疾病传染科
            {
                this.QueryHospitalReport();
            }
            else
            {
                this.QueryDeptReport();
            }
        }

        /// <summary>
        /// 查询全院传染病报卡
        /// </summary>
        private void QueryHospitalReport()
        {
            ArrayList al = new ArrayList();
            if (this.PatientType == FS.SOC.HISFC.DCP.Enum.PatientType.O)
            {
                al = this.diseaseMgr.GetCommonReportListByReportTime(this.dtpBeginTime.Value.Date, this.dtpEndTime.Value.AddDays(1).Date);
            }
            if (al.Count > 0)
            {
                //显示报告
                this.TreeViewAddReportsIgnorState(al);
            }
        }

        #region 科室查询
        private int QueryDeptReport()
        {
            return this.QueryDeptReport("AAA");
        }
        /// <summary>
        /// 科室查询保告卡
        /// </summary>
        private int QueryDeptReport(FS.SOC.HISFC.DCP.Enum.ReportState reportState)
        {
            return QueryDeptReport(((int)reportState).ToString());
        }

        public int QueryDeptReport(FS.SOC.HISFC.DCP.Enum.ReportState reportState,string deptID)
        {
            return QueryDeptReport(((int)reportState).ToString());
        } 

        /// <summary>
        /// 科室查询保告卡
        /// </summary>
        private int QueryDeptReport(string reportState)
        {
            ArrayList al = this.diseaseMgr.GetCommonReportListByMore("report_date", this.dtpBeginTime.Value.Date.ToString(), this.dtpEndTime.Value.AddDays(1).Date.ToString(), reportState, ((FS.HISFC.Models.Base.Employee)this.diseaseMgr.Operator).Dept.ID);
            if (al == null)
            {
                MessageBox.Show("根据登录科室查询患者报告信息时发生错误" + this.diseaseMgr.Err);
                return -1;
            }
            this.TreeViewAddReportsIgnorState(al);

            return 1;
        }

        #endregion

        #region 医生工号查询该医生已填写报告

        /// <summary>
        /// 根据报告医生查询报告卡
        /// </summary>
        private void QueryByDoctorNO()
        {
            this.QueryByDoctorNO(this.txtDocterNO.Text.Trim());
        }

        public void QueryByDoctorNO(string doctorID)
        {
            this.tvReport.Nodes.Clear();
            if (string.IsNullOrEmpty(doctorID))
            {
                return;
            }
            ArrayList al = new ArrayList();
            foreach (FS.SOC.HISFC.DCP.Enum.ReportState s in System.Enum.GetValues(typeof(FS.SOC.HISFC.DCP.Enum.ReportState)))
            {
                al.AddRange(this.diseaseMgr.GetReportListByStateAndDoctor(Function.ConvertState(s), doctorID.PadLeft(6, '0')));
            }

            this.TreeViewAddReportsIgnorState(al);
        }

        public void QueryByDoctorNO(FS.SOC.HISFC.DCP.Enum.ReportState state,string doctorID)
        {
            this.tvReport.Nodes.Clear();
            if (string.IsNullOrEmpty(doctorID))
            {
                return;
            }
            ArrayList al = this.diseaseMgr.GetReportListByStateAndDoctor(Function.ConvertState(state), doctorID.PadLeft(6, '0'));

            this.TreeViewAddReportsIgnorState(al);
        }

        #endregion

        #region 患者查询

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <returns></returns>
        private int QueryPatientInfo()
        {
            if (this.txtPatientNO.Text.Trim() != "")
            {
                this.QueryByPatientNO();
            }
            else if (this.txtPatientName.Text != "")
            {
                this.QueryByPatientName();
            }
            else
            {
                if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
                {
                    this.QueryPatientByDeptIN();
                }
                else if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)    //--修改
                {
                    QueryPatientByDco();
                }
            }

            return 1;
        }

        /// <summary>
        /// 患者号查询
        /// </summary>
        private void QueryByPatientNO()
        {
            //患者号查询
            ArrayList al = new ArrayList();
            if (this.txtPatientNO.Text.Trim() == "")
            {
                return;
            }
            string patientno = this.txtPatientNO.Text.Trim().PadLeft(10, '0');

            al = this.diseaseMgr.GetCommonReportListByPatientNO(patientno);
            if (al == null)
            {
                MessageBox.Show("根据患者ID查询患者报告信息时发生错误" + this.diseaseMgr.Err);
                return;
            }
            else if (al.Count > 0)
            {
                this.TreeViewAddReportsIgnorState(al);
            }
            else             //无报卡信息 显示患者信息
            {
                #region 无报卡信息时显示患者信息

                //住院患者
                if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
                {
                    al = this.patientProcess.GetPatientInfoByPatientNOAll(patientno);
                }
                //门诊患者
                else if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
                {
                    al = this.patientProcess.Query(patientno, DateTime.Now.AddYears(-1000));
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show(this, "没有患者信息！", "提示>>");
                    this.tvReport.Nodes.Clear();
                    return;
                }
                this.TreeViewAddPatientInfo(al);

                #endregion
            }
        }

        /// <summary>
        /// 患者姓名查询
        /// </summary>
        private void QueryByPatientName()
        {
            ArrayList al = new ArrayList();
            if (this.txtPatientName.Text.Trim() == "")
            {
                return;
            }
            string patientName = this.txtPatientName.Text.Trim();

            al = this.diseaseMgr.GetReportListByPatientName(patientName);
            if (al == null)
            {
                MessageBox.Show("根据患者姓名检索报告卡信息时发生错误" + this.diseaseMgr.Err);
                return;
            }
            else if (al.Count > 0)
            {
                this.TreeViewAddReportsIgnorState(al);
            }
            else            //无报告卡时显示患者信息
            {
                //住院患者
                if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
                {
                    al = this.patientProcess.GetPatientInfoByPatientName(patientName);
                }
                //门诊患者
                else if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
                {
                    al = this.patientProcess.QueryValidPatientsByName(patientName);
                }

                if (al == null || al.Count == 0)
                {
                    MessageBox.Show(this, "没有患者信息！", "提示>>");
                    this.tvReport.Nodes.Clear();
                    return;
                }

                this.TreeViewAddPatientInfo(al);
            }
        }

        /// <summary>
        /// 按照住院科室查找患者
        /// </summary>
        private void QueryPatientByDeptIN()
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.RADT.InStateEnumService instate = new FS.HISFC.Models.RADT.InStateEnumService();
            instate.ID = "I";
            //住院患者
            if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
            {
                al = this.patientProcess.QueryPatientByDeptCode(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, instate);
            }

            if (al == null || al.Count == 0)
            {
                MessageBox.Show(this, "没有患者信息！", "提示>>");
                this.tvReport.Nodes.Clear();
                return;
            }

            if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
            {
                this.TreeViewAddPatientInfo(al);
            }
        }
        /// <summary>
        /// 按照医生查询门诊病人 --修改
        /// </summary>
        private void QueryPatientByDco()
        {
            ArrayList al = new ArrayList();

            if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                al = this.patientProcess.QueryBySeeDoc(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID, FS.FrameWork.Function.NConvert.ToDateTime(this.diseaseMgr.GetSysDateTime()).AddDays(-this.days).Date, FS.FrameWork.Function.NConvert.ToDateTime(this.diseaseMgr.GetSysDateTime()).AddDays(1).Date, true);
            }

            if (al == null || al.Count == 0)
            {
                MessageBox.Show(this, "没有患者信息！", "提示>>");
                this.tvReport.Nodes.Clear();
                return;
            }
            if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                //门诊医生站 按患者查询的时候 应该显示门诊患者
                this.TreeViewAddPatientInfo(al);
            }
        }

        #endregion

        /// <summary>
        /// 科室反馈信息
        /// </summary>
        //private void QueryFeedBackByDept()
        //{
        //    ArrayList al =this.diseaseMgr.GetFeedBackByDoctAndDept(this.User.ID,this.User.Dept.ID);
        //    if (al.Count == 0)
        //    {
        //        MessageBox.Show(this, "没有科室反馈信息", "提示>>");
        //        return;
        //    }
        //    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载本科室反馈信息");
        //    Application.DoEvents();
        //    this.TreeViewAddFeedBack(al);
        //    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        //}

        #region 树节点内容
        /// <summary>
        /// 显示报卡信息（分状态）
        /// </summary>
        /// <param name="al"></param>
        public void TreeViewAddReportsIgnorState(ArrayList al)
        {
            this.tvReport.Nodes.Clear();
            if (al == null || al.Count < 1)
            {
                return;
            }
            string msg = "";
            foreach (FS.HISFC.DCP.Object.CommonReport report in al)
            {
                switch (FS.FrameWork.Function.NConvert.ToInt32(report.State))
                {
                    case (int)FS.SOC.HISFC.DCP.Enum.ReportState.New:
                        this.TreeViewAddReport(Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.New), "新填", 4, report,ref msg);
                        break;
                    case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible:
                        this.TreeViewAddReport(report.State, "合格", 4, report,ref msg);
                        break;
                    case (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible://审核
                        this.TreeViewAddReport(report.State, "不合格（请修改报卡）", 3,Color.Red, report,ref msg);
                        break;
                    case (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel:
                        this.TreeViewAddReport(report.State, "报告作废", 3, Color.Red, report, ref msg);
                        break;
                    case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel://作废
                        this.TreeViewAddReport(report.State, "保健科作废", 3, report, ref msg);
                        break;
                    default:
                        break;
                }
            }

            this.tvReport.ExpandAll();
            if (this.tvReport.Nodes.Count == 1 && this.tvReport.Nodes[0].Nodes.Count == 1)
            {
                this.tvReport.SelectedNode = this.tvReport.Nodes[0].Nodes[0];
            }
        }

        /// <summary>
        ///  显示报告卡[同状态的报告]
        /// </summary>
        /// <param name="al">同状态的报告</param>
        private void TreeViewAddReport(string nodeID, string nodeName, int nodeImageIndex, FS.HISFC.DCP.Object.CommonReport report,ref string msg)
        {
            this.TreeViewAddReport(nodeID, nodeName, nodeImageIndex, Color.Black, report, ref msg);

        }

        private void TreeViewAddReport(string nodeID, string nodeName, int nodeImageIndex,Color color, FS.HISFC.DCP.Object.CommonReport report, ref string msg)
        {
            System.Windows.Forms.TreeNode node = null;
            if (!this.tvReport.Nodes.ContainsKey(nodeID))
            {
                node = new TreeNode();
                node.Name = nodeID;
                node.Text = nodeName;
                node.ForeColor = color;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                node.Tag = "root";
                this.tvReport.Nodes.Add(node);
            }

            TreeNode childNode = new TreeNode();
            childNode.Tag = report;
            if (report.Patient.Name == null || report.Patient.Name == "")
            {
                childNode.Text = report.PatientParents + "[" + report.ReportNO + "]" + report.ExtendInfo3;
            }
            else
            {
                childNode.Text = report.Patient.Name + "[" + report.ReportNO + "]" + report.ExtendInfo3; ;
            }
            childNode.Name = report.ID;

            childNode.ImageIndex = nodeImageIndex;
            childNode.SelectedImageIndex = 2;

            this.tvReport.Nodes[nodeID].Nodes.Add(childNode);

            if (nodeID == Function.ConvertState(FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible) && report.ReportDoctor.ID == this.diseaseMgr.Operator.ID)
            {
                msg += childNode.Text + "||";
            }

        }

        /// <summary>
        /// 显示患者信息（分门诊和住院）
        /// </summary>
        private void TreeViewAddPatientInfo(ArrayList al)
        {
            this.tvReport.Nodes.Clear();
            if (al == null || al.Count == 0)
            {
                return;
            }
            TreeNode node = null;
            if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.I)
            {
                node = new TreeNode("患者列表--[姓名][入院日期][入院科室]");
                node.ImageIndex = 1;
                foreach (FS.HISFC.Models.RADT.PatientInfo patient in al)
                {
                    TreeNode childNode = new TreeNode();

                    childNode.ImageIndex = 4;
                    childNode.SelectedImageIndex = 2;
                    childNode.Text = "[" + patient.Name + "][" + patient.PVisit.InTime.ToShortDateString() + "][" + patient.PVisit.PatientLocation.Dept.Name + "]";
                    childNode.Tag = patient;
                    node.Nodes.Add(childNode);
                }
            }
            else if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                node = new TreeNode("患者列表--[姓名][挂号日期][挂号科室]");
                node.ImageIndex = 1;
                foreach (FS.HISFC.Models.Registration.Register patient in al)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.ImageIndex = 4;
                    childNode.SelectedImageIndex = 2;
                    childNode.Text = "[" + patient.Name + "][" + patient.DoctorInfo.SeeDate.ToShortDateString() + "][" + patient.DoctorInfo.Templet.Dept.Name + "]";
                    childNode.Tag = patient;
                    node.Nodes.Add(childNode);
                }
            }

            this.tvReport.Nodes.Add(node);
            this.tvReport.ExpandAll();
            if (this.tvReport.Nodes.Count > 0 && node.Nodes.Count>0)
            {
                this.tvReport.SelectedNode = node.Nodes[0];
            }
            //else
            //{
            //    node = new TreeNode("患者列表--[姓名][日期][科室]");
            //    foreach (FS.HISFC.Models.RADT.PatientInfo patient in al)
            //    {
            //        TreeNode childNode = new TreeNode();
            //        childNode.Text = "[" + patient.Name + "][" + patient.PVisit.InTime.ToShortDateString() + "][" + patient.PVisit.PatientLocation.Dept.Name + "]";
            //        childNode.Tag = patient;
            //    }
            //}


        }

        /// <summary>
        /// 显示报告列表[按状态在列表中增加分级节点]
        /// </summary>
        private void TreeViewAddReports(ArrayList al, FS.SOC.HISFC.DCP.Enum.ReportState reportState)
        {
            try
            {
                string nodeText = "";
                //父节点名称 显示报告状态
                int imagindex = 4;
                Color color = Color.Black;
                switch (reportState)
                {
                    case FS.SOC.HISFC.DCP.Enum.ReportState.New:
                        nodeText = "新填";
                        break;
                    case FS.SOC.HISFC.DCP.Enum.ReportState.Eligible:
                        nodeText = "合格";
                        imagindex = 4;
                        break;
                    case FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible://审核
                        nodeText = "不合格（请修改报卡）";
                        imagindex = 3;
                        color = System.Drawing.Color.Red;
                        break;
                    case FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel:
                        nodeText = "报告作废";
                        imagindex = 3;
                        color = System.Drawing.Color.Red;
                        break;
                    case FS.SOC.HISFC.DCP.Enum.ReportState.Cancel://作废
                        nodeText = "保健科作废";//保健科作废
                        imagindex = 3;
                        break;
                    default:
                        break;
                }
                
                //子节点加载 显示患者姓名 报告编号
                string msg = "";
                foreach (FS.HISFC.DCP.Object.CommonReport report in al)
                {
                    this.TreeViewAddReport(Function.ConvertState(reportState), nodeText, imagindex, color, report, ref msg);
                }
                if (msg != "")
                {
                    MessageBox.Show(this, "您填写的" + msg + "报告卡不合格，请查看[退卡原因]栏进行相应修改", "退卡原因：");
                }
                this.tvReport.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "加载历史报告信息失败" + ex.Message, "错误>>");
            }
        }

        #endregion

        /// <summary>
        /// 设置llbPatientNO的可用
        /// </summary>
        /// <param name="patientType"></param>
        private void SetEnablellb(FS.SOC.HISFC.DCP.Enum.PatientType patientType)
        {
            if (FS.SOC.HISFC.DCP.Enum.PatientType.C == patientType)
            {
                this.llbPatientNO.Enabled = false;
                this.llbPatientNO.Text = "门诊卡号：";
                this.llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;
            }
            else if (FS.SOC.HISFC.DCP.Enum.PatientType.I == patientType)
            {
                this.llbPatientNO.Enabled = false;
                this.llbPatientNO.Text = "住 院 号：";
                this.llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            }
            else
            {
                this.llbPatientNO.Enabled = true;
                this.llbPatientNO.Text = "门诊卡号：";
                this.llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;
            }
        }

        /// <summary>
        /// 删除报告卡
        /// </summary>
        /// <param name="ID">编号</param>
        public int DeleteReport(string ID)
        {
            System.Windows.Forms.DialogResult dr = new DialogResult();
            dr = MessageBox.Show("确定要删除报告卡吗？\n删除后不能恢复！", "提示>>", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button2);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                int param = this.diseaseMgr.DeleteCommonReport(ID);
                if (param == 1)
                {
                    this.MyMessageBox("报告卡删除成功!", "提示>>");
                    return -1;
                }
                else if (param == 0)
                {
                    this.MyMessageBox("报告卡已经过修订或审核 无法进行删除", "提示");
                }
                else
                {
                    this.MyMessageBox("报告卡删除失败!" + this.diseaseMgr.Err, "错误>>");
                }

                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 删除报卡
        /// </summary>
        public void DeleteReport()
        {
            if ( this.tvReport.SelectedNode == null)
            {
                return;
            }
            if (this.tvReport.SelectedNode.Tag.ToString() == "root")
            {
                return;
            }

            FS.HISFC.DCP.Object.CommonReport report = new FS.HISFC.DCP.Object.CommonReport();
            report = (FS.HISFC.DCP.Object.CommonReport)this.tvReport.SelectedNode.Tag;
            if (report != null && report.ID != "")
            {
                if (this.DeleteReport(report.ID) == 0)
                {
                    this.tvReport.Nodes.Remove(this.tvReport.SelectedNode);

                    //查询新加
                    ArrayList alTempReport = new ArrayList();
                    foreach (ArrayList al in this.AlReport)
                    {
                        ArrayList altemp = new ArrayList();
                        foreach (FS.HISFC.DCP.Object.CommonReport rpt in al)
                        {
                            if (rpt.ID != report.ID)
                            {
                                altemp.Add(rpt);
                            }
                        }
                        if (altemp != null && altemp.Count > 0)
                        {
                            alTempReport.Add(altemp);
                        }
                    }
                    this.AlReport = alTempReport;
                    this.ReflashTreeView(this.AlReport);
                }
            }
        }

        /// <summary>
        /// MessageBox
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="type">err错误 其它作标题</param>
        private void MyMessageBox(string message, string type)
        {
            switch (type)
            {
                case "err":
                    MessageBox.Show(message, "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                default:
                    MessageBox.Show(message, type, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
            }
        }

        /// <summary>
        /// 按列表刷新
        /// </summary>
        /// <param name="alAllState">列表中的所有报告</param>
        private void ReflashTreeView(ArrayList alAllState)
        {
            //保健科审核时刷新报告列表 郁闷的算法

            //窗口初始化化前报告卡属性已经赋值，但经操作后状态改变。所以按状态重新分类显示

            try
            {
                ArrayList alNew = new ArrayList();//新加
                ArrayList alGood = new ArrayList();//合格
                ArrayList alBad = new ArrayList();//不合格
                ArrayList alCancel = new ArrayList();//报告人作废

                //ArrayList alfive = new ArrayList();//保健科作废


                foreach (ArrayList alonestate in alAllState)
                {
                    foreach (FS.HISFC.DCP.Object.CommonReport report in alonestate)
                    {
                        FS.HISFC.DCP.Object.CommonReport tempreport = new FS.HISFC.DCP.Object.CommonReport();
                        tempreport = this.diseaseMgr.GetCommonReportByID(report.ID);
                        switch (Int32.Parse(tempreport.State))
                        {
                            case (int)FS.SOC.HISFC.DCP.Enum.ReportState.New:
                                alNew.Add(tempreport);
                                break;
                            case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Eligible:
                                alGood.Add(tempreport);
                                break;
                            case (int)FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible:
                                alBad.Add(tempreport);
                                break;
                            case (int)FS.SOC.HISFC.DCP.Enum.ReportState.OwnCancel:
                            case (int)FS.SOC.HISFC.DCP.Enum.ReportState.Cancel:
                                alCancel.Add(tempreport);
                                break;
                        }
                    }
                }
                this.tvReport.Nodes.Clear();
                this.TreeViewAddReports(alNew, FS.SOC.HISFC.DCP.Enum.ReportState.New);
                this.TreeViewAddReports(alGood, FS.SOC.HISFC.DCP.Enum.ReportState.Eligible);
                this.TreeViewAddReports(alBad, FS.SOC.HISFC.DCP.Enum.ReportState.UnEligible);
                this.TreeViewAddReports(alCancel, FS.SOC.HISFC.DCP.Enum.ReportState.Cancel);

            }
            catch (Exception ex)
            {
                MessageBox.Show("刷新列表出错>>" + ex.Message);
            }
        }

        public void QueryByCache()
        {
            if (this.AlReport != null && this.AlReport.Count > 0)
            {
                this.ReflashTreeView(this.AlReport);
            }
            else
            {
                this.QueryOldReport();
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 根据操作患者的类型确定是门诊卡号还是住院号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llbPatientNO_Click(object sender, EventArgs e)
        {
            if (this.llbPatientType==FS.SOC.HISFC.DCP.Enum.PatientType.C)
            {
                this.llbPatientNO.Text = "住 院 号：";
                this.llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.I;
            }
            else
            {
                this.llbPatientNO.Text = "门诊卡号：";
                this.llbPatientType = FS.SOC.HISFC.DCP.Enum.PatientType.C;
            }
        }

        /// <summary>
        /// 查询类别发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbQueryContent_SelectedValueChanged(object sender, EventArgs e)
        {
            this.Clear();
            this.tvReport.Nodes.Clear();

            if (this.cmbQueryContent.Tag.ToString() == "PatientInfo")
            {
                //患者基本信息查询

                this.dtpBeginTime.Enabled = false;
                this.dtpEndTime.Enabled = false;
                this.txtReportNO.Enabled = false;
                this.txtDocterNO.Enabled = false;
                this.txtPatientName.Enabled = true;
                this.txtPatientNO.Enabled = true;
                this.SetEnablellb(this.patientType);
            }
            else if (this.cmbQueryContent.Tag.ToString() == "ReportInfo")
            {
                //患者报卡信息查询

                this.dtpBeginTime.Enabled = true;
                this.dtpEndTime.Enabled = true;
                this.txtReportNO.Enabled = true;
                this.txtDocterNO.Enabled = true;
                this.txtPatientNO.Enabled = true;
                this.txtPatientName.Enabled = true;
                this.SetEnablellb(this.patientType);
            }
            else if (this.cmbQueryContent.Tag.ToString() == "DeptReport" || this.cmbQueryContent.Tag.ToString() == "DeptUnReport" || this.cmbQueryContent.Tag.ToString() == "FeedBack")
            {
                //科室报告信息查询
                this.dtpBeginTime.Enabled = true;
                this.dtpEndTime.Enabled = true;
                this.txtDocterNO.Enabled = false;
                this.txtPatientNO.Enabled = false;
                this.txtPatientName.Enabled = false;
                this.txtReportNO.Enabled = false;
            }
            //else if (this.cmbQueryContent.Tag.ToString() == "choose")
            //{
            //    this.txtDoctor.Enabled = false;
            //    this.txtInPatienNo.Enabled = false;
            //    this.txtName.Enabled = false;
            //    this.txtReportNo.Enabled = false;
            //    this.dtBegin.Enabled = false;
            //    this.dtEnd.Enabled = false;
            //}
            //else if (this.cmbQueryContent.Tag.ToString() == "deptLisResult")
            //{
            //    this.groupBox3.Enabled = true;
            //    this.txtDoctor.Enabled = false;
            //    this.txtInPatienNo.Enabled = false;
            //    this.txtName.Enabled = false;
            //    this.txtReportNo.Enabled = false;
            //    this.dtBegin.Enabled = false;
            //    this.dtEnd.Enabled = false;
            //}
        }

        /// <summary>
        /// 选择节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvReport_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is FS.HISFC.Models.RADT.PatientInfo || e.Node.Tag is FS.HISFC.Models.Registration.Register)
            {
                this.commonReport = null;
                this.patient = e.Node.Tag as FS.HISFC.Models.RADT.Patient;
                this.ShowInfo.Invoke(this.patient);
            }
            else if(e.Node.Tag is FS.HISFC.DCP.Object.CommonReport)
            {
                this.commonReport = e.Node.Tag as FS.HISFC.DCP.Object.CommonReport;
                this.patient = this.commonReport.Patient;
                this.ShowInfo.Invoke(this.commonReport);
            }
        }

        #endregion 

        private void txtPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.llbPatientType == FS.SOC.HISFC.DCP.Enum.PatientType.C)
                {
                    FS.HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
                    if (this.txtPatientNO.Text.Trim() == "")
                    {
                        this.MyMessageBox("请输入病历号!", "提示<<");
                        this.txtPatientNO.Focus();
                        this.txtPatientNO.SelectAll();
                        return;
                    }
                    string strCardNO = this.txtPatientNO.Text.Trim();
                    FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();
                    int iTemp = feeManage.ValidMarkNO(strCardNO, ref objCard);
                    if (iTemp <= 0 || objCard == null)
                    {
                        this.MyMessageBox("无效卡号，请联系管理员！", "err");
                        this.txtPatientNO.Focus();
                        this.txtPatientNO.SelectAll();
                        return;
                    }
                    string cardNo = objCard.Patient.PID.CardNO;
                    this.txtPatientNO.Text = cardNo;
                }

                this.Query();
            }
        }
    }

    /// <summary>
    /// 状态
    /// </summary>
    public enum enumTreeViewType
    {
        PatientInfo,
        ReportInfo,
        FeedBackInfo
    }
}
