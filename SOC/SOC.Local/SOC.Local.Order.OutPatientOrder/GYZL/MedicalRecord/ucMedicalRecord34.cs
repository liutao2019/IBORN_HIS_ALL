using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Order.OutPatientOrder.GYZL.Common;
using FS.FrameWork.Models;
using FS.HISFC.Models.Order.OutPatient;
using System;
using System.Drawing;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL.MedicalRecord
{
    /// <summary>
    /// 门诊诊疗记录卡
    /// </summary>
    public partial class ucMedicalRecord34 : UserControl,
        FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint
    {

        public ucMedicalRecord34()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 每页显示的条数
        /// </summary>
        private int iSet = 10;

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
        /// 存储项目类型
        /// </summary>
        private ArrayList listSysClass = new ArrayList();

        /// <summary>
        /// 用法帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 频次帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper freHelper = null;

        /// <summary>
        /// 频次信息
        /// </summary>
        FS.HISFC.Models.Order.Frequency frequencyObj = null;

        /// <summary>
        /// 用法列表，用来判断是否是针剂用法
        /// </summary>
        private ArrayList alUsage = null;

        private string judPrint = string.Empty;

        /// <summary>
        /// 门诊病历实体
        /// </summary>
        FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory caseHistory = null;
        #endregion

        #region  业务类
        /// <summary>
        /// 频次原子业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();

        /// <summary>
        /// 医嘱原子业务层
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.Order OrderManagement = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 诊断原子业务层
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// 管理
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 药品业务逻辑层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        #endregion

        #region 函数
        /// <summary>
        /// 生成显示信息
        /// </summary>
        /// <param name="alOrder"></param>
        private void MakaLabel(IList<FS.HISFC.Models.Order.OutPatient.Order> OrderList)
        {
            hsCombID = new Hashtable();

            StringBuilder buffer = new System.Text.StringBuilder();
            //填充数据
            string[] parm = { "  ", "┌", "│", "└" };

            if (showEmplLength == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                showEmplLength = controlMgr.GetControlParam<int>("HN0002", true, 6);
            }

            this.lblPhaDoc.Text = OrderList.First().ReciptDoctor.ID.Substring(6 - showEmplLength, showEmplLength) + "/" + OrderList.First().ReciptDoctor.Name;


            #region /*画组合符号*/

            var comparer = new CommonComparer<FS.HISFC.Models.Order.OutPatient.Order>((x, y) => x.SortID - y.SortID);

            //按照sortID排序
            OrderList.ToList().Sort(comparer);

            foreach (FS.HISFC.Models.Order.OutPatient.Order outPatientOrder in OrderList)
            {
                string printUsage = "";
                IList<FS.HISFC.Models.Order.OutPatient.Order> al = this.GetOrderByCombId(outPatientOrder, OrderList);
                string drugType = string.Empty;

                if (al.Count == 0)
                {
                    continue;
                }
                else
                {
                    drugType = al.First().Item.SysClass.ID.ToString();
                    #region 形成字符串

                    if (drugType.Equals("PCC"))
                    {
                        #region 草药处理

                        decimal days = 1m;
                        string freq = "";
                        string usage = "";
                        int k = 0;
                        foreach (FS.HISFC.Models.Order.OutPatient.Order order in al)
                        {
                            days = order.HerbalQty;
                            freq = order.Frequency.Name;

                            if ((k + 1) % 2 == 1)
                            {
                                buffer.Append("[");
                                buffer.Append(this.GetSubCombNo(order, OrderList).ToString());
                                buffer.Append("]");
                            }

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
                                if (((FS.HISFC.Models.Base.Const)usageHelper.GetObjectFromID(order.Usage.ID)).UserCode != "HJZ")
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
                        buffer.Remove(buffer.Length - 1, 1);
                        buffer.Append("");
                        buffer.Append(days.ToString());
                        buffer.Append("剂");
                        buffer.Append("\n");
                        buffer.Append("用法: ");
                        //buffer.Append("每日一剂");
                        buffer.Append("每日" + GetFrequencyCount(outPatientOrder.Frequency.ID) + "剂");
                        buffer.Append(" ");
                        buffer.Append("(" + usage + ")\n\n");

                        ////判断是否是煎药方式
                        //if ("自煎、代煎、复渣、代复渣".Contains(outPatientOrder.Usage.Memo))
                        //{
                        //    buffer.Append("(" + outPatientOrder.Memo + ")");
                        //}

                        #endregion
                    }
                    else if (drugType.Equals("P") || drugType.Equals("PCZ"))
                    {
                        #region 西药、成药

                        //FS.HISFC.Models.Order.OutPatient.Order ord = null;
                        //FS.FrameWork.Models.NeuObject quaulity = null;
                        FS.HISFC.Models.Pharmacy.Item phaItem = null;
                        FS.FrameWork.Models.NeuObject objFre = null;

                        string combLabel = "";
                        int k = 0;
                        foreach (FS.HISFC.Models.Order.OutPatient.Order order in al)
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
                                        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
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

                            //ord = new FS.HISFC.Models.Order.OutPatient.Order();
                            string specs = "";

                            #region 药品名称后面显示信息（基本剂量、、规格、总量等）

                            buffer.Append(order.Item.Specs);

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
                                        buffer.Append(order.Frequency.Name);
                                    }
                                }
                                #endregion

                                //buffer.Append("×");

                                //#region 天数

                                //if (order.HerbalQty <= 0)
                                //{
                                //    order.HerbalQty = 1;
                                //}

                                //buffer.Append(order.HerbalQty.ToString());

                                //#endregion

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

                            k++;
                            #endregion
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region 非药品处理
                        foreach (FS.HISFC.Models.Order.OutPatient.Order order in al)
                        {
                            #region 组号

                            buffer.Append(" [");
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
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
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
        /// 打印门诊记录(重构代码)
        /// </summary>
        /// <param name="OrderList"></param>
        private void PrintAllPage(IList<FS.HISFC.Models.Order.OutPatient.Order> OrderList, bool isPreview)
        {
            #region 去除附材医嘱 order.IsSubtbl,并记录order.Item.SysClass种类
            //ArrayList typeList = new ArrayList();//记录种类
            //for (int i = OrderList.Count -1; i >= 0; i--)
            //{
            //    FS.HISFC.Models.Order.OutPatient.Order order = OrderList[i];
            //    if (order.IsSubtbl)
            //    {
            //        OrderList.RemoveAt(i);
            //    }
            //    else
            //    {
            //        string curClass = order.Item.SysClass.Name;
            //        if (!typeList.Contains(curClass))
            //        {
            //            typeList.Add(curClass);
            //        }
            //    }
            //}
            ////默认把草药、西药、中成药放前面
            //int index = typeList.Count - 1;
            //if (typeList.Contains("中草药"))
            //{
            //    typeList.Remove("中草药");
            //    typeList.Insert(index--, "中草药");
            //}
            //if (typeList.Contains("西药"))
            //{
            //    typeList.Remove("西药");
            //    typeList.Insert(index--, "西药");
            //}
            //if (typeList.Contains("中成药"))
            //{
            //    typeList.Remove("中成药");
            //    typeList.Insert(index--, "中成药");
            //}
            #endregion

            #region 按类别分类

            //IList<FS.HISFC.Models.Order.OutPatient.Order> newOrderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
            //for (int i = typeList.Count - 1; i >= 0; i--)
            //{
            //    for (int j = 0; j < OrderList.Count; j++)
            //    {
            //        if (typeList[i].Equals(OrderList[j].Item.SysClass.Name))
            //        {
            //            newOrderList.Add(OrderList[j]);
            //        }
            //    }
            //}

            #endregion

            #region 分页打印

            //IList<FS.HISFC.Models.Order.OutPatient.Order> alPrint = new List<FS.HISFC.Models.Order.OutPatient.Order>();
            //int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(newOrderList.Count) / iSet));
            //ArrayList tempList;
            //for (int i = 1; i <= icount; i++)
            //{
            //    alPrint.Clear();
            //    //分页打印
            //    if (i == icount)
            //    {
            //        //最后一页
            //        int num = newOrderList.Count % iSet;
            //        if (num == 0)
            //        {
            //            num = iSet;
            //        }
            //        tempList = ArrayList.Adapter(newOrderList.ToArray()).GetRange(iSet * (i - 1), num);
            //        for (int j = 0; j < tempList.Count; j++)
            //        {
            //            alPrint.Add(tempList[j] as FS.HISFC.Models.Order.OutPatient.Order);
            //        }
            //        this.MakeLabel(alPrint);
            //        if (!isPreview)
            //        {
            //            this.Print();
            //        }
            //    }
            //    else
            //    {
            //        tempList = ArrayList.Adapter(newOrderList.ToArray()).GetRange(iSet * (i - 1), iSet);
            //        for (int j = 0; j < tempList.Count; j++)
            //        {
            //            alPrint.Add(tempList[j] as FS.HISFC.Models.Order.OutPatient.Order);
            //        }
            //        this.MakeLabel(alPrint);
            //        if (!isPreview)
            //        {
            //            this.Print();
            //        }
            //    }
            //}

            #endregion

            #region 新建
            this.MakeLabel(OrderList);
            if (!isPreview)
            {
                this.Print();
            }
            #endregion
        }

        /// <summary>
        /// 生成显示信息(重构代码)
        /// </summary>
        /// <param name="OrderList"></param>
        private void MakeLabel(IList<FS.HISFC.Models.Order.OutPatient.Order> OrderList)
        {
            hsCombID = new Hashtable();
            listSysClass = new ArrayList();
            string drugType;
            StringBuilder result = new System.Text.StringBuilder();
            string[] parm = { "  ", "┌", "│", "└" };//组合符号
            foreach (FS.HISFC.Models.Order.OutPatient.Order order in OrderList)
            {
                IList<FS.HISFC.Models.Order.OutPatient.Order> combOrder = this.GetOrderByCombId(order, OrderList);
                if (combOrder.Count == 0)
                {
                    continue;
                }

                drugType = combOrder.First().Item.SysClass.ID.ToString();
                if (!listSysClass.Contains(drugType))
                {
                    //该类别第一个项目前显示类别名称
                    result.Append(combOrder.First().Item.SysClass.Name + ":\n");
                    listSysClass.Add(drugType);
                }


                if (drugType.Equals("PCC"))
                {
                    #region 中草药处理
                    int combCount = 0;//当前组内序号
                    foreach (FS.HISFC.Models.Order.OutPatient.Order tempOrder in combOrder)
                    {
                        #region 显示名称、用量

                        string name = tempOrder.Item.Name;
                        string dose = tempOrder.Qty + "(" + tempOrder.DoseOnce + tempOrder.DoseUnit + ")";
                        result.Append(string.Format("{0,-15}", name + dose));

                        #endregion

                        #region 每行显示两个中草药

                        if ((combCount + 1) % 2 == 0 || combCount == combOrder.Count - 1)
                        {
                            result.Append("\n");
                        }

                        #endregion

                        #region 显示用法

                        if (combCount == combOrder.Count - 1)
                        {
                            result.Append("      用法:" + tempOrder.HerbalQty + "剂");
                            result.Append(tempOrder.Frequency.Name);
                            result.Append("\n");
                        }
                        #endregion

                        combCount++;
                    }
                    #endregion
                }
                else if (drugType.Equals("P") || drugType.Equals("PCZ"))
                {
                    #region 西药、中成药处理
                    FS.HISFC.Models.Pharmacy.Item phaItem;//药品信息
                    FS.FrameWork.Models.NeuObject objFre;//频次
                    int combCount = 0;//当前组内序号
                    foreach (FS.HISFC.Models.Order.OutPatient.Order tempOrder in combOrder)
                    {
                        phaItem = phaIntegrate.GetItem(tempOrder.Item.ID);//获取药品信息

                        #region 显示组合标记

                        if (combOrder.Count == 1)
                        {
                            //组内只有一个项目
                            result.Append(parm[0]);//parm[0]:"  "
                        }
                        else
                        {
                            if (combCount == 0)
                            {
                                //组内第1个项目
                                result.Append(parm[1]);//parm[1]:"┌"
                            }
                            else if (combCount == combOrder.Count - 1)
                            {
                                //组内最后1个项目
                                result.Append(parm[3]);//parm[3]:"└"
                            }
                            else
                            {
                                //组内中间的项目
                                result.Append(parm[2]);//parm[2]:"│"
                            }
                        }

                        #endregion

                        #region 显示药品名称
                        if (phaItem != null)
                        {
                            //显示通用名
                            if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                            {
                                result.Append(phaItem.Name);
                            }
                            else
                            {
                                result.Append(phaItem.NameCollection.RegularName);
                            }
                        }
                        else
                        {
                            result.Append(tempOrder.Item.Name);
                        }
                        #endregion

                        #region 显示基础量x总量

                        result.Append(phaItem.BaseDose + phaItem.DoseUnit);
                        result.Append("x");
                        result.Append(tempOrder.Qty + tempOrder.Unit);

                        #endregion

                        result.Append("\n");

                        #region 显示每次量、用法

                        result.Append("      用法:" + tempOrder.DoseOnce + tempOrder.DoseUnit + " ");
                        result.Append(tempOrder.Usage.Name);

                        #endregion

                        result.Append(" ");

                        #region 显示频次

                        if ((tempOrder.Frequency.Name == null || tempOrder.Frequency.Name.Length <= 0)
                            && this.freHelper != null && this.freHelper.ArrayObject.Count > 0)
                        {
                            objFre = this.freHelper.GetObjectFromID(tempOrder.Frequency.ID);
                            if (objFre != null)
                            {
                                result.Append(objFre.Name);
                            }
                        }
                        else
                        {
                            result.Append(tempOrder.Frequency.Name);
                        }

                        #endregion

                        #region 显示备注

                        result.Append(tempOrder.Memo);

                        #endregion

                        result.Append("\n");

                        combCount++;
                    }
                    #endregion
                }
                else
                {
                    #region 其他处理
                    int combCount = 0;//当前组内序号
                    foreach (FS.HISFC.Models.Order.OutPatient.Order tempOrder in combOrder)
                    {
                        #region 显示组合标记

                        if (combOrder.Count == 1)
                        {
                            //组内只有一个项目
                            result.Append(parm[0]);//parm[0]:"  "
                        }
                        else
                        {
                            if (combCount == 0)
                            {
                                //组内第1个项目
                                result.Append(parm[1]);//parm[1]:"┌"
                            }
                            else if (combCount == combOrder.Count - 1)
                            {
                                //组内最后1个项目
                                result.Append(parm[3]);//parm[3]:"└"
                            }
                            else
                            {
                                //组内中间的项目
                                result.Append(parm[2]);//parm[2]:"│"
                            }
                            combCount++;
                        }

                        #endregion

                        #region 显示项目名称

                        result.Append(tempOrder.Item.Name);

                        #endregion

                        result.Append(" ");

                        #region 显示次数

                        result.Append(tempOrder.Qty + tempOrder.Unit);

                        #endregion

                        #region 显示备注

                        result.Append(tempOrder.Memo);

                        #endregion

                        result.Append("\n");
                    }
                    #endregion
                }
            }
            this.lblOrder.Text = result.ToString();
        }

        /// <summary>
        /// 得到组合处方的组合显示
        /// </summary>
        /// <param name="order"></param>
        /// <param name="usageID">一组统一显示的用法</param>
        /// <returns></returns>
        private IList<FS.HISFC.Models.Order.OutPatient.Order> GetOrderByCombId(FS.HISFC.Models.Order.OutPatient.Order order, IList<FS.HISFC.Models.Order.OutPatient.Order> IorderList)
        {
            IList<FS.HISFC.Models.Order.OutPatient.Order> al = new List<FS.HISFC.Models.Order.OutPatient.Order>();

            if (!hsCombID.Contains(order.Combo.ID))
            {
                foreach (FS.HISFC.Models.Order.OutPatient.Order ord in IorderList)
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
        private int GetSubCombNo(FS.HISFC.Models.Order.OutPatient.Order order, IList<FS.HISFC.Models.Order.OutPatient.Order> IOrderList)
        {
            int subCombNo = 1;
            Hashtable hsCombNo = new Hashtable();

            foreach (FS.HISFC.Models.Order.OutPatient.Order ord in IOrderList)
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
        private bool SetCaseHistory(FS.HISFC.Models.Registration.Register regObj)
        {
            ClinicCaseHistory clinicCaseHistory = this.OrderManagement.QueryCaseHistoryByClinicCode(regObj.ID);
            if (!object.Equals(clinicCaseHistory, null))
            {
                //主诉
                this.lblCaseMain.Text = "\r\n" + clinicCaseHistory.CaseMain;
                //现病史
                this.lblCaseNow.Text = "\r\n" + clinicCaseHistory.CaseNow;
                //查体
                this.lblCheckBody.Text = "\r\n" + clinicCaseHistory.CheckBody;
                return true;
            }
            return true;
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.Registration.Register register)
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

            this.lblCaseMain.Text = "";
            lblCaseNow.Text = "";
            lblCheckBody.Text = "";
            lblOrder.Text = "";

            if (register.Pact.PayKind.ID == "03")
            {
                try
                {
                    FS.HISFC.BizLogic.Fee.PactUnitInfo pact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                    FS.HISFC.Models.Base.PactInfo info = pact.GetPactUnitInfoByPactCode(register.Pact.ID);
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

            this.judPrint = register.User03;

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
                ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                if (al == null)
                {
                    return;
                }
                string strDiagHappenNO = "";
                string strDiag = "";
                if (strDiagHappenNO == null || strDiagHappenNO == "")
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
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
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
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

            this.caseHistory = this.OrderManagement.QueryCaseHistoryByClinicCode(register.ID);

            if (this.caseHistory != null)
            {
                if (!string.IsNullOrEmpty(this.caseHistory.CheckBody))
                {
                    this.lblCheckBody.Text = "\r\n" + this.caseHistory.CheckBody;
                }
                if (!string.IsNullOrEmpty(this.caseHistory.CaseMain))
                {
                    this.lblCaseMain.Text = "\r\n" + this.caseHistory.CaseMain;
                }
                if (!string.IsNullOrEmpty(this.caseHistory.CaseNow))
                {
                    this.lblCaseNow.Text = "\r\n" + this.caseHistory.CaseNow;
                }
            }

            #region 设置没有内容的不显示

            pnCaseMain.Visible = !string.IsNullOrEmpty(lblCaseMain.Text);
            this.pnCaseNow.Visible = !string.IsNullOrEmpty(lblCaseNow.Text);
            pnCheckBody.Visible = !string.IsNullOrEmpty(lblCheckBody.Text);

            #endregion
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 800));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.IsDataAutoExtend = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager || judPrint == "BD")
            {
                print.PrintPage(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }
        #endregion

        #region IOutPatientOrderPrint 成员


        Graphics g = null;

        /// <summary>
        ///  实现接口打印功能<泛型>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IList"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            if (orderList.Count == 0)
            {
                return 1;
            }

            //设置人员基本信息
            this.SetPatientInfo(regObj);

            #region 获取字体高度

            //Graphics g = this.CreateGraphics();
            //g.PageUnit = GraphicsUnit.Pixel;
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //StringFormat sf = new StringFormat();
            //sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

            #endregion

            #region 设置病历内容显示

            if (g == null)
            {
                g = lblCaseMain.CreateGraphics();
            }

            //获取每行字数宽度
            float width = g.MeasureString("哈", lblCaseMain.Font).Width;

            int line = (Int32)Math.Ceiling(lblCaseMain.Text.Length * width / lblCaseMain.Width);
            pnCaseMain.Height = (Int32)Math.Ceiling(line * 17M);


            //Graphics gCaseNow = lblCaseNow.CreateGraphics();
            //width = gCaseNow.MeasureString("哈", lblCaseNow.Font).Width;

            line = (Int32)Math.Ceiling(lblCaseNow.Text.Length * width / lblCaseNow.Width);
            this.pnCaseNow.Height = (Int32)Math.Ceiling(line * 17M);

            //Graphics gCheckBody = lblCheckBody.CreateGraphics();
            //width = gCheckBody.MeasureString("哈", lblCheckBody.Font).Width;
            line = (Int32)Math.Ceiling(lblCheckBody.Text.Length * width / lblCheckBody.Width);
            this.pnCheckBody.Height = (Int32)Math.Ceiling(line * 17M);

            #endregion

            #region 获取每页可以打印的行数

            //SizeF sizeF = g.MeasureString("哈", this.lblOrder.Font, 500, sf);

            //int lineCount = (Int32)Math.Floor(lblOrder.Height / sizeF.Height);
            int lineCount = (Int32)Math.Floor(lblOrder.Height / 17M);
            lineCount = lineCount - 1;

            //每行的字体个数
            //int columnCount = (Int32)Math.Ceiling(lblDiag.Width / sizeF.Width);

            #endregion

            for (int j = 0; j < 10; j++)
            {
                List<FS.HISFC.Models.Order.OutPatient.Order> alPrint = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                for (int i = 0; i < lineCount; i++)
                {
                    if ((i + j * lineCount) >= orderList.Count)
                    {
                        break;
                    }
                    alPrint.Add(orderList[i + j * lineCount]);
                }
                if (alPrint.Count > 0)
                {
                    PrintAllPage(alPrint, isPreview);
                }
            }
            return 1;
        }

        private void Init()
        {
            if (alUsage == null || alUsage.Count == 0)
            {
                FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                alUsage = interMgr.GetConstantList("USAGE");
                if (alUsage == null)
                {
                    MessageBox.Show("获取用法列表出错：" + interMgr.Err);
                    return;
                }

                usageHelper.ArrayObject = alUsage;
            }
        }

        /// <summary>
        /// 实现接口打印功能
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <returns></returns>
        public int PrintOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            return 1;
        }

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
        }

        #endregion

        private void ucMedicalRecord_Load(object sender, System.EventArgs e)
        {
            Init();
        }

        #region IOutPatientOrderPrint Members


        public void SetPage(string pageStr)
        {
        }

        #endregion
    }
}