using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Xml;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FS.WinForms.Controls;
using System.Collections.Generic;

namespace FS.HISFC.Components.MTOrder
{
    /// <summary>
    /// �Ű�ά��
    /// </summary>
    public partial class ucSchema : UserControl
    {
        public ucSchema()
        {
            InitializeComponent();

            this.fpSpread1.Change += new ChangeEventHandler(fpSpread1_Change);
            this.fpSpread1.EditModeOff += new EventHandler(fpSpread1_EditModeOff);
            this.fpSpread1.EditModeOn += new System.EventHandler(this.fpSpread1_EditModeOn);
            this.fpSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_EditChange);
			
        }

        #region ��
        //--------------------------------------------------------------------
        /// <summary>
        /// ģ�弯��
        /// </summary>
        private DataTable dsItems;
        private DataView dv;
        private List<FS.HISFC.Models.Base.Const> MTTypeList = new List<FS.HISFC.Models.Base.Const>();

        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        private DateTime dtNow;
        #region ������
        /// <summary>
        /// ҽ���Ű������
        /// </summary>
        private FS.HISFC.BizLogic.MedicalTechnology.Arrange arrangeMgr = new FS.HISFC.BizLogic.MedicalTechnology.Arrange();
        /// <summary>
        /// ����������(��ȡҽ���б��ҽ������)
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// ���ҹ�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager doctMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ�����������ʾ����
        /// </summary>
        public bool IsCheckChangceAndSave { get; set; }
        /// <summary>
        /// �Ƿ���ʾID��
        /// </summary>
        public bool IsShowID { get; set; }
        /// <summary>
        /// ͣ����ɫ
        /// </summary>
        public Color StopColor { get; set; }

        /// <summary>
        /// �����Ű���ɫ
        /// </summary>
        public Color ExpiredColor { get; set; }

        /// <summary>
        /// �޶�Ϊ0����ɫ
        /// </summary>
        public Color OutColor { get; set; }
        /// <summary>
        /// �����Ű��Ƿ�����(������༭)
        /// </summary>
        public bool IsLockExpired { get; set; }

        /// <summary>
        /// ��ǰҽ����Ŀ
        /// </summary>
        public FS.HISFC.Models.Base.Const MedTechType { get; set; }

        private DateTime seeDate = DateTime.MinValue;
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime SeeDate
        {
            get { return seeDate; }
            set { seeDate = value.Date; }
        }
        #endregion

        #region �б�
        /// <summary>
        /// ҽ����Ŀ�б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbTech = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// ҽ���б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbDoct = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// ҽ����λ�б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbTechType = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        #endregion

        #endregion

        #region ��
        protected enum cols
        {
            /// <summary>
            /// ���
            /// </summary>
            ID=0,
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ItemCode,
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ItemName,
            /// <summary>
            /// ҽ����λ����
            /// </summary>
            TypeCode,
            /// <summary>
            /// ҽ����λ����
            /// </summary>
            TypeName,
            /// <summary>
            /// ҽ����Ա
            /// </summary>
            DoctCode,
            /// <summary>
            /// ҽ������
            /// </summary>
            DoctName,
            /// <summary>
            /// ����ԤԼ�޶�
            /// </summary>
            BookLimit,
            /// <summary>
            /// סԺԤԼ�޶�
            /// </summary>
            HostLimit,
            /// <summary>
            /// ������ԤԼ��
            /// </summary>
            BookNum,
            /// <summary>
            /// סԺ��ԤԼ��
            /// </summary>
            HostNum,
            /// <summary>
            /// ��ʼʱ��
            /// </summary>
            BeginTime,
            /// <summary>
            /// ����ʱ��
            /// </summary>
            EndTime,
            /// <summary>
            /// �Ƿ�ͣ��
            /// </summary>
            IsStop,
            /// <summary>
            /// ͣ��ԭ��
            /// </summary>
            StopReason,
            /// <summary>
            /// ͣ����ID
            /// </summary>
            StopOper,
            /// <summary>
            /// ͣ������
            /// </summary>
            StopDate
        }
        #endregion

