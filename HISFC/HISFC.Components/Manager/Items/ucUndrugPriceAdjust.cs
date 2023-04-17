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
    public partial class ucUndrugPriceAdjust : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #region ����ȫ�ֱ���

        //��������洢��ҩƷ��Ϣ
        private DataTable UndrugTable = null;
        private DataView UndrugView = null;
        //������� �洢Ҫ���۵���Ϣ
        private DataTable AdjustTable = null;
        private DataView AdjustView = null;
        private System.Windows.Forms.CheckBox immediate;
        //����ҵ��������
        FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
        private bool IsTextFocus = false;

        #endregion

        public ucUndrugPriceAdjust()
        {
            InitializeComponent();
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveInfo();
            return 1;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("��ʷ", "��ʾ��ʷ��Ϣ", 1, true, false, null);
            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "��ʷ":
                    this.AdjustInfo();
                    break;
                default: break;
            }
            //base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private void loadUndrug()
        {
            try
            {
                UndrugTable = new DataTable();
                System.Type dtStr = System.Type.GetType("System.String");
                System.Type dtDec = System.Type.GetType("System.Decimal");
                System.Type dtDTime = System.Type.GetType("System.DateTime");
                System.Type dtBool = System.Type.GetType("System.Boolean");
                UndrugTable = new DataTable();
                UndrugTable.Columns.AddRange(new DataColumn[] {
																	new DataColumn( "����",  dtStr ),		//0
																	new DataColumn("����",    dtStr),       //0
                                                                    new DataColumn("������",	 dtStr),    //2
                                                                    new DataColumn("���",    dtStr),       //3
																	new DataColumn("ƴ����",  dtStr),		//4  
																	new DataColumn("���",	 dtStr),		//5
																	new DataColumn("Ĭ�ϼ�",  dtDec),		//6
																	new DataColumn("��ͯ��",  dtDec),		//7
																	new DataColumn("�����",  dtDec) ,		//8
                                                                    new DataColumn("������",dtStr)
																});

                //��������Ϊ����
                CreateKeys(UndrugTable);
                UndrugView = new DataView(UndrugTable);
                this.neuSpread1_Sheet1.DataSource = UndrugView;

                ArrayList alReturn = new ArrayList();//���صķ�ҩƷ��Ϣ;
                //��÷�ҩƷ��Ϣ
                alReturn = item.Query("all", "1");
                if (alReturn == null)
                {
                    MessageBox.Show("��÷�ҩƷ��Ϣ����!" + item.Err);
                    return;
                }
                //ѭ��������Ϣ
                foreach (FS.HISFC.Models.Fee.Item.Undrug obj in alReturn)
                {
                    //{F8CA49B3-D36B-4172-9C8E-7D5CDD077A42} ��Ŀ�б������θ�����Ŀ ���ܶԸ�����Ŀ���е���
                    if (obj.UnitFlag == "1")
                    {
                        continue;
                    }

                    DataRow row = UndrugTable.NewRow();
                    SetRow(obj, row);
                    UndrugTable.Rows.Add(row);
                }
                UndrugTable.AcceptChanges(); //�������
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��������Ϊ����
        /// </summary>
        private void CreateKeys(DataTable table)
        {
            DataColumn[] keys = new DataColumn[] { table.Columns["����"] };
            table.PrimaryKey = keys;
        }

        /// <summary>
        /// �����Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        private void SetRow(FS.HISFC.Models.Fee.Item.Undrug obj, DataRow row)
        {
            row["����"] = obj.ID;			//0                                             
            row["����"] = obj.Name;         //1 
            row["������"] = obj.UserCode;   //2	
            row["���"] = obj.Specs;        //3
            row["ƴ����"] = obj.SpellCode;	//4											
            row["���"] = obj.WBCode;		//5																
            row["Ĭ�ϼ�"] = obj.Price;		//6											
            row["��ͯ��"] = obj.ChildPrice;		//7											
            row["�����"] = obj.SpecialPrice;		//8		
            row["������"] = obj.GBCode;													
        }

        /// <summary>
        /// ��� ������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        private void SetAdjustRow(FS.HISFC.Models.Fee.Item.AdjustPrice obj, DataRow row)
        {
            row["����"] = obj.OrgItem.ID;			    //0                                             
            row["����"] = obj.OrgItem.Name;				//1																	
            row["��ǰĬ�ϼ�"] = obj.OrgItem.Price;		//5											
            row["��ǰ��ͯ��"] = obj.OrgItem.ChildPrice;		//6											
            row["��ǰ�����"] = obj.OrgItem.SpecialPrice;		//7		
            row["����Ĭ�ϼ�"] = obj.NewItem.Price;		//5											
            row["�����ͯ��"] = obj.NewItem.ChildPrice;		//6											
            row["���������"] = obj.NewItem.SpecialPrice;		//7	
        }

        /// <summary>
        /// ����
        /// </summary>
        private void AdjustInfo()
        {
            //����������ʷ����
            ucPriceHistory form = new ucPriceHistory();
            //form.Show();
            FS.FrameWork.WinForms.Classes.Function.ShowControl(form);
            
            //Manager.FrmPricehistry form = new FrmPricehistry();
            //form.ShowDialog();
        }

        /// <summary>
        /// �õ���Ҫ���۵���Ϣ �����б� ׼���������
        /// </summary>
        /// <returns></returns>
        private ArrayList GetInfo(string SequenceNo)
        {
            ArrayList ItemList = new ArrayList();
            if (AdjustTable == null)
            {
                return null;
            }
            FS.HISFC.Models.Fee.Item.AdjustPrice item = null;
            //ѭ��ȡ���� 
            foreach (DataRow row in AdjustTable.Rows)
            {
                item = new FS.HISFC.Models.Fee.Item.AdjustPrice();
                item.AdjustPriceNO = SequenceNo;
                item.OrgItem.ID = row["����"].ToString();
                item.OrgItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["��ǰĬ�ϼ�"].ToString());
                item.OrgItem.ChildPrice = FS.FrameWork.Function.NConvert.ToDecimal(row["��ǰ��ͯ��"].ToString());
                item.OrgItem.SpecialPrice = FS.FrameWork.Function.NConvert.ToDecimal(row["��ǰ�����"].ToString());
                item.NewItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(row["����Ĭ�ϼ�"].ToString());
                item.NewItem.ChildPrice = FS.FrameWork.Function.NConvert.ToDecimal(row["�����ͯ��"].ToString());
                item.NewItem.SpecialPrice = FS.FrameWork.Function.NConvert.ToDecimal(row["���������"].ToString());
                item.BeginTime = this.dtpImmediate.Value;
                if (this.ckbImmediate.Checked) //�Ƿ�ʱ��Ч
                {
                    item.User03 = "����Ч";// ��ʱ��Ч ���ó�δ��Ч
                }
                else
                {
                    item.User03 = "δ��Ч"; //�Ǽ�ʱ��Ч �����ó�����Ч
                }
                ItemList.Add(item);
                item = null;
            }
            return ItemList;
        }

        private void SaveInfo()
        {
            try
            {
                this.neuSpread1.StopCellEditing();
                FS.HISFC.BizLogic.Manager.AdjustPrice price = new FS.HISFC.BizLogic.Manager.AdjustPrice();
                //��ȡ���۵���ˮ��
                string SequenceNo = price.GetAdjustPriceSequence();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction Addtrans = new FS.FrameWork.Management.Transaction(price.Connection);
                //Addtrans.BeginTransaction();

                price.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                #region �ж�ʱ���Ƿ���Ч
                if (!ckbImmediate.Checked)  //��ʱ��Ч
                {
                    if (this.dtpImmediate.Value < System.DateTime.Now)
                    {
                        this.dtpImmediate.Focus();
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ч��ʱ�䲻��С�ڵ�ǰʱ��"));
                        return;
                    }
                }
                #endregion
                //��ȡҪ���۵���Ϣ
                ArrayList list = GetInfo(SequenceNo);
                if (list == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ������Ϣ����"));
                    return;
                }

                #region  ����ͷ�����ϸ�� ���������Ϣ
                foreach (FS.HISFC.Models.Fee.Item.AdjustPrice info in list)
                {
                    //�������ǰ���ۻ�û����Ч�� ������ε����г�ͻ�� ��������ǰ�ĵ��ۼ�¼
                    int temp = price.UpdateAdjustPriceDetail(info.OrgItem.ID);
                }
                //�������ͷ��
                //���������ϸ��
                bool PriceHead = false;
                bool Result = true;
                foreach (FS.HISFC.Models.Fee.Item.AdjustPrice info in list)
                {
                    if (!PriceHead)
                    {
                        //�����ͷ���в���һ���µļ�¼ fin_com_adjustundrugpricehead 
                        if (price.InsertAdjustPrice(info) <= 0)
                        {
                            Result = false;
                            break;
                        }
                        else
                        {
                            PriceHead = true;
                        }
                    }
                    //�������ϸ�в����µļ�¼ fin_com_adjustundrugpricedetai
                    if (price.InsertAdjustPriceDetail(info) <= 0)
                    {
                        Result = false;
                        break;
                    }
                }
                if (Result)
                {
                    //�ύ����
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ�"));
                    if (AdjustTable != null)
                    {
                        AdjustTable.Clear();
                    }
                }
                else
                {
                    //������Ϣ
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���������Ϣʧ��"));
                }
                #endregion

                #region �����������Ч �����ҩƷ �۸�
                if (ckbImmediate.Checked) //������Ч
                {
                    FS.HISFC.BizLogic.Fee.Item manItem = new FS.HISFC.BizLogic.Fee.Item();
                    manItem.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    FS.HISFC.Models.Fee.Item.Undrug temItem = null;
                    foreach (FS.HISFC.Models.Fee.Item.AdjustPrice info in list)
                    {
                        //��ת����ITEMȻ��ִ�и��²���
                        temItem = new FS.HISFC.Models.Fee.Item.Undrug();
                        //ҩƷ����
                        temItem.ID = info.OrgItem.ID;
                        //Ĭ�ϼ�
                        temItem.Price = info.NewItem.Price;
                        //��ͯ��
                        temItem.ChildPrice = info.NewItem.ChildPrice;
                        //�����
                        temItem.SpecialPrice = info.NewItem.SpecialPrice;
                        //ִ�и��²�����
                        if (manItem.AdjustPrice(temItem) < 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg(manItem.Err));
                            return;
                        }
                        //{6DF09817-9532-4129-BE60-DED731C7E5B9} �������к��и���Ŀ�ĸ�����Ŀ�۸�

                        //manItem.QueryZTListByDetailItem(temItem);


                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(ex.Message));
            }
        }

        /// <summary>
        /// �����������Ч �ȱ����ҩƷ��Ϣ ���ٱ��������Ϣ
        /// </summary>
        private bool UpdateUndrug(ArrayList list)
        {
            bool Result = true;
            if (list == null)
            {
                return true;
            }
            FS.HISFC.Models.Fee.Item.Undrug temItem = null;
            foreach (FS.HISFC.Models.Fee.Item.AdjustPrice info in list)
            {
                //��ת����ITEMȻ��ִ�и��²���
                temItem = new FS.HISFC.Models.Fee.Item.Undrug();
                //ҩƷ����
                temItem.ID = info.OrgItem.ID;
                //Ĭ�ϼ�
                temItem.Price = info.NewItem.Price;
                //��ͯ��
                temItem.ChildPrice = info.NewItem.ChildPrice;
                //�����
                temItem.SpecialPrice = info.NewItem.SpecialPrice;
            }
            return Result;
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private void loadAdjustUndrug()
        {
            try
            {
                AdjustTable = new DataTable();
                System.Type dtStr = System.Type.GetType("System.String");
                System.Type dtDec = System.Type.GetType("System.Decimal");
                System.Type dtDTime = System.Type.GetType("System.DateTime");
                System.Type dtBool = System.Type.GetType("System.Boolean");
                AdjustTable = new DataTable();
                AdjustTable.Columns.AddRange(new DataColumn[] {
																   new DataColumn( "����",  dtStr ),		//0
																   new DataColumn("����",    dtStr),		//1
																   new DataColumn("��ǰĬ�ϼ�",  dtDec),		//5
																   new DataColumn("��ǰ��ͯ��",  dtDec),		//6
																   new DataColumn("��ǰ�����",  dtDec), 		//7
																   new DataColumn("����Ĭ�ϼ�",  dtDec),		//5
																   new DataColumn("�����ͯ��",  dtDec),		//6
																   new DataColumn("���������",  dtDec), 		//7
															   });

                //��������Ϊ����
                CreateKeys(AdjustTable);
                AdjustView = new DataView(AdjustTable);
                this.neuSpread2_Sheet1.DataSource = AdjustView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��ѯ������λ��
        /// </summary>
        /// <returns></returns>
        private int GetColumnKey(string str)
        {
            foreach (FarPoint.Win.Spread.Column col in this.neuSpread1_Sheet1.Columns)
            {
                if (col.Label == str)
                {
                    return col.Index;
                }
            }
            return 0;
        }

        /// <summary>
        /// ����һ�����ݵ����۴���
        /// </summary>
        private void AddDataInfo()
        {
            try
            {
                if (this.neuSpread1_Sheet1.RowCount < 1)
                {
                    return; //���û�����ݷ��ؿ� 
                }
                ArrayList alInfo = new ArrayList();
                FS.HISFC.Models.Fee.Item.Undrug info = new FS.HISFC.Models.Fee.Item.Undrug();
                //�����ݿ��ȡҪ�޸ĵ���Ϣ
                alInfo = item.Query(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, GetColumnKey("����")].Text, "all");
                if (info == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ��ҩƷ��Ϣ����"));
                    return;
                }
                if (alInfo == null)
                {
                    return;
                }
                info.ID = ((FS.HISFC.Models.Fee.Item.Undrug)alInfo[0]).ID;
                info.Name = ((FS.HISFC.Models.Fee.Item.Undrug)alInfo[0]).Name;
                info.Price = ((FS.HISFC.Models.Fee.Item.Undrug)alInfo[0]).Price;
                info.ChildPrice = ((FS.HISFC.Models.Fee.Item.Undrug)alInfo[0]).ChildPrice;
                info.SpecialPrice = ((FS.HISFC.Models.Fee.Item.Undrug)alInfo[0]).SpecialPrice;

                //�������
                object[] keys = new object[] { info.ID };
                DataRow tempRow = AdjustTable.Rows.Find(keys);
                if (tempRow != null)
                {
                    MessageBox.Show(info.Name + "�Ѿ��ڵ�����Ϣ������");
                    return;
                }

                FS.HISFC.Models.Fee.Item.AdjustPrice tail = new FS.HISFC.Models.Fee.Item.AdjustPrice();
                tail.OrgItem.ID = info.ID; //��ҩƷ����
                tail.OrgItem.Name = info.Name;//��ҩƷ����
                tail.OrgItem.Price = info.Price;//Ĭ�ϼ�
                tail.OrgItem.ChildPrice = info.ChildPrice;//��ͯ��
                tail.OrgItem.SpecialPrice = info.SpecialPrice;//�����
                tail.NewItem.Price = info.Price;
                tail.NewItem.ChildPrice = info.ChildPrice;
                tail.NewItem.SpecialPrice = info.SpecialPrice;
                //����
                DataRow row = AdjustTable.NewRow();
                //�������
                SetAdjustRow(tail, row);
                //���ӵ�����
                AdjustTable.Rows.Add(row);
                //�������
                UndrugTable.AcceptChanges();
                LockfpSpread2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LockfpSpread2()
        {
            FarPoint.Win.Spread.CellType.NumberCellType num = new FarPoint.Win.Spread.CellType.NumberCellType();
            num.MaximumValue = 999999;
            num.DecimalPlaces = 4;

            this.neuSpread2_Sheet1.Columns[2].CellType = num;
            neuSpread2_Sheet1.Columns[3].CellType = num;
            neuSpread2_Sheet1.Columns[4].CellType = num;
            neuSpread2_Sheet1.Columns[5].CellType = num;
            neuSpread2_Sheet1.Columns[6].CellType = num;
            neuSpread2_Sheet1.Columns[7].CellType = num;

            neuSpread2_Sheet1.Columns[0].Visible = false;
            neuSpread2_Sheet1.Columns[1].Locked = true;
            neuSpread2_Sheet1.Columns[2].Locked = true;
            neuSpread2_Sheet1.Columns[3].Locked = true;
            neuSpread2_Sheet1.Columns[4].Locked = true;
        }

        private void ucUndrugPriceAdjust_Load(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ժ�...");
            Application.DoEvents();
            //��ʼ����ҩƷ�б�
            loadUndrug();

            this.neuSpread1_Sheet1.Columns[0].Visible = false; ;

            //��ʼ�����۱�
            loadAdjustUndrug();
            LockfpSpread2();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            AddDataInfo();
        }

        private void ckbImmediate_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbImmediate.Checked)
            {
                this.dtpImmediate.Visible = false;
            }
            else
            {
                this.dtpImmediate.Visible = true;
            }
        }

        private void neuTextBox1_Leave(object sender, EventArgs e)
        {
            this.IsTextFocus = false;
        }

        private void neuTextBox1_Enter(object sender, EventArgs e)
        {
            this.IsTextFocus = true;
        }

        private void neuTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                //�趨 �ƶ����ٸ����һ��
                this.neuSpread1.SetViewportTopRow(0, neuSpread1_Sheet1.ActiveRowIndex - 5);
                //��ǰλ�������ƶ�һ��
                this.neuSpread1_Sheet1.ActiveRowIndex--;
                this.neuSpread1_Sheet1.AddSelection(this.neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                //�趨 �ƶ����ٸ����һ��
                this.neuSpread1.SetViewportTopRow(0, neuSpread1_Sheet1.ActiveRowIndex - 5);
                //��ǰλ�������ƶ�һ��
                this.neuSpread1_Sheet1.ActiveRowIndex++;
                this.neuSpread1_Sheet1.AddSelection(neuSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
        }

        private void neuTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                AddDataInfo(); //����һ�����ݵ� ���۴���
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            int AltKey = Keys.Alt.GetHashCode();
            if (keyData.GetHashCode() == AltKey + Keys.S.GetHashCode())
            {
                //����
                SaveInfo();
            }

            if (keyData.GetHashCode() == AltKey + Keys.A.GetHashCode())
            {
                //������ʷ
                AdjustInfo();
            }

            if (keyData.GetHashCode() == AltKey + Keys.X.GetHashCode())
            {
                //�˳�
                this.FindForm().Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!IsTextFocus) //��ѯ��û�л�ý��� 
            {
                if (keyData.GetHashCode() == Keys.Enter.GetHashCode())
                {
                    //�س����� 
                    if (neuSpread2_Sheet1.Rows.Count > 0)
                    {
                        //��ǰ���
                        int i = neuSpread2_Sheet1.ActiveRowIndex;
                        int j = neuSpread2_Sheet1.ActiveColumnIndex;
                        if (j + 1 <= neuSpread2_Sheet1.ColumnCount - 1)
                        {
                            if (j <= 4)
                            {
                                j = 4; //ֱ������Ҫ�޸ĵ���
                            }
                            //�������һ�� ������ƶ�һ��
                            neuSpread2_Sheet1.SetActiveCell(i, j + 1);
                        }
                        else if (i < neuSpread2_Sheet1.Rows.Count)
                        {
                            //�Ѿ������һ��  ����������һ�� ��������һ��
                            neuSpread2_Sheet1.SetActiveCell(i + 1, 5);
                        }
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            string temp = " like  '%" + this.neuTextBox1.Text + "%' ";
            this.UndrugView.RowFilter = "����" + temp + " or " +"������"+temp+ " or " + " ����" + temp + " or " + "ƴ����" + temp + " or " + "���" + temp + " or " + "������" + temp+" or  ������"+temp;
            this.UndrugView.RowStateFilter = DataViewRowState.CurrentRows;
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void neuPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
