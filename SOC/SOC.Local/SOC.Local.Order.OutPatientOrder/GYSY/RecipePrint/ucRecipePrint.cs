using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.GYSY.RecipePrint
{
    public partial class ucRecipePrint : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.HISFC.BizProcess.Interface.IRecipePrint, Neusoft.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint
    {
        public ucRecipePrint()
        {
            InitializeComponent();
        }

        #region 变量

        #region 业务层

        /// <summary>
        /// 
        /// </summary>
        Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

        Neusoft.HISFC.BizLogic.Fee.Outpatient outPatientManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        Neusoft.HISFC.BizLogic.Order.OutPatient.Order orderManager = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        Neusoft.HISFC.BizLogic.Pharmacy.DrugStore drugManager = new Neusoft.HISFC.BizLogic.Pharmacy.DrugStore();

        Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        Neusoft.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();

        Neusoft.HISFC.BizLogic.Manager.Frequency frequencyManagement = new Neusoft.HISFC.BizLogic.Manager.Frequency();

        /// <summary>
        /// 参数帮助类
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
        #endregion

        /// <summary>
        /// 药品性质帮助类
        /// </summary>
        Neusoft.FrameWork.Public.ObjectHelper drugQuaulityHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 特殊处方标记
        /// </summary>
        string speRecipeLabel = "";

        /// <summary>
        /// 是否英文
        /// </summary>
        public bool bEnglish = false;

        /// <summary>
        /// 医嘱排序
        /// </summary>
        OrderCompare orderCompare = new OrderCompare();

        /// <summary>
        /// 门诊处方调剂实体
        /// </summary>
        Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = new Neusoft.HISFC.Models.Pharmacy.DrugRecipe();

        /// <summary>
        /// 药房编码
        /// </summary>
        string drugDept = "";

        /// <summary>
        /// 操作员
        /// </summary>
        Neusoft.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// 人员列表
        /// </summary>
        Hashtable hsEmpl = new Hashtable();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        private static Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper freHelper = null;

        /// <summary>
        /// 频次帮助类
        /// </summary>
        public Neusoft.FrameWork.Public.ObjectHelper FreHelper
        {
            set
            {
                this.freHelper = value;
            }
        }

        /// <summary>
        /// 打印处方项目名称是否是通用名：1 通用名；0 商品名
        /// </summary>
        private int printItemNameType = -1;

        /// <summary>
        /// 是否打印签名信息？（否则手工签名）
        /// </summary>
        private int isPrintSignInfo = -1;


        //UsageCompare compare = new UsageCompare();
        ArrayList alSort = new ArrayList();

        /// <summary>
        /// 判断是否为双字符的正则表达式
        /// </summary>
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{15,18}$");

        //private Neusoft.FrameWork.WinForms.Controls.NeuPictureBox npbBarCode;
        //private Neusoft.FrameWork.WinForms.Controls.NeuPictureBox npbRecipeNo;

        /// <summary>
        /// 医嘱数组
        /// </summary>
        public ArrayList alOrder = new ArrayList();

        private ArrayList alTemp = new ArrayList();

        /// <summary>
        /// 存储处方组合号
        /// </summary>
        private Hashtable hsCombID = new Hashtable();

        /// <summary>
        /// 员工显示工号的位数
        /// </summary>
        private int showEmplLength = -1;

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        private Neusoft.HISFC.Models.Registration.Register myReg;

        /// <summary>
        /// 用法列表，用来判断是否是针剂用法
        /// </summary>
        private ArrayList alUsage = null;

        /// <summary>
        /// 合同单位编码
        /// </summary>
        private ArrayList alPactCodeMark = null;
        
        /// <summary>
        /// 用法帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper usageHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 合同单位帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper pactCodeMarkHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        
        #endregion

        #region 初始化

        /// <summary>
        /// load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucRecipePrint_Load(object sender, System.EventArgs e)
        {
            this.Init();
        }

        private void Init()
        {
            if (alUsage == null || alUsage.Count == 0)
            {
                alUsage = interMgr.GetConstantList("USAGE");
                if (alUsage == null)
                {
                    MessageBox.Show("获取用法列表出错：" + interMgr.Err);
                    return;
                }

                usageHelper.ArrayObject = alUsage;
            }

            if (printItemNameType == -1)
            {
               
                printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
            }

            if (freHelper == null || freHelper.ArrayObject.Count == 0)
            {
                freHelper = new Neusoft.FrameWork.Public.ObjectHelper();
                ArrayList alFrequency = frequencyManagement.GetAll("ROOT");
                if (alFrequency != null && alFrequency.Count > 0)
                {
                    freHelper.ArrayObject = alFrequency;
                }
                else
                {
                    MessageBox.Show("获取频次错误：" + frequencyManagement.Err);
                    return;
                }
            }


            if (alPactCodeMark == null || alPactCodeMark.Count == 0)
            {
                alPactCodeMark = interMgr.GetConstantList("PactCodeMark");
                if (alUsage == null)
                {
                    MessageBox.Show("获取用法列表出错：" + interMgr.Err);
                    return;
                }

                pactCodeMarkHelper.ArrayObject = alPactCodeMark;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取医院名称和医院LOGO
        /// </summary>
        private void GetHospLogo()
        {
            Common.ComFunc cf = new Neusoft.SOC.Local.Order.OutPatientOrder.Common.ComFunc();
            string erro = "出错";
            string imgpath = Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + cf.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
        }

        /// <summary>
        /// 查询医嘱
        /// </summary>
        private void Query()
        {
            Init();
            if (deptHelper.ArrayObject.Count <= 0)
            {
                deptHelper.ArrayObject = interMgr.GetDepartment();
                if (deptHelper.ArrayObject == null)
                {
                    MessageBox.Show(interMgr.Err);
                }
            }
            //从处方表获取
            ArrayList al = this.orderManager.QueryOrderByRecipeNO(this.myReg.ID, this.recipeId);
            //没有的话从费用表获取
            if (al.Count <= 0)
            {
                ArrayList alFees = new ArrayList();
                alFees = this.outPatientManager.QueryFeeDetailFromRecipeNO(this.recipeId);
                if (alFees == null || alFees.Count <= 0)
                {
                    return;
                }
                else
                {
                    foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFees)
                    {
                        Neusoft.HISFC.Models.Order.OutPatient.Order ot = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                        ot.Item.SysClass = itemlist.Item.SysClass;
                        ot.ReciptNO = itemlist.RecipeNO;
                        ot.DoctorDept = itemlist.RecipeOper.Dept;
                        ot.MOTime = itemlist.ChargeOper.OperTime;
                        ot.StockDept = itemlist.ExecOper.Dept;
                        ot.Patient.Patient.PID.ID = itemlist.Patient.ID;
                        ot.Frequency.ID = itemlist.FreqInfo.ID;
                        ot.Frequency.Name = itemlist.FreqInfo.Name;
                        ot.Usage = itemlist.Order.Usage;
                        ot.Item.Name = itemlist.Name;
                        ot.HerbalQty = itemlist.Days;
                        ot.ReciptDept = itemlist.RecipeOper.Dept;
                        ot.Combo.ID = itemlist.Order.Combo.ID;
                        ot.Qty = itemlist.Item.Qty;
                        ot.Unit = itemlist.Item.PriceUnit;
                        ot.DoseOnce = itemlist.Order.DoseOnce;
                        ot.DoseUnit = itemlist.Order.DoseUnit;
                        ot.Item.Specs = itemlist.Item.Specs;
                        ot.Item.ItemType = itemlist.Item.ItemType;
                        al.Add(ot);
                    }
                }
            }
            if (this.alOrder.Count > 0)
            {
                this.alOrder.Clear();
            }
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in al)
            {
                if (order.Item.ItemType == Neusoft.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.alOrder.Add(order);
                }
            }
            this.MakaLabel(this.alOrder);
        }

        /// <summary>
        /// 返回组号
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int GetSubCombNo(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            int subCombNo = 1;
            Hashtable hsCombNo = new Hashtable();

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order ord in alOrder)
            {
                if (ord.SubCombNO < order.SubCombNO && !hsCombNo.Contains(ord.Combo.ID))
                {
                    hsCombNo.Add(ord.Combo.ID, ord);
                    subCombNo++;
                }
            }
            return subCombNo;
        }

        /// <summary>
        /// 生成显示信息
        /// </summary>
        /// <param name="alOrder"></param>
        private void MakaLabel(ArrayList alOrder)
        {
            if (alOrder == null)
            {
                return;
            }
            alTemp = new ArrayList();
            for (int i = 0; i < alOrder.Count; i++)
            {
                this.alTemp.Add(alOrder[i]);
            }
            hsCombID = new Hashtable();

            StringBuilder buffer = new System.Text.StringBuilder();
            //填充数据
            string[] parm = { "  ", "┌", "│", "└" };
            decimal phaMoney = 0m;//药费
            decimal ownPhaMoney = 0m;//自费药费
            decimal injectMoney = 0m;//注射费
            //decimal ownInjectMoney = 0m;//自费注射费
            string drugType = "";
            if (showEmplLength == -1)
            {
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                showEmplLength = controlMgr.GetControlParam<int>("HN0002", true, 6);
            }



            #region 查询所有收费项目（附材）

            string feeSeq = "";
            ArrayList alSubAndOrder = null;

            alSubAndOrder = new ArrayList();
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order OutPatientOrder in alOrder)
            {
                if (!feeSeq.Contains(OutPatientOrder.ReciptSequence))
                {
                    ArrayList al = outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(OutPatientOrder.Patient.ID, OutPatientOrder.ReciptSequence, "ALL");
                    if (al == null)
                    {
                        //errInfo = outpatientFeeMgr.Err;
                        //return -1;
                        al = new ArrayList();
                    }

                    alSubAndOrder.AddRange(al);
                    feeSeq += "|" + OutPatientOrder.ReciptSequence + "|";
                }

                this.lblSeeDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(OutPatientOrder.ReciptDept.ID);
                //this.lblSeeDept.Text = OutPatientOrder.ReciptDept.Name;
            }

            #endregion

            #region /*画组合符号*/

            Hashtable hsCombNo = new Hashtable();

            //按照sortID排序
            alOrder.Sort(orderCompare);
            Neusoft.HISFC.Models.Order.OutPatient.Order order = null;
            for (int i = 0; i < alOrder.Count; i++)
            {
                order = alOrder[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                if (order == null)
                {
                    continue;
                }
                if (i == 0)
                {
                    drugType = order.Item.SysClass.ID.ToString();
                    this.lblRecipeNo.Text = "*" + order.ReciptNO + "*";
                    this.npbRecipeNo.Image = this.CreateBarCode(order.ReciptNO);
                    if (hsEmpl.Contains(order.ReciptDoctor.ID))
                    {
                        oper = hsEmpl[order.ReciptDoctor.ID] as Neusoft.HISFC.Models.Base.Employee;
                    }
                    else
                    {
                        oper = this.interMgr.GetEmployeeInfo(order.ReciptDoctor.ID);
                    }

                    if (oper != null && oper.UserCode.Length > 0)
                    {
                        this.lblPhaDoc.Text = oper.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + order.ReciptDoctor.Name;
                    }
                    else
                    {
                        this.lblPhaDoc.Text = order.ReciptDoctor.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + order.ReciptDoctor.Name;
                    }
                    lbData.Text = order.MOTime.Date.ToString("yyyy") + "/" + order.MOTime.Date.ToString("MM") + "/" + order.MOTime.Date.ToString("dd");
                    this.myReg = this.regIntegrate.GetByClinic(this.myReg.ID);
                    //this.currentRecipe = this.drugManager.GetDrugRecipe(order.StockDept.ID, "M1", this.recipeId, "1");
                    //FrameWork.Management.ControlParam controlParam = new Neusoft.FrameWork.Management.ControlParam();  //控制参数管理类
 
                    //string[] MM = (controlParam.QueryControlerInfo("MZ0887")).Split(','); //获取门慢控制参数
                    //string[] MT = (controlParam.QueryControlerInfo("MZ0888")).Split(','); //门特
                    //string[] YBTC = (controlParam.QueryControlerInfo("MZ0889")).Split(','); //医保统筹            

                    Neusoft.FrameWork.Models.NeuObject pactObject = pactCodeMarkHelper.GetObjectFromID(this.myReg.Pact.ID);

                    if (!object.Equals(pactObject, null))
                    {
                        this.lbPatientUnit.Text = pactObject.Memo;
                        lbPatientUnit.Visible = true;
                    }
                    else {
                        lbPatientUnit.Visible = false;
                    }
                   
                    drugDept = order.StockDept.ID;
                }
                string printUsage = "";
                ArrayList al = this.GetOrderByCombId(order, ref printUsage);
                if (al.Count == 0)
                {
                    continue;
                }
                else
                {
                    #region 计算费用
                    try
                    {
                        if (!hsCombNo.Contains(order.Combo.ID))
                        {
                            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alSubAndOrder)
                            {
                                if (feeItem.Order.Combo.ID == order.Combo.ID)
                                {
                                    if (feeItem.Item.IsMaterial)
                                    {
                                        injectMoney += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                                    }
                                    else
                                    {
                                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order ord in alOrder)
                                        {
                                            if (ord.Item.ID == feeItem.Item.ID
                                                && ord.ReciptNO == feeItem.RecipeNO
                                                && ord.SequenceNO == feeItem.SequenceNO)
                                            {
                                                phaMoney += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                            hsCombNo.Add(order.Combo.ID, null);
                        }

                        //ArrayList alFee;
                        //alFee = this.outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(order.Combo.ID, this.myReg.ID, "ALL");
                        //if (alFee != null && alFee.Count >= 1)
                        //{
                        //    foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                        //    {
                        //        if (!itemlist.Item.IsMaterial)
                        //        {
                        //            itemlist.FT.TotCost = itemlist.FT.OwnCost + itemlist.FT.PayCost + itemlist.FT.PubCost;
                        //            phaMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;
                        //            ownPhaMoney += itemlist.FT.OwnCost;
                        //        }

                        //    }
                        //}
                    }
                    catch
                    { }
                    #endregion

                    #region 形成字符串
                    if (drugType == "PCC")
                    {
                        #region 草药处理

                        decimal days = 1m;
                        string freq = "";
                        string usage = "";

                        for (int k = 0; k < al.Count; k++)
                        {
                            order = al[k] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                            days = order.HerbalQty;
                            freq = order.Frequency.Name;

                            try
                            {
                                //草药用法只有系统类别为HJZ的才是正常用法，特殊用法不统一打印
                                if (((Neusoft.HISFC.Models.Base.Const)usageHelper.GetObjectFromID(order.Usage.ID)).UserCode == "HJZ")
                                {
                                    usage = order.Usage.Name;
                                }
                            }
                            catch
                            {
                                usage = order.Usage.Name;
                            }

                            string priceAndCost = "";
                            System.Text.StringBuilder buff = new System.Text.StringBuilder();

                            string itemName = "";

                            if (order.Memo != "")
                            {
                               itemName = order.Item.Name + "(" + order.Memo + ")";
                            }
                            else
                            {
                                itemName = order.Item.Name;
                            }

                            if (order.Item.ID == "999" && !itemName.Contains("自备"))
                            {
                                itemName += "[自备]";
                            }
                            buffer.Append("  ");//项目名前的空格
                            buff.Append(itemName);
                            //特殊用法
                            //try
                            //{
                            //    //草药用法只有系统类别为HJZ的才是正常用法，特殊用法不统一打印
                            //    if (((Neusoft.HISFC.Models.Base.Const)usageHelper.GetObjectFromID(order.Usage.ID)).UserCode != "HJZ")
                            //    {
                            //        buff.Append("(" + order.Usage.Name + ")");
                            //    }
                            //}
                            //catch
                            //{
                            //}

                            //buff.Append("  ");

                            string name = buff.ToString();
                            string dose = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;
                            string cost = "  " + priceAndCost;
                            try
                            {
                                buffer.Append(SetToByteLength((SetToByteLength(name, 15) + dose), 16));
                            }
                            catch { }
                            if ((k + 1) % 3 == 0 || k == al.Count - 1)
                            {
                                buffer.Append("\n");
                            }
                        }

                        buffer.Append("  剂量：");
                        buffer.Append(days.ToString());
                        buffer.Append("剂");
                        buffer.Append("\n");
                        buffer.Append("  用法: ");
                        buffer.Append("每日" + GetFrequencyCount(order.Frequency.ID) + "剂");
                        buffer.Append(" ");
                        buffer.Append(usage);

                        //判断是否是煎药方式
                      
                            if (!string.IsNullOrEmpty(order.Usage.Name))
                            {
                                buffer.Append("(" + order.Usage.Name + ")");
                            }                            
                
                         buffer.Append("\n");
                        #endregion
                    }
                    else
                    {
                        #region 西药、成药
                        Neusoft.HISFC.BizLogic.Order.OutPatient.Order ord = null;
                        Neusoft.FrameWork.Models.NeuObject quaulity = null;
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = null;
                        Neusoft.FrameWork.Models.NeuObject objFre = null;
                        string combLabel = "";
                        for (int k = 0; k < al.Count; k++)
                        {
                            order = al[k] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                            #region 组合标记

                            if (al.Count == 1)
                            {
                                buffer.Append(parm[0]);

                                combLabel = parm[0];
                            }
                            else
                            {
                                if (k == 0)
                                {
                                    buffer.Append(parm[1]);

                                    combLabel = parm[1];
                                }
                                else if (k == al.Count - 1)
                                {
                                    buffer.Append(parm[3]);

                                    combLabel = parm[3];
                                }
                                else
                                {
                                    buffer.Append(parm[2]);

                                    combLabel = parm[2];
                                }
                            }
                            #endregion

                            #region 组号

                            //buffer.Append("组[");
                            buffer.Append("[");
                            buffer.Append(this.GetSubCombNo(order).ToString());
                            buffer.Append("]");
                            #endregion

                            if (order.Item.ID == "999")
                            {
                                phaItem = null;
                            }
                            else
                            {
                                phaItem = phaIntegrate.GetItem(order.Item.ID);
                            }

                            #region 药品名称

                            string itemName = "";

                            if (phaItem != null)
                            {
                                if (this.bEnglish)
                                {
                                    itemName = phaItem.NameCollection.EnglishName;
                                }
                                else
                                {
                                    if (printItemNameType == -1)
                                    {
                                        Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                                        printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
                                    }

                                    if (printItemNameType == 0)
                                    {
                                        itemName = phaItem.Name;
                                    }
                                    else
                                    {
                                        //2011-6-10 houwb 通用名没有维护时，打印商品名
                                        if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                        {
                                            itemName = phaItem.Name;
                                        }
                                        else
                                        {
                                            itemName = phaItem.NameCollection.RegularName;
                                        }
                                    }
                                }

                                quaulity = drugQuaulityHelper.GetObjectFromID(phaItem.Quality.ID);

                                if (quaulity != null && quaulity.ID.Length > 0)
                                {
                                    #region 区分毒麻、精一、精二
                                    if (quaulity.Name.Trim().IndexOf("毒") >= 0
                                        || quaulity.Name.Trim().IndexOf("麻") >= 0)
                                    {
                                        speRecipeLabel = "毒麻";
                                    }
                                    else if (quaulity.Name.Trim().IndexOf("精") >= 0 && quaulity.Name.Trim().IndexOf("一") >= 0)
                                    {
                                        speRecipeLabel = "精一";
                                    }
                                    else if (quaulity.Name.Trim().IndexOf("精") >= 0 && quaulity.Name.Trim().IndexOf("二") >= 0)
                                    {
                                        speRecipeLabel = "精二";
                                    }
                                    else if (quaulity.Name.Trim().IndexOf("精") >= 0)
                                    {
                                        speRecipeLabel = "精";
                                    }
                                    #endregion
                                    /*
                                    switch (quaulity.ID)
                                    {
                                        case "S": speRecipeLabel = "毒麻";    //毒麻类
                                            break;
                                        case "P": speRecipeLabel = "精 ";      //精神类
                                            break;
                                        case "P1": speRecipeLabel = "精一";    //第一类精神药
                                            break;
                                        case "P2": speRecipeLabel = "精二";    //第二类精神药
                                            break;
                                        default:
                                            break;
                                    }
                                    */
                                }
                            }
                            else
                            {
                                itemName = order.Item.Name;
                            }

                            if (order.Item.ID == "999" && !itemName.Contains("自备"))
                            {
                                itemName += "[自备]";
                            }
                            buffer.Append(itemName);
                            #endregion

                            //药品名称后面增加规格显示
                            if (phaItem != null)
                            {
                                buffer.Append("[" + phaItem.Specs + "]");
                            }
                            buffer.Append(orderManager.TransHypotest(order.HypoTest));                           
                            buffer.Append(" ");

                            ord = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();
                            string specs = "";
                            #region 药品名称后面显示信息（基本剂量、、规格、总量等）
                            
                            if (!string.IsNullOrEmpty(order.DoseOnceDisplay))
                            {
                                specs = order.DoseOnceDisplay.Trim() + order.DoseUnit;
                            }
                            else
                            {
                                specs = order.DoseOnce + order.DoseUnit;
                            }
                            
                            buffer.Append("每次"+specs);

                            buffer.Append(" 共");
                            if (this.bEnglish)
                            {
                                buffer.Append(order.Qty.ToString());
                            }
                            else
                            {
                                buffer.Append(order.Qty.ToString() + order.Unit);
                            }
                            
                            #endregion
                            

                            buffer.Append(" ");

                            //院注次数显示 if (order.InjectCount > 0)

                            #region 备注
                            if (!string.IsNullOrEmpty(order.Memo))
                            {
                                buffer.Append(" 备注:");
                                buffer.Append(order.Memo);
                            }
                            #endregion

                            buffer.Append("\n");

                            #region 用法显示

                            #region 单条显示

                            if (combLabel == parm[0])
                            {
                                if (this.bEnglish)
                                {
                                    buffer.Append("    Usage:");
                                }
                                else
                                {
                                    buffer.Append("    用法:");
                                }
                                buffer.Append(" ");

                                #region 每次量
                                if (!string.IsNullOrEmpty(order.DoseOnceDisplay))
                                {
                                    buffer.Append(order.DoseOnceDisplay.Trim());
                                }
                                else
                                {
                                    buffer.Append(order.DoseOnce.ToString());
                                }

                                buffer.Append(order.DoseUnit);
                                #endregion

                                buffer.Append(" ");

                                #region 用法

                                if (this.bEnglish)
                                {
                                    buffer.Append(order.Usage.ID);
                                }
                                else
                                {
                                    buffer.Append(order.Usage.Name);
                                }
                                #endregion

                                buffer.Append("   ");

                                #region 频次
                                if ((order.Frequency.Name == null || order.Frequency.Name.Length <= 0) && this.freHelper != null && this.freHelper.ArrayObject.Count > 0)
                                {
                                    objFre = this.freHelper.GetObjectFromID(order.Frequency.ID);
                                    if (objFre != null)
                                    {
                                        if (this.bEnglish)
                                        {
                                            buffer.Append(objFre.ID.ToLower());
                                        }
                                        else
                                        {
                                            buffer.Append(objFre.Name);
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.bEnglish)
                                    {
                                        buffer.Append(order.Frequency.ID.ToLower());
                                    }
                                    else
                                    {
                                        buffer.Append(order.Frequency.ID.ToLower());
                                    }
                                }
                                #endregion                                                               

                                #region 天数
                                //buffer.Append("×");
                                //if (order.HerbalQty <= 0)
                                //{
                                //    order.HerbalQty = 1;
                                //}

                                //buffer.Append(order.HerbalQty.ToString());
                                #endregion

                                buffer.Append(" ");



                                buffer.Append("\n");
                            }
                            #endregion

                            #region 组合显示

                            else if (combLabel == parm[3])
                            {
                                for (int kk = 0; kk < al.Count; kk++)
                                {
                                    order = al[kk] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                                    if (kk == 0)
                                    {
                                        if (this.bEnglish)
                                        {
                                            buffer.Append("    Usage:");
                                        }
                                        else
                                        {
                                            buffer.Append("    用法:");
                                        }
                                        buffer.Append(" ");

                                        #region 用法
                                        //buffer.Append(printUsage);
                                        if (this.bEnglish)
                                        {
                                            buffer.Append(order.Usage.ID);
                                        }
                                        else
                                        {
                                            buffer.Append(order.Usage.Name);
                                        }
                                        #endregion

                                        buffer.Append(" ");

                                        #region 频次

                                        if ((order.Frequency.Name == null
                                            || order.Frequency.Name.Length <= 0)
                                            && this.freHelper != null
                                            && this.freHelper.ArrayObject.Count > 0)
                                        {
                                            objFre = this.freHelper.GetObjectFromID(order.Frequency.ID);
                                            if (objFre != null)
                                            {
                                                if (this.bEnglish)
                                                {
                                                    buffer.Append(objFre.ID.ToLower());
                                                }
                                                else
                                                {
                                                    buffer.Append(objFre.ID.ToLower());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (this.bEnglish)
                                            {
                                                buffer.Append(order.Frequency.ID.ToLower());
                                            }
                                            else
                                            {
                                                buffer.Append(order.Frequency.ID.ToLower());
                                            }
                                        }
                                        #endregion

                                        #region 天数
                                        //buffer.Append("×");
                                        //if (order.HerbalQty <= 0)
                                        //{
                                        //    order.HerbalQty = 1;
                                        //}
                                        //buffer.Append(order.HerbalQty.ToString());
                                        #endregion

                                        //buffer.Append(" ");

                                        //buffer.Append(order.Memo);
                                        buffer.Append("\n");
                                    }
                                }
                            }
                            #endregion
                            #endregion
                        }
                        #endregion
                    }
                    //加虚线分隔
                    if (this.alOrder.Count > 0)
                    {
                        buffer.Append("------------------------------------------------------------------");
                    }
                    buffer.Append("\n");
                    #endregion
                }
            }
            #endregion

            buffer.Append("\n以下空白");
            this.lblOrder.Text = buffer.ToString();
            this.lblPhaMoney.Text = phaMoney.ToString();
            this.drugRecipe = drugManager.GetDrugRecipe(this.drugDept, this.recipeId);
            if (this.drugDept == null)
            {
                MessageBox.Show("查询处方发药信息错误：" + drugManager.Err);
                return;
            }
            this.SetPatientInfo(this.myReg);

            //lblTotal.Text = "(本次诊疗 药费:￥" + phaMoney.ToString("F2") + " 注射费:￥" + injectMoney.ToString("F2") + " 应付总计:￥" + (phaMoney + injectMoney).ToString("F2") + ")";

            if (!string.IsNullOrEmpty(drugRecipe.DrugedOper.ID))
            {
                if (hsEmpl.Contains(drugRecipe.DrugedOper.ID))
                {
                    oper = hsEmpl[drugRecipe.DrugedOper.ID] as Neusoft.HISFC.Models.Base.Employee;
                }
                else
                {
                    oper = this.interMgr.GetEmployeeInfo(drugRecipe.DrugedOper.ID);
                    this.hsEmpl.Add(drugRecipe.DrugedOper.ID, oper.Clone());
                }
                this.lblDrugDoct.Text = oper.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + oper.Name;
            }
            else
            {
                this.lblDrugDoct.Text = "";
            }
            if (!string.IsNullOrEmpty(drugRecipe.SendOper.ID))
            {
                if (hsEmpl.Contains(drugRecipe.SendOper.ID))
                {
                    oper = hsEmpl[drugRecipe.SendOper.ID] as Neusoft.HISFC.Models.Base.Employee;
                }
                else
                {
                    oper = this.interMgr.GetEmployeeInfo(drugRecipe.SendOper.ID);
                    this.hsEmpl.Add(drugRecipe.SendOper.ID, oper.Clone());
                }
                this.lblSendDoct.Text = oper.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + oper.Name;
            }
            else
            {
                this.lblSendDoct.Text = "";
            }

            if (this.isPrintSignInfo == -1)
            {
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                isPrintSignInfo = controlMgr.GetControlParam<int>("HNMZ12", true, 0);
            }

            if (isPrintSignInfo == 1)
            {
                this.lblPhaDoc.Visible = true;
                this.lblDrugDoct.Visible = true;
                this.lblSendDoct.Visible = true;
                //this.lblPhaMoney.Visible = true;
            }
            else
            {
                this.lblPhaDoc.Visible = false;
                this.lblDrugDoct.Visible = false;
                this.lblSendDoct.Visible = false;
                //this.lblPhaMoney.Visible = false;
            }
        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }

        /// <summary>
        /// 获取字节长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padLength"></param>
        /// <returns></returns>
        private string SetToByteLength(string str, int padLength)
        {
            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[^\x00-\xff]"))
                {
                    len += 1;
                }
            }

            if (padLength - str.Length - len > 0)
            {
                return str + "".PadRight(padLength - str.Length - len, ' ');
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <param name="usageID">一组统一显示的用法</param>
        /// <returns></returns>
        private ArrayList GetOrderByCombId(Neusoft.HISFC.Models.Order.OutPatient.Order order, ref string usage)
        {
            ArrayList al = new ArrayList();

            Neusoft.HISFC.Models.Order.OutPatient.Order ord = null;
            if (!hsCombID.Contains(order.Combo.ID))
            {
                for (int i = 0; i < this.alTemp.Count; i++)
                {
                    ord = alTemp[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                    if (order.Combo.ID == ord.Combo.ID)
                    {
                        al.Add(ord);
                    }
                }
                hsCombID.Add(order.Combo.ID, order);
            }

            return al;
        }

        #region 显示信息
        private void SetShowMainInfo()
        {
            this.chkPub.Text = "公费";
            this.chkPay.Text = "医保";
            this.chkOwn.Text = "自费";
            this.chkOth.Text = "其他";
            this.chkMale.Text = "男";
            this.chkFemale.Text = "女";

            if (this.bEnglish)
            {
                this.ChangeToEnglish();
            }

            #region 显示体重信息

            Neusoft.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();
            string height = "";
            string weight = "";
            string SBP = "";
            string DBP = "";
            string tem = "";//体温
            string bloodGlu = ""; //血糖
            if (outOrderMgr.GetHealthInfo(this.myReg.ID, ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu) == -1)
            {
                this.lblHeight.Text = "";
            }
            else
            {
                if (string.IsNullOrEmpty(weight))
                {
                    this.lblHeight.Text = "";
                }
                else
                {
                    this.lblHeight.Text = "体重" + weight.ToString() + "千克";
                }
            }
            #endregion

            if (this.speRecipeLabel.Length <= 0)
            {
                if (this.alOrder == null || this.alOrder.Count <= 0)
                {
                    return;
                }
                ////判断是否急诊？ 妇幼要求在急诊时间段都是打印急诊处方
                //if (this.regIntegrate.IsEmergency((this.alOrder[0] as Neusoft.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID, (this.alOrder[0] as Neusoft.HISFC.Models.Order.OutPatient.Order).MOTime))
                //{
                //    this.speRecipeLabel = "急 诊";
                //    if (string.IsNullOrEmpty(this.lblHeight.Text))
                //    {
                //        this.lblHeight.Text = "体重    千克";
                //    }
                //}
                //儿科和急诊科显示体重信息
                //根据第一条医嘱的开立科室判断 houwb
                else if (deptHelper.GetObjectFromID((this.alOrder[0] as Neusoft.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID).Name.IndexOf("急") >= 0)
                {
                    this.speRecipeLabel = "急 诊";
                    if (string.IsNullOrEmpty(this.lblHeight.Text))
                    {
                        this.lblHeight.Text = "体重   千克";
                    }
                }
                else if (deptHelper.GetObjectFromID((this.alOrder[0] as Neusoft.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID).Name.IndexOf("儿") >= 0)
                {
                    this.speRecipeLabel = "儿 科";
                    if (string.IsNullOrEmpty(this.lblHeight.Text))
                    {
                        this.lblHeight.Text = "体重   千克";
                    }
                }
            }
            /*    不用调整了
            //调整控件位置
            if (string.IsNullOrEmpty(this.lblHeight.Text))
            {
                this.label12.Location = new Point(2, 181);
                this.lblCardNo.Location = new Point(122, 181);
                this.label10.Location = new Point(271, 181);
                this.lblSeeDept.Location = new Point(402, 181);
            }
            else
            {
                this.label12.Location = new Point(108, 181);
                this.lblCardNo.Location = new Point(224, 181);
                this.label10.Location = new Point(332, 181);
                this.lblSeeDept.Location = new Point(464, 181);
            }
            */
            if (this.speRecipeLabel.Length > 0)
            {
                this.lblJS.Visible = true;
            }
            else
            {
                this.lblJS.Visible = false;
            }
            this.lblJS.Text = this.speRecipeLabel;
        }
        #endregion

        /// <summary>
        /// 转换成英文
        /// </summary>
        private void ChangeToEnglish()
        {
            label14.Text = "FeeType:";
            label5.Text = "RecipeNum:";
            label9.Text = "MedicalNum:";
            label6.Text = "Name:";
            label7.Text = "Sex:";
            label8.Text = "Age:";
            label10.Text = "Dept:";
            label12.Text = "CardNum:";
            label19.Text = "Diagnose:";
            label31.Text = "Date:";
            label20.Text = "Phone/Address:";
            label22.Text = "Doctor:";
            label3.Text = "Cost:";
            label24.Text = "Audit Apothecary:";
            label25.Text = "Prepare Apothecary:";
            label27.Text = "Check Apothecary:";

            this.chkOwn.Text = "OwnCost";
            this.chkPub.Text = "PubCost";
            this.chkPay.Text = "PayCost";
            this.chkOth.Text = "Ohter";
            this.chkMale.Text = "Male";
            this.chkFemale.Text = "Female";

            //label1.Text = "         Recipe";
            lblJS.Text = "";
        }

        /// <summary>
        /// 从消耗品和医嘱数组中移除医嘱
        /// </summary>
        /// <param name="alOrder"></param>
        /// <param name="alOrderAndSub"></param>
        private void RemoveOrderFromArray(ArrayList alOrder, ref ArrayList alOrderAndSub)
        {
            if (alOrder == null || alOrder.Count == 0)
            {
                return;
            }
            if (alOrderAndSub == null || alOrderAndSub.Count == 0)
            {
                return;
            }
            ArrayList alTemp = new ArrayList();
            for (int i = 0; i < alOrderAndSub.Count; i++)
            {
                Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item = alOrderAndSub[i] as Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList;
                for (int j = 0; j < alOrder.Count; j++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order temp = alOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                    if ((temp.ReciptNO == item.RecipeNO) && (temp.SequenceNO == item.SequenceNO))
                    {
                        item.Item.MinFee.User03 = "1";
                    }
                }
            }
            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList item in alOrderAndSub)
            {
                if (item.Item.MinFee.User03 != "1")
                {
                    alTemp.Add(item);
                }
            }
            alOrderAndSub = alTemp;
        }

        /// <summary>
        /// 频次信息
        /// </summary>
        Neusoft.HISFC.Models.Order.Frequency frequencyObj = null;

        /// <summary>
        /// 根据频次获得每天剂数
        /// </summary>
        /// <param name="frequencyID"></param>
        /// <returns></returns>
        private int GetFrequencyCount(string frequencyID)
        {
            if (string.IsNullOrEmpty(frequencyID))
            {
                return -1;
            }
            string id = frequencyID.ToLower();
            if (id == "qd")//每天一次
            {
                return 1;
            }
            else if (id.Contains("qd"))
            {
                return 1;
            }
            else if (id == "bid")//每天两次
            {
                return 2;
            }
            else if (id == "tid")//每天三次
            {
                return 3;
            }
            else if (id == "hs")//睡前
            {
                return 1;
            }
            else if (id == "qn")//每晚一次
            {
                return 1;
            }
            else if (id == "qid")//每天四次
            {
                return 4;
            }
            else if (id == "pcd")//晚餐后
            {
                return 1;
            }
            else if (id == "pcl")//午餐后
            {
                return 1;
            }
            else if (id == "pcm")//早餐后
            {
                return 1;
            }
            else if (id == "prn")//必要时服用
            {
                return 1;
            }
            else if (id == "遵医嘱")
            {
                return 1;
            }
            else
            {
                if (freHelper != null)
                {
                    frequencyObj = freHelper.GetObjectFromID(frequencyID) as Neusoft.HISFC.Models.Order.Frequency;
                }

                if (frequencyObj == null)
                {
                    ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID.ToLower());

                    if (alFrequency != null && alFrequency.Count > 0)
                    {
                        Neusoft.HISFC.Models.Order.Frequency obj = alFrequency[0] as Neusoft.HISFC.Models.Order.Frequency;
                        string[] str = obj.Time.Split('-');
                        return str.Length;
                    }
                }

                return -1;
            }
        }

        #endregion

        /// <summary>
        /// 预览打印
        /// </summary>
        public void Preview()
        {
            SetShowMainInfo();
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 575, 790));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintDocument.DefaultPageSettings.Landscape = false;
            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            }
            print.PrintPreview(5, 5, this);
        }

        /// <summary>
        /// print the recipe 白纸打印
        /// </summary>
        public void Print()
        {
            SetShowMainInfo();
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new Neusoft.HISFC.Models.Base.PageSize("A5", 575, 790));
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintDocument.DefaultPageSettings.Landscape = false;

            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            }
            if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        #region IDrugPrint 成员

        public void AddAllData(ArrayList al, Neusoft.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new NotImplementedException();
        }

        public void AddAllData(ArrayList al, Neusoft.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            throw new NotImplementedException();
        }

        public void AddAllData(ArrayList al)
        {
            return;
        }

        public void AddCombo(ArrayList alCombo)
        {
            return;
        }

        public void AddSingle(Neusoft.HISFC.Models.Pharmacy.ApplyOut info)
        {
            return;
        }

        public decimal DrugTotNum
        {
            set { throw new NotImplementedException(); }
        }

        public Neusoft.HISFC.Models.RADT.PatientInfo InpatientInfo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal LabelTotNum
        {
            set { throw new NotImplementedException(); }
        }

        public Neusoft.HISFC.Models.Registration.Register OutpatientInfo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        #endregion

        #region IRecipePrint 成员实现
        //设置患者基本信息       
        public int SetPatientInfo(Neusoft.HISFC.Models.Registration.Register register)
        {
            this.myReg = register;
            if (this.myReg == null)
            {
                return 1;
            }
            try
            {
                GetHospLogo();
                if (drugQuaulityHelper.ArrayObject.Count <= 0)
                {
                    //取药品剂型
                    ArrayList alDrugQuaulity = interMgr.GetConstantList("DRUGQUALITY");

                    if (alDrugQuaulity != null && alDrugQuaulity.Count > 0)
                    {
                        drugQuaulityHelper.ArrayObject = alDrugQuaulity;
                    }
                }
            }
            catch
            { }
            this.lblName.Text = this.myReg.Name;
            if (this.myReg.Pact.PayKind.ID == "03")
            {
                try
                {
                    Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pact = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();
                    Neusoft.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(this.myReg.Pact.ID);
                }
                catch
                { }
            }
            //年龄按照统一格式
            this.lblAge.Text = this.orderManager.GetAge(this.myReg.Birthday, false);
            if (this.myReg.Sex.Name == "男")
            {
                this.chkMale.Checked = true;
                this.chkFemale.Checked = false;
            }
            else
            {
                this.chkMale.Checked = false;
                this.chkFemale.Checked = true;
            }
            this.lblMCardNo.Text = this.myReg.SSN;
            this.lblCardNo.Text = myReg.PID.CardNO;
            this.lblCard.Text = "*" + myReg.PID.CardNO + "*";
            this.npbBarCode.Image = this.CreateBarCode(myReg.PID.CardNO);
            this.chkOwn.Checked = false;
            this.chkPay.Checked = false;
            this.chkPub.Checked = false;
            this.chkOth.Checked = false;

            if (this.myReg.Pact.PayKind.ID == "01")
            {
                this.chkOwn.Checked = true;
            }
            else if (this.myReg.Pact.PayKind.ID == "02")
            {
                this.chkPay.Checked = true;
            }
            else
            {
                this.chkPub.Checked = true;
            }           

            if (myReg.AddressHome != null && myReg.AddressHome.Length > 0)
            {
                this.lblTel.Text = myReg.AddressHome + "/" + myReg.PhoneHome;
            }
            else
            {
                this.lblTel.Text = myReg.PhoneHome;
            }
            #region 诊断
            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(this.myReg.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            string strDiag = "";
            int i = 1;
            foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    //strDiag += i.ToString()+diag.DiagInfo.ICD10.Name + "、";
                    strDiag += i.ToString() + "、" + diag.DiagInfo.ICD10.Name + "； ";
                    i++;
                }
            }
            //lblDiag.Text = strDiag.TrimEnd('、');
            lblDiag.Text = strDiag;
            #endregion
            return 1;
        }

        //当前处方号
        private string recipeId = "";
        public string RecipeNO
        {
            get
            {
                return this.recipeId;
            }
            set
            {
                this.recipeId = value;
                this.speRecipeLabel = "";
                this.Query();
            }
        }

        //打印
        public int PrintRecipe()
        {
            this.Print();
            return 1;
        }

        //预览打印
        public int PrintRecipeView(ArrayList alRecipe)
        {
            Preview();
            return 1;
        }

        //打印机名称
        private string printer = "";
        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }

        #endregion
    }

    public class OrderCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order order1 = x as Neusoft.HISFC.Models.Order.OutPatient.Order;
                Neusoft.HISFC.Models.Order.OutPatient.Order order2 = y as Neusoft.HISFC.Models.Order.OutPatient.Order;

                if (order1.SortID > order2.SortID)
                {
                    return 1;
                }
                else if (order1.SortID == order2.SortID)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion
    }

    /// <summary>
    /// 用法排序：一组内可能用法不一致，此处只显示一个用法
    /// </summary>
    public class UsageCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order order1 = x as Neusoft.HISFC.Models.Order.OutPatient.Order;
                Neusoft.HISFC.Models.Order.OutPatient.Order order2 = y as Neusoft.HISFC.Models.Order.OutPatient.Order;

                if (Neusoft.FrameWork.Function.NConvert.ToInt32(order1.Usage.ID) < Neusoft.FrameWork.Function.NConvert.ToInt32(order2.Usage.ID))
                {
                    return 1;
                }
                else if (Neusoft.FrameWork.Function.NConvert.ToInt32(order1.Usage.ID) == Neusoft.FrameWork.Function.NConvert.ToInt32(order2.Usage.ID))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion
    }
}
