using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Gysy.ILisCalculateTube
{
    public class LisCalculateTube : FS.HISFC.BizProcess.Interface.FeeInterface.ILisCalculateTube
    {
        public LisCalculateTube()
        {
        }

        /// <summary>
        /// 对应Lis血管项目
        /// </summary>
        private static Dictionary<string, FS.HISFC.Models.Fee.Item.Undrug> dictionaryItem = new Dictionary<string, FS.HISFC.Models.Fee.Item.Undrug>();
        private static Dictionary<string, string> hsItem = new Dictionary<string, string>();
        Dictionary<string, decimal> MapCuvetteCounts = new Dictionary<string, decimal>();

        #region ILisCalculateTube 成员

        private string errInfo = string.Empty;
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

        public int LisCalculateTubeForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            return 1;
        }

        public int LisCalculateTubeForOutPatient(FS.HISFC.Models.Registration.Register r, System.Collections.ArrayList alFeeItemList, string recipeSequence, ref decimal owncost, ref System.Collections.ArrayList alTubeList)
        {
            //初始化试管项目
            if (this.InitDictionaryItem() < 0)
            {
                return -1;
            }

            //首先只检测Lis项目
            ArrayList alLisItemList = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alFeeItemList)
            {
                //去掉收取的Lis试管项目
                if (feeItemList.FTSource.Equals("0") && hsItem.ContainsKey(feeItemList.Item.ID))//收费处自己收取的
                {
                    //目前的试管数量
                    MapCuvetteCounts[hsItem[feeItemList.Item.ID]] += feeItemList.Item.Qty;
                    continue;
                }

                //只判断检验项目
                if (feeItemList.Item.SysClass.ID.ToString().Equals(FS.HISFC.Models.Base.EnumSysClass.UL.ToString()))
                {
                    alLisItemList.Add(feeItemList);
                }
            }

            //转换费用信息
            ArrayList alItemList = Function.ConvertItemToPackage(alLisItemList, ref this.errInfo);
            if (alItemList == null)
            {
                return -1;
            }

            ArrayList alComboFeeItemList = new ArrayList();
            //按照组合号分组
            Dictionary<string, ArrayList> dictionaryCombo = new Dictionary<string, ArrayList>();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alItemList)
            {
                if (!dictionaryCombo.ContainsKey(feeItemList.Order.Combo.ID))
                {
                    dictionaryCombo[feeItemList.Order.Combo.ID] = new ArrayList();
                }

                dictionaryCombo[feeItemList.Order.Combo.ID].Add(feeItemList);
            }

            foreach (KeyValuePair<string, ArrayList> entry in dictionaryCombo)
            {
                alComboFeeItemList.Add(entry.Value);
            }
            //计算试管
            int i = this.ListCalculate(alComboFeeItemList, ref alTubeList, ref this.errInfo);
            if (alTubeList != null)
            {
                owncost = 0;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alTubeList)
                {
                    owncost += feeItem.FT.OwnCost;
                }
            }

            return i;

        }

        #endregion

        #region 门诊检验试管带出

        public int ListCalculate(ArrayList alAllCombo, ref ArrayList alFeeItemList, ref string errorInfo)
        {
            string al = string.Empty;
            //FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = null;
            foreach (ArrayList alTempSameCombo in alAllCombo)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemListTemp = alTempSameCombo[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                //if (feeItemListTemp.Order.Sample.Name.Contains("血"))
                {
                    al += feeItemListTemp.Item.ID + "|";
                }

                //feeItemList = feeItemListTemp;
            }

            if (string.IsNullOrEmpty(al))
            {
                return 1;
            }

            al = al.Remove(al.Length - 1, 1);

            Dictionary<string, string> MapCuvetteItems = new Dictionary<string, string>();
            Dictionary<string, int> MapCuvetteNums = new Dictionary<string, int>();

            LisCalculateTubeManager orderMgr = new LisCalculateTubeManager();

            if (orderMgr.GetCuvetteItems(al, ref MapCuvetteItems, ref MapCuvetteNums) == -1)
            {
                errorInfo = "计算检验试管带出失败！";
                return -1;
            }

            foreach (ArrayList alTempSameCombo in alAllCombo)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = alTempSameCombo[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                //首先判断是否应该带出试管
                if (MapCuvetteItems.ContainsKey(feeItemList.Item.ID))
                {
                    //如果不存在该颜色试管，则数量默认为0
                    if (!MapCuvetteCounts.ContainsKey(MapCuvetteItems[feeItemList.Item.ID]))
                    {
                        errorInfo = "Lis试管[" + MapCuvetteItems[feeItemList.Item.ID] + "管]对应的HIS带出项目没有维护，请联系管理员进行维护[常数编码：OutLisCalculateItem]！";
                        return -1;
                    }

                    //判断带出试管数量有没有超出，并管的情况下，后面的医嘱不带试管。
                    if (MapCuvetteCounts[MapCuvetteItems[feeItemList.Item.ID]] < MapCuvetteNums[MapCuvetteItems[feeItemList.Item.ID]])
                    {
                        #region 带出项目
                        foreach (string key in dictionaryItem.Keys)
                        {

                            string temp = key;
                            if (key.IndexOf('[')>0)
                            {
                                temp = key.Substring(0, key.IndexOf('['));
                            }                           

                            if (temp==MapCuvetteItems[feeItemList.Item.ID])
                            {
                                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = feeItemList.Clone();
                                feeItem.Item = dictionaryItem[key].Clone();
                                if (feeItem.Item == null)
                                {
                                    errorInfo = "Lis试管[" + MapCuvetteItems[feeItemList.Item.ID] + "管]对应的HIS带出项目没有维护，请联系管理员进行维护[常数编码：OutLisCalculateItem]！";
                                    return -1;
                                }
                                feeItem.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                                feeItem.Item.Qty = 1;
                                feeItem.Days = 1;
                                feeItem.FeePack = "0";
                                feeItem.Order.Sample.Name = "";
                                feeItem.FTSource = "0";//收费员自己收费
                                feeItem.OrgItemRate = 1;
                                feeItem.NewItemRate = 1;
                                feeItem.ItemRateFlag = "1";
                                feeItem.Item.PackQty = 1;
                                feeItem.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItem.Item.Price * feeItem.Item.Qty / feeItem.Item.PackQty, 2);
                                feeItem.FT.PayCost = 0;
                                feeItem.FT.PubCost = 0;
                                feeItem.FT.OwnCost = feeItem.FT.TotCost;
                                feeItem.FT.RebateCost = 0;
                                feeItem.NoBackQty = Math.Abs(feeItemList.Item.Qty);

                                alFeeItemList.Add(feeItem);
                                MapCuvetteCounts[MapCuvetteItems[feeItemList.Item.ID]]++;
                            }



                        }
                        #endregion
                    }
                    //如果已经带出项目超出了，这种情况如何处理，插入负记录，待确认？
                    else
                    {
                    }
                }
                        



            }




            return 1;
        }

        /// <summary>
        /// 返回Lis血管对应的项目
        /// </summary>
        /// <param name="TubeColor"></param>
        /// <returns></returns>
        private int InitDictionaryItem()
        {
            if (dictionaryItem == null || dictionaryItem.Count == 0)
            {
                FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();
                FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
                //ID为Lis试管的颜色，Name为HIS对应的项目名称
                ArrayList alConst = constManager.GetList("OutLisCalculateItem");
                if (alConst == null || alConst.Count == 0)
                {
                    this.errInfo = "初始化Lis试管对应的HIS项目失败，请联系管理员进行维护[常数编码：OutLisCalculateItem]！";
                    return -1;
                }

                foreach (FS.FrameWork.Models.NeuObject con in alConst)
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;
                    string[] arrCon = con.Name.Split(',');
                    if (arrCon.Length > 1)
                    {
                        for (int i = 0; i <= arrCon.Length - 1; ++i)
                        {
                            if (!string.IsNullOrEmpty(arrCon[i]))
                            {
                                undrug = itemManager.GetUndrugByCode(arrCon[i]);
                                if (undrug == null || string.IsNullOrEmpty(undrug.ID))
                                {
                                    this.errInfo = "Lis试管[" + con.ID + "管]对应的HIS带出项目已过期，请联系管理员进行维护[常数编码：OutLisCalculateItem]！";
                                    return -1;
                                }
                                dictionaryItem.Add(con.ID + "[" + i.ToString() + "]", undrug);//红(1),红(2)

                                hsItem[undrug.ID + "[" + con.ID + "(" + i.ToString() + ")]"] = con.ID + "[" + i.ToString() + "]";
                            }

                        }

                    }
                    else
                    {
                        undrug = itemManager.GetUndrugByCode(con.Name);
                        if (undrug == null || string.IsNullOrEmpty(undrug.ID))
                        {
                            this.errInfo = "Lis试管[" + con.ID + "管]对应的HIS带出项目已过期，请联系管理员进行维护[常数编码：OutLisCalculateItem]！";
                            return -1;
                        }
                        dictionaryItem.Add(con.ID, undrug);

                        hsItem[undrug.ID + "[" + con.ID + "]"] = con.ID;
                    }
                }
            }

            foreach (KeyValuePair<string, string> entry in hsItem)
            {
                string color = "";
                if (entry.Value.IndexOf('[') > 0)
                {
                    color = entry.Value.Substring(0, entry.Value.IndexOf('['));
                }
                else
                {
                    color = entry.Value;
                }

                //   string color=entry.Value.Substring(0,entry.Value.Length -entry.Value.IndexOf('[')
                //初始化试管数量
                MapCuvetteCounts[color] = 0;
            }
            return 1;

        }

        #endregion
    }
}
