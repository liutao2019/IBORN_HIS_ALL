using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
namespace FS.HISFC.Components.EPR.Controls
{
    /// <summary>
    /// ���Ӳ�����snomed����
    /// </summary>
    public partial class ucEMRInput : System.Windows.Forms.UserControl, FS.FrameWork.EPRControl.IGroup, FS.FrameWork.EPRControl.IUserControlable
    {
        private FS.FrameWork.WinForms.Controls.RichTextBox richTextBoxEx1;
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ucEMRInput()
        {
            //
            // Windows ���������֧���������
            //
            InitializeComponent();

            base.Tag = FS.FrameWork.EPRControl.emrNode.UserTag;
            this.richTextBoxEx1.Enter += new EventHandler(richTextBoxEx1_Enter);

            this.richTextBoxEx1.HideSelection = false;
            //
            // TODO: �� InitializeComponent ���ú�����κι��캯������
            //
        }

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxEx1 = new FS.FrameWork.WinForms.Controls.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxEx1
            // 
            this.richTextBoxEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEx1.Font = new System.Drawing.Font("����_GB2312", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.richTextBoxEx1.HideSelection = false;
            this.richTextBoxEx1.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.Size = new System.Drawing.Size(408, 296);
            this.richTextBoxEx1.SuperText = "";
            this.richTextBoxEx1.TabIndex = 0;
            this.richTextBoxEx1.Text = "";
            //this.richTextBoxEx1.���� = "richTextBoxEx1";
            //this.richTextBoxEx1.�Ƿ��� = false;
            //this.richTextBoxEx1.�� = "��";
            this.richTextBoxEx1.TextChanged += new System.EventHandler(this.richTextBoxEx1_TextChanged);
            // 
            // ucEMRInput
            // 
            this.Controls.Add(this.richTextBoxEx1);
            this.Font = new System.Drawing.Font("����_GB2312", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
            this.Name = "ucEMRInput";
            this.Size = new System.Drawing.Size(408, 296);
            this.Load += new System.EventHandler(this.frmTestEMRInput_Load);
            this.ResumeLayout(false);

        }
        #endregion

        string flag = "@^";

        ucSNOMED ucShow = new ucSNOMED();

        #region ����
        /// <summary>
        /// ��С
        /// </summary>
        public new System.Drawing.Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
                if (value.Height < 30)
                {
                    this.richTextBoxEx1.Multiline = false;
                }
                else
                {
                    this.richTextBoxEx1.Multiline = true;
                }
            }
        }

        private bool bIsOnSaveSuperText = true;
        /// <summary>
        /// �Ƿ����ڴ洢SuperText
        /// </summary>
        public bool IsOnSaveSuperText
        {
            get
            {
                return this.bIsOnSaveSuperText;
            }
            set
            {
                this.bIsOnSaveSuperText = value;
                if (value == false)
                {
                    tooltip.SetToolTip(this.richTextBoxEx1, "��ǰ�����ʽ");
                }
                else
                {
                    tooltip.SetToolTip(this.richTextBoxEx1, "��ǰֻ�����ı�");
                }
                tooltip.Active = true;
                tooltip.InitialDelay = 1;
            }
        }
        /// <summary>
        /// rtf
        /// </summary>
        public string Rtf
        {
            get
            {
                return this.richTextBoxEx1.Rtf;
            }
            set
            {
                this.richTextBoxEx1.Rtf = value;
            }
        }
        /// <summary>
        /// �ı�
        /// </summary>
        [CategoryAttribute("���"), Description("�ı�"), Browsable(false)]
        public override string Text
        {
            get
            {
                return this.richTextBoxEx1.Text;
            }
            set
            {
                //this.richTextBoxEx1.Text = value;
                this.OnTextChanged(null);
            }
        }
        /// <summary>
        /// �ı�
        /// </summary>
        [CategoryAttribute("���"), Description("�ı�"), Browsable(true)]
        public string SuperText
        {
            get
            {
                if (this.bIsOnSaveSuperText)
                    return this.richTextBoxEx1.SuperText;
                else
                    return "";
            }
            set
            {
                if (this.bIsOnSaveSuperText)
                {
                    this.richTextBoxEx1.SuperText = value;
                    bakSuperText = value;
                }
            }
        }

