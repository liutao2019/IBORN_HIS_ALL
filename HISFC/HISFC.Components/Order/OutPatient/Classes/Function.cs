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
    /// 本地处方处理
    /// </summary>
    public class Function
    {
        public Function()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            //查询全部医嘱参数
            ArrayList alControler = CacheManager.InterMgr.QueryControlerInfoByKind("MET");
            if (alControler == null)
            {
                MessageBox.Show("获取医嘱控制参数出错，系统将按照默认值进行操作！");
            }
            else
            {
                Function.controlerHelper.ArrayObject = alControler;
            }

            //是否新辅材带出算法
            isNewSubFeeSet = CacheManager.ContrlManager.GetControlParam<bool>("LHMZ01", false, false);
            if (ITruncFee == null)
            {
                ITruncFee = (FS.HISFC.BizProcess.Interface.Fee.ITruncFee)FS.HISFC.BizProcess.Interface.Fee.InterfaceManager.GetTruncFeeType();
            }
        }

        #region 接口配置
        static FS.HISFC.BizProcess.Interface.Fee.ITruncFee ITruncFee = null;
        #endregion


        #region 变量

        /// <summary>
        /// 是否新辅材带出算法
        /// 新表met_com_subtblitem
        /// 旧表fin_opb_inject
        /// </summary>
        private static bool isNewSubFeeSet = false;

        /// <summary>
        /// 用法和附材
        /// </summary>
        private static Hashtable hsUsageAndSub = new Hashtable();

        /// <summary>
        /// 用法和附材
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
        /// 全局控制参数帮助类
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper controlerHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 用法列表
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper usageHelper = null;

        /// <summary>
        /// 院注用法
        /// </summary>
        public static FS.FrameWork.Public.ObjectHelper injectUsageHelper = null;

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected static FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 是否按医保价进行直接结费
        /// </summary>
        public static bool isDirectSIFEE = false;

        #endregion

        /// <summary>
        ///  插入费用档
        /// </summary>
        /// <param name="fee"></param>//收费管理类
        /// <param name="order"></param>//医嘱实体
        /// <param name="reciptNo"></param>//处方号
        /// <param name="seqNo"></param>//处方流水号
        /// <param name="dtNow"></param>//操作时间
        /// <returns></returns>
        public static int SaveToFee(FS.HISFC.Models.Order.OutPatient.Order order, string reciptNo, int seqNo, DateTime dtNow)
        {
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitemlist = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();

            feeitemlist.Item.Qty = order.Item.Qty; //记价数量
            feeitemlist.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;//操作类型
            feeitemlist.Patient.ID = order.Patient.PID.ID;//门诊流水号
            feeitemlist.Patient.PID.CardNO = order.Patient.PID.CardNO;//门诊卡号 

            feeitemlist.ChargeOper.OperTime = dtNow;//划价日期
            feeitemlist.ChargeOper.ID = FS.FrameWork.Management.Connection.Operator.ID;//划价人
            feeitemlist.Order.CheckPartRecord = order.CheckPartRecord;//检体 
            feeitemlist.Order.Combo.ID = order.Combo.ID;//组合号
            if (order.Unit == "[复合项]")//如果是复合项目，置标记
            {
                feeitemlist.IsGroup = true;
                feeitemlist.UndrugComb.ID = order.User01;
                feeitemlist.UndrugComb.Name = order.User02;
            }

            //if(order.Item.IsPharmacy)
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                feeitemlist.ExecOper.Dept = order.StockDept.Clone();//传扣库科室，by zuowy
            }
            else
            {
                feeitemlist.ExecOper.Dept = order.ExeDept.Clone();//传执行科室
            }
            feeitemlist.InjectCount = order.InjectCount;//院内次数

            if (order.Item.PackQty <= 0)//包装单位为0，赋1
            {
                order.Item.PackQty = 1;
            }
            order.FT.OwnCost = order.Qty * order.Item.Price / order.Item.PackQty;//自付金额
            feeitemlist.FT = Round(order, 2);//取两位
            feeitemlist.Days = order.HerbalQty;//草药付数
            feeitemlist.Order.ReciptDept = order.ReciptDept;//开方科室信息
            feeitemlist.Order.ReciptDoctor = order.ReciptDoctor;//开方医生信息
            feeitemlist.Order.DoseOnce = order.DoseOnce;//每次用量
            feeitemlist.Order.DoseUnit = order.DoseUnit;//用量单位
            feeitemlist.Order.Frequency = order.Frequency.Clone();//频次信息
            feeitemlist.IsGroup = false;//是否组套
            feeitemlist.Order.Combo.IsMainDrug = order.Combo.IsMainDrug;//是否主药
            feeitemlist.ID = order.Item.ID;
            feeitemlist.Name = order.Item.Name;
            //if(order.Item.IsPharmacy )//是否药品
            if (order.Item.ItemType == EnumItemType.Drug)//是否药品
            {
                //add by sunm 不知道以下写法是否正确
                ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).BaseDose = ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose;//基本计量
                ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).Quality = ((FS.HISFC.Models.Pharmacy.Item)order.Item).Quality;//药品性质
                ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).DosageForm = ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm;//剂型

                feeitemlist.IsConfirmed = false;//是否终端确认
                feeitemlist.Item.PackQty = order.Item.PackQty;//包装数量
            }
            else
            {

                FS.HISFC.Models.Fee.Item.Undrug myobj = order.Item as FS.HISFC.Models.Fee.Item.Undrug;
                feeitemlist.Item.IsNeedConfirm = myobj.IsNeedConfirm;//下面的代码不太懂,不知道这么修改是否正确
                //if(myobj.ConfirmFlag == FS.HISFC.Models.Fee.ConfirmState.All////????????????
                //    ||myobj.ConfirmFlag==FS.HISFC.Models.Fee.ConfirmState.Outpatient)
                //{
                //    feeitemlist.IsConfirmed = true;
                //}
                //else
                //{
                //    feeitemlist.IsConfirmed = false;
                //}
                feeitemlist.Item.PackQty = 1;//包装数量
            }

            //feeitemlist.Item.IsPharmacy = order.Item.IsPharmacy;//是否药品
            feeitemlist.Item.ItemType = order.Item.ItemType;//是否药品
            //if(order.Item.IsPharmacy)//药品
            if (order.Item.ItemType == EnumItemType.Drug)//药品
            {
                feeitemlist.Item.Specs = order.Item.Specs;
            }
            feeitemlist.IsUrgent = order.IsEmergency;//是否加急
            feeitemlist.Order.Sample = order.Sample;//样本信息
            feeitemlist.Memo = order.Memo;//备注
            feeitemlist.Item.MinFee = order.Item.MinFee;//最小费用
            feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//收费状态
            feeitemlist.Item.Price = order.Item.Price;//价格

            feeitemlist.Item.PriceUnit = order.Item.PriceUnit;//价格单位
            feeitemlist.Item.Qty = order.Qty;//数量
            ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.SeeDate = order.RegTime;//登记日期
            ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.Templet.Dept = order.ReciptDept;//登记科室
            feeitemlist.Item.SysClass = order.Item.SysClass;//系统类别
            feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//交易类型
            feeitemlist.Order.Usage = order.Usage;//用法

            if (order.ReciptNO == "")
            {
                feeitemlist.RecipeNO = reciptNo;//处方号
                feeitemlist.SequenceNO = seqNo;//处方内流水号

                return CacheManager.FeeIntegrate.InsertFeeItemList(feeitemlist);
            }
            else
            {
                feeitemlist.RecipeNO = order.ReciptNO;
                feeitemlist.SequenceNO = order.SequenceNO;
                int i = -1;
                i = CacheManager.FeeIntegrate.UpdateFeeItemList(feeitemlist);//更新
                if (i == -1)
                    return -1;
                else if (i == 0)
                    return CacheManager.FeeIntegrate.InsertFeeItemList(feeitemlist);//插入
                else
                    return i;
            }
        }

        /// <summary>
        /// 插入费用档
        /// </summary>
        /// <param name="fee"></param>
        /// <param name="feeitem"></param>
        /// <param name="dtNow"></param>
        /// <returns></returns>
        public static int SaveToFee(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitem, DateTime dtNow)
        {

            int i = -1;//临时变量
            i = CacheManager.FeeIntegrate.UpdateFeeItemList(feeitem);//更新
            if (i == -1)
                i = -1;
            else if (i == 0)
                i = CacheManager.FeeIntegrate.InsertFeeItemList(feeitem);//插入
            return i;
        }

        /// <summary>
        /// 将复合项目拆分成明细
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static ArrayList ChangeZtToSingle(FS.HISFC.Models.Order.OutPatient.Order order, FS.HISFC.Models.Registration.Register reg, FS.HISFC.Models.Base.PactInfo pactInfo)
        {
            ArrayList alZt = CacheManager.InterMgr.QueryUndrugPackageDetailByCode(order.Item.ID);

            if (alZt == null)
            {
                MessageBox.Show("查找复合项目" + order.Item.Name + "失败!", "提示");
                return null;
            }

            ArrayList alOrder = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Item.Undrug info in alZt)
            {
                FS.HISFC.Models.Fee.Item.Undrug item = CacheManager.FeeIntegrate.GetItem(info.ID);

                if (item == null)
                {
                    MessageBox.Show("查找复合项目明细" + info.ID + "失败!", "提示");
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
                    MessageBox.Show("获取发票信息错误！\r\n" + CacheManager.FeeIntegrate.Err);
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
                str += keys.ToString() + ":" + FS.FrameWork.Function.NConvert.ToDecimal(hs[keys]).ToString("F4").TrimEnd('0').TrimEnd('.') + "元 ";
            }
            return str;
        }

        /// <summary>
        /// 将医嘱实体转成费用实体
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
                feeitemlist.Patient.ID = order.Patient.ID;//门诊流水号
                feeitemlist.Patient.PID.CardNO = order.Patient.PID.CardNO;//门诊卡号 

                try
                {
                    //houwb 医保方自费方问题 {A1FE7734-267C-43f1-A824-BF73B482E0B2}
                    feeitemlist.Order.Patient.Pact.ID = order.Patient.Pact.ID;
                    feeitemlist.Order.Patient.Pact.PayKind.ID = order.Patient.Pact.PayKind.ID;
                    //end houwb
                }
                catch { }

                feeitemlist.Order.ID = order.ID;//医嘱流水号
                feeitemlist.Order.SortID = order.SortID;
                feeitemlist.Order.SubCombNO = order.SubCombNO;

                feeitemlist.ChargeOper.ID = FS.FrameWork.Management.Connection.Operator.ID;
                feeitemlist.Order.CheckPartRecord = order.CheckPartRecord;//检体 
                feeitemlist.Order.Combo.ID = order.Combo.ID;//组合号

                //FS.HISFC.Models.Fee.Item.Undrug unDrugItem = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
                //if (unDrugItem.UnitFlag == "1")
                //{
                //}

                if (order.Unit == "[复合项]")
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
                    feeitemlist.ExecOper.Dept.ID = order.StockDept.Clone().ID;//传扣库科室
                    feeitemlist.ExecOper.Dept.Name = order.StockDept.Clone().Name;
                }
                else
                {
                    feeitemlist.ExecOper.Dept.ID = order.ExeDept.Clone().ID;
                    feeitemlist.ExecOper.Dept.Name = order.ExeDept.Clone().Name;
                }
                feeitemlist.InjectCount = order.InjectCount;//院内次数
                
                decimal orgPrice = 0;

                if (!string.Equals(order.Item.ID, "999")) //自备物品不查价格
                {
                    
                    //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                    // isDirectSIFEE 参数影响的合同单位
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
                //自批价项目
                ////if (order.Item.Price == 0)
                ////{
                ////    order.Item.Price = order.Item.Price;
                ////}
                //by zuowy 根据收费是否是最小单位 确定收费 改时慎重
                //if (order.Item.IsPharmacy)
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    feeitemlist.Item.SpecialFlag = order.Item.SpecialFlag;

                    if (order.MinunitFlag == "")//user03为空,说明不知道开立的什么单位 默认为最小单位
                    {
                        order.MinunitFlag = "1";//默认
                    }
                    if (order.MinunitFlag != "1")//开立最小单位 !=((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit)
                    {
                        feeitemlist.Item.Qty = order.Item.PackQty * order.Qty;
                        order.FT.OwnCost = order.Qty * order.Item.Price;

                        order.Item.PriceUnit = order.Unit;
                        feeitemlist.FeePack = "1";//开立单位 1:包装单位 0:最小单位
                    }
                    else
                    {
                        if (order.Item.PackQty == 0)
                        {
                            order.Item.PackQty = 1;
                        }
                        order.FT.OwnCost = order.Qty * order.Item.Price / order.Item.PackQty;

                        order.Item.PriceUnit = order.Unit;
                        feeitemlist.FeePack = "0";//开立单位 1:包装单位 0:最小单位
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

                feeitemlist.Days = order.HerbalQty;//草药付数
                feeitemlist.RecipeOper.Dept = order.ReciptDept;//开方科室信息
                feeitemlist.RecipeOper.Name = order.ReciptDoctor.Name;//开方医生信息
                feeitemlist.RecipeOper.ID = order.ReciptDoctor.ID;
                feeitemlist.Order.DoseUnit = order.DoseUnit;//用量单位
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
                        feeitemlist.Order.DoseOnce = order.DoseOnce;//每次用量
                    }
                }
                feeitemlist.Order.Frequency = order.Frequency.Clone();//频次信息

                feeitemlist.Order.Combo.IsMainDrug = order.Combo.IsMainDrug;//是否主药

                //if (order.Item.IsPharmacy)//是否药品
                if (order.Item.ItemType == EnumItemType.Drug)//是否药品
                {
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).BaseDose = ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose;//基本计量
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).Quality = ((FS.HISFC.Models.Pharmacy.Item)order.Item).Quality;//药品性质
                    ((FS.HISFC.Models.Pharmacy.Item)feeitemlist.Item).DosageForm = ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm;//剂型

                    feeitemlist.IsConfirmed = false;//是否终端确认
                    feeitemlist.Item.PackQty = order.Item.PackQty;//包装数量
                }
                else
                {
                    if (order.ReTidyInfo != "SUBTBL")
                    {
                        feeitemlist.IsConfirmed = false;
                        feeitemlist.Item.PackQty = 1;//包装数量
                    }
                    else//附材中的复合项目
                    {
                        feeitemlist.IsConfirmed = false;//FS.neHISFC.Components.Function.NConvert.ToBoolean(order.Mark2);
                        feeitemlist.Item.PackQty = 1;
                    }
                }

                //feeitemlist.Order.Item.IsPharmacy = order.Item.IsPharmacy;//是否药品
                feeitemlist.Order.Item.ItemType = order.Item.ItemType;//是否药品
                //if (order.Item.IsPharmacy)//药品
                if (order.Item.ItemType == EnumItemType.Drug)//药品
                {
                    feeitemlist.Item.Specs = order.Item.Specs;
                }
                feeitemlist.IsUrgent = order.IsEmergency;//是否加急
                feeitemlist.Order.Sample = order.Sample;//样本信息
                feeitemlist.Memo = order.Memo;//备注
                feeitemlist.Item.MinFee = order.Item.MinFee;//最小费用
                feeitemlist.PayType = FS.HISFC.Models.Base.PayTypes.Charged;//划价状态
                feeitemlist.Item.Price = order.Item.Price;//价格
                feeitemlist.OrgPrice = order.Item.Price;
                feeitemlist.Item.PriceUnit = order.Item.PriceUnit;//价格单位
                if (order.Item.SysClass.ID.ToString() == "PCC" && order.HerbalQty > 0)
                {

                }
                order.FT.TotCost = order.FT.TotCost;
                order.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
                order.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(order.FT.TotCost, 2);
               
                feeitemlist.FT = Round(order, 2);//取两位
                

                //{B9303CFE-755D-4585-B5EE-8C1901F79450} 用药品超标金额保存原来的总费用
                if (feeitemlist.Item.ItemType == EnumItemType.Drug)
                {
                    if (order.MinunitFlag != "1")//开立最小单位
                    {
                        feeitemlist.FT.ExcessCost = order.Qty * ((FS.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice;
                    }
                    else
                    {
                        feeitemlist.FT.ExcessCost = order.Qty * ((FS.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice / order.Item.PackQty;
                        feeitemlist.FT.ExcessCost = FS.FrameWork.Public.String.FormatNumber(feeitemlist.FT.ExcessCost, 2);
                    }
                }
                ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.SeeDate = order.RegTime;//登记日期
                ((FS.HISFC.Models.Registration.Register)feeitemlist.Patient).DoctorInfo.Templet.Dept = order.ReciptDept;//登记科室
                feeitemlist.Item.SysClass = order.Item.SysClass;//系统类别
                feeitemlist.TransType = FS.HISFC.Models.Base.TransTypes.Positive;//交易类型
                feeitemlist.Order.Usage = order.Usage;//用法
                feeitemlist.RecipeSequence = order.ReciptSequence;//收费序列
                feeitemlist.RecipeNO = order.ReciptNO;//处方号
                feeitemlist.SequenceNO = order.SequenceNO;//处方流水号

                feeitemlist.IsExceeded = order.IsExceeded;//是否超过

                feeitemlist.ChargeOper.OperTime = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
                if (feeitemlist.Item.IsMaterial)
                {
                    //附材的费用来源为收费
                    feeitemlist.FTSource = "0";
                }
                else
                {
                    feeitemlist.FTSource = "1";//来自医嘱
                }

                if (order.IsSubtbl)
                {
                    feeitemlist.Item.IsMaterial = true;//是附材
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
        /// 转换成带00的字符串
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
        /// 检查收费项目
        /// </summary>
        /// <param name="item"></param>
        public static void CheckFeeItemList(FS.HISFC.Models.Fee.Outpatient.FeeItemList item)
        {
            if (item.UndrugComb.Package.Name == "[复合项]")
            {
                item.IsGroup = true;
            }
            item.FT.OwnCost = item.Item.Qty * item.Item.Price;
        }

        /// <summary>
        /// 为费用取整
        /// </summary>
        /// <param name="order"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static FS.HISFC.Models.Base.FT Round(FS.HISFC.Models.Order.OutPatient.Order order, int i)
        {
            FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();
            //为NULL返回新实体
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
        /// 是否相同用法
        /// </summary>
        /// <param name="usageID1"></param>
        /// <param name="usageID2"></param>
        /// <returns></returns>
        public static bool IsSameUsage(string usageID1, string usageID2)
        {
            return Components.Order.Classes.Function.IsSameUsage(usageID1, usageID2);
        }

        /// <summary>
        /// 是否忽略系统类别 允许西药、成药组合、分在同一处方
        /// </summary>
        static bool isDecSysClassWhenGetRecipeNO = false;

        /// <summary>
        /// 判断组合约束条件
        /// </summary>
        /// <param name="orderFrom"></param>
        /// <param name="orderTo"></param>
        /// <param name="isNew">是不是新医嘱？ 新医嘱部分信息可以不判断</param>
        /// <param name="isIgnore">忽略的话，允许用法、频次、院注不判断是否相同</param>
        /// <returns></returns>
        public static int ValidComboOrder(FS.HISFC.Models.Order.OutPatient.Order orderFrom, FS.HISFC.Models.Order.OutPatient.Order orderTo, bool isNew, bool isIgnore)
        {
            if (orderFrom.IsSubtbl || orderTo.IsSubtbl)
            {
                return 1;
            }

            isDecSysClassWhenGetRecipeNO = CacheManager.ContrlManager.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE, false, false);

            //根据分方设置，允许西药和成药组合使用
            if (isDecSysClassWhenGetRecipeNO)
            {
                if ("PCZ,P".Contains(orderFrom.Item.SysClass.ID.ToString()) &&
                    "PCZ,P".Contains(orderTo.Item.SysClass.ID.ToString()))
                {
                    //西药和成药允许组合
                }
                else
                {
                    if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                    {
                        MessageBox.Show("系统类别不同，不可以组合用！");
                        return -1;
                    }
                }
            }
            else
            {
                if (orderFrom.Item.SysClass.ID.ToString() != orderTo.Item.SysClass.ID.ToString())
                {
                    MessageBox.Show("系统类别不同，不可以组合用！");
                    return -1;
                }
            }

            if (!isIgnore)
            {
                if (isNew && !string.IsNullOrEmpty(orderFrom.Frequency.ID) && orderFrom.Frequency.ID != "PRN")
                {
                    if (orderFrom.Frequency.ID != orderTo.Frequency.ID)
                    {
                        MessageBox.Show("频次不同，不可以组合用！");
                        return -1;
                    }
                }

                if (isNew && orderFrom.InjectCount > 0)
                {
                    if (orderFrom.InjectCount != orderTo.InjectCount)
                    {
                        MessageBox.Show("院注次数不同，不可以组合用！");
                        return -1;
                    }
                }
            }

            if (orderFrom.ExeDept.ID != orderTo.ExeDept.ID)
            {
                MessageBox.Show("执行科室不同，不能组合使用!", "提示");
                return -1;
            }

            if (orderFrom.Item.ItemType == EnumItemType.Drug)		//只对药品判断用法是否相同
            {
                if (isNew && !string.IsNullOrEmpty(orderFrom.Usage.ID))
                {
                    if (!isIgnore)
                    {
                        if (orderFrom.Item.SysClass.ID.ToString() != "PCC")
                        {
                            #region 用法判断
                            if (!IsSameUsage(orderFrom.Usage.ID, orderTo.Usage.ID))
                            {
                                MessageBox.Show("用法不同，不可以进行组合！");
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
                            MessageBox.Show("草药付数不同，不可以组合用！");
                            return -1;
                        }
                    }
                }
            }
            else
            {
                if (orderFrom.Item.SysClass.ID.ToString() == "UL")//检验
                {
                    if (isNew && orderFrom.Qty > 0)
                    {
                        if (orderFrom.Qty != orderTo.Qty)
                        {
                            MessageBox.Show("检验数量不同，不可以组合用！");
                            return -1;
                        }
                    }

                    if (isNew && string.IsNullOrEmpty(orderFrom.Sample.Name))
                    {
                        if (orderFrom.Sample.Name != orderTo.Sample.Name)
                        {
                            MessageBox.Show("检验样本不同，不可以组合用！");
                            return -1;
                        }
                    }
                }
            }
            return 1;
        }

        ///// <summary>
        ///// 获得是否可以开库存为零的药品
        ///// </summary>
        ///// <returns></returns>
        //public static int GetIsOrderCanNoStock()
        //{
        //    return CacheManager.ContrlManager.GetControlParam<int>("200001", false, 0);
        //}

        /// <summary>
        /// 检查库存
        /// </summary>
        /// <param name="iCheck"></param>
        /// <param name="itemID"></param>
        /// <param name="itemName"></param>
        /// <param name="deptCode"></param>
        /// <param name="qty"></param>
        /// <param name="sendType">发送类型 A:全部、O:门诊、I:住院</param>
        /// <returns></returns>
        public static bool CheckPharmercyItemStock(int iCheck, string itemID, string itemName, string deptCode, decimal qty, string sendType)
        {
            FS.HISFC.Models.Pharmacy.Storage item = null;
            switch (iCheck)
            {
                case 0:
                    //houwb 2011-5-30 增加发送类型判断
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
                        if (MessageBox.Show("药品【" + itemName + "】的库存不够！是否继续执行！", "提示库存不足", MessageBoxButtons.OKCancel) == DialogResult.OK)
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
        /// 门诊是否使用预扣库存 P00320
        /// </summary>
        static int isUseOutDrugPreOut = -1;

        #region 即将作废 

        /// <summary>
        /// 获得非药品信息
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
            if (order.Unit == "[复合项]")
                return 0;//如果是复合项目不变
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
            order.Item.SysClass = item.SysClass.Clone();//付给系统类别
            return 0;
        }
        #endregion

        #region 整理新方法，增加缺药等判断

        /// <summary>
        /// 检查缺药、停用
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
        /// 检查缺药、停用
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
        /// 填充医嘱最新药品相关信息 并判断有效性
        /// </summary>
        /// <param name="stockDept">药品扣库科室</param>
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
                //MessageBox.Show(errInfo, "错误", MessageBoxButtons.OK);
                return -1;
            }

            order.StockDept = storage.StockDept;

            order.Item.MinFee = item.MinFee;
            //{B9303CFE-755D-4585-B5EE-8C1901F79450} 保存原来的购入价
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).ChildPrice = item.Price;
            ((FS.HISFC.Models.Pharmacy.Item)order.Item).SpecialPrice = item.SpecialPrice;
            //decimal orgPrice = 0;
            //order.Item.Price = CacheManager.FeeIntegrate.GetPrice(order.Item.ID, regObj, 0, item.Price, item.ChildPrice, item.SpecialPrice, 0, ref orgPrice);

            order.Item.Price = item.Price;

            order.Item.Name = item.Name;
            order.Item.UserCode = item.UserCode;
            order.Item.SysClass = item.SysClass.Clone();//付给系统类别
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
        /// 填充医嘱最新非药品相关信息 并判断有效性
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
                MessageBox.Show(errInfo, "错误", MessageBoxButtons.OK);
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
            order.Item.SysClass = item.SysClass.Clone();//付给系统类别
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
        /// 计算院注次数
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int CalculateInjNum(FS.HISFC.Models.Order.OutPatient.Order order, ref string errInfo)
        {
            try
            {
                if (order == null)
                {
                    errInfo = "医嘱信息为空 null值！";
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

                //院注次数
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
        /// 获取数量限制范围
        /// </summary>
        /// <param name="outOrder"></param>
        /// <param name="type">0 下限，其他 上限</param>
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
        /// 判断廉价药总量
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

            #region 廉价药总量判断
            if (outOrder != null)
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID);
                //廉价药标识
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
                        errInfo = "开立的廉价药品[" + outOrder.Item.Name + "]，总量不能小于" + GetLimitQty(outOrder, phaItem, "0").ToString() + phaItem.PackUnit + "(" + (GetLimitQty(outOrder, phaItem, "0") * phaItem.PackQty).ToString() + phaItem.MinUnit + ")";
                    }
                    else if (GetLimitQty(outOrder, phaItem, "0") != 0
                        && qty > GetLimitQty(outOrder, phaItem, "1"))
                    {
                        errInfo = "开立的廉价药品[" + outOrder.Item.Name + "]，总量不能大于" + GetLimitQty(outOrder, phaItem, "1").ToString() + phaItem.PackUnit + "(" + (GetLimitQty(outOrder, phaItem, "1") * phaItem.PackQty).ToString() + phaItem.MinUnit + ")";
                    }
                    if (!string.IsNullOrEmpty(errInfo))
                    {
                        //MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //SetQtyValue(outOrder.Qty.ToString());
                        //同时修改数量和单位的时候，没法修改，所以放到最后保存的时候再判断吧
                        return -1;
                    }
                }
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 根据药品拆分属性计算总数量和总单位
        /// </summary>
        /// <param name="itm">药品项目</param>
        /// <param name="DoseOnce">每次剂量</param>
        /// <param name="BaseDose">基本剂量</param>
        /// <param name="Frequence">频次</param>
        /// <param name="Days">天数</param>
        /// <param name="Qty">数量</param>
        /// <param name="unit">返回的单位</param>
        /// <param name="unitFlag">是否最小单位：1 最小单位，0 包装单位</param>
        [Obsolete("作废,移植到FS.HISFC.Components.Order.Classes.Function里面ReComputeQty方法", true)]
        public static int ReComputeQtyBase(FS.HISFC.Models.Pharmacy.Item itemObj, decimal doseOnce, decimal baseDose, decimal frequence, decimal days, ref decimal qty, ref string unit, ref string unitFlag, ref string errInfo)
        {
            try
            {
                //草药和频次无关
                if (itemObj.SysClass.ID.ToString() == "PCC")
                {
                    frequence = 1;
                }

                //0 最小单位总量取整" 数据库值 0
                //包装单位总量取整" 数据库值 1  口服特别是中成药、妇科用药较多
                //最小单位每次取整" 数据库值 2  针剂较多这样
                //包装单位每次取整" 数据库值 3  几乎没有用   
                switch (itemObj.SplitType)
                {
                    case "0":
                        //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                        //草药的总量不取整，开出1.5g就是1.5g
                        //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                        if (itemObj.SysClass.ID.ToString() == "PCC")
                        {
                            qty = Math.Round(doseOnce * frequence * days / baseDose, 2);
                            unit = itemObj.MinUnit;
                            unitFlag = "1";//最小单位
                        }
                        else
                        {
                            //西药允许输入分数，对于每次用量2/3片的，
                            // 由于除不尽，总量这里计算出来截取一下 再取整 houwb
                            qty = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce * frequence * days / baseDose, 3)));
                            unit = itemObj.MinUnit;
                            unitFlag = "1";
                        }
                        break;
                    case "1":
                        //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                        //草药的总量不取整，开出1.5g就是1.5g
                        //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                        if (itemObj.SysClass.ID.ToString() == "PCC")
                        {
                            qty = Math.Round((doseOnce * frequence * days / baseDose) / itemObj.PackQty, 2);
                            unit = itemObj.PackUnit;
                            unitFlag = "0";//包装单位
                        }
                        else
                        {
                            qty = Math.Ceiling((doseOnce * frequence * days / baseDose) / itemObj.PackQty);
                            unit = itemObj.PackUnit;
                            unitFlag = "0";
                        }
                        break;
                    case "2":
                        //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                        //草药的总量不取整，开出1.5g就是1.5g
                        //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                        if (itemObj.SysClass.ID.ToString() == "PCC")
                        {
                            qty = Math.Round(doseOnce * frequence * days / baseDose, 2);
                            unit = itemObj.MinUnit;
                            unitFlag = "1";//最小单位
                        }
                        else
                        {
                            qty = Math.Ceiling(Math.Round(Math.Ceiling(doseOnce / baseDose) * frequence * days, 6));
                            unit = itemObj.MinUnit;
                            unitFlag = "1";
                        }
                        break;
                    case "3":
                        //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                        //草药的总量不取整，开出1.5g就是1.5g
                        //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                        if (itemObj.SysClass.ID.ToString() == "PCC")
                        {
                            qty = Math.Round((doseOnce / baseDose * frequence * days) / itemObj.PackQty, 2);
                            //unit = itemObj.MinUnit;
                            unit = itemObj.PackUnit;
                            unitFlag = "0";//包装单位
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
        /// 重新计算总量
        /// {37C70D4B-53A8-46a4-B9CB-FF4583E9DFAE}
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [Obsolete("作废,移植到FS.HISFC.Components.Order.Classes.Function里面ReComputeQty方法", true)]
        public static int ReComputeQty(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            //天数没有填时，不计算
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

                    //草药计算方式不一样
                    if (order.Item.ItemType == EnumItemType.Drug)
                    {
                        HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                        if (phaItem == null)
                        {
                            MessageBox.Show("查找药品项目失败");
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
        /// 显示医嘱信息
        /// </summary>
        /// <param name="sender"></param>
        public static void ShowOrder(object sender, ArrayList alOrder)
        {
            ShowOrder(sender, alOrder, 0);
        }

        /// <summary>
        /// 显示医嘱信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="alOrder"></param>
        /// <param name="type"></param>
        public static void ShowOrder(object sender, ArrayList alOrder, int type)
        {
            try
            {
                #region 设置dataSet

                #region 变量声明及初始化
                //定义传出DataSet
                DataSet myDataSet = new DataSet();
                myDataSet.EnforceConstraints = false;//是否遵循约束规则
                //定义类型
                System.Type dtStr = System.Type.GetType("System.String");
                System.Type dtBool = System.Type.GetType("System.Boolean");
                System.Type dtInt = System.Type.GetType("System.Int32");
                //定义表********************************************************
                //Main Table
                DataTable dtMain = new DataTable();
                dtMain = myDataSet.Tables.Add("TableMain");

                dtMain.Columns.AddRange(new DataColumn[]{  new DataColumn("ID", dtStr),new DataColumn("组合号", dtStr), new DataColumn("医嘱名称", dtStr),new DataColumn("规格", dtStr),
                                                            new DataColumn("组合", dtStr),new DataColumn("间隔时间", dtStr),new DataColumn("每次剂量", dtStr),
                                                            new DataColumn("频次", dtStr),new DataColumn("数量", dtStr),new DataColumn("付数", dtStr),
                                                            new DataColumn("用法", dtStr),new DataColumn("医嘱类型", dtStr),new DataColumn("加急", dtBool),
                                                            new DataColumn("开始时间", dtStr),new DataColumn("开立时间", dtStr),new DataColumn("开立医生", dtStr),
                                                            new DataColumn("执行科室", dtStr),new DataColumn("停止时间", dtStr),new DataColumn("停止医生", dtStr),
                                                            new DataColumn("备注", dtStr),new DataColumn("顺序号", dtStr)});

                //FS.HISFC.BizLogic..OrderType orderType = new FS.HISFC.BizLogic.Manager.OrderType();

                FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(CacheManager.InterMgr.QueryOrderTypeList());
                #endregion

                string beginDate = "", endDate = "", moDate = "";
                ArrayList alTemp = new ArrayList();

                for (int i = 0; i < alOrder.Count; i++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order o = alOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;
                    FS.HISFC.Models.Base.Item tempItem = null;

                    #region 更新项目信息
                    if (o.Item == null || o.Item.ID == "")
                    {
                        if (o.ID == "999")//自备项目
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
                        else if (o.ID.Substring(0, 1) == "F")//非药品
                        {
                            #region 非药品
                            tempItem = CacheManager.FeeIntegrate.GetItem(o.Item.ID);
                            if (tempItem == null || tempItem.ID == "")
                            {
                                MessageBox.Show("项目" + o.Name + "已经停用！!", "提示");

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
                        else if (o.ID.Substring(0, 1) == "Y")//药品
                        {
                            #region 药品
                            ////FS.HISFC.Models.RADT.Person p = pManager.Operator as FS.HISFC.Models.RADT.Person;
                            ////if (p == null) return;
                            ////tempItem = pManager.GetItemForInpatient(p.Dept.ID, o.ID);
                            tempItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(o.Item.ID);
                            if (tempItem == null || tempItem.ID == "")
                            {
                                MessageBox.Show("项目" + o.Name + "已经停用！!", "提示");

                                o.ExtendFlag2 = "N";
                            }
                            else
                            {
                                //药品执行科室为空

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
                        else if (o.Unit == "[复合项]")//复合项目
                        {
                            #region 复合项
                            FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
                            FS.HISFC.Models.Fee.Item.Undrug zt = CacheManager.FeeIntegrate.GetItem(o.ID);
                            if (zt == null)
                            {
                                MessageBox.Show("复合项目:" + o.Name + "已经停用或者删除,不能调用!", "提示");

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

                    #region 显示医嘱
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
                            dtMain.Rows.Add(new Object[] {  o.ID,o.Combo.ID,o.Item.Price.ToString()+"元/"+o.Item.Name,o.Item.Specs,
                                                             "",o.User03,o.DoseOnce.ToString()+item.DoseUnit ,
                                                             o.Frequency.ID,o.Qty.ToString()+o.Unit,o.HerbalQty,o.Usage.Name,
                                                             /*o.OrderType.Name*/"门诊",o.IsEmergency,beginDate,moDate,o.ReciptDoctor.Name,o.ExeDept.Name,endDate,
                                                             o.DCOper.Name,o.Memo,o.SortID});

                        }
                        else if (o.Item.ItemType == EnumItemType.UnDrug)
                        {
                            if (o.Unit == "[复合项]")
                            {
                                o.Item.Price = Order.OutPatient.Classes.Function.GetUndrugZtPrice(o.Item.ID);
                            }
                            dtMain.Rows.Add(new Object[] { o.ID,o.Combo.ID,o.Item.Price.ToString()+"元/"+o.Item.Name,o.Item.Specs,
                                                             "",o.User03,"" ,
                                                             o.Frequency.ID,o.Qty.ToString()+o.Unit,"","",
                                                             /*o.OrderType.Name*/"门诊",o.IsEmergency,beginDate,moDate,o.ReciptDoctor.Name,
                                                             o.ExeDept.Name,endDate,
                                                             o.DCOper.Name,o.Memo,o.SortID});

                        }
                        else
                        {
                            dtMain.Rows.Add(new Object[] { o.ID,o.Combo.ID,o.Item.Name,o.Item.Specs,
                                                             "",o.User03,o.DoseOnce.ToString()+o.DoseUnit,
                                                             o.Frequency.ID,o.Qty.ToString()+o.Unit,o.HerbalQty,o.Usage.Name,
                                                             /*o.OrderType.Name*/"门诊",o.IsEmergency,beginDate,moDate,o.ReciptDoctor.Name,
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
                        #region 设置列
                        o.Columns[0].Visible = false;
                        o.Columns[1].Visible = false;
                        //2 ("医嘱名称", dtStr),3("规格", dtStr),4 组合,5间隔时间,6("每次剂量", dtStr),
                        //7("频次", dtStr),8("数量", dtStr),9("付数", dtStr),
                        //10("用法", dtStr),11("医嘱类型", dtStr),12("加急", dtBool),
                        //13("开始时间", dtStr),14("开立时间", dtStr),15("开立医生", dtStr),
                        //16("执行科室", dtStr),17("停止时间", dtStr),18("停止医生", dtStr),
                        //19("备注", dtStr),20("顺序号", dtStr)});
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
                        if (type == 1)//组套
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
        /// 根据复合项编码获得其价格
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
        /// 根据复合项编码获得其样本
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
        /// 获得组套价格
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
        /// 获得项目价格
        /// </summary>
        /// <param name="order">医嘱实体</param>
        /// <param name="item">医嘱项目</param>
        /// <param name="patintInfo">患者实体</param>
        /// <param name="pactInfo">合同单位信息</param>
        /// <param name="isNewItem">当前医嘱项目是否是从数据库重取</param>
        /// <returns></returns>
        [Obsolete("作废，使用integrate中的函数", true)]
        public static decimal GetPrice(FS.HISFC.Models.Order.OutPatient.Order order, FS.HISFC.Models.Base.Item itemNew, FS.HISFC.Models.RADT.Patient patintInfo, FS.HISFC.Models.Base.PactInfo pactInfo, bool isNewItem)
        {
            if (order.Item.ID == "999")
            {
                return 0;
            }

            FS.FrameWork.Management.DataBaseManger db = new FS.FrameWork.Management.DataBaseManger();

            DateTime nowDate = db.GetDateTimeFromSysDateTime();

            int age = (int)((new TimeSpan(nowDate.Ticks - patintInfo.Birthday.Ticks)).TotalDays / 365);
            //{B9303CFE-755D-4585-B5EE-8C1901F79450}增加获取购入价
            string priceForm = pactInfo.PriceForm;

            if (order.Unit != "[复合项]")
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
                            MessageBox.Show("查找项目" + order.Item.Name + "失败!", "提示");
                            return order.Item.Price;
                        }
                        return CacheManager.FeeIntegrate.GetPrice(priceForm, age, item.Price, item.ChildPrice, item.SpecialPrice, item.Price);

                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);

                        if (item == null)
                        {
                            MessageBox.Show("查找项目" + order.Item.Name + "失败!", "提示");
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
                    MessageBox.Show("查找复合项目" + order.Item.Name + "失败!", "提示");
                    return order.Item.Price;
                }

                decimal price = 0m;

                foreach (FS.HISFC.Models.Fee.Item.UndrugComb info in alZt)
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = CacheManager.FeeIntegrate.GetItem(info.ID);

                    if (item == null)
                    {
                        MessageBox.Show("查找复合项目明细" + info.ID + "失败!", "提示");
                        return order.Item.Price;
                    }

                    //价格*数量
                    price += CacheManager.FeeIntegrate.GetPrice(priceForm, age, item.Price, item.ChildPrice, item.SpecialPrice, item.Price);
                }

                return price;
            }
        }

        /// <summary>
        /// 取医嘱流水号
        /// </summary>
        /// <returns></returns>
        public static string GetNewOrderID(ref string errInfo)
        {
            CacheManager.OutOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            string rtn = "";
            rtn = CacheManager.InOrderMgr.GetNewOrderID();
            if (rtn == null || rtn == "")
            {
                errInfo = "错误获得医嘱流水号！";
            }
            else
            {
                return rtn;
            }
            return "";
        }

        /// <summary>
        /// 新建XML
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

            XmlElement e = myXml.AddXmlNode(doc, root, "延长队列", "");
            myXml.AddNodeAttibute(e, "ExtendTime", extendTime);

            e = myXml.AddXmlNode(doc, root, "保存时间", "");
            myXml.AddNodeAttibute(e, "LastOperTime", opertime);

            try
            {
                StreamWriter sr = new StreamWriter(fileName, false, System.Text.Encoding.Default);
                string cleandown = doc.OuterXml;
                sr.Write(cleandown);
                sr.Close();
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show("无法保存！" + ex.Message); }

            return 1;
        }

        /// <summary>
        /// 获得所有附材信息
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
        /// 是否院注用法
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
        /// 存放复制拷贝的门诊处方
        /// </summary>
        public static ArrayList AlCopyOrders = new ArrayList();

        #region 通用控制参数

        /// <summary>
        /// LIS复合项目是否允许从组套中勾选明细项目（目前广医肿瘤有用到）
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
                //检查项目不用开立明细，按照组套名称进行开立{43E10122-9C4D-4e3d-AA1A-1C10AC68D20B}
                return false;
                //return true;
            }
            return false;
        }

        public static Hashtable hsControlParam = null;

        /// <summary>
        /// 批量查询控制参数
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

                //是否允许修改每次开立保存时的处方合同单位信息
                //houwb 医保方自费方问题 {A1FE7734-267C-43f1-A824-BF73B482E0B2}
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

                //检验复合项目是否允许勾选明细
                hsControlParam.Add("HNMZ80", "0");

                //是否自动带出挂号费和诊金、差额等
                // 0 不带出；1 自动带出；2 只补收诊金和挂号费；3 只补收差额费
                hsControlParam.Add("HNMZ42", "1");
                //是否提示同一次就诊有开立重复项目或未执行项目
                hsControlParam.Add("HNMZ97", "0");
                //是否提示已执行医嘱不可退费
                hsControlParam.Add("HNMZ95", "0");
                if (CacheManager.ContrlManager.GetBatchControlParam(ref hsControlParam) == -1)
                {
                    MessageBox.Show("查询控制参数出错！\r\n" + CacheManager.ContrlManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 是否启用门诊分诊
        /// </summary>
        private static int isUseNurseArray = -1;

        /// <summary>
        /// 是否启用门诊分诊 1 启用；其他 不启用
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
        /// 挂号级别帮助类
        /// </summary>
        private static FS.FrameWork.Public.ObjectHelper regLevelHelper = null;

        /// <summary>
        /// 挂号级别帮助类
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
                        MessageBox.Show("查询挂号级别出错！\r\n" + CacheManager.RegInterMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return Function.regLevelHelper;
            }
        }

        #endregion

        #region 无用

        /// <summary>
        /// 去除具有相同设备类型和容器类型的医嘱(未实现)
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
            //    //设备类型和容器类型相同
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
        /// 查找复合要求的限定额度
        /// </summary>
        /// <param name="stats">统计编码</param>
        /// <param name="relations">限定额度关系</param>
        /// <returns>当前显额</returns>
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
        /// 计算公费超标
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

            //// TODO:  添加 ComputePubFee.FS.Common.Clinic.Interface.IComputePubFee.Compute 实现
            //if (rInfo == null || rInfo.ID == "")
            //{
            //    errText = "患者基本信息为空！";
            //    return -1;
            //}

            //if (feeDetails == null)
            //{
            //    errText = "费用明细集合为空！";
            //    return -1;
            //}

            //if (rInfo.Pact == null || rInfo.Pact.ID == "")
            //{
            //    errText = "患者合同单位为空！";
            //    return -1;
            //}


            //FS.HISFC.BizLogic.Fee.FeeCodeStat feeMgr = new FS.HISFC.BizLogic.Fee.FeeCodeStat();

            ////清空总额
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

            //    //rowFindStat --根据最小费用找到对应大类

            //    ArrayList stats = feeMgr.GetGFDYFeeCodeStatByFeeCode(f.Item.MinFee.ID);

            //    //没有找到对应的费用大类说明肯定不存在超标限制
            //    if (stats == null)//if(rowFindStat == null)
            //    {
            //        continue;
            //    }

            //    //获得限定额度关系
            //    FS.FrameWork.Models.NeuObject relation = GetRelation(stats, relations);

            //    //没有找到对应的费用大类限额说明不存在超标限制
            //    if (relation == null)//if(index == -1)
            //    {
            //        continue;
            //    }

            //    //临时存储患者花费总金额
            //    decimal tempTotCost = FS.FrameWork.Function.NConvert.ToDecimal(relation.User03);
            //    //统计大类的限额
            //    decimal limitCost = FS.FrameWork.Function.NConvert.ToDecimal(((FS.HISFC.Models.Base.PactStatRelation)relation).Quota);

            //    //超过限额
            //    if (tempTotCost + f.FT.TotCost > limitCost)
            //    {
            //        if (relation.User01 != "TRUE")
            //        {
            //            MessageBox.Show("患者" + rInfo.Name + "的" + ((FS.HISFC.Models.Base.PactStatRelation)relation).StatClass.Name + "已经超标！察看限额请在患者费用信息处点击", "提示");
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
        /// 获得当前公费插件实例
        /// </summary>
        /// <returns>null 获得实例失败</returns>
        public static object GetPubFeeInstance()
        {
            //公费算法插件路径
            string pubFeeComputeDll = null;
            string errText = "";
            //获得公费插件路径
            ////pubFeeComputeDll = CacheManager.ContrlManager.QueryControlerInfo(FS.Common.Clinic.Class.Const.PUBFEECOMPUTE);
            if (pubFeeComputeDll == null || pubFeeComputeDll == "")
            {
                errText = "没有维护公费算法方案!请维护";
                return null;
            }
            //得到全路径
            pubFeeComputeDll = Application.StartupPath + pubFeeComputeDll;
            Assembly a = null;
            System.Type[] types = null;
            //临时实例
            object objInstance = null;
            try
            {
                //获得当前路径dll的反射信息
                a = Assembly.LoadFrom(pubFeeComputeDll);
                //得到反射获得所有类型.
                types = a.GetTypes();
                foreach (System.Type type in types)
                {
                    //如果符合公费计算接口,那么实例化,并结束循环.
                    if (type.GetInterface("IComputePubFee") != null)
                    {
                        //实例化公费实体.
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
        /// 公费费用计算
        /// </summary>
        /// <param name="pubFeeInstance">公费费用计算插件实例</param>
        /// <param name="r">挂号实体</param>
        /// <param name="feeDetails">费用明细集合</param>
        /// <param name="relations">限额关系</param>
        /// <param name="errText">错误信息</param>
        /// <returns>- 1 失败 0 成功</returns>
        public static int ComputePubFee(object pubFeeInstance, FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails, ArrayList relations, ref string errText)
        {
            if (pubFeeInstance == null)
            {
                errText = "公费算法实例为空!";
                return -1;
            }
            if (pubFeeInstance.GetType().GetInterface("IComputePubFee") == null)
            {
                errText = "公费算法没有实现IComputePubFee接口!";
                return -1;
            }
            int iReturn = 0;//返回值
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

        #region 检查是否是有效挂号记录

        /// <summary>
        /// 获取挂号有效时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetRegValideDate(bool isEmergency)
        {
            //普通门诊当天有效，急诊24小时有效,对应控制参数为200022

            //默认24小时有效
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

        #region 界面翻译

        /// <summary>
        /// 翻译缓存
        /// </summary>
        private static Dictionary<string, string> dictTranslate = null;

        /// <summary>
        /// 获取翻译
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static string GetMsg(string strMsg)
        {
            //判断
            if (dictTranslate == null)
            {
                dictTranslate = new Dictionary<string, string>();
            }

            if (!dictTranslate.ContainsKey(strMsg))
            {
                #region 不存在

                string strTranslate = Language.Msg(strMsg);
                dictTranslate.Add(strMsg, strTranslate);
                return strTranslate;

                #endregion
            }
            else
            {
                #region 存在

                string strTranslate = dictTranslate[strMsg];
                return strTranslate;

                #endregion
            }

        }

        /// <summary>
        /// 界面翻译{038E4663-6430-4cc5-9F00-BD7128DE5C8F}
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
                    if (c.Text.Contains("：") || c.Text.Contains(":"))
                    {
                        endChar = ":";
                    }
                    c.Text = c.Text.Trim('：');
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
        /// 获取项目英文名称
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

            //获取项目英文名称
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
    /// 日志记录
    /// </summary>
    public class LogManager
    {
        public static void Write(string logInfo)
        {
            //return;
            //检查目录是否存在
            if (System.IO.Directory.Exists("./Log/Order/OutOrder") == false)
            {
                System.IO.Directory.CreateDirectory("./Log/Order/OutOrder");
            }

            FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();
            DateTime dtNow = dbMgr.GetDateTimeFromSysDateTime();

            //保存一周的日志
            System.IO.File.Delete("./Log/Order/OutOrder/" + dtNow.AddDays(-7).ToString("yyyyMMdd") + ".LOG");
            string name = dtNow.ToString("yyyyMMdd");
            System.IO.StreamWriter w = new System.IO.StreamWriter("./Log/Order/OutOrder/" + name + ".LOG", true);
            w.WriteLine(dtNow.ToString("yyyy-MM-dd HH:mm:ss") + logInfo);
            w.Flush();
            w.Close();
        }
    }
}