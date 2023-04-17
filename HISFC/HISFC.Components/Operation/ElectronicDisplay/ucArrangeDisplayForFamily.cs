using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Xml;

namespace FS.HISFC.Components.Operation.ElectronicDisplay
{
    /// <summary>
    /// 此处展示电子显示屏的查看麻醉安排和手术安排信息
    /// </summary>
    public partial class ucArrangeDisplayForFamily : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucArrangeDisplayForFamily()
        {
            InitializeComponent();
            //MenuItem itemAll = new MenuItem("全部", new EventHandler(itemAll_Click));
            //MenuItem itemAnae = new MenuItem("麻醉安排", new EventHandler(itemAnae_Click));
            //MenuItem itemOperation = new MenuItem("手术安排", new EventHandler(itemOperation_Click));
            //MenuItem itemReady = new MenuItem("未安排", new EventHandler(itemReady_Click));
            //MenuItem[] itemRange = { itemReady, itemAnae, itemOperation, itemAll };
            //menu.MenuItems.AddRange(itemRange);
        }


        #region 属性


        private string sqlstr = @"SELECT t.NAME,--姓名 
                        CASE t.SEX_CODE WHEN 'M' THEN '男'
                        ELSE '女' END ,--性别
                        (SELECT s.DEPT_NAME FROM COM_DEPARTMENT s WHERE s.DEPT_CODE= t.DEPT_CODE),--住院科室  
                        substr (t.BED_NO,5) ,--床号 
                        CASE t.APPLY_STATE WHEN '' THEN '等候中' 
                        ELSE t.APPLY_STATE END, --手术状态
                        t.APPLY_TIPS  --给家属的提示信息
                        FROM MET_OPS_RECORD t  WHERE 
                        t.OPER_DATE BETWEEN TIMESTAMP('{0}') AND TIMESTAMP('{1}')";


        [Category("控件设置"), Description("该页面的SQLSTR")]
        public string SqlStrSet
        {
            set
            {
                this.sqlstr = value;
            }
            get
            {
                return this.sqlstr;
            }
        }

        #endregion 


        #region 域变量

        private FS.FrameWork.WinForms.Controls.NeuContexMenu menu = new FS.FrameWork.WinForms.Controls.NeuContexMenu();
        private HISFC.BizProcess.Integrate.Manager mgr = new HISFC.BizProcess.Integrate.Manager();
        frmArrangeDisplayInWeb dw = new frmArrangeDisplayInWeb();
        private int timerInterval = 20000;
        private int times = 0;
        public int TimerInterval
        {
            get { return timerInterval; }
            set { timerInterval = value; }
        }

        int autoSaveTime = 1;
        /// <summary>
        /// 当前登录科室实体
        /// </summary>
        protected FS.HISFC.Models.Base.Department DeptObj
        {
            get
            {  
                string deptID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                return mgr.GetDepartment(deptID);
            }
        }

        private static string FileName = Application.StartupPath + @".\profile\ArrangeQueryForFamily.xml";
        ArrangeQueryForFamily arrQuery;
        string TipList = "";
        #endregion

       



        #region 事件

        private void ucArrangeDisplayForFamily_Load(object sender, EventArgs e)
        {
            //this.neuDTEnd.Enabled = false;
            this.timer1.Enabled = false;
            this.timer1.Interval = 20000;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在安排列表,请稍候......");
            Application.DoEvents();
            //FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.FamliyNotesSpread_Sheet1, FileName);

            

            this.neuLabel4.Text = "";
            DateTime dt = Environment.OperationManager.GetDateTimeFromSysDateTime();
            arrQuery = new ArrangeQueryForFamily();
            this.SetData(dt, dt);//当天的安排
            InitSet();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
        /// <summary>
        /// 保存配置并启动屏幕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            #region 保存配置文件

            if (SaveSet(GetInfo()) == -1)
            {
                MessageBox.Show("保存设置失败！");
            }

            #endregion

            #region  重新生成html

            WriteHtmlFiles();

            #endregion
            this.timer1.Enabled = true;
            timer1.Interval = FS.FrameWork.Function.NConvert.ToInt32(this.tbTimerValue.Text) * 60000;
            MessageBox.Show("配置已保存！(带*号为重启该界面后生效)已自动开启定时刷新功能！刷新间隔时间为" + ((this.timer1.Interval / 60000 <= 1) ? (this.timer1.Interval / 1000 + "秒！") : (Math.Round((decimal)this.timer1.Interval / 60000, 2).ToString() + "分！")));

            try
            {
                dw.LoadHtmlTemp("ucArrangeDisplayForFamily");
            }
            catch (Exception ee)
            {
                dw = new frmArrangeDisplayInWeb();
                InitSet();
                dw.LoadHtmlTemp("ucArrangeDisplayForFamily");
            }
            finally
            {
                if (!dw.Visible)
                {
                    dw.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(this.tbXPoint.Text.ToString()), FS.FrameWork.Function.NConvert.ToInt32(this.tbYPoint.Text.ToString()));
                    dw.Show();
                }
            }

