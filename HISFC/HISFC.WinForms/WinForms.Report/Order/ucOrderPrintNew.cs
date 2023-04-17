using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Report.Order
{
    /// <summary>
    /// ҽ������ӡ��������
    /// </summary>
    public partial class ucOrderPrintNew : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.IPrintOrder
    {
        /// <summary>
        /// 
        /// </summary>
        public ucOrderPrintNew()
        {
            InitializeComponent();
        }

        //��ʼ������ҽ������ʱҽ�� �б�
        ArrayList alLong = new ArrayList();
        ArrayList alShort = new ArrayList();
        //�洢û�д�ӡ��ҽ���б�
        ArrayList alLong1 = new ArrayList();
        ArrayList alShort1 = new ArrayList();
        //�洢����ҽ������ʱҽ��Ŀǰ��ӡ����ҳ����������ʾ�û�
        int longordercurrentpage = 1;
        int shortordercurrentpage = 1;

        #region ����

        ////ҽ��ҵ��
        //private FS.HISFC.BizLogic.Order.Order ordManager = new FS.HISFC.BizLogic.Order.Order();

        //������Ϣ
        private FS.HISFC.Models.RADT.PatientInfo pInfo = new FS.HISFC.Models.RADT.PatientInfo();

        //��Ա��Ϣ
        private FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();

        /// ���Ұ�����
        private FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();

        //����ҽ����ӡҵ����
        private FS.WinForms.Report.Order.Function myfun = new FS.WinForms.Report.Order.Function();

        //ҽ������ӡ
        private FS.HISFC.BizLogic.Order.OrderBill orderBillMgr = new FS.HISFC.BizLogic.Order.OrderBill();

        //ÿҳ�������
        private const int orderline = 27;

        #endregion

        #region IPrintOrder ��Ա
        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            //MessageBox.Show("����ҽ��������" + this.longordercurrentpage.ToString() + "ҳ\n\t" + "��ʱҽ��������"+this.shortordercurrentpage.ToString() + "ҳ");
            #region ������´�ӡ��� 
            if (this.checkBox1.Checked)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (this.tabControl1.SelectedIndex == 0)//����ҽ��
                {
                    foreach (object obj in alLong1)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                        //User03 �Ƿ��ӡ�� 
                        if (order.User03 != "1")
                        {
                            if (this.myfun.UpdateOrderStatus(order.ID, "1") == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(myfun.Err);
                                return;
                            }
                        }
                        if (order.Status == 3 && order.EndTime.ToString() != System.DateTime.MinValue.ToString())
                        {
                            if (this.orderBillMgr.UpdatePrinStopFlag(order.ID) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(orderBillMgr.Err);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    foreach (object obj in alShort1)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                        if (order.User03 != "1")
                        {
                            if (this.myfun.UpdateOrderStatus(order.ID, "1") == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(myfun.Err);
                                return;
                            }
                        }
                        if (order.Status == 3 && order.EndTime.ToString() != System.DateTime.MinValue.ToString())
                        {
                            if (this.orderBillMgr.UpdatePrinStopFlag(order.ID) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(orderBillMgr.Err);
                                return;
                            }
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            else //��������
            {
                if (this.tabControl1.SelectedIndex == 0)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    foreach (object obj in alLong)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                        if (order.User03 != "1")
                        {
                            if (this.myfun.UpdateOrderStatus(order.ID, "1") == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(myfun.Err);
                                return;
                            }
                        }
                        if (order.Status == 3 && order.EndTime.ToString() != System.DateTime.MinValue.ToString())
                        {
                            if (this.orderBillMgr.UpdatePrinStopFlag(order.ID) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(orderBillMgr.Err);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    foreach (object obj in alShort)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                        if (order.User03 != "1")
                        {
                            if (this.myfun.UpdateOrderStatus(order.ID, "1") == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(myfun.Err);
                                return;
                            }
                        }
                        if (order.Status == 3 && order.EndTime.ToString() != System.DateTime.MinValue.ToString())
                        {
                            if (this.orderBillMgr.UpdatePrinStopFlag(order.ID) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(orderBillMgr.Err);
                                return;
                            }
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            #endregion
            #region ����ҽ��
            if (tabControl1.SelectedIndex == 0)
            {
                if (this.checkBox1.Checked)
                {
                    if (this.alLong1.Count > 0)
                    {
                        MessageBox.Show("����ҽ������" + this.longordercurrentpage.ToString() + "ҳ");
                    }
                    else
                    {
                        MessageBox.Show("û��Ҫ��ӡ������ҽ��");
                        return;
                    }
                }
                FS.WinForms.Report.Common.frmPreviewDataWindow frm = new FS.WinForms.Report.Common.frmPreviewDataWindow();
                frm.PreviewDataWindow = this.dwLongOrder;
                frm.Show();
            }
            #endregion
            #region ��ʱҽ��
            else
            {
                if (this.checkBox1.Checked)
                {
                    if (this.alShort1.Count > 0)
                    {
                        MessageBox.Show("��ʱҽ������" + this.shortordercurrentpage.ToString() + "ҳ");
                    }
                    else
                    {
                        MessageBox.Show("û��Ҫ��ӡ������ҽ��");
                        return;
                    }
                }
                FS.WinForms.Report.Common.frmPreviewDataWindow frm = new FS.WinForms.Report.Common.frmPreviewDataWindow();
                frm.PreviewDataWindow = this.dwShortOrder;
                frm.Show();
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientInfo"></param>
        public void SetPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            #region ��ʼ�� ���ݴ���
            this.dwLongOrder.LibraryList =@"Report\met_ord.pbl;Report\met_ord.pbd";
            this.dwShortOrder.LibraryList = @"Report\met_ord.pbl;Report\met_ord.pbd";
            this.dwLongOrder.DataWindowObject = "d_longorder_print";
            this.dwShortOrder.DataWindowObject = "d_shortorder_print";
            #endregion
            #region ��ʼ��ȫ��ҽ��
            pInfo = patientInfo;
            
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ʾҽ����Ϣ,���Ժ�......");
            Application.DoEvents();
            ArrayList alAll = new ArrayList();
            try
            {
                alAll = this.myfun.QueryDcOrder(patientInfo.ID);
            }
            catch
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            alLong.Clear();
            alShort.Clear();
            
            if (alAll != null)
            {
                foreach (object obj in alAll)
                {
                    FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                    if (order.Status == 0) continue;
                    //ֹͣ��־�Ƿ��ӡ��
                    FS.HISFC.Models.Order.OrderBill orderBill = null;
                    orderBill = this.orderBillMgr.GetOrderBillByOrderID(order.ID);
                    if (orderBill != null)
                    {
                        order.User01 = orderBill.PrintDCFlag;
                        //order.User03 = orderBill.PrintFlag;
                    }
                    else
                    {
                        order.User01 = "0";
                        //order.User03 = "0";
                    }
                    if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        //����ҽ��
                        alLong.Add(obj);
                    }
                    else
                    {
                        //��ʱҽ��
                        alShort.Add(obj);
                    }
                }
            }
            #endregion
            #region �ֳ��ں���ʱд�����ݴ���
            if (this.tabControl1.SelectedIndex == 0)
                AddDataToLongOrder(alLong,"0");
            else
                AddDataToShortOrder(alShort,"0");
           
           
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            #endregion
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="patientInfo"></param>
        public void SetPatientOn(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            #region ��ʼ�����ݴ���
            //this.dwLongOrder.LibraryList = @"Report\met_ord.pbl;Report\met_ord.pbd";
            //this.dwShortOrder.LibraryList = @"Report\met_ord.pbl;Report\met_ord.pbd";
            //this.dwLongOrder.DataWindowObject = "d_longorderon_print";
            //this.dwShortOrder.DataWindowObject = "d_shortorderon_print";
            pInfo = patientInfo;
            #endregion

            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ʾ����ҽ����Ϣ,���Ժ�......");
            #region ��ѯ��ӡ��û�д�ӡ��ҽ��
            Application.DoEvents();
            //�洢��ӡ����ҽ��
            ArrayList alPrinted = new ArrayList();
            //�洢û�д�ӡ��ҽ��
            ArrayList alNoPrint = new ArrayList();
            ArrayList alAll = new ArrayList();

            try
            {
                //�Ѿ���ӡ��ҽ��
                alPrinted = this.myfun.QueryDcOrder(patientInfo.ID,"1");
                //û�д�ӡ��ҽ��
                alNoPrint = this.myfun.QueryDcOrder(patientInfo.ID,"0");
            }
            catch
            {
                //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            //�洢û�д�ӡ���ĳ��ں���ʱҽ��
            alLong1.Clear();
            alShort1.Clear();
            alLong.Clear();
            alShort.Clear();

            #endregion

            #region ��ӡ����ҽ�����ֳ���ҽ������ʱҽ��
            if (alPrinted != null)
            {
                FS.HISFC.Models.Order.Inpatient.Order order = null;
                FS.HISFC.Models.Order.OrderBill orderbill = null;
                foreach (object obj in alPrinted)
                {
                    
                    order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                    if (order.Status == 0) continue;
                    orderbill = this.orderBillMgr.GetOrderBillByOrderID(order.ID);
                    if (orderbill != null)
                    {
                        order.User01 = orderbill.PrintDCFlag;
                        order.User03 = orderbill.PrintFlag;
                    }
                    else
                    {
                        order.User01 = "0";
                        order.User03 = "0";
                    }
                    if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        //����ҽ��
                        alLong.Add(order);
                    }
                    else
                    {
                        //��ʱҽ��
                        alShort.Add(order);
                    }
                }
            }
            #endregion

            #region û�д�ӡ����ҽ�����ֳ���ҽ������ʱҽ��
            if (alNoPrint != null)
            {
                FS.HISFC.Models.Order.Inpatient.Order order = null;
                FS.HISFC.Models.Order.OrderBill orderbill = null;
                foreach (object obj in alNoPrint)
                {
                    order = obj as FS.HISFC.Models.Order.Inpatient.Order;
                    orderbill = this.orderBillMgr.GetOrderBillByOrderID(order.ID);
                    if (orderbill != null)
                    {
                        order.User01 = orderbill.PrintDCFlag;
                    }
                    else
                    {
                        order.User01 = "0";
                    }
                    //order.User03 = orderbill.PrintFlag;
                    if (order.Status == 0) continue;
                    if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        //����ҽ��
                        alLong1.Add(order);
                    }
                    else
                    {
                        //��ʱҽ��
                        alShort1.Add(order);
                    }
                }
            }
            #endregion

            #region ����ʼ
            //�ϴδ�ӡ�������� 25���� ������ȡ����
            int longleave = 0;
            int shortleave = 0;

            //ȡ��
            longleave = alLong.Count % orderline;
            shortleave = alShort.Count % orderline;

            //����Ӧ�ô�ӡ����ҳ��
            this.longordercurrentpage = alLong.Count / orderline + 1;
            this.shortordercurrentpage = alShort.Count / orderline + 1;

            ArrayList alLongTemp = new ArrayList();
            ArrayList alShortTemp = new ArrayList();
            if(longleave >= 0)
            {
                alLongTemp = alLong.GetRange( alLong.Count - longleave, longleave);
            }
            if(shortleave >= 0)
            {
                alShortTemp = alShort.GetRange(alShort.Count - shortleave, shortleave);
            }
            alLongTemp.AddRange(alLong1);
            alShortTemp.AddRange(alShort1);
            #endregion

            #region ��ʾ��ӡ
            if (this.tabControl1.SelectedIndex == 0)
                AddDataToLongOrder(alLongTemp,"1");
            else
                AddDataToShortOrder(alShortTemp,"1");
            #endregion

            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ShowPrintSet()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region ����

        /// <summary>
        /// ���� ����
        /// </summary>
        /// <param name="al"></param>
        /// <param name="sgoon"></param>
        private void AddDataToLongOrder(ArrayList al,string sgoon)
        {
            this.dwLongOrder.Reset();
          
            #region д�������
            this.dwLongOrder.Modify("t_3.text ='" + FS.HISFC.BizProcess.Integrate.Function.GetHosName() + "'");
            this.dwLongOrder.Modify("t_name.text ='" + pInfo.Name + "'");
            this.dwLongOrder.Modify("t_sex.text ='" + pInfo.Sex.Name + "'");
            this.dwLongOrder.Modify("t_age.text ='" + pInfo.Age + "'");
            this.dwLongOrder.Modify("t_inpatientno.text ='" + pInfo.ID + "'");
            this.dwLongOrder.Modify("t_bed.text ='" + pInfo.PVisit.PatientLocation.Bed.ToString() + "'");
            this.dwLongOrder.Modify("t_dept.text ='" + pInfo.PVisit.PatientLocation.Dept.Name + "'");
            #endregion

            #region ��������
            FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
            FS.HISFC.Models.Pharmacy.Item item = null;
            ArrayList all = new ArrayList();//�洢������Ҫ�޸ĵ���
            ArrayList combLocation = new ArrayList();//��Ҫ�洢������
            ArrayList tempComb = new ArrayList();//��Ҫ�洢������
            string tempID = (al.Count > 0) ? ((FS.HISFC.Models.Order.Inpatient.Order)al[0]).Combo.ID : "";
            string combID = "";
            int count = 1;//�ж��Ƿ����飬�Ƿ���Ҫ�洢
            int alcount = 0;//һ���ж�����ҽ��
            int times = al.Count;//�ܹ�Ҫѭ����������ʼ��ҽ������һ���������������Ƶ�κ����ӣ�
            string frequency = "";//��ʱ�洢���Ƶ��
            string name = "";//�洢����ҩ������
            FS.HISFC.Models.Order.Inpatient.Order tempobj = new FS.HISFC.Models.Order.Inpatient.Order();//��������ĵ�һ��ҩƷ
            #endregion

            #region ѭ��дҽ��
            for (int i = 0; i < times; i++)
            {
                order = al[alcount] as FS.HISFC.Models.Order.Inpatient.Order;
                this.dwLongOrder.InsertRow();
                int rows = this.dwLongOrder.RowCount;

                combID = order.Combo.ID;
                if (i > 0)
                {
                #region ����ϲ�ͬ����count����1ʱ˵������һ��Ҫ��ʾ���Ƶ��
                    if (tempID != combID && count > 1)
                    {
                        //ȡ��һ��ҽ��
                        FS.HISFC.Models.Order.Inpatient.Order tempOrder = al[alcount - 1] as FS.HISFC.Models.Order.Inpatient.Order;
                        //��ʼ����
                        this.dwLongOrder.SetItemDateTime(rows, 1, tempOrder.MOTime);
                        //��ʼʱ��
                        this.dwLongOrder.SetItemDateTime(rows, 2, tempOrder.MOTime);
                        this.dwLongOrder.SetItemString(rows, 14, tempOrder.User03);
                        this.dwLongOrder.SetItemDecimal(rows, 15, longordercurrentpage);
                        this.dwLongOrder.SetItemString(rows, 16, sgoon);
                        this.dwLongOrder.SetItemString(rows, 17, tempOrder.User01);
                        this.dwLongOrder.SetItemString(rows, 3, frequency);
                        this.dwLongOrder.SetItemString(rows, 13, "dfdf");//��ֹ��Ϻ���ͬ
                        
                        //ҽ����
                        this.dwLongOrder.SetItemString(rows, 5, tempOrder.ReciptDoctor.Name);
                        //������
                        this.dwLongOrder.SetItemString(rows, 6, objHelper.GetName(tempOrder.Nurse.ID));
                        //����ʱ��
                        if (tempOrder.ConfirmTime != DateTime.MinValue)
                        {
                            this.dwLongOrder.SetItemDateTime(rows, 7, tempOrder.ConfirmTime);
                        }
                        //���ҽ���Ѿ�ֹͣ
                        if (tempOrder.EndTime != DateTime.MinValue)
                        {
                            //ֹͣ����
                            this.dwLongOrder.SetItemDateTime(rows, 8, tempOrder.EndTime);
                            //ֹͣʱ��
                            this.dwLongOrder.SetItemDateTime(rows, 9, tempOrder.EndTime);
                            //ֹͣҽ��
                            this.dwLongOrder.SetItemString(rows, 10, tempOrder.DCOper.Name);
                            //ֹͣ��ʿ�������ߣ�
                            this.dwLongOrder.SetItemString(rows, 11, objHelper.GetName(tempOrder.ExecOper.ID));
                            //�����ߴ���ʱ��
                            if (order.ExecOper.OperTime != DateTime.MinValue)
                            {
                                this.dwLongOrder.SetItemDateTime(rows, 12, tempOrder.ExecOper.OperTime);
                            }
                        }

                        times++;//������һ�У���Ҫ����һ��ѭ������
                        count = 1;//
                        tempID = "fuck";//��ֹ��Ϻ���ͬ
                        tempComb = combLocation.Clone() as ArrayList;
                        all.Add(tempComb);//���������Ҫ�ĵ�ҩƷ��Ϣ
                        combLocation.Clear();//������ݹ���һ��ʹ��
                        continue;
                    }
                    //if (tempID == combID)
                    //{
                    //    FS.HISFC.Models.Order.Inpatient.Order obj = new FS.HISFC.Models.Order.Inpatient.Order();
                    //    obj.ID = rows.ToString();
                    //    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    //    {
                    //        item = order.Item as FS.HISFC.Models.Pharmacy.Item;
                    //        obj.Name = order.Item.Name + "  " + order.DoseOnce + item.DoseUnit;
                    //    }
                    //    if (combLocation.Count == 0)
                    //    {
                    //        combLocation.Add(obj);
                    //        FS.HISFC.Models.Order.Inpatient.Order o = tempobj.Clone();
                    //        combLocation.Add(o);
                    //    }
                    //    else
                    //    {
                    //        combLocation.Add(obj);
                    //    }
                    //    count++;
                    //}
                }
                #endregion

                //��ʼ����
                this.dwLongOrder.SetItemDateTime(rows, 1, order.MOTime);
                //��ʼʱ��
                this.dwLongOrder.SetItemDateTime(rows, 2, order.MOTime);
                #region ҩƷƵ��
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    item = order.Item as FS.HISFC.Models.Pharmacy.Item;
                    //ҩƷҽ��
                    this.dwLongOrder.SetItemString(rows, 3, order.Item.Name + "  " + order.DoseOnce + item.DoseUnit + " " + order.Usage.Name.ToLower() + " " + order.Frequency.ID.ToLower() + " " + order.Memo);
                    frequency = "                      " + order.Usage.Name.ToLower() + " " + ((order.Frequency.ID.ToUpper() == "WPC") ? null : order.Frequency.ID.ToLower());
                    name = order.Item.Name + "  " + order.DoseOnce + item.DoseUnit ;
                }
                #endregion

                #region ��ҩƷƵ��
                else
                {
                    //��ҩƷҽ��
                    if (order.Frequency.ID == "WPC")
                    {
                        this.dwLongOrder.SetItemString(rows, 3, order.Item.Name + "  " + order.Usage.Name.ToLower() + order.Memo);
                    }
                    else
                    {
                        this.dwLongOrder.SetItemString(rows, 3, order.Item.Name + "  " + order.Usage.Name.ToLower() + "  " + order.Frequency.ID.ToLower() + " " + order.Memo);
                    }
                }
                #endregion
                //���
                this.dwLongOrder.SetItemString(rows, 13, order.Combo.ID);
                //ҽ����
                this.dwLongOrder.SetItemString(rows, 5, order.ReciptDoctor.Name);
                //������
                this.dwLongOrder.SetItemString(rows, 6, objHelper.GetName(order.Nurse.ID));
                //����ʱ��
                if (order.ConfirmTime != DateTime.MinValue)
                {
                    this.dwLongOrder.SetItemDateTime(rows, 7, order.ConfirmTime);
                }
                #region ֹͣҽ��
                //���ҽ���Ѿ�ֹͣ
                if (order.EndTime != DateTime.MinValue)
                {
                    //ֹͣ����
                    this.dwLongOrder.SetItemDateTime(rows, 8, order.EndTime);
                    //ֹͣʱ��
                    this.dwLongOrder.SetItemDateTime(rows, 9, order.EndTime);
                    //ֹͣҽ��
                    this.dwLongOrder.SetItemString(rows, 10, order.DCOper.Name);
                    //ֹͣ��ʿ�������ߣ�
                    this.dwLongOrder.SetItemString(rows, 11, objHelper.GetName(order.ExecOper.ID));
                    //�����ߴ���ʱ��
                    if (order.ExecOper.OperTime != DateTime.MinValue)
                    {
                        this.dwLongOrder.SetItemDateTime(rows, 12, order.ExecOper.OperTime);
                    }
                }
                #endregion
                this.dwLongOrder.SetItemString(rows, 14, order.User03);//�Ƿ��ӡ��
                this.dwLongOrder.SetItemDecimal(rows, 15, longordercurrentpage);
                this.dwLongOrder.SetItemString(rows, 16, sgoon);
                this.dwLongOrder.SetItemString(rows, 17, order.User01);//�Ƿ��ӡ��ֹͣ
                tempobj.ID = rows.ToString();
                tempobj.Name = name;
                tempID = combID;
                alcount++;
            }
            #endregion
            
            /*

            #region  ������һ��������Ҫ�ٵ�������Ƶ����
            //������һ��������Ҫ�ٵ�������Ƶ����
            if (tempID == combID && count > 1)
            {
                this.dwLongOrder.InsertRow();
                //��ʼ����
                this.dwLongOrder.SetItemDateTime(this.dwLongOrder.RowCount, 1, order.MOTime);
                //��ʼʱ��
                this.dwLongOrder.SetItemDateTime(this.dwLongOrder.RowCount, 2, order.MOTime);
                this.dwLongOrder.SetItemString(this.dwLongOrder.RowCount, 14, order.User03);
                this.dwLongOrder.SetItemString(this.dwLongOrder.RowCount, 3, frequency);
                this.dwLongOrder.SetItemString(this.dwLongOrder.RowCount, 13, "dfd");
                this.dwLongOrder.SetItemString(this.dwLongOrder.RowCount, 14, order.User03);
                this.dwLongOrder.SetItemDecimal(this.dwLongOrder.RowCount, 15, longordercurrentpage);
                //�Ƿ�������
                this.dwLongOrder.SetItemString(this.dwLongOrder.RowCount, 16, sgoon);
                //�Ƿ��ӡ��ֹͣʱ��
                this.dwLongOrder.SetItemString(this.dwLongOrder.RowCount,17, order.User01);

                //ҽ����
                this.dwLongOrder.SetItemString(this.dwLongOrder.RowCount, 5, order.ReciptDoctor.Name);
                //������
                this.dwLongOrder.SetItemString(this.dwLongOrder.RowCount, 6, objHelper.GetName(order.Nurse.ID));
                //����ʱ��
                if (order.ConfirmTime != DateTime.MinValue)
                {
                    this.dwLongOrder.SetItemDateTime(this.dwLongOrder.RowCount, 7, order.ConfirmTime);
                }
                //���ҽ���Ѿ�ֹͣ
                if (order.EndTime != DateTime.MinValue)
                {
                    //ֹͣ����
                    this.dwLongOrder.SetItemDateTime(this.dwLongOrder.RowCount, 8, order.EndTime);
                    //ֹͣʱ��
                    this.dwLongOrder.SetItemDateTime(this.dwLongOrder.RowCount, 9, order.EndTime);
                    //ֹͣҽ��
                    this.dwLongOrder.SetItemString(this.dwLongOrder.RowCount, 10, order.DCOper.Name);
                    //ֹͣ��ʿ�������ߣ�
                    this.dwLongOrder.SetItemString(this.dwLongOrder.RowCount, 11, objHelper.GetName(order.ExecOper.ID));
                    //�����ߴ���ʱ��
                    if (order.ExecOper.OperTime != DateTime.MinValue)
                    {
                        this.dwLongOrder.SetItemDateTime(this.dwLongOrder.RowCount, 12, order.ExecOper.OperTime);
                    }
                }
            }
            for (int i = this.dwLongOrder.RowCount; i >= 2; i--)
            {
                if (!dwLongOrder.IsItemNull(i, 13) && !dwLongOrder.IsItemNull(i - 1, 13) && !dwLongOrder.IsItemNull(i - 1, 10))
                    if (this.dwLongOrder.GetItemString(i, 13) == this.dwLongOrder.GetItemString(i - 1, 13))
                    {
                        this.dwLongOrder.SetItemString(i - 1, 10, dwLongOrder.GetItemString(i, 10));
                    }
            }
            #endregion

            #region ѭ�������������Ҫ�޸ĵ���
            if (combLocation.Count != 0)
                        {
                            tempComb = combLocation.Clone() as ArrayList;
                            all.Add(tempComb);
                            combLocation.Clear();
                        }
                        //�޸�������Ҫ�޸ĵ���
                        for (int j = 0; j < all.Count; j++)
                        {
                            ArrayList yunal = all[j] as ArrayList;
                            for (int k = 0; k < yunal.Count; k++)
                            {
                                FS.HISFC.Models.Order.Inpatient.Order changeobj = yunal[k] as FS.HISFC.Models.Order.Inpatient.Order;
                                this.dwLongOrder.SetItemString(Convert.ToInt16(changeobj.ID), 3, changeobj.Name);
                                //this.dwLongOrder.SetItemString(Convert.ToInt16(changeobj.ID), 17, changeobj.User01);
                            }
                        }

            FS.WinForms.Report.Order.Function.DrawCombo(dwLongOrder, 13, 4);
            //FS.HISFC.Components.Common.Classes.Function.DrawCombo(dwLongOrder, 13, 4);
            #endregion

            */
            FS.WinForms.Report.Order.Function.DrawCombo(dwLongOrder, 13, 4);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="al"></param>
        /// <param name="sgoon"></param>
        private void AddDataToShortOrder(ArrayList al,string sgoon)
        {
            this.dwShortOrder.Reset();
            #region д�������
            this.dwShortOrder.Modify("t_3.text ='" + FS.HISFC.BizProcess.Integrate.Function.GetHosName() + "'");
            this.dwShortOrder.Modify("t_name.text ='" + pInfo.Name + "'");
            this.dwShortOrder.Modify("t_sex.text ='" + pInfo.Sex.Name + "'");
            this.dwShortOrder.Modify("t_age.text ='" + pInfo.Age + "'");
            this.dwShortOrder.Modify("t_inpatientno.text ='" + pInfo.ID + "'");
            this.dwShortOrder.Modify("t_bed.text ='" + pInfo.PVisit.PatientLocation.Bed.ToString() + "'");
            this.dwShortOrder.Modify("t_dept.text ='" + pInfo.PVisit.PatientLocation.Dept.Name + "'");
            #endregion

            #region  ��������
            FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
            FS.HISFC.Models.Pharmacy.Item item = null;
            ArrayList all = new ArrayList();//�洢������Ҫ�޸ĵ���
            ArrayList combLocation = new ArrayList();//��Ҫ�洢������
            ArrayList tempComb = new ArrayList();//��Ҫ�洢������
            string tempID = (al.Count > 0) ? ((FS.HISFC.Models.Order.Inpatient.Order)al[0]).Combo.ID : "";
            string combID = "";
            int count = 1;//�ж��Ƿ����飬�Ƿ���Ҫ�洢
            int alcount = 0;//һ���ж�����ҽ��
            int times = al.Count;//�ܹ�Ҫѭ����������ʼ��ҽ������һ���������������Ƶ�κ����ӣ�
            string frequency = "";//��ʱ�洢���Ƶ��
            string name = "";//�洢����ҩ������
            FS.HISFC.Models.Order.Inpatient.Order tempobj = new FS.HISFC.Models.Order.Inpatient.Order();//��������ĵ�һ��ҩƷ
            #endregion

            #region ѭ��ҽ��
            for (int i = 0; i < times; i++)
            {
                this.dwShortOrder.InsertRow();
                int rows = this.dwShortOrder.RowCount;
                order = al[alcount] as FS.HISFC.Models.Order.Inpatient.Order;

                combID = order.Combo.ID;
                if (i > 0)
                {
                    //����ϲ�ͬ����count����1ʱ˵������һ��Ҫ��ʾ���Ƶ��
                    if (tempID != combID && count > 1)
                    {
                        //ȡ��һ��ҽ������������������
                        FS.HISFC.Models.Order.Inpatient.Order tempOrder = al[alcount - 1] as FS.HISFC.Models.Order.Inpatient.Order;
                        //��ʼ����
                        this.dwShortOrder.SetItemDateTime(rows, 1, tempOrder.BeginTime);
                        //��ʼʱ��
                        this.dwShortOrder.SetItemDateTime(rows, 2, tempOrder.BeginTime);

                        this.dwShortOrder.SetItemString(rows, 3, frequency);
                        this.dwShortOrder.SetItemString(rows, 10, tempOrder.User03);
                        this.dwShortOrder.SetItemDecimal(rows, 11, this.shortordercurrentpage);
                        this.dwShortOrder.SetItemString(rows, 12, sgoon);
                        this.dwShortOrder.SetItemString(rows, 13, tempOrder.User01);
                        //ҽ����
                        this.dwShortOrder.SetItemString(rows, 5, tempOrder.ReciptDoctor.Name);
                        //������
                        this.dwShortOrder.SetItemString(rows, 6, objHelper.GetName(tempOrder.Nurse.ID));
                        //��������
                        if (order.ConfirmTime != DateTime.MinValue)
                        {
                            this.dwShortOrder.SetItemDateTime(rows, 7, tempOrder.ConfirmTime);
                        }
                        //����ʱ��
                        if (order.ConfirmTime != DateTime.MinValue)
                        {
                            this.dwShortOrder.SetItemDateTime(rows, 8, tempOrder.ConfirmTime);
                        }

                        this.dwShortOrder.SetItemString(rows, 9, "dfdf");//��ֹ��Ϻ���ͬ

                        times++;//������һ�У���Ҫ����һ��ѭ������
                        count = 1;//
                        tempID = "fuck";//��ֹ��Ϻ���ͬ
                        tempComb = combLocation.Clone() as ArrayList;
                        all.Add(tempComb);//���������Ҫ�ĵ�ҩƷ��Ϣ
                        combLocation.Clear();//������ݹ���һ��ʹ��
                        continue;
                    }
                    //if (tempID == combID)
                    //{
                    //    FS.HISFC.Models.Order.Inpatient.Order obj = new FS.HISFC.Models.Order.Inpatient.Order();
                    //    obj.ID = rows.ToString();
                    //    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    //    {
                    //        item = order.Item as FS.HISFC.Models.Pharmacy.Item;
                    //        obj.Name = order.Item.Name + "  " + order.DoseOnce + item.DoseUnit;
                    //    }
                    //    if (combLocation.Count == 0)
                    //    {
                    //        combLocation.Add(obj);
                    //        FS.HISFC.Models.Order.Inpatient.Order o = tempobj.Clone();
                    //        combLocation.Add(o);
                    //    }
                    //    else
                    //    {
                    //        combLocation.Add(obj);
                    //    }
                    //    count++;
                    //}
                }

                //��ʼ����
                this.dwShortOrder.SetItemDateTime(rows, 1, order.BeginTime);
                //��ʼʱ��
                this.dwShortOrder.SetItemDateTime(rows, 2, order.BeginTime);

                #region ҩƷƵ�δ���
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    item = order.Item as FS.HISFC.Models.Pharmacy.Item;
                    //ҩƷҽ��
                    if (order.Memo == "����" || order.Memo == "������" || order.Memo == "�������")
                    {//ҽ����ע����ǡ����á�������֮һ����ֻ��ʾ���*����
                        this.dwShortOrder.SetItemString(rows, 3, order.Item.Name + "  " + order.Item.Specs + "*" + order.Qty + order.Unit + " " + order.Memo);
                    }
                    else if (order.Frequency.ID.ToUpper() != "ST" && order.Frequency.ID.ToUpper() != "SOS")
                    {//��ʱҩ������ST��SOSƵ�Σ�����Ƶ�ζ�����ӡ
                        this.dwShortOrder.SetItemString(rows, 3, order.Item.Name + "  " + order.DoseOnce + item.DoseUnit + " " + order.Usage.Name.ToLower());
                        frequency = "                                         " + order.Usage.Name.ToLower() + " " + order.Memo;
                    }
                    else
                    {
                        this.dwShortOrder.SetItemString(rows, 3, order.Item.Name + "  " + order.DoseOnce + item.DoseUnit + " " + order.Usage.Name.ToLower() + " " + order.Frequency.ID.ToLower());
                        frequency = "                                         " + order.Usage.Name.ToLower() + " " + ((order.Frequency.ID.ToUpper() == "WPC") ? null : order.Frequency.ID.ToLower()) + " " + order.Memo;
                    }
                    name = order.Item.Name + "  " + order.DoseOnce + item.DoseUnit;
                }
                #endregion

                #region ��ҩƷƵ�δ���
                else
                {//��ҩƷҽ��
                    if (order.Memo == "����" || order.Memo == "������" || order.Memo == "�������")
                    {//ҽ����ע����ǡ����á�������֮һ����ֻ��ʾ���*����
                        this.dwShortOrder.SetItemString(rows, 3, order.Item.Name + "  " + order.Item.Specs + "*" + order.Qty + " " + order.Memo);
                    }
                    else if(order.Frequency.ID != "ST" && order.Frequency.ID != "SOS")
                    {//��ҩƷ��������Ƶ�λ���ʲô��ֻҪ����ST����SOS�Ͳ���ʾƵ��
                        this.dwShortOrder.SetItemString(rows, 3, order.Item.Name + "  " + order.Usage.Name.ToLower() + " " + order.Memo);
                    }
                    else
                    {
                        this.dwShortOrder.SetItemString(rows, 3, order.Item.Name + "  " + order.Usage.Name.ToLower() + "  " + order.Frequency.ID.ToLower() + " "  +order.Memo);
                    }
                }
                #endregion

                this.dwShortOrder.SetItemString(rows, 10, order.User03);
                this.dwShortOrder.SetItemDecimal(rows, 11, this.shortordercurrentpage);
                this.dwShortOrder.SetItemString(rows, 12, sgoon);
                this.dwShortOrder.SetItemString(rows, 13, order.User01);
                //���
                this.dwShortOrder.SetItemString(rows, 9, order.Combo.ID);
                //ҽ����
                this.dwShortOrder.SetItemString(rows, 5, order.ReciptDoctor.Name);
                //������
                this.dwShortOrder.SetItemString(rows, 6, objHelper.GetName(order.Nurse.ID));

                //��������
                if (order.ConfirmTime != DateTime.MinValue)
                {
                    this.dwShortOrder.SetItemDateTime(rows, 7, order.ConfirmTime);
                }
                //����ʱ��
                if (order.ConfirmTime != DateTime.MinValue)
                {
                    this.dwShortOrder.SetItemDateTime(rows, 8, order.ConfirmTime);
                }
                tempobj.ID = rows.ToString();
                tempobj.Name = name;
                tempID = combID;
                alcount++;
            }
            #endregion
            /*
            #region  ������һ��������Ҫ�ٵ�������Ƶ����
            //������һ��������Ҫ�ٵ�������Ƶ����
            if (tempID == combID && count > 1)
            {
                this.dwShortOrder.InsertRow();
                //��ʼ����
                this.dwShortOrder.SetItemDateTime(this.dwShortOrder.RowCount, 1, order.BeginTime);
                //��ʼʱ��
                this.dwShortOrder.SetItemDateTime(this.dwShortOrder.RowCount, 2, order.BeginTime);

                this.dwShortOrder.SetItemString(this.dwShortOrder.RowCount, 3, frequency);
                this.dwShortOrder.SetItemString(this.dwShortOrder.RowCount, 10, order.User03);
                this.dwShortOrder.SetItemDecimal(this.dwShortOrder.RowCount, 11, this.shortordercurrentpage);
                this.dwShortOrder.SetItemString(this.dwShortOrder.RowCount, 12, sgoon);

                //ҽ����
                this.dwShortOrder.SetItemString(this.dwShortOrder.RowCount, 5, order.ReciptDoctor.Name);
                //������
                this.dwShortOrder.SetItemString(this.dwShortOrder.RowCount, 6, objHelper.GetName(order.Nurse.ID));
                //��������
                if (order.ConfirmTime != DateTime.MinValue)
                {
                    this.dwShortOrder.SetItemDateTime(this.dwShortOrder.RowCount, 7, order.ConfirmTime);
                }
                //����ʱ��
                if (order.ConfirmTime != DateTime.MinValue)
                {
                    this.dwShortOrder.SetItemDateTime(this.dwShortOrder.RowCount, 8, order.ConfirmTime);
                }
                this.dwShortOrder.SetItemString(this.dwShortOrder.RowCount, 9, "dfd");
                this.dwShortOrder.SetItemString(this.dwShortOrder.RowCount, 13, order.User01);
            }
            #endregion

            #region ��Ϻ�
            //ѭ�������������Ҫ�޸ĵ���
            if (combLocation.Count != 0)
            {
                tempComb = combLocation.Clone() as ArrayList;
                all.Add(tempComb);
                combLocation.Clear();
            }
            //�޸�������Ҫ�޸ĵ���
            for (int j = 0; j < all.Count; j++)
            {
                ArrayList yunal = all[j] as ArrayList;
                for (int k = 0; k < yunal.Count; k++)
                {
                    FS.HISFC.Models.Order.Inpatient.Order changeobj = yunal[k] as FS.HISFC.Models.Order.Inpatient.Order;
                    this.dwShortOrder.SetItemString(Convert.ToInt16(changeobj.ID), 3, changeobj.Name);
                    //this.dwShortOrder.SetItemString(Convert.ToInt16(changeobj.ID), 13, changeobj.User01);
                }
            }
             *  #endregion
             * */

            //FS.WinForms.Report.Order.Function.DrawCombo(dwLongOrder, 9, 4);
            FS.WinForms.Report.Order.Function.DrawCombo(dwShortOrder, 9, 4);
           
           
        }
        /// <summary>
        /// ����Ϻŷ���
        /// </summary>
        /// <param name="o"></param>
        /// <param name="column"></param>
        /// <param name="DrawColumn"></param>
        private void DrawCombo(FSDataWindow.Controls.FSDataWindow o, int column, int DrawColumn)
        {
            if (o.RowCount < 1)//���û��ҽ������
                return;
            int i = 0;
            string tmp = "", curComboNo = "";
            tmp = o.GetItemString(1, (short)column);
            for (i = 2; i <= o.RowCount; i++)
            {
                curComboNo = o.GetItemString(i, (short)column);
                if (tmp == curComboNo)
                {
                    //��Ϻ���ȣ������һ��û�б�־˵������ϵĵ�һ��
                    if (o.IsItemNull(i - 1, (short)DrawColumn))
                    {
                        //��ϵ�һ����ֵ
                        o.SetItemSqlString(i - 1, (short)DrawColumn, "��");
                        //��������һ��
                        if (i == o.RowCount)
                            o.SetItemString(i, (short)DrawColumn, "��");
                        else
                            o.SetItemString(i, (short)DrawColumn, "��");//���ﲻ���Ƿ���һ�����һ�������һ������ϺŲ���ʱ������
                    }
                    else
                    {
                        //��������һ��
                        if (i == o.RowCount)
                            o.SetItemString(i, (short)DrawColumn, "��");
                        else
                            o.SetItemString(i, (short)DrawColumn, "��");                        
                    }
                }
                else
                {
                    //��ϺŲ��ȣ���ʱ��ı�����Ϻ����ʱ���õ�"��"����"��"��Ϊ"��"
                    if (!o.IsItemNull(i - 1, (short)DrawColumn))
                    {
                        //����һ������һ������
                        if (o.GetItemString(i - 1, (short)DrawColumn) == "��" || o.GetItemString(i - 1, (short)DrawColumn) == "��")
                            o.SetItemString(i - 1, (short)DrawColumn, "��");
                    }
                }
                tmp = curComboNo;
            }
        }

        #endregion

        #region �¼�
        private void ucOrderPrintXajd_Load(object sender, EventArgs e)
        {
            this.objHelper.ArrayObject = person.GetEmployeeAll();
        }

        

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dwLongOrder.SetRedrawOff();
            this.dwShortOrder.SetRedrawOff();
            if (this.checkBox1.Checked)
            { this.SetPatientOn(pInfo); }
            else
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    AddDataToLongOrder(alLong,"0");
                }
                else
                {
                    AddDataToShortOrder(alShort,"0");
                }
            }
            this.dwLongOrder.SetRedrawOn();
            this.dwShortOrder.SetRedrawOn();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.dwLongOrder.SetRedrawOff();
            this.dwShortOrder.SetRedrawOff();
            if (checkBox1.Checked)
            {
                this.SetPatientOn(pInfo);
            }
            else
            {
                this.SetPatient(pInfo);
            }
            this.dwLongOrder.SetRedrawOn();
            this.dwShortOrder.SetRedrawOn();
        }
        #endregion
    }
}