        private bool bMust = false;
        /// <summary>
        /// ����ѡ��
        /// </summary>
        public bool ����
        {
            get
            {
                return this.bMust;
            }
            set
            {
                this.bMust = value;
            }
        }

        public int GetTextLength()
        {
            return this.richTextBoxEx1.TextLength;
        }

        public int GetLineFromCharIndex(int Index)
        {
            return this.richTextBoxEx1.GetLineFromCharIndex(Index);
        }

        public Point GetPositionFromCharIndex(int Index)
        {
            return this.richTextBoxEx1.GetPositionFromCharIndex(Index);
        }

        public int GetFirstCharIndexFromLine(int Index)
        {
            return this.richTextBoxEx1.GetFirstCharIndexFromLine(Index);
        }

        #endregion

        #region �ı�����

        ToolTip tooltip = new ToolTip();
        private void ucShow_Selected(object sender, EventArgs e)
        {
            try
            {
                int i = this.richTextBoxEx1.SelectionStart;
                this.richTextBoxEx1.SelectedText = flag;//((FS.HISFC.Models.ClinicalPath.SNOPMED)sender).Name;
                this.richTextBoxEx1.InsertLink(((FS.HISFC.Models.EPR.SNOMED)sender).Name, ((FS.HISFC.Models.EPR.SNOMED)sender).ParentCode, i);
                this.richTextBoxEx1.Select(this.richTextBoxEx1.SelectionStart, flag.Length);
                this.richTextBoxEx1.SelectedText = "\0";
            }
            catch { }
        }

        private void frmTestEMRInput_Load(object sender, System.EventArgs e)
        {
            ucShow.Selected += new EventHandler(ucShow_Selected);

            this.richTextBoxEx1.MouseDown += new MouseEventHandler(richTextBoxEx1_MouseDown);
            this.richTextBoxEx1.KeyDown += new KeyEventHandler(richTextBoxEx1_KeyDown);
            this.richTextBoxEx1.KeyPress += new KeyPressEventHandler(richTextBoxEx1_KeyPress);
            this.richTextBoxEx1.LostFocus += new EventHandler(richTextBoxEx1_LostFocus);

            if ((this.bIsOnSaveSuperText) && bakSuperText != "")
                this.richTextBoxEx1.SuperText = bakSuperText;
            this.initListForm();


        }

