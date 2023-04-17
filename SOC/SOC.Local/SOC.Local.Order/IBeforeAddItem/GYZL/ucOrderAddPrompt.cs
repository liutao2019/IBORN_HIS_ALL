using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.IBeforeAddItem.GYZL
{
    /// <summary>
    /// 开立项目之前判断
    /// </summary>
    public class ucOrderAddPrompt : FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem
    {
        #region IBeforeAddItem 成员


        /// <summary>
        /// 错误信息
        /// </summary>
        string err = "";
        public string ErrInfo
        {
            get
            {
                return err;
            }
            set
            {
                err = value;
            }
        }

        public int OnBeforeAddItemForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            return this.CheckOrder(patientInfo, reciptDept, reciptDoct, alOrder, FS.HISFC.Models.Base.ServiceTypes.I);
        }

        public int OnBeforeAddItemForOutPatient(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            return this.CheckOutOrder(regObj, reciptDept, reciptDoct, alOrder, FS.HISFC.Models.Base.ServiceTypes.C);
        }

        #endregion


        System.Windows.Forms.NotifyIcon notify = null;

        FS.HISFC.BizLogic.Fee.Interface feeInterface = new FS.HISFC.BizLogic.Fee.Interface();

        /// <summary>
        /// 保证跟医生站显示的是一样的
        /// </summary>
        //FS.HISFC.BizProcess.Interface.Common.IItemExtendInfo iItemExtendInfo = FS.HISFC.Components.Common.Classes.Function.IItemExtendInfo;


        FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo IItemCompareInfo = null;

        FS.HISFC.BizLogic.Manager.UndrugztManager ztManager = new FS.HISFC.BizLogic.Manager.UndrugztManager();

        #region 函数实现

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CheckOrder(FS.HISFC.Models.RADT.Patient pateint, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder, FS.HISFC.Models.Base.ServiceTypes type)
        {
            try
            {
                #region 警戒线判断

                if (pateint.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
                {
                    bool isFreeItem = true;
                    foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrder)
                    {
                        if (order.Item.Price > 0 && order.OrderType.IsCharge)
                        {
                            isFreeItem = false;
                            break;
                        }
                    }

                    //实时获取最新的警戒线、余额等信息
                    FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
                    FS.HISFC.Models.RADT.PatientInfo pInfo = inpatientMgr.QueryPatientInfoByInpatientNO(pateint.ID);

                    if (!isFreeItem && pInfo.PVisit.MoneyAlert != 0 && pInfo.FT.LeftCost < pInfo.PVisit.MoneyAlert)
                    {
                        MessageBox.Show(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + pInfo.Name + "】已经欠费，\r\n\r\n余额： " + pInfo.FT.LeftCost.ToString() + "\r\n警戒线： " + pInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n不允许继续开立！\r\n\r\n如有疑问请联系医务科、财务科！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                    }
                }

                #endregion

                #region 项目扩展信息提示

                #region 采用新接口

                if (IItemCompareInfo == null)
                {
                    IItemCompareInfo = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Common.Controls.ucInputItem), typeof(FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo)) as FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo;
                }
                if (IItemCompareInfo == null)
                {
                    return 1;
                }

                string itmExtendInfo = "";
                ArrayList al = new ArrayList();

                FS.HISFC.Models.Fee.Item.Undrug undrug = null;
                List<FS.HISFC.Models.Fee.Item.UndrugComb> lstzt = null;

                FS.HISFC.Models.SIInterface.Compare compare = null;

                //是否需审批（公医）
                bool approvalFlag = false;

                foreach (FS.HISFC.Models.Order.Order order in alOrder)
                {
                    //非药品
                    undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);

                    #region 非药品（套餐）
                    if (undrug != null && undrug.UnitFlag == "1")
                    {
                        lstzt = FS.SOC.HISFC.BizProcess.Cache.Fee.GetUndrugZTDetail(undrug.ID);
                        if (lstzt == null)
                        {
                            if (this.ztManager.QueryUnDrugztDetail(undrug.ID, ref lstzt) == -1)
                            {
                                MessageBox.Show(this.ztManager.Err);
                                return -1;
                            }
                        }
                        if (lstzt.Count == 0)
                        {
                            return 1;
                        }

                        string extendItemInfos = "";
                        FS.HISFC.Models.Fee.Item.Undrug undrugItem = null;

                        for (int j = 0; j < lstzt.Count; j++)
                        {
                            FS.HISFC.Models.Fee.Item.UndrugComb obj = lstzt[j];

                            undrugItem = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(obj.ID);

                            if (IItemCompareInfo != null)
                            {
                                //int iRtn = IItemCompareInfo.GetItemExtendInfo(undrugItem, pateint, ref compare, ref approvalFlag, ref itmExtendInfo);
                                //if (iRtn == -1)
                                //{
                                //    MessageBox.Show(IItemCompareInfo.ErrInfo);
                                //    return -1;
                                //}

                                //string showInfo = "";
                                ////提示自费项目
                                //if (compare.CenterItem.ItemGrade == "3")
                                //{
                                //    showInfo += " [自费]";
                                //}

                                ////提示需审批项目
                                //if (approvalFlag)
                                //{
                                //    showInfo += " [需审批]";
                                //}

                                ////提示限制性用药项目
                                //if (!string.IsNullOrEmpty(compare.Practicablesymptomdepiction))
                                //{
                                //    showInfo += " " + compare.Practicablesymptomdepiction;
                                //}

                                //if (!string.IsNullOrEmpty(showInfo))
                                //{
                                //    extendItemInfos += "\r\n" + undrugItem.Name + (string.IsNullOrEmpty(undrugItem.Specs) ? "" : "[" + undrugItem.Specs + "]") + showInfo;
                                //}
                            }
                        }

                        if (!string.IsNullOrEmpty(extendItemInfos))
                        {
                            if (MessageBox.Show("套餐【" + order.Item.Name + "】包含项目：\n" + extendItemInfos + "\r\n\r\n是否继续开立？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                err = "已取消【" + order.Item.Name + "】开立！";
                                return -1;
                            }
                        }

                    }
                    #endregion

                    #region 药品
                    else
                    {
                        //int iRtn = IItemCompareInfo.GetItemExtendInfo(order.Item, pateint, ref compare, ref approvalFlag, ref itmExtendInfo);
                        //if (iRtn == -1)
                        //{
                        //    MessageBox.Show(IItemCompareInfo.ErrInfo);
                        //    return -1;
                        //}

                        string showInfo = "";
                        //提示自费项目
                        if (compare.CenterItem.ItemGrade == "3")
                        {
                            showInfo += " [自费]";
                        }

                        //提示需审批项目
                        if (approvalFlag)
                        {
                            showInfo += " [需审批]";
                        }

                        //提示限制性用药项目
                        if (!string.IsNullOrEmpty(compare.Practicablesymptomdepiction))
                        {
                            showInfo += " " + compare.Practicablesymptomdepiction;
                        }
                        if (!string.IsNullOrEmpty(compare.CenterItem.Memo))
                        {
                            showInfo += " " + compare.CenterItem.Memo;
                        }

                        if (!string.IsNullOrEmpty(showInfo))
                        {
                            if (MessageBox.Show("\r\n" + order.Item.Name + (string.IsNullOrEmpty(order.Item.Specs) ? "" : " [" + order.Item.Specs + "]") + "\r\n\r\n" + showInfo + "\r\n\r\n是否继续开立？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                err = "已取消【" + order.Item.Name + "】开立！";
                                return -1;
                            }
                        }
                    }
                    #endregion
                }

                #endregion

                #endregion

                #region 开立纤维结肠镜检查、纤维十二指肠检查 提示知情同意书

                foreach (FS.HISFC.Models.Order.Order order in alOrder)
                {
                    if (order.Item.Name.Contains("纤维结肠镜检查")
                        || order.Item.Name.Contains("纤维胃十二指肠镜检查"))
                    {
                        MessageBox.Show("【" + order.Item.Name + "】需要签订合同书，并在病案首页登记！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                #endregion

                return 1;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                //return -1;
            }
            return 1;
        }
        #endregion

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CheckOutOrder(FS.HISFC.Models.RADT.Patient regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder, FS.HISFC.Models.Base.ServiceTypes type)
        {
            try
            {
                FS.HISFC.Models.Base.Const conObj = null;

                IList<string> hsWarn = new List<string>();

                //当患者为医保患者
                if (regObj.Pact.PayKind.ID.Equals("02"))
                {
                    FS.HISFC.BizLogic.Fee.Interface feeInterface = new FS.HISFC.BizLogic.Fee.Interface();
                    bool judgeItemGrade = true;
                    foreach (FS.HISFC.Models.Order.Order order in alOrder)
                    {
                        if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                        {
                            if (((FS.HISFC.Models.Fee.Item.Undrug)(order.Item)).UnitFlag == "1")
                            {
                                judgeItemGrade = false;
                            }
                        }
                        if (judgeItemGrade)
                        {
                            FS.HISFC.Models.SIInterface.Compare objCompare = feeInterface.GetCompareDrugItemByItemCode(regObj.Pact.ID, order.Item.ID);
                            if (object.Equals(objCompare, null) || objCompare.CenterItem.ItemGrade.Equals("3"))
                            {
                                if (MessageBox.Show("该药品【" + order.Item.Name + "】属于自费项目，请注意！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                                {
                                    return -1;
                                }
                                hsWarn.Add(order.Item.Name);
                            }
                        }
                    }
                }
                //当患者为公费患者（）
                if (regObj.Pact.PayKind.ID.Equals("03") || regObj.Pact.PayKind.ID.Equals("04") || regObj.Pact.PayKind.ID.Equals("05"))
                {



                }

                if (hsWarn.Count > 0)
                {
                    if (notify == null)
                    {
                        notify = new System.Windows.Forms.NotifyIcon();
                        notify.Icon = FS.SOC.Local.Order.Properties.Resources.HIS;
                    }

                    string warn = "";
                    foreach (string key in hsWarn)
                    {

                        warn += key + "\r\n";
                    }

                    notify.Visible = true;
                    notify.ShowBalloonTip(4, "自费药提示", warn, System.Windows.Forms.ToolTipIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                err = ex.Message;
                return -1;
            }

            return 1;
        }
    }
}
