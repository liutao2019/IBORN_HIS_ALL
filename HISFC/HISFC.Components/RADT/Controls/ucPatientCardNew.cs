using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Order.Medical;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [功能描述: 患者卡片]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucPatientCardNew : UserControl,
        FS.FrameWork.WinForms.Classes.IControlPrintable
    {
        //{46063507-0C5A-405d-BD9D-4762ADE8DE02}
        FS.HISFC.Models.RADT.PatientInfo PInfo = null;
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        FS.HISFC.BizLogic.Order.Medical.AllergyManager allergyManager = new FS.HISFC.BizLogic.Order.Medical.AllergyManager();
        FS.HISFC.BizLogic.Manager.Bed bedMgr = new FS.HISFC.BizLogic.Manager.Bed();
        string allergyStr = string.Empty;
        ArrayList allAllergyInfo = new ArrayList();
        string babyInfoStr = string.Empty;
        ArrayList allBabyInfo = new ArrayList();
        public ucPatientCardNew()
        {
            InitializeComponent();
        }

        #region IControlPrintable 成员

        public int BeginHorizontalBlankWidth
        {
            get
            {
                return 5;
            }
            set
            {
               
            }
        }

        public int BeginVerticalBlankHeight
        {
            get
            {
                return 0;
            }
            set
            {
                
            }
        }

        public System.Collections.ArrayList Components
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        public Size ControlSize
        {
            get { return new Size(212, 289); }
        }
        protected int vnum = 0;
        protected int hnum = 0;
        public object ControlValue
        {
            get
            {
                //{46063507-0C5A-405d-BD9D-4762ADE8DE02}
                return PInfo;
            }
            set
            {
                //{46063507-0C5A-405d-BD9D-4762ADE8DE02}
                PInfo = value as FS.HISFC.Models.RADT.PatientInfo;
                if (PInfo == null)
                {
                    return;
                }
                FS.HISFC.Models.Base.Bed bed = new FS.HISFC.Models.Base.Bed();
                bed = bedMgr.GetBedInfo(PInfo.PVisit.PatientLocation.Bed.ID.ToString());
                if (PInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W")
                    this.lblBed.Text = bed.SickRoom.ID + "[" + PInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "]" + "【包】";
                else
                    this.lblBed.Text = PInfo.PVisit.PatientLocation.Bed.ID.Length >= 4 ? bed.SickRoom.ID + "[" + PInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "]" : PInfo.PVisit.PatientLocation.Bed.Name + "[" + PInfo.PVisit.PatientLocation.Bed.ID + "]";
                if (PInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "U")// {43D3B083-0C3D-459d-9E2E-007368A21857}
                {
                    this.BackColor = Color.White;
                }
                if (!string.IsNullOrEmpty(PInfo.ID))
                {
                    allAllergyInfo = this.allergyManager.QueryAllergyInfo(PInfo.ID);//皮试结果显示
                    if (allAllergyInfo != null && allAllergyInfo.Count > 0)
                    {
                        allergyStr = string.Empty;
                        int i = 1;
                        foreach (AllergyInfo obj in allAllergyInfo)
                        {
                            string sympStr = string.Empty;
                            if (obj.Symptom.ID == "1")
                            {
                                sympStr = "皮试阳性";
                            }
                            else if (obj.Symptom.ID == "2")
                            {
                                sympStr = "休克";
                            }
                            else if (obj.Symptom.ID == "2")
                            {
                                sympStr = "药疹";
                            }
                            allergyStr += i +"、"+obj.Allergen.Name + "  " + sympStr + "\n";
                            i++;
                        }
                        this.AllergyDrugInfo.Visible = true;
                        this.ToolTipShow(this.AllergyDrugInfo, allergyStr);
                    }

                    allBabyInfo = radtManager.QueryBabiesByMother(PInfo.ID);//婴儿信息
                    if (allBabyInfo != null && allBabyInfo.Count > 0)
                    {
                        babyInfoStr = string.Empty;
                        int i = 1;
                        foreach (PatientInfo babyInfo in allBabyInfo)
                        {
                            if (babyInfo.Sex.ID.ToString() == "F")
                            {
                                babyInfo.Sex.Name = "女";
                            }
                            else if (babyInfo.Sex.ID.ToString() == "M")
                            {
                                babyInfo.Sex.Name = "男";
                            }
                            babyInfoStr += i + "、姓名：" + babyInfo.Name + "\n     性别：" + babyInfo.Sex.Name + "\n     出生日期：" + babyInfo.Birthday + "\n     分娩方式：" + babyInfo.DeliveryMode.Name + "\n";
                            i++;
                        }
                        this.BabyInfo.Visible = true;
                        this.ToolTipShow(this.BabyInfo, babyInfoStr);
                    }

                }
                this.lblName.Text = PInfo.Name;
                this.ToolTipShow(this.lblName, this.lblName.Text);
                this.lblSex.Text = PInfo.Sex.Name;
                //{BDE6F26B-CD80-4974-A18E-C6B62158C42F}添加合同单位
                this.lblPact.Text = PInfo.Pact.Name;
                #region {5F752A30-7971-4b65-A84B-D233EF2A4406}
                if (PInfo.Name != "")
                {
                    this.lblAge.Text = PInfo.Age;
                }
                else
                {
                    this.lblAge.Text = "";
                }

                this.lblInpatientNo.Text = PInfo.PID.PatientNO;
                string foodName = this.radtManager.GetFoodName(PInfo.ID);

                //if (foodName.Length > 0 && foodName != "-1")
                //{
                //    this.lblFood.Text = foodName;
                //}
                //else
                //{
                //    lblFood.Text = "";
                //}

                //this.lblTend.Text = PInfo.Disease.Tend.Name;
                //if (PInfo.Diagnoses != null && PInfo.Diagnoses.Count > 0)
                //{
                //    this.lblDiagnose.Text = PInfo.Diagnoses[0].ToString();
                //    //{F764A18C-FF2B-4f36-BDE2-B5C795077097}
                //    ToolTip to = new ToolTip();
                //    to.ShowAlways = true;
                //    to.SetToolTip(lblDiagnose, PInfo.Diagnoses[0].ToString());
                    
                //}
                //else
                //{
                //    this.lblDiagnose.Text = "";
                //}

                
                //{997A8EEC-A27E-492f-941A-CDEAA3CC4AE7}
                this.lblIndate.Text = PInfo.PVisit.InTime.ToString("yyyy-MM-dd");
                if (this.lblIndate.Text.Contains("0001"))
                {
                    this.lblIndate.Text = "";
                }
                //if (PInfo.Diagnoses.Count > 0) this.lblDiagnose.Text = PInfo.Diagnoses[0].ToString();
                #region {6429B24A-3573-429f-8CE3-C375549CE30F}
                //this.lblPact.Text = PInfo.Pact.Name;
                ////{C9F9006D-AE0A-4e73-9ECE-68265A7A583E}                
                //if (PInfo.ExtendFlag1 == "1")
                //{
                //    this.lblCondition.Text = "病重";
                //}
                //else if (PInfo.ExtendFlag1 == "2")
                //{
                //    this.lblCondition.Text = "病危";
                //}
                //else
                //{
                //    this.lblCondition.Text = "";
                //}
                //if (PInfo.Disease.IsAlleray)//是否过敏
                //{

                //    this.AllergyInfo.Text = "有";
                //}
                //else
                //{
                //    this.AllergyInfo.Text = "无";
                //}
                this.AllergyInfo.Text = PInfo.AllergyInfo.Length > 16 ? PInfo.AllergyInfo.Substring(0, 16) + "..." : PInfo.AllergyInfo;
                this.ToolTipShow(this.AllergyInfo, PInfo.AllergyInfo);
                //this.lblDoctor.Text = PInfo.PVisit.AdmittingDoctor.Name;
                this.txtMark.Text = PInfo.Memo.Length > 16 ? PInfo.Memo.Substring(0, 16) + "..." : PInfo.Memo;
                this.ToolTipShow(this.txtMark, PInfo.Memo);
                FS.FrameWork.WinForms.Classes.ControlParam ctrlParm = new FS.FrameWork.WinForms.Classes.ControlParam();
                List<FS.FrameWork.WinForms.Classes.ControlParam> listCtrl = new List<FS.FrameWork.WinForms.Classes.ControlParam>();
                FS.FrameWork.WinForms.Classes.ControlParamManager ctrlManager = new FS.FrameWork.WinForms.Classes.ControlParamManager();

                // {43D3B083-0C3D-459d-9E2E-007368A21857}
                //if (PInfo.Disease.Tend.Name.IndexOf("一级护理") >= 0 || PInfo.Disease.Tend.Name.IndexOf("1级护理") >= 0
                //|| PInfo.Disease.Tend.Name.IndexOf("I级护理") >= 0 || PInfo.Disease.Tend.Name.IndexOf("Ⅰ级护理") >= 0)
                //{

                //    listCtrl = ctrlManager.QueryByID("200305");
                //    if (listCtrl != null && listCtrl.Count > 0)
                //    {
                //        ctrlParm = listCtrl[0];
                //        if (ctrlParm.ParamControlKind == "颜色")
                //        {
                //            this.SetTextForeColor(Color.FromArgb(FS.FrameWork.Function.NConvert.ToInt32(ctrlParm.ParamValue)));
                //        }
                //    }
                //}
                // if (PInfo.Disease.Tend.Name.IndexOf("二级护理") >= 0 || PInfo.Disease.Tend.Name.IndexOf("2级护理") >= 0
                //|| PInfo.Disease.Tend.Name.IndexOf("II级护理") >= 0 || PInfo.Disease.Tend.Name.IndexOf("Ⅱ级护理") >= 0)
                // {
                //    listCtrl = ctrlManager.QueryByID("200306");
                //    if (listCtrl != null && listCtrl.Count > 0)
                //    {
                //        ctrlParm = listCtrl[0];
                //        if (ctrlParm.ParamControlKind == "颜色")
                //        {
                //            this.SetTextForeColor(Color.FromArgb(FS.FrameWork.Function.NConvert.ToInt32(ctrlParm.ParamValue)));
                //        }
                //    }
                //}
                // if (PInfo.Disease.Tend.Name.IndexOf("三级护理") >= 0 || PInfo.Disease.Tend.Name.IndexOf("3级护理") >= 0
                //|| PInfo.Disease.Tend.Name.IndexOf("III级护理") >= 0 || PInfo.Disease.Tend.Name.IndexOf("Ⅲ级护理") >= 0)
                // {
                //    listCtrl = ctrlManager.QueryByID("200307");
                //    if (listCtrl != null && listCtrl.Count > 0)
                //    {
                //        ctrlParm = listCtrl[0];
                //        if (ctrlParm.ParamControlKind == "颜色")
                //        {
                //            this.SetTextForeColor(Color.FromArgb(FS.FrameWork.Function.NConvert.ToInt32(ctrlParm.ParamValue)));
                //        }
                //    }
                //}

                #endregion
                #endregion


            }
        }

        public int HorizontalBlankWidth
        {
            get
            {
                return 10;
            }
            set
            {
                
            }
        }

        public int HorizontalNum
        {
            get
            {
                return hnum;
            }
            set
            {
                hnum = value;
            }
        }

        public bool IsCanExtend
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public bool IsShowGrid
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public int VerticalBlankHeight
        {
            get
            {
                return 10;
            }
            set
            {
                
            }
        }

        public int VerticalNum
        {
            get
            {
                return vnum;
            }
            set
            {
                vnum = value;
            }
        }

        #endregion

        /// <summary>
        /// {6429B24A-3573-429f-8CE3-C375549CE30F}
        /// </summary>
        /// <param name="curcolor"></param>
        private void SetTextForeColor(Color curcolor)
        {
            this.lblAge.ForeColor = curcolor;
            this.lblBed.ForeColor = curcolor;
            //this.lblDiagnose.ForeColor = curcolor;
            //this.lblDoctor.ForeColor = curcolor;
            //this.lblFood.ForeColor = curcolor;
            this.lblIndate.ForeColor = curcolor;
            this.lblInpatientNo.ForeColor = curcolor;
            this.lblName.ForeColor = curcolor;
            //this.lblPact.ForeColor = curcolor;
            this.lblSex.ForeColor = curcolor;
            //this.lblTend.ForeColor = curcolor;
            //this.lblCondition.ForeColor = curcolor;//{C9F9006D-AE0A-4e73-9ECE-68265A7A583E}
            //this.lblBed.ForeColor = curcolor;
            this.AllergyInfo.ForeColor = curcolor;
            this.txtMark.ForeColor = curcolor;

        }

        private void ucPatientCard_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.White)
            {
                return;
            }
            if (this.BackColor == System.Drawing.SystemColors.Control)
            {
                this.BackColor = Color.Blue;
            }
            else
            {
                this.BackColor = System.Drawing.SystemColors.Control;
            }
        }
        private void ToolTipShow(Control control, string Mess)// {43D3B083-0C3D-459d-9E2E-007368A21857}
        {
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 30000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 200;
            toolTip.ShowAlways = true;
            toolTip.ForeColor = Color.Red;
            toolTip.SetToolTip(control, Mess);

        }

        private void AllergyDrugInfo_Click(object sender, EventArgs e)// {43D3B083-0C3D-459d-9E2E-007368A21857}
        {
            this.ToolTipShow(this.AllergyDrugInfo, allergyStr);
        }

        private void BabyInfo_Click(object sender, EventArgs e)// {43D3B083-0C3D-459d-9E2E-007368A21857}
        {

            this.ToolTipShow(this.BabyInfo, babyInfoStr);
        }

    }
}
