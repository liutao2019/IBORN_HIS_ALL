using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Neusoft.SOC.Local.RADT.GuangZhou.GYSY.Base.Inpatient
{
    public partial class ucDayLimitTot : UserControl
    {
        public ucDayLimitTot()
        {
            InitializeComponent();
        }

        #region 定义域
        private Neusoft.HISFC.BizLogic.RADT.InPatient patientInfo = new Neusoft.HISFC.BizLogic.RADT.InPatient();

        private Neusoft.HISFC.BizLogic.Fee.InPatient inPatient = new Neusoft.HISFC.BizLogic.Fee.InPatient();

        private Neusoft.HISFC.Models.RADT.PatientInfo oEPatientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();

        //变更后日限额
        Neusoft.FrameWork.Models.NeuObject newObj = new Neusoft.FrameWork.Models.NeuObject();
        //变更后日限额
        Neusoft.FrameWork.Models.NeuObject oldObj = new Neusoft.FrameWork.Models.NeuObject();

        #endregion

        /// <summary>
        /// 受txtinpatientno控件委托的事件
        /// </summary>
        private void txtInpatientNo_myEvent()
        {
            //			this.Clear();			
            //判断是否有该患者
            if (this.txtInpatientNo.InpatientNo == null || this.txtInpatientNo.InpatientNo.Trim() == "")
            {
                MessageBox.Show(this.txtInpatientNo.Err);
                this.txtInpatientNo.Focus();
                return;
            }
            else
            {//获得患者信息实体
                this.oEPatientInfo = this.patientInfo.QueryPatientInfoByInpatientNO(this.txtInpatientNo.InpatientNo);
                //向患者信息控件赋值
                if (this.oEPatientInfo.PayKind.ID == "03")
                {//判断合同单为是否为公费患者
                    //设置患者信息到控件
                    this.setValue();
                }
                else
                {
                    MessageBox.Show("该患者不是公费患者,不能进行修改日限额累计!");
                    return;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void setValue()
        {
            this.txtCost.Text = Neusoft.FrameWork.Public.String.FormatNumber(oEPatientInfo.FT.DayLimitTotCost, 2).ToString();//日限额累计

            this.txtName.Text = oEPatientInfo.Name;
            this.txtDeptName.Text = oEPatientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtBalanceType.Text = oEPatientInfo.Pact.Name;//合同单位
            this.txtBedNo.Text = oEPatientInfo.PVisit.PatientLocation.Bed.ID;

            txtBirthday.Text = oEPatientInfo.Birthday.ToString("yyyy-MM-dd");
            txtNurseStation.Text = oEPatientInfo.PVisit.PatientLocation.NurseCell.Name;
            txtDateIn.Text = oEPatientInfo.PVisit.InTime.ToString();
            txtDoctor.Text = oEPatientInfo.PVisit.AdmittingDoctor.Name;
            this.txtCost.Focus();
        }
        /// <summary>
        /// 清控患者实体
        /// </summary>
        private void Clear()
        {
            this.txtInpatientNo.Text = "";
            this.txtCost.Text = "0.00";
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    foreach (System.Windows.Forms.Control crl in c.Controls)
                    {
                        if (crl.GetType() != typeof(System.Windows.Forms.Label) && crl.GetType() != typeof(System.Windows.Forms.Button))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                        }
                    }
                }
            }
            this.oEPatientInfo.ID = "";

        }
        /// <summary>
        /// 修改日限额事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            //有效性判断
            if (!this.validCost()) return;
            //更新操作
            if (this.updateLimCost() == -1) return;

            MessageBox.Show("修改成功!");
            //清空
            this.Clear();
            this.txtInpatientNo.Focus();
        }

        /// <summary>
        /// 修改日先累计方法
        /// </summary>
        /// <returns></returns>
        private int updateLimCost()
        {
            //定义累计日限额			
            decimal dCost = 0.00m;
            newObj.ID = this.txtCost.Text;//变更后的金额
            newObj.Name = this.txtCost.Text;
            dCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.txtCost.Text.ToString());
            oldObj.ID = this.oEPatientInfo.FT.DayLimitTotCost.ToString();//变更前的金额
            oldObj.Name = this.oEPatientInfo.FT.DayLimitTotCost.ToString();
            //开始一个事物
            //neusoft.neuFC.Management.Transaction t = new neusoft.neuFC.Management.Transaction(this.inPatient.Connection);
            //t.BeginTransaction();
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.inPatient.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            this.patientInfo.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            //更新日限额方法
            if (this.inPatient.UpdateLimitTot(this.oEPatientInfo.ID, dCost) < 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("修改日限额累计出错!" + this.inPatient.Err, 211);
                return -1;
            }
            //插入变更日志
            if (this.patientInfo.SetShiftData(this.txtInpatientNo.InpatientNo, Neusoft.HISFC.Models.Base.EnumShiftType.BT, "日限额累计", oldObj, newObj,oEPatientInfo.IsBaby) != 1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("插入变更出错!" + this.patientInfo.Err, 211);
                return -1;
            }
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            return 0;
        }
        /// <summary>
        /// 判断输入的数据是否数字
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public bool IsNumber(string strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            string strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(strNumber) &&
                !objTwoDotPattern.IsMatch(strNumber) &&
                !objTwoMinusPattern.IsMatch(strNumber) &&
                objNumberPattern.IsMatch(strNumber);

        }
        /// <summary>
        /// 验证修改的日限累计额
        /// </summary>
        /// <returns></returns>
        private bool validCost()
        {
            //判断是否有患者信息
            if (this.oEPatientInfo == null)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("请输入患者!", 111);
                this.txtInpatientNo.Focus();
                return false;
            }
            if (this.oEPatientInfo.ID == "")
            {
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("请输入患者!", 111);
                this.txtInpatientNo.Focus();
                return false;
            }


            if (IsNumber(this.txtCost.Text))
            {//判断输入是否数值
                if (Neusoft.FrameWork.Function.NConvert.ToDecimal(this.txtCost.Text) < 0)
                {
                    MessageBox.Show("请输入大于0的数字!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("您输入的不是数字!");
                return false;
            }
            //判断修改前后金额是否一致
            if (this.oEPatientInfo.FT.DayLimitTotCost == Neusoft.FrameWork.Function.NConvert.ToDecimal(this.txtCost.Text))
            {
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("修改前累计金额等于修改后累计金额,请重新输入", 111);
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {


            this.FindForm().Close();

        }

        private void txtCost_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnOK.Focus();
            }
        }

        private void txtInpatientNo_Load(object sender, System.EventArgs e)
        {
            //设置住院号控件风格
            this.txtInpatientNo.TextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }
    }
}
