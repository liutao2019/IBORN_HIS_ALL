using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientItemInputAndDisplay
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
        FS.HISFC.Models.Registration.Register reg = null;
        public FS.HISFC.Models.Registration.Register Register
        {
            
            set { reg = value; }
        }

        FS.HISFC.Models.Fee.Outpatient.FeeItemList fItem = null;
        public FS.HISFC.Models.Fee.Outpatient.FeeItemList FItem
        {
            set 
            { 
                fItem = value; 
            }
        }


        #endregion

        private ArrayList alItemsObject = new ArrayList();
        Hashtable hsItemsObject = new Hashtable();

        DataSet dsItems = new DataSet();//存储项目明细
        DataSet dsItemsBind = new DataSet();//存储项目明细（剔除项目代码）

        ArrayList myUndrugCombs = new ArrayList();

        public ArrayList UndrugCombs
        {
            set { myUndrugCombs = value; }
        }

        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
        Function fun = new Function();

        public delegate void ButtonHandler(ArrayList ht);
        public event ButtonHandler ButtonEvent;

        public void QueryFeeItems(FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {

            if (myUndrugCombs.Count>0)
            {
               
                FS.HISFC.BizLogic.Fee.Item itemMgr=new FS.HISFC.BizLogic.Fee.Item();
              
                ArrayList alFeeItems=new ArrayList();
                foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrug in myUndrugCombs)
                {
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList obj = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
                    FS.HISFC.Models.Fee.Item.Undrug item = itemMgr.GetItemByUndrugCode(undrug.ID);                    
                    obj.Item = item;
                    obj.ID = item.UserCode;
                    obj.Name = item.Name;
                    obj.Item.Qty = f.Item.Qty * undrug.Qty;
                    obj.FT.TotCost = item.Price * obj.Item.Qty;
                    obj.FT.OwnCost = obj.FT.TotCost;
                    obj.FT.PubCost = 0;
                    obj.FT.PayCost = 0;
                    alFeeItems.Add(obj);
                }

                BindDataToFP(this.fpSelectedItem_Sheet1,alFeeItems, this.dsItemsBind);

            }
        }

        /// <summary>
        /// 将查询的数据集转化成绑定到FP的数据集
        /// </summary>
        /// <param name="sv">FP</param>
        /// <param name="dsSel">查询出的数据集</param>
        /// <param name="dsBind">绑定到FP的数据集</param>
        private void BindDataToFP(FarPoint.Win.Spread.SheetView sv, ArrayList alFeeItems, DataSet dsBind)
        {
            if (alFeeItems == null || alFeeItems.Count <= 0 ) return;
            if (dsBind != null && dsBind.Tables.Count > 0 && dsBind.Tables[0].Rows.Count > 0)
            {
                dsBind.Tables[0].Rows.Clear();
            }
            if (reg.Pact.PayKind.ID == "03")
            {
                this.ComputeFeeItems(this.reg, ref alFeeItems);

            }            

            for (int i = 0; i <= alFeeItems.Count - 1; ++i)
            {
                DataRow drBind = dsBind.Tables[0].NewRow();
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f = alFeeItems[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (reg.Pact.PayKind.ID == "02")
                {
                    string errorInfo="";
                    if (-1 == fun.RecomputeFeeItemListOutPatient(reg, ref f, ref errorInfo))
                    {
                        MessageBox.Show(f.Item.Name + errorInfo);
                    } 
                }

                drBind["项目编号"] =f.ID;
                drBind["项目名称"] =f.Name;
                drBind["规则"] = f.Item.Specs;
                drBind["等级"] = "自费";
                if (reg.Pact.PayKind.ID=="03")
                {
                     if (f.ItemRateFlag == "3")
                    {
                        drBind["等级"] = "特批";
                    }
                     else if (f.NewItemRate == 0)
                     {
                         drBind["等级"] = "记账";
                     }

                }
                else if (reg.Pact.PayKind.ID == "02")
                {
                    if (f.Compare.CenterItem.ItemGrade=="1")
                    {
                         drBind["等级"] = "甲类";
                    }
                    else if (f.Compare.CenterItem.ItemGrade == "2")
                    {
                        drBind["等级"] = "乙类";
                    }
                }

                drBind["费用类别"] = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetConstantName(FS.HISFC.Models.Base.EnumConstant.MINFEE, f.Item.MinFee.ID); 
                drBind["单价"] = f.Item.Price;
                drBind["数量"] = f.Item.Qty;
                drBind["费用金额"] = f.FT.TotCost;
                drBind["自费金额"] = f.FT.OwnCost;
                drBind["自付金额"] = f.FT.PayCost;
                drBind["公费金额"] = f.FT.PubCost;
                drBind["优惠金额"] = f.FT.RebateCost;

                dsBind.Tables[0].Rows.Add(drBind);
            }
        

        }

        private void frmFeeItems_Load(object sender, EventArgs e)
        {
            if (fItem!=null && !string.IsNullOrEmpty(fItem.Item.ID ))
            {
                QueryFeeItems(fItem);
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
                                                                   new DataColumn("等级",stStr),
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

        private int ComputeFeeItems(FS.HISFC.Models.Registration.Register register,ref ArrayList comFeeItemLists)
        {

            //设置待遇的合同单位参数
            this.medcareInterfaceProxy.SetPactCode(register.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;
            //连接待遇接口
            long returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                //医保回滚可能出错，此处提示
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }

            }

            //待遇接口结算计算,应用公费和医保
            //returnValue = this.medcareInterfaceProxy.BalanceOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
            returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(register, ref comFeeItemLists);
            if (returnValue == -1)
            {
                #region {88364E78-EC32-450a-95E4-A589AD361F34}
                FS.FrameWork.Management.PublicTrans.RollBack();
                //医保回滚可能出错，此处提示
                if (this.medcareInterfaceProxy.Rollback() == -1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                this.medcareInterfaceProxy.Disconnect();
                #endregion
                return -1;
            }
            return 1;

        }
 

    }
    /// <summary>
    /// 费用项目枚举
    /// </summary>
    public enum ColumnFeeItem
    {
        ItemCode,
        ItemName,
        Specise,
        ItemGrade,//医保等级
        FeeCode,
        Price,
        Qty,
        TotCost,
        OwnCost,
        PayCost,
        PubCost,
        DecCost
    }
}
