using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.Local.OutpatientFee.GYZL.Report
{
    public partial class ucOutpatientFeeInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOutpatientFeeInfo()
        {
            InitializeComponent();
            neuLabel2.Text = DBManager.Hospital.Name;
        }

        FS.FrameWork.Management.DataBaseManger DBManager = new FS.FrameWork.Management.DataBaseManger();

        public override int Query(object sender, object neuObject)
        {
            this.Clear();
            if (this.neuClinicCode.Text == string.Empty || this.neuClinicCode.Text == null || this.neuClinicCode.Text == "")
            {
                MessageBox.Show("必须输入一个发票号!");
                this.neuClinicCode.Focus();
                return -1;
            }
            this.QueryOutpatientInfo(this.neuClinicCode.Text.Trim());
            return base.Query(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            this.Print();
            return base.Print(sender, neuObject);
        }

        /// <summary>
        /// 获取患者基本挂号信息 填充到neuSpread1_Sheet1中
        /// </summary>
        /// <param name="clinicCode">患者门诊号</param>
        /// <returns>成功返回1  失败返回-1</returns>
        private int QueryOutpatientInfo(string clinicCode)
        {
            string sqlPqtientInfo = @"
select r.name,        --0患者姓名
       r.sex_code,    --1性别
       r.birthday,    --2生日
       f.oper_date,    --3挂号日期
       r.card_no, --4门诊号
       r.dept_name,   --5科室
       --r.paykind_code,--6结算类别 --del xf 考虑挂号表里的合同单位到收费时可以选择合同单位那么结算类别也就改变了，所以取发票表里的结算类别
       f.paykind_code,
       r.pact_name,    --7合同单位 --结算方式到底是哪一个呢？ 住院登记时界面上显示的结算方式是pact_name 这里也用这个吧
       f.own_cost + f.pay_cost,      -- 不考虑2张发票
       f.pub_cost
  from fin_opr_register r, fin_opb_invoiceinfo f
 where r.clinic_code = f.clinic_code
 and f.invoice_no = '{0}'";
            sqlPqtientInfo = string.Format(sqlPqtientInfo, clinicCode);

            DataSet patientDS = new DataSet();

            FS.FrameWork.Management.DataBaseManger DSManager = new FS.FrameWork.Management.DataBaseManger();
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
                MessageBox.Show("没有该挂号患者！请检查输入的门诊号是否正确");
                this.neuClinicCode.Focus();
                return -1;
            }

            this.neulblName.Text += patientDS.Tables[0].Rows[0][0].ToString();
            if (patientDS.Tables[0].Rows[0][1].ToString() == "M")
            {
                this.neulblSex.Text += "男";
            }
            if (patientDS.Tables[0].Rows[0][1].ToString() == "F")
            {
                this.neulblSex.Text += "女";
            }
            this.neulblAge.Text += DBManager.GetAge(FS.FrameWork.Function.NConvert.ToDateTime(patientDS.Tables[0].Rows[0][2].ToString()));
            this.neulblRegDate.Text += patientDS.Tables[0].Rows[0][3].ToString();
            this.neulblClinicCode.Text += patientDS.Tables[0].Rows[0][4].ToString();
            this.neulblDept.Text += patientDS.Tables[0].Rows[0][5].ToString();
            this.neuSpread1_Sheet1.Cells[1, 0].Text += patientDS.Tables[0].Rows[0][7];

            string sqlFeeInfo = @"
select decode(d.gb_code, null, d.input_code, d.gb_code), --0项目编码
       decode(d.item_noarea, null, d.item_name, d.item_noarea), --1项目名称
       f.specs, --2规格
       f.unit_price, --3单价
       f.qty, --4数量
       f.price_unit, --5计价单位
       f.pub_cost + f.pay_cost + f.own_cost, --6金额
       --f.own_cost, --7自费额
       f.own_cost + f.pay_cost,--add xf 
       --100 * (f.own_cost / (f.pub_cost + f.pay_cost + f.own_cost)) || '%', --8自费比例
       f.eco_cost --9优惠金额
  from fin_opb_feedetail f, fin_com_undruginfo d
 where f.item_code = d.item_code
   and f.drug_flag = '0'
   and f.cancel_flag in ('1', '2')
   and f.invoice_no = '{0}'

union all

select p.custom_code, --0项目编码
       p.trade_name, --1项目名称
       f.specs, --2规格
       f.unit_price, --3单价
       f.qty, --4数量
       f.price_unit, --5计价单位       
       f.pub_cost + f.pay_cost + f.own_cost, --6金额
       --f.own_cost, --7自费额
       f.own_cost + f.pay_cost,--add xf 
       --100 * (f.own_cost / (f.pub_cost + f.pay_cost + f.own_cost)) || '%', --8自费比例
       f.eco_cost --9优惠金额
  from fin_opb_feedetail f, pha_com_baseinfo p
 where f.item_code = p.drug_code
   and f.drug_flag = '1'
   and f.cancel_flag in ('1', '2')
   and f.invoice_no = '{0}'";
            sqlFeeInfo = string.Format(sqlFeeInfo, clinicCode);
            DataSet FeeInfoDS = new DataSet();
            decimal totCost = 0;
            decimal ownCost = 0;
            decimal ecoCost = 0;

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

            this.neuSpread1_Sheet1.AddRows(1, FeeInfoDS.Tables[0].Rows.Count);
            for (int row = 0; row < FeeInfoDS.Tables[0].Rows.Count; row++)
            {
                this.neuSpread1_Sheet1.Cells[row + 1, 0].Text = FeeInfoDS.Tables[0].Rows[row][0].ToString();
                this.neuSpread1_Sheet1.Cells[row + 1, 1].Text = FeeInfoDS.Tables[0].Rows[row][1].ToString();
                this.neuSpread1_Sheet1.Cells[row + 1, 2].Text = FeeInfoDS.Tables[0].Rows[row][2].ToString();
                this.neuSpread1_Sheet1.Cells[row + 1, 3].Text = FeeInfoDS.Tables[0].Rows[row][3].ToString();
                this.neuSpread1_Sheet1.Cells[row + 1, 4].Text = FeeInfoDS.Tables[0].Rows[row][4].ToString();
                this.neuSpread1_Sheet1.Cells[row + 1, 5].Text = FeeInfoDS.Tables[0].Rows[row][5].ToString();
                this.neuSpread1_Sheet1.Cells[row + 1, 6].Text = FeeInfoDS.Tables[0].Rows[row][6].ToString();
                this.neuSpread1_Sheet1.Cells[row + 1, 7].Text = FeeInfoDS.Tables[0].Rows[row][7].ToString();

                totCost += FS.FrameWork.Function.NConvert.ToDecimal(FeeInfoDS.Tables[0].Rows[row][6].ToString());
                ownCost += FS.FrameWork.Function.NConvert.ToDecimal(FeeInfoDS.Tables[0].Rows[row][7].ToString());
                ecoCost += FS.FrameWork.Function.NConvert.ToDecimal(FeeInfoDS.Tables[0].Rows[row][8].ToString());
            }

            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 3].Text = FS.FrameWork.Public.String.FormatNumberReturnString(totCost, 3);
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Text = FS.FrameWork.Public.String.FormatNumberReturnString(ownCost, 3);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 6].Text = patientDS.Tables[0].Rows[0][8].ToString();
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 9].Text = FS.FrameWork.Public.String.FormatNumberReturnString(ecoCost, 3);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 8].Text = patientDS.Tables[0].Rows[0][9].ToString();

            return 1;
        }

        /// <summary>
        /// 清空neuSpread1_Sheet1至初始状态
        /// </summary>
        private void Clear()
        {
            if (this.neuSpread1_Sheet1.Rows.Count > 2)
            {
                this.neuSpread1_Sheet1.RemoveRows(1, this.neuSpread1_Sheet1.Rows.Count - 2);
                this.neuSpread1_Sheet1.Cells[1, 0].Text = "说明：结算方式为  ";
                this.neuSpread1_Sheet1.Cells[1, 3].Text = "";
                this.neuSpread1_Sheet1.Cells[1, 6].Text = "";
                this.neuSpread1_Sheet1.Cells[1, 8].Text = "";

                this.neulblName.Text = "姓名：";
                this.neulblSex.Text = "性别：";
                this.neulblAge.Text = "年龄：";
                this.neulblRegDate.Text = "日期：";
                this.neulblClinicCode.Text = "病历号：";
                this.neulblDept.Text = "科室：";
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        private int Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;
            return print.PrintPage(50, 10, this.panelMain);
        }

        private int Export()
        {
            //if (this.neuSpread1.Export() == -1)
            //{
            //    MessageBox.Show("导出失败！");
            //    return -1;
            //}
            //else
            //{
            //    MessageBox.Show("导出成功！");
            //    return 1;
            //}

            return -1;
        }

        private void neuClinicCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.neuClinicCode.Text = FS.FrameWork.Function.NConvert.ToDBC(this.neuClinicCode.Text.Trim());
                if (this.neuClinicCode.Text.Length >= 2 && System.Text.Encoding.Default.GetBytes(this.neuClinicCode.Text.Substring(0, 1)).Length > 1)
                {

                    ArrayList alPatient = this.QueryInvoiceNoByName(this.neuClinicCode.Text);

                    if (alPatient == null || alPatient.Count <= 0)
                    {
                        MessageBox.Show("没有找到名为：" + this.neuClinicCode.Text + "的患者");
                        return;
                    }

                    FS.FrameWork.WinForms.Controls.NeuListView lvAllReg = new FS.FrameWork.WinForms.Controls.NeuListView();

                    System.Windows.Forms.ColumnHeader colInvoiceNo1 = new ColumnHeader();
                    System.Windows.Forms.ColumnHeader colTotCost1 = new ColumnHeader();
                    System.Windows.Forms.ColumnHeader colCardID1 = new ColumnHeader();
                    System.Windows.Forms.ColumnHeader colName1 = new ColumnHeader();
                    System.Windows.Forms.ColumnHeader colSex1 = new ColumnHeader();
                    System.Windows.Forms.ColumnHeader colBirthday1 = new ColumnHeader();
                    System.Windows.Forms.ColumnHeader colPact1 = new ColumnHeader();
                    System.Windows.Forms.ColumnHeader colIDDENO1 = new ColumnHeader();

                    colInvoiceNo1.Text = "发票号";
                    colInvoiceNo1.Width = 100;
                    colTotCost1.Text = "金额";
                    colTotCost1.Width = 50;
                    colCardID1.Text = "病历号";
                    colCardID1.Width = 90;
                    colName1.Text = "姓名";
                    colName1.Width = 60;
                    colSex1.Text = "性别";
                    colSex1.Width = 30;
                    colBirthday1.Text = "出生日期";
                    colBirthday1.Width = 90;
                    colPact1.Text = "结算类别";
                    colPact1.Width = 125;
                    colIDDENO1.Text = "身份证";
                    colIDDENO1.Width = 150;

                    lvAllReg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                colInvoiceNo1,
                                                colTotCost1,
                                                colCardID1,
                                                colName1,
                                                colSex1,
                                                colBirthday1,
                                                colPact1,
                                                colIDDENO1});

                    lvAllReg.Dock = System.Windows.Forms.DockStyle.Fill;
                    lvAllReg.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lvAllReg.FullRowSelect = true;
                    lvAllReg.GridLines = true;
                    lvAllReg.Location = new System.Drawing.Point(0, 0);
                    lvAllReg.Name = "lvAllReg";
                    lvAllReg.Size = new System.Drawing.Size(500, 250);
                    lvAllReg.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
                    lvAllReg.TabIndex = 1;
                    lvAllReg.UseCompatibleStateImageBehavior = false;
                    lvAllReg.View = System.Windows.Forms.View.Details;

                    foreach (FS.HISFC.Models.Fee.Outpatient.Balance bal in alPatient)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = bal.Invoice.ID;
                        item.Tag = bal;
                        item.SubItems.Add(bal.FT.TotCost.ToString());
                        item.SubItems.Add(bal.Patient.Card.ID);
                        item.SubItems.Add(bal.Patient.Name);
                        item.SubItems.Add(bal.Patient.Sex.Name);
                        item.SubItems.Add(bal.Patient.Birthday.ToString());
                        item.SubItems.Add(bal.Patient.Pact.Name);
                        item.SubItems.Add(bal.Patient.IDCard);

                        lvAllReg.Items.Add(item);
                    }

                    lvAllReg.DoubleClick += new EventHandler(lvAllReg_DoubleClick);

                    if (alPatient.Count > 1)
                    {
                        FS.FrameWork.WinForms.Classes.Function.PopShowControl(lvAllReg, FormBorderStyle.None);
                    }
                    else
                    {
                        ListViewItem listItem = lvAllReg.Items[0];

                        if (listItem != null)
                        {
                            this.neuClinicCode.Text = listItem.SubItems[0].Text;
                        }
                    }
                }
                this.Query(sender, e);
            }
        }

        /// <summary>
        /// 双击检索的患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lvAllReg_DoubleClick(object sender, EventArgs e)
        {
            if ((sender as FS.FrameWork.WinForms.Controls.NeuListView).SelectedItems.Count > 0)
            {
                ListViewItem listItem = (sender as FS.FrameWork.WinForms.Controls.NeuListView).SelectedItems[0];

                if (listItem != null)
                {
                    this.neuClinicCode.Text = listItem.SubItems[0].Text;
                }
            }

            ((sender as ListView).Parent as Form).Close();
        }

        /// <summary>
        /// 根据姓名查询发票号，如果同名同姓，显示多条
        /// </summary>
        /// <param name="name"></param>
        private ArrayList QueryInvoiceNoByName(string name)
        {
            string strSql = @"
select i.invoice_no,
i.tot_cost,
i.card_no,
i.name,
p.sex_code,
p.birthday,
i.pact_name,
p.idenno
from fin_opb_invoiceinfo i,com_patientinfo p
where i.card_no = p.card_no
and i.name like '%{0}%'";
            strSql = String.Format(strSql, name);
            DataSet dsPatient = new DataSet();
            FS.FrameWork.Management.DataBaseManger DSManager = new FS.FrameWork.Management.DataBaseManger();
            if (DSManager.ExecQuery(strSql, ref dsPatient) == -1)
            {
                MessageBox.Show("获取患者信息失败！" + DSManager.Err);
                return null;
            }
            try
            {
                ArrayList alPatient = new ArrayList();
                for (int i = 0; i < dsPatient.Tables[0].Rows.Count; i++)
                {
                    FS.HISFC.Models.Fee.Outpatient.Balance bal = new FS.HISFC.Models.Fee.Outpatient.Balance();
                    bal.Invoice.ID = dsPatient.Tables[0].Rows[i][0].ToString();
                    bal.FT.TotCost = NConvert.ToDecimal(dsPatient.Tables[0].Rows[i][1].ToString());
                    bal.Patient.Card.ID = dsPatient.Tables[0].Rows[i][2].ToString();
                    bal.Patient.Name = dsPatient.Tables[0].Rows[i][3].ToString();
                    bal.Patient.Sex.ID = dsPatient.Tables[0].Rows[i][4].ToString();
                    bal.Patient.Birthday = NConvert.ToDateTime(dsPatient.Tables[0].Rows[i][5].ToString());
                    bal.Patient.Pact.Name = dsPatient.Tables[0].Rows[i][6].ToString();
                    bal.Patient.IDCard = dsPatient.Tables[0].Rows[i][7].ToString();

                    alPatient.Add(bal);
                }

                return alPatient;
            }
            catch
            {
                return null;
            }
        }
    }
}
