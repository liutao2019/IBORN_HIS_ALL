using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    public partial class ucGyhisCaseFristPrint : UserControl ,FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface
    {
        public ucGyhisCaseFristPrint()
        {
            InitializeComponent();
            LoadInfo();
        }

        #region 初时化
        public void LoadInfo()
        {
            try
            {
                FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();
                //查询职业列表
                //初始化工作信息:
                this.workComboBox.ShowCustomerList = false;
                this.workComboBox.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.PROFESSION));
                //查询民族列表
                ArrayList Nationallist1 = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.NATION);
                this.txtNationality.AddItems(Nationallist1);
                //g查询国家列表
                ArrayList list1 = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
                this.txtCountry.AddItems(list1);
                //获取病人来源
                ArrayList Insourcelist = Constant.GetAllList("PATIENTINSOURCE");
                this.txtPatientInSource.AddItems(Insourcelist);
                //与联系人关系
                ArrayList RelationList = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.RELATIVE);
                this.txtRelation.AddItems(RelationList);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion

        private FS.HISFC.BizLogic.Manager.Department inpatientManager = new FS.HISFC.BizLogic.Manager.Department();
        FS.HISFC.Components.HealthRecord.Function fun = new Function();
        /// <summary>
        /// 设置预览比例 
        /// 新的FrameWork才有改属性
        /// </summary>
        private bool isPrintViewZoom = false;
        #region HealthRecordInterface 成员

        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface.ControlValue(FS.HISFC.Models.HealthRecord.Base obj)
        {
            try
            {
                this.Clear();
                this.SetVisible();

                #region 赋值

                FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
                FS.HISFC.BizLogic.HealthRecord.DeptShift deptMger = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
                FS.HISFC.BizLogic.HealthRecord.Fee feeCaseMgr = new FS.HISFC.BizLogic.HealthRecord.Fee();
                FS.HISFC.Models.HealthRecord.Base myItem = obj as FS.HISFC.Models.HealthRecord.Base;
                FS.HISFC.BizLogic.Fee.PactUnitInfo pactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

                string HosName = baseDml.Hospital.Name;
                #region 标题位置
                if (HosName.Length == 10)
                {
                    this.label3.Location = new System.Drawing.Point(232, 36);
                }
                else if (HosName.Length == 9)
                {
                    this.label3.Location = new System.Drawing.Point(249, 36);
                }
                else if (HosName.Length == 8)
                {
                    this.label3.Location = new System.Drawing.Point(261, 36);
                }
                else if (HosName.Length == 7)
                {
                    this.label3.Location = new System.Drawing.Point(274, 35);
                }
                else if (HosName.Length == 6)
                {
                    this.label3.Location = new System.Drawing.Point(291, 36);
                }
                else if (HosName.Length == 5)
                {
                    this.label3.Location = new System.Drawing.Point(309, 36);
                }
                else if (HosName.Length == 4)
                {
                    this.label3.Location = new System.Drawing.Point(320, 36);
                }
                #endregion
                this.label3.Text = HosName;
                #region 全部重新打印出来  屏蔽套打设置
                //if (myItem.PetNum == "all" || myItem.PetNum=="")
                //{
                //    this.payKindCbx.Visible = true;
                //    this.txtInHosNo.Visible = true;
                //    this.InpatientNO.Visible = true;
                //    this.nameTextBox.Visible = true;
                //    this.sexComboBox.Visible = true;
                //    this.birYear.Visible = true;
                //    this.birMon.Visible = true;
                //    this.birDay.Visible = true;
                //    this.ageTextBox.Visible = true;
                //    this.marryTextBox.Visible = true;
                //    this.workComboBox.Visible = true;
                //    this.birthInComboBox.Visible = true;
                //    this.txtNationality.Visible = true;
                //    this.txtCountry.Visible = true;
                //    this.IDTextBox.Visible = true;
                //    this.workAdressTextBox.Visible = true;
                //    this.workPhoneTextBox.Visible = true;
                //    this.workZipTextBox.Visible = true;
                //    this.homeAdTextBox.Visible = true;
                //    this.homeZipTextBox.Visible = true;
                //    this.linkNameTextBox.Visible = true;
                //    this.txtRelation.Visible = true;
                //    this.linkAdressTextBox.Visible = true;
                //    this.linkPhoneTextBox.Visible = true;
                //    this.CYear2.Visible = true;
                //    this.CMon2.Visible = true;
                //    this.CDay2.Visible = true;
                //    this.inDeptComboBox.Visible = true;
                //    this.neuLabelZKKBBS.Visible = true;
                //    this.neuLabel2ZKKB.Visible = true;
                //    this.neuLabelryqkw.Visible = true;
                //    this.neuLabelryqkj.Visible = true;
                //    this.neuLabelryqkyb.Visible = true;

                //}
                #endregion
                //医疗付款方式
                if (myItem.PatientInfo.Pact.ID!=null || myItem.PatientInfo.Pact.ID != "")
                {
                    FS.HISFC.Models.Base.PactInfo pactInfo = pactMgr.GetPactUnitInfoByPactCode(myItem.PatientInfo.Pact.ID);

                    switch (pactInfo.PayKind.ID.TrimStart('0'))
                    {
                        case "1":
                            this.payKindCbx.Text = "3";
                            break;
                        case "2":
                            this.payKindCbx.Text = "1";
                            break;
                        case "3":
                            this.payKindCbx.Text = "4";
                            break;
                        case "4":
                            this.payKindCbx.Text = "1";
                            break;
                        case "5":
                            this.payKindCbx.Text = "1";
                            break;
                        case "6":
                            this.payKindCbx.Text = "1";
                            break;
                        case "7":
                            this.payKindCbx.Text = "3";
                            break;
                        case "8":
                            this.payKindCbx.Text = "3";
                            break;
                        case "9":
                            this.payKindCbx.Text = "1";
                            break;
                        case "10":
                            this.payKindCbx.Text = "1";
                            break;
                        case "11":
                            this.payKindCbx.Text = "1";
                            break;
                        case "12":
                            this.payKindCbx.Text = "1";
                            break;
                        default:
                            this.payKindCbx.Text = "3";
                            break;
                    }
                }
                else
                {
                    this.payKindCbx.Text = "/";
                }
                this.txtInHosNo.Text = myItem.PatientInfo.InTimes.ToString(); //住院次数
                this.InpatientNO.Text = myItem.PatientInfo.PID.PatientNO;//病案号

                this.nameTextBox.Text = myItem.PatientInfo.Name;//姓名
                if (myItem.PatientInfo.Sex.ID!=null && myItem.PatientInfo.Sex.ID.ToString() == "M") //性别
                {
                    sexComboBox.Text = "1";
                }
                else if (myItem.PatientInfo.Sex.ID != null && myItem.PatientInfo.Sex.ID.ToString() == "F")
                {
                    sexComboBox.Text = "2";
                }
                this.birYear.Text = myItem.PatientInfo.Birthday.Year.ToString();//出生年
                this.birMon.Text = myItem.PatientInfo.Birthday.Month.ToString();//出生月
                this.birDay.Text = myItem.PatientInfo.Birthday.Day.ToString();//出生日
                //this.ageTextBox.Text = this.inpatientManager.GetAge(myItem.PatientInfo.Birthday).ToString();//年龄
                this.ageTextBox.Text = baseDml.GetAgeByFun(myItem.PatientInfo.Birthday.Date, myItem.PatientInfo.PVisit.InTime.Date);

                switch (myItem.PatientInfo.MaritalStatus.ID.ToString()) //婚姻
                {
                    case "M":
                        marryTextBox.Text = "2";
                        break;
                    case "W":
                        marryTextBox.Text = "4";
                        break;
                    case "A":
                        marryTextBox.Text = "3";
                        break;
                    case "D":
                        marryTextBox.Text = "3";
                        break;
                    case "R":
                        marryTextBox.Text = "2";
                        break;
                    case "S":
                        marryTextBox.Text = "1";
                        break;
                }
                if (myItem.PatientInfo.Profession.ID == "")//职业
                {
                    this.lblWork.Visible = false;
                    this.lblWork1.Visible = false;
                    this.lblWork2.Visible = false;
                    this.workComboBox.Text = "/";
                }
                else
                {
                    this.workComboBox.Tag = myItem.PatientInfo.Profession.ID;
                }

                string work = this.workComboBox.Text.Replace('（', '(');
                int len = work.IndexOf("(");
                if (len > 0)
                {
                    work = work.Substring(0, len).ToString();
                }
                if (work.Length > 10)
                {
                    this.workComboBox.Visible = false;
                    this.lblWork.Visible = false;

                    this.lblWork1.Visible = true;
                    this.lblWork2.Visible = true;

                    this.lblWork1.Text = work.Substring(0, 10);
                    if (work.Length >= 20)
                    {
                        this.lblWork2.Text = work.Substring(10, 10);
                    }
                    else
                    {
                        this.lblWork2.Text = work.Substring(10, work.Length - 10);
                    }

                }
                else
                {
                    this.lblWork1.Visible = false;
                    this.lblWork2.Visible = false;
                    this.workComboBox.Visible = false;
                    this.lblWork.Visible = true;
                    this.lblWork.Text = work;
                    if (this.lblWork.Text.Trim() == "")
                    {
                        this.lblWork.Text = "  / ";
                    }
                }

                if (myItem.PatientInfo.AreaCode == "")//出生地
                {
                    this.birthInComboBox.Text = "  / ";
                }
                else
                {
                    this.birthInComboBox.Text = myItem.PatientInfo.AreaCode;
                }
                if (myItem.PatientInfo.Nationality.ID == "")//民族
                {
                    this.txtNationality.Text = " / ";
                }
                else
                {
                    this.txtNationality.Tag = myItem.PatientInfo.Nationality.ID;
                }
                if (myItem.PatientInfo.Country.ID == "")//国籍
                {
                    this.txtCountry.Text = " / ";
                }
                else
                {
                    this.txtCountry.Tag = myItem.PatientInfo.Country.ID;
                }
                if (myItem.PatientInfo.IDCard == "")
                {
                    this.IDTextBox.Text = "   / ";
                }
                else
                {
                    this.IDTextBox.Text = myItem.PatientInfo.IDCard;//身份证
                }

                if (myItem.PatientInfo.AddressBusiness == "")//工作单位地址
                {
                    this.workAdressTextBox.Text = "   / ";
                }
                else
                {
                    this.workAdressTextBox.Text = myItem.PatientInfo.AddressBusiness;
                }
                if (myItem.PatientInfo.PhoneBusiness == "")//工作单位电话
                {
                    this.workPhoneTextBox.Text = "   / ";
                }
                else
                {
                    this.workPhoneTextBox.Text = myItem.PatientInfo.PhoneBusiness;
                }
                if (myItem.PatientInfo.BusinessZip == "")//邮编　
                {
                    this.workZipTextBox.Text = "   / ";
                }
                else
                {
                    this.workZipTextBox.Text = myItem.PatientInfo.BusinessZip;
                }
                if (myItem.PatientInfo.AddressHome == "")//家庭住址
                {
                    this.homeAdTextBox.Text = "   / ";
                }
                else
                {
                    this.homeAdTextBox.Text = myItem.PatientInfo.AddressHome;
                }
                if (myItem.PatientInfo.PVisit.InSource.ID == "")
                {
                    this.txtPatientInSource.Text = "   / ";
                }
                {
                    this.txtPatientInSource.Tag = myItem.PatientInfo.PVisit.InSource.ID;//病人来源
                }
                if (myItem.PatientInfo.PhoneHome == "")
                {
                    this.neuLabelHomePhone.Text = "   / ";
                }
                else
                {
                    this.neuLabelHomePhone.Text = myItem.PatientInfo.PhoneHome;
                }
                if (myItem.PatientInfo.HomeZip == "")//邮编　
                {
                    this.homeZipTextBox.Text = "   / ";
                }
                else
                {
                    this.homeZipTextBox.Text = myItem.PatientInfo.HomeZip;
                }
                if (myItem.PatientInfo.Kin.Name == "")//联系人名称
                {
                    this.linkNameTextBox.Text = "   / ";
                }
                else
                {
                    this.linkNameTextBox.Text = myItem.PatientInfo.Kin.Name;
                }
                if (myItem.PatientInfo.Kin.RelationLink == "")//联系人关系
                {
                    this.txtRelation.Text = "   / ";
                }
                else
                {
                    this.txtRelation.Tag = myItem.PatientInfo.Kin.RelationLink;
                }

                if (myItem.PatientInfo.Kin.RelationAddress == "")
                {
                    this.linkAdressTextBox.Text = "   / ";
                }
                else
                {
                    this.linkAdressTextBox.Text = myItem.PatientInfo.Kin.RelationAddress; //联系人地址 有待考察;
                }
                if (myItem.PatientInfo.Kin.RelationPhone == "")
                {
                    this.linkPhoneTextBox.Text = "   / ";
                }
                else
                {
                    this.linkPhoneTextBox.Text = myItem.PatientInfo.Kin.RelationPhone; //联系人电话 有待考察;
                }
                this.CYear2.Text = myItem.PatientInfo.PVisit.InTime.Year.ToString();//入院时间年

                this.CMon2.Text = myItem.PatientInfo.PVisit.InTime.Month.ToString();//入院时间月

                this.CDay2.Text = myItem.PatientInfo.PVisit.InTime.Day.ToString();//入院时间日

                this.inDeptComboBox.Tag = myItem.InDept.ID;//入院科室 
                this.inDeptComboBox.Text = myItem.InDept.Name;//入院科室 

                string changeBed = deptMger.QueryWardNoBedNOByInpatienNO(myItem.PatientInfo.ID, "1");
                string WardNoBedNO = deptMger.QueryWardNoBedNOByInpatienNO(changeBed, "2");
                this.neuLabelZKKBBS.Text = WardNoBedNO;//入院病室
             

                //ArrayList NurseList = new ArrayList();
                //NurseList = deptMger.QueryNurseCellNameByInpatienNO(myItem.PatientInfo.ID, "1");
                //if (NurseList == null || NurseList.Count == 0)
                //{
                //    this.neuLabelZKKBBS.Text = myItem.PatientInfo.PVisit.PatientLocation.NurseCell.Name;
                //}
                //else
                //{
                //    foreach (FS.HISFC.Object.RADT.Location Info in NurseList)
                //    {
                //        this.neuLabelZKKBBS.Text = Info.Dept.Name; //入院病区
                //    }
                //}

                //转科信息
                ArrayList deptList = new ArrayList();
                deptList = deptMger.QueryChangeDeptFromShiftApply(myItem.PatientInfo.ID, "2");
                if (deptList.Count == 0)
                {
                    this.neuLabel2ZKKB.Text = "  / ";
                }
                else
                {
                    if (deptList.Count == 1)
                    {
                        this.lblDeptChange1.Visible = false;
                        this.lblDeptChange2.Visible = false;
                        this.neuLabel2ZKKB.Visible = true;
                        FS.HISFC.Models.RADT.Location tempInfo = deptList[0] as FS.HISFC.Models.RADT.Location;
                        this.neuLabel2ZKKB.Text = tempInfo.Dept.Name;

                    }
                    else
                    {
                        string deptChengeName = string.Empty;
                        foreach (FS.HISFC.Models.RADT.Location tempInfo in deptList)
                        {
                            if (deptChengeName == string.Empty)
                            {
                                deptChengeName = tempInfo.Dept.Name;
                            }
                            else
                            {
                                deptChengeName += "->" + tempInfo.Dept.Name;
                            }
                        }
                        if (deptChengeName.Length > 8)
                        {
                            this.lblDeptChange1.Visible = true;
                            this.lblDeptChange2.Visible = true;
                            this.neuLabel2ZKKB.Visible = false;
                            this.lblDeptChange1.Text = deptChengeName.Substring(0, 8);
                            this.lblDeptChange2.Text = deptChengeName.Substring(8);
                        }
                        else
                        {
                            this.lblDeptChange1.Visible = false;
                            this.lblDeptChange2.Visible = false;
                            this.neuLabel2ZKKB.Visible = true;
                            this.neuLabel2ZKKB.Text = deptChengeName;
                        }
                    }
                }
                this.OutYear.Text = myItem.PatientInfo.PVisit.OutTime.Year.ToString();//出院日期
                this.OutMon.Text = myItem.PatientInfo.PVisit.OutTime.Month.ToString();//出院日期
                this.OutDay.Text = myItem.PatientInfo.PVisit.OutTime.Day.ToString();//出院日期
                this.outDeptComboBox.Text = myItem.OutDept.Name;//出院科室

                FS.HISFC.Models.RADT.PatientInfo patientObj = new FS.HISFC.Models.RADT.PatientInfo();
                FS.HISFC.BizLogic.RADT.InPatient inPatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
                patientObj = inPatientMgr.QueryPatientInfoByInpatientNO(myItem.PatientInfo.ID);

                this.neuLabel3CYKBBS.Text = deptMger.QueryWardNoBedNOByInpatienNO(patientObj.PVisit.PatientLocation.Bed.ID, "2"); //出院病室

                //ArrayList AlList = new ArrayList();
                //AlList = deptMger.QueryNurseCellNameByInpatienNO(myItem.PatientInfo.ID, "1");
                //if (AlList == null || AlList.Count == 0)
                //{
                //    this.neuLabel3CYKBBS.Text = myItem.PatientInfo.PVisit.PatientLocation.NurseCell.Name;
                //}
                //else
                //{
                //    foreach (FS.HISFC.Object.RADT.Location Info1 in AlList)
                //    {
                //        this.neuLabel3CYKBBS.Text = Info1.Dept.Name;//出院病区
                //    }
                //}
                if (myItem.InHospitalDays.ToString() == "")//住院天数
                {
                    this.inDaysTextBox.Text = " / ";
                }
                else
                {
                    this.inDaysTextBox.Text = myItem.InHospitalDays.ToString();
                }
                if (myItem.ClinicDiag.Name == "")//门诊诊断
                {
                    this.ClinicDiag.Text = " / ";
                }
                else
                {
                    this.ClinicDiag.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(myItem.ClinicDiag.Name,false);
                }
                if (myItem.ClinicDoc.Name.ToString() == "")
                {
                    this.ClinicDoc.Text = " / ";
                }
                else
                {
                    this.ClinicDoc.Text = myItem.ClinicDoc.Name.ToString();//门（急）诊医生

                }
                if (myItem.PatientInfo.PVisit.Circs.ID.ToString() == "")//入院时情况
                {
                    this.neuLabel21RYQK.Text = " / ";
                }
                else
                {
                    this.neuLabel21RYQK.Text = myItem.PatientInfo.PVisit.Circs.ID.ToString();
                }
                if (myItem.InHospitalDiag.Name.ToString() == "")//入院诊断
                {
                    this.inDiagComboBox.Text = " / ";
                }
                else
                {
                    this.inDiagComboBox.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(myItem.InHospitalDiag.Name,false);
                }

                this.DiagnoseYear.Text = myItem.DiagDate.Year.ToString();//确诊日期
                this.DiagnoseMon.Text = myItem.DiagDate.Month.ToString();//确诊日期
                this.DiagnoseDay.Text = myItem.DiagDate.Day.ToString();//确诊日期

                #region 诊断信息
                FS.HISFC.BizLogic.HealthRecord.Diagnose DiaMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                ArrayList al = new ArrayList();
                al = DiaMgr.QueryCaseDiagnose(myItem.PatientInfo.ID,"%",FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I);//DiaMgr.QueryDiagNoseNew(myItem.PatientInfo.ID);
                if (al == null)
                {

                }
                else
                {
                    if (al.Count > 0)
                    {
                        FS.HISFC.Models.HealthRecord.Diagnose diagNose = new FS.HISFC.Models.HealthRecord.Diagnose();
                        int row = 0;
                        for (int i = 0; i < al.Count; i++)
                        {
                            diagNose = al[i] as FS.HISFC.Models.HealthRecord.Diagnose;
                            if (diagNose.DiagInfo.DiagType.ID != "1" && diagNose.DiagInfo.DiagType.ID!="2")//并发症诊断归入到其他诊断中打印出来
                            {
                                diagNose.DiagInfo.DiagType.ID = "2";
                            }

                            if (i > 0 && diagNose.DiagInfo.DiagType.ID == "1")//防止多个主要诊断打印不出来情况
                            {
                                diagNose.DiagInfo.DiagType.ID = "2";
                            }

                            if (diagNose.DiagInfo.DiagType.ID == "1")
                            {
                                MainDiagName.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name,false);//主要诊断
                                if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                {
                                    mainDiagICD.Text = "";
                                }
                                else
                                {
                                    mainDiagICD.Text = diagNose.DiagInfo.ICD10.ID;
                                }
                                switch (diagNose.DiagOutState)
                                {
                                    case "1":
                                        this.MainDiag1.Text = "√";
                                        break;
                                    case "2":
                                        this.MainDiag2.Text = "√";
                                        break;
                                    case "3":
                                        this.MainDiag3.Text = "√";
                                        break;
                                    case "4":
                                        this.MainDiag4.Text = "√";
                                        break;
                                    case "5":
                                        this.MainDiag5.Text = "√";
                                        break;
                                }
                            }
                            else if (i > 0 && diagNose.DiagInfo.DiagType.ID == "1")
                            {
                                MainDiagName.Text += CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name,false); //存在多个主诊断则加载到一起
                            }
                            else if (diagNose.DiagInfo.DiagType.ID == "2")//其他诊断
                            {
                                row = row + 1;
                                switch (row)
                                {
                                    case 1:
                                        this.otherDiagName1.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name,false); //其他诊断
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.otherDiagCode1.Text = "";
                                        }
                                        else
                                        {
                                            this.otherDiagCode1.Text = diagNose.DiagInfo.ICD10.ID;//其他诊断icd
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.otherDiag11.Text = "√";
                                                break;
                                            case "2":
                                                this.otherDiag12.Text = "√";
                                                break;
                                            case "3":
                                                this.otherDiag13.Text = "√";
                                                break;
                                            case "4":
                                                this.otherDiag14.Text = "√";
                                                break;
                                            case "5":
                                                this.otherDiag15.Text = "√";
                                                break;
                                        }
                                        break;
                                    case 2:
                                        this.otherDiagName2.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name,false);
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.otherDiagCode2.Text = "";
                                        }
                                        else
                                        {
                                            this.otherDiagCode2.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.otherDiag21.Text = "√";
                                                break;
                                            case "2":
                                                this.otherDiag22.Text = "√";
                                                break;
                                            case "3":
                                                this.otherDiag23.Text = "√";
                                                break;
                                            case "4":
                                                this.otherDiag24.Text = "√";
                                                break;
                                            case "5":
                                                this.otherDiag25.Text = "√";
                                                break;
                                        }
                                        break;
                                    case 3:
                                        this.otherDiagName3.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name,false);
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.otherDiagCode3.Text = "";
                                        }
                                        else
                                        {
                                            this.otherDiagCode3.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.otherDiag31.Text = "√";
                                                break;
                                            case "2":
                                                this.otherDiag32.Text = "√";
                                                break;
                                            case "3":
                                                this.otherDiag33.Text = "√";
                                                break;
                                            case "4":
                                                this.otherDiag34.Text = "√";
                                                break;
                                            case "5":
                                                this.otherDiag35.Text = "√";
                                                break;
                                        }
                                        break;
                                    case 4:
                                        this.otherDiagName4.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.otherDiagCode4.Text = "";
                                        }
                                        else
                                        {
                                            this.otherDiagCode4.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.otherDiag41.Text = "√";
                                                break;
                                            case "2":
                                                this.otherDiag42.Text = "√";
                                                break;
                                            case "3":
                                                this.otherDiag43.Text = "√";
                                                break;
                                            case "4":
                                                this.otherDiag44.Text = "√";
                                                break;
                                            case "5":
                                                this.otherDiag45.Text = "√";
                                                break;
                                        }
                                        break;
                                    case 5:
                                        this.otherDiagName5.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.otherDiagCode5.Text = "";
                                        }
                                        else
                                        {
                                            this.otherDiagCode5.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.otherDiag51.Text = "√";
                                                break;
                                            case "2":
                                                this.otherDiag52.Text = "√";
                                                break;
                                            case "3":
                                                this.otherDiag53.Text = "√";
                                                break;
                                            case "4":
                                                this.otherDiag54.Text = "√";
                                                break;
                                            case "5":
                                                this.otherDiag55.Text = "√";
                                                break;
                                        }
                                        break;
                                    case 6:
                                        this.otherDiagName6.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.otherDiagCode6.Text = "";
                                        }
                                        else
                                        {
                                            this.otherDiagCode6.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.otherDiag61.Text = "√";
                                                break;
                                            case "2":
                                                this.otherDiag62.Text = "√";
                                                break;
                                            case "3":
                                                this.otherDiag63.Text = "√";
                                                break;
                                            case "4":
                                                this.otherDiag64.Text = "√";
                                                break;
                                            case "5":
                                                this.otherDiag65.Text = "√";
                                                break;
                                        }
                                        break;
                                    case 7:
                                        this.otherDiagName7.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.otherDiagCode7.Text = "";
                                        }
                                        else
                                        {
                                            this.otherDiagCode7.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.otherDiag71.Text = "√";
                                                break;
                                            case "2":
                                                this.otherDiag72.Text = "√";
                                                break;
                                            case "3":
                                                this.otherDiag73.Text = "√";
                                                break;
                                            case "4":
                                                this.otherDiag74.Text = "√";
                                                break;
                                            case "5":
                                                this.otherDiag75.Text = "√";
                                                break;
                                        }
                                        break;
                                    case 8:
                                        this.otherDiagName8.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.otherDiagCode8.Text = "";
                                        }
                                        else
                                        {
                                            this.otherDiagCode8.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.otherDiag81.Text = "√";
                                                break;
                                            case "2":
                                                this.otherDiag82.Text = "√";
                                                break;
                                            case "3":
                                                this.otherDiag83.Text = "√";
                                                break;
                                            case "4":
                                                this.otherDiag84.Text = "√";
                                                break;
                                            case "5":
                                                this.otherDiag85.Text = "√";
                                                break;
                                        }
                                        break;
                                    case 9:
                                        this.otherDiagName9.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                        if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                        {
                                            this.otherDiagCode9.Text = "";
                                        }
                                        else
                                        {
                                            this.otherDiagCode9.Text = diagNose.DiagInfo.ICD10.ID;
                                        }
                                        switch (diagNose.DiagOutState)
                                        {
                                            case "1":
                                                this.otherDiag91.Text = "√";
                                                break;
                                            case "2":
                                                this.otherDiag92.Text = "√";
                                                break;
                                            case "3":
                                                this.otherDiag93.Text = "√";
                                                break;
                                            case "4":
                                                this.otherDiag94.Text = "√";
                                                break;
                                            case "5":
                                                this.otherDiag95.Text = "√";
                                                break;
                                        }
                                        break;
                                }
                            }
                            else if (diagNose.DiagInfo.DiagType.ID == "4")//医院感染诊断
                            {
                                txtInfectionPosition.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                                if (diagNose.DiagInfo.ICD10.ID == "MS999")
                                {
                                    this.neuLabel30ICD.Text = "";
                                }
                                else
                                {
                                    neuLabel30ICD.Text = diagNose.DiagInfo.ICD10.ID;
                                }
                                switch (diagNose.DiagOutState)
                                {
                                    case "1":
                                        this.neuLabel34ZY.Text = "√";
                                        break;
                                    case "2":
                                        this.neuLabel35WY.Text = "√";
                                        break;
                                    case "3":
                                        this.neuLabel33WY.Text = "√";
                                        break;
                                    case "4":
                                        this.neuLabel32SW.Text = "√";
                                        break;
                                    case "5":
                                        this.neuLabel31QT.Text = "√";
                                        break;
                                }
                            }
                            else if (diagNose.DiagInfo.DiagType.ID == "6")//病理诊断
                            {
                                txtDiagClPa.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(diagNose.DiagInfo.ICD10.Name, false);
                            }
                        }
                    }
                }
                #endregion

                if (this.txtInfectionPosition.Text.Trim() == "")
                {
                    if (myItem.InfectionPosition.Name == null || myItem.InfectionPosition.Name == "")//医院感染名称
                    {
                        this.txtInfectionPosition.Text = " / ";
                    }
                    else
                    {
                        this.txtInfectionPosition.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(myItem.InfectionPosition.Name,false);
                    }
                }

                if (myItem.PathologicalDiagName != "")
                {
                    this.txtDiagClPa.Text = myItem.PathologicalDiagName;
                }
                else
                {
                    this.txtDiagClPa.Text = "/";
                }
                //if (txtDiagClPa.Text.Trim() == "")//保留原来的赋值
                //{
                //    if (myItem.PatientInfo.User02 ==null  || myItem.PatientInfo.User02 == "")//病理诊断
                //    {
                //        this.txtDiagClPa.Text = " / ";
                //    }
                //    else
                //    {
                //        this.txtDiagClPa.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(myItem.PatientInfo.User02,false);
                //    }
                //}
                if (myItem.InjuryOrPoisoningCause==null || myItem.InjuryOrPoisoningCause == "")//损伤、中毒的外部因素
                {
                    this.OutsideCausetxt.Text = " 未发现 ";
                }
                else
                {
                    this.OutsideCausetxt.Text = CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(myItem.InjuryOrPoisoningCause,false);
                }
                if (myItem.FirstAnaphyPharmacy.ID == null || myItem.FirstAnaphyPharmacy.ID == "")
                {
                    this.txtAnaphyFlag.Text = " / ";
                }
                else
                {
                    this.txtAnaphyFlag.Text = myItem.FirstAnaphyPharmacy.ID.ToString();//药物过敏
                }
                //if (myItem.SecondAnaphyPharmacy.ID != null && myItem.SecondAnaphyPharmacy.ID != "")
                //{
                //    this.txtAnaphyFlag.Text += "；" + myItem.SecondAnaphyPharmacy.ID;
                //}
                if (this.txtAnaphyFlag.Text.Trim() == "")//兼容原来的
                {
                    if (myItem.FirstAnaphyPharmacy.Name == null || myItem.FirstAnaphyPharmacy.Name == "")
                    {
                        this.txtAnaphyFlag.Text = " / ";
                    }
                    else
                    {
                        this.txtAnaphyFlag.Text = myItem.FirstAnaphyPharmacy.Name.ToString();//药物过敏
                    }
                    //if (myItem.SecondAnaphyPharmacy.Name != null && myItem.SecondAnaphyPharmacy.Name != "")
                    //{
                    //    this.txtAnaphyFlag.Text += "；" + myItem.SecondAnaphyPharmacy.Name;
                    //}
                }
                if (myItem.Hbsag==null || myItem.Hbsag == "")
                {
                    this.HbsAg.Text = "0";
                }
                else
                {
                    this.HbsAg.Text = myItem.Hbsag;//HBsAg
                }
                if (myItem.HcvAb==null || myItem.HcvAb == "")
                {
                    this.HCVAb.Text = "0";
                }
                else
                {
                    this.HCVAb.Text = myItem.HcvAb;//HCV-Ab
                }
                if (myItem.HivAb==null || myItem.HivAb == "")
                {
                    this.HIVAb.Text = "0";
                }
                else
                {
                    this.HIVAb.Text = myItem.HivAb;//HIV-Ab
                }

                if (myItem.CePi==null || myItem.CePi == "")
                {
                    this.txtCepi.Text = "0";
                }
                else
                {
                    this.txtCepi.Text = myItem.CePi;//门诊与出院
                }

                if (myItem.PiPo==null || myItem.PiPo == "")
                {
                    this.txtPiPo.Text = "0";
                }
                else
                {
                    this.txtPiPo.Text = myItem.PiPo;//入院与出院
                }

                if (myItem.OpbOpa==null || myItem.OpbOpa == "")
                {
                    this.txtOpbOpa.Text = "0";
                }
                else
                {
                    this.txtOpbOpa.Text = myItem.OpbOpa;//术前与术后
                }

                if (myItem.ClPa==null || myItem.ClPa == "")
                {
                    this.txtClPa.Text = "0";
                }
                else
                {
                    this.txtClPa.Text = myItem.ClPa;//临床与病理

                }

                if (myItem.FsBl==null || myItem.FsBl == "")
                {
                    this.txtFsBl.Text = "0";
                }
                else
                {
                    this.txtFsBl.Text = myItem.FsBl;//放射与病理
                }

                if (myItem.SalvTimes.ToString() == "")
                {
                    this.txtSalvTimes.Text = "0";
                }
                else
                {
                    this.txtSalvTimes.Text = myItem.SalvTimes.ToString();//抢救次数
                }
                if (myItem.SuccTimes.ToString() == "")
                {
                    this.txtSuccTimes.Text = "0";
                }
                else
                {
                    this.txtSuccTimes.Text = myItem.SuccTimes.ToString();//抢救成功次数
                }
                this.txtDeptChiefDonm.Text = myItem.PatientInfo.PVisit.ReferringDoctor.Name.ToString();//科主任
                this.txtChiefDocName.Text = myItem.PatientInfo.PVisit.ConsultingDoctor.Name.ToString();//主（副）主任医师
                this.txtChargeDocName.Text = myItem.PatientInfo.PVisit.AttendingDoctor.Name.ToString();//主治医师
                this.txtHouseDocName.Text = myItem.PatientInfo.PVisit.AdmittingDoctor.Name.ToString();//住院医师
                if (myItem.RefresherDoc.Name==null || myItem.RefresherDoc.Name == "")
                {
                    this.RefresherDoc.Text = "/";
                }
                else
                {
                    this.RefresherDoc.Text = myItem.RefresherDoc.Name.ToString();//进修医师
                }
                if (myItem.GraduateDoc.Name==null || myItem.GraduateDoc.Name == "")
                {
                    this.txtGraDocName.Text = "/";
                }
                else
                {
                    this.txtGraDocName.Text = myItem.GraduateDoc.Name.ToString();//研究生实习医师

                }
                if (myItem.PatientInfo.PVisit.TempDoctor.Name==null || myItem.PatientInfo.PVisit.TempDoctor.Name == "")
                {
                    this.txtPraDocCode.Text = "/";
                }
                else
                {
                    this.txtPraDocCode.Text = myItem.PatientInfo.PVisit.TempDoctor.Name.ToString();//实习医师
                }
                if (myItem.CodingOper.Name == null || myItem.CodingOper.Name == "")
                {
                    this.txtCodingCode.Text = "/";
                }
                else
                {
                    this.txtCodingCode.Text = myItem.CodingOper.Name.ToString();//编码员

                }
                if (myItem.MrQuality==null || myItem.MrQuality== "")
                {
                    this.MrQuality.Text = "/";
                }
                else
                {
                    this.MrQuality.Text = myItem.MrQuality.ToString();//病案质量
                }
                if (myItem.QcDoc.Name ==null || myItem.QcDoc.Name == "")
                {
                    this.txtQcDocName.Text = "/";
                }
                else
                {
                    this.txtQcDocName.Text = myItem.QcDoc.Name;//质控医生
                }
                if (myItem.QcNurse.Name == null || myItem.QcNurse.Name == "")
                {
                    this.QcNurseName.Text = "/";
                }
                else
                {
                    this.QcNurseName.Text = myItem.QcNurse.Name.ToString();//质控护士
                }
                this.CheckDateYear.Text = myItem.CheckDate.Year.ToString();//质控日期
                this.CheckDateMon.Text = myItem.CheckDate.Month.ToString();//质控日期
                this.CheckDateDay.Text = myItem.CheckDate.Day.ToString();//质控日期
                #region 暂时不用 打印的时候 不需要打印人员
                #endregion

                //if (myItem.PatientInfo.PVisit.OutTime.Year > 1995)
                //{
                //    int inDays = (int)new System.TimeSpan(myItem.PatientInfo.PVisit.OutTime.Ticks
                //   - myItem.PatientInfo.PVisit.InTime.Ticks).TotalDays;

                //    inDays = (myItem.PatientInfo.PVisit.OutTime.Date - myItem.PatientInfo.PVisit.InTime.Date).Days;

                //    if (inDays <= 0)
                //        inDays = 1;

                //    if (inDays > 0)
                //        this.inDaysTextBox.Text = inDays.ToString();//住院天数
                //}
                //else
                //{
                //    this.inDaysTextBox.Text = "1";
                //}
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.TargetSite + "******" + eee.StackTrace);
            }
            //if (this.OutYear.Text != "1")
            //{
            //    int inDays = (int)new System.TimeSpan(myItem.PatientInfo.PVisit.OutTime.Ticks
            //        - myItem.PatientInfo.PVisit.InTime.Ticks).TotalDays;
            //    if (inDays > 0)
            //        this.inDaysTextBox.Text = inDays.ToString();//住院天数
            //}
            //else
            //{
            //    this.inDaysTextBox.Text = "";
            //}
            //this.inSourceComboBox.Tag = myItem.PatientInfo.PVisit.InSource.ID;//入院来源

            #endregion

            ////保存转科信息的列表
            //ArrayList changeDept = new ArrayList();
            ////获取转科信息
            //changeDept = deptMger.QueryChangeDeptFromShiftApply(myItem.PatientInfo.ID, "2");
            //LoadChangeDept(changeDept);

            //ArrayList alOrg = new ArrayList();
            //FS.HISFC.Management.HealthRecord.Diagnose diag = new FS.HISFC.Management.HealthRecord.Diagnose();
            //alOrg = diag.QueryCaseDiagnose(myItem.PatientInfo.ID, "%", FS.HISFC.Object.HealthRecord.EnumServer.frmTypes.DOC);
        }

        private void Clear()
        {
            this.label3.Text = "";
            this.payKindCbx.Text = "";//医疗付款方式
            this.txtInHosNo.Text = ""; //住院次数
            this.InpatientNO.Text = "";//病案号
            this.nameTextBox.Text = "";//姓名
            sexComboBox.Text = "";//性别
            this.birYear.Text = "";//出生日期
            this.birMon.Text = "";//出生日期
            this.birDay.Text = "";//出生日期
            this.ageTextBox.Text = "";//年龄 
            marryTextBox.Text = ""; //婚姻
            this.workComboBox.Text = "";//职业
            this.birthInComboBox.Text = "";//出生地
            this.txtNationality.Text = "";//民族
            this.txtCountry.Text = "";//国籍
            this.IDTextBox.Text = "";//身份证
            this.workAdressTextBox.Text = "";//工作单位地址
            this.workPhoneTextBox.Text = "";//工作单位电话
            this.workZipTextBox.Text = "";//邮编　
            this.homeAdTextBox.Text = "";//家庭住址
            this.txtPatientInSource.Tag = "";//病人来源
            this.neuLabelHomePhone.Text = "";
            this.homeZipTextBox.Text = "";//邮编　
            this.linkNameTextBox.Text = "";//联系人名称
            this.txtRelation.Text = "";//联系人关系
            this.linkAdressTextBox.Text = ""; //联系人地址 有待考察;
            this.linkPhoneTextBox.Text = ""; //联系人电话 有待考察;
            this.CYear2.Text = "";//入院时间年
            this.CMon2.Text = "";//入院时间月
            this.CDay2.Text = "";//入院时间日
            this.inDeptComboBox.Tag = "";//入院科室 
            this.inDeptComboBox.Text = "";//入院科室 
            this.neuLabelZKKBBS.Text = "";
            //转科信息
            this.neuLabel2ZKKB.Text = "";
            this.OutYear.Text = "";//出院日期
            this.OutMon.Text = "";//出院日期
            this.OutDay.Text = "";//出院日期
            this.outDeptComboBox.Text = "";//出院科室
            //出院病床：
            this.neuLabel3CYKBBS.Text = "";
            this.inDaysTextBox.Text = "";//住院天数
            this.ClinicDiag.Text = "";//门诊诊断
            this.ClinicDoc.Text = "";//门（急）诊医生
            this.neuLabel21RYQK.Text = "";//入院时情况
            this.inDiagComboBox.Text = "";//入院诊断
            this.DiagnoseYear.Text = "";//确诊日期
            this.DiagnoseMon.Text = "";//确诊日期
            this.DiagnoseDay.Text = "";//确诊日期
            MainDiagName.Text = "";//主要诊断
            this.MainDiag1.Text = "";
            this.MainDiag2.Text = "";
            this.MainDiag3.Text = "";
            this.MainDiag4.Text = "";
            this.MainDiag5.Text = "";
            this.otherDiagName1.Text = ""; //其他诊断
            this.otherDiagCode1.Text = ""; //-----
            this.otherDiag11.Text = "";
            this.otherDiag12.Text = "";
            this.otherDiag13.Text = "";
            this.otherDiag14.Text = "";
            this.otherDiag15.Text = "";
            this.otherDiagName2.Text = ""; //其他诊断
            this.otherDiagCode2.Text = ""; //-----
            this.otherDiag21.Text = "";
            this.otherDiag22.Text = "";
            this.otherDiag23.Text = "";
            this.otherDiag24.Text = "";
            this.otherDiag25.Text = "";
            this.otherDiagName3.Text = ""; //其他诊断
            this.otherDiagCode3.Text = ""; //-----
            this.otherDiag31.Text = "";
            this.otherDiag32.Text = "";
            this.otherDiag33.Text = "";
            this.otherDiag34.Text = "";
            this.otherDiag35.Text = "";
            this.otherDiagName4.Text = ""; //其他诊断
            this.otherDiagCode4.Text = ""; //-----
            this.otherDiag41.Text = "";
            this.otherDiag42.Text = "";
            this.otherDiag43.Text = "";
            this.otherDiag44.Text = "";
            this.otherDiag45.Text = "";
            this.otherDiagName5.Text = ""; //其他诊断
            this.otherDiagCode5.Text = ""; //-----
            this.otherDiag51.Text = "";
            this.otherDiag52.Text = "";
            this.otherDiag53.Text = "";
            this.otherDiag54.Text = "";
            this.otherDiag55.Text = "";
            this.otherDiagName6.Text = ""; //其他诊断
            this.otherDiagCode6.Text = ""; //-----
            this.otherDiag61.Text = "";
            this.otherDiag62.Text = "";
            this.otherDiag63.Text = "";
            this.otherDiag64.Text = "";
            this.otherDiag65.Text = "";

            this.otherDiagName7.Text = "";
            this.otherDiagCode7.Text = "";

            this.otherDiag71.Text = "";

            this.otherDiag72.Text = "";

            this.otherDiag73.Text = "";

            this.otherDiag74.Text = "";

            this.otherDiag75.Text = "";

            this.otherDiagName8.Text = "";
            this.otherDiagCode8.Text = "";

            this.otherDiag81.Text = "";

            this.otherDiag82.Text = "";

            this.otherDiag83.Text = "";

            this.otherDiag84.Text = "";

            this.otherDiag85.Text = "";

            this.otherDiagName9.Text = "";
            this.otherDiagCode9.Text = "";

            this.otherDiag91.Text = "";

            this.otherDiag92.Text = "";

            this.otherDiag93.Text = "";

            this.otherDiag94.Text = "";

            this.otherDiag95.Text = "";


            this.txtInfectionPosition.Text = "";//医院感染名称
            neuLabel34ZY.Text = "";
            neuLabel35WY.Text = "";
            neuLabel33WY.Text = "";
            neuLabel32SW.Text = "";
            neuLabel31QT.Text = "";
            neuLabel30ICD.Text = "";
            this.txtDiagClPa.Text = "";//病理诊断
            this.OutsideCausetxt.Text = "";//损伤、中毒的外部因素
            this.txtAnaphyFlag.Text = "";//药物过敏
            this.HbsAg.Text = "";//HBsAg
            this.HCVAb.Text = "";//HCV-Ab
            this.HIVAb.Text = "";//HIV-Ab
            this.txtCepi.Text = "";//门诊与出院
            this.txtPiPo.Text = "";//入院与出院
            this.txtOpbOpa.Text = "";//术前与术后
            this.txtClPa.Text = "";//临床与病理
            this.txtFsBl.Text = "";//放射与病理
            this.txtSalvTimes.Text = "";//抢救次数
            this.txtSuccTimes.Text = "";//抢救成功次数
            this.txtDeptChiefDonm.Text = "";//科主任
            this.txtChiefDocName.Text = "";//主（副）主任医师
            this.txtChargeDocName.Text = "";//主治医师
            this.txtHouseDocName.Text = "";//住院医师
            this.RefresherDoc.Text = "";//进修医师

            this.txtGraDocName.Text = "";//研究生实习医师

            this.txtPraDocCode.Text = "";//实习医师

            this.txtCodingCode.Text = "";//编码员

            this.MrQuality.Text = "";//病案质量

            this.txtQcDocName.Text = "";//质控医生

            this.QcNurseName.Text = "";//质控护士
            this.CheckDateYear.Text = "";//质控日期
            this.CheckDateMon.Text = "";//质控日期
            this.CheckDateDay.Text = "";//质控日期
            this.inDaysTextBox.Text = "";

            this.lblDeptChange1.Text = "";
            this.lblDeptChange1.Visible = false;
            this.lblDeptChange2.Text = "";
            this.lblDeptChange2.Visible = false;

        }
        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface.Reset()
        {
            payKindCbx.Tag = "";//付费方式、
            InpatientNO.Text = "";//住院流水号
            //this.medCardTextBox.Text = "";//社保号

            txtInHosNo.Text = "";//入院次数
            //this.inpatientNOTextBox.Text = "";//住院号

            this.nameTextBox.Text = "";//姓名
            this.sexComboBox.Tag = "";//性别
            this.birYear.Text = "";//出生日期
            this.birMon.Text = "";//出生日期
            this.birDay.Text = "";//出生日期
            this.ageTextBox.Text = "";//年龄
            //this.marryComboBox.Tag = "";//婚姻状况
            //this.workComboBox.Tag = "";//职业
            this.birthInComboBox.Tag = ""; //出生地
            //this.nationComboBox.Tag = "";//民族
            //this.districtComboBox.Text = ""; //籍贯
            this.IDTextBox.Text = "";//身份证
            this.workAdressTextBox.Text = ""; ;//工作单位地址
            this.workPhoneTextBox.Text = "";//工作单位电话
            this.workZipTextBox.Text = "";//邮编　
            this.homeAdTextBox.Text = "";//家庭住址
            this.homeZipTextBox.Text = "";//邮编　
            this.linkNameTextBox.Text = "";//联系人名称
            //this.relationComboBox.Tag = "";//联系人关系
            this.linkAdressTextBox.Text = ""; //联系人地址 有待考察;
            this.linkPhoneTextBox.Text = ""; //联系人电话 有待考察;\
            inDaysTextBox.Text = "";
        }
        /// <summary>
        /// 屏蔽不打印的控件
        /// </summary>
        private void SetVisible()
        {
            #region
            //this.label3.Visible = false;//
            //this.neuLabelZYBASY.Visible = false;//
            //this.neuLabelPayKind.Visible = false;//
            //this.neuLabelNum1.Visible = false;//
            //this.neuPanel7.Visible = false;//
            //this.neuLabelNum2.Visible = false;//
            //this.neuLabelPatientNo.Visible = false;//
            //this.neuPanel6.Visible = false;//
            //this.neuPanel1.Visible = false;

            //this.neuLabelName.Visible = false;//
            //this.neuPanel2.Visible = false;//
            //this.neuLabelSex.Visible = false;//
            //this.neuLabelSex2.Visible = false;//
            //this.neuLabelBir.Visible = false;//
            //this.neuPanel12.Visible = false;//
            //this.neuLabelBirYear.Visible = false;//
            //this.neuPanel11.Visible = false;//
            //this.neuLabelbirMon.Visible = false;//
            //this.neuPanel10.Visible = false;//
            //this.neuLabelBirDay.Visible = false;//
            //this.neuLabelAge.Visible = false;//
            //this.neuPanel9.Visible = false;//
            //this.neuLabelMarry.Visible = false;//
            //this.neuLabelMarry2.Visible = false;//
            //this.neuLabelJob.Visible = false;//
            //this.neuPanel8.Visible = false;//
            //this.neuLabelBirArea.Visible = false;//
            //this.neuPanel14.Visible = false;//
            //this.neuLabelArea.Visible = false;//
            //this.neuLabelNation.Visible = false;//
            //this.neuPanel13.Visible = false;//
            //this.neuLabelCountry.Visible = false;//
            //this.neuPanel15.Visible = false;//
            //this.neuLabelCardId.Visible = false;//
            //this.neuPanel16.Visible = false;//
            //this.neuLabelWorkAdr.Visible = false;//
            //this.neuPanel17.Visible = false;//
            //this.neuLabelTel.Visible = false;//
            //this.neuPanel18.Visible = false;//
            //this.neuLabelYb.Visible = false;//
            //this.neuPanel19.Visible = false;//
            //this.neuLabelHkdz.Visible = false;//
            //this.neuPanel20.Visible = false;//
            
          
            //this.neuLabelYouB.Visible = false;//
            //this.neuPanel22.Visible = false;//
            //this.neuLabelLXR.Visible = false;//
            //this.neuPanel23.Visible = false;//
            //this.neuLabelGX.Visible = false;//
            //this.neuPanel24.Visible = false;//
            //this.neuLabelDZ.Visible = false;//
            //this.neuPanel25.Visible = false;//
            //this.neuLabelDH.Visible = false;//
            //this.neuPanel26.Visible = false;//

            //this.neuLabelRYRQ.Visible = false;//
            //this.neuPanel27.Visible = false;//
            //this.neuLabelNian.Visible = false;//
            //this.neuPanel28.Visible = false;//
            //this.neuLabelYu.Visible = false;//
            //this.neuPanel29.Visible = false;//
            //this.neuLabelRi.Visible = false;//
            //this.neuLabelRykb.Visible = false;//
            //this.neuPanel30.Visible = false;//
            //this.neuLabelBs.Visible = false;//
            //this.neuPanel31.Visible = false;//


          
            //this.neuLabelZKKB.Visible = false;//
            //this.neuPanel35.Visible = false;//

            //this.neuLabelCYRQ.Visible = false;//
            //this.neuPanel36.Visible = false;//
            //this.neuLabelCYN.Visible = false;//
            //this.neuPanel37.Visible = false;//
            //this.neuLabelCYY.Visible = false;//
            //this.neuPanel38.Visible = false;//
            //this.neuLabelCYR.Visible = false;//
            //this.neuLabelCYKB.Visible = false;//
            //this.neuPanel39.Visible = false;//
            //this.neuLabelCYBS.Visible = false;//
            //this.neuPanel41.Visible = false;//
            //this.neuLabelSJZYTS.Visible = false;//
            //this.neuPanel40.Visible = false;//
            //this.neuLabelTian.Visible = false;//

            //this.neuLabelMJZZD.Visible = false;//
            //this.neuPanel42.Visible = false;//
            //this.neuLabelMJZYS.Visible = false;//
            //this.neuPanel43.Visible = false;//
            //this.neuLabelRYQK.Visible = false;//
            //this.neuLabelRYQK2.Visible = false;//

            //this.neuLabelRYZD.Visible = false;//
            //this.neuPanel44.Visible = false;//
            //this.neuLabelRYQZ.Visible = false;//
            //this.neuPanel47.Visible = false;//
            //this.neuLabelRYQZN.Visible = false;//
            //this.neuPanel46.Visible = false;//
            //this.neuLabelRYQZY.Visible = false;//
            //this.neuPanel45.Visible = false;//
            //this.neuLabelRYQZR.Visible = false;//

            //this.neuPanel1Diag.Visible = false;//
            //this.neuLabelCYZD.Visible = false;//
            //this.neuLabelCYQK.Visible = false;//
            //this.neuLabel1ZY.Visible = false;//
            //this.neuLabel2HZ.Visible = false;//
            //this.neuLabel3WY.Visible = false;//
            //this.neuLabel4SW.Visible = false;//
            //this.neuLabel5QT.Visible = false;//
            //this.neuLabelICD.Visible = false;//
            //this.neuPanel3Diag.Visible = false;//
            //this.neuPanel4Diag.Visible = false;//
            //this.neuPanel5Diag.Visible = false;
            //this.neuPanel7Diag.Visible = false;//
            //this.neuPanel8Diag.Visible = false;//
            //this.neuPanel9Diag.Visible = false;//
            //this.neuPanel10Diag.Visible = false;//
            //this.neuLabelZYZD.Visible = false;//
            //this.neuPanel2Diag.Visible = false;//
            //this.neuPanel11Diag.Visible = false;//
            //this.neuLabelQTZD.Visible = false;//
            //this.neuPanel12Diag.Visible = false;//
            //this.neuPanel13Diag.Visible = false;//
            //this.neuPanel14Diag.Visible = false;//
            //this.neuPanel15Diag.Visible = false;//
            //this.neuPanel16Diag.Visible = false;//
            //this.neuPanel17Diag.Visible = false;//
            //this.neuPanel6Diag.Visible = false;//
            //this.neuLabelYYGRMC.Visible = false;//
            //this.neuPanel18Diag.Visible = false;//
            //this.neuLabelBLZD.Visible = false;//
            //this.neuPanel54.Visible = false;//
            //this.neuLabelSSZDDWBYY.Visible = false;//
            //this.neuPanel53.Visible = false;//

            //this.neuLabelYWGM.Visible = false;//
            //this.neuLabelHbsAg.Visible = false;//
            //this.neuLabelHVCAb.Visible = false;//
            //this.neuLabelHIVAb.Visible = false;//
            //this.neuLabelJYJG.Visible = false;//
            //this.neuPanel52.Visible = false;

            //this.neuLabelZDFHQK.Visible = false;
            //this.neuLabelMZYCY.Visible = false;
            //this.neuLabelRYYCY.Visible = false;
            //this.neuLabelSQYSH.Visible = false;
            //this.neuLabelLCYBL.Visible = false;
            //this.neuPanel51.Visible = false;
            //this.neuLabelFSYBL.Visible = false;
            //this.neuLabelFHQKMX.Visible = false;
            //this.neuLabelQJ.Visible = false;
            //this.neuPanel56.Visible = false;
            //this.neuLabelQJC.Visible = false;
            //this.neuLabelCG.Visible = false;
            //this.neuPanel55.Visible = false;
            //this.neuLabelCGC.Visible = false;
            //this.neuPanel50.Visible = false;
            //this.neuLabelKZR.Visible = false;
            //this.neuLabelZFZRYS.Visible = false;
            //this.neuLabelZZYS.Visible = false;
            //this.neuLabelZYYS.Visible = false;
            //this.neuPanel49.Visible = false;
            //this.neuLabelJXYS.Visible = false;
            //this.neuLabelYJSSSYS.Visible = false;
            //this.neuLabelSXYS.Visible = false;
            //this.neuLabelBMY.Visible = false;
            //this.neuPanel48.Visible = false;
            //this.neuLabelBAZL.Visible = false;
            //this.neuLabelBAZLMX.Visible = false;
            //this.neuLabelZKYS.Visible = false;
            //this.neuLabelZKHS.Visible = false;
            //this.neuLabelZKRQ.Visible = false;
            //this.neuPanel59.Visible = false;
            //this.neuLabelZKRQN.Visible = false;
            //this.neuPanel58.Visible = false;
            //this.neuLabelZKRQY.Visible = false;
            //this.neuPanel57.Visible = false;
            //this.neuLabelZKRQR.Visible = false;
            //this.neuPanel5.Visible = false;
            //this.label7.Visible = false;
            //this.label8.Visible = false;

            //this.neuPanel3.Visible = false;
            //this.neuPanel4.Visible = false;

            ////住院处打印的字段：

            //this.payKindCbx.Visible = false;
            //this.txtInHosNo.Visible = false;
            //this.InpatientNO.Visible = false;
            //this.nameTextBox.Visible = false;
            //this.sexComboBox.Visible = false;
            //this.birYear.Visible = false;
            //this.birMon.Visible = false;
            //this.birDay.Visible = false;
            //this.ageTextBox.Visible = false;
            //this.marryTextBox.Visible = false;
            //this.workComboBox.Visible = false;
            //this.birthInComboBox.Visible = false;
            //this.txtNationality.Visible = false;
            //this.txtCountry.Visible = false;
            //this.IDTextBox.Visible = false;
            //this.workAdressTextBox.Visible = false;
            //this.workPhoneTextBox.Visible = false;
            //this.workZipTextBox.Visible = false;
            //this.homeAdTextBox.Visible = false;
         
            //this.homeZipTextBox.Visible = false;
            //this.linkNameTextBox.Visible = false;
            //this.txtRelation.Visible = false;
            //this.linkAdressTextBox.Visible = false;
            //this.linkPhoneTextBox.Visible = false;
            //this.CYear2.Visible = false;
            //this.CMon2.Visible = false;
            //this.CDay2.Visible = false;
            //this.inDeptComboBox.Visible = false;
            //this.neuLabelZKKBBS.Visible = false;
            //this.neuLabel2ZKKB.Visible = false;
            
            //this.neuLabel21RYQK.Visible = false;//入院情况不打印

            //this.neuLabelryqkw.Visible = false;
            //this.neuLabelryqkj.Visible = false;
            //this.neuLabelryqkyb.Visible = false;
            #endregion

            
            this.neuLabelLaiYuan.Visible = false;//病人来源
            //this.neuPanel21.Visible = false;//病人来源下划线
            this.txtPatientInSource.Visible = false;//病人来源不打印
            this.neuLabelZYRQ.Visible = false;//转科日期
            this.neuLabel10ZKRQ.Visible = false;//转科日期不打印
            this.neuLabel9ZKRQY.Visible = false;//转科日期不打印
            this.neuLabel8ZKRQR.Visible = false;//转科日期不打印
            this.neuPanel32.Visible = false;//转科日期年下划线
            this.neuLabelZYN.Visible = false;//转科日期年
            this.neuPanel33.Visible = false;//转科日期月下划线
            this.neuLabelZYY.Visible = false;//转科日期月
            this.neuPanel34.Visible = false;//转科日期日下划线
            this.neuLabelZYR.Visible = false;//转科日期日
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
            return p.PrintPage(20, 10, this);
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
        int FS.FrameWork.WinForms.Forms.IReportPrinter.PrintPreview()
        {
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            this.PreviewZoom();
            if (this.isPrintViewZoom)
            {
                p.PreviewZoomFactor = 1;
            }
            return p.PrintPreview(20, 10, this);
        }

        #endregion

    }
}
