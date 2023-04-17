using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Items
{
    public partial class ucPriceHistory : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private System.Data.DataTable PriceHistry = null;
        private System.Data.DataView PricehistryDV = null;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        public ucPriceHistory()
        {
            InitializeComponent();
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("��ӡ", "��ӡ����", 0, true, false, null);
            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "��ӡ":
                    this.PrintInfo();
                    this.SerWidth();
                    break;
                default: break;
            }
            //base.ToolStrip_ItemClicked(sender, e);
        }

        private void initDataTable()
        {
            PriceHistry = new System.Data.DataTable("����");

            DataColumn his_DataColumn1 = new DataColumn("���۵���");
            his_DataColumn1.DataType = typeof(System.String);
            PriceHistry.Columns.Add(his_DataColumn1);

            DataColumn his_DataColumn2 = new DataColumn("��ҩƷ����");
            his_DataColumn2.DataType = typeof(System.String);
            PriceHistry.Columns.Add(his_DataColumn2);

            DataColumn his_DataColumn3 = new DataColumn("����");
            his_DataColumn3.DataType = typeof(System.String);
            PriceHistry.Columns.Add(his_DataColumn3);

            DataColumn his_DataColumn4 = new DataColumn("��ǰĬ�ϼ�");
            his_DataColumn4.DataType = typeof(System.Decimal);
            PriceHistry.Columns.Add(his_DataColumn4);

            DataColumn his_DataColumn5 = new DataColumn("����Ĭ�ϼ�");
            his_DataColumn5.DataType = typeof(System.Decimal);
            PriceHistry.Columns.Add(his_DataColumn5);

            DataColumn his_DataColumn6 = new DataColumn("��ǰ��ͯ��");
            his_DataColumn6.DataType = typeof(System.Decimal);
            PriceHistry.Columns.Add(his_DataColumn6);

            DataColumn his_DataColumn7 = new DataColumn("�����ͯ��");
            his_DataColumn7.DataType = typeof(System.Decimal);
            PriceHistry.Columns.Add(his_DataColumn7);

            DataColumn his_DataColumn8 = new DataColumn("��ǰ�����");
            his_DataColumn8.DataType = typeof(System.Decimal);
            PriceHistry.Columns.Add(his_DataColumn8);

            DataColumn his_DataColumn9 = new DataColumn("���������");
            his_DataColumn9.DataType = typeof(System.Decimal);
            PriceHistry.Columns.Add(his_DataColumn9);

            DataColumn his_DataColumn10 = new DataColumn("��Ч");
            his_DataColumn10.DataType = typeof(System.String);
            PriceHistry.Columns.Add(his_DataColumn10);
        }

        private void AddDataToTable(ArrayList List)
        {
            if (PriceHistry != null)
            {
                PriceHistry.Clear();
            }
            if (List != null)
            {
                try
                {
                    foreach (FS.HISFC.Models.Fee.Item.AdjustPrice info in List)
                    {
                        PriceHistry.Rows.Add(new object[] { info.AdjustPriceNO, 
                                                            info.OrgItem.ID, 
                                                            info.OrgItem.Name, 
                                                            info.OrgItem.Price, 
                                                            info.NewItem.Price, 
                                                            info.OrgItem.ChildPrice, 
                                                            info.NewItem.ChildPrice, 
                                                            info.OrgItem.SpecialPrice, 
                                                            info.NewItem.SpecialPrice, 
                                                            info.User03 });
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        private void AddDataToTree(ArrayList List)
        {
            try
            {
                this.neuTreeView1.Nodes.Clear();
                if (List != null)
                {
                    TreeNode node = null;
                    foreach (FS.HISFC.Models.Fee.Item.AdjustPrice info in List)
                    {
                        string temp = "���۵� " + info.AdjustPriceNO;// +" ��Ч����:" + info.Oper.OperTime.ToString("yyyy-MM-dd");
                        node = new TreeNode(temp);
                        node.Tag = info.AdjustPriceNO;
                        this.neuTreeView1.Nodes.Add(node);
                        node = null;
                    }
                }
            }
            catch (Exception ee)
            {
                string Error = ee.Message;
            }
        }

        private void GetData()
        {
            FS.HISFC.BizLogic.Manager.AdjustPrice pric = new FS.HISFC.BizLogic.Manager.AdjustPrice();
            string strMorning = this.neuDateTimePicker1.Value.ToShortDateString() + " 00:00:00";
            string strNight = this.neuDateTimePicker2.Value.ToShortDateString() + " 23:59:59";
            ArrayList List = pric.SelectPriceAdjustHead(Convert.ToDateTime(strMorning), Convert.ToDateTime(strNight));
            AddDataToTree(List);
        }

        private void SerWidth()
        {
            this.neuSpread1_Sheet1.Columns[1].Visible = false;
            FarPoint.Win.Spread.CellType.CheckBoxCellType check = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            neuSpread1_Sheet1.Columns[1].CellType = check;
        }

        private void PrintInfo()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
                p.PrintPreview(this.neuPanel4);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            PricehistryDV.RowFilter = "���� like  '" + neuTextBox1.Text + "%'";
            PricehistryDV.RowStateFilter = DataViewRowState.CurrentRows;
        }

        private void neuDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (this.neuDateTimePicker1.Value > this.neuDateTimePicker2.Value)
            {
                this.neuDateTimePicker1.Value = this.neuDateTimePicker2.Value;
            }
            GetData();
        }

        private void neuDateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (this.neuDateTimePicker1.Value > this.neuDateTimePicker2.Value)
            {
                this.neuDateTimePicker2.Value = this.neuDateTimePicker1.Value;
            }
            GetData();
        }

        private void neuTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                FS.HISFC.BizLogic.Manager.AdjustPrice pric = new FS.HISFC.BizLogic.Manager.AdjustPrice();
                ArrayList List = pric.SelectPriceAdjustTail(e.Node.Tag.ToString());
                AddDataToTable(List);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            SerWidth();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            int altKey = Keys.Alt.GetHashCode();
            if (keyData.GetHashCode() == altKey + Keys.P.GetHashCode())
            {
                //��ӡ
                this.PrintInfo();
            }
            if (keyData.GetHashCode() == altKey + Keys.P.GetHashCode())
            {
                //�˳�
                this.FindForm().Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        private void ucPriceHistory_Load(object sender, EventArgs e)
        {
            this.initDataTable();
            PricehistryDV = new DataView(PriceHistry);
            neuSpread1_Sheet1.DataSource = PricehistryDV;
            GetData();
            SerWidth();
        }
    }
}
