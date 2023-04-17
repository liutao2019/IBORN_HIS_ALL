using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FSDataWindow.Controls
{
    public partial class ucQueryBaseForDataWindow : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryBaseForDataWindow()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ���汨���־{40E52092-F854-464f-8A23-874BE7D4A543}
        /// </summary>
        protected bool isAcross = false;
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
        /// �����ݴ�pbl·��
        /// </summary>
        protected string mainDWLabrary = string.Empty;

        /// <summary>
        /// �����ݴ�DataObject
        /// </summary>
        protected string mainDWDataObject = string.Empty;

        /// <summary>
        /// ����ѯĬ�Ͽؼ�
        /// </summary>
        protected QueryControls mainQueryControl = QueryControls.DataWindow;

        /// <summary>
        /// �Ƿ��ѯ����ֻ�п�ʼʱ��,����ʱ��
        /// </summary>
        protected bool isRetrieveArgsOnlyTime = false;

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
        /// ���ݴ�����Title
        /// </summary>
        protected string reportTitle = string.Empty;

        /// <summary>
        /// ���ݴ�����Title��Text�ؼ�����
        /// </summary>
        protected string reportTitleObjectName = string.Empty;

        /// <summary>
        /// ���һ���������� {A652EF19-B5B2-4148-AAB1-774C2D3AE1B2}
        /// </summary>
        protected string lastSortedColumnName = string.Empty; 
        #endregion

        #region ����

        /// <summary>
        /// ���ݴ�����Title��Text�ؼ�����
        /// </summary>
        [Category("�ؼ�����"), Description("���ݴ�����Title��Text�ؼ�����")]
        public string ReportTitleObjectName 
        {
            get 
            {
                return this.reportTitleObjectName;
            }
            set 
            {
                this.reportTitleObjectName = value;
            }
        }

        /// <summary>
        /// ���ݴ�����Title
        /// </summary>
        [Category("�ؼ�����"), Description("���ݴ�����Title")]
        public string ReportTitle 
        {
            get 
            {
                return this.reportTitle;
            }
            set 
            {
                this.reportTitle = value;
            }
        }

        /// <summary>
        /// ����ѯĬ�Ͽؼ�
        /// </summary>
        [Category("�ؼ�����"), Description("�����ݴ�pbl·��")]
        public QueryControls MainQueryControl 
        {
            get 
            {
                return this.mainQueryControl;
            }
            set 
            {
                this.mainQueryControl = value;

                this.SetMainQueryControl();
            }
        }

        /// <summary>
        /// �����ݴ�pbl·��
        /// </summary>
        [Category("�ؼ�����"), Description("�����ݴ�pbl·��")]
        public string MainDWLabrary 
        {
            get 
            {
                return this.mainDWLabrary;
            }
            set 
            {
                this.mainDWLabrary = value;
            }
        }

        /// <summary>
        /// �����ݴ�DataObject
        /// </summary>
        [Category("�ؼ�����"), Description("�����ݴ�DataObject")]
        public string MainDWDataObject 
        {
            get 
            {
                return this.mainDWDataObject;
            }
            set 
            {
                this.mainDWDataObject = value;
            }
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

        /// <summary>
        /// �Ƿ��ѯ����ֻ�п�ʼʱ��,����ʱ��
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��ѯ����ֻ�п�ʼʱ��,����ʱ��")]
        public bool IsRetrieveArgsOnlyTime
        {
            get
            {
                return this.isRetrieveArgsOnlyTime;
            }
            set
            {
                this.isRetrieveArgsOnlyTime = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// �������ݴ���Title����
        /// </summary>
        protected virtual void SetTitle() 
        {
            if (this.dwMain != null) 
            {
                if (this.reportTitleObjectName != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.reportTitleObjectName + ".Text = " + "'" + this.reportTitle + "'");
                    }
                    catch { }
                }
            }
        }

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
        /// ��������ѯ�ؼ�����
        /// </summary>
        protected virtual void SetMainQueryControl() 
        {
            this.plRightTop.Controls.Clear();

            switch (this.mainQueryControl) 
            {
                case QueryControls.DataWindow:

                    this.dwMain = new FSDataWindow();

                    this.plRightTop.Controls.Add(this.dwMain);

                    this.dwMain.LiveScroll = true;
                    this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
                    this.dwMain.Dock = DockStyle.Fill;
                    this.dwMain.BringToFront();

                    break;
            }
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
                return;
            }
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
        /// ���տ�ʼʱ��ͽ���ʱ���ѯdw
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int RetrieveMainOnlyByTime() 
        {
            if (this.dwMain == null) 
            {
                return -1;
            }

            if (this.GetQueryTime() == -1) 
            {
                return -1;
            }

            return this.dwMain.Retrieve(beginTime, endTime);
        }
        
        /// <summary>
        /// �����ݴ�Retrieve����
        /// </summary>
        /// <param name="args">Retrieve�����б�</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int RetrieveMain(params object[] args) 
        {
            if (this.dwMain != null) 
            {
                try
                {
                    return this.dwMain.Retrieve(args);
                }
                catch { }
            }
            
            return 1;
        }
        /// <summary>
        /// �Ƿ�֧������
        /// </summary>
        protected bool isSort = true;

        protected string sortColumn = string.Empty;

        /// <summary>
        /// ��������
        /// </summary>
        protected string sortType = "A";

        /// <summary>
        /// ���� �ɹ� 1 ʧ�� -1
        /// </summary>
        /// <returns></returns>
        protected int OnSort() 
        {
            try
            {
                if (this.isSort)
                {
                    string ls_CurObj = "";

                    int ll_CurRowNumber = 0;
                    ls_CurObj = this.dwMain.ObjectUnderMouse.Gob.Name; //�ó�objectName
                    ll_CurRowNumber = this.dwMain.ObjectUnderMouse.RowNumber; //�ó���ǰRow

                    if (this.dwMain.Describe(ls_CurObj + ".Band") == "header")
                    {
                        if (ll_CurRowNumber == 0 & this.dwMain.Describe(ls_CurObj + ".Text") != "!")
                        {
                            sortColumn = ls_CurObj.Substring(0, ls_CurObj.Length - 2);

                            //{A652EF19-B5B2-4148-AAB1-774C2D3AE1B2}
                            if (sortType == "A")
                            {
                                if (DataWindowSort(this.dwMain, sortColumn, sortType))
                                {
                                    sortType = "D";
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            else
                            {
                                if (DataWindowSort(this.dwMain, sortColumn, sortType))
                                {
                                    sortType = "A";
                                }
                                else
                                {
                                    return -1;
                                }
                            }
                            //{A652EF19-B5B2-4148-AAB1-774C2D3AE1B2}
                            this.lastSortedColumnName = ls_CurObj;
                        }
                    }
                }
            }
            catch
            {
                return -1;
            }

            finally
            {

            }

            return 1;
        }

        /// <summary>
        /// ȡ���������е��������
        /// </summary>
        /// <param name="dwControl">��ǰ���ݴ���</param>
        private void DeleleSortFlag(Sybase.DataWindow.DataWindowControl dwControl) 
        {
            //{A652EF19-B5B2-4148-AAB1-774C2D3AE1B2}

            //string columnName = string.Empty;

            //try
            //{
            //    for (int i = 1; i < dwControl.ColumnCount + 1; i++)
            //    {
            //        columnName = dwControl.Describe('#' + i.ToString() + ".name") + "_t";

            //        //dwControl.Modify(columnName + ".text = '" + this.dwMain.Describe(columnName + ".text").Replace("��", string.Empty) + "'");
            //        //dwControl.Modify(columnName + ".text = '" + this.dwMain.Describe(columnName + ".text").Replace("��", string.Empty) + "'");
            //    }
            //}
            //catch { }

            try
            {
                if (!this.lastSortedColumnName.Equals(string.Empty))
                {
                    this.dwMain.Modify(this.lastSortedColumnName + ".text = '" + this.dwMain.Describe(this.lastSortedColumnName + ".text").Replace("��", string.Empty) + "'");
                    this.dwMain.Modify(this.lastSortedColumnName + ".text = '" + this.dwMain.Describe(this.lastSortedColumnName + ".text").Replace("��", string.Empty) + "'");
                }
            }
            catch
            {
                //�������� 
            }
        }

        /// <summary>
        /// ����ķ���
        /// </summary>
        /// <param name="dwControl">��ǰ���ݴ�</param>
        /// <param name="currColumn">��ǰ��</param>
        /// <param name="sortType">��������</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        private bool DataWindowSort(Sybase.DataWindow.DataWindowControl dwControl, string currColumn, string sortType) 
        {
            try
            {
                //����  
                dwControl.SetSort(currColumn + " " + sortType);
                dwControl.Sort();

                //��������ļ�ͷͼ��

                DeleleSortFlag(dwControl);

                switch (sortType)
                {
                    case "A":

                        dwControl.Modify(currColumn + "_t" + ".text = '" + this.dwMain.Describe(currColumn + "_t" + ".text") + "��'");

                        break;
                    case "D":
                        dwControl.Modify(currColumn + "_t" + ".text = '" + this.dwMain.Describe(currColumn + "_t" + ".text") + "��'");

                        break;
                }

                return true;
            }
            catch
            {
                return false;
            }

            finally
            {

            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected int Init() 
        {
            if (this.dwMain != null)
            {
                this.dwMain.LibraryList = Application.StartupPath + "\\" + this.mainDWLabrary;

                this.dwMain.DataWindowObject = this.mainDWDataObject;
            }

            this.dtpBeginTime.Value = this.dataBaseManager.GetDateTimeFromSysDateTime();
            this.dtpEndTime.Value = this.dataBaseManager.GetDateTimeFromSysDateTime();

            this.OnDrawTree();

            if (this.tvLeft != null) 
            {
                if (this.tvLeft.Nodes.Count > 0) 
                {
                    this.tvLeft.Select();
                    this.tvLeft.SelectedNode = this.tvLeft.Nodes[0];
                }
            }

            this.SetTitle();

            return 1;
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
            #region ���汨�����³�ʼ������ֹ��ѯ����{40E52092-F854-464f-8A23-874BE7D4A543}
            if (this.isAcross)
            {
                this.dwMain.Dispose();
                this.dwMain = new  FSDataWindow();
                this.plRightTop.Controls.Add(this.dwMain);
                this.dwMain.DataWindowObject = "";
                this.dwMain.Dock = System.Windows.Forms.DockStyle.Fill;
                this.dwMain.LibraryList = "";
                this.dwMain.Location = new System.Drawing.Point(0, 0);
                this.dwMain.Name = "dwMain";
                this.dwMain.ScrollBars = Sybase.DataWindow.DataWindowScrollBars.Both;
                this.dwMain.Size = new System.Drawing.Size(544, 276);
                this.dwMain.TabIndex = 0;
                this.dwMain.Text = "neuDataWindow1";
                this.dwMain.Click += new System.EventHandler(this.dwMain_Click);
                if (this.dwMain != null)
                {
                    this.dwMain.LibraryList = Application.StartupPath + "\\" + this.mainDWLabrary;

                    this.dwMain.DataWindowObject = this.mainDWDataObject;
                }
            }
            #endregion



            if (dwMain != null)
            {
                dwMain.Retrieve(objects);
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
            if (dwMain == null)
            {
                return -1;
            }    

            this.DeleleSortFlag(dwMain);

            System.Windows.Forms.SaveFileDialog dd = new SaveFileDialog();
            dd.Filter = "txt files (*.xls)|*.xls";
            if (dd.ShowDialog() == DialogResult.Cancel)
            {
                return 1;
            }
            //dwMain.SaveAs(dd.FileName, Sybase.DataWindow.FileSaveAsType.Excel, true);
            dwMain.SaveAsFormattedText(dd.FileName);

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
            if (this.dwMain != null)
            {
                frmPreviewDataWindow frmPreview = new frmPreviewDataWindow();

                frmPreview.PreviewDataWindow = dwMain;

                if (frmPreview.ShowDialog() == DialogResult.OK)
                    this.dwMain.PrintProperties.Preview = false;
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
            if (this.dwMain != null) 
            {
                try
                {
                    this.DeleleSortFlag(dwMain);
                    this.dwMain.PrintProperties.Preview = false;
                    this.dwMain.Print(true, true);
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

        #endregion

        #region �¼�

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            Cursor = Cursors.WaitCursor;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ����,��ȴ�....");

            Application.DoEvents();
            
            if (this.isRetrieveArgsOnlyTime)
            {
                this.RetrieveMainOnlyByTime();
            }
            else 
            {
                this.OnRetrieve();
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            Cursor = Cursors.Arrow;

            return 1;
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

        private void dwMain_Click(object sender, EventArgs e)
        {
            if (this.dwMain != null)
            {
                this.OnSort();
            }
        }




        #endregion

       
    }
}
