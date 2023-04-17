using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace FS.SOC.Local.Order.IBeforeSaveOrder.ZDLY
{
    /// <summary>
    /// 草药控制 
    /// </summary>
    class BeforeSaveOrderInterface : FS.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder
    {
        #region IBeforeSaveOrder 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        private string errInfo = string.Empty;

        /// <summary>
        /// 错误信息
        /// </summary>
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


        /// <summary>
        /// 项目扩展信息接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo iItemCompareInfo = null;

        /// <summary>
        /// 项目扩展信息接口
        /// </summary>
        public FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo IItemCompareInfo
        {
            get
            {
                if (iItemCompareInfo == null)
                {
                    iItemCompareInfo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.IBeforeSaveOrder.ZDLY.BeforeSaveOrderInterface), typeof(FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo)) as FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo;
                }
                return iItemCompareInfo;
            }
        }

        FS.HISFC.BizLogic.Manager.PactStatRelation pactStatRelationBizLogic = new FS.HISFC.BizLogic.Manager.PactStatRelation();

        /// <summary>
        /// 住院
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int BeforeSaveOrderForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            #region 开立限制

            /*
             * 根据医务科规定。住院在出院带药有所限制：
             * 广州医保、其他医保出院带药 限制开药天数7天   药费没有限制
             * 省公医出院带药 限制开药天数7天  药费没有限制
             * 市公医出院带药 限制开药天数30天  药费限制350块
             * 区公医出院带药 限制开药天数30天  药费限制350块
             * 自费出院带药 限制开药天数30天  药费没有限制
             * */

            /*
                医保类别：
                    1、广州医保：
                        1.1、居民医保
                        1.2、门慢医保
                        1.3、门特医保
                        1.4、生育保险
                        1.5、职工医保
                    2、东莞医保
                    3、深圳医保
                    4、异地医保（省医保）
                公医：
                    1、省公医
                        1.1、省公医
                        1.2、本院职工
                        1.3、特约单位
                    2、市公医
                    3、区公医
                自费
             **/

            string sql = @"select max(parent_name)  from com_pactcompare t where pact_code='{0}'";
            errInfo = string.Empty;

            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            string pactName = deptMgr.ExecSqlReturnOne(string.Format(sql, patientInfo.Pact.ID));



            #region
            if (FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID).PayKind.ID == "01")
            {
                foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrder)
                {
                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (order.Item.SysClass.ID.ToString() != "PCC"
                            && order.OrderType.ID == "CD" //出院带药
                            && order.HerbalQty > 30)
                        {
                            errInfo += order.Item.Name + "\r\n";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(errInfo))
                {
                    errInfo = "患者【" + patientInfo.Name + "】 合同单位【" + FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID).Name + "】\r\n" + errInfo + "出院带药天数不允许超过30天！\r\n\r\n如有疑问请联系医务科！";
                    return -1;
                }
            }
            #endregion


            #region 广州医保（异地医保参考广州医保）出院带药 限制开药天数7天   药费没有限制

            if (FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID).PayKind.ID == "02")
            {
                //if (!"东莞医保|深圳医保|异地医保".Contains(pactName.Trim()))
                //{
                foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrder)
                {
                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (order.Item.SysClass.ID.ToString() != "PCC"
                            && order.OrderType.ID == "CD" //出院带药
                            && order.HerbalQty > 7)
                        {
                            errInfo += order.Item.Name + "\r\n";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(errInfo))
                {
                    errInfo = "患者【" + patientInfo.Name + "】 合同单位【" + FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID).Name + "】\r\n" + errInfo + "出院带药天数不允许超过7天！\r\n\r\n如有疑问请联系医务科！";
                    return -1;
                }
                //}
            }
            #endregion

            else if (FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID).PayKind.ID == "03")
            {
                #region 省公医出院带药 限制开药天数7天  药费没有限制

                if ("省公医|本院职工|特约单位".Contains(pactName.Trim()))
                {
                    foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrder)
                    {
                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (order.Item.SysClass.ID.ToString() != "PCC"
                                && order.OrderType.ID == "CD" //出院带药
                                && order.HerbalQty > 7)
                            {
                                errInfo += order.Item.Name + "\r\n";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(errInfo))
                    {
                        errInfo = "患者【" + patientInfo.Name + "】 合同单位【" + FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID).Name + "】\r\n" + errInfo + "出院带药天数不允许超过7天！\r\n\r\n如有疑问请联系医务科！";
                        return -1;
                    }
                }

                #endregion

                #region 市公医、区公医出院带药 限制开药天数30天  药费限制350块

                else if ("市公医|市公医|区公医".Contains(pactName.Trim()))
                {
                    decimal totCost = 0;
                    foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrder)
                    {
                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug
                            && order.Item.ID != "999"
                            && order.OrderType.IsCharge)
                        {
                            if (order.Item.SysClass.ID.ToString() != "PCC"
                                && order.OrderType.ID == "CD") //出院带药
                            {
                                if (order.HerbalQty > 7)
                                {
                                    errInfo += order.Item.Name + "\r\n";
                                }

                                FS.HISFC.Models.SIInterface.Compare compareItem = new FS.HISFC.Models.SIInterface.Compare();
                                bool approvalFlag = false;
                                string extendInfo = string.Empty;
                                //if (this.IItemCompareInfo.GetItemExtendInfo(order.Item, patientInfo, ref compareItem, ref approvalFlag, ref extendInfo) == 1)
                                //{
                                //    if (!object.Equals(compareItem, null))
                                //    {
                                //        if (order.Unit == FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).MinUnit)
                                //        {
                                //            totCost += order.Item.Price * order.Qty / FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).PackQty;
                                //        }
                                //        else
                                //        {
                                //            totCost += order.Item.Price * order.Qty;
                                //        }
                                //    }
                                //}
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(errInfo))
                    {
                        errInfo += "出院带药天数不允许超过30天！\r\n";
                    }
                    if (totCost > 350)
                    {
                        //errInfo += "出院带药总金额为" + Math.Round(totCost, 2).ToString() + "元,不允许超过" + statRelation + "元！\r\n";
                        if (MessageBox.Show("患者【" + patientInfo.Name + "】 合同单位【" + FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID) + "】出院带药总金额为" + Math.Round(totCost, 2).ToString() + "元,超过" + 350 + "元！\r\n", "仅供参考", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return -1;
                        }
                    }

                    if (!string.IsNullOrEmpty(errInfo))
                    {
                        errInfo = "患者【" + patientInfo.Name + "】 合同单位【" + FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patientInfo.Pact.ID).Name + "】\r\n" + errInfo + "\r\n\r\n如有疑问请联系医务科！";
                        return -1;
                    }
                }
                #endregion
            }

            #endregion

            #region 开立提示
            /* 1、长嘱存在护理级别再新开时，给出提示
             * 2、长嘱临嘱存在相同的有效医嘱时给出提示
             * */

            string UN_sql = @"select wm_concat( f.item_name)
