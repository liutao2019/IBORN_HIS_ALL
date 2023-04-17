using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Common.Report
{
    public partial class ucQueryBaseForFarPoint : FS.FrameWork.WinForms .Controls.ucBaseControl
    {
        public ucQueryBaseForFarPoint()
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
        protected bool isLeftVisible = true;

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
        /// ��ҳ�����ڵ�����
        /// </summary>
        private int rowPageBreak = -1;

        /// <summary>
        /// ����������ļ���λ��
        /// </summary>
        private string mainSheetXmlFileName = string.Empty;
       
        /// <summary>
        /// ��ϸ��������ļ���
        /// </summary>
        private string detailSheetXmlFileName = string.Empty;
        
        /// <summary>
        /// ���ݿ�ʼ�е�����
        /// </summary>
        private int dataBeginRowIndex = 0;
        /// <summary>
        /// ��ʾ��ʼ�е�����
        /// </summary>
        private int dataBeginColumnIndex = 0;
        /// <summary>
        /// ������ʾ������
        /// </summary>
        private string[] dataDisplayColumns =new  string[0] ;

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
        DB db = new DB();
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
        public string  HospitalName
        {
            get
            {
                if (DesignMode==false )
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
        public DB Db
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
        /// <summary>
        /// �������ݼ���������ʾ���У�
        /// </summary>
        [Category("�������"), Description("���ñ�����Ҫ��ʾ�������У�")]
        public string DataDisplayColumns
        {
            get
            {
                string rtn = string.Empty;
                if (dataDisplayColumns != null)
                {
                    rtn = string.Join("|", this.dataDisplayColumns);
                }
                return rtn;
            }
            set
            {
                dataDisplayColumns = value.Split('|');

            }
        }
        [Category("�������"), Description("���ñ����ѯ�������ݼ�����ʾ����ʼ�е�������")]
        public int DataBeginRowIndex
        {
            get { return dataBeginRowIndex; }
            set { dataBeginRowIndex = value; }
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
               
            }
        }
        [Category("�������"), Description("���ñ�������������ļ�����")]
        public string MainSheetXml
        {
            get { return mainSheetXmlFileName; }
            set { mainSheetXmlFileName = value; }
        }
        [Category("�������"), Description("���ñ�����ϸ��������ļ�����")]
        public string DetailSheetXml
        {
            get { return detailSheetXmlFileName; }
            set { detailSheetXmlFileName = value; }
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
                this.isShowDetail = value;

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
            this.dtpBeginTime.Value = this.dataBaseManager.GetDateTimeFromSysDateTime().AddMonths(-1);
            this.dtpEndTime.Value = this.dataBaseManager.GetDateTimeFromSysDateTime();
            #endregion
     
            #region ������ݱ��
            if (SvMain .RowCount > this.DataBeginRowIndex + 1)
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
        protected void  OpenSpread()
        {
            //SvMain = new FarPoint.Win.Spread.SheetView();
            //this.neuSpread1.Sheets.Add(SvMain);
              #region ��xml����fp
            if (string.IsNullOrEmpty(this.MainSheetXml) == false)
            {
                this.neuSpread1.Open(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Report\\" + this.MainSheetXml);
            }
            if (string.IsNullOrEmpty(this.DetailSheetXml) == false)
            {
                this.neuSpread2.Open(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Report\\" + this.DetailSheetXml);
            }
              #endregion
            #region ��open��������fp��ʽ���������ָ��sht����ʱ����
            if (this.neuSpread1.Sheets.Count>0)
            {
                string f = string.Empty;
                
                SvMain = neuSpread1.Sheets[0];
                SvMain.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            }
            if (neuSpread2.Sheets.Count >0)
            {
                SvDetail = neuSpread2.Sheets[0];   
                SvDetail.OperationMode= FarPoint.Win.Spread.OperationMode.ReadOnly;
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
            //int rtnVal = -1;
            Cursor = Cursors.WaitCursor;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ����,��ȴ�....");

            Application.DoEvents();
                    
            #region ������ݱ��
            if (SvMain.RowCount >= this.dataBeginRowIndex + 1)
            {
                SvMain.ClearRange(this.dataBeginRowIndex ,dataBeginColumnIndex,SvMain.Rows.Count-this.dataBeginRowIndex+1, this.dataDisplayColumns.Length ,false); 
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
                    if (Db.QueryDataBySqlId(this.QuerySql, ref dt,this.QueryParams) != 1)
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
            #region �������״̬
            if (this.dataBeginColumnIndex>0 ||this.dataBeginRowIndex>0)
            {
                //����������ȡԭ��������
                for (int i = dataBeginColumnIndex; i < this.dataDisplayColumns.Length; i++)
                {
                    //û�С������ž�������
                    if (SvMain.Cells[this.dataBeginRowIndex - 1, i].Text.IndexOf("��") >= 0)
                    {
                        //
                        SvMain.Cells[this.dataBeginRowIndex - 1, i].Text = SvMain.Cells[this.dataBeginRowIndex - 1, i].Text.Replace("��", "");
                    }
                    //û�С������ž�������
                    if (SvMain.Cells[this.dataBeginRowIndex - 1, i].Text.IndexOf("��") >= 0)
                    {
                        //
                        SvMain.Cells[this.dataBeginRowIndex - 1, i].Text = SvMain.Cells[this.dataBeginRowIndex - 1, i].Text.Replace("��", "");
                    }
                } 
            }
            #endregion
              DataSetHelper dsh = new DataSetHelper();
            DataTable dtValues = dsh.SelectIntoByIndex(string.Empty, dt, dataDisplayColumns, string.Empty, string.Empty);
            this.dataRowCount = dtValues.Rows.Count;
            #region ���ñ������
            //this.SvMain.ColumnCount = dataDisplayColumns.Length ;
            if (this.SvMain.Rows.Count<DataBeginRowIndex  + dt.Rows.Count)
            {
                this.SvMain.RowCount = DataBeginRowIndex  + dt.Rows.Count; 
            }
          
            #endregion
            #region �����Ԫ���������
            Function.DisplayToFp(SvMain, dtValues, DataBeginRowIndex, DataBeginColumnIndex);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    foreach (DataColumn dc in dt.Columns)
            //    {                                       
            //        if (Array.IndexOf(dataDisplayColumns,dt.Columns.IndexOf(dc).ToString())>=0)
            //        {
            //            SvMain.Cells[dt.Rows.IndexOf(dr) + 1 + DataBeginRowIndex, Array.IndexOf(dataDisplayColumns, dt.Columns.IndexOf(dc).ToString())].Text = dr[dt.Columns.IndexOf(dc)].ToString(); 
            //        }
        
            //    }
            //}
            #endregion

            #endregion

            #region ���÷�ҳ��
            if (this.rowPageBreak > 0)
            {

                for (int i = 0; i < this.SvMain.Rows.Count; i = ((i+1) * this.rowPageBreak + this.DataBeginRowIndex))
                {
                    this.SvMain.SetRowPageBreak((i * this.rowPageBreak + this.DataBeginRowIndex), true);
                }
            }
            #endregion

            Function.DrawGridLine(SvMain,this.dataBeginRowIndex,this.dataBeginColumnIndex,dtValues.Rows.Count,dtValues.Columns.Count);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            Cursor = Cursors.Arrow;

            return 1;
        }
        protected virtual void OnSort()
        {
            //����Ǳ�����
            if (this.SvMain.ActiveRowIndex==this.dataBeginRowIndex-1)
            {
                //����Ǳ�����
                if (this.SvMain.ActiveColumnIndex <=this.dataBeginColumnIndex+ this.dataDisplayColumns.Length - 1 && this.SvMain.ActiveColumnIndex >= this.dataBeginColumnIndex)
                {
                    //�½���������
                    FarPoint.Win.Spread.SortInfo[] sort = new FarPoint.Win.Spread.SortInfo[1];
                    //����������ȡԭ��������
                    for (int i = dataBeginColumnIndex; i < this.dataDisplayColumns.Length; i++)
                    {
                        bool ascending = true;
                        if (i == this.SvMain.ActiveColumnIndex)
                        {
                            //û�С������ž�������
                            if (SvMain.Cells[this.dataBeginRowIndex-1, i].Text.IndexOf("��") < 0)
                            {
                                //
                                SvMain.Cells[this.dataBeginRowIndex-1, i].Text = SvMain.Cells[this.dataBeginRowIndex-1, i].Text.Replace("��", "");
                                ascending = false;
                                SvMain.Cells[this.dataBeginRowIndex-1, i].Text = SvMain.Cells[this.dataBeginRowIndex-1, i].Text + "��";
                            }
                            else
                            {
                                //
                                SvMain.Cells[this.dataBeginRowIndex-1, i].Text = SvMain.Cells[this.dataBeginRowIndex-1, i].Text.Replace("��", "");
                                ascending = true;
                                SvMain.Cells[this.dataBeginRowIndex-1, i].Text = SvMain.Cells[this.dataBeginRowIndex-1, i].Text + "��";
                            }
                            //����������Ϣ
                            sort[0] = new FarPoint.Win.Spread.SortInfo(i, ascending, System.Collections.Comparer.Default);
                        }
                        else
                        {
                            //û�С������ž�������
                            if (SvMain.Cells[this.dataBeginRowIndex - 1, i].Text.IndexOf("��") < 0)
                            {
                                SvMain.Cells[this.dataBeginRowIndex - 1, i].Text = SvMain.Cells[this.dataBeginRowIndex - 1, i].Text.Replace("��", "");
                                //ascending = false;
                                //SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
                            }
                            else
                            {
                                SvMain.Cells[this.dataBeginRowIndex - 1, i].Text = SvMain.Cells[this.dataBeginRowIndex - 1, i].Text.Replace("��", "");
                                //ascending = true;
                                //SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
                            }
                        }
                        //����������Ϣ
                        //sort[i] = new FarPoint.Win.Spread.SortInfo(i, ascending, System.Collections.Comparer.Default);
                    }
                    ////�½���������
                    //FarPoint.Win.Spread.SortInfo[] sort = new FarPoint.Win.Spread.SortInfo[this.dataDisplayColumns.Length];
                    ////����������ȡԭ��������
                    //for (int i = 0; i < this.dataDisplayColumns.Length; i++)
                    //{
                    //    bool ascending = true;
                    //    if (i == this.SvMain.ActiveColumnIndex )
                    //    {
                    //        //û�С������ž�������
                    //        if (SvMain.Cells[this.dataBeginRowIndex, i].Text.IndexOf("��") < 0)
                    //        {
                    //            //
                    //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
                    //            ascending = false;
                    //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
                    //        }
                    //        else
                    //        {
                    //            //
                    //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
                    //            ascending = true;
                    //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //û�С������ž�������
                    //        if (SvMain.Cells[this.dataBeginRowIndex, i].Text.IndexOf("��") <0)
                    //        {
                    //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
                    //            ascending = false;
                    //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
                    //        }
                    //        else
                    //        {
                    //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text.Replace("��", "");
                    //            ascending = true;
                    //            SvMain.Cells[this.dataBeginRowIndex, i].Text = SvMain.Cells[this.dataBeginRowIndex, i].Text + "��";
                    //        } 
                    //    }
                    //    //����������Ϣ
                    //    sort[i] = new FarPoint.Win.Spread.SortInfo(i, ascending,System.Collections.Comparer.Default );
                    //}
                    SvMain.SortRange(this.dataBeginRowIndex, this.dataBeginColumnIndex, this.dataRowCount, this.dataDisplayColumns.Length , true, sort);
                    
                    
                }
            }
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
