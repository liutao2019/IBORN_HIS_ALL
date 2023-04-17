using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    public partial class ucCaseOnlySecPrint : UserControl, FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack
    {
        /// <summary>
        /// 病案首页第二页
        /// </summary>
        public ucCaseOnlySecPrint()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打印业务层
        /// </summary>
        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

        //费用业务层

        FS.HISFC.BizLogic.HealthRecord.Fee feeManager = new FS.HISFC.BizLogic.HealthRecord.Fee();
       
        //麻醉列表
        FS.FrameWork.Public.ObjectHelper NarcListHelper = new FS.FrameWork.Public.ObjectHelper();
        //择期
        private FS.FrameWork.Public.ObjectHelper selectOpDateHelper = new FS.FrameWork.Public.ObjectHelper();
        //单位列表
        FS.FrameWork.Public.ObjectHelper UnitListHelper = new FS.FrameWork.Public.ObjectHelper();
        //疗程列表
        FS.FrameWork.Public.ObjectHelper PeriodListHelper = new FS.FrameWork.Public.ObjectHelper();
       
        //字符转换函数
        FS.HISFC.Components.HealthRecord.Function fun = new Function();
        /// <summary>
        /// 设置预览比例 
        /// 新的FrameWork才有改属性
        /// </summary>
        private bool isPrintViewZoom = false;
        #region 初时化
        /// <summary>
        /// 
        /// </summary>
        private void LoadInfo()
        {
            try
            {
                FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = "-";
                obj.Name = "-";
                //切口列表
                ArrayList NickTypeList = Constant.GetList("INCITYPE");
                NickTypeList.Add(obj);
                this.txtOpNickType1.AddItems(NickTypeList);
                this.txtOpNickType2.AddItems(NickTypeList);
                this.txtOpNickType3.AddItems(NickTypeList);
                this.txtOpNickType4.AddItems(NickTypeList);
                this.txtOpNickType5.AddItems(NickTypeList);
                this.txtOpNickType6.AddItems(NickTypeList);
                //this.txtOpNickType7.AddItems(NickTypeList);
                //this.txtOpNickType8.AddItems(NickTypeList);
                //愈合列表
                ArrayList CicaTypelist = Constant.GetAllList("CICATYPE");
                CicaTypelist.Add(obj);
                this.txtOpCicaType1.AddItems(CicaTypelist);
                this.txtOpCicaType2.AddItems(CicaTypelist);
                this.txtOpCicaType3.AddItems(CicaTypelist);
                this.txtOpCicaType4.AddItems(CicaTypelist);
                this.txtOpCicaType5.AddItems(CicaTypelist);
                this.txtOpCicaType6.AddItems(CicaTypelist);
                //this.txtOpCicaType7.AddItems(CicaTypelist);
                //this.txtOpCicaType8.AddItems(CicaTypelist);
                //查询麻醉方式列表
                ArrayList NarcList = Constant.GetList("ANESTYPE");//Constant.GetList("CASEANESTYPE");
                this.txtOpAnesType1.AddItems(NarcList);
                this.txtOpAnesType2.AddItems(NarcList);
                this.txtOpAnesType3.AddItems(NarcList);
                this.txtOpAnesType4.AddItems(NarcList);
                this.txtOpAnesType5.AddItems(NarcList);
                this.txtOpAnesType6.AddItems(NarcList);
                //this.txtOpAnesType7.AddItems(NarcList);
                //this.txtOpAnesType8.AddItems(NarcList);
                NarcListHelper.ArrayObject = NarcList;

                ArrayList selectOpDatelist = Constant.GetList("CASESELECTOPDATE");
                selectOpDateHelper.ArrayObject = selectOpDatelist;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion
    

        #region 私有方法
        /// <summary>
        /// 处理出院诊断名称
        /// </summary>
        /// <param name="strDiag">出院诊断名称</param>
        /// <param name="Lenght">能显示的最大长度</param>
        /// <returns></returns>
        private string OpName(string strDiag, int Lenght)
        {
            string ret = string.Empty;
            if (Lenght >= 19)
            {
                if (strDiag.Length > 19)
                {
                    ret = strDiag.Substring(0, 19);
                }
                else
                {
                    ret = strDiag;
                }
            }
            //else if (Lenght >= 25)
            //{
            //    if (strDiag.Length > 25)
            //    {
            //        ret = strDiag.Substring(0, 25);
            //    }
            //    else
            //    {
            //        ret = strDiag;
            //    }
            //}
            return ret;
        }
        /// <summary>
        /// 返回默认字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string DefaultString(string str)
        {
            string ret = string.Empty;
            if (str == null)
            {
                ret = "-";
            }
            else if (str == "")
            {
                ret = "-";
            }
            else
            {
                ret = str;
            }
            return ret;
        }
        /// <summary>
        /// 常数列表
        /// </summary>
        private void InitList()
        {
            FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();

            //单位列表
            ArrayList UnitList = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.DOSEUNIT);
            UnitListHelper.ArrayObject = UnitList;

            //疗程列表 
            ArrayList PeriodList = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.PERIODOFTREATMENT);// da.GetPeriodList();
            PeriodListHelper.ArrayObject = PeriodList;
        }
        #endregion


        #region HealthRecordInterface 成员

        /// <summary>
        /// 设置病案首页第二页值
        /// </summary>
        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack.ControlValue(FS.HISFC.Models.HealthRecord.Base info)
        {
            this.LoadInfo();
            if (info.Ever_Sickintodeath == "1")
            {
                this.lblHavedOps.Text = "有无本项信息【】有【√】无";
            }
            else if (info.Ever_Sickintodeath == "2")
            {
                this.lblHavedOps.Text = "有无本项信息【√】有【】无";
            }
            else
            {
                this.lblHavedOps.Visible = false;
            }

            FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList opsCodePrint = Constant.GetList("CASENOTPRIOPSICD");
            bool isNotPrintOpsCode = false;
            if (opsCodePrint != null && opsCodePrint.Count > 0)
            {
                isNotPrintOpsCode = true;
            }
            #region 手术信息
            FS.HISFC.BizLogic.HealthRecord.Operation operationManager = new FS.HISFC.BizLogic.HealthRecord.Operation();
            ArrayList alOpr = operationManager.QueryOperationByInpatientNo(info.PatientInfo.ID);
            if (alOpr != null)
            {
                int rowNumber = 1;
                string opName = string.Empty;
                foreach (FS.HISFC.Models.HealthRecord.OperationDetail operationInfo in alOpr)
                {
                    switch (rowNumber)
                    {
                        case 1:
                            #region
                            //编码
                            if (isNotPrintOpsCode)
                            {
                                this.txtOpCode1.Text = "";
                            }
                            else
                            {
                                if (operationInfo.OperationInfo.ID == "MS999")
                                {
                                    this.txtOpCode1.Text = "";
                                }
                                else
                                {
                                    this.txtOpCode1.Text = operationInfo.OperationInfo.ID;
                                }
                            }
                            //日期
                            this.txtOpDate1.Text = operationInfo.OperationDate.ToShortDateString();
                            //手术级别
                            this.txtOpLevel1.Text = this.DefaultString(operationInfo.FourDoctInfo.Name);
                            //手术及操作名称
                            this.txtOpName1.Text = this.OpName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(operationInfo.OperationInfo.Name, false), 19);
                            //术者
                            this.txtOpDoc1.Text = operationInfo.FirDoctInfo.Name;
                            //一助
                            this.txtOpFirDoc1.Text = this.DefaultString(operationInfo.SecDoctInfo.Name);
                            //二助
                            this.txtOpSecDoc1.Text = this.DefaultString(operationInfo.ThrDoctInfo.Name);
                            //手术切口
                            this.txtOpNickType1.Tag = this.DefaultString(operationInfo.NickKind);
                            //愈合情况
                            this.txtOpCicaType1.Tag = this.DefaultString(operationInfo.CicaKind);
                            //择期手术
                            this.txtSelectOpDate1.Text = this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
                            //麻醉方式
                            opName = string.Empty;
                            //if (operationInfo.MarcKind != null && operationInfo.MarcKind != "")
                            //{
                            //    switch (operationInfo.MarcKind)
                            //    {
                            //        case "1": opName = "全麻";
                            //            break;
                            //        case "2": opName = "硬外";
                            //            break;
                            //        case "3": opName = "基础麻";
                            //            break;
                            //        case "4": opName = "基麻+局麻";
                            //            break;
                            //        case "5": opName = "局麻";
                            //            break;
                            //        case "6": opName = "腰麻";
                            //            break;
                            //        case "7": opName = "骶麻";
                            //            break;
                            //        case "8": opName = "臂丛";
                            //            break;
                            //        case "9": opName = "颈丛";
                            //            break;
                            //        case "10": opName = "表麻";
                            //            break;
                            //        case "11": opName = "静脉麻";
                            //            break;
                            //        case "12": opName = "气管麻";
                            //            break;
                            //        case "13": opName = "插管全麻";
                            //            break;
                            //        case "14": opName = "其它";
                            //            break;
                            //        default: opName = "未对照";
                            //            break;
                            //    }
                            //}
                            if (operationInfo.MarcKind != null && operationInfo.MarcKind != "")
                            {
                                opName = NarcListHelper.GetName(operationInfo.MarcKind);
                            }
                            if (operationInfo.OpbOpa != null && operationInfo.OpbOpa != "" && operationInfo.OpbOpa.Trim() != "-")
                            {
                                opName += "+" + NarcListHelper.GetName(operationInfo.OpbOpa);
                            }
                            if (opName.Length > 5)
                            {
                                this.txtOpAnesType1.Text = opName.Substring(0, 5);
                                this.txtOpAnesType11.Text = opName.Substring(5);
                            }
                            else
                            {
                                this.txtOpAnesType1.Text = this.DefaultString(opName);
                            }
                            //麻醉医师
                            this.txtOpAnesDoc1.Text = this.DefaultString(operationInfo.NarcDoctInfo.Name);
                            rowNumber++;
                            break;
                            #endregion
                        case 2:
                            #region
                            //编码
                            if (isNotPrintOpsCode)
                            {
                                this.txtOpCode2.Text = "";
                            }
                            else
                            {
                                if (operationInfo.OperationInfo.ID == "MS999")
                                {
                                    this.txtOpCode2.Text = "";
                                }
                                else
                                {
                                    this.txtOpCode2.Text = operationInfo.OperationInfo.ID;
                                }
                            }
                            //日期
                            this.txtOpDate2.Text = operationInfo.OperationDate.ToShortDateString();
                            //手术级别
                            this.txtOpLevel2.Text = this.DefaultString(operationInfo.FourDoctInfo.Name);
                            //手术及操作名称
                            this.txtOpName2.Text = this.OpName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(operationInfo.OperationInfo.Name, false), 19);
                            //术者
                            this.txtOpDoc2.Text = operationInfo.FirDoctInfo.Name;
                            //一助
                            this.txtOpFirDoc2.Text = this.DefaultString(operationInfo.SecDoctInfo.Name);
                            //二助
                            this.txtOpSecDoc2.Text = this.DefaultString(operationInfo.ThrDoctInfo.Name);
                            //手术切口
                            this.txtOpNickType2.Tag = this.DefaultString(operationInfo.NickKind);
                            //愈合情况
                            this.txtOpCicaType2.Tag = this.DefaultString(operationInfo.CicaKind);
                            //择期手术
                            this.txtSelectOpDate2.Text = this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
                            //麻醉方式
                            opName = string.Empty;
                            if (operationInfo.MarcKind != null && operationInfo.MarcKind != "")
                            {
                                opName = NarcListHelper.GetName(operationInfo.MarcKind);
                            }
                            if (operationInfo.OpbOpa != null && operationInfo.OpbOpa != "" && operationInfo.OpbOpa.Trim() != "-")
                            {
                                opName += "+" + NarcListHelper.GetName(operationInfo.OpbOpa);
                            }
                            if (opName.Length > 5)
                            {
                                this.txtOpAnesType2.Text = opName.Substring(0, 5);
                                this.txtOpAnesType21.Text = opName.Substring(5);
                            }
                            else
                            {
                                this.txtOpAnesType2.Text = this.DefaultString(opName);
                            }
                            //麻醉医师
                            this.txtOpAnesDoc2.Text = this.DefaultString(operationInfo.NarcDoctInfo.Name);
                            rowNumber++;
                            break;
                            #endregion
                        case 3:
                            #region
                            //编码
                            if (isNotPrintOpsCode)
                            {
                                this.txtOpCode3.Text = "";
                            }
                            else
                            {
                                if (operationInfo.OperationInfo.ID == "MS999")
                                {
                                    this.txtOpCode3.Text = "";
                                }
                                else
                                {
                                    this.txtOpCode3.Text = operationInfo.OperationInfo.ID;
                                }
                            }
                            //日期
                            this.txtOpDate3.Text = operationInfo.OperationDate.ToShortDateString();
                            //手术级别
                            this.txtOpLevel3.Text = this.DefaultString(operationInfo.FourDoctInfo.Name);
                            //手术及操作名称
                            this.txtOpName3.Text = this.OpName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(operationInfo.OperationInfo.Name, false), 19);
                            //术者
                            this.txtOpDoc3.Text = operationInfo.FirDoctInfo.Name;
                            //一助
                            this.txtOpFirDoc3.Text = this.DefaultString(operationInfo.SecDoctInfo.Name);
                            //二助
                            this.txtOpSecDoc3.Text = this.DefaultString(operationInfo.ThrDoctInfo.Name);
                            //手术切口
                            this.txtOpNickType3.Tag = this.DefaultString(operationInfo.NickKind);
                            //愈合情况
                            this.txtOpCicaType3.Tag = this.DefaultString(operationInfo.CicaKind);
                            //择期手术
                            this.txtSelectOpDate3.Text = this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
                            //麻醉方式
                            opName = string.Empty;
                            if (operationInfo.MarcKind != null && operationInfo.MarcKind != "")
                            {
                                opName = NarcListHelper.GetName(operationInfo.MarcKind);
                            }
                            if (operationInfo.OpbOpa != null && operationInfo.OpbOpa != "" && operationInfo.OpbOpa.Trim() != "-")
                            {
                                opName += "+" + NarcListHelper.GetName(operationInfo.OpbOpa);
                            }
                            if (opName.Length > 5)
                            {
                                this.txtOpAnesType3.Text = opName.Substring(0, 5);
                                this.txtOpAnesType31.Text = opName.Substring(5);
                            }
                            else
                            {
                                this.txtOpAnesType3.Text = this.DefaultString(opName);
                            }
                            //麻醉医师
                            this.txtOpAnesDoc3.Text = this.DefaultString(operationInfo.NarcDoctInfo.Name);
                            rowNumber++;
                            break;
                            #endregion
                        case 4:
                            #region
                            //编码
                            if (isNotPrintOpsCode)
                            {
                                this.txtOpCode4.Text = "";
                            }
                            else
                            {
                                if (operationInfo.OperationInfo.ID == "MS999")
                                {
                                    this.txtOpCode4.Text = "";
                                }
                                else
                                {
                                    this.txtOpCode4.Text = operationInfo.OperationInfo.ID;
                                }
                            }
                            //日期
                            this.txtOpDate4.Text = operationInfo.OperationDate.ToShortDateString();
                            //手术级别
                            this.txtOpLevel4.Text = this.DefaultString(operationInfo.FourDoctInfo.Name);
                            //手术及操作名称
                            this.txtOpName4.Text = this.OpName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(operationInfo.OperationInfo.Name, false), 19);
                            //术者
                            this.txtOpDoc4.Text = operationInfo.FirDoctInfo.Name;
                            //一助
                            this.txtOpFirDoc4.Text = this.DefaultString(operationInfo.SecDoctInfo.Name);
                            //二助
                            this.txtOpSecDoc4.Text = this.DefaultString(operationInfo.ThrDoctInfo.Name);
                            //手术切口
                            this.txtOpNickType4.Tag = this.DefaultString(operationInfo.NickKind);
                            //愈合情况
                            this.txtOpCicaType4.Tag = this.DefaultString(operationInfo.CicaKind);
                            //择期手术
                            this.txtSelectOpDate4.Text = this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
                            //麻醉方式
                            opName = string.Empty;
                            if (operationInfo.MarcKind != null && operationInfo.MarcKind != "")
                            {
                                opName = NarcListHelper.GetName(operationInfo.MarcKind);
                            }
                            if (operationInfo.OpbOpa != null && operationInfo.OpbOpa != "" && operationInfo.OpbOpa.Trim() != "-") 
                            {
                                opName += "+" + NarcListHelper.GetName(operationInfo.OpbOpa);
                            }
                            if (opName.Length > 5)
                            {
                                this.txtOpAnesType4.Text = opName.Substring(0, 5);
                                this.txtOpAnesType41.Text = opName.Substring(5);
                            }
                            else
                            {
                                this.txtOpAnesType4.Text = this.DefaultString(opName);
                            }
                            //麻醉医师
                            this.txtOpAnesDoc4.Text = this.DefaultString(operationInfo.NarcDoctInfo.Name);
                            rowNumber++;
                            break;
                            #endregion
                        case 5:
                            #region
                            //编码
                            if (isNotPrintOpsCode)
                            {
                                this.txtOpCode5.Text = "";
                            }
                            else
                            {
                                if (operationInfo.OperationInfo.ID == "MS999")
                                {
                                    this.txtOpCode5.Text = "";
                                }
                                else
                                {
                                    this.txtOpCode5.Text = operationInfo.OperationInfo.ID;
                                }
                            }
                            //日期
                            this.txtOpDate5.Text = operationInfo.OperationDate.ToShortDateString();
                            //手术级别
                            this.txtOpLevel5.Text = this.DefaultString(operationInfo.FourDoctInfo.Name);
                            //手术及操作名称
                            this.txtOpName5.Text = this.OpName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(operationInfo.OperationInfo.Name, false), 19);
                            //术者
                            this.txtOpDoc5.Text = operationInfo.FirDoctInfo.Name;
                            //一助
                            this.txtOpFirDoc5.Text = this.DefaultString(operationInfo.SecDoctInfo.Name);
                            //二助
                            this.txtOpSecDoc5.Text = this.DefaultString(operationInfo.ThrDoctInfo.Name);
                            //手术切口
                            this.txtOpNickType5.Tag = this.DefaultString(operationInfo.NickKind);
                            //愈合情况
                            this.txtOpCicaType5.Tag = this.DefaultString(operationInfo.CicaKind);
                            //择期手术
                            this.txtSelectOpDate5.Text = this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
                            //麻醉方式
                            opName = string.Empty;
                            if (operationInfo.MarcKind != null && operationInfo.MarcKind != "")
                            {
                                opName = NarcListHelper.GetName(operationInfo.MarcKind);
                            }
                            if (operationInfo.OpbOpa != null && operationInfo.OpbOpa != "" && operationInfo.OpbOpa.Trim() != "-") 
                            {
                                opName += "+" + NarcListHelper.GetName(operationInfo.OpbOpa);
                            }
                            if (opName.Length > 5)
                            {
                                this.txtOpAnesType5.Text = opName.Substring(0, 5);
                                this.txtOpAnesType51.Text = opName.Substring(5);
                            }
                            else
                            {
                                this.txtOpAnesType5.Text = this.DefaultString(opName);
                            }
                            //麻醉医师
                            this.txtOpAnesDoc5.Text = this.DefaultString(operationInfo.NarcDoctInfo.Name);
                            rowNumber++;
                            break;
                            #endregion
                        case 6:
                            #region
                            //编码
                            if (isNotPrintOpsCode)
                            {
                                this.txtOpCode6.Text = "";
                            }
                            else
                            {
                                if (operationInfo.OperationInfo.ID == "MS999")
                                {
                                    this.txtOpCode6.Text = "";
                                }
                                else
                                {
                                    this.txtOpCode6.Text = operationInfo.OperationInfo.ID;
                                }
                            }
                            //日期
                            this.txtOpDate6.Text = operationInfo.OperationDate.ToShortDateString();
                            //手术级别
                            this.txtOpLevel6.Text = this.DefaultString(operationInfo.FourDoctInfo.Name);
                            //手术及操作名称
                            this.txtOpName6.Text = this.OpName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(operationInfo.OperationInfo.Name, false), 19);
                            //术者
                            this.txtOpDoc6.Text = operationInfo.FirDoctInfo.Name;
                            //一助
                            this.txtOpFirDoc6.Text = this.DefaultString(operationInfo.SecDoctInfo.Name);
                            //二助
                            this.txtOpSecDoc6.Text = this.DefaultString(operationInfo.ThrDoctInfo.Name);
                            //手术切口
                            this.txtOpNickType6.Tag = this.DefaultString(operationInfo.NickKind);
                            //愈合情况
                            this.txtOpCicaType6.Tag = this.DefaultString(operationInfo.CicaKind);
                            //择期手术
                            this.txtSelectOpDate6.Text = this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
                            //麻醉方式
                            opName = string.Empty;
                            if (operationInfo.MarcKind != null && operationInfo.MarcKind != "")
                            {
                                opName = NarcListHelper.GetName(operationInfo.MarcKind);
                            }
                            if (operationInfo.OpbOpa != null && operationInfo.OpbOpa != "" && operationInfo.OpbOpa.Trim() != "-") 
                            {
                                opName += "+" + NarcListHelper.GetName(operationInfo.OpbOpa);
                            }
                            if (opName.Length > 5)
                            {
                                this.txtOpAnesType6.Text = opName.Substring(0, 5);
                                this.txtOpAnesType61.Text = opName.Substring(5);
                            }
                            else
                            {
                                this.txtOpAnesType6.Text = this.DefaultString(opName);
                            }
                            //麻醉医师
                            this.txtOpAnesDoc6.Text = this.DefaultString(operationInfo.NarcDoctInfo.Name);
                            rowNumber++;
                            break;
                            #endregion
                        //case 7:
                        //    #region
                        //    //编码
                        //    if (isNotPrintOpsCode)
                        //    {
                        //        this.txtOpCode7.Text = "";
                        //    }
                        //    else
                        //    {
                        //        if (operationInfo.OperationInfo.ID == "MS999")
                        //        {
                        //            this.txtOpCode7.Text = "";
                        //        }
                        //        else
                        //        {
                        //            this.txtOpCode7.Text = operationInfo.OperationInfo.ID;
                        //        }
                        //    }
                        //    //日期
                        //    this.txtOpDate7.Text = operationInfo.OperationDate.ToShortDateString();
                        //    //手术级别
                        //    this.txtOpLevel7.Text = this.DefaultString(operationInfo.FourDoctInfo.Name);
                        //    //手术及操作名称
                        //    this.txtOpName7.Text = this.OpName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(operationInfo.OperationInfo.Name, false), 19);
                        //    //术者
                        //    this.txtOpDoc7.Text = operationInfo.FirDoctInfo.Name;
                        //    //一助
                        //    this.txtOpFirDoc7.Text = this.DefaultString(operationInfo.SecDoctInfo.Name);
                        //    //二助
                        //    this.txtOpSecDoc7.Text = this.DefaultString(operationInfo.ThrDoctInfo.Name);
                        //    //手术切口
                        //    this.txtOpNickType7.Tag = this.DefaultString(operationInfo.NickKind);
                        //    //愈合情况
                        //    this.txtOpCicaType7.Tag = this.DefaultString(operationInfo.CicaKind);
                        //    //择期手术
                        //    this.txtSelectOpDate7.Text = this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
                        //    //麻醉方式
                        //    opName = string.Empty;
                        //    if (operationInfo.MarcKind != null && operationInfo.MarcKind != "")
                        //    {
                        //        opName = NarcListHelper.GetName(operationInfo.MarcKind);
                        //    }
                        //    if (operationInfo.OpbOpa != null && operationInfo.OpbOpa != "" && operationInfo.OpbOpa.Trim() != "-") 
                        //    {
                        //        opName += "+" + NarcListHelper.GetName(operationInfo.OpbOpa);
                        //    }
                        //    if (opName.Length > 5)
                        //    {
                        //        this.txtOpAnesType7.Text = opName.Substring(0, 5);
                        //        this.txtOpAnesType71.Text = opName.Substring(5);
                        //    }
                        //    else
                        //    {
                        //        this.txtOpAnesType7.Text = this.DefaultString(opName);
                        //    }
                        //    //麻醉医师
                        //    this.txtOpAnesDoc7.Text = this.DefaultString(operationInfo.NarcDoctInfo.Name);
                        //    rowNumber++;
                        //    break;
                        //    #endregion
                        //case 8:
                        //    #region
                        //    //编码
                        //    if (isNotPrintOpsCode)
                        //    {
                        //        this.txtOpCode8.Text = "";
                        //    }
                        //    else
                        //    {
                        //        if (operationInfo.OperationInfo.ID == "MS999")
                        //        {
                        //            this.txtOpCode8.Text = "";
                        //        }
                        //        else
                        //        {
                        //            this.txtOpCode8.Text = operationInfo.OperationInfo.ID;
                        //        }
                        //    }
                        //    //日期
                        //    this.txtOpDate8.Text = operationInfo.OperationDate.ToShortDateString();
                        //    //手术级别
                        //    this.txtOpLevel8.Text = this.DefaultString(operationInfo.FourDoctInfo.Name);
                        //    //手术及操作名称
                        //    this.txtOpName8.Text = this.OpName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(operationInfo.OperationInfo.Name, false), 19);
                        //    //术者
                        //    this.txtOpDoc8.Text = operationInfo.FirDoctInfo.Name;
                        //    //一助
                        //    this.txtOpFirDoc8.Text = this.DefaultString(operationInfo.SecDoctInfo.Name);
                        //    //二助
                        //    this.txtOpSecDoc8.Text = this.DefaultString(operationInfo.ThrDoctInfo.Name);
                        //    //手术切口
                        //    this.txtOpNickType8.Tag = this.DefaultString(operationInfo.NickKind);
                        //    //愈合情况
                        //    this.txtOpCicaType8.Tag = this.DefaultString(operationInfo.CicaKind);
                        //    //择期手术
                        //    this.txtSelectOpDate8.Text = this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
                        //    //麻醉方式
                        //    opName = string.Empty;
                        //    if (operationInfo.MarcKind != null && operationInfo.MarcKind != "")
                        //    {
                        //        opName = NarcListHelper.GetName(operationInfo.MarcKind);
                        //    }
                        //    if (operationInfo.OpbOpa != null && operationInfo.OpbOpa != "" && operationInfo.OpbOpa.Trim() != "-") 
                        //    {
                        //        opName += "+" + NarcListHelper.GetName(operationInfo.OpbOpa);
                        //    }
                        //    if (opName.Length > 5)
                        //    {
                        //        this.txtOpAnesType8.Text = opName.Substring(0, 5);
                        //        this.txtOpAnesType81.Text = opName.Substring(5);
                        //    }
                        //    else
                        //    {
                        //        this.txtOpAnesType8.Text = this.DefaultString(opName);
                        //    }
                        //    //麻醉医师
                        //    this.txtOpAnesDoc8.Text = this.DefaultString(operationInfo.NarcDoctInfo.Name);
                        //    rowNumber++;
                        //    break;
                        //    #endregion
                        default:
                            break;
                    }
                }
            }
            #endregion

            this.txtLeaveHopitalType.Text = this.fun.ReturnStringValue(info.Out_Type,true);
            this.txtHighReceiveHopital.Text = this.fun.ReturnStringValue(info.HighReceiveHopital,true);
            this.txtLowerReceiveHopital.Text = this.fun.ReturnStringValue(info.LowerReceiveHopital,true);
            this.txtComeBackInMonth.Text = this.fun.ReturnStringValue(info.ComeBackInMonth,true);
            this.txtComeBackPurpose.Text = this.fun.ReturnStringValue(info.ComeBackPurpose,true);
            this.txtOutComeDay.Text = info.OutComeDay.ToString();
            this.txtOutComeHour.Text = info.OutComeHour.ToString();
            this.txtOutComeMin.Text = info.OutComeMin.ToString();
            this.txtInComeDay.Text = info.InComeDay.ToString();
            this.txtInComeHour.Text = info.InComeHour.ToString();
            this.txtInComeMin.Text = info.InComeMin.ToString();

            #region 费用信息
            DataSet ds = new DataSet();
            this.feeManager.QueryFeeForDrgsByInpatientNO(info.PatientInfo.ID, ref ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.FeeTotCost.Text = ds.Tables[0].Rows[0][0].ToString();//总费用


                this.FeeOwnCost.Text = ds.Tables[0].Rows[0][1].ToString();//自负金额
                //this.txtFee1.Text = ds.Tables[0].Rows[0][2].ToString();//一般医疗服务费
                //this.txtFee2.Text = ds.Tables[0].Rows[0][3].ToString();//一般治疗操作费
                //this.txtFee3.Text = ds.Tables[0].Rows[0][4].ToString();//护理费


                //this.txtFee4.Text = ds.Tables[0].Rows[0][5].ToString();//其他费用
                //this.txtFee5.Text = ds.Tables[0].Rows[0][6].ToString();//病理诊断费


                //this.txtFee6.Text = ds.Tables[0].Rows[0][7].ToString();//实验室诊断费
                //this.txtFee7.Text = ds.Tables[0].Rows[0][8].ToString();//影像诊断费


                //this.txtFee8.Text = ds.Tables[0].Rows[0][9].ToString();//临床诊断费


                //this.txtFee9.Text = ds.Tables[0].Rows[0][10].ToString();//非手术治疗项目费
                //this.txtFee91.Text = ds.Tables[0].Rows[0][11].ToString();//临床物理治疗费


                //this.txtFee10.Text = ds.Tables[0].Rows[0][12].ToString();//手术治疗费


                //this.txtFee101.Text = ds.Tables[0].Rows[0][13].ToString();//麻醉费


                //this.txtFee102.Text = ds.Tables[0].Rows[0][14].ToString();//手术费


                //this.txtFee11.Text = ds.Tables[0].Rows[0][15].ToString();//康复费


                //this.txtFee12.Text = ds.Tables[0].Rows[0][16].ToString();//中医治疗
                //this.txtFee13.Text = ds.Tables[0].Rows[0][17].ToString();//西药费


                //this.txtFee131.Text = ds.Tables[0].Rows[0][18].ToString();//抗菌药物费用
                //this.txtFee14.Text = ds.Tables[0].Rows[0][19].ToString();//中成药费
                //this.txtFee15.Text = ds.Tables[0].Rows[0][20].ToString();//中草药费
                //this.txtFee16.Text = ds.Tables[0].Rows[0][21].ToString();//血费


                //this.txtFee17.Text = ds.Tables[0].Rows[0][22].ToString();//白蛋白类制品费


                //this.txtFee18.Text = ds.Tables[0].Rows[0][23].ToString();//球蛋白类制品费


                //this.txtFee19.Text = ds.Tables[0].Rows[0][24].ToString();//凝血因子类制品费
                //this.txtFee20.Text = ds.Tables[0].Rows[0][25].ToString();//细胞因子类制品费
                //this.txtFee21.Text = ds.Tables[0].Rows[0][26].ToString();//检查用一次性医用材料费
                //this.txtFee22.Text = ds.Tables[0].Rows[0][27].ToString();//治疗用一次性医用材料费
                //this.txtFee23.Text = ds.Tables[0].Rows[0][28].ToString();//手术用一次性性医用材料费
                //this.txtFee24.Text = ds.Tables[0].Rows[0][29].ToString();//其他费


            }
            #endregion

            this.InitList();
            if (info.Ever_Firstaid == "1")
            {
                this.lblHavedBaby.Text = "有无本项信息【】有【√】无";
            }
            else if (info.Ever_Firstaid == "2")
            {
                this.lblHavedBaby.Text = "有无本项信息【√】有【】无";
            }
            else
            {
                this.lblHavedBaby.Visible = false;
            }
            if (info.Ever_Firstaid != "1")
            {
                #region 妇婴卡信息
                FS.HISFC.BizLogic.HealthRecord.Baby baby = new FS.HISFC.BizLogic.HealthRecord.Baby();
                ArrayList babyList = baby.QueryBabyByInpatientNo(info.PatientInfo.ID);
                int row = 1;
                if (babyList != null && babyList.Count > 0)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Baby babyInfo in babyList)
                    {
                        switch (row)
                        {
                            case 1:
                                //性别
                                if (babyInfo.SexCode == "M")
                                {
                                    this.txtBabySexM11.Text = "√";
                                }
                                else
                                {
                                    this.txtBabySexF12.Text = "√";
                                }
                                //分娩结果
                                if (babyInfo.BirthEnd == "1")
                                {
                                    this.txtBirEndH13.Text = "√";
                                }
                                else if (babyInfo.BirthEnd == "2")
                                {
                                    this.txtBirEndD14.Text = "√";
                                }
                                else if (babyInfo.BirthEnd == "3")
                                {
                                    this.txtBirEndD15.Text = "√";
                                }
                                //婴儿体重
                                this.txtWeight16.Text = babyInfo.Weight.ToString();
                                //婴儿转归
                                if (babyInfo.BabyState == "1")
                                {
                                    this.txtBabyStateD17.Text = "√";
                                }
                                else if (babyInfo.BabyState == "2")
                                {
                                    this.txtBabyStateZ18.Text = "√";
                                }
                                else if (babyInfo.BabyState == "3")
                                {
                                    this.txtBabyStateC19.Text = "√";
                                }
                                //呼吸
                                if (babyInfo.Breath == "1")
                                {
                                    this.txtBreathZ110.Text = "√";
                                }
                                else if (babyInfo.Breath == "2")
                                {
                                    this.txtBreathZS111.Text = "√";
                                }
                                else if (babyInfo.Breath == "3")
                                {
                                    this.txtBreathZS112.Text = "√";
                                }
                                //抢救次数
                                this.txtSalvTimes116.Text = babyInfo.SalvNum.ToString();
                                //成功抢救次数
                                this.txtSuccTimes117.Text = babyInfo.SuccNum.ToString();
                                row++;
                                break;
                            case 2:
                                //性别
                                if (babyInfo.SexCode == "M")
                                {
                                    this.txtBabySexM21.Text = "√";
                                }
                                else
                                {
                                    this.txtBabySexF22.Text = "√";
                                }
                                //分娩结果
                                if (babyInfo.BirthEnd == "1")
                                {
                                    this.txtBirEndH23.Text = "√";
                                }
                                else if (babyInfo.BirthEnd == "2")
                                {
                                    this.txtBirEndD24.Text = "√";
                                }
                                else if (babyInfo.BirthEnd == "3")
                                {
                                    this.txtBirEndD25.Text = "√";
                                }
                                //婴儿体重
                                this.txtWeight26.Text = babyInfo.Weight.ToString();
                                //婴儿转归
                                if (babyInfo.BabyState == "1")
                                {
                                    this.txtBabyStateD27.Text = "√";
                                }
                                else if (babyInfo.BabyState == "2")
                                {
                                    this.txtBabyStateZ28.Text = "√";
                                }
                                else if (babyInfo.BabyState == "3")
                                {
                                    this.txtBabyStateC29.Text = "√";
                                }
                                //呼吸
                                if (babyInfo.Breath == "1")
                                {
                                    this.txtBreathZ210.Text = "√";
                                }
                                else if (babyInfo.Breath == "2")
                                {
                                    this.txtBreathZS211.Text = "√";
                                }
                                else if (babyInfo.Breath == "3")
                                {
                                    this.txtBreathZS212.Text = "√";
                                }
                                //抢救次数
                                this.txtSalvTimes216.Text = babyInfo.SalvNum.ToString();
                                //成功抢救次数
                                this.txtSuccTimes217.Text = babyInfo.SuccNum.ToString();
                                row++;
                                break;
                            case 3:
                                //性别
                                if (babyInfo.SexCode == "M")
                                {
                                    this.txtBabySexM31.Text = "√";
                                }
                                else
                                {
                                    this.txtBabySexF32.Text = "√";
                                }
                                //分娩结果
                                if (babyInfo.BirthEnd == "1")
                                {
                                    this.txtBirEndH33.Text = "√";
                                }
                                else if (babyInfo.BirthEnd == "2")
                                {
                                    this.txtBirEndD34.Text = "√";
                                }
                                else if (babyInfo.BirthEnd == "3")
                                {
                                    this.txtBirEndD35.Text = "√";
                                }
                                //婴儿体重
                                this.txtWeight36.Text = babyInfo.Weight.ToString();
                                //婴儿转归
                                if (babyInfo.BabyState == "1")
                                {
                                    this.txtBabyStateD37.Text = "√";
                                }
                                else if (babyInfo.BabyState == "2")
                                {
                                    this.txtBabyStateZ38.Text = "√";
                                }
                                else if (babyInfo.BabyState == "3")
                                {
                                    this.txtBabyStateC39.Text = "√";
                                }
                                //呼吸
                                if (babyInfo.Breath == "1")
                                {
                                    this.txtBreathZ310.Text = "√";
                                }
                                else if (babyInfo.Breath == "2")
                                {
                                    this.txtBreathZS311.Text = "√";
                                }
                                else if (babyInfo.Breath == "3")
                                {
                                    this.txtBreathZS312.Text = "√";
                                }
                                //抢救次数
                                this.txtSalvTimes316.Text = babyInfo.SalvNum.ToString();
                                //成功抢救次数
                                this.txtSuccTimes317.Text = babyInfo.SuccNum.ToString();
                                row++;
                                break;
                            case 4:
                                //性别
                                if (babyInfo.SexCode == "M")
                                {
                                    this.txtBabySexM41.Text = "√";
                                }
                                else
                                {
                                    this.txtBabySexF42.Text = "√";
                                }
                                //分娩结果
                                if (babyInfo.BirthEnd == "1")
                                {
                                    this.txtBirEndH43.Text = "√";
                                }
                                else if (babyInfo.BirthEnd == "2")
                                {
                                    this.txtBirEndD44.Text = "√";
                                }
                                else if (babyInfo.BirthEnd == "3")
                                {
                                    this.txtBirEndD45.Text = "√";
                                }
                                //婴儿体重
                                this.txtWeight46.Text = babyInfo.Weight.ToString();
                                //婴儿转归
                                if (babyInfo.BabyState == "1")
                                {
                                    this.txtBabyStateD47.Text = "√";
                                }
                                else if (babyInfo.BabyState == "2")
                                {
                                    this.txtBabyStateZ48.Text = "√";
                                }
                                else if (babyInfo.BabyState == "3")
                                {
                                    this.txtBabyStateC49.Text = "√";
                                }
                                //呼吸
                                if (babyInfo.Breath == "1")
                                {
                                    this.txtBreathZ410.Text = "√";
                                }
                                else if (babyInfo.Breath == "2")
                                {
                                    this.txtBreathZS411.Text = "√";
                                }
                                else if (babyInfo.Breath == "3")
                                {
                                    this.txtBreathZS412.Text = "√";
                                }
                                //抢救次数
                                this.txtSalvTimes416.Text = babyInfo.SalvNum.ToString();
                                //成功抢救次数
                                this.txtSuccTimes417.Text = babyInfo.SuccNum.ToString();
                                row++;
                                break;
                            default:
                                break;
                        }
                    }
                }
                #endregion
            }
            if (info.Ever_Difficulty == "1")
            {
                this.lblHavedTum.Text = "有无本项信息【】有【√】无";
            }
            else if (info.Ever_Difficulty == "2")
            {
                this.lblHavedTum.Text = "有无本项信息【√】有【】无";
            }
            else
            {
                this.lblHavedTum.Visible = false;
            }
            if (info.Ever_Difficulty != "1")
            {
                #region 肿瘤卡信息
                FS.HISFC.BizLogic.HealthRecord.Tumour tumourMgr = new FS.HISFC.BizLogic.HealthRecord.Tumour();
                FS.HISFC.Models.HealthRecord.Tumour tumourInfo = new FS.HISFC.Models.HealthRecord.Tumour();

                tumourInfo = tumourMgr.GetTumour(info.PatientInfo.ID);
                if (tumourInfo != null)
                {
                    //肿瘤分期类型
                    if (tumourInfo.Tumour_Type == null || tumourInfo.Tumour_Type == "")
                    {
                        this.txtTumourType.Text = "-";
                    }
                    else
                    {
                        this.txtTumourType.Text = tumourInfo.Tumour_Type;
                    }
                    //原发肿瘤T
                    if (tumourInfo.Tumour_T == null || tumourInfo.Tumour_T == "")
                    {
                        this.txtTumourT.Text = "-";
                    }
                    else
                    {
                        this.txtTumourT.Text = tumourInfo.Tumour_T;
                    }
                    //淋巴转移N
                    if (tumourInfo.Tumour_N == null || tumourInfo.Tumour_N == "")
                    {
                        this.txtTumourN.Text = "-";
                    }
                    else
                    {
                        this.txtTumourN.Text = tumourInfo.Tumour_N;
                    }
                    //远程转移M
                    if (tumourInfo.Tumour_M == null || tumourInfo.Tumour_M == "")
                    {
                        this.txtTumourM.Text = "-";
                    }
                    else
                    {
                        this.txtTumourM.Text = tumourInfo.Tumour_M;
                    }
                    //分期
                    if (tumourInfo.Tumour_Stage == null || tumourInfo.Tumour_Stage == "")
                    {
                        this.txtTumourStage.Text = "-";
                    }
                    else
                    {
                        //this.txtTumourStage.Text = tumourInfo.Tumour_Stage;
                        switch (tumourInfo.Tumour_Stage)
                        {
                            
                            case "11":
                                this.txtTumourStage.Text = "IA期";
                                break;
                            case "12":
                                this.txtTumourStage.Text = "IB期";
                                break;
                            case "13":
                                this.txtTumourStage.Text = "IC期";
                                break;
                            case "21":
                                this.txtTumourStage.Text = "IIA期";
                                break;
                            case "22":
                                this.txtTumourStage.Text = "IIB期";
                                break;
                            case "23":
                                this.txtTumourStage.Text = "IIC期";
                                break;
                            case "31":
                                this.txtTumourStage.Text = "IIIA期";
                                break;
                            case "32":
                                this.txtTumourStage.Text = "IIIB期";
                                break;
                            case "33":
                                this.txtTumourStage.Text = "IIIC期";
                                break;
                            case "41":
                                this.txtTumourStage.Text = "IVA期";
                                break;
                            case "42":
                                this.txtTumourStage.Text = "IVB期";
                                break;
                            case "43":
                                this.txtTumourStage.Text = "IVC期";
                                break;
                            default:
                                break;
                        }
                    }
                    //放疗方式
                    if (tumourInfo.Rmodeid == null || tumourInfo.Rmodeid == "")
                    {
                        this.txtRmodeid.Text = "-";
                    }
                    else
                    {
                        this.txtRmodeid.Text = tumourInfo.Rmodeid;
                    }
                    //放疗程式
                    if (tumourInfo.Rprocessid == null || tumourInfo.Rprocessid == "")
                    {
                        this.txtRprocessid.Text = "-";
                    }
                    else
                    {
                        this.txtRprocessid.Text = tumourInfo.Rprocessid;
                    }
                    //放疗装置
                    if (tumourInfo.Rdeviceid == null || tumourInfo.Rdeviceid == "")
                    {
                        this.txtRdeviceid.Text = "-";
                    }
                    else
                    {
                        this.txtRdeviceid.Text = tumourInfo.Rdeviceid;
                    }

                    if (tumourInfo.Gy1 > 0)
                    {
                        this.txtCy1.Text = tumourInfo.Gy1.ToString();	//原发灶gy剂量
                        this.txtTimes1.Text = tumourInfo.Time1.ToString(); //原发灶次数
                        this.txtDay1.Text = tumourInfo.Day1.ToString();		//原发灶天数
                        this.txtYearBegin1.Text = tumourInfo.BeginDate1.Year.ToString();//原发灶开始时间
                        this.txtMonthBegin1.Text = tumourInfo.BeginDate1.Month.ToString();//原发灶开始时间
                        this.txtDayBegin1.Text = tumourInfo.BeginDate1.Day.ToString();//原发灶结束时间
                        this.txtYearEnd1.Text = tumourInfo.EndDate1.Year.ToString();//原发灶结束时间
                        this.txtMonthEnd1.Text = tumourInfo.EndDate1.Month.ToString();//原发灶结束时间
                        this.txtDayEnd1.Text = tumourInfo.EndDate1.Day.ToString();//原发灶结束时间
                    }
                    else
                    {
                        this.txtCy1.Text = string.Empty;	//原发灶gy剂量
                        this.txtTimes1.Text = string.Empty; //原发灶次数
                        this.txtDay1.Text = string.Empty;		//原发灶天数
                        this.txtYearBegin1.Text = string.Empty;//原发灶开始时间
                        this.txtMonthBegin1.Text = string.Empty;//原发灶开始时间
                        this.txtDayBegin1.Text = string.Empty;//原发灶结束时间
                        this.txtYearEnd1.Text = string.Empty;//原发灶结束时间
                        this.txtMonthEnd1.Text = string.Empty;//原发灶结束时间
                        this.txtDayEnd1.Text = string.Empty;//原发灶结束时间
                    }
                    if (tumourInfo.Gy2 > 0)
                    {
                        this.txtCy2.Text = tumourInfo.Gy2.ToString(); //区域淋巴结gy剂量
                        this.txtTimes2.Text = tumourInfo.Time2.ToString();		//区域淋巴结次数
                        this.txtDay2.Text = tumourInfo.Day2.ToString();		//区域淋巴结天数
                        this.txtYearBegin2.Text = tumourInfo.BeginDate2.Year.ToString();//区域淋巴结开始时间
                        this.txtMonthBegin2.Text = tumourInfo.BeginDate2.Month.ToString();//区域淋巴结开始时间
                        this.txtDayBegin2.Text = tumourInfo.BeginDate2.Day.ToString();//区域淋巴结开始时间
                        this.txtYearEnd2.Text = tumourInfo.EndDate2.Year.ToString();//区域淋巴结结束时间
                        this.txtMonthEnd2.Text = tumourInfo.EndDate2.Month.ToString();//区域淋巴结结束时间
                        this.txtDayEnd2.Text = tumourInfo.EndDate2.Day.ToString();//区域淋巴结结束时间
                    }
                    else
                    {
                        this.txtCy2.Text = string.Empty; //区域淋巴结gy剂量
                        this.txtTimes2.Text = string.Empty; //区域淋巴结次数
                        this.txtDay2.Text = string.Empty; //区域淋巴结天数
                        this.txtYearBegin2.Text = string.Empty;//区域淋巴结开始时间
                        this.txtMonthBegin2.Text = string.Empty;//区域淋巴结开始时间
                        this.txtDayBegin2.Text = string.Empty;//区域淋巴结开始时间
                        this.txtYearEnd2.Text = string.Empty;//区域淋巴结结束时间
                        this.txtMonthEnd2.Text = string.Empty;//区域淋巴结结束时间
                        this.txtDayEnd2.Text = string.Empty;//区域淋巴结结束时间
                    }
                    if (tumourInfo.Gy3 > 0)
                    {
                        this.txtCy3.Text = tumourInfo.Gy3.ToString(); //转移灶gy剂量
                        this.txtTimes3.Text = tumourInfo.Time3.ToString();		//转移灶次数
                        this.txtDay3.Text = tumourInfo.Day3.ToString();		//转移灶天数
                        this.txtYearBegin3.Text = tumourInfo.BeginDate3.Year.ToString();//转移灶开始时间
                        this.txtMonthBegin3.Text = tumourInfo.BeginDate3.Month.ToString();//转移灶开始时间
                        this.txtDayBegin3.Text = tumourInfo.BeginDate3.Day.ToString();//转移灶开始时间
                        this.txtYearEnd3.Text = tumourInfo.EndDate3.Year.ToString();//转移灶结束时间
                        this.txtMonthEnd3.Text = tumourInfo.EndDate3.Month.ToString();//转移灶结束时间
                        this.txtDayEnd3.Text = tumourInfo.EndDate3.Day.ToString();//转移灶结束时间
                    }
                    else
                    {
                        this.txtCy3.Text = string.Empty;//转移灶gy剂量
                        this.txtTimes3.Text = string.Empty;//转移灶次数
                        this.txtDay3.Text = string.Empty;//转移灶天数
                        this.txtYearBegin3.Text = string.Empty;//转移灶开始时间
                        this.txtMonthBegin3.Text = string.Empty;//转移灶开始时间
                        this.txtDayBegin3.Text = string.Empty;//转移灶开始时间
                        this.txtYearEnd3.Text = string.Empty;//转移灶结束时间
                        this.txtMonthEnd3.Text = string.Empty;//转移灶结束时间
                        this.txtDayEnd3.Text = string.Empty;//转移灶结束时间
                    }
                    if (tumourInfo.Cmodeid == null || tumourInfo.Cmodeid == "")
                    {
                        this.txtCmodeid.Text = "-";	//化疗方式
                    }
                    else
                    {
                        this.txtCmodeid.Text = tumourInfo.Cmodeid;	//化疗方式
                    }
                    if (tumourInfo.Cmethod == null || tumourInfo.Cmethod == "")
                    {
                        this.txtCmethod.Text = "-";	//化疗方法
                    }
                    else
                    {
                        this.txtCmethod.Text = tumourInfo.Cmethod;	//化疗方法
                    }
                }
                ArrayList tumourList = new ArrayList();
                tumourList = tumourMgr.QueryTumourDetail(info.PatientInfo.ID);
                if (tumourList != null && tumourList.Count > 0)
                {
                    int j = 1;
                    foreach (FS.HISFC.Models.HealthRecord.TumourDetail tumourDatailInfo in tumourList)
                    {
                        switch (j)
                        {
                            case 1:

                                this.txtCR2.Visible = false;
                                this.txtPR2.Visible = false;
                                this.txtSD2.Visible = false;
                                this.txtPD2.Visible = false;
                                this.txtNA2.Visible = false;
                                this.txtCR3.Visible = false;
                                this.txtPR3.Visible = false;
                                this.txtSD3.Visible = false;
                                this.txtPD3.Visible = false;
                                this.txtNA3.Visible = false;
                                this.txtCR4.Visible = false;
                                this.txtPR4.Visible = false;
                                this.txtSD4.Visible = false;
                                this.txtPD4.Visible = false;
                                this.txtNA4.Visible = false;
                                this.txtCR5.Visible = false;
                                this.txtPR5.Visible = false;
                                this.txtSD5.Visible = false;
                                this.txtPD5.Visible = false;
                                this.txtNA5.Visible = false;

                                this.txtDate1.Text = tumourDatailInfo.CureDate.ToString();//日期
                                this.txtEndDate1.Text = tumourDatailInfo.OperInfo.OperTime.ToString();//结束日期
                                this.txtDrugName1.Text = tumourDatailInfo.DrugInfo.Name + "(" + tumourDatailInfo.Qty + this.UnitListHelper.GetName(tumourDatailInfo.Unit) + ")";//药物名称 
                                this.txtDrugTreatment1.Text = this.PeriodListHelper.GetName(tumourDatailInfo.Period);//疗程
                                switch (FS.FrameWork.Function.NConvert.ToInt32(tumourDatailInfo.Result))
                                {
                                    case 1:
                                        this.txtCR1.Text = "√";
                                        this.txtPR1.Visible = false;
                                        this.txtSD1.Visible = false;
                                        this.txtPD1.Visible = false;
                                        this.txtNA1.Visible = false;
                                        break;
                                    case 2:
                                        this.txtCR1.Visible = false;
                                        this.txtPR1.Text = "√";
                                        this.txtSD1.Visible = false;
                                        this.txtPD1.Visible = false;
                                        this.txtNA1.Visible = false;
                                        break;
                                    case 3:
                                        this.txtCR1.Visible = false;
                                        this.txtPR1.Visible = false;
                                        this.txtSD1.Text = "√";
                                        this.txtPD1.Visible = false;
                                        this.txtNA1.Visible = false;
                                        break;
                                    case 4:
                                        this.txtCR1.Visible = false;
                                        this.txtPR1.Visible = false;
                                        this.txtSD1.Visible = false;
                                        this.txtPD1.Text = "√";
                                        this.txtNA1.Visible = false;
                                        break;
                                    case 5:
                                        this.txtCR1.Visible = false;
                                        this.txtPR1.Visible = false;
                                        this.txtSD1.Visible = false;
                                        this.txtPD1.Visible = false;
                                        this.txtNA1.Text = "√";
                                        break;
                                    default:
                                        break;

                                }
                                j++;
                                break;
                            case 2:
                                this.txtCR2.Visible = true;
                                this.txtPR2.Visible = true;
                                this.txtSD2.Visible = true;
                                this.txtPD2.Visible = true;
                                this.txtNA2.Visible = true;
                                this.txtDate2.Text = tumourDatailInfo.CureDate.ToString();//日期
                                this.txtEndDate2.Text = tumourDatailInfo.OperInfo.OperTime.ToString();//结束日期
                                this.txtDrugName2.Text = tumourDatailInfo.DrugInfo.Name + "(" + tumourDatailInfo.Qty + this.UnitListHelper.GetName(tumourDatailInfo.Unit) + ")";//药物名称 
                                this.txtDrugTreatment2.Text = this.PeriodListHelper.GetName(tumourDatailInfo.Period);//疗程
                                switch (FS.FrameWork.Function.NConvert.ToInt32(tumourDatailInfo.Result))
                                {
                                    case 1:
                                        this.txtCR2.Text = "√";
                                        this.txtPR2.Visible = false;
                                        this.txtSD2.Visible = false;
                                        this.txtPD2.Visible = false;
                                        this.txtNA2.Visible = false;
                                        break;
                                    case 2:
                                        this.txtCR2.Visible = false;
                                        this.txtPR2.Text = "√";
                                        this.txtSD2.Visible = false;
                                        this.txtPD2.Visible = false;
                                        this.txtNA2.Visible = false;
                                        break;
                                    case 3:
                                        this.txtCR2.Visible = false;
                                        this.txtPR2.Visible = false;
                                        this.txtSD2.Text = "√";
                                        this.txtPD2.Visible = false;
                                        this.txtNA2.Visible = false;
                                        break;
                                    case 4:
                                        this.txtCR2.Visible = false;
                                        this.txtPR2.Visible = false;
                                        this.txtSD2.Visible = false;
                                        this.txtPD2.Text = "√";
                                        this.txtNA2.Visible = false;
                                        break;
                                    case 5:
                                        this.txtCR2.Visible = false;
                                        this.txtPR2.Visible = false;
                                        this.txtSD2.Visible = false;
                                        this.txtPD2.Visible = false;
                                        this.txtNA2.Text = "√";
                                        break;
                                    default:
                                        break;
                                }
                                j++;
                                break;
                            case 3:
                                this.txtCR3.Visible = true;
                                this.txtPR3.Visible = true;
                                this.txtSD3.Visible = true;
                                this.txtPD3.Visible = true;
                                this.txtNA3.Visible = true;
                                this.txtDate3.Text = tumourDatailInfo.CureDate.ToString();//日期
                                this.txtEndDate3.Text = tumourDatailInfo.OperInfo.OperTime.ToString();//结束日期
                                this.txtDrugName3.Text = tumourDatailInfo.DrugInfo.Name + "(" + tumourDatailInfo.Qty + this.UnitListHelper.GetName(tumourDatailInfo.Unit) + ")";//药物名称 
                                this.txtDrugTreatment3.Text = this.PeriodListHelper.GetName(tumourDatailInfo.Period);//疗程
                                switch (FS.FrameWork.Function.NConvert.ToInt32(tumourDatailInfo.Result))
                                {
                                    case 1:
                                        this.txtCR3.Text = "√";
                                        this.txtPR3.Visible = false;
                                        this.txtSD3.Visible = false;
                                        this.txtPD3.Visible = false;
                                        this.txtNA3.Visible = false;
                                        break;
                                    case 2:
                                        this.txtCR3.Visible = false;
                                        this.txtPR3.Text = "√";
                                        this.txtSD3.Visible = false;
                                        this.txtPD3.Visible = false;
                                        this.txtNA3.Visible = false;
                                        break;
                                    case 3:
                                        this.txtCR3.Visible = false;
                                        this.txtPR3.Visible = false;
                                        this.txtSD3.Text = "√";
                                        this.txtPD3.Visible = false;
                                        this.txtNA3.Visible = false;
                                        break;
                                    case 4:
                                        this.txtCR3.Visible = false;
                                        this.txtPR3.Visible = false;
                                        this.txtSD3.Visible = false;
                                        this.txtPD3.Text = "√";
                                        this.txtNA3.Visible = false;
                                        break;
                                    case 5:
                                        this.txtCR3.Visible = false;
                                        this.txtPR3.Visible = false;
                                        this.txtSD3.Visible = false;
                                        this.txtPD3.Visible = false;
                                        this.txtNA3.Text = "√";
                                        break;
                                    default:
                                        break;
                                }
                                j++;
                                break;
                            case 4:
                                this.txtCR4.Visible = true;
                                this.txtPR4.Visible = true;
                                this.txtSD4.Visible = true;
                                this.txtPD4.Visible = true;
                                this.txtNA4.Visible = true;
                                this.txtDate4.Text = tumourDatailInfo.CureDate.ToString();//日期
                                this.txtEndDate4.Text = tumourDatailInfo.OperInfo.OperTime.ToString();//结束日期
                                this.txtDrugName4.Text = tumourDatailInfo.DrugInfo.Name + "(" + tumourDatailInfo.Qty + this.UnitListHelper.GetName(tumourDatailInfo.Unit) + ")";//药物名称 
                                this.txtDrugTreatment4.Text = this.PeriodListHelper.GetName(tumourDatailInfo.Period);//疗程
                                switch (FS.FrameWork.Function.NConvert.ToInt32(tumourDatailInfo.Result))
                                {
                                    case 1:
                                        this.txtCR4.Text = "√";
                                        this.txtPR4.Visible = false;
                                        this.txtSD4.Visible = false;
                                        this.txtPD4.Visible = false;
                                        this.txtNA4.Visible = false;
                                        break;
                                    case 2:
                                        this.txtCR4.Visible = false;
                                        this.txtPR4.Text = "√";
                                        this.txtSD4.Visible = false;
                                        this.txtPD4.Visible = false;
                                        this.txtNA4.Visible = false;
                                        break;
                                    case 3:
                                        this.txtCR4.Visible = false;
                                        this.txtPR4.Visible = false;
                                        this.txtSD4.Text = "√";
                                        this.txtPD4.Visible = false;
                                        this.txtNA4.Visible = false;
                                        break;
                                    case 4:
                                        this.txtCR4.Visible = false;
                                        this.txtPR4.Visible = false;
                                        this.txtSD4.Visible = false;
                                        this.txtPD4.Text = "√";
                                        this.txtNA4.Visible = false;
                                        break;
                                    case 5:
                                        this.txtCR4.Visible = false;
                                        this.txtPR4.Visible = false;
                                        this.txtSD4.Visible = false;
                                        this.txtPD4.Visible = false;
                                        this.txtNA4.Text = "√";
                                        break;
                                    default:
                                        break;
                                }
                                j++;
                                break;
                            case 5:
                                this.txtCR5.Visible = true;
                                this.txtPR5.Visible = true;
                                this.txtSD5.Visible = true;
                                this.txtPD5.Visible = true;
                                this.txtNA5.Visible = true;
                                this.txtDate5.Text = tumourDatailInfo.CureDate.ToString();//日期
                                this.txtEndDate5.Text = tumourDatailInfo.OperInfo.OperTime.ToString();//结束日期
                                this.txtDrugName5.Text = tumourDatailInfo.DrugInfo.Name + "(" + tumourDatailInfo.Qty + this.UnitListHelper.GetName(tumourDatailInfo.Unit) + ")";//药物名称 
                                this.txtDrugTreatment5.Text = this.PeriodListHelper.GetName(tumourDatailInfo.Period);//疗程
                                switch (FS.FrameWork.Function.NConvert.ToInt32(tumourDatailInfo.Result))
                                {
                                    case 1:
                                        this.txtCR5.Text = "√";
                                        this.txtPR5.Visible = false;
                                        this.txtSD5.Visible = false;
                                        this.txtPD5.Visible = false;
                                        this.txtNA5.Visible = false;
                                        break;
                                    case 2:
                                        this.txtCR5.Visible = false;
                                        this.txtPR5.Text = "√";
                                        this.txtSD5.Visible = false;
                                        this.txtPD5.Visible = false;
                                        this.txtNA5.Visible = false;
                                        break;
                                    case 3:
                                        this.txtCR5.Visible = false;
                                        this.txtPR5.Visible = false;
                                        this.txtSD5.Text = "√";
                                        this.txtPD5.Visible = false;
                                        this.txtNA5.Visible = false;
                                        break;
                                    case 4:
                                        this.txtCR5.Visible = false;
                                        this.txtPR5.Visible = false;
                                        this.txtSD5.Visible = false;
                                        this.txtPD5.Text = "√";
                                        this.txtNA5.Visible = false;
                                        break;
                                    case 5:
                                        this.txtCR5.Visible = false;
                                        this.txtPR5.Visible = false;
                                        this.txtSD5.Visible = false;
                                        this.txtPD5.Visible = false;
                                        this.txtNA5.Text = "√";
                                        break;
                                    default:
                                        break;
                                }
                                j++;
                                break;
                            default:
                                break;
                        }
                        if (j > 5)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    this.txtCR1.Visible = false;
                    this.txtPR1.Visible = false;
                    this.txtSD1.Visible = false;
                    this.txtPD1.Visible = false;
                    this.txtNA1.Visible = false;
                    this.txtCR2.Visible = false;
                    this.txtPR2.Visible = false;
                    this.txtSD2.Visible = false;
                    this.txtPD2.Visible = false;
                    this.txtNA2.Visible = false;
                    this.txtCR3.Visible = false;
                    this.txtPR3.Visible = false;
                    this.txtSD3.Visible = false;
                    this.txtPD3.Visible = false;
                    this.txtNA3.Visible = false;
                    this.txtCR4.Visible = false;
                    this.txtPR4.Visible = false;
                    this.txtSD4.Visible = false;
                    this.txtPD4.Visible = false;
                    this.txtNA4.Visible = false;
                    this.txtCR5.Visible = false;
                    this.txtPR5.Visible = false;
                    this.txtSD5.Visible = false;
                    this.txtPD5.Visible = false;
                    this.txtNA5.Visible = false;
                }
                #endregion
            }

        }
        /// <summary>
        /// 
        /// </summary>
        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack.Reset()
        {
            #region 手术信息
            this.txtOpCode1.Text = "";
            this.txtOpCode2.Text = "";
            this.txtOpCode3.Text = "";
            this.txtOpCode4.Text = "";
            this.txtOpCode5.Text = "";
            this.txtOpCode6.Text = "";
            //this.txtOpCode7.Text = "";
            //this.txtOpCode8.Text = "";
            this.txtOpDate1.Text = "";
            this.txtOpDate2.Text = "";
            this.txtOpDate3.Text = "";
            this.txtOpDate4.Text = "";
            this.txtOpDate5.Text = "";
            this.txtOpDate6.Text = "";
            //this.txtOpDate7.Text = "";
            //this.txtOpDate8.Text = "";
            this.txtOpLevel1.Text = "";
            this.txtOpLevel2.Text = "";
            this.txtOpLevel3.Text = "";
            this.txtOpLevel4.Text = "";
            this.txtOpLevel5.Text = "";
            this.txtOpLevel6.Text = "";
            //this.txtOpLevel7.Text = "";
            //this.txtOpLevel8.Text = "";
            this.txtOpName1.Text = "";
            this.txtOpName2.Text = "";
            this.txtOpName3.Text = "";
            this.txtOpName4.Text = "";
            this.txtOpName5.Text = "";
            this.txtOpName6.Text = "";
            //this.txtOpName7.Text = "";
            //this.txtOpName8.Text = "";
            this.txtOpDoc1.Text = "";
            this.txtOpDoc2.Text = "";
            this.txtOpDoc3.Text = "";
            this.txtOpDoc4.Text = "";
            this.txtOpDoc5.Text = "";
            this.txtOpDoc6.Text = "";
            //this.txtOpDoc7.Text = "";
            //this.txtOpDoc8.Text = "";
            this.txtOpFirDoc1.Text = "";
            this.txtOpFirDoc2.Text = "";
            this.txtOpFirDoc3.Text = "";
            this.txtOpFirDoc4.Text = "";
            this.txtOpFirDoc5.Text = "";
            this.txtOpFirDoc6.Text = "";
            //this.txtOpFirDoc7.Text = "";
            //this.txtOpFirDoc8.Text = "";
            this.txtOpSecDoc1.Text = "";
            this.txtOpSecDoc2.Text = "";
            this.txtOpSecDoc3.Text = "";
            this.txtOpSecDoc4.Text = "";
            this.txtOpSecDoc5.Text = "";
            this.txtOpSecDoc6.Text = "";
            //this.txtOpSecDoc7.Text = "";
            //this.txtOpSecDoc8.Text = "";
            this.txtOpNickType1.Text = "";
            this.txtOpNickType2.Text = "";
            this.txtOpNickType3.Text = "";
            this.txtOpNickType4.Text = "";
            this.txtOpNickType5.Text = "";
            this.txtOpNickType6.Text = "";
            //this.txtOpNickType7.Text = "";
            //this.txtOpNickType8.Text = "";
            this.txtOpCicaType1.Text = "";
            this.txtOpCicaType2.Text = "";
            this.txtOpCicaType3.Text = "";
            this.txtOpCicaType4.Text = "";
            this.txtOpCicaType5.Text = "";
            this.txtOpCicaType6.Text = "";
            //this.txtOpCicaType7.Text = "";
            //this.txtOpCicaType8.Text = "";
            this.txtOpAnesType1.Text = "";
            this.txtOpAnesType11.Text = "";
            this.txtOpAnesType2.Text = "";
            this.txtOpAnesType21.Text = "";
            this.txtOpAnesType3.Text = "";
            this.txtOpAnesType31.Text = "";
            this.txtOpAnesType4.Text = "";
            this.txtOpAnesType41.Text = "";
            this.txtOpAnesType5.Text = "";
            this.txtOpAnesType51.Text = "";
            this.txtOpAnesType6.Text = "";
            this.txtOpAnesType61.Text = "";
            //this.txtOpAnesType7.Text = "";
            //this.txtOpAnesType71.Text = "";
            //this.txtOpAnesType8.Text = "";
            //this.txtOpAnesType81.Text = "";
            this.txtOpAnesDoc1.Text = "";
            this.txtOpAnesDoc2.Text = "";
            this.txtOpAnesDoc3.Text = "";
            this.txtOpAnesDoc4.Text = "";
            this.txtOpAnesDoc5.Text = "";
            this.txtOpAnesDoc6.Text = "";
            //this.txtOpAnesDoc7.Text = "";
            //this.txtOpAnesDoc8.Text = "";
            this.txtSelectOpDate1.Text = "";
            this.txtSelectOpDate2.Text = "";
            this.txtSelectOpDate3.Text = "";
            this.txtSelectOpDate4.Text = "";
            this.txtSelectOpDate5.Text = "";
            this.txtSelectOpDate6.Text = "";
            //this.txtSelectOpDate7.Text = "";
            //this.txtSelectOpDate8.Text = "";
            #endregion
            #region 其他
            this.txtLeaveHopitalType.Text = "";
            this.txtHighReceiveHopital.Text = "";
            this.txtLowerReceiveHopital.Text = "";
            this.txtComeBackInMonth.Text = "";
            this.txtComeBackPurpose.Text = "";
            this.txtOutComeDay.Text = "";
            this.txtOutComeHour.Text = "";
            this.txtOutComeMin.Text = "";
            this.txtInComeDay.Text = "";
            this.txtInComeHour.Text = "";
            this.txtInComeMin.Text = "";

            this.FeeTotCost.Text = "0";
            this.FeeOwnCost.Text = "0";
            //this.txtFee1.Text = "0";
            //this.txtFee2.Text = "0";
            //this.txtFee3.Text = "0";
            //this.txtFee4.Text = "0";
            //this.txtFee5.Text = "0";
            //this.txtFee6.Text = "0";
            //this.txtFee7.Text = "0";
            //this.txtFee8.Text = "0";
            //this.txtFee9.Text = "0";
            //this.txtFee91.Text = "0";
            //this.txtFee10.Text = "0";
            //this.txtFee101.Text = "0";
            //this.txtFee102.Text = "0";
            //this.txtFee11.Text = "0";
            //this.txtFee12.Text = "0";
            //this.txtFee13.Text = "0";
            //this.txtFee131.Text = "0";
            //this.txtFee14.Text = "0";
            //this.txtFee15.Text = "0";
            //this.txtFee16.Text = "0";
            //this.txtFee17.Text = "0";
            //this.txtFee18.Text = "0";
            //this.txtFee19.Text = "0";
            //this.txtFee20.Text = "0";
            //this.txtFee21.Text = "0";
            //this.txtFee22.Text = "0";
            //this.txtFee23.Text = "0";
            //this.txtFee24.Text = "0";
            #endregion
            #region 妇婴卡信息
            this.txtBabySexM11.Text = "";
            this.txtBabySexM21.Text = "";
            this.txtBabySexM31.Text = "";
            this.txtBabySexM41.Text = "";
            this.txtBabySexF12.Text = "";
            this.txtBabySexF22.Text = "";
            this.txtBabySexF32.Text = "";
            this.txtBabySexF42.Text = "";
            this.txtBirEndH13.Text = "";
            this.txtBirEndH23.Text = "";
            this.txtBirEndH33.Text = "";
            this.txtBirEndH43.Text = "";
            this.txtBirEndD14.Text = "";
            this.txtBirEndD24.Text = "";
            this.txtBirEndD34.Text = "";
            this.txtBirEndD44.Text = "";
            this.txtBirEndD15.Text = "";
            this.txtBirEndD25.Text = "";
            this.txtBirEndD35.Text = "";
            this.txtBirEndD45.Text = "";
            this.txtWeight16.Text = "";
            this.txtWeight26.Text = "";
            this.txtWeight36.Text = "";
            this.txtWeight46.Text = "";
            this.txtBabyStateD17.Text = "";
            this.txtBabyStateD27.Text = "";
            this.txtBabyStateD37.Text = "";
            this.txtBabyStateD47.Text = "";
            this.txtBabyStateZ18.Text = "";
            this.txtBabyStateZ28.Text = "";
            this.txtBabyStateZ38.Text = "";
            this.txtBabyStateZ48.Text = "";
            this.txtBabyStateC19.Text = "";
            this.txtBabyStateC29.Text = "";
            this.txtBabyStateC39.Text = "";
            this.txtBabyStateC49.Text = "";
            this.txtBreathZ110.Text = "";
            this.txtBreathZ210.Text = "";
            this.txtBreathZ310.Text = "";
            this.txtBreathZ410.Text = "";
            this.txtBreathZS111.Text = "";
            this.txtBreathZS211.Text = "";
            this.txtBreathZS311.Text = "";
            this.txtBreathZS411.Text = "";
            this.txtBreathZS112.Text = "";
            this.txtBreathZS212.Text = "";
            this.txtBreathZS312.Text = "";
            this.txtBreathZS412.Text = "";
            this.txtSalvTimes116.Text = "";
            this.txtSalvTimes216.Text = "";
            this.txtSalvTimes316.Text = "";
            this.txtSalvTimes416.Text = "";
            this.txtSuccTimes117.Text = "";
            this.txtSuccTimes217.Text = "";
            this.txtSuccTimes317.Text = "";
            this.txtSuccTimes417.Text = "";
            #endregion

            #region 肿瘤卡信息
            this.txtTumourType.Text = "";
            this.txtTumourT.Text = "";
            this.txtTumourN.Text = "";
            this.txtTumourM.Text = "";
            this.txtTumourStage.Text = "";
            this.txtRmodeid.Text = "";
            this.txtRprocessid.Text = "";
            this.txtRdeviceid.Text = "";
            this.txtCy1.Text = "";
            this.txtTimes1.Text = "";
            this.txtDay1.Text = "";
            this.txtYearBegin1.Text = "";
            this.txtMonthBegin1.Text = "";
            this.txtDayBegin1.Text = "";
            this.txtYearEnd1.Text = "";
            this.txtMonthEnd1.Text = "";
            this.txtDayEnd1.Text = "";
            this.txtCy2.Text = "";
            this.txtTimes2.Text = "";
            this.txtDay2.Text = "";
            this.txtYearBegin2.Text = "";
            this.txtMonthBegin2.Text = "";
            this.txtDayBegin2.Text = "";
            this.txtYearEnd2.Text = "";
            this.txtMonthEnd2.Text = "";
            this.txtDayEnd2.Text = "";
            this.txtPosition.Text = "";
            this.txtCy3.Text = "";
            this.txtTimes3.Text = "";
            this.txtDay3.Text = "";
            this.txtYearBegin3.Text = "";
            this.txtMonthBegin3.Text = "";
            this.txtDayBegin3.Text = "";
            this.txtYearEnd3.Text = "";
            this.txtMonthEnd3.Text = "";
            this.txtDayEnd3.Text = "";
          

            this.txtCmodeid.Text = "";
            this.txtCmethod.Text = "";
            this.txtDate1.Text = "";
            this.txtEndDate1.Text = "";
            this.txtDrugName1.Text = "";
            this.txtDrugTreatment1.Text = "";
            this.txtCR1.Text = "";
            this.txtPR1.Text = "";
            this.txtSD1.Text = "";
            this.txtPD1.Text = "";
            this.txtNA1.Text = "";
            this.txtDate2.Text = "";
            this.txtEndDate2.Text = "";
            this.txtDrugName2.Text = "";
            this.txtDrugTreatment2.Text = "";
            this.txtCR2.Text = "";
            this.txtPR2.Text = "";
            this.txtSD2.Text = "";
            this.txtPD2.Text = "";
            this.txtNA2.Text = "";
            this.txtDate3.Text = "";
            this.txtEndDate3.Text = "";
            this.txtDrugName3.Text = "";
            this.txtDrugTreatment3.Text = "";
            this.txtCR3.Text = "";
            this.txtPR3.Text = "";
            this.txtSD3.Text = "";
            this.txtPD3.Text = "";
            this.txtNA3.Text = "";
            this.txtDate4.Text = "";
            this.txtEndDate4.Text = "";
            this.txtDrugName4.Text = "";
            this.txtDrugTreatment4.Text = "";
            this.txtCR4.Text = "";
            this.txtPR4.Text = "";
            this.txtSD4.Text = "";
            this.txtPD4.Text = "";
            this.txtNA4.Text = "";
            this.txtDate5.Text = "";
            this.txtEndDate5.Text = "";
            this.txtDrugName5.Text = "";
            this.txtDrugTreatment5.Text = "";
            this.txtCR5.Text = "";
            this.txtPR5.Text = "";
            this.txtSD5.Text = "";
            this.txtPD5.Text = "";
            this.txtNA5.Text = "";
            #endregion
        }
        #endregion

        #region IReportPrinter 成员


        int FS.FrameWork.WinForms.Forms.IReportPrinter.Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int FS.FrameWork.WinForms.Forms.IReportPrinter.Print()
        {
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.PrintPage(0, 0, this);
            return 1;
        }
        private void PreviewZoom()
        {
            FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();

            //是否需要设置预览效果100%
            ArrayList priViewZoom = Constant.GetList("CASEPRINTVIEWZOOM");
            if (priViewZoom != null && priViewZoom.Count > 0)
            {
                this.isPrintViewZoom = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int FS.FrameWork.WinForms.Forms.IReportPrinter.PrintPreview()
        {
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            this.PreviewZoom();
            if (this.isPrintViewZoom)
            {
                p.PreviewZoomFactor = 1;
            }
            return p.PrintPreview(0, 0, this);
        }

        #endregion
    }
}
