using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Base;
using FS.HISFC.BizProcess.Integrate;
using FS.HISFC.BizLogic.Operation;
using FS.HISFC.Models.Operation;
using FS.FrameWork.Models;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [功能描述: 手术申请单]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2006-11-28]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucApplicationForm : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucApplicationForm()
        {

            InitializeComponent();
            if (!this.DesignMode)
            {
                this.Reset();
                this.Init();
            }

        }

        #region 字段
        private FS.HISFC.BizProcess.Integrate.RADT radtmanager = new RADT();
        public FS.HISFC.Models.Operation.OperationAppllication applyoldMZ = null;  //{0E140FEC-2F31-4414-8401-86E78FA3ADDC} 旧门诊手术申请//{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
        private FS.FrameWork.Public.ObjectHelper payKindHelper=new FS.FrameWork.Public.ObjectHelper();
        private FS.HISFC.Models.Operation.OperationAppllication operationApplication = new FS.HISFC.Models.Operation.OperationAppllication();
        private FS.HISFC.BizProcess.Integrate.Operation.OpsDiagnose opsDiagnose = new FS.HISFC.BizProcess.Integrate.Operation.OpsDiagnose();
        private bool isNew = true;     //是否新建申请
        private FS.HISFC.BizProcess.Interface.Operation.IArrangeNotifyFormPrint arrangeFormPrint;
        private System.Windows.Forms.Control contralActive = new Control();
        private bool dirty = false;
        private FS.HISFC.BizLogic.Operation.OpsTableManage opsMgr = new OpsTableManage();
        private FS.HISFC.Models.Base.Employee var = null;
        private bool checkApplyTime = false;
        private bool checkEmergency = true;
        private bool checkDate = true;
        private bool isOwnPrivilege = true;// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
        private bool isNeedApprove = false;//是否需要审核申请单
        private bool isHavingApprove = false;//有审核权
        private bool isupdatestate = false;
        private string defaultApplyDept = string.Empty;
        private PatientInfo pinfo; //{66E970CB-3342-4c38-9B14-0E514DDC82A3}
        /// <summary>
        /// 保存后是否提示打印
        /// </summary>
        private bool isSavePrint = true;

        /// <summary>
        /// 保存后是否提示打印
        /// </summary>
        [Category("控件设置"), Description("保存后是否提示打印")]
        public bool IsSavePrint
        {
            get
            {
                return isSavePrint;
            }
            set
            {
                isSavePrint = value;
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 台类型和台序对照
        /// </summary>
        ArrayList alTableTypeCompare;
        Hashtable hsTableTypeCompare;
        /// <summary>
        /// 所有台序
        /// </summary>
        ArrayList alOrder;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PatientInfo PatientInfo
        {
            set
            {
                this.Reset();

                if (value == null)
                    return;

                #region 赋值
                this.lblName.Text = value.Name; //{66E970CB-3342-4c38-9B14-0E514DDC82A3}
                this.lblGender.Text = value.Sex.Name;
                FS.FrameWork.Management.DataBaseManger daMgr = new FS.FrameWork.Management.DataBaseManger();
                int age = daMgr.GetDateTimeFromSysDateTime().Year - value.Birthday.Year;
                this.lblAge.Text = age.ToString() + "岁";
                this.lblID.Text = value.PID.PatientNO;

                this.pinfo = value; //{66E970CB-3342-4c38-9B14-0E514DDC82A3}
                this.lblType.Text = payKindHelper.GetName(value.Pact.PayKind.ID);
                this.lblDept.Text = value.PVisit.PatientLocation.Dept.Name;
                this.lblBed.Text = value.PVisit.PatientLocation.Bed.Name;
                this.lblBalance.Text = value.FT.LeftCost.ToString();
                this.lblPhone.Text = value.PhoneHome; //{0a73b038-1b02-4881-b4e3-31728e3e8c4a}
                this.nlbOrdered.Text = this.GetOrdered(((FS.HISFC.Models.Base.Employee)(Environment.OperationManager.Operator)).Dept.ID, this.dtOperDate.Value);

               
                //手术室
                //如果操作员为手术室人员,默认手术室为操作员所在科室
                foreach (Department dept in this.cmbExeDept.alItems)
                {
                    if (dept.ID == Environment.OperatorDeptID)
                    {
                        this.cmbExeDept.Tag = dept.ID;
                        break;
                    }
                }
                //没有赋值,表明操作员不是手术室人员,默认列表中第一项
                if (this.cmbExeDept.Tag == null || this.cmbExeDept.Tag.ToString() == "")
                {
                    if (this.cmbExeDept.Items.Count > 0)
                    {
                        if (string.IsNullOrEmpty(defaultApplyDept))
                        {
                            this.cmbExeDept.SelectedIndex = 0;
                        }
                        else
                        {
                            this.cmbExeDept.Tag = defaultApplyDept;
                        }
                    }
                }
                //根据指定时间和手术室判断当天是否有正台,如无自动变为加台
                Department d = this.cmbExeDept.SelectedItem as Department;

                int num = Environment.OperationManager.GetEnableTableNum(d, value.PVisit.PatientLocation.Dept.ID, this.dtOperDate.Value);
                if (num > 0)
                    this.cmbTableType.SelectedIndex = 0;//正台
                else
                    this.cmbTableType.SelectedIndex = 1;//加台
                #endregion

                this.operationApplication.PatientInfo = value;
                this.isNew = true;


            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.Operation.OperationAppllication OperationApplication
        {
            get
            {
                if (this.operationApplication == null)
                    this.operationApplication = new OperationAppllication();

                return this.operationApplication;
            }
            set
            {
                this.operationApplication = value;


                #region 赋值

                #region 是否发送作废申请控制
                FS.HISFC.Models.Operation.OperationAppllication applicationCancelApply = Environment.OperationManager.QueryCancelApplyByAppID(value.ID);
                if (applicationCancelApply == null || string.IsNullOrEmpty(applicationCancelApply.ID))
                {
                    this.nlbCancelApply.Visible = false;
                }
                else
                {
                    this.nlbCancelApply.Visible = true;
                }
                #endregion

                //{DF7604C3-C7C6-4c8e-ADB1-A50C116BC378}
               // {66E970CB-3342-4c38-9B14-0E514DDC82A3}
                PatientInfo p;
                if (this.operationApplication.PatientSouce == "2")
                {
                    p = this.radtmanager.GetPatientInfomation(value.PatientInfo.ID);
                    this.pinfo = p;
                }
                else
                {
                    p = this.operationApplication.PatientInfo;
                    Manager manager = new Manager();
                    try
                    {
                        if (!string.IsNullOrEmpty(p.PVisit.PatientLocation.Dept.ID))
                        {
                            p.PVisit.PatientLocation.Dept = manager.GetDepartment(p.PVisit.PatientLocation.Dept.ID);
                            p.PVisit.PatientLocation.Bed.Name = " /";
                        }
                    }
                    catch
                    {
                    }
                }

                if (p == null)
                {
                    MessageBox.Show("无此患者信息!", "提示");
                    return;
                }
                this.PatientInfo = p;// this.operationApplication.PatientInfo;

                if (value.OperateKind == "1")
                { this.cmbOperKind.SelectedIndex = 0; }//择期
                else if (value.OperateKind == "2")
                { this.cmbOperKind.SelectedIndex = 1; }//急诊
                else
                { this.cmbOperKind.SelectedIndex = 2; }//感染

                if (value.DiagnoseAl.Count > 0)//第一诊断
                {
                    dirty = true;
                    this.txtDiag.Text = (value.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//诊断
                    dirty = false;
                    FS.HISFC.Models.HealthRecord.ICD icd = new FS.HISFC.Models.HealthRecord.ICD();
                    icd.ID = (value.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                    icd.Name = (value.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                    this.txtDiag.Tag = icd;

                    if (value.DiagnoseAl.Count >= 2) //第二诊断
                    {
                        //dirty = true;
                        this.txtDiag2.Text = (value.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//诊断
                        //dirty = false;
                        icd = new FS.HISFC.Models.HealthRecord.ICD();
                        icd.ID = (value.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                        icd.Name = (value.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                        this.txtDiag2.Tag = icd;
                        if (value.DiagnoseAl.Count >= 3) //第三诊断 
                        {
                            dirty = true;
                            this.txtDiag3.Text = (value.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//诊断
                            dirty = false;
                            icd = new FS.HISFC.Models.HealthRecord.ICD();
                            icd.ID = (value.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                            icd.Name = (value.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                            this.txtDiag3.Tag = icd;

                        }

                    }
                }
                if (value.OperationInfos.Count > 0) //第一手术 
                {
                    dirty = true;
                    this.txtOperation.Text = (value.OperationInfos[0] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                    dirty = false;
                    this.txtOperation.Tag = (FS.HISFC.Models.Operation.OperationInfo)value.OperationInfos[0];//手术名称

                    if (value.OperationInfos.Count >= 2) //第二手术 
                    {
                        dirty = true;
                        this.txtOperation2.Text = (value.OperationInfos[1] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                        dirty = false;
                        this.txtOperation2.Tag = (FS.HISFC.Models.Operation.OperationInfo)value.OperationInfos[1];//手术名称
                        if (value.OperationInfos.Count >= 3)//第三手术
                        {
                            dirty = true;
                            this.txtOperation3.Text = (value.OperationInfos[2] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                            dirty = false;
                            this.txtOperation3.Tag = (FS.HISFC.Models.Operation.OperationInfo)value.OperationInfos[2];//手术名称
                        }
                    }
                }
                //add by chengym 2012-5-14
                int le=value.AnesType.ID.IndexOf("|");
                if ( le> 0)
                {
                    this.cmbAnae.Tag = value.AnesType.ID.Substring(0, le);
                    value.AnesType.Name = this.cmbAnae.Text;
                    this.cmbAnae2.Tag = value.AnesType.ID.Substring(le + 1);
                }
                else
                {
                    this.cmbAnae.Tag = value.AnesType.ID;//麻醉方式
                    value.AnesType.Name = this.cmbAnae.Text;
                }

                ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
                this.cmbAnseWay.Tag = value.AnesWay;
                this.dtOperDate.Value = value.PreDate;//手术日期
                this.nlbOrdered.Text = this.GetOrdered(((FS.HISFC.Models.Base.Employee)(Environment.OperationManager.Operator)).Dept.ID,this.dtOperDate.Value);
                this.cmbExeDept.Tag = value.ExeDept.ID;//执行科室
                this.comDept.Tag = value.OperationDoctor.Dept.ID;
                if (value.TableType == "1")
                { this.cmbTableType.SelectedIndex = 0; }//正台
                else if (value.TableType == "2")
                { this.cmbTableType.SelectedIndex = 1; }//加台
                else
                { this.cmbTableType.SelectedIndex = 2; }//点台
                this.cmbDoctor.SelectedIndexChanged -= new System.EventHandler(this.cmbDoctor_SelectedIndexChanged);
                this.cmbDoctor.Tag = value.OperationDoctor.ID;//术者
                this.cmbDoctor.SelectedIndexChanged += new System.EventHandler(this.cmbDoctor_SelectedIndexChanged);
                foreach (FS.HISFC.Models.Operation.ArrangeRole role in value.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.Helper1.ToString())
                    {
                        this.cmbHelper1.Tag = role.ID;//一助
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper2.ToString())
                    {
                        this.cmbHelper2.Tag = role.ID;//二助
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper3.ToString())
                    {
                        this.cmbHelper3.Tag = role.ID;//三助
                    }
                }
                //{2F7180FD-7B71-4001-A4BB-DBA9B7D40BE8}
                this.cmbSpecial.SelectedIndex = Convert.ToInt32(value.BloodNum);//是否特殊手术
                this.cmbSpecial.Tag = int.Parse(value.BloodNum.ToString());
                this.cmbSpecial.Text = cmbSpecial.alItems[int.Parse(value.BloodNum.ToString())].ToString();
                this.cmbOrder.Text = value.BloodUnit;//台序

                if (value.IsAccoNurse)
                    this.cbxNeedQX.Checked = true;//器械护士
                if (value.IsPrepNurse)
                    this.cbxNeedXH.Checked = true;//巡回护士

                if (value.IsHeavy)//是否同意使用自费项目
                    this.cmbOwn.SelectedIndex = 0;
                else
                    this.cmbOwn.SelectedIndex = 1;

                this.rtbApplyNote.Text = value.ApplyNote;//说明

                this.cmbApplyDoct.Tag = value.ApplyDoctor.ID;//申请医生
                this.lbApplyDate.Text = value.ApplyDate.ToString("yyyy年MM月dd日 HH时mm分");

                //{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
                this.cmbIncitepe.Tag = value.InciType.ID;
                #endregion
                //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
                #region 赋值主要病种
                if (value.Eneity.Length > 0)
                {
                    string[] str = value.Eneity.Split('|');
                    for (int i = 0; i < str.Length; i++)
                    {
                        foreach (FS.FrameWork.WinForms.Controls.NeuCheckBox chk in neuPanel1.Controls)
                        {
                            if (str[i].ToString() == chk.Text)
                            {
                                chk.Checked = true;
                            }
                        }
                    }
                }
                #endregion
                #region 赋值体位和部位
                if (value.Position.Length > 0)
                {
                    string[] str1 = value.Position.Split('|');
                    //this.cmbPortPosition.Text = str1[0].ToString();
                    this.txtPortPosition.Text = str1[0].ToString();
                    this.cmbBodyPosion.Text = str1[1].ToString();
                }
                #endregion
                #region 赋值是否隔离
                if (value.IsOlation == "1")
                {
                    this.cmbIsDevide.Text = "是";
                }
                else
                {
                    this.cmbIsDevide.Text = "否";
                }
                #endregion

                //拟手术持续时间
                if (value.LastTime.Length > 0)
                {
                    string[] strTime = value.LastTime.Split('|');
                    this.txtLastTime.Text = strTime[0].ToString();
                    this.cmbUnit.Text = strTime[1].ToString();
                }
                //手术级别
                this.cmbOpType.Tag = value.OperationType.ID;
                #region 审核信息
                //审核人
                this.cmbApproveDoctor.Tag = value.ApproveDoctor.ID;
                //审核时间
                if (value.ApproveDate == System.DateTime.MinValue)
                {
                    this.dtApproveDate.Value = System.DateTime.Now;
                }
                else
                {
                    this.dtApproveDate.Value = value.ApproveDate;
                }
                //审核意见
                this.cmbApproveNote.Tag = value.ApproveNote;
                #endregion
                this.operationApplication = value;
                this.isNew = false;//修改
            }
        }

        
        //{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
        /// <summary>
        /// 根据门诊手术窗体初始化住院手术申请窗体
        /// </summary>
        public void SetOperationApplyBYMz()
        {
            if (applyoldMZ != null)
            {
                this.operationApplication.PatientSouce = "2";
              if (applyoldMZ.OperateKind == "1")
                { this.cmbOperKind.SelectedIndex = 0; }//择期
                else if (applyoldMZ.OperateKind == "2")
                { this.cmbOperKind.SelectedIndex = 1; }//急诊
                else
                { this.cmbOperKind.SelectedIndex = 2; }//感染

                if (applyoldMZ.DiagnoseAl.Count > 0)//第一诊断
                {
                    dirty = true;
                    this.txtDiag.Text = (applyoldMZ.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//诊断
                    dirty = false;
                    FS.HISFC.Models.HealthRecord.ICD icd = new FS.HISFC.Models.HealthRecord.ICD();
                    icd.ID = (applyoldMZ.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                    icd.Name = (applyoldMZ.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                    this.txtDiag.Tag = icd;

                    if (applyoldMZ.DiagnoseAl.Count >= 2) //第二诊断
                    {
                        dirty = true;
                        this.txtDiag2.Text = (applyoldMZ.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//诊断
                        dirty = false;
                        icd = new FS.HISFC.Models.HealthRecord.ICD();
                        icd.ID = (applyoldMZ.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                        icd.Name = (applyoldMZ.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                        this.txtDiag2.Tag = icd;
                        if (applyoldMZ.DiagnoseAl.Count >= 3) //第三诊断 
                        {
                            dirty = true;
                            this.txtDiag3.Text = (applyoldMZ.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;//诊断
                            dirty = false;
                            icd = new FS.HISFC.Models.HealthRecord.ICD();
                            icd.ID = (applyoldMZ.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ID;
                            icd.Name = (applyoldMZ.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).Name;
                            this.txtDiag3.Tag = icd;

                        }

                    }
                }
                if (applyoldMZ.OperationInfos.Count > 0) //第一手术 
                {
                    dirty = true;
                    this.txtOperation.Text = (applyoldMZ.OperationInfos[0] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                    dirty = false;
                    this.txtOperation.Tag = (FS.HISFC.Models.Operation.OperationInfo)applyoldMZ.OperationInfos[0];//手术名称

                    if (applyoldMZ.OperationInfos.Count >= 2) //第二手术 
                    {
                        dirty = true;
                        this.txtOperation2.Text = (applyoldMZ.OperationInfos[1] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                        dirty = false;
                        this.txtOperation2.Tag = (FS.HISFC.Models.Operation.OperationInfo)applyoldMZ.OperationInfos[1];//手术名称
                        if (applyoldMZ.OperationInfos.Count >= 3)//第三手术
                        {
                            dirty = true;
                            this.txtOperation3.Text = (applyoldMZ.OperationInfos[2] as FS.HISFC.Models.Operation.OperationInfo).OperationItem.Name;
                            dirty = false;
                            this.txtOperation3.Tag = (FS.HISFC.Models.Operation.OperationInfo)applyoldMZ.OperationInfos[2];//手术名称
                        }
                    }
                }
                //add by chengym 2012-5-14
                int le=applyoldMZ.AnesType.ID.IndexOf("|");
                if ( le> 0)
                {
                    this.cmbAnae.Tag = applyoldMZ.AnesType.ID.Substring(0, le);
                    applyoldMZ.AnesType.Name = this.cmbAnae.Text;
                    this.cmbAnae2.Tag = applyoldMZ.AnesType.ID.Substring(le + 1);
                }
                else
                {
                    this.cmbAnae.Tag = applyoldMZ.AnesType.ID;//麻醉方式
                    applyoldMZ.AnesType.Name = this.cmbAnae.Text;
                }

                ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
                this.cmbAnseWay.Tag = applyoldMZ.AnesWay;
                this.dtOperDate.Value = applyoldMZ.PreDate;//手术日期
                this.nlbOrdered.Text = this.GetOrdered(((FS.HISFC.Models.Base.Employee)(Environment.OperationManager.Operator)).Dept.ID,this.dtOperDate.Value);
                this.cmbExeDept.Tag = applyoldMZ.ExeDept.ID;//执行科室
                this.comDept.Tag = applyoldMZ.OperationDoctor.Dept.ID;
                if (applyoldMZ.TableType == "1")
                { this.cmbTableType.SelectedIndex = 0; }//正台
                else if (applyoldMZ.TableType == "2")
                { this.cmbTableType.SelectedIndex = 1; }//加台
                else
                { this.cmbTableType.SelectedIndex = 2; }//点台
                this.cmbDoctor.SelectedIndexChanged -= new System.EventHandler(this.cmbDoctor_SelectedIndexChanged);
                this.cmbDoctor.Tag = applyoldMZ.OperationDoctor.ID;//术者
                this.cmbDoctor.SelectedIndexChanged += new System.EventHandler(this.cmbDoctor_SelectedIndexChanged);
                foreach (FS.HISFC.Models.Operation.ArrangeRole role in applyoldMZ.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.Helper1.ToString())
                    {
                        this.cmbHelper1.Tag = role.ID;//一助
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper2.ToString())
                    {
                        this.cmbHelper2.Tag = role.ID;//二助
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Helper3.ToString())
                    {
                        this.cmbHelper3.Tag = role.ID;//三助
                    }
                }
                //this.cmbSpecial.SelectedIndex = int.Parse(applyoldMZ.BloodNum.ToString());//是否特殊手术
                //this.cmbSpecial.Tag = int.Parse(applyoldMZ.BloodNum.ToString());
                this.cmbSpecial.SelectedIndex = Convert.ToInt32(applyoldMZ.BloodNum);//是否特殊手术  {2F7180FD-7B71-4001-A4BB-DBA9B7D40BE8}
                this.cmbSpecial.Tag = int.Parse(applyoldMZ.BloodNum.ToString());
                this.cmbSpecial.Text = cmbSpecial.alItems[int.Parse(applyoldMZ.BloodNum.ToString())].ToString();
                this.cmbOrder.Text = applyoldMZ.BloodUnit;//台序

                if (applyoldMZ.IsAccoNurse)
                    this.cbxNeedQX.Checked = true;//器械护士
                if (applyoldMZ.IsPrepNurse)
                    this.cbxNeedXH.Checked = true;//巡回护士

                if (applyoldMZ.IsHeavy)//是否同意使用自费项目
                    this.cmbOwn.SelectedIndex = 0;
                else
                    this.cmbOwn.SelectedIndex = 1;

                this.rtbApplyNote.Text = applyoldMZ.ApplyNote;//说明

                this.cmbApplyDoct.Tag = applyoldMZ.ApplyDoctor.ID;//申请医生
                this.lbApplyDate.Text = applyoldMZ.ApplyDate.ToString("yyyy年MM月dd日 HH时mm分");

                //{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
                this.cmbIncitepe.Tag = applyoldMZ.InciType.ID;
                #endregion
                //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
                #region 赋值主要病种
                if (applyoldMZ.Eneity.Length > 0)
                {
                    string[] str = applyoldMZ.Eneity.Split('|');
                    for (int i = 0; i < str.Length; i++)
                    {
                        foreach (FS.FrameWork.WinForms.Controls.NeuCheckBox chk in neuPanel1.Controls)
                        {
                            if (str[i].ToString() == chk.Text)
                            {
                                chk.Checked = true;
                            }
                        }
                    }
                }
                #endregion
                #region 赋值体位和部位
                if (applyoldMZ.Position.Length > 0)
                {
                    string[] str1 = applyoldMZ.Position.Split('|');
                    //this.cmbPortPosition.Text = str1[0].ToString();
                    this.txtPortPosition.Text = str1[0].ToString();
                    this.cmbBodyPosion.Text = str1[1].ToString();
                }
                #endregion
                #region 赋值是否隔离
                if (applyoldMZ.IsOlation == "1")
                {
                    this.cmbIsDevide.Text = "是";
                }
                else
                {
                    this.cmbIsDevide.Text = "否";
                }
                #endregion

                //拟手术持续时间
                if (applyoldMZ.LastTime.Length > 0)
                {
                    string[] strTime = applyoldMZ.LastTime.Split('|');
                    this.txtLastTime.Text = strTime[0].ToString();
                    this.cmbUnit.Text = strTime[1].ToString();
                }
                //手术级别
                this.cmbOpType.Tag = applyoldMZ.OperationType.ID;
                #region 审核信息
                //审核人
                this.cmbApproveDoctor.Tag = applyoldMZ.ApproveDoctor.ID;
                //审核时间
                if (applyoldMZ.ApproveDate == System.DateTime.MinValue)
                {
                    this.dtApproveDate.Value = System.DateTime.Now;
                }
                else
                {
                    this.dtApproveDate.Value = applyoldMZ.ApproveDate;
                }
                //审核意见
                this.cmbApproveNote.Tag = applyoldMZ.ApproveNote;
                #endregion
              
                this.isNew = true;//修改
            }
        
        }

        
        /// <summary>
        /// 根据手术预约日期获取日期内已排手术情况
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private string GetOrdered(string deptCode,DateTime dateTime)
        {
            string ordered = string.Empty;
            ordered = Environment.OperationManager.GetOrderByApplyDeptAndTime(deptCode, dateTime);
            if(!string.IsNullOrEmpty(ordered))
            {
                ordered = ordered.Replace(",", "\n");
            }
            return ordered;
        }

        protected new bool DesignMode
        {
            get
            {
                return (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv");


            }
        }

        /// <summary>
        /// 主手术项目
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NeuObject MainOperation
        {
            set
            {
                this.txtOperation.Text = value.Name;
                this.txtOperation.Tag = value.ID;
            }
        }

        /// <summary>
        /// 是否是新申请手术
        /// </summary>
        public bool IsNew
        {
            get
            {
                return this.isNew;
            }
            set
            {
                this.isNew = value;
            }
        }
        
        [Category("控件设置"), Description("允许申请比当前时间早的手术")]
        public bool CheckApplyTime
        {
            get
            {
                return checkApplyTime;
            }
            set
            {
                checkApplyTime = value;
            }
        }

        [Category("控件设置"), Description("是否打印即更新申请单状态为已安排")]
        public bool IsUpdateSate
        {
            get
            {
                return isupdatestate;
            }
            set
            {
                isupdatestate = value;
            }
        }


        [Category("控件设置"), Description("申请时间超过截止时间，是否需要改为急诊")]
        public bool CheckEmergency
        {
            get
            {
                return checkEmergency;
            }
            set
            {
                checkEmergency = value;
            }
        }
        [Category("控件设置"), Description("周六周日不能申请周一的手术")]
        public bool CheckDate
        {
            get
            {
                return checkDate;
            }
            set
            {
                checkDate = value;
            }
        }

        // {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
        [Category("控件设置"), Description("手术级别是否按个人权限获取，否则按医生级别获取手术级别")]
        public bool IsOwnPrivilege
        {
            get
            {
                return isOwnPrivilege;
            }
            set
            {
                isOwnPrivilege = value;
            }
        }
        private string note = "前发送手术申请，否则将使用接台。";
        /// <summary>
        /// 提示文字
        /// </summary>
        public string Note
        {
            get { return this.note; }
            set
            {
                this.note = value;
                this.SetNote();
            }
        }
      

        #region 方法

        /// <summary>
        /// 初使化
        /// </summary>
        private void Init()
        {
            //台类型台序对照
           
            alTableTypeCompare = Environment.IntegrateManager.GetConstantList("TableTypeCompare");
            if (alTableTypeCompare != null)
            {
                hsTableTypeCompare = new Hashtable();
                foreach (FS.FrameWork.Models.NeuObject tableCompareInfo in alTableTypeCompare)
                {
                    if (hsTableTypeCompare.Contains(tableCompareInfo.Name))
                    {
                        ArrayList allData = hsTableTypeCompare[tableCompareInfo.Name] as ArrayList;
                        allData.Add(Environment.IntegrateManager.GetConstant("OperatoinOrder", tableCompareInfo.Memo));
                    }
                    else
                    { 
                        ArrayList allData = new ArrayList();
                        allData.Add(Environment.IntegrateManager.GetConstant("OperatoinOrder", tableCompareInfo.Memo));
                        hsTableTypeCompare.Add(tableCompareInfo.Name, allData);
                    }
                }
            }

            var = (FS.HISFC.Models.Base.Employee)opsMgr.Operator;
            //支付类型
            this.payKindHelper = new FS.FrameWork.Public.ObjectHelper(Environment.IntegrateManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND));

            ArrayList alRet;

            //麻醉类型
            alRet = Environment.IntegrateManager.GetConstantList("CASEANESTYPE");//FS.HISFC.Models.Base.EnumConstant.ANESTYPE);
            this.cmbAnae.AddItems(alRet);
            this.cmbAnae.IsListOnly = true;
            this.cmbAnae2.AddItems(alRet);
            this.cmbAnae2.IsListOnly = true;

            //麻醉类别'麻醉类别（局麻或选麻，医生申请时填写）//{B9DDCC10-3380-4212-99E5-BB909643F11B}
            alRet = Environment.IntegrateManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ANESWAY);
            this.cmbAnseWay.AddItems(alRet);
            this.cmbAnseWay.IsListOnly = true;

            ArrayList alTypeOfInfection;
            //感染类型
            alTypeOfInfection = Environment.IntegrateManager.GetConstantList("TYPEOFINFECTION");
            this.cmbSpecial.AddItems(alTypeOfInfection);
            this.cmbSpecial.IsListOnly = true;

            //手术室
            alRet = Environment.IntegrateManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.OP);
            //不加载麻醉科{08A723B7-658F-4488-9730-4A8E2748D3C2}
            ArrayList al = new ArrayList();
            al = alRet.Clone() as ArrayList;
            alRet.Clear();
            foreach (FS.HISFC.Models.Base.Department dpt in al)
            {
                if (dpt.SpecialFlag != "2" && dpt.Name.IndexOf("护士站") <= 0)
                {
                    alRet.Add(dpt);
                }
            }
            ArrayList deptList = Environment.IntegrateManager.GetDeptmentAllValid();
            this.comDept.AddItems(deptList); //加载科室

            this.cmbExeDept.AddItems(alRet);
            //this.cmbExeDept.SelectedIndex = 1;
            this.cmbExeDept.IsListOnly = true;
            if (alRet.Count == 2)
            {
                this.cmbExeDept.Text = alRet[0].ToString();
                this.cmbExeDept.Tag = alRet[1].ToString();
            }
            //术者
            ArrayList alRetTmp;
            alRetTmp = Environment.IntegrateManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            alRet.Clear(); //{A157131C-EEE9-4bb9-A47F-11595614B68C}
            foreach (FS.HISFC.Models.Base.Employee empl in alRetTmp)
            {
                if (empl.ValidState==EnumValidState.Valid)
                {
                if (empl.Dept.ID == "1001" || empl.Dept.ID == "5010" || empl.Dept.ID == "5106")
                {
                    alRet.Add(empl);
                }
                }
            }

           // alRet = Environment.IntegrateManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            this.cmbDoctor.AddItems(alRet);
            this.cmbDoctor.IsListOnly = true;
            //一助
            this.cmbHelper1.AddItems(alRet);
            this.cmbHelper1.IsListOnly = true;
            //二助          
            this.cmbHelper2.AddItems(alRet);
            this.cmbHelper2.IsListOnly = true;
            //三助手            
            this.cmbHelper3.AddItems(alRet);
            this.cmbHelper3.IsListOnly = true;
            //申请医生
            this.cmbApplyDoct.AddItems(alRet);
            this.cmbApplyDoct.IsListOnly = true;
            //审核人 2012-3-9 chengym
            this.cmbApproveDoctor.AddItems(alRet);
            this.cmbApproveDoctor.Tag = string.Empty;
            this.cmbApproveDoctor.IsListOnly = true;
            //审核时间
            this.dtApproveDate.Value = System.DateTime.Now;
            //审核意见
            alRet=Environment.IntegrateManager.GetConstantList("OperatoinApprNote");
            this.cmbApproveNote.AddItems(alRet);
            this.cmbApproveNote.IsListOnly=true;
            this.cmbApproveNote.Tag = "1";
            //切口类型{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
            alRet = Environment.IntegrateManager.GetConstantList(EnumConstant.INCITYPE);
            this.cmbIncitepe.AddItems(alRet);
            this.cmbIncitepe.IsListOnly = true;
            //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}feng.ch
            //台序从常数里面取
            alRet = Environment.IntegrateManager.GetConstantList("OperatoinOrder");
            this.cmbOrder.ClearItems();
            this.cmbOrder.AddItems(alRet);
            alOrder = alRet.Clone() as ArrayList;
            //手术类别从常数里面取
            this.cmbOperKind.ClearItems();
            alRet = Environment.IntegrateManager.GetConstantList("Operatetype");
            this.cmbOperKind.AddItems(alRet);
            //体位
            alRet = Environment.IntegrateManager.GetConstantList("OPERBODY");
            this.cmbBodyPosion.AddItems(alRet);
            //部位
            alRet = Environment.IntegrateManager.GetConstantList("OPERPORT");
            this.cmbPortPosition.AddItems(alRet);
            //手术级别
            alRet = Environment.IntegrateManager.GetConstantList("OPERATETYPE");
            this.cmbOpType.AddItems(alRet);
            ////设置注意事项
            FS.HISFC.BizProcess.Integrate.Manager ctlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            //try
            //{	//查询手术申请截至时间
            //    string control = ctlMgr.QueryControlerInfo("optime");

            //    if (control != "" && control != "-1") this.lbNote.Text = "要求在" + control + this.note;//"前发送手术申请，否则将使用接台。";

            //    if (this.cmbExeDept.Items.Count > 0)
            //    {
            //        ArrayList list = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("ExeDept");
            //        if (list.Count == 2)
            //        {
            //            this.cmbExeDept.Text = list[0].ToString();
            //            this.cmbExeDept.Tag = list[1].ToString();
            //        }
            //    }
            //}
            //catch { }
            #region 诊断
            ucDiag1 = new FS.HISFC.Components.Common.Controls.ucDiagnose();
            this.Controls.Add(ucDiag1);
            ucDiag1.Size = new Size(456, 312);
            ucDiag1.SelectItem += new FS.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(ucDiag1_SelectItem);
            ucDiag1.Init();
            ucDiag1.Visible = false;
            #endregion

            #region 手术
            ucOpItem1 = new ucOpItem();
            this.Controls.Add(ucOpItem1);
            ucOpItem1.Size = new Size(518, 338);
            ucOpItem1.SelectItem += new ucOpItem.MyDelegate(ucOpItem1_SelectItem);
            ucOpItem1.Init();
            ucOpItem1.Visible = false;
            #endregion 
            #region 手术申请单审核权
            try
            {
                //是否需要审核
                string strNeedApprove = ctlMgr.QueryControlerInfo("opappr");
                if (strNeedApprove == "1")
                {
                    List<FS.FrameWork.Models.NeuObject> myPrivDept = new List<FS.FrameWork.Models.NeuObject>();
                    string OperID = FS.FrameWork.Management.Connection.Operator.ID;
                    string OperDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    myPrivDept = Environment.IntegrateManager.QueryUserPrivCollection(OperID, "op01", OperDept);
                    if (myPrivDept.Count > 0)
                    {
                        this.groupBox1.Visible = true;
                        this.isHavingApprove = true;
                    }
                    else
                    {
                        this.groupBox1.Visible = false;
                        this.isHavingApprove = false;
                    }

                    this.isNeedApprove = true;
                }
                else
                {
                    this.groupBox1.Visible = false;
                }
            }
            catch
            {
            }

            #endregion

            #region 默认选中的手术室
            defaultApplyDept = ctlMgr.QueryControlerInfo("OPS001");
            #endregion
        }

        /// <summary>
        /// 重新设置控件
        /// </summary>
        private void Reset()
        {
            this.operationApplication = new OperationAppllication();
            this.applyoldMZ = null;//{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
            this.lblName.Text = "";
            this.lblGender.Text = "";
            this.lblAge.Text = "";
            this.lblID.Text = "";
            this.lblType.Text = "";
            this.lblDept.Text = "";
            this.lblBed.Text = "";
            this.lblBalance.Text = "";
            this.lblPhone.Text = ""; //{0a73b038-1b02-4881-b4e3-31728e3e8c4a}
            //手术类别
            this.cmbOperKind.SelectedIndex = 0;//普通

            //dirty = true;
            this.txtDiag.Text = "";//诊断
            this.txtDiag.Tag = null;
            this.txtDiag2.Text = "";//诊断
            this.txtDiag2.Tag = null;
            this.txtDiag3.Text = "";//诊断
            this.txtDiag3.Tag = null;

            this.txtOperation.Text = "";//手术名称
            this.txtOperation.Tag = null;
            this.txtOperation2.Text = "";//手术名称
            this.txtOperation2.Tag = null;
            this.txtOperation3.Text = "";//手术名称
            this.txtOperation3.Tag = null;
            //dirty = false;

            this.cmbAnae.Text = "";//麻醉类型
            this.cmbAnae.Tag = null;
            //add by chengym 2012-5-14
            this.cmbAnae2.Text = "";
            this.cmbAnae2.Tag = null;
            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            this.cmbAnseWay.Text = "";
            this.cmbAnseWay.Tag = null;
            DateTime dtNow = Environment.OperationManager.GetDateTimeFromSysDateTime();
            this.lbApplyDate.Text = dtNow.ToString("yyyy年MM月dd日 HH时mm分");//申请日期

            dtNow = DateTime.Parse(string.Concat(dtNow.Date.AddDays(1).ToString("yyyy-MM-dd"), " 09:00:00"));
            this.dtOperDate.Value = dtNow;//预约时间

            this.cmbExeDept.Text = "";//手术室
            this.cmbExeDept.Tag = null;

            this.rtbApplyNote.Text = "";//备注
            this.cbxNeedQX.Checked = false;
            this.cbxNeedXH.Checked = false;

            this.cmbDoctor.Text = "";//手术者
            this.cmbDoctor.Tag = null;
            this.cmbHelper1.Text = "";//一助
            this.cmbHelper1.Tag = null;
            this.cmbHelper2.Text = "";//二助
            this.cmbHelper2.Tag = null;
            this.cmbHelper3.Text = "";//三助
            this.cmbHelper3.Tag = null;

            //this.cmbSpecial.SelectedIndex = 0;//是否特殊手术
            this.cmbSpecial.Text = "";
            this.cmbOrder.Text = "";//台序
            this.cmbOwn.SelectedIndex = 0;
            // {F0B32D1F-99B6-4b1a-8393-C1F89B98543B}
            this.cmbIsDevide.Text = "否";
            this.cmbPortPosition.Tag = null;
            this.txtPortPosition.Text = "";
            this.cmbBodyPosion.Text = null;
            this.cmbUnit.Text = "";
            this.txtLastTime.Text = "";
            this.chkHBV.Checked = false;
            this.chkHIV.Checked = false;
            this.chkHCV.Checked = false;
            this.operationApplication = new FS.HISFC.Models.Operation.OperationAppllication();
            this.isNew = true;

            this.cmbApplyDoct.Text = "";//申请医生
            this.cmbApplyDoct.Tag = null;

            foreach (Employee person in this.cmbApplyDoct.alItems)
            {
                if (person.ID == FS.FrameWork.Management.Connection.Operator.ID)
                {
                    this.cmbApplyDoct.Tag = FS.FrameWork.Management.Connection.Operator.ID;
                    break;
                }
            }
            this.cmbIncitepe.Tag = null;

            this.cmbApproveDoctor.Tag = null;
            this.dtApproveDate.Value = System.DateTime.Now;
            this.cmbApproveNote.Tag = null;
        }

        /// <summary>
        /// 实体赋值
        /// </summary>
        /// <returns></returns>
        private int GetValue()
        {
            //新录入的，生成新的申请单
            if (this.isNew)
                this.operationApplication.ID = Environment.OperationManager.GetNewOperationNo();

            // TODO: 诊断为实现
            #region 诊断
            FS.HISFC.Models.HealthRecord.DiagnoseBase diag = new FS.HISFC.Models.HealthRecord.DiagnoseBase();

            diag.OperationNo = this.operationApplication.ID;//申请号
            //diag.ICD10=(FS.HISFC.Object.Case.ICD10)this.txtDiag.Tag;
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}
            if (((FS.FrameWork.Models.NeuObject)this.txtDiag.Tag).ID == "D991")
            {
                diag.ID = "D991";
                diag.Name = this.txtDiag.Text;
            }
            else
            {
                diag.ID = (this.txtDiag.Tag as FS.HISFC.Models.HealthRecord.ICD).ID;
                diag.Name = (this.txtDiag.Tag as FS.HISFC.Models.HealthRecord.ICD).Name;
            }
            diag.Patient = this.operationApplication.PatientInfo.Clone();//.PatientInfo.Patient.Clone();
            diag.DiagType.ID = "7";//诊断类型
            diag.DiagType.Name = FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OTHER.ToString();//术前诊断
            diag.DiagDate = opsMgr.GetDateTimeFromSysDateTime();//诊断时间
            diag.Doctor.ID = var.ID;//诊断医生
            diag.Doctor.Name = var.Name;//诊断医生
            diag.Dept.ID = var.Dept.ID;//诊断科室
            diag.IsValid = true;//是否有效
            diag.IsMain = true;//主诊断

            if (operationApplication.DiagnoseAl.Count == 0)
                diag.HappenNo = opsDiagnose.GetNewDignoseNo();//序号
            else
                diag.HappenNo = (operationApplication.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).HappenNo;

            operationApplication.DiagnoseAl.Clear();
            operationApplication.DiagnoseAl.Add(diag);
            #region 第二诊断
            if (txtDiag2.Tag != null)
            {
                diag = new FS.HISFC.Models.HealthRecord.DiagnoseBase();
                diag.OperationNo = this.operationApplication.ID;//申请号
                //diag.ICD10=(FS.HISFC.Object.Case.ICD10)this.txtDiag.Tag;
                //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}
                if (((FS.FrameWork.Models.NeuObject)this.txtDiag2.Tag).ID == "D992")
                {
                    diag.ID = "D992";
                    diag.Name = this.txtDiag2.Text;
                }
                else
                {
                    diag.ID = (this.txtDiag2.Tag as FS.HISFC.Models.HealthRecord.ICD).ID;
                    diag.Name = (this.txtDiag2.Tag as FS.HISFC.Models.HealthRecord.ICD).Name;
                }
                diag.Patient = this.operationApplication.PatientInfo.Clone();
                diag.DiagType.ID = "7";//诊断类型
                diag.DiagType.Name = FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OTHER.ToString();//术前诊断
                diag.DiagDate = opsMgr.GetDateTimeFromSysDateTime();//诊断时间
                diag.Doctor.ID = var.ID;//诊断医生
                diag.Doctor.Name = var.Name;//诊断医生
                diag.Dept.ID = var.Dept.ID;//诊断科室
                diag.IsValid = true;//是否有效
                diag.IsMain = false;//主诊断

                if (operationApplication.DiagnoseAl.Count == 1)
                    diag.HappenNo = opsDiagnose.GetNewDignoseNo();//序号
                else
                    diag.HappenNo = (operationApplication.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).HappenNo;
                operationApplication.DiagnoseAl.Add(diag);
            }
            #endregion
            #region 第三诊断
            if (txtDiag3.Tag != null)
            {
                diag = new FS.HISFC.Models.HealthRecord.DiagnoseBase();
                diag.OperationNo = this.operationApplication.ID;//申请号
                //diag.ICD10=(FS.HISFC.Object.Case.ICD10)this.txtDiag.Tag;
                //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}
                if (((FS.FrameWork.Models.NeuObject)this.txtDiag3.Tag).ID == "D993")
                {
                    diag.ID = "D993";
                    diag.Name = this.txtDiag3.Text;
                }
                else
                {
                    diag.ID = (this.txtDiag3.Tag as FS.HISFC.Models.HealthRecord.ICD).ID;
                    diag.Name = (this.txtDiag3.Tag as FS.HISFC.Models.HealthRecord.ICD).Name;
                }
                diag.Patient = this.operationApplication.PatientInfo.Clone();
                diag.DiagType.ID = "7";//诊断类型
                diag.DiagType.Name = FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OTHER.ToString();//术前诊断
                diag.DiagDate = opsMgr.GetDateTimeFromSysDateTime();//诊断时间
                diag.Doctor.ID = var.ID;//诊断医生
                diag.Doctor.Name = var.Name;//诊断医生
                diag.Dept.ID = var.Dept.ID;//诊断科室
                diag.IsValid = true;//是否有效
                diag.IsMain = false;//主诊断

                if (operationApplication.DiagnoseAl.Count == 2)
                    diag.HappenNo = opsDiagnose.GetNewDignoseNo();//序号
                else
                    diag.HappenNo = (operationApplication.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).HappenNo;
                operationApplication.DiagnoseAl.Add(diag);
            }
            #endregion
            #endregion
            #region 手术项目

            this.operationApplication.OperationInfos.Clear();
            //-----------------------------------------------------------------------------
            if (this.txtOperation.Text.Trim() != "" && this.txtOperation.Tag != null)
            {
                if (this.txtOperation.Tag.GetType() == typeof(FS.HISFC.Models.Operation.OperationInfo))
                {
                    FS.HISFC.Models.Operation.OperationInfo obj = (FS.HISFC.Models.Operation.OperationInfo)txtOperation.Tag;
                    operationApplication.OperationInfos.Add(obj);
                }
                else
                {
                    FS.HISFC.Models.Operation.OperationInfo opItem = new FS.HISFC.Models.Operation.OperationInfo();
                    opItem.OperationItem = (FS.HISFC.Models.Base.Item)this.txtOperation.Tag;//手术项目
                    opItem.FeeRate = 1m;//比率
                    opItem.Qty = 1;//数量
                    opItem.StockUnit = (this.txtOperation.Tag as FS.HISFC.Models.Base.Item).PriceUnit;//单位
                    //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}
                    if (opItem.OperationItem.ID.IndexOf("S99")==-1)
                    {
                        opItem.OperateType.ID = (this.txtOperation.Tag as FS.HISFC.Models.Fee.Item.Undrug).OperationScale.ID;
                    }
                    opItem.IsValid = true;
                    opItem.IsMainFlag = true;
                    operationApplication.OperationInfos.Add(opItem);
                    operationApplication.OperationType.ID = opItem.OperateType.ID;
                }
            } 
            //-----------------------------------------------------------------------------
            //this.SetOperationItem(this.txtOperation.Tag);

            if (this.txtOperation2.Text.Trim() != "" && this.txtOperation2.Tag != null)
            {
                this.SetOperationItem(this.txtOperation2.Tag);
            }
            if (this.txtOperation3.Text.Trim() != "" && this.txtOperation3.Tag != null)
            {
                this.SetOperationItem(this.txtOperation3.Tag);
            }
            
            #endregion
            //麻醉方式
            this.operationApplication.AnesType.ID = this.cmbAnae.Tag.ToString();
            this.operationApplication.AnesType.Name = this.cmbAnae.Text;
            //add by chengym 2012-5-14
            if (this.cmbAnae2.Tag != null && this.cmbAnae2.Text != "")
            {
                this.operationApplication.AnesType.ID += "|"+this.cmbAnae2.Tag.ToString();
                this.operationApplication.AnesType.Name += "|" + this.cmbAnae2.Text;
            }

            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            if (!this.cmbAnseWay.Visible)
            {
                this.cmbAnseWay.Tag = "1";
            }
            this.operationApplication.AnesWay = this.cmbAnseWay.Tag.ToString();

            //特殊说明
            this.operationApplication.ApplyNote = this.rtbApplyNote.Text.Trim();
            #region 术者
            FS.HISFC.Models.Operation.ArrangeRole role;
            role = new FS.HISFC.Models.Operation.ArrangeRole();
            role.OperationNo = this.operationApplication.ID;                   //申请号
            role.ID = this.cmbDoctor.Tag.ToString();                             //人员代码
            role.Name = this.cmbDoctor.Text;
            role.RoleType.ID = FS.HISFC.Models.Operation.EnumOperationRole.Operator;    //角色编码
            role.ForeFlag = "0";                                                        //术前录入
            this.operationApplication.RoleAl.Clear();
            this.operationApplication.RoleAl.Add(role);
            this.operationApplication.OperationDoctor.ID = role.ID;
            this.operationApplication.OperationDoctor.Name = role.Name;
            #endregion
            #region 一助
            role = new FS.HISFC.Models.Operation.ArrangeRole();
            role.OperationNo = this.operationApplication.ID;//申请号
            role.ID = this.cmbHelper1.Tag.ToString();//人员代码
            role.Name = this.cmbHelper1.Text;
            role.RoleType.ID = FS.HISFC.Models.Operation.EnumOperationRole.Helper1;//角色编码
            role.ForeFlag = "0";//术前录入
            this.operationApplication.RoleAl.Add(role);

            FS.FrameWork.Models.NeuObject person;
            person = new FS.FrameWork.Models.NeuObject();

            person.ID = role.ID;
            person.Name = role.Name;
            this.operationApplication.HelperAl.Clear();
            this.operationApplication.HelperAl.Add(person);
            #endregion
            #region 二助
            if (this.cmbHelper2.Tag != null && this.cmbHelper2.Tag.ToString() != "")
            {
                role = new FS.HISFC.Models.Operation.ArrangeRole();
                role.OperationNo = this.operationApplication.ID;//申请号
                role.ID = this.cmbHelper2.Tag.ToString();//人员代码
                role.Name = this.cmbHelper2.Text;
                role.RoleType.ID = FS.HISFC.Models.Operation.EnumOperationRole.Helper2;//角色编码
                role.ForeFlag = "0";//术前录入
                this.operationApplication.RoleAl.Add(role);

                person = new FS.FrameWork.Models.NeuObject();

                person.ID = role.ID;
                person.Name = role.Name;
                this.operationApplication.HelperAl.Clear();
                this.operationApplication.HelperAl.Add(person);
            }
            #endregion
            #region 三助
            if (this.cmbHelper3.Tag != null && this.cmbHelper3.Tag.ToString() != "")
            {
                role = new FS.HISFC.Models.Operation.ArrangeRole();
                role.OperationNo = this.operationApplication.ID;//申请号
                role.ID = this.cmbHelper3.Tag.ToString();//人员代码
                role.Name = this.cmbHelper3.Text;
                role.RoleType.ID = FS.HISFC.Models.Operation.EnumOperationRole.Helper3;//角色编码
                role.ForeFlag = "0";//术前录入
                this.operationApplication.RoleAl.Add(role);

                person = new FS.FrameWork.Models.NeuObject();

                person.ID = role.ID;
                person.Name = role.Name;
                this.operationApplication.HelperAl.Clear();
                this.operationApplication.HelperAl.Add(person);
            }
            #endregion
            //预约日期
            this.operationApplication.PreDate = this.dtOperDate.Value;
            //手术室
            this.operationApplication.OperateRoom.ID = this.cmbExeDept.Tag.ToString();
            this.operationApplication.OperateRoom.Name = this.cmbExeDept.Text;
            this.operationApplication.ExeDept = this.operationApplication.OperateRoom.Clone();
            //手术台类型
            int index = this.cmbTableType.SelectedIndex + 1;
            this.operationApplication.TableType = index.ToString();
            //是否特殊手术
            this.operationApplication.SpecialItem = this.cmbSpecial.Text;
            this.operationApplication.BloodNum = this.cmbSpecial.SelectedIndex;
            if (this.cmbSpecial.SelectedIndex == 0)//否
                this.operationApplication.IsSpecial = false;
            else
                this.operationApplication.IsSpecial = true;
            //台序
            this.operationApplication.BloodUnit = this.cmbOrder.Text;

            //是否需要巡回
            this.operationApplication.IsPrepNurse = this.cbxNeedXH.Checked;
            //是否需要器械
            this.operationApplication.IsAccoNurse = this.cbxNeedQX.Checked;

            if (this.cmbOwn.SelectedIndex == 0)//是否同意使用自费项目
                this.operationApplication.IsHeavy = true;
            else
                this.operationApplication.IsHeavy = false;

            index = this.cmbOperKind.SelectedIndex + 1;
            this.operationApplication.OperateKind = index.ToString();

            //操作人
            this.operationApplication.User.ID = Environment.OperatorID;
            //申请医生
            this.operationApplication.ApplyDoctor.ID = this.cmbApplyDoct.Tag.ToString();
            this.operationApplication.ApplyDoctor.Name = this.cmbApplyDoct.Text;
            //申请科室
            this.operationApplication.ApplyDoctor.Dept.ID = Environment.OperatorDeptID;
            //患者来源
            this.operationApplication.PatientSouce = "2";//住院患者
            this.operationApplication.OperationDoctor.Dept.ID = this.comDept.Tag.ToString();
            this.operationApplication.AnesWay = this.cmbAnseWay.Tag.ToString();

            //{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
            this.operationApplication.InciType.ID = this.cmbIncitepe.Tag.ToString();
            //{F0B32D1F-99B6-4b1a-8393-C1F89B98543B}feng.ch
            //部位和体位
            //this.operationApplication.Position = this.cmbPortPosition.Text + "|" + this.cmbBodyPosion.Text;
            this.operationApplication.Position = this.txtPortPosition.Text + "|" + this.cmbBodyPosion.Text;
            //主要病种
            string tag = string.Empty;
            foreach (FS.FrameWork.WinForms.Controls.NeuCheckBox chk in neuPanel1.Controls)
            {
                if (chk.Checked)
                {
                    tag += chk.Text+"|";
                }
            }
            this.operationApplication.Eneity = tag;
            //拟手术持续时间
            this.operationApplication.LastTime = this.txtLastTime.Text+"|"+this.cmbUnit.Text;
            //是否隔离
            if (this.cmbIsDevide.Text.ToString() == "是")
            {
                this.operationApplication.IsOlation = "1";
            }
            else
            {
                this.operationApplication.IsOlation = "0";
            }

            this.operationApplication.OperationType.ID = this.cmbOpType.Tag.ToString();
            #region 手术审核信息 2012-3-9 chengym
            this.operationApplication.ApproveDoctor.ID = this.cmbApproveDoctor.Tag.ToString();
            this.operationApplication.ApproveDate= this.dtApproveDate.Value;
            this.operationApplication.ApproveNote = this.cmbApproveNote.Tag.ToString();
            #endregion
            return 0;
        }

        /// <summary>
        /// 设置手术项目
        /// </summary>
        /// <param name="operationItem"></param>
        private void SetOperationItem(object operationItem)
        {
            if (operationItem.GetType() == typeof(FS.HISFC.Models.Operation.OperationInfo))
            {
                FS.HISFC.Models.Operation.OperationInfo obj = (FS.HISFC.Models.Operation.OperationInfo)operationItem;
                this.operationApplication.OperationInfos.Add(obj);
            }
            else
            {
                FS.HISFC.Models.Operation.OperationInfo operationInfo = new FS.HISFC.Models.Operation.OperationInfo();
                operationInfo.OperationItem = operationItem as FS.HISFC.Models.Base.Item;//手术项目
                operationInfo.FeeRate = 1m;//比率
                operationInfo.Qty = 1;//数量
                operationInfo.StockUnit = (operationItem as FS.HISFC.Models.Base.Item).PriceUnit;//单位
                //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}
                if (operationInfo.OperationItem.ID.IndexOf("S99")==-1)
                {
                    operationInfo.OperateType.ID = (operationItem as FS.HISFC.Models.Fee.Item.Undrug).OperationScale.ID;
                }
                operationInfo.IsValid = true;
                operationInfo.IsMainFlag = false;

                this.operationApplication.OperationInfos.Add(operationInfo);
                this.operationApplication.OperationType.ID = operationInfo.OperateType.ID;
            }
        }

        /// <summary>
        /// 有效性验证
        /// </summary>
        /// <returns></returns>
        private int Valid()
        {
            if (this.isNew == false)
            {
                if (this.operationApplication.ExecStatus == "3" || this.operationApplication.ExecStatus == "4")
                {
                    MessageBox.Show("该申请单已安排或登记,不能修改!", "提示");
                    return -1;
                }
                if (this.operationApplication.ExecStatus == "5")
                {
                    MessageBox.Show("该申请单已取消登记,不能修改！", "提示");
                    return -1;
                }
                if (this.operationApplication.IsValid == false)
                {
                    MessageBox.Show("该申请单已经作废!", "提示");
                    return -1;
                }
            } 
            if (operationApplication.PatientInfo.ID == "")
            {
                MessageBox.Show("请选择申请患者!", "提示");
                return -1;
            }
            if (this.txtDiag.Text.Length == 0)
            {
                MessageBox.Show("术前诊断一不能为空!", "提示");
                txtDiag.Focus();
                return -1;
            }
            string Diag1 = txtDiag.Text;
            string Diag2 = txtDiag2.Text;
            string Diag3 = txtDiag3.Text;
            //.
            if (Diag1 == "")
            { 
                MessageBox.Show("术前诊断一不能为空！");
                txtDiag.Focus();
                return -1;
            }
            //
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtOperation.Text, 100) == false)
            {
                txtOperation.Focus();
                MessageBox.Show("拟手术名称过长！");
                return -1;
            }
            //
            if ((Diag1 == Diag2 && Diag2 != "") || (Diag1 == Diag3 && Diag1 != "") || (Diag3 == Diag2 && Diag2 != ""))
            {
                MessageBox.Show("术前诊断不能重复");
                txtDiag.Focus();
                return -1;
            }
            // TODO: 需要加入病案后修改
            if (this.txtOperation.Text.Length == 0)
            {
                MessageBox.Show("拟手术名称不能为空!", "提示");
                txtOperation.Focus();
                return -1;
            }
            string Oper1 = txtOperation.Text;
            string Oper2 = txtOperation2.Text;
            string Oper3 = txtOperation3.Text;
            if ((Oper1 == Oper2 && Oper2 != "") || (Oper1 == Oper3 && Oper1 != "") || (Oper3 == Oper2 && Oper2 != ""))
            {
                MessageBox.Show("拟手术名称不能重复");
                txtOperation.Focus();
                return -1;
            }
            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            if (this.cmbAnae.Tag == null || this.cmbAnae.Tag.ToString() == "")
            {
                MessageBox.Show("麻醉方式不能为空!", "提示");
                cmbAnae.Focus();
                return -1;
            }
            //if (!this.cmbAnseWay.Visible)
            //{
            //    if (this.cmbAnseWay.Tag == null || this.cmbAnseWay.Tag.ToString() == "")
            //    {
            //        MessageBox.Show("麻醉类别不能为空!", "提示");
            //        cmbAnseWay.Focus();
            //        return -1;
            //    }
            //}
            if (this.cmbExeDept.Tag == null || this.cmbExeDept.Tag.ToString() == "")
            {
                MessageBox.Show("手术室不能为空!", "提示");
                cmbExeDept.Focus();
                return -1;
            }
            if (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == "")
            {
                MessageBox.Show("术者不能为空!", "提示");
                cmbDoctor.Focus();
                return -1;
            }
            if (comDept.Text.Trim() == "" || comDept.Tag == null || comDept.Tag.ToString()=="")
            {
                MessageBox.Show("术者科室不能为空!", "提示");
                comDept.Focus();
                return -1;
            }
            if (string.IsNullOrEmpty(this.txtLastTime.Text))
            {
                MessageBox.Show("拟手术时间不能为空!", "提示");
                this.txtLastTime.Focus();
                return -1;
            }
            if (this.cmbHelper1.Tag == null || this.cmbHelper1.Tag.ToString() == "")
            {
                MessageBox.Show("一助不能为空!", "提示");
                cmbHelper1.Focus();
                return -1;
            }
            string helper1 = "";
            string helper2 = "";
            string helper3 = "";
            this.cmbHelper1.Tag.ToString();
            if (this.cmbDoctor.Tag.ToString() == this.cmbHelper1.Tag.ToString())
            {
                MessageBox.Show("术者与一助不能重复!", "提示");
                cmbDoctor.Focus();
                return -1;
            }

            if (cmbHelper2.Tag != null)
            {
                helper2 = this.cmbHelper2.Tag.ToString();
            }
            if (this.cmbHelper3.Tag != null)
            {
                helper3 = this.cmbHelper3.Tag.ToString();
            }
            if ((helper1 == helper2 && helper1 != "") || (helper1 == helper3 && helper3 != "") || (helper2 == helper3 && helper3 != ""))
            {
                MessageBox.Show("一助二助三助不能重复");
                cmbHelper1.Focus();
                return -1;
            }
            if (this.cmbOrder.Text == "")
            {
                MessageBox.Show("请指定台序!", "提示");
                cmbOrder.Focus();
                return -1;
            }

            #region 判断每个科手术申请时只能选择唯一一个台序
            string Ordered = this.GetOrdered(((FS.HISFC.Models.Base.Employee)(Environment.OperationManager.Operator)).Dept.ID, this.dtOperDate.Value);
            if (this.OperationApplication.BloodUnit!=this.cmbOrder.Text && Ordered.Contains(this.cmbOrder.Text))
            {
                MessageBox.Show("你选择的" + this.cmbOrder.Text + "已经被其他医生选择,请重新选择!", "提示");
                cmbOrder.Focus();
                return -1;
            }
            #endregion

            #region 屏蔽
            //根据指定时间和手术室判断当天是否有正台,如无自动变为加台
            //Department d = new Department();

            //d.ID = this.cmbExeDept.Tag.ToString();
            //int num = Environment.OperationManager.GetEnableTableNum(d, operationApplication.PatientInfo.PVisit.PatientLocation.Dept.ID, this.dtOperDate.Value);
            //int mm = Environment.OperationManager.SameDeptApplication(this.dtOperDate.Value.Date.ToString(), this.dtOperDate.Value.Date.AddDays(1).ToString(), d.ID, operationApplication.PatientInfo.PVisit.PatientLocation.Dept.ID, cmbOrder.Text.Substring(0, 1));
            //if (mm == -1)
            //{
            //    MessageBox.Show("判断是否是应该是正台出错" + Environment.OperationManager.Err);
            //    return -1;
            //}
            //if (num <= 0 && this.cmbTableType.SelectedIndex == 0 && isNew)
            //{
            //    MessageBox.Show("申请日期内已无正台,请修改手术台类型!", "提示");
            //    cmbTableType.Focus();
            //    return -1;
            //}
            //if (num <= 0 && this.cmbTableType.SelectedIndex == 0 && mm == 1 && isNew)//无正台,不能申请正台
            //{
            //    MessageBox.Show("申请日期内已无正台,请修改手术台类型!", "提示");
            //    cmbTableType.Focus();
            //    return -1;
            //}
            #endregion
            //#region 判断择期手术是否正台 没有只能填加台或者急诊 2012-3-12 add  by chengym
            ////1 有正台  -1 只能加台或者急诊
            //string Error = string.Empty;
            //int lostNum = 0;
            //if (this.cmbTableType.SelectedIndex == 0 && this.isNew)//正台才判断
            //{
            //    lostNum = FS.HISFC.Components.Operation.Funtion.CheckLimitedLostNumber(this.dtOperDate.Value.Date, this.dtOperDate.Value.Date.AddDays(1), this.cmbExeDept.Tag.ToString(), operationApplication.PatientInfo.PVisit.PatientLocation.Dept.ID, "", ref Error);
            //    if (lostNum == -2)
            //    {
            //        if (Error != string.Empty)
            //        {
            //            MessageBox.Show(Error, "提示");
            //            return -1;
            //        }
            //    }
            //    if (lostNum == -1)
            //    {
            //        if (Error != string.Empty)
            //        {
            //            MessageBox.Show(Error, "提示");
            //            return -1;
            //        }
            //        else
            //        {
            //            MessageBox.Show("申请日期内已无正台,请修改手术台类型!", "提示");
            //            cmbTableType.Focus();
            //            return -1;
            //        }
            //    }
            //}
            //#endregion

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.rtbApplyNote.Text.Trim(), 200) == false)
            {
                MessageBox.Show("特殊说明必须小于100个汉字!", "提示");
                rtbApplyNote.Focus();
                return -1;
            }
            if (this.cmbApplyDoct.Tag == null || this.cmbApplyDoct.Tag.ToString() == "")
            {
                MessageBox.Show("申请医生不能为空!", "提示");
                cmbApplyDoct.Focus();

                return -1;
            }
            if (!checkDate)
            {
                if (((System.DateTime.Now.DayOfWeek == System.DayOfWeek.Saturday || System.DateTime.Now.DayOfWeek == System.DayOfWeek.Sunday) && cmbOperKind.Text == "择期") && dtOperDate.Value.DayOfWeek == System.DayOfWeek.Monday)
                {
                    MessageBox.Show("周六,周日不能申请周一的手术");
                    return -1;
                }
            }
            //判断申请时间是否合法
            string rtn = Environment.OperationManager.PreDateValidity(this.dtOperDate.Value);
            if (rtn == "Error")
            {
                MessageBox.Show(Environment.OperationManager.Err, "参数设置");
                return -1;
            }
            else if (rtn == "Before")
            {
                #region
                if (!CheckApplyTime)
                {
                    MessageBox.Show("申请时间不能小于当前时间!", "提示");
                    //this.dtOperDate.Select();
                    this.dtOperDate.Focus();
                    //this.dtOperDate.ShowUpDown = true;
                    //this.dtOperDate.ShowUpDown = false;
                    return -1;
                }
                #endregion
            }
            else if (rtn == "Over")
            {
                if (this.cmbOperKind.SelectedIndex != 1)
                {
                    if (checkEmergency)
                    {
                        #region 如果科室属性是手术室 ，则不提示
                        FS.HISFC.BizProcess.Integrate.Manager dp = new FS.HISFC.BizProcess.Integrate.Manager();

                        FS.HISFC.Models.Base.Department dd = dp.GetDepartment(Environment.OperatorDeptID);
                        if (dd.SpecialFlag != "1")
                        {
                            MessageBox.Show("已超过该日手术申请的截止时间,\n请预约至其他日期进行手术申请,或者将手术类别改为急诊!", "提示");
                            cmbOperKind.Focus();
                            return -1;
                        }
                        #endregion
                    }
                }
            }
            if (this.cmbSpecial.Text == string.Empty)
            {
                MessageBox.Show("请选择是否“特殊手术”");
                this.cmbSpecial.Focus();
                return -1;
            }

            #region 校验诊断
            //{6C784A56-3FFD-47c3-A2A1-6382F7A7C7E1}

            if (this.txtDiag.Text.Trim() != string.Empty &&  this.txtDiag.Tag == null  )
            {
                MessageBox.Show("所录入的“术前诊断一”不存在,请重新输入");
                this.txtDiag.Focus();
                return -1;
            }

            if (this.txtDiag2.Text.Trim() != string.Empty &&  this.txtDiag2.Tag == null  )
            {
                MessageBox.Show("所录入的“术前诊断二”不存在,请重新输入");
                this.txtDiag2.Focus();
                return -1;
            }

            if (this.txtDiag3.Text.Trim() != string.Empty &&  this.txtDiag3.Tag == null )
            {
                MessageBox.Show("所录入的“术前诊断三”不存在,请重新输入");
                this.txtDiag3.Focus();
                return -1;
            }


           

            if (this.txtOperation.Text.Trim() != string.Empty &&  this.txtOperation.Tag == null )
            {
                MessageBox.Show("所录入的第一“拟手术名称”不存在,请重新输入");
                this.txtOperation.Focus();
                return -1;
            }


            if (this.txtOperation2.Text.Trim() != string.Empty &&  this.txtOperation2.Tag == null  )
            {
                MessageBox.Show("所录入的第二“拟手术名称”不存在,请重新输入");
                this.txtOperation2.Focus();
                return -1;
            }

            if (this.txtOperation3.Text.Trim() != string.Empty &&  this.txtOperation3.Tag == null  )
            {
                MessageBox.Show("所录入的第三“拟手术名称”不存在,请重新输入");
                this.txtOperation3.Focus();
                return -1;
            }
            #endregion
            //有审核权的需要填写审核医生
            if (this.cmbOperKind.Tag == null)
            {
                MessageBox.Show("请选择手术类别！");
                this.cmbOperKind.Focus();
                return -1;
            }

            if (this.isHavingApprove && this.cmbOperKind.Tag.ToString()=="1" && this.cmbApproveDoctor.Tag.ToString() == string.Empty)
            {
                MessageBox.Show("请选择“审核医生”，填写审核信息！");
                this.cmbApproveDoctor.Focus();
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (Valid() == -1)
            {
                return -1;
            }

            if (this.GetValue() == -1)
            {
                return -1;
            }
           
            #region 判断是否存在重复手术申请
            if (this.isNew)
            {
                //默认取第一个诊断为统计术前诊断
                string strDiagnose = "";
                string strDiagName = "";
                if (this.applyoldMZ != null)
                {//{0E140FEC-2F31-4414-8401-86E78FA3ADDC}设置来源门诊手术申请主键保存
                    this.operationApplication.Appsourceid=applyoldMZ.ID;
                }
                if (operationApplication.DiagnoseAl.Count > 0)
                {
                    foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase MainDiagnose in operationApplication.DiagnoseAl)
                    {
                        if (MainDiagnose.IsValid)
                        {
                            strDiagnose = MainDiagnose.Name + "(" + MainDiagnose.ID.ToString() + ")";
                            strDiagName = MainDiagnose.Name;
                        }
                    }
                }
                int i = Environment.OperationManager.IsExistSameApplication(operationApplication.PatientInfo.ID, strDiagnose, operationApplication.PreDate.ToString());
                if (i == -1) //查询出错
                {
                    MessageBox.Show("查询病人手术信息" + Environment.OperationManager.Err);
                    return -1;
                }
                if (i == 2) //有重复申请的信息 
                {
                    System.Windows.Forms.DialogResult result = MessageBox.Show("病人(" + operationApplication.PatientInfo.Name + ")已经存在(" + strDiagName + ")的手术申请,是否要重新申请一个?", "提示", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No)
                    {
                        return -1;
                    }
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            opsDiagnose.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (this.isNew)//新增
                {
                    //add by chengym 2012-3-12 有审核权的医生申请可能直接审核了
                    if (this.isNeedApprove && operationApplication.ApproveDoctor.ID!="")
                    {
                        if (operationApplication.ExecStatus == "1")
                        {
                            operationApplication.ExecStatus = "2";//1 申请状态 2 审核状态 3 已安排 4 已登记
                        }
                    }
                   
                    if (Environment.OperationManager.CreateApplication(operationApplication) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Environment.OperationManager.Err, "提示");
                        return -1;
                    }
                    //{0E140FEC-2F31-4414-8401-86E78FA3ADDC}置门诊手术状态
                    //if (SetOperationMzFinish() < 1)//设置门诊手术申请状态为完成，如果失败那么回退所有
                    //{//{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    return -1;
                   
                    //}{2F7180FD-7B71-4001-A4BB-DBA9B7D40BE8}
                    if (CancelApplyOldMZ() < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
                else//修改
                {
                    //先判断状态
                    OperationAppllication obj = Environment.OperationManager.GetOpsApp(this.operationApplication.ID);
                    if (obj == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("无该申请单信息!", "提示");
                        return -1;
                    }
                    //1申请2审批3安排4完成
                    if (obj.ExecStatus == "3" || obj.ExecStatus == "4")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("该申请单已被安排或登记,不能进行修改!", "提示");
                        return -1;
                    }
                    if (obj.ExecStatus == "5")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("该申请单已被取消登记,不能进行修改!", "提示");
                        return -1;
                    }
                    if (obj.ExecStatus == "6")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("该申请单是已手术未收费状态,不能进行修改!", "提示");
                        return -1;
                    }

                    if (obj.IsValid == false)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("该申请单已经作废!", "提示");
                        return -1;
                    }
                    //add by chengym 2012-3-12 更新审核
                    if (this.isNeedApprove && this.isHavingApprove && this.cmbOperKind.Tag.ToString()=="1")
                    {
                        if (operationApplication.ExecStatus == "1")
                        {
                            operationApplication.ExecStatus = "2";//1 申请状态 2 审核状态 3 已安排 4 已登记
                        }
                    }
                    //if (SetOperationMzFinish() < 1)//设置门诊手术申请状态为完成，如果失败那么回退所有 {0E140FEC-2F31-4414-8401-86E78FA3ADDC}
                    //{//{0E140FEC-2F31-4414-8401-86E78FA3ADDC}
                    //    FS.FrameWork.Management.PublicTrans.RollBack();
                    //    return -1;
                    //}
                    if (Environment.OperationManager.UpdateApplication(operationApplication) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Environment.OperationManager.Err, "提示");
                        return -1;
                    }
                    
                }
                #region 诊断信息
                //ArrayList oldDiag = opsDiagnose.QueryOpsDiagnose(operationApplication.PatientInfo.ID, "7");
                //if (oldDiag == null)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("查询手术诊断失败");
                //    return -1;
                //}

                // ArrayList IcdAl = opsDiagnose.QueryOpsDiagnose(operationApplication.PatientInfo.ID, "7");
                //if (IcdAl == null)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("查询手术诊断失败");
                //    return -1;
                //}

                //ArrayList oldDiag = new ArrayList();
                //foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase diag in IcdAl)
                //{
                //    if (diag.OperationNo == operationApplication.ID)
                //        oldDiag.Add(diag);
                //}

                int returnValue = opsDiagnose.DeleteDiagnoseByOperationNO(operationApplication.ID);

                ArrayList oldDiag = new ArrayList();

                bool bIsExist = false;
                //遍历要加入的诊断信息列表(OpsApp.DiagnoseAl)
                foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase willAddDiagnose in operationApplication.DiagnoseAl)
                {
                    bIsExist = false;
                    //遍历患者已有的所有手术诊断，如果willAddDiagnose已经存在，更新其状态，
                    //如果willAddDiagnose尚不存在，则新增该记录到数据库中
                    foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase thisDiagnose in oldDiag)
                    {
                        if (thisDiagnose.HappenNo == willAddDiagnose.HappenNo && thisDiagnose.Patient.ID.ToString() == willAddDiagnose.Patient.ID.ToString())
                        {
                            //已经存在	更新				
                            if (opsDiagnose.UpdatePatientDiagnose(willAddDiagnose) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                return -1; 
                            }
                            bIsExist = true;
                        }
                    }
                    //遍历完毕后发现不存在 新增
                    if (bIsExist == false)
                    {
                        if (opsDiagnose.CreatePatientDiagnose(willAddDiagnose) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }
                }
                #endregion 
                if (this.cmbExeDept.Tag != null)
                {
                    string[] str = new string[2];
                    str[0] = this.cmbExeDept.Text;
                    str[1] = this.cmbExeDept.Tag.ToString();
                    FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("ExeDept", str);
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.ucDiag1.Visible = false;
                this.ucOpItem1.Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "提示");
                return -1;
            }
            //急诊手术,弹出提示
            if (this.operationApplication.OperateKind == "2")
                MessageBox.Show("该手术为急诊手术,请电话通知手术室!", "提示");
            // TODO: 发消息
            //FS.HISFC.Common.Class.Message.SendMessage(this.lblDept.Text + "患者：" + this.lblName.Text + "有新手术申请,请核收!", this.operationApplication.ExeDept.ID);
            if (this.isNew)
            {
                //普通择期手术
                if (this.isNeedApprove && this.operationApplication.OperateKind == "1" && this.operationApplication.ApproveDoctor.ID == "")
                {
                    MessageBox.Show("保存成功!请及时通知科主任审核手术申请！", "提示");
                }
                else
                {
                    MessageBox.Show("申请成功!", "提示");
                }
            }
            else
            {
                if (this.isNeedApprove && this.operationApplication.OperateKind == "1" && this.operationApplication.ApproveDoctor.ID != "")
                {
                    MessageBox.Show("申请审核成功!");
                }
                else
                {
                    MessageBox.Show("申请修改成功，请通知手术室!");
                }
            }
            
            if (isSavePrint)
            {
                if (MessageBox.Show("是否打印手术申请单", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Print();
                }
            }

            //{C51C7189-3DA7-4ebc-9D1F-11864D26D059}
            this.OnSendMessage(null,"");
            if (this.isNew)
            {
                this.isNew = false;
            }
            this.Reset();
            return 0;
        }
        /// <summary>
        /// 设置门诊手术申请记录的状态为1 {0E140FEC-2F31-4414-8401-86E78FA3ADDC}
        /// </summary>
        /// <returns>1为成功，-1为失败</returns>
        private int SetOperationMzFinish()
        {
            if (this.applyoldMZ == null)
            {
                return 1;
            }
            if (Environment.OperationManager.DoAnaeRecord(this.applyoldMZ.ID, "4") < 1)
            {
                MessageBox.Show("更新收费标记失败！" + Environment.OperationManager.Err);
                return -1;
            }
            else
            {
                return 1;
            }
        }
        /// <summary>
        /// 即时消息{C51C7189-3DA7-4ebc-9D1F-11864D26D059}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        protected override void OnSendMessage(object sender, string msg)
        {
            Patient p = this.operationApplication.PatientInfo.Clone();
            //消息文字
            msg = "患者：" + p.Name +"住院号:"+ p.PID.PatientNO+"\n为急诊手术，请尽快进行手术安排!";
            //科室
            FS.FrameWork.Models.NeuObject targetDept = this.cmbExeDept.Tag as FS.FrameWork.Models.NeuObject;

            base.OnSendMessage(targetDept, msg);

        }
        /// <summary>
        /// 作废旧门诊手术申请单{2F7180FD-7B71-4001-A4BB-DBA9B7D40BE8}
        /// </summary>
        /// <returns></returns>
        public int CancelApplyOldMZ()
        {
            if (this.applyoldMZ != null && this.applyoldMZ.ID.Length > 0)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {
                    if (Environment.OperationManager.CancelApplication(this.applyoldMZ) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Environment.OperationManager.Err, "提示");
                        return -1;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    return 1;
                }
                catch (Exception e)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(e.Message, "提示");
                    return -1;
                }
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 对于已安排的手术发作废申请单
        /// </summary>
        /// <returns></returns>
        public int CancelApply()
        {
            if (this.isNew) return -1;
            if (this.operationApplication.ID.Length == 0)
            {
                MessageBox.Show("请选择待作废申请单!", "提示");
                return -1;
            }

            this.operationApplication = Environment.OperationManager.GetOpsApp(this.operationApplication.ID);
            if (this.operationApplication == null)
            {
                MessageBox.Show("获取申请单信息出错!", "提示");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "4")
            {
                MessageBox.Show("该申请单已登记,不能作废!", "提示");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "5")
            {
                MessageBox.Show("该申请单已取消登记,不能作废！", "提示");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "6")
            {
                MessageBox.Show("该申请单是已手术未收费状态,不能作废!", "提示");
                return -1;
            }

            if (this.operationApplication.IsValid == false)
            {
                MessageBox.Show("该申请单已经作废!", "提示");
                return -1;
            }
            if (MessageBox.Show("是否对该安排的申请单发送作废申请到手术室?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return -1;

            //1.判断是否已经发送过作废申请
            //2.插入手术作废申请表

            ucCancelApplicationApply ucCancelApplicationApply = new ucCancelApplicationApply();
            ucCancelApplicationApply.Application = this.OperationApplication;
            FS.FrameWork.WinForms.Classes.Function.ShowControl(ucCancelApplicationApply);
            return 1;
            
        }

        /// <summary>
        /// 作废当前修改申请单
        /// </summary>
        /// <returns></returns>
        public int Cancel()
        {
            if (this.isNew) return -1;
            if (this.operationApplication.ID.Length == 0)
            {
                MessageBox.Show("请选择待作废申请单!", "提示");
                return -1;
            }

            this.operationApplication = Environment.OperationManager.GetOpsApp(this.operationApplication.ID);
            if (this.operationApplication == null)
            {
                MessageBox.Show("获取申请单信息出错!", "提示");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "4")
            {
                MessageBox.Show("该申请单已登记,不能作废!", "提示");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "5")
            {
                MessageBox.Show("该申请单已取消登记,不能作废！", "提示");
                return -1;
            }

            if (this.operationApplication.ExecStatus == "6")
            {
                MessageBox.Show("该申请单是已手术未收费状态,不能作废!", "提示");
                return -1;
            }

            if (this.operationApplication.IsValid == false)
            {
                MessageBox.Show("该申请单已经作废!", "提示");
                return -1;
            }
            if (MessageBox.Show("是否作废当前申请单?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return -1;
            


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                if (Environment.OperationManager.CancelApplication(this.operationApplication) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Environment.OperationManager.Err, "提示");
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }

           // MessageBox.Show("请电话通知手术室!", "提示");
            MessageBox.Show("作废成功!", "提示");
            return 0;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public int Print()
        {
            //打印预览
            if (operationApplication.PatientInfo.ID == "")
                return -1;

            if (this.operationApplication.PreDate == System.DateTime.MinValue)
            {
                #region  判断数据有效性
                if (operationApplication.PatientInfo.ID == "")
                {
                    MessageBox.Show("请选择申请患者!", "提示");
                    return -1;
                }
                if (this.txtDiag.Tag == null)
                {
                    MessageBox.Show("术前诊断不能为空!", "提示");
                    return -1;
                }
                if (this.txtOperation.Tag == null)
                {
                    MessageBox.Show("拟手术名称不能为空!", "提示");
                    return -1;
                }
                //if (this.cmbAnae.Tag == null || this.cmbAnae.Tag.ToString() == "")
                //{
                //    MessageBox.Show("麻醉方式不能为空!", "提示");
                //    return -1;
                //}

                ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
                //if (!this.cmbAnseWay.Visible)//chengym 12-8-17  具体不知道哪里加的，屏蔽 
                //{
                //    if (this.cmbAnseWay.Tag == null || this.cmbAnseWay.Tag.ToString() == "")
                //    {
                //        MessageBox.Show("麻醉类别不能为空!", "提示");
                //        this.cmbAnseWay.Focus();
                //        return -1;
                //    }
                //}
                if (this.cmbExeDept.Tag == null || this.cmbExeDept.Tag.ToString() == "")
                {
                    MessageBox.Show("手术室不能为空!", "提示");
                    return -1;
                }
                if (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == "")
                {
                    MessageBox.Show("术者不能为空!", "提示");
                    return -1;
                }
                if (this.cmbHelper1.Tag == null || this.cmbHelper1.Tag.ToString() == "")
                {
                    MessageBox.Show("一助不能为空!", "提示");
                    return -1;
                }
                string helper1 = "";
                string helper2 = "";
                string helper3 = "";
                this.cmbHelper1.Tag.ToString();
                if (cmbHelper2.Tag != null)
                {
                    helper2 = this.cmbHelper2.Tag.ToString();
                }
                if (this.cmbHelper3.Tag != null)
                {
                    helper3 = this.cmbHelper3.Tag.ToString();
                }
                if ((helper1 == helper2 && helper1 != "") || (helper1 == helper3 && helper3 != "") || (helper2 == helper3 && helper3 != ""))
                {
                    MessageBox.Show("一助二助三助不能重复");
                    return -1;
                }
                if (this.cmbOrder.Text == "")
                {
                    MessageBox.Show("请指定台序!", "提示");
                    return -1;
                }
                //判断申请时间是否合法
                string rtn = Environment.OperationManager.PreDateValidity(this.dtOperDate.Value);
                if (rtn == "Error")
                {
                    MessageBox.Show(Environment.OperationManager.Err, "参数设置");
                    return -1;
                }
                else if (rtn == "Before")
                {
                    MessageBox.Show("申请时间不能小于当前时间!", "提示");
                    return -1;
                }
                #endregion

            }
            if (GetValue() == -1)
                return -1;

            #region 删除原来手术申请单信息 用 手术安排通知单代替
            //			ucCreateAppPrint ucCreateAppPrint1 = new ucCreateAppPrint();
            //			FS.HISFC.Object.RADT.PatientInfo patient=patientMgr.PatientQuery(operationApplication.PatientInfo.Patient.ID);
            //			if(patient==null)return -1;
            //			FS.HISFC.Object.Operator.OpsApplication t=operationApplication.Clone();
            //			t.PatientInfo=patient;
            //
            //			ucCreateAppPrint1.ControlValue = t;
            //FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            //p.ControlBorder = FS.HISFC.Components.Interface.Classes.enuControlBorder.None;

            //p.PrintPreview(10, 40, ucCreateAppPrint1);

            #endregion


            if (this.arrangeFormPrint == null)
            {
                this.arrangeFormPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Operation.IArrangeNotifyFormPrint)) as FS.HISFC.BizProcess.Interface.Operation.IArrangeNotifyFormPrint;
                if (this.arrangeFormPrint == null)
                {
                    MessageBox.Show("获得接口IArrangeNotifyFormPrint错误，请与系统管理员联系。");

                    return -1;
                }
            }

            this.arrangeFormPrint.OperationApplicationForm = this.operationApplication.Clone();
            this.arrangeFormPrint.IsPrintExtendTable = false;
            this.arrangeFormPrint.Print();

            if (isupdatestate) //是否更新状态
            {
             
                //if (Environment.OperationManager.DoAnaeRecord(this.operationApplication.ID, "3")!=1)
                //{
                //    MessageBox.Show("更新申请单状态失败。");
                //}
                 
            }



            //this.arrangeFormPrint.PrintPreview();

            return 0;
        }


       

        #region 手术
        FS.HISFC.Components.Operation.ucOpItem ucOpItem1 = null; 
        int ucOpItem1_SelectItem(Keys key)
        {
            this.ProcessOps();
            this.txtOperation.Focus();
            return 1;
        }
        private int ProcessOps()
        {
            FS.HISFC.Models.Fee.Item.Undrug item = null;
            if (this.ucOpItem1.GetItem(ref item) == -1)
            {
                //MessageBox.Show("获取项目出错!","提示");
                return -1;
            }
            dirty = true;
            this.contralActive.Text = (item as FS.HISFC.Models.Fee.Item.Undrug).Name;
            dirty = false;

            this.contralActive.Tag = item;
            this.ucOpItem1.Visible = false;

            return 0;
        }
        private void txtOperation_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtOperation;
            this.ucDiag1.Visible = false;
        }

        private void txtOperation2_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtOperation2;
            this.ucDiag1.Visible = false;
        }

        private void txtOperation3_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtOperation3;
            this.ucDiag1.Visible = false;
        }

        private void txtOperation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucOpItem1.Visible)
                {
                    if (this.ProcessOps() == -1)
                        return;
                }

                this.txtOperation2.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucOpItem1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucOpItem1.NextRow();
            }
        }

        private void txtOperation2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucOpItem1.Visible)
                {
                    if (this.ProcessOps() == -1)
                        return;
                }

                this.txtOperation3.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucOpItem1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucOpItem1.NextRow();
            }
        }

        private void txtOperation3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucOpItem1.Visible)
                {
                    if (this.ProcessOps() == -1)
                        return;
                }

                ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
                this.cmbAnae.Focus();
               // this.cmbAnseWay.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucOpItem1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucOpItem1.NextRow();
            }
        }

        private void txtOperation_TextChanged(object sender, EventArgs e)
        {

            if (!txtOperation.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            string text = this.txtOperation.Text;

            if (this.ucOpItem1.Visible == false) this.ucOpItem1.Visible = true;
            this.ucOpItem1.Location = new System.Drawing.Point(txtOperation.Location.X, txtOperation.Location.Y + txtOperation.Height + 2);
            ucOpItem1.BringToFront();
            this.ucOpItem1.Filter(text);
            this.txtOperation.Tag = null;
        }

        private void txtOperation2_TextChanged(object sender, EventArgs e)
        {
            if (!txtOperation2.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            string text = this.txtOperation2.Text;

            if (this.ucOpItem1.Visible == false) this.ucOpItem1.Visible = true;
            this.ucOpItem1.Location = new System.Drawing.Point(txtOperation2.Location.X, txtOperation2.Location.Y + txtOperation2.Height + 2);
            ucOpItem1.BringToFront();
            this.ucOpItem1.Filter(text);
            this.txtOperation2.Tag = null;
        }

        private void txtOperation3_TextChanged(object sender, EventArgs e)
        {
            if (!txtOperation3.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            string text = this.txtOperation3.Text;

            if (this.ucOpItem1.Visible == false) this.ucOpItem1.Visible = true;
            this.ucOpItem1.Location = new System.Drawing.Point(txtOperation3.Location.X, txtOperation3.Location.Y + txtOperation3.Height + 2);
            ucOpItem1.BringToFront();
            this.ucOpItem1.Filter(text);
            this.txtOperation3.Tag = null;
        }

        #endregion

        #region 诊断
        FS.HISFC.Components.Common.Controls.ucDiagnose ucDiag1 = null;
        int ucDiag1_SelectItem(Keys key)
        {
            this.ProcessDiag();
            this.txtDiag.Focus();
            return 1;
        } 
        private int ProcessDiag()
        {
            FS.HISFC.Models.HealthRecord.ICD item = null;
            if (this.ucDiag1.GetItem(ref item) == -1)
            {
                //MessageBox.Show("获取项目出错!","提示");
                return -1;
            }
            dirty = true;
            this.contralActive.Text = (item as FS.HISFC.Models.HealthRecord.ICD).Name;
            dirty = false;

            this.contralActive.Tag = item;
            this.ucDiag1.Visible = false;

            return 0;
        }
        private void txtDiag_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtDiag;
            this.ucOpItem1.Visible = false;
        }

        private void txtDiag2_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtDiag2;
            this.ucOpItem1.Visible = false;
        }

        private void txtDiag3_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtDiag3;
            this.ucOpItem1.Visible = false;
        }

        private void txtDiag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucDiag1.Visible)
                {
                    if (this.ProcessDiag() == -1) return;
                }

                this.txtDiag2.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucDiag1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucDiag1.NextRow();
            }
        }

        private void txtDiag2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucDiag1.Visible)
                {
                    if (this.ProcessDiag() == -1) return;
                }

                this.txtDiag3.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucDiag1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucDiag1.NextRow();
            }
        }

        private void txtDiag3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ucDiag1.Visible)
                {
                    if (this.ProcessDiag() == -1) return;
                }

                this.txtOperation.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.ucDiag1.PriorRow();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.ucDiag1.NextRow();
            }
        }

        private void txtDiag_TextChanged(object sender, EventArgs e)
        {
            if (!txtDiag.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            contralActive = this.txtDiag;
            string text = this.txtDiag.Text;
            this.ucDiag1.Location = new System.Drawing.Point(txtDiag.Location.X, txtDiag.Location.Y + txtDiag.Height + 2);
            ucDiag1.BringToFront();
            if (this.ucDiag1.Visible == false) this.ucDiag1.Visible = true;

            this.ucDiag1.Filter(text);
            this.txtDiag.Tag = null;
            ucDiag1.BringToFront();
        }

        private void txtDiag2_TextChanged(object sender, EventArgs e)
        {
            if (!txtDiag2.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            contralActive = this.txtDiag2;
            string text = this.txtDiag2.Text;
            ucDiag1.BringToFront();
            this.ucDiag1.Location = new System.Drawing.Point(txtDiag2.Location.X, txtDiag2.Location.Y + txtDiag2.Height + 2);
            if (this.ucDiag1.Visible == false) this.ucDiag1.Visible = true;

            this.ucDiag1.Filter(text);
            this.txtDiag2.Tag = null;
        }

        private void txtDiag3_TextChanged(object sender, EventArgs e)
        {
            if (!txtDiag3.Focused)
            {
                return;
            }
            //{6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
            if (this.InputBySelf() == 1)
            {
                return;
            }
            contralActive = this.txtDiag3;
            string text = this.txtDiag3.Text;
            this.ucDiag1.Location = new System.Drawing.Point(txtDiag2.Location.X, txtDiag3.Location.Y + txtDiag3.Height + 2);
            ucDiag1.BringToFront();
            if (this.ucDiag1.Visible == false) this.ucDiag1.Visible = true;

            this.ucDiag1.Filter(text);
            this.txtDiag3.Tag = null;
        }
        #endregion 
        /// <summary>
        /// 手工录入诊断和拟手术名称
        /// {6864A1A5-1965-41a2-BBA5-853DA4AD3FFF}feng.ch
        /// </summary>
        /// <returns>成功返回1</returns>
        private int InputBySelf()
        {
            if (this.chkInputBySelf.Checked==false)
            {
                return -1;
            }
            else 
            {
                FS.FrameWork.Models.NeuObject obj = null;
                FS.HISFC.Models.Base.Item item = null;
                //第一手术
                if (this.txtOperation.Text != "")
                {
                    item = new FS.HISFC.Models.Base.Item();
                    item.ID = "S991";
                    item.Name = this.txtOperation.Text;
                    item.PriceUnit = "次";
                    this.txtOperation.Tag = item;
                }
                //第二手术
                if (this.txtOperation2.Text != "")
                {
                    item = new FS.HISFC.Models.Base.Item();
                    item.ID = "S992";
                    item.Name = this.txtOperation2.Text;
                    item.PriceUnit = "次";
                    this.txtOperation2.Tag = item;
                }
                //第三手术
                if (this.txtOperation3.Text != "")
                {
                    item = new FS.HISFC.Models.Base.Item();
                    item.ID = "S993";
                    item.Name = this.txtOperation3.Text;
                    item.PriceUnit = "次";
                    this.txtOperation3.Tag = item;
                }
                //第一诊断
                if (this.txtDiag.Text != "")
                {
                    obj = new NeuObject();
                    obj.ID = "D991";
                    obj.Name = this.txtDiag.Text;
                    this.txtDiag.Tag = obj;
                }
                //第二诊断
                if (this.txtDiag2.Text != "")
                {
                    obj = new NeuObject();
                    obj.ID = "D992";
                    obj.Name = this.txtDiag2.Text;
                    this.txtDiag2.Tag = obj;
                }
                //第三诊断
                if (this.txtDiag3.Text != "")
                {
                    obj = new NeuObject();
                    obj.ID = "D993";
                    obj.Name = this.txtDiag3.Text;
                    this.txtDiag3.Tag = obj;
                }          
            }
            return 1;
        }
        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //if (!this.DesignMode)
            //{
            //    this.Reset();
            //    this.Init();
            //}
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Escape.GetHashCode())
            {
                this.ucOpItem1.Visible = false;
                this.ucDiag1.Visible = false;
                if (txtDiag.Focused)
                {
                    if (txtDiag.Text.Trim() == "")
                    {
                        txtDiag.Tag = null;
                    }
                }
                if (txtDiag3.Focused)
                {
                    if (txtDiag3.Text.Trim() == "")
                    {
                        txtDiag3.Tag = null;
                    }
                }
                if (txtDiag2.Focused)
                {
                    if (txtDiag2.Text.Trim() == "")
                    {
                        txtDiag2.Tag = null;
                    }
                }
                if (txtOperation.Focused)
                {
                    if (txtOperation.Text.Trim() == "")
                    {
                        txtOperation.Tag = null;
                    }
                }
                if (txtOperation3.Focused)
                {
                    if (txtOperation3.Text.Trim() == "")
                    {
                        txtOperation3.Tag = null;
                    }
                }
                if (txtOperation2.Focused)
                {
                    if (txtOperation2.Text.Trim() == "")
                    {
                        txtOperation2.Tag = null;
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.Operation.IArrangeNotifyFormPrint) };
            }
        }

        #endregion 

        private void cmbAnae_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbAnae2.Focus();
            }
        }
        private void cmbAnae2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbSpecial.Focus();
            }
        }
        private void dtOperDate_KeyDown(object sender, KeyEventArgs e)
        {//
            if (e.KeyCode == Keys.Enter)
            {
                cmbExeDept.Focus();
            }
        }

        private void cmbExeDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbTableType.Focus();
            }
            
        }

        private void cmbTableType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbDoctor.Focus();
            }
        }

        private void cmbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comDept.Focus();
            }
        }

        private void comDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOrder.Focus();
            }
        }

        private void cmbHelper1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbHelper2.Focus();
            }
        }

        private void cmbHelper2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbHelper3.Focus();
            }
        }

        private void cmbHelper3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOpType.Focus();
            }
        }

        private void cmbSpecial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbHelper1.Focus();
            }
        }

        private void cmbOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbHelper1.Focus();
            }
        }

        private void cbxNeedQX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxNeedXH.Focus();
            }
        }
        private void cmbOpType_KeyDown(object sender, KeyEventArgs e)
        {
            this.cmbIncitepe.Focus();
        }
        private void cbxNeedXH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOwn.Focus();
            }

        }

        private void cmbOwn_KeyDown(object sender, KeyEventArgs e)
        {//rtbApplyNote
            if (e.KeyCode == Keys.Enter)
            {
                cmbApplyDoct.Focus();
            }
        }

        private void ucApplicationForm_Load(object sender, EventArgs e)
        {

        }

        ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
        private void cmbAnseWay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbAnae.Focus();
            }
        }
        //{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
        private void cmbIncitepe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxNeedQX.Focus();
            }
        }

        /// <summary>
        /// 根据医生职级对应显示手术级别  {8794572D-2030-4692-B7B8-95000D75F698}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.comDept.Tag = ((FS.HISFC.Models.Base.Employee)(((FS.FrameWork.WinForms.Controls.NeuComboBox)(sender)).SelectedItem)).Dept.ID;
                
                this.cmbOpType.ClearItems();

                ArrayList al = Environment.IntegrateManager.GetConstantList("OPERATETYPE");


                ArrayList alType = new ArrayList();
                // "LEVELOPERATETYPE"
                // {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                if (isOwnPrivilege)
                {
                    NeuObject NeuObj = Environment.IntegrateManager.GetConstant("LEVELOPERATETYPE", ((FS.HISFC.Models.Base.Employee)(((FS.FrameWork.WinForms.Controls.NeuComboBox)(sender)).SelectedItem)).Level.ID);

                    if (string.IsNullOrEmpty(NeuObj.Memo))
                    {
                        return;
                    }

                    string[] operateType = NeuObj.Memo.Split('|');



                    foreach (NeuObject obj in al)
                    {
                        for (int i = 0; i < operateType.Length; i++)
                        {
                            if (obj.ID == operateType[i])
                            {
                                alType.Add(obj);
                            }
                        }
                    }


                }
                else
                {
                    if (string.IsNullOrEmpty(cmbDoctor.Tag.ToString()))
                    {
                        return;
                    }

                    FS.HISFC.BizLogic.Order.Medical.Ability abilityManager = new FS.HISFC.BizLogic.Order.Medical.Ability();
                    List<FS.HISFC.Models.Order.Medical.Popedom> popedomList = new List<FS.HISFC.Models.Order.Medical.Popedom>();
                    popedomList = abilityManager.QueryPopedomByEmplID(cmbDoctor.Tag.ToString());

                    foreach (NeuObject obj in al)
                    {
                        foreach (FS.HISFC.Models.Order.Medical.Popedom popedom in popedomList)
                        {
                            if (popedom.PopedomType.ID == "0")
                            {
                                if (obj.ID == popedom.Popedoms.ID)
                                {
                                    alType.Add(obj);
                                }
                            }

                        }
                    }

                }

                this.cmbOpType.AddItems(alType);
            }
            catch
            {

            }
            finally
            {

            }
        }

        /// <summary>
        /// 手工录入切换时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkInputBySelf_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkInputBySelf.Checked)//由手工录入切换到非手工录入时，将text清空，必须重新选择诊断和手术名称
            {
                DialogResult r = MessageBox.Show("是否确定要转成非手工录入？"+"\r\n"+"如果选择'确定',术前诊断和拟手术名称将会清空", "警示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (r == DialogResult.OK)
                {
                    this.txtDiag.Text = string.Empty;
                    this.txtDiag2.Text = string.Empty;
                    this.txtDiag3.Text = string.Empty;
                    this.txtOperation.Text = string.Empty;
                    this.txtOperation2.Text = string.Empty;
                    this.txtOperation3.Text = string.Empty;
                }
                else
                {
                    this.chkInputBySelf.Checked = true;
                }
            }
            else
            {
                InputBySelf();
            }
        }

        private void SetNote()
        {
            //设置注意事项
            FS.HISFC.BizProcess.Integrate.Manager ctlMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            try
            {	//查询手术申请截至时间
                string control = ctlMgr.QueryControlerInfo("optime");

                if (control != "" && control != "-1") this.lbNote.Text = "要求在" + control + this.note;//"前发送手术申请，否则将使用接台。

                if (this.cmbExeDept.Items.Count > 0)
                {
                    ArrayList list = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("ExeDept");
                    if (list.Count == 2)
                    {
                        this.cmbExeDept.Text = list[0].ToString();
                        this.cmbExeDept.Tag = list[1].ToString();
                    }
                }
            }
            catch { }
        }


        public void Clean()
        {
            Reset();
          
        }


        public int Update(string state)
        {

            if (this.operationApplication == null && string.IsNullOrEmpty(operationApplication.PatientInfo.ID))
            {
                MessageBox.Show("请选择需要操作的申请单！");
                return -1;
            }
            else
            {
                if (Environment.OperationManager.DoAnaeRecord(this.operationApplication.ID, state)!=1)
                {
                    MessageBox.Show("更新申请单状态失败。");
                    return -1;
                }
            }
            if (state == "3")
            {
                MessageBox.Show("手术申请已排班！");
                return -1;
            }
            else if (state == "4")
            {
                MessageBox.Show("手术申请已收费，患者出院将不会限制！");
            }
            return 1;
        }

        private void cmbOperKind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbBodyPosion.Focus();
            }

        }

        private void cmbBodyPosion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPortPosition.Focus();
            }
        }

        private void txtPortPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiag.Focus();
            }
            
        }

        private void cmbSpecial_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbExeDept.Focus();
            }
        }

        private void cmbTableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (alTableTypeCompare == null || alTableTypeCompare.Count == 0)
            {
                return;
            }
            this.cmbOrder.ClearItems();
            this.cmbOrder.AddItems(hsTableTypeCompare[this.cmbTableType.Text] as ArrayList);
        }

        private void dtOperDate_ValueChanged(object sender, EventArgs e)
        {
            this.nlbOrdered.Text = this.GetOrdered(((FS.HISFC.Models.Base.Employee)(Environment.OperationManager.Operator)).Dept.ID, this.dtOperDate.Value);
        }

        private void cmbSpecial_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
