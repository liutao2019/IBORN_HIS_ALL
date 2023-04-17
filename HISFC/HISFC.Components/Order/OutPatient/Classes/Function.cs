using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using FS.HISFC.Models.Base;
using System.Xml;
using System.IO;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Order.OutPatient.Classes
{
    /// <summary>
    /// ���ش�������
    /// </summary>
    public class Function
    {
        public Function()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            //��ѯȫ��ҽ������
            ArrayList alControler = CacheManager.InterMgr.QueryControlerInfoByKind("MET");
            if (alControler == null)
            {
                MessageBox.Show("��ȡҽ�����Ʋ�������ϵͳ������Ĭ��ֵ���в�����");
            }
            else
            {
                Function.controlerHelper.ArrayObject = alControler;
            }

            //�Ƿ��¸��Ĵ����㷨
            isNewSubFeeSet = CacheManager.ContrlManager.GetControlParam<bool>("LHMZ01", false, false);
            if (ITruncFee == null)
            {
                ITruncFee = (FS.HISFC.BizProcess.Interface.Fee.ITruncFee)FS.HISFC.BizProcess.Interface.Fee.InterfaceManager.GetTruncFeeType();
            }
        }

        #region �ӿ�����
        static FS.HISFC.BizProcess.Interface.Fee.ITruncFee ITruncFee = null;
        #endregion


        #region ����

        /// <summary>
        /// �Ƿ��¸��Ĵ����㷨
        /// �±�met_com_subtblitem
        /// �ɱ�fin_opb_inject
        /// </summary>
        private static bool isNewSubFeeSet = false;

        /// <summary>
        /// �÷��͸���
        /// </summary>
        private static Hashtable hsUsageAndSub = new Hashtable();

        /// <summary>
        /// �÷��͸���
        /// </summary>
        public static Hashtable HsUsageAndSub
        {
            get
            {
                if (hsUsageAndSub == null)
                {
                    SethsUsageAndSub();
                }
                return hsUsageAndSub;
            }
        }

        /// <summary>
        /// ȫ�ֿ��Ʋ���������
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper controlerHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �÷��б�
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper usageHelper = null;

        /// <summary>
        /// Ժע�÷�
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper injectUsageHelper = null;

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected static FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �Ƿ�ҽ���۽���ֱ�ӽ��
        /// </summary>
        public static bool isDirectSIFEE = false;

        #endregion

        /// <summary>
        ///  ������õ�
        /// </summary>
        /// <param name="fee"></param>//�շѹ�����
        /// <param name="order"></param>//ҽ��ʵ��
        /// <param name="reciptNo"></param>//������
        /// <param name="seqNo"></param>//������ˮ��
        /// <param name="dtNow"></param>//����ʱ��
        /// <returns></returns>
        public static int SaveToFee(FS.HISFC.Models.Order.OutPatient.Order order, string reciptNo, int seqNo, DateTime dtNow)
        {
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitemlist = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

            feeitemlist.Item.Qty = order.Item.Qty; //�Ǽ�����
            feeitemlist.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;//��������
            feeitemlist.Patient.ID = order.Patient.PID.ID;//������ˮ��
            feeitemlist.Patient.PID.CardNO = order.Patient.PID.CardNO;//���￨�� 

            feeitemlist.ChargeOper.OperTime = dtNow;//��������
            feeitemlist.ChargeOper.ID = FS.FrameWork.Management.Connection.Operator.ID;//������
            feeitemlist.Order.CheckPartRecord = order.CheckPartRecord;//���� 
            feeitemlist.Order.Combo.ID = order.Combo.ID;//��Ϻ�
            if (order.Unit == "[������]")//����Ǹ�����Ŀ���ñ��
            {
                feeitemlist.IsGroup = true;
                feeitemlist.UndrugComb.ID = order.User01;
                feeitemlist.UndrugComb.Name = order.User02;
            }

            //if(order.Item.IsPharmacy)
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                feeitemlist.ExecOper.Dept = order.StockDept.Clone();//���ۿ���ң�by zuowy
            }
            else
            {
                feeitemlist.ExecOper.Dept = order.ExeDept.Clone();//��ִ�п���
            }
            feeitemlist.InjectCount = order.InjectCount;//Ժ�ڴ���

            if (order.Item.PackQty <= 0)//��װ��λΪ0����1
            {
                order.Item.PackQty = 1;
            }
            order.FT.OwnCost = order.Qty * order.Item.Price / order.Item.PackQty;//�Ը����
            feeitemlist.FT = Round(order, 2);//ȡ��λ
            feeitemlist.Days = order.HerbalQty;//��ҩ����
            feeitemlist.Order.ReciptDept = order.ReciptDept;//����������Ϣ
            feeitemlist.Order.ReciptDoctor = order.ReciptDoctor;//����ҽ����Ϣ
            feeitemlist.Order.DoseOnce = order.DoseOnce;//ÿ������
            feeitemlist.Order.DoseUnit = order.DoseUnit;//������λ
            feeitemlist.Order.Frequency = order.Frequency.Clone();//Ƶ����Ϣ
            feeitemlist.IsGroup = false;//�Ƿ�����
            feeitemlist.Order.Combo.IsMainDrug = order.Combo.IsMainDrug;//�Ƿ���ҩ
            feeitemlist.ID = order.Item.ID;
            feeitemlist.Name = order.Item.Name;
            //if(order.Item.IsPharmacy )//�Ƿ�ҩƷ
            if (order.Item.ItemType == EnumItemType.Drug)//�Ƿ�ҩƷ
            {
                //add by sunm ��֪������д���Ƿ���ȷ
                ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).BaseDose = ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose;//��������
                ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).Quality = ((FS.HISFC.Models.Pharmacy.Item)order.Item).Quality;//ҩƷ����
                ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).DosageForm = ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm;//����

                feeitemlist.IsConfirmed = false;//�Ƿ��ն�ȷ��
                feeitemlist.Item.PackQty = order.Item.PackQty;//��װ����
            }
            else
            {

                FS.HISFC.Models.Fee.Item.Undrug myobj = order.Item as FS.HISFC.Models.Fee.Item.Undrug;
                feeitemlist.Item.IsNeedConfirm = myobj.IsNeedConfirm;//����Ĵ��벻̫��,��֪����ô�޸��Ƿ���ȷ
                //if(myobj.ConfirmFlag == FS.HISFC.Models.Fee.ConfirmState.All////????????????
                //    ||myobj.ConfirmFlag==FS.HISFC.Models.Fee.ConfirmState.Outpatient)
                //{
                //    feeitemlist.IsConfirmed = true;
                //}
                //else
                //{
                //    feeitemlist.IsConfirmed = false;
                //}
                feeitemlist.Item.PackQty = 1;//��װ����
            }

            //feeitemlist.Item.IsPharmacy = order.Item.IsPharmacy;//�Ƿ�ҩƷ
            feeitemlist.Item.ItemType = order.Item.ItemType;//�Ƿ�ҩƷ
            //if(order.Item.IsPharmacy)//ҩƷ
            if (order.Item.ItemType == EnumItemType.Drug)//ҩƷ
            {
                feeitemlist.Item.Specs = order.Item.Specs;
            }
            feeitemlist.IsUrgent = order.IsEmergency;//�Ƿ�Ӽ�
            feeitemlist.Order.Sample = order.Sample;//������Ϣ
            feeitemlist.Memo = order.Memo;//��ע
            feeitemlist.Item.MinFee = order.Item.MinFee;//��С����
            feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//�շ�״̬
            feeitemlist.Item.Price = order.Item.Price;//�۸�

            feeitemlist.Item.PriceUnit = order.Item.PriceUnit;//�۸�λ
            feeitemlist.Item.Qty = order.Qty;//����
            ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.SeeDate = order.RegTime;//�Ǽ�����
            ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.Templet.Dept = order.ReciptDept;//�Ǽǿ���
            feeitemlist.Item.SysClass = order.Item.SysClass;//ϵͳ���
            feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//��������
            feeitemlist.Order.Usage = order.Usage;//�÷�

            if (order.ReciptNO == "")
            {
                feeitemlist.RecipeNO = reciptNo;//������
                feeitemlist.SequenceNO = seqNo;//��������ˮ��

                return CacheManager.FeeIntegrate.InsertFeeItemList(feeitemlist);
            }
            else
            {
                feeitemlist.RecipeNO = order.ReciptNO;
                feeitemlist.SequenceNO = order.SequenceNO;
                int i = -1;
                i = CacheManager.FeeIntegrate.UpdateFeeItemList(feeitemlist);//����
                if (i == -1)
                    return -1;
                else if (i == 0)
                    return CacheManager.FeeIntegrate.InsertFeeItemList(feeitemlist);//����
                else
                    return i;
            }
        }

        /// <summary>
        /// ������õ�
        /// </summary>
        /// <param name="fee"></param>
        /// <param name="feeitem"></param>
        /// <param name="dtNow"></param>
        /// <returns></returns>
        public static int SaveToFee(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitem, DateTime dtNow)
        {

            int i = -1;//��ʱ����
            i = CacheManager.FeeIntegrate.UpdateFeeItemList(feeitem);//����
            if (i == -1)
                i = -1;
            else if (i == 0)
                i = CacheManager.FeeIntegrate.InsertFeeItemList(feeitem);//����
            return i;
        }

        /// <summary>
        /// ��������Ŀ��ֳ���ϸ
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static ArrayList ChangeZtToSingle(FS.HISFC.Models.Order.OutPatient.Order order, FS.HISFC.Models.Registration.Register reg, FS.HISFC.Models.Base.PactInfo pactInfo)
        {
            ArrayList alZt = CacheManager.InterMgr.QueryUndrugPackageDetailByCode(order.Item.ID);

            if (alZt == null)
            {
                MessageBox.Show("���Ҹ�����Ŀ" + order.Item.Name + "ʧ��!", "��ʾ");
                return null;
            }

            ArrayList alOrder = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Item.Undrug info in alZt)
            {
                FS.HISFC.Models.Fee.Item.Undrug item = CacheManager.FeeIntegrate.GetItem(info.ID);

                if (item == null)
                {
                    MessageBox.Show("���Ҹ�����Ŀ��ϸ" + info.ID + "ʧ��!", "��ʾ");
                    return null;
                }

                FS.HISFC.Models.Order.OutPatient.Order temp = new FS.HISFC.Models.Order.OutPatient.Order();

                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                temp.Item = item.Clone();
                temp.Item.ID = item.ID;
                temp.Package.ID = order.Item.ID;
                temp.Package.Name = order.Item.Name;
                temp.Combo = order.Combo;
                temp.ReciptDoctor = order.ReciptDoctor;
                temp.DoseOnce = order.DoseOnce;
                temp.DoseUnit = order.DoseUnit;
                temp.ExeDept = order.ExeDept;
                temp.Frequency = order.Frequency.Clone();
                temp.HerbalQty = order.HerbalQty;
                temp.ID = order.ID;
                temp.Usage = order.Usage;
                temp.Unit = item.PriceUnit;
                temp.NurseStation = order.NurseStation;
                //temp.Item.Price = GetPrice(temp, item, reg, pactInfo, true);
                decimal orgPrice = 0;

                temp.Item.Price = CacheManager.FeeIntegrate.GetPrice(temp.Item.ID, reg, 0, item.Price, item.ChildPrice, item.SpecialPrice, 0, ref orgPrice);

                temp.Qty = info.Qty * order.Qty;
                //Add By Maokb
                temp.Item.SysClass = order.Item.SysClass;

                alOrder.Add(temp);
            }

            return alOrder;
        }

        static Hashtable hsFeeCodeStat = null;

        public static string GetFeeInfo(ArrayList alFeeInfo)
        {
            if (hsFeeCodeStat == null)
            {
                hsFeeCodeStat = new Hashtable();
                DataSet ds = null;
                if (CacheManager.FeeIntegrate.GetInvoiceClass("MZ01", ref ds) == -1)
                {
                    MessageBox.Show("��ȡ��Ʊ��Ϣ����\r\n" + CacheManager.FeeIntegrate.Err);
                    return "";
                }

                foreach (DataRow drow in ds.Tables[0].Rows)
                {
                    if (!hsFeeCodeStat.Contains(drow["fee_code"].ToString()))
                    {
                        hsFeeCodeStat.Add(drow["fee_code"], drow["fee_stat_name"].ToString());
                    }
                }
            }

            Hashtable hs = new Hashtable();

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alFeeInfo)
            {
                feeItem.FT.TotCost = feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                if (hsFeeCodeStat.Contains(feeItem.Item.MinFee.ID))
                {
                    if (!hs.Contains(hsFeeCodeStat[feeItem.Item.MinFee.ID]))
                    {
                        hs[hsFeeCodeStat[feeItem.Item.MinFee.ID]] = feeItem.FT.TotCost;
                    }
                    else
                    {
                        hs[hsFeeCodeStat[feeItem.Item.MinFee.ID]] = FS.FrameWork.Function.NConvert.ToDecimal(hs[hsFeeCodeStat[feeItem.Item.MinFee.ID]]) + feeItem.FT.TotCost;
                    }
                }
            }

            string str = "";
            foreach (string keys in hs.Keys)
            {
                str += keys.ToString() + ":" + FS.FrameWork.Function.NConvert.ToDecimal(hs[keys]).ToString("F4").TrimEnd('0').TrimEnd('.') + "Ԫ ";
            }
            return str;
        }

        /// <summary>
        /// ��ҽ��ʵ��ת�ɷ���ʵ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Fee.Outpatient.FeeItemList ChangeToFeeItemList(FS.HISFC.Models.Order.OutPatient.Order order, FS.HISFC.Models.Registration.Register regObj)
        {
            try
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitemlist = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

                //if (order.Item.IsPharmacy)
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    feeitemlist.Item = new FS.HISFC.Models.Pharmacy.Item();

                }
                else
                {
                    feeitemlist.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                }

                feeitemlist.Item.ID = order.Item.ID;
                feeitemlist.Item.Name = order.Item.Name;
                feeitemlist.Item.Qty = order.Qty;
                feeitemlist.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                feeitemlist.Patient.ID = order.Patient.ID;//������ˮ��
                feeitemlist.Patient.PID.CardNO = order.Patient.PID.CardNO;//���￨�� 

                try
                {
                    //houwb ҽ�����Էѷ����� {A1FE7734-267C-43f1-A824-BF73B482E0B2}
                    feeitemlist.Order.Patient.Pact.ID = order.Patient.Pact.ID;
                    feeitemlist.Order.Patient.Pact.PayKind.ID = order.Patient.Pact.PayKind.ID;
                    //end houwb
                }
                catch { }

                feeitemlist.Order.ID = order.ID;//ҽ����ˮ��
                feeitemlist.Order.SortID = order.SortID;
                feeitemlist.Order.SubCombNO = order.SubCombNO;

                feeitemlist.ChargeOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                feeitemlist.Order.CheckPartRecord = order.CheckPartRecord;//���� 
                feeitemlist.Order.Combo.ID = order.Combo.ID;//��Ϻ�

                //FS.HISFC.Models.Fee.Item.Undrug unDrugItem = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
                //if (unDrugItem.UnitFlag == "1")
                //{
                //}

                if (order.Unit == "[������]")
                {
                    feeitemlist.IsGroup = true;
                    feeitemlist.UndrugComb.ID = order.User01;
                    feeitemlist.UndrugComb.Name = order.User02;
                }
                else
                {
                    if (IsLisSelectDetail(feeitemlist.Item.SysClass.ID.ToString()))
                    {
                        feeitemlist.UndrugComb.ID = order.ApplyNo;
                    }
                }

                //if (order.Item.IsPharmacy && !((FS.HISFC.Models.Pharmacy.Item)order.Item).IsSubtbl )
                //{209C4FB3-9309-4703-AA82-F05D7089821E}
                //if (order.Item.ItemType == EnumItemType.Drug && !((FS.HISFC.Models.Pharmacy.Item)order.Item).IsSubtbl)
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    feeitemlist.ExecOper.Dept.ID = order.StockDept.Clone().ID;//���ۿ����
                    feeitemlist.ExecOper.Dept.Name = order.StockDept.Clone().Name;
                }
                else
                {
                    feeitemlist.ExecOper.Dept.ID = order.ExeDept.Clone().ID;
                    feeitemlist.ExecOper.Dept.Name = order.ExeDept.Clone().Name;
                }
                feeitemlist.InjectCount = order.InjectCount;//Ժ�ڴ���
                
                decimal orgPrice = 0;

                if (!string.Equals(order.Item.ID, "999")) //�Ա���Ʒ����۸�
                {
                    
                    //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                    // isDirectSIFEE ����Ӱ��ĺ�ͬ��λ
                    bool isDirectSIFEE = controlParamIntegrate.GetControlParam<bool>("GZSI01", false, false);
                    string SIFEEPACT = controlParamIntegrate.GetControlParam<string>("GZSI02", false, "");

                    if (feeitemlist.Item.ItemType == EnumItemType.UnDrug)
                    {
                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeitemlist.Item.ID);
                        decimal price = CacheManager.FeeIntegrate.GetPrice(order.Item.ID, regObj, 0, undrug.Price, undrug.ChildPrice, undrug.SpecialPrice, 0, ref orgPrice);
                        //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                        if (SIFEEPACT.Contains(regObj.Pact.PayKind.ID) && !isDirectSIFEE)
                        {
                            price = undrug.Price;
                        }
                        order.Item.Price = price;
                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.Item drug = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                        decimal price = CacheManager.FeeIntegrate.GetPrice(order.Item.ID, regObj, 0, drug.Price, drug.Price, drug.Price, 0, ref orgPrice);
                        //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                        if (SIFEEPACT.Contains(regObj.Pact.PayKind.ID) && !isDirectSIFEE)
                        {
                            price = drug.Price;
                        }
                        order.Item.Price = price;
                    }
                }
                if (order.Item.PackQty <= 0)
                {
                    order.Item.PackQty = 1;
                }
                //��������Ŀ
                ////if (order.Item.Price == 0)
                ////{
                ////    order.Item.Price = order.Item.Price;
                ////}
                //by zuowy �����շ��Ƿ�����С��λ ȷ���շ� ��ʱ����
                //if (order.Item.IsPharmacy)
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    feeitemlist.Item.SpecialFlag = order.Item.SpecialFlag;

                    if (order.MinunitFlag == "")//user03Ϊ��,˵����֪��������ʲô��λ Ĭ��Ϊ��С��λ
                    {
                        order.MinunitFlag = "1";//Ĭ��
                    }
                    if (order.MinunitFlag != "1")//������С��λ !=((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit)
                    {
                        feeitemlist.Item.Qty = order.Item.PackQty * order.Qty;
                        order.FT.OwnCost = order.Qty * order.Item.Price;

                        order.Item.PriceUnit = order.Unit;
                        feeitemlist.FeePack = "1";//������λ 1:��װ��λ 0:��С��λ
                    }
                    else
                    {
                        if (order.Item.PackQty == 0)
                        {
                            order.Item.PackQty = 1;
                        }
                        order.FT.OwnCost = order.Qty * order.Item.Price / order.Item.PackQty;

                        order.Item.PriceUnit = order.Unit;
                        feeitemlist.FeePack = "0";//������λ 1:��װ��λ 0:��С��λ
                    }
                }
                else
                {
                    order.FT.OwnCost = order.Qty * order.Item.Price;
                    feeitemlist.FeePack = "1";
                }

                if (order.HerbalQty == 0)
                {
                    order.HerbalQty = 1;
                }

                feeitemlist.Days = order.HerbalQty;//��ҩ����
                feeitemlist.RecipeOper.Dept = order.ReciptDept;//����������Ϣ
                feeitemlist.RecipeOper.Name = order.ReciptDoctor.Name;//����ҽ����Ϣ
                feeitemlist.RecipeOper.ID = order.ReciptDoctor.ID;
                feeitemlist.Order.DoseUnit = order.DoseUnit;//������λ
                //if (order.Item.IsPharmacy)
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    if (((FS.HISFC.Models.Pharmacy.Item)order.Item).SysClass.ID.ToString() == "PCC")
                    {
                        if (order.HerbalQty == 0)
                        {
                            order.HerbalQty = 1;
                        }

                        feeitemlist.Order.DoseOnce = order.DoseOnce;

                    }
                    else
                    {
                        feeitemlist.Order.DoseOnce = order.DoseOnce;//ÿ������
                    }
                }
                feeitemlist.Order.Frequency = order.Frequency.Clone();//Ƶ����Ϣ

                feeitemlist.Order.Combo.IsMainDrug = order.Combo.IsMainDrug;//�Ƿ���ҩ

                //if (order.Item.IsPharmacy)//�Ƿ�ҩƷ
                if (order.Item.ItemType == EnumItemType.Drug)//�Ƿ�ҩƷ
                {
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).BaseDose = ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose;//��������
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).Quality = ((FS.HISFC.Models.Pharmacy.Item)order.Item).Quality;//ҩƷ����
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).DosageForm = ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm;//����

                    feeitemlist.IsConfirmed = false;//�Ƿ��ն�ȷ��
                    feeitemlist.Item.PackQty = order.Item.PackQty;//��װ����
                }
                else
                {
                    if (order.ReTidyInfo != "SUBTBL")
                    {
                        feeitemlist.IsConfirmed = false;
                        feeitemlist.Item.PackQty = 1;//��װ����
                    }
                    else//�����еĸ�����Ŀ
                    {
                        feeitemlist.IsConfirmed = false;//FS.neHISFC.Components.Function.NConvert.ToBoolean(order.Mark2);
                        feeitemlist.Item.PackQty = 1;
                    }
                }

                //feeitemlist.Order.Item.IsPharmacy = order.Item.IsPharmacy;//�Ƿ�ҩƷ
                feeitemlist.Order.Item.ItemType = order.Item.ItemType;//�Ƿ�ҩƷ
                //if (order.Item.IsPharmacy)//ҩƷ
                if (order.Item.ItemType == EnumItemType.Drug)//ҩƷ
                {
                    feeitemlist.Item.Specs = order.Item.Specs;
                }
                feeitemlist.IsUrgent = order.IsEmergency;//�Ƿ�Ӽ�
                feeitemlist.Order.Sample = order.Sample;//������Ϣ
                feeitemlist.Memo = order.Memo;//��ע
                feeitemlist.Item.MinFee = order.Item.MinFee;//��С����
                feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//����״̬
                feeitemlist.Item.Price = order.Item.Price;//�۸�
                feeitemlist.OrgPrice = order.Item.Price;
                feeitemlist.Item.PriceUnit = order.Item.PriceUnit;//�۸�λ
                if (order.Item.SysClass.ID.ToString() == "PCC" && order.HerbalQty > 0)
                {

                }
                order.FT.TotCost = order.FT.TotCost;
                order.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
                order.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
               
                feeitemlist.FT = Round(order, 2);//ȡ��λ
                

                //{B9303CFE-755D-4585-B5EE-8C1901F79450} ��ҩƷ�������ԭ�����ܷ���
                if (feeitemlist.Item.ItemType == EnumItemType.Drug)
                {
                    if (order.MinunitFlag != "1")//������С��λ
                    {
                        feeitemlist.FT.ExcessCost = order.Qty * ((FS.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice;
                    }
                    else
                    {
                        feeitemlist.FT.ExcessCost = order.Qty * ((FS.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice / order.Item.PackQty;
                        feeitemlist.FT.ExcessCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.ExcessCost, 2);
                    }
                }
                ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.SeeDate = order.RegTime;//�Ǽ�����
                ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.Templet.Dept = order.ReciptDept;//�Ǽǿ���
                feeitemlist.Item.SysClass = order.Item.SysClass;//ϵͳ���
                feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//��������
                feeitemlist.Order.Usage = order.Usage;//�÷�
                feeitemlist.RecipeSequence = order.ReciptSequence;//�շ�����
                feeitemlist.RecipeNO = order.ReciptNO;//������
                feeitemlist.SequenceNO = order.SequenceNO;//������ˮ��

                feeitemlist.IsExceeded = order.IsExceeded;//�Ƿ񳬹�

                feeitemlist.ChargeOper.OperTime = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
                if (feeitemlist.Item.IsMaterial)
                {
                    //���ĵķ�����ԴΪ�շ�
                    feeitemlist.FTSource = "0";
                }
                else
                {
                    feeitemlist.FTSource = "1";//����ҽ��
                }

                if (order.IsSubtbl)
                {
                    feeitemlist.Item.IsMaterial = true;//�Ǹ���
                }

                //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                feeitemlist.Item.IsNeedConfirm = order.Item.IsNeedConfirm;
                if (feeitemlist.Item.ItemType == EnumItemType.UnDrug)
                {
                    ((FS.HISFC.Models.Fee.Item.Undrug)feeitemlist.Item).NeedConfirm =
                    ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).NeedConfirm;
                }
                return feeitemlist;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ת���ɴ�00���ַ���
        /// </summary>
        /// <param name="val"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ToDecimal(decimal val, int i)
        {
            try
            {
                decimal m = 0m;
                if (val.ToString().LastIndexOf(".") > 0)
                {
                    m = System.Math.Round(val, i);
                    return m.ToString();
                }
                else
                {
                    System.Text.StringBuilder buffer = null;
                    buffer = new System.Text.StringBuilder();
                    buffer.Append(val.ToString());
                    buffer.Append(".");
                    for (int j = 0; j < i; j++)
                    {
                        buffer.Append("0");
                    }
                    return buffer.ToString();
                }
            }
            catch
            {
                return val.ToString();
            }
        }

        /// <summary>
        /// ����շ���Ŀ
        /// </summary>
        /// <param name="item"></param>
        public static void CheckFeeItemList(FS.HISFC.Models.Fee.Outpatient.FeeItemList item)
        {
            if (item.UndrugComb.Package.Name == "[������]")
            {
                item.IsGroup = true;
            }
            item.FT.OwnCost = item.Item.Qty * item.Item.Price;
        }

        /// <summary>
        /// Ϊ����ȡ��
        /// </summary>
        /// <param name="order"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static FS.HISFC.Models.Base.FT Round(FS.HISFC.Models.Order.OutPatient.Order order, int i)
        {
            FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();
            //ΪNULL������ʵ��
            if (order == null || order.FT == null)
            {
                return ft;
            }

            if (ITruncFee == null)
            {
                ITruncFee = (FS.HISFC.BizProcess.Interface.Fee.ITruncFee)FS.HISFC.BizProcess.Interface.Fee.InterfaceManager.GetTruncFeeType();
            }

            if (ITruncFee != null)
            {
                ft = (FS.HISFC.Models.Base.FT)ITruncFee.TruncFee(order);

            }
            else
            {
                ft.AdjustOvertopCost = FS.FrameWork.Public.String.FormatNumber(order.FT.AdjustOvertopCost, i);
                ft.AirLimitCost = FS.FrameWork.Public.String.FormatNumber(order.FT.AirLimitCost, i);
                ft.BalancedCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BalancedCost, i);
                ft.BalancedPrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BalancedPrepayCost, i);
                ft.BedLimitCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BedLimitCost, i);
                ft.BedOverDeal = order.FT.BedOverDeal;
                ft.BloodLateFeeCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BloodLateFeeCost, i);
                ft.BoardCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BoardCost, i);
                ft.BoardPrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.BoardPrepayCost, i);
                ft.DrugFeeTotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DrugFeeTotCost, i);
                ft.TransferPrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TransferPrepayCost, i);
                ft.TransferTotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TransferTotCost, i);
                ft.DayLimitCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DayLimitCost, i);
                ft.DerateCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DerateCost, i);
                ft.FixFeeInterval = order.FT.FixFeeInterval;
                ft.ID = order.FT.ID;
                ft.LeftCost = FS.FrameWork.Public.String.FormatNumber(order.FT.LeftCost, i);
                ft.OvertopCost = FS.FrameWork.Public.String.FormatNumber(order.FT.OvertopCost, i);
                ft.DayLimitTotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.DayLimitTotCost, i);
                ft.Memo = order.FT.Memo;
                ft.Name = order.FT.Name;
                ft.OwnCost = FS.FrameWork.Public.String.FormatNumber(order.FT.OwnCost, i);
                ft.FTRate.OwnRate = order.FT.FTRate.OwnRate;
                ft.PayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.PayCost, i);
                ft.FTRate.PayRate = order.FT.FTRate.PayRate;
                ft.PreFixFeeDateTime = order.FT.PreFixFeeDateTime;
                ft.PrepayCost = FS.FrameWork.Public.String.FormatNumber(order.FT.PrepayCost, i);
                ft.PubCost = FS.FrameWork.Public.String.FormatNumber(order.FT.PubCost, i);
                ft.RebateCost = FS.FrameWork.Public.String.FormatNumber(order.FT.RebateCost, i);
                ft.ReturnCost = FS.FrameWork.Public.String.FormatNumber(order.FT.ReturnCost, i);
                ft.SupplyCost = FS.FrameWork.Public.String.FormatNumber(order.FT.SupplyCost, i);
                ft.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, i);

                ft.User01 = order.FT.User01;
                ft.User02 = order.FT.User02;
                ft.User03 = order.FT.User03;
            }
            return ft;
        }

        /// <summary>
        /// �Ƿ���ͬ�÷�
        /// </summary>
        /// <param name="usageID1"></param>
        /// <param name="usageID2"></param>
        /// <returns></returns>
        public static bool IsSameUsage(string usageID1, string usageID2)
        {
            return Components.Order.Classes.Function.IsSameUsage(usageID1, usageID2);
        }

        /// <summary>
        /// �Ƿ����ϵͳ��� ������ҩ����ҩ��ϡ�����ͬһ����
        /// </summary>
        static bool isDecSysClassWhenGetRecipeNO = false;

        /// <summary>
        /// �ж����Լ������
        /// </summary>
        /// <param name="orderFrom"></param>
        /// <param name="orderTo"></param>
        /// <param name="isNew">�ǲ�����ҽ���� ��ҽ��������Ϣ���Բ��ж�</param>
        /// <param name="isIgnore">���ԵĻ��������÷���Ƶ�Ρ�Ժע���ж��Ƿ���ͬ</param>
        /// <returns></returns>
        public static int ValidComboOrder(FS.HISFC.Models.Order.OutPatient.Order orderFrom, FS.HISFC.Models.Order.OutPatient.Order orderTo, bool isNew, bool isIgnore)
        {
            if (orderFrom.IsSubtbl || orderTo.IsSubtbl)
            {
                return 1;
            }

            isDecSysClassWhenGetRecipeNO = CacheManager.ContrlManager.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, false, false);

            //���ݷַ����ã�������ҩ�ͳ�ҩ���ʹ��
            if (isDecSysClassWhenGetRecipeNO)
            {
                if ("PCZ,P".Contains(orderFrom.Item.SysClass.ID.ToString()) &&
                    "PCZ,P".Contains(orderTo.Item.SysClass.ID.ToString()))
                {
                    //��ҩ�ͳ�ҩ�������
                }
                else
                {
                    if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                    {
                        MessageBox.Show("ϵͳ���ͬ������������ã�");
                        return -1;
                    }
                }
            }
            else
            {
                if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                {
                    MessageBox.Show("ϵͳ���ͬ������������ã�");
                    return -1;
                }
            }

            if (!isIgnore)
            {
                if (isNew && !string.IsNullOrEmpty(orderFrom.Frequency.ID) && orderFrom.Frequency.ID != "PRN")
                {
                    if (orderFrom.Frequency.ID != orderTo.Frequency.ID)
                    {
                        MessageBox.Show("Ƶ�β�ͬ������������ã�");
                        return -1;
                    }
                }

                if (isNew && orderFrom.InjectCount > 0)
                {
                    if (orderFrom.InjectCount != orderTo.InjectCount)
                    {
                        MessageBox.Show("Ժע������ͬ������������ã�");
                        return -1;
                    }
                }
            }

            if (orderFrom.ExeDept.ID != orderTo.ExeDept.ID)
            {
                MessageBox.Show("ִ�п��Ҳ�ͬ���������ʹ��!", "��ʾ");
                return -1;
            }

            if (orderFrom.Item.ItemType == EnumItemType.Drug)		//ֻ��ҩƷ�ж��÷��Ƿ���ͬ
            {
                if (isNew && !string.IsNullOrEmpty(orderFrom.Usage.ID))
                {
                    if (!isIgnore)
                    {
                        if (orderFrom.Item.SysClass.ID.ToString() != "PCC")
                        {
                            #region �÷��ж�
                            if (!IsSameUsage(orderFrom.Usage.ID, orderTo.Usage.ID))
                            {
                                MessageBox.Show("�÷���ͬ�������Խ�����ϣ�");
                                return -1;
                            }
                            #endregion
                        }
                    }
                }

                if (orderFrom.Item.SysClass.ID.ToString() == "PCC" || orderFrom.Item.SysClass.ID.ToString() == "C")
                {

                    if (isNew && orderFrom.HerbalQty > 0)
                    {
                        if (orderFrom.HerbalQty != orderTo.HerbalQty)
                        {
                            MessageBox.Show("��ҩ������ͬ������������ã�");
                            return -1;
                        }
                    }
                }
            }
            else
            {
                if (orderFrom.Item.SysClass.ID.ToString() == "UL")//����
                {
                    if (isNew && orderFrom.Qty > 0)
                    {
                        if (orderFrom.Qty != orderTo.Qty)
                        {
                            MessageBox.Show("����������ͬ������������ã�");
                            return -1;
                        }
                    }

                    if (isNew && string.IsNullOrEmpty(orderFrom.Sample.Name))
                    {
                        if (orderFrom.Sample.Name != orderTo.Sample.Name)
                        {
                            MessageBox.Show("����������ͬ������������ã�");
                            return -1;
                        }
                    }
                }
            }
            return 1;
        }

        ///// <summary>
        ///// ����Ƿ���Կ����Ϊ���ҩƷ
        ///// </summary>
        ///// <returns></returns>
        //public static int GetIsOrderCanNoStock()
        //{
        //    return CacheManager.ContrlManager.GetControlParam<int>("200001", false, 0);
        //}

        /// <summary>
        /// �����
        /// </summary>
        /// <param name="iCheck"></param>
        /// <param name="itemID"></param>
        /// <param name="itemName"></param>
        /// <param name="deptCode"></param>
        /// <param name="qty"></param>
        /// <param name="sendType">�������� A:ȫ����O:���I:סԺ</param>
        /// <returns></returns>
        public static bool CheckPharmercyItemStock(int iCheck, string itemID, string itemName, string deptCode, decimal qty, string sendType)
        {
            FS.HISFC.Models.Pharmacy.Storage item = null;
            switch (iCheck)
            {
                case 0:
                    //houwb 2011-5-30 ���ӷ��������ж�
                    item = CacheManager.PhaIntegrate.GetItemStorage(deptCode, sendType, itemID);

                    if (item == null) return true;
                    if (qty > FS.FrameWork.Function.NConvert.ToDecimal(item.StoreQty))
                    {
                        return false;
                    }
                    break;
                case 1:
                    //item = manager.GetItemForInpatient(deptCode, itemID);                    
                    item = CacheManager.PhaIntegrate.GetItemStorage(deptCode, "O", itemID);
                    if (item == null)
                        return true;
                    if (qty > FS.FrameWork.Function.NConvert.ToDecimal(item.StoreQty))
                    {
                        if (MessageBox.Show("ҩƷ��" + itemName + "���Ŀ�治�����Ƿ����ִ�У�", "��ʾ��治��", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            return true;
                        else
                            return false;
                    }
                    break;
                case 2:
                    break;
                default:
                    return true;
            }
            return true;
        }

        /// <summary>
        /// �����Ƿ�ʹ��Ԥ�ۿ�� P00320
        /// </summary>
        static int isUseOutDrugPreOut = -1;

        #region �������� 

        /// <summary>
        /// ��÷�ҩƷ��Ϣ
        /// </summary>
        /// <param name="itemManager"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int FillFeeItem(ref FS.HISFC.Models.Order.OutPatient.Order order)
        {
            if (order.Item.ID == "999")
            {
                return 0;
            }
            if (order.Unit == "[������]")
                return 0;//����Ǹ�����Ŀ����
            FS.HISFC.Models.Fee.Item.Undrug item = CacheManager.FeeIntegrate.GetItem(order.Item.ID);
            if (item == null)
            {
                return -1;
            }

            ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm = item.IsNeedConfirm;
            ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).UnitFlag = item.UnitFlag;

            //sunm modified(do not know istrue)
            order.IsNeedConfirm = item.IsNeedConfirm;

            if (order.ExeDept == null || string.IsNullOrEmpty(order.ExeDept.ID))
            {
                if (!string.IsNullOrEmpty(item.ExecDept))
                {
                    order.ExeDept = new FS.FrameWork.Models.NeuObject(item.ExecDept, "", "");
                }
            }

            order.Item.Price = item.Price;
            order.Item.MinFee = item.MinFee;
            order.Item.SysClass = item.SysClass.Clone();//����ϵͳ���
            return 0;
        }
        #endregion

        #region �����·���������ȱҩ���ж�

        /// <summary>
        /// ���ȱҩ��ͣ��
        /// </summary>
        /// <param name="drugDept"></param>
        /// <param name="order"></param>
        /// <param name="IsOutPatient"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int CheckDrugState(FS.FrameWork.Models.NeuObject patientInfo,FS.FrameWork.Models.NeuObject stockDept, FS.FrameWork.Models.NeuObject reciptDept, FS.HISFC.Models.Base.Item item, bool IsOutPatient, ref string errInfo)
        {
            if (item.ID == "999")
            {
                return 1;
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = null;
                FS.HISFC.Models.Pharmacy.Storage storage = null;
                return SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckDrugState(patientInfo,stockDept, reciptDept, item, true, ref phaItem, ref storage, ref errInfo);
            }
        }

        /// <summary>
        /// ���ȱҩ��ͣ��
        /// </summary>
        /// <param name="drugDept"></param>
        /// <param name="order"></param>
        /// <param name="IsOutPatient"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int CheckDrugState(FS.FrameWork.Models.NeuObject patientInfo,FS.FrameWork.Models.NeuObject stockDept, FS.FrameWork.Models.NeuObject reciptDept, FS.HISFC.Models.Base.Item item, bool IsOutPatient, ref FS.HISFC.Models.Pharmacy.Item phaItem, ref string errInfo)
        {
            if (item.ID == "999")
            {
                return 1;
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Storage storage = null;
                return SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckDrugState(patientInfo,stockDept, reciptDept, item, true, ref phaItem, ref storage, ref errInfo);
            }
        }

        /// <summary>
        /// ���ҽ������ҩƷ�����Ϣ ���ж���Ч��
        /// </summary>
        /// <param name="stockDept">ҩƷ�ۿ����</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int FillDrugItem(FS.FrameWork.Models.NeuObject stockDept, FS.HISFC.Models.Registration.Register regObj, ref FS.HISFC.Models.Order.OutPatient.Order order, ref string errInfo)
        {
            if (order.Item.ID == "999")
            {
                return 1;
            }
            if (order.Item.ItemType != EnumItemType.Drug)
            {
                return 1;
            }

            FS.HISFC.Models.Pharmacy.Item item = null;
            FS.HISFC.Models.Pharmacy.Storage storage = null;

            if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckDrugState(null,stockDept, order.ReciptDept, order.Item, true, ref item, ref storage, ref errInfo) <= 0)
            {
                //MessageBox.Show(errInfo, "����", MessageBoxButtons.OK);
                return -1;
            }

            order.StockDept = storage.StockDept;

            order.Item.MinFee = item.MinFee;
            //{B9303CFE-755D-4585-B5EE-8C1901F79450} ����ԭ���Ĺ����
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice = item.Price;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).SpecialPrice = item.SpecialPrice;
            //decimal orgPrice = 0;
            //order.Item.Price = CacheManager.FeeIntegrate.GetPrice(order.Item.ID, regObj, 0, item.Price, item.ChildPrice, item.SpecialPrice, 0, ref orgPrice);

            order.Item.Price = item.Price;

            order.Item.Name = item.Name;
            order.Item.UserCode = item.UserCode;
            order.Item.SysClass = item.SysClass.Clone();//����ϵͳ���
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = item.IsAllergy;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackUnit = item.PackUnit;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = item.MinUnit;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose = item.BaseDose;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm = item.DosageForm;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).SplitType = item.SplitType;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).Quality = item.Quality;

            if (stockDept == null)
            {
                order.StockDept.ID = storage.StockDept.ID;
                order.StockDept.Name = storage.StockDept.Name;
            }

            return 1;
        }

        /// <summary>
        /// ���ҽ�����·�ҩƷ�����Ϣ ���ж���Ч��
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int FillUndrugItem(FS.HISFC.Models.Registration.Register regObj, ref FS.HISFC.Models.Order.OutPatient.Order order, ref string errInfo)
        {
            if (order.Item.ID == "999")
            {
                return 1;
            }
            if (order.Item.ItemType == EnumItemType.Drug)
            {
                return 1;
            }

            FS.HISFC.Models.Fee.Item.Undrug item = null;

            if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckUnDrugState(order, ref item, ref errInfo) == -1)
            {
                MessageBox.Show(errInfo, "����", MessageBoxButtons.OK);
                return -1;
            }

            if (order.ExeDept == null || string.IsNullOrEmpty(order.ExeDept.ID))
            {
                if (!string.IsNullOrEmpty(item.ExecDept))
                {
                    order.ExeDept = new FS.FrameWork.Models.NeuObject(item.ExecDept, "", "");
                }
            }

            //decimal orgPrice = 0;
            //order.Item.Price = CacheManager.FeeIntegrate.GetPrice(order.Item.ID, regObj, 0, item.Price, item.ChildPrice, item.SpecialPrice, 0, ref orgPrice);

            order.Item.MinFee = item.MinFee;
            order.Item.SysClass = item.SysClass.Clone();//����ϵͳ���
            order.Item.PriceUnit = item.PriceUnit;
            order.Unit = item.PriceUnit;
            order.Item.Specs = item.Specs;
            order.Item.Price = item.Price;
            order.Item.ChildPrice = item.ChildPrice;
            order.Item.SpecialPrice = item.SpecialPrice;

            ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm = item.IsNeedConfirm;
            ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).UnitFlag = item.UnitFlag;

            return 1;
        }

        #endregion

        /// <summary>
        /// ����Ժע����
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int CalculateInjNum(FS.HISFC.Models.Order.OutPatient.Order order, ref string errInfo)
        {
            try
            {
                if (order == null)
                {
                    errInfo = "ҽ����ϢΪ�� nullֵ��";
                    return -1;
                }
                decimal Frequence = 0;

                if (order.Frequency.Days[0] == "0" || string.IsNullOrEmpty(order.Frequency.Days[0]))
                {
                    order.Frequency.Days[0] = "1";
                    Frequence = order.Frequency.Times.Length;
                }
                else
                {
                    try
                    {
                        Frequence = Math.Round(order.Frequency.Times.Length / FS.FrameWork.Function.NConvert.ToDecimal(order.Frequency.Days[0]), 2);
                    }
                    catch
                    {
                        Frequence = order.Frequency.Times.Length;
                    }
                }

                //Ժע����
                if (order.Usage != null
                    //&& Classes.Function.HsUsageAndSub.Contains(order.Usage.ID)
                    && CheckIsInjectUsage(order.Usage.ID)
                    )
                {
                    order.InjectCount = (int)Math.Ceiling((double)(Frequence * order.HerbalQty));
                }
                else
                {
                    order.InjectCount = 0;
                }

                return 1;
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return -1;
            }
        }
        

        /// <summary>
        /// ��ȡ�������Ʒ�Χ
        /// </summary>
        /// <param name="outOrder"></param>
        /// <param name="type">0 ���ޣ����� ����</param>
        /// <returns></returns>
        private static decimal GetLimitQty(FS.HISFC.Models.Order.OutPatient.Order outOrder, FS.HISFC.Models.Pharmacy.Item phaItem, string type)
        {
            FS.HISFC.Models.Order.OutPatient.Order ord = outOrder.Clone();

            Components.Order.Classes.Function.ReComputeQty(ord);

            if (ord.Qty == 0)
            {
                return 0;
            }

            decimal floorQty = 0;
            decimal ceilingQty = 0;

            if (phaItem.PackUnit == ord.Unit)
            {
                floorQty = Math.Floor(ord.Qty);
                ceilingQty = Math.Ceiling(ord.Qty);
            }
            else
            {
                floorQty = Math.Floor(ord.Qty / phaItem.PackQty);
                ceilingQty = Math.Ceiling(ord.Qty / phaItem.PackQty);
            }

            if (type == "0")
            {
                return floorQty;
            }
            else
            {
                return ceilingQty;
            }
        }

        /// <summary>
        /// �ж�����ҩ����
        /// </summary>
        /// <param name="outOrder"></param>
        /// <param name="qtyValue"></param>
        /// <param name="unitValue"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int CheckLimitQty(FS.HISFC.Models.Order.OutPatient.Order outOrder, decimal qtyValue, string unitValue, ref string errInfo)
        {
            if (outOrder.Item.ItemType != EnumItemType.Drug
                || outOrder.Item.ID == "999")
            {
                return 1;
            }

            #region ����ҩ�����ж�
            if (outOrder != null)
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID);
                //����ҩ��ʶ
                if (phaItem.SpecialFlag1 == "1")
                {
                    decimal qty = qtyValue;
                    if (unitValue != phaItem.PackUnit)
                    {
                        qty = qty / phaItem.PackQty;
                    }

                    if (GetLimitQty(outOrder, phaItem, "0") != 0
                        && qty < GetLimitQty(outOrder, phaItem, "0"))
                    {
                        errInfo = "����������ҩƷ[" + outOrder.Item.Name + "]����������С��" + GetLimitQty(outOrder, phaItem, "0").ToString() + phaItem.PackUnit + "(" + (GetLimitQty(outOrder, phaItem, "0") * phaItem.PackQty).ToString() + phaItem.MinUnit + ")";
                    }
                    else if (GetLimitQty(outOrder, phaItem, "0") != 0
                        && qty > GetLimitQty(outOrder, phaItem, "1"))
                    {
                        errInfo = "����������ҩƷ[" + outOrder.Item.Name + "]���������ܴ���" + GetLimitQty(outOrder, phaItem, "1").ToString() + phaItem.PackUnit + "(" + (GetLimitQty(outOrder, phaItem, "1") * phaItem.PackQty).ToString() + phaItem.MinUnit + ")";
                    }
                    if (!string.IsNullOrEmpty(errInfo))
                    {
                        //MessageBox.Show(msg, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //SetQtyValue(outOrder.Qty.ToString());
                        //ͬʱ�޸������͵�λ��ʱ��û���޸ģ����Էŵ���󱣴��ʱ�����жϰ�
                        return -1;
                    }
                }
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// ����ҩƷ������Լ������������ܵ�λ
        /// </summary>
        /// <param name="itm">ҩƷ��Ŀ</param>
        /// <param name="DoseOnce">ÿ�μ���</param>
        /// <param name="BaseDose">��������</param>
        /// <param name="Frequence">Ƶ��</param>
        /// <param name="Days">����</param>
        /// <param name="Qty">����</param>
        /// <param name="unit">���صĵ�λ</param>
        /// <param name="unitFlag">�Ƿ���С��λ��1 ��С��λ��0 ��װ��λ</param>
        [Obsolete("����,��ֲ��FS.HISFC.Components.Order.Classes.Function����ReComputeQty����", true)]
        public static int ReComputeQtyBase(FS.HISFC.Models.Pharmacy.Item itemObj, decimal doseOnce, decimal baseDose, decimal frequence, decimal days, ref decimal qty, ref string unit, ref string unitFlag, ref string errInfo)
        {
            try
            {
                //��ҩ��Ƶ���޹�
                if (itemObj.SysClass.ID.ToString() == "PCC")
                {
                    frequence = 1;
                }

                //0 ��С��λ����ȡ��" ���ݿ�ֵ 0
                //��װ��λ����ȡ��" ���ݿ�ֵ 1  �ڷ��ر����г�ҩ��������ҩ�϶�
                //��С��λÿ��ȡ��" ���ݿ�ֵ 2  ����϶�����
                //��װ��λÿ��ȡ��" ���ݿ�ֵ 3  ����û����   
                switch (itemObj.SplitType)
                {
                    case "0":
                        //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                        //��ҩ��������ȡ��������1.5g����1.5g
                        //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                        if (itemObj.SysClass.ID.ToString() == "PCC")
                        {
                            qty = Math.Round(doseOnce * frequence * days / baseDose, 2);
                            unit = itemObj.MinUnit;
                            unitFlag = "1";//��С��λ
                        }
                        else
                        {
                            //��ҩ�����������������ÿ������2/3Ƭ�ģ�
                            // ���ڳ�����������������������ȡһ�� ��ȡ�� houwb
                            qty = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce * frequence * days / baseDose, 3)));
                            unit = itemObj.MinUnit;
                            unitFlag = "1";
                        }
                        break;
                    case "1":
                        //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                        //��ҩ��������ȡ��������1.5g����1.5g
                        //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                        if (itemObj.SysClass.ID.ToString() == "PCC")
                        {
                            qty = Math.Round((doseOnce * frequence * days / baseDose) / itemObj.PackQty, 2);
                            unit = itemObj.PackUnit;
                            unitFlag = "0";//��װ��λ
                        }
                        else
                        {
                            qty = Math.Ceiling((doseOnce * frequence * days / baseDose) / itemObj.PackQty);
                            unit = itemObj.PackUnit;
                            unitFlag = "0";
                        }
                        break;
                    case "2":
                        //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                        //��ҩ��������ȡ��������1.5g����1.5g
                        //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                        if (itemObj.SysClass.ID.ToString() == "PCC")
                        {
                            qty = Math.Round(doseOnce * frequence * days / baseDose, 2);
                            unit = itemObj.MinUnit;
                            unitFlag = "1";//��С��λ
                        }
                        else
                        {
                            qty = Math.Ceiling(Math.Round(Math.Ceiling(doseOnce / baseDose) * frequence * days, 6));
                            unit = itemObj.MinUnit;
                            unitFlag = "1";
                        }
                        break;
                    case "3":
                        //��ҩ������ȡ��  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                        //��ҩ��������ȡ��������1.5g����1.5g
                        //�����1��ÿ�μ�����λ6g��������һ��3g �Ͱ���0.5����ҩ
                        if (itemObj.SysClass.ID.ToString() == "PCC")
                        {
                            qty = Math.Round((doseOnce / baseDose * frequence * days) / itemObj.PackQty, 2);
                            //unit = itemObj.MinUnit;
                            unit = itemObj.PackUnit;
                            unitFlag = "0";//��װ��λ
                        }
                        else
                        {
                            qty = Math.Ceiling(Math.Round((Math.Ceiling((doseOnce / baseDose) / itemObj.PackQty)) * frequence * days, 6));
                            unit = itemObj.PackUnit;
                            unitFlag = "0";
                        }
                        break;
                    default:
                        qty = Math.Round(doseOnce / baseDose, 2) * frequence * days;
                        unit = itemObj.MinUnit;
                        unitFlag = "1";
                        break;
                }
            }
            catch (Exception ex)
            {
                errInfo = ex.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���¼�������
        /// {37C70D4B-53A8-46a4-B9CB-FF4583E9DFAE}
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [Obsolete("����,��ֲ��FS.HISFC.Components.Order.Classes.Function����ReComputeQty����", true)]
        public static int ReComputeQty(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            //����û����ʱ��������
            if (order.HerbalQty <= 0)
            {
                return 1;
            }
            if (order.Item.ID != "999")
            {
                try
                {
                    decimal frequence = 0;
                    FS.HISFC.Models.Order.Frequency freqTemp = new FS.HISFC.Models.Order.Frequency();

                    freqTemp = Components.Order.Classes.Function.GetFreqHelper().GetObjectFromID(order.Frequency.ID) as FS.HISFC.Models.Order.Frequency;
                    if (freqTemp == null)
                    {
                        freqTemp = order.Frequency;
                    }

                    if (freqTemp.Days[0] == "0" || string.IsNullOrEmpty(freqTemp.Days[0]))
                    {
                        freqTemp.Days[0] = "1";
                        frequence = freqTemp.Times.Length;
                    }
                    else
                    {
                        try
                        {
                            frequence = Math.Round(freqTemp.Times.Length / FS.FrameWork.Function.NConvert.ToDecimal(freqTemp.Days[0]), 2);
                        }
                        catch
                        {
                            frequence = freqTemp.Times.Length;
                        }
                    }

                    //��ҩ���㷽ʽ��һ��
                    if (order.Item.ItemType == EnumItemType.Drug)
                    {
                        HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                        if (phaItem == null)
                        {
                            MessageBox.Show("����ҩƷ��Ŀʧ��");
                            return -1;
                        }

                        string unit = order.Unit;
                        string unitFlag = order.MinunitFlag;
                        decimal qty = order.Qty;

                        string err = "";

                        decimal doseOnce = order.DoseOnce;

                        if (order.DoseUnit == phaItem.MinUnit)
                        {
                            doseOnce = order.DoseOnce * phaItem.BaseDose;
                        }
                        //order.DoseOnce = doseOnce;

                        if (ReComputeQtyBase(phaItem, doseOnce, phaItem.BaseDose, frequence, order.HerbalQty, ref qty, ref unit, ref unitFlag, ref err) == -1)
                        {
                            MessageBox.Show(err);
                            return -1;
                        }
                        order.Qty = qty;
                        order.Unit = unit;
                        order.MinunitFlag = unitFlag;
                    }
                    else
                    {
                        order.Qty = frequence * order.HerbalQty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ReComputeQty" + ex.Message);
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ��ʾҽ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        public static void ShowOrder(object sender, ArrayList alOrder)
        {
            ShowOrder(sender, alOrder, 0);
        }

        /// <summary>
        /// ��ʾҽ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="alOrder"></param>
        /// <param name="type"></param>
        public static void ShowOrder(object sender, ArrayList alOrder, int type)
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

                dtMain.Columns.AddRange(new DataColumn[]{  new DataColumn("ID", dtStr),new DataColumn("��Ϻ�", dtStr), new DataColumn("ҽ������", dtStr),new DataColumn("���", dtStr),
                                                            new DataColumn("���", dtStr),new DataColumn("���ʱ��", dtStr),new DataColumn("ÿ�μ���", dtStr),
                                                            new DataColumn("Ƶ��", dtStr),new DataColumn("����", dtStr),new DataColumn("����", dtStr),
                                                            new DataColumn("�÷�", dtStr),new DataColumn("ҽ������", dtStr),new DataColumn("�Ӽ�", dtBool),
                                                            new DataColumn("��ʼʱ��", dtStr),new DataColumn("����ʱ��", dtStr),new DataColumn("����ҽ��", dtStr),
                                                            new DataColumn("ִ�п���", dtStr),new DataColumn("ֹͣʱ��", dtStr),new DataColumn("ֹͣҽ��", dtStr),
                                                            new DataColumn("��ע", dtStr),new DataColumn("˳���", dtStr)});

                //FS.HISFC.BizLogic..OrderType orderType = new FS.HISFC.BizLogic.Manager.OrderType();

                FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(CacheManager.InterMgr.QueryOrderTypeList());
                #endregion

                string beginDate = "", endDate = "", moDate = "";
                ArrayList alTemp = new ArrayList();

                for (int i = 0; i < alOrder.Count; i++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order o = alOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;
                    FS.HISFC.Models.Base.Item tempItem = null;

                    #region ������Ŀ��Ϣ
                    if (o.Item == null || o.Item.ID == "")
                    {
                        if (o.ID == "999")//�Ա���Ŀ
                        {
                            FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                            undrug.ID = o.ID;
                            undrug.Name = o.Name;
                            undrug.Qty = o.Item.Qty;
                            //undrug.IsPharmacy = false;
                            undrug.ItemType = EnumItemType.UnDrug;
                            undrug.SysClass.ID = "M";
                            undrug.PriceUnit = o.Unit;
                            tempItem = undrug;
                            o.Item = tempItem;
                            alTemp.Add(o);
                        }
                        else if (o.ID.Substring(0, 1) == "F")//��ҩƷ
                        {
                            #region ��ҩƷ
                            tempItem = CacheManager.FeeIntegrate.GetItem(o.Item.ID);
                            if (tempItem == null || tempItem.ID == "")
                            {
                                MessageBox.Show("��Ŀ" + o.Name + "�Ѿ�ͣ�ã�!", "��ʾ");

                                o.Item.ID = o.ID;
                                o.Item.Name = o.Name;
                                o.ExtendFlag2 = "N";
                            }
                            else
                            {
                                if (o.ExeDept.ID.Length <= 0)
                                {
                                    if (((FS.HISFC.Models.Fee.Item.Undrug)tempItem).ExecDepts.Count > 0)
                                        o.ExeDept.ID = (((FS.HISFC.Models.Fee.Item.Undrug)tempItem).ExecDepts[0]).ToString();
                                    else
                                        o.ExeDept = new FS.FrameWork.Models.NeuObject();
                                }
                                tempItem.Qty = o.Item.Qty;
                                o.Item = tempItem;
                                alTemp.Add(o);
                            }
                            #endregion
                        }
                        else if (o.ID.Substring(0, 1) == "Y")//ҩƷ
                        {
                            #region ҩƷ
                            ////FS.HISFC.Models.RADT.Person p = pManager.Operator as FS.HISFC.Models.RADT.Person;
                            ////if (p == null) return;
                            ////tempItem = pManager.GetItemForInpatient(p.Dept.ID, o.ID);
                            tempItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(o.Item.ID);
                            if (tempItem == null || tempItem.ID == "")
                            {
                                MessageBox.Show("��Ŀ" + o.Name + "�Ѿ�ͣ�ã�!", "��ʾ");

                                o.ExtendFlag2 = "N";
                            }
                            else
                            {
                                //ҩƷִ�п���Ϊ��

                                tempItem.Qty = o.Item.Qty;
                                o.Item = tempItem;
                                o.StockDept.ID = tempItem.User02;

                                FS.HISFC.Models.Base.Department dept = null;
                                if (o.StockDept != null && o.StockDept.ID != null && o.StockDept.ID != "")
                                    dept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(o.StockDept.ID);
                                if (dept != null && dept.ID != "")
                                    o.StockDept.Name = dept.Name;

                                alTemp.Add(o);
                            }
                            #endregion
                        }
                        else if (o.Unit == "[������]")//������Ŀ
                        {
                            #region ������
                            FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                            FS.HISFC.Models.Fee.Item.Undrug zt = CacheManager.FeeIntegrate.GetItem(o.ID);
                            if (zt == null)
                            {
                                MessageBox.Show("������Ŀ:" + o.Name + "�Ѿ�ͣ�û���ɾ��,���ܵ���!", "��ʾ");

                                o.ExtendFlag2 = "N";
                            }
                            else
                            {

                                undrug.ID = o.ID;
                                undrug.Name = o.Name;
                                undrug.Qty = o.Item.Qty;
                                //undrug.IsPharmacy = false;
                                undrug.ItemType = EnumItemType.UnDrug;
                                undrug.SysClass.ID = zt.SysClass;
                                undrug.PriceUnit = o.Unit;
                                o.ExeDept.ID = zt.ExecDept;
                                tempItem = undrug as FS.HISFC.Models.Base.Item;
                                o.Item = tempItem;

                                alTemp.Add(o);
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        alTemp.Add(o);
                    }
                    #endregion

                    #region ��ʾҽ��
                    if (o.Item != null && o.ExtendFlag2 != "N")
                    {

                        if (o.BeginTime == DateTime.MinValue)
                            beginDate = "";
                        else
                            beginDate = o.BeginTime.ToString();

                        if (o.EndTime == DateTime.MinValue)
                            endDate = "";
                        else
                            endDate = o.EndTime.ToString();

                        if (o.MOTime == DateTime.MinValue)
                            moDate = "";
                        else
                            moDate = o.MOTime.ToString();

                        if (o.Item.ItemType == EnumItemType.Drug)
                        {
                            FS.HISFC.Models.Pharmacy.Item item = o.Item as FS.HISFC.Models.Pharmacy.Item;
                            o.DoseUnit = item.DoseUnit;
                            dtMain.Rows.Add(new Object[] {  o.ID,o.Combo.ID,o.Item.Price.ToString()+"Ԫ/"+o.Item.Name,o.Item.Specs,
                                                             "",o.User03,o.DoseOnce.ToString()+item.DoseUnit ,
                                                             o.Frequency.ID,o.Qty.ToString()+o.Unit,o.HerbalQty,o.Usage.Name,
                                                             /*o.OrderType.Name*/"����",o.IsEmergency,beginDate,moDate,o.ReciptDoctor.Name,o.ExeDept.Name,endDate,
                                                             o.DCOper.Name,o.Memo,o.SortID});

                        }
                        else if (o.Item.ItemType == EnumItemType.UnDrug)
                        {
                            if (o.Unit == "[������]")
                            {
                                o.Item.Price = Order.OutPatient.Classes.Function.GetUndrugZtPrice(o.Item.ID);
                            }
                            dtMain.Rows.Add(new Object[] { o.ID,o.Combo.ID,o.Item.Price.ToString()+"Ԫ/"+o.Item.Name,o.Item.Specs,
                                                             "",o.User03,"" ,
                                                             o.Frequency.ID,o.Qty.ToString()+o.Unit,"","",
                                                             /*o.OrderType.Name*/"����",o.IsEmergency,beginDate,moDate,o.ReciptDoctor.Name,
                                                             o.ExeDept.Name,endDate,
                                                             o.DCOper.Name,o.Memo,o.SortID});

                        }
                        else
                        {
                            dtMain.Rows.Add(new Object[] { o.ID,o.Combo.ID,o.Item.Name,o.Item.Specs,
                                                             "",o.User03,o.DoseOnce.ToString()+o.DoseUnit,
                                                             o.Frequency.ID,o.Qty.ToString()+o.Unit,o.HerbalQty,o.Usage.Name,
                                                             /*o.OrderType.Name*/"����",o.IsEmergency,beginDate,moDate,o.ReciptDoctor.Name,
                                                             o.ExeDept.Name,endDate,
                                                             o.DCOper.Name,o.Memo,o.SortID});
                        }

                    #endregion
                    }
                }
                #endregion

                switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
                {
                    case "SheetView":
                        FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                        o.RowCount = 0;
                        o.DataSource = myDataSet.Tables[0];
                        for (int i = 0; i < alTemp.Count; i++)
                        {
                            if ((alTemp[i] as FS.HISFC.Models.Order.OutPatient.Order).ExtendFlag2 != "N")
                            {
                                o.Rows[i].Tag = alTemp[i];
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
                        o.Columns[2].Width = 150;
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
                        o.Columns[13].Width = 100;
                        o.Columns[14].Width = 80;
                        o.Columns[15].Width = 80;
                        o.Columns[16].Width = 80;
                        o.Columns[17].Width = 80;
                        o.Columns[18].Width = 80;
                        o.Columns[19].Width = 80;
                        o.Columns[20].Width = 30;
                        if (type == 1)//����
                        {
                            o.Columns[5].Visible = true;
                        }
                        else
                        {
                            o.Columns[5].Visible = false;
                        }
                        #endregion

                        Order.Classes.Function.DrawCombo(o, 1, 4);
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

        /// <summary>
        /// ���ݸ������������۸�
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static decimal GetUndrugZtPrice(string ID)
        {
            decimal tot_cost = 0m;
            tot_cost = CacheManager.FeeIntegrate.GetUndrugCombPrice(ID);
            return tot_cost;
        }

        /// <summary>
        /// ���ݸ����������������
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetUndrugZtSample(string ID)
        {
            if (ID == "")
            {
                return "";
            }

            ArrayList al = null;

            al = CacheManager.InterMgr.QueryUndrugPackageDetailByCode(ID);
            if (al == null)
            {
                return "";
            }

            string sampleName = "";

            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Fee.Item.UndrugComb info = al[i] as FS.HISFC.Models.Fee.Item.UndrugComb;
                if (info == null || info.ValidState == "1")
                {
                    continue;
                }
                FS.HISFC.Models.Fee.Item.Undrug item = CacheManager.FeeIntegrate.GetItem(info.ID);
                if (item == null)
                {
                    continue;
                }

                if (item.CheckBody != null && item.CheckBody.Length > 0)
                {
                    sampleName = item.CheckBody;
                    break;
                }
            }
            return sampleName;
        }

        /// <summary>
        /// ������׼۸�
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static decimal GetGroupPrice(string ID)
        {
            if (ID == "")
            {
                return 0m;
            }
            ArrayList al = CacheManager.InterMgr.QueryGroupDetailByGroupCode(ID);
            if (al == null || al.Count <= 0)
            {
                return 0m;
            }
            decimal tot = 0m;
            foreach (FS.HISFC.Models.Fee.ComGroupTail detail in al)
            {
                if (detail.itemCode.Substring(0, 1) == "Y")
                {
                    FS.HISFC.Models.Pharmacy.Item phaitem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(detail.itemCode);
                    if (phaitem == null)
                    {
                        continue;
                    }
                    if (detail.unitFlag == "1")
                    {
                        if (phaitem.PackQty == 0)
                        {
                            phaitem.PackQty = 1;
                        }
                        tot += phaitem.Price * detail.qty / phaitem.PackQty;
                    }
                    else
                    {
                        if (phaitem.PackQty == 0)
                        {
                            phaitem.PackQty = 1;
                        }
                        tot += phaitem.Price * detail.qty;
                    }
                }
                else if (detail.itemCode.Substring(0, 1) == "F")
                {
                    FS.HISFC.Models.Fee.Item.Undrug feeitem = CacheManager.FeeIntegrate.GetItem(detail.itemCode);
                    if (feeitem == null)
                    {
                        continue;
                    }
                    tot += feeitem.Price * detail.qty;
                }
                else
                {
                    tot += Function.GetUndrugZtPrice(detail.itemCode);
                }
            }
            return tot;
        }

        /// <summary>
        /// �����Ŀ�۸�
        /// </summary>
        /// <param name="order">ҽ��ʵ��</param>
        /// <param name="item">ҽ����Ŀ</param>
        /// <param name="patintInfo">����ʵ��</param>
        /// <param name="pactInfo">��ͬ��λ��Ϣ</param>
        /// <param name="isNewItem">��ǰҽ����Ŀ�Ƿ��Ǵ����ݿ���ȡ</param>
        /// <returns></returns>
        [Obsolete("���ϣ�ʹ��integrate�еĺ���", true)]
        public static decimal GetPrice(FS.HISFC.Models.Order.OutPatient.Order order, FS.HISFC.Models.Base.Item itemNew, FS.HISFC.Models.RADT.Patient patintInfo, FS.HISFC.Models.Base.PactInfo pactInfo, bool isNewItem)
        {
            if (order.Item.ID == "999")
            {
                return 0;
            }

            FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger();

            DateTime nowDate = db.GetDateTimeFromSysDateTime();

            int age = (int)((new TimeSpan(nowDate.Ticks - patintInfo.Birthday.Ticks)).TotalDays / 365);
            //{B9303CFE-755D-4585-B5EE-8C1901F79450}���ӻ�ȡ�����
            string priceForm = pactInfo.PriceForm;

            if (order.Unit != "[������]")
            {
                if (isNewItem)
                {
                    if (itemNew.ItemType == EnumItemType.Drug)
                    {
                        return CacheManager.FeeIntegrate.GetPrice(priceForm, age, itemNew.Price, itemNew.ChildPrice, itemNew.SpecialPrice, ((FS.HISFC.Models.Pharmacy.Item)itemNew).RetailPrice2);
                    }
                    else
                    {
                        return CacheManager.FeeIntegrate.GetPrice(priceForm, age, itemNew.Price, itemNew.ChildPrice, itemNew.SpecialPrice, itemNew.Price);
                    }
                }
                else
                {
                    if (order.Item.ID.Substring(0, 1) == "F")
                    {
                        FS.HISFC.Models.Fee.Item.Undrug item = CacheManager.FeeIntegrate.GetItem(order.Item.ID);

                        if (item == null)
                        {
                            MessageBox.Show("������Ŀ" + order.Item.Name + "ʧ��!", "��ʾ");
                            return order.Item.Price;
                        }
                        return CacheManager.FeeIntegrate.GetPrice(priceForm, age, item.Price, item.ChildPrice, item.SpecialPrice, item.Price);

                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);

                        if (item == null)
                        {
                            MessageBox.Show("������Ŀ" + order.Item.Name + "ʧ��!", "��ʾ");
                            return order.Item.Price;
                        }
                        return CacheManager.FeeIntegrate.GetPrice(priceForm, age, item.Price, item.Price, item.Price, item.RetailPrice2);
                    }
                }
            }
            else
            {
                ArrayList alZt = CacheManager.InterMgr.QueryUndrugPackageDetailByCode(order.Item.ID);

                if (alZt == null)
                {
                    MessageBox.Show("���Ҹ�����Ŀ" + order.Item.Name + "ʧ��!", "��ʾ");
                    return order.Item.Price;
                }

                decimal price = 0m;

                foreach (FS.HISFC.Models.Fee.Item.UndrugComb info in alZt)
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = CacheManager.FeeIntegrate.GetItem(info.ID);

                    if (item == null)
                    {
                        MessageBox.Show("���Ҹ�����Ŀ��ϸ" + info.ID + "ʧ��!", "��ʾ");
                        return order.Item.Price;
                    }

                    //�۸�*����
                    price += CacheManager.FeeIntegrate.GetPrice(priceForm, age, item.Price, item.ChildPrice, item.SpecialPrice, item.Price);
                }

                return price;
            }
        }

        /// <summary>
        /// ȡҽ����ˮ��
        /// </summary>
        /// <returns></returns>
        public static string GetNewOrderID(ref string errInfo)
        {
            CacheManager.OutOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            string rtn = "";
            rtn = CacheManager.InOrderMgr.GetNewOrderID();
            if (rtn == null || rtn == "")
            {
                errInfo = "������ҽ����ˮ�ţ�";
            }
            else
            {
                return rtn;
            }
            return "";
        }

        /// <summary>
        /// �½�XML
        /// </summary>
        /// <returns></returns>
        public static int CreateXML(string fileName, string extendTime, string opertime)
        {
            string path;
            try
            {
                path = fileName.Substring(0, fileName.LastIndexOf(@"\"));
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch { }

            FS.FrameWork.Xml.XML myXml = new FS.FrameWork.Xml.XML();
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            root = myXml.CreateRootElement(doc, "Setting", "1.0");

            XmlElement e = myXml.AddXmlNode(doc, root, "�ӳ�����", "");
            myXml.AddNodeAttibute(e, "ExtendTime", extendTime);

            e = myXml.AddXmlNode(doc, root, "����ʱ��", "");
            myXml.AddNodeAttibute(e, "LastOperTime", opertime);

            try
            {
                StreamWriter sr = new StreamWriter(fileName, false, System.Text.Encoding.Default);
                string cleandown = doc.OuterXml;
                sr.Write(cleandown);
                sr.Close();
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show("�޷����棡" + ex.Message); }

            return 1;
        }

        /// <summary>
        /// ������и�����Ϣ
        /// </summary>
        public static void SethsUsageAndSub()
        {
            if (isNewSubFeeSet)
            {
                hsUsageAndSub = CacheManager.OutOrderMgr.GetNewUsageAndSub();
            }
            else
            {
                hsUsageAndSub = CacheManager.OutOrderMgr.GetUsageAndSub();
            }
        }

        /// <summary>
        /// �Ƿ�Ժע�÷�
        /// </summary>
        /// <param name="usageID"></param>
        /// <returns></returns>
        public static bool CheckIsInjectUsage(string usageID)
        {
            if (injectUsageHelper == null)
            {
                ArrayList alUsage = CacheManager.GetConList("InjectUsage");
                injectUsageHelper = new FS.FrameWork.Public.ObjectHelper(alUsage);
            }

            if (HsUsageAndSub.Contains(usageID))
            {
                return true;
            }
            else if (injectUsageHelper.GetObjectFromID(usageID) != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ��Ÿ��ƿ��������ﴦ��
        /// </summary>
        public static ArrayList AlCopyOrders = new ArrayList();

        #region ͨ�ÿ��Ʋ���

        /// <summary>
        /// LIS������Ŀ�Ƿ�����������й�ѡ��ϸ��Ŀ��Ŀǰ��ҽ�������õ���
        /// </summary>
        /// <returns></returns>
        public static bool IsLisSelectDetail(string sysClassID)
        {
            string vlue = Classes.Function.GetBatchControlParam("HNMZ80", false, "0");

            string control="";

            if (vlue.Length == 1)
            {
                control = vlue;
            }
            else if (vlue.Length == 2)
            {
                if (sysClassID == "UL")
                {
                    control = vlue.Substring(0, 1);
                }
                else if (sysClassID == "UC")
                {
                    control = vlue.Substring(1, 1);
                }
            }
            if (control == "0")
            {
                return false;
            }
            else if (control == "1")
            {
                //�����Ŀ���ÿ�����ϸ�������������ƽ��п���{43E10122-9C4D-4e3d-AA1A-1C10AC68D20B}
                return false;
                //return true;
            }
            return false;
        }

        public static Hashtable hsControlParam = null;

        /// <summary>
        /// ������ѯ���Ʋ���
        /// </summary>
        /// <param name="controlCode"></param>
        /// <param name="isRefresh"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetBatchControlParam(string controlCode, bool isRefresh,string defaultValue)
        {
            if (hsControlParam == null)
            {
                hsControlParam = new Hashtable();

                hsControlParam.Add("MZ0093", "0");
                hsControlParam.Add("HNMZ39", "0");
                hsControlParam.Add("HNMZ41", "00");
                hsControlParam.Add("200004", "0");
                hsControlParam.Add("HNMZ29", "0");
                hsControlParam.Add("HNMZ30", "0");
                hsControlParam.Add("HNMZ50", "0");
                hsControlParam.Add("200000", "0");
                hsControlParam.Add("200005", "0");
                hsControlParam.Add("200006", "1");
                hsControlParam.Add("200007", "1");
                hsControlParam.Add("200022", "1");
                hsControlParam.Add("200021", "0");
                hsControlParam.Add("HNMZ25", "0");

                //�Ƿ������޸�ÿ�ο�������ʱ�Ĵ�����ͬ��λ��Ϣ
                //houwb ҽ�����Էѷ����� {A1FE7734-267C-43f1-A824-BF73B482E0B2}
                hsControlParam.Add("HNMZ43", "0");
                //end houwb

                hsControlParam.Add("HNMZ26", "0");
                hsControlParam.Add("200201", "1");
                hsControlParam.Add("MZ5001", "1");
                hsControlParam.Add("200302", "0");
                hsControlParam.Add("HNMZ27", "0");
                hsControlParam.Add("HNMZ23", "0");
                hsControlParam.Add("HNMZ24", "0");
                hsControlParam.Add("HNMZ98", "0");
                hsControlParam.Add("HNMZ96", "0");
                hsControlParam.Add("HNMZ31", "0");
                hsControlParam.Add("HNMZ32", "-1");
                hsControlParam.Add("HNMZ33", "0");
                hsControlParam.Add("P01015", "0");
                hsControlParam.Add("MZ0073", "0");
                hsControlParam.Add("200212", "0");


                hsControlParam.Add("200018", "0");

                hsControlParam.Add("HNMZ99", "1");
                hsControlParam.Add("HNMZ91", "0");
                hsControlParam.Add("HNMZ88", "0");
                hsControlParam.Add("200301", "0");
                hsControlParam.Add("HNMZ34", "0");
                hsControlParam.Add("200320", "1");
                hsControlParam.Add("HNMZ10", "0");

                hsControlParam.Add("200001", "0");

                hsControlParam.Add(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, "0");
                hsControlParam.Add("MZ0300", "0");

                //���鸴����Ŀ�Ƿ�����ѡ��ϸ
                hsControlParam.Add("HNMZ80", "0");

                //�Ƿ��Զ������ҺŷѺ���𡢲���
                // 0 ��������1 �Զ�������2 ֻ�������͹Һŷѣ�3 ֻ���ղ���
                hsControlParam.Add("HNMZ42", "1");
                //�Ƿ���ʾͬһ�ξ����п����ظ���Ŀ��δִ����Ŀ
                hsControlParam.Add("HNMZ97", "0");
                //�Ƿ���ʾ��ִ��ҽ�������˷�
                hsControlParam.Add("HNMZ95", "0");
                if (CacheManager.ContrlManager.GetBatchControlParam(ref hsControlParam) == -1)
                {
                    MessageBox.Show("��ѯ���Ʋ�������\r\n" + CacheManager.ContrlManager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            if (!hsControlParam.Contains(controlCode))
            {
                return CacheManager.ContrlManager.GetControlParam<string>(controlCode, isRefresh, defaultValue);
            }
            else
            {
                return hsControlParam[controlCode].ToString();
            }
        }

        /// <summary>
        /// �Ƿ������������
        /// </summary>
        private static int isUseNurseArray = -1;

        /// <summary>
        /// �Ƿ������������ 1 ���ã����� ������
        /// </summary>
        /// <returns></returns>
        public static bool IsUseNurseArray()
        {
            if (isUseNurseArray == -1)
            {
                isUseNurseArray = FS.FrameWork.Function.NConvert.ToInt32(GetBatchControlParam("200018", false, "0"));
            }
            if (isUseNurseArray == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// �Һż��������
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper regLevelHelper = null;

        /// <summary>
        /// �Һż��������
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper RegLevelHelper
        {
            get
            {
                if (regLevelHelper == null)
                {
                    regLevelHelper = new FS.FrameWork.Public.ObjectHelper();
                    regLevelHelper.ArrayObject = CacheManager.RegInterMgr.QueryRegLevel();
                    if (regLevelHelper.ArrayObject == null)
                    {
                        MessageBox.Show("��ѯ�Һż������\r\n" + CacheManager.RegInterMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return Function.regLevelHelper;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ȥ��������ͬ�豸���ͺ��������͵�ҽ��(δʵ��)
        /// </summary>
        /// <param name="altempMedItem"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int RemoveOrderHaveSameContiner(ArrayList altempMedItem, FS.HISFC.Models.MedTech.Management.Member item)
        {
            //if (altempMedItem.Count <= 0)
            //{
            //    return 0;
            //}

            int count = 0;
            //ArrayList al = new ArrayList();

            //foreach (FS.HISFC.Models.MedTech.Management.Member temp in altempMedItem)
            //{
            //    //�豸���ͺ�����������ͬ
            //    if (temp.ItemExtend.Container == item.ItemExtend.Container && temp.ItemExtend.MachineType == item.ItemExtend.MachineType)
            //    {
            //        al.Add(temp);
            //        count++;
            //    }
            //}
            //for (int i = 0; i < al.Count; i++)
            //{
            //    altempMedItem.Remove(al[i]);
            //}
            return count;
        }

        /// <summary>
        /// ���Ҹ���Ҫ����޶����
        /// </summary>
        /// <param name="stats">ͳ�Ʊ���</param>
        /// <param name="relations">�޶���ȹ�ϵ</param>
        /// <returns>��ǰ�Զ�</returns>
        private static FS.FrameWork.Models.NeuObject GetRelation(ArrayList stats, ArrayList relations)
        {
            foreach (FS.FrameWork.Models.NeuObject stat in stats)
            {
                foreach (FS.HISFC.Models.Base.PactStatRelation obj in relations)
                {
                    if (stat.ID == obj.Group.ID)
                    {
                        return obj;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// ���㹫�ѳ���
        /// </summary>
        /// <param name="rInfo"></param>
        /// <param name="alOrder"></param>
        /// <param name="relations"></param>
        /// <param name="errText"></param>
        public static int Compute(FS.HISFC.Models.Registration.Register rInfo, ArrayList alOrder, ArrayList relations, ref string PayType, ref string errText)
        {
            //ArrayList feeDetails = new ArrayList();

            //foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            //{
            //    FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = ChangeToFeeItemList(order);

            //    if (feeItem != null)
            //    {
            //        feeDetails.Add(feeItem);
            //    }
            //}

            //// TODO:  ��� ComputePubFee.FS.Common.Clinic.Interface.IComputePubFee.Compute ʵ��
            //if (rInfo == null || rInfo.ID == "")
            //{
            //    errText = "���߻�����ϢΪ�գ�";
            //    return -1;
            //}

            //if (feeDetails == null)
            //{
            //    errText = "������ϸ����Ϊ�գ�";
            //    return -1;
            //}

            //if (rInfo.Pact == null || rInfo.Pact.ID == "")
            //{
            //    errText = "���ߺ�ͬ��λΪ�գ�";
            //    return -1;
            //}


            //FS.HISFC.BizLogic.Fee.FeeCodeStat feeMgr = new FS.HISFC.BizLogic.Fee.FeeCodeStat();

            ////����ܶ�
            //for (int i = 0; i < relations.Count; i++)
            //{
            //    ((FS.FrameWork.Models.NeuObject)relations[i]).User03 = "0";
            //}

            //foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
            //{
            //    if (f == null)
            //    {
            //        continue;
            //    }

            //    //rowFindStat --������С�����ҵ���Ӧ����

            //    ArrayList stats = feeMgr.GetGFDYFeeCodeStatByFeeCode(f.Item.MinFee.ID);

            //    //û���ҵ���Ӧ�ķ��ô���˵���϶������ڳ�������
            //    if (stats == null)//if(rowFindStat == null)
            //    {
            //        continue;
            //    }

            //    //����޶���ȹ�ϵ
            //    FS.FrameWork.Models.NeuObject relation = GetRelation(stats, relations);

            //    //û���ҵ���Ӧ�ķ��ô����޶�˵�������ڳ�������
            //    if (relation == null)//if(index == -1)
            //    {
            //        continue;
            //    }

            //    //��ʱ�洢���߻����ܽ��
            //    decimal tempTotCost = FS.FrameWork.Function.NConvert.ToDecimal(relation.User03);
            //    //ͳ�ƴ�����޶�
            //    decimal limitCost = FS.FrameWork.Function.NConvert.ToDecimal(((FS.HISFC.Models.Base.PactStatRelation)relation).Quota);

            //    //�����޶�
            //    if (tempTotCost + f.FT.TotCost > limitCost)
            //    {
            //        if (relation.User01 != "TRUE")
            //        {
            //            MessageBox.Show("����" + rInfo.Name + "��" + ((FS.HISFC.Models.Base.PactStatRelation)relation).StatClass.Name + "�Ѿ����꣡�쿴�޶����ڻ��߷�����Ϣ�����", "��ʾ");
            //            relation.User01 = "TRUE";
            //        }
            //        return 0;
            //    }
            //    else
            //    {
            //        relation.User03 = (tempTotCost + f.FT.TotCost).ToString();
            //    }
            //}
            return 1;
        }

        /// <summary>
        /// ��õ�ǰ���Ѳ��ʵ��
        /// </summary>
        /// <returns>null ���ʵ��ʧ��</returns>
        public static object GetPubFeeInstance()
        {
            //�����㷨���·��
            string pubFeeComputeDll = null;
            string errText = "";
            //��ù��Ѳ��·��
            ////pubFeeComputeDll = CacheManager.ContrlManager.QueryControlerInfo(FS.Common.Clinic.Class.Const.PUBFEECOMPUTE);
            if (pubFeeComputeDll == null || pubFeeComputeDll == "")
            {
                errText = "û��ά�������㷨����!��ά��";
                return null;
            }
            //�õ�ȫ·��
            pubFeeComputeDll = Application.StartupPath + pubFeeComputeDll;
            Assembly a = null;
            System.Type[] types = null;
            //��ʱʵ��
            object objInstance = null;
            try
            {
                //��õ�ǰ·��dll�ķ�����Ϣ
                a = Assembly.LoadFrom(pubFeeComputeDll);
                //�õ���������������.
                types = a.GetTypes();
                foreach (System.Type type in types)
                {
                    //������Ϲ��Ѽ���ӿ�,��ôʵ����,������ѭ��.
                    if (type.GetInterface("IComputePubFee") != null)
                    {
                        //ʵ��������ʵ��.
                        objInstance = System.Activator.CreateInstance(type);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                errText = ex.Message;
                return null;
            }
            finally
            {
                a = null;
                types = null;
            }

            return objInstance;
        }

        /// <summary>
        /// ���ѷ��ü���
        /// </summary>
        /// <param name="pubFeeInstance">���ѷ��ü�����ʵ��</param>
        /// <param name="r">�Һ�ʵ��</param>
        /// <param name="feeDetails">������ϸ����</param>
        /// <param name="relations">�޶��ϵ</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>- 1 ʧ�� 0 �ɹ�</returns>
        public static int ComputePubFee(object pubFeeInstance, FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails, ArrayList relations, ref string errText)
        {
            if (pubFeeInstance == null)
            {
                errText = "�����㷨ʵ��Ϊ��!";
                return -1;
            }
            if (pubFeeInstance.GetType().GetInterface("IComputePubFee") == null)
            {
                errText = "�����㷨û��ʵ��IComputePubFee�ӿ�!";
                return -1;
            }
            int iReturn = 0;//����ֵ
            try
            {
                ////iReturn = ((FS.Common.Clinic.Interface.IComputePubFee)pubFeeInstance).Compute(r, ref feeDetails, relations, ref errText);
            }
            catch (Exception ex)
            {
                errText += ex.Message;
                return -1;
            }
            if (iReturn == -1)
            {
                return -1;
            }

            return 0;
        }

        #endregion

        #region ����Ƿ�����Ч�Һż�¼

        /// <summary>
        /// ��ȡ�Һ���Чʱ��
        /// </summary>
        /// <returns></returns>
        public static DateTime GetRegValideDate(bool isEmergency)
        {
            //��ͨ���ﵱ����Ч������24Сʱ��Ч,��Ӧ���Ʋ���Ϊ200022

            //Ĭ��24Сʱ��Ч
            decimal validDays = 24;

            DateTime dtNow = CacheManager.ConManager.GetDateTimeFromSysDateTime();
            DateTime dtDate = dtNow;

            validDays = CacheManager.ContrlManager.GetControlParam<decimal>("200022", false, 24);

            if (validDays <= 0)
            {
                dtDate = dtNow.Date;
            }
            else
            {
                dtDate = dtNow.AddDays(0 - (double)validDays);
            }

            if (isEmergency)
            {
                validDays = Math.Ceiling(validDays) * 24;
                if (validDays == 0)
                {
                    validDays = 24;
                }

                dtDate = dtNow.AddDays(0 - (double)validDays);
            }

            return dtDate;
        }

        #endregion

        #region ���淭��

        /// <summary>
        /// ���뻺��
        /// </summary>
        private static Dictionary<string, string> dictTranslate = null;

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static string GetMsg(string strMsg)
        {
            //�ж�
            if (dictTranslate == null)
            {
                dictTranslate = new Dictionary<string, string>();
            }

            if (!dictTranslate.ContainsKey(strMsg))
            {
                #region ������

                string strTranslate = Language.Msg(strMsg);
                dictTranslate.Add(strMsg, strTranslate);
                return strTranslate;

                #endregion
            }
            else
            {
                #region ����

                string strTranslate = dictTranslate[strMsg];
                return strTranslate;

                #endregion
            }

        }

        /// <summary>
        /// ���淭��{038E4663-6430-4cc5-9F00-BD7128DE5C8F}
        /// </summary>
        /// <param name="controls"></param>
        public static void TranslateUI(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuTextBox) ||
                    c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuLabel) ||
                    c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox) ||
                    c.GetType() == typeof(System.Windows.Forms.ToolStrip) ||
                    c.GetType() == typeof(System.Windows.Forms.ToolStripButton) ||
                    c.GetType() == typeof(System.Windows.Forms.PrintPreviewControl) ||
                    c.GetType() == typeof(System.Windows.Forms.CheckedListBox) ||
                    c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuRadioButton) ||
                    c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuButton) ||
                    c.GetType() == typeof(System.Windows.Forms.Label))
                {
                    string endChar = string.Empty;
                    if (c.Text.Contains("��") || c.Text.Contains(":"))
                    {
                        endChar = ":";
                    }
                    c.Text = c.Text.Trim('��');
                    c.Text = c.Text.Trim(':');

                    c.Text = GetMsg(c.Text) + endChar;
                }

                if (c.Controls.Count > 0)
                {
                    TranslateUI(c.Controls);
                }
            }
        }

        /// <summary>
        /// ��ȡ��ĿӢ������
        /// </summary>
        /// <param name="outOrd"></param>
        /// <returns></returns>
        public static string GetItemEnglishName(FS.HISFC.Models.Order.OutPatient.Order outOrd)
        {
            string englishName = string.Empty;
            if (outOrd == null || string.IsNullOrEmpty(outOrd.Item.ID))
            {
                englishName = string.Empty;
                return englishName;
            }

            //��ȡ��ĿӢ������
            if (outOrd.Item.ItemType == EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item drugItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrd.Item.ID);
                if (drugItem != null && !string.IsNullOrEmpty(drugItem.ID))
                {
                    englishName = drugItem.NameCollection.EnglishName;
                    //outOrd.EnglishName = englishName;
                }
            }
            else
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem(outOrd.Item.ID);
                if (undrug != null && !string.IsNullOrEmpty(undrug.ID))
                {
                    englishName = undrug.NameCollection.EnglishName;
                    //outOrd.EnglishName = englishName;
                }
            }
            if (string.IsNullOrEmpty(englishName))
            {
                englishName = outOrd.Item.Name;
            }
            return englishName;
        }

        #endregion
    }

    /// <summary>
    /// ��־��¼
    /// </summary>
    public class LogManager
    {
        public static void Write(string logInfo)
        {
            //return;
            //���Ŀ¼�Ƿ����
            if (System.IO.Directory.Exists("./Log/Order/OutOrder") == false)
            {
                System.IO.Directory.CreateDirectory("./Log/Order/OutOrder");
            }

            FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();
            DateTime dtNow = dbMgr.GetDateTimeFromSysDateTime();

            //����һ�ܵ���־
            System.IO.File.Delete("./Log/Order/OutOrder/" + dtNow.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = dtNow.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Log/Order/OutOrder/" + name + ".LOG", true);
            w.WriteLine(dtNow.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
            w.Flush();
            w.Close();
        }
    }
}