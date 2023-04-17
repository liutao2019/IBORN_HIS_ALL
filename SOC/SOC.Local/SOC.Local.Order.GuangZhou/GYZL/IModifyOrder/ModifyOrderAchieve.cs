using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Order.GuangZhou.GYZL.IModifyOrder
{
    /// <summary>
    /// 修改医嘱（处方）接口
    /// </summary>
    public class ModifyOrderAchieve : SOC.HISFC.BizProcess.OrderInterface.Common.IModifyOrder
    {
        #region IModifyOrder 成员

        string errInfo = "";

        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
            set
            {
                errInfo = value;
            }
        }

        public int ModifyInOrder(FS.HISFC.Models.Order.Inpatient.Order inOrder, string changedField)
        {
            return 1;
        }

        System.Windows.Forms.NotifyIcon notify = null;

        public int ModifyOutOrder(FS.HISFC.Models.Order.OutPatient.Order outOrder, string changedField)
        {
            return 1;

            //草药的提示 不允许在开立界面修改，必须要到草药界面修改
            if (outOrder.Item.SysClass.ID.ToString() == "PCC")
            {
                errInfo = "请在草药开立界面新增或修改草药信息！";
                return -1;
            }

            if (outOrder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                if (changedField == "总量" || changedField == "总量单位")
                {
                    try
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID);

                        if (phaItem == null)
                        {
                            errInfo = "查找药品信息失败：" + outOrder.Item.Name;
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
                            if (outOrder.Frequency == null)
                            {
                                frequence = 1;
                            }
                            else
                            {
                                if (outOrder.Frequency.Days[0] == "0" || string.IsNullOrEmpty(outOrder.Frequency.Days[0]))
                                {
                                    outOrder.Frequency.Days[0] = "1";
                                    frequence = outOrder.Frequency.Times.Length;
                                }
                                else
                                {
                                    try
                                    {
                                        frequence = Math.Round(outOrder.Frequency.Times.Length / FS.FrameWork.Function.NConvert.ToDecimal(outOrder.Frequency.Days[0]), 2);
                                    }
                                    catch
                                    {
                                        frequence = outOrder.Frequency.Times.Length;
                                    }
                                }
                            }
                        }
                        #endregion

                        string err = "";

                        decimal doseOnce = outOrder.DoseOnce;
                        if (outOrder.DoseUnit == phaItem.MinUnit)
                        {
                            doseOnce = outOrder.DoseOnce * phaItem.BaseDose;
                        }
                        #endregion

                        #region 计算取整总量

                        //0 最小单位总量取整" 数据库值 0
                        //1 包装单位总量取整" 数据库值 1  口服特别是中成药、妇科用药较多
                        //2 最小单位每次取整" 数据库值 2  针剂较多这样
                        //3 包装单位每次取整" 数据库值 3  几乎没有用
                        //4 最小单位可拆分 即不处理任何取整

                        string splitType = "4";
                        splitType = phaItem.SplitType;

                        //0 包装单位；1 最小单位
                        string unitFlag = "";

                        //计算的默认取药天数
                        //（数量*基本计量）/（每次量*频次） 下取整 是最大天数
                        int days = 1;

                        switch (splitType)
                        {

                            //0 最小单位总量取整" 数据库值 0
                            case "0":
                                //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                //草药的总量不取整，开出1.5g就是1.5g
                                //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                if (phaItem.SysClass.ID.ToString() == "PCC")
                                {
                                    //outOrder.Qty = Math.Round(doseOnce * frequence * outOrder.HerbalQty / phaItem.BaseDose, 2);
                                    //outOrder.Unit = phaItem.MinUnit;
                                    //unitFlag = "1";//最小单位
                                }
                                else
                                {
                                    //西药允许输入分数，对于每次用量2/3片的，
                                    // 由于除不尽，总量这里计算出来截取一下 再取整 houwb
                                    //outOrder.Qty = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(FS.FrameWork.Public.String.FormatNumberReturnString(doseOnce * frequence * outOrder.HerbalQty / phaItem.BaseDose, 3)));
                                    //outOrder.Unit = phaItem.MinUnit;
                                    //unitFlag = "1";

                                    days = (Int32)Math.Floor((outOrder.Qty * phaItem.BaseDose) / (doseOnce * frequence));
                                }
                                break;
                            //1 包装单位总量取整" 数据库值 1  口服特别是中成药、妇科用药较多
                            case "1":
                                //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                //草药的总量不取整，开出1.5g就是1.5g
                                //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                if (phaItem.SysClass.ID.ToString() == "PCC")
                                {
                                    //outOrder.Qty = Math.Round((doseOnce * frequence * outOrder.HerbalQty / phaItem.BaseDose) / phaItem.PackQty, 2);
                                    //outOrder.Unit = phaItem.PackUnit;
                                    //unitFlag = "0";//包装单位
                                }
                                else
                                {
                                    //outOrder.Qty = Math.Ceiling((doseOnce * frequence * outOrder.HerbalQty / phaItem.BaseDose) / phaItem.PackQty);
                                    //outOrder.Unit = phaItem.PackUnit;
                                    //unitFlag = "0";

                                    days = (Int32)Math.Floor((outOrder.Qty * phaItem.PackQty * phaItem.BaseDose) / (doseOnce * frequence));
                                }
                                break;

                            //2 最小单位每次取整" 数据库值 2  针剂较多这样
                            case "2":
                                //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                //草药的总量不取整，开出1.5g就是1.5g
                                //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                if (phaItem.SysClass.ID.ToString() == "PCC")
                                {
                                    //outOrder.Qty = Math.Round(doseOnce * frequence * outOrder.HerbalQty / phaItem.BaseDose, 2);
                                    //outOrder.Unit = phaItem.MinUnit;
                                    //unitFlag = "1";//最小单位
                                }
                                else
                                {
                                    //outOrder.Qty = Math.Ceiling(Math.Round(Math.Ceiling(doseOnce / phaItem.BaseDose) * frequence * outOrder.HerbalQty, 6));
                                    //outOrder.Unit = phaItem.MinUnit;
                                    //unitFlag = "1";

                                    days = (Int32)Math.Floor(outOrder.Qty / (Math.Ceiling(doseOnce / phaItem.BaseDose) * frequence));
                                }
                                break;
                            //3 包装单位每次取整" 数据库值 3  几乎没有用
                            case "3":
                                //草药开立不取整  houwb 2011-3-15{E0FCD746-5605-4318-860B-50467115BE6D}
                                //草药的总量不取整，开出1.5g就是1.5g
                                //如果是1包每次计量单位6g，开出来一天3g 就按照0.5包发药
                                if (phaItem.SysClass.ID.ToString() == "PCC")
                                {
                                    //outOrder.Qty = Math.Round((doseOnce / phaItem.BaseDose * frequence * outOrder.HerbalQty) / phaItem.PackQty, 2);
                                    ////outOrder.Unit = phaItem.MinUnit;
                                    //outOrder.Unit = phaItem.PackUnit;
                                    //unitFlag = "0";//包装单位
                                }
                                else
                                {
                                    //outOrder.Qty = Math.Ceiling(Math.Round((Math.Ceiling((doseOnce / phaItem.BaseDose) / phaItem.PackQty)) * frequence * outOrder.HerbalQty, 6));
                                    //outOrder.Unit = phaItem.PackUnit;
                                    //unitFlag = "0";
                                    days = (Int32)Math.Floor((outOrder.Qty * phaItem.PackQty) / (Math.Ceiling(doseOnce / phaItem.BaseDose) * frequence));
                                }
                                break;
                            //4 最小单位可拆分 即不处理任何取整
                            default:
                                //outOrder.Qty = Math.Round(doseOnce / phaItem.BaseDose, 2) * frequence * outOrder.HerbalQty;
                                //outOrder.Unit = phaItem.MinUnit;
                                //unitFlag = "1";

                                days = (Int32)Math.Ceiling((outOrder.Qty * phaItem.BaseDose) / (doseOnce * frequence));

                                break;
                        }

                        outOrder.HerbalQty = days;
                        #endregion

                        if (notify == null)
                        {
                            notify = new System.Windows.Forms.NotifyIcon();
                            //notify.Icon = FS.SOC.Local.Order.GuangZhou.Properties.Resources.HIS;
                        }

                        notify.Visible = true;
                        notify.ShowBalloonTip(3, "提示", "\r\n项目【" + outOrder.Item.Name + "】\r\n\r\n根据总量计算的取药天数应该为：" + days.ToString() + "天\r\n\r\n\r\n", System.Windows.Forms.ToolTipIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        errInfo = ex.Message;
                        return -1;
                    }
                }
            }
            return 1;
        }

        #endregion
    }
}
