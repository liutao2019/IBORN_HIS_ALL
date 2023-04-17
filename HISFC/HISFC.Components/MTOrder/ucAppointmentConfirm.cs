using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Components.MTOrder.Forms;

namespace FS.HISFC.Components.MTOrder
{
    public partial class ucAppointmentConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAppointmentConfirm()
        {
            InitializeComponent();
            #region 加载事件
            this.Load += new EventHandler(ucAppointmentConfirm_Load);
            neuSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);
            this.ReflashTimer.Tick += new EventHandler(ReflashTimer_Tick);
            #endregion
            ToolTip tip = new ToolTip();
            tip.IsBalloon = true;
            tip.SetToolTip(lblRemain, "剩余人数/总人数");

            #region 颜色初始化
            ClPaid.BackColor = SystemColors.Window;
            ClUnPaid.BackColor = Color.Pink;
            ClExecuted.BackColor = Color.SkyBlue;
            #endregion
        }


        #region 列
        enum cols
        {
            /// <summary>
            /// 预约ID
            /// </summary>
            AppointID = 0,
            /// <summary>
            /// 序号
            /// </summary>
            OrderNo,
            /// <summary>
            /// 时段
            /// </summary>
            ArrangeTime,
            /// <summary>
            /// 缴费状态
            /// </summary>
            Status,
            /// <summary>
            /// 患者姓名
            /// </summary>
            Name,
            /// <summary>
            /// 年龄
            /// </summary>
            Age,
            /// <summary>
            /// 姓别
            /// </summary>
            Sex,
            /// <summary>
            /// 联系电话
            /// </summary>
            Telephone,
            /// <summary>
            /// 预约类型
            /// </summary>
            OrderType,
            /// <summary>
            /// 床号
            /// </summary>
            BedNo,
            /// <summary>
            /// 医技类型
            /// </summary>
            MTType,
            /// <summary>
            /// 申请项目名称
            /// </summary>
            ItemName,
            /// <summary>
            /// 医技项目
            /// </summary>
            MTName,
            /// <summary>
            /// 到达方式
            /// </summary>
            ArrivalPattern,
            /// <summary>
            /// 病历/住院号
            /// </summary>
            CardNo,
            /// <summary>
            /// 门诊/住院流水号
            /// </summary>
            ClinicNo,
            /// <summary>
            /// 开立科室
            /// </summary>
            DeptName,
            /// <summary>
            /// 开立医生
            /// </summary>
            Doctor,
            /// <summary>
            /// 执行医生
            /// </summary>
            ExecDoctor,
            /// <summary>
            /// 执行科室
            /// </summary>
            ExecDept,
            /// <summary>
            /// 申请时间
            /// </summary>
            ApplyTime
        }
        #endregion

        #region 域
        private DataTable dsItems;
        private DataView dv;
        private List<FS.HISFC.Models.MedicalTechnology.Appointment> AppointmentList = new List<FS.HISFC.Models.MedicalTechnology.Appointment>();

        private DateTime date = DateTime.Now.Date;
        public DateTime Date
        {
            get { return this.date; }
            set
            {
                this.date = value;
                this.lblDate.Text = "预约详情(" + date.ToString("yyyy-MM-dd") + ")";
                Query();
            }
        }
        #region 管理类
        /// <summary>
        /// 预约管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalTechnology.Appointment appMgr = new FS.HISFC.BizLogic.MedicalTechnology.Appointment();
        /// <summary>
        /// 常数管理类(获取医技列表和医技类型)
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 门诊医嘱单管理类
        /// </summary>
        FS.HISFC.BizLogic.Order.OutPatient.Order outPatientMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        /// <summary>
        /// 住院医嘱单管理类
        /// </summary>
        FS.HISFC.BizLogic.Order.Order inPatientMgr = new FS.HISFC.BizLogic.Order.Order();
        #endregion

        #region 属性

        private bool isShowAll = true;
        /// <summary>
        /// 是否显示全部医技类型患者
        /// </summary>
        [Category("控件设置"), Description("是否显示全部医技类型患者")]
        public bool IsShowAll
        {
            get
            {
                return isShowAll;
            }
            set
            {
                isShowAll = value;
            }
        }
        private bool isPrintBill = true;
        /// <summary>
        /// 是否打印预约单
        /// </summary>
        [Category("控件设置"), Description("是否打印预约单")]
        public bool IsPrintBill
        {
            get
            {
                return isPrintBill;
            }
            set
            {
                isPrintBill = value;
            }
        }

        private bool isAbleCanclePaid = false;
        /// <summary>
        /// 是否可以取消已缴费预约信息
        /// </summary>
        [Category("控件设置"), Description("是否可以取消已缴费预约信息")]
        public bool IsAbleCanclePaid
        {
            get
            {
                return isAbleCanclePaid;
            }
            set
            {
                isAbleCanclePaid = value;
            }
        }

        private bool isUseCurrentUserAsDoctor = false;
        /// <summary>
        /// 是否可以取消已缴费预约信息
        /// </summary>
        [Category("控件设置"), Description("改约时是否以当前登录科室及人员为开立科室/医生")]
        public bool IsUseCurrentUserAsDoctor
        {
            get
            {
                return isUseCurrentUserAsDoctor;
            }
            set
            {
                isUseCurrentUserAsDoctor = value;
            }
        }

        private bool isAbleChangePaid = true;
        /// <summary>
        /// 是否可以改约已缴费预约信息
        /// </summary>
        [Category("控件设置"), Description("是否可以改约已缴费预约信息")]
        public bool IsAbleChangePaid
        {
            get
            {
                return isAbleChangePaid;
            }
            set
            {
                isAbleChangePaid = value;
            }
        }
        /// <summary>
        /// 是否启用自动刷新
        /// </summary>
        [Category("控件设置"), Description("是否启用自动刷新")]
        public bool IsAutoReflash
        {
            get
            {
                return ReflashTimer.Enabled;
            }
            set
            {
                ReflashTimer.Enabled = value;
            }
        }
        /// <summary>
        /// 自动刷新频率
        /// </summary>
        [Category("控件设置"), Description("自动刷新频率")]
        public int Interval
        {
            get
            {
                return ReflashTimer.Interval;
            }
            set
            {
                ReflashTimer.Interval = value;
            }
        }
        /// <summary>
        /// 未收费显示颜色
        /// </summary>
        [Category("颜色设置"), Description("未收费显示颜色")]
        public Color UnPaidColor
        {
            get
            {
                return ClUnPaid.BackColor;
            }
            set
            {
                ClUnPaid.BackColor = value;
            }
        }
        /// <summary>
        /// 已收费显示颜色
        /// </summary>
        [Category("颜色设置"), Description("已收费显示颜色")]
        public Color PaidColor
        {
            get
            {
                return ClPaid.BackColor;
            }
            set
            {
                ClPaid.BackColor = value;
            }
        }
        /// <summary>
        /// 已执行显示颜色
        /// </summary>
        [Category("颜色设置"), Description("已执行显示颜色")]
        public Color ExecutedColor
        {
            get
            {
                return ClExecuted.BackColor;
            }
            set
            {
                ClExecuted.BackColor = value;
            }
        }
        #endregion

        #endregion

        #region 菜单
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("查找", "查找相关病人信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查找, true, false, null);
            toolBarService.AddToolButton("清屏", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("日期", "选择日期", (int)FS.FrameWork.WinForms.Classes.EnumImageList.R日期, true, false, null);
            toolBarService.AddToolButton("上一天", "上一天", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S上一个, true, false, null);
            toolBarService.AddToolButton("下一天", "下一天", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X下一个, true, false, null);

            toolBarService.AddToolButton("医技预约", "医技预约", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预约, true, false, null);

            toolBarService.AddToolButton("改约", "改预约日期", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预约, true, false, null);
            toolBarService.AddToolButton("取消预约", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

            toolBarService.AddToolButton("打印预约单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);

            toolBarService.AddToolButton("刷卡", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            toolBarService.AddToolButton("身份证", "身份证", (int)FS.FrameWork.WinForms.Classes.EnumImageList.M模版, true, false, null);

            return toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清屏":
                    Clear();
                    break;

                case "医技预约":
                    {
                        frmMTApply frmApply = new frmMTApply();
                        frmApply.IsShowBtnSearch = true;
                        frmApply.ShowDialog();
                    }
                    break;
                case "改约":
                    ChangeAppointment();
                    break;
                case "取消预约":
                    CancleAppointment();
                    break;

                case "打印预约单":
                    HISFC.Models.MedicalTechnology.Appointment app = txtAppID.Tag as HISFC.Models.MedicalTechnology.Appointment;
                    if (app == null)
                    {
                        MessageBox.Show("请选择预约信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (app.ApplyStatus == Models.MedicalTechnology.EnumApplyStatus.未缴费 ||app.ApplyStatus == Models.MedicalTechnology.EnumApplyStatus.已缴费)
                    {
                        FS.HISFC.Components.MTOrder.OrderPrint.ucApplyOrderPrint print = new FS.HISFC.Components.MTOrder.OrderPrint.ucApplyOrderPrint();
                        print.SetValues(app);
                    }
                    else
                    {
                        MessageBox.Show("该项目不可打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "日期":
                    {
                        frmSelectDate frmDate = new frmSelectDate();
                        frmDate.SelectedDate = this.date;
                        if (frmDate.ShowDialog() == DialogResult.Yes)
                        {
                            this.Date = frmDate.SelectedDate;
                        }
                        break;
                    }

                case "上一天":
                    {
                        Date = Date.AddDays(-1);
                        break;
                    }
                case "下一天":
                    {
                        Date = Date.AddDays(1);
                        break;
                    }
                case "刷卡":
                    break;

                case "身份证":
                    break;

                case "查找":
                    new frmInputKeys().ShowDialog();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion
        #region 初始化
        private void ucAppointmentConfirm_Load(object sender, EventArgs e)
        {
            InitDataSet();
            this.Date = DateTime.Now;
        }
        private void InitDataSet()
        {
            dsItems = new DataTable("Appointment");

            dsItems.Columns.AddRange(new DataColumn[]
			{
				new DataColumn(cols.AppointID.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.OrderNo.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ArrangeTime.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.Status.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.Name.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.Age.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.Sex.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.Telephone.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.OrderType.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.BedNo.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ArrivalPattern.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.CardNo.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ClinicNo.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.DeptName.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.Doctor.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.MTName.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.MTType.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ItemName.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ExecDoctor.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ExecDept.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ApplyTime.ToString(),System.Type.GetType("System.String"))
			});
        }

        private void SetFpFormat()
        {
            this.neuSpread1_Sheet1.Columns[(int)cols.AppointID].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)cols.OrderNo].Width = 20F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ArrangeTime].Width = 85F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Status].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Name].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Age].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Sex].Width = 20F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Telephone].Width = 80F;

            this.neuSpread1_Sheet1.Columns[(int)cols.OrderType].Width = 40F;
            this.neuSpread1_Sheet1.Columns[(int)cols.BedNo].Width = 40F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ArrivalPattern].Width = 40F;

            this.neuSpread1_Sheet1.Columns[(int)cols.CardNo].Width = 80F;

            this.neuSpread1_Sheet1.Columns[(int)cols.ClinicNo].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)cols.DeptName].Width = 80F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Doctor].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.MTName].Width = 60F;

            this.neuSpread1_Sheet1.Columns[(int)cols.MTType].Width = 150F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ItemName].Width = 200F;

            this.neuSpread1_Sheet1.Columns[(int)cols.ExecDoctor].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ExecDept].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)cols.ApplyTime].Width = 120F;
            //全部居中显示
            for (int i = 0; i < neuSpread1_Sheet1.ColumnCount; i++)
            {
                neuSpread1_Sheet1.Columns[i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                neuSpread1_Sheet1.Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            }

            DisplayColor();
        }
        /// <summary>
        /// 显示颜色
        /// </summary>
        private void DisplayColor()
        {
            if (AppointmentList.Count < 1) return;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                //如果没有缴费
                if (AppointmentList[i].ApplyStatus == Models.MedicalTechnology.EnumApplyStatus.未缴费)
                {
                    int couCnt = this.neuSpread1_Sheet1.ColumnCount;
                    for (int j = 0; j < couCnt; j++)
                    {
                        this.neuSpread1_Sheet1.Cells[i, j].BackColor = ClUnPaid.BackColor ;
                    }
                    this.neuSpread1_Sheet1.Rows[i].Locked = true;
                    continue;
                }
                //如果已执行
                if (AppointmentList[i].ApplyStatus == Models.MedicalTechnology.EnumApplyStatus.已执行)
                {
                    int couCnt = this.neuSpread1_Sheet1.ColumnCount;
                    for (int j = 0; j < couCnt; j++)
                    {
                        this.neuSpread1_Sheet1.Cells[i, j].BackColor = ClExecuted.BackColor;
                    }
                    this.neuSpread1_Sheet1.Rows[i].Locked = true;
                    continue;
                }

                for (int j = 0; j < this.neuSpread1_Sheet1.ColumnCount; j++)
                {
                    this.neuSpread1_Sheet1.Cells[i, j].BackColor = ClPaid.BackColor;

                }

            }
        }
        #endregion

        #region 查询
        private void Query()
        {
            //当前登录科室
            string deptCode = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
            if (isShowAll)
                deptCode = "ALL";

            this.AppointmentList = appMgr.GetAppointmentByDateAndDept(date, deptCode);

            if (this.AppointmentList == null)
            {
                MessageBox.Show("查询预约信息出错!" + this.appMgr.Err, "提示");
                return;
            }
            dsItems.Clear();
            try
            {
                AppointmentList.ForEach(app =>
                {
                    dsItems.Rows.Add(new object[]
                            {
                                app.ID,
                                app.OrderNo,
                                app.ArrangeTime,
                                app.ApplyStatus.ToString(),
                                app.Name,
                                app.Age,
                                app.Sex,
                                app.Telephone,
                                ApplyTypeConverter(app.OrderType),
                                app.BedNo,
                                app.TypeName,
                                app.ItemName,
                                app.MTName,
                                app.ArrivalPattern,
                                app.CardNo,
                                app.ClinicCode,
                                app.DeptName,
                                app.DoctName,
                                app.ExecDoctName,
                                app.ExecDeptName,
                                app.ApplyDate.ToString("yyyy-MM-dd HH:mm:ss")
                            });
                });

            }
            catch (Exception e)
            {
                MessageBox.Show("查询预约信息生成DataSet时出错!" + e.Message, "提示");
                return;
            }
            dsItems.AcceptChanges();

            dv = dsItems.DefaultView;
            //if (this.neuSpread1_Sheet1.Rows.Count > 0)
            //    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
            this.neuSpread1_Sheet1.DataSource = dv;

            this.SetFpFormat();
            //更新剩余人数
            lblRemain.Text = AppointmentList.Where(t => t.ApplyStatus != Models.MedicalTechnology.EnumApplyStatus.已执行).Count() + "/" + AppointmentList.Count;
        }
        #endregion
        /// <summary>
        /// 将OrderType转成中文
        /// </summary>
        /// <param name="ApplyType"></param>
        /// <returns></returns>
        private string ApplyTypeConverter(FS.HISFC.Models.MedicalTechnology.EnumApplyType ApplyType)
        {
            switch (ApplyType)
            {
                case Models.MedicalTechnology.EnumApplyType.Clinic: return "门诊";
                case Models.MedicalTechnology.EnumApplyType.Hospital: return "住院";
            }
            return string.Empty;
        }
        #region 事件
        /// <summary>
        /// 取消预约
        /// </summary>
        private void CancleAppointment()
        {
            FS.HISFC.Models.MedicalTechnology.Appointment app = this.txtAppID.Tag as FS.HISFC.Models.MedicalTechnology.Appointment;
            if (app == null)
            {
                MessageBox.Show("请选择预约信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (app.ApplyStatus != Models.MedicalTechnology.EnumApplyStatus.未缴费 && IsAbleCanclePaid == false)
            {
                MessageBox.Show("该预约信息不可取消,请选择【未缴费】预约信息进行该操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show("是否取消【" + app.Name + "】的【" + app.ItemName + "】预约信息?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    appMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    int result = appMgr.Cancle(this.txtAppID.Tag as FS.HISFC.Models.MedicalTechnology.Appointment, Models.Base.CancelTypes.Canceled);
                    if (result < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("取消预约出错,Err:" + appMgr.Err);
                        return;
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                        MessageBox.Show("取消预约成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reflash();
                    }
                }
                catch (System.Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("取消预约出错,Err:" + ex.Message);
                    return;
                }

            }
        }
        /// <summary>
        /// 改约
        /// </summary>
        private void ChangeAppointment()
        {
            FS.HISFC.Models.MedicalTechnology.Appointment app = this.txtAppID.Tag as FS.HISFC.Models.MedicalTechnology.Appointment;

            if (IsAbleChangePaid == false && app.ApplyStatus != Models.MedicalTechnology.EnumApplyStatus.未缴费)
            {
                MessageBox.Show("根据医院政策,不允许更改已缴费预约信息,请选择【未缴费】预约信息进行该操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (app == null)
            {
                MessageBox.Show("请选择预约信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show("是否为【" + app.Name + "】的【" + app.ItemName + "】预约改约日期?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                ucMTList mtList = new ucMTList();
                mtList.MedTechItem = new FS.HISFC.Models.Base.Const() { ID = app.MTCode, Name = app.MTName };
                if (app.OrderType == Models.MedicalTechnology.EnumApplyType.Clinic)
                    mtList.MedTechOrder = outPatientMgr.QueryOneOrder(app.SequenceNo);
                else
                    mtList.MedTechOrder = inPatientMgr.QueryOneOrder(app.SequenceNo);
                mtList.ApplyMedticalTechnology = new ucMTList.ApplyEvent(MedicalTechnology_Applying);

                FS.FrameWork.WinForms.Classes.Function.ShowControl(mtList);
            }
        }

        /// <summary>
        /// 预约界面双击事件(改约处理代码)
        /// </summary>
        /// <param name="arrange"></param>
        /// <param name="order"></param>
        private void MedicalTechnology_Applying(FS.HISFC.Models.MedicalTechnology.Arrange arrange, FS.HISFC.Models.Order.Order order)
        {
            if (arrange.EndTime <= DateTime.Now)
            {
                MessageBox.Show("该排班已过期,无法预约", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //当前的预约信息
            FS.HISFC.Models.MedicalTechnology.Appointment currentApp = this.txtAppID.Tag as FS.HISFC.Models.MedicalTechnology.Appointment;

            if (currentApp.ArrangeID == arrange.ID)
            {
                MessageBox.Show("当前预约排班与所选排班相同,无法改约", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult dr = MessageBox.Show("是否将【" + currentApp.Name + "】所预约的【" + currentApp.TypeName + " -- " + currentApp.ArrangeDate.ToString("yyyy-MM-dd ") + currentApp.ArrangeTime +
                "】\r\n改为【" + arrange.TypeName + " -- " + arrange.SeeDate.ToString("yyyy-MM-dd ") + arrange.BeginTime.ToString("HH:mm - ") + arrange.EndTime.ToString("HH:mm") + "】?",
                "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.No)
                return;


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            appMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime dt = appMgr.GetDateTimeFromSysDateTime();

            
            #region 添加当前操作人信息
            currentApp.OperCode = FS.FrameWork.Management.Connection.Operator.ID;
            currentApp.OperDate = dt;
            #endregion
            try
            {
                //取消当前的预约
                int result = appMgr.Cancle(currentApp, Models.Base.CancelTypes.Reprint);
                if (result < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("取消预约出错,Err:" + appMgr.Err);
                    return;
                }

                FS.HISFC.Models.MedicalTechnology.Appointment app = new FS.HISFC.Models.MedicalTechnology.Appointment();

                #region 赋值

                app.ID = appMgr.GetSequence("MedicalTechnology.Appointment.ID");
                app.CardNo = currentApp.CardNo;
                app.ClinicCode = currentApp.ClinicCode;
                app.Name = currentApp.Name;
                app.Sex = currentApp.Sex;
                app.SequenceNo = currentApp.SequenceNo;
                app.Telephone = currentApp.Telephone;
                app.Age = currentApp.Age;
                app.ArrivalPattern = currentApp.ArrivalPattern;
                app.ItemCode = currentApp.ItemCode;
                app.ItemName = currentApp.ItemName;
                app.ItemCost = currentApp.ItemCost;
                app.MTCode = currentApp.MTCode;
                app.MTName = currentApp.MTName;
                app.TypeCode = arrange.TypeCode;
                app.TypeName = arrange.TypeName;
                app.OrderType = currentApp.OrderType;
                app.OrderNo = appMgr.GetOrderNo(arrange);
                app.BedNo = currentApp.BedNo;

                app.ArrangeID = arrange.ID;
                app.ArrangeDate = arrange.SeeDate;
                app.BeginTime = arrange.BeginTime;
                app.EndTime = arrange.EndTime;

                app.MedicalHistory = currentApp.MedicalHistory;
                app.Diagnosis = currentApp.Diagnosis;
                app.ExecDeptCode = currentApp.ExecDeptCode;
                app.ExecDeptName = currentApp.ExecDeptName;
                app.ExecDoctCode = currentApp.ExecDoctCode;
                app.ExecDoctName = currentApp.ExecDoctName;

                //是否将开立科室/医生改为当前登录科室/医生
                if (IsUseCurrentUserAsDoctor)
                {
                    app.DeptCode = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                    app.DeptName = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.Name;
                    app.DoctCode = FS.FrameWork.Management.Connection.Operator.ID;
                    app.DoctName = FS.FrameWork.Management.Connection.Operator.Name;
                }
                else
                {
                    app.DeptCode = currentApp.DeptCode;
                    app.DeptName = currentApp.DeptName;
                    app.DoctCode = currentApp.DoctCode;
                    app.DoctName = currentApp.DoctName;
                }

                app.ApplyDate = dt;

                app.OperDate = dt;
                app.IsValid = true;

                app.CancleAppointment = currentApp.ID;
                app.CancleFlag = Models.Base.CancelTypes.Reprint;
                #endregion

                result = appMgr.Insert(app);
                if (result < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("预约出错,Err:" + appMgr.Err);
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                if (IsPrintBill && MessageBox.Show("是否打印预约单?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    new FS.HISFC.Components.MTOrder.OrderPrint.ucApplyOrderPrint().SetValues(app);
                }

                MessageBox.Show("改约成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("预约出错,Err:" + ex.Message);
                return;
            }
            Reflash();

        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.HISFC.Models.MedicalTechnology.Appointment app = AppointmentList[e.Row];
            #region 赋值
            this.txtAppID.Text = app.ID;
            this.txtAppID.Tag = app;
            this.txtStatues.Text = app.ApplyStatus.ToString();
            this.txtOrderNo.Text = app.OrderNo;
            this.txtOrderType.Text = ApplyTypeConverter(app.OrderType);
            this.txtMT.Text = app.MTName;
            this.txtMTType.Text = app.TypeName;
            this.txtItemName.Text = app.ItemName;
            this.txtDoctor.Text = app.DoctName;
            this.txtDeptName.Text = app.DeptName;
            this.txtExecDoctor.Text = app.ExecDoctName;
            this.txtExecDept.Text = app.ExecDeptName;
            this.txtApplyTime.Text = app.ApplyDate.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtArrangeDate.Text = app.ArrangeDate.ToString("yyyy-MM-dd");
            this.txtArrangeTime.Text = app.ArrangeTime;
            this.txtCardNo.Text = app.CardNo;
            this.txtClinicNo.Text = app.ClinicCode;
            this.txtName.Text = app.Name;
            this.txtSex.Text = app.Sex;
            this.txtTelephone.Text = app.Telephone;
            this.txtDiagnosis.Text = app.Diagnosis;
            this.txtMedicalHistory.Text = app.MedicalHistory;
            #endregion

        }
        /// <summary>
        /// 清除信息
        /// </summary>
        private void Clear()
        {
            this.txtAppID.Text = string.Empty;
            this.txtAppID.Tag = null;
            this.txtStatues.Text = string.Empty;
            this.txtOrderNo.Text = string.Empty;
            this.txtOrderType.Text = string.Empty;
            this.txtMT.Text = string.Empty;
            this.txtMTType.Text = string.Empty;
            this.txtItemName.Text = string.Empty;
            this.txtDoctor.Text = string.Empty;
            this.txtDeptName.Text = string.Empty;
            this.txtExecDoctor.Text = string.Empty;
            this.txtExecDept.Text = string.Empty;
            this.txtApplyTime.Text = string.Empty;
            this.txtArrangeTime.Text = string.Empty;
            this.txtArrangeDate.Text = string.Empty;
            this.txtCardNo.Text = string.Empty;
            this.txtClinicNo.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtSex.Text = string.Empty;
            this.txtTelephone.Text = string.Empty;
            this.txtDiagnosis.Text = string.Empty;
            this.txtMedicalHistory.Text = string.Empty;
        }

        /// <summary>
        /// 定时刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ReflashTimer_Tick(object sender, EventArgs e)
        {
            Reflash();
        }
        /// <summary>
        /// 刷新界面
        /// </summary>
        void Reflash()
        {
            Clear();
            Query();
        }
        #endregion
    }

}
