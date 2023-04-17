using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Order.IBeforeSaveOrder.NanZhuang
{
    /// <summary>
    /// 保存医嘱（处方）前操作接口
    /// 1、超过警戒线不允许继续开立
    /// </summary>
    class BeforeSaveOrderInterface : FS.HISFC.BizProcess.Interface.Order.IBeforeSaveOrder
    {
        #region IBeforeSaveOrder 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        private string errInfo = "";

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
        /// 住院
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int BeforeSaveOrderForInPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
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
            return CheckOrder(regObj, reciptDept, reciptDoct, alOrder, FS.HISFC.Models.Base.ServiceTypes.C);

            return 1;
        }

        #endregion

        /// <summary>
        /// 药品管理
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();

        FS.HISFC.BizLogic.Pharmacy.Constant phaConMgr = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// 取药科室列表
        /// </summary>
        List<FS.FrameWork.Models.NeuObject> alStockDept = null;

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CheckOrder(FS.HISFC.Models.RADT.Patient regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder, FS.HISFC.Models.Base.ServiceTypes type)
        {
            try
            {
                if (CheckFeeQty(regObj, reciptDept, reciptDoct, alOrder, type) == -1)
                {
                    return -1;
                }

                FS.FrameWork.Models.NeuObject stockDept = null;
                FS.HISFC.Models.Pharmacy.Storage storage = null;

                foreach (FS.HISFC.Models.Order.Order order in alOrder)
                {
                    if (order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        continue;
                    }

                    //没有取药科室信息就重新获取一个
                    if (string.IsNullOrEmpty(order.StockDept.ID))
                    {
                        alStockDept = phaConMgr.GetRecipeDrugDept(reciptDept.ID, ((FS.HISFC.Models.Pharmacy.Item)order.Item).Type.ID);
                        if (alStockDept != null && alStockDept.Count > 0)
                        {
                            order.StockDept.ID = alStockDept[0].ID;
                        }
                    }

                    if (!string.IsNullOrEmpty(order.StockDept.ID))
                    {
                        storage = this.itemMgr.GetStockInfoByDrugCode(order.StockDept.ID, order.Item.ID);

                        decimal qty = order.Unit == storage.Item.PackUnit ? order.Qty * storage.Item.PackQty : order.Qty;
                        if (storage != null
                            && storage.LowQty > 0
                            && storage.LowQty >= storage.StoreQty - qty)
                        {
                            if (MessageBox.Show("【" + order.Item.Name + "】剩余库存" + (storage.StoreQty - qty).ToString() + storage.Item.MinUnit + ",已超过警戒线" + storage.LowQty.ToString() + storage.Item.MinUnit + "，是否继续开立！\r\n\r\n如有疑问请联系药剂科！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.No)
                            {
                                return -1;
                            }
                        }
                    }
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
        /// 检验限制的收费数量
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private int CheckFeeQty(FS.HISFC.Models.RADT.Patient regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder, FS.HISFC.Models.Base.ServiceTypes type)
        {
            //限制数量
            int limitCount = 1;

            if (alLimitItem == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                alLimitItem = inteMgr.GetConstantList("LimitItem");
                limitItemHelper = new FS.FrameWork.Public.ObjectHelper(alLimitItem);
            }

            FS.HISFC.BizProcess.Integrate.Fee inteFeeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
            ArrayList alFee = inteFeeMgr.QueryAllFeeItemListsByClinicNO(regObj.ID, "ALL", "ALL", "ALL");

            if (alLimitItem != null)
            {
                Hashtable hsLimitItem = new Hashtable();

                //用来限制修改医嘱时候的错误提示
                string moOrder = "";

                foreach (FS.HISFC.Models.Base.Const conObj in alLimitItem)
                {
                    if (!conObj.IsValid)
                    {
                        continue;
                    }

                    try
                    {
                        if (!string.IsNullOrEmpty(conObj.Memo))
                        {
                            limitCount = FS.FrameWork.Function.NConvert.ToInt32(conObj.Memo);
                        }
                    }
                    catch
                    {
                        limitCount = 1;
                    }

                    int count = 0;

                    //查询当前医嘱
                    foreach (FS.HISFC.Models.Order.OutPatient.Order outOrder in alOrder)
                    {
                        if (outOrder.Item.ID == conObj.ID.Trim())
                        {
                            count += 1;
                            moOrder += "|" + outOrder.ID + "|";
                        }
                    }

                    //查询已收费信息
                    if (alFee != null)
                    {
                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList outFee in alFee)
                        {
                            if (outFee.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid)
                            {
                                if (outFee.Item.ID == conObj.ID.Trim()
                                    && !moOrder.Contains("|" + outFee.Order.ID + "|"))
                                {
                                    count += 1;
                                }
                            }
                        }
                    }

                    if (count > limitCount)
                    {
                        hsLimitItem.Add(conObj.Name, count);
                    }
                }

                //错误提示
                if (hsLimitItem.Keys.Count > 0)
                {
                    errInfo = "以下项目超过限制收费数量：\r\n\r\n";

                    foreach (string limitInfo in hsLimitItem.Keys)
                    {
                        errInfo += "【" + limitInfo + "】  收费数量 " + hsLimitItem[limitInfo].ToString() + "  限制数量 " + limitItemHelper.GetObjectFromName(limitInfo).Memo + "\r\n";
                    }
                    errInfo += "\r\n是否继续保存？";

                    if (MessageBox.Show(errInfo, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        errInfo = "";
                        return -1;
                    }

                    //if (notify == null)
                    //{
                    //    notify = new System.Windows.Forms.NotifyIcon();
                    //    notify.Icon = FS.SOC.Local.Order.Properties.Resources.HIS;
                    //}

                    //notify.Visible = true;
                    //notify.ShowBalloonTip(4, "禁用药警告", errInfo, System.Windows.Forms.ToolTipIcon.Warning);

                    //return -1;
                }
            }
            return 1;
        }
    }
}
