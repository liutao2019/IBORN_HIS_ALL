using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Xml;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// 预约挂号
    /// </summary>
    public partial class ucBooking : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBooking()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucBooking_Load);
            this.txtCardNo.KeyDown += new KeyEventHandler(txtCardNo_KeyDown);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            this.txtPhone.KeyDown += new KeyEventHandler(txtPhone_KeyDown);
            this.txtIdenNo.KeyDown += new KeyEventHandler(txtIdenNo_KeyDown);
            this.txtAdress.KeyDown += new KeyEventHandler(txtAdress_KeyDown);
            this.cmbDept.KeyDown += new KeyEventHandler(cmbDept_KeyDown);
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbDoct.KeyDown += new KeyEventHandler(cmbDoct_KeyDown);
            this.cmbDoct.SelectedIndexChanged += new EventHandler(cmbDoct_SelectedIndexChanged);
            this.dtBookingDate.ValueChanged += new EventHandler(dtBookingDate_ValueChanged);
            this.dtBookingDate.KeyDown += new KeyEventHandler(dtBookingDate_KeyDown);
            this.dtBegin.ValueChanged += new EventHandler(dtBegin_ValueChanged);
            this.dtBegin.KeyDown += new KeyEventHandler(dtBegin_KeyDown);
            this.dtEnd.ValueChanged += new EventHandler(dtEnd_ValueChanged);
            this.dtEnd.KeyDown += new KeyEventHandler(dtEnd_KeyDown);
            this.bnQuery.Click += new EventHandler(bnQuery_Click);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
            
            this.label13.Click += new EventHandler(label13_Click);
            this.txtOrder.KeyDown += new KeyEventHandler(txtOrder_KeyDown);
            this.txtOrder.Validating += new CancelEventHandler(txtOrder_Validating);
            this.txtAge.KeyDown+=new KeyEventHandler(txtAge_KeyDown);
            this.dtBirthday.KeyDown+=new KeyEventHandler(dtBirthday_KeyDown);
            this.cmbSex.KeyDown+=new KeyEventHandler(cmbSex_KeyDown);
            this.cmbUnit.SelectedIndexChanged+=new EventHandler(cmbUnit_SelectedIndexChanged);
            this.cmbUnit.KeyDown+=new KeyEventHandler(cmbUnit_KeyDown);
        }

        #region 变量
        /// <summary>
        /// 门诊科室列表
        /// </summary>
        private ArrayList alDept = new ArrayList();
        /// <summary>
        /// 门诊医生列表
        /// </summary>
        private ArrayList alDoct = new ArrayList();

        /// <summary>
        /// 卫生局预约平台
        /// </summary>
        private AppointmentService appointmentService = new AppointmentService();
        /// <summary>
        /// 预约订单管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Appointment appointmentMgr = new FS.HISFC.BizLogic.Registration.Appointment();
        /// <summary>
        /// 预约管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Booking bookingMgr = new FS.HISFC.BizLogic.Registration.Booking();
        /// <summary>
        /// 排班管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// 人员管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager Mgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 患者信息管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT patMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// 预约实体类
        /// </summary>
        private FS.HISFC.Models.Registration.Booking booking;
        
        /// <summary>
        /// 预约时间选择表
        /// </summary>
        private ucChooseBookingDate ucChooseDate;
        /// <summary>
        /// 是否触发SelectedIndexChanged事件
        /// </summary>
        private bool IsTriggerSelectedIndexChanged = true;
        /// <summary>
        /// 合同单位管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        #endregion

        #region 事件
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBooking_Load(object sender, EventArgs e)
        {
            this.InitDept();
            this.InitDoct();
            this.InitChooseDate();

            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            this.cmbSex.IsFlat = true;
            this.Clear();

            this.Retrieve();
            
            this.cmbDoct.Focus();

            #region by lijp 2012-09-25 {C108A02B-D1A3-4c0b-B024-67EECA401A6C}
            if (isShowBookingType)
            {
                this.cmbBookingType.Visible = true;
                this.cmbBookingType.Enabled = true;
                this.lblBookingType.Visible = true;
                this.InitBookingType();
            }
            else
            {
                this.lblBookingType.Visible = false;
                this.cmbBookingType.Visible = false;
            }
            #endregion
        }


        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// by lijp 2012-09-25 {C108A02B-D1A3-4c0b-B024-67EECA401A6C}
        /// 初始化预约类型"BookingType"
        /// </summary>
        private void InitBookingType()
        {
            try
            {
                ArrayList arrBookingType = new ArrayList();
                arrBookingType = this.conMgr.QueryConstantList("BookingType");
                this.cmbBookingType.AddItems(arrBookingType);
            }
            catch
            {
                MessageBox.Show("获取门诊预约挂号类别失败！");
                return;
            }
        }

        /// <summary>
        /// 根据病历号检索患者基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNo = this.txtCardNo.Text.Trim();

                if (cardNo == "")
                {
                    if (string.IsNullOrEmpty(cardNo))
                    {
                        if (MessageBox.Show("病历号为空，是否根据姓名检索？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            this.txtCardNo.Focus();
                            return;
                        }
                        else
                        {
                            //直接跳到姓名处,可根据输入的姓名检索患者信息
                            this.txtName.Focus();
                            return;
                        }
                    }

                    return;
                }


                if (this.ValidCardNO(cardNo) < 0)
                {
                    this.txtCardNo.Focus();
                    return;
                }

                this.ClearPatient();

                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                //用作查找卡记录时的挂号标记
                //accountCard.Memo = "1";
                int rev = this.feeMgr.ValidMarkNO(cardNo, ref accountCard);

                if (rev > 0)
                {
                    cardNo = accountCard.Patient.PID.CardNO;
                }

                cardNo = cardNo.PadLeft(10, '0');


                this.booking = this.getPatientInfo(cardNo);
                if (this.booking == null) return;

                //赋值
                this.SetPatient(this.booking);

                //if(this.booking.Name == null ||this.booking.Name.Trim() == "")
                //{
                this.txtName.Focus();
                //}
                //else
                //{
                //this.cmbDoct.Focus() ;
                //}
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }
        /// <summary>
        /// 姓名回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtName.Text.Trim() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请输入患者姓名"), "提示");
                    this.txtName.Focus();
                    return;
                }

                //没有输入病历号,需根据患者姓名检索挂号信息
                if (this.txtCardNo.Text.Trim() == "")
                {
                    string CardNo = this.GetCardNoByName(this.txtName.Text.Trim());

                    if (CardNo == "")
                    {
                        int autoGetCardNO;
                        autoGetCardNO = regMgr.AutoGetCardNO();
                        if (autoGetCardNO == -1)
                        {
                            MessageBox.Show("未能成功自动产生卡号，请手动输入！", "提示");
                        }
                        else
                        {
                            this.txtCardNo.Text = autoGetCardNO.ToString().PadLeft(10, '0');
                        }

                        this.booking = new FS.HISFC.Models.Registration.Booking();
                        this.booking.PID.CardNO = this.txtCardNo.Text;
                        this.booking.Name = this.txtName.Text;
                        this.cmbSex.Focus();
                        return;
                    }
                    else
                    {
                        this.txtCardNo.Enabled = false;
                    }
                    this.txtCardNo.Text = CardNo;

                    this.txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));

                }
                else
                {
                    this.cmbSex.Focus();
                }

            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }

        /// <summary>
        /// 电话回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //				if(this.txtPhone.Text == null || this.txtPhone.Text.Trim() == "")
                //				{
                //					MessageBox.Show("请输入患者联系电话!","提示") ;
                //					this.txtPhone.Focus() ;
                //					return ;
                //				}

                this.txtIdenNo.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }

        /// <summary>
        /// 身份证号回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIdenNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtAdress.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }

        /// <summary>
        /// 地址回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAdress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Save();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }

        /// <summary>
        /// 科室回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //设定预约时间段,默认为今天				
                if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")//没有选择科室
                {
                    DateTime today = this.bookingMgr.GetDateTimeFromSysDateTime();

                    this.SetBookingDate(today);

                    //没有选择科室,医生列表显示全部医生
                    this.cmbDoct.AddItems(this.alDoct);
                    this.cmbDoct.Tag = "";
                    //设定预约时间段,由于无排班信息,设置默认选择
                    this.SetDefaultBookingTime(today);
                }

                this.cmbDoct.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }


        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsTriggerSelectedIndexChanged == false) return;

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

            //选择科室
            DateTime today = this.bookingMgr.GetDateTimeFromSysDateTime();
            //设定预约日期,默认为当日
            this.SetBookingDate(today);
            //不显示医生
            this.cmbDoct.Tag = "";
            //显示该科室下排班医生列表
            this.GetDoctByDept(this.cmbDept.Tag.ToString());
            //设定科室预约安排时间段
            this.SetDeptZone(this.cmbDept.Tag.ToString(), today);
            //显示流水号
            this.GetOrder();
        }
        /// <summary>
        /// 医生回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //没有选择医生,认为预约到专科
                if (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == "")
                {
                    if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请指定预约医生" ), "提示" );
                        this.cmbDoct.Focus();
                        return;
                    }

                    //					if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Dept,null,
                    //						this.cmbDept.Tag.ToString(), "") == -1)
                    //					{
                    //						this.cmbDept.Focus() ;
                    //						return ;
                    //					}
                }
                //				else
                //				{
                //					if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Doct,null ,
                //						"", this.cmbDoct.Tag.ToString()) == -1)
                //					{
                //						this.cmbDoct.Focus() ;
                //						return ;
                //					}
                //				}

                this.dtBookingDate.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }


        private void cmbDoct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsTriggerSelectedIndexChanged == false) return;

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

            //选择医生
            DateTime today = this.bookingMgr.GetDateTimeFromSysDateTime();
            //设定预约日期,默认为当日
            this.SetBookingDate(today);
            //设定医生默认安排时间段
            this.SetDoctZone(this.cmbDoct.Tag.ToString(), today);
            //显示流水号
            this.GetOrder();
        }
        /// <summary>
        /// 日期变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBookingDate_ValueChanged(object sender, EventArgs e)
        {
            this.SetBookingTag(null);
            //变更星期
            this.lbWeek.Text = this.getWeek(this.dtBookingDate.Value);

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }
        /// <summary>
        /// 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBookingDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.dtBookingDate.Value.Date < this.bookingMgr.GetDateTimeFromSysDateTime().Date)
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "预约日期不能小于当前日期" ), "提示" );
                    this.dtBookingDate.Focus();
                    return;
                }

                this.dtBegin.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.bnQuery_Click(new object(), new System.EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }
        /// <summary>
        /// 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBegin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtEnd.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.bnQuery_Click(new object(), new System.EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }
        /// <summary>
        /// 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCardNo.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.bnQuery_Click(new object(), new System.EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }

        private void dtBegin_ValueChanged(object sender, EventArgs e)
        {
            this.SetBookingTag(null);
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }

        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            this.SetBookingTag(null);
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }
        /// <summary>
        /// bnQuery button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnQuery_Click(object sender, EventArgs e)
        {
            if (this.ucChooseDate.Visible)
            {
                this.ucChooseDate.Visible = false;
                this.dtBookingDate.Focus();
            }
            else
            {
                this.SetZone();
            }
        }
        /// <summary>
        /// 选择预约时间段
        /// </summary>
        /// <param name="sender"></param>
        private void ucChooseDate_SelectedItem(FS.HISFC.Models.Registration.Schema sender)
        {
            this.ucChooseDate.Visible = false;

            if (sender == null) return;

            //			if(sender.Templet.TelLmt <= sender.TelReging)
            //			{
            //				if(MessageBox.Show("预约号数已经大于设号数,是否继续?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question,
            //					MessageBoxDefaultButton.Button2) == DialogResult.No)
            //				{
            //					this.dtBookingDate.Focus() ;
            //					return ;
            //				}
            //			}
            //科室
            this.IsTriggerSelectedIndexChanged = false;
            this.cmbDept.Tag = sender.Templet.Dept.ID;
            //医生
            if (sender.Templet.Doct.ID == "None")
            {
                this.cmbDoct.Tag = "";
            }
            else
            {
                this.cmbDoct.Tag = sender.Templet.Doct.ID;
            }
            this.IsTriggerSelectedIndexChanged = true;

            //预约时间
            this.SetBookingDate(sender.SeeDate);
            //预约时间段
            this.SetBookingTime(sender);
            this.dtEnd.Focus();

        }
        /// <summary>
        /// farpoint cell doubleclick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || this.fpSpread1_Sheet1.RowCount == 0) return;

            //int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            //this.SetBookingInfo((FS.HISFC.Models.Registration.Booking)this.fpSpread1_Sheet1.Rows[row].Tag);
        }
        #endregion

        #region 方法

        /// <summary>
        /// 生成门诊全部科室列表
        /// </summary>
        /// <returns></returns>
        private int InitDept()
        {
            FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            this.alDept = deptMgr.QueryRegDepartment();
            if (alDept == null)
            {
                MessageBox.Show("获取门诊科室列表时出错!" + deptMgr.Err, "提示");
                return -1;
            }

            this.cmbDept.AddItems(alDept);

            return 0;
        }
        /// <summary>
        /// 生成门诊全部医生列表
        /// </summary>
        /// <returns></returns>
        private int InitDoct()
        {
            FS.HISFC.BizProcess.Integrate.Manager personMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            alDoct = personMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (alDoct == null)
            {
                MessageBox.Show("获取门诊医生列表时出错!" + personMgr.Err, "提示");
                return -1;
            }

            this.cmbDoct.AddItems(alDoct);

            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitChooseDate()
        {
            this.ucChooseDate = new ucChooseBookingDate();

            this.panel1.Controls.Add(ucChooseDate);

            this.ucChooseDate.BringToFront();
            this.ucChooseDate.Location = new Point(this.dtBookingDate.Left, this.dtBookingDate.Top + this.dtBookingDate.Height);
            this.ucChooseDate.Visible = false;
            this.ucChooseDate.SelectedItem += new Registration.ucChooseBookingDate.dSelectedItem(ucChooseDate_SelectedItem);
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            this.cmbDept.Tag = "";
            this.cmbDoct.Tag = ""; 
            this.cmbSex.Text = "男";
            this.cmbDoct.AddItems(this.alDoct);//显示全院医生

            DateTime current = this.bookingMgr.GetDateTimeFromSysDateTime();

            this.SetBookingDate(current);
            this.SetDefaultBookingTime(current);
            this.SetBookingTag(null);

            this.lbOrder.Text = "";

            this.ClearPatient();
        }
        /// <summary>
        /// 清除患者信息
        /// </summary>
        private void ClearPatient()
        {
            this.txtCardNo.Text = "";
            this.txtCardNo.Enabled = true;
            this.txtName.Text = "";
            this.txtIdenNo.Text = "";
            this.txtPhone.Text = "";
            this.txtAdress.Text = "";

            this.booking = null;
        }

        /// <summary>
        /// 检索患者预约信息
        /// </summary>
        private void Retrieve()
        {
            DateTime today = this.bookingMgr.GetDateTimeFromSysDateTime();
            ArrayList al = this.bookingMgr.Query(today, this.bookingMgr.Operator.ID);

            if (al == null)
            {
                MessageBox.Show("获取患者预约信息时出错!" + this.bookingMgr.Err, "提示");
                return;
            }

            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            foreach (FS.HISFC.Models.Registration.Booking obj in al)
            {
                this.AddBookingToFP(obj);
            }
        }
        /// <summary>
        /// 获取患者预约信息
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Booking getPatientInfo(string CardNo)
        {
            FS.HISFC.Models.Registration.Booking objBooking;

            //先检索患者基本信息表,看是否存在该患者信息
            FS.HISFC.BizProcess.Integrate.RADT PatientMgr = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.BizLogic.Registration.Register RegMgr = new FS.HISFC.BizLogic.Registration.Register();

            FS.HISFC.Models.RADT.PatientInfo objPatient = PatientMgr.QueryComPatientInfo(CardNo);
            if (objPatient == null || objPatient.Name == "")
            {
                //不存在基本信息,看是否存在预约信息
                
                objBooking = this.getBooking(CardNo);                
            }
            else
            {
                //存在患者基本信息,取基本信息
                objBooking = new FS.HISFC.Models.Registration.Booking();
                objBooking.PID.CardNO = CardNo;
                objBooking.Name = objPatient.Name;
                objBooking.IDCard = objPatient.IDCard;
                objBooking.Sex.ID = objPatient.Sex.ID;
                objBooking.Birthday = objPatient.Birthday;
                objBooking.PhoneHome = objPatient.PhoneHome;
                objBooking.AddressHome = objPatient.AddressHome;
                objBooking.Pact = objPatient.Pact;
                objBooking.Pact.PayKind.ID = objPatient.Pact.PayKind.ID;
                objBooking.SSN = objPatient.SSN;
                objBooking.Memo = objPatient.Memo;//证件类型

            }

            return objBooking;
        }
        /// <summary>
        /// 获取患者预约信息
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Booking getBooking(string CardNo)
        {
            FS.HISFC.Models.Registration.Booking objBooking;

            objBooking = this.bookingMgr.Get(CardNo);
            if (objBooking == null)
            {
                MessageBox.Show("获取患者预约信息时出错!" + this.bookingMgr.Err, "提示");
                return null;
            }

            if (objBooking.ID == null || objBooking.ID == "")
            {
                objBooking.PID.CardNO = CardNo;
                objBooking.Pact.PayKind.ID = "01";//自费
            }

            objBooking.IsSee = false;

            return objBooking;
        }
        /// <summary>
        /// 设置界面信息
        /// </summary>
        /// <param name="objBooking"></param>
        private void SetPatient(FS.HISFC.Models.Registration.Booking objBooking)
        {
            this.txtCardNo.Text = objBooking.PID.CardNO;
            this.txtName.Text = objBooking.Name;
            this.txtPhone.Text = objBooking.PhoneHome;
            this.txtAdress.Text = objBooking.AddressHome;
            this.txtIdenNo.Text = objBooking.IDCard;
            this.cmbSex.Text = objBooking.Sex.Name;
            this.dtBirthday.Value = objBooking.Birthday;
            this.setAge(objBooking.Birthday);
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
            if (CardNO != "" && CardNO != string.Empty&&CardNO.Length==10)
            {
                if (CardNO.Substring(0, 1) == cardRule)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("此号段为直接收费使用，请选择其它号段"), FS.FrameWork.Management.Language.Msg("提示"));
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// set booking date
        /// </summary>
        /// <param name="seeDate"></param>
        private void SetBookingDate(DateTime seeDate)
        {
            this.dtBookingDate.Value = seeDate.Date;
            this.lbWeek.Text = this.getWeek(seeDate);

        }
        /// <summary>
        /// 设置无预约排班信息时,时间段显示情况
        /// </summary>
        /// <param name="seeDate"></param>
        private void SetDefaultBookingTime(DateTime seeDate)
        {
            FS.HISFC.Models.Registration.Schema schema = new FS.HISFC.Models.Registration.Schema();
            schema.Templet.Begin = seeDate.Date;
            schema.Templet.End = seeDate.Date;

            this.SetBookingTime(schema);
        }
        /// <summary>
        /// Set booking time;
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void SetBookingTime(FS.HISFC.Models.Registration.Schema schema)
        {
            this.dtBegin.Value = schema.Templet.Begin;
            this.dtEnd.Value = schema.Templet.End;

            this.SetBookingTag(schema);
        }
        /// <summary>
        /// 保存使用的预约排班信息
        /// </summary>
        /// <param name="schema"></param>
        private void SetBookingTag(FS.HISFC.Models.Registration.Schema schema)
        {
            this.dtBookingDate.Tag = schema;

            if (schema != null)
            {
                this.lbTelLmt.Text = schema.Templet.TelQuota.ToString();
                this.lbTelReging.Text = schema.TelingQTY.ToString();
            }
            else
            {
                this.lbTelLmt.Text = "0";
                this.lbTelReging.Text = "0";
            }
        }
        /// <summary>
        /// 获取使用的预约排班信息
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetBookingTag()
        {
            if (this.dtBookingDate.Tag == null) return null;

            return (FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag;
        }

        /// <summary>
        /// 生成预约流水号
        /// </summary>
        private void GetOrder()
        {
            if (this.lbOrder.Text == "")
            {
                this.lbOrder.Text = this.bookingMgr.GetSequence("Registration.Booking.Query.3");
            }
        }
        /// <summary>
        /// 根据日期获取星期
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string getWeek(DateTime current)
        {
            string[] week = new string[] { "日", "一", "二", "三", "四", "五", "六" };

            return "星期" + week[(int)current.DayOfWeek];
        }
        /// <summary>
        /// 设定科室预约安排时间段
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="bookingDate"></param>
        /// <returns></returns>
        private int SetDeptZone(string deptID, DateTime bookingDate)
        {
            this.ucChooseDate.QueryDeptBooking(bookingDate, deptID, Registration.RegTypeNUM.Booking);

            //默认显示第一条符合条件（时间未过期、限额未满）的排班信息
            FS.HISFC.Models.Registration.Schema schema = this.ucChooseDate.GetValidBooking(RegTypeNUM.Booking);

            if (schema == null)//没有符合条件的
            {
                this.SetDefaultBookingTime(bookingDate.Date);
            }
            else
            {
                this.SetBookingTime(schema);
            }

            return 0;
        }

        /// <summary>
        /// 根据科室代码,出诊时间查询排班医生
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        private int GetDoctByDept(string deptID)
        {
            ArrayList al = this.Mgr.QueryEmployeeForScama(FS.HISFC.Models.Base.EnumEmployeeType.D, deptID);
            if (al == null)
            {
                MessageBox.Show("获取排班医生时出错!" + this.Mgr.Err, "提示");
                return -1;
            }

            this.cmbDoct.AddItems(al);

            return 0;
        }
        /// <summary>
        /// 根据人员代码获取人员信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Employee GetPerson(string ID)
        {
            if (this.alDoct == null) return null;

            foreach (FS.HISFC.Models.Base.Employee p in alDoct)
            {
                if (p.ID == ID) return p;
            }

            return null;
        }
        /// <summary>
        /// 设定医生预约安排时间段
        /// </summary>
        /// <param name="doctID"></param>
        /// <param name="bookingDate"></param>
        /// <returns></returns>
        private int SetDoctZone(string doctID, DateTime bookingDate)
        {
            this.ucChooseDate.QueryDoctBooking(bookingDate, doctID, Registration.RegTypeNUM.Booking);

            //默认显示第一条符合条件（时间未过期、限额未满）的排班信息
            FS.HISFC.Models.Registration.Schema schema = this.ucChooseDate.GetValidBooking(Registration.RegTypeNUM.Booking);

            if (schema == null)//没有符合条件的
            {
                this.SetDefaultBookingTime(bookingDate.Date);
            }
            else
            {
                this.SetBookingTime(schema);
                //没有科室,指定科室
                if (this.cmbDept.Tag.ToString() == "")
                {
                    this.IsTriggerSelectedIndexChanged = false;
                    this.cmbDept.Tag = schema.Templet.Dept.ID;
                    this.IsTriggerSelectedIndexChanged = true;
                }
            }

            return 0;
        }
        /// <summary>
        /// 设置预约时间段
        /// </summary>
        /// <returns></returns>
        private int SetZone()
        {
            string deptID = this.cmbDept.Tag.ToString();
            string doctID = this.cmbDoct.Tag.ToString();

            DateTime bookingDate = this.dtBookingDate.Value;
            DateTime current = this.bookingMgr.GetDateTimeFromSysDateTime();

            if (bookingDate.Date < current.Date)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "预约日期不能小于当前日期" ), "提示" );
                this.dtBookingDate.Focus();
                return -1;
            }

            if (doctID == null || doctID == "")//没有选择医生,预约到专科
            {
                if (deptID == null || deptID == "")//也没有预约科室,显示默认
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请指定预约专家" ), "提示" );
                    this.cmbDoct.Focus();
                    return 0;
                    //this.SetBookingTime(bookingDate,bookingDate) ;
                }
                else//预约到科室
                {
                    this.SetDeptZone(deptID, bookingDate);

                    if (this.ucChooseDate.Count > 0)
                    {
                        this.ucChooseDate.Visible = true;
                        this.ucChooseDate.Focus();
                    }
                    else if (this.ucChooseDate.Bookings.Count > 0)
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "没有符合条件的排班信息,请重新选择预约日期" ), "提示" );
                        this.dtBookingDate.Focus();
                        return -1;
                    }
                    else
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "专科没有排班" ), "提示" );
                        this.dtBookingDate.Focus();
                        return -1;
                    }
                }
            }
            else//预约到医生
            {
                this.SetDoctZone(doctID, bookingDate);

                if (this.ucChooseDate.Count > 0)
                {
                    this.ucChooseDate.Visible = true;
                    this.ucChooseDate.Focus();
                }
                else if (this.ucChooseDate.Bookings.Count > 0)
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "没有符合条件的排班信息,请重新选择预约日期" ), "提示" );
                    this.dtBookingDate.Focus();
                    return -1;
                }
                else
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "专家没有排班" ), "提示" );
                    this.dtBookingDate.Focus();
                    return -1;
                }
            }

            return 0;
        }
        /// <summary>
        /// 显示已预约登记患者信息
        /// </summary>
        /// <param name="booking"></param>
        private void SetBookingInfo(FS.HISFC.Models.Registration.Booking booking)
        {
            this.Clear();

            this.txtCardNo.Text = booking.Card.ID;
            this.txtName.Text = booking.Name;
            this.txtIdenNo.Text = booking.IDCard;
            this.txtPhone.Text = booking.PhoneHome;
            this.txtAdress.Text = booking.AddressHome;

            this.IsTriggerSelectedIndexChanged = false;
            this.cmbDept.Tag = booking.DoctorInfo.Templet.Dept.ID;
            this.cmbDoct.Tag = booking.DoctorInfo.Templet.Doct.ID;
            this.IsTriggerSelectedIndexChanged = true;

            this.dtBookingDate.Value = booking.DoctorInfo.SeeDate;
            this.dtBegin.Value = booking.DoctorInfo.Templet.Begin;
            this.dtEnd.Value = booking.DoctorInfo.Templet.End;

            this.lbOrder.Text = booking.ID;
        }

        
        /// <summary>
        /// 通过患者姓名检索患者挂号信息
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string GetCardNoByName(string Name)
        {
            frmQueryPatientByName f = new frmQueryPatientByName();

            if (f.QueryByName(Name) > 0)
            {
                DialogResult dr = f.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    string CardNo = f.SelectedCardNo;
                    f.Dispose();
                    return CardNo;
                }

                f.Dispose();
            }

            return "";
        }
        #endregion

        #region PageUp,PageDown切换焦点跳转
        /// <summary>
        /// 设置上一个控件获得焦点
        /// </summary>
        private void setPriorControlFocus()
        {
            System.Windows.Forms.SendKeys.Send("+{TAB}");
        }

        /// <summary>
        /// 设置下一个控件获得焦点
        /// </summary>
        private void setNextControlFocus()
        {
            System.Windows.Forms.SendKeys.Send("{TAB}");
        }
        #endregion

        #region toolbarClick    

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (this.Valid() == -1) return -1;

            if (this.GetValue() == -1) return -1;
            if (this.ValidCardNO(this.booking.PID.CardNO) < 0)
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.bookingMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.schemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.patMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //更新看诊数
                FS.HISFC.Models.Registration.Schema schema = this.GetBookingTag();

                if (this.schemaMgr.Increase(schema.Templet.ID, false, true, false, false) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新医生排班信息时出错!" + this.schemaMgr.Err, "提示");
                    return -1;
                }

                schema = this.schemaMgr.GetByID(schema.Templet.ID);
                if (schema == null || schema.Templet.ID == "" || schema.Templet.TelQuota < schema.TelingQTY)
                {
                    if (MessageBox.Show("排班信息限额已满,是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.dtBookingDate.Focus();
                        return -1;
                    }
                }

                //登记预约信息
                this.booking.DoctorInfo.Templet.ID = schema.Templet.ID;


                string Err = "";

                #region 更新看诊序号
                int orderNo = 0;

                //2看诊序号		
                if (this.UpdateSeeID(this.booking.DoctorInfo.Templet.Dept.ID, this.booking.DoctorInfo.Templet.Doct.ID,
                    this.booking.DoctorInfo.Templet.Noon.ID, this.booking.DoctorInfo.SeeDate, ref orderNo,
                    ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                booking.DoctorInfo.SeeNO = orderNo;

                //1全院流水号			
                if (this.Update(this.regMgr, this.booking.DoctorInfo.SeeDate, ref orderNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "提示");
                    return -1;
                }

                booking.OrderNO = orderNo;
                #endregion


                if (this.bookingMgr.Insert(this.booking) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("登记患者预约信息时出错!" + this.bookingMgr.Err, "提示");
                    return -1;
                }

                //更新患者信息
                if (this.UpdatePatientinfo(booking, patMgr, regMgr, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新患者基本信息出错!" + Err, "提示");
                    return -1;
                }

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("登记患者预约信息时出错!" + e.Message, "提示");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(string.Format("保存成功，登记病人的看诊序号为{0}({1})",booking.OrderNO,booking.DoctorInfo.SeeNO), "提示" );

            this.AddBookingToFP(this.booking);
            this.Clear();

            this.cmbDoct.Focus();

            return 0;
        }


        #region 更新看诊序号
        /// <summary>
        /// 更新全院看诊序号
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(FS.HISFC.BizLogic.Registration.Register rMgr, DateTime current, ref int seeNo,
            ref string Err)
        {
            //更新看诊序号
            //全院是全天大排序，所以午别不生效，默认 1
            if (rMgr.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            //获取全院看诊序号
            if (rMgr.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 更新医生或科室的看诊序号
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <param name="noonID"></param>
        /// <param name="regDate"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSeeID(string deptID, string doctID, string noonID, DateTime regDate,
            ref int seeNo, ref string Err)
        {
            string Type = "", Subject = "";

            #region ""

            if (doctID != null && doctID != "")
            {
                Type = "1";//医生
                Subject = doctID;
            }
            else
            {
                Type = "2";//科室
                Subject = deptID;
            }

            #endregion

            //更新看诊序号
            if (this.regMgr.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            //获取看诊序号		
            if (this.regMgr.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            return 0;
        }

        #endregion

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private int Valid()
        {
            DateTime begin = this.dtBegin.Value;
            DateTime end = this.dtEnd.Value;
            if (this.booking == null || this.booking.PID.CardNO == "")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请输入预约患者信息" ), "提示" );
                this.txtCardNo.Focus();
                return -1;
            }
            //			if(this.txtName.Text.Trim() == "")
            //			{
            //				MessageBox.Show("请输入患者姓名!","提示") ;
            //				this.txtName.Focus() ;
            //				return -1;
            //			}
            //			if(this.txtPhone.Text.Trim() == "")
            //			{
            //				MessageBox.Show("请输入患者电话!","提示") ;
            //				this.txtPhone.Focus() ;
            //				return -1 ;
            //			}
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text.Trim(), 16) == false)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "患者名称不能为空，且最多可录入20个汉字" ), "提示" );
                this.txtName.Focus();
                return -1;
            }
            if (this.txtName.Text.Trim() == null || this.txtName.Text.Trim() == "")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "患者名称不能为空，且最多可录入20个汉字" ), "提示" );
                this.txtName.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAdress.Text.Trim(), 60) == false)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "联系人地址最多可录入30个汉字" ), "提示" );
                this.txtAdress.Focus();
                return -1;
            }
            if ((this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "") &&
                (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == ""))
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请选择预约专家" ), "提示" );
                this.cmbDoct.Focus();
                return -1;
            }

            #region add by lijp 2012-08-24 {5195341F-12C4-41cb-B2E0-EB35365990FC}
            if (this.intCanBookingDays != -1)
            {
                if (DateTime.Compare(this.dtBookingDate.Value, this.regMgr.GetDateTimeFromSysDateTime().Date.AddDays(intCanBookingDays)) > 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("只能提前" + this.IntCanBookingDays + "天预约！"));
                    return -1;
                }
            }

            if (this.isIdentityCardMustFill)
            {
                if (isIdentityCardMustFill && string.IsNullOrEmpty(this.txtIdenNo.Text))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("身份证为必填信息！"), "提示");
                    this.txtIdenNo.Focus();
                    return -1;
                }
                //校验身份证号
                if (!string.IsNullOrEmpty(this.txtIdenNo.Text))
                {

                    int reurnValue = FS.HISFC.Components.Registration.Function.ProcessIDENNO(this.txtIdenNo.Text, FS.HISFC.Components.Registration.Function.EnumCheckIDNOType.Saveing);

                    if (reurnValue < 0)
                    {
                        this.txtIdenNo.Focus();
                        return -1;
                    }
                }
            }
            #endregion

            if (begin.TimeOfDay > end.TimeOfDay)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "预约起始时间不能大于截至时间" ) );
                return -1;
            }

            #region ""
            /*FS.HISFC.Models.Registration.Schema schema = this.GetBookingTag() ;
			if(schema == null || schema.Templet.ID == null ||schema.Templet.ID == "")
			{
				MessageBox.Show("请选择预约时间!","提示") ;
				this.dtBookingDate.Focus() ;
				return -1 ;
			}			
			
			if(this.dtBegin.Value.TimeOfDay >this.dtEnd.Value.TimeOfDay)
			{
				MessageBox.Show("预约起始时间不能大于结束时间!","提示") ;
				this.dtBegin.Focus() ;
				return -1;
			}			

			DateTime current = this.bookingMgr.GetDateTimeFromSysDateTime() ;
			DateTime bookingDate = DateTime.Parse(this.dtBookingDate.Value.Date.ToString() +" "+this.dtEnd.Value.Hour.ToString() +
				":" + this.dtEnd.Value.Minute.ToString() + ":00") ;
			if(bookingDate <current) 
			{
				MessageBox.Show("预约时间小于当前时间!","提示") ;
				this.dtBegin.Focus() ;
				return -1 ;
			}*/
            #endregion

            return 0;
        }
        /// <summary>
        /// 实体赋值
        /// </summary>
        /// <returns></returns>
        private int GetValue()
        {
            this.booking.ID = this.lbOrder.Text;
            
            //{A47CE41F-3AC7-4289-AFB2-01DC5481F917}
            this.booking.Name = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtName.Text.Trim(), "'", "[", "]");
            //{A47CE41F-3AC7-4289-AFB2-01DC5481F917}
            this.booking.IDCard = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtIdenNo.Text.Trim(),"'","[", "]");
            //{A47CE41F-3AC7-4289-AFB2-01DC5481F917}
            this.booking.PhoneHome = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtPhone.Text.Trim(), "'", "[", "]");
            //{A47CE41F-3AC7-4289-AFB2-01DC5481F917}
            this.booking.AddressHome = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtAdress.Text.Trim(),"'", "[", "]");

            FS.HISFC.Models.Registration.Schema schema = this.GetBookingTag();
            //造成这种情况是1、没有符合条件的排班信息,2、变动了预约日期、时间,所以重新检索一遍进行确认
            if (schema == null || schema.Templet.ID == null || schema.Templet.ID == "")
            {
                schema = this.GetValidSchema();
                if (schema == null)
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "预约时间指定错误,没有符合条件的排班信息" ), "提示" );
                    this.dtBookingDate.Focus();
                    return -1;
                }

                this.SetBookingTag(schema);
            }           

            this.booking.DoctorInfo = schema.Clone();
           
            if (this.booking.DoctorInfo.Templet.Doct.ID == "None") this.booking.DoctorInfo.Templet.Doct.ID = "";
            this.booking.DoctorInfo.SeeDate = DateTime.Parse(schema.SeeDate.ToString("yyyy-MM-dd") + " " +
                                                schema.Templet.Begin.ToString("HH:mm:ss"));
            /*
            this.booking.DoctorInfo.Templet.Begin = schema.Templet.Begin;
            this.booking.DoctorInfo.Templet.End = schema.Templet.End;
            this.booking.DoctorInfo.Templet.Dept.ID = schema.Templet.Dept.ID;
            this.booking.DoctorInfo.Templet.Dept.Name = schema.Templet.Dept.Name;
            this.booking.DoctorInfo.Templet.Doct.ID = schema.Templet.Doct.ID;
            if (this.booking.DoctorInfo.Templet.Doct.ID == "None") this.booking.DoctorInfo.Templet.Doct.ID = "";
            this.booking.DoctorInfo.Templet.Doct.Name = schema.Templet.Doct.Name;
            this.booking.Noon = schema.Templet.NoonID;
            this.booking.IsAppend = schema.Templet.IsAppend;*/
            this.booking.DoctorInfo.Templet.Begin = this.dtBegin.Value;
            this.booking.DoctorInfo.Templet.End = dtEnd.Value;
            this.booking.Oper.ID = this.bookingMgr.Operator.ID;
            this.booking.Oper.OperTime = this.bookingMgr.GetDateTimeFromSysDateTime();

            //{0B4C5A74-98EB-4adc-9E52-47295201EB97}
            this.booking.DoctorInfo.Templet.RegLevel.ID = schema.Templet.RegLevel.ID;
            this.booking.BookingTypeId = this.cmbBookingType.Tag == null? string.Empty:this.cmbBookingType.Tag.ToString();
            this.booking.BookingTypeName = this.cmbBookingType.Text.Trim();
            if (this.cmbSex.Tag != null)
            {
                this.booking.Sex.ID = this.cmbSex.Tag.ToString();
                this.booking.Sex.Name = this.cmbSex.Text;
            }
            this.booking.Birthday = this.dtBirthday.Value;

            return 0;
        }
        /// <summary>
        /// 重新获取有效的排班信息
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetValidSchema()
        {
            string deptID = this.cmbDept.Tag.ToString();
            string doctID = this.cmbDoct.Tag.ToString();

            DateTime bookingDate = this.dtBookingDate.Value.Date;

            ArrayList al;

            if (doctID == "")//预约专科
            {
                al = this.schemaMgr.QueryByDept(bookingDate, deptID);
                if (al == null || al.Count == 0) return null;

            }
            else//预约专家
            {
                al = this.schemaMgr.QueryByDoct(bookingDate, doctID);
                if (al == null || al.Count == 0) return null;
            }

            return this.GetValidSchema(al);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Schemas"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetValidSchema(ArrayList Schemas)
        {

            DateTime current = this.schemaMgr.GetDateTimeFromSysDateTime();
            DateTime begin = this.dtBegin.Value;
            DateTime end = this.dtEnd.Value;

            foreach (FS.HISFC.Models.Registration.Schema obj in Schemas)
            {
                if (obj.SeeDate < current.Date) continue;//小于当前日期
                //if(obj.Templet.TelLmt == 0)continue ;//没有设定预约限额

                //by niuxy  在排班范围内任意预约时间
                //if (obj.Templet.Begin.TimeOfDay != begin.TimeOfDay) continue;//开始时间不等
                //if (obj.Templet.End.TimeOfDay != end.TimeOfDay) continue;//结束时间不等
                if ((obj.Templet.Begin.TimeOfDay > begin.TimeOfDay) || (obj.Templet.End.TimeOfDay < end.TimeOfDay)) continue;//开始时间不等


                //if(obj.Templet.TelLmt <= obj.TelReging) continue;//超出限额
                //
                //只有日期相同,才判断时间是否超时,否则就是预约到以后日期,时间不用判断
                //
                if (current.Date == this.dtBookingDate.Value.Date)
                {
                    if (obj.Templet.End.TimeOfDay < current.TimeOfDay) continue;//时间小于当前时间
                }

                return obj;
            }
            return null;
        }
        /// <summary>
        /// 添加预约信息到farpoint
        /// </summary>
        /// <param name="booking"></param>
        private void AddBookingToFP(FS.HISFC.Models.Registration.Booking booking)
        {
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
            int row = this.fpSpread1_Sheet1.RowCount - 1;

            this.fpSpread1_Sheet1.SetValue(row, 0, booking.ID, false);
            this.fpSpread1_Sheet1.SetValue(row, 1, booking.PID.CardNO, false);
            this.fpSpread1_Sheet1.SetValue(row, 2, booking.Name, false);
            this.fpSpread1_Sheet1.SetValue(row, 3, booking.DoctorInfo.Templet.Dept.Name, false);
            this.fpSpread1_Sheet1.SetValue(row, 4, booking.DoctorInfo.Templet.Doct.Name, false);
            this.fpSpread1_Sheet1.SetValue(row, 5, booking.DoctorInfo.SeeDate.ToString("yyyy-MM-dd") + "[" + booking.DoctorInfo.Templet.Begin.ToString("HH:mm") + "~" + booking.DoctorInfo.Templet.End.ToString("HH:mm") + "]", false);
            this.fpSpread1_Sheet1.Rows[row].Tag = booking;
        }
        /// <summary>
        /// 更新患者基本信息
        /// </summary>
        /// <param name="booking"></param>
        /// <param name="patMgr"></param>
        /// <param name="registerMgr"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdatePatientinfo(FS.HISFC.Models.Registration.Booking booking,
            FS.HISFC.BizProcess.Integrate.RADT patMgr, FS.HISFC.BizLogic.Registration.Register registerMgr,
            ref string Err)
        {
            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();


            regInfo.PID.CardNO = booking.PID.CardNO;
            regInfo.Name = booking.Name;
            regInfo.Sex.ID = booking.Sex.ID;
            regInfo.Birthday = booking.Birthday;
            regInfo.Pact = booking.Pact;
            regInfo.Pact.PayKind.ID = booking.Pact.PayKind.ID;
            regInfo.SSN = booking.SSN;
            regInfo.PhoneHome = booking.PhoneHome;
            regInfo.AddressHome = booking.AddressHome;
            regInfo.IDCard = booking.IDCard;
            regInfo.CardType.ID = booking.Memo;

            int rtn = registerMgr.Update(FS.HISFC.BizLogic.Registration.EnumUpdateStatus.PatientInfo,
                                            regInfo);

            if (rtn == -1)
            {
                Err = registerMgr.Err;
                return -1;
            }

            if (rtn == 0)//没有更新到患者信息，插入
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

                p.PID.CardNO = regInfo.PID.CardNO;
                p.Name = regInfo.Name;
                p.Sex.ID = regInfo.Sex.ID;
                p.Birthday = regInfo.Birthday;
                p.Pact = regInfo.Pact;
                p.Pact.PayKind.ID = regInfo.Pact.PayKind.ID;
                p.SSN = regInfo.SSN;
                p.PhoneHome = regInfo.PhoneHome;
                p.AddressHome = regInfo.AddressHome;
                p.IDCard = regInfo.IDCard;
                p.Memo = regInfo.CardType.ID;

                if (patMgr.RegisterComPatient(p) == -1)
                {
                    Err = patMgr.Err;
                    return -1;
                }
            }

            return 0;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        private int Delete()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            if (row < 0 || this.fpSpread1_Sheet1.RowCount == 0) return 0;

            if (this.Delete((FS.HISFC.Models.Registration.Booking)this.fpSpread1_Sheet1.Rows[row].Tag) == -1)
            {
                return -1;
            }

            this.fpSpread1_Sheet1.Rows.Remove(row, 1);

            return 0;
        }
        private int CancelAppointment()
        {
            Forms.frmInputSerialNo newfrm = new Forms.frmInputSerialNo();
            if (newfrm.ShowDialog() == DialogResult.OK)
            {
                Models.Registration.Booking booking = bookingMgr.GetByID(newfrm.SerialNo);
                if (booking.IsSee == true)
                {
                    MessageBox.Show("该预约已看诊,不允许删除");
                    return -1;
                }
                if (booking == null||string.IsNullOrEmpty(booking.ID))
                {
                    MessageBox.Show("没找到该预约信息");
                    return -1;
                }
                else return Delete(booking);
            }
            return -1;
        }
        /// <summary>
        /// 删除预约信息
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private int Delete(FS.HISFC.Models.Registration.Booking b)
        {
            if (MessageBox.Show("是否删除【" + b.Name + "】的预约【" + b.DoctorInfo.Templet.Doct.Name +"】【"+b.DoctorInfo.Templet.Begin +"】信息?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.No) return -1;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.bookingMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.schemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int rtn = this.bookingMgr.Delete(b.ID);

                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除预约信息时出错!" + this.bookingMgr.Err, "提示");
                    return -1;
                }

                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "该预约信息已经挂号,不能删除" ), "提示" );
                    return -1;
                }

                ///恢复预约看诊限额
                ///
                rtn = this.schemaMgr.Reduce(b.DoctorInfo.Templet.ID, false, true, false, false);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新预约限额出错!" + this.schemaMgr.Err, "提示");
                    return -1;
                }
                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "已无排班信息,无法恢复限额" ), "提示" );
                    return -1;
                }

                FS.HISFC.Models.Registration.AppointmentOrder appOrder = appointmentMgr.QueryAppointmentOrderBySerialNO(b.ID);
                //如果不为空的话,那么说明这个号是卫生局预约过来的号
                if (appOrder != null)
                {
                    rtn = appointmentMgr.UpdateAppointmentOrder(appOrder.OrderID, "2", FS.FrameWork.Management.Connection.Operator.ID);
                    if (rtn > 0)
                    {
                        AppointmentService appointmentService = new AppointmentService();
                        //多线程异步调用WebService通知卫生局 add by yerl
                        appointmentService.Invoke(AppointmentService.funs.cancelOrderbyHis,
                            new AppointmentService.InvokeCompletedEventHandler(appointmentService_InvokeCompleted),
                            appOrder.OrderID, 
                            "门诊取消预约",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            AppointmentService.funs.cancelOrderbyHis.ToString());
                    }
                }
                
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "提示");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show( FS.FrameWork.Management.Language.Msg( "删除成功" ), "提示" );

            return 0;
        }

        /// <summary>
        /// 异步获取Web调用结果
        /// </summary>
        /// <param name="result"></param>
        private void appointmentService_InvokeCompleted(AppointmentService.InvokeResult result)
        {
            if (result.ResultCode == "0")
                MessageBox.Show("通知卫生局已取消成功");
            else
                MessageBox.Show("通知卫生局已取消失败,原因: " + result.ResultDesc);
        }

        #endregion
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                bool IsSelected = false;

                if (this.ucChooseDate.Visible)
                {
                    IsSelected = true;

                    this.ucChooseDate.Visible = false;
                    this.dtBookingDate.Focus();
                }

                if (!IsSelected)
                {
                    this.FindForm().Close();
                }

                return true;
            }// 自定义快捷键屏蔽 {6A58ADC6-04D1-48a5-AF0C-82B730D55094}
            //else if (keyData == Keys.F8)
            //{
            //    this.Clear();
            //    this.cmbDoct.Focus();

            //    return true;
            //}            
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
                return true;
            }//自定义快捷键屏蔽 {6A58ADC6-04D1-48a5-AF0C-82B730D55094}
            //else if (keyData == Keys.F12)
            //{
            //    this.Save();

            //    return true;
            //}
            //else if (keyData == Keys.Subtract || keyData == Keys.OemMinus)
            //{
            //    this.Delete();
            //    this.cmbDoct.Focus();

            //    return true;
            //}
            //else if (keyData == Keys.F1)
            //{
            //    this.Switch();
            //    return true;
            //}
            //else if (keyData == Keys.F9)
            //{
            //    this.ChangeCard();
            //}

            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// 切换到挂号窗口
        /// </summary>
        private void Switch()
        {
            //Form[] forms = this.ParentForm.MdiChildren;

            //foreach (Form f in forms)
            //{
            //    if (f.GetType().FullName == "Registration.frmRegister")
            //    {
            //        f.Show();
            //        f.BringToFront();
            //        return;
            //    }
            //}

            //frmRegister form = new frmRegister(var);

            //form.MdiParent = this.ParentForm;
            //form.Show();
        }

        /// <summary>
        /// 换卡
        /// </summary>
        private void ChangeCard()
        {
            //Local.Clinic.Form.frmCreateCard f = new Local.Clinic.Form.frmCreateCard(var);
            //f.ShowDialog();
            //f.Dispose();
        }


        /// <summary>
        /// 获取出生日期
        /// </summary>
        private void getBirthday()
        {
            string age = this.txtAge.Text.Trim();
            int i = 0;

            if (age == "") age = "0";

            try
            {
                i = int.Parse(age);
            }
            catch (Exception e)
            {
                string error = e.Message;
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入年龄不正确,请重新输入"), "提示");
                this.txtAge.Focus();
                return;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(age) > 110)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("输入年龄过大,请重新输入"), "提示");
                this.txtAge.Focus();
                return;
            }
            ///
            ///

            DateTime birthday = DateTime.MinValue;

            this.getBirthday(i, this.cmbUnit.Text, ref birthday);

            if (birthday < this.dtBirthday.MinDate)
            {
                MessageBox.Show("年龄不能过大!", "提示");
                this.txtAge.Focus();
                return;
            }

            //this.dtBirthday.Value = birthday ;

            if (this.cmbUnit.Text == "岁")
            {

                //数据库中存的是出生日期,如果年龄单位是岁,并且算出的出生日期和数据库中出生日期年份相同
                //就不进行重新赋值,因为算出的出生日期生日为当天,所以以数据库中为准

                if (this.dtBirthday.Value.Year != birthday.Year)
                {
                    this.dtBirthday.Value = birthday;
                }
            }
            else
            {
                this.dtBirthday.Value = birthday;
            }
        }


        /// <summary>
        /// 根据年龄得到出生日期
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <param name="birthday"></param>
        private void getBirthday(int age, string ageUnit, ref DateTime birthday)
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            if (ageUnit == "岁")
            {
                birthday = current.AddYears(-age);
            }
            else if (ageUnit == "月")
            {
                birthday = current.AddMonths(-age);
            }
            else if (ageUnit == "天")
            {
                birthday = current.AddDays(-age);
            }
        }


        /// <summary>
        /// Set Age
        /// </summary>
        /// <param name="birthday"></param>
        private void setAge(DateTime birthday)
        {
            this.txtAge.Text = "";

            if (birthday == DateTime.MinValue)
            {
                return;
            }

            DateTime current;
            int year = 0, month = 0, day = 0;

            current = this.regMgr.GetDateTimeFromSysDateTime();
            this.regMgr.GetAge(birthday, current, ref year, ref month, ref day);
            //year = current.Year - birthday.Year;
            //month = current.Month - birthday.Month;
            //day = current.Day - birthday.Day;

            if (year > 1)
            {
                this.txtAge.Text = year.ToString();
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 0;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }
            else if (year == 1)
            {
                if (month >= 0)//一岁
                {
                    this.txtAge.Text = year.ToString();
                    this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                    this.cmbUnit.SelectedIndex = 0;
                    this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
                }
                else
                {
                    this.txtAge.Text = Convert.ToString(12 + month);
                    this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                    this.cmbUnit.SelectedIndex = 1;
                    this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
                }
            }
            else if (month > 0)
            {
                this.txtAge.Text = month.ToString();
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 1;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }
            else if (day > 0)
            {
                this.txtAge.Text = day.ToString();
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 2;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }

        }

        private void txtAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getBirthday();

                this.cmbUnit.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void dtBirthday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime().Date;

                if (this.dtBirthday.Value.Date > current)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("出生日期不能大于当前时间"), "提示");
                    this.dtBirthday.Focus();
                    return;
                }

                //计算年龄
                if (this.dtBirthday.Value.Date != current)
                {
                    this.setAge(this.dtBirthday.Value);
                }

            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void cmbSex_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cmbSex.Text.Trim().Length > 0 && this.cmbSex.Text.Trim().Length < 2)
                {
                    try
                    {
                        int intsex = int.Parse(this.cmbSex.Text);
                        switch (intsex)
                        {
                            case 1:
                                this.cmbSex.Tag = "M";
                                break;
                            case 2:
                                this.cmbSex.Tag = "F";
                                break;
                            default:
                                break;
                        }
                    }
                    catch
                    {

                    }

                }
                if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择患者性别"), "提示");
                    this.cmbSex.Focus();
                    return;
                }

                this.dtBirthday.Focus();

            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getBirthday();
        }
        private void cmbUnit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPhone.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //反相跳转
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        #region 删除预约信息

        bool isLeave = true;
        /// <summary>
        /// 根据流水号检索预约信息,然后删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label13_Click(object sender, EventArgs e)
        {
            this.isLeave = true;
            this.txtOrder.Visible = true;
            this.txtOrder.Focus();
        }


        /// <summary>
        /// 删除预约信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                this.isLeave = false;
                string ID = this.txtOrder.Text.Trim();

                if (ID == "")
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请指定流水号" ), "提示" );
                    this.txtOrder.Focus();
                    this.isLeave = true;
                    return;
                }
                //获取预约实体

                FS.HISFC.Models.Registration.Booking b = this.bookingMgr.GetByID(ID);
                if (b == null || b.ID == "")
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "没有对应该流水号的预约信息" ), "提示" );
                    this.txtOrder.Focus();
                    this.isLeave = true;
                    return;
                }

                this.SetBookingInfo(b);

                //删除预约信息
                if (this.Delete(b) == -1)
                {
                    this.txtOrder.Focus();
                    this.isLeave = true;
                    return;
                }

                //this.txtOrder.Visible = false ;				
                this.isLeave = true;
                this.Retrieve();
                this.Clear();
                this.cmbDoct.Focus();

            }
        }


        private void txtOrder_Validating(object sender, CancelEventArgs e)
        {
            if (this.isLeave)
            {
                this.txtOrder.Visible = false;
            }
        }
        #endregion

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //屏蔽{6A58ADC6-04D1-48a5-AF0C-82B730D55094}
            //this.toolBarService.AddToolButton("保存", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A保存, true, false, null);
            this.toolBarService.AddToolButton("删除", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            this.toolBarService.AddToolButton("清屏", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("刷卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            toolBarService.AddToolButton("取消预约", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            return this.toolBarService;
        }

        //新加{6A58ADC6-04D1-48a5-AF0C-82B730D55094}
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "取消预约":
                    CancelAppointment();
                    break;

                //屏蔽 {6A58ADC6-04D1-48a5-AF0C-82B730D55094}
                //case "保存":
                //    this.Save();

                //    break;
                case "删除":
                    this.Delete();
                    this.cmbDoct.Focus();

                    break;
                case "清屏":
                    this.Clear();
                    this.cmbDoct.Focus();

                    break;
                case "刷卡":
                    {
                        string cardNo = "";
                        string error = "";
                        if (Function.OperCard(ref cardNo, ref error) == -1)
                        {
                            CommonController.Instance.MessageBox(error, MessageBoxIcon.Error);
                            return;
                        }

                        txtCardNo.Text = cardNo;
                        txtCardNo.Focus();
                        this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));

                        break;
                    }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 预约日期限制 add by lijp 2012-08-24 {5195341F-12C4-41cb-B2E0-EB35365990FC}
        /// </summary>
        private int intCanBookingDays;

        /// <summary>
        /// 预约日期限制 add by lijp 2012-08-24 {5195341F-12C4-41cb-B2E0-EB35365990FC}
        /// </summary>
        [Category("控件设置"), Description("可预约挂号天数,不写则不限制"), DefaultValue("")]
        public string IntCanBookingDays
        {
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        intCanBookingDays = -1;
                    }
                    else
                    {
                        intCanBookingDays = Convert.ToInt32(value);
                    }
                    //this.dtBookingDate.MinDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-1);
                    //this.dtBookingDate.MaxDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(intCanBookingDays);
                }
                catch
                {
                    intCanBookingDays = -1;
                    // Do nothing.
                }
            }
            get
            {
                return intCanBookingDays.ToString();
            }
        }

        /// <summary>
        /// 是否身份证号必填 add by lijp 2012-08-23 {5195341F-12C4-41cb-B2E0-EB35365990FC}
        /// </summary>
        private bool isIdentityCardMustFill = false;

        /// <summary>
        /// 是否身份证号必填 add by lijp 2012-08-23 {5195341F-12C4-41cb-B2E0-EB35365990FC}
        /// </summary>
        [Category("控件设置"), Description("是否身份证号必填"), DefaultValue(false)]
        public bool IsIdentityCardMustFill
        {
            set
            {
                isIdentityCardMustFill = value;

                if (isIdentityCardMustFill)
                {
                    this.label7.ForeColor = Color.Blue;
                }
                else
                {
                    this.label7.ForeColor = Color.Black;
                }
            }
            get
            {
                return isIdentityCardMustFill;
            }
        }

        #region by lijp 2012-09-25 {C108A02B-D1A3-4c0b-B024-67EECA401A6C}
        /// <summary>
        /// 是否显示预约类型
        /// </summary>
        private bool isShowBookingType = false;

        /// <summary>
        /// 是否显示预约类型
        /// </summary>
        [Category("控件设置"), Description("是否显示预约类型"), DefaultValue(false)]
        public bool IsShowBookingType
        {
            get { return isShowBookingType; }
            set { isShowBookingType = value; }
        }
        #endregion
    }
}
