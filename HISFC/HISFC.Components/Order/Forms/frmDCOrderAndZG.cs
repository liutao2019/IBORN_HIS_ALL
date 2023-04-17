using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Forms
{
    /// <summary>
    /// 医生站开立 预约出院 医嘱时弹出的 停长嘱填转归 窗口 by huangchw 2012-10-29
    /// </summary>
    public partial class frmDCOrderAndZG : Form
    {
        /// <summary>
        /// 常数管理类
        /// </summary>
        HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 当前开立患者
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;

        /// <summary>
        /// 当前开立患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get { return this.myPatientInfo; }
            set { this.myPatientInfo = value; }
        }

        /// <summary>
        /// 住院入出转管理
        /// </summary>
        private FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = null;

        /// <summary>
        /// 住院入出转管理
        /// </summary>
        public FS.HISFC.BizLogic.RADT.InPatient InPatientMgr
        {
            get
            {
                if (inPatientMgr == null)
                {
                    inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
                }
                return inPatientMgr;
            }
        }


        public frmDCOrderAndZG()
        {
            InitializeComponent();
        }

        private void frmDCOrder_Load(object sender, EventArgs e)
        {
            this.cmbDiag.Text = string.Empty;
            this.cmbDiag.SelectedIndex = 0;
            this.dateTimeBox1.Value = CacheManager.InterMgr.GetDateTimeFromSysDateTime().AddMinutes(5);
            this.dateTimeBox1.MinDate = this.dateTimeBox1.Value.Date;
            this.treamtype.Text = "旧待遇类型：" + InPatientMgr.GetYIBAODAIYU(this.myPatientInfo.ID);//{d88ca0f0-6235-4a5d-b04e-4eac0f7a78e7}
        }


        public void Init()
        {
            try
            {
                System.Collections.ArrayList alReason = CacheManager.GetConList("DCREASON");
                if (alReason == null || alReason.Count == 0)
                {
                    MessageBox.Show("查询停止原因出错！" + CacheManager.InterMgr.Err);
                    return;
                }
                this.cmbDC.AddItems(alReason);
                if (this.cmbDC.Items.Count > 0)
                {
                    this.cmbDC.SelectedIndex = 0;
                }

                System.Collections.ArrayList alZG = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.ZG);
                if (alZG == null || alZG.Count == 0)
                {
                    MessageBox.Show("查询转归情况出错！" + CacheManager.InterMgr.Err);
                    return;
                }
                this.cmbZG.AddItems(alZG);
                if (this.cmbZG.Items.Count > 0)
                {
                    this.cmbZG.SelectedIndex = 0;
                }

                ArrayList ICD10List = new ArrayList();
                ICD10List.Add(new FS.FrameWork.Models.NeuObject());
                ArrayList alICD10 = SOC.HISFC.BizProcess.Cache.Order.GetICD10();
                ICD10List.AddRange(alICD10);

                //添加出院诊断
                this.cmbDiag.AddItems(ICD10List);

                //{5936B0A0-598F-43a8-BB31-E812EB8D61EE}
                string icdCode = QueryEMROutDiagnose();
                if (string.IsNullOrEmpty(icdCode))
                {
                    this.cmbDiag.Tag = icdCode;
                }

                //添加医保类型{9BCBF464-EB90-4c07-AD4D-29481A069D3D}
                System.Collections.ArrayList alHealthCareType = CacheManager.GetConList("MEDICALINSURANCEITEM");
                alHealthCareType.Add(new FS.FrameWork.Models.NeuObject());
                if (alHealthCareType == null)
                {
                    MessageBox.Show("查询医保待遇类型出错！" + CacheManager.InterMgr.Err);
                    return;
                }
                else if (alHealthCareType.Count == 0)
                {
                    //cbxHealthCareType.Enabled = false;
                }
                else
                {
                    this.cbxHealthCareType.AddItems(alHealthCareType);
                }

                HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;

                HISFC.Models.Base.Department dept = empl.Dept as HISFC.Models.Base.Department;

                if (dept.ID == "1001")
                {
                    neuLabel5.Visible = true;
                    cbxHealthCareType.Visible = true;
                }
                else
                {
                    neuLabel5.Visible = false;
                    cbxHealthCareType.Visible = false;
                }

                

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
      

        /// <summary>
        /// 查询电子病历出院诊断
        /// {5936B0A0-598F-43a8-BB31-E812EB8D61EE}
        /// </summary>
        /// <returns></returns>
        private string QueryEMROutDiagnose()
        {
            string sql = @"select t.出院主要诊断,t.出院主要诊断编码 from disease@emr1_dblink t
                            where t.HIS内部标识 = '{0}'
                              and t.住院号 = '{1}'";

            try
            {
                if (this.myPatientInfo != null)
                {
                    DataSet ds = new DataSet();
                    sql = string.Format(sql, this.myPatientInfo.ID, this.myPatientInfo.PID.PatientNO);
                    this.constManager.ExecQuery(sql, ref ds);

                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

                    while (this.constManager.Reader.Read())
                    {
                        try
                        {
                            obj.ID = this.constManager.Reader[0].ToString();
                            obj.Name = this.constManager.Reader[1].ToString();
                            break;
                        }
                        catch (Exception ex)
                        {

                            this.constManager.Reader.Close();
                            this.constManager.Err = "查询出院诊断赋值错误" + ex.Message;
                            this.constManager.ErrCode = ex.Message;
                            return string.Empty;
                        }
                    }

                    this.constManager.Reader.Close();
                    return obj.ID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询患者出院诊断出现错误，请手工录入，错误信息：" + ex.Message);
            }

            return string.Empty;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            DateTime now = CacheManager.InterMgr.GetDateTimeFromSysDateTime();
            if (this.dateTimeBox1.Value < now)
            {
                MessageBox.Show("停止日期不能小于当前日期！");
                return;
            }

            HISFC.Models.Base.Employee empl2 = FS.FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;

            HISFC.Models.Base.Department dept2 = empl2.Dept as HISFC.Models.Base.Department;
            if (dept2.ID == "1001")
            {
                //如果登录的是妇产科就一定要检测医保待遇填写情况
                //校验医保类型
                if (this.cbxHealthCareType.Text == string.Empty)
                {
                    MessageBox.Show("医保待遇类型不能为空！");
                    return;
                }
            }

            //{5936B0A0-598F-43a8-BB31-E812EB8D61EE}
            //if (this.cmbDiag.Text == string.Empty)
            if (this.cmbDiag.SelectedItem == null)
            {
                MessageBox.Show("出院诊断不能为空！");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }
        /// <summary>
        /// 停止时间
        /// </summary>
        public DateTime DCDateTime
        {
            get
            {
                return this.dateTimeBox1.Value;
            }
        }
        /// <summary>
        /// 停止原因
        /// </summary>
        public FS.FrameWork.Models.NeuObject DCReason
        {
            get
            {
                if (this.cmbDC.Text == "") return new FS.FrameWork.Models.NeuObject();
                return this.cmbDC.alItems[this.cmbDC.SelectedIndex] as FS.FrameWork.Models.NeuObject;
            }
        }

        /// <summary>
        /// 转归情况
        /// </summary>
        public FS.FrameWork.Models.NeuObject ZG
        {
            get
            {
                if (this.cmbZG.Text == "") return new FS.FrameWork.Models.NeuObject();
                return this.cmbZG.alItems[this.cmbZG.SelectedIndex] as FS.FrameWork.Models.NeuObject;
            }
        }
        /// <summary>     
        /// 医保待遇类型：MII001	严重高危妊娠，MII005	剖宫产，MII010	阴式分娩（含妊娠7个月以上引产），MII015	分娩期住院，MII020	妊娠3个月以上引产（住院），MII025	妊娠3个月以下人流（住院）
        /// </summary>
        public string HealthCareObject
        {
            get
            {
                return this.cbxHealthCareType.Tag.ToString();
                //return "0";
            }
        }
        /// <summary>
        /// 出院诊断
        /// </summary>
        public string DiagInfo
        {
            get
            {
                return this.cmbDiag.Text;
            }
        }
    }
}
