using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace SOC.HISFC.Components.DrugStoreByInvoice.OutPatient.PrescriptionComment
{
   public class Function:FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获得门诊摆药处方(处方调剂)信息
        /// </summary>
        /// <param name="strSQL">查询的SQl语句</param>
        /// <returns>成功返回数组 失败返回null</returns>
        protected ArrayList myGetDrugRecipeInfo(string strSQL)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获取门诊处方调剂信息出错" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.DrugRecipe info;
                while (this.Reader.Read())
                {
                    #region 由结果集内读取数据
                    info = new FS.HISFC.Models.Pharmacy.DrugRecipe();

                    info.StockDept.ID = this.Reader[0].ToString();						//药房编码
                    info.RecipeNO = this.Reader[1].ToString();							//处方号
                    info.SystemType = this.Reader[2].ToString();						//出库申请分类
                    info.TransType = this.Reader[3].ToString();							//交易类型,1正交易，2反交易
                    info.RecipeState = this.Reader[4].ToString();						//处方状态: 0申请,1打印,2配药,3发药,4还药(当天未发的药品返回货价)
                    info.ClinicNO = this.Reader[5].ToString();						//门诊号
                    info.CardNO = this.Reader[6].ToString();							//病历卡号
                    info.PatientName = this.Reader[7].ToString();						//患者姓名
                    info.Sex.ID = this.Reader[8].ToString();							//性别
                    info.Age = NConvert.ToDateTime(this.Reader[9].ToString());			//年龄
                    info.PayKind.ID = this.Reader[10].ToString();						//结算类别代码
                    info.PatientDept.ID = this.Reader[11].ToString();					//患者科室编码
                    info.RegTime = NConvert.ToDateTime(this.Reader[12].ToString());		//挂号日期
                    info.Doct.ID = this.Reader[13].ToString();							//开方医师
                    info.DoctDept.ID = this.Reader[14].ToString();						//开方医师所在科室
                    info.DrugTerminal.ID = this.Reader[15].ToString();					//配药终端（打印台）
                    info.SendTerminal.ID = this.Reader[16].ToString();					//发药终端（发药窗口）
                    info.FeeOper.ID = this.Reader[17].ToString();							//收费人编码(申请人编码)
                    info.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[18].ToString());		//收费时间(申请时间)
                    info.InvoiceNO = this.Reader[19].ToString();						//票据号
                    info.Cost = NConvert.ToDecimal(this.Reader[20].ToString());			//处方金额（零售金额）
                    info.RecipeQty = NConvert.ToDecimal(this.Reader[21].ToString());	//处方中药品数量(中山一用品种数)
                    info.DrugedQty = NConvert.ToDecimal(this.Reader[22].ToString());	//已配药的药品数量(中山一用品种数)
                    info.DrugedOper.ID = this.Reader[23].ToString();						//配药人
                    info.StockDept.ID = this.Reader[24].ToString();					    //配药科室
                    info.DrugedOper.OperTime = NConvert.ToDateTime(this.Reader[25].ToString());	//配药日期
                    info.SendOper.ID = this.Reader[26].ToString();							//发药人
                    info.SendOper.OperTime = NConvert.ToDateTime(this.Reader[27].ToString());	//发药时间
                    info.StockDept.ID = this.Reader[28].ToString();						//发药科室

                    info.ValidState = (FS.HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[29]));					//有效状态：0有效，1无效 2 发药后退费
                    info.IsModify = NConvert.ToBoolean(this.Reader[30].ToString());						//退药改药0否1是

                    info.BackOper.ID = this.Reader[31].ToString();							//-还药人
                    info.BackOper.OperTime = NConvert.ToDateTime(this.Reader[32].ToString());	//还药时间
                    info.CancelOper.ID = this.Reader[33].ToString();						//取消操作员
                    info.CancelOper.OperTime = NConvert.ToDateTime(this.Reader[34].ToString());	//取消日期
                    info.Memo = this.Reader[35].ToString();								//备注
                    info.SumDays = NConvert.ToDecimal(this.Reader[36].ToString());

                    al.Add(info);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取门诊处方调剂信息出错，执行SQL语句出错" + ex.Message;
                this.ErrCode = ex.ToString();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        public ArrayList QueryDrugRecipe(string deptCode, string class3MeaningCode, string recipeState, DateTime beginTime, DateTime endTime, int billType, string billNo,string doctCode)
        {
            string strSqlSelect = "", strSqlWhere = "";
            string strWhereIndex = "";				//SQL语句Where条件 索引
            if (this.Sql.GetSql("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            switch (billType)
            {
                case 0:			//处方号
                    strWhereIndex = "Pharmacy.DrugStore.GetList.Where6";
                    break;
                case 1:			//发票号
                    strWhereIndex = "Pharmacy.DrugStore.GetList.Where7";
                    break;
                default:		//病历卡号
                    strWhereIndex = "Pharmacy.DrugStore.GetList.Where8";
                    break;
            }
            if (this.Sql.GetSql(strWhereIndex, ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, class3MeaningCode, billNo, recipeState, beginTime, endTime, doctCode);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            if (al == null)
                return null;
            return al;
        }
    }
}
