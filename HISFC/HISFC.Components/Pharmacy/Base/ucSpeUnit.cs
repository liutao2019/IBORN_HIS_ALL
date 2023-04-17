using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������: ���ⵥλά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-04]<br></br>
    /// <˵��>
    ///     ������
    /// </˵��>
    /// </summary>
    public partial class ucSpeUnit : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSpeUnit()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        private ucSpeUnitManager uc = null;

        private System.Data.DataSet myDataSet = new System.Data.DataSet();

        private System.Data.DataView myDataView;

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "�����༶��λ��Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ���༶��λ��Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "����")
            {
                this.Add();
            }
            if (e.ClickedItem.Text == "ɾ��")
            {
                this.Del();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        /// <summary>
        /// DataSet��ʼ��
        /// </summary>
        private void InitDataSet()
        {
            this.myDataSet.Tables.Clear();
            this.myDataSet.Tables.Add();

            this.myDataView = new System.Data.DataView(this.myDataSet.Tables[0]);
            this.neuSpread1_Sheet1.DataSource = this.myDataView;

            System.Type tStr = System.Type.GetType("System.String");
            System.Type tDec = System.Type.GetType("System.Decimal");

            this.myDataSet.Tables[0].Columns.AddRange(new System.Data.DataColumn[] {
																					   new DataColumn("����",tStr),
																					   new DataColumn("����",tStr),
																					   new DataColumn("���",tStr),
																					   new DataColumn("��װ��λ",tStr),
				                                                                       new DataColumn("��װ����",tDec),
																					   new DataColumn("��С��λ",tStr),
																					   new DataColumn("ƴ����",tStr),
																					   new DataColumn("�����",tStr),
																					   new DataColumn("�Զ�����",tStr)
																				   });

            DataColumn[] keys = new DataColumn[] { this.myDataSet.Tables[0].Columns["����"] };
            this.myDataSet.Tables[0].PrimaryKey = keys;

        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Init()
        {
            this.InitDataSet();

            if (uc == null)
            {
                uc = new ucSpeUnitManager();
            }

            this.uc.SaveDataEvent += new HISFC.Components.Pharmacy.Base.ucSpeUnitManager.SaveDataHander( uc_SaveDataEvent );
            this.uc.Init();

            this.neuSpread1_Sheet1.RowHeaderVisible = true;
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
        }

        /// <summary>
        /// ����ҩƷ������ʾ 
        /// </summary>
        private void ShowList()
        {
            this.myDataSet.Tables[0].Rows.Clear();

            ArrayList al = this.itemManager.QuerySpeUnitList();
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ά�������ⵥλ�б�������" + itemManager.Err));
                return;
            }
            if (this.uc == null)
            {
                this.uc.HsItem = new Hashtable();
            }

            FS.HISFC.Models.Pharmacy.Item item;

            foreach (FS.HISFC.Models.Pharmacy.DrugSpeUnit info in al)
            {
                item = this.itemManager.GetItem(info.Item.ID);
                if (!this.uc.HsItem.ContainsKey(item.ID))
                {
                    this.uc.HsItem.Add(item.ID, item);
                }

                this.myDataSet.Tables[0].Rows.Add(new object[]
					{
						item.ID,
						item.Name,
						item.Specs,
						item.PackUnit,
						item.PackQty.ToString(),
						item.MinUnit,
						item.NameCollection.SpellCode,
						item.NameCollection.WBCode,
						item.NameCollection.UserCode
					});
            }

            this.myDataSet.AcceptChanges();

            this.SetFormat();

        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private void AddItem(FS.HISFC.Models.Pharmacy.Item item)
        {
            this.myDataSet.Tables[0].Rows.Add(new object[] {
																		item.ID,
																		item.Name,
																		item.Specs,
																		item.PackUnit,
																		item.PackQty.ToString(),
																		item.MinUnit,
																		item.NameCollection.SpellCode,
																		item.NameCollection.WBCode,
																		item.NameCollection.UserCode
																   });
        }

        /// <summary>
        /// ������Ŀ
        /// </summary>
        private void Add()
        {
            if (uc != null)
            {
                this.uc.Item = null;
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            }
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        private void Del()
        {
            DialogResult rs = MessageBox.Show(Language.Msg("ȷ��ɾ������ҩƷ��"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.No)
                return;

            int i = this.neuSpread1_Sheet1.ActiveRowIndex;

            //��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.itemManager.DeleteSpeUnit(this.neuSpread1_Sheet1.Cells[i, 0].Text) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ɾ������ʧ��");
                return;
            }
            if (this.uc.HsItem != null)
            {
                if (this.uc.HsItem.ContainsKey(this.neuSpread1_Sheet1.Cells[i, 0].Text))
                {
                    this.uc.HsItem.Remove(this.neuSpread1_Sheet1.Cells[i, 0].Text);
                }
            }

            this.neuSpread1_Sheet1.RemoveRows(i, 1);

            this.myDataSet.AcceptChanges();

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show(Language.Msg("ɾ���ɹ�"));

        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private void FilterItem()
        {
            if (this.myDataSet.Tables[0].Rows.Count == 0) return;

            try
            {
                string queryCode = "";
                queryCode = "%" + this.txtFilter.Text.Trim() + "%";

                string filter = "(ƴ���� LIKE '" + queryCode + "') OR " +
                    "(����� LIKE '" + queryCode + "') OR " +
                    "(�Զ����� LIKE '" + queryCode + "')";

                //���ù�������
                this.myDataView.RowFilter = filter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.SetFormat();
        }

        /// <summary>
        /// ��ʽ��
        /// </summary>
        private void SetFormat()
        {
            this.neuSpread1_Sheet1.Columns[0].Visible = false;		//����

            this.neuSpread1_Sheet1.Columns[6].Visible = false;
            this.neuSpread1_Sheet1.Columns[7].Visible = false;
            this.neuSpread1_Sheet1.Columns[8].Visible = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();

                this.ShowList();
            }

            base.OnLoad(e);
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (uc != null)
            {
                this.uc.SetSpeUnit(this.uc.HsItem[this.neuSpread1_Sheet1.Cells[e.Row, 0].Text] as FS.HISFC.Models.Pharmacy.Item);

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            }
        }

        private void uc_SaveDataEvent(object data)
        {
            try
            {
                FS.HISFC.Models.Pharmacy.Item item = data as FS.HISFC.Models.Pharmacy.Item;
                object[] keys = new object[] { item.ID };
                DataRow row = this.myDataSet.Tables[0].Rows.Find(keys);
                if (row == null)
                {
                    this.AddItem(data as FS.HISFC.Models.Pharmacy.Item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            this.FilterItem();
        }

    }
}
