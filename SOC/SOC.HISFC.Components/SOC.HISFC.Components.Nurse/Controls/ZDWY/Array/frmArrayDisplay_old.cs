using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Nurse.Controls.ZDWY.Array
{
    /// <summary>
    /// 中大五院门诊护士分诊外屏显示
    /// </summary>
    public partial class frmArrayDisplay_old : Form, FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay
    {
        public frmArrayDisplay_old()
        {
            InitializeComponent();

            //timerShow.Tick += new EventHandler(timerShow_Tick);
            timerCall.Tick += new EventHandler(timerCall_Tick);
            timerChange.Tick += new EventHandler(timerChange_Tick);

            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
        }

        string showXML = Application.StartupPath + "/Setting/NurseShowSetting.xml";

        void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, showXML);
            }
        }


        #region 变量
        private Classes.CallSpeak callSpeak = new FS.SOC.HISFC.Components.Nurse.Classes.CallSpeak();
        private FS.HISFC.BizLogic.Nurse.Assign assignMgr = new FS.HISFC.BizLogic.Nurse.Assign();

        FS.SOC.HISFC.Assign.BizLogic.Assign socAsignMgr = new FS.SOC.HISFC.Assign.BizLogic.Assign();
        private FS.SOC.HISFC.CallQueue.BizLogic.NurseAssign nurseAssignMgr = new FS.SOC.HISFC.CallQueue.BizLogic.NurseAssign();

        private FS.HISFC.Models.Base.Employee oper = ((FS.HISFC.Models.Base.Employee)FrameWork.Management.Connection.Operator);

        /// <summary>
        /// 一页显示的行数
        /// </summary>
        private int showRowCount = 2;

        /// <summary>
        /// 当前页码
        /// </summary>
        private int currentPage = 0;
        
        #endregion

        #region 事件

        /// <summary>
        /// 是否隐藏姓名
        /// </summary>
        bool isDiaplayName = true;

        /// <summary>
        /// 医生或诊室名称字体大小
        /// </summary>
        float doctFontSize = 40F;

        /// <summary>
        /// 患者姓名字体大小
        /// </summary>
        float patientFontSize = 40F;

        /// <summary>
        /// 候诊人数字体大小
        /// </summary>
        float countFontSize = 40F;

        /// <summary>
        /// 显示列头字体大小
        /// </summary>
        float colTitleSize = 40F;

        /// <summary>
        /// 显示的自动刷新时间(s)
        /// </summary>
        int freshTime = 5;

        /// <summary>
        /// 行高
        /// </summary>
        float rowHeight = 100;

        private int Init()
        {
            #region 获取设置

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            string pathName = Application.StartupPath + "/Setting/NurseSetting.xml";
            if (System.IO.File.Exists(pathName))
            {
                doc.Load(pathName);
                System.Xml.XmlNode xn = doc.SelectSingleNode("//是否隐藏姓名");
                if (xn != null)
                {
                    isDiaplayName = FS.FrameWork.Function.NConvert.ToBoolean(xn.Attributes[0].Value);
                }

                System.Xml.XmlNode xnDoct = doc.SelectSingleNode("//医生姓名字体");
                if (xnDoct != null)
                {
                    try
                    {
                        doctFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnDoct.Attributes[0].Value);
                    }
                    catch (Exception)
                    {
                        doctFontSize = 30;
                    }

                }
                System.Xml.XmlNode xnPatient = doc.SelectSingleNode("//患者姓名字体");
                if (xnPatient != null)
                {
                    try
                    {
                        patientFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnPatient.Attributes[0].Value);
                    }
                    catch (Exception)
                    {
                        patientFontSize = 30;
                    }

                }

                System.Xml.XmlNode xnColTitle = doc.SelectSingleNode("//列头字体");
                if (xnColTitle != null)
                {
                    try
                    {
                        colTitleSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnColTitle.Attributes[0].Value);
                    }
                    catch (Exception)
                    {
                        patientFontSize = 30;
                    }
                }

                System.Xml.XmlNode xnCount = doc.SelectSingleNode("//候诊人数字体");
                if (xnCount != null)
                {
                    try
                    {
                        countFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnCount.Attributes[0].Value);
                    }
                    catch (Exception)
                    {
                        countFontSize = 30;
                    }
                    if (countFontSize < 0)
                    {
                        countFontSize = 0;
                    }
                }
                xnCount = doc.SelectSingleNode("//自动分诊刷新");
                if (xnCount != null)
                {
                    try
                    {
                        freshTime = FS.FrameWork.Function.NConvert.ToInt32(xnCount.Attributes[0].Value);
                    }
                    catch (Exception)
                    {
                        freshTime = 5;
                    }
                }

                System.Xml.XmlNode xnPageRowCount = doc.SelectSingleNode("//每页显示候诊队列个数");
                if (xnPageRowCount != null)
                {
                    try
                    {
                        showRowCount = FS.FrameWork.Function.NConvert.ToInt32(xnPageRowCount.Attributes[0].Value);
                    }
                    catch (Exception)
                    {
                        showRowCount = 8;
                    }

                }
            }

            #endregion

            #region 初始化FarPoint显示

            pnTop.BackColor = Color.DarkTurquoise;
            pnTop.ForeColor = Color.White;
            lblCallInfo.BackColor = Color.DarkTurquoise;
            lblCallInfo.ForeColor = Color.White;

            FarPoint.Win.LineBorder whiteBorder = new FarPoint.Win.LineBorder(System.Drawing.Color.White);

            fpSpread1_Sheet1.RowHeader.ColumnCount = 0;
            fpSpread1.BackColor = Color.White;

            for (int row = 0; row < fpSpread1_Sheet1.ColumnHeader.RowCount; row++)
            {
                fpSpread1_Sheet1.ColumnHeader.Rows[row].Border = whiteBorder;
                fpSpread1_Sheet1.ColumnHeader.Rows[row].BackColor = Color.DarkTurquoise;
            }

            for (int row = 0; row < fpSpread1_Sheet1.RowCount; row++)
            {
                fpSpread1_Sheet1.Rows[row].Border = whiteBorder;
            }

            for (int row = 0; row < fpSpread1_Sheet1.ColumnHeader.RowCount; row++)
            {
                fpSpread1_Sheet1.ColumnHeader.Rows[row].Font = new Font(fpSpread1.Font.FontFamily, this.colTitleSize);
            }

            fpSpread1_Sheet1.ColumnCount = (Int32)EnumCol.ColCout;
            for (int col = 0; col < fpSpread1_Sheet1.ColumnCount; col++)
            {
                this.fpSpread1_Sheet1.Columns[(Int32)EnumCol.D队列名称].Label = "诊室（医生）";
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D队列名称].Font = new Font(fpSpread1.Font.FontFamily, doctFontSize);
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D队列名称].BackColor = Color.White;
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D队列名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D队列名称].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                this.fpSpread1_Sheet1.Columns[(Int32)EnumCol.Z在诊患者].Label = "在诊(排序号)";
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.Z在诊患者].Font = new Font(fpSpread1.Font.FontFamily, patientFontSize);
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.Z在诊患者].BackColor = Color.White;
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.Z在诊患者].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.Z在诊患者].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                this.fpSpread1_Sheet1.Columns[(Int32)EnumCol.D待诊1].Label = "待诊1(排序号)";
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D待诊1].Font = new Font(fpSpread1.Font.FontFamily, patientFontSize);
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D待诊1].BackColor = Color.White;
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D待诊1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D待诊1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                this.fpSpread1_Sheet1.Columns[(Int32)EnumCol.D待诊2].Label = "待诊2(排序号)";
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D待诊2].Font = new Font(fpSpread1.Font.FontFamily, patientFontSize);
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D待诊2].BackColor = Color.White;
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D待诊2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                fpSpread1_Sheet1.Columns[(Int32)EnumCol.D待诊2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            #endregion

            #region 第一次初始化显示队列信息

            int rev = SetQueue();
            if (rev < 0)
            {
                return -1;
            }

            #endregion

            timerCall.Enabled = true;
            timerCall.Interval = freshTime * 1000;
            timerCall.Start();
            timerCall_Tick(new object(), new EventArgs());

            if (rev > 0)
            {
                timerChange.Enabled = true;
                timerChange.Interval = freshTime * 1000 * 2;
                timerChange.Start();
                timerChange_Tick(new object(), new EventArgs());

                //timerShow.Enabled = true;
                //timerShow.Interval = freshTime;
                //timerShow.Start();
                //timerShow_Tick(new object(), new EventArgs());
            }
            else
            {
                timerChange.Enabled = false;
            }

            #region 初始化显示队列信息

            SetQueue();

            #endregion

            return 1;
        }

        private int SetQueue()
        {
            FS.HISFC.BizLogic.Nurse.Queue queueMgr = new FS.HISFC.BizLogic.Nurse.Queue();

            DateTime dtNow = queueMgr.GetDateTimeFromSysDateTime();
            string noonID = Function.GetNoonID(dtNow);//午别
            ArrayList alQueue = queueMgr.Query(oper.Nurse.ID, dtNow.Date, noonID);
            if (alQueue == null)
            {
                MessageBox.Show("查询门诊护士站对应的队列信息出错！\r\n" + queueMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            else if (alQueue.Count == 0)
            {
                return 0;
            }

            fpSpread1_Sheet1.RowCount = 0;


            //根据行数设置行高
            //float fpHeaderHeight = 0;
            float height = pnMain.Height - 30;

            //fpHeaderHeight += fpSpread1_Sheet1.ColumnHeader.Rows.Get(0).Height;

            rowHeight = FS.FrameWork.Function.NConvert.ToInt32(height / (this.showRowCount + 1));
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].Height = rowHeight;
            fpSpread1_Sheet1.ColumnHeader.Rows[0].Font = new Font(fpSpread1.Font.FontFamily, patientFontSize);

            //这里要增加一个排序还是SQL排序，按照诊室排序

            foreach (FS.HISFC.Models.Nurse.Queue queue in alQueue)
            {
                int rowIndex = fpSpread1_Sheet1.RowCount;

                fpSpread1_Sheet1.AddRows(rowIndex, 1);

                //队列名称
                fpSpread1_Sheet1.Cells[rowIndex, (Int32)EnumCol.D队列名称].Text = queue.SRoom.Name + (queue.ExpertFlag == "1" ? "(" + SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(queue.Doctor.ID) + ")" : "");
                fpSpread1_Sheet1.Cells[rowIndex, (Int32)EnumCol.D队列名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //在诊患者
                fpSpread1_Sheet1.Cells[rowIndex, (Int32)EnumCol.Z在诊患者].Text = "";
                //待诊1
                fpSpread1_Sheet1.Cells[rowIndex, (Int32)EnumCol.D待诊1].Text = "";
                //待诊2
                fpSpread1_Sheet1.Cells[rowIndex, (Int32)EnumCol.D待诊2].Text = "";

                fpSpread1_Sheet1.Rows[rowIndex].Tag = queue;

                fpSpread1_Sheet1.Rows[rowIndex].BackColor = Color.White;
                fpSpread1_Sheet1.Rows[rowIndex].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                fpSpread1_Sheet1.Rows[rowIndex].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

                fpSpread1_Sheet1.Rows[rowIndex].Height = rowHeight;
            }

            //设置列宽
            float width = 0;
            width = pnMain.Width - 30;
            float colWidth = FS.FrameWork.Function.NConvert.ToInt32(width / this.fpSpread1_Sheet1.ColumnCount);
            for (int col = 0; col < fpSpread1_Sheet1.ColumnCount; col++)
            {
                if (col == 0)
                {
                    fpSpread1_Sheet1.Columns[col].Width = colWidth + 90;
                }
                else
                {
                    fpSpread1_Sheet1.Columns[col].Width = colWidth - 30;
                }
            }

            if (System.IO.File.Exists(showXML))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(fpSpread1_Sheet1, showXML);
            }

            return 1;
        }

        int count = 0;

        /// <summary>
        /// 刷新显示队列信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerShow_Tick(object sender, EventArgs e)
        {
            //先清空
            for (int row = 0; row < fpSpread1_Sheet1.RowCount; row++)
            {
                fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.Z在诊患者].Text = "";
                fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.D待诊1].Text = "";
                fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.D待诊2].Text = "";
            }

            count += 1;
            for (int row = 0; row < fpSpread1_Sheet1.RowCount; row++)
            {
                FS.HISFC.Models.Nurse.Queue queue = fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Nurse.Queue;
                if (queue != null)
                {
                    ArrayList alIn = assignMgr.Query(oper.Dept.ID, queue.QueueDate.Date, queue.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.In);
                    if (alIn == null)
                    {
                        MessageBox.Show("查询进诊患者出错！\r\n" + assignMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        this.timerShow.Enabled = false;
                    }

                    ArrayList alTriage = assignMgr.Query(oper.Dept.ID, queue.QueueDate.Date, queue.ID, FS.HISFC.Models.Nurse.EnuTriageStatus.Triage);
                    if (alTriage == null)
                    {
                        MessageBox.Show("查询候诊患者出错！\r\n" + assignMgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        this.timerShow.Enabled = false;
                    }

                    alIn.AddRange(alTriage);
                    for (int index = 0; index < alIn.Count; index++)
                    //for (int index = 0; index < fpSpread1_Sheet1.RowCount; index++)
                    {
                        FS.HISFC.Models.Nurse.Assign assign = alIn[index] as FS.HISFC.Models.Nurse.Assign;

                        if (assign != null)
                        {
                            Color foreColor = Color.Black;
                            if (assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.In)
                            {
                                foreColor = Color.Blue;
                            }

                            string showName = "";

                            //设置名称截取
                            string name = assign.Register.Name;

                            if(isDiaplayName)
                            {
                                if (assign.Register.Name.Length > 0)
                                {
                                    string newName = assign.Register.Name.Substring(0, assign.Register.Name.Length - 1);
                                    name = newName.PadRight(name.Length, '*');
                                }
                            }

                            showName = name;

                            if (index == 0)
                            {
                                //在诊患者
                                //fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.Z在诊患者].Text = "在诊" + count.ToString();
                                fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.Z在诊患者].Text = showName + "(" + assign.SeeNO.ToString() + ")";
                                fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.Z在诊患者].ForeColor = foreColor;
                            }
                            else if (index == 1)
                            {
                                //待诊1
                                //fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.D待诊1].Text = "1待诊" + count.ToString();
                                fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.D待诊1].Text = showName + "(" + assign.SeeNO.ToString() + ")";
                                fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.D待诊1].ForeColor = foreColor;
                            }
                            else if (index == 2)
                            {
                                //待诊2
                                //fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.D待诊2].Text = "2待诊" + count.ToString();
                                fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.D待诊2].Text = showName + "(" + assign.SeeNO.ToString() + ")";
                                fpSpread1_Sheet1.Cells[row, (Int32)EnumCol.D待诊2].ForeColor = foreColor;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 换页显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timerChange_Tick(object sender, EventArgs e)
        {
            if (currentPage == 0)
            {
                #region 初始化显示队列信息

                SetQueue();

                #endregion
                this.timerShow_Tick(sender, e);
            }

            for (int rowIndex = 0; rowIndex < fpSpread1_Sheet1.RowCount; rowIndex++)
            {
                if (rowIndex >= currentPage * showRowCount
                    && rowIndex < (currentPage + 1) * showRowCount)
                {
                    fpSpread1_Sheet1.Rows[rowIndex].Visible = true;
                }
                else
                {
                    fpSpread1_Sheet1.Rows[rowIndex].Visible = false;
                }
                //fpSpread1_Sheet1.Rows[rowIndex].Height = rowHeight;
            }

            currentPage += 1;

            if (currentPage >= Math.Ceiling((decimal)fpSpread1_Sheet1.RowCount / showRowCount))
            {
                currentPage = 0;
            }
        }


        /// <summary>
        /// 叫号定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCall_Tick(object sender, EventArgs e)
        {
            #region 叫号

            DateTime dtNow = this.socAsignMgr.GetDateTimeFromSysDateTime();
            string noonID = Function.GetNoonID(dtNow);//午别

            //根据护士站和午别查找所有的叫号申请信息
            FS.HISFC.Models.Base.Noon noon = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().GetNoon(noonID);
            List<FS.SOC.HISFC.CallQueue.Models.NurseAssign> list = nurseAssignMgr.Query(oper.Dept.ID, noon);

            if (list != null && list.Count > 0)
            {
                lblCallInfo.Text = "";

                foreach (FS.SOC.HISFC.CallQueue.Models.NurseAssign nurseAssign in list)
                {
                    string strPatient = "请 " + nurseAssign.PatientSeeNO + " 号 " + nurseAssign.PatientName + " 到 " + nurseAssign.Room.Name + " 就 诊";
                    if (!lblCallInfo.Text.Contains(strPatient))
                    {
                        lblCallInfo.Text += strPatient;
                    }

                    //开始逐一叫号
                    //Speech(nurseAssign);
                    this.callSpeak.Speech(nurseAssign); 

                    //删除叫号信息
                    if (!FS.FrameWork.WinForms.Classes.Function.IsManager())
                    {
                        this.nurseAssignMgr.Delete(nurseAssign.ID);
                    }
                }
            }
            else
            {
                lblCallInfo.Text = "挂号后请您在座椅上静候显示或声音叫号";
            }

            #endregion

            #region 刷新队列

            this.timerShow_Tick(sender, e);

            #endregion
        }

        /// <summary>
        /// 叫号发声
        /// </summary>
        /// <param name="assignObj"></param>
        //public void Speech(FS.SOC.HISFC.CallQueue.Models.NurseAssign assignObj)
        //{
        //    //string converSeeNo = "";
        //    ////填充空格
        //    //if (assignObj..SeeNO.ToString().Length > 0)
        //    //{
        //    //    for (int i = 0; i < assignObj.SeeNO.ToString().Length; i++)
        //    //    {
        //    //        converSeeNo += assignObj.SeeNO.ToString().Substring(i, 1) + " ";
        //    //    }
        //    //}

        //    //string speakText = "   请 " + converSeeNo + "  号  到    " + assignObj.Queue.SRoom.Name + "  就诊";
        //    //this.interMgr.Speak(speakText);


        //    string callName = "";
        //    for (int len = 0; len < assignObj.PatientName.Length; len++)
        //    {
        //        callName += assignObj.PatientName.Substring(len, 1) + "  ";
        //    }
        //    //callName = assignObj.PatientName;

        //    string speakText = "请" + assignObj.PatientSeeNO + "号" + callName + "到" + assignObj.Room.Name + "就诊！";

        //    try
        //    {
        //        this.callSpeak.Speech(speakText);
        //        //int speckCount = 3;
        //        //for (int i = 0; i < speckCount; i++)
        //        //{
        //        //    this.callSpeak.Speech(speakText);
        //        //}
        //    }
        //    catch
        //    {
        //        FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "警告", "叫号失败，可能是语音设备有问题！\r\n请确认是否已正确安装语音播放设备！", ToolTipIcon.Warning);
        //    }
        //}

        protected override void OnLoad(EventArgs e)
        {
            //this.FindForm().WindowState = FormWindowState.Maximized;
            this.LoadMenuSet();

            //if (FS.FrameWork.WinForms.Classes.Function.IsManager())
            //{
            //    this.DesktopBounds = Screen.AllScreens[0].Bounds;
            //}

            Init();

            base.OnLoad(e);
        }

        private void frmDisplay_DoubleClick(object sender, EventArgs e)
        {
            this.FindForm().WindowState = FormWindowState.Normal;
            this.FindForm().Close();
        }


        #endregion


        #region 函数

        /// <summary>
        /// 获取大屏幕参数
        /// </summary>
        private void LoadMenuSet()
        {
            //try
            //{

            //    #region 叫号设置

            //    if (!System.IO.File.Exists(Application.StartupPath + "/Setting/ExtendQueue.xml"))
            //    {
            //        Function.CreateXML(Application.StartupPath + "/Setting/ExtendQueue.xml", "60", this.assignMgr.Operator.ID);
            //    }
            //    //是否延长队列时间 叫号的本地设置
            //    XmlDocument doc = new XmlDocument();
            //    doc.Load(Application.StartupPath + "/Setting/ExtendQueue.xml");
            //    XmlNode node = doc.SelectSingleNode("//延长队列");
            //    if (node != null)
            //    {
            //        this.extendTime = double.Parse(node.Attributes["ExtendTime"].Value);
            //    }
            //    node = doc.SelectSingleNode("//叫号刷新时间");
            //    if (node != null)
            //    {
            //        this.callRefreshTime = int.Parse(node.Attributes["CallRefreshTime"].Value);
            //    }

            //    #endregion

            //    #region 显示设置

            //    string pathName = Application.StartupPath + "/Setting/NurseSetting.xml";
            //    if (!System.IO.File.Exists(pathName)) return;
            //    doc.Load(pathName);
            //    System.Xml.XmlNode xn = doc.SelectSingleNode("//是否隐藏姓名");
            //    if (xn != null)
            //    {
            //        isDiaplayName = FS.FrameWork.Function.NConvert.ToBoolean(xn.Attributes[0].Value);
            //    }

            //    System.Xml.XmlNode xnDoct = doc.SelectSingleNode("//医生姓名字体");
            //    if (xnDoct != null)
            //    {
            //        try
            //        {
            //            doctFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnDoct.Attributes[0].Value);
            //        }
            //        catch (Exception)
            //        {
            //            doctFontSize = 30;
            //        }

            //    }
            //    System.Xml.XmlNode xnPatient = doc.SelectSingleNode("//患者姓名字体");
            //    if (xnPatient != null)
            //    {
            //        try
            //        {
            //            patientFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnPatient.Attributes[0].Value);
            //        }
            //        catch (Exception)
            //        {
            //            patientFontSize = 30;
            //        }

            //    }
            //    System.Xml.XmlNode xnCount = doc.SelectSingleNode("//候诊人数字体");
            //    if (xnCount != null)
            //    {
            //        try
            //        {
            //            countFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnCount.Attributes[0].Value);
            //        }
            //        catch (Exception)
            //        {
            //            countFontSize = 30;
            //        }
            //    }

            //    System.Xml.XmlNode xnFlash = doc.SelectSingleNode("//广告设置");
            //    if (xnFlash != null)
            //    {
            //        try
            //        {
            //            if (xnFlash.Attributes["Height"] != null)
            //            {
            //                flashHeight = int.Parse(xnFlash.Attributes["Height"].Value);
            //            }

            //            if (xnFlash.Attributes["Title"] != null)
            //            {

            //                flashTitle = xnFlash.Attributes["Title"].Value;
            //            }

            //            if (xnFlash.Attributes["Font"] != null)
            //            {
            //                flashFontSize = (float)FS.FrameWork.Function.NConvert.ToDecimal(xnFlash.Attributes["Font"].Value);
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            MessageBox.Show(e.Message);

            //        }
            //    }

            //    System.Xml.XmlNode xnShow = doc.SelectSingleNode("//候诊显示人数");
            //    if (xnShow != null)
            //    {
            //        try
            //        {
            //            if (xnShow.Attributes["Count"] != null)
            //            {
            //                showCount = int.Parse(xnShow.Attributes["Count"].Value);
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            MessageBox.Show(e.Message);

            //        }
            //    }
            //    #endregion
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "提示");
            //}
        }

        public class AssignInDateCompare : IComparer
        {
            #region IComparer 成员

            public int Compare(object x, object y)
            {
                try
                {
                    FS.HISFC.Models.Nurse.Assign assignX = x as FS.HISFC.Models.Nurse.Assign;
                    FS.HISFC.Models.Nurse.Assign assignY = y as FS.HISFC.Models.Nurse.Assign;


                    if (assignX == null)
                    {
                        return (assignY != null) ? 1 : 0;
                    }
                    else if (assignY == null)
                    {
                        return -1;
                    }


                    if (assignX.InTime < assignY.InTime)
                    {
                        return 1;
                    }
                    else if (assignX.InTime > assignY.InTime)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch
                {
                    return 0;
                }

                return 0;
            }

            #endregion
        }

        /// <summary>
        /// 列
        /// </summary>
        enum EnumCol
        {
            D队列名称,
            Z在诊患者,
            D待诊1,
            D待诊2,

            ColCout
        }

        #endregion

        #region IAssignDisplay 成员

        //public void Close()
        //{
        //    this.Close();
        //}

        //public void Show()
        //{
        //    this.Show();
        //}

        #endregion

        #region IDisposable 成员

        //public void Dispose()
        //{
        //    if (this != null && !this.IsDisposed)
        //    {
        //        this.Close();
        //        this.Dispose();
        //    }
        //}

        #endregion

        private void lblCallInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                this.Close();
            }
        }

        #region IAssignDisplay 成员

        void FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay.Close()
        {
            if (this != null && !this.IsDisposed)
            {
                this.Hide();
            }
        }

        void FS.SOC.HISFC.Assign.Interface.Components.IAssignDisplay.Show()
        {
            this.Show();
            if (Screen.AllScreens.Length > 1)
            {
                if (Screen.AllScreens[0].Primary)
                {
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
                else
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            else
            {
                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            Init();
        }

        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            if (this != null && !this.IsDisposed)
            {
                this.Close();
                this.Dispose();
            }
        }

        #endregion
    }
}
