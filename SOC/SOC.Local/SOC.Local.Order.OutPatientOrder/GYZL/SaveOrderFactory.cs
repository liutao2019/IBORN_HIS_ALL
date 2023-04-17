using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Base;
using System.Collections;
using System.Windows.Forms;
using FS.HISFC.Models.Order.OutPatient;
using FS.FrameWork.WinForms.Classes;
using FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder;
using System.Drawing;
//using FS.SOC.Local.Order.OutPatientOrder.GYZL.Common.EasiLab.Models.LabManagementing;
//using FS.SOC.Local.Order.OutPatientOrder.GYZL.Common.EasiLab.DataAccess.Models.HisInterface;

namespace FS.SOC.Local.Order.OutPatientOrder.GYZL
{
    /// <summary>
    /// 医嘱保存后工厂类
    /// </summary>
    public class SaveOrderFactory : FS.HISFC.BizProcess.Interface.Order.IOutPatientPrint
    {

        #region 变量
        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = string.Empty;

        List<FS.HISFC.Models.Order.OutPatient.Order> orderList = null;

        private Hashtable hsCombID = new Hashtable();

        private Hashtable hsTmpCombID = new Hashtable();
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
        #endregion

        #region 业务层
        /// <summary>
        /// 医嘱业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Order.OutPatient.Order OrderManagement = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        /// <summary>
        /// 费用业务类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 药品业务逻辑层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        /// <summary>
        /// 频次原子业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
        /// <summary>
        /// 诊断原子业务层
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        /// <summary>
        /// 常数业务层
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();
        #endregion

        #region 定义接口

        /// <summary>
        /// 处方打印接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.IRecipePrint iRecipePrint = null;

        /// <summary>
        /// 麻毒精处方打印接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.IRecipePrint iLimitRecipePrint = null;

        /// <summary>
        /// 检查打印接口
        /// </summary>
        FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint iPacsPrint = null;

        /// <summary>
        /// 检验打印接口
        /// </summary>
        FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint iLisPrint = null;

        /// <summary>
        /// 实现门诊院注单打印接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCInjectBillPrint = null;

        /// <summary>
        /// 实现门诊治疗单打印接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCTreatmentBillPrint = null;

        /// <summary>
        /// 实现门诊指引单打印接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCOutPatientGuidePrint = null;

        /// <summary>
        /// 实现门诊诊疗记录打印接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCMedicalRecordPrint = null;

        // <summary>
        /// 实现门诊诊疗单打印接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCTreatBillPrint = null;


        private FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint siDiagnosePrint = null;
        #endregion

        #region 本地化接口函数

        #region 设置数组
        /// <summary>
        /// 设置数组
        /// </summary>
        /// <param name="orderDictionary"></param>
        /// <param name="order"></param>
        private void SetOrderList(Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> orderDictionary, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            if (orderDictionary.ContainsKey(order.ReciptNO))
            {
                orderList = orderDictionary[order.ReciptNO];
                if (orderList == null)
                {
                    orderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                    orderList.Add(order);
                    orderDictionary[order.ReciptNO] = orderList;
                }
                else
                {
                    orderList.Add(order);
                    orderDictionary[order.ReciptNO] = orderList;
                }
            }
            else
            {
                orderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                orderList.Add(order);
                orderDictionary.Add(order.ReciptNO, orderList);
            }
        }

        #endregion

        #region 普通处方
        //interface = 'FS.HISFC.BizProcess.Interface.IRecipePrint' and printerindex = 0

