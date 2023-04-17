using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Manager.Items
{
    public partial class ucUndrugFeeRule : FS.HISFC.Components.Manager.Items.ucUnDrugItems
    {
        public ucUndrugFeeRule()
        {
            InitializeComponent();
        }

        #region �����

        private HISFC.BizLogic.Fee.UndrugFeeRegularManager feeRegularManager = new FS.HISFC.BizLogic.Fee.UndrugFeeRegularManager();

        private FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();

        private FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        private Hashtable feeTypeHash = new Hashtable();

        private Hashtable itemHash = new Hashtable();

        private FS.HISFC.Components.Manager.Items.ucHandleFeeRule ucHandleFeeRule = null;

        private DataTable dtFeeRegular = null;

        private string FilterAll = "";

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region ����

        /// <summary>
        /// ��� �շѹ���
        /// </summary>
        private void FillFeeRule()
        {
            ArrayList feeRule = new ArrayList();

            //feeRule = this.conManager.GetAllList("FEEREGULAR");
            feeRule.AddRange(new FS.FrameWork.Models.NeuObject[] { new FS.FrameWork.Models.NeuObject("02", "�����շ�,ÿ�첻������������", "�����շ�,ÿ�첻������������"), new FS.FrameWork.Models.NeuObject("03", "ÿ�����ͬ����Ŀ���ܳ�����������", "ÿ�����ͬ����Ŀ���ܳ�����������"), new FS.FrameWork.Models.NeuObject("04", "��Ŀ����", "��Ŀ����"),new FS.FrameWork.Models.NeuObject("05","��������Ŀ����","����������ָ��ֵ�����ڴ���ָ��ֵ�򻥳�") });

            foreach (FS.FrameWork.Models.NeuObject tempObj in feeRule)
            {
                //��ʼ���շѹ����ϣ��
                this.feeTypeHash.Add(tempObj.ID, tempObj);
            }

            if (this.itemHash.Count == 0)
            {
                foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in base.undrugList)
                {
                    if (this.itemHash.ContainsKey(undrug.ID) == false)
                    {
                        this.itemHash.Add(undrug.ID, undrug);
                    }
                }
            }
        }

        private void InitFeeRule()
        {
            ArrayList al = this.feeRegularManager.QueryAllFeeRegular();
            if (al == null)
            {
                MessageBox.Show(this, "��ȡ�շѹ�����Ϣ����" + this.feeRegularManager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.AddToDataTable(al);
        }

        private void InitDataTable()
        {
            dtFeeRegular = new DataTable();

            dtFeeRegular.Columns.AddRange(new DataColumn[] { 
                    new DataColumn("�������",typeof(System.String)),
                    new DataColumn("��Ŀ����",typeof(System.String)),
                    new DataColumn("�Զ�����",typeof(System.String)),
                    new DataColumn("��Ŀ����",typeof(System.String)),
                    new DataColumn("��������",typeof(System.String)),
                    new DataColumn("��������",typeof(System.String)),
                    new DataColumn("��������",typeof(System.String)),
                    new DataColumn("������Ŀ",typeof(System.String)),
                    new DataColumn("����Ա",typeof(System.String)),
                    new DataColumn("����ʱ��",typeof(System.DateTime)),
                    new DataColumn("��ע",typeof(System.String))
                            });
            this.dtFeeRegular.DefaultView.Sort = "��Ŀ���� DESC";
            this.neuSpread1_Sheet1.DataSource = dtFeeRegular.DefaultView;

        }

        private void AddToDataTable(ArrayList al)
        {
            if (al == null)
            {
                return;
            }

            this.InitDataTable();

            foreach (FS.HISFC.Models.Fee.Item.UndrugFeeRegular feeRegular in al)
            {
                DataRow dr = this.dtFeeRegular.NewRow();

                FS.HISFC.Models.Fee.Item.Undrug undrug = itemHash[feeRegular.Item.ID] as FS.HISFC.Models.Fee.Item.Undrug;
                if (string.IsNullOrEmpty(undrug.UserCode))
                {
                    MessageBox.Show("�Զ�����Ϊ�գ���Ŀ���ƣ�" + feeRegular.Item.Name);
                    //return;
                }
                dr["�������"] = feeRegular.ID;
                dr["��Ŀ����"] = feeRegular.Item.ID;
                dr["�Զ�����"] = undrug.UserCode;
                dr["��Ŀ����"] = feeRegular.Item.Name;
                if (FS.FrameWork.Function.NConvert.ToInt32(feeRegular.LimitCondition) == 1)
                {
                    dr["��������"] = "��ʱ��";
                }
                else
                {
                    dr["��������"] = "������";
                }
                dr["��������"] = ((FS.FrameWork.Models.NeuObject)this.feeTypeHash[feeRegular.Regular.ID]).Name;
                dr["��������"] = feeRegular.DayLimit.ToString();
                string[] mutex = feeRegular.LimitItem.ID.Split('|');

                string itemListStr = "";

                if (mutex != null && mutex.Length > 0)
                {
                    for (int j = 0; j < mutex.Length; j++)
                    {
                        if (string.IsNullOrEmpty(mutex[j].ToString()))
                        {
                            continue;
                        }
                        //��ѯ��ҩƷ��Ϣ������
                        if (itemHash.ContainsKey(mutex[j].ToString()))
                        {
                            itemListStr += itemHash[mutex[j].ToString()].ToString() + ",";
                        }
                        else
                        {
                            FS.HISFC.Models.Fee.Item.Undrug undrugTemp = this.itemManager.GetValidItemByUndrugCode(mutex[j].ToString());
                            if (undrugTemp == null)
                            {
                                MessageBox.Show(this, "��ȡ��ҩƷ��Ϣ����" + this.itemManager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            itemListStr += undrugTemp.Name + ",";
                            this.itemHash.Add(undrugTemp.ID, undrugTemp);
                        }

                    }

                    if (string.IsNullOrEmpty(itemListStr) == false)
                    {
                        itemListStr = itemListStr.Substring(0, itemListStr.Length - 1);
                    }
                }
                dr["������Ŀ"] = itemListStr;
                dr["����Ա"] = feeRegular.Oper.ID;
                dr["����ʱ��"] = feeRegular.Oper.OperTime.ToString();
                dr["��ע"] = feeRegular.Memo;

                this.dtFeeRegular.Rows.Add(dr);

            }

            this.neuSpread1_Sheet1.Columns[1].Visible = false;

            this.dtFeeRegular.AcceptChanges();
        }

        private void AddRegular()
        {
            if (base.neuSpreadItems.ActiveSheet.ActiveRowIndex < 0)
            {
                return;
            }

            if (ucHandleFeeRule == null)
            {
                ucHandleFeeRule = new ucHandleFeeRule();

                this.ucHandleFeeRule.feeTypeHash = this.feeTypeHash;
                this.ucHandleFeeRule.itemHash = this.itemHash;

                this.ucHandleFeeRule.InitFeeRegular(new ArrayList(base.undrugList));

            }
            this.ucHandleFeeRule.operMode = 0;
            FS.HISFC.Models.Fee.Item.UndrugFeeRegular undrugFeeRegular = new FS.HISFC.Models.Fee.Item.UndrugFeeRegular();
            undrugFeeRegular.Item.ID = base.neuSpreadItems.ActiveSheet.GetText(base.neuSpreadItems.ActiveSheet.ActiveRowIndex, base.GetCloumn("��Ŀ���"));
            undrugFeeRegular.Item.Name = base.neuSpreadItems.ActiveSheet.GetText(base.neuSpreadItems.ActiveSheet.ActiveRowIndex, base.GetCloumn("��Ŀ����"));
            this.ucHandleFeeRule.SetFeeRegular(undrugFeeRegular);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�շѹ�����ϸ";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucHandleFeeRule, FormBorderStyle.FixedDialog);

            //ˢ�½���
            this.InitFeeRule();
            this.FilterRegular();
        }

        private void DeleteRegular()
        {
            if (this.neuSpread1.ActiveSheet.ActiveRowIndex < 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, 0].Text) == false)
            {
                if (feeRegularManager.DeleteUndrugFeeRegular(this.neuSpread1.ActiveSheet.Cells[this.neuSpread1.ActiveSheet.ActiveRowIndex, 0].Text) <= 0)
                {
                    MessageBox.Show(this, "ɾ������" + this.feeRegularManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                MessageBox.Show(this, "ɾ���ɹ�!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.neuSpread1_Sheet1.RemoveRows(this.neuSpread1.ActiveSheet.ActiveRowIndex, 1);
                this.dtFeeRegular.AcceptChanges();
            }
        }

        private void FilterRegular()
        {

            DataTable dtTemp = base.dvUndrugItem.ToTable();

            if (dtTemp == null)
            {
                return;
            }

            string filter = "";
            if (dtTemp.Rows.Count == base.dvUndrugItem.Table.Rows.Count)
            {
                filter = this.FilterAll;
            }
            else
            {
                foreach (DataRow dr in dtTemp.Rows)
                {
                    filter += "'" + dr["��Ŀ���"].ToString() + "',";
                }

                if (string.IsNullOrEmpty(filter) == false)
                {
                    filter = filter.Substring(0, filter.Length - 1);
                }
                else
                {
                    filter = "''";
                }
            }

            this.dtFeeRegular.DefaultView.RowFilter = "��Ŀ���� in (" + filter + ")";
        }

        #endregion

        #region �¼�

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ////ɾ��˫���¼�
            base.neuSpreadItems.CellDoubleClick -= new FarPoint.Win.Spread.CellClickEventHandler(base.neuSpreadItems_CellDoubleClick);

            ////���ӵ����¼�
            //this.ucUnDrugItems1.neuSpreadItems.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpreadItems_CellClick);

            this.FillFeeRule();
            this.InitFeeRule();

            DataTable dtTemp = base.dvUndrugItem.Table;

            if (dtTemp == null)
            {
                return;
            }

            this.FilterAll = "";
            foreach (DataRow dr in dtTemp.Rows)
            {
                this.FilterAll += "'" + dr["��Ŀ���"].ToString() + "',";
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService = base.OnInit(sender, neuObject, param);

            this.toolBarService.AddToolButton("����շѹ���", "����շѹ���", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            this.toolBarService.AddToolButton("ɾ���շѹ���", "ɾ���շѹ���", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            base.ToolStrip_ItemClicked(sender, e);

            switch (e.ClickedItem.Text)
            {
                case "����շѹ���":
                    this.AddRegular();
                    break;
                case "ɾ���շѹ���":
                    this.DeleteRegular();
                    break;
                default:
                    break;
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0)
            {
                return;
            }

            if (ucHandleFeeRule == null)
            {
                ucHandleFeeRule = new ucHandleFeeRule();
                this.ucHandleFeeRule.feeTypeHash = this.feeTypeHash;
                this.ucHandleFeeRule.itemHash = this.itemHash;

                this.ucHandleFeeRule.InitFeeRegular(new ArrayList(base.undrugList));
            }
            FS.HISFC.Models.Fee.Item.UndrugFeeRegular undrugFeeRegular = this.feeRegularManager.GetSingleFeeRegularById(this.neuSpread1_Sheet1.Cells[e.Row, 0].Text);
            if (undrugFeeRegular == null)
            {
                MessageBox.Show(this, "��ȡ�շѹ�����Ϣ����" + this.feeRegularManager.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.ucHandleFeeRule.operMode = 1;
            this.ucHandleFeeRule.SetFeeRegular(undrugFeeRegular);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�շѹ�����ϸ";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucHandleFeeRule, FormBorderStyle.FixedDialog);

            //ˢ�½���
            this.InitFeeRule();
            this.FilterRegular();
        }

        protected override void neuSpreadItems_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            base.neuSpreadItems_CellClick(sender, e);

            if (e.Row >= 0)
            {
                this.dtFeeRegular.DefaultView.RowFilter = "��Ŀ���� in ('" + base.neuSpreadItems.ActiveSheet.GetText(e.Row, base.GetCloumn("��Ŀ���")) + "')";
            }
        }

        protected override void GenerateRowFilter(string whereValue, string sct, string sft, string state, string tag, string itemType)
        {
            base.GenerateRowFilter(whereValue, sct, sft, state, tag, itemType);

            if (string.IsNullOrEmpty(whereValue) && string.IsNullOrEmpty(sct) && string.IsNullOrEmpty(sft))
            {
                this.dtFeeRegular.DefaultView.RowFilter = "��Ŀ���� in (" + this.FilterAll + ")";
            }
            else
            {
                this.FilterRegular();
            }
        }

        #endregion
    }
}
