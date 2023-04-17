using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Fee.Inpatient;

namespace FS.SOC.Local.InpatientFee.GuangZhou.ChangePact
{
    public partial class frmChangePact : FS.FrameWork.WinForms.Forms.BaseForm
    {

        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.HISFC.BizLogic.Fee.PactUnitInfo pactInfoMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        /// <summary>
        /// 住院患者信息实体
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        ChangePact.ChangePactManager chagerMgr = new ChangePactManager();
        FS.HISFC.BizLogic.Fee.InPatient inpatientMgr = new FS.HISFC.BizLogic.Fee.InPatient();


        /// <summary>
        ///待遇接口类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        #region 变量
        /// <summary>
        /// 是否保存身份变更的原始记录
        /// </summary>
        bool isCopyFeeDetail = true;

        /// <summary>
        /// 新合同单位
        /// </summary>
        FS.FrameWork.Models.NeuObject newPactObj = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 旧合同单位
        /// </summary>
        FS.FrameWork.Models.NeuObject oldPactObj = new FS.FrameWork.Models.NeuObject();

        #endregion


        public frmChangePact()
        {
            InitializeComponent();
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo1_myEvent);
        }

        void ucQueryInpatientNo1_myEvent()
        {
            this.Clear();
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                if (this.ucQueryInpatientNo1.Err == "")
                {
                    this.ucQueryInpatientNo1.Err = "此患者不在院";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo1.Err, 211);

                this.ucQueryInpatientNo1.Focus();
                return;
            }
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            if (this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.N.ToString() || this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.O.ToString())
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该患者已经出院!", 111);

                this.patientInfo.ID = null;

