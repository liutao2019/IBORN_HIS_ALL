﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using FS.FrameWork.Management;
using System.Collections;
using FS.FrameWork.WinForms.Classes;

namespace FS.HISFC.Components.InpatientFee.Register
{
    /// <summary>
    /// ucUnregisterNoFee<br></br>
    /// [功能描述: 无费退院]<br></br>
    /// [创 建 者: 周雪松]<br></br>
    /// [创建时间: 2007-1-5]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>

    public partial class ucUnregisterNoFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造函数


        /// </summary>
        public ucUnregisterNoFee()
        {
            this.InitializeComponent();
        }
        #region "变量"

        /// <summary>
        /// 患者基本信息综合实体

        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 入出转integrate层

        /// </summary>
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 入出转manager层

        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager radtManger = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 住院费用业务层

        /// </summary>
        FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// 住院费用组合业务层

        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// toolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// adt接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IHE.IADT adt = null;

        #endregion

        #region 属性

        private bool isRecyclePatientNO = false;
        [Category("A.参数设置"), Description("是否回收住院号，True:回收:False:不回收")]
        public bool IsRecyclePatientNO
        {
            get
            {
                return isRecyclePatientNO;
            }
            set
            {
                isRecyclePatientNO = value;
            }
        }

        private bool isTipReyclePatientNO = false;
        [Category("A.参数设置"), Description("是否提示回收住院号，True:提示:False:不提示")]
        public bool IsTipReyclePatientNO
        {
            get
            {
                return isTipReyclePatientNO;
            }
            set
            {
                isTipReyclePatientNO = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 得到患者信息实体

        /// </summary>
        private void ucQueryInpatientNo_myEvent()
        {
            //清空
            this.Clear();

            



            

            //有效性判断


            if (!IsValid())
            {
                return;
            }

            this.SetPatientInfo();
        }

        private bool SetPatientInfo() 
        {
            //获取住院号赋值给实体
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo.InpatientNo);

            if (this.patientInfo == null)
            {
                MessageBox.Show(this.radtIntegrate.Err);
                this.ucQueryInpatientNo.Focus();

                return false;
            }

            //控件赋值患者信息


            this.EvaluteByPatientInfo(this.patientInfo);

            //判断该患者是否已经出院

            if (this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.N.ToString() || this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.O.ToString())
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("该患者已经出院!", 111);
                this.patientInfo.ID = null;
                this.ucQueryInpatientNo.Focus();

                return false;
            }

            //判断预约金

            if (this.patientInfo.FT.PrepayCost > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("预交金额大于0,不能无费退院", 111);
                this.ucQueryInpatientNo.Focus();

                return false;
            }

            //判断费用总额 不考虑费用条数|| feeInpatient.QueryBalancesByInpatientNO(this.patientInfo.ID).Count != 0
            if (this.patientInfo.FT.TotCost > 0 || this.patientInfo.FT.BalancedCost > 0)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("已发生费用或结算记录,不能无费退院", 111);
                this.ucQueryInpatientNo.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// 有效性判断

        /// </summary>
        private bool IsValid()
        {
            if (this.ucQueryInpatientNo.Text.Trim() == string.Empty) 
            {
                MessageBox.Show("请输入患者基本信息");

                return false;
            }
            
            
            if (this.ucQueryInpatientNo.InpatientNo == null || this.ucQueryInpatientNo.InpatientNo.Trim() == "")
            {
                if (this.ucQueryInpatientNo.Err == "")
                {
                    ucQueryInpatientNo.Err = "此患者不在院!";
                }
                FS.FrameWork.WinForms.Classes.Function.Msg(this.ucQueryInpatientNo.Err, 211);

                this.ucQueryInpatientNo.Focus();
                return false;
            }

            

           

            return true;
        }

        /// <summary>
        /// 清空信息
        /// </summary>
        protected virtual void Clear()
        {
            this.patientInfo = null;
            this.txtSumPreCost.Text = "";
            this.txtTotCost.Text = "";
            this.txtName.Text = "";
            this.txtDept.Text = "";
            this.txtPact.Text = "";
            this.txtBedNo.Text = "";
            this.txtOwnCost.Text = "";
            txtFreeCost.Text = "";
            txtDateIn.Text = "";
            txtDoctor.Text = "";
            this.txtPayCost.Text = "";
            this.txtPubCost.Text = "";
            //this.ucQueryInpatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
           
           
        }

        /// <summary>
        /// 利用患者信息实体进行控件赋值

        /// </summary>
        /// <param name="patientInfo">患者基本信息实体</param>
        protected virtual void EvaluteByPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            }
            //预交金总额
            this.txtSumPreCost.Text = patientInfo.FT.PrepayCost.ToString();
            //费用金额
            this.txtTotCost.Text = patientInfo.FT.TotCost.ToString();
            // 姓名
            this.txtName.Text = patientInfo.Name;
            // 科室
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            // 合同单位
            this.txtPact.Text = patientInfo.Pact.Name;
            //床号
            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;
            //自费金额
            this.txtOwnCost.Text = patientInfo.FT.OwnCost.ToString();
            //余额
            txtFreeCost.Text = patientInfo.FT.LeftCost.ToString();
            //入院日期
            txtDateIn.Text = patientInfo.PVisit.InTime.ToString();
            // 医生
            txtDoctor.Text = patientInfo.PVisit.AdmittingDoctor.Name;
            //住院号

