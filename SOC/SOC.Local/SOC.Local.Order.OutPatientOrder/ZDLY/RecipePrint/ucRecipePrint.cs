using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Order.OutPatientOrder.Common;

namespace FS.SOC.Local.Order.OutPatientOrder.ZDLY.RecipePrint
{
    public partial class ucRecipePrint : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IRecipePrint, FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint, FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
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
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        FS.HISFC.BizLogic.Order.OutPatient.Order orderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        FS.HISFC.BizLogic.Pharmacy.DrugStore drugManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();

        /// <summary>
        /// 参数帮助类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        #endregion

        /// <summary>
        /// 药品性质帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper drugQuaulityHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 特殊处方标记
        /// </summary>
        string speRecipeLabel = "";


        /// <summary>
        /// 医嘱排序
        /// </summary>
        OrderCompare orderCompare = new OrderCompare();

        /// <summary>
        /// 门诊处方调剂实体
        /// </summary>
        FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = new FS.HISFC.Models.Pharmacy.DrugRecipe();

        /// <summary>
        /// 药房编码
        /// </summary>
        string drugDept = "";

        /// <summary>
        /// 操作员
        /// </summary>
        FS.HISFC.Models.Base.Employee oper = null;

        /// <summary>
        /// 人员列表
        /// </summary>
        Hashtable hsEmpl = new Hashtable();

        /// <summary>
        /// 科室帮助类
        /// </summary>
        //private static FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper freHelper = null;

        /// <summary>
        /// 频次帮助类
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper FreHelper
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


        ArrayList alSort = new ArrayList();

        /// <summary>
        /// 判断是否为双字符的正则表达式
        /// </summary>
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{15,18}$");


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
        private FS.HISFC.Models.Registration.Register myReg;

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
        private FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 合同单位帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper pactCodeMarkHelper = new FS.FrameWork.Public.ObjectHelper();
        #endregion

        private void GetHospLogo()
        {

            return;
           // Common.ComFunc cf = new ComFunc();
           // string erro = "出错";
           // string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + cf.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
           //picLogo.Image = Image.FromFile(imgpath);

            try
            {
                System.IO.MemoryStream image = new System.IO.MemoryStream(((FS.HISFC.Models.Base.Hospital)this.orderManager.Hospital).HosLogoImage);
                this.picLogo.Image = Image.FromStream(image);

            }
            catch
            {

            }

        }
       
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
            GetHospLogo();

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
               
                printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", false, 1);
            }

            if (freHelper == null || freHelper.ArrayObject.Count == 0)
            {
                freHelper = new FS.FrameWork.Public.ObjectHelper();
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
        /// 查询医嘱
        /// </summary>
        private void Query()
        {
            Init();
            //if (deptHelper.ArrayObject.Count <= 0)
            //{
            //    //deptHelper.ArrayObject = interMgr.GetDepartment();
            //    deptHelper.ArrayObject
            //    if (deptHelper.ArrayObject == null)
            //    {
            //        MessageBox.Show(interMgr.Err);
            //    }
            //}
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
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFees)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order ot = new FS.HISFC.Models.Order.OutPatient.Order();
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
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in al)
            {
                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.alOrder.Add(order); 
                }
            }
            this.MakaLabel(this.alOrder);

            SetShowMainInfo();
        }

        /// <summary>
        /// 返回组号
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int GetSubCombNo(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            int subCombNo = 1; 
            Hashtable hsCombNo = new Hashtable();

            foreach (FS.HISFC.Models.Order.OutPatient.Order ord in alOrder)
            {
                if (ord.SubCombNO < order.SubCombNO && !hsCombNo.Contains(ord.Combo.ID))
                {
                    hsCombNo.Add(ord.Combo.ID, ord);
                    subCombNo++;
                }
            }
            return subCombNo;
        }

        string feeInfo = "";

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
            //decimal ownPhaMoney = 0m;//自费药费
            decimal injectMoney = 0m;//注射费
            //decimal ownInjectMoney = 0m;//自费注射费
            string drugType = "";

            #region 查询所有收费项目（附材）

            string feeSeq = "";
            ArrayList alSubAndOrder = null;

            alSubAndOrder = new ArrayList();
            foreach (FS.HISFC.Models.Order.OutPatient.Order OutPatientOrder in alOrder)
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
            }

            #endregion
        

            #region /*画组合符号*/

            Hashtable hsCombNo = new Hashtable();

            //按照sortID排序
            alOrder.Sort(orderCompare);
            FS.HISFC.Models.Order.OutPatient.Order order = null;
            for (int i = 0; i < alOrder.Count; i++)
            {
                order = alOrder[i] as FS.HISFC.Models.Order.OutPatient.Order;
                
                if (order == null)
                {
                    continue;
                }
                if (i == 0)
                {
                    drugType = order.Item.SysClass.ID.ToString();
                    this.lblRecipeNo.Text = order.ReciptNO ;
                    if (hsEmpl.Contains(order.ReciptDoctor.ID))
                    {
                        oper = hsEmpl[order.ReciptDoctor.ID] as FS.HISFC.Models.Base.Employee;
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

                    FS.FrameWork.Models.NeuObject pactObject = pactCodeMarkHelper.GetObjectFromID(this.myReg.Pact.ID);

                 
                   
                    drugDept = order.StockDept.ID;
                }
                lblSeeDate.Text = this.orderManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
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
                            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem in alSubAndOrder)
                            {
                                if (feeItem.Order.Combo.ID == order.Combo.ID)
                                {
                                    if (feeItem.Item.IsMaterial)
                                    {
                                        injectMoney += feeItem.FT.OwnCost + feeItem.FT.PayCost + feeItem.FT.PubCost;
                                    }
                                    else
                                    {
                                        foreach (FS.HISFC.Models.Order.OutPatient.Order ord in alOrder)
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
                        //alFee = this.outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(order.Combo.ID, this.myReg.ID);
                        ////alFee = this.outPatientManager.QueryFeeDetailByClinicCodeAndRecipeNO(this.myReg.ID, order.ReciptNO, "ALL");
                        //if (alFee != null && alFee.Count >= 1)
                        //{
                        //    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                        //    {
                        //        if (!itemlist.Item.IsMaterial)
                        //        {
                        //            itemlist.FT.TotCost = itemlist.FT.OwnCost + itemlist.FT.PayCost + itemlist.FT.PubCost;
                        //            phaMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//药品金额
                        //            ownPhaMoney += itemlist.FT.OwnCost;
                        //        }
                        //        else 
                        //        {
                        //            injectMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//注射费
                        //        }


                        //    }
                        //}
                    }
                    catch
                    { }
                    #endregion

                    #region 形成字符串

                    #region 草药
                    if (drugType == "PCC")
                    {
                        #region 草药处理

                        decimal days = 1m;
                        string freq = "";
                        string usage = "";

                        for (int k = 0; k < al.Count; k++)
                        {
                            order = al[k] as FS.HISFC.Models.Order.OutPatient.Order;

                            days = order.HerbalQty;
                            freq = order.Frequency.Name;

                            try
                            {
                                //草药用法只有系统类别为HJZ的才是正常用法，特殊用法不统一打印
                                if (((FS.HISFC.Models.Base.Const)usageHelper.GetObjectFromID(order.Usage.ID)).UserCode == "HJZ")
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

                            if (order.Memo != ""&& !order.Memo.Contains("煎服"))
                            {
                               itemName = order.Item.Name +  "(" + order.Memo + ")";
                            }
                            else
                            {
                                itemName = order.Item.Name;
                            }

                            if (order.Item.ID == "999" && !itemName.Contains("自备"))
                            {
                                itemName += "[自备]";
                            }
                            buffer.Append(" ");
                            buff.Append(itemName);

                            string name = buff.ToString();
                            string dose = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;
                            string cost = "  " + priceAndCost;
                            try
                            {
                                buffer.Append(SetToByteLength((SetToByteLength(name, 15) + dose), 19));
                            }
                            catch { }
                            if ((k + 1) % 3 == 0 || k == al.Count - 1)
                            {
                                buffer.Append("\n");
                            }
                        }

                        buffer.Append("\n\n\n");                     
                        buffer.Append("    剂量：");
                        buffer.Append(days.ToString());
                        buffer.Append("剂");
                        buffer.Append("\n");                     
                        buffer.Append("    Sig: ");
                        buffer.Append(usage);
                        buffer.Append("每日" + GetFrequencyCount(order.Frequency.ID) + "剂");
                        buffer.Append(" ");
                      
                        //判断是否是煎药方式
                      
                            if (!string.IsNullOrEmpty(order.Usage.Name))
                            {
                                buffer.Append("(" + order.Usage.Name + ")");
                            }                            
                
                         buffer.Append("\n");
                        #endregion
                    }
                    #endregion

                    #region 西药
                    else
                    {
                        #region 西药、成药
                        FS.HISFC.BizLogic.Order.OutPatient.Order ord = null;
                        FS.FrameWork.Models.NeuObject quaulity = null;
                        FS.HISFC.Models.Pharmacy.Item phaItem = null;
                        FS.FrameWork.Models.NeuObject objFre = null;
                        string combLabel = "";
                        for (int k = 0; k < al.Count; k++)
                        {
                            order = al[k] as FS.HISFC.Models.Order.OutPatient.Order;

                            #region 组合标记

                            if (al.Count == 1)
                            {
                                //buffer.Append(parm[0]);

                                combLabel = parm[0];
                            }
                            else
                            {
                                if (k == 0)
                                {
                                    //buffer.Append(parm[1]);

                                    combLabel = parm[1];
                                }
                                else if (k == al.Count - 1)
                                {
                                   // buffer.Append(parm[3]);

                                    combLabel = parm[3];
                                }
                                else
                                {
                                   // buffer.Append(parm[2]);

                                    combLabel = parm[2];
                                }
                            }
                            #endregion

                            #region 组号
                            /*
                            buffer.Append("[");
                            buffer.Append(this.GetSubCombNo(order).ToString());
                            buffer.Append("]");
                            */ 

                            if (k == 0)
                            {
                                buffer.Append(this.GetSubCombNo(order).ToString() + "、");
                            }
                            else
                            {
                                buffer.Append("   ");
                            }
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
                                if (printItemNameType == -1)
                                {
                                    FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                                    printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
                                }

                                if (printItemNameType == 0)
                                {
                                    itemName = phaItem.Name;
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                    {
                                        itemName = phaItem.Name;
                                    }
                                    else
                                    {
                                        itemName = phaItem.NameCollection.RegularName;
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
                            buffer.Append((itemName + orderManager.TransHypotest(order.HypoTest)).PadRight(15, ' '));
                            #endregion

                            buffer.Append(" ");

                            ord = new FS.HISFC.BizLogic.Order.OutPatient.Order();
                           
                       
                            #region 药品名称后面显示信息（规格、总量等）
                           
                            buffer.Append(phaItem.Specs);

                            buffer.Append("×");

                            buffer.Append(order.Qty.ToString() + order.Unit);

                            #endregion

                            buffer.Append(" ");

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
                                buffer.Append("    Sig:");

                                buffer.Append(" ");
                                #region 用法

                                buffer.Append(order.Usage.Name);

                                #endregion

                                #region 每次量
                                buffer.Append(" "+order.DoseOnce.ToString());

                                buffer.Append(order.DoseUnit);
                                #endregion

                                buffer.Append(" ");


                                buffer.Append("   ");

                                #region 频次
                                if ((order.Frequency.Name == null || order.Frequency.Name.Length <= 0) && this.freHelper != null && this.freHelper.ArrayObject.Count > 0)
                                {
                                    objFre = this.freHelper.GetObjectFromID(order.Frequency.ID);
                                    if (objFre != null)
                                    {
                                        buffer.Append(objFre.Name);
                                    }
                                }
                                else
                                {
                                    buffer.Append(order.Frequency.Name);
                                }
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
                                    order = al[kk] as FS.HISFC.Models.Order.OutPatient.Order;

                                    if (kk == 0)
                                    {
                                        buffer.Append("    Sig:");
                                        buffer.Append("\n");
                                        
                                    }

                                    phaItem = phaIntegrate.GetItem(order.Item.ID);

                                    if (printItemNameType == 0)
                                    {
                                        itemName = phaItem.Name;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                        {
                                            itemName = phaItem.Name;
                                        }
                                        else
                                        {
                                            itemName = phaItem.NameCollection.RegularName;
                                        }
                                    }


                                    buffer.Append("          "+itemName);
                                    buffer.Append(" "+order.DoseOnce.ToString());
                                    buffer.Append(order.DoseUnit);
                                    buffer.Append("\n");


                                    if (kk == al.Count - 1)
                                    {
                                        buffer.Append("              ");
                                        #region 用法
                                        buffer.Append(order.Usage.Name);
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
                                                buffer.Append(objFre.Name.ToLower());
                                            }
                                        }
                                        else
                                        {
                                            buffer.Append(order.Frequency.Name.ToLower());
                                        }
                                        #endregion
                                        buffer.Append("\n");
                                    }
                                }
                            }
                            #endregion
                            #endregion
                        }
                        #endregion
                    }

                    #endregion

                    //加虚线分隔
                    //if (this.alOrder.Count > 0)
                    //{
                    //    buffer.Append("--------------------------------------------------------------------");
                    //}
                    buffer.Append("\n");
                    #endregion
                }
            }
            #endregion
            //buffer.Append("\n以下空白");
            this.lblOrder.Text = buffer.ToString();
            this.lblPhaMoney.Text = phaMoney.ToString();
            this.lblSubFee.Text = injectMoney.ToString();

            //feeInfo = "(本次诊疗 药费:￥" + phaMoney.ToString("F2") + " 注射费:￥" + injectMoney.ToString("F2") + " 应付总计:￥" + (phaMoney + injectMoney).ToString("F2") + ")";

            //lblTotal.Text = feeInfo;

         //   this.drugRecipe = drugManager.GetDrugRecipe(this.drugDept, this.recipeId); this.lblSeeDate.Text =
            if (this.drugDept == null)
            {
                MessageBox.Show("查询处方发药信息错误：" + drugManager.Err);
                return;
            }
            this.SetPatientInfo(this.myReg);
            if (!string.IsNullOrEmpty(drugRecipe.DrugedOper.ID))
            {
                if (hsEmpl.Contains(drugRecipe.DrugedOper.ID))
                {
                    oper = hsEmpl[drugRecipe.DrugedOper.ID] as FS.HISFC.Models.Base.Employee;
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
                    oper = hsEmpl[drugRecipe.SendOper.ID] as FS.HISFC.Models.Base.Employee;
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
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
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
        private ArrayList GetOrderByCombId(FS.HISFC.Models.Order.OutPatient.Order order, ref string usage)
        {
            ArrayList al = new ArrayList();

            FS.HISFC.Models.Order.OutPatient.Order ord = null;
            if (!hsCombID.Contains(order.Combo.ID))
            {
                for (int i = 0; i < this.alTemp.Count; i++)
                {
                    ord = alTemp[i] as FS.HISFC.Models.Order.OutPatient.Order;
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
            #region 显示体重信息

            //FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
            //outOrderMgr.GetHealthInfo(this.myReg.ID, ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu) == -1
            #endregion

            if (this.speRecipeLabel.Length <= 0)
            {
                if (this.alOrder == null || this.alOrder.Count <= 0)
                {
                    return;
                }
                FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate=new FS.HISFC.BizProcess.Integrate.Registration.Registration();

                //if (regIntegrate.IsEmergencyHolidays((this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID, (this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).MOTime))
                //{
                //    this.speRecipeLabel = "急 诊";
                //}
                if (this.myReg.DoctorInfo.Templet.RegLevel.IsEmergency)
                {
                    this.speRecipeLabel = "急 诊";
                }
                else if (SOC.HISFC.BizProcess.Cache.Common.GetDeptName((this.alOrder[0] as FS.HISFC.Models.Order.OutPatient.Order).ReciptDept.ID).IndexOf("儿") >= 0)
                {
                    this.speRecipeLabel = "儿 科";
                }
            }
        
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
                FS.HISFC.Models.Fee.Outpatient.FeeItemList item = alOrderAndSub[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                for (int j = 0; j < alOrder.Count; j++)
                {
                    FS.HISFC.Models.Order.OutPatient.Order temp = alOrder[j] as FS.HISFC.Models.Order.OutPatient.Order;
                    if ((temp.ReciptNO == item.RecipeNO) && (temp.SequenceNO == item.SequenceNO))
                    {
                        item.Item.MinFee.User03 = "1";
                    }
                }
            }
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList item in alOrderAndSub)
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
        FS.HISFC.Models.Order.Frequency frequencyObj = null;

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
                    frequencyObj = freHelper.GetObjectFromID(frequencyID) as FS.HISFC.Models.Order.Frequency;
                }

                if (frequencyObj == null)
                {
                    ArrayList alFrequency = frequencyManagement.GetBySysClassAndID("ROOT", "ALL", frequencyID.ToLower());

                    if (alFrequency != null && alFrequency.Count > 0)
                    {
                        FS.HISFC.Models.Order.Frequency obj = alFrequency[0] as FS.HISFC.Models.Order.Frequency;
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
            //SetShowMainInfo();
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 790));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
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
            //SetShowMainInfo();
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            
            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 790));
            print.SetPageSize(FS.SOC.Local.Order.OutPatientOrder.ZDLY.Common.Function.getPrintPage(false));


            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.PrintDocument.DefaultPageSettings.Landscape = false;

            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            }
            if (FS.SOC.Local.Order.OutPatientOrder.ZDLY.Common.Function.IsPreview())
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

        #region IDrugPrint 成员

        public void AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new NotImplementedException();
        }

        public void AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
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

        public void AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut info)
        {
            return;
        }

        public decimal DrugTotNum
        {
            set { throw new NotImplementedException(); }
        }

        public FS.HISFC.Models.RADT.PatientInfo InpatientInfo
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

        public FS.HISFC.Models.Registration.Register OutpatientInfo
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
        public int SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.myReg = register;
            if (this.myReg == null)
            {
                return 1;
            }
            this.myReg = this.regIntegrate.GetByClinic(this.myReg.ID);

            if (showEmplLength == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                showEmplLength = controlMgr.GetControlParam<int>("HN0002", true, 6);
            }

            try
            {
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
            this.npbBarCode.Image = FS.SOC.Local.Order.OutPatientOrder.Common.ComFunc.CreateBarCode(this.myReg.PID.CardNO, this.npbBarCode.Width, this.npbBarCode.Height);
           // this.lblSeeDate.Text = IList[0].MOTime.Date.ToString("yyyy") + "-" + IList[0].MOTime.Date.ToString("MM") + "-" + IList[0].MOTime.Date.ToString("dd");
            FS.HISFC.BizLogic.Fee.PactUnitInfo pact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            FS.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(this.myReg.Pact.ID);
            if (info.PayKind.ID == "01")
            {
                this.label4.Visible = true;
                this.lblPercent.Visible = true;
                this.lblPercent.Text = "100%";
            }
            else if (info.PayKind.ID == "03")
            {
                this.label4.Visible = true;
                this.lblPercent.Visible = true;
                this.lblPercent.Text = Convert.ToString(info.Rate.PayRate *100)+"%";
            }
            else
            {
                this.label4.Visible = false;
                this.lblPercent.Visible = false;
            }

            this.lblFeeType.Text = info.Name;
            //年龄按照统一格式
            this.lblAge.Text = this.orderManager.GetAge(this.myReg.Birthday, false);

            this.lblSex.Text = this.myReg.Sex.Name;

            if (!string.IsNullOrEmpty(this.myReg.SeeDoct.Dept.ID))
            {
                this.lblSeeDept.Text = this.interMgr.GetDepartment(this.myReg.SeeDoct.Dept.ID).Name;
            }
            if (!string.IsNullOrEmpty(myReg.SeeDoct.ID))
            {
                this.lblSeeDoctor.Text = this.interMgr.GetEmployeeInfo(myReg.SeeDoct.ID).Name + "(" + myReg.SeeDoct.ID.Substring(6 - showEmplLength, showEmplLength) + ")";
            }



            this.lblMCardNo.Text = this.myReg.SSN;
            this.lblCardNo.Text = myReg.PID.CardNO;
            //
            //this.lblOrderNo.Text = myReg.OrderNO.ToString();
            //
            this.lblOrderNo.Text = myReg.ID.ToString();
        
            if (myReg.AddressHome != null && myReg.AddressHome.Length > 0)
            {
                this.lblTel.Text = myReg.AddressHome + "/" + myReg.PhoneHome;
            }
            else
            {
                this.lblTel.Text = myReg.PhoneHome;
            }
            #region 诊断
            ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(this.myReg.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (al == null)
            {
                MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            string strDiag = "";
            foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            {
                if (diag != null && diag.IsValid)
                {
                    strDiag +=  diag.DiagInfo.ICD10.Name + "/";
                }
            }
            lblDiag.Text = strDiag.TrimEnd('/');
            #endregion

            #region 所有费用
            string[] strMoney = myReg.User01.Split('|');
            //string[] strMoney = feeInfo;

            if (strMoney.Length==3)
            {
                lblTotal.Text = strMoney[0];
                //lblTotal.Text = feeInfo;
                lblDrugCls.Text = strMoney[1];
                lblPage.Text = strMoney[2];                
            }
           
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

            this.MakaLabel(alRecipe);
            SetShowMainInfo();
            this.lblJS.Text = "博济处方";
            this.lblJS.Visible = true;
            SetFee(alRecipe);
            return 1;
        }


        private void SetFee(ArrayList alRecipe)
        {

            decimal phaMoney =0;
            if (alRecipe != null && alRecipe.Count >= 1)
            {
                foreach (FS.HISFC.Models.Order.OutPatient.Order  order in alRecipe)
                {
                    if (!order.Item.IsMaterial)
                    {
                        phaMoney += order.FT.PubCost + order.FT.PayCost + order.FT.OwnCost;//药品金额
                    }

                }
            }

            this.lblPhaMoney.Text = phaMoney.ToString();
        
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




        #region IOutPatientOrderPrint 成员

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> List, bool isPreview)
        {
            throw new NotImplementedException();
        }

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            throw new NotImplementedException();
        }

        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList alOrder, bool isPreview)
        {
            throw new NotImplementedException();
        }

        public void SetPage(string pageStr)
        {
            this.lblPage.Visible = true;
            this.lblPage.Text = pageStr;
            return;
        }

        #endregion
    }


    #region 排序

    public class OrderCompare : IComparer
    {
        #region IComparer 成员

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.OutPatient.Order order1 = x as FS.HISFC.Models.Order.OutPatient.Order;
                FS.HISFC.Models.Order.OutPatient.Order order2 = y as FS.HISFC.Models.Order.OutPatient.Order;

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
                FS.HISFC.Models.Order.OutPatient.Order order1 = x as FS.HISFC.Models.Order.OutPatient.Order;
                FS.HISFC.Models.Order.OutPatient.Order order2 = y as FS.HISFC.Models.Order.OutPatient.Order;

                if (FS.FrameWork.Function.NConvert.ToInt32(order1.Usage.ID) < FS.FrameWork.Function.NConvert.ToInt32(order2.Usage.ID))
                {
                    return 1;
                }
                else if (FS.FrameWork.Function.NConvert.ToInt32(order1.Usage.ID) == FS.FrameWork.Function.NConvert.ToInt32(order2.Usage.ID))
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

    #endregion
}
