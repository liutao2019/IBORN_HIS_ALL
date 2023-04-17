using System;
using System.Collections;
using FS.HISFC.Models.SIInterface;
using FS.HISFC.Models.Fee;
using System.Data;
using FS.FrameWork.Function;
using System.Xml;

namespace GZSI.Management
{
    public class SILocalManager : FS.FrameWork.Management.Database 
    {
         /// <summary>
        /// 医保接口业务管理
        /// </summary>
        public SILocalManager()
        {
        }

        #region 对照

        /// <summary>
        /// 获取医保中心药品信息代码
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        private ArrayList QueryCenterItem(string strSql)
        {
            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Item obj = new FS.HISFC.Models.SIInterface.Item();

                    obj.PactCode = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SysClass = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.EnglishName = Reader[4].ToString();
                    obj.Specs = Reader[5].ToString();
                    obj.DoseCode = Reader[6].ToString();
                    obj.SpellCode = Reader[7].ToString();
                    obj.FeeCode = Reader[8].ToString();
                    obj.ItemType = Reader[9].ToString();
                    obj.ItemGrade = Reader[10].ToString();
                    obj.Rate = FS.FrameWork.Function.NConvert.ToDecimal( Reader[11].ToString() );
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal( Reader[12].ToString() );
                    obj.Memo = Reader[13].ToString();
                    obj.OperCode = Reader[14].ToString();
                    obj.OperDate = Convert.ToDateTime( Reader[15].ToString() );

                    al.Add( obj );
                }

                Reader.Close();

