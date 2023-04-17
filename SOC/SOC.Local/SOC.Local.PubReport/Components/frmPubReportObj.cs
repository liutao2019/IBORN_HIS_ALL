using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.PubReport.Components
{
    public partial class frmPubReportObj : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmPubReportObj()
        {
            InitializeComponent();
            Init();
        }

        FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// 操作状态 所有事件在true的情况下才会响应
        /// </summary>
        bool IsEdit = true;
        DateTime staticMonth = new DateTime();

        public DateTime StaticMonth
        {
            get
            {
                return staticMonth;
            }
            set
            {
                staticMonth = value;
            }
        }

        static ArrayList alPubPactUint; 

        private void Init()
        {
            if (alPubPactUint == null)
            {
                alPubPactUint = pactMgr.QueryPactUnitByPayKindCode("03");
            }
            cmbPact.AddItems(alPubPactUint);
        }


        SOC.Local.PubReport.Models.PubReport pubReportObj = new SOC.Local.PubReport.Models.PubReport();

        void SetColor(FS.FrameWork.WinForms.Controls.NeuNumericTextBox txt)
        {
            if (txt.Text == "0.00")
            {
                txt.ForeColor = Color.Black;
            }
            else
            {
                txt.ForeColor = Color.Red;
            }
        }

        public void WriteToPanel(SOC.Local.PubReport.Models.PubReport obj)
        {
            IsEdit = false;
            dataChanged = false;
            this.txtInvNo.Text = obj.ID;
            this.txtPactHead.Text = obj.Pact.Memo;
            this.txtMcardNo.Text = obj.MCardNo;
            this.txtName.Text = obj.Name;
            this.cmbPact.Tag = obj.Pact.ID;
            this.txtPubCost.Text = obj.Pub_Cost.ToString();
            this.txtTotCost.Text = obj.Tot_Cost.ToString();
            decimal payCost = obj.Tot_Cost - obj.Pub_Cost;
            this.txtPayCost.Text = payCost.ToString();
            this.txtOperCode.Text = obj.OperCode.ToString();
            this.txtYaoPin.Text = obj.YaoPin.ToString();
            this.txtChengyao.Text = obj.ChengYao.ToString();
            this.txtCaoyao.Text = obj.CaoYao.ToString();
            this.txtJiancha.Text = obj.JianCha.ToString();
            txtZhiliao.Text = obj.ZhiLiao.ToString();
            txtShoushu.Text = obj.ShouShu.ToString();
            txtCT.Text = obj.CT.ToString();
            txtMR.Text = obj.MR.ToString();
            txtZhenjin.Text = obj.ZhenJin.ToString();
            txtShuYang.Text = obj.ShuYang.ToString();
            txtShuxue.Text = obj.ShuXue.ToString();
            txtTeJian.Text = obj.TeJian.ToString();
            txtHuayan.Text = obj.HuaYan.ToString();
            txtChuanwei.Text = obj.Bed_Fee.ToString();
            txtJianHu.Text = obj.JianHu.ToString();
            txtTeYao.Text = obj.SpDrugFeeTot.ToString();
            txtTeYaoRate.Text = obj.TeYaoRate.ToString();
            txtTeZhi.Text = obj.TeZhi.ToString();
            txtTeZhiRate.Text = obj.TeZhiRate.ToString();
            txtPatientNo.Text = obj.PatientNO;
            txtDays.Text = obj.Amount.ToString();

            //[2010-02-24]zhaozf 修改，添加自费金额和医疗费总金额（方便处理空军医院病人信息）
            txtSelfPay.Text = obj.SelfPay.ToString();
            txtTotalFee.Text = obj.TotalFee.ToString();
            txtOverLimitDurgFee.Text = obj.OverLimitDrugFee.ToString();

            if (obj.WorkCode != null)
            {
                obj.WorkCode = obj.WorkCode.Trim();
            }
            if (obj.WorkName != null)
            {
                obj.WorkName = obj.WorkName.Trim();
            }
            if (obj.WorkName == "")
            {
                SOC.Local.PubReport.BizLogic.PubReport pubMgr = new SOC.Local.PubReport.BizLogic.PubReport();
                FS.FrameWork.Models.NeuObject o = pubMgr.GetWorkHome(obj.PatientNO);
                obj.WorkName = o.Name;
                obj.WorkCode = o.ID;
            }
            txtWorkName.Text = obj.WorkName;
            txtWorkCode.Text = obj.WorkCode;
            obj.PatientNO = obj.PatientNO.Trim();
            this.txtWorkName.ForeColor = Color.Black;
            txtWorkCode.ForeColor = Color.Black;
            txtWorkCode.Text = obj.WorkCode;
            
            if (obj.OperDate == DateTime.MinValue)
            {
                obj.OperDate = new DateTime(1800,1,1);
            }
            if (obj.Begin == DateTime.MinValue)
            {
                obj.Begin = new DateTime(1800, 1, 1);
            }
            if (obj.End == DateTime.MinValue)
            {
                obj.End = new DateTime(1800, 1, 1);
            }
            dtBalance.Value = obj.OperDate;
            dtBegin.Value = obj.Begin;
            dtEnd.Value = obj.End;

            foreach (Control c in this.neuPanel1.Controls)
            {
                if (c is FS.FrameWork.WinForms.Controls.NeuNumericTextBox)
                { 
                    FS.FrameWork.WinForms.Controls.NeuNumericTextBox txt = c as FS.FrameWork.WinForms.Controls.NeuNumericTextBox;
                    SetColor(txt);
                }
            }
           
            obj.IsValid = "1";  //默认核对
            if (obj.IsValid == "0")
            {
                chkChecked.Checked = false;
            }
            else
            {
                chkChecked.Checked = true;
            }
            if (obj.Fee_Type == "1")
            {
                dtBegin.Visible = false;
                dtEnd.Visible = false;
                lbBegin.Visible = false;
                lbEnd.Visible = false;
            }
            else
            {
                dtBegin.Visible = true;
                dtEnd.Visible = true;
                lbBegin.Visible = true;
                lbEnd.Visible = true;
            }
            this.pubReportObj = obj.Clone();
            specialCheck = new SOC.Local.PubReport.Models.SpecialCheck();
            IsEdit = true;
        }

        public int Check(SOC.Local.PubReport.Models.PubReport obj)
        {
            this.pubReportObj = this.ReadFromPanel();
            if (obj.ID == "")
            {
                return 1;
            }
            decimal totCost = 0;
            decimal pubCost = 0;
            decimal ownCost = 0;
            this.GetSum(ref totCost, ref pubCost, ref ownCost);
            if (obj.Tot_Cost != totCost)
            {
                MessageBox.Show("总金额为" + obj.Tot_Cost.ToString() + " 计算的总金额" + totCost.ToString() + "不符,请核对");
                return -1;
            }
            if (obj.Pub_Cost != pubCost && obj.TeJian == 0)
            {
                MessageBox.Show("实际记账金额为" + obj.Pub_Cost.ToString() + " 计算的实际金额" + pubCost.ToString() + "不符,请核对");
                return -1;
            }
           return 1;
        }

        public SOC.Local.PubReport.Models.PubReport ReadFromPanel()
        {
            SOC.Local.PubReport.Models.PubReport obj = this.pubReportObj;
            obj.ID = this.txtInvNo.Text.Trim();
            obj.MCardNo = this.txtMcardNo.Text;
            obj.Name = this.txtName.Text;
            obj.Pact.ID = this.cmbPact.Tag.ToString();
            obj.Pact.Memo = this.txtPactHead.Text;
            obj.Pact.Name = this.cmbPact.Text;
            obj.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPubCost.Text);
            obj.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtTotCost.Text);
            obj.OperCode =this.txtOperCode.Text;
            obj.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.txtYaoPin.Text);
            obj.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.txtChengyao.Text);
            obj.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.txtCaoyao.Text);
            obj.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.txtJiancha.Text);
            obj.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(txtZhiliao.Text);
            obj.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(txtShoushu.Text);
            obj.CT = FS.FrameWork.Function.NConvert.ToDecimal(txtCT.Text);
            obj.MR = FS.FrameWork.Function.NConvert.ToDecimal(txtMR.Text);
            obj.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(txtZhenjin.Text);
            obj.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(txtShuYang.Text);
            obj.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(txtShuxue.Text);
            obj.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(txtTeJian.Text);
            obj.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(txtChuanwei.Text);
            obj.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(txtHuayan.Text);
            obj.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(txtJianHu.Text);
            obj.WorkName = txtWorkName.Text;
            obj.WorkCode = txtWorkCode.Text;
            obj.SpDrugFeeTot = FS.FrameWork.Function.NConvert.ToDecimal(txtTeYao.Text);
            obj.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(txtTeYao.Text);//审批肿瘤药费就是特药费
            obj.TeYaoRate = FS.FrameWork.Function.NConvert.ToDecimal(txtTeYaoRate.Text);
            obj.TeZhi = FS.FrameWork.Function.NConvert.ToDecimal(txtTeZhi.Text);
            obj.TeZhiRate = FS.FrameWork.Function.NConvert.ToDecimal(txtTeZhiRate.Text);
            obj.PatientNO = txtPatientNo.Text;

            //[2010-02-24]zhaozf 修改，添加自费金额和医疗费总金额（方便处理空军医院病人信息）
            obj.SelfPay = FS.FrameWork.Function.NConvert.ToDecimal(txtSelfPay.Text);
            obj.TotalFee = FS.FrameWork.Function.NConvert.ToDecimal(txtTotalFee.Text);
            obj.OverLimitDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(txtOverLimitDurgFee.Text);
            FS.HISFC.Models.Base.PactInfo pact = cmbPact.SelectedItem as FS.HISFC.Models.Base.PactInfo;
            if (pact == null)
            {
                pact = new FS.HISFC.Models.Base.PactInfo();
            }
            obj.Pay_Rate = pact.Rate.PayRate;
            obj.Begin = this.dtBegin.Value;
            obj.End = this.dtEnd.Value;
            obj.OperDate = this.dtBalance.Value;
            obj.Amount = FS.FrameWork.Function.NConvert.ToInt32(txtDays.Text);
            //specialCheck = new SOC.Local.PubReport.Models.SpecialCheck();
            

            if (chkChecked.Checked)
            {
                obj.IsValid = "1";
            }
            else
            {
                obj.IsValid = "0";
            }
            return obj;
        }

        public DialogResult dialogResult = DialogResult.Yes;

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!IsEdit)
            {
                return;
            }
            this.dialogResult = DialogResult.Yes;
            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!IsEdit)
            {
                return;
            }
            this.dialogResult = DialogResult.No;
            this.FindForm().Close();
        }

        private void txtInvNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsEdit)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                ComputeCost();
            }
        }

        private void cmbPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsEdit)
            {
                return;
            }
            IsEdit = false;
            FS.HISFC.Models.Base.PactInfo pactUint = this.cmbPact.SelectedItem as FS.HISFC.Models.Base.PactInfo;
            string pactHead = SOC.Local.PubReport.BizProcess.PubReport.GetPactHead(pactUint.ID);
            txtPactHead.Text = pactHead;
            this.pubReportObj = this.ReadFromPanel();
            ComputeCost();
            IsEdit = true;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (!IsEdit)
            {
                return;
            }
            IsEdit = false;
            SOC.Local.PubReport.Components.frmSpecailCheck frmSpecailCheck1 = new SOC.Local.PubReport.Components.frmSpecailCheck();
            frmSpecailCheck1.StaticMonth = this.StaticMonth;
            this.pubReportObj = this.ReadFromPanel();
            frmSpecailCheck1.SetPubObj(this.pubReportObj);
            frmSpecailCheck1.ShowDialog();
            this.specialCheck = frmSpecailCheck1.SpecialCheck.Clone();
            this.txtTeJian.Text = frmSpecailCheck1.SpecialCheck.TotCost.ToString();
            ComputeCost();

            IsEdit = true;
        }

        //private void GetSum(ref decimal totCost, ref decimal pubCost, ref decimal ownCost)
        //{
        //    if (this.pubReportObj.ID == "")
        //    {
        //        return;
        //    }
        //    decimal tempTot = this.pubReportObj.YaoPin + this.pubReportObj.JianHu 
        //        + this.pubReportObj.ChengYao + this.pubReportObj.JianCha + this.pubReportObj.ShouShu
        //        + this.pubReportObj.CT + this.pubReportObj.MR + this.pubReportObj.ShuXue 
        //        + this.pubReportObj.CaoYao + this.pubReportObj.ZhiLiao 
        //        + this.pubReportObj.ShuYang + this.pubReportObj.Bed_Fee 
        //        + this.pubReportObj.HuaYan;
        //    FS.HISFC.Models.Base.PactInfo pactUint = this.cmbPact.SelectedItem as FS.HISFC.Models.Base.PactInfo;
        //    if (pactUint == null)
        //    {
        //        return;
        //    }
        //    decimal zhenjinRate = 0;
        //    if (this.pubReportObj.Pact.Memo.StartsWith("8") || this.pubReportObj.Pact.Memo.StartsWith("J8") || this.pubReportObj.Pact.Memo.StartsWith("9"))
        //    {
        //        zhenjinRate = 0;
        //    }
        //    else
        //    {
        //        zhenjinRate = pactUint.Rate.PayRate;
        //    }

        //    //this.pubReportObj.Tot_Cost = tempTot;
        //    decimal zhenjinPay = FS.FrameWork.Public.String.FormatNumber(this.pubReportObj.ZhenJin * zhenjinRate, 2);
        //    decimal teyaoPay = FS.FrameWork.Public.String.FormatNumber(this.pubReportObj.SpDrugFeeTot * this.pubReportObj.TeYaoRate, 2);
        //    decimal tezhiPay = FS.FrameWork.Public.String.FormatNumber(this.pubReportObj.TeZhi * this.pubReportObj.TeZhiRate, 2);

        //    ownCost = tempTot * pactUint.Rate.PayRate + this.pubReportObj.ZhenJin * zhenjinRate
        //        + this.pubReportObj.SpDrugFeeTot * this.pubReportObj.TeYaoRate 
        //        + this.pubReportObj.TeZhi * this.pubReportObj.TeZhiRate
        //        +this.specialCheck.TotCost-specialCheck.PubCost;

        //    ownCost = FS.FrameWork.Public.String.FormatNumber(ownCost, 2);
        //    totCost = tempTot + this.pubReportObj.ZhenJin + this.pubReportObj.TeZhi + this.pubReportObj.SpDrugFeeTot + this.pubReportObj.TeJian;
        //    pubCost = totCost - ownCost;

        //    //pubCost = (this.pubReportObj.Tot_Cost) - FS.FrameWork.Public.String.FormatNumber((this.pubReportObj.Tot_Cost) * pactUint.Rate.PayRate, 2) + this.specialCheck.PubCost + zhenjinPub + teyaoPub + tezhiPub;
        //    //totCost = tempTot + this.pubReportObj.ZhenJin + this.pubReportObj.TeZhi + this.pubReportObj.SpDrugFeeTot + this.pubReportObj.TeJian;
        //    //ownCost = totCost - pubCost;

        //    return ;
        //}

        private void GetSum(ref decimal totCost, ref decimal pubCost, ref decimal ownCost)
        {
            if (this.pubReportObj.ID == "")
            {
                return;
            }
            decimal tempTot = this.pubReportObj.YaoPin + this.pubReportObj.JianHu
                + this.pubReportObj.ChengYao + this.pubReportObj.JianCha + this.pubReportObj.ShouShu
                + this.pubReportObj.CT + this.pubReportObj.MR + this.pubReportObj.ShuXue
                + this.pubReportObj.CaoYao + this.pubReportObj.ZhiLiao
                + this.pubReportObj.ShuYang + this.pubReportObj.Bed_Fee
                + this.pubReportObj.HuaYan;
            FS.HISFC.Models.Base.PactInfo pactUint = this.cmbPact.SelectedItem as FS.HISFC.Models.Base.PactInfo;
            if (pactUint == null)
            {
                return;
            }
            decimal zhenjinRate = 0;
            if (this.pubReportObj.Pact.Memo.StartsWith("8") || this.pubReportObj.Pact.Memo.StartsWith("J8") || this.pubReportObj.Pact.Memo.StartsWith("9"))
            {
                zhenjinRate = 0;
            }
            else
            {
                zhenjinRate = pactUint.Rate.PayRate;
            }

            //this.pubReportObj.Tot_Cost = tempTot;
            decimal zhenjinPay = FS.FrameWork.Public.String.FormatNumber(this.pubReportObj.ZhenJin * zhenjinRate, 2);
            decimal teyaoPay = FS.FrameWork.Public.String.FormatNumber(this.pubReportObj.SpDrugFeeTot * this.pubReportObj.TeYaoRate, 2);
            decimal tezhiPay = FS.FrameWork.Public.String.FormatNumber(this.pubReportObj.TeZhi * this.pubReportObj.TeZhiRate, 2);

            ownCost = tempTot * pactUint.Rate.PayRate + this.pubReportObj.ZhenJin * zhenjinRate
                + this.pubReportObj.SpDrugFeeTot * this.pubReportObj.TeYaoRate
                + this.pubReportObj.TeZhi * this.pubReportObj.TeZhiRate
                + this.specialCheck.TotCost - specialCheck.PubCost;

            ownCost = FS.FrameWork.Public.String.FormatNumber(ownCost, 2);
            totCost = tempTot + this.pubReportObj.ZhenJin + this.pubReportObj.TeZhi + this.pubReportObj.SpDrugFeeTot + this.pubReportObj.TeJian;
            pubCost = totCost - ownCost;

            //pubCost = (this.pubReportObj.Tot_Cost) - FS.FrameWork.Public.String.FormatNumber((this.pubReportObj.Tot_Cost) * pactUint.Rate.PayRate, 2) + this.specialCheck.PubCost + zhenjinPub + teyaoPub + tezhiPub;
            //totCost = tempTot + this.pubReportObj.ZhenJin + this.pubReportObj.TeZhi + this.pubReportObj.SpDrugFeeTot + this.pubReportObj.TeJian;
            //ownCost = totCost - pubCost;

            return;
        }
        
        private void ComputeCost()
        {
            this.pubReportObj = this.ReadFromPanel();
            decimal totCost = 0;
            decimal pubCost = 0;
            decimal ownCost = 0;
            this.GetSum(ref totCost, ref pubCost, ref ownCost);
            this.txtTotCost.Text =  totCost.ToString();
            this.txtPubCost.Text = pubCost.ToString();
            this.txtPayCost.Text = ownCost.ToString();
        }

        private void txtDetail_Leave(object sender, EventArgs e)
        {
            if (!IsEdit)
            {
                return;
            }
            IsEdit = false;
            //ComputeCost();
            IsEdit = true;
        }

        bool dataChanged = false;

        public bool DataChanged
        {
            get
            {
                return dataChanged;
            }
        }

        private void txtYaoPin_TextChanged(object sender, EventArgs e)
        {
            dataChanged = true;
        }

        SOC.Local.PubReport.Models.SpecialCheck specialCheck = new SOC.Local.PubReport.Models.SpecialCheck();
 
        private void btnComput_Click(object sender, EventArgs e)
        {
            this.pubReportObj = this.ReadFromPanel();
            this.pubReportObj.YaoPin = this.pubReportObj.YaoPin - this.specialCheck.Xianyin;
            this.pubReportObj.JianCha = this.pubReportObj.JianCha - this.specialCheck.Xiangji;
            this.txtYaoPin.Text = this.pubReportObj.YaoPin.ToString();
            this.txtJiancha.Text = this.pubReportObj.JianCha.ToString();
        }

        private FS.FrameWork.Models.NeuObject GetWorkHome()
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            string sql = @" select p.WORK_HOME,p.spell_code from com_patientinfo p 
 where p.CARD_NO = '{0}'";
            sql = string.Format(sql, this.pubReportObj.PatientNO);
            this.pactMgr.ExecQuery(sql);
            while (this.pactMgr.Reader.Read())
            {
                obj.Name = this.pactMgr.Reader[0].ToString();
                obj.ID = this.pactMgr.Reader[1].ToString();
            }
            return obj;
        }

        private void UpdateAddress(string newAddr,string workCode)
        {
            string sql = @" update com_patientinfo p 
 set p.WORK_HOME = '{1}',
     p.spell_code = '{2}'
 where p.CARD_NO ='{0}' ";
            sql = string.Format(sql, this.pubReportObj.PatientNO, newAddr,workCode);
            this.pactMgr.ExecNoQuery(sql);
        }

        private void frmPubReportObj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                FS.FrameWork.Models.NeuObject obj = GetWorkHome();
                this.txtWorkName.Text = obj.Name;
                txtWorkCode.Text = obj.ID;
            }
            else if (e.KeyCode == Keys.F4)
            {
                this.UpdateAddress(this.txtWorkName.Text,txtWorkCode.Text);
                this.txtWorkName.ForeColor = Color.Red;
                txtWorkCode.ForeColor = Color.Red;
            }
        }


    
    }

    
    enum Col
    {
        选择,
        ID,
        流水号,
        发票号,
        字头,
        医疗证号,
        姓名,
        结算方式,
        记帐金额,
        总金额,
        工号,
        药品,
        成药,
        草药,
        检查,
        特殊检查,
        治疗,
        手术,
        CT,
        MR,
        诊金,
        输氧,
        输血,
        化验,
        床位,
        监护,
        特殊药品,
        特药比例,
        特殊治疗,
        特殊治疗比例,
        入院日期,
        出院日期,
        结算日期,
        单位名称,
        单位代号
    }

    enum Col2
    {
        符号,
        来源,
        月份,
        发票号,
        操作员,
        地区,
        记帐金额,
        总金额,
        结算日期,  
        字头,
        医疗证号,
        姓名,
        备注
    }

    enum Col3
    {
        发票号,

        来源1,
        地区1,
        记帐金额1,
        总金额1,

        来源2,
        地区2,
        记帐金额2,
        总金额2,

        记帐差额,
        总金额差额,

        收费员,
        结算日期,
        医疗证号,
        姓名,
        备注
    }

    enum Col4
    {
        公医办发票号,
        公医办日期,
        公医办收费员,
        公医办记帐金额,
        公医办总金额,

        收费处发票号,
        收费处日期,
        收费处收费员,
        收费处记帐金额,
        收费处总金额,

        记帐差额,
        总金额差额,

        合同单位,
        医疗证号,
        病人姓名,
        结算日期,
        备注
    }
}
