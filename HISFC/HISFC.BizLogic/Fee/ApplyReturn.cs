using System;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Fee.Inpatient;
using FS.HISFC.Models.RADT;
using System.Collections.Generic;
using System.Data;

namespace FS.HISFC.BizLogic.Fee
{
    /// <summary>
    /// ReturnApply<br></br>
    /// [功能描述: 退费申请，核准业务类]<br></br>
    /// [创 建 者: 王宇]<br></br>
    /// [创建时间: 2006-10-01]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class ReturnApply : FS.FrameWork.Management.Database
    {
        #region 私有方法

        #region 退费申请       

        /// <summary>
        /// 根据SQL语句获得退费申请信息
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="args">SQL语句的参数</param>
        /// <returns>成功:退费申请信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        private ArrayList QueryReturnApplysBySql(string sql, params string[] args)
        {
            //执行SQL语句失败
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList returnApplys = new ArrayList();

            try
            {
                FS.HISFC.Models.Fee.ReturnApply returnApply;//临时退药申请实体

                //循环读取数据
                while (this.Reader.Read())
                {
                    returnApply = new FS.HISFC.Models.Fee.ReturnApply();

                    returnApply.ID = this.Reader[0].ToString();//申请流水号
                    returnApply.ApplyBillNO = this.Reader[1].ToString();//申请单据号
                    returnApply.Patient.ID = this.Reader[2].ToString();//住院流水号
                    returnApply.Name = this.Reader[3].ToString();//患者姓名
                    returnApply.IsBaby = NConvert.ToBoolean(this.Reader[4].ToString());//婴儿序号
                    ((FS.HISFC.Models.RADT.PatientInfo)returnApply.Patient).PVisit.PatientLocation.Dept.ID = this.Reader[5].ToString();//患者所在科室
                    ((FS.HISFC.Models.RADT.PatientInfo)returnApply.Patient).PVisit.PatientLocation.NurseCell.ID = this.Reader[6].ToString();//所在病区
                    //returnApply.Item.IsPharmacy = NConvert.ToBoolean(this.Reader[7].ToString());//药品标志1药品/0非药
                    returnApply.Item.ItemType = (HISFC.Models.Base.EnumItemType)NConvert.ToInt32(this.Reader[7].ToString());//药品标志1药品/0非药
                    //如果是药品则Item创建为药品实体
                    if (returnApply.Item.ItemType == HISFC.Models.Base.EnumItemType.Drug)
                    {
                        returnApply.Item = new FS.HISFC.Models.Pharmacy.Item();
                    }
                    else if (returnApply.Item.ItemType == HISFC.Models.Base.EnumItemType.UnDrug)//否则创建为非药品实体
                    {
                        returnApply.Item = new FS.HISFC.Models.Fee.Item.Undrug();
                    }
                    else
                    {
                        returnApply.Item = new FS.HISFC.Models.FeeStuff.MaterialItem();
                    }
                    //重新赋值
                    returnApply.Item.ItemType = (HISFC.Models.Base.EnumItemType)NConvert.ToInt32(this.Reader[7].ToString());//药品标志1药品/0非药
                    returnApply.Item.ID = this.Reader[8].ToString();//项目编码
                    returnApply.Item.Name = this.Reader[9].ToString();//项目名称
                    returnApply.Item.Specs = this.Reader[10].ToString();//规格
                    returnApply.Item.Price = NConvert.ToDecimal(this.Reader[11].ToString());//零售价
                    returnApply.Item.Qty = NConvert.ToDecimal(this.Reader[12].ToString());//申请退药数量（乘以付数后的总数量）
                    returnApply.Days = NConvert.ToDecimal(this.Reader[13].ToString());//付数
                    returnApply.Item.PriceUnit = this.Reader[14].ToString(); //计价单位
                    returnApply.ExecOper.Dept.ID = this.Reader[15].ToString();//执行科室
                    returnApply.Oper.ID = this.Reader[16].ToString();//操作员编码
                    returnApply.Oper.OperTime = NConvert.ToDateTime(this.Reader[17].ToString());//操作时间
                    returnApply.Oper.Dept.ID = this.Reader[18].ToString();//操作员所在科室
                    returnApply.RecipeOper.Dept.ID = returnApply.Oper.Dept.ID;
                    returnApply.RecipeNO = this.Reader[19].ToString();//对应收费明细处方号
                    returnApply.SequenceNO = NConvert.ToInt32(this.Reader[20].ToString());//对应处方内流水号
                    returnApply.ConfirmBillNO = this.Reader[21].ToString();
                    returnApply.IsConfirmed = NConvert.ToBoolean(Reader[22].ToString());//退药确认标识 0未确认/1确认
                    returnApply.ConfirmOper.Dept.ID = this.Reader[23].ToString();//确认科室代码
                    returnApply.ConfirmOper.ID = this.Reader[24].ToString();//确认人编码
                    returnApply.ConfirmOper.OperTime = NConvert.ToDateTime(this.Reader[25].ToString());//确认时间
                    returnApply.CancelType = (FS.HISFC.Models.Base.CancelTypes)NConvert.ToInt32(Reader[26].ToString());//退费标识 0未退费/1退费
                    returnApply.CancelOper.ID = this.Reader[27].ToString();//退费确认人
                    returnApply.CancelOper.OperTime = NConvert.ToDateTime(this.Reader[28].ToString());//退费确认时间
                    returnApply.FeePack = this.Reader[29].ToString();
                    returnApply.Item.PackQty = NConvert.ToDecimal(this.Reader[30].ToString());
                    returnApply.UndrugComb.ID = this.Reader[31].ToString();
                    returnApply.UndrugComb.Name = this.Reader[32].ToString();

                    if (this.Reader.FieldCount > 33)
                    {
                        returnApply.Patient.PID.CardNO = this.Reader[33].ToString();
                    }
                    if (this.Reader.FieldCount > 34)
                    {
                        returnApply.Order.ID = this.Reader[34].ToString();
                    }
                    if (this.Reader.FieldCount > 35)
                    {
                        returnApply.ExecOrder.ID = this.Reader[35].ToString();
                    }
                    if (this.Reader.FieldCount > 36)
                    {
                        returnApply.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[36].ToString());
                    }
                    if (this.Reader.FieldCount > 37)
                    {
                        returnApply.UndrugComb.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[37].ToString());
                    }
                    returnApplys.Add(returnApply);
                }//循环结束

                this.Reader.Close();
            }//抛出错误
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return returnApplys;
        }
        /// <summary>
        /// 获得update或者insert退费申请的传入参数数组
        /// </summary>
        /// <param name="returnApply">退费申请实体</param>
        /// <returns>参数数组</returns>
        private string[] GetReturnApplyParams(FS.HISFC.Models.Fee.ReturnApply returnApply)
        {
            string deptCode = string.Empty;
            string nurseCellCode = string.Empty;

            if (returnApply.Patient is FS.HISFC.Models.RADT.PatientInfo)
            {
                deptCode = ((FS.HISFC.Models.RADT.PatientInfo)returnApply.Patient).PVisit.PatientLocation.Dept.ID;
                nurseCellCode = ((FS.HISFC.Models.RADT.PatientInfo)returnApply.Patient).PVisit.PatientLocation.NurseCell.ID;
            }
            else if (returnApply.Patient is FS.HISFC.Models.Registration.Register)
            {
                deptCode = ((FS.HISFC.Models.Registration.Register)returnApply.Patient).DoctorInfo.Templet.Dept.ID;
                nurseCellCode = string.Empty;
            }

            if (string.IsNullOrEmpty(deptCode) || string.IsNullOrEmpty(nurseCellCode))// {15CDA661-3D42-4c15-A32B-F88CC1CD7907}
            {

                string sql = string.Empty;
                if (returnApply.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    sql = @"select INHOS_DEPTCODE,NURSE_CELL_CODE from fin_ipb_itemlist h 
                        where h.recipe_no = '{0}' 
                        and h.item_code = '{1}'
                        and h.sequence_no = '{2}'";
                }
                else if (returnApply.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    sql = @"select INHOS_DEPTCODE,NURSE_CELL_CODE from fin_ipb_medicinelist o 
                        where o.recipe_no = '{0}' 
                        and o.DRUG_CODE = '{1}'
                        and o.sequence_no = '{2}'";
                }
                sql = string.Format(sql, returnApply.RecipeNO, returnApply.Item.ID, returnApply.SequenceNO.ToString());
                DataSet dsResult = null;
                if (this.ExecQuery(sql, ref dsResult) != -1)
                {
                    DataTable dtResult = dsResult.Tables[0];
                    foreach (DataRow dr in dtResult.Rows)
                    {
                        deptCode = dr["INHOS_DEPTCODE"].ToString();
                        nurseCellCode = dr["NURSE_CELL_CODE"].ToString();
                    }
                }
            }
            string[] args =
			{
				//returnApply.ID ,                   //0申请流水号
                this.GetSequence("Fee.ApplyReturn.CancelApplyNO"),
				returnApply.ApplyBillNO,              //1申请单据号
				returnApply.Patient.ID,           //2住院流水号
				returnApply.Patient.Name,                  //3患者姓名
				NConvert.ToInt32(returnApply.IsBaby).ToString(),//4婴儿序号
				deptCode,               //5患者所在科室
				nurseCellCode,      //6所在病区
				//FS.FrameWork.Function.NConvert.ToInt32(returnApply.Item.IsPharmacy).ToString(),              //7药品标志,1药品/2非药
                ((int)(returnApply.Item.ItemType)).ToString(),              //7药品标志,1药品/2非药
				returnApply.Item.ID,               //8项目编码
				returnApply.Item.Name,             //9项目名称
				returnApply.Item.Specs,            //10规格
				returnApply.Item.Price.ToString(), //11零售价
				returnApply.Item.Qty.ToString(),//12申请退药数量（乘以付数后的总数量）
				returnApply.Days.ToString(),       //13付数
				returnApply.Item.PriceUnit,        //14计价单位
				returnApply.ExecOper.Dept.ID,              //15执行科室
				this.Operator.ID,                  //16操作员编码
				returnApply.Oper.OperTime.ToString(),   //17操作时间
				returnApply.Oper.Dept.ID,              //18操作员所在科室
				returnApply.RecipeNO,              //19对应收费明细处方号
				returnApply.SequenceNO.ToString(), //20对应处方内流水号
                returnApply.ConfirmBillNO,//确认单号
				FS.FrameWork.Function.NConvert.ToInt32(returnApply.IsConfirmed).ToString(),           //21退药确认标识 0未确认/1确认
				returnApply.ConfirmOper.Dept.ID,           //22确认科室代码
				returnApply.ConfirmOper.ID,           //23确认人编码
				returnApply.ConfirmOper.OperTime.ToString(),//24确认时间
				((int)returnApply.CancelType).ToString(),            //25退费标识 0未退费/1退费
				returnApply.CancelOper.ID,            //26退费确认人
				returnApply.CancelOper.OperTime.ToString(),  //27退费确认时间
                returnApply.FeePack,
                returnApply.Item.PackQty.ToString(),
                returnApply.UndrugComb.ID,
                returnApply.UndrugComb.Name,
                //returnApply.StockNo //物资库存序号
                returnApply.Patient.PID.CardNO,
                returnApply.Order.ID,
                returnApply.ExecOrder.ID,
                returnApply.FeeOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss")
			};

            return args;
        }

        /// <summary>
        /// 获得退费申请SELECT语句
        /// </summary>
        /// <returns>成功:获得退费申请SELECT语句 失败:null</returns>
        private string GetReturnApplySelectSql()
        {
            string sql = string.Empty;

            //取SQL语句
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetApplyReturnList", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.ApplyReturn.GetApplyReturnList的SQL语句!";

                return null;
            }

            return sql;
        }

        /// <summary>
        /// 获得退费申请信息
        /// </summary>
        /// <param name="whereIndex">where条件</param>
        /// <param name="args">参数</param>
        /// <returns>成功: 退费申请信息 失败: null 没有找到数据ArrayList.Count = 0</returns>
        private ArrayList QueryReturnApplys(string whereIndex, params string[] args)
        {
            string select = string.Empty;
            string where = string.Empty;

            select = this.GetReturnApplySelectSql();

            if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            {
                this.Err = "没有找到索引为:" + whereIndex + "的SQL语句";

                return null;
            }

            return this.QueryReturnApplysBySql(select + " " + where, args);
        }

        #endregion

        #region 物资退费申请

        /// <summary>
        /// 获得update或者insert物资退费申请的传入参数数组
        /// </summary>
        /// <param name="returnApplyMet"></param>
        /// <returns></returns>
        private string[] GetReturnApplyMetParams(FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet)
        {
            string[] args =
			{
                returnApplyMet.ApplyNo, //APPLY_NO 申请流水号
                returnApplyMet.OutNo,//OUT_NO 出库单流水号
                returnApplyMet.StockNo,//STOCK_NO 库存序号
                returnApplyMet.RecipeNo,//RECIPE_NO 处方号
                returnApplyMet.SequenceNo,//SEQUENCE_NO 处方内项目流水号
                returnApplyMet.Item.ID,//ITEM_CODE 物品编码
                returnApplyMet.Item.Name,//ITEM_NAME 物品名称
                returnApplyMet.Item.Specs,//SPECS	规格
                returnApplyMet.Item.PriceUnit,//STAT_UNIT 计量单位
                returnApplyMet.Item.Price.ToString(),//SALE_PRICE 零售单价
                returnApplyMet.Item.Qty.ToString(),//OUT_NUM 出库数量
                ((int)returnApplyMet.ApplyFlag).ToString()//CANCELFLAF 确认标识（0申请，1取消，2确认）
			};

            return args;
        }

        /// <summary>
        /// 获得物资退费申请SELECT语句
        /// </summary>
        /// <returns>成功:获得退费申请SELECT语句 失败:null</returns>
        private string GetReturnApplyMetSelectSql()
        {
            string sql = string.Empty;

            //取SQL语句
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetApplyReturnMetList", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.ApplyReturn.GetApplyReturnMetList的SQL语句!";

                return null;
            }

            return sql;
        }

        /// <summary>
        /// 根据SQL语句获得退费申请信息
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="args">SQL语句的参数</param>
        /// <returns>成功:退费申请信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        private List<HISFC.Models.Fee.ReturnApplyMet> QueryReturnApplyMetBySql(string sql, params string[] args)
        {
            //执行SQL语句失败
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            //ArrayList returnApplyMetList = new ArrayList();
            List<HISFC.Models.Fee.ReturnApplyMet> returnApplyMetList = new List<FS.HISFC.Models.Fee.ReturnApplyMet>();
            try
            {
                FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet;//临时退药申请实体

                //循环读取数据
                while (this.Reader.Read())
                {
                    returnApplyMet = new FS.HISFC.Models.Fee.ReturnApplyMet();

                    returnApplyMet.ApplyNo = this.Reader[0].ToString();//APPLY_NO 申请流水号
                    returnApplyMet.OutNo = this.Reader[1].ToString();//OUT_NO 出库单流水号
                    returnApplyMet.StockNo = this.Reader[2].ToString();//STOCK_NO 库存序号
                    returnApplyMet.RecipeNo = this.Reader[3].ToString();//RECIPE_NO 处方号
                    returnApplyMet.SequenceNo = this.Reader[4].ToString();//SEQUENCE_NO 处方内项目流水号
                    returnApplyMet.Item.ID = this.Reader[5].ToString();//ITEM_CODE 物品编码
                    returnApplyMet.Item.Name = this.Reader[6].ToString();//ITEM_NAME 物品名称
                    returnApplyMet.Item.Specs = this.Reader[7].ToString();//SPECS	规格
                    returnApplyMet.Item.PriceUnit = this.Reader[8].ToString();//STAT_UNIT 计量单位
                    returnApplyMet.Item.Price = FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());//SALE_PRICE 零售单价
                    returnApplyMet.Item.Qty = FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());//OUT_NUM 出库数量
                    returnApplyMet.ApplyFlag = (HISFC.Models.Base.CancelTypes)(FrameWork.Function.NConvert.ToInt32(this.Reader[11].ToString()));//CANCELFLAF 确认标识（0申请，1取消，2确认）

                    returnApplyMetList.Add(returnApplyMet);
                }//循环结束

                this.Reader.Close();
            }//抛出错误
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return returnApplyMetList;
        }

        /// <summary>
        /// 根据SQL语句获得退费申请信息
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="args">SQL语句的参数</param>
        /// <returns>成功:退费申请信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        private List<HISFC.Models.FeeStuff.Output> QueryQueryOutPutFromApplyBySql(string sql, params string[] args)
        {
            //执行SQL语句失败
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            //ArrayList returnApplyMetList = new ArrayList();
            List<HISFC.Models.FeeStuff.Output> list = new List<HISFC.Models.FeeStuff.Output>();
            try
            {
                HISFC.Models.FeeStuff.Output outItem;//临时退药申请实体

                //循环读取数据
                while (this.Reader.Read())
                {
                    outItem = new FS.HISFC.Models.FeeStuff.Output();
                    outItem.ID = this.Reader[1].ToString();//OUT_NO 出库单流水号
                    outItem.StoreBase.StockNO = this.Reader[2].ToString();//STOCK_NO 库存序号
                    outItem.RecipeNO = this.Reader[3].ToString();//RECIPE_NO 处方号
                    outItem.SequenceNO = NConvert.ToInt32(this.Reader[4]);//SEQUENCE_NO 处方内项目流水号
                    outItem.StoreBase.Item.ID = this.Reader[5].ToString();//ITEM_CODE 物品编码
                    outItem.StoreBase.Item.Name = this.Reader[6].ToString();//ITEM_NAME 物品名称
                    outItem.StoreBase.Item.Specs = this.Reader[7].ToString();//SPECS	规格
                    outItem.StoreBase.Item.PriceUnit = this.Reader[8].ToString();//STAT_UNIT 计量单位
                    outItem.StoreBase.Item.Price = FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());//SALE_PRICE 零售单价
                    outItem.StoreBase.Item.Qty = FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());//OUT_NUM 出库数量
                    list.Add(outItem);
                }//循环结束

                this.Reader.Close();
            }//抛出错误
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return list;
        }
        /// <summary>
        /// 根据物资退费申请信息获得物资出库信息
        /// </summary>
        /// <param name="whereIndex">where条件</param>
        /// <param name="args">参数</param>
        /// <returns>成功: 退费申请信息 失败: null 没有找到数据ArrayList.Count = 0</returns>
        private List<HISFC.Models.FeeStuff.Output> QueryOutPutByApply(string whereIndex, params string[] args)
        {
            string select = string.Empty;
            string where = string.Empty;

            select = this.GetReturnApplyMetSelectSql();

            if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            {
                this.Err = "没有找到索引为:" + whereIndex + "的SQL语句";

                return null;
            }

            return this.QueryQueryOutPutFromApplyBySql(select + " " + where, args);
        }

        /// <summary>
        /// 获得物资退费申请信息
        /// </summary>
        /// <param name="whereIndex">where条件</param>
        /// <param name="args">参数</param>
        /// <returns>成功: 退费申请信息 失败: null 没有找到数据ArrayList.Count = 0</returns>
        private List<HISFC.Models.Fee.ReturnApplyMet> QueryReturnApplyMet(string whereIndex, params string[] args)
        {
            string select = string.Empty;
            string where = string.Empty;

            select = this.GetReturnApplyMetSelectSql();

            if (this.Sql.GetCommonSql(whereIndex, ref where) == -1)
            {
                this.Err = "没有找到索引为:" + whereIndex + "的SQL语句";

                return null;
            }

            return this.QueryReturnApplyMetBySql(select + " " + where, args);
        }

        #endregion

        #endregion

        #region 公有方法

        #region 退费申请

        /// <summary>
        /// 根据发票号获取待确认(退药确认/退费确认)信息
        /// </summary>
        ///  <param name="inpatientNo">门诊流水号</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="isConfirm">是否退药确认/医技退费确认</param>
        /// <param name="isCharged">是否退费确认</param>
        /// <param name="drugFlag">药品标记 1 药品	0 非药品 A 全部</param>
        /// <returns>成功返回所有待确认数据数组 失败返回null</returns>
        public ArrayList GetList(string inpatientNo, string invoiceNo, bool isConfirm, bool isCharged, string drugFlag)
        {
            ArrayList al = new ArrayList();

            //string strWhere = ""; //Where条件
            string chargeFlag = "0"; //退费状态：0未退费（申请），1已退费（核准）
            string confrmFlag = "0"; //退药状态：0未确认 1 已确认

            //设置申请状态参数
            if (isConfirm)
                confrmFlag = "1";
            else
                confrmFlag = "0";
            if (isCharged)
                chargeFlag = "1";
            else
                chargeFlag = "0";

            //取退费申请数据			
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where.ByInvoice", inpatientNo, invoiceNo, confrmFlag, chargeFlag, drugFlag);
        }

        /// <summary>
        /// 取退费申请流水号
        /// </summary>
        /// <returns>"-1"出错，oterhs 成功</returns>
        public string GetReturnApplySequence()
        {
            return this.GetSequence("Fee.ApplyReturn.GetApplyReturnID");
        }

        /// <summary>
        /// 获取退费申请单号
        /// </summary>
        /// <returns></returns>
        public string GetReturnApplyBillCode()
        {
            return this.GetSequence("Fee.ApplyReturn.GetBillCode");
        }

        /// <summary>
        /// 根据患者住院流水号，取此患者的退费申请数据
        /// </summary>
        /// <param name="inpatientNO">患者住院流水号</param>
        /// <returns>成功:退费申请数组 失败: null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryReturnApplys(string inpatientNO)
        {
            return this.QueryReturnApplys(string.Empty, inpatientNO);
        }

        /// <summary>
        /// 获得单条退费申请信息
        /// </summary>
        /// <param name="inpatientNO">患者流水号</param>
        /// <param name="applySequence">申请流水号</param>
        /// <returns>成功 退费申请实体 失败 null</returns>
        public FS.HISFC.Models.Fee.ReturnApply GetReturnApplyByApplySequence(string inpatientNO, string applySequence)
        {
            ArrayList tempList = this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnWhere", inpatientNO, applySequence);

            if (tempList == null || tempList.Count == 0)
            {
                return null;
            }
            else
            {
                return tempList[0] as FS.HISFC.Models.Fee.ReturnApply;
            }
        }

        /// <summary>
        /// 根据患者住院流水号，取此患者的退费申请数据,附加条件是是否被确认
        /// </summary>
        /// <param name="inpatientNO">患者住院流水号</param>
        /// <param name="isCharged">是否被确认的数据</param>
        /// <returns>成功:退费申请数组 失败: null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryReturnApplys(string inpatientNO, bool isCharged)
        {
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where", inpatientNO, NConvert.ToInt32(isCharged).ToString());
        }

        /// <summary>
        /// 根据患者住院流水号，取此患者的退费申请数据
        /// </summary>
        /// <param name="inpatientNO">患者住院流水号</param>
        /// <param name="deptCode">退费操作员所在科室</param>
        /// <param name="isCharged">是否被确认数据</param>
        /// <param name="isPhamacy">是否药品</param>
        /// <returns>成功:退费申请数组 失败: null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryReturnApplys(string inpatientNO, string deptCode, bool isCharged, bool isPhamacy)
        {
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where.isUndrug", inpatientNO, deptCode, NConvert.ToInt32(isCharged).ToString(),
                NConvert.ToInt32(isPhamacy).ToString());
        }

        /// <summary>
        /// 根据患者住院流水号，取此患者的退费申请数据
        /// </summary>
        /// <param name="inpatientNO">患者住院流水号</param>
        /// <param name="deptCode">退费操作员所在科室</param>
        /// <param name="isCharged">是否被确认数据</param>
        /// <param name="ItemType">项目类别</param>
        /// <returns>成功:退费申请数组 失败: null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryReturnApplys(string inpatientNO, bool isCharged, HISFC.Models.Base.EnumItemType ItemType)
        {
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where.isUndrug", inpatientNO, "AAAA", NConvert.ToInt32(isCharged).ToString(),
                ((int)ItemType).ToString());
        }

        /// <summary>
        /// 根据患者住院流水号，取此患者的退费申请数据
        /// </summary>
        /// <param name="inpatientNO">患者住院流水号</param>
        /// <param name="isCharged">是否被确认数据</param>
        /// <param name="isPhamacy">是否非药品</param>
        /// <returns>成功返回退费申请数组 失败返回null</returns>
        public ArrayList QueryReturnApplys(string inpatientNO, bool isCharged,bool isPhamacy)
        {
            return this.QueryReturnApplys(inpatientNO, "AAAA", isCharged, isPhamacy);
        }

        /// <summary>
        /// 根据患者住院流水号、是否确认 、是否发药取患者药品退费申请数据
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="isCharged">是否退费确认</param>
        /// <param name="isConfirm">是否发药</param>
        /// <returns>成功:退费申请数组 失败: null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryDrugReturnApplys(string inpatientNO, bool isCharged, bool isConfirm)
        {
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where.DrugList", inpatientNO, NConvert.ToInt32(isCharged).ToString(),
                NConvert.ToInt32(isConfirm).ToString());
        }

        /// <summary>
        /// 插入一条退费申请记录
        /// </summary>
        /// <param name="returnApply">退费申请实体</param>
        /// <returns>成功: 1 失败: -1 没有插入数据 : 0</returns>
        public int InsertReturnApply(FS.HISFC.Models.Fee.ReturnApply returnApply)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.InsertApplyReturn", this.GetReturnApplyParams(returnApply));
        }

        /// <summary>
        /// 取消退费申请 置状态为无效 2
        /// </summary>
        /// <param name="applySequence">申请流水号</param>
        /// <param name="operCode">取消人</param>
        /// <returns>成功1 失败－1 无记录 0</returns>
        public int CancelReturnApply(string applySequence, string operCode)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.CancelApplyReturn", applySequence, operCode);
        }

        /// <summary>
        /// 删除一条退费申请记录
        /// </summary>
        /// <param name="applySequence">退费申请流水号</param>
        /// <returns>成功1 失败－1 无记录 0</returns>
        public int DeleteReturnApply(string applySequence)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.DeleteApplyReturn", applySequence);
        }

        /// <summary>
        /// 确认退费申请
        /// </summary>
        /// <param name="returnApply">退费申请实体</param>
        /// <returns>成功1 失败－1 无记录 0</returns>
        public int ConfirmApply(FS.HISFC.Models.Fee.ReturnApply returnApply)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.ConfirmApply", returnApply.ID, returnApply.ApplyBillNO, ((int)returnApply.CancelType).ToString(),
               returnApply.ConfirmOper.ID, returnApply.ConfirmOper.OperTime.ToString(), returnApply.Item.ID);
            
            
        }

        /// <summary>
        /// 查询一段时间内申请退费的患者
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="confirmFlag">确认标记</param>
        /// <returns>成功:退费申请的患者信息数组 失败: null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryAppliedPatientsByTime(string beginTime, string endTime, string confirmFlag)
        {

            string sql = string.Empty;//查询申请患者的SQL语句

            ArrayList patients = new ArrayList();

            if (this.Sql.GetCommonSql("Fee.ApplyReturn.QueryPatients", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.ApplyReturn.QueryPatients的SQL语句!";

                return null;
            }

            try
            {
                sql = string.Format(sql, beginTime.ToString(), endTime.ToString(), confirmFlag);
            }
            catch (Exception e)
            {
                this.Err = e.Message;

                return null;
            }

            try
            {
                //执行SQL语句
                if (this.ExecQuery(sql) == -1)
                {
                    return null;
                }

                //循环读取数据
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject patient = new FS.FrameWork.Models.NeuObject();

                    patient.ID = this.Reader[2].ToString();//住院流水号
                    patient.Name = this.Reader[3].ToString();//姓名
                    patient.Memo = this.Reader[4].ToString();//护士站代码

                    patients.Add(patient);
                }//循环结束

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return patients;
        }

        /// <summary>
        /// 退费申请
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <param name="feeItemList">费用明细</param>
        /// <param name="operTime">操作时间</param>
        /// <returns>成功: 1 失败: -1 没有插入申请信息: 0</returns>
        public int Apply(PatientInfo patient, FeeItemList feeItemList, DateTime operTime)
        {
            //定义退费申请实体
            FS.HISFC.Models.Fee.ReturnApply applyReturn = new FS.HISFC.Models.Fee.ReturnApply();

            //将费用实体对应成退费申请实体
            applyReturn.Patient = patient.Clone();
            applyReturn.IsBaby = feeItemList.IsBaby;//婴儿标记
            applyReturn.Item = feeItemList.Item;//项目编码
            applyReturn.Days = feeItemList.Days;//付数
            applyReturn.ExecOper.Dept = feeItemList.ExecOper.Dept;//执行科室
            applyReturn.ExecOrder.ID = !string.IsNullOrEmpty(feeItemList.ExecOrder.ID)?feeItemList.ExecOrder.ID:feeItemList.FeeOper.OperTime.ToString();//执行档流水号或收费时间
            applyReturn.CancelOper.OperTime = operTime;//操作时间
            applyReturn.CancelOper.Dept = ((FS.HISFC.Models.Base.Employee)this.Operator).Dept;//操作员所在科室
            applyReturn.RecipeNO = feeItemList.RecipeNO;//对应收费明细处方号
            applyReturn.SequenceNO = feeItemList.SequenceNO;//对应处方内流水号
            applyReturn.IsConfirmed = feeItemList.IsConfirmed;//退药确认
            //退费申请确认单据号
            applyReturn.ApplyBillNO = feeItemList.User02;//确认单号
            applyReturn.ConfirmOper.Dept = ((FS.HISFC.Models.Base.Employee)this.Operator).Dept;//确认科室代码
            applyReturn.ConfirmOper.ID = this.Operator.ID;//确认人编码
            applyReturn.ConfirmOper.OperTime = operTime;//确认时间
            applyReturn.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled; //.Valid;//退费标识
            //applyReturn.Item.IsPharmacy = feeItemList.Item.IsPharmacy;
            applyReturn.Item.ItemType = feeItemList.Item.ItemType;
            applyReturn.UndrugComb = feeItemList.UndrugComb;
            //物资信息
            applyReturn.MateList = feeItemList.MateList;
            //医嘱信息
            applyReturn.Order.ID = feeItemList.Order.ID;
            applyReturn.ExecOrder.ID = feeItemList.ExecOrder.ID;
            applyReturn.FeeOper.OperTime = feeItemList.FeeOper.OperTime;

            int resultValue = 0;
            //退费申请表
            resultValue = this.InsertReturnApply(applyReturn);
            if (resultValue < 0) return -1;
            //物资申请明细表
            if (applyReturn.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                resultValue = this.InsertReturnApplyMet(applyReturn);
                if (resultValue < 0) return -1;
            }
            return 1;
        }

        /// <summary>
        /// 根据病历号、处方号、处方流水号、确认标记查询退费申请信息
        /// </summary>
        /// <param name="cardNO">病历号</param>
        /// <param name="recipeNO">处方号</param>
        /// <param name="seqenceNO">处方流水号</param>
        /// <param name="confirmFlag">确认标记</param>
        /// <returns></returns>
        public ArrayList QueryReturnApplys(string cardNO, string recipeNO, int seqenceNO, string confirmFlag)
        {
            string sql = string.Empty;

            //取SQL语句
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetDirectFeeApplyReturnList", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.ApplyReturn.GetApplyReturnListByCardNO的SQL语句!";

                return null;
            }
            return QueryReturnApplysBySql(sql, cardNO,
                                         recipeNO,
                                         seqenceNO.ToString(),
                                         confirmFlag);
        }

        /// <summary>
        /// 根据处方号和处方内流水号查询退费申请，用于医生站判断药房是否已经做过退费申请了。
        /// create by lh 10-05-24
        /// {08CC9125-1F28-4f5d-BF05-517108088111}
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <param name="sequenceNo"></param>
        /// <returns></returns>
        public ArrayList QueryReturnApplysByRecipeNoSequenceNo(string inpatientNO, string recipeNo, string sequenceNo)
        {
            return this.QueryReturnApplys("Fee.ApplyReturn.GetApplyReturnList.Where.recipe", inpatientNO, recipeNo, sequenceNo);
        }

        /// <summary>
        /// 获取退费单据列表
        /// </summary>
        /// <param name="inPatientNo">住院流水号</param>
        /// <param name="deptCode">退费科室</param>
        /// <param name="dtBegin">查询起始时间</param>
        /// <param name="dtEnd">查询终止时间</param>
        /// <param name="chargeFlag">收费标志</param>
        /// <returns>成功返回nueobject数组 失败返回null</returns>
        public ArrayList GetList(string inPatientNo, string deptCode, System.DateTime dtBegin, System.DateTime dtEnd, string chargeFlag)
        {
            string strSQL = "";
            ArrayList al = new ArrayList();

            //取SQL语句
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetBillList", ref strSQL) == -1)
            {
                this.Err = "没有找到Fee.ApplyReturn.GetBillList字段!";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, inPatientNo, deptCode, dtBegin.ToString(), dtEnd.ToString(), chargeFlag);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Fee.ApplyReturn.GetBillList" + ex.Message;
                return null;
            }
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取退药申请时出错：" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject info;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.ID = this.Reader[0].ToString();		//退费单据号
                    info.Memo = this.Reader[1].ToString();		//药品标志 1 药品 0 非药品
                    info.User01 = this.Reader[2].ToString();	//退费确认标志 1 确认 0 未确认
                    info.User02 = this.Reader[3].ToString();	//备注 送住院处 或送药房
                    al.Add(info);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得退库申请时出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return al;
        }
        #endregion

        #region 物资退费申请

        /// <summary>
        /// 插入一条物资退费申请记录
        /// </summary>
        /// <param name="returnApply">退费申请实体</param>
        /// <returns>成功: 1 失败: -1 没有插入数据 : 0</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public int InsertReturnApplyMet(FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.InsertApplyReturnMet", this.GetReturnApplyMetParams(returnApplyMet));
        }

        /// <summary>
        /// 根据费用信息插入物资退费申请记录
        /// </summary>
        /// <param name="feeItemList">费用信息</param>
        /// <returns>1 成功 -1失败</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public int InsertReturnApplyMet(FS.HISFC.Models.Fee.ReturnApply applyItem)
        {

            List<FS.HISFC.Models.Fee.ReturnApplyMet> list = GetApplyMetItem(applyItem);
            if (list.Count == 0) return 1;
            int resultValue = 0;
            foreach (FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet in list)
            {
                resultValue = InsertReturnApplyMet(returnApplyMet);
                if (resultValue < 0)
                    return -1;
            }
            return 1;
            
        }

        /// <summary>
        /// 根据费用信息生成物资退费申请实体集合
        /// </summary>
        /// <param name="feeItemList">费用信息</param>
        /// <returns>退费申请实体集合</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        private List<FS.HISFC.Models.Fee.ReturnApplyMet> GetApplyMetItem(FS.HISFC.Models.Fee.ReturnApply applyItem)
        {
            List<FS.HISFC.Models.Fee.ReturnApplyMet> list = new List<FS.HISFC.Models.Fee.ReturnApplyMet>();
            FS.HISFC.Models.Fee.ReturnApplyMet item = null;
            string recipeNo = applyItem.RecipeNO;
            string sequenceNo = applyItem.SequenceNO.ToString();
            foreach (HISFC.Models.FeeStuff.Output outItem in applyItem.MateList)
            {
                item = new FS.HISFC.Models.Fee.ReturnApplyMet();
                item.Item = outItem.StoreBase.Item;
                item.Item.Qty = outItem.ReturnApplyNum;
                item.RecipeNo = recipeNo;
                item.SequenceNo = sequenceNo;
                item.StockNo = outItem.StoreBase.StockNO;
                item.OutNo = outItem.ID;
                item.ApplyNo = applyItem.ID;
                list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// 更新一条物资退费申请记录
        /// </summary>
        /// <param name="returnApply">退费申请实体</param>
        /// <returns>成功: 1 失败: -1 没有插入数据 : 0</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public int UpdateReturnApplyMet(FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.UpdateApplyReturnMet", this.GetReturnApplyMetParams(returnApplyMet));
        }

        /// <summary>
        /// 更新物资退费申请状态
        /// </summary>
        /// <param name="returnApplyMetList">物资退费申请实体集合</param>
        /// <returns>1成功 -1失败</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public int UpdateReturnApplyState(List<FS.HISFC.Models.Fee.ReturnApplyMet> returnApplyMetList,FS.HISFC.Models.Base.CancelTypes applyFalg)
        {
            foreach (FS.HISFC.Models.Fee.ReturnApplyMet returnApplyMet in returnApplyMetList)
            {
                returnApplyMet.ApplyFlag = applyFalg;
                if (UpdateReturnApplyMet(returnApplyMet) <= 0)
                    return -1;
            }
            return 1;
        }

        /// <summary>
        /// 根据申请序号获取物资退费申请记录
        /// </summary>
        /// <param name="applyNo">申请序号</param>
        /// <returns>物资退费申请记录</returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public List<HISFC.Models.Fee.ReturnApplyMet> QueryReturnApplyMetByApplyNo(string applyNo,HISFC.Models.Base.CancelTypes applyFlag)
        {
            return this.QueryReturnApplyMet("Fee.ApplyReturn.GetApplyReturnMetList.Where.ApplyNoAndState", applyNo, ((int)applyFlag).ToString());
        }

        /// <summary>
        /// 根据申请序号获取物资退费申请记录
        /// </summary>
        /// <param name="applyNo">申请序号</param>
        /// <param name="applyFlag">确认标识（0申请，1退费，2作废）</param>
        /// <returns></returns>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        public List<HISFC.Models.FeeStuff.Output> QueryOutPutByApplyNo(string applyNo, HISFC.Models.Base.CancelTypes applyFlag)
        {
            return this.QueryOutPutByApply("Fee.ApplyReturn.GetApplyReturnMetList.Where.ApplyNoAndState", applyNo, ((int)applyFlag).ToString());
        }

        #endregion

        #endregion

        #region 作废方法

        /// <summary>
        /// 查询一段时间内申请退费的患者
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ConfirmFlag"></param>
        /// <returns></returns>
        [Obsolete("作废,使用QueryAppliedPatientsbyTime()", true)]
        public ArrayList QueryPatients(string BeginDate, string EndDate,string ConfirmFlag)
        {
            #region sql
            //			SELECT distinct parent_code,   --父级医疗机构编码
            //					current_code,   --本级医疗机构编码
            //					inpatient_no,   --住院流水号
            //					name,   --患者姓名
            //					nurse_cell_code    --所在病区
            //				FROM met_nui_cancelitem   --病区退费申请表
            //				WHERE parent_code='[父级编码]' and current_code='[本级编码]'
            //				and oper_date>=to_date('{0}','yyyy-mm-dd HH24:mi:ss')
            //				and oper_date<=to_date('{1}','yyyy-mm-dd HH24:mi:ss')
            //				and confirm_flag='{3}'
            #endregion
            string strSql = "";
            ArrayList al = new ArrayList();

            if (Sql.GetSql("Fee.ApplyReturn.QueryPatients", ref strSql) == -1)
            {
                this.Err = "没有找到Fee.ApplyReturn.QueryPatients的SQL语句!";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, BeginDate, EndDate, ConfirmFlag);
            }
            catch (Exception e)
            {
                this.Err = "传入参数不正确！Fee.ApplyReturn.QueryPatients" + e.Message;
                return null;
            }
            try
            {
                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }
                while (Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = Reader[2].ToString();//住院流水号
                    obj.Name = Reader[3].ToString();//姓名
                    obj.Memo = Reader[4].ToString();//护士站代码

                    al.Add(obj);
                }
            }
            catch (Exception e)
            {
                this.Err = "执行Sql语句错误！Fee.ReturnApply.QueryPatients" + e.Message;
                return null;
            }
            return al;
        }



        /// <summary>
        /// 删除一条退费申请记录
        /// </summary>
        /// <param name="applySquence">退费申请流水号</param>
        /// <returns>成功1 失败－1 无记录 0</returns>
        [Obsolete("作废,使用DeleteReturnApply()", true)]
        public int DeleteApplyReturn(string applySquence)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.ReturnApply.DeleteApplyReturn", ref strSql) == -1)
            {
                this.Err = "没有找到Fee.ReturnApply.DeleteApplyReturn字段!";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Fee.ReturnApply.DeleteApplyReturn" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 插入一条退费申请记录
        /// </summary>
        /// <param name="info">摆药台实体</param>
        /// <returns>1成功，-1失败</returns>
        [Obsolete("作废,使用InsertReturnApply()", true)]
        public int InsertApplyReturn(FS.HISFC.Models.Fee.ReturnApply info)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.ReturnApply.InsertApplyReturn", ref strSql) == -1)
            {
                this.Err = "没有找到Fee.ReturnApply.InsertApplyReturn字段!";
                return -1;
            }
            try
            {
                string[] strParm = GetReturnApplyParams(info);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = "传入参数不正确！Fee.ReturnApply.InsertApplyReturn" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 取退费申请流水号
        /// </summary>
        /// <returns>"-1"出错，oterhs 成功</returns>
        [Obsolete("作废,使用GetReturnApplySequence()", true)]
        public string GetApplyReturnID()
        {
            return this.GetSequence("Fee.ReturnApply.GetApplyReturnID");
        }

        /// <summary>
        /// 根据患者住院流水号，取此患者的退费申请数据
        /// </summary>
        /// <param name="inPatientNo">患者住院流水号</param>
        /// <returns>退费申请数组</returns>
        [Obsolete("作废,使用QueryReturnApplys()", true)]
        public ArrayList GetList(string inPatientNo)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";  //SELECT语句

            //取SQL语句
            if (this.Sql.GetCommonSql("Fee.ReturnApply.GetApplyReturnList", ref strSQL) == -1)
            {
                this.Err = "没有找到Fee.ReturnApply.GetApplyReturnList字段!";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL, inPatientNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Fee.ReturnApply.GetApplyReturnList" + ex.Message;
                return null;
            }
            //取摆药台数据列表			
            return this.QueryReturnApplysBySql(strSQL);
        }

        /// <summary>
        /// 根据患者住院流水号，取此患者的退费申请数据
        /// </summary>
        /// <param name="inPatientNo">患者住院流水号</param>
        /// <param name="isCharged">是否被确认的数据</param>
        /// <returns>退费申请数组</returns>
        [Obsolete("作废,使用QueryReturnApplys()", true)]
        public ArrayList GetList(string inPatientNo, bool isCharged)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";  //SELECT语句
            string strWhere = ""; //Where条件
            string chargeFlag = "0"; //退费状态：0未退费（申请），1已退费（核准）

            //取SQL语句
            if (this.Sql.GetCommonSql("Fee.ReturnApply.GetApplyReturnList", ref strSQL) == -1)
            {
                this.Err = "没有找到Fee.ReturnApply.GetApplyReturnList字段!";
                return null;
            }


            //取Where语句
            if (this.Sql.GetCommonSql("Fee.ReturnApply.GetApplyReturnList.Where", ref strWhere) == -1)
            {
                this.Err = "没有找到Fee.ReturnApply.GetApplyReturnList.Where字段!";
                return null;
            }

            //设置申请状态参数
            if (isCharged)
                chargeFlag = "1";
            else
                chargeFlag = "0";

            try
            {
                strSQL = string.Format(strSQL + " " + strWhere, inPatientNo, chargeFlag);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Fee.ReturnApply.GetApplyReturnList.Where" + ex.Message;
                return null;
            }
            //取摆药台数据列表			
            return this.QueryReturnApplysBySql(strSQL);
        }

        /// <summary>
        /// 根据患者住院流水号，取此患者的退费申请数据
        /// </summary>
        /// <param name="inPatientNo">患者住院流水号</param>
        /// <param name="operDept">退费操作员所在科室</param>
        /// <param name="isCharged">是否被确认数据</param>
        /// <param name="isUndrug">是否非药品</param>
        /// <returns>成功返回退费申请数组 失败返回null</returns>
        [Obsolete("作废,使用QueryReturnApplys()", true)]
        public ArrayList GetList(string inPatientNo, string operDept,bool isCharged,bool isUndrug)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";  //SELECT语句
            string strWhere = ""; //Where条件
            string chargeFlag = "0"; //退费状态：0未退费（申请），1已退费（核准）
            string drugFlag = "2"; //药品标记：1 药品	2 非药品

            //取SQL语句
            if (this.Sql.GetCommonSql("Fee.ReturnApply.GetApplyReturnList", ref strSQL) == -1)
            {
                this.Err = "没有找到Fee.ReturnApply.GetApplyReturnList字段!";
                return null;
            }


            //取Where语句
            if (this.Sql.GetCommonSql("Fee.ReturnApply.GetApplyReturnList.Where.isUndrug", ref strWhere) == -1)
            {
                this.Err = "没有找到Fee.ReturnApply.GetApplyReturnList.Where.isUndrug字段!";
                return null;
            }

            //设置申请状态参数
            if (isCharged)
                chargeFlag = "1";
            else
                chargeFlag = "0";

            if (isUndrug)
                drugFlag = "0";     //非药品
            else
                drugFlag = "1";		//药品

            try
            {
                strSQL = string.Format(strSQL + " " + strWhere, inPatientNo, operDept, chargeFlag, drugFlag);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Fee.ReturnApply.GetApplyReturnList.Where" + ex.Message;
                return null;
            }
            //取摆药台数据列表			
            return this.QueryReturnApplysBySql(strSQL);
        }

        /// <summary>
        /// 根据患者住院流水号、是否确认 、是否发药取患者药品退费申请数据
        /// Add By liangjz 2005-10
        /// </summary>
        /// <param name="inPatientNo">住院流水号</param>
        /// <param name="isCharged">是否退费确认</param>
        /// <param name="isConfirm">是否发药</param>
        /// <returns>成功返回退费申请数组、失败返回null</returns>
        [Obsolete("作废,使用QueryDrugReturnApplys()", true)]
        public ArrayList GetDrugList(string inPatientNo, bool isCharged,bool isConfirm)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";  //SELECT语句
            string strWhere = ""; //Where条件
            string chargeFlag = "0"; //退费状态：0未退费（申请），1已退费（核准）
            string confirmFlag = "0"; //发药标记 0 未发药 1 已发药

            //取SQL语句
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetApplyReturnList", ref strSQL) == -1)
            {
                this.Err = "没有找到Fee.ReturnApply.GetApplyReturnList字段!";
                return null;
            }
            //取Where语句
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetApplyReturnList.Where.DrugList", ref strWhere) == -1)
            {
                this.Err = "没有找到Fee.ReturnApply.GetApplyReturnList.Where.DrugList字段!";
                return null;
            }

            //设置申请状态参数
            if (isCharged)
                chargeFlag = "1";
            else
                chargeFlag = "0";

            if (isConfirm)
                confirmFlag = "1";       //已发药
            else
                confirmFlag = "0";		 //未发药

            try
            {
                strSQL = string.Format(strSQL + " " + strWhere, inPatientNo, chargeFlag, confirmFlag);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！" + ex.Message;
                return null;
            }
            //取摆药台数据列表			
            return this.QueryReturnApplysBySql(strSQL);
        }

        /// <summary>
        /// 取消退费申请 置状态为无效 2
        /// </summary>
        /// <param name="ID">申请流水号</param>
        /// <param name="operCode">取消人</param>
        /// <returns>成功1 失败－1 无记录 0</returns>
        [Obsolete("作废,使用CancelReturnApply()", true)]
        public int CancelApplyReturn(string ID, string operCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.ReturnApply.CancelApplyReturn", ref strSql) == -1)
            {
                this.Err = "没有找到Fee.ReturnApply.CancelApplyReturn字段!";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, ID, operCode);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Fee.ReturnApply.CancelApplyReturn" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        #region 账户新增
        /// <summary>
        /// 根据病历号时间段查询申请信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isDrug"></param>
        /// <returns></returns>
        public ArrayList GetApplyReturn(string cardNO, bool isConfirm, bool isCharge, bool isDrug)
        {
            string sql = string.Empty;

            //取SQL语句
            if (this.Sql.GetCommonSql("Fee.ApplyReturn.GetApplyReturnListByCardNO", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.ApplyReturn.GetApplyReturnListByCardNO的SQL语句!";

                return null;
            }
            return QueryReturnApplysBySql(sql, cardNO,
                                         NConvert.ToInt32(isConfirm).ToString(),
                                         NConvert.ToInt32(isCharge).ToString(),
                                         NConvert.ToInt32(isDrug).ToString());

        }

        /// <summary>
        /// 更新退费申请表退费标识
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        public int UpdateApplyCharge(FS.HISFC.Models.Fee.ReturnApply returnApply)
        {
            return this.UpdateSingleTable("Fee.ApplyReturn.ChargeApplyReturn",
                                   returnApply.ID,
                                   returnApply.CancelOper.ID,
                                   returnApply.CancelOper.OperTime.ToString());
        }
        #endregion
    }
}