using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.PubReport.Components
{
    public partial class ucModifyPublicBill : UserControl
    {
        public ucModifyPublicBill()
        {
            InitializeComponent();
        }

        #region 变量

        FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
        SOC.Local.PubReport.Models.PubReport report = new SOC.Local.PubReport.Models.PubReport();
        SOC.Local.PubReport.BizLogic.PubReport myReport = new SOC.Local.PubReport.BizLogic.PubReport();

        #endregion

        #region 属性
        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                p = value;
            }
        }
        /// <summary>
        /// 托收信息
        /// </summary>
        public SOC.Local.PubReport.Models.PubReport Report
        {
            set
            {
                report = value;
            }
        }

        #endregion

        #region 函数

        public int SetInfo()
        {
            if (p == null)
            {
                MessageBox.Show("没有患者基本信息!");
                return -1;
            }
            if (report == null)
            {
                MessageBox.Show("没有患者托收信息");
                return -1;
            }
            //this.tbPatientNo.Text = p.Patient.PID.PatientNO;
            this.tbPatientNo.Text = p.PID.PatientNO;
            this.tbName.Text = p.Name;
            this.tbPact.Text = p.Pact.Name;
            this.tbWork.Text = p.CompanyName;//p.Patient.AddressBusiness;
            this.tbMonth.Text = report.Static_Month.Year.ToString() + "年" + report.Static_Month.Month.ToString() + "月";
            this.tbMCardNo.Text = report.MCardNo;
            this.dtpBegin.Value = report.Begin;
            this.dtpEnd.Value = report.End;
            this.tbBill.Text = report.Bill_No;

            //费用

            this.tbBedFee.Text = report.Bed_Fee.ToString();
            this.tbYaoPin.Text = report.YaoPin.ToString();
            this.tbChengYao.Text = report.ChengYao.ToString();
            this.tbHuaYan.Text = report.HuaYan.ToString();
            this.tbJianCha.Text = report.JianCha.ToString();
            this.tbFangShe.Text = report.FangShe.ToString();
            this.tbZhiLiao.Text = report.ZhiLiao.ToString();
            this.tbShouShu.Text = report.ShouShu.ToString();
            this.tbShuXue.Text = report.ShuXue.ToString();
            this.tbShuYang.Text = report.ShuYang.ToString();
            this.tbJieSheng.Text = report.JieSheng.ToString();
            this.tbGaoYang.Text = report.GaoYang.ToString();
            this.tbMR.Text = report.MR.ToString();
            this.tbCT.Text = report.CT.ToString();
            this.tbXueTou.Text = report.XueTou.ToString();
            this.tbZhenJin.Text = report.ZhenJin.ToString();
            this.tbCaoYao.Text = report.CaoYao.ToString();
            this.tbTeJian.Text = report.TeJian.ToString();
            this.tbShenYao.Text = report.ShenYao.ToString();
            this.tbJianHu.Text = report.JianHu.ToString();
            this.tbShengZhen.Text = report.ShengZhen.ToString();
            this.tbTot.Text = report.Tot_Cost.ToString();
            this.tbReal.Text = report.Pub_Cost.ToString();
            this.tbRate.Text = (report.Pay_Rate * 100).ToString();
            this.tbPay.Text = (report.Tot_Cost - report.Pub_Cost).ToString();
            if (report.IsInHos == "1")//在院
            {
                this.cmbInstate.Text = "在院";
            }
            else
            {
                this.cmbInstate.Text = "出院";
            }
            ArrayList al = new ArrayList();
            p.User01 = report.Begin.ToString();
            p.User02 = report.End.ToString();
            al.Add(p);
            //FS.Common.Controls.Function.AddControlToPanel(al, new Control.ucTrusteeBill(), this.plPrint, new System.Drawing.Size(850, 1164));
            //FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(al, new SOC.Local.PubReport.Components.ucTrusteeBill(), this.plPrint, new System.Drawing.Size(850, 1164), 0);
            FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(al, new ucTrusteeBill(), this.plPrint, new System.Drawing.Size(850, 1164),0);
            return 0;
        }

        private bool IsValid()
        {
            decimal decTemp = 0;
            decimal decTot = 0;
            try
            {
                foreach (System.Windows.Forms.Control c in this.gbFee.Controls)
                {
                    if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuTextBox))
                    {
                        if (c.Name == "tbRate" || c.Name == "tbPay" || c.Name == "tbShengZhen")
                        {
                            continue;
                        }
                        else
                        {
                            decTemp = FS.FrameWork.Function.NConvert.ToDecimal(c.Text);
                            decTot += decTemp;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入的数据不合法!" + "/n" + ex.Message);
                return false;
            }
            if (decTot - FS.FrameWork.Function.NConvert.ToDecimal(this.tbReal.Text) - FS.FrameWork.Function.NConvert.ToDecimal(this.tbTot.Text) !=
                FS.FrameWork.Function.NConvert.ToDecimal(this.tbTot.Text))
            {
                MessageBox.Show("调整后的费用总额 不等于原始费用总额，请重新调整!");
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.tbMCardNo.Text, 20))
            {
                MessageBox.Show("公费卡号不能超过20位");
                return false;
            }

            return true;
        }

        public int Modify()
        {
            if (!IsValid())
            {
                return -1;
            }
            //FS.neuFC.Management.Transaction t = new FS.neuFC.Management.Transaction(myReport.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myReport.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            report.MCardNo = this.tbMCardNo.Text.Trim();

            report.Begin = this.dtpBegin.Value;
            report.End = this.dtpEnd.Value;
            if (p.PVisit.InState.ID.ToString() == "O")
            {
                report.Amount = (report.End - report.Begin).Days;
            }
            else
            {
                report.Amount = (report.End - report.Begin).Days + 1;
            }

            //费用

            report.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(tbBedFee.Text);
            report.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(tbYaoPin.Text);
            report.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.tbChengYao.Text);
            report.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.tbHuaYan.Text);
            report.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.tbJianCha.Text);
            report.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.tbFangShe.Text);
            report.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.tbZhiLiao.Text);
            report.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.tbShouShu.Text);
            report.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.tbShuXue.Text);
            report.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.tbShuYang.Text);
            report.JieSheng = FS.FrameWork.Function.NConvert.ToDecimal(this.tbJieSheng.Text);
            report.GaoYang = FS.FrameWork.Function.NConvert.ToDecimal(this.tbGaoYang.Text);
            report.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.tbMR.Text);
            report.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.tbCT.Text);
            report.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.tbXueTou.Text);
            report.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.tbZhenJin.Text);
            report.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.tbCaoYao.Text);
            report.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.tbTeJian.Text);
            report.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.tbShenYao.Text);
            report.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.tbJianHu.Text);
            report.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.tbShengZhen.Text);
            report.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbTot.Text);
            report.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbReal.Text);
            if (this.cmbInstate.Text == "在院")
            {
                report.IsInHos = "1";
            }
            else
            {
                report.IsInHos = "0";
            }

            int iReturn = myReport.UpdatePubReport(report);
            if (iReturn <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新失败!" + myReport.Err);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("修改成功!");

            ArrayList al = new ArrayList();
            p.User01 = report.Begin.ToString();
            p.User02 = report.End.ToString();
            al.Add(p);
            //FS.Common.Controls.Function.AddControlToPanel(al, new Control.ucTrusteeBill(), this.plPrint, new System.Drawing.Size(850, 1164));
            FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(al, new ucTrusteeBill(), this.plPrint, new System.Drawing.Size(850, 1164),0);
            return 0;
        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //FS.Common.Class.Function.GetPageSize("bill", ref p);
            p.SetPageSize((new FS.HISFC.BizLogic.Manager.PageSize()).GetPageSize("bill"));
            p.PrintPage(0, 0, this.plPrint);
        }

        #endregion

        private void tbMake_Click(object sender, System.EventArgs e)
        {
            ArrayList al = new ArrayList();
            p.User01 = report.Begin.ToString();
            p.User02 = report.End.ToString();
            al.Add(p);
            //FS.Common.Controls.Function.AddControlToPanel(al, new Control.ucTrusteeBill(), this.plPrint, new System.Drawing.Size(850, 1164));
            FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(al, new ucTrusteeBill(), this.plPrint, new System.Drawing.Size(850, 1164),0);
        }

        private void tbPrint_Click(object sender, System.EventArgs e)
        {
            this.Print();
        }

        private void tbOk_Click(object sender, System.EventArgs e)
        {
            this.Modify();
        }

        private void tbExit_Click(object sender, System.EventArgs e)
        {
            this.FindForm().Close();
        }

        private void tbCompute_Click(object sender, System.EventArgs e)
        {
            decimal payCost = 0;
            try
            {
                report.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(tbBedFee.Text);
                report.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(tbYaoPin.Text);
                report.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.tbChengYao.Text);
                report.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.tbHuaYan.Text);
                report.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.tbJianCha.Text);
                report.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.tbFangShe.Text);
                report.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.tbZhiLiao.Text);
                report.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.tbShouShu.Text);
                report.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.tbShuXue.Text);
                report.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.tbShuYang.Text);
                report.JieSheng = FS.FrameWork.Function.NConvert.ToDecimal(this.tbJieSheng.Text);
                report.GaoYang = FS.FrameWork.Function.NConvert.ToDecimal(this.tbGaoYang.Text);
                report.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.tbMR.Text);
                report.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.tbCT.Text);
                report.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.tbXueTou.Text);
                report.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.tbZhenJin.Text);
                report.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.tbCaoYao.Text);
                report.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.tbTeJian.Text);
                report.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.tbShenYao.Text);
                report.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.tbJianHu.Text);
                report.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.tbShengZhen.Text);
                payCost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbPay.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入的金额不合法!" + ex.Message);
                return;
            }
            report.Tot_Cost = report.Bed_Fee + report.YaoPin + report.ChengYao + report.HuaYan
                + report.JianCha + report.FangShe + report.ZhiLiao + report.ShouShu + report.ShuXue
                + report.ShuYang + report.JieSheng + report.GaoYang + report.MR + report.CT
                + report.XueTou + report.ZhenJin + report.CaoYao + report.TeJian + report.ShenYao
                + report.JianHu;
            report.Pub_Cost = report.Tot_Cost - payCost;
            this.tbTot.Text = report.Tot_Cost.ToString();
            this.tbReal.Text = report.Pub_Cost.ToString();
            this.tbPay.Text = (report.Tot_Cost - report.Pub_Cost).ToString();


        }
    }
}