        /// <summary>
        /// 普通处方
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="drugDictionary"></param>
        /// <param name="alOrder"></param>
        /// <param name="ControlList"></param>
        /// <param name="isPreview"></param>
        private void CommonDrug(FS.HISFC.Models.Registration.Register regObj, Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> drugDictionary, System.Collections.ArrayList alOrder, List<Control> ControlList, bool isPreview)
        {
            if (drugDictionary.Keys.Count > 0)
            {
                if (iRecipePrint == null)
                {
                    iRecipePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.HISFC.BizProcess.Interface.IRecipePrint), 0) as FS.HISFC.BizProcess.Interface.IRecipePrint;
                }
                if (iRecipePrint == null)
                {
                    this.errInfo = "处方打印接口未实现！";
                }
                else
                {
                    #region 暂时屏蔽原有
                    //if (regObj.User03 == "CD")        //初打
                    //{
                    //    foreach (string recipeNO in drugDictionary.Keys)
                    //    {
                    //        iRecipePrint.SetPatientInfo(regObj);
                    //        iRecipePrint.RecipeNO = recipeNO;
                    //        iRecipePrint.PrintRecipe();
                    //    }
                    //}
                    //else
                    //{           //补打
                    //    foreach (string recipeNO in drugDictionary.Keys)
                    //    {
                    //        iRecipePrint.SetPatientInfo(regObj);
                    //        iRecipePrint.RecipeNO = recipeNO;
                    //        iRecipePrint.PrintRecipeView(alOrder);
                    //    }
                    //}
                    #endregion
                    #region 新增
                    int page = drugDictionary.Keys.Count;//总页数
                    int i = 1;//当前页数
                    int j = 1;//当前草药页数
                    int k = 1;//当前西药页数
                    int totalPCC = 0;//草药总数
                    int totalPCZ = 0;//西药总数

                    //预览打印 补打的时候 预览打印
                    ZDLY.RecipePrint.frmRecipePrint frm = null;

                    foreach (string recipeNO in drugDictionary.Keys)
                    {
                        string type = drugDictionary[recipeNO][0].Item.SysClass.ID.ToString() == "PCC" ? "草药" : "西药";
                        if (type == "草药")
                        {
                            totalPCC++;
                        }
                        else
                        {
                            totalPCZ++;
                        }
                    }

                    frm = new FS.SOC.Local.Order.OutPatientOrder.ZDLY.RecipePrint.frmRecipePrint();

                    frm.ClearUC();

                    int index = 0;
                    foreach (string recipeNO in drugDictionary.Keys)
                    {
                        string type = drugDictionary[recipeNO][0].Item.SysClass.ID.ToString() == "PCC" ? "草药" : "西药";
                        string labelPage = " 第" + i + "页/共" + page + "页";

                        string labelDrug = " ";

                        if (type == "草药")
                        {
                            labelDrug = type + j.ToString() + "/" + totalPCC.ToString();
                            j++;
                        }
                        else
                        {
                            labelDrug = type + k.ToString() + "/" + totalPCZ.ToString();
                            k++;
                        }

                        regObj.User01 = getTotalMoney(drugDictionary, regObj) + "|" + labelDrug + "|" + labelPage;
                        iRecipePrint.SetPatientInfo(regObj);
                        iRecipePrint.RecipeNO = recipeNO;

                        if (isPreview)
                        {
                            FS.HISFC.BizProcess.Interface.IRecipePrint iRecipe = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.HISFC.BizProcess.Interface.IRecipePrint), 0) as FS.HISFC.BizProcess.Interface.IRecipePrint;

                            iRecipe.SetPatientInfo(regObj);
                            iRecipe.RecipeNO = recipeNO;

                            ControlList.Add(iRecipe as Control);
                        }
                        else
                        {
                            iRecipePrint.PrintRecipe();//初打
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region 麻毒精处方
        //interface = 'FS.HISFC.BizProcess.Interface.IRecipePrint' and printerindex = 4

        /// <summary>
        /// 麻毒精处方
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="limitDrugDictionary"></param>
        /// <param name="alOrder"></param>
        /// <param name="isPreview"></param>
        private void AnestheticChlorpromazine(FS.HISFC.Models.Registration.Register regObj, Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> limitDrugDictionary, System.Collections.ArrayList alOrder, bool isPreview)
        {
            if (limitDrugDictionary.Keys.Count > 0)
            {
                if (iLimitRecipePrint == null)
                {
                    iLimitRecipePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.HISFC.BizProcess.Interface.IRecipePrint), 4) as FS.HISFC.BizProcess.Interface.IRecipePrint;
                }
                if (iLimitRecipePrint == null)
                {
                    this.errInfo = "麻毒精处方打印接口未实现！";
                }
                else
                {
                    if (regObj.User03 == "CD")        //初打
                    {
                        foreach (string recipeNO in limitDrugDictionary.Keys)
                        {
                            iLimitRecipePrint.SetPatientInfo(regObj);
                            iLimitRecipePrint.RecipeNO = recipeNO;
                            iLimitRecipePrint.PrintRecipe();
                        }
                    }
                    else
                    {
                        foreach (string recipeNO in limitDrugDictionary.Keys)
                        {
                            iLimitRecipePrint.SetPatientInfo(regObj);
                            iLimitRecipePrint.RecipeNO = recipeNO;
                            iLimitRecipePrint.PrintRecipeView(alOrder);
                        }
                    }
                }
            }
        }
        #endregion

        #region 实现院注射单接口
        //interface = 'FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint' and printerindex = 1

        /// <summary>
        /// 实现院注射单接口
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        /// <param name="isPreview"></param>
        /// <param name="ControlList"></param>
        private void Injection(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview, List<Control> ControlList)
        {
            if (object.Equals(ISOCInjectBillPrint, null))
            {
                ISOCInjectBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 1) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }

