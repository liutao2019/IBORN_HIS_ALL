using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.MTOrder
{
    public partial class ucMTList : UserControl
    {
        
        public ucMTList()
        {
            InitializeComponent();
            #region 加载事件
            this.Load += new System.EventHandler(this.ucMTList_Load);
            this.dtpSearchDate.ValueChanged += new System.EventHandler(this.dtpSearchDate_ValueChanged);
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            this.cmbMTType.SelectedIndexChanged += new System.EventHandler(this.cmbMTType_SelectedIndexChanged);
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            neuSpread1.CellDoubleClick+=new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);
            #endregion
            MTTypeList = constMgr.GetList("MTType"); 
        }
        #region 属性
        /// <summary>
        /// 当前医技项目
        /// </summary>
        public FS.HISFC.Models.Base.Const MedTechItem { get; set; }

        /// <summary>
        /// 当前医技类型
        /// </summary>
        public FS.HISFC.Models.Base.Const MedTechType { get; set; }

        /// <summary>
        /// 申请项目
        /// </summary>
        public FS.HISFC.Models.Order.Order MedTechOrder { get { return medTechOrder; }
            set
            {
                medTechOrder = value;
                currentOrderMedTechType = unDrugMgr.GetUndrugMTType(medTechOrder.Item.ID);
            }
        }

        private FS.HISFC.Models.Order.Order medTechOrder = null;

        private FS.HISFC.Models.Base.Const currentOrderMedTechType = null;
        /// <summary>
        /// 预约类型(门诊/住院)
        /// </summary>
        public FS.HISFC.Models.MedicalTechnology.EnumApplyType ApplyType { set; get; }

        public delegate void ApplyEvent(FS.HISFC.Models.MedicalTechnology.Arrange arrange, FS.HISFC.Models.Order.Order order);

        /// <summary>
        /// 双击Farpoint引起的事件
        /// </summary>
        public ApplyEvent ApplyMedticalTechnology { get; set; }


        private ArrayList MTTypeList = null;
        #endregion

        #region 列
        enum cols
        {
            /// <summary>
            /// ID
            /// </summary>
            ID = 0,
            /// <summary>
            /// 项目代码
            /// </summary>
            ItemCode,
            /// <summary>
            /// 项目名称
            /// </summary>
            ItemName,
            /// <summary>
            /// 类型代码
            /// </summary>
            TypeCode,
            /// <summary>
            /// 类型名称
            /// </summary>
            TypeName,
            /// <summary>
            /// 医生工号
            /// </summary>
            DoctCode,
            /// <summary>
            /// 医生姓名
            /// </summary>
            DoctName,
            /// <summary>
            /// 可用限额
            /// </summary>
            BookLimit,
            /// <summary>
            /// 日期
            /// </summary>
            Date,
            /// <summary>
            /// 星期
            /// </summary>
            Week,
            /// <summary>
            /// 开始时间
            /// </summary>
            BeginTime,
            /// <summary>
            /// 结束时间
            /// </summary>
            EndTime,
            /// <summary>
            /// 停诊原因
            /// </summary>
            StopReason
        }
        #endregion

        #region 域
        private DataTable dsItems;
        private DataView dv;
        List<FS.HISFC.Models.MedicalTechnology.Arrange> arrangeList = new List<FS.HISFC.Models.MedicalTechnology.Arrange>();
        /// <summary>
        /// 排班管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalTechnology.Arrange arrangeMgr = new FS.HISFC.BizLogic.MedicalTechnology.Arrange();
        /// <summary>
        /// 常数管理类(获取医技列表和医技类型)
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        FS.SOC.HISFC.Fee.BizLogic.Undrug unDrugMgr = new FS.SOC.HISFC.Fee.BizLogic.Undrug();

        #endregion

        #region 初始化
        private void ucMTList_Load(object sender, EventArgs e)
        {
            //if ((FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).CurrentGroup.Name.Contains("门诊"))
            //    ApplyType = FS.HISFC.Models.MedicalTechnology.EnumApplyType.Clinic;
            //else
            //    ApplyType = FS.HISFC.Models.MedicalTechnology.EnumApplyType.Hospital;
            InitTitle();
            InitDataSet();
            InitColor();
            InitMTType();
            Query();
        }
        private void InitMTType()
        {
            ArrayList mt = new ArrayList();
            FS.HISFC.Models.Base.Const All=new FS.HISFC.Models.Base.Const();
            All.ID="ALL";
            All.Name="全部";
            All.SpellCode="QB";
            All.WBCode="WU";
            All.SortID=0;
            All.IsValid=true;
            mt.Add(All);
            if (MTTypeList == null)
            {
                MessageBox.Show("获取[医技类型]时出错!" + constMgr.Err, "提示");
                return;
            }
            foreach (FS.HISFC.Models.Base.Const c in MTTypeList)
            {
                if (c.Memo == MedTechItem.ID)
                    mt.Add(c);
            }
            cmbMTType.AddItems(mt);
            cmbMTType.SelectedIndex = 0;
        }
        private void InitTitle()
        {
            lbOrder.Text = MedTechOrder.Item.Name;
            lbOrder.Location = new Point((neuPanel1.Width - lbOrder.Width) / 2, lbOrder.Location.Y);
        }
        private void InitDataSet()
        {
            dsItems = new DataTable("Templet");

            dsItems.Columns.AddRange(new DataColumn[]
			{
				new DataColumn(cols.ID.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.ItemCode.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ItemName.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.TypeCode.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.TypeName.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.DoctCode.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.DoctName.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.BookLimit.ToString(),System.Type.GetType("System.Decimal")),
				new DataColumn(cols.Date.ToString(),System.Type.GetType("System.DateTime")),
				new DataColumn(cols.Week.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.BeginTime.ToString(),System.Type.GetType("System.DateTime")),
				new DataColumn(cols.EndTime.ToString(),System.Type.GetType("System.DateTime")),
				new DataColumn(cols.StopReason.ToString(),System.Type.GetType("System.String"))
			});
        }

        private void InitColor()
        {
            pbOut.BackColor = Color.Silver;
            pbZero.BackColor = Color.Pink;
            pbStop.BackColor = Color.Yellow;
        }
        #endregion

        #region Farpoint设置
        /// <summary>
        /// 设置Farpoint显示格式
        /// </summary>
        private void SetFpFormat()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numbCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numbCellType.DecimalPlaces = 0;
            numbCellType.MaximumValue = 9999;
            numbCellType.MinimumValue = 0;

            FarPoint.Win.Spread.CellType.DateTimeCellType dtCellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            dtCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dtCellType.UserDefinedFormat = "HH:mm";

            this.neuSpread1_Sheet1.Columns[(int)cols.ID].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)cols.ItemCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)cols.TypeCode].Visible = false;
            this.neuSpread1_Sheet1.Columns[(int)cols.DoctCode].Visible = false;

            this.neuSpread1_Sheet1.Columns[(int)cols.BookLimit].CellType = numbCellType;
            this.neuSpread1_Sheet1.Columns[(int)cols.BookLimit].ForeColor = Color.Red;
            this.neuSpread1_Sheet1.Columns[(int)cols.BookLimit].Font = new Font("宋体", 9, FontStyle.Bold);

            this.neuSpread1_Sheet1.Columns[(int)cols.BeginTime].CellType = dtCellType;
            this.neuSpread1_Sheet1.Columns[(int)cols.EndTime].CellType = dtCellType;

            this.neuSpread1_Sheet1.Columns[(int)cols.ItemName].Width = 60F;
            this.neuSpread1_Sheet1.Columns[(int)cols.TypeName].Width = 100F;
            this.neuSpread1_Sheet1.Columns[(int)cols.DoctName].Width = 75F;
            this.neuSpread1_Sheet1.Columns[(int)cols.BookLimit].Width = 40F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Date].Width = 75F;
            this.neuSpread1_Sheet1.Columns[(int)cols.Week].Width = 50F;

            this.neuSpread1_Sheet1.Columns[(int)cols.BeginTime].Width = 55F;
            this.neuSpread1_Sheet1.Columns[(int)cols.EndTime].Width = 55F;
            this.neuSpread1_Sheet1.Columns[(int)cols.StopReason].Width = 125F;

            DisplayColor();
        }
        /// <summary>
        /// 过期项目不能选择
        /// </summary>
        private void DisplayColor()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                //显示行状态
                //过期项目显示为灰(银)色
                if (arrangeList[i].EndTime < DateTime.Now)
                {
                    int couCnt = this.neuSpread1_Sheet1.ColumnCount;
                    for (int j = 0; j < couCnt; j++)
                    {
                        this.neuSpread1_Sheet1.Cells[i, j].BackColor = pbOut.BackColor;
                    }
                    this.neuSpread1_Sheet1.Rows[i].Locked = true;
                    continue;
                }
                //限额没有了为粉红色
                if ((ApplyType == Models.MedicalTechnology.EnumApplyType.Clinic && arrangeList[i].BookLimit - arrangeList[i].BookNum < 1)
                    || (ApplyType == Models.MedicalTechnology.EnumApplyType.Hospital && arrangeList[i].HostLimit - arrangeList[i].HostNum < 1))
                {
                    int couCnt = this.neuSpread1_Sheet1.ColumnCount;
                    for (int j = 0; j < couCnt; j++)
                    {
                        this.neuSpread1_Sheet1.Cells[i, j].BackColor = pbZero.BackColor;
                    }
                    this.neuSpread1_Sheet1.Rows[i].Locked = true;
                    continue;
                }
                //停班了
                if (arrangeList[i].IsStop == true)
                {
                    int couCnt = this.neuSpread1_Sheet1.ColumnCount;
                    for (int j = 0; j < couCnt; j++)
                    {
                        this.neuSpread1_Sheet1.Cells[i, j].BackColor = pbStop.BackColor;
                    }
                    this.neuSpread1_Sheet1.Rows[i].Locked = true;
                    continue;
                }

                for (int j = 0; j < this.neuSpread1_Sheet1.ColumnCount; j++)
                {
                    this.neuSpread1_Sheet1.Cells[i, j].BackColor = SystemColors.Window;

                }

            }
        }

        #endregion

        #region 事件
        private void dtpSearchDate_ValueChanged(object sender, EventArgs e)
        {
            Query();
        }
        private void cmbMTType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.MedTechType = cmbMTType.SelectedItem as FS.HISFC.Models.Base.Const;
            Query();
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            dtpSearchDate.Value = dtpSearchDate.Value.AddDays(-1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            dtpSearchDate.Value = dtpSearchDate.Value.AddDays(1);
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.Cells[e.Row, e.Column].BackColor != SystemColors.Window)
            {
                MessageBox.Show("不能预约该排班");
                return;
            }
            FS.HISFC.Models.MedicalTechnology.Arrange arrange= arrangeMgr.GetByID(neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRowIndex,(int)cols.ID].Text);

            //如果当前项目的医技类型不为空,则检查一下预约类型与当前医嘱类型是否一致,不一致则不给预约
            if (currentOrderMedTechType != null && !string.IsNullOrEmpty(currentOrderMedTechType.ID))
            {
                if (!arrange.TypeCode.Equals(currentOrderMedTechType.ID))
                {
                    MessageBox.Show("当前医嘱类型为【" + currentOrderMedTechType.Name + "】,与排班类型不一致,不能预约该排班!");
                    return;
                }
            }
            if (ApplyMedticalTechnology != null)
            {
                ApplyMedticalTechnology(arrange, MedTechOrder);
            }
            //如果是弹窗出来的,顺便关了吧...
            if (this.Parent is FS.FrameWork.WinForms.Forms.BaseForm)
            {
                (this.Parent as FS.FrameWork.WinForms.Forms.BaseForm).Close();
            }
        }

        #endregion

        #region 查询
        /// <summary>
        /// 
        /// </summary>
        public void Query()
        {
            string ItemCode = "ALL";
            string TypeCode = "ALL";
            if (MedTechItem != null) ItemCode = MedTechItem.ID;
            if (MedTechType != null) TypeCode = MedTechType.ID;
            if (currentOrderMedTechType != null && !string.IsNullOrEmpty(currentOrderMedTechType.ID) && TypeCode.Equals("ALL")) TypeCode = currentOrderMedTechType.ID;

            arrangeList = this.arrangeMgr.GetArrange(dtpSearchDate.Value, ItemCode, TypeCode);
            if (arrangeList == null)
            {
                MessageBox.Show("查询排班信息出错!" + this.arrangeMgr.Err, "提示");
                return;
            }

            dsItems.Rows.Clear();

            try
            {
                arrangeList.Select(ar =>
                {
                    int limit = 0;
                    if (ApplyType == Models.MedicalTechnology.EnumApplyType.Clinic)
                        limit = ar.BookLimit - ar.BookNum;
                    else limit = ar.HostLimit - ar.HostNum;
                    return new { ar.ID, ar.ItemCode, ar.ItemName, ar.TypeCode, ar.TypeName, ar.DoctCode, ar.DoctName, limit, ar.SeeDate, ar.BeginTime, ar.EndTime, ar.StopReason };
                }).ToList().ForEach(ar =>
                {
                    dsItems.Rows.Add(new object[]
                            {
                                ar.ID,
                                ar.ItemCode,
                                ar.ItemName,
                                ar.TypeCode,
                                ar.TypeName,
                                ar.DoctCode,
                                ar.DoctName,
                                ar.limit,
                                ar.SeeDate,
                                ar.SeeDate.ToString("dddd"),
                                ar.BeginTime,
                                ar.EndTime,
                                ar.StopReason
                            });
                });
            }
            catch (Exception e)
            {
                MessageBox.Show("查询排班信息生成DataSet时出错!" + e.Message, "提示");
                return;
            }
            dsItems.AcceptChanges();

            dv = dsItems.DefaultView;
            //if (this.neuSpread1_Sheet1.Rows.Count > 0)
            //    this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
            this.neuSpread1_Sheet1.DataSource = dv;

            this.SetFpFormat();
        }
        #endregion

    }
}
