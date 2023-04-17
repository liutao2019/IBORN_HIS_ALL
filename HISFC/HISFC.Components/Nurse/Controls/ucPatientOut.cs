using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Nurse.Controls
{
    /// <summary>
    /// [功能描述: 出院登记组件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucPatientOut : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientOut()
        {
            InitializeComponent();
        }

        private void ucPatientOut_Load(object sender, EventArgs e)
        {

        }

        #region 变量
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

        //FS.HISFC.Models.RADT.PatientInfo PatientInfo = null;
        FS.HISFC.Models.Registration.Register PatientInfo = null;
        //挂号中间层
        FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        #endregion

        #region 函数
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            try
            {
                this.cmbZg.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ZG));
                //出院登记的时间默认为系统时间
                this.dtOutDate.Value = this.dataManager.GetDateTimeFromSysDateTime();
            }
            catch { }

        }


        /// <summary>
        /// 设置患者信息到控件
        /// </summary>
        /// <param name="PatientInfo"></param>
       
        ///<summary>
        /// 设置患者信息到控件
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(FS.HISFC.Models.Registration.Register patientInfo)
        {

            //this.txtPatientNo.Text = patientInfo.PID.PatientNO;		//住院号
            this.txtPatientNo.Text =patientInfo.ID;
            this.txtCard.Text = patientInfo.PID.CardNO;	//门诊卡号
            this.txtPatientNo.Tag = patientInfo.ID;				//住院流水号
            this.txtName.Text = patientInfo.Name;						//姓名
            this.txtSex.Text = patientInfo.Sex.Name;					//性别
            this.txtIndate.Text = patientInfo.PVisit.InTime.ToString();	//入院日期
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;	//科室名称
            this.txtDept.Tag = patientInfo.PVisit.PatientLocation.Dept.ID;	//科室编码

            FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND));
            this.txtBalKind.Text = helper.GetName(patientInfo.Pact.PayKind.ID);


            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;	//床号
            //this.txtFreePay.Text = patientInfo.FT.LeftCost.ToString();		//剩余预交金
            //this.txtTotcost.Text = patientInfo.FT.TotCost.ToString();		//总金额
            this.cmbZg.Tag = patientInfo.PVisit.ZG;				//转归
            this.dtOutDate.Value = patientInfo.PVisit.OutTime;				//出院日期
        }


        /// <summary>
        /// 从控件获得出院登记信息
        /// </summary>
        private void GetOutInfo()
        {
            PatientInfo.PVisit.ZG.ID = this.cmbZg.Tag.ToString();
            PatientInfo.PVisit.ZG.Name = this.cmbZg.Text;
            PatientInfo.PVisit.PreOutTime = this.dtOutDate.Value;
            PatientInfo.PVisit.OutTime = this.dtOutDate.Value;
        }


        /// <summary>
        ///清屏
        /// </summary>
        private void ClearPatintInfo()
        {
            this.cmbZg.Text = "";
            this.cmbZg.Tag = "";
            this.dtOutDate.Value = this.dataManager.GetDateTimeFromSysDateTime();
        }


       

        string Err = "";
        /// <summary>
        /// 重写校验用
        /// </summary>
        /// <param name="Inpatient_no"></param>
        /// <returns></returns>
        public virtual int Valid(string Inpatient_no)
        {
            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            if (this.Valid(this.PatientInfo.ID) < 0)
            {
                return -1;
            }
            //如果患者不是当天出院提示
            if (this.dtOutDate.Value.Date != this.dataManager.GetDateTimeFromSysDateTime().Date)
            {
                DialogResult dr = MessageBox.Show("该患者的留观出院日期是： " +
                    this.dtOutDate.Value.ToString("yyyy年MM月dd日") + "  是否继续？", "提示",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
                if (dr == DialogResult.No)
                {
                    this.Err = "";
                    return -1;
                }
            }
            //取患者最新的住院主表信息

            ArrayList alPatientlist = new ArrayList();
            alPatientlist = this.registerIntegrate.QueryPatient(this.PatientInfo.ID);
            PatientInfo = alPatientlist[0] as FS.HISFC.Models.Registration.Register;
            if (PatientInfo == null)
            {
                this.Err = this.registerIntegrate.Err;
                return -1;
            }
            this.Err = "";

            #region {BD72C9FF-2F8D-46f3-8EE6-3AE410A4A459}
            //急诊留观不需要判断患者科室和病区---sunm
            //如果患者已不在本科,则清空数据---当患者转科后,如果本窗口没有关闭,会出现此种情况
            //if (PatientInfo.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
            //{
            //    this.Err = "患者已不在本病区,请刷新当前窗口";
            //    return -1;
            //}

            #endregion

            //如果患者在院状态发生变化,则不允许操作
            if (PatientInfo.PVisit.InState.ID.ToString() != "P")
            {
                this.Err = "患者信息已发生变化,请刷新当前窗口";
                return -1;
            }

            //取出院登记信息
            this.GetOutInfo();

            //出院登记

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(inpatient.con);
            //t.BeginTransaction();

            this.radt.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int i =this.radt.OutPatient(PatientInfo);
            this.Err = radt.Err;
            if( i== -1)　//失败
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            else if (i == 0)//取消
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "";
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            base.OnRefreshTree();//刷新树
            return 1;
        }
        #endregion

        #region 事件
        private void button1_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            if (this.Save() > 0)//成功
            {
                MessageBox.Show(Err);
                base.OnRefreshTree();
                ((Control)sender).Enabled = true;
                return;
            }
            else
            {
                if (Err != "") MessageBox.Show(Err);
            }
            ((Control)sender).Enabled = true;

        }
        string strInpatientNo;

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            //this.strInpatientNo = (neuObject as FS.FrameWork.Models.NeuObject).ID;
            FS.HISFC.Models.Registration.Register register = e.Tag as FS.HISFC.Models.Registration.Register;
            if ( register != null  )
            {
                try
                {
                    this.PatientInfo = register;
                    this.SetPatientInfo(register);  
                }
                catch (Exception ex) { this.Err = ex.Message; }

            }
            return 0;
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return null;
        }

        #endregion

    }
}
