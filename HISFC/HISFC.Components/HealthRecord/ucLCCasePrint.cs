using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections; 

namespace FS.HISFC.Components.HealthRecord
{
    public partial class ucLCCasePrint : UserControl, FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface
    {
        public ucLCCasePrint()
        {
            InitializeComponent();
            LoadInfo();
        }

        #region 私有函数
        /// <summary>
        /// 加载前三次转科信息
        /// </summary>
        /// <param name="list"></param>
        private void LoadChangeDept(ArrayList list)
        {
            FS.HISFC.Models.RADT.Location firDept = null;
            FS.HISFC.Models.RADT.Location secDept = null;
            FS.HISFC.Models.RADT.Location thirDept = null;

            if (list == null)
            {
                return;
            }

            #region 转科信息的前三个在界面上显示
            if (list.Count > 0) //有转科信息
            {
                //转科信息的前三个在界面上显示
                int j = 0;
                if (list.Count >= 3)
                {
                    j = 3;
                }
                else
                {
                    j = list.Count;
                }
                for (int i = 0; i < j; i++)
                {
                    switch (i)
                    {
                        case 0:
                            firDept = (FS.HISFC.Models.RADT.Location)list[0];
                            break;
                        case 1:
                            secDept = (FS.HISFC.Models.RADT.Location)list[1];
                            break;
                        case 2:
                            thirDept = (FS.HISFC.Models.RADT.Location)list[2];
                            break;
                    }
                }
            }
            #endregion

            #region 转科信息
            if (firDept != null)
            {
                //changeDeptFirstComboBox.Text = firDept.Dept.Name;
                //changeDeptFirstComboBox.Tag = firDept.Dept.ID;
                System.DateTime dd = FS.FrameWork.Function.NConvert.ToDateTime(firDept.User01);
                this.CYear2.Text = dd.Year.ToString();
                this.CMon2.Text = dd.Month.ToString();
                this.CDay2.Text = dd.Day.ToString();
            }
            if (secDept != null)
            {
                //changeDeptSecondComboBox.Text = secDept.Dept.Name;
                //changeDeptSecondComboBox.Tag = secDept.Dept.ID;
                System.DateTime mm = FS.FrameWork.Function.NConvert.ToDateTime(secDept.User01);
                //this.CMon3.Text = mm.Month.ToString();
                //this.CDay3.Text = mm.Day.ToString();
            }
            if (thirDept != null)
            {
                //changeDeptThirdComboBox.Text = thirDept.Dept.Name;
                //changeDeptThirdComboBox.Tag = thirDept.Dept.ID;
                System.DateTime cc = FS.FrameWork.Function.NConvert.ToDateTime(thirDept.User01);
                //this.CMon4.Text = cc.Month.ToString();
                this.CDay4.Text = cc.Day.ToString();
            }
            #endregion
        }
        #endregion

