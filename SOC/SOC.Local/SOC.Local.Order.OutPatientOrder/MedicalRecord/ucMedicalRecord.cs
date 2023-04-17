using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.SOC.Local.Order.OutPatientOrder.Common;
using Neusoft.FrameWork.Models;
using Neusoft.HISFC.Models.Order.OutPatient;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.MedicalRecord
{
    /// <summary>
    /// 门诊诊疗记录卡
    /// </summary>
    public partial class ucMedicalRecord : UserControl, Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {

        public ucMedicalRecord()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 员工显示工号的位数
        /// </summary>
        private int showEmplLength = -1;

        /// <summary>
        /// 打印处方项目名称是否是通用名：1 通用名；0 商品名
        /// </summary>
        private int printItemNameType = -1;

        /// <summary>
        /// 是否打印签名信息？（否则手工签名）
        /// </summary>
        private int isPrintSignInfo = -1;

        /// <summary>
        /// 是否英文
        /// </summary>
        public bool bEnglish = false;

        /// <summary>
        /// 存储处方组合号
        /// </summary>
        private Hashtable hsCombID = new Hashtable();

        /// <summary>
        /// 用法帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper usageHelper = new Neusoft.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper freHelper = null;

        /// <summary>
        /// 频次信息
        /// </summary>
        Neusoft.HISFC.Models.Order.Frequency frequencyObj = null;
        #endregion


        #region  业务类 
        /// <summary>
        /// 频次原子业务层
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.Frequency frequencyManagement = new Neusoft.HISFC.BizLogic.Manager.Frequency();
        
        /// <summary>
        /// 医嘱原子业务层
        /// </summary>
         Neusoft.HISFC.BizLogic.Order.OutPatient.Order OrderManagement = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
         /// 诊断原子业务层
        /// </summary>
        Neusoft.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new Neusoft.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// 管理
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Manager interMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 药品业务逻辑层
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        #endregion

        #region 函数
        /// <summary>
        /// 生成显示信息
        /// </summary>
        /// <param name="alOrder"></param>
        private void MakaLabel(IList<Neusoft.HISFC.Models.Order.OutPatient.Order> OrderList)
        {
            hsCombID = new Hashtable();

            StringBuilder buffer = new System.Text.StringBuilder();
            //填充数据
            string[] parm = { "  ", "┌", "│", "└" };

            string drugType = OrderList.First().Item.SysClass.ID.ToString();

            if (showEmplLength == -1)
            {
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                showEmplLength = controlMgr.GetControlParam<int>("HN0002", true, 6);
            }

            this.lblPhaDoc.Text = OrderList.First().ReciptDoctor.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + OrderList.First().ReciptDoctor.Name;
              

            #region /*画组合符号*/

            var comparer = new CommonComparer<Neusoft.HISFC.Models.Order.OutPatient.Order>((x, y) => x.SortID - y.SortID);

            //按照sortID排序
            OrderList.ToList().Sort(comparer);

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order outPatientOrder in OrderList)
            {
                string printUsage = "";
                IList<Neusoft.HISFC.Models.Order.OutPatient.Order> al = this.GetOrderByCombId(outPatientOrder, OrderList, ref printUsage);
                if (al.Count == 0)
                {
                    continue;
                }
                else
                {
                    #region 形成字符串

                    if (drugType.Equals("PCC"))
                    {
                        #region 草药处理

                        decimal days = 1m;
                        string freq = "";
                        string usage = "";
                        int k = 0;
                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in al)
                        {
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

                            if (order.Memo != "")
                            {
                                //判断是否是煎药方式
                                if ("自煎、代煎、复渣、代复渣".Contains(order.Usage.Memo))
                                {
                                    buff.Append(order.Item.Name);
                                }
                                else
                                {
                                    buff.Append(order.Item.Name + "(" + order.Memo + ")");
                                }
                            }
                            else
                            {
                                buff.Append(order.Item.Name);
                            }

                            //特殊用法
                            try
                            {
                                //草药用法只有系统类别为HJZ的才是正常用法，特殊用法不统一打印
                                if (((Neusoft.HISFC.Models.Base.Const)usageHelper.GetObjectFromID(order.Usage.ID)).UserCode != "HJZ")
                                {
                                    buff.Append("(" + order.Usage.Name + ")");
                                }
                            }
                            catch
                            {
                            }

                            buff.Append("  ");

                            string name = buff.ToString();
                            string dose = order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + order.DoseUnit;
                            string cost = "  " + priceAndCost;

                            try
                            {
                                buffer.Append(this.SetToByteLength(name + dose, 30));
                            }
                            catch { }
                            if ((k + 1) % 2 == 0 || k == al.Count - 1)
                            {
                                buffer.Append("\n\n");
                            }
                            k++;
                        }

                        buffer.Append("             ");
                        buffer.Append(days.ToString());
                        buffer.Append("剂");
                        buffer.Append("\n");
                        buffer.Append("       用法: ");
                        //buffer.Append("每日一剂");
                        buffer.Append("每日" + GetFrequencyCount(outPatientOrder.Frequency.ID) + "剂");
                        buffer.Append(" ");
                        buffer.Append(usage);

                        //判断是否是煎药方式
                        if ("自煎、代煎、复渣、代复渣".Contains(outPatientOrder.Usage.Memo))
                        {
                            buffer.Append("(" + outPatientOrder.Memo + ")");
                        }

                        #endregion
                    }
                    else if (drugType.Equals("P") || drugType.Equals("PCZ"))
                    {
                        #region 西药、成药

                        //Neusoft.HISFC.Models.Order.OutPatient.Order ord = null;
                        //Neusoft.FrameWork.Models.NeuObject quaulity = null;
                        Neusoft.HISFC.Models.Pharmacy.Item phaItem = null;
                        Neusoft.FrameWork.Models.NeuObject objFre = null;

                        string combLabel = "";
                        int k = 0;
                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in al)
                        {
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
                            buffer.Append(this.GetSubCombNo(order, OrderList).ToString());
                            buffer.Append("]");
                            #endregion

                            phaItem = phaIntegrate.GetItem(order.Item.ID);

                            #region 药品名称

                            if (phaItem != null)
                            {
                                if (this.bEnglish)
                                {
                                    buffer.Append(phaItem.NameCollection.EnglishName);
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
                                        buffer.Append(phaItem.Name);
                                    }
                                    else
                                    {
                                        //2011-6-10 houwb 通用名没有维护时，打印商品名
                                        if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                                        {
                                            buffer.Append(phaItem.Name);
                                        }
                                        else
                                        {
                                            buffer.Append(phaItem.NameCollection.RegularName);
                                        }
                                    }
                                }

                                // quaulity = drugQuaulityHelper.GetObjectFromID(phaItem.Quality.ID);

                                //if (quaulity != null && quaulity.ID.Length > 0)
                                //{
                                //    //处方规范规定，麻醉药品和一类精神药品印刷为“麻、精一”
                                //    if (quaulity.Memo.Trim().IndexOf("精一") >= 0
                                //        || quaulity.Memo.Trim().IndexOf("麻") >= 0)
                                //    {
                                //        speRecipeLabel = "麻、精一";
                                //    }
                                //    else if (quaulity.Memo.Trim().IndexOf("精二") >= 0)
                                //    {
                                //        speRecipeLabel = "精 二";
                                //    }
                                //}
                            }
                            else
                            {
                                buffer.Append(order.Item.Name);
                            }
                            #endregion

                            //药品名称后面增加规格显示
                            //buffer.Append("[" + phaItem.Specs + "]");

                            buffer.Append(" ");

                            //ord = new Neusoft.HISFC.Models.Order.OutPatient.Order();
                            string specs = "";

                            #region 药品名称后面显示信息（基本剂量、、规格、总量等）
                            //妇幼特殊需求：组合的药品不显示基本剂量和总量
                            if (combLabel == parm[0])//单条的显示基本计量信息
                            {
                                specs = phaItem.BaseDose + phaItem.DoseUnit;
                            }
                            else //其他的只显示
                            {
                                if (!string.IsNullOrEmpty(order.ExtendFlag4))
                                {
                                    specs = order.ExtendFlag4.Trim() + order.DoseUnit;
                                }
                                else
                                {
                                    specs = order.DoseOnce + order.DoseUnit;
                                }
                            }
                            buffer.Append(specs);

                            //单条显示
                            if (combLabel == parm[0])
                            {
                                buffer.Append("×");
                                if (this.bEnglish)
                                {
                                    buffer.Append(order.Qty.ToString());
                                }
                                else
                                {
                                    buffer.Append(order.Qty.ToString() + order.Unit);
                                }
                            }
                            #endregion

                            buffer.Append(" ");
                            if (order.InjectCount > 0)
                            {
                                //妇幼无院注次数显示，此处先屏蔽
                                //buffer.Append("  院注:" + order.InjectCount.ToString() + "次");
                            }

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
                                if (!string.IsNullOrEmpty(order.ExtendFlag4))
                                {
                                    buffer.Append(order.ExtendFlag4.Trim());
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

                                buffer.Append("×");

                                #region 天数

                                if (order.HerbalQty <= 0)
                                {
                                    order.HerbalQty = 1;
                                }

                                buffer.Append(order.HerbalQty.ToString());

                                #endregion

                                buffer.Append(" ");

                                #region 备注

                                buffer.Append(order.Memo);
                                #endregion
                                buffer.Append("\n");
                            }
                            #endregion

                            #region 组合显示
                            else if (combLabel == parm[3])
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

                                buffer.Append(printUsage);

                                //if (this.bEnglish)
                                //{
                                //    buffer.Append(order.Usage.ID);
                                //}
                                //else
                                //{
                                //    buffer.Append(order.Usage.Name);
                                //}
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

                                buffer.Append("×");

                                #region 天数

                                if (order.HerbalQty <= 0)
                                {
                                    order.HerbalQty = 1;
                                }

                                buffer.Append(order.HerbalQty.ToString());
                                #endregion

                                buffer.Append(" ");

                                buffer.Append(order.Memo);

                                buffer.Append("\n");
                            }
                            buffer.Append("\n");

                            k++;
                            #endregion
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region 非药品处理
                        foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in al)
                        {
                            #region 组号

                            buffer.Append("[");
                            buffer.Append(this.GetSubCombNo(order, OrderList).ToString());
                            buffer.Append("]");
                            #endregion

                            // 非药品名称
                            buffer.Append(order.Item.Name);

                            buffer.Append(" ");

                            #region 非药品名称后面显示信息（次数、用法、样本、部位等）
                            //单条显示

                            buffer.Append("×");
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

                            #region 用法显示
                            if (!string.IsNullOrEmpty(order.Usage.ID))
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

                            if (this.bEnglish)
                            {
                                buffer.Append(order.Usage.ID);
                            }
                            else
                            {
                                buffer.Append(order.Usage.Name);
                            }
                            }
                            #endregion

                            buffer.Append("   ");

                            #region 样本
                            if (!order.Sample.Equals(null) && !string.IsNullOrEmpty(order.Sample.Name))
                            {
                                buffer.Append("样本：");
                                buffer.Append(order.Sample.Name);
                            }
                            #endregion

                            #region 部位
                            if (!string.IsNullOrEmpty(order.CheckPartRecord))
                            {
                                buffer.Append("检查部位：");
                                buffer.Append(order.CheckPartRecord);
                            }
                            #endregion

                            buffer.Append(" ");


                            #region 备注

                            buffer.Append(order.Memo);

                            #endregion
                            buffer.Append("\n");
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            #endregion

            buffer.Append("\n以下空白");

            this.lblOrder.Text = buffer.ToString();


            //this.SetPatientInfo(this.myReg);


            if (this.isPrintSignInfo == -1)
            {
                Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                isPrintSignInfo = controlMgr.GetControlParam<int>("HNMZ12", true, 0);
            }

            if (isPrintSignInfo == 1)
            {
                this.lblPhaDoc.Visible = true;
            }
            else
            {
                this.lblPhaDoc.Visible = false;
            }
        }

        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <param name="usageID">一组统一显示的用法</param>
        /// <returns></returns>
        private IList<Neusoft.HISFC.Models.Order.OutPatient.Order> GetOrderByCombId(Neusoft.HISFC.Models.Order.OutPatient.Order order, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IorderList, ref string usage)
        {
            IList<Neusoft.HISFC.Models.Order.OutPatient.Order> al = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();

            if (!hsCombID.Contains(order.Combo.ID))
            {
                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order ord in IorderList)
                {
                    if (order.Combo.ID == ord.Combo.ID)
                    {
                        al.Add(ord);
                    }
                }
                hsCombID.Add(order.Combo.ID, order);
            }

            return al;
        }

        /// <summary>
        /// 返回组号
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int GetSubCombNo(Neusoft.HISFC.Models.Order.OutPatient.Order order, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IOrderList)
        {
            int subCombNo = 1;
            Hashtable hsCombNo = new Hashtable();

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order ord in IOrderList)
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
        /// 设置门诊病历信息
        /// </summary>
        /// <param name="regObj"></param>
        private bool SetCaseHistory(Neusoft.HISFC.Models.Registration.Register regObj)
        {
            ClinicCaseHistory clinicCaseHistory = this.OrderManagement.QueryCaseHistoryByClinicCode(regObj.ID);
            if (!object.Equals(clinicCaseHistory, null))
            {
                //主诉
                this.lblCaseMain.Text = clinicCaseHistory.CaseMain;
                //现病史
                this.lblCaseNow.Text = clinicCaseHistory.CaseNow;
                //查体
                this.lblCheckBody.Text = clinicCaseHistory.CheckBody;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(Neusoft.HISFC.Models.Registration.Register register)
        {

            if (register == null)
            {
                return;
            }

            try
            {
                this.label1.Text = this.interMgr.GetHospitalName() + "病程记录";
            }
            catch
            { }

            this.lblName.Text = register.Name;

            if (register.Pact.PayKind.ID == "03")
            {
                try
                {
                    Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pact = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();
                    Neusoft.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(register.Pact.ID);
                }
                catch
                { }
            }

            //年龄按照统一格式
            this.lblAge.Text = this.OrderManagement.GetAge(register.Birthday, false);

            this.lblSex.Text = register.Sex.Name;

            //this.lblSeeDept.Text = register.DoctorInfo.Templet.Dept.Name;
            this.lblMCardNo.Text = register.SSN;
            this.lblCardNo.Text = register.PID.CardNO;

            this.lblPayKind.Text = register.Pact.Name;

            if (register.AddressHome != null && register.AddressHome.Length > 0)
            {
                this.lblTel.Text = register.AddressHome + "/" + register.PhoneHome;
            }
            else
            {
                this.lblTel.Text = register.PhoneHome;
            }
            try
            {
                ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, Neusoft.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                if (al == null)
                {
                    return;
                }
                string strDiagHappenNO = "";
                string strDiag = "";
                if (strDiagHappenNO == null || strDiagHappenNO == "")
                {
                    foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
                    {
                        if (diag != null && diag.Memo != null && diag.Memo != "")
                        {
                            strDiag += diag.Memo + "、";
                        }
                        else
                        {
                            strDiag += diag.DiagInfo.ICD10.Name;
                        }
                    }
                    strDiag = strDiag.TrimEnd(new char[] { '、' });
                    this.lblDiag.Text = strDiag;
                }
                else
                {
                    foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in al)
                    {
                        if (diag.DiagInfo.HappenNo.ToString() == strDiagHappenNO)
                        {
                            this.lblDiag.Text = diag.Memo;
                        }
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();

            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;

            #region 横打

            print.PrintDocument.DefaultPageSettings.Landscape = true;

            ///打印机名称
            //if (!string.IsNullOrEmpty(printer))
            //{
            //    print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            //}

           // print.PrintPage(580, 1, this);
            print.PrintPreview(580, 1, this);
            #endregion
        }
        #endregion


        #region IOutPatientOrderPrint 成员
        /// <summary>
        ///  实现接口打印功能<泛型>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IList"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IList)
        {

            //设置病历信息 查
            if (this.SetCaseHistory(regObj))
            {
                if (MessageBox.Show("是否打印门诊记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    //设置人员基本信息
                    this.SetPatientInfo(regObj);

                    Dictionary<string, IList<Neusoft.HISFC.Models.Order.OutPatient.Order>> recipeInfoDic = new Dictionary<string, IList<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

                    ///查询门诊处方记录
                    List<Neusoft.FrameWork.Models.NeuObject> IRecipe = this.OrderManagement.GetRecipeNoByClinicNoAndSeeNo(regObj.ID, regObj.DoctorInfo.SeeNO.ToString(), "0").ToList();

                    ///取出处方信息
                    foreach (Neusoft.FrameWork.Models.NeuObject NeuObject in IRecipe)
                    {
                        var undrug1 = from cust in IList
                                      where cust.ReciptNO.Equals(NeuObject.ID)
                                      select cust;

                        recipeInfoDic.Add(NeuObject.ID, undrug1.ToList());
                    }

                    ///循环打印信息
                    foreach (KeyValuePair<string, IList<Neusoft.HISFC.Models.Order.OutPatient.Order>> infoDic in recipeInfoDic)
                    {

                        this.MakaLabel(infoDic.Value);
                        this.Print();

                    }
                }
            }
            return 1;
        }


        /// <summary>
        /// 实现接口打印功能
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {


            return 1;
        }
        #endregion
    }
}
