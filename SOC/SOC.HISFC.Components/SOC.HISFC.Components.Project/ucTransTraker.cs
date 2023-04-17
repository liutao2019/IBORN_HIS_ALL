using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace FS.SOC.HISFC.Components.Project
{
    public partial class ucTransTraker : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucTransTraker()
        {
            InitializeComponent();
            this.nllPreferredHeight.LinkClicked += new LinkLabelLinkClickedEventHandler(nllPreferredHeight_LinkClicked);
            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
            this.neuLinkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(neuLinkLabel1_LinkClicked);
            this.nlbAssignMission.DoubleClick += new EventHandler(nlbAssignMission_DoubleClick);
            this.nlbLogionInfo.DoubleClick += new EventHandler(nlbLogionInfo_DoubleClick);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
            this.frmMissionInput.InputCompletedEven += new frmMissionInput.InputCompletedHander(frmMissionInput_InputCompleted);
            this.frmMissionInput.InputCompletedAndNextEven += new frmMissionInput.InputCompletedAndNextHander(frmMissionInput_InputCompletedAndNext);
            this.frmMissionAssign.InputCompletedEven += new frmMissionAssign.InputCompletedHander(frmMissionAssign_InputCompleted);
            this.frmMissionAssign.InputCompletedAndNextEven += new frmMissionAssign.InputCompletedAndNextHander(frmMissionAssign_InputCompletedAndNext);
            this.frmMissionAssign.InputCompletedAndAllEven += new frmMissionAssign.InputCompletedAndAllHander(frmMissionAssign_InputCompletedAndAll);

            this.frmMissionCompleted.InputCompletedEven += new frmMissionCompleted.InputCompletedHander(frmMissionCompleted_InputCompleted);
            this.frmMissionCompleted.InputCompletedAndNextEven += new frmMissionCompleted.InputCompletedAndNextHander(frmMissionCompleted_InputCompletedAndNext);


            this.frmFilterSetting.InputCompletedEven += new frmFilterSetting.InputCompletedHander(frmFilterSetting_InputCompleted);
        }

        int curAssignRowIndex = 0;
        int curAssignColIndex = 0;

        int curCompletedRowIndex = 0;
        int curCompletedColIndex = 0;

        MissionManager curMissionManager = new MissionManager();
        System.Data.DataSet curDataDetail = new DataSet();
        private string curAutoFilterColumnIndex = "2,8,9,11,15,18,21";

        frmLogin frmLogion = new frmLogin();
        frmMissionInput frmMissionInput = new frmMissionInput();
        frmMissionAssign frmMissionAssign = new frmMissionAssign();
        frmMissionCompleted frmMissionCompleted = new frmMissionCompleted();
        frmFilterSetting frmFilterSetting = new frmFilterSetting();

        AccountInfo curLogionAccount = null;

        /// <summary>
        /// 允许过滤的字段索引
        /// </summary>
        [Description("允许过滤的字段索引，注意逗号分割，半角字符，如0,1"), Category("设置")]
        public string AutoFilterColumnIndex
        {
            get { return curAutoFilterColumnIndex; }
            set { curAutoFilterColumnIndex = value; }
        }

        private string curSettingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\FPMissionManager.xml";

        /// <summary>
        /// 配置文件名称
        /// </summary>
        [Description("Farpoint配置文件名称"), Category("设置")]
        public string SettingFile
        {
            get { return curSettingFile; }
        }

        private string curVirtrueColumns = "项目名称";

        /// <summary>
        /// 虚拟列名称
        /// </summary>
        [Description("虚拟列名称"), Category("设置")]
        public string VirtrueColumns
        {
            get { return curVirtrueColumns; }
            set { curVirtrueColumns = value; }
        }

        private int SetFarPointPreferredHeight()        
        {
            for (int index = 0; index < this.fpSpread1_Sheet1.RowCount; index++)
            {
                this.fpSpread1_Sheet1.Rows[index].Height = this.fpSpread1_Sheet1.Rows[index].GetPreferredHeight() + 4;
            }

            return 1;
        }

        decimal curPrimaryKey = 0;

        private int Init()
        {
            this.ncmbModeName.AddItems(this.curMissionManager.QueryAllModels());
            this.ncmbState.AddItems(this.curMissionManager.QueryAllStates());
            this.ncmbOper.AddItems(this.curMissionManager.QueryAllMissionAccepters());
            this.neuDateTimePicker1.Value = DateTime.Now.AddMonths(-3);
            return 1;
        }

        private int SetPrimaryKey(System.Data.DataSet ds)
        {

            DataColumn[] keys = new DataColumn[1];

            keys[0] = ds.Tables[0].Columns["主键"];

            ds.Tables[0].PrimaryKey = keys;

            return 1;
        }

        private int SetFormat()
        {
            this.fpSpread1.ReadSchema(this.SettingFile);
            this.fpSpread1_Sheet1.DefaultStyle.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1_Sheet1.Rows.Default.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.fpSpread1.AllowDragFill = true;
            int columnIndex = this.fpSpread1.GetColumnIndex(0, "锁定标记");
            this.fpSpread1_Sheet1.Columns[columnIndex].Locked = true;
            columnIndex = this.fpSpread1.GetColumnIndex(0, "锁定时间");
            this.fpSpread1_Sheet1.Columns[columnIndex].Locked = true; 
            columnIndex = this.fpSpread1.GetColumnIndex(0, "锁定人");
            this.fpSpread1_Sheet1.Columns[columnIndex].Locked = true;
            this.fpSpread1_Sheet1.Columns[columnIndex].ForeColor = Color.Red;

            //设置过滤
            for (int index = 0; index < this.fpSpread1_Sheet1.ColumnCount; index++)
            {
                if (AutoFilterColumnIndex.Contains("," + index.ToString() + ",")
                    || AutoFilterColumnIndex.StartsWith(index.ToString() + ",")
                    || AutoFilterColumnIndex.EndsWith("," + index.ToString())
                    || AutoFilterColumnIndex == index.ToString()
                    )
                {
                    this.fpSpread1_Sheet1.Columns.Get(index).AllowAutoFilter = true;
                }
            }

            if (curDataDetail.Tables[0].Columns.Contains("主键"))
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;
                this.fpSpread1_Sheet1.Columns[curDataDetail.Tables[0].Columns.IndexOf("主键")].CellType = t;
            }

            if (curDataDetail.Tables[0].Columns.Contains("需求说明"))
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.Multiline = true;
                t.WordWrap = true;
                this.fpSpread1_Sheet1.Columns[curDataDetail.Tables[0].Columns.IndexOf("需求说明")].CellType = t;
            }
           

            return 1;
        }

        private int SetInitFormat()
        {
            if (curMissionManager.QueryMission("", "", "", DateTime.Now, ref curDataDetail) != -1)
            {
                curDataDetail.Tables[0].DefaultView.AllowEdit = true;
                curDataDetail.Tables[0].DefaultView.AllowNew = true;
                this.fpSpread1_Sheet1.DataSource = curDataDetail.Tables[0].DefaultView;

                curDataDetail.AcceptChanges();
                this.SetFormat();
            }
            this.neuGroupBox2.Visible = ((FS.HISFC.Models.Base.Employee)curMissionManager.Operator).IsManager;
            //this.neuGroupBox3.Visible = !((FS.HISFC.Models.Base.Employee)curMissionManager.Operator).IsManager;
            return 1;
        }

        private int QueryData()
        {
            if (this.Login() != 1)
            {
                return 0;
            }
            
            this.curDataDetail.Tables[0].Clear();
            string modelName = "All";
            if (this.ncmbModeName.Text != "")
            {
                modelName = this.ncmbModeName.Text;
            }
            string state = "All";
            if (this.ncmbState.Text != "")
            {
                state = this.ncmbState.Text;
            }
            string missionAccepter = "All";
            if (this.ncmbOper.Text != "")
            {
                missionAccepter = this.ncmbOper.Text;
            }
            if (curMissionManager.QueryMission(modelName, state,missionAccepter, this.neuDateTimePicker1.Value, ref curDataDetail) != -1)
            {
                curDataDetail.Tables[0].DefaultView.AllowEdit = true;
                curDataDetail.Tables[0].DefaultView.AllowNew = true;
                this.fpSpread1_Sheet1.DataSource = curDataDetail.Tables[0].DefaultView;
              
                curDataDetail.AcceptChanges();
                this.SetPrimaryKey(this.curDataDetail);
                this.SetFormat();
                if (this.nckAutoFitMission.Checked)
                {
                    this.SetFarPointPreferredHeight();
                }
            }

            return 1;
        }

        private int Login()
        {
            if (this.curLogionAccount == null || this.curLogionAccount.AccountID == "")
            {
                this.frmLogion.ShowDialog(this);
                this.curLogionAccount = this.frmLogion.CurLoginAccountInfo;
                if (this.curLogionAccount == null || this.curLogionAccount.AccountID == "")
                {
                    return 0;
                }
                else
                {
                    this.nlbLogionInfo.Text = "当前账号：" + curLogionAccount.AccountID + "，已登录";
                    if (this.curLogionAccount.RoleID == "PM" || this.curLogionAccount.RoleID == "PSM" || this.curLogionAccount.RoleID == "LEADER")
                    {
                        this.nlbAssignMission.Visible = true;
                    }
                    else
                    {
                        this.nlbAssignMission.Visible = false;
                    }
                }
            }

            return 1;
        }


        private int ImportData()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string dataFilePath = "";

            openFileDialog1.Filter = "Excel files (*.xls)|*.xls";

            System.Data.OleDb.OleDbCommand oledbDataCommand = null;

            System.Data.OleDb.OleDbConnection oledbDataConnection = null;

            System.Data.OleDb.OleDbDataAdapter oledbDataAdapter = null;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                Stream dataStream = null;
                try
                {
                    dataStream = openFileDialog1.OpenFile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(ex.Message));
                    return -1;
                }

                if (dataStream != null)
                {
                    dataFilePath = openFileDialog1.FileName;
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("无效文件"));
                    return -1;
                }
            }
            else
            {
                return 0;
            }
            //if (curDataDetail != null)
            //{
            //    curDataDetail.Clear();
            //}
            //else
            //{
            //    curDataDetail = new DataSet();
            //}

            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg("正在读入数据 请稍候.."));
                Application.DoEvents();



                oledbDataConnection = new System.Data.OleDb.OleDbConnection();
                oledbDataConnection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dataFilePath + @";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""";

                oledbDataCommand = new System.Data.OleDb.OleDbCommand();
                oledbDataCommand.Connection = oledbDataConnection;
                oledbDataCommand.CommandText = "SELECT *  FROM " + "[Sheet1$]";

                oledbDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
                oledbDataAdapter.SelectCommand = oledbDataCommand;

                DataSet ds = new DataSet();
               
                oledbDataAdapter.Fill(ds);

                if (ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count==0)
                {
                    oledbDataConnection.Close();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("没有获取到你要导入的数据！\n请注意：\n1    1、Sheet名称必须是Sheet1，并且区分大小写\n    2、表中必须含有列[主键]，值为空表示新加需求"));
                    return 1;
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    curPrimaryKey = curPrimaryKey + 1;
                    if (row["主键"].ToString() == "")
                    {
                        row["主键"] = "I" + curPrimaryKey.ToString();
                    }
                    
                }
                //foreach (DataColumn dc in ds.Tables[0].Columns)
                //{
                //    dc.DataType = this.curDataDetail.Tables[0].Columns[dc.ColumnName].DataType;
                //}

                this.SetPrimaryKey(ds);
                this.curDataDetail.Tables[0].Merge(ds.Tables[0], false, MissingSchemaAction.AddWithKey);

                curDataDetail.Tables[0].DefaultView.AllowEdit = true;
                curDataDetail.Tables[0].DefaultView.AllowNew = true;
                this.fpSpread1_Sheet1.DataSource = curDataDetail.Tables[0].DefaultView;

                //curDataDetail.AcceptChanges();
                this.SetFormat();

                if (this.nckAutoFitMission.Checked)
                {
                    this.SetFarPointPreferredHeight();
                }
                oledbDataConnection.Close();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString());
                return -1;
            }

            return 1;
        }

        private int ExportPDF()
        {
            //Class1 c = new Class1();
            //c.test();

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            //设置文件类型   
            saveFileDialog.Filter = "pdf files(*.pdf)|*.pdf";

            //保存对话框是否记忆上次打开的目录   
            saveFileDialog.RestoreDirectory = true;

            //点了保存按钮进入   
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string localFilePath = saveFileDialog.FileName;
                string fileType = localFilePath.Substring(localFilePath.LastIndexOf("."), 4);
                int columns = this.fpSpread1_Sheet1.Columns.Count;
                int rows = this.fpSpread1_Sheet1.Rows.Count;
             

                #region 导出pdf

                float[] withs = new float[columns];
                float with = 0;
                for (int index = 0; index < columns; index++)
                {
                    withs[index] = this.fpSpread1_Sheet1.Columns[index].Width;
                    with += this.fpSpread1_Sheet1.Columns[index].Width;
                }

                iTextSharp.text.Document dc = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(with + 80, 1280));
            
                try
                {
                    FileStream fs = new FileStream(localFilePath, FileMode.Create);
                    iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(dc, fs);
                    dc.Open();
                
                    //iTextSharp.text.pdf.BaseFont bfBase = iTextSharp.text.pdf.BaseFont.createFont(@"C:\WINDOWS\Fonts\simsun.ttc,1", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.pdf.BaseFont bfBase = iTextSharp.text.pdf.BaseFont.CreateFont(@"C:\WINDOWS\Fonts\simsun.ttc,1", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(bfBase, 8);

                    iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(columns);


                    for (int i = 0; i < columns; i++)
                    {
                        if (this.fpSpread1_Sheet1.Columns[i].Width > 0)
                        {
                            table.AddCell(new iTextSharp.text.Phrase(this.fpSpread1_Sheet1.ColumnHeader.Cells[0, i].Text, font));
                        }
                        else
                        {
                            table.AddCell(new iTextSharp.text.Phrase("", font));
                        }
                    }
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {

                            if (this.fpSpread1_Sheet1.Columns[j].Width > 0)
                            {
                                table.AddCell(new iTextSharp.text.Phrase(this.fpSpread1_Sheet1.Cells[i, j].Text, font));
                            }
                            else
                            {
                                table.AddCell(new iTextSharp.text.Phrase("", font));
                            }
                        }
                    }

                    table.SetWidths(withs);

                    dc.Add(table);
                }
                catch (Exception ex)
                {
                    dc.Close();
                    MessageBox.Show(ex.Message);
                    return -1;
                }
                dc.Close();
                #endregion


                MessageBox.Show("保存路径为：" + localFilePath, "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return 1;
        }

        private int SaveData()
        {
            DataSet ds = this.curDataDetail.GetChanges();
            this.fpSpread1_Sheet2.DataSource = ds;
            return 1;
        }

        private int CheckOut(int rowIndex,DateTime now)
        {
            if (rowIndex > this.fpSpread1_Sheet1.RowCount - 1 || rowIndex < 0)
            {
                return 0;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int param = curMissionManager.CheckOutMission(this.fpSpread1.GetCellText(0, rowIndex, "主键"), this.curLogionAccount.AccountID, now);
            if (param == 1)
            {
                this.fpSpread1.SetCellValue(0, rowIndex, "锁定标记", "是");
                this.fpSpread1.SetCellValue(0, rowIndex, "锁定人", this.curLogionAccount.AccountID);
                this.fpSpread1.SetCellValue(0, rowIndex, "锁定时间", now);
                this.fpSpread1_Sheet1.Rows[rowIndex].Label = (rowIndex + 1).ToString();
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            else
            {
                this.fpSpread1_Sheet1.Rows[rowIndex].Label = "╳";
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            return 1;
        }

        private int CancelCheckOut(int rowIndex)
        {
            if (rowIndex > this.fpSpread1_Sheet1.RowCount - 1 || rowIndex < 0)
            {
                return 0;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int param = curMissionManager.CancelCheckOutMission(this.fpSpread1.GetCellText(0, rowIndex, "主键"), this.curLogionAccount.AccountID);
            if (param == 1)
            {
                this.fpSpread1.SetCellValue(0, rowIndex, "锁定标记", "否");
                this.fpSpread1.SetCellValue(0, rowIndex, "锁定人", "");
                this.fpSpread1_Sheet1.Rows[rowIndex].Label = (rowIndex + 1).ToString();
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            else
            {
                this.fpSpread1_Sheet1.Rows[rowIndex].Label = "╳";
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            return 1;
        }


        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("导入", "导入数据", FS.FrameWork.WinForms.Classes.EnumImageList.D导入, true, false, null);
            toolBarService.AddToolButton("PDF", "导出PDF", FS.FrameWork.WinForms.Classes.EnumImageList.D导出, true, false, null);
            toolBarService.AddToolButton("登录", "登录", FS.FrameWork.WinForms.Classes.EnumImageList.R人员组, true, false, null);
            toolBarService.AddToolButton("注册账号", "注册账号", FS.FrameWork.WinForms.Classes.EnumImageList.R人员, true, false, null);
            toolBarService.AddToolButton("问题卡", "使用问题卡录入需求或其他问题", FS.FrameWork.WinForms.Classes.EnumImageList.B摆药单, true, false, null);
            //toolBarService.AddToolButton("任务计划", "将需求或者问题分配到人，制定计划", FS.FrameWork.WinForms.Classes.EnumImageList.R日期, true, false, null);
            //toolBarService.AddToolButton("处理结果", "录入需求或者问题处理结果", FS.FrameWork.WinForms.Classes.EnumImageList.J计划单, true, false, null);
            //toolBarService.AddToolButton("测试结果", "录入需求或者问题测试结果", FS.FrameWork.WinForms.Classes.EnumImageList.J计算器, true, false, null);
            return this.toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "导入")
            {
                this.ImportData();
            }
            else if (e.ClickedItem.Text == "PDF")
            {
                this.ExportPDF();
            }
            else if (e.ClickedItem.Text == "登录")
            {
                this.frmLogion.ShowDialog(this);
                if (this.frmLogion.CurLoginAccountInfo != null && this.frmLogion.CurLoginAccountInfo.AccountID != "")
                {
                    curLogionAccount = this.frmLogion.CurLoginAccountInfo;
                    this.nlbLogionInfo.Text = "当前账号：" + curLogionAccount.AccountID + "，已登录";
                }
            }
            else if (e.ClickedItem.Text == "注册账号")
            {
                frmRegister frmRegister = new frmRegister();
                frmRegister.ShowDialog(this);
            }
            else if (e.ClickedItem.Text == "问题卡")
            {
                DataRow row = this.curDataDetail.Tables[0].NewRow();
                row["主键"] = "I" + this.curPrimaryKey.ToString();
                this.curPrimaryKey = this.curPrimaryKey + 1;

                this.frmMissionInput.SetMission(row);
                this.frmMissionInput.ShowDialog(this);
            }

        }

        public override int Query(object sender, object neuObject)
        {
            this.QueryData();
            return base.Query(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            this.SetInitFormat();
            this.SetPrimaryKey(this.curDataDetail);

            frmFilterSetting_InputCompleted();

            base.OnLoad(e);
        }

        public override int Export(object sender, object neuObject)
        {
            this.fpSpread1_Sheet1.Protect = false;
            this.fpSpread1.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
            return base.Export(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveData();
            return base.OnSave(sender, neuObject);
        }

        void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.fpSpread1.SaveSchema(this.SettingFile);
        }


        void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (!e.RowHeader)
            { 
                return;
            }
            bool isCheckOutBySelf = (this.fpSpread1.GetCellText(0, e.Row, "锁定人") == this.curLogionAccount.AccountID);
            if (!this.fpSpread1.GetCellText(0, e.Row, "主键").StartsWith("I"))
            {
                if (this.curLogionAccount == null || this.curLogionAccount.AccountID == "")
                {
                    this.frmLogion.ShowDialog(this);
                    this.curLogionAccount = this.frmLogion.CurLoginAccountInfo;
                    if (this.curLogionAccount == null || this.curLogionAccount.AccountID == "")
                    {
                        return;
                    }
                    else
                    {
                        this.nlbLogionInfo.Text = "当前账号：" + curLogionAccount.AccountID + "，已登录";
                    }
                }
                if (this.CheckOut(e.Row, DateTime.Now) == -1 
                    && this.fpSpread1.GetCellText(0, e.Row, "锁定人") != this.curLogionAccount.AccountID)
                {
                    MessageBox.Show("需求或问题无法签出，不能进行修改\n一般情况是其他人签出问题正在处理");
                    return;
                }
            }

           

            if (e.Row > -1)
            {
                DataRow row = this.curDataDetail.Tables[0].Rows.Find(this.fpSpread1.GetCellText(0, e.Row, "主键"));
                if (row != null)
                {
                    this.frmMissionInput.SetMission(row);
                    if (this.frmMissionInput.ShowDialog(this) == DialogResult.Cancel && !isCheckOutBySelf)
                    {
                        this.CancelCheckOut(e.Row);
                    }
                }
            }
        }

        void frmMissionInput_InputCompleted()
        {
            DataRow row = this.frmMissionInput.GetData();
            try
            {
                if (row["主键"].ToString().StartsWith("I") && this.curDataDetail.Tables[0].Rows.Find(row["主键"].ToString()) == null)
                {
                    this.curDataDetail.Tables[0].Rows.Add(row);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void frmMissionInput_InputCompletedAndNext()
        {
            DataRow row = this.frmMissionInput.GetData();
            try
            {
                if (row["主键"].ToString().StartsWith("I") && this.curDataDetail.Tables[0].Rows.Find(row["主键"].ToString()) == null)
                {
                    this.curDataDetail.Tables[0].Rows.Add(row);
                }
                DataRow newRow = this.curDataDetail.Tables[0].NewRow();
                newRow["主键"] = "I" + this.curPrimaryKey.ToString();
                this.curPrimaryKey = this.curPrimaryKey + 1;

                this.frmMissionInput.SetMission(newRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void frmMissionAssign_InputCompletedAndNext()
        {
            this.frmMissionAssign.GetData();
            if (curAssignRowIndex > this.fpSpread1_Sheet1.RowCount - 1)
            {
                return;
            }
            for (; curAssignRowIndex < this.fpSpread1_Sheet1.RowCount; curAssignRowIndex++)
            {
                bool isSelected = false;
                for (; curAssignColIndex < this.fpSpread1_Sheet1.ColumnCount; curAssignColIndex++)
                {
                    if (this.fpSpread1_Sheet1.IsSelected(curAssignRowIndex, curAssignColIndex))
                    {
                        isSelected = true;
                        break;
                    }
                }
                if (isSelected)
                {
                    DataRow row = this.curDataDetail.Tables[0].Rows.Find(this.fpSpread1.GetCellText(0, curAssignRowIndex, "主键"));
                    curAssignRowIndex++;
                    if (row != null)
                    {
                        this.frmMissionAssign.SetMission(row);
                    }
                    break;
                }
            }
        }
        void frmMissionAssign_InputCompleted()
        {
            this.frmMissionAssign.GetData();
        }

        void frmMissionCompleted_InputCompletedAndNext()
        {
            this.frmMissionCompleted.GetData();
            if (curCompletedRowIndex > this.fpSpread1_Sheet1.RowCount - 1)
            {
                return;
            }
            for (; curCompletedRowIndex < this.fpSpread1_Sheet1.RowCount; curCompletedRowIndex++)
            {
                bool isSelected = false;
                for (; curCompletedColIndex < this.fpSpread1_Sheet1.ColumnCount; curCompletedColIndex++)
                {
                    if (this.fpSpread1_Sheet1.IsSelected(curCompletedRowIndex, curCompletedColIndex))
                    {
                        isSelected = true;
                        break;
                    }
                }
                if (isSelected)
                {
                    DataRow row = this.curDataDetail.Tables[0].Rows.Find(this.fpSpread1.GetCellText(0, curCompletedRowIndex, "主键"));
                    curCompletedRowIndex++;
                    if (row != null)
                    {
                        this.frmMissionCompleted.SetMission(row, this.curLogionAccount.AccountID);
                    }
                    break;
                }
            }
        }
        void frmMissionCompleted_InputCompleted()
        {
            this.frmMissionCompleted.GetData();
        }

        void frmMissionAssign_InputCompletedAndAll(string modelName, string accepter)
        {
            for (int rowIndex = 0; rowIndex < this.fpSpread1_Sheet1.RowCount; rowIndex++)
            {
                bool isSelected = false;
                for (int colIndex = 0; colIndex < this.fpSpread1_Sheet1.ColumnCount; colIndex++)
                {
                    if (this.fpSpread1_Sheet1.IsSelected(rowIndex, colIndex))
                    {
                        isSelected = true;
                        break;
                    }
                }
                if (!isSelected)
                {
                    continue;
                }
                if (this.fpSpread1.GetCellText(0, rowIndex, "模块名称") == modelName)
                {
                    this.fpSpread1.SetCellValue(0, rowIndex, "开发人", accepter);
                    this.fpSpread1.SetCellValue(0, rowIndex, "受任人", accepter);
                }
            }
        }

        void frmFilterSetting_InputCompleted()
        {
            this.neuGroupBox3.Controls.Clear();
            Point labelPoint = new Point(21, 29);
            Point textBoxPoint = new Point(80,26);

            for (int colIndex = 0; colIndex < this.fpSpread1_Sheet1.ColumnCount; colIndex++)
            {
                string headerLabel = this.fpSpread1_Sheet1.Columns[colIndex].Label;
                string filterState = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml", "FilterSetting", "S" + headerLabel, "False");
                if (headerLabel.Contains("日期") || headerLabel.Contains("时间"))
                { 
                    continue;
                }
                if (FS.FrameWork.Function.NConvert.ToBoolean(filterState))
                {
                    this.neuGroupBox3.Visible = true;
                    FS.FrameWork.WinForms.Controls.NeuLabel l = new FS.FrameWork.WinForms.Controls.NeuLabel();
                    l.Name = "nlb" + headerLabel;
                    l.Text = headerLabel;
                    l.AutoSize = true;
                    l.Location = labelPoint;
                    this.neuGroupBox3.Controls.Add(l);

                    FS.FrameWork.WinForms.Controls.NeuTextBox t = new FS.FrameWork.WinForms.Controls.NeuTextBox();
                    t.Name = "ntxt" + headerLabel;
                    t.Tag = headerLabel;
                    t.Width = 90;
                    t.Height = 21;
                    textBoxPoint = new Point(labelPoint.X + l.PreferredWidth + 4, textBoxPoint.Y);
                    t.Location = textBoxPoint;
                    this.neuGroupBox3.Controls.Add(t);
                    t.TextChanged += new EventHandler(t_TextChanged);
                    labelPoint = new Point(textBoxPoint.X + t.Width + 20, labelPoint.Y);

                }
            }

            if (this.neuGroupBox3.Controls.Count == 0)
            {
                this.neuGroupBox3.Visible = false;
            }
        }

        void t_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string filter = "";
                foreach (Control c in this.neuGroupBox3.Controls)
                {
                    if (c.Text == "")
                    {
                        continue;
                    }
                    if (c is FS.FrameWork.WinForms.Controls.NeuTextBox)
                    {
                        filter += c.Tag.ToString() + " Like '" + c.Text + "%' AND ";
                    }
                }
                filter = filter.TrimEnd().TrimEnd('D', 'N','A');
                curDataDetail.Tables[0].DefaultView.RowFilter = filter;
                //this.fpSpread1_Sheet1.DataSource = curDataDetail.Tables[0].DefaultView;
                this.fpSpread1.ReadSchema(this.SettingFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filter = "";
            for (int colIndex = 0; colIndex < this.fpSpread1_Sheet1.ColumnCount; colIndex++)
            {
                if (this.fpSpread1_Sheet1.Columns[colIndex].Label.Contains("日期") || this.fpSpread1_Sheet1.Columns[colIndex].Label.Contains("时间"))
                {
                    continue;
                }
                filter += this.fpSpread1_Sheet1.Columns[colIndex].Label + ",";
            }
            filter = filter.TrimEnd(',');
            frmFilterSetting.SetFilterStruct(filter, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "Profile\\MissionManager.xml");
            frmFilterSetting.ShowDialog();
        }


        void nlbLogionInfo_DoubleClick(object sender, EventArgs e)
        {
            this.curLogionAccount = null;
            this.nlbLogionInfo.Text = "未登录";
        }


        void nlbAssignMission_DoubleClick(object sender, EventArgs e)
        {
            this.Login();

            if (this.curLogionAccount.RoleID != "PM" && this.curLogionAccount.RoleID != "PSM" && this.curLogionAccount.RoleID != "LEADER")
            {
                return;
            }
            curAssignRowIndex = 0;
            curAssignColIndex = 0;
            bool isSelected = false;
            for (; curAssignRowIndex < this.fpSpread1_Sheet1.RowCount; curAssignRowIndex++)
            {
                curAssignColIndex = 0;
                for (; curAssignColIndex < this.fpSpread1_Sheet1.ColumnCount; curAssignColIndex++)
                {
                    if (this.fpSpread1_Sheet1.IsSelected(curAssignRowIndex, curAssignColIndex))
                    {
                        isSelected = true;
                        break;
                    }
                }
                if (isSelected)
                {
                    break;
                }
            }
            if (isSelected)
            {
                DataRow row = this.curDataDetail.Tables[0].Rows.Find(this.fpSpread1.GetCellText(0, curAssignRowIndex, "主键"));
                curAssignRowIndex++;
                curAssignColIndex = 0;
                if (row != null)
                {
                    this.frmMissionAssign.SetMission(row);
                    this.frmMissionAssign.ShowDialog(this);
                }
            }
            else
            {
                this.frmMissionAssign.SetMission(this.curDataDetail.Tables[0].NewRow());
                this.frmMissionAssign.ShowDialog(this);
            }
        }


        void nllPreferredHeight_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SetFarPointPreferredHeight();
        }

        private void 签出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Login();
            DateTime now = DateTime.Now;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在签出...");
            Application.DoEvents();
            for (int rowIndex = 0; rowIndex < this.fpSpread1_Sheet1.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.fpSpread1_Sheet1.ColumnCount; colIndex++)
                {
                    if (this.fpSpread1_Sheet1.IsSelected(rowIndex, colIndex))
                    {
                        //跳过不能签出的行
                        this.CheckOut(rowIndex,now);
                        break;
                    }

                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void 签入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Login();
           
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在签入...");
            Application.DoEvents();
            for (int rowIndex = 0; rowIndex < this.fpSpread1_Sheet1.RowCount; rowIndex++)
            {
                bool isSelected = false;
                for (int colIndex = 0; colIndex < this.fpSpread1_Sheet1.ColumnCount; colIndex++)
                {
                    if (this.fpSpread1_Sheet1.IsSelected(rowIndex, colIndex))
                    {
                        isSelected = true;
                        break;
                    }
                }
                if(!isSelected)
                {
                    continue;
                }

                string SQL = "";
                string ID = this.fpSpread1.GetCellText(0, rowIndex, "主键");

                if (ID.StartsWith("I"))
                {
                    #region 新加的数据插入表
                    ID = this.curMissionManager.GetMissionPrimaryKey();
                    if (ID == "-1")
                    {
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("获取主键发送错误！");
                        return;
                    }
                    SQL = "insert into view_soc_project_mission( ";
                    foreach (DataColumn dc in curDataDetail.Tables[0].Columns)
                    {
                        if (this.VirtrueColumns.Contains(dc.ColumnName))
                        {
                            //虚拟列，不更新
                            continue;
                        }
                        SQL += dc.ColumnName + ",\n";
                    }
                    SQL = SQL.TrimEnd('\n').TrimEnd(',') + "\n) values \n(\n";
                    foreach (DataColumn dc in curDataDetail.Tables[0].Columns)
                    {
                        string val = this.fpSpread1.GetCellText(0, rowIndex, dc.ColumnName);
                        if (this.VirtrueColumns.Contains(dc.ColumnName))
                        {
                            //虚拟列，不更新
                            continue;
                        }
                        
                        if (dc.ColumnName == "主键")
                        {
                            val = ID;
                        }

                        if (dc.ColumnName.Contains("时间") || dc.ColumnName.Contains("日期"))
                        {
                            if (dc.ColumnName == "录入时间" || dc.ColumnName == "录入日期")
                            {
                                if (val == "")
                                {
                                    val = "to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                                    val = string.Format(val, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    this.fpSpread1.SetCellValue(0, rowIndex, dc.ColumnName, DateTime.Now);
                                }
                                else
                                {
                                    val = "''";
                                }
                            }
                            else
                            {
                                DateTime dt = new DateTime();
                                if (val != "" && DateTime.TryParse(val, out dt))
                                {
                                    val = "to_date('{0}','yyyy-mm-dd hh24:mi:ss')";
                                    val = string.Format(val, dt.ToString("yyyy-MM-dd HH:mm:ss"));
                                    this.fpSpread1.SetCellValue(0, rowIndex, dc.ColumnName, dt);
                                }
                                else
                                {
                                    val = "''";
                                }
                            }
                            if (curDataDetail.Tables[0].Columns.IndexOf(dc) == curDataDetail.Tables[0].Columns.Count - 1)
                            {
                                SQL += "" + val + "--" + dc.ColumnName + "\n";
                            }
                            else
                            {
                                SQL += "" + val + ",--" + dc.ColumnName + "\n";
                            }
                            continue;
                        }
                        else if (dc.ColumnName == "录入人" && val == "")
                        {
                            val = this.curLogionAccount.AccountID;
                            this.fpSpread1.SetCellValue(0, rowIndex, dc.ColumnName, this.curLogionAccount.AccountID);
                        }

                        if (curDataDetail.Tables[0].Columns.IndexOf(dc) == curDataDetail.Tables[0].Columns.Count - 1)
                        {
                            SQL += "'" + val + "'--" + dc.ColumnName + "\n";
                        }
                        else
                        {
                            SQL += "'" + val + "',--" + dc.ColumnName + "\n";
                        }
                    }
                    SQL = SQL.TrimEnd(',') + ")";
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    if (curMissionManager.ExecNoQuery(SQL) != 1)
                    {
                        this.fpSpread1_Sheet1.Rows[rowIndex].Label = "╳";
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Rows[rowIndex].Label = (rowIndex + 1).ToString();
                        this.fpSpread1.SetCellValue(0, rowIndex, "主键", ID);
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    #endregion
                }
                else
                {
                    #region 原有的数据更新表
                    SQL = "update view_soc_project_mission set ";
                    foreach (DataColumn dc in curDataDetail.Tables[0].Columns)
                    {
                        string val = this.fpSpread1.GetCellText(0, rowIndex, dc.ColumnName);
                        if (this.VirtrueColumns.Contains(dc.ColumnName))
                        {
                            //虚拟列，不更新
                            continue;
                        }
                        else if (dc.ColumnName.Contains("时间") || dc.ColumnName.Contains("日期"))
                        {
                            DateTime dt = new DateTime();
                            if (val != "" && DateTime.TryParse(val, out dt))
                            {
                                SQL += dc.ColumnName + " = to_date('" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss'),";
                            }
                        }
                        else if (dc.ColumnName == "锁定标记")
                        {
                            SQL += dc.ColumnName + " = '否',"; 
                        }
                       
                        else if (dc.ColumnName == "锁定人")
                        {
                            SQL += dc.ColumnName + " = '',";
                        }
                        else
                        {
                            SQL += dc.ColumnName + " = '" + val + "',";
                        }
                    }

                    SQL = SQL.TrimEnd(',');
                    SQL += " where 主键 = '" + ID + "' and 锁定标记 = '是' and 锁定人 = '" + this.curLogionAccount.AccountID + "'";

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    if (curMissionManager.ExecNoQuery(SQL) != 1)
                    {
                        this.fpSpread1_Sheet1.Rows[rowIndex].Label = "╳";
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    else
                    {
                        this.fpSpread1.SetCellValue(0, rowIndex, "锁定标记", "否");
                        this.fpSpread1.SetCellValue(0, rowIndex, "锁定人", "");
                        this.fpSpread1_Sheet1.Rows[rowIndex].Label = (rowIndex + 1).ToString();
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }

                    #endregion
                }

            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void 重置序号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int rowIndex = 0; rowIndex < this.fpSpread1_Sheet1.RowCount; rowIndex++)
            {
                this.fpSpread1.SetCellValue(0, rowIndex, "序号", (rowIndex + 1).ToString());
            }
        }

        private void 撤销签出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Login();
           
            DateTime now = DateTime.Now;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在撤销签出...");
            Application.DoEvents();
            for (int rowIndex = 0; rowIndex < this.fpSpread1_Sheet1.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.fpSpread1_Sheet1.ColumnCount; colIndex++)
                {
                    if (this.fpSpread1_Sheet1.IsSelected(rowIndex, colIndex))
                    {
                        //跳过不能签出的行
                        this.CancelCheckOut(rowIndex);
                        break;
                    }

                }
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        private void missionCompletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Login();

            curCompletedRowIndex = 0;
            curCompletedColIndex = 0;
            bool isSelected = false;
            for (; curCompletedRowIndex < this.fpSpread1_Sheet1.RowCount; curCompletedRowIndex++)
            {
                curCompletedColIndex = 0;
                for (; curCompletedColIndex < this.fpSpread1_Sheet1.ColumnCount; curCompletedColIndex++)
                {
                    if (this.fpSpread1_Sheet1.IsSelected(curCompletedRowIndex, curCompletedColIndex))
                    {
                        isSelected = true;
                        break;
                    }
                }
                if (isSelected)
                {
                    break;
                }
            }
            if (isSelected)
            {
                DataRow row = this.curDataDetail.Tables[0].Rows.Find(this.fpSpread1.GetCellText(0, curCompletedRowIndex, "主键"));
                curCompletedRowIndex++;
                curCompletedColIndex = 0;
                if (row != null)
                {
                    this.frmMissionCompleted.SetMission(row, this.curLogionAccount.AccountID);
                    this.frmMissionCompleted.ShowDialog(this);
                }
            }
        }
    }
}