                return;
            }
            //界面显示基本信息
            this.SetpatientInfo(this.patientInfo);
            //费用信息
          //  this.DisplayDetail(this.patientInfo.ID);
        }

        private void frmChangePact_Load(object sender, EventArgs e)
        {
            //初始化合同单位
            this.InitPact();

        }
        /// <summary>
        /// 界面显示基本信息
        /// </summary>
        /// <param name="patientInfo">患者信息实体</param>
        protected virtual void SetpatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtName.Text = patientInfo.Name;
            this.txtOldPact.Text = patientInfo.Pact.Name;
            this.txtOldPact.Tag = patientInfo.Pact.ID;
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtNurseStation.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            this.txtDoctor.Text = patientInfo.PVisit.AdmittingDoctor.Name;
            this.txtBirthday.Text = patientInfo.Birthday.ToShortDateString();
            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;
            this.txtDateIn.Text = patientInfo.PVisit.InTime.ToShortDateString();
            this.txtPact.Text = patientInfo.Pact.Name;

            //
            this.txtTotCost.Text = patientInfo.FT.TotCost.ToString();
            this.txtPubCost.Text = patientInfo.FT.PubCost.ToString();
            this.txtPayCost.Text = patientInfo.FT.PayCost.ToString();
            this.txtOwnCost.Text = patientInfo.FT.OwnCost.ToString();

            //
            this.txtIDNO.Text = patientInfo.IDCard;//身份证号
            this.txtMedicalNo.Text = patientInfo.SSN;//医疗证号
        }

        /// <summary>
        /// 清屏
        /// </summary>
        protected virtual void Clear()
        {
            this.patientInfo = null;
            this.txtDept.Text = "";
            this.txtDoctor.Text = "";
            this.txtName.Text = "";
            this.txtNurseStation.Text = "";
            this.txtOldPact.Text = "";
            this.cmbNewPact.Text = "";
            this.txtBirthday.Text = "";
            this.txtDateIn.Text = "";
            this.txtBedNo.Text = "";
            this.txtPact.Text = "";
            this.ucQueryInpatientNo1.Focus();

        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Name=="tbQurey")
            {
                this.ChangePact();
            }
            else if (e.Button.Name == "tbQuit")
            {
                this.Close();
            }
        }
        /// <summary>
        /// 初始化合同单位
        /// </summary>
        /// <returns></returns>
        private void InitPact()
        {
            ArrayList al = new ArrayList();
            al = this.managerIntegrate.QueryPactUnitInPatient();
            if (al.Count > 0)
            {
                this.cmbNewPact.AddItems(al);
            }
        }

        private void ChangePact()
        {
            //验证有效性
            if (IsValid()==-1)
            {
                return;
            }
            this.tbQuit.Enabled = false;

            FS.HISFC.Models.Base.PactInfo selectPactObj = this.pactInfoMgr.GetPactUnitInfoByPactCode(this.cmbNewPact.SelectedItem.ID);
            if (selectPactObj.PayKind.ID=="03" && this.txtMedicalNo.Text!="")
            {
                this.txtMedicalNo.ReadOnly = false;
                MessageBox.Show("公费患者请输入医疗证号！");
                this.tbQuit.Enabled = true;
                return;
            }
            else if (selectPactObj.PayKind.ID == "02" && this.txtIDNO.Text != "")
            {
                this.txtIDNO.ReadOnly = false;
                MessageBox.Show("医保患者请输入身份证号！");
                this.tbQuit.Enabled = true;
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.pactInfoMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.chagerMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.inpatientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

             DateTime dtChange=this.pactInfoMgr.GetDateTimeFromSysDateTime();
            //备份费用明细
            if (this.isCopyFeeDetail)
            {
               
                if (chagerMgr.BackOldFeeDetail(patientInfo.ID, 0, dtChange)!=0)
                {
                    if (chagerMgr.BackOldFeeDetail(patientInfo.ID,1,dtChange)==0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.tbQuit.Enabled = true;
                        MessageBox.Show("备份费用明细出错！"+chagerMgr.Err);
                    }
                   
                }

            }

            #region 待遇算法
            FS.HISFC.Models.RADT.PatientInfo siPatientInfo = this.patientInfo.Clone();
            siPatientInfo.Pact=selectPactObj;

            long returnValue = 0;
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("请确保待遇接口存在或正常初始化初始化失败" + this.medcareInterfaceProxy.ErrMsg);
                this.Clear();
                return ;
            }
            returnValue = this.medcareInterfaceProxy.GetRegInfoInpatient(siPatientInfo);
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                //{0C35F3E3-2E72-4ae3-8809-FF3809DA2A16}
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("待遇接口获得患者信息失败" + this.medcareInterfaceProxy.ErrMsg);
                this.Clear();
                return ;
            }
             returnValue = this.medcareInterfaceProxy.UploadRegInfoInpatient(siPatientInfo);
                if (returnValue < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    //{0C35F3E3-2E72-4ae3-8809-FF3809DA2A16}
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("待遇接口上传住院登记信息失败" + this.medcareInterfaceProxy.ErrMsg);
                    this.Clear();
                    return ;
                }
                #endregion

            //查询费用明细
            //药品
             ArrayList alFeeItems = this.inpatientMgr.GetMedItemsForInpatient(patientInfo.ID, patientInfo.PVisit.InTime, dtChange);
             if (alFeeItems==null) 
	         {
		         FS.FrameWork.Management.PublicTrans.RollBack();
                 this.medcareInterfaceProxy.Rollback();
                  FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                  MessageBox.Show("查询药品费用明细失败" + this.inpatientMgr.Err);
                    this.Clear();
	         }
              //非药品
            ArrayList alUnDrug = this.inpatientMgr.QueryFeeItemLists(patientInfo.ID, patientInfo.PVisit.InTime, dtChange);

            if (alUnDrug != null && alUnDrug.Count > 0)
            {
                alFeeItems.AddRange(alUnDrug);
            }
            else
            {
                 FS.FrameWork.Management.PublicTrans.RollBack();
                 this.medcareInterfaceProxy.Rollback();
                  FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                  MessageBox.Show("查询非药品费用明细失败" + this.inpatientMgr.Err);
                    this.Clear();
            }
            //处理费用明细表
            foreach (FeeItemList feeitem in alFeeItems)
            {
                if (-1 != this.medcareInterfaceProxy.RecomputeFeeItemListInpatient(siPatientInfo, feeitem))
                {
                    //更药品费用明细表
                    if (-1 == this.chagerMgr.UpdateFeeItemList(siPatientInfo, feeitem))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        //{0C35F3E3-2E72-4ae3-8809-FF3809DA2A16}
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("更药品费用明细表出错！" + this.chagerMgr.Err);
                    }

                }
                else 
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    //{0C35F3E3-2E72-4ae3-8809-FF3809DA2A16}
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("计算费用项目明细出错！" + this.medcareInterfaceProxy.ErrMsg);
                }
                
 
            }

            //处理费用汇总表
            ArrayList alFeeInfos = this.chagerMgr.QueryFeeInfo(siPatientInfo.ID, "0", 1);
            if (alFeeInfos == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("查询药品费用汇总失败" + this.chagerMgr.Err);
                this.Clear();
            }

            ArrayList alFeeInfoUnDrugs = this.chagerMgr.QueryFeeInfo(siPatientInfo.ID, "0", 0);
            if (alFeeInfoUnDrugs == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("查询非药品费用汇总失败" + this.chagerMgr.Err);
                this.Clear();
            }

            
            alFeeInfos.AddRange(alFeeInfoUnDrugs);
            foreach (FeeInfo fInfo in alFeeInfos)
            {
              
            }
          




            //更新主表合同单位信息
            if (this.radtIntegrate.SetPactShiftData(this.patientInfo, newPactObj, oldPactObj) != 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.tbQuit.Enabled = true;
                MessageBox.Show("调用中间层出错" + radtIntegrate.Err);
                this.Clear();
                return;
            }
            this.tbQuit.Enabled = true;
            
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <returns></returns>
        protected int IsValid()
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                MessageBox.Show("没有患者基本信息，请正确输入住院号并回车确认!");

                return -1;
            }

            //判断合同单位
            if (this.cmbNewPact.SelectedIndex < 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择合同单位"));
                return -1;
            }
            if (this.cmbNewPact.SelectedItem.ID == this.txtOldPact.Tag.ToString())
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("新合同单位与原合同单位相同，请重新选择"));
                return -1;
            }

            return 1;
        }      
    
    }
}