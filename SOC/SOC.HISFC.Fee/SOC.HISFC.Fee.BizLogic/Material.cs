using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.BizLogic
{
    public class Material : FS.FrameWork.Management.Database
    {

        /// <summary>
        /// 根据字典编码获得字典实体
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public FS.SOC.HISFC.Fee.Models.MatBase GetBaseInfoByID(string itemCode)
        {
            string sql = "";
            if (this.Sql.GetSql("Mat.Base.BaseInfo.Select", ref sql) == -1)
            {
                this.Err = "没有找到id为Mat.Base.BaseInfo.Select的sql语句!";
                return null;
            }
            string sqlWhere = "";
            if (this.Sql.GetSql("Mat.Base.BaseInfo.Where.ByItemCode", ref sqlWhere) == -1)
            {
                this.Err = "没有找到id为Mat.Base.BaseInfo.Where.ByItemCode的sql语句!";
                return null;
            }
            try
            {
                sqlWhere = string.Format(sqlWhere, itemCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化Mat.Base.BaseInfo.Where.ByItemCode时出错：" + ex.Message;
                return null;
            }
            List<FS.SOC.HISFC.Fee.Models.MatBase> matBaseList = this.MyGetBaseListBySql(sql + sqlWhere);
            if (matBaseList == null)
            {
                return null;
            }
            if (matBaseList.Count == 0)
            {
                this.Err = "通过编码[" + itemCode + "]没有找到字典信息";
                return null;
            }
            return matBaseList[0];
        }

        /// <summary>
        /// 根据sql语句返回物资字典实体列表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<FS.SOC.HISFC.Fee.Models.MatBase> MyGetBaseListBySql(string sql)
        {
            List<FS.SOC.HISFC.Fee.Models.MatBase> matBaseList = new List<FS.SOC.HISFC.Fee.Models.MatBase>();
            FS.SOC.HISFC.Fee.Models.MatBase matBase;
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "获得物资字典信息时，执行SQL语句出错！" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    matBase = new FS.SOC.HISFC.Fee.Models.MatBase();

                    matBase.ID = this.Reader[0].ToString();  //物品编码
                  //  matBase.Kind.ID = this.Reader[1].ToString();  //物品科目编码
                    matBase.Storage.ID = this.Reader[2].ToString();  //仓库代码
                    matBase.Name = this.Reader[3].ToString();  //物品名称
                    matBase.SpellCode = this.Reader[4].ToString();  //拼音编码
                    matBase.WBCode = this.Reader[5].ToString();  //五笔码
                    matBase.UserCode = this.Reader[6].ToString();  //自定义码
                    matBase.GBCode = this.Reader[7].ToString();  //国家编码
                    matBase.OtherName.Name = this.Reader[8].ToString();  //别名
                    matBase.OtherName.SpellCode = this.Reader[9].ToString();  //别名拼音码
                    matBase.OtherName.WBCode = this.Reader[10].ToString();  //别名五笔码
                    matBase.OtherName.UserCode = this.Reader[11].ToString();  //别名自定义码
                   // matBase.EffectArea = (BizLogic.EnumEffectArea)(NConvert.ToInt32(this.Reader[12].ToString()));  //有效范围(0-本科室,1-本科室及下级科室,2-全院,3-指定科室)
                    matBase.EffectDept = this.Reader[13].ToString();  //有效科室(EFFECT_AREA=3时有效)
                    matBase.Specs = this.Reader[14].ToString();  //规格
                    matBase.MinUnit = this.Reader[15].ToString();  //最小单位
                    matBase.InPrice = FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());  //最新入库单价(包装单位)
                    matBase.SalePrice = FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());  //零售价格
                    matBase.Price = FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());  //零售价格-物品基类
                    matBase.PackUnit = this.Reader[18].ToString();  //大包装单位
                    matBase.PackQty = FrameWork.Function.NConvert.ToDecimal(this.Reader[19].ToString());  //大包装数量
                    matBase.PackPrice = FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());  //大包装价格
                  //  matBase.AddRateType = (BizLogic.EnumAddRateType)FrameWork.Function.NConvert.ToInt32(this.Reader[21].ToString());  //加价规则、用于入库自动加价
                    matBase.MinFee.ID = this.Reader[22].ToString();  //最小费用代码
                    matBase.FeeCode = this.Reader[22].ToString();  //最小费用代码--基类
                    matBase.IsFee = FrameWork.Function.NConvert.ToBoolean(this.Reader[23].ToString());  //财务收费标志(0－否,1－是)
                    matBase.IsValid = FrameWork.Function.NConvert.ToBoolean(this.Reader[24].ToString());  //停用标记(0－停用,1－使用)
                    matBase.IsSpecial = FrameWork.Function.NConvert.ToBoolean(this.Reader[25].ToString());  //特殊材料标志
                    matBase.IsHighValue = FrameWork.Function.NConvert.ToBoolean(this.Reader[26].ToString());  //高值耗材标志
                    //matBase.Factory.ID = this.Reader[27].ToString();  //生产厂家
                    //matBase.Company.ID = this.Reader[28].ToString();  //供货公司
                    matBase.InSource = this.Reader[29].ToString();  //来源
                    matBase.Usage = this.Reader[30].ToString();  //用途
                    matBase.ApproveInfo = this.Reader[31].ToString();  //批文信息
                    matBase.Mader = this.Reader[32].ToString();  //生产者
                    matBase.RegisterCode = this.Reader[33].ToString();  //注册号
                    matBase.SpecialType = this.Reader[34].ToString();  //特殊类别
                    matBase.RegisterDate = FrameWork.Function.NConvert.ToDateTime(this.Reader[35].ToString());  //注册时间
                    matBase.OverDate = FrameWork.Function.NConvert.ToDateTime(this.Reader[36].ToString());  //到期时间
                    matBase.IsPack = FrameWork.Function.NConvert.ToBoolean(this.Reader[37].ToString());  //是否打包-供应室用(1是0否)
                    matBase.IsExamine = FrameWork.Function.NConvert.ToBoolean(this.Reader[38].ToString());  //财务审核标记
                    matBase.IsNoRecycle = FrameWork.Function.NConvert.ToBoolean(this.Reader[39].ToString());  //是否一次性耗材-供应室用(1是0否)
                    matBase.Memo = this.Reader[40].ToString();  //备注
                    matBase.Oper.ID = this.Reader[41].ToString();  //操作员
                    matBase.Oper.OperTime = FrameWork.Function.NConvert.ToDateTime(this.Reader[42].ToString());  //操作日期
                    //{5E811F39-FCA7-4bbf-B2E0-62AD5D499D35}
                    matBase.IsNeedBatchNo = FrameWork.Function.NConvert.ToBoolean(this.Reader[43].ToString());//是否按批次管理
                    //{7A8734DD-78FB-40ec-817E-8964CE065D90}
                    matBase.IsPlan = FrameWork.Function.NConvert.ToBoolean(this.Reader[44].ToString());// 是否按月计划入库
                    matBase.IsB = FrameWork.Function.NConvert.ToBoolean(this.Reader[45].ToString());
                    matBase.RegisterNo = this.Reader[46].ToString(); //物品注册号
                    //add by chbf 2011-11-22
                   // matBase.Kind.Name = this.Reader[47].ToString();//物品分类
                    matBaseList.Add(matBase);
                }
                return matBaseList;
            }
            catch (Exception ex)
            {
                this.Err = "获得物资字典信息时,由Reader内读取信息发生异常 \n" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }
    }
}
