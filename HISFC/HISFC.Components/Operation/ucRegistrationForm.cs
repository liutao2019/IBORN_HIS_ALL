using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Operation;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [功能描述: 手术登记单]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2006-12-12]<br></br>
    /// <修改记录 
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucRegistrationForm : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucRegistrationForm()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
            {
                this.InitControl();
                this.Clear();
            }
        }

        #region 字段

        private OperationRecord operationRecord = new OperationRecord();
        /// <summary>
        /// 手术申请或登记科室
        /// </summary>
        //private string dept;
        /// <summary>
        /// 是否新录入
        /// </summary>
        public bool IsNew = true;
        /// <summary>
        /// 是否补录
        /// </summary>
        private bool isRenew = false;

        private bool isCancled = false;

        /// <summary>
        /// 手术规模是否必录
        /// </summary>
        private bool isOpScaleNeeded = true;

        private FS.HISFC.BizProcess.Interface.Operation.IRecordFormPrint recordFormPrint;
        FS.FrameWork.Public.ObjectHelper employHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.HISFC.BizLogic.Operation.OpsTableManage opsTableMgr = new FS.HISFC.BizLogic.Operation.OpsTableManage();
        FS.HISFC.BizProcess.Integrate.Operation.OpsDiagnose opsDiagnose = new FS.HISFC.BizProcess.Integrate.Operation.OpsDiagnose();
        #endregion

        #region 属性
        /// <summary>
        /// 手术是否已被取消
        /// </summary>
        public bool IsCancled
        {
            set
            {
                isCancled = value;
            }
        }
        /// <summary>
        /// 是否是手工登记手术 
        /// </summary>
        public bool HandInput
        {
            get
            {
                return isRenew;
            }
            set
            {
                try
                {
                    this.Clear();
                    isRenew = value;
                    this.SetEnable(isRenew);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private FS.HISFC.Models.Base.Employee var = new Employee();
        /// <summary>
        ///  新建患者
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperationAppllication OperationApplication
        {
            set
            {
                this.Clear();//清空
                if (this.IsNew)
                    this.operationRecord = new OperationRecord();

                OperationAppllication apply = value;

                if (apply.PatientInfo.ID.Length == 0)
                {
                    MessageBox.Show("传入申请单为空!", "提示");
                    return;
                }
                #region 赋值
                this.lbName.Text = apply.PatientInfo.Name;//姓名
                this.lbSex.Text = apply.PatientInfo.Sex.Name;//性别

                int age = Environment.OperationManager.GetDateTimeFromSysDateTime().Year - apply.PatientInfo.Birthday.Year;
                if (age == 0)
                    age = 1;
                this.lbAge.Text = age.ToString() + "岁";//年龄

                this.lbPatient.Text = apply.PatientInfo.PID.PatientNO;//住院号
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                if (apply.PatientSouce == "2")
                {
                    patientInfo = Environment.RadtManager.GetPatientInfomation(apply.PatientInfo.ID);
                }
                else
                {
                    FS.HISFC.BizProcess.Integrate.Registration.Registration regMana = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
                    ArrayList alReg = regMana.QueryPatient(apply.PatientInfo.ID);
                    if (alReg == null || alReg.Count == 0)
                    {
                        MessageBox.Show("没有该患者的挂号信息");
                        return;
                    }
                    FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();
                    regObj = alReg[0] as FS.HISFC.Models.Registration.Register;

                    patientInfo.ID = regObj.ID;//流水号
                    patientInfo.PID.PatientNO = regObj.PID.CardNO;//卡号
                    patientInfo.PID.CardNO = regObj.PID.CardNO;//卡号
                    patientInfo.Name = regObj.Name;//姓名
                    patientInfo.Birthday = regObj.Birthday;
                    patientInfo.Sex.ID = regObj.Sex.ID;
                    if (regObj.SeeDoct.Dept.ID == null || regObj.SeeDoct.Dept.ID == "")
                    {
                        patientInfo.PVisit.PatientLocation.Dept.ID = regObj.DoctorInfo.Templet.Dept.ID;
                        patientInfo.PVisit.PatientLocation.Dept.Name = regObj.DoctorInfo.Templet.Dept.Name;
                    }
                    else
                    {
                        patientInfo.PVisit.PatientLocation.Dept.ID = regObj.SeeDoct.Dept.ID;
                    }
                    patientInfo.Pact.PayKind.ID = regObj.Pact.PayKind.ID;
                }
                if (patientInfo == null || patientInfo.ID.Length == 0)
                {
                    MessageBox.Show("无此患者信息!", "提示");
                    return;
                }
                #region 结算类别

                NeuObject kind = Environment.GetPayKind(patientInfo.Pact.PayKind.ID);
                if (kind == null)
                    this.lbPaykind.Text = patientInfo.Pact.PayKind.ID;
                else
                    this.lbPaykind.Text = kind.Name;
                #endregion
                //by zlw 2006-5-24 取手术申请科室
                this.lbDept.Text = apply.PatientInfo.PVisit.PatientLocation.Dept.Name;
                this.lbDept.Tag = apply.PatientInfo.PVisit.PatientLocation.Dept.ID;
                //this.lbDept.Tag = this.operationRecord.OperationAppllication.PatientInfo.PVisit.PatientLocation.Dept.ID;
                //			this.lbDept.Text=p.PVisit.PatientLocation.Dept.Name;//住院科室
                this.lbBed.Text = apply.PatientInfo.PVisit.PatientLocation.Bed.ID;//床号
                this.lbFree.Text = patientInfo.FT.LeftCost.ToString();//余额
                this.lbOpsDept.Text = apply.ExeDept.Name;//手术室
                #region 台类型
                if (apply.TableType == "1")
                {
                    this.lbTableType.Text = "正台";
                }//正台
                else if (apply.TableType == "2")
                {
                    this.lbTableType.Text = "加台";
                }//加台
                else
                {
                    this.lbTableType.Text = "点台";
                }//点台
                // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20
                if (apply.ExeDept.ID == null || apply.ExeDept.ID == "")//手工录入的情况
                {
                    ArrayList tabletypeAl = new ArrayList();
                    FS.FrameWork.Models.NeuObject obj = new NeuObject();
                    obj.ID = "1";
                    obj.Name = "正台";
                    tabletypeAl.Add(obj);
                    obj = new NeuObject();
                    obj.ID = "2";
                    obj.Name = "加台";
                    tabletypeAl.Add(obj);
                    obj = new NeuObject();
                    obj.ID = "3";
                    obj.Name = "点台";
                    tabletypeAl.Add(obj);
                    this.lbTableType.AddItems(tabletypeAl);
                    this.lbTableType.Enabled = true;
                    this.txtDiag1.Enabled = true;
                    this.txtDiag2.Enabled = true;
                    this.txtDiag3.Enabled = true;
                }
                else
                {
                    this.lbTableType.Enabled = false;
                    this.txtDiag1.Enabled = false;
                    this.txtDiag2.Enabled = false;
                    this.txtDiag3.Enabled = false;
                }
                #endregion
                if (apply.ExeDept.ID == null || apply.ExeDept.ID=="")
                {
                    apply.ExeDept.ID = var.Dept.ID;
                }
                // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B}
                this.lbOpsDept.Text = Environment.GetDept(apply.ExeDept.ID).Name;//手术室
                lbOpsDept.Tag = apply.ExeDept.ID;//科室编码
                neuLabel27.Tag = apply.ApplyDoctor.ID;
                this.lbApplyDoct.Text = employHelper.GetName(apply.ApplyDoctor.ID);//申请医生
                // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B}
                if (apply.PreDate == System.DateTime.MinValue || apply.PreDate.Year < System.DateTime.Now.Date.AddYears(-1).Year)
                {
                    apply.PreDate = System.DateTime.Now;
                }
                //end {F8F24C97-75C6-4fe4-8907-86D253C8D97B}
                this.lbPreDate.Text = apply.PreDate.Date.ToString("yyyy-MM-dd");//预定手术时间
                #region 诊断
                // TODO: 添加诊断
                if (apply.DiagnoseAl.Count > 0)
                {
                    this.txtDiag1.Text = (apply.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ICD10.Name;//诊断
                    this.txtDiag1.Tag = (apply.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ICD10;//诊断

                    if (apply.DiagnoseAl.Count > 1)
                    {
                        this.txtDiag2.Text = (apply.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ICD10.Name;//诊断
                        this.txtDiag2.Tag = (apply.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ICD10;//诊断
                        if (apply.DiagnoseAl.Count > 2)
                        {
                            this.txtDiag3.Text = (apply.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ICD10.Name;//诊断
                            this.txtDiag3.Tag = (apply.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).ICD10;//诊断
                        }
                    }
                }

                #endregion
                #region 麻醉方式
                if (apply.AnesType.ID != null && apply.AnesType.ID != "")
                {
                    int le = apply.AnesType.ID.IndexOf("|");
                    if (le > 0)
                    {
                        NeuObject obj = Environment.GetAnes(apply.AnesType.ID.Substring(0,le));
                        if (obj != null)
                        {
                            this.lbAnae.Text = obj.Name;
                            this.lbAnae.Tag = obj.ID;
                        }
                        obj = Environment.GetAnes(apply.AnesType.ID.Substring(le + 1));
                        if (obj != null)
                        {
                            this.lbAnae2.Text = obj.Name;
                            this.lbAnae2.Tag = obj.ID;
                        }
                    }
                    else
                    {
                        NeuObject obj = Environment.GetAnes(apply.AnesType.ID);
                        if (obj != null)
                        {
                            this.lbAnae.Text = obj.Name;
                            this.lbAnae.Tag = obj.ID;
                        }
                    }
                }
                #endregion
                #region 手术名称
                if (apply.OperationInfos.Count > 0)
                {
                    this.txtOperation.Text = (apply.OperationInfos[0] as OperationInfo).OperationItem.Name;
                    this.txtOperation.Tag = (OperationInfo)apply.OperationInfos[0];

                    if (apply.OperationInfos.Count > 1)
                    {
                        this.txtOperation2.Text = (apply.OperationInfos[1] as OperationInfo).OperationItem.Name;
                        this.txtOperation2.Tag = (OperationInfo)apply.OperationInfos[1];
                        if (apply.OperationInfos.Count > 2)
                        {
                            this.txtOperation3.Text = (apply.OperationInfos[2] as OperationInfo).OperationItem.Name;
                            this.txtOperation3.Tag = (OperationInfo)apply.OperationInfos[2];
                        }
                    }
                }
                #endregion
                this.dtInRoomDate.Value = apply.PreDate;//手术入室时间
                this.dtOutRoomDate.Value = System.DateTime.Now;//出室时间
                this.dtBeginDate.Value = apply.PreDate;//手术开始时间
                this.dtEndDate.Value = System.DateTime.Now;
                this.cmbRoom.Tag = apply.RoomID;//房号
                // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20
                //ArrayList al = Environment.TableManager.GetOpsTable(apply.RoomID);
                //if (al == null)
                //{
                //    MessageBox.Show("获取房间" + apply.RoomID + "内的手术台号出错");
                //}

                //this.cmbOrder.Items.Clear();
                //this.cmbOrder.AddItems(al);

                if (apply.RoomID != null && apply.RoomID != "")
                {
                    this.cmbRoom.Tag = apply.RoomID;//房号
                    ArrayList al = Environment.TableManager.GetOpsTable(apply.RoomID);
                    if (al == null)
                    {
                        MessageBox.Show("获取房间" + apply.RoomID + "内的手术台号出错");
                    }

                    this.cmbOrder.Items.Clear();
                    this.cmbOrder.AddItems(al);
                }
                else
                {
                    ArrayList al = Environment.TableManager.GetOpsTableByDept(apply.ExeDept.ID);
                    if (al == null)
                    {
                        MessageBox.Show("获取房间" + apply.RoomID + "内的手术台号出错");
                    }

                    this.cmbOrder.Items.Clear();
                    this.cmbOrder.AddItems(al);
                }
                // end {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20
                this.cmbOrder.Tag = apply.OpsTable.ID;//台序

                this.cmbDoctor.Tag = apply.OperationDoctor.ID;//手术者
                #region 护士
                foreach (ArrangeRole role in apply.RoleAl)
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
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse1.ToString())
                    {
                        //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}防止接替护士赋值到正常护士控件上
                        if (role.SupersedeDATE == DateTime.MinValue && (this.cmbWash1.Tag == null || this.cmbWash1.Tag.ToString() == ""))
                        {
                            this.cmbWash1.Tag = role.ID;
                        }//洗手护士}
                        else if (this.cmbNextNurse1.Tag == null || this.cmbNextNurse1.Tag.ToString() == "")
                        {
                            this.cmbNextNurse1.Tag = role.ID;
                            if (role.SupersedeDATE != DateTime.MinValue)
                            {
                                this.dtNext1.Value = role.SupersedeDATE;
                            }
                        }
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse2.ToString())
                    {
                        if (role.SupersedeDATE == DateTime.MinValue && (this.cmbWash2.Tag == null || this.cmbWash2.Tag.ToString() == ""))
                        {
                            this.cmbWash2.Tag = role.ID;
                        }
                        else if (this.cmbNextNurse2.Tag == null || this.cmbNextNurse2.Tag.ToString() == "")
                        {
                            this.cmbNextNurse2.Tag = role.ID;
                            if (role.SupersedeDATE != DateTime.MinValue)
                            {
                                this.dtNext2.Value = role.SupersedeDATE;
                            }
                        }
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse3.ToString())
                    {
                        if (role.SupersedeDATE == DateTime.MinValue && (this.cmbWash3.Tag == null || this.cmbWash3.Tag.ToString() == ""))
                        {
                            this.cmbWash3.Tag = role.ID;
                        }
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse1.ToString())
                    {
                        if (role.SupersedeDATE == DateTime.MinValue && (this.cmbXH1.Tag == null || this.cmbXH1.Tag.ToString() == ""))
                        {
                            this.cmbXH1.Tag = role.ID;
                        }//巡回护士
                        else if (this.cmbNextNurse3.Tag == null || this.cmbNextNurse3.Tag.ToString() == "")
                        {
                            this.cmbNextNurse3.Tag = role.ID;
                            if (role.SupersedeDATE != DateTime.MinValue)
                            {
                                this.dtNext3.Value = role.SupersedeDATE;
                            }
                        }
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse2.ToString())
                    {
                        if (role.SupersedeDATE == DateTime.MinValue && (this.cmbXH2.Tag == null || this.cmbXH2.Tag.ToString() == ""))
                        {
                            this.cmbXH2.Tag = role.ID;
                        }
                        else if (this.cmbNextNurse4.Tag == null || this.cmbNextNurse4.Tag.ToString() == "")
                        {
                            this.cmbNextNurse4.Tag = role.ID;
                            if (role.SupersedeDATE != DateTime.MinValue)
                            {
                                this.dtNext4.Value = role.SupersedeDATE;
                            }
                        }
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse3.ToString())
                    {
                        if (role.SupersedeDATE == DateTime.MinValue && (this.cmbXH3.Tag == null || this.cmbXH3.Tag.ToString() == ""))
                        {
                            this.cmbXH3.Tag = role.ID;
                        }
                    }
                    #region 屏蔽
                    //else if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse.ToString())
                    //{
                    //    //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}防止接替护士赋值到正常护士控件上
                    //    if (role.SupersedeDATE == DateTime.MinValue && (this.cmbWash1.Tag == null || this.cmbWash1.Tag.ToString() == ""))
                    //    {
                    //        this.cmbWash1.Tag = role.ID;
                    //    }//洗手护士}
                    //    else if (role.SupersedeDATE == DateTime.MinValue && (this.cmbWash2.Tag == null || this.cmbWash2.Tag.ToString() == ""))
                    //    {
                    //        this.cmbWash2.Tag = role.ID;
                    //    }
                    //    else if (role.SupersedeDATE == DateTime.MinValue && (this.cmbWash3.Tag == null || this.cmbWash3.Tag.ToString() == ""))
                    //    {
                    //        this.cmbWash3.Tag = role.ID;
                    //    }
                    //    //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}
                    //    else if (this.cmbNextNurse1.Tag == null || this.cmbNextNurse1.Tag.ToString() == "")
                    //    {
                    //        this.cmbNextNurse1.Tag = role.ID;
                    //        if (role.SupersedeDATE != DateTime.MinValue)
                    //        {
                    //            this.dtNext1.Value = role.SupersedeDATE;
                    //        }                           
                    //    }
                    //    else if (this.cmbNextNurse2.Tag == null || this.cmbNextNurse2.Tag.ToString() == "")
                    //    {
                    //        this.cmbNextNurse2.Tag = role.ID;
                    //        if (role.SupersedeDATE !=DateTime.MinValue)
                    //        {
                    //            this.dtNext2.Value = role.SupersedeDATE;
                    //        }
                    //    }
                    //}
                    //else if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse.ToString())
                    //{
                    //    if (role.SupersedeDATE == DateTime.MinValue && (this.cmbXH1.Tag == null || this.cmbXH1.Tag.ToString() == ""))
                    //    {
                    //        this.cmbXH1.Tag = role.ID;
                    //    }//巡回护士
                    //    else if (role.SupersedeDATE == DateTime.MinValue && (this.cmbXH2.Tag == null || this.cmbXH2.Tag.ToString() == ""))
                    //    {
                    //        this.cmbXH2.Tag = role.ID;
                    //    }
                    //    else if (role.SupersedeDATE == DateTime.MinValue && (this.cmbXH3.Tag == null || this.cmbXH3.Tag.ToString() == ""))
                    //    {
                    //        this.cmbXH3.Tag = role.ID;
                    //    }
                    //    //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}
                    //    else if (this.cmbNextNurse3.Tag == null || this.cmbNextNurse3.Tag.ToString() == "")
                    //    {
                    //        this.cmbNextNurse3.Tag = role.ID;
                    //        if (role.SupersedeDATE != DateTime.MinValue)
                    //        {
                    //            this.dtNext3.Value = role.SupersedeDATE;
                    //        }
                    //    }
                    //    else if (this.cmbNextNurse4.Tag == null || this.cmbNextNurse4.Tag.ToString() == "")
                    //    {
                    //        this.cmbNextNurse4.Tag = role.ID;
                    //        if (role.SupersedeDATE != DateTime.MinValue)
                    //        {
                    //            this.dtNext4.Value = role.SupersedeDATE;
                    //        }
                    //    }
                    //}
                    #endregion
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.Anaesthetist.ToString())
                    {
                        this.cmbAnaeDoctor.Tag = role.ID;//麻醉医师
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.AnaesthesiaHelper.ToString())
                    {
                        this.cmbAnaeHelper1.Tag = role.ID;//麻醉医师助手
                    }
                }
                #endregion

                //手术规模
                this.cmbOpType.Tag = apply.OperationType.ID;
                //手术分类
                this.cmbOperKind.Tag = apply.OperateKind;
                //if (apply.OperateKind == "1")
                //{ this.cmbOperKind.SelectedIndex = 0; }//普通
                //else if (apply.OperateKind == "2")
                //{ this.cmbOperKind.SelectedIndex = 1; }//急诊
                //else
                //{
                //    this.cmbOperKind.SelectedIndex = 0;//感染
                //    this.cbxInfect.Checked = true;
                //}
                 
                this.rtbApplyNote.Text = apply.ApplyNote; 
                #endregion

                this.operationRecord.OperationAppllication = apply;
                #region {AC85B19A-8849-45b0-BD30-D8090F98B85F}
                //处理手术登记时，如果申请记录已进行批费，那么手术登记记录也置成批费
                this.operationRecord.IsCharged = apply.IsChange;
                #endregion
                comDept.Tag = this.operationRecord.OperationAppllication.OperationDoctor.Dept.ID;
                //this.IsNew = true;//新增 
                // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
                if (this.operationRecord.OperationAppllication.RelaCode.Memo != "HandInput")
                {
                    this.isRenew = false;
                }
                //this.isRenew = false;
                // end {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20
                this.ucDiag1.Visible = false;
                this.ucOpItem1.Visible = false;
                //{B9DDCC10-3380-4212-99E5-BB909643F11B}
                this.cmbAnseWay.Tag = apply.AnesWay;
                //{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
                this.cmbIncityep.Tag = this.operationRecord.OperationAppllication.InciType.ID;
                //{24517986-0535-44a3-A25F-F6FD4B362496}
                this.chkYNGERM.Checked =this.operationRecord.OperationAppllication.IsGermCarrying;
                //{5DFF5830-8094-4ee0-A830-93731510284C}
                this.ckbIsOtherHos.Checked = this.operationRecord.IsOtherHosDoc;
                this.txtDeviceRegist.Text = this.operationRecord.SpecialDevoice;
            }
        }

        /// <summary>
        /// 修改申请单
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperationRecord OperationRecord
        {
            set
            {

                this.operationRecord = value;
                this.OperationApplication = value.OperationAppllication;
                //if (value.OutDate != System.DateTime.MinValue)
                //{
                //    this.dtEndDate.Value = value.OutDate;
                //}
                if (value.EnterDate != System.DateTime.MinValue)
                {
                    this.dtInRoomDate.Value = value.EnterDate;
                }
                if (value.OutDate != System.DateTime.MinValue)
                {
                    this.dtOutRoomDate.Value = value.OutDate;
                }
                if (value.OpsEndDate != System.DateTime.MinValue)
                {
                    this.dtEndDate.Value = value.OpsEndDate;
                }
                //{455F0D5D-89B7-4e06-974A-931534B52AC3}手术开始时间
                if (value.OpsDate != System.DateTime.MinValue)
                {
                    this.dtBeginDate.Value = value.OpsDate;
                }
                this.cbxInfect.Checked = this.operationRecord.IsInfected;
                this.chbRescue.Checked = this.operationRecord.IsRescue;
                    this.rtbApplyNote.Text = this.operationRecord.Memo;
                this.rtbExtraMemo.Text = this.operationRecord.ExtraMemo;
            }
        }

        [Category("控件设置"), Description("手术规模是否必录"), DefaultValue(true)]
        public bool IsOpScaleNeeded
        {//{8A0C5C0D-129C-4ba8-98E5-A7530036F60F} 手术规模是否必录  20100914
            get
            {
                return this.isOpScaleNeeded;
            }
            set
            {
                this.isOpScaleNeeded = value;
                this.lblOpType.ForeColor = value ? Color.Blue : Color.Black;
            }
        }
        //{8FE825BB-C2FA-4c83-AD18-689F035C27EC} add20120417 洗手护士是否必填
        private bool isWashNurseNeeded = true;
        [Category("控件设置"), Description("洗手护士是否必填，默认true"), DefaultValue(true)]        
        public bool IsWashNurseNeeded
        {
            get
            {
                return isWashNurseNeeded;
            }
            set
            {
                isWashNurseNeeded = value;
                this.neuLabel2.ForeColor = value ? Color.Blue : Color.Black;
            }
        }

        private bool isOnlyOpsNurse = true;
        [Category("控件设置"), Description("单加载手术室护士，默认true"), DefaultValue(true)]
        public bool IsOnlyOpsNurse
        {
            get
            {
                return isOnlyOpsNurse;
            }
            set
            {
                isOnlyOpsNurse = value;
            }
        }
        #endregion

        #region 方法

        public void InitControl()
        {
            var = (FS.HISFC.Models.Base.Employee)opsTableMgr.Operator;
            FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            employHelper.ArrayObject = managerMgr.QueryEmployeeAll();
            //房间号
            this.cmbRoom.Items.Clear();
            ArrayList al = Environment.TableManager.GetRoomsByDept(Environment.OperatorDeptID);
            if (al != null)
            {
                this.cmbRoom.AddItems(al);
                this.cmbRoom.IsListOnly = true;
            }

            //申请手术室 
            ArrayList alRet = Environment.IntegrateManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.OP);
            lbOpsDept.AddItems(alRet);
            //lbOpsDept.AddItems(Environment.IntegrateManager.QueryDepartment("1"));//手术室
            //麻醉类型
            ArrayList alRetAnes=Environment.IntegrateManager.GetConstantList("CASEANESTYPE");
            this.lbAnae.AddItems(alRetAnes);//(FS.HISFC.Models.Base.EnumConstant.ANESTYPE));
            this.lbAnae2.AddItems(alRetAnes);
            ArrayList deptList = Environment.IntegrateManager.GetDeptmentAllValid();
            this.comDept.AddItems(deptList); //加载科室
            //性别
            //lbSex.AddItems(FS.HISFC.Models.RADT.EnumSex.List());
            //科室 
            //lbDept.AddItems(Environment.IntegrateManager.GetDepartment());
            //申请医生
            al = Environment.IntegrateManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            //this.lbApplyDoct.Items.Clear();
            //this.lbApplyDoct.AddItems(al);
            //术者
            this.cmbDoctor.Items.Clear();
            this.cmbDoctor.AddItems(al);
            this.cmbDoctor.IsListOnly = true;
            //一助
            this.cmbHelper1.Items.Clear();
            this.cmbHelper1.AddItems(al);
            this.cmbHelper1.IsListOnly = true;
            //二助
            this.cmbHelper2.Items.Clear();
            this.cmbHelper2.AddItems(al);
            this.cmbHelper2.IsListOnly = true;
            //三助手
            this.cmbHelper3.Items.Clear();
            this.cmbHelper3.AddItems(al);
            this.cmbHelper3.IsListOnly = true;
            //麻醉医师
            this.cmbAnaeDoctor.Items.Clear();
            this.cmbAnaeDoctor.AddItems(al);
            this.cmbAnaeDoctor.IsListOnly = true;
            //麻醉医师助手
            this.cmbAnaeHelper1.Items.Clear();
            this.cmbAnaeHelper1.AddItems(al);
            this.cmbAnaeHelper1.IsListOnly = true;
            //洗手护士
            if (this.isOnlyOpsNurse)
            {
                FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                if (empl != null)
                {
                    al = Environment.IntegrateManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N, empl.Dept.ID);
                }
                else
                {
                    al = Environment.IntegrateManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N);
                }
            }
            else
            {
                al = Environment.IntegrateManager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N);
            }
            if (al == null) al = new ArrayList();
            this.cmbWash1.Items.Clear();
            this.cmbWash1.AddItems(al);
            this.cmbWash1.IsListOnly = true;
            //洗手二
            this.cmbWash2.AddItems(al);
            this.cmbWash2.IsListOnly = true;
            //洗手三
            this.cmbWash3.AddItems(al);
            this.cmbWash3.IsListOnly = true;
            //巡回
            this.cmbXH1.AddItems(al);
            this.cmbXH1.IsListOnly = true;
            //巡回2
            this.cmbXH2.AddItems(al);
            this.cmbXH2.IsListOnly = true;
            //巡回3
            this.cmbXH3.AddItems(al);
            this.cmbXH3.IsListOnly = true;
            //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}
            //接替洗手护士1
            this.cmbNextNurse1.AddItems(al);
            this.cmbNextNurse1.IsListOnly = true;
            //接替洗手护士2
            this.cmbNextNurse2.AddItems(al);
            this.cmbNextNurse2.IsListOnly = true;
            //接替巡回护士1
            this.cmbNextNurse3.AddItems(al);
            this.cmbNextNurse3.IsListOnly = true;
            //接替巡回护士2
            this.cmbNextNurse4.AddItems(al);
            this.cmbNextNurse4.IsListOnly = true;
            //手术规模
            al = Environment.IntegrateManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.OPERATETYPE);
            if (al == null) al = new ArrayList();
            this.cmbOpType.AddItems(al);
            this.cmbOpType.IsListOnly = true;

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

            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            //麻醉类别'麻醉类别（局麻或选麻，医生申请时填写）//{B9DDCC10-3380-4212-99E5-BB909643F11B}
            ArrayList a1 = Environment.IntegrateManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ANESWAY);
            this.cmbAnseWay.AddItems(a1);
            this.cmbAnseWay.IsListOnly = true;

            alRet = Environment.IntegrateManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.INCITYPE);

            this.cmbIncityep.AddItems(alRet);
            this.cmbIncityep.IsListOnly = true;
            //台序{3A7BEF28-9DF2-4c06-BBC8-0BBCED720689}
            alRet = Environment.IntegrateManager.GetConstantList("OperatoinOrder");
            this.cmbOrder.ClearItems();
            this.cmbOrder.AddItems(alRet);
            //手术类别从常数里面取
            this.cmbOperKind.ClearItems();
            alRet = Environment.IntegrateManager.GetConstantList("Operatetype");
            this.cmbOperKind.AddItems(alRet);
        }
        #region 手术
        FS.HISFC.Components.Operation.ucOpItem ucOpItem1 = null;
        private System.Windows.Forms.Control contralActive = new Control();
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
            this.contralActive.Text = (item as FS.HISFC.Models.Fee.Item.Undrug).Name;
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

                //{B9DDCC10-3380-4212-99E5-BB909643F11B}
                //this.lbAnae.Focus();
                this.cmbAnseWay.Focus();
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
            // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            if (this.InputBySelf() == 1)
            {
                return;
            }
            // end {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
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
            // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            if (this.InputBySelf() == 1)
            {
                return;
            }
            // end {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
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
            // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            if (this.InputBySelf() == 1)
            {
                return;
            }
            // end {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            string text = this.txtOperation3.Text;

            if (this.ucOpItem1.Visible == false) this.ucOpItem1.Visible = true;
            this.ucOpItem1.Location = new System.Drawing.Point(txtOperation3.Location.X, txtOperation3.Location.Y + txtOperation3.Height + 2);
            ucOpItem1.BringToFront();
            this.ucOpItem1.Filter(text);
            this.txtOperation3.Tag = null;
        }
        private void txtOperation3_Leave(object sender, EventArgs e)
        {
            //if (!ucOpItem1.Focused)
            //{
            //    this.ucOpItem1.Visible = false;
            //}
        }
        #endregion 
        #region 诊断
        FS.HISFC.Components.Common.Controls.ucDiagnose ucDiag1 = null;
        int ucDiag1_SelectItem(Keys key)
        {
            // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            this.ProcessDiag();
            this.txtDiag1.Focus();
            // end {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            return 1;
        }
        private void txtDiag1_Enter(object sender, EventArgs e)
        {
            contralActive = this.txtDiag1;
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
        private void txtDiag1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtDiag1.Visible)
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
                if (this.txtDiag2.Visible)
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
                if (this.txtDiag3.Visible)
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
        private void txtDiag1_TextChanged(object sender, EventArgs e)
        {
            if (!txtDiag1.Focused)
            {
                return;
            }
            // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            if (this.InputBySelf() == 1)
            {
                return;
            }
            // end {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            contralActive = this.txtDiag1;
            string text = this.txtDiag1.Text;
            this.ucDiag1.Location = new System.Drawing.Point(txtDiag1.Location.X, txtDiag1.Location.Y + txtDiag1.Height + 2);
            ucDiag1.BringToFront();
            if (this.ucDiag1.Visible == false) this.ucDiag1.Visible = true;

            this.ucDiag1.Filter(text);
            this.txtDiag1.Tag = null;
        }

        private void txtDiag2_TextChanged(object sender, EventArgs e)
        {
            if (!txtDiag2.Focused)
            {
                return;
            }
            // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            if (this.InputBySelf() == 1)
            {
                return;
            }
            // end {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            contralActive = this.txtDiag1;
            string text = this.txtDiag2.Text;
            this.ucDiag1.Location = new System.Drawing.Point(txtDiag2.Location.X, txtDiag2.Location.Y + txtDiag2.Height + 2);
            txtDiag2.BringToFront();
            if (this.txtDiag2.Visible == false) this.txtDiag2.Visible = true;

            this.ucDiag1.Filter(text);
            this.txtDiag2.Tag = null;
        }

        private void txtDiag3_TextChanged(object sender, EventArgs e)
        {
            if (!txtDiag3.Focused)
            {
                return;
            }
            // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            if (this.InputBySelf() == 1)
            {
                return;
            }
            // end {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
            contralActive = this.txtDiag3;
            string text = this.txtDiag3.Text;
            this.ucDiag1.Location = new System.Drawing.Point(txtDiag3.Location.X, txtDiag3.Location.Y + txtDiag3.Height + 2);
            txtDiag2.BringToFront();
            if (this.txtDiag3.Visible == false) this.txtDiag3.Visible = true;

            this.ucDiag1.Filter(text);
            this.txtDiag3.Tag = null;
        }
        private int ProcessDiag()
        {
            FS.HISFC.Models.HealthRecord.ICD item = null;
            if (this.ucDiag1.GetItem(ref item) == -1)
            {
                //MessageBox.Show("获取项目出错!","提示");
                return -1;
            } 
            this.contralActive.Text = (item as FS.HISFC.Models.HealthRecord.ICD).Name; 
            this.contralActive.Tag = item;
            this.ucDiag1.Visible = false;

            return 0;
        }
        #endregion 
        /// <summary>
        /// 清屏
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {


            this.lbAge.Text = "";//年龄
            this.lbBed.Text = "";//床号
            this.lbDept.Text = "";//科室
            this.lbFree.Text = "";//余额
            this.lbName.Text = "";//姓名
            this.lbPaykind.Text = "";
            this.lbSex.Text = "";
            this.lbPatient.Text = "";//住院号
            this.lbOpsDept.Text = "";//手术室
            this.lbTableType.Text = "";//台类型
            this.lbApplyDoct.Text = "";//申请医生
            this.lbPreDate.Text = "";//预约日期
            this.lbAnae.Text = "";//麻醉方式
            this.lbAnae2.Text = "";
            this.lbAnae2.Tag = null;
            txtDiag1.Text = "";
            txtDiag1.Tag = null;
            txtDiag2.Text = "";
            txtDiag2.Tag = null;
            txtDiag3.Text = "";
            txtDiag3.Tag = null;
            this.txtDiag1.Text = "";
            this.txtDiag2.Text = "";
            this.txtDiag3.Text = "";
            this.txtDiag1.Tag = null;
            this.txtDiag2.Tag = null;
            this.txtDiag3.Tag = null; 
            this.txtOperation.Text = "";//手术名称
            this.txtOperation.Tag = null;
            this.txtOperation2.Text = "";
            this.txtOperation2.Tag = null;
            this.txtOperation3.Text = "";
            this.txtOperation3.Tag = null; 

            DateTime dtNow = Environment.OperationManager.GetDateTimeFromSysDateTime();
            this.dtInRoomDate.Value = dtNow;
            this.dtOutRoomDate.Value = dtNow;
            this.dtBeginDate.Value = dtNow;//开始时间
            this.dtEndDate.Value = dtNow;//结束时间

            this.lbOpsDept.Text = "";//房号
            this.lbOpsDept.Tag = null;
            this.cmbOrder.Text = "";//台序
            this.cmbAnaeDoctor.Text = "";//麻醉医师
            this.cmbAnaeDoctor.Tag = null;
            this.cmbAnaeHelper1.Text = "";//麻醉医师助手
            this.cmbAnaeHelper1.Tag = null;

            this.cmbDoctor.Text = "";//手术者
            this.cmbDoctor.Tag = null;
            this.cmbHelper1.Text = "";//一助
            this.cmbHelper1.Tag = null;
            this.cmbHelper2.Text = "";//二助
            this.cmbHelper2.Tag = null;
            this.cmbHelper3.Text = "";//三助
            this.cmbHelper3.Tag = null;
            this.cmbWash1.Text = "";//洗手1
            this.cmbWash1.Tag = null;
            this.cmbWash2.Text = "";//洗手2
            this.cmbWash2.Tag = null;
            this.cmbWash3.Text = "";//洗手3
            this.cmbWash3.Tag = null;
            this.cmbXH1.Text = "";//巡回1
            this.cmbXH1.Tag = null;
            this.cmbXH2.Text = "";//巡回2
            this.cmbXH2.Tag = null;
            this.cmbXH3.Text = "";//巡回3
            this.cmbXH3.Tag = null;

            this.cmbOpType.Text = "";//手术规模
            this.cmbOpType.Tag = null;
            this.cmbOperKind.Tag = null;//分类			
            this.cbxInfect.Checked = false;
            this.chbRescue.Checked = false;
            this.rtbApplyNote.Text = "";
            this.rtbExtraMemo.Text = "";
            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            this.cmbAnseWay.Text = "";
            this.cmbAnseWay.Tag = null;
            this.cmbIncityep.Tag = null;
            //this.OperationApplication = new OperationAppllication();
            ////this.OperationApplication.PatientInfo.ID = string.Empty;

            //this.IsNew = true;
            //this.isRenew = false;
            //{E0CC2169-08A9-4500-BDB8-9271DEBD5679}
            this.lbLastTime.Visible = false;
            this.lbLastTime.Text = "";
            this.chkYNGERM.Checked = false;
            //{5DFF5830-8094-4ee0-A830-93731510284C}
            this.txtDeviceRegist.Text = "";
            ClearAllCheckBox();
            //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}
            this.cmbNextNurse1.Tag = null;
            this.cmbNextNurse2.Tag = null;
            this.cmbNextNurse3.Tag = null;
            this.cmbNextNurse4.Tag = null;
            this.dtNext1.Value = Environment.OperationManager.GetDateTimeFromSysDateTime();
            this.dtNext2.Value = Environment.OperationManager.GetDateTimeFromSysDateTime();
            this.dtNext3.Value = Environment.OperationManager.GetDateTimeFromSysDateTime();
            this.dtNext4.Value = Environment.OperationManager.GetDateTimeFromSysDateTime();
            return 0;
        }


        /// <summary>
        /// 实体赋值
        /// </summary>
        /// <returns></returns>
        private int GetValue()
        {
            if (HandInput)
            {

                if (this.lbPatient.Text == "")
                {
                    MessageBox.Show("请输入住院/门诊号");
                }

                if (this.lbTableType.Text == "正台")
                {
                    this.operationRecord.OperationAppllication.TableType = "1";
                }
                else if (this.lbTableType.Text == "加台")
                {
                    this.operationRecord.OperationAppllication.TableType = "2";
                }
                if (lbOpsDept.Tag != null)
                {
                    this.operationRecord.OperationAppllication.ExeDept.Name = this.lbOpsDept.Text;//手术室
                    this.operationRecord.OperationAppllication.ExeDept.ID = this.lbOpsDept.Tag.ToString();//手术室
                }

                isRenew = true;
            }
            operationRecord.OperationAppllication.ApplyDoctor.ID = neuLabel27.Tag.ToString();
            operationRecord.OperationAppllication.ApplyDoctor.Name = this.lbApplyDoct.Text;//申请医生
            if (this.IsNew && this.isRenew)
            {
                this.operationRecord.OperationAppllication.OperationDoctor.Dept = Environment.IntegrateManager.GetEmployeeInfo(cmbDoctor.Tag.ToString()).Dept;
                this.operationRecord.OperationAppllication.ID = Environment.OperationManager.GetNewOperationNo();
            }
            //新登记、非补录、非修改申请获得诊断
            //if (this.IsNew && !this.isRenew)
            //{
            //    foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase diag in this.operationRecord.OperationAppllication.DiagnoseAl)
            //    {
            //        //用术前血压属性记录诊断名称
            //        if (diag.IsMain) this.opRecord.ForePress = diag.Name;
            //    }
            //}
            //if (this.HandInput)
            //{
            // TODO: 添加诊断
            #region 诊断
            FS.HISFC.Models.HealthRecord.DiagnoseBase diag = new FS.HISFC.Models.HealthRecord.DiagnoseBase();
            diag.OperationNo = operationRecord.OperationAppllication.ID;//申请号
            diag.ICD10 = (FS.HISFC.Models.HealthRecord.ICD)this.txtDiag1.Tag;
            diag.ID = this.txtDiag1.Tag.ToString(); 
            diag.Name = this.txtDiag1.Text; 
            diag.Patient = operationRecord.OperationAppllication.PatientInfo.Clone();
            diag.DiagType.ID = "7";//诊断类型
            diag.DiagType.Name = FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OTHER.ToString();//术前诊断
            diag.DiagDate = opsTableMgr.GetDateTimeFromSysDateTime();//诊断时间
            diag.Doctor.ID = var.ID;//诊断医生
            diag.Doctor.Name = var.Name;//诊断医生
            diag.Dept.ID = var.Dept.ID;//诊断科室
            diag.IsValid = true;//是否有效
            diag.IsMain = true;//主诊断

            if (operationRecord.OperationAppllication.DiagnoseAl.Count == 0)
                diag.HappenNo = opsDiagnose.GetNewDignoseNo();//序号
            else
                diag.HappenNo = (operationRecord.OperationAppllication.DiagnoseAl[0] as FS.HISFC.Models.HealthRecord.DiagnoseBase).HappenNo;

            operationRecord.OperationAppllication.DiagnoseAl.Clear();
            operationRecord.OperationAppllication.DiagnoseAl.Add(diag);
            #region 第二诊断
            if (txtDiag2.Tag != null)
            {
                diag = new FS.HISFC.Models.HealthRecord.DiagnoseBase();
                diag.OperationNo = operationRecord.OperationAppllication.ID;//申请号
                diag.ICD10 = (FS.HISFC.Models.HealthRecord.ICD)this.txtDiag2.Tag;
                diag.ID = (this.txtDiag2.Tag as FS.HISFC.Models.HealthRecord.ICD).ID;
                diag.Name = (this.txtDiag2.Tag as FS.HISFC.Models.HealthRecord.ICD).Name;
                diag.Patient = operationRecord.OperationAppllication.PatientInfo.Clone();
                diag.DiagType.ID = "7";//诊断类型
                diag.DiagType.Name = FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OTHER.ToString();//术前诊断
                diag.DiagDate = opsTableMgr.GetDateTimeFromSysDateTime();//诊断时间
                diag.Doctor.ID = var.ID;//诊断医生
                diag.Doctor.Name = var.Name;//诊断医生
                diag.Dept.ID = var.Dept.ID;//诊断科室
                diag.IsValid = true;//是否有效
                diag.IsMain = false;//主诊断

                if (operationRecord.OperationAppllication.DiagnoseAl.Count == 1)
                    diag.HappenNo = opsDiagnose.GetNewDignoseNo();//序号
                else
                    diag.HappenNo = (operationRecord.OperationAppllication.DiagnoseAl[1] as FS.HISFC.Models.HealthRecord.DiagnoseBase).HappenNo;
                operationRecord.OperationAppllication.DiagnoseAl.Add(diag);
            }
            #endregion
            #region 第三诊断
            if (txtDiag3.Tag != null)
            {
                diag = new FS.HISFC.Models.HealthRecord.DiagnoseBase();
                diag.OperationNo = operationRecord.OperationAppllication.ID;//申请号
                diag.ICD10 = (FS.HISFC.Models.HealthRecord.ICD)this.txtDiag3.Tag;
                diag.ID = (this.txtDiag3.Tag as FS.HISFC.Models.HealthRecord.ICD).ID;
                diag.Name = (this.txtDiag3.Tag as FS.HISFC.Models.HealthRecord.ICD).Name;
                diag.Patient = operationRecord.OperationAppllication.PatientInfo.Clone();
                diag.DiagType.ID = "7";//诊断类型
                diag.DiagType.Name = FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OTHER.ToString();//术前诊断
                diag.DiagDate = opsTableMgr.GetDateTimeFromSysDateTime();//诊断时间
                diag.Doctor.ID = var.ID;//诊断医生
                diag.Doctor.Name = var.Name;//诊断医生
                diag.Dept.ID = var.Dept.ID;//诊断科室
                diag.IsValid = true;//是否有效
                diag.IsMain = false;//主诊断

                if (operationRecord.OperationAppllication.DiagnoseAl.Count == 2)
                    diag.HappenNo = opsDiagnose.GetNewDignoseNo();//序号
                else
                    diag.HappenNo = (operationRecord.OperationAppllication.DiagnoseAl[2] as FS.HISFC.Models.HealthRecord.DiagnoseBase).HappenNo;
                operationRecord.OperationAppllication.DiagnoseAl.Add(diag);
            }
            #endregion

            #endregion
            //}

            #region 麻醉方式
            //麻醉方式
            this.operationRecord.OperationAppllication.AnesType.ID = this.lbAnae.Tag.ToString();
            this.operationRecord.OperationAppllication.AnesType.Name = this.lbAnae.Text;
            if (this.lbAnae2.Tag != null && this.lbAnae2.Text.Trim() != "")
            {
                this.operationRecord.OperationAppllication.AnesType.ID += "|" + this.lbAnae2.Tag.ToString();
                this.operationRecord.OperationAppllication.AnesType.Name += "|" + this.lbAnae2.Text;
            }
            //{B9DDCC10-3380-4212-99E5-BB909643F11B}
            this.operationRecord.OperationAppllication.AnesWay = this.cmbAnseWay.Tag.ToString();
            #endregion

            #region 手术项目
            this.operationRecord.OperationAppllication.OperationInfos.Clear();

            if (this.txtOperation.Tag != null)
            {
                this.operationRecord.OperationAppllication.AddOperation(this.txtOperation.Tag, true);
            }

            if (this.txtOperation2.Tag != null)
            {
                this.operationRecord.OperationAppllication.AddOperation(this.txtOperation2.Tag);
            }
            if (this.txtOperation3.Tag != null)
            {
                this.operationRecord.OperationAppllication.AddOperation(this.txtOperation3.Tag);
            }

            this.operationRecord.OperationAppllication.OperationType.ID = this.cmbOpType.Tag.ToString();
            #endregion

            this.operationRecord.OperationAppllication.RoomID = this.cmbRoom.Tag.ToString();//房号
            if (this.cmbOrder.Tag != null)
            {
                this.operationRecord.OperationAppllication.OpsTable.ID = this.cmbOrder.Tag.ToString();
            }
            else
            {
                this.operationRecord.OperationAppllication.OpsTable.ID = "";
            }

            //特殊说明
            this.operationRecord.Memo = this.rtbApplyNote.Text.Trim();
            this.operationRecord.ExtraMemo = this.rtbExtraMemo.Text.Trim();
            this.operationRecord.EnterDate = this.dtInRoomDate.Value;//手术入室时间
            this.operationRecord.OutDate = this.dtOutRoomDate.Value;//出室时间
            this.operationRecord.OpsDate = this.dtBeginDate.Value;//手术开始时间 
            this.operationRecord.OpsEndDate = this.dtEndDate.Value;//手术结束时间
            #region 术者
            ArrangeRole role = new ArrangeRole();
            role.OperationNo = this.operationRecord.OperationAppllication.ID;//申请号
            role.ID = this.cmbDoctor.Tag.ToString();//人员代码
            role.Name = this.cmbDoctor.Text;
            role.RoleType.ID = EnumOperationRole.Operator;//角色编码
            role.ForeFlag = "1";//术后录入
            this.operationRecord.OperationAppllication.RoleAl.Clear();
            this.operationRecord.OperationAppllication.RoleAl.Add(role);
            this.operationRecord.OperationAppllication.OperationDoctor.ID = role.ID;
            this.operationRecord.OperationAppllication.OperationDoctor.Name = role.Name;
            this.operationRecord.BloodPressureIn = this.txtDiag1.Text; //第一诊断
            #endregion
            #region 一助
            role = new ArrangeRole();
            role.OperationNo = this.operationRecord.OperationAppllication.ID;//申请号
            role.ID = this.cmbHelper1.Tag.ToString();//人员代码
            role.Name = this.cmbHelper1.Text;
            role.RoleType.ID = EnumOperationRole.Helper1;//角色编码
            role.ForeFlag = "1";//术后录入
            this.operationRecord.OperationAppllication.RoleAl.Add(role);
            this.operationRecord.OperationAppllication.HelperAl.Clear();
            this.operationRecord.OperationAppllication.HelperAl.Add(role);
            #endregion
            #region 二助
            if (this.cmbHelper2.Tag != null && this.cmbHelper2.Tag.ToString() != "")
            {
                role = new ArrangeRole();
                role.OperationNo = this.operationRecord.OperationAppllication.ID;//申请号
                role.ID = this.cmbHelper2.Tag.ToString();//人员代码
                role.Name = this.cmbHelper2.Text;
                role.RoleType.ID = EnumOperationRole.Helper2;//角色编码
                role.ForeFlag = "1";//术后录入
                this.operationRecord.OperationAppllication.RoleAl.Add(role);

                this.operationRecord.OperationAppllication.HelperAl.Add(role);
            }
            #endregion
            #region 三助
            if (this.cmbHelper3.Tag != null && this.cmbHelper3.Tag.ToString() != "")
            {
                role = new ArrangeRole();
                role.OperationNo = this.operationRecord.OperationAppllication.ID;//申请号
                role.ID = this.cmbHelper3.Tag.ToString();//人员代码
                role.Name = this.cmbHelper3.Text;
                role.RoleType.ID = EnumOperationRole.Helper3;//角色编码
                role.ForeFlag = "1";//术后录入
                this.operationRecord.OperationAppllication.RoleAl.Add(role);

                this.operationRecord.OperationAppllication.HelperAl.Add(role);
            }
            #endregion
            #region 洗手护士
            if (this.cmbWash1.Tag != null && this.cmbWash1.Tag.ToString() != "")
            {
                this.operationRecord.OperationAppllication.AddRole(this.cmbWash1.Tag.ToString(), this.cmbWash1.Text, "1",
                    EnumOperationRole.WashingHandNurse1,"");
            }
            if (this.cmbWash2.Tag != null && this.cmbWash2.Tag.ToString() != "")
            {
                this.operationRecord.OperationAppllication.AddRole(this.cmbWash2.Tag.ToString(), this.cmbWash2.Text, "1",
                    EnumOperationRole.WashingHandNurse2,"");
            }
            if (this.cmbWash3.Tag != null && this.cmbWash3.Tag.ToString() != "")
            {
                this.operationRecord.OperationAppllication.AddRole(this.cmbWash3.Tag.ToString(), this.cmbWash3.Text, "1",
                    EnumOperationRole.WashingHandNurse3,"");
            }
            #endregion
            #region 巡回护士
            if (this.cmbXH1.Tag != null && this.cmbXH1.Tag.ToString() != "")
            {
                this.operationRecord.OperationAppllication.AddRole(this.cmbXH1.Tag.ToString(), this.cmbXH1.Text, "1",
                    EnumOperationRole.ItinerantNurse1,"");
            }
            if (this.cmbXH2.Tag != null && this.cmbXH2.Tag.ToString() != "")
            {
                this.operationRecord.OperationAppllication.AddRole(this.cmbXH2.Tag.ToString(), this.cmbXH2.Text, "1",
                    EnumOperationRole.ItinerantNurse2,"");
            }
            if (this.cmbXH3.Tag != null && this.cmbXH3.Tag.ToString() != "")
            {
                this.operationRecord.OperationAppllication.AddRole(this.cmbXH3.Tag.ToString(), this.cmbXH3.Text, "1",
                    EnumOperationRole.ItinerantNurse3,"");
            }
            #endregion
            //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}
            #region 接替洗手护士
            if (this.cmbNextNurse1.Tag != null && this.cmbNextNurse1.Tag.ToString() != "")
            {
                //接替洗手护士
                this.operationRecord.OperationAppllication.AddRole(this.cmbNextNurse1.Tag.ToString(), this.cmbNextNurse1.Text, "1",
                    EnumOperationRole.WashingHandNurse1,this.dtNext1.Value.ToString());
            }
            if (this.cmbNextNurse2.Tag != null && this.cmbNextNurse2.Tag.ToString() != "")
            {
                this.operationRecord.OperationAppllication.AddRole(this.cmbNextNurse2.Tag.ToString(), this.cmbNextNurse2.Text, "1",
                    EnumOperationRole.WashingHandNurse2, this.dtNext2.Value.ToString());
            } 
            #endregion
            //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}
            #region 接替巡回护士
            if (this.cmbNextNurse3.Tag != null && this.cmbNextNurse3.Tag.ToString() != "")
            {
                this.operationRecord.OperationAppllication.AddRole(this.cmbNextNurse3.Tag.ToString(), this.cmbNextNurse3.Text, "1",
                    EnumOperationRole.ItinerantNurse1, this.dtNext3.Value.ToString());
            }
            if (this.cmbNextNurse4.Tag != null && this.cmbNextNurse4.Tag.ToString() != "")
            {
                this.operationRecord.OperationAppllication.AddRole(this.cmbNextNurse4.Tag.ToString(), this.cmbNextNurse4.Text, "1",
                    EnumOperationRole.ItinerantNurse2, this.dtNext4.Value.ToString());
            }
            #endregion

            #region 麻醉医师
            role = new ArrangeRole();
            role.OperationNo = this.operationRecord.OperationAppllication.ID;//申请号
            role.ID = this.cmbAnaeDoctor.Tag.ToString();//人员代码
            role.Name = this.cmbAnaeDoctor.Text;
            role.RoleType.ID = EnumOperationRole.Anaesthetist;//角色编码
            role.ForeFlag = "1";//术后录入
            this.operationRecord.OperationAppllication.RoleAl.Add(role);
            this.operationRecord.OperationAppllication.HelperAl.Clear();
            this.operationRecord.OperationAppllication.HelperAl.Add(role);
            #endregion
            #region 麻醉医师助手
            if (this.cmbAnaeHelper1.Tag != null && this.cmbAnaeHelper1.Tag.ToString() != "")
            {
                role = new ArrangeRole();
                role.OperationNo = this.operationRecord.OperationAppllication.ID;//申请号
                role.ID = this.cmbAnaeHelper1.Tag.ToString();//人员代码
                role.Name = this.cmbAnaeHelper1.Text;
                role.RoleType.ID = EnumOperationRole.AnaesthesiaHelper;//角色编码
                role.ForeFlag = "1";//术后录入
                this.operationRecord.OperationAppllication.RoleAl.Add(role);
                this.operationRecord.OperationAppllication.HelperAl.Clear();
                this.operationRecord.OperationAppllication.HelperAl.Add(role);
            }
            #endregion
            //手术分类
            //this.operationRecord.OperationAppllication.OperateKind = System.Convert.ToString(this.cmbOperKind.SelectedIndex + 1);
            this.operationRecord.OperationAppllication.OperateKind = this.cmbOperKind.Tag.ToString();
            //是否感染
            this.operationRecord.IsInfected = this.cbxInfect.Checked;
            this.operationRecord.IsRescue = this.chbRescue.Checked;
            this.operationRecord.OperationAppllication.IsFinished = true;
            this.operationRecord.OperationAppllication.PatientInfo.Weight = "0";//体重
            this.operationRecord.OperationAppllication.ExecStatus = "4";//登记完成
            this.operationRecord.OperationAppllication.OperationDoctor.Dept.ID  = this.comDept.Tag.ToString();
            //{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
            this.operationRecord.OperationAppllication.InciType.ID = this.cmbIncityep.Tag.ToString();
            //{24517986-0535-44a3-A25F-F6FD4B362496}feng.ch增加是否有菌手术标示
            this.operationRecord.OperationAppllication.IsGermCarrying =this.chkYNGERM.Checked;
            //{5DFF5830-8094-4ee0-A830-93731510284C}
            this.operationRecord.IsOtherHosDoc = this.ckbIsOtherHos.Checked; //外院专家标志
            this.operationRecord.SpecialDevoice = this.txtDeviceRegist.Text;//特殊设备仪器
            return 0;
        }

        /// <summary>
        /// 有效性验证
        /// </summary>
        /// <returns></returns>
        private int Valid()
        {
            if (this.IsNew)
            {
                if (this.operationRecord.OperationAppllication.IsValid == false)
                {
                    MessageBox.Show("该申请单已经作废!", "提示");
                    return -1;
                }
            }

            if (operationRecord.OperationAppllication.PatientInfo.ID.Length == 0)
            {
                MessageBox.Show("请选择申请患者!", "提示");
                return -1;
            }

            if (this.txtOperation.Tag == null && this.txtOperation2.Tag == null && this.txtOperation3.Tag == null)
            {
                MessageBox.Show("拟手术名称不能为空!", "提示");
                txtOperation.Focus();
                return -1;
            }
            if (dtInRoomDate.Value > dtOutRoomDate.Value)
            {
                MessageBox.Show("入室时间不能大于出室时间");
                dtBeginDate.Focus();
                return -1;
            }
            if (dtBeginDate.Value > dtEndDate.Value)
            {
                MessageBox.Show("开始时间不能大于结束时间");
                dtBeginDate.Focus();
                return -1;
            }

            if (this.cmbRoom.Tag == null || this.cmbRoom.Tag.ToString() == "")
            {
                MessageBox.Show("房号不能为空!", "提示");
                cmbRoom.Focus();
                return -1;
            }
            //			if(this.dtBeginDate.Value>this.dtEndDate.Value)
            //			{
            //				MessageBox.Show("手术开始时间必须小于结束时间!","提示");
            //				return -1;
            //			}
            if (this.cmbOrder.Text == "")
            {
                MessageBox.Show("台序不能为空!", "提示");
                cmbOrder.Focus();
                return -1;
            }
            if (this.cmbDoctor.Tag == null || this.cmbDoctor.Tag.ToString() == "")
            {
                MessageBox.Show("术者不能为空!", "提示");
                cmbDoctor.Focus();
                return -1;
            }
            if (this.comDept.Tag == null || this.comDept.Tag.ToString() == "" || this.comDept.Text.Trim() == "")
            {
                MessageBox.Show("术者科室不能为空");
                comDept.Focus();
                return -1;
            }
            if (this.cmbHelper1.Tag == null || this.cmbHelper1.Tag.ToString() == "")
            {
                MessageBox.Show("一助不能为空!", "提示");
                return -1;
            }

            if ((this.cmbWash1.Tag == null || this.cmbWash1.Tag.ToString() == "") &&
                (this.cmbWash2.Tag == null || this.cmbWash2.Tag.ToString() == "") &&
                (this.cmbWash3.Tag == null || this.cmbWash3.Tag.ToString() == ""))
            {//{8FE825BB-C2FA-4c83-AD18-689F035C27EC} add20120417 洗手护士是否必填
                if (this.IsWashNurseNeeded)
                {
                    MessageBox.Show("洗手护士不能为空!", "提示");
                    cmbWash1.Focus();
                    return -1;
                }
            }

            if (this.isOpScaleNeeded)
            {
                if (this.cmbOpType.Tag == null || this.cmbOpType.Tag.ToString() == "")
                {
                    MessageBox.Show("手术规模不能为空!", "提示");
                    cmbOpType.Focus();
                    return -1;
                }
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.rtbApplyNote.Text.Trim(), 200) == false)
            {
                MessageBox.Show("特殊说明必须小于100个汉字!", "提示");
                rtbApplyNote.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.rtbExtraMemo.Text.Trim(), 128) == false)
            {
                MessageBox.Show("备注必须小于64个汉字!", "提示");
                this.rtbExtraMemo.Focus();
                return -1;
            }
            string Oper1 = Conobj(txtOperation.Tag); //第一手术
            string Oper2 = Conobj(txtOperation2.Tag); //第二手术
            string Oper3 = Conobj(txtOperation3.Tag); //第三手术
            if ((Oper1 == Oper2 && Oper1 != "") || (Oper1 == Oper3 && Oper3 != "") || (Oper2 == Oper3 && Oper3 != ""))
            {
                MessageBox.Show("手术项目不能重复");
                txtOperation.Focus();
                return -1;
            }
            string Oper11 = Conobj(txtOperation.Text); //第一手术
            string Oper12 = Conobj(txtOperation2.Text); //第二手术
            string Oper13 = Conobj(txtOperation3.Text); //第三手术
            if ((Oper11 == Oper12 && Oper11 != "") || (Oper11 == Oper13 && Oper13 != "") || (Oper12 == Oper13 && Oper13 != ""))
            {
                MessageBox.Show("手术项目不能重复");
                txtOperation.Focus();
                return -1;
            }
            string Helper1 = Conobj(cmbHelper1.Tag); //一助
            string Helper2 = Conobj(cmbHelper2.Tag); //二助
            string Helper3 = Conobj(cmbHelper3.Tag); //三助
            if ((Helper1 == Helper2 && Helper1 != "") || (Helper1 == Helper3 && Helper3 != "") || (Helper2 == Helper3 && Helper3 != ""))
            {
                MessageBox.Show("一助二助三助不能重复");
                cmbHelper2.Focus();
                return -1;
            }
            string anaeDoctor = Conobj(cmbAnaeDoctor.Tag); //麻醉医师
            string anaeDoctorHelper1 = Conobj(cmbAnaeHelper1.Tag); //麻醉医师助手
            if ((anaeDoctor == anaeDoctorHelper1 && anaeDoctor != ""))
            {
                MessageBox.Show("麻醉医师与麻醉助手不能重复");
                cmbHelper2.Focus();
                return -1;
            }
            string Wash1 = Conobj(cmbWash1.Tag); //洗手护士一
            string Wash2 = Conobj(cmbWash2.Tag); //洗手护士二
            string Wash3 = Conobj(cmbWash3.Tag); //洗手护士三
            if ((Wash1 == Wash2 && Wash1 != "") || (Wash1 == Wash3 && Wash3 != "") || (Wash2 == Wash3 && Wash3 != ""))
            {
                MessageBox.Show("三个洗手护士不能重复");
                cmbWash2.Focus();
                return -1;
            }
            string XH1 = Conobj(cmbXH1.Tag); //巡回护士
            string XH2 = Conobj(cmbXH2.Tag); //巡回护士
            string XH3 = Conobj(cmbXH3.Tag); //巡回护士
            if ((XH1 == XH2 && XH1 != "") || (XH1 == XH3 && XH3 != "") || (XH2 == XH3 && XH3 != ""))
            {
                MessageBox.Show("三个巡回护士不能重复");
                cmbXH2.Focus();
                return -1;
            }
            //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}
            #region 校验接替护士信息
            string JZSH1 = Conobj(this.cmbNextNurse1.Tag); //交接洗手护士1
            string JZSH2 = Conobj(this.cmbNextNurse2.Tag); //交接洗手护士2
            string JZXH1 = Conobj(this.cmbNextNurse3.Tag); //交接巡回护士1
            string JZXH2 = Conobj(this.cmbNextNurse4.Tag); //交接巡回护士2
            if (JZSH1 == JZSH2 && JZSH1 != "")
            {
                MessageBox.Show("接替洗手护士不能重复");
                this.cmbNextNurse1.Focus();
                return -1;
            }
            if (JZXH1 == JZXH2 && JZXH1 != "")
            {
                MessageBox.Show("接替巡回护士不能重复");
                this.cmbNextNurse3.Focus();
                return -1;
            }
            if ((JZSH1 != "" && JZSH1 == Wash1) || (JZSH1 != "" && JZSH1 == Wash2) || (JZSH1 != "" && JZSH1 == Wash3))
            {
                MessageBox.Show("接替洗手护士一不能和洗手护士相同！");
                this.cmbNextNurse1.Focus();
                return -1;
            }
            if ((JZSH2 != "" && JZSH2 == Wash1) || (JZSH2 != "" && JZSH2 == Wash2) || (JZSH2 != "" && JZSH2 == Wash3))
            {
                MessageBox.Show("接替洗手护士二不能和洗手护士相同！");
                this.cmbNextNurse1.Focus();
                return -1;
            }
            if ((JZXH1 != "" && JZXH1 == XH1) || (JZXH1 != "" && JZXH1 == XH2) || (JZXH1 != "" && JZXH1 == XH3))
            {
                MessageBox.Show("接替巡回护士一不能和巡回护士相同！");
                this.cmbNextNurse1.Focus();
                return -1;
            }
            if ((JZXH2 != "" && JZXH2 == XH1) || (JZXH2 != "" && JZXH2 == XH2) || (JZXH2 != "" && JZXH2 == XH3))
            {
                MessageBox.Show("接替巡回护士二不能和巡回护士相同！");
                this.cmbNextNurse1.Focus();
                return -1;
            } 
            #endregion
            if (this.lbAnae.Text.IndexOf("局麻") < 0 || this.lbAnae2.Text.IndexOf("局麻") < 0)
            {
                if (this.cmbAnaeDoctor.Tag == null || this.cmbAnaeDoctor.Tag.ToString() == "")
                {
                    MessageBox.Show("麻醉医师不能为空!", "提示");
                    cmbAnaeDoctor.Focus();
                    return -1;
                }
            }
            return 0;
        }

        private string Conobj(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            try
            {
                //int CaseReturn = 0;
                if (Valid() == -1)
                    return -1;

                if (this.GetValue() == -1)
                    return -1;

                //数据库事务
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction trans = new
                //    FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //trans.BeginTransaction();

                Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                Environment.RecordManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //this.opdMgr.SetTrans(trans.Trans);
                //this.icdMgr.SetTrans(trans.Trans);
                //this.iteMgr.SetTrans(trans.Trans);
                int rtn = 0;

                //获取数据库系统时间，使手术登记和病案登记的操作时间相一致。——Add By Maokb
                DateTime inTime;
                inTime = Environment.OperationManager.GetDateTimeFromSysDateTime();
                // TODO: 添加病案
                //this.opDetail.OperDate = inTime;
                this.operationRecord.OperDate = inTime;

                //判断是否插入病案手术信息
                //CaseReturn = GetDetail(inTime);

                try
                {
                    this.operationRecord.OperationAppllication.PatientInfo.PVisit.PatientLocation.Dept.ID = this.lbDept.Tag.ToString();
                    if (this.IsNew)//新增
                    {
                        #region new
                        if (Environment.RecordManager.AddOperatorRecord(this.operationRecord) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Environment.RecordManager.Err, "提示");
                            return -1;
                        }

                        //更新申请单状态
                        if (this.isRenew == false)
                        {
                            rtn = Environment.OperationManager.DoOperatorRecord(this.operationRecord.OperationAppllication.ID);
                            if (rtn == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Environment.OperationManager.Err, "提示");
                                return -1;
                            }
                            if (rtn == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("该申请单状态已经改变,请刷新屏幕重新录入!", "提示");
                                return -1;
                            }
                        }
                        #region 登记手术项目
                        if (Environment.OperationManager.DelOperationInfo(this.operationRecord.OperationAppllication) == -1)//删除手术项目
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Environment.OperationManager.Err, "提示");
                            return -1;
                        }
                        //针对本申请单中涉及到的手术添加手术项目信息
                        foreach (OperationInfo OperateInfo in this.operationRecord.OperationAppllication.OperationInfos)
                        {
                            //添加手术项目信息
                            if (Environment.OperationManager.AddOperationInfo(this.operationRecord.OperationAppllication, OperateInfo) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Environment.OperationManager.Err, "提示");
                                return -1;
                            }
                        }
                        #endregion
                        #region 登记人员信息
                        if (Environment.OperationManager.ProcessRoleForApply(this.operationRecord.OperationAppllication) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Environment.OperationManager.Err, "提示");
                            return -1;
                        }
                        #endregion 
                        #endregion
                    }
                    else//修改
                    {
                        #region modify

                        if (Environment.RecordManager.GetModifyEnabled() != "1")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("您没有修改手术登记记录的权限!", "提示");
                            return -1;
                        }

                        //先判断状态
                        if (this.operationRecord.IsValid == false)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("该申请单已经作废!", "提示");
                            return -1;
                        }

                        if (Environment.RecordManager.UpdateOperatorRecord(this.operationRecord) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Environment.RecordManager.Err, "提示");
                            return -1;
                        }

                        #region 登记手术项目
                        if (Environment.OperationManager.DelOperationInfo(this.operationRecord.OperationAppllication) == -1)//删除手术项目
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Environment.OperationManager.Err, "提示");
                            return -1;
                        }
                        //针对本申请单中涉及到的手术添加手术项目信息
                        foreach (OperationInfo OperateInfo in this.operationRecord.OperationAppllication.OperationInfos)
                        {
                            //添加手术项目信息
                            if (Environment.OperationManager.AddOperationInfo(this.operationRecord.OperationAppllication, OperateInfo) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Environment.OperationManager.Err, "提示");
                                return -1;
                            }
                        }
                        #endregion
                        #region 登记人员信息
                        if (Environment.OperationManager.ProcessRoleForApply(this.operationRecord.OperationAppllication) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Environment.OperationManager.Err, "提示");
                            return -1;
                        }
                        #endregion

                        // TODO: 添加病案
                        #region 登记病案信息 --Add By maokb

                        //if (CaseReturn == 0)
                        //{
                        //    //删除原来纪录
                        //    if (opdMgr.DeleteByCodeAndTime(operDate, this.opDetail.InpatientNO) == -1)
                        //    {
                        //        FS.FrameWork.Management.PublicTrans.RollBack();
                        //        MessageBox.Show(this.opdMgr.Err, "提示");
                        //        return -1;
                        //    }

                        //    //添加更改后的记录
                        //    if (this.alDetail != null)
                        //    {
                        //        foreach (FS.HISFC.Object.Case.OperationDetail opdinfo in alDetail)
                        //        {
                        //            if (opdMgr.Insert(FS.HISFC.Management.Case.frmTypes.DOC, opdinfo) == -1)
                        //            {
                        //                FS.FrameWork.Management.PublicTrans.RollBack();
                        //                MessageBox.Show(this.opdMgr.Err, "提示");
                        //                return -1;
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        FS.FrameWork.Management.PublicTrans.RollBack();
                        //        MessageBox.Show("没有要登记的手术项目", "提示");
                        //        return -1;
                        //    }
                        //}

                        #endregion
                        #endregion
                    }
                    // begin {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
                    if (this.operationRecord.OperationAppllication.RelaCode.Memo == "HandInput")
                    {
                        #region 诊断信息
                        int returnValue = opsDiagnose.DeleteDiagnoseByOperationNO(this.operationRecord.OperationAppllication.ID);
                        //遍历要加入的诊断信息列表(OpsApp.DiagnoseAl)
                        foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase willAddDiagnose in this.operationRecord.OperationAppllication.DiagnoseAl)
                        {
                            if (opsDiagnose.CreatePatientDiagnose(willAddDiagnose) == -1) return -1;
                        }
                        #endregion
                    }
                    //end  {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20 chengym
                    FS.FrameWork.Management.PublicTrans.Commit();
                    this.isRenew = false;
                }
                catch (Exception e)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(e.Message, "提示");
                    return -1;
                }

                MessageBox.Show("登记成功!", "提示");
                this.ucDiag1.Visible = false;
                this.ucOpItem1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// 作废登记单
        /// </summary>
        /// <returns></returns>
        public int Cancel()
        {


            if (this.isCancled)
            {
                MessageBox.Show("该手术病区已做废!", "提示");
                return -1;
            }
            if (this.IsNew)
            {
                MessageBox.Show("该手术还没有做登记,不能作废,或者没有双击手术申请信息!", "提示");
                return -1;
            }

            DialogResult dr = MessageBox.Show("【作废】操作将把该手术置为“作废”状态，该状态不可恢复\n，是否继续", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.No)
            {
                return -1;
            }

            //开始事务

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            int rtn = 0;
            Environment.RecordManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                //没有作废手术项目,如果统计需要自己添加作废功能,或者关联一下
                rtn = Environment.RecordManager.CancelRecord(this.operationRecord.OperationAppllication.ID);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Environment.RecordManager.Err, "提示");
                    return -1;
                }

                rtn = Environment.RecordManager.CacelApply(this.operationRecord.OperationAppllication.ID);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("该登记单作废失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }

                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("该登记单已经作废!", "提示");
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
            MessageBox.Show("作废成功!", "提示");

            return 0;
        }
        //{80D89813-7B64-4acf-A2CD-55BFD9F1E7C6}
        public int DeleleteRegInfo()
        {
            //删除登记信息

            DialogResult dr = MessageBox.Show("【取消】操作将把该手术申请恢复到“未登记”状态\n，是否继续", "提示", MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.No)
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            int returnValue = Environment.RecordManager.DeleteOpsRecord(this.operationRecord.OperationAppllication.ID);

            //returnValue =  Environment.OperationManager.DelOperationInfo(this.operationRecord.OperationAppllication);
            //returnValue =  Environment.OperationManager.DelArrangeRoleAll(this.operationRecord.OperationAppllication);

            returnValue = Environment.OperationManager.DoAnaeRecord(this.operationRecord.OperationAppllication.ID,"3");

            FS.FrameWork.Management.PublicTrans.Commit();

            return 1;

        }
        /// <summary>
        /// 锁定
        /// </summary>
        public void SetEnable(bool type)
        {
            lbName.Enabled = type;
            //			lbName.BackColor = System.Drawing.Color.MintCream;
            lbSex.Enabled = type;
            //			lbSex.BackColor = System.Drawing.Color.MintCream;
            lbAge.Enabled = type;
            //			lbAge.BackColor = System.Drawing.Color.MintCream;
            lbPatient.Enabled = type;
            //			lbPatient.BackColor = System.Drawing.Color.MintCream;
            lbPaykind.Enabled = type;
            //			lbPaykind.BackColor = System.Drawing.Color.MintCream;

            //允许进行科室修改
            //			lbDept.Enabled = type;
            //			lbDept.BackColor = System.Drawing.Color.MintCream;

            lbBed.Enabled = type;
            //			lbBed.BackColor = System.Drawing.Color.MintCream;
            lbFree.Enabled = type;
            //			lbFree.BackColor = System.Drawing.Color.MintCream;
            //lbOpsDept.Enabled = type;
            //			lbOpsDept.BackColor = System.Drawing.Color.MintCream;
            lbTableType.Enabled = type;
            //			lbTableType.BackColor = System.Drawing.Color.MintCream;
            lbApplyDoct.Enabled = type;
            //			lbApplyDoct.BackColor = System.Drawing.Color.MintCream;
            lbPreDate.Enabled = type;
            //			lbPreDate.BackColor = System.Drawing.Color.MintCream;
            txtDiag1.Enabled = type;
            txtDiag1.BackColor = System.Drawing.Color.MintCream;
            txtDiag2.Enabled = type;
            txtDiag2.BackColor = System.Drawing.Color.MintCream;
            txtDiag3.Enabled = type;
            txtDiag3.BackColor = System.Drawing.Color.MintCream;
        }

        public int Print()
        {
            if (this.recordFormPrint == null)
            {
                this.recordFormPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Operation.IRecordFormPrint)) as FS.HISFC.BizProcess.Interface.Operation.IRecordFormPrint;
                if (this.recordFormPrint == null)
                {
                    MessageBox.Show("获得接口IRecordFormPrint错误，请与系统管理员联系。");

                    return -1;
                }
            }
            if (this.GetValue() == -1)
                return -1;

            this.recordFormPrint.OperationRecord = this.operationRecord;
            return this.recordFormPrint.Print();
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Escape.GetHashCode())
            {
                this.ucOpItem1.Visible = false;
            }
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.Operation.IRecordFormPrint) };
            }
        }

        #endregion   

        private void lbAnae_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.lbAnae2.Focus();
            }
        }
        private void lbAnae2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtInRoomDate.Focus();
            }
        }
        private void dtInRoomDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtOutRoomDate.Focus();
            }
        }
        private void dtOutRoomDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbRoom.Focus();
            }
        }
        private void dtBeginDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtEndDate.Focus();
            } 
        }

        private void dtEndDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOrder.Focus();
            } 
        }

        private void cmbRoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtBeginDate.Focus();
            } 
        }

        private void cmbOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbAnaeDoctor.Focus();
            } 
        }
        private void cmbAnaeDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbAnaeHelper1.Focus();
            } 
        }

        private void cmbAnaeHelper1_KeyDown(object sender, KeyEventArgs e)
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
                cmbHelper1.Focus();
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
                cmbWash1.Focus();
            }
        }

        private void cmbWash1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbWash2.Focus();
            }
        }

        private void cmbWash2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbWash3.Focus();
            }
        }

        private void cmbWash3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbXH1.Focus();
            }
        }

        private void cmbXH1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbXH2.Focus();
            }
        }

        private void cmbXH2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbXH3.Focus();
            }
        }

        private void cmbXH3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOpType.Focus();
            }
        }

        private void cmbOpType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbOperKind.Focus();
            }
        }

        private void cmbOperKind_KeyDown(object sender, KeyEventArgs e)
        {
            ////{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbIncityep.Focus();
            }
        }

        private void cbxInfect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chbRescue.Focus();
            }
        }

        private void chbRescue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rtbApplyNote.Focus();
            }
        }

        //{B9DDCC10-3380-4212-99E5-BB909643F11B}
        private void cmbAnseWay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.lbAnae.Focus();
            }
        }

        private void cmbIncityep_KeyDown(object sender, KeyEventArgs e)
        {
            ////{37A0B524-70DB-413c-8C33-AAC69C40EAC6}
            if (e.KeyCode == Keys.Enter)
            {
                cbxInfect.Focus();
            }
        }
        /// <summary>
        /// {E0CC2169-08A9-4500-BDB8-9271DEBD5679}feng.ch
        /// 当结束时间触发时候显示自动算出手术持续时间并显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtEndDate_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan ts1 = new TimeSpan(this.dtEndDate.Value.Ticks);
            TimeSpan ts2 = new TimeSpan(this.dtBeginDate.Value.Ticks);
            TimeSpan ts = ts2.Subtract(ts1).Duration();
            String spanTime = ts.Days.ToString()+"天"+ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒";
            this.lbLastTime.Text = "";
            this.lbLastTime.Visible = true;
            this.lbLastTime.Text = "此次手术持续时间：" + spanTime; 
        }
        //{5DFF5830-8094-4ee0-A830-93731510284C}feng.ch
        #region 特殊设备仪器登记
        /// <summary>
        /// 打开界面
        /// </summary>
        private void llbDevoice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.gbExtendInfo.Visible)
            {
                this.gbExtendInfo.Visible = false;
            }
            if (this.gbDegisner.Visible)
            {
                this.gbDegisner.Visible = false;
                this.txtDeviceRegist.Text = "";
                return;
            }
            this.gbDegisner.Visible = true;
            this.ScrollControlIntoView(this.gbDegisner);
            string[] str = null;
            //根据常数中维护的特殊设备自动生成复选框
            ArrayList al = Environment.IntegrateManager.GetConstantList("SpecialDevice");
            if (al == null)
            {
                MessageBox.Show("获取特殊设备仪器出错!" + Environment.IntegrateManager.Err);
                return;
            }
            #region 根据常数维护的特殊设备信息添加复选框，同时赋值TRUE OR FALSE
            int x = this.llbDevoice.Location.X;
            int y = this.llbDevoice.Location.Y;
            int row = al.Count / 5 + 1;
            if (this.txtDeviceRegist.Text.ToString().Length > 0)
            {
                str = this.txtDeviceRegist.Text.ToString().Split('|');
            }
            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    if ((j - 1) + (i - 1) * 5 > al.Count - 1)
                    {
                        break;
                    }
                    FS.FrameWork.Models.NeuObject conInfo = al[(j - 1) + (i - 1) * 5] as FS.FrameWork.Models.NeuObject;
                    FS.FrameWork.WinForms.Controls.NeuCheckBox checkBox = new FS.FrameWork.WinForms.Controls.NeuCheckBox();
                    checkBox.Name = conInfo.ID;
                    checkBox.Text = conInfo.Name;
                    checkBox.Tag = conInfo.Memo;
                    checkBox.AutoSize = true;
                    this.gbDegisner.Controls.Add(checkBox);
                    //取值赋值CHECK属性
                    if (str != null && str.Length > 0)
                    {
                        for (int z = 0; z <= str.Length - 1; z++)
                        {
                            if (checkBox.Name == str[z].ToString())
                            {
                                checkBox.Checked = true;
                            }
                        }
                    }
                    Point p = new Point(10, 10);
                    p.X += 80 * j - 40;
                    p.Y += 27 * i;
                    checkBox.Location = p;
                }
            }
            #endregion
        }
        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.gbDegisner.Visible = false;
        }
        /// <summary>
        /// 清空复选框
        /// </summary>
        private void ClearAllCheckBox()
        {
            foreach (Control c in this.gbDegisner.Controls)
            {
                FS.FrameWork.WinForms.Controls.NeuCheckBox checkBox = c as FS.FrameWork.WinForms.Controls.NeuCheckBox;
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                {
                    this.gbDegisner.Controls.Remove(checkBox);
                }
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            
            string str = "";
            this.txtDeviceRegist.Text = "";
            foreach (Control c in this.gbDegisner.Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                {
                    FS.FrameWork.WinForms.Controls.NeuCheckBox checkBox = c as FS.FrameWork.WinForms.Controls.NeuCheckBox;
                    if (checkBox.Checked)
                    {
                        //防止重复添加
                        if (str.Contains(checkBox.Name.ToString()))
                        {
                            continue;
                        }
                        str = str + checkBox.Name.ToString() + "|";
                    }
                }
            }
            this.txtDeviceRegist.Text = str;
            this.gbDegisner.Visible = false;
        } 
        #endregion
        //{69F783B4-65EB-4cc3-B489-2A7D5B5A5F00}feng.ch 增加一些扩展登记信息
        //接替洗手护士1，接替洗手护士2，接替巡回护士1，接替巡回护士2以及每个接替时间
        /// <summary>
        /// 交接信息登记界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llbLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {           
            if (this.gbDegisner.Visible)
            {
                this.gbDegisner.Visible = false;
            }
            if (this.gbExtendInfo.Visible)
            {
                this.gbExtendInfo.Visible = false;
            }
            else
            {
                this.gbExtendInfo.Visible = true;
                this.ScrollControlIntoView(this.gbExtendInfo);
            }
        }

        #region {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20
        /// <summary>
        /// 手工录入切换时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkInputBySelf_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkInputBySelf.Checked)//由手工录入切换到非手工录入时，将text清空，必须重新选择诊断和手术名称
            {
                DialogResult r = MessageBox.Show("是否确定要转成非手工录入？" + "\r\n" + "如果选择'确定',术前诊断和拟手术名称将会清空", "警示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (r == DialogResult.OK)
                {
                    this.txtDiag1.Text = string.Empty;
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

        /// <summary>
        /// 手工录入
        /// </summary>
        /// <returns></returns>
        private int InputBySelf()
        {
            if (this.chkInputBySelf.Checked == false)
            {
                return -1;
            }
            else
            {
                FS.HISFC.Models.HealthRecord.ICD obj = null;
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
                if (this.txtDiag1.Text != "")
                {
                    obj = new  FS.HISFC.Models.HealthRecord.ICD();
                    obj.ID = "D991";
                    obj.Name = this.txtDiag1.Text;
                    this.txtDiag1.Tag = obj;
                }
                //第二诊断
                if (this.txtDiag2.Text != "")
                {
                    obj = new FS.HISFC.Models.HealthRecord.ICD();
                    obj.ID = "D992";
                    obj.Name = this.txtDiag2.Text;
                    this.txtDiag2.Tag = obj;
                }
                //第三诊断
                if (this.txtDiag3.Text != "")
                {
                    obj = new FS.HISFC.Models.HealthRecord.ICD();
                    obj.ID = "D993";
                    obj.Name = this.txtDiag3.Text;
                    this.txtDiag3.Tag = obj;
                }
            }
            return 1;
        }

        #endregion {F8F24C97-75C6-4fe4-8907-86D253C8D97B} 直接登记 12-8-20

    }
}
