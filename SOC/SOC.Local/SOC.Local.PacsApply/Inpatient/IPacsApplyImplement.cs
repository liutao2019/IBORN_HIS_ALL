using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.PacsApply.Inpatient
{
    /// <summary>
    /// 电子申请单默认实现
    /// </summary>
    class IPacsApplyImplement : FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInpateintPacsApply
    {
        /// <summary>
        /// 申请单
        /// </summary>
        //protected static FS.ApplyInterface.HisInterface PACSApplyInterface = null;

        #region IInpateintPacsApply 成员

        /// <summary>
        /// 删除申请单
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alInOrder"></param>
        /// <returns></returns>
        public int Delete(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order == null )
            {
                this.errInfo = "没有传入有效的医嘱！";
                return -1;
            }

            if (order.Status != 3 && (order.ApplyNo != null && order.ApplyNo != ""))
            {
                //if (PACSApplyInterface == null)
                //{
                //    if (this.Init(patientInfo) < 0)
                //    {
                //        this.errInfo = "初始化电子申请单接口时出错！";
                //        return -1;
                //    }
                //}
                //PACSApplyInterface.DeleteApply(order.ApplyNo);
            }
            return 1;
        }

        /// <summary>
        /// 编辑申请单
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alInOrder"></param>
        /// <returns></returns>
        public int Edit(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (order == null)
            {
                this.errInfo = "没有传入有效的医嘱！";
                return -1;
            }

            //if (PACSApplyInterface == null)
            //{
            //    if (this.Init(patientInfo) < 0)
            //    {
            //        this.errInfo = "初始化电子申请单接口时出错！";
            //        return -1;
            //    }
            //}
            FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
            order = orderMgr.QueryOneOrder(order.ID);

            string applyNo = order.ApplyNo;

            if (applyNo != string.Empty && applyNo != null)
            {
                try
                {

                    //{014680EC-6381-408b-98FB-A549DAA49B82}
                    //PACSApplyInterface.UpdateApply(applyNo);
                    //PACSApplyInterface.UpdateApply(applyNo,false);
                }
                catch (Exception ex)
                {
                    errInfo =ex.Message;
                    return -1;
                }
            }
            else
            {
                FS.HISFC.Models.Base.Item hisItem = order.Item as FS.HISFC.Models.Base.Item;
                //bool compare = PACSApplyInterface.IsCompareItem(hisItem.ID, hisItem.NationCode);

                //if (compare)
                //{
                //    //{014680EC-6381-408b-98FB-A549DAA49B82}
                //    //PACSApplyInterface.SaveApplysGComboNo(patientInfo.ID, 1, order.Combo.ID);
                //    PACSApplyInterface.SaveApplysGComboNo(patientInfo.ID, 1, order.Combo.ID,false);
                //}
                //else
                {
                    errInfo = order.Item.Name + "没有维护申请单对照，可能开错项目，请联系信息科！";
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = "";

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
        /// 初始化
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int Init(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                //PACSApplyInterface = new FS.ApplyInterface.HisInterface();

                return 1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int Quit(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            return 1;
        }

        /// <summary>
        /// 保存申请单
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="alInOrder"></param>
        /// <returns></returns>
        public int Save(FS.HISFC.Models.RADT.PatientInfo patientInfo, System.Collections.ArrayList alInOrder)
        {
            return 1;
            //中大六院保存的时候不弹出，要自己去点编辑。
            //if (PACSApplyInterface == null)
            //{
            //    if (this.Init(patientInfo) < 0)
            //    {
            //        this.errInfo = "初始化电子申请单接口时出错！";
            //        return -1;
            //    }
            //}
            //PACSApplyInterface.SaveApplysG(patientInfo.ID, 1);
            //return 1;
        }

        #endregion
    }
}
