using System;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.Common.OrderSplit
{
    /// <summary>
    /// 本地化取整规则（根据药房单独设置取整规则）
    /// </summary>
    public class OrderSplit : FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit
    {
        #region 变量

        private FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();

        #endregion

        #region IOrderSplit 成员

        private string errInfo;

        public int ComputeOrderQty(FS.HISFC.Models.Order.ExecOrder execOrder, ref string feeFlag, ref decimal feeNum, ref decimal phaNum)
        {
            //住院的只是获取拆分属性而已，具体的拆分算法 在交叉业务层自己处理了
            return 1;

            #region 根据不同配药属性 设置临时变量值


            /* CDSplitType
                * 0、最小单位总量取整 
                * 1、包装单位总量取整 
                * 2、最小单位每次取整
                * 3、包装单位每次取整
                * 4、最小单位可拆分 如果此参数不维护，为空，默认按照此规则处理
                * */

            /*
            
            switch (itemPha.CDSplitType)
            {
                case "":
                //4、最小单位可拆分 
                case "4":
                    feeFlag = "2";//0 不计费 1 根据计费数量feeNum进行计费 2 按原流程进行
                    break;
                //0、最小单位总量取整 
                case "0":
                    if (this.GetQtyByPatientStore(execOrder, itemPha, false, ref feeFlag, ref feeNum, ref isFee, ref phaNum) == -1)
                    {
                        return -1;
                    }
                    break;
                //1、包装单位总量取整 
                case "1":
                    if (this.GetQtyByPatientStore(execOrder, itemPha, true, ref feeFlag, ref feeNum, ref isFee, ref phaNum) == -1)
                    {
                        return -1;
                    }
                    break;
                //2、最小单位每次取整
                case "2":
                    feeFlag = "1";
                    feeNum = (decimal)Math.Ceiling(Math.Round((double)execOrder.Order.DoseOnce / (double)itemPha.BaseDose, 5));
                    phaNum = feeNum;
                    break;
                //3、包装单位每次取整
                case "3":
                    feeFlag = "1";
                    feeNum = (decimal)Math.Ceiling(Math.Round(((double)execOrder.Order.DoseOnce / (double)itemPha.BaseDose) / (double)itemPha.PackQty, 5)) * itemPha.PackQty;
                    phaNum = feeNum;
                    break;
                default:
                    feeFlag = "2";
                    break;
            }
            */
            #endregion
            return 1;
        }

        /// <summary>
        /// 每次计算都查询效率太低了
        /// 现在每次修改每次量、频次等都会重新计算总量，都会重取这个东东
        /// </summary>
        Hashtable hsDrug = new Hashtable();

        FS.HISFC.Models.Pharmacy.Storage storage = null;

        public int ComputeOrderQty(FS.HISFC.Models.Order.Order orderBase)
        {
            #region 默认取整规则
            try
            {

                //草药计算方式不一样
                if (orderBase.Item.ItemType == EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(orderBase.Item.ID);

                    if (phaItem == null)
                    {
                        MessageBox.Show("查找药品项目失败");
                        return -1;
                    }

                    #region 处理数据

                    #region 处理频次
                    decimal frequence = 0;

                    if (phaItem.SysClass.ID.ToString() == "PCC")
                    {
                        frequence = 1;
                    }
                    else
                    {
                        if (orderBase.Frequency.Days[0] == "0" || string.IsNullOrEmpty(orderBase.Frequency.Days[0]))
                        {
                            orderBase.Frequency.Days[0] = "1";
                            frequence = orderBase.Frequency.Times.Length;
                        }
                        else
                        {
                            try
                            {
                                frequence = Math.Round(orderBase.Frequency.Times.Length / FS.FrameWork.Function.NConvert.ToDecimal(orderBase.Frequency.Days[0]), 2);
                            }
                            catch
                            {
                                frequence = orderBase.Frequency.Times.Length;
                            }
                        }
                    }
                    #endregion

                    string err = "";

                    decimal doseOnce = orderBase.DoseOnce;
                    if (orderBase.DoseUnit == phaItem.MinUnit)
                    {
                        doseOnce = orderBase.DoseOnce * phaItem.BaseDose;
                    }
                    #endregion

                    #region 计算取整总量

                    //0 最小单位总量取整" 数据库值 0
                    //1 包装单位总量取整" 数据库值 1  口服特别是中成药、妇科用药较多
                    //2 最小单位每次取整" 数据库值 2  针剂较多这样
                    //3 包装单位每次取整" 数据库值 3  几乎没有用
                    //4 最小单位可拆分 即不处理任何取整


                    string splitType = "4";


                    //增加如果药房维护取整规则，获取取整规则
                    if (hsDrug.Contains(orderBase.Item.ID))
                    {
                        storage = hsDrug[orderBase.Item.ID] as FS.HISFC.Models.Pharmacy.Storage;
                    }
                    else
                    {
                        storage = storageManager.GetStockInfoByDrugCode(orderBase.StockDept.ID, orderBase.Item.ID);
                        hsDrug.Add(orderBase.Item.ID, storage);
                    }
                    if (!string.IsNullOrEmpty(storage.Item.ID))
                    {
                        if (!string.IsNullOrEmpty(storage.SplitType))
                        {
                            phaItem.SplitType = storage.SplitType;
                        }
                        if (!string.IsNullOrEmpty(storage.LZSplitType))
                        {
                            phaItem.LZSplitType = storage.LZSplitType;
                        }
                        if (!string.IsNullOrEmpty(storage.CDSplitType))
                        {
                            phaItem.CDSplitType = storage.CDSplitType;
                        }
                    }

                    if (orderBase.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order))
                    {
                        splitType = phaItem.SplitType;
                    }
                    else
                    {
                        if (((FS.HISFC.Models.Order.Inpatient.Order)orderBase).OrderType.IsDecompose)
                        {
                            return 1;
                        }
                        splitType = phaItem.LZSplitType;
                    }

                    //0 包装单位；1 最小单位
                    string unitFlag = "";

                    //获取执行频次，只能为整数
                    decimal execQty = Math.Ceiling(frequence * orderBase.HerbalQty);

                    switch (splitType)
                    {
                        case "0":
                            //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                            //草药的总量不取整，开出1.5g就是1.5g
                            //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                            //if (phaItem.SysClass.ID.ToString() == "PCC")
                            //{
                            //    orderBase.Qty = Math.Round(doseOnce * execQty / phaItem.BaseDose, 2);
                            //    orderBase.Unit = phaItem.MinUnit;
                            //    unitFlag = "1";//最小单位
                            //}
                            //else
                            //{
                            //西药允许输入分数，对于每次用量2/3片的，
                            // 由于除不尽，总量这里计算出来截取一下 再取整 houwb
                            orderBase.Qty = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce * execQty / phaItem.BaseDose, 3)));
                            orderBase.Unit = phaItem.MinUnit;
                            unitFlag = "1";
                            //}
                            break;
                        case "1":
                            //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                            //草药的总量不取整，开出1.5g就是1.5g
                            //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                            //if (phaItem.SysClass.ID.ToString() == "PCC")
                            //{
                            //    orderBase.Qty = Math.Round((doseOnce * execQty / phaItem.BaseDose) / phaItem.PackQty, 2);
                            //    orderBase.Unit = phaItem.PackUnit;
                            //    unitFlag = "0";//包装单位
                            //}
                            //else
                            //{
                            orderBase.Qty = Math.Ceiling((doseOnce * execQty / phaItem.BaseDose) / phaItem.PackQty);
                            orderBase.Unit = phaItem.PackUnit;
                            unitFlag = "0";
                            //}
                            break;
                        case "2":
                            //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                            //草药的总量不取整，开出1.5g就是1.5g
                            //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                            //if (phaItem.SysClass.ID.ToString() == "PCC")
                            //{
                            //    orderBase.Qty = Math.Round(doseOnce * execQty / phaItem.BaseDose, 2);
                            //    orderBase.Unit = phaItem.MinUnit;
                            //    unitFlag = "1";//最小单位
                            //}
                            //else
                            //{
                            orderBase.Qty = Math.Ceiling(Math.Round(Math.Ceiling(doseOnce / phaItem.BaseDose) * execQty, 6));
                            orderBase.Unit = phaItem.MinUnit;
                            unitFlag = "1";
                            //}
                            break;
                        case "3":
                            //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                            //草药的总量不取整，开出1.5g就是1.5g
                            //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                            //if (phaItem.SysClass.ID.ToString() == "PCC")
                            //{
                            //    orderBase.Qty = Math.Round((doseOnce / phaItem.BaseDose * execQty) / phaItem.PackQty, 2);
                            //    orderBase.Unit = phaItem.MinUnit;
                            //    orderBase.Unit = phaItem.PackUnit;
                            //    unitFlag = "0";//包装单位
                            //}
                            //else
                            //{
                            orderBase.Qty = Math.Ceiling(Math.Round((Math.Ceiling((doseOnce / phaItem.BaseDose) / phaItem.PackQty)) * execQty, 6));
                            orderBase.Unit = phaItem.PackUnit;
                            unitFlag = "0";
                            //}
                            break;
                        default:
                            orderBase.Qty = Math.Round(doseOnce / phaItem.BaseDose, 2) * execQty;
                            orderBase.Unit = phaItem.MinUnit;
                            unitFlag = "1";
                            break;
                    }

                    if (orderBase.GetType() == typeof(FS.HISFC.Models.Order.OutPatient.Order))
                    {
                        ((FS.HISFC.Models.Order.OutPatient.Order)orderBase).MinunitFlag = unitFlag;
                    }

                    #endregion
                }
                else if (orderBase.Item.ItemType == EnumItemType.UnDrug)
                {
                    orderBase.Qty = orderBase.Frequency.Times.Length * orderBase.HerbalQty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }


            #endregion

            return 1;
        }

        public string ErrInfo
        {
            get
            {
                return this.errInfo;
            }
            set
            {
                this.errInfo = value;
            }
        }

        #endregion

        #region IOrderSplit 成员

        /// <summary>
        /// 获取药品的取整类型
        /// </summary>
        /// <param name="index">0 门诊；1 住院临嘱；2 住院长嘱</param>
        /// <param name="order"></param>
        /// <returns></returns>
        public string GetSplitType(int index, FS.HISFC.Models.Order.Order orderBase)
        {
            //增加如果药房维护取整规则，获取取整规则
            if (hsDrug.Contains(orderBase.Item.ID))
            {
                storage = hsDrug[orderBase.Item.ID] as FS.HISFC.Models.Pharmacy.Storage;
            }
            else
            {
                storage = storageManager.GetStockInfoByDrugCode(orderBase.StockDept.ID, orderBase.Item.ID);
                hsDrug.Add(orderBase.Item.ID, storage);
            }
            if (!string.IsNullOrEmpty(storage.Item.ID))
            {
                if (index == 0
                    //&& !string.IsNullOrEmpty(storage.SplitType)
                    )
                {
                    return storage.SplitType;
                }
                if (index == 1
                    //&& !string.IsNullOrEmpty(storage.LZSplitType)
                    )
                {
                    return storage.LZSplitType;
                }
                if (index == 2
                    //&& !string.IsNullOrEmpty(storage.CDSplitType)
                    )
                {
                    return storage.CDSplitType;
                }
            }

            return "";
        }

        #endregion
    }
}
