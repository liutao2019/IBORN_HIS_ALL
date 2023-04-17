using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.BizLogic.Pharmacy
{
    /// <summary>
    /// [功能描述: 药品盘点管理类]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2011-06]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public class Check : Storage
    {

        #region 基础增、删、改操作

        /// <summary>
        /// 获得对盘点明细表进行update或insert操作的传入参数数组
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>成功返回参数数组，失败返回null</returns>
        private string[] myGetParmForCheckDetail(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            try
            {
                string[] parm = {
									checkInfo.ID,				//0 盘点流水号
									checkInfo.CheckNO,		//1 盘点单号
									checkInfo.StockDept.ID,			//2 库房编码 0 为全部部门
									checkInfo.Item.ID,			//3 药品编码
									checkInfo.BatchNO,			//4 批号
									checkInfo.Item.Name,		//5 商品名称
									checkInfo.Item.Specs,		//6 药品规格
									checkInfo.Item.PriceCollection.RetailPrice.ToString(),	//7 零售价
									checkInfo.Item.PriceCollection.WholeSalePrice.ToString(),//8 批发价
									checkInfo.Item.PriceCollection.PurchasePrice.ToString(),//9 购入价
									checkInfo.Item.Type.ID.ToString(),		//10 药品类别
									checkInfo.Item.Quality.ID.ToString(),	//11 药品性质
									checkInfo.Item.MinUnit,					//12 最小单位
									checkInfo.Item.PackUnit,				//13 包装单位
									checkInfo.Item.PackQty.ToString(),		//14 包装数量
									checkInfo.PlaceNO,					//15 货位号
									checkInfo.ValidTime.ToString(),			//16 有效期
									checkInfo.Producer.ID,					//17 生产厂家
									checkInfo.FStoreQty.ToString(),			//18 封帐盘存数量
									checkInfo.AdjustQty.ToString(),			//19 实际盘存数量
									checkInfo.CStoreQty.ToString(),			//20 结存盘存数量
									checkInfo.MinQty.ToString(),			//21 最小数量
									checkInfo.PackQty.ToString(),			//22 包装数量
									checkInfo.ProfitStatic,					//23 盈亏状态
									checkInfo.QualityFlag,					//24 药品质量情况
									NConvert.ToInt32(checkInfo.IsAdd).ToString(),						//25 是否附加药品
									checkInfo.DisposeWay,					//26 处理方式
									checkInfo.State,					//27 盘点状态 0 封帐 1 结存 2 取消
									checkInfo.Operation.Oper.ID,						//28 操作员
									checkInfo.Operation.Oper.OperTime.ToString(),			//29 操作时间
									checkInfo.ProfitLossQty.ToString(),		//30 盈亏数量		
                                    checkInfo.OtherAdjustQty.ToString() //31其他盘点数量，一般指发药机库存数量，包含在盘点总数量中
								
								};
                return parm;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获得盘点详细信息
        /// </summary>
        /// <param name="SQLString">查询的SQL语句</param>
        /// <returns>成功返回盘点实体数组，失败返回null</returns>
        private ArrayList myGetCheckDetailInfo(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Check checkInfo;		//盘点实体

            //执行查询语句
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得盘点明细信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    checkInfo = new FS.HISFC.Models.Pharmacy.Check();
                    checkInfo.ID = this.Reader[0].ToString();					//0 盘点流水号
                    checkInfo.CheckNO = this.Reader[1].ToString();			//1 盘点单号
                    checkInfo.StockDept.ID = this.Reader[2].ToString();				//2 库房编码
                    checkInfo.Item.ID = this.Reader[3].ToString();				//3 药品编码
                    checkInfo.BatchNO = this.Reader[4].ToString();				//4 批号
                    checkInfo.Item.Name = this.Reader[5].ToString();			//5 商品名称
                    checkInfo.Item.Specs = this.Reader[6].ToString();			//6 药品规格
                    checkInfo.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[7].ToString());		//7 零售价
                    checkInfo.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[8].ToString());	//8 批发价
                    checkInfo.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[9].ToString());		//9 购入价
                    checkInfo.Item.Type.ID = this.Reader[10].ToString();						//10 药品类别
                    checkInfo.Item.Quality.ID = this.Reader[11].ToString();						//11 药品性质
                    checkInfo.Item.MinUnit = this.Reader[12].ToString();						//12 最小单位
                    checkInfo.Item.PackUnit = this.Reader[13].ToString();						//13 包装单位
                    checkInfo.Item.PackQty = NConvert.ToDecimal(this.Reader[14].ToString());	//14 包装数量
                    checkInfo.PlaceNO = this.Reader[15].ToString();							//15 货位号
                    checkInfo.ValidTime = NConvert.ToDateTime(this.Reader[16].ToString());		//16 有效期
                    checkInfo.Producer.ID = this.Reader[17].ToString();							//17 生产厂家
                    checkInfo.FStoreQty = NConvert.ToDecimal(this.Reader[18].ToString());		//18 封帐盘存数量
                    checkInfo.AdjustQty = NConvert.ToDecimal(this.Reader[19].ToString());		//19 实际盘存数量
                    checkInfo.CStoreQty = NConvert.ToDecimal(this.Reader[20].ToString());		//20 结存盘存数量
                    checkInfo.MinQty = NConvert.ToDecimal(this.Reader[21].ToString());			//21 最小数量
                    checkInfo.PackQty = NConvert.ToDecimal(this.Reader[22].ToString());			//22 包装数量
                    checkInfo.ProfitStatic = this.Reader[23].ToString();						//23 盈亏状态
                    checkInfo.QualityFlag = this.Reader[24].ToString();							//24 药品质量情况
                    checkInfo.IsAdd = NConvert.ToBoolean(this.Reader[25].ToString());								//25 是否附加药品 0 不附加 1 附加 
                    checkInfo.DisposeWay = this.Reader[26].ToString();							//26 处理方式
                    checkInfo.State = this.Reader[27].ToString();							//27 盘点状态 0 封帐 1 结存 2 取消
                    checkInfo.Operation.Oper.ID = this.Reader[28].ToString();							//28 操作员
                    checkInfo.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[29].ToString());		//29 操作时间
                    checkInfo.ProfitLossQty = NConvert.ToDecimal(this.Reader[30].ToString());	//30 盈亏数量
                    checkInfo.OtherAdjustQty = NConvert.ToDecimal(this.Reader[31].ToString());  //31其他盘点数量，一般指发药机库存，包含在盘点总量里面

                    al.Add(checkInfo);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得盘点明细信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 获得对盘点统计表进行update或insert操作的传入参数数组
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>成功返回参数数组，失败返回null</returns>
        private string[] myGetParmForCheckStatic(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            try
            {
                string[] strParm = {
									   checkInfo.CheckNO,				        //0 盘点单号
                                       checkInfo.CheckName,                     //1 盘点单名称
									   checkInfo.StockDept.ID,					//2 库存单位编码
									   checkInfo.State,				            //3 盘点状态 0 封帐 1 结存 2 取消
									   checkInfo.FOper.ID,				        //4 封帐人
									   checkInfo.FOper.OperTime.ToString(),		//5 封帐时间
									   checkInfo.COper.ID,				        //6 结存人
									   checkInfo.COper.OperTime.ToString(),		//7 结存时间
									   checkInfo.User01,					    //8 盘亏金额
									   checkInfo.User02,					    //9 盘盈金额
									   checkInfo.Operation.ID,					//10 操作员
									   checkInfo.Operation.Oper.OperTime.ToString()		//11 操作时间
								   };
                return strParm;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获得盘点统计信息
        /// </summary>
        /// <param name="SQLString">查询的SQL语句</param>
        /// <returns>成功返回盘点实体数组，失败返回null</returns>
        private ArrayList myGetCheckStaticInfo(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Check checkInfo;		//盘点实体
            //执行查询语句
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得盘点统计信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    checkInfo = new  FS.HISFC.Models.Pharmacy.Check();
                    checkInfo.CheckNO = this.Reader[0].ToString();							        //0 盘点单号
                    checkInfo.CheckName = this.Reader[1].ToString();                                    //1 盘点单名称
                    checkInfo.StockDept.ID = this.Reader[2].ToString();								//2 库存单位编码
                    checkInfo.State = this.Reader[3].ToString();							            //3 盘点状态 0 封帐 1 结存 2 取消
                    checkInfo.FOper.ID = this.Reader[4].ToString();							        //4 封帐人
                    checkInfo.FOper.OperTime = NConvert.ToDateTime(this.Reader[5].ToString());		//5 封帐时间
                    checkInfo.COper.ID = this.Reader[6].ToString();							        //6 结存人
                    checkInfo.COper.OperTime = NConvert.ToDateTime(this.Reader[7].ToString());		//7 结存时间
                    checkInfo.User01 = this.Reader[8].ToString();								    //8 盘亏金额
                    checkInfo.User02 = this.Reader[9].ToString();								    //9 盘盈金额
                    checkInfo.Operation.Oper.ID = this.Reader[10].ToString();								        //10 操作员
                    checkInfo.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[11].ToString());		//11 操作时间

                    al.Add(checkInfo);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得盘点统计信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// 获得对盘点批次表进行update或insert操作的传入参数数组
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>成功返回参数数组，失败返回null</returns>
        private string[] myGetParmForCheckBatch(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            try
            {
                string[] parm = {
									checkInfo.ID,				  //0 盘点流水号
									checkInfo.GroupNO.ToString(), //1 批次
									checkInfo.CheckNO,		//2 盘点单号
									checkInfo.StockDept.ID,			//3 库房编码 0 为全部部门
									checkInfo.Item.ID,			//4 药品编码
									checkInfo.BatchNO,			//5 批号
									checkInfo.Item.Name,		//6 商品名称
									checkInfo.Item.Specs,		//7 药品规格
									checkInfo.Item.PriceCollection.RetailPrice.ToString(),		//8 零售价
									checkInfo.Item.PriceCollection.WholeSalePrice.ToString(),		//9 批发价
									checkInfo.Item.PriceCollection.PurchasePrice.ToString(),		//10 购入价
									checkInfo.Item.Type.ID,					//11 药品类别
									checkInfo.Item.Quality.ID.ToString(),	//12 药品性质
									checkInfo.Item.MinUnit,					//13 最小单位
									checkInfo.Item.PackUnit,				//14 包装单位
									checkInfo.Item.PackQty.ToString(),		//15 包装数量
									checkInfo.PlaceNO,					//16 货位号
									checkInfo.ValidTime.ToString(),			//17 有效期
									checkInfo.Producer.ID,					//18 生产厂家
									checkInfo.ProfitLossQty.ToString(),		//19 盈亏数量
									checkInfo.ProfitStatic,					//20 盈亏状态
									checkInfo.QualityFlag,					//21 药品质量情况
									checkInfo.DisposeWay,					//22 处理方式
									checkInfo.State,					//23 盘点状态 0 封帐 1 结存 2 取消
									checkInfo.Operation.Oper.ID,						//24 操作员
									checkInfo.Operation.Oper.OperTime.ToString()			//25 操作时间							
								
								};
                return parm;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获得盘点批次信息
        /// </summary>
        /// <param name="SQLString">查询的SQL语句</param>
        /// <returns>成功返回盘点实体数组，失败返回null</returns>
        private ArrayList myGetCheckBatchInfo(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Check checkInfo;		//盘点实体

            //执行查询语句
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得盘点批次信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    checkInfo = new  FS.HISFC.Models.Pharmacy.Check();
                    checkInfo.ID = this.Reader[0].ToString();					//0 盘点流水号
                    checkInfo.GroupNO = NConvert.ToDecimal(this.Reader[1].ToString());	//1 批次号
                    checkInfo.CheckNO = this.Reader[2].ToString();			//2 盘点单号
                    checkInfo.StockDept.ID = this.Reader[3].ToString();				//3 库房编码
                    checkInfo.Item.ID = this.Reader[4].ToString();				//4 药品编码
                    checkInfo.BatchNO = this.Reader[5].ToString();				//5 批号
                    checkInfo.Item.Name = this.Reader[6].ToString();			//6 商品名称
                    checkInfo.Item.Specs = this.Reader[7].ToString();			//7 药品规格
                    checkInfo.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[8].ToString());		//8 零售价
                    checkInfo.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[9].ToString());	//9 批发价
                    checkInfo.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[10].ToString());	//10 购入价
                    checkInfo.Item.Type.ID = this.Reader[11].ToString();						//11 药品类别
                    checkInfo.Item.Quality.ID = this.Reader[12].ToString();						//12 药品性质
                    checkInfo.Item.MinUnit = this.Reader[13].ToString();						//13 最小单位
                    checkInfo.Item.PackUnit = this.Reader[14].ToString();						//14 包装单位
                    checkInfo.Item.PackQty = NConvert.ToDecimal(this.Reader[15].ToString());	//15 包装数量
                    checkInfo.PlaceNO = this.Reader[16].ToString();							//16 货位号
                    checkInfo.ValidTime = NConvert.ToDateTime(this.Reader[17].ToString());		//17 有效期
                    checkInfo.Producer.ID = this.Reader[18].ToString();							//18 生产厂家
                    checkInfo.ProfitLossQty = NConvert.ToDecimal(this.Reader[19].ToString());	//19 盈亏数量
                    checkInfo.ProfitStatic = this.Reader[20].ToString();						//20 盈亏状态
                    checkInfo.QualityFlag = this.Reader[21].ToString();							//21 药品质量情况
                    checkInfo.DisposeWay = this.Reader[22].ToString();							//22 处理方式
                    checkInfo.State = this.Reader[23].ToString();							//23 盘点状态 0 封帐 1 结存 2 取消
                    checkInfo.Operation.Oper.ID = this.Reader[24].ToString();							//24 操作员
                    checkInfo.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[25].ToString());		//25 操作时间	

                    al.Add(checkInfo);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得盘点批次信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 获得对盘点附加表进行update或insert操作的传入参数数组
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>成功返回参数数组，失败返回null</returns>
        private string[] myGetParmForCheckAdd(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            try
            {
                string[] parm = {
									checkInfo.PlaceNO,			//0 库位号
									checkInfo.StockDept.ID,				//1 库房编码
									checkInfo.Item.ID,				//2 药品编码
									checkInfo.BatchNO,				//3 批号 如为'ALL'则为所有批号的药品
									checkInfo.Operation.Oper.ID,				//4 操作员编码
									checkInfo.Operation.Oper.OperTime.ToString()	//5 操作时间
								};
                return parm;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 获得盘点附加信息
        /// </summary>
        /// <param name="SQLString">查询的SQL语句</param>
        /// <returns>成功返回盘点实体数组，失败返回null</returns>
        private ArrayList myGetCheckAddInfo(string SQLString)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Check checkInfo;		//盘点实体

            //执行查询语句
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得盘点附加信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    checkInfo = new FS.HISFC.Models.Pharmacy.Check();
                    checkInfo.PlaceNO = this.Reader[0].ToString();						//0 库位号
                    checkInfo.StockDept.ID = this.Reader[1].ToString();							//1 库房编码
                    checkInfo.Item.ID = this.Reader[2].ToString();							//2 药品编码
                    checkInfo.BatchNO = this.Reader[3].ToString();							//3 批号 如为'ALL'则为所有批号的药品
                    checkInfo.Operation.Oper.ID = this.Reader[4].ToString();							//4 操作员编码
                    checkInfo.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[5].ToString());	//5 操作时间

                    al.Add(checkInfo);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得盘点批次信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 向盘点附加表内插入一条数据
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>0 没有更新 1 成功 －1 失败</returns>
        public int InsertCheckAdd(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.InsertCheckAdd", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertCheckAdd字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmForCheckAdd(checkInfo);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.InsertCheckAdd:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 对盘点附加表进行更新
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>0 没有更新 1 成功 －1 失败</returns>
        public int UpdateCheckAdd(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateCheckAdd", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateCheckAdd字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmForCheckAdd(checkInfo);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateCheckAdd:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 对药品附加信息删除一条记录
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="drugCode">药品编码 'ALL'时对所有药品执行删除</param>
        /// <returns>成功返回1 失败返回－1，无更新返回0</returns>
        public int DeleteCheckAdd(string deptCode, string drugCode)
        {
            string strSQL = "";
            //取删除操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.DeleteCheckAdd", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteCheckAdd字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode, drugCode);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.DeleteCheckAdd:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除科室全部附加药品信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <returns>成功返回1 失败返回－1，无更新返回0</returns>
        public int DeleteCheckAdd(string deptCode)
        {
            return this.DeleteCheckAdd(deptCode, "ALL");
        }

        /// <summary>
        /// 向盘点统计表内插入一条数据
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>0 没有更新 1 成功 －1 失败</returns>
        public int InsertCheckStatic(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.InsertCheckStatic", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertCheckStatic字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmForCheckStatic(checkInfo);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.InsertCheckStatic:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新盘点统计表
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>0没有更新 1 成功 －1 失败</returns>
        public int UpdateCheckStatic(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateCheckStatic", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateCheckStatic字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmForCheckStatic(checkInfo);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateCheckStatic:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 向盘点明细表内插入一条数据
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>0 没有更新 1 成功 －1 失败</returns>
        public int InsertCheckDetail(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.InsertCheckDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertCheckDetail字段!";
                return -1;
            }
            try
            {
                //取流水号
                checkInfo.ID = this.GetSequence("Pharmacy.Item.GetCheckNo");
                string[] strParm = this.myGetParmForCheckDetail(checkInfo);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.InsertCheckDetail:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新盘点明细表
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>0 没有更新 1 成功 －1 失败</returns>
        public int UpdateCheckDetail(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateCheckDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateCheckDetail字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmForCheckDetail(checkInfo);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateCheckDetail:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 对盘点明细记录进行删除
        /// </summary>
        /// <param name="checkNo">盘点流水号</param>
        /// <returns>0 没有更新 1 成功 －1 失败</returns>
        public int DeleteCheckDetail(string checkNo)
        {
            string strSQL = "";
            //取删除操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.DeleteCheckDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeleteCheckDetail字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, checkNo);    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.DeleteCheckDetail:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 向盘点批次表内插入数据
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>0 没有更新 1 成功 －1 失败</returns>
        public int InsertCheckBatch(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            string strSQL = "";
            //取插入操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.InsertCheckBatch", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertCheckBatch字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmForCheckBatch(checkInfo);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.InsertCheckBatch:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region 内部使用

        /// <summary>
        /// 获得盘点单号
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <returns>成功返回盘点单号四位年+两位月+三位流水号,失败返回null</returns>
        public string GetCheckCode(string deptCode)
        {
            string strSQL = "";
            string temp1, temp2;
            string newCheckCode;
            //系统时间 yyyymm
            temp1 = this.GetSysDateNoBar().Substring(0, 6);
            //取最大入库计划单号
            if (this.GetSQL("Pharmacy.Item.GetMaxCheckCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetMaxCheckCode字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetMaxCheckCode:" + ex.Message;
                return null;
            }

            temp2 = this.ExecSqlReturnOne(strSQL);
            if (temp2.ToString() == "-1" || temp2.ToString() == "")
            {
                temp2 = "001";
            }
            else
            {
                decimal i = NConvert.ToDecimal(temp2.Substring(6, 3)) + 1;
                temp2 = i.ToString().PadLeft(3, '0');
            }
            newCheckCode = temp1 + temp2;

            return newCheckCode;
        }

        /// <summary>
        /// 更新盘点状态
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="checkCode">盘点单号</param>
        /// <param name="checkState">盘点状态</param>
        /// <returns>失败返回－1 没有更新返回0 成功返回1</returns>
        public int UpdateCheckDetailForState(string deptCode, string checkCode, string checkState)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateCheckDetailForState", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateCheckDetailForState字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode, checkCode, checkState, this.Operator.ID);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateCheckDetailForState:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);

        }

        /// <summary>
        /// 更新盘点状态
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="checkCode">盘点单号</param>
        /// <param name="checkState">盘点状态</param>
        /// <returns>失败返回－1 没有更新返回0 成功返回1</returns>
        public int UpdateCheckStaticForState(string deptCode, string checkCode, string checkState)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateCheckStaticForState", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateCheckStaticForState字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode, checkCode, checkState, this.Operator.ID);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateCheckStaticForState:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);

        }

        /// <summary>
        /// 盘点单列表名称更新
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="checkCode">盘点单号</param>
        /// <param name="newCheckListName">新盘点单名称</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateCheckListName(string deptCode, string checkCode, string newCheckListName)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateCheckListName", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateCheckListName字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, checkCode, deptCode, newCheckListName, this.Operator.ID);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateCheckListName:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 获得盘点附加信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <returns>成功返回动态数组，失败返回null</returns>
        public ArrayList QueryCheckAddByDept(string deptCode)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetCheckAdd", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckAdd字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetCheckAddByDept", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckAddByDept字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetCheckAddByDept:" + ex.Message;
                return null;
            }

            //取盘点详细信息数据
            return this.myGetCheckAddInfo(strSQL);
        }

        /// <summary>
        /// 获取盘点单列表，如不限制封帐人则传为"ALL"
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="checkState">盘点状态</param>
        /// <param name="fOperCode">封帐人</param>
        /// <returns>Check实体</returns>
        public List<FS.HISFC.Models.Pharmacy.Check> QueryCheckList(string deptCode, string checkState, string fOperCode)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetCheckList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckList字段!";
                return null;
            }
            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptCode, checkState, fOperCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetCheckList:" + ex.Message;
                return null;
            }

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得盘点列表信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            //将返回数据加入动态数组
            FS.HISFC.Models.Pharmacy.Check check;
            List<FS.HISFC.Models.Pharmacy.Check> alList = new List<FS.HISFC.Models.Pharmacy.Check>();

            try
            {
                while (this.Reader.Read())
                {
                    //此语句不能加到循环外面，否则会在al数组内加入相同的数据（最后一条数据）
                    check = new  FS.HISFC.Models.Pharmacy.Check();
                    check.CheckNO = this.Reader[0].ToString();                   //盘点单号
                    check.CheckName = this.Reader[1].ToString();                    //盘点单名称
                    check.FOper.ID = this.Reader[2].ToString();                  //封帐人
                    check.FOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3]);            //封帐时间

                    alList.Add(check);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得盘点列表息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return alList;

        }

        /// <summary>
        /// 获取盘点单的DisposeWay列表
        /// </summary>
        /// <param name="deptCode">科室</param>
        /// <param name="checkCode">单号</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.Check> QueryCheckDisposeWayList(string deptCode, string checkCode)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("SOC.Pharmacy.Check.GetDisposeWay", ref strSQL) == -1)
            {
                 strSQL = @"  SELECT DISTINCT CHECK_CODE,DISPOSE_WAY
                              FROM   PHA_COM_CHECKDETAIL
                              WHERE  DRUG_DEPT_CODE = '{0}'
                              AND    CHECK_CODE = '{1}'
                              AND    DISPOSE_WAY IS NOT NULL
                              AND    DISPOSE_WAY <> '0'
                              ORDER  BY DISPOSE_WAY
                         ";

                this.CacheSQL("SOC.Pharmacy.Check.GetDisposeWay", strSQL);
            }
            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptCode, checkCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错SOC.Pharmacy.Check.GetDisposeWay:" + ex.Message;
                return null;
            }

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得盘点列表信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            //将返回数据加入动态数组
            FS.HISFC.Models.Pharmacy.Check check;
            List<FS.HISFC.Models.Pharmacy.Check> alList = new List<FS.HISFC.Models.Pharmacy.Check>();

            try
            {
                while (this.Reader.Read())
                {
                    //此语句不能加到循环外面，否则会在al数组内加入相同的数据（最后一条数据）
                    check = new FS.HISFC.Models.Pharmacy.Check();
                    check.CheckNO = this.Reader[0].ToString();                   //盘点单号
                    check.DisposeWay = this.Reader[1].ToString();                    //盘点单名称
                    alList.Add(check);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得盘点列表息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return alList;

        }

        /// <summary>
        /// 获取盘点详细信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="checkCode">盘点单号</param>
        /// <returns>成功返回动态数组，失败返回null</returns>
        public ArrayList QueryCheckDetailByCheckCode(string deptCode, string checkCode)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetCheckDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckDetail字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetCheckDetailByCheckCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckDetailByCheckCode字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptCode, checkCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetCheckDetailByCheckCode:" + ex.Message;
                return null;
            }

            //取盘点详细信息数据
            return this.myGetCheckDetailInfo(strSQL);
        }

        /// <summary>
        /// 获取盘点详细信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="checkCode">盘点单号</param>
        /// <param name="state">状态</param>
        /// <returns>成功返回动态数组，失败返回null</returns>
        public ArrayList QueryProLossCheckDetail(string deptCode, string checkCode, string state)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetCheckDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckDetail字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("SOC.Pharmacy.Item.GetProLossCheckDetail.ByCheckCode", ref strWhere) == -1)
            {
                //this.Err = "没有找到SOC.Pharmacy.Item.GetProLossCheckDetail.ByCheckCode字段!";
                //return null;
                strWhere = @" WHERE pha_com_checkdetail.fstore_num<>pha_com_checkdetail.adjust_num           
                              and   pha_com_checkdetail.check_state = '{2}'
                              and   PHA_COM_CHECKDETAIL.DRUG_DEPT_CODE = '{0}'
                              AND   PHA_COM_CHECKDETAIL.CHECK_CODE = '{1}'
                              ORDER BY PHA_COM_CHECKDETAIL.DRUG_CODE";

                this.CacheSQL("SOC.Pharmacy.Item.GetProLossCheckDetail.ByCheckCode", strWhere);
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptCode, checkCode, state);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错SOC.Pharmacy.Item.GetProLossCheckDetail.ByCheckCode:" + ex.Message;
                return null;
            }

            //取盘点详细信息数据
            return this.myGetCheckDetailInfo(strSQL);
        }

        /// <summary>
        /// 根据科室编码与盘点单号 获取盘点统计信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="checkNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Pharmacy.Check GetCheckStat(string deptCode, string checkNO)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetCheckStat", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckStat字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetCheckStatByCheckCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckStatByCheckCode字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptCode, checkNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetCheckStatByCheckCode:" + ex.Message;
                return null;
            }

            //取盘点统计信息数据
            ArrayList alList = this.myGetCheckStaticInfo(strSQL);
            if (alList == null)
                return null;
            if (alList.Count > 0)
                return alList[0] as FS.HISFC.Models.Pharmacy.Check;
            else
                return new  FS.HISFC.Models.Pharmacy.Check();
        }

        /// <summary>
        /// 根据科室编码与盘点单号 获取盘点统计信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="checkNO"></param>
        /// <returns></returns>
        public ArrayList GetCheckStat(string deptCode, DateTime fromDate,DateTime toDate)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetCheckStat", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckStat字段!";
                return null;
            }

            string strWhere = "";
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetCheckStatByDate", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckStatByDate字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL += " " + strWhere;
                strSQL = string.Format(strSQL, deptCode,fromDate.ToString(),toDate.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetCheckStatByDate:" + ex.Message;
                return null;
            }

            //取盘点统计信息数据
            return this.myGetCheckStaticInfo(strSQL);           
        }

        /// <summary>
        /// 获取所有指定盘点单状态的盘点单列表
        /// </summary>
        /// <param name="checkState">盘点单状态</param>
        /// <returns>返回neuobject数组 ID 盘点单号 Name 封帐人-盘点科室 Memo封帐时间 User01 盘点科室</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryCheckList(string checkState)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetCheckList.State", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCheckList.State字段!";
                return null;
            }
            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, checkState);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetCheckList.State:" + ex.Message;
                return null;
            }
            //将返回数据加入动态数组
            FS.FrameWork.Models.NeuObject info;
            List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得盘点列表信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();            //盘点单号
                    info.Name = this.Reader[1].ToString();          //封帐人
                    info.Memo = this.Reader[2].ToString();          //封帐时间
                    info.User01 = this.Reader[3].ToString();		//科室
                    info.Name = info.Name + "-" + info.User01;
                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得盘点列表息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// 判断对某药品进行封帐时 是否仍存在有效的盘点记录
        /// </summary>
        /// <param name="drugNO">药品编码</param>
        /// <param name="deptNO">科室编码</param>
        /// <param name="checkState">需检查的盘点单状态</param>
        /// <param name="checkID">盘点记录流水号 不对自身记录进行判断</param>
        /// <returns>仍存在盘点记录返回True 否则返回False</returns>
        public bool JudgeCheckState(string drugNO, string deptNO, string checkState, string checkID)
        {
            /*            
             *  select t.check_code
                from   pha_com_checkdetail t
                where  t.drug_code = '{0}'
                and    t.drug_dept_code = '{1}'
                and    t.check_state = '{2}'
            */
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("SOC.Pharmacy.Item.JudgeCheckState", ref strSQL) == -1)
            {
                strSQL = @"
                            select t.check_code
                              from pha_com_checkdetail t
                             where t.drug_code = '{0}'
                               and t.drug_dept_code = '{1}'
                               and t.check_state = '{2}'
                               and t.check_code = '{3}'
                          ";
            }
            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, drugNO, deptNO, checkState, checkID);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错SOC.Pharmacy.Item.JudgeCheckState:" + ex.Message;
                return false;
            }

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "判断药品盘点执行情况时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return false;
            }
            try
            {
                while (this.Reader.Read())
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得盘点列表息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return false;
            }
            finally
            {
                this.Reader.Close();
            }
            return false;
        }

        /// <summary>
        /// 封帐数量更新
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="checkCode">盘点单号</param>
        /// <param name="drugNO">药品编码</param>
        /// <param name="fstoreNum">封帐数量</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateFStoreNum(string deptCode, string checkCode, string drugNO, decimal fstoreNum)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateFStoreNum", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateFStoreNum字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode, checkCode, drugNO, fstoreNum.ToString());            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateFStoreNum:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region 盘点封帐
        /// <summary>
        /// 对本库房所有药品进行封帐处理，更新盘点明细表
        /// </summary>
        /// <param name="stockDeptNO">库房编码</param>
        /// <param name="isBatch">是否按批号盘点</param>
        /// <param name="days">库存为0但在days天数内有出入库等业务的封账</param>
        /// <returns>成功返回封帐数组，失败返回null</returns>
        public ArrayList CloseAll(string stockDeptNO, bool isBatch, int days)
        {
            #region 获取Sql语句
            string strSQL = "";
            //取查找库存的SELECT语句
            if (isBatch)
            {	//按批号盘点    由库存明细表Storage内获取
                if (this.GetSQL("SOC.Pharmacy.Check.CloseAll.ByBatch", ref strSQL) == -1)
                {
                    //this.Err = "没有找到SOC.Pharmacy.Check.CloseAll.ByBatch字段!";
                    //return null;
                    strSQL = @"select  u.drug_code,
                                       s.place_code,
                                       sum(u.store_sum),
                                       u.batch_no,
                                       min(u.valid_date),
                                        max(u.purchase_price) purchase_price
                                from
                                (
                                select s.drug_code,
                                       s.group_code,
                                       s.batch_no,
                                       s.valid_date,
                                       s.purchase_price,
                                       s.store_sum
                                from   pha_com_storage s
                                where  s.drug_dept_code = '{0}'
                                and    s.store_sum > 0
                                union  all
                                select s.drug_code,
                                       s.group_code,
                                       s.batch_no,
                                       s.valid_date,
                                       s.purchase_price,
                                       s.store_sum
                                from   pha_com_storage s
                                where  s.drug_dept_code = '{0}'
                                and    s.store_sum <= 0
                                and    exists (
                                                select drug_code 
                                                from   pha_com_record r
                                                where  r.record_type in ('0310','0320')
                                                and    s.drug_dept_code = r.source_dept_code
                                                and    s.drug_code = r.drug_code
                                                and    s.group_code = r.group_code
                                                and    r.oper_date > sysdate - {1}
                                                )
                                ) u,pha_com_stockinfo s
                                where s.drug_dept_code = '{0}'
                                and   u.drug_code = s.drug_code
                                group by u.drug_code,
                                       u.batch_no,
                                       --u.valid_date,
                                       --u.purchase_price,
                                       s.place_code
                                order  by s.place_code";

                    this.CacheSQL("SOC.Pharmacy.Check.CloseAll.ByBatch", strSQL);
                }
            }
            else
            {	//不按批号盘点  由StockInfo内获取汇总统计量
                if (this.GetSQL("SOC.Pharmacy.Check.CloseAll.NoBatch", ref strSQL) == -1)
                {
                    //this.Err = "没有找到SOC.Pharmacy.Check.CloseAll.NoBatch字段!";
                    //return null;
                    strSQL = @"select  u.drug_code,
                                       u.place_code,
                                       u.store_sum
                                from
                                (
                                select s.drug_code,
                                       s.place_code,
                                       s.store_sum
                                from   pha_com_stockinfo s
                                where  s.drug_dept_code = '{0}'
                                and    s.store_sum > 0
                                union  all
                                select s.drug_code,
                                       s.place_code,
                                       s.store_sum
                                from   pha_com_stockinfo s
                                where  s.drug_dept_code = '{0}'
                                and    s.store_sum <= 0
                                and    exists (
                                                select drug_code 
                                                from   pha_com_record r
                                                where  r.record_type in ('0310','0320')
                                                and    s.drug_dept_code = r.source_dept_code
                                                and    s.drug_code = r.drug_code
                                                and    r.oper_date > sysdate - {1}
                                                )
                                ) u
                                order  by u.place_code";

                    this.CacheSQL("SOC.Pharmacy.Check.CloseAll.NoBatch", strSQL);
                }
            }

            #endregion

            #region Sql语句执行
            ArrayList alCheckDetail = new ArrayList();			//用于库存信息的存储
            //执行查询语句
            if (this.ExecQuery(strSQL, stockDeptNO, days.ToString()) == -1)
            {
                this.Err = "封账执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {

                    FS.HISFC.Models.Pharmacy.Check checkDetail = new FS.HISFC.Models.Pharmacy.Check();

                    checkDetail.IsAdd = false;
                    checkDetail.DisposeWay = "0";

                    checkDetail.Item.ID = this.Reader[0].ToString();
                    checkDetail.PlaceNO = this.Reader[1].ToString();
                    checkDetail.FStoreQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    if (isBatch)
                    {
                        //DisposeWay==1则按照批次处理，BatchNO记录批次号，否则记录批号
                        checkDetail.BatchNO = this.Reader[3].ToString();

                        checkDetail.ValidTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                        checkDetail.Item.PriceCollection.PurchasePrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);

                        //DisposeWay==1则按照批次处理，BatchNO记录批次号
                        if (this.Reader.FieldCount > 6)
                        {
                            checkDetail.DisposeWay = this.Reader[6].ToString();
                        }
                    }
                    else
                    {
                        checkDetail.BatchNO = "All";
                    }

                    checkDetail.CStoreQty = 0;
                    checkDetail.State = "0";

                    checkDetail.StockDept.ID = stockDeptNO;

                    alCheckDetail.Add(checkDetail);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得库房库存信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            #endregion

            return alCheckDetail;
        }


        /// <summary>
        /// 对本库房所有药品进行封帐处理，更新盘点明细表
        /// </summary>
        /// <param name="stockDeptNO">库房编码</param>
        /// <param name="isBatch">是否按批号盘点</param>
        /// <param name="days">库存为0但在days天数内有出入库等业务的封账</param>
        /// <returns>成功返回封帐数组，失败返回null</returns>
        public ArrayList CloseAll(string stockDeptNO, bool isBatch, int days,string checkCode)
        {
            #region 获取Sql语句
            string strSQL = "";
            //取查找库存的SELECT语句
            	//不按批号盘点  由StockInfo内获取汇总统计量
                if (this.GetSQL("SOC.Pharmacy.Check.CloseAll.NoBatch.WithSendMachine", ref strSQL) == -1)   
                {
                    #region 中大五院的，有发药机的，改为按批号的
//                    strSQL = @"select a.drug_code,
//                                      a.place_code,
//                                      a.store_sum,
//                                      nvl(rr.store_sum,0) otherAdjustQty
//                                from (select  u.drug_code,
//                                       u.place_code,
//                                       u.store_sum
//                                from
//                                (
//                                select s.drug_code,
//                                       s.place_code,
//                                       s.store_sum
//                                from   pha_com_stockinfo s
//                                where  s.drug_dept_code = '{0}'
//                                and    s.store_sum > 0
//                              
//                                union  all
//                                select s.drug_code,
//                                       s.place_code,
//                                       s.store_sum
//                                from   pha_com_stockinfo s
//                                where  s.drug_dept_code = '{0}'
//                                and    s.store_sum <= 0
//                                and   s.drug_code in 
//                                (select drug_code 
//                                                from   pha_com_record r
//                                                where  r.record_type in ('0310','0320')
//                                                and     r.source_dept_code='{0}'                                                
//                                                and    r.oper_date > sysdate - {1}
//                                                union
//                                                select i.drug_code 
//                                                from pha_com_iron_storage i 
//                                                where i.drug_dept_code = '{0}' 
//                                                and i.check_no= '{2}'
//                                                and i.store_sum>0
//                                                ) 
//                                ) u
//                                order  by u.place_code) a left join
//                                pha_com_iron_storage rr
//                                on a.drug_code = rr.drug_code                                
//                                and rr.drug_dept_code = '{0}'
//                                and rr.check_no = '{2}'";
                    #endregion

                    #region 爱博恩新增 2017年4月9日 by CJF
                    strSQL = @"select s.drug_code,s.place_code,sum(s.store_sum) store_sum,0 otherAdjustQty
                                FROM  PHA_COM_STORAGE s 
                                where s.drug_code in 
                                (
                                select s.drug_code
                                from   pha_com_stockinfo s
                                where  s.drug_dept_code = '{0}'
                                and    s.store_sum > 0
                              
                                union  all
                                select s.drug_code
                                from   pha_com_stockinfo s
                                where  s.drug_dept_code = '{0}'
                                and    s.store_sum <= 0
                                and   s.drug_code in 
                                (select drug_code 
                                                from   pha_com_record r
                                                where  r.record_type in ('0310','0320')
                                                and     r.source_dept_code='{0}'                                                
                                                and    r.oper_date > sysdate - {1}
                                                union
                                                select i.drug_code 
                                                from pha_com_iron_storage i 
                                                where i.drug_dept_code = '{0}' 
                                                and i.check_no= '{2}'
                                                and i.store_sum>0
                                                ) 
                                )";

                    #endregion

                    this.CacheSQL("SOC.Pharmacy.Check.CloseAll.NoBatch.WithSendMachine", strSQL);
                
            }

            #endregion

            #region Sql语句执行
            ArrayList alCheckDetail = new ArrayList();			//用于库存信息的存储
            //执行查询语句
            if (this.ExecQuery(strSQL, stockDeptNO, days.ToString(),checkCode) == -1)
            {
                this.Err = "封账执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {

                    FS.HISFC.Models.Pharmacy.Check checkDetail = new FS.HISFC.Models.Pharmacy.Check();

                    checkDetail.IsAdd = false;
                    checkDetail.DisposeWay = "0";

                    checkDetail.Item.ID = this.Reader[0].ToString();
                    checkDetail.PlaceNO = this.Reader[1].ToString();
                    checkDetail.FStoreQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    
                        //checkDetail.BatchNO = "All";
                    checkDetail.BatchNO = this.Reader[4].ToString();
                        checkDetail.OtherAdjustQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);

                    checkDetail.CStoreQty = 0;
                    checkDetail.State = "0";

                    checkDetail.StockDept.ID = stockDeptNO;

                    alCheckDetail.Add(checkDetail);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得库房库存信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            #endregion

            return alCheckDetail;
        }


        /// <summary>
        /// 对本库房所有药品进行封帐处理，更新盘点明细表
        /// </summary>
        /// <param name="stockDeptNO">库房编码</param>
        /// <param name="isBatch">是否按批号盘点</param>
        /// <param name="days">库存为0但在days天数内有出入库等业务的封账</param>
        /// <param name="drugNO">药品编码</param>
        /// <returns>成功返回封帐数组，失败返回null</returns>
        public ArrayList CloseSingleDrug(string stockDeptNO, bool isBatch, int days, string drugNO)
        {
            #region 获取Sql语句
            string strSQL = "";
            //取查找库存的SELECT语句
            if (isBatch)
            {	//按批号盘点    由库存明细表Storage内获取
                if (this.GetSQL("SOC.Pharmacy.Check.CloseSingleDrug.ByBatch", ref strSQL) == -1)
                {
                    //this.Err = "没有找到SOC.Pharmacy.Check.CloseAll.ByBatch字段!";
                    //return null;
                    strSQL = @"select  u.drug_code,
                                       s.place_code,
                                       sum(u.store_sum),
                                       u.batch_no,
                                       u.valid_date,
                                       u.purchase_price
                                from
                                (
                                select s.drug_code,
                                       s.group_code,
                                       s.batch_no,
                                       s.valid_date,
                                       s.purchase_price,
                                       s.store_sum
                                from   pha_com_storage s
                                where  s.drug_dept_code = '{0}'
                                and    s.drug_code = '{2}'
                                and    s.store_sum > 0
                                union  all
                                select s.drug_code,
                                       s.group_code,
                                       s.batch_no,
                                       s.valid_date,
                                       s.purchase_price,
                                       s.store_sum
                                from   pha_com_storage s
                                where  s.drug_dept_code = '{0}'
                                and    s.drug_code = '{2}'
                                and    s.store_sum <= 0
                                and    exists (
                                                select drug_code 
                                                from   pha_com_record r
                                                where  r.record_type in ('0310','0320')
                                                and    s.drug_dept_code = r.source_dept_code
                                                and    s.drug_code = r.drug_code
                                                and    s.group_code = r.group_code
                                                and    r.oper_date > sysdate - {1}
                                                )
                                ) u,pha_com_stockinfo s
                                where s.drug_dept_code = '{0}'
                                and   s.drug_code = '{2}'
                                and   u.drug_code = s.drug_code
                                group by u.drug_code,
                                       u.batch_no,
                                       u.valid_date,
                                       u.purchase_price,
                                       s.place_code
                                order  by s.place_code";

                    this.CacheSQL("SOC.Pharmacy.Check.CloseSingleDrug.ByBatch", strSQL);
                }
            }
            else
            {	//不按批号盘点  由StockInfo内获取汇总统计量
                if (this.GetSQL("SOC.Pharmacy.Check.CloseSingleDrug.NoBatch", ref strSQL) == -1)
                {
                    //this.Err = "没有找到SOC.Pharmacy.Check.CloseAll.NoBatch字段!";
                    //return null;
                    strSQL = @"select  u.drug_code,
                                       u.place_code,
                                       u.store_sum
                                from
                                (
                                select s.drug_code,
                                       s.place_code,
                                       s.store_sum
                                from   pha_com_stockinfo s
                                where  s.drug_dept_code = '{0}'
                                and    s.drug_code = '{2}'
                                and    s.store_sum > 0
                                union  all
                                select s.drug_code,
                                       s.place_code,
                                       s.store_sum
                                from   pha_com_stockinfo s
                                where  s.drug_dept_code = '{0}'
                                and    s.drug_code = '{2}'
                                and    s.store_sum <= 0
                                and    exists (
                                                select drug_code 
                                                from   pha_com_record r
                                                where  r.record_type in ('0310','0320')
                                                and    s.drug_dept_code = r.source_dept_code
                                                and    s.drug_code = r.drug_code
                                                and    r.oper_date > sysdate - {1}
                                                )
                                ) u
                                order  by u.place_code";

                    this.CacheSQL("SOC.Pharmacy.Check.CloseSingleDrug.NoBatch", strSQL);
                }
            }

            #endregion

            #region Sql语句执行
            ArrayList alCheckDetail = new ArrayList();			//用于库存信息的存储
            //执行查询语句
            if (this.ExecQuery(strSQL, stockDeptNO, days.ToString(), drugNO) == -1)
            {
                this.Err = "封账执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {

                    FS.HISFC.Models.Pharmacy.Check checkDetail = new FS.HISFC.Models.Pharmacy.Check();

                    checkDetail.IsAdd = false;
                    checkDetail.DisposeWay = "0";

                    checkDetail.Item.ID = this.Reader[0].ToString();
                    checkDetail.PlaceNO = this.Reader[1].ToString();
                    checkDetail.FStoreQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    if (isBatch)
                    {
                        //DisposeWay==1则按照批次处理，BatchNO记录批次号，否则记录批号
                        checkDetail.BatchNO = this.Reader[3].ToString();

                        checkDetail.ValidTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                        checkDetail.Item.PriceCollection.PurchasePrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);

                        //DisposeWay==1则按照批次处理，BatchNO记录批次号
                        if (this.Reader.FieldCount > 6)
                        {
                            checkDetail.DisposeWay = this.Reader[6].ToString();
                        }
                    }
                    else
                    {
                        checkDetail.BatchNO = "All";
                    }

                    checkDetail.CStoreQty = 0;
                    checkDetail.State = "0";

                    checkDetail.StockDept.ID = stockDeptNO;

                    alCheckDetail.Add(checkDetail);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得库房库存信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            #endregion

            return alCheckDetail;
        }


        /// <summary>
        /// 对本库房所有药品进行封帐处理，更新盘点明细表
        /// </summary>
        /// <param name="stockDeptNO">库房编码</param>
        /// <param name="checkBillNO">盘点单号</param>
        /// <returns>-1失败</returns>
        public int RefreshCheckBillStorageInfo(string stockDeptNO, string checkBillNO)
        {
            //将所有药品的库存刷新
            string strSQLNoBatch = "";
            if (this.GetSQL("SOC.Pharmacy.Check.ReFStore.NoBatch", ref strSQLNoBatch) == -1)
            {
                strSQLNoBatch = @"
                                update pha_com_checkdetail c
                                set    c.fstore_num = 
                                      nvl(( 
                                        select s.store_sum
                                        from   pha_com_stockinfo s 
                                        where  s.drug_dept_code = c.drug_dept_code
                                        and    s.drug_code = c.drug_code
                                        
                                      ),c.fstore_num)
                                where c.drug_dept_code = '{0}'
                                and   c.check_code = '{1}'
                                and   c.check_state = '0'
                                 ";

                this.CacheSQL("SOC.Pharmacy.Check.ReFStore.NoBatch", strSQLNoBatch);


            }
            int param = this.ExecNoQuery(strSQLNoBatch, stockDeptNO, checkBillNO);
            if (param < 0)
            {
                return param;
            }
                      
            //将分批的药品库存刷新
            string strSQLBatch = "";
            if (this.GetSQL("SOC.Pharmacy.Check.ReFStore.Batch", ref strSQLBatch) == -1)
            {
                strSQLBatch = @"update pha_com_checkdetail c
                    set    c.fstore_num = 
                          nvl(( 
                            select sum(s.store_sum) 
                            from   pha_com_storage s 
                            where  s.drug_dept_code = c.drug_dept_code
                            and    s.drug_code = c.drug_code
                            and    s.batch_no = c.batch_no
                            and    s.valid_date = c.valid_date
                            and    s.purchase_price = c.purchase_price
                            
                          ),c.fstore_num)
                    where c.drug_dept_code = '{0}'
                    and   c.check_code = '{1}'
                    and   c.check_state = '0'
                    ";

                this.CacheSQL("SOC.Pharmacy.Check.ReFStore.Batch", strSQLBatch);
            }
            //将分批次的药品库存刷新，保留功能，实际使用的可能性不大，暂时不处理

            return this.ExecNoQuery(strSQLBatch, stockDeptNO, checkBillNO);
        }

        /// <summary>
        /// 更新盘点汇总表的封账信息
        /// </summary>
        /// <param name="stockDeptNO">库存科室</param>
        /// <param name="checkBillNO">盘点单号</param>
        /// <returns></returns>
        public int UpdateCheckStaticFStroeInfo(string stockDeptNO, string checkBillNO)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Check.ReFStore.UpdateStatic", ref strSQL) == -1)
            {
                strSQL = @"
                           update pha_com_checkstatic c 
                            set    c.foper_code = '{2}',
                                   c.foper_time = sysdate,
                                   c.oper_code = '{2}',
                                   c.oper_date = sysdate
                            where  c.drug_dept_code = '{0}'
                            and    c.check_code = '{1}'
                            and    c.check_state = '0'
                                 ";

                this.CacheSQL("SOC.Pharmacy.Check.ReFStore.UpdateStatic", strSQL);

            }
            return this.ExecNoQuery(strSQL, stockDeptNO, checkBillNO, this.Operator.ID);
        }
        #endregion

        #region 盘点保存

        /// <summary>
        /// 在盘点过程中进行盘点保存，更新结存数量
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="checkCode">盘点单号</param>
        /// <returns>成功返回1 失败返回－1 无更新返回0</returns>
        public int SaveCheck(string deptCode, string checkCode)
        {
            string strSQL1 = "";
            string strSQL2 = "";
            //取SELECT语句
            //更新盘点数量
            if (this.GetSQL("Pharmacy.Item.SaveCheck", ref strSQL1) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.SaveCheck字段!";
                return -1;
            }
            //更新盘点盈亏标记
            if (this.GetSQL("Pharmacy.Item.SaveCheckForState", ref strSQL2) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.SaveChecForStatek字段!";
                return -1;
            }

            //格式化SQL语句
            try
            {
                strSQL1 = string.Format(strSQL1, deptCode, checkCode);
                strSQL2 = string.Format(strSQL2, deptCode, checkCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.SaveCheck:" + ex.Message;
                return -1;
            }
            int flag = this.ExecNoQuery(strSQL1);
            if (flag == -1)
                return -1;
            else
                if (flag == 0)
                    return 0;
            return this.ExecNoQuery(strSQL2);
        }

        /// <summary>
        /// 增量更新盘点明细表
        /// </summary>
        /// <param name="checkInfo">盘点实体</param>
        /// <returns>0 没有更新 1 成功 －1 失败</returns>
        public int UpdateCheckDetailAddSave(FS.HISFC.Models.Pharmacy.Check checkInfo)
        {
            string strSQL = "";
            //取更新操作的SQL语句
            if (this.GetSQL("Pharmacy.Item.UpdateCheckDetail.AddSave", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateCheckDetail.AddSave字段!";
                return -1;
            }
            try
            {
                string[] strParm = this.myGetParmForCheckDetail(checkInfo);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.UpdateCheckDetail.AddSave:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region 盘点解封

        /// <summary>
        /// 对盘点进行解封，更新盘点明细、统计表内盘点状态
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="checkCode">盘点单号</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int CancelCheck(string deptCode, string checkCode)
        {
            //更新盘点明细表
            int i = this.UpdateCheckDetailForState(deptCode, checkCode, "2");
            if (i == -1 || i == 0) return -1;
            //更新盘点统计表
            int j = this.UpdateCheckStaticForState(deptCode, checkCode, "2");
            if (j == -1 || j == 0) return -1;
            return 1;
        }

        #endregion

        #region 盘点结存

        /// <summary>
        /// 执行盘点结存存储过程
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="checkCode">盘点单号</param>
        /// <param name="isBatch">是否按批号盘点</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int ExecProcedurgCheckCStore(string deptCode, string checkCode, bool isBatch)
        {
            //获取是否按批号盘点标志
            string batchFlag;
            if (isBatch)
                batchFlag = "1";
            else
                batchFlag = "0";
            //操作员
            string operCode = this.Operator.ID;
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Procedure.pkg_pha.prc_check_cstore", ref strSQL) == -1)
            {
                this.Err = "找不到存储过程执行语句Pharmacy.Procedure.pkg_pha.prc_check_cstore";
                return -1;
            }

            string sqlErr = "";
            int sqlCode = 0;
            try
            {
                strSQL = string.Format(strSQL, deptCode, checkCode, batchFlag, operCode, sqlCode, sqlErr);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            string strReturn = "";
            if (this.ExecEvent(strSQL, ref strReturn) == -1)
            {
                this.Err = strReturn + "执行存储过程出错!prc_check_cstore:" + this.Err;
                this.ErrCode = "prc_check_cstore";
                this.WriteErr();
                return -1;
            };
            if (strReturn != "")
            {
                string[] strParam = strReturn.Split(',');
                if (strParam.Length > 1)
                {
                    if (strParam[0] == "-1")
                    {
                        this.Err = this.Err + strParam[1];
                        return -1;
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// 盘点结存
        /// </summary>
        /// <param name="stockDeptNO">科室编码</param>
        /// <param name="checkBillNO">盘点单号</param>
        /// <param name="isCheckStoreQtyWhenLoss">盘亏时是否判断库存</param>
        /// <param name="isCheckPurchasePrice">库存判断是否检测购入价</param>
        /// <param name="isCheckValidTime">库存判断是否检测有效期</param>
        /// <returns>-1失败 0状态已经改变 1成功</returns>
        public int ConfirmCheck(string stockDeptNO, string checkBillNO, bool isCheckStoreQtyWhenLoss, bool isCheckPurchasePrice, bool isCheckValidTime)
        {
            ArrayList alCheckDetail = this.QueryProLossCheckDetail(stockDeptNO, checkBillNO,"0");
            if (alCheckDetail == null)
            {
                return -1;
            }

            //alCheckDetail.Count==0时没有盈亏，但是需要更新盘点单的状态，所以继续执行

            DateTime sysTime = this.GetDateTimeFromSysDateTime();

            foreach (FS.HISFC.Models.Pharmacy.Check checkDetail in alCheckDetail)
            {
                if (checkDetail.State != "0")
                {
                    continue;
                }
                if (checkDetail.FStoreQty == checkDetail.AdjustQty)
                {
                    continue;
                }
                if (checkDetail.BatchNO.ToUpper() != "ALL")
                {
                    isCheckPurchasePrice = true;
                    isCheckValidTime = true;
                }
                //这个在盘点盈亏批次明细表中插入数据
                FS.HISFC.Models.Pharmacy.Check checkBatch = checkDetail.Clone();
                checkBatch.ProfitLossQty = checkDetail.AdjustQty - checkDetail.FStoreQty;
                checkBatch.Operation.Oper.OperTime = sysTime;
                checkBatch.Operation.Oper.ID = this.Operator.ID;

                #region 盘盈：增加库存
                if (checkDetail.AdjustQty > checkDetail.FStoreQty)
                {
                    FS.HISFC.Models.Pharmacy.StorageBase storageBase = new FS.HISFC.Models.Pharmacy.StorageBase();
                    ArrayList alStorage = new ArrayList();
                    if (checkDetail.DisposeWay == "1")
                    {
                        checkDetail.GroupNO = NConvert.ToDecimal(checkDetail.BatchNO);
                    }
                    if (checkDetail.GroupNO > 0)
                    {
                        alStorage = this.QueryStorageList(stockDeptNO, checkDetail.Item.ID, checkDetail.GroupNO);
                    }
                    else
                    {
                        alStorage = this.QueryStorageList(stockDeptNO, checkDetail.Item.ID, checkDetail.BatchNO);
                    }
                    if (alStorage == null)
                    {
                        return -1;
                    }
                    if (isCheckPurchasePrice || isCheckValidTime)
                    {
                        for (int index1 = alStorage.Count - 1; index1 > -1; index1--)
                        {
                            FS.HISFC.Models.Pharmacy.StorageBase info = alStorage[index1] as FS.HISFC.Models.Pharmacy.StorageBase;

                            if (isCheckPurchasePrice && info.Item.PriceCollection.PurchasePrice != checkDetail.Item.PriceCollection.PurchasePrice)
                            {
                                alStorage.RemoveAt(index1);
                                continue;
                            }

                            if (isCheckValidTime && info.ValidTime != checkDetail.ValidTime)
                            {
                                alStorage.RemoveAt(index1);
                                continue;
                            }
                        }
                    }

                    if (alStorage.Count == 0)
                    {
                        //新来的，没有库存，则获取批次号
                        string groupNO = this.GetNewGroupNO();
                        if (groupNO == "-1")
                        {
                            return -1;
                        }
                        storageBase.GroupNO = NConvert.ToDecimal(groupNO);
                        storageBase.BatchNO = "1";

                        //有效期不能太大，因为出库根据有效期排序了
                        storageBase.ValidTime = DateTime.Now.AddMonths(6).Date;

                        //这里单价取盘点表中的，如果盘点期间调价的话，在表现层决定是否取最新单价
                        storageBase.Item = checkDetail.Item;
                    }
                    else
                    {
                        //最后入库的应该在最后，盘盈的加在最后一批
                        storageBase = alStorage[alStorage.Count - 1] as FS.HISFC.Models.Pharmacy.StorageBase;
                    }
                    storageBase.StockDept.ID = stockDeptNO;

                    //库存变化量是盘盈量
                    storageBase.Quantity = checkDetail.AdjustQty - checkDetail.FStoreQty;

                    storageBase.Class2Type = "0305";
                    storageBase.PrivType = "01";
                    storageBase.ID = checkBillNO;

                    checkBatch.GroupNO = storageBase.GroupNO;
                    checkBatch.BatchNO = storageBase.BatchNO;
                    checkBatch.Item = storageBase.Item;
                    checkBatch.State = "1";

                    int param = this.SetStorage(storageBase);
                    if (param == -1)
                    {
                        return -1;
                    }

                    //设置盘亏批次明细
                    param = this.InsertCheckBatch(checkBatch);
                    if (param == -1)
                    {
                        return -1;
                    }
                }
                #endregion

                #region 盘亏：减少库存
                else if (checkDetail.AdjustQty < checkDetail.FStoreQty)
                {
                    decimal lossQty = checkDetail.FStoreQty - checkDetail.AdjustQty;
                    decimal storageNum = 0;

                    #region 库存量是否足够判断
                    if (isCheckStoreQtyWhenLoss)
                    {
                        if (checkDetail.DisposeWay == "1")
                        {
                            checkDetail.GroupNO = NConvert.ToDecimal(checkDetail.BatchNO);
                        }
                        if (checkDetail.GroupNO > 0)
                        {
                            if (this.GetStorageNum(checkDetail.StockDept.ID, checkDetail.Item.ID, checkDetail.GroupNO, out storageNum) == -1)
                            {
                                return -1;
                            }
                        }
                        else if (this.GetStorageNum(checkDetail.StockDept.ID, checkDetail.Item.ID, checkDetail.BatchNO, out storageNum) == -1)
                        {
                            return -1;
                        }

                        //判断库存是否不足，退库允许没有库存或者不足
                        if (storageNum < lossQty)
                        {
                            this.Err = "货位：" + checkDetail.PlaceNO + " "
                                + checkDetail.Item.Name
                                + "[" + checkDetail.Item.Specs + "]"
                                + (checkDetail.BatchNO.ToUpper() == "ALL" ? "" : "批号：" + checkDetail.BatchNO)
                                + "\n库存数量不足，您可能少盘了：" + (lossQty - storageNum).ToString() + checkDetail.Item.MinUnit
                                + "\n原因通常是您封账后进行了出库或者发药，然后才实盘药品，请检查后修改盘点数量";
                            this.ErrCode = "2";
                            return -1;

                        }
                    }
                    #endregion

                    #region 库存明细获取
                    FS.HISFC.Models.Pharmacy.StorageBase storageBase = new FS.HISFC.Models.Pharmacy.StorageBase();
                    ArrayList alStorage = new ArrayList();
                    if (checkDetail.DisposeWay == "1")
                    {
                        checkDetail.GroupNO = NConvert.ToDecimal(checkDetail.BatchNO);
                    }
                    if (checkDetail.GroupNO > 0)
                    {
                        alStorage = this.QueryStorageList(stockDeptNO, checkDetail.Item.ID, checkDetail.GroupNO);
                        if (alStorage == null)
                        {
                            return -1;
                        }
                        if (alStorage.Count == 0)
                        {
                            this.Err = "货位：" + checkDetail.PlaceNO + " "
                            + checkDetail.Item.Name
                            + "[" + checkDetail.Item.Specs + "]"
                            + (checkDetail.BatchNO.ToUpper() == "ALL" ? "" : "批号：" + checkDetail.BatchNO)
                            + "\n库存数量不足：不可以盘亏\n请检查后修改盘点数量";
                            this.ErrCode = "2";
                            return -1;
                        }
                    }
                    else
                    {
                        alStorage = this.QueryStorageList(stockDeptNO, checkDetail.Item.ID, checkDetail.BatchNO);
                        if (alStorage == null)
                        {
                            return -1;
                        }
                        if (alStorage.Count == 0)
                        {
                            this.Err = "货位：" + checkDetail.PlaceNO + " "
                            + checkDetail.Item.Name
                            + "[" + checkDetail.Item.Specs + "]"
                            + (checkDetail.BatchNO.ToUpper() == "ALL" ? "" : "批号：" + checkDetail.BatchNO)
                            + "\n库存数量不足：不可以盘亏\n请检查后修改盘点数量";
                            this.ErrCode = "2";
                            return -1;
                        }

                        //{18D18AA3-208E-49db-B6A7-52FE735A0DA4}
                        storageNum = 0;
                        for (int index1 = alStorage.Count - 1; index1 > -1; index1--)
                        {
                            FS.HISFC.Models.Pharmacy.StorageBase info = alStorage[index1] as FS.HISFC.Models.Pharmacy.StorageBase;
                            storageNum += info.StoreQty;
                        }

                        //{18D18AA3-208E-49db-B6A7-52FE735A0DA4}
                        //生成盘点单时，价格统一取的所有批次中的最高价格
                        //所以此处不能简单根据价格是否相等来进行判断
                        //只要是同一个批次，就可以进行盘亏操作
                        //if (isCheckPurchasePrice || isCheckValidTime)
                        //{
                        //    storageNum = 0;
                        //    for (int index1 = alStorage.Count - 1; index1 > -1; index1--)
                        //    {
                        //        FS.HISFC.Models.Pharmacy.StorageBase info = alStorage[index1] as FS.HISFC.Models.Pharmacy.StorageBase;

                        //        if (isCheckPurchasePrice && info.Item.PriceCollection.PurchasePrice != checkDetail.Item.PriceCollection.PurchasePrice)
                        //        {
                        //            alStorage.RemoveAt(index1);
                        //            continue;
                        //        }

                        //        if (isCheckValidTime && info.ValidTime != checkDetail.ValidTime)
                        //        {
                        //            alStorage.RemoveAt(index1);
                        //            continue;
                        //        }
                        //        storageNum += info.StoreQty;
                        //    }
                        //}

                        //判断库存是否不足
                        if (storageNum < lossQty && isCheckStoreQtyWhenLoss)
                        {
                            this.Err = "货位：" + checkDetail.PlaceNO + " "
                            + checkDetail.Item.Name
                            + "[" + checkDetail.Item.Specs + "]"
                            + (checkDetail.BatchNO.ToUpper() == "ALL" ? "" : "批号：" + checkDetail.BatchNO)
                            + "\n库存数量不足，您可能少盘了：" + (lossQty - storageNum).ToString() + checkDetail.Item.MinUnit
                            + "\n原因通常是您封账后进行了出库或者发药，然后才实盘药品，请检查后修改盘点数量";
                            this.ErrCode = "2";
                            return -1;

                        }
                    }
                    
                    #endregion

                    #region 减少库存

                    int index2 = 0;
                    while (lossQty > 0 && index2 < alStorage.Count)
                    {
                        storageBase = alStorage[index2] as FS.HISFC.Models.Pharmacy.StorageBase;
                        index2++;

                        if (storageBase.StoreQty == 0)
                        {
                            continue;
                        }
                        storageBase.StockDept.ID = stockDeptNO;

                        storageBase.Class2Type = "0305";
                        storageBase.PrivType = "01";
                        storageBase.ID = checkBillNO;

                        checkBatch.GroupNO = storageBase.GroupNO;
                        checkBatch.BatchNO = storageBase.BatchNO;
                        checkBatch.Item = storageBase.Item;
                        checkBatch.State = "1";

                       
                        if (storageBase.StoreQty >= lossQty)
                        {
                            //盘亏量作为库存更改量
                            storageBase.Quantity = -Math.Abs(lossQty);

                            lossQty = 0;
                        }
                        else
                        {
                            //全部库存减掉
                            storageBase.Quantity = -Math.Abs(storageBase.StoreQty);
                            lossQty = lossQty - Math.Abs(storageBase.Quantity);
                        }

                        int param = this.SetStorage(storageBase);
                        if (param == -1)
                        {
                            return -1;
                        }

                        checkBatch.ProfitLossQty = storageBase.Quantity;

                        //设置盘亏批次明细
                        param = this.InsertCheckBatch(checkBatch);
                        if (param == -1)
                        {
                            return -1;
                        }
                    }
                    if (lossQty > 0)
                    {
                        if (isCheckStoreQtyWhenLoss)
                        {
                            this.Err = "货位：" + checkDetail.PlaceNO + " "
                                + checkDetail.Item.Name
                                + "[" + checkDetail.Item.Specs + "]"
                                + (checkDetail.BatchNO.ToUpper() == "ALL" ? "" : "批号：" + checkDetail.BatchNO)
                                + "\n库存数量不足，您可能少盘了：" + lossQty.ToString() + checkDetail.Item.MinUnit
                                + "\n原因通常是您封账后进行了出库或者发药，然后才实盘药品，请检查后修改盘点数量";
                            this.ErrCode = "2";
                            return -1;
                        }
                        else
                        {
                            //校正结存库存数量
                            string strSQL = "";
                            if (this.GetSQL("SOC.Pharmacy.Check.AdjustCStoreQty", ref strSQL) == -1)
                            {
                                strSQL = @"UPDATE PHA_COM_CHECKDETAIL set CSTORE_NUM = {1} where CHECK_NO = {0}";
                                this.CacheSQL("SOC.Pharmacy.Check.AdjustCStoreQty", strSQL);
                            }

                            strSQL = string.Format(strSQL, checkDetail.ID, (checkDetail.AdjustQty + lossQty).ToString());

                            int param = this.ExecNoQuery(strSQL);

                            if (param <= 0)
                            {
                                return -1;
                            }
                        }
                    }
                    #endregion

                }
                #endregion
            }

            #region 盘点状态更新
            int returnVal = this.UpdateCheckDetailForState(stockDeptNO, checkBillNO, "1");
            if (returnVal < 0)
            {
                this.Err = "更新盘点单状态发生错误";
                return -1;
            }
            if (returnVal == 0)
            {
                this.Err = "盘点单已经确认或解封";
                return -1;
            }
            returnVal = this.UpdateCheckStaticForState(stockDeptNO, checkBillNO, "1");
            if (returnVal < 0)
            {
                this.Err = "更新盘点单状态发生错误";
                return -1;
            }
            if (returnVal == 0)
            {
                this.Err = "盘点单已经确认或解封";
                return -1;
            }
            #endregion

            return 1;
        }
               
        #endregion 

      
    }
}
