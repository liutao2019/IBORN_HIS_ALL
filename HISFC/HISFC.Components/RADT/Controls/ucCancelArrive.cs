using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//{28C63B3A-9C64-4010-891D-46F846EA093D}
using System.Collections;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [功能描述: 取消接诊]<br></br>
    /// [创 建 者: cao-lin]<br></br>
    /// [创建时间: 2013-09-23]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucCancelArrive: FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucCancelArrive()
        {
            InitializeComponent();
        }

        private void ucPatientOut_Load(object sender, EventArgs e)
        {

        }

        #region 变量

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.HISFC.BizLogic.RADT.InPatient inpatient = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 当前患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;

        /// <summary>
        /// 参数控制类{28C63B3A-9C64-4010-891D-46F846EA093D}
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 药品业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 当前登陆人员（基本信息数据）
        /// </summary>
        private FS.HISFC.Models.Base.Employee Oper = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// 提示信息
        /// </summary>
        string Err = "";

        /// <summary>
        /// 当前患者住院流水号
        /// </summary>
        string strInpatientNo;

        #endregion


        #region 函数

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            try
            {
                this.Oper = manager.GetEmployeeInfo(this.inpatient.Operator.ID);
                if (this.Oper == null)
                {
                    MessageBox.Show("获取人员基本信息出错:" + manager.Err);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 设置患者信息到控件
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtPatientNo.Text = patientInfo.PID.PatientNO;		//病人号
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
            this.txtFreePay.Text = patientInfo.FT.LeftCost.ToString();		//剩余预交金
            this.txtTotcost.Text = patientInfo.FT.TotCost.ToString();		//总金额
            this.dtpInDate.Value = patientInfo.PVisit.InTime; //入院时间
        }

     
        /// <summary>
        /// 刷新患者信息
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void RefreshList(string inPatientNo)
        {
            try
            {
                myPatientInfo = this.inpatient.QueryPatientInfoByInpatientNO(inPatientNo);

                //如果患者已不在本科,则清空数据
                if (myPatientInfo.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
                {
                    MessageBox.Show("患者已不在本病区,请刷新当前窗口", "提示");
                    myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                }

                this.SetPatientInfo(myPatientInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {

            if (this.CheckPatientState() != 1)
            {
                MessageBox.Show(Err);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();


            #region 护士取消接诊

            //清空床位:空床,病人号为N
            FS.HISFC.Models.Base.Bed newBed = myPatientInfo.PVisit.PatientLocation.Bed.Clone();

            newBed.Status.ID = FS.HISFC.Models.Base.EnumBedStatus.U.ToString();

            newBed.InpatientNO = "N";

            //更新床位状态,并判断并发
            int i = inpatient.UpdateBedStatus(newBed, myPatientInfo.PVisit.PatientLocation.Bed);

            this.Err = inpatient.Err;

            if (i <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show(Err);

                return i;
            }

            //更新患者状态
            FS.HISFC.Models.RADT.InStateEnumService inState = new FS.HISFC.Models.RADT.InStateEnumService();

            inState.ID = FS.HISFC.Models.Base.EnumInState.R.ToString();

            i = radt.UpdatePatientState(myPatientInfo, inState);

            this.Err = radt.Err;

            if (i == -1)　//失败
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show(Err);

                return -1;
            }


            string sql = @"UPDATE fin_ipr_inmaininfo
   set bed_no          = null,
       CHARGE_DOC_CODE = null,
       CHARGE_DOC_NAME = null,
       CHIEF_DOC_CODE  = null,
       CHIEF_DOC_NAME  = null,
       HOUSE_DOC_CODE  = null,
       HOUSE_DOC_NAME  = null,
       DUTY_NURSE_CODE = null,
       DUTY_NURSE_NAME = null,
       --nurse_cell_code = null,
       --nurse_cell_name = null,
       prefixfee_date  = null,
       out_date        = null,
       TEND            = null,
       DIETETIC_MARK   = null
 WHERE inpatient_no = '{0}'
";

            sql = string.Format(sql, myPatientInfo.ID);
            i = inpatient.ExecNoQuery(sql);
            if (i == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show("撤销接诊失败！未找到患者信息！\r\n" + inpatient.Err);

                return -1;
            }
            else if (i < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show("撤销接诊失败！\r\n" + inpatient.Err);

                return -1;
            }

            //变更记录表处理
            i = inpatient.SetShiftData(myPatientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.CK, "取消接诊", myPatientInfo.PVisit.PatientLocation.Dept, myPatientInfo.PVisit.PatientLocation.NurseCell, myPatientInfo.IsBaby);

            if (i <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                MessageBox.Show(Err);

                return -1;
            }
           

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("取消接诊成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            #endregion

            
            return 1;
        }

        /// <summary>
        /// 检查患者状态
        /// </summary>
        /// <returns></returns>
        private int CheckPatientState()
        {
            #region 判断患者状态
            //重取数据库信息
            //取患者最新的住院主表信息
            myPatientInfo = this.inpatient.QueryPatientInfoByInpatientNO(this.myPatientInfo.ID);
            if (myPatientInfo == null)
            {
                this.Err = this.inpatient.Err;
                return -1;
            }
            this.Err = "";

            //如果患者已不在本科,则清空数据---当患者转科后,如果本窗口没有关闭,会出现此种情况
            if (myPatientInfo.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
            {
                this.Err = "患者已不在本病区,请刷新当前窗口";
                return -1;
            }

            //如果患者在院状态发生变化,则不允许操作
            if (myPatientInfo.PVisit.InState.ID.ToString() != "I")
            {
                this.Err = "患者信息已发生变化,请刷新当前窗口";
                return -1;
            }

            #endregion

            //#region 判断诊断
            ////// {5EE0CCC3-9A2B-4039-AA59-F779D222E3AD}  取消接诊判断有效诊断判断
            ////ArrayList allDiagNose = diagMgr.QueryDiagenoseByPateintID(myPatientInfo.ID, FS.HISFC.Models.Base.ServiceTypes.I);
            ////ArrayList allDiagNose = diagMgr.QueryDiagenoseByPateintIDSZ(myPatientInfo.ID, FS.HISFC.Models.Base.ServiceTypes.I);
            ////ArrayList allDiagNose = diagMgr.QueryCaseDiagnoseForClinicSI(myPatientInfo.ID, FS.HISFC.Models.Base.ServiceTypes.I);
            //if (allDiagNose.Count > 0)
            //{
            //    this.Err = "该患者已经下过诊断，不允许取消接诊，请通知医生办理出院";
            //    return -1;
            //}
            //#endregion

            #region 判断医嘱

            int count = this.GetOrderCount(myPatientInfo.ID);

            if (count > 0)
            {
                this.Err = "该患者已经下过医嘱，不允许取消接诊，请通知医生办理出院";
                return -1;
            }

            #endregion

            #region 判断费用

            if (myPatientInfo.FT.TotCost > 0)
            {
                this.Err = "该患者已经有费用发生，不允许取消接诊";
                return -1;
            }

            #endregion

            #region 判断手术

            count = this.GetOpsCount(myPatientInfo.ID);

            if (count > 0)
            {
                this.Err = "该患者已经有手术申请，不允许取消接诊";
                return -1;
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 获取未完成的手术申请
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <returns></returns>
        private int GetOpsCount(string inPatientNO)
        {
            string sql = @"select count(*) from met_ops_apply f
                                                                    where f.clinic_code='{0}'
                                                                    and f.ynvalid='1'
                                                                        and f.execstatus!='4'
                                                                        and f.execstatus!='5'";
            sql = string.Format(sql, inPatientNO);

            string returnValue = diagMgr.ExecSqlReturnOne(sql, "0");

            return FS.FrameWork.Function.NConvert.ToInt32(returnValue);
        }

        /// <summary>
        /// 获取医嘱数目
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private int GetOrderCount(string inpatientNo)
        {
            string strSql = @"  
  select count(*) from met_ipm_order a where a.inpatient_no = '{0}'";

            strSql = string.Format(strSql, inpatientNo);

            string returnValue = diagMgr.ExecSqlReturnOne(strSql, "0");

            return FS.FrameWork.Function.NConvert.ToInt32(returnValue);
        }


        #endregion

        #region 事件

        private void button1_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            if (this.Save() > 0)//成功
            {
                base.OnRefreshTree();
                ((Control)sender).Enabled = true;
                return;
            }
            else
            {
                if (Err != "")
                {
                    MessageBox.Show(Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            ((Control)sender).Enabled = true;

        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.strInpatientNo = (neuObject as FS.FrameWork.Models.NeuObject).ID;
            if (this.strInpatientNo != null || this.strInpatientNo != "")
            {
                try
                {
                    RefreshList(strInpatientNo);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                }
            }
            return 0;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return null;
        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid) };
            }
        }

        #endregion

    }
}
