using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [功能描述: 婴儿登记组件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucBabyInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ucBabyInfo()
        {
            InitializeComponent();
        }

        #region 变量
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizProcess.Integrate.RADT radtInterMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.Models.RADT.PatientInfo MomInfo = null;
        FS.HISFC.Models.RADT.PatientInfo BabyInfo = null;
        /// <summary>
        /// 全部医生人员列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper allDoctHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 存储旧的婴儿信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo oldBabyInfo = null;

        /// <summary>
        /// 是否新登记
        /// </summary>
        bool isNew = false;
        private string InpatientNo = "";

        private int babyNo = 0;
        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();


        /// <summary>
        /// 住院接诊、召回、转入、换医师接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.NurseInterface.IArriveBase IArriveBase = null;

        #endregion

        #region 属性

        [Category("参数设置"), Description("按床号查询患者信息时，按患者的状态查询，如果全部则'ALL'")]
        public string PatientInState
        {
            get
            {
                return this.ucQueryInpatientNo.PatientInState;
            }
            set
            {
                this.ucQueryInpatientNo.PatientInState = value;
            }
        }

        [Category("参数设置"), Description("是否可以录入住院号"), DefaultValue(false)]
        public bool IsinputEnable
        {//{3E2B1A30-6689-4ea7-9946-DC60E1886D4E} 婴儿登记可以通过敲号单立使用 20100919
            get
            {
                return this.gpbPatientNO.Visible;
            }
            set
            {
                this.gpbPatientNO.Visible = value;
            }
        }

        private bool isModifyBirthday = true;
        /// <summary>
        /// 是否可以修改婴儿出生时间
        /// </summary>
        [Category("参数设置"), Description("是否可以修改婴儿出生时间"), DefaultValue(true)]
        public bool IsModifyBirthday
        {
            get
            {
                return this.isModifyBirthday;
            }
            set
            {
                this.isModifyBirthday = value;
            }
        }


        #endregion

        #region 函数
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());	 //取性别列表

            ArrayList al = managerIntegrate.GetConstantList("DeliveryMode");// {DD27333B-4CBF-4bb2-845D-8D28D616937E}
            this.cmbDeliveryMode.AddItems(al);	//分娩方式	

            allDoctHelper.ArrayObject = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            this.cmbResponsibleDoc.AddItems(allDoctHelper.ArrayObject);//责任医生

            this.cmbBlood.AddItems(FS.HISFC.Models.RADT.BloodTypeEnumService.List());	//取血型列表
            this.cmbBlood.IsListOnly = true;
            this.cmbSex.IsListOnly = true;
            this.dtBirthday.Value = this.inpatientManager.GetDateTimeFromSysDateTime();		//默认婴儿出生日期为系统时间
            this.dtOperatedate.Value = dtBirthday.Value;	//默认操作日期为系统时间
            ClearInfo();

            if (this.IArriveBase == null)
            {
                IArriveBase = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.NurseInterface.IArriveBase)) as FS.SOC.HISFC.BizProcess.NurseInterface.IArriveBase;
            }
        }


        /// <summary>
        /// 设置患者信息到控件
        /// </summary>
        /// <param name="patientInfo"></param>
        private void ShowBabyInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //如果没有患者信息,则清屏
            if (patientInfo == null)
            {
                this.ClearInfo();
                return;
            }
            this.txtInpatientNo.Text = patientInfo.PID.ID;	//住院号
            this.txtInpatientNo.Tag = patientInfo.User01;			//发生序号
            this.txtName.Text = patientInfo.Name;					//姓名
            this.cmbSex.Tag = patientInfo.Sex.ID;			//性别
            this.cmbBlood.Tag = patientInfo.BloodType.ID;	//血型
            this.txtHeight.Text = patientInfo.Height;		//身高
            this.txtWeight.Text = patientInfo.Weight;			//体重
            this.txtGestationalAge.Text = patientInfo.Gestation;     //胎龄
            this.dtBirthday.Value = patientInfo.Birthday;		//出生日期
            //this.dtOperatedate.Text = patientInfo.User03;			//登记日期
            this.cmbDeliveryMode.Tag = patientInfo.DeliveryMode.ID;
            this.dtOperatedate.Value = patientInfo.PVisit.InTime; //登记日期就是入院日期

            this.cmbResponsibleDoc.Tag = patientInfo.PVisit.ResponsibleDoctor.ID;//责任医生

            this.InpatientNo = patientInfo.ID; //住院流水号
            this.dtBirthday.Enabled = this.isModifyBirthday;

            if (patientInfo.PVisit.InState.ID.ToString() == "C")
            {
                btSave.Enabled = false;
                btCancel.Enabled = false;
            }
            else
            {
                btSave.Enabled = true;
                btCancel.Enabled = true;
            }
        }

        /// <summary>
        /// 清空控件
        /// </summary>
        private void ClearInfo()
        {

            this.txtInpatientNo.Text = "";	//发生序号
            this.txtInpatientNo.Tag = "";	//

            #region 选择母亲节点，新增的婴儿名称默认为：姓名+B
            //if (this.MomInfo != null)
            //{
            //    if (this.MomInfo.IsBaby)
            //        this.txtName.Text = "";		//姓名
            //    else
            //        this.txtName.Text = string.Format("{0}{1}", this.MomInfo.Name, "B");//姓名+B
            //}
            //else
            //    this.txtName.Text = "";		//姓名
            #endregion

            this.cmbSex.Tag = "M";		//默认性别为男性
            this.cmbBlood.Tag = "";	//血型为空
            this.cmbBlood.Text = "";	//血型为空

            try
            {
                this.cmbDept.Text = this.MomInfo.PVisit.PatientLocation.Dept.Name;	//婴儿科室=母亲科室
                this.cmbDept.Tag = this.MomInfo.PVisit.PatientLocation.Dept.ID;	//婴儿科室=母亲科室

            }
            catch { }
            this.txtHeight.Text = "";   //身高
            this.txtWeight.Text = "";		//体重
            this.txtGestationalAge.Text = "";         //胎龄
            this.dtBirthday.Value = this.inpatientManager.GetDateTimeFromSysDateTime();		//出生日期默认当天
            this.dtOperatedate.Value = this.inpatientManager.GetDateTimeFromSysDateTime();	//操作时间
            this.BabyInfo = null;
            this.dtBirthday.Enabled = true;
            this.cmbDeliveryMode.Tag = "";// {DD27333B-4CBF-4bb2-845D-8D28D616937E}
            this.cmbDeliveryMode.Text = "";
            this.cmbResponsibleDoc.Tag = "";//责任医生
            this.cmbResponsibleDoc.Text = "";
        }


        /// <summary>
        /// 取婴儿信息
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.PatientInfo GetBabyInfo()
        {			//发生序号
            if (this.BabyInfo == null)
            {
                //如果是新增婴儿且没有输入姓名,则生成婴儿姓名,并取该婴儿的序号
                this.BabyInfo = new FS.HISFC.Models.RADT.PatientInfo();
                //科室床位信息
                this.BabyInfo.PVisit.PatientLocation = this.MomInfo.PVisit.PatientLocation.Clone();
                //取控件中住院医生
                this.BabyInfo.PVisit.AdmittingDoctor = this.MomInfo.PVisit.AdmittingDoctor.Clone();
                //取控件中主治医生
                this.BabyInfo.PVisit.AttendingDoctor = this.MomInfo.PVisit.AttendingDoctor.Clone();
                //取控件中主任医生
                this.BabyInfo.PVisit.ConsultingDoctor = this.MomInfo.PVisit.ConsultingDoctor.Clone();
                //取控件中责任护士
                this.BabyInfo.PVisit.AdmittingNurse = this.MomInfo.PVisit.AdmittingNurse.Clone();
                //入院情况
                this.BabyInfo.PVisit.InSource = this.MomInfo.PVisit.InSource.Clone();
                //入院途径
                this.BabyInfo.PVisit.Circs = this.MomInfo.PVisit.Circs.Clone();
                //入院来源
                this.BabyInfo.PVisit.AdmitSource = this.MomInfo.PVisit.AdmitSource.Clone();

                this.BabyInfo.DeliveryMode.ID = this.cmbDeliveryMode.Tag.ToString();// {DD27333B-4CBF-4bb2-845D-8D28D616937E}
                this.BabyInfo.DeliveryMode.Name = this.cmbDeliveryMode.Text;
                //新婴儿
                isNew = true;

                //取婴儿最大序号
                string happenNo = this.txtInpatientNo.Tag.ToString();
                happenNo = this.inpatientManager.GetMaxBabyNO(this.MomInfo.ID);
                if (happenNo == "-1")
                {
                    MessageBox.Show("取婴儿最大序号时出错:" + this.inpatientManager.Err, "错误提示");
                    return null;
                }

                //加1得到当前婴儿序号
                happenNo = (FS.FrameWork.Function.NConvert.ToInt32(happenNo) + 1).ToString();

                this.BabyInfo.User01 = happenNo; //用User01来保存婴儿序号
                //取婴儿名
                if (this.txtName.Text == "")
                {
                    //根据目前名字和婴儿性别生成婴儿名字
                    this.BabyInfo.Name = CreatBabyName(this.MomInfo.Name, this.cmbSex.Tag.ToString(), int.Parse(happenNo));
                }
                else
                {
                    this.BabyInfo.Name = this.txtName.Text;
                }

                //入院日期等于系统当前时间
                //this.BabyInfo.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime();
                //入院时间取界面上的登记时间
                this.BabyInfo.PVisit.InTime = this.dtOperatedate.Value;

                //生成住院号
                this.BabyInfo.PID.ID = "B" + happenNo + MomInfo.PID.PatientNO.Substring(2);

                //生成住院流水号
                //this.BabyInfo.ID = MomInfo.ID.Substring(0, 4) + "B" + happenNo + MomInfo.PID.PatientNO.Substring(2);
                this.BabyInfo.ID = this.inpatientManager.GetNewInpatientNO();

                //生成门诊卡号
                this.BabyInfo.PID.CardNO = "TB" + happenNo + MomInfo.PID.PatientNO.Substring(3);

                this.BabyInfo.Pact.PayKind.ID = "01";			//自费
                this.BabyInfo.Pact.ID = "1";		//自费
                this.BabyInfo.Pact.Name = "自费儿童";//自费儿童
                this.BabyInfo.PVisit.InState.ID = "R";		//入院登记

                this.BabyInfo.FT.FixFeeInterval = 1;
            }
            else
            {
                oldBabyInfo = this.BabyInfo.Clone();

                //修改婴儿信息
                isNew = false;
                //如果是已登记的婴儿,提示用户输入婴儿姓名
                if (this.txtName.Text == "")
                {
                    MessageBox.Show("请输入婴儿姓名", "提示");
                    return null;
                }

                this.BabyInfo.Name = this.txtName.Text;//婴儿姓名

                //入院时间取界面上的登记时间
                this.BabyInfo.PVisit.InTime = this.dtOperatedate.Value;
            }


            this.BabyInfo.DeliveryMode.ID = this.cmbDeliveryMode.Tag.ToString();// {DD27333B-4CBF-4bb2-845D-8D28D616937E}
            this.BabyInfo.DeliveryMode.Name = this.cmbDeliveryMode.Text;
            this.BabyInfo.Sex.ID = this.cmbSex.Tag.ToString();				//性别
            this.BabyInfo.BloodType.ID = this.cmbBlood.Tag.ToString();	//血型
            this.BabyInfo.Height = this.txtHeight.Text;						//身高
            this.BabyInfo.Weight = this.txtWeight.Text;						//体重
            this.BabyInfo.Gestation = this.txtGestationalAge.Text;        //胎龄
            //{BF93FB13-06F4-4e4e-90E4-171B0F38CF6F}
            this.BabyInfo.Birthday = System.DateTime.Parse(this.dtBirthday.Value.ToString("yyyy-MM-dd HH:mm"));					//出生日期

            this.BabyInfo.PVisit.ResponsibleDoctor.ID = this.cmbResponsibleDoc.Tag.ToString();//责任医生编码
            this.BabyInfo.PVisit.ResponsibleDoctor.Name = this.cmbResponsibleDoc.Text;//责任医生姓名
                
            //判断姓名的长度
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.BabyInfo.Name, 50) == false)
            {
                txtName.Text = this.BabyInfo.Name;
                MessageBox.Show("名称过长请重新输入！");
                return null;
            }

            //妈妈的住院流水号
            this.BabyInfo.PID.MotherInpatientNO = this.MomInfo.ID;

            return this.BabyInfo;
        }

        int BabyNum = 0;
        /// <summary>
        /// 产生婴儿姓名
        /// </summary>
        /// <param name="MumName">妈妈名字</param>
        /// <param name="SexId">性别</param>
        /// <param name="HappenNo">发生序号</param>
        /// <returns></returns>
        private string CreatBabyName(string MumName, string SexId, int HappenNo)
        {
            string BabyName;
            //{FEA519C4-2379-40a9-8271-829A76E04EF6}屏蔽下面
            //BabyNum++;

            //switch (HappenNo)
            //{
            //    case 1:
            //        BabyName = "1";
            //        break;
            //    case 2:
            //        BabyName = "2";
            //        break;
            //    case 3:
            //        BabyName = "3";
            //        break;
            //    case 4:
            //        BabyName = "4";
            //        break;
            //    case 5:
            //        BabyName = "5";
            //        break;
            //    case 6:
            //        BabyName = "6";
            //        break;
            //    case 7:
            //        BabyName = "7";
            //        break;
            //    case 8:
            //        BabyName = "8";
            //        break;
            //    default:
            //        BabyName = "";
            //        break;
            //}

            #region {FEA519C4-2379-40a9-8271-829A76E04EF6}

            //if (SexId == FS.HISFC.Models.Base.EnumSex.M.ToString())
            //{

            //    return MumName + "之子" + HappenNo + "";
            //}
            //else
            //{
            //    return MumName + "之女" + HappenNo + "";
            //}
            return MumName + "BB" + HappenNo + "";
            #endregion
        }


        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="inPatientNo"></param>
        public virtual void RefreshList(string inPatientNo)
        {
            //情况控件
            ClearInfo();
            //显示婴儿列表
            ShowTreeView();
        }


        /// <summary>
        /// 显示婴儿列表
        /// </summary>
        private void ShowTreeView()
        {
            ArrayList al;
            al = new ArrayList();

            //根节点
            al.Add("婴儿列表");

            //取婴儿列表
            al = this.inpatientManager.QueryBabiesByMother(this.MomInfo.ID);
            if (al == null)
            {
                MessageBox.Show(inpatientManager.Err);
                return;
            }

            //根据婴儿的性别,计算每种性别的最大序号.并去婴儿在住院主表中的信息

            BabyNum = 0;
            foreach (FS.HISFC.Models.RADT.PatientInfo baby in al)
            {
                if (baby.PVisit.InState.ID.ToString() != "C")
                {
                    BabyNum++;
                }
            }


            //显示在树型控件中
            this.tvPatientList1.BeginUpdate();
            this.tvPatientList1.SetPatient(al);
            this.tvPatientList1.EndUpdate();

            //展开节点,显示根节点
            this.tvPatientList1.ExpandAll();
            this.tvPatientList1.SelectedNode = this.tvPatientList1.Nodes[0];

            foreach (TreeNode node in tvPatientList1.Nodes[0].Nodes)
            {
                FS.HISFC.Models.RADT.PatientInfo p = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                if (p.PVisit.InState.ID.ToString() == "C")
                {
                    node.ForeColor = Color.Red;
                    node.Text = node.Text + " 【取消】";
                }
            }
        }

        #endregion

        #region 事件
        private void btAdd_Click(object sender, System.EventArgs e)
        {
            this.ClearInfo();
            #region {FEA519C4-2379-40a9-8271-829A76E04EF6}

            this.babyNo = this.BabyNum + 1;

            this.txtName.Text = this.CreatBabyName(this.MomInfo.Name, this.cmbSex.Tag.ToString(), babyNo);

            #endregion
        }


        private void btCancel_Click(object sender, System.EventArgs e)
        {
            if (this.BabyInfo == null || this.txtInpatientNo.Text == "")
            {
                MessageBox.Show("请选择预取消的婴儿！", "提示");
                return;
            }

            try
            {
                //string sPatientNo = this.MomInfo.PID.PatientNO;
                //sPatientNo = "B" + this.txtInpatientNo.Tag.ToString() + sPatientNo.Substring(2);
                //sPatientNo = MomInfo.ID.Substring(0, 4) + sPatientNo;

                string sPatientNo = this.BabyInfo.ID;

                FS.HISFC.Models.RADT.PatientInfo p = this.inpatientManager.QueryPatientInfoByInpatientNO(sPatientNo);
                if ((p.FT.TotCost + p.FT.BalancedCost) > 0)
                {
                    MessageBox.Show("该婴儿已经发生费用，不能取消！");
                    return;
                }

                #region {23EE5EA6-27CB-49c9-810A-310A1515D85E}
                if (p.FT.PrepayCost > 0)
                {
                    MessageBox.Show("该婴儿预交金额大于0，不能取消！");
                    return;
                }
                #endregion

                //取消婴儿

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.inpatientManager.Connection);
                //t.BeginTransaction();

                this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //if (this.inpatientManager.DiscardBabyRegister(this.BabyInfo.ID) < 0)
                if (radtInterMgr.DiscardBabyRegister(BabyInfo) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("取消失败！" + this.inpatientManager.Err);
                }
                else
                {

                    if (this.IArriveBase != null)
                    {
                        if (IArriveBase.PatientArrive(FS.HISFC.Models.RADT.EnumArriveType.CallBack, this.BabyInfo, this.BabyInfo) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(IArriveBase.ErrInfo);
                            return;
                        }
                    }


                    FS.HISFC.Models.RADT.InStateEnumService status = new FS.HISFC.Models.RADT.InStateEnumService();
                    status.ID = "C";
                    p.ID = sPatientNo;
                    if (this.inpatientManager.UpdatePatientStatus(p, status) == -1)
                    {//更新为住院
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("取消登记失败！" + this.inpatientManager.Err);
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                        //查找婴儿所在的节点,并删除此节点
                        if (this.tv != null)
                        {//{3E2B1A30-6689-4ea7-9946-DC60E1886D4E} 婴儿登记可以通过敲号单立使用 20100919
                            TreeNode node = this.tv.FindNode(0, this.BabyInfo);
                            if (node != null) node.Remove();
                        }

                        //刷新婴儿列表
                        RefreshList(this.MomInfo.ID);
                        this.BabyInfo = null;
                        MessageBox.Show("取消登记成功！", "提示");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private FS.HISFC.Components.Common.Controls.tvPatientList tv = null;

        private void btSave_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbDeliveryMode.Tag.ToString()) || string.IsNullOrEmpty(cmbDeliveryMode.Text))
            {
                MessageBox.Show("分娩方式不能为空！");// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
                cmbDeliveryMode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.cmbResponsibleDoc.Tag.ToString()) || string.IsNullOrEmpty(cmbResponsibleDoc.Text))
            {
                MessageBox.Show("责任医生不能为空！");
                this.cmbResponsibleDoc.Focus();
                return;
            }
            //取患者主表中最新的信息,用来判断并发
            FS.HISFC.Models.RADT.PatientInfo patient = this.inpatientManager.QueryPatientInfoByInpatientNO(this.MomInfo.ID);
            if (patient == null)
            {
                MessageBox.Show(this.inpatientManager.Err);
                return;
            }
            //如果患者已不是在院状态,则不允许操作
            if (patient.PVisit.InState.ID.ToString() != this.MomInfo.PVisit.InState.ID.ToString())
            {
                MessageBox.Show("患者信息已发生变化,请刷新当前窗口", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //控制只有母亲才能做婴儿登记
            //if (this.MomInfo.Sex.ID.ToString() != "F" || this.MomInfo.ID.Substring(4, 1) == "B")
            if (this.MomInfo.Sex.ID.ToString() != "F" || this.MomInfo.PID.PatientNO.StartsWith("B"))
            {
                MessageBox.Show("只有母亲才能做婴儿登记！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cmbSex.Text == "")
            {
                MessageBox.Show("请选择性别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dtOperatedate.Value > this.inpatientManager.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("登记时间不能大于当前时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.GetBabyInfo();

            if (this.isNew)
            {
                if (BabyInfo.PVisit.InTime < this.inpatientManager.GetDateTimeFromSysDateTime().Date.AddDays(-1))
                {
                    MessageBox.Show("登记时间不能早于当前时间的前一天！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (this.oldBabyInfo.PVisit.InTime < BabyInfo.PVisit.InTime.AddDays(-1))
                {
                    MessageBox.Show("登记时间不能早于当前时间的前一天！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.BabyInfo.Name, 50) == false)
            {
                MessageBox.Show("名称不能超过10个汉字或者20个英文字符,请重新输入！", "名称过长");
                return;
            }
            ////判断身高是否为数字
            //for (int i = 0, j = this.txtHeight.Text.Length; i < j; i++)
            //{
            //    if (!char.IsDigit(this.txtHeight.Text, i))
            //    {
            //        //可以说明是第几个字符错误了
            //        MessageBox.Show("身高必须是数字", "提示", MessageBoxButtons.OK);
            //        return;
            //    }
            //}
            ////判断体重是否为数字
            //for (int i = 0, j = this.txtWeight.Text.Length; i < j; i++)
            //{
            //    if (!char.IsDigit(this.txtWeight.Text, i))
            //    {
            //        //可以说明是第几个字符错误了
            //        MessageBox.Show("体重必须是数字", "提示", MessageBoxButtons.OK);
            //        return;
            //    }
            //}
            //性别不为空时

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                this.InpatientNo = this.BabyInfo.ID;

                if (BabyInfo.FT.FixFeeInterval == 0)
                {
                    BabyInfo.FT.FixFeeInterval = 1;
                }

                #region 新登记

                //如果是新登记的婴儿,则登记婴儿表和患者住院主表信息,否则更新婴儿表和患者住院主表
                if (this.isNew)
                {
                    //登记婴儿表
                    //if (this.inpatientManager.InsertNewBabyInfo(this.BabyInfo) != 1)
                    if (radtInterMgr.InsertNewBabyInfo(BabyInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                        return;
                    }

                    //登记患者主表
                    if (this.inpatientManager.InsertPatient(this.BabyInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                        return;
                    }

                    //更新变更记录主表
                    if (this.inpatientManager.SetShiftData(this.BabyInfo.ID, FS.HISFC.Models.Base.EnumShiftType.B, "入院登记",
                        this.BabyInfo.PVisit.PatientLocation.Dept, this.BabyInfo.PVisit.PatientLocation.Dept, true) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                        return;
                    }

                    //更新变更记录
                    this.BabyInfo.PVisit.PatientLocation.Bed.Name = this.BabyInfo.PVisit.PatientLocation.Bed.ID;
                    if (this.inpatientManager.SetShiftData(this.BabyInfo.ID, FS.HISFC.Models.Base.EnumShiftType.K, "接诊",
                        this.BabyInfo.PVisit.PatientLocation.NurseCell, this.BabyInfo.PVisit.PatientLocation.Bed, true) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                        return;
                    }

                    //更新病案标记,婴儿不登记病案
                    if (this.inpatientManager.UpdateCaseSend(this.BabyInfo.ID, false) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                        return;
                    }

                    //更新母亲是否有婴儿标记
                    if (this.inpatientManager.UpdateMumBabyFlag(this.MomInfo.ID, true) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                        return;
                    }

                    //登记婴儿住院主表中的在院状态
                    FS.HISFC.Models.RADT.InStateEnumService status = new FS.HISFC.Models.RADT.InStateEnumService();
                    status.ID = "I"; //住院登记
                    if (this.inpatientManager.UpdatePatientStatus(this.BabyInfo, status) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                        return;
                    }

                    if (this.IArriveBase != null)
                    {
                        if (IArriveBase.PatientArrive(FS.HISFC.Models.RADT.EnumArriveType.Accepts, this.BabyInfo, this.BabyInfo) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(IArriveBase.ErrInfo);
                            return;
                        }
                    }


                }
                #endregion

                #region 修改
                else
                {

                    //更新患者住院主表(更新主表的同时,会更新婴儿表,患者变更表)
                    if (this.inpatientManager.UpdatePatient(this.BabyInfo) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                        return;
                    }

                    if (oldBabyInfo.PVisit.InTime != BabyInfo.PVisit.InTime)
                    {
                        //方式不太合理，先这样处理了
                        //更改登记时间时，增加修改登记时间的变更，然后更新接诊变更


                        //增加修改登记时间的变更记录
                        if (this.inpatientManager.SetShiftData(this.BabyInfo.ID, this.inpatientManager.GetDateTimeFromSysDateTime(), FS.HISFC.Models.Base.EnumShiftType.F, "更改登记时间", new FS.FrameWork.Models.NeuObject("", oldBabyInfo.PVisit.InTime.ToString(), ""), new FS.FrameWork.Models.NeuObject("", BabyInfo.PVisit.InTime.ToString(), ""), true) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                            return;
                        }


                        //获取接诊时间
                        ArrayList al = inpatientManager.QueryPatientShiftInfoNew(BabyInfo.ID);
                        if (al == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                            return;
                        }

                        foreach (FS.HISFC.Models.Invalid.CShiftData shiftData in al)
                        {
                            //找到等于接诊的K
                            if (shiftData.ShitType == "K")
                            {
                                //dtInTime = FS.FrameWork.Function.NConvert.ToDateTime(shiftData.Memo);

                                if (this.inpatientManager.UpdateShiftData(shiftData) < 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                                    return;
                                }

                                break;
                            }
                        }
                    }
                }
                #endregion

                //提交数据库
                FS.FrameWork.Management.PublicTrans.Commit();
                //如果是新登记婴儿,则在患者列表中插入一个新节点,否则更新节点
                if (this.tv != null)
                {
                    if (this.isNew)
                    {
                        this.tv.AddTreeNode(0, this.BabyInfo);
                    }
                    else
                    {
                        //查找婴儿所在的节点,并修改此节点
                        TreeNode node = this.tv.FindNode(0, this.BabyInfo);
                        if (node != null)
                        {
                            this.tv.ModifiyNode(node, this.BabyInfo);
                        }
                    }
                }

                if (MessageBox.Show("婴儿登记成功！\r\n是否打印腕带？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SOC.Local.RADT.ZhuHai.ZDWY.Register.ucBabyBraceletIBORN uc = new SOC.Local.RADT.ZhuHai.ZDWY.Register.ucBabyBraceletIBORN();
                    uc.myPatientInfo = this.BabyInfo;
                    uc.Print();
                }

                //刷新婴儿列表
                RefreshList(this.MomInfo.ID);

                ShowBabyInfo(this.BabyInfo);
                base.OnRefreshTree();
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return;
            }
        }


        private void tvPatientList1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            //取节点上的信息
            this.BabyInfo = e.Node.Tag as FS.HISFC.Models.RADT.PatientInfo;
            //将婴儿信息显示在控件中
            this.ShowBabyInfo(this.BabyInfo);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            tv = sender as FS.HISFC.Components.Common.Controls.tvPatientList;
            this.InitControl();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.MomInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            this.BabyInfo = MomInfo.Clone();
            if (this.MomInfo.ID != null || this.MomInfo.ID != "")
            {
                try
                {
                    this.txtMomInfo.Text = "[" + MomInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "]" + MomInfo.Name;
                    RefreshList(MomInfo.ID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return 0;
        }

        #region {FEA519C4-2379-40a9-8271-829A76E04EF6}

        private void cmbSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.MomInfo != null && this.MomInfo.ID != "")
            //{
            //    //this.ClearInfo();

            //    this.babyNo = this.BabyNum + 1;
            //    this.txtName.Text = this.CreatBabyName(this.MomInfo.Name, this.cmbSex.Tag.ToString(), this.babyNo);
            //}
        }
        #endregion

        //{3E2B1A30-6689-4ea7-9946-DC60E1886D4E} 婴儿登记可以通过敲号单立使用 20100919
        private void ucQueryInpatientNo_myEvent()
        {
            ucQueryInpatientNo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.InHos;
            this.MomInfo = inpatientManager.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo.InpatientNo);
            this.BabyInfo = MomInfo.Clone();
            if (this.MomInfo.ID != null || this.MomInfo.ID != "")
            {
                try
                {
                    this.txtMomInfo.Text = "[" + MomInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "]" + MomInfo.Name;
                    RefreshList(MomInfo.ID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        #endregion

        /// <summary>
        /// 还原取消登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRevokCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
