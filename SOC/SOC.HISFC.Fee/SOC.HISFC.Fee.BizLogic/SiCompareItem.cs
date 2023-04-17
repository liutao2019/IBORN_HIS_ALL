using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Fee.BizLogic
{
    /// <summary>
    /// [功能描述:医保对照项目比例SOC业务层，从核心版本独立出来，只对SOC层有效]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class SiCompareItem : FS.FrameWork.Management.Database
    {

        private object[] getParams(FS.SOC.HISFC.Fee.Models.CompareItemModel info)
        {
            return new Object[] { 
                   info.PactCode, 
                    info.HisCode, 
                    info.HisName, 
                    info.HisUserCode, 
                    info.CenterCode, 
                    info.CenterName, 
                    info.CenterItemType, 
                    this.Operator.ID,
                    info.User01,//医保等级
                    info.User02,//备注
                    info.User03 //比例
            }
                ;
        }

        /// <summary>
        /// 获取所有项目信息(本地 ）
        /// </summary>
        /// <returns></returns>
        public int  QueryLocalItems(ref System.Data.DataSet ds)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractSiCompareItem.Current.QueryLocalItems;

            try
            {
                ds=new System.Data.DataSet();
                return this.ExecQuery(strSql, ref ds);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                this.WriteErr();
            }
            finally
            {
    
            }
            return -1;
        }


        /// <summary>
        /// 获取所有项目信息(中心 ）
        /// </summary>
        /// <returns></returns>
        public int QueryCenterItems(string strPacts,ref System.Data.DataSet ds)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractSiCompareItem.Current.QueryCenterItems;

            try
            {
                ds = new System.Data.DataSet();
                strSql = string.Format(strSql, strPacts);
                return this.ExecQuery(strSql,ref ds);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                this.WriteErr();
            }
            finally
            {

            }
            return -1;
        }

        /// <summary>
        /// 获取已对照项目信息
        /// </summary>
        /// <returns></returns>
        public List<FS.SOC.HISFC.Fee.Models.CompareItemModel> QueryComparedItem(string hisCode,string strPacts)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractCompareItem.Current.QueryComparedItems;

            try
            {
                if (this.ExecQuery(string.Format(strSql, hisCode,strPacts)) < 0)
                {
                    this.WriteDebug(this.Err);
                    return null;
                }
                List<FS.SOC.HISFC.Fee.Models.CompareItemModel> list = new List<FS.SOC.HISFC.Fee.Models.CompareItemModel>();
                FS.SOC.HISFC.Fee.Models.CompareItemModel info = null;
                while (this.Reader.Read())
                {
                    info = new FS.SOC.HISFC.Fee.Models.CompareItemModel();
                    info.PactCode = Reader[0].ToString();
                    info.PactName = Reader[1].ToString();
                    info.HisCode = Reader[2].ToString();
                    info.HisName = this.Reader[3].ToString();
                    info.CenterCode = this.Reader[4].ToString();
                    info.CenterName = this.Reader[5].ToString();
                    info.HisUserCode = this.Reader[6].ToString();
                    info.SpellCode = this.Reader[7].ToString();
                    info.WBCode = this.Reader[8].ToString();
                    info.User01 = this.Reader[9].ToString();
                    info.User02 = this.Reader[10].ToString();
                    info.User03= this.Reader[11].ToString();
                    list.Add(info);

                }
                return list;
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                this.WriteErr();
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return null;
        }

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="itemType"> 1 药品 2 非药品</param>
        /// <returns></returns>
        public List<FS.SOC.HISFC.Fee.Models.CompareItemModel> QueryItemsByType(string itemType)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractCompareItem.Current.QueryItemsByType;
            try
            {
                if (this.ExecQuery(strSql,itemType) < 0)
                {
                    this.WriteDebug(this.Err);
                    return null;
                }
                List<FS.SOC.HISFC.Fee.Models.CompareItemModel> list = new List<FS.SOC.HISFC.Fee.Models.CompareItemModel>();
                FS.SOC.HISFC.Fee.Models.CompareItemModel info = null;
                while (this.Reader.Read())
                {
                    info = new FS.SOC.HISFC.Fee.Models.CompareItemModel();
                    info.PactCode= this.Reader[0].ToString();
                    info.PactName = Reader[1].ToString();
                    info.HisCode = Reader[3].ToString();
                    info.HisName = Reader[4].ToString();
                    info.HisUserCode = Reader[5].ToString();
                    info.CenterCode = Reader[6].ToString();
                    info.CenterName = Reader[7].ToString();
                    info.CenterItemType= Reader[8].ToString();
                    info.SpellCode = Reader[9].ToString();
                    info.WBCode = Reader[10].ToString();


                    list.Add(info);

                }

                return list;
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                this.WriteErr();
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return null;
        }

        public List<FS.HISFC.Models.Base.PactInfo> QueryPactUnit(string centerType)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractCompareItem.Current.QueryPacts;

            if (this.ExecQuery(strSql, centerType) == -1)
            {
                return null;
            }

            List<FS.HISFC.Models.Base.PactInfo> pactUnitList = new  List<FS.HISFC.Models.Base.PactInfo>();//费用明细数组
            FS.HISFC.Models.Base.PactInfo pactUnit = null;

            try
            {
                while (this.Reader.Read())
                {
                    pactUnit = new FS.HISFC.Models.Base.PactInfo();
                    pactUnit.ID = this.Reader[0].ToString();//合同代码          
                    pactUnit.Name = this.Reader[1].ToString();//合同单位名称                    
                    pactUnit.PayKind.ID = this.Reader[2].ToString();//结算类别                               
                    pactUnit.ShortName = Reader[3].ToString();//合同单位简称
                    pactUnit.PactDllName = Reader[4].ToString(); //待遇dll名称
                    pactUnit.PactDllDescription = Reader[5].ToString();//待遇dll说明
                    pactUnit.SpellCode = Reader[6].ToString();//拼音码
                    pactUnit.WBCode = Reader[7].ToString();//五笔码
                    pactUnit.PactSystemType = Reader[8].ToString().Trim();
                    pactUnitList.Add(pactUnit);

                }


                return pactUnitList;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        public ArrayList QueryCenterType()
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractCompareItem.Current.QueryCenterTypes;

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            ArrayList centerTypeList = new ArrayList();//医保类型数组
            FS.HISFC.Models.Base.Spell centerType = null;

            try
            {
                while (this.Reader.Read())
                {
                    centerType = new FS.HISFC.Models.Base.Spell();
                    centerType.ID = this.Reader[0].ToString();//dll名称        
                    centerType.Name = this.Reader[1].ToString();//待遇描叙                    
                    centerType.SpellCode = Reader[2].ToString();//拼音码
                    centerType.WBCode = Reader[3].ToString();//五笔码
                    centerTypeList.Add(centerType);

                }


                return centerTypeList;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

      
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Delete(FS.SOC.HISFC.Fee.Models.CompareItemModel info)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractCompareItem.Current.Delete;

            try
            {
                strSql = string.Format(strSql, info.PactCode, info.HisCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <returns></returns>
        public int Insert(FS.SOC.HISFC.Fee.Models.CompareItemModel info)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractCompareItem.Current.Insert;
            try
            {
                strSql = string.Format(strSql, this.getParams(info));
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public int Update(FS.SOC.HISFC.Fee.Models.CompareItemModel info)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractCompareItem.Current.Update;
            try
            {
                strSql = string.Format(strSql, this.getParams(info));
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        
    }

}
