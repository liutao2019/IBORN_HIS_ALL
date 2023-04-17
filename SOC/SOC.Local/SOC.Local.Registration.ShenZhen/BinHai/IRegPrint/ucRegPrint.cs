using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.FrameWork.Models;
using Neusoft.HISFC.Models.Registration;
using System.Collections;

namespace Neusoft.SOC.Local.Registration.ShenZhen.BinHai.IRegPrint
{
    public partial class ucRegPrint : UserControl
    {
        public ucRegPrint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置打印值
        /// </summary>
        /// <param name="register">挂号实体</param>
        /// <returns></returns>
        public int SetPrintValue(Neusoft.HISFC.Models.Registration.Register register)
        {

            this.lblHosptialName.Text = Neusoft.FrameWork.Management.Connection.Hospital.Name;

            MessageBox.Show("请记录门诊号：" + register.PID.CardNO);
            try
            {
                // 卡费用，不包含在挂号总费用中
                // 但打印时，在一起打印
                decimal decCardFee = 0;
                if (register.User01 == "CARDFEE")
                {
                    decCardFee = Neusoft.FrameWork.Function.NConvert.ToDecimal(register.User02);
                }


                this.InitReceipt();
                //门诊号
                this.lblCardNo.Text = register.PID.CardNO;
                //挂号科室
                this.lblDeptName.Text = register.DoctorInfo.Templet.Dept.Name;
                //号别
                this.lblRegLevel.Text = register.DoctorInfo.Templet.RegLevel.Name;
                //挂号发票号
                this.lblInvoiceno.Text = register.InvoiceNO;
                //姓名
                this.lblPatientName.Text = register.Name;
                //挂号员号
                this.lblRegOper.Text = register.InputOper.ID;
                //小记                
                this.lblCostsum.Text = Neusoft.FrameWork.Public.String.FormatNumberReturnString(
                    register.PubCost + register.PayCost + register.OwnCost + decCardFee, 2) +
                    "元";
                //大写
                this.lblUpperCostSum.Text = Neusoft.FrameWork.Public.String.LowerMoneyToUpper(
                   register.PubCost + register.PayCost + register.OwnCost + decCardFee
                    );
                //挂号日期
                this.lblRegDate.Text = register.DoctorInfo.SeeDate.ToString();
                string medicalTypeName = string.Empty;

                //this.lblPayCostTitle.Visible = false;
                //this.lblOwnCostTitle.Visible = false;
                //this.lblIndividualBalanceTitle.Visible = false;
                //register.Pact.ID = "2";

                //医疗类别
                this.lblPactName.Text = register.Pact.Name + medicalTypeName;

                //挂号费 
                this.lblRegFee.Text = Neusoft.FrameWork.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.RegFee, 2) +
                    "元";
                //诊察费
                this.lblChkFee.Text = Neusoft.FrameWork.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.ChkFee + register.RegLvlFee.PubDigFee + register.RegLvlFee.OwnDigFee, 2) +
                    "元";
                //病历手册 
                this.lblOherFee.Text = Neusoft.FrameWork.Public.String.FormatNumberReturnString(
                    register.RegLvlFee.OthFee, 2) +
                    "元";

                // 卡工本费
                if (decCardFee > 0)
                {
                    lblCardFeeTitle.Visible = true;
                    lblCardFee.Visible = true;

                    lblCardFee.Text = Neusoft.FrameWork.Public.String.FormatNumberReturnString(decCardFee, 2) + "元";

                }


                SetToPrintMode();
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 0;
        }

        #region 报表打印用函数
        /// <summary>
        /// 设置为打印模式
        /// </summary>
        private void SetToPrintMode()
        {
            //将预览项设为不可见
            SetLableVisible(false, lblPreview);
            foreach (Label lbl in lblPrint)
            {
                lbl.BorderStyle = BorderStyle.None;
                lbl.BackColor = SystemColors.ControlLightLight;
            }
        }

        /// <summary>
        /// 打印用的标签集合
        /// </summary>
        private Collection<Label> lblPrint;
        /// <summary>
        /// 预览用的标签集合
        /// </summary>
        private Collection<Label> lblPreview;

        /// <summary>
        /// 初始化收据
        /// </summary>
        /// <remarks>
        /// 把打印项和预览项根据ｔａｇ标签的值区分开
        /// </remarks>
        private void InitReceipt()
        {
            lblPreview = new Collection<Label>();
            lblPrint = new Collection<Label>();
            foreach (Control c in this.Controls)
            {
                if (c.GetType().FullName == "System.Windows.Forms.Label" ||
                    c.GetType().FullName == "Neusoft.FrameWork.WinForms.Controls.NeuLabel")
                {
                    Label l = (Label)c;
                    if (l.Tag != null)
                    {
                        if (l.Tag.ToString() == "print")
                        {
                            #region 将代印字的打印项值清空
                            if (!string.IsNullOrEmpty(l.Text) && l.Text == "印")
                            {
                                l.Text = "";
                            }
                            #endregion
                            lblPrint.Add(l);
                        }
                        else
                        {
                            lblPreview.Add(l);
                        }
                    }
                    else
                    {
                        lblPreview.Add(l);
                    }
                }
            }
        }
        /// <summary>
        /// 设置标签集合的可见性
        /// </summary>
        /// <param name="v">是否可见</param>
        /// <param name="l">标签集合</param>
        private void SetLableVisible(bool v, Collection<Label> l)
        {
            foreach (Label lbl in l)
            {
                lbl.Visible = v;
            }
        }

        #endregion
    }
}
