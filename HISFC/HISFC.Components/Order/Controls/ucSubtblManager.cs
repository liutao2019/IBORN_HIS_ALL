using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ҽ�������޸�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucSubtblManager : UserControl
    {
        public ucSubtblManager()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// operFlag "1" ������� "2" ɾ������
        /// isShowSubtblFlag  �����Ƿ���Ҫ���"@"��־
        /// sender ɾ��/ֹͣ��ҽ�����ļ�¼
        /// </summary>
        public delegate void ShowSubtblFlagEvent(string operFlag, bool isShowSubtblFlag, object sender);
        /// <summary>
        /// �Ƿ���Ҫ��ҽ�������"@"���ı�־
        /// </summary>
        public event ShowSubtblFlagEvent ShowSubtblFlag;
        private string orderID;										//ҽ����ˮ��		
        private string comboNo;										//ҽ����Ϻ�
        private bool isDCOrder = false;								//��ǰ������ҽ���Ƿ�Ϊ��ֹͣ��
        private ArrayList addSubInfo;							    //���������ӵĸ���
        private ArrayList editSubInfo;								//���θ��ĵĸ���
        private FS.HISFC.Models.Base.Employee myOperator;		//����Ա
        private FS.HISFC.Models.RADT.PatientInfo patientInfo;	//��ǰ�����Ļ�����Ϣ �������ڵ�������

        /// <summary>
        /// ҩƷ�ͷ�ҩƷ�ļ۸�Ϊ0�Ƿ��շѣ�0����ȡ��1��ȡ��Ĭ��ֵΪ0����ȡ
        /// </summary>
        private string isFeeWhenPriceZero = "-1";

        #endregion

        #region ����
        /// <summary>
        /// ҽ����ˮ��
        /// </summary>
        public string OrderID
        {
            set
            {
                this.orderID = value;
            }
        }

        /// <summary>
        /// ҽ����Ϻ�
        /// </summary>
        public string ComboNo
        {
            set
            {
                this.comboNo = value;
                this.Clear();
                this.QuerySubtbl();
            }
        }

        /// <summary>
        /// ��ǰ������ҽ���Ƿ�Ϊ��ֹͣ��
        /// </summary>
        public bool IsDCOrder
        {
            set
            {
                this.isDCOrder = value;
            }
        }
        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        protected FS.HISFC.Models.Base.Employee Operator
        {
            get
            {
                if (myOperator == null) 
                    myOperator = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                return myOperator;
            }
            set
            {
                this.myOperator = value;
            }
        }


        /// <summary>
        /// ��ǰ��ѯ�Ļ�����Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                if (this.patientInfo != value)
                {
                    this.patientInfo = value;
                    this.Clear();
                }
            }
        }
        
        /// <summary>
        /// �Ƿ�������ʾ 
        /// </summary>
        public bool IsVerticalShow
        {
            set
            {
                if (value)
                {
                    this.Width = 452;
                }
                else
                {
                    this.Width = 750;
                }

            }
        }
        /// <summary>
        /// �������ӵĸ���
        /// </summary>
        public ArrayList AddSubInfo
        {
            get
            {
                if (this.addSubInfo == null)
                    this.addSubInfo = new ArrayList();
                return this.addSubInfo;
            }
        }
        /// <summary>
        /// ���θ��ĵĸ���
        /// </summary>
        public ArrayList EditSubInfo
        {
            get
            {
                if (this.editSubInfo == null)
                    this.editSubInfo = new ArrayList();
                return this.editSubInfo;
            }
        }

        /// <summary>
        /// ҩƷ�ͷ�ҩƷ�ļ۸�Ϊ0�Ƿ��շѣ�0����ȡ��1��ȡ��Ĭ��ֵΪ0����ȡ
        /// </summary>
        public string IsFeeWhenPriceZero
        {
            get
            {
                if (this.isFeeWhenPriceZero == "-1")
                {
                    FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                    this.isFeeWhenPriceZero = con.GetControlParam<string>("FEE001", false, "0");
                }
                return this.isFeeWhenPriceZero;
            }
        }

        #endregion

        #region ��ʼ������
        protected void InitSubtbl()
        {
            this.ucInputItem1.ShowCategory = FS.HISFC.Components.Common.Controls.EnumCategoryType.SysClass;
            this.ucInputItem1.ShowItemType = FS.HISFC.Components.Common.Controls.EnumShowItemType.Undrug;
            this.ucInputItem1.IsShowDeptGroup = false;
            this.ucInputItem1.DeptCode = this.Operator.Dept.ID;
            //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
            this.ucInputItem1.IsIncludeMat = true;

            this.ucInputItem1.Init();
        }
       
        #endregion

        #region ����

        /// <summary>
        /// �����Ҳ���ʾ���沼��
        /// </summary>
        public void SetRightShow()
        {
            this.panelItem.Size = new Size(724, 62);
            this.neuLabel2.Location = new Point(2, 43);
            this.txtQty.Location = new Point(45, 39);
            this.txtUnit.Location = new Point(98, 38);
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        protected void QuerySubtbl()
        {
            //������Ӹ���
            if (this.comboNo == "")
                return;
            ArrayList al = CacheManager.InOrderMgr.QuerySubtbl(this.comboNo);
            if (al == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѯҽ�����ĳ���") + CacheManager.InOrderMgr.Err);
            }
            try
            {
                this.sheetView1.RowCount = 0;
            }
            catch { }
            //�жϵ�ǰҽ���Ƿ�Ϊֹͣҽ��
            if (CacheManager.InOrderMgr.QueryOneOrderState(this.orderID) == 3)
            {
                this.isDCOrder = true;
            }
            else
            {
                this.isDCOrder = false;
            }
            for (int i = 0; i < al.Count; i++)
            {
                this.AddSubtbl((FS.HISFC.Models.Order.Inpatient.Order)al[i], false);
            }
        }
        /// <summary>
        /// ���һ������
        /// </summary>
        protected void AddSubtbl(FS.HISFC.Models.Order.Inpatient.Order order, bool isNew)
        {
            if (order == null)
                return;

            //����ֹͣ���Ĳ���ʾ
            if (!isNew && order.Status == 3)
            {
                if (!this.isDCOrder)
                    return;
            }

            if (!this.IsFee(order))  //order.Item.Price == 0)
            {
                MessageBox.Show("�۸�Ϊ�㣬�����ϸ���������");
                return;
            }

            //����ִ�п���Ϊ���߿���
            //order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();//this.Operator.Nurse.Clone();
            //order.ReciptDept = order.Patient.PVisit.PatientLocation.Dept.Clone();//this.Operator.Nurse.Clone();

            //�˴������޸Ŀ������Һ�ִ�п��ң���֤���ĺ�ҽ���Ŀ�����ִ�п���һ��
            //���ڼ���ĸ��ģ����⴦��ִ�п���Ϊ��������
            //if (order.Item.SysClass.ID.ToString() == "UL")
            //{
            //    order.ExeDept.ID = order.ReciptDept.ID;
            //    order.ExeDept.Name = order.ReciptDept.Name;
            //}

            order.Oper.ID = this.Operator.ID;
            order.Oper.Name = this.Operator.Name;
            //order.ReciptDoctor.ID = this.Operator.ID;
            //order.ReciptDoctor.Name = this.Operator.Name;
            order.IsSubtbl = true;

            //�޸Ŀ���ʱ��
            order.MOTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            //���޸Ŀ�ʼʱ�䣬���⿪ʼʱ����´ηֽ�ʱ����
            //order.BeginTime = order.MOTime;

            //���һ��
            this.sheetView1.Rows.Add(0, 1);
            this.sheetView1.Cells[0, 0].Text = order.Item.ID;				//����
            //this.sheetView1.Cells[0, 1].Text = order.Item.Name;			//����

            if (string.IsNullOrEmpty(order.Item.Specs))
            {
                if (SOC.HISFC.BizProcess.Cache.Common.IsContainYKDept(this.Operator.Nurse.Clone().ID))
                {
                    this.sheetView1.Cells[0, 1].Text = order.Item.Name + "��" + SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).SpecialPrice + "Ԫ��";//����
                }
                else
                {
                    this.sheetView1.Cells[0, 1].Text = order.Item.Name + "��" + order.Item.Price + "Ԫ��";//����
                }
            }
            else
            {
                if (SOC.HISFC.BizProcess.Cache.Common.IsContainYKDept(this.Operator.Nurse.Clone().ID))
                {
                    this.sheetView1.Cells[0, 1].Text = order.Item.Name + "��" + order.Item.Specs + "��" + "��" + SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).SpecialPrice + "Ԫ��";//����
                }
                else
                {
                    this.sheetView1.Cells[0, 1].Text = order.Item.Name + "��" + order.Item.Specs + "��" + "��" + order.Item.Price + "Ԫ��";//����
                }
            }
            if (order.Item.Price == 0)
            {
                this.sheetView1.Cells[0, 3].Locked = false;				//�۸�
            }
            else
            {
                this.sheetView1.Cells[0, 3].Locked = true;					//�۸�
            }

            if (SOC.HISFC.BizProcess.Cache.Common.IsContainYKDept(this.Operator.Nurse.Clone().ID))
            {
                this.sheetView1.Cells[0, 3].Value = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).SpecialPrice;			//�۸�
            }
            else
            {
                this.sheetView1.Cells[0, 3].Value = order.Item.Price;			//�۸�
            }
            if (order.Item.Qty == 0)
                order.Item.Qty = 1;
            this.sheetView1.Cells[0, 4].Value = order.Item.Qty;			//����
            this.txtQty.Text = order.Item.Qty.ToString();
            this.sheetView1.Cells[0, 5].Text = order.Item.PriceUnit;		//��λ 
            order.Unit = order.Item.PriceUnit;
            this.txtUnit.Text = order.Item.PriceUnit;
            this.sheetView1.Cells[0, 6].Text = order.Frequency.ID;			//Ƶ��

            this.sheetView1.Cells[0, 7].Text = order.BeginTime.ToString();	//��ʼʱ��
            this.sheetView1.Cells[0, 8].Text = order.EndTime.ToString();	//����ʱ��
            this.sheetView1.Cells[0, 9].Text = order.Memo;				//��ע1
            //FS.HISFC.Models.Base.Department mydept = new FS.HISFC.Models.Base.Department();
            //mydept = this.deptManager.GetDepartment(order.ReciptDept.ID);
            //this.sheetView1.Cells[0, 10].Text = mydept.Name;		//ִ�п���
            this.sheetView1.Cells[0, 10].Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.ExeDept.ID);		//ִ�п���
            this.sheetView1.Cells[0, 11].Text = isNew ? "1" : "0";				//�Ƿ������ӵ� 0 �����ݿ������ԭ���� 1 �¸���
            this.sheetView1.Rows[0].Tag = order;
        }
        /// <summary>
        /// ���渽��
        /// </summary>
        public int SaveSubtbl()
        {
            //�統ǰ������Ϊ��ֹͣҽ����������ĸ���
            if (this.isDCOrder)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ֹͣҽ����������ĸ���"));
                return -1;
            }
            //���汾�������ӵĸ���
            try
            {
                if (this.addSubInfo == null)
                    this.addSubInfo = new ArrayList();
                else
                    this.addSubInfo.Clear();
                if (this.editSubInfo == null)
                    this.editSubInfo = new ArrayList();
                else
                    this.editSubInfo.Clear();
            }
            catch { }

            #region ��������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #endregion

            FS.HISFC.Models.Order.Inpatient.Order order;
            for (int i = 0; i < this.sheetView1.Rows.Count; i++)
            {
                if (this.sheetView1.Rows[i].Tag != null)
                {
                    try
                    {
                        #region ʵ�帳ֵ
                        order = this.sheetView1.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                        if (order == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("�����ı�������г��� ��������ת������");
                            return -1;
                        }
                        try
                        {
                            order.Qty = System.Convert.ToDecimal(sheetView1.Cells[i, 4].Value);	//����
                        }
                        catch
                        {
                            order.Qty = 1;
                        }
                        if (order.Qty == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show(order.Item.Name + "����Ӧ������!");
                            return -1;
                        }
                       
                        order.Unit = order.Item.PriceUnit;												//��λ
                        order.ExtendFlag1 = this.sheetView1.Cells[i, 9].Text;							//��ע
                        
                        #endregion

                        // �ж�ҽ��״̬ ������
                        FS.HISFC.Models.Order.Inpatient.Order od = CacheManager.InOrderMgr.QueryOneOrder(orderID);
                        if (od.Status == 3)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("ҽ��״̬�ѷ����仯����ֹͣҽ����������ĸ���");
                            return -1;
                        }
                        //��ʱҽ��
                        if (od.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                        {
                            if (od.Status != 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();;
                                MessageBox.Show("�������/ִ�е���ʱҽ����������и����޸ģ�");
                                return -1;
                            }
                        }
                        if (od.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)//��ҩƷ
                        {
                            //������ȡִ�п��ң������ն�ȷ�ϸ��ĵ�ȷ��������
                            //order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();
                        }
                        order.IsSubtbl = true; //���ı��

                        if (CacheManager.InOrderMgr.SetOrder(order) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show(CacheManager.InOrderMgr.Err);
                            return -1;
                        }

                        if (this.sheetView1.Cells[i, 11].Text == "1")			//������ӵĸ���
                        {
                            this.addSubInfo.Add(order);
                        }
                        else
                        {
                            this.editSubInfo.Add(order);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("�������" + ex.Message);
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            this.SetSubtblFlag("1", null);
            return 0;
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        public int DelSubtbl(object order)
        {
            FS.HISFC.Models.Order.Inpatient.Order myOrder = order as FS.HISFC.Models.Order.Inpatient.Order;
            if (myOrder == null)
            {
                MessageBox.Show("ɾ�����Ĺ����г��� ��������ת������");
                return -1;
            }
            if (myOrder.ID.Trim() != "")
            {
                if (myOrder.Status == 3)
                {
                    MessageBox.Show("��ֹͣҽ���޷����ĸ���");
                    return 0;
                }
                if (myOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                {
                    if (myOrder.Status != 0)
                    {
                        MessageBox.Show("�����/ִ�е�������������и����޸ģ�");
                        return 0;
                    }
                }
                if (MessageBox.Show("�Ƿ񳹵�ɾ������" + myOrder.Item.Name, "��ʾ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (myOrder.Status != 1 && myOrder.Status != 2)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                        //t.BeginTransaction();
                        CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //��δ���ҽ��ɾ������
                        if (CacheManager.InOrderMgr.DeleteOrder(myOrder) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("�޷�ɾ��!" + CacheManager.InOrderMgr.Err);
                            return -1;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    else
                    {//������ˡ���ִ��ҽ��ֹͣ����
                        if (this.DCSub(myOrder) == -1)
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// ����Ѿ���˻�ִ�У���ֹͣ����
        /// </summary>
        /// <returns></returns>
        public int DCSub(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(CacheManager.InOrderMgr.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            order.DCOper.ID = this.Operator.ID;
            order.DCOper.Name = this.Operator.Name;
            order.EndTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
            order.Status = 3;
            order.DcReason.Name = "��ʿվ��˲�ѯֹͣ";
            if (CacheManager.InOrderMgr.DcOneOrder(order) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show("ɾ��ҽ��ʧ��!" + CacheManager.InOrderMgr.Err);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;
        }
        /// <summary>
        /// �����Ƿ���Ҫ��ʾҽ�����ı�־
        /// </summary>
        /// <param name="operFlag">������־ "1" ������� "2" ɾ������</param>
        /// <param name="sender">ɾ��/ֹͣ�ĸ���ҽ��</param>
        private void SetSubtblFlag(string operFlag, object sender)
        {
            if (this.sheetView1.Rows.Count > 0)
            {
                if (this.ShowSubtblFlag != null)
                    this.ShowSubtblFlag(operFlag, true, sender);
            }
            else
            {
                if (this.ShowSubtblFlag != null)
                    this.ShowSubtblFlag(operFlag, false, sender);
            }
        }
        /// <summary>
        /// ���������ʾ
        /// </summary>
        public void Clear()
        {
            try
            {
                this.ucInputItem1.FeeItem = new FS.FrameWork.Models.NeuObject();
                this.txtQty.Text = "";
                this.txtUnit.Text = "";
              
                this.sheetView1.Rows.Count = 0;
                if (this.addSubInfo != null)
                    this.addSubInfo.Clear();
            }
            catch
            { }
        }

        /// <summary>
        /// �Ƿ�����շ�����
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool IsFee( FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (this.IsFeeWhenPriceZero == "1")
            {
                return true;
            }

            if (order.Item.Price > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region �¼�
        protected override void OnLoad(EventArgs e)
        {
            #region ��ʼ�� ���ظ�����Ŀ��Ϣ
            this.InitSubtbl();
            #endregion


            #region �¼�����
            this.Spread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
            this.Spread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);
            this.ucInputItem1.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.txtQty.Leave += new EventHandler(txtNum_Leave);
            this.btnSave.Click += new EventHandler(btnOK_Click);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);

            #endregion
        }
        
        void btnDelete_Click(object sender, EventArgs e)
        {
            int rowCount = this.sheetView1.RowCount;
            for (int i = rowCount - 1; i >= 0; i--)
            {
                if (this.sheetView1.IsSelected(i,0) ==true)
                {
                    if (this.sheetView1.Rows[i].Tag != null)
                    {
                        if (this.sheetView1.Cells[i, 11].Text == "0")				//�����ݿ��ڼ����ĸ���
                        {
                            if (this.DelSubtbl(this.sheetView1.Rows[i].Tag) != 1)
                            {
                                return;
                            }
                        }
                        object temp = this.sheetView1.Rows[i].Tag;
                        this.sheetView1.Rows.Remove(i, 1);
                        //���¸���ά����־
                        this.SetSubtblFlag("2", temp);
                    }
                }
            }
            
        }

        void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            ucItem1_SelectedItem();
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (!e.RowHeader && !e.ColumnHeader)
            {
                if (this.sheetView1.ActiveRow.Tag != null)
                {
                    if (this.sheetView1.Cells[e.Row, 11].Text == "0")				//�����ݿ��ڼ����ĸ���
                    {
                        if (this.DelSubtbl(this.sheetView1.ActiveRow.Tag) != 1)
                        {
                            return;
                        }
                    }
                    object temp = this.sheetView1.ActiveRow.Tag;
                    this.sheetView1.Rows.Remove(this.sheetView1.ActiveRowIndex, 1);
                    //���¸���ά����־
                    this.SetSubtblFlag("2", temp);
                }
            }
        }

        private void ucItem1_SelectedItem()
        {
            try
            {
                FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(this.orderID);
                if (this.orderID != "" && order == null)			//ҽ����ˮ�Ų�Ϊ����δ��ȡ��Чorderʵ��
                {
                    MessageBox.Show("��ȡҽ����Ϣ����" + CacheManager.InOrderMgr.Err);
                    return;
                }
                if (order == null)
                {
                    return;
                }
                //{A94C1C0F-EEC1-471e-9CCE-3ED8DE582AB8}  ҽ�����ʹ���
                if (!order.OrderType.IsCharge)
                {
                    if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        order.OrderType.ID = "CZ";
                        order.OrderType.Name = "����ҽ��";
                        order.OrderType.IsCharge = true;
                    }
                    else
                    {
                        order.OrderType.ID = "LZ";
                        order.OrderType.Name = "��ʱҽ��";
                        order.OrderType.IsCharge = true;
                    }
                }

                //�˴������޸Ŀ������Һ�ִ�п��ң���֤���ĺ�ҽ���Ŀ�����ִ�п���һ��
                //���ڼ���ĸ��ģ����⴦��ִ�п���Ϊ��������
                //�����жϵ���ԭҽ����Ŀ
                if (order.Item.SysClass.ID.ToString() == "UL")
                {
                    order.ExeDept.ID = order.ReciptDept.ID;
                    order.ExeDept.Name = order.ReciptDept.Name;
                }

                FS.HISFC.Models.Base.Item item = this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item;
                if (item == null)
                {
                    MessageBox.Show("��ȡ��ѡ�����Ŀ��Ϣ����!");
                    return;
                }
                if (item.PriceUnit == "[����]")		//����Ի�ʿά�����ײ͵Ŀ���
                {
                    //#region ��ӻ�ʿά�����ײ�
                    //FS.HISFC.BizLogic.Manager.ComGroupTail group = new FS.HISFC.BizLogic.Manager.ComGroupTail();
                    //ArrayList groupDetails = new ArrayList();
                    ////��������id��ȡ������ϸ
                    //groupDetails = group.GetComGroupTailByGroupID(item.ID);
                    //if (groupDetails == null || groupDetails.Count == 0) return;
                    //FS.HISFC.Models.Order.Order info;
                    //for (int i = 0; i < groupDetails.Count; i++)
                    //{
                    //    FS.HISFC.Models.Fee.ComGroupTail obj = groupDetails[i] as FS.HISFC.Models.Fee.ComGroupTail;
                    //    if (obj == null)
                    //    {
                    //        MessageBox.Show("�޷���Ӹ��� ��ȡ�ײ���ϸ����");
                    //        return;
                    //    }
                    //    if (obj.drugFlag == "1")//ҩƷ
                    //    {
                    //        info = order.Clone();
                    //        //����ҩƷid��ȡҩƷʵ��
                    //        FS.HISFC.Models.Pharmacy.Item drug = null;
                    //        FS.HISFC.BizLogic.Pharmacy.Item drugManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                    //        drug = drugManager.GetItemForInpatient(patientInfo.PVisit.PatientLocation.Dept.ID, obj.itemCode);
                    //        if (drug == null || drug.ID == "") continue;
                    //        FS.HISFC.Models.Base.Item drugBase = drug as FS.HISFC.Models.Base.Item;
                    //        drugBase.isPharmacy = true;
                    //        drugBase.Amount = obj.qty;
                    //        info.Item = drugBase;
                    //        info.ID = "";
                    //        this.AddSubtbl(info, true);
                    //    }
                    //    else//��ҩƷ
                    //    {
                    //        info = order.Clone();
                    //        //���ݷ�ҩƷid��ȡ��ҩƷʵ��
                    //        FS.HISFC.Models.Fee.Item undrug = null;
                    //        FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
                    //        undrug = undrugManager.GetItem(obj.itemCode);
                    //        if (undrug == null) continue;
                    //        //��ӻ�����Ŀ
                    //        FS.HISFC.Models.Base.Item undrugBase = undrug as FS.HISFC.Models.Base.Item;
                    //        undrugBase.isPharmacy = false;
                    //        undrugBase.Amount = obj.qty;//����
                    //        info.Item = undrugBase;
                    //        info.ID = "";
                    //        this.AddSubtbl(info, true);
                    //    }
                    //}
                    //#endregion

                    return;
                }
                else
                {
                    order.Item = item.Clone();
                    order.ID = "";										//ҽ����ˮ��
                    this.AddSubtbl(order, true);
                }
            }
            catch
            {
                MessageBox.Show("���ҽ������" + CacheManager.InOrderMgr.Err);
            }
            this.txtQty.Focus();
        }


        private void txtNum_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.sheetView1.Rows.Count > 0)
                    this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, 4].Value = this.txtQty.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, 4].Value = 1;				//����
            }
        }

      

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SaveSubtbl();
        }

      

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = this.sheetView1.Rows[this.sheetView1.ActiveRowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;
            if (order == null)
            {
                MessageBox.Show("��ȡҽ��ʵ����Ϣʱ��������ת������");
                return;
            }
            try
            {
                this.ucInputItem1.FeeItem = order.Item;
                this.txtQty.Text = this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, 4].Text;
                this.txtUnit.Text = order.Unit;
         }
            catch
            { }
        }

      
        #endregion

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                ucInputItem1.txtItemCode.Focus();
            }
        }

        private void txtQty_Enter(object sender, EventArgs e)
        {
            this.txtQty.Select(0, this.txtQty.Value.ToString().Length);
        }
    }
}
