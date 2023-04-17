using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Base
{
	/// <summary>
    /// [控件描述: 药品全院特限维护 {1A398A34-0718-47ed-AAE9-36336430265E}]
    /// [创 建 人: Sunjh]
    /// [创建时间: 2010-10-1]
	/// </summary>
    public partial class ucSpeDrugMaintenance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
		/// <summary>
		/// 
		/// </summary>
        public ucSpeDrugMaintenance()
        {
            InitializeComponent();
        }
        #region 变量

        FS.HISFC.BizLogic.Pharmacy.Item itemPha = new FS.HISFC.BizLogic.Pharmacy.Item();
        FS.HISFC.BizLogic.Pharmacy.Constant constant = new FS.HISFC.BizLogic.Pharmacy.Constant();

        //{902C80AC-58AE-4ff1-9B5A-0E72C80B025B}  edit by shizj 2010-05-03 
        //ArrayList alSpeDrug = new ArrayList();
        List<FS.HISFC.Models.Pharmacy.Item> alSpeDrug = new List<FS.HISFC.Models.Pharmacy.Item>( );
        List<FS.HISFC.Models.Pharmacy.Item> alSpeDrugNew = new List<FS.HISFC.Models.Pharmacy.Item>();
        Hashtable speHa = new Hashtable();
        ArrayList alSpeDrugFilter = new ArrayList();

        private FS.HISFC.BizLogic.Pharmacy.Constant phaContManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
        private FS.HISFC.BizLogic.Pharmacy.Dictionary phaDicManager = new FS.HISFC.BizLogic.Pharmacy.Dictionary();
        private FS.HISFC.Models.Pharmacy.Company phaComany = new FS.HISFC.Models.Pharmacy.Company();
        private FS.HISFC.BizLogic.Pharmacy.Item phaItemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        List<FS.HISFC.Models.Pharmacy.Item> phaItemList = new List<FS.HISFC.Models.Pharmacy.Item>();
        private ArrayList phaList = new ArrayList();
        #endregion

        #region 初始化工具栏
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="neuObject"></param>
		/// <param name="param"></param>
		/// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("删除", "删除一条数据", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            return toolBarService;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                this.DelSpeDrug();
            }
            //else if (e.ClickedItem.Text == "增加")
            //{
            //    this.Add();
            //}
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion
        
        #region 方法

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
			if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
			{
				this.Init();
				this.InitSpeDrug();
				this.InitNoSpeDrug(alSpeDrugNew);
			}
            base.OnLoad(e);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="neuObject"></param>
		/// <returns></returns>
		protected override int OnQuery(object sender, object neuObject)
		{
			this.Init();
			this.InitSpeDrug();
			this.InitNoSpeDrug(alSpeDrugNew);
			return 1;
		}

        /// <summary>
        /// 初始化
        /// </summary>
        private int Init()
        {
            //{902C80AC-58AE-4ff1-9B5A-0E72C80B025B}  edit by shizj 2010-05-03 
			alSpeDrug.Clear();
			this.cmbDrug.Items.Clear();
            alSpeDrug = itemPha.QueryItemAvailableList( );
			FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();
			helper.ArrayObject = phaContManager.QueryCompany("0");

            if (alSpeDrug == null)
            {
                MessageBox.Show(itemPha.Err);
                return -1;
            }
            else//{62AAA983-5F51-4786-BAF9-FB032B84A23D}
            {
                foreach(FS.HISFC.Models.Pharmacy.Item itemObj in alSpeDrug)
                {
                    //phaComany = phaContManager.QueryCompanyByCompanyID(itemObj.Product.Producer.ID);
                    if (phaComany != null)
                    {
						itemObj.Product.Producer.Name = helper.GetName(itemObj.Product.Producer.ID);
                    }
                    alSpeDrugNew.Add(itemObj);
                }
            }//{62AAA983-5F51-4786-BAF9-FB032B84A23D}

            #region 药品列表加载,{62AAA983-5F51-4786-BAF9-FB032B84A23D}
            phaItemList = new List<FS.HISFC.Models.Pharmacy.Item>();
            phaItemList = phaItemManager.QueryItemList();
            if (phaItemList != null)
            {
                foreach (FS.HISFC.Models.Pharmacy.Item itemObj in phaItemList)
                {
                    phaList.Add(itemObj);
                }
                this.cmbDrug.AddItems(phaList);
            }
            #endregion
            return 0;
        }

        /// <summary>
        /// 增加没有设置的特限药品
        /// </summary>
        /// <returns></returns>
        /// //{902C80AC-58AE-4ff1-9B5A-0E72C80B025B}  edit by shizj 2010-05-03 
        private void InitNoSpeDrug ( List<FS.HISFC.Models.Pharmacy.Item> alSpeDrug )
        {
            DataTable dtSpecDrug = new DataTable();
            System.Type strType = typeof(string);
            dtSpecDrug.Columns.AddRange(new DataColumn[] {
            new DataColumn("药品编码",strType),
                new DataColumn("通用名",strType),//{62AAA983-5F51-4786-BAF9-FB032B84A23D}               
            new DataColumn("商品名称",strType),
                new DataColumn("规格",strType),//{62AAA983-5F51-4786-BAF9-FB032B84A23D}
                new DataColumn("生产厂家",strType),//{62AAA983-5F51-4786-BAF9-FB032B84A23D}
            new DataColumn("拼音码",strType),
            new DataColumn("五笔码",strType),
                new DataColumn("通用名拼音码",strType),//{62AAA983-5F51-4786-BAF9-FB032B84A23D}
                new DataColumn("通用名五笔码",strType),//{62AAA983-5F51-4786-BAF9-FB032B84A23D}
            new DataColumn("自定义码",strType)});

            foreach (FS.HISFC.Models.Pharmacy.Item item in alSpeDrug)
            {
                if (this.neuSpread1_Sheet1.Rows.Count != 0)
                {
                    if (speHa.Contains(item.ID))
                    {
                        continue;
                    }
                    else
                    {
                        //dtSpecDrug.Rows.Add(new object[] { item.ID, item.Name, item.SpellCode, item.WBCode, item.UserCode });//{62AAA983-5F51-4786-BAF9-FB032B84A23D}
                        dtSpecDrug.Rows.Add(new object[] { item.ID, item.NameCollection.RegularName, item.Name, item.Specs, item.Product.Producer.Name, item.SpellCode, item.WBCode, item.NameCollection.RegularSpell.SpellCode, item.NameCollection.RegularSpell.WBCode, item.UserCode });
                        alSpeDrugFilter.Add(item);
                    }
                }
                else
                {
                    //dtSpecDrug.Rows.Add(new object[] { item.ID, item.Name, item.SpellCode, item.WBCode, item.UserCode });//{62AAA983-5F51-4786-BAF9-FB032B84A23D}
                    dtSpecDrug.Rows.Add(new object[] { item.ID, item.NameCollection.RegularName, item.Name, item.Specs, item.Product.Producer.Name, item.SpellCode, item.WBCode, item.NameCollection.RegularSpell.SpellCode, item.NameCollection.RegularSpell.WBCode, item.UserCode });
                }
            }
            ucDrugList1.AddDataToFP(dtSpecDrug);

            this.neuSpread1_Sheet1.Columns[0].Visible = false;//第一列药品编码隐藏
        }

        /// <summary>
        /// 初始化特限药品
        /// </summary>
        /// <returns></returns>
        private int InitSpeDrug()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
			speHa.Clear();

            DataTable dt = new DataTable();

			dt = itemPha.GetSpeDruMaintenance();
			dt.Columns.Add("flag", typeof(string));
		
            if (dt == null)
            {
                MessageBox.Show(itemPha.Err);
                return -1;
            }
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                speHa.Add(dt.Rows[row][0], dt.Rows[row][1]);
            }
            this.neuSpread1_Sheet1.Rows.Count = dt.Rows.Count;

            this.neuSpread1_Sheet1.DataSource = dt.DefaultView;
            this.neuSpread1_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpread1_Sheet1.Columns[4].Locked = true;

			//{130E67E6-730B-493f-ABB5-8D0A72155BAE}消耗量大于最大量的用红色显示
			for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
			{
				decimal max = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 3].Text);
				decimal expand = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, 4].Text);
				if (expand > max)
				{
					this.neuSpread1_Sheet1.Rows[i].ForeColor = Color.Red;
				}
			}
            return 0;
        }

        /// <summary>
        /// 增加一条
        /// </summary>
        /// <returns></returns>
        private void Add()
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
        }

        /// <summary>
        /// 删除一条
        /// </summary>
        /// <returns></returns>
        private int DelSpeDrug()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return 1;
            }

            DialogResult rs = MessageBox.Show("确认删除该条维护信息吗?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (rs == DialogResult.No)
            {
                return 0;
            }
            string drugCode = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRow.Index, 0].Text;

            if (itemPha.DelSpeDrugMaintenance(drugCode) == -1)
            {
                MessageBox.Show(itemPha.Err);
                return -1;
            }

            this.neuSpread1_Sheet1.RemoveRows(this.neuSpread1_Sheet1.ActiveRow.Index, 1);
            MessageBox.Show("删除成功");
            return 0;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
			//{3B10E4E4-0824-4a09-9148-EF23A7EEF7F1}修改保存数据方式
			DateTime dt = itemPha.GetDateTimeFromSysDateTime();
			this.neuSpread1.StopCellEditing();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            #region  保存数据
            for (int row = 0; row < this.neuSpread1_Sheet1.Rows.Count; row++)
            {
				string flagStr = this.neuSpread1_Sheet1.Cells[row, 11].Text;
				if (flagStr == "add" || flagStr == "modify")
				{
					FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe = new FS.HISFC.Models.Pharmacy.DrugSpecial();
					//{902C80AC-58AE-4ff1-9B5A-0E72C80B025B}  edit by shizj 2010-05-03 
					drugSpe.Item.ID = this.neuSpread1_Sheet1.Cells[row, 0].Text;
					drugSpe.Item.Name = this.neuSpread1_Sheet1.Cells[row, 1].Text;
					drugSpe.Item.Specs = this.neuSpread1_Sheet1.Cells[row, 2].Text;
					drugSpe.Item.Oper.OperTime = dt;

					#region{3B7ABA42-7918-411e-8433-4821D7C4BB11}
					//FS.HISFC.Models.Pharmacy.Item item = itemPha.GetItem(drugSpe.ID);
					//if (item != null)
					//{
					drugSpe.Item.PriceCollection.PurchasePrice = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[row, 3].Text);//上限量
					//FS.FrameWork.Function.NConvert.ToDecimal( this.neuSpread1_Sheet1.Cells[row, 3].Text ) * item.PackQty; //上限量
					//消耗量不可以修改 锁定列
					drugSpe.Item.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[row, 4].Text);  //消耗量

					drugSpe.Item.PriceCollection.TopRetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[row, 7].Text);//追加量
					//drugSpe.Item.PriceCollection.TopRetailPrice = FS.FrameWork.Function.NConvert.ToDecimal( this.neuSpread1_Sheet1.Cells[row, 5].Text ) * item.PackQty; //追加量
					//}
					#endregion
					drugSpe.Oper.OperTime = dt;
					drugSpe.Oper.ID = this.itemPha.Operator.ID;
				
					if (flagStr == "add")
					{
						int flag = itemPha.InsertSpeDrugMaintenance(drugSpe);
						if (flag == -1)
						{
							FS.FrameWork.Management.PublicTrans.RollBack();
							MessageBox.Show("保存数据出错");
							return -1;
						}
					}
					else
					{
						int flag = itemPha.UpdateSpeDrugMaintenance(drugSpe);
						if (flag == -1)
						{
							FS.FrameWork.Management.PublicTrans.RollBack();
							MessageBox.Show("更新数据出错");
							return -1;
						}
					}
				}
				else
				{
					continue;
				}
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功");
			ClearFlag();
			return 1;
        }
        #endregion

		/// <summary>
		/// 清空标志位
		/// {3B10E4E4-0824-4a09-9148-EF23A7EEF7F1}
		/// </summary>
		private void ClearFlag()
		{
			for (int row = 0; row < this.neuSpread1_Sheet1.Rows.Count; row++)
			{
				this.neuSpread1_Sheet1.Cells[row, 11].Text = "";
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="activeRow"></param>
        private void ucDrugList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            string drugCode = sv.Cells[activeRow, 0].Text;
            DateTime beginTime = FS.FrameWork.Function.NConvert.ToDateTime(itemPha.GetDateTimeFromSysDateTime());
            int day = beginTime.Day;
            //beginTime = beginTime.AddDays(-day);
            beginTime=new DateTime (beginTime.Year,beginTime.Month,1,0,0,0);
            DateTime endTime = beginTime.AddMonths(1);

            FS.HISFC.Models.Pharmacy.Item item = itemPha.GetSpeDrugInfo(drugCode, beginTime, endTime);
            FS.HISFC.Models.Pharmacy.Item item2 = itemPha.GetItem(drugCode);//{B2945230-4AFC-4a30-B1E8-33E7CA547301}      
            decimal num = 0;
            string totNum = GetMonthCost(drugCode, beginTime, endTime, item2,ref num);//{B2945230-4AFC-4a30-B1E8-33E7CA547301}

            if (item == null)
            {
                MessageBox.Show("获取药品信息失败" + itemPha.Err);
            }
            this.neuSpread1_Sheet1.Rows.Count++;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count-1, 0].Text = item.ID;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count-1, 1].Text = item.Name;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count-1, 2].Text = item.Specs;
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count-1, 4].Text = item.Qty.ToString();
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 4].Text = totNum;
			this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.Rows.Count - 1, 11].Text = "add";//添加还是修改
        }

        private string GetMonthCost(string drugCode, DateTime begion,
            DateTime end, FS.HISFC.Models.Pharmacy.Item item2,ref decimal Num)//{B2945230-4AFC-4a30-B1E8-33E7CA547301}
        {
            string totNum = "";
            Num = FS.FrameWork.Function.NConvert.ToDecimal(itemPha.GetSpeDrugConsume(drugCode, begion, end));
            if (Num % item2.PackQty != 0)
            {
                totNum = string.Format("{0}", Math.Floor(Num / item2.PackQty)) + item2.PackUnit + string.Format("{0}", Num % item2.PackQty) + item2.MinUnit;
            }
            else
            {
                totNum = string.Format("{0}", Num / item2.PackQty) + item2.PackUnit;
            }
            return totNum;
        }

        /// <summary>
        /// 药品列表过滤，{62AAA983-5F51-4786-BAF9-FB032B84A23D}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDrug_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDrug.Text != string.Empty)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Text == this.cmbDrug.SelectedItem.ID)
                    {
                        this.neuSpread1_Sheet1.Rows[i].Visible = true;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Rows[i].Visible = true;
                }
            }
            
        }
        /// <summary>
        /// 药品列表过滤框文本改变事件，{62AAA983-5F51-4786-BAF9-FB032B84A23D}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDrug_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbDrug.Text == string.Empty)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Rows[i].Visible = true;
                }
            }
        }

		private void neuSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
		{
			if (this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 11].Text != "add")
				this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 11].Text = "modify";
		}
    }
}
