using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Fee;
using FS.HISFC.Models.Registration;

namespace GZSI.Controls
{
    /// <summary>
    /// ucGetSiInHosInfo 的摘要说明
    /// </summary>
    public partial class ucGetSiInHosInfo : System.Windows.Forms.UserControl
    {

        public ucGetSiInHosInfo()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();
            // TODO: 在 InitializeComponent 调用后添加任何初始化
        }

        FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
        FS.HISFC.BizLogic.Fee.InPatient myFee = new FS.HISFC.BizLogic.Fee.InPatient();
        Management.SIConnect myConnectSI;
        ArrayList alPatientInfo = new ArrayList();
        DataTable dtIn = new DataTable();
        DataTable dtInfo = new DataTable();
        DataTable dtDisplay = new DataTable();
        DataView dvDisplay = new DataView();
        private FS.HISFC.Models.RADT.PatientInfo personInfo = new FS.HISFC.Models.RADT.PatientInfo();
        private PatientType queryPatientType = PatientType.InPatient;
        private string regNo = null;
        private bool isSpecialPact = false;

        /// <summary>
        /// 是否住院处理的医保（门特）
        /// </summary>
        public bool IsSpecialPact
        {
            get { return this.isSpecialPact; }
            set { isSpecialPact = value; }
        }
        /// <summary>
        /// 是否住院处理的医保（单病种、从化医保）
        /// </summary>
        private bool isInPatientDealPact = false;

        /// <summary>
        /// 是否住院处理的医保（单病种、从化医保）
        /// </summary>
        public bool IsInPatientDealPact
        {
            get { return this.isInPatientDealPact; }
            set { isInPatientDealPact = value; } 
        }


        private bool isSpecialSiServer = false;
        /// <summary>
        /// 是否需要更换医保服务器
        /// </summary>
        public bool IsSpecialSiServer
        {
            get { return this.isSpecialSiServer; }
            set { isSpecialSiServer = value; }
        }

        /// <summary>
        /// 就诊登记号
        /// </summary>
        public string RegNo
        {
            get { return regNo; }
        }

        private string regName = string.Empty;
        public string RegName
        {
            get { return regName; }
            set { regName = value; }
        }
        /// <summary>
        /// 查询的患者类型 住院/门诊
        /// </summary>
        public PatientType QueryPatientType
        {
            set
            {
                this.queryPatientType = value;
            }
        }
        public FS.HISFC.Models.RADT.PatientInfo PersonInfo
        {
            get
            {
                return personInfo;
            }
            set
            {
                personInfo = value;
            }
        }
        /// <summary>
        /// 设置指定日期
        /// </summary>
        public DateTime DtPatientIn
        {
            set 
            {
                this.dateTimePicker1.Value = value;
            }
        }


