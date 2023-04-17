using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DCP.Report
{
    public partial class ucDailyRecordQuery : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucDailyRecordQuery()
        {
            InitializeComponent();
        }

        FS.SOC.HISFC.BizProcess.DCP.Common commonProcessMgr = new FS.SOC.HISFC.BizProcess.DCP.Common();
        FS.SOC.HISFC.BizLogic.DCP.DiseaseReport diseaseReportMgr = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();
       
        /// <summary>
        /// 窗口使用权限
        /// </summary>
        private string curPriveCode = "8001+02";

        /// <summary>
        /// 窗口使用权限
        /// </summary>
        /// <returns></returns>
        [Description("窗口使用权限"), Category("设置"), Browsable(true)]
        public bool CheckPrive()
        {
            if (string.IsNullOrEmpty(this.curPriveCode))
            {
                curPriveCode = "8001+02";
            }
            if (curPriveCode.Split('+').Length < 2)
            {
                curPriveCode = curPriveCode + "+02";
            }
            string[] prives = curPriveCode.Split('+');

            if (Classes.Function.JugePrive(prives[0], prives[1]))
            {
                return true;
            }
            else
            {
                return Classes.Function.JugeManager(this.diseaseReportMgr.Operator.ID);
            }
        }

        private string curOutpatientSettingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPDCPOutpatientDailyRecordSetting.xml";

        /// <summary>
        /// 门诊日志Farpoint格式
        /// </summary>
        [Description("门诊日志Farpoint格式"), Category("设置"), Browsable(true)]
        public string OutpatientSettingFile
        {
            get { return curOutpatientSettingFile; }
        }
        private string curInpatientSettingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPDCPInpatientDailyRecordSetting.xml";

        /// <summary>
        /// 住院日志Farpoint格式
        /// </summary>
        [Description("住院日志Farpoint格式"), Category("设置"), Browsable(true)]
        public string InpatientSettingFile
        {
            get { return curInpatientSettingFile; }
        }


        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.CheckPrive())
            {
                return 1;
            }
            MessageBox.Show("您没有权限！");
            return -1;
        }

        #endregion

        #region 初始化
        public int Init()
        {
            ArrayList alDept = this.commonProcessMgr.QueryDeptAllValid();
            if (alDept == null || alDept.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取科室信息失败！"));
                return -1;
            }
            this.ncmbDept.AddItems(alDept);

            ArrayList alDoctor = this.commonProcessMgr.QueryEmployeeAllValidAndUnvalid();
            if (alDoctor == null || alDoctor.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取员工信息失败！"));
                return -1;
            }
            this.ncmbDoctor.AddItems(alDoctor);

            ArrayList alInfDiagnose = this.commonProcessMgr.QueryConstantList("INFDIAGNOSE");

            if (alInfDiagnose == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取诊断信息失败！"));
                return -1;
            }

            //暂时不处理住院日志
            this.tabControl1.TabPages.Remove(this.tpInpatient);

            this.ncmbDiagnose.AddItems(alInfDiagnose);

            this.ndtpBeginTime.Value = this.ndtpBeginTime.Value.Date;
            this.ndtpEndTime.Value = this.ndtpEndTime.Value.Date.AddDays(1);

            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
            this.fpSpread2.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread2_ColumnWidthChanged);
            return 1;
        }


        #endregion

        #region Farpoint格式

        private int SetFarpoint()
        {
            if (System.IO.File.Exists(this.curOutpatientSettingFile))
            {
                this.fpSpread1.ReadSchema(this.curOutpatientSettingFile);
            }

            if (System.IO.File.Exists(this.curInpatientSettingFile))
            {
                this.fpSpread2.ReadSchema(this.curInpatientSettingFile);
            }
          
            return 1;
        }

        private int ResetFarpoint()
        {
            if (System.IO.File.Exists(this.curOutpatientSettingFile))
            {
                System.IO.File.Delete(this.curOutpatientSettingFile);
            }

            if (System.IO.File.Exists(this.curInpatientSettingFile))
            {
                System.IO.File.Delete(this.curInpatientSettingFile);
            }

            return 1;
        }
        #endregion 

        private int QueryRecord()
        {
            string dept = "All";
            if (this.ncmbDept.Tag != null)
            {
                dept = this.ncmbDept.Tag.ToString();
            }
            if (dept == "")
            {
                dept = "All";
            }
            string doctor = "All";
            if (this.ncmbDoctor.Tag != null)
            {
                doctor = this.ncmbDoctor.Tag.ToString();
            }
            if (doctor == "")
            {
                doctor = "All";
            }
            string patient = this.ntxtPatient.Text;
            if (patient == "")
            {
                patient = "All";
            }
            string diagnose = this.ncmbDiagnose.Text;
            if (diagnose == "")
            {
                diagnose = "All";
            }
            if (this.tabControl1.SelectedTab == this.tpOutpatient)
            {
                DataSet ds = new DataSet();
                string SQL = "";
                if (this.diseaseReportMgr.GetSQL("SOC.DCP.DailyRecord.QueryOutpatient", ref SQL) == -1)
                {
                    SQL = @"
                                select t.diag_date 诊断日期,
                                       t.inpatient_no 门诊流水号,
                                       c.name 姓名,
                                       decode(c.sex_code,'M','男','F','女','O','其他','未知') 性别, 
                                       fun_get_age(c.birthday) 年龄,     
                                       c.prof_code 职业,
                                       c.home 现住地址,
                                       c.home_tel 电话,
                                       decode(t.diag_kind,'1','主要诊断','其他诊断') 诊断类型,
                                       t.icd_code 诊断编码,
                                       t.diag_name 诊断名称,
                                       t.doct_code 诊断医师编码,
                                       t.doct_name 诊断医师
                                  from met_cas_diagnose t,com_patientinfo c,fin_opr_register r
                                  where t.persson_type = '0'
                                  and   t.valid_flag = '1'
                                  and   t.inpatient_no = r.clinic_code
                                  and   r.card_no= c.card_no
                                  and   (r.see_dpcd = '{0}' or 'All' = '{0}')
                                  and   t.diag_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                  and   t.diag_date <  to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                  and   (t.doct_code = '{3}' or 'All' = '{3}')
                                  and   (t.diag_name like '{4}' or 'All' = '{4}')
                                  and   (r.name = '{5}' or r.card_no = '{5}' or 'All' = '{5}')
                                ";
                }
                SQL = string.Format(SQL, dept, this.ndtpBeginTime.Value.ToString(),
                    this.ndtpEndTime.Value.ToString(), doctor, diagnose, patient);
                
                if (diseaseReportMgr.ExecQuery(SQL, ref ds) == -1)
                {
                    MessageBox.Show("获取门诊日志失败，请与系统管理员联系并报告错误：" + diseaseReportMgr.Err);
                    return -1;
                }
                this.fpSpread1_Sheet1.DataSource = ds;
            }
            else if (this.tabControl1.SelectedTab == this.tpInpatient)
            {
                DataSet ds = new DataSet();
                if (diseaseReportMgr.ExecQuery("SOC.DCP.DailyRecord.QueryInpatient", ref ds) == -1)
                {
                    MessageBox.Show("获取住院日志失败，请与系统管理员联系并报告错误：" + diseaseReportMgr.Err);
                    return -1;
                }
                this.sheetView1.DataSource = ds;
            }

            return this.SetFarpoint();
        }

        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            base.ToolStrip_ItemClicked(sender, e);
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.tabControl1.SelectedTab == this.tpOutpatient)
            {
                this.fpSpread1.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            }
            else if(this.tabControl1.SelectedTab == this.tpInpatient)
            {
                this.fpSpread2.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            }

            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryRecord();
            return base.OnQuery(sender, neuObject);
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        private void nlbResetFarpoint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.ResetFarpoint();
        }


        void fpSpread2_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.fpSpread2.SaveSchema(this.curInpatientSettingFile);
        }

        void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.fpSpread1.SaveSchema(this.curOutpatientSettingFile);
        }
    }
}