        #region 初时化
        public void LoadInfo()
        {
            try
            {
                ArrayList alZG = new ArrayList();
                ArrayList alDepts = null;//在院科室
                ArrayList alDoctors = null;//在院医生
                FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();
                FS.HISFC.BizLogic.Manager.Person p = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.BizLogic.Manager.Department managerDept = new FS.HISFC.BizLogic.Manager.Department();
                //初始化结算方式
        //        this.payKindCbx.ShowCustomerList = false;
        //        this.payKindCbx.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.PAYKIND));
        //        //初始化性别:
        //        this.sexComboBox.ShowCustomerList = false;
        //        this.sexComboBox.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
        //        //初始化婚姻信息:
        //        this.marryComboBox.ShowCustomerList = false;
        //        this.marryComboBox.AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());
        //        //初始化工作信息:
                this.neuComboBox1.ShowCustomerList = false;
                this.neuComboBox1.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.PROFESSION));
                //初始化出生地信息:
                this.birthInComboBox1.ShowCustomerList = false;
                this.birthInComboBox1.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.AREA));
 
                //初始化民族信息
                 this.nationComboBox1.ShowCustomerList = false;
                 this.nationComboBox1.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.NATION));
                //初始化籍贯信息
                //this.districtComboBox1.ShowCustomerList = false;
                //this.districtComboBox1.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.DIST));
                 //初始化与患者关系信息
                 this.neuComboBox2.ShowCustomerList = false;
                 this.neuComboBox2.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.RELATIVE));
        //        //初始化入院情况信息
        //        this.inCircsComboBox.ShowCustomerList = false;
        //        this.inCircsComboBox.AddItems(Constant.GetList(FS.HISFC.Models.Base.EnumConstant.INCIRCS));


        //        alDoctors = p.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
        //        //质控医生
        //        this.QcDocComboBox.AddItems(alDoctors);
        //        //主任医生
        //        this.chiefDocComboBox.AddItems(alDoctors);
        //        //主治医生
        //        this.chargeDocComboBox.AddItems(alDoctors);
        //        //住院医生
        //        this.houseDocComboBox.AddItems(alDoctors);
        //        //实习医生
        //        this.refDocComboBox.AddItems(alDoctors);
        //        //实习
        //        this.praDocComboBox.AddItems(alDoctors);
        //        //研究生
        //        this.graDocComboBox.AddItems(alDoctors);
        //        //护士
        //        this.QcNurComboBox.AddItems(p.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N));
        //        //操作员
        //        this.operComboBox.AddItems(p.GetEmployeeAll());

        //        //转归
        //        alZG = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.ZG);

        //        try
        //        {
        //            alDepts = managerDept.GetInHosDepartment();
        //        }
        //        catch { MessageBox.Show("获得在院科室出错！"); }

        //        //转往科室
        //        inDeptComboBox.AddItems(alDepts);
        //        this.changeDeptFirstComboBox.AddItems(alDepts);
        //        this.changeDeptSecondComboBox.AddItems(alDepts);
        //        this.changeDeptThirdComboBox.AddItems(alDepts);
        //        this.outDeptComboBox.AddItems(alDepts);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion

        /// <summary>
        /// 住院费用业务层
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department inpatientManager = new FS.HISFC.BizLogic.Manager.Department();

        #region HealthRecordInterface 成员

        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface.ControlValue(FS.HISFC.Models.HealthRecord.Base obj)
        {
            #region 赋值
            FS.HISFC.BizLogic.HealthRecord.Base baseDml = new FS.HISFC.BizLogic.HealthRecord.Base();
            FS.HISFC.BizLogic.HealthRecord.DeptShift deptMger = new FS.HISFC.BizLogic.HealthRecord.DeptShift();
            FS.HISFC.BizLogic.HealthRecord.Fee feeCaseMgr = new FS.HISFC.BizLogic.HealthRecord.Fee();
            FS.HISFC.Models.HealthRecord.Base myItem = obj as FS.HISFC.Models.HealthRecord.Base;

            this.payKindCbx.Text = myItem.PatientInfo.Pact.Name;//.PayKind.ID;//付费方式、
            

            this.InpatientNO.Text = myItem.PatientInfo.PID.PatientNO;//住院号
            //this.medCardTextBox.Text = myItem.PatientInfo.SSN;//社保号
            txtInHosNo.Text = myItem.PatientInfo.InTimes.ToString();//入院次数
            //this.inpatientNOTextBox.Text = myItem.PatientInfo.ID; //住院流水号
            this.nameTextBox.Text = myItem.PatientInfo.Name;//姓名
            this.sexComboBox.Text = myItem.PatientInfo.Sex.Name;//性别
            if (myItem.PatientInfo.Sex.ID.ToString() == "M")
            {
                sexComboBox.Text = "1";
            }
            else if (myItem.PatientInfo.Sex.ID.ToString() == "F")
            {
                sexComboBox.Text = "2";
            }
            //if(myItem.PatientInfo.MainDiagnose.ToString()!= null)
            if(myItem.PatientInfo.ClinicDiagnose != null)
            this.inDiagComboBox.Text = myItem.PatientInfo.ClinicDiagnose.ToString(); //住院诊断
            this.birYear.Text = myItem.PatientInfo.Birthday.Year.ToString();//出生日期
            this.birMon.Text = myItem.PatientInfo.Birthday.Month.ToString();//出生日期
            this.birDay.Text = myItem.PatientInfo.Birthday.Day.ToString();//出生日期
            this.ageTextBox.Text = this.inpatientManager.GetAge(myItem.PatientInfo.Birthday);
            //this.ageTextBox.Text = myItem.PatientInfo.Age;//年龄
            //this.marryComboBox.Tag = myItem.PatientInfo.MaritalStatus.ID;//婚姻状况

            switch (myItem.PatientInfo.MaritalStatus.ID.ToString())
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
            //marryTextBox.Text = myItem.PatientInfo.ClinicDiagnose.ToString();// MaritalStatus.ID.ToString();
            this.neuComboBox1.Tag = myItem.PatientInfo.Profession.ID;
            this.workComboBox.Text = this.neuComboBox1.Text;
            //this.workComboBox.Tag = myItem.PatientInfo.Profession.ID;//职业
            //this.workComboBox.Text = myItem.PatientInfo.Profession.Name;
             
            this.birthInComboBox1.Tag = myItem.PatientInfo.AreaCode; //出生地 
            this.birthInComboBox.Text = this.birthInComboBox1.Text;
            //switch (myItem.PatientInfo.Nationality.ID.ToString())
            //{
            //    case "1":
            //        this.nationComboBox.Text = "汉族";
            //}


            this.nationComboBox1.Tag = myItem.PatientInfo.Nationality.ID;//.ID;//民族
            this.nationComboBox.Text = this.nationComboBox1.Text;   //myItem.PatientInfo.Country.ToString();
            //this.districtComboBox.Text = myItem.PatientInfo.Country.ToString(); //国籍
            this.districtComboBox.Text = myItem.PatientInfo.DIST; //籍贯
            this.IDTextBox.Text = myItem.PatientInfo.IDCard;//身份证
            this.workAdressTextBox.Text = myItem.PatientInfo.CompanyName; ;//工作单位地址
            this.workPhoneTextBox.Text = myItem.PatientInfo.PhoneBusiness;//工作单位电话
            this.workZipTextBox.Text = myItem.PatientInfo.BusinessZip;//邮编　
            this.homeAdTextBox.Text = myItem.PatientInfo.AddressHome;//家庭住址
            this.homeZipTextBox.Text = myItem.PatientInfo.HomeZip;//邮编　
            this.linkNameTextBox.Text = myItem.PatientInfo.Kin.Name;//联系人名称
            //this.relationComboBox.Tag = myItem.PatientInfo.Kin.RelationLink;//联系人关系
            this.neuComboBox2.Tag = myItem.PatientInfo.Kin.Relation.ID;// PatientInfo.Kin.RelationLink;
            this.relationComboBox.Text = this.neuComboBox2.Text;

            //this.relationComboBox.Text =  myItem.PatientInfo.Kin.Relation.Name;
            this.linkAdressTextBox.Text = myItem.PatientInfo.Kin.RelationAddress; //联系人地址 有待考察;
            this.linkPhoneTextBox.Text = myItem.PatientInfo.Kin.RelationPhone; //联系人电话 有待考察;

            //由变更表获取 入院科室
            FS.HISFC.Models.RADT.Location indept = baseDml.GetDeptIn(myItem.PatientInfo.ID);
            if (indept != null) //入院科室 
            {
                //入院科室代码
                inDeptComboBox.Tag = indept.Dept.ID;
                //入院科室名称
                inDeptComboBox.Text = indept.Dept.Name;
            }
            else
            {
                this.inDeptComboBox.Tag = myItem.PatientInfo.PVisit.PatientLocation.Dept.ID;
                this.inDeptComboBox.Text = myItem.PatientInfo.PVisit.PatientLocation.Dept.Name;
            }
            //由变更表获取 出院科室
            FS.HISFC.Models.RADT.Location outDept = baseDml.GetDeptOut(myItem.PatientInfo.ID);
            if (outDept != null)
            {
                this.outDeptComboBox.Tag = outDept.Dept.ID;
                this.outDeptComboBox.Text = outDept.Dept.Name;
            }

            this.CYear2.Text = myItem.PatientInfo.PVisit.InTime.Year.ToString();//入院时间
            this.CMon2.Text = myItem.PatientInfo.PVisit.InTime.Month.ToString();//入院时间
            this.CDay2.Text = myItem.PatientInfo.PVisit.InTime.Day.ToString();//入院时间
            this.neuLabel21.Text = myItem.PatientInfo.PVisit.Circs.ID;//入院情况

            //出院时间不等于最小时间且患者状态为 出院登记状态
            if (myItem.PatientInfo.PVisit.OutTime != System.DateTime.MinValue && myItem.PatientInfo.PVisit.InState.ID.ToString() == "B")
            {
                this.OutYear.Text = myItem.PatientInfo.PVisit.OutTime.Year.ToString();//出院日期
                this.OutMon.Text = myItem.PatientInfo.PVisit.OutTime.Month.ToString();//出院日期
                this.OutDay.Text = myItem.PatientInfo.PVisit.OutTime.Day.ToString();//出院日期
            }

            #region 暂时不用 打印的时候 不需要打印人员
            //				houseDocComboBox.Tag = myItem.PatientInfo.PVisit.AdmittingDoctor.ID ;
            //				houseDocTextBox.Text = myItem.PatientInfo.PVisit.AdmittingDoctor.ID ;
            //				//住院医师姓名
            //				houseDocComboBox.Text = myItem.PatientInfo.PVisit.AdmittingDoctor.Name;
            //				//主治医师代码
            //				chargeDocComboBox.Tag = myItem.PatientInfo.PVisit.AttendingDoctor.ID;
            //				chargeDocComboBox.Text = myItem.PatientInfo.PVisit.AttendingDoctor.Name;
            //				chargeDocTextBox.Text =  myItem.PatientInfo.PVisit.AttendingDoctor.ID;
            //				//主任医师代码
            //				chiefDocComboBox.Tag = myItem.PatientInfo.PVisit.ConsultingDoctor.ID;
            //				chiefDocComboBox.Text = myItem.PatientInfo.PVisit.ConsultingDoctor.Name;
            //				chiefDocTextBox.Text = myItem.PatientInfo.PVisit.ConsultingDoctor.ID;
            //				//科主任代码
            //				//			info.PVisit.ReferringDoctor.ID
            //				//进修医师代码
            //				refDocComboBox.Tag = myItem.RefresherDocd;
            //				refDocTextBox.Text = myItem.RefresherDocd;
            //				refDocComboBox.Text = myItem.RefresherDonm;
            //				//研究生实习医师代码
            //				graDocComboBox.Tag = myItem.GraDocCode;
            //				graDocComboBox.Text = myItem.GraDocName;
            //				//实习医师代码
            //				praDocComboBox.Tag = myItem.PraDocCode;
            //				praDocComboBox.Text = myItem.PraDocName;
            #endregion

            if (this.OutYear.Text != "1")
            {
                int inDays = (int)new System.TimeSpan(myItem.PatientInfo.PVisit.OutTime.Ticks
                    - myItem.PatientInfo.PVisit.InTime.Ticks).TotalDays;
                if (inDays > 0)
                    this.inDaysTextBox.Text = inDays.ToString();//住院天数
            }
            else
            {
                this.inDaysTextBox.Text = "";
            }
            //this.inSourceComboBox.Tag = myItem.PatientInfo.PVisit.InSource.ID;//入院来源

            #endregion

            ////保存转科信息的列表
            //ArrayList changeDept = new ArrayList();
            ////获取转科信息
            //changeDept = deptMger.QueryChangeDeptFromShiftApply(myItem.PatientInfo.ID, "2");
            //LoadChangeDept(changeDept);

            //ArrayList alOrg = new ArrayList();
            //FS.HISFC.BizLogic.HealthRecord.Diagnose diag = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            //alOrg = diag.QueryCaseDiagnose(myItem.PatientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
        }

        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface.Reset()
        {
            payKindCbx.Tag = "";//付费方式、
            InpatientNO.Text = "";//住院流水号
            //this.medCardTextBox.Text = "";//社保号
            txtInHosNo.Text = "";//入院次数
            //this.inpatientNOTextBox.Text = "";//住院号
            this.nameTextBox.Text = "";//姓名
            this.birMon.Tag = "";//性别
            this.birYear.Text = "";//出生日期
            this.birMon.Text = "";//出生日期
            this.birDay.Text = "";//出生日期
            this.ageTextBox.Text = "";//年龄
            //this.marryComboBox.Tag = "";//婚姻状况
            this.workComboBox.Tag = "";//职业
            this.birthInComboBox.Tag = ""; //出生地
            this.nationComboBox.Tag = "";//民族
            this.districtComboBox.Text = ""; //籍贯
            this.IDTextBox.Text = "";//身份证
            this.workAdressTextBox.Text = ""; ;//工作单位地址
            this.workPhoneTextBox.Text = "";//工作单位电话
            this.workZipTextBox.Text = "";//邮编　
            this.homeAdTextBox.Text = "";//家庭住址
            this.homeZipTextBox.Text = "";//邮编　
            this.linkNameTextBox.Text = "";//联系人名称
            this.relationComboBox.Tag = "";//联系人关系
            this.linkAdressTextBox.Text = ""; //联系人地址 有待考察;
            this.linkPhoneTextBox.Text = ""; //联系人电话 有待考察;\
            inDaysTextBox.Text = "";
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
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
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

            return print.PrintPage(0, 0, this);

            //return p.PrintPage(20, 10, this);   
        }

        int FS.FrameWork.WinForms.Forms.IReportPrinter.PrintPreview()
        {
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
            return p.PrintPreview(20, 10, this);
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
