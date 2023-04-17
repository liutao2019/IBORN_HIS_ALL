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
    public partial class ucQueryBaseForDataWindowExtend : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryBaseForDataWindowExtend()
        {
            InitializeComponent();
        }

        #region  //{2C89BBBC-10FB-4f7e-B080-712A6C228719}

        private string sqlid = string.Empty;

        [Category("�ؼ�����"), Description("SQL���IDά��")]
        public string SqlID
        {
            get
            {
                return sqlid;
            }
            set
            {
                sqlid = value;
            }
        }

        private string sql = string.Empty;


        #endregion

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

    
    



        #endregion

        #region ����



        /// <summary>
        /// ���ݴ�����Title
        /// </summary>
        protected string reportTitle = string.Empty;


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

        #region ��ݸ�ӵ�

        /// <summary>
        /// ���ݴ�����Title��Text�ؼ�����
        /// </summary>
        protected string reportTitleObjectName = "t_title";

        /// <summary>
        /// ���ݴ�����Title��Text�ؼ�����
        /// </summary>
        [Category("�����ʽ����"), Description("���ݴ�����Title��Text�ؼ�����")]
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
        /// ���
        /// </summary>
        string reportType = "0";

        [Category("�����ʽ����"), Description("������𣬣����סԺ����дö����")]
        public string ReportType
        {
            get
            {
                return reportType;
            }
            set
            {
                reportType = value;
            }
        }

      


        /// <summary>
        /// ��ʾ����Ա�Ŀؼ�����
        /// </summary>
        string operObjectName = "t_oper";

        [Category("�����ʽ����"), Description("��ʾ����Ա�Ŀؼ�����")]
        public string OperObjectName
        {
            get
            {
                return operObjectName;
            }
            set
            {
                operObjectName = value;
            }
        }

        /// <summary>
        ///  ��ʾ����Ա
        /// </summary>
        protected virtual void SetOper()
        {
            if (this.dwMain != null)
            {
                if (this.operObjectName.Trim() != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.operObjectName + ".Text = " + "'�Ƶ��ˣ�" + FS.FrameWork.Management.Connection.Operator.ID + "(" + FS.FrameWork.Management.Connection.Operator.Name + ")'");
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// ��ʾ����ʱ��Ŀؼ�����
        /// </summary>
        string operDateObjectName = "t_date";

        [Category("�����ʽ����"), Description("��ʾ����ʱ��Ŀؼ�����")]
        public string OperDateObjectName
        {
            get
            {
                return operDateObjectName;
            }
            set
            {
                operDateObjectName = value;
            }
        }

        /// <summary>
        ///  ��ʾ����Ա
        /// </summary>
        protected virtual void SetOperDate()
        {
            if (this.dwMain != null)
            {
                if (this.operDateObjectName.Trim() != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.operDateObjectName + ".Text = " + "'��ӡʱ�䣺" +��this.dataBaseManager.GetDateTimeFromSysDateTime().ToShortDateString() + "'");
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// ��ʾ��ѯʱ��εĿؼ�����
        /// </summary>
        string queryDateObjectName = "t_query";

        [Category("�����ʽ����"), Description("��ʾ��ѯʱ��εĿؼ�����")]
        public string QueryDateObjectName
        {
            get
            {
                return queryDateObjectName;
            }
            set
            {
                queryDateObjectName = value;
            }
        }

        /// <summary>
        ///  ��ʾͳ��ʱ���
        /// </summary>
        protected virtual void SetOperQueryDate(DateTime beginTime, DateTime endTime)
        {
            if (this.dwMain != null)
            {
                if (this.queryDateObjectName.Trim() != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.queryDateObjectName + ".Text = " + "'ʱ�䣺" + beginTime.ToShortDateString() + " �� " + endTime.ToShortDateString() + "'");
                    }
                    catch
                    {
                    }
                }
            }
        }
        /// <summary>
        /// ���汨�������ʾһ�еĿؼ�
        /// </summary>
        string queryFooterName = "t_footer";

        [Category("�����ʽ����"), Description("���汨�������ʾһ�еĿؼ�����")]
        public string QueryFooterName
        {
            get
            {
                return queryFooterName;
            }
            set
            {
                queryFooterName = value;
            }
        }

        /// <summary>
        ///  ��ʾͳ��ʱ���
        /// </summary>
        protected virtual void SetQueryFooter()
        {
            if (this.dwMain != null)
            {
                if (this.queryFooterName.Trim() != string.Empty)
                {
                    try
                    {
                        this.dwMain.Modify(this.QueryFooterName + ".Text = " + "    '����Ա��" + FS.FrameWork.Management.Connection.Operator.ID + "(" + FS.FrameWork.Management.Connection.Operator.Name + ")                          �������ң�"  +              (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.Name+"'");   // +                   "                           ��ӡʱ�䣺" + this.dataBaseManager.GetDateTimeFromSysDateTime().ToShortDateString() + "'");
                    }
                    catch
                    {
                    }
                }
            }
        }
        #endregion

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
            this.tvLeft.ImageList.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.A����));
            this.tvLeft.ImageList.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.L���));

            return 1;
        }

        /// <summary>
        /// ��ò�ѯʱ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int GetQueryTime() 
        {
            /*
            if (this.dtpEndTime.Value < this.dtpBeginTime.Value) 
            {
                MessageBox.Show("����ʱ�䲻��С�ڿ�ʼʱ��");

                return -1;
            }
            */ 

            this.beginTime = this.dtpBeginTime.Value;
            this.endTime = this.dtpEndTime.Value;

            return 1;
        }

        protected void ClearSql()
        {
            this.sql = string.Empty;
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

                    this.dwMain = new FSDataWindow.Controls.FSDataWindowExtend();

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

            //{2C89BBBC-10FB-4f7e-B080-712A6C228719}
            //if (FS.FrameWork.Management.Connection.Instance.GetType().ToString().IndexOf("IBM") >= 0)
            if (this.sqlid.Trim() != string.Empty)
            {
                //if (this.sqlid == string.Empty)
                //{
                //    MessageBox.Show("û��ά��SqlID��");
                //    return -1;
                //}
                if (this.sql == string.Empty)
                {
                    if (this.dataBaseManager.Sql.GetSql(this.sqlid, ref this.sql) < 0)
                    {
                        MessageBox.Show("�Ҳ���SQL��䣡");
                        return -1;
                    }
                }

                try
                {
                    string exeSql = string.Format(this.sql, beginTime.ToString(), endTime.ToString());

                    DataSet ds = new DataSet();

                    //���ó�ʱʱ��(Ĭ�ϵĳ�ʱʱ��Ϊ30S)

                    if (this.dataBaseManager.ExecQuery(exeSql, ref ds) < 0)
                    {
                        MessageBox.Show("ִ��SQL������");
                        return -1;
                    }

                    if (ds == null)
                    {
                        MessageBox.Show("ִ��SQL������");
                        return -1;
                    }

                    
                    if (dwMain.RetrieveDataTable( ds.Tables[0]) < 0)
                    {
                        MessageBox.Show(dwMain.Error);

                        return -1;
                    }

                    return 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }
            }

            if (FS.FrameWork.Management.Connection.Instance.GetType().ToString().IndexOf("IBM") >= 0)
            {
                if (this.dwMain.Retrieve(beginTime.ToString(), endTime.ToString()) < 0)
                {
                    MessageBox.Show(dwMain.Error);

                    return -1;
                }
            }
            else
            {
                if (this.dwMain.Retrieve(beginTime, endTime) < 0)
                {
                    MessageBox.Show(dwMain.Error);

                    return -1;
                }
            }

            return 1;
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

                            if (sortType == "A")
                            {
                                DataWindowSort(this.dwMain, sortColumn, sortType);
                                sortType = "D";
                            }
                            else
                            {
                                DataWindowSort(this.dwMain, sortColumn, sortType);
                                sortType = "A";
                            }
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
        protected void DeleleSortFlag(Sybase.DataWindow.DataWindowControl dwControl) 
        {
            string columnName = string.Empty;

            try
            {
                for (int i = 1; i < dwControl.ColumnCount + 1; i++)
                {
                    columnName = dwControl.Describe('#' + i.ToString() + ".name") + "_t";

                    dwControl.Modify(columnName + ".text = '" + this.dwMain.Describe(columnName + ".text").Replace("��", string.Empty) + "'");
                    dwControl.Modify(columnName + ".text = '" + this.dwMain.Describe(columnName + ".text").Replace("��", string.Empty) + "'");
                }
            }
            catch { }
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
                this.dwMain.LibraryList = FS.FrameWork.WinForms.Classes.Function.CurrentPath + this.mainDWLabrary;

                this.dwMain.DataWindowObject = this.mainDWDataObject;
            }
            DateTime dtSysdate = new DateTime();
            dtSysdate = this.dataBaseManager.GetDateTimeFromSysDateTime();

            this.dtpBeginTime.Value = new DateTime(dtSysdate.Year,dtSysdate.Month,dtSysdate.Day,0,0,0);
            this.dtpEndTime.Value = new DateTime(this.dataBaseManager.GetDateTimeFromSysDateTime().Year,this.dataBaseManager.GetDateTimeFromSysDateTime().Month,this.dataBaseManager.GetDateTimeFromSysDateTime().Day,23,59,59);

            this.OnDrawTree();

            if (this.tvLeft != null) 
            {
                if (this.tvLeft.Nodes.Count > 0) 
                {
                    this.tvLeft.Select();
                    this.tvLeft.SelectedNode = this.tvLeft.Nodes[0];
                }
            }
            //by zengft
            //this.dwMain.PrintProperties.Preview = true;
            try
            {
                this.dwMain.PrintProperties.PrinterName = this.PrinterName;
                this.dwMain.PrintProperties.PaperSize = 1;
            }
            catch
            {
            }

            this.SetTitle();

            this.SetOper();
            this.SetOperDate();

            this.SetQueryFooter();

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
            //{2C89BBBC-10FB-4f7e-B080-712A6C228719}
            //if (FS.FrameWork.Management.Connection.Instance.GetType().ToString().IndexOf("IBM") >= 0)
            if (this.sqlid.Trim() != string.Empty)
            {
                //if (this.sqlid == string.Empty)
                //{
                //    MessageBox.Show("û��ά��SqlID��");
                //    return -1;
                //}
                if (this.sql == string.Empty)
                {
                    if (this.dataBaseManager.Sql.GetSql(this.sqlid, ref this.sql) < 0)
                    {
                        MessageBox.Show("�Ҳ���SQL��䣡");
                        return -1;
                    }
                }

                try
                {
                    string exeSql = string.Format(this.sql, objects);

                    DataSet ds = new DataSet();

                    //���ó�ʱʱ��(Ĭ�ϵĳ�ʱʱ��Ϊ30S)

                    if (this.dataBaseManager.ExecQuery(exeSql, ref ds) < 0)
                    {
                        MessageBox.Show("ִ��SQL������");
                        return -1;
                    }


                    if (ds == null)
                    {
                        MessageBox.Show("ִ��SQL������");
                        return -1;
                    }
                    //by zengft
                    //this.dwMain.PrintProperties.Preview = true;
                    this.dwMain.PrintProperties.PrinterName = this.PrinterName;
                    this.dwMain.PrintProperties.PaperSize = 1;
                    if (dwMain.RetrieveDataTable(ds.Tables[0]) < 0)
                    {
                        MessageBox.Show(dwMain.Error);

                        return -1;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return -1;
                }

                return 1;
            }

            if (dwMain != null)
            {
                if (dwMain.Retrieve(objects) < 0)
                {
                    MessageBox.Show(dwMain.Error);

                    return -1;
                }
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
            dwMain.SaveAs(dd.FileName, Sybase.DataWindow.FileSaveAsType.Excel, true);

            return 1;
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
                    //this.dwMain.Print(true, true);
                    //if (this.printerName.Trim() != "")
                    //{
                    //    dwMain.SetProperty("DataWindow.Printer", printerName);
                    //}
                    //by zengft
                    //this.dwMain.PrintProperties.Preview = true;
                    this.dwMain.PrintProperties.PrinterName = this.PrinterName;
                    this.dwMain.PrintProperties.PaperSize = this.PrinterPaper;

                    if (this.PageHeight != 0 && this.PageWidth != 0)
                    {
                        dwMain.Modify("DataWindow.Print.Paper.Size=256");
                        dwMain.Modify("DataWindow.Print.CustomPage.Length=" + PageHeight.ToString());
                        dwMain.Modify("DataWindow.Print.CustomPage.Width=" + PageWidth.ToString());
                    }

                    this.dwMain.Print(ShowCancelDiag, ShowPrinterDiag);
                }
                catch { }
            }
            
            return base.OnPrint(sender, neuObject);
        }


        /// <summary>
        /// ��Щ�ؼ�û��ʵ����Ҫ����
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetControlVisible(bool isVisible)
        {
           
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

            this.GetQueryTime();
            if (this.isRetrieveArgsOnlyTime)
            {
                this.RetrieveMainOnlyByTime();
            }
            else 
            {
                this.OnRetrieve();
            }

            //this.SetOperQueryDate(this.beginTime, this.endTime);

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

        #region ��ӡ����

        int printerPaper =1;


        [Category("��ӡ����") ,Description("��ӡֽ����������")]
        public int PrinterPaper 
        {
            get { return this.printerPaper; }
            set { this.printerPaper = value; }
        }



        /// <summary>
        /// ��ӡ����
        /// </summary>
        string printerName = "";

        /// <summary>
        /// ��ӡ����
        /// </summary>
        [Category("��ӡ����"), Description("��ӡ����")]
        public string PrinterName
        {
            get
            {
                return printerName;
            }
            set
            {
                printerName = value;
            }
        }

        /// <summary>
        /// ֽ�ſ��
        /// </summary>
        int pageWidth = 0;

        /// <summary>
        /// ֽ�ſ��
        /// </summary>
        [Category("��ӡ����"), Description("ֽ�ſ��")]
        public int PageWidth
        {
            get
            {
                return pageWidth;
            }
            set
            {
                pageWidth = value;
            }
        }

        /// <summary>
        /// ֽ�Ÿ߶�
        /// </summary>
        int pageHeight = 0;

        /// <summary>
        /// ֽ�Ÿ߶�
        /// </summary>
        [Category("��ӡ����"), Description("ֽ�Ÿ߶�")]
        public int PageHeight
        {
            get
            {
                return pageHeight;
            }
            set
            {
                pageHeight = value;
            }
        }

        /// <summary>
        /// ��ӡʱ�Ƿ���ʾ��ӡ���öԻ���
        /// </summary>
        bool showPrinterDiag = false;

        /// <summary>
        /// ��ӡʱ�Ƿ���ʾ��ӡ���öԻ���
        /// </summary>
        [Category("��ӡ����"), Description("��ӡʱ�Ƿ���ʾ��ӡ���öԻ���")]
        public bool ShowPrinterDiag
        {
            get
            {
                return showPrinterDiag;
            }
            set
            {
                showPrinterDiag = value;
            }
        }

        /// <summary>
        /// ��ӡʱ�Ƿ���ʾȡ����ӡ�Ի���
        /// </summary>
        bool showCancelDiag = false;

        /// <summary>
        /// ��ӡʱ�Ƿ���ʾȡ����ӡ�Ի���
        /// </summary>
        [Category("��ӡ����"), Description("��ӡʱ�Ƿ���ʾȡ����ӡ�Ի���")]
        public bool ShowCancelDiag
        {
            get
            {
                return showCancelDiag;
            }
            set
            {
                showCancelDiag = value;
            }
        }

        #endregion

    }
}
