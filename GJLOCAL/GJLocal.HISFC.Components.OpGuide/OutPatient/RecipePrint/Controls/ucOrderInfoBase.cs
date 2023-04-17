using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GJLocal.HISFC.Components.OpGuide.RecipePrint.Controls
{
    public partial class ucOrderInfoBase : UserControl, RecipePrint.Interface.IOrderInfo
    {
        public ucOrderInfoBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 存储处方组合号
        /// </summary>
        private Dictionary<string, ArrayList> dicCombOrder = null;

        /// <summary>
        /// 打印处方项目名称是否是通用名：1 通用名；0 商品名
        /// </summary>
        private int printItemNameType = -1;

        private string toSimple(decimal dec)
        {
            return Neusoft.FrameWork.Public.String.ToSimpleString(dec);
        }

        /// <summary>
        /// 显示药品名称+规格+总量
        /// </summary>
        /// <param name="outOrder"></param>
        /// <returns></returns>
        private string GetName(Neusoft.HISFC.Models.Order.OutPatient.Order outOrder)
        {
            Neusoft.HISFC.Models.Pharmacy.Item phaItem = null;

            string showName = "";

            if (outOrder.Item.ID == "999")
            {
                showName += outOrder.Item.Name;

                if (outOrder.Item.ID == "999" && !showName.Contains("自备"))
                {
                    showName += "[自备]";
                }

                showName += " " + outOrder.Item.Qty + outOrder.Unit;
            }
            else
            {
                phaItem = Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(outOrder.Item.ID);

                #region 显示药品名称

                if (printItemNameType == -1)
                {
                    Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                    printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
                }

                if (printItemNameType == 0)
                {
                    showName += phaItem.Name;
                }
                else
                {
                    if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                    {
                        showName += phaItem.Name;
                    }
                    else
                    {
                        showName += phaItem.NameCollection.RegularName;
                    }
                }
                #endregion

                //药品名称后面显示信息（基本剂量、规格、总量等）
                if (outOrder.Unit == phaItem.PackUnit)
                {
                    showName += toSimple(phaItem.BaseDose) + phaItem.DoseUnit + "*" + phaItem.PackQty + phaItem.MinUnit + "/" + phaItem.PackUnit + "×" + toSimple(outOrder.Qty) + outOrder.Unit;
                }
                else
                {
                    showName += toSimple(phaItem.BaseDose) + phaItem.DoseUnit + "/" + phaItem.MinUnit + "×" + toSimple(outOrder.Qty) + outOrder.Unit;
                }
            }

            return showName;
        }

        public int SetOrderInfo(System.Collections.ArrayList alOrder)
        {
            if (alOrder == null)
            {
                return 1;
            }

            //按照sortID排序
            alOrder.Sort(new Neusoft.SOC.Local.Order.ZhuHai.ZDWY.Common.Comparer.OrderCompare());

            dicCombOrder = new Dictionary<string, ArrayList>();
            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order outOrder in alOrder)
            {
                if (dicCombOrder.ContainsKey(outOrder.Combo.ID))
                {
                    dicCombOrder[outOrder.Combo.ID].Add(outOrder);
                }
                else
                {
                    ArrayList al = new ArrayList();
                    al.Add(outOrder);
                    dicCombOrder.Add(outOrder.Combo.ID, al);
                }
            }

            //填充数据
            string[] parm = { "  ", "┌", "│", "└" };


            StringBuilder buffer = new System.Text.StringBuilder();

            int sortID = 1;
            foreach (string key in dicCombOrder.Keys)
            {
                ArrayList alCombOrder = dicCombOrder[key];

                //组号
                buffer.Append(sortID.ToString());

                for (int i = 0; i < alCombOrder.Count; i++)
                {
                    Neusoft.HISFC.Models.Order.OutPatient.Order ord = alCombOrder[i] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                    string combLabel = "";

                    //组合标记
                    if (alCombOrder.Count == 1)
                    {
                        buffer.Append(parm[0]);
                        buffer.Append(GetName(ord));

                        buffer.Append("\n");

                        buffer.Append("  用法:");

                        //用量    外用药不显示每次量
                        if (Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ord.Item.ID).SpecialFlag2 != "1")
                        {
                            buffer.Append(" ");
                            buffer.Append(toSimple(ord.DoseOnce) + ord.DoseUnit);
                            buffer.Append(" ");
                        }
                        //用法
                        buffer.Append(ord.Usage.Name);
                        buffer.Append(" ");

                        buffer.Append(Neusoft.SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(ord.Frequency.ID));
                        buffer.Append(" ");

                        //备注
                        buffer.Append(ord.Memo);
                    }
                    else
                    {
                        if (i == 0)
                        {
                            buffer.Append(parm[1]);
                            buffer.Append(GetName(ord));
                        }
                        else if (i == alCombOrder.Count - 1)
                        {
                            buffer.Append(parm[3]);
                            buffer.Append(GetName(ord));

                            buffer.Append("\n");
                            for (int m = 0; m < alCombOrder.Count; m++)
                            {
                                Neusoft.HISFC.Models.Order.OutPatient.Order orderM = alCombOrder[m] as Neusoft.HISFC.Models.Order.OutPatient.Order;

                                if (m == 0)
                                {
                                    buffer.Append("    用法:");

                                    if (Neusoft.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ord.Item.ID).SpecialFlag2 != "1")
                                    {
                                        buffer.Append(" ");

                                        bool flag = false;
                                        bool changeDoseOnce = false;
                                        for (int j = 0; j < alCombOrder.Count; j++)
                                        {
                                            Neusoft.HISFC.Models.Order.OutPatient.Order orderJ = alCombOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                                            Neusoft.HISFC.Models.Pharmacy.Item item = orderJ.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                                            if (orderJ.DoseOnce != item.BaseDose)
                                            {
                                                changeDoseOnce = true;
                                            }
                                        }
                                        for (int j = 0; j < alCombOrder.Count; j++)
                                        {
                                            Neusoft.HISFC.Models.Order.OutPatient.Order orderJ = alCombOrder[j] as Neusoft.HISFC.Models.Order.OutPatient.Order;
                                            Neusoft.HISFC.Models.Pharmacy.Item item = orderJ.Item as Neusoft.HISFC.Models.Pharmacy.Item;
                                            if (changeDoseOnce)
                                            {
                                                flag = true;

                                                if (j > 0)
                                                {
                                                    //如果每次量不等于基础量，则在用法后显示每次量
                                                    buffer.Append("          ");
                                                }
                                                buffer.Append(item.Name);
                                                buffer.Append(orderJ.DoseOnce + orderJ.DoseUnit);
                                                buffer.Append("\n");
                                            }

                                            if (j == alCombOrder.Count - 1 && flag == true)
                                            {
                                                buffer.Remove(buffer.Length - 1, 1);
                                                buffer.Append(" ");
                                            }
                                        }
                                    }

                                    buffer.Append(orderM.Usage.Name);
                                    buffer.Append(" ");

                                    buffer.Append(Neusoft.SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(ord.Frequency.ID));

                                    buffer.Append(" ");
                                    buffer.Append(orderM.Memo);
                                    buffer.Append("\n");
                                }
                            }
                        }
                        else
                        {
                            buffer.Append(parm[2]);
                            buffer.Append(GetName(ord));
                        }
                    }

                    //if (Neusoft.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(ord.Usage.ID))
                    //{
                    //    buffer.Append("(余液弃去)");
                    //}
                    buffer.Append("\n");
                }

                sortID += 1;
            }
            buffer.Append("\r\n-----------------------以下空白-----------------------");

            this.lblOrder.Text = buffer.ToString();

            return 1;
        }
    }
}
