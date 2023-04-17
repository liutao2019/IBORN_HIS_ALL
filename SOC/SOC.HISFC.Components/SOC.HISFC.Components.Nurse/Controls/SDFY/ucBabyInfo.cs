using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Nurse.Controls.SDFY
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
        BabyManager babyManager = new BabyManager();
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.Models.RADT.PatientInfo MomInfo = null;
        FS.HISFC.Models.RADT.PatientInfo BabyInfo = null;
        bool isNew = false;
        private string InpatientNo = "";

        #region {FEA519C4-2379-40a9-8271-829A76E04EF6}

        private int babyNo = 0;

        #endregion

        #endregion

        #region 属性
        [Category("控制"), Description("是否可以录入住院号"), DefaultValue(false)]
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
        #endregion

        #region 函数
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());				//取性别列表
            this.cmbBlood.AddItems(FS.HISFC.Models.RADT.BloodTypeEnumService.List());	//取血型列表
            this.cmbBlood.IsListOnly = true;
            this.cmbSex.IsListOnly= true;
            this.dtBirthday.Value = this.inpatientManager.GetDateTimeFromSysDateTime();		//默认婴儿出生日期为系统时间
            this.dtOperatedate.Value = dtBirthday.Value;	//默认操作日期为系统时间
            ClearInfo();
            this.ucQueryInpatientNo.Focus();
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
            this.cmbSex.SelectedIndexChanged -= new EventHandler(this.cmbSex_SelectedIndexChanged);
            this.cmbSex.Tag = patientInfo.Sex.ID;			//性别
            this.cmbSex.SelectedIndexChanged += new EventHandler(this.cmbSex_SelectedIndexChanged);
            this.cmbBlood.Tag = patientInfo.BloodType.ID;	//血型
            this.txtHeight.Text = patientInfo.Height;		//身高
            this.txtWeight.Text = patientInfo.Weight;			//体重
            this.dtBirthday.Value = patientInfo.Birthday;		//出生日期
            this.dtOperatedate.Text = patientInfo.User03;			//登记日期
            this.InpatientNo = patientInfo.ID; //住院流水号

            if (patientInfo.PVisit.User01 == "1")   //母亲是否乙肝抗原阳性
            { this.neuCkbIsMomHBAP.Checked = true; }
            else
            { this.neuCkbIsMomHBAP.Checked = false; }
            if (patientInfo.PVisit.User02 == "1")   //婴儿是否已经注射高效免疫球蛋白
            { this.neuCkbIsImmu.Checked = true; }
            else
            { this.neuCkbIsImmu.Checked = false; }
        }

        /// <summary>
        /// 清空控件
        /// </summary>
        private void ClearInfo()
        {
            
            this.txtInpatientNo.Text = "";	//发生序号
            this.txtInpatientNo.Tag = "";	//
            this.txtName.Text = "";		//姓名
            //this.cmbSex.SelectedIndexChanged -= new EventHandler(this.cmbSex_SelectedIndexChanged);
            //this.cmbSex.Tag = "M";		//默认性别为男性
            //this.cmbSex.SelectedIndexChanged += new EventHandler(this.cmbSex_SelectedIndexChanged);
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
            this.dtBirthday.Value = this.inpatientManager.GetDateTimeFromSysDateTime();		//出生日期默认当天
            this.dtOperatedate.Value = this.inpatientManager.GetDateTimeFromSysDateTime();	//操作时间

            this.neuCkbIsMomHBAP.Checked = false; //母亲是否乙肝抗原阳性
            this.neuCkbIsImmu.Checked = false;    //婴儿是否已经注射高效免疫球蛋白

            this.BabyInfo = null;
                        
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
                this.BabyInfo.PVisit.InTime = this.inpatientManager.GetDateTimeFromSysDateTime();

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
            }
            else
            {
                //修改婴儿信息
                isNew = false;
                //如果是已登记的婴儿,提示用户输入婴儿姓名
                if (this.txtName.Text == "")
                {
                    MessageBox.Show("请输入婴儿姓名", "提示");
                    return null;
                }

                this.BabyInfo.Name = this.txtName.Text;									//婴儿姓名
            }


            this.BabyInfo.Sex.ID = this.cmbSex.Tag.ToString();				//性别
            this.BabyInfo.BloodType.ID = this.cmbBlood.Tag.ToString();	//血型
            this.BabyInfo.Height = this.txtHeight.Text;						//身高
            this.BabyInfo.Weight = this.txtWeight.Text;						//体重
            this.BabyInfo.Birthday = this.dtBirthday.Value;					//出生日期

            //判断姓名的长度
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.BabyInfo.Name, 20) == false)
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

            switch (HappenNo)
            {
                case 1:
                    BabyName = "长";
                    break;
                case 2:
                    BabyName = "次";
                    break;
                case 3:
                    BabyName = "三";
                    break;
                case 4:
                    BabyName = "四";
                    break;
                default:
                    BabyName = "";
                    break;
            }

            #region {FEA519C4-2379-40a9-8271-829A76E04EF6}

            if (SexId == FS.HISFC.Models.Base.EnumSex.M.ToString())
            {

                return MumName + "之" + BabyName + "子";
            }
            else
            {
                return MumName + "之" + BabyName + "女";
            }

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
               
                BabyNum++;

              
            }


            //显示在树型控件中
            this.tvPatientList1.BeginUpdate();
            this.tvPatientList1.SetPatient( al);
            this.tvPatientList1.EndUpdate();

            //展开节点,显示根节点
            this.tvPatientList1.ExpandAll();
            this.tvPatientList1.SelectedNode = this.tvPatientList1.Nodes[0];

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

                if (this.inpatientManager.DiscardBabyRegister(this.BabyInfo.ID) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("取消失败！" + this.inpatientManager.Err);
                }
                else
                {
                    FS.HISFC.Models.RADT.InStateEnumService status = new FS.HISFC.Models.RADT.InStateEnumService();
                    status.ID = "C";
                    p.ID = sPatientNo;
                    if (this.inpatientManager.UpdatePatientStatus(p,status) == -1)
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
                MessageBox.Show("患者信息已发生变化,请刷新当前窗口", "提示");
                return;
            }


            //控制只有母亲才能做婴儿登记
            //if (this.MomInfo.Sex.ID.ToString() != "F" || this.MomInfo.ID.Substring(4, 1) == "B")
            if (this.MomInfo.Sex.ID.ToString() != "F" || this.MomInfo.PID.PatientNO.StartsWith("B"))
            {
                MessageBox.Show("只有母亲才能做婴儿登记！", "提示");
                return;
            }

            if (cmbSex.Text == "")
            {
                MessageBox.Show("请选择性别", "提示");
                return;
            }

            //取控件中填写的婴儿信息
            //FS.HISFC.Models.RADT.PatientInfo  objBaby = new FS.HISFC.Models.RADT.PatientInfo();
            this.GetBabyInfo();

            if (FS.FrameWork.Public.String.ValidMaxLengh(this.BabyInfo.Name, 20) == false)
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

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.inpatientManager.Connection);
            //t.BeginTransaction();

            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.babyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                this.InpatientNo = this.BabyInfo.ID;
                //如果是新登记的婴儿,则登记婴儿表和患者住院主表信息,否则更新婴儿表和患者住院主表
                if (this.isNew)
                {
                    //登记婴儿表
                    if (this.inpatientManager.InsertNewBabyInfo(this.BabyInfo) != 1)
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

                    //更新婴儿登记表中的字段 母亲是否乙肝抗原阳性 和 婴儿是否已经注射高效免疫球蛋白
                    string IsMomHBAP = "0";
                    string IsImmu = "0";
                    if (this.neuCkbIsMomHBAP.Checked == true)
                    {
                        IsMomHBAP = "1";
                    }
                    if (this.neuCkbIsImmu.Checked == true)
                    {
                        IsImmu = "1";
                    }
                    if (this.babyManager.InsertBabyInfoExtend(this.BabyInfo, IsMomHBAP, IsImmu)==-1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.babyManager.Err, "提示");
                        return;
                    }
                }
                else
                {
                    //更新患者住院主表(更新主表的同时,会更新婴儿表)
                    if (this.inpatientManager.UpdatePatient(this.BabyInfo) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.inpatientManager.Err, "提示");
                        return;
                    }

                    //更新婴儿登记表中的字段 母亲是否乙肝抗原阳性 和 婴儿是否已经注射高效免疫球蛋白 
                    string IsMomHBAP = "0";
                    string IsImmu = "0";
                    if (this.neuCkbIsMomHBAP.Checked == true)
                    {
                        IsMomHBAP = "1";
                    }
                    if (this.neuCkbIsImmu.Checked == true)
                    {
                        IsImmu = "1";
                    }
                    if (this.babyManager.UpdateBabyInfo(IsMomHBAP, IsImmu, this.BabyInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败！" + this.babyManager.Err, "提示");
                        return;
                    }
                }

                //提交数据库
                FS.FrameWork.Management.PublicTrans.Commit();
                //如果是新登记婴儿,则在患者列表中插入一个新节点,否则更新节点
                if (this.tv != null)
                {//{3E2B1A30-6689-4ea7-9946-DC60E1886D4E} 婴儿登记可以通过敲号单立使用 20100919
                    if (this.isNew)
                    {
                        this.tv.AddTreeNode(0, this.BabyInfo);
                    }
                    else
                    {
                        //查找婴儿所在的节点,并修改此节点
                        TreeNode node = this.tv.FindNode(0, this.BabyInfo);
                        if (node != null) this.tv.ModifiyNode(node, this.BabyInfo);
                    }
                }
                MessageBox.Show("保存成功！");
                
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

            //根据获取到的婴儿住院号获取其余两个扩展字段
            FS.FrameWork.Models.NeuObject baby = new FS.FrameWork.Models.NeuObject();
            if (this.BabyInfo != null)
            {
                baby = this.babyManager.GetBabyInfoExtend(this.BabyInfo.ID);
                if (baby == null)
                {
                    MessageBox.Show("获取婴儿信息失败！" + this.babyManager.Err);
                }
                else
                {
                    this.BabyInfo.PVisit.User01 = baby.User01;
                    this.BabyInfo.PVisit.User02 = baby.User02; //使用user01 user02记录婴儿的两个扩展字段
                }
            }
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
            if (neuObject != null)
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
            }

            return 0;
        }

        #region {FEA519C4-2379-40a9-8271-829A76E04EF6}

        private void cmbSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.MomInfo != null && this.MomInfo.ID != "")
            {
                this.babyNo = this.BabyNum + 1;
                this.txtName.Text = this.CreatBabyName(this.MomInfo.Name, this.cmbSex.Tag.ToString(), this.babyNo);
            }
        }
        #endregion

        //{3E2B1A30-6689-4ea7-9946-DC60E1886D4E} 婴儿登记可以通过敲号单立使用 20100919
        private void ucQueryInpatientNo_myEvent()
        {
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
            this.setNextControlFocus();
        }

        /// <summary>
        /// 设置下一个控件获得焦点
        /// </summary>		
        private void setNextControlFocus()
        {
            System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void dtBirthday_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                this.setNextControlFocus();
            }
        }

        private void cmbSex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                this.setNextControlFocus();
            }
        }
        #endregion  

        private void txtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                this.setNextControlFocus();
            }
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                this.setNextControlFocus();
            }
        }

    }
}
