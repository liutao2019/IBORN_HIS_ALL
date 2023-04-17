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
    /// �˴�չʾ������ʾ���Ĳ鿴�����ź�����������Ϣ
    /// </summary>
    public partial class ucArrangeDisplay : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucArrangeDisplay()
        {
            InitializeComponent();
            MenuItem itemAll = new MenuItem("ȫ��", new EventHandler(itemAll_Click));
            MenuItem itemAnae = new MenuItem("������", new EventHandler(itemAnae_Click));
            MenuItem itemOperation = new MenuItem("��������", new EventHandler(itemOperation_Click));
            MenuItem itemReady = new MenuItem("δ����", new EventHandler(itemReady_Click));
            MenuItem[] itemRange = {itemReady, itemAnae, itemOperation, itemAll};
            menu.MenuItems.AddRange(itemRange);
        }

        #region �����

        private FS.FrameWork.WinForms.Controls.NeuContexMenu menu = new FS.FrameWork.WinForms.Controls.NeuContexMenu();
        private HISFC.BizProcess.Integrate.Manager mgr = new FS.HISFC.BizProcess.Integrate.Manager();
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
        /// ��ǰ��¼����ʵ��
        /// </summary>
        protected FS.HISFC.Models.Base.Department DeptObj
        {
            get
            {
                string deptID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                return mgr.GetDepartment(deptID);
            }
        }

        private static string FileName = Application.StartupPath + @".\profile\ArrangeQuery.xml";
        ArrangeQuery arrQuery;

        #endregion

        #region �¼�

        private void ucArrangeDisplay_Load(object sender, EventArgs e)
        {
            //this.neuDTEnd.Enabled = false;
            this.timer1.Enabled = false;
            this.timer1.Interval = 20000;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڰ����б�,���Ժ�......");
            Application.DoEvents();
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, FileName);
            this.neuLabel4.Text = "";
            DateTime dt = Environment.OperationManager.GetDateTimeFromSysDateTime();
            arrQuery = new ArrangeQuery();
            this.SetData(dt, dt);//����İ���
            InitSet();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
        /// <summary>
        /// �������ò�������Ļ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            #region ���������ļ�

            if (SaveSet(GetInfo()) == -1)
            {
                MessageBox.Show("��������ʧ�ܣ�");
            }

            #endregion

            #region  ��������html

            WriteHtmlFiles();

            #endregion
            this.timer1.Enabled = true;
            timer1.Interval = FS.FrameWork.Function.NConvert.ToInt32(this.tbTimerValue.Text) * 60000;
            MessageBox.Show("�����ѱ��棡(��*��Ϊ�����ý������Ч)���Զ�������ʱˢ�¹��ܣ�ˢ�¼��ʱ��Ϊ" + ((this.timer1.Interval / 60000 <= 1) ? (this.timer1.Interval / 1000 + "�룡") : (Math.Round((decimal)this.timer1.Interval / 60000, 2).ToString() + "�֣�")));

            try
            {
                dw.LoadHtmlTemp("ucArrangeDisplay");
            }
            catch (Exception ee)
            {
                dw = new frmArrangeDisplayInWeb();
                InitSet();
                dw.LoadHtmlTemp("ucArrangeDisplay");
            }
            finally
            {
                if (!dw.Visible)
                {
                    dw.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(this.tbXPoint.Text.ToString()), FS.FrameWork.Function.NConvert.ToInt32(this.tbYPoint.Text.ToString()));
                    dw.Show();
                }
            }
            return 1;
        }
        /// <summary>
        /// ��������html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReGenerHtml(object sender, EventArgs e)
        {
            #region ���²�ѯ��������

            DateTime dt2 = DateTime.Now;
            DateTime dt1 = Convert.ToDateTime(dt2.ToString("yyyy-MM-dd 00:00:00"));
            SetData(dt1, dt2);

            #endregion

            #region  ��������html

            WriteHtmlFiles(); 

            #endregion

            times++;
            dw.Text = "������Ļ��ʾ(��ʱˢ�£���ˢ��" + this.times + "��)";

            try
            {
                dw.LoadHtmlTemp("ucArrangeDisplay");
            }
            catch (Exception ee)
            {
                dw = new frmArrangeDisplayInWeb();
                InitSet();
                dw.LoadHtmlTemp("ucArrangeDisplay");
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
        /// ��ʾ��������
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        private void SetData(DateTime dtBegin, DateTime dtEnd)
        {
            //this.neuSpread1_Sheet1.
            DataTable dt = null;
            FS.HISFC.Models.Base.DepartmentTypeEnumService deptSvr = new FS.HISFC.Models.Base.DepartmentTypeEnumService();
            if (DeptObj.DeptType.Name == deptSvr.GetName(FS.HISFC.Models.Base.EnumDepartmentType.OP) || DeptObj.DeptType.Name == deptSvr.GetName(FS.HISFC.Models.Base.EnumDepartmentType.D))
            {
                dt = arrQuery.SetDataTableData(dtBegin, dtEnd);
            }
            else
            {
                dt = arrQuery.SetDataTableData(DeptObj, dtBegin, dtEnd);
            }
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }

            this.neuSpread1_Sheet1.DataSource = dt;
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                FarPoint.Win.Spread.CellType.TextCellType cellType = new FarPoint.Win.Spread.CellType.TextCellType();
                cellType.WordWrap = true;
                this.neuSpread1_Sheet1.Columns[i].CellType = cellType;

                //#region �����־��ɫ

                //if (this.neuSpread1_Sheet1.Cells[i, 4].Text == "")//��λΪ����Ϊ����
                //{
                //    this.neuSpread1_Sheet1.Rows[i].BackColor = Color.DeepSkyBlue;
                //}

                //#endregion

            }
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, FileName);

            #region �����пɼ���

            neuSpread1_Sheet1.Columns[0].Visible = this.checkBox1.Checked;

            neuSpread1_Sheet1.Columns[1].Visible = this.checkBox2.Checked;

            neuSpread1_Sheet1.Columns[2].Visible = this.checkBox3.Checked;

            neuSpread1_Sheet1.Columns[3].Visible = this.checkBox4.Checked;

            neuSpread1_Sheet1.Columns[4].Visible = this.checkBox5.Checked;

            neuSpread1_Sheet1.Columns[5].Visible = this.checkBox6.Checked;

            neuSpread1_Sheet1.Columns[6].Visible = this.checkBox7.Checked;

            neuSpread1_Sheet1.Columns[7].Visible = this.checkBox8.Checked;

            neuSpread1_Sheet1.Columns[8].Visible = this.checkBox9.Checked;

            neuSpread1_Sheet1.Columns[9].Visible = this.checkBox10.Checked;

            neuSpread1_Sheet1.Columns[10].Visible = this.checkBox11.Checked;

            neuSpread1_Sheet1.Columns[11].Visible = this.checkBox12.Checked;

            neuSpread1_Sheet1.Columns[12].Visible = this.checkBox13.Checked;

            neuSpread1_Sheet1.Columns[13].Visible = this.checkBox14.Checked;

            neuSpread1_Sheet1.Columns[14].Visible = this.checkBox15.Checked;

            neuSpread1_Sheet1.Columns[15].Visible = this.checkBox16.Checked;

            neuSpread1_Sheet1.Columns[16].Visible = this.checkBox17.Checked;

            neuSpread1_Sheet1.Columns[17].Visible = this.checkBox18.Checked;

            neuSpread1_Sheet1.Columns[18].Visible = this.checkBox19.Checked;

            #endregion

            this.neuLabel4.Text = "��ӡ����: ��" + dtBegin.Date.ToString("yyyy-MM-dd hh:mm:ss") + "��" + dtEnd.Date.ToString("yyyy-MM-dd hh:mm:ss");
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
        /// ��������
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
                StreamWriter sr = new StreamWriter(path + "\\ArrangeDisplay\\DisplaySet.xml", false, System.Text.Encoding.Default);
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
        /// ��ʼ����ʾ����
        /// </summary>
        private void InitSet()
        {
            if (File.Exists(Application.StartupPath + "\\ArrangeDisplay\\DisplaySet.xml"))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\ArrangeDisplay\\DisplaySet.xml", System.Text.Encoding.Default);

                    string cleandown = sr.ReadToEnd();
                    doc.LoadXml(cleandown);

                    sr.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("�޷���ȡ�������ļ���" + ex.Message);
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

                #region ���ÿɼ���

                if (seeCols.Length == 19)
                {

                    this.checkBox1.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[0].ToString());

                    this.checkBox2.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[1].ToString());

                    this.checkBox3.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[2].ToString());

                    this.checkBox4.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[3].ToString());

                    this.checkBox5.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[4].ToString());

                    this.checkBox6.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[5].ToString());

                    this.checkBox7.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[6].ToString());

                    this.checkBox8.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[7].ToString());

                    this.checkBox9.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[8].ToString());

                    this.checkBox10.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[9].ToString());

                    this.checkBox11.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[10].ToString());

                    this.checkBox12.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[11].ToString());

                    this.checkBox13.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[12].ToString());

                    this.checkBox14.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[13].ToString());

                    this.checkBox15.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[14].ToString());

                    this.checkBox16.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[15].ToString());

                    this.checkBox17.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[16].ToString());

                    this.checkBox18.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[17].ToString());

                    this.checkBox19.Checked = FS.FrameWork.Function.NConvert.ToBoolean(seeCols[18].ToString());

                }
                #endregion

                this.tbSpeed.Text = nodes[0].Attributes["Speed"].Value.ToString();
                this.tbTimerValue.Text = nodes[0].Attributes["Timer"].Value.ToString();
                dw.SetBounds(b_w, b_h, dw.Width, dw.Height);
            }
        }


        private void WriteHtmlFiles()
        {
            StreamWriter SW;
            string path = Application.StartupPath;
            path += "\\ArrangeDisplay\\ucArrangeDisplay.html";

            SW = System.IO.File.CreateText(path);
            SW.WriteLine("<html>");
            SW.WriteLine("<head>");
            SW.WriteLine("<title>���ӹ�����Ϣ</title>");
            SW.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            SW.WriteLine("<link type=\"text/css\" rel=\"stylesheet\" href=\"ucArrangeDisplay.css\" />");
            SW.WriteLine("</head>");
            SW.WriteLine("<body oncontextmenu=\"window.event.returnValue=false\" >");
            SW.WriteLine(ConvertToHtml());
            SW.WriteLine("</body></html>");

            SW.Close();
        }
        

        private string GetTableHead()
        {
            #region ͷ����װ �����ing~~~

            StringBuilder StrHead = new StringBuilder();
            if (checkBox1.Checked)
            {
                StrHead.Append("<td class='td1'>" + this.checkBox1.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox2.Checked)
            {
                StrHead.Append("<td class='td2'>" + this.checkBox2.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox3.Checked)
            {
                StrHead.Append("<td class='td3'>" + this.checkBox3.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox4.Checked)
            {
                StrHead.Append("<td class='td4'>" + this.checkBox4.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox5.Checked)
            {
                StrHead.Append("<td class='td5'>" + this.checkBox5.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox6.Checked)
            {
                StrHead.Append("<td class='td6'>" + this.checkBox6.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox7.Checked)
            {
                StrHead.Append("<td class='td7'>" + this.checkBox7.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox8.Checked)
            {
                StrHead.Append("<td class='td8'>" + this.checkBox8.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox9.Checked)
            {
                StrHead.Append("<td class='td9'>" + this.checkBox9.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox10.Checked)
            {
                StrHead.Append("<td class='td10'>" + this.checkBox10.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox11.Checked)
            {
                StrHead.Append("<td class='td11'>" + this.checkBox11.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox12.Checked)
            {
                StrHead.Append("<td class='td12'>" + this.checkBox12.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox13.Checked)
            {
                StrHead.Append("<td class='td13'>" + this.checkBox13.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox14.Checked)
            {
                StrHead.Append("<td class='td14'>" + this.checkBox14.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox15.Checked)
            {
                StrHead.Append("<td class='td15'>" + this.checkBox15.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox16.Checked)
            {
                StrHead.Append("<td class='td16'>" + this.checkBox16.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox17.Checked)
            {
                StrHead.Append("<td class='td17'>" + this.checkBox17.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox18.Checked)
            {
                StrHead.Append("<td class='td18'>" + this.checkBox18.Text + "</td>");
                StrHead.Append("\r\n");
            }
            if (checkBox19.Checked)
            {
                StrHead.Append("<td class='td19'>" + this.checkBox19.Text + "</td>");
                StrHead.Append("\r\n");
            }

            return StrHead.ToString();

            #endregion

        }

        /// <summary>
        /// FarPintתHTML
        /// </summary>
        private string ConvertToHtml()
        {
            #region HTML��װ

            StringBuilder StrHtml = new StringBuilder();

            StrHtml.Append("<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">");
            StrHtml.Append("\r\n");

            #region ��װͷ��

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

            for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
            {
                StrHtml.Append("<tr>");
                for (int j = 0; j < neuSpread1_Sheet1.Columns.Count - 1; j++)//��2��Ϊ�˸ɵ��������ź���������
                {
                    if (neuSpread1_Sheet1.Columns[j].Visible)
                    {
                        StrHtml.Append("<td class='td" + (j + 1) + "'>" + neuSpread1_Sheet1.Cells[i, j].Text + "</td>");
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
                StrJS.Append("eval(_InitScroll(\"A1\",\"A2\"," + this.tbdwWidth.Text + ",19*" + this.neuSpread1_Sheet1.Rows.Count + ",1000));");
                StrJS.Append("</SCRIPT>");
            }
            return StrJS.ToString();
        }

        private void TimerStartFun()
        {
            //��ѯ����
            SetData(this.neuDTBegin.Value, this.neuDTEnd.Value);
            //������ʾ
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

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, FileName);
        }

        private void neuDTBegin_ValueChanged(object sender, EventArgs e)
        {
            this.neuDTEnd.Value = this.neuDTBegin.Value.AddDays(1);
        }

        private void itemAll_Click(object o, EventArgs e)
        {
            this.EnumFilter = Filter.ȫ��;
        }

        private void itemAnae_Click(object o, EventArgs e)
        {
            this.EnumFilter = Filter.������;
        }

        private void itemOperation_Click(object o, EventArgs e)
        {
            this.EnumFilter = Filter.��������;
        }

        private void itemReady_Click(object o, EventArgs e)
        {
            this.EnumFilter = Filter.δ����;
        }

        Filter filter;

        /// <summary>
        /// ����ʵ��
        /// </summary>
        Filter EnumFilter
        {
            get 
            {
                return this.filter;
            }
            set
            {
                this.filter = value;
                switch (this.filter)
                {
                    case Filter.δ����:
                        for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                        {
                            if (this.neuSpread1_Sheet1.Cells[i, this.neuSpread1_Sheet1.Columns.Count - 1].Text == "δ����")
                            {
                                //this.neuSpread1_Sheet1.Rows[i].BackColor = Color.Red;
                                //this.neuSpread1_Sheet1.RowHeader.Rows[i].BackColor = Color.Red;
                                this.neuSpread1_Sheet1.Rows[i].Visible = true;
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Rows[i].Visible = false;
                            }

                        }
                        break;
                    case Filter.������:
                        for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                        {
                            if (this.neuSpread1_Sheet1.Cells[i, this.neuSpread1_Sheet1.Columns.Count - 1].Text == "������")
                            {
                                this.neuSpread1_Sheet1.Rows[i].Visible = true;
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Rows[i].Visible = false;
                            }

                        }
                        break;
                    case Filter.��������:
                        for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                        {
                            if (this.neuSpread1_Sheet1.Cells[i, this.neuSpread1_Sheet1.Columns.Count - 1].Text == "��������")
                            {
                                this.neuSpread1_Sheet1.Rows[i].Visible = true;
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Rows[i].Visible = false;
                            }

                        }
                        break;
                    case Filter.ȫ��:
                        for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                        {
                            this.neuSpread1_Sheet1.Rows[i].Visible = true;
                        }
                        break;
                }
            }
        }

        private void neuSpread1_MouseUp(object sender, MouseEventArgs e)
        {
            if (neuSpread1_Sheet1.ActiveRow.Index >= 0 && e.Button == MouseButtons.Right)
            {
                menu.Show(this.neuSpread1, new Point(e.X, e.Y));
            }
        }


        #region ѡ��鿴��

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[0].Visible = this.checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[1].Visible = this.checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[2].Visible = this.checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[3].Visible = this.checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[4].Visible = this.checkBox5.Checked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[5].Visible = this.checkBox6.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[6].Visible = this.checkBox7.Checked;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[7].Visible = this.checkBox8.Checked;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[8].Visible = this.checkBox9.Checked;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[9].Visible = this.checkBox10.Checked;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[10].Visible = this.checkBox11.Checked;
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[11].Visible = this.checkBox12.Checked;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[12].Visible = this.checkBox13.Checked;
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[13].Visible = this.checkBox14.Checked;
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[14].Visible = this.checkBox15.Checked;
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[15].Visible = this.checkBox16.Checked;
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[16].Visible = this.checkBox17.Checked;
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[17].Visible = this.checkBox18.Checked;
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            neuSpread1_Sheet1.Columns[18].Visible = this.checkBox19.Checked;
        }

        #endregion

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = checkBox1.Checked;
            if (this.timer1.Enabled)
            {
                this.TimerInterval = FS.FrameWork.Function.NConvert.ToInt32(this.tbTimerValue.Text) * 600000;
                this.timer1.Interval = this.TimerInterval;
            }
        }

        private void ucArrangeDisplay_ExitChanged(object sender, EventArgs e)
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
    /// ��������
    /// </summary>
    public enum Filter
    {
        δ����,
        ������,
        ��������,
        ȫ��
    }

    /// <summary>
    /// �����뵽��Ҫ��ʾ����Ϣ��������һ���������������ߡ��������֡���е��ʿ������ϵͳ�����ϴ�ֻ�ʿ����Ѳ�ػ�ʿ��
    /// </summary>
    public class ArrangeQuery
    {
        public ArrangeQuery() { }

        /// <summary>
        /// �������Ų�ѯ
        /// </summary>
        /// <param name="dt">Ĭ�ϲ�ѯ������</param>
        public ArrangeQuery(DateTime dt)
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
        /// ��ѯ�����������
        /// </summary>
        public DateTime DtBegin
        {
            get { return dtBegin; }
            set { dtBegin = value; }
        }

        /// <summary>
        /// a�������ڲ�ѯ
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public ArrayList QueryOpsApp(DateTime dtBegin, DateTime dtEnd)
        {
            ArrayList al = new ArrayList();
            dtBegin = Convert.ToDateTime(dtBegin.ToString("yyyy-MM-dd 00:00:00"));
            dtEnd = Convert.ToDateTime(dtEnd.ToString("yyyy-MM-dd 23:59:59"));
            //al = Environment.OperationManager.GetOpsAppList(dtBegin, dtEnd, "0");//�˴�0��ʱ���� �����Ժ���չ��
            al = Environment.OperationManager.GetOpsAppList(dtBegin, dtEnd);
            return al;

        }
        public ArrayList QueryOpsApp(FS.FrameWork.Models.NeuObject dept, DateTime dtBegin, DateTime dtEnd)
        {
            ArrayList al = new ArrayList();
            dtBegin = Convert.ToDateTime(dtBegin.ToString("yyyy-MM-dd 00:00:00"));
            dtEnd = Convert.ToDateTime(dtEnd.ToString("yyyy-MM-dd 23:59:59"));
            al = Environment.OperationManager.GetOpsAppList(dept, dtBegin, dtEnd);//�˴�0��ʱ���� �����Ժ���չ��
            return al;
        }
        /// <summary>
        /// ���������б�
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        private DataTable GetOpsData(ArrayList al)
        {
            DataTable dt = new DataTable("OperaApp");
            DataColumn[] dc = new DataColumn[]
            {
                new DataColumn("������"), new DataColumn("̨��"), new DataColumn("סԺ��"), new DataColumn("��������"),new DataColumn("��λ"), new DataColumn("��������"), new DataColumn("��ǰ���"),
                new DataColumn("��������"), new DataColumn("����"), 
                new DataColumn("һ��"), new DataColumn("����"), new DataColumn("��������"),
                new DataColumn("������"), new DataColumn("��������"), new DataColumn("��е��ʿ"), new DataColumn("Ѳ�ػ�ʿ"),new DataColumn("�Ƿ�Σ��"), new DataColumn("��ע"),new DataColumn("����ʱ��"), new DataColumn("�������")
            };

            try
            {
                dt.Columns.AddRange(dc);
                foreach (FS.HISFC.Models.Operation.OperationAppllication operaApp in al)
                {
                    #region ���
                    string roomName = string.Empty;
                    if (string.IsNullOrEmpty(operaApp.RoomID))
                    {
                        roomName = "";
                    }
                    else
                    {
                        roomName = Environment.OperationManager.ExecSqlReturnOne(string.Format("SELECT room_name  FROM MET_OPS_ROOM WHERE ROOM_ID = '{0}'", operaApp.RoomID));
                    }
                    string anesName = Environment.OperationManager.ExecSqlReturnOne(string.Format("SELECT d.NAME FROM COM_DICTIONARY d WHERE d.TYPE = 'ANESTYPE' AND d.CODE = '{0}'", operaApp.AnesType.ID));
                    string mainOperator = "", helper1 = "", helper2 = "", mainAnes = "", anesHelper = "", washNurse = "", cirNurse = "";
                    foreach (FS.HISFC.Models.Operation.ArrangeRole role in operaApp.RoleAl)
                    {
                        if (role.RoleType.ID.ToString() ==FS.HISFC.Models.Operation.EnumOperationRole.Operator.ToString())
                        {
                            mainOperator = role.Name;
                        }
                        if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper1.ToString())
                        {
                            helper1 = role.Name;
                        }
                        else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Helper2.ToString())
                        {
                            helper2 = role.Name;//����
                        }
                        else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.Anaesthetist.ToString())
                        {
                            mainAnes = role.Name;
                        }
                        else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.AnaesthesiaHelper.ToString())
                        {
                            anesHelper = role.Name;
                        }
                        else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.WashingHandNurse1.ToString())
                        {
                            washNurse = role.Name; //qixiehushi
                        }
                        else if (role.RoleType.ID.ToString() == FS.HISFC.Models.Operation.EnumOperationRole.ItinerantNurse1.ToString())
                        {
                            cirNurse = role.Name;
                        }
                    }
                    #endregion

                    DataRow dr = dt.NewRow();// = new DataRow();
                    #region ��ȡ�ַ���ǰ����
                    string BedID = "";
                    string DiagnoseAlList="";
                    if (operaApp.PatientInfo.PVisit.PatientLocation.Bed.ID.Length >= 4)
                    {
                        BedID = operaApp.PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
                    }
                    if (operaApp.PatientInfo.PVisit.PatientLocation.Dept.Name == "")
                    {
                        operaApp.PatientInfo.PVisit.PatientLocation.Dept.Name = Environment.OperationManager.ExecSqlReturnOne(string.Format("SELECT e.DEPT_NAME FROM COM_DEPARTMENT e WHERE e.DEPT_CODE='{0}'", operaApp.PatientInfo.PVisit.PatientLocation.Dept.ID)); 
                    }
                    if (operaApp.DiagnoseAl.Count != 0)
                    {
                        DiagnoseAlList = ((FS.FrameWork.Models.NeuObject)(operaApp.DiagnoseAl[0])).Name;
                    }
                    #endregion

 
                    string[] items = new string[]
                    { 
                        roomName, operaApp.BloodUnit, operaApp.PatientInfo.PID.PatientNO.TrimStart('0'), operaApp.PatientInfo.Name, BedID, operaApp.PatientInfo.PVisit.PatientLocation.Dept.Name,
                        DiagnoseAlList, 
                        operaApp.OperationInfos[0].OperationItem.Name, mainOperator, helper1, helper2, anesName, mainAnes, anesHelper, washNurse, cirNurse, 
                        operaApp.IsHeavy == true?"��":"��", operaApp.ApplyNote, operaApp.PreDate.ToString("yyyy-MM-dd hh:mm:ss"), operaApp.ExecStatus == "1" ?"δ����":"�Ѱ���"
                    };//ding dong ע���ˣ����������ֶε�ʱ��Ҫע��������Ǽ��仰��Ŷ �Ҽ��������鳤�� �ٺ�
                    if (operaApp.ExecStatus == "1")
                    {
                        if (string.IsNullOrEmpty(anesName.Trim()) && string.IsNullOrEmpty(anesHelper.Trim()))
                        {
                            items[items.Length - 1] = "δ����";
                        }
                        else
                        {
                            items[items.Length - 1] = "������";
                        }
                    }
                    else if (operaApp.ExecStatus == "3")
                    {
                        items[items.Length - 1] = "��������";
                    }
                    dr.ItemArray = items;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return dt;
        }

        /// <summary>
        /// ���ݿ�ʼʱ��ͽ���ʱ���ѯ����������Ϣ
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataTable SetDataTableData(DateTime begin, DateTime end)
        {
            ArrayList al = this.QueryOpsApp(begin, end);
            return this.GetOpsData(al);
        }

        /// <summary>
        /// ���ݿ�ʼʱ��ͽ���ʱ��Ϳ��ұ����ѯ����������Ϣ
        /// </summary>
        /// <param name="dept">���ұ���</param>
        /// <param name="begin">��ʼʱ��</param>
        /// <param name="end">����ʱ��</param>
        /// <returns></returns>
        public DataTable SetDataTableData(FS.FrameWork.Models.NeuObject dept, DateTime begin, DateTime end)
        {
            ArrayList al = this.QueryOpsApp(dept, begin, end);
            return this.GetOpsData(al);
        }

    }
}
