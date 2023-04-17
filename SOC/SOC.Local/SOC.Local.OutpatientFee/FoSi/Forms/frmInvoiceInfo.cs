using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.OutpatientFee.FoSi.Controls
{
    public partial class frmInvoiceInfo : Form
    {
        #region 变量
        /// <summary>
        /// 患者挂号信息
        /// </summary>
        FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 人员管理业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Person perManager = new FS.HISFC.BizLogic.Manager.Person();
        /// <summary>
        /// 科室管理业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// 费用业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        FS.HISFC.BizLogic.Pharmacy.Item phaManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
        #endregion
        //结算类别
        private string payType = null;

        public frmInvoiceInfo()
        {
            InitializeComponent();
        }

        public bool SetInvoiceInfo(FS.HISFC.Models.Fee.Outpatient.Balance invoice)
        {
            if (invoice == null)
            {
                return false;
            }

            if (invoice.CancelType == FS.HISFC.Models.Base.CancelTypes.Canceled)
            {
                MessageBox.Show("该发票已退费！");
                return false;
            }
            if (invoice.CancelType == FS.HISFC.Models.Base.CancelTypes.LogOut)
            {
                MessageBox.Show("该发票已注销！");
                return false;
            }
            if (invoice.CancelType == FS.HISFC.Models.Base.CancelTypes.Reprint)
            {
                MessageBox.Show("该发票已重打！");
                return false;
            }

            ArrayList alPatient = registerManager.QueryPatient(invoice.Patient.ID);
            FS.HISFC.Models.Registration.Register r = alPatient[0] as FS.HISFC.Models.Registration.Register;

            this.lblInvoiceNo.Text = invoice.Invoice.ID;

            this.lblName.Text = r.Name;
            //修改成收费时间
            this.lblDate.Text = invoice.BalanceOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblCardNO.Text = r.PID.CardNO;
            this.lblPrintDate.Text = invoice.PrintedInvoiceNO;
            this.lblTotCost.Text = invoice.FT.TotCost.ToString();
            this.lblOwnCost.Text = (invoice.FT.OwnCost - invoice.FT.RebateCost).ToString();
            this.lblPubCost.Text = invoice.FT.PubCost.ToString();
            this.lblRebateCost.Text = invoice.FT.RebateCost.ToString();
            this.lblDept.Text = r.DoctorInfo.Templet.Dept.ID.ToString();
            FS.FrameWork.Models.NeuObject obj = perManager.GetPersonByID(invoice.BalanceOper.ID);
            if (obj != null)
            {
                this.lblFeeOper.Text = obj.ID;
            }

            ArrayList alFee = outPatientManager.QueryFeeItemListsByInvoiceNO(invoice.Invoice.ID);
            if (alFee == null || alFee.Count <= 0)
            {
                MessageBox.Show("查询费用明细失败！" + outPatientManager.Err);
                return false;
            }
            this.payType = invoice.Patient.Pact.ID;
            if (!SetFeeInfo(alFee))
            {
                return false;
            }
            int row = this.neuSpread1_Sheet1.RowCount;
            this.neuSpread1_Sheet1.Cells[row - 1, 7].Text = "'";


            invoice.FT.User01 = invoice.DrugWindowsNO;
            this.ucBalanceInfo.SetInfomation(r, invoice.FT, alFee, null, "4");

            return true;
        }


        private bool SetFeeInfo(ArrayList alFee)
        {
            if (alFee == null || alFee.Count <= 0)
            {
                return false;
            }

            int count = 0;
            decimal cost = 0;
            decimal totOwnCost = 0;
            decimal totPubCost = 0;
            decimal totRebateCost = 0;
            string userCode = "";
            FS.HISFC.Models.Fee.Outpatient.FeeItemList tempf = null;
            FS.HISFC.Models.Pharmacy.Item item = null;
            FS.HISFC.Models.Fee.Item.Undrug undrug = null;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFee)
            {
                string priceUnit = string.Empty;
                decimal price = 0m, qty = 0m;
                if (f.PayType == PayTypes.Balanced && f.CancelType == CancelTypes.Valid)
                {
                    price = f.Item.Price;
                    priceUnit = f.Item.PriceUnit;
                    qty = f.Item.Qty;
                    if (f.Item.ItemType == EnumItemType.Drug)
                    {
                        item = phaManager.GetItem(f.Item.ID);
                        if (item == null)
                        {
                            MessageBox.Show("查询药品信息失败！" + phaManager.Err);
                            return false;
                        }
                        userCode = item.NameCollection.UserCode;
                        if (f.Item.PriceUnit != item.PackUnit)
                        {
                            price = FS.FrameWork.Public.String.FormatNumber(f.Item.Price / f.Item.PackQty, 2);
                        }
                        else
                        {
                            qty = qty / item.PackQty;
                        }
                    }
                    else
                    {
                        undrug = undrugManager.GetValidItemByUndrugCode(f.Item.ID);
                        if (undrug == null)
                        {
                            MessageBox.Show("查询药品信息失败！" + phaManager.Err);
                            return false;
                        }
                        userCode = undrug.UserCode;
                    }

                    count = this.neuSpread1_Sheet1.Rows.Count;
                    this.neuSpread1_Sheet1.Rows.Add(count, 1);
                    this.neuSpread1_Sheet1.Cells[count, 0].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.neuSpread1_Sheet1.Cells[count, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                    this.neuSpread1_Sheet1.Cells[count, 0].Text = userCode;
                    this.neuSpread1_Sheet1.Cells[count, 1].Text = f.Item.Name;
                    this.neuSpread1_Sheet1.Cells[count, 2].Text = f.Item.Specs;
                    this.neuSpread1_Sheet1.Cells[count, 3].Text = priceUnit;
                    this.neuSpread1_Sheet1.Cells[count, 4].Text = FS.FrameWork.Public.String.FormatNumber(qty, 2).ToString();
                    this.neuSpread1_Sheet1.Cells[count, 5].Text = FS.FrameWork.Public.String.FormatNumber(price, 2).ToString();
                    this.neuSpread1_Sheet1.Cells[count, 6].Text = (f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString();
                    this.neuSpread1_Sheet1.Cells[count, 7].Text = f.Days.ToString();
                    this.neuSpread1_Sheet1.Cells[count, 8].Text = this.GetYiBaoBiaoZhi(this.payType,f.Item.ID);
                    this.neuSpread1_Sheet1.Cells[count, 9].Text = this.SetRemark(f.RecipeNO,f.SequenceNO);
                    cost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                    totOwnCost += f.FT.OwnCost + f.FT.PayCost;
                    totRebateCost += f.FT.RebateCost;
                    totPubCost += f.FT.PubCost;
                    if (tempf == null)
                    {
                        tempf = f;
                    }
                }
            }
            this.lblOwnCost.Text = (totOwnCost - totRebateCost).ToString();
            this.lblPubCost.Text = totPubCost.ToString();
            this.lblRebateCost.Text = totRebateCost.ToString();
            this.lblTotCost.Text = cost.ToString();

            FS.FrameWork.Models.NeuObject obj = perManager.GetPersonByID(tempf.RecipeOper.ID);
            if (obj != null)
            {
                lblDoct.Text = obj.ID;
            }

            obj = deptManager.GetDeptmentById(tempf.RecipeOper.Dept.ID);
            if (obj != null)
            {
                lblDept.Text = obj.Name;
            }

            return true;
        }

        private void frmInvoiceInfo_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
                return;

            this.ucBalanceInfo.Focus();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        ///  获取列表要显示的行数
        /// </summary>
        /// <param name="operid"> 操作员</param>
        /// <returns></returns>
        public string GetYiBaoBiaoZhi(string hiscode,string pactid)
        {
            string  BiaoZhi =null;
            string sql = @" select t.* from fin_com_compare t
                            where t.pact_code ={0}, t.his_code ='{1}'";
            sql = string.Format(sql,pactid,hiscode);
            BiaoZhi = this.outPatientManager.ExecSqlReturnOne(sql);
            if (BiaoZhi =="-1")
            {
                BiaoZhi = null;
            }
            return BiaoZhi;
        }
        /// <summary>
        /// 获得处方备注
        /// </summary>
        private string SetRemark(string recipeNO, int sequenceNO)
        {
            string remark = null;
            string sql = @"select t.remark from met_ord_recipedetail t
                           where t.recipe_no ='{0}'
                           and   t.recipe_seq ='{1}'";
            try
            {
                sql = string.Format(sql, recipeNO, sequenceNO);
            }
            catch (Exception ex)
            {
                ;
                return null;
            }
            if (this.registerManager.ExecSqlReturnOne(sql) != "-1")
            {
                remark = this.registerManager.ExecSqlReturnOne(sql);
            }
            return remark;
        }
    }
}
