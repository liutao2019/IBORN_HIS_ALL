using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Material.Base
{
    public partial class ucCrossQueryBaseForFarPoint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCrossQueryBaseForFarPoint()
        {
            InitializeComponent();

        }

        #region ����

        /// <summary>
        /// ���ؼ���ʼ���
        /// </summary>
        protected const int LEFT_CONTROL_WIDTH = 200;

        /// <summary>
        /// ϸ����ʾ���ָ߶�
        /// </summary>
        protected const int DETAIL_CONTROL_HEIGHT = 300;

        /// <summary>
        /// �����ʾ���ؼ��Ƿ�ɼ�
        /// </summary>
        protected bool isLeftVisible = false;

        /// <summary>
        /// �Ƿ���ʾϸ�ڲ���
        /// </summary>
        protected bool isShowDetail = false;

        /// <summary>
        /// ���ؼ�,Ĭ��Ϊ�����ؼ�
        /// </summary>
        protected QueryControls leftControl = QueryControls.Other;

        /// <summary>
        /// ������ؼ�
        /// </summary>
        protected TreeView tvLeft = null;

        /// <summary>
        /// �Ƿ�ѡ�����ڵ��,����Retrieve();
        /// </summary>
        protected bool isAfterSelectRetrieve = false;
      
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        protected DateTime beginTime = DateTime.MinValue;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        protected DateTime endTime = DateTime.MinValue;

        /// <summary>
        /// 
        /// </summary>
        protected FS.FrameWork.Management.DataBaseManger dataBaseManager = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// ��¼��Ա��Ϣ
        /// </summary>
        protected FS.HISFC.Models.Base.Employee employee = null;

        /// <summary>
        /// ��ѯ�õ����
        /// </summary>
        private string querySql = string.Empty;

        /// <summary>
        /// ��ҳ�����ڵ�����10
        /// </summary>
        private int rowPageBreak = -1;

        /// <summary>
        /// ����������ļ���λ��
        /// </summary>
        private string mainSheetXmlFileName = string.Empty;

        ///// <summary>
        ///// ��ϸ��������ļ���
        ///// </summary>
        //private string detailSheetXmlFileName = string.Empty;

        /// <summary>
        /// ��ʾ��ʼ�е�����
        /// </summary>
        private int dataBeginRowIndex = 0;
        /// <summary>
        /// ��ʾ��ʼ�е�����
        /// </summary>
        private int dataBeginColumnIndex = 0;
        /// <summary>
        /// �б��⿪ʼ�е�����
        /// </summary>
        private int rowsHeaderBeginRowIndex = 0;
        /// <summary>
        /// �б��⿪ʼ�е����� 
        /// </summary>
        private int rowsHeaderBeginColumnIndex = 0;
        /// <summary>
        /// �б��⿪ʼ�е�����
        /// </summary>
        private int columnsHeaderBeginRowIndex = 0;
        /// <summary>
        /// �б��⿪ʼ�е�����
        /// </summary>
        private int columnsHeaderBeginColumnIndex = 0;
        #region {9F609C45-B357-4807-A1E1-3741F08D471A}
        /// <summary>
        /// ���ܼƼ���
        /// </summary>
        private string[] columnsTotalLevel = new string[0];
        /// <summary>
        /// ���ܼƼ���
        /// </summary>
        [Category("�������"), Description("�������ݼ������ܼƼ���")]
        public string ColumnsTotalLevel
        {
            get
            {
                string rtn = string.Empty;
                if (columnsTotalLevel != null)
                {
                    //Array.Reverse(dataCrossColumns);
                    rtn = string.Join("|", this.columnsTotalLevel);
                }
                return rtn;
            }
            set
            {
                columnsTotalLevel = value.Split('|');

            }
        }
        /// <summary>
        /// ���ܼƼ���
        /// </summary>
        private string[] rowsTotalLevel = new string[0];
        /// <summary>
        /// ���ܼƼ���
        /// </summary>
        [Category("�������"), Description("�������ݼ������ܼƼ���")]
        public string RowsTotalLevel
        {
            get
            {
                string rtn = string.Empty;
                if (rowsTotalLevel != null)
                {
                    //Array.Reverse(dataCrossColumns);
                    rtn = string.Join("|", this.rowsTotalLevel);
                }
                return rtn;
            }
            set
            {
                rowsTotalLevel = value.Split('|');
            }
        } 
        #endregion
        /// <summary>
        /// ������ʾ������
        /// </summary>
        // private string[] dataDisplayColumns = new string[0];
        /// <summary>
        /// ���ݽ�����
        /// </summary>
        private string[] dataCrossColumns = new string[0];

        private bool isRowTotFrontShow = false;

        private bool isColumnTotFrontShow = false;

        private int frozenColumnCount = 0;

        private int frozenRowCount = 0;

        [Category("�������"), Description("�����й̶������������أ�")]
        public int FrozenColumnCount
        {
            get { return frozenColumnCount; }
            set { frozenColumnCount = value; }
        }

        [Category("�������"), Description("�����й̶������������أ�")]
        public int FrozenRowCount
        {
            get { return frozenRowCount; }
            set { frozenRowCount = value; }
        }

        [Category("�������"), Description("�кϼ��Ƿ�ǰ����ʾ��")]
        public bool IsRowTotFrontShow
        {
            get { return isRowTotFrontShow; }
            set { isRowTotFrontShow = value; }
        }

        [Category("�������"), Description("�кϼ��Ƿ�ǰ����ʾ��")]
        public bool IsColumnTotFrontShow
        {
            get { return isColumnTotFrontShow; }
            set { isColumnTotFrontShow = value; }
        }
        /// <summary>
        /// �������ݼ��������γ����ݽ����е��У�
        /// </summary>
        [Category("�������"), Description("�������ݼ��������γ����ݽ����е��У�")]
        public string DataCrossColumns
        {
            get
            {
                string rtn = string.Empty;
                if (dataCrossColumns != null)
                {
                    //Array.Reverse(dataCrossColumns);
                    rtn = string.Join("|", this.dataCrossColumns);
                }
                return rtn;
            }
            set
            {
                dataCrossColumns = value.Split('|');
                ComputeIndex();
            }
        }
        /// <summary>
        /// ���ݽ�����
        /// </summary>
        private string[] dataCrossRows = new string[0];
        /// <summary>
        /// �������ݼ��������γ����ݽ����е��У�
        /// </summary>
        [Category("�������"), Description("�������ݼ��������γ����ݽ����е��У�")]
        public string DataCrossRows
        {
            get
            {
                string rtn = string.Empty;
                if (dataCrossRows != null)
                {
                    rtn = string.Join("|", this.dataCrossRows);
                }
                return rtn;
            }
            set
            {
                dataCrossRows = value.Split('|');
                ComputeIndex();
            }
        }
        /// <summary>
        /// ���ݽ���ֵ
        /// </summary>
        private string[] dataCrossValues = new string[0];
        /// <summary>
        /// �������ݼ��������γ����ݽ���ֵ���У�
        /// </summary>
        [Category("�������"), Description("�������ݼ��������γ����ݽ���ֵ���У�")]
        public string DataCrossValues
        {
            get
            {
                string rtn = string.Empty;
                if (dataCrossValues != null)
                {
                    rtn = string.Join("|", this.dataCrossValues);
                }
                return rtn;
            }
            set
            {
                dataCrossValues = value.Split('|');
                ComputeIndex();
            }
        }
        /// <summary>
        /// ��ѯ����
        /// </summary>
        private System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> queryParams = new List<FS.FrameWork.Models.NeuObject>();
        /// <summary>
        /// ��������
        /// </summary>
        protected int dataRowCount = 0;
        /// <summary>
        /// ��ѯ����
        /// </summary>
        private QuerySqlType querySqlTypeValue = QuerySqlType.id;
        DataBaseLogic db = new DataBaseLogic();
        private FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        private string hospitalName = string.Empty;
        private int useParamCellsCount = 0;

        #endregion

        #region ����
        [Category("�������"), Description("���ñ����ѯʱ��Ҫ�ò����滻�ĵ�Ԫ��������")]
        public int UseParamCellsCount
        {
            get { return useParamCellsCount; }
            set { useParamCellsCount = value; }
        }
        public string HospitalName
        {
            get
            {
                if (DesignMode == false)
                {
                    if (string.IsNullOrEmpty(hospitalName) == true)
                    {
                        hospitalName = con.GetHospitalName();
                    }
                }
                return hospitalName;
            }
            set { hospitalName = value; }
        }
        public DataBaseLogic Db
        {
            get { return db; }
            set { db = value; }
        }
        public FarPoint.Win.Spread.SheetView SvMain
        {
            get
            {
                if (this.neuSpread1.Sheets.Count > 0)
                {
                    return this.neuSpread1.Sheets[0];
                }
                return null;
            }
            set
            {
                //FarPoint.Win.Spread.SheetView v = null;
                if (this.neuSpread1.Sheets.Count > 0)
                {
                    this.neuSpread1.Sheets[0] = value;
                }
                //v = value ;
            }
        }

        public FarPoint.Win.Spread.SheetView SvDetail
        {
            get
            {
                if (this.neuSpread2.Sheets.Count > 0)
                {
                    return this.neuSpread2.Sheets[0];
                }
                return null;
            }
            set
            {
                //FarPoint.Win.Spread.SheetView v = null;
                if (this.neuSpread2.Sheets.Count > 0)
                {
                    this.neuSpread2.Sheets[0] = value;
                }
                //v = value;
            }
        }
        [Category("�������"), Description("���ñ����ѯʱ�Ĳ�����")]
        public System.Collections.Generic.List<FS.FrameWork.Models.NeuObject> QueryParams
        {
            get { return queryParams; }
            set { queryParams = value; }
        }
        [Category("�������"), Description("���ñ����ѯʱʹ�õ�sql���ͣ�")]
        public QuerySqlType QuerySqlTypeValue
        {
            get { return querySqlTypeValue; }
            set { querySqlTypeValue = value; }
        }
        ///// <summary>
        ///// �������ݼ���������ʾ���У�
        ///// </summary>
        //[Category("�������"), Description("���ñ�����Ҫ��ʾ�������У�")]
        //public string DataDisplayColumns
        //{
        //    get
        //    {
        //        string rtn = string.Empty;
        //        if (dataDisplayColumns != null)
        //        {
        //            rtn = string.Join("|", this.dataDisplayColumns);
        //        }
        //        return rtn;
        //    }
        //    set
        //    {
        //        dataDisplayColumns = value.Split('|');
        //        rowsHeaderBeginColumnIndex = dataBeginRowIndex;
        //        if (this.dataCrossValues.Length > 1)
        //        {
        //            rowsHeaderBeginRowIndex = dataBeginColumnIndex + dataCrossRows.Length + 1;
        //        }
        //        else
        //        {
        //            rowsHeaderBeginRowIndex = dataBeginColumnIndex + dataCrossRows.Length;
        //        }
        //        columnsHeaderBeginColumnIndex = dataBeginRowIndex + dataCrossColumns.Length; ;
        //        columnsHeaderBeginRowIndex = dataBeginRowIndex;
        //    }
        //}
        [Category("�������"), Description("���ñ����ѯ�������ݼ�����ʾ����ʼ�е�������")]
        public int DataBeginRowIndex
        {
            get
            {
                return dataBeginRowIndex;
            }
            set
            {
                dataBeginRowIndex = value;
                ComputeIndex();
            }
        }
        [Category("�������"), Description("���ñ����ѯ�������ݼ�����ʾ����ʼ�е�������")]
        public int DataBeginColumnIndex
        {
            get
            {
                return dataBeginColumnIndex;
            }
            set
            {
                dataBeginColumnIndex = value;
                ComputeIndex();
            }
        }
        [Category("�������"), Description("���ñ�������������ļ�����")]
        public string MainSheetXml
        {
            get { return mainSheetXmlFileName; }
            set { mainSheetXmlFileName = value; }
        }

        [EditorAttribute(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor)), Category("�������"), Description("���ñ����ѯʱʹ�õ�sql��")]
        public string QuerySql
        {
            get { return querySql; }
            set { querySql = value; }
        }
        /// <summary>
        /// ���ݴ�����Title��Text�ؼ�����
        /// </summary>
        [Category("�������"), Description("ÿҳ��ʾ��������")]
        public int PageRowCount
        {
            get { return rowPageBreak; }
            set { rowPageBreak = value; }
        }



        /// <summary>
        /// �����ʾ���ؼ��Ƿ�ɼ�
        /// </summary>
        [Category("�ؼ�����"), Description("��������Ƿ�ɼ�(��)")]
        public bool IsLeftVisible
        {
            get
            {
                return this.isLeftVisible;
            }
            set
            {
                this.isLeftVisible = value;

                //�������ؼ��Ƿ�ɼ�
                this.SetLeftControlVisible();
            }
        }

        /// <summary>
        /// �Ƿ�ѡ�����ڵ��,����Retrieve();
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�ѡ�����ڵ��,����Retrieve")]
        public bool IsAfterSelectRetrieve
        {
            get
            {
                return this.isAfterSelectRetrieve;
            }
            set
            {
                this.isAfterSelectRetrieve = value;
            }
        }

        /// <summary>
        /// ���ؼ�,Ĭ��Ϊ�����ؼ�
        /// </summary>
        [Category("�ؼ�����"), Description("���ؼ�,Ĭ��Ϊ�����ؼ�")]
        public QueryControls LeftControl
        {
            get
            {
                return this.leftControl;
            }
            set
            {
                this.leftControl = value;

                this.SetLeftControl();
            }
        }

        /// <summary>
        /// �Ƿ���ʾϸ�ڲ���
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾϸ�ڲ���")]
        public bool IsShowDetail
        {
            get
            {
                return this.isShowDetail;
            }
            set
            {
                this.isShowDetail = false;

                this.SetDetailVisible();
            }
        }



        #endregion

        #region ����




        /// <summary>
        /// TreeView�ڵ��ѯ
        /// </summary>
        /// <returns></returns>
        protected virtual int OnQueryTree()
        {
            if (this.tvLeft == null)
            {
                return -1;
            }

            string queryText = this.cmbQuery.Text;

            if (queryText == string.Empty)
            {
                return -1;
            }

            if (this.tvLeft.Nodes.Count <= 0)
            {
                return -1;
            }

            TreeNode queryNode = this.tvLeft.Nodes[0];

            this.QueryTree(queryNode, queryText);

            return 1;
        }

        private void QueryTree(TreeNode nowNode, string queryText)
        {

            if (nowNode == null)
            {
                return;
            }

            if (nowNode.Tag != null && nowNode.Tag.ToString() == queryText)
            {
                this.tvLeft.Select();
                this.tvLeft.SelectedNode = nowNode;

                if (this.cmbQuery.Items.IndexOf(queryText) < 0)
                {
                    this.cmbQuery.Items.Add(queryText);
                }

                return;
            }
            if (nowNode.Text == queryText)
            {
                this.tvLeft.Select();
                this.tvLeft.SelectedNode = nowNode;

                if (this.cmbQuery.Items.IndexOf(queryText) < 0)
                {
                    this.cmbQuery.Items.Add(queryText);
                }

                return;
            }

            foreach (TreeNode node in nowNode.Nodes)
            {
                QueryTree(node, queryText);
            }

            return;
        }


        /// <summary>
        /// ��������.
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int OnDrawTree()
        {
            if (this.tvLeft == null)
            {
                return -1;
            }

            this.tvLeft.ImageList = new ImageList();
            this.tvLeft.ImageList.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ));
            this.tvLeft.ImageList.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B��ҩ��));

            return 1;
        }

        /// <summary>
        /// ��ò�ѯʱ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int GetQueryTime()
        {
            if (this.dtpEndTime.Value < this.dtpBeginTime.Value)
            {
                MessageBox.Show("����ʱ�䲻��С�ڿ�ʼʱ��");

                return -1;
            }

            this.beginTime = this.dtpBeginTime.Value;
            this.endTime = this.dtpEndTime.Value;

            return 1;
        }


        /// <summary>
        /// ����ϸ�ڲ����Ƿ�ɼ�
        /// </summary>
        protected virtual void SetDetailVisible()
        {
            this.plRightBottom.Visible = this.isShowDetail;

            this.plRightBottom.Height = this.isShowDetail ? DETAIL_CONTROL_HEIGHT : 0;

            this.slTop.Enabled = this.isShowDetail;

            this.slTop.Visible = this.isShowDetail;


        }
        /// <summary>
        /// �������ݴ���Title����
        /// </summary>
        protected virtual void SetTitle()
        {
            if (this.SvMain != null)
            {
                if (this.HospitalName != string.Empty)
                {
                    try
                    {
                        // SvMain.Cells["{title}"].Text = HospitalName + SvMain.Cells["{title}"].Text;
                        FarPoint.Win.Spread.Cell c = null;
                        string CellText = string.Empty;
                        c = SvMain.GetCellFromTag(c, "{hospitalName}");
                        if (c != null)
                        {
                            CellText = c.Note;
                            CellText = CellText.Replace("{hospitalName}", HospitalName);
                            c.Text = CellText;
                        }
                    }
                    catch { }
                }
            }
        }
        /// <summary>
        /// �������ؼ�
        /// </summary>
        protected virtual void SetLeftControl()
        {
            //������ؼ��Ѿ����ɼ�,���´��벻��������.
            if (!this.isLeftVisible)
            {
                return;
            }

            //������ؼ������Ѿ����صĿؼ�
            this.plLeftControl.Controls.Clear();

            switch (this.leftControl)
            {
                case QueryControls.Tree:

                    this.tvLeft = new TreeView();

                    this.plLeftControl.Controls.Add(tvLeft);

                    this.tvLeft.Dock = DockStyle.Fill;

                    this.tvLeft.AfterSelect += new TreeViewEventHandler(tvLeft_AfterSelect);

                    break;
            }
        }

        /// <summary>
        /// ��������Ļ�,����AfterSelect�¼�������,ִ�з���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!this.isAfterSelectRetrieve)
            {
                this.OnQuery(sender, e);
            }
            return;
        }

        void tvLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tv == null)
            {
                return;
            }

            this.OnTreeViewAfterSelect(sender, e);
        }

        /// <summary>
        /// �������ؼ��Ƿ�ɼ�
        /// </summary>
        protected virtual void SetLeftControlVisible()
        {
            this.plLeft.Visible = this.isLeftVisible;

            this.plLeft.Width = this.isLeftVisible ? LEFT_CONTROL_WIDTH : 0;

            this.slLeft.Enabled = this.isLeftVisible;

            this.slLeft.Visible = this.isLeftVisible;
        }




        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected int Init()
        {
            #region ����XML����
            OpenSpread();
            #endregion
            #region Ĭ��ʱ�����óɵ�ǰʱ�䵽��ǰʱ��ǰһ���µ�ʱ��
            System.DateTime dtNow = this.dataBaseManager.GetDateTimeFromSysDateTime();
            this.dtpBeginTime.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);
            this.dtpEndTime.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 23, 59, 59);
            #endregion

            #region ������ݱ��
            if (SvMain.RowCount > this.DataBeginRowIndex + 1)
            {
                SvMain.ClearRange(this.DataBeginRowIndex + 1, 0, SvMain.Rows.Count - 1, SvMain.Columns.Count - 1, false);
            }
            #endregion

            this.OnDrawTree();

            if (this.tvLeft != null)
            {
                if (this.tvLeft.Nodes.Count > 0)
                {
                    this.tvLeft.Select();
                    this.tvLeft.SelectedNode = this.tvLeft.Nodes[0];
                }
            }
            //this.neuSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellClick);
            this.neuSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread1_SelectionChanged);
            this.SetTitle();
            return 1;
        }

        void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            OnSort();
        }

        void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            OnSort();
        }

        /// <summary>
        /// Load�¼�
        /// </summary>
        protected virtual void OnLoad()
        {
            this.employee = (FS.HISFC.Models.Base.Employee)this.dataBaseManager.Operator;
        }

        /// <summary>
        /// ������Ʋ�ѯ�����Ĳ�ѯ,�̳���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int OnRetrieve(params object[] objects)
        {
            if (SvMain != null)
            {
                //SvMain.Retrieve(objects);
            }

            return 1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            OnExport();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int OnExport()
        {
            if (SvMain == null)
            {
                return -1;
            }

            //this.DeleleSortFlag(SvMain);

            System.Windows.Forms.SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "txt files (*.xls)|*.xls";
            if (sfd.ShowDialog() == DialogResult.Cancel)
            {
                return 1;
            }
            //SvMain.SaveAs(dd.FileName, Sybase.DataWindow.FileSaveAsType.Excel, true);
            neuSpread1.SaveExcel(sfd.FileName, FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
            return 1;
        }

        /// <summary>
        /// Ԥ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrintPreview(object sender, object neuObject)
        {
            if (this.SvMain != null)
            {
                //frmPreviewDataWindow frmPreview = new frmPreviewDataWindow();

                ////frmPreview.PreviewDataWindow = SvMain;

                //if (frmPreview.ShowDialog() == DialogResult.OK)
                //{ }
                //    //this.SvMain.PrintProperties.Preview = false;
                //this.neuSpread1.OwnerPrintDraw(
            }

            return base.OnPrintPreview(sender, neuObject);


        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            if (this.SvMain != null)
            {
                try
                {
                    this.neuSpread1.PrintSheet(SvMain);
                }
                catch { }
            }

            return base.OnPrint(sender, neuObject);
        }

        #endregion

        #region ö��

        /// <summary>
        /// �������װ�ؿؼ�
        /// </summary>
        public enum QueryControls
        {
            /// <summary>
            /// TreeView�ؼ�
            /// </summary>
            Tree = 0,

            /// <summary>
            /// DataWindow�ؼ�
            /// </summary>
            DataWindow,

            /// <summary>
            /// �ı��ؼ�
            /// </summary>
            Text,

            /// <summary>
            /// �����ؼ�
            /// </summary>
            Other
        }
        public enum QuerySqlType
        {
            /// <summary>
            /// id
            /// </summary>
            id,
            /// <summary>
            /// 
            /// </summary>
            text,
        }

        #endregion

        #region �¼�
        protected void OpenSpread()
        {
            //SvMain = new FarPoint.Win.Spread.SheetView();
            //this.neuSpread1.Sheets.Add(SvMain);
            #region ��xml����fp
            if (string.IsNullOrEmpty(this.MainSheetXml) == false)
            {
                this.neuSpread1.Open(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Report\\" + this.MainSheetXml);
            }
            //if (string.IsNullOrEmpty(this.DetailSheetXml) == false)
            //{
            //    this.neuSpread2.Open(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Report\\" + this.DetailSheetXml);
            //}
            #endregion
            #region ��open��������fp��ʽ���������ָ��sht����ʱ����
            if (this.neuSpread1.Sheets.Count > 0)
            {
                string f = string.Empty;

                SvMain = neuSpread1.Sheets[0];
                SvMain.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            }
            if (neuSpread2.Sheets.Count > 0)
            {
                SvDetail = neuSpread2.Sheets[0];
                SvDetail.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            }
            #endregion
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            Cursor = Cursors.WaitCursor;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ����,��ȴ�....");

            Application.DoEvents();

            #region ������ݱ��
            if (SvMain.RowCount >= this.dataBeginRowIndex + 1)
            {
                SvMain.ClearRange(this.dataBeginRowIndex, dataBeginColumnIndex, SvMain.Rows.Count, SvMain.Columns.Count, false);
            }
            #endregion
            #region �����滻
            FarPoint.Win.Spread.Cell c = null;
            for (int j = 0; j < this.useParamCellsCount; j++)
            {
                string CellText = string.Empty;
                c = SvMain.GetCellFromTag(c, "{QueryParams}");
                if (c != null)
                {
                    CellText = c.Note;
                    for (int i = 0; i < this.QueryParams.Count; i++)
                    {
                        CellText = CellText.Replace("{" + i + "}", this.QueryParams[i].ToString());
                    }
                    c.Text = CellText;
                }
            }
            #endregion
            #region ��ʾ�����

            DataTable dt = new DataTable();
            dataRowCount = 0;
            switch (this.QuerySqlTypeValue)
            {
                case QuerySqlType.id:
                    if (Db.QueryDataBySqlId(this.QuerySql, ref dt, this.QueryParams) != 1)
                    {
                        Cursor = Cursors.Arrow;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }
                    break;
                case QuerySqlType.text:
                    if (Db.QueryDataBySql(this.QuerySql, ref dt, this.QueryParams) != 1)
                    {
                        Cursor = Cursors.Arrow;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        return -1;
                    }
                    break;
                default:
                    break;
            }
            ///���ݼ�dt
            //******************
            //��	��	��	��	��	��	��	��	��	��	��	��	��
            //��	A	��	C	��	G	��	I	��	1	��	0	��
            //��	A	��	C	��	G	��	J	��	1	��	0	��
            //��	A	��	C	��	H	��	K	��	1	��	0	��
            //��	A	��	C	��	H	��	L	��	1	��	0	��
            //��	A	��	D	��	G	��	I	��	1	��	0	��
            //��	A	��	D	��	G	��	J	��	1	��	0	��
            //��	A	��	D	��	H	��	K	��	1	��	0	��
            //��	A	��	D	��	H	��	L	��	1	��	0	��
            //��	B	��	E	��	G	��	I	��	1	��	0	��
            //��	B	��	E	��	G	��	J	��	1	��	0	��
            //��	B	��	E	��	H	��	K	��	1	��	0	��
            //��	B	��	E	��	H	��	L	��	1	��	0	��
            //��	B	��	F	��	G	��	I	��	1	��	0	��
            //��	B	��	F	��	G	��	J	��	1	��	0	��
            //��	B	��	F	��	H	��	K	��	1	��	0	��
            //��	B	��	F	��	H	��	L	��	1	��	0	��
            //��	��	��	��	��	��	��	��	��	��	��	��	��
            //******************
            #region �ֱ𽻲�������
            DataTable dtCrossRows = null;
            DataTable dtCrossColumns = null;
            DataTable dtCrossValues = null;


            #region ����ֵ�󽻲�
            DataSetHelper dsh = new DataSetHelper();

            ///���������ݼ����������ϼ���
            dtCrossRows = dsh.SelectDistinctByIndexs("", dt, dataCrossRows);
            ///���ݼ�dtCrossRows
            //******************
            //��	��	��	��	��
            //��	G	��	I	��
            //��	G	��	J	��
            //��	H	��	K	��
            //��	H	��	L	��
            //��	��	��	��	��       
            //******************
            ///���������ݼ����������ϼ���
            dtCrossColumns = dsh.SelectDistinctByIndexs("", dt, dataCrossColumns);
            ///���ݼ�dtCrossColumns
            //******************
            //��	��	��	��	��
            //��	A	��	C	��
            //��	A	��	D	��
            //��	B	��	E	��
            //��	B	��	F	��
            //��	��	��	��	��          
            //******************
            //SvMain.DataSource = dtCrossColumns;
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //return 1;
            #region ���������ݼ�����ֵ���ݼ������,�������ֵ
            ///ֵ���ݼ�

            dtCrossValues = new DataTable();
            foreach (DataRow dr in dtCrossColumns.Rows)
            {
                #region ��ֵ���ݼ������
                //string columnsName = string.Empty;
                //foreach (object o in dr.ItemArray)
                //{
                //    columnsName = columnsName + o.ToString() + "|";
                //}
                foreach (string s in dataCrossValues)
                {
                    //ֵ���ݼ������һ��
                    dtCrossValues.Columns.Add(new DataColumn("", dt.Columns[int.Parse(s)].DataType));

                }
                #endregion
            }
            ///���ݼ�dtCrossValues
            //********  
            //��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��
            //��		��		��		��		��		��		��		��		��
            //��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��
            //********
            #region ���������ݼ�ÿһ��
            ///��ǰ�����ݼ���������
            int currRowIdx = 0;
            //���������ݼ�
            foreach (DataRow drRow in dtCrossRows.Rows)
            {
                ///ֵ���ݼ������һ��
                dtCrossValues.Rows.Add(dtCrossValues.NewRow());
                //���㵱ǰ�����ݼ���������
                currRowIdx = dtCrossRows.Rows.IndexOf(drRow);
                #region ���������ݼ�ÿһ��
                //��ǰ�����ݼ���������
                int currColIdx = 0;
                foreach (DataRow drColumn in dtCrossColumns.Rows)
                {
                    //���㵱ǰ�����ݼ���������
                    currColIdx = dtCrossColumns.Rows.IndexOf(drColumn);

                    //��ǰֵ���ݼ���������
                    int valColCnt = 0;
                    //���㵱ǰֵ���ݼ���������
                    valColCnt = this.dataCrossValues.Length;
                    #region ���㵱ǰ��Ԫ�������ֵ
                    ///��ǰ��Ԫ�������ֵ���ʽ
                    string currExp = string.Empty;
                    #region �����м������㵱ǰ��Ԫ��ֵ���б��ʽ����
                    //��ǰ�м���ֵ����
                    int expRowsIdx = 0;
                    //string currRowHeader = string.Empty;
                    foreach (string sDataCrossRows in this.dataCrossRows)
                    {
                        //���㵱ǰ�м���ֵ��������Ϊû���ظ�ֵ���Կ�����ôȡ��
                        expRowsIdx = Array.IndexOf(this.dataCrossRows, sDataCrossRows);
                        //����ԭʼ���ݼ���Ӧֵ�е����ͼ�����ʽ�����msdn��ms-help://MS.VSCC.v80/MS.MSDN.v80/MS.NETDEVFX.v20.chs/cpref4/html/P_System_Data_DataColumn_Expression.htm��
                        switch (dt.Columns[int.Parse(sDataCrossRows)].DataType.ToString())
                        {
                            case "System.Decimal":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossRows)].Caption + " = " + dtCrossRows.Rows[currRowIdx][expRowsIdx].ToString() + " AND ";
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossRows)].Caption + " = #" + dtCrossRows.Rows[currRowIdx][expRowsIdx] + "# AND ";
                                    break;
                                }
                            case "System.String":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossRows)].Caption + " = '" + dtCrossRows.Rows[currRowIdx][expRowsIdx] + "' AND ";
                                    break;
                                }
                            default:
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossRows)].Caption + " = '" + dtCrossRows.Rows[currRowIdx][expRowsIdx] + "' AND ";
                                    break;
                                }
                        }
                    }
                    #endregion
                    #region �����м������㵱ǰ��Ԫ���,����ֵ�ģ��б��ʽ����
                    //��ǰ�м���ֵ����
                    int expColumnsIdx = 0;
                    foreach (string sDataCrossColumns in this.dataCrossColumns)
                    {
                        //���㵱ǰ�м���ֵ��������Ϊû���ظ�ֵ���Կ�����ôȡ��
                        expColumnsIdx = Array.IndexOf(this.dataCrossColumns, sDataCrossColumns);
                        //����ԭʼ���ݼ���Ӧֵ�е����ͼ�����ʽ�����msdn��ms-help://MS.VSCC.v80/MS.MSDN.v80/MS.NETDEVFX.v20.chs/cpref4/html/P_System_Data_DataColumn_Expression.htm��
                        switch (dt.Columns[int.Parse(sDataCrossColumns)].DataType.ToString())
                        {
                            case "System.Decimal":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossColumns)].Caption + " = " + dtCrossColumns.Rows[currColIdx][expColumnsIdx].ToString() + " AND ";
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossColumns)].Caption + " = #" + dtCrossColumns.Rows[currColIdx][expColumnsIdx] + "# AND ";
                                    break;
                                }
                            case "System.String":
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossColumns)].Caption + " = '" + dtCrossColumns.Rows[currColIdx][expColumnsIdx] + "' AND ";
                                    break;
                                }
                            default:
                                {
                                    currExp = currExp + dt.Columns[int.Parse(sDataCrossColumns)].Caption + " = '" + dtCrossColumns.Rows[currColIdx][expColumnsIdx] + "' AND ";
                                    break;
                                }
                        }
                    }
                    #endregion
                    #region ȥβ���ġ�AND��
                    currExp = currExp.Remove(currExp.Length - 4);
                    #endregion
                    #region ����ֵ���ݼ�ÿһ��
                    //��ǰ��Ԫ�������ֵ
                    string[] currVal = new string[this.dataCrossValues.Length];
                    //��ǰֵ����ֵ����
                    int currValIdx = 0;
                    DataRow[] arryDr;
                    arryDr = dt.Select(currExp);
                    foreach (string s in this.dataCrossValues)
                    {
                        //���㵱ǰֵ����ֵ��������Ϊû���ظ�ֵ���Կ�����ôȡ��
                        currValIdx = Array.IndexOf(this.dataCrossValues, s);
                        currVal[currValIdx] = "0";
                        #region ����ȡ��ѯ�������У������Ӧλ�õ�ֵ
                        if (arryDr.Length > 0)
                        {
                            //�п����ж��У�����ȡֵ
                            foreach (DataRow drCurrExp in arryDr)
                            {
                                //TODO:��Ҫ��Ӹ��ּ��㷽ʽ,���ڽ�֧��sum����
                                // currVal[currValIdx] = (int.Parse(currVal[currValIdx]) + int.Parse(drCurrExp[int.Parse(s)].ToString())).ToString();
                                switch (drCurrExp.Table.Columns[int.Parse(s)].DataType.ToString())
                                {
                                    case "System.Decimal":
                                        {
                                            currVal[currValIdx] = (decimal.Parse(currVal[currValIdx]) + decimal.Parse(drCurrExp[int.Parse(s)].ToString())).ToString();
                                            break;
                                        }
                                    case "System.DateTime":
                                        {
                                            currVal[currValIdx] = DateTime.Parse(drCurrExp[int.Parse(s)].ToString()).ToString();
                                            break;
                                        }
                                    case "System.String":
                                        {
                                            currVal[currValIdx] = drCurrExp[int.Parse(s)].ToString();
                                            break;
                                        }
                                    default:
                                        {
                                            currVal[currValIdx] = (int.Parse(currVal[currValIdx]) + int.Parse(drCurrExp[int.Parse(s)].ToString())).ToString();
                                            break;
                                        }
                                }
                            }
                        }
                        #endregion
                        dtCrossValues.Rows[currRowIdx][currColIdx * valColCnt + currValIdx] = currVal[currValIdx];
                    }
                    #endregion
                    #endregion
                }
                #endregion
            }
            #endregion
            #endregion
            ///���ݼ�dtCrossValues
            //********  
            //��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��
            //��	1	��	0	��	1	��	0	��	1	��	0	��	1	��	0	��
            //��	1	��	0	��	1	��	0	��	1	��	0	��	1	��	0	��
            //��	1	��	0	��	1	��	0	��	1	��	0	��	1	��	0	��
            //��	1	��	0	��	1	��	0	��	1	��	0	��	1	��	0	��
            //��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��          
            //********  
            #region ����кϼ�ֵ
            DataTable dtCrossViewColumns = this.TotalCrossDataTableColumns(dtCrossValues, dt, this.dataCrossColumns, this.dataCrossValues);


            //SvMain.DataSource = dtCrossViewColumns;
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //return 1;
            #endregion
            #region ����кϼ�ֵ
            DataTable dtCrossViewRows = this.TotalCrossDataTableRows(dtCrossViewColumns, dt, this.dataCrossRows);
            //SvMain.DataSource = dtCrossViewRows;
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //return 1;
            #endregion
            #region �������б��⣬��ʾ���б���
            #region ����б��⣬�кϼƱ���
             DataTable dtCrossViewColumnsTitle = this.CrossDataTable(dt, this.dataCrossColumns,this.columnsTotalLevel);
        
            //SvMain.DataSource = dtCrossViewColumnsTitle;
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //return 1;
            #endregion
            #region ����б��⣬�кϼƱ���
             DataTable dtCrossViewRowsTitle = this.CrossDataTable(dt, this.dataCrossRows, this.rowsTotalLevel);
            #endregion

            #region �ϲ�
            #region ��ʾ�ϲ���
            DataTable dtCrossViewColumnsTitleIncludValues = dtCrossViewColumnsTitle.Clone();
            //���ֵ�д���һ��,����Ҫ��һ��ֵ������ʾ�õ���
            if (this.dataCrossValues.Length > 1)
            {
                int dataCrossValuesIdx = 0;
                dtCrossViewColumnsTitleIncludValues.Columns.Add(
                    new DataColumn("", typeof(string)));
                dataCrossValuesIdx = this.dataCrossColumns.Length;
                int dtCrossViewColumnsTitleRowIdx = 0;
                while (dtCrossViewColumnsTitleRowIdx < dtCrossViewColumnsTitle.Rows.Count)
                {
                    int dataCrossValuesItemIdx = 0;
                    foreach (string s in this.dataCrossValues)
                    {
                        dtCrossViewColumnsTitleIncludValues.Rows.Add(dtCrossViewColumnsTitle.Rows[dtCrossViewColumnsTitleRowIdx].ItemArray);
                        dataCrossValuesItemIdx = Array.IndexOf(dataCrossValues, s);
                        dtCrossViewColumnsTitleIncludValues.Rows
                            [dtCrossViewColumnsTitleRowIdx * dataCrossValues.Length + dataCrossValuesItemIdx]
                            [dataCrossValuesIdx]
                            = dt.Columns[int.Parse(s)].Caption;
                    }
                    dtCrossViewColumnsTitleRowIdx++;
                }
                //SvMain.DataSource = dtCrossViewColumnsTitleIncludValues;
                //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //return 1;
                //Funcation.DisplayToFpReverse(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
                Function.DisplayToFpReverse(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
                SpanDisplayedFpReverse(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
                SpanDisplayedFpReverseRows(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
            }
            else
            {
                int dataCrossValuesIdx = 0;
                dataCrossValuesIdx = this.dataCrossColumns.Length;
                int dtCrossViewColumnsTitleRowIdx = 0;
                while (dtCrossViewColumnsTitleRowIdx < dtCrossViewColumnsTitle.Rows.Count)
                {
                    foreach (string s in this.dataCrossValues)
                    {
                        dtCrossViewColumnsTitleIncludValues.Rows.Add(dtCrossViewColumnsTitle.Rows[dtCrossViewColumnsTitleRowIdx].ItemArray);
                    }
                    dtCrossViewColumnsTitleRowIdx++;
                }
                //SvMain.DataSource = dtCrossViewColumnsTitleIncludValues;
                //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //return 1;
                Function.DisplayToFpReverse(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
                SpanDisplayedFpReverseOneValue(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
                SpanDisplayedFpReverseRowsOneValue(SvMain, dtCrossViewColumnsTitleIncludValues, columnsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
            }

            #endregion
            #region ��ʾ�ϲ���
            Function.DisplayToFp(SvMain, dtCrossViewRowsTitle, rowsHeaderBeginRowIndex, rowsHeaderBeginColumnIndex);
            SpanDisplayedFp(SvMain, dtCrossViewRowsTitle, rowsHeaderBeginRowIndex, rowsHeaderBeginColumnIndex);
            SpanDisplayedFpRows(SvMain, dtCrossViewRowsTitle, rowsHeaderBeginRowIndex, rowsHeaderBeginColumnIndex);
            int colIdx = 0;
            foreach (string s in dataCrossRows)
            {
                SvMain.Cells[rowsHeaderBeginRowIndex - 1, rowsHeaderBeginColumnIndex + colIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                SvMain.Cells[rowsHeaderBeginRowIndex - 1, rowsHeaderBeginColumnIndex + colIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                SvMain.Cells[rowsHeaderBeginRowIndex - 1, rowsHeaderBeginColumnIndex + colIdx].Text = dt.Columns[int.Parse(s)].Caption;
                colIdx++;
            }
            #endregion
            #endregion
            #endregion
            #endregion
            //SvMain.DataSource = dtCrossViewRowsTitle;
            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            //return 1;
            #endregion
            this.dataRowCount = dtCrossViewRows.Rows.Count;
            #region ���ñ������
            if (this.SvMain.Rows.Count < DataBeginRowIndex + 1 + dtCrossViewRows.Rows.Count)
            {
                this.SvMain.RowCount = DataBeginRowIndex + 1 + dtCrossViewRows.Rows.Count;
            }
            #endregion

            #region �����Ԫ����佻������������
            Function.DisplayToFp(SvMain, dtCrossViewRows, rowsHeaderBeginRowIndex, columnsHeaderBeginColumnIndex);
            #region ���ܼ���ǰ
            if (this.IsColumnTotFrontShow)
            {
                int rowTotIndex = -1;
                //�������ܼ�
                for (int i = 0; i < SvMain.RowCount; i++)
                {
                    if (SvMain.Cells[i, 0].Text == "�ܼ�")
                    {
                        rowTotIndex = i;
                        break;
                    }
                }
                if (rowTotIndex != -1)
                {
                    SvMain.Rows.Add(rowsHeaderBeginRowIndex, 1);
                    for (int i = 0; i < SvMain.ColumnCount; i++)
                    {
                        Function.CloneSheetViewCell(SvMain.Cells[rowsHeaderBeginRowIndex, i], SvMain.Cells[rowTotIndex + 1, i]);
                    }
                    SvMain.Rows.Remove(rowTotIndex + 1, 1);
                }
            }
            #endregion

            #region ���ܼ���ǰ
            if (this.IsRowTotFrontShow)
            {
                int columnTotIndex = -1;
                //�������ܼ�
                for (int i = 0; i < SvMain.ColumnCount; i++)
                {
                    if (SvMain.Cells[rowsHeaderBeginRowIndex - 1, i].Text == "�ܼ�")
                    {
                        columnTotIndex = i;
                        break;
                    }
                }
                if (columnTotIndex != -1)
                {
                    SvMain.Columns.Add(columnsHeaderBeginColumnIndex, 1);

                    for (int i = 0; i < SvMain.RowCount; i++)
                    {
                        Function.CloneSheetViewCell(SvMain.Cells[i, columnsHeaderBeginColumnIndex], SvMain.Cells[i, columnTotIndex + 1]);
                    }
                    SvMain.Columns.Remove(columnTotIndex + 1, 1);
                }
            }
            #endregion

            #region ���ù̶�
            SvMain.FrozenColumnCount = this.FrozenColumnCount;
            SvMain.FrozenRowCount = this.FrozenRowCount;
            #endregion
            #endregion

            #endregion

            #region ���÷�ҳ��
            if (this.rowPageBreak > 0)
            {
                for (int i = 1; (i * this.rowPageBreak + this.DataBeginRowIndex) + 1 < this.SvMain.Rows.Count; i++)
                {
                    this.SvMain.SetRowPageBreak((i * this.rowPageBreak + this.DataBeginRowIndex) + 1, true);
                }
            }
            #endregion

            Function.DrawGridLine(SvMain, this.dataBeginRowIndex, this.dataBeginColumnIndex, dtCrossViewRows.Rows.Count + dtCrossViewColumnsTitleIncludValues.Columns.Count, dtCrossViewRows.Columns.Count + dtCrossViewRowsTitle.Columns.Count);

            this.SvMain.RowCount = this.DataBeginRowIndex + dtCrossViewRows.Rows.Count + dtCrossViewColumnsTitleIncludValues.Columns.Count;
            this.SvMain.ColumnCount = this.DataBeginColumnIndex + dtCrossViewRows.Columns.Count + dtCrossViewRowsTitle.Columns.Count;
            
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            #region �ϲ���ͬ��

            #endregion
            Cursor = Cursors.Arrow;

            return 1;
        }
        protected virtual int SpanDisplayedFp(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int ColumnSpan = 0;
            int dtRowIdx = 0;
            string tempVal = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                dtRowIdx = dt.Rows.IndexOf(dr);
                int dtColumnIdx = 0;
                ColumnSpan = 1;
                tempVal = string.Empty;
                foreach (DataColumn dc in dt.Columns)
                {
                    dtColumnIdx = (dt.Columns.Count - 1) - dt.Columns.IndexOf(dc);
                    tempVal = dr[dtColumnIdx].ToString();
                    if (string.IsNullOrEmpty(tempVal) == false)
                    {
                        break;
                    }
                    else
                    {
                        ColumnSpan++;
                    }
                }
                sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].ColumnSpan = ColumnSpan;
                //sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                if (sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].Text == "�ܼ�")
                {
                    sv.Cells[dtRowIdx + beginRowIdx, dtColumnIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                }
            }
            return 1;
        }
        protected virtual int SpanDisplayedFpReverseOneValue(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int RowSpan = 0;
            int dtRowIdx = 0;
            string tempVal = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                dtRowIdx = dt.Rows.IndexOf(dr);
                int dtColumnIdx = 0;
                RowSpan = 1;
                tempVal = string.Empty;
                foreach (DataColumn dc in dt.Columns)
                {
                    dtColumnIdx = (dt.Columns.Count - 1) - dt.Columns.IndexOf(dc);
                    tempVal = dr[dtColumnIdx].ToString();
                    if (string.IsNullOrEmpty(tempVal) == false)
                    {
                        break;
                    }
                    else
                    {
                        RowSpan++;
                    }
                }
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].RowSpan = RowSpan;
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="dt"></param>
        /// <param name="beginRowIdx"></param>
        /// <param name="beginColumnIdx"></param>
        /// <returns></returns>
        protected virtual int SpanDisplayedFpReverse(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int RowSpan = 0;
            int dtRowIdx = 0;
            string tempVal = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                dtRowIdx = dt.Rows.IndexOf(dr);
                int dtColumnIdx = 0;
                RowSpan = 1;
                tempVal = string.Empty;
                foreach (DataColumn dc in dt.Columns)
                {
                    dtColumnIdx = (dt.Columns.Count - 1) - dt.Columns.IndexOf(dc);
                    if (dtColumnIdx < dt.Columns.Count - 1)
                    {
                        tempVal = dr[dtColumnIdx].ToString();
                        if (string.IsNullOrEmpty(tempVal) == false)
                        {
                            break;
                        }
                        else
                        {
                            RowSpan++;
                        }
                    }
                }
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].RowSpan = RowSpan;
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sv.Cells[dtColumnIdx + beginRowIdx, dtRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="dt"></param>
        /// <param name="beginRowIdx"></param>
        /// <param name="beginColumnIdx"></param>
        /// <returns></returns>
        protected virtual int SpanDisplayedFpReverseRowsOneValue(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int ColumnSpan = 0;
            string tempVal = string.Empty;
            int dtColumnIdx = 0;
            int spanRowIdx = 0;
            int spanColumnIdx = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                dtColumnIdx = dt.Columns.IndexOf(dc);
                if (dtColumnIdx < dt.Columns.Count)
                {
                    ColumnSpan = 1;
                    tempVal = string.Empty;
                    int dtRowIdx = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dtRowIdx = dt.Rows.IndexOf(dr);

                        //tempVal = dr[dtColumnIdx].ToString();
                        if (string.IsNullOrEmpty(tempVal) == false)
                        {
                            if (tempVal == dr[dtColumnIdx].ToString())
                            {
                                ColumnSpan++;
                                if (dtRowIdx == dt.Rows.Count - 1)
                                {
                                    //tempVal = dr[dtColumnIdx].ToString();
                                    //spanRowIdx = dtRowIdx;
                                    //spanColumnIdx = dtColumnIdx;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].ColumnSpan = ColumnSpan;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                }
                            }
                            else
                            {
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].ColumnSpan = ColumnSpan;
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                tempVal = dr[dtColumnIdx].ToString();
                                spanRowIdx = dtRowIdx;
                                spanColumnIdx = dtColumnIdx;
                                ColumnSpan = 1;
                                // break;

                            }
                        }
                        else
                        {
                            tempVal = dr[dtColumnIdx].ToString();
                            spanRowIdx = dtRowIdx;
                            spanColumnIdx = dtColumnIdx;
                            ColumnSpan = 1;
                            //ColumnSpan++;
                        }

                    }

                }
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="dt"></param>
        /// <param name="beginRowIdx"></param>
        /// <param name="beginColumnIdx"></param>
        /// <returns></returns>
        protected virtual int SpanDisplayedFpReverseRows(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int ColumnSpan = 0;
            string tempVal = string.Empty;
            int dtColumnIdx = 0;
            int spanRowIdx = 0;
            int spanColumnIdx = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                dtColumnIdx = dt.Columns.IndexOf(dc);
                if (dtColumnIdx < dt.Columns.Count - 1)
                {
                    int dtRowIdx = 0;
                    ColumnSpan = 1;
                    tempVal = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dtRowIdx = dt.Rows.IndexOf(dr);


                        //tempVal = dr[dtColumnIdx].ToString();
                        if (string.IsNullOrEmpty(tempVal) == false)
                        {
                            if (tempVal == dr[dtColumnIdx].ToString())
                            {
                                ColumnSpan++;
                                //if (dtRowIdx==dt.Rows.Count -1 && dtColumnIdx==0)
                                if (dtRowIdx == dt.Rows.Count - 1)
                                {
                                    //tempVal = dr[dtColumnIdx].ToString();
                                    //spanRowIdx = dtRowIdx;
                                    //spanColumnIdx = dtColumnIdx;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].ColumnSpan = ColumnSpan;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                    sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                }
                            }
                            else
                            {
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].ColumnSpan = ColumnSpan;
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                sv.Cells[spanColumnIdx + beginRowIdx, spanRowIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                tempVal = dr[dtColumnIdx].ToString();
                                spanRowIdx = dtRowIdx;
                                spanColumnIdx = dtColumnIdx;
                                ColumnSpan = 1;
                                //break;

                            }
                        }
                        else
                        {
                            tempVal = dr[dtColumnIdx].ToString();
                            spanRowIdx = dtRowIdx;
                            spanColumnIdx = dtColumnIdx;
                            ColumnSpan = 1;
                            //ColumnSpan++;
                        }

                    }

                }
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sv"></param>
        /// <param name="dt"></param>
        /// <param name="beginRowIdx"></param>
        /// <param name="beginColumnIdx"></param>
        /// <returns></returns>
        protected virtual int SpanDisplayedFpRows(FarPoint.Win.Spread.SheetView sv, DataTable dt, int beginRowIdx, int beginColumnIdx)
        {
            int RowsSpan = 0;
            string tempVal = string.Empty;
            int dtColumnIdx = 0;
            int spanRowIdx = 0;
            int spanColumnIdx = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                dtColumnIdx = dt.Columns.IndexOf(dc);
                //���һ�в��ϲ�
                if (dtColumnIdx < dt.Columns.Count - 1)
                {
                    int dtRowIdx = 0;
                    RowsSpan = 1;
                    tempVal = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        dtRowIdx = dt.Rows.IndexOf(dr);
                        //��Ϊ��
                        if (string.IsNullOrEmpty(tempVal) == false)
                        {
                            //���
                            if (tempVal == dr[dtColumnIdx].ToString())
                            {
                                //tempVal = dr[dtColumnIdx].ToString();
                                RowsSpan++;
                                if (dtRowIdx == dt.Rows.Count - 1)
                                {
                                    //tempVal = dr[dtColumnIdx].ToString();
                                    //spanRowIdx = dtRowIdx;
                                    //spanColumnIdx = dtColumnIdx;
                                    //�ϲ���
                                    sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].RowSpan = RowsSpan;
                                    sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                    sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                }
                            }
                            //�����
                            else
                            {
                                //�ϲ���
                                sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].RowSpan = RowsSpan;
                                sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                                sv.Cells[spanRowIdx + beginRowIdx, spanColumnIdx + beginColumnIdx].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                                //���²���
                                tempVal = dr[dtColumnIdx].ToString();
                                spanRowIdx = dtRowIdx;
                                spanColumnIdx = dtColumnIdx;
                                RowsSpan = 1;
                                //break;
                            }
                        }
                        //Ϊ��
                        else
                        {
                            tempVal = dr[dtColumnIdx].ToString();
                            spanRowIdx = dtRowIdx;
                            spanColumnIdx = dtColumnIdx;
                            RowsSpan = 1;
                            //RowsSpan++;
                        }
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// �ϼƽ������ݱ���
        /// </summary>
        /// <param name="dtCrossValues"></param>
        /// <param name="dtCrossColumns"></param>
        /// <param name="fieldIndexsColumns"></param>
        /// <param name="fieldIndexsValues"></param>
        /// <returns></returns>
        protected virtual DataTable TotalCrossDataTableColumns(DataTable dtCrossValues, DataTable dt, string[] fieldIndexsColumns, string[] fieldIndexsValues)
        {
            DataSetHelper dsh = new DataSetHelper();
            #region �������ݣ�����ֱ�۵��㷨ʵ��
            DataTable dtCrossValuesColumns = dsh.SelectDistinctByIndexs("", dt, fieldIndexsColumns);
            //���������ݼ�����ֵ���飻
            object[] tempRowsValue;
            //��ǰ�����������
            int computedIdx;

            #region ���������ݼ�����ֵ����,�ĳ���Ϊ�����еĳ���
            //tempRowsValue[2]
            //��	��	2	��	��
            //��	��	��	��	��
            //��		��		��
            //��	��	��	��	��
            tempRowsValue = new object[fieldIndexsColumns.Length];
            computedIdx = 0;

            ///�ݴ�ϼ�ֵ����һά�ĳ����ǣ����ֶ������ĳ���
            ///�ڶ�ά�ĳ����ǣ�ֵ�������
            object[,] tempTotalValues;
            //tempTotalValues[2*2,4]          
            //��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��
            //��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��
            //��		��		��		��		��		��		��		��		��		��		��		��		��		��		��		��		��
            //��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��
            //tempTotalValues[2*2,4]          
            //��	��	2	��	��	��	2	��	��	��
            //��	��	��	��	��	��	��	��	��	��
            //��		��		��		��		��	��
            //��		��		��		��		��	4
            //��		��		��		��		��	��
            //��		��		��		��		��	��
            //��	��	��	��	��	��	��	��	��	��

            tempTotalValues = new object[fieldIndexsColumns.Length * fieldIndexsValues.Length, dtCrossValues.Rows.Count];
            #region �ϼ�ֵ��ʼ��Ϊ0
            int valueRowIdxDefault = 0;
            ///�ϼƺ��ֵ��
            DataTable dtCrossValuesTotalCross = new DataTable();
            //����������
            //��ʼ��Ϊ��0��
            //��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��
            //��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��
            //��	0	��	0	��	0	��	0	��	0	��	0	��	0	��	0	��	0	��	0	��	0	��	0	��	0	��	0	��	0	��	0	��
            //��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��
            //��	��	2	��	��	��	2	��	��	��
            //��	��	��	��	��	��	��	��	��	��
            //��	0	��	0	��	0	��	0	��	��
            //��	0	��	0	��	0	��	0	��	4
            //��	0	��	0	��	0	��	0	��	��
            //��	0	��	0	��	0	��	0	��	��
            //��	��	��	��	��	��	��	��	��	��

            foreach (DataRow dr in dtCrossValues.Rows)
            {
                valueRowIdxDefault = dtCrossValues.Rows.IndexOf(dr);
                //����������
                for (int i = 0; i < dtCrossValuesColumns.Columns.Count; i++)
                {
                    //��������ֵ
                    for (int j = 0; j < fieldIndexsValues.Length; j++)
                    {
                        //��ʼ��Ϊ��0��
                        #region {4B8178AF-8ABF-409d-B241-89EB7E311224}
                        //tempTotalValues[i * fieldIndexsValues.Length + j * (fieldIndexsValues.Length - 1), valueRowIdxDefault] = "0";
                        tempTotalValues[i * fieldIndexsValues.Length + j, valueRowIdxDefault] = "0"; 
                        #endregion
                    }
                }
                //��ӿհ����ڽ������ݼ���
                dtCrossValuesTotalCross.Rows.Add(dtCrossValuesTotalCross.NewRow());
            }
            //dtCrossValuesTotalCross
            //��	��	��
            //��	��	��
            //��	��	��
            //��	��	4
            //��	��	��
            //��	��	��
            //��	��	��


            #endregion
            //ѭ�����н��������ݼ�����
            while (computedIdx < dtCrossValuesColumns.Rows.Count)
            {
                //ȡ���������ݼ���ǰ��ֵ
                //��	��	2	��	��
                //��	��	��	��	��
                //��	A	��	C	��
                //��	��	��	��	��
                tempRowsValue = dtCrossValuesColumns.Rows[computedIdx].ItemArray;
                #region ����ÿһ����ĺϼ�ֵ
                //��������ֵ
                for (int j = 0; j < fieldIndexsValues.Length; j++)
                {
                    //��Ϊ����ģʽ��ӣ�����Ҫÿ���½���
                    //dtCrossValuesTotalCross
                    //��	��	��	��	��+����
                    //��	��	��	��	��
                    dtCrossValuesTotalCross.Columns.Add(
                        new DataColumn("", dt.Columns[int.Parse(fieldIndexsValues[j].ToString())].DataType));
                }
                //dtCrossValuesTotalCross whileѭ�����Ժ������
                //��	��	2	��	��	��	2	��	��	��	2	��	��	��	2	��	��	��
                //��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��
                //��		��		��		��		��		��		��		��		��	��
                //��		��		��		��		��		��		��		��		��	4
                //��		��		��		��		��		��		��		��		��	��
                //��		��		��		��		��		��		��		��		��	��
                //��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��	��
                int valueRowIdx = 0;
                //��������ֵ���ۼ�

                foreach (DataRow dr in dtCrossValues.Rows)
                {
                    //����ֵ����(0-3)
                    valueRowIdx = dtCrossValues.Rows.IndexOf(dr);
                    //�������������ݼ�ÿһ�У��ݴ�ֵ���ۼ�����
                    //i(0-1)
                    for (int i = 0; i < dtCrossValuesColumns.Columns.Count; i++)
                    {
                        //��������ֵ
                        //j(0-1)
                        for (int j = 0; j < fieldIndexsValues.Length; j++)
                        {
                            //tempTotalValues[i * (fieldIndexsValues.Length) + j * (fieldIndexsValues.Length - 1), valueRowIdx] = (int.Parse(tempTotalValues[i * (fieldIndexsValues.Length) + j * (fieldIndexsValues.Length - 1), valueRowIdx].ToString()) + int.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString())).ToString();
                            switch (dr.Table.Columns[computedIdx * fieldIndexsValues.Length + j].DataType.ToString())
                            {
                                #region {4B8178AF-8ABF-409d-B241-89EB7E311224}
                                case "System.Decimal":
                                    {
                                        //tempTotalValues[i * (fieldIndexsValues.Length)
                                        //    + j * (fieldIndexsValues.Length - 1), valueRowIdx]
                                        //    = (Decimal.Parse(tempTotalValues[i * (fieldIndexsValues.Length)
                                        //    + j * (fieldIndexsValues.Length - 1), valueRowIdx].ToString())
                                        //    + Decimal.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString())).ToString();
                                        tempTotalValues[i * fieldIndexsValues.Length
                                            + j, valueRowIdx]
                                            = (Decimal.Parse(tempTotalValues[i * fieldIndexsValues.Length
                                            + j, valueRowIdx].ToString())
                                            + Decimal.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString())).ToString();
                                        break;
                                    }
                                case "System.DateTime":
                                    {
                                        tempTotalValues[i * fieldIndexsValues.Length
                                          + j, valueRowIdx] = dr[computedIdx * fieldIndexsValues.Length + j].ToString();
                                        break;
                                    }
                                case "System.String":
                                    {
                                        tempTotalValues[i * fieldIndexsValues.Length
                                          + j, valueRowIdx] = dr[computedIdx * fieldIndexsValues.Length + j].ToString();
                                        break;
                                    }
                                default:
                                    {
                                        tempTotalValues[i * fieldIndexsValues.Length
                                          + j, valueRowIdx]
                                          = (int.Parse(tempTotalValues[i * fieldIndexsValues.Length
                                          + j, valueRowIdx].ToString())
                                            + int.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString())).ToString();
                                        break;
                                    } 
                                #endregion
                            }
                        }
                        //tempTotalValues[2*2,4]          
                        //��	��	2	��	��	��	2	��	��	��
                        //��	��	��	��	��	��	��	��	��	��
                        //��	1	��	0	��	0	��	0	��	��
                        //��	1	��	0	��	0	��	0	��	4
                        //��	1	��	0	��	0	��	0	��	��
                        //��	1	��	0	��	0	��	0	��	��
                        //��	��	��	��	��	��	��	��	��	��
                    }
                    //tempTotalValues[2*2,4]          
                    //��	��	2	��	��	��	2	��	��	��
                    //��	��	��	��	��	��	��	��	��	��
                    //��	1	��	0	��	1	��	0	��	��
                    //��	1	��	0	��	1	��	0	��	4
                    //��	1	��	0	��	1	��	0	��	��
                    //��	1	��	0	��	1	��	0	��	��
                    //��	��	��	��	��	��	��	��	��	��
                    //��������ֵ������ԭֵ
                    for (int j = 0; j < fieldIndexsValues.Length; j++)
                    {
                        ///��Ϊ����ǿյ������У����������������Ҫ���ƣ�����ֵ������ĵ�ǰ�е�ÿһ��ֵ
                        //dtCrossValuesTotalCross.Rows[valueRowIdx][dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)] = (int.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString())).ToString();
                        switch (dr.Table.Columns[computedIdx * fieldIndexsValues.Length + j].DataType.ToString())
                        {
                            case "System.Decimal":
                                {
                                    dtCrossValuesTotalCross.Rows[valueRowIdx]
                                        [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                                        = (Decimal.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString()));
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    dtCrossValuesTotalCross.Rows[valueRowIdx]
                                        [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                                        = dr[computedIdx * fieldIndexsValues.Length + j];
                                    break;
                                }
                            case "System.String":
                                {
                                    dtCrossValuesTotalCross.Rows[valueRowIdx]
                                        [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                                        = dr[computedIdx * fieldIndexsValues.Length + j];
                                    break;
                                }
                            default:
                                {
                                    dtCrossValuesTotalCross.Rows[valueRowIdx]
                                        [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                                        = (int.Parse(dr[computedIdx * fieldIndexsValues.Length + j].ToString()));
                                    break;
                                }
                        }
                    }
                }
                #endregion
                #region �������н����м�
                    for (int i = dtCrossValuesColumns.Columns.Count - 2; i >= 0; i--)
                    {
                        //tempTitle = tempTitle + "|" + dtCrossValuesColumns.Rows[computedIdx][i] + "|";
                        //ѭ�������У�����ѵ�������л�ֵ��ͬ���ж�˳���ܵߵ���������һ���ϼ���              
                        if ((computedIdx < dtCrossValuesColumns.Rows.Count - 1)
                            && (tempRowsValue[i].ToString()
                            == dtCrossValuesColumns.Rows[computedIdx + 1][i].ToString()))
                        {
                            //���������һ��
                            continue;
                        }
                        //�����ֵ��ͬ
                        else
                        {

                            #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                            bool isTotal = false;
                            if (columnsTotalLevel.Length < dtCrossValuesColumns.Columns.Count - 2 - i ||
                            (columnsTotalLevel.Length >= dtCrossValuesColumns.Columns.Count - 2 - i
                            && columnsTotalLevel[dtCrossValuesColumns.Columns.Count - 2 - i] != "0"))
                            {
                                //��������ֵ
                                for (int j = 0; j < fieldIndexsValues.Length; j++)
                                {
                                    ///��һ���ϼ��У�����Ϊ�գ�
                                    dtCrossValuesTotalCross.Columns.Add(
                                        new DataColumn(
                                            "", dt.Columns[int.Parse(fieldIndexsValues[j].ToString())].DataType));
                                }
                                isTotal = true;
                            } 
                            #endregion
                            int valueRowIdxSum = 0;
                            foreach (DataRow dr in dtCrossValues.Rows)
                            {
                                valueRowIdxSum = dtCrossValues.Rows.IndexOf(dr);
                                //��������ֵ
                                for (int j = 0; j < fieldIndexsValues.Length; j++)
                                {
                                    #region {4B8178AF-8ABF-409d-B241-89EB7E311224}
                                    #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                                    if (isTotal == true)
                                    {
                                        ///���ݵ�ǰ������д���Ӧ�ϼ�ֵ
                                        dtCrossValuesTotalCross.Rows[valueRowIdxSum]
                                            [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                                            = tempTotalValues[i * fieldIndexsValues.Length
                                          + j, valueRowIdxSum];
                                    }
                                    #endregion
                                    //д��һ�����һ��
                                    tempTotalValues[i * fieldIndexsValues.Length
                                          + j, valueRowIdxSum] = "0"; 
                                    #endregion
                                }
                            }

                        }
                    }
                
                #endregion
                //��ǰ������һλ
                computedIdx++;
            }
            #region ����һ���ܼ�
            #region {9F609C45-B357-4807-A1E1-3741F08D471A}
            if (columnsTotalLevel.Length < dataCrossColumns.Length ||
                 (this.columnsTotalLevel.Length >= this.dataCrossColumns.Length
                 && this.columnsTotalLevel[this.dataCrossColumns.Length - 1] != "0")) 
            #endregion
            {

                //��������ֵ
                for (int j = 0; j < fieldIndexsValues.Length; j++)
                {
                    ///��һ���ϼ��У�����Ϊ�գ�
                    dtCrossValuesTotalCross.Columns.Add(
                        new DataColumn("", dt.Columns[int.Parse(fieldIndexsValues[j].ToString())].DataType));
                }
                int valueRowIdxGrandTotal = 0;
                foreach (DataRow dr in dtCrossValues.Rows)
                {
                    valueRowIdxGrandTotal = dtCrossValues.Rows.IndexOf(dr);
                    //��������ֵ
                    for (int j = 0; j < fieldIndexsValues.Length; j++)
                    {
                        #region {4B8178AF-8ABF-409d-B241-89EB7E311224}
                        /////���ݵ�ǰ������д���Ӧ�ϼ�ֵ
                        //dtCrossValuesTotalCross.Rows[valueRowIdxGrandTotal]
                        //    [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                        //    = tempTotalValues[(fieldIndexsValues.Length) * (dtCrossValuesColumns.Columns.Count - 1)
                        //    + j * (fieldIndexsValues.Length - 1), valueRowIdxGrandTotal];
                        ////д��һ�����һ��
                        //tempTotalValues[(fieldIndexsValues.Length) * (dtCrossValuesColumns.Columns.Count - 1)
                        //    + j * (fieldIndexsValues.Length - 1), valueRowIdxGrandTotal] = "0";
                        ///���ݵ�ǰ������д���Ӧ�ϼ�ֵ
                        dtCrossValuesTotalCross.Rows[valueRowIdxGrandTotal]
                            [dtCrossValuesTotalCross.Columns.Count - 1 - (fieldIndexsValues.Length - 1 - j)]
                            = tempTotalValues[(fieldIndexsValues.Length) * (dtCrossValuesColumns.Columns.Count - 1)
                            + j, valueRowIdxGrandTotal];
                        //д��һ�����һ��
                        tempTotalValues[(fieldIndexsValues.Length) * (dtCrossValuesColumns.Columns.Count - 1)
                            + j, valueRowIdxGrandTotal] = "0";

                        #endregion
                    }
                } 
            }
            
            #endregion
            #endregion
            return dtCrossValuesTotalCross;
            #endregion
        }
        /// <summary>
        /// �ϼƽ������ݱ���
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fieldIndexs"></param>
        /// <returns></returns>
        protected virtual DataTable TotalCrossDataTableRows(DataTable dtCrossValues, DataTable dtCrossRows, string[] fieldIndexs)
        {
            DataSetHelper dsh = new DataSetHelper();

            #region �������ݣ�����ֱ�۵��㷨ʵ��

            DataTable dtCrossValuesRows = dsh.SelectDistinctByIndexs("", dtCrossRows, fieldIndexs);
            object[] tempRowsValue;
            int computedIdx;

            #region �����м�
            tempRowsValue = new object[fieldIndexs.Length];
            computedIdx = 0;

            ///�ݴ�ϼ�ֵ����һά�ĳ����ǣ����ֶ������ĳ���
            ///�ڶ�ά�ĳ����ǣ�ֵ�������
            object[,] tempTotalValues;
            tempTotalValues = new object[fieldIndexs.Length, dtCrossValues.Columns.Count];
            #region �ϼ�ֵ��ʼ��Ϊ0
            ///�ϼƺ��ֵ��
            DataTable dtCrossValuesTotalCross = dtCrossValues.Clone();
            for (int i = 0; i < dtCrossValuesRows.Columns.Count; i++)
            {
                int valueColumnIdxDefault = 0;
                foreach (DataColumn dc in dtCrossValues.Columns)
                {
                    valueColumnIdxDefault = dtCrossValues.Columns.IndexOf(dc);
                    tempTotalValues[i, valueColumnIdxDefault] = "0";
                }
            }
            #endregion
            //ѭ��������
            while (computedIdx < dtCrossValuesRows.Rows.Count)
            {
                //ȡ��ǰ��ֵ
                tempRowsValue = dtCrossValuesRows.Rows[computedIdx].ItemArray;
                #region �ݴ�
                dtCrossValuesTotalCross.Rows.Add(dtCrossValues.Rows[computedIdx].ItemArray);

                for (int i = 0; i < dtCrossValuesRows.Columns.Count; i++)
                {
                    ///���������У�
                    int valueColumnIdx;
                    valueColumnIdx = 0;
                    foreach (DataColumn dc in dtCrossValues.Columns)
                    {
                        valueColumnIdx = dtCrossValues.Columns.IndexOf(dc);
                        //tempTotalValues[i, valueColumnIdx] = (int.Parse(tempTotalValues[i, valueColumnIdx].ToString()) + int.Parse(dtCrossValues.Rows[computedIdx][valueColumnIdx].ToString())).ToString();
                        switch (dtCrossValues.Columns[valueColumnIdx].DataType.ToString())
                        {
                            case "System.Decimal":
                                {
                                    tempTotalValues[i, valueColumnIdx] = (Decimal.Parse(tempTotalValues[i, valueColumnIdx].ToString()) + Decimal.Parse(dtCrossValues.Rows[computedIdx][valueColumnIdx].ToString())).ToString();
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    tempTotalValues[i, valueColumnIdx] = dtCrossValues.Rows[computedIdx][valueColumnIdx].ToString();
                                    break;
                                }
                            case "System.String":
                                {
                                    tempTotalValues[i, valueColumnIdx] = dtCrossValues.Rows[computedIdx][valueColumnIdx].ToString();
                                    break;
                                }
                            default:
                                {
                                    tempTotalValues[i, valueColumnIdx] = (int.Parse(tempTotalValues[i, valueColumnIdx].ToString()) + int.Parse(dtCrossValues.Rows[computedIdx][valueColumnIdx].ToString())).ToString();
                                    break;
                                }
                        }
                    }
                }

                #endregion
                //ѭ�������У����ֵ��ͬ����һ��������
                for (int i = dtCrossValuesRows.Columns.Count - 2; i >= 0; i--)
                {
                    //����������һ������ֵ��ͬ
                    if ((computedIdx < dtCrossValuesRows.Rows.Count - 1) && (tempRowsValue[i].ToString() == dtCrossValuesRows.Rows[computedIdx + 1][i].ToString()))
                    {
                        //���������һ��
                        continue;
                    }
                    //�����ֵ��ͬ
                    else
                    {
                        #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                        bool isTotal = false;
                        if (rowsTotalLevel.Length < dtCrossValuesRows.Columns.Count - 2 - i ||
                            (rowsTotalLevel.Length >= dtCrossValuesRows.Columns.Count - 2 - i
                            && rowsTotalLevel[dtCrossValuesRows.Columns.Count - 2 - i] != "0"))
                        {
                            ///��һ���ϼ���
                            dtCrossValuesTotalCross.Rows.Add(dtCrossValuesTotalCross.NewRow());
                            isTotal = true;
                        } 
                        #endregion
                        int valueColumnIdxSum = 0;
                        foreach (DataColumn dc in dtCrossValues.Columns)
                        {
                            valueColumnIdxSum = dtCrossValues.Columns.IndexOf(dc);
                            #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                            ///���ݵ�ǰ������д���Ӧ�ϼ�ֵ
                            if (isTotal == true)
                            {
                                dtCrossValuesTotalCross.Rows[dtCrossValuesTotalCross.Rows.Count - 1][valueColumnIdxSum] = tempTotalValues[i, valueColumnIdxSum];
                            }  
                            #endregion
                            tempTotalValues[i, valueColumnIdxSum] = "0";
                        }
                    }
                }
                //��ǰ������һλ
                computedIdx++;
            }
            #region ���ܼ�
            #region {9F609C45-B357-4807-A1E1-3741F08D471A}
            if (rowsTotalLevel.Length < dataCrossRows.Length ||
                (this.rowsTotalLevel.Length >= this.dataCrossRows.Length
                && this.rowsTotalLevel[this.dataCrossRows.Length - 1] != "0"))
            {
                ///��һ���ϼ���
                dtCrossValuesTotalCross.Rows.Add(dtCrossValuesTotalCross.NewRow());
                int valueColumnIdxGrandTotal = 0;
                foreach (DataColumn dc in dtCrossValues.Columns)
                {
                    valueColumnIdxGrandTotal = dtCrossValues.Columns.IndexOf(dc);
                    ///���ݵ�ǰ������д���Ӧ�ϼ�ֵ
                    dtCrossValuesTotalCross.Rows[dtCrossValuesTotalCross.Rows.Count - 1][valueColumnIdxGrandTotal] = tempTotalValues[dtCrossValuesRows.Columns.Count - 1, valueColumnIdxGrandTotal];
                    tempTotalValues[dtCrossValuesRows.Columns.Count - 1, valueColumnIdxGrandTotal] = "0";
                }
            } 
            #endregion
            #endregion
            #endregion

            return dtCrossValuesTotalCross;
            #endregion
        }
        /// <summary>
        /// �������ݱ�
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fieldIndexs"></param>
        /// <returns></returns>
        protected virtual DataTable CrossDataTable(DataTable dt, string[] fieldIndexs, string[] fieldIndexsTotleLevel)
        {
            DataSetHelper dsh = new DataSetHelper();

            #region �������ݣ�����ֱ�۵��㷨ʵ��
            #region {0D9300B1-E85A-486d-96CD-D41A508A74F4} ���������뽻���������ֵ�ʱ��ǿ��תΪ�ַ�
            DataTable dtStringRows = dsh.SelectDistinctByIndexs("", dt, fieldIndexs);

            DataTable dtRows = new DataTable();
            foreach (DataColumn dc in dtStringRows.Columns)
            {
                dtRows.Columns.Add(dc.Caption, typeof(System.String));
            }
            foreach (DataRow dr in dtStringRows.Rows)
            {
                dtRows.Rows.Add(dr.ItemArray);
            } 
            #endregion
            // DataTable dtColumns = dsh.SelectDistinctByIndexs("", dt, dataCrossColumns);
            object[] tempRowsValue;
            int computedIdx;
            int insertRowIdx;
            #region �����м�
            tempRowsValue = new object[fieldIndexs.Length];
            computedIdx = 0;
            insertRowIdx = 0;
            //ѭ��������
            while (computedIdx < dtRows.Rows.Count - 1)
            {
                //ȡ��ǰ��ֵ
                tempRowsValue = dtRows.Rows[computedIdx].ItemArray;
                string tempTitle = string.Empty;
                //ѭ�������У����ֵ��ͬ����һ��������
                for (int i = dtRows.Columns.Count - 2; i >= 0; i--)
                {
                    //tempTitle = tempTitle + "|" + tempRowsValue[i].ToString();
                    //if (computedIdx < dtRows.Rows.Count-1)
                    //{
                    //�����ֵ��ͬ
                    if (tempRowsValue[i].ToString() == dtRows.Rows[insertRowIdx + 1][i].ToString())
                    {
                        //���������һ��
                        continue;
                    }
                    //�����ֵ��ͬ
                    else
                    {
                        #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                        if (fieldIndexsTotleLevel.Length < dtRows.Columns.Count - 2 - i ||
                                         (fieldIndexsTotleLevel.Length >= dtRows.Columns.Count - 2 - i
                                         && fieldIndexsTotleLevel[dtRows.Columns.Count - 2 - i] != "0"))
                        {
                            //�½�һ����
                            DataRow drNew = dtRows.NewRow();
                            for (int j = i; j >= 0; j--)
                            {
                                drNew[j] = tempRowsValue[j].ToString();
                            }
                            //���½��е���ֵ��ͬ���и�ֵΪ��ǰ�еĶ�Ӧ��ֵ
                            drNew[i + 1] = tempTitle + "С��";
                            //�����м���λ������һ��
                            insertRowIdx++;
                            ////�жϵ�ǰ�����е�λ���Ƿ���С�����������
                            //if (insertRowIdx <= dtRows.Rows.Count - 1)
                            //{
                            //    //���������ڲ�����
                            dtRows.Rows.InsertAt(drNew, insertRowIdx);
                            //}
                            //else
                            //{
                            //    //׷�����������
                            //    dtRows.Rows.Add(drNew);
                            //} 
                        } 
                        #endregion
                    }
                    //}
                    //else
                    //{

                    //}
                }
                //��ǰ������һλ
                computedIdx = insertRowIdx + 1;
                insertRowIdx = computedIdx;
            }
            //�������һ������
            if (dtRows.Rows.Count > 0)
            {
                //ȡ��ǰ��ֵ
                tempRowsValue = dtRows.Rows[dtRows.Rows.Count - 1].ItemArray;
                string tempTitle = string.Empty;
                //ѭ�������У����ֵ��ͬ����һ��������
                for (int i = dtRows.Columns.Count - 2; i >= 0; i--)
                {

                    #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                    if (fieldIndexsTotleLevel.Length < dtRows.Columns.Count - 2 - i ||
                                      (fieldIndexsTotleLevel.Length >= dtRows.Columns.Count - 2 - i
                                      && fieldIndexsTotleLevel[dtRows.Columns.Count - 2 - i] != "0"))
                    {
                        //tempTitle = tempTitle + "|" + tempRowsValue[i].ToString();
                        //�½�һ����
                        DataRow drNew = dtRows.NewRow();
                        for (int j = i; j >= 0; j--)
                        {
                            drNew[j] = tempRowsValue[j].ToString();
                        }
                        //���½��е���ֵ��ͬ���и�ֵΪ��ǰ�еĶ�Ӧ��ֵ
                        drNew[i + 1] = "С��";

                        //׷�����������
                        dtRows.Rows.Add(drNew);
                    }

                    #endregion
                }
                #region {9F609C45-B357-4807-A1E1-3741F08D471A}
                if (fieldIndexsTotleLevel.Length < fieldIndexs.Length ||
                          (fieldIndexsTotleLevel.Length >= fieldIndexs.Length
                          && fieldIndexsTotleLevel[fieldIndexs.Length - 1] != "0"))
                {
                    //�½�һ����
                    DataRow drNewGrandTotal = dtRows.NewRow();
                    drNewGrandTotal[0] = "�ܼ�";
                    dtRows.Rows.Add(drNewGrandTotal);
                } 
                #endregion
            }
            #endregion

            return dtRows;
            #endregion
        }
        protected virtual int CrossDataTable(DataTable dt, string[] fieldIndexs, ref DataTable dtRows, ref int[] totalAndGrand)
        {
            DataSetHelper dsh = new DataSetHelper();

            #region �������ݣ�����ֱ�۵��㷨ʵ��
            string totalAndGrandString = string.Empty;
            //DataTable dtRows = dsh.SelectDistinctByIndexs("", dt, fieldIndexs);
            dtRows = dsh.SelectDistinctByIndexs("", dt, fieldIndexs);
            // DataTable dtColumns = dsh.SelectDistinctByIndexs("", dt, dataCrossColumns);
            object[] tempRowsValue;
            int computedIdx;
            int insertRowIdx;
            #region �����м�
            tempRowsValue = new object[fieldIndexs.Length];
            computedIdx = 0;
            insertRowIdx = 0;
            //ѭ��������
            while (computedIdx < dtRows.Rows.Count - 1)
            {
                //ȡ��ǰ��ֵ
                tempRowsValue = dtRows.Rows[computedIdx].ItemArray;
                string tempTitle = string.Empty;
                //ѭ�������У����ֵ��ͬ����һ��������
                for (int i = dtRows.Columns.Count - 2; i >= 0; i--)
                {
                    //tempTitle = tempTitle + "|" + tempRowsValue[i].ToString();
                    //if (computedIdx < dtRows.Rows.Count-1)
                    //{
                    //�����ֵ��ͬ
                    if (tempRowsValue[i].ToString() == dtRows.Rows[insertRowIdx + 1][i].ToString())
                    {
                        //���������һ��
                        continue;
                    }
                    //�����ֵ��ͬ
                    else
                    {
                        //�½�һ����
                        DataRow drNew = dtRows.NewRow();
                        for (int j = i; j >= 0; j--)
                        {
                            drNew[j] = tempRowsValue[j].ToString();
                        }
                        //���½��е���ֵ��ͬ���и�ֵΪ��ǰ�еĶ�Ӧ��ֵ
                        drNew[i + 1] = tempTitle + "С��";
                        //�����м���λ������һ��
                        insertRowIdx++;
                        ////�жϵ�ǰ�����е�λ���Ƿ���С�����������
                        //if (insertRowIdx <= dtRows.Rows.Count - 1)
                        //{
                        //    //���������ڲ�����
                        dtRows.Rows.InsertAt(drNew, insertRowIdx);
                        totalAndGrandString = totalAndGrandString + insertRowIdx.ToString() + ",";
                        //}
                        //else
                        //{
                        //    //׷�����������
                        //    dtRows.Rows.Add(drNew);
                        //}
                    }
                    //}
                    //else
                    //{

                    //}
                }
                //��ǰ������һλ
                computedIdx = insertRowIdx + 1;
                insertRowIdx = computedIdx;
            }
            //�������һ������
            if (dtRows.Rows.Count > 0)
            {
                //ȡ��ǰ��ֵ
                tempRowsValue = dtRows.Rows[dtRows.Rows.Count - 1].ItemArray;
                string tempTitle = string.Empty;
                //ѭ�������У����ֵ��ͬ����һ��������
                for (int i = dtRows.Columns.Count - 2; i >= 0; i--)
                {

                    //tempTitle = tempTitle + "|" + tempRowsValue[i].ToString();
                    //�½�һ����
                    DataRow drNew = dtRows.NewRow();
                    for (int j = i; j >= 0; j--)
                    {
                        drNew[j] = tempRowsValue[j].ToString();
                    }
                    //���½��е���ֵ��ͬ���и�ֵΪ��ǰ�еĶ�Ӧ��ֵ
                    drNew[i + 1] = "С��";

                    //׷�����������
                    dtRows.Rows.Add(drNew);
                    totalAndGrandString = totalAndGrandString + (dt.Rows.Count - 1).ToString() + ",";
                }
                //�½�һ����
                DataRow drNewGrandTotal = dtRows.NewRow();
                totalAndGrandString = totalAndGrandString + (dt.Rows.Count - 1).ToString();
                drNewGrandTotal[0] = "�ܼ�";
                dtRows.Rows.Add(drNewGrandTotal);
            }
            #endregion
            string[] totalAndGrandStringArr = totalAndGrandString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            totalAndGrand = new int[totalAndGrandStringArr.Length];
            for (int i = 0; i < totalAndGrandStringArr.Length; i++)
            {
                totalAndGrand[i] = int.Parse(totalAndGrandStringArr[i]);
            }
            return 0;
            #endregion
        }
        protected virtual void OnSort()
        {

            ////����Ǳ�����
            //if (this.SvMain.ActiveRowIndex == this.dataBeginRowIndex)
            //{
            //    //����Ǳ�����
            //    if (this.SvMain.ActiveColumnIndex <= this.dataDisplayColumns.Length - 1)
            //    {
            //        //�½���������
            //        FarPoint.Win.Spread.SortInfo[] sort = new FarPoint.Win.Spread.SortInfo[1];
            //        //����������ȡԭ��������
            //        for (int i = 0; i < this.dataDisplayColumns.Length; i++)
            //        {
            //            bool ascending = true;
            //            if (i == this.SvMain.ActiveColumnIndex)
            //            {
            //                //û�С������ž�������
            //                if (SvMain.Cells[this.dataBeginRowIndex, i].Text.IndexOf("��") < 0)
            //                {
            //                    //
            //                    SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
            //                    ascending = false;
            //                    SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
            //                }
            //                else
            //                {
            //                    //
            //                    SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
            //                    ascending = true;
            //                    SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
            //                }
            //                //����������Ϣ
            //                sort[0] = new FarPoint.Win.Spread.SortInfo(i, ascending, System.Collections.Comparer.Default);
            //            }
            //            else
            //            {
            //                //û�С������ž�������
            //                if (SvMain.Cells[this.dataBeginRowIndex, i].Text.IndexOf("��") < 0)
            //                {
            //                    SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
            //                    //ascending = false;
            //                    //SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
            //                }
            //                else
            //                {
            //                    SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
            //                    //ascending = true;
            //                    //SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
            //                }
            //            }
            //            //����������Ϣ
            //            //sort[i] = new FarPoint.Win.Spread.SortInfo(i, ascending, System.Collections.Comparer.Default);
            //        }
            //        ////�½���������
            //        //FarPoint.Win.Spread.SortInfo[] sort = new FarPoint.Win.Spread.SortInfo[this.dataDisplayColumns.Length];
            //        ////����������ȡԭ��������
            //        //for (int i = 0; i < this.dataDisplayColumns.Length; i++)
            //        //{
            //        //    bool ascending = true;
            //        //    if (i == this.SvMain.ActiveColumnIndex )
            //        //    {
            //        //        //û�С������ž�������
            //        //        if (SvMain.Cells[this.dataBeginRowIndex, i].Text.IndexOf("��") < 0)
            //        //        {
            //        //            //
            //        //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
            //        //            ascending = false;
            //        //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
            //        //        }
            //        //        else
            //        //        {
            //        //            //
            //        //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
            //        //            ascending = true;
            //        //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
            //        //        }
            //        //    }
            //        //    else
            //        //    {
            //        //        //û�С������ž�������
            //        //        if (SvMain.Cells[this.dataBeginRowIndex, i].Text.IndexOf("��") <0)
            //        //        {
            //        //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
            //        //            ascending = false;
            //        //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
            //        //        }
            //        //        else
            //        //        {
            //        //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
            //        //            ascending = true;
            //        //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
            //        //        } 
            //        //    }
            //        //    //����������Ϣ
            //        //    sort[i] = new FarPoint.Win.Spread.SortInfo(i, ascending,System.Collections.Comparer.Default );
            //        //}
            //        SvMain.SortRange(this.dataBeginRowIndex + 1, 0, this.dataRowCount, this.dataDisplayColumns.Length - 1, true, sort);


            //    }
            //}
        }

        private void ComputeIndex()
        {
            rowsHeaderBeginColumnIndex = dataBeginColumnIndex;
            if (this.dataCrossValues.Length > 1)
            {
                rowsHeaderBeginRowIndex = dataBeginRowIndex + dataCrossColumns.Length + 1;
            }
            else
            {
                rowsHeaderBeginRowIndex = dataBeginRowIndex + dataCrossColumns.Length;
            }
            columnsHeaderBeginColumnIndex = dataBeginColumnIndex + dataCrossRows.Length; ;
            columnsHeaderBeginRowIndex = dataBeginRowIndex;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.IsShowDetail = false;
        }

        private void ucQueryBaseForDataWindow_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            this.OnLoad();

            this.Init();
        }

        private void cmbQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnQueryTree();
            }
        }

        private void btnQueryTree_Click(object sender, EventArgs e)
        {
            this.OnQueryTree();
        }

        #endregion

    }
}