        #region ��ʼ��
        /// <summary>
        ///  ��ʼ��
        /// </summary>
        /// <param name="basevar"></param>
        /// <param name="w"></param>
        /// <param name="type"></param>
        public void Init(DateTime seeDate)
        {
            this.SeeDate = seeDate.Date;

            this.initDataSet();
            this.initTech();
            initTechType();
            initDoct();

            this.visible(false);
            this.initFp();
            SetFpFormat();
        }
        /// <summary>
        /// Init DataSet
        /// </summary>
        private void initDataSet()
        {

            dsItems = new DataTable("Schema");
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
				new DataColumn(cols.HostLimit.ToString(),System.Type.GetType("System.Decimal")),
				new DataColumn(cols.BookNum.ToString(),System.Type.GetType("System.Decimal")),
				new DataColumn(cols.HostNum.ToString(),System.Type.GetType("System.Decimal")),
				new DataColumn(cols.BeginTime.ToString(),System.Type.GetType("System.DateTime")),
				new DataColumn(cols.EndTime.ToString(),System.Type.GetType("System.DateTime")),
				new DataColumn(cols.IsStop.ToString(),System.Type.GetType("System.Boolean")),
				new DataColumn(cols.StopReason.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.StopOper.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.StopDate.ToString(),System.Type.GetType("System.DateTime"))
			});

        }
        /// <summary>
        /// ��ʼ��farpoint
        /// </summary>
        private void initFp()
        {
            InputMap im;

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        /// <summary>
        /// ��ʼ��ҽ���б�
        /// </summary>
        private void initTech()
        {
            this.lbTech.ItemSelected += new EventHandler(lbTech_SelectItem);
            this.groupBox1.Controls.Add(this.lbTech);
            this.lbTech.BackColor = this.label1.BackColor;
            this.lbTech.Font = new Font("����", 11F);
            this.lbTech.BorderStyle = BorderStyle.None;
            this.lbTech.Cursor = Cursors.Hand;
            this.lbTech.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbTech.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);
            //�õ�ҽ���б�
            ArrayList al = constMgr.GetList("MT#MINFEE");

            if (al == null)
            {
                MessageBox.Show("��ȡ[ҽ����Ϣ�б�]ʱ����!" + constMgr.Err, "��ʾ");
                return;
            }

            this.lbTech.AddItems(al);
        }
        /// <summary>
        /// ��ʼ��ҽ����λ�б�
        /// </summary>
        private void initTechType()
        {
            //�õ�ҽ���б�
            ArrayList al = constMgr.GetList("MTType");

            if (al == null)
            {
                MessageBox.Show("��ȡ[ҽ������]ʱ����!" + constMgr.Err, "��ʾ");
                return;
            }

            foreach (FS.HISFC.Models.Base.Const c in al)
            {
                MTTypeList.Add(c);
            }
        }
        private void initTechType(string ItemCode)
        {
            groupBox1.Controls.Remove(lbTechType);
            this.lbTechType = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
            this.lbTechType.ItemSelected += new EventHandler(lbTechType_SelectItem);
            this.groupBox1.Controls.Add(this.lbTechType);
            this.lbTechType.BackColor = this.label1.BackColor;
            this.lbTechType.Font = new Font("����", 11F);
            this.lbTechType.BorderStyle = BorderStyle.None;
            this.lbTechType.Cursor = Cursors.Hand;
            this.lbTechType.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbTechType.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            ArrayList al = new ArrayList();
            if (!string.IsNullOrEmpty(ItemCode))
                MTTypeList.Where(t => t.Memo == ItemCode).ToList().ForEach(t => al.Add(t));
            else
                MTTypeList.ForEach(t => al.Add(t));

            this.lbTechType.AddItems(al);
        }
        /// <summary>
        /// ��ʼ������ҽ��
        /// </summary>
        private void initDoct()
        {
            this.lbDoct.ItemSelected += new EventHandler(lbDoct_SelectItem);
            this.groupBox1.Controls.Add(this.lbDoct);
            this.lbDoct.BackColor = this.label1.BackColor;
            this.lbDoct.Font = new Font("����", 11F);
            this.lbDoct.BorderStyle = BorderStyle.None;
            this.lbDoct.Cursor = Cursors.Hand;
            this.lbDoct.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbDoct.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            //�õ�ҽ���б�
            ArrayList al = doctMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al == null)
            {
                MessageBox.Show("��ȡ[ҽ���б�]ʱ����!" + doctMgr.Err, "��ʾ");
                return;
            }

            this.lbDoct.AddItems(al);
            this.lbDoct.BringToFront();
        }
        #endregion


        #region ����
        public void Query()
        {
            Query(MedTechType);
        }
        /// <summary>
        /// ��ѯһ��ĳ��������Ű��¼
        /// </summary>
        /// <param name="deptID"></param>
        public void Query(FS.HISFC.Models.Base.Const medTectType)
        {
            this.MedTechType = medTectType;
            string ItemCode = "ALL";
            if (medTectType != null)
                ItemCode = medTectType.ID;
            List<FS.HISFC.Models.MedicalTechnology.Arrange> arrangeList = this.arrangeMgr.Query(this.SeeDate, ItemCode);
            if (arrangeList == null)
            {
                MessageBox.Show("��ѯ�Ű���Ϣ����!" + this.arrangeMgr.Err, "��ʾ");
                return;
            }

            dsItems.Rows.Clear();

            try
            {
                arrangeList.ForEach(ar =>
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
                                ar.BookLimit,
                                ar.HostLimit,
                                ar.BookNum,
                                ar.HostNum,
                                ar.BeginTime,
                                ar.EndTime,
                                ar.IsStop,
                                ar.StopReason,
                                ar.StopOper,
                                ar.StopDate
                            });
                    });
            }
            catch (Exception e)
            {
                MessageBox.Show("��ѯ�Ű���Ϣ����DataSetʱ����!" + e.Message, "��ʾ");
                return;
            }
            dsItems.AcceptChanges();

            dv = dsItems.DefaultView;
            //if (this.fpSpread1_Sheet1.Rows.Count > 0)
            //    this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);
            this.fpSpread1_Sheet1.DataSource = dv;

            this.SetFpFormat();

            this.DisplayColor();

        }
        #region ��ֹ�ظ���ʾ

        ///// <summary>
        ///// ��ֹ�ظ���ʾ
        ///// </summary>
        //private void Span()
        //{
        //    ///
        //    int colLastDept = 0;
        //    int colLastDoct = 0;
        //    int colLastNoon = 0;
        //    int rowCnt = this.fpSpread1_Sheet1.RowCount;

        //    FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
        //    txtCellType.ReadOnly = true;

        //    for (int i = 0; i < rowCnt; i++)
        //    {

        //        if (i > 0 && this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName))
        //        {
        //            if (i - colLastDept > 1)
        //            {
        //                this.fpSpread1_Sheet1.Models.Span.Add(colLastDept, (int)cols.DeptName, i - colLastDept, 1);
        //            }

        //            colLastDept = i;
        //        }

        //        //���һ�д���
        //        if (i > 0 && i == rowCnt - 1 && this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName))
        //        {
        //            this.fpSpread1_Sheet1.Models.Span.Add(colLastDept, (int)cols.DeptName, i - colLastDept + 1, 1);
        //        }

        //        ///ҽ��
        //        ///
        //        if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Doct&&
        //            i > 0 &&
        //            this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName))
        //        {
        //            if (i - colLastDoct > 1)
        //            {
        //                this.fpSpread1_Sheet1.Models.Span.Add(colLastDoct, (int)cols.DoctName, i - colLastDoct, 1);
        //            }
        //            colLastDoct = i;
        //        }

        //        //���һ��
        //        if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Doct &&
        //            i > 0 &&
        //            i == rowCnt - 1 && this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName))
        //        {
        //            this.fpSpread1_Sheet1.Models.Span.Add(colLastDoct, (int)cols.DoctName, i - colLastDoct + 1, 1);
        //        }

        //        ///���
        //        ///
        //        if (i > 0 &&
        //            (this.fpSpread1_Sheet1.GetText(i, (int)cols.Noon) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.Noon) ||
        //             this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName) ||
        //             this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName)))
        //        {
        //            if (i - colLastNoon > 1)
        //            {
        //                this.fpSpread1_Sheet1.Models.Span.Add(colLastNoon, (int)cols.Noon, i - colLastNoon, 1);
        //            }
        //            colLastNoon = i;
        //        }
        //        //���һ��
        //        if (i > 0 && i == rowCnt - 1 &&
        //            (this.fpSpread1_Sheet1.GetText(i, (int)cols.Noon) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.Noon) ||
        //            this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName) ||
        //            this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName)))
        //        {
        //            this.fpSpread1_Sheet1.Models.Span.Add(colLastNoon, (int)cols.Noon, i - colLastNoon + 1, 1);
        //        }

        //    }
        //}

        #endregion

        
        /// <summary>
        /// ��ʾ��ɫ,���ڲ�����༭
        /// </summary>
        private void DisplayColor()
        {
            dtNow = arrangeMgr.GetDateTimeFromSysDateTime();

            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                showColor(i);
            }
        }
        /// <summary>
        /// ��������ʾ��ɫ
        /// </summary>
        /// <param name="row"></param>
        private void showColor(int row)
        {
            DateTime endTime = DateTime.Parse(this.fpSpread1_Sheet1.Cells[row, (int)cols.EndTime].Value.ToString());
            if (endTime < dtNow)
            {
                for (int j = 0; j < this.fpSpread1_Sheet1.ColumnCount; j++)
                {
                    //�����Ƿ�����
                    if (IsLockExpired)
                        this.fpSpread1_Sheet1.Cells[row, j].Locked = true;

                    this.fpSpread1_Sheet1.Cells[row, j].BackColor = ExpiredColor;
                }
                return;
            }
            //�޶�û����
            if ((int.Parse(this.fpSpread1_Sheet1.GetText(row, (int)cols.BookLimit)) - int.Parse(this.fpSpread1_Sheet1.GetText(row, (int)cols.BookNum)) < 1)
                && (int.Parse(this.fpSpread1_Sheet1.GetText(row, (int)cols.HostLimit)) - int.Parse(this.fpSpread1_Sheet1.GetText(row, (int)cols.HostNum)) < 1))
            {
                for (int j = 0; j < this.fpSpread1_Sheet1.ColumnCount; j++)
                {
                    this.fpSpread1_Sheet1.Cells[row, j].BackColor = OutColor;

                }
                return;
            }

            //ͣ����
            if (this.fpSpread1_Sheet1.GetText(row, (int)cols.IsStop).ToUpper() == "TRUE")
            {
                for (int j = 0; j < this.fpSpread1_Sheet1.ColumnCount; j++)
                {
                    this.fpSpread1_Sheet1.Cells[row, j].BackColor = StopColor;

                }
                return;
            }

            for (int j = 0; j < this.fpSpread1_Sheet1.ColumnCount; j++)
            {
                this.fpSpread1_Sheet1.Cells[row, j].BackColor = SystemColors.Window;
            }
            this.fpSpread1_Sheet1.Cells[row, (int)cols.BeginTime].BackColor = Color.MistyRose;
            this.fpSpread1_Sheet1.Cells[row, (int)cols.EndTime].BackColor = Color.MistyRose;
        }
        /// <summary>
        /// ����Farpoint��ʾ��ʽ
        /// </summary>
        private void SetFpFormat()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numbCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numbCellType.DecimalPlaces = 0;
            numbCellType.MaximumValue = 9999;
            numbCellType.MinimumValue = 0;

            FarPoint.Win.Spread.CellType.NumberCellType numReadonlyCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numReadonlyCellType.DecimalPlaces = 0;
            numReadonlyCellType.ReadOnly = true;

            FarPoint.Win.Spread.CellType.DateTimeCellType dtCellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            dtCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dtCellType.UserDefinedFormat = "HH:mm";

            this.fpSpread1_Sheet1.Columns[(int)cols.ID].Visible = IsShowID;
            this.fpSpread1_Sheet1.Columns[(int)cols.ID].CellType = new FarPoint.Win.Spread.CellType.TextCellType() { ReadOnly = true };

            this.fpSpread1_Sheet1.Columns[(int)cols.ItemCode].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)cols.TypeCode].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)cols.DoctCode].Visible = false;            

            //û�취,Ҫ��ѹ���ظ���ʾ��Ͳ����������޸��ˣ�������
            //if (!this.IsShowDoctType)
            //{
            //    if (this.fpSpread1_Sheet1.RowCount > 0)
            //    {
            //        this.fpSpread1_Sheet1.Cells[0, (int)cols.DeptName, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.DeptName].CellType = txtCellType;
            //        this.fpSpread1_Sheet1.Cells[0, (int)cols.Noon, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.Noon].CellType = txtCellType;
            //    }
            //}

            this.fpSpread1_Sheet1.Columns[(int)cols.BookLimit].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[(int)cols.BookLimit].ForeColor = Color.Red;
            this.fpSpread1_Sheet1.Columns[(int)cols.BookLimit].Font = new Font("����", 9, FontStyle.Bold);
            this.fpSpread1_Sheet1.Columns[(int)cols.HostLimit].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[(int)cols.HostLimit].ForeColor = Color.Purple;
            this.fpSpread1_Sheet1.Columns[(int)cols.HostLimit].Font = new Font("����", 9, FontStyle.Bold);
            this.fpSpread1_Sheet1.Columns[(int)cols.BookNum].CellType = numReadonlyCellType;
            this.fpSpread1_Sheet1.Columns[(int)cols.BookNum].ForeColor = Color.Red;
            this.fpSpread1_Sheet1.Columns[(int)cols.BookNum].Font = new Font("����", 9, FontStyle.Bold);
            this.fpSpread1_Sheet1.Columns[(int)cols.HostNum].CellType = numReadonlyCellType;
            this.fpSpread1_Sheet1.Columns[(int)cols.HostNum].ForeColor = Color.Purple;
            this.fpSpread1_Sheet1.Columns[(int)cols.HostNum].Font = new Font("����", 9, FontStyle.Bold);

            this.fpSpread1_Sheet1.Columns[(int)cols.BeginTime].CellType = dtCellType;
            this.fpSpread1_Sheet1.Columns[(int)cols.EndTime].CellType = dtCellType;

            this.fpSpread1_Sheet1.Columns[(int)cols.ItemName].Width = 80F;
            this.fpSpread1_Sheet1.Columns[(int)cols.TypeName].Width = 220F;
            this.fpSpread1_Sheet1.Columns[(int)cols.DoctName].Width = 100F;
            this.fpSpread1_Sheet1.Columns[(int)cols.HostLimit].Width = 50F;
            this.fpSpread1_Sheet1.Columns[(int)cols.BookLimit].Width = 50F;
            this.fpSpread1_Sheet1.Columns[(int)cols.BookNum].Width = 50F;
            this.fpSpread1_Sheet1.Columns[(int)cols.HostNum].Width = 50F;

            this.fpSpread1_Sheet1.Columns[(int)cols.BeginTime].Width = 50F;
            this.fpSpread1_Sheet1.Columns[(int)cols.EndTime].Width = 50F;

            this.fpSpread1_Sheet1.Columns[(int)cols.IsStop].Width = 40F;
            this.fpSpread1_Sheet1.Columns[(int)cols.StopReason].Width = 100F;

            this.fpSpread1_Sheet1.Columns[(int)cols.StopOper].Width = 70F;
            this.fpSpread1_Sheet1.Columns[(int)cols.StopOper].CellType = new FarPoint.Win.Spread.CellType.TextCellType() { ReadOnly = true };
            this.fpSpread1_Sheet1.Columns[(int)cols.StopDate].Width = 120F;
            this.fpSpread1_Sheet1.Columns[(int)cols.StopDate].CellType = new FarPoint.Win.Spread.CellType.DateTimeCellType() { ReadOnly = true };


        }
        /// <summary>
        /// 
        /// </summary>
        public void Add()
        {
            if (dsItems.GetChanges(DataRowState.Deleted) != null)
            {
                if (MessageBox.Show("�����Ѿ������仯,��[ȷ��]�������ϴβ���!\n�Ƿ���� ?", "��ʾ ", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    Save();
                }
            }
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
            this.fpSpread1_Sheet1.ActiveRowIndex = this.fpSpread1_Sheet1.RowCount - 1;
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            this.fpSpread1_Sheet1.SetValue(row, (int)cols.ID, arrangeMgr.GetSequence("MedicalTechnology.Arrange.ID"), false);

            //���ҽ�����Ͳ�Ϊ��(ѡ�������ڵ�)
            if (MedTechType != null)
            {
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.ItemCode, MedTechType.ID, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.ItemName, MedTechType.Name, false);
            }
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.IsStop, false, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.BookLimit, 0, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.HostLimit, 0, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.BookNum, 0, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.HostNum, 0, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.BeginTime, DateTime.Now, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.EndTime, DateTime.Now, false);


            this.fpSpread1.Focus();

            string ItemCode = this.fpSpread1_Sheet1.GetText(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.ItemCode);


            if (string.IsNullOrEmpty(ItemCode))
            {
                this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.ItemName, false);
            }
            else
            {
                this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BookLimit, false);
            }

        }
        /// <summary>
        /// ���(���ڸ���ģ��)
        /// </summary>
        /// <param name="templet"></param>
        public void Add(FS.HISFC.Models.MedicalTechnology.Templet templet)
        {
            DataRowView dr = dv.AddNew();

            dr[cols.ID.ToString()] = this.arrangeMgr.GetSequence("MedicalTechnology.Arrange.ID");
            dr[cols.ItemCode.ToString()] = templet.ItemCode;
            dr[cols.ItemName.ToString()] = templet.ItemName;
            dr[cols.TypeCode.ToString()] = templet.TypeCode;
            dr[cols.TypeName.ToString()] = templet.TypeName;
            dr[cols.DoctCode.ToString()] = templet.DoctCode;
            dr[cols.DoctName.ToString()] = templet.DoctName;
            dr[cols.BookLimit.ToString()] = templet.BookLimit;
            dr[cols.HostLimit.ToString()] = templet.HostLimit;
            dr[cols.BookNum.ToString()] = 0;
            dr[cols.HostNum.ToString()] =0;
            dr[cols.BeginTime.ToString()] = templet.BeginTime;
            dr[cols.EndTime.ToString()] = templet.EndTime;
            dr[cols.IsStop.ToString()] = templet.IsStop;
            dr[cols.StopReason.ToString()] = templet.StopReason;
            dr[cols.StopOper.ToString()] = string.Empty;
            dr[cols.StopDate.ToString()] = DateTime.MinValue;

            dr.EndEdit();
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        public void Del()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (row < 0 || this.fpSpread1_Sheet1.RowCount == 0) return;

            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ƿ�ɾ�������Ű���Ϣ"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                this.fpSpread1.Focus();
                return;
            }

            this.fpSpread1.StopCellEditing();

            if (DateTime.Parse(this.seeDate.ToString("yyyy-MM-dd ") + this.fpSpread1_Sheet1.Cells[row, (int)cols.EndTime].Text + ":00") <= DateTime.Now.AddMinutes(-30))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���Ű���Ϣ�Ѿ�����,����ɾ��"), "��ʾ");
                return;
            }

            if (decimal.Parse(this.fpSpread1_Sheet1.Cells[row, (int)cols.BookNum].Text) > 0 &&
                this.fpSpread1_Sheet1.Cells[row, (int)cols.IsStop].Text.ToUpper() == "FALSE")
            {
                DialogResult dr = MessageBox.Show(FS.FrameWork.Management.Language.Msg("���Ű���Ϣ�Ѿ�ʹ��,����ɾ��,�Ƿ�ͣ��?"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No) return;
                else
                {
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.IsStop, true);
                    Save();
                    return;
                }
            }

            if (this.ValidByIDForUpdate(this.fpSpread1_Sheet1.Cells[row,(int)cols.ID].Text) < 0)
            {
                return;
            }

            this.fpSpread1_Sheet1.Rows.Remove(row, 1);

            this.fpSpread1.Focus();
        }
        /// <summary>
        /// ɾ��ȫ��
        /// </summary>
        public void DelAll()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0) return;

            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ƿ�ɾ��ȫ���Ű�"), "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                this.fpSpread1.Focus();
                return;
            }

            for (int i = 0; i < fpSpread1_Sheet1.RowCount; i++)
            {
                //{8FCEF5D9-FC8B-493c-8EE6-C00E67A02C74}
                if (DateTime.Parse(this.seeDate.ToString("yyyy-MM-dd ") + this.fpSpread1_Sheet1.Cells[i, (int)cols.EndTime].Text + ":00") <= DateTime.Now)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���Ű���Ϣ�Ѿ�����,����ɾ��"), "��ʾ");
                    return;
                }
                if (this.fpSpread1_Sheet1.GetText(i, (int)cols.HostNum) != "0" ||
                    this.fpSpread1_Sheet1.GetText(i, (int)cols.BookNum) != "0")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ű��Ѿ�ʹ��,����ɾ��,�кţ�") + (i + 1).ToString());
                    return;
                }
            }

            this.fpSpread1_Sheet1.Rows.Remove(0,fpSpread1_Sheet1.RowCount);
            this.fpSpread1.Focus();
        }

        ////========================

        //private DataTable SetTable(DataTable dt)
        //{
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dt.Rows[i]["DeptID"] = this.fpSpread1_Sheet1.Cells[i, (int)cols.DeptID].Text;
        //        dt.Rows[i]["Noon"] = this.fpSpread1_Sheet1.Cells[i, (int)cols.Noon].Text;
        //    }
        //    return dt;
        //}

        ///=======================

        /// <summary>
        /// ����
        /// </summary>
        public int Save()
        {
            //if (!this.isCheckChangceAndSave)
            //{
            //    return -1;
            //}

            this.fpSpread1.StopCellEditing();

            if (fpSpread1_Sheet1.RowCount > 0)
            {
                dsItems.Rows[fpSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }

            //����
            DataTable dtAdd = dsItems.GetChanges(DataRowState.Added);
            //�޸�
            DataTable dtModify = dsItems.GetChanges(DataRowState.Modified);
            //ɾ��
            DataTable dtDel = dsItems.GetChanges(DataRowState.Deleted);

            //dtAdd = this.SetTable(dtAdd);
            //��֤
            if (Valid(dtAdd) == -1) return -1;
            if (Valid(dtModify) == -1) return -1;
            //תΪʵ�弯��
            IList alAdd = this.GetChanges(dtAdd);
            if (alAdd == null) return -1;
            IList alModify = this.GetChanges(dtModify);
            if (alModify == null) return -1;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.arrangeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            string rtnText = "";

            if (dtDel != null)
            {
                dtDel.RejectChanges();

                if (this.SaveDel(this.arrangeMgr, dtDel, ref rtnText) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(rtnText, "��ʾ");
                    return -1;
                }
            }

            if (this.SaveAdd(this.arrangeMgr, alAdd, ref rtnText) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(rtnText, "��ʾ");
                return -1;
            }

            if (this.SaveModify(this.arrangeMgr, alModify, ref rtnText) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(rtnText, "��ʾ");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            dsItems.AcceptChanges();
            //�Ƚ���,ֱ�Ӷ����ݿ�ˢ����
            Query(this.MedTechType);
            SetFpFormat();
            DisplayColor();

            return 0;
        }
        /// <summary>
        /// �Ƿ��޸����ݣ�
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            if (!this.IsCheckChangceAndSave)
            {
                return false;
            }

            this.fpSpread1.StopCellEditing();

            if (fpSpread1_Sheet1.RowCount > 0)
            {
                dsItems.Rows[fpSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }
            
            DataTable dt = dsItems.GetChanges();

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// �������ӵļ�¼
        /// </summary>
        /// <param name="schMgr"></param>
        /// <param name="alAdd"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveAdd(FS.HISFC.BizLogic.MedicalTechnology.Arrange schMgr, IList alAdd, ref string Err)
        {
            try
            {
                foreach (FS.HISFC.Models.MedicalTechnology.Arrange schema in alAdd)
                {
                    if (schMgr.Insert(schema) == -1)
                    {
                        Err = schMgr.Err;
                        return -1;
                    }
                }

                #region HL7 ��Ϣ����
                //if (alAdd.Count > 0)
                //{

                //    string errInfo = "";
                //    int param = Function.SendBizMessage(alAdd, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Schema, ref errInfo);

                //    if (param == -1)
                //    {
                //        Err = FS.FrameWork.Management.Language.Msg("�Ű���Ϣ�޸�ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo);
                //        return -1;

                //    }
                //}
                #endregion
            }
            catch (Exception e)
            {
                Err = e.Message;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// �����޸ļ�¼
        /// </summary>
        /// <param name="templetMgr"></param>
        /// <param name="alModify"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveModify(FS.HISFC.BizLogic.MedicalTechnology.Arrange schMgr, IList alModify, ref string Err)
        {
            //int rtn = 0;
            try
            {
                foreach (FS.HISFC.Models.MedicalTechnology.Arrange arrange in alModify)
                {
                    if (arrange.IsStop == true)
                    {
                        arrange.StopOper = FS.FrameWork.Management.Connection.Operator.ID;
                        arrange.StopDate = DateTime.Now;
                    }
                    else
                    {
                        arrange.StopOper = string.Empty;
                        arrange.StopDate = DateTime.MinValue;
                    }
                    if (schMgr.Update(arrange) == -1)
                    {
                        Err = schMgr.Err;
                        return -1;
                    }

                }
            }
            catch (Exception e)
            {
                Err = e.Message;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// ����ɾ����¼
        /// </summary>
        /// <param name="schMgr"></param>
        /// <param name="dvDel"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveDel(FS.HISFC.BizLogic.MedicalTechnology.Arrange schMgr, DataTable dvDel, ref string Err)
        {
            try
            {
                foreach (DataRow row in dvDel.Rows)
                {

                    int rtn = schMgr.Delete(row[cols.ID.ToString()].ToString());
                    if (rtn == -1)
                    {
                        Err = schMgr.Err;
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                Err = e.Message;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// ����������ת��Ϊʵ�弯��
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private IList GetChanges(DataTable dt)
        {
            List<FS.HISFC.Models.MedicalTechnology.Arrange> arrangeList = new List<FS.HISFC.Models.MedicalTechnology.Arrange>();
            if (dt != null)
            {
                try
                {
                    DateTime current = this.arrangeMgr.GetDateTimeFromSysDateTime();

                    foreach (DataRow row in dt.Rows)
                    {
                        FS.HISFC.Models.MedicalTechnology.Arrange obj = new FS.HISFC.Models.MedicalTechnology.Arrange();

                        obj.ID = row[cols.ID.ToString()].ToString();

                        if (this.ValidByIDForUpdate(obj.ID) == -1)
                        {
                            return null;
                        }
                        obj.SeeDate = this.SeeDate;
                        obj.Week = this.SeeDate.DayOfWeek;
                        obj.ItemCode = row[cols.ItemCode.ToString()].ToString();
                        obj.ItemName = row[cols.ItemName.ToString()].ToString();
                        obj.TypeCode = row[cols.TypeCode.ToString()].ToString();
                        obj.TypeName = row[cols.TypeName.ToString()].ToString();
                        obj.DoctCode = row[cols.DoctCode.ToString()].ToString();
                        obj.DoctName = row[cols.DoctName.ToString()].ToString();
                        obj.BeginTime =DateTime.Parse( row[cols.BeginTime.ToString()].ToString());
                        obj.EndTime = DateTime.Parse(row[cols.EndTime.ToString()].ToString());
                        obj.BookLimit = FS.FrameWork.Function.NConvert.ToInt32(row[cols.BookLimit.ToString()].ToString());
                        obj.HostLimit = FS.FrameWork.Function.NConvert.ToInt32(row[cols.HostLimit.ToString()].ToString());
                        obj.BookNum = FS.FrameWork.Function.NConvert.ToInt32(row[cols.BookNum.ToString()].ToString());
                        obj.HostNum = FS.FrameWork.Function.NConvert.ToInt32(row[cols.HostNum.ToString()].ToString());
                        obj.IsStop = FS.FrameWork.Function.NConvert.ToBoolean(row[cols.IsStop.ToString()].ToString());
                        obj.StopReason = row[cols.StopReason.ToString()].ToString();
                        //obj.StopDate = DateTime.Parse(row[cols.StopDate.ToString()].ToString());
                        //obj.StopOper = row[cols.StopOper.ToString()].ToString();
                        obj.OperCode = arrangeMgr.Operator.ID;
                        obj.OperDate = current;
                        obj.IsValid = true;
                        arrangeList.Add(obj);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("����ʵ�弯��ʱ����!" + e.Message, "��ʾ");
                    return null;
                }
            }

            return arrangeList;
        }
        /// <summary>
        /// ��֤
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int Valid(DataTable dt)
        {
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (string.IsNullOrEmpty(row[cols.ItemCode.ToString()].ToString()) || string.IsNullOrEmpty(row[cols.ItemName.ToString()].ToString()))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҽ����Ŀ����Ϊ��"), "��ʾ");
                        return -1;
                    }
                    if (string.IsNullOrEmpty(row[cols.TypeCode.ToString()].ToString().Trim()) || string.IsNullOrEmpty(row[cols.TypeName.ToString()].ToString()))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҽ�����Ͳ���Ϊ��"), "��ʾ");
                        return -1;
                    }
                    if (string.IsNullOrEmpty(row[cols.DoctCode.ToString()].ToString().Trim()) || string.IsNullOrEmpty(row[cols.DoctName.ToString()].ToString()))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҽ����������Ϊ��"), "��ʾ");
                        return -1;
                    }
                    if (row[cols.BeginTime.ToString()] == null || row[cols.BeginTime.ToString()].ToString().Trim() == "" ||
                        row[cols.EndTime.ToString()] == null || row[cols.EndTime.ToString()].ToString().Trim() == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ԤԼʱ���"), "��ʾ");
                        return -1;
                    }
                    if (DateTime.Parse(row[cols.BeginTime.ToString()].ToString()).TimeOfDay >= DateTime.Parse(row[cols.EndTime.ToString()].ToString()).TimeOfDay)
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ʼʱ�䲻�ܴ��ڽ���ʱ��"), "��ʾ");
                        return -1;
                    }
                    if (row[cols.BookLimit.ToString()].ToString() == null || row[cols.BookLimit.ToString()].ToString() == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ԤԼ�޶����¼��"), "��ʾ");
                        return -1;
                    }
                    if (row[cols.HostLimit.ToString()].ToString() == null || row[cols.HostLimit.ToString()].ToString() == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("ԤԼ�޶����¼��"), "��ʾ");
                        return -1;
                    }

                }
                if (this.valid() < 0) return -1;

            }

            return 0;
        }
        /// <summary>
        /// �˶���û���ظ�ʱ���
        /// </summary>
        /// <returns></returns>
        private int valid()
        {

            if (this.fpSpread1_Sheet1.RowCount <= 0) return -1;
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                int count = MTTypeList.Where(t => t.ID == fpSpread1_Sheet1.Cells[i, (int)cols.TypeCode].Text &&
                     t.Memo == fpSpread1_Sheet1.Cells[i, (int)cols.ItemCode].Text).Count();
                if (count < 1)
                {
                    MessageBox.Show("��" + (i + 1) + "��ҽ����Ŀ�����Ͳ�ƥ��", "��ʾ", MessageBoxButtons.OK);
                    return -1;
                }
                if (this.fpSpread1_Sheet1.GetValue(i, (int)cols.IsStop).ToString().ToUpper() != "TRUE")
                {
                    DateTime beginDTi = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(i, (int)cols.BeginTime));
                    DateTime endDTi = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(i, (int)cols.EndTime));
                    for (int j = i + 1; j < this.fpSpread1_Sheet1.RowCount; j++)
                    {
                        if (this.fpSpread1_Sheet1.GetValue(i, (int)cols.IsStop).ToString().ToUpper() != "TRUE")
                        {
                            DateTime beginDTj = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(j, (int)cols.BeginTime));
                            DateTime endDTj = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(j, (int)cols.EndTime));
                            if (//this.fpSpread1_Sheet1.GetValue(i, (int)cols.ItemCode).ToString() == this.fpSpread1_Sheet1.GetValue(j, (int)cols.ItemCode).ToString() &&
                                //this.fpSpread1_Sheet1.GetValue(i, (int)cols.TypeCode).ToString() == this.fpSpread1_Sheet1.GetValue(j, (int)cols.TypeCode).ToString() &&
                                this.fpSpread1_Sheet1.GetValue(i, (int)cols.DoctCode).ToString() != "000000" &&
                                this.fpSpread1_Sheet1.GetValue(i, (int)cols.DoctCode).ToString() == this.fpSpread1_Sheet1.GetValue(j, (int)cols.DoctCode).ToString() &&
                                ((beginDTj.TimeOfDay <= beginDTi.TimeOfDay && endDTj.TimeOfDay > beginDTi.TimeOfDay) ||
                                (beginDTj.TimeOfDay < endDTi.TimeOfDay && endDTj.TimeOfDay >= endDTi.TimeOfDay) ||
                                (beginDTj.TimeOfDay >= beginDTi.TimeOfDay && endDTj.TimeOfDay <= endDTi.TimeOfDay) ||
                                (beginDTj.TimeOfDay <= beginDTi.TimeOfDay && endDTj.TimeOfDay >= endDTi.TimeOfDay)))
                            {
                                MessageBox.Show("��" + (j + 1).ToString() + "���Ű�ʱ������");
                                return -1;
                            }
                        }
                    }
                }

            }
            return 0;
        }
        /// <summary>
        /// �����Ű��Ƿ���ʹ��
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private int ValidByIDForUpdate(string ID)
        {
            int returnValue = this.arrangeMgr.CheckIsExpiredByID(ID);
            if (returnValue < 0 )
            {
                MessageBox.Show(this.arrangeMgr.Err);
                return -1;
            }
            if (returnValue > 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ű��¼�Ѿ�ʹ�ã������޸Ļ�ɾ��"));
                return -1;
            }
            return 1;
        }

        #endregion

        #region FarPoint����
        /// <summary>
        /// ���������б�λ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditModeOn(object sender, System.EventArgs e)
        {
            //���ɼ�

            this.visible(false);


            if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.ItemName ||
                this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.TypeName ||
                this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.DoctName)
            {
                if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.TypeName)
                {
                    initTechType(fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, (int)cols.ItemCode].Text);
                }
                this.setLocation();
                this.visible(false);
            }
            fpSpread1.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            fpSpread1.EditingControl.DoubleClick += new EventHandler(EditingControl_DoubleClick);
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditModeOff(object sender, EventArgs e)
        {
            try
            {
                this.fpSpread1.EditingControl.KeyDown -= new KeyEventHandler(EditingControl_KeyDown);
                this.fpSpread1.EditingControl.DoubleClick -= new EventHandler(EditingControl_DoubleClick);
            }
            catch { }
        }
        /// <summary>
        /// ���������б�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            string text = this.fpSpread1_Sheet1.ActiveCell.Text.Trim();
            text = FS.FrameWork.Public.String.TakeOffSpecialChar(text, "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\");
            if (col == (int)cols.ItemName)
            {
                this.fpSpread1_Sheet1.Cells[row, (int)cols.ItemName].Text = "";
                this.lbTech.Filter(text);

                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == (int)cols.TypeName)
            {
                this.fpSpread1_Sheet1.Cells[row, (int)cols.TypeName].Text = "";
                this.lbTechType.Filter(text);

                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == (int)cols.DoctName)
            {
                this.fpSpread1_Sheet1.Cells[row, (int)cols.DoctName].Text = "";
                this.lbDoct.Filter(text);

                if (this.groupBox1.Visible == false) this.visible(true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_Change(object sender, ChangeEventArgs e)
        {
            if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.IsStop)
            {
                string cellText = this.fpSpread1_Sheet1.GetText(this.fpSpread1_Sheet1.ActiveRowIndex, this.fpSpread1_Sheet1.ActiveColumnIndex);

                if (cellText.ToUpper() == "FALSE")
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime].BackColor = Color.MistyRose;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime].BackColor = Color.MistyRose;
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime].BackColor = SystemColors.Window;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime].BackColor = SystemColors.Window;
                }
            }

        }


        /// <summary>
        /// �س�����
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                #region enter
                if (this.fpSpread1.ContainsFocus)
                {
                    int col = this.fpSpread1_Sheet1.ActiveColumnIndex;

                    if (col == (int)cols.ItemName)
                    {
                        if (this.selectTech() == -1) return false;
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.TypeName, false);
                    }
                    else if (col == (int)cols.TypeName)
                    {
                        if (this.selectTechType() == -1) return false;
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.DoctName, false);
                    }
                    else if (col == (int)cols.DoctName)
                    {
                        if (this.selectDoct() == -1) return false;
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime, false);
                    }
                    else if (col == (int)cols.BeginTime)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime, false);
                    }
                    else if (col == (int)cols.EndTime)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BookLimit, false);
                    }
                    else if (col == (int)cols.BookLimit)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.HostLimit, false);
                    }
                    else if (col == (int)cols.HostLimit)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.IsStop, false);
                    }
                    else if (col == (int)cols.IsStop)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.StopReason, false);
                    }
                    else if (col == (int)cols.StopReason)
                    {
                        this.Add();
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Up)
            {
                #region up
                if (this.fpSpread1.ContainsFocus)
                {
                    if (this.groupBox1.Visible)
                    { this.priorRow(); }
                    else
                    {
                        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow > 0)
                        {
                            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow - 1;
                            fpSpread1_Sheet1.AddSelection(CurrentRow - 1, 0, 1, 0);
                        }
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Down)
            {
                #region down
                if (this.fpSpread1.ContainsFocus)
                {
                    if (this.groupBox1.Visible)
                    { this.nextRow(); }
                    else
                    {
                        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow >= 0 && CurrentRow <= fpSpread1_Sheet1.RowCount - 2)
                        {
                            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                            fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 0);
                        }
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Escape)
            {
                this.visible(false);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region �����ؼ�����
        /// <summary>
        /// ����groupBox1����ʾλ��
        /// </summary>
        /// <returns></returns>
        private void setLocation()
        {
            Control cell = fpSpread1.EditingControl;
            if (cell == null) return;

            int y = fpSpread1.Top + cell.Top + cell.Height + this.groupBox1.Height + 7;
            if (y <= this.Height)
            {
                if (fpSpread1.Left + cell.Left + this.groupBox1.Width + 20 <= this.Width)
                {
                    this.groupBox1.Location = new Point(fpSpread1.Left + cell.Left + 20, y - this.groupBox1.Height);
                }
                else
                {
                    this.groupBox1.Location = new Point(this.Width - this.groupBox1.Width - 10, y - this.groupBox1.Height);
                }
            }
            else
            {
                if (fpSpread1.Left + cell.Left + this.groupBox1.Width + 20 <= this.Width)
                {
                    this.groupBox1.Location = new Point(fpSpread1.Left + cell.Left + 20, fpSpread1.Top + cell.Top - this.groupBox1.Height - 7);
                }
                else
                {
                    this.groupBox1.Location = new Point(this.Width - this.groupBox1.Width - 10, fpSpread1.Top + cell.Top - this.groupBox1.Height - 7);
                }
            }
        }

        /// <summary>
        /// ���ÿؼ��Ƿ�ɼ�
        /// </summary>
        /// <param name="visible"></param>
        private void visible(bool visible)
        {
            if (visible == false)
            { this.groupBox1.Visible = false; }
            else
            {
                int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
                if (col == (int)cols.ItemName)
                {
                    this.lbTech.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == (int)cols.TypeName)
                {
                    this.lbTechType.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == (int)cols.DoctName)
                {
                    this.lbDoct.BringToFront();
                    this.groupBox1.Visible = true;
                }
            }
        }
        /// <summary>
        /// ǰһ��
        /// </summary>
        private void nextRow()
        {
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            if (col == (int)cols.ItemName)
            {
                this.lbTech.NextRow();
            }
            else if (col == (int)cols.TypeName)
            {
                this.lbTechType.NextRow();
            }
            else if (col == (int)cols.DoctName)
            {
                this.lbDoct.NextRow();
            }
        }
        /// <summary>
        /// ��һ��
        /// </summary>
        private void priorRow()
        {
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            if (col == (int)cols.ItemName)
            {
                this.lbTech.PriorRow();
            }
            else if (col == (int)cols.TypeName)
            {
                this.lbTechType.PriorRow();
            }
            else if (col == (int)cols.DoctName)
            {
                this.lbDoct.PriorRow();
            }
        }



        /// <summary>
        /// ѡ��ҽ��
        /// </summary>
        /// <returns></returns>
        private int selectTech()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.lbTech.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.ItemName, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.ItemCode, obj.ID, false);
                this.visible(false);
            }
            else
            {
                string ItemCode = this.fpSpread1_Sheet1.GetText(row, (int)cols.ItemCode);

                if (string.IsNullOrEmpty(ItemCode))
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.ItemName, "", false);
            }

            return 0;
        }
        /// <summary>
        /// ѡ��ҽ����λ
        /// </summary>
        /// <returns></returns>
        private int selectTechType()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.lbTechType.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.TypeName, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.TypeCode, obj.ID, false);
                this.visible(false);
            }
            else
            {
                string TypeCode = this.fpSpread1_Sheet1.GetText(row, (int)cols.TypeCode);

                if (string.IsNullOrEmpty(TypeCode))
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.TypeName, "", false);
            }

            return 0;
        }
        /// <summary>
        /// ѡ��ҽ��
        /// </summary>
        /// <returns></returns>
        private int selectDoct()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.lbDoct.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctName, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctCode, obj.ID, false);
                this.visible(false);
            }
            else
            {
                string DoctCode = this.fpSpread1_Sheet1.GetText(row, (int)cols.DoctCode);

                if (string.IsNullOrEmpty(DoctCode))
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctName, "", false);
            }

            return 0;
        }
        /// <summary>
        /// ѡ��ҽ��
        /// </summary>
        /// <param name="sender"></param>
        ///<param name="e"></param>
        private void lbTech_SelectItem(object sender, EventArgs e)
        {
            this.selectTech();
            this.visible(false);
            return;
        }
        /// <summary>
        /// ѡ��ҽ��
        /// </summary>
        /// <param name="sender"></param>
        ///<param name="e"></param>
        private void lbTechType_SelectItem(object sender, EventArgs e)
        {
            this.selectTechType();
            this.visible(false);
            return;
        }

        /// <summary>
        /// ѡ��ҽ��
        /// </summary>
        /// <param name="sender"></param>
        ///<param name="e"></param>
        private void lbDoct_SelectItem(object sender, EventArgs e)
        {
            this.selectDoct();
            this.visible(false);
            return;
        }
        #endregion

        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                FarPoint.Win.Spread.CellType.GeneralEditor t = fpSpread1.EditingControl as FarPoint.Win.Spread.CellType.GeneralEditor;
                if (t.SelectionStart == 0 && t.SelectionLength == 0)
                {
                    int row = 0, column = 0;
                    if (fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.ItemName && fpSpread1_Sheet1.ActiveRowIndex != 0)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex - 1;
                        column = (int)cols.StopReason;
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex != (int)cols.ItemName)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex;
                        column = this.getPriorVisibleColumn(this.fpSpread1_Sheet1.ActiveColumnIndex);

                    }
                    if (column != -1)
                    {
                        //����ѹ����ʾ����

                        if ((column == (int)cols.ItemName || column == (int)cols.TypeName || column == (int)cols.DoctName) && dv[row].Row.RowState != DataRowState.Added)
                        {
                            return;
                        }

                        fpSpread1_Sheet1.SetActiveCell(row, column, true);
                    }
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                FarPoint.Win.Spread.CellType.GeneralEditor t = fpSpread1.EditingControl as FarPoint.Win.Spread.CellType.GeneralEditor;

                if (t.Text == null || t.Text == "" || t.SelectionStart == t.Text.Length)
                {
                    int row = fpSpread1_Sheet1.RowCount - 1, column = fpSpread1_Sheet1.ColumnCount - 1;
                    if (fpSpread1_Sheet1.ActiveColumnIndex == column && fpSpread1_Sheet1.ActiveRowIndex != row)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex + 1;

                        if (dv[row].Row.RowState == DataRowState.Added)
                        {
                            column = (int)cols.ItemName;
                        }
                        else
                        {
                            column = (int)cols.TypeName;
                        }
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex != column)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex;

                        column = this.getNextVisibleColumn(this.fpSpread1_Sheet1.ActiveColumnIndex);
                    }

                    if (column != -1)
                    {
                        //����ѹ����ʾ����
                        if ((column == (int)cols.ItemName || column == (int)cols.TypeName || column == (int)cols.DoctName) && dv[row].Row.RowState != DataRowState.Added)
                        {
                            return;
                        }

                        fpSpread1_Sheet1.SetActiveCell(row, column, true);
                    }
                }
            }
        }


        /// <summary>
        /// ˫������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditingControl_DoubleClick(object sender, EventArgs e)
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            if (row < 0) return;

            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;

            if (col == (int)cols.BeginTime || col == (int)cols.EndTime)
            {
                //��ʾ��״̬
                this.showColor(row);
            }
        }
 
        private int getNextVisibleColumn(int col)
        {
            int count = this.fpSpread1_Sheet1.Columns.Count;

            while (col < count - 1)
            {
                col++;

                if (this.fpSpread1_Sheet1.Columns[col].Visible)
                {
                    return col;
                }
            }

            return -1;
        }

        private int getPriorVisibleColumn(int col)
        {
            while (col > 0)
            {
                col--;

                if (this.fpSpread1_Sheet1.Columns[col].Visible)
                {
                    return col;
                }
            }

            return -1;
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
        /// ��ȡ��ǰ��sheetҳ�����ݵ�������
        /// </summary>
        /// <returns></returns>
        public FarPoint.Win.Spread.SheetView GetFpSheet()
        {
            return this.fpSpread1_Sheet1;
        }
    }
}
