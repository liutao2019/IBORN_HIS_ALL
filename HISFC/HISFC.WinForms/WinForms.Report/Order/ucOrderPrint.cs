using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.WinForms.Report.Order
{
    /// <summary>
    /// ҽ������ӡ{3A9485F2-F5AB-44c5-901C-3B1C271242DA}
    /// </summary>
    public partial class ucOrderPrint : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IPrintOrder
    {
        /// <summary>
        /// 
        /// </summary>
        public ucOrderPrint()
        {
            InitializeComponent();
        }

        //��ʼ������ҽ������ʱҽ�� �б�
        ArrayList alLong = new ArrayList();
        ArrayList alShort = new ArrayList();
        ArrayList alLongPrint = new ArrayList();
        ArrayList alShortPrint = new ArrayList();

        #region ����

        //ҽ��ҵ��
        private FS.HISFC.BizLogic.Order.Order ordManager = new FS.HISFC.BizLogic.Order.Order();

        //������Ϣ
        private FS.HISFC.Models.RADT.PatientInfo pInfo = new FS.HISFC.Models.RADT.PatientInfo();

        //ҽ����ӡҵ���
        private FS.HISFC.BizLogic.Order.OrderBill orderBillManager = new FS.HISFC.BizLogic.Order.OrderBill();

        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlManager = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        private int intLineNum = 0;
        private FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();

        //��λҵ���
        private FS.HISFC.BizLogic.Manager.Bed bedMgr = new FS.HISFC.BizLogic.Manager.Bed();
           

        #endregion

        #region IPrintOrder ��Ա
        /// <summary>
        /// ��ӡ
        /// </summary>
        //public void Print()
        //{
        //    FS.FrameWork.WinForms.Classes.Print pf = new FS.FrameWork.WinForms.Classes.Print();
        //    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(ordManager.Connection);
        //    FS.FrameWork.Management.PublicTrans.BeginTransaction();
        //    //t.BeginTransaction();

        //    //orderBillManager.SetTrans(t.Trans);
        //    orderBillManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

        //    if (this.tabControl1.SelectedIndex == 0)
        //    {
        //        pf.PageLabel = (Control)this.lblPage;
        //        this.SetLongRePrint(false);
        //        for (int i = 0; i < sheetLong.Rows.Count; i++)
        //        {
        //            int j = FS.FrameWork.Function.NConvert.ToInt32(sheetLong.Rows.Count / this.intLineNum);

        //            FS.HISFC.Models.Order.Inpatient.Order ord = sheetLong.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
        //            if (ord != null && ord.ID != null && ord.ID != "")
        //            {
        //                FS.HISFC.Models.Order.OrderBill orderBill = new FS.HISFC.Models.Order.OrderBill();
        //                orderBill = orderBillManager.GetOrderBillByOrderID(ord.ID);
        //                if (FS.FrameWork.Function.NConvert.ToBoolean(orderBill.PrintFlag))
        //                {
        //                    this.sheetLong.SetValue(i, 0, "");
        //                    this.sheetLong.SetValue(i, 1, "");
        //                    this.sheetLong.SetValue(i, 2, "");
        //                    this.sheetLong.SetValue(i, 3, "");
        //                    this.sheetLong.SetValue(i, 4, "");
        //                    this.sheetLong.SetValue(i, 5, "");
        //                    this.sheetLong.SetValue(i, 6, "");
        //                    this.sheetLong.SetValue(i, 7, "");
        //                    this.sheetLong.SetValue(i, 8, "");
        //                    this.sheetLong.SetValue(i, 9, "");
        //                    this.sheetLong.SetValue(i, 10, "");
        //                }
        //                else
        //                {
        //                    int myLineNo = 0;
        //                    int myPageNo = 0;
        //                    int myPrnSeq = 0;
        //                    this.orderBillManager.GetLastOrderBillArg(this.pInfo.ID, orderBill.Order.OrderType.ID, out myPrnSeq, out myPageNo, out myLineNo);

        //                    if (myPageNo > 0)
        //                    {
        //                        if (i < this.intLineNum)
        //                        {
        //                            myLineNo = i + 1;
                                    
        //                        }
        //                        else
        //                        {
        //                            myLineNo = 1;
        //                            myPageNo = myPageNo + 1;
        //                            this.SetLongRePrint(true);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        myLineNo = i + 1;
        //                        myPageNo = 1;
        //                        this.SetLongRePrint(true);
        //                    }
        //                    if (FS.FrameWork.Function.NConvert.ToBoolean(orderBill.PrintDCFlag) && orderBill.PageNO > 0)
        //                    {
        //                        this.sheetLong.SetValue(i, 0, "");
        //                        this.sheetLong.SetValue(i, 1, "");
        //                        this.sheetLong.SetValue(i, 2, "");
        //                        this.sheetLong.SetValue(i, 3, "");
        //                        this.sheetLong.SetValue(i, 4, "");
        //                        this.sheetLong.SetValue(i, 5, "");
        //                    }
        //                    if (orderBill.PageNO == 0)
        //                    {
        //                        if (orderBillManager.UpdateLineNoPageNo(ord.ID, myLineNo, myPageNo) < 0)
        //                        {
        //                            //t.RollBack();
        //                            FS.FrameWork.Management.PublicTrans.RollBack();
        //                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ�����кš�ҳ��ʧ�ܣ�" + orderBillManager.Err));
        //                            return;
        //                        }
        //                    }

        //                    if (orderBillManager.UpdatePrnFlag(ord.ID, "1") < 0)
        //                    {
        //                        //t.RollBack();
        //                        FS.FrameWork.Management.PublicTrans.RollBack();
        //                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ������ӡ��־ʧ�ܣ�" + orderBillManager.Err));
        //                        return;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                this.sheetLong.SetValue(i, 0, "");
        //                this.sheetLong.SetValue(i, 1, "");
        //                this.sheetLong.SetValue(i, 2, "");
        //                this.sheetLong.SetValue(i, 3, "");
        //                this.sheetLong.SetValue(i, 4, "");
        //                this.sheetLong.SetValue(i, 5, "");
        //                this.sheetLong.SetValue(i, 6, "");
        //                this.sheetLong.SetValue(i, 7, "");
        //                this.sheetLong.SetValue(i, 8, "");
        //                this.sheetLong.SetValue(i, 9, "");
        //                this.sheetLong.SetValue(i, 10, "");

                        
        //            }
        //        }
        //        //this.neuLabel15.Text = "��";
        //        //this.neuLabel19.Text = "ҳ";

        //        //if (this.sheetLong.RowCount < intLineNum)
        //        //{
        //        //    this.sheetLong.RowCount = intLineNum;

        //        //}
        //        //if (this.sheetLong.RowCount > intLineNum)
        //        //{
        //        //    long longNum = 0;
        //        //    System.Math.DivRem(this.sheetLong.RowCount, intLineNum, out longNum);
        //        //    this.sheetLong.RowCount = this.sheetLong.RowCount - FS.FrameWork.Function.NConvert.ToInt32(longNum) + intLineNum;
        //        //}

                
        //        pf.PrintPreview(this.plLong);

        //    }
        //    else
        //    {
        //        pf.PageLabel = (Control)this.lblPages;
        //        this.SetShortRePrint(false);
        //        for (int i = 0; i < sheetShort.Rows.Count; i++)
        //        {
                    
        //            FS.HISFC.Models.Order.Inpatient.Order ord = sheetShort.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
        //            if (ord != null && ord.ID != null && ord.ID != "")
        //            {
        //                FS.HISFC.Models.Order.OrderBill orderBill = new FS.HISFC.Models.Order.OrderBill();
        //                orderBill = orderBillManager.GetOrderBillByOrderID(ord.ID);
        //                if (FS.FrameWork.Function.NConvert.ToBoolean(orderBill.PrintFlag))
        //                {
        //                    this.sheetShort.SetValue(i, 0, "");
        //                    this.sheetShort.SetValue(i, 1, "");
        //                    this.sheetShort.SetValue(i, 2, "");
        //                    this.sheetShort.SetValue(i, 3, "");
        //                    this.sheetShort.SetValue(i, 4, "");
        //                    this.sheetShort.SetValue(i, 5, "");
        //                    this.sheetShort.SetValue(i, 6, "");
        //                    this.sheetShort.SetValue(i, 7, "");
        //                    this.sheetShort.SetValue(i, 8, "");
        //                }
        //                else
        //                {
        //                    int myLineNo = 0;
        //                    int myPageNo = 0;
        //                    int myPrnSeq = 0;
        //                    this.orderBillManager.GetLastOrderBillArg(this.pInfo.ID, orderBill.Order.OrderType.ID, out myPrnSeq, out myPageNo, out myLineNo);

        //                    if (myPageNo > 0)
        //                    {
        //                        if (i < this.intLineNum)
        //                        {
        //                            myLineNo = i + 1;

        //                        }
        //                        else
        //                        {
        //                            myLineNo = 1;
        //                            myPageNo = myPageNo + 1;
        //                            this.SetShortRePrint(true);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        myLineNo = i + 1;
        //                        myPageNo = 1;
        //                        this.SetShortRePrint(true);
        //                    }
        //                    if (orderBill.PageNO == 0)
        //                    {
        //                        if (orderBillManager.UpdateLineNoPageNo(ord.ID, myLineNo, myPageNo) < 0)
        //                        {
        //                            //t.RollBack();
        //                            FS.FrameWork.Management.PublicTrans.RollBack();
        //                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ�����кš�ҳ��ʧ�ܣ�" + orderBillManager.Err));
        //                            return;
        //                        }
        //                    }
        //                    if (orderBillManager.UpdatePrnFlag(ord.ID, "1") < 0)
        //                    {
        //                        //t.RollBack();
        //                        FS.FrameWork.Management.PublicTrans.RollBack();
        //                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ������ӡ��־ʧ�ܣ�" + orderBillManager.Err));
        //                        return;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                this.sheetShort.SetValue(i, 0, "");
        //                this.sheetShort.SetValue(i, 1, "");
        //                this.sheetShort.SetValue(i, 2, "");
        //                this.sheetShort.SetValue(i, 3, "");
        //                this.sheetShort.SetValue(i, 4, "");
        //                this.sheetShort.SetValue(i, 5, "");
        //                this.sheetShort.SetValue(i, 6, "");
        //                this.sheetShort.SetValue(i, 7, "");
        //                this.sheetShort.SetValue(i, 8, "");

        //                this.SetShortRePrint(false);
        //            }
        //        }

        //        //if (this.sheetShort.RowCount < intLineNum)
        //        //{
        //        //    this.sheetShort.RowCount = intLineNum;
        //        //}
        //        //if (this.sheetShort.RowCount > intLineNum)
        //        //{
        //        //    long shortNum = 0;
        //        //    System.Math.DivRem(this.sheetShort.RowCount, intLineNum, out shortNum);
        //        //    this.sheetShort.RowCount = this.sheetShort.RowCount - FS.FrameWork.Function.NConvert.ToInt32(shortNum) + intLineNum;
        //        //}
                
        //        pf.PrintPreview(this.plShort);
                
        //        //this.lblPage.Text = pf.PageLabel.Text;
        //    }
        //    //t.Commit();
        //    FS.FrameWork.Management.PublicTrans.Commit();
        //    this.SetShortRePrint(true);
        //    this.SetLongRePrint(true);
        //    this.SetPatient(this.pInfo);
        //}
        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        /// <param name="patientInfo"></param>
        public void SetPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.intLineNum = this.controlManager.GetControlParam<int>("200008", false, 21);
            //��ӡ����
            
            //��ѯҽ������ʱ��֧������
            pInfo = patientInfo;
            string hospitalName = constant.GetHospitalName();

            if (string.IsNullOrEmpty(patientInfo.PVisit.PatientLocation.Room) && patientInfo.PVisit.PatientLocation.Bed != null)
            {//��ѯ����
                FS.HISFC.Models.Base.Bed bed = this.bedMgr.GetBedInfo(patientInfo.PVisit.PatientLocation.Bed.ID);
                if (bed != null)
                {
                    patientInfo.PVisit.PatientLocation.Room = bed.SickRoom.ID;
                }
            }

            this.lbLongPatientNo.Text = patientInfo.ID;
            this.lbLonglName.Text = patientInfo.Name;
            this.lblShortInpatientNO.Text = patientInfo.ID;
            this.lblShortName.Text = patientInfo.Name;
            this.neuLabel2.Text = hospitalName;
            this.neuLabel10.Text = hospitalName;
                        
            if (patientInfo.Sex != null)
            {
                this.lblSex.Text = patientInfo.Sex.Name;
                this.lblSexs.Text = patientInfo.Sex.Name;
            }
            if (patientInfo.Age != null)
            {
                this.lblAge.Text = patientInfo.Age.ToString();
                this.lblAges.Text = patientInfo.Age;
            }
            if (patientInfo.PVisit.PatientLocation.Room != null)
            {
                this.lblRoom.Text = patientInfo.PVisit.PatientLocation.Room.ToString();
                this.lblRooms.Text = patientInfo.PVisit.PatientLocation.Room;
            }
            if (patientInfo.PVisit.PatientLocation.Bed != null)
            {
                this.lblBed.Text = patientInfo.PVisit.PatientLocation.Bed.ToString();
                this.lblBeds.Text = patientInfo.PVisit.PatientLocation.Bed.ToString();
            }
            if (patientInfo.PVisit.PatientLocation.Dept != null)
            {
                this.lblDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name.ToString();
                this.lblDepts.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��ʾҽ����Ϣ,���Ժ�......");

            Application.DoEvents();

            ArrayList alAll = new ArrayList();

            try
            {

                //alAll = this.ordManager.QueryDcOrder(patientInfo.ID);
                alAll = this.ordManager.QueryPrnOrder(patientInfo.ID);
            }
            catch
            {
                FS.FrameWork.WinForms.Classes.Function.MessageBox(ordManager.Err);
            }

            alLong.Clear();
            alShort.Clear();
            
            if (alAll != null)
            {
                foreach (object obj in alAll)
                {
                    FS.HISFC.Models.Order.Inpatient.Order order = obj as FS.HISFC.Models.Order.Inpatient.Order;
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
            AddObjecToLongOrderSheet(alLong);
            AddObjecToShortOrderSheet(alShort);
            this.GetPrintOrder();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ShowPrintSet()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Print()
        {
            this.PrintOrder();
        }
        #endregion

        private void AddObjecToLongOrderSheet(ArrayList arr)
        {
            sheetLong.Rows.Count = 0;
            alLongPrint.Clear();
            
            FS.HISFC.Models.Order.Inpatient.Order ord = new FS.HISFC.Models.Order.Inpatient.Order();
            for (int i = 0; i < arr.Count; i++)
            {
                sheetLong.Rows.Add(i, 1);
                ord = arr[i] as FS.HISFC.Models.Order.Inpatient.Order;


                this.sheetLong.SetValue(i, 0, ord.BeginTime.Month.ToString() + "." + ord.BeginTime.Day.ToString());//.ToShortDateString());
                sheetLong.SetValue(i, 1, ord.BeginTime.ToShortTimeString());
                sheetLong.SetValue(i, 2, ord.Item.Name + "  " + ord.Frequency.Name + "  " + ord.Usage.Name);
                sheetLong.SetValue(i, 3, ord.ReciptDoctor.Name);
                sheetLong.SetValue(i, 4, ord.Nurse.Name);
                sheetLong.SetValue(i, 5, ord.ConfirmTime);
                //sheetLong.SetValue(i, 10, ord.Combo.ID);
                if (ord.EndTime != DateTime.MinValue)
                {
                    sheetLong.SetValue(i, 6, ord.EndTime.Month.ToString() + "." + ord.EndTime.Day.ToString());
                    sheetLong.SetValue(i, 7, ord.EndTime.ToShortTimeString());//.ExecOper.OperTime.ToShortTimeString());
                    sheetLong.SetValue(i, 8, ord.DCOper.Name);
                    sheetLong.SetValue(i, 9, ord.ExecOper.Name);
                    sheetLong.SetValue(i, 10, ord.ExecOper.OperTime.ToShortTimeString());
                }

                sheetLong.Rows[i].Tag = ord;
            }
            if (this.sheetLong.RowCount < this.intLineNum)
            {
                this.sheetLong.RowCount = intLineNum;

            }
            if (this.sheetLong.RowCount > intLineNum)
            {
                long longNum = 0;
                System.Math.DivRem(this.sheetLong.RowCount, intLineNum, out longNum);
                this.sheetLong.RowCount = this.sheetLong.RowCount - FS.FrameWork.Function.NConvert.ToInt32(longNum) + intLineNum;
                //int page = this.sheetLong.RowCount / 21;
                //this.lblPage.Text =  page.ToString();
            }
            //Function.DrawCombo(sheetLong, 10, 4);
        }


        private void AddObjecToShortOrderSheet(ArrayList arr)
        {
            sheetShort.Rows.Count = 0;
            FS.HISFC.Models.Order.Inpatient.Order ord = new FS.HISFC.Models.Order.Inpatient.Order();
            for (int i = 0; i < arr.Count; i++)
            {
                sheetShort.Rows.Add(i, 1);
                ord = arr[i] as FS.HISFC.Models.Order.Inpatient.Order;
                this.sheetShort.SetValue(i, 0, ord.BeginTime.ToShortDateString());
                sheetShort.SetValue(i, 1, ord.BeginTime.ToShortTimeString());
                sheetShort.SetValue(i, 2, ord.Item.Name + "  " + ord.Frequency.Name + "  " + ord.Usage.Name);
                sheetShort.SetValue(i, 3, ord.ReciptDoctor.Name);
                sheetShort.SetValue(i, 4, ord.BeginTime);
                sheetShort.SetValue(i, 5, ord.ExecOper.Name);
                sheetShort.SetValue(i, 6, ord.EndTime.ToShortDateString());
                sheetShort.SetValue(i, 7, ord.EndTime.ToShortTimeString());

                //if (ord.EndTime != DateTime.MinValue)
                //{
                //    sheetShort.SetValue(i, 6, ord.EndTime.Month.ToString() + "-" + ord.EndTime.Day.ToString());
                //    sheetShort.SetValue(i, 7, ord.EndTime.ToShortTimeString());
                //    sheetShort.SetValue(i, 8, ord.ExecOper.Name);
                //}
                sheetShort.Rows[i].Tag = ord;
            }
            if (this.sheetShort.RowCount < intLineNum)
            {
                this.sheetShort.RowCount = intLineNum;
                //this.neuLabel24.Text = "1";
            }
            else if (this.sheetShort.RowCount > intLineNum)
            {
                long shortNum = 0;
                System.Math.DivRem(this.sheetShort.RowCount, intLineNum, out shortNum);
                this.sheetShort.RowCount = this.sheetShort.RowCount - FS.FrameWork.Function.NConvert.ToInt32(shortNum) + intLineNum;

                //int page = this.sheetShort.RowCount / 21;
                //this.neuLabel24.Text =  page.ToString();
            }
            //Function.DrawCombo(sheetShort, 9, 3);
        }

        private void GetPrintOrder()
        {
            for (int i = 0; i < sheetLong.Rows.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order ord = sheetLong.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                alLongPrint.Add(ord);
            }
            for (int i = 0; i < sheetShort.Rows.Count; i++)
            {
                FS.HISFC.Models.Order.Inpatient.Order ord = sheetShort.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                alShortPrint.Add(ord);
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void PrintOrder()
        {
            FS.FrameWork.WinForms.Classes.Print pf = new FS.FrameWork.WinForms.Classes.Print();
            
            if (this.tabControl1.SelectedIndex == 0)
            {
                //pf.PageLabel = (Control)this.lblPage;
                this.SetLongRePrint(false);
                //this.sheetLong.RowCount = 0;
                int pageCount = FS.FrameWork.Function.NConvert.ToInt32(alLongPrint.Count / this.intLineNum);
                for (int iPage = 0; iPage < pageCount; iPage++)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    
                    orderBillManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    this.sheetLong.RowCount = 0;
                    bool needPrint = false;
                    for (int i = 0; i < intLineNum; i++)
                    {
                        sheetLong.Rows.Add(i, 1);
                        FS.HISFC.Models.Order.Inpatient.Order ord = null;
                        
                        try
                        {
                            ord = alLongPrint[i + iPage * intLineNum] as FS.HISFC.Models.Order.Inpatient.Order;
                        }
                        catch
                        { }
                        if (ord != null && ord.ID != null && ord.ID != "")
                        {
                            FS.HISFC.Models.Order.OrderBill orderBill = new FS.HISFC.Models.Order.OrderBill();
                            orderBill = orderBillManager.GetOrderBillByOrderID(ord.ID);
                            if (FS.FrameWork.Function.NConvert.ToBoolean(orderBill.PrintFlag))
                            {
                                this.sheetLong.SetValue(i, 0, "");
                                this.sheetLong.SetValue(i, 1, "");
                                this.sheetLong.SetValue(i, 2, "");
                                this.sheetLong.SetValue(i, 3, "");
                                this.sheetLong.SetValue(i, 4, "");
                                this.sheetLong.SetValue(i, 5, "");
                                this.sheetLong.SetValue(i, 6, "");
                                this.sheetLong.SetValue(i, 7, "");
                                this.sheetLong.SetValue(i, 8, "");
                                this.sheetLong.SetValue(i, 9, "");
                                this.sheetLong.SetValue(i, 10, "");
                                
                            }
                            else
                            {
                                needPrint = true;
                                this.sheetLong.SetValue(i, 0, ord.BeginTime.Month.ToString() + "." + ord.BeginTime.Day.ToString());//.ToShortDateString());
                                sheetLong.SetValue(i, 1, ord.BeginTime.ToShortTimeString());
                                sheetLong.SetValue(i, 2, ord.Item.Name + "  " + ord.Frequency.Name + "  " + ord.Usage.Name);
                                sheetLong.SetValue(i, 3, ord.ReciptDoctor.Name);
                                sheetLong.SetValue(i, 4, ord.Nurse.Name);
                                sheetLong.SetValue(i, 5, ord.ConfirmTime);

                                if (ord.EndTime != DateTime.MinValue)
                                {
                                    sheetLong.SetValue(i, 6, ord.EndTime.Month.ToString() + "." + ord.EndTime.Day.ToString());
                                    sheetLong.SetValue(i, 7, ord.EndTime.ToShortTimeString());//.ExecOper.OperTime.ToShortTimeString());
                                    sheetLong.SetValue(i, 8, ord.DCOper.Name);
                                    sheetLong.SetValue(i, 9, ord.ExecOper.Name);
                                    sheetLong.SetValue(i, 10, ord.ExecOper.OperTime.ToShortTimeString());
                                }
                                else
                                {
                                    this.sheetLong.SetValue(i, 6, "");
                                    this.sheetLong.SetValue(i, 7, "");
                                    this.sheetLong.SetValue(i, 8, "");
                                    this.sheetLong.SetValue(i, 9, "");
                                    this.sheetLong.SetValue(i, 10, "");
                                }
                                                                
                                int myLineNo = 0;
                                int myPageNo = 0;
                                int myPrnSeq = 0;
                                this.orderBillManager.GetLastOrderBillArg(this.pInfo.ID, orderBill.Order.OrderType.ID, out myPrnSeq, out myPageNo, out myLineNo);

                                if (myPageNo > 0)
                                {
                                    if (myLineNo < this.intLineNum)
                                    {
                                        myLineNo = i + 1;

                                    }
                                    else
                                    {
                                        myLineNo = i + 1;
                                        myPageNo = iPage + 1;
                                        this.SetLongRePrint(true);
                                    }
                                }
                                else
                                {
                                    myLineNo = i + 1;
                                    myPageNo = 1;
                                    this.SetLongRePrint(true);
                                }
                                if (FS.FrameWork.Function.NConvert.ToBoolean(orderBill.PrintDCFlag) && orderBill.PageNO > 0)
                                {
                                    this.sheetLong.SetValue(i, 0, "");
                                    this.sheetLong.SetValue(i, 1, "");
                                    this.sheetLong.SetValue(i, 2, "");
                                    this.sheetLong.SetValue(i, 3, "");
                                    this.sheetLong.SetValue(i, 4, "");
                                    this.sheetLong.SetValue(i, 5, "");
                                }
                                if (orderBill.PageNO == 0)
                                {
                                    if (orderBillManager.UpdateLineNoPageNo(ord.ID, myLineNo, myPageNo) < 0)
                                    {
                                        
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ�����кš�ҳ��ʧ�ܣ�" + orderBillManager.Err));
                                        return;
                                    }
                                }

                                if (orderBillManager.UpdatePrnFlag(ord.ID, "1") < 0)
                                {
                                    
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ������ӡ��־ʧ�ܣ�" + orderBillManager.Err));
                                    return;
                                }
                            }
                        }
                        else
                        {
                            this.sheetLong.SetValue(i, 0, "");
                            this.sheetLong.SetValue(i, 1, "");
                            this.sheetLong.SetValue(i, 2, "");
                            this.sheetLong.SetValue(i, 3, "");
                            this.sheetLong.SetValue(i, 4, "");
                            this.sheetLong.SetValue(i, 5, "");
                            this.sheetLong.SetValue(i, 6, "");
                            this.sheetLong.SetValue(i, 7, "");
                            this.sheetLong.SetValue(i, 8, "");
                            this.sheetLong.SetValue(i, 9, "");
                            this.sheetLong.SetValue(i, 10, "");

                        }
                    }
                    //this.neuLabel15.Text = "��";
                    //this.neuLabel19.Text = "ҳ";
                    this.lblPage.Text = "��" + (iPage + 1).ToString() + "ҳ";
                    if (needPrint)
                    {
                        pf.PrintPreview(this.plLong);
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }

            }
            else
            {
                //pf.PageLabel = (Control)this.lblPages;
                this.SetShortRePrint(false);
                this.sheetShort.RowCount = 0;
                int pageCount = FS.FrameWork.Function.NConvert.ToInt32(alShortPrint.Count / this.intLineNum);
                for (int iPage = 0; iPage < pageCount; iPage++)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    orderBillManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    bool needPrint = false;
                    for (int i = 0; i < intLineNum; i++)
                    {
                        sheetShort.Rows.Add(i, 1);
                        FS.HISFC.Models.Order.Inpatient.Order ord = null;

                        try
                        {
                            ord = alShortPrint[i + iPage * intLineNum] as FS.HISFC.Models.Order.Inpatient.Order;
                        }
                        catch
                        { }
                        
                        if (ord != null && ord.ID != null && ord.ID != "")
                        {
                            FS.HISFC.Models.Order.OrderBill orderBill = new FS.HISFC.Models.Order.OrderBill();
                            orderBill = orderBillManager.GetOrderBillByOrderID(ord.ID);
                            if (FS.FrameWork.Function.NConvert.ToBoolean(orderBill.PrintFlag))
                            {
                                this.sheetShort.SetValue(i, 0, "");
                                this.sheetShort.SetValue(i, 1, "");
                                this.sheetShort.SetValue(i, 2, "");
                                this.sheetShort.SetValue(i, 3, "");
                                this.sheetShort.SetValue(i, 4, "");
                                this.sheetShort.SetValue(i, 5, "");
                                this.sheetShort.SetValue(i, 6, "");
                                this.sheetShort.SetValue(i, 7, "");
                                this.sheetShort.SetValue(i, 8, "");
                            }
                            else
                            {
                                needPrint = true;
                                sheetShort.Rows.Add(i, 1);
                                this.sheetShort.SetValue(i, 0, ord.BeginTime.ToShortDateString());
                                sheetShort.SetValue(i, 1, ord.BeginTime.ToShortTimeString());
                                sheetShort.SetValue(i, 2, ord.Item.Name + "  " + ord.Frequency.Name + "  " + ord.Usage.Name);
                                sheetShort.SetValue(i, 3, ord.ReciptDoctor.Name);
                                sheetShort.SetValue(i, 4, ord.BeginTime);
                                sheetShort.SetValue(i, 5, ord.ExecOper.Name);
                                sheetShort.SetValue(i, 6, ord.EndTime.ToShortDateString());
                                sheetShort.SetValue(i, 7, ord.EndTime.ToShortTimeString());
                                
                                int myLineNo = 0;
                                int myPageNo = 0;
                                int myPrnSeq = 0;
                                this.orderBillManager.GetLastOrderBillArg(this.pInfo.ID, orderBill.Order.OrderType.ID, out myPrnSeq, out myPageNo, out myLineNo);

                                if (myPageNo > 0)
                                {
                                    if (i < this.intLineNum)
                                    {
                                        myLineNo = i + 1;

                                    }
                                    else
                                    {
                                        myLineNo = i + 1;
                                        myPageNo = iPage + 1;
                                        this.SetShortRePrint(true);
                                    }
                                }
                                else
                                {
                                    myLineNo = i + 1;
                                    myPageNo = 1;
                                    this.SetShortRePrint(true);
                                }
                                if (orderBill.PageNO == 0)
                                {
                                    if (orderBillManager.UpdateLineNoPageNo(ord.ID, myLineNo, myPageNo) < 0)
                                    {
                                        
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ�����кš�ҳ��ʧ�ܣ�" + orderBillManager.Err));
                                        return;
                                    }
                                }
                                if (orderBillManager.UpdatePrnFlag(ord.ID, "1") < 0)
                                {
                                    
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҽ������ӡ��־ʧ�ܣ�" + orderBillManager.Err));
                                    return;
                                }
                            }
                        }
                        else
                        {
                            this.sheetShort.SetValue(i, 0, "");
                            this.sheetShort.SetValue(i, 1, "");
                            this.sheetShort.SetValue(i, 2, "");
                            this.sheetShort.SetValue(i, 3, "");
                            this.sheetShort.SetValue(i, 4, "");
                            this.sheetShort.SetValue(i, 5, "");
                            this.sheetShort.SetValue(i, 6, "");
                            this.sheetShort.SetValue(i, 7, "");
                            this.sheetShort.SetValue(i, 8, "");

                            this.SetShortRePrint(false);
                        }
                    }
                    this.lblPages.Text = "��" + (iPage + 1).ToString() + "ҳ";
                    if (needPrint)
                    {
                        pf.PrintPreview(this.plShort);
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
                        
            this.SetShortRePrint(true);
            this.SetLongRePrint(true);
            this.SetPatient(this.pInfo);
        }


        /// <summary>
        /// ����ӡʱ������������
        /// </summary>
        /// <param name="isShow"></param>
        protected virtual void SetLongRePrint(bool isShow)
        {
            this.lbLongPatientNo.Visible = isShow;
            this.lbLonglName.Visible = isShow;

            this.lblSex.Visible = isShow;
            this.lblAge.Visible = isShow;
            this.lblRoom.Visible = isShow;
            this.lblBed.Visible = isShow;
            this.lblDept.Visible = isShow;
            this.lblPage.Visible = isShow;
            //this.neuLabel1.Visible = isShow;
            //this.neuLabel10.Visible = isShow;
            this.neuLabel11.Visible = isShow;
            this.neuLabel12.Visible = isShow;
            //this.neuLabel13.Visible = isShow;
            this.neuLabel14.Visible = isShow;
            //this.neuLabel16.Visible = isShow;
            //this.neuLabel18.Visible = isShow;
            this.neuLabel2.Visible = isShow;
            //this.neuLabel21.Visible = isShow;
            //this.neuLabel23.Visible = isShow;
            this.neuLabel3.Visible = isShow;
            this.neuLabel4.Visible = isShow;
            this.neuLabel5.Visible = isShow;
            //this.neuLabel6.Visible = isShow;
            //this.neuLabel7.Visible = isShow;
            this.neuLabel8.Visible = isShow;
            this.neuLabel9.Visible = isShow;
            if (isShow)
            {
                this.sheetLong.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, true, false, true, true);
                this.sheetLong.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 0).Value = " ��   ʼ";
                this.sheetLong.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 2).Value = "    ��    ��    ҽ    �� ";
                this.sheetLong.ColumnHeader.Cells.Get(0, 3).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 3).Value = "ҽ����";
                this.sheetLong.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 4).Value = "������";
                this.sheetLong.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 5).Value = "����ʱ��";
                this.sheetLong.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 6).Value = "ͣ ֹ";
                this.sheetLong.ColumnHeader.Cells.Get(0, 8).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 8).Value = "ҽ����";
                this.sheetLong.ColumnHeader.Cells.Get(0, 9).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 9).Value = "������";
                this.sheetLong.ColumnHeader.Cells.Get(0, 10).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 10).Value = "����ʱ��";
                this.sheetLong.ColumnHeader.Cells.Get(1, 0).Value = "����";
                this.sheetLong.ColumnHeader.Cells.Get(1, 1).Value = "ʱ��";
                this.sheetLong.ColumnHeader.Cells.Get(1, 6).Value = "����";
                this.sheetLong.ColumnHeader.Cells.Get(1, 7).Value = "ʱ��";
                this.sheetLong.ColumnHeaderHorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, Color.Black);
                this.sheetLong.ColumnHeaderVerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, Color.Black);
                this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            }
            else
            {
                this.sheetLong.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, true, false, true, true);
                this.sheetLong.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 0).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 2).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(0, 3).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 3).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 4).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 5).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 6).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(0, 8).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 8).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(0, 9).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 9).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(0, 10).RowSpan = 2;
                this.sheetLong.ColumnHeader.Cells.Get(0, 10).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(1, 0).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(1, 1).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(1, 6).Value = "";
                this.sheetLong.ColumnHeader.Cells.Get(1, 7).Value = "";
                this.sheetLong.ColumnHeaderHorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, Color.White);
                this.sheetLong.ColumnHeaderVerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, Color.White);
                this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            }
            
        }

        /// <summary>
        /// ����ӡʱ������������
        /// </summary>
        /// <param name="isShow"></param>
        protected virtual void SetShortRePrint(bool isShow)
        {
            this.lblShortInpatientNO.Visible = isShow;
            this.lblShortName.Visible = isShow;

            this.lblSexs.Visible = isShow;
            this.lblAges.Visible = isShow;
            this.lblRooms.Visible = isShow;
            this.lblBeds.Visible = isShow;
            this.lblDepts.Visible = isShow;
            this.lblPages.Visible = isShow;
            this.neuLabel1.Visible = isShow;
            this.neuLabel10.Visible = isShow;
            //this.neuLabel11.Visible = isShow;
            //this.neuLabel12.Visible = isShow;
            this.neuLabel13.Visible = isShow;
            //this.neuLabel14.Visible = isShow;
            this.neuLabel16.Visible = isShow;
            this.neuLabel18.Visible = isShow;
            //this.neuLabel2.Visible = isShow;
            this.neuLabel21.Visible = isShow;
            this.neuLabel23.Visible = isShow;
            //this.neuLabel3.Visible = isShow;
            //this.neuLabel4.Visible = isShow;
            //this.neuLabel5.Visible = isShow;
            this.neuLabel6.Visible = isShow;
            this.neuLabel7.Visible = isShow;
            //this.neuLabel8.Visible = isShow;
            //this.neuLabel9.Visible = isShow;
            if (isShow)
            {
                this.sheetShort.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, true, false, true, true);
                this.sheetShort.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 0).Value = "  ��    ʼ";
                this.sheetShort.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 2).Value = "    ��  ʱ  ҽ  ��";
                this.sheetShort.ColumnHeader.Cells.Get(0, 3).RowSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 3).Value = "ҽ����";
                this.sheetShort.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 4).Value = "ҽ��ʱ��";
                this.sheetShort.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 5).Value = "ִ����";
                this.sheetShort.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 6).Value = " ִ �� ʱ ��";
                this.sheetShort.ColumnHeader.Cells.Get(1, 0).Value = "����";
                this.sheetShort.ColumnHeader.Cells.Get(1, 1).Value = "ʱ��";
                this.sheetShort.ColumnHeader.Cells.Get(1, 6).Value = "����";
                this.sheetShort.ColumnHeader.Cells.Get(1, 7).Value = "ʱ��";
                this.sheetShort.ColumnHeaderHorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, Color.Black);
                this.sheetShort.ColumnHeaderVerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, Color.Black);
                this.fpSpread3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            }
            else
            {
                this.sheetShort.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.Control, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, true, true, false, true, true);
                this.sheetShort.ColumnHeader.Cells.Get(0, 0).ColumnSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 0).Value = "";
                this.sheetShort.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 2).Value = "";
                this.sheetShort.ColumnHeader.Cells.Get(0, 3).RowSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 3).Value = "";
                this.sheetShort.ColumnHeader.Cells.Get(0, 4).RowSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 4).Value = "";
                this.sheetShort.ColumnHeader.Cells.Get(0, 5).RowSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 5).Value = "";
                this.sheetShort.ColumnHeader.Cells.Get(0, 6).ColumnSpan = 2;
                this.sheetShort.ColumnHeader.Cells.Get(0, 6).Value = "";
                this.sheetShort.ColumnHeader.Cells.Get(1, 0).Value = "";
                this.sheetShort.ColumnHeader.Cells.Get(1, 1).Value = "";
                this.sheetShort.ColumnHeader.Cells.Get(1, 6).Value = "";
                this.sheetShort.ColumnHeader.Cells.Get(1, 7).Value = "";
                this.sheetShort.ColumnHeaderHorizontalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, Color.White);
                this.sheetShort.ColumnHeaderVerticalGridLine = new FarPoint.Win.Spread.GridLine(FarPoint.Win.Spread.GridLineType.Flat, Color.White);
                this.fpSpread3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            }

        }

    }
}
