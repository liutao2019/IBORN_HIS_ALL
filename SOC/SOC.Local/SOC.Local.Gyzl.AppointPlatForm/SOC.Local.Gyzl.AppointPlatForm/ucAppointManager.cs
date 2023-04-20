using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using FS.HISFC.Models.Account;

namespace SOC.Local.Gyzl.AppointPlatForm
{
    public partial class ucAppointManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 2处方号对操作员连续、1由操作员自己录入处方号
        /// </summary>
        private int GetRecipeType = 1;

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 预约平台订单管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Appointment appointmentMgr = new FS.HISFC.BizLogic.Registration.Appointment();

        /// <summary>
        /// 预约管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Booking bookingMgr = new FS.HISFC.BizLogic.Registration.Booking();

        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 排班管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schMgr = new FS.HISFC.BizLogic.Registration.Schema();

        /// <summary>
        /// 帐户管理
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accMgr = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 合同单位管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 参数控制类
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 系统参数控制
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParma = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        public ucAppointManager()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucAppointManager_Load);
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucAppointManager_Load(object sender, EventArgs e)
        {
            //获取处方号类型（1物理票号,2电脑票号－－挂号收据号,3电脑票号－－门诊收据号）
            string rtn = this.ctlMgr.QueryControlerInfo("400019");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";
            this.GetRecipeType = int.Parse(rtn);

            DateTime dtNow = this.regMgr.GetDateTimeFromSysDateTime();
            this.dtStartDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.ToString("yyyy-MM-dd 00:00:00"));
            this.dtEndDate.Value = FS.FrameWork.Function.NConvert.ToDateTime(dtNow.ToString("yyyy-MM-dd 23:59:59"));
            this.neuFpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
            ArrayList al = this.conMgr.GetConstantList("AppointmentCardType");
            if (al == null)
            {
                MessageBox.Show("获取证件类型时出错!" + this.conMgr.Err, "提示");
                return;
            }
            this.cboCardType.AddItems(al);

            this.InitInvoiceInfo();
            this.SetFpFormat();
        }

        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBar = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBar.AddToolButton("建卡", "建卡", (int)FS.FrameWork.WinForms.Classes.EnumImageList.R人员, true, false, null);
            toolBar.AddToolButton("取消预约", "取消预约", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBar.AddToolButton("取号", "取号", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A安排, true, false, null);
            toolBar.AddToolButton("退费", "退费", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            return toolBar;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.SetAppointmentInfo();
            return base.OnQuery(sender, neuObject);
        }

        private void SetAppointmentInfo()
        {
            string cardNo = this.txtCardNO.Text.Trim();
            ArrayList appointmentOrderList;
            if (this.cboCardType.SelectedItem != null && cardNo != string.Empty)
            {
                appointmentOrderList = appointmentMgr.QueryAppointmentOrder(this.dtStartDate.Value, this.dtEndDate.Value,
                    cardNo, this.cboCardType.SelectedItem.ID);
                if (appointmentOrderList.Count == 0)
                {
                    if (this.cboCardType.SelectedItem.Name.IndexOf("健康") >= 0)
                    {
                        RHINCardServiceImplService.RHINCardServiceImplService cardService = new RHINCardServiceImplService.RHINCardServiceImplService();

                        RHINCardServiceImplService.QueryPersonRequestType queryPersonRequest = new RHINCardServiceImplService.QueryPersonRequestType();
                        RHINCardServiceImplService.CardType cardType = new RHINCardServiceImplService.CardType();
                        cardType.type = "0";
                        cardType.number = cardNo;

                        queryPersonRequest.authObject = this.getAuthObject();
                        queryPersonRequest.card = cardType;

                        string icCardNO = string.Empty;
                        RHINCardServiceImplService.QuestPersonResponseType questPersonResponse = cardService.queryPerson(queryPersonRequest);
                        if (questPersonResponse.status.Equals("0"))
                        {
                            if (questPersonResponse.size > 0)
                            {
                                foreach (RHINCardServiceImplService.CardType card in questPersonResponse.persons[0].ids)
                                {
                                    //身份证
                                    if (card.type == "01")
                                    {
                                        icCardNO = card.number;
                                        break;
                                    }
                                }
                            }
                        }
                        if (icCardNO != string.Empty)
                        {
                            appointmentOrderList = appointmentMgr.QueryAppointmentOrder(this.dtStartDate.Value, this.dtEndDate.Value,
                                icCardNO, "01");
                        }
                    }
                }
            }
            else
            {
                appointmentOrderList = appointmentMgr.QueryAppointmentOrder(this.dtStartDate.Value, this.dtEndDate.Value);
            }

            this.neuFpEnter1_Sheet1.Rows.Remove(0, this.neuFpEnter1_Sheet1.RowCount);
            for (int i = 0; i < appointmentOrderList.Count; i++)
            {
                FS.HISFC.Models.Registration.AppointmentOrder appointmentOrder = appointmentOrderList[i] as FS.HISFC.Models.Registration.AppointmentOrder;
                this.neuFpEnter1_Sheet1.Rows.Add(this.neuFpEnter1_Sheet1.RowCount, 1);
                this.neuFpEnter1_Sheet1.Rows[i].Tag = appointmentOrder;

                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.AppointSerino, appointmentOrder.SerialNO, false);
                string orderType = string.Empty;
                if (appointmentOrder.OrderType == "0")
                {
                    orderType = "网络";
                }
                else if (appointmentOrder.OrderType == "1")
                {
                    orderType = "电话（12580）";
                }
                else if (appointmentOrder.OrderType == "2")
                {
                    orderType = "电话（114）";
                }
                else if (appointmentOrder.OrderType == "3")
                {
                    orderType = "自助终端";
                }
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.AppointType, orderType, false);
                string state = string.Empty;
                if (appointmentOrder.OrderState == "0")
                {
                    state = "预约";
                }
                else if (appointmentOrder.OrderState == "3")
                {
                    state = "支付";
                }
                else if (appointmentOrder.OrderState == "4")
                {
                    state = "取号";
                }
                else if (appointmentOrder.OrderState == "5")
                {
                    state = "退费";
                }
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.Status, state, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.CardNO, appointmentOrder.CardNo, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.Name, appointmentOrder.UserName, false);
                string gender = string.Empty;
                if (appointmentOrder.UserGender == "M")
                {
                    gender = "男";
                }
                else if (appointmentOrder.UserGender == "F")
                {
                    gender = "女";
                }
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.Sex, gender, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.Idenno, appointmentOrder.UserIdCard, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.Birthday, appointmentOrder.UserBirthday.ToString("yyyy-MM-dd"), false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.RelaPhone, appointmentOrder.UserMobile, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.Dept, appointmentOrder.Dept.Name, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.Doct, appointmentOrder.Doct.Name, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.SeeDate, appointmentOrder.RegDate.ToString("yyyy-MM-dd"), false);
                string timeFlag = string.Empty;
                if (appointmentOrder.TimeFlag == "1")
                {
                    timeFlag = "上午";
                }
                else if (appointmentOrder.TimeFlag == "2")
                {
                    timeFlag = "下午";
                }
                else if (appointmentOrder.TimeFlag == "3")
                {
                    timeFlag = "晚上";
                }
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.Noon, timeFlag, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.StartTime, appointmentOrder.StartTime, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.EndTime, appointmentOrder.EndTime, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.RegFee, appointmentOrder.RegFee / 100, false);
                this.neuFpEnter1_Sheet1.SetValue(i, (int)Cols.ChkFee, appointmentOrder.TreatFee / 100, false);
            }
        }

        private RHINCardServiceImplService.CommonCardAuthObject getAuthObject()
        {
            RHINCardServiceImplService.CommonCardAuthObject authObject = new RHINCardServiceImplService.CommonCardAuthObject();
            authObject.InstitutionCode = "455350760";
            authObject.departmentCode = "0001";
            //				authObject.staffNo = var.User.ID;
            //				authObject.Name = var.User.Name;
            authObject.staffNo = "0001";
            authObject.Name = "测试";
            authObject.role = "455350760_001";
            authObject.passWord = "888888";
            return authObject;
        }

        private void SetFpFormat()
        {
            this.neuFpEnter1_Sheet1.Columns.Count = 17;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.AppointSerino].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.AppointSerino].Label = "预约流水号";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.AppointSerino].Width = 90F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.AppointType].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.AppointType].Label = "预约方式";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.AppointType].Width = 80F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Status].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Status].Label = "状态";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Status].Width = 60F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.CardNO].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.CardNO].Label = "病历号";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.CardNO].Width = 80F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Name].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Name].Label = "姓名";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Name].Width = 60F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Sex].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Sex].Label = "性别";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Sex].Width = 60F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Idenno].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Idenno].Label = "身份证号";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Idenno].Width = 120F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Birthday].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Birthday].Label = "出生日期";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Birthday].Width = 80F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.RelaPhone].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.RelaPhone].Label = "联系电话";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.RelaPhone].Width = 90F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Dept].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Dept].Label = "科室";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Dept].Width = 100F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Doct].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Doct].Label = "医生";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Doct].Width = 80F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Doct].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.SeeDate].Label = "出诊日期";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.SeeDate].Width = 80F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Noon].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Noon].Label = "午别";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.Noon].Width = 60F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.StartTime].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.StartTime].Label = "开始时间";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.StartTime].Width = 60F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.EndTime].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.EndTime].Label = "结束时间";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.EndTime].Width = 60F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.RegFee].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.RegFee].Label = "挂号费";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.RegFee].Width = 60F;

            this.neuFpEnter1_Sheet1.Columns[(int)Cols.ChkFee].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.ChkFee].Label = "诊疗费";
            this.neuFpEnter1_Sheet1.Columns[(int)Cols.ChkFee].Width = 60F;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "建卡":
                    {
                        //this.ClearData();
                        break;
                    }
                case "取消预约":
                    {
                        this.CancelOrder();
                        this.SetAppointmentInfo();
                        break;
                    }
                case "取号":
                    {
                        this.GetRegInvoice();
                        this.SetAppointmentInfo();
                        break;
                    }
                case "退费":
                    {
                        this.CancelReg();
                        this.SetAppointmentInfo();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private int CancelOrder()
        {
            #region 验证
            if (this.neuFpEnter1_Sheet1.RowCount == 0)
            {
                MessageBox.Show("没有预约记录!", "提示");
                return -1;
            }

            int row = this.neuFpEnter1_Sheet1.ActiveRowIndex;

            if (MessageBox.Show("是否要取消该预约信息?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
                return -1;

            DateTime dtNow = this.regMgr.GetDateTimeFromSysDateTime();

            //实体
            FS.HISFC.Models.Registration.AppointmentOrder appointmentOrder =
                this.neuFpEnter1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Registration.AppointmentOrder;

            //防止并发
            appointmentOrder = this.appointmentMgr.QueryAppointmentOrderByOrderId(appointmentOrder.OrderID);

            if (appointmentOrder.OrderState == "3")
            {
                MessageBox.Show("该订单已支付！", "提示");
                return -1;
            }
            else if (appointmentOrder.OrderState == "4")
            {
                MessageBox.Show("该订单已取号！", "提示");
                return -1;
            }
            else if (appointmentOrder.OrderState == "5")
            {
                MessageBox.Show("该订单已退费！", "提示");
                return -1;
            }

            if (appointmentOrder.RegDate.Date < dtNow.Date)
            {
                MessageBox.Show("请在就诊日期之前取消预约！", "提示");
                return -1;
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.appointmentMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.schMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int rtn;

            FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

            try
            {
                if (this.appointmentMgr.UpdateAppointmentOrder(appointmentOrder.OrderID, "2", employee.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新预约平台订单状态出错!" + this.appointmentMgr.Err, "提示");
                    return -1;
                }

                #region 恢复限额
                //恢复原来排班限额
                //如果原来更新限额,那么恢复限额
                rtn = this.schMgr.Reduce(appointmentOrder.SchemaID, false, true, false, false);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.schMgr.Err, "提示");
                    return -1;
                }

                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("已无排班信息,无法恢复限额!", "提示");
                    return -1;
                }
                #endregion

                rtn = this.bookingMgr.Delete(appointmentOrder.SerialNO);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.bookingMgr.Err, "提示");
                    return -1;
                }

                string cancelTime = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
                AppointmentService.AppointmentService appointmentService = new AppointmentService.AppointmentService();
                string xmlResult = appointmentService.cancelOrderbyHis(appointmentOrder.OrderID, cancelTime, string.Empty);

                string resultCode = string.Empty;
                string resultDesc = string.Empty;
                if (this.ParseXmlResponse(xmlResult, ref resultCode, ref resultDesc) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("调用预约平台取消预约接口失败!", "提示");
                    return -1;
                }
                else
                {
                    if (resultCode.Equals("0"))
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("调用预约平台取消预约接口失败!" + resultDesc, "提示");
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("取消预约出错!" + e.Message, "提示");
                return -1;
            }

            MessageBox.Show("取消预约成功!", "提示");
            return 0;
        }

        private int CancelReg()
        {
            #region 验证
            if (this.neuFpEnter1_Sheet1.RowCount == 0)
            {
                MessageBox.Show("没有可退挂号记录!", "提示");
                return -1;
            }

            int row = this.neuFpEnter1_Sheet1.ActiveRowIndex;

            if (MessageBox.Show("是否要作废该挂号信息?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
                return -1;

            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            //实体
            FS.HISFC.Models.Registration.Register regSelect = null;
            FS.HISFC.Models.Registration.AppointmentOrder appointmentOrder =
                this.neuFpEnter1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Registration.AppointmentOrder;

            //防止并发
            appointmentOrder = this.appointmentMgr.QueryAppointmentOrderByOrderId(appointmentOrder.OrderID);

            if (appointmentOrder.OrderState == "0")
            {
                MessageBox.Show("该订单未支付！", "提示");
                return -1;
            }
            else if (appointmentOrder.OrderState == "5")
            {
                MessageBox.Show("该订单已退费！", "提示");
                return -1;
            }

            if (appointmentOrder.RegDate.Date < current.Date)
            {
                MessageBox.Show("请在就诊日期之前进行退费！", "提示");
                return -1;
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.accMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.schMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.appointmentMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int rtn;
            
            FS.HISFC.BizLogic.Registration.EnumUpdateStatus flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Cancel;

            try
            {
                //重新获取患者实体,防止并发
                regSelect = this.regMgr.GetByClinic(appointmentOrder.VisitingNum);
                //出错
                if (regSelect == null || string.IsNullOrEmpty(regSelect.ID))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }

                //使用,不能作废
                if (regSelect.IsSee)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该号已经看诊,不能作废"), "提示");
                    return -1;
                }

                //是否已经退号
                if (regSelect.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号记录已经退号，不能再次退号"), "提示");
                    return -1;
                }

                //是否已经作废
                if (regSelect.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号记录已经作废，不能进行退号"), "提示");
                    return -1;
                }

                const string OPERID = "00W999";
                string operId = OPERID;

                //				//退人工收费
                //				if (reg.OperID != OPERID)
                //				{
                //					operId = var.User.ID;
                //				}
                //				else
                //				{
                //					//羊城通支付的时候，在人工窗口退现金。
                //					if (appointmentOrder.PayMode == "3")
                //					{
                //						operId = var.User.ID;
                //					}
                //					else
                //					{
                //						operId = OPERID;
                //					}
                //				}

                FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

                List<AccountCardFee> lstReturnFee = new List<AccountCardFee>();

                rtn = this.accMgr.QueryAccCardFeeByClinic(regSelect.PID.CardNO, regSelect.ID, out lstReturnFee);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取挂号费诊金出错!"), "提示");
                    return -1;
                }

                AccountCardFee cardFee = null;
                int iRes = 0;
                for (int idx = 0; idx < lstReturnFee.Count; idx++)
                {
                    cardFee = lstReturnFee[idx].Clone();
                    cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    cardFee.Oper.ID = operId;
                    //cardFee.Oper.Oper.Name = employee.Name;
                    cardFee.Oper.OperTime = current;
                    cardFee.Tot_cost = -cardFee.Tot_cost;
                    cardFee.Own_cost = -cardFee.Own_cost;
                    cardFee.Pub_cost = -cardFee.Pub_cost;
                    cardFee.Pay_cost = -cardFee.Pay_cost;

                    cardFee.IStatus = 2;

                    iRes = this.accMgr.InsertAccountCardFee(cardFee);
                    if (iRes <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + this.accMgr.Err), "提示");
                        return -1;
                    }
                }

                FS.HISFC.Models.Registration.Register objReturn = regSelect.Clone();

                objReturn.RegLvlFee.ChkFee = -objReturn.RegLvlFee.ChkFee;
                objReturn.RegLvlFee.OwnDigFee = -objReturn.RegLvlFee.OwnDigFee;
                objReturn.RegLvlFee.OthFee = -objReturn.RegLvlFee.OthFee;
                objReturn.RegLvlFee.RegFee = -objReturn.RegLvlFee.RegFee;

                objReturn.OwnCost = -objReturn.OwnCost;
                objReturn.PubCost = -objReturn.PubCost;
                objReturn.PayCost = -objReturn.PayCost;

                objReturn.BalanceOperStat.IsCheck = false;
                objReturn.BalanceOperStat.ID = "";
                objReturn.BalanceOperStat.Oper.ID = "";

                objReturn.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Back;//退号
                objReturn.InputOper.OperTime = current;//操作时间
                objReturn.InputOper.ID = operId;//操作人
                objReturn.CancelOper.ID = employee.ID;//退号人
                objReturn.CancelOper.OperTime = current;//退号时间

                objReturn.TranType = FS.HISFC.Models.Base.TransTypes.Negative;

                if (this.regMgr.Insert(objReturn) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }

                #region 更新原来项目为作废
                flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Return;

                regSelect.CancelOper.ID = regMgr.Operator.ID;
                regSelect.CancelOper.OperTime = current;

                //更新原来项目为作废
                rtn = this.regMgr.Update(flag, regSelect);
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

                #endregion

                #region 恢复限额
                //恢复原来排班限额
                //如果原来更新限额,那么恢复限额
                if (regSelect.DoctorInfo.Templet.ID != null && regSelect.DoctorInfo.Templet.ID != "")
                {
                    //现场号、预约号、特诊号

                    bool IsReged = false, IsTeled = false, IsSped = false;

                    if (regSelect.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                    {
                        IsTeled = true; //预约号
                    }
                    else if (regSelect.RegType == FS.HISFC.Models.Base.EnumRegType.Reg)
                    {
                        IsReged = true;//现场号
                    }
                    else
                    {
                        IsSped = true;//特诊号
                    }

                    rtn = this.schMgr.Reduce(regSelect.DoctorInfo.Templet.ID, IsReged, false, IsTeled, IsSped);
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

                if (this.appointmentMgr.UpdateAppointmentOrder(appointmentOrder.OrderID, "5", employee.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新预约平台订单状态出错!" + this.appointmentMgr.Err, "提示");
                    return -1;
                }

                rtn = this.bookingMgr.Delete(appointmentOrder.SerialNO);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.bookingMgr.Err, "提示");
                    return -1;
                }

                string refundTime = string.Empty;
                refundTime = current.ToString("yyyy-MM-dd HH:mm:ss");

                if (this.appointmentMgr.InsertBookingPayHistory(appointmentOrder.OrderID, "用户请求退费", employee.ID, current) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入预约平台支付记录表出错!" + this.appointmentMgr.Err, "提示");
                    return -1;
                }

                AppointmentService.AppointmentService appointmentService = new AppointmentService.AppointmentService();
                string xmlResult = appointmentService.refundPay(appointmentOrder.OrderID, "2", refundTime,
                    Convert.ToString(appointmentOrder.RegFee + appointmentOrder.TreatFee), string.Empty);

                string resultCode = string.Empty;
                string resultDesc = string.Empty;
                if (this.ParseXmlResponse(xmlResult, ref resultCode, ref resultDesc) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("调用预约平台退号接口失败!", "提示");
                    return -1;
                }
                else
                {
                    if (resultCode.Equals("0"))
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("调用预约平台退号接口退号失败!" + resultDesc, "提示");
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("退号出错!" + e.Message, "提示");
                return -1;
            }

            MessageBox.Show(FS.FrameWork.Management.Language.Msg("退号成功"), "提示");
            return 0;
        }

        private int GetRegInvoice()
        {
            #region 验证
            if (this.neuFpEnter1_Sheet1.RowCount == 0)
            {
                MessageBox.Show("没有挂号记录!", "提示");
                return -1;
            }

            int row = this.neuFpEnter1_Sheet1.ActiveRowIndex;

            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();
            //实体
            FS.HISFC.Models.Registration.Register regSelect = null;
            FS.HISFC.Models.Registration.AppointmentOrder appointmentOrder =
                this.neuFpEnter1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Registration.AppointmentOrder;

            if (appointmentOrder.RegDate.Date != current.Date)
            {
                MessageBox.Show("请在就诊当天进行取号操作！", "提示");
                return -1;
            }

            //防止并发
            appointmentOrder = this.appointmentMgr.QueryAppointmentOrderByOrderId(appointmentOrder.OrderID);

            if (appointmentOrder.OrderState == "0")
            {
                MessageBox.Show("该订单状态未支付，请往挂号界面挂号！", "提示");
                return -1;
            }
            else if (appointmentOrder.OrderState == "4")
            {
                MessageBox.Show("该订单已取号！", "提示");
                return -1;
            }
            else if (appointmentOrder.OrderState == "5")
            {
                MessageBox.Show("该订单已退费！", "提示");
                return -1;
            }
            #endregion

            if (appointmentOrder.OrderState == "3")
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.accMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.schMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.appointmentMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int rtn;

                FS.HISFC.BizLogic.Registration.EnumUpdateStatus flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Cancel;

                //重新获取患者实体,防止并发
                regSelect = this.regMgr.GetByClinic(appointmentOrder.VisitingNum);
                //出错
                if (regSelect == null || string.IsNullOrEmpty(regSelect.ID))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }

                //是否已经退号
                if (regSelect.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号记录已经退号"), "提示");
                    return -1;
                }

                //是否已经作废
                if (regSelect.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("该挂号记录已经作废"), "提示");
                    return -1;
                }

                List<AccountCardFee> lstAccFee = new List<AccountCardFee>();

                rtn = this.accMgr.QueryAccCardFeeByClinic(regSelect.PID.CardNO, regSelect.ID, out lstAccFee);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取挂号费诊金出错!"), "提示");
                    return -1;
                }

                #region 取发票号

                string strInvioceNO = "";
                string strRealInvoiceNO = "";
                string strErrText = "";
                int iRes = 0;
                string strInvoiceType = "R";

                FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

                //有费用信息的时候才打发票
                if (lstAccFee.Count > 0)
                {

                    if (this.GetRecipeType == 1)
                    {
                        //strInvioceNO = this.regObj.RecipeNO.ToString().PadLeft(12, '0');
                        strRealInvoiceNO = "";
                    }
                    else
                    {
                        if (this.GetRecipeType == 2)
                        {
                            strInvoiceType = "R";
                        }
                        else if (this.GetRecipeType == 3)
                        {
                            // 取门诊收据
                            strInvoiceType = "C";
                        }

                        iRes = this.feeMgr.GetInvoiceNO(employee, strInvoiceType, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText);

                        if (iRes == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(strErrText);
                            return -1;
                        }
                    }
                }

                #endregion

                AccountCardFee cardFee = null;
                for (int idx = 0; idx < lstAccFee.Count; idx++)
                {
                    cardFee = lstAccFee[idx].Clone();

                    cardFee.InvoiceNo = strInvioceNO;
                    cardFee.Print_InvoiceNo = strRealInvoiceNO;

                    cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    cardFee.Oper.ID = employee.ID;
                    cardFee.Oper.Name = employee.Name;
                    cardFee.Oper.OperTime = current;
                    cardFee.Tot_cost = -cardFee.Tot_cost;
                    cardFee.Own_cost = -cardFee.Own_cost;
                    cardFee.Pub_cost = -cardFee.Pub_cost;
                    cardFee.Pay_cost = -cardFee.Pay_cost;

                    cardFee.IStatus = 2;
                    iRes = this.accMgr.InsertAccountCardFee(cardFee);
                    if (iRes <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + this.accMgr.Err), "提示");
                        return -1;
                    }
                }

                for (int idx = 0; idx < lstAccFee.Count; idx++)
                {
                    cardFee = lstAccFee[idx].Clone();

                    cardFee.InvoiceNo = strInvioceNO;
                    cardFee.Print_InvoiceNo = strRealInvoiceNO;

                    cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    cardFee.Oper.ID = employee.ID;
                    cardFee.Oper.Name = employee.Name;
                    cardFee.Oper.OperTime = current;

                    cardFee.IStatus = 1;
                    iRes = this.accMgr.InsertAccountCardFee(cardFee);
                    if (iRes <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("操作失败！" + this.accMgr.Err), "提示");
                        return -1;
                    }
                }

                FS.HISFC.Models.Registration.Register objReturn = regSelect.Clone();

                objReturn.RegLvlFee.ChkFee = -objReturn.RegLvlFee.ChkFee;
                objReturn.RegLvlFee.OwnDigFee = -objReturn.RegLvlFee.OwnDigFee;
                objReturn.RegLvlFee.OthFee = -objReturn.RegLvlFee.OthFee;
                objReturn.RegLvlFee.RegFee = -objReturn.RegLvlFee.RegFee;

                objReturn.OwnCost = -objReturn.OwnCost;
                objReturn.PubCost = -objReturn.PubCost;
                objReturn.PayCost = -objReturn.PayCost;

                objReturn.BalanceOperStat.IsCheck = false;
                objReturn.BalanceOperStat.ID = "";
                objReturn.BalanceOperStat.Oper.ID = "";

                objReturn.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Back;//退号
                objReturn.InputOper.OperTime = current;//操作时间
                objReturn.InputOper.ID = employee.ID;//操作人
                objReturn.CancelOper.ID = employee.ID;//退号人
                objReturn.CancelOper.OperTime = current;//退号时间

                objReturn.TranType = FS.HISFC.Models.Base.TransTypes.Negative;

                if (this.regMgr.Insert(objReturn) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }

                #region 更新原来项目为作废
                flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Return;

                regSelect.CancelOper.ID = regMgr.Operator.ID;
                regSelect.CancelOper.OperTime = current;

                //更新原来项目为作废
                rtn = this.regMgr.Update(flag, regSelect);
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

                #endregion

                regSelect.InputOper.OperTime = current;//操作时间
                regSelect.InputOper.ID = employee.ID;//操作人
                regSelect.CancelOper.ID = "";
                regSelect.CancelOper.OperTime = DateTime.MinValue;
                regSelect.TranType = FS.HISFC.Models.Base.TransTypes.Positive;
                regSelect.InvoiceNO = strInvioceNO;
                regSelect.ID = this.regMgr.GetSequence("Registration.Register.ClinicID");

                if (this.regMgr.Insert(regSelect) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "提示");
                    return -1;
                }

                //有费用信息的时候，才处理发票
                if (lstAccFee.Count > 0)
                {

                    if (this.GetRecipeType == 2 || this.GetRecipeType == 3)
                    {
                        string invoiceStytle = controlParma.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");
                        if (this.feeMgr.UseInvoiceNO(employee, invoiceStytle, strInvoiceType, 1, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.feeMgr.Err);
                            return -1;
                        }

                        if (this.feeMgr.InsertInvoiceExtend(strInvioceNO, strInvoiceType, strRealInvoiceNO, "00") < 1)
                        {
                            // 发票头暂时先保存00
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.feeMgr.Err);
                            return -1;
                        }
                    }

                }

                if (this.appointmentMgr.UpdateAppointmentOrderSerialNO(appointmentOrder.OrderID, "4", employee.ID, regSelect.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新预约平台订单状态出错!" + this.appointmentMgr.Err, "提示");
                    return -1;
                }

                try
                {
                    AppointmentService.AppointmentService appointmentService = new AppointmentService.AppointmentService();
                    string infoSeq = string.Empty;
                    infoSeq = this.appointmentMgr.GetSequence("Registration.AppointmentSerialNo");
                    string infoTime = string.Empty;
                    infoTime = this.appointmentMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                    string xmlResult = appointmentService.printRegInfo(appointmentOrder.OrderID, infoSeq, infoTime);

                    string resultCode = string.Empty;
                    string resultDesc = string.Empty;
                    if (this.ParseXmlResponse(xmlResult, ref resultCode, ref resultDesc) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("调用预约平台取号接口失败!", "提示");
                        return -1;
                    }
                    else
                    {
                        if (resultCode.Equals("0"))
                        {
                            FS.FrameWork.Management.PublicTrans.Commit();
                            //条码打印
                            this.PrintBarCode(regSelect);
                            this.Print(regSelect);
                            MessageBox.Show("取号成功!", "提示");
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("调用预约平台取号接口取号失败!" + resultDesc, "提示");
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("调用预约平台取号接口失败!" + ex.Message, "提示");
                    return -1;
                }
            }
            this.InitInvoiceInfo();
            return 0;
        }

        private int ParseXmlResponse(string xmlResponse, ref string resultCode, ref string resultDesc)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResponse);
                XmlNode resultCodeNode = xmlDoc.SelectSingleNode("res/resultCode");
                resultCode = resultCodeNode.InnerText;
                XmlNode resultDescNode = xmlDoc.SelectSingleNode("res/resultDesc");
                resultDesc = resultDescNode.InnerText;
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 初始化界面的发票信息显示
        /// </summary>
        /// <returns></returns>
        public int InitInvoiceInfo()
        {
            string strInvioceNO = "";
            string strRealInvoiceNO = "";
            string strErrText = "";
            int iRes = 0;
            string strInvoiceType = "R";   //挂号收据

            FS.HISFC.Models.Base.Employee employee = this.regMgr.Operator as FS.HISFC.Models.Base.Employee;

            iRes = this.feeMgr.GetInvoiceNO(employee, strInvoiceType, ref strInvioceNO, ref strRealInvoiceNO, ref strErrText);

            if (iRes == -1)
            {
                MessageBox.Show("获取挂号发票出错!");
                return -1;
            }

            this.tbInvoiceNO.Text = strInvioceNO;
            this.tbRealInvoiceNO.Text = strRealInvoiceNO;

            return 1;
        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="regObj"></param>
        private void PrintBarCode(FS.HISFC.Models.Registration.Register register)
        {
            //if (register.PrintInvoiceCnt == 2 || (register.PrintInvoiceCnt == 0 && register.IsFirst))
            //本地化里面判断是否打印条码
            {
                FS.HISFC.BizProcess.Interface.Registration.IPrintBar printBarCode = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Registration.ucRegisterNew), typeof(FS.HISFC.BizProcess.Interface.Registration.IPrintBar)) as FS.HISFC.BizProcess.Interface.Registration.IPrintBar;
                if (printBarCode != null)
                {
                    string errText = string.Empty;
                    if (printBarCode.printBar(register, ref errText) < 0)
                    {
                        MessageBox.Show(errText, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

        }

        /// <summary>
        /// 打印挂号发票
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(FS.HISFC.Models.Registration.Register regObj)
        {
            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Registration.ucRegisterNew), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;
            if (regprint == null)
            {
                MessageBox.Show(FS.FrameWork.WinForms.Classes.UtilInterface.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //regprint.SetPrintValue(regObj,regmr);
            if (regObj.IsEncrypt)
            {
                regObj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(regObj.NormalName);
            }

            regprint.SetPrintValue(regObj);
            regprint.Print();
        }

        #region 列
        protected enum Cols
        {
            AppointSerino,
            AppointType,
            Status,
            CardNO,
            Name,
            Sex,
            Idenno,
            Birthday,
            RelaPhone,
            Dept,
            Doct,
            SeeDate,
            Noon,
            StartTime,
            EndTime,
            RegFee,
            ChkFee
        }
        #endregion
    }
}