from met_ipm_order f
where f.inpatient_no='{0}'
and f.decmps_state='1'
and f.class_code='UN'
and (f.mo_order!='{1}' or '{1}'='ALL')
and f.mo_stat not in('3','4','7')";

            string Item_sql = @"select 1
from met_ipm_order f
where f.inpatient_no='{0}'
and f.item_code='{1}'
and (f.mo_order!='{2}' or '{2}'='ALL')
and f.mo_stat not in('3','4','7')";

            bool isHaveUNOrder = false;

            string unOrder = "";

            string repeatItem = "";

            string moOrder = "";

            foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrder)
            {
                moOrder = string.IsNullOrEmpty(order.ID) ? "ALL" : order.ID;
                //护理级别
                if (order.Item.SysClass.ID.ToString() == "UN")
                {
                    isHaveUNOrder = true;
                    unOrder = moOrder;
                }

                //按照项目提示的先屏蔽了 会影响效率
                //if (deptMgr.ExecSqlReturnOne(string.Format(Item_sql, patientInfo.ID, order.Item.ID, moOrder)) == "1")
                //{
                //    repeatItem += order.Item.Name + "\r\n";
                //}
            }

            if (isHaveUNOrder)
            {
                if (string.IsNullOrEmpty(unOrder))
                {
                    unOrder = "ALL";
                }

                string unItem = deptMgr.ExecSqlReturnOne(string.Format(UN_sql, patientInfo.ID, unOrder));
                if (!string.IsNullOrEmpty(unItem) && unItem != "-1")
                {
                    MessageBox.Show("已存在有效的护理级别医嘱：" + unItem, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //if (!string.IsNullOrEmpty(repeatItem))
            //{
            //    Classes.Function.ShowBalloonTip(3, "提示", "以下项目可能之前已经开立：\r\n\r\n" + repeatItem, ToolTipIcon.Info);
            //}

            #endregion

            return 1;
        }

        /// <summary>
        /// 限制项目
        /// </summary>
        ArrayList alLimitItem = null;

        FS.FrameWork.Public.ObjectHelper limitItemHelper = null;

        //System.Windows.Forms.NotifyIcon notify = null;

        /// <summary>
        /// 门诊
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int BeforeSaveOrderForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            errInfo = "";

            #region 

            #endregion

            #region 草药限制
            //1、必须组合
            //2、不允许项目重复

            Hashtable itemList = new Hashtable();
            string combo = string.Empty;
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in alOrder)
            {
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (order.Item.SysClass.ID.ToString() == "PCC")
                    {
                        if (string.IsNullOrEmpty(combo))
                        {
                            combo = order.Combo.ID;
                        }
                        if (combo != order.Combo.ID)
                        {
                            errInfo = "草药必须组合！";
                            return -1;
                        }

                        if (!itemList.ContainsKey(order.Item.ID))
                        {
                            itemList.Add(order.Item.ID, order.Item.ID);
                        }
                        else
                        {
                            errInfo = "[" + order.Item.Name + "]项目重复！";
                            return -1;
                        }
                    }
                }
            }

            #endregion

            #region 开立限制
            //广州医保和公费医疗(这里是所有的公费...)： 急诊号  限制开药天数 3天
            //                     非急诊号   普通疾病 限制7天药
            //                     慢性病 限制30天药。

            /*
                医保类别：
                    1、广州医保：
                        1.1、居民医保
                        1.2、门慢医保
                        1.3、门特医保
                        1.4、生育保险
                        1.5、职工医保
                    2、东莞医保
                    3、深圳医保
                    4、异地医保（省医保）
                公医：
                    1、省公医
                        1.1、省公医
                        1.2、本院职工
                        1.3、特约单位
                    2、市公医
                    3、区公医
                自费
             **/

            string sql = @"select max(t.parent_name)  from com_pactcompare where pact_code='{0}'";

            //广州医保、所有公医提示
            bool isCheck = false;
            if (FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(regObj.Pact.ID).PayKind.ID == "03")
            {
                isCheck = true;
            }
            else if (FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(regObj.Pact.ID).PayKind.ID == "02")
            {
                FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
                string pactName = deptMgr.ExecSqlReturnOne(string.Format(sql, regObj.Pact.ID));
                if (!"东莞医保|深圳医保|异地医保|职工医保".Contains(pactName.Trim()))
                {
                    isCheck = true;
                }
            }

            if (isCheck)
            {
                //急诊号 限药3天
                if (FS.SOC.HISFC.BizProcess.Cache.Fee.GetRegLevl(regObj.DoctorInfo.Templet.RegLevel.ID).IsEmergency)
                {
                    decimal totCost = 0;
                    foreach (FS.HISFC.Models.Order.Order order in alOrder)
                    {
                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (order.Item.SysClass.ID.ToString() != "PCC"
                                && order.HerbalQty > 3)
                            {
                                errInfo += order.Item.Name + "\r\n";
                            }
                            FS.HISFC.Models.SIInterface.Compare compareItem = new FS.HISFC.Models.SIInterface.Compare();
                            bool approvalFlag = false;
                            string extendInfo = string.Empty;
                            //if (this.IGetItemExtendInfo.GetItemExtendInfo(order.Item, regObj, ref compareItem, ref approvalFlag, ref extendInfo) == 1)
                            //{
                            //    if (!object.Equals(compareItem, null))
                            //    {
                            //        if (!string.Equals(compareItem.CenterItem.ItemGrade, "3") && order.Unit == FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).MinUnit)
                            //        {
                            //            totCost += order.Item.Price * order.Qty / FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).PackQty;
                            //        }
                            //        else if (!string.Equals(compareItem.CenterItem.ItemGrade, "3"))
                            //        {
                            //            totCost += order.Item.Price * order.Qty;
                            //        }
                            //    }
                            //}

                        }
                    }
                    if (!string.IsNullOrEmpty(errInfo))
                    {
                        errInfo += "用药天数不允许超过3天！\r\n\r\n如有疑问请联系医务科！";
                        return -1;
                    }


                    string statRelation = this.pactStatRelationBizLogic.GetStatRelation(regObj.Pact.ID, "01", regObj.DoctorInfo.Templet.RegLevel.IsEmergency);

                    if (statRelation!= "-1")
                    {
                        if (totCost > FS.FrameWork.Function.NConvert.ToInt32(statRelation))
                        {
                            if (MessageBox.Show("患者【" + regObj.Name + "】\r\n 合同单位【" + FS.SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(regObj.Pact.ID) + "】\r\n急诊用药总金额为" + Math.Round(totCost, 2).ToString() + "元,超过" + statRelation + "元！是否继续开立？\r\n", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return -1;
                            }
                        }
                    }
                }
                else
                {
                    //普通门诊限药7天，慢病限药30天
                    string stopInfo = "";
                    string warnInfo = "";
                    foreach (FS.HISFC.Models.Order.Order order in alOrder)
                    {

                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            if (order.Item.SysClass.ID.ToString() != "PCC")
                            {
                                if (order.HerbalQty > 30)
                                {
                                    stopInfo += order.Item.Name + "\r\n";
                                }
                                else if (order.HerbalQty > 7)
                                {
                                    warnInfo += order.Item.Name + "\r\n";
                                    //if (MessageBox.Show("患者【" + regObj.Name + "】是否是慢病患者？\r\n\r\n[" + order.Item.Name + "]用药天数超过7天,非慢病患者限制开立7天内药量！\r\n\r\n如有疑问请联系医务科！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                    //{
                                    //    errInfo = "[" + order.Item.Name + "]用药天数不允许超过7天！\r\n\r\n如有疑问请联系医务科！";
                                    //    return -1;
                                    //}
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(stopInfo))
                    {
                        errInfo = stopInfo + "用药天数不允许超过30天！\r\n\r\n如有疑问请联系医务科！";
                        return -1;
                    }

                    if (!string.IsNullOrEmpty(warnInfo))
                    {
                        if (MessageBox.Show("患者【" + regObj.Name + "】是否是慢病患者？\r\n\r\n[" + warnInfo + "]用药天数超过7天,非慢病患者限制开立7天内药量！\r\n\r\n如有疑问请联系医务科！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return -1;
                        }
                    }
                }
            }

            #endregion

            return 1;
        }

        #endregion
    }
}
