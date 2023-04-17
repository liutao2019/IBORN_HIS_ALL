using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
using FS.HISFC.Models.Registration;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// 退号/注销
    /// </summary>
    public partial class ucCancel : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer//{B700292D-50A6-4cdf-8B03-F556F990BB9B}
    {
        FS.HISFC.BizLogic.Fee.Account account = new FS.HISFC.BizLogic.Fee.Account();
        public ucCancel()
        {
            InitializeComponent();

            this.fpSpread1.KeyDown  += new KeyEventHandler(fpSpread1_KeyDown);
            this.txtInvoice.KeyDown += new KeyEventHandler(txtInvoice_KeyDown);
            this.txtCardNo.KeyDown  += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);

            this.Init();
        }

        #region 域

        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 控制管理类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 排班管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schMgr = new FS.HISFC.BizLogic.Registration.Schema();

        /// <summary>
        /// 费用
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 分诊管理类
        /// </summary>
        //private FS.HISFC.BizProcess.Integrate assMgr = new FS.HISFC.BizLogic.Nurse.Assign();

        /// <summary>
        /// 可退号天数
        /// </summary>
        private int PermitDays = 0;
        private ArrayList al = new ArrayList();

        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}

        private bool isQuitAccount = false;

        /// <summary>
        /// 是否打印退号票
        /// {B700292D-50A6-4cdf-8B03-F556F990BB9B}
        /// </summary>
        private bool isPrintBackBill = false;
      
        #endregion

        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        #region 属性

        /// <summary>
        /// 是否打印退号票
        /// </summary>
        [Category("控件设置"), Description("是否打印退号票(未实现)"), DefaultValue(false)]
        public bool IsPrintBackBill
        {
            set
            {
                this.isPrintBackBill = value;
            }
            get
            {
                return this.isPrintBackBill;
            }
        } 

        /// <summary>
        /// //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// </summary>
        [Category("控件设置"), Description("帐户患者是否退帐户"), DefaultValue(false)]
        public bool IsQuitAccount
        {
            get { return isQuitAccount; }
            set { isQuitAccount = value; }
        }

        //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
        private bool isSeeedCanCancelRegInfo = false;

        //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
        [Category("控件设置"), Description("已看诊挂号记录是否能退号？"), DefaultValue(false)]
        public bool IsSeeedCanCancelRegInfo
        {
            get { return isSeeedCanCancelRegInfo; }
            set { isSeeedCanCancelRegInfo = value; }
        }
       
        #endregion

        #region 医保接口

        /// <summary>
        /// 医保接口代理服务器
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// 门诊挂号－挂号费中otherfee的意义 0:床费(广医专用) 1：病历本费 2：其他费
        /// </summary>
        string otherFeeType = string.Empty;

        #endregion

        #region 方法
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            //门诊退号－允许退号天数
            string Days = this.ctlMgr.QueryControlerInfo("400006");

            if (Days == null || Days == "" || Days == "-1")
            {
                this.PermitDays = 1;
            }
            else
            {
                this.PermitDays = int.Parse(Days);
            }

            //门诊挂号－挂号费中otherfee的意义 0:床费(广医专用) 1：病历本费 2：其他费
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            Days = this.ctlMgr.QueryControlerInfo("400027");

            if (string.IsNullOrEmpty(Days))
            {
                Days = "2"; //默认其他费
            }

            this.otherFeeType = Days;

            if (this.otherFeeType == "1")
            {
                this.chbQuitFeeBookFee.Checked = true;
                this.chbQuitFeeBookFee.Visible = true;
            }
            else
            {
                this.chbQuitFeeBookFee.Visible = false;
                this.chbQuitFeeBookFee.Checked = true;
            }

            this.txtCardNo.Focus();

            return 0;
        }
        
        /// <summary>
        /// 添加患者挂号明细
        /// </summary>
        /// <param name="registers"></param>
        private void addRegister(ArrayList registers)
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            FS.HISFC.Models.Registration.Register obj;

            for (int i = registers.Count - 1; i >= 0; i--)
            {
                obj = (FS.HISFC.Models.Registration.Register)registers[i];
                this.addRegister(obj);
            }
        }
        /// <summary>
        /// 不允许使用直接收费生成的号再进行挂号
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        private int ValidCardNO(string CardNO)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParams = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            string cardRule = controlParams.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
            if (CardNO != "" && CardNO != string.Empty)
            {
                if (CardNO.Substring(0, 1) == cardRule)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("此号段为直接收费使用，不可以退号"), FS.FrameWork.Management.Language.Msg("提示"));
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// 显示挂号信息
        /// </summary>
        /// <param name="reg"></param>
        private void addRegister(FS.HISFC.Models.Registration.Register reg)
        {
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

            int cnt = this.fpSpread1_Sheet1.RowCount - 1;

            this.fpSpread1_Sheet1.SetValue(cnt, 0, reg.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 1, reg.Sex.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 2, reg.DoctorInfo.SeeDate.ToString(), false);
            this.fpSpread1_Sheet1.SetValue(cnt, 3, reg.DoctorInfo.Templet.Dept.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 4, reg.DoctorInfo.Templet.RegLevel.Name, false);
            //新增标记：是否已看诊 {7ADE3D11-1E7E-42ea-988B-0B23D9726300}
            this.fpSpread1_Sheet1.SetValue(cnt, 5, reg.IsSee, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 6, reg.DoctorInfo.Templet.Doct.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 7, reg.RegLvlFee.RegFee , false);
            this.fpSpread1_Sheet1.SetValue(cnt, 8, reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.ChkFee + reg.RegLvlFee.OthFee, false);
            this.fpSpread1_Sheet1.Rows[cnt].Tag = reg;

            if (reg.IsSee)
            {
                this.fpSpread1_Sheet1.Rows[cnt].BackColor = Color.LightCyan;
            }
            if (reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back||
                reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                this.fpSpread1_Sheet1.Rows[cnt].BackColor = Color.MistyRose;
            }
        }
       
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private int save()
        {
            #region 验证
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有可退挂号记录"), "提示");
                return -1;
            }

            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            //实体
            FS.HISFC.Models.Registration.Register reg = (FS.HISFC.Models.Registration.Register)this.fpSpread1_Sheet1.Rows[row].Tag;
            if (reg.IsSee)
            {
                MessageBox.Show("该号已经看诊，不允许退号！", "提示");
                return -1;
            }
            else if (reg.PVisit.InState.ID.ToString() == "I" || reg.PVisit.InState.ID.ToString() == "R")//{A0D66B1F-78B5-440c-A7C0-E98C56CFBCF1}
            {
                MessageBox.Show("留观患者不允许退号", "提示");
                return -1;
            }

            #endregion

            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("是否要作废该挂号信息") + "?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.regMgr.con);
            //t.BeginTransaction();

            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.schMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.assMgr.SetTrans(t.Trans);

            int rtn;
            FS.HISFC.BizLogic.Registration.EnumUpdateStatus flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Cancel;

            try
            {
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime();


                //重新获取患者实体,防止并发

                reg = this.regMgr.GetByClinic(reg.ID);
                if (this.ValidCardNO(reg.PID.CardNO) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
                //出错
                if (reg == null || reg.ID == null || reg.ID == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }

                //使用,不能作废
                //{05E82D53-9B25-44b1-902E-36F8FF4F50F3}
                //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
                //if ((reg.IsSee || reg.IsFee) && !this.isSeeedCanCancelRegInfo)
                if (reg.IsSee && !this.isSeeedCanCancelRegInfo)
                {


                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该号已经看诊,不能作废"), "提示");
                    return -1;
                }

                //是否已经退号
                if (reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号记录已经退号，不能再次退号"), "提示");
                    return -1;
                }

                //是否已经作废
                if (reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号记录已经作废，不能进行退号"), "提示");
                    return -1;
                }

                #region 判断是不是门诊帐户患者
                decimal vacancy = 0;

                int result = this.feeMgr.GetAccountVacancy(reg.PID.CardNO, ref vacancy);
                if (result < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.feeMgr.Err);
                    return -1;
                }
                #endregion
                //{5839C7FC-8162-4586-8473-B5F26C018DDE}
                //if (reg.InputOper.ID == regMgr.Operator.ID && reg.BalanceOperStat.IsCheck == false && result == 0  )
                //{
                //    #region 作废
                //    #endregion
                //}
                //else
                //{
                #region 退号
                FS.HISFC.Models.Registration.Register objReturn = reg.Clone();
                objReturn.RegLvlFee.ChkFee = -reg.RegLvlFee.ChkFee;//检查费
                objReturn.RegLvlFee.OwnDigFee = -reg.RegLvlFee.OwnDigFee;//侦察费


                objReturn.RegLvlFee.OthFee = -reg.RegLvlFee.OthFee;//其他费
                objReturn.RegLvlFee.RegFee = -reg.RegLvlFee.RegFee;//挂号费
                if (result > 0) //（有门诊帐户的患者）帐户全部退到自费里
                {
                    objReturn.OwnCost = -(reg.OwnCost + reg.PayCost);
                    objReturn.PayCost = 0;
                }
                else
                {
                    objReturn.PayCost = -reg.PayCost;
                    objReturn.OwnCost = -reg.OwnCost;
                }
                objReturn.PubCost = -reg.PubCost;
                objReturn.BalanceOperStat.IsCheck = false;//是否结算
                objReturn.BalanceOperStat.ID = "";
                objReturn.BalanceOperStat.Oper.ID = "";
                //objReturn.BeginTime = DateTime.MinValue; 
                objReturn.CheckOperStat.IsCheck = false;//是否核查
                objReturn.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Back;//退号
                objReturn.InputOper.OperTime = current;//操作时间
                objReturn.InputOper.ID = regMgr.Operator.ID;//操作人
                objReturn.CancelOper.ID = regMgr.Operator.ID;//退号人
                objReturn.CancelOper.OperTime = current;//退号时间
                //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
                //objReturn.OwnCost = -reg.OwnCost;//自费
                //objReturn.PayCost = -reg.PayCost;
                objReturn.PubCost = -reg.PubCost;
                //病历本本费处理
                //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
                if (this.otherFeeType == "1" && !this.chbQuitFeeBookFee.Checked)
                {
                    objReturn.OwnCost = objReturn.OwnCost - objReturn.RegLvlFee.OthFee;
                    objReturn.RegLvlFee.OthFee = 0;
                }

                objReturn.TranType = FS.HISFC.Models.Base.TransTypes.Negative;

                if (this.regMgr.Insert(objReturn) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }

                flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Return;
                #endregion
                //}

                reg.CancelOper.ID = regMgr.Operator.ID;
                reg.CancelOper.OperTime = current;

                //更新原来项目为作废
                rtn = this.regMgr.Update(flag, reg);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }
                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号信息状态已经变更,请重新检索数据"), "提示");
                    return -1;
                }

                //取消分诊4.5
                //if (this.assMgr.Delete(reg.ID) == -1)
                //{
                //    t.RollBack();
                //    MessageBox.Show("删除分诊信息出错!" + this.assMgr.Err, "提示");
                //    return -1;
                //}

                #region 恢复限额
                //恢复原来排班限额
                //如果原来更新限额,那么恢复限额
                if (reg.DoctorInfo.Templet.ID != null && reg.DoctorInfo.Templet.ID != "")
                {
                    //现场号、预约号、特诊号

                    bool IsReged = false, IsTeled = false, IsSped = false;

                    if (reg.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                    {
                        IsTeled = true; //预约号
                    }
                    else if (reg.RegType == FS.HISFC.Models.Base.EnumRegType.Reg)
                    {
                        if (reg.DoctorInfo.SeeDate > current)
                        {
                            IsTeled = true;//预约号
                        }
                        else
                        {
                            IsReged = true;//现场号
                        }
                    }
                    else
                    {
                        IsSped = true;//特诊号
                    }

                    rtn = this.schMgr.Reduce(reg.DoctorInfo.Templet.ID, IsReged, false, IsTeled, IsSped);
                    if (rtn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.schMgr.Err, "提示");
                        return -1;
                    }

                    if (rtn == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("已无排班信息,无法恢复限额"), "提示");
                        return -1;
                    }
                }
                #endregion


                long returnValue = 0;
                FS.HISFC.Models.Registration.Register myYBregObject = reg.Clone();
                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.medcareInterfaceProxy.SetPactCode(reg.Pact.ID);
                //初始化医保dll
                returnValue = this.medcareInterfaceProxy.Connect();
                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("初始化失败") + this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                //读卡取患者信息
                returnValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(myYBregObject);
                if (returnValue == -1)
                {

                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("读取患者信息失败") + this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                //医保信息赋值
                reg.SIMainInfo = myYBregObject.SIMainInfo;
                //退号
                reg.User01 = "-1";//退号借用
                //错误的调用了挂号方法{719DEE22-E3E3-4d3c-8711-829391BEA73C} by GengXiaoLei
                //returnValue = this.medcareInterfaceProxy.UploadRegInfoOutpatient(reg);
                reg.TranType = FS.HISFC.Models.Base.TransTypes.Negative;
                returnValue = this.medcareInterfaceProxy.CancelRegInfoOutpatient(reg);
                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者退号失败") + this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                returnValue = this.medcareInterfaceProxy.Commit();
                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者退号提交失败") + this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                this.medcareInterfaceProxy.Disconnect();


                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("退号出错!" + e.Message, "提示");
                return -1;
            }

            this.fpSpread1_Sheet1.Rows.Remove(row, 1);
            //如果已经打印发票,提示收回发票
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("退号成功"), "提示");

            //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
            if (this.IsPrintBackBill)
            {
                //打印推退号票
                this.Print(reg);

            }
            this.Clear();

            return 0;
        }
        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            this.txtCardNo.Text = "";
            this.txtInvoice.Text = "";
            this.lbTot.Text = "";
            this.lbReturn.Text = "";

            this.txtCardNo.Focus();
        }

        /// <summary>
        /// 显示应退挂号金额
        /// </summary>
        /// <param name="row"></param>
        private void SetReturnFee(int row)
        {
            if (this.fpSpread1_Sheet1.RowCount <= 0) return;

            FS.HISFC.Models.Registration.Register obj = (FS.HISFC.Models.Registration.Register)this.fpSpread1_Sheet1.Rows[row].Tag;
 
            if (obj == null) return;

            decimal ownCost = 0;
            //病例本处理
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            if (this.otherFeeType == "1" && !this.chbQuitFeeBookFee.Checked) //不退病例本
            {
                ownCost = obj.OwnCost - obj.RegLvlFee.OthFee;//减去病历本
            }

            //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
            //帐户处理
            if (!IsQuitAccount)
            {
                if (this.otherFeeType == "1" && !this.chbQuitFeeBookFee.Checked) //不退病例本
                {
                    ownCost = obj.OwnCost - obj.RegLvlFee.OthFee + obj.PayCost;//减去病历本
                }
                else
                {
                    ownCost = obj.OwnCost + obj.PayCost;
                }
            }


            this.lbTot.Text = Convert.ToString(ownCost + obj.PayCost + obj.PubCost);
            this.lbReturn.Text = ownCost.ToString();
        }

        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(FS.HISFC.Models.Registration.Register regObj)
        {

            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;

            if (regprint == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("打印票据失败,请在报表维护中维护退号票"));
            }
            else
            {

                if (regObj.IsEncrypt)
                {
                    regObj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(regObj.NormalName);
                }

                regprint.SetPrintValue(regObj);
                regprint.Print();
            }



        }

        

        #endregion

        #region 事件
        /// <summary>
        /// 处理快捷键
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.F12)
            //{
            //    this.save();

            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            //{
            //    this.FindForm().Close();

            //    return true;
            //}
            //else if (keyData == Keys.Escape)
            //{
            //    this.FindForm().Close();

            //    return true;
            //}
            //else if (keyData == Keys.F8)
            //{
            //    this.Clear();

            //    return true;
            //}

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// fp回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.save();
            }
        }

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.SetReturnFee(e.Range.Row);
        }
        
        /// <summary>
        /// 根据病历号检索患者挂号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Models.Account.AccountCard accountObj=new FS.HISFC.Models.Account.AccountCard();
                if (this.feeMgr.ValidMarkNO(this.txtCardNo.Text, ref accountObj) == -1)
                {
                    MessageBox.Show(feeMgr.Err);
                    return;
                }

                string cardNum = accountObj.Patient.PID.CardNO;

                //{4661623D-235A-4380-A7E0-476C977650CD}
                cardNum = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtCardNo.Text.Trim(), "'", "[", "]");//cardNum:就诊卡号
                if (cardNum == ""||cardNum==null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("就诊卡号不能为空"), "提示");
                    this.txtCardNo.Focus();
                    return;
                }

                cardNum = cardNum.PadLeft(10, '0');
                this.txtCardNo.Text = cardNum;
                string cardNo = "";//门诊唯一标识
                bool flag = account.GetCardNoByMarkNo(cardNum, ref cardNo);
                if (!flag)
                {
                    if (accountObj.MarkType.ID == "Card_No" && accountObj.Patient.PID.CardNO.Length > 0)
                    {
                        cardNo = accountObj.Patient.PID.CardNO;
                    }
                    else
                    {

                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("您输入的就诊卡号不存在"), "提示");
                        this.txtCardNo.Text = "";
                        this.txtCardNo.Focus();
                        return;
                    }
                }
              
                DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays).Date;
                //检索患者有效号
                this.al = this.regMgr.Query(cardNo, permitDate);
                if (this.al == null)
                {
                    MessageBox.Show("检索患者挂号信息时出错!" + this.regMgr.Err, "提示");
                    return;
                }

                if (this.al.Count == 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该患者没有可退号"), "提示");
                    this.txtCardNo.Text = "";
                    this.txtCardNo.Focus();
                    return;
                }
                else
                {
                    this.addRegister(al);

                    this.SetReturnFee(0);

                    this.fpSpread1.Focus();
                    this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                    this.fpSpread1_Sheet1.AddSelection(0, 0, 1, 0);
                }
            }
        }

        /// <summary>
        /// 根据处方号检索挂号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //{4661623D-235A-4380-A7E0-476C977650CD}
                string invoiceNo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtInvoice.Text.Trim(), "'", "[", "]");
                //string recipeNo = this.txtInvoice.Text.Trim();
                if (invoiceNo == "")
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "发票号不能为空" ), "提示" );
                    this.txtInvoice.Focus();
                    return;
                }
                invoiceNo=invoiceNo.PadLeft(12,'0');
                txtInvoice.Text = invoiceNo;

                DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays).Date;
                //检索患者有效号
                //{B6E76F4C-1D79-4fa2-ABAD-4A22DE89A6F7} by 牛鑫元

                //之前用处方号查询的，改为用发票号查询{7ADE3D11-1E7E-42ea-988B-0B23D9726300}
                //this.al = this.regMgr.QueryByRecipe( recipeNo);
                this.al = this.regMgr.QueryByRegInvoice(invoiceNo);
                if (this.al == null)
                {
                    MessageBox.Show("检索患者挂号信息时出错!" + this.regMgr.Err, "提示");
                    return;
                }
                else if (this.al.Count == 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("无该发票号对应的的挂号信息!"), "提示");
                    this.txtInvoice.Focus();
                    return;
                }

                ArrayList alRegCollection = new ArrayList();

                //移除超过限定时间的挂号信息
                foreach (FS.HISFC.Models.Registration.Register obj in this.al)
                {
                    if (obj.DoctorInfo.SeeDate.Date < permitDate.Date) continue;

                    alRegCollection.Add(obj);
                }

                if (alRegCollection.Count == 0)
                {
                    //超过期限的具体提示{7ADE3D11-1E7E-42ea-988B-0B23D9726300}
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("允许退号天数为" + this.PermitDays.ToString() + "天，此挂号票已超过期限，不允许退号！"), "提示");
                    this.txtInvoice.Focus();
                    return;
                }
                else
                {
                    this.addRegister(alRegCollection);

                    this.SetReturnFee(0);

                    this.fpSpread1.Focus();
                    this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                    this.fpSpread1_Sheet1.AddSelection(0, 0, 1, 0);
                }
            }
        }

        #endregion

        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("退号", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolbarService.AddToolButton("清屏", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            return toolbarService;
        }        

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "退号":
                    //if (txtCardNo.Text == null || txtCardNo.Text.Trim() == "")
                    //{
                    //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入病历号"), FS.FrameWork.Management.Language.Msg("提示"));
                    //    return;
                    //}
                    e.ClickedItem.Enabled = false;
                    if (this.save() == -1)
                    {
                        e.ClickedItem.Enabled = true;
                        return;
                    }
                    e.ClickedItem.Enabled = true;

                    break;
                case "清屏":

                    this.Clear();

                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private void txtCardNo_TextChanged(object sender, EventArgs e)
        {

        }

        #region IInterfaceContainer 成员
        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        public Type[] InterfaceTypes
        {

            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint);

                return type;
            }
        }

        #endregion
        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
        private void chbQuitFeeBookFee_CheckedChanged(object sender, EventArgs e)
        {
            this.SetReturnFee(this.fpSpread1_Sheet1.ActiveRowIndex);
        }

       
    }
}
