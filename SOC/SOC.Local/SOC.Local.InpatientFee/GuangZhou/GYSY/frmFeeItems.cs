using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.GuangZhou.GYSY
{
    public partial class frmFeeItems : Form
    {
        public frmFeeItems()
        {
            InitializeComponent();

            this.InitdsItemsBind();
        }

        FS.HISFC.BizLogic.Fee.InPatient Fee = new FS.HISFC.BizLogic.Fee.InPatient();

        #region 属性
        string inpatientNo = string.Empty;
        public string InpatientNo
        {
            set { inpatientNo = value; }
        }

        string fee_code = string.Empty;
        /// <summary>
        /// 费用类别
        /// </summary>
        public string FeeCode
        {
            set { fee_code = value; }
        }
        
        string dtBegin=string.Empty;
        /// <summary>
        /// 开始时间
        /// </summary>
        public string Begin
        {
           set{ dtBegin=value;}
        }

         string dtEnd=string.Empty;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string End
        {
           set{ dtEnd=value;}
        }


        ArrayList alMinFee = null;
        public ArrayList AlMinFee
        {
            set
            {
                alMinFee = null;
                alMinFee = value; 
            }
        }


        #endregion

        private ArrayList alItemsObject = new ArrayList();
        Hashtable hsItemsObject = new Hashtable();

        DataSet dsItems = new DataSet();//存储项目明细
        DataSet dsItemsBind = new DataSet();//存储项目明细（剔除项目代码）
  

        public delegate void ButtonHandler(ArrayList ht);
        public event ButtonHandler ButtonEvent;

        Hashtable htItemSelected = new Hashtable();
        ArrayList alItemSelected = new ArrayList();
        /// <summary>
        /// 将查询的数据集转化成绑定到FP的数据集
        /// </summary>
        /// <param name="sv">FP</param>
        /// <param name="dsSel">查询出的数据集</param>
        /// <param name="dsBind">绑定到FP的数据集</param>
        private void BindDataToFP(FarPoint.Win.Spread.SheetView sv, DataSet dsSel, DataSet dsBind)
        {
            if (this.alItemsObject.Count > 0) this.alItemsObject.Clear();
            if (dsSel == null || dsSel.Tables.Count <= 0 || dsSel.Tables[0].Rows.Count <= 0) return;
            if (dsBind != null && dsBind.Tables.Count > 0 && dsBind.Tables[0].Rows.Count > 0)
            {
                dsBind.Tables[0].Rows.Clear();
            }
            for (int i = 0; i <= this.dsItems.Tables[0].Rows.Count - 1; ++i)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeInfo fInfo = new  FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                DataRow drBind = dsBind.Tables[0].NewRow();
                DataRow dr = dsSel.Tables[0].Rows[i];

                drBind["项目编号"] = dr["CODE"].ToString();
                drBind["项目名称"] = dr["NAME"].ToString();
                drBind["规则"] = dr["SPECS"].ToString();
                drBind["费用类别"] = dr["FEENAME"].ToString();
                drBind["单价"] = dr["UNIT_PRICE"];
                drBind["数量"] = dr["QTY"];
                drBind["费用金额"] = dr["TOT_COST"];
                drBind["自费金额"] = dr["OWNCOST"];
                drBind["自付金额"] = dr["PAYCOST"];
                drBind["公费金额"] = dr["PUBCOST"];
                drBind["优惠金额"] = dr["ECOCOST"];

                dsBind.Tables[0].Rows.Add(drBind);


                fInfo.ID = dr["CODE"].ToString();
                fInfo.Name = dr["NAME"].ToString();
                if (dr["ITEMTYPE"].ToString() == "0")
                {
                    //非药品
                    fInfo.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                }
                else 
                {
                    //药品
                    fInfo.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                }
                fInfo.Item.MinFee.ID = dr["FEECODE"].ToString();//最小费用代码
                fInfo.Memo = dr["FEENAME"].ToString();
                fInfo.Item.Price =  FS.FrameWork.Function.NConvert.ToDecimal(dr["UNIT_PRICE"].ToString());
                fInfo.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(dr["QTY"].ToString());
                fInfo.Item.Specs = dr["SPECS"].ToString();
                fInfo.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(dr["TOT_COST"].ToString());
                fInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(dr["OWNCOST"].ToString());
                fInfo.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(dr["PUBCOST"].ToString());
                fInfo.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(dr["PAYCOST"].ToString());
                fInfo.FT.RebateCost = FS.FrameWork.Function.NConvert.ToDecimal(dr["ECOCOST"].ToString());

                this.hsItemsObject.Add(fInfo.ID+fInfo.Name + fInfo.Item.Specs+FS.FrameWork.Public.String.FormatNumberReturnString(fInfo.Item.Price,4), fInfo);

            }
          ////  sv.DataSource = dsBind.Tables[0].DefaultView;
          //  if (sv.Rows.Count > 0)
          //  {
          //      for (int j = 0; j <= sv.Rows.Count - 1; ++j)
          //      {
          //          sv.Rows[j].Tag = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.alItemsObject[j];
          //      }
          //  }


        }

        public void QueryFeeItems(string strMinFeeCode)
        {
            if (strMinFeeCode != "")
            {
                strMinFeeCode = strMinFeeCode.Trim(',');
                if (-1 == this.Fee.GetItemsByMinFeeId(this.inpatientNo, this.dtBegin, this.dtEnd, strMinFeeCode, ref dsItems)) 
                    return;

                this.BindDataToFP(fpSelectedItem_Sheet1, this.dsItems, this.dsItemsBind);


            }
            else
            {
                if (fpSelectedItem_Sheet1.Rows.Count > 0)
                {
                    fpSelectedItem_Sheet1.Rows.Remove(0, fpSelectedItem_Sheet1.Rows.Count);

                }
                return;
            }
        }

        /// <summary>
        /// 初始化绑定到项目列表FP的数据集
        /// </summary>
        protected void InitdsItemsBind()
        {
            try
            {
                System.Type stStr = System.Type.GetType("System.String");
                System.Type stInt = System.Type.GetType("System.Int16");
                System.Type stDec = System.Type.GetType("System.Single");
                System.Type stDate = System.Type.GetType("System.DateTime");
                System.Type stBool = System.Type.GetType("System.Boolean");


                DataTable dtItemsDetail = this.dsItemsBind.Tables.Add("MyTable1");
                dtItemsDetail.Columns.AddRange(new DataColumn[]{ 
                                                                  new DataColumn("项目编号",stStr),
																   new DataColumn("项目名称",stStr),
																   new DataColumn("规则",stStr),
																   new DataColumn("费用类别",stStr),
																   new DataColumn("单价",stDec),
																   new DataColumn("数量",stInt),
																   new DataColumn("费用金额",stDec),
																   new DataColumn("自费金额",stDec),
																   new DataColumn("自付金额",stDec),
																   new DataColumn("公费金额",stDec),
																   new DataColumn("优惠金额",stDec)
					
															   });
                this.fpSelectedItem_Sheet1.DataSource = this.dsItemsBind.Tables[0].DefaultView;
                this.fpSelectedItem_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.fpSelectedItem_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;

                this.dsItemsBind.Tables[0].AcceptChanges();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void frmFeeItems_Load(object sender, EventArgs e)
        {
           //加载最小费用分类
            if (alMinFee.Count >1)
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = "ALL";
                obj.Name = "全部";
                alMinFee.Add(obj);
            }
            this.cmbMinFee.AddItems(this.alMinFee);
            this.cmbMinFee.Tag = this.fee_code;
            this.cmbMinFee.Enabled = false;

            if (!string.IsNullOrEmpty(this.fee_code))
            {
                QueryFeeItems(this.fee_code);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.ButtonEvent != null)
            {
                ButtonEvent(null);
            }
            this.Close();
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            GetSelectedFeeItem();
            if (this.ButtonEvent !=null)
            {
                ButtonEvent(this.alItemSelected);
            }
            this.Close();
        }

        private void fpSelectedItem_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeInfo fInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.fpSelectedItem_Sheet1.Rows[e.Row].Tag;
                if ((bool)this.fpSelectedItem_Sheet1.Cells[e.Row, 0].Value)
                {
                    this.fpSelectedItem_Sheet1.Cells[e.Row, 0].Value = false;

                }
                else
                {
                    this.fpSelectedItem_Sheet1.Cells[e.Row, 0].Value = true;

                }
            }
        }

        /// <summary>
        /// 获取选择的费用项目
        /// </summary>
        private void GetSelectedFeeItem()
        {
            if (this.htItemSelected.Count > 0)
            {
                
                foreach(FS.HISFC.Models.Fee.Inpatient.FeeInfo fObj in this.htItemSelected.Values)
                {
                    this.alItemSelected.Add(fObj);

                }
            }
        }

        private void cmbMinFee_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.fee_code = this.cmbMinFee.Tag.ToString();
        }


        #region 过滤
        private string filter = "1=1";

        /// <summary>
        /// 连接过滤字符串
        /// </summary>
        /// <param name="filterStr">原始过滤字符串</param>
        /// <param name="newFilterStr">新增加的过滤条件</param>
        /// <param name="logicExpression">逻辑运算符</param>
        /// <returns>成功返回连接后的过滤字符串</returns>
        public  string ConnectFilterStr(string filterStr, string newFilterStr, string logicExpression)
        {
            string connectStr = "";
            if (filterStr == "")
            {
                connectStr = newFilterStr;
            }
            else
            {
                connectStr = filterStr + " " + logicExpression + " " + newFilterStr;
            }
            return connectStr;
        }

        /// <summary>
        /// 根据药品名称的默认过滤字段 返回过滤字符串
        /// </summary>
        /// <param name="dv">需过滤的DataView</param>
        /// <param name="queryCode">过滤数据字符串</param>
        /// <returns>成功返回过滤字符串 失败返回null</returns>
        public string GetFilterStr(DataView dv, string queryCode)
        {
            string filterStr = "";
            if (dv.Table.Columns.Contains("自定义码"))
                filterStr = ConnectFilterStr(filterStr, string.Format("自定义码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("拼音码"))
                filterStr = ConnectFilterStr(filterStr, string.Format("拼音码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("五笔码"))
                filterStr = ConnectFilterStr(filterStr, string.Format("五笔码 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("项目名称"))
                filterStr = ConnectFilterStr(filterStr, string.Format("项目名称 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("项目编号"))
                filterStr = ConnectFilterStr(filterStr, string.Format("项目编号 like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("国家编码"))
                filterStr = ConnectFilterStr(filterStr, string.Format("国家编码 like '{0}'", queryCode), "or");
            return filterStr;
        }

        private void filterItem()
        {
            this.filter = GetFilterStr(this.dsItemsBind.Tables[0].DefaultView, "%" + this.txtInput.Text + "%");

            //filter = "(" + filter + ")";

            this.dsItemsBind.Tables[0].DefaultView.RowFilter = filter;

        }
        #endregion

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            this.filterItem();
        }

        private void fpSelectedItem_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int curRow = this.fpSelectedItem.ActiveSheet.ActiveRow.Index;
            FS.HISFC.Models.Fee.Inpatient.FeeInfo fInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)
                this.hsItemsObject[this.fpSelectedItem_Sheet1.Cells[curRow, (int)ColumnFeeItem.ItemCode].Text.ToString() 
                + this.fpSelectedItem_Sheet1.Cells[curRow, (int)ColumnFeeItem.ItemName].Text.ToString()
                + this.fpSelectedItem_Sheet1.Cells[curRow, (int)ColumnFeeItem.Specise].Text.ToString()
                + FS.FrameWork.Public.String.FormatNumberReturnString(FS.FrameWork.Function.NConvert.ToDecimal(this.fpSelectedItem_Sheet1.Cells[curRow, (int)ColumnFeeItem.Price].Value),4)];
            

            this.sheetView1.RowCount++;

            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.ItemCode].Value = fInfo.ID;
            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.ItemName].Value = fInfo.Name;
            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.Specise].Value = fInfo.Item.Specs;
            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.FeeCode].Value = fInfo.Memo;
            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.Price].Value = fInfo.Item.Price.ToString();
            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.Qty].Value = fInfo.Item.Qty.ToString();
            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.TotCost].Value = fInfo.FT.TotCost;
            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.OwnCost].Value = fInfo.FT.OwnCost;
            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.PayCost].Value = fInfo.FT.PayCost;
            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.PubCost].Value = fInfo.FT.PubCost; ;
            this.sheetView1.Cells[this.sheetView1.RowCount - 1, (int)ColumnFeeItem.DecCost].Value = fInfo.FT.RebateCost;

            this.dsItemsBind.Tables[0].DefaultView.Delete(curRow);
            if (!this.htItemSelected.Contains(fInfo.ID+fInfo.Name+fInfo.Item.Specs+FS.FrameWork.Public.String.FormatNumberReturnString(fInfo.Item.Price,4)))
            {
                this.htItemSelected.Add(fInfo.ID + fInfo.Name + fInfo.Item.Specs + FS.FrameWork.Public.String.FormatNumberReturnString(fInfo.Item.Price,4), fInfo);
            }
             
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int curRow = this.sheetView1.ActiveRow.Index;
            FS.HISFC.Models.Fee.Inpatient.FeeInfo fInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)
                this.hsItemsObject[this.sheetView1.Cells[curRow, (int)ColumnFeeItem.ItemCode].Text.ToString() 
                + this.sheetView1.Cells[curRow, (int)ColumnFeeItem.ItemName].Text.ToString()
                + this.sheetView1.Cells[curRow, (int)ColumnFeeItem.Specise].Text.ToString()
                + FS.FrameWork.Public.String.FormatNumberReturnString(FS.FrameWork.Function.NConvert.ToDecimal(this.sheetView1.Cells[curRow, (int)ColumnFeeItem.Price].Value), 4)];

            DataRowView drv = this.dsItemsBind.Tables[0].DefaultView.AddNew();
            drv.Row["项目编号"] = fInfo.ID;
            drv.Row["项目名称"] = fInfo.Name;
            drv.Row["规则"] = fInfo.Item.Specs;
            drv.Row["费用类别"] = fInfo.Item.MinFee.Name;
            drv.Row["单价"] = fInfo.Item.Price;
            drv.Row["数量"] = fInfo.Item.Qty;
            drv.Row["费用金额"] = fInfo.FT.TotCost;
            drv.Row["自费金额"] = fInfo.FT.OwnCost;
            drv.Row["自付金额"] = fInfo.FT.PayCost;
            drv.Row["公费金额"] = fInfo.FT.PubCost;
            drv.Row["优惠金额"] = fInfo.FT.RebateCost;

            this.htItemSelected.Remove(fInfo.ID + fInfo.Name + fInfo.Item.Specs + FS.FrameWork.Public.String.FormatNumberReturnString(fInfo.Item.Price,4));   
        }

    }
}