            this.ucQueryInpatientNo.TextBox.Text = patientInfo.PID.PatientNO;

            //记帐金额
            txtPubCost.Text = patientInfo.FT.PubCost.ToString();
            //自付金额
            txtPayCost.Text = patientInfo.FT.PayCost.ToString();

        }

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("退院", "进行无费退院", FS.FrameWork.WinForms.Classes.EnumImageList.C出院登记, true, false, null);
            toolBarService.AddToolButton("清屏", "清屏", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空,true, false, null);
            toolBarService.AddToolButton("帮助", "打开帮助文件", FS.FrameWork.WinForms.Classes.EnumImageList.B帮助, true, false, null);
            toolBarService.AddToolButton("退出", "关闭登记窗口", FS.FrameWork.WinForms.Classes.EnumImageList.T退出, true, false, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "退院":

                    this.Confirm();
                    break;

                case "清屏":
                    this.Clear();
                    this.ucQueryInpatientNo.Text = "";
                    this.ucQueryInpatientNo.Focus();
                    break;

                case "帮助":

                    break;
                case "退出":

                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        
        /// <summary>
        /// 无费退院

        /// </summary>
        protected virtual void Confirm()
        {
            //有效性判断

            if (!IsValid())
            {
                return;
            }

            if (!this.SetPatientInfo()) 
            {
                return;
            }

            bool isRecycle = this.isRecyclePatientNO;

            //住院次数大于1的 说明已经有住院信息了 不需要在回收
            if (patientInfo.InTimes <= 1)
            {
                //提示是否回收住院号
                if (this.isRecyclePatientNO)
                {
                    if (this.isTipReyclePatientNO)
                    {
                        if (DialogResult.Yes == MessageBox.Show(this, "确认是否回收住院号？", "提示>>", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            isRecycle = true;
                        }
                        else
                        {
                            isRecycle = false;
                        }
                    }
                }
            }
            else
            {
                isRecycle = false;
            }


            //定义事务
            long returnValue = this.medcareInterfaceProxy.SetPactCode(this.patientInfo.Pact.ID);
            if (returnValue != 1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口取合同单位失败") + this.medcareInterfaceProxy.ErrMsg);
                return;
            }
            //FS.FrameWork.Management.Transaction t  = new Transaction(this.feeInpatient.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (radtIntegrate.UnregisterNoFee(this.patientInfo) != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                
                FS.FrameWork.WinForms.Classes.Function.Msg(this.radtManger.Err, 211);

                return;
            }

            if (isRecycle)
            {
                if (this.radtIntegrate.UnregisterChangePatientNO(this.patientInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("回收住院号错误！") + this.radtIntegrate.Err);
                    return;
                }
            }

            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            
           
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口初始化失败") + this.medcareInterfaceProxy.ErrMsg);
                return;
            }
            returnValue = this.medcareInterfaceProxy.CancelRegInfoInpatient(this.patientInfo);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("待遇接口无费退院失败") + this.medcareInterfaceProxy.ErrMsg);
                return;
            }

            #region addby xuewj 2010-3-15 

            if (InterfaceManager.GetIADT() != null)
            {
                if (InterfaceManager.GetIADT().Register(this.patientInfo, false) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this, "无费退院失败，请向系统管理员报告错误信息：" + InterfaceManager.GetIADT().Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;

                }
            }

            #endregion

            this.medcareInterfaceProxy.Commit();
            //{009FC822-DE2B-45ac-BEB7-E49F24B1605F}
            this.medcareInterfaceProxy.Disconnect();
            FS.FrameWork.Management.PublicTrans.Commit();
            this.patientInfo.ID = "";
            this.ucQueryInpatientNo.txtInputCode.Text = "";
            FS.FrameWork.WinForms.Classes.Function.Msg("无费退院成功",111);

        }

        #endregion
    }
}
