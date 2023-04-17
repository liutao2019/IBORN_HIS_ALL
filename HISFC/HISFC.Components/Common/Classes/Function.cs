using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Components.Common.Controls;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System.Runtime.InteropServices;
using System.IO;
using FS.HISFC.BizProcess.Integrate;

namespace FS.HISFC.Components.Common.Classes
{
    /// <summary>
    /// [��������: ���ú���]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Function
    {
        public Function()
        {

        }

        private static FS.HISFC.BizLogic.Fee.Interface managerInterface = null;

        /// <summary>
        /// �ж��Ƿ��ڱ���״̬
        /// </summary>
        public static bool DesignMode
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");
            }
        }

        /// <summary>
        /// ��ʾ��־
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string ShowItemFlag(FS.HISFC.Models.Base.Item item)
        {
            if (managerInterface == null)
                managerInterface = new FS.HISFC.BizLogic.Fee.Interface();
            return managerInterface.ShowItemFlag(item);
        }

        /// <summary>
        /// �����ѡ��
        /// </summary>
        /// <returns>��ѡ�еĿ�������</returns>
        public static List<FS.HISFC.Models.Base.Department> ChooseMultiDept()
        {
            ucChooseMultiDept uc = new ucChooseMultiDept();
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����ѡ��";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            return uc.SelectedDeptList;
        }

        #region Ȩ�޿���

        /// <summary>
        /// ȡ��ǰ����Ա�Ƿ���ĳһȨ�ޡ�
        /// </summary>
        /// <param name="class2Code">����Ȩ�ޱ���</param>
        /// <returns>True ��Ȩ��, False ��Ȩ��</returns>
        public static bool ChoosePiv(string class2Code)
        {
            List<FS.FrameWork.Models.NeuObject> al = null;
            //Ȩ�޹�����
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            //ȡ����Աӵ��Ȩ�޵Ŀ���
            al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code);

            if (al == null || al.Count == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// ��ȡ��ǰ����Ȩ�޿��Ҽ���
        /// </summary>
        /// <param name="class2Code">����Ȩ�ޱ���</param>
        /// <param name="class3Code">����Ȩ�ޱ���</param>
        /// <param name="isShowErrMsg">�Ƿ񵯳�������Ϣ</param>
        /// <returns>�ɹ�����ӵ��Ȩ�޿����б� ʧ�ܷ���null</returns>
        public static List<FS.FrameWork.Models.NeuObject> QueryPrivList(string class2Code, string class3Code, bool isShowErrMsg)
        {
            List<FS.FrameWork.Models.NeuObject> al = null;
            //Ȩ�޹�����
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            //ȡ����Աӵ��Ȩ�޵Ŀ���
            if (class3Code == null || class3Code == "")
                al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code);
            else
                al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code, class3Code);

            if (al == null)
            {
                if (isShowErrMsg)
                {
                    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg(privManager.Err));
                }
                return null;
            }
            if (al.Count == 0)
            {
                if (isShowErrMsg)
                {
                    System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("��û�д˴��ڵĲ���Ȩ��"));
                }
                return al;
            }

            //�ɹ���Ȩ�޿�������
            return al;
        }

        /// <summary>
        /// ��ȡ��ǰ����Ȩ�޿��Ҽ���
        /// </summary>
        /// <param name="class2Code">����Ȩ�ޱ���</param>
        /// <param name="isShowErrMsg">�Ƿ񵯳�������Ϣ</param>
        /// <returns>�ɹ�����ӵ��Ȩ�޿����б� ʧ�ܷ���null</returns>
        public static List<FS.FrameWork.Models.NeuObject> QueryPrivList(string class2Code, bool isShowErrMsg)
        {
            return Function.QueryPrivList(class2Code, null, isShowErrMsg);
        }

        /// <summary>
        /// �������ڣ�ѡ��Ȩ�޿��ң�����û�ֻ��һ�����ҵ�Ȩ�ޣ������ֱ�ӵ�½
        /// </summary>
        /// <param name="class2Code">����Ȩ�ޱ���</param>
        /// <param name="privDept">Ȩ�޿���</param>
        /// <returns>1 ��������Ȩ�޿��� 0 �û�ѡ��ȡ�� ��1 �û���Ȩ��</returns>
        public static int ChoosePivDept(string class2Code, ref FS.FrameWork.Models.NeuObject privDept)
        {
            return ChoosePrivDept(class2Code, null, ref privDept);
        }

        /// <summary>
        /// �������ڣ�ѡ��Ȩ�޿��ң�����û�ֻ��һ�����ҵ�Ȩ�ޣ������ֱ�ӵ�½
        /// </summary>
        /// <param name="class2Code">����Ȩ�ޱ���</param>
        /// <param name="class3Code">����Ȩ�ޱ���</param>
        /// <param name="privDept">Ȩ�޿���</param>
        /// <returns>1 ��������Ȩ�޿��� 0 �û�ѡ��ȡ�� ��1 �û���Ȩ��</returns>
        public static int ChoosePrivDept(string class2Code, string class3Code, ref FS.FrameWork.Models.NeuObject privDept)
        {
            List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();
            if (class3Code == null || class3Code == "")
                al = QueryPrivList(class2Code, true);
            else
                al = QueryPrivList(class2Code, class3Code, true);

            if (al == null || al.Count == 0)
            {
                return -1;
            }

            //����û�ֻ��һ�����ҵ�Ȩ�ޣ��򷵻ش˿���
            if (al.Count == 1)
            {
                privDept = al[0] as FS.FrameWork.Models.NeuObject;
                return 1;
            }

            //�������ڣ�ȡȨ�޿���
            FS.HISFC.Components.Common.Forms.frmChoosePrivDept formPrivDept = new FS.HISFC.Components.Common.Forms.frmChoosePrivDept();
            formPrivDept.SetPriv(al, true);
            System.Windows.Forms.DialogResult Result = formPrivDept.ShowDialog();

            //ȡ���ڷ���Ȩ�޿���
            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                privDept = formPrivDept.SelectData;
                return 1;
            }

            return 0;
        }

        #endregion

        #region ��ʾҽ��

        static FS.HISFC.BizLogic.Pharmacy.Item pManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        static FS.HISFC.BizLogic.Fee.Item fManager = new FS.HISFC.BizLogic.Fee.Item();
        static FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
        static FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper();
        static FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        //private static FS.SOC.HISFC.BizProcess.OrderIntegrate.OrderBase socOrderIntegrate = new FS.SOC.HISFC.BizProcess.OrderIntegrate.OrderBase();

        private static Hashtable hsPhaStorage = new Hashtable();

        /// <summary>
        /// ��ҩƷ������Ϣ�б�
        /// </summary>
        private static List<FS.HISFC.Models.Fee.Item.Undrug> undrugList = null;

        private static Hashtable hsUndrugItem = null;
        static FS.HISFC.Models.Pharmacy.Storage storage = null;

        /// <summary>
        /// ҽ����Ŀ����ͬ��λ+��Ŀ����
        /// </summary>
        public static Hashtable HsItemPactInfo = null;

        /// <summary>
        /// סԺ�Ƿ�ʹ��Ԥ�ۿ�� P00200
        /// </summary>
        //private static int isUseInDrugPreOut = -1;

        ///// <summary>
        ///// �����Ƿ�ʹ��Ԥ�ۿ�� P00320
        ///// </summary>
        //private static int isUseOutDrugPreOut = -1;
        //static FS.HISFC.Models.Base.Employee oper = pManager.Operator as FS.HISFC.Models.Base.Employee;

        /// <summary>
        /// ���ȱҩ��ͣ��
        /// </summary>
        /// <param name="drugDept">�ۿ����</param>
        /// <param name="item">��Ŀ</param>
        /// <param name="IsOutPatient">�Ƿ����￪��</param>
        /// <param name="drugItem">���ص���Ŀ��Ϣ</param>
        /// <param name="errInfo">������Ϣ</param>
        /// <returns></returns>
        //public static int CheckDrugState(FS.FrameWork.Models.NeuObject drugDept, string itemCode, FS.HISFC.Models.Base.Item item, bool IsOutPatient, ref string errInfo)
        //{
        //    if (item.ID == "999")
        //    {
        //        return 1;
        //    }
        //    storage = pManager.GetStockInfoByDrugCode(drugDept.ID, item.ID);
        //    if (storage == null)
        //    {
        //        errInfo = "����" + pManager.Err;
        //        return -1;
        //    }
        //    else if (storage.Item.ID == "")
        //    {
        //        storage.Item = pManager.GetItem(itemCode);
        //        if (IsOutPatient)
        //        {
        //            errInfo = "��" + item.Name + "���ڱ�����û���ҵ���Ӧ����ϵͳ��ȡҩҩ��!";
        //        }
        //        else
        //        {
        //            errInfo = "��" + item.Name + "���ڱ�����û���ҵ���ӦסԺϵͳ��ȡҩҩ��!";
        //        }
        //        item = null;
        //        return -1;
        //    }
        //    else if (storage.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
        //    {
        //        //��ҩ����ͣ��
        //        errInfo = "��" + item.Name + "����" + drugDept.Name + "��ͣ��!";
        //        return -1;
        //    }
        //    if (IsOutPatient)
        //    {
        //        if (!storage.IsUseForOutpatient)
        //        {
        //            //������������
        //            errInfo = "��" + item.Name + "����" + drugDept.Name + "����Ϊ��������������ҩ!";
        //            return -1;
        //        }
        //        //����ȱҩ
        //        else if (storage.IsLack)
        //        {
        //            //ҩ��ȱҩ
        //            errInfo = "��" + item.Name + "��" + drugDept.Name + "��ȱҩ!";
        //            return -1;
        //        }

        //        if (isUseOutDrugPreOut == -1)
        //        {
        //            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        //            isUseOutDrugPreOut = ctrlIntegrate.GetControlParam<int>(PharmacyConstant.OutDrug_Pre_Out, true);
        //        }

        //        if (isUseOutDrugPreOut == 1)
        //        {
        //            if (storage.StoreQty - storage.PreOutQty <= 0)
        //            {
        //                errInfo = "��" + item.Name + "����" + drugDept.Name + "���Ϊ0����������!";
        //                return -1;
        //            }
        //        }
        //        else
        //        {
        //            if (storage.StoreQty <= 0)
        //            {
        //                errInfo = "��" + item.Name + "����" + drugDept.Name + "���Ϊ0����������!";
        //                return -1;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (!storage.IsUseForInpatient)
        //        {
        //            //������������
        //            errInfo = "��" + item.Name + "����" + drugDept.Name + "����Ϊ��������סԺ��ҩ!";
        //            return -1;
        //        }
        //        //סԺȱҩ
        //        else if (storage.IsLackForInpatient)
        //        {
        //            //ҩ��ȱҩ
        //            errInfo = "��" + item.Name + "��" + drugDept.Name + "��ȱҩ!";
        //            return -1;
        //        }

        //        if (isUseInDrugPreOut == -1)
        //        {
        //            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        //            isUseInDrugPreOut = ctrlIntegrate.GetControlParam<int>(PharmacyConstant.InDrug_Pre_Out, true);
        //        }

        //        if (isUseInDrugPreOut == 1)
        //        {
        //            if (storage.StoreQty - storage.PreOutQty <= 0)
        //            {
        //                errInfo = "��" + item.Name + "����" + drugDept.Name + "���Ϊ0����������!";
        //                return -1;
        //            }
        //        }
        //        else
        //        {
        //            if (storage.StoreQty <= 0)
        //            {
        //                errInfo = "��" + item.Name + "����" + drugDept.Name + "���Ϊ0����������!";
        //                return -1;
        //            }
        //        }
        //    }
        //    return 1;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        private static void CheckOrderState(ArrayList alOrder, FS.HISFC.Models.Base.ServiceTypes serviceType)
        {
            FS.HISFC.Models.Fee.Item.Undrug undrug = null;
            string errInfo = "";
            string undrugCodes = "''";

            if (helper.ArrayObject.Count == 0)
            {
                FS.HISFC.BizLogic.Manager.OrderType orderType = new FS.HISFC.BizLogic.Manager.OrderType();
                helper.ArrayObject = orderType.GetList();
            }

            if (serviceType == FS.HISFC.Models.Base.ServiceTypes.I)
            {
                hsUndrugItem = new Hashtable();
                foreach (FS.HISFC.Models.Order.Inpatient.Order inOrder in alOrder)
                {
                    if (inOrder.Item.ID != "999" && inOrder.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        undrugCodes += ",'" + inOrder.Item.ID + "'";
                    }
                }

                if (!string.IsNullOrEmpty(undrugCodes))
                {
                    undrugList = fManager.GetItemByCodeBatch("(" + undrugCodes + ")");
                    if (undrugList != null)
                    {
                        for (int i = 0; i < undrugList.Count; i++)
                        {
                            if (!hsUndrugItem.Contains(undrugList[i].ID))
                            {
                                hsUndrugItem.Add(undrugList[i].ID, undrugList[i]);
                            }
                        }
                    }
                }

                foreach (FS.HISFC.Models.Order.Inpatient.Order inOrder in alOrder)
                {
                    inOrder.OrderType = helper.GetObjectFromID(inOrder.OrderType.ID) as FS.HISFC.Models.Order.OrderType;

                    //storage = new FS.HISFC.Models.Pharmacy.Storage();
                    undrug = new FS.HISFC.Models.Fee.Item.Undrug();

                    #region ������Ŀ��Ϣ
                    if (inOrder.ID == "999")//�Ա���Ŀ
                    {
                        undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                        undrug.ID = inOrder.ID;
                        undrug.Name = inOrder.Name;
                        undrug.Qty = inOrder.Item.Qty;

                        undrug.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                        undrug.SysClass.ID = "M";
                        undrug.PriceUnit = inOrder.Unit;
                        inOrder.Item = undrug;
                    }
                    else if (inOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)//ҩƷ
                    {
                        if (hsPhaStorage.Contains(inOrder.Item.ID))
                        {
                            storage = hsPhaStorage[inOrder.Item.ID] as FS.HISFC.Models.Pharmacy.Storage;
                        }

                        //if (storage == null || string.IsNullOrEmpty(storage.StockDept.ID))
                        //{
                        //    storage = pManager.GetItemForInpatient(((FS.HISFC.Models.Base.Employee)pManager.Operator).Dept.ID, inOrder.Item.ID);
                        //}
                        FS.HISFC.Models.Pharmacy.Item drugItem = null;
                        if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckDrugState(inOrder.Patient,null, ((FS.HISFC.Models.Base.Employee)pManager.Operator).Dept, inOrder.Item, false, ref drugItem, ref storage, ref errInfo) <=0)
                        {
                            inOrder.ExtendFlag2 = "N";
                        }
                        storage.Item = drugItem;

                        storage.Item.Qty = inOrder.Qty;
                        try
                        {
                            //storage.Item.DoseUnit = ((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).DoseUnit;
                        }
                        catch
                        {
                        }

                        if (storage.Item != null && !string.IsNullOrEmpty(storage.Item.ID))
                        {
                            inOrder.Item = storage.Item;
                        }

                        //ҩƷִ�п���Ϊ��
                        inOrder.ExeDept = new FS.FrameWork.Models.NeuObject();
                    }
                    else//������Ŀ
                    {
                        //if (hsUndrugItem.Contains(inOrder.Item.ID))
                        //{
                        //    undrug = hsUndrugItem[inOrder.Item.ID] as FS.HISFC.Models.Fee.Item.Undrug;
                        //}
                        //else
                        //{
                            if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckUnDrugState(inOrder, ref undrug, ref errInfo) == -1)
                            {
                                inOrder.ExtendFlag2 = "N";
                            }
                        //}
                        if (undrug == null || !undrug.IsValid)
                        {
                            inOrder.ExtendFlag2 = "N";
                        }
                        else
                        {
                            //undrug.ID = inOrder.ID;
                            //undrug.Name = inOrder.Name;
                            undrug.Qty = inOrder.Item.Qty;
                            undrug.PriceUnit = inOrder.Unit;
                            inOrder.Item = undrug;
                            inOrder.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                hsUndrugItem = new Hashtable();
                foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in alOrder)
                {
                    if (outOrder.Item.ID != "999" && outOrder.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        undrugCodes += ",'" + outOrder.Item.ID + "'";
                    }
                }

                if (!string.IsNullOrEmpty(undrugCodes))
                {
                    undrugList = fManager.GetItemByCodeBatch("(" + undrugCodes + ")");
                    if (undrugList != null)
                    {
                        for (int i = 0; i < undrugList.Count; i++)
                        {
                            if (!hsUndrugItem.Contains(undrugList[i].ID))
                            {
                                hsUndrugItem.Add(undrugList[i].ID, undrugList[i]);
                            }
                        }
                    }
                }

                foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in alOrder)
                {
                    //storage = new FS.HISFC.Models.Pharmacy.Storage();
                    undrug = new FS.HISFC.Models.Fee.Item.Undrug();

                    #region ������Ŀ��Ϣ
                    if (outOrder.Item.ID == "999")//�Ա���Ŀ
                    {
                        //undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                        //undrug.ID = outOrder.ID;
                        //undrug.Name = outOrder.Name;
                        //undrug.Qty = outOrder.Item.Qty;

                        //undrug.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                        //undrug.SysClass.ID = "M";
                        //undrug.PriceUnit = outOrder.Unit;
                        //outOrder.Item = undrug;
                    }
                    else if (outOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)//ҩƷ
                    {
                        //if (hsPhaStorage.Contains(outOrder.Item.ID))
                        //{
                        //    storage = hsPhaStorage[outOrder.Item.ID] as FS.HISFC.Models.Pharmacy.Storage;
                        //}

                        //if (storage == null || string.IsNullOrEmpty(storage.StockDept.ID))
                        //{
                        //    storage = pManager.GetItemForInpatient(((FS.HISFC.Models.Base.Employee)pManager.Operator).Dept.ID, outOrder.Item.ID);
                        //}

                        storage = pManager.GetItemForInpatient(((FS.HISFC.Models.Base.Employee)pManager.Operator).Dept.ID, outOrder.Item.ID);

                        FS.HISFC.Models.Pharmacy.Item drugItem = null;

                        if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckDrugState(outOrder.Patient,null, ((FS.HISFC.Models.Base.Employee)pManager.Operator).Dept, outOrder.Item, true, ref drugItem, ref storage, ref errInfo) == -1)
                        {
                            outOrder.ExtendFlag2 = "N";
                        }
                        storage.Item = drugItem;

                        try
                        {
                            storage.Item.DoseUnit = ((FS.HISFC.Models.Pharmacy.Item)outOrder.Item).DoseUnit;
                        }
                        catch
                        {
                        }
                        if (storage.Item != null && !string.IsNullOrEmpty(storage.Item.ID))
                        {
                            decimal qty = outOrder.Item.Qty;
                            outOrder.Item = storage.Item.Clone();//��Ҫ�ÿ�¡����
                            outOrder.Item.Qty = qty;
                        }

                        //ҩƷִ�п���Ϊ��
                        outOrder.ExeDept = new FS.FrameWork.Models.NeuObject();
                    }
                    else
                    {
                        //if (hsUndrugItem.Contains(outOrder.Item.ID))
                        //{
                        //    undrug = hsUndrugItem[outOrder.Item.ID] as FS.HISFC.Models.Fee.Item.Undrug;
                        //}
                        //else
                        //{
                            if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckUnDrugState(outOrder, ref undrug, ref errInfo) == -1)
                            {
                                outOrder.ExtendFlag2 = "N";
                            }
                        //}
                        if (undrug == null || !undrug.IsValid)
                        {
                            outOrder.ExtendFlag2 = "N";
                        }
                        else
                        {
                            //undrug.ID = outOrder.ID;
                            //undrug.Name = outOrder.Name;
                            undrug.Qty = outOrder.Item.Qty;
                            undrug.PriceUnit = outOrder.Unit;
                            outOrder.Item = undrug;
                            outOrder.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                        }
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// ��ʾҽ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        public static void ShowOrder(object sender, ArrayList alOrder, FS.HISFC.Models.Base.ServiceTypes serviceType)
        {
            ShowOrder(sender, alOrder, 0, serviceType);
        }

        /// <summary>
        /// ��ʾҽ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="alOrder"></param>
        /// <param name="type"></param>
        public static void ShowOrder(object sender, ArrayList alOrder, int type, FS.HISFC.Models.Base.ServiceTypes serviceType)
        {
            try
            {
                #region ����dataSet

                #region ������������ʼ��
                //���崫��DataSet
                DataSet myDataSet = new DataSet();
                myDataSet.EnforceConstraints = false;//�Ƿ���ѭԼ������
                //��������
                System.Type dtStr = System.Type.GetType("System.String");
                System.Type dtBool = System.Type.GetType("System.Boolean");
                System.Type dtInt = System.Type.GetType("System.Int32");
                //�����********************************************************
                //Main Table
                DataTable dtMain = new DataTable();
                dtMain = myDataSet.Tables.Add("TableMain");

                dtMain.Columns.AddRange(new DataColumn[]{
                    new DataColumn("ID", dtStr),
                    new DataColumn("��Ϻ�", dtStr), 
                    new DataColumn("ҽ������", dtStr),
                    new DataColumn("���", dtStr),
                    new DataColumn("���", dtStr),
                    new DataColumn("���ʱ��", dtStr),
                    new DataColumn("ÿ�μ���", dtStr),
                    new DataColumn("Ƶ��", dtStr),
                    new DataColumn("����", dtStr),
                    new DataColumn("����", dtStr),
                    new DataColumn("�÷�", dtStr),
                    new DataColumn("ҽ������", dtStr),
                    new DataColumn("�Ӽ�", dtBool),
                    new DataColumn("ִ��״̬", dtStr),// {A046F9F6-0367-4ab5-9966-9C3F29C38C41}
                    new DataColumn("��ʼʱ��", dtStr),
                    new DataColumn("����ʱ��", dtStr),
                    new DataColumn("����ҽ��", dtStr),
                    new DataColumn("ִ�п���", dtStr),
                    new DataColumn("ֹͣʱ��", dtStr),
                    new DataColumn("ֹͣҽ��", dtStr),
                    new DataColumn("��ע", dtStr),
                    new DataColumn("˳���", dtStr)
                });
                #endregion

                string beginDate = "", endDate = "", moDate = "";

                FS.HISFC.Models.Order.Inpatient.Order inOrder = null;
                FS.HISFC.Models.Order.OutPatient.Order outOrder = null;
                FS.HISFC.Models.Pharmacy.Item item = null;

                CheckOrderState(alOrder, serviceType);

                for (int i = 0; i < alOrder.Count; i++)
                {
                    #region סԺ����

                    if (serviceType == FS.HISFC.Models.Base.ServiceTypes.I)
                    {
                        inOrder = alOrder[i] as FS.HISFC.Models.Order.Inpatient.Order;

                        #region ��ʾҽ��
                        if (inOrder.Item != null)
                        {
                            if (inOrder.BeginTime == DateTime.MinValue)
                                beginDate = "";
                            else
                                beginDate = inOrder.BeginTime.ToString();

                            if (inOrder.EndTime == DateTime.MinValue)
                                endDate = "";
                            else
                                endDate = inOrder.EndTime.ToString();

                            if (inOrder.MOTime == DateTime.MinValue)
                                moDate = "";
                            else
                                moDate = inOrder.MOTime.ToString();

                            if (inOrder.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                            {
                                item = inOrder.Item as FS.HISFC.Models.Pharmacy.Item;
                                if (string.IsNullOrEmpty(inOrder.DoseUnit))
                                {
                                    inOrder.DoseUnit = item.DoseUnit;
                                }
                                dtMain.Rows.Add(new Object[] 
                                {  inOrder.ID,
                                    inOrder.Combo.ID,
                                    inOrder.Item.Name,
                                    inOrder.Item.Specs,															 
                                    "",
                                    inOrder.User03,
                                    //o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.')+item.DoseUnit ,	
                                    //��������С��λ��Ϊÿ��������λ��
                                    inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.')+inOrder.DoseUnit ,																 
                                    inOrder.Frequency.ID,
                                    inOrder.Qty.ToString()+inOrder.Unit,inOrder.HerbalQty,
                                    inOrder.Usage.Name,
                                    inOrder.OrderType.Name,
                                    inOrder.IsEmergency,
                                    "",
                                    beginDate,
                                    moDate,
                                    inOrder.ReciptDoctor.Name,
                                    inOrder.ExeDept.Name,
                                    endDate,
                                    inOrder.DCOper.Name,
                                    inOrder.Memo,inOrder.SortID
                                });
                            }
                            else if (inOrder.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
                            {
                                dtMain.Rows.Add(new Object[] 
                                { 
                                    inOrder.ID,
                                    inOrder.Combo.ID,
                                    inOrder.Item.Name,
                                    inOrder.Item.Specs,
                                    "",
                                    inOrder.User03,
                                    "" ,
                                    inOrder.Frequency.ID,
                                    inOrder.Qty.ToString()+inOrder.Unit,
                                    "","",
                                    inOrder.OrderType.Name,
                                    inOrder.IsEmergency,
                                    "",
                                    beginDate,
                                    moDate,
                                    inOrder.ReciptDoctor.Name,
                                    inOrder.ExeDept.Name,
                                    endDate,
                                    inOrder.DCOper.Name,
                                    inOrder.Memo,
                                    inOrder.SortID});
                            }
                            else
                            {
                                dtMain.Rows.Add(new Object[] 
                                { 
                                    inOrder.ID,
                                    inOrder.Combo.ID,
                                    inOrder.Item.Name,
                                    inOrder.Item.Specs,
                                    "",
                                    inOrder.User03,
                                    inOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.')+inOrder.DoseUnit,
                                    inOrder.Frequency.ID,
                                    inOrder.Qty.ToString()+inOrder.Unit,
                                    inOrder.HerbalQty,
                                    inOrder.Usage.Name,
                                    inOrder.OrderType.Name,
                                    inOrder.IsEmergency,
                                    "",
                                    beginDate,
                                    moDate,
                                    inOrder.ReciptDoctor.Name,
                                    inOrder.ExeDept.Name,
                                    endDate,
                                    inOrder.DCOper.Name,
                                    inOrder.Memo,
                                    inOrder.SortID});
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region ��������
                    else
                    {
                        outOrder = alOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;

                        #region ��ʾҽ��
                        if (outOrder.Item != null)
                        {
                            if (outOrder.BeginTime == DateTime.MinValue)
                                beginDate = "";
                            else
                                beginDate = outOrder.BeginTime.ToString();

                            if (outOrder.EndTime == DateTime.MinValue)
                                endDate = "";
                            else
                                endDate = outOrder.EndTime.ToString();

                            if (outOrder.MOTime == DateTime.MinValue)
                                moDate = "";
                            else
                                moDate = outOrder.MOTime.ToString();

                            if (outOrder.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                            {
                                item = outOrder.Item as FS.HISFC.Models.Pharmacy.Item;

                                dtMain.Rows.Add(new Object[] 
                                {  
                                    outOrder.ID,
                                    outOrder.Combo.ID,
                                    outOrder.Item.Name,
                                    outOrder.Item.Specs,
                                    "",
                                    outOrder.User03,
                                    //o.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.')+item.DoseUnit ,
                                    //��������С��λ��Ϊÿ��������λ��
                                    outOrder.DoseUnit == item.MinUnit ?outOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.')+outOrder.DoseUnit :outOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.')+item.DoseUnit ,
                                    outOrder.Frequency.ID,
                                    outOrder.Qty.ToString()+outOrder.Unit,outOrder.HerbalQty,outOrder.Usage.Name,
                                    "",
                                    outOrder.IsEmergency,
                                    "",
                                    beginDate,
                                    moDate,
                                    outOrder.ReciptDoctor.Name,
                                    outOrder.ExeDept.Name,endDate,
                                    outOrder.DCOper.Name,
                                    outOrder.Memo,
                                    outOrder.SortID
                                });

                            }
                            else if (outOrder.Item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
                            {
                                dtMain.Rows.Add(new Object[] 
                                { 
                                    outOrder.ID,
                                    outOrder.Combo.ID,
                                    outOrder.Item.Name,
                                    outOrder.Item.Specs,
                                    "",
                                    outOrder.User03,
                                    "" ,
                                    outOrder.Frequency.ID,
                                    outOrder.Qty.ToString()+outOrder.Unit,
                                    "",
                                    "",
                                    "",
                                    outOrder.IsEmergency,
                                    outOrder.User01,// {A046F9F6-0367-4ab5-9966-9C3F29C38C41}
                                    beginDate,moDate,
                                    outOrder.ReciptDoctor.Name,
                                    outOrder.ExeDept.Name,
                                    endDate,
                                    outOrder.DCOper.Name,
                                    outOrder.Memo,
                                    outOrder.SortID
                                });
                            }
                            else
                            {
                                dtMain.Rows.Add(new Object[] 
                                { 
                                    outOrder.ID,
                                    outOrder.Combo.ID, 
                                    outOrder.Item.Name,
                                    outOrder.Item.Specs,
                                    "",
                                    outOrder.User03,
                                    outOrder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.')+outOrder.DoseUnit,
                                    outOrder.Frequency.ID,
                                    outOrder.Qty.ToString()+outOrder.Unit,outOrder.HerbalQty,outOrder.Usage.Name,
                                    "",
                                    outOrder.IsEmergency,
                                    "",
                                    beginDate,
                                    moDate,
                                    outOrder.ReciptDoctor.Name,
                                    outOrder.ExeDept.Name,
                                    endDate,
                                    outOrder.DCOper.Name,
                                    outOrder.Memo,
                                    outOrder.SortID
                                });
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion

                switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
                {
                    case "SheetView":
                        FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                        o.RowCount = 0;
                        o.DataSource = myDataSet.Tables[0];
                        for (int i = 0; i < alOrder.Count; i++)
                        {
                            o.Rows[i].Tag = alOrder[i];

                            //����ҽ��״̬������ɫ
                            FS.HISFC.Models.Order.Order tmpOrder = alOrder[i] as FS.HISFC.Models.Order.Order;

                            switch (tmpOrder.Status)
                            {
                                case 0:
                                    o.RowHeader.Rows[i].BackColor = Color.FromArgb(128, 255, 128);
                                    break;
                                case 1:
                                    o.RowHeader.Rows[i].BackColor = Color.FromArgb(106, 174, 242);
                                    break;
                                case 2:
                                    o.RowHeader.Rows[i].BackColor = Color.FromArgb(243, 230, 105);
                                    break;
                                case 3:
                                    o.RowHeader.Rows[i].BackColor = Color.FromArgb(248, 120, 222);
                                    break;
                                default:
                                    o.RowHeader.Rows[i].BackColor = Color.Black;
                                    break;
                            }

                            //{5C7887F1-A4D5-4a66-A814-18D45367443E}
                            if (tmpOrder.QuitFlag == 1)
                            {
                                o.RowHeader.Rows[i].ForeColor = Color.Red;
                                o.Rows[i].ForeColor = Color.Red;
                            }
                        }
                        #region ������
                        o.Columns[0].Visible = false;
                        o.Columns[1].Visible = false;
                        //2 ("ҽ������", dtStr),3("���", dtStr),4 ���,5���ʱ��,6("ÿ�μ���", dtStr),
                        //7("Ƶ��", dtStr),8("����", dtStr),9("����", dtStr),
                        //10("�÷�", dtStr),11("ҽ������", dtStr),12("�Ӽ�", dtBool),
                        //13("��ʼʱ��", dtStr),14("����ʱ��", dtStr),15("����ҽ��", dtStr),
                        //16("ִ�п���", dtStr),17("ֹͣʱ��", dtStr),18("ֹͣҽ��", dtStr),
                        //19("��ע", dtStr),20("˳���", dtStr)});
                        o.Columns[2].Width = 300;
                        o.Columns[3].Width = 50;
                        o.Columns[4].Width = 40;
                        o.Columns[5].Width = 80;
                        o.Columns[5].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                        o.Columns[6].Width = 100;
                        o.Columns[7].Width = 80;
                        o.Columns[8].Width = 80;
                        o.Columns[9].Width = 60;
                        o.Columns[10].Width = 80;
                        o.Columns[11].Width = 60;
                        o.Columns[12].Width = 40;
                        o.Columns[13].Width = 80;
                        o.Columns[14].Width = 80;
                        o.Columns[15].Width = 80;
                        o.Columns[16].Width = 80;
                        o.Columns[17].Width = 80;
                        o.Columns[18].Width = 80;
                        o.Columns[19].Width = 80;
                        o.Columns[20].Width = 80;
                        o.Columns[21].Width = 30;
                        if (type == 1)//����
                        {
                            o.Columns[5].Visible = true;
                        }
                        else
                        {
                            o.Columns[5].Visible = false;
                        }
                        #endregion

                        Function.DrawCombo(o, 1, 4);
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ShowOrder" + ex.Message);
                return;
            }
        }

        public static void DrawCombo(object sender, int column, int DrawColumn, int ChildViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = "", curComboNo = "";
                    if (ChildViewLevel == 0)
                    {
                        for (i = 0; i < o.RowCount; i++)
                        {
                            #region "��"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //��ͷ
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "��";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, DrawColumn].Text = "��";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "��";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "";
                            //o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Red;
                            #endregion
                        }
                    }
                    else if (ChildViewLevel == 1)
                    {
                        for (int m = 0; m < o.RowCount; m++)
                        {
                            FarPoint.Win.Spread.SheetView c = o.GetChildView(m, 0);
                            for (int j = 0; j < c.RowCount; j++)
                            {
                                #region "��"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //��ͷ
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "��";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "��";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "��";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "";
                                //c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
            }

        }


        public static void DrawComboLeft(object sender, int column, int DrawColumn, int ChildViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = "", curComboNo = "";
                    if (ChildViewLevel == 0)
                    {
                        for (i = 0; i < o.RowCount; i++)
                        {
                            #region "��"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //��ͷ
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "��";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, DrawColumn].Text = "��";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "��";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "";
                            //o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Red;
                            #endregion
                        }
                    }
                    else if (ChildViewLevel == 1)
                    {
                        for (int m = 0; m < o.RowCount; m++)
                        {
                            FarPoint.Win.Spread.SheetView c = o.GetChildView(m, 0);
                            for (int j = 0; j < c.RowCount; j++)
                            {
                                #region "��"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //��ͷ
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "��";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "��";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "��";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "";
                                //c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
            }

        }

        /// <summary>
        /// ����Ϻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// 		/// <param name="DrawColumn"></param>
        public static void DrawCombo(object sender, int column, int DrawColumn)
        {
            DrawCombo(sender, column, DrawColumn, 0);
        }

        /// <summary>
        /// ����Ϻ�(��ࣩ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// 		/// <param name="DrawColumn"></param>
        public static void DrawComboLeft(object sender, int column, int DrawColumn)
        {
            DrawComboLeft(sender, column, DrawColumn, 0);
        }

        #endregion

        #region ���ֽ�Ŵ�С
        protected static FS.HISFC.BizLogic.Manager.PageSize manager = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// ���ô�ӡֽ��
        /// ֻ�Է�Ĭ��ֽ��A4��Ҫ��������
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="print"></param>
        public static void GetPageSize(FS.HISFC.Models.Base.PageSize p, ref FS.FrameWork.WinForms.Classes.Print print)
        {
            print.SetPageSize(p);
        }

        /// <summary>
        /// ���ô�ӡֽ��
        /// ֻ�Է�Ĭ��ֽ��A4��Ҫ��������
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="print"></param>
        public static void GetPageSize(string ID, ref FS.FrameWork.WinForms.Classes.Print print)
        {

            FS.HISFC.Models.Base.PageSize p = manager.GetPageSize(ID);
            if (p == null || p.Name.Trim() == "") return;
            print.SetPageSize(p);
            //manager = null;
        }
        /// <summary>
        /// ���ô�ӡֽ��
        /// ��Ҫ��Transaction
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="print"></param>
        /// <param name="t"></param>
        public static void GetPageSize(string ID, ref FS.FrameWork.WinForms.Classes.Print print, ref FS.FrameWork.Management.Transaction t)
        {
            try
            {
                manager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            }
            catch { }
            FS.HISFC.Models.Base.PageSize p = manager.GetPageSize(ID);
            if (p == null || p.Name.Trim() == "") return;
            print.SetPageSize(p);
            //manager = null;
        }
        /// <summary>
        /// ��ô�ӡֽ��
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Base.PageSize GetPageSize(string ID)
        {
            return manager.GetPageSize(ID);
        }
        #endregion

        #region "ISql interface����"
        //private static FS.FrameWork.Management.Interface sql = null;
        ///// <summary>
        ///// sql סԺ������
        ///// </summary>
        //public static FS.FrameWork.Management.Interface ISql
        //{
        //    get
        //    {
        //        if (sql == null)
        //        {
        //            sql = new FS.FrameWork.Management.Interface();
        //            string fileName = TemplateDesignerHost.Function.SystemPath + "PATIENTINFO.xml";
        //            sql.ReadXML(fileName);
        //            try
        //            {
        //                sql.SetParam(FS.FrameWork.Management.Connection.Operator.ID, "");
        //                sql.RefreshVariant();
        //            }
        //            catch { }
        //            return sql;
        //        }
        //        else
        //        {
        //            return sql;
        //        }
        //    }
        //}
        //private static FS.FrameWork.Management.Interface sqlOutPatient = null;
        ///// <summary>
        ///// sql ���������
        ///// </summary>
        //public static FS.FrameWork.Management.Interface ISqlOutPatient
        //{
        //    get
        //    {
        //        if (sqlOutPatient == null)
        //        {
        //            sqlOutPatient = new FS.FrameWork.Management.Interface();
        //            string fileName = TemplateDesignerHost.Function.SystemPath + "OUTPATIENTINFO.xml";
        //            sqlOutPatient.ReadXML(fileName);
        //            try
        //            {
        //                sqlOutPatient.SetParam(FS.FrameWork.Management.Connection.Operator.ID, "");
        //                sqlOutPatient.RefreshVariant();
        //            }
        //            catch { }
        //            return sqlOutPatient;
        //        }
        //        else
        //        {
        //            return sqlOutPatient;
        //        }
        //    }
        //}
        //private static FS.FrameWork.Management.Interface sqlCheck = null;
        ///// <summary>
        ///// sql ��������
        ///// </summary>
        //public static FS.FrameWork.Management.Interface ISqlCheck
        //{
        //    get
        //    {
        //        if (sqlCheck == null)
        //        {
        //            sqlCheck = new FS.FrameWork.Management.Interface();
        //            string fileName = TemplateDesignerHost.Function.SystemPath + "CHECKPATIENTINFO.xml";
        //            sqlCheck.ReadXML(fileName);
        //            try
        //            {
        //                sqlCheck.SetParam(FS.FrameWork.Management.Connection.Operator.ID, "");
        //                sqlCheck.RefreshVariant();
        //            }
        //            catch { }
        //            return sqlCheck;
        //        }
        //        else
        //        {
        //            return sqlCheck;
        //        }
        //    }
        //}

        //private static FS.FrameWork.Management.Interface sqlOther = null;
        ///// <summary>
        ///// sql ��������
        ///// </summary>
        //public static FS.FrameWork.Management.Interface ISqlOther
        //{
        //    get
        //    {
        //        if (sqlOther == null)
        //        {
        //            sqlOther = new FS.FrameWork.Management.Interface();
        //            string fileName = FS.FrameWork.Management.Connection.SystemPath + "\\OtherPATIENTINFO.xml";
        //            sqlOther.ReadXML(fileName);
        //            try
        //            {
        //                sqlOther.SetParam(FS.FrameWork.Management.Connection.Operator.ID, "");
        //                sqlOther.RefreshVariant();
        //            }
        //            catch { }
        //            return sqlOther;
        //        }
        //        else
        //        {
        //            return sqlOther;
        //        }
        //    }
        //}

        #endregion


        #region ���Ӳ���ʹ�õĵ���
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="patientInfo"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        //public static int EMRPrint(Control parentControl, FS.HISFC.Models.RADT.PatientInfo patientInfo, string type)
        //{
        //    TemplateDesignerApplication.ucDataFileLoader ucDataFileLoader1 = new TemplateDesignerApplication.ucDataFileLoader();
        //    string[] param = { FS.FrameWork.Management.Connection.Operator.ID, patientInfo.ID };
        //    ucDataFileLoader1.Location = new Point(0, 2000);
        //    ucDataFileLoader1.ISql = FS.HISFC.Components.Common.Classes.Function.ISql;
        //    ucDataFileLoader1.InitSql("", param);
        //    ucDataFileLoader1.Init(type, patientInfo.ID);//3 ��Ժ֪ͨ��
        //    ucDataFileLoader1.index1 = patientInfo.ID;
        //    ucDataFileLoader1.index2 = patientInfo.Name;
        //    ucDataFileLoader1.IsShowInterface = false;
        //    ucDataFileLoader1.RefreshForm();
        //    ucDataFileLoader1.Visible = true;
        //    parentControl.Controls.Add(ucDataFileLoader1);

        //    FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
        //    int i = p.PrintPage(0, 0, ucDataFileLoader1.CurrntPanel);
        //    ucDataFileLoader1.Visible = false;
        //    return i;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="parentControl"></param>
        ///// <param name="patientInfo"></param>
        ///// <param name="type"></param>
        ///// <param name="printer"></param>
        ///// <returns></returns>
        //public static int EMRPrint(Control parentControl, FS.HISFC.Models.RADT.PatientInfo patientInfo, string type, FS.FrameWork.WinForms.Classes.Print printer)
        //{
        //    TemplateDesignerApplication.ucDataFileLoader ucDataFileLoader1 = new TemplateDesignerApplication.ucDataFileLoader();
        //    string[] param = { FS.FrameWork.Management.Connection.Operator.ID, patientInfo.ID };
        //    ucDataFileLoader1.Location = new Point(0, 2000);
        //    ucDataFileLoader1.ISql = FS.HISFC.Components.Common.Classes.Function.ISql;
        //    ucDataFileLoader1.InitSql("", param);
        //    ucDataFileLoader1.Init(type, patientInfo.ID);//3 ��Ժ֪ͨ��
        //    ucDataFileLoader1.index1 = patientInfo.ID;
        //    ucDataFileLoader1.index2 = patientInfo.Name;
        //    ucDataFileLoader1.IsShowInterface = false;
        //    ucDataFileLoader1.RefreshForm();
        //    ucDataFileLoader1.Visible = true;
        //    parentControl.Controls.Add(ucDataFileLoader1);
        //    int i = printer.PrintPage(0, 0, ucDataFileLoader1.CurrntPanel);
        //    ucDataFileLoader1.Visible = false;
        //    return i;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="parentControl"></param>
        ///// <param name="patientInfo"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public static int EMRPrintPreview(Control parentControl, FS.HISFC.Models.RADT.PatientInfo patientInfo, string type)
        //{
        //    TemplateDesignerApplication.ucDataFileLoader ucDataFileLoader1 = new TemplateDesignerApplication.ucDataFileLoader();
        //    string[] param = { FS.FrameWork.Management.Connection.Operator.ID, patientInfo.ID };
        //    ucDataFileLoader1.Location = new Point(0, 2000);
        //    ucDataFileLoader1.ISql = FS.HISFC.Components.Common.Classes.Function.ISql;
        //    ucDataFileLoader1.InitSql("", param);
        //    ucDataFileLoader1.Init(type, patientInfo.ID);//3 ��Ժ֪ͨ��
        //    ucDataFileLoader1.index1 = patientInfo.ID;
        //    ucDataFileLoader1.index2 = patientInfo.Name;
        //    ucDataFileLoader1.IsShowInterface = false;
        //    ucDataFileLoader1.RefreshForm();
        //    ucDataFileLoader1.Visible = true;
        //    parentControl.Controls.Add(ucDataFileLoader1);
        //    FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
        //    int i = p.PrintPreview(ucDataFileLoader1.CurrntPanel);
        //    ucDataFileLoader1.Visible = false;
        //    return i;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="parentControl"></param>
        ///// <param name="patientInfo"></param>
        ///// <param name="type"></param>
        ///// <param name="printer"></param>
        ///// <returns></returns>
        //public static int EMRPrintPreview(Control parentControl, FS.HISFC.Models.RADT.PatientInfo patientInfo, string type, FS.FrameWork.WinForms.Classes.Print printer)
        //{
        //    TemplateDesignerApplication.ucDataFileLoader ucDataFileLoader1 = new TemplateDesignerApplication.ucDataFileLoader();
        //    string[] param = { FS.FrameWork.Management.Connection.Operator.ID, patientInfo.ID };
        //    ucDataFileLoader1.Location = new Point(0, 2000);
        //    ucDataFileLoader1.ISql = FS.HISFC.Components.Common.Classes.Function.ISql;
        //    ucDataFileLoader1.InitSql("", param);
        //    ucDataFileLoader1.Init(type, patientInfo.ID);//3 ��Ժ֪ͨ��
        //    ucDataFileLoader1.index1 = patientInfo.ID;
        //    ucDataFileLoader1.index2 = patientInfo.Name;
        //    ucDataFileLoader1.IsShowInterface = false;
        //    ucDataFileLoader1.RefreshForm();
        //    ucDataFileLoader1.Visible = true;
        //    parentControl.Controls.Add(ucDataFileLoader1);
        //    int i = printer.PrintPreview(ucDataFileLoader1.CurrntPanel);
        //    ucDataFileLoader1.Visible = false;
        //    return i;
        //}
        ///// <summary>
        ///// /
        ///// </summary>
        ///// <param name="parentControl"></param>
        ///// <param name="patientInfo"></param>
        ///// <param name="type"></param>
        //public static void EMRShow(Control parentControl, FS.HISFC.Models.RADT.PatientInfo patientInfo, string type)
        //{
        //    TemplateDesignerApplication.ucDataFileLoader ucDataFileLoader1 = new TemplateDesignerApplication.ucDataFileLoader();
        //    string[] param = { FS.FrameWork.Management.Connection.Operator.ID, patientInfo.ID };
        //    ucDataFileLoader1.Location = new Point(0, 0);
        //    ucDataFileLoader1.Dock = DockStyle.Fill;
        //    ucDataFileLoader1.ISql = FS.HISFC.Components.Common.Classes.Function.ISql;
        //    ucDataFileLoader1.InitSql("", param);
        //    ucDataFileLoader1.Init(type, patientInfo.ID);
        //    ucDataFileLoader1.index1 = patientInfo.ID;
        //    ucDataFileLoader1.index2 = patientInfo.Name;
        //    ucDataFileLoader1.IsShowInterface = false;
        //    ucDataFileLoader1.RefreshForm();
        //    ucDataFileLoader1.Visible = true;
        //    parentControl.Controls.Add(ucDataFileLoader1);
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="parentControl"></param>
        ///// <param name="patientInfo"></param>
        ///// <param name="type"></param>
        ///// <param name="bShowInterface"></param>
        //public static void EMRShow(Control parentControl, FS.HISFC.Models.RADT.PatientInfo patientInfo, string type, bool bShowInterface)
        //{
        //    TemplateDesignerApplication.ucDataFileLoader ucDataFileLoader1 = new TemplateDesignerApplication.ucDataFileLoader();
        //    string[] param = { FS.FrameWork.Management.Connection.Operator.ID, patientInfo.ID };
        //    ucDataFileLoader1.Location = new Point(0, 0);
        //    ucDataFileLoader1.Dock = DockStyle.Fill;
        //    ucDataFileLoader1.ISql = FS.HISFC.Components.Common.Classes.Function.ISql;
        //    ucDataFileLoader1.InitSql("", param);
        //    ucDataFileLoader1.Init(type, patientInfo.ID);
        //    ucDataFileLoader1.index1 = patientInfo.ID;
        //    ucDataFileLoader1.index2 = patientInfo.Name;
        //    ucDataFileLoader1.IsShowInterface = bShowInterface;
        //    ucDataFileLoader1.RefreshForm();
        //    ucDataFileLoader1.Visible = true;
        //    parentControl.Controls.Add(ucDataFileLoader1);
        //}
        #endregion

        /// <summary>
        /// ���������ļ� feeSetting.xml �ṩ�������뷨
        /// </summary>
        /// <returns></returns>
        public static int CreateFeeSetting()
        {
            try
            {
                XmlDocument docXml = new XmlDocument();
                if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
                {
                    System.IO.File.Delete(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                }
                else
                {
                    System.IO.Directory.CreateDirectory(FS.FrameWork.WinForms.Classes.Function.SettingPath);
                }
                docXml.LoadXml("<setting>  </setting>");
                XmlNode root = docXml.DocumentElement;

                XmlElement elem1 = docXml.CreateElement("���뷨");
                System.Xml.XmlComment xmlcomment;
                xmlcomment = docXml.CreateComment("��ѯ��ʽ0:ƴ���� 1:����� 2:�Զ����� 3:������ 4:Ӣ��");
                elem1.SetAttribute("currentmodel", "0");
                root.AppendChild(xmlcomment);
                root.AppendChild(elem1);

                XmlElement elem2 = docXml.CreateElement("IME");
                System.Xml.XmlComment xmlcomment2;
                xmlcomment2 = docXml.CreateComment("��ǰĬ�����뷨");
                elem2.SetAttribute("currentmodel", "�Ϲ�ƴ�����뷨");
                root.AppendChild(xmlcomment2);
                root.AppendChild(elem2);

                docXml.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("CreateFeeSetting" + ex.Message);
                return -1;
            }
            return 1;
        }

        static Hashtable hsUCULItem = null;

        /// <summary>
        /// �Ƿ�����������
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool isUCUCForLong(FS.HISFC.Models.Base.Item item)
        {
            if ("UC��UL".Contains(item.SysClass.ID.ToString()))
            {
                if (hsUCULItem == null)
                {
                    hsUCULItem = new Hashtable();
                    FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    ArrayList alUCUL = interMgr.GetConstantList("LongUCUL");
                    foreach (FS.HISFC.Models.Base.Const con in alUCUL)
                    {
                        if (con.IsValid && !hsUCULItem.Contains(con.ID))
                        {
                            hsUCULItem.Add(con.ID, null);
                        }
                    }
                }
                if (hsUCULItem != null
                    && hsUCULItem.Contains(item.ID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ���������ļ� feeSettingOutpatientFee.xml �ṩ�������뷨
        /// </summary>
        /// <returns></returns>
        public static int CreateFeeSettingOutpatient()
        {
            try
            {
                XmlDocument docXml = new XmlDocument();
                if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSettingOutpatientFee.xml"))
                {
                    System.IO.File.Delete(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSettingOutpatientFee.xml");
                }
                else
                {
                    System.IO.Directory.CreateDirectory(FS.FrameWork.WinForms.Classes.Function.SettingPath);
                }
                docXml.LoadXml("<setting>  </setting>");
                XmlNode root = docXml.DocumentElement;

                XmlElement elem1 = docXml.CreateElement("����");
                System.Xml.XmlComment xmlcomment;
                xmlcomment = docXml.CreateComment("��ѯ��ʽ0:ƴ���� 1:����� 2:�Զ�����");
                elem1.SetAttribute("currentmodel", "0");
                root.AppendChild(xmlcomment);
                root.AppendChild(elem1);

                docXml.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSettingOutpatientFee.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("CreateFeeSetting" + ex.Message);
                return -1;
            }
            return 1;
        }


        #region ���ݿ����

        /// <summary>
        /// ��ѯ�ۼ�����Sql
        /// </summary>
        private static string insertSql = @"INSERT INTO COM_QUERY_LOG (
	REPORT_ID ,                             --����ID
	REPORT_NAME ,                           --��������
	REPORT_CONDITION ,                      --��ѯ����
	QUERY_OPER ,                            --��ѯ��
	LOGIN_FUN ,                             --��½������
	LOGIN_DEPT ,                            --��½����
	MACHINE_NAME ,                          --��������
	MACHINE_IP ,                            --��½IP
	QUERY_DATE                              --��ѯʱ��
)  VALUES(
	'{0}' ,       --����ID
	'{1}' ,       --��������
	'{2}' ,       --��ѯ����
	'{3}' ,       --��ѯ��
	'{4}' ,       --��½������
	'{5}' ,       --��½����
	'{6}' ,       --��������
	'{7}' ,       --��½IP
	to_date('{8}','yyyy-mm-dd HH24:mi:ss')         --��ѯʱ��
) 
";

        #endregion



        /// <summary>
        /// ͼ��֪ͨ
        /// </summary>
        static System.Windows.Forms.NotifyIcon notify = null;

        /// <summary>
        /// ͼ��֪ͨ�¼�
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="tipTitle"></param>
        /// <param name="tipText"></param>
        /// <param name="tipIcon"></param>
        public static void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            if (notify == null)
            {
                notify = new System.Windows.Forms.NotifyIcon();
                notify.Icon = Properties.Resources.HIS;
            }
            notify.Visible = true;
            notify.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
        }

        /// <summary>
        /// ������Ϣ��ѯ�ۼ���������
        /// </summary>
        /// <param name="reportID">����ID</param>
        /// <param name="reportName">��������</param>
        /// <param name="reportCondition">��ѯ����</param>
        public static void SaveQueryLog(string reportID, string reportName, string reportCondition)
        {
            FS.HISFC.Models.Base.Employee person = new FS.HISFC.Models.Base.Employee();
            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

            string hosName = System.Net.Dns.GetHostName();
            string ip = System.Net.Dns.GetHostEntry(hosName).AddressList[0].ToString();

            person = dataManager.Operator as FS.HISFC.Models.Base.Employee;
            if (person == null)
            {
                return;
            }

            //person.ID = person.ID;							//����ԱID
            //person.Name = person.Name;						//����Ա����
            //person.Memo = person.Dept.ID;					//��½������
            //person.User01 = person.Dept.Name;				//��½����ID
            //person.User02 = hosName;						//��������
            //person.User03 = ip;								//IP
            //person.CurrentGroup.Name = person.CurrentGroup.Name;	//��½����
            //person.CurrentGroup.ID = person.CurrentGroup.ID;		//�Ǽ���ID

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(dataManager.Connection);
            //t.BeginTransaction();
            dataManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            string sql = string.Format(Function.insertSql,
                                        reportID,
                                        reportName,
                                        reportCondition,
                                        person.ID,
                                        person.CurrentGroup.Name,
                                        person.Dept.ID,
                                        hosName,
                                        ip,
                                        dataManager.GetDateTimeFromSysDateTime().ToString());
            int parm = dataManager.ExecNoQuery(sql);
            if (parm == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            if (parm > 0)
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                return;
            }
        }

        #region ���뷨ȫ���л��ɰ��

        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr himc);
        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr himc, bool b);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr himc, ref int lpdw, ref int lpdw2);
        [DllImport("imm32.dll")]
        public static extern int ImmSimulateHotKey(IntPtr hwnd, int lngHotkey);
        public const int IME_CMODE_FULLSHAPE = 0x8;
        public const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;

        /// <summary>
        /// ���ô���ؼ��е����뷨״̬Ϊ���
        /// </summary>
        /// <param name="frm">��ǰ����</param>
        public static void SetIme(Form frm)
        {
            frm.Paint += new PaintEventHandler(frm_Paint);
            ChangeAllControl(frm);
        }
        /// <summary>
        /// ���ÿؼ������뷨״̬Ϊ���
        /// </summary>
        /// <param name="ctl">�ؼ�</param>
        public static void SetIme(Control ctl)
        {
            ChangeAllControl(ctl);
        }
        /// <summary>
        /// ���ö�������뷨״̬Ϊ���
        /// </summary>
        /// <param name="Handel">������</param>
        public static void SetIme(IntPtr Handel)
        {
            ChangeControlIme(Handel);
        }
        private static void ChangeAllControl(Control ctl)
        {
            //�ڿؼ��ĵ�Enter�¼��д������������뷨״̬


            ctl.Enter += new EventHandler(ctl_Enter);
            //�����ӿؼ���ʹÿ���ؼ�������Enter��ί�д���


            foreach (Control ctlChild in ctl.Controls)
            {
                ChangeAllControl(ctlChild);
            }
        }

        static void frm_Paint(object sender, PaintEventArgs e)
        {
            ChangeControlIme(sender);
        }
        //�ؼ���Enter�������
        static void ctl_Enter(object sender, EventArgs e)
        {
            ChangeControlIme(sender);
        }
        private static void ChangeControlIme(object sender)
        {
            Control ctl = (Control)sender;
            ChangeControlIme(ctl.Handle);
        }
        /// <summary>
        /// �������������������������뷨��ȫ�ǰ��״̬


        /// </summary>
        /// <param name="h"></param>
        private static void ChangeControlIme(IntPtr h)
        {
            IntPtr HIme = ImmGetContext(h);
            if (ImmGetOpenStatus(HIme)) //������뷨���ڴ�״̬
            {
                int iMode = 0;
                int iSentence = 0;
                bool bSuccess = ImmGetConversionStatus(HIme, ref iMode, ref iSentence); //�������뷨��Ϣ
                if (bSuccess)
                {
                    if ((iMode & IME_CMODE_FULLSHAPE) > 0) //�����ȫ��
                    {
                        ImmSimulateHotKey(h, IME_CHOTKEY_SHAPE_TOGGLE); //ת���ɰ��


                    }
                }
            }
        }

        #endregion

        //{ED51E97B-B752-4c32-BD93-F80209A24879}���������������

        #region ������Ŀʹ��Ƶ������

        /// <summary>
        /// ����xml �ļ���ַ
        /// </summary>
        public static string SORT_FILE_PATH = Application.StartupPath + "\\Setting\\Profiles\\itemSort.xml";

        /// <summary>
        /// ����XML �ļ�λ��
        /// </summary>
        public static string FEE_SET_PATH = Application.StartupPath + "\\Setting\\Profiles\\feeSetting.xml";

        /// <summary>
        /// ��ǰxml��ʵ��
        /// </summary>
        public static XmlDocument xmlDoc = null;

        /// <summary>
        /// ��ǰ����xmlʵ��
        /// </summary>
        public static XmlDocument xmlSetDoc = null;

        /// <summary>
        /// �Ƿ���ڹ���xml
        /// </summary>
        /// <returns>�ɹ� true ʧ�� false</returns>
        private static bool IsExistSettingFeeXML()
        {
            try
            {
                return System.IO.File.Exists(FEE_SET_PATH);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ��õ�ǰ��Ŀ���������,���û���ҵ�,��Ĭ��Ϊ0
        /// </summary>
        /// <param name="xDoc">��ǰxml</param>
        /// <param name="itemCode">��Ŀ����</param>
        /// <returns>�ɹ� ��ǰ��Ŀ��������� û���ҵ����� 0</returns>
        public static string GetDeptValue(XmlDocument xDoc)
        {
            XmlNode root = xDoc.SelectSingleNode("setting//defaultDept");

            if (root != null)
            {
                return root.InnerText;
            }

            return null;
        }

        /// <summary>
        /// ���õ�ǰĬ��ִ��ҩ��
        /// </summary>
        /// <param name="deptCode">���Ҵ���</param>
        public static void SetDefaultDeptXML(string deptCode)
        {
            if (xmlSetDoc == null)
            {
                xmlSetDoc = GetSetXML();
            }

            if (xmlDoc == null)
            {
                return;
            }

            XmlNode root = xmlSetDoc.SelectSingleNode("setting");

            XmlNode xFind = root.SelectSingleNode("defaultDept");
            if (xFind == null)//û���ҵ�,Ҫ����һ���ڵ�
            {
                InsertSettingNewItem(deptCode, xmlSetDoc);
            }
            else//���ĵ�ǰ��Ŀ��ʹ��Ƶ�� + 1 
            {
                ModifySettingItem(deptCode, xmlSetDoc, xFind);
            }
        }

        /// <summary>
        /// ���һ���µĽڵ�,���ĵ�һ��Ϊ����,��Ŀ����ǰ�����һ����д��ĸ"A"
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="xDoc">��ǰxml</param>
        private static void InsertSettingNewItem(string deptCode, XmlDocument xDoc)
        {
            if (xDoc == null)
            {
                return;
            }

            XmlElement xe = xDoc.CreateElement("defaultDept");
            xe.InnerText = deptCode;

            XmlNode root = xDoc.SelectSingleNode("Setting");

            root.AppendChild(xe);

            xDoc.Save(FEE_SET_PATH);
        }

        /// <summary>
        /// �޸�һ���ڵ�
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="xDoc">��ǰxml</param>
        /// <param name="xe">��ǰ�ڵ�</param>
        private static void ModifySettingItem(string deptCode, XmlDocument xDoc, XmlNode xe)
        {
            if (xDoc == null)
            {
                return;
            }

            xe.InnerText = deptCode;

            xDoc.Save(FEE_SET_PATH);
        }

        /// <summary>
        /// �Ƿ���ڹ���xml
        /// </summary>
        /// <returns>�ɹ� true ʧ�� false</returns>
        private static bool IsExistSettingXML()
        {
            try
            {
                return System.IO.File.Exists(SORT_FILE_PATH);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ��õ�ǰsortXML
        /// </summary>
        /// <returns>�ɹ� ����xml ʧ�� null</returns>
        public static XmlDocument GetSortXML()
        {
            if (!IsExistSettingXML())
            {
                return null;
            }

            XmlDocument xdoc = new XmlDocument();

            xdoc.Load(SORT_FILE_PATH);

            return xdoc;
        }

        /// <summary>
        /// ��õ�ǰsortXML
        /// </summary>
        /// <returns>�ɹ� ����xml ʧ�� null</returns>
        public static XmlDocument GetSetXML()
        {
            if (!IsExistSettingXML())
            {
                return null;
            }

            XmlDocument xdoc = new XmlDocument();

            xdoc.Load(FEE_SET_PATH);

            return xdoc;
        }

        /// <summary>
        /// ���һ���µĽڵ�,���ĵ�һ��Ϊ����,��Ŀ����ǰ�����һ����д��ĸ"A"
        /// </summary>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="xDoc">��ǰxml</param>
        private static void InsertNewItem(string itemCode, XmlDocument xDoc)
        {
            if (xDoc == null)
            {
                return;
            }

            XmlElement xe = xDoc.CreateElement("A" + itemCode);
            xe.InnerText = "1";

            XmlNode root = xDoc.SelectSingleNode("Column");

            root.AppendChild(xe);

            xDoc.Save(SORT_FILE_PATH);
        }

        /// <summary>
        /// �޸�һ���ڵ�
        /// </summary>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="xDoc">��ǰxml</param>
        /// <param name="xe">��ǰ�ڵ�</param>
        private static void ModifyItem(string itemCode, XmlDocument xDoc, XmlNode xe)
        {
            if (xDoc == null)
            {
                return;
            }

            xe.InnerText = (FS.FrameWork.Function.NConvert.ToInt32(xe.InnerText) + 1).ToString();

            xDoc.Save(SORT_FILE_PATH);
        }

        /// <summary>
        /// ���õ�ǰ�������Ŀ������Ŀ����һ������ڵ�,����Ѿ�����,�����������+1
        /// </summary>
        /// <param name="itemCode">��Ŀ����</param>
        public static void SetSortItemXML(string itemCode)
        {
            if (xmlDoc == null)
            {
                xmlDoc = GetSortXML();
            }

            if (xmlDoc == null)
            {
                return;
            }

            XmlNode root = xmlDoc.SelectSingleNode("Column");

            XmlNode xFind = root.SelectSingleNode("A" + itemCode);
            if (xFind == null)//û���ҵ�,Ҫ����һ���ڵ�
            {
                InsertNewItem(itemCode, xmlDoc);
            }
            else//���ĵ�ǰ��Ŀ��ʹ��Ƶ�� + 1 
            {
                ModifyItem(itemCode, xmlDoc, xFind);
            }
        }

        /// <summary>
        /// ��õ�ǰ��Ŀ���������,���û���ҵ�,��Ĭ��Ϊ0
        /// </summary>
        /// <param name="xDoc">��ǰxml</param>
        /// <param name="itemCode">��Ŀ����</param>
        /// <returns>�ɹ� ��ǰ��Ŀ��������� û���ҵ����� 0</returns>
        public static int GetSortValue(XmlDocument xDoc, string itemCode)
        {
            if (xDoc == null)
            {
                return 0;
            }

            XmlNode root = xDoc.SelectSingleNode("Column");

            XmlNode xFind = root.SelectSingleNode("A" + itemCode);
            if (xFind != null)
            {
                return FS.FrameWork.Function.NConvert.ToInt32(xFind.InnerText);
            }

            return 0;
        }

        #endregion

        //{ED51E97B-B752-4c32-BD93-F80209A24879}������������������

        #region MQǶ�뵽��ʿվ
        //{839D3A8A-49FA-4d47-A022-6196EB1A5715} ��MQǶ��ϵͳ��ҽ��վ����ʱ���Զ�֪ͨ��ʿվ����ʿվҽ���������Զ���Ӧ������QQ��ͷ��ζ��������Ը���������ʾ

        public static string currentPath = ".";
        public static string strLabel = "";

        /// <summary>
        /// ��¼ɾ�����MQ��Ϣ
        /// </summary>
        /// <param name="sRecord"></param>
        public static string Label(string sRecord)
        {
            FileStream fs;
            string path = currentPath + "\\Label.txt";
            if (!File.Exists(path))
            {
                fs = File.Create(path);
                fs.Close();
            }
            StreamReader sr = File.OpenText(path);
            strLabel = sr.ReadToEnd();
            strLabel = strLabel.Trim('\0') + '\0';
            sr.Close();

            if (!string.IsNullOrEmpty(sRecord))
            {
                strLabel += sRecord + "\0";

                fs = File.OpenWrite(path);
                Byte[] info = new UTF8Encoding(true).GetBytes(strLabel);
                fs.Write(info, 0, info.Length);
                fs.Close();
            }

            return strLabel;
        }

        /// <summary>
        /// ɾ���������ʾ
        /// </summary>
        /// <param name="inpatientNO"></param>
        public static void DelLabel(string inpatientNO)
        {
            Label("");

            string sRecord = "";
            string[] messageArr = strLabel.Split('\0');
            ArrayList alInpatienNO = new ArrayList();

            int start = 0;
            int end = 0;
            string inpatientNOTemp = "";

            foreach (string messageInfo in messageArr)
            {
                if (messageInfo != "")
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

                    start = messageInfo.IndexOf("סԺ�ţ�");
                    end = messageInfo.IndexOf("\n���ţ�");
                    if (start > -1 && end > -1 && end - start - "סԺ�ţ�".Length > 0 && start - "סԺ�ţ�".Length > -1)
                    {
                        inpatientNOTemp = messageInfo.Substring(start + "סԺ�ţ�".Length, end - start - "סԺ�ţ�".Length);
                    }

                    //if (messageInfo.Substring(messageInfo.IndexOf("ZY"), 14) == inpatientNO)
                    if (inpatientNOTemp == inpatientNO)
                    {
                        sRecord += messageInfo + '\0';
                    }
                }
            }

            if (strLabel.IndexOf(sRecord) < 0)
            {
                return;
            }
            strLabel = strLabel.Remove(strLabel.LastIndexOf(sRecord), sRecord.Length);

            if (File.Exists(currentPath + "\\Label.txt"))
            {
                File.Delete(currentPath + "\\Label.txt");
            }
            Label(strLabel);
        }

        #endregion

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public static int QueryComPatientInfo(ref FS.HISFC.Models.RADT.PatientInfo patient)
        {
            ucQueryPatientInfo uc = new ucQueryPatientInfo();

            //{B6EFC117-AEFB-441b-9BB2-3B8A1108CD5A}
            if(patient != null && !string.IsNullOrEmpty(patient.Name))
            {
                uc.QueryStr = patient.Name;
            }

            DialogResult diaResult = FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
            if (diaResult == DialogResult.OK)
            {
                patient = uc.Patient;
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// ��Ŀ��չ��Ϣ�ӿ�
        /// </summary>
        private static FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo iItemCompareInfo = null;

        /// <summary>
        /// ��Ŀ��չ��Ϣ�ӿ�
        /// </summary>
        public static FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo IItemCompareInfo
        {
            get
            {
                if (iItemCompareInfo == null)
                {
                    iItemCompareInfo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Common.Controls.ucInputItem), typeof(FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo)) as FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo;
                }
                return Function.iItemCompareInfo;
            }
        }

        //{DCD2CA71-41A8-438e-AF1C-80AC75B3B4E4}
        public static ArrayList CalculateCurrency(decimal Amount, decimal TotCost, decimal RealCost, decimal GiftCost, decimal EtcCost)
        {
            decimal totCost = TotCost;
            decimal realCost = RealCost;
            decimal giftCost = GiftCost;
            decimal etcCost = EtcCost;

            decimal lasttot = TotCost;
            decimal lastreal = RealCost;
            decimal lastgift = GiftCost;
            decimal lastetc = EtcCost;

            ArrayList costArray = new ArrayList();

            for (int i = 0; i < Amount; i++)
            {
                decimal[] cost = new decimal[4];
                decimal tot = 0.0m;
                decimal real = 0.0m;
                decimal gift = 0.0m;
                decimal etc = 0.0m;

                //�������һ��
                if (i != Amount - 1)
                {
                    tot = TotCost / Amount;
                    //ֱ����ȥС����λ�Ժ����ֵ�������н�λ
                    real = Math.Floor(RealCost * 100 / Amount)/100;
                    gift = Math.Floor(GiftCost * 100 / Amount) / 100;
                    etc = Math.Floor(EtcCost * 100 / Amount) / 100;
                }
                else
                {
                    tot = lasttot;
                    real = lastreal;
                    gift = lastgift;
                    etc = lastetc;
                }

                lasttot -= tot;

                if (lastreal < real)
                {
                    real = lastreal;
                    lastreal = 0;
                }
                else
                {
                    lastreal -= real;
                }

                if (lastgift < gift)
                {
                    gift = lastgift;
                    lastgift = 0;
                }
                else
                {
                    lastgift -= gift;
                }

                if (lastetc < etc)
                {
                    etc = lastetc;
                    lastetc = 0;
                }
                else
                {
                    lastetc -= etc;
                }

                if (real < 0 || gift < 0 || etc < 0)
                {
                    return null;
                }

                //�����ܽ�� = ʵ�ս�� + ���ͽ�� + �Żݽ��
                if (tot > real + gift + etc)
                {
                    decimal diff = tot - real - gift - etc;

                    if (lastreal + lastgift + lastetc >= diff)
                    {
                        if (lastreal >= diff)
                        {
                            real += diff;
                            lastreal -= diff;
                            diff = 0;
                        }
                        else
                        {
                            real += lastreal;
                            diff -= lastreal;

                            if (lastgift >= diff)
                            {
                                gift += diff;
                                lastgift -= diff;
                                diff = 0;
                            }
                            else
                            {
                                gift += lastgift;
                                diff -= lastgift;

                                if (lastetc >= diff)
                                {
                                    etc += diff;
                                    lastetc -= diff;
                                    diff = 0;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
                else if (tot < real + gift + etc)
                {
                    return null;
                }

                cost[0] = tot;
                cost[1] = real;
                cost[2] = gift;
                cost[3] = etc;
                costArray.Add(cost);
            }

            return costArray;
        }


        #region ��ӡ��

        /// <summary>
        /// ��ӡ��ѡ��
        /// </summary>
        /// <returns></returns>
        public static string ChoosePrinter()
        {
            Forms.frmChoosePrinter frmPrint = new FS.HISFC.Components.Common.Forms.frmChoosePrinter();
            frmPrint.Init();
            frmPrint.StartPosition = FormStartPosition.CenterParent;
            frmPrint.ShowDialog();

            //{3E7EFECA-5375-420b-A435-323463A0E56C}
            //����û�ȡ�����򷵻ؿ�ֵ
            if (frmPrint.DialogResult == DialogResult.Cancel)
            {
                return string.Empty;
            }

            return frmPrint.PrinterName;
        }

        #endregion

        /// <summary>
        /// д�벡����
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int WriterMCard(string mCardNO, ref string errInfo)
        {
            if (InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "û��ά�������ӿڣ�";
                return -1;
            }

            int result = InterfaceManager.GetIOperCard().WriterCardNO(2, mCardNO, ref  errInfo);//�����2����
            if (result == -1)
            {
                errInfo = "����ʧ�ܣ���ȷ���Ƿ���ȷ�������ƿ���";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// д�벡���� // {DC68D3DF-1CB9-4d58-93E0-4F85B58E1647}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int WriterCardNo(string cardNO, ref string errInfo)
        {
            if (InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "û��ά�������ӿڣ�";
                return -1;
            }

            int result = InterfaceManager.GetIOperCard().WriterCardNO(3, cardNO, ref  errInfo);//������3����
            if (result == -1)
            {
                errInfo = "����ʧ�ܣ���ȷ���Ƿ���ȷ�������ƿ���";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperCard(ref string cardNO, ref string errInfo)
        {
            if (InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "û��ά�������ӿڣ�";
                return -1;
            }

            int result = InterfaceManager.GetIOperCard().ReadCardNO(ref cardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "����ʧ�ܣ���ȷ���Ƿ���ȷ�������ƿ���";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperMCard(ref string McardNO, ref string errInfo)
        {
            if (InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "û��ά�������ӿڣ�";
                return -1;
            }

            int result = InterfaceManager.GetIOperCard().ReadMCardNO(ref McardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "����ʧ�ܣ���ȷ���Ƿ���ȷ�������ƿ���";
                return -1;
            }

            return 1;
        }
    }
}