                return al;

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获取医保中心药品信息代码
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="sysClass"></param>
        /// <returns></returns>
        public ArrayList GetCenterItem( string pactCode, string sysClass )
        {
            string centerDrugType = "";
            string centerDrugTypeSec = "";
            string strSql = "";
            if (sysClass == "P")
            {
                centerDrugType = "X"; //西药项目
                centerDrugTypeSec = "X";
                if (this.Sql.GetSql( "Fee.Interface.GetCenterItem.Select.1", ref strSql ) == -1)
                    return null;
            }
            else if (sysClass == "Undrug")
            {
                centerDrugType = "L";
                centerDrugTypeSec = "F";
                if (this.Sql.GetSql( "Fee.Interface.GetCenterItem.Select.2", ref strSql ) == -1)
                    return null;
            }
            else
            {
                centerDrugType = "Z"; //中草药项目
                centerDrugTypeSec = "Z";
                if (this.Sql.GetSql( "Fee.Interface.GetCenterItem.Select.1", ref strSql ) == -1)
                    return null;
            }

            try
            {
                strSql = string.Format( strSql, pactCode, centerDrugType, centerDrugTypeSec );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            return QueryCenterItem( strSql );
        }

        /// <summary>
        /// 获取医保中心所有信息代码
        /// </summary>
        /// <param name="pactCode">合同单位</param>
        /// <returns></returns>
        public ArrayList GetCenterItem( string pactCode )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.GetCenterItem.Select.3", ref strSql ) == -1)
                return null;
            try
            {
                strSql = string.Format( strSql, pactCode );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            return QueryCenterItem( strSql );
        }

        /// <summary>
        ///执行sql获得医保对照
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList QueryCompareItem( string strSql )
        {
            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Compare obj = new Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();
                    obj.HisCode = Reader[1].ToString();
                    obj.CenterItem.ID = Reader[2].ToString();
                    obj.CenterItem.SysClass = Reader[3].ToString();
                    obj.CenterItem.Name = Reader[4].ToString();
                    obj.CenterItem.EnglishName = Reader[5].ToString();
                    obj.CenterItem.Specs = Reader[6].ToString();
                    obj.CenterItem.DoseCode = Reader[7].ToString();
                    obj.CenterItem.SpellCode = Reader[8].ToString();
                    obj.CenterItem.FeeCode = Reader[9].ToString();
                    obj.CenterItem.ItemType = Reader[10].ToString();
                    obj.CenterItem.ItemGrade = Reader[11].ToString();
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal( Reader[12].ToString() );
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal( Reader[13].ToString() );
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal( Reader[19].ToString() );
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = Convert.ToDateTime( Reader[22].ToString() );
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();

                    al.Add( obj );
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获得对照后的项目信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="sysClass"></param>
        /// <returns></returns>
        public ArrayList GetCompareItem( string pactCode, string sysClass )
        {
            string centerDrugType = "";
            string centerDrugTypeSec = "";

            if (sysClass == "P")
            {
                centerDrugType = "X"; //西药项目
                centerDrugTypeSec = "Z";
            }
            else if (sysClass == "Undrug")
            {
                centerDrugType = "L";
                centerDrugTypeSec = "F";
            }
            else
            {
                centerDrugType = "X"; //中草药项目
                centerDrugTypeSec = "Z";
            }

            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.GetCompareItem.Select.1", ref strSql ) == -1)
                return null;

            try
            {
                strSql = string.Format( strSql, pactCode, centerDrugType, centerDrugTypeSec );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            return QueryCompareItem( strSql );
        }

        #region 作废
        /*
        /// <summary>
        /// 获得对照后的所有类别药品信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public ArrayList GetCompareDrugItem(string pactCode)
        {
            string strSql = "";
            if(this.Sql.GetSql("Fee.Interface.GetCompareItem.Select.3", ref strSql) == -1) 
                return null;
			
            try
            {   				
                strSql = string.Format(strSql, pactCode);
            }
            catch(Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while(Reader.Read())
                {
                    FS.HISFC.Models.InterfaceSi.Compare obj = new FS.HISFC.Models.InterfaceSi.Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();
                    obj.HisCode = Reader[1].ToString();
                    obj.CenterItem.ID = Reader[2].ToString();
                    obj.CenterItem.SysClass = Reader[3].ToString();
                    obj.CenterItem.Name = Reader[4].ToString();
                    obj.CenterItem.EnglishName = Reader[5].ToString();
                    obj.CenterItem.Specs = Reader[6].ToString();
                    obj.CenterItem.DoseCode = Reader[7].ToString();
                    obj.CenterItem.SpellCode = Reader[8].ToString();
                    obj.CenterItem.FeeCode = Reader[9].ToString();
                    obj.CenterItem.ItemType = Reader[10].ToString();
                    obj.CenterItem.ItemGrade = Reader[11].ToString();
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = Convert.ToDateTime(Reader[22].ToString());
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch(Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }
        */
        #endregion

        /// <summary>
        /// 是否适应症用药
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="orderID"></param>
        /// <returns>空，非限制性用药；1限制性用药允许报销；0不报销</returns>
        public string IsIndicationsItemForOut(string clinicCode, string orderID)
        {
            string strSql = "";
            if (this.Sql.GetSql("Fee.Interface.GetItemIndicationsFlag.Out", ref strSql) == -1)
            {
                strSql = @"select f.indications from met_ord_recipedetail_extend f where f.clinic_code='{0}' and f.mo_order ='{1}'";
            }
            try
            {
                strSql = string.Format(strSql, clinicCode, orderID);
                this.ExecQuery(strSql);

                string flag = string.Empty;
                while (Reader.Read())
                {
                    flag = Reader[0].ToString();
                }
                return flag;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取中心项目信息
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        /// <param name="itemCode">项目编码</param>
        /// <returns>中心项目信息</returns>
        public FS.HISFC.Models.SIInterface.Item GetCenterItemInfo( string pactCode, string itemCode )
        {
            string strSql = "";
            if (this.Sql.GetSql( "Fee.Interface.GetCenterItem", ref strSql ) == -1)
                return null;
            try
            {
                strSql = string.Format( strSql, pactCode, itemCode );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            FS.HISFC.Models.SIInterface.Item obj = new FS.HISFC.Models.SIInterface.Item();
            try
            {
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    obj = new FS.HISFC.Models.SIInterface.Item();

                    obj.PactCode = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SysClass = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.EnglishName = Reader[4].ToString();
                    obj.Specs = Reader[5].ToString();
                    obj.DoseCode = Reader[6].ToString();
                    obj.SpellCode = Reader[7].ToString();
                    obj.FeeCode = Reader[8].ToString();
                    obj.ItemType = Reader[9].ToString();
                    obj.ItemGrade = Reader[10].ToString();
                    obj.Rate = FS.FrameWork.Function.NConvert.ToDecimal( Reader[11].ToString() );
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal( Reader[12].ToString() );
                    obj.Memo = Reader[13].ToString();
                    obj.OperCode = Reader[14].ToString();
                    obj.OperDate = Convert.ToDateTime( Reader[15].ToString() );
                }

                Reader.Close();
                if (obj == null || obj.ID == "")
                    this.Err = "未获取医保中心项目信息 请先对启用更新程序更新本地中心项目信息";
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 是否对该项目已对照
        /// </summary>
        /// <param name="hisUserCode">his内项目自定义码 对应医保服务器内医保对照信息的his项目代码</param>
        /// <param name="centerCode">中心项目代码</param>
        /// <returns>-1 出错 0 未对照 1 已对照</returns>
        public int IsCompared( string hisUserCode, string centerCode )
        {
            string strSql = "";
            if (this.Sql.GetSql( "Fee.Interface.IsCompared", ref strSql ) == -1)
                return -1;
            try
            {
                strSql = string.Format( strSql, hisUserCode, centerCode );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            try
            {
                this.ExecQuery( strSql );
                return 0;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获得单条已对照信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="objCompare"></param>
        /// <returns></returns>
        public int GetCompareSingleItem( string pactCode, string itemCode, ref Compare objCompare )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.GetCompareSingleItem.Select.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format( strSql, pactCode, itemCode );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Compare obj = new Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();
                    obj.HisCode = Reader[1].ToString();
                    obj.CenterItem.ID = Reader[2].ToString();
                    obj.CenterItem.SysClass = Reader[3].ToString();
                    obj.CenterItem.Name = Reader[4].ToString();
                    obj.CenterItem.EnglishName = Reader[5].ToString();
                    obj.CenterItem.Specs = Reader[6].ToString();
                    obj.CenterItem.DoseCode = Reader[7].ToString();
                    obj.CenterItem.SpellCode = Reader[8].ToString();
                    obj.CenterItem.FeeCode = Reader[9].ToString();
                    obj.CenterItem.ItemType = Reader[10].ToString();
                    obj.CenterItem.ItemGrade = Reader[11].ToString();
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal( Reader[12].ToString() );
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal( Reader[13].ToString() );
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal( Reader[19].ToString() );
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = Convert.ToDateTime( Reader[22].ToString() );
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();

                    al.Add( obj );
                }

                Reader.Close();

                if (al.Count > 0)
                {
                    objCompare = (Compare)al[0];
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 转化为参数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string[] GetCompareItemParm( Compare obj )
        {
            string[] compareItemParm = 
                {
                    obj.CenterItem.PactCode, 
                    obj.HisCode, 
                    obj.CenterItem.ID,
                    obj.CenterItem.SysClass,
                    obj.CenterItem.Name,
                    obj.CenterItem.EnglishName,
                    obj.CenterItem.Specs,
                    obj.CenterItem.DoseCode, 
                    obj.CenterItem.SpellCode,
                    obj.CenterItem.FeeCode,
                    obj.CenterItem.ItemType, 
                    obj.CenterItem.ItemGrade,
                    obj.CenterItem.Rate.ToString(),
                    obj.CenterItem.Price.ToString(),
                    obj.CenterItem.Memo,
                    obj.SpellCode.SpellCode,
                    obj.SpellCode.WBCode, 
                    obj.SpellCode.UserCode,
                    obj.Specs,
                    obj.Price.ToString(), 
                    obj.DoseCode, 
                    obj.CenterItem.OperCode,
                    obj.Name, 
                    obj.RegularName
                };
            return compareItemParm;
        }

        /// <summary>
        /// 插入对照后的项目信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertCompareItem( Compare obj )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.InsertCompareItem.1", ref strSql ) == -1)
                return -1;
            string[] objParm = GetCompareItemParm( obj );
            try
            {
                return this.ExecNoQuery( strSql, objParm );//this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新一条医保对照数据
        /// </summary>
        /// <param name="compareObj">医保实体</param>
        /// <returns></returns>
        public int UpdateCompareItem( Compare compareObj )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.UpdateCompareItem.1", ref strSql ) == -1)
                return -1;

            string[] objParm = GetCompareItemParm( compareObj );
            try
            {
                return this.ExecNoQuery( strSql, objParm );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 删除已对照信息
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        /// <param name="hisCode">HIS本地编码</param>
        /// <returns></returns>
        public int DeleteCompareItem( string pactCode, string hisCode )
        {
            string strSql = "";
            if (this.Sql.GetSql( "Fee.Interface.DeleteCompareItem.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format( strSql, pactCode, hisCode );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获得未对照非药品项目信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public ArrayList GetNoCompareUndrugItem( string pactCode )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.GetNoCompareUndrugItem.Select.1", ref strSql ) == -1)
                return null;

            try
            {
                strSql = string.Format( strSql, pactCode );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.Undrug obj = new FS.HISFC.Models.Fee.Item.Undrug();

                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.SpellCode = Reader[2].ToString();
                    obj.WBCode = Reader[3].ToString();
                    obj.UserCode = Reader[4].ToString();
                    obj.Specs = Reader[5].ToString();
                    obj.NationCode = Reader[6].ToString();
                    obj.GBCode = Reader[7].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal( Reader[8].ToString() );
                    obj.PriceUnit = Reader[9].ToString();

                    al.Add( obj );
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获得药品信息
        /// </summary>
        /// <param name="pactCode">合同单位</param>
        /// <param name="drugType">药品类别</param>
        /// <returns></returns>
        public ArrayList GetNoCompareDrugItem( string pactCode, string drugType )
        {
            string strSql = "";
            string centerDrugType = "";

            //本地项目P 西药 Z 中成药 C 草药

            if (drugType == "P")
            {
                centerDrugType = "X"; //西药项目
            }
            else
            {
                centerDrugType = "Z"; //中草药项目
            }
            if (drugType == "ALL")			//忽略药品类别
                centerDrugType = "ALL";

            if (this.Sql.GetSql( "Fee.Interface.GetNoCompareDrugItem.Select.1", ref strSql ) == -1)
                return null;

            try
            {
                strSql = string.Format( strSql, pactCode, drugType, centerDrugType );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {

                ArrayList al = new ArrayList();
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    FS.HISFC.Models.Pharmacy.Item obj = new FS.HISFC.Models.Pharmacy.Item();

                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.SpellCode = Reader[2].ToString();
                    obj.WBCode = Reader[3].ToString();
                    obj.UserCode = Reader[4].ToString();
                    obj.Specs = Reader[5].ToString();
                    obj.NameCollection.RegularName = Reader[6].ToString();
                    obj.NameCollection.RegularSpell.SpellCode = Reader[7].ToString();
                    obj.NameCollection.RegularSpell.WBCode = Reader[8].ToString();
                    obj.NameCollection.InternationalCode = Reader[9].ToString();
                    obj.GBCode = Reader[10].ToString();
                    obj.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal( Reader[11].ToString() );
                    obj.Type.ID = Reader[12].ToString();
                    obj.DosageForm.ID = Reader[13].ToString();

                    al.Add( obj );
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获得本地医保目录信息
        /// </summary>
        /// <returns></returns>
        public int GetLocalSIItemCounts()
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.GetLocalSIItemCounts.Select", ref strSql ) == -1)
                return -1;

            if (this.ExecQuery( strSql ) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保项目目录失败!";
                return -1;
            }

            int count = 0;
            try
            {
                while (Reader.Read())
                {
                    count = FS.FrameWork.Function.NConvert.ToInt32( Reader[0].ToString() );
                }

                Reader.Close();

                return count;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return -1;
            }
        }

        /// <summary>
        /// 删除所有医保信息
        /// </summary>
        /// <returns></returns>
        public int DeleteSIItem()
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.DeleteSIItem.Delete", ref strSql ) == -1)
                return -1;

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

        }

        /// <summary>
        /// 转化为参数
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string[] GetSIItemParm( FS.HISFC.Models.SIInterface.Item item )
        {
            string[] SIItemParm = 
                {
                    item.ID,
                    item.SysClass,
                    item.Name, 
                    item.EnglishName,
                    item.Specs, 
                    item.DoseCode, 
                    item.SpellCode, 
                    item.FeeCode, 
                    item.ItemType,
                    item.ItemGrade,
                    item.Rate.ToString(),
                    this.Operator.ID
                };
            return SIItemParm;
        }

        /// <summary>
        /// Update医保项目信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int UpdateSIItem( FS.HISFC.Models.SIInterface.Item item )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.InsertSIItem.Update", ref strSql ) == -1)
                return -1;
            try
            {
                return this.ExecNoQuery( strSql, GetSIItemParm( item ) );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新费用日期小于出院日期
        /// </summary>
        /// <param name="inpatienNo"></param>
        /// <param name="dtOut"></param>
        /// <returns></returns>
        public int UpdateSIFeeDate( string inpatienNo, DateTime dtOut )
        {
            string strSql = "";
            string strReturn = "";

            //获取SQL语句
            if (this.Sql.GetSql( "Fee.Interface.UpdateSIFeeDate", ref strSql ) == -1)
            {
                this.Err = "没有找到 Fee.Interface.UpdateSIFeeDate 字段!";
                return -1;
            }
            //格式化字符串
            strSql = string.Format( strSql, inpatienNo, dtOut.ToString() );

            if (this.ExecEvent( strSql, ref strReturn ) == -1)
            {
                this.Err = "执行存储过程出错!Fee.Interface.UpdateSIFeeDate" + this.Err;
                this.ErrCode = "Fee.Interface.UpdateSIFeeDate";
                this.WriteErr();
                return -1;
            }
            string[] s = strReturn.Split( ',' );
            this.Err = s[1];
            return FS.FrameWork.Function.NConvert.ToInt32( s[0] );

        }

        /// <summary>s
        /// 插入医保项目信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int InsertSIItem( FS.HISFC.Models.SIInterface.Item item )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.InsertSIItem.Insert", ref strSql ) == -1)
                return -1;
            try
            {
                return this.ExecNoQuery( strSql, GetSIItemParm( item ) );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 下载医保项目
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int LoadSIItem( FS.HISFC.Models.SIInterface.Item item )
        {
            int iReturn = this.UpdateSIItem( item );
            if (iReturn == 0)
            {
                return InsertSIItem( item );
            }
            else
            {
                return iReturn;
            }
        }

        #endregion

        #region 医保政策维护

        /// <summary>
        /// 获得政策维护的所有合同单位信息;
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllPactInfo()
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.Ruls.GetAllPactInfo.Select.1", ref strSql ) == -1)
                return null;

            try
            {
                strSql = string.Format( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    Insurance obj = new Insurance();

                    obj.PactInfo.ID = Reader[0].ToString();
                    obj.PactInfo.Name = Reader[1].ToString();
                    obj.Kind.ID = Reader[2].ToString();

                    al.Add( obj );
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

        }

        /// <summary>
        /// 插入医保政策信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertInsuranceDeal( Insurance obj )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.InsertInsuranceDeal.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format( strSql, obj.PactInfo.ID, obj.Kind.ID, obj.PartId, obj.Rate,
                    obj.BeginCost, obj.EndCost, obj.Memo, obj.OperCode.ID );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新医保政策信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateInsuranceDeal( Insurance obj )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.UpdateInsuranceDeal.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format( strSql, obj.PactInfo.ID, obj.Kind.ID, obj.PartId, obj.Rate,
                    obj.BeginCost, obj.EndCost, obj.Memo, obj.OperCode.ID );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 删除医保政策信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int DeleteInsuranceDeal( Insurance obj )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.DeleteInsuranceDeal.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format( strSql, obj.PactInfo.ID, obj.Kind.ID, obj.PartId );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 得到指定合同单位,类别下的规则信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public ArrayList GetAllInsuranceInfo( string pactCode, string kind )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.Ruls.GetAllInsuranceInfo.Select.1", ref strSql ) == -1)
                return null;

            try
            {
                strSql = string.Format( strSql, pactCode, kind );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    Insurance obj = new Insurance();

                    //					obj.PactInfo.ID = Reader[0].ToString();
                    //obj.PactInfo.Name = Reader[1].ToString();
                    //obj.Kind.ID = Reader[2].ToString();
                    obj.PartId = Reader[0].ToString();
                    obj.Rate = FS.FrameWork.Function.NConvert.ToDecimal( Reader[1].ToString() );
                    obj.BeginCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[2].ToString() );
                    obj.EndCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[3].ToString() );
                    obj.Memo = Reader[4].ToString();
                    obj.OperCode.ID = Reader[5].ToString();
                    obj.OperDate = Convert.ToDateTime( Reader[6].ToString() );

                    al.Add( obj );
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

        }

        #endregion

        #region 黑名单维护

        /// <summary>
        /// 插入黑名单信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertBlackList( BlackList obj )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.InsertBlackList.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format( strSql, obj.MCardNo, obj.Kind, obj.Name, obj.ValidState,
                    obj.OperInfo.ID );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新黑名单信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateBlackList( BlackList obj )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.UpdateBlackList.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format( strSql, obj.ID, obj.MCardNo, obj.Kind, obj.Name, obj.ValidState,
                    obj.OperInfo.ID, obj.OperDate );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 删除黑名单信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mCardNo"></param>
        /// <returns></returns>
        public int DeleteBlackList( string id, string mCardNo )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.DeleteBlackList.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format( strSql, id, mCardNo );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 删除黑名单信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mCardNo"></param>
        /// <returns></returns>
        public int DeleteBlackList(string mzPactCode)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.DeleteBlackList.2", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, mzPactCode);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 通过黑名单类别获得所有黑名单列表
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="validState"></param>
        /// <returns></returns>
        public ArrayList GetBlackListFromKind( string kind, string validState )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.GetBlackListFromKind.Select.1", ref strSql ) == -1)
                return null;

            try
            {
                strSql = string.Format( strSql, kind, validState );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    BlackList obj = new BlackList();

                    obj.ID = Reader[0].ToString();
                    obj.MCardNo = Reader[1].ToString();
                    obj.Kind = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.ValidState = Reader[4].ToString();
                    obj.OperInfo.ID = Reader[5].ToString();
                    obj.OperDate = Convert.ToDateTime( Reader[6].ToString() );

                    al.Add( obj );
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 判断患者得单位或个人是否存在黑名单内
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="mCardNo"></param>
        /// <returns></returns>
        public bool ExistBlackList( string pactCode, string mCardNo )
        {
            string strSql = "";
            string strSql2 = "";
            int pactCount = 0;
            int personCount = 0;

            if (this.Sql.GetSql( "Fee.Interface.ExistBlackList.Select.1", ref strSql ) == -1)
                return false;

            try
            {
                strSql = string.Format( strSql, pactCode );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
            try
            {

                pactCount = FS.FrameWork.Function.NConvert.ToInt32( this.ExecSqlReturnOne( strSql ) );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }

            if (this.Sql.GetSql( "Fee.Interface.ExistBlackList.Select.2", ref strSql2 ) == -1)
                return false;

            try
            {
                strSql = string.Format( strSql2, pactCode );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
            try
            {

                personCount = FS.FrameWork.Function.NConvert.ToInt32( this.ExecSqlReturnOne( strSql2 ) );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }

            if (pactCount + personCount > 0)
                return true;
            else
                return false;

        }

        #endregion

        #region 医保接口计算

        /// <summary>
        /// 计算比例
        /// </summary>
        /// <param name="pactCode">合同单位编码</param>
        /// <param name="item">费用实体</param>
        /// <returns></returns>
        public int ComputeRate( string pactCode, ref FS.HISFC.Models.Fee.Inpatient.FeeItemList item )
        {
            int returnValue = 0;

            Compare objCompare = new Compare();

            returnValue = GetCompareSingleItem(pactCode, item.Item.ID, ref objCompare);

            if (returnValue == -1)
                return returnValue;
            if (returnValue == -2)
                objCompare.CenterItem.Rate = 1;

            item.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(item.FT.TotCost * objCompare.CenterItem.Rate, 2);
            item.FT.PubCost = item.FT.TotCost - item.FT.OwnCost;
          
            //if (pactCode == "2")
            //{
            //    item.FT.OwnCost = item.FT.TotCost;
            //    item.FT.PubCost = item.FT.TotCost - item.FT.OwnCost;
            //}

            return 0;
        }

        /// <summary>
        /// 得到对照信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetItemCompareInfo( string pactCode, FS.HISFC.Models.Fee.Inpatient.FeeItemList obj )
        {
            return 0;
        }

        #endregion

        #region 医保限额维护

        /// <summary>
        /// 获得输入月份的科室限额信息
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public ArrayList GetDeptSICostFromMonth( string month )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.GetDeptSICostFromMonth.Select.1", ref strSql ) == -1)
                return null;

            try
            {
                strSql = string.Format( strSql, month );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    DeptSICost obj = new DeptSICost();

                    obj.ID = Reader[0].ToString();
                    obj.Month = Reader[1].ToString();
                    obj.Name = Reader[2].ToString();
                    obj.AlertMoney = FS.FrameWork.Function.NConvert.ToDecimal( Reader[3].ToString() );
                    obj.ValidStateId = Reader[4].ToString();
                    obj.SortId = FS.FrameWork.Function.NConvert.ToInt32( Reader[5].ToString() );
                    obj.OperInfo.ID = Reader[6].ToString();
                    obj.OperDate = Convert.ToDateTime( Reader[7].ToString() );
                    obj.SpellCode.SpellCode = Reader[8].ToString();

                    al.Add( obj );
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 插入未维护到医保限额的科室信息,警戒线默认为0; sortId默认为10000
        /// </summary>
        /// <param name="month"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int InsertNoExistDeptInfo( string month, string operCode )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.InsertNoExistDeptInfo.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format( strSql, month, operCode );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新医保限额
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateDeptSICost( DeptSICost obj )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.UpdateDeptSICost.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format( strSql, obj.ID, obj.Month, obj.Name, obj.AlertMoney, obj.ValidStateId,
                    obj.SortId, obj.OperInfo.ID );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获得某科室某月的限额
        /// </summary>
        /// <param name="month"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DeptSICost GetSingleDeptSICost( string month, string deptCode )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.GetSingleDeptSICost.Select.1", ref strSql ) == -1)
                return null;

            try
            {
                strSql = string.Format( strSql, month, deptCode );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery( strSql );
                while (Reader.Read())
                {
                    DeptSICost obj = new DeptSICost();

                    obj.ID = Reader[2].ToString();
                    obj.Month = Reader[3].ToString();
                    obj.Name = Reader[4].ToString();
                    obj.AlertMoney = FS.FrameWork.Function.NConvert.ToDecimal( Reader[5].ToString() );
                    obj.ValidStateId = Reader[6].ToString();
                    obj.SortId = FS.FrameWork.Function.NConvert.ToInt32( Reader[7].ToString() );
                    obj.OperInfo.ID = Reader[8].ToString();
                    obj.OperDate = Convert.ToDateTime( Reader[9].ToString() );
                    al.Add( obj );
                }

                Reader.Close();

                if (al.Count > 0)
                    return (DeptSICost)al[0];
                else
                {
                    this.Err = month + "的科室限额没有维护,请通知信息科！";
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        #endregion

        #region 医保结算表

        /// <summary>
        /// 得到结算序号
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public string GetBalNo( string inpatientNo )
        {
            string strSql = "";
            string balNo = "";
            if (this.Sql.GetSql( "Fee.Interface.GetBalNo.1", ref strSql ) == -1)
                return "";
            try
            {
                strSql = string.Format( strSql, inpatientNo );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
            this.ExecQuery( strSql );
            try
            {
                while (Reader.Read())
                {
                    balNo = Reader[0].ToString();
                }
                Reader.Close();
                return balNo;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 插入医保结算信息表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertSIMainInfo( FS.HISFC.Models.RADT.PatientInfo obj )
        {
            string balNo = GetBalNo(obj.ID);

            if (balNo == null || balNo == "")
            {
                balNo = "0";
            }

            balNo = (Convert.ToInt32(balNo) + 1).ToString();
            string strSql = "";

            if (this.Sql.GetSql( "Fee.Interface.InsertSIMainInfo.1", ref strSql ) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, obj.ID, balNo, obj.SIMainInfo.InvoiceNo, obj.PVisit.MedicalType.ID, obj.PID.PatientNO,
                    obj.PID.CardNO, obj.SSN, obj.SIMainInfo.AppNo, obj.SIMainInfo.ProceatePcNo,
                    obj.SIMainInfo.SiBegionDate.ToString(), obj.SIMainInfo.SiState, obj.Name, obj.Sex.ID.ToString(),
                    obj.IDCard, "", obj.Birthday.ToString(), obj.SIMainInfo.EmplType, obj.CompanyName,
                    obj.SIMainInfo.InDiagnose.Name, obj.PVisit.PatientLocation.Dept.ID, obj.PVisit.PatientLocation.Dept.Name,
                    obj.Pact.PayKind.ID, obj.Pact.ID, obj.Pact.Name, obj.PVisit.PatientLocation.Bed.ID,
                    obj.PVisit.InTime.ToString(), obj.PVisit.InTime.ToString(), obj.SIMainInfo.InDiagnose.ID,
                    obj.SIMainInfo.InDiagnose.Name, this.Operator.ID, obj.SIMainInfo.HosNo, obj.SIMainInfo.RegNo,
                    obj.SIMainInfo.FeeTimes, obj.SIMainInfo.HosCost, obj.SIMainInfo.YearCost, obj.PVisit.OutTime.ToString(),
                    obj.SIMainInfo.OutDiagnose.ID, obj.SIMainInfo.OutDiagnose.Name, obj.SIMainInfo.BalanceDate.ToString(),
                    obj.SIMainInfo.TotCost, obj.SIMainInfo.PayCost, obj.SIMainInfo.PubCost, obj.SIMainInfo.ItemPayCost,
                    obj.SIMainInfo.BaseCost, obj.SIMainInfo.ItemYLCost, obj.SIMainInfo.PubOwnCost, obj.SIMainInfo.OwnCost,
                    obj.SIMainInfo.OverTakeOwnCost, FS.FrameWork.Function.NConvert.ToInt32( obj.SIMainInfo.IsValid ),
                    FS.FrameWork.Function.NConvert.ToInt32( obj.SIMainInfo.IsBalanced ),"2" );

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 插入医保结算表(门诊部分)
        /// </summary>
        /// <param name="obj">挂号信息</param>
        /// <returns>-1 失败 1成功</returns>
        public int InsertSIMainInfo(FS.HISFC.Models.Registration.Register obj)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertSIMainInfo.1", ref strSql) == -1)
                return -1;

            string balNo = GetBalNo(obj.ID);

            if (balNo == null || balNo == "")
            {
                balNo = "0";
            }

            balNo = (Convert.ToInt32(balNo) + 1).ToString();
            try
            {
                string[] str = new string[]{
                                                obj.ID, //0
                                                balNo, 
                                                obj.SIMainInfo.InvoiceNo, 
                                                obj.SIMainInfo.MedicalType.ID, 
                                                obj.PID.CardNO,
                                                obj.PID.CardNO, 
                                                obj.SSN, 
                                                obj.SIMainInfo.AppNo.ToString(), 
                                                obj.SIMainInfo.ProceatePcNo,
											   obj.SIMainInfo.SiBegionDate.ToString(), 
                                               obj.SIMainInfo.SiState, //10
                                               obj.Name,
                                               obj.Sex.ID.ToString(),
											   obj.IDCard, 
                                               "", 
                                               obj.Birthday.ToString(), 
                                               obj.SIMainInfo.EmplType,
                                               obj.CompanyName,
											   obj.SIMainInfo.ClinicDiagNose, 
                                               obj.DoctorInfo.Templet.Dept.ID, 
                                               obj.DoctorInfo.Templet.Dept.Name,//20 
											   obj.Pact.PayKind.ID,
                                               obj.Pact.ID,
                                               obj.Pact.Name, 
                                               "", 
											   obj.DoctorInfo.SeeDate.ToString(), 
                                               obj.DoctorInfo.SeeDate.ToString(), 
                                               obj.SIMainInfo.InDiagnose.ID,
											   obj.SIMainInfo.InDiagnose.Name, 
                                               this.Operator.ID, 
                                               obj.SIMainInfo.HosNo,
                                               obj.SIMainInfo.RegNo,
											   obj.SIMainInfo.FeeTimes.ToString(), 
                                               obj.SIMainInfo.HosCost.ToString(), 
                                               obj.SIMainInfo.YearCost.ToString(),
                                               obj.DoctorInfo.Templet.End.ToString(),
											   obj.SIMainInfo.OutDiagnose.ID, 
                                               obj.SIMainInfo.OutDiagnose.Name, 
                                               obj.SIMainInfo.BalanceDate.ToString(),
											   obj.SIMainInfo.TotCost.ToString(),
                                               obj.SIMainInfo.PayCost.ToString(), //40 
                                               obj.SIMainInfo.PubCost.ToString(),
                                               obj.SIMainInfo.ItemPayCost.ToString(),
											   obj.SIMainInfo.BaseCost.ToString(), 
                                               obj.SIMainInfo.ItemYLCost.ToString(), 
                                               obj.SIMainInfo.PubOwnCost.ToString(), 
                                               obj.SIMainInfo.OwnCost.ToString(),
											   obj.SIMainInfo.OverTakeOwnCost.ToString(),
                                               NConvert.ToInt32(obj.SIMainInfo.IsValid).ToString(),
											   NConvert.ToInt32(obj.SIMainInfo.IsBalanced).ToString(), 
                                               obj.SIMainInfo.TypeCode,//50
                                               obj.SIMainInfo.IndividualBalance.ToString(),
											   obj.SIMainInfo.FreezeMessage, 
                                               obj.SIMainInfo.ApplySequence, 
                                               obj.SIMainInfo.ApplyType.ID,
											   obj.SIMainInfo.ApplyType.Name, 
                                               obj.SIMainInfo.Fund.ID, 
                                               obj.SIMainInfo.Fund.Name, 
                                               obj.SIMainInfo.BusinessSequence//58
										   };
                return this.ExecNoQuery(strSql, str);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 得到医保患者基本信息;
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetSIPersonInfo( string inpatientNo )
        {
            FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
            string HospitalId = this.GetApiHosIdXmlSetting();//{0A47824B-8679-48e2-943B-8A893B36D076} zhang-wx 神奇的医院，无语了，住院医保上传读取api的xml配置文件中的医院编码
            string strSql = "";
            string balNo = this.GetBalNo( inpatientNo );
            //string balNo = "0";  //当前使用的医保信息
            if (balNo == "")
                return null;
            if (this.Sql.GetSql( "Fee.Interface.GetSIPersonInfo.Select.1", ref strSql ) == -1)
                return null;
            try
            {
                strSql = string.Format( strSql, inpatientNo, balNo );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            this.ExecQuery( strSql );
            try
            {
                while (Reader.Read())
                {

                    obj.SIMainInfo.HosNo = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SIMainInfo.BalNo = Reader[2].ToString();
                    obj.SIMainInfo.InvoiceNo = Reader[3].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[4].ToString();
                    if (obj.SIMainInfo.MedicalType.ID == "1")
                        obj.SIMainInfo.MedicalType.Name = "住院";
                    else
                        obj.SIMainInfo.MedicalType.Name = "门诊特定项目";
                    obj.PID.PatientNO = Reader[5].ToString();
                    obj.PID.CardNO = Reader[6].ToString();
                    obj.SSN = Reader[7].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32( Reader[8].ToString() );
                    obj.SIMainInfo.ProceatePcNo = Reader[9].ToString();
                    obj.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime( Reader[10].ToString() );
                    obj.SIMainInfo.SiState = Reader[11].ToString();
                    obj.Name = Reader[12].ToString();
                    obj.Sex.ID = Reader[13].ToString();
                    obj.IDCard = Reader[14].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime( Reader[15].ToString() );
                    obj.SIMainInfo.EmplType = Reader[16].ToString();
                    obj.CompanyName = Reader[17].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[18].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[19].ToString();
                    obj.PVisit.PatientLocation.Dept.Name = Reader[20].ToString();
                    obj.Pact.PayKind.ID = Reader[21].ToString();
                    obj.Pact.ID = Reader[22].ToString();
                    obj.Pact.Name = Reader[23].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[24].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime( Reader[25].ToString() );
                    obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime( Reader[25].ToString() );
                    obj.SIMainInfo.InDiagnose.ID = Reader[26].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[27].ToString();
                    if (!Reader.IsDBNull( 28 ))
                        obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime( Reader[28].ToString() );
                    obj.SIMainInfo.OutDiagnose.ID = Reader[29].ToString();
                    obj.SIMainInfo.OutDiagnose.Name = Reader[30].ToString();
                    if (!Reader.IsDBNull( 31 ))
                        obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime( Reader[31].ToString() );

                    obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[32].ToString() );
                    obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[33].ToString() );
                    obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[34].ToString() );
                    obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[35].ToString() );
                    obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[36].ToString() );
                    obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[37].ToString() );
                    obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[38].ToString() );
                    obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[39].ToString() );
                    obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[40].ToString() );
                    obj.SIMainInfo.Memo = Reader[41].ToString();
                    obj.SIMainInfo.OperInfo.ID = Reader[42].ToString();
                    obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime( Reader[43].ToString() );
                    obj.SIMainInfo.RegNo = Reader[44].ToString();
                    obj.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32( Reader[45].ToString() );
                    obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[46].ToString() );
                    obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal( Reader[47].ToString() );
                    obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean( Reader[48].ToString() );
                    obj.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean( Reader[49].ToString() );
                }
                Reader.Close();
                if (string.IsNullOrEmpty(obj.SIMainInfo.HosNo)&&obj.SIMainInfo.RegNo.Length>6)
                {
                    obj.SIMainInfo.HosNo = obj.SIMainInfo.RegNo.Substring(0, 6);
                }
                //{0A47824B-8679-48e2-943B-8A893B36D076} zhang-wx 神奇的医院，无语了，住院医保上传读取api的xml配置文件中的医院编码
                if (!string.IsNullOrEmpty(HospitalId) && HospitalId.Trim() != "")
                {
                    obj.SIMainInfo.HosNo = HospitalId;
                }

                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return null;
            }
        }

        /// <summary>
        /// 得到医保患者基本信息;
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetSZSIPersonInfo(string inpatientNo)
        {
            FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
            string strSql = "";
            string balNo = this.GetBalNo(inpatientNo);
            //string balNo = "0";  //当前使用的医保信息
            if (balNo == "")
                return null;
            if (this.Sql.GetSql("Fee.Interface.GetSZSIPersonInfo.Select.1", ref strSql) == -1)
                return null;
            try
            {
                strSql = string.Format(strSql, inpatientNo, balNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }          
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {

                    obj.SIMainInfo.HosNo = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SIMainInfo.BalNo = Reader[2].ToString();
                    obj.SIMainInfo.InvoiceNo = Reader[3].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[4].ToString();
                    if (obj.SIMainInfo.MedicalType.ID == "1")
                        obj.SIMainInfo.MedicalType.Name = "住院";
                    else
                        obj.SIMainInfo.MedicalType.Name = "门诊特定项目";
                    obj.PID.PatientNO = Reader[5].ToString();
                    obj.PID.CardNO = Reader[6].ToString();
                    obj.SSN = Reader[7].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[8].ToString());
                    obj.SIMainInfo.ProceatePcNo = Reader[9].ToString();
                    obj.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    obj.SIMainInfo.SiState = Reader[11].ToString();
                    obj.Name = Reader[12].ToString();
                    obj.Sex.ID = Reader[13].ToString();
                    obj.IDCard = Reader[14].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                    obj.SIMainInfo.EmplType = Reader[16].ToString();
                    obj.CompanyName = Reader[17].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[18].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[19].ToString();
                    obj.PVisit.PatientLocation.Dept.Name = Reader[20].ToString();
                    obj.Pact.PayKind.ID = Reader[21].ToString();
                    obj.Pact.ID = Reader[22].ToString();
                    obj.Pact.Name = Reader[23].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[24].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnose.ID = Reader[26].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[27].ToString();
                    if (!Reader.IsDBNull(28))
                        obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());
                    obj.SIMainInfo.OutDiagnose.ID = Reader[29].ToString();
                    obj.SIMainInfo.OutDiagnose.Name = Reader[30].ToString();
                    if (!Reader.IsDBNull(31))
                        obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString());

                    obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());
                    obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());
                    obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());
                    obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());
                    obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());
                    obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());
                    obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString());
                    obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString());
                    obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString());
                    obj.SIMainInfo.Memo = Reader[41].ToString();
                    obj.SIMainInfo.OperInfo.ID = Reader[42].ToString();
                    obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[43].ToString());
                    obj.SIMainInfo.RegNo = Reader[44].ToString();
                    obj.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[45].ToString());
                    obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[46].ToString());
                    obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[47].ToString());
                    obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[48].ToString());
                    obj.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString());
                    obj.SIMainInfo.User01 = Reader[50].ToString();
                    obj.SIMainInfo.User02 = Reader[51].ToString();
                }
                Reader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return null;
            }
        }

        /// <summary>
        /// 得到医保患者基本信息;
        /// </summary>
        /// <param name="inpatientNo"></param>
        ///<param name="invoiceNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetSIPersonInfo(string inpatientNo,string invoiceNo)
        {
            FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.GetSIPersonInfo.Select.invoice_no", ref strSql) == -1)
                return null;
            try
            {
                strSql = string.Format(strSql, inpatientNo, invoiceNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {

                    obj.SIMainInfo.HosNo = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SIMainInfo.BalNo = Reader[2].ToString();
                    obj.SIMainInfo.InvoiceNo = Reader[3].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[4].ToString();
                    if (obj.SIMainInfo.MedicalType.ID == "1")
                        obj.SIMainInfo.MedicalType.Name = "住院";
                    else
                        obj.SIMainInfo.MedicalType.Name = "门诊特定项目";
                    obj.PID.PatientNO = Reader[5].ToString();
                    obj.PID.CardNO = Reader[6].ToString();
                    obj.SSN = Reader[7].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[8].ToString());
                    obj.SIMainInfo.ProceatePcNo = Reader[9].ToString();
                    obj.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    obj.SIMainInfo.SiState = Reader[11].ToString();
                    obj.Name = Reader[12].ToString();
                    obj.Sex.ID = Reader[13].ToString();
                    obj.IDCard = Reader[14].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                    obj.SIMainInfo.EmplType = Reader[16].ToString();
                    obj.CompanyName = Reader[17].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[18].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[19].ToString();
                    obj.PVisit.PatientLocation.Dept.Name = Reader[20].ToString();
                    obj.Pact.PayKind.ID = Reader[21].ToString();
                    obj.Pact.ID = Reader[22].ToString();
                    obj.Pact.Name = Reader[23].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[24].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnose.ID = Reader[26].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[27].ToString();
                    if (!Reader.IsDBNull(28))
                        obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());
                    obj.SIMainInfo.OutDiagnose.ID = Reader[29].ToString();
                    obj.SIMainInfo.OutDiagnose.Name = Reader[30].ToString();
                    if (!Reader.IsDBNull(31))
                        obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString());

                    obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());
                    obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());
                    obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());
                    obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());
                    obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());
                    obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());
                    obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString());
                    obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString());
                    obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString());
                    obj.SIMainInfo.Memo = Reader[41].ToString();
                    obj.SIMainInfo.OperInfo.ID = Reader[42].ToString();
                    obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[43].ToString());
                    obj.SIMainInfo.RegNo = Reader[44].ToString();
                    obj.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[45].ToString());
                    obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[46].ToString());
                    obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[47].ToString());
                    obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[48].ToString());
                    obj.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString());
                }
                Reader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return null;
            }
        }

        /// <summary>
        /// 更新医保结算主表信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateSiMainInfo( FS.HISFC.Models.RADT.PatientInfo obj )
        {
            string strSql = "";
            string balNo = this.GetBalNo( obj.ID );

            if (balNo == null || balNo == "")
            {
                balNo = "0";
            }

            //balNo = (Convert.ToInt32(balNo) + 1).ToString();
            if (this.Sql.GetSql( "Fee.Interface.UpdateSiMainInfo.Update.1", ref strSql ) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql,
                                                    obj.ID,
                                                    balNo, 
                                                    obj.SIMainInfo.InvoiceNo,
                                                    obj.PVisit.MedicalType.ID, 
                                                    obj.PID.PatientNO,
                                                    obj.PID.CardNO, 
                                                    obj.SSN, 
                                                    obj.SIMainInfo.AppNo, 
                                                    obj.SIMainInfo.ProceatePcNo, 
                                                    obj.SIMainInfo.SiBegionDate.ToString(),
                                                    obj.SIMainInfo.SiState, 
                                                    obj.Name, 
                                                    obj.Sex.ID.ToString(), 
                                                    obj.IDCard, 
                                                    "",
                                                    obj.Birthday.ToString(), 
                                                    obj.SIMainInfo.EmplType, 
                                                    obj.CompanyName, 
                                                    obj.SIMainInfo.InDiagnose.Name,
                                                    obj.PVisit.PatientLocation.Dept.ID,
                                                    obj.PVisit.PatientLocation.Dept.Name, 
                                                    obj.Pact.PayKind.ID, obj.Pact.ID, 
                                                    obj.Pact.Name, 
                                                    obj.PVisit.PatientLocation.Bed.ID,
                                                    obj.PVisit.InTime.ToString(), 
                                                    obj.PVisit.InTime.ToString(), 
                                                    obj.SIMainInfo.InDiagnose.ID, 
                                                    obj.SIMainInfo.InDiagnose.Name, 
                                                    obj.PVisit.OutTime,
                                                    obj.SIMainInfo.OutDiagnose.ID, 
                                                    obj.SIMainInfo.OutDiagnose.Name, 
                                                    obj.SIMainInfo.BalanceDate.ToString(), 
                                                    obj.SIMainInfo.TotCost, 
                                                    obj.SIMainInfo.PayCost,
                                                    obj.SIMainInfo.PubCost, 
                                                    obj.SIMainInfo.ItemPayCost, 
                                                    obj.SIMainInfo.BaseCost, 
                                                    obj.SIMainInfo.PubOwnCost, 
                                                    obj.SIMainInfo.ItemYLCost,
                                                    obj.SIMainInfo.OwnCost, 
                                                    obj.SIMainInfo.OverTakeOwnCost,
                                                    obj.SIMainInfo.Memo, 
                                                    this.Operator.ID, 
                                                    obj.SIMainInfo.RegNo,
                                                    obj.SIMainInfo.FeeTimes, 
                                                    obj.SIMainInfo.HosCost, 
                                                    obj.SIMainInfo.YearCost,
                                                    FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid),
                                                    FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced), 
                                                    0, 
                                                    0,
                                                    obj.SIMainInfo.TypeCode
                                                    );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery( strSql );

        }

        /// <summary>
        /// 插入医保结算表(门诊部分)
        /// </summary>
        /// <param name="obj">挂号信息</param>
        /// <returns>-1 失败 1成功</returns>
        public int UpdateSiMainInfo(FS.HISFC.Models.Registration.Register obj)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.UpdateSiMainInfo.Update.1", ref strSql) == -1)
                return -1;

            string balNo = GetBalNo(obj.ID);

            if (balNo == null || balNo == "")
            {
                balNo = "0";
            }

            //balNo = (Convert.ToInt32(balNo) + 1).ToString();


            try
            {
                strSql = string.Format(strSql,
                                            obj.ID, 
                                            balNo, 
                                            obj.SIMainInfo.InvoiceNo, 
                                            obj.SIMainInfo.MedicalType.ID, 
                                            obj.PID.CardNO,
                                            obj.PID.CardNO, 
                                            obj.SSN, 
                                            obj.SIMainInfo.AppNo.ToString(),
                                            obj.SIMainInfo.ProceatePcNo, 
                                            obj.SIMainInfo.SiBegionDate.ToString(),
                                            obj.SIMainInfo.SiState,
                                            obj.Name, 
                                            obj.Sex.ID.ToString(), 
                                            obj.IDCard, 
                                            "",//14      
                                            obj.Birthday.ToString(), 
                                            obj.SIMainInfo.EmplType, 
                                            obj.CompanyName, 
                                            obj.SIMainInfo.ClinicDiagNose,
                                            obj.DoctorInfo.Templet.Dept.ID,
                                            obj.DoctorInfo.Templet.Dept.Name, 
                                            obj.Pact.PayKind.ID, 
                                            obj.Pact.ID, 
                                            obj.Pact.Name, 
                                            "",
                                            obj.DoctorInfo.SeeDate.ToString(), //25
                                            obj.DoctorInfo.SeeDate.ToString(), 
                                            obj.SIMainInfo.InDiagnose.ID, 
                                            obj.SIMainInfo.InDiagnose.Name, 
                                            obj.DoctorInfo.Templet.End.ToString(),
                                            obj.SIMainInfo.OutDiagnose.ID, 
                                            obj.SIMainInfo.OutDiagnose.Name,
                                            obj.SIMainInfo.BalanceDate.ToString(),
                                            obj.SIMainInfo.TotCost.ToString(),
                                            obj.SIMainInfo.PayCost.ToString(),
                                            obj.SIMainInfo.PubCost, 
                                            obj.SIMainInfo.ItemPayCost, 
                                            obj.SIMainInfo.BaseCost, 
                                            obj.SIMainInfo.PubOwnCost, 
                                            obj.SIMainInfo.ItemYLCost,
                                            obj.SIMainInfo.OwnCost, //40
                                            obj.SIMainInfo.OverTakeOwnCost, 
                                            obj.SIMainInfo.Memo, 
                                            this.Operator.ID, 
                                            obj.SIMainInfo.RegNo,
                                            obj.SIMainInfo.FeeTimes, 
                                            obj.SIMainInfo.HosCost,
                                            obj.SIMainInfo.YearCost.ToString(), 
                                            FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid),
                                            FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced),
                                            0, //50
                                            0,
                                            obj.SIMainInfo.TypeCode
                                          );
                return this.ExecNoQuery(strSql);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        #endregion

        #region 临时

        #region 判断该合同单位是否为住院处理的医保

        /// <summary>
        /// 判断该合同单位是否为住院处理的医保(门特)
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public bool IsPactDealByInpatient( string pactCode )
        {
            string strSql = "";

            if (this.Sql.GetSql( "Fee.OutPatient.IsPactDealByInpatient", ref strSql ) == -1)
            {
                this.Err = "Can't Find Sql:Fee.OutPatient.IsPactDealByInpatient";
                return false;
            }

            strSql = System.String.Format( strSql, pactCode );

            return NConvert.ToBoolean( this.ExecSqlReturnOne( strSql ) );
        }

        /// <summary>
        /// 判断该合同单位是否为住院处理的医保（单病种、从化）
        /// 备注必须维护为INPATIENT
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="memo">INPATIENT:住院处理</param>
        /// <returns></returns>
        public bool IsPactDealByInpatient(string pactCode,string memo)
        {
            try
            {           
            string strSql = @"select count(*) from com_dictionary t
                            where t.type = 'PactSiSpecial'
                            and   t.code = '{0}' and t.mark='{1}'";

            strSql = System.String.Format(strSql, pactCode, memo.ToUpper());

            return NConvert.ToBoolean(this.ExecSqlReturnOne(strSql));
            }
            catch 
            {
                return false;
            }
        }

        #endregion

        #region 判断合同单位是否走门慢服务器

        public bool IsPactDealByOtherServer(string pactCode)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.OutPatient.IsPactDealByOtherServer", ref strSql) == -1)
            {
                strSql = @"select count(*) from com_dictionary t
                        where t.type = 'ybmm'
                        and   t.code = '{0}'";
            }

            strSql = System.String.Format(strSql, pactCode);

            return NConvert.ToBoolean(this.ExecSqlReturnOne(strSql));
        }

        #endregion

        #endregion

        #region 医保预结算

        /// <summary>
        /// 更新医保患者预结算费用信息
        /// </summary>
        /// <param name="InpatientNo">住院流水号</param>
        /// <returns>-1失败，0成功</returns>
        public int UpdateSICostForPreBalance( string InpatientNo )
        {
            string strSql = "";
            if (this.Sql.GetSql( "Fee.Interface.UpdateSICostForPreBalance", ref strSql ) == -1)
            {
                this.Err = "没有找到 Fee.Interface.UpdateSICostForPreBalance 字段!";
                this.ErrCode = "-1";
                return -1;
            }
            decimal owncost = 0m;
            decimal pubcost = 0m;
            decimal realOwncost = 0m;
            decimal realPubcost = 0m;
            string Error = "";
            if (this.ExecutePackage( InpatientNo, ref owncost, ref pubcost, ref realOwncost, ref realPubcost, ref Error ) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format( strSql, InpatientNo, owncost.ToString(), pubcost.ToString(),
                    realOwncost.ToString() );
                this.ExecNoQuery( strSql );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 医保预结算
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="myFee"></param>
        /// <param name="refFT"></param>
        /// <returns></returns>
        public bool CalculateSiFee( FS.HISFC.Models.RADT.PatientInfo obj, FS.HISFC.Models.Fee.Inpatient.FeeItemList myFee, FS.HISFC.Models.Base.FT refFT )
        {
            FS.HISFC.Models.RADT.PatientInfo myObj = GetSIPersonInfo( obj.ID );
            if (myObj == null)
            {
                this.Err = "获得患者信息出错!";
                return false;
            }
            if (!this.Calculate( myObj, myFee, refFT ))
            {
                this.Err = "预结算计算失败!";
                return false;
            }
            if (UpdateSiMainInfo( myObj ) == -1)
            {
                this.Err = "更新医保患者结算主表出错!";
                return false;
            }

            obj.SIMainInfo = myObj.SIMainInfo;

            return true;
        }

        #region 执行存储过程

        /// <summary>
        /// 1.获得医保患者计算项目比例后德自费总额和统筹总额 2.按照医保待遇算法后计算德最终自费和统筹费用   
        /// </summary>
        /// <param name="inpatient_no">病人住院流水号</param>
        /// <param name="ownCost">自费金额</param>
        /// <param name="pubCost">公费金额</param>
        /// <param name="realOwnCost">计算比例后自费金额</param>
        /// <param name="realPubCost">计算比例后公费金额</param>
        /// <param name="Error">错误信息</param>
        /// <returns>-1 操作数据库失败 0 成功 </returns>
        public int ExecutePackage( string inpatient_no, ref decimal ownCost, ref decimal pubCost, ref decimal realOwnCost, ref decimal realPubCost, ref string Error )
        {
            //定义字符串 存储SQL语句
            string strSql = "";
            string strReturn = "";
            int iReturn = 0;
            //获取SQL语句
            if (this.Sql.GetSql( "RADT.Inpatient.ExecutePackage.ComputeSICost", ref strSql ) == -1)
            {
                this.Err = "没有找到 RADT.Inpatient.ExecutePackage 字段!";
                this.ErrCode = "-1";
                return -1;
            }
            //格式化字符串
            strSql = string.Format( strSql, inpatient_no, "1", "1", "1", "1", "1", "1" );

            if (this.ExecEvent( strSql, ref strReturn ) == -1)
            {
                this.Err = "执行存储过程出错!PRC_PRE_BALANCE";
                this.ErrCode = "PRC_PRE_BALANCE";
                this.WriteErr();
                return -1;
            }

            string[] s = strReturn.Split( ',' );

            try
            {
                ownCost = FS.FrameWork.Public.String.FormatNumber( FS.FrameWork.Function.NConvert.ToDecimal( s[0] ), 2 );
                pubCost = FS.FrameWork.Public.String.FormatNumber( FS.FrameWork.Function.NConvert.ToDecimal( s[1] ), 2 );
                realOwnCost = FS.FrameWork.Public.String.FormatNumber( FS.FrameWork.Function.NConvert.ToDecimal( s[2] ), 2 );
                realPubCost = FS.FrameWork.Public.String.FormatNumber( FS.FrameWork.Function.NConvert.ToDecimal( s[3] ), 2 );
                iReturn = FS.FrameWork.Function.NConvert.ToInt32( s[4] );
                Error = s[5];
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err += ex.Message;
                return -1;
            }
            return iReturn;
        }

        /// <summary>
        /// （报表用）1.获得医保患者计算项目比例后德自费总额和统筹总额 2.按照医保待遇算法后计算德最终自费和统筹费用   
        /// </summary>
        /// <param name="inpatient_no">病人住院流水号</param>
        /// <param name="ownCost">自费金额</param>
        /// <param name="pubCost">公费金额</param>
        /// <param name="realOwnCost">计算比例后自费金额</param>
        /// <param name="realPubCost">计算比例后公费金额</param>
        /// <param name="drugOwnCost">自费药品</param>
        /// <param name="drugPubCost">统筹药品</param>
        /// <param name="Error">错误信息</param>
        /// <returns>-1 操作数据库失败 0 成功</returns>
        public int ExecutePackage( string inpatient_no, ref decimal ownCost, ref decimal pubCost, ref decimal realOwnCost, ref decimal realPubCost, ref decimal drugOwnCost, ref decimal drugPubCost, ref string Error )
        {
            //定义字符串 存储SQL语句
            string strSql = "";
            string strReturn = "";
            int iReturn = 0;
            //获取SQL语句
            if (this.Sql.GetSql( "RADT.Inpatient.ExecutePackage.ComputeSICost.2", ref strSql ) == -1)
            {
                this.Err = "没有找到 RADT.Inpatient.ExecutePackage 字段!";
                this.ErrCode = "-1";
                return -1;
            }
            //格式化字符串
            strSql = string.Format( strSql, inpatient_no, "1", "1", "1", "1", "1", "1", "1", "1" );

            if (this.ExecEvent( strSql, ref strReturn ) == -1)
            {
                this.Err = "执行存储过程出错!PRC_PRE_BALANCE";
                this.ErrCode = "PRC_PRE_BALANCE";
                this.WriteErr();
                return -1;
            }

            string[] s = strReturn.Split( ',' );

            try
            {
                ownCost = FS.FrameWork.Public.String.FormatNumber( FS.FrameWork.Function.NConvert.ToDecimal( s[0] ), 2 );
                pubCost = FS.FrameWork.Public.String.FormatNumber( FS.FrameWork.Function.NConvert.ToDecimal( s[1] ), 2 );
                realOwnCost = FS.FrameWork.Public.String.FormatNumber( FS.FrameWork.Function.NConvert.ToDecimal( s[2] ), 2 );
                realPubCost = FS.FrameWork.Public.String.FormatNumber( FS.FrameWork.Function.NConvert.ToDecimal( s[3] ), 2 );
                drugOwnCost = FS.FrameWork.Public.String.FormatNumber( FS.FrameWork.Function.NConvert.ToDecimal( s[4] ), 2 );
                drugPubCost = FS.FrameWork.Public.String.FormatNumber( FS.FrameWork.Function.NConvert.ToDecimal( s[5] ), 2 );

                iReturn = FS.FrameWork.Function.NConvert.ToInt32( s[6] );
                Error = s[7];
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err += ex.Message;
                return -1;
            }
            return iReturn;
        }

        #endregion

        /// <summary>
        /// 医保预结算
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="myFeeItemList"></param>
        /// <param name="refFT"></param>
        /// <returns></returns>
        public bool Calculate( FS.HISFC.Models.RADT.PatientInfo obj, FS.HISFC.Models.Fee.Inpatient.FeeItemList myFeeItemList, FS.HISFC.Models.Base.FT refFT )
        {
            if (myFeeItemList == null || myFeeItemList.FT == null)
            {
                return true;
            }

            if (obj.SIMainInfo == null)
            {
                //m_error = "个人信息无效！";
                return false;
            }

            //m_error = "";


            //累计医疗费用

            obj.SIMainInfo.TotCost += myFeeItemList.FT.TotCost;	//上传总金额


            if (myFeeItemList.FT.TotCost == myFeeItemList.FT.OwnCost)
            {
                obj.SIMainInfo.OwnCost += myFeeItemList.FT.OwnCost;//个人自费项目金额
            }
            else
            {
                obj.SIMainInfo.ItemYLCost += myFeeItemList.FT.OwnCost;//个人自付金额（乙类自付部分）
            }



            //获取该类人员的医保支付比例列表
            string pactCode = obj.Pact.ID;		//合同单位代码
            string emplType = obj.SIMainInfo.EmplType;	//人员类别,1在职..

            // 分段支付比例列表

            ArrayList rateList = this.GetAllInsuranceInfo( pactCode, obj.SIMainInfo.EmplType );

            if (rateList.Count == 0)
            {
                //m_error = "医保支付比例列表无效！";
                return false;
            }


            // 纳入医保统筹总费用
            decimal nrzfy = obj.SIMainInfo.TotCost - obj.SIMainInfo.OwnCost - obj.SIMainInfo.ItemYLCost;

            decimal zje1 = 0;
            decimal zje2 = 0;
            decimal bfb = 0;
            decimal gffy1 = 0;
            decimal gffy2 = 0;

            decimal[] zf_fy = new decimal[rateList.Count];	//存放各部分自付金额

            for (int i = 0; i < rateList.Count; i++)
            {
                zf_fy[i] = 0;
            }


            // 按照纳入医保统筹总费用计算各部分统筹/个人支付费用
            for (int i = 0; i < rateList.Count; i++)
            {
                FS.HISFC.Models.SIInterface.Insurance rate = (FS.HISFC.Models.SIInterface.Insurance)rateList[i];

                zje1 = zje2;
                gffy1 = gffy2;

                zje2 = rate.EndCost;	//限额	(decimal)DbAssist.GetItemNumber(drv,"zje",0);
                bfb = rate.Rate * 100;		//比例	(decimal)DbAssist.GetItemNumber(drv,"zfbl",100);

                if (nrzfy <= gffy1)
                    break;

                if (bfb == 100)
                {
                    if (zje2 == 0)
                    {
                        zf_fy[i] += ( nrzfy - gffy1 );
                    }
                    else
                    {
                        if (nrzfy >= zje2)
                        {
                            zf_fy[i] += zje2;
                        }
                        else
                        {
                            zf_fy[i] += nrzfy;
                        }
                    }

                    gffy2 = zf_fy[i]; // needed
                }
                else
                {
                    gffy2 = zje2 / ( 100 - bfb ) * 100 + gffy1;
                    if (nrzfy < gffy2)
                        gffy2 = nrzfy;

                    zf_fy[i] += ( gffy2 - gffy1 ) * bfb / 100;
                }
            }

            ////////////////////////////////////////////////////////////////

            decimal ybzf = 0;	//医保自付金额 = 起付部分 + 共付自付部分 + 超限额自付部分

            for (int i = 0; i < rateList.Count; i++)
            {
                ybzf += zf_fy[i];
            }

            //医保记帐支付金额 = 医疗费用 - 自费部分 - 自付部分
            obj.SIMainInfo.PubCost = obj.SIMainInfo.TotCost - obj.SIMainInfo.OwnCost - obj.SIMainInfo.ItemYLCost - ybzf;


            //obj.SIMainInfo.ItemPayCost	= 0;//部分项目自付金额

            obj.SIMainInfo.BaseCost = ( rateList.Count > 0 ) ? zf_fy[0] : 0;	//个人起付金额
            obj.SIMainInfo.PubOwnCost = ( rateList.Count > 1 ) ? zf_fy[1] : 0;	//个人自付金额

            obj.SIMainInfo.OverTakeOwnCost = 0;				//超统筹支付限额个人自付金额

            for (int i = 2; i < rateList.Count; i++)
            {
                obj.SIMainInfo.OverTakeOwnCost += zf_fy[i];	//超统筹支付限额个人自付金额
            }



            refFT.TotCost = obj.SIMainInfo.TotCost;
            refFT.OwnCost = obj.SIMainInfo.TotCost - obj.SIMainInfo.PubCost;
            refFT.PubCost = obj.SIMainInfo.PubCost;

            return true;

        }

        #endregion

        #region 新版医保本地预算

        #region 医保本地预约结算

        /// <summary>
        /// <summary>
        /// 获得单条已对照信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="objCompare"></param>
        /// <returns></returns>
        public int GetCompareSingleItemNew(string pactCode, string itemCode, ref Compare objCompare)
        {
            string strSql = @"SELECT pact_code,   --合同单位
                     his_code,   --本地项目编码
                     center_code,   --医保收费项目编码
                    -- center_sys_class,   --项目类别 X-西药 Z-中药 L-诊疗项目 F-医疗服务设施
		                 (SELECT fin_xnh_siitem.item_flag FROM fin_xnh_siitem WHERE  fin_xnh_siitem.item_code = FIN_COM_COMPARE.center_code) AS item_flag,
                     center_name,   --医保收费项目中文名称
                     center_ename,   --医保收费项目英文名称
                     center_specs,   --医保规格
                     center_dose,   --医保剂型编码
                     center_spell,   --医保拼音代码
                     center_fee_code,   --医保费用分类代码 1 床位费 2西药费3中药费4中成药5中草药6检查费7治疗费8放射费9手术费10化验费11输血费12输氧费13其他
                     center_item_type,   --医保目录级别 1 基本医疗范围 2 广东省厅补充
                     center_item_grade,   --医保目录等级 1 甲类(统筹全部支付) 2 乙类(准予部分支付) 3 自费
                     center_rate,   --自负比例
                     center_price,   --基准价格
                     center_memo,   --限制使用说明(医保备注)
                     his_spell,   --医院拼音
                     his_wb_code,   --医院五笔码
                     his_user_code,   --医院自定义码
                     his_specs,   --医院规格
                     his_price,   --医院基本价格
                     his_dose,   --医院剂型
                     oper_code,   --操作员
                     oper_date,
                     his_name,
                     REGULARNAME    --操作时间

                FROM fin_com_compare   --医疗保险对照表
                WHERE   pact_code = '{0}'
                AND    his_code = '{1}'
                ";

            try
            {
                strSql = string.Format(strSql, pactCode, itemCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Compare obj = new Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();
                    obj.HisCode = Reader[1].ToString();
                    obj.CenterItem.ID = Reader[2].ToString();
                    obj.CenterItem.SysClass = Reader[3].ToString();
                    obj.CenterItem.Name = Reader[4].ToString();
                    obj.CenterItem.EnglishName = Reader[5].ToString();
                    obj.CenterItem.Specs = Reader[6].ToString();
                    obj.CenterItem.DoseCode = Reader[7].ToString();
                    obj.CenterItem.SpellCode = Reader[8].ToString();
                    obj.CenterItem.FeeCode = Reader[9].ToString();
                    obj.CenterItem.ItemType = Reader[10].ToString();
                    obj.CenterItem.ItemGrade = Reader[11].ToString();
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[22].ToString());
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();


                    al.Add(obj);
                }

                Reader.Close();

                if (al.Count > 0)
                {
                    objCompare = (Compare)al[0];
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新药品费用明细表
        /// 根据处方号，处方号，交易类型
        /// </summary>
        /// <returns></returns>
        public int UpdateMedItemList(FS.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {
            string strSql = @"update fin_ipb_medicinelist a set a.tot_cost={3},a.own_cost={4},a.pub_cost={5},a.pay_cost={6} 
                where a.recipe_no='{0}' and a.sequence_no='{1}' and a.trans_type='{2}'";
            try
            {
                string transType = (item.TransType == FS.HISFC.Models.Base.TransTypes.Positive ? "1" : "2");
                strSql = string.Format(strSql, item.RecipeNO, item.SequenceNO, transType, item.FT.TotCost, item.FT.OwnCost, item.FT.PubCost, item.FT.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exe)
            {
                this.Err = "更新药品费用明细表出错！" + exe.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更新非药品费用明细表
        /// </summary>
        /// 根据处方号，处方号流水号，交易类型
        /// <returns></returns>
        public int UpdateItemList(FS.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {
            string strSql = @"update fin_ipb_itemlist a set a.tot_cost={3},a.own_cost={4},a.pub_cost={5},a.pay_cost={6} 
                    where a.recipe_no='{0}' and a.sequence_no='{1}' and a.trans_type='{2}'";
            try
            {
                string transType = (item.TransType == FS.HISFC.Models.Base.TransTypes.Positive ? "1" : "2");
                strSql = string.Format(strSql, item.RecipeNO, item.SequenceNO, transType, item.FT.TotCost, item.FT.OwnCost, item.FT.PubCost, item.FT.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exe)
            {
                this.Err = "更新非药品费用明细表出错！" + exe.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 获取农保预结算上次执行时间
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public string GetJobLastExecDate(string inpatientNo, string pactCode)
        {
            string strSql = @"select A.lasttime from FIN_XNH_Localbalance A  WHERE  A.inpatientno='{0}' AND A.pactcode='{1}'";
            try
            {
                strSql = string.Format(strSql, inpatientNo, pactCode);
                return this.ExecSqlReturnOne(strSql, "");

            }
            catch (Exception exe)
            {
                this.Err = "获取农保预结算上次执行时间出错！" + exe.Message;
                return "";
            }

        }

        /// <summary>
        /// 获取农保预结算下次执行时间
        /// </summary>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        public string GetJobNextExecDate(string jobCode)
        {
            string strSql = @"select j.next_dtime from com_job j where j.job_code='{0}'";
            try
            {
                strSql = string.Format(strSql, jobCode);
                return this.ExecSqlReturnOne(strSql, "");

            }
            catch (Exception exe)
            {
                this.Err = "获取农保预结算下次执行时间出错！" + exe.Message;
                return "";
            }

        }

        /// <summary>
        /// 插入或更新预结算时间表
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int InsertOrUpdateLocalBalanceTime(FS.HISFC.Models.RADT.PatientInfo patient, DateTime dt)
        {
            string strSql = @"insert into FIN_XNH_Localbalance(inpatientno,pactcode,lasttime,instate) values('{0}','{1}',to_date('{2}','yyyy-MM-dd hh24:mi:ss'),'{3}')";
            try
            {
                strSql = string.Format(strSql, patient.ID, patient.Pact.ID, dt, patient.PVisit.InState.ID.ToString());
                //唯一键错误
                if (-1 == this.ExecNoQuery(strSql))
                {
                    strSql = @"update FIN_XNH_Localbalance set lasttime=to_date('{2}','yyyy-MM-dd hh24:mi:ss'),instate='{3}' where inpatientno='{0}' and pactcode='{1}'";
                    strSql = string.Format(strSql, patient.ID, patient.Pact.ID, dt, patient.PVisit.InState.ID.ToString());
                    return this.ExecNoQuery(strSql);
                }

            }
            catch (Exception exe)
            {
                this.Err = "插入或更新预结算时间表出错！" + exe.Message;
                return -1;
            }
            return 1;

        }

        /// <summary>
        /// 获取费用汇总记录
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <param name="feeCode"></param>
        /// <param name="execDept"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT QueryFeeInfo(string recipeNo, string feeCode, string execDept)
        {
            string strSql = @"SELECT f.tot_cost,f.own_cost,f.pub_cost,f.pay_cost  from fin_ipb_feeinfo f where f.recipe_no ='{0}' and f.fee_code='{1}' and f.execute_deptcode='{2}'";
            try
            {
                strSql = string.Format(strSql, recipeNo, feeCode, execDept);
                this.ExecQuery(strSql);
                FS.HISFC.Models.Base.FT ft = null;
                if (Reader.Read())
                {
                    ft = new FS.HISFC.Models.Base.FT();
                    ft.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                    ft.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                    ft.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                    ft.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
                }
                Reader.Close();
                return ft;
            }
            catch (Exception exp)
            {
                this.Err = "获取费用汇总记录出错" + exp.Message;
                return null;
            }
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
        }

        /// <summary>
        /// 更新费用汇总
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="recipeNo"></param>
        /// <param name="feeCode"></param>
        /// <param name="execDept"></param>
        /// <returns></returns>
        public int UpdateFeeInfo(FS.HISFC.Models.Base.FT ft, string recipeNo, string feeCode, string execDept)
        {
            string strSql = @"update fin_ipb_feeinfo f set f.tot_cost={3},f.own_cost={4},f.pub_cost={5},f.pay_cost={6} 
                where f.recipe_no ='{0}' and f.fee_code='{1}' and f.execute_deptcode='{2}'";
            try
            {
                strSql = string.Format(strSql, recipeNo, feeCode, execDept, ft.TotCost, ft.OwnCost, ft.PubCost, ft.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exp)
            {
                this.Err = "更新费用汇总记录出错" + exp.Message;
                return -1;
            }
        }

        /// <summary>
        /// 获取住院主表信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT QueryInMainInfo(string inpatientNo)
        {
            string strSql = @"select i.tot_cost,i.own_cost,i.pub_cost,i.pay_cost  from fin_ipr_inmaininfo i where i.inpatient_no='{0}'";
            try
            {
                strSql = string.Format(strSql, inpatientNo);
                this.ExecQuery(strSql);
                FS.HISFC.Models.Base.FT ft = null;
                if (Reader.Read())
                {
                    ft = new FS.HISFC.Models.Base.FT();
                    ft.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[0].ToString());
                    ft.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                    ft.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                    ft.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
                }
                Reader.Close();
                return ft;
            }
            catch (Exception exp)
            {
                this.Err = "获取住院主表记录出错" + exp.Message;
                return null;
            }
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
        }

        /// <summary>
        /// 更新住院主表记录
        /// </summary>
        /// <param name="ft"></param>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public int UpdateInMainInfo(FS.HISFC.Models.Base.FT ft, string inpatientNo)
        {
            string strSql = @"update fin_ipr_inmaininfo i set i.tot_cost={1},i.own_cost={2},i.pub_cost={3},i.pay_cost={4} where  i.inpatient_no='{0}'";
            try
            {
                strSql = string.Format(strSql, inpatientNo, ft.TotCost, ft.OwnCost, ft.PubCost, ft.PayCost);
                return this.ExecNoQuery(strSql);

            }
            catch (Exception exp)
            {
                this.Err = "更新住院主表记录出错" + exp.Message;
                return -1;
            }
        }

        #endregion

        #endregion

        #region 医保传明细

        /// <summary>
        /// 获得医保患者要传递的明细信息
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList GetSIPersonDetail( string pactCode, string inpatientNo )
        {
            string strSql = "";
            if (this.Sql.GetSql( "Fee.Interface.GetSIPersonDetail.Select.1", ref strSql ) == -1)
                return null;
            try
            {
                strSql = string.Format( strSql, pactCode, inpatientNo );
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            this.ExecQuery( strSql );
            try
            {
                ArrayList al = new ArrayList();

                while (Reader.Read())
                {
                    Compare obj = new Compare();
                    if (!Reader.IsDBNull( 0 ))
                        obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime( Reader[0].ToString() );
                    obj.ID = Reader[1].ToString();
                    obj.Name = Reader[2].ToString();
                    obj.CenterItem.FeeCode = Reader[3].ToString();
                    obj.Specs = Reader[4].ToString();
                    obj.DoseCode = Reader[5].ToString();
                    //单价
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal( Reader[6].ToString() );
                    //数量
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal( Reader[7].ToString() );
                    //金额
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal( Reader[8].ToString() );

                    al.Add( obj );
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                Reader.Close();
                return null;
            }
        }

        /// <summary>
        /// 回写已上传标志
        /// </summary>
        /// <param name="noteNo">处方号</param>
        /// <param name="seqNo">处方流水号</param>
        /// <param name="flag">项目标记, 1 药品 2 非药品</param>
        /// <returns>-1数据库操作失败, 0 没有找到行(并发), 1 成功</returns>
        public int UpdateUploadedDetailFlag( string noteNo, int seqNo, string flag )
        {
            string strSQL = "";

            if (flag == "1") //药品
            {
                if (this.Sql.GetSql( "Fee.Interface.UpdateUploadedDetailFlag.Drug", ref strSQL ) == -1)
                {
                    this.ErrCode = "-1";
                    this.Err = "获得更新药品上传SQL语句出错!";
                    return -1;
                }

            }
            else if (flag == "2")//非药品
            {
                if (this.Sql.GetSql( "Fee.Interface.UpdateUploadedDetailFlag.Undrug", ref strSQL ) == -1)
                {
                    this.ErrCode = "-1";
                    this.Err = "获得更新非药品上传SQL语句出错!";
                    return -1;
                }
            }
            else
            {
                return 0;
            }

            try
            {
                strSQL = string.Format( strSQL, noteNo, seqNo );
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery( strSQL );
        }

        /// <summary>
        /// 更新医保要上传的明细信息
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="flag">更新标记 0 没有上传 1 已经上传</param>
        /// <returns>-1更新失败 0 没有记录 >=1 更新成功</returns>
        public int UpdateAllDetailFlag( string inpatientNo, string flag )
        {
            string strSqlDrug = "", strSqlItem = "";
            int iReturnDrug = 0, iReturnItem = 0;
            if (this.Sql.GetSql( "Fee.Interface.UpdateAllDetailFlag.Drug", ref strSqlDrug ) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "获得更新药品上传SQL语句出错!";
                return -1;
            }
            if (this.Sql.GetSql( "Fee.Interface.UpdateAllDetailFlag.UnDrug", ref strSqlItem ) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "获得更新非药品上传SQL语句出错!";
                return -1;
            }
            if (FS.FrameWork.Public.String.FormatString( strSqlDrug, out strSqlDrug, inpatientNo, flag ) == -1)
            {
                this.Err += "药品SQL语句负值出错!";
                return -1;
            }
            if (FS.FrameWork.Public.String.FormatString( strSqlItem, out strSqlItem, inpatientNo, flag ) == -1)
            {
                this.Err += "非药品SQL语句负值出错!";
                return -1;
            }
            iReturnDrug = this.ExecNoQuery( strSqlDrug );
            if (iReturnDrug < 0)
            {
                return iReturnDrug;
            }
            iReturnItem = this.ExecNoQuery( strSqlItem );
            if (iReturnItem < 0)
            {
                return iReturnItem;
            }

            return iReturnItem + iReturnDrug;
        }

        /// <summary>
        /// 更新不上传的项目的上传标记
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="condition">不上传项目列表</param>
        /// <param name="flag">0 需要上传 3 不上传  4不确定是否上传</param>
        /// <returns>-1更新失败 0 没有记录 >=1 更新成功</returns>
        public int UpdateFlagForNotUpload( string inpatientNo, string condition, string flag )
        {
            string strSqlDrug = "", strSqlItem = "";
            int iReturnDrug = 0, iReturnItem = 0;
            if (this.Sql.GetSql( "Fee.Interface.UpdateFlagForNotUpload.Drug", ref strSqlDrug ) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "获得更新不上传药品SQL语句出错!";
                return -1;
            }
            if (this.Sql.GetSql( "Fee.Interface.UpdateFlagForNotUpload.UnDrug", ref strSqlItem ) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "获得更新不上传非药品SQL语句出错!";
                return -1;
            }
            //			if(FS.FrameWork.Public.String.FormatString(strSqlDrug, out strSqlDrug, inpatientNo, flag) == -1)
            //			{
            //				this.Err += "药品SQL语句赋值出错!";
            //				return -1;
            //			}
            //			if(FS.FrameWork.Public.String.FormatString(strSqlItem, out strSqlItem, inpatientNo, flag) == -1)
            //			{
            //				this.Err += "非药品SQL语句赋值出错!";
            //				return -1;
            //			}
            try
            {
                strSqlDrug = string.Format( strSqlDrug, inpatientNo, flag, condition );
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                return -1;
            }
            try
            {
                strSqlItem = string.Format( strSqlItem, inpatientNo, flag, condition );
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                return -1;
            }
            iReturnDrug = this.ExecNoQuery( strSqlDrug );
            if (iReturnDrug < 0)
            {
                return iReturnDrug;
            }
            iReturnItem = this.ExecNoQuery( strSqlItem );
            if (iReturnItem < 0)
            {
                return iReturnItem;
            }

            //string sqlDrug = "", sqlItem = "";
            //int drugNum = 0, itemNum = 0;
            //if (this.Sql.GetSql("Fee.Interface.UpdateFlagForWhetherUpload.Drug", ref sqlDrug) == -1)
            //{
            //    this.ErrCode = "-1";
            //    this.Err = "获得更新不上传药品SQL语句出错!";
            //    return -1;
            //}
            //if (this.Sql.GetSql("Fee.Interface.UpdateFlagForWhetherUpload.UnDrug", ref sqlItem) == -1)
            //{
            //    this.ErrCode = "-1";
            //    this.Err = "获得更新不上传非药品SQL语句出错!";
            //    return -1;
            //}
            //			if(FS.FrameWork.Public.String.FormatString(strSqlDrug, out strSqlDrug, inpatientNo, flag) == -1)
            //			{
            //				this.Err += "药品SQL语句赋值出错!";
            //				return -1;
            //			}
            //			if(FS.FrameWork.Public.String.FormatString(strSqlItem, out strSqlItem, inpatientNo, flag) == -1)
            //			{
            //				this.Err += "非药品SQL语句赋值出错!";
            //				return -1;
            //			}
            //try
            //{
            //    sqlDrug = string.Format(sqlDrug, inpatientNo);
            //}
            //catch (Exception ex)
            //{
            //    this.ErrCode = "-1";
            //    this.Err = ex.Message;
            //    return -1;
            //}
            //try
            //{
            //    sqlItem = string.Format(sqlItem, inpatientNo);
            //}
            //catch (Exception ex)
            //{
            //    this.ErrCode = "-1";
            //    this.Err = ex.Message;
            //    return -1;
            //}
            //drugNum = this.ExecNoQuery(sqlDrug);
            //if (drugNum < 0)
            //{
            //    return drugNum;
            //}
            //itemNum = this.ExecNoQuery(sqlItem);
            //if (itemNum < 0)
            //{
            //    return itemNum;
            //}

            return iReturnItem + iReturnDrug;
        }

        /// <summary>
        ///  更新不上传项目的上传标志
        /// </summary>
        /// <param name="feeItem"></param>
        /// <returns></returns>
        public int UpdateFlagForNotUpload(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem)
        {
            if (feeItem.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                return this.ExecNoQueryByIndex("Fee.Interface.UpdateNotUploadedDetailFlag.Drug", feeItem.RecipeNO, feeItem.SequenceNO.ToString());
            }
            else
            {
                return this.ExecNoQueryByIndex("Fee.Interface.UpdateNotUploadedDetailFlag.Undrug", feeItem.RecipeNO, feeItem.SequenceNO.ToString());
            }
        }

        /// <summary>
        ///  更新不上传项目的上传标志
        /// </summary>
        /// <param name="feeItem"></param>
        /// <param name="flag">0 未上传  1已上传 3不需要上传</param>
        /// <returns></returns>
        public int UpdateUploadFlag(string inpatientNO, DateTime beginTime, DateTime endTime, string flag, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem)
        {
            if (feeItem.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                string sql = @" update fin_ipb_medicinelist a
                                            set a.upload_flag = '{3}'
                                            where inpatient_no = '{0}'
                                            and balance_state='0'
				                            and charge_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
				                            and charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                            and drug_code='{4}'
                                            ";
                return this.ExecNoQuery(sql, inpatientNO, beginTime.ToString(), endTime.ToString(), flag, feeItem.Item.ID);
            }
            else
            {
                string sql = @" update fin_ipb_itemlist a
                                            set a.upload_flag = '{3}'
                                            where inpatient_no = '{0}'
                                            and balance_state='0'
				                            and charge_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
				                            and charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                            and item_code='{4}'
                                            and split_fee_flag='0'
                                            ";
                return this.ExecNoQuery(sql, inpatientNO, beginTime.ToString(), endTime.ToString(), flag, feeItem.Item.ID);
            }
        }

        #endregion

        #region 医保报表

        /// <summary>
        /// 获得指定月份之间的科室医保限额
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="inState">在院状态</param>
        /// <param name="pactCode">合同单位</param>
        /// <param name="ds">返回的显示信息</param>
        /// <returns>-1 失败 0 成功</returns>
        public int QueryDeptSiCost( DateTime dtBegin, DateTime dtEnd, string inState, string pactCode, ref DataSet ds )
        {
            string strSql = "";
            if (inState == "B")
            {
                if (this.Sql.GetSql( "Fee.Interface.QueryDeptSiCost.DataSet.OUT", ref strSql ) == -1)
                {
                    this.Err += "获得SQL语句出错!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, inState, dtBegin.ToString(), dtEnd.ToString(), pactCode ) == -1)
                {
                    this.Err += "SQL语句负值出错!";
                    return -1;
                }
            }
            if (inState == "I")
            {
                if (this.Sql.GetSql( "Fee.Interface.QueryDeptSiCost.DataSet.IN", ref strSql ) == -1)
                {
                    this.Err += "获得SQL语句出错!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, pactCode ) == -1)
                {
                    this.Err += "SQL语句负值出错!";
                    return -1;
                }
            }

            return this.ExecQuery( strSql, ref ds );
        }

        /// <summary>
        /// 获得一段时间内的病区医保患者用药情况
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="inState">在院状态</param>
        /// <param name="pactCode">合同单位</param>
        /// <param name="ds">返回的显示信息</param>
        /// <returns>-1 失败 0 成功</returns>
        public int QuerySIDeptDrug( DateTime dtBegin, DateTime dtEnd, string inState, string pactCode, ref DataSet ds )
        {
            string strSql = "";
            if (inState == "B")
            {
                if (this.Sql.GetSql( "Fee.Interface.QuerySIDeptDrug.DataSet.OUT", ref strSql ) == -1)
                {
                    this.Err += "获得SQL语句出错!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, dtBegin.ToString(), dtEnd.ToString(), pactCode ) == -1)
                {
                    this.Err += "SQL语句负值出错!";
                    return -1;
                }
            }
            if (inState == "I")
            {
                if (this.Sql.GetSql( "Fee.Interface.QuerySIDeptDrug.DataSet.IN", ref strSql ) == -1)
                {
                    this.Err += "获得SQL语句出错!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, pactCode ) == -1)
                {
                    this.Err += "SQL语句负值出错!";
                    return -1;
                }
            }

            return this.ExecQuery( strSql, ref ds );
        }

        /// <summary>
        /// 查询指定病区的医保患者的药品信息
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="deptCode">病区代码</param>
        /// <param name="inState">在院状态</param>
        /// <param name="pactCode">合同单位</param>
        /// <param name="ds">显示信息DataSet</param>
        /// <returns>-1 失败 0 成功</returns>
        public int QuerySIPateintDrugForDept( DateTime dtBegin, DateTime dtEnd, string deptCode, string inState, string pactCode, ref DataSet ds )
        {
            string strSql = "";
            if (inState == "O")
            {
                if (this.Sql.GetSql( "Fee.Interface.QuerySIPateintDrugForDept.DataSet.OUT", ref strSql ) == -1)
                {
                    this.Err += "获得SQL语句出错!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, deptCode, dtBegin.ToString(), dtEnd.ToString(), pactCode ) == -1)
                {
                    this.Err += "SQL语句负值出错!";
                    return -1;
                }
            }
            if (inState == "I")
            {
                if (this.Sql.GetSql( "Fee.Interface.QuerySIPateintDrugForDept.DataSet.IN", ref strSql ) == -1)
                {
                    this.Err += "获得SQL语句出错!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, deptCode, pactCode, dtBegin.ToString(), dtEnd.ToString() ) == -1)
                {
                    this.Err += "SQL语句负值出错!";
                    return -1;
                }
            }

            return this.ExecQuery( strSql, ref ds );
        }

        /// <summary>
        /// 查询指定病区的医保患者的药品信息, 报表2
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="deptCode">病区代码</param>
        /// <param name="inState">在院状态</param>
        /// <param name="pactCode">合同单位</param>
        /// <param name="ds">显示信息DataSet</param>
        /// <returns>-1 失败 0 成功</returns>
        public int QuerySIPateintDrugForDeptSec( DateTime dtBegin, DateTime dtEnd, string deptCode, string inState, string pactCode, ref DataSet ds )
        {
            string strSql = "";
            if (inState == "O")
            {
                if (this.Sql.GetSql( "Fee.Interface.QuerySIPateintDrugForDeptSec.DataSet.OUT", ref strSql ) == -1)
                {
                    this.Err += "获得SQL语句出错!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, deptCode, dtBegin.ToString(), dtEnd.ToString(), pactCode ) == -1)
                {
                    this.Err += "SQL语句负值出错!";
                    return -1;
                }
            }
            if (inState == "I")
            {
                if (this.Sql.GetSql( "Fee.Interface.QuerySIPateintDrugForDeptSec.DataSet.IN", ref strSql ) == -1)
                {
                    this.Err += "获得SQL语句出错!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, deptCode, pactCode, dtBegin.ToString(), dtEnd.ToString() ) == -1)
                {
                    this.Err += "SQL语句负值出错!";
                    return -1;
                }
            }

            return this.ExecQuery( strSql, ref ds );
        }

        /// <summary>
        ///	获得出院结算的医保患者信息
        /// </summary>
        /// <param name="dtBegin">住院登记开始时间</param>
        /// <param name="dtEnd">住院登记结束时间</param>
        /// <param name="pactCode">合同单位代码</param>
        /// <returns></returns>
        public ArrayList QueryOutHosPatients( DateTime dtBegin, DateTime dtEnd, string pactCode )
        {
            string strSql = "";
            if (this.Sql.GetSql( "Fee.Interface.QueryOutHosPatients.Select.1", ref strSql ) == -1)
            {
                this.Err += "获得SQL语句出错!";
                return null;
            }
            if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, dtBegin.ToString(), dtEnd.ToString(), pactCode ) == -1)
            {
                this.Err += "SQL语句负值出错!";
                return null;
            }
            this.ExecQuery( strSql );
            ArrayList al = new ArrayList();
            string temp = "";
            try
            {
                while (Reader.Read())
                {
                    temp = Reader[0].ToString();
                    al.Add( temp );
                }

                this.Reader.Close();
                return al;
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.Err += ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获得当前所有在院医保患者
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="pactCode">合同单位代码</param>
        /// <returns>null 失败 ArrayList.Count > 1 成功</returns>
        public ArrayList QueryInHosPatients( DateTime dtBegin, DateTime dtEnd, string pactCode )
        {
            string strSql = "";
            if (this.Sql.GetSql( "Fee.Interface.QueryInHosPatient.Select.1", ref strSql ) == -1)
            {
                this.Err += "获得SQL语句出错!";
                return null;
            }
            if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, dtBegin.ToString(), dtEnd.ToString(), pactCode ) == -1)
            {
                this.Err += "SQL语句负值出错!";
                return null;
            }
            this.ExecQuery( strSql );
            ArrayList al = new ArrayList();
            string temp = "";
            try
            {
                while (Reader.Read())
                {
                    temp = Reader[0].ToString();
                    al.Add( temp );
                }

                this.Reader.Close();
                return al;
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.Err += ex.Message;
                return null;
            }
        }

        /// <summary>
        ///	获得出院结算的医保患者信息
        /// </summary>
        /// <param name="dtBegin">住院登记开始时间</param>
        /// <param name="dtEnd">住院登记结束时间</param>
        /// <param name="pactCode">合同单位代码</param>
        /// <param name="sqlWhere">科室代码</param>
        /// <returns></returns>
        public ArrayList QueryOutHosPatients( DateTime dtBegin, DateTime dtEnd, string pactCode, string sqlWhere )
        {
            string strSql = "";
            if (this.Sql.GetSql( "Fee.Interface.QueryOutHosPatients.Select.1", ref strSql ) == -1)
            {
                this.Err += "获得SQL语句出错!";
                return null;
            }
            if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, dtBegin.ToString(), dtEnd.ToString(), pactCode ) == -1)
            {
                this.Err += "SQL语句负值出错!";
                return null;
            }
            strSql = strSql + sqlWhere;
            this.ExecQuery( strSql );
            ArrayList al = new ArrayList();
            string temp = "";
            try
            {
                while (Reader.Read())
                {
                    temp = Reader[0].ToString();
                    al.Add( temp );
                }

                this.Reader.Close();
                return al;
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.Err += ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获得当前所有在院医保患者
        /// </summary>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <param name="pactCode">合同单位代码</param>
        /// <param name="sqlWhere">科室代码</param>
        /// <returns>null 失败 ArrayList.Count > 1 成功</returns>
        public ArrayList QueryInHosPatients( DateTime dtBegin, DateTime dtEnd, string pactCode, string sqlWhere )
        {
            string strSql = "";
            if (this.Sql.GetSql( "Fee.Interface.QueryInHosPatient.Select.1", ref strSql ) == -1)
            {
                this.Err += "获得SQL语句出错!";
                return null;
            }
            if (FS.FrameWork.Public.String.FormatString( strSql, out strSql, dtBegin.ToString(), dtEnd.ToString(), pactCode ) == -1)
            {
                this.Err += "SQL语句负值出错!";
                return null;
            }
            strSql = strSql + sqlWhere;
            this.ExecQuery( strSql );
            ArrayList al = new ArrayList();
            string temp = "";
            try
            {
                while (Reader.Read())
                {
                    temp = Reader[0].ToString();
                    al.Add( temp );
                }

                this.Reader.Close();
                return al;
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.Err += ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 查询医保病人费用构成
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="state"></param>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public DataSet QueryFeeStruct( string dtBegin, string dtEnd, string[] state, string reportName )
        {
            string strSql = "";
            DataSet dsFee = new DataSet();
            if (this.Sql.GetSql( "Fee.Interface.QueryFeeStruct", ref strSql ) == -1)
            {
                this.Err = "Can't Find The SqlExpression";
                return null;
            }
            strSql = System.String.Format( strSql, reportName, dtBegin, dtEnd, state[0] + "','" + state[1] );
            this.ExecQuery( strSql, ref dsFee );
            return dsFee;
        }

        /// <summary>
        /// 按照科室或者全院查询社会医疗保险定点机构医疗费用病区明细
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public DataSet QuerySIDeptFee( string dtBegin, string dtEnd, string DeptCode )
        {
            string strSql = "";
            DataSet dsFee = new DataSet();
            FS.HISFC.BizLogic.Manager.Department manager = new FS.HISFC.BizLogic.Manager.Department();
            if (this.Sql.GetSql( "Fee.Interface.QuerySIDeptFee", ref strSql ) == -1)
            {
                this.Err = "Can't Find the Sql";
                return null;
            }
            if (DeptCode == "")
            {
                strSql = System.String.Format( strSql, dtBegin, dtEnd );
                strSql += " ORDER BY A.DEPT_NAME";
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = DeptCode;
                ArrayList alDept = manager.GetDeptFromNurseStation( obj );
                DeptCode = "";
                for (int i = 0; i < alDept.Count; i++)
                {
                    DeptCode += ( alDept[i] as FS.FrameWork.Models.NeuObject ).ID + "','";
                }
                DeptCode = DeptCode.Substring( 0, DeptCode.Length - 2 );
                strSql += " AND A.DEPT_CODE IN( '" + DeptCode + ")";
                strSql += " ORDER BY A.DEPT_NAME";
                strSql = System.String.Format( strSql, dtBegin, dtEnd );
            }
            this.ExecQuery( strSql, ref dsFee );
            return dsFee;
        }

        /// <summary>
        /// 查询医保费用对帐明细
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public DataSet QueryFeeCollateDetail( string dtBegin, string dtEnd, string reportName )
        {
            string strSql = "";
            DataSet dsFee = new DataSet();
            if (this.Sql.GetSql( "Fee.Interface.QueryFeeCollateDetail", ref strSql ) == -1)
            {
                this.Err = "Can't Find The Sql";
                return null;
            }
            strSql = System.String.Format( strSql, dtBegin, dtEnd, reportName );
            this.ExecQuery( strSql, ref dsFee );
            return dsFee;
        }

        /// <summary>
        /// 查询医保费用结算申请信息
        /// </summary>
        /// <param name="dtBegin">起始日期</param>
        /// <param name="dtEnd">终止日期</param>
        /// <param name="state">住院or门诊特定项目</param>
        /// <param name="empl_type">在职or离休</param>
        /// <returns></returns>
        public DataSet QueryStatFeePactByInsure( string dtBegin, string dtEnd, string[] state, string[] empl_type )
        {
            string strSql = "";
            DataSet ds = new DataSet();
            if (this.Sql.GetSql( "Fee.Interface.QueryStatFeePactByInsure", ref strSql ) == -1)
            {
                this.Err = "Can't Find Sql";
                return null;
            }
            strSql = System.String.Format( strSql, dtBegin, dtEnd, state[0] + "','" + state[1], empl_type[0] + "','" + empl_type[1] );
            this.ExecQuery( strSql, ref ds );
            return ds;
        }

        #endregion

        #region 医保项目显示

        /// <summary>
        /// 获得项目标记
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string ShowItemFlag( FS.HISFC.Models.Base.Item item )
        {
            bool b1, b2, b3, b4;
            if (item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item o = item as FS.HISFC.Models.Pharmacy.Item;
                b1 = FS.FrameWork.Function.NConvert.ToBoolean( o.SpecialFlag );
                b2 = FS.FrameWork.Function.NConvert.ToBoolean( o.SpecialFlag1 );
                b3 = FS.FrameWork.Function.NConvert.ToBoolean( o.SpecialFlag2 );
                b4 = FS.FrameWork.Function.NConvert.ToBoolean( o.SpecialFlag3 );
            }
            else
            {
                FS.HISFC.Models.Fee.Item.Undrug o = item as FS.HISFC.Models.Fee.Item.Undrug;

                b1 = FS.FrameWork.Function.NConvert.ToBoolean( o.SpecialFlag );
                b2 = FS.FrameWork.Function.NConvert.ToBoolean( o.SpecialFlag1 );
                b3 = FS.FrameWork.Function.NConvert.ToBoolean( o.SpecialFlag2 );
                b4 = FS.FrameWork.Function.NConvert.ToBoolean( o.SpecialFlag3 );
            }
            string s = "";
            if (b1)
                s = s + "X";
            if (b2)
                s = s + "S";
            if (b3)
                s = s + "Z";
            if (b4)
                s = s + "T";
            if (s != "")
                s = "[" + s + "]";
            return s;
        }

        /// <summary>
        /// 获得项目类别 甲,..
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public string ShowItemGrade( string ItemCode )
        {
            FS.HISFC.Models.SIInterface.Compare obj = null;
            int iReturn = this.GetCompareSingleItem( "2", ItemCode, ref obj );
            if (iReturn == -1)
            {
                return "获得医保比例出错！";
            }
            else if (iReturn == -2)//没对照
            {
                return "自费【100%】";
            }
            else
            {
                switch (int.Parse( obj.CenterItem.ItemGrade ))
                {
                    case 1:
                        return "甲类【" + ( obj.CenterItem.Rate * 100 ).ToString() + "%】";
                    case 2:
                        return "乙类【" + ( obj.CenterItem.Rate * 100 ).ToString() + "%】";
                    case 3:
                        return "自费【" + ( obj.CenterItem.Rate * 100 ).ToString() + "%】";
                    default:
                        break;
                }
            }
            return "";

        }

        /// <summary>
        /// 传入ItemGrade编码，返回甲，乙，丙类
        /// wolf 添加,静态的大家都可以用
        /// </summary>
        /// <param name="itemGrade"></param>
        /// <returns></returns>
        public static string ShowItemGradeByCode( string itemGrade )
        {
            if (itemGrade == "1")
            {
                return "甲类";
            }
            else if (itemGrade == "2")
            {
                return "乙类";
            }
            else if (itemGrade == "3")
            {
                return "自费";
            }

            return "未知";

        }

        #endregion

        # region 深圳医保结算返回明细

        /* 先注释20111227
        string[] GetParms(FS.HISFC.Models.Fee.SIBalanceList siB)
        {
            //INPATIENTNO, FEECODE, COST, BALANCEDATE, VALID_STATE, TYPE
            string[] s = new string[6];
            s[0] = siB.InpatientNo;
            s[1] = siB.FeeCode;
            s[2] = siB.Cost.ToString();
            s[3] = siB.Balancedate.ToString();
            s[4] = siB.ValidState;
            s[5] = siB.Type;
            return s;
        }

        public int UpdateSiBalanceList(FS.HISFC.Models.Fee.SIBalanceList siB)
        {
            string sql = @"UPDATE FIN_COM_SIBANLANCELIST
                SET INPATIENTNO = '{1}', 
                COST = {2}, 
                FEECODE = '{3}', 
                BALANCEDATE = '{4}', 
                VALID_STATE = '{5}'
                WHERE INPATIENTNO = '{0}' and type = '{6}'";
            return this.ExecNoQuery(sql, GetParms(siB));
        }

        public int InsertSiBalanceList(FS.HISFC.Models.Fee.SIBalanceList siB)
        {
            string sql = @"INSERT INTO FIN_COM_SIBANLANCELIST
                (INPATIENTNO, FEECODE, COST, BALANCEDATE, VALID_STATE, TYPE) 
                VALUES ('{0}', '{1}', {2}, '{3}', '{4}', '{5}')";
            return this.ExecNoQuery(sql, GetParms(siB));
        }

        public int UpdateSiBalanceListValidState(string inpatientNO, string validState)
        {
            string sql = @"update FIN_COM_SIBANLANCELIST
                set VALID_STATE = '{1}'
                where INPATIENTNO = '{0}'";
            return this.ExecNoQuery(sql, inpatientNO, validState);
        }

        public ArrayList QuerySiBalanceList(string inpatientNO, string validState,string type)
        {
            string sql = @"select INPATIENTNO, FEECODE, COST, BALANCEDATE, VALID_STATE, TYPE 
                from FIN_COM_SIBANLANCELIST
                where NPATIENTNO = '{0}' and valid_state = '{1}' and type = '{2}'";
            if (this.ExecQuery(sql, inpatientNO, validState, type) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                int i = 0;
                FS.HISFC.Models.Fee.SIBalanceList siB = new SIBalanceList();
                siB.InpatientNo = this.Reader[i++].ToString();
                siB.FeeCode = this.Reader[i++].ToString();
                siB.Cost = FS.FrameWork.Function.NConvert.ToDecimal( this.Reader[i++].ToString());
                siB.Balancedate = FS.FrameWork.Function.NConvert.ToDateTime( this.Reader[i++].ToString());
                siB.ValidState = this.Reader[i++].ToString();
                siB.Type = this.Reader[i++].ToString();

                al.Add(type);
            }
            return al;
           
        }
       
       */
        #endregion

        #region 深圳医保对照维护

        ////根据HIS_CODE得到医保码
        //public DataSet GetYBCode(string his_code)
        //{
        //    string sql = "select pact_code,center_code,his_user_code from fin_com_compare where his_code='{0}'";
        //    return this.ExecQuery(sql, his_code);
        //}
        ////插入HIS_CODE，HIS_User_code,center_code到对照表
        //public int  InsertYBCode(string pact_code ,string his_code ,string his_user_code,string center_code)
        //{
        //    string sql = "insert into fin_com_compare ( pact_code,his_code,his_user_code,center_code ) values ('{0}','{1}','{2}','{3}')";
        //    return this.ExecNoQuery(sql, pact_code, his_code, his_user_code, center_code);
        //}
        //// 更新his_code对照的医保码
        //public int UpdateYBCode(string pact_code, string his_code, string his_user_code, string center_code)
        //{
        //    string sql = "update fin_com_compare set his_user_code='{0}',center_code='{1}' where pact_code='{2}' and his_code='{3}'";
        //    return this.ExecNoQuery(sql, his_user_code, center_code,pact_code, his_code);
        //}

        #endregion

        /// <summary>
        /// 根据合同单位获取可报销的药品
        /// </summary>
        /// <returns></returns>
        public System.Collections.Generic.Dictionary<string, string> GetInsurDrugItem(string pactCode)
        { 
            string strSql = "";
            if (this.Sql.GetSql("DoctorWorkStation.GetInsurDrugItem", ref strSql) == -1)
                return null;
            try
            {
                strSql = string.Format(strSql, pactCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            this.ExecQuery(strSql);
            try
            {
                System.Collections.Generic.Dictionary<string, string> dicInsItems = new System.Collections.Generic.Dictionary<string, string>();
                while (Reader.Read())
                {
                    if (!Reader.IsDBNull(0) && !Reader.IsDBNull(1))
                    {
                        string hisCode = Reader[0].ToString().TrimEnd().TrimStart();
                        string hisName = Reader[1].ToString().TrimEnd().TrimStart();
                        if (!dicInsItems.ContainsKey(hisCode))
                        {
                            dicInsItems.Add(hisCode, hisName);
                        }
                    }
                }
                Reader.Close();
                return dicInsItems;
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                Reader.Close();
                return null;
            }
        }

        /// <summary>
        /// 市公医医保算法
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public string SGYBdll(string pactCode)
        {
            string strSql = @"select mark from com_dictionary aa where aa.type='SGYYB' and code='{0}'";
            try
            {
                strSql = string.Format(strSql, pactCode);
                return base.ExecSqlReturnOne(strSql,"");
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// 获取广州医保的基准合同单位
        /// </summary>
        /// <param name="type"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        public string GetGzSiParamPactCode(string type, string mark)
        {
            string pactCode = string.Empty;
            string sql = "select code from com_dictionary d where d.type='{0}' and d.mark='{1}'";
            try
            {
                sql = string.Format(sql, type, mark);

                this.ExecQuery(sql);
                while (this.Reader.Read())
                {
                    pactCode = this.Reader[0].ToString();
                }
                this.Reader.Close();
                return pactCode;
            }
            catch (Exception ex)
            {
                this.Err = "获取广州医保的基准合同单位！ type:" + type + " mark:" + mark + "/n" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// //{0A47824B-8679-48e2-943B-8A893B36D076} zhang-wx 神奇的医院，无语了，住院医保上传读取api的xml配置文件中的医院编码
        /// 获取API医院编码
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetApiHosIdXmlSetting()
        {

            //filePath=@"D:\广州市第八人民医院\lib\PlugIns\MedicareInterface\SiApiSetting.xml";
            string HosId = "";
            string filePath = @".\profile\SiApiSetting.xml";//医保Api调用开关设置;
            if (!System.IO.File.Exists(filePath))
            {
                return HosId;
            }
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                System.IO.StreamReader sr = new System.IO.StreamReader(filePath, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                xmlDoc.LoadXml(cleandown);
                sr.Close();
                XmlNode rootNode = xmlDoc.SelectSingleNode("//API开关");
                HosId = rootNode.Attributes["医院编码"].Value.Trim().ToString();
                if (HosId != null && HosId.Trim() != "")
                {
                    return HosId;
                }
                else
                {
                    return HosId;
                }

            }
            catch (Exception ex)
            {
                return HosId;
            }
            return HosId;
        }

        #region 存储医保前置机明细信息

        /// <summary>
        /// 医院编码
        /// </summary>
        public string HosCode = string.Empty;

        /// <summary>
        /// 插入多条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="feeDetails">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertShareData(FS.HISFC.Models.Registration.Register r, ArrayList feeDetails, DateTime operDate)
        {
            int iReturn = 0;
            int uploadRows = 0;
            try
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    iReturn = DeleteShareData(r, f);
                    if (iReturn < 0)
                    {
                        this.Err += "删除历史费用明细失败!";
                        return -1;
                    }

                    iReturn = InsertShareData(r, f, operDate);
                    if (iReturn <= 0)
                    {
                        this.Err += "插入明细失败!";
                        return -1;
                    }

                    ////处理限制用药信息
                    //if (!string.IsNullOrEmpty(f.Item.User03))
                    //{
                    //    iReturn = this.InsertIndicationsShareData(r, f, operDate);
                    //    if (iReturn <= 0)
                    //    {
                    //        this.Err += "插入医保限制性用药明细失败!";
                    //        return -1;
                    //    }
                    //}

                    uploadRows += iReturn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return uploadRows;
        }

        /// <summary>
        /// 插入单条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertShareData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, DateTime operDate)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            string strSQL = @"INSERT INTO GZSI_HIS_MZXM     --广州医保费用明细信息表
                                        (
                                        JYDJH,   --就医登记号
                                        YYBH,   --医院编号
                                        GMSFHM,   --公民身份证号
                                        ZYH,   --住院号/门诊号
                                        RYRQ,   --挂号/入院时间
                                        FYRQ,   --收费时间
                                        XMXH,   --项目序号
                                        XMBH,   --医院的项目编号
                                        XMMC,   --医院的项目名称
                                        FLDM,   --分类代码
                                        YPGG,   --规格
                                        YPJX,   --剂型
                                        JG,   --单价
                                        MCYL,   --数量
                                        JE,   --金额
                                        BZ1,   --备注1，存放记录产生时间
                                        BZ2,   --备注2
                                        BZ3,   --备注3,存放费用明细读入时有效性检查的处理结果代码
                                        DRBZ,   --读入标志
                                        YPLY,   --1-国产 2-进口 3-合资
                                        CLINIC_CODE,   --门诊就诊流水号
                                        CARD_NO,   --门诊号
                                        OPER_CODE,   --操作员
                                        OPER_DATE,   --操作时间
                                        INVOICE_NO,   --发票号
                                        FYPC   --费用批次
                                        ) 
                                        VALUES
                                        (
                                        '{0}',   --就医登记号
                                        '{1}',   --医院编号
                                        '{2}',   --公民身份证号
                                        '{3}',   --住院号/门诊号
                                        TO_DATE('{4}','YYYY-MM-DD HH24:MI:SS'),   --挂号/入院时间
                                        TO_DATE('{5}','YYYY-MM-DD HH24:MI:SS'),   --收费时间
                                        '{6}',   --项目序号
                                        '{7}',   --医院的项目编号
                                        '{8}',   --医院的项目名称
                                        '{9}',   --分类代码
                                        '{10}',   --规格
                                        '{11}',   --剂型
                                        '{12}',   --单价
                                        '{13}',   --数量
                                        '{14}',   --金额
                                        '{15}',   --备注1，存放记录产生时间
                                        '{16}',   --备注2
                                        '{17}',   --备注3,存放费用明细读入时有效性检查的处理结果代码
                                        '{18}',   --读入标志
                                        '{19}',   --1-国产   2-进口3-合资
                                        '{20}',   --门诊就诊流水号
                                        '{21}',   --门诊号
                                        '{22}',   --操作员
                                        sysdate,   --操作时间
                                        '{23}',   --发票号
                                        '{24}'   --费用批次
                                        ) ";
            string name = "";
            name = f.Item.Name;
            if (name.Length > 20)
            {
                name = name.Substring(0, 20);
            }
            try
            {
                decimal price = this.GetPrice(f.Item);
                decimal totCost = f.Item.Qty * price;

                strSQL = string.Format(strSQL,
                    r.SIMainInfo.RegNo,
                    r.SIMainInfo.HosNo,
                    r.IDCard,
                    r.PID.CardNO,
                    r.DoctorInfo.SeeDate.ToString(),
                    operDate.ToString(),
                    f.Order.SequenceNO.ToString(),
                    f.Item.UserCode,
                    name,
                    "000",
                    f.Item.Specs,
                    r.MainDiagnose, //诊断
                    FS.FrameWork.Public.String.FormatNumber(price / f.Item.PackQty, 4),
                    f.Item.Qty,                 //总量Amount
                    totCost,//(f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString(),
                    "",
                    "",
                    "",
                    "0",
                    "", 
                    r.ID,
                    r.PID.CardNO,
                    this.Operator.ID,
                    r.SIMainInfo.InvoiceNo,
                    r.SIMainInfo.FeeTimes.ToString()
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 插入单条适应症信息
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertIndicationsShareData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, DateTime operDate)
        {
            decimal price = this.GetPrice(f.Item);
            string sql1 = "insert into HIS_CFXM_JKSYBZ values('" + r.SIMainInfo.RegNo + "','" +
                                                              r.SIMainInfo.HosNo + "','" +
                                                              r.IDCard + "','" +
                                                                "','" +
                                                              f.Order.ID.ToString() + "','" +
                                                              f.Item.UserCode + "','" +
                                                              f.Item.Name + "'," +
                                                              f.Item.Qty.ToString() + "," +
                                                              f.FT.TotCost.ToString() + ",'" +
                                                              f.Item.User03.ToString() + "','" +
                                                              "" + "','" + "" + "','" +
                                                              "" + "','" + "" + "')";
            if (this.ExecNoQuery(sql1) == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更新医保费用明细表里的费用批次信息
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int UpdateShareData(FS.HISFC.Models.Registration.Register r)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            string strSQL = @"update gzsi_his_mzxm f
                                                set f.fypc={0}
                                                where f.jydjh='{1}'
                                                and f.yybh='{2}'
                                                and f.clinic_code='{3}'
                                                and f.RYRQ=to_date('{4}','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                strSQL = string.Format(strSQL, r.SIMainInfo.FeeTimes, r.SIMainInfo.RegNo, r.SIMainInfo.HosNo, r.ID, r.DoctorInfo.SeeDate);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 插入多条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="feeDetails">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertShareDataInpatient(FS.HISFC.Models.Registration.Register r, ArrayList feeDetails, DateTime operDate)
        {
            int iReturn = 0;
            int uploadRows = 0;
            try
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    iReturn = InsertShareDataInpatient(r, f, operDate);
                    if (iReturn <= 0)
                    {
                        this.Err += "插入明细失败!";
                        return -1;
                    }
                    uploadRows += iReturn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return uploadRows;
        }

        /// <summary>
        /// 插入单条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int InsertShareDataInpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, DateTime operDate)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }
            //r.SIMainInfo.HosNo = GZSI.HosCode;
            string sqlMaxNo = "select isnull(max(XMXH), 0) from GZSI_his_cfxm where JYDJH = " + "'" + r.SIMainInfo.RegNo + "'";
            int i = 1;

            if (this.ExecQuery(sqlMaxNo) == -1)
            {
                this.Err = "获得项目序号出错!";
                return -1;
            }
            while (Reader.Read())
            {
                i = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
            }

            Reader.Close();

            i++;

            string strSQL = "INSERT INTO GZSI_his_cfxm (JYDJH, YYBH, GMSFHM, ZYH, RYRQ, FYRQ, XMXH, XMBH, XMMC, " +
                "FLDM, YPGG, YPJX, JG, MCYL, JE, BZ1, BZ2, BZ3, DRBZ, YPLY)" +
                "VALUES('{0}','{1}','{2}','{3}',to_date('{4}','yyyy-mm-dd hh24:mi:ss'),to_date('{5}','yyyy-mm-dd hh24:mi:ss'), " +
                "{6},'{7}','{8}',{9},'{10}','{11}',{12},{13},'{14}','{15}','{16}','{17}',{18},'{19}')";
            string name = "";
            name = f.Item.Name;
            if (name.Length > 20)
            {
                name = name.Substring(0, 20);
            }

            try
            {
                strSQL = string.Format(strSQL, r.SIMainInfo.RegNo, r.SIMainInfo.HosNo, r.IDCard, r.PID.CardNO,
                    r.DoctorInfo.SeeDate.ToString(), operDate.ToString(), i.ToString(), f.Item.UserCode, name, "000", f.Item.Specs, r.MainDiagnose,
                    FS.FrameWork.Public.String.FormatNumber(f.Item.Price / f.Item.PackQty, 4), f.Item.Qty,
                    (f.FT.TotCost).ToString(),
                    "", "", "", "0", "");
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 循环插入医嘱明细
        /// </summary>
        /// <param name="pInfo">患者基本信息,包括医保信息</param>
        /// <param name="itemList">患者费用明细实体数组</param>
        /// <returns></returns>
        public int InsertItemList(FS.HISFC.Models.RADT.PatientInfo pInfo, ArrayList itemList)
        {
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in itemList)
            {
                if (this.InsertItemList(pInfo, obj) == -1)
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 插入单条医嘱明细
        /// </summary>
        /// <param name="pInfo">住院患者基本信息,包括医保基本信息</param>
        /// <param name="obj">费用明细信息</param>
        /// <returns></returns>
        public int InsertItemList(FS.HISFC.Models.RADT.PatientInfo pInfo, FS.HISFC.Models.Fee.Inpatient.FeeItemList obj)
        {
            //if (string.IsNullOrEmpty(pInfo.SIMainInfo.HosNo))
            //{
            pInfo.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? pInfo.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            //}

            string sqlMaxNo = "select nvl(max(XMXH), 0) from gzsi_his_cfxm where JYDJH = " + "'" + pInfo.SIMainInfo.RegNo + "'";
            int i = 1;

            if (this.ExecQuery(sqlMaxNo) == -1)
            {
                return -1;
            }
            while (Reader.Read())
            {
                i = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
            }

            Reader.Close();

            i++;

            decimal uploadprice = 0;
            if (obj.FTRate.OwnRate == 0)
            {
                obj.FTRate.OwnRate = 1;
            }

            decimal itemPrice = this.GetPrice(obj.Item);
            uploadprice = FS.FrameWork.Public.String.FormatNumber(itemPrice * obj.FTRate.OwnRate, 4);
            if (uploadprice == 0)
            {
                uploadprice = FS.FrameWork.Public.String.FormatNumber(obj.FT.TotCost / obj.Item.Qty, 4);
            }
            decimal totcost = FS.FrameWork.Public.String.FormatNumber(uploadprice * obj.Item.Qty, 2);
            if (obj.Item.Name.Length > 20)
            {
                obj.Item.Name = obj.Item.Name.Substring(1, 20);
            }

            string strSQL = @"INSERT INTO GZSI_HIS_CFXM --住院医嘱明细信息表
                                                                      (JYDJH, --就医登记号
                                                                       YYBH, --医院编号
                                                                       GMSFHM, --身份证号
                                                                       ZYH, --医保端住院号
                                                                       RYRQ, --入院日期
                                                                       FYRQ, --费用发生日期
                                                                       XMXH, --项目序号
                                                                       XMBH, --项目编号
                                                                       XMMC, --项目名称
                                                                       FLDM, --分类代码
                                                                       YPGG, --规格
                                                                       YPJX, --剂型
                                                                       JG, --单价
                                                                       MCYL, --数量
                                                                       JE, --金额
                                                                       BZ1, --记录保存到医保系统的时间
                                                                       BZ2, --
                                                                       BZ3, --
                                                                       DRBZ, --读入标志
                                                                       YPLY, --药品来源1-国产 2-进口 3-合资
                                                                       PATIENT_NO, --住院号
                                                                       INPATIENT_NO, --住院流水号
                                                                       INVOICE_NO, --发票号
                                                                       OPER_CODE, --操作员
                                                                       OPER_DATE, --操作时间
                                                                       RECIPE_NO, --HIS处方号
                                                                       SEQUENCE_NO, --HIS处方内项目流水号
                                                                       ITEM_CODE --HIS项目编码
                                                                       )
                                                                    VALUES
                                                                      ('{0}', --就医登记号
                                                                       '{1}', --医院编号
                                                                       '{2}', --身份证号
                                                                       '{3}', --医保端住院号
                                                                       TO_DATE('{4}', 'YYYY-MM-DD HH24:MI:SS'), --入院日期
                                                                       TO_DATE('{5}', 'YYYY-MM-DD HH24:MI:SS'), --费用发生日期
                                                                       '{6}', --项目序号
                                                                       '{7}', --项目编号
                                                                       '{8}', --项目名称
                                                                       '{9}', --分类代码
                                                                       '{10}', --规格
                                                                       '{11}', --剂型
                                                                       '{12}', --单价
                                                                       '{13}', --数量
                                                                       '{14}', --金额
                                                                       '{15}', --记录保存到医保系统的时间
                                                                       '{16}', --
                                                                       '{17}', --
                                                                       '{18}', --读入标志
                                                                       '{19}', --药品来源1-国产 2-进口 3-合资
                                                                       '{20}', --住院号
                                                                       '{21}', --住院流水号
                                                                       '{22}', --发票号
                                                                       '{23}', --操作员
                                                                       sysdate, --操作时间
                                                                       '{24}', --HIS处方号
                                                                       '{25}', --HIS处方内项目流水号
                                                                       '{26}' --HIS项目编码
                                                                       )
                                                                    ";
            strSQL = string.Format(strSQL, pInfo.SIMainInfo.RegNo,
                                                                pInfo.SIMainInfo.HosNo,
                                                                pInfo.IDCard,
                                                                pInfo.PID.PatientNO,
                                                                pInfo.PVisit.InTime.ToString(),
                                                                obj.ChargeOper.OperTime.ToString(),
                                                                i.ToString(),
                                                                obj.Item.UserCode.ToString(),
                                                                obj.Item.Name,
                                                                "0",
                                                                obj.Item.Specs,
                                                                "",
                                                                uploadprice.ToString(),
                                                                obj.Item.Qty.ToString(),
                                                                totcost.ToString(),
                                                                "",
                                                                "",
                                                                "",
                                                                "0",
                                                                "",
                                                                pInfo.PID.PatientNO,
                                                                pInfo.ID,
                                                                pInfo.SIMainInfo.InvoiceNo,
                                                                this.Operator.ID,
                                                                obj.RecipeNO,
                                                                obj.SequenceNO.ToString(),
                                                                obj.Item.ID
                                                                );
            if (this.ExecNoQuery(strSQL) == -1)
            {
                return -1;
            }
            return 0;
        }

        #region 处理价格

        /// <summary>
        /// 获得上传的价格
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private decimal GetPrice(FS.HISFC.Models.Base.Item item)
        {
            decimal price = item.Price;
            if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
                //if (undrug.SpecialPrice > 0)
                //{
                    price = undrug.SpecialPrice;
                //}
            }
            else if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                if (phaItem.SpecialPrice > 0)
                {
                    price = phaItem.SpecialPrice;
                }
            }
            if (price == 0)
            {
                price = item.Price;
            }
            return price;
        }

        #endregion

        #endregion

        #region 查询数据

        /// <summary>
        /// 查询历史结算记录
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public ArrayList GetBalanceList(string cardNo, DateTime dtBegin, DateTime dtEnd)
        {
            #region 作废
            /* string strSQL = @"select JYDJH, --就医登记号
                                                                       FYPC, --费用批次
                                                                       YYBH, --医院编号
                                                                       GMSFHM, --身份证号
                                                                       ZYH, --门诊号/住院号
                                                                       RYRQ, --就诊日期
                                                                       JSRQ, --结算日期
                                                                       ZYZJE, --总金额
                                                                       SBZFJE, --社保支付金额
                                                                       ZHZFJE, --账户支付金额
                                                                       BFXMZFJE, --部分项目自付金额
                                                                       QFJE, --个人起付金额
                                                                       GRZFJE1, --个人自费项目金额
                                                                       GRZFJE2, --个人自付金额
                                                                       GRZFJE3, --个人自负金额
                                                                       CXZFJE, --超统筹支付限额个人自付金额
                                                                       ZFYY, --自费原因
                                                                       YYFDJE, --医药机构分单金额
                                                                       BZ1, --备注1,记录产生时间
                                                                       BZ2, --备注2
                                                                       BZ3, --备注3
                                                                       DRBZ, --读入标志
                                                                       CLINIC_CODE, --门诊流水号
                                                                       CARD_NO, --门诊号
                                                                       o.name
                                                                  from gzsi_his_mzjs f,com_patientinfo o
                                                                  where f.card_no='{0}'
                                                                  and f.card_no=o.card_no
                                                                  and f.jsrq>=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                                                  and f.jsrq<to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                                                ";**/
            #endregion

            string strSQL = @"select f.reg_no, --就医登记号
                                                           FEE_TIMES, --费用批次
                                                           '006181', --医院编号
                                                           f.idenno, --身份证号
                                                           f.patient_no, --门诊号/住院号
                                                           f.IN_DATE, --就诊日期
                                                           f.balance_date, --结算日期
                                                           f.tot_cost, --总金额
                                                           f.pub_cost, --社保支付金额
                                                           f.PAY_COST, --账户支付金额
                                                           f.ITEM_PAYCOST, --部分项目自付金额
                                                           f.BASE_COST, --个人起付金额
                                                           f.ITEM_PAYCOST2, --个人自费项目金额
                                                           f.ITEM_YLCOST, --个人自付金额
                                                           f.OWN_COST, --个人自负金额
                                                           f.OVERTAKE_OWNCOST, --超统筹支付限额个人自付金额
                                                           f.OWN_CAUSE, --自费原因
                                                           f.HOS_COST, --医药机构分单金额
                                                           f.inpatient_no, --门诊流水号
                                                           f.patient_no, --门诊号
                                                           f.name,
                                                           f.dept_code,
                                                           f.dept_name
                                                      from fin_ipr_siinmaininfo f
                                                     where f.card_no = '{0}'
                                                       and f.balance_date >= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                                                       and f.balance_date < to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
                                                    ";

            strSQL = string.Format(strSQL, cardNo, dtBegin.ToString(), dtEnd.ToString());

            if (this.ExecQuery(strSQL) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保患者结算信息失败!" + this.Err;
                return null;
            }

            ArrayList alBalanceList = new ArrayList();
            while (Reader.Read())
            {
                FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();
                regInfo.SIMainInfo.RegNo = Reader[0].ToString();
                regInfo.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[1].ToString());
                regInfo.SIMainInfo.HosNo = Reader[2].ToString();
                regInfo.IDCard = Reader[3].ToString();
                regInfo.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5].ToString());
                regInfo.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[6].ToString());
                regInfo.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[7].ToString());
                regInfo.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());
                regInfo.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[9].ToString());
                regInfo.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[10].ToString());
                regInfo.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[11].ToString());
                regInfo.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                regInfo.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                regInfo.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[14].ToString());
                regInfo.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[15].ToString());
                regInfo.SIMainInfo.Memo = Reader[16].ToString();
                regInfo.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[17].ToString());
                regInfo.ID = Reader[18].ToString();
                regInfo.PID.CardNO = Reader[19].ToString();
                regInfo.Name = Reader[20].ToString();
                regInfo.DoctorInfo.Templet.Dept.ID = Reader[21].ToString();
                regInfo.DoctorInfo.Templet.Dept.Name = Reader[22].ToString();

                alBalanceList.Add(regInfo);
            }
            if (!Reader.IsClosed)
            {
                Reader.Close();
            }
            return alBalanceList;
        }
        
        /// <summary>
        /// 查询结算明细记录
        /// </summary>
        /// <param name="regInfo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public ArrayList GetBalanceDetailList(FS.HISFC.Models.Registration.Register regObj)
        {
            string strSQL = @"select f.xmxh 项目序号,
                                                           f.xmbh 项目编号,
                                                           f.xmmc 项目名称,
                                                           f.ypgg 规格,
                                                           f.ypjx 剂型,
                                                           f.je   单价,
                                                           f.mcyl 数量,
                                                           f.je   金额
                                                      from gzsi_his_mzxm f
                                                     where f.clinic_code = '{0}'
                                                       and f.jydjh = '{1}'
                                                       and f.yybh = '{2}'
                                                       and f.gmsfhm = '{3}'
                                                       and f.fypc = '{4}'
                                                    ";

            strSQL = string.Format(strSQL, regObj.ID, regObj.SIMainInfo.RegNo, regObj.SIMainInfo.HosNo, regObj.IDCard, regObj.SIMainInfo.FeeTimes);

            if (this.ExecQuery(strSQL) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "查询医保患者结算信息失败!";
                return null;
            }

            ArrayList alBalanceDetailList = new ArrayList();
            while (Reader.Read())
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList fItem = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
                fItem.Patient = regObj;
                fItem.Order.ID = Reader[0].ToString();
                fItem.Item.UserCode = Reader[1].ToString();
                fItem.Item.Name = Reader[2].ToString();
                fItem.Item.Specs = Reader[3].ToString();
                fItem.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());
                fItem.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(Reader[6].ToString());
                fItem.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[7].ToString());

                alBalanceDetailList.Add(fItem);
            }
            if (!Reader.IsClosed)
            {
                Reader.Close();
            }
            return alBalanceDetailList;
        }

        #endregion

        #region 删除本地存储的前置机数据

        #region 门诊

        /// <summary>
        /// 删除单条费用明细
        /// </summary>
        /// <param name="r">挂号信息，包括医保患者基本信息</param>
        /// <param name="f">费用明细</param>
        /// <param name="operDate">操作时间</param>
        /// <returns>-1错误 0 没有插入 1正确</returns>
        public int DeleteShareData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            string strSQL = "delete from gzsi_his_mzxm t where t.clinic_code='{0}' and t.jydjh='{1}' and t.xmbh='{2}' and t.xmxh='{3}' ";
            try
            {
                strSQL = string.Format(strSQL,
                    r.ID,
                    r.SIMainInfo.RegNo,
                    f.Item.UserCode,
                    f.Order.ID.ToString()
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除单次结算数据
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int DeleteBalanceInfo(FS.HISFC.Models.Registration.Register r)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            string strSQL = "delete from gzsi_his_mzjs t where t.clinic_code='{0}' and t.jydjh='{1}' and t.jsrq='{2}' and t.fypc='{3}'  ";
            try
            {
                strSQL = string.Format(strSQL,
                    r.ID,
                    r.SIMainInfo.RegNo,
                    r.SIMainInfo.BalanceDate.ToString(),
                    r.SIMainInfo.FeeTimes.ToString()
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        #endregion

        /// <summary>
        /// 删除本地住院医保费用明细
        /// </summary>
        public int DeleteShareData(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            string strSQL = "delete from GZSI_HIS_CFXM t where t.INPATIENT_NO='{0}' and t.INVOICE_NO is null";
            try
            {
                strSQL = string.Format(strSQL, pInfo.ID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region 创建账户信息

        private FS.HISFC.BizLogic.Fee.Account accountManager = null;

        public FS.HISFC.BizLogic.Fee.Account AccountManager
        {
            get
            {
                if (accountManager == null)
                {
                    accountManager = new FS.HISFC.BizLogic.Fee.Account();
                }
                return accountManager;
            }
            set { accountManager = value; }
        }

        public int SaveAccount(string cardNo, decimal amout)
        {
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            int resultValue = AccountManager.GetCardByRule(cardNo, ref accountCard);
            if (resultValue <= 0)
            {
                this.Err = AccountManager.Err;
                return -1;
            }

            DateTime dtNow = accountManager.GetDateTimeFromSysDateTime();
            FS.HISFC.Models.Account.AccountDetail accountDetail = null;
            FS.HISFC.Models.Account.Account account = AccountManager.GetAccountByCardNoEX(cardNo);
            if (account == null)
            {
                account = this.GetAccount(accountCard, dtNow);
                if (account == null)
                {
                    return -1;
                }

                if (AccountManager.InsertAccount(account) < 0)
                {
                    Err = "建立账户失败！" + AccountManager.Err;
                    return -1;
                }
                if (CreateAccountDetail(cardNo, account.ID, dtNow, ref accountDetail) == -1)
                {
                    return -1;
                }
            }
            else
            {
                System.Collections.Generic.List<FS.HISFC.Models.Account.AccountDetail> accountList = AccountManager.GetAccountDetail(account.ID, "3", "1");
                if (accountList != null && accountList.Count > 0)
                {
                    accountDetail = accountList[0];
                }

                if (accountDetail == null)
                {
                    if (CreateAccountDetail(cardNo, account.ID, dtNow, ref accountDetail) == -1)
                    {
                        return -1;
                    }
                }
                else
                {
                }
            }

            #region 存入医保账户

            FS.HISFC.Models.Account.PrePay prePay = GetPrePayEX(cardNo, account.ID, accountDetail, FS.HISFC.Models.Base.EnumValidState.Valid, amout);
            if (prePay == null)
            {
                return -1;
            }
            if (!AccountManager.AccountPrePayManagerEX(prePay, 1))
            {
                Err = "建立账户失败！" + AccountManager.Err;
                return -1;
            }
            #endregion
            return 1;
        }

        private int CreateAccountDetail(string cardNo, string accountID, DateTime operTime, ref FS.HISFC.Models.Account.AccountDetail accountDetail)
        {
            //创建明细账户
            accountDetail = this.GetAccountDetail(cardNo, accountID, operTime);
            if (AccountManager.InsertAccountDetail(accountDetail) < 0)
            {
                Err = "建立账户失败！" + AccountManager.Err;
                return -1;
            }
            //生成账户流水信息
            FS.HISFC.Models.Account.AccountRecord accountRecord = this.GetAccountRecord(FS.HISFC.Models.Account.OperTypes.NewAccount, accountID, cardNo, operTime);
            if (accountRecord != null)
            {
                if (AccountManager.InsertAccountRecordEX(accountRecord) < 0)
                {
                    Err = "建立账户失败！" + AccountManager.Err;
                    return -1;
                }
            }
            else
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 账户实体
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Account.Account GetAccount(FS.HISFC.Models.Account.AccountCard accountCard, DateTime operTime)
        {
            try
            {
                //账户信息
                FS.HISFC.Models.Account.Account account = new FS.HISFC.Models.Account.Account();
                account.ID = AccountManager.GetAccountNO();
                account.AccountCard = accountCard;
                account.PassWord = "000000";
                //account.AccountLevel.ID = this.cmbAccountLevel.Tag.ToString();
                account.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                account.CreateEnvironment.ID = accountManager.Operator.ID;
                account.OperEnvironment.ID = accountManager.Operator.ID;
                account.CreateEnvironment.OperTime = operTime;
                account.OperEnvironment.OperTime = operTime;

                return account;
            }
            catch (Exception ex)
            {
                this.Err = "获取账户信息失败!" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 账户明细实体
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="accountID"></param>
        /// <param name="operTime"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Account.AccountDetail GetAccountDetail(string cardNo, string accountID, DateTime operTime)
        {
            try
            {
                FS.HISFC.Models.Account.AccountDetail accountDetail = new FS.HISFC.Models.Account.AccountDetail();
                accountDetail.ID = accountID;
                accountDetail.AccountType.ID = "3"; //3代表医保报销账户
                accountDetail.CardNO = cardNo;
                accountDetail.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                accountDetail.CreateEnvironment.ID = accountManager.Operator.ID;
                accountDetail.CreateEnvironment.OperTime = operTime;
                accountDetail.OperEnvironment.ID = accountManager.Operator.ID;
                accountDetail.OperEnvironment.OperTime = operTime;
                return accountDetail;
            }
            catch(Exception ex)
            {
                this.Err = "获取账户信息失败！" + ex.Message;
                return null;
            }

        }

        /// <summary>
        /// 得到卡的交易实体
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Account.AccountRecord GetAccountRecord(FS.HISFC.Models.Account.OperTypes opertype, string accountID, string cardNo, DateTime operTime)
        {
            try
            {
                //交易信息
                FS.HISFC.Models.Account.AccountRecord accountRecord = new FS.HISFC.Models.Account.AccountRecord();
                accountRecord.AccountNO = accountID;//帐号
                accountRecord.Patient.PID.CardNO = cardNo;//门诊卡号
                accountRecord.FeeDept.ID = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;//科室编码
                accountRecord.Oper.ID = accountManager.Operator.ID;//操作员
                accountRecord.OperTime = operTime;//操作时间
                accountRecord.IsValid = true;//是否有效
                accountRecord.OperType.ID = (int)opertype;
                accountRecord.AccountType.ID = "3";
                return accountRecord;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取预约金实体
        /// </summary>
        /// <param name="state">实体状态</param>
        /// <returns></returns>
        private FS.HISFC.Models.Account.PrePay GetPrePayEX(string cardNo, string accountID, FS.HISFC.Models.Account.AccountDetail accountDetail, FS.HISFC.Models.Base.EnumValidState state, decimal amount)
        {
            string invoiceNO = "";

            #region 获取发票号
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            invoiceNO = feeIntegrate.GetNewInvoiceNO("A");
            if (invoiceNO == null || invoiceNO == string.Empty)
            {
                Err = "获得发票号出错!" + feeIntegrate.Err;
                return null;
            }
            #endregion

            #region 预交金实体
            FS.HISFC.Models.Account.PrePay prePay = new FS.HISFC.Models.Account.PrePay();
            prePay.Patient.PID.CardNO = cardNo;
            prePay.InvoiceNO = invoiceNO;
            prePay.PayType.ID = "SI";
            prePay.PayType.Name = "医保账户报销";
            prePay.PrePayOper.ID = accountManager.Operator.ID;//操作员编号
            prePay.PrePayOper.Name = accountManager.Operator.Name;//操作员姓名
            prePay.ValidState = state;
            prePay.BaseCost = amount;//预交金
            prePay.PrePayOper.OperTime = accountManager.GetDateTimeFromSysDateTime();//系统时间
            prePay.AccountNO = accountID; //帐号
            prePay.IsHostory = false; //是否历史数据
            if (accountDetail != null)
            {
                prePay.BaseVacancy = accountDetail.BaseVacancy + amount;
            }
            else
            {
                prePay.BaseVacancy = amount;
            }
            //prePay.DonateVacancy = accountDetail.DonateVacancy + 0;
            prePay.AccountType.ID = "3";//账户类型编码
            prePay.AccountType.Name = "医保报销账户";//账户类型编码
            #endregion

            return prePay;
        }

        #endregion
    }
}