            if (!object.Equals(ISOCInjectBillPrint, null))
            {
                if (ISOCInjectBillPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alOrder, isPreview) >= 0)
                {
                    ControlList.Add(ISOCInjectBillPrint as Control);
                }
            }
        }
        #endregion

        #region 实现院治疗单接口
        //interface = 'FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint' and printerindex = 9

        /// <summary>
        /// 实现院治疗单接口
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        private void TreatMentItem(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview, List<Control> ControlList)
        {
            if (object.Equals(ISOCTreatmentBillPrint, null))
            {
                ISOCTreatmentBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 9) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }

            if (!object.Equals(ISOCTreatmentBillPrint, null))
            {
                ISOCTreatmentBillPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alOrder, isPreview);
                ControlList.Add(ISOCTreatmentBillPrint as Control);
            }
        }
        #endregion

        #region 实现门诊指引单
        // interface = 'FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint' and printerindex = 2

        /// <summary>
        /// 实现门诊指引单
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IOrderList"></param>
        /// <param name="isPreview"></param>
        private void Guide(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> IOrderList, bool isPreview)
        {
            if (object.Equals(ISOCOutPatientGuidePrint, null))
            {
                ISOCOutPatientGuidePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 2) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }

            if (!object.Equals(ISOCOutPatientGuidePrint, null))
            {
                ISOCOutPatientGuidePrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, IOrderList, isPreview);
            }
        }
        #endregion

        #region 实现门诊记录单
        //interface = 'FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint' and printerindex = 3

        /// <summary>
        /// 实现门诊记录单
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IOrderList"></param>
        /// <param name="isPreview"></param>
        /// <param name="ControlList"></param>
        private void MedicalRecord(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> IOrderList, bool isPreview, List<Control> ControlList)
        {
            if (object.Equals(ISOCMedicalRecordPrint, null))
            {
                ISOCMedicalRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 3) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }

            if (!object.Equals(ISOCMedicalRecordPrint, null))
            {
                #region 去除附材医嘱 order.IsSubtbl,并记录order.Item.SysClass种类
                ArrayList typeList = new ArrayList();//记录种类
                int iSet = 26;
                Hashtable hsType = new Hashtable();
                for (int i = IOrderList.Count - 1; i >= 0; i--)
                {
                    FS.HISFC.Models.Order.OutPatient.Order order = IOrderList[i];
                    if (order.IsSubtbl)
                    {
                        IOrderList.RemoveAt(i);
                    }
                    else
                    {
                        string curClass = order.Item.SysClass.Name;
                        if (order.Item.SysClass.ID.Equals("PCZ") || order.Item.SysClass.ID.Equals("P"))
                        {
                            continue;
                        }
                        if (!typeList.Contains(curClass))
                        {
                            typeList.Add(curClass);
                        }
                        if (!hsType.Contains(curClass))
                        {
                            hsType.Add(curClass, curClass);
                        }
                    }
                }

                //默认把草药、西药、中成药放前面
                int index = typeList.Count - 1;
                if (typeList.Contains("中草药"))
                {
                    typeList.Remove("中草药");
                    typeList.Insert(index--, "中草药");
                }
                #endregion

                #region 按类别分类

                IList<FS.HISFC.Models.Order.OutPatient.Order> newOrderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                for (int j = 0; j < IOrderList.Count; j++)
                {
                    if (!hsType.Contains(IOrderList[j].Item.SysClass.Name))
                    {
                        newOrderList.Add(IOrderList[j]);
                    }
                }
                for (int i = typeList.Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < IOrderList.Count; j++)
                    {
                        if (typeList[i].Equals(IOrderList[j].Item.SysClass.Name))
                        {
                            newOrderList.Add(IOrderList[j]);
                        }
                    }
                }

                #endregion

                #region 计算行数
                //第一页总共34行
                Graphics g = null;
                System.Windows.Forms.RichTextBox lblOrder = new System.Windows.Forms.RichTextBox();
                lblOrder.BorderStyle = System.Windows.Forms.BorderStyle.None;
                lblOrder.Location = new System.Drawing.Point(73, 3);
                lblOrder.Name = "lblOrder";
                lblOrder.Size = new System.Drawing.Size(478, 551);
                if (g == null)
                {
                    g = lblOrder.CreateGraphics();
                }

                float width = g.MeasureString("哈", new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)))).Width;

                #region 主诉诊断等信息
                ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(regObj.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                string strDiagHappenNO = "";
                string strDiag = "";
                if (al != null)
                {
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
                    }
                    else
                    {
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
                        {
                            if (diag.DiagInfo.HappenNo.ToString() == strDiagHappenNO)
                            {
                                strDiag = diag.Memo;
                            }
                        }
                    }
                }
                ClinicCaseHistory clinicCaseHistory = this.OrderManagement.QueryCaseHistoryByClinicCode(regObj.ID);
                if (!object.Equals(clinicCaseHistory, null))
                {
                    //主诉
                    if (!string.IsNullOrEmpty(clinicCaseHistory.CaseMain))
                    {
                        lblOrder.Text += clinicCaseHistory.CaseMain + "\r\n";
                    }
                    else
                    {
                        lblOrder.Text += "\r\n";
                    }
                    //现病史
                    if (!string.IsNullOrEmpty(clinicCaseHistory.CaseNow))
                    {
                        lblOrder.Text += clinicCaseHistory.CaseNow + "\r\n";
                    }
                    else
                    {
                        lblOrder.Text += "\r\n";
                    }
                    //查体
                    if (!string.IsNullOrEmpty(clinicCaseHistory.CheckBody))
                    {
                        lblOrder.Text += clinicCaseHistory.CheckBody + "\r\n";
                    }
                    else
                    {
                        lblOrder.Text += "\r\n";
                    }
                }
                else
                {
                    lblOrder.Text += "\r\n";
                    lblOrder.Text += "\r\n";
                    lblOrder.Text += "\r\n";
                }
                //诊断 末尾加行数判断，避免打印计算错误
                if (!string.IsNullOrEmpty(strDiag))
                {
                    lblOrder.Text += strDiag + "〓" + "\r\n";
                }
                else
                {
                    lblOrder.Text += "〓" + "\r\n";
                }
                #endregion

                int orderIndex = lblOrder.Text.IndexOf("〓");
                Point crntLastP = lblOrder.GetPositionFromCharIndex(orderIndex);
                int crntLastIndex = lblOrder.GetCharIndexFromPosition(crntLastP);

                int crntLastLine = lblOrder.GetLineFromCharIndex(crntLastIndex);

                int line = crntLastLine;
                //(Int32)Math.Ceiling(lblOrder.Text.Length * width / lblOrder.Width);
                //lblOrder.Height = (Int32)Math.Ceiling(line * 17M);
                #endregion

                #region 分页打印

                #region 原有程序
                IList<FS.HISFC.Models.Order.OutPatient.Order> alPrint = new List<FS.HISFC.Models.Order.OutPatient.Order>();

                ArrayList tempList;
                if (line < 4)
                {
                    line = 4;//至少都是4行了
                }
                int lineCount = 31 - line;//(Int32)Math.Floor(346 / 17M);
                //lineCount = lineCount - 1;
                int pRows = 0;
                int oRows = 0;
                bool onePage = true;
                foreach (FS.HISFC.Models.Order.OutPatient.Order order in newOrderList)
                {
                    if (order.Item.SysClass.ID.ToString() == "P" || order.Item.SysClass.ID.ToString() == "PCZ")
                    {
                        pRows++;
                    }
                    else if (order.Item.SysClass.ID.ToString() != "PCC")
                    {
                        oRows++;
                    }
                }
                if (pRows >= 11)
                {
                    onePage = false;
                }
                else if (((pRows * 2 + 3 + oRows) > lineCount) && oRows > 0)
                {
                    onePage = false;
                }

                if (newOrderList.Count <= lineCount && newOrderList.Count > 0 && onePage)
                {
                    ISOCMedicalRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 3) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                    ISOCMedicalRecordPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, newOrderList, isPreview);
                    ControlList.Add(ISOCMedicalRecordPrint as Control);
                }
                else
                {
                    int n = lineCount;
                    if (newOrderList.Count > 0)
                    {
                        n = this.MakeLabel(newOrderList, regObj, lineCount);

                        if (n > newOrderList.Count)
                        {
                            n = newOrderList.Count;
                        }
                    }
                    if (newOrderList.Count > 0)
                    {
                        tempList = ArrayList.Adapter(newOrderList.ToArray()).GetRange(0, n);
                        for (int j = 0; j < tempList.Count; j++)
                        {
                            alPrint.Add(tempList[j] as FS.HISFC.Models.Order.OutPatient.Order);
                        }
                        if (alPrint.Count > 0)
                        {
                            ISOCMedicalRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 3) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                            ISOCMedicalRecordPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alPrint, isPreview);
                            ControlList.Add(ISOCMedicalRecordPrint as Control);
                        }
                    }
                    else
                    {
                        ISOCMedicalRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 3) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                        ISOCMedicalRecordPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alPrint, isPreview);
                        ControlList.Add(ISOCMedicalRecordPrint as Control);
                        return;
                    }


                    int icount = FS.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(newOrderList.Count - n) / iSet));

                    for (int i = 1; i <= icount; i++)
                    {
                        alPrint.Clear();
                        //分页打印
                        if (i == icount)
                        {
                            //最后一页
                            int num = (newOrderList.Count - n) % iSet;
                            if (num == 0)
                            {
                                num = iSet;
                            }
                            tempList = ArrayList.Adapter(newOrderList.ToArray()).GetRange(iSet * (i - 1) + n, num);
                            for (int j = 0; j < tempList.Count; j++)
                            {
                                alPrint.Add(tempList[j] as FS.HISFC.Models.Order.OutPatient.Order);
                            }
                            if (alPrint.Count > 0)
                            {
                                ISOCMedicalRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 4) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                                ISOCMedicalRecordPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alPrint, isPreview);
                                ControlList.Add(ISOCMedicalRecordPrint as Control);
                            }
                        }
                        else
                        {
                            tempList = ArrayList.Adapter(newOrderList.ToArray()).GetRange(iSet * (i - 1) + n, iSet);
                            for (int j = 0; j < tempList.Count; j++)
                            {
                                alPrint.Add(tempList[j] as FS.HISFC.Models.Order.OutPatient.Order);
                            }
                            if (alPrint.Count > 0)
                            {
                                ISOCMedicalRecordPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 4) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                                ISOCMedicalRecordPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alPrint, isPreview);
                                ControlList.Add(ISOCMedicalRecordPrint as Control);
                            }
                        }
                    }
                }
                #endregion
                #endregion
            }
        }

        /// <summary>
        /// 返回第一页处方总数
        /// </summary>
        /// <param name="OrderList"></param>
        private int MakeLabel(IList<FS.HISFC.Models.Order.OutPatient.Order> OrderList, FS.HISFC.Models.Registration.Register regObj, int totalRows)
        {
            hsTmpCombID = new Hashtable();
            listSysClass = new ArrayList();
            string drugType;
            int rows = 0;
            int orderRows = 0;
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
                    rows++;
                    if (rows == totalRows)
                    {
                        return orderRows;
                    }
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
                        orderRows++;
                        #endregion

                        #region 每行显示两个中草药

                        if ((combCount + 1) % 2 == 0 || combCount == combOrder.Count - 1)
                        {
                            result.Append("\n");
                            rows++;
                            if (rows == totalRows)
                            {
                                return orderRows;
                            }
                        }

                        #endregion

                        #region 显示用法

                        if (combCount == combOrder.Count - 1)
                        {
                            result.Append("      用法:" + tempOrder.HerbalQty + "剂");
                            result.Append(tempOrder.Frequency.Name);
                            result.Append("\n");
                            tempOrder.Item.Memo = "————————————————————\n";
                            rows++;
                            if (rows == totalRows)
                            {
                                return orderRows;
                            }
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
                        phaItem = tempOrder.Item as FS.HISFC.Models.Pharmacy.Item;
                        if (tempOrder.Item.ID != "999")
                        {
                            phaItem = phaIntegrate.GetItem(tempOrder.Item.ID);//获取药品信息
                        }

                        orderRows++;
                        #region 显示组合标记

                        if (combOrder.Count == 1)
                        {
                            //组内只有一个项目
                            result.Append(parm[0]);//parm[0]:"  "
                            tempOrder.Item.Memo = "————————————————————\n";
                            rows++;
                            if (rows == totalRows)
                            {
                                return orderRows;
                            }
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
                                tempOrder.Item.Memo = "————————————————————\n";
                                rows++;
                                if (rows == totalRows)
                                {
                                    return orderRows;
                                }
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
                            result.Append(phaItem.Name);
                        }
                        else
                        {
                            result.Append(tempOrder.Item.Name);
                        }
                        #endregion

                        #region 显示基础量x总量

                        result.Append("    " + phaItem.BaseDose + phaItem.DoseUnit);
                        result.Append("x");
                        result.Append(tempOrder.Qty + tempOrder.Unit);

                        #endregion

                        result.Append("\n");
                        rows++;
                        if (rows == totalRows)
                        {
                            return orderRows;
                        }
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
                        rows++;
                        if (rows == totalRows)
                        {
                            return orderRows;
                        }
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
                            rows++;
                            if (rows == totalRows)
                            {
                                return orderRows;
                            }
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
                                rows++;
                                if (rows == totalRows)
                                {
                                    return orderRows;
                                }
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
                        orderRows++;
                        #endregion

                        result.Append(" ");

                        #region 显示次数

                        result.Append(tempOrder.Qty + tempOrder.Unit);

                        #endregion

                        #region 显示备注|去除“不可选”备注（只是用于提示医生）

                        if (!"不可选".Equals(tempOrder.Memo))
                        {
                            result.Append(tempOrder.Memo);
                        }

                        #endregion

                        result.Append("\n");
                        rows++;
                        if (rows == totalRows)
                        {
                            return orderRows;
                        }
                    }
                    #endregion
                }
            }
            return orderRows;
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

            if (!hsTmpCombID.Contains(order.Combo.ID))
            {
                foreach (FS.HISFC.Models.Order.OutPatient.Order ord in IorderList)
                {
                    if (order.Combo.ID == ord.Combo.ID)
                    {
                        al.Add(ord);
                    }
                }
                hsTmpCombID.Add(order.Combo.ID, order);
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
        #endregion

        #region 检查项目打印
        //interface = 'FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint' and printerindex = 5

        /// <summary>
        /// 检查项目打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="pacsDictionary"></param>
        /// <param name="isPreview"></param>
        /// <param name="ControlList"></param>
        private void Examine(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> pacsDictionary, bool isPreview, List<Control> ControlList)
        {
            if (pacsDictionary.Keys.Count > 0)
            {
                int iSet = 8;
                if (iPacsPrint == null)
                {
                    iPacsPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 5) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                }
                if (iPacsPrint == null)
                {
                    this.errInfo = "处方打印接口未实现！";
                }
                else
                {
                    foreach (string item in pacsDictionary.Keys)
                    {
                        if (pacsDictionary[item].Count > 0)
                        {
                            int i = 0;
                            int sameCombRows = 1;
                            IList<FS.HISFC.Models.Order.OutPatient.Order> alPrint = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                            string curComb = string.Empty;
                            foreach (FS.HISFC.Models.Order.OutPatient.Order detail in pacsDictionary[item])
                            {
                                if (curComb != detail.Combo.ID)
                                {
                                    curComb = detail.Combo.ID;
                                    sameCombRows++;
                                }
                            }
                            if (sameCombRows > 7)
                            {
                                foreach (FS.HISFC.Models.Order.OutPatient.Order detail in pacsDictionary[item])
                                {
                                    if (curComb != detail.Combo.ID)
                                    {
                                        curComb = detail.Combo.ID;
                                        i++;
                                    }
                                    alPrint.Add(detail);
                                    if (i > 7)
                                    {
                                        alPrint.Remove(detail);
                                        iPacsPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 5) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                                        iPacsPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alPrint, isPreview);
                                        Control c = iPacsPrint as Control;
                                        ControlList.Add(c);
                                        i = 0;
                                        alPrint.Clear();
                                        alPrint.Add(detail);
                                    }
                                }
                                iPacsPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 5) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                                iPacsPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alPrint, isPreview);
                                Control c1 = iPacsPrint as Control;
                                ControlList.Add(c1);
                            }
                            else
                            {
                                foreach (FS.HISFC.Models.Order.OutPatient.Order detail in pacsDictionary[item])
                                {
                                    alPrint.Add(detail);
                                }
                                iPacsPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 5) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                                iPacsPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alPrint, isPreview);
                                Control c = iPacsPrint as Control;
                                ControlList.Add(c);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 检验项目打印
        //interface = 'FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint' and printerindex = 7

        /// <summary>
        /// 检验项目打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="lisDictionary"></param>
        /// <param name="isPreview"></param>
        /// <param name="ControlList"></param>
        private void Inspection(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> lisDictionary, bool isPreview, List<Control> ControlList)
        {
            if (lisDictionary.Keys.Count > 0)
            {
                #region 试管带出屏蔽，不放打印后，放辅材带出
                //ArrayList alSepTube = new ArrayList();
                ArrayList alLisOrd = new ArrayList();
                //SeparationTubeTestInfo lisSep = new SeparationTubeTestInfo();
                FS.HISFC.Models.Order.OutPatient.Order ordTmp = new FS.HISFC.Models.Order.OutPatient.Order();
                //foreach (string item in lisDictionary.Keys)
                //{
                //    for (int i = 0; i < lisDictionary[item].Count; i++)
                //    {
                //        ordTmp = lisDictionary[item][i].Clone();
                //        alLisOrd.Add(ordTmp);
                //    }
                //}
                //if (alSepTube != null)
                //{
                //    if (alSepTube.Count > 0)
                //    {
                //        int k = 0;
                //        SeparationTubeTestInfo[] lisSepTube = new SeparationTubeTestInfo[alSepTube.Count];
                //        foreach (SeparationTubeTestInfo listmp in alSepTube)
                //        {
                //            if (listmp != null)
                //            {
                //                lisSepTube[k] = listmp;
                //            }
                //            k++;
                //        }
                //        List<SeparationTubeTestInfo> sp = new List<SeparationTubeTestInfo>() { new SeparationTubeTestInfo() { HospitalId = "00001", TestId = "08733", SexId = "F", PriorityId = "R" } };

                //        Common.LisGetTubeSub lisSubMgr = new FS.SOC.Local.Order.OutPatientOrder.GYZL.Common.LisGetTubeSub();
                //        TubeGroupInfo lisTubeGPI = lisSubMgr.InvokeWebservice(sp.ToArray());
                //        //ServiceReference1.HisInterfaceClient sf = new FS.SOC.Local.Order.OutPatientOrder.ServiceReference1.HisInterfaceClient();
                //        ////sf.GetTubeGroupsForHisCode(sp.ToArray());
                //        //sf.GetTubeGroups(sp.ToArray());
                //        if (lisTubeGPI != null)
                //        {
                //            lisSubMgr.GetHisTubeSub(lisTubeGPI, ordTmp);
                //        }
                //    }
                //}
                #endregion

                if (iLisPrint == null)
                {
                    iLisPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 7) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                }

                if (iLisPrint == null)
                {
                    this.errInfo = "处方打印接口未实现！";
                }
                else
                {
                    List<FS.HISFC.Models.Order.OutPatient.Order> OneItem = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                    //for (int t = 0; t < alLisOrd.Count; t++)
                    //{
                    //    OneItem.Add(alLisOrd[t] as FS.HISFC.Models.Order.OutPatient.Order);
                    //}
                    //iLisPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, OneItem, isPreview);
                    //if (OneItem.Count > 0)
                    //{
                    //    ControlList.Add(iLisPrint as Control);
                    //}
                    int itemPerPageLimit = 10;
                    int curCount = 0;
                    ArrayList hsPackage = new ArrayList();

                    foreach (string item in lisDictionary.Keys)
                    {
                        alLisOrd.Clear();
                        alLisOrd.AddRange(lisDictionary[item]);
                        for (int i = 0; i < alLisOrd.Count; i++)
                        {
                            FS.HISFC.Models.Order.OutPatient.Order outOrder = alLisOrd[i] as FS.HISFC.Models.Order.OutPatient.Order;
                            if (outOrder.Item.ID != "999"
                                && "1".Equals(SOC.HISFC.BizProcess.Cache.Fee.GetItem(outOrder.Item.ID).UnitFlag))
                            {
                                OneItem.Add(outOrder);
                                curCount++;
                            }
                            else
                            {
                                string itemCode = outOrder.ApplyNo;
                                if (!hsPackage.Contains(itemCode) && !string.IsNullOrEmpty(itemCode))
                                {
                                    hsPackage.Add(itemCode);
                                    OneItem.Add(outOrder);
                                    curCount++;
                                }
                                else if (hsPackage.Contains(itemCode))
                                {
                                    OneItem.Add(outOrder);
                                    if (i != alLisOrd.Count - 1)
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    OneItem.Add(outOrder);
                                    curCount++;
                                }
                            }
                            if (curCount == itemPerPageLimit || i == alLisOrd.Count - 1)
                            {
                                iLisPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, OneItem, isPreview);
                                OneItem.Clear();
                                ControlList.Add(iLisPrint as Control);
                                iLisPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 7) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                                curCount = 0;
                            }
                        }
                    }
                }
            }
        }

        private void InertLisSub(Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> lisDictionary)
        {

        }
        #endregion

        #region 诊断证明打印
        //interface = 'FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint' and printerindex = 6

        /// <summary>
        /// 诊断证明打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IOrderList"></param>
        /// <param name="isPreview"></param>
        private void Prove(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> IOrderList, bool isPreview)
        {
            if (object.Equals(siDiagnosePrint, null))
            {
                siDiagnosePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 6) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }

            if (!object.Equals(siDiagnosePrint, null))
            {
                siDiagnosePrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, IOrderList, isPreview);
            }
        }
        #endregion

        #region 实现门诊诊疗单

        /// <summary>
        /// 实现门诊诊疗单
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="IOrderList"></param>
        /// <param name="isPreview"></param>
        /// <param name="ControlList"></param>
        private void ClinicsItem(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, IList<FS.HISFC.Models.Order.OutPatient.Order> IOrderList, bool isPreview, List<Control> ControlList)
        {
            if (IOrderList.Count > 0)
            {
                try
                {
                    Hashtable hsClinics = new Hashtable();
                    foreach (FS.HISFC.Models.Order.OutPatient.Order ordTmp in IOrderList)
                    {
                        if (!hsClinics.Contains(ordTmp.ExeDept.ID))
                        {
                            hsClinics.Add(ordTmp.ExeDept.ID, ordTmp);
                        }
                    }
                    foreach (string s in hsClinics.Keys)
                    {
                        IList<FS.HISFC.Models.Order.OutPatient.Order> Ilist = new List<FS.HISFC.Models.Order.OutPatient.Order>();

                        foreach (FS.HISFC.Models.Order.OutPatient.Order ordTmp in IOrderList)
                        {
                            if (s == ordTmp.ExeDept.ID)
                            {
                                Ilist.Add(ordTmp);
                            }
                        }
                        if (Ilist.Count > 0)
                        {
                            //FS.SOC.Local.Order.OutPatientOrder.GYZL.ClinicsBillPrint.ucClinicsBillPrint clinicBill = new FS.SOC.Local.Order.OutPatientOrder.GYZL.ClinicsBillPrint.ucClinicsBillPrint();
                            ISOCTreatBillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.SOC.Local.Order.OutPatientOrder.GYZL.SaveOrderFactory), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 8) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                            ISOCTreatBillPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, Ilist, isPreview);
                            if (isPreview)
                            {
                                ControlList.Add(ISOCTreatBillPrint as Control);
                            }
                        }
                    }
                }
                catch
                {
                }
            }
        }

        #endregion

        /// <summary>
        /// 获取总药费、总注射费
        /// </summary>
        private string getTotalMoney(Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> drugDictionary, FS.HISFC.Models.Registration.Register regObj)
        {
            decimal phaMoney = 0m;//药费
            decimal injectMoney = 0m;//注射费

            ArrayList alFee;

            string label = string.Empty;

            hsCombID = new Hashtable();

            //foreach (string drug in drugDictionary.Keys)
            //{
            //    foreach (FS.HISFC.Models.Order.OutPatient.Order order in drugDictionary[drug])
            //    {
            //        if (!GetOrderByCombId(order))
            //        {

            //            alFee = this.outPatientManager.QueryFeeDetailbyComoNOAndClinicCode(order.Combo.ID, regObj.ID);
            //            if (alFee != null && alFee.Count >= 1)
            //            {
            //                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
            //                {
            //                    if (!itemlist.Item.IsMaterial)
            //                    {
            //                        itemlist.FT.TotCost = itemlist.FT.OwnCost + itemlist.FT.PayCost + itemlist.FT.PubCost;
            //                        phaMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//药品金额
            //                    }
            //                    else
            //                    {
            //                        injectMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//注射费
            //                    }


            //                }
            //            }
            //        }
            //    }

            //}
            label = "(本次诊疗 药费:￥" + phaMoney.ToString("F2") + " 注射费:￥" + injectMoney.ToString("F2") + " 应付总计:￥" + (phaMoney + injectMoney).ToString("F2") + ")";
            return label;

        }

        /// <summary>
        /// 判断医嘱组合号是否已经存在
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool GetOrderByCombId(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            if (!hsCombID.Contains(order.Combo.ID))
            {
                hsCombID.Add(order.Combo.ID, order);
                return false;
            }
            else
            {
                return true;
            }

        }
        #endregion

        #region IOutPatientPrint 成员

        public string ErrInfo
        {
            get
            {
                return this.errInfo;
            }
            set
            {
                this.errInfo = value;
            }

        }

        /// <summary>
        /// 门诊单据打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="orderPrintList"></param>
        /// <param name="orderSelectList"></param>
        /// <param name="IsReprint"></param>
        /// <param name="isPreview"></param>
        /// <param name="printType"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int OnOutPatientPrint(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, List<FS.HISFC.Models.Order.OutPatient.Order> orderPrintList, List<FS.HISFC.Models.Order.OutPatient.Order> orderSelectList, bool IsReprint, bool isPreview, string printType, object obj)
        {

            //转换为Ilist,ArrayList效率低下，不安全应该摒弃
            IList<FS.HISFC.Models.Order.OutPatient.Order> IOrderList = OrderManagement.QueryOrderByCardNOandClinicNO(regObj.PID.CardNO, regObj.ID).Cast<FS.HISFC.Models.Order.OutPatient.Order>().ToList();
            Dictionary<Int32, List<Control>> previewDictionary = new Dictionary<Int32, List<Control>>();

            List<Control> recipeControlList = new List<Control>();

            List<Control> clinicsControlList = new List<Control>();

            List<Control> guideList = new List<Control>();

            List<Control> lisControlList = new List<Control>();

            List<Control> pacsControlList = new List<Control>();

            List<Control> injectionControlList = new List<Control>();

            List<Control> treatMentControlList = new List<Control>();

            List<FS.HISFC.Models.Order.OutPatient.Order> clinicsList = new List<FS.HISFC.Models.Order.OutPatient.Order>();

            //List<FS.HISFC.Models.Order.OutPatient.Order> ZLDList = new List<FS.HISFC.Models.Order.OutPatient.Order>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> limitDrugDictionary = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> drugDictionary = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> pacsDictionary = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> lisDictionary = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> treatmentDictionary = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> guideDictionary = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            ArrayList alTdept = deptMgr.GetDepartment(EnumDepartmentType.T);
            Hashtable hsTdept = new Hashtable();
            ArrayList alPrintDept = new ArrayList();
            foreach (FS.HISFC.Models.Base.Const con in constantManager.GetList("PrintClinicsBillDept"))
            {
                alPrintDept.Add(con.ID);
            }
            if (alTdept != null)
            {
                if (alTdept.Count > 0)
                {
                    foreach (FS.HISFC.Models.Base.Department dept in alTdept)
                    {
                        if (dept != null && !hsCombID.ContainsKey(dept.ID))
                        {
                            if (alPrintDept.Contains(dept.ID))//通过PrintClinicsBillDept常数维护需要单独打印诊疗单的科室
                            {
                                continue;
                            }
                            hsTdept.Add(dept.ID, dept);
                        }
                    }
                }
            }

            foreach (FS.HISFC.Models.Order.OutPatient.Order order in orderPrintList)
            {
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item PharmacyItem = order.Item as FS.HISFC.Models.Pharmacy.Item;

                    if (PharmacyItem.Quality.ID.Equals("P") || PharmacyItem.Quality.ID.Equals("P1") || PharmacyItem.Quality.ID.Equals("P2") || PharmacyItem.Quality.ID.Equals("S"))
                    {
                        //this.SetOrderList(limitDrugDictionary, order);
                        this.SetOrderList(drugDictionary, order);
                    }
                    else
                    {
                        this.SetOrderList(drugDictionary, order);
                    }
                }
                else if (order.Item.ItemType == EnumItemType.UnDrug)
                {
                    #region 屏蔽原有代码
                    //switch (order.Item.SysClass.ID.ToString())
                    //{
                    //    case "UL":
                    //        this.SetOrderList(lisDictionary, order);
                    //        break;
                    //    case "UC":
                    //        this.SetOrderList(pacsDictionary, order);
                    //        break;
                    //    //除了处方、检查单、检验单之外的都打印治疗单
                    //    case "UZ":
                    //        //this.SetOrderList(treatmentDictionary, order);
                    //        //break;
                    //    case "UO":
                    //        //this.SetOrderList(treatmentDictionary, order);
                    //        //break;
                    //    default:
                    //        //ZLDList.Add(order);
                    //        clinicsList.Add(order);
                    //        break;
                    //}
                    #endregion
                    #region 新处理单据分类
                    if (order.ExeDept.ID == "6009" || order.ExeDept.ID == "4017")
                    {
                        this.SetOrderList(lisDictionary, order);
                    }
                    else
                    {
                        if (hsTdept.ContainsKey(order.ExeDept.ID))
                        {
                            this.SetOrderList(pacsDictionary, order);
                        }
                        else if (!order.IsSubtbl)
                        {
                            //非附材带出加入诊疗单
                            //例子：如果药品带出附材因为在处方上已经加入了材料费的统计，如果在诊疗单再打印则金额会错误
                            clinicsList.Add(order);
                        }
                    }
                    #endregion
                }
            }

            //根据传递的单据类型打印
            switch ((EnumOutPatientBill)FS.FrameWork.Function.NConvert.ToInt32(printType))
            {
                case EnumOutPatientBill.AllBill:
                    CommonDrug(regObj, drugDictionary, new ArrayList(orderPrintList), recipeControlList, isPreview);        //普通处方
                    //AnestheticChlorpromazine(regObj, limitDrugDictionary, alOrder);  //麻毒精处方
                    //Injection(regObj, reciptDept, reciptDoct, new ArrayList(orderPrintList), isPreview, injectionControlList);    //注射单
                    Examine(regObj, reciptDept, reciptDoct, pacsDictionary, isPreview, pacsControlList);      //检查
                    Inspection(regObj, reciptDept, reciptDoct, lisDictionary, isPreview, lisControlList);    //检验
                    ClinicsItem(regObj, reciptDept, reciptDoct, clinicsList, isPreview, clinicsControlList);//诊疗单
                    MedicalRecord(regObj, reciptDept, reciptDoct, IOrderList, isPreview, guideList); //门诊记录单


                    if (recipeControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.RecipeBill, recipeControlList);
                    }
                    if (pacsControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.PacsBill, pacsControlList);
                    }
                    if (lisControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.LisBill, lisControlList);
                    }
                    if (injectionControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.InjectBill, injectionControlList);
                    }
                    if (clinicsControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.ClinicsBill, clinicsControlList);
                    }
                    if (guideList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.GuideBill, guideList);
                    }
                    break;
                case EnumOutPatientBill.RecipeBill:
                    CommonDrug(regObj, drugDictionary, new ArrayList(orderPrintList), recipeControlList, isPreview);        //普通处方
                    //AnestheticChlorpromazine(regObj, limitDrugDictionary, alOrder);  //麻毒精处方
                    if (recipeControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.RecipeBill, recipeControlList);
                    }
                    break;
                case EnumOutPatientBill.InjectBill:
                    Injection(regObj, reciptDept, reciptDoct, new ArrayList(orderPrintList), isPreview, injectionControlList);    //注射单
                    if (injectionControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.InjectBill, injectionControlList);
                    }
                    break;
                case EnumOutPatientBill.TreatmentBill:



                    break;
                case EnumOutPatientBill.GuideBill:
                    MedicalRecord(regObj, reciptDept, reciptDoct, IOrderList, isPreview, guideList); //门诊记录单
                    if (guideList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.GuideBill, guideList);
                    }
                    break;
                case EnumOutPatientBill.PacsBill:
                    Examine(regObj, reciptDept, reciptDoct, pacsDictionary, isPreview, pacsControlList);      //检查
                    if (pacsControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.PacsBill, pacsControlList);
                    }
                    break;
                case EnumOutPatientBill.LisBill:
                    Inspection(regObj, reciptDept, reciptDoct, lisDictionary, isPreview, lisControlList);    //检验
                    if (lisControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.LisBill, lisControlList);
                    }
                    break;
                //case EnumOutPatientBill.ClinicsBill:
                //    ClinicsItem(regObj, reciptDept, reciptDoct, clinicsList, isPreview, clinicsControlList);//诊疗单

                //    if (clinicsControlList.Count > 0)
                //    {
                //        previewDictionary.Add((int)EnumOutPatientBill.ClinicsBill, clinicsControlList);
                //    }

                //    break;
                case EnumOutPatientBill.ClinicsBill:
                    ClinicsItem(regObj, reciptDept, reciptDoct, clinicsList, isPreview, clinicsControlList);//诊疗单
                    if (clinicsControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.ClinicsBill, clinicsControlList);
                    }
                    break;
                case EnumOutPatientBill.DiagNoseBill:
                    Prove(regObj, reciptDept, reciptDept, orderPrintList, isPreview);
                    break;
                default: //医嘱保存后需要打印单据
                    CommonDrug(regObj, drugDictionary, new ArrayList(orderPrintList), recipeControlList, isPreview);        //普通处方
                    //AnestheticChlorpromazine(regObj, limitDrugDictionary, alOrder);  //麻毒精处方
                    Injection(regObj, reciptDept, reciptDoct, new ArrayList(orderPrintList), isPreview, injectionControlList);    //注射单
                    MedicalRecord(regObj, reciptDept, reciptDoct, IOrderList, isPreview, clinicsControlList); //门诊记录单
                    Examine(regObj, reciptDept, reciptDoct, pacsDictionary, isPreview, pacsControlList);      //检查
                    Inspection(regObj, reciptDept, reciptDoct, lisDictionary, isPreview, lisControlList);    //检验
                    ClinicsItem(regObj, reciptDept, reciptDoct, clinicsList, isPreview, clinicsControlList);//诊疗单
                    if (recipeControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.RecipeBill, recipeControlList);
                    }
                    if (clinicsControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.ClinicsBill, clinicsControlList);
                    }
                    if (injectionControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.InjectBill, injectionControlList);
                    }
                    if (lisControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.LisBill, lisControlList);
                    }
                    if (pacsControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.PacsBill, pacsControlList);
                    }
                    break;
            }

            if (isPreview && previewDictionary.Keys.Count > 0)
            {
                FS.SOC.Local.Order.OutPatientOrder.Common.frmPreviewControl frmPreviewControl = new FS.SOC.Local.Order.OutPatientOrder.Common.frmPreviewControl(previewDictionary);

                frmPreviewControl.ShowPreviewControlDialog();

                frmPreviewControl.ShowDialog();
            }
            return 1;
        }

        #endregion
    }
}
