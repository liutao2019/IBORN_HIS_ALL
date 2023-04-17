using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using Neusoft.FrameWork.Function;


namespace Neusoft.SOC.Local.Pharmacy.Extend.ZDLY
{
    public class BizExtendInterfaceImplement : Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend
    {
        ArrayList alPlan = new ArrayList();

        protected Neusoft.SOC.HISFC.BizLogic.Pharmacy.Check itemMgr = new Neusoft.SOC.HISFC.BizLogic.Pharmacy.Check();

        #region IPharmacyBizExtend 成员 其它

        public int AfterSave(string class2Code, string class3code, System.Collections.ArrayList alData, ref string errInfo)
        {
            return 1;
        }

        public Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting GetChooseDataSetting(string class2Code, string class3MeaningCode, string class3Code, string listType, ref string errInfo)
        {
            //使用核心默认的
            Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = new Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
            chooseDataSetting.IsDefault = true;
            return chooseDataSetting;
        }

        public uint GetCostDecimals(string class2Code, string class3MeaningCode, string type)
        {
            return 2;
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
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
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



            List<Neusoft.HISFC.Models.Pharmacy.Check> checkList = this.itemMgr.QueryCheckList(stockDeptNO, "0", "ALL");
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

            if (Neusoft.FrameWork.Function.NConvert.ToInt32(count) > 0)
            {
                Function.ShowMessage("还存在没有入库的药品，目前不可以封账，请【核准入库】！", MessageBoxIcon.Information);
                return null;
            }

            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            DialogResult dr = MessageBox.Show("封账开始前，请通知库房所有人员停止出入库、调价等其他业务操作\n\n确认封账吗?", "提示>>", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return null;
            }
            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍候...");
            Application.DoEvents();

            List<Neusoft.HISFC.Models.Pharmacy.Item> alItem = this.itemMgr.QueryItemList();
            if (alItem == null)
            {
                Function.ShowMessage("获取药品基本信息发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return null;
            }

            Hashtable hsItem = new Hashtable();
            foreach (Neusoft.HISFC.Models.Pharmacy.Item item in alItem)
            {
                hsItem.Add(item.ID, item);
            }

            //对所有药品进行封帐处理


            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();


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

                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        return null;
                    }
                }
            }


            Neusoft.SOC.HISFC.BizLogic.Pharmacy.Constant phaConstantMgr = new Neusoft.SOC.HISFC.BizLogic.Pharmacy.Constant();
            bool isManageBatch = phaConstantMgr.IsManageBatch(stockDeptNO);

            Neusoft.SOC.HISFC.BizLogic.Pharmacy.Check checkMgr = new Neusoft.SOC.HISFC.BizLogic.Pharmacy.Check();

