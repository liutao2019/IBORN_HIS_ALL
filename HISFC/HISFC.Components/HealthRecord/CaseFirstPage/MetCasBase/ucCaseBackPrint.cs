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
    public partial class ucCaseBackPrint : UserControl, FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack
    {
        /// <summary>
        /// 病案首页第二页
        /// </summary>
        public ucCaseBackPrint()
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
                this.txtOpNickType7.AddItems(NickTypeList);
                this.txtOpNickType8.AddItems(NickTypeList);
                //愈合列表
                ArrayList CicaTypelist = Constant.GetAllList("CICATYPE");
                CicaTypelist.Add(obj);
                this.txtOpCicaType1.AddItems(CicaTypelist);
                this.txtOpCicaType2.AddItems(CicaTypelist);
                this.txtOpCicaType3.AddItems(CicaTypelist);
                this.txtOpCicaType4.AddItems(CicaTypelist);
                this.txtOpCicaType5.AddItems(CicaTypelist);
                this.txtOpCicaType6.AddItems(CicaTypelist);
                this.txtOpCicaType7.AddItems(CicaTypelist);
                this.txtOpCicaType8.AddItems(CicaTypelist);
                //查询麻醉方式列表
                ArrayList NarcList = Constant.GetList("CASEANESTYPE");
                this.txtOpAnesType1.AddItems(NarcList);
                this.txtOpAnesType2.AddItems(NarcList);
                this.txtOpAnesType3.AddItems(NarcList);
                this.txtOpAnesType4.AddItems(NarcList);
                this.txtOpAnesType5.AddItems(NarcList);
                this.txtOpAnesType6.AddItems(NarcList);
                this.txtOpAnesType7.AddItems(NarcList);
                this.txtOpAnesType8.AddItems(NarcList);
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
        #endregion


        #region HealthRecordInterface 成员

        /// <summary>
        /// 设置病案首页第二页值
        /// </summary>
        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack.ControlValue(FS.HISFC.Models.HealthRecord.Base info)
        {
            this.LoadInfo();
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
                            this.txtSelectOpDate1.Text =this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
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
                            this.txtOpDoc2.Text =  operationInfo.FirDoctInfo.Name;
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
                        case 7:
                            #region
                            //编码
                            if (isNotPrintOpsCode)
                            {
                                this.txtOpCode7.Text = "";
                            }
                            else
                            {
                                if (operationInfo.OperationInfo.ID == "MS999")
                                {
                                    this.txtOpCode7.Text = "";
                                }
                                else
                                {
                                    this.txtOpCode7.Text = operationInfo.OperationInfo.ID;
                                }
                            }
                            //日期
                            this.txtOpDate7.Text = operationInfo.OperationDate.ToShortDateString();
                            //手术级别
                            this.txtOpLevel7.Text = this.DefaultString(operationInfo.FourDoctInfo.Name);
                            //手术及操作名称
                            this.txtOpName7.Text = this.OpName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(operationInfo.OperationInfo.Name, false), 19);
                            //术者
                            this.txtOpDoc7.Text = operationInfo.FirDoctInfo.Name;
                            //一助
                            this.txtOpFirDoc7.Text = this.DefaultString(operationInfo.SecDoctInfo.Name);
                            //二助
                            this.txtOpSecDoc7.Text = this.DefaultString(operationInfo.ThrDoctInfo.Name);
                            //手术切口
                            this.txtOpNickType7.Tag = this.DefaultString(operationInfo.NickKind);
                            //愈合情况
                            this.txtOpCicaType7.Tag = this.DefaultString(operationInfo.CicaKind);
                            //择期手术
                            this.txtSelectOpDate7.Text = this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
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
                                this.txtOpAnesType7.Text = opName.Substring(0, 5);
                                this.txtOpAnesType71.Text = opName.Substring(5);
                            }
                            else
                            {
                                this.txtOpAnesType7.Text = this.DefaultString(opName);
                            }
                            //麻醉医师
                            this.txtOpAnesDoc7.Text = this.DefaultString(operationInfo.NarcDoctInfo.Name);
                            rowNumber++;
                            break;
                            #endregion
                        case 8:
                            #region
                            //编码
                            if (isNotPrintOpsCode)
                            {
                                this.txtOpCode8.Text = "";
                            }
                            else
                            {
                                if (operationInfo.OperationInfo.ID == "MS999")
                                {
                                    this.txtOpCode8.Text = "";
                                }
                                else
                                {
                                    this.txtOpCode8.Text = operationInfo.OperationInfo.ID;
                                }
                            }
                            //日期
                            this.txtOpDate8.Text = operationInfo.OperationDate.ToShortDateString();
                            //手术级别
                            this.txtOpLevel8.Text = this.DefaultString(operationInfo.FourDoctInfo.Name);
                            //手术及操作名称
                            this.txtOpName8.Text = this.OpName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(operationInfo.OperationInfo.Name, false), 19);
                            //术者
                            this.txtOpDoc8.Text = operationInfo.FirDoctInfo.Name;
                            //一助
                            this.txtOpFirDoc8.Text = this.DefaultString(operationInfo.SecDoctInfo.Name);
                            //二助
                            this.txtOpSecDoc8.Text = this.DefaultString(operationInfo.ThrDoctInfo.Name);
                            //手术切口
                            this.txtOpNickType8.Tag = this.DefaultString(operationInfo.NickKind);
                            //愈合情况
                            this.txtOpCicaType8.Tag = this.DefaultString(operationInfo.CicaKind);
                            //择期手术
                            this.txtSelectOpDate8.Text = this.DefaultString(selectOpDateHelper.GetName(operationInfo.OperationKind));
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
                                this.txtOpAnesType8.Text = opName.Substring(0, 5);
                                this.txtOpAnesType81.Text = opName.Substring(5);
                            }
                            else
                            {
                                this.txtOpAnesType8.Text = this.DefaultString(opName);
                            }
                            //麻醉医师
                            this.txtOpAnesDoc8.Text = this.DefaultString(operationInfo.NarcDoctInfo.Name);
                            rowNumber++;
                            break;
                            #endregion
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
            if (info.OutComeDay == 0)
            {
                this.txtOutComeDay.Text = "-";
            }
            else
            {
                this.txtOutComeDay.Text = info.OutComeDay.ToString();
            }
            if (info.OutComeHour == 0)
            {
                this.txtOutComeHour.Text = "-";
            }
            else
            {
                this.txtOutComeHour.Text = info.OutComeHour.ToString();
            }
            if (info.OutComeMin == 0)
            {
                this.txtOutComeMin.Text = "-";
            }
            else
            {
                this.txtOutComeMin.Text = info.OutComeMin.ToString();
            }
            if (info.InComeDay == 0)
            {
                this.txtInComeDay.Text = "-";
            }
            else
            {
                this.txtInComeDay.Text = info.InComeDay.ToString();
            }
            if (info.InComeHour == 0)
            {
                this.txtInComeHour.Text = "-";
            }
            else
            {
                this.txtInComeHour.Text = info.InComeHour.ToString();
            }
            if (info.InComeMin == 0)
            {
                this.txtInComeMin.Text = "-";
            }
            else
            {
                this.txtInComeMin.Text = info.InComeMin.ToString();
            }

            #region 费用信息
            DataSet ds = new DataSet();
            this.feeManager.QueryFeeForDrgsByInpatientNO(info.PatientInfo.ID, ref ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.FeeTotCost.Text = ds.Tables[0].Rows[0][0].ToString();//总费用


                this.FeeOwnCost.Text = ds.Tables[0].Rows[0][1].ToString();//自负金额
                this.txtFee1.Text = ds.Tables[0].Rows[0][2].ToString();//一般医疗服务费
                this.txtFee2.Text = ds.Tables[0].Rows[0][3].ToString();//一般治疗操作费
                this.txtFee3.Text = ds.Tables[0].Rows[0][4].ToString();//护理费


                this.txtFee4.Text = ds.Tables[0].Rows[0][5].ToString();//其他费用
                this.txtFee5.Text = ds.Tables[0].Rows[0][6].ToString();//病理诊断费


                this.txtFee6.Text = ds.Tables[0].Rows[0][7].ToString();//实验室诊断费
                this.txtFee7.Text = ds.Tables[0].Rows[0][8].ToString();//影像诊断费


                this.txtFee8.Text = ds.Tables[0].Rows[0][9].ToString();//临床诊断费


                this.txtFee9.Text = ds.Tables[0].Rows[0][10].ToString();//非手术治疗项目费
                this.txtFee91.Text = ds.Tables[0].Rows[0][11].ToString();//临床物理治疗费


                this.txtFee10.Text = ds.Tables[0].Rows[0][12].ToString();//手术治疗费


                this.txtFee101.Text = ds.Tables[0].Rows[0][13].ToString();//麻醉费


                this.txtFee102.Text = ds.Tables[0].Rows[0][14].ToString();//手术费


                this.txtFee11.Text = ds.Tables[0].Rows[0][15].ToString();//康复费


                this.txtFee12.Text = ds.Tables[0].Rows[0][16].ToString();//中医治疗
                this.txtFee13.Text = ds.Tables[0].Rows[0][17].ToString();//西药费


                this.txtFee131.Text = ds.Tables[0].Rows[0][18].ToString();//抗菌药物费用
                this.txtFee14.Text = ds.Tables[0].Rows[0][19].ToString();//中成药费
                this.txtFee15.Text = ds.Tables[0].Rows[0][20].ToString();//中草药费
                this.txtFee16.Text = ds.Tables[0].Rows[0][21].ToString();//血费


                this.txtFee17.Text = ds.Tables[0].Rows[0][22].ToString();//白蛋白类制品费


                this.txtFee18.Text = ds.Tables[0].Rows[0][23].ToString();//球蛋白类制品费


                this.txtFee19.Text = ds.Tables[0].Rows[0][24].ToString();//凝血因子类制品费
                this.txtFee20.Text = ds.Tables[0].Rows[0][25].ToString();//细胞因子类制品费
                this.txtFee21.Text = ds.Tables[0].Rows[0][26].ToString();//检查用一次性医用材料费
                this.txtFee22.Text = ds.Tables[0].Rows[0][27].ToString();//治疗用一次性医用材料费
                this.txtFee23.Text = ds.Tables[0].Rows[0][28].ToString();//手术用一次性性医用材料费
                this.txtFee24.Text = ds.Tables[0].Rows[0][29].ToString();//其他费


            }
            #endregion

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
            this.txtOpCode7.Text = "";
            this.txtOpCode8.Text = "";
            this.txtOpDate1.Text = "";
            this.txtOpDate2.Text = "";
            this.txtOpDate3.Text = "";
            this.txtOpDate4.Text = "";
            this.txtOpDate5.Text = "";
            this.txtOpDate6.Text = "";
            this.txtOpDate7.Text = "";
            this.txtOpDate8.Text = "";
            this.txtOpLevel1.Text = "";
            this.txtOpLevel2.Text = "";
            this.txtOpLevel3.Text = "";
            this.txtOpLevel4.Text = "";
            this.txtOpLevel5.Text = "";
            this.txtOpLevel6.Text = "";
            this.txtOpLevel7.Text = "";
            this.txtOpLevel8.Text = "";
            this.txtOpName1.Text = "";
            this.txtOpName2.Text = "";
            this.txtOpName3.Text = "";
            this.txtOpName4.Text = "";
            this.txtOpName5.Text = "";
            this.txtOpName6.Text = "";
            this.txtOpName7.Text = "";
            this.txtOpName8.Text = "";
            this.txtOpDoc1.Text = "";
            this.txtOpDoc2.Text = "";
            this.txtOpDoc3.Text = "";
            this.txtOpDoc4.Text = "";
            this.txtOpDoc5.Text = "";
            this.txtOpDoc6.Text = "";
            this.txtOpDoc7.Text = "";
            this.txtOpDoc8.Text = "";
            this.txtOpFirDoc1.Text = "";
            this.txtOpFirDoc2.Text = "";
            this.txtOpFirDoc3.Text = "";
            this.txtOpFirDoc4.Text = "";
            this.txtOpFirDoc5.Text = "";
            this.txtOpFirDoc6.Text = "";
            this.txtOpFirDoc7.Text = "";
            this.txtOpFirDoc8.Text = "";
            this.txtOpSecDoc1.Text = "";
            this.txtOpSecDoc2.Text = "";
            this.txtOpSecDoc3.Text = "";
            this.txtOpSecDoc4.Text = "";
            this.txtOpSecDoc5.Text = "";
            this.txtOpSecDoc6.Text = "";
            this.txtOpSecDoc7.Text = "";
            this.txtOpSecDoc8.Text = "";
            this.txtOpNickType1.Text = "";
            this.txtOpNickType2.Text = "";
            this.txtOpNickType3.Text = "";
            this.txtOpNickType4.Text = "";
            this.txtOpNickType5.Text = "";
            this.txtOpNickType6.Text = "";
            this.txtOpNickType7.Text = "";
            this.txtOpNickType8.Text = "";
            this.txtOpCicaType1.Text = "";
            this.txtOpCicaType2.Text = "";
            this.txtOpCicaType3.Text = "";
            this.txtOpCicaType4.Text = "";
            this.txtOpCicaType5.Text = "";
            this.txtOpCicaType6.Text = "";
            this.txtOpCicaType7.Text = "";
            this.txtOpCicaType8.Text = "";
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
            this.txtOpAnesType7.Text = "";
            this.txtOpAnesType71.Text = "";
            this.txtOpAnesType8.Text = "";
            this.txtOpAnesType81.Text = "";
            this.txtOpAnesDoc1.Text = "";
            this.txtOpAnesDoc2.Text = "";
            this.txtOpAnesDoc3.Text = "";
            this.txtOpAnesDoc4.Text = "";
            this.txtOpAnesDoc5.Text = "";
            this.txtOpAnesDoc6.Text = "";
            this.txtOpAnesDoc7.Text = "";
            this.txtOpAnesDoc8.Text = "";
            this.txtSelectOpDate1.Text = "";
            this.txtSelectOpDate2.Text = "";
            this.txtSelectOpDate3.Text = "";
            this.txtSelectOpDate4.Text = "";
            this.txtSelectOpDate5.Text = "";
            this.txtSelectOpDate6.Text = "";
            this.txtSelectOpDate7.Text = "";
            this.txtSelectOpDate8.Text = "";

            #endregion

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
            this.txtFee1.Text = "0";
            this.txtFee2.Text = "0";
            this.txtFee3.Text = "0";
            this.txtFee4.Text = "0";
            this.txtFee5.Text = "0";
            this.txtFee6.Text = "0";
            this.txtFee7.Text = "0";
            this.txtFee8.Text = "0";
            this.txtFee9.Text = "0";
            this.txtFee91.Text = "0";
            this.txtFee10.Text = "0";
            this.txtFee101.Text = "0";
            this.txtFee102.Text = "0";
            this.txtFee11.Text = "0";
            this.txtFee12.Text = "0";
            this.txtFee13.Text = "0";
            this.txtFee131.Text = "0";
            this.txtFee14.Text = "0";
            this.txtFee15.Text = "0";
            this.txtFee16.Text = "0";
            this.txtFee17.Text = "0";
            this.txtFee18.Text = "0";
            this.txtFee19.Text = "0";
            this.txtFee20.Text = "0";
            this.txtFee21.Text = "0";
            this.txtFee22.Text = "0";
            this.txtFee23.Text = "0";
            this.txtFee24.Text = "0";
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