            //dw.LoadHtmlTemp("ucArrangeDisplayForFamily");

            //if (!dw.Visible)
            //{
            //    dw.Show();
            //}

            return 1;
        }
        /// <summary>
        /// 重新生成html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReGenerHtml(object sender, EventArgs e)
        {
            #region 重新查询当天数据

            DateTime dt2 = DateTime.Now;
            DateTime dt1 = Convert.ToDateTime(dt2.ToString("yyyy-MM-dd 00:00:00"));
            dt2 = Convert.ToDateTime(dt2.ToString("yyyy-MM-dd 23:59:59"));
            SetData(dt1, dt2);

            #endregion

            #region  重新生成html

            WriteHtmlFiles();

            #endregion

            times++;
            dw.Text = "电子屏幕显示(定时刷新，已刷新" + this.times + "次)";

            try
            {
                dw.LoadHtmlTemp("ucArrangeDisplayForFamily");
            }
            catch (Exception ee)
            {
                dw = new frmArrangeDisplayInWeb();
                InitSet();
                dw.LoadHtmlTemp("ucArrangeDisplayForFamily");
            }
            finally
            {
                if (!dw.Visible)
                {
                    dw.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(this.tbXPoint.Text.ToString()), FS.FrameWork.Function.NConvert.ToInt32(this.tbYPoint.Text.ToString()));
                    dw.Show();
                }
            }

            //if (!dw.Visible)
            //{
            //    dw.Show();
            //}

        }
        /// <summary>
        /// 显示当天数据
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        private void SetData(DateTime dtBegin, DateTime dtEnd)
        {
            //this.FamliyNotesSpread_Sheet1.
            DataTable dt = null;
            FS.HISFC.Models.Base.DepartmentTypeEnumService deptSvr = new FS.HISFC.Models.Base.DepartmentTypeEnumService();
            //if (DeptObj.DeptType.Name == deptSvr.GetName(FS.HISFC.Models.Base.EnumDepartmentType.OP) || DeptObj.DeptType.Name == deptSvr.GetName(FS.HISFC.Models.Base.EnumDepartmentType.D))
            //{
            dt = arrQuery.SetDataTableData(dtBegin, dtEnd, this.SqlStrSet);
            //}
            //else
            //{
            //    dt = arrQuery.SetDataTableData(DeptObj, dtBegin, dtEnd);
            //}
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDTime = System.Type.GetType("System.DateTime");
            System.Type dtBool = System.Type.GetType("System.Boolean");
            System.Type dtInt = System.Type.GetType("System.Int32");

            
            this.FamliyNotesSpread_Sheet1.DataSource = dt;

            #region 设置列信息

            this.FamliyNotesSpread_Sheet1.Columns[0].Label = "病人姓名";
            this.FamliyNotesSpread_Sheet1.Columns[0].Width = 80;
            this.FamliyNotesSpread_Sheet1.Columns[1].Label = "性别";
            this.FamliyNotesSpread_Sheet1.Columns[1].Width = 50;
            this.FamliyNotesSpread_Sheet1.Columns[2].Label = "科室";
            this.FamliyNotesSpread_Sheet1.Columns[2].Width = 50;
            this.FamliyNotesSpread_Sheet1.Columns[3].Label = "床号";
            this.FamliyNotesSpread_Sheet1.Columns[3].Width = 150;
            //this.FamliyNotesSpread_Sheet1.Columns[3].Visible = false;
            this.FamliyNotesSpread_Sheet1.Columns[4].Label = "手术状态";
            this.FamliyNotesSpread_Sheet1.Columns[4].Width = 150;
            //this.FamliyNotesSpread_Sheet1.Columns[4].Visible = false;
            //this.FamliyNotesSpread_Sheet1.Columns[5].Label = "xx";//保留
            //this.FamliyNotesSpread_Sheet1.Columns[5].Width = 150;
            //this.FamliyNotesSpread_Sheet1.Columns[5].Visible = false;
            //this.FamliyNotesSpread_Sheet1.Columns[6].Label = "xx";
            //this.FamliyNotesSpread_Sheet1.Columns[6].Width = 150;
            //this.FamliyNotesSpread_Sheet1.Columns[6].Visible = false;
            //this.FamliyNotesSpread_Sheet1.Columns[7].Label = "xx";
            //this.FamliyNotesSpread_Sheet1.Columns[7].Width = 150;
            //this.FamliyNotesSpread_Sheet1.Columns[7].Visible = false;

            #endregion


            //for (int i = 0; i < this.FamliyNotesSpread_Sheet1.Columns.Count; i++)
            //{
            //    #region 补充标志说明

            //    if (i == 5)
            //    {
            //        for (int j = 0; j < this.FamliyNotesSpread_Sheet1.RowCount; j++)
            //        {
            //            if (this.FamliyNotesSpread_Sheet1.Cells[j, i].Text == "-")
            //            {
            //                DateTime now = Environment.OperationManager.GetDateTimeFromSysDateTime();
            //                if (now > FS.FrameWork.Function.NConvert.ToDateTime(this.FamliyNotesSpread_Sheet1.Cells[j, 4].Text.ToString()))
            //                {
            //                    this.FamliyNotesSpread_Sheet1.Cells[j, i].Text = "出手术室";
            //                }
            //                else
            //                {
            //                    if (this.FamliyNotesSpread_Sheet1.Cells[j, 3].Text.ToString() != "1/1/1")
            //                    {
            //                        if (now > FS.FrameWork.Function.NConvert.ToDateTime(this.FamliyNotesSpread_Sheet1.Cells[j, 3].Text.ToString()))
            //                        {
            //                            this.FamliyNotesSpread_Sheet1.Cells[j, i].Text = "入手术室";
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    #endregion

            //}
            //FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.FamliyNotesSpread_Sheet1, FileName);



            this.neuLabel4.Text = "打印日期: 从" + dtBegin.Date.ToString("yyyy-MM-dd hh:mm:ss") + "到" + dtEnd.Date.ToString("yyyy-MM-dd hh:mm:ss");
        }
        #endregion
        private string[] GetInfo()
        {
            return new string[] {this.tbdwWidth.Text,this.tbdwHeight.Text,this.tbXPoint.Text,this.tbYPoint.Text,
            (FS.FrameWork.Function.NConvert.ToInt32(this.checkBox1.Checked)).ToString()+"|"
                +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox2.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox3.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox4.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox5.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox6.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox7.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox8.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox9.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox10.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox11.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox12.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox13.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox14.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox15.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox16.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox17.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox18.Checked)).ToString()+"|"
                 +(FS.FrameWork.Function.NConvert.ToInt32(this.checkBox19.Checked)).ToString(),this.tbSpeed.Text,this.tbTimerValue.Text
            };
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private int SaveSet(string[] SetInfo)
        {
            try
            {
                string path = Application.StartupPath;
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                XmlDocument doc = new XmlDocument();

                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "GB2312", ""));
                XmlElement root = doc.CreateElement("Seting");
                root.SetAttribute("Version", "1.0");
                doc.AppendChild(root);
                XmlElement e = doc.CreateElement("columns");
                root.AppendChild(e);
                XmlCDataSection text = doc.CreateCDataSection("");
                e.AppendChild(text);
                e.SetAttribute("Width", SetInfo[0]);
                e.SetAttribute("Height", SetInfo[1]);
                e.SetAttribute("Bounds_w", SetInfo[2]);
                e.SetAttribute("Bounds_h", SetInfo[3]);
                e.SetAttribute("CanSeeCols", SetInfo[4]);
                e.SetAttribute("Speed", SetInfo[5]);
                e.SetAttribute("Timer", SetInfo[6]);
                StreamWriter sr = new StreamWriter(path + "\\ArrangeDisplay\\DisplayForFamilySet.xml", false, System.Text.Encoding.Default);
                string cleandown = doc.OuterXml;
                sr.Write(cleandown);

                sr.Close();

            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 初始化显示配置
        /// </summary>
        private void InitSet()
        {
            if (File.Exists(Application.StartupPath + "\\ArrangeDisplay\\DisplayForFamilySet.xml"))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\ArrangeDisplay\\DisplayForFamilySet.xml", System.Text.Encoding.Default);

                    string cleandown = sr.ReadToEnd();
                    doc.LoadXml(cleandown);

                    sr.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("无法读取列配置文件！" + ex.Message);
                    return;
                }
                XmlNodeList nodes = doc.SelectNodes("//columns");
                this.tbdwWidth.Text = nodes[0].Attributes["Width"].Value.ToString();
                this.tbdwHeight.Text = nodes[0].Attributes["Height"].Value.ToString();
                dw.Width = FS.FrameWork.Function.NConvert.ToInt32(this.tbdwWidth.Text);
                dw.Height = FS.FrameWork.Function.NConvert.ToInt32(this.tbdwHeight.Text);
                this.tbXPoint.Text = nodes[0].Attributes["Bounds_w"].Value.ToString();
                this.tbYPoint.Text = nodes[0].Attributes["Bounds_h"].Value.ToString();
                int b_w = FS.FrameWork.Function.NConvert.ToInt32(this.tbXPoint.Text);
                int b_h = FS.FrameWork.Function.NConvert.ToInt32(this.tbYPoint.Text);
                string[] seeCols = nodes[0].Attributes["CanSeeCols"].Value.ToString().Split('|');

                #region 设置可见列

                //if (seeCols.Length == 19)
                //{

                //    this.checkBox1.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[0].ToString());

                //    this.checkBox2.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[1].ToString());

                //    this.checkBox3.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[2].ToString());

                //    this.checkBox4.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[3].ToString());

                //    this.checkBox5.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[4].ToString());

                //    this.checkBox6.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[5].ToString());

                //    this.checkBox7.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[6].ToString());

                //    this.checkBox8.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[7].ToString());

                //    this.checkBox9.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[8].ToString());

                //    this.checkBox10.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[9].ToString());

                //    this.checkBox11.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[10].ToString());

                //    this.checkBox12.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[11].ToString());

                //    this.checkBox13.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[12].ToString());

                //    this.checkBox14.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[13].ToString());

                //    this.checkBox15.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[14].ToString());

                //    this.checkBox16.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[15].ToString());

                //    this.checkBox17.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[16].ToString());

                //    this.checkBox18.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[17].ToString());

                //    this.checkBox19.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[18].ToString());

                //}
                #endregion

                this.tbSpeed.Text = nodes[0].Attributes["Speed"].Value.ToString();
                this.tbTimerValue.Text = nodes[0].Attributes["Timer"].Value.ToString();
                dw.SetBounds(b_w, b_h, dw.Width, dw.Height);
            }
        }


        private void WriteHtmlFiles()
        {
            InitTipGroups();

            StreamWriter SW;
            string path = Application.StartupPath;
            path += "\\ArrangeDisplay\\ucArrangeDisplayForFamily.html";

            SW = System.IO.File.CreateText(path);
            SW.WriteLine("<html>");
            SW.WriteLine("<head>");
            SW.WriteLine("<title>电子滚屏信息</title>");
            SW.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            SW.WriteLine("<link type=\"text/css\" rel=\"stylesheet\" href=\"ucArrangeDisplayForFamily.css\" />");
            SW.WriteLine("</head>");
            SW.WriteLine("<body oncontextmenu=\"window.event.returnValue=false\" >");
            SW.WriteLine(ConvertToHtml());
            SW.WriteLine("</body></html>");

            SW.Close();
        }

        /// <summary>
        /// 初始化提示集合
        /// </summary>
        private void InitTipGroups()
        {
            TipList = "";
            for (int i = 0; i < FamliyNotesSpread_Sheet1.Rows.Count; i++)
            {
                if (FamliyNotesSpread_Sheet1.Cells[i, 5].Text != "")
                {
                    TipList += string.Format("{0}科室{1}床{2}家属{3}" ,FamliyNotesSpread_Sheet1.Cells[i, 2].Text,
                        FamliyNotesSpread_Sheet1.Cells[i, 3].Text,
                        FamliyNotesSpread_Sheet1.Cells[i, 0].Text,
                        FamliyNotesSpread_Sheet1.Cells[i, 5].Text);
                    TipList += "&nbsp&nbsp&nbsp&nbsp";
                }
            }
        }

        private string GetTableHead()
        {
            #region 头部组装 疯狂复制ing~~~

            StringBuilder StrHead = new StringBuilder();

            for (int i = 0; i < this.FamliyNotesSpread_Sheet1.Columns.Count - 1; i++)
            {
                if (this.FamliyNotesSpread_Sheet1.Columns[i].Visible == true)
                {
                    StrHead.Append("<td class='td" + (i + 1) + "'>" + this.FamliyNotesSpread_Sheet1.Columns[i].Label + "</td>");
                    StrHead.Append("\r\n");
                }
            }
            return StrHead.ToString();

            #endregion

        }

        /// <summary>
        /// FarPint转HTML
        /// </summary>
        private string ConvertToHtml()
        {
            #region HTML组装

            StringBuilder StrHtml = new StringBuilder();

            StrHtml.Append("<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">");
            StrHtml.Append("\r\n");

            #region 组装头部

            StrHtml.Append("<tr>");
            StrHtml.Append("\r\n");
            StrHtml.Append("<td width=\"100%\">");
            StrHtml.Append("\r\n");
            StrHtml.Append("<table width=\"" + dw.Width + "\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" class=\"headTable\">");
            StrHtml.Append("\r\n");
            StrHtml.Append("<tr class=\"titletd\">");
            //"<th>" + this.checkBox1.Text + "</td>"
            StrHtml.Append(GetTableHead());
            StrHtml.Append("</tr>");
            StrHtml.Append("\r\n");
            StrHtml.Append("</table>");
            StrHtml.Append("\r\n");
            StrHtml.Append("</td>");
            StrHtml.Append("</tr><tr><td width=\"100%\">");
            StrHtml.Append("\r\n");
            StrHtml.Append("<div id=\"A1\">");
            StrHtml.Append("\r\n");
            StrHtml.Append("<div id=\"A2\">");
            StrHtml.Append("\r\n");
            StrHtml.Append("<table width=\"" + dw.Width + "\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" class=\"headTable\" style=\"margin-top:-1px;\">");
            StrHtml.Append("\r\n");
            StrHtml.Append("<tbody class=\"datatd\">");
            StrHtml.Append("\r\n");

            #endregion

            for (int i = 0; i < FamliyNotesSpread_Sheet1.Rows.Count; i++)
            {
                StrHtml.Append("<tr>");
                for (int j = 0; j < FamliyNotesSpread_Sheet1.Columns.Count - 1; j++)//减一是为了干掉最后一列显示
                {
                    if (FamliyNotesSpread_Sheet1.Columns[j].Visible)
                    {
                        StrHtml.Append("<td class='td" + (j + 1) + "'>" + FamliyNotesSpread_Sheet1.Cells[i, j].Text + "</td>");
                        StrHtml.Append("\r\n");
                    }
                }
                StrHtml.Append("</tr>");
                StrHtml.Append("\r\n");
            }
            StrHtml.Append("</tbody>");
            StrHtml.Append("\r\n");
            StrHtml.Append("</table>");
            StrHtml.Append("\r\n");
            StrHtml.Append("</div></div>");
            StrHtml.Append("\r\n");
            StrHtml.Append("</td></tr>");
            StrHtml.Append("\r\n");
            StrHtml.Append("</table>");
            StrHtml.Append("\r\n");

            if (this.TipList != "")
            {
                StrHtml.Append("<div class=\"d1\" id=\"div1\">");
                StrHtml.Append("<div class=\"scroll\" id=\"scroll\">");
                StrHtml.Append("<div class=\"div2\" id=\"div2\">");
                StrHtml.Append(this.TipList);
                StrHtml.Append("</div>");
                StrHtml.Append("<div id=\"div3\" class=\"div2\"></div>");
                StrHtml.Append("</div>");
                StrHtml.Append("</div>");
            }

            StrHtml.Append(GetJS());

            #endregion

            return StrHtml.ToString();
        }

        private string GetJS()
        {
            StringBuilder StrJS = new StringBuilder();
            string speed = this.tbSpeed.Text;
            if (speed == "0")
            {
                return "";
            }
            else
            {
                StrJS.Append("<SCRIPT language=\"JavaScript\">");
                StrJS.Append("function _InitScroll(_S1,_S2,_W,_H,_T){");
                StrJS.Append("return \"var marqueesHeight\"+_S1+\"=\"+_H+\";var stopscroll\"+_S1+\"=false;var scrollElem\"+_S1+\"=document.getElementById('\"+_S1+\"');with(scrollElem\"+_S1+\"){style.width=\"+_W+\";style.height=marqueesHeight\"+_S1+\";style.overflow='hidden';noWrap=true;}var preTop\"+_S1+\"=0; var currentTop\"+_S1+\"=0; var stoptime\"+_S1+\"=0;var leftElem\"+_S2+\"=document.getElementById('\"+_S2+\"');scrollElem\"+_S1+\".appendChild(leftElem\"+_S2+\".cloneNode(true));setTimeout('init_srolltext\"+_S1+\"()',\"+_T+\");function init_srolltext\"+_S1+\"(){scrollElem\"+_S1+\".scrollTop=0;setInterval('scrollUp\"+_S1+\"()',50);}function scrollUp\"+_S1+\"(){if(stopscroll\"+_S1+\"){return;}currentTop\"+_S1+\"+=" + speed + ";if(currentTop\"+_S1+\"==(marqueesHeight\"+_S1+\"+1)) {stoptime\"+_S1+\"+=" + speed + ";currentTop\"+_S1+\"-=1;if(stoptime\"+_S1+\"==\"+_T/50+\") {currentTop\"+_S1+\"=0;stoptime\"+_S1+\"=0;}}else{preTop\"+_S1+\"=scrollElem\"+_S1+\".scrollTop;scrollElem\"+_S1+\".scrollTop +=" + speed + ";if(preTop\"+_S1+\"==scrollElem\"+_S1+\".scrollTop){scrollElem\"+_S1+\".scrollTop=0;scrollElem\"+_S1+\".scrollTop +=" + speed + ";}}}\";");
                StrJS.Append("}");
                //StrJS.Append("eval(_InitScroll(\"A1\",\"A2\"," + this.tbdwWidth.Text + ",19*" + this.FamliyNotesSpread_Sheet1.Rows.Count + ",1000));");
                StrJS.Append("eval(_InitScroll(\"A1\",\"A2\"," + this.tbdwWidth.Text + ",19*5,1000));");
                StrJS.Append("</SCRIPT>");
            }

            if (this.TipList != "")
            {
                StrJS.Append("<script language=\"javascript\" type=\"text/javascript\">");
                StrJS.Append("var s,s2,s3,s4,timer;");
                StrJS.Append("function init(){");
                StrJS.Append("s=getid(\"div1\");");
                StrJS.Append("s2=getid(\"div2\");");
                StrJS.Append("s3=getid(\"div3\");");
                StrJS.Append("s4=getid(\"scroll\");");
                StrJS.Append("s4.style.width=(s2.offsetWidth*2)+\"px\"; ");
                StrJS.Append("s3.innerHTML=s2.innerHTML;");
                StrJS.Append("timer=setInterval(mar,30)");
                StrJS.Append("}");
                StrJS.Append("function mar(){");
                StrJS.Append("if(s2.offsetWidth<=s.scrollLeft){");
                StrJS.Append("s.scrollLeft-=s2.offsetWidth;");
                StrJS.Append("}else{s.scrollLeft++;}");
                StrJS.Append("}");
                StrJS.Append("function getid(id){");
                StrJS.Append("return document.getElementById(id);");
                StrJS.Append("}");
                StrJS.Append("window.onload=init;");
                StrJS.Append("</script>");
            }
            return StrJS.ToString();
        }

        private void TimerStartFun()
        {
            //查询数据
            SetData(this.neuDTBegin.Value, this.neuDTEnd.Value);
            //滚动显示
        }

       


        protected override int OnQuery(object sender, object neuObject)
        {
            SetData(this.neuDTBegin.Value, this.neuDTEnd.Value);
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.IsLandScape = true;
            print.PrintPage(0, 0, this.neuPanel1);
            return base.OnPrint(sender, neuObject);
        }

        private void FamliyNotesSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.FamliyNotesSpread_Sheet1, FileName);
        }

        private void neuDTBegin_ValueChanged(object sender, EventArgs e)
        {
            this.neuDTEnd.Value = this.neuDTBegin.Value.AddDays(1);
        }

        private void itemAll_Click(object o, EventArgs e)
        {
            this.EnumFilterForFamily = FilterForFamily.全部;
        }

        private void itemAnae_Click(object o, EventArgs e)
        {
            this.EnumFilterForFamily = FilterForFamily.麻醉安排;
        }

        private void itemOperation_Click(object o, EventArgs e)
        {
            this.EnumFilterForFamily = FilterForFamily.手术安排;
        }

        private void itemReady_Click(object o, EventArgs e)
        {
            this.EnumFilterForFamily = FilterForFamily.未安排;
        }

        FilterForFamily filter;

        /// <summary>
        /// 过滤实体
        /// </summary>
        FilterForFamily EnumFilterForFamily
        {
            get 
            {
                return this.filter;
            }
            set
            {
                //this.filter = value;
                //switch (this.filter)
                //{
                //    case FilterForFamily.未安排:
                //        for (int i = 0; i < this.FamliyNotesSpread_Sheet1.RowCount; i++)
                //        {
                //            if (this.FamliyNotesSpread_Sheet1.Cells[i, this.FamliyNotesSpread_Sheet1.Columns.Count - 1].Text == "未安排")
                //            {
                //                //this.FamliyNotesSpread_Sheet1.Rows[i].BackColor = Color.Red;
                //                //this.FamliyNotesSpread_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                //                this.FamliyNotesSpread_Sheet1.Rows[i].Visible = true;
                //            }
                //            else
                //            {
                //                this.FamliyNotesSpread_Sheet1.Rows[i].Visible = false;
                //            }

                //        }
                //        break;
                //    case FilterForFamily.麻醉安排:
                //        for (int i = 0; i < this.FamliyNotesSpread_Sheet1.RowCount; i++)
                //        {
                //            if (this.FamliyNotesSpread_Sheet1.Cells[i, this.FamliyNotesSpread_Sheet1.Columns.Count - 1].Text == "麻醉安排")
                //            {
                //                this.FamliyNotesSpread_Sheet1.Rows[i].Visible = true;
                //            }
                //            else
                //            {
                //                this.FamliyNotesSpread_Sheet1.Rows[i].Visible = false;
                //            }

                //        }
                //        break;
                //    case FilterForFamily.手术安排:
                //        for (int i = 0; i < this.FamliyNotesSpread_Sheet1.RowCount; i++)
                //        {
                //            if (this.FamliyNotesSpread_Sheet1.Cells[i, this.FamliyNotesSpread_Sheet1.Columns.Count - 1].Text == "手术安排")
                //            {
                //                this.FamliyNotesSpread_Sheet1.Rows[i].Visible = true;
                //            }
                //            else
                //            {
                //                this.FamliyNotesSpread_Sheet1.Rows[i].Visible = false;
                //            }

                //        }
                //        break;
                //    case FilterForFamily.全部:
                //        for (int i = 0; i < this.FamliyNotesSpread_Sheet1.RowCount; i++)
                //        {
                //            this.FamliyNotesSpread_Sheet1.Rows[i].Visible = true;
                //        }
                //        break;
                //}
            }
        }

        private void FamliyNotesSpread_MouseUp(object sender, MouseEventArgs e)
        {
            if (FamliyNotesSpread_Sheet1.ActiveRow.Index >= 0 && e.Button == MouseButtons.Right)
            {
                menu.Show(this.FamliyNotesSpread, new Point(e.X, e.Y));
            }
        }

 

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = checkBox1.Checked;
            if (this.timer1.Enabled)
            {
                this.TimerInterval = FS.FrameWork.Function.NConvert.ToInt32(this.tbTimerValue.Text) * 600000;
                this.timer1.Interval = this.TimerInterval;
            }
        }

        private void ucArrangeDisplayForFamily_ExitChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.dw.Close();
        }

        public override int Exit(object sender, object neuObject)
        {
            this.timer1.Enabled = false;
            this.dw.Close();

            return 0;
        }
    }

    /// <summary>
    /// 过滤类型
    /// </summary>
    public enum FilterForFamily
    {
        未安排,
        麻醉安排,
        手术安排,
        全部
    }

    /// <summary>
    /// 我所想到的要显示的信息：主刀、一助、二助、麻醉者、麻醉助手、器械护士（就是系统里面的洗手护士）、巡回护士、
    /// </summary>
    public class ArrangeQueryForFamily
    {
        public ArrangeQueryForFamily() { }

        /// <summary>
        /// 手术安排查询
        /// </summary>
        /// <param name="dt">默认查询的日期</param>
        public ArrangeQueryForFamily(DateTime dt)
        {
            this.dtBegin = dt;
            this.dtEnd = dt;
        }

        private DateTime dtBegin = DateTime.Now;
        private DateTime dtEnd = DateTime.Now;


        public DateTime DtEnd
        {
            get { return dtEnd; }
            set { dtEnd = value; }
        }


        /// <summary>
        /// 查询结果所属日期
        /// </summary>
        public DateTime DtBegin
        {
            get { return dtBegin; }
            set { dtBegin = value; }
        }

        /// <summary>
        /// a按照日期查询
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataSet QueryOpsApp(DateTime dtBegin, DateTime dtEnd,string sql)
        {
            dtBegin = Convert.ToDateTime(dtBegin.ToString("yyyy-MM-dd 00:00:00"));
            dtEnd = Convert.ToDateTime(dtEnd.ToString("yyyy-MM-dd 23:59:59"));
            //al = Environment.OperationManager.GetOpsAppList(dtBegin, dtEnd, 0);//此处0暂时无用 留作以后扩展吧

            DataSet ds = new DataSet();
            #region sql

            sql = string.Format(sql, dtBegin, dtEnd);

            #endregion

            Environment.OperationManager.ExecQuery(sql, ref ds);

            return ds;

        }
        public ArrayList QueryOpsApp(FS.FrameWork.Models.NeuObject dept, DateTime dtBegin, DateTime dtEnd)
        {
            ArrayList al = new ArrayList();
            dtBegin = Convert.ToDateTime(dtBegin.ToString("yyyy-MM-dd 00:00:00"));
            dtEnd = Convert.ToDateTime(dtEnd.ToString("yyyy-MM-dd 23:59:59"));
            al = Environment.OperationManager.GetOpsAppList(dept, dtBegin, dtEnd);//此处0暂时无用 留作以后扩展吧
            return al;
        }


        /// <summary>
        /// 根据开始时间和结束时间查询手术安排信息
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataTable SetDataTableData(DateTime begin, DateTime end,string sql)
        {
            DataSet ds = this.QueryOpsApp(begin, end, sql);
            return ds.Tables[0];
            //return this.GetOpsData(al);
        }

        

    }
}
