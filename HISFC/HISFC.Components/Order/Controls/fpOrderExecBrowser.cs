using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ҽ��ִ�е���ѯ]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class fpOrderExecBrowser : System.Windows.Forms.UserControl
    {
        public fpOrderExecBrowser()
        {
            InitializeComponent();

        }


        public int ColumnIndexSelection = 0;
        public int ColumnIndexCombo = 0;
        public int ColumnIndexUsage = 0;
        public int ColumnIndexFrequency = 0;

        /// <summary>
        /// ҽ��������
        /// </summary>
        private HISFC.BizLogic.Order.Order myOrderMgr = new FS.HISFC.BizLogic.Order.Order();

        #region {13EAF764-E1CA-4d5a-8250-056AD1DEE61B}
        /// <summary>
        /// ����ҵ����
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// �����б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
        #endregion

        #region ����

        /// <summary>
        /// ��õ�ǰ�к�
        /// </summary>
        public int GetCurrentRowIndex
        {
            get
            {
                return this.fpSpread.Sheets[0].ActiveRowIndex;
            }
        }
        /// <summary>
        /// ��ǰҽ��
        /// </summary>
        public FS.HISFC.Models.Order.ExecOrder CurrentExecOrder
        {
            get
            {
                try
                {
                    if (this.fpSpread.Sheets[0].ActiveRowIndex >= 0)
                    {
                        if (this.fpSpread.Sheets[0].ActiveRow.Tag != null)
                        {
                            return this.fpSpread.Sheets[0].ActiveRow.Tag as FS.HISFC.Models.Order.ExecOrder;
                        }
                    }
                }
                catch { }
                return null;
            }
        }


        protected bool bIsShowBed = true;
        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        public bool IsShowBed
        {
            get
            {
                return this.bIsShowBed;
            }
            set
            {
                this.bIsShowBed = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾRowHeader
        /// </summary>
        public bool IsShowRowHeader
        {
            set
            {
                this.fpSpread.Sheets[0].RowHeaderVisible = value;
            }
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        public void Init()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
            txtType.WordWrap = true;

            this.fpSpread.Sheets[0].GrayAreaBackColor = Color.White;
            this.fpSpread.Sheets[0].ColumnHeader.DefaultStyle.BackColor = Color.White;

            this.fpSpread.Sheets[0].RowHeader.DefaultStyle.BackColor = Color.Yellow;
            //this.fpSpread.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
            //fpSpread.Sheets[0].SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
            //fpSpread.Sheets[0].SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
            this.fpSpread.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;

            #region ������
            int i = 0;
            this.fpSpread.Sheets[0].Columns[i].Width = 80;
            this.fpSpread.Sheets[0].Columns[i].Label = "����";
            this.fpSpread.Sheets[0].Columns[i].Locked = false;
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 40;
            this.fpSpread.Sheets[0].Columns[i].Label = "ѡ��";
            this.fpSpread.Sheets[0].Columns[i].Locked = false;
            this.fpSpread.Sheets[0].Columns[i].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            ColumnIndexSelection = i;
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 250;
            this.fpSpread.Sheets[0].Columns[i].Label = "��Ŀ����";
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            this.fpSpread.Sheets[0].Columns[i].CellType = txtType;
            this.fpSpread.Sheets[0].Columns[i].Visible = true;
            this.fpSpread.Sheets[0].Columns[i].AllowAutoSort = true;
            ColumnIndexCombo = i;
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 20;
            this.fpSpread.Sheets[0].Columns[i].Label = "��Ϻ�";
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            this.fpSpread.Sheets[0].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread.Sheets[0].Columns[i].Visible = false;
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 20;
            this.fpSpread.Sheets[0].Columns[i].Label = "���";
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 40;
            this.fpSpread.Sheets[0].Columns[i].Label = "����";
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            this.fpSpread.Sheets[0].Columns[i].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 40;
            this.fpSpread.Sheets[0].Columns[i].Label = "��λ";
            this.fpSpread.Sheets[0].Columns[i].CellType = txtType;
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 60;
            this.fpSpread.Sheets[0].Columns[i].Label = "�÷�";
            this.fpSpread.Sheets[0].Columns[i].CellType = txtType;
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            this.fpSpread.Sheets[0].Columns[i].AllowAutoSort = true;
            ColumnIndexUsage = i;
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 60;
            this.fpSpread.Sheets[0].Columns[i].Label = "Ƶ��";
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            ColumnIndexFrequency = i;
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 100;
            this.fpSpread.Sheets[0].Columns[i].Label = "ʹ��ʱ��";
            //this.fpSpread1.Sheets[0].Columns[i].CellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 20;
            this.fpSpread.Sheets[0].Columns[i].Label = "���";
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            this.fpSpread.Sheets[0].Columns[i].Visible = false;


            #region {13EAF764-E1CA-4d5a-8250-056AD1DEE61B}
            i++;
            //����ȡҩ���� 
            this.fpSpread.Sheets[0].Columns[i].Width = 80;
            this.fpSpread.Sheets[0].Columns[i].Label = "ȡҩ����";
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            this.fpSpread.Sheets[0].Columns[i].Visible = true;
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.deptHelper.ArrayObject = this.deptManager.GetDeptmentAllValid();
            }
            #endregion

            #region �Ƿ���ҩ��ʶ
            i++;
            this.fpSpread.Sheets[0].Columns[i].Width = 80;
            this.fpSpread.Sheets[0].Columns[i].Label = "�Ƿ���ҩ";
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            this.fpSpread.Sheets[0].Columns[i].CellType = txtType;
            this.fpSpread.Sheets[0].Columns[i].ForeColor = Color.Red;
            this.fpSpread.Sheets[0].Columns[i].Font = new Font("����", 9F, FontStyle.Bold);
            this.fpSpread.Sheets[0].Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.fpSpread.Sheets[0].Columns[i].Visible = false;

            #endregion

            i++;

            this.fpSpread.Sheets[0].Columns[i].Width = 180;
            this.fpSpread.Sheets[0].Columns[i].Label = "������Ϣ";
            this.fpSpread.Sheets[0].Columns[i].Locked = true;
            this.fpSpread.Sheets[0].Columns[i].Visible = false;
            fpSpread.Sheets[0].Columns[i].CellType = txtType;

            #endregion
            this.fpSpread.Sheets[0].RowCount = 0;
            this.fpSpread.Sheets[0].ColumnCount = i + 1;
            this.fpSpread.Sheets[0].SetColumnMerge(0, FarPoint.Win.Spread.Model.MergePolicy.Restricted);

            for (int col = 0; col < fpSpread.Sheets[0].ColumnCount; col++)
            {
                fpSpread.Sheets[0].Columns[col].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
        }

        public void BeginInit()
        {
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).BeginInit();
            for (int i = 0; i < this.fpSpread.Sheets.Count; i++)
                ((System.ComponentModel.ISupportInitialize)(this.fpSpread.Sheets[i])).BeginInit();
        }
        public void EndInit()
        {
            ((System.ComponentModel.ISupportInitialize)(this.fpSpread)).EndInit();
            for (int i = 0; i < this.fpSpread.Sheets.Count; i++)
                ((System.ComponentModel.ISupportInitialize)(this.fpSpread.Sheets[i])).EndInit();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
        }
        #endregion

        #region ����

        /// <summary>
        /// ��ʾҽ��������
        /// </summary>
        /// <param name="inOrder"></param>
        /// <returns></returns>
        private string ShowOrderName(FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            //��Ŀ����+���+�Ա�ҩ+�۸�+Ƥ��+��ע
            string price = "";

            if (inOrder.Item.ID == "999" || !inOrder.OrderType.IsCharge)
            {
                price = "[" + "0Ԫ/" + inOrder.Item.PriceUnit + "]";
            }
            else
            {
                if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (inOrder.Unit == SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).PackUnit)
                    {
                        price = "[" + ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ/" + ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).PriceUnit + "]";
                    }
                    else
                    {
                        price = "[" + (((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).Price / ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ/" + ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).MinUnit + "]";
                    }
                }
                else
                {
                    if (inOrder.Item.Price > 0)
                    {
                        price = "[" + inOrder.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ/" + ((FS.HISFC.Models.Fee.Item.Undrug)inOrder.Item).PriceUnit.Trim() + "]";
                    }
                }
            }

            //�Ա������б��  ���ڻ�ʿ��ӡ���ݺ�ҽ������ʾ����
            string byoStr = "";

            FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
            string strHypoTest = orderMgr.TransHypotest(inOrder.HypoTest);

            inOrder.Item.Name = inOrder.Item.Name + byoStr + strHypoTest;

            //ҽ������ 
            if (inOrder.Item.Specs == null || inOrder.Item.Specs.Trim() == "")
            {
                return inOrder.Item.Name + (inOrder.IsPermission ? "���̡�" : "") + price;
            }
            else
            {
                return inOrder.Item.Name + (inOrder.IsPermission ? "���̡�" : "") + "[" + inOrder.Item.Specs + "]" + price;
            }
        }

        /// <summary>
        /// ���һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public int AddRow(object sender, int i)
        {
            if (sender.GetType() != typeof(FS.HISFC.Models.Order.ExecOrder)) return -1;
            FS.HISFC.Models.Order.ExecOrder order = sender as FS.HISFC.Models.Order.ExecOrder;

            #region ��ѯ���������ҩƷ���ģ�����Ϊ��Ʒѣ���������ʾ
            if (order.Order.IsSubtbl)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
                bool bCharge = con.GetControlParam<bool>("S00020", false);
                //��ҩ�Ʒ�
                if (!bCharge)
                {
                    if (order.Order.IsSubtbl)
                    {
                        bool bChargeSubtbl = con.GetControlParam<bool>("200050", false);
                        //ҩƷ������ҩƷ��Ʒ�
                        if (!bChargeSubtbl)
                        {
                            System.Collections.ArrayList alSubtblOrder = orderManager.QueryOrderByCombNO(order.Order.Combo.ID, true);
                            foreach (FS.HISFC.Models.Order.Inpatient.Order subtblOrder in alSubtblOrder)
                            {
                                if (!subtblOrder.IsSubtbl)
                                {
                                    //ȷ������ҩΪҩƷ
                                    if (subtblOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                    {
                                        return 0;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            this.fpSpread.Sheets[0].Rows.Add(i, 1);

            //if (order.DateUse <= new DateTime(order.Order.BeginTime.Year, order.Order.BeginTime.Month, order.Order.BeginTime.Day, 12, 0, 0))
            //{
            //    this.fpSpread.Sheets[0].Rows[i].ForeColor = Color.Blue;
            //}

            if (this.bIsShowBed)
            {
                this.fpSpread.Sheets[0].SetValue(i, 0, order.Order.Patient.Name + "[" + Classes.Function.BedDisplay(order.Order.Patient.PVisit.PatientLocation.Bed.ID) + "]", false);
            }
            else
            {
                this.fpSpread.Sheets[0].SetValue(i, 0, order.Order.Patient.Name, false);
            }
            if (order.Order.Combo.ID == "0" || order.Order.Combo.ID == "")
            {
            }
            else
            {

                this.fpSpread.Sheets[0].SetValue(i, 3, order.Order.Combo.ID + order.DateUse.ToString(), false);

            }

            string itemName = ShowOrderName(order.Order);
            this.fpSpread.Sheets[0].SetValue(i, 2, itemName, false);
            //if (order.Order.Item.Specs == null || order.Order.Item.Specs == "")//���
            //{
            //    this.fpSpread.Sheets[0].SetValue(i, 2, order.Order.Item.Name, false);
            //}
            //else
            //{
            //    this.fpSpread.Sheets[0].SetValue(i, 2, order.Order.Item.Name + "[" + order.Order.Item.Specs + "]", false);
            //}

            //ҩƷ
            //ҩƷ��Ŀ
            if (order.Order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                FS.HISFC.Models.Pharmacy.Item item = (FS.HISFC.Models.Pharmacy.Item)order.Order.Item;
                if (order.Order.OrderType.IsDecompose)//����ҽ��
                {
                    if (order.Order.Item.ID == "999" || (item.BaseDose != 0 && order.Order.DoseOnce != 0)) //��ҩû��һ�μ�������ʾ����
                    {
                        decimal d = 0;
                        if (order.Order.Item.ID == "999")
                        {
                            if (string.IsNullOrEmpty(order.Order.Unit))
                            {
                                order.Order.Unit = order.Order.DoseUnit;
                            }
                            if (string.IsNullOrEmpty(item.MinUnit))
                            {
                                item.MinUnit = order.Order.DoseUnit;
                            }
                            d = order.Order.DoseOnce;
                        }
                        else if (order.Order.Qty == 0)//����Ϊ��,�ɲ�ֵ�ҩ
                        {
                            d = order.Order.DoseOnce / item.BaseDose; //����
                        }
                        else//���ɲ��
                        {
                            d = order.Order.Qty;
                        }
                        this.fpSpread.Sheets[0].SetValue(i, 5, d, false);
                        this.fpSpread.Sheets[0].SetValue(i, 6, item.MinUnit, false);
                    }
                    else//��ҩ���г�ҩ��һ�μ���Ϊ��
                    {

                        if (order.Order.HerbalQty == 0)
                        {
                            order.Order.HerbalQty = 1;
                        }
                        this.fpSpread.Sheets[0].SetValue(i, 5, order.Order.Qty * order.Order.HerbalQty, false);//��ʾ����
                        if (order.Order.Qty == 0)//һ�μ���Ϊ��&&����Ϊ��� Ϊ��������
                        {
                            this.fpSpread.Sheets[0].SetValue(i, 6, "��������Ϊ�㣡", false);
                            this.fpSpread.Sheets[0].Cells[i, 6].BackColor = Color.CadetBlue;
                        }
                        else//��ʾ��������λ
                        {
                            this.fpSpread.Sheets[0].SetValue(i, 6, order.Order.Unit, false);
                        }
                    }

                }
                else//��ʱҽ��
                {
                    this.fpSpread.Sheets[0].SetValue(i, 5, order.Order.Qty, false);
                    this.fpSpread.Sheets[0].SetValue(i, 6, item.MinUnit, false);
                }
                #region {13EAF764-E1CA-4d5a-8250-056AD1DEE61B}
                //����ȡҩ����
                this.fpSpread.Sheets[0].Columns[11].Visible = true;
                this.fpSpread.Sheets[0].SetValue(i, 11, this.deptHelper.GetName(order.Order.StockDept.ID), false);//ȡҩ����
                #endregion

                #region �Ƿ���ҩ
                FS.HISFC.Models.Pharmacy.Item pha = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Order.Item.ID);
                if (pha != null && pha.SpecialFlag.Equals("1"))
                {
                    this.fpSpread.Sheets[0].SetValue(i, 12, "��");
                }
                #endregion
            }
            else//��ҩƷ
            {

                this.fpSpread.Sheets[0].SetValue(i, 5, order.Order.Qty, false);

                this.fpSpread.Sheets[0].SetValue(i, 6, order.Order.Unit, false);
                #region {13EAF764-E1CA-4d5a-8250-056AD1DEE61B}
                this.fpSpread.Sheets[0].Columns[11].Visible = false;
                #endregion
            }
            this.fpSpread.Sheets[0].SetValue(i, 7, order.Order.Usage.Name, false);
            this.fpSpread.Sheets[0].SetValue(i, 8, order.Order.Frequency.ID, false);
            this.fpSpread.Sheets[0].SetValue(i, 9, order.DateUse.ToString(), false);
            this.fpSpread.Sheets[0].SetValue(i, 10, order.Order.SortID, false);

            this.fpSpread.Sheets[0].Rows[i].Tag = order;
            return 0;
        }
        /// <summary>
        /// Ĭ����ӵ����һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public int AddRow(object sender)
        {
            return this.AddRow(sender, this.fpSpread.Sheets[0].Rows.Count);
        }

        public void RefreshComboNo()
        {
            Classes.Function.DrawCombo(this.fpSpread.Sheets[0], 3, 4);
        }

        /// <summary>
        /// �ֽ⵱����ǰ��ҽ����ʾ������ɫ
        /// </summary>
        public void ShowOverdueOrder()
        {
            //FS.FrameWork.Management.DataBaseManger dbm = new FS.FrameWork.Management.DataBaseManger();
            //for (int i = 0; i < this.fpSpread.Sheets[0].RowCount; i++)
            //{
            //    if (FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread.Sheets[0].Cells[i, 9].Text) < FS.FrameWork.Function.NConvert.ToDateTime(dbm.GetSysDate()))
            //    {
            //        this.fpSpread.Sheets[0].Rows[i].BackColor = System.Drawing.Color.PowderBlue;
            //        this.fpSpread.Sheets[0].Cells[i,0].BackColor = System.Drawing.Color.White;
            //    }
            //}
        }
        /// <summary>
        /// ��õ�ǰ������
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int GetFpRowCount(int i)
        {
            return this.fpSpread.Sheets[i].RowCount;
        }

        /// <summary>
        /// ���ݴ������������ʾ������ҽ����Ϣ
        /// {E08AD6B3-4987-44b0-A5A9-B660D24FBC4D}
        /// </summary>
        /// <param name="days"></param>
        public void DeleteRow(int days)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlManager = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            string date = controlManager.GetControlParam<string>("200011", true);

            DateTime dt = this.myOrderMgr.GetDateTimeFromSysDateTime();
            try
            {
                dt = FS.FrameWork.Function.NConvert.ToDateTime(dt.ToString("yyyy-MM-dd") + " " + date).AddDays(days);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return;
            }
            for (int i = this.fpSpread.Sheets[0].RowCount - 1; i >= 0; i--)
            {
                DateTime dtOrder = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread_Sheet1.Cells[i, 9].Value.ToString());
                if (dtOrder > dt)
                {
                    this.fpSpread.Sheets[0].RemoveRows(i, 1);
                }
            }
        }

        /// <summary>
        /// clear row
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < this.fpSpread.Sheets.Count; i++)
            {
                this.fpSpread.Sheets[i].RowCount = 0;
                fpSpread.Sheets[i].Columns[fpSpread.Sheets[i].ColumnCount - 1].Visible = false;
            }
        }
        #endregion
    }
}
