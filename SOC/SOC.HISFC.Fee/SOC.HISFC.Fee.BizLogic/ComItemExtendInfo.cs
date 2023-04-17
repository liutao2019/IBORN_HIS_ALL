using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.SOC.HISFC.Fee.BizLogic
{
    /// <summary>
    /// [功能描述: 基础项目信息扩展维护]<br></br>
    /// [创 建 者: xaingf]<br></br>
    /// [创建时间: 2012-05]<br></br>
    /// </summary>
    public class ComItemExtendInfo : FS.FrameWork.Management.Database
    {

        #region 插入、更新、删除操作

        /// <summary>
        /// 插入基础项目信息扩展表
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Insert(FS.SOC.HISFC.Fee.Models.ComItemExtend item)
        {
            string sql = null;
            //取Insert语句
            if (this.Sql.GetSql("ComItem.ExtendInfo.Insert", ref sql) == -1)
            {
                this.Err = "获得插入SQL语句ComItem.ExtendInfo.Insert出错!";

                return -1;
            }

            //格式化SQL语句
            try
            {
                //取参数列表
                string[] parms = this.getItemParams(item);
                //替换SQL语句中的参数。
                sql = string.Format(sql, parms);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新基础项目信息扩展表
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(FS.SOC.HISFC.Fee.Models.ComItemExtend item)
        {
            string sql = null;
            //取Update语句
            if (this.Sql.GetSql("ComItem.ExtendInfo.Update", ref sql) == -1)
            {
                this.Err = "获得更新SQL语句ComItem.ExtendInfo.Update出错!";

                return -1;
            }
            //格式化SQL语句
            try
            {
                //取参数列表
                string[] parms = this.getItemParams(item);
                //替换SQL语句中的参数。
                sql = string.Format(sql, parms);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 删除基础项目信息扩展表记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Delete(FS.SOC.HISFC.Fee.Models.ComItemExtend item)
        {
            string strSql = null;
            //取Delete语句
            if (this.Sql.GetSql("ComItem.ExtendInfo.Delete", ref strSql) == -1)
            {
                this.Err = "获得更新SQL语句ComItem.ExtendInfo.Delete出错!";

                return -1;
            }
            //格式化SQL语句
            try
            {
                strSql = string.Format(strSql, item.ItemCode);//根据项目编号删除记录
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 获得所有项目信息
        /// </summary>
        /// <returns>成功 所有项目信息, 失败 null</returns>
        public List<FS.SOC.HISFC.Fee.Models.ComItemExtend> QueryAllItemList()
        {
            string sql = null;
            //取SELECT语句
            if (this.Sql.GetSql("ComItem.ExtendInfo.Query", ref sql) == -1)
            {
                this.Err = "获得查询SQL语句ComItem.ExtendInfo.Query出错!";

                return null;
            }

            return this.query(string.Format(sql, "ALL"));
        }

        /// <summary>
        /// 获得所有项目信息
        /// </summary>
        /// <returns>成功 所有项目信息, 失败 null</returns>
        public List<FS.SOC.HISFC.Fee.Models.ComItemExtend> QueryItemListByItemCode(string itemCode)
        {
            string sql = null;
            //取SELECT语句
            if (this.Sql.GetSql("ComItem.ExtendInfo.Query", ref sql) == -1)
            {
                this.Err = "获得查询SQL语句ComItem.ExtendInfo.Query出错!";

                return null;
            }

            return this.query(string.Format(sql, itemCode));
        }

        /// 根据typeCode获得相应项目信息 
        /// </summary>
        /// <param name="itemCode">项目编号</param>
        /// <param name="typeCode">1为药品，2为非药品</param>
        /// <returns>成功 所有项目信息, 失败 null</returns>
        public List<FS.SOC.HISFC.Fee.Models.ComItemExtend> QueryItemListByItemCode(string itemCode, string typeCode)
        {
            string sql = null;
            //取SELECT语句
            if (this.Sql.GetSql("ComItem.ExtendInfo.QueryByItemCodeAndTypeCode", ref sql) == -1)
            {
                this.Err = "获得查询SQL语句ComItem.ExtendInfo.Query出错!";

                return null;
            }

            return this.QueryItemInfo(string.Format(sql, itemCode, typeCode));
        }

        #endregion

        #region 内置

        /// <summary>
        /// 根据sql获取项目信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<FS.SOC.HISFC.Fee.Models.ComItemExtend> query(string sql)
        {
            List<FS.SOC.HISFC.Fee.Models.ComItemExtend> items = new List<FS.SOC.HISFC.Fee.Models.ComItemExtend>(); //用于返回基础项目扩展信息的数组

            //执行当前Sql语句Undrug
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }

            if (this.Reader == null)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    FS.SOC.HISFC.Fee.Models.ComItemExtend item = new FS.SOC.HISFC.Fee.Models.ComItemExtend();
                    item.ItemCode = this.Reader[0].ToString();//项目编码
                    item.ItemName = this.Reader[1].ToString();//项目名称
                    item.TypeCode = this.Reader[2].ToString();//项目类别
                    item.ItemGrade = this.Reader[3].ToString();//项目等级
                    item.ProvinceFlag = this.Reader[4].ToString();//省限制
                    item.CityFlag = this.Reader[5].ToString();//市限制
                    item.AreaFlag = this.Reader[6].ToString();//区限制
                    item.SpePactFlag = this.Reader[7].ToString();//特约单位限制
                    item.ZFFlag = this.Reader[8].ToString();//自费项目
                    item.SynFlag = this.Reader[9].ToString();//同步标记
                    item.MlgFlag = this.Reader[10].ToString();//肿瘤用药标记
                    item.OperCode = this.Reader[11].ToString();//操作员代码
                    item.OperDate = this.Reader[12].ToString();//操作日期
                    items.Add(item);
                }//循环结束

                return items;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                //如果还没有关闭Reader 关闭之
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 根据sql获取项目信息，有类别和拼音码
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<FS.SOC.HISFC.Fee.Models.ComItemExtend> QueryItemInfo(string sql)
        {
            List<FS.SOC.HISFC.Fee.Models.ComItemExtend> items = new List<FS.SOC.HISFC.Fee.Models.ComItemExtend>(); //用于返回基础项目扩展信息的数组

            //执行当前Sql语句Undrug
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }

            if (this.Reader == null)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    FS.SOC.HISFC.Fee.Models.ComItemExtend item = new FS.SOC.HISFC.Fee.Models.ComItemExtend();
                    item.ItemCode = this.Reader[0].ToString();//项目编码
                    item.ItemName = this.Reader[1].ToString();//项目名称
                    item.Spell_code = this.Reader[2].ToString();//拼音码
                    item.Specs = this.Reader[3].ToString();//规格
                    item.TypeCode = this.Reader[4].ToString();//项目类别
                    item.ItemGrade = this.Reader[5].ToString();//项目等级
                    item.ProvinceFlag = this.Reader[6].ToString();//省限制
                    item.CityFlag = this.Reader[7].ToString();//市限制
                    item.AreaFlag = this.Reader[8].ToString();//区限制
                    item.SpePactFlag = this.Reader[9].ToString();//特约单位限制
                    item.ZFFlag = this.Reader[10].ToString();//自费项目
                    item.SynFlag = this.Reader[11].ToString();//同步标记
                    item.MlgFlag = this.Reader[12].ToString();//肿瘤用药标记
                    item.OperCode = this.Reader[13].ToString();//操作员代码
                    item.OperDate = this.Reader[14].ToString();//操作日期
                    items.Add(item);
                }//循环结束

                return items;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                //如果还没有关闭Reader 关闭之
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 获得update或者insert基础项目信息扩展表的传入参数数组
        /// </summary>
        /// <param name="undrug">非药品实体</param>
        /// <returns>参数数组</returns>
        private string[] getItemParams(FS.SOC.HISFC.Fee.Models.ComItemExtend item)
        {
            string[] args = 
			{	
				    item.ItemCode,//项目编码
                    item.ItemName,//项目名称
                    item.TypeCode,//项目类别
                    item.ItemGrade,//项目等级
                    item.ProvinceFlag,//省限制
                    item.CityFlag,//市限制
                    item.AreaFlag,//区限制
                    item.SpePactFlag,//特约单位限制
                    item.ZFFlag,//自费项目
                    item.SynFlag,//同步标记
                    item.MlgFlag,//肿瘤用药标记
                    item.OperCode,//操作员代码
                    item.OperDate,//操作日期
			};

            return args;
        }

        #endregion
    }
}
