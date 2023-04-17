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
    /// 病案首页第三页附页
    /// </summary>
    public partial class ucCaseAdditionalPrint : UserControl,FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional
    {
        /// <summary>
        /// 病案首页附页
        /// </summary>
        public ucCaseAdditionalPrint()
        {
            InitializeComponent();
        }
        //单位列表
        FS.FrameWork.Public.ObjectHelper UnitListHelper = new FS.FrameWork.Public.ObjectHelper();
        //疗程列表
        FS.FrameWork.Public.ObjectHelper PeriodListHelper = new FS.FrameWork.Public.ObjectHelper();
        //分期
        FS.FrameWork.Public.ObjectHelper TumourStageHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 设置预览比例 
        /// 新的FrameWork才有改属性
        /// </summary>
        private bool isPrintViewZoom = false;
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
            // 分期
            ArrayList TumourStageList = Constant.GetList("CASETUMOURSTAGE");
            TumourStageHelper.ArrayObject = TumourStageList;
        }
        #region   HealthRecordInterfaceAdditional 成员
        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional.ControlValue(FS.HISFC.Models.HealthRecord.Base info)
        {
            this.InitList();

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
                    if (TumourStageHelper.GetName(tumourInfo.Tumour_Stage) == null
                        || TumourStageHelper.GetName(tumourInfo.Tumour_Stage) == "")
                    {
                        this.txtTumourStage.Text = tumourInfo.Tumour_Stage;
                    }
                    else
                    {
                        this.txtTumourStage.Text = TumourStageHelper.GetName(tumourInfo.Tumour_Stage);
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
            if (tumourList != null && tumourList.Count>0)
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
                            this.txtCR3.Visible = true ;
                            this.txtPR3.Visible = true ;
                            this.txtSD3.Visible = true ;
                            this.txtPD3.Visible = true ;
                            this.txtNA3.Visible = true ;
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
                        default:
                            break;
                    }
                    if (j > 3)
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
            }
            #endregion
        }
        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceAdditional.Reset()
        {
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
            this.txtPosition.Text="";
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
            this.txtDrugName3.Text="";
            this.txtDrugTreatment3.Text="";
            this.txtCR3.Text = "";
            this.txtPR3.Text = "";
            this.txtSD3.Text = "";
            this.txtPD3.Text = "";
            this.txtNA3.Text = "";
            #endregion

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
            return p.PrintPreview(0, 1, this);
        }

        #endregion

        private void ucCaseAdditionalPrint_Load(object sender, EventArgs e)
        {

        }
    }
}
