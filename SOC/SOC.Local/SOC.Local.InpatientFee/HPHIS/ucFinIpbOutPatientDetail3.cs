using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.HPHIS
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
    public partial class ucFinIpbOutPatientDetail3 : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        private string feeType = "";
        
        public ucFinIpbOutPatientDetail3()
        {
            InitializeComponent();
        }

        [Category("控件设置"), Description("住院号输入框默认输入0住院号，5床号")]
        public int DefaultInput
        {
            get
            {
                return this.ucQueryInpatientNo1.DefaultInputType;
            }
            set
            {
                this.ucQueryInpatientNo1.DefaultInputType = value;
            }
        }

        [Category("控件设置"), Description("按床号查询患者信息时，按患者的状态查询，如果全部则'ALL'")]
        public string PatientInState
        {
            get
            {
                return this.ucQueryInpatientNo1.PatientInState;
            }
            set
            {
                this.ucQueryInpatientNo1.PatientInState = value;
            }
        }

        private Size printSize = new Size();
        [Category("控件设置"), Description("设置打印纸张的高度")]
        public int PrintHeight
        {
            get
            {
                return printSize.Height;
            }
            set
            {
                printSize.Height = value;
            }

        }
        [Category("控件设置"), Description("设置打印纸张的宽度")]
        public int PrintWidth
        {
            get
            {
                return printSize.Width;
            }
            set
            {
                printSize.Width = value;
            }

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
                if (!printSize.IsEmpty)
                {
                    dwMain.Modify("DataWindow.Print.Paper.Size=256");
                    //此处设置letter纸为：216*279 像素为850*1100
                    //dwMain.Modify("DataWindow.Print.CustomPage.Length=1100");
                    //dwMain.Modify("DataWindow.Print.CustomPage.Width=425");
                    //此处设置letter纸为：216*279
                    dwMain.Modify(string.Format("DataWindow.Print.CustomPage.Length={0}",printSize.Height));
                    dwMain.Modify(string.Format("DataWindow.Print.CustomPage.Width={0}", printSize.Width));
                }
                this.dwMain.Print();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印出错:" + ex.Message, "提示");
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
            inDay = radt.GetInDays(inpatientNO);// FS.HISFC.BizProcess.Integrate.Function.GetInHosDay(patientInfo);
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
                    base.OnRetrieve(inpatientNO, this.feeType, this.dtpBeginTime.Value, this.dtpEndTime.Value);
                    //dwMain.Modify("t_statdate.text = '统计时间:" + this.dtpBeginTime.Value.ToString() + " 至 " + this.dtpEndTime.Value.ToString() + "'");
                }
                else
                {
                    base.OnRetrieve(inpatientNO, this.feeType, DateTime.MinValue, DateTime.MaxValue);
                    //dwMain.Modify("t_statdate.visible = false");
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