            if (needSendMachine)
            {
                alDetail = checkMgr.CloseAll(stockDeptNO, isManageBatch, 90, checkNO);    
            }
            else
            {
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

            Neusoft.HISFC.Models.Pharmacy.Check checkStatic = new Neusoft.HISFC.Models.Pharmacy.Check();

            checkStatic.CheckNO = checkNO;				            //盘点单号
            checkStatic.StockDept = new Neusoft.FrameWork.Models.NeuObject(stockDeptNO, "", "");			        //库房编码
            checkStatic.State = "0";					            //封帐状态
            checkStatic.User01 = "0";						        //盘亏金额
            checkStatic.User02 = "0";						        //盘盈金额

            checkStatic.FOper.ID = this.itemMgr.Operator.ID;   //封帐人
            checkStatic.FOper.OperTime = curFOperTime;				    //封帐时间
            checkStatic.Operation.Oper = checkStatic.FOper;               //操作人

            if (this.itemMgr.InsertCheckStatic(checkStatic) != 1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("插入盘点汇总信息发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                return null;
            }

            foreach (Neusoft.HISFC.Models.Pharmacy.Check checkDetail in alDetail)
            {
                checkDetail.CheckNO = checkStatic.CheckNO;

                decimal purchasePrice = 0;
                if (isManageBatch)
                {
                    purchasePrice = checkDetail.Item.PriceCollection.PurchasePrice;
                }
                checkDetail.Item = hsItem[checkDetail.Item.ID] as Neusoft.HISFC.Models.Pharmacy.Item;
                if (isManageBatch)
                {
                    checkDetail.Item.PriceCollection.PurchasePrice = purchasePrice;
                }
                checkDetail.Operation = checkStatic.Operation;

                if (needSendMachine)
                {
                    checkDetail.AdjustQty = checkDetail.PackQty * checkDetail.Item.PackQty + checkDetail.MinQty + checkDetail.OtherAdjustQty;    
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
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();

                    Function.ShowMessage("插入盘点明细信息发生错误，请与系统管理员联系并报告错误：" + this.itemMgr.Err, MessageBoxIcon.Error);
                    return null;
                }

            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();

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
                frmSetPlan.Init(applyDeptNO, Neusoft.HISFC.Models.Pharmacy.EnumDrugStencil.Apply);
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
                        Neusoft.HISFC.Models.Pharmacy.ApplyOut plan1 = alData[index1] as Neusoft.HISFC.Models.Pharmacy.ApplyOut;
                        for (int index2 = 0; index2 < alPlan.Count; index2++)
                        {
                            Neusoft.HISFC.Models.Pharmacy.InPlan plan2 = alPlan[index2] as Neusoft.HISFC.Models.Pharmacy.InPlan;
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
                    foreach (Neusoft.HISFC.Models.Pharmacy.InPlan plan in alPlan)
                    {
                        Neusoft.HISFC.Models.Pharmacy.ApplyOut applyOut = new Neusoft.HISFC.Models.Pharmacy.ApplyOut();
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
            return null;

            using (frmSetPlan frmSetPlan = new frmSetPlan())
            {
                frmSetPlan.SetCompletedEven -= new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.SetCompletedEven += new frmSetPlan.SetCompletedHander(frmSetPlan_SetCompletedHander);
                frmSetPlan.Init(stockDeptNO, Neusoft.HISFC.Models.Pharmacy.EnumDrugStencil.Plan);
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
                        Neusoft.HISFC.Models.Pharmacy.InPlan plan1 = alData[index1] as Neusoft.HISFC.Models.Pharmacy.InPlan;
                        for (int index2 = 0; index2 < alPlan.Count; index2++)
                        {
                            Neusoft.HISFC.Models.Pharmacy.InPlan plan2 = alPlan[index2] as Neusoft.HISFC.Models.Pharmacy.InPlan;
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
                    foreach (Neusoft.HISFC.Models.Pharmacy.InPlan plan in alPlan)
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
            string billNO = "default";

            //Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

            //billNO = phaIntegrate.GetInOutListNO(stockDeptNO, (class2Code == "0310"));
            //if (billNO == null)
            //{
            //    errInfo = "获取最新入库单号出错" + phaIntegrate.Err;
            //    return "-1";
            //}
            return billNO;
        }

        #endregion

        #region IPharmacyBizExtend 成员 入库录入信息


        public Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl GetInputInfoControl(string class3code, bool isSpecial, Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl defaultInputInfoControl)
        {
            if (isSpecial)
            {
                return defaultInputInfoControl;
            }
            else
            {
                return new ucCommonInput();
            }
        }

        #endregion

        #region IPharmacyBizExtend 成员 药品基本信息扩展

        public Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl GetItemExtendControl(Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl defaultItemExtendControl)
        {
            return null;
        }

        #endregion

        void frmSetPlan_SetCompletedHander(frmSetPlan.CreatePlanType type, string formula, params string[] param)
        {
            if (type == frmSetPlan.CreatePlanType.Consume)
            {
                SetPlan setPlanMgr = new SetPlan();
                //GetPlan(string deptNO, DateTime consumeBeginTime, DateTime consumeEndTime, int lowDays, int upDays, string drugType, string stencilNO)
                alPlan = setPlanMgr.GetPlan(param[0],
                    Neusoft.FrameWork.Function.NConvert.ToDateTime(param[1]),
                    Neusoft.FrameWork.Function.NConvert.ToDateTime(param[2]),
                    Neusoft.FrameWork.Function.NConvert.ToInt32(param[3]),
                    Neusoft.FrameWork.Function.NConvert.ToInt32(param[4]),
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
        }
    }
}