        private void richTextBoxEx1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int start = this.richTextBoxEx1.GetCharIndexFromPosition(new System.Drawing.Point(e.X, e.Y));
                if (this.richTextBoxEx1.GetPositionFromCharIndex(this.richTextBoxEx1.TextLength).Y < e.Y - 9
                    && start == this.richTextBoxEx1.TextLength - 1)
                {
                    start = start + 1;
                }
                if (this.SelectLinkText(start) == 0)
                    this.ShowList();
            }

        }

        private int SelectLinkText(int index)
        {
            try
            {

                this.richTextBoxEx1.Select(index, 1);

                if (this.richTextBoxEx1.GetSelectionLink().ToString() == "0")
                {
                    this.richTextBoxEx1.Select(index, 0);
                    this.frmList.Hide();
                    return -1;
                }

                int start, end;
                int i;
                for (i = index; i >= 0; i--)
                {
                    this.richTextBoxEx1.Select(i, 1);
                    {
                        if (this.richTextBoxEx1.GetSelectionLink().ToString() == "1")
                        {

                        }
                        else
                        {
                            break;
                        }
                    }
                }
                start = i + 1;
                for (i = index; i <= this.richTextBoxEx1.TextLength; i++)
                {
                    this.richTextBoxEx1.Select(i, 1);
                    {
                        if (this.richTextBoxEx1.GetSelectionLink().ToString() == "1")
                        {

                        }
                        else
                        {
                            break;
                        }
                    }
                }
                end = i;
                this.richTextBoxEx1.Select(start, end - start);
                string s = this.GetLinkText(this.richTextBoxEx1.SelectedText);
                if (s != "")
                {
                    this.Filter(s);
                    return 0;
                }

            }
            catch { return -1; }
            return -1;
        }

        private void richTextBoxEx1_Click(object sender, EventArgs e)
        {
            this.frmList.Hide();
        }

        private void richTextBoxEx1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.frmList.Visible)
            {

                if (e.KeyCode == Keys.Down)
                {
                    ucShow.MoveNext();
                }
                else if (e.KeyCode == Keys.Up)
                {
                    ucShow.MovePre();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    ucShow.Get();
                }
                else if (e.KeyCode == Keys.Space)
                {
                    ucShow.Get();
                    ucShow.ShowWindow();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    frmList.Hide();
                }
                else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
                {
                    frmList.Hide();
                }
                else
                {
                    ucShow.AddKey(e.KeyData.ToString());
                }
                e.Handled = true;
            }
            else
            {
                if (e.Control && e.KeyCode == Keys.J)
                {
                    if (this.SelectLinkText(this.richTextBoxEx1.SelectionStart) == 0)
                        this.ShowList();
                }
                else if (e.Control && e.KeyCode == Keys.K)
                {
                    if (this.SelectLinkText(this.richTextBoxEx1.SelectionStart) == 0)
                        ucShow.ShowWindow();
                }
            }

        }
        Form frmList = new Form();

        /// <summary>
        /// ��ʾ�б�
        /// </summary>
        public void ShowList(Point point)
        {
            frmList.TopMost = true;
            frmList.Show();
            frmList.Location = point;
        }

        private void initListForm()
        {
            frmList.Closing += new CancelEventHandler(frmList_Closing);
            frmList.Size = new Size(1, 1);
            frmList.Show();
            frmList.Hide();
            frmList.Size = new Size(180, 280);
            frmList.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            ucShow.Visible = true;
            ucShow.Dock = DockStyle.Fill;
            frmList.Controls.Add(ucShow);


        }
        private void frmList_Closing(object sender, CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        /// <summary>
        /// ��ʾ
        /// </summary>
        public void ShowList()
        {
            System.Drawing.Point point = this.richTextBoxEx1.GetPositionFromCharIndex(this.richTextBoxEx1.SelectionStart);
            point = this.PointToScreen(point);
            point.Y = point.Y + this.Font.Height + 10;
            this.ShowList(point);
        }

        public override Font Font
        {
            get
            {
                return this.richTextBoxEx1.Font;
            }
            set
            {
                if (this.bIsOnSaveSuperText)
                {
                    this.richTextBoxEx1.SuperText = bakSuperText;
                }
                this.richTextBoxEx1.Font = value;
            }
        }


        public void SaveSuperText()
        {
            bakSuperText = this.richTextBoxEx1.SuperText;
        }
        public void ResetSuperText()
        {
            this.richTextBoxEx1.SuperText = bakSuperText;
        }

        private string bakSuperText = "";
        private string GetLinkText(string text)
        {
            string s = this.richTextBoxEx1.SuperText;
            int k = s.IndexOf("^@" + text);
            if (k < 0) return "";
            int start = s.IndexOf("#", k);
            int end = s.IndexOf("@^", start);
            if (end > start && start > 0)
            {
                return s.Substring(start + 1, end - start - 1) + "@^";
            }
            return "";
        }

        private void richTextBoxEx1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void richTextBoxEx1_LostFocus(object sender, EventArgs e)
        {
            if (this.ContainsFocus == false)
                frmList.Hide();
        }
       

        #endregion

        #region �б����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkText"></param>
        public void Filter(string linkText)
        {
            try
            {
                try
                {
                    if (linkText.LastIndexOf('#') >= 0)
                        linkText = linkText.Substring(linkText.LastIndexOf('#') + 1);
                }
                catch { }
                if (linkText == "") return;

                ucShow.Filter(linkText);
            }
            catch { }
        }

        #region
        protected override void ScaleCore(float dx, float dy)
        {
            SaveSuperText();
            base.ScaleCore(dx, dy);
        }
        protected override void NotifyInvalidate(Rectangle invalidatedArea)
        {
            base.NotifyInvalidate(invalidatedArea);
            if (this.bIsOnSaveSuperText) this.richTextBoxEx1.SuperText = bakSuperText;
        }

        #endregion

        #endregion

        #region IGroup ��Ա

        public event FS.FrameWork.EPRControl.IsGroupChangedEventHandler IsGroupChanged;

        public event FS.FrameWork.EPRControl.NameChangedEventHandler NameChanged;
        private bool bGroup = false;
        [Browsable(false)]
        public bool �Ƿ���
        {
            get
            {
                // TODO:  ��� ucEMRInput.�Ƿ��� getter ʵ��
                return false;
            }
            set
            {
                // TODO:  ��� ucEMRInput.�Ƿ��� setter ʵ��
                this.bGroup = value;
                if (this.IsGroupChanged != null)
                    this.IsGroupChanged(this, null);

            }
        }
        protected string strName;
        [CategoryAttribute("���")]
        public string ����
        {
            get
            {
                // TODO:  ��� ucEMRInput.���� getter ʵ��
                return strName;
            }
            set
            {
                // TODO:  ��� ucEMRInput.���� setter ʵ��
                strName = value;
                if (this.NameChanged != null)
                    this.NameChanged(this, null);
            }
        }
        protected string strGroup;
        [TypeConverter(typeof(FS.FrameWork.EPRControl.emrGroup)), CategoryAttribute("���"), DefaultValueAttribute(""), DescriptionAttribute("ѡ��ؼ�������")]
        public string ��
        {
            get
            {
                // TODO:  ��� ucEMRInput.�� getter ʵ��
                return strGroup;
            }
            set
            {
                // TODO:  ��� ucEMRInput.�� setter ʵ��
                strGroup = value;
                if (this.GroupChanged != null)
                    this.GroupChanged(this, null);
            }
        }

        public event FS.FrameWork.EPRControl.GroupChangedEventHandler GroupChanged;

        #endregion

        #region IControlTextable ��Ա

        private string level1;
        private string level2;
        private string temp;
        private int iLevel = 3;
        public ArrayList GetQCData()
        {
            // TODO:  ��� ucEMRInput.GetQCData ʵ��
            return null;
        }

        public Control FocedControl
        {
            get
            {
                // TODO:  ��� ucEMRInput.FocedControl getter ʵ��
                return this.richTextBoxEx1;
            }
        }

        public new event EventHandler Enter;
        private void richTextBoxEx1_Enter(object sender, EventArgs e)
        {
            if (Enter != null)
                Enter(sender, e);
        }

        public void ShowLevel3Text()
        {
            // TODO:  ��� ucEMRInput.ShowLevel3Text ʵ��
            if (iLevel == 1)
            {
                this.Level1Rtf = this.richTextBoxEx1.Rtf;
                this.richTextBoxEx1.Rtf = temp;
            }
            else if (iLevel == 2)
            {
                this.Level2Rtf = this.richTextBoxEx1.Rtf;
                this.richTextBoxEx1.Rtf = temp;
            }
        }

        public string Level1Rtf
        {
            get
            {
                // TODO:  ��� ucEMRInput.Level1Rtf getter ʵ��
                if (this.level1 == "")
                    this.level1 = this.richTextBoxEx1.Rtf;
                return this.level1;
            }
            set
            {
                // TODO:  ��� ucEMRInput.Level1Rtf setter ʵ��
                this.level1 = value;
            }
        }

        public void ShowLevel1Text()
        {
            // TODO:  ��� ucEMRInput.ShowLevel1Text ʵ��
            iLevel = 1;
            temp = this.richTextBoxEx1.Rtf;
            this.richTextBoxEx1.Rtf = this.Level1Rtf;
        }

        public string Level2Rtf
        {
            get
            {
                // TODO:  ��� ucEMRInput.Level2Rtf getter ʵ��
                if (this.level2 == "")
                    this.level2 = this.richTextBoxEx1.Rtf;
                return this.level2;
            }
            set
            {
                // TODO:  ��� ucEMRInput.Level2Rtf setter ʵ��
                this.level2 = value;
            }
        }

        public void ShowLevel2Text()
        {
            // TODO:  ��� ucEMRInput.ShowLevel2Text ʵ��
            iLevel = 2;
            temp = this.richTextBoxEx1.Rtf;
            this.richTextBoxEx1.Rtf = this.Level2Rtf;
        }

        #endregion

        private void richTextBoxEx1_TextChanged(object sender, System.EventArgs e)
        {
            this.Text = System.DateTime.Now.ToString();
        }

        #region IUserControlable ��Ա

        public bool IsPrint
        {
            get
            {
                // TODO:  ��� ucEMRInput.IsPrint getter ʵ��
                return false;
            }
            set
            {
                // TODO:  ��� ucEMRInput.IsPrint setter ʵ��
            }
        }

        public int Valid(object sender)
        {
            if ((this.Text == "-" || this.Text == "") && this.bMust)
            {
                MessageBox.Show(this.���� + "û����д!");
                return -1;
            }
            return 0;
        }

        public void LoadUC(object sender, string[] @params)
        {
            // TODO:  ��� ucEMRInput.LoadUC ʵ��
        }

        public void RefreshUC(object sender, string[] @params)
        {
            // TODO:  ��� ucEMRInput.RefreshUC ʵ��
        }

        public short Save(object sender, FS.FrameWork.Management.Transaction t)
        {
            // TODO:  ��� ucEMRInput.Save ʵ��
            return 0;
        }

        #endregion

        #region IUserControlable ��Ա

        public Control FocusedControl
        {
            get { return this.richTextBoxEx1; }
        }

        public void Init(object sender, string[] @params)
        {
           
        }

        public int Save(object sender)
        {
            TemplateDesignerApplication.ucLoader loader = sender as TemplateDesignerApplication.ucLoader;
            if (sender == null) return 0;

            //���浽���ݿ�����
            if (this.����!="")
            {
                FS.HISFC.Models.File.DataFileInfo dt = loader.dt.Clone();
                
                string txt = this.richTextBoxEx1.SuperText;
                while (txt != "")
                {
                    try
                    {
                        txt = getString(txt, dt);
                    }
                    catch { }
                }
                
            }
            this.IsOnSaveSuperText = false;
            
            return 0;
        }

        private string getString(string sss,FS.HISFC.Models.File.DataFileInfo dt)
        {
            string beginString = "^@";
            string endString = "@^";
            int iBegin = sss.IndexOf(beginString);
            int iEnd = -2;
            if (iBegin > 0 )
            {
                 iEnd = sss.Substring(iBegin).IndexOf(endString);
                 if (iEnd > 2 )
                 {
                     string info = sss.Substring(iBegin + 2, iEnd  - 2);
                     string[] ss = info.Split('#');
                     if (ss.Length >= 2)
                     {
                         ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetSNOMED(ss[1], false);

                         if (al.Count > 0)
                         {
                             
                             FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.SaveNodeToDataStore("DataStore_EMR", dt, this.����+"\\"+al[0].ToString(),al[0].ToString(), ss[0]);
                         }
                     }
                 }
                 else
                 {
                     return "";
                 }
            }
            else
            {
                return "";
            }
            return sss.Substring(iBegin + iEnd + 2);
            //+ snomed.Name + "#" + snomed.ID + "@^";
        }

        #endregion

        #region ������ӡ
        private const double aninch = 15;
        private const int em_formatrange = 0x439;
        private const int wm_user = 0x400;

        public int print(int charfrom, int charto, PrintPageEventArgs e)
        {
            Rectangle rectangle = new Rectangle(0, 0, e.MarginBounds.Width, 0);
            return this.print(charfrom, charto, e, rectangle, e.Graphics);
      }

        public int print(int charfrom, int charto, PrintPageEventArgs e, Rectangle rectangle)
        {
            return this.print(charfrom, charto, e, rectangle, e.Graphics);
        }
        public int print(int charfrom, int charto, PrintPageEventArgs e, Rectangle rectangle, Graphics grap)
        {
            charRange cRange;
            formatRange fRange;
            Rect rectPage;
            Rect rectToPrint;

            //Ҫ��ӡ���ַ�
            cRange.charPositionMin = charfrom;
            cRange.charPositionMax = charto;

            //��ӡ����
            rectToPrint.top = (int)Math.Round((double)((rectangle.Top + e.MarginBounds.Top) * aninch));
            if (rectangle.Height == 0)
            {
                rectToPrint.bottom = (int)Math.Round((double)(e.MarginBounds.Bottom * aninch));
            }
            else
            {
                rectToPrint.bottom = (int)Math.Round((double)((e.MarginBounds.Top + rectangle.Bottom) * aninch));
            }
            rectToPrint.left = (int)Math.Round((double)((rectangle.Left + e.MarginBounds.Left) * aninch));
            rectToPrint.right = (int)Math.Round((double)((rectangle.Right + e.MarginBounds.Left)* aninch));

            //����ҳ��
            rectPage.top = (int)Math.Round((double)(e.PageBounds.Top * aninch));
            rectPage.bottom = (int)Math.Round((double)(e.PageBounds.Bottom * aninch));
            rectPage.left = (int)Math.Round((double)(e.PageBounds.Left * aninch));
            rectPage.right = (int)Math.Round((double)(e.PageBounds.Right * aninch));

            //��ӡ����
            IntPtr hdc = grap.GetHdc();
            //IntPtr hdc1 = e.Graphics.GetHdc();
            fRange.chrg = cRange;
            fRange.hdc = hdc;
            fRange.hdctarget = hdc;
            fRange.rc = rectToPrint;
            fRange.rcpage = rectPage;
            IntPtr res = IntPtr.Zero;
            IntPtr wparam = IntPtr.Zero;
            wparam = new IntPtr(1);
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fRange));
            Marshal.StructureToPtr(fRange, lparam, false);

            //��ӡ
            res = sendmessage(this.richTextBoxEx1.Handle, em_formatrange, wparam, lparam);

            //�ͷ���Դ
            Marshal.FreeCoTaskMem(lparam);
            grap.ReleaseHdc(hdc);
            return res.ToInt32();
        }

        [DllImport("user32.dll", EntryPoint="SendMessageA", CharSet=CharSet.Ansi, SetLastError=true, ExactSpelling=true)]
        private static extern IntPtr sendmessage(IntPtr hwnd, int msg, IntPtr wp, IntPtr lp);
        public int VirtualPrint(int charfrom, int charto, PrintPageEventArgs e, Image image)
        {
            charRange crange;
            formatRange fmtrange;
            Rect rectpage;
            Rect recttoprint;

            //Ҫ��ӡ���ַ�
            crange.charPositionMin = charfrom;
            crange.charPositionMax = charto;

            //��ӡ����
            recttoprint.top = (int) Math.Round((double) (e.MarginBounds.Top * aninch));
            recttoprint.bottom = (int) Math.Round((double) (e.MarginBounds.Bottom * aninch));
            recttoprint.left = (int) Math.Round((double) (e.MarginBounds.Left * aninch));
            recttoprint.right = (int) Math.Round((double) (e.MarginBounds.Right * aninch));
            
            //����ҳ��
            rectpage.top = (int) Math.Round((double) (e.PageBounds.Top * aninch));
            rectpage.bottom = (int) Math.Round((double) (e.PageBounds.Bottom * aninch));
            rectpage.left = (int) Math.Round((double) (e.PageBounds.Left * aninch));
            rectpage.right = (int) Math.Round((double) (e.PageBounds.Right * aninch));
            Graphics graph = Graphics.FromImage(image);

            //��ӡ����
            IntPtr hdc = graph.GetHdc();
            fmtrange.chrg = crange;
            fmtrange.hdc = hdc;
            fmtrange.hdctarget = hdc;
            fmtrange.rc = recttoprint;
            fmtrange.rcpage = rectpage;
            IntPtr res = IntPtr.Zero;
            IntPtr wparam = IntPtr.Zero;
            wparam = new IntPtr(1);
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtrange));
            Marshal.StructureToPtr(fmtrange, lparam, false);

            //��ӡ
            res = sendmessage(this.richTextBoxEx1.Handle, em_formatrange, wparam, lparam);

            //�ͷ���Դ
            Marshal.FreeCoTaskMem(lparam);
            graph.ReleaseHdc(hdc);
            return res.ToInt32();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct charRange
        {
            public int charPositionMin;
            public int charPositionMax;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct formatRange
        {
            public IntPtr hdc;
            public IntPtr hdctarget;
            public Rect rc;
            public Rect rcpage;
            public charRange chrg;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
#endregion ������ӡ

        #region Snomed ��Ա

        string snomed = "";
        [CategoryAttribute("���"), DefaultValueAttribute(""), DescriptionAttribute("Snomed����")]
        public string Snomed
        {
            get
            {
                return snomed;
            }
            set
            {
                snomed = value;

            }
        }

        #endregion
    }
}