        /// <summary>
        /// 初始化信息
        /// </summary>
        private int Init()
        {

            try
            {
                if (this.isSpecialSiServer)
                {
                    myConnectSI = new global::GZSI.Management.SIConnect(System.Windows.Forms.Application.StartupPath + @".\profile\SiDataBaseYBMM.xml");
                }
                else
                {
                    myConnectSI = new Management.SIConnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接医保服务器失败!,请重新配置连接" + ex.Message);
                usSetConnectSqlServer usSetConnectSqlServer1 = new usSetConnectSqlServer();
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(usSetConnectSqlServer1);
                this.personInfo = null;
                return -1;
            }

            return 0;
        }

        private void InitData()
        {
            DateTime dtNow = this.dateTimePicker1.Value;

            if (myConnectSI == null)
            {
                return;
            }

            if (queryPatientType == PatientType.InPatient)
            {
                alPatientInfo = myConnectSI.GetRegPersonInfo(dtNow, "1");
                try
                {
                    this.DisPlayDataInpatient();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                //住院收费处增加门特病人选取
                if (this.isSpecialPact)  //门特
                {
                    alPatientInfo.AddRange( myConnectSI.GetRegPersonInfo(dtNow, "2"));
                    try
                    {
                        this.DisPlayDataInpatient();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            else
            {   //门诊
                try
                {
                    if (this.isSpecialPact)  //门特
                    {
                        alPatientInfo = myConnectSI.GetRegPersonInfo(dtNow, "2");
                        try
                        {
                            this.DisPlayDataInpatient();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                    else if (this.isInPatientDealPact)//单病种、从化
                    {
                        alPatientInfo = myConnectSI.GetRegPersonInfo(dtNow, "1");
                        try
                        {
                            this.DisPlayDataInpatient();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
 
                    }
                    else  //门慢
                    {
                        alPatientInfo = myConnectSI.GetRegInfo(dtNow);
                        try
                        {
                            this.DisPlayDataClinic();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            //alPatientInfo = myConnectSI.GetRegPersonInfo(dtNow);
            //try
            //{
            //    this.DisPlayDataInpatient();
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}

        }

        /// <summary>
        /// 初始化列信息
        /// </summary>
        public void InitColumns()
        {
            int width = 20;

            this.fpSpread1_Sheet1.Columns[0].Visible = true;
            this.fpSpread1_Sheet1.Columns[1].Width = width * 3;
            this.fpSpread1_Sheet1.Columns[2].Width = width * 6;
            this.fpSpread1_Sheet1.Columns[3].Width = width * 3;
            this.fpSpread1_Sheet1.Columns[4].Width = width * 3;
            this.fpSpread1_Sheet1.Columns[5].Visible = false;
            this.fpSpread1_Sheet1.Columns[6].Width = width * 3;
            this.fpSpread1_Sheet1.Columns[7].Width = width * 10;
            this.fpSpread1_Sheet1.Columns[8].Width = width * 4;
            this.fpSpread1_Sheet1.Columns[9].Width = width * 6;
            this.fpSpread1_Sheet1.Columns[10].Width = width * 4;
            this.fpSpread1_Sheet1.Columns[11].Visible = false;
            this.fpSpread1_Sheet1.Columns[12].Visible = false;
            this.fpSpread1_Sheet1.Columns[13].Width = width * 6;
            this.fpSpread1_Sheet1.Columns[14].Width = width * 4;
            this.fpSpread1_Sheet1.Columns[15].Width = width * 6;
            this.fpSpread1_Sheet1.Columns[16].Width = width * 6;


        }

        private void DisPlayDataInpatient()
        {
            //if (queryPatientType == PatientType.OutPatient)
            //{
            //    return;
            //}
            if (alPatientInfo == null && alPatientInfo.Count == 0)
            {
                return;
            }
            Type str = typeof(System.String);
            Type date = typeof(System.DateTime);
            dtDisplay = new DataTable();


            dtDisplay.Columns.AddRange(new DataColumn[]{new DataColumn("就医登记号", str),
                                                        new DataColumn("医院编号", str), 
                                                        new DataColumn("身份证号", str),
                                                        new DataColumn("姓名", str),
                                                        new DataColumn("人员类别", str),
                                                        new DataColumn("住院号", str),
                                                        new DataColumn("就诊类别", str),
                                                        new DataColumn("单位", str),
                                                        new DataColumn("入院日期", date),
                                                        new DataColumn("门诊诊断", str),
                                                        new DataColumn("ICD10码", str),
                                                        new DataColumn("住院科室", str),
                                                        new DataColumn("床位代号", str),
                                                        new DataColumn("特诊对象审批号", str),
                                                        new DataColumn("出生日期", date),
                                                        new DataColumn("性别",str),  //Add By Maokb
                                                        new DataColumn("基本医疗参保日期", date),													    
                                                        new DataColumn("基本医疗参保状态", str)});

            foreach (FS.HISFC.Models.RADT.PatientInfo obj in alPatientInfo)
            {
                DataRow rowDisplay = dtDisplay.NewRow();

                rowDisplay["就医登记号"] = obj.SIMainInfo.RegNo;
                rowDisplay["医院编号"] = obj.SIMainInfo.HosNo;
                rowDisplay["身份证号"] = obj.IDCard;
                rowDisplay["姓名"] = obj.Name;
                //rowDisplay["人员类别"] = myFee.GetComDictionaryNameById("EMPLSTATUS", obj.SIMainInfo.EmplType);//Modify By Maokb,
                rowDisplay["人员类别"] = this.GetTypeName(obj.SIMainInfo.EmplType, "code");//Modify By Maokb
                rowDisplay["住院号"] = obj.PID.PatientNO;
                rowDisplay["单位"] = obj.CompanyName;
                rowDisplay["就诊类别"] = GetJZName(obj.PVisit.MedicalType.ID, "code");
                rowDisplay["入院日期"] = obj.PVisit.InTime;
                rowDisplay["门诊诊断"] = obj.SIMainInfo.InDiagnose.Name;
                rowDisplay["ICD10码"] = obj.SIMainInfo.InDiagnose.ID;
                rowDisplay["住院科室"] = obj.PVisit.PatientLocation.Dept.ID;
                rowDisplay["床位代号"] = obj.PVisit.PatientLocation.Bed.ID;
                rowDisplay["特诊对象审批号"] = obj.SIMainInfo.AppNo;
                rowDisplay["出生日期"] = obj.Birthday;
                rowDisplay["性别"] = obj.Sex.ID;

                dtDisplay.Rows.Add(rowDisplay);
            }
            dvDisplay = new DataView(dtDisplay);
            this.fpSpread1_Sheet1.DataSource = dvDisplay;
            InitColumns();
        }

        private void DisPlayDataClinic()
        {
            if (queryPatientType == PatientType.InPatient)
            {
                return;
            }
            if (alPatientInfo == null || alPatientInfo.Count == 0)
            {
                return;
            }
            Type str = typeof(System.String);
            Type date = typeof(System.DateTime);
            dtDisplay = new DataTable();

            dtDisplay.Columns.AddRange(new DataColumn[]{new DataColumn("就医登记号", str),
                                                        new DataColumn("医院编号", str), 
                                                        new DataColumn("身份证号", str),
                                                        new DataColumn("姓名", str),
                                                        new DataColumn("人员类别", str),
                                                        new DataColumn("住院号", str),
                                                        new DataColumn("就诊类别", str),
                                                        new DataColumn("单位", str),
                                                        new DataColumn("挂号日期", date),
                                                        new DataColumn("门诊诊断", str),
                                                        new DataColumn("ICD10码", str),
                                                        new DataColumn("住院科室", str),
                                                        new DataColumn("床位代号", str),
                                                        new DataColumn("特诊对象审批号", str),
                                                        new DataColumn("出生日期", date),
                                                        new DataColumn("性别",str),  //Add By Maokb
                                                        new DataColumn("基本医疗参保日期", date),													    
                                                        new DataColumn("基本医疗参保状态", str)});

            foreach (FS.HISFC.Models.Registration.Register obj in this.alPatientInfo)
            {

                DataRow rowDisplay = dtDisplay.NewRow();

                rowDisplay["就医登记号"] = obj.SIMainInfo.RegNo;
                rowDisplay["医院编号"] = obj.SIMainInfo.HosNo;
                rowDisplay["身份证号"] = obj.IDCard;
                rowDisplay["姓名"] = obj.Name;
                rowDisplay["人员类别"] = this.GetTypeName(obj.SIMainInfo.EmplType, "code");
                rowDisplay["单位"] = obj.CompanyName;
                rowDisplay["就诊类别"] = GetJZName(obj.SIMainInfo.MedicalType.ID, "code");
                rowDisplay["挂号日期"] = obj.DoctorInfo.SeeDate;
                rowDisplay["门诊诊断"] = obj.SIMainInfo.InDiagnose.Name;
                rowDisplay["ICD10码"] = obj.SIMainInfo.InDiagnose.ID;
                rowDisplay["特诊对象审批号"] = obj.SIMainInfo.AppNo;
                rowDisplay["出生日期"] = obj.Birthday;
                rowDisplay["性别"] = obj.Sex.ID;
                dtDisplay.Rows.Add(rowDisplay);
            }
            dvDisplay = new DataView(dtDisplay);
            this.fpSpread1_Sheet1.DataSource = dvDisplay;
            InitColumns();
        }

        /// <summary>
        /// 获得性别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetSexName(string code)
        {
            if (code == "0")
                return "女";
            else
                return "男";
        }
        /// <summary>
        /// 获得人员类别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetTypeName(string code, string flag)
        {
            //1－在职2－退休3－离休4－1－4级工残5－无业7－退职

            //string temp = "";
            if (flag == "code")
            {
                //				switch(code)
                //				{
                //					case "1":
                //						temp = "在职";
                //						break;
                //					case "2":
                //						temp = "退休";
                //						break;
                //					case "3":
                //						temp = "ss";
                //						break;
                //				}
                //if (code == "1")
                //    return "在职";
                //else
                //    return "退休";
                if (code == "1")
                    return "在职";
                else if (code == "2")
                    return "退休";
                else if (code == "3")
                    return "离休";
                else if (code == "4")
                    return "1-4级工残";
                else if (code == "5")
                    return "无业";
                else if (code == "6")
                    return "已趸缴";
                else if (code == "7")
                    return "退职";
                else if (code == "70")
                    return "学龄前儿童";
                else if (code == "71")
                    return "中小学生";
                else if (code == "72")
                    return "大中专学生";
                else if (code == "73")
                    return "其他未成年人";
                else if (code == "74")
                    return "非从业人员";
                else if (code == "75")
                    return "老年人";
                else if (code == "66")
                {
                    return "企业离休";
                }
                else if (code == "62")
                {
                    return "公医退休";
                }
                else if (code == "61")
                {
                    return "公医在职";
                }
                else
                    return "居民医保";
            }
            else
            {
                if (code == "在职")
                    return "1";
                else if (code == "退休")
                    return "2";
                else if (code == "离休")
                    return "3";
                else if (code == "1-4级工残")
                    return "4";
                else if (code == "无业")
                    return "5";
                else if (code == "已趸缴")
                    return "6";
                else if (code == "退职")
                    return "7";
                else if (code == "学龄前儿童")
                    return "70";
                else if (code == "中小学生")
                    return "71";
                else if (code == "大中专学生")
                    return "72";
                else if (code == "其他未成年人")
                    return "73";
                else if (code == "非从业人员")
                    return "74";
                else if (code == "老年人")
                    return "75";
                else if (code == "企业离休")
                {
                    return "66";
                }
                else if (code == "公医退休")
                {
                    return "62";
                }
                else if (code == "公医在职")
                {
                    return "61";
                }
                else
                    return "2";
            }

        }
        /// <summary>
        /// 获得就诊类别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetJZName(string code, string flag)
        {
            if (flag == "code")
            {
                if (code == "1")
                    return "住院";
                else
                    return "门诊特定项目";
            }
            else
            {
                if (code == "住院")
                    return "1";
                else
                    return "2";
            }
        }

        /// <summary>
        /// 获得参保类型名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetCBZTName(string code, string flag)
        {
            if (flag == "code")
            {
                if (code == "3")
                    return "参保缴费";
                if (code == "4")
                    return "暂停缴费";
                else
                    return "终止参保";
            }
            else
            {
                if (code == "参保缴费")
                    return "3";
                if (code == "暂停缴费")
                    return "4";
                else
                    return "7";
            }
        }

        /// <summary>
        /// 给人员信息赋值
        /// </summary>
        private void GetPersonInfo()
        {
            this.personInfo = new FS.HISFC.Models.RADT.PatientInfo();
            this.regNo = null;

            int i = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (this.fpSpread1_Sheet1.RowCount <= 0)
            {
                return;
            }
            if(string.IsNullOrEmpty(this.fpSpread1_Sheet1.Cells[i, 0].Text))
                return;

            this.personInfo.SIMainInfo.RegNo = this.fpSpread1_Sheet1.Cells[i, 0].Text;
            this.personInfo.SIMainInfo.HosNo = this.fpSpread1_Sheet1.Cells[i, 1].Text;
            this.personInfo.IDCard = this.fpSpread1_Sheet1.Cells[i, 2].Text;
            //this.personInfo.SSN = this.fpSpread1_Sheet1.Cells[i, 2].Text;
            if (string.IsNullOrEmpty(this.personInfo.IDCard))
            {
                DialogResult dt = MessageBox.Show("该患者的身份证号码为空，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2);
                if (dt == DialogResult.Yes)
                {
                    //this.personInfo.SSN = "123456";
                    this.personInfo.IDCard = "123456";
                }
                else
                {
                    return;
                }
            }
            if (/*this.personInfo.SSN != "123456" &&*/ this.personInfo.IDCard != "123456")
            {
                //存在身份证号码
                if (!string.IsNullOrEmpty(regName) && this.fpSpread1_Sheet1.Cells[i, 3].Text.Equals(regName) == false)
                {
                    if (MessageBox.Show(string.Format("当前选择的患者信息【{0}】与【{1}】不符合，是否继续？", this.fpSpread1_Sheet1.Cells[i, 3].Text, regName), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        return;
                    }
                }
                
            }

            this.personInfo.Name = this.fpSpread1_Sheet1.Cells[i, 3].Text;
            this.personInfo.Name = this.fpSpread1_Sheet1.Cells[i, 3].Text;

            this.personInfo.SIMainInfo.EmplType = GetTypeName(this.fpSpread1_Sheet1.Cells[i, 4].Text, "Name");
            this.personInfo.CompanyName = this.fpSpread1_Sheet1.Cells[i, 7].Text;
            this.personInfo.PVisit.MedicalType.Name = this.fpSpread1_Sheet1.Cells[i, 6].Text;
            this.personInfo.PVisit.MedicalType.ID = GetJZName(this.fpSpread1_Sheet1.Cells[i, 6].Text, "Name");
            this.personInfo.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, 8].Text);
            this.personInfo.SIMainInfo.InDiagnose.Name = this.fpSpread1_Sheet1.Cells[i, 9].Text;
            this.personInfo.SIMainInfo.InDiagnose.ID = this.fpSpread1_Sheet1.Cells[i, 10].Text;
            this.personInfo.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread1_Sheet1.Cells[i, 13].Text);
            this.personInfo.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[i, 14].Text);
            string SexID = this.fpSpread1_Sheet1.Cells[i, 15].Text.Trim();
            if(!string.IsNullOrEmpty(SexID))
            {
                if (SexID.Equals("1"))
                {
                    this.personInfo.Sex.ID = "M";
                }
                if (SexID.Equals("2"))
                {
                    this.personInfo.Sex.ID = "F";
                }
            }
            //this.personInfo.Sex.ID = this.fpSpread1_Sheet1.Cells[i, 15].Text;//Add By Maokb
            this.personInfo.SIMainInfo.IsValid = true;

            this.regNo = this.fpSpread1_Sheet1.Cells[i, 0].Text;

            //#if DEBUG
            //            Management.SIConnect con = new Management.SIConnect();
            //            con.WriteLog(RegNo + " \n " + PersonInfo.Name);

            this.FindForm().Close();
        }

        /// <summary>
        /// 清空原有已挂医保患者信息
        /// </summary>
        public void ClearPersonInfo()
        {
            this.personInfo = new FS.HISFC.Models.RADT.PatientInfo();
            this.regNo = null;
        }

        private void ucGetSiInHosInfo_Load(object sender, System.EventArgs e)
        {
            //this.dateTimePicker1.Value = myInterface.GetDateTimeFromSysDateTime();

            if (Init() == -1)
            {
                //				this.FindForm().Close();
                //				return;
            }

            InitData();

            this.FindForm().Text = "选择医保登记患者信息";
            this.regNo = null;
            this.personInfo = new FS.HISFC.Models.RADT.PatientInfo();
           // this.rbCode.Checked = true;
            this.rbName.Checked = true;

            this.tbFilter.Text = regName;
            tbFilter_TextChanged(null, null);

            this.panel2.Select();
            this.panel2.Focus();
            this.fpSpread1.Select();
            this.fpSpread1.Focus();
            this.fpSpread1_Sheet1.ActiveRowIndex = 0;
           
        }

        private void tbFilter_TextChanged(object sender, System.EventArgs e)
        {
            if (this.regName.IndexOf("之子")>0 || this.regName.IndexOf("之女")>0)
            {
                this.tbFilter.Text = "";
            }
            string filterString = "";
            filterString = "姓名" + " like '" + this.tbFilter.Text + "%' or  就医登记号 like '" + this.tbFilter.Text + "%' " + " or  身份证号 like '" + this.tbFilter.Text + "%' ";

            //if (rbName.Checked)
            //{
            //    filterString = "姓名" + " like '" + this.tbFilter.Text + "%'";
            //}
            //else
            //{
            //    filterString = "就医登记号" + " like '" + this.tbFilter.Text + "%'";
            //}
            this.dvDisplay.RowFilter = filterString;
            InitColumns();
        }

        private void bOk_Click(object sender, System.EventArgs e)
        {
            this.GetPersonInfo();
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.GetPersonInfo();
        }

        private void fpSpread1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.GetPersonInfo();
        }

        private void tbFilter_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.fpSpread1.SetViewportTopRow(0, this.fpSpread1_Sheet1.ActiveRowIndex - 5);
                this.fpSpread1_Sheet1.ActiveRowIndex--;
                this.fpSpread1_Sheet1.AddSelection(this.fpSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.fpSpread1.SetViewportTopRow(0, this.fpSpread1_Sheet1.ActiveRowIndex - 4);
                this.fpSpread1_Sheet1.ActiveRowIndex++;
                this.fpSpread1_Sheet1.AddSelection(this.fpSpread1_Sheet1.ActiveRowIndex, 0, 1, 0);
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.GetPersonInfo();
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            ClearPersonInfo();
            this.FindForm().Close();
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.Init();

            string text = this.tbFilter.Text.Trim();
            InitData();
            this.tbFilter.Text = string.Empty;
            this.tbFilter.Text = text;

            this.fpSpread1.Focus();
            Cursor.Current = Cursors.Arrow;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            usSetConnectSqlServer uc = new usSetConnectSqlServer();
            Form frmTemp = new Form();
            frmTemp.Width = uc.Width + 8;
            frmTemp.Height = uc.Height + 34;
            uc.Dock = DockStyle.Fill;
            frmTemp.Controls.Add(uc);
            frmTemp.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            //显示临时窗口
            frmTemp.ShowDialog();
        }

        private void button1_Click_1(object sender, System.EventArgs e)
        {
            FS.HISFC.BizLogic.RADT.InPatient myInpatient = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
            Management.SIConnect myConn = new Management.SIConnect();
            FS.HISFC.BizLogic.Fee.InPatient myFee = new FS.HISFC.BizLogic.Fee.InPatient();

            FS.HISFC.Models.RADT.PatientInfo pInfo = myInpatient.QueryPatientInfoByInpatientNO("ZY010020050136");

            FS.HISFC.Models.RADT.PatientInfo pInfoSi = myInterface.GetSIPersonInfo("ZY010020050136");

            pInfo.SIMainInfo = pInfoSi.SIMainInfo;

            ArrayList al = myFee.QueryFeeItemLists("ZY010020050136", pInfo.PVisit.InTime, myFee.GetDateTimeFromSysDateTime());

            myConn.UpdateBalaceReadFlag("0002", 1);
            myConn.Commit();

        }

        public enum PatientType
        {
            /// <summary>
            /// 门诊 1
            /// </summary>
            OutPatient = 1,
            /// <summary>
            /// 住院 2
            /// </summary>
            InPatient = 2
        }

       
    }
}




