using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.BizLogic.Pharmacy
{
    /// <summary>
    /// [功能描述: 药品出库申请管理类]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2011-06]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public class Apply : Storage
    {     

        /// <summary>
        /// 更新申请数据状态
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="listCode">申请单据号</param>
        /// <param name="state">申请状态</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateApplyOutState(string deptCode, string listCode, string state)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateApplyOutState", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateApplyOutState";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode, listCode, state, this.Operator.ID);
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyOutState";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新申请数据状态0为1
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="listCode">申请单据号</param>
        /// <param name="state">申请状态</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int ExamApplyOut(string deptCode, string listCode)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.ApplyOut.Exam", ref strSQL) == -1)
            {
                //this.Err = "没有找到SQL语句SOC.Pharmacy.ApplyOut.Exam";
                //return -1;
                strSQL = @" UPDATE  PHA_COM_APPLYOUT  
                            SET     PHA_COM_APPLYOUT.APPLY_STATE = '{2}',              --申请单状态
                                    PHA_COM_APPLYOUT.PRINT_EMPL = '{3}',         --操作人
                                    PHA_COM_APPLYOUT.PRINT_DATE = SYSDATE         --操作时间
                            WHERE   PHA_COM_APPLYOUT.APPLY_BILLCODE = '{1}'       --出库申请单据号
                            AND	    PHA_COM_APPLYOUT.DEPT_CODE = '{0}' 				--申请科室
                            AND     PHA_COM_APPLYOUT.APPLY_STATE = '{4}'";

                this.CacheSQL("SOC.Pharmacy.ApplyOut.Exam", strSQL);
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode, listCode, "1", this.Operator.ID, "0");
            }
            catch
            {
                this.Err = "传入参数不正确！SOC.Pharmacy.ApplyOut.Exam";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// 更新已打印标记
        /// </summary>
        /// <param name="applyID">申请流水号</param>
        /// <param name="isPrint">是否已打印</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateApplyOutPrintState(string applyID, bool isPrint)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateApplyOutPrintState", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateApplyOutPrintState";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, applyID, NConvert.ToInt32(isPrint));
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyOutPrintState";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 对未审核的申请数据更新申请数量、申请日期
        /// </summary>
        /// <param name="ID">申请流水号</param>
        /// <param name="applyNum">申请数量</param>
        /// <returns>成功返回1 失败返回－1 无数据返回0</returns>
        public int UpdateApplyOutNum(string ID, decimal applyNum)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateApplyOutNum", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateApplyOutNum";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID, applyNum, this.Operator.ID);
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyOutNum";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        
        /// <summary>
        /// 取消出库申请
        /// 根据出库申请流水号，作废出库申请
        /// </summary>
        /// <param name="ID">出库申请流水号</param>
        /// <param name="validState">有效状态</param>
        /// <returns>正确1,没找到数据0,错误－1</returns>
        public int UpdateApplyOutValidState(string ID, string validState)
        {
            string strSQL = "";
            //根据处方流水号和处方内序号，作废出库申请记录的Update语句
            if (this.GetSQL("Pharmacy.Item.UpdateApplyOutValidState", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateApplyOutValidState";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID, validState, this.Operator.ID);
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyOutValidState";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 根据流水号更新申请有效性
        /// </summary>
        /// <param name="applyID">申请流水号</param>
        /// <param name="isValid">是否有效 True 有效 False 无效</param>
        /// <returns>成功返回1 失败返回-1</returns>
        protected int UpdateApplyOutValidByID(string applyID, bool isValid)
        {
            string strSQL = "";
            //根据执行档流水号，作废出库申请记录的Update语句
            if (this.GetSQL("Pharmacy.Item.CancelApplyOut.ApplyID", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.CancelApplyOut.ApplyID";
                return -1;
            }

            //1 恢复申请有效性 0 作废申请
            if (isValid)
                strSQL = string.Format(strSQL, applyID, this.Operator.ID, ((int)FS.HISFC.Models.Base.EnumValidState.Valid).ToString());
            else
                strSQL = string.Format(strSQL, applyID, this.Operator.ID, ((int)FS.HISFC.Models.Base.EnumValidState.Invalid).ToString());

            int parm = this.ExecNoQuery(strSQL);
            if (parm != 1)
                return parm;
            return 1;
        }

        /// <summary>
        /// 根据流水号获取申请信息
        /// </summary>
        /// <param name="applyOutID">执行档流水号</param>
        /// <returns>成功返回出库申请实体信息 失败返回null 无数据返回空实体</returns>
        public FS.HISFC.Models.Pharmacy.ApplyOut GetApplyOutByID(string applyOutID)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.ByID", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.ByID字段!";
                return null;
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, applyOutID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            ArrayList al = this.myGetApplyOut(strSelect);
            if (al == null) return null;

            if (al.Count == 0)
                return new FS.HISFC.Models.Pharmacy.ApplyOut();
            else
                return al[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
        }

        /// <summary>
        /// 作废出库申请信息
        /// </summary>
        /// <param name="applyID">申请档流水号</param>
        /// <param name="isPreOut">是否预出库</param>
        /// <returns>成功返回受影响条数 失败返回-1</returns>
        public int CancelApplyOutByID(string applyID, bool isPreOut)
        {
            //申请信息作废
            int parm = this.UpdateApplyOutValidByID(applyID, false);
            if (parm != 1)
                return parm;

            //如果预扣库存,则在取消出库申请的时候,还回预扣的库存
            if (isPreOut)
            {
                //取摆药申请数据
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.GetApplyOutByID(applyID);
                if (applyOut == null)
                    return -1;

                //还回预扣库存       //取消摆药申请时预扣减少（负数），取消退药申请时不处理预扣库存（退药确认时处理）
                if (applyOut.BillClassNO != "R")
                {
                    //{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                    if (this.UpdateStockinfoPreOutNum(applyOut, -applyOut.Operation.ApplyQty, applyOut.Days) == -1)
                    {
                        return -1;
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// 撤销取消出库申请（取消申请的逆过程）
        /// 根据申请流水号进行更新
        /// </summary>
        /// <param name="applyID">申请流水号</param>
        /// <param name="isPreOut">是否预扣库存</param>
        /// <returns>正确1,没找到数据0,错误－1</returns>
        public int UndoCancelApplyOutByID(string applyID, bool isPreOut)
        {
            //申请信息置为有效
            int parm = this.UpdateApplyOutValidByID(applyID, true);
            if (parm != 1)
                return parm;

            //定义药房管理类
            DrugStore drugStoreManager = new DrugStore();
            drugStoreManager.SetTrans(this.Trans);
            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.GetApplyOutByID(applyID);
            if (applyOutTemp == null)
                return -1;

            if (drugStoreManager.UpdateDrugMessage(applyOutTemp.StockDept.ID, applyOutTemp.ApplyDept.ID, applyOutTemp.BillClassNO, applyOutTemp.SendType, "0") != 1)
            {
                this.Err = "更新摆药通知记录发生错误" + drugStoreManager.Err;
                return -1;
            }

            //如果预扣库存,则在取消出库申请的时候,还回预扣的库存
            if (isPreOut)
            {
                //还回预扣库存       //恢复摆药申请时预扣增加（正数），恢复退药申请时不处理预扣（退药确认时处理）
                if (applyOutTemp.BillClassNO != "R")
                {
                    //{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                    if (this.UpdateStockinfoPreOutNum(applyOutTemp, applyOutTemp.Operation.ApplyQty, applyOutTemp.Days) == -1)
                    {
                        return -1;
                    }
                }
            }
            return 1;
        }


        #region 基础增、删、改操作

        /// <summary>
        /// 获得update或者insert出库申请表的传入参数数组
        /// 
        /// </summary>
        /// <param name="ApplyOut">出库申请类</param>
        /// <returns>成功返回参数字符串数组 失败返回null</returns>
        protected string[] myGetParmApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut ApplyOut)
        {
            //默认申请状态为:0申请状态
            if (ApplyOut.State == null || ApplyOut.State == "")
                ApplyOut.State = "0";
            if (ApplyOut.UseTime == null)
            {
                ApplyOut.UseTime = System.DateTime.MinValue;
            }
            string applyOper = ApplyOut.Operation.ApplyOper.ID;
            if (applyOper == "")
            {
                applyOper = this.Operator.ID;
            }

            string[] strParm ={   ApplyOut.ID,                                 //0申请流水号
								 ApplyOut.ApplyDept.ID,                       //1申请部门编码（科室或者病区）
								 ApplyOut.StockDept.ID,                      //2发药部门编码
								 ApplyOut.SystemType,                          //3出库申请分类
								 ApplyOut.GroupNO.ToString(),                 //4批次号
								 ApplyOut.Item.ID,                            //5药品编码
								 ApplyOut.Item.Name,                          //6药品商品名
								 ApplyOut.BatchNO,                            //7批号
								 ApplyOut.Item.Type.ID,                       //8药品类别
								 ApplyOut.Item.Quality.ID.ToString(),         //9药品性质
								 ApplyOut.Item.Specs,                         //10规格
								 ApplyOut.Item.PackUnit,                      //11包装单位
								 ApplyOut.Item.PackQty.ToString(),            //12包装数
								 ApplyOut.Item.MinUnit,                       //13最小单位
								 ApplyOut.ShowState,                          //14显示的单位标记
								 ApplyOut.ShowUnit,                           //15显示的单位
								 ApplyOut.Item.PriceCollection.RetailPrice.ToString(),        //16零售价
								 ApplyOut.Item.PriceCollection.WholeSalePrice.ToString(),     //17批发价
								 ApplyOut.Item.PriceCollection.PurchasePrice.ToString(),      //18购入价
								 ApplyOut.BillNO,                           //19申请单号
								 applyOper,                                 //20申请人编码
								 ApplyOut.Operation.ApplyOper.OperTime.ToString(),               //21申请日期
								 ApplyOut.State,                         //22申请状态 0申请，1核准（出库），2作废，3暂不摆药
								 ApplyOut.Operation.ApplyQty.ToString(),                //23申请出库量(每付的总数量)
								 ApplyOut.Days.ToString(),                    //24付数（草药）
								 NConvert.ToInt32(ApplyOut.IsPreOut).ToString(), //25预扣库存状态（'0'不预扣库存，'1'预扣库存）
								 NConvert.ToInt32(ApplyOut.IsCharge).ToString(), //26收费状态：0未收费，1已收费
								 ApplyOut.PatientNO,                          //27患者编号
								 ApplyOut.PatientDept.ID,                     //28患者科室
								 ApplyOut.DrugNO,                           //29摆药单号
								 ApplyOut.Operation.ApproveOper.Dept.ID,                     //30摆药科室
								 ApplyOut.Operation.ApproveOper.ID,                    //31摆药人
								 ApplyOut.Operation.ApproveOper.OperTime.ToString(),             //32摆药日期
								 ApplyOut.Operation.ApproveQty.ToString(),              //33摆药数量
								 ApplyOut.DoseOnce.ToString(),                //34每次剂量
								 ApplyOut.Item.DoseUnit,                      //35剂量单位
								 ApplyOut.Usage.ID,                           //36用法代码
								 ApplyOut.Usage.Name,                         //37用法名称
								 ApplyOut.Frequency.ID,                       //38频次代码
								 ApplyOut.Frequency.Name,                     //39频次名称
								 ApplyOut.Item.DosageForm.ID,                 //40剂型编码
								 ApplyOut.OrderType.ID,                       //41医嘱类型
								 ApplyOut.OrderNO,                            //42医嘱流水号
								 ApplyOut.CombNO,                             //43组合序号
								 ApplyOut.ExecNO,                             //44执行单流水号
								 ApplyOut.RecipeNO,                           //45处方号
								 ApplyOut.SequenceNO.ToString(),              //46处方内项目流水号
								 ApplyOut.SendType.ToString(),                //47医嘱发送类型1集中，2临时
								 ApplyOut.BillClassNO,                        //48摆药单分类
								 ApplyOut.PrintState,                         //49打印状态
								 ApplyOut.OutBillNO,                          //50出库单号（退库申请时，保存出库时对应的记录）
								 ((int)ApplyOut.ValidState).ToString(),	      //51有效标记（1有效，0无效，不摆药）
								 ApplyOut.Memo,								  //52医嘱备注
								 ApplyOut.PlaceNO,						      //53货位号
								 ApplyOut.UseTime.ToString(),							  //54取消日期(用药时间)
                                 ApplyOut.RecipeInfo.Dept.ID,
                                 ApplyOut.RecipeInfo.ID,
                                 NConvert.ToInt32(ApplyOut.IsBaby).ToString(),
                                 ApplyOut.ExtFlag,
                                 ApplyOut.ExtFlag1,
                                 ApplyOut.CompoundGroup,
                                 NConvert.ToInt32(ApplyOut.Compound.IsNeedCompound).ToString(),
                                 NConvert.ToInt32(ApplyOut.Compound.IsExec).ToString(),
                                 ApplyOut.Compound.CompoundOper.ID,
                                 ApplyOut.Compound.CompoundOper.OperTime.ToString()
							 };

            return strParm;
        }

        /// <summary>
        /// 取出库申请表中的全部字段数据
        /// 私有方法，在其他方法中调用  
        /// 使用该函数的索引 : Pharmacy.Item.GetApplyOutList Pharmacy.Item.GetApplyOutList.Patient
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>成功返回出库申请实体数组 失败返回null</returns>
        protected ArrayList myGetApplyOut(string SQLString)
        {
            ArrayList al = new ArrayList();              //用于返回出库申请信息的数组
            FS.HISFC.Models.Pharmacy.ApplyOut info; //返回数组中的出库申请类

            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得出库申请信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.ApplyOut();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                                  //申请流水号
                        info.ApplyDept.ID = this.Reader[1].ToString();                        //申请部门编码（科室或者病区）
                        info.StockDept.ID = this.Reader[2].ToString();                       //发药部门编码
                        info.SystemType = this.Reader[3].ToString();                           //出库申请分类
                        info.GroupNO = NConvert.ToDecimal(this.Reader[4].ToString());                  //批次号
                        info.Item.ID = this.Reader[5].ToString();                             //药品编码
                        info.Item.Name = this.Reader[6].ToString();                           //药品商品名
                        info.BatchNO = this.Reader[7].ToString();                             //批号
                        info.Item.Type.ID = this.Reader[8].ToString();                        //药品类别
                        info.Item.Quality.ID = this.Reader[9].ToString();                      //药品性质
                        info.Item.Specs = this.Reader[10].ToString();                         //规格
                        info.Item.PackUnit = this.Reader[11].ToString();                      //包装单位
                        info.Item.PackQty = NConvert.ToDecimal(this.Reader[12].ToString());   //包装数
                        info.Item.MinUnit = this.Reader[13].ToString();                       //最小单位
                        info.ShowState = this.Reader[14].ToString();                          //显示的单位标记
                        info.ShowUnit = this.Reader[15].ToString();                           //显示的单位
                        info.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[16].ToString());    //零售价
                        info.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[17].ToString()); //批发价
                        info.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[18].ToString());  //购入价
                        info.BillNO = this.Reader[19].ToString();                           //申请单号
                        info.Operation.ApplyOper.ID = this.Reader[20].ToString();                      //申请人编码
                        info.Operation.ApplyOper.OperTime = NConvert.ToDateTime(this.Reader[21].ToString());     //申请日期
                        info.State = this.Reader[22].ToString();                         //申请状态 0申请，1核准（出库），2作废，3暂不摆药
                        info.Operation.ApplyQty = NConvert.ToDecimal(this.Reader[23].ToString());       //申请出库量(每付的总数量)
                        info.Days = NConvert.ToDecimal(this.Reader[24].ToString());           //付数（草药）
                        info.IsPreOut = NConvert.ToBoolean(this.Reader[25].ToString());       //是否预扣库存：0未预扣，1已预扣
                        info.IsCharge = NConvert.ToBoolean(this.Reader[26].ToString());       //是否收费：0未收费，1已收费
                        info.PatientNO = this.Reader[27].ToString();                          //患者编号
                        info.PatientDept.ID = this.Reader[28].ToString();                     //患者科室
                        info.DrugNO = this.Reader[29].ToString();                           //摆药单号
                        info.Operation.ApproveOper.Dept.ID = this.Reader[30].ToString();                     //摆药科室
                        info.Operation.ApproveOper.ID = this.Reader[31].ToString();                    //摆药人
                        info.Operation.ApproveOper.OperTime = NConvert.ToDateTime(this.Reader[32].ToString());   //摆药日期
                        info.Operation.ApproveQty = NConvert.ToDecimal(this.Reader[33].ToString());     //摆药数量

                        info.Operation.ExamQty = info.Operation.ApproveQty;

                        info.DoseOnce = NConvert.ToDecimal(this.Reader[34].ToString());       //每次剂量
                        info.Item.DoseUnit = this.Reader[35].ToString();                      //剂量单位
                        info.Usage.ID = this.Reader[36].ToString();                           //用法代码
                        info.Usage.Name = this.Reader[37].ToString();                         //用法名称
                        info.Frequency.ID = this.Reader[38].ToString();                       //频次代码
                        info.Frequency.Name = this.Reader[39].ToString();                     //频次名称
                        info.Item.DosageForm.ID = this.Reader[40].ToString();                 //剂型编码
                        info.OrderType.ID = this.Reader[41].ToString();                       //医嘱类型编码
                        info.OrderNO = this.Reader[42].ToString();                            //医嘱流水号
                        info.CombNO = this.Reader[43].ToString();                             //组合序号
                        info.ExecNO = this.Reader[44].ToString();                             //执行单流水号
                        info.RecipeNO = this.Reader[45].ToString();                           //处方号
                        info.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[46].ToString());              //处方内项目流水号
                        info.SendType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[47].ToString());                //医嘱发送类型0全部，1集中，2临时
                        info.BillClassNO = this.Reader[48].ToString();                      //摆药单分类
                        info.PrintState = this.Reader[49].ToString();                         //打印状态
                        info.Operation.ExamOper.ID = this.Reader[50].ToString();                       //审批人（打印人）
                        info.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[51].ToString());      //审批时间（打印时间）
                        info.OutBillNO = this.Reader[52].ToString();                        //出库单号（退库申请时，保存出库时对应的记录）
                        info.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[53]);                         //有效标记（0有效，1无效，2不摆药）

                        //by cube 2011-1-15
                        //info.User01 = this.Reader[54].ToString();                             //患者床位号
                        //info.User02 = this.Reader[55].ToString();                             //患者姓名
                        info.BedNO = this.Reader[54].ToString();                             //患者床位号
                        info.PatientName = this.Reader[55].ToString();                             //患者姓名
                        //end by

                        info.Memo = this.Reader[56].ToString();								  //医嘱备注
                        info.RecipeInfo.Dept.ID = this.Reader[57].ToString();
                        info.RecipeInfo.ID = this.Reader[58].ToString();
                        info.IsBaby = NConvert.ToBoolean(this.Reader[59]);
                        info.ExtFlag = this.Reader[60].ToString();
                        info.ExtFlag1 = this.Reader[61].ToString();
                        info.CompoundGroup = this.Reader[62].ToString();
                        info.Compound.IsNeedCompound = NConvert.ToBoolean(this.Reader[63].ToString());
                        info.Compound.IsExec = NConvert.ToBoolean(this.Reader[64].ToString());
                        info.Compound.CompoundOper.ID = this.Reader[65].ToString();
                        info.Compound.CompoundOper.OperTime = NConvert.ToDateTime(this.Reader[66].ToString());
                        if (this.Reader.FieldCount > 67)
                        {
                            info.UseTime = NConvert.ToDateTime(this.Reader[67].ToString());
                        }
                        if (this.Reader.FieldCount > 68 && !this.Reader.IsDBNull(68))
                        {
                            info.PlaceNO = this.Reader[68].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得出库申请信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得出库申请信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 插入一条出库申请记录
        /// </summary>
        /// <param name="applyOut">申请出库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertApplyOut", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.InsertApplyOut";
                return -1;
            }
            try
            {
                //{C37BEC96-D671-46d1-BCDD-C634423755A4}  更改预扣库存管理模式
                if (string.IsNullOrEmpty(applyOut.ID))
                {
                    applyOut.ID = this.GetSequence("Pharmacy.Item.GetNewApplyOutID");
                }

                string[] strParm = myGetParmApplyOut(applyOut);  //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入出库申请SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新出库申请记录
        /// </summary>
        /// <param name="applyOut">出库申请记录</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            return this.UpdateApplyOut(applyOut, false);
        }
        /// <summary>
        /// 更新出库申请记录
        /// </summary>
        /// <param name="applyOut">出库申请记录</param>
        /// <param name="preState">先前状态</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut,string preState)
        {

            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateApplyOut", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = myGetParmApplyOut(applyOut);  //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新出库申请SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }

            string strWhere = "and apply_state='{0}'";

            strWhere = string.Format(strWhere, preState);

            return this.ExecNoQuery(strSQL + strWhere);
        }

        /// <summary>
        /// 更新出库申请记录
        /// {EE05DA01-8969-404d-9A6B-EE8AD0BC1CD0}
        /// </summary>
        /// <param name="applyOut">出库申请记录</param>
        /// <param name="isApplyState">是否判断是申请状态</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, bool isJudgeApplyState)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateApplyOut", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = myGetParmApplyOut(applyOut);  //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新出库申请SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            string strWhere = "";
            if (isJudgeApplyState)
            {
                if (this.GetSQL("Pharmacy.Item.UpdateApplyOutByApplyState", ref strWhere) == -1)
                {
                    this.Err += "获取Pharmacy.Item.UpdateApplyOutByApplyState语句出错！";
                    return -1;
                }
            }
            return this.ExecNoQuery(strSQL + strWhere);
        }

        /// <summary>
        /// 删除出库申请记录
        /// </summary>
        /// <param name="ID">出库申请记录流水号</param>
        /// <returns>0没有删除 1成功 -1失败</returns>
        public int DeleteApplyOut(string ID)
        {
            string strSQL = "";
            //根据出库申请流水号删除某一条出库申请记录的DELETE语句
            if (this.GetSQL("Pharmacy.Item.DeleteApplyOut", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.DeleteApplyOut";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, ID);
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.DeleteApplyOut";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 先进行更新操作 更新数据为零则进行插入操作
        /// </summary>
        /// <param name="applyOut">出库申请实体</param>
        /// <returns>成功返回更新数量 失败返回－1</returns>
        public int SetApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            int parm;
            parm = this.UpdateApplyOut(applyOut);
            if (parm == -1)
                return -1;
            if (parm == 0)
                parm = this.InsertApplyOut(applyOut);
            return parm;
        }

        #endregion

    }
}
