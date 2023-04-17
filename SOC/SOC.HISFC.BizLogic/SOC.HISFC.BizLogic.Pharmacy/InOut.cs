using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.BizLogic.Pharmacy
{
    /// <summary>
    /// [功能描述: 药品入出库管理类]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2011-06]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public class InOut : Apply
    {
        #region 药库申请

        /// <summary>
        /// 读取某科室的内部入库申请单列表
        /// </summary>
        /// <param name="deptCode">库房编码 申请科室</param>
        /// <param name="class3MeaningCode">三级权限码</param>
        /// <param name="applyState">申请单状态 0 申请 1 审批 2 核准 3 作废</param>
        /// <returns>成功返回neuobject数组 id 申请单号 Name 供货单位名称 meno 供货单位编码</returns>
        public ArrayList QueryApplyOutList(string deptCode, string class3MeaningCode, string applyState)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            string strString = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutListByApplyDept", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutListByApplyDept字段!";
                return null;
            }
            try
            {
                strString = string.Format(strSQL, deptCode, class3MeaningCode, applyState);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            FS.FrameWork.Models.NeuObject info;

            if (this.ExecQuery(strString) == -1)
            {
                this.Err = "获得内部入库申请信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();		//申请单号
                    info.Name = this.Reader[1].ToString();		//供货单位名称
                    info.Memo = this.Reader[2].ToString();		//供货单位编码

                    al.Add(info);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获取内部入库申请列表信息出错" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 根据供货单位获取发送到该单位的申请列表
        /// </summary>
        /// <param name="targetDept">供货单位</param>
        /// <param name="class3MeaningCode">三级权限码</param>
        /// <param name="applyState">申请单状态 0 申请 1 审批 2 核准 3 作废 </param>
        /// <returns>成功返回neuobject数组 id 申请单号 Name 供货单位名称 meno 供货单位编码</returns>
        public ArrayList QueryApplyOutListByTargetDept(string targetDept, string class3MeaningCode, string applyState)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            string strString = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutListByTargetDept", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutListByTargetDept字段!";
                return null;
            }
            try
            {
                strString = string.Format(strSQL, targetDept, class3MeaningCode, applyState);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            FS.FrameWork.Models.NeuObject info;
            if (this.ExecQuery(strString) == -1)
            {
                this.Err = "获得内部入库申请信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();		//申请单号
                    info.Name = this.Reader[1].ToString();		//申请单位名称
                    info.Memo = this.Reader[2].ToString();		//申请单位编码
                    //{455251A2-1D85-4a97-A517-C82E2A331775} 增加货位号
                    info.User01 = this.Reader[3].ToString();    //货位号

                    al.Add(info);
                }
                this.Reader.Close();
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获取内部入库申请列表信息出错" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据内部入库申请单号获取详细申请信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="listCode">申请单号</param>
        /// <param name="state">申请单状态</param>
        /// <returns>成功返回ApplyOut数组、失败返回null</returns>
        public ArrayList QueryApplyOutInfoByListCode(string deptCode, string listCode, string state)
        {
            string strSelect = "";
            string strWhere = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutInfoByListCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutInfoByListCode字段!";
                return null;
            }

            try
            {
                string[] strParm = { deptCode, listCode, state };
                strSelect = string.Format(strSelect + " " + strWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSelect);
        }

        /// <summary>
        /// 取某一科室申请，某一目标本科室未核准的申请列表
        /// 例如，某一药房查看某一科室的领用申请信息	
        /// </summary>
        /// <param name="targetDeptCode">出库部门编码</param>
        /// <param name="applyDeptCode">申请部门编码</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutList(string applyDeptCode, string targetDeptCode)
        {
            string strSelect = "";  //取某一科室申请，某一目标本科室未核准的SELECT语句
            string strWhere = "";  //取某一科室申请，某一目标本科室未核准的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.ByTargeDept", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.ByTargeDept字段!";
                return null;
            }

            try
            {
                string[] strParm = { applyDeptCode, targetDeptCode };
                strSelect = string.Format(strSelect + " " + strWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSelect);
        }

        public int InValidApplyOut(string ID,string operCode)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InValidApplyOutByBILLCODE", ref strSQL) == -1)
            {
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID,operCode);
            }
            catch (Exception ex)
            {
                this.Err = "传入参数不正确！Pharmacy.Item.InValidApplyOutByBILLCODE" + ex.Message;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region 入库申请表操作
        #region 基础增删改操作


        /// <summary>
        /// 删除入库申请记录
        /// </summary>
        /// <param name="ID">入库申请记录流水号</param>
        /// <returns>0没有删除 1成功 -1失败</returns>
        public int DeleteApplyIn(string ID)
        {
            string strSQL = "";
            //根据入库申请流水号删除某一条入库申请记录的DELETE语句
            if (this.GetSQL("Pharmacy.Item.DeleteApplyIn", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, ID);
            }
            catch(Exception ex)
            {
                this.Err = "传入参数不正确！Pharmacy.Item.DeleteApplyIn" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新入库申请记录
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int UpdateApplyIn(string ID)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateApplyInByBILLCODE", ref strSQL) == -1)
            {
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID);
            }
            catch(Exception ex)
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyInByBILLCODE" + ex.Message;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 按apply_num更新入库申请表信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int UpdateApplyInByApplyId(string ID)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateApplyInByApplyCode", ref strSQL) == -1)
            {
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL,ID);
            }
            catch(Exception ex)
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyInByApplyCode" + ex.Message;
            }
            return this.ExecNoQuery(strSQL);
        }

        
        /// <summary>
        /// 插入入库申请表
        /// </summary>
        /// <param name="applyIn"></param>
        /// <returns></returns>
        public int InsertApplyIn(FS.HISFC.Models.Pharmacy.Input applyIn)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertApplyIn", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = myGetParmApplyIn(applyIn);    //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入入库申请SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 获取指定科室、指定状态的外部申请信息
        /// </summary>
        /// <param name="deptCode">申请科室</param>
        /// <param name="listCode">单据号</param>
        /// <param name="state">状态</param>
        /// <returns>成功返回实体 失败返回null 无数据返回空实体</returns>
        public ArrayList QueryApplyIn(string deptCode, string listCode, string state)
        {
            string strSelect = "";
            string strWhere = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyIn", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyIn字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetApplyIn.DeptListCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyIn.DeptListCode字段!";
                return null;
            }

            try
            {
                string[] strParm = { deptCode, listCode, state };
                strSelect = string.Format(strSelect + " " + strWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyIn(strSelect);
        }
        #endregion
        #region 内部使用

        /// <summary>
        /// 获得update或者insert入库申请表的传入参数数组
        /// </summary>
        /// <param name="applyIn">入库申请类</param>
        /// <returns>成功返回字符串数组 失败返回null</returns>
        private string[] myGetParmApplyIn(FS.HISFC.Models.Pharmacy.Input applyIn)
        {
            try
            {
                //获取统计金额
                if (applyIn.Item.PackQty == 0)
                    applyIn.Item.PackQty = 1;
                decimal retailCost = applyIn.Operation.ApplyQty / applyIn.Item.PackQty * applyIn.Item.PriceCollection.RetailPrice;
                decimal wholesaleCost = applyIn.Operation.ApplyQty / applyIn.Item.PackQty * applyIn.Item.PriceCollection.WholeSalePrice;
                decimal purchaseCost = applyIn.Operation.ApplyQty / applyIn.Item.PackQty * applyIn.Item.PriceCollection.PurchasePrice;
                string[] strParm ={
									 applyIn.ID,									//0 申请流水号
									 applyIn.StockDept.ID,								//1 申请科室 管理库存的科室									 
									 applyIn.InListNO,							//2 申请单据号
									 applyIn.Item.ID,								//3 药品编码
									 applyIn.Item.Name,								//4 药品商品名
									 applyIn.Item.Type.ID,							//5 药品类别
									 applyIn.Item.Quality.ID.ToString(),			//6 药品性质
									 applyIn.Item.Specs,							//7 规格
									 applyIn.Item.PackUnit,							//8 包装单位
									 applyIn.Item.PackQty.ToString(),				//9 包装数量
									 applyIn.Item.MinUnit,							//10 最小单位
									 applyIn.ShowState,								//11 显示单位标记
									 applyIn.ShowUnit,								//12 显示单位
									 applyIn.BatchNO,								//13 批号
									 applyIn.ValidTime.ToString(),					//14 有效期
									 applyIn.Producer.ID,							//15 生产厂家
									 applyIn.Company.ID,							//16 供货单位
									 applyIn.Item.PriceCollection.RetailPrice.ToString(),			//17 零售价
									 applyIn.Item.PriceCollection.WholeSalePrice.ToString(),		//18 批发价
									 applyIn.Item.PriceCollection.PurchasePrice.ToString(),			//19 购入价
									 System.Math.Round(retailCost,2).ToString(),	//20 零售金额
									 System.Math.Round(wholesaleCost,2).ToString(),	//21 批发金额
									 System.Math.Round(purchaseCost,2).ToString(),	//22 购入金额
									 applyIn.Operation.ApplyOper.ID,							//23 申请人
									 applyIn.Operation.ApplyOper.OperTime.ToString(),					//24 申请日期
									 applyIn.State,									//25 申请状态0 申请 1 审批 2 核准
									 applyIn.Operation.ApplyQty.ToString(),					//26 申请数量
									 applyIn.Operation.ExamQty.ToString(),					//27 入库数量
									 applyIn.Operation.ExamOper.ID,							//28 入库人
									 applyIn.Operation.ExamOper.OperTime.ToString(),					//29 入库日期
									 applyIn.PlaceNO,								//30 货位号
									 applyIn.MedNO,									//31 制剂序号
									 applyIn.InvoiceNO,								//32 发票号
									 applyIn.DeliveryNO,							//33 送货单号
									 applyIn.TenderNO,								//34 招标单序号
									 applyIn.ActualRate.ToString(),					//35 实际扣率
									 this.Operator.ID,								//36 操作员
									 //操作时间 由SQL取得
									 applyIn.Memo,									//37 备注
									 applyIn.User01,								//38 扩展字段
									 applyIn.User02,								//39 扩展字段1
									 applyIn.User03									//40 扩展字段2
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
        /// 获取入库申请数据
        /// </summary>
        /// <param name="strSql">执行的SQL语句</param>
        /// <returns>成功返回Input数组 失败返回null</returns>
        private ArrayList myGetApplyIn(string strSql)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Input applyIn;

            //执行查询语句
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "获得入库申请明细信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    applyIn = new Input();

                    applyIn.ID = this.Reader[0].ToString();										//0 申请流水号
                    applyIn.StockDept.ID = this.Reader[1].ToString();								//1 申请科室 管理库存的科室					
                    applyIn.InListNO = this.Reader[2].ToString();								//2 申请单据号
                    applyIn.Item.ID = this.Reader[3].ToString();								//3 药品编码
                    applyIn.Item.Name = this.Reader[4].ToString();								//4 药品商品名
                    applyIn.Item.Type.ID = this.Reader[5].ToString();							//5 药品类别
                    applyIn.Item.Quality.ID = this.Reader[6].ToString();						//6 药品性质
                    applyIn.Item.Specs = this.Reader[7].ToString();								//7 规格
                    applyIn.Item.PackUnit = this.Reader[8].ToString();							//8 包装单位
                    applyIn.Item.PackQty = NConvert.ToDecimal(this.Reader[9].ToString());		//9 包装数量
                    applyIn.Item.MinUnit = this.Reader[10].ToString();							//10 最小单位
                    applyIn.ShowState = this.Reader[11].ToString();								//11 显示单位标记
                    applyIn.ShowUnit = this.Reader[12].ToString();								//12 显示单位
                    applyIn.BatchNO = this.Reader[13].ToString();								//13 批号
                    applyIn.ValidTime = NConvert.ToDateTime(this.Reader[14].ToString());		//14 有效期
                    applyIn.Producer.ID = this.Reader[15].ToString();							//15 生产厂家
                    applyIn.Company.ID = this.Reader[16].ToString();							//16 供货单位
                    applyIn.TargetDept.ID = applyIn.Company.ID;
                    applyIn.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[17].ToString());	//17 零售价
                    applyIn.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[18].ToString());	//18 批发价
                    applyIn.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[19].ToString());	//19 购入价
                    applyIn.RetailCost = NConvert.ToDecimal(this.Reader[20].ToString());		//20 零售金额
                    applyIn.WholeSaleCost = NConvert.ToDecimal(this.Reader[21].ToString());		//21 批发金额
                    applyIn.PurchaseCost = NConvert.ToDecimal(this.Reader[22].ToString());		//22 购入金额
                    applyIn.Operation.ApplyOper.ID = this.Reader[23].ToString();							//23 申请人
                    applyIn.Operation.ApplyOper.OperTime = NConvert.ToDateTime(this.Reader[24].ToString());	    //24 申请日期
                    applyIn.State = this.Reader[25].ToString();									//25 入库状态0 申请 1 审批 2 核准
                    applyIn.Operation.ApplyQty = NConvert.ToDecimal(this.Reader[26].ToString());			//26 申请数量					
                    applyIn.Operation.ExamQty = NConvert.ToDecimal(this.Reader[27].ToString());			//27 入库数量
                    applyIn.Operation.ExamOper.ID = this.Reader[28].ToString();							//28 入库人
                    applyIn.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[29].ToString());			//29 入库日期
                    applyIn.PlaceNO = this.Reader[30].ToString();								//30 货位号
                    applyIn.MedNO = this.Reader[31].ToString();									//31 制剂序号
                    applyIn.InvoiceNO = this.Reader[32].ToString();								//32 发票号
                    applyIn.DeliveryNO = this.Reader[33].ToString();							//33 送货单号
                    applyIn.TenderNO = this.Reader[34].ToString();								//34 招标单序号
                    applyIn.ActualRate = NConvert.ToDecimal(this.Reader[35].ToString());		//35 实际扣率
                    applyIn.Operation.Oper.ID = this.Reader[36].ToString();								//36 操作员
                    applyIn.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[37].ToString());			//37 操作时间
                    applyIn.Memo = this.Reader[38].ToString();									//38 备注
                    applyIn.User01 = this.Reader[39].ToString();								//39 扩展字段
                    applyIn.User02 = this.Reader[40].ToString();								//40 扩展字段1
                    applyIn.User03 = this.Reader[41].ToString();								//41 扩展字段2

                    al.Add(applyIn);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取入库申请明细信息时出错" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }
        #endregion
        #endregion

        #region 入库操作

        #region 基础增、删、改操作

        /// <summary>
        /// 获得update或者insert入库就表的传入参数数组
        /// </summary>
        /// <param name="input">入库记录类</param>
        /// <returns>成功返回字符串数组 失败返回null</returns>
        protected string[] myGetParmInput(FS.HISFC.Models.Pharmacy.Input input)
        {
            try
            {

                //获取统计金额

                // by cube 2011-03-25 财务统计最关键的就是金额，不可以在此写死了
                //if (input.Item.PackQty == 0)
                //    input.Item.PackQty = 1;
                //decimal retailCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.RetailPrice;
                //decimal wholesaleCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.WholeSalePrice;
                //decimal purchaseCost = input.Quantity / input.Item.PackQty * input.Item.PriceCollection.PurchasePrice;

                //string isTenderOffer="0";
                //if(input.Item.TenderOffer.IsTenderOffer==true)
                //{
                //    isTenderOffer="1";
                //}
                //else
                //{
                //    isTenderOffer="0";
                //}
                //end by

                string[] strParm ={
									 input.StockDept.ID,								//0 申请科室 管理库存的科室
									 input.ID,									//1 入库流水号
									 input.SerialNO.ToString(),					//2 单内序号
									 input.GroupNO.ToString(),					//3 批次号
									 input.InListNO,							//4 入库单据号
									 input.PrivType,							//6 入库分类  0310
									 input.SystemType,							//5 入库类型  三级权限码一般入库 特殊入库等
									 input.OutBillNO,							//7 出库单流水号
									 input.OutSerialNO.ToString(),				//8 出库单内序号
									 input.OutListNO,							//9 出库单据号
									 input.Item.ID,								//10 药品编码
									 input.Item.Name,							//11 药品商品名
									 input.Item.Type.ID,						//12 药品类别
									 input.Item.Quality.ID.ToString(),			//13 药品性质
									 input.Item.Specs,							//14 规格
									 input.Item.PackUnit,						//15 包装单位
									 input.Item.PackQty.ToString(),				//16 包装数量
									 input.Item.MinUnit,						//17 最小单位
									 input.ShowState,							//18 显示单位标记
									 input.ShowUnit,							//19 显示单位
									 input.BatchNO,								//20 批号
									 input.ValidTime.ToString(),				//21 有效期
									 input.Producer.ID,							//22 生产厂家
									 input.Company.ID,							//23 供货单位
									 input.Item.PriceCollection.RetailPrice.ToString(),			//24 零售价
									 input.Item.PriceCollection.WholeSalePrice.ToString(),		//25 批发价
									 input.Item.PriceCollection.PurchasePrice.ToString(),		//26 购入价
									 input.Quantity.ToString(),					//27 入库数量
                                     //不再进行舍入，保持与myGetParmOutput一致，避免出库与入库金额不等{2C227FDD-4B0A-4a0a-9F98-40B51BCD9F10}
                                     //System.Math.Round(retailCost,2).ToString(),//28 零售金额
                                     //System.Math.Round(wholesaleCost,2).ToString(),	//29 批发金额
                                     //System.Math.Round(purchaseCost,2).ToString(),	//30 购入金额
                                     //retailCost.ToString(),//28 零售金额
                                     //wholesaleCost.ToString(),	//29 批发金额
                                     //purchaseCost.ToString(),	//30 购入金额

                                     input.RetailCost.ToString(),
                                     input.WholeSaleCost.ToString(),
                                     input.PurchaseCost.ToString(),

									 input.StoreQty.ToString(),					//31 入库后库存数量
									 input.StoreCost.ToString(),				//32 入库后库存金额
									 input.SpecialFlag,							//33 特殊标记 1 是 0 否
									 input.State,								//34 入库状态0 申请 1 审批 2 核准
									 input.Operation.ApplyQty.ToString(),					//35 申请数量
									 input.Operation.ApplyOper.ID,						//36 申请人
									 input.Operation.ApplyOper.OperTime.ToString(),			    //37 申请日期
									 input.Operation.ExamQty.ToString(),					//38 审批数量
									 input.Operation.ExamOper.ID,						//39 审核人
									 input.Operation.ExamOper.OperTime.ToString(),					//40 审核日期
									 input.Operation.ApproveOper.ID,						//41 核准人
									 input.Operation.ApproveOper.OperTime.ToString(),				//42 核准日期
									 input.PlaceNO,							//43 货位号
									 input.Operation.ReturnQty.ToString(),				//44 退库数量
									 input.MedNO,								//45 制剂序号
									 input.InvoiceNO,							//46 发票号
									 input.DeliveryNO,							//47 送货单号
									 input.TenderNO,							//48 招标单序号
									 input.ActualRate.ToString(),				//49 实际扣率
									 input.CashFlag,							//50 扣现金标志
									 input.PayState,							//51 供货商结存状态
									 this.Operator.ID,							//52 操作员
									 //操作时间 由SQL取得
									 input.Memo,								//53 备注
									 input.User01,								//54 扩展字段1
									 input.User02,								//55 扩展字段2
									 input.User03,								//56 扩展字段3
                                     FS.FrameWork.Function.NConvert.ToInt32(input.Item.TenderOffer.IsTenderOffer).ToString(),                             //57招标标记{D28CC3CF-C502-4987-BC01-1AEBF2F9D17F} sel 增加下面三个字段的插入
                                     input.CommonPurchasePrice.ToString(),      //58一般入库时的购入价
                                     input.InvoiceDate.ToString(),               //59发票上的发票时间
                                     input.InDate.ToString(),                   //{24E12384-34F7-40c1-8E2A-3967CECAF615} 增加入库时间、供货单位类型字段
                                     input.SourceCompanyType,
                                     input.Item.Product.ApprovalInfo // 批准文号
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
        /// 获取入库数据
        /// </summary>
        /// <param name="strSQL">执行的SQL语句</param>
        /// <returns>成功返回Input数组 失败返回null</returns>
        protected ArrayList myGetInput(string strSQL)
        {
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Input input;

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得入库明细信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    input = new Input();

                    input.StockDept.ID = this.Reader[0].ToString();								//0 申请科室 管理库存的科室
                    input.ID = this.Reader[1].ToString();									//1 入库流水号
                    input.SerialNO = NConvert.ToInt32(this.Reader[2].ToString());			//2 单内序号
                    input.GroupNO = NConvert.ToDecimal(this.Reader[3].ToString());			//3 批次号
                    input.InListNO = this.Reader[4].ToString();							//4 入库单据号
                    input.PrivType = this.Reader[5].ToString();								//6 入库分类 0310
                    input.SystemType = this.Reader[6].ToString();							//5 系统类型 三级权限码
                    input.OutBillNO = this.Reader[7].ToString();							//7 出库单流水号
                    input.OutSerialNO = NConvert.ToInt32(this.Reader[8].ToString());		//8 出库单内序号
                    input.OutListNO = this.Reader[9].ToString();							//9 出库单据号
                    input.Item.ID = this.Reader[10].ToString();								//10 药品编码
                    input.Item.Name = this.Reader[11].ToString();							//11 药品商品名
                    input.Item.Type.ID = this.Reader[12].ToString();						//12 药品类别
                    input.Item.Quality.ID = this.Reader[13].ToString();						//13 药品性质
                    input.Item.Specs = this.Reader[14].ToString();							//14 规格
                    input.Item.PackUnit = this.Reader[15].ToString();						//15 包装单位
                    input.Item.PackQty = NConvert.ToDecimal(this.Reader[16].ToString());	//16 包装数量
                    input.Item.MinUnit = this.Reader[17].ToString();						//17 最小单位
                    input.ShowState = this.Reader[18].ToString();							//18 显示单位标记
                    input.ShowUnit = this.Reader[19].ToString();							//19 显示单位
                    input.BatchNO = this.Reader[20].ToString();								//20 批号
                    input.ValidTime = NConvert.ToDateTime(this.Reader[21].ToString());		//21 有效期
                    input.Producer.ID = this.Reader[22].ToString();							//22 生产厂家
                    input.Company.ID = this.Reader[23].ToString();							//23 供货单位
                    input.TargetDept.ID = input.Company.ID;
                    input.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[24].ToString());//24 零售价
                    input.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[25].ToString());	//25 批发价
                    input.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[26].ToString());	//26 购入价
                    input.Quantity = NConvert.ToDecimal(this.Reader[27].ToString());		//27 入库数量
                    input.RetailCost = NConvert.ToDecimal(this.Reader[28].ToString());		//28 零售金额
                    input.WholeSaleCost = NConvert.ToDecimal(this.Reader[29].ToString());	//29 批发金额
                    input.PurchaseCost = NConvert.ToDecimal(this.Reader[30].ToString());	//30 购入金额
                    input.StoreQty = NConvert.ToDecimal(this.Reader[31].ToString());		//31 入库后库存数量
                    input.StoreCost = NConvert.ToDecimal(this.Reader[32].ToString());		//32 入库后库存金额
                    input.SpecialFlag = this.Reader[33].ToString();							//33 特殊标记 1 是 0 否
                    input.State = this.Reader[34].ToString();								//34 入库状态0 申请 1 审批 2 核准
                    input.Operation.ApplyQty = NConvert.ToDecimal(this.Reader[35].ToString());		//35 申请数量
                    input.Operation.ApplyOper.ID = this.Reader[36].ToString();						//36 申请人
                    input.Operation.ApplyOper.OperTime = NConvert.ToDateTime(this.Reader[37].ToString());	    //37 申请日期
                    input.Operation.ExamQty = NConvert.ToDecimal(this.Reader[38].ToString());			//38 审批数量
                    input.Operation.ExamOper.ID = this.Reader[39].ToString();						//39 审核人
                    input.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[40].ToString());		//40 审核日期
                    input.Operation.ApproveOper.ID = this.Reader[41].ToString();						//41 核准人
                    input.Operation.ApproveOper.OperTime = NConvert.ToDateTime(this.Reader[42].ToString());	//42 核准日期
                    input.PlaceNO = this.Reader[43].ToString();							//43 货位号
                    input.Operation.ReturnQty = NConvert.ToDecimal(this.Reader[44].ToString());		//44 退库数量
                    input.User01 = this.Reader[45].ToString();								//45 申请序号
                    input.MedNO = this.Reader[46].ToString();								//46 制剂序号
                    input.InvoiceNO = this.Reader[47].ToString();							//47 发票号
                    input.DeliveryNO = this.Reader[48].ToString();							//48 送货单号
                    input.TenderNO = this.Reader[49].ToString();							//49 招标单序号
                    input.ActualRate = NConvert.ToDecimal(this.Reader[50].ToString());		//50 实际扣率
                    input.CashFlag = this.Reader[51].ToString();							//51 扣现金标志
                    input.PayState = this.Reader[52].ToString();							//52 供货商结存状态
                    input.Operation.Oper.ID = this.Reader[53].ToString();							//53 操作员
                    input.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[54].ToString());		//54 操作时间
                    input.Memo = this.Reader[55].ToString();								//55 备注
                    input.User01 = this.Reader[56].ToString();								//56 扩展字段1
                    input.User02 = this.Reader[57].ToString();								//57 扩展字段2
                    input.User03 = this.Reader[58].ToString();								//58 扩展字段3 

                    //{24E12384-34F7-40c1-8E2A-3967CECAF615} 增加入库时间、供货单位类型字段
                    if (this.Reader.FieldCount > 59)
                    {
                        input.CommonPurchasePrice = NConvert.ToDecimal(this.Reader[59]);
                        input.Item.TenderOffer.IsTenderOffer = NConvert.ToBoolean(this.Reader[60]);
                        input.InvoiceDate = NConvert.ToDateTime(this.Reader[61]);

                        input.InDate = NConvert.ToDateTime(this.Reader[62]);
                        input.SourceCompanyType = this.Reader[63].ToString();
                    }

                    al.Add(input);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取入库计划明细信息时出错" + ex.Message;
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
        /// 插入一条入库记录
        /// </summary>
        /// <param name="Input">入库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertInput(FS.HISFC.Models.Pharmacy.Input Input)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertInput", ref strSQL) == -1) return -1;
            try
            {
                //获取入库流水号
                Input.ID = this.GetSequence("Pharmacy.Item.GetInputBillID");
                if (Input.ID == "")
                {
                    this.Err = "获取入库流水号出错！";
                    return -1;
                }

                //by cube 屏蔽，从界面取值
                //{24E12384-34F7-40c1-8E2A-3967CECAF615} 数据赋值
                //DateTime sysDate = this.GetDateTimeFromSysDateTime();
                //Input.InDate = sysDate;
                //if (string.IsNullOrEmpty( Input.SourceCompanyType ) == true)
                //{
                //    Input.SourceCompanyType = "1";          //1 院内科室 2 供货单位 3 扩展
                //}

                string[] strParm = myGetParmInput(Input); //取参数列表
                strSQL = string.Format(strSQL, strParm);      //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入入库记录的SQl参数赋值时出错！Pharmacy.Item.InsertInput" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新一条入库记录
        /// </summary>
        /// <param name="Input">入库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateInput(FS.HISFC.Models.Pharmacy.Input Input)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateInput", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = myGetParmInput(Input);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新入库记录的SQl参数赋值时出错！Pharmacy.Item.UpdateInput" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除入库记录
        /// </summary>
        /// <param name="ID">入库记录流水号</param>
        /// <returns>0没有删除 1成功 -1失败</returns>
        public int DeleteInput(string ID)
        {
            string strSQL = "";
            //根据入库记录流水号删除某一条入库记录的DELETE语句
            if (this.GetSQL("Pharmacy.Item.DeleteInput", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, ID);
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.DeleteInput";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region 内部使用

        /// <summary>
        /// 根据科室、状态由入库表内获取药品列表
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="inState">状态标志</param>
        /// <param name="offerCompanyID">供货单位编码 "AAAA"忽略供货单位</param>
        /// <returns>成功返回Item动态数组 失败返回null</returns>
        public ArrayList QueryPharmacyListForInput(string deptCode, string inState, string offerCompanyID)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetPharmacyListForInput", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetPharmacyListForInput字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptCode, inState, offerCompanyID);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetPharmacyListForInput:" + ex.Message;
                return null;
            }

            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Item item;

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得申请入库信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    item = new FS.HISFC.Models.Pharmacy.Item();

                    item.ID = this.Reader[0].ToString();							//药品编码
                    item.Name = this.Reader[1].ToString();   						//药品名称
                    item.Specs = this.Reader[2].ToString();							//规格
                    item.User01 = this.Reader[3].ToString();						//供货单位	
                    item.User02 = this.Reader[4].ToString();						//入库数量
                    item.User03 = this.Reader[5].ToString();						//入库流水号
                    item.SpellCode = this.Reader[6].ToString();					//拼音码
                    item.WBCode = this.Reader[7].ToString();						//五笔码
                    item.NameCollection.RegularSpell.SpellCode = this.Reader[8].ToString();   //通用名拼音码
                    item.NameCollection.RegularSpell.WBCode = this.Reader[9].ToString();		//通用名五笔码

                    al.Add(item);
                }

            }
            catch (Exception ex)
            {
                this.Err = "获取入库列表信息时出错！" + ex.Message;
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
        /// 获取入库单号
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <returns>成功返回入库单据号 yymmdd＋三位流水号</returns>
        public string GetListCode(string deptCode)
        {
            string strSQL = "";
            string temp1, temp2;
            string newListCode;
            //系统时间 yymmdd
            temp1 = this.GetSysDateNoBar().Substring(2, 6);
            //取最大入库计划单号
            if (this.GetSQL("Pharmacy.Item.GetMaxInListCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetMaxInputListCode字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptCode, temp1);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetMaxInListCode:" + ex.Message;
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
            newListCode = temp1 + temp2;

            return newListCode;
        }

        /// <summary>
        /// 更新一条入库记录中的"已退库数量"字段（加操作）
        /// </summary>
        /// <param name="inputBillCode">入库单号</param>
        /// <param name="SerialNO">单内序号</param>
        /// <param name="returnNum">退库数量</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateInputReturnNum(string inputBillCode, int SerialNO, decimal returnNum)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateInputReturnNum", ref strSQL) == -1)
            {
                this.Err = "找不到SQL语句！Pharmacy.Item.UpdateInputReturnNum";
                return -1;
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   inputBillCode, 
									   SerialNO.ToString(), 
									   returnNum.ToString(),
									   this.Operator.ID
								   };
                strSQL = string.Format(strSQL, strParm);              //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新退库数量的SQl参数赋值出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更改入库供货公司
        /// </summary>
        /// <param name="inBillNO">入库单据号input.ID</param>
        /// <param name="SerialNO">单内流水号</param>
        /// <param name="compancyNO">公司编码</param>
        /// <returns></returns>
        public int UpdateInputCompany(string inBillNO, int SerialNO, string compancyNO)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Item.UpdateInputCompany", ref strSQL) == -1)
            {
                //this.Err = "找不到SQL语句！SOC.Pharmacy.Item.UpdateInputCompany";
                //return -1;

                strSQL = @"
                         update pha_com_input 
                         set    company_code = '{2}',
                                oper_code = '{3}',
                                oper_date = sysdate
                         where  in_bill_code = {0}
                         and    serial_code = {1}
                         and    in_state <> '2'
                         ";

                this.CacheSQL("SOC.Pharmacy.Item.UpdateInputCompany", strSQL);
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   inBillNO, 
									   SerialNO.ToString(), 
									   compancyNO,
									   this.Operator.ID
								   };
                strSQL = string.Format(strSQL, strParm);              //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新供货公司SQl参数赋值出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新入库送单日期
        /// </summary>
        /// <param name="inBillNO"></param>
        /// <param name="SerialNO"></param>
        /// <param name="diliverTime"></param>
        /// <returns></returns>
        public int UpdateInputCompanyDiliverTime(string inBillNO, string SerialNO, string diliverTime)
        {
            string strSql = "SOC.Pharmacy.Item.UpdateInputCompanyDiliverTime";

            if (this.GetSQL("", ref strSql) == -1)
            {
                strSql = @"
                         update pha_com_input 
                         set    ext_code = '{2}',
                                oper_code = '{3}',
                                oper_date = sysdate
                         where  in_bill_code = {0}
                         and    serial_code = {1}
                         and    in_state <> '2'

    ";
            }

            try
            {
                string[] param = {inBillNO,
                                  SerialNO,
                                  diliverTime,
                                  this.Operator.ID
                                  };

                strSql = string.Format(strSql, param);
            }
            catch (Exception ex)
            {
                this.Err = "获取送单日期相关参数出错" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
            
        }
        #region 获取各库房入库单据情况、发票情况

        /// <summary>
        /// 获取出库单据列表 供入库核准
        /// </summary>
        /// <param name="outDeptCode">出库科室</param>
        /// <param name="storageDept">领药科室</param>
        /// <param name="class3MeaningCode">三级权限码 "A"忽略权限信息</param>
        /// <returns>成功返回neuobject数组 Id 单据号 Name 出库科室 Memo 出库科室编码 失败返回null</returns>
        public ArrayList QueryOutputListForApproveInput(string outDeptCode, string storageDept, string class3MeaningCode)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            string strString = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetOutListForApproveInput", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutListForApproveInput字段!";
                return null;
            }
            try
            {
                strString = string.Format(strSQL, outDeptCode, storageDept, class3MeaningCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            FS.FrameWork.Models.NeuObject info;
            if (this.ExecQuery(strString) == -1)
            {
                this.Err = "获得出库信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();		//单据号
                    info.Name = this.Reader[1].ToString();		//出库单位名称
                    info.Memo = this.Reader[2].ToString();		//出库单位编码
                    info.User01 = this.Reader[3].ToString();	//审批人

                    al.Add(info);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获取出库列表信息出错" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 获取某科室入库数据列表、不包含详细数据
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="inPrivType">入库分类 三级权限码 'AAAA'忽略该参数</param>
        /// <param name="inState">入库状态 0 申请 1 审批 2 核准 'AAAA'忽略该参数</param>
        /// <returns>成功返回入库实体数组 失败返回null</returns>
        public ArrayList QueryInputList(string deptCode, string inPrivType, string inState)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetInputList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInputList字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptCode, inPrivType, inState);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetInputList:" + ex.Message;
                return null;
            }

            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Input input;

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得入库列表信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    input = new Input();
                    input.StockDept.ID = this.Reader[0].ToString();							//申请科室编码
                    input.InListNO = this.Reader[1].ToString();						//入库单据号
                    input.PrivType = this.Reader[2].ToString();							//入库分类 0310
                    input.SystemType = this.Reader[3].ToString();						//三级权限码
                    input.OutListNO = this.Reader[4].ToString();						//出库单据号
                    input.Company.ID = this.Reader[5].ToString();						//供货单位编码
                    input.TargetDept.ID = input.Company.ID;
                    input.SpecialFlag = this.Reader[6].ToString();						//特殊标记 1 是  0 否
                    input.State = this.Reader[7].ToString();							//入库状态 0 申请 1 审批 2 核准
                    input.Operation.ApplyOper.ID = this.Reader[8].ToString();					//申请操作员
                    input.Operation.ApplyOper.OperTime = NConvert.ToDateTime(this.Reader[9].ToString());	//申请时间
                    input.Operation.ExamOper.ID = this.Reader[10].ToString();					//审批人
                    input.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[11].ToString());	//审批时间
                    input.Operation.ApproveOper.ID = this.Reader[12].ToString();					//核准人
                    input.Operation.ApproveOper.OperTime = NConvert.ToDateTime(this.Reader[13].ToString());//核准时间
                    input.InvoiceNO = this.Reader[14].ToString();						//发票号
                    input.PayState = this.Reader[15].ToString();						//供货商结存状态
                    input.DeliveryNO = this.Reader[16].ToString();						//送货单号
                    //					input.Memo = this.Reader[16].ToString();							//备注
                    al.Add(input);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取入库列表信息时出错！" + ex.Message;
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
        /// 获取某科室入库数据列表 不保护详细数据
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="inPrivType">入库分类 AAAA 检索所有分类</param>
        /// <param name="inState">入库状态 0 申请 1 审批 2 核准</param>
        /// <param name="dtBegin">查询起始时间</param>
        /// <param name="dtEnd">查询终止时间</param>
        /// <returns>成功返回入库实体数组 失败返回null</returns>
        public ArrayList QueryInputList(string deptCode, string inPrivType, string inState, DateTime dtBegin, DateTime dtEnd)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetInputList.OperTime", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInputList.OperTime字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptCode, inPrivType, inState, dtBegin.ToString(), dtEnd.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetInputList.OperTime:" + ex.Message;
                return null;
            }

            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Input input;

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得入库列表信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    input = new Input();
                    input.StockDept.ID = this.Reader[0].ToString();							//申请科室编码
                    input.InListNO = this.Reader[1].ToString();						//入库单据号
                    input.PrivType = this.Reader[2].ToString();							//入库分类 0310
                    input.SystemType = this.Reader[3].ToString();						//三级权限码
                    input.OutListNO = this.Reader[4].ToString();						//出库单据号
                    input.Company.ID = this.Reader[5].ToString();						//供货单位编码
                    input.TargetDept.ID = input.Company.ID;
                    input.SpecialFlag = this.Reader[6].ToString();						//特殊标记 1 是  0 否
                    input.State = this.Reader[7].ToString();							//入库状态 0 申请 1 审批 2 核准
                    input.Operation.ApplyOper.ID = this.Reader[8].ToString();					//申请操作员
                    input.Operation.ApplyOper.OperTime = NConvert.ToDateTime(this.Reader[9].ToString());	//申请时间
                    input.Operation.ExamOper.ID = this.Reader[10].ToString();					//审批人
                    input.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[11].ToString());	//审批时间
                    input.Operation.ApproveOper.ID = this.Reader[12].ToString();					//核准人
                    input.Operation.ApproveOper.OperTime = NConvert.ToDateTime(this.Reader[13].ToString());//核准时间
                    input.InvoiceNO = this.Reader[14].ToString();						//发票号
                    input.PayState = this.Reader[15].ToString();						//供货商结存状态
                    input.DeliveryNO = this.Reader[16].ToString();						//送货单号
                    al.Add(input);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取入库列表信息时出错！" + ex.Message;
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
        /// 根据供货单位获取库存药品列表
        /// </summary>
        /// <param name="deptCode">库存科室库房编码</param>
        /// <param name="offerCompanyID">供货单位编码</param>
        /// <returns>成功返回Item数组 失败返回null</returns>
        public ArrayList QueryStorageListForBackInput(string deptCode, string offerCompanyID)
        {
            ArrayList al = new ArrayList();

            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageListForBackInput", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageListForBackInput字段!";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSQL = string.Format(strSQL, deptCode, offerCompanyID);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetStorageListForBackInput:" + ex.Message;
                return null;
            }
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得库存明细信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            //流水号、药品商品名、规格、购入价、发票号
            FS.HISFC.Models.Pharmacy.Item item;
            try
            {
                while (this.Reader.Read())
                {
                    item = new FS.HISFC.Models.Pharmacy.Item();
                    item.ID = this.Reader[0].ToString();								//入库流水号
                    item.Name = this.Reader[1].ToString();								//药品商品名
                    item.Specs = this.Reader[2].ToString();								//规格
                    item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[3].ToString());	//购入价
                    item.User01 = this.Reader[4].ToString();							//发票号
                    item.SpellCode = this.Reader[5].ToString();						//拼音码
                    item.WBCode = this.Reader[6].ToString();							//五笔码
                    item.NameCollection.RegularSpell.SpellCode = this.Reader[7].ToString();		//通用名拼音码
                    item.NameCollection.RegularSpell.WBCode = this.Reader[8].ToString();			//通用名五笔码

                    al.Add(item);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// 根据发票号获取入库明细信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="inState">入库状态 0 申请 1 审批 2 核准 A 检索全部状态</param>
        /// <returns>成功返回入库实体数组。失败返回null</returns>
        public ArrayList QueryInputInfoByInvoice(string deptCode, string invoiceNo, string inState)
        {
            string strSelect = "";
            string strWhere = "";
            //取Select语句
            if (this.GetSQL("Pharmacy.Item.GetInput", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInput字段Sql";
                return null;
            }
            //取Where语句
            if (this.GetSQL("Pharmacy.Item.GetInput.Invoice", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInput.Invoice字段Sql";
                return null;
            }
            //格式化SQL语句
            try
            {
                strSelect = string.Format(strSelect + strWhere, deptCode, invoiceNo, inState);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetInputInfoByInvoice:" + ex.Message;
                return null;
            }

            return this.myGetInput(strSelect);
        }

        /// <summary>
        /// 根据入库单据号获取指定供货单位入库明细信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="inListCode">入库单据号</param>
        /// <param name="offerCompany">供货单位编码 传"AAAA"则忽略该参数，查询所有供货单位</param>
        /// <param name="inState">入库单状态 "AAAA"则忽略该参数</param>
        /// <returns>成功返回入库实体数组 失败返回null</returns>
        public ArrayList QueryInputInfoByListID(string deptCode, string inListCode, string offerCompany, string inState)
        {
            string strSelect = "";
            string strWhere = "";
            //取Select语句
            if (this.GetSQL("Pharmacy.Item.GetInput", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInput字段Sql";
                return null;
            }
            //取Where语句
            if (this.GetSQL("Pharmacy.Item.GetInput.ListID", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInput.ListID字段Sql";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSelect = string.Format(strSelect + strWhere, deptCode, inListCode, offerCompany, inState);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetInputInfoByListID:" + ex.Message;
                return null;
            }
            return this.myGetInput(strSelect);
        }

        /// <summary>
        /// 根据入库单据号获取指定供货单位入库明细信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="inListCode">入库单据号</param>
        /// <param name="offerCompany">供货单位编码 传"AAAA"则忽略该参数，查询所有供货单位</param>
        /// <param name="inState">入库单状态 "AAAA"则忽略该参数</param>
        /// <returns>成功返回入库实体数组 失败返回null</returns>
        public ArrayList QueryInputInfoByList(string inListCode, string offerCompany, string inState)
        {
            string strSelect = "";
            string strWhere = "";
            //取Select语句
            if (this.GetSQL("Pharmacy.Item.GetInput", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInput字段Sql";
                return null;
            }
            //取Where语句
            if (this.GetSQL("Pharmacy.Item.GetInput.List", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInput.List字段Sql";
                return null;
            }

            //格式化SQL语句
            try
            {
                strSelect = string.Format(strSelect + strWhere, inListCode, offerCompany, inState);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetInputInfoByList:" + ex.Message;
                return null;
            }
            return this.myGetInput(strSelect);
        }

        /// <summary>
        /// 根据入库流水号获取入库明细信息
        /// </summary>
        /// <param name="inBillCode">入库流水号</param>
        /// <returns>成功返回入库实体 失败返回null</returns>
        public FS.HISFC.Models.Pharmacy.Input GetInputInfoByID(string inBillCode)
        {
            string strSelect = "";
            string strWhere = "";
            //取Select语句
            if (this.GetSQL("Pharmacy.Item.GetInput", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInput字段Sql";
                return null;
            }
            //取Where语句
            if (this.GetSQL("Pharmacy.Item.GetInput.ID", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInput.ID字段Sql";
                return null;
            }
            //格式化SQL语句
            try
            {
                strSelect = string.Format(strSelect + strWhere, inBillCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetInputInfoByID:" + ex.Message;
                return null;
            }

            ArrayList al = this.myGetInput(strSelect);
            if (al == null)
            {
                return null;
            }
            if (al.Count == 0)
            {
                this.Err = "数据发生变动！";
                return null;
            }
            return al[0] as FS.HISFC.Models.Pharmacy.Input;
        }

        /// <summary>
        /// 查询一组入库单号的入库明细按药品汇总
        /// </summary>
        /// <param name="deptCode">库存科室编码</param>
        /// <param name="inListCodes">入库单集合</param>
        /// <returns></returns>
        public ArrayList QueryInputInfoByListID(string deptCode, string inListCodes)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.Item.GetInputSum.ByDrugCode", ref strSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetInputSum.ByDrugCode字段";
                return null;
            }

            try
            {
                strSql = string.Format(strSql, deptCode, inListCodes);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.GetInputSum.ByDrugCode:" + ex.Message;
                return null;
            }

            ArrayList al = new ArrayList();
            FS.HISFC.Models.Pharmacy.Input input;

            //执行查询语句
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "获得入库明细信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    input = new Input();
                    input.Item.ID = this.Reader[0].ToString();
                    input.BatchNO = this.Reader[1].ToString();
                    input.Item.Qty = Convert.ToDecimal(this.Reader[2]);
                    al.Add(input);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取入库明细信息时出错" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        #endregion

        #region 入库保存

        /// <summary>
        /// 审核入库信息（药库发票入库）只用于状态更新
        /// </summary>
        /// <param name="Input">入库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int ExamInput(FS.HISFC.Models.Pharmacy.Input Input)
        {
            string strSQL = "";
            //审批入库信息（药库发票入库），更新入库状态为'1'			
            try
            {
                //by cube 2011-03-30 
                //不更改购入价和购入金额，否则对财务账务平衡有影响
                //更改供货公司和发票日期，这个属于扩展功能
                //decimal purchaseCost = System.Math.Round(Input.Quantity / Input.Item.PackQty * Input.Item.PriceCollection.PurchasePrice, 2);
                //取参数列表
                string[] strParm = {
									   Input.ID,								//0 入库流水号
									   Input.Operation.ExamQty.ToString(),				//1 审批数量
									   Input.Operation.ExamOper.ID,						//2 审批人
									   Input.Operation.ExamOper.OperTime.ToString(),				//3 审批日期
									   Input.InvoiceNO,							//4 发票号码
									   Input.Item.PriceCollection.PurchasePrice.ToString(),		//5 购入价
									   Input.PurchaseCost.ToString(),					//6 购入金额
									   this.Operator.ID,						//7 操作人
									   Input.Item.ID,							//8 药品编码
									   Input.GroupNO.ToString(),				//9 批次
                                       Input.Company.ID,
                                       Input.InvoiceDate.ToString(),
				};
                //end by

                int parm;
                //更新本条入库信息
                if (this.GetSQL("SOC.Pharmacy.Item.ExamInput", ref strSQL) == -1)
                {
                    strSQL = @" UPDATE  PHA_COM_INPUT SET
                                        EXAM_NUM = {1},    --审批数量
                                        EXAM_OPERCODE = '{2}',  --审批人
                                        EXAM_DATE = to_date('{3}','yyyy-mm-dd HH24:mi:ss') , --审批日期（药库发票入库日期）
                                        --IN_STATE = '1',
                                        INVOICE_NO = '{4}' ,        --发票号
                                        --PURCHASE_PRICE = {5} ,      --购入价
                                        --PURCHASE_COST = {6},         --购入金额
                                        Company_Code='{10}',
                                        Invoice_Date =to_date('{11}','yyyy-mm-dd HH24:mi:ss'),
                                        OPER_CODE = '{7}',
                                        OPER_DATE = sysdate
                                WHERE   IN_BILL_CODE = {0}
                                AND     DRUG_CODE = '{8}'
                                AND     GROUP_CODE = '{9}'
                                AND     nvl(Pay_State,'0') = '0'        
                         ";

                    this.CacheSQL("SOC.Pharmacy.Item.ExamInput", strSQL);
                }
                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
                parm = this.ExecNoQuery(strSQL);
                if (parm == -1)
                {
                    this.Err = "更新药品入库审批出错！";
                    return -1;
                }
                if (parm == 0)
                {
                    this.Err = "本记录已被核准！无法再次修改审批";
                    return 0;
                }

                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "审批入库记录的SQl参数赋值出错！Pharmacy.Item.ExamInput" + ex.Message;
                this.WriteErr();
                return -1;
            }
        }

        /// <summary>
        /// 冲红发票入库信息（药库发票入库）只用于状态更新
        /// </summary>
        /// <param name="Input">入库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int ExamInvoiceInput(FS.HISFC.Models.Pharmacy.Input Input)
        {
            string strSQL = "";
            //审批入库信息（药库发票入库），更新入库状态为'1'			
            try
            {
                //by cube 2011-03-30 
                //不更改购入价和购入金额，否则对财务账务平衡有影响
                //更改供货公司和发票日期，这个属于扩展功能
                //decimal purchaseCost = System.Math.Round(Input.Quantity / Input.Item.PackQty * Input.Item.PriceCollection.PurchasePrice, 2);
                //取参数列表
                string[] strParm = {
									   Input.ID,								//0 入库流水号
									   Input.Operation.ExamQty.ToString(),				//1 审批数量
									   Input.Operation.ExamOper.ID,						//2 审批人
									   Input.Operation.ExamOper.OperTime.ToString(),				//3 审批日期
									   Input.InvoiceNO,							//4 发票号码
									   Input.Item.PriceCollection.PurchasePrice.ToString(),		//5 购入价
									   Input.PurchaseCost.ToString(),					//6 购入金额
									   this.Operator.ID,						//7 操作人
									   Input.Item.ID,							//8 药品编码
									   Input.GroupNO.ToString(),				//9 批次
                                       Input.Company.ID,
                                       Input.InvoiceDate.ToString(),
                                       Input.RetailCost.ToString(),					//6 零售金额
				};
                //end by

                int parm;
                //更新本条入库信息
                if (this.GetSQL("SOC.Pharmacy.Item.ExamInvoiceInput", ref strSQL) == -1)
                {
                    strSQL = @" UPDATE  PHA_COM_INPUT SET
                                        IN_NUM = {1},    --入库数量
                                        EXAM_NUM = {1},    --审批数量
                                        EXAM_OPERCODE = '{2}',  --审批人
                                        EXAM_DATE = to_date('{3}','yyyy-mm-dd HH24:mi:ss') , --审批日期（药库发票入库日期）
                                        --IN_STATE = '1',
                                        INVOICE_NO = '{4}' ,        --发票号
                                        --PURCHASE_PRICE = {5} ,      --购入价
                                        PURCHASE_COST = {6},         --购入金额
                                        retail_cost = {12},         --零售金额
                                        Company_Code='{10}',
                                        Invoice_Date =to_date('{11}','yyyy-mm-dd HH24:mi:ss'),
                                        OPER_CODE = '{7}',
                                        OPER_DATE = sysdate
                                WHERE   IN_BILL_CODE = {0}
                                AND     DRUG_CODE = '{8}'
                                AND     GROUP_CODE = '{9}'
                                AND     nvl(Pay_State,'0') = '0'        
                         ";

                    this.CacheSQL("SOC.Pharmacy.Item.ExamInput", strSQL);
                }
                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
                parm = this.ExecNoQuery(strSQL);
                if (parm == -1)
                {
                    this.Err = "更新药品入库审批出错！";
                    return -1;
                }
                if (parm == 0)
                {
                    this.Err = "本记录已被核准！无法再次修改审批";
                    return 0;
                }

                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "审批入库记录的SQl参数赋值出错！Pharmacy.Item.ExamInput" + ex.Message;
                this.WriteErr();
                return -1;
            }
        }


        /// <summary>
        /// 核准入库信息（发票核准） 0 不更新库存 1 更新库存
        /// </summary>
        /// <param name="Input">入库记录类</param>
        /// <param name="updateStorageFlag">是否更新库存 0 不更新 1 更新</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int ApproveInput(FS.HISFC.Models.Pharmacy.Input Input, string updateStorageFlag)
        {
            string strSQL = "";
            int parm;
            //入库流水号不为空 说明已有入库记录 直接进行状态更新操作
            if (Input.ID != "")
            {
                #region 入库流水号不为空 对入库记录直接进行更新操作 更新库存信息状态
                //核准入库信息（发票核准），更新申请状态为'2'。
                if (this.GetSQL("Pharmacy.Item.ApproveInput", ref strSQL) == -1) return -1;
                try
                {
                    //取参数列表
                    string[] strParm = {
										   Input.ID,                        //入库流水号
										   Input.Quantity.ToString(),       //核准数量
										   Input.Operation.ApproveOper.ID,           //核准人
										   Input.Operation.ApproveOper.OperTime.ToString(),    //核准日期
                                           Input.InvoiceNO,                 //发票号
                                           Input.Item.PriceCollection.PurchasePrice.ToString(),      //购入价
										   this.Operator.ID,                //操作人                  
					};
                    strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
                    parm = this.ExecNoQuery(strSQL);
                    if (parm == -1)
                    {
                        this.Err = "核准入库记录执行出错！";
                        return -1;
                    }
                    //更新库存记录的库存状态 0暂入库 1 正式入库
                    FS.HISFC.Models.Pharmacy.StorageBase storageBase = Input.Clone() as FS.HISFC.Models.Pharmacy.StorageBase;
                    if (storageBase == null)
                    {
                        this.Err = "处理库存时候 发生数据类型转换错误";
                        return -1;
                    }

                    storageBase.Class2Type = Input.Class2Type;
                    storageBase.PrivType = Input.PrivType;

                    if (updateStorageFlag == "0")			//不更新
                        parm = this.UpdateStorageState(storageBase, "1", false);
                    else									//更新
                        parm = this.UpdateStorageState(storageBase, "1", true);
                    if (parm == -1)
                    {
                        this.Err = "更新申请科室库存数据入库状态时出错！";
                        return -1;
                    }
                    if (parm == 0)
                    {
                        storageBase.State = "1";		//库存状态
                        parm = this.InsertStorage(storageBase);
                        if (parm == -1)
                        {
                            this.Err = "对申请科室增加库存出错！";
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Err = "核准入库记录的SQl参数赋值时出错！Pharmacy.Item.ApproveInput" + ex.Message;
                    this.WriteErr();
                    return -1;
                }
                #endregion
            }
            else	//如无入库记录，则插入一条入库记录
            {
                parm = this.Input(Input, updateStorageFlag, "1");
                if (parm == -1)
                {
                    return -1;
                }
            }
            //处理申请数据 更新状态
            if (Input.OutListNO != "")
            {
                //不进行申请数据状态更新。该状态更新由外部操作完成
                //if( this.UpdateApplyOutState( Input.StockDept.ID , Input.OutListNO , "2" ) == -1 )
                //{
                //    return -1;
                //}
            }
            //如存在对应的出库记录 则更新出库状态为 2
            if (Input.OutBillNO != "" && Input.OutBillNO != "0")
            {
                #region 处理出库记录
                //ArrayList alOutput;
                FS.HISFC.Models.Pharmacy.Output output;
                //全部更新导致多条出库记录对应一条入库
                //alOutput = this.QueryOutputList( Input.OutBillNO );                
                //if( alOutput == null )
                //{
                //    this.Err = "更新出库记录过程中 获取出库记录出错！";
                //    return -1;
                //}
                //for( int i = 0 ; i < alOutput.Count ; i++ )
                //{
                //    output = alOutput[ i ] as FS.HISFC.Models.Pharmacy.Output;
                //    if( output == null )
                //    {
                //        this.Err = "更新出库记录过程中 数据类型转换出错！";
                //        return -1;
                //    }
                //    output.State = "2";
                //    output.InListNO = Input.InListNO;
                //    output.InBillNO = Input.ID;

                //    parm = this.UpdateOutput( output );
                //    if( parm == -1 )
                //    {
                //        this.Err = "更新出库记录执行出错！" + this.Err;
                //        return -1;
                //    }
                //}

                output = this.GetOutputDetail(Input.OutBillNO, Input.OutSerialNO);
                if (output == null)
                {
                    this.Err = "更新出库记录过程中 获取出库记录出错！";
                    return -1;
                }
                //并发判断，防止重复核准入库
                if (output.State == "2")
                {
                    this.Err = output.Item.Name + "该数据已进行过核准，不能重复核准入库";
                    return -1;
                }
                //并发判断，防止退库
                if (Input.Quantity != output.Quantity - output.Operation.ReturnQty)
                {
                    this.Err = output.Item.Name + "已经退库，不能再核准入库";
                    return -1;
                }

                output.State = "2";
                output.InListNO = Input.InListNO;
                output.InBillNO = Input.ID;
                output.InSerialNO = Input.SerialNO;
                output.Operation.ApproveOper = Input.Operation.Oper;

                parm = this.UpdateOutput(output);
                if (parm == -1)
                {
                    this.Err = "更新出库记录执行出错！" + this.Err;
                    return -1;
                }
                #endregion
            }

            #region by cube 药品财务月结不可以更新这些信息，只能退库处理
            /*
            if (Input.StockDept.Memo == "PI")		//标志是药库的入库核准  
            {
                #region 对全院库存更新购入价、发票信息
                decimal purchaseCost = System.Math.Round(Input.Quantity / Input.Item.PackQty * Input.Item.PriceCollection.PurchasePrice, 2);
                //取参数列表
                string[] strParmPrice = {
											Input.ID,								//0 入库流水号
											Input.Operation.ExamQty.ToString(),				//1 审批数量
											Input.Operation.ExamOper.ID,						//2 审批人
											Input.Operation.ExamOper.OperTime.ToString(),				//3 审批日期
											Input.InvoiceNO,						//4 发票号码
											Input.Item.PriceCollection.PurchasePrice.ToString(),	//5 购入价
											purchaseCost.ToString(),				//6 购入金额
											this.Operator.ID,						//7 操作人
											Input.Item.ID,							//8 药品编码
											Input.GroupNO.ToString(),				//9 批次
				};
                //更新全院药品库存购入价、入库发票号
                if (this.GetSQL("Pharmacy.Item.UpdatePriceStorage", ref strSQL) == -1) return -1;
                strSQL = string.Format(strSQL, strParmPrice);        //替换SQL语句中的参数。
                parm = this.ExecNoQuery(strSQL);
                if (parm == -1)
                {
                    this.Err = "更新库存表购入价时出错！";
                    return -1;
                }

                //更新全院药品出库购入价
                if (this.GetSQL("Pharmacy.Item.UpdatePriceOutput", ref strSQL) == -1) return -1;
                strSQL = string.Format(strSQL, strParmPrice);        //替换SQL语句中的参数。
                parm = this.ExecNoQuery(strSQL);
                if (parm == -1)
                {
                    this.Err = "更新药品出库表购入价时出错！";
                    return -1;
                }
                #endregion

                //设定控制参数是否对此进行更新 更新药品字典内 信息
                //控制参数为 1 更新药品字典信息
                FS.FrameWork.Management.ControlParam ctrlManager = new FS.FrameWork.Management.ControlParam();
                ctrlManager.SetTrans(this.Trans);
                //string approveUpdateBaseFlag = ctrlManager.QueryControlerInfo("510002");
                string approveUpdateBaseFlag = ctrlManager.QueryControlerInfo("P00572");
                if (approveUpdateBaseFlag == "1")
                {
                    parm = this.UpdateItemInputInfo(Input);
                    if (parm == -1)
                    {
                        this.Err = "更新药品字典表内信息出错" + this.Err;
                        return -1;
                    }
                }

                #region 生成供货商结存信息

                if (Input.Item.PackQty == 0)
                    Input.Item.PackQty = 1;

                Input.RetailCost = System.Math.Round((Input.Quantity / Input.Item.PackQty * Input.Item.PriceCollection.RetailPrice), 2);
                Input.WholeSaleCost = System.Math.Round((Input.Quantity / Input.Item.PackQty * Input.Item.PriceCollection.WholeSalePrice), 2);
                Input.PurchaseCost = System.Math.Round((Input.Quantity / Input.Item.PackQty * Input.Item.PriceCollection.PurchasePrice), 2);

                if (this.Pay(Input) == -1)
                {
                    this.Err = "供货商结存信息生成错误" + this.Err;
                    return -1;
                }

                #endregion
            }
            */
            #endregion

            return parm;
        }

        /// <summary>
        /// 根据入库信息更新药品字典信息
        /// 
        /// //{476ED544-49A6-4070-9ACB-C581F403347D} 对字典记录进行入库信息更新
        /// </summary>
        /// <param name="input">药品字典</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateBaseItemWithInputInfo(FS.HISFC.Models.Pharmacy.Input input)
        {
            //设定控制参数是否对此进行更新 更新药品字典内 信息
            //控制参数为 1 更新药品字典信息
            FS.FrameWork.Management.ControlParam ctrlManager = new FS.FrameWork.Management.ControlParam();
            string approveUpdateBaseFlag = ctrlManager.QueryControlerInfo("P00572");
            if (approveUpdateBaseFlag == "1")
            {
                int parm = this.UpdateItemInputInfo(input);
                if (parm == -1)
                {
                    this.Err = "更新药品字典表内信息出错" + this.Err;
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 同时更新入库审批（发票入库）、入库核准（发票核准）信息、更新状态为"2"
        /// </summary>
        /// <param name="Input">入库记录类</param>
        /// <param name="updateStorageFlag">是否同步更新库存 0 不更新 1 更新库存</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int SetInput(FS.HISFC.Models.Pharmacy.Input Input, string updateStorageFlag)
        {
            int parm;
            //进行入库审批操作
            parm = this.ExamInput(Input);
            if (parm == -1)
                return -1;
            //入库核准操作
            return this.ApproveInput(Input, updateStorageFlag);
        }

        /// <summary>
        /// 对生产入库进行处理、同步扣除原材料的库存
        /// </summary>
        /// <param name="input">入库实体</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int ProduceInput(FS.HISFC.Models.Pharmacy.Input input)
        {
            return 1;
        }

        /// <summary>
        /// 对一般入库、特殊入库进行处理 根据是否同步更新库存、库存 入库状态为暂入库 0 
        /// </summary>
        /// <param name="input">入库实体</param>
        /// <param name="updateStorageFlag">是否更新库存 0 不更新 1 更新</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int Input(FS.HISFC.Models.Pharmacy.Input input, string updateStorageFlag)
        {
            return Input(input, updateStorageFlag, "0");
        }

        /// <summary>
        /// 对一般入库、特殊入库进行处理 根据是否同步更新库存、库存
        /// </summary>
        /// <param name="input">入库实体</param>
        /// <param name="updateStorageFlag">是否更新库存 0 不更新 1 更新</param>
        /// <param name="storageState">库存状态 0 暂入库 1 正式入库</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int Input(FS.HISFC.Models.Pharmacy.Input input, string updateStorageFlag, string storageState)
        {
            //对入库退库进行处理 插入负记录 更新原记录 退库数量
            if (input.SystemType == "19")
            {
                #region 入库退库
                if (input.ID != "")
                {
                    //更新原入库记录退库数量
                    if (this.UpdateInputReturnNum(input.ID, input.SerialNO, -input.Quantity) != 1)
                    {
                        this.Err = this.Err + "更新入库记录退库数量出错！";
                        return -1;
                    }
                }

                //插入负记录
                input.ID = "";
                if (this.InsertInput(input) == -1)
                {
                    return -1;
                }
                #endregion
            }
            else	//对其他类型直接进行插入操作
            {
                if (this.InsertInput(input) == -1)
                {
                    return -1;
                }
            }
            //需要更新库存
            if (updateStorageFlag == "1")
            {
                if (input.SystemType == EnumIMAInTypeService.GetNameFromEnum(EnumIMAInType.CommonInput) || input.SystemType == EnumIMAInTypeService.GetNameFromEnum(EnumIMAInType.SpecialInput))
                {
                    //...一般入库、特殊入库不进行价格判断。
                }
                else
                {
                    #region  判断入库价格与库存价格(当前最新价格)是否一致 不一致处理调价记录---零售价
                    decimal dNowPrice = 0;
                    DateTime sysTime = this.GetDateTimeFromSysDateTime();
                    if (this.GetNowPrice(input.StockDept.ID, input.Item.ID, ref dNowPrice) == -1)
                    {
                        this.Err = "处理入库记录退库过程中 获取药品" + input.Item.Name + "零售价出错";
                        return -1;
                    }
                    if (input.Item.PriceCollection.RetailPrice != dNowPrice)    
                    {
                        if (input.SystemType == "19")                //只对入库退库的情况 进行判断 
                        {

                            #region 调价盈亏处理-零售价

                            string adjustPriceID = this.GetSequence("Pharmacy.Item.GetNewAdjustPriceID");
                            if (adjustPriceID == null)
                            {
                                this.Err = "入库退库药品已发生调价 插入调价盈亏记录过程中获取调价单号出错！";
                                return -1;
                            }
                            FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = new AdjustPrice();
                            adjustPrice.ID = adjustPriceID;								//调价单号
                            adjustPrice.SerialNO = 0;									//调价单内序号
                            adjustPrice.Item = input.Item.Clone();
                            adjustPrice.StockDept.ID = input.StockDept.ID;						//调价科室 
                            adjustPrice.State = "1";									//调价状态 1 已调价
                            adjustPrice.StoreQty = input.Quantity;
                            adjustPrice.Operation.ID = this.Operator.ID;
                            adjustPrice.Operation.Name = this.Operator.Name;
                            adjustPrice.Operation.Oper.OperTime = sysTime;
                            adjustPrice.InureTime = sysTime;
                            adjustPrice.AfterRetailPrice = dNowPrice;					//调价后零售价
                            if (dNowPrice - input.Item.PriceCollection.RetailPrice > 0)
                                adjustPrice.ProfitFlag = "1";							//调盈
                            else
                                adjustPrice.ProfitFlag = "0";							//调亏

                            adjustPrice.Memo = "入库退库补调价盈亏";
                            if (this.InsertAdjustPriceInfo(adjustPrice) == -1)
                            {
                                return -1;
                            }
                            if (this.InsertAdjustPriceDetail(adjustPrice) == -1)
                            {
                                return -1;
                            }

                            #endregion
                        }
                        else
                        {
                            #region 调价盈亏处理

                            string adjustPriceID = this.GetSequence("Pharmacy.Item.GetNewAdjustPriceID");
                            if (adjustPriceID == null)
                            {
                                this.Err = "入库核准药品已发生调价 插入调价盈亏记录过程中获取调价单号出错！";
                                return -1;
                            }
                            FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = new AdjustPrice();
                            adjustPrice.ID = adjustPriceID;								//调价单号
                            adjustPrice.SerialNO = 0;									//调价单内序号
                            adjustPrice.Item = input.Item;
                            adjustPrice.StockDept.ID = input.StockDept.ID;						//调价科室 
                            adjustPrice.State = "1";									//调价状态 1 已调价
                            adjustPrice.StoreQty = input.Quantity;
                            adjustPrice.Operation.ID = this.Operator.ID;
                            adjustPrice.Operation.Name = this.Operator.Name;
                            adjustPrice.Operation.Oper.OperTime = sysTime;
                            adjustPrice.InureTime = sysTime;
                            adjustPrice.AfterRetailPrice = dNowPrice;					//调价后零售价
                            if (dNowPrice - input.Item.PriceCollection.RetailPrice > 0)
                                adjustPrice.ProfitFlag = "1";							//调盈
                            else
                                adjustPrice.ProfitFlag = "0";							//调亏

                            adjustPrice.Memo = "入库核准补调价盈亏";
                            if (this.InsertAdjustPriceInfo(adjustPrice) == -1)
                            {
                                return -1;
                            }
                            if (this.InsertAdjustPriceDetail(adjustPrice) == -1)
                            {
                                return -1;
                            }

                            #endregion
                        }
                        //{39EBA591-1666-4ab5-B3F3-5B273DA4A623}     入库后，单科调价，退库 此时不应形成调价盈亏 造成账目不平
                    }
                    #endregion

                    #region 判断退库购入价与库存购入价是否一致，不一致处理购入价调价记录
                    if (input.SystemType == "19")                //只对入库退库的情况 进行判断 
                    {                        
                        ArrayList alStorage = this.QueryStorageList(input.StockDept.ID, input.Item.ID, input.GroupNO);
                        if (alStorage != null && alStorage.Count > 0)
                        {
                            FS.HISFC.Models.Pharmacy.Storage sto = alStorage[0] as FS.HISFC.Models.Pharmacy.Storage;
                            if (sto.Item.PriceCollection.PurchasePrice != input.Item.PriceCollection.PurchasePrice && input.SystemType == "19")
                            {



                                string adjustPurchasePriceID = this.GetSequence("Pharmacy.Item.GetNewAdjustPriceID.Purchase");
                                if (adjustPurchasePriceID == null)
                                {
                                    this.Err = "入库退库药品购入价发送调价 插入调价盈亏记录过程中获取调价单号出错！";
                                    return -1;
                                }
                                FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = new AdjustPrice();
                                adjustPrice.ID = adjustPurchasePriceID;								//调价单号
                                adjustPrice.SerialNO = 0;									//调价单内序号
                                adjustPrice.Item = input.Item.Clone();
                                adjustPrice.StockDept.ID = input.StockDept.ID;						//调价科室 
                                adjustPrice.State = "1";									//调价状态 1 已调价
                                adjustPrice.StoreQty = Math.Abs(input.Quantity);
                                adjustPrice.Operation.ID = this.Operator.ID;
                                adjustPrice.Operation.Name = this.Operator.Name;
                                adjustPrice.Operation.Oper.OperTime = sysTime;
                                adjustPrice.InureTime = sysTime;
                                adjustPrice.AfterWholesalePrice = input.Item.PriceCollection.PurchasePrice;		//调价后购入价，即退库时输入的购入价
                                adjustPrice.PriceCollection.WholeSalePrice = sto.Item.PriceCollection.PurchasePrice;                 //调价前购入价，即库存购入价，放到批发价属性里
                                adjustPrice.FileNO = input.InListNO;
                                adjustPrice.GroupNO = input.GroupNO;
                                if (sto.Item.PriceCollection.PurchasePrice - input.Item.PriceCollection.PurchasePrice > 0)
                                    adjustPrice.ProfitFlag = "0";							//调亏
                                else
                                    adjustPrice.ProfitFlag = "1";							//调亏

                                adjustPrice.Memo = "入库退库补调价盈亏";
                                if (this.InsertAdjustPurchasePriceInfo(adjustPrice) == -1)
                                {
                                    return -1;
                                }
                            }





                        }
                    }
                    #endregion
                }

                #region 库存更新
                if (this.UpdateStorageForInput(input, storageState) == -1)
                    return -1;

                #endregion
            }
            //更新药品字典表内信息
            //----
            return 1;
        }

        #endregion

        /// <summary>
        /// 获取上次一般入库部分信息，用于一般入库界面信息填充 by Sunjh 2010-10-29 {97C93751-7EED-4160-931A-EC77C1F4E291}
        /// </summary>
        /// <param name="drugCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Pharmacy.Input GetLastInBillRecord(string drugCode)
        {
            string sqlStr = @"select t.batch_no,t.valid_date,t.company_code,t.producer_code from pha_com_input t 
                              where t.in_bill_code=(select max(t.in_bill_code) from pha_com_input t 
                                                    where t.drug_code='{0}' and t.class3_meaning_code='11')";
            sqlStr = string.Format(sqlStr, drugCode);
            if (this.ExecQuery(sqlStr) == -1)
            {
                this.Err = "获取上次入库信息失败!" + this.Err;

                return null;
            }

            FS.HISFC.Models.Pharmacy.Input inputObj = new FS.HISFC.Models.Pharmacy.Input();
            try
            {
                while (this.Reader.Read())
                {
                    inputObj.BatchNO = this.Reader[0].ToString();
                    inputObj.ValidTime = Convert.ToDateTime(this.Reader[1].ToString());
                    inputObj.Company.ID = this.Reader[2].ToString();
                    inputObj.Producer.ID = this.Reader[3].ToString();
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取上次入库信息失败!" + ex.Message;

                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return inputObj;
        }

        #endregion

        #endregion

        #region 出库表操作

        #region 内部使用

        #region 出库记录/信息查询

        /// <summary>
        /// 按出库单流水号查询出库记录（可能多条）
        /// </summary>
        /// <returns>成功返回满足条件的出库记录 失败返回null</returns>
        public ArrayList QueryOutputList(string outputID)
        {
            string strSQL = "";
            string strWhere = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetOutputList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetOutputList.ByID", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList.ByID字段!";
                return null;
            }

            strSQL = string.Format(strSQL + strWhere, outputID);

            //根据SQL语句取药品类数组并返回数组
            return this.myGetOutput(strSQL);
        }

        /// <summary>
        /// 按出库单流水号查询出库记录
        /// </summary>
        /// <param name="outputID">出库流水号</param>
        /// <param name="groupNO">库存批次</param>
        /// <returns>成功返回满足条件的出库记录 失败返回null</returns>
        public FS.HISFC.Models.Pharmacy.Output GetOutputDetail(string outputID, string groupNO)
        {
            ArrayList al = this.QueryOutputList(outputID);
            if (al == null)
            {
                return null;
            }
            if (al.Count == 0)
            {
                return new Output();
            }
            foreach (FS.HISFC.Models.Pharmacy.Output output in al)
            {
                if (output.GroupNO.ToString() == groupNO)
                {
                    return output;
                }
            }

            return null;
        }

        /// <summary>
        /// 按出库单流水号查询出库记录
        /// </summary>
        /// <param name="outputID">出库流水号</param>
        /// <param name="serialNO">单内序号</param>
        /// <returns>成功返回满足条件的出库记录 失败返回null</returns>
        public FS.HISFC.Models.Pharmacy.Output GetOutputDetail(string outputID, int serialNO)
        {
            ArrayList al = this.QueryOutputList(outputID);
            if (al == null)
            {
                return null;
            }
            if (al.Count == 0)
            {
                return new Output();
            }
            foreach (FS.HISFC.Models.Pharmacy.Output output in al)
            {
                if (output.SerialNO == serialNO)
                {
                    return output;
                }
            }

            return null;
        }

        /// <summary>
        /// 根据处方号、处方内项目流水号获取出库实体
        /// </summary>
        /// <param name="recipeNo">处方号</param>
        /// <param name="sequenceNo">处方内项目流水号</param>
        /// <param name="systemType">系统类别 M1门诊收 M2门诊退 Z1住院收 Z2住院退</param>
        /// <returns>成功返回1  失败返回－1</returns>
        public ArrayList QueryOutputList(string recipeNo, int sequenceNo, string systemType)
        {
            string strSQL = "";
            string strWhere = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetOutputList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetOutputList.ByRecipeNo", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList.ByRecipeNo字段!";
                return null;
            }

            strSQL = string.Format(strSQL + strWhere, recipeNo, sequenceNo.ToString(), systemType);

            //根据SQL语句取药品类数组并返回数组
            return this.myGetOutput(strSQL);
        }

        /// <summary>
        /// 根据出库单据号、取药人字段获取出库信息
        /// 制剂管理调用时 取药人字段存储成品编码 用于确定同一生产计划内不同药品
        /// </summary>
        /// <param name="outListCode">出库单据号</param>
        /// <param name="getPersonID">取药人</param>
        /// <returns>成功返回对应的出库实体数组 失败返回null</returns>
        public ArrayList QueryOutList(string outListCode, string getPersonID)
        {
            string strSQL = "";
            string strWhere = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetOutputList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetOutputList.ByListCode.PersonID", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList..ByListCode.PersonID字段!";
                return null;
            }

            strSQL = string.Format(strSQL + strWhere, outListCode, getPersonID);

            //根据SQL语句取药品类数组并返回数组
            return this.myGetOutput(strSQL);
        }

        /// <summary>
        /// 按出库单据号查询出库记录
        /// </summary>
        /// <param name="deptCode">出库科室</param>
        /// <param name="outListCode">出库单据号</param>
        /// <param name="state">出库状态 "A"忽略出库状态</param>
        /// <returns>成功返回 output实体数组 失败返回null</returns>
        public ArrayList QueryOutputInfo(string deptCode, string outListCode, string state)
        {
            string strSQL = "";
            string strWhere = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetOutputList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetOutputList.ByListCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList.ByListCode字段!";
                return null;
            }

            strSQL = string.Format(strSQL + strWhere, deptCode, outListCode, state);

            //根据SQL语句取药品类数组并返回数组
            return this.myGetOutput(strSQL);
        }

        /// <summary>
        /// 获取出库单据列表
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="class3MeaningCode">三级权限码 "A"忽略权限信息</param>
        /// <param name="outState">出库状态 "A"忽略状态信息</param>
        /// <returns>成功返回neuobject数组 ID 出库单号 Name 领药单位名称 Memo 领药单位编码 User01 申请出库人编码 出错返回null</returns>
        public ArrayList QueryOutputList(string deptCode, string class3MeaningCode, string outState)
        {
            return this.QueryOutputList(deptCode, class3MeaningCode, "AAAA", outState);
        }

        /// <summary>
        /// 获取出库单据列表
        /// </summary>
        /// <param name="outDeptCode">出库科室</param>
        /// <param name="class3MeaningCode">三级权限码 “A”忽略权限信息</param>
        /// <param name="storageDept">领药科室编码</param>
        /// <param name="outState">出库状态 "A"忽略状态信息</param>
        /// <returns>成功返回neuobject数组 ID 出库单号 Name 领药单位名称 Memo 领药单位编码 User01 申请出库人编码 出错返回null</returns>
        public ArrayList QueryOutputList(string outDeptCode, string class3MeaningCode, string storageDept, string outState)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            string strString = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetOutListInfo", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutListInfo字段!";
                return null;
            }
            try
            {
                strString = string.Format(strSQL, outDeptCode, class3MeaningCode, storageDept, outState);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            FS.FrameWork.Models.NeuObject info;

            if (this.ExecQuery(strString) == -1)
            {
                this.Err = "获得出库信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();		//出库单号
                    info.Name = this.Reader[1].ToString();		//领药单位名称
                    info.Memo = this.Reader[2].ToString();		//领药单位编码
                    info.User01 = this.Reader[3].ToString();	//申请人

                    al.Add(info);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获取出库列表信息出错" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 获取出库单据列表
        /// </summary>
        /// <param name="outDeptCode">出库科室</param>
        /// <param name="class3MeaningCode">三级权限码</param>
        /// <param name="outState">出库状态 </param>
        /// <param name="dtBegin">查询起始时间</param>
        /// <param name="dtEnd">查询终止时间</param>
        /// <returns>成功返回neuobject数组 ID 出库单号 Name 领药单位 Memo 领药单位编码 User01 自定义类型 出错返回null</returns>
        public ArrayList QueryOutputList(string outDeptCode, string class3MeaningCode, string outState, DateTime dtBegin, DateTime dtEnd)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            string strString = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetOutListInfo.OperTime", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutListInfo.OperTime字段!";
                return null;
            }
            try
            {
                strString = string.Format(strSQL, outDeptCode, class3MeaningCode, outState, dtBegin.ToString(), dtEnd.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            FS.FrameWork.Models.NeuObject info;

            if (this.ExecQuery(strString) == -1)
            {
                this.Err = "获得出库信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();		//出库单号
                    info.Name = this.Reader[1].ToString();		//领药单位名称
                    info.Memo = this.Reader[2].ToString();		//领药单位编码
                    info.User01 = this.Reader[3].ToString();	//权限类型 自定义类型

                    al.Add(info);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获取出库列表信息出错" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 获取出库单据列表 供入库核准
        /// </summary>
        /// <param name="storageDept">领药科室</param>
        /// <param name="dtBegin">统计起始时间</param>
        /// <param name="dtEnd">统计截至时间</param>
        /// <returns>成功返回neuobject数组 Id 单据号 Name 出库科室 Memo 出库科室编码 失败返回null</returns>
        public ArrayList QueryOutputListForApproveInput(string storageDept, DateTime dtBegin, DateTime dtEnd)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";
            string strString = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetOutListForApproveInput.OperTime", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutListForApproveInput.OperTime字段!";
                return null;
            }
            try
            {
                strString = string.Format(strSQL, storageDept, dtBegin.ToString(), dtEnd.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            FS.FrameWork.Models.NeuObject info;
            if (this.ExecQuery(strString) == -1)
            {
                this.Err = "获得出库信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();		//单据号
                    info.Name = this.Reader[1].ToString();		//出库单位名称
                    info.Memo = this.Reader[2].ToString();		//出库单位编码
                    info.User01 = this.Reader[3].ToString();	//入库类型

                    al.Add(info);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获取出库列表信息出错" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 出库记录
        /// </summary>
        /// <param name="output">出库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public ArrayList QueryOutputList(FS.HISFC.Models.Pharmacy.Output output)
        {
            string strSQL = "";
            string strWhere = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetOutputList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetOutputList.ByID", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList.ByID字段!";
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetOutput(strSQL);
        }

        #endregion

        /// <summary>
        /// 更新一条出库记录中的"已退库数量"字段（加操作）
        /// </summary>
        /// <param name="outputID">出库单号</param>
        /// <param name="SerialNO">单内序号</param>
        /// <param name="returnNum">退库数量</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateOutputReturnNum(string outputID, int SerialNO, decimal returnNum)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateOutputReturnNum", ref strSQL) == -1)
            {
                this.Err = "找不到SQL语句！Pharmacy.Item.UpdateOutputReturnNum";
                return -1;
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   outputID, 
									   SerialNO.ToString(), 
									   returnNum.ToString(),
									   this.Operator.ID
								   };
                strSQL = string.Format(strSQL, strParm);              //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新退库数量的SQl参数赋值出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 审核出库信息（打印摆药单、摆药）
        /// </summary>
        /// <param name="Output">出库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int ExamOutput(FS.HISFC.Models.Pharmacy.Output Output)
        {
            string strSQL = "";
            //审核出库信息（打印摆药单、摆药），更新出库状态为'1'。
            if (this.GetSQL("Pharmacy.Item.ExamOutput", ref strSQL) == -1)
            {
                this.Err = "找不到SQL语句！Pharmacy.Item.ExamOutput";
                return -1;
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   Output.ID,                     //出库流水号
									   Output.Operation.ExamQty.ToString(),     //审批数量
									   Output.Operation.ExamOper.ID,           //审批人
									   Output.Operation.ExamOper.OperTime.ToString(),    //审批日期
									   this.Operator.ID,              //操作人
									   Output.Operation.ExamOper.OperTime.ToString()     //操作时间	                   
								   };


                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "审批出库记录的SQl参数赋值出错！Pharmacy.Item.ExamOutput" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 核准出库信息（摆药确认）
        /// </summary>
        /// <param name="Output">出库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int ApproveOutput(FS.HISFC.Models.Pharmacy.Output Output)
        {
            string strSQL = "";
            if (Output.SequenceNO > 0)
            {
                if (this.GetSQL("SOC.Pharmacy.Item.ApproveOutput", ref strSQL) == -1)
                {
                    strSQL = @"update  pha_com_output t
                            set    t.out_state = '2',
                                   t.approve_opercode = '{2}',
                                   t.approve_date = to_date('{3}','yyyy-mm-dd HH24:mi:ss'),
                                   t.oper_code = '{4}',
                                   t.oper_date =  to_date('{5}','yyyy-mm-dd HH24:mi:ss')  
                            where  t.out_bill_code = {0} and t.serial_code = {6}";
                    this.CacheSQL("SOC.Pharmacy.Item.ApproveOutput", strSQL);
                }
            }
            else
            {
                if (this.GetSQL("Pharmacy.Item.ApproveOutput", ref strSQL) == -1)
                {
                    strSQL = @"update  pha_com_output t
                            set    t.out_state = '2',
                                   t.approve_opercode = '{2}',
                                   t.approve_date = to_date('{3}','yyyy-mm-dd HH24:mi:ss'),
                                   t.oper_code = '{4}',
                                   t.oper_date =  to_date('{5}','yyyy-mm-dd HH24:mi:ss')  
                            where  t.out_bill_code = {0}";
                    this.CacheSQL("Pharmacy.Item.ApproveOutput", strSQL);
                }
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   Output.ID,                        //出库流水号
									   Output.Quantity.ToString(),       //核准数量
									   Output.Operation.ApproveOper.ID,           //核准人
									   Output.Operation.ApproveOper.OperTime.ToString(),    //核准日期
									   this.Operator.ID,                 //操作人
									   Output.Operation.ApproveOper.OperTime.ToString(),     //操作时间	 
                                       Output.SerialNO.ToString()
								   };


                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "核准出库记录的SQl参数赋值时出错！Pharmacy.Item.ApproveOutput" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 同时更新出库审批（摆药）、出库核准（摆药确认）信息
        /// </summary>
        /// <param name="Output">出库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int SetOutput(FS.HISFC.Models.Pharmacy.Output Output)
        {
            string strSQL = "";
            //同时更新出库审批（摆药）、出库核准（摆药确认）信息，更新申请状态为'2'。
            if (this.GetSQL("Pharmacy.Item.SetOutput", ref strSQL) == -1)
            {
                this.Err = "找不到SQL语句！Pharmacy.Item.SetOutput";
                return -1;
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   Output.ID,                  //出库流水号
									   Output.Quantity.ToString(), //审批数量
									   Output.Operation.ExamOper.ID,        //审批人
									   Output.Operation.ExamOper.OperTime.ToString(), //审批日期
									   Output.Quantity.ToString(), //核准数量
									   Output.Operation.ExamOper.ID,        //核准人
									   Output.Operation.ExamOper.OperTime.ToString(), //核准日期
									   this.Operator.ID,           //操作人
									   Output.Operation.ExamOper.OperTime.ToString()  //操作时间	                   
								   };


                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新出库记录的SQl参数赋值时出错！Pharmacy.Item.SetOutput" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

       

        #region 出库操作

        /// <summary>
        /// 根据出库申请进行摆药出库。
        /// 此方法适合于摆药同时扣库存的情况。如果摆药时，不扣库存而只是核准出库申请单，调用ApproveApplyOut();
        /// 如果此方法返回0，说明程序产生并发，欲核准的出库申请已经被其他调用者核准或者作废。
        /// 执行操作：
        /// 1、将出库申请数据转为出库数据
        /// 2、进行出库综合处理
        /// 3、核准出库申请单
        /// 4、消减预扣库存
        /// </summary>
        /// <param name="applyOut">出库申请实体</param>
        /// <returns>1成功，0没有更新，-1失败 ErrCode 2 库存不足</returns>
        public int Output(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {

            //将出库申请数据转为出库数据。								 
            FS.HISFC.Models.Pharmacy.Output output = new Output();
            //by cube 2011-05-19
            if ((applyOut.SystemType == "M1" || applyOut.SystemType == "M2") && !string.IsNullOrEmpty(applyOut.DrugNO))
            {
                //替代药房，0和1都是默认值
                if (applyOut.DrugNO != "0" && applyOut.DrugNO != "1")
                {
                    output.StockDept.ID = applyOut.DrugNO;             //出库科室＝摆药核准科室
                }
                else
                {
                    output.StockDept.ID = applyOut.Operation.ApproveOper.Dept.ID;             //出库科室＝摆药核准科室
                }
            }
            else
            {
                output.StockDept.ID = applyOut.Operation.ApproveOper.Dept.ID;             //出库科室＝摆药核准科室
            }

            output.SystemType = applyOut.SystemType;                            //系统类型＝出库申请类型
            output.PrivType = applyOut.PrivType;
            output.Item = applyOut.Item;                                        //药品实体
            output.ShowState = applyOut.ShowState;                              //显示的单位标记（0最小单位，1包装单位）
           
            //output.Quantity = applyOut.Operation.ApproveQty * applyOut.Days;    //出库数量＝摆药核准数量
            output.Quantity = applyOut.Operation.ApproveQty;    //出库数量＝摆药核准数量
            
            output.State = applyOut.State;                                      //出库状态＝摆药状态
            output.GetPerson = applyOut.PatientNO;                              //取 药 人＝患者ID
            output.DrugedBillNO = applyOut.DrugNO;                              //摆药单号
            output.SpecialFlag = "0";                                           //特殊标记。1是，0否
            output.TargetDept = applyOut.ApplyDept;                             //领用科室＝出库申请科室
            output.RecipeNO = applyOut.RecipeNO;                                //处方号
            output.SequenceNO = applyOut.SequenceNO;                            //处方内流水号

            //output.Operation.ApplyQty = applyOut.Operation.ApplyQty * applyOut.Days;     //出库申请数量
            output.Operation.ApplyQty = applyOut.Operation.ApplyQty;     //出库申请数量
           
            output.Operation.ApplyOper.ID = applyOut.Operation.ApplyOper.ID;             //出库申请人编码
            output.Operation.ApplyOper.OperTime = applyOut.Operation.ApplyOper.OperTime; //出库申请日期

            //output.Operation.ExamQty = applyOut.Operation.ApproveQty * applyOut.Days;    //审批出库数量＝摆药核准数量
            output.Operation.ExamQty = applyOut.Operation.ApproveQty;    //审批出库数量＝摆药核准数量
            
            output.Operation.ExamOper.ID = applyOut.Operation.ExamOper.ID;               //审批人 ＝打印人
            output.Operation.ExamOper.OperTime = applyOut.Operation.ExamOper.OperTime;   //审批日期＝打印日期

            //by cube 2011-04-07 出库时间赋值
            output.OutDate = applyOut.Operation.ExamOper.OperTime;
            output.Operation.Oper.ID = this.Operator.ID;
            output.Operation.Oper.OperTime = applyOut.Operation.ExamOper.OperTime;
            //end by

            output.State = "2";

            //制剂管理业务中 存储生产计划号
            if (applyOut.BillNO != "")
                output.OutListNO = applyOut.BillNO;

            if (applyOut.State == "2")
            {
                //如果是核准出库状态，则赋值
                output.Operation.ApproveOper.ID = applyOut.Operation.ApproveOper.ID; //核准人（用户录入的工号）
            }

            //如果是退库，则进行退库综合处理，否则进行出库综合处理。
            if (applyOut.SystemType.Substring(1) == "2")
            {
                //退库处理
                output.Quantity = -output.Quantity;	//退库数量为负数
                output.Operation.ExamQty = -output.Operation.ExamQty;		//退库数量为负数
                //{0B42E3DB-BDD9-46dd-95EF-D1424327587D}  参数调整
                if (this.OutputReturn(output, applyOut.OutBillNO, -1) == -1) return -1;
            }
            else
            {
                //出库处理
                if (this.Output(output) == -1) return -1;
            }
            int parm;

            //如果出库申请的时候预扣了库存，则在核准的时候消减预扣库存（加操作） 退库是不减预扣
            if (applyOut.SystemType.Substring(1) != "2" && applyOut.IsPreOut)
            {
                //{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                parm = this.UpdateStockinfoPreOutNum(applyOut, -applyOut.Operation.ApproveQty, applyOut.Days);
                if (parm != 1) return parm;
            }

            //返回出库单号，保存在出库申请实体中
            applyOut.OutBillNO = output.ID == null ? "0" : output.ID;
            return 1;
        }

        /// <summary>
        /// 出库－－对其他子系统公开的函数。
        /// 此方法试用于没有出库申请，直接出库时调用。
        /// 目前此方法中没有科隆新的出库实体，而是使用传入的出库实体，方法中会对此实体中的属性做修改。以后改进
        /// 参数Output中必须传入的项目：
        ///		output.Dept.ID,           ***出库科室编码
        ///		output.OutListNO,       ***出库单据号
        ///		output.SystemType,        ***出库分类
        ///		output.Item.ID,           ***药品编码
        ///		output.Item.Name,         ***药品商品名
        ///		output.Item.Type.ID,      ***药品类别
        ///		output.Item.Quality.ID,   ***药品性质
        ///		output.Item.Specs,        ***规格
        ///		output.Item.PackUnit,     ***包装单位
        ///		output.Item.PackQty,      ***包装数
        ///		output.Item.MinUnit,      ***最小单位
        ///		output.ShowState,         ***显示的单位标记（0最小单位，1包装单位）
        ///		output.RetailPrice,       ***零售价
        ///		output.Quantity,          ***出库量
        ///		output.ExamNum,           ***审批数量（扣库存的数量）
        ///		output.ExamOperCode,      ***审批人（扣库存操作的人）
        ///		output.ExamDate,          ***审批日期（扣库存的时间）
        ///		output.TargetDept.ID,     ***领药单位编码
        ///		output.RecipeNo,          ***处方号（药房发药出库时必须填写）
        ///		output.SequenceNo,        ***处方流水号（药房发药出库时必须填写）
        ///		output.OperDate,          ***操作时间
        ///		
        ///	内部处理流程：
        /// 1、取实际库存汇总表中此药品总数量
        /// 2、判断库存是否不足，退库允许没有库存或者不足
        /// 3、取本次出库药品的库存明细记录数组
        /// 4、循环。按照效期近、批次小先出库的原则进行出库处理。对于退库的药品，处理方式相同。
        ///    4.1当库存数量大于出库数量时，则将此批次库存记录出库，出库数量等于待出库数量。(本次循环结束不再找下一个批次)
        ///    4.2如果库存数量小于出库数量，则将此批次库存数量全部出库。（程序会继续查找下一个批次的库存信息）
        ///	   4.3剩余待摆药数量＝本次待摆药数量－本次摆药数量。如果剩余待摆药数量大于0，循环将继续进行。
        ///	   4.4库存数量减少，减少的量等于出库数量
        ///	   4.5插入出库记录
        ///	   4.6修改库存数据（通过库存明细表的触发器实现台帐表，库存汇总表处理）
        ///	   4.7如果出库的药品零售价跟库存中的药品零售价不同，则记录调价盈亏
        ///	循环当待出库数量小于等于0时结束。
        /// </summary>
        /// <returns>0没有删除1 成功 -1 失败 ErrCode 2 库存不足 </returns>
        public int Output(FS.HISFC.Models.Pharmacy.Output output)
        {
            return Output(output, null, false);
        }

        /// <summary>
        /// 处理出库信息 并根据标志处理入库记录
        /// </summary>
        /// <param name="output">出库实体</param>
        /// <param name="input">入库实体</param>
        /// <param name="isManagerInput">是否处理入库记录</param>
        /// <returns>1 成功 -1 失败 ErrCode 2 库存不足</returns>
        public int Output(FS.HISFC.Models.Pharmacy.Output output, FS.HISFC.Models.Pharmacy.Input input, bool isManagerInput)
        {
            //入库实体临时变量 用于处理入库记录
            FS.HISFC.Models.Pharmacy.Input inputTemp;

            #region 库存量是否足够判断

            //住院摆药性能优化【修改撤销，为了不影响住院摆药之外的出库库存判断】 by Sunjh 2010-8-30 {32F6FA1C-0B8E-4b9c-83B6-F9626397AC7C}

            //***批次>0表示退库或者按某一批次进行出库
            //出库数量为output.Quantity，更新库存变化数量为storageBase.Quantity
            //取实际库存汇总表中此药品总数量
            decimal storageNum = 0;
            if (output.BatchNO == "ALL")
            {
                output.BatchNO = null;
            }
            //by cube 2011-04-18 根据批号获取库存明细
            if (output.GroupNO > 0)
            {
                if (this.GetStorageNum(output.StockDept.ID, output.Item.ID, output.GroupNO, out storageNum) == -1)
                {
                    return -1;
                }
            }
            else if (this.GetStorageNum(output.StockDept.ID, output.Item.ID, output.BatchNO, out storageNum) == -1)
            {
                return -1;
            }
            //end by

            //判断库存是否不足，退库允许没有库存或者不足
            if ((Item.MinusStore == false) && (storageNum < output.Quantity) && (output.Quantity > 0))
            {
                this.Err = output.Item.Name + "的库存数量不足。请补充库存";
                this.ErrCode = "2";
                return -1;
            }

            #endregion

            //by cube 2011-04-07 根据批号获取库存明细
            //取本次出库药品的库存明细记录数组
            ArrayList al = new ArrayList();
            if (output.GroupNO > 0)
            {
                al = this.QueryStorageList(output.StockDept.ID, output.Item.ID, output.GroupNO);
            }
            else
            {
                al = this.QueryStorageList(output.StockDept.ID, output.Item.ID, output.BatchNO);
            }
            if (al == null)
            {
                return -1;
            }

            //库存明细和汇总可能会不一致，此处判断避免程序异常
            if (al.Count == 0)
            {
                this.Err = output.Item.Name + "的库存数量不足。请补充库存";
                return -1;
            }
            //end by

            //取出库单流水号保存在output中，可以被外面调用，一个药品一个出库流水号，可能对应多个批次
            output.ID = this.GetNewOutputNO();
            if (output.ID == null)
            {
                return -1;
            }

            //临时存储出库总数量和待出库数量
            FS.HISFC.Models.Pharmacy.StorageBase storageBase = new StorageBase();
            decimal totOutNum = output.Quantity;
            decimal leftOutNum = output.Quantity;

            //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}   增加对出库时间的赋值
            //by cube 2011-04-07 屏蔽 从界面赋值
            //DateTime sysDate = this.GetDateTimeFromSysDateTime();
            //end by

            //by cao-lin 一个药品出多个批时标记第一个来处理申请量
            int index = 0;
            //按照效期近、批次小先出库的原则进行出库处理。对于退库的药品，处理方式相同。
            for (int i = 0; leftOutNum > 0; i++)
            {
                #region 循环进行出库处理

                if (al.Count > 0)
                {
                    #region 库存明细中存在记录  如果库存明细记录大于零时，取库存中的数据

                    //取库存记录中的数据
                    storageBase = al[i] as StorageBase;
                    //对库存明细中为零的数据 不生成出库记录
                    if (storageBase.StoreQty == 0)
                    {
                        continue;
                    }

                    index++;

                    //在库存实体中保存相应的出库信息
                    storageBase.ID = output.ID;                     //出库单流水号
                    storageBase.SerialNO = output.SerialNO;         //出库单内序号
                    storageBase.SystemType = output.SystemType;     //系统出库类型

                    storageBase.PrivType = output.PrivType;
                    storageBase.Class2Type = output.Class2Type;

                    //原处理方式
                    //if (output.PrivType.IndexOf("|") == -1)
                    //    storageBase.PrivType = "0320|" + output.PrivType;    //出库类型

                    storageBase.TargetDept = output.TargetDept;     //领药部门

                    //将部门库存信息保存到出库记录中
                    output.GroupNO = storageBase.GroupNO;           //批次
                    output.BatchNO = storageBase.BatchNO;           //批号
                    output.Company = storageBase.Company;           //供货公司
                    output.PlaceNO = storageBase.PlaceNO;           //货位号
                    output.Producer = storageBase.Producer;         //生产厂家
                    output.ValidTime = storageBase.ValidTime;       //有效期

                    #endregion
                }

                //当库存数量大于出库数量时（或者库存中无数据，只要当允许为负库存时才能出现此中情况），则将此批次库存记录出库，出库数量等于待出库数量
                if (storageBase.StoreQty >= leftOutNum || al.Count == 0)
                {
                    //出库数量等于待出库数量（待出库数量会随着循环的增加而逐渐减少）
                    output.Quantity = leftOutNum;
                }
                else
                {
                    //如果库存数量小于出库数量，则将此批次库存数量全部出库。（程序会继续查找下一个批次的库存信息）
                    output.Quantity = storageBase.StoreQty;
                }

                //库存数量减少，减少的量等于出库数量（此处的storageBase.Quantity用来保存库存变化量）
                storageBase.Quantity = -output.Quantity;

                //剩余待摆药数量＝本次待摆药数量－本次摆药数量。如果剩余待摆药数量大于0，循环将继续进行。
                leftOutNum = leftOutNum - output.Quantity;

                //按批次出库时，如果同一样物品产生多条出库记录，单内序号增加
                //by cube 2011-04-07 单内序号
                output.SerialNO = index;
                //end by 

                //对于一条入库申请，如果出库记录多于一条，只有第一条出库记录中保存“申请数量",其余的出库记录中的申请数量为0，保证汇总数量正确
                if (index > 1)
                {
                    output.Operation.ApplyQty = 0;
                }

                //出库后库存量
                output.StoreQty = storageBase.StoreQty + storageBase.Quantity;
                //审核数量
                output.Operation.ExamQty = output.Quantity;


                //插入出库记录
                //取库存表里边的价格。购入价、零售价。
                //对于价让出库、发药消耗，取出库传入的价格；其他类型取库存内最新价格
                if (output.SystemType != FS.HISFC.Models.Base.EnumIMAOutTypeService.GetNameFromEnum(FS.HISFC.Models.Base.EnumIMAOutType.TransferOutput)
                    && output.SystemType != "M1"
                    && output.SystemType != "M2"
                    && output.SystemType != "Z1"
                    && output.SystemType != "Z2"
                    )
                {
                    output.Item.PriceCollection = storageBase.Item.PriceCollection;
                }
                else
                {
                    output.Item.PriceCollection.PurchasePrice = storageBase.Item.PriceCollection.PurchasePrice;
                    output.Item.PriceCollection.WholeSalePrice = storageBase.Item.PriceCollection.WholeSalePrice;

                    //记录原始零售价
                    output.DrugedBillNO = storageBase.Item.PriceCollection.RetailPrice.ToString();

                    if (this.OutputAdjust(output, storageBase, i, output.Operation.Oper.OperTime) == -1)
                    {
                        return -1;
                    }
                }
                //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出库时间赋值 记录出库记录发生时间
                //by cube 2011-04-07 屏蔽 从界面赋值
                //output.OutDate = sysDate;
                //end by

                //by cube 2011-04-20

                //购入金额
                output.PurchaseCost = output.Item.PriceCollection.PurchasePrice * (output.Quantity / output.Item.PackQty);
                output.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(output.PurchaseCost.ToString("F" + output.CostDecimals.ToString()));

                //零售金额
                output.RetailCost = output.Item.PriceCollection.RetailPrice * (output.Quantity / output.Item.PackQty);
                output.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(output.RetailCost.ToString("F" + output.CostDecimals.ToString()));

                //批发金额
                output.WholeSaleCost = output.Item.PriceCollection.WholeSalePrice * (output.Quantity / output.Item.PackQty);
                output.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(output.WholeSaleCost.ToString("F" + output.CostDecimals.ToString()));

                //end by

                #region 处理对应领用部门的入库数据（"特殊出库"不处理领用单位的入库，台帐，库存）

                //特殊出库、生产出库不处理
                if (output.SystemType != "26" && output.SystemType != "31")
                {
                    //判断是否需要处理入库记录 在不管理库存的情况下才处理入库记录 自动插入入库记录
                    if (isManagerInput && input != null)
                    {	//插入领用部门入库记录
                        inputTemp = new Input();
                        inputTemp = input.Clone();
                        inputTemp.OutBillNO = output.ID;				//出库流水号
                        inputTemp.Item = output.Item;					//药品实体
                        inputTemp.Quantity = output.Quantity;			//数量
                        inputTemp.GroupNO = output.GroupNO;				//批次
                        inputTemp.BatchNO = output.BatchNO;				//批号
                        inputTemp.Company = output.StockDept;				//供货单位
                        inputTemp.PlaceNO = output.PlaceNO;			//货位号
                        inputTemp.Producer = output.Producer;			//生产厂家
                        inputTemp.ValidTime = output.ValidTime;			//有效期

                        //by cube 2011-04-07 单内序号
                        inputTemp.OutSerialNO = output.SerialNO;
                        inputTemp.OutListNO = output.OutListNO;
                        inputTemp.SerialNO = output.SerialNO;
                        //end by 

                        //by cube 2011-04-07 金额计算，保持和出库一致
                        inputTemp.PurchaseCost = output.PurchaseCost;
                        inputTemp.WholeSaleCost = output.WholeSaleCost;
                        inputTemp.RetailCost = output.RetailCost;
                        //end

                        if (this.Input(inputTemp, "1", "1") == -1)
                        {
                            return -1;
                        }

                        output.InBillNO = inputTemp.ID;
                        output.InSerialNO = inputTemp.SerialNO;
                        output.InListNO = inputTemp.InListNO;

                        //by cube 2011-04-07 先处理入库数据，然后一次性插入出库数据即可
                        //if (this.UpdateOutput(output) == -1)
                        //{
                        //    this.Err = "入库记录生成后，更新出库记录执行出错！" + this.Err;
                        //    return -1;
                        //}
                        //end by
                    }
                }

                #endregion

                #region 插入出库记录 更新库存

                if (this.InsertOutput(output) != 1)
                {
                    this.Err = "插入出库记录时出错！" + this.Err;
                    return -1;
                }

                //修改库存数据（通过库存明细表的触发器实现台帐表，库存汇总表处理）
                //先执行更新数量操作，如果数据库中没有记录则执行插入操作
                storageBase.Class2Type = "0320";
                if (this.SetStorage(storageBase) != 1)
                {
                    this.Err = "更新库存表时出错！" + this.Err;
                    return -1;
                }
                #endregion


                #endregion
            }

            //恢复output实体中传入时的数值
            output.Quantity = totOutNum;

            return 1;
        }

        /// <summary>
        /// 出库－－对其他子系统公开的函数。
        /// 此方法试用于没有出库申请，直接出库时调用。
        /// 目前此方法中没有科隆新的出库实体，而是使用传入的出库实体，方法中会对此实体中的属性做修改。以后改进
        ///		
        ///	内部处理流程：
        /// 1、取实际库存汇总表中此药品总数量
        /// 2、判断库存是否不足，退库允许没有库存或者不足
        /// 3、取本次出库药品的库存明细记录数组
        /// 4、循环。按照效期近、批次小先出库的原则进行出库处理。对于退库的药品，处理方式相同。
        ///    4.1当库存数量大于出库数量时，则将此批次库存记录出库，出库数量等于待出库数量。(本次循环结束不再找下一个批次)
        ///    4.2如果库存数量小于出库数量，则将此批次库存数量全部出库。（程序会继续查找下一个批次的库存信息）
        ///	   4.3剩余待摆药数量＝本次待摆药数量－本次摆药数量。如果剩余待摆药数量大于0，循环将继续进行。
        ///	   4.4库存数量减少，减少的量等于出库数量
        ///	   4.5插入出库记录
        ///	   4.6修改库存数据（通过库存明细表的触发器实现台帐表，库存汇总表处理）
        ///	   4.7如果出库的药品零售价跟库存中的药品零售价不同，则记录调价盈亏
        ///	循环当待出库数量小于等于0时结束。
        /// </summary>
        /// <returns>0没有删除 1成功 -1失败</returns>
        public int OutputReturn(FS.HISFC.Models.Pharmacy.Output output, string outputID, int serialNO)
        {
            return this.OutputReturn(output, outputID, serialNO, false);
        }

        /// <summary>
        /// 实现出库库退库
        /// </summary>
        /// <param name="output">待出库实体</param>
        /// <param name="outputID">出库流水号</param>
        /// <param name="isManagerInput">是否处理入库记录</param>
        /// <returns>0 没有删除 1 成功 －1 失败</returns>
        public int OutputReturn(FS.HISFC.Models.Pharmacy.Output output, string outputID, int serialNO, bool isManagerInput)
        {
            FS.HISFC.Models.Pharmacy.Input inputInfo;
            FS.HISFC.Models.Pharmacy.Input inputTemp;

            //取出库单流水号保存在output中，可以被外面调用
            output.ID = this.GetNewOutputNO();
            if (output.ID == null) return -1;

            //临时存储出库总数量和待出库数量
            FS.HISFC.Models.Pharmacy.StorageBase storageBase;
            decimal totOutNum = output.Quantity;
            decimal leftOutNum = output.Quantity;

            #region 根据出库退库记录中的出库单流水号，取出库数据列表。

            //{0B42E3DB-BDD9-46dd-95EF-D1424327587D}  此段改动为了保证 出库退库时可按所选择的批号退库
            ArrayList al = new ArrayList();

            ArrayList alOriginal = this.QueryOutputList(outputID);
            if (alOriginal == null)
            {
                return -1;
            }
            //如果al超出索引，则提示出错
            if (alOriginal.Count == 0)
            {
                this.Err = "没有找到退库操作所对应的出库记录！";
                return -1;
            }
            if (serialNO != -1)
            {
                foreach (FS.HISFC.Models.Pharmacy.Output outputSerial in alOriginal)
                {
                    if (outputSerial.SerialNO == serialNO)
                    {
                        al.Add(outputSerial);
                    }
                }
            }
            else
            {
                al = alOriginal;
            }
            //{0B42E3DB-BDD9-46dd-95EF-D1424327587D}  
            #endregion

            FS.HISFC.Models.Pharmacy.Output info;
            //如果退库申请中，指定确定的批次，则将此批次记录退掉。
            //否则，在与出库申请对应的出库记录中按批次小先退的原则，做退库处理。

            DateTime sysTime = this.GetDateTimeFromSysDateTime();

            string inListCode = "";

            for (int i = 0; leftOutNum < 0; i++)
            {
                if (al.Count == i)
                {
                    this.Err = "该条出库记录的出库数量不足以进行此次退库 请重新选择退库记录";
                    return -1;
                }
                //取出库记录中的数据  
                info = al[i] as Output;
                //出库数量＝退库数量 且为最后出库记录
                if (info.Quantity == info.Operation.ReturnQty)
                {
                    continue;
                }

                #region 根据原出库记录 生成退库记录

                //将出库时的信息保存到退库记录中
                output.GroupNO = info.GroupNO;					//批次
                output.BatchNO = info.BatchNO;					//批号
                output.Company = info.Company;					//供货公司
                output.PlaceNO = info.PlaceNO;					//货位号
                output.Producer = info.Producer;					//生产厂家
                output.ValidTime = info.ValidTime;					//有效期

                //2011-05-27 by cube 出库退库取原来的价格，包括购入、批发、零售
                //output.Item.PriceCollection.RetailPrice = info.Item.PriceCollection.RetailPrice;	//零售价 利用原出库价格退库
                output.Item.PriceCollection = info.Item.PriceCollection;	//零售价 利用原出库价格退库
                //end by

                //当某一批次的可退数量（已出库数量－已退库数量）大于待退库数量时，则将此批次出库记录退库，退库数量等于待退库数量。(本次循环结束不再找下一个批次)
                if (info.Quantity - info.Operation.ReturnQty >= Math.Abs(leftOutNum))
                {
                    //退库数量等于待退库数量（待退库数量会随着循环的增加而逐渐减少）
                    //退库数量是负数
                    output.Quantity = leftOutNum;
                }
                else
                {
                    //如果可退数量（已出库数量－已退库数量）小于于待退库数量，则将此批次出库记录中的可退库数量全部退库。（程序会继续查找下一个批次的库存信息）
                    //退库数量是负数
                    output.Quantity = -(info.Quantity - info.Operation.ReturnQty);
                }

                //剩余待退库数量（负数）＝本次待退药数量（负数）－本次退药数量（负数）。如果剩余待退药数量小于0，则循环将继续进行。
                leftOutNum = leftOutNum - output.Quantity;

                //单内序号增加
                output.SerialNO = i + 1;

                //对于一条入库申请，如果出库记录多于一条，只有第一条出库记录中保存“申请数量",其余的出库记录中的申请数量为0，保证汇总数量正确
                if (i > 0) output.Operation.ApplyQty = 0;

                //插入出库记录
                output.State = "2";					//不需核准操作

                #endregion

                //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出/退库记录发生时间
                //药库退库，根据出库单退库，时间已经
                //output.OutDate = sysTime;

                //by cube 2011-04-19
                //购入金额
                output.PurchaseCost = output.Item.PriceCollection.PurchasePrice * (output.Quantity / output.Item.PackQty);
                output.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(output.PurchaseCost.ToString("F" + output.CostDecimals.ToString()));

                //零售金额
                output.RetailCost = output.Item.PriceCollection.RetailPrice * (output.Quantity / output.Item.PackQty);
                output.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(output.RetailCost.ToString("F" + output.CostDecimals.ToString()));

                //批发金额
                output.WholeSaleCost = output.Item.PriceCollection.WholeSalePrice * (output.Quantity / output.Item.PackQty);
                output.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(output.WholeSaleCost.ToString("F" + output.CostDecimals.ToString()));
                //end

                if (this.InsertOutput(output) != 1)
                {
                    this.Err = "插入出库记录时出错！" + this.Err;
                    return -1;
                }

                //更新出库记录中的"已退库数量"字段（加操作）
                output.Quantity = -output.Quantity;
                if (this.UpdateOutputReturnNum(outputID, info.SerialNO, output.Quantity) != 1)
                {
                    this.Err = "更新出库记录中的已退库数量时出错！" + this.Err;
                    return -1;
                }

                //{7788EE66-74E7-4b9d-B4DA-EFE14DBFAD0E}  说明是对审批记录的退库 更新该审批记录为已核准状态
                //by cube 2011-05-19  && Math.Abs(info.Quantity) - output.Quantity <= 0
                if (output.SystemType == "22" && info.State == "1" && Math.Abs(info.Quantity) - output.Quantity <= 0)
                {
                    info.Operation.ApproveOper = output.Operation.ApproveOper;
                    if (this.ApproveOutput(info) == -1)
                    {
                        this.Err = "针对审批记录退库 更新原审批记录为核准状态出错！" + this.Err;
                        return -1;
                    }
                }

                //将出库数据赋值给库存数据，退库数量output.Quantity即是库存变化量（库存更新数量时是减操作）storageBase.Quantity（负数）
                storageBase = output.Clone() as StorageBase;

                storageBase.Class2Type = output.Class2Type;
                storageBase.PrivType = output.PrivType;

                //原实现方式
                //if (output.PrivType.IndexOf("|") == -1)
                //    storageBase.PrivType = "0320|" + output.PrivType;

                //修改库存数据（通过库存明细表的触发器实现台帐表，库存汇总表处理）
                //库存变化的数量（库存修改执行的加操作）跟出库的数量相反。
                //先执行更新数量操作，如果数据库中没有记录则执行插入操作
                if (this.SetStorage(storageBase) != 1)
                {
                    this.Err = "更新库存表时出错！" + this.Err;
                    return -1;
                }

                #region 如果出库的药品零售价跟库存中的药品零售价不同，则记录调价盈亏

                #region 屏蔽该段代码 通过函数调用实现

                //string adjustPriceID = "";
                //bool isDoAdjust = false;
                //decimal dNowPrice = 0;
                //dNowPrice = output.Item.PriceCollection.RetailPrice;

                //if (info.Item.PriceCollection.RetailPrice != dNowPrice)
                //{
                //    if (!isDoAdjust)
                //    {
                //        adjustPriceID = this.GetSequence("Pharmacy.Item.GetNewAdjustPriceID");
                //        if (adjustPriceID == null)
                //        {
                //            this.Err = "出库退库药品已发生调价 插入调价盈亏记录过程中获取调价单号出错！";
                //            return -1;
                //        }
                //    }
                //    FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = new AdjustPrice();
                //    adjustPrice.ID = adjustPriceID;								//调价单号
                //    adjustPrice.SerialNO = i;									//调价单内序号
                //    adjustPrice.Item = info.Item;
                //    adjustPrice.StockDept.ID = info.StockDept.ID;				//调价科室 
                //    adjustPrice.State = "1";									//调价状态 1 已调价
                //    adjustPrice.StoreQty = output.Quantity;
                //    adjustPrice.Operation.Oper.ID = this.Operator.ID;
                //    adjustPrice.Operation.Oper.Name = this.Operator.Name;
                //    adjustPrice.Operation.Oper.OperTime = sysTime;
                //    adjustPrice.InureTime = sysTime;
                //    adjustPrice.AfterRetailPrice = dNowPrice;//调价后零售价
                //    if (dNowPrice - info.Item.PriceCollection.RetailPrice > 0)
                //        adjustPrice.ProfitFlag = "1";							//调盈
                //    else
                //        adjustPrice.ProfitFlag = "0";							//调亏
                //    adjustPrice.Memo = "出库退库补调价盈亏";
                //    if (!isDoAdjust)			//每次只插入一次调价汇总表
                //    {
                //        if (this.InsertAdjustPriceInfo(adjustPrice) == -1)
                //        {
                //            return -1;
                //        }
                //        isDoAdjust = true;
                //    }
                //    if (this.InsertAdjustPriceDetail(adjustPrice) == -1)
                //    {
                //        return -1;
                //    }
                //}

                #endregion

                //2011-05-27 by cube
                //if (this.OutputAdjust(info, output, sysTime, i) == -1)
                //{
                //    return -1;
                //}
                if (this.OutputAdjust(output, null, i, sysTime) == -1)
                {
                    return -1;
                }
                //end by

                #endregion

                #region 处理对应领用部门的入库数据（"特殊出库"不处理领用单位的入库，台帐，库存）

                //插入领用部门入库记录
                //判断是否需要对领用部门进行库存管理。如果管理库存，则进行下面处理：
                if (isManagerInput)
                {
                    inputInfo = this.GetInputInfoByID(info.InBillNO);
                    if (inputInfo == null)
                    {
                        return -1;
                    }

                    inputTemp = inputInfo.Clone();

                    inputTemp.ID = "";							//流水号
                    inputTemp.Quantity = -output.Quantity;		//数量
                    inputTemp.GroupNO = output.GroupNO;			//批次
                    inputTemp.BatchNO = output.BatchNO;			//批号
                    inputTemp.Company = output.StockDept;		//供货公司
                    inputTemp.PlaceNO = output.PlaceNO;		    //货位号
                    inputTemp.Producer = output.Producer;		//生产厂家
                    inputTemp.ValidTime = output.ValidTime;		//有效期
                    inputTemp.Operation.ReturnQty = 0;

                    inputTemp.InListNO = output.OutListNO;
                    inputTemp.OutBillNO = output.ID;          //出库单据号

                    //by cube 2011-04-07 记录入库时间
                    inputTemp.InDate = output.Operation.Oper.OperTime;
                    //end by

                    //直接退库是2
                    inputTemp.State = "2";

                    inputTemp.Operation.Oper.ID = output.Operation.Oper.ID;              //操作信息
                    inputTemp.Operation.Oper.OperTime = output.Operation.Oper.OperTime;              //操作信息
                    inputTemp.Operation.ApplyOper.ID = output.Operation.Oper.ID;
                    inputTemp.Operation.ApplyOper.OperTime = output.Operation.Oper.OperTime;
                    inputTemp.Operation.ExamOper.ID = output.Operation.Oper.ID;  //审核人
                    inputTemp.Operation.ExamOper.OperTime = output.Operation.Oper.OperTime;                   //审核日期
                    inputTemp.Operation.ApproveOper.OperTime = output.Operation.Oper.OperTime;
                    inputTemp.Operation.ApproveOper.ID = output.Operation.Oper.ID;

                    inputTemp.PrivType = "06";               //入库类型
                    inputTemp.SystemType = "19";           //系统类型

                    //by cube 2011-04-20 计算金额,保证和出库一致
                    inputTemp.RetailCost = output.RetailCost;
                    inputTemp.WholeSaleCost = output.WholeSaleCost;
                    inputTemp.PurchaseCost = output.PurchaseCost;
                    //end by

                    //插入入库负记录
                    if (this.Input(inputTemp, "1", "1") == -1)
                    {
                        this.Err = this.Err + "插入入库负记录出错！";
                        return -1;
                    }
                    //更新原入库记录退库数量
                    if (this.UpdateInputReturnNum(inputInfo.ID, inputInfo.SerialNO, -inputTemp.Quantity) != 1)
                    {
                        this.Err = this.Err + "更新入库记录退库数量出错！";
                        return -1;
                    }
                }

                #endregion
            }

            //恢复output实体中传入时的数值
            output.Quantity = totOutNum;
            return 1;
        }

        /// <summary>
        /// 出库补调价盈亏
        /// 2011-05-27 by cube
        /// </summary>
        /// <param name="output">出库实体</param>
        /// <param name="storage">库存明细实体</param>
        /// <param name="serialNo">单内序号</param>
        /// <param name="sysTime">系统时间</param>
        /// <returns></returns>
        public int OutputAdjust(Output output, StorageBase storage, int serialNo, DateTime sysTime)
        {
            string adjustPriceID = "";
            bool isDoAdjust = false;
            decimal afterRetailPrice = 0;
            decimal beforeRetailPrice = 0;

            if (storage == null)
            {
                storage = new StorageBase();
                decimal price = 0;
                if (GetNowPrice(output.StockDept.ID, output.Item.ID, ref price) == -1)
                {
                    this.Err = "补调价盈亏发生错误：获取药品零售价出错！";
                    return -1;
                }
                storage.Item.PriceCollection.RetailPrice = price;
            }
            if (output.SystemType == "22" || output.SystemType == "Z2" || output.SystemType == "M2")
            {
                //退库
                //调价前的价格是出库价，调价后的价格是库存价格
                //相当于将一部分药品入库后再调整这一部分药品价格到目前库房里的价格
                beforeRetailPrice = output.Item.PriceCollection.RetailPrice;
                afterRetailPrice = storage.Item.PriceCollection.RetailPrice;

            }
            else
            {
                //价让出库，一般是亏损：调价前的价格比调价后的价格大
                //患者用药有可能在收完费发药前调价，这样也需要补充调价盈亏
                //调价前的价格是库存价，调价后的价格是实际出库价
                //相当于是将一部分药品调价后出库
                beforeRetailPrice = storage.Item.PriceCollection.RetailPrice;
                afterRetailPrice = output.Item.PriceCollection.RetailPrice;
            }
            if (beforeRetailPrice != afterRetailPrice)
            {
                if (!isDoAdjust)
                {
                    adjustPriceID = this.GetSequence("Pharmacy.Item.GetNewAdjustPriceID");
                    if (adjustPriceID == null)
                    {
                        this.Err = "补调价盈亏发生错误：插入调价盈亏记录过程中获取调价单号出错！";
                        return -1;
                    }
                }
                FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = new  AdjustPrice();
                adjustPrice.ID = adjustPriceID;								//调价单号
                adjustPrice.SerialNO = serialNo;									//调价单内序号

                adjustPrice.Item = output.Item.Clone();
                //调价前的价格在此赋值
                adjustPrice.Item.PriceCollection.RetailPrice = beforeRetailPrice;

                adjustPrice.StockDept.ID = output.StockDept.ID;				//调价科室 
                adjustPrice.State = "1";									//调价状态 1 已调价
                adjustPrice.StoreQty = output.Quantity;
                adjustPrice.Operation.Oper.ID = this.Operator.ID;
                adjustPrice.Operation.Oper.Name = this.Operator.Name;
                adjustPrice.Operation.Oper.OperTime = sysTime;
                adjustPrice.InureTime = sysTime;
                adjustPrice.AfterRetailPrice = afterRetailPrice;//调价后零售价

                if (beforeRetailPrice < adjustPrice.AfterRetailPrice)
                {
                    adjustPrice.ProfitFlag = "1";							//调盈
                }
                else
                {
                    adjustPrice.ProfitFlag = "0";							//调亏
                }
                if (output.SystemType == "29")
                {
                    adjustPrice.Memo = "价让出库补调价盈亏";
                }
                else if (output.SystemType == "22")
                {
                    adjustPrice.Memo = "出库退库补调价盈亏";
                }
                else if (output.SystemType == "Z2")
                {
                    adjustPrice.Memo = "住院退药补调价盈亏";
                }
                else if (output.SystemType == "M2")
                {
                    adjustPrice.Memo = "门诊退药补调价盈亏";
                }
                else if (output.SystemType == "Z1")
                {
                    adjustPrice.Memo = "住院发药补调价盈亏";
                }
                else if (output.SystemType == "M1")
                {
                    adjustPrice.Memo = "门诊发药补调价盈亏";
                }
                else
                {
                    adjustPrice.Memo = "出库补调价盈亏";
                }
                if (!isDoAdjust)			//每次只插入一次调价汇总表
                {
                    if (this.InsertAdjustPriceInfo(adjustPrice) == -1)
                    {
                        return -1;
                    }
                    isDoAdjust = true;
                }
                if (this.InsertAdjustPriceDetail(adjustPrice) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 出库退库时 对价格发生变化时 更新调价记录
        /// </summary>
        /// <returns></returns>
        public int OutputAdjust(FS.HISFC.Models.Pharmacy.Output privOutput, FS.HISFC.Models.Pharmacy.Output nowOutput, DateTime sysTime, int serialNo)
        {
            string adjustPriceID = "";
            bool isDoAdjust = false;
            decimal dNowPrice = 0;
            dNowPrice = nowOutput.Item.PriceCollection.RetailPrice;
            if (this.GetNowPrice(nowOutput.Item.ID, ref dNowPrice) == -1)
            {
                this.Err = "出库退库处理调价盈亏时 获取最新药品零售价失败";
                return -1;
            }

            if (privOutput.Item.PriceCollection.RetailPrice != dNowPrice)
            {
                if (!isDoAdjust)
                {
                    adjustPriceID = this.GetSequence("Pharmacy.Item.GetNewAdjustPriceID");
                    if (adjustPriceID == null)
                    {
                        this.Err = "出库退库药品已发生调价 插入调价盈亏记录过程中获取调价单号出错！";
                        return -1;
                    }
                }
                FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = new  AdjustPrice();
                adjustPrice.ID = adjustPriceID;								//调价单号
                adjustPrice.SerialNO = serialNo;									//调价单内序号
                adjustPrice.Item = privOutput.Item;
                adjustPrice.StockDept.ID = privOutput.StockDept.ID;				//调价科室 
                adjustPrice.State = "1";									//调价状态 1 已调价
                adjustPrice.StoreQty = nowOutput.Quantity;
                adjustPrice.Operation.Oper.ID = this.Operator.ID;
                adjustPrice.Operation.Oper.Name = this.Operator.Name;
                adjustPrice.Operation.Oper.OperTime = sysTime;
                adjustPrice.InureTime = sysTime;
                adjustPrice.AfterRetailPrice = dNowPrice;//调价后零售价
                if (dNowPrice - privOutput.Item.PriceCollection.RetailPrice > 0)
                    adjustPrice.ProfitFlag = "1";							//调盈
                else
                    adjustPrice.ProfitFlag = "0";							//调亏
                adjustPrice.Memo = "出库退库补调价盈亏";
                if (!isDoAdjust)			//每次只插入一次调价汇总表
                {
                    if (this.InsertAdjustPriceInfo(adjustPrice) == -1)
                    {
                        return -1;
                    }
                    isDoAdjust = true;
                }
                if (this.InsertAdjustPriceDetail(adjustPrice) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        #endregion

        #region 药柜出、退库

        /// <summary>
        /// 药柜出库摆药
        /// </summary>
        /// <param name="applyOut">摆药申请信息</param>
        /// <param name="arkDept">摆药药柜信息</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int ArkOutput(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, FS.FrameWork.Models.NeuObject arkDept)
        {
            //将出库申请数据转为出库数据。								 
            FS.HISFC.Models.Pharmacy.Output output = new Output();

            #region 出库数据赋值

            output.StockDept = applyOut.Operation.ApproveOper.Dept;             //出库科室＝摆药核准科室
            output.SystemType = applyOut.SystemType;                            //系统类型＝出库申请类型
            output.PrivType = applyOut.PrivType;
            output.Item = applyOut.Item;                                        //药品实体
            output.ShowState = applyOut.ShowState;                              //显示的单位标记（0最小单位，1包装单位）
            output.Quantity = applyOut.Operation.ApproveQty * applyOut.Days;    //出库数量＝摆药核准数量
            output.State = applyOut.State;                                      //出库状态＝摆药状态
            output.GetPerson = applyOut.PatientNO;                              //取 药 人＝患者ID
            output.DrugedBillNO = applyOut.DrugNO;                              //摆药单号
            output.SpecialFlag = "0";                                           //特殊标记。1是，0否
            output.TargetDept = applyOut.ApplyDept;                             //领用科室＝出库申请科室
            output.RecipeNO = applyOut.RecipeNO;                                //处方号
            output.SequenceNO = applyOut.SequenceNO;                            //处方内流水号
            output.Operation.ApplyQty = applyOut.Operation.ApplyQty * applyOut.Days;     //出库申请数量
            output.Operation.ApplyOper.ID = applyOut.Operation.ApplyOper.ID;             //出库申请人编码
            output.Operation.ApplyOper.OperTime = applyOut.Operation.ApplyOper.OperTime; //出库申请日期
            output.Operation.ExamQty = applyOut.Operation.ApproveQty * applyOut.Days;    //审批出库数量＝摆药核准数量
            output.Operation.ExamOper.ID = applyOut.Operation.ExamOper.ID;               //审批人 ＝打印人
            output.Operation.ExamOper.OperTime = applyOut.Operation.ExamOper.OperTime;   //审批日期＝打印日期
            output.State = "2";

            #endregion

            if (applyOut.State == "2")
            {
                //如果是核准出库状态，则赋值
                output.Operation.ApproveOper.ID = applyOut.Operation.ApproveOper.ID; //核准人（用户录入的工号）
            }

            //如果是退库，则进行退库综合处理，否则进行出库综合处理。
            if (applyOut.SystemType.Substring(1) == "2")
            {
                //退库处理
                output.Quantity = -output.Quantity;	//退库数量为负数
                output.Operation.ExamQty = -output.Operation.ExamQty;		//退库数量为负数
                //药房退库处理
                if (this.ArkOutputReturn(output, applyOut.OutBillNO, false) == -1)
                {
                    return -1;
                }
            }
            else
            {
                FS.FrameWork.Models.NeuObject stockDept = output.StockDept.Clone();

                output.StockDept = arkDept;
                //对药柜 出库处理
                if (this.ArkOutput(output.Clone(), true, false, true, false) == -1)
                {
                    return -1;
                }

                //对药房 出库处理
                output.ArkOutNO = output.ID;                //药柜出库记录流水号
                output.ID = "";                             //出库记录流水号清空
                output.StockDept = stockDept;

                if (this.ArkOutput(output, false, false, true, true) == -1)
                {
                    return -1;
                }

            }
            int parm;

            #region 库存预扣处理 如果出库申请的时候预扣了库存，则在核准的时候消减预扣库存（加操作）

            if (applyOut.IsPreOut)
            {
                //{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                parm = this.UpdateStockinfoPreOutNum(applyOut, -applyOut.Operation.ApproveQty, applyOut.Days);
                if (parm != 1)
                {
                    return parm;
                }
            }

            //返回出库单号，保存在出库申请实体中
            applyOut.OutBillNO = output.ID == null ? "0" : output.ID;

            #endregion
            return 1;
        }

        /// <summary>
        /// 药柜管理 处理出库信息 并根据标志处理入库记录
        /// </summary>
        /// <说明>
        ///     1、如果入库科室是药柜 出库科室是药房 则 不处理药房的出库记录
        ///     2、如果入库科室是药柜 出库科室是药柜 则 入库/出库记录都处理
        /// </说明>
        /// <param name="output">出库实体</param>  
        /// <param name="isChestOut">是否药柜出库</param>
        /// <param name="isChestIn">接受科室是否药柜</param>
        /// <param name="isPatientOut">是否患者出库</param>
        /// <param name="isUpdateArkQty">是否更新药柜数量</param>
        /// <returns>1 成功 -1 失败 ErrCode 2 库存不足</returns>
        public int ArkOutput(FS.HISFC.Models.Pharmacy.Output output, bool isChestOut, bool isChestIn, bool isPatientOut, bool isUpdateArkQty)
        {
            #region 科室库存量判断

            //取实际库存汇总表中此药品总数量  对于药柜来说 ArkQty为0 所以对判断没影响           
            FS.HISFC.Models.Pharmacy.Storage storage = this.GetStockInfoByDrugCode(output.StockDept.ID, output.Item.ID);
            //药房向药柜出库时 进行此类判断
            if (!isChestOut && isChestIn)
            {
                if (output.Quantity > (storage.StoreQty - storage.ArkQty))
                {
                    this.Err = output.Item.Name + "的库存数量不足。请补充库存";
                    this.ErrCode = "2";
                    return -1;
                }
            }
            else
            {
                if (output.Quantity > storage.StoreQty)
                {
                    this.Err = output.Item.Name + "的库存数量不足。请补充库存";
                    this.ErrCode = "2";
                    return -1;
                }
            }
            #endregion

            //取本次出库药品的库存明细记录数组（批次＝0，则取本批次库存明细）
            ArrayList al = this.QueryStorageList(output.StockDept.ID, output.Item.ID, output.BatchNO);
            if (al == null)
            {
                return -1;
            }

            //取出库单流水号保存在output中，可以被外面调用，一个药品一个出库流水号，可能对应多个批次
            output.ID = this.GetNewOutputNO();
            if (output.ID == null)
            {
                return -1;
            }

            //临时存储出库总数量和待出库数量
            FS.HISFC.Models.Pharmacy.StorageBase storageBase = null;
            decimal totOutNum = output.Quantity;
            decimal leftOutNum = output.Quantity;

            //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出/退库记录发生时间
            DateTime sysTime = this.GetDateTimeFromSysDateTime();

            //按照效期近、批次小先出库的原则进行出库处理
            for (int i = 0; leftOutNum > 0; i++)
            {
                if (al.Count > 0)
                {
                    #region 库存明细中存在记录  如果库存明细记录大于零时，取库存中的数据

                    //取库存记录中的数据
                    storageBase = al[i] as StorageBase;
                    //对库存明细中为零的数据 不生成出库记录  {45938EF6-62DE-4df5-85C2-7D07FA0C1166}
                    if (isPatientOut == false && isChestOut == false && isChestIn == true)               //药库出库且接受科室为药柜，此时判断库存时需考虑药柜量
                    {
                        if (storageBase.StoreQty - storageBase.ArkQty <= 0)
                        {
                            continue;
                        }
                    }
                    else                                                       //药库/药柜发药时 不需要判断药柜；药房给非药柜科室出库时  不判断
                    {
                        if (storageBase.StoreQty <= 0)
                        {
                            continue;
                        }
                    }


                    //在库存实体中保存相应的出库信息
                    storageBase.ID = output.ID;                     //出库单流水号
                    storageBase.SerialNO = output.SerialNO;         //出库单内序号
                    storageBase.SystemType = output.SystemType;     //系统出库类型

                    storageBase.PrivType = output.PrivType;
                    storageBase.Class2Type = output.Class2Type;

                    storageBase.TargetDept = output.TargetDept;     //领药部门

                    //将部门库存信息保存到出库记录中
                    output.GroupNO = storageBase.GroupNO;           //批次
                    output.BatchNO = storageBase.BatchNO;           //批号
                    output.Company = storageBase.Company;           //供货公司
                    output.PlaceNO = storageBase.PlaceNO;           //货位号
                    output.Producer = storageBase.Producer;         //生产厂家
                    output.ValidTime = storageBase.ValidTime;       //有效期

                    #endregion
                }

                #region 变量赋值处理

                //当库存数量大于出库数量时（或者库存中无数据，只要当允许为负库存时才能出现此中情况），则将此批次库存记录出库，出库数量等于待出库数量
                if ((storageBase.StoreQty - storageBase.ArkQty) >= leftOutNum || al.Count == 0)
                {
                    //出库数量等于待出库数量（待出库数量会随着循环的增加而逐渐减少）
                    output.Quantity = leftOutNum;
                }
                else
                {
                    //如果库存数量小于出库数量，则将此批次库存数量全部出库。（程序会继续查找下一个批次的库存信息）
                    output.Quantity = storageBase.StoreQty - storageBase.ArkQty;
                }
                //药柜管理数量
                if (!isChestOut)            //非药柜出库 修改药柜管理库存汇总数量
                {
                    storageBase.ArkQty = output.Quantity;
                }
                else                        //药柜出库  药柜间正常调拨
                {
                    storageBase.ArkQty = 0;
                }

                //库存数量减少，减少的量等于出库数量（此处的storageBase.Quantity用来保存库存变化量）
                storageBase.Quantity = -output.Quantity;

                //剩余待摆药数量＝本次待摆药数量－本次摆药数量。如果剩余待摆药数量大于0，循环将继续进行。
                leftOutNum = leftOutNum - output.Quantity;

                //按批次出库时，如果同一样物品产生多条出库记录，单内序号增加
                output.SerialNO = i + 1;

                //对于一条入库申请，如果出库记录多于一条，只有第一条出库记录中保存“申请数量",其余的出库记录中的申请数量为0，保证汇总数量正确
                if (i > 0)
                {
                    output.Operation.ApplyQty = 0;
                }

                #endregion

                //药房向药柜出库时 置出库记录标记 
                if (isChestIn && !isChestOut)
                {
                    output.IsArkManager = true;
                }
                else
                {
                    output.IsArkManager = false;
                }
                //插入出库记录

                output.Item.PriceCollection = storageBase.Item.PriceCollection;

                //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出/退库记录发生时间
                output.OutDate = sysTime;

                if (this.InsertOutput(output) != 1)
                {
                    this.Err = "插入出库记录时出错！" + this.Err;
                    return -1;
                }
                //库存更新 对非药房向药柜出库情况下 更新库存
                if (!output.IsArkManager)
                {
                    //修改库存数据（通过库存明细表的触发器实现台帐表，库存汇总表处理）
                    //先执行更新数量操作，如果数据库中没有记录则执行插入操作
                    if (this.SetStorage(storageBase) != 1)
                    {
                        this.Err = "更新库存表时出错！" + this.Err;
                        return -1;
                    }
                }
                //更新药房内药柜库存汇总量 ArkQty数量
                if (output.IsArkManager || isUpdateArkQty)
                {
                    if (!output.IsArkManager)
                    {
                        storageBase.ArkQty = -storageBase.ArkQty;
                    }
                    //更新药柜 ArkQty数量  库存数量不变
                    if (this.SetArkStorage(storageBase) != 1)
                    {
                        this.Err = "更新库存表时出错！" + this.Err;
                        return -1;
                    }
                }
            }

            //恢复output实体中传入时的数值
            output.Quantity = totOutNum;

            return 1;
        }

        /// <summary>
        /// 实现出库库退库
        /// </summary>
        /// <说明>
        ///     药柜管理退库流程处理
        ///     1、如果 入出均非药柜 则按原流程处理
        ///     2、如果药柜退库 那么目标接受科室一定为药房 直接按原流程处理
        ///     3、如果药房退库 目标接受科室为药柜 则 生成药房负出库记录 药柜出库记录标志为True
        ///         更新药房药柜库存汇总量  生成药柜负入库记录 处理药柜库存
        /// </说明>
        /// <param name="output">待出库实体</param>
        /// <param name="outputID">出库流水号</param>
        /// <param name="isManagerInput">是否处理入库记录</param>
        /// <returns>0 没有删除 1 成功 －1 失败</returns>
        public int ArkOutputReturn(FS.HISFC.Models.Pharmacy.Output output, string outputID, bool isManagerInput)
        {
            #region 根据出库退库记录中的出库单流水号，取出库数据列表

            ArrayList al = this.QueryOutputList(outputID);
            if (al == null) return -1;

            //如果al超出索引，则提示出错
            if (al.Count == 0)
            {
                this.Err = "没有找到退库操作所对应的出库记录！";
                return -1;
            }
            //判断是否存在药柜出库记录
            FS.HISFC.Models.Pharmacy.Output outputTemp = al[0] as FS.HISFC.Models.Pharmacy.Output;
            //存在药柜出库记录 先进行药柜退库
            if (outputTemp.ArkOutNO != null && outputTemp.ArkOutNO != "")
            {
                #region 药柜记录退库

                ArrayList alTemp = this.QueryOutputList(outputTemp.ArkOutNO);
                if (alTemp == null)
                {
                    return -1;
                }
                if (alTemp.Count == 0)
                {
                    this.Err = "没有找到退库操作所对应的出库记录！";
                    return -1;
                }

                FS.HISFC.Models.Pharmacy.Output arkOut = alTemp[0] as FS.HISFC.Models.Pharmacy.Output;

                arkOut.Quantity = output.Quantity;
                arkOut.SystemType = output.SystemType;
                arkOut.PrivType = output.PrivType;
                arkOut.Operation = output.Operation;

                if (this.ArkOutputReturn(arkOut, outputTemp.ArkOutNO, false) != 1)
                {
                    this.Err = "对于药柜记录退库发生错误" + this.Err;
                    return -1;
                }

                output.ArkOutNO = arkOut.ID;

                #endregion
            }

            #endregion

            //当前时间
            DateTime sysTime = this.GetDateTimeFromSysDateTime();

            //取出库单流水号保存在output中，可以被外面调用
            output.ID = this.GetNewOutputNO();
            if (output.ID == null)
            {
                return -1;
            }

            //临时存储出库总数量和待出库数量
            FS.HISFC.Models.Pharmacy.StorageBase storageBase = null;
            decimal totOutNum = output.Quantity;
            decimal leftOutNum = output.Quantity;

            FS.HISFC.Models.Pharmacy.Input inputInfo;
            FS.HISFC.Models.Pharmacy.Input inputTemp;

            FS.HISFC.Models.Pharmacy.Output info = null;
            for (int i = 0; leftOutNum < 0; i++)
            {
                #region 退库有效性判断

                if (al.Count == i)
                {
                    this.Err = "该条出库记录的出库数量不足以进行此次退库 请重新选择退库记录";
                    return -1;
                }
                //取出库记录中的数据  
                info = al[i] as Output;
                //出库数量＝退库数量  此种记录已做过退库处理 继续查找
                if (info.Quantity == info.Operation.ReturnQty)
                {
                    continue;
                }

                #endregion

                #region 根据原出库记录 生成退库记录

                //将出库时的信息保存到退库记录中
                output.GroupNO = info.GroupNO;					//批次
                output.BatchNO = info.BatchNO;					//批号
                output.Company = info.Company;					//供货公司
                output.PlaceNO = info.PlaceNO;					//货位号
                output.Producer = info.Producer;					//生产厂家
                output.ValidTime = info.ValidTime;					//有效期
                output.Item.PriceCollection.RetailPrice = info.Item.PriceCollection.RetailPrice;	//零售价 利用原出库价格退库

                //当某一批次的可退数量（已出库数量－已退库数量）大于待退库数量时，则将此批次出库记录退库，退库数量等于待退库数量。(本次循环结束不再找下一个批次)
                if (info.Quantity - info.Operation.ReturnQty >= Math.Abs(leftOutNum))
                {
                    //退库数量等于待退库数量（待退库数量会随着循环的增加而逐渐减少）
                    //退库数量是负数
                    output.Quantity = leftOutNum;
                }
                else
                {
                    //如果可退数量（已出库数量－已退库数量）小于于待退库数量，则将此批次出库记录中的可退库数量全部退库。（程序会继续查找下一个批次的库存信息）
                    //退库数量是负数
                    output.Quantity = -(info.Quantity - info.Operation.ReturnQty);
                }

                //剩余待退库数量（负数）＝本次待退药数量（负数）－本次退药数量（负数）。如果剩余待退药数量小于0，则循环将继续进行。
                leftOutNum = leftOutNum - output.Quantity;

                //单内序号增加
                output.SerialNO = i + 1;

                //对于一条入库申请，如果出库记录多于一条，只有第一条出库记录中保存“申请数量",其余的出库记录中的申请数量为0，保证汇总数量正确
                if (i > 0) output.Operation.ApplyQty = 0;

                //插入出库记录
                output.State = "2";					                //不需核准操作

                //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出/退库记录发生时间
                output.OutDate = sysTime;

                if (this.InsertOutput(output) != 1)
                {
                    this.Err = "插入出库记录时出错！" + this.Err;
                    return -1;
                }

                //更新出库记录中的"已退库数量"字段（加操作）
                output.Quantity = -output.Quantity;
                if (this.UpdateOutputReturnNum(outputID, info.SerialNO, output.Quantity) != 1)
                {
                    this.Err = "更新出库记录中的已退库数量时出错！" + this.Err;
                    return -1;
                }

                #endregion

                #region 对非药房向药柜的出库记录 处理本科室库存处理

                //将出库数据赋值给库存数据，退库数量output.Quantity即是库存变化量（库存更新数量时是减操作）storageBase.Quantity（负数）
                storageBase = output.Clone() as StorageBase;

                storageBase.Class2Type = output.Class2Type;
                storageBase.PrivType = output.PrivType;

                if (!output.IsArkManager)
                {
                    //修改库存数据（通过库存明细表的触发器实现台帐表，库存汇总表处理）
                    //库存变化的数量（库存修改执行的加操作）跟出库的数量相反。
                    //先执行更新数量操作，如果数据库中没有记录则执行插入操作
                    if (this.SetStorage(storageBase) != 1)
                    {
                        this.Err = "更新库存表时出错！" + this.Err;
                        return -1;
                    }
                }

                #endregion

                #region 对于药柜退库 更新相应药房记录的药柜汇总库存量
                //药房向药柜出库记录(Output.IsArkManager 为True)
                //药房发药 存在对应的药柜出库记录  此两种情况下需更新药柜库存汇总量
                if (output.IsArkManager || (output.ArkOutNO != null && output.ArkOutNO != ""))
                {
                    if (output.IsArkManager)        //药房向药柜的出库记录 退库时 扣减药柜量
                    {
                        storageBase.ArkQty = -storageBase.Quantity;
                    }
                    else                           //正常退库 增加药柜量
                    {
                        storageBase.ArkQty = storageBase.Quantity;
                    }

                    if (this.SetArkStorage(storageBase) != 1)
                    {
                        this.Err = "更新库存表时出错！" + this.Err;
                        return -1;
                    }
                }

                #endregion

                #region 如果出库的药品零售价跟库存中的药品零售价不同，则记录调价盈亏

                this.OutputAdjust(info, output, sysTime, i);

                #endregion

                #region 处理对应领用部门的入库数据（"特殊出库"不处理领用单位的入库，台帐，库存）

                //插入领用部门入库记录
                //判断是否需要对领用部门进行库存管理。如果管理库存，则进行下面处理：
                if (isManagerInput)
                {
                    inputInfo = this.GetInputInfoByID(info.InBillNO);
                    if (inputInfo == null)
                    {
                        return -1;
                    }
                    inputTemp = inputInfo.Clone();
                    inputTemp.ID = "";							//流水号
                    inputTemp.Quantity = -output.Quantity;		//数量
                    inputTemp.GroupNO = output.GroupNO;			//批次
                    inputTemp.BatchNO = output.BatchNO;			//批号
                    inputTemp.Company = output.StockDept;		//供货公司
                    inputTemp.PlaceNO = output.PlaceNO;		    //货位号
                    inputTemp.Producer = output.Producer;		//生产厂家
                    inputTemp.ValidTime = output.ValidTime;		//有效期
                    inputTemp.Operation.ReturnQty = 0;

                    inputTemp.OutBillNO = output.ID;          //出库单据号

                    inputTemp.StoreQty = inputTemp.StoreQty + inputTemp.Quantity;

                    //插入入库负记录
                    if (this.Input(inputTemp, "1", "1") == -1)
                    {
                        this.Err = this.Err + "插入入库负记录出错！";
                        return -1;
                    }
                    //更新原入库记录退库数量
                    if (this.UpdateInputReturnNum(inputInfo.ID, inputInfo.SerialNO, -inputTemp.Quantity) != 1)
                    {
                        this.Err = this.Err + "更新入库记录退库数量出错！";
                        return -1;
                    }
                }

                #endregion
            }

            //恢复output实体中传入时的数值
            output.Quantity = totOutNum;
            return 1;
        }

        #endregion

        #endregion

        #region 外部接口

        /// <summary>
        /// 门诊退库
        /// 如果退库申请中，指定确定的批次，则将此批次记录退掉。
        /// 否则，在与出库申请对应的出库记录中按批次小先退的原则，做退库处理。
        /// </summary>
        /// <param name="feeInfo">收费费用实体</param>
        /// <returns>成功返回1 失败返回-1 无记录返回0</returns>
        public int OutputReturn(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeInfo, string operCode, DateTime operDate)
        {

            #region 等待门诊费用加字段 存储出库单据号
            #endregion

            DateTime sysTime = this.GetDateTimeFromSysDateTime();
            FS.HISFC.Models.Pharmacy.Output output = new Output();

            #region Output实体赋值

            output.ID = this.GetNewOutputNO();
            if (output.ID == null) return -1;

            FS.HISFC.Models.Pharmacy.Item item = this.GetItem(feeInfo.Item.ID);
            if (item == null)
            {
                this.Err = "获取药品基本信息失败" + this.Err;
                return -1;
            }

            output.Item.MinUnit = item.MinUnit;					                        //最小单位 ＝ 记价单位
            output.Item.PackUnit = item.PackUnit;
            output.Item.PriceCollection.RetailPrice = feeInfo.Item.Price;				//零售价
            output.Item.ID = feeInfo.Item.ID;							                    //药品编码
            output.Item.Name = feeInfo.Item.Name;						                    //药品名称
            output.Item.Type = item.Type;							                    //药品类别
            output.Item.Quality = ((FS.HISFC.Models.Pharmacy.Item)feeInfo.Item).Quality;			//药品性质

            output.Item.Specs = feeInfo.Item.Specs;					                    //规格
            output.Item.PackQty = feeInfo.Item.PackQty;					                //包装数量
            output.Item.DoseUnit = feeInfo.Order.DoseUnit;				                //每次剂量单位
            output.Item.DosageForm = ((FS.HISFC.Models.Pharmacy.Item)feeInfo.Item).DosageForm;				//剂型

            output.TargetDept = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Dept;				//申请科室＝开方科室   出库申请科室
            //[2011-5-12]by zhaozf：退库药房=出库药房，改为在下面赋出库记录中的出库药房
            //output.StockDept = feeInfo.ExecOper.Dept;						            //发药药房＝执行科室   出库科室
            output.SystemType = "M2";								                    //申请类型＝"M2" 门诊退药 
            output.PrivType = "M2";                                                     //用户自定义类型
            output.Operation.ApplyOper.OperTime = sysTime;							    //申请时间＝操作时间                    
            output.RecipeNO = feeInfo.RecipeNO;					                        //处方号
            output.SequenceNO = feeInfo.SequenceNO;						                //处方内流水号

            output.ShowState = "0";								                        //显示的单位标记（0最小单位，1包装单位）
            //此处赋值为乘以Days后的数量 药品部分申请表里边是剂数及每次剂量分开存储的
            output.Quantity = feeInfo.Item.Qty;						                    //出库数量
            output.GetPerson = feeInfo.Patient.ID;					                            //取药患者门诊流水号

            output.DrugedBillNO = "0";							                        //摆药单号 必须传值 

            output.SpecialFlag = "0";								                    //特殊标记。1是，0否
            output.Operation.ApplyOper.ID = operCode;						            //出库申请人编码
            output.Operation.ApplyOper.OperTime = operDate;						        //出库申请日期
            output.Operation.ApplyQty = output.Quantity;						        //申请数量
            output.Operation.ExamQty = output.Quantity;					                //审核数量
            output.Operation.ExamOper.ID = operCode;							        //审批人 ＝打印人
            output.Operation.ExamOper.OperTime = operDate;							    //审批日期＝打印日期
            output.Operation.ApproveOper.ID = operCode;						            //出库核准
            output.State = "2";

            #endregion

            //临时存储出库总数量和待出库数量
            FS.HISFC.Models.Pharmacy.StorageBase storageBase;

            output.Operation.ExamQty = -output.Quantity;
            decimal totOutNum = -output.Quantity;
            decimal leftOutNum = -output.Quantity;

            //根据出库退库记录中的出库单流水号，取出库数据列表。
            ArrayList al = new ArrayList();
            al = this.QueryOutputList(feeInfo.RecipeNO, feeInfo.SequenceNO, "M1");
            if (al == null) return -1;

            //如果al超出索引，用于控制重复退费
            if (al.Count == 0)
            {
                this.Err = "没有找到退库操作所对应的出库记录！";
                return 0;
            }

            FS.HISFC.Models.Pharmacy.Output info;
            //如果退库申请中，指定确定的批次，则将此批次记录退掉。
            //否则，在与出库申请对应的出库记录中按批次小先退的原则，做退库处理。

            for (int i = 0; leftOutNum < 0; i++)
            {
                #region 根据出库记录 生成 退库记录

                if (al.Count <= i)
                {
                    this.Err = "药品" + feeInfo.Item.Name + "本次申请数量 大于 已出库数量";
                    return -1;
                }

                //取出库记录中的数据  
                info = al[i] as Output;

                //将出库时的信息保存到退库记录中
                //[2011-5-12]by zhaozf：退库科室=出库科室，会多次赋值，不过没关系
                output.StockDept = info.StockDept;
                output.GroupNO = info.GroupNO;					//批次
                output.BatchNO = info.BatchNO;					//批号
                output.Company = info.Company;					//供货公司
                output.PlaceNO = info.PlaceNO;					//货位号
                output.Producer = info.Producer;					//生产厂家
                output.ValidTime = info.ValidTime;					//有效期
                output.Item.PriceCollection.RetailPrice = info.Item.PriceCollection.RetailPrice;	//零售价 利用原出库价格退库
                //{92FE9833-A574-496b-93D9-A4BEDF5AD7CD}  保证购入价的赋值
                output.Item.PriceCollection = info.Item.PriceCollection;

                //当某一批次的可退数量（已出库数量－已退库数量）大于待退库数量时，则将此批次出库记录退库，退库数量等于待退库数量。(本次循环结束不再找下一个批次)
                if (info.Quantity - info.Operation.ReturnQty >= Math.Abs(leftOutNum))
                {
                    //退库数量等于待退库数量（待退库数量会随着循环的增加而逐渐减少）
                    //退库数量是负数
                    output.Quantity = leftOutNum;
                }
                else
                {
                    //如果可退数量（已出库数量－已退库数量）小于于待退库数量，则将此批次出库记录中的可退库数量全部退库。（程序会继续查找下一个批次的库存信息）
                    //退库数量是负数
                    output.Quantity = -(info.Quantity - info.Operation.ReturnQty);
                }

                //剩余待退库数量（负数）＝本次待退药数量（负数）－本次退药数量（负数）。如果剩余待退药数量小于0，则循环将继续进行。
                leftOutNum = leftOutNum - output.Quantity;

                //单内序号增加
                output.SerialNO = i + 1;

                //对于一条入库申请，如果出库记录多于一条，只有第一条出库记录中保存“申请数量",其余的出库记录中的申请数量为0，保证汇总数量正确
                if (i > 0) output.Operation.ApplyQty = 0;

                //插入出库记录
                output.State = "2";					//不需核准操作

                #endregion

                #region 退库记录保持 库存更新

                //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出/退库记录发生时间
                output.OutDate = sysTime;

                if (this.InsertOutput(output) != 1)
                {
                    this.Err = "插入出库记录时出错！" + this.Err;
                    return -1;
                }

                //更新出库记录中的"已退库数量"字段（加操作）
                output.Quantity = -output.Quantity;
                if (this.UpdateOutputReturnNum(info.ID, info.SerialNO, output.Quantity) != 1)
                {
                    this.Err = "更新出库记录中的已退库数量时出错！" + this.Err;
                    return -1;
                }

                //将出库数据赋值给库存数据，退库数量output.Quantity即是库存变化量（库存更新数量时是减操作）storageBase.Quantity（负数）
                storageBase = output.Clone() as StorageBase;

                storageBase.Class2Type = output.Class2Type;
                storageBase.PrivType = output.PrivType;

                //storageBase.PrivType = "0320" + output.PrivType;

                //修改库存数据（通过库存明细表的触发器实现台帐表，库存汇总表处理）
                //库存变化的数量（库存修改执行的加操作）跟出库的数量相反。
                //storageBase.Quantity = -output.Quantity; output中已经转为负数
                //先执行更新数量操作，如果数据库中没有记录则执行插入操作
                if (this.SetStorage(storageBase) != 1)
                {
                    this.Err = "更新库存表时出错！" + this.Err;
                    return -1;
                }

                #endregion

                #region 如果出库的药品零售价跟库存中的药品零售价不同，则记录调价盈亏

                #region 屏蔽以下处理 通过函数调用实现

                //bool isDoAdjust = false;
                //string adjustPriceID = "";
                //decimal dNowPrice = 0;
                //dNowPrice = output.Item.PriceCollection.RetailPrice;

                //if (info.Item.PriceCollection.RetailPrice != dNowPrice)
                //{
                //    //调价处理
                //    //
                //    if (!isDoAdjust)
                //    {
                //        adjustPriceID = this.GetSequence("Pharmacy.Item.GetNewAdjustPriceID");
                //        if (adjustPriceID == null)
                //        {
                //            this.Err = "出库退库药品已发生调价 插入调价盈亏记录过程中获取调价单号出错！";
                //            return -1;
                //        }
                //    }
                //    FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = new AdjustPrice();
                //    adjustPrice.ID = adjustPriceID;								//调价单号
                //    adjustPrice.SerialNO = i;									//调价单内序号
                //    adjustPrice.Item = info.Item;
                //    adjustPrice.StockDept.ID = info.StockDept.ID;				//调价科室 
                //    adjustPrice.State = "1";									//调价状态 1 已调价
                //    adjustPrice.StoreQty = output.Quantity;
                //    adjustPrice.Operation.Oper.ID = this.Operator.ID;
                //    adjustPrice.Operation.Oper.Name = this.Operator.Name;
                //    adjustPrice.Operation.Oper.OperTime = sysTime;
                //    adjustPrice.InureTime = sysTime;
                //    adjustPrice.AfterRetailPrice = dNowPrice;//调价后零售价
                //    if (dNowPrice - info.Item.PriceCollection.RetailPrice > 0)
                //        adjustPrice.ProfitFlag = "1";							//调盈
                //    else
                //        adjustPrice.ProfitFlag = "0";							//调亏
                //    adjustPrice.Memo = "出库退库补调价盈亏";
                //    if (!isDoAdjust)			//每次只插入一次调价汇总表
                //    {
                //        if (this.InsertAdjustPriceInfo(adjustPrice) == -1)
                //        {
                //            return -1;
                //        }
                //        isDoAdjust = true;
                //    }
                //    if (this.InsertAdjustPriceDetail(adjustPrice) == -1)
                //    {
                //        return -1;
                //    }
                //}

                #endregion

                //2011-05-27 by cube
                //this.OutputAdjust(info, output, sysTime, i);
                if (this.OutputAdjust(output, null, i, sysTime) == -1)
                {
                    return -1;
                }
                //end by
                #endregion

            }
            return 1;
        }

        /// <summary>
        /// 住院退库
        /// 如果退库申请中，指定确定的批次，则将此批次记录退掉。
        /// 否则，在与出库申请对应的出库记录中按批次小先退的原则，做退库处理。
        /// </summary>
        /// <param name="feeInfo">收费费用实体</param>
        /// <returns>成功返回1 失败返回-1 无记录返回0</returns>
        public int OutputReturn(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeInfo, string operCode, DateTime operDate)
        {
            DateTime sysTime = this.GetDateTimeFromSysDateTime();
            FS.HISFC.Models.Pharmacy.Output output = new Output();

            #region Output实体赋值

            output.ID = this.GetNewOutputNO();
            if (output.ID == null) return -1;

            FS.HISFC.Models.Pharmacy.Item item = this.GetItem(feeInfo.Item.ID);
            if (item == null)
            {
                this.Err = "获取药品基本信息失败" + this.Err;
                return -1;
            }

            output.Item.MinUnit = item.MinUnit;					                        //最小单位 ＝ 记价单位
            output.Item.PackUnit = item.PackUnit;
            output.Item.PriceCollection.RetailPrice = feeInfo.Item.Price;				//零售价
            output.Item.ID = feeInfo.Item.ID;							                //药品编码
            output.Item.Name = feeInfo.Item.Name;						                //药品名称
            output.Item.Type = item.Type;							                    //药品类别
            output.Item.Quality = ((FS.HISFC.Models.Pharmacy.Item)feeInfo.Item).Quality;			//药品性质

            output.Item.Specs = feeInfo.Item.Specs;					                    //规格
            output.Item.PackQty = feeInfo.Item.PackQty;					                //包装数量
            output.Item.DoseUnit = feeInfo.Order.DoseUnit;				                //每次剂量单位
            output.Item.DosageForm = ((FS.HISFC.Models.Pharmacy.Item)feeInfo.Item).DosageForm;				//剂型

            output.TargetDept = ((FS.HISFC.Models.RADT.PatientInfo)feeInfo.Patient).PVisit.PatientLocation.Dept;				                //申请科室＝开方科室   出库申请科室
            output.StockDept = feeInfo.StockOper.Dept;						            //发药药房＝执行科室   出库科室
            output.SystemType = "Z2";								                    //申请类型＝"M2" 门诊退药 
            output.PrivType = "Z2";                                                     //用户自定义类型
            output.Operation.ApplyOper.OperTime = sysTime;							    //申请时间＝操作时间                    
            output.RecipeNO = feeInfo.RecipeNO;					                        //处方号
            output.SequenceNO = feeInfo.SequenceNO;						                //处方内流水号

            output.ShowState = "0";								                        //显示的单位标记（0最小单位，1包装单位）
            //此处赋值为乘以Days后的数量 药品部分申请表里边是剂数及每次剂量分开存储的
            output.Quantity = feeInfo.Item.Qty;						                    //出库数量
            output.GetPerson = feeInfo.Patient.ID;					                            //取药患者门诊流水号

            output.DrugedBillNO = "0";							                        //摆药单号 必须传值 

            output.SpecialFlag = "0";								                    //特殊标记。1是，0否
            output.Operation.ApplyOper.ID = operCode;						            //出库申请人编码
            output.Operation.ApplyOper.OperTime = operDate;						        //出库申请日期
            output.Operation.ApplyQty = output.Quantity;						        //申请数量
            output.Operation.ExamQty = output.Quantity;					                //审核数量
            output.Operation.ExamOper.ID = operCode;							        //审批人 ＝打印人
            output.Operation.ExamOper.OperTime = operDate;							    //审批日期＝打印日期
            output.Operation.ApproveOper.ID = operCode;						            //出库核准
            output.State = "2";

            #endregion

            //临时存储出库总数量和待出库数量
            FS.HISFC.Models.Pharmacy.StorageBase storageBase;

            output.Operation.ExamQty = -output.Quantity;
            decimal totOutNum = -output.Quantity;
            decimal leftOutNum = -output.Quantity;

            //根据出库退库记录中的出库单流水号，取出库数据列表。
            ArrayList al = new ArrayList();
            al = this.QueryOutputList(feeInfo.RecipeNO, feeInfo.SequenceNO, "Z1");
            if (al == null) return -1;

            //如果al超出索引，用于控制重复退费
            if (al.Count == 0)
            {
                this.Err = "没有找到退库操作所对应的出库记录！";
                return 0;
            }

            FS.HISFC.Models.Pharmacy.Output info;
            //如果退库申请中，指定确定的批次，则将此批次记录退掉。
            //否则，在与出库申请对应的出库记录中按批次小先退的原则，做退库处理。

            for (int i = 0; leftOutNum < 0; i++)
            {
                #region 根据出库记录 生成 退库记录

                if (al.Count <= i)
                {
                    this.Err = "药品" + feeInfo.Item.Name + "本次申请数量 大于 已出库数量";
                    return -1;
                }

                //取出库记录中的数据  
                info = al[i] as Output;

                //将出库时的信息保存到退库记录中
                output.GroupNO = info.GroupNO;					//批次
                output.BatchNO = info.BatchNO;					//批号
                output.Company = info.Company;					//供货公司
                output.PlaceNO = info.PlaceNO;					//货位号
                output.Producer = info.Producer;					//生产厂家
                output.ValidTime = info.ValidTime;					//有效期
                output.Item.PriceCollection.RetailPrice = info.Item.PriceCollection.RetailPrice;	//零售价 利用原出库价格退库
                //{92FE9833-A574-496b-93D9-A4BEDF5AD7CD}  保证购入价的赋值
                output.Item.PriceCollection = info.Item.PriceCollection;

                //当某一批次的可退数量（已出库数量－已退库数量）大于待退库数量时，则将此批次出库记录退库，退库数量等于待退库数量。(本次循环结束不再找下一个批次)
                if (info.Quantity - info.Operation.ReturnQty >= Math.Abs(leftOutNum))
                {
                    //退库数量等于待退库数量（待退库数量会随着循环的增加而逐渐减少）
                    //退库数量是负数
                    output.Quantity = leftOutNum;
                }
                else
                {
                    //如果可退数量（已出库数量－已退库数量）小于于待退库数量，则将此批次出库记录中的可退库数量全部退库。（程序会继续查找下一个批次的库存信息）
                    //退库数量是负数
                    output.Quantity = -(info.Quantity - info.Operation.ReturnQty);
                }

                //剩余待退库数量（负数）＝本次待退药数量（负数）－本次退药数量（负数）。如果剩余待退药数量小于0，则循环将继续进行。
                leftOutNum = leftOutNum - output.Quantity;

                //单内序号增加
                output.SerialNO = i + 1;

                //对于一条入库申请，如果出库记录多于一条，只有第一条出库记录中保存“申请数量",其余的出库记录中的申请数量为0，保证汇总数量正确
                if (i > 0) output.Operation.ApplyQty = 0;

                //插入出库记录
                output.State = "2";					//不需核准操作

                #endregion

                #region 退库记录保持 库存更新

                //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出/退库记录发生时间
                output.OutDate = sysTime;

                if (this.InsertOutput(output) != 1)
                {
                    this.Err = "插入出库记录时出错！" + this.Err;
                    return -1;
                }

                //更新出库记录中的"已退库数量"字段（加操作）
                output.Quantity = -output.Quantity;
                if (this.UpdateOutputReturnNum(info.ID, info.SerialNO, output.Quantity) != 1)
                {
                    this.Err = "更新出库记录中的已退库数量时出错！" + this.Err;
                    return -1;
                }

                //将出库数据赋值给库存数据，退库数量output.Quantity即是库存变化量（库存更新数量时是减操作）storageBase.Quantity（负数）
                storageBase = output.Clone() as StorageBase;

                storageBase.Class2Type = output.Class2Type;
                storageBase.PrivType = output.PrivType;

                //storageBase.PrivType = "0320" + output.PrivType;

                //修改库存数据（通过库存明细表的触发器实现台帐表，库存汇总表处理）
                //库存变化的数量（库存修改执行的加操作）跟出库的数量相反。
                //storageBase.Quantity = -output.Quantity; output中已经转为负数
                //先执行更新数量操作，如果数据库中没有记录则执行插入操作
                if (this.SetStorage(storageBase) != 1)
                {
                    this.Err = "更新库存表时出错！" + this.Err;
                    return -1;
                }

                #endregion

                #region 如果出库的药品零售价跟库存中的药品零售价不同，则记录调价盈亏

                //2011-05-27 by cube
                //this.OutputAdjust(info, output, sysTime, i);
                if (this.OutputAdjust(output, null, i, sysTime) == -1)
                {
                    return -1;
                }
                //end by

                #endregion

            }
            return 1;
        }

        /// <summary>
        /// 根据药品、退库数量、源/目标科室直接退库
        /// {1E95F7E5-7C6F-483a-9B7E-EA1DBDD9540F}
        /// 该部分需退库数据由库存列表选择产生，所以源科室、目标科室肯定都管理库存，不需进行是否管理库存的判断
        /// </summary>
        /// <param name="backDrugInformation">退库药品 需包含单据号、当前库存数</param>
        /// <param name="backDrugQty">退库药品数量</param>
        /// <param name="sourceDept">源科室(退库科室)</param>
        /// <param name="isSourceArk">源科室是否为药柜方式管理</param>
        /// <param name="targetDept">目标科室(退库目的科室)</param>
        /// <param name="isTargetArk">目标科室是否为药柜方式管理</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int OutputReturnForSingleDrug(Output backDrugInformation, decimal backDrugQty, FS.FrameWork.Models.NeuObject sourceDept, bool isSourceArk, FS.FrameWork.Models.NeuObject targetDept, bool isTargetArk)
        {
            #region 获取源科室退库药品的库存信息 按照批次流水号 由小到大

            //by cube 2011-04-20 有批次号是根据批次号获取库存
            ArrayList alSourceStoreList = new ArrayList();
            if (backDrugInformation.GroupNO > 0)
            {
                alSourceStoreList = this.QueryStorageList(sourceDept.ID, backDrugInformation.Item.ID, backDrugInformation.GroupNO);
            }
            else
            {
                alSourceStoreList = this.QueryStorageList(sourceDept.ID, backDrugInformation.Item.ID);
            }
            //end 

            if (alSourceStoreList == null)
            {
                return -1;
            }
            if (alSourceStoreList.Count == 0)
            {
                this.Err = backDrugInformation.Item.Name + "  在" + sourceDept.Name + "已无库存，不能进行退库操作";
                return -1;
            }

            #endregion

            #region 根据退库数量进行退库处理  退库批次由小到大退

            DateTime sysTime = this.GetDateTimeFromSysDateTime();

            decimal totBackQty = backDrugQty;
            //临时货位号，存目标科室的
            string placeCodeTemp = "";
            FS.HISFC.Models.Pharmacy.Output output = backDrugInformation.Clone();            
            foreach (FS.HISFC.Models.Pharmacy.Storage store in alSourceStoreList)
            {
                if (totBackQty <= 0)
                {
                    break;
                }

                decimal batchBackQty = totBackQty;

                #region 计算本循环处理的退库数量

                if (store.StoreQty >= totBackQty)       //库存数量大于退库数量
                {
                    batchBackQty = totBackQty;
                    totBackQty = 0;
                }
                else                                   //库存数量小于退库数量
                {
                    batchBackQty = store.StoreQty;
                    totBackQty = totBackQty - store.StoreQty;
                }

                #endregion

                FS.HISFC.Models.Pharmacy.Storage alterStore = store.Clone();

                #region 形成目标科室 退库记录(出库负记录)

                #region 出库实体信息赋值

                output.StockDept = targetDept;          //库存管理科室 即本次出库记录对应的库存变化科室
                output.TargetDept = sourceDept;         //出库目标科室 对应退库记录的目标科室 

                if (string.IsNullOrEmpty(output.InListNO))
                {
                    output.Quantity = -batchBackQty;         //出库数量
                }
                else
                {
                    output.Quantity = -backDrugQty;
                }

                //将部门库存信息保存到出库记录中
                output.GroupNO = alterStore.GroupNO;           //批次
                output.BatchNO = alterStore.BatchNO;           //批号
                output.Company = alterStore.Company;           //供货公司
                //退库不更新货位号，否则当药房是初始化库存，并且药库没有对应批次号的情况会把药库货位号更新掉的
                //{83A75FE2-89DE-4b11-9BCB-6092837FE537}
                output.PlaceNO = alterStore.PlaceNO;           //货位号
                placeCodeTemp = this.GetPlaceNO(targetDept.ID, backDrugInformation.Item.ID);
                alterStore.PlaceNO = placeCodeTemp;
                output.Producer = alterStore.Producer;         //生产厂家
                output.ValidTime = alterStore.ValidTime;       //有效期

                output.SerialNO++;

                //by cube 药品表现层已经赋值
                //output.Operation.ApplyOper.ID = this.Operator.ID;
                //output.Operation.ApplyOper.OperTime = sysTime;
                //output.Operation.ApplyQty = output.Quantity;

                //output.Operation.ApproveOper = output.Operation.ApplyOper;
                //output.Operation.ApproveQty = output.Quantity;

                //output.Operation.ExamOper = output.Operation.ApplyOper;
                //output.Operation.ExamQty = output.Quantity;
                //end by

                output.DrugedBillNO = "1";
                output.State = "2";

                //by cube 2011-04-19

                //购入金额
                output.PurchaseCost = output.Item.PriceCollection.PurchasePrice * (output.Quantity / output.Item.PackQty);
                output.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(output.PurchaseCost.ToString("F" + output.CostDecimals.ToString()));

                //零售金额
                output.RetailCost = output.Item.PriceCollection.RetailPrice * (output.Quantity / output.Item.PackQty);
                output.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(output.RetailCost.ToString("F" + output.CostDecimals.ToString()));

                //批发金额
                output.WholeSaleCost = output.Item.PriceCollection.WholeSalePrice * (output.Quantity / output.Item.PackQty);
                output.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(output.WholeSaleCost.ToString("F" + output.CostDecimals.ToString()));

                //end by

                #endregion

                //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出/退库记录发生时间
                //药库的手工选择药品退库，时间在表现层赋值
                //output.OutDate = sysTime;

                if (this.InsertOutput(output) == -1)
                {
                    return -1;
                }

                #endregion

                if (!(!isTargetArk && isSourceArk))        //如源科室为药柜 且 目标科室不为药柜 则不处理目标科室库存
                {
                    #region 增加目标科室库存

                    alterStore.StockDept = targetDept;          //库存科室
                    if (string.IsNullOrEmpty(output.InListNO))
                    {
                        alterStore.Quantity = batchBackQty;         //库存变化数量
                    }
                    else
                    {
                        alterStore.Quantity = backDrugQty;
                    }
                    alterStore.TargetDept = sourceDept;         //目标科室
                    alterStore.Class2Type = output.Class2Type;
                    alterStore.PrivType = output.PrivType;
                    alterStore.ID = output.ID;
                    alterStore.SerialNO = output.SerialNO;
                    if (this.SetStorage(alterStore) == -1)
                    {
                        return -1;
                    }

                    #endregion
                }

                #region 形成源科室 入库负记录
                if (string.IsNullOrEmpty(output.InListNO))
                {
                    #region 入库实体信息赋值

                    Input inputTemp = new Input();

                    inputTemp.StockDept = sourceDept;
                    inputTemp.TargetDept = targetDept;
                    inputTemp.Item = output.Item;

                    inputTemp.Quantity = -batchBackQty;		//数量
                    inputTemp.GroupNO = output.GroupNO;			//批次
                    inputTemp.BatchNO = output.BatchNO;			//批号
                    inputTemp.Company = output.StockDept;		//供货公司
                    inputTemp.PlaceNO = output.PlaceNO;		    //货位号
                    alterStore.PlaceNO = output.PlaceNO;	
                    inputTemp.Producer = output.Producer;		//生产厂家
                    inputTemp.ValidTime = output.ValidTime;		//有效期
                    inputTemp.Operation.ReturnQty = 0;

                    inputTemp.InListNO = output.OutListNO;
                    inputTemp.OutBillNO = output.ID;          //出库单据号

                    inputTemp.Operation = output.Operation;
                    inputTemp.Operation.ApplyQty = inputTemp.Quantity;
                    inputTemp.Operation.ExamQty = inputTemp.Quantity;
                    inputTemp.Operation.ApproveQty = inputTemp.Quantity;
                    inputTemp.State = "2";
                    inputTemp.OutListNO = output.OutListNO;
                    inputTemp.OutBillNO = output.ID;
                    inputTemp.OutSerialNO = output.SerialNO;
                    inputTemp.SystemType = FS.HISFC.Models.Base.EnumIMAInTypeService.GetNameFromEnum(EnumIMAInType.InnerBackApply);
                    inputTemp.PrivType = "01";

                    inputTemp.InDate = output.OutDate;

                    //by cube 金额和出库一致
                    inputTemp.RetailCost = output.RetailCost;
                    inputTemp.WholeSaleCost = output.WholeSaleCost;
                    inputTemp.PurchaseCost = output.PurchaseCost;
                    //end by

                    #endregion

                    if (this.InsertInput(inputTemp) == -1)
                    {
                        return -1;
                    }
              
                #endregion

                if (!(isTargetArk && !isSourceArk))         //如源科室不为药柜 且 目标科室为药柜
                {
                    #region 减少源科室库存

                    alterStore.StockDept = sourceDept;          //库存科室
                    alterStore.Quantity = -batchBackQty;         //库存变化数量
                    alterStore.TargetDept = targetDept;         //目标科室
                    alterStore.Class2Type = inputTemp.Class2Type;
                    alterStore.PrivType = inputTemp.PrivType;
                    alterStore.ID = inputTemp.ID;
                    alterStore.SerialNO = inputTemp.SerialNO;
                    if (this.SetStorage(alterStore) == -1)
                    {
                        return -1;
                    }

                    #endregion
                }
                }
            }

            #endregion

            if (string.IsNullOrEmpty(output.InListNO)&&totBackQty > 0)
            {
                this.Err = backDrugInformation.Item.Name + "  在" + sourceDept.Name + "库存不足，不能进行退库操作";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 根据药品、退库数量、源/目标科室直接退库
        /// {1E95F7E5-7C6F-483a-9B7E-EA1DBDD9540F}
        /// 该部分需退库数据由库存列表选择产生，所以源科室、目标科室肯定都管理库存，不需进行是否管理库存的判断
        /// </summary>
        /// <param name="backDrugInformation">退库药品 需包含单据号、当前库存数</param>
        /// <param name="backDrugQty">退库药品数量</param>
        /// <param name="sourceDept">源科室(退库科室)</param>
        /// <param name="isSourceArk">源科室是否为药柜方式管理</param>
        /// <param name="targetDept">目标科室(退库目的科室)</param>
        /// <param name="isTargetArk">目标科室是否为药柜方式管理</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int OutputReturnForSelectDrug(Output backDrugInformation, decimal backDrugQty, FS.FrameWork.Models.NeuObject sourceDept, bool isSourceArk, FS.FrameWork.Models.NeuObject targetDept, bool isTargetArk)
        {
            #region 获取源科室退库药品的库存信息 按照批次流水号 由小到大

            //by cube 2011-04-20 有批次号是根据批次号获取库存
            ArrayList alSourceStoreList = new ArrayList();
            if (backDrugInformation.GroupNO > 0)
            {
                alSourceStoreList = this.QueryStorageList(sourceDept.ID, backDrugInformation.Item.ID, backDrugInformation.GroupNO);
            }
            else
            {
                alSourceStoreList = this.QueryStorageList(sourceDept.ID, backDrugInformation.Item.ID);
            }
            //end 

            if (alSourceStoreList == null)
            {
                return -1;
            }
            if (alSourceStoreList.Count == 0)
            {
                this.Err = backDrugInformation.Item.Name + "  在" + sourceDept.Name + "已无库存，不能进行退库操作";
                return -1;
            }

            #endregion

            #region 根据退库数量进行退库处理  退库批次由小到大退

            DateTime sysTime = this.GetDateTimeFromSysDateTime();

            decimal totBackQty = backDrugQty;
            FS.HISFC.Models.Pharmacy.Output output = backDrugInformation.Clone();


            foreach (FS.HISFC.Models.Pharmacy.Storage store in alSourceStoreList)
            {
                if (totBackQty <= 0)
                {
                    break;
                }

                decimal batchBackQty = totBackQty;

                #region 计算本循环处理的退库数量

                if (store.StoreQty >= totBackQty)       //库存数量大于退库数量
                {
                    batchBackQty = totBackQty;
                    totBackQty = 0;
                }
                else                                   //库存数量小于退库数量
                {
                    batchBackQty = store.StoreQty;
                    totBackQty = totBackQty - store.StoreQty;
                }

                #endregion

                FS.HISFC.Models.Pharmacy.Storage alterStore = store.Clone();

                #region 形成目标科室 退库记录(出库负记录)

                #region 出库实体信息赋值

                output.StockDept = targetDept;          //库存管理科室 即本次出库记录对应的库存变化科室
                output.TargetDept = sourceDept;         //出库目标科室 对应退库记录的目标科室 

                output.Quantity = -batchBackQty;         //出库数量

                //将部门库存信息保存到出库记录中
                output.GroupNO = alterStore.GroupNO;           //批次
                output.BatchNO = alterStore.BatchNO;           //批号
                output.Company = alterStore.Company;           //供货公司
                output.PlaceNO = alterStore.PlaceNO;           //货位号
                output.Producer = alterStore.Producer;         //生产厂家
                output.ValidTime = alterStore.ValidTime;       //有效期

                output.SerialNO++;

                //by cube 药品表现层已经赋值
                //output.Operation.ApplyOper.ID = this.Operator.ID;
                //output.Operation.ApplyOper.OperTime = sysTime;
                //output.Operation.ApplyQty = output.Quantity;

                //output.Operation.ApproveOper = output.Operation.ApplyOper;
                //output.Operation.ApproveQty = output.Quantity;

                //output.Operation.ExamOper = output.Operation.ApplyOper;
                //output.Operation.ExamQty = output.Quantity;
                //end by

                output.DrugedBillNO = "1";
                output.State = "2";

                //by cube 2011-04-19

                //购入金额
                output.PurchaseCost = output.Item.PriceCollection.PurchasePrice * (output.Quantity / output.Item.PackQty);
                output.PurchaseCost = FS.FrameWork.Function.NConvert.ToDecimal(output.PurchaseCost.ToString("F" + output.CostDecimals.ToString()));

                //零售金额
                output.RetailCost = output.Item.PriceCollection.RetailPrice * (output.Quantity / output.Item.PackQty);
                output.RetailCost = FS.FrameWork.Function.NConvert.ToDecimal(output.RetailCost.ToString("F" + output.CostDecimals.ToString()));

                //批发金额
                output.WholeSaleCost = output.Item.PriceCollection.WholeSalePrice * (output.Quantity / output.Item.PackQty);
                output.WholeSaleCost = FS.FrameWork.Function.NConvert.ToDecimal(output.WholeSaleCost.ToString("F" + output.CostDecimals.ToString()));

                //end by

                #endregion

                //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出/退库记录发生时间
                //药库的手工选择药品退库，时间在表现层赋值
                //output.OutDate = sysTime;

                if (this.InsertOutput(output) == -1)
                {
                    return -1;
                }

                #endregion

                if (!(!isTargetArk && isSourceArk))        //如源科室为药柜 且 目标科室不为药柜 则不处理目标科室库存
                {
                    #region 增加目标科室库存

                    alterStore.StockDept = targetDept;          //库存科室
                    alterStore.Quantity = batchBackQty;         //库存变化数量
                    alterStore.TargetDept = sourceDept;         //目标科室
                    alterStore.Class2Type = output.Class2Type;
                    alterStore.PrivType = output.PrivType;
                    alterStore.ID = output.ID;
                    alterStore.SerialNO = output.SerialNO;
                    if (this.SetStorage(alterStore) == -1)
                    {
                        return -1;
                    }

                    #endregion
                }

                #region 形成源科室 入库负记录

                #region 入库实体信息赋值

                Input inputTemp = new Input();

                inputTemp.StockDept = sourceDept;
                inputTemp.TargetDept = targetDept;
                inputTemp.Item = output.Item;

                inputTemp.Quantity = -batchBackQty;		//数量
                inputTemp.GroupNO = output.GroupNO;			//批次
                inputTemp.BatchNO = output.BatchNO;			//批号
                inputTemp.Company = output.StockDept;		//供货公司
                inputTemp.PlaceNO = output.PlaceNO;		    //货位号
                inputTemp.Producer = output.Producer;		//生产厂家
                inputTemp.ValidTime = output.ValidTime;		//有效期
                inputTemp.Operation.ReturnQty = 0;

                inputTemp.InListNO = output.OutListNO;
                inputTemp.OutBillNO = output.ID;          //出库单据号

                inputTemp.Operation = output.Operation;
                inputTemp.Operation.ApplyQty = inputTemp.Quantity;
                inputTemp.Operation.ExamQty = inputTemp.Quantity;
                inputTemp.Operation.ApproveQty = inputTemp.Quantity;
                inputTemp.State = "2";
                inputTemp.OutListNO = output.OutListNO;
                inputTemp.OutBillNO = output.ID;
                inputTemp.OutSerialNO = output.SerialNO;
                inputTemp.SystemType = "19";
                inputTemp.PrivType = "06";

                inputTemp.InDate = output.OutDate;

                //by cube 金额和出库一致
                inputTemp.RetailCost = output.RetailCost;
                inputTemp.WholeSaleCost = output.WholeSaleCost;
                inputTemp.PurchaseCost = output.PurchaseCost;
                //end by

                #endregion

                if (this.InsertInput(inputTemp) == -1)
                {
                    return -1;
                }

                #endregion

                if (!(isTargetArk && !isSourceArk))         //如源科室不为药柜 且 目标科室为药柜
                {
                    #region 减少源科室库存

                    alterStore.StockDept = sourceDept;          //库存科室
                    alterStore.Quantity = -batchBackQty;         //库存变化数量
                    alterStore.TargetDept = targetDept;         //目标科室
                    alterStore.Class2Type = inputTemp.Class2Type;
                    alterStore.PrivType = inputTemp.PrivType;
                    alterStore.ID = inputTemp.ID;
                    alterStore.SerialNO = inputTemp.SerialNO;
                    if (this.SetStorage(alterStore) == -1)
                    {
                        return -1;
                    }

                    #endregion
                }
            }

            #endregion

            if (totBackQty > 0)
            {
                this.Err = backDrugInformation.Item.Name + "  在" + sourceDept.Name + "库存不足，不能进行退库操作";
                return -1;
            }

            return 1;
        }

        #endregion

        #region 基础增、删、改操作

        /// <summary>
        /// 取药品基本信息列表，可能是一条或者多条药品记录
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>药品对象数组</returns>
        private ArrayList myGetOutput(string SQLString)
        {
            ArrayList al = new ArrayList();                //用于返回药品信息的数组
            FS.HISFC.Models.Pharmacy.Output output; //返回数组中的出库实体

            this.ExecQuery(SQLString);
            try
            {
                while (this.Reader.Read())
                {
                    output = new Output();
                    try
                    {
                        #region 由结果集读取数据
                        output.StockDept.ID = this.Reader[0].ToString();                                  //0出库科室编码
                        output.ID = this.Reader[1].ToString();                                       //1出库单流水号
                        output.SerialNO = NConvert.ToInt32(this.Reader[2].ToString());               //2序号
                        output.GroupNO = NConvert.ToDecimal(this.Reader[3].ToString());                //3批次号
                        output.OutListNO = this.Reader[4].ToString();                              //4出库单据号
                        output.PrivType = this.Reader[5].ToString();                                 //5出库类型
                        output.SystemType = this.Reader[6].ToString();                               //6出库分类
                        output.InBillNO = this.Reader[7].ToString();                               //7入库单流水号
                        output.InSerialNO = NConvert.ToInt32(this.Reader[8].ToString());             //8入库单序号
                        output.InListNO = this.Reader[9].ToString();                               //9入库单据号
                        output.Item.ID = this.Reader[10].ToString();                                 //10药品编码
                        output.Item.Name = this.Reader[11].ToString();                               //11药品商品名
                        output.Item.Type.ID = this.Reader[12].ToString();                            //12药品类别
                        output.Item.Quality.ID = this.Reader[13].ToString();                         //13药品性质
                        output.Item.Specs = this.Reader[14].ToString();                              //14规格
                        output.Item.PackUnit = this.Reader[15].ToString();                           //15包装单位
                        output.Item.PackQty = NConvert.ToDecimal(this.Reader[16].ToString());        //16包装数
                        output.Item.MinUnit = this.Reader[17].ToString();                            //17最小单位
                        output.ShowState = this.Reader[18].ToString();                               //18显示的单位标记
                        output.BatchNO = this.Reader[19].ToString();                                 //19批号
                        output.ValidTime = NConvert.ToDateTime(this.Reader[20].ToString());          //20有效期
                        output.Producer.ID = this.Reader[21].ToString();                             //21生产厂家代码
                        output.Company.ID = this.Reader[22].ToString();                              //22供货单位代码
                        output.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[23].ToString());    //23零售价
                        output.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[24].ToString()); //24批发价
                        output.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[25].ToString());  //25购入价
                        output.Quantity = NConvert.ToDecimal(this.Reader[26].ToString());            //26出库量
                        output.RetailCost = NConvert.ToDecimal(this.Reader[27].ToString());          //27零售金额
                        output.WholeSaleCost = NConvert.ToDecimal(this.Reader[28].ToString());       //28批发金额
                        output.PurchaseCost = NConvert.ToDecimal(this.Reader[29].ToString());        //39购入金额
                        output.StoreQty = NConvert.ToDecimal(this.Reader[30].ToString());            //30出库后库存数量
                        output.StoreCost = NConvert.ToDecimal(this.Reader[31].ToString());           //31出库后库存总金额
                        output.SpecialFlag = this.Reader[32].ToString();                             //32特殊标记。1是，0否
                        output.State = this.Reader[33].ToString();                                   //33出库状态 0申请、1审批、2核准
                        output.Operation.ApplyQty = NConvert.ToDecimal(this.Reader[34].ToString());            //34申请数量
                        output.Operation.ApplyOper.ID = this.Reader[35].ToString();                           //35申请出库人
                        output.Operation.ApplyOper.OperTime = NConvert.ToDateTime(this.Reader[36].ToString());          //36申请出库日期
                        output.Operation.ExamQty = NConvert.ToDecimal(this.Reader[37].ToString());            //37审批数量
                        output.Operation.ExamOper.ID = this.Reader[38].ToString();                            //38审批人
                        output.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[39].ToString());           //39审批日期
                        output.Operation.ApproveOper.ID = this.Reader[40].ToString();                         //40核准人
                        output.Operation.ApproveOper.OperTime = NConvert.ToDateTime(this.Reader[41].ToString());        //41核准日期
                        output.PlaceNO = this.Reader[42].ToString();                               //42货位号
                        output.Operation.ReturnQty = NConvert.ToDecimal(this.Reader[43].ToString());          //43退库数量
                        output.DrugedBillNO = this.Reader[44].ToString();                          //44摆药单号
                        output.MedNO = this.Reader[45].ToString();                                   //45制剂序号－生产序号或检验序号
                        output.TargetDept.ID = this.Reader[46].ToString();                           //46领药单位编码
                        output.RecipeNO = this.Reader[47].ToString();                                //47处方号
                        output.SequenceNO = NConvert.ToInt32(this.Reader[48].ToString());           //48处方流水号
                        output.GetPerson = this.Reader[49].ToString();                               //49领药人
                        output.Memo = this.Reader[50].ToString();                                    //50备注
                        output.Operation.Oper.ID = this.Reader[51].ToString();                                //51操作员
                        output.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[52].ToString());           //52操作日期
                        output.IsArkManager = NConvert.ToBoolean(this.Reader[53]);
                        output.ArkOutNO = this.Reader[54].ToString();
                        if (this.Reader.FieldCount > 55)
                        {
                            output.OutDate = NConvert.ToDateTime(this.Reader[55]);
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得药品基本信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(output);
                }

                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得药品基本信息时，执行SQL语句出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 获得update或者insert出库表的传入参数数组
        /// </summary>
        /// <param name="output">出库类</param>
        /// <returns>成功返回字符串数组 失败返回null</returns>
        private string[] myGetParmOutput(FS.HISFC.Models.Pharmacy.Output output)
        {
            #region "接口说明"

            #endregion

            string arkNO = "0";
            if (output.ArkOutNO != null && output.ArkOutNO != "")
            {
                arkNO = output.ArkOutNO;
            }

            string[] strParm ={
								 output.StockDept.ID,                        //0出库科室编码
								 output.ID,                             //1出库单流水号
								 output.SerialNO.ToString(),            //2序号
								 output.GroupNO.ToString(),             //3批次号
								 output.OutListNO,                    //4出库单据号
								 output.PrivType,                       //5出库类型
								 output.SystemType,                     //6出库分类
								 output.InBillNO,                     //7入库单流水号
								 output.InSerialNO.ToString(),          //8入库单序号
								 output.InListNO,                     //9入库单据号
								 output.Item.ID,                        //10药品编码
								 output.Item.Name,                      //11药品商品名
								 output.Item.Type.ID,                   //12药品类别
								 output.Item.Quality.ID.ToString(),     //13药品性质
								 output.Item.Specs,                     //14规格
								 output.Item.PackUnit,                  //15包装单位
								 output.Item.PackQty.ToString(),        //16包装数
								 output.Item.MinUnit,                   //17最小单位
								 output.ShowState,                      //18显示的单位标记
								 output.ShowUnit,                       //19显示的单位
								 output.BatchNO,                        //20批号
								 output.ValidTime.ToString(),           //21有效期
								 output.Producer.ID,                    //22生产厂家代码
								 output.Company.ID,                     //23供货单位代码
								 output.Item.PriceCollection.RetailPrice.ToString(),    //24零售价
								 output.Item.PriceCollection.WholeSalePrice.ToString(), //25批发价
								 output.Item.PriceCollection.PurchasePrice.ToString(),  //26购入价
								 output.Quantity.ToString(),            //27出库量
                                 //by cube 2011-04-19 金额不可以在此计算，财务账务用，对四舍五入有要求
                                 //(output.Quantity * output.Item.PriceCollection.RetailPrice / output.Item.PackQty).ToString(),          //28零售金额
                                 //(output.Quantity * output.Item.PriceCollection.WholeSalePrice / output.Item.PackQty).ToString(),       //29批发金额
                                 //(output.Quantity * output.Item.PriceCollection.PurchasePrice / output.Item.PackQty).ToString(),        //30购入金额
								 output.RetailCost.ToString(),
                                 output.WholeSaleCost.ToString(),
                                 output.PurchaseCost.ToString(),
                                 //end by
                                 output.StoreQty.ToString(),            //31出库后库存数量
								 output.StoreCost.ToString(),           //32出库后库存总金额
								 output.SpecialFlag,                    //33特殊标记。1是，0否
								 output.State,                          //34出库状态 0申请、1审批、2核准
								 output.Operation.ApplyQty.ToString(),            //35申请数量
								 output.Operation.ApplyOper.ID,                  //36申请出库人
								 output.Operation.ApplyOper.OperTime.ToString(),           //37申请出库日期
								 output.Operation.ExamQty.ToString(),             //38审批数量
								 output.Operation.ExamOper.ID,                   //39审批人
								 output.Operation.ExamOper.OperTime.ToString(),            //40审批日期
								 output.Operation.ApproveOper.ID,                //41核准人
								 output.PlaceNO,                      //42货位号
								 output.Operation.ReturnQty.ToString(),           //43退库数量
								 output.DrugedBillNO,                 //44摆药单号
								 output.MedNO,                          //45制剂序号－生产序号或检验序号
								 output.TargetDept.ID,                  //46领药单位编码
								 output.RecipeNO,                       //47处方号
								 output.SequenceNO.ToString(),          //48处方流水号
								 output.GetPerson,                      //49领药人
								 output.Memo,                           //50备注
								 this.Operator.ID,                      //51操作员
                                 NConvert.ToInt32(output.IsArkManager).ToString(),
                                 arkNO,
                                 //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出/退库记录发生时间
                                 output.OutDate.ToString(),
                                 output.Summary                        //56摘要

			};
            return strParm;
        }

        /// <summary>
        /// 插入一条出库记录
        /// </summary>
        /// <param name="output">出库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertOutput(FS.HISFC.Models.Pharmacy.Output output)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertOutput", ref strSQL) == -1)
            {
                this.Err = "找不到SQL语句！Pharmacy.Item.InsertOutput";
                return -1;
            }
            try
            {
                //如果出库实体中没有出库单流水号，则取出库单流水号
                if (output.ID == "")
                {
                    output.ID = this.GetNewOutputNO();
                    if (output.ID == null) return -1;
                }

                //取参数列表
                string[] strParm = myGetParmOutput(output);
                strSQL = string.Format(strSQL, strParm);  //替换SQL语句中的参数。          
            }
            catch (Exception ex)
            {
                this.Err = "插入出库记录SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新一条出库记录
        /// </summary>
        /// <param name="output">出库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateOutput(FS.HISFC.Models.Pharmacy.Output output)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateOutput", ref strSQL) == -1)
            {
                this.Err = "找不到SQL语句！Pharmacy.Item.UpdateOutput";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmOutput(output);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新出库记录SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除出库记录
        /// </summary>
        /// <param name="ID">出库记录流水号</param>
        /// <returns>0没有删除 1成功 -1失败</returns>
        public int DeleteOutput(string ID)
        {
            string strSQL = "";
            //根据出库记录流水号删除某一条出库记录的DELETE语句
            if (this.GetSQL("Pharmacy.Item.DeleteOutput", ref strSQL) == -1)
            {
                this.Err = "找不到SQL语句！Pharmacy.Item.DeleteOutput";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID);
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.DeleteOutput";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #endregion

        public int InsertAdjustPriceInfo(AdjustPrice adjustPrice)
        {
            Adjust adjustMgr = new Adjust();
            return adjustMgr.InsertAdjustPriceInfo(adjustPrice);
        }

        public int InsertAdjustPriceDetail(AdjustPrice adjustPrice)
        {
            Adjust adjustMgr = new Adjust();
            return adjustMgr.InsertAdjustPriceDetail(adjustPrice);
        }

        public int InsertAdjustPurchasePriceInfo(AdjustPrice adjustPrice)
        {
            Adjust adjustMgr = new Adjust();
            return adjustMgr.InsertAdjustPurchasePriceInfo(adjustPrice);
        }

        #region 发票入库时修改购入价
        public int GetBackInputRecord(string deptCode, string drugCode, decimal groupCode,ref int count)
        {
            string sql = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.UpdatePurchasePrice.GetBackInputRecord", ref sql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdatePurchasePrice.GetBackInputRecord字段!";
                return -1;
            }

            sql = string.Format(sql,deptCode,drugCode,groupCode.ToString());


            this.ExecQuery(sql);
            try
            {
                while (this.Reader.Read())
                {
                    try
                    {
                        count = NConvert.ToInt32(this.Reader[0].ToString());
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得药品退库记录出错！" + ex.Message;
                        this.WriteErr();
                        return -1;
                    }
                }               
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得药品退库记录出错，执行SQL语句出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
            return 1;
        }

        public int GetOutputRecord(string deptCode, string drugCode, decimal groupCode, ref int count)
        {
            string sql = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.UpdatePurchasePrice.GetOutputRecord", ref sql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdatePurchasePrice.GetOutputRecord字段!";
                return -1;
            }

            sql = string.Format(sql, deptCode, drugCode, groupCode.ToString());


            this.ExecQuery(sql);
            try
            {
                while (this.Reader.Read())
                {
                    try
                    {
                        count = NConvert.ToInt32(this.Reader[0].ToString());
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得药品出库记录出错！" + ex.Message;
                        this.WriteErr();
                        return -1;
                    }
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得药品出库记录出错，执行SQL语句出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
            return 1;
        }

        public int GetCheckRecord(string deptCode, string drugCode, decimal groupCode, ref int count)
        {
            string sql = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.UpdatePurchasePrice.GetCheckRecord", ref sql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdatePurchasePrice.GetCheckRecord字段!";
                return -1;
            }

            sql = string.Format(sql, deptCode, drugCode, groupCode.ToString());


            this.ExecQuery(sql);
            try
            {
                while (this.Reader.Read())
                {
                    try
                    {
                        count = NConvert.ToInt32(this.Reader[0].ToString());
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得药品盘点记录出错！" + ex.Message;
                        this.WriteErr();
                        return -1;
                    }
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得药品盘点记录出错，执行SQL语句出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
            return 1;
        }

        public int UpdateStoragePurchasePrice(string deptCode, decimal drugCode, decimal groupCode, decimal newPrice)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdatePurchasePrice.UpdateStoragePrice", ref strSQL) == -1)
            {
                this.Err = "找不到SQL语句！Pharmacy.Item.UpdatePurchasePrice.UpdateStoragePrice";
                return -1;
            }
            try
            {
               
                strSQL = string.Format(strSQL, deptCode,drugCode,groupCode.ToString(),newPrice.ToString());            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新入库记录SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        public int ExamInputWithPurchasePrice(FS.HISFC.Models.Pharmacy.Input Input)
        {
            string strSQL = "";
            //审批入库信息（药库发票入库），更新入库状态为'1'			
            try
            {
                //by cube 2011-03-30 
                //不更改购入价和购入金额，否则对财务账务平衡有影响
                //更改供货公司和发票日期，这个属于扩展功能
                //decimal purchaseCost = System.Math.Round(Input.Quantity / Input.Item.PackQty * Input.Item.PriceCollection.PurchasePrice, 2);
                //取参数列表
                string[] strParm = {
									   Input.ID,								//0 入库流水号
									   Input.Operation.ExamQty.ToString(),				//1 审批数量
									   Input.Operation.ExamOper.ID,						//2 审批人
									   Input.Operation.ExamOper.OperTime.ToString(),				//3 审批日期
									   Input.InvoiceNO,							//4 发票号码
									   Input.PriceCollection.PurchasePrice.ToString(),		//5 购入价
									   Input.PurchaseCost.ToString(),					//6 购入金额
									   this.Operator.ID,						//7 操作人
									   Input.Item.ID,							//8 药品编码
									   Input.GroupNO.ToString(),				//9 批次
                                       Input.Company.ID,
                                       Input.InvoiceDate.ToString(),
				};
                //end by

                int parm;
                //更新本条入库信息
                if (this.GetSQL("SOC.Pharmacy.Item.ExamInput.WithPurchasePrice", ref strSQL) == -1)
                {
                    strSQL = @" UPDATE  PHA_COM_INPUT SET
                                        EXAM_NUM = {1},    --审批数量
                                        EXAM_OPERCODE = '{2}',  --审批人
                                        EXAM_DATE = to_date('{3}','yyyy-mm-dd HH24:mi:ss') , --审批日期（药库发票入库日期）
                                        --IN_STATE = '1',
                                        INVOICE_NO = '{4}' ,        --发票号
                                        PURCHASE_PRICE = {5} ,      --购入价
                                        PURCHASE_COST = {6},         --购入金额
                                        Company_Code='{10}',
                                        Invoice_Date =to_date('{11}','yyyy-mm-dd HH24:mi:ss'),
                                        OPER_CODE = '{7}',
                                        OPER_DATE = sysdate
                                WHERE   IN_BILL_CODE = {0}
                                AND     DRUG_CODE = '{8}'
                                AND     GROUP_CODE = '{9}'
                                AND     nvl(Pay_State,'0') = '0'        
                         ";

                    this.CacheSQL("SOC.Pharmacy.Item.ExamInput.WithPurchasePrice", strSQL);
                }
                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
                parm = this.ExecNoQuery(strSQL);
                if (parm == -1)
                {
                    this.Err = "更新药品入库审批出错！";
                    return -1;
                }
                if (parm == 0)
                {
                    this.Err = "本记录已被核准！无法再次修改审批";
                    return 0;
                }

                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "审批入库记录的SQl参数赋值出错！Pharmacy.Item.ExamInput" + ex.Message;
                this.WriteErr();
                return -1;
            }
        }

        public int SaveChangePurchasePriceLog(Input input, decimal oldPrice)
        {
            string strSQL = "";
            //审批入库信息（药库发票入库），更新入库状态为'1'			
            try
            {
                //by cube 2011-03-30 
                //不更改购入价和购入金额，否则对财务账务平衡有影响
                //更改供货公司和发票日期，这个属于扩展功能
                //decimal purchaseCost = System.Math.Round(Input.Quantity / Input.Item.PackQty * Input.Item.PriceCollection.PurchasePrice, 2);
                //取参数列表
                string[] strParm = {
                                       input.StockDept.ID,
                                       input.GroupNO.ToString(),
									   input.ID,								//0 入库流水号
                                       input.InListNO,
                                       input.Item.ID,							//8 药品编码
                                       input.Item.NameCollection.RegularName,
                                       oldPrice.ToString(),
                                       input.Item.PriceCollection.PurchasePrice.ToString(),
                                       input.Quantity.ToString(),									  
									   this.Operator.ID,						//7 操作人
									   this.GetDateTimeFromSysDateTime().ToString()
				};
                //end by

                int parm;
                //更新本条入库信息
                if (this.GetSQL("Pharmacy.Item.UpdatePurchasePrice.SaveLog", ref strSQL) == -1)
                {
                    strSQL = @"insert into PHA_COM_PURCHASEPRICE_LOG 
                                (DRUG_DEPT_CODE, 
                                GROUP_CODE,
                                IN_BILL_CODE,
                                 IN_LIST_CODE, 
                                 DRUG_CODE, 
                                 TRADE_NAME, 
                                 PURCHASE_PRICE_OLD,
                                 PURCHASE_PRICE_NEW, 
                                 IN_NUM, 
                                 OPER_CODE,
                                 OPER_DATE)
                                values ('{0}',--科室编码
                                 '{1}', --批次号
                                  {2}, --入库单流水号
                                 '{3}', --入库单号
                                 '{4}', --药品编码
                                 '{5}', --药品名称
                                 {6}, --原购入价
                                 {7}, --新购入价
                                 {8}, --入库数量
                                 '{9}', --操作员
                                 to_date('{10}', 'yyyy-mm-dd hh24:mi:ss') --操作时间
                                 )        
                         ";

                    this.CacheSQL("Pharmacy.Item.UpdatePurchasePrice.SaveLog", strSQL);
                }
                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
                parm = this.ExecNoQuery(strSQL);
                if (parm == -1)
                {
                    this.Err = "保存购入价修改记录失败！";
                    return -1;
                }               

                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "保存购入价修改记录的SQl参数赋值出错！Pharmacy.Item.UpdatePurchasePrice.SaveLog" + ex.Message;
                this.WriteErr();
                return -1;
            }
        }
        #endregion
    }
}
