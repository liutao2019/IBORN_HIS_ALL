using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace SOC.Local.Order.IBeforeAddItem.NanZhuang
{
    /// <summary>
    /// 开立项目前接口
    /// 当药品库存低于警戒线时，限制医生继续开立该药品
    /// </summary>
    public class BeforeAddItemInterface : Neusoft.HISFC.BizProcess.Interface.Order.IBeforeAddItem
    {
        #region IBeforeAddItem 成员

        /// <summary>
        /// 错误信息
        /// </summary>
        string err = "";

        /// <summary>
        /// 错误信息
        /// </summary>
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

        /// <summary>
        /// 住院开立判断
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnBeforeAddItemForInPatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder)
        {
            //return this.CheckOrder(patientInfo, reciptDept, reciptDoct, alOrder, Neusoft.HISFC.Models.Base.ServiceTypes.I);
            return 1;
        }



        System.Windows.Forms.NotifyIcon notify = null;

        /// <summary>
        /// 药品管理
        /// </summary>
        Neusoft.HISFC.BizLogic.Pharmacy.Item itemMgr = new Neusoft.HISFC.BizLogic.Pharmacy.Item();

        Neusoft.HISFC.BizLogic.Pharmacy.Constant phaConMgr = new Neusoft.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// 取药科室列表
        /// </summary>
        List<Neusoft.FrameWork.Models.NeuObject> alStockDept = null;

        /// <summary>
        /// 门诊开立判断
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int OnBeforeAddItemForOutPatient(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder)
        {
            return this.CheckOrder(regObj, reciptDept, reciptDoct, alOrder, Neusoft.HISFC.Models.Base.ServiceTypes.C);
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
        public int CheckOrder(Neusoft.HISFC.Models.RADT.Patient regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder, Neusoft.HISFC.Models.Base.ServiceTypes type)
        {
            try
            {
                Neusoft.FrameWork.Models.NeuObject stockDept = null;
                Neusoft.HISFC.Models.Pharmacy.Storage storage = null;
                foreach (Neusoft.HISFC.Models.Order.Order order in alOrder)
                {
                    if(order.Item.ItemType!=Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        continue;
                    }

                    //没有取药科室信息就重新获取一个
                    if (string.IsNullOrEmpty(order.StockDept.ID))
                    {
                        alStockDept = phaConMgr.GetRecipeDrugDept(reciptDept.ID, ((Neusoft.HISFC.Models.Pharmacy.Item)order.Item).Type.ID);
                        if (alStockDept != null && alStockDept.Count > 0)
                        {
                            order.StockDept.ID = alStockDept[0].ID;
                        }
                    }

                    //if (!string.IsNullOrEmpty(order.StockDept.ID))
                    //{
                    //    if (!itemMgr.GetWarnDrugStock(order.StockDept.ID, order.Item.ID))
                    //    {
                    //        if (MessageBox.Show("【" + order.Item.Name + "】已超过库存警戒线，是否继续开立！\r\n\r\n如有疑问请联系药剂科！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.No)
                    //        {
                    //            return -1;
                    //        }
                    //    }
                    //}
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
                err = ex.Message;
                return -1;
            }

            return 1;
        }
    }
}
