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
    public partial class ucGyhisCaseBackPrint : UserControl, FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack
    {
        public ucGyhisCaseBackPrint()
        {
            InitializeComponent();
            LoadInfo();
        }

        /// <summary>
        /// 打印业务层
        /// </summary>
        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

        //费用业务层

        FS.HISFC.BizLogic.HealthRecord.Fee feeManager = new FS.HISFC.BizLogic.HealthRecord.Fee();
        //单位列表
        FS.FrameWork.Public.ObjectHelper UnitListHelper = new FS.FrameWork.Public.ObjectHelper();
        //疗程列表
        FS.FrameWork.Public.ObjectHelper PeriodListHelper = new FS.FrameWork.Public.ObjectHelper();

        //麻醉列表
        FS.FrameWork.Public.ObjectHelper NarcListHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 设置预览比例 
        /// 新的FrameWork才有改属性
        /// </summary>
        private bool isPrintViewZoom = false;
        #region 初时化
        /// <summary>
        /// 
        /// </summary>
        public void LoadInfo()
        {
            try
            {
                FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();
      
                //查询麻醉方式列表
                ArrayList NarcList = Constant.GetList("CASEANESTYPE");
                this.txtOp17.AddItems(NarcList);
                this.txtOp27.AddItems(NarcList);
                this.txtOp37.AddItems(NarcList);
                this.txtOp47.AddItems(NarcList);
                this.txtOp57.AddItems(NarcList);
                //切口列表
                ArrayList NickTypeList = Constant.GetList("INCITYPE");
                this.txtOp18.AddItems(NickTypeList);
                this.txtOp28.AddItems(NickTypeList);
                this.txtOp38.AddItems(NickTypeList);
                this.txtOp48.AddItems(NickTypeList);
                this.txtOp58.AddItems(NickTypeList);
                //愈合列表
                ArrayList CicaTypelist = Constant.GetAllList("CICATYPE");
                this.txtOp19.AddItems(CicaTypelist);
                this.txtOp29.AddItems(CicaTypelist);
                this.txtOp39.AddItems(CicaTypelist);
                this.txtOp49.AddItems(CicaTypelist);
                this.txtOp59.AddItems(CicaTypelist);
                //单位列表
                ArrayList UnitList = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.DOSEUNIT);
                UnitListHelper.ArrayObject = UnitList;

                //疗程列表 
                ArrayList PeriodList = Constant.GetList(FS.HISFC.Models.Base.EnumConstant.PERIODOFTREATMENT);// da.GetPeriodList();
                PeriodListHelper.ArrayObject = PeriodList;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion
        #region 方法


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //this.SetVisible(false);
            p.PrintPage(0, 0, this.panel1);
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
        public int PrintPreview()
        {
            //this.SetVisible(true);
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            this.PreviewZoom();
            if (this.isPrintViewZoom)
            {
                p.PreviewZoomFactor = 1;
            }
            return p.PrintPreview(20, 10, this);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置打印时控件的可见性
        /// </summary>
        /// <param name="isSee"></param>
        private void SetVisible(bool isSee)
        {
            foreach (Control c in this.panel1.Controls)
            {
                if (c is FS.FrameWork.WinForms.Controls.NeuLabel && !c.Name.StartsWith("lblPri"))
                {
                    c.Visible = isSee;
                }
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            foreach (Control c in this.panel1.Controls)
            {
                if (c is FS.FrameWork.WinForms.Controls.NeuLabel && c.Name.StartsWith("lblPri"))
                {
                    FS.FrameWork.WinForms.Controls.NeuLabel lbl = c as FS.FrameWork.WinForms.Controls.NeuLabel;
                    lbl.Text = " ";
                }
            }

        }

        #endregion
        #region HealthRecordInterface 成员

        /// <summary>
        /// 设置病案反面值
        /// </summary>
        void FS.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterfaceBack.ControlValue(FS.HISFC.Models.HealthRecord.Base obj)
        {
            this.Clear();
            this.SetVisible();
            FS.HISFC.Models.HealthRecord.Base healthReord = obj as FS.HISFC.Models.HealthRecord.Base;


            #region 婴儿信息
            ////查询符合条件的数据  如果是一胞胎后面婴儿信息就不要显示

            FS.HISFC.BizLogic.HealthRecord.Baby baby = new FS.HISFC.BizLogic.HealthRecord.Baby();
            ArrayList list = baby.QueryBabyByInpatientNo(healthReord.PatientInfo.ID);
           
            int tempi = 1;
            foreach (FS.HISFC.Models.HealthRecord.Baby babyInfo in list)
            {
                switch (tempi)
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
                        else if(babyInfo.BirthEnd=="2")
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
                        //感染次数
                        this.txtInfech113.Text = babyInfo.InfectNum.ToString();
                        //主要感染名称
                        this.txtInfechName114.Text = babyInfo.Infect.Name.ToString();

                        //ICD10
                        this.txtIcd115.Text = babyInfo.Infect.ID.ToString();
                        //抢救次数
                        this.txtSalvTimes116.Text = babyInfo.SalvNum.ToString();
                        //成功抢救次数
                        this.txtSuccTimes117.Text = babyInfo.SuccNum.ToString();
                        tempi++;
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
                        //感染次数
                        this.txtInfech213.Text = babyInfo.InfectNum.ToString();
                        //主要感染名称
                        this.txtInfechName214.Text = babyInfo.Infect.Name.ToString();

                        //ICD10
                        this.txtIcd215.Text = babyInfo.Infect.ID.ToString();
                        //抢救次数
                        this.txtSalvTimes216.Text = babyInfo.SalvNum.ToString();
                        //成功抢救次数
                        this.txtSuccTimes217.Text = babyInfo.SuccNum.ToString();
                        tempi++;
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
                        //感染次数
                        this.txtInfech313.Text = babyInfo.InfectNum.ToString();
                        //主要感染名称
                        this.txtInfechName314.Text = babyInfo.Infect.Name.ToString();

                        //ICD10
                        this.txtIcd315.Text = babyInfo.Infect.ID.ToString();
                        //抢救次数
                        this.txtSalvTimes316.Text = babyInfo.SalvNum.ToString();
                        //成功抢救次数
                        this.txtSuccTimes317.Text = babyInfo.SuccNum.ToString();
                        tempi++;
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
                        //感染次数
                        this.txtInfech413.Text = babyInfo.InfectNum.ToString();
                        //主要感染名称
                        this.txtInfechName414.Text = babyInfo.Infect.Name.ToString();

                        //ICD10
                        this.txtIcd415.Text = babyInfo.Infect.ID.ToString();
                        //抢救次数
                        this.txtSalvTimes416.Text = babyInfo.SalvNum.ToString();
                        //成功抢救次数
                        this.txtSuccTimes417.Text = babyInfo.SuccNum.ToString();
                        tempi++;
                        break;
                    default:
                        break;
                }
                if (tempi > 4)
                {
                    break;
                }
            }
            #endregion

            #region 手术信息
            FS.HISFC.BizLogic.HealthRecord.Operation operationManager = new FS.HISFC.BizLogic.HealthRecord.Operation();
            ArrayList alOpr = operationManager.QueryOperationByInpatientNo(healthReord.PatientInfo.ID);

            int i = 1;
            if(alOpr.Count>0)
            {
                foreach (object opr in alOpr)
                {
                    FS.HISFC.Models.HealthRecord.OperationDetail opration = opr as FS.HISFC.Models.HealthRecord.OperationDetail;
                    switch (i)
                    {
                        case 1:
                            if (opration.OperationDate.Date > FS.FrameWork.Function.NConvert.ToDateTime("2011-10-09 23:00:00"))//更改了常数
                            {
                                FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();
                                //查询麻醉方式列表
                                ArrayList NarcList = Constant.GetList("CASEANESTYPE");
                                this.txtOp17.AddItems(NarcList);
                                this.txtOp27.AddItems(NarcList);
                                this.txtOp37.AddItems(NarcList);
                                this.txtOp47.AddItems(NarcList);
                                this.txtOp57.AddItems(NarcList);
                                NarcListHelper.ArrayObject = NarcList;
                            }
                            //编码
                            if (opration.OperationInfo.ID == "MS999")
                            {
                                this.txtOP11.Text = "";
                            }
                            else
                            {
                                this.txtOP11.Text = opration.OperationInfo.ID;
                            }
                            //日期
                            this.txtOp12.Text = opration.OperationDate.ToShortDateString();
                            //名称
                            this.txtOp13.Text = opration.OperationInfo.Name;
                            //术者
                            this.txtOp14.Text = opration.FirDoctInfo.Name;
                            //一助
                            this.txtOp15.Text = opration.SecDoctInfo.Name;
                            //二助
                            this.txtOp16.Text = opration.ThrDoctInfo.Name;
                            //麻醉方式
                            //this.txtOp17.Tag = opration.MarcKind;
                            if (opration.MarcKind != "" && opration.OpbOpa != "")
                            {
                                string marcKind = string.Empty;
                                if (NarcListHelper.GetName(opration.MarcKind) != null)
                                {
                                    marcKind = NarcListHelper.GetName(opration.MarcKind);
                                }
                                if (NarcListHelper.GetName(opration.OpbOpa) != null)
                                {
                                    marcKind += "+" + NarcListHelper.GetName(opration.OpbOpa);
                                }
                                if (marcKind.Length > 5)
                                {
                                    this.txtOp17.Visible = false;
                                    this.txtOp17_1.Text = marcKind.Substring(0, 5);
                                    this.txtOp17_2.Text = marcKind.Substring(5);
                                }
                                else
                                {
                                    this.txtOp17.Visible = false;
                                    this.txtOp17_2.Visible = false;
                                    this.txtOp17_1.Text = marcKind;
                                }
                            }
                            else if (opration.MarcKind != "" && opration.OpbOpa == "")
                            {
                                this.txtOp17_1.Visible = false;
                                this.txtOp17_2.Visible = false;
                                this.txtOp17.Tag = opration.MarcKind;
                            }
                            else if (opration.MarcKind == "" && opration.OpbOpa != "")
                            {
                                this.txtOp17_1.Visible = false;
                                this.txtOp17_2.Visible = false;
                                this.txtOp17.Tag = opration.OpbOpa;
                            }
                            //切口愈合等级
                            this.txtOp18.Tag = opration.NickKind;
                            this.txtOp19.Tag = opration.CicaKind;
                            //麻醉医师
                            this.txtOp110.Text = opration.NarcDoctInfo.Name;
                            i++;
                            break;
                        case 2:
                            //编码
                            if (opration.OperationInfo.ID == "MS999")
                            {
                                this.txtOp21.Text = "";
                            }
                            else
                            {
                                this.txtOp21.Text = opration.OperationInfo.ID;
                            }
                            //日期
                            this.txtOp22.Text = opration.OperationDate.ToShortDateString();
                            //名称
                            this.txtOp23.Text = opration.OperationInfo.Name;
                            //术者
                            this.txtOp24.Text = opration.FirDoctInfo.Name;
                            //一助
                            this.txtOp25.Text = opration.SecDoctInfo.Name;
                            //二助
                            this.txtOp26.Text = opration.ThrDoctInfo.Name;
                            //麻醉方式
                            //this.txtOp27.Tag = opration.MarcKind;
                            if (opration.MarcKind != "" && opration.OpbOpa != "")
                            {
                                string marcKind = string.Empty;
                                if (NarcListHelper.GetName(opration.MarcKind) != null)
                                {
                                    marcKind = NarcListHelper.GetName(opration.MarcKind);
                                }
                                if (NarcListHelper.GetName(opration.OpbOpa) != null)
                                {
                                    marcKind += "+" + NarcListHelper.GetName(opration.OpbOpa);
                                }
                                if (marcKind.Length > 5)
                                {
                                    this.txtOp27.Visible = false;
                                    this.txtOp27_1.Text = marcKind.Substring(0, 5);
                                    this.txtOp27_2.Text = marcKind.Substring(5);
                                }
                                else
                                {
                                    this.txtOp27.Visible = false;
                                    this.txtOp27_2.Visible = false;
                                    this.txtOp27_1.Text = marcKind;
                                }
                            }
                            else if (opration.MarcKind != "" && opration.OpbOpa == "")
                            {
                                this.txtOp27_1.Visible = false;
                                this.txtOp27_2.Visible = false;
                                this.txtOp27.Tag = opration.MarcKind;
                            }
                            else if (opration.MarcKind == "" && opration.OpbOpa != "")
                            {
                                this.txtOp27_1.Visible = false;
                                this.txtOp27_2.Visible = false;
                                this.txtOp27.Tag = opration.OpbOpa;
                            }
                            //切口愈合等级
                            this.txtOp28.Tag = opration.NickKind;
                            this.txtOp29.Tag = opration.CicaKind;
                            //麻醉医师
                            this.txtOp210.Text = opration.NarcDoctInfo.Name;
                            i++;
                            break;
                        case 3:
                            //编码
                            if (opration.OperationInfo.ID == "MS999")
                            {
                                this.txtOp31.Text = "";
                            }
                            else
                            {
                                this.txtOp31.Text = opration.OperationInfo.ID;
                            }
                            //日期
                            this.txtOp32.Text = opration.OperationDate.ToShortDateString();
                            //名称
                            this.txtOp33.Text = opration.OperationInfo.Name;
                            //术者
                            this.txtOp34.Text = opration.FirDoctInfo.Name;
                            //一助
                            this.txtOp35.Text = opration.SecDoctInfo.Name;
                            //二助
                            this.txtOp36.Text = opration.ThrDoctInfo.Name;
                            //麻醉方式
                            //this.txtOp37.Tag = opration.MarcKind;
                            if (opration.MarcKind != "" && opration.OpbOpa != "")
                            {
                                string marcKind = string.Empty;
                                if (NarcListHelper.GetName(opration.MarcKind) != null)
                                {
                                    marcKind = NarcListHelper.GetName(opration.MarcKind);
                                }
                                if (NarcListHelper.GetName(opration.OpbOpa) != null)
                                {
                                    marcKind += "+" + NarcListHelper.GetName(opration.OpbOpa);
                                }
                                if (marcKind.Length > 5)
                                {
                                    this.txtOp37.Visible = false;
                                    this.txtOp37_1.Text = marcKind.Substring(0, 5);
                                    this.txtOp37_2.Text = marcKind.Substring(5);
                                }
                                else
                                {
                                    this.txtOp37.Visible = false;
                                    this.txtOp37_2.Visible = false;
                                    this.txtOp37_1.Text = marcKind;
                                }
                            }
                            else if (opration.MarcKind != "" && opration.OpbOpa == "")
                            {
                                this.txtOp37_1.Visible = false;
                                this.txtOp37_2.Visible = false;
                                this.txtOp37.Tag = opration.MarcKind;
                            }
                            else if (opration.MarcKind == "" && opration.OpbOpa != "")
                            {
                                this.txtOp37_1.Visible = false;
                                this.txtOp37_2.Visible = false;
                                this.txtOp37.Tag = opration.OpbOpa;
                            }
                            //切口愈合等级
                            this.txtOp38.Tag = opration.NickKind;
                            this.txtOp39.Tag = opration.CicaKind;
                            //麻醉医师
                            this.txtOp310.Text = opration.NarcDoctInfo.Name;
                            i++;
                            break;
                        case 4:
                            //编码
                            if (opration.OperationInfo.ID == "MS999")
                            {
                                this.txtOp41.Text = "";
                            }
                            else
                            {
                                this.txtOp41.Text = opration.OperationInfo.ID;
                            }
                            //日期
                            this.txtOp42.Text = opration.OperationDate.ToShortDateString();
                            //名称
                            this.txtOp43.Text = opration.OperationInfo.Name;
                            //术者
                            this.txtOp44.Text = opration.FirDoctInfo.Name;
                            //一助
                            this.txtOp45.Text = opration.SecDoctInfo.Name;
                            //二助
                            this.txtOp46.Text = opration.ThrDoctInfo.Name;
                            //麻醉方式
                            //this.txtOp47.Tag = opration.MarcKind;
                            if (opration.MarcKind != "" && opration.OpbOpa != "")
                            {
                                string marcKind = string.Empty;
                                if (NarcListHelper.GetName(opration.MarcKind) != null)
                                {
                                    marcKind = NarcListHelper.GetName(opration.MarcKind);
                                }
                                if (NarcListHelper.GetName(opration.OpbOpa) != null)
                                {
                                    marcKind += "+" + NarcListHelper.GetName(opration.OpbOpa);
                                }
                                if (marcKind.Length > 5)
                                {
                                    this.txtOp47.Visible = false;
                                    this.txtOp47_1.Text = marcKind.Substring(0, 5);
                                    this.txtOp47_2.Text = marcKind.Substring(5);
                                }
                                else
                                {
                                    this.txtOp47.Visible = false;
                                    this.txtOp47_2.Visible = false;
                                    this.txtOp47_1.Text = marcKind;
                                }
                            }
                            else if (opration.MarcKind != "" && opration.OpbOpa == "")
                            {
                                this.txtOp47_1.Visible = false;
                                this.txtOp47_2.Visible = false;
                                this.txtOp47.Tag = opration.MarcKind;
                            }
                            else if (opration.MarcKind == "" && opration.OpbOpa != "")
                            {
                                this.txtOp47_1.Visible = false;
                                this.txtOp47_2.Visible = false;
                                this.txtOp47.Tag = opration.OpbOpa;
                            }
                            //切口愈合等级
                            this.txtOp48.Tag = opration.NickKind;
                            this.txtOp49.Tag = opration.CicaKind;
                            //麻醉医师
                            this.txtOp410.Text = opration.NarcDoctInfo.Name;
                            i++;
                            break;
                        case 5:
                            //编码
                            if (opration.OperationInfo.ID == "MS999")
                            {
                                this.txtOp51.Text = "";
                            }
                            else
                            {
                                this.txtOp51.Text = opration.OperationInfo.ID;
                            }
                            //日期
                            this.txtOp52.Text = opration.OperationDate.ToShortDateString();
                            //名称
                            this.txtOp53.Text = opration.OperationInfo.Name;
                            //术者
                            this.txtOp54.Text = opration.FirDoctInfo.Name;
                            //一助
                            this.txtOp55.Text = opration.SecDoctInfo.Name;
                            //二助
                            this.txtOp56.Text = opration.ThrDoctInfo.Name;
                            //麻醉方式
                            //this.txtOp57.Tag = opration.MarcKind;
                            if (opration.MarcKind != "" && opration.OpbOpa != "")
                            {
                                string marcKind = string.Empty;
                                if (NarcListHelper.GetName(opration.MarcKind) != null)
                                {
                                    marcKind = NarcListHelper.GetName(opration.MarcKind);
                                }
                                if (NarcListHelper.GetName(opration.OpbOpa) != null)
                                {
                                    marcKind += "+" + NarcListHelper.GetName(opration.OpbOpa);
                                }
                                if (marcKind.Length > 5)
                                {
                                    this.txtOp57.Visible = false;
                                    this.txtOp57_1.Text = marcKind.Substring(0, 5);
                                    this.txtOp57_2.Text = marcKind.Substring(5);
                                }
                                else
                                {
                                    this.txtOp57.Visible = false;
                                    this.txtOp57_2.Visible = false;
                                    this.txtOp57_1.Text = marcKind;
                                }
                            }
                            else if (opration.MarcKind != "" && opration.OpbOpa == "")
                            {
                                this.txtOp57_1.Visible = false;
                                this.txtOp57_2.Visible = false;
                                this.txtOp57.Tag = opration.MarcKind;
                            }
                            else if (opration.MarcKind == "" && opration.OpbOpa != "")
                            {
                                this.txtOp57_1.Visible = false;
                                this.txtOp57_2.Visible = false;
                                this.txtOp57.Tag = opration.OpbOpa;
                            }
                            //切口愈合等级
                            this.txtOp58.Tag = opration.NickKind;
                            this.txtOp59.Tag = opration.CicaKind;
                            //麻醉医师
                            this.txtOp510.Text = opration.NarcDoctInfo.Name;
                            i++;
                            break;
                        default:
                            break;
                    }
                    if (i > 5)
                    {
                        break;
                    }
                }
            }

            #endregion
            #region 肿瘤信息
            FS.HISFC.BizLogic.HealthRecord.Tumour tumourMgr = new FS.HISFC.BizLogic.HealthRecord.Tumour();
            FS.HISFC.Models.HealthRecord.Tumour tumourInfo = new FS.HISFC.Models.HealthRecord.Tumour();
            
            tumourInfo = tumourMgr.GetTumour(healthReord.PatientInfo.ID);
            if (tumourInfo != null)
            {
                if (tumourInfo.Rmodeid == null || tumourInfo.Rmodeid == "")
                {
                    this.neuLabel56.Text = "/";
                }
                else
                {
                    this.neuLabel56.Text = tumourInfo.Rmodeid; //放疗方式
                }
                if (tumourInfo.Rprocessid == null || tumourInfo.Rprocessid == "")
                {
                    this.neuLabel59.Text = "/";
                }
                else
                {
                    this.neuLabel59.Text = tumourInfo.Rprocessid;//放疗程式
                }
                if (tumourInfo.Rdeviceid == null || tumourInfo.Rdeviceid == "")
                {
                    this.neuLabel76.Text = "/";
                }
                else
                {
                    this.neuLabel76.Text = tumourInfo.Rdeviceid;//放疗装置
                }

                this.neuLabel1.Text = tumourInfo.Gy1.ToString();	//原发灶gy剂量
                this.neuLabel2.Text = tumourInfo.Time1.ToString(); //原发灶次数
                this.neuLabel3.Text = tumourInfo.Day1.ToString();		//原发灶天数

                if (tumourInfo.Gy1 > 0)
                {
                    this.neuLabel4.Text = tumourInfo.BeginDate1.Year.ToString();//原发灶开始时间
                    this.neuLabel5.Text = tumourInfo.BeginDate1.Month.ToString();//原发灶开始时间
                    this.neuLabel6.Text = tumourInfo.BeginDate1.Day.ToString();//原发灶结束时间
                    this.neuLabel7.Text = tumourInfo.EndDate1.Year.ToString();//原发灶结束时间
                    this.neuLabel8.Text = tumourInfo.EndDate1.Month.ToString();//原发灶结束时间
                    this.neuLabel9.Text = tumourInfo.EndDate1.Day.ToString();//原发灶结束时间
                }
                else
                {
                    this.neuLabel4.Text = string.Empty;//原发灶开始时间
                    this.neuLabel5.Text = string.Empty;//原发灶开始时间
                    this.neuLabel6.Text = string.Empty;//原发灶结束时间
                    this.neuLabel7.Text = string.Empty;//原发灶结束时间
                    this.neuLabel8.Text = string.Empty;//原发灶结束时间
                    this.neuLabel9.Text = string.Empty;//原发灶结束时间
                }

                this.neuLabel10.Text = tumourInfo.Gy2.ToString(); //区域淋巴结gy剂量
                this.neuLabel11.Text = tumourInfo.Time2.ToString();		//区域淋巴结次数
                this.neuLabel12.Text = tumourInfo.Day2.ToString();		//区域淋巴结天数

                if (tumourInfo.Gy2 > 0)
                {
                    this.neuLabel13.Text = tumourInfo.BeginDate2.Year.ToString();//区域淋巴结开始时间
                    this.neuLabel14.Text = tumourInfo.BeginDate2.Month.ToString();//区域淋巴结开始时间
                    this.neuLabel15.Text = tumourInfo.BeginDate2.Day.ToString();//区域淋巴结开始时间
                    this.neuLabel16.Text = tumourInfo.EndDate2.Year.ToString();//区域淋巴结结束时间
                    this.neuLabel17.Text = tumourInfo.EndDate2.Month.ToString();//区域淋巴结结束时间
                    this.neuLabel18.Text = tumourInfo.EndDate2.Day.ToString();//区域淋巴结结束时间
                }
                else
                {
                    this.neuLabel13.Text = string.Empty;//区域淋巴结开始时间
                    this.neuLabel14.Text = string.Empty;//区域淋巴结开始时间
                    this.neuLabel15.Text = string.Empty;//区域淋巴结开始时间
                    this.neuLabel16.Text = string.Empty;//区域淋巴结结束时间
                    this.neuLabel17.Text = string.Empty;//区域淋巴结结束时间
                    this.neuLabel18.Text = string.Empty;//区域淋巴结结束时间
                }

                this.neuLabel20.Text = tumourInfo.Gy3.ToString(); //转移灶gy剂量
                this.neuLabel21.Text = tumourInfo.Time3.ToString();		//转移灶次数
                this.neuLabel22.Text = tumourInfo.Day3.ToString();		//转移灶天数

                if (tumourInfo.Gy3 > 0)
                {
                    this.neuLabel23.Text = tumourInfo.BeginDate3.Year.ToString();//转移灶开始时间
                    this.neuLabel24.Text = tumourInfo.BeginDate3.Month.ToString();//转移灶开始时间
                    this.neuLabel25.Text = tumourInfo.BeginDate3.Day.ToString();//转移灶开始时间
                    this.neuLabel26.Text = tumourInfo.EndDate3.Year.ToString();//转移灶结束时间
                    this.neuLabel27.Text = tumourInfo.EndDate3.Month.ToString();//转移灶结束时间
                    this.neuLabel28.Text = tumourInfo.EndDate3.Day.ToString();//转移灶结束时间
                }
                else
                {
                    this.neuLabel23.Text = string.Empty;//转移灶开始时间
                    this.neuLabel24.Text = string.Empty;//转移灶开始时间
                    this.neuLabel25.Text = string.Empty;//转移灶开始时间
                    this.neuLabel26.Text = string.Empty;//转移灶结束时间
                    this.neuLabel27.Text = string.Empty;//转移灶结束时间
                    this.neuLabel28.Text = string.Empty;//转移灶结束时间
                }
                if (tumourInfo.Cmodeid == null || tumourInfo.Cmodeid == "")
                {
                    this.neuLabel60.Text = "/";	//化疗方式
                }
                else
                {
                    this.neuLabel60.Text = tumourInfo.Cmodeid;	//化疗方式
                }
                if (tumourInfo.Cmethod == null || tumourInfo.Cmethod == "")
                {
                    this.neuLabel67.Text = "/";	//化疗方法
                }
                else
                {
                    this.neuLabel67.Text = tumourInfo.Cmethod;	//化疗方法
                }
            }
            
            ArrayList tumourList = new ArrayList();
            tumourList = tumourMgr.QueryTumourDetail(healthReord.PatientInfo.ID);
            if (tumourList.Count != 0)
            {
                int j = 1;
                foreach (FS.HISFC.Models.HealthRecord.TumourDetail info in tumourList)
                {
                    switch (j)
                    {
                        case 1:
                            this.neuLabel47.Visible = false;
                            this.neuLabel46.Visible = false;
                            this.neuLabel45.Visible = false;
                            this.neuLabel44.Visible = false;
                            this.neuLabel43.Visible = false;
                            this.neuLabel42.Visible = false;
                            this.neuLabel54.Visible = false;
                            this.neuLabel53.Visible = false;
                            this.neuLabel52.Visible = false;
                            this.neuLabel51.Visible = false;
                            this.neuLabel50.Visible = false;
                            this.neuLabel49.Visible = false;
                            this.neuLabel29.Text = info.CureDate.ToString();//日期
                            this.neuLabel30.Text = info.DrugInfo.Name + "(" + info.Qty + this.UnitListHelper.GetName(info.Unit) + ")";//药物名称 
                            this.neuLabel31.Text = this.PeriodListHelper.GetName(info.Period);//疗程
                            switch (FS.FrameWork.Function.NConvert.ToInt32(info.Result))
                            {
                                case 1:
                                    this.neuLabel32.Text = "√";
                                    this.neuLabel33.Visible = false;
                                    this.neuLabel34.Visible = false;
                                    this.neuLabel35.Visible = false;
                                    this.neuLabel36.Visible = false;
                                    this.neuLabel37.Visible = false;
                                    break;
                                case 2:
                                    this.neuLabel32.Visible = false;
                                    this.neuLabel33.Text = "√";
                                    this.neuLabel34.Visible = false;
                                    this.neuLabel35.Visible = false;
                                    this.neuLabel36.Visible = false;
                                    this.neuLabel37.Visible = false;
                                    break;
                                case 3:
                                    this.neuLabel32.Visible = false;
                                    this.neuLabel33.Visible = false;
                                    this.neuLabel34.Text = "√";
                                    this.neuLabel35.Visible = false;
                                    this.neuLabel36.Visible = false;
                                    this.neuLabel37.Visible = false;
                                    break;
                                case 4:
                                    this.neuLabel32.Visible = false;
                                    this.neuLabel33.Visible = false;
                                    this.neuLabel34.Visible = false;
                                    this.neuLabel35.Text = "√";
                                    this.neuLabel36.Visible = false;
                                    this.neuLabel37.Visible = false;
                                    break;
                                case 5:
                                    this.neuLabel32.Visible = false;
                                    this.neuLabel33.Visible = false;
                                    this.neuLabel34.Visible = false;
                                    this.neuLabel35.Visible = false;
                                    this.neuLabel36.Text = "√";
                                    this.neuLabel37.Visible = false;
                                    break;
                                case 6:
                                    this.neuLabel32.Visible = false;
                                    this.neuLabel33.Visible = false;
                                    this.neuLabel34.Visible = false;
                                    this.neuLabel35.Visible = false;
                                    this.neuLabel36.Visible = false;
                                    this.neuLabel37.Text = "√";
                                    break;
                                default:
                                    break;

                            }
                            j++;
                            break;
                        case 2:
                            this.neuLabel47.Visible = true;
                            this.neuLabel46.Visible = true;
                            this.neuLabel45.Visible = true;
                            this.neuLabel44.Visible = true;
                            this.neuLabel43.Visible = true;
                            this.neuLabel42.Visible = true;
                            this.neuLabel39.Text = info.CureDate.ToString();//日期
                            this.neuLabel38.Text = info.DrugInfo.Name + "(" + info.Qty + this.UnitListHelper.GetName(info.Unit) + ")";//药物名称 
                            this.neuLabel48.Text = this.PeriodListHelper.GetName(info.Period);//疗程
                            switch (FS.FrameWork.Function.NConvert.ToInt32(info.Result))
                            {
                                case 1:
                                    this.neuLabel47.Text = "√";
                                    this.neuLabel46.Visible = false;
                                    this.neuLabel45.Visible = false;
                                    this.neuLabel44.Visible = false;
                                    this.neuLabel43.Visible = false;
                                    this.neuLabel42.Visible = false;
                                    break;
                                case 2:
                                    this.neuLabel47.Visible = false;
                                    this.neuLabel46.Text = "√";
                                    this.neuLabel45.Visible = false;
                                    this.neuLabel44.Visible = false;
                                    this.neuLabel43.Visible = false;
                                    this.neuLabel42.Visible = false;
                                    break;
                                case 3:
                                    this.neuLabel47.Visible = false;
                                    this.neuLabel46.Visible = false;
                                    this.neuLabel45.Text = "√";
                                    this.neuLabel44.Visible = false;
                                    this.neuLabel43.Visible = false;
                                    this.neuLabel42.Visible = false;
                                    break;
                                case 4:
                                    this.neuLabel47.Visible = false;
                                    this.neuLabel46.Visible = false;
                                    this.neuLabel45.Visible = false;
                                    this.neuLabel44.Text = "√";
                                    this.neuLabel43.Visible = false;
                                    this.neuLabel42.Visible = false;
                                    break;
                                case 5:
                                    this.neuLabel47.Visible = false;
                                    this.neuLabel46.Visible = false;
                                    this.neuLabel45.Visible = false;
                                    this.neuLabel44.Visible = false;
                                    this.neuLabel43.Text = "√";
                                    this.neuLabel42.Visible = false;
                                    break;
                                case 6:
                                    this.neuLabel47.Visible = false;
                                    this.neuLabel46.Visible = false;
                                    this.neuLabel45.Visible = false;
                                    this.neuLabel44.Visible = false;
                                    this.neuLabel43.Visible = false;
                                    this.neuLabel42.Text = "√";
                                    break;
                                default:
                                    break;
                            }
                            j++;
                            break;
                        case 3:
                            this.neuLabel54.Visible = true;
                            this.neuLabel53.Visible = true;
                            this.neuLabel52.Visible = true;
                            this.neuLabel51.Visible = true;
                            this.neuLabel50.Visible = true;
                            this.neuLabel49.Visible = true;
                            this.neuLabel41.Text = info.CureDate.ToString();//日期
                            this.neuLabel40.Text = info.DrugInfo.Name + "(" + info.Qty + this.UnitListHelper.GetName(info.Unit) + ")";//药物名称 
                            this.neuLabel55.Text = this.PeriodListHelper.GetName(info.Period);//疗程
                            switch (FS.FrameWork.Function.NConvert.ToInt32(info.Result))
                            {
                                case 1:
                                    this.neuLabel54.Text = "√";
                                    this.neuLabel53.Visible = false;
                                    this.neuLabel52.Visible = false;
                                    this.neuLabel51.Visible = false;
                                    this.neuLabel50.Visible = false;
                                    this.neuLabel49.Visible = false;
                                    break;
                                case 2:
                                    this.neuLabel54.Visible = false;
                                    this.neuLabel53.Text = "√";
                                    this.neuLabel52.Visible = false;
                                    this.neuLabel51.Visible = false;
                                    this.neuLabel50.Visible = false;
                                    this.neuLabel49.Visible = false;
                                    break;
                                case 3:
                                    this.neuLabel54.Visible = false;
                                    this.neuLabel53.Visible = false;
                                    this.neuLabel52.Text = "√";
                                    this.neuLabel51.Visible = false;
                                    this.neuLabel50.Visible = false;
                                    this.neuLabel49.Visible = false;
                                    break;
                                case 4:
                                    this.neuLabel54.Visible = false;
                                    this.neuLabel53.Visible = false;
                                    this.neuLabel52.Visible = false;
                                    this.neuLabel51.Text = "√";
                                    this.neuLabel50.Visible = false;
                                    this.neuLabel49.Visible = false;
                                    break;
                                case 5:
                                    this.neuLabel54.Visible = false;
                                    this.neuLabel53.Visible = false;
                                    this.neuLabel52.Visible = false;
                                    this.neuLabel51.Visible = false;
                                    this.neuLabel50.Text = "√";
                                    this.neuLabel49.Visible = false;
                                    break;
                                case 6:
                                    this.neuLabel54.Visible = false;
                                    this.neuLabel53.Visible = false;
                                    this.neuLabel52.Visible = false;
                                    this.neuLabel51.Visible = false;
                                    this.neuLabel50.Visible = false;
                                    this.neuLabel49.Text = "√";
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
                this.neuLabel32.Visible = false;
                this.neuLabel33.Visible = false;
                this.neuLabel34.Visible = false;
                this.neuLabel35.Visible = false;
                this.neuLabel36.Visible = false;
                this.neuLabel37.Visible = false;
                this.neuLabel47.Visible = false;
                this.neuLabel46.Visible = false;
                this.neuLabel45.Visible = false;
                this.neuLabel44.Visible = false;
                this.neuLabel43.Visible = false;
                this.neuLabel42.Visible = false;
                this.neuLabel54.Visible = false;
                this.neuLabel53.Visible = false;
                this.neuLabel52.Visible = false;
                this.neuLabel51.Visible = false;
                this.neuLabel50.Visible = false;
                this.neuLabel49.Visible = false;
            }

            #endregion

            #region 费用信息

            //Modify by lk 2008-09-12 根据统计大类编码，显示金额  有时间也可以把统计大类名称也 ：）
            ArrayList alFee = feeManager.QueryFeeInfoState(healthReord.PatientInfo.ID);
            decimal totFee = 0.0M;
            decimal qtFee = 0.0M;
            decimal zlFee = 0.0M;
            foreach (FS.HISFC.Models.RADT.Patient patientinfo in alFee)
            {
                switch (patientinfo.DIST.TrimStart('0'))
                {
                    #region
                    //case "1":
                    //    this.lblPrixycost.Text = patientinfo.IDCard;
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "2":
                    //    this.lblPrizcycost.Text = patientinfo.IDCard;
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "3":
                    //    this.lblPrizhongcaoyaocost.Text = patientinfo.IDCard;
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "4":
                    //    zlFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "5":
                    //    this.lblPrihycost.Text = patientinfo.IDCard;
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "6":
                    //    this.lblPrijccost.Text = patientinfo.IDCard;
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "7":
                    //    this.lblPrisscost.Text = patientinfo.IDCard;
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "8":
                    //    zlFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "9":
                    //    qtFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "10":
                    //    this.lblPricwcost.Text = patientinfo.IDCard;
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "11":
                    //    this.lblPrihlcost.Text = patientinfo.IDCard;
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    //case "12":
                    //    qtFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    //    break;
                    ////case "13":
                    ////    this.lblPrijscost.Text = patientinfo.IDCard;
                    ////    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    ////    break;
                    ////case "14":
                    ////    this.lblPriprccost.Text = patientinfo.IDCard;
                    ////    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    ////    break;
                    ////case "15":
                    ////    this.lblPriqtcost.Text = patientinfo.IDCard;
                    ////    totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                    ////    break;
                    //default:
                    //    break;
                    #endregion
                    case "1":
                        this.lblPricwcost.Text = patientinfo.IDCard; //床位费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "2":
                        this.lblPrihlcost.Text = patientinfo.IDCard;//护理费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "3":
                        this.lblPrixycost.Text = patientinfo.IDCard;//lblPrixycost 西药
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "4":
                        this.lblPrizcycost.Text = patientinfo.IDCard;//中成药
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "5":
                        this.lblPrizhongcaoyaocost.Text = patientinfo.IDCard;//lblPrizhongcaoyaocost 中草药
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "6":
                        this.lblPrifscost.Text = patientinfo.IDCard;//放射
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "7":
                        this.lblPrihycost.Text = patientinfo.IDCard;//lblPrihycost 化验
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "8":
                        this.lblPrisycost.Text = patientinfo.IDCard; //输氧费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "9":
                        this.lblPrisxcost.Text = patientinfo.IDCard;//输血费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "10":
                        this.lblPrizlcost.Text = patientinfo.IDCard;//治疗费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "11":
                        this.lblPrisscost.Text = patientinfo.IDCard;//lblPrisscost手术费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "12":
                        this.lblPrijscost.Text = patientinfo.IDCard;//接生费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "13":
                        this.lblPrijccost.Text = patientinfo.IDCard;//lblPrijccost检查费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "14":
                        this.lblPrimzcost.Text =patientinfo.IDCard;//麻醉费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "15":
                        this.lblPritextBox15.Text = patientinfo.IDCard;//婴儿费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "16":
                        this.lblPriprccost.Text = patientinfo.IDCard; //lblPriprccost陪床费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    case "17":
                        this.lblPriqtcost.Text = patientinfo.IDCard;//其他费
                        totFee += FS.FrameWork.Function.NConvert.ToDecimal(patientinfo.IDCard);
                        break;
                    default:
                        break;
                }
                //this.lblPrizlcost.Text = FS.NFC.Public.String.FormatNumberReturnString(zlFee, 2);
                //this.lblPriqtcost.Text = FS.NFC.Public.String.FormatNumberReturnString(qtFee, 2);
                this.lblPriTotcost.Text = FS.FrameWork.Public.String.FormatNumberReturnString(totFee, 2);
            }

            #endregion

            #region 尸检，手术治疗是否为第一例
            if (healthReord.CadaverCheck == "1")
            {
                this.lblPriShijian.Text = "1";
            }
            else
            {
                this.lblPriShijian.Text = "2";
            }
            if (healthReord.YnFirst == "1")
            {
                this.lblPriDiyili.Text = "1";
            }
            else
            {
                this.lblPriDiyili.Text = "2";
            }
            #endregion

            #region 随诊,示教病例

            if (healthReord.VisiStat == "1")
            {
                this.lblPriSuiZhen.Text = "1";
            }
            else
            {
                this.lblPriSuiZhen.Text = "2";
            }

            //随诊年月周
            this.lblPriSuizhenQixianNian.Text = healthReord.VisiPeriodYear;
            this.lblPriSuizhenQixianYue.Text = healthReord.VisiPeriodMonth;
            this.lblPriSuizhenQixianZhou.Text = healthReord.VisiPeriodWeek;

            //示教病例
            if (healthReord.TechSerc == "1")
            {
                this.lblPriShijiaoBingli.Text = "1";
            }
            else
            {
                this.lblPriShijiaoBingli.Text = "2";
            }

            #endregion

            #region 血型、输血品种

            //血型不是从常数中获取
            switch (healthReord.PatientInfo.BloodType.ID.ToString())
            {
                case "A":
                    this.lblPriXuexing.Text = "1";
                    break;
                case "1":
                    this.lblPriXuexing.Text = "1";
                    break;
                case "B":
                    this.lblPriXuexing.Text = "2";
                    break;
                case "2":
                    this.lblPriXuexing.Text = "2";
                    break;
                case "AB":
                    this.lblPriXuexing.Text = "3";
                    break;
                case "4":
                    this.lblPriXuexing.Text = "3";
                    break;
                case "O":
                    this.lblPriXuexing.Text = "4";
                    break;
                case "3":
                    this.lblPriXuexing.Text = "4";
                    break;
                case "U":
                    this.lblPriXuexing.Text = "5";
                    break;
                case "9":
                    this.lblPriXuexing.Text = "9";
                    break;
                default:
                    this.lblPriXuexing.Text = "9";
                    break;
            }
            if (healthReord.RhBlood != "")
            {
                this.lblPriRH.Text = healthReord.RhBlood;//Rh血型
            }
            else
            {
                this.lblPriRH.Text = "3";
            }
            if (healthReord.ReactionBlood == "")
            {
                this.lblPriShuxueFanying.Text = "/";
            }
            else
            {
                this.lblPriShuxueFanying.Text = healthReord.ReactionBlood;// 输血反应
            }
            if (healthReord.ReactionLiquid == "")//输液反应
            {
                this.lblPriShuyeFanying.Text = "/";
            }
            else
            {
                this.lblPriShuyeFanying.Text = healthReord.ReactionLiquid;
            }
            //输血品种
            this.lblPriShuxuePinzhongHongxibao.Text = healthReord.BloodRed;//红细胞
            this.lblPriShuxuePinzhongXuexiaoban.Text = healthReord.BloodPlatelet;//血小板
            this.lblPriShuxuePinzhongXuejiang.Text = healthReord.BloodPlasma;//血浆
            this.lblPriShuxuePinzhongXueQuanxue.Text = healthReord.BloodWhole;//全血
            this.lblPriShuxuePinzhongQita.Text = healthReord.BloodOther;//其他
            this.txtYuanjiHuizhen.Text = healthReord.InconNum.ToString();//院际会诊
            this.txtYuanchengHuizhen.Text = healthReord.OutconNum.ToString();//远程会诊
            this.SuperNus.Text = healthReord.SuperNus.ToString();//特级护理
            this.INus.Text = healthReord.INus.ToString();//一级护理
            this.IINus.Text = healthReord.IINus.ToString();//二级护理
            this.IIINus.Text = healthReord.IIINus.ToString();//三级护理
            this.ICU.Text = healthReord.StrictNuss.ToString();//重症监护
            this.CCU.Text = healthReord.SpecalNus.ToString();//特殊护理
            #endregion

            txtOpIf.Text = healthReord.DeadKind;
        }
        /// <summary>
        /// 清空控件
        /// </summary>
        private void Clear()
        {

            #region 婴儿信息
            ////查询符合条件的数据  如果是一胞胎后面婴儿信息就不要显示
            //性别
            this.txtBabySexM11.Text = "";
            this.txtBabySexF12.Text = "";
            this.txtBirEndH13.Text = "";
            this.txtBirEndD14.Text = "";
            this.txtBirEndD15.Text = "";
            //婴儿体重
            this.txtWeight16.Text = "";
            //婴儿转归
            this.txtBabyStateD17.Text = "";
            this.txtBabyStateZ18.Text = "";
            this.txtBabyStateC19.Text = "";
            //呼吸
            this.txtBreathZ110.Text = "";
            this.txtBreathZS111.Text = "";
            this.txtBreathZS112.Text = "";
            //感染次数
            this.txtInfech113.Text = "";
            //主要感染名称
            this.txtInfechName114.Text = "";

            //ICD10
            this.txtIcd115.Text = "";
            //抢救次数
            this.txtSalvTimes116.Text = "";
            //成功抢救次数
            this.txtSuccTimes117.Text = "";
            //性别
            this.txtBabySexM21.Text = "";
            this.txtBabySexF22.Text = "";
            //分娩结果
            this.txtBirEndH23.Text = "";
            this.txtBirEndD24.Text = "";
            this.txtBirEndD25.Text = "";
            //婴儿体重
            this.txtWeight26.Text = "";
            //婴儿转归
            this.txtBabyStateD27.Text = "";
            this.txtBabyStateZ28.Text = "";
            this.txtBabyStateC29.Text = "";
            //呼吸
            this.txtBreathZ210.Text = "";
            this.txtBreathZS211.Text = "";
            this.txtBreathZS212.Text = "";
            //感染次数
            this.txtInfech213.Text = "";
            //主要感染名称
            this.txtInfechName214.Text = "";

            //ICD10
            this.txtIcd215.Text = "";
            //抢救次数
            this.txtSalvTimes216.Text = "";
            //成功抢救次数
            this.txtSuccTimes217.Text = "";
            //性别
            this.txtBabySexM31.Text = "";
            this.txtBabySexF32.Text = "";
            //分娩结果
            this.txtBirEndH33.Text = "";
            this.txtBirEndD34.Text = "";
            this.txtBirEndD35.Text = "";
            //婴儿体重
            this.txtWeight36.Text = "";
            //婴儿转归
            this.txtBabyStateD37.Text = "";
            this.txtBabyStateZ38.Text = "";
            this.txtBabyStateC39.Text = "";
            //呼吸
            this.txtBreathZ310.Text = "";
            this.txtBreathZS311.Text = "";
            this.txtBreathZS312.Text = "";
            //感染次数
            this.txtInfech313.Text = "";
            //主要感染名称
            this.txtInfechName314.Text = "";

            //ICD10
            this.txtIcd315.Text = "";
            //抢救次数
            this.txtSalvTimes316.Text = "";
            //成功抢救次数
            this.txtSuccTimes317.Text = "";
            //性别
            this.txtBabySexM41.Text = "";
            this.txtBabySexF42.Text = "";
            //分娩结果
            this.txtBirEndH43.Text = "";
            this.txtBirEndD44.Text = "";
            this.txtBirEndD45.Text = "";
            //婴儿体重
            this.txtWeight46.Text = "";
            //婴儿转归
            this.txtBabyStateD47.Text = "";
            this.txtBabyStateZ48.Text = "";
            this.txtBabyStateC49.Text = "";
            //呼吸
            this.txtBreathZ410.Text = "";
            this.txtBreathZS411.Text = "";
            this.txtBreathZS412.Text = "";
            //感染次数
            this.txtInfech413.Text = "";
            //主要感染名称
            this.txtInfechName414.Text = "";

            //ICD10
            this.txtIcd415.Text = "";
            //抢救次数
            this.txtSalvTimes416.Text = "";
            //成功抢救次数
            this.txtSuccTimes417.Text = "";
            #endregion

            #region 手术信息
            //编码
            this.txtOP11.Text = "";
            //日期
            this.txtOp12.Text = "";
            //名称
            this.txtOp13.Text = "";
            //术者
            this.txtOp14.Text = "";
            //一助
            this.txtOp15.Text = "";
            //二助
            this.txtOp16.Text = "";
            //麻醉方式
            this.txtOp17.Tag = "";
            //切口愈合等级
            this.txtOp18.Tag = "";
            this.txtOp19.Tag = "";
            //麻醉医师
            this.txtOp110.Text = "";
            //编码
            this.txtOp21.Text = "";
            //日期
            this.txtOp22.Text = "";
            //名称
            this.txtOp23.Text = "";
            //术者
            this.txtOp24.Text = "";
            //一助
            this.txtOp25.Text = "";
            //二助
            this.txtOp26.Text = "";
            //麻醉方式
            this.txtOp27.Tag = "";

            //切口愈合等级
            this.txtOp28.Tag = "";
            this.txtOp29.Tag = "";
            //麻醉医师
            this.txtOp210.Text = "";
            //编码
            this.txtOp31.Text = "";
            //日期
            this.txtOp32.Text = "";
            //名称
            this.txtOp33.Text = "";
            //术者
            this.txtOp34.Text = "";
            //一助
            this.txtOp35.Text = "";
            //二助
            this.txtOp36.Text = "";
            //麻醉方式
            this.txtOp37.Tag = "";

            //切口愈合等级
            this.txtOp38.Tag = "";
            this.txtOp39.Tag = "";
            //麻醉医师
            this.txtOp310.Text = "";
            //编码
            this.txtOp41.Text = "";
            //日期
            this.txtOp42.Text = "";
            //名称
            this.txtOp43.Text = "";
            //术者
            this.txtOp44.Text = "";
            //一助
            this.txtOp45.Text = "";
            //二助
            this.txtOp46.Text = "";
            //麻醉方式
            this.txtOp47.Tag = "";
            //切口愈合等级
            this.txtOp48.Tag = "";
            this.txtOp49.Tag = "";
            //麻醉医师
            this.txtOp410.Text = "";
            //编码
            this.txtOp51.Text = "";
            //日期
            this.txtOp52.Text = "";
            //名称
            this.txtOp53.Text = "";
            //术者
            this.txtOp54.Text = "";
            //一助
            this.txtOp55.Text = "";
            //二助
            this.txtOp56.Text = "";
            //麻醉方式
            this.txtOp57.Tag = "";

            //切口愈合等级
            this.txtOp58.Tag = "";
            this.txtOp59.Tag = "";
            //麻醉医师
            this.txtOp510.Text = "";

            this.txtOp17_1.Text = "";
            this.txtOp17_2.Text = "";
            this.txtOp27_1.Text = "";
            this.txtOp27_2.Text = "";
            this.txtOp37_1.Text = "";
            this.txtOp37_2.Text = "";
            this.txtOp47_1.Text = "";
            this.txtOp47_2.Text = "";
            this.txtOp57_1.Text = "";
            this.txtOp57_2.Text = "";
            #endregion
            #region 肿瘤信息
            this.neuLabel56.Text = ""; //放疗方式
            this.neuLabel59.Text = "";//放疗程式
            this.neuLabel76.Text = "";//放疗装置

            this.neuLabel1.Text = "";	//原发灶gy剂量
            this.neuLabel2.Text = ""; //原发灶次数
            this.neuLabel3.Text = "";	//原发灶天数

            this.neuLabel4.Text = string.Empty;//原发灶开始时间
            this.neuLabel5.Text = string.Empty;//原发灶开始时间
            this.neuLabel6.Text = string.Empty;//原发灶结束时间
            this.neuLabel7.Text = string.Empty;//原发灶结束时间
            this.neuLabel8.Text = string.Empty;//原发灶结束时间
            this.neuLabel9.Text = string.Empty;//原发灶结束时间


            this.neuLabel10.Text = ""; //区域淋巴结gy剂量
            this.neuLabel11.Text = "";	//区域淋巴结次数
            this.neuLabel12.Text = "";		//区域淋巴结天数


            this.neuLabel13.Text = string.Empty;//区域淋巴结开始时间
            this.neuLabel14.Text = string.Empty;//区域淋巴结开始时间
            this.neuLabel15.Text = string.Empty;//区域淋巴结开始时间
            this.neuLabel16.Text = string.Empty;//区域淋巴结结束时间
            this.neuLabel17.Text = string.Empty;//区域淋巴结结束时间
            this.neuLabel18.Text = string.Empty;//区域淋巴结结束时间

            this.neuLabel20.Text = ""; //转移灶gy剂量
            this.neuLabel21.Text = "";		//转移灶次数
            this.neuLabel22.Text = "";	//转移灶天数


            this.neuLabel23.Text = string.Empty;//转移灶开始时间
            this.neuLabel24.Text = string.Empty;//转移灶开始时间
            this.neuLabel25.Text = string.Empty;//转移灶开始时间
            this.neuLabel26.Text = string.Empty;//转移灶结束时间
            this.neuLabel27.Text = string.Empty;//转移灶结束时间
            this.neuLabel28.Text = string.Empty;//转移灶结束时间

            this.neuLabel60.Text = "";	//化疗方式
            this.neuLabel67.Text = "";	//化疗方法

            this.neuLabel29.Text = "";//日期
            this.neuLabel30.Text = "";//药物名称 
            this.neuLabel31.Text = "";//疗程
            this.neuLabel32.Text = "";
            this.neuLabel33.Text = "";
            this.neuLabel34.Text = "";
            this.neuLabel35.Text = "";
            this.neuLabel36.Text = "";
            this.neuLabel37.Text = "";
            this.neuLabel39.Text = "";//日期
            this.neuLabel38.Text = "";//药物名称 
            this.neuLabel48.Text = "";//疗程
            this.neuLabel47.Text = "";
            this.neuLabel46.Text = "";
            this.neuLabel45.Text = "";
            this.neuLabel44.Text = "";
            this.neuLabel43.Text = "";
            this.neuLabel42.Text = "";
            this.neuLabel41.Text = "";//日期
            this.neuLabel40.Text = "";//药物名称 
            this.neuLabel55.Text = "";//疗程

            this.neuLabel54.Text = "";
            this.neuLabel53.Text = "";
            this.neuLabel52.Text = "";
            this.neuLabel51.Text = "";
            this.neuLabel50.Text = "";
            this.neuLabel49.Text = "";


            #endregion

            #region 费用信息

            //Modify by lk 2008-09-12 根据统计大类编码，显示金额  有时间也可以把统计大类名称也 ：）
            this.lblPrixycost.Text = "0";
            this.lblPrizcycost.Text = "0";
            this.lblPrizhongcaoyaocost.Text = "0";
            this.lblPrihycost.Text = "0";
            this.lblPrijccost.Text = "0";
            this.lblPrisscost.Text = "0";
            this.lblPricwcost.Text = "0";
            this.lblPrihlcost.Text = "0";
            this.lblPrifscost.Text = "0";
            this.lblPrizlcost.Text = "0";
            this.lblPriqtcost.Text = "0";
            this.lblPriTotcost.Text = "0";
            this.lblPrisycost.Text = "0";
            this.lblPrisxcost.Text = "0";
            this.lblPrijscost.Text = "0";
            this.lblPrimzcost.Text = "0";
            this.lblPritextBox15.Text = "0";
            #endregion

            #region 尸检，手术治疗是否为第一例
            this.lblPriShijian.Text = "";

            this.lblPriDiyili.Text = "";

            #endregion

            #region 随诊,示教病例
            this.lblPriSuiZhen.Text = "";

            //随诊年月周
            this.lblPriSuizhenQixianNian.Text = "";
            this.lblPriSuizhenQixianYue.Text = "";
            this.lblPriSuizhenQixianZhou.Text = "";

            //示教病例
            this.lblPriShijiaoBingli.Text = "";


            #endregion

            #region 血型、输血品种

            //血型不是从常数中获取
            this.lblPriXuexing.Text = "";
            this.lblPriXuexing.Text = "";
            this.lblPriXuexing.Text = "";
            this.lblPriXuexing.Text = "";
            this.lblPriXuexing.Text = "";
            this.lblPriXuexing.Text = "";



            this.lblPriRH.Text = "";//Rh血型


            this.lblPriShuxueFanying.Text = "";// 输血反应

            this.lblPriShuyeFanying.Text = "";//保存时借用的属性 输液反应

            //输血品种
            this.lblPriShuxuePinzhongHongxibao.Text = "";//红细胞
            this.lblPriShuxuePinzhongXuexiaoban.Text = "";//血小板
            this.lblPriShuxuePinzhongXuejiang.Text = "";//血浆
            this.lblPriShuxuePinzhongXueQuanxue.Text = "";//全血
            this.lblPriShuxuePinzhongQita.Text = "";//其他
            this.txtYuanjiHuizhen.Text = "";//院际会诊
            this.txtYuanchengHuizhen.Text = "";//远程会诊
            this.SuperNus.Text = "";//特级护理
            this.INus.Text = "";//一级护理
            this.IINus.Text = "";//二级护理
            this.IIINus.Text = "";//三级护理
            this.ICU.Text = "";//重症监护
            this.CCU.Text = "";//特殊护理
            #endregion

            txtOpIf.Text = "";
        }

        /// <summary>
        /// 屏蔽不打印控件

        /// </summary>
        private void SetVisible()
        {
            #region  屏蔽套打
            //this.neuPanel1.Visible = false;
            //this.label1.Visible = false;
            //this.label2.Visible = false;
            //this.label3.Visible = false;
            //this.label4.Visible = false;
            //this.label5.Visible = false;
            //this.label6.Visible = false;
            //this.label7.Visible = false;
            //this.label8.Visible = false;
            //this.label9.Visible = false;
            //this.label10.Visible = false;
            //this.label11.Visible = false;
            //this.label12.Visible = false;
            //this.label13.Visible = false;
            //this.label14.Visible = false;
            //this.label15.Visible = false;
            //this.neuPanel3Diag.Visible = false;
            //this.neuPanel17.Visible = false;
            //this.neuPanel16.Visible = false;
            //this.neuPanel15.Visible = false;
            //this.neuPanel18.Visible = false;
            //this.neuPanel14.Visible = false;
            //this.neuPanel13.Visible = false;
            //this.neuPanel12.Visible = false;
            //this.neuPanel11.Visible = false;
            //this.neuPanel10.Visible = false;
            //this.neuPanel9.Visible = false;
            //this.neuPanel2.Visible = false;
            //this.neuPanel3.Visible = false;
            //this.neuPanel5.Visible = false;
            //this.neuPanel4.Visible = false;
            //this.neuPanel6.Visible = false;
            //this.neuPanel7.Visible = false;
           
            
            //this.label189.Visible = false;
            //this.label190.Visible = false;
            //this.label191.Visible = false;
            //this.label192.Visible = false;

            //this.label18.Visible = false;
            //this.neuPanel19.Visible = false;
            //this.neuPanel44.Visible = false;
            //this.neuPanel45.Visible = false;
            //this.neuPanel20.Visible = false;
            //this.neuPanel21.Visible = false;
            //this.neuPanel22.Visible = false;
            //this.neuPanel23.Visible = false;
            //this.neuPanel24.Visible = false;
            //this.neuPanel25.Visible = false;
            //this.neuPanel26.Visible = false;
            //this.neuPanel27.Visible = false;
            //this.neuPanel28.Visible = false;
            //this.neuPanel29.Visible = false;
            //this.neuPanel30.Visible = false;
            //this.neuPanel31.Visible = false;
            //this.neuPanel32.Visible = false;
            //this.neuPanel33.Visible = false;
            //this.neuPanel34.Visible = false;
            //this.neuPanel35.Visible = false;
            //this.neuPanel36.Visible = false;
            //this.neuPanel37.Visible = false;
            //this.neuPanel38.Visible = false;
            //this.neuPanel39.Visible = false;
            //this.neuPanel40.Visible = false;
            //this.neuPanel41.Visible = false;
            //this.neuPanel42.Visible = false;
            //this.neuPanel43.Visible = false;
            //this.label19.Visible = false;
            //this.label20.Visible = false;
            //this.label21.Visible = false;
            //this.label22.Visible = false;
            //this.label79.Visible = false;
            //this.label80.Visible = false;
            //this.label81.Visible = false;
            //this.label82.Visible = false;
            //this.label23.Visible = false;
            //this.label25.Visible = false;
            //this.label26.Visible = false;
            //this.label28.Visible = false;
            //this.label29.Visible = false;
            //this.label30.Visible = false;
            //this.label32.Visible = false;
            //this.label33.Visible = false;
            //this.label35.Visible = false;
            //this.label36.Visible = false;
            //this.label38.Visible = false;
            //this.label39.Visible = false;
            //this.label40.Visible = false;
            //this.label41.Visible = false;
            //this.label42.Visible = false;
            //this.label43.Visible = false;
            //this.label45.Visible = false;
            //this.label46.Visible = false;
            //this.label48.Visible = false;
            //this.label49.Visible = false;
            //this.label51.Visible = false;
            //this.label52.Visible = false;
            //this.label53.Visible = false;
            //this.label55.Visible = false;
            //this.label56.Visible = false;
            //this.label58.Visible = false;
            //this.label59.Visible = false;
            //this.label60.Visible = false;
            //this.label61.Visible = false;
            //this.label63.Visible = false;
            //this.label64.Visible = false;
            //this.label65.Visible = false;
            //this.label66.Visible = false;
            //this.label67.Visible = false;
            //this.label68.Visible = false;
            //this.label69.Visible = false;
            //this.label70.Visible = false;
            //this.label71.Visible = false;
            //this.label72.Visible = false;
            //this.label73.Visible = false;
            //this.label74.Visible = false;
            //this.label75.Visible = false;
            //this.label76.Visible = false;
            //this.label77.Visible = false;
            //this.label78.Visible = false;

            //this.label83.Visible = false;
            
            //this.label88.Visible = false;
            //this.label89.Visible = false;
            //this.label90.Visible = false;
            //this.label91.Visible = false;
            //this.label92.Visible = false;
            //this.label93.Visible = false;
            //this.label94.Visible = false;
            //this.label95.Visible = false;
            //this.label96.Visible = false;
            //this.label97.Visible = false;
            //this.label98.Visible = false;
            //this.label99.Visible = false;
            //this.label100.Visible = false;
            //this.label101.Visible = false;
            //this.label102.Visible = false;
            //this.label103.Visible = false;
            //this.label104.Visible = false;
            //this.label105.Visible = false;
            //this.label106.Visible = false;
            //this.label107.Visible = false;
            //this.label108.Visible = false;
            //this.label109.Visible = false;
            //this.label110.Visible = false;
            //this.label111.Visible = false;
            //this.label112.Visible = false;
            //this.label113.Visible = false;
            //this.label114.Visible = false;
            //this.label115.Visible = false;
            //this.label116.Visible = false;
            //this.label117.Visible = false;
            //this.label118.Visible = false;
            //this.label119.Visible = false;
            //this.label120.Visible = false;
            //this.label121.Visible = false;
            //this.label122.Visible = false;
            //this.label123.Visible = false;
            //this.label124.Visible = false;
            //this.label125.Visible = false;
            //this.label126.Visible = false;
            //this.label127.Visible = false;
            
            //this.label129.Visible = false;
            //this.label130.Visible = false;
            //this.label131.Visible = false;
            //this.label132.Visible = false;
            //this.label133.Visible = false;
            //this.label134.Visible = false;
            //this.label135.Visible = false;
            //this.label136.Visible = false;
            //this.label137.Visible = false;
            //this.label138.Visible = false;
          
            //this.neuPanel47.Visible = false;
            //this.neuPanel48.Visible = false;
            //this.neuPanel61.Visible = false;
            //this.neuPanel68.Visible = false;
            //this.neuPanel67.Visible = false;
            //this.neuPanel66.Visible = false;
            //this.neuPanel65.Visible = false;
            //this.neuPanel64.Visible = false;
            //this.neuPanel63.Visible = false;
            //this.neuPanel62.Visible = false;
            //this.neuPanel69.Visible = false;
            //this.neuPanel70.Visible = false;
            //this.neuPanel71.Visible = false;
            
            //this.neuPanel73.Visible = false;
            //this.neuPanel74.Visible = false;


            //this.neuPanel49.Visible = false;
            //this.neuPanel50.Visible = false;
            //this.neuPanel51.Visible = false;
            //this.neuPanel52.Visible = false;
            //this.neuPanel53.Visible = false;
            //this.neuPanel54.Visible = false;
            //this.neuPanel55.Visible = false;
          
        
            //this.neuPanel58.Visible = false;
            //this.neuPanel59.Visible = false;
            //this.neuPanel60.Visible = false;
            //this.label139.Visible = false;
            //this.label140.Visible = false;
            //this.label141.Visible = false;
            //this.label142.Visible = false;
            //this.label143.Visible = false;
            //this.label144.Visible = false;
            //this.label145.Visible = false;
            //this.label146.Visible = false;
            //this.label147.Visible = false;
            //this.label148.Visible = false;
            //this.label149.Visible = false;
            //this.label150.Visible = false;
            //this.label151.Visible = false;
            //this.label152.Visible = false;
            //this.label153.Visible = false;
            //this.label154.Visible = false;
            //this.label155.Visible = false;
            //this.label156.Visible = false;
            //this.label157.Visible = false;
            //this.label158.Visible = false;
            //this.label159.Visible = false;
            //this.label160.Visible = false;
            //this.label161.Visible = false;
            //this.label162.Visible = false;
            //this.label163.Visible = false;
            //this.label164.Visible = false;
            //this.label165.Visible = false;
            //this.label166.Visible = false;
            //this.label167.Visible = false;
            //this.label168.Visible = false;
            //this.label169.Visible = false;
            //this.label170.Visible = false;
            //this.label171.Visible = false;
            //this.label172.Visible = false;
            //this.label173.Visible = false;
            //this.label174.Visible = false;
            //this.label175.Visible = false;
            //this.label176.Visible = false;
            //this.label177.Visible = false;
            //this.label178.Visible = false;
            //this.label179.Visible = false;
            //this.label180.Visible = false;
            //this.label181.Visible = false;
            //this.label182.Visible = false;
            //this.label183.Visible = false;
            //this.label184.Visible = false;
            //this.label185.Visible = false;
            //this.label186.Visible = false;
            //this.label187.Visible = false;
            //this.label188.Visible = false;

            //this.lblPriShuyeFanying.Visible = false;
            #endregion

            this.label16.Visible = false;//是否因麻醉死亡

            this.txtOpIf.Visible = false;//是否因麻醉死亡

            this.label17.Visible = false;//是否因麻醉死亡

            this.label84.Visible = false;//肿瘤分期 类型
            this.label85.Visible = false;
            this.label86.Visible = false;
            this.label87.Visible = false;
            this.label88.Visible = false;

            this.label128.Visible = false;//结束日期
            this.neuPanel72.Visible = false;//结束日期
            //费用现实金额
            //this.lblPriTotcost.Visible = false;
            //this.lblPricwcost.Visible = false;
            //this.lblPrihlcost.Visible = false;
            //this.lblPrixycost.Visible = false;
            //this.lblPrizcycost.Visible = false;
            //this.lblPrizhongcaoyaocost.Visible = false;
            //this.lblPrifscost.Visible = false;
            //this.lblPrihycost.Visible = false;
            //this.lblPrisycost.Visible = false;
            //this.lblPrisxcost.Visible = false;
            //this.lblPrizlcost.Visible = false;
            //this.lblPrisscost.Visible = false;
            //this.lblPrijscost.Visible = false;
            //this.lblPrijccost.Visible = false;
            //this.lblPrimzcost.Visible = false;
            //this.lblPritextBox15.Visible = false;
            //this.lblPriprccost.Visible = false;
            //this.lblPriqtcost.Visible = false;


        }
        #endregion

        #region IReportPrinter 成员

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
