using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [功能描述: 转科申请，取消控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucPatientShiftOut : Neusoft.FrameWork.WinForms.Controls.ucBaseControl,Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPatientShiftOut()
        {
            InitializeComponent();

            cmbNewDept.SelectedIndexChanged += new EventHandler(cmbNewDept_SelectedIndexChanged);
        }

        #region 变量
        Neusoft.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();
        Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        Neusoft.HISFC.BizLogic.RADT.InPatient inpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = null;
        private bool isCancel = false;

        /// <summary>
        /// 接口 出院、出院召回等地方的判断,是否可以执行下一步操作
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid IPatientShiftValid = null;
        
        #endregion

        #region 属性
        /// <summary>
        /// 是否取消申请
        /// </summary>
        public bool IsCancel
        {
            get
            {
                return this.isCancel;
            }
            set
            {
                this.isCancel = value;
            }
        }
        #endregion

        #region 函数
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {           
            try
            {
                ArrayList al = Neusoft.HISFC.Models.Base.SexEnumService.List();
                this.cmbNewDept.AddItems(manager.QueryDeptmentsInHos(true));
                // this.cmbNewDept.Text = "";
                //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
                this.cmbNewNurse.AddItems(manager.GetDepartment(Neusoft.HISFC.Models.Base.EnumDepartmentType.N));
                IPatientShiftValid = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid)) as Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid;
            }
            catch { }

        }

        /// <summary>
        /// 根据科室找到病区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbNewDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            Neusoft.HISFC.BizProcess.Integrate.Manager interMgr=new Neusoft.HISFC.BizProcess.Integrate.Manager();
            ArrayList alNurse = interMgr.QueryNurseStationByDept(new Neusoft.FrameWork.Models.NeuObject(this.cmbNewDept.Tag.ToString(), "", ""));
            if (alNurse != null && alNurse.Count > 0)
            {
                this.cmbNewNurse.Tag = (alNurse[0] as Neusoft.FrameWork.Models.NeuObject).ID;
            }
            else
            {
                alNurse = interMgr.QueryDepartment(this.cmbNewDept.Tag.ToString());
                if (alNurse != null && alNurse.Count > 0)
                {
                    this.cmbNewNurse.Tag = (alNurse[0] as Neusoft.FrameWork.Models.NeuObject).ID;
                }
            }
        }


        /// <summary>
        /// 将患者信息显示在控件中
        /// </summary>
        private void ShowPatientInfo()
        {
            this.txtPatientNo.Text = this.patientInfo.PID.PatientNO;		//住院号
            this.txtPatientNo.Tag = this.patientInfo.ID;							//住院流水号
            this.txtName.Text = this.patientInfo.Name;								//患者姓名
            this.txtSex.Text = this.patientInfo.Sex.Name;					//性别
            this.txtOldDept.Text = this.patientInfo.PVisit.PatientLocation.Dept.Name;//源科室名称
            this.cmbBedNo.Text = this.patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? this.patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";	//床号
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.txtOldNurse.Text = this.patientInfo.PVisit.PatientLocation.NurseCell.Name;
            //定义患者Location实体
            Neusoft.HISFC.Models.RADT.Location newLocation = new Neusoft.HISFC.Models.RADT.Location();
            //取患者转科申请信息
            newLocation = this.inpatient.QueryShiftNewLocation(this.patientInfo.ID, this.patientInfo.PVisit.PatientLocation.Dept.ID);
            this.patientInfo.User03 = newLocation.User03;
            if (this.patientInfo.User03 == null)
                this.patientInfo.User03 = "1";//申请

            if (newLocation == null)
            {
                MessageBox.Show(this.inpatient.Err);
                return;
            }
            
            this.cmbNewDept.Tag = newLocation.Dept.ID;	//新科室名称
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.cmbNewNurse.Tag = newLocation.NurseCell.ID;
            this.txtNote.Text = newLocation.Memo;		//备注
            //如果没有转科申请,则清空新科室编码
            if (newLocation.Dept.ID == "")
            {
                this.cmbNewDept.Text = null;
            }

            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (newLocation.NurseCell.ID == "")
            {
                this.cmbNewNurse.Text = null;
            }

            if (this.patientInfo.User03 != null && this.patientInfo.User03 == "0")
                this.label8.Visible = true;
            else
                this.label8.Visible = false;
        }


        /// <summary>
        /// 清屏
        /// </summary>
        public void ClearPatintInfo()
        {
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            this.cmbNewNurse.Tag = "";
            this.cmbNewNurse.Text = "";
            this.cmbNewDept.Text = "";
            this.cmbNewDept.Tag = "";
        }


        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="patientInfo"></param>
        public void RefreshList(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                //将患者信息显示在控件中
                this.ShowPatientInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }        

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as Neusoft.HISFC.Models.RADT.PatientInfo;
            RefreshList(this.patientInfo);
            return 0;
        }

        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.cmbNewDept.Tag == null || this.cmbNewDept.Tag.ToString() == "")
            {
                MessageBox.Show("请选择要转入的科室!");
                return;
            }

            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            if (this.cmbNewNurse.Tag == null || this.cmbNewNurse.Tag.ToString() == "")
            {
                MessageBox.Show("请选择要转入的病区!");
                return;
            }

            Neusoft.FrameWork.Models.NeuObject dept = new Neusoft.FrameWork.Models.NeuObject();
            dept.ID = this.cmbNewDept.Tag.ToString();
            dept.Name = this.cmbNewDept.Text;
            dept.Memo = this.txtNote.Text;
            
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            Neusoft.FrameWork.Models.NeuObject nurseCell = new Neusoft.FrameWork.Models.NeuObject();
            nurseCell.ID = this.cmbNewNurse.Tag.ToString();
            nurseCell.Name = this.cmbNewNurse.Text;

            if (!this.IsCancel)
            {
                //转科前对患者各种信息的检查
                if (this.CheckShiftOut(this.patientInfo) == -1)
                {
                    return;
                }
            }

            //是否选择的停止全部长嘱
            bool autoDC = false;
            int rtn = this.orderIntegrate.IsOwnOrders(patientInfo.ID);
            if (rtn == -1) //出错
            {
                MessageBox.Show("查询医嘱出错。");
                return;
            }

            if (!this.isAutoDcOrder && this.isUseShiftAutoDcOrder && rtn == 1)
            {
                DialogResult rev = MessageBox.Show("是否停止全部长嘱？", "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (rev == DialogResult.Cancel)
                {
                    return;
                }
                else if (rev == DialogResult.Yes)
                {
                    autoDC = true;
                }
            }

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            Neusoft.HISFC.BizProcess.Integrate.RADT radt = new Neusoft.HISFC.BizProcess.Integrate.RADT();

            if (radt.ShiftOut(this.patientInfo, dept,nurseCell ,this.patientInfo.User03, this.isCancel) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
            }

            if (this.isUseShiftAutoDcOrder)
            {
                string errInfo = "";
                if (this.isAutoDcOrder || autoDC)
                {
                    if (this.AutoDcOrder(ref errInfo) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errInfo);
                        return;
                    }
                }
            }

            Neusoft.FrameWork.Management.PublicTrans.Commit();
            if (bSaveAndClose)
            {
                dialogResult = DialogResult.OK;
                this.FindForm().Close();
                return;
            }

            MessageBox.Show(radt.Err);
            
            base.OnRefreshTree();//刷新树
        }

        /// <summary>
        /// 对患者进行转科判断 by huangchw 2012-11-20
        /// </summary>
        /// <returns></returns>
        private int CheckShiftOut(Neusoft.HISFC.Models.RADT.PatientInfo patient)
        { 
            //整理：把提示统一放到一起

            //需要提示选择的东东
            string checkMessage = "";

            //提示禁止的东东
            string stopMessage = "";

            Classes.Function funMgr = new Neusoft.HISFC.Components.RADT.Classes.Function();
            if (IPatientShiftValid != null)
            {
                bool bl = IPatientShiftValid.IsPatientShiftValid(patient, Neusoft.HISFC.Models.Base.EnumPatientShiftValid.O, ref stopMessage);
                if (!bl)
                {
                    if (!string.IsNullOrEmpty(stopMessage))
                    {
                        MessageBox.Show(stopMessage);
                    }
                    return -1;
                }
            }


            //注意不要在业务层弹出MessageBox！！！

            /*
             * 一、费用
             *  1、存在退费申请，不允许办理转科登记
             * 二、药品
             *  1、存在退药申请，不允许办理转科登记
             *  2、存在发药申请，提示是否办理转科登记
             * 三、终端确认
             *  1、存在未确认项目，不允许或提示是否允许办理转科登记
             * 
             * 对于其他情况，采用接口本地化实现
             * 1、是否长嘱全停
             * 2、是否开立出院医嘱
             * 3、是否有未审核医嘱
             * 4、判断床位数和护理费的收取是否正确
             * */


            #region 1、存在退费申请，不允许办理转科登记

            string ReturnApplyItemInfo = Neusoft.HISFC.Components.RADT.Classes.Function.CheckReturnApply(patient.ID);
            if (ReturnApplyItemInfo != null)
            {
                string[] item = ReturnApplyItemInfo.Split('\r');
                string tip = "";
                for (int i = 0; i < item.Length; i++)
                {
                    if (i <= 2)
                    {
                        tip += item[i] + "\r";
                        if (i == item.Length - 1 || i == 2)
                        {
                            tip += "  等";
                        }
                    }
                }

                checkMessage += "\r\n★存在未确认的退费申请！\r\n" + tip;
            }
            #endregion

            #region 2、存在退药申请，提示是否继续

            int returnValue = this.pharmacyIntegrate.QueryNoConfirmQuitApply(patient.ID);
            if (returnValue == -1)
            {
                MessageBox.Show("查询患者退药申请信息出错!" + this.pharmacyIntegrate.Err);

                return -1;
            }
            if (returnValue > 0) //有申请但是没有核准的退药信息
            {
                checkMessage += "\r\n\r\n★存在未审核的退药申请信息！";
            }

            #endregion

            #region 3、判断患者是存在未摆药的药品 提示是否继续

            string msg = Neusoft.HISFC.Components.RADT.Classes.Function.CheckDrugApplay(patient.ID);
            if (msg != null)
            {
                string[] item = msg.Split('\r');
                string tip = "";
                for (int i = 0; i < item.Length; i++)
                {
                    if (i <= 2)
                    {
                        tip += item[i] + "\r";
                        if (i == item.Length - 1 || i == 2)
                        {
                            tip += "   等";
                        }
                    }
                }

                checkMessage += "\r\n\r\n★存在未摆药的药品项目！\r\n" + tip;
                
            }
            #endregion

            #region 4、存在未终端确认项目，提示是否继续

            string UnConfirmItemInfo = Neusoft.HISFC.Components.RADT.Classes.Function.CheckUnConfirm(patient.ID);
            if (UnConfirmItemInfo != null)
            {
                string[] item = UnConfirmItemInfo.Split('\r');
                string tip = "";
                for (int i = 0; i < item.Length; i++)
                {
                    if (i <= 2)
                    {
                        tip += item[i] + "\r";
                        if (i == item.Length - 1 || i == 2)
                        {
                            tip += "   等";
                        }
                    }
                }

                checkMessage += "\r\n\r\n★存在未确认收费的终端项目！\r\n" + tip;
            }

            #endregion

            #region 5、判断是否开立转科医嘱

            Neusoft.HISFC.Models.Order.Inpatient.Order inOrder = null;

            int rev = this.orderIntegrate.GetShiftOutOrderType(patientInfo.ID, ref inOrder);
            int rtn = this.orderIntegrate.IsOwnOrders(patientInfo.ID);

            if (rev < 0 || rtn == -1) //若查转科出错，查全部医嘱再错，则return
            {
                MessageBox.Show("查询转科医嘱出错!\r\n" + orderIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return -1;
            }
            else if (rev == 0 && rtn == 1)//已开立过医嘱且没有转科医嘱
            {
                stopMessage += "\r\n\r\n★未开立转科医嘱！";
            }

            #endregion

            #region 6、判断长嘱是否全停

            if (!funMgr.CheckIsAllLongOrderStop(patient.ID))
            {
                stopMessage += "\r\n\r\n★" + funMgr.Err;
            }

            #endregion

            #region 7、判断是否有未审核医嘱

            if (!funMgr.CheckIsAllOrderConfirm(patient.ID))
            {
                stopMessage += "\r\n\r\n★" + funMgr.Err;
            }

            #endregion

            #region 8、判断是否有未收费的非药品医嘱执行档

            string returnStr = Neusoft.HISFC.Components.RADT.Classes.Function.CheckExecOrderCharge(patient.ID);
            if (returnStr != null)
            {
                string[] strArray = returnStr.Split(new char[3] { '|', '|', '|' });

                if (Convert.ToInt32(strArray[3]) > 0)
                {
                    checkMessage += "\r\n\r\n★存在未收费项目！\r\n" + strArray[0];
                }
            }

            #endregion

            #region 9、出院情况不允许为空

            //if (this.cmbZg.Tag == null || string.IsNullOrEmpty(cmbZg.Tag.ToString()))
            //{
            //    stopMessage += "\r\n\r\n病案室要求，出院情况不允许为空！\r\n";
            //}
            #endregion

            #region 10、存在未收费手术申请单不允许办理转科

            //            if (isCanOutWhenUnFeeUOApply != CheckState.Yes)
            //            {
            //                string sql = @"select count(*) from met_ops_apply f
            //                                                        where f.clinic_code='{0}'
            //                                                        and f.ynvalid='1'
            //                                                            and f.execstatus!='4'
            //                                                            and f.execstatus!='5'";

            //                string rev = judgeMgr.ExecSqlReturnOne(string.Format(sql, patient.ID));
            //                try
            //                {
            //                    if (Neusoft.FrameWork.Function.NConvert.ToInt32(rev) > 0)
            //                    {
            //                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
            //                        {
            //                            checkMessage += "\r\n\r\n存在未完成的手术申请单！";
            //                        }
            //                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
            //                        {
            //                            stopMessage += "\r\n\r\n存在未完成的手术申请单！";
            //                        }
            //                    }
            //                }
            //                catch
            //                {
            //                }
            //            }

            #endregion

            #region 欠费判断

            //if (isCanOutWhenLackFee != CheckState.Yes)
            //{
            //    try
            //    {
            //        if (patient.PVisit.MoneyAlert != 0 && patient.FT.LeftCost < this.patient.PVisit.MoneyAlert)
            //        {
            //            if (isCanOutWhenUnFeeUOApply == CheckState.Check)
            //            {
            //                checkMessage += "\r\n\r\n已经欠费，\r\n余额： " + patient.FT.LeftCost.ToString() + "\r\n警戒线： " + patient.PVisit.MoneyAlert.ToString();
            //            }
            //            else if (isCanOutWhenUnFeeUOApply == CheckState.No)
            //            {
            //                stopMessage += "\r\n\r\n已经欠费，\r\n余额： " + patient.FT.LeftCost.ToString() + "\r\n警戒线： " + patient.PVisit.MoneyAlert.ToString();
            //            }
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}
            #endregion

            if (!string.IsNullOrEmpty(checkMessage))
            {
                if (MessageBox.Show(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + patient.Name + "】\r\n存在以下问题未处理,是否继续办理转科？\r\n\r\n" + checkMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
                }
            }

            if (!string.IsNullOrEmpty(stopMessage))
            {
                MessageBox.Show(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + patient.Name + "】\r\n存在以下问题未处理,不能继续办理转科！\r\n\r\n" + stopMessage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return -1;
            }
            return 1;
        }

        private Neusoft.HISFC.BizProcess.Integrate.Order orderIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 出院自动停止长嘱的停止医生
        /// </summary>
        private AotuDcDoct autoDcDoct = AotuDcDoct.ExecutOutDoct;

        /// <summary>
        /// 出院自动停止长嘱的停止医生
        /// </summary>
        [Category("控件设置"), Description("出院自动停止长嘱的停止医生")]
        public AotuDcDoct AutoDcDoct
        {
            get
            {
                return autoDcDoct;
            }
            set
            {
                autoDcDoct = value;
            }
        }

        /// <summary>
        /// 是否使用转科自动停止医嘱功能
        /// </summary>
        private bool isUseShiftAutoDcOrder = false;

        /// <summary>
        /// 是否使用转科自动停止医嘱功能
        /// </summary>
        [Category("出院设置"), Description("是否使用转科自动停止医嘱功能，使用后必须开立转科医嘱才能转科！")]
        public bool IsUseShiftAutoDcOrder
        {
            get
            {
                return isUseShiftAutoDcOrder;
            }
            set
            {
                isUseShiftAutoDcOrder = value;
            }
        }

        /// <summary>
        /// 是否转科自动停止医嘱
        /// </summary>
        private bool isAutoDcOrder = false;

        /// <summary>
        /// 是否转科自动停止医嘱
        /// </summary>
        public bool IsAutoDcOrder
        {
            get
            {
                return isAutoDcOrder;
            }
            set
            {
                isAutoDcOrder = value;
            }
        }

        /// <summary>
        /// 出院自动停止全部长嘱
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int AutoDcOrder(ref string errInfo)
        {
            //开立出院医嘱的医生
            if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                Neusoft.HISFC.Models.Order.Inpatient.Order orderObj = null;
                int rev = this.orderIntegrate.GetShiftOutOrderType(patientInfo.ID, ref orderObj);
                if (rev == -1)
                {
                    errInfo = orderIntegrate.Err;
                    return -1;
                }
                else if (rev == 0)
                {
                    errInfo = "患者【" + patientInfo.Name + "】还未开立转科医嘱！";
                    return -1;
                }

                if (orderObj == null || orderObj.ReciptDoctor == null || string.IsNullOrEmpty(orderObj.ReciptDoctor.ID))
                {
                    errInfo = "患者【" + patientInfo.Name + "】还未开立转科医嘱！";
                    return -1;
                }

                if (this.orderIntegrate.AutoDcOrder(patientInfo.ID, orderObj.ReciptDoctor, this.inpatient.Operator, "", "转科自动停止") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }
            //主任医生
            else if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                if (this.patientInfo.PVisit.AttendingDirector == null ||
                    string.IsNullOrEmpty(patientInfo.PVisit.AttendingDirector.ID))
                {
                    errInfo = "患者【" + patientInfo.Name + "】没有维护主任医师，不能自动停止医嘱！";
                    return -1;
                }

                if (this.orderIntegrate.AutoDcOrder(patientInfo.ID, patientInfo.PVisit.AttendingDirector, this.inpatient.Operator, "", "转科自动停止") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }
            //管床医生
            else if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                if (this.orderIntegrate.AutoDcOrder(patientInfo.ID, patientInfo.PVisit.AdmittingDoctor, this.inpatient.Operator, "", "转科自动停止") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }

            return 1;
        }

        #endregion

        #region ITransferDeptApplyable 成员
        bool bSaveAndClose = false;
        DialogResult dialogResult = DialogResult.None;

        public Neusoft.FrameWork.Models.NeuObject Dept
        {
            get
            {
                return this.cmbNewDept.SelectedItem;
            }
        }

        public void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.InitControl();
            this.patientInfo = patientInfo.Clone();
            RefreshList(this.patientInfo);
           
        }

        public DialogResult ShowDialog()
        {
            bSaveAndClose = true;
            Neusoft.FrameWork.WinForms.Classes.Function.PopShowControl(this);
            return dialogResult;
            
        }
        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get 
            {
                return new Type[] { typeof(Neusoft.HISFC.BizProcess.Interface.ITransferDeptApplyable), 
                                    typeof(Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid) };
            }
        }

        #endregion

        private void cbxModefyNurse_CheckedChanged(object sender, EventArgs e)
        {
            cmbNewNurse.Enabled = cbxModefyNurse.Checked;
        }
    }
}
