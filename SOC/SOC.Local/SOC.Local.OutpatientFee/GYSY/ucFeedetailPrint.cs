using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.GYSY
{
    public partial class ucFeedetailPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFeedetailPrint()
        {
            InitializeComponent();
            this.ClearInfo();
        }

        #region 变量
        int maxRowNumPerPage = 15;  //每页显示的行数
        FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.BizProcess.Integrate.Manager mger = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.FrameWork.Management.DataBaseManger DSManager = new FS.FrameWork.Management.DataBaseManger();
        #endregion
        
        /// <summary>
        /// 获取患者基本挂号信息 填充到neuSpread1_Sheet1中
        /// </summary>
        /// <param name="clinicCode">患者门诊号</param>
        /// <returns>成功返回1  失败返回-1</returns>
        private int QueryOutpatientInfo(string clinicCode)
        {     
            
            string sqlFeeInfo = @"select p.custom_code, --0项目编码
       p.trade_name, --1项目名称
       f.specs, --2规格
       f.unit_price, --3单价
       f.qty, --4数量
       f.price_unit, --5计价单位       
       f.pub_cost + f.pay_cost + f.own_cost, --6金额
       f.own_cost, --7自费额
       f.pay_cost,--8自负金额
       --100 * (f.own_cost / (f.pub_cost + f.pay_cost + f.own_cost)) || '%', --8自费比例
       f.eco_cost, --9优惠金额
       f.DOCT_CODE, --10医生
       decode(o.CANCEL_FLAG,'0','退费','1','有效','2','重打','3','注销'),      --11发票状态
       f.FEE_CPCD,         --12操作员
       f.FEE_DATE,            --13收费时间
       decode(c.CENTER_ITEM_GRADE,'1','甲类','2','乙类','自费'),    --14医保等级
       c.CENTER_RATE,     --15自负比例
o.own_cost, --16自费总额
       o.pay_cost, --17自付总额
    o.tot_cost --18总额
  from fin_opb_feedetail f, pha_com_baseinfo p,fin_opb_invoiceinfo o,
  (select tem.his_code,tem.center_item_grade,tem.center_rate from fin_com_compare tem where tem.pact_code in(
SELECT  code 
FROM  com_dictionary
WHERE  TYPE='gzyb' and mark='param' and rownum=1)) c
 where f.item_code = p.drug_code
   and f.invoice_no=o.invoice_no
   and f.item_code=c.his_code(+)
   and f.INVOICE_SEQ=o.INVOICE_SEQ
   and f.trans_type=o.trans_type
   and f.drug_flag = '1'
   and f.cancel_flag in ('1', '2')
   and f.invoice_no = '{0}'
   and o.trans_type='1'
   union all   
   select decode(d.gb_code, null, d.input_code, d.gb_code), --0项目编码
       decode(d.item_noarea, null, d.item_name, d.item_noarea), --1项目名称
       f.specs, --2规格
       f.unit_price, --3单价
       f.qty, --4数量
       f.price_unit, --5计价单位
       f.pub_cost + f.pay_cost + f.own_cost, --6金额
       f.own_cost, --7自费额
       f.pay_cost,--8自负金额
       --100 * (f.own_cost / (f.pub_cost + f.pay_cost + f.own_cost)) || '%', --8自费比例
       f.eco_cost, --9优惠金额
       f.DOCT_CODE, --10医生
       decode(o.CANCEL_FLAG,'0','退费','1','有效','2','重打','3','注销'),      --11发票状态
       f.FEE_CPCD,         --12操作员
       f.FEE_DATE,            --13收费时间
       decode(c.CENTER_ITEM_GRADE,'1','甲类','2','乙类','自费'),    --14医保等级
        c.CENTER_RATE,     --15自负比例
o.own_cost, --16自费总额
       o.pay_cost, --17自付总额
        o.tot_cost --18总额
  from fin_opb_feedetail f, fin_com_undruginfo d,fin_opb_invoiceinfo o,
  (select tem.his_code,tem.center_item_grade,tem.center_rate from fin_com_compare tem where tem.pact_code in(
SELECT  code 
FROM  com_dictionary
WHERE  TYPE='gzyb' and mark='param' and rownum=1)) c
 where f.item_code = d.item_code
   and f.invoice_no=o.invoice_no
   and f.item_code=c.his_code(+)
   and f.INVOICE_SEQ=o.INVOICE_SEQ
   and f.trans_type=o.trans_type  
   and f.drug_flag = '0' 
   and f.cancel_flag in ('1', '2')
   and f.invoice_no = '{0}'
   and o.trans_type='1'";
            sqlFeeInfo = string.Format(sqlFeeInfo, clinicCode);
            DataSet FeeInfoDS = new DataSet();            
            if (DSManager.ExecQuery(sqlFeeInfo, ref FeeInfoDS) == -1)
            {
                MessageBox.Show("获取患者费用明细失败！" + DSManager.Err);
                return -1;
            }
            if (FeeInfoDS == null)
            {
                MessageBox.Show("获取患者信息失败！");
                return -1;
            }
            if (FeeInfoDS.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("该患者没有费用信息，请检查输入的门诊号是否正确");
                return -1;
            }
            //int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(FeeInfoDS.Tables[0].Rows.Count) / 14));
            //if (true)
            //{
                
            //}
            if (neuSpread1_Sheet1.Rows.Count>0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, neuSpread1_Sheet1.Rows.Count);  
            }           
            for (int row = 0; row < FeeInfoDS.Tables[0].Rows.Count; row++)
            {
                this.neuSpread1_Sheet1.Rows.Add(row, 1);
                this.neuSpread1_Sheet1.Cells[row, 0].Text = FeeInfoDS.Tables[0].Rows[row][1].ToString();
                this.neuSpread1_Sheet1.Cells[row, 1].Text = FeeInfoDS.Tables[0].Rows[row][2].ToString();
                this.neuSpread1_Sheet1.Cells[row, 2].Text = FeeInfoDS.Tables[0].Rows[row][3].ToString();
                this.neuSpread1_Sheet1.Cells[row, 3].Text = FeeInfoDS.Tables[0].Rows[row][4].ToString();
                this.neuSpread1_Sheet1.Cells[row, 4].Text = FeeInfoDS.Tables[0].Rows[row][5].ToString();
                this.neuSpread1_Sheet1.Cells[row, 5].Text = FeeInfoDS.Tables[0].Rows[row][6].ToString();
                this.neuSpread1_Sheet1.Cells[row, 6].Text = FeeInfoDS.Tables[0].Rows[row][14].ToString() + "[" + FeeInfoDS.Tables[0].Rows[row][15].ToString()+"%]"; //医保等级                      
            }         
            this.lbDoctorName.Text = mger.GetEmployeeInfo(FeeInfoDS.Tables[0].Rows[0][10].ToString()).Name;  //医生       
            this.lbFeeStatus.Text = FeeInfoDS.Tables[0].Rows[0][11].ToString();  //发票状态
            this.lbOprater.Text = mger.GetEmployeeInfo(FeeInfoDS.Tables[0].Rows[0][12].ToString()).Name;  //操作员
            this.lbFeeTime.Text = FeeInfoDS.Tables[0].Rows[0][13].ToString();  //收费时间
            this.lbPrintTime.Text = feeMgr.GetSysDateTime();

            string sqlPqtientInfo = @"select       
       r.name,        --0患者姓名
       r.sex_code,    --1性别
       r.birthday,    --2生日
       f.oper_date,    --3挂号日期
       r.card_no, --4门诊号
       r.dept_name,   --5科室
       --r.paykind_code,--6结算类别 --del xf 考虑挂号表里的合同单位到收费时可以选择合同单位那么结算类别也就改变了，所以取发票表里的结算类别
       decode(f.paykind_code,'01','自费','02','医保','03','公费','04','其他'),--6结算类别
       r.pact_name,    --7合同单位 --结算方式到底是哪一个呢？ 住院登记时界面上显示的结算方式是pact_name 这里也用这个吧
       f.invoice_no,  --8发票号      
       f.tot_cost,--9总额
       f.own_cost,--10自费金额
       f.pub_cost,  --11记账金额       
       f.pay_cost,--12自付金额
       r.REGLEVL_NAME  --13挂号级别       
  from fin_opr_register r, fin_opb_invoiceinfo f
 where r.clinic_code = f.clinic_code
 and f.invoice_seq in (select t.invoice_seq from fin_opb_invoiceinfo t where t.invoice_no = '{0}' )
 and f.CANCEL_FLAG = '1'";
            sqlPqtientInfo = string.Format(sqlPqtientInfo, clinicCode);
            DataSet patientDS = new DataSet();

            if (DSManager.ExecQuery(sqlPqtientInfo, ref patientDS) == -1)
            {
                MessageBox.Show("获取患者信息失败！" + DSManager.Err);
                return -1;
            }
            if (patientDS == null)
            {
                MessageBox.Show("获取患者信息失败！");
                return -1;
            }
            if (patientDS.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("没有该发票号相关的数据，请检查输入的发票号是否正确");
                this.txtInvoiceNo.Focus();
                return -1;
            }
            string invoNo = "";
            decimal total = 0;
            decimal own_cost = 0;
            decimal pub_cost = 0;
            decimal pay_cost = 0;
            for (int i = 0; i < patientDS.Tables[0].Rows.Count; i++)
            {
                invoNo += patientDS.Tables[0].Rows[i][8].ToString() + "; ";
                total += FS.FrameWork.Function.NConvert.ToDecimal(patientDS.Tables[0].Rows[i][9].ToString());
                own_cost += FS.FrameWork.Function.NConvert.ToDecimal(patientDS.Tables[0].Rows[i][10].ToString());
                pub_cost += FS.FrameWork.Function.NConvert.ToDecimal(patientDS.Tables[0].Rows[i][11].ToString());
                pay_cost += FS.FrameWork.Function.NConvert.ToDecimal(patientDS.Tables[0].Rows[i][12].ToString());
            }
            this.lbFeeNum.Text = invoNo.ToString();   //发票号
            this.lbFeeTotal.Text = total.ToString();    //总金额       
            this.lbFeeSelf.Text = own_cost.ToString();  //自费金额
            this.lbFeeTally.Text = pub_cost.ToString();//记账金额
            this.lbSelfMinus.Text = pay_cost.ToString();  //自负金额
            this.lbName.Text = patientDS.Tables[0].Rows[0][0].ToString();   //姓名
            if (patientDS.Tables[0].Rows[0][1].ToString() == "M")
            {
                this.lbSex.Text = "男";
            }
            if (patientDS.Tables[0].Rows[0][1].ToString() == "F")
            {
                this.lbSex.Text = "女";
            }
            this.lbCarNo.Text = patientDS.Tables[0].Rows[0][4].ToString();   //卡号
            this.lbInspectionDept.Text = patientDS.Tables[0].Rows[0][5].ToString(); //看诊科室
            this.lbFeeCategories.Text = patientDS.Tables[0].Rows[0][6].ToString();//费用类别
            //lbFeeCategories.Text = patientDS.Tables[0].Rows[0][7].ToString();         
            this.lbRegisteredLevel.Text = patientDS.Tables[0].Rows[0][13].ToString();//挂号级别       

            return 1;
        }
              
        protected override int OnPrint(object sender, object neuObject)
        {
            if (MessageBox.Show("是否打印?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return 1;
            }

            if (this.neuSpread1_Sheet1.RowCount == 0)
            {
                return -1;
            }
            int maxPageNO = this.neuSpread1_Sheet1.RowCount / this.maxRowNumPerPage;
            if (maxPageNO * this.maxRowNumPerPage < this.neuSpread1_Sheet1.RowCount)
            {
                maxPageNO = maxPageNO + 1;
            }

            int fromPageNO = 1;
            int toPageNO = maxPageNO;


            for (int pageNO = 0; pageNO < maxPageNO; pageNO++)
            {
                this.lbPage.Text = "页：" + (pageNO + 1).ToString() + "/" + maxPageNO.ToString();
                int rowIndex = 0;
                for (; rowIndex < this.neuSpread1_Sheet1.RowCount; rowIndex++)
                {
                    if (rowIndex < maxRowNumPerPage * (pageNO + 1) && rowIndex >= maxRowNumPerPage * pageNO)
                    {
                        this.neuSpread1_Sheet1.Rows[rowIndex].Visible = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[rowIndex].Visible = false;
                    }
                }
                if (pageNO + 1 < fromPageNO || pageNO + 1 > toPageNO)
                {
                    continue;
                }
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                FS.HISFC.Models.Base.PageSize ps = null;
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                ps = pgMgr.GetPageSize("ClinicFeedetail");
                if (ps != null && ps.Printer.ToLower() == "default")
                {
                    ps.Printer = "";
                }
                if (ps == null)
                {
                    //默认为A4纸张
                    ps = new FS.HISFC.Models.Base.PageSize("ClinicFeedetail",900,550);
                    ps.Printer = "";
                }
                print.SetPageSize(ps);
                print.PrintPage(ps.Left, ps.Top, this.neuPanel1);
            }            
            this.ClearInfo();
            return 1;
            //return base.OnPrint(sender, neuObject);
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (this.txtInvoiceNo.Text.Trim().Length==0)
                {
                    MessageBox.Show("请输入发票号");
                    return;
                }                
                QueryOutpatientInfo(this.txtInvoiceNo.Text.Trim());

            }
        }

        /// <summary>
        /// 清空信息
        /// </summary>
        private void ClearInfo()
        {
            this.lbCarNo.Text = string.Empty;
            this.lbDoctorName.Text = string.Empty;
            this.lbFeeCategories.Text = string.Empty;            
            this.lbFeeNum.Text = string.Empty;
            this.lbFeeSelf.Text = string.Empty;
            this.lbFeeStatus.Text = string.Empty;
            this.lbFeeTally.Text = string.Empty;
            this.lbFeeTime.Text = string.Empty;
            this.lbFeeTotal.Text = string.Empty;
            this.lbInspectionDept.Text = string.Empty;
            this.lbName.Text = string.Empty;
            this.lbOprater.Text = string.Empty;
            //this.lbPage.Text = string.Empty;
            this.lbPrintTime.Text = string.Empty;
            this.lbRegisteredLevel.Text = string.Empty;
            this.lbSelfMinus.Text = string.Empty;
            this.lbSex.Text = string.Empty;
            this.neuSpread1_Sheet1.RowCount = 0;
        }

    }
}
