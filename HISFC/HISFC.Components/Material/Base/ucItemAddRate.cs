using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Material.Base
{
    public partial class ucItemAddRate : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucItemAddRate()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��Ʒ������Ϣ������
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// ObjectHelper
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �Ӽ۷�ʽ
        /// </summary>
        ArrayList alRateKindInfo = new ArrayList();

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected virtual void Init()
        {
            this.ucMaterialItemList1.ChooseDataEvent += new ucMaterialItemList.ChooseDataHandler(ucMaterialItemList1_ChooseDataEvent);

            //{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            this.ucMaterialItemList1.ShowMaterialList(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);

            this.SetColumnType();

            FS.HISFC.Models.Material.MaterialAddRateEnumService matAddRateEnum = new FS.HISFC.Models.Material.MaterialAddRateEnumService();

            ArrayList al = FS.HISFC.Models.Material.MaterialAddRateEnumService.List();
            this.alRateKindInfo = al;
            this.helper.ArrayObject = al;
        }

        /// <summary>
        /// �������
        /// </summary>
        protected virtual void SetColumnType()
        {
            FS.HISFC.Models.Material.MaterialAddRate addRate = new FS.HISFC.Models.Material.MaterialAddRate();
            FarPoint.Win.Spread.CellType.ComboBoxCellType cmbAddRuleCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();

            cmbAddRuleCellType.Items = addRate.RateKind.StringItems;
            this.FpMetItemAddRule_Sheet1.Columns[5].CellType = cmbAddRuleCellType;
            this.FpMetItemAddRule_Sheet1.Columns[0].Visible = false;
            this.FpMetItemAddRule_Sheet1.Columns[1].Locked = true;
            this.FpMetItemAddRule_Sheet1.Columns[2].Locked = true;
            this.FpMetItemAddRule_Sheet1.Columns[3].Locked = true;
            this.FpMetItemAddRule_Sheet1.Columns[4].Locked = true;
            this.FpMetItemAddRule_Sheet1.Columns[8].Visible = false;
        }

        /// <summary>
        /// ������ݵ����
        /// </summary>
        /// <param name="item"></param>
        protected virtual void AddDataToFP(FS.HISFC.Models.Material.MaterialItem item, bool isNew)
        {
            int rowCount = this.FpMetItemAddRule_Sheet1.RowCount;
            this.FpMetItemAddRule_Sheet1.Rows.Add(rowCount, 1);

            this.FpMetItemAddRule_Sheet1.Cells[rowCount, 0].Text = item.ID;
            this.FpMetItemAddRule_Sheet1.Cells[rowCount, 1].Text = item.Name;
            this.FpMetItemAddRule_Sheet1.Cells[rowCount, 2].Text = item.Specs;
            this.FpMetItemAddRule_Sheet1.Cells[rowCount, 3].Text = item.UnitPrice.ToString();
            this.FpMetItemAddRule_Sheet1.Cells[rowCount, 4].Text = item.MinUnit;
            this.FpMetItemAddRule_Sheet1.Cells[rowCount, 5].Text = this.helper.GetName(item.AddRule);

            if (isNew)
            {
                this.FpMetItemAddRule_Sheet1.Cells[rowCount, 8].Text = "ADD";
            }

            this.FpMetItemAddRule_Sheet1.Rows[rowCount].Tag = item;
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        protected virtual void QueryAll()
        {
            this.FpMetItemAddRule_Sheet1.RowCount = 0;
            List<FS.HISFC.Models.Material.MaterialItem> al = this.itemManager.QueryMetItemHaveAddRule();
            if (al != null && al.Count > 0)
            {
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Material.MaterialItem item = al[i] as FS.HISFC.Models.Material.MaterialItem;
                    this.AddDataToFP(item, false);
                }
            }
        }

        protected virtual int SaveAddRule()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.itemManager.Connection);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            for (int i = 0; i < this.FpMetItemAddRule_Sheet1.RowCount; i++)
            {
                if (this.FpMetItemAddRule_Sheet1.Cells[i, 8].Text != "")
                {
                    int iReturn = 0;
                    string addRule = "";
                    string itemID = "";

                    itemID = this.FpMetItemAddRule_Sheet1.Cells[i, 0].Text;
                    addRule = this.helper.GetID(this.FpMetItemAddRule_Sheet1.Cells[i, 5].Text.Trim());
                    iReturn = this.itemManager.UpdateMetItemAddRule(itemID, addRule);

                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����������Ŀ�Ӽ���Ϣʧ�ܣ�" + this.itemManager.Err);
                        return -1;
                    }
                }

            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ɹ���");
            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAll();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveAddRule();
            return base.OnSave(sender, neuObject);
        }

        #endregion

        #region ���з���

        #endregion

        #region �¼�

        private void ucMaterialItemList1_ChooseDataEvent(FarPoint.Win.Spread.SheetView sv, int activeRow)
        {
            //by yuyun 08-7-28 ��һ�б���Զ�����  ԭ�Զ������г����ʱ���{7019A2A6-ADCA-4984-944B-C4F1A312449A}
            //string itemCode = sv.Cells[activeRow, 0].Text;
            string itemCode = sv.Cells[activeRow, 10].Text;

            FS.HISFC.Models.Material.MaterialItem item = this.itemManager.GetMetItemByMetID(itemCode);
            for (int i = 0; i < this.FpMetItemAddRule_Sheet1.RowCount; i++)
            {
                if (item.ID == this.FpMetItemAddRule_Sheet1.Cells[i, 0].Text)
                {
                    this.FpMetItemAddRule.Focus();
                    this.FpMetItemAddRule_Sheet1.ActiveRowIndex = i;

                    return;
                }
            }
            this.AddDataToFP(item, true);
        }

        private void ucItemAddRate_Load(object sender, EventArgs e)
        {
            this.Init();

            this.QueryAll();
        }

        private void FpMetItemAddRule_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.FpMetItemAddRule_Sheet1.Cells[e.Row, 0].Text == "" && e.Column == 8)
            {
                this.FpMetItemAddRule_Sheet1.Cells[e.Row, 0].Text = "MODIFY";

            }
        }

        #endregion
    }
}

