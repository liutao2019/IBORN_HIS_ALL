using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucNoHisPatient : UserControl
    {
        #region 私有变量
        private SpecPatient specPatient;
        private SpecBase specBase;
        #endregion

        #region 属性
        public SpecBase SpecBase
        {
            get
            {
                return specBase;
            }
            set
            {
                specBase = value;
            }
        }

        public SpecPatient SpecPatient
        {
            get
            {
                return specPatient;
            }
            set
            {
                specPatient = value;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// Constructor Function
        /// </summary>
        public ucNoHisPatient()
        {
            InitializeComponent();
            specPatient = new SpecPatient();
            specBase = new SpecBase();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 从页面上读取标本库病人信息
        /// </summary>
        public void SetPatient()
        {
            specPatient.PatientName = txtPatName.Text.TrimStart().TrimEnd();            
       
            specPatient.ContactNum = txtContactNum.Text.Trim();
            specPatient.HomePhoneNum = txtPhoneNum.Text.Trim();
            if (txtNationality.Tag != null)
                specPatient.Nationality = txtNationality.Tag.ToString();
            if (txtNation.Tag != null)
                specPatient.Nation = txtNation.Tag.ToString();
            specPatient.IcCardNo = txtIcCardNO.Text.Trim();
            if (rbtFemale.Checked)
                specPatient.Gender = 'F';
            else
                specPatient.Gender = 'M';
            if (txtBloodType.Tag != null)
                specPatient.BloodType = txtBloodType.Tag.ToString();
            specPatient.Home = txtHome.Text;
            specPatient.Address = txtAddress.Text;
            if (txtMaritalStatus.Tag != null)
                specPatient.IsMarried = txtMaritalStatus.Tag.ToString();
        }

        /// <summary>
        /// 从页面读取标本库病人病案信息
        /// </summary>
        public void SetBase()
        {
            if (txtHcv.Tag != null)
                specBase.HCV_AB = txtHcv.Tag.ToString();
            if (txtHbs.Tag != null)
                specBase.HbSAG = txtHbs.Tag.ToString();
            if (txtHiv.Tag != null)
                specBase.Hiv_AB = txtHiv.Tag.ToString();
            if (txtRh.Tag != null)
                specBase.RHBlood = txtRh.Tag.ToString();
            specBase.X_Times = Convert.ToInt32(nudX.Value).ToString();
            specBase.MR_Times = Convert.ToInt32(nudMr.Value).ToString();
            specBase.PET_Times = Convert.ToInt32(nudPet.Value).ToString();
            specBase.ECT_Times = Convert.ToInt32(nudECT.Value).ToString();
            if (txtICD.Tag != null)
                specBase.MainDiaICD = txtICD.Tag.ToString();
            specBase.MainDiaName = txtIcdName.Text;
            if (txtCure.Tag != null)
                specBase.Main_DiagState = txtCure.Tag.ToString();
            if (chkIs30Disease.Checked)
            {
                specBase.Is30Disease = "1";
            }
            else
                specBase.Is30Disease = "0";
            if (chkIsOper.Checked)
            {
                specBase.Diagnose_Oper_Flag = "1";
            }
            else
                specBase.Diagnose_Oper_Flag = "0";
        }
        #endregion

        #region 事件
        private void ucNoHisPatient_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //查询国家列表
            ArrayList countryList = new ArrayList();
            countryList = con.GetList(FS.HISFC.Models.Base.EnumConstant.COUNTRY);
            this.txtNationality.AddItems(countryList);

            //查询民族列表
            ArrayList Nationallist = con.GetList(FS.HISFC.Models.Base.EnumConstant.NATION);
            this.txtNation.AddItems(Nationallist);

            //血型列表
            ArrayList BloodTypeList = con.GetList(FS.HISFC.Models.Base.EnumConstant.BLOODTYPE);// baseDml.GetBloodType();
            this.txtBloodType.AddItems(BloodTypeList);

            //Rh
            ArrayList rhTypeList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RHSTATE);// baseDml.GetBloodType();
            this.txtRh.AddItems(rhTypeList);

            //婚姻列表
            ArrayList MaritalStatusList = con.GetList("MaritalStatus");
            this.txtMaritalStatus.AddItems(MaritalStatusList);

            ArrayList listHbsag = con.GetList("HbsAg");
            this.txtHbs.AddItems(listHbsag);
            this.txtHcv.AddItems(listHbsag);
            this.txtHiv.AddItems(listHbsag);

            //患者的治疗情况
            ArrayList cureList = con.GetList(FS.HISFC.Models.Base.EnumConstant.ZG);
            this.txtCure.AddItems(cureList);

            //ICDCode
            FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase icdMgr = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
            ArrayList alICD = icdMgr.ICDQuery(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
            //this.txtICD.IsShowID = true;
            this.txtICD.EnterVisiable = true;
            this.txtICD.IsFind = true;
            this.txtICD.AddItems(alICD);
            //this.SetPatient();
            //this.SetBase();
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 如果是HIS病人信息初始化
        /// </summary>
        public void GetPatient()
        {
            txtPatName.Text = SpecPatient.PatientName;
           
            txtContactNum.Text = SpecPatient.ContactNum;
            txtPhoneNum.Text = SpecPatient.HomePhoneNum;
            txtNationality.Tag = SpecPatient.Nationality;
            txtNation.Tag = SpecPatient.Nation;
            txtIcCardNO.Text = SpecPatient.IcCardNo;
            if (SpecPatient.Gender == 'F')
                rbtFemale.Checked = true;
            else
                rbtMale.Checked = true;
            txtBloodType.Tag = SpecPatient.BloodType;
            txtHome.Text = SpecPatient.Home;
            txtAddress.Text = SpecPatient.Address;
            txtMaritalStatus.Tag = SpecPatient.IsMarried;
            txtBloodType.Tag = SpecPatient.BloodType;
            txtIcCardNO.Text = SpecPatient.IcCardNo;
          
        }

        /// <summary>
        /// 如果是HIS病案信息初始化
        /// </summary>
        public void GetBase()
        {
            txtHcv.Tag = SpecBase.HCV_AB;
            txtHbs.Tag = SpecBase.HbSAG;
            txtHiv.Tag = SpecBase.Hiv_AB;
            txtRh.Tag = SpecBase.RHBlood;
            nudX.Value = Convert.ToDecimal(SpecBase.X_Times);
            nudMr.Value = Convert.ToDecimal(SpecBase.MR_Times);
            nudPet.Value = Convert.ToDecimal(SpecBase.PET_Times);
            nudECT.Value = Convert.ToDecimal(SpecBase.ECT_Times);
            //txtICD.Tag = SpecBase.main;
            //txtIcdName.Text = SpecBase.Main_DiagName;
            txtCure.Tag = SpecBase.Main_DiagState;

            if (SpecBase.Is30Disease == "1")
            {
                chkIs30Disease.Checked = true;
            }
            else
                chkIs30Disease.Checked = false;
            if (SpecBase.Diagnose_Oper_Flag == "1")
            {
                chkIsOper.Checked = true;
            }
            else
                chkIsOper.Checked = false;
        }
        #endregion

    }
}
