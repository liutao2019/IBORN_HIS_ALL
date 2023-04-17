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
    /// <summary>
    /// 新版首页第一页打印界面
    /// 2012-2-3
    /// </summary>
    public partial class ucCaseFirstPrint : UserControl,FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface
    {
        /// <summary>
        /// 
        /// </summary>
        public ucCaseFirstPrint()
        {
            InitializeComponent();
        }

        private FS.HISFC.BizLogic.Manager.Department inpatientManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// 常数业务类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 设置预览比例 
        /// 新的FrameWork才有改属性
        /// </summary>
        private bool isPrintViewZoom = false;
        #region HealthRecordInterface 成员

        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface.ControlValue(FS.HISFC.Models.HealthRecord.Base info)
        {
            try
            {
                //初始化下拉列表
                this.initList();
                ArrayList diagCodePrint = Constant.GetList("CASENOTPRINTICD");
                bool isNotPrintIcdCode = false;
                if (diagCodePrint != null && diagCodePrint.Count > 0)
                {
                    isNotPrintIcdCode = true;
                }
                FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
                //医疗机构名称//医疗机构编码
                ArrayList hosList = Constant.GetList("CASEHOSPITAL");
                if (hosList != null && hosList.Count == 1)//假设没有分院情况
                {
                    foreach (FS.FrameWork.Models.NeuObject obj in hosList)
                    {
                        this.txtHopitalName.Text = obj.Name;
                        this.txtHospitalCode.Text = obj.Memo;
                    }
                }
                else
                {
                    this.txtHopitalName.Text = "-";
                    this.txtHospitalCode.Text = "-";
                }
                //医疗付费方式
                this.txtPactKind.Text = info.PatientInfo.Pact.ID;
                //健康卡号
                this.txtHealthyCard.Text = info.PatientInfo.SSN;
                //住院次数
                this.txtInTimes.Text = info.PatientInfo.InTimes.ToString();
                //病案号
                this.txtCaseNum.Text = info.PatientInfo.PID.PatientNO;
                //患者姓名
                this.txtPatientName.Text = info.PatientInfo.Name;
                //性别
                if (info.PatientInfo.Sex.ID != null)
                {
                    switch (info.PatientInfo.Sex.ID.ToString())
                    {
                        case "M":
                            this.txtPatientSex.Text = "1";
                            break;
                        case "F":
                            this.txtPatientSex.Text = "2";
                            break;
                        case "1":
                            this.txtPatientSex.Text = "1";
                            break;
                        case "2":
                            this.txtPatientSex.Text = "2";
                            break;
                        default:
                            this.txtPatientSex.Text = "-";
                            break;
                    }
                }
                else
                {
                    this.txtPatientSex.Text = "-";
                }
                //出生日期
                this.txtPatientBirthdayYear.Text = info.PatientInfo.Birthday.Year.ToString();
                this.txtPatientBirthdayMonth.Text = info.PatientInfo.Birthday.Month.ToString();
                this.txtPatientBirthdayDay.Text = info.PatientInfo.Birthday.Day.ToString();
                #region //年龄
                if (info.PatientInfo.Age == "0" || info.PatientInfo.Age == "-")
                {
                    if (info.AgeUnit.IndexOf("岁") > 0 && info.AgeUnit.IndexOf("月") < 0) //整岁
                    {
                        this.txtPatientAge.Text = "Y" + info.AgeUnit.Replace("岁", "");
                    }
                    else if (info.AgeUnit.IndexOf("岁") < 0 && info.AgeUnit.IndexOf("月") > 0 && info.AgeUnit.IndexOf("天") < 0)//整月
                    {
                        this.txtPatientAge.Text = "M" + info.AgeUnit.Replace("月", "");
                    }
                    else if (info.AgeUnit.IndexOf("岁") < 0 && info.AgeUnit.IndexOf("月") < 0 && this.txtPatientAge.Text.IndexOf("周") < 0 && info.AgeUnit.IndexOf("天") > 0)//整天
                    {
                        this.txtPatientAge.Text = "D" + info.AgeUnit.Replace("天", "");
                    }
                    else if (info.AgeUnit.IndexOf("岁") > 0 && info.AgeUnit.IndexOf("月") > 0 && info.AgeUnit.IndexOf("天") < 0)//N岁N月
                    {
                        string[] PAge = info.AgeUnit.Split('岁');
                        this.txtPatientAge.Text = "Y" + PAge[0] + "M" + PAge[1].Replace("岁", "").Replace("月", "");
                    }
                    else if (info.AgeUnit.IndexOf("岁") < 0 && info.AgeUnit.IndexOf("月") > 0 && info.AgeUnit.IndexOf("天") > 0)//N月N天
                    {
                        string[] PAge = info.AgeUnit.Split('月');
                        this.txtPatientAge.Text = "M" + PAge[0] + "D" + PAge[1].Replace("月", "").Replace("天", "");
                    }
                    else if (info.AgeUnit.IndexOf("岁") > 0 && info.AgeUnit.IndexOf("月") > 0 && info.AgeUnit.IndexOf("天") > 0)//N岁N月N天
                    {
                        string[] PAge = info.AgeUnit.Split('岁');

                        string[] PAge1 = PAge[1].Split('月');
                        this.txtPatientAge.Text = "Y" + PAge[0] + "M" + PAge1[0] + "D" + PAge1[1].Replace("月", "").Replace("天", "");
                    }
                    else if (this.txtPatientAge.Text.IndexOf("岁") < 0 && this.txtPatientAge.Text.IndexOf("月") < 0 && this.txtPatientAge.Text.IndexOf("周") > 0 && this.txtPatientAge.Text.IndexOf("天") > 0)//N周N天
                    {
                        string[] PAge = this.txtPatientAge.Text.Split('周');
                        this.txtPatientAge.Text = "W" + PAge[0] + "D" + PAge[1].Replace("周", "").Replace("天", "");
                    }
                    else
                    {
                        this.txtPatientAge.Text = info.AgeUnit;//年龄 
                    }
                }
                else
                {
                    this.txtPatientAge.Text = info.PatientInfo.Age + "" + info.AgeUnit;//年龄 
                }
                #endregion
                //国籍
                this.txtCountry.Tag = info.PatientInfo.Country.ID;
                //新生儿出生体重
                this.txtBabyBirthWeight.Text = info.BabyBirthWeight;
                //新生儿入院体重
                this.txtBabyInWeight.Text = info.BabyInWeight;
                //出生地
                this.txtBirthAddr.Text = info.PatientInfo.AreaCode;
                //籍贯
                this.txtDist.Text = info.PatientInfo.DIST;
                //民族
                this.txtNationality.Tag = info.PatientInfo.Nationality.ID;
                //身份证号
                this.txtIDNo.Text = info.PatientInfo.IDCard;
                //职业
                this.txtProfession.Tag = info.PatientInfo.Profession.ID;

                #region //婚姻
                if (info.PatientInfo.MaritalStatus.ID != null)
                {
                    switch (info.PatientInfo.MaritalStatus.ID.ToString())
                    {
                        case "M":
                            this.txtMaritalStatus.Text = "2";
                            break;
                        case "W":
                            this.txtMaritalStatus.Text = "3";
                            break;
                        case "A":
                            this.txtMaritalStatus.Text = "9";
                            break;
                        case "D":
                            this.txtMaritalStatus.Text = "4";
                            break;
                        case "R":
                            this.txtMaritalStatus.Text = "9";
                            break;
                        case "S":
                            this.txtMaritalStatus.Text = "1";
                            break;
                        default:
                            this.txtMaritalStatus.Text = info.PatientInfo.MaritalStatus.ID.ToString();
                            break;
                    }
                }
                else
                {
                    this.txtMaritalStatus.Text = "-";
                }
                #endregion
                //现住址
                this.txtCurrentAdrr.Text = info.CurrentAddr;
                //现住址电话
                this.txtCurrentPhone.Text = info.CurrentPhone;
                //现住址邮编
                this.txtCurrentZip.Text = info.CurrentZip;
                //户口地址
                this.txtHomeAdrr.Text = info.PatientInfo.AddressHome;
                //户口邮编
                this.txtHomeZip.Text = info.PatientInfo.HomeZip;
                //工作单位及地址
                this.txtAddressBusiness.Text = info.PatientInfo.AddressBusiness;
                //工作电话
                this.txtPhoneBusiness.Text = info.PatientInfo.PhoneBusiness;
                //工作邮编
                this.txtBusinessZip.Text = info.PatientInfo.BusinessZip;
                //联系方式
                this.txtKin.Text = info.PatientInfo.Kin.Name;
                //与患者关系
                this.txtRelation.Tag = info.PatientInfo.Kin.RelationLink;
                //联系地址
                this.txtLinkmanAdd.Text =info.PatientInfo.Kin.RelationAddress;
                //联系电话
                this.txtLinkmanTel.Text = info.PatientInfo.Kin.RelationPhone;
                //入院途径
                this.txtInAvenue.Text = info.InPath;
                //入院日期
                this.txtdtDateInYear.Text = info.PatientInfo.PVisit.InTime.Year.ToString();
                this.txtdtDateInMonth.Text = info.PatientInfo.PVisit.InTime.Month.ToString();
                this.txtdtDateInDay.Text = info.PatientInfo.PVisit.InTime.Day.ToString();
                this.txtdtDateInHour.Text = info.PatientInfo.PVisit.InTime.Hour.ToString();
                //入院科室
                this.txtDeptInHospital.Text = info.InDept.Name;
                //入院病房
                this.txtInRoom.Text = info.InRoom;

                #region 转科科别
                FS.HISFC.BizLogic.HealthRecord.DeptShift deptChange = new FS.HISFC.BizLogic.HealthRecord.DeptShift();

                //保存转科信息的列表

                ArrayList changeDept = new ArrayList();
                DateTime dtChange = new DateTime();
                FS.HISFC.Models.RADT.Location locaInfo =null;
                //获取转科信息
                changeDept = deptChange.QueryChangeDeptFromShiftApply(info.PatientInfo.ID, "2");
                if (changeDept != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (i >= changeDept.Count)
                        {
                            break;
                        }
                        if (i == 0)
                        {
                            locaInfo = new FS.HISFC.Models.RADT.Location();
                            locaInfo = changeDept[0] as FS.HISFC.Models.RADT.Location;
                            dtChange = new DateTime();
                            dtChange = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                            this.txtChangeDept1.Text = dtChange.Year.ToString() + "年" + dtChange.Month.ToString() + "月" + dtChange.Day.ToString() + "日" + dtChange.Hour.ToString() + "时" + "转" + locaInfo.Dept.Name;
                        }
                        else if (i == 1)
                        {
                            locaInfo = new FS.HISFC.Models.RADT.Location();
                            locaInfo = changeDept[1] as FS.HISFC.Models.RADT.Location;
                            dtChange = new DateTime();
                            dtChange = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                            this.txtChangeDept2.Text = dtChange.Year.ToString() + "年" + dtChange.Month.ToString() + "月" + dtChange.Day.ToString() + "日" + dtChange.Hour.ToString() + "时" + "转" + locaInfo.Dept.Name;
                        }
                        else if (i == 2)
                        {
                            locaInfo = new FS.HISFC.Models.RADT.Location();
                            locaInfo = changeDept[2] as FS.HISFC.Models.RADT.Location;
                            dtChange = new DateTime();
                            dtChange = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                            this.txtChangeDept3.Text = dtChange.Year.ToString() + "年" + dtChange.Month.ToString() + "月" + dtChange.Day.ToString() + "日" + dtChange.Hour.ToString() + "时" + "转" + locaInfo.Dept.Name;
                        }

                    }
                }
                else
                {
                    changeDept = new ArrayList();
                    //获取转科信息
                    changeDept = deptChange.QueryChangeDeptFromShiftApply(info.PatientInfo.ID, "1");
                    if (changeDept != null)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i >= changeDept.Count)
                            {
                                break;
                            }
                            if (i == 0)
                            {
                                locaInfo = new FS.HISFC.Models.RADT.Location();
                                locaInfo = changeDept[0] as FS.HISFC.Models.RADT.Location;
                                dtChange = new DateTime();
                                dtChange = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                                this.txtChangeDept1.Text = dtChange.Year.ToString() + "年" + dtChange.Month.ToString() + "月" + dtChange.Day.ToString() + "日" + dtChange.Hour.ToString() + "时" + "转" + locaInfo.Dept.Name;
                            }
                            else if (i == 1)
                            {
                                locaInfo = new FS.HISFC.Models.RADT.Location();
                                locaInfo = changeDept[1] as FS.HISFC.Models.RADT.Location;
                                dtChange = new DateTime();
                                dtChange = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                                this.txtChangeDept2.Text = dtChange.Year.ToString() + "年" + dtChange.Month.ToString() + "月" + dtChange.Day.ToString() + "日" + dtChange.Hour.ToString() + "时" + "转" + locaInfo.Dept.Name;
                            }
                            else if (i == 2)
                            {
                                locaInfo = new FS.HISFC.Models.RADT.Location();
                                locaInfo = changeDept[2] as FS.HISFC.Models.RADT.Location;
                                dtChange = new DateTime();
                                dtChange = FS.FrameWork.Function.NConvert.ToDateTime(locaInfo.Dept.Memo);
                                this.txtChangeDept3.Text = dtChange.Year.ToString() + "年" + dtChange.Month.ToString() + "月" + dtChange.Day.ToString() + "日" + dtChange.Hour.ToString() + "时" + "转" + locaInfo.Dept.Name;
                            }

                        }
                    }
                }
                if (this.txtChangeDept1.Text.Trim() == "" || this.txtChangeDept1.Text.Trim() == "-")
                {
                    this.txtChangeDept1.Text = "  - 年 -月 -日 -时转 -";
                }
                if (this.txtChangeDept2.Text.Trim() == "" || this.txtChangeDept2.Text.Trim() == "-")
                {
                    this.txtChangeDept2.Text = "  - 年 -月 -日 -时转 -";
                }
                if (this.txtChangeDept3.Text.Trim() == "" || this.txtChangeDept3.Text.Trim() == "-")
                {
                    this.txtChangeDept3.Text = "  - 年 -月 -日 -时转 -";
                }
                #endregion 

                //出院日期
                this.txtDateOutYear.Text = info.PatientInfo.PVisit.OutTime.Year.ToString();
                this.txtDateOutMonth.Text = info.PatientInfo.PVisit.OutTime.Month.ToString();
                this.txtDateOutDay.Text = info.PatientInfo.PVisit.OutTime.Day.ToString();
                this.txtDateOutHour.Text = info.PatientInfo.PVisit.OutTime.Hour.ToString();
                //出院科室
                this.txtDeptOutHospital.Text = info.OutDept.Name;
                //出院病房
                this.txtOutRoom.Text = info.OutRoom;
                //实际住院天数
                this.txtPiDays.Text = info.InHospitalDays.ToString();
                //门诊诊断
                this.txtClinicDiagName.Text = info.ClinicDiag.Name;
                if (isNotPrintIcdCode)
                {
                    this.txtClinicDiagCode.Text = "";
                }
                else
                {
                    this.txtClinicDiagCode.Text = info.ClinicDiag.ID;
                }
                //门（急）诊诊断
                this.txtClincDoc.Text = info.ClinicDoc.Name;
                #region 出院诊断
                int diagNameNum = 26;
                ArrayList diagList = Constant.GetList("CASEDIAGNAME");
                if (diagList != null && diagList.Count > 0)
                {
                    FS.FrameWork.Models.NeuObject diagObj = diagList[0] as FS.FrameWork.Models.NeuObject;
                    if (diagObj.Memo != "")
                    {
                        try
                        {
                            diagNameNum = FS.FrameWork.Function.NConvert.ToInt32(diagObj.Memo);
                        }
                        catch
                        {
                            diagNameNum = 26;
                        }
                    }
                    else
                    {
                        diagNameNum = 26;
                    }
                }
                FS.HISFC.BizLogic.HealthRecord.Diagnose DiaMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                ArrayList al = new ArrayList();
                al = DiaMgr.QueryCaseDiagnose(info.PatientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, FS.HISFC.Models.Base.ServiceTypes.I);//DiaMgr.QueryDiagNoseNew(myItem.PatientInfo.ID);
                if (al != null)
                {
                    if (al.Count > 0)
                    {
                        int number = 0;
                        foreach (FS.HISFC.Models.HealthRecord.Diagnose diagNose in al)
                        {
                            if (number != 0 && diagNose.DiagInfo.DiagType.ID == "1")//防止多个主要诊断打印不出来情况
                            {
                                diagNose.DiagInfo.DiagType.ID = "2";
                            }
                            if (diagNose.DiagInfo.DiagType.ID != "1" && diagNose.DiagInfo.DiagType.ID != "2")//并发症诊断归入到其他诊断中打印出来
                            {
                                diagNose.DiagInfo.DiagType.ID = "2";
                            }

                            if (diagNose.DiagInfo.DiagType.ID == "1")
                            {
                                if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                {
                                    this.txtMainDiag1.Visible = true;
                                    this.txtMainDiag1.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//主要诊断
                                }
                                else if (diagNose.DiagInfo.ICD10.Name.Length > diagNameNum && diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum + 4)
                                {
                                    this.txtMainDiag1.Visible = true;
                                    this.txtMainDiag1.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//主要诊断
                                    this.txtMainDiag1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                }
                                else if (diagNose.DiagInfo.ICD10.Name.Length > diagNameNum + 4)
                                {
                                    this.txtMainDiagName1.Visible = true;
                                    this.txtMainDiagName2.Visible = true;
                                    this.txtMainDiag1.Visible = false;
                                    string mainDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//主要诊断
                                    //this.txtMainDiagName1.Text = mainDiag;
                                    this.txtMainDiagName1.Text = mainDiag.Substring(0, 30);
                                    this.txtMainDiagName2.Text = mainDiag.Substring(30);
                                    this.txtMainDiagName1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                    this.txtMainDiagName2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                }

                                if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                {
                                    this.txtMainDiagCode.Text = "";
                                }
                                else
                                {
                                    this.txtMainDiagCode.Text = diagNose.DiagInfo.ICD10.ID;
                                }
                                if (isNotPrintIcdCode)//不打印ICD编码
                                {
                                    this.txtMainDiagCode.Text = "";
                                }
                                switch (diagNose.DiagOutState)
                                {
                                    case "1":
                                        this.txtMainDiagInStateA.Text = "√";
                                        this.txtMainDiagInStateB.Text = "";
                                        this.txtMainDiagInStateC.Text = "";
                                        this.txtMainDiagInStateD.Text = "";
                                        break;
                                    case "2":
                                        this.txtMainDiagInStateA.Text = "";
                                        this.txtMainDiagInStateB.Text = "√";
                                        this.txtMainDiagInStateC.Text = "";
                                        this.txtMainDiagInStateD.Text = "";
                                        break;
                                    case "3":
                                        this.txtMainDiagInStateA.Text = "";
                                        this.txtMainDiagInStateB.Text = "";
                                        this.txtMainDiagInStateC.Text = "√";
                                        this.txtMainDiagInStateD.Text = "";
                                        break;
                                    case "4":
                                        this.txtMainDiagInStateA.Text = "";
                                        this.txtMainDiagInStateB.Text = "";
                                        this.txtMainDiagInStateC.Text = "";
                                        this.txtMainDiagInStateD.Text = "√";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else if (diagNose.DiagInfo.DiagType.ID == "2")//其他诊断
                            {
                                number++;
                                string diagStr = string.Empty;
                                switch (number.ToString())
                                {
                                    case "1":
                                        //this.txtOtherDiag1.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 35);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag1.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag1.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag1.Text = otherDiag;

                                            }
                                            this.txtOtherDiag1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }

                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode1.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode1.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode1.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState1A.Text = "√";
                                                this.txtOtherDiagInState1B.Text = "";
                                                this.txtOtherDiagInState1C.Text = "";
                                                this.txtOtherDiagInState1D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState1A.Text = "";
                                                this.txtOtherDiagInState1B.Text = "√";
                                                this.txtOtherDiagInState1C.Text = "";
                                                this.txtOtherDiagInState1D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState1A.Text = "";
                                                this.txtOtherDiagInState1B.Text = "";
                                                this.txtOtherDiagInState1C.Text = "√";
                                                this.txtOtherDiagInState1D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState1A.Text = "";
                                                this.txtOtherDiagInState1B.Text = "";
                                                this.txtOtherDiagInState1C.Text = "";
                                                this.txtOtherDiagInState1D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "2":
                                        //this.txtOtherDiag2.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag2.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag2.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag2.Text = otherDiag;

                                            }
                                            this.txtOtherDiag2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }

                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode2.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode2.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode2.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState2A.Text = "√";
                                                this.txtOtherDiagInState2B.Text = "";
                                                this.txtOtherDiagInState2C.Text = "";
                                                this.txtOtherDiagInState2D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState2A.Text = "";
                                                this.txtOtherDiagInState2B.Text = "√";
                                                this.txtOtherDiagInState2C.Text = "";
                                                this.txtOtherDiagInState2D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState2A.Text = "";
                                                this.txtOtherDiagInState2B.Text = "";
                                                this.txtOtherDiagInState2C.Text = "√";
                                                this.txtOtherDiagInState2D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState2A.Text = "";
                                                this.txtOtherDiagInState2B.Text = "";
                                                this.txtOtherDiagInState2C.Text = "";
                                                this.txtOtherDiagInState2D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "3":
                                        //this.txtOtherDiag3.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag3.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag3.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag3.Text = otherDiag;

                                            }
                                            this.txtOtherDiag3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode3.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode3.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode3.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState3A.Text = "√";
                                                this.txtOtherDiagInState3B.Text = "";
                                                this.txtOtherDiagInState3C.Text = "";
                                                this.txtOtherDiagInState3D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState3A.Text = "";
                                                this.txtOtherDiagInState3B.Text = "√";
                                                this.txtOtherDiagInState3C.Text = "";
                                                this.txtOtherDiagInState3D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState3A.Text = "";
                                                this.txtOtherDiagInState3B.Text = "";
                                                this.txtOtherDiagInState3C.Text = "√";
                                                this.txtOtherDiagInState3D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState3A.Text = "";
                                                this.txtOtherDiagInState3B.Text = "";
                                                this.txtOtherDiagInState3C.Text = "";
                                                this.txtOtherDiagInState3D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "4":
                                        //this.txtOtherDiag4.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag4.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag4.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag4.Text = otherDiag;

                                            }
                                            this.txtOtherDiag4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode4.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode4.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode4.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState4A.Text = "√";
                                                this.txtOtherDiagInState4B.Text = "";
                                                this.txtOtherDiagInState4C.Text = "";
                                                this.txtOtherDiagInState4D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState4A.Text = "";
                                                this.txtOtherDiagInState4B.Text = "√";
                                                this.txtOtherDiagInState4C.Text = "";
                                                this.txtOtherDiagInState4D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState4A.Text = "";
                                                this.txtOtherDiagInState4B.Text = "";
                                                this.txtOtherDiagInState4C.Text = "√";
                                                this.txtOtherDiagInState4D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState4A.Text = "";
                                                this.txtOtherDiagInState4B.Text = "";
                                                this.txtOtherDiagInState4C.Text = "";
                                                this.txtOtherDiagInState4D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "5":
                                        //this.txtOtherDiag5.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag5.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag5.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag5.Text = otherDiag;

                                            }
                                            this.txtOtherDiag5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode5.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode5.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode5.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState5A.Text = "√";
                                                this.txtOtherDiagInState5B.Text = "";
                                                this.txtOtherDiagInState5C.Text = "";
                                                this.txtOtherDiagInState5D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState5A.Text = "";
                                                this.txtOtherDiagInState5B.Text = "√";
                                                this.txtOtherDiagInState5C.Text = "";
                                                this.txtOtherDiagInState5D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState5A.Text = "";
                                                this.txtOtherDiagInState5B.Text = "";
                                                this.txtOtherDiagInState5C.Text = "√";
                                                this.txtOtherDiagInState5D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState5A.Text = "";
                                                this.txtOtherDiagInState5B.Text = "";
                                                this.txtOtherDiagInState5C.Text = "";
                                                this.txtOtherDiagInState5D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "6":
                                        //this.txtOtherDiag6.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag6.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag6.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag6.Text = otherDiag;

                                            }
                                            this.txtOtherDiag6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode6.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode6.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode6.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState6A.Text = "√";
                                                this.txtOtherDiagInState6B.Text = "";
                                                this.txtOtherDiagInState6C.Text = "";
                                                this.txtOtherDiagInState6D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState6A.Text = "";
                                                this.txtOtherDiagInState6B.Text = "√";
                                                this.txtOtherDiagInState6C.Text = "";
                                                this.txtOtherDiagInState6D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState6A.Text = "";
                                                this.txtOtherDiagInState6B.Text = "";
                                                this.txtOtherDiagInState6C.Text = "√";
                                                this.txtOtherDiagInState6D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState6A.Text = "";
                                                this.txtOtherDiagInState6B.Text = "";
                                                this.txtOtherDiagInState6C.Text = "";
                                                this.txtOtherDiagInState6D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "7":
                                        //this.txtOtherDiag7.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag7.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag7.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag7.Text = otherDiag;

                                            }
                                            this.txtOtherDiag7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode7.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode7.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode7.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState7A.Text = "√";
                                                this.txtOtherDiagInState7B.Text = "";
                                                this.txtOtherDiagInState7C.Text = "";
                                                this.txtOtherDiagInState7D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState7A.Text = "";
                                                this.txtOtherDiagInState7B.Text = "√";
                                                this.txtOtherDiagInState1C.Text = "";
                                                this.txtOtherDiagInState7D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState7A.Text = "";
                                                this.txtOtherDiagInState7B.Text = "";
                                                this.txtOtherDiagInState7C.Text = "√";
                                                this.txtOtherDiagInState7D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState7A.Text = "";
                                                this.txtOtherDiagInState7B.Text = "";
                                                this.txtOtherDiagInState7C.Text = "";
                                                this.txtOtherDiagInState7D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "8":
                                        //this.txtOtherDiag8.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag8.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag8.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag8.Text = otherDiag;

                                            }
                                            this.txtOtherDiag8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode8.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode8.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode8.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState8A.Text = "√";
                                                this.txtOtherDiagInState8B.Text = "";
                                                this.txtOtherDiagInState8C.Text = "";
                                                this.txtOtherDiagInState8D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState8A.Text = "";
                                                this.txtOtherDiagInState8B.Text = "√";
                                                this.txtOtherDiagInState8C.Text = "";
                                                this.txtOtherDiagInState8D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState8A.Text = "";
                                                this.txtOtherDiagInState8B.Text = "";
                                                this.txtOtherDiagInState8C.Text = "√";
                                                this.txtOtherDiagInState8D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState8A.Text = "";
                                                this.txtOtherDiagInState8B.Text = "";
                                                this.txtOtherDiagInState8C.Text = "";
                                                this.txtOtherDiagInState8D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "9":
                                        //this.txtOtherDiag9.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag9.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag9.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag9.Text = otherDiag;

                                            }
                                            this.txtOtherDiag9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode9.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode9.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode9.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState9A.Text = "√";
                                                this.txtOtherDiagInState9B.Text = "";
                                                this.txtOtherDiagInState9C.Text = "";
                                                this.txtOtherDiagInState9D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState9A.Text = "";
                                                this.txtOtherDiagInState9B.Text = "√";
                                                this.txtOtherDiagInState9C.Text = "";
                                                this.txtOtherDiagInState9D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState9A.Text = "";
                                                this.txtOtherDiagInState9B.Text = "";
                                                this.txtOtherDiagInState9C.Text = "√";
                                                this.txtOtherDiagInState9D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState9A.Text = "";
                                                this.txtOtherDiagInState9B.Text = "";
                                                this.txtOtherDiagInState9C.Text = "";
                                                this.txtOtherDiagInState9D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "10":
                                        //this.txtOtherDiag10.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag10.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag10.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag10.Text = otherDiag;

                                            }
                                            this.txtOtherDiag10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode10.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode10.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode10.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState10A.Text = "√";
                                                this.txtOtherDiagInState10B.Text = "";
                                                this.txtOtherDiagInState10C.Text = "";
                                                this.txtOtherDiagInState10D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState10A.Text = "";
                                                this.txtOtherDiagInState10B.Text = "√";
                                                this.txtOtherDiagInState10C.Text = "";
                                                this.txtOtherDiagInState10D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState10A.Text = "";
                                                this.txtOtherDiagInState10B.Text = "";
                                                this.txtOtherDiagInState10C.Text = "√";
                                                this.txtOtherDiagInState10D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState10A.Text = "";
                                                this.txtOtherDiagInState10B.Text = "";
                                                this.txtOtherDiagInState10C.Text = "";
                                                this.txtOtherDiagInState10D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "11":
                                        //this.txtOtherDiag11.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag11.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag11.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag11.Text = otherDiag;

                                            }
                                            this.txtOtherDiag11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode11.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode11.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode11.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState11A.Text = "√";
                                                this.txtOtherDiagInState11B.Text = "";
                                                this.txtOtherDiagInState11C.Text = "";
                                                this.txtOtherDiagInState11D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState11A.Text = "";
                                                this.txtOtherDiagInState11B.Text = "√";
                                                this.txtOtherDiagInState11C.Text = "";
                                                this.txtOtherDiagInState11D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState11A.Text = "";
                                                this.txtOtherDiagInState11B.Text = "";
                                                this.txtOtherDiagInState11C.Text = "√";
                                                this.txtOtherDiagInState11D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState11A.Text = "";
                                                this.txtOtherDiagInState11B.Text = "";
                                                this.txtOtherDiagInState11C.Text = "";
                                                this.txtOtherDiagInState11D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    case "12":
                                        //this.txtOtherDiag12.Text = this.DiagName(CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false), 41);
                                        if (diagNose.DiagInfo.ICD10.Name.Length <= diagNameNum)
                                        {
                                            this.txtOtherDiag12.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);//其他诊断
                                        }
                                        else
                                        {
                                            string otherDiag = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                            if (otherDiag.Length > diagNameNum + 4)
                                            {
                                                this.txtOtherDiag12.Text = otherDiag.Substring(0, diagNameNum + 4);
                                            }
                                            else
                                            {
                                                this.txtOtherDiag12.Text = otherDiag;

                                            }
                                            this.txtOtherDiag12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                                        }
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.txtOtherDiagCode12.Text = "";
                                        }
                                        else
                                        {
                                            this.txtOtherDiagCode12.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        if (isNotPrintIcdCode)//不打印ICD编码
                                        {
                                            this.txtOtherDiagCode12.Text = "";
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.txtOtherDiagInState12A.Text = "√";
                                                this.txtOtherDiagInState12B.Text = "";
                                                this.txtOtherDiagInState12C.Text = "";
                                                this.txtOtherDiagInState12D.Text = "";
                                                break;
                                            case "2":
                                                this.txtOtherDiagInState12A.Text = "";
                                                this.txtOtherDiagInState12B.Text = "√";
                                                this.txtOtherDiagInState12C.Text = "";
                                                this.txtOtherDiagInState12D.Text = "";
                                                break;
                                            case "3":
                                                this.txtOtherDiagInState12A.Text = "";
                                                this.txtOtherDiagInState12B.Text = "";
                                                this.txtOtherDiagInState12C.Text = "√";
                                                this.txtOtherDiagInState12D.Text = "";
                                                break;
                                            case "4":
                                                this.txtOtherDiagInState12A.Text = "";
                                                this.txtOtherDiagInState12B.Text = "";
                                                this.txtOtherDiagInState12C.Text = "";
                                                this.txtOtherDiagInState12D.Text = "√";
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                #endregion
                //病例分型
                this.txtExampleType.Text = info.ExampleType;
                //是否临床路径
                this.txtClinicPath.Text = info.ClinicPath;
                //抢救次数
                this.txtSalvTimes.Text = info.SalvTimes.ToString();
                //成功次数
                this.txtSuccTimes.Text = info.SuccTimes.ToString();
                //损伤中毒原因
                this.txtInjuryOrPoisoningCause.Text = info.InjuryOrPoisoningCause;
                if (isNotPrintIcdCode)
                {
                    this.txtInjuryOrPoisoningCauseCode.Text = "";
                }
                else
                {
                    if (info.InjuryOrPoisoningCauseCode == "MS999" || info.InjuryOrPoisoningCauseCode == "-")
                    {
                        this.txtInjuryOrPoisoningCauseCode.Text = "";
                    }
                    else
                    {
                        this.txtInjuryOrPoisoningCauseCode.Text = info.InjuryOrPoisoningCauseCode;
                    }
                }
                //病理诊断
                this.txtPathologicalDiagName.Text = info.PathologicalDiagName;
                //病理诊断编码
                if (isNotPrintIcdCode)
                {
                    this.txtPathologicalDiagCode.Text = "";
                }
                else
                {
                    this.txtPathologicalDiagCode.Text = info.PathologicalDiagCode;
                }
                //病理号
                this.txtPathologicalDiagNum.Text = info.PathNum;
                //药物过敏标志
                this.txtIsDrugAllergy.Text = info.AnaphyFlag;
                //过敏药物
                this.txtDrugAllergy.Text = info.FirstAnaphyPharmacy.ID.ToString();
                //死亡患者尸体检查
                //this.txtDeathPatientBobyCheck.Text = info.CadaverCheck;
                switch (info.CadaverCheck)
                {
                    case "1":
                        this.txtDeathPatientBobyCheck.Text = "1";
                        break;
                    case "2":
                        this.txtDeathPatientBobyCheck.Text = "2";
                        break;
                    default:
                        this.txtDeathPatientBobyCheck.Text = "-";
                        break;
                }
                //ABO血型
                if (info.PatientInfo.BloodType.ID != null)
                {
                    switch (info.PatientInfo.BloodType.ID.ToString())
                    {
                        case "A":
                            this.txtBloodType.Text = "1";
                            break;
                        case "B":
                            this.txtBloodType.Text = "2";
                            break;
                        case "AB":
                            this.txtBloodType.Text = "4";
                            break;
                        case "O":
                            this.txtBloodType.Text = "3";
                            break;
                        case "U":
                            this.txtBloodType.Text = "5";
                            break;
                        case "9":
                            this.txtBloodType.Text = "6";
                            break;
                        default:
                            this.txtBloodType.Text = info.PatientInfo.BloodType.ID.ToString();
                            break;
                    }
                }
                else
                {
                    this.txtBloodType.Text = "-";
                }
                //RH血型
                if (info.RhBlood != null && info.RhBlood != "")
                {
                    this.txtRhBlood.Text = info.RhBlood;
                }
                else
                {
                    this.txtRhBlood.Text = "-";
                }
                //医生信息
                //科主任
                this.txtDeptChiefDoc.Text = info.PatientInfo.PVisit.ReferringDoctor.Name;
                //主任（副主任）医师
                this.txtConsultingDoctor.Text = info.PatientInfo.PVisit.ConsultingDoctor.Name;
                //主治医师
                this.txtAttendingDoctor.Text = info.PatientInfo.PVisit.AttendingDoctor.Name;
                //住院医师
                this.txtAdmittingDoctor.Text = info.PatientInfo.PVisit.AdmittingDoctor.Name;
                //责任护士
                this.txtDutyNurse.Text = info.DutyNurse.Name;
                //进修医师
                this.txtRefresherDocd.Text = info.RefresherDoc.Name;
                //实习医师
                this.txtPraDocCode.Text = info.PatientInfo.PVisit.TempDoctor.Name;
                //编码员
                this.txtCodingCode.Text = info.CodingOper.Name;
                //病案质量
                this.txtMrQual.Text = info.MrQuality;
                //质控医师
                this.txtQcDocd.Text = info.QcDoc.Name;
                //质控护士
                this.txtQcNucd.Text = info.QcNurse.Name;
                //质控日期  广医四院要求手工填写质控日期2012-9-19
                ArrayList checkDtList = Constant.GetList("CASECHECKDATE");
                if (checkDtList != null && checkDtList.Count > 0)
                {
                    this.txtCheckDateYear.Text = "";
                    this.txtCheckDateMonth.Text = "";
                    this.txtCheckDateDay.Text = "";
                }
                else
                {
                    this.txtCheckDateYear.Text = info.CheckDate.Year.ToString();
                    this.txtCheckDateMonth.Text = info.CheckDate.Month.ToString();
                    this.txtCheckDateDay.Text = info.CheckDate.Day.ToString();
                }
            }
            catch
            {
            }
               
        }
        private void initList()
        {
            try
            {

                //查询国家列表
                ArrayList countryList = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
                this.txtCountry.AddItems(countryList);
                //查询民族列表
                ArrayList nationalList = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.NATION);
                this.txtNationality.AddItems(nationalList);
                //查询职业列表
                //ArrayList professionList = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.PROFESSION);
                ArrayList professionList = Constant.GetList("CASEPROFESSION");
                this.txtProfession.AddItems(professionList);
                //与联系人关系
                ArrayList RelationList = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.RELATIVE);
                this.txtRelation.AddItems(RelationList);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface.Reset()
        {
            this.txtHopitalName.Text = "";
            this.txtHospitalCode.Text = "";
            this.txtPactKind.Text = "";
            this.txtHealthyCard.Text = "";
            this.txtInTimes.Text = "";
            this.txtCaseNum.Text = "";
            this.txtPatientName.Text = "";
            this.txtPatientSex.Text = "";
            this.txtPatientBirthdayYear.Text = "";
            this.txtPatientBirthdayMonth.Text = "";
            this.txtPatientBirthdayDay.Text = "";
            this.txtPatientAge.Text = "";
            this.txtCountry.Text = "";
            this.txtBabyBirthWeight.Text = "";
            this.txtBabyInWeight.Text = "";
            this.txtBirthAddr.Text = "";
            this.txtDist.Text = "";
            this.txtNationality.Text = "";
            this.txtIDNo.Text = "";
            this.txtProfession.Text = "";
            this.txtMaritalStatus.Text = "";
            this.txtCurrentAdrr.Text = "";
            this.txtCurrentPhone.Text = "";
            this.txtCurrentZip.Text = "";
            this.txtHomeAdrr.Text = "";
            this.txtHomeZip.Text = "";
            this.txtAddressBusiness.Text = "";
            this.txtPhoneBusiness.Text = "";
            this.txtBusinessZip.Text = "";
            this.txtKin.Text = "";
            this.txtRelation.Text = "";
            this.txtLinkmanAdd.Text = "";
            this.txtLinkmanTel.Text = "";
            this.txtInAvenue.Text = "";
            this.txtdtDateInYear.Text = "";
            this.txtdtDateInMonth.Text = "";
            this.txtdtDateInDay.Text = "";
            this.txtdtDateInHour.Text="";
            this.txtDeptInHospital.Text = "";
            this.txtInRoom.Text="";
            this.txtChangeDept1.Text = "";
            this.txtChangeDept2.Text = "";
            this.txtChangeDept3.Text = "";
            this.txtDateOutYear.Text = "";
            this.txtDateOutMonth.Text = "";
            this.txtDateOutDay.Text = "";
            this.txtDateOutHour.Text = "";
            this.txtDeptOutHospital.Text = "";
            this.txtOutRoom.Text = "";
            this.txtPiDays.Text = "";
            this.txtClinicDiagName.Text = "";
            this.txtClinicDiagCode.Text = "";
            this.txtClincDoc.Text = "";
            //出院诊断
            this.txtMainDiagName1.Visible = false;
            this.txtMainDiagName2.Visible = false;
            this.txtMainDiagName1.Text = "";
            this.txtMainDiagName2.Text = "";
            this.txtMainDiag1.Text = "";
            this.txtMainDiagCode.Text = "";
            this.txtMainDiagInStateA.Text = "";
            this.txtOtherDiag1.Text = "";
            this.txtOtherDiag2.Text = "";
            this.txtOtherDiag3.Text="";
            this.txtOtherDiag4.Text="";
            this.txtOtherDiag5.Text="";
            this.txtOtherDiag6.Text="";
            this.txtOtherDiag7.Text="";
            this.txtOtherDiag8.Text="";
            this.txtOtherDiag9.Text = "";
            this.txtOtherDiag10.Text = "";
            this.txtOtherDiag11.Text = "";
            this.txtOtherDiag12.Text = "";
            this.txtMainDiagInStateB.Text = "";
            this.txtOtherDiagInState1B.Text = "";
            this.txtOtherDiagInState2B.Text = "";
            this.txtOtherDiagInState3B.Text = "";
            this.txtOtherDiagInState4B.Text = "";
            this.txtOtherDiagInState5B.Text = "";
            this.txtOtherDiagInState6B.Text = "";
            this.txtOtherDiagInState7B.Text = "";
            this.txtOtherDiagInState8B.Text = "";
            this.txtOtherDiagInState9B.Text = "";
            this.txtOtherDiagInState10B.Text = "";
            this.txtOtherDiagInState11B.Text = "";
            this.txtOtherDiagInState12B.Text = "";
            this.txtOtherDiagCode1.Text = "";
            this.txtOtherDiagCode2.Text = "";
            this.txtOtherDiagCode3.Text = "";
            this.txtOtherDiagCode4.Text = "";
            this.txtOtherDiagCode5.Text = "";
            this.txtOtherDiagCode6.Text = "";
            this.txtOtherDiagCode7.Text = "";
            this.txtOtherDiagCode8.Text = "";
            this.txtOtherDiagCode9.Text = "";
            this.txtOtherDiagCode10.Text = "";
            this.txtOtherDiagCode11.Text = "";
            this.txtOtherDiagCode12.Text = "";
            this.txtMainDiagInStateC.Text = "";
            this.txtOtherDiagInState1C.Text = "";
            this.txtOtherDiagInState2C.Text = "";
            this.txtOtherDiagInState3C.Text = "";
            this.txtOtherDiagInState4C.Text = "";
            this.txtOtherDiagInState5C.Text = "";
            this.txtOtherDiagInState6C.Text = "";
            this.txtOtherDiagInState7C.Text = "";
            this.txtOtherDiagInState8C.Text = "";
            this.txtOtherDiagInState9C.Text = "";
            this.txtOtherDiagInState10C.Text = "";
            this.txtOtherDiagInState11C.Text = "";
            this.txtOtherDiagInState12C.Text = "";
            this.txtOtherDiagInState1A.Text = "";
            this.txtOtherDiagInState2A.Text = "";
            this.txtOtherDiagInState3A.Text = "";
            this.txtOtherDiagInState4A.Text = "";
            this.txtOtherDiagInState5A.Text = "";
            this.txtOtherDiagInState6A.Text = "";
            this.txtOtherDiagInState7A.Text = "";
            this.txtOtherDiagInState8A.Text = "";
            this.txtOtherDiagInState9A.Text = "";
            this.txtOtherDiagInState10A.Text = "";
            this.txtOtherDiagInState11A.Text = "";
            this.txtOtherDiagInState12A.Text = "";
            this.txtMainDiagInStateD.Text = "";
            this.txtOtherDiagInState1D.Text = "";
            this.txtOtherDiagInState2D.Text = "";
            this.txtOtherDiagInState3D.Text = "";
            this.txtOtherDiagInState4D.Text = "";
            this.txtOtherDiagInState5D.Text = "";
            this.txtOtherDiagInState6D.Text = "";
            this.txtOtherDiagInState7D.Text = "";
            this.txtOtherDiagInState8D.Text = "";
            this.txtOtherDiagInState9D.Text = "";
            this.txtOtherDiagInState10D.Text = "";
            this.txtOtherDiagInState11D.Text = "";
            this.txtOtherDiagInState12D.Text = "";

            this.txtExampleType.Text = "";
            this.txtClinicPath.Text = "";
            this.txtSalvTimes.Text = "";
            this.txtSuccTimes.Text = "";

            this.txtInjuryOrPoisoningCause.Text = "";
            this.txtInjuryOrPoisoningCauseCode.Text="";
            this.txtPathologicalDiagName.Text = "";
            this.txtPathologicalDiagCode.Text = "";
            this.txtPathologicalDiagNum.Text = "";
            this.txtIsDrugAllergy.Text = "";
            this.txtDrugAllergy.Text = "";
            this.txtDeathPatientBobyCheck.Text = "";
            this.txtBloodType.Text = "";
            this.txtRhBlood.Text = "";
            //医生信息
            this.txtDeptChiefDoc.Text = "";
            this.txtConsultingDoctor.Text = "";
            this.txtAttendingDoctor.Text = "";
            this.txtAdmittingDoctor.Text = "";
            this.txtDutyNurse.Text = "";
            this.txtRefresherDocd.Text = "";
            this.txtPraDocCode.Text = "";
            this.txtCodingCode.Text = "";

            this.txtMrQual.Text = "";
            this.txtQcDocd.Text = "";
            this.txtQcNucd.Text = "";
            this.txtCheckDateYear.Text = "";
            this.txtCheckDateMonth.Text = "";
            this.txtCheckDateDay.Text = "";
        }

        #endregion

        #region IReportPrinter 成员

        int FS.FrameWork.WinForms.Forms.IReportPrinter.Export()
        {
            return 1;
        }
        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
        int FS.FrameWork.WinForms.Forms.IReportPrinter.Print()
        {
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.FrameWork.WinForms.Classes.Print print = null;

            try
            {
                print = new FS.FrameWork.WinForms.Classes.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show("初始化打印机失败");
            }
            print.SetPageSize(pageSizeManager.GetPageSize("BAGL"));
            return p.PrintPage(0, 1, this);
        }
        private void PreviewZoom()
        {
            //是否需要设置预览效果100%
            ArrayList priViewZoom = Constant.GetList("CASEPRINTVIEWZOOM");
            if (priViewZoom != null && priViewZoom.Count > 0)
            {
                this.isPrintViewZoom = true;
            }
        }
        int FS.FrameWork.WinForms.Forms.IReportPrinter.PrintPreview()
        {
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            this.PreviewZoom();
            if (this.isPrintViewZoom)
            {
                p.PreviewZoomFactor = 1;
            }
            return p.PrintPreview(0, 1, this);
        }

        #endregion

        private void ucCaseFirstPrint_Load(object sender, EventArgs e)
        {

        }
    }
}
