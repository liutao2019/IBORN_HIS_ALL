using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP.Controls
{
    /// <summary>
    /// ucReasonOfNot<br></br>
    /// [功能描述: 不报卡原因uc]<br></br>
    /// [创 建 者: yeph]<br></br>
    /// [创建时间: 2015-1-5]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucReasonOfNot : ucBaseMainReport
    {
        public ucReasonOfNot()
        {
            InitializeComponent();
            this.Init();
          
        }

        FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();

        FS.SOC.HISFC.BizLogic.DCP.DiseaseReport ds = new FS.SOC.HISFC.BizLogic.DCP.DiseaseReport();

        FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot report = new FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot();

        public ucReasonOfNot(FS.HISFC.Models.RADT.Patient pat,string diag)
        {
             
            
            InitializeComponent();
            this.patient = pat;
            report.DiagName = diag.ToString();
            this.Init();
            report.Patient.PID.CardNO = patient.PID.CardNO;
            report.Patient.PID.PatientNO = ((FS.FrameWork.Models.NeuObject)(patient)).ID;

           
            
        }
     
        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>-1 失败 1 成功</returns>
        public int Init()
        {

            if (this.InitInfectReason() == -1)
            {
                return -1;
            }

            return 1;
        }


        

        /// <summary>
        /// 初始化不报卡原因
        /// </summary>
        /// <returns>-1 失败 1成功</returns>
        private int InitInfectReason()
        {
            ArrayList reason = new ArrayList();
            FS.SOC.HISFC.BizProcess.DCP.Common commonProcess = new FS.SOC.HISFC.BizProcess.DCP.Common();
            reason = commonProcess.QueryConstantList("REASONOFNOT");

            this.cmbReasons.AddItems(reason);
            return 1;
        }


        /// <summary>
        /// 取不填卡原因信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public  int GetValue1(ref FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot report)
        {
            string msg = "";
            Control c = null;
            if (this.Valid(ref msg, ref c) == -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(msg));
                if (c != null)
                {
                    c.Select();
                }
                return -1;
            }
 
            try
            {  
                report.ReasonOfNot1 = this.cmbReasons.Text.ToString();
                report.OtherName = this.txtOtherName.Text.ToString();
                report.ReportTime = this.sysdate;
                report.DoctorDept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                report.ReportDoctor.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID;
      
                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(e.Message));
                return -1;
            }
        }


        /// <summary>
        ///赋不填卡原因信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int SetValue1(ref FS.SOC.HISFC.BizLogic.DCP.Object.ReasonOfNot report)
        {

            if (report == null)
            {
                return -1;
            }
            try
            {
                this.cmbReasons.Text = report.ReasonOfNot1.ToString();
                this.txtOtherName.Text = report.OtherName.ToString();
                
                return 0;
            }
            catch (Exception e)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(e.Message));
                return -1;
            }
        }
        /// <summary>
        /// 检查信息是否完整
        /// </summary>
        /// <returns></returns>
        public int Valid(ref string msg,ref Control control)
        {
            
                if (this.cmbReasons.Tag == null || this.cmbReasons.Tag.ToString()=="")
                {
                    msg = FS.FrameWork.Management.Language.Msg("请填写不报卡原因");
                     control = this.cmbReasons;
                     return -1;
                   
                }
                if (this.cmbReasons.Text == "外院已报卡" && string.IsNullOrEmpty(this.txtOtherName.Text))
                {
                    msg = FS.FrameWork.Management.Language.Msg("请填写已报卡的外院名称！");
                    control = this.txtOtherName;
                    return -1;
                }

                return 1;
            
        }

       
        public override void Clear()
        {
          
            this.cmbReasons.Tag = "";
            this.cmbReasons.Text = "";
            this.txtOtherName.Text = "";
            base.Clear();
        }

        #endregion

      

        private void cmbReasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbReasons.Tag != null)
            {
                if (this.cmbReasons.Text == "外院已报卡")
                {
                    this.txtOtherName.Enabled = true;
                }
                else
                {
                    this.txtOtherName.Text = "";
                    this.txtOtherName.Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if (this.GetValue1(ref report) == -1)
            {
               
            }
            else
            {
                if (ds.InsertReportOfNot(report) == -1)
                {

                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }

               if (ds.UpdateReportOfNot(report) == -1)

                  {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    }


                else
                {
                    MessageBox.Show("保存成功");
                    this.FindForm().Close();
                }
                
               }
            }

        private void button2_Click(object sender, EventArgs e)
        {
             

             
        }           
        }
    }

