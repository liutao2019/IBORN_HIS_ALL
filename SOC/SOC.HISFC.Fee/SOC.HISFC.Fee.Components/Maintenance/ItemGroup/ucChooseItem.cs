using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.ItemGroup
{
    public partial class ucChooseItem : UserControl
    {
        public ucChooseItem()
        {
            InitializeComponent();

            this.neuSpread.KeyDown += new KeyEventHandler(neuSpread_KeyDown);
            this.neuSpread.CellDoubleClick+=new FarPoint.Win.Spread.CellClickEventHandler(neuSpread_CellDoubleClick); 

            this.Init();
        }

        #region 字段
        public event System.EventHandler ItemSelected;
        private ArrayList alItems = new ArrayList();
        private DataSet dsItems = new DataSet();
        private int spell = 0;
        #endregion
        #region 属性
        /// <summary>
        /// 项目列表
        /// </summary>
        public ArrayList NeuItems
        {
            get
            {
                if (alItems == null)
                {
                    alItems = new ArrayList();
                }

                return alItems;
            }
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            dsItems.Tables.Add("items");
            dsItems.Tables["items"].Columns.AddRange(new DataColumn[]
				{
					new DataColumn("自定义码",Type.GetType("System.String")),
					new DataColumn("编码",Type.GetType("System.String")),//ID
					new DataColumn("名称",Type.GetType("System.String")),//名称
					new DataColumn("拼音码",Type.GetType("System.String")),//拼音码
					new DataColumn("五笔码",Type.GetType("System.String"))
				});
            dsItems.CaseSensitive = false;
            this.neuSpread.DataSource = dsItems.Tables["items"].DefaultView;
            return 1;
        }

        /// <summary>
        /// 清除所有信息
        /// </summary>
        public void ClearItems()
        {
            this.alItems = new ArrayList();
        }
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public int AddItems(ArrayList items)
        {
            alItems = items;
            NeuObject objItem=null;
            dsItems.Tables["items"].Rows.Clear();
            FS.HISFC.Models.Base.ISpell objspell;

            try
            {
                for (int i = 0; i < alItems.Count; i++)
                {
                    objItem=alItems[i] as NeuObject;
                    objspell = objItem as FS.HISFC.Models.Base.ISpell;
                    dsItems.Tables["items"].Rows.Add(new object[]{
																	 objspell.UserCode,objItem.ID,objItem.Name,objspell.SpellCode,
																	 objspell.WBCode});
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("添加项目列表出错!" + error.Message, "ListBox");
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 获得选中项
        /// </summary>        
        /// <returns></returns>
        public NeuObject GetSelectedItem()
        {
            int index = this.fpItemGroup.ActiveRowIndex;
            if (index < 0 || index > this.fpItemGroup.RowCount - 1)
            {
                return null;
            }

            //获得ID
            string itemID = this.fpItemGroup.Cells[index, this.neuSpread.GetColumnIndex(0, "编码")].Value.ToString();
            FS.HISFC.Models.Base.Spell obj = null;
            if (!string.IsNullOrEmpty(itemID))
            {
                obj = new FS.HISFC.Models.Base.Spell();
                obj.ID = itemID;
                obj.Name = this.fpItemGroup.Cells[index, this.neuSpread.GetColumnIndex(0, "名称")].Value.ToString();
                obj.UserCode = this.fpItemGroup.Cells[index, this.neuSpread.GetColumnIndex(0, "自定义码")].Value.ToString();
                obj.SpellCode = this.fpItemGroup.Cells[index, this.neuSpread.GetColumnIndex(0, "拼音码")].Value.ToString();
                obj.WBCode = this.fpItemGroup.Cells[index, this.neuSpread.GetColumnIndex(0, "五笔码")].Value.ToString();
            }
            return obj;
        }

        /// <summary>
        /// 过滤项目
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Filter(string where)
        {
            dsItems.Tables["items"].DefaultView.RowFilter = Function.GetFilterStr(dsItems.Tables["items"].DefaultView, "%"+where+"%");

            if (dsItems.Tables["items"].DefaultView.ToTable().Rows.Count > 0)
            {
                this.fpItemGroup.ActiveRowIndex = 0;
                this.fpItemGroup.AddSelection(0, 0, 1, 1);
                this.neuSpread.ShowRow(0, this.fpItemGroup.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
            }


            return 1;
        }
        /// <summary>
        /// 移动下一行
        /// </summary>
        /// <returns></returns>
        public int NextRow()
        {
            int index = this.fpItemGroup.ActiveRowIndex;
            if (index >= this.fpItemGroup.RowCount - 1) return 1;

            this.fpItemGroup.ActiveRowIndex = index + 1;
            this.neuSpread.ShowRow(0, this.fpItemGroup.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
            return 1;
        }
        /// <summary>
        /// 移动上一行
        /// </summary>
        /// <returns></returns>
        public int PriorRow()
        {
            int index = this.fpItemGroup.ActiveRowIndex;
            if (index <= 0) return 1;

            this.fpItemGroup.ActiveRowIndex = index - 1;
            this.neuSpread.ShowRow(0, this.fpItemGroup.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Nearest);
            return 1;
        }

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Enter)
            {
                if (ItemSelected != null)
                    ItemSelected(this, e);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (ItemSelected != null)
                    ItemSelected(this, null);
            }
            return base.ProcessDialogKey(keyData);
        }

        void neuSpread_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ItemSelected != null)
                    ItemSelected(this, e);
            }
        }

        void neuSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (ItemSelected != null)
                ItemSelected(this, e);
        }

    }
}
