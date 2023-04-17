using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using FS.FrameWork.Function;


namespace FS.SOC.Local.Pharmacy.ZhuHai.Extend.ZDWY
{
    public class BizExtendInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend
    {
        ArrayList alPlan = new ArrayList();

        protected FS.SOC.HISFC.BizLogic.Pharmacy.Check itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();

        #region IPharmacyBizExtend 成员 其它

        public int AfterSave(string class2Code, string class3code, System.Collections.ArrayList alData, ref string errInfo)
        {
            #region 一般入库处理
            if (class2Code == "0310" && class3code == "01")
            {
                LocalPlanBizlogic localMgr = new LocalPlanBizlogic();
                if (alData == null || alData.Count == 0)
                {
                    return 1;
                }

                FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
                bool isDrugStock = false;
                FS.HISFC.Models.Pharmacy.Input inputTemp = alData[0] as FS.HISFC.Models.Pharmacy.Input;
                FS.HISFC.Models.Base.Department deptinfo = deptMgr.GetDeptmentById(inputTemp.StockDept.ID);
                if (deptinfo.DeptType.ID.ToString() == "PI")
                {
                    isDrugStock = true;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                FS.SOC.HISFC.BizLogic.Pharmacy.Adjust adjustMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Adjust();
                string AdjustPriceBillNO = "";
                int SerialNO = 0;
                foreach (FS.HISFC.Models.Pharmacy.Input inputInfo in alData)
                {
                    if (localMgr.InsertInputExtend(inputInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }

                    //爱博恩特殊需求 ，药库的话入库直接进行调价
                    //1、药库入库
                    //2、不包括疫苗及非药品
                    //3、旧零售价不等于新零售价
                    if (isDrugStock
                        & (inputInfo.Item.Name.Contains("疫苗") || inputInfo.Item.SysClass.ID.ToString() != "O")
                        & inputInfo.Item.PriceCollection.RetailPrice != inputInfo.Item.PriceCollection.WholeSalePrice)
                        //{738625BE-10F2-41cf-AC76-A2A1AA54307F}
                        //!= decimal.Round(inputInfo.Item.PriceCollection.PurchasePrice * FS.FrameWork.Function.NConvert.ToDecimal(1.15), 2))
                    {
                        FS.HISFC.Models.Pharmacy.AdjustPrice adjustPrice = new FS.HISFC.Models.Pharmacy.AdjustPrice();
                        FS.HISFC.Models.Pharmacy.Item item = adjustMgr.GetItem(inputInfo.Item.ID);
                        if (AdjustPriceBillNO == "")
                        {
                            AdjustPriceBillNO = adjustMgr.GetAdjustPriceBillNO();
                        }
                        adjustPrice.ID = AdjustPriceBillNO;                     //0 调价单号
                        adjustPrice.SerialNO = SerialNO;         //1 调价单内序号
                        SerialNO = SerialNO + 1;
                        adjustPrice.StockDept.ID = inputInfo.StockDept.ID;                      //2 库房编码  
                        adjustPrice.Item = item.Clone();                       //3 药品编码
                        //adjustPrice.Item.PriceCollection.RetailPrice.ToString(),   //6 调价前零售价格
                        //adjustPrice.Item.PriceCollection.WholeSalePrice.ToString(),//7 调价前批发价格
                        //{738625BE-10F2-41cf-AC76-A2A1AA54307F}
                        adjustPrice.AfterRetailPrice = inputInfo.Item.PriceCollection.WholeSalePrice;
                            //adjustPrice.Item.PriceCollection.PurchasePrice * FS.FrameWork.Function.NConvert.ToDecimal( 1.15);   //8 调价后零售价格
                        adjustPrice.AfterWholesalePrice = adjustPrice.Item.PriceCollection.WholeSalePrice;//9 调价后批发价格
                        if (adjustPrice.AfterRetailPrice > adjustPrice.Item.PriceCollection.RetailPrice)
                        {
                            adjustPrice.ProfitFlag = "1";                   //10盈亏标记1-盈，0-亏
                        }
                        else
                        {
                            adjustPrice.ProfitFlag = "0";
                        }
                        adjustPrice.InureTime = adjustMgr.GetDateTimeFromSysDateTime();       //11调价执行时间
                        adjustPrice.State = "0";                         //18调价单状态：0、未调价；1、已调价；2、无效
                        adjustPrice.Memo = "入库自动调价" + inputInfo.InListNO;                         //20备注
                        adjustPrice.Operation.Oper.ID = adjustMgr.Operator.ID;                     //21操作员编码
                        adjustPrice.Operation.Oper.Name = adjustMgr.Operator.Name;                      //22操作员名称
                        adjustPrice.Operation.Oper.OperTime = adjustPrice.InureTime;            //23操作时间
                        adjustPrice.IsDDAdjust = false;
                        adjustPrice.IsDSAdjust = false;
                        adjustPrice.AdjustPriceType = "1";               //26调价类型
                        //adjustPrice.Item.RetailPrice2.ToString(),   //27调价前零差价
                        adjustPrice.AfterRetailPrice2 = 0;    //28调价后零差价
                        if (adjustMgr.InsertAdjustPriceInfo(adjustPrice) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + adjustMgr.Err, MessageBoxIcon.Error);
                            return -1;
                        }
                        int param = adjustMgr.UpdateStoragePrice(inputInfo.StockDept.ID, inputInfo.Item.ID, adjustPrice.AfterRetailPrice);
                        if (param == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + adjustMgr.Err, MessageBoxIcon.Error);
                            return -1;
                        }
                    }
                }
                if (AdjustPriceBillNO != "")
                {
                    if (adjustMgr.ExecProcedureChangPrice() == -1)
                    {
                        Function.ShowMessage("调价失败：执行存储过程发生错误，" + adjustMgr.Err, MessageBoxIcon.Error);
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            #endregion
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting GetChooseDataSetting(string class2Code, string class3MeaningCode, string class3Code, string listType, ref string errInfo)
        {
            //使用核心默认的
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
            chooseDataSetting.IsDefault = true;
            return chooseDataSetting;
        }

        public uint GetCostDecimals(string class2Code, string class3MeaningCode, string type)
        {
            //{FD859C21-15CA-4457-A659-1A27B769D969}
            return 4;
        }

        public System.Collections.ArrayList SetCheckDetail(string stockDeptNO)
        {
            bool needSendMachine = false;
            if (stockDeptNO == "6027")    //正式
            //if (stockDeptNO == "0019")  //测试
            {
                needSendMachine = true;
            }
            //返回null使用核心默认的
            ArrayList alDetail = new ArrayList();
            DateTime curFOperTime = this.itemMgr.GetDateTimeFromSysDateTime();
            //获取新盘点单号
            string checkNO = this.itemMgr.GetCheckCode(stockDeptNO);
            if (string.IsNullOrEmpty(checkNO))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("获取盘点单号发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return null;
            }

            DataSet myDataSet = new DataSet();
            if (needSendMachine)
            {

                try
                {

                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = "data source = 192.168.236.51;initial catalog = Hospital_pharmacy_MZYF; user id = sa;password = iron";
                    conn.Open();
                    string strCmd = string.Format("SELECT * FROM v_storetable");

                    SqlDataAdapter myDataAdapter;
                    myDataAdapter = new SqlDataAdapter(strCmd, conn);
                    myDataAdapter.Fill(myDataSet, "test");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    //Function.ShowMessage("获取发药机库存发生错误，请与系统管理员联系并报告错误：" + ex.Message, MessageBoxIcon.Error);
                    DialogResult dialogResult = MessageBox.Show("获取发药机库存发生错误，请与系统管理员联系并报告错误：\n" + ex.Message + "\n\n是否手工录入发药机数量？", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        return null;
                    }
                }

            }



            List<FS.HISFC.Models.Pharmacy.Check> checkList = this.itemMgr.QueryCheckList(stockDeptNO, "0", "ALL");
            if (checkList == null)
            {
                Function.ShowMessage("获取封账列表发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return null;
            }
            if (checkList.Count > 0)
            {
                Function.ShowMessage("已经存在盘点单，不可以再封账!", MessageBoxIcon.Information);
                return null;
            }

            string SQL = @" select count(*) 
                            from   pha_com_output o 
                            where  o.drug_storage_code = '{0}'
                            and    o.out_state <> '2'";


            string count = this.itemMgr.ExecSqlReturnOne(string.Format(SQL, stockDeptNO));

            if (count == "-1")
            {
                Function.ShowMessage("检查入库情况发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return null;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(count) > 0)
            {
                DialogResult drTmp = MessageBox.Show("还存在没有入库的药品,请确认是否封账？", "提示", MessageBoxButtons.YesNo);
                {
                    if (drTmp == DialogResult.No)
                    {
                        return null;
                    }
                }
            }
            //{FD859C21-15CA-4457-A659-1A27B769D969}
            SQL = @"select count(*) from pha_com_applyout a where a.drug_dept_code in ('4003','4103') and a.apply_state ='1' and a.class3_meaning_code = '18' and a.valid_state = '1'  ";

            count = this.itemMgr.ExecSqlReturnOne(string.Format(SQL, stockDeptNO));

            if (count == "-1")
            {
                Function.ShowMessage("检查入库情况发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return null;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(count) > 0)
            {
                DialogResult drTmp = MessageBox.Show("还存在没有内退的药品,请确认是否封账？", "提示", MessageBoxButtons.YesNo);
                {
                    if (drTmp == DialogResult.No)
                    {
                        return null;
                    }
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            DialogResult dr = MessageBox.Show("封账开始前，请通知库房所有人员停止出入库、调价等其他业务操作\n\n确认封账吗?", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return null;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍候...");
            Application.DoEvents();

            List<FS.HISFC.Models.Pharmacy.Item> alItem = this.itemMgr.QueryItemList();
            if (alItem == null)
            {
                Function.ShowMessage("获取药品基本信息发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return null;
            }

            Hashtable hsItem = new Hashtable();
            foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
            {
                hsItem.Add(item.ID, item);
            }

            //对所有药品进行封帐处理


            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            if (needSendMachine)
            {
                for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
                {
                    string sqlInsert = "insert into pha_com_iron_storage values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},to_date('{8}','yyyy-mm-dd hh24:mi:ss'))";
                    sqlInsert = string.Format(sqlInsert, 
                        
                        checkNO, 
                        stockDeptNO, 
                        myDataSet.Tables[0].Rows[i][0].ToString(), 
                        myDataSet.Tables[0].Rows[i][1].ToString(), 
                        myDataSet.Tables[0].Rows[i][6].ToString(),
                        myDataSet.Tables[0].Rows[i][2].ToString(), 
                        myDataSet.Tables[0].Rows[i][3].ToString(), 
                        myDataSet.Tables[0].Rows[i][4].ToString(), 
                        curFOperTime);
                    if (this.itemMgr.ExecNoQuery(sqlInsert) == -1)
                    {
                        Function.ShowMessage("封账发生错误，请与系统管理员联系并报告错误：/n" + this.itemMgr.Err, MessageBoxIcon.Error);

                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return null;
                    }
                }
            }


            FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConstantMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
            bool isManageBatch = phaConstantMgr.IsManageBatch(stockDeptNO);

            FS.SOC.HISFC.BizLogic.Pharmacy.Check checkMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Check();

            if (needSendMachine)
            {
                //{83DABC8A-E421-45ea-85DC-4C28B97BBBE1}
                alDetail = checkMgr.CloseAll(stockDeptNO, isManageBatch, 90, checkNO);    
            }
            else
            {
                //{83DABC8A-E421-45ea-85DC-4C28B97BBBE1}
                alDetail = checkMgr.CloseAll(stockDeptNO, isManageBatch,90);
            }

            if (alDetail == null)
            {
                Function.ShowMessage("封账发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return null ;
            }

            if (alDetail.Count == 0)
            {
                Function.ShowMessage("没有库存!", MessageBoxIcon.Information);
                return null;
            }

            //保存新生成的盘点单号
            //this.curCheckBillNO = checkNO;

            FS.HISFC.Models.Pharmacy.Check checkStatic = new FS.HISFC.Models.Pharmacy.Check();

            checkStatic.CheckNO = checkNO;				            //盘点单号
            checkStatic.StockDept = new FS.FrameWork.Models.NeuObject(stockDeptNO, "", "");			        //库房编码
            checkStatic.State = "0";					            //封帐状态
            checkStatic.User01 = "0";						        //盘亏金额
            checkStatic.User02 = "0";						        //盘盈金额

            checkStatic.FOper.ID = this.itemMgr.Operator.ID;   //封帐人
            checkStatic.FOper.OperTime = curFOperTime;				    //封帐时间
            checkStatic.Operation.Oper = checkStatic.FOper;               //操作人

            if (this.itemMgr.InsertCheckStatic(checkStatic) != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("插入盘点汇总信息发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return null;
            }

            foreach (FS.HISFC.Models.Pharmacy.Check checkDetail in alDetail)
            {
                checkDetail.CheckNO = checkStatic.CheckNO;

                decimal purchasePrice = 0;
                if (isManageBatch)
                {
                    purchasePrice = checkDetail.Item.PriceCollection.PurchasePrice;
                }
                checkDetail.Item = hsItem[checkDetail.Item.ID] as FS.HISFC.Models.Pharmacy.Item;
                if (isManageBatch)
                {
                    checkDetail.Item.PriceCollection.PurchasePrice = purchasePrice;
                }
                checkDetail.Operation = checkStatic.Operation;

                if (needSendMachine)
                {
                    checkDetail.AdjustQty = checkDetail.PackQty * checkDetail.Item.PackQty + checkDetail.MinQty + checkDetail.OtherAdjustQty * checkDetail.Item.PackQty;    
                }
                
                checkDetail.CStoreQty = checkDetail.AdjustQty;
                checkDetail.ProfitLossQty = checkDetail.AdjustQty - checkDetail.FStoreQty;

                if (checkDetail.ProfitLossQty < 0)
                {
                    checkDetail.ProfitStatic = "0";
                }
                else if (checkDetail.ProfitLossQty == 0)
                {
                    checkDetail.ProfitStatic = "2";
                }
                else
                {
                    checkDetail.ProfitStatic = "1";
                }

                //对盘点明细表插入数据
                if (this.itemMgr.InsertCheckDetail(checkDetail) != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    Function.ShowMessage("插入盘点明细信息发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return null;
                }

            }

            FS.FrameWork.Management.PublicTrans.Commit();

            return alDetail;

        }



        #endregion

        #region IPharmacyBizExtend 成员 内部入库申请
        public System.Collections.ArrayList SetInnerInputApply(string applyDeptNO, string stockDeptNO, System.Collections.ArrayList alData)
        {
            return null;
            using (frmSetPlan frmSetPlan = new frmSetPlan())
            {
                frmSetPlan.SetCompletedEven -= new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.SetCompletedEven += new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.Init(applyDeptNO, FS.HISFC.Models.Pharmacy.EnumDrugStencil.Apply);
                frmSetPlan.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frmSetPlan.ShowDialog();


                if (alPlan == null)
                {
                    MessageBox.Show("生成内部入库申请发生错误", "错误>>");
                    return null;
                }
                if (alData != null && alData.Count > 0)
                {
                    //界面加载了项目，根据现有项目生成计划
                    for (int index1 = 0; index1 < alData.Count; index1++)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut plan1 = alData[index1] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        for (int index2 = 0; index2 < alPlan.Count; index2++)
                        {
                            FS.HISFC.Models.Pharmacy.InPlan plan2 = alPlan[index2] as FS.HISFC.Models.Pharmacy.InPlan;
                            if (plan1.Item.ID == plan2.Item.ID)
                            {
                                plan1.ExtFlag = plan2.OutputQty.ToString();//消耗量
                                plan1.ExtFlag1 = plan2.Extend;//参考量

                                alPlan.RemoveAt(index2);

                                break;
                            }
                        }
                    }
                    return alData;
                }
                else
                {
                    ArrayList alAppply = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.InPlan plan in alPlan)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
                        applyOut.Item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(plan.Item.ID);

                        applyOut.StockDept.ID = stockDeptNO;
                        applyOut.ApplyDept.ID = applyDeptNO;
                        applyOut.ExtFlag = plan.OutputQty.ToString();//消耗量
                        applyOut.ExtFlag1 = plan.Extend;//参考量

                        applyOut.Class2Type = "0310";
                        applyOut.PrivType = "02";
                        applyOut.SystemType = "13";

                        applyOut.State = "0";                                   //状态 申请
                        applyOut.ShowState = "1";
                        applyOut.ShowUnit = applyOut.Item.PackUnit;

                        alAppply.Add(applyOut);
                    }

                    return alAppply;
                }
            }
        }
        #endregion

        #region IPharmacyBizExtend 成员 入库计划
        public System.Collections.ArrayList SetInputPlan(string stockDeptNO, System.Collections.ArrayList alData)
        {
            //return null;

            using (frmSetPlan frmSetPlan = new frmSetPlan())
            {
                frmSetPlan.SetCompletedEven -= new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.SetCompletedEven += new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.Init(stockDeptNO, FS.HISFC.Models.Pharmacy.EnumDrugStencil.Plan);
                frmSetPlan.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frmSetPlan.ShowDialog();

                if (alPlan == null)
                {
                    MessageBox.Show("生成入库计划发生错误", "错误>>");
                    return null;
                }
                if (alData != null && alData.Count > 0)
                {
                    //界面加载了项目，根据现有项目生成计划
                    for (int index1 = 0; index1 < alData.Count; index1++)
                    {
                        FS.HISFC.Models.Pharmacy.InPlan plan1 = alData[index1] as FS.HISFC.Models.Pharmacy.InPlan;
                        for (int index2 = 0; index2 < alPlan.Count; index2++)
                        {
                            FS.HISFC.Models.Pharmacy.InPlan plan2 = alPlan[index2] as FS.HISFC.Models.Pharmacy.InPlan;
                            if (plan1.Item.ID == plan2.Item.ID)
                            {
                                plan1.PlanQty = plan2.PlanQty;
                                plan1.Formula = plan2.Formula;
                                plan1.Extend = plan2.Extend;//公式生成的参考计划量，将保存到数据库字段
                                alPlan.RemoveAt(index2);
                                break;
                            }
                        }
                    }
                    return alData;
                }
                else
                {
                    foreach (FS.HISFC.Models.Pharmacy.InPlan plan in alPlan)
                    {
                        plan.Item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(plan.Item.ID);
                        plan.Dept.ID = stockDeptNO;
                        //plan.Extend;//公式生成的参考计划量，将保存到数据库字段
                        if (string.IsNullOrEmpty(plan.Item.Product.Company.ID) && plan.Item.TenderOffer.IsTenderOffer)
                        {
                            plan.Company = plan.Item.TenderOffer.Company;
                        }
                        else
                        {
                            plan.Company = plan.Item.Product.Company;
                        }
                        if (plan.Company != null && !string.IsNullOrEmpty(plan.Company.ID))
                        {
                            plan.Company.Name = SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(plan.Company.ID);
                        }
                    }
                }
            }
            return alPlan;
        }

        #endregion

        #region IPharmacyBizExtend 成员 入出库单号


        public string GetBillNO(string stockDeptNO, string class2Code, string class3code, ref string errInfo)
        {

            FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();

            string ListNO = "";
            decimal iSequence = 0;
            DateTime sysDate = extentManager.GetDateTimeFromSysDateTime().Date;
         
            if(class2Code == "0310")
            {
                //获取当前科室的单据最大流水号
                FS.HISFC.Models.Base.ExtendInfo deptExt = extentManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "InListCode", stockDeptNO);
                if (deptExt == null)
                {
                    return null;
                }
                else
                {
                    if (deptExt.Item.ID == "")          //当前科室尚无记录 流水号置为1
                    {
                        iSequence = 1;
                    }
                    else                                //当前科室存在记录 根据日期是否为当天 确定流水号是否归1
                    {
                        if (deptExt.DateProperty.Date != sysDate)
                        {
                            iSequence = 1;
                        }
                        else
                        {
                            iSequence = deptExt.NumberProperty + 1;
                        }
                    }
                    //生成单据号
                    ListNO = stockDeptNO + sysDate.Year.ToString().Substring(2, 2) + sysDate.Month.ToString().PadLeft(2, '0') + sysDate.Day.ToString().PadLeft(2, '0')
                      + "1"  + iSequence.ToString().PadLeft(3, '0');

                    //保存当前最大流水号
                    deptExt.Item.ID = stockDeptNO;
                    deptExt.DateProperty = sysDate;
                    deptExt.NumberProperty = iSequence;
                    deptExt.PropertyCode = "InListCode";
                    deptExt.PropertyName = "科室单据号最大流水号";

                    if (extentManager.SetComExtInfo(deptExt) == -1)
                    {
                        return null;
                    }
                }
            }
            else if (class2Code == "0320")
            {
                //获取当前科室的单据最大流水号
                FS.HISFC.Models.Base.ExtendInfo deptExt = extentManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "OutListCode", stockDeptNO);
                if (deptExt == null)
                {
                    return null;
                }
                else
                {
                    if (deptExt.Item.ID == "")          //当前科室尚无记录 流水号置为1
                    {
                        iSequence = 1;
                    }
                    else                                //当前科室存在记录 根据日期是否为当天 确定流水号是否归1
                    {
                        if (deptExt.DateProperty.Date != sysDate)
                        {
                            iSequence = 1;
                        }
                        else
                        {
                            iSequence = deptExt.NumberProperty + 1;
                        }
                    }
                    //生成单据号
                    ListNO = stockDeptNO + sysDate.Year.ToString().Substring(2, 2) + sysDate.Month.ToString().PadLeft(2, '0') + sysDate.Day.ToString().PadLeft(2, '0')
                    + "2" + iSequence.ToString().PadLeft(3, '0');

                    //保存当前最大流水号
                    deptExt.Item.ID = stockDeptNO;
                    deptExt.DateProperty = sysDate;
                    deptExt.NumberProperty = iSequence;
                    deptExt.PropertyCode = "OutListCode";
                    deptExt.PropertyName = "科室单据号最大流水号";

                    if (extentManager.SetComExtInfo(deptExt) == -1)
                    {
                        return null;
                    }
                }
            }
    
            return ListNO;
        }

        #endregion

        #region IPharmacyBizExtend 成员 入库录入信息


        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl GetInputInfoControl(string class3code, bool isSpecial, FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl defaultInputInfoControl)
        {
            if (isSpecial)
            {
                return new ucSpecialInput();
            }
            else
            {
                return new ucCommonInput();
            }
        }

        #endregion

        #region IPharmacyBizExtend 成员 药品基本信息扩展

        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl GetItemExtendControl(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl defaultItemExtendControl)
        {
            return new ucItemExtend();
        }

        #endregion

        void frmSetPlan_SetCompletedHander(frmSetPlan.CreatePlanType type, string formula, params string[] param)
        {
            if (type == frmSetPlan.CreatePlanType.Consume)
            {
                SetPlan setPlanMgr = new SetPlan();
                //GetPlan(string deptNO, DateTime consumeBeginTime, DateTime consumeEndTime, int lowDays, int upDays, string drugType, string stencilNO)
                alPlan = setPlanMgr.GetPlan(param[0],
                    FS.FrameWork.Function.NConvert.ToDateTime(param[1]),
                    FS.FrameWork.Function.NConvert.ToDateTime(param[2]),
                    FS.FrameWork.Function.NConvert.ToInt32(param[3]),
                    FS.FrameWork.Function.NConvert.ToInt32(param[4]),
                    param[5],
                    param[6]
                    );
            }
            else if (type == frmSetPlan.CreatePlanType.Warning)
            {
                SetPlan setPlanMgr = new SetPlan();
                //GetPlan(string deptNO, string drugType, string stencilNO)
                alPlan = setPlanMgr.GetPlan(param[0], param[1], param[2]);
            }
            else if (type == frmSetPlan.CreatePlanType.Formula)
            {
                SetPlan setPlanMgr = new SetPlan();
                alPlan = setPlanMgr.GetPlan(param[0], param[1].ToString(), param[2].ToString(),param[3].ToString());
            }
        }
    }
}
