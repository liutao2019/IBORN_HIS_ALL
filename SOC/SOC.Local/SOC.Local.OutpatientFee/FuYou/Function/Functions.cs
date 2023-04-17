﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.FuYou.Function
{
    public class Functions : FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff
    {
        public Functions()
        {

        }

        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// 四舍五入费费用编码{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
        /// </summary>
        protected string roundFeeItemCode = string.Empty;
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #region IOutPatientFeeRoundOff 成员

        void FS.HISFC.BizProcess.Interface.FeeInterface.IOutPatientFeeRoundOff.OutPatientFeeRoundOff(FS.HISFC.Models.Registration.Register r, ref decimal totCost, ref FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList, string recipeSequence)
        {
            //四舍五入费费用代码{DE54BEAE-EF40-4aa4-8DF5-8CCB2A3DDA1D}
            this.roundFeeItemCode = this.controlParamIntegrate.GetControlParam<string>("MZ9926", true, string.Empty);

            decimal totCostOld = totCost;

            if (r.Pact.PayKind.ID == "01" || r.Pact.PayKind.ID == "03")
            {
                string s = "0" + totCost.ToString().Substring(totCost.ToString().IndexOf('.'));
                decimal dTotCost = FS.FrameWork.Function.NConvert.ToDecimal(s);
                if (dTotCost > 0 && dTotCost < 0.40m)
                {
                    dTotCost = 0.00m;
                }
                else if (dTotCost >= 0.40m && dTotCost < 0.80m)
                {
                    dTotCost = 0.50m;
                }
                else if (dTotCost >= 0.80m && dTotCost < 1.00m)
                {
                    dTotCost = 1.00m;
                }
                totCost = Math.Truncate(totCost) + dTotCost;
            }
            else if (r.Pact.PayKind.ID == "02")
            {
                totCost = FS.FrameWork.Function.NConvert.ToDecimal(totCost.ToString("F2"));
            }
            decimal quantity = totCost - totCostOld;
            if (quantity != 0)
            {
                DataSet dsItemNow = new DataSet();
                int iReturn = this.outpatientManager.QueryItemList("undrug", this.roundFeeItemCode, ref dsItemNow);
                if (iReturn == -1)
                {
                    MessageBox.Show("获得项目出错!" + this.outpatientManager.Err);
                    return;
                }
                DataRow findRowNow;
                DataRow[] rowFindsNow = dsItemNow.Tables[0].Select("ITEM_CODE = " + "'" + this.roundFeeItemCode + "' and drug_flag = '0'");

                if (rowFindsNow == null || rowFindsNow.Length == 0)
                {
                    MessageBox.Show("编码为: [" + this.roundFeeItemCode + " ] 的项目查找失败!");

                    return;
                }

                findRowNow = rowFindsNow[0];
                //项目基本信息实体
                feeItemList.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                feeItemList.Item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                feeItemList.IsGroup = false;
                feeItemList.Item.ID = this.roundFeeItemCode;
                feeItemList.ID = this.roundFeeItemCode;
                feeItemList.Item.UserCode = findRowNow["User_Code"].ToString();
                feeItemList.Item.Specs = findRowNow["SPECS"].ToString();
                feeItemList.Item.Name = findRowNow["ITEM_NAME"].ToString();
                feeItemList.Name = feeItemList.Item.Name;
                feeItemList.Days = 1;
                feeItemList.Item.SysClass.ID = findRowNow["SYS_CLASS"].ToString();
                feeItemList.Order.Sample.Name = findRowNow["DEFAULT_SAMPLE"].ToString();//样本
                //是否需要预约
                feeItemList.Item.IsNeedBespeak = FS.FrameWork.Function.NConvert.ToBoolean(findRowNow["NEEDBESPEAK"].ToString());
                if (r.DoctorInfo.Templet.Doct.ID == null || r.DoctorInfo.Templet.Doct.ID == string.Empty)
                {
                    if (r.SeeDoct.ID != null && r.SeeDoct.ID != string.Empty)
                    {
                        feeItemList.RecipeOper.ID = r.SeeDoct.ID;
                        feeItemList.RecipeOper.Name = r.SeeDoct.Name;
                    }
                    else
                    {
                        feeItemList.RecipeOper.ID = r.DoctorInfo.Templet.Doct.ID;
                        feeItemList.RecipeOper.Name = r.DoctorInfo.Templet.Doct.Name;
                    }
                }
                else
                {
                    feeItemList.RecipeOper.ID = r.DoctorInfo.Templet.Doct.ID;
                    feeItemList.RecipeOper.Name = r.DoctorInfo.Templet.Doct.Name;
                }
                //{33607355-C383-4271-B46C-0FBBAC251382} 开方医生所属科室编码
                feeItemList.RecipeOper.Dept.ID = r.DoctorInfo.Templet.Dept.ID;
                feeItemList.RecipeOper.Dept.Name = r.DoctorInfo.Templet.Dept.Name;
                feeItemList.RecipeOper.Dept.User01 = r.DoctorInfo.Templet.Doct.User01;
                feeItemList.FTSource = "0";//收费员自己收费
                feeItemList.ExecOper.Dept.ID = r.DoctorInfo.Templet.Dept.ID;
                feeItemList.ExecOper.Dept.Name = r.DoctorInfo.Templet.Dept.Name;
                //包装数量，非药品，组合项目为1
                decimal pactQty = FS.FrameWork.Function.NConvert.ToDecimal(findRowNow["PACK_QTY"].ToString());
                feeItemList.Item.PackQty = pactQty;
                string minUnit = findRowNow["MIN_UNIT"].ToString();
                if (minUnit == string.Empty)
                {
                    minUnit = "次";
                }
                feeItemList.Item.PriceUnit = minUnit;
                feeItemList.FeePack = "0";
                feeItemList.Item.Qty = quantity;
                //包装单位单价，保留4位小数
                decimal price = FS.FrameWork.Function.NConvert.ToDecimal(findRowNow["UNIT_PRICE"].ToString());
                price = FS.FrameWork.Public.String.FormatNumber(price, 4);
                feeItemList.Item.Price = price;
                feeItemList.SpecialPrice = price;
                feeItemList.Item.MinFee.ID = findRowNow["FEE_CODE"].ToString();
                feeItemList.RecipeSequence = recipeSequence;
                feeItemList.Patient = r.Clone();
                feeItemList.OrgItemRate = 1;
                feeItemList.NewItemRate = 1;
                feeItemList.ItemRateFlag = "1";
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                feeItemList.FT.PayCost = 0;
                feeItemList.FT.PubCost = 0;
                feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                feeItemList.FT.RebateCost = 0;
                feeItemList.NoBackQty = Math.Abs(feeItemList.Item.Qty);
            }
        }

        #endregion
    }
}
