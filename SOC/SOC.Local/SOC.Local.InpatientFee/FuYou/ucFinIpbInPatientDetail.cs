using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.FuYou
{
    /// <summary>
    /// [功能描述: 住院患者费用汇总明细查询]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: 2009-11-17]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucFinIpbInPatientDetail : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        private string feeType = "";

        private string trans_type = "";

        public ucFinIpbInPatientDetail()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                this.dwMain.Print();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印出错", "提示");
                return -1;
            }

        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <returns></returns>
        protected override int OnExport()
        {
            //如果存在多个DataWindow时导出方法需要重写，否则不需要重写该方法，根据焦点判断导出具体哪个DataWindow
            try
            {
                //导出Excel格式文件
                SaveFileDialog saveDial = new SaveFileDialog();
                saveDial.Filter = "Excel文件（*.xls）|*.xls";
                //文件名

                string fileName = string.Empty;
                if (saveDial.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveDial.FileName;
                }
                this.dwMain.SaveAs(fileName, Sybase.DataWindow.FileSaveAsType.Excel);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出出错", "提示");
                return -1;
            }
        }

        protected override int OnRetrieve(params object[] objects)
        {

            ucQueryInpatientNo1_myEvent();
            return 1;
            //switch (this.neuComboBox1.Text)
            //{
            //    case "全部":
            //        this.feeType = "ALL";
            //        break;
            //    case "药品":
            //        this.feeType = "DRUG";
            //        break;
            //    case "非药品":
            //        this.feeType = "UNDRUG";
            //        break;
            //}

            //if (base.GetQueryTime() == -1)
            //{
            //    return -1;
            //}
            //if (this.ucQueryInpatientNo1.InpatientNo == null)
            //{
            //    return -1;
            //}

            ////时间

            //if (neuCheckBox1.Checked)
            //{
            //    this.dwMain.DataWindowObject = "d_fin_ipb_outpatient4";
            //    this.MainDWDataObject = "d_fin_ipb_outpatient4";
            //    this.MainDWLabrary = "Report\\pharmacy.pbd;Report\\pharmacy.pbl";
            //    return base.OnRetrieve(this.ucQueryInpatientNo1.InpatientNo, this.feeType, this.dtpBeginTime.Value, this.dtpEndTime.Value);

            //}
            //else
            //{
            //    this.dwMain.DataWindowObject = "d_fin_ipb_outpatient4";
            //    this.MainDWDataObject = "d_fin_ipb_outpatient3";
            //    this.MainDWLabrary = "Report\\pharmacy.pbd;Report\\pharmacy.pbl";
            //    return base.OnRetrieve(this.ucQueryInpatientNo1.InpatientNo, this.feeType);
            //}
           
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.Text == null || this.ucQueryInpatientNo1.Text.Trim() == "")
            {
                MessageBox.Show("请输入住院号");
                
                return;
            }

            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                if (string.IsNullOrEmpty(this.ucQueryInpatientNo1.Err))
                {
                    ucQueryInpatientNo1.Err = "此患者不在院!";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo1.Err, 211);

                this.ucQueryInpatientNo1.Focus();
                return;
            }
            else 
            {
               // base.OnRetrieve(this.ucQueryInpatientNo1.InpatientNo,this.feeType);
                this.Query(this.ucQueryInpatientNo1.InpatientNo);
            }
        }

        private void Query(string inpatientNO)
        {
            switch (this.neuComboBox1.Text)
            {
                case "全部":
                    this.feeType = "ALL";
                    break;
                case "药品":
                    this.feeType = "DRUG";
                    break;
                case "非药品":
                    this.feeType = "UNDRUG";
                    break;
            }

            if (this.cbQuitFee.Checked)
            {
                this.trans_type = "2";
            }
            else
            {
                this.trans_type = "ALL";
            }

            if (base.GetQueryTime() == -1)
            {
                return;
            }
            if (string.IsNullOrEmpty(inpatientNO))
            {
                return;
            }
            FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
            //取患者实体
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            patientInfo = radt.QueryPatientInfoByInpatientNO(inpatientNO);
            //住院天数
            int inDay = 0;
            inDay = radt.GetInDays(patientInfo.ID);// Function.GetInHosDay(patientInfo);
            //时间

            this.dwMain.DataWindowObject = this.MainDWDataObject;
            this.MainDWDataObject = this.MainDWDataObject;
            this.MainDWLabrary = this.MainDWLabrary;
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据，请稍后...");
                Application.DoEvents();
                if (neuCheckBox1.Checked)
                {
                    base.OnRetrieve(inpatientNO, this.feeType, this.dtpBeginTime.Value.Date, this.dtpEndTime.Value.AddDays(1).Date, this.trans_type);

                }
                else
                {
                    base.OnRetrieve(inpatientNO, this.feeType, DateTime.MinValue, DateTime.MaxValue, this.trans_type);
                }
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            dwMain.Modify("t_inDay.text = '住院天数:" + inDay.ToString() + "'");
        }
        
        private void ucMetNuiOutPatientDetail_Load(object sender, EventArgs e)
        {
            if (this.neuComboBox1.Items.Count > 0)
            {
                this.neuComboBox1.SelectedIndex = 0;
            }
            else
            {
                return;
            }
        }

        private void neuCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.neuCheckBox1.Checked)
            {
                this.dtpBeginTime.Enabled = true;
                this.dtpEndTime.Enabled = true;
            }
            else
            {
               
                this.dtpBeginTime.Enabled = false;
                this.dtpEndTime.Enabled = false;
            }
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (neuObject is FS.HISFC.Models.RADT.PatientInfo)
            {
                this.Query(((FS.HISFC.Models.RADT.PatientInfo)neuObject).ID);
            }

            return base.OnSetValue(neuObject, e);
        }

    }
}
