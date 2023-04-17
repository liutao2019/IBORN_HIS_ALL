using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;
using Neusoft.HISFC.Models.Fee.Outpatient;
using System.Collections;

namespace Neusoft.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientInfomation
{
    public class YkHighChargeFee
    {
        Neusoft.HISFC.BizLogic.Fee.Outpatient outpatientManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();
        Neusoft.HISFC.BizLogic.Registration.RegLvlFee regLvlFeeMgr = new Neusoft.HISFC.BizLogic.Registration.RegLvlFee();

        /// <summary>
        /// 高收费差额项目
        /// </summary>
        private string roundFeeItemCode = "高收费项目";

        /// <summary>
        /// 获取高收费项目信息
        /// </summary>
        /// <returns></returns>
        private DataRow GetFeeItemInfo()
        {
            DataSet dsItemNow = new DataSet();
            int iReturn = this.outpatientManager.QueryItemList("undrug", roundFeeItemCode, ref dsItemNow);
            if (iReturn == -1)
            {
                CommonController.Instance.MessageBox("获得高收费项目出错!" + this.outpatientManager.Err, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            DataRow[] rowFindsNow = dsItemNow.Tables[0].Select("ITEM_CODE = " + "'" + roundFeeItemCode + "' and drug_flag = '0'");

            if (rowFindsNow == null || rowFindsNow.Length == 0)
            {
                CommonController.Instance.MessageBox("编码为: [" + this.roundFeeItemCode + " ] 的项目查找失败!", System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }

            return rowFindsNow[0];
        }

        /// <summary>
        /// 转换高收费部分的收费信息
        /// </summary>
        /// <param name="oldFeeItemList"></param>
        /// <returns></returns>
        private ArrayList GetFeeItemList(FeeItemList oldFeeItemList, DataRow newItem)
        {
            ArrayList alItemList = new ArrayList();
            //高收费部分
            #region 高收项目
            FeeItemList newHighFeeItemList = oldFeeItemList.Clone();
            newHighFeeItemList.Item.ID = newItem["ITEM_CODE"].ToString();
            newHighFeeItemList.Item.Name = newItem["ITEM_NAME"].ToString();
            //最小费用ID
            newHighFeeItemList.Item.MinFee.ID = newItem["FEE_CODE"].ToString();
            newHighFeeItemList.Item.SysClass.ID = newItem["SYS_CLASS"].ToString();

            //价格转变
            newHighFeeItemList.Item.Price = newHighFeeItemList.Item.Price - newHighFeeItemList.OrgPrice;
            newHighFeeItemList.OrgPrice = newHighFeeItemList.Item.Price;
            newHighFeeItemList.SpecialPrice = newHighFeeItemList.Item.Price;
            //数量为1
            newHighFeeItemList.Item.Qty = 1;
            newHighFeeItemList.Days = 1;
            newHighFeeItemList.NoBackQty = 1;
            //规格
            newHighFeeItemList.Item.UserCode = newItem["User_Code"].ToString();
            newHighFeeItemList.Item.Specs = newItem["SPECS"].ToString();
            //包装数量，非药品，组合项目为1
            newHighFeeItemList.Item.PackQty = Neusoft.FrameWork.Function.NConvert.ToDecimal(newItem["PACK_QTY"].ToString());
            string minUnit = newItem["MIN_UNIT"].ToString();
            newHighFeeItemList.Item.PriceUnit = string.IsNullOrEmpty(minUnit) ? "次" : minUnit;
            newHighFeeItemList.FeePack = "0";
            //开方科室不变
            //执行科室，是否需要改变？

            //计算金额
            newHighFeeItemList.FT.TotCost = Neusoft.FrameWork.Public.String.FormatNumber(newHighFeeItemList.Item.Price * newHighFeeItemList.Item.Qty / newHighFeeItemList.Item.PackQty, 2);
            newHighFeeItemList.FT.PayCost = 0;
            newHighFeeItemList.FT.PubCost = 0;
            newHighFeeItemList.FT.OwnCost = newHighFeeItemList.FT.TotCost;
            newHighFeeItemList.FT.RebateCost = 0;
            #endregion

            alItemList.Add(newHighFeeItemList);

            #region 原始项目
            //将原来金额进行转换
            oldFeeItemList.Item.Price = oldFeeItemList.OrgPrice;
            //计算金额
            oldFeeItemList.FT.TotCost = Neusoft.FrameWork.Public.String.FormatNumber(oldFeeItemList.Item.Price * oldFeeItemList.Item.Qty / oldFeeItemList.Item.PackQty, 2);
            oldFeeItemList.FT.PayCost = 0;
            oldFeeItemList.FT.PubCost = 0;
            oldFeeItemList.FT.OwnCost = oldFeeItemList.FT.TotCost;
            oldFeeItemList.FT.RebateCost = 0;
            #endregion

            alItemList.Add(oldFeeItemList);

            return alItemList;

        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public ArrayList GetYkFeeItemList(ArrayList feeDetails)
        {
            //判断登陆科室是否宜康病区
            Neusoft.HISFC.Models.Base.Employee employee = this.outpatientManager.Operator as Neusoft.HISFC.Models.Base.Employee;
            if (Function.IsContainYKDept(employee.Dept.ID))
            {
                ArrayList alFeeItemList = new ArrayList();
                DataRow dr = this.GetFeeItemInfo();
                if (dr == null)
                {
                    return feeDetails;
                }
                //拆分价格
                foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in feeDetails)
                {
                    if (feeItemList.Item.Price > feeItemList.OrgPrice)//如果当前价格大于原始正常价格，则进行拆分
                    {
                        alFeeItemList.AddRange(this.GetFeeItemList(feeItemList, dr));
                    }
                    else
                    {
                        alFeeItemList.Add(feeItemList);
                    }
                }

                return alFeeItemList;
            }
            else
            {
                return feeDetails;
            }

        }
    }
}
