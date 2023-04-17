using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    public partial class ucPatientPubInfo : UserControl
    {
        public ucPatientPubInfo()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.RADT.InPatient patientInfo = new FS.HISFC.BizLogic.RADT.InPatient();

        private FS.HISFC.BizLogic.Fee.InPatient inPatient = new FS.HISFC.BizLogic.Fee.InPatient();

        private FS.HISFC.Models.RADT.PatientInfo oEPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.oEPatientInfo; }
            set { this.oEPatientInfo = value; }
        }
        public void init()
        {

            //向患者信息控件赋值

            this.setValue();

            //超标处理
            ArrayList alLimit = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject O = new FS.FrameWork.Models.NeuObject();
            O.ID = "0";
            O.Name = "超标不限";
            alLimit.Add(O);

            obj.ID = "1";
            obj.Name = "超标自理";
            alLimit.Add(obj);

            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            obj1.ID = "2";
            obj1.Name = "超标不计";
            alLimit.Add(obj1);
            this.cmbBedOverDeal.AddItems(alLimit);
            //this.cmbBedOverDeal.isItemOnly = true;

            this.loadEven();

            txtBedInterval.Focus();

        }

        /// <summary>
        /// 诊断控件事件
        /// </summary>
        private void loadEven()
        {
            //回车跳转焦点		
            foreach (Control c in this.groupBox3.Controls)
            {
                c.KeyDown += new KeyEventHandler(c_KeyDown);
            }
        }

        /// <summary>
        /// 焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                System.Windows.Forms.SendKeys.Send("{tab}");
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                e.Handled = false;
            }

        }

        /// <summary>
        /// 检查输入错误
        /// </summary>
        /// <returns></returns>
        private int CheckInputValid()
        {




            if (FS.FrameWork.Function.NConvert.ToDecimal(this.txtDayLimit.Text) == 0)
            {
                MessageBox.Show("请输入公费患者的药品日限额", "提示");
                return -1;
            }

            //			if(FS.FrameWork.Function.NConvert.ToDecimal(this.txtDayLimit.Text)!=0) {
            //				MessageBox.Show("该患者不是公费患者,药品日限额输入金额无意义!","提示");
            //				return -1;
            //			}
            //			this.txtDayLimit.Text="0.00";		

            //判断床费间隔
            if (int.Parse(this.txtBedInterval.Text) < 1)
            {
                MessageBox.Show("床费收取时间间隔必须大于等于1", "提示");
                return -1;
            }

            //			//判断超标床和超标空调 自费患者没有报销超标所以都置为0
            //			if(this.PatientInfo.PayKind.ID=="01") {
            //				this.txtBedLimit.Text="0.00";
            //				this.txtAirLimit.Text="0.00";
            //				this.cmbBedOverDeal.Text="";
            //			}
            //判断超标处理
            if ((decimal.Parse(this.txtBedLimit.Text) > 0) && (this.cmbBedOverDeal.Text == ""))
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("请选择床费超标处理!", 111);
                this.cmbBedOverDeal.Focus();
                return -1;
            }

            try
            {
                if (this.PatientInfo.PVisit.MoneyAlert.ToString() == "") this.PatientInfo.PVisit.MoneyAlert = 0;
            }
            catch { this.PatientInfo.PVisit.MoneyAlert = 0; }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void setValue()
        {
            this.txtName.Text = PatientInfo.Name;
            this.txtDeptName.Text = PatientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtBalanceType.Text = PatientInfo.Pact.Name;//合同单位
            this.txtBedNo.Text = PatientInfo.PVisit.PatientLocation.Bed.ID;

            txtBirthday.Text = PatientInfo.Birthday.ToString("yyyy-MM-dd");
            txtNurseStation.Text = PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            txtDateIn.Text = PatientInfo.PVisit.InTime.ToString();
            txtDoctor.Text = PatientInfo.PVisit.AdmittingDoctor.Name;
            //			this.txtCost.Focus();

            //txtBedInterval.Text = this.oEPatientInfo
            this.txtBedInterval.Text = PatientInfo.FT.FixFeeInterval.ToString(); //床费间隔

            this.txtLimitTot.Text = PatientInfo.FT.DayLimitTotCost.ToString();//日限额累计

            this.txtDayLimit.Text = PatientInfo.FT.DayLimitCost.ToString();//日限额
            //this.txtDayLimit.Text = PatientInfo.FT.LimitTot.LimitTot.ToString();//日限额累计
            //			PatientInfo.Fee.Limit_OverTop=-PatientInfo.FT.DayLimitCost;//超标金额

            try
            {
                this.txtBedLimit.Text = PatientInfo.FT.BedLimitCost.ToString(); //床费上限
                this.txtAirLimit.Text = PatientInfo.FT.AirLimitCost.ToString(); //空调上限
            }
            catch
            {
                this.txtBedLimit.Text = "0";
                this.txtAirLimit.Text = "0";
            }
            try
            {
                this.cmbBedOverDeal.SelectedIndex = 1; //床费超标处理
            }
            catch { }

            //医疗证号
            this.txtSSN.Text = PatientInfo.SSN;// add by huangxw

        }

        private int getValue()
        {

            //txtBedInterval.Text = this.oEPatientInfo
            PatientInfo.FT.FixFeeInterval = int.Parse(this.txtBedInterval.Text); //床费间隔

            PatientInfo.FT.DayLimitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDayLimit.Text);//日限额
            PatientInfo.FT.DayLimitTotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtLimitTot.Text);//日限额累计

            PatientInfo.FT.OvertopCost = -PatientInfo.FT.DayLimitTotCost;//超标金额
            try
            {
                PatientInfo.FT.BedLimitCost = decimal.Parse(this.txtBedLimit.Text); //床费上限
                PatientInfo.FT.AirLimitCost = decimal.Parse(this.txtAirLimit.Text); //监护床标准
                PatientInfo.FT.BedOverDeal = this.cmbBedOverDeal.Tag.ToString(); //床费超标处理

                //判断床费间隔
                if (int.Parse(this.txtBedInterval.Text) < 1)
                {
                    MessageBox.Show("床费收取时间间隔必须大于等于1", "提示");
                    this.txtBedInterval.SelectAll();
                    return -1;
                }
                if (this.cmbBedOverDeal.Text == "" || this.cmbBedOverDeal.SelectedItem == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("请选择床费超标处理!", 111);
                    this.cmbBedOverDeal.Focus();
                    return -1;
                }
                FS.FrameWork.Models.NeuObject obj = (FS.FrameWork.Models.NeuObject)this.cmbBedOverDeal.SelectedItem;
                PatientInfo.FT.BedOverDeal = obj.ID;
                if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtSSN.Text.Trim(), 18) == false)
                {
                    MessageBox.Show("医疗证号长度不能超过18位数字!", "提示");
                    this.txtSSN.SelectAll();
                    return -1;
                }

                PatientInfo.SSN = this.txtSSN.Text.Trim();
                PatientInfo.Pact.PayKind.ID = "03";//公费
                if (PatientInfo.FT.BedLimitCost == 0)
                {
                    MessageBox.Show("床位标准不能为0!", "提示");
                    this.txtBedLimit.SelectAll();
                    return -1;
                }
                if (PatientInfo.FT.DayLimitCost == 0)
                {
                    MessageBox.Show("日限额不能为0", "提示");
                    this.txtDayLimit.SelectAll();
                    return -1;
                }
                if (PatientInfo.SSN == "")
                {
                    MessageBox.Show("请输入医疗证号", "提示");
                    this.txtSSN.SelectAll();
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
                return -1;
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.FindForm().Close();
        }

        private void ucPatientPubInfo_Load(object sender, System.EventArgs e)
        {
            this.init();

            //			this.txtBedInterval.Text = "0";
            this.txtBedInterval.SelectAll();
            this.txtBedInterval.Focus();
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (this.getValue() == -1) return;
            this.FindForm().Close();
        }

        private void txtDayLimit_Leave(object sender, System.EventArgs e)
        {
            decimal DayLimit = 0;
            decimal LimitTot = 0;
            string strDate = "";
            DateTime RepDate;
            DateTime DateNow;
            int Amount = 0;
            try
            {
                DayLimit = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDayLimit.Text);
                if (DayLimit <= 0) return;
                strDate = this.inPatient.GetPubReportDate("1");
                if (strDate == "-1")
                {
                    MessageBox.Show("查询月结时间出错" + this.inPatient.Err);
                    return;
                }
                DateNow = this.inPatient.GetDateTimeFromSysDateTime();
                RepDate = FS.FrameWork.Function.NConvert.ToDateTime(strDate);
                /*如果患者入院时间大于上次报表时间
                 * 取患者入院时间为开始时间*/
                if (this.PatientInfo.PVisit.InTime > RepDate)
                {
                    Amount = (DateNow.Date - this.PatientInfo.PVisit.InTime.Date).Days + 1;
                }
                else
                {
                    Amount = (DateNow.Date - RepDate.Date).Days + 1;
                }
                LimitTot = DayLimit * Amount;
                this.txtLimitTot.Text = LimitTot.ToString("##.00");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
