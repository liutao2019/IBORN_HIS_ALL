using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [��������: ���ϵ�ҽ��������]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Order : IntegrateBase
    {
        #region ����
        protected FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        private FS.HISFC.BizProcess.Integrate.Pharmacy managerPharmacy = null;

        protected FS.HISFC.BizProcess.Integrate.Pharmacy ManagerPharmacy
        {
            get
            {
                if (managerPharmacy == null)
                {
                    managerPharmacy = new Pharmacy();
                }
                return managerPharmacy;
            }
        }

        private FS.HISFC.BizProcess.Integrate.Fee managerFee = null;

        protected FS.HISFC.BizProcess.Integrate.Fee ManagerFee
        {
            get
            {
                if (managerFee == null)
                {
                    managerFee = new Fee();
                }
                return managerFee;
            }
        }

        protected FS.HISFC.BizLogic.Fee.UndrugPackAge managerPack = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
        protected FS.HISFC.BizLogic.RADT.InPatient managerRADT = new FS.HISFC.BizLogic.RADT.InPatient();
        protected FS.HISFC.BizLogic.Order.OutPatient.Order outOrderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        protected FS.HISFC.BizLogic.Order.OrderBill orderBillManager = new FS.HISFC.BizLogic.Order.OrderBill();
        protected FS.HISFC.BizLogic.Manager.Department deptMangager = new FS.HISFC.BizLogic.Manager.Department();
        //{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} ����ҽ���鴦��
        protected FS.HISFC.BizLogic.Order.MedicalTeamForDoct medicalTeamForDoctBizLogic = new FS.HISFC.BizLogic.Order.MedicalTeamForDoct();

        private static FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = null;

        public static FS.HISFC.BizProcess.Integrate.Common.ControlParam CtrlIntegrate
        {
            get
            {
                if (ctrlIntegrate == null)
                {
                    ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                }
                return Order.ctrlIntegrate;
            }
        }


        /// <summary>
        /// ��ȡ�۸�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice IGetItemPrice = null;

        /// <summary>
        /// �Ƿ�֧�ָ���ת�ƣ���ʳ��������Զ�����
        /// </summary>
        public bool IsUpdateOther = true;

        /// <summary>
        /// Ƿ���ж�����
        /// </summary>
        private FS.HISFC.Models.Base.MessType messType = MessType.M;

        /// <summary>
        /// СʱƵ��
        /// </summary>
        private static string hourFerquenceID = string.Empty;

        private static Hashtable hsDepts = new Hashtable();

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ��ж�Ƿ�ѣ�Ƿ���Ƿ���ʾ
        /// </summary>
        public FS.HISFC.Models.Base.MessType MessageType
        {
            set
            {
                messType = value;
            }
            get
            {
                return messType;
            }
        }

        #endregion

        #region ����

        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            managerRADT.SetTrans(trans);
            orderManager.SetTrans(trans);
            outOrderManager.SetTrans(trans);
            ManagerPharmacy.SetTrans(trans);
            fee.SetTrans(trans);
            managerPack.SetTrans(trans);
            orderBillManager.SetTrans(trans);
            deptMangager.SetTrans(trans);

            this.trans = trans;
        }

        /// <summary>
        /// �����´ηֽ�ʱ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public int UpdateDecoTime(string inpatientNo, int days)
        {
            this.SetDB(orderManager);
            return orderManager.UpdateDecoTime(inpatientNo, days);
        }

        /// <summary>
        /// �����´ηֽ�ʱ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dtNextTime"></param>
        /// <returns></returns>
        public int UpdateDecoTime(string inpatientNo, DateTime dtNextTime)
        {
            this.SetDB(orderManager);
            return orderManager.UpdateDecoTime(inpatientNo, dtNextTime);
        }

        #endregion

        #region ����

        #region ҽ�����

        /// <summary>
        ///  ��˱��棬���ҽ��������ʱҽ�������շѣ�
        /// ��Ҫ��fee����Commit��RollBack����
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alOrders"></param>
        /// <param name="isLong">�Ƿ���ҽ��</param>
        /// <param name="isCharge">�Ƿ��շѣ� Ƿ�ѻ��߿���ֻ���治�շ�</param>
        //public int SaveChecked(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList alOrders, bool isLong, string nurseCode, bool isCharge, bool iSCDCharge)
        //{

        //    //�շѿ��� �ж���(true)--ҩ����ҩʱ�շ� ����(false)--���/�ֽ�ʱ�շ�
        //    //True ��ʿվ�շ� False ҩ���շ�
        //    bool isNurseCharge = GetIsNurseCharge(ref this.trans);

        //    DateTime dt = orderManager.GetDateTimeFromSysDateTime();

        //    ArrayList alFeeOrder = new ArrayList(); //�շ�ҽ��
        //    ArrayList alSendDrug = new ArrayList(); //��Ҫ��ҩҩƷ
        //    Hashtable hsCombo = new Hashtable();

        //    #region ���ϰ汾������ {2AFC76CB-3353-4865-AEB4-AFBEE09DD1D7}
        //    //FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        //    ////������Ϣά�������Ƿ�ά��ҽ������ӡ����  ���ϰ汾������
        //    //bool val = con.GetControlParam<bool>("B00002",false,);
        //    //ArrayList itemList = null;
        //    //if (val)
        //    //{
        //    //    FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
        //    //    itemList = item.SelectAllItemByOrderPrint("1");
        //    //}
        //    #endregion

        //    #region ���ҽ��

        //    //��¼��һ�����
        //    string strComboNo = "";

        //    //����ҽ��
        //    for (int i = 0; i < alOrders.Count; i++) //����ҽ��
        //    {
        //        if (isLong)
        //        {
        //            #region ����ҽ������
        //            FS.HISFC.Models.Order.Inpatient.Order order = alOrders[i] as FS.HISFC.Models.Order.Inpatient.Order;

        //            if (order.Status == 0)//δ���ҽ��
        //            {
        //                #region δ���ҽ������

        //                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug) //ҩƷ
        //                {
        //                    //�Ѿ��޸ķ�ҩ������ҹ��򣬴˴�����ȡ���ڿ���
        //                    //ִ�п���Ϊ��ʿ���ڿ���
        //                    //order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();
        //                    order.Patient.Name = patient.Name;
        //                }
        //                if (order.Combo.ID != strComboNo)
        //                {
        //                    ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//��ѯ����

        //                    for (int f = 0; f < alSubtbl.Count; f++)//���Ĵ���
        //                    {
        //                        if (((FS.HISFC.Models.Order.Order)alSubtbl[f]).Status == 0)
        //                        {
        //                            if (orderManager.ConfirmAndExecOrder((FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f], false, "", dt) == -1) //�����շѱ��
        //                            {
        //                                MessageBox.Show("������˴���:" + orderManager.Err);
        //                                this.Err = orderManager.Err;
        //                                return -1;
        //                            }
        //                        }
        //                    }
        //                    strComboNo = order.Combo.ID;
        //                }
        //                if (this.UpdateOther(order) == -1)
        //                    return -1;
        //                //���ҽ��-���շ���
        //                if (orderManager.ConfirmAndExecOrder(order, false, "", dt) == -1)
        //                {
        //                    MessageBox.Show("ҽ����˴���:" + orderManager.Err);
        //                    this.Err = orderManager.Err;
        //                    return -1;
        //                }
        //                #endregion

        //                #region ʵ�ֶ�Σ�ػ��ߵ�״̬����

        //                if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("����") != -1)
        //                {
        //                    if (managerRADT.UpdateCondition_Info("1", patient.ID) < 0)
        //                    {
        //                        this.Err = managerRADT.Err;
        //                        return -1;
        //                    }
        //                }
        //                if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("��Σ") != -1)
        //                {
        //                    if (managerRADT.UpdateCondition_Info("2", patient.ID) < 0)
        //                    {
        //                        this.Err = managerRADT.Err;
        //                        return -1;
        //                    }
        //                }
        //                #endregion
        //            }
        //            else if (order.Status == 3 || order.Status == 4)//���ϵ�
        //            {
        //                if (orderManager.ConfirmOrder(order, false, dt) == -1)
        //                {
        //                    this.Err = orderManager.Err;
        //                    return -1;
        //                }

        //                if (order.Status == 3)
        //                {
        //                    if (this.UpdateOther(order) == -1)
        //                        return -1;//{A921CA7F-6607-406c-9DF2-C2A58C792ED4}

        //                    #region �˷�ҽ���͸���
        //                    if (!hsCombo.Contains(order.Combo.ID))
        //                    {
        //                        ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//��ѯ����

        //                        for (int f = 0; f < alSubtbl.Count; f++)//���Ĵ���
        //                        {
        //                            if (orderManager.ConfirmOrder((FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f], false, dt) == -1)
        //                            {
        //                                this.Err = orderManager.Err;
        //                                return -1;
        //                            }
        //                        }
        //                        if (fee.SaveApply(order.Combo.ID, FS.FrameWork.Function.NConvert.ToInt32(order.Item.User03)) == -1)
        //                        {
        //                            this.Err = fee.Err;
        //                            return -1;
        //                        }

        //                        hsCombo.Add(order.Combo.ID, order.Combo);
        //                    }
        //                    #endregion

        //                    #region ֹͣҽ����ʱ��״̬�����ͨ

        //                    if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("����") != -1
        //                                        || order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("��Σ") != -1)
        //                    {

        //                        if (managerRADT.UpdateCondition_Info("0", patient.ID) != -1)
        //                        {
        //                            this.Err = managerRADT.Err;
        //                            return -1;
        //                        }
        //                    }
        //                    #endregion
        //                }
        //            }
        //            else
        //            {
        //                this.Err = "ҽ���Ѿ������仯����ˢ����Ļ��";
        //                return -1;
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            #region ��ʱҽ��

        //            ManagerFee.MessageType = messType;
        //            FS.HISFC.Models.Order.Inpatient.Order order = alOrders[i] as FS.HISFC.Models.Order.Inpatient.Order;

        //            if (ConfirmShortOrder(order, patient, isNurseCharge, nurseCode, alFeeOrder, alSendDrug, dt, isCharge, iSCDCharge) == -1)
        //            {
        //                return -1;
        //            }

        //            //�˷ѵ��Զ����˷�����
        //            if (order.Status == 3)
        //            {
        //                if (!hsCombo.Contains(order.Combo.ID))
        //                {
        //                    hsCombo.Add(order.Combo.ID, order.Combo);

        //                    if (fee.SaveApply(order.Combo.ID, FS.FrameWork.Function.NConvert.ToInt32(order.Item.User03)) == -1)
        //                    {
        //                        this.Err = fee.Err;
        //                        return -1;
        //                    }
        //                }
        //            }

        //            #endregion
        //        }
        //    }
        //    #endregion

        //    #region �շ�

        //    if (isLong == false && alFeeOrder.Count > 0) //��ʱҽ��
        //    {
        //        fee.MessageType = messType;
        //        if (fee.FeeItem(patient, ref alFeeOrder) == -1)
        //        {
        //            this.Err = fee.Err;
        //            return -1;
        //        }
        //    }

        //    #endregion

        //    //MessageBox.Show("���͵�ҩ������");
        //    //���RecipeNo��ҩ��
        //    System.Collections.Hashtable hsRecipe = new Hashtable();
        //    foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeOrder)
        //    {
        //        if (feeItem.Order.Item.ItemType == EnumItemType.Drug)
        //        {
        //            hsRecipe.Add(feeItem.Order.ID, feeItem);
        //            {
        //                if (!hsRecipe.ContainsKey(feeItem.Order.ID))
        //                {
        //                    hsRecipe.Add(feeItem.Order.ID, feeItem);
        //                }
        //            }
        //        }
        //    }

        //    if (alFeeOrder.Count > 0)
        //    {
        //        foreach (FS.HISFC.Models.Order.Inpatient.Order drugOrder in alSendDrug)
        //        {
        //            //{A8ABA1D3-C025-43d3-A02C-60FFB5A166AF}  ���ж�HashTable���Ƿ����
        //            //������Ϊҩ���շ�ʱ��alFeeOrder�ڲ�����ҩƷ����
        //            if (hsRecipe.ContainsKey(drugOrder.ID))
        //            {
        //                FS.HISFC.Models.Fee.Inpatient.FeeItemList tempFee = hsRecipe[drugOrder.ID] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
        //                drugOrder.ReciptNO = tempFee.RecipeNO;
        //                drugOrder.SequenceNO = tempFee.SequenceNO;
        //            }
        //        }
        //    }

        //    #region ���ͷ�ҩ����

        //    if (alSendDrug.Count > 0)
        //    {
        //        if (this.SendDrugWithOrderList(alSendDrug, isNurseCharge, dt) == -1)
        //        {
        //            return -1;
        //        }
        //    }
        //    #endregion

        //    return 0;
        //}

        /// <summary>
        /// ��˱��棬���ҽ��������ʱҽ�������շѣ�
        /// ��Ҫ��fee����Commit��RollBack����
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alOrders"></param>
        /// <param name="isLong"></param>
        /// <param name="nurseCode"></param>
        /// <param name="quitFee"></param>
        /// <param name="isCharge">�Ƿ��շѣ� Ƿ�ѻ��߿���ֻ���治�շ�</param>
        /// <param name="iSCDCharge">��Ժ��ҩ�Ƿ��жϾ����ߣ� Ƿ�ѻ��߳�Ժ��ҩ���Բ��жϾ�����</param>
        /// <returns></returns>
        public int SaveChecked(FS.HISFC.Models.RADT.PatientInfo patient, ArrayList alOrders, bool isLong, string nurseCode, bool quitFee, bool isCharge, bool iSCDCharge)
        {
            //�շѿ��� �ж���(true)--ҩ����ҩʱ�շ� ����(false)--���/�ֽ�ʱ�շ�
            //True ��ʿվ�շ� False ҩ���շ�
            bool bCharge = GetIsNurseCharge(ref this.trans);

            DateTime dt = orderManager.GetDateTimeFromSysDateTime();

            string strComboNo = "";

            Hashtable hsCombo = new Hashtable();

            //�շ�ҽ��
            ArrayList alFeeOrder = new ArrayList();
            //��Ҫ��ҩҩƷ
            ArrayList alSendDrug = new ArrayList();

            #region ��ӡҽ��������
            //FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //bool val = con.GetControlParam<bool>("B00002");
            //ArrayList itemList = null;
            //if (val)
            //{
            //    FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
            //    itemList = item.SelectAllItemByOrderPrint("1");
            //}
            #endregion

            //�Զ��˷���ʾ��Ϣ ���ڲ��ֲ���ֱ���˷ѵ���ʾ��Ϣ
            Hashtable hsQuitFeeWarning = new Hashtable();

            for (int i = 0; i < alOrders.Count; i++) //����ҽ��
            {
                if (isLong)
                {
                    #region ����ҽ������
                    FS.HISFC.Models.Order.Inpatient.Order order = alOrders[i] as FS.HISFC.Models.Order.Inpatient.Order;

                    if (order.Status == 0)//δ���ҽ��
                    {
                        #region δ���ҽ������

                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug) //ҩƷ
                        {
                            //�Ѿ��޸ķ�ҩ������ҹ��򣬴˴�����ȡ���ڿ���
                            //ִ�п���Ϊ��ʿ���ڿ���
                            //order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();
                            order.Patient.Name = patient.Name;
                        }
                        else//��ҩƷ������ִ�п���
                        {

                        }
                        if (order.Combo.ID != strComboNo)
                        {
                            ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//��ѯ����

                            for (int f = 0; f < alSubtbl.Count; f++)//���Ĵ���
                            {
                                if (((FS.HISFC.Models.Order.Order)alSubtbl[f]).Status == 0)
                                {
                                    if (orderManager.ConfirmAndExecOrder((FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f], false, "", dt) == -1) //�����շѱ��
                                    {
                                        this.Err = orderManager.Err;
                                        return -1;
                                    }
                                }
                            }
                            strComboNo = order.Combo.ID;
                        }
                        if (this.UpdateOther(order) == -1) return -1;
                        //���ҽ��-���շ���
                        if (orderManager.ConfirmAndExecOrder(order, false, "", dt) == -1)
                        {
                            this.Err = orderManager.Err;
                            return -1;
                        }
                        #endregion

                        #region ʵ�ֶ�Σ�ػ��ߵ�״̬����

                        if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("����") != -1)
                        {

                            if (managerRADT.UpdateCondition_Info("1", patient.ID) < 0)
                            {
                                this.Err = managerRADT.Err;
                                return -1;
                            }
                        }
                        if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("��Σ") != -1)
                        {

                            if (managerRADT.UpdateCondition_Info("2", patient.ID) < 0)
                            {
                                this.Err = managerRADT.Err;
                                return -1;
                            }
                        }
                        #endregion
                    }
                    else if (order.Status == 3 || order.Status == 4)//���ϵ�
                    {
                        if (orderManager.ConfirmOrder(order, false, dt) == -1)
                        {
                            this.Err = orderManager.Err;
                            return -1;
                        }

                        if (order.Status == 3)
                        {
                            if (this.UpdateOther(order) == -1)
                                return -1;//{A921CA7F-6607-406c-9DF2-C2A58C792ED4}

                            #region �˷�ҽ���͸���

                            if (!hsCombo.Contains(order.Combo.ID))
                            {
                                ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//��ѯ����

                                for (int f = 0; f < alSubtbl.Count; f++)//���Ĵ���
                                {
                                    if (orderManager.ConfirmOrder((FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f], false, dt) == -1)
                                    {
                                        this.Err = orderManager.Err;
                                        return -1;
                                    }
                                }
                                if (fee.SaveApply(patient, order.Combo.ID, FS.FrameWork.Function.NConvert.ToInt32(order.Item.User03), quitFee, ref hsQuitFeeWarning) == -1)
                                {
                                    this.Err = fee.Err;
                                    return -1;
                                }

                                hsCombo.Add(order.Combo.ID, order.Combo);
                            }
                            #endregion

                            #region ֹͣҽ����ʱ��״̬�����ͨ

                            if (order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("����") != -1
                                                || order.Item.SysClass.ID.ToString() == "UF" && order.Item.Name.IndexOf("��Σ") != -1)
                            {

                                if (managerRADT.UpdateCondition_Info("0", patient.ID) == -1)
                                {
                                    this.Err = managerRADT.Err;
                                    return -1;
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        this.Err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + order.Patient.Name + "\r\n\r\n���:" + order.SubCombNO.ToString() + " " + order.Item.Name + "\r\nҽ���Ѿ������仯����ˢ����Ļ��";
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    #region ��ʱҽ��

                    ManagerFee.MessageType = messType;
                    FS.HISFC.Models.Order.Inpatient.Order order = alOrders[i] as FS.HISFC.Models.Order.Inpatient.Order;

                    if (ConfirmShortOrder(order, patient, bCharge, nurseCode, alFeeOrder, alSendDrug, dt, isCharge, iSCDCharge) == -1)
                    {
                        return -1;
                    }

                    //�˷ѵ��Զ����˷�����
                    if (order.Status == 3)
                    {
                        if (!hsCombo.Contains(order.Combo.ID))
                        {
                            ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//��ѯ����

                            for (int f = 0; f < alSubtbl.Count; f++)//���Ĵ���
                            {
                                if (ConfirmShortOrder((FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f], patient, bCharge, nurseCode, alFeeOrder, alSendDrug, dt, isCharge, iSCDCharge) == -1)
                                {
                                    return -1;
                                }
                            }

                            hsCombo.Add(order.Combo.ID, order.Combo);

                            if (fee.SaveApply(patient, order.Combo.ID, FS.FrameWork.Function.NConvert.ToInt32(order.Item.User03), quitFee, ref hsQuitFeeWarning) == -1)
                            {
                                this.Err = fee.Err;
                                return -1;
                            }
                        }
                    }

                    #endregion
                }
            }

            if (isLong == false && alFeeOrder.Count > 0) //��ʱҽ��
            {
                fee.MessageType = messType;
                if (fee.FeeItem(patient, ref alFeeOrder) == -1)
                {
                    this.Err = fee.Err;
                    return -1;
                }
            }

            //���RecipeNo��ҩ��
            System.Collections.Hashtable hsRecipe = new Hashtable();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeOrder)
            {
                if (feeItem.Order.Item.ItemType == EnumItemType.Drug)
                {
                    if (!hsRecipe.ContainsKey(feeItem.Order.ID))
                    {
                        hsRecipe.Add(feeItem.Order.ID, feeItem);
                    }
                }
            }
            if (alFeeOrder.Count > 0)
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order drugOrder in alSendDrug)
                {
                    //{A8ABA1D3-C025-43d3-A02C-60FFB5A166AF}  ���ж�HashTable���Ƿ����
                    //������Ϊҩ���շ�ʱ��alFeeOrder�ڲ�����ҩƷ����
                    if (hsRecipe.ContainsKey(drugOrder.ID))
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList tempFee = hsRecipe[drugOrder.ID] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                        drugOrder.ReciptNO = tempFee.RecipeNO;
                        drugOrder.SequenceNO = tempFee.SequenceNO;
                    }
                }
            }

            if (alSendDrug.Count > 0)
            {
                if (this.SendDrugWithOrderList(alSendDrug, bCharge, dt) == -1)
                {
                    return -1;
                }
            }

            //�˴������Զ��˷�ʱ����ʾ��Ϣ
            //������Ŀ�����շѿ��Ҳ�һ�£�����ֻ�������룬�������ʾ
            if (hsQuitFeeWarning.Count > 0)
            {
                this.Err = "������ĿΪ�˷�����״̬������ϵ��ؿ���ȷ���˷ѣ�\r\n";
                foreach (string info in hsQuitFeeWarning.Keys)
                {
                    this.Err = this.Err + "\r\n��" + info + "��";
                }
            }

            return 0;
        }


        /// <summary>
        /// ҩƷ���뷢��
        /// 
        /// {F766D3A5-CC25-4dd7-809E-3CBF9B152362}  ���һ��ҽ���ֽ�Ŀ��ͳһԤ��
        /// </summary>
        /// <param name="execOrderList"></param>
        /// <param name="bCharge"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected int SendDrug(ArrayList execOrderList, bool bCharge, DateTime dt)
        {
            ArrayList al = new ArrayList();
            foreach (FS.HISFC.Models.Order.ExecOrder info in execOrderList)
            {
                al.Add(info.Order);
            }
            if (mySendExecDrug(al, bCharge, dt, execOrderList) == -1)
                return -1;

            return 1;

        }

        /// <summary>
        /// ҩƷ���뷢��
        /// 
        /// {BA8B6888-3114-4575-8CD9-AA09DBA1A954}  ���һ��ҽ����˷��͵Ŀ��ͳһԤ��
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="bCharge"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected int SendDrugWithOrderList(ArrayList orderList, bool bCharge, DateTime dt)
        {
            List<FS.HISFC.Models.Order.ExecOrder> execOrderCollection = new List<FS.HISFC.Models.Order.ExecOrder>();
            ArrayList ordList = new ArrayList();//{4828DBC4-F5CA-4f1c-A3DE-CEBBAD646910}
            ArrayList exeOrdList = new ArrayList();//{4828DBC4-F5CA-4f1c-A3DE-CEBBAD646910}
            foreach (FS.HISFC.Models.Order.Inpatient.Order info in orderList)
            {
                if (info.Item.ItemType == EnumItemType.Drug) //ҩƷ-��Ҫ��ҩ
                {
                    ArrayList al = orderManager.QueryExecOrderByOneOrder(info.ID, "1");
                    if (al == null || al.Count == 0)
                    {
                        this.Err = "��ѯҩƷִ�е�����" + orderManager.Err;
                        return -1;
                    }

                    foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in al)
                    {
                        if (exeOrder.ID == info.User03)
                        {
                            exeOrder.Order.ReciptNO = info.ReciptNO;
                            exeOrder.Order.SequenceNO = info.SequenceNO;
                        }
                        //add by houwb ����ҩ���ĵ�λ����...
                        ((FS.HISFC.Models.Pharmacy.Item)exeOrder.Order.Item).PackUnit = ((FS.HISFC.Models.Pharmacy.Item)info.Item).PackUnit;

                        execOrderCollection.Add(exeOrder);
                        exeOrdList.Add(exeOrder);
                    }
                    ordList.Add(info);//{4828DBC4-F5CA-4f1c-A3DE-CEBBAD646910}
                }
            }
            if (mySendExecDrug(ordList, bCharge, dt, exeOrdList) == -1)//{4828DBC4-F5CA-4f1c-A3DE-CEBBAD646910}
            {
                return -1;
            }

            return ManagerPharmacy.InpatientDrugPreOutNum(execOrderCollection, dt, false);
        }


        /// <summary>
        /// ���·���ҩƷ���뺯��
        /// </summary>
        /// <param name="orderList"></param>
        /// <param name="bCharge"></param>
        /// <param name="dt"></param>
        /// <param name="al"></param>
        /// <returns></returns>
        private int mySendExecDrug(ArrayList orderList, bool bCharge, DateTime dt, ArrayList al)
        {
            ArrayList myList = new ArrayList();

            if (hsDepts.Count <= 0)
            {
                Manager mangerIntegrate = new Manager();

                ArrayList alDepts = mangerIntegrate.GetDepartment();

                foreach (FS.HISFC.Models.Base.Department dept in alDepts)
                {
                    if (!hsDepts.Contains(dept.ID))
                    {
                        hsDepts.Add(dept.ID, dept);
                    }
                }
            }

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Order.ExecOrder order = al[i] as FS.HISFC.Models.Order.ExecOrder;

                #region ��������ҽ������

                //����������ҽ������houwb �ᵽ���洦������
                //���������ҵ��������ԣ���ҩ��ApplyOut���ã���ʾ����Ƶ��������
                //��ȡҩ����
                if (hsDepts.Contains(order.Order.ReciptDept.ID))
                {
                    if ("1,2".Contains(((FS.HISFC.Models.Base.Department)hsDepts[order.Order.ReciptDept.ID]).SpecialFlag))
                    {
                        order.Order.ReciptDept = hsDepts[order.Order.ReciptDept.ID] as FS.HISFC.Models.Base.Department;
                    }
                }
                else
                {
                    FS.HISFC.Models.Base.Department newDept = new Department();
                    newDept.ID = order.Order.ReciptDept.ID;
                    newDept.Name = order.Order.ReciptDept.Name;
                    order.Order.ReciptDept = newDept;
                }

                #endregion

                int iSendFlag = -1;//���ͱ��
                /*ȡ���ҷ�ҩ���*/
                iSendFlag = 2;//��ʱ����

                order.DrugFlag = iSendFlag;//0,δ���ͣ�1 ���з��� 2 ��ʱ����
                if (order.Order.OrderType.IsNeedPharmacy && bCharge) //��Ҫ��ҩ���Ѿ��շ�
                {
                    if (order.Order.OrderType.ID == "QL" || order.Order.OrderType.ID == "CD")//��Ժ��ҩ����ٴ�ҩ��ʱ����
                    {
                        order.DrugFlag = 2;
                    }
                    else
                    {
                        order.DrugFlag = iSendFlag;
                        order.IsCharge = bCharge;
                    }
                    myList.Add(order);

                }
                else if (order.Order.OrderType.IsNeedPharmacy == false)//����Ҫ��ҩ��ҩƷ
                {
                    order.DrugFlag = 3;//�Ѿ���
                }
                else //��Ҫ��ҩ��δ�շ�
                {
                    order.DrugFlag = 2;
                    myList.Add(order);
                }
            }
            if (myList.Count != 0)
            {
                if (SendToDrugStore(myList, dt) == -1)
                {
                    return -1;
                }
            }

            foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in al)
            {
                //��ִ�з�ҩ���
                if (orderManager.SetDrugFlag(exeOrder.ID, exeOrder.DrugFlag) == -1)
                {
                    this.Err = orderManager.Err;

                    return -1;
                }
                //if (hsOrderExecSeq.Contains(exeOrder.ID))
                //{
                //    hsOrderExecSeq[exeOrder.ID] = exeOrder.DrugFlag;
                //}

                //if (hsApplyExecSeq[exeOrder.ID] != null)
                //{
                //    string[] seq = hsApplyExecSeq[exeOrder.ID].ToString().Split('|');
                //    for (int i = 0; i < seq.Length; i++)
                //    {
                //        if (!string.IsNullOrEmpty(seq[i].ToString())
                //            && seq[i].ToString() != exeOrder.ID)
                //        {
                //            if (orderManager.SetDrugFlag(seq.ToString(), FS.FrameWork.Function.NConvert.ToInt32(hsOrderExecSeq[exeOrder.ID])) == -1)
                //            {
                //                this.Err = orderManager.Err;

                //                return -1;
                //            }
                //        }
                //    }
                //}
            }
            return 0;
        }


        /// <summary>
        /// ���ȷ��
        /// </summary>
        /// <param name="order"></param>
        /// <param name="patient"></param>
        /// <param name="isNurseCharge">�Ƿ�ʿվ�շѣ�������ҩ����ҩʱ�շ�</param>
        /// <param name="nurseCode"></param>
        /// <param name="alFeeOrder"></param>
        /// <param name="alSendDrug"></param>
        /// <param name="dt"></param>
        /// <param name="isCharge">�Ƿ��շѣ� Ƿ�ѻ��߿���ֻ���治�շ�</param>
        /// <returns></returns>
        protected int ConfirmShortOrder(FS.HISFC.Models.Order.Inpatient.Order order, FS.HISFC.Models.RADT.PatientInfo patient, bool isNurseCharge, string nurseCode, ArrayList alFeeOrder, ArrayList alSendDrug, DateTime dt, bool isCharge, bool iSCDCharge)
        {
            //�Ż��Ĺ�ϣ����Ҫ���...houwb
            htDrug.Clear();
            htItem.Clear();
            //htStorage.Clear();

            //��ȡ��ִ�е���ˮ��,���շѵ�ͬʱ����ִ�е���ˮ��
            string execId = orderManager.GetNewOrderExecID();

            if (execId == "" || execId == "-1")
            {
                return -1;
            }

            //bool myCharge = false;
            bool mySendDrug = false;

            try
            {
                if (order.Status == 0)
                {
                    order.Patient = patient;//�������¸�ֵ

                    bool isNeedConfirm = false;

                    if (order.OrderType.IsCharge) //�շ�ҽ��
                    {
                        if (order.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))//��ҩƷ��ѯ�ն�ȷ�ϱ��
                        {
                            #region ��ҩƷ
                            string err = "";
                            if (FillFeeItem(ref order, out err, 1) == -1)
                            {
                                this.Err = err;
                                return -1;
                            }
                            FeeUndrug(order, patient, nurseCode, alFeeOrder, execId, isCharge, ref isNeedConfirm);
                            #endregion

                        }
                        else //ҩƷ--�����Ƿ��շѽ����շ�
                        {
                            #region ҩƷ
                            //ִ�п���Ϊ��ʿ���ڿ���
                            //ִ�п��Ҳ������»�ȡ������ȡҩ���ң��Ѿ��޸�������ҵĹ���
                            //order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();//((FS.HISFC.Models.RADT.Person)feeManagement.Operator).Dept.Clone();
                            if (isNurseCharge) //�Ƿ�ʿվ�շ�
                            {
                                string err = "";
                                if (FillPharmacyItem(ref order, out err) == -1)
                                {
                                    this.Err = err;
                                    return -1;
                                }
                                string strProperty = orderManager.GetDrugProperty(order.Item.ID,
                                    ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm.ID,
                                    order.Patient.PVisit.PatientLocation.Dept.ID);

                                if (strProperty == "0")	//���ɲ�֣����ȡ��
                                {
                                    order.Qty = (decimal)System.Math.Ceiling((double)order.Qty);
                                }

                                if (order.ExeDept == null || order.ExeDept.ID == "")
                                {
                                    order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();//order.NurseStation;
                                }
                                order.User03 = execId;

                                //if (isCharge)
                                if (isCharge || (!iSCDCharge && order.OrderType.ID.Equals("CD"))) //Editor by liuww ��Ժ��ҩ���ж�
                                {
                                    if (IsFee(patient, order))
                                    {
                                        mySendDrug = true;
                                        //myCharge = true;
                                        //��ӵ��շ���Ŀ����
                                        order.Oper.OperTime = dt;
                                        alFeeOrder.Add(order);
                                    }
                                    else //���շѣ����շ�
                                    {
                                        mySendDrug = true;
                                        //myCharge = false;
                                    }
                                }
                                else
                                {
                                    mySendDrug = false;
                                    //myCharge = false;
                                }
                            }
                            else
                            {
                                //if (isCharge) 
                                if (isCharge || (!iSCDCharge && order.OrderType.ID.Equals("CD"))) //Editor by liuww ��Ժ��ҩ���ж�
                                {
                                    mySendDrug = true;
                                    //myCharge = false;
                                }
                                else
                                {
                                    mySendDrug = false;
                                    //myCharge = false;
                                }
                            }

                            #endregion
                        }
                    }
                    else //���շ���Ŀ
                    {
                        //����ҩ�����շ�
                    }

                    #region ���ҽ��

                    if (this.UpdateOther(order) == -1)
                    {
                        return -1;
                    }

                    //{FE127946-53ED-4bec-8223-45AAE5398C6C} Ϊ�˴���ͬ������
                    if (order.Item.ItemType == EnumItemType.Drug)             //��ʿվ���շ� �� ��ĿΪҩƷ
                    {
                        //��ʿվ���շѻ���Ƿ�Ѳ��շ�ʱ��ҩƷΪ��Ҫȷ��״̬
                        if (!isNurseCharge
                            || (!isCharge && iSCDCharge) 
                            || (!isCharge && !iSCDCharge 
                                && !order.OrderType.ID.Equals("CD")))
                        {
                            isNeedConfirm = true;
                        }
                    }
                    else  //��ҩƷ��Ƿ���򲻼��ˣ���isNeedConfirm����⣬��չ��Ĵ�������⣩
                    {
                        //isNeedConfirm = !isCharge;
                    }

                    //if (orderManager.ConfirmAndExecOrder(order, isNurseCharge, execId, dt) == -1) //����ִ�е����
                    if (orderManager.ConfirmAndExecOrder(order, !isNeedConfirm, execId, dt) == -1) //����ִ�е����
                    {
                        this.Err = orderManager.Err;
                        return -1;
                    }
                    #endregion

                    #region ���ͷ�ҩ����
                    if (mySendDrug)
                    {
                        alSendDrug.Add(order);
                    }
                    #endregion

                    #region ����

                    #region ҩƷ�����Ƿ���ҩ����ҩʱ�Ʒѣ���ͬҩƷ��ƷѲ���һ��ʹ��

                    bool bChargeSubtbl = true;
                    if (!isNurseCharge && order.Item.ItemType == EnumItemType.Drug) //��ʿվ���Ʒ�
                    {
                        FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                        //ҩƷ��Ʒ�ʱ��ҩƷ�����Ƿ���ҩƷͬʱ�Ʒ� 1 ��ʿվ�Ʒ� 0 ҩ���Ʒ�
                        bChargeSubtbl = con.GetControlParam<bool>("200050", false, true);
                    }
                    #endregion

                    ArrayList alSubtbl = orderManager.QuerySubtbl(order.Combo.ID);//���Ĵ���

                    //{C05D5AB9-1ED9-4510-A70C-4E4D131CEA73} �޸���ʱҽ�������������Ŀ��ʱ����շѿ�ʼ
                    FS.HISFC.Models.Order.Inpatient.Order obj = null;
                    for (int f = 0; f < alSubtbl.Count; f++)
                    {
                        obj = (FS.HISFC.Models.Order.Inpatient.Order)alSubtbl[f];
                        string err = string.Empty;

                        if (FillFeeItem(ref obj, out err, 1) == -1)
                        {
                            this.Err = err;
                            return -1;
                        }
                        if (obj.Status == 0) //û�����Ҫ�շ�
                        {
                            /*******��Ŀ���********/
                            string execIdSub = orderManager.GetNewOrderExecID();
                            if (execIdSub == "" || execIdSub == "-1")
                            {
                                this.Err = "��ø���ִ����ˮ�ų���!" + orderManager.Err;
                                return -1;
                            }

                            #region �����շ�

                            #region ��ҩƷ (��ҩƷҪ�����ն�ȷ�ϵ�������ն�ȷ����Ŀ�ĸ���Ҳ����Ҫ�ն�ȷ���շѣ�

                            if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                FeeUndrug(obj, patient, nurseCode, alFeeOrder, execIdSub, isCharge, ref isNeedConfirm);
                            }
                            #endregion

                            #region ҩƷ
                            else
                            {
                                if (bChargeSubtbl)
                                {
                                    if (((FS.HISFC.Models.Fee.Item.Undrug)obj.Item).UnitFlag == "1")
                                        //order.Order.Unit == "[������]")
                                    {
                                        ArrayList al = managerPack.QueryUndrugPackagesBypackageCode(obj.Item.ID);
                                        if (al == null)
                                        {
                                            this.Err = "���ϸ�����" + managerPack.Err;

                                            return -1;
                                        }

                                        FS.HISFC.Models.Order.Inpatient.Order myorder = null;

                                        decimal rate = 1;
                                        foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in al)
                                        {
                                            myorder = new FS.HISFC.Models.Order.Inpatient.Order();
                                            decimal qty = obj.Qty;
                                            myorder = obj.Clone();
                                            myorder.Patient = patient.Clone();
                                            myorder.Name = undrug.Name;
                                            myorder.Item.Name = undrug.Name;

                                            myorder.Item = undrug.Clone();
                                            myorder.Qty = qty * undrug.Qty;//����==������Ŀ����*С��Ŀ����
                                            myorder.Item.Qty = qty * undrug.Qty;//����==������Ŀ����*С��Ŀ����
                                            myorder.Oper = obj.Oper.Clone();
                                            myorder.Oper.OperTime = dt;
                                            myorder.User03 = execIdSub;

                                            rate = fee.GetItemRateForZT(obj.Item.ID, undrug.ID);
                                            if (FillFeeItem(ref myorder, out err, rate) == -1)
                                            {
                                                this.Err = err;
                                                return -1;
                                            }
                                            #region {92A0CF31-27BC-472e-9C15-1ED2C8A81F36} by zhang.xs 2010.10.19

                                            //if (myorder.Item.Price > 0)
                                            if (this.IsFee(patient, myorder) && bChargeSubtbl)
                                            {
                                                alFeeOrder.Add(myorder);
                                            }

                                            #endregion

                                        }
                                    }
                                    else
                                    {
                                        if (FillFeeItem(ref obj, out err, 1) == -1)
                                        {
                                            this.Err = err;
                                            return -1;
                                        }
                                        obj.Patient = patient.Clone();


                                        obj.User03 = execIdSub;
                                        if (obj.Item.Price >= 0)
                                        {
                                            if (IsFee(patient, obj) && bChargeSubtbl)
                                            {
                                                obj.Oper.OperTime = dt;
                                                alFeeOrder.Add(obj); //�շ�
                                            }
                                            else
                                            {
                                                //���շ�
                                            }
                                        }
                                    }
                                }
                                isNeedConfirm = false;
                                if (!bChargeSubtbl
                                    || !isCharge)
                                {
                                    isNeedConfirm = true;
                                }
                            }
                            #endregion

                            #endregion

                            if (orderManager.ConfirmAndExecOrder(obj, !isNeedConfirm, execIdSub, dt) == -1)//���±��
                            {
                                this.Err = orderManager.Err;
                                return -1;
                            }
                        }
                    }
                    #endregion
                }
                else if (order.Status == 3) //����ҽ��
                {
                    if (orderManager.ConfirmOrder(order, false, dt) == -1)
                    {
                        this.Err = orderManager.Err;
                        return -1;
                    }

                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                    {
                        if (orderManager.DcExecImmediate(order, FS.FrameWork.Management.Connection.Operator) < 0)
                        {
                            this.Err = orderManager.Err;
                            return -1;
                        }
                    }

                    //foreach(
                }
                else
                {
                    this.Err = "ҽ���Ѿ������仯����ˢ����Ļ��";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ������" + ex.Message;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// סԺ���ҽ�����������ն�ȷ����Ŀ�Ƿ��շ�
        /// 0��������Ŀ��˲��շѣ��ն�ȷ����Ŀ��˲��շ�
        /// 1��������Ŀ����շѣ��ն�ȷ����Ŀ����շѣ�������ִ����Ŀ��
        /// 2��������Ŀ��˲��շѣ��ն�ȷ����Ŀ����շѣ�������ִ����Ŀ��
        /// 3��������Ŀ����շѣ��ն�ȷ����Ŀ��˲��շ�
        /// </summary>
        private int isCheckConfirmModel = -1;

        /// <summary>
        /// סԺ���ҽ�����������ն�ȷ����Ŀ�Ƿ��շ�
        /// 0��������Ŀ��˲��շѣ��ն�ȷ����Ŀ��˲��շ�
        /// 1��������Ŀ����շѣ��ն�ȷ����Ŀ����շѣ�������ִ����Ŀ��
        /// 2��������Ŀ��˲��շѣ��ն�ȷ����Ŀ����շѣ�������ִ����Ŀ��
        /// 3��������Ŀ����շѣ��ն�ȷ����Ŀ��˲��շ�
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool CheckCharge(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order.Item.ID == "999" || !order.OrderType.IsCharge)
            {
                return true;
            }

            if (this.isCheckConfirmModel == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                isCheckConfirmModel = con.GetControlParam<int>("HNZY10", true, 1);
            }
            if (order.Item.ID != "999")
            {
                //���ڴ��ҽ��״̬�����⣬�����������ʱ���ٴ򿪣�2013��1��9��18:07:00
                //if (!order.IsSubtbl)
                //{
                ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm = this.fee.GetItem(order.Item.ID).IsNeedConfirm;
                //}
            }

            FS.HISFC.Models.Base.Department execDept = deptMangager.GetDeptmentById(order.ExeDept.ID);

            switch (isCheckConfirmModel)
            {
                //0��������Ŀ��˲��շѣ��ն�ȷ����Ŀ��˲��շ�
                case 0:
                    if (order.Item.SysClass.ID.ToString() == "UO")
                    {
                        return false;
                    }
                    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm)
                    {
                        return false;
                    }
                    break;
                //1��������Ŀ����շѣ��ն�ȷ����Ŀ����շѣ�������ִ����Ŀ��
                case 1:
                    if (order.Item.SysClass.ID.ToString() == "UO")
                    {
                        return true;
                    }
                    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm)
                    {
                        if (order.ExeDept.ID == ((FS.HISFC.Models.Base.Employee)this.managerRADT.Operator).Dept.ID
                        || (execDept != null && execDept.DeptType.ID.ToString() == "OP")
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.Dept.ID
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.NurseCell.ID) 
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                //2��������Ŀ��˲��շѣ��ն�ȷ����Ŀ����շѣ�������ִ����Ŀ��
                case 2:
                    if (order.Item.SysClass.ID.ToString() == "UO")
                    {
                        return false;
                    }
                    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm)
                    {
                        if (order.ExeDept.ID == ((FS.HISFC.Models.Base.Employee)this.managerRADT.Operator).Dept.ID
                        || (execDept != null && execDept.DeptType.ID.ToString() == "OP")
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.Dept.ID
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.NurseCell.ID)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                //3��������Ŀ����շѣ��ն�ȷ����Ŀ��˲��շ�
                case 3:

                    if (order.Item.SysClass.ID.ToString() == "UO")
                    {
                        return true;
                    }
                    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm)
                    {
                        return false;
                    }
                    break;
                default:
                    if (order.Item.SysClass.ID.ToString() == "UO")
                    {
                        return true;
                    }
                    if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm)
                    {
                        if (order.ExeDept.ID == ((FS.HISFC.Models.Base.Employee)this.managerRADT.Operator).Dept.ID
                        || (execDept != null && execDept.DeptType.ID.ToString() == "OP")
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.Dept.ID
                        || order.ExeDept.ID == patient.PVisit.PatientLocation.NurseCell.ID)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
            }
            return true;
        }

        /// <summary>
        /// ��˷�ҩƷ�Ʒ�
        /// </summary>
        /// <param name="order"></param>
        /// <param name="patient"></param>
        /// <param name="nurseCode"></param>
        /// <param name="alFeeOrder"></param>
        /// <param name="execId"></param>
        /// <param name="isCharge">�Ƿ��շѣ�Ƿ�ѻ��߿���ֻ���治�շ�</param>
        /// <param name="isNeedConfirm"></param>
        private void FeeUndrug(FS.HISFC.Models.Order.Inpatient.Order order, FS.HISFC.Models.RADT.PatientInfo patient, string nurseCode, ArrayList alFeeOrder, string execId, bool isCharge, ref bool isNeedConfirm)
        {
            FS.HISFC.Models.Base.Department execDept = deptMangager.GetDeptmentById(order.ExeDept.ID);

            if (execDept == null)
            {
                execDept = new Department();
            }

            //��������ҽ���������շѴ��� ,����ִ�п���Ϊ�����ҵģ�ֱ���շ�
            //if ((order.Item.SysClass.ID.ToString() != "UO" &&
            //        ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm == false) ||
            //    (order.Item.SysClass.ID.ToString() == "UO"
            //        && (order.ExeDept.ID == ((FS.HISFC.Models.Base.Employee)this.managerRADT.Operator).Dept.ID
            //        || execDept.DeptType.ID.ToString() == "OP"
            //        || order.ExeDept.ID == patient.PVisit.PatientLocation.Dept.ID
            //        || order.ExeDept.ID == patient.PVisit.PatientLocation.NurseCell.ID)
            //    ))//������ҽ��

            isNeedConfirm = true;

            if (order.Item.ID == "999" || !order.OrderType.IsCharge)
            {
                isNeedConfirm = false;
            }

            if (isCharge)
            {
                //if (order.Item.ID != "999")
                //{
                //    ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm = this.fee.GetItem(order.Item.ID).IsNeedConfirm;
                //}
                ////��������Ŀ
                //if ((order.Item.SysClass.ID.ToString() != "UO"
                //    && ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm == false)

                //    //������Ŀ
                //    || ((order.Item.SysClass.ID.ToString() == "UO" && isUOAutoConfirmed == 1) &&
                //        (order.ExeDept.ID == ((FS.HISFC.Models.Base.Employee)this.managerRADT.Operator).Dept.ID
                //        || execDept.DeptType.ID.ToString() == "OP"
                //        || order.ExeDept.ID == patient.PVisit.PatientLocation.Dept.ID
                //        || order.ExeDept.ID == patient.PVisit.PatientLocation.NurseCell.ID)))

                ////��ȡ����Ŀǰ������Ŀ��Ҫô���������ѣ�ҩƷ�������ն�ȷ��

                if (CheckCharge(patient, order))
                {
                    isNeedConfirm = false;
                    if (order.OrderType.IsCharge == false && order.IsSubtbl == false)
                    {
                        //ҽ�������У����Ǹ��ĵĲ��շѡ�
                    }
                    else if (!IsFee(patient, order))   //order.Item.Price <= 0  /*&& !������Ŀ*/)
                    {
                        //���Ǹ�����Ŀ���۸�С����Ĳ��շ�
                    }
                    else//�շ�
                    {
                        #region ����Ǹ�����Ŀ�����ϸ��
                        if (((FS.HISFC.Models.Fee.Item.Undrug)order.Item).UnitFlag == "1")
                        {
                            /*�����*/
                            ArrayList al = managerPack.QueryUndrugPackagesBypackageCode(order.Item.ID);
                            if (al == null)
                            {
                                this.Err = "���ϸ�����" + managerPack.Err;

                                return;
                            }

                            decimal rate = 1;
                            foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in al)
                            {
                                FS.HISFC.Models.Order.Inpatient.Order myorder = null;
                                decimal qty = order.Qty;
                                myorder = order.Clone();
                                myorder.Name = undrug.Name;

                                myorder.Item = undrug.Clone();
                                myorder.Qty = qty * undrug.Qty;//����==������Ŀ����*С��Ŀ����
                                myorder.Item.Qty = qty * undrug.Qty;//����==������Ŀ����*С��Ŀ����

                                #region {10C9E65E-7122-4a89-A0BE-0DF62B65C647} д�븴����Ŀ���롢����

                                myorder.Package.ID = order.Item.ID;
                                myorder.Package.Name = order.Item.Name;
                                myorder.Package.Qty = qty;

                                #endregion

                                rate = fee.GetItemRateForZT(order.Item.ID, undrug.ID);
                                string err = "";
                                if (FillFeeItem(ref myorder, out err, rate) == -1)
                                {
                                    this.Err = err;
                                    return;
                                }
                                //������Ŀ�ڷ��ñ�û�м�¼ִ����ˮ�� add by houwb 2011-4-7
                                myorder.User03 = execId;
                                if (IsFee(patient, myorder))
                                {
                                    //��ӵ��շ���Ŀ����
                                    myorder.Oper.OperTime = orderManager.GetDateTimeFromSysDateTime();
                                    if (this.IsFee(patient, myorder)) // myorder.Item.Price > 0)
                                    {
                                        alFeeOrder.Add(myorder);
                                    }
                                }
                                else
                                {
                                    /*���շ�*/
                                }
                            }
                        }
                        else
                        {
                            #region �շ�

                            if (order.ExeDept.ID == "")//ִ�п���Ĭ��
                            {
                                order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();//order.NurseStation;
                            }
                            order.User03 = execId;//execOrderID
                            if (IsFee(patient, order))
                            {
                                //��ӵ��շ���Ŀ����
                                order.Oper.OperTime = orderManager.GetDateTimeFromSysDateTime();
                                alFeeOrder.Add(order);
                            }
                            else
                            {
                                /*���շ�*/
                            }

                            #endregion
                        }
                        #endregion
                    }
                }
            }
        }

        /// <summary>
        /// ����ҽ�����»��߸���״̬
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        protected int UpdateOther(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order.Status == 0)//{A921CA7F-6607-406c-9DF2-C2A58C792ED4}
            {
                if (IsUpdateOther == false) return 0;
                if (order.Item.SysClass.ID.ToString() == "MRD")//ת��
                {
                    if (order.ExeDept == null || order.ExeDept.ID == order.Patient.PVisit.PatientLocation.Dept.ID) return 0;//�Լ����ҵĲ���
                    FS.HISFC.Models.RADT.Location newDept = new FS.HISFC.Models.RADT.Location();
                    newDept.Dept = order.ExeDept.Clone();
                    newDept.Memo = order.Memo;
                    if (managerRADT.TransferPatientApply(order.Patient.Clone(), newDept, false, "1") == -1)
                    {
                        this.Err = managerRADT.Err;
                        return -1;
                    }
                }
                else if (order.Item.SysClass.ID.ToString() == "UN")//����
                {
                    //{36E3CA9D-FD23-42b5-802E-C365C04D93A0}
                    if (order.Item.Name.IndexOf("������") >= 0 
                        || order.Item.Name.IndexOf("�ػ�") >= 0
                        || order.Item.Name.IndexOf("��Σ") >= 0 
                        || order.Item.Name.IndexOf("��֢") >= 0)//�жϻ�����û�취
                    {
                        if (managerRADT.UpdatePatientTend(order.Patient.ID, order.Item.Name) == -1)
                        {
                            this.Err = managerRADT.Err;
                            return -1;
                        }

                    }
                }
                else if (order.Item.SysClass.ID.ToString() == "MF")//��ʳ	his
                {
                    if (managerRADT.UpdatePatientFood(order.Patient.ID, order.Item.Name) == -1)
                    {
                        this.Err = managerRADT.Err;
                        return -1;
                    }
                }
                //��ʱ��没Σ�没�ز�һ���ǲ���ҽ��
                //else if (order.Item.SysClass.ID.ToString() == "UF")//{C9F9006D-AE0A-4e73-9ECE-68265A7A583E} 
                //{
                //    int flag = 0;
                //    switch (order.Item.Name)
                //    {
                //        case "����":
                //            flag = 1;
                //            break;
                //        case "��Σ":
                //            flag = 2;
                //            break;
                //    }
                //    managerRADT.UpdateCondition_Info(flag.ToString(), order.Patient.ID);
                //}

                if (order.Item.Name.Contains("����"))
                {
                    managerRADT.UpdateCondition_Info("1", order.Patient.ID);
                }
                else if (order.Item.Name.Contains("��Σ"))
                {
                    managerRADT.UpdateCondition_Info("2", order.Patient.ID);
                }
            }
            else if (order.Status == 3)//{A921CA7F-6607-406c-9DF2-C2A58C792ED4}
            {
                if (order.Item.SysClass.ID.ToString() == "UN")//����
                {
                    //{36E3CA9D-FD23-42b5-802E-C365C04D93A0}
                    if (order.Item.Name.IndexOf("������") >= 0 || order.Item.Name.IndexOf("�ػ�") >= 0)//�жϻ�����û�취
                    {
                        if (managerRADT.UpdatePatientTend(order.Patient.ID, "") == -1)
                        {
                            this.Err = managerRADT.Err;
                            return -1;
                        }

                    }
                }
            }
            return 0;
        }
        //{2AFC76CB-3353-4865-AEB4-AFBEE09DD1D7}
        /// <summary>
        /// ������ҽ����Ŀ��������Ҫ��ӡҽ��������Ŀ���ж�
        /// </summary>
        /// <param name="tag">��ĿID</param>
        /// <returns></returns>
        private bool JudgeInsertOrderPrint(ArrayList itemList, string tag)
        {
            if (itemList != null && itemList.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in itemList)
                {
                    if (undrug.ID == tag)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region ��û�ʿվ�շ���Ϣ
        /// <summary>
        /// �Ƿ�ʿվ��ҩƷ
        /// </summary>
        /// <param name="t"></param>
        /// <returns>����True ˵��ʹ��ִ�С��۷ѷֿ����� ��ʿվ�Ʒ� ����False ˵��ִ��ʱ�۷�</returns>
        public static bool GetIsNurseCharge(ref System.Data.IDbTransaction t)
        {
            //FS.FrameWork.Management.ControlParam controler = new FS.FrameWork.Management.ControlParam();
            //if(t!=null) controler.SetTrans(t);
            //if (controler.QueryControlerInfo("100003") == "1") //��ҩ�շѷֿ���
            //{
            //    return true;
            //}
            //else //ҩ���շ�
            //{
            //    return false;
            //}
            if (t != null)
            {
                CtrlIntegrate.SetTrans(t);
            }
            //����True ˵��ʹ��ִ�С��۷ѷֿ����� ��ʿվ�Ʒ� ����False ˵��ִ��ʱ�۷�
            return CtrlIntegrate.GetControlParam<bool>(SysConst.Use_Drug_ApartFee, false, true);
        }

        #endregion

        #region ҽ������

        public FS.HISFC.BizProcess.Integrate.Fee fee = new Fee();
        Hashtable hsOrderTemp = new Hashtable();

        /// <summary>
        /// �洢��ҩ�����е�ִ�е���ˮ�Ŷ�Ӧ���е�ִ�е���ˮ�ţ�����ҩ���洢����ִ�е���ҩ���
        /// </summary>
        //Hashtable hsApplyExecSeq = new Hashtable();

        /// <summary>
        /// �洢���е�ִ�е���ˮ��
        /// </summary>
        //Hashtable hsOrderExecSeq = new Hashtable();

        /// <summary>
        /// �ֽⱣ��ҽ��
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="alExecOrder"></param>
        /// <param name="nurseCode"></param>
        /// <param name="dt"></param>
        /// <param name="isPharmacy"></param>
        /// <param name="isCharge">�Ƿ��շѣ� Ƿ�ѻ��߿���ֻ���治�շ�,Ϊfalse</param>
        /// <param name="isCharge">�Ƿ���ȷ���շѣ�true Ϊִ�е�ȷ���շѣ�falseΪҽ���ֽⱣ��</param>
        /// <returns></returns>
        public int ComfirmExec(FS.HISFC.Models.RADT.PatientInfo patient,
            List<FS.HISFC.Models.Order.ExecOrder> alExecOrder, string nurseCode, DateTime dt, bool isPharmacy,
            bool isCharge, bool isOrderConfirmFee)
        {
            //�Ż��Ĺ�ϣ����Ҫ���...houwb
            htDrug.Clear();
            htItem.Clear();
            //htStorage.Clear();

            //hsApplyExecSeq.Clear();
            //hsOrderExecSeq.Clear();

            //True ��ʿվ�շ� False ҩ���շ�
            bool isNurseCharge = GetIsNurseCharge(ref this.trans); //�Ƿ�ʿվ�շ�
            ArrayList alChargeOrders = new ArrayList();
            ArrayList alDrugSendOrders = new ArrayList(); //��ҩҽ��

            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemLists = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
            FS.HISFC.BizLogic.Fee.InPatient inpatient = new FS.HISFC.BizLogic.Fee.InPatient();
            int iReturn = 0;
            FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            string NoteNo = "";

            //��Դ�������Ĵ��룬�����ۼ���ҩ��
            FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new Pharmacy();

            #region ҩƷ

            if (isPharmacy)
            {
                //�洢��һ�����뷢ҩ������ִ�е���ˮ��
                //string applyOutExecSeq = "";

                foreach (FS.HISFC.Models.Order.ExecOrder order in alExecOrder)
                {
                    string deptCode = order.Order.StockDept.ID;

                    if (order.Order.Item.ID != "999" && order.Order.OrderType.IsCharge)//���Զ�����Ŀ�����շ���Ŀ����ȡ��Ϣ
                    {
                        #region �����Ŀ��Ϣ ������Ч��
                        FS.HISFC.Models.Order.Inpatient.Order o = order.Order;
                        string err = "";

                        if (FillPharmacyItemWithStockDept(ref o, out err) == -1)
                        {
                            this.Err = err;
                            return -1;
                        }
                        #endregion
                    }

                    order.Order.StockDept.ID = deptCode;

                    order.Order.Patient = patient;
                    order.Order.ExecOper.Dept = order.Order.ExeDept.Clone();

                    #region �շ�

                    if (order.Order.OrderType.IsCharge)
                    {
                        if (isCharge)
                        {
                            #region ���߿����� ���Ƚ��л��߿���ж�
                            string feeFlag = "";
                            decimal feeNum = 0;
                            bool isManageFee = true;		//�Ƿ���Ҫ�����շѺ���
                            bool isFee = false;				//�Ƿ����շ� true �ѼƷ� false ����δ�Ʒ�
                            decimal phaNum = 0; //�Ƿ��ǵ������߿�棬�ǿ��ҡ��������

                            //���ڿ��ҡ�����ȡ����ҩ����ҩȡ�������ǼƷѲ�ȡ������֤�۷Ѿ���
                            //if (pharmacyIntegrate.PatientStore(order, ref feeFlag, ref feeNum, ref isFee, ref phaNum, ref applyOutExecSeq) == -1)
                            if (pharmacyIntegrate.PatientStore(order, ref feeFlag, ref feeNum, ref isFee, ref phaNum) == -1)
                            {
                                this.Err = ("���߿����⴦��������" + pharmacyIntegrate.Err);
                                return -1;
                            }
                            #endregion

                            if (isNurseCharge) //��ʿվ�շ�
                            {
                                order.IsCharge = true;
                                order.ChargeOper.Dept = order.Order.NurseStation.Clone();
                                order.ChargeOper.ID = oper.ID;
                                order.ChargeOper.Name = oper.Name;
                                order.ChargeOper.OperTime = dt;
                                order.Order.Oper = order.ChargeOper.Clone();

                                switch (feeFlag)
                                {
                                    case "2":			//�������̴���																
                                        break;
                                    case "1":			//���պ������صļƷ��������мƷѴ���
                                        order.Order.Qty = feeNum;
                                        break;
                                    case "0":			//�ۻ��߿�� �����мƷѴ���
                                        if (isFee)		//�ѼƷ�
                                        {
                                            order.DrugFlag = 3;			//����ҩ
                                            order.IsCharge = true;		//���շ�
                                            order.ChargeOper.Dept = order.Order.NurseStation.Clone();
                                            order.ChargeOper.ID = oper.ID;
                                            order.ChargeOper.Name = oper.Name;
                                            order.ChargeOper.OperTime = dt;
                                            order.Order.Oper = order.ChargeOper.Clone();
                                        }
                                        isManageFee = false;
                                        break;
                                    default:
                                        break;
                                }
                                if (isManageFee)
                                {
                                    alChargeOrders.Add(order.Order);
                                }
                            }
                            else //ҩ���շ�
                            {
                                switch (feeFlag)
                                {
                                    case "2":			//�������̴���																
                                        break;
                                    case "1":			//���պ������صļƷ��������мƷѴ���
                                        order.Order.Qty = feeNum;
                                        order.Order.User03 = phaNum.ToString();
                                        break;
                                    case "0":			//�ۻ��߿�� �����мƷѴ���
                                        isManageFee = false;
                                        break;
                                    default:
                                        break;
                                }
                                order.IsCharge = false;
                            }
                            order.Order.User03 = order.ID;

                            #region ����ҩƷ���ͱ�

                            if (order.Order.OrderType.IsNeedPharmacy)
                            {
                                int iSendFlag = 2;/*��ҩ��� ��ʱ����ʱ����*/
                                order.DrugFlag = iSendFlag;

                                if (isManageFee)
                                {
                                    if (feeFlag == "2")
                                    {
                                        alDrugSendOrders.Add(order);
                                        //applyOutExecSeq = order.ID;
                                    }
                                    else
                                    {
                                        if (phaNum >= 0)
                                        {
                                            FS.HISFC.Models.Order.ExecOrder orderTemp = order.Clone();
                                            orderTemp.Order.Qty = phaNum;
                                            alDrugSendOrders.Add(orderTemp);
                                            //applyOutExecSeq = orderTemp.ID;
                                        }
                                    }
                                }
                                //else
                                //{
                                //    string applyNum = "";
                                //    string execSeqAll = "";
                                //    if (pharmacyIntegrate.GetExecSeqAllByExecSeq(applyOutExecSeq, ref applyNum, ref execSeqAll) == -1)
                                //    {
                                //        this.Err = ( pharmacyIntegrate.Err);
                                //        return -1;
                                //    }

                                //    this.ManagerPharmacy.UpdateApplyOutForOrderSeq(applyNum, execSeqAll + "|" + order.ID);
                                //}

                                //if (hsApplyExecSeq.Contains(applyOutExecSeq))
                                //{
                                //    hsApplyExecSeq[applyOutExecSeq] = hsApplyExecSeq[applyOutExecSeq].ToString() + "|" + order.ID;
                                //}
                                //else
                                //{
                                //    hsApplyExecSeq.Add(applyOutExecSeq, "|" + order.ID);
                                //}

                                //if (!hsOrderExecSeq.Contains(order.ID))
                                //{
                                //    hsOrderExecSeq.Add(order.ID, null);
                                //}
                            }
                            else
                            {
                                order.DrugFlag = 3;//��ҩ��־ 
                            }
                            #endregion
                        }
                    }

                    #endregion

                    #region ����ִ�б�� //����ȷ�ϱ�Ǽ�ִ�б��
                    try
                    {
                        //��ִ������
                        order.ExecOper.ID = oper.ID;
                        order.ExecOper.Name = oper.Name;
                        order.IsExec = true;
                        order.ExecOper.OperTime = dt;

                        if (order.Order.Item.ID == "999" || !order.Order.OrderType.IsCharge)
                        {
                            order.IsCharge = true;
                            order.ChargeOper.Dept = order.Order.NurseStation.Clone();
                            order.ChargeOper.ID = oper.ID;
                            order.ChargeOper.Name = oper.Name;
                            order.ChargeOper.OperTime = dt;
                            order.Order.Oper = order.ChargeOper.Clone();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Err = "����ִ�����ݳ���";
                        return -1;
                    }

                    iReturn = orderManager.UpdateForConfirmExecDrug(order, isOrderConfirmFee);

                    if (iReturn == -1)
                    {
                        this.Err = orderManager.Err;
                        return -1;
                    }
                    else if (iReturn == 0)
                    {
                        this.Err = order.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + order.Order.Patient.Name +"\r\n\r\n���:" + order.SubCombNO.ToString() + " " + order.Order.Item.Name + "\r\nҽ���Ѿ������仯����ˢ����Ļ��";
                        return -1;
                    }
                    #endregion

                    #region ��ҩƷ������ҽ��������ִ�б��

                    if (order.Order.Item.ID == "999" || !order.Order.OrderType.IsCharge)
                    {
                        iReturn = orderManager.UpdateOrderStatus(order.Order.ID, 2);
                        if (iReturn == -1) //����
                        {
                            this.Err = orderManager.Err;
                            return -1;
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region ��ҩƷ

            else
            {
                FS.HISFC.BizProcess.Integrate.FeeInterface.InpatientRuleFee ruleFeeMgr = new FS.HISFC.BizProcess.Integrate.FeeInterface.InpatientRuleFee();
                List<string> strList = ruleFeeMgr.GetFeeRuleItemCode();

                if (strList == null)
                {
                    strList = new List<string>();
                }

                foreach (FS.HISFC.Models.Order.ExecOrder order in alExecOrder)
                {
                    order.Order.Patient = patient;
                    order.Order.ExecOper.Dept = order.Order.ExeDept.Clone();
                    string err = "";
                    #region �շ�
                    if (order.Order.Item.ID != "999" 
                        || !order.Order.OrderType.IsCharge)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order o = order.Order;

                        if (FillFeeItem(ref o, out err, 1) == -1)
                        {
                            this.Err = err;
                            return -1;
                        }
                    }

                    //By Maokb 061016
                    bool isNeedConfirm = true;

                    //���ڲ���Ҫ�ն�ȷ����Ŀ��ִ�п����Ǳ����һ������Ľ����շѺ�ȷ��ִ�д���
                    if (order.Order.Item.ID != "999")
                    {
                        //���ڴ��ҽ��״̬�����⣬�����������ʱ���ٴ򿪣�2013��1��9��18:07:00
                        //if (!order.Order.IsSubtbl)
                        //{
                        ((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).IsNeedConfirm = this.fee.GetItem(order.Order.Item.ID).IsNeedConfirm;
                        //}
                    }
                    //if (((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).IsNeedConfirm == false ||
                    //    order.Order.ExeDept.ID == order.Order.ReciptDept.ID ||
                    //      order.Order.ExeDept.ID == nurseCode)
                    //{
                    if (CheckCharge(patient, order.Order))
                    {
                        isNeedConfirm = false;
                        order.IsCharge = false;

                        if (order.Order.OrderType.IsCharge == false &&
                            order.Order.IsSubtbl == false)
                        {
                            //ҽ�������У����Ǹ��ĵĲ��շѡ�
                        }
                        else if (!this.IsFee(patient, order.Order) &&  //order.Order.Item.Price <= 0 &&
                            ((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).UnitFlag != "1")
                        {
                            //���Ǹ�����Ŀ���۸�С����Ĳ��շ�
                            order.IsCharge = true;
                        }
                        else
                        {
                            //�����շѵĲ�����
                            if (strList.Contains(order.Order.Item.ID.Trim()))
                            {
                                isNeedConfirm = false;
                            }
                            else if (!isCharge)
                            {
                                isNeedConfirm = false;
                            }
                            else
                            {
                                /*�շ�*/
                                order.IsCharge = true;
                                order.ChargeOper.Dept = order.Order.NurseStation;
                                order.ChargeOper.ID = oper.ID;
                                order.ChargeOper.OperTime = dt;
                                order.Order.Oper = order.ChargeOper.Clone();

                                #region ����Ǹ�����Ŀ�����ϸ��
                                if (((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).UnitFlag == "1")
                                {
                                    ArrayList al = managerPack.QueryUndrugPackagesBypackageCode(order.Order.Item.ID);
                                    if (al == null)
                                    {
                                        this.Err = "���ϸ�����" + managerPack.Err;

                                        return -1;
                                    }

                                    decimal rate = 1;
                                    foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in al)
                                    {
                                        FS.HISFC.Models.Order.ExecOrder myorder = null;
                                        decimal qty = order.Order.Qty;
                                        myorder = order.Clone();
                                        myorder.Name = undrug.Name;
                                        myorder.Order.Name = undrug.Name;
                                        myorder.Order.User03 = order.ID;
                                        /*�շ�*/
                                        myorder.IsCharge = true;
                                        myorder.ChargeOper.Dept = order.Order.NurseStation;
                                        myorder.ChargeOper.ID = oper.ID;
                                        myorder.ChargeOper.OperTime = dt;

                                        myorder.Order.Oper = myorder.ChargeOper.Clone();

                                        myorder.Order.Item = undrug.Clone();
                                        myorder.Order.Qty = qty * undrug.Qty;//����==������Ŀ����*С��Ŀ����
                                        myorder.Order.Item.Qty = qty * undrug.Qty;//����==������Ŀ����*С��Ŀ����

                                        //д�븴����Ŀ���롢����
                                        myorder.Order.Package.ID = order.Order.Item.ID;
                                        myorder.Order.Package.Name = order.Order.Item.Name;
                                        myorder.Order.Package.Qty = qty;
                                        FS.HISFC.Models.Order.Inpatient.Order o = myorder.Order;

                                        rate = fee.GetItemRateForZT(order.Order.Item.ID, undrug.ID);
                                        if (FillFeeItem(ref o, out err, rate) == -1)
                                        {
                                            this.Err = err;
                                            return -1;
                                        }

                                        if (this.IsFee(patient, myorder.Order))  // myorder.Order.Item.Price > 0)
                                        {
                                            alChargeOrders.Add(myorder.Order);
                                        }
                                    }
                                }
                                #endregion

                                #region ������Ŀ�շ�
                                else //��ͨ��Ŀ�շ�
                                {
                                    order.Order.User03 = order.ID;

                                    alChargeOrders.Add(order.Order);
                                }
                                #endregion
                            }
                        }
                    }
                    //}

                    #region //����ȷ�ϱ�Ǽ�ִ�б��
                    try
                    {
                        //��ִ������
                        order.ExecOper.ID = oper.ID;
                        order.ExecOper.Name = oper.Name;
                        //�����Ҫ�ն�ȷ�� ��IsExecΪδִ�� IsChargeΪδ�շ�
                        order.IsExec = !isNeedConfirm;

                        //�����Ѿ���ֵ
                        //order.IsCharge = !isNeedConfirm;

                        //�������еķ�ҩƷ��Ŀ ����ȷ�ϱ��
                        //order.IsConfirm = !isNeedConfirm;

                        //�����ǻ�ʿ�Ѿ�������ҽ���ˣ��ն�ȷ����IsExec�������Ƿ�ȷ��ִ��
                        order.IsConfirm = true;

                        order.ExecOper.OperTime = dt;

                        if (order.ExecOper.Dept.ID != "")
                        {
                            order.Order.ExecOper = order.ExecOper.Clone();
                        }
                        order.Order.Oper.ID = oper.ID;

                        if (order.Order.Item.ID == "999" || !order.Order.OrderType.IsCharge)
                        {
                            order.IsCharge = true;
                            order.ChargeOper.Dept = order.Order.NurseStation.Clone();
                            order.ChargeOper.ID = oper.ID;
                            order.ChargeOper.Name = oper.Name;
                            order.ChargeOper.OperTime = dt;
                            order.Order.Oper = order.ChargeOper.Clone();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Err = "����ִ�����ݳ���" + ex.Message;
                        return -1;
                    }
                    //����ִ�е�ִ�б�� 
                    iReturn = orderManager.UpdateForConfirmExecUnDrug(order, isOrderConfirmFee);

                    #endregion

                    if (iReturn == -1) //����
                    {
                        this.Err = orderManager.Err;
                        return -1;
                    }
                    else if (iReturn == 0)
                    {
                        this.Err = order.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + order.Order.Patient.Name + "\r\n\r\n���:" + order.SubCombNO.ToString() + " " + order.Order.Item.Name + "\r\nҽ���Ѿ������仯����ˢ����Ļ��";
                        return -1;
                    }

                    #region ��ҩƷ������ҽ��������ִ�б��

                    if (order.Order.Item.ID == "999" || !order.Order.OrderType.isCharge)
                    {
                        iReturn = orderManager.UpdateOrderStatus(order.Order.ID, 2);
                        if (iReturn == -1) //����
                        {
                            this.Err = orderManager.Err;
                            return -1;
                        }
                    }
                    #endregion

                    #endregion
                }
            }
            #endregion

            if (alChargeOrders.Count > 0) //��ʱҽ��
            {
                //{B2E4E2ED-08CF-41a8-BF68-B9DF7454F9BB} Ƿ���ж�
                fee.MessageType = this.messType;

                if (fee.FeeItem(patient, ref alChargeOrders) == -1)
                {
                    this.Err = fee.Err;
                    return -1;
                }
            }

            System.Collections.Hashtable hsRecipe = new Hashtable();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alChargeOrders)
            {
                if (!hsRecipe.ContainsKey(feeItem.ExecOrder.ID))
                {
                    hsRecipe.Add(feeItem.ExecOrder.ID, feeItem);
                }
            }

            if (alDrugSendOrders.Count > 0)
            {
                foreach (FS.HISFC.Models.Order.ExecOrder drugOrder in alDrugSendOrders)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList tempFee = hsRecipe[drugOrder.ID] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                    drugOrder.Order.ReciptNO = tempFee.RecipeNO;
                    drugOrder.Order.SequenceNO = tempFee.SequenceNO;
                }
            }

            if (alDrugSendOrders.Count > 0) //��Ҫ��ҩҽ��
            {
                if (SendDrug(alDrugSendOrders, isNurseCharge, dt) == -1)
                {
                    return -1;
                }
            }

            return 0;
        }
        #endregion

        #region ҩƷ����ҩƷ��Ŀ��ֵ

        private static Fee interFee = new Fee();

        /// <summary>
        /// ��÷�ҩƷ��Ϣ
        /// </summary>
        /// <param name="order"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int FillFeeItem(ref FS.HISFC.Models.Order.Inpatient.Order order, out string err)
        {
            return FillFeeItem(ref order, out err, 1);
        }

        /// <summary>
        /// ��÷�ҩƷ��Ϣ
        /// </summary>
        /// <param name="order"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int FillFeeItem(ref FS.HISFC.Models.Order.Inpatient.Order order, out string err, decimal rate)
        {
            err = "";
            if (order.Item.ID == "999")
            {
                return 0;
            }

            FS.HISFC.BizProcess.Integrate.Fee tempManagerFee = new Fee();

            //ManagerFee.SetTrans(t);

            //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
            //FS.HISFC.Models.Fee.Item.Undrug item = tempManagerFee.GetItem(order.Item.ID);
            FS.HISFC.Models.Base.Item item = tempManagerFee.GetUndrugAndMatItem(order.Item.ID, order.Item.Price);

            if (item == null)
            {
                err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + order.Patient.Name + "\r\n\r\n���:" + order.SubCombNO.ToString() + " " + order.Item.Name + "\r\n�Ѿ�ͣ��!";
                return -1;
            }

            //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
            if (item is FS.HISFC.Models.Fee.Item.Undrug)
            {
                //���ڴ��ҽ��״̬�����⣬�����������ʱ���ٴ򿪣�2013��1��9��18:07:00
                //if (!order.IsSubtbl)
                //{
                ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm = item.IsNeedConfirm;
                //}
                order.Item.Price = item.Price;
                order.Item.SpecialPrice = item.SpecialPrice;
                order.Item.ChildPrice = item.ChildPrice;
                order.Item.Name = item.Name;
                order.Item.Specs = item.Specs;

                decimal price = 0;
                decimal orgPrice = 0;

                interFee.GetPriceForInpatient(order.Patient, order.Item, ref price, ref orgPrice, rate);
                if (price > 0)
                {
                    order.Item.Price = price;
                }

                order.Item.MinFee = item.MinFee;
                order.Item.SysClass = item.SysClass.Clone();
                //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
                ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).UnitFlag = ((FS.HISFC.Models.Fee.Item.Undrug)item).UnitFlag;
                ////����ҽ��ʱ���˴����Ի�ȡ������Ϣ�д洢��ִ�п��� houwb 2011-4-7
                //if (string.IsNullOrEmpty(order.ExeDept.ID) && !string.IsNullOrEmpty(((FS.HISFC.Models.Fee.Item.Undrug)item).ExecDept))
                //{
                //    order.ExeDept = new FS.FrameWork.Models.NeuObject(((FS.HISFC.Models.Fee.Item.Undrug)item).ExecDept, "", "");
                //}
            }
            else if (item is FS.HISFC.Models.FeeStuff.MaterialItem)
            {
                (item as FS.HISFC.Models.FeeStuff.MaterialItem).IsNeedConfirm = false;
                order.Item.Price = item.Price;
                order.Item.SpecialPrice = item.SpecialPrice;
                order.Item.ChildPrice = item.ChildPrice;
                order.Item.Name = item.Name;
                order.Item.Specs = item.Specs;

                decimal price = 0;
                decimal orgPrice = 0;
                interFee.GetPriceForInpatient(order.Patient, order.Item, ref price, ref orgPrice);
                if (price > 0)
                {
                    order.Item.Price = price;
                }

                order.Item.MinFee = item.MinFee;
                order.Item.SysClass = item.SysClass.Clone();
                order.StockDept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                order.ExecOper.Dept.ID = order.StockDept.ID;
                order.Item.ItemType = EnumItemType.MatItem;
            }
            return 0;
        }

        #region �����Ż�{AD50C155-BE2D-47b8-8AF9-4AF3548A2726}

        private Hashtable htItem = new Hashtable();

        private Hashtable htDrug = new Hashtable();

        /// <summary>
        /// ��û�����Ϣ
        /// </summary>
        /// <param name="order"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int FillPharmacyItem(ref FS.HISFC.Models.Order.Inpatient.Order order, out string err)
        {
            err = "";
            if (order.Item.ID == "999")
            {
                if (order.Item.ItemType == EnumItemType.Drug)//ҩƷ
                {
                    try
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).Type.ID = order.Item.SysClass.ID.ToString().Substring(order.Item.SysClass.ID.ToString().Length - 1, 1);
                    }
                    catch { }
                }
                return 0;
            }
            FS.HISFC.Models.Pharmacy.Item item;

            if (htDrug.Contains(order.Item.ID))
            {
                item = htDrug[order.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
            }
            else
            {
                item = ManagerPharmacy.GetItem(order.Item.ID);

                htDrug.Add(order.Item.ID, item);
            }


            if (item == null || item.ValidState != EnumValidState.Valid)
            {
                err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + order.Patient.Name + "\r\n\r\n���:" + order.SubCombNO.ToString() + " " + order.Item.Name + "\r\n�Ѿ�ͣ��!";
                return -1;
            }

            //������¸�ֵ���ۼ�
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).PriceCollection.RetailPrice = item.Price;

            // {884A444E-D843-4a8f-8264-01C755D93424}
            // ��ӵڶ����ۼ�
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).RetailPrice2 = item.RetailPrice2;

            order.Item.MinFee = item.MinFee;
            order.Item.Price = item.Price;
            order.Item.Name = item.Name;
            order.Item.Specs = item.Specs;
            order.Item.SysClass = item.SysClass.Clone();//����ϵͳ���
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = item.IsAllergy;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackUnit = item.PackUnit;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = item.MinUnit;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose = item.BaseDose;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm = item.DosageForm;
            return 0;
        }

        /// <summary>
        /// ���ҩƷ��Ϣ
        /// </summary>
        /// <param name="order"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public int FillPharmacyItemWithStockDept(ref FS.HISFC.Models.Order.Inpatient.Order order, out string err)
        {
            err = "";
            if (order.Item.ID == "999")
            {
                if (order.Item.ItemType == EnumItemType.Drug)//ҩƷ
                {
                    try
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = order.DoseUnit;

                        //��ҩƷ���͸�ҩƷ?????
                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).Type.ID = order.Item.SysClass.ID.ToString().Substring(order.Item.SysClass.ID.ToString().Length - 1, 1);
                    }
                    catch
                    {
                    }
                }
                return 0;
            }
            else
            {
                if (order.Patient != null)
                {
                    FS.HISFC.Models.Pharmacy.Storage storage;

                    FS.HISFC.Models.Pharmacy.Item item;
                    if (htDrug.Contains(order.Item.ID))
                    {
                        item = htDrug[order.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
                    }
                    else
                    {
                        item = ManagerPharmacy.GetItem(order.Item.ID);

                        htDrug.Add(order.Item.ID, item);
                    }

                    //if (htStorage.Contains(order.Item.ID + order.Patient.PVisit.PatientLocation.Dept.ID))
                    //{
                    //    storage = htStorage[order.Item.ID + order.Patient.PVisit.PatientLocation.Dept.ID] as FS.HISFC.Models.Pharmacy.Storage;
                    //}
                    //else
                    //{
                    try
                    {
                        FS.HISFC.BizProcess.Integrate.Pharmacy ManagerPharmacy = new FS.HISFC.BizProcess.Integrate.Pharmacy();

                        if (!string.IsNullOrEmpty(order.StockDept.ID))
                        {
                            storage = ManagerPharmacy.GetStockInfoByDrugCode(order.StockDept.ID, order.Item.ID);
                        }
                        else
                        {
                            //houwb 2011-5-30 ���ӷ�ҩ�����ж�
                            if (!string.IsNullOrEmpty(order.ReciptDept.ID))
                            {
                                storage = ManagerPharmacy.GetItemStorage(order, order.ReciptDept.ID, "I", order.Item.ID);
                            }
                            else
                            {
                                storage = ManagerPharmacy.GetItemStorage(order, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID, "I", order.Item.ID);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        err = "��ѯ������\r\n" + ex.Message;
                        err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + order.Patient.Name + "\r\n\r\n���:" + order.SubCombNO.ToString() + " " + order.Item.Name + " ��ѯ������\r\n" + ex.Message;
                        return -1;
                    }

                    //htStorage.Add(order.Item.ID + order.Patient.PVisit.PatientLocation.Dept.ID, storage);
                    //}
                    if (storage == null || storage.ValidState != EnumValidState.Valid)
                    {
                        err = "�Ѿ�ͣ��!";
                        err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + order.Patient.Name + "\r\n\r\n���:" + order.SubCombNO.ToString() + " " + order.Item.Name + " �Ѿ�ͣ��!";

                        return -1;
                    }
                    else if (storage.Item.ID == "" || storage.StoreQty - storage.PreOutQty <= 0)
                    {
                        if (order.OrderType.IsCharge)
                        {
                            err = "��治��!";
                            err = order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� " + order.Patient.Name + "\r\n\r\n���:" + order.SubCombNO.ToString() + " " + order.Item.Name + " ��治��!";

                            return -1;
                        }
                    }

                    if (string.IsNullOrEmpty(order.StockDept.ID))
                    {
                        order.StockDept.ID = storage.StockDept.ID;//
                        order.StockDept.Name = storage.StockDept.Name;//
                    }

                    //������¸�ֵ���ۼ�
                    ((FS.HISFC.Models.Pharmacy.Item)order.Item).PriceCollection.RetailPrice = storage.Item.Price;

                    // {884A444E-D843-4a8f-8264-01C755D93424}
                    // ��ӵڶ����ۼ�
                    ((FS.HISFC.Models.Pharmacy.Item)order.Item).RetailPrice2 = storage.Item.RetailPrice2;

                     if (IGetItemPrice == null)
                     {
                        IGetItemPrice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice)) as FS.HISFC.BizProcess.Interface.Fee.IGetItemPrice;
                     }

                     if (IGetItemPrice != null)
                     {
                         decimal orgPrice = 0;
                         order.Item.Price =  IGetItemPrice.GetPriceForInpatient(storage.Item.ID, order.Patient, storage.Item.Price, storage.Item.Price, storage.Item.Price, storage.Item.RetailPrice2, ref orgPrice);
                     }
                     else
                     {
                         order.Item.Price = storage.Item.Price;
                     }

                    if (order.Item.ID != "999")
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = item;
                        if (phaItem != null)
                        {
                            order.Item.MinFee = phaItem.MinFee;
                            order.Item.Name = phaItem.Name;
                            order.Item.Specs = phaItem.Specs;
                            order.Item.SysClass = phaItem.SysClass.Clone();//����ϵͳ���
                            ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = phaItem.IsAllergy;
                            ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackUnit = phaItem.PackUnit;
                            ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = phaItem.MinUnit;
                            ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose = phaItem.BaseDose;
                            ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm = phaItem.DosageForm;
                        }
                    }
                }
            }
            return 0;
        }

        #endregion


        #endregion

        #region ҽ������

        /// <summary>
        /// סԺ�����㷨�ӿ�
        /// </summary>
        static FS.HISFC.BizProcess.Interface.Order.IDealSubjob iDealSubjob = null;

        /// <summary>
        /// סԺ���Ĵ���
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="order"></param>
        /// <param name="alOrders"></param>
        /// <param name="alSubOrders"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int DealSubjobByInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, ref ArrayList alSubOrders, ref string errInfo)
        {
            if (iDealSubjob == null)
            {
                iDealSubjob = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Order), typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob)) as FS.HISFC.BizProcess.Interface.Order.IDealSubjob;
            }
            if (iDealSubjob != null)
            {
                //���Ĵ���
                return iDealSubjob.DealSubjob(patientInfo, true, order, alOrders, ref alSubOrders, ref errInfo);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        /// <param name="al"></param>
        /// <param name="deptCode"></param>
        /// <param name="err"></param>
        /// <param name="strNameNotUpdate"></param>
        /// <returns></returns>
        public int SaveOrder(List<FS.HISFC.Models.Order.Inpatient.Order> al, string deptCode,
            out string err, out string strNameNotUpdate, System.Data.IDbTransaction t)
        {
            FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
            FS.HISFC.BizLogic.Order.AdditionalItem AdditionalItemManagement = new FS.HISFC.BizLogic.Order.AdditionalItem();
            FS.HISFC.BizProcess.Integrate.Fee itemManager = new FS.HISFC.BizProcess.Integrate.Fee();

            itemManager.SetTrans(t);
            AdditionalItemManagement.SetTrans(t);
            orderManager.SetTrans(t);

            string strComboNo = "";//��Ϻ�
            FS.HISFC.Models.Order.Inpatient.Order order = null;
            string strID = "";
            strNameNotUpdate = "";
            err = "";

            FS.HISFC.Models.Order.Inpatient.Order orderTemp = null;
            for (int i = 0; i < al.Count; i++)
            {
                order = al[i];

                #region  ����Ƥ��ѡ��
                //if (order.Item.ItemType == EnumItemType.Drug)
                //{
                //    if (order.HypoTest == 1 || order.HypoTest == 4)			//����Ƥ�Ի�Ϊ����
                //    {
                //        ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = false;
                //    }
                //    else
                //    {
                //        ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = true;
                //    }
                //}
                #endregion

                #region ����ҽ��

                //�ⲿ��ȡҽ����ˮ��
                orderTemp = orderManager.QueryOneOrder(order.ID);
                if (orderTemp == null)
                {
                    if (orderManager.InsertOrder(order) == -1)
                    {
                        err = orderManager.Err;
                        order.ID = "";
                        return -1;
                    }
                }
                else
                {
                    int mystatus = orderTemp.Status;
                    if (mystatus == 0 || mystatus == 5)//�ж�ҽ��״̬
                    { }
                    else
                    {
                        strNameNotUpdate += "[" + order.Item.Name + "]";
                        continue;
                    }

                    if (orderManager.UpdateOrder(order) == -1)
                    {
                        err = orderManager.Err;
                        return -1;
                    }
                }

                #region �ɵ�����

                //if (order.ID == "")
                //{
                //    try
                //    {
                //        #region �¼ӵ�ҽ��
                //        strID = GetNewOrderID(orderManager);
                //        if (strID == "")
                //        {
                //            err = FS.FrameWork.Management.Language.Msg("���ҽ����ˮ�ų���");
                //            order.ID = "";
                //            return -1;
                //        }
                //        order.ID = strID; //���ҽ����ˮ��

                //        if (orderManager.InsertOrder(order) == -1)
                //        {
                //            err = orderManager.Err;
                //            order.ID = "";
                //            return -1;
                //        }
                //        #endregion
                //    }
                //    catch (Exception ex)
                //    {
                //        err = ex.Message;
                //        order.ID = "";
                //        return -1;
                //    }
                //}
                //else
                //{
                //    #region ���µ�ҽ��

                //    int mystatus = orderManager.QueryOneOrder(order.ID).Status;
                //    if (mystatus == 0 || mystatus == 5)//�ж�ҽ��״̬
                //    { }
                //    else
                //    {
                //        strNameNotUpdate += "[" + order.Item.Name + "]";
                //        continue;
                //    }

                //    #endregion
                //    if (orderManager.UpdateOrder(order) == -1)
                //    {
                //        err = orderManager.Err;
                //        return -1;
                //    }
                //}
                #endregion

                #endregion

                #region ���ҽ��

                if (strComboNo != order.Combo.ID || order.Item.ItemType != EnumItemType.Drug)
                {
                    //ҩƷ,��ҩƷ
                    strComboNo = order.Combo.ID;
                    #region ��ø���
                    //ɾ���Ѿ��еĸ���
                    if (orderManager.DeleteOrderSubtbl(order.Combo.ID) == -1)
                    {
                        err = FS.FrameWork.Management.Language.Msg("ɾ��������Ŀ��Ϣ����") + orderManager.Err;
                        return -1;
                    }
                    ArrayList alSubtbls = null;


                    string errInfo = "";
                    int rev = DealSubjobByInpatient(order.Patient, order.Clone(), new ArrayList(al), ref alSubtbls, ref errInfo);
                    if (rev == -1)
                    {
                        err = errInfo;
                        return -1;
                    }
                    else if (rev == 0)
                    {
                        if (order.Item.ItemType == EnumItemType.Drug)//ҩƷ�������÷�
                        {
                            alSubtbls = AdditionalItemManagement.QueryAdditionalItem(true, order.Usage.ID, deptCode);
                        }
                        else//��ҩƷ�����ݸ�����Ŀ����
                        {
                            alSubtbls = AdditionalItemManagement.QueryAdditionalItem(false, order.Item.ID, deptCode);
                        }
                    }

                    for (int m = 0; m < alSubtbls.Count; m++)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug item = null;

                        item = itemManager.GetItem(((FS.HISFC.Models.Base.Item)alSubtbls[m]).ID);//���������Ŀ��Ϣ

                        if (item == null || !item.IsValid)
                        {
                            //����ͣ�ã�û�ҵ�
                        }
                        else
                        {
                            item.Qty = ((FS.HISFC.Models.Base.Item)alSubtbls[m]).Qty;

                            FS.HISFC.Models.Order.Inpatient.Order newOrder = order.Clone();

                            if (!order.OrderType.IsCharge)
                            {
                                if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                                {
                                    newOrder.OrderType.ID = "CZ";
                                    newOrder.OrderType.Name = "����ҽ��";
                                    newOrder.OrderType.IsCharge = true;
                                }
                                else
                                {
                                    newOrder.OrderType.ID = "LZ";
                                    newOrder.OrderType.Name = "��ʱҽ��";
                                    newOrder.OrderType.IsCharge = true;
                                }
                            }

                            newOrder.Item = item.Clone();
                            newOrder.Qty = item.Qty;
                            newOrder.Unit = item.PriceUnit;
                            newOrder.IsSubtbl = true;
                            newOrder.Usage = new FS.FrameWork.Models.NeuObject();
                            if (order.Item.SysClass.ID.ToString() == "UL")
                            {
                                newOrder.ExeDept.ID = newOrder.ReciptDept.ID;
                                newOrder.ExeDept.Name = newOrder.ReciptDept.Name;
                            }

                            strID = GetNewOrderID(orderManager);

                            if (order.Item.ItemType != EnumItemType.Drug)//��ҩƷ���÷�ҩƷ���ı��
                            {
                                //���ĵ�ִ�п�����ҽ����ͬ
                                //newOrder.ExeDept = newOrder.Patient.PVisit.PatientLocation.Dept.Clone();//ִ�п���Ϊ�������ڿ���
                            }

                            if (strID == "")
                            {
                                err = FS.FrameWork.Management.Language.Msg("���ҽ����ˮ�ų���");
                                return -1;
                            }

                            newOrder.ID = strID; //���ҽ����ˮ��
                            if (orderManager.InsertOrder(newOrder) == -1)
                            {
                                err = orderManager.Err;
                                return -1;
                            }
                        }
                    }
                    #endregion

                }
                #endregion
            }
            return 0;

        }
        #endregion

        #region ���ҽ����ˮ��
        /// <summary>
        /// ���ҽ����ˮ��
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetNewOrderID(FS.HISFC.BizLogic.Order.Order o)
        {

            string rtn = o.GetNewOrderID();
            if (rtn == null || rtn == "")
            {
                MessageBox.Show("������ҽ����ˮ�ţ�");
            }
            else
            {
                return rtn;
            }
            return "";
        }
        #endregion

        #region ���з���
        /// <summary>
        /// ���з���ҩƷ
        /// </summary>
        /// <returns></returns>
        public int SendDrug(List<FS.HISFC.Models.Order.ExecOrder> alExecOrder, int sendFlag)
        {
            FS.HISFC.Models.Order.ExecOrder order = null;
            DateTime dt = orderManager.GetDateTimeFromSysDateTime();
            #region ҩƷ
            for (int i = 0; i < alExecOrder.Count; i++)
            {

                order = alExecOrder[i];
                if (order == null)
                {

                    this.Err = "û��ѯ��ҽ����";
                    return -1;
                }

                #region �����Ŀ��Ϣ ������Ч��
                string err;
                FS.HISFC.Models.Order.Inpatient.Order myOrder = order.Order;
                if (FillPharmacyItem(ref myOrder, out err) == -1)
                {

                    this.Err = err;
                    return -1;
                }
                #endregion

                #region ����ҩƷ���ͱ�
                if (order.IsCharge)
                {
                    order.DrugFlag = sendFlag;
                    int parm = this.SendToDrugStore(order, dt);
                    if (parm == -1)
                    {
                        if (ManagerPharmacy.ErrCode == "-1") //��ҩ����Oracle����Ϊ�㣬û�ҵ���ҩ��
                        {
                            #region ���Ͱ�ҩ���ж�

                            FS.HISFC.Models.Pharmacy.Item item = order.Order.Item as FS.HISFC.Models.Pharmacy.Item;
                            if (item == null)
                            {

                                this.Err = "��ҩƷ �޷����м��з���";
                                return -1;
                            }
                            else
                            {
                                this.Err = ("��ҩ��Ӧ�İ�ҩ��δ��������! ����ҩѧ������Ϣ����ϵ" +
                                    "\nҽ������:" + order.Order.OrderType.ID + " \nҩƷ����:" + item.Type.ID +
                                    " \n�÷�:" + order.Order.Usage.Name + " \nҩƷ����:" + item.Quality.ID +
                                    " \nҩƷ����:" + item.DosageForm.ID);
                                return -1;
                            }

                            #endregion
                        }
                        else
                        {

                            this.Err = ("����ҩƷ����ʧ�ܣ�\n" + ManagerPharmacy.Err);
                            return -1;
                        }
                    }
                }
                #endregion

                #region ��ҩ���
                int iReturn = 0;

                iReturn = orderManager.SetDrugFlag(order.ID, sendFlag);

                if (iReturn == -1) //����!
                {
                    this.Err = orderManager.Err;
                    return -1;
                }
                if (iReturn == 0) //����
                {
                    this.Err = ("���з�ҩ��Ϣ�Ѿ������仯,��رմ�������!");
                    return -1;
                }
                #endregion

            }
            #endregion
            return 0;
        }

        #endregion

        #region �շ�

        /// <summary>
        /// ҩƷ�ͷ�ҩƷ�ļ۸�Ϊ0�Ƿ��շѣ�0����ȡ��1��ȡ��Ĭ��ֵΪ0����ȡ
        /// </summary>
        private string isFeeWhenPriceZero = "-1";

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

        /// <summary>
        /// �Ƿ��շ�
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool IsFee(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Order.Inpatient.Order order)
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

        #endregion

        #region ���⺯��


        /// <summary>
        /// �Ƿ����
        /// �Բ�������ҽԺ��������1
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="order"></param>
        /// <returns>0 ���շ�/���� 1 �շ� -1 ������</returns>
        public int IsCanFee(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            return 1;
        }


        /// <summary>
        /// ִ�м�¼
        /// ����ҽ��ִ����Ϣ
        /// ��ҽ������ʹ��
        /// </summary>
        /// <param name="execOrder">ִ�е���Ϣ</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateRecordExec(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            return orderManager.UpdateRecordExec(execOrder);
        }

        /// <summary>
        /// �շѼ�¼
        /// ����ִ��ҽ���շ��ˣ��շѱ�ǣ���Ʊ�ŵ�
        /// �Է��ÿ���ʹ��
        /// </summary>
        /// <param name="execOrder">ִ�е���Ϣ</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateChargeExec(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            return orderManager.UpdateChargeExec(execOrder);
        }
        /// <summary>
        /// ��ҩ��¼
        /// ��ҩ������ʹ��,����DrugFlag
        /// </summary>
        /// <param name="execOrder">ִ�е���Ϣ</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateDrugExec(FS.HISFC.Models.Order.ExecOrder execOrder)
        {
            return orderManager.UpdateDrugExec(execOrder);
        }
        /// <summary>
        /// ����ҽ����ҩ���
        /// ��ҩ���Ľӿ�
        /// ��ҩ������ʹ��
        /// </summary>
        /// <param name="execOrderID">ִ�е�ID</param>
        /// <param name="orderNo">����ID</param>
        /// <param name="userID">����Ա</param>
        /// <param name="deptID">��ҩ����</param>
        /// <returns>-1 ʧ�� 0 �ɹ�</returns>
        public int UpdateOrderDruged(string execOrderID, string orderNo, string userID, string deptID)
        {
            return orderManager.UpdateOrderDruged(execOrderID, orderNo, userID, deptID);
        }
        /// <summary>
        /// ����ҽ����ҩ���
        /// ��ҩ������ʹ��
        /// </summary>
        /// <param name="execOrderID">ִ�е�ID</param>
        /// <param name="userID">����Ա</param>
        /// <param name="deptID">��ҩ����</param>
        /// <returns>-1 ʧ�� 0 �ɹ�</returns>
        public int UpdateOrderDruged(string execOrderID, string userID, string deptID)
        {
            return UpdateOrderDruged(execOrderID, "", userID, deptID);
        }


        /// <summary>
        /// ���Ͱ�ҩ֪ͨ\���÷�ҩ��ʽ
        /// 0���跢��/1���з���/2��ɢ����/3����ҩ
        /// ��ҩ������ʹ��
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="drugFlag">0���跢��/1���з���/2��ɢ����/3����ҩ</param>
        /// <returns></returns>
        public int SetDrugFlag(string execOrderID, int drugFlag)
        {
            return orderManager.SetDrugFlag(execOrderID, drugFlag);
        }
        /// <summary>
        /// ���·���֪ͨ
        /// ��ҩ������ʹ��
        /// </summary>
        /// <param name="nurse"></param>
        /// <returns></returns>
        public int SendInformation(FS.FrameWork.Models.NeuObject nurse)
        {
            return orderManager.SendInformation(nurse);

        }

        /// <summary>
        /// ��ҩ������ҩƷ�����
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int SendToDrugStore(FS.HISFC.Models.Order.ExecOrder order, DateTime dt)
        {
            if (order.DrugFlag == 0) return 0;//δ��ҩ������ҩƷ�����

            int i = ManagerPharmacy.ApplyOut(order, dt, false);
            if (i == -1) //��ҩ����Oracle����Ϊ�㣬û�ҵ���ҩ��
            {
                if (ManagerPharmacy.ErrCode == "-1")
                {
                    #region ���Ͱ�ҩ���ж�

                    FS.HISFC.Models.Pharmacy.Item item = order.Order.Item as FS.HISFC.Models.Pharmacy.Item;
                    if (item == null)
                    {

                        this.Err = "��ҩƷ �޷����м��з���";
                        return -1;
                    }
                    else
                    {
                        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
                        consManager.SetTrans(this.trans);
                        FS.FrameWork.Models.NeuObject consDosage = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM, item.DosageForm.ID);
                        string dosageForm = consDosage.Name;

                        FS.FrameWork.Models.NeuObject consQuality = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY, item.Quality.ID);
                        string drugQuality = consQuality.Name;

                        FS.FrameWork.Models.NeuObject consType = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE, item.Type.ID);
                        string drugType = consType.Name;

                        FS.HISFC.BizLogic.Manager.OrderType orderTypeManager = new FS.HISFC.BizLogic.Manager.OrderType();
                        orderTypeManager.SetTrans(this.trans);

                        ArrayList alList = orderTypeManager.GetList();
                        string orderType = order.Order.OrderType.ID;
                        if (alList != null)
                        {
                            foreach (FS.HISFC.Models.Order.OrderType tempType in alList)
                            {
                                if (tempType.ID == order.Order.OrderType.ID)
                                {
                                    orderType = tempType.Name;
                                }
                            }
                        }

                        this.Err = ("��ҩ��Ӧ�İ�ҩ��δ��������! ����ҩѧ������Ϣ����ϵ" +
                            "\nҽ������: " + orderType + " \nҩƷ����: " + consType +
                            " \n�÷�:     " + order.Order.Usage.Name + " \nҩƷ����: " + drugQuality +
                            " \nҩƷ����: " + consDosage.Name);
                        return -1;
                    }

                    #endregion
                }
                else
                {
                    this.Err = ManagerPharmacy.Err;
                }
            }

            return i;

        }


        /// <summary>
        /// ��ҩ����
        /// </summary>
        /// <param name="execOrder"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int SendToDrugStore(ArrayList execOrder, DateTime dt)
        {
            string err = "";
            for (int i = 0; i < execOrder.Count; i++)
            {
                FS.HISFC.Models.Order.ExecOrder order = execOrder[i] as FS.HISFC.Models.Order.ExecOrder;
                if (order.DrugFlag == 0)
                {
                    execOrder.Remove(order);
                    i--;
                }
            }

            //ManagerPharmacy.HsApplyExecSeq = this.hsApplyExecSeq;

            int j = ManagerPharmacy.ApplyOutByExeOrder(execOrder, dt, false, ref err);
            if (j == -1) //��ҩ����Oracle����Ϊ�㣬û�ҵ���ҩ��
            {
                if (ManagerPharmacy.ErrCode == "-1" && err != "")
                {
                    #region ���Ͱ�ҩ���ж�
                    foreach (FS.HISFC.Models.Order.ExecOrder execOrdObj in execOrder)
                    {
                        if (execOrdObj.Order.Item.ID == err)
                        {
                            FS.HISFC.Models.Pharmacy.Item item = execOrdObj.Order.Item as FS.HISFC.Models.Pharmacy.Item;
                            if (item == null)
                            {

                                this.Err = "��ҩƷ �޷����м��з���";
                                return -1;
                            }
                            else
                            {
                                FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
                                consManager.SetTrans(this.trans);
                                FS.FrameWork.Models.NeuObject consDosage = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM, item.DosageForm.ID);
                                string dosageForm = consDosage.Name;

                                FS.FrameWork.Models.NeuObject consQuality = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY, item.Quality.ID);
                                string drugQuality = consQuality.Name;

                                FS.FrameWork.Models.NeuObject consType = consManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE, item.Type.ID);
                                string drugType = consType.Name;

                                FS.HISFC.BizLogic.Manager.OrderType orderTypeManager = new FS.HISFC.BizLogic.Manager.OrderType();
                                orderTypeManager.SetTrans(this.trans);

                                ArrayList alList = orderTypeManager.GetList();
                                string orderType = execOrdObj.Order.OrderType.ID;
                                if (alList != null)
                                {
                                    foreach (FS.HISFC.Models.Order.OrderType tempType in alList)
                                    {
                                        if (tempType.ID == execOrdObj.Order.OrderType.ID)
                                        {
                                            orderType = tempType.Name;
                                        }
                                    }
                                }

                                this.Err = ("��ҩ��Ӧ�İ�ҩ��δ��������! ����ҩѧ������Ϣ����ϵ" +
                                    "\nҽ������: " + orderType + " \nҩƷ����: " + consType +
                                    " \n�÷�:     " + execOrdObj.Order.Usage.Name + " \nҩƷ����: " + drugQuality +
                                    " \nҩƷ����: " + consDosage.Name);
                                return -1;
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    this.Err = ManagerPharmacy.Err;
                }
            }

            return j;
        }


        /// <summary>
        /// ��ѯһ��ҽ��
        /// </summary>
        /// <param name="clinicCode">����������ˮ��</param>
        /// <param name="id">ҽ����ˮ��</param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.Order GetOneOrder(string clinicCode, string id)
        {
            return outOrderManager.QueryOneOrder(clinicCode, id);
        }

        /// <summary>
        /// ��������ҽ��
        /// </summary>
        /// <param name="outPatientOrder"></param>
        /// <returns></returns>
        public int UpdateOrderBeCaceled(FS.HISFC.Models.Order.OutPatient.Order outPatientOrder)
        {
            this.SetDB(outOrderManager);
            return outOrderManager.UpdateOrderBeCaceled(outPatientOrder);
        }

        /// <summary>
        /// ��ҽ����ˮ�Ų�ѯҽ����Ϣ-������Ч����
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.Inpatient.Order QueryOneOrder(string OrderNO)
        {
            return orderManager.QueryOneOrder(OrderNO);
        }

        /// <summary>
        /// ����ѯִ��ҽ����Ϣ
        /// </summary>
        /// <param name="execOrderID"></param>
        /// <param name="itemType"></param>
        /// <returns>FS.HISFC.Models.Order.ExecOrder</returns>
        public FS.HISFC.Models.Order.ExecOrder QueryExecOrderByExecOrderID(string execOrderID, string itemType)
        {
            return orderManager.QueryExecOrderByExecOrderID(execOrderID, itemType);
        }

        /// <summary>
        /// ��ȡ����ҽ����ˮ��
        /// </summary>
        /// <returns>�ɹ� ����ҽ����ˮ�� ʧ�� null</returns>
        public string GetNewOrderID()
        {
            this.SetDB(orderManager);

            return orderManager.GetNewOrderID();
        }

        /// <summary>
        /// ����ִ�п��Ҳ�ѯ��Ҫȷ����Ŀ�Ļ��ߵ����ڿ���
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QueryPatientDeptByConfirmDeptID(string deptID)
        {
            this.SetDB(orderManager);
            return orderManager.QueryPatientDeptByConfirmDeptID(deptID);
        }

        /// <summary>
        /// ����סԺ��ˮ�Ų�ѯ һ��ʱ���ڵ�ҽ��ִ�����
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="Type">��� 1 ҩƷ 2��ҩƷ</param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrder(string inPatientNo, string type, DateTime begin, DateTime end)
        {
            this.SetDB(orderManager);
            return orderManager.QueryExecOrder(inPatientNo, type, begin, end);
        }

        /// <summary>
        /// ����ִ�п��ҡ��������ڿ��Ҳ�ѯ��Ҫȷ����Ŀ�Ļ���
        /// </summary>
        /// <param name="confirmDept"></param>
        /// <param name="patientDept"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByConfirmDeptAndPatDept(string confirmDept, string patientDept)
        {
            this.SetDB(orderManager);
            return orderManager.QueryPatientByConfirmDeptAndPatDept(confirmDept, patientDept);
        }

        public ArrayList QueryExecOrderByDept(string inpatientNo, string itemType, bool isExec, string deptCode)
        {
            this.SetDB(orderManager);
            return orderManager.QueryExecOrderByDept(inpatientNo, itemType, isExec, deptCode);
        }

        /// <summary>
        /// ����������ִ�е�
        /// </summary>
        /// <param name="execOrderID">ִ�е���ˮ��</param>
        /// <param name="isDrug">�Ƿ�ҩƷ</param>
        /// <param name="dc">Neuobject.IDֹͣ�ˣ�Neuobject.Name���</param>
        /// <returns></returns>
        public int DcExecImmediateUnNormal(string execOrderID, bool isDrug, FS.FrameWork.Models.NeuObject dc)
        {
            this.SetDB(orderManager);
            return orderManager.DcExecImmediate(execOrderID, isDrug, dc);
        }

        /// <summary>
        /// ��ȡ��Ժҽ������������Ժ�������ֵ�������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int GetOutOrder(string inpatientNo, ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            this.SetDB(orderManager);
            return orderManager.GetOutOrder(inpatientNo, ref order);
        }

        /// <summary>
        /// ��ȡת��ҽ�����������ͣ�
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int GetShiftOutOrderType(string inpatientNo, ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            this.SetDB(orderManager);
            return orderManager.GetShiftOutOrderType(inpatientNo, ref order);

        }
        /// <summary>
        /// ��ȡ�����ڲ���������������
        /// add by yerl
        /// </summary>
        public int GetFeeInfoCount(string inpatientNo)
        {
            this.SetDB(orderManager);
            return orderManager.GetFeeInfoCount(inpatientNo);
        }
        /// <summary>
        /// ��ȡת��ҽ����������ת�ơ������ֵ�������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public int GetShiftOutOrder(string inpatientNo, ref FS.HISFC.Models.Order.Inpatient.Order order)
        {
            this.SetDB(orderManager);
            return orderManager.GetShiftOutOrder(inpatientNo, ref order);

        }

        /// <summary>
        /// ��黼���Ƿ��п�����ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="order"></param>
        /// <returns>-1 ����  0 û��ҽ��  1 �ѿ�����ҽ��</returns>
        public int IsOwnOrders(string inpatientNo)
        {
            this.SetDB(this.orderManager);
            return this.orderManager.IsOwnOrders(inpatientNo);
        }

        /// <summary>
        /// ��ʿվ��Ժ�Ǽǣ��Զ�ֹͣȫ������
        /// ֹͣ��Ϊ��ֹͣ�������״̬
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dcDoct">ֹͣҽ��</param>
        /// <param name="confirmNurse">��˻�ʿ</param>
        /// <param name="dcReasonCode">ֹͣԭ�����</param>
        /// <param name="dcReasonName">ֹͣԭ��</param>
        /// <returns></returns>
        public int AutoDcOrder(string inpatientNo, FS.FrameWork.Models.NeuObject dcDoct, FS.FrameWork.Models.NeuObject confirmNurse, string dcReasonCode, string dcReasonName)
        {
            this.SetDB(orderManager);
            return orderManager.AutoDcOrder(inpatientNo, dcDoct, confirmNurse, dcReasonCode, dcReasonName);
        }

        #region {5197289A-AB55-410b-81EE-FC7C1B7CB5D7}
        /// <summary>
        /// У�鳤�ڷ�ҩƷҽ��ִ�е���ʿ�Ƿ�ֽⱣ��
        /// </summary>
        /// <param name="execOrderID">ִ�е���ˮ��</param>
        /// <returns></returns>
        public bool CheckLongUndrugIsConfirm(string execOrderID)
        {
            this.SetDB(orderManager);
            return orderManager.CheckLongUndrugIsConfirm(execOrderID);
        }
        #endregion

        /// <summary>
        ///  ��ҽ�����ͣ�����/��ʱ����״̬����ѯҽ�������������ģ�
        /// </summary>
        /// <returns>�ɹ� ����ҽ����ˮ�� ʧ�� null</returns>
        public ArrayList QueryOrder(string inpatientNO, FS.HISFC.Models.Order.EnumType type, int status)
        {
            this.SetDB(orderManager);

            return orderManager.QueryOrder(inpatientNO, type, status);
        }

        /// <summary>
        /// ����סԺ��ˮ�ź�ҽ�����Ͳ�ѯ����ҽ��
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="type">���� 1ҩƷ 2��ҩƷ</param>
        /// <param name="strOrderID">����ҽ����ˮ����ɵ��ַ��� ��IN��ѯ</param>
        /// <returns></returns>
        public ArrayList QueryOrder(string inPatientNo, string type, string strOrderID)
        {
            this.SetDB(orderManager);
            return orderManager.QueryOrder(inPatientNo, type, strOrderID);
        }

        #endregion

        /// <summary>
        /// ����ҽ������λ��ִ��
        /// </summary>
        /// <param name="orderNo">ҽ����ˮ��</param>
        /// <param name="status">ҽ��״̬</param>
        /// <returns></returns>
        public int UpdateOrderStatus(string orderNo, int status)
        {

            return orderManager.UpdateOrderStatus(orderNo, status);
        }

        /// <summary>
        /// ����ҽ��Ƥ�Խ��//{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
        /// </summary>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        public int UpdateOrderHyTest(string hyTestValue, string sequenceNO)
        {
            this.SetDB(outOrderManager);
            return outOrderManager.UpdateOrderHyTest(hyTestValue, sequenceNO);
        }

        /// <summary>
        /// ���ݿ��Ҳ�ѯҽ����
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Order.Inpatient.MedicalTeamForDoct> QueryQueryMedicalTeamForDoctInfo(string deptCode)
        {
            return this.medicalTeamForDoctBizLogic.QueryQueryMedicalTeamForDoctInfo(deptCode);
        }

        /// <summary>
        /// ����Ƥ����Ϣ
        /// </summary>
        /// <param name="i"></param>
        /// <returns>1 [����] 2 [��Ƥ��] 3 [+] 4 [-]</returns>
        public string TransHypotest(FS.HISFC.Models.Order.EnumHypoTest HypotestCode)
        {
            this.SetDB(outOrderManager);
            return this.orderManager.TransHypotest(HypotestCode);
        }

        /// <summary>
        /// ����ѯ�Ƿ��շ�ҽ��
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="itemType"></param>
        /// <param name="isCharge"></param>
        /// <returns></returns>
        public ArrayList QueryExecOrderIsCharg(string inpatientNo, string itemType, bool isCharge)
        {
            this.SetDB(this.orderManager);
            return orderManager.QueryExecOrderIsCharg(inpatientNo, itemType, isCharge);
        }


        #region ҽ�����



        #endregion
    }
}
