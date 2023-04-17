using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.BizLogic
{  
    /// <summary>
    /// [功能描述: 非药品收费业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class ItemList:FS.FrameWork.Management.Database
    {
        #region 身份变更

        /// <summary>
        /// 插入当前患者流水号所有有效非药品费用的负记录和正记录
        /// （可以用于身份变更）
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="pactInfo"></param>
        /// <returns></returns>
        public int InsertAllOldAndNewFee(string inpatientNO, FS.HISFC.Models.Base.PactInfo pactInfo)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractItemList.Current.InsertOldFee, inpatientNO, this.Operator.ID, pactInfo.PayKind.ID, pactInfo.ID);
            if (this.ExecNoQuery(sql) >= 0)
            {
                sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractItemList.Current.InsertNewFee, inpatientNO, this.Operator.ID, pactInfo.PayKind.ID, pactInfo.ID);

                return this.ExecNoQuery(sql);
            }
            return -1;
        }

        /// <summary>
        /// 更新可退数量（更新原合同单位所有非药品费用的可退数量=0）
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public int UpdateAllOldFeeNoBackNum(string inpatientNO, FS.HISFC.Models.Base.PactInfo oldPactInfo)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractItemList.Current.UpdateNoBackNumByInPatientNO, inpatientNO,oldPactInfo.ID);

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新所有新非药品费用的费用来源（FTSource.Source3=1）
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public int UpdateAllNewFeeFTSource(string inpatientNO)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractItemList.Current.UpdateFTSource, inpatientNO);

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新费用信息（只更新金额）
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        public int UpdateAllNewFeeFTCost(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractItemList.Current.UpdateFTCost, feeItemList.ExtCode, feeItemList.RecipeNO, feeItemList.SequenceNO, (int)feeItemList.TransType, feeItemList.FT.TotCost, feeItemList.FT.OwnCost, feeItemList.FT.PayCost, feeItemList.FT.PubCost, feeItemList.FT.RebateCost);

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 查询所有的费用信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> QueryAllNewFee(string inpatientNO)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractItemList.Current.SelectAllNewFee, inpatientNO);


            if (this.ExecQuery(sql) < 0)
            {
                this.WriteErr();
                return null;
            }

            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> list = null;
            if (this.Reader != null)
            {
                list = new List<FS.HISFC.Models.Fee.Inpatient.FeeItemList>();
                FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = null;
                while (this.Reader.Read())
                {
                    feeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    feeItemList.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                    feeItemList.ExtCode = this.Reader[0].ToString();//旧处方号
                    feeItemList.RecipeNO = this.Reader[1].ToString();
                    feeItemList.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                    feeItemList.TransType = FS.FrameWork.Public.EnumHelper.Current.GetEnum<FS.HISFC.Models.Base.TransTypes>(this.Reader[3].ToString());
                    feeItemList.Patient.ID = this.Reader[4].ToString();
                    feeItemList.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    feeItemList.Item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    feeItemList.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                    feeItemList.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                    feeItemList.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                    feeItemList.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                    feeItemList.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                    feeItemList.FT.RebateCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                    feeItemList.PayType = FS.FrameWork.Public.EnumHelper.Current.GetEnum<FS.HISFC.Models.Base.PayTypes>(this.Reader[13].ToString());
                    list.Add(feeItemList);
                }
            }
            if (this.Reader != null && !this.Reader.IsClosed)
            {
                this.Reader.Close();
            }

            return list;
        }

        #endregion

        #region 拆分费用

        /// <summary>
        /// 判断是否存在未拆分的费用
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public bool IsExistNoSplitFeeInfo(string inpatientNO, DateTime beginTime, DateTime endTime)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractItemList.Current.SelectNoSplitFeeItemListCount, inpatientNO, beginTime, endTime);

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql, "0"))>0;
        }

        #endregion
    }
}
