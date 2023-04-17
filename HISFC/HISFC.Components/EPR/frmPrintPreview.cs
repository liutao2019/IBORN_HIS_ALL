using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR
{
    public partial class frmPrintPreview : Form
    {

        #region "�ֶ�"
        private int intPage;
        ArrayList arrPageNumber;
        private int[] checkprints;
        private int startPage = 1;
        private int TotalPage;
        private Rectangle rect;
        private int[] yEnds;
        private int top;
        private int[] tops;
        private bool[] isPrinteds;
        private FS.HISFC.Models.RADT.PatientInfo patient;

        //private ArrayList richtextboxs;
        //private richtextboxprintctrl.richtextboxprintctrl richtextboxprintctrl1;
        private int row;
        private Point pointStart;
        private ArrayList arrControls;
        private ArrayList arrPicture;
        private Image gCheked = null;
        private Image gUnCheked = null;
        private Image gRadioCheked = null;
        private Image gRadioUnCheked = null;
        private bool bIsAutoFont = true;
        #endregion "�ֶ�"

        #region "��ӡ"
        //private void GetCheckPrint(e System.Drawing.Printing.PrintPageEventArgs)
        //    checkprint = 0
        //    Dim temp int = 0
        //    While (temp <= textPos AndAlso temp < this.richtextboxprintctrl1.TextLength)
        //        startPage += 1
        //        checkprint = temp
        //        temp = richtextboxprintctrl1.VirtualPrint(temp, richtextboxprintctrl1.TextLength, e, this.PictureBox1.BackgroundImage)
        //    }
        //}
        #endregion ��ӡ

        #region "���캯��"
        public frmPrintPreview(Panel panel, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            // �˵����� Windows ���������������ġ�
            InitializeComponent();
            this.arrControls = this.SortControls(panel);
            this.patient = patient;
        }
        public frmPrintPreview(ArrayList arrPicture, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            InitializeComponent();
            this.arrPicture = arrPicture;
            this.patient = patient;
        }

        private void frmPrintView_Load(object sender, System.EventArgs e)
        {
            //this.PrintPreviewControl1.Zoom = 1;
            if (this.arrPicture == null || this.arrPicture.Count == 0)
            {
            gCheked = this.imageList1.Images[0];
            gUnCheked = this.imageList1.Images[1];
            gRadioCheked = this.imageList1.Images[2];
            gRadioUnCheked = this.imageList1.Images[3];
                this.Print();
            }
            else
            {
                for (int i = 0; i < arrPicture.Count; i++)
                {
                    this.cboPage.Items.Add(i + 1);
                }
                cboPage.SelectedIndexChanged += new EventHandler(this.ComboBox1_SelectedIndexChanged);
                this.cboPage.SelectedIndex = 0;
            }
        }
        #endregion "���캯��"

        #region "�¼����¼�������"

        /// <summary>
        /// �����С�ı��¼�����ӡԤ��ʼ�վ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPrintPreview_Resize(object sender, EventArgs e)
        {
            if (this.Panel2.Controls.Count > 0)
            {
                Control pic = this.Panel2.Controls[0];
                if (Panel2.Width > pic.Width + 26)
                {
                    pic.Left = (Panel2.Width - pic.Width) / 2;
                }
                else
                {
                    pic.Left = 12;
                }
                pic.Top = 13 - this.Panel2.VerticalScroll.Value * 1169 / (this.Panel2.VerticalScroll.Maximum - this.Panel2.VerticalScroll.Minimum);
            }
        }
        
        /// <summary>
        /// ҳ��������ѡ��ı��¼�
        /// �򿪶�Ӧ��Ԥ��ҳ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //this.PrintPreviewControl1.StartPage = this.ComboBox1.SelectedIndex;
            this.Panel2.Controls.Clear();
            PictureBox pic = new PictureBox();
            pic.Size = new Size(820, 1150);
            pic.MaximumSize = new Size(820, 1150);
            pic.MinimumSize = new Size(820, 1150);
            if(Panel2.Width > pic.Width + 26)
            {
                pic.Left = (Panel2.Width - pic.Width) / 2;
            }
            else
            {
                pic.Left = 12;
            }
            pic.Top = 13 - this.Panel2.VerticalScroll.Value * 1169 / (this.Panel2.VerticalScroll.Maximum - this.Panel2.VerticalScroll.Minimum);
            Image img = (Image)arrPicture[this.cboPage.SelectedIndex];
            pic.Image = img;
            pic.MouseDown += new MouseEventHandler(pic_MouseDown);
            pic.MouseMove += new MouseEventHandler(pic_MouseMove);
            //pic.Dock = DockStyle.Top;
            pic.Anchor = AnchorStyles.None;
            pic.BackColor = Color.White;
            this.Panel2.Controls.Add(pic);

        }

        /// <summary>
        /// Ԥ��ͼ����ƶ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || System.Math.Max(System.Math.Abs(e.X - pointStart.X), System.Math.Abs(e.Y - pointStart.Y)) <= 5)
            {
                return;
            }
            PictureBox pic = (PictureBox)sender;
            //ѡ���ӡ��Χ
            if (this.rdoPrintSelection.Checked)
            {
                PictureBox picPanel = new PictureBox();
                picPanel.Left = System.Math.Min(e.X, pointStart.X);
                picPanel.Top = System.Math.Min(e.Y, pointStart.Y);
                picPanel.Width = System.Math.Abs(e.X - pointStart.X);
                picPanel.Height = System.Math.Abs(e.Y - pointStart.Y);
                picPanel.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                pic.Controls.Clear();
                pic.Controls.Add(picPanel);
                pic.Update();
            }
            //���򣬰��������������
            else if (this.rdoContinuePrint.Checked)
            {
                //�µ����·�
                if (e.Y - pointStart.Y > 5)
                {
                    //�µ������·�
                    if (e.X - pointStart.X < -5)
                    {
                        PictureBox picPanel1 = new PictureBox();
                        picPanel1.Left = 0;
                        picPanel1.Top = pointStart.Y;
                        picPanel1.Width = pointStart.X;
                        picPanel1.Height = e.Y - pointStart.Y;
                        picPanel1.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        PictureBox picPanel2 = new PictureBox();
                        picPanel2.Left = 0;
                        picPanel2.Top = e.Y;
                        picPanel2.Width = pic.Width;
                        picPanel2.Height = pic.Height - e.Y;
                        picPanel2.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        pic.Controls.Clear();
                        pic.Controls.Add(picPanel1);
                        pic.Controls.Add(picPanel2);
                        pic.Update();
                    }
                    //�µ������·�
                    else if (e.X - pointStart.X >5)
                    {
                        PictureBox picPanel1 = new PictureBox();
                        picPanel1.Left = pointStart.X;
                        picPanel1.Top = pointStart.Y;
                        picPanel1.Width = pic.Width - pointStart.X;
                        picPanel1.Height = e.Y - pointStart.Y;
                        picPanel1.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        PictureBox picPanel2 = new PictureBox();
                        picPanel2.Left = 0;
                        picPanel2.Top = e.Y;
                        picPanel2.Width = pic.Width;
                        picPanel2.Height = pic.Height - e.Y;
                        picPanel2.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        pic.Controls.Clear();
                        pic.Controls.Add(picPanel1);
                        pic.Controls.Add(picPanel2);
                        pic.Update();
                    }
                    //�µ���һ��ֱ�߿��󣬺��Ż����¼�ȵ�
                    else if (e.X - pointStart.X < 0)
                    {
                        PictureBox picPanel1 = new PictureBox();
                        picPanel1.Left = 0;
                        picPanel1.Top = 0;
                        picPanel1.Width = pointStart.X;
                        picPanel1.Height = pic.Height;
                        picPanel1.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        pic.Controls.Clear();
                        pic.Controls.Add(picPanel1);
                        pic.Update();
                    }
                    //�µ���һ��ֱ�߿��ң����µ��ȵ�
                    else
                    {
                        PictureBox picPanel1 = new PictureBox();
                        picPanel1.Left = pointStart.X;
                        picPanel1.Top = 0;
                        picPanel1.Width = pic.Width - pointStart.X;
                        picPanel1.Height = pic.Height;
                        picPanel1.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        pic.Controls.Clear();
                        pic.Controls.Add(picPanel1);
                        pic.Update();
                    }
                }
                //�µ����Ϸ�
                else if (e.Y - pointStart.Y < -5)
                {
                    //�µ������Ϸ�
                    if (e.X - pointStart.X > 5)
                    {
                        PictureBox picPanel1 = new PictureBox();
                        picPanel1.Left = pointStart.X;
                        picPanel1.Top = e.Y;
                        picPanel1.Width = pic.Width - pointStart.X;
                        picPanel1.Height = pointStart.Y - e.Y;
                        picPanel1.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        PictureBox picPanel2 = new PictureBox();
                        picPanel2.Left = 0;
                        picPanel2.Top = pointStart.Y;
                        picPanel2.Width = pic.Width;
                        picPanel2.Height = pic.Height - pointStart.Y;
                        picPanel2.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        pic.Controls.Clear();
                        pic.Controls.Add(picPanel1);
                        pic.Controls.Add(picPanel2);
                        pic.Update();
                    }
                    //�µ������Ϸ�
                    else if (e.X - pointStart.X < -5)
                    {
                        PictureBox picPanel1 = new PictureBox();
                        picPanel1.Left = 0;
                        picPanel1.Top = e.Y;
                        picPanel1.Width = pointStart.X;
                        picPanel1.Height = pointStart.Y - e.Y;
                        picPanel1.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        PictureBox picPanel2 = new PictureBox();
                        picPanel2.Left = 0;
                        picPanel2.Top = pointStart.Y;
                        picPanel2.Width = pic.Width;
                        picPanel2.Height = pic.Height - pointStart.Y;
                        picPanel2.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        pic.Controls.Clear();
                        pic.Controls.Add(picPanel1);
                        pic.Controls.Add(picPanel2);
                        pic.Update();
                    }
                    //�µ���һ��ֱ�߿��󣬺��Ż����¼�ȵ�
                    else if (e.X - pointStart.X < 0)
                    {
                        PictureBox picPanel1 = new PictureBox();
                        picPanel1.Left = 0;
                        picPanel1.Top = 0;
                        picPanel1.Width = pointStart.X;
                        picPanel1.Height = pic.Height;
                        picPanel1.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        pic.Controls.Clear();
                        pic.Controls.Add(picPanel1);
                        pic.Update();
                    }
                    //�µ���һ��ֱ�߿��ң����µ��ȵ�
                    else
                    {
                        PictureBox picPanel1 = new PictureBox();
                        picPanel1.Left = pointStart.X;
                        picPanel1.Top = 0;
                        picPanel1.Width = pic.Width - pointStart.X;
                        picPanel1.Height = pic.Height;
                        picPanel1.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                        pic.Controls.Clear();
                        pic.Controls.Add(picPanel1);
                        pic.Update();
                    }
                }
                //��һ��ֱ����
                else
                {
                    PictureBox picPanel1 = new PictureBox();
                    picPanel1.Left = 0;
                    picPanel1.Top = pointStart.Y;
                    picPanel1.Width = pic.Width;
                    picPanel1.Height = pic.Height - pointStart.Y;
                    picPanel1.BackColor = System.Drawing.Color.FromArgb(70, 0, 100, 100);
                    pic.Controls.Clear();
                    pic.Controls.Add(picPanel1);
                    pic.Update();
                }
            }
        }

        /// <summary>
        /// Ԥ��ͼ��갴���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pic_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            //��λ��ӡ��ʼλ��
            if (e.Button == MouseButtons.Left)
            {
                pointStart = e.Location;
                pic.Controls.Clear();
                pic.Update();
            }
        }

        /// <summary>
        /// ��ӡ��ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            if (Panel2.Controls == null || Panel2.Controls.Count == 0)
            {
                MessageBox.Show("û��ҳ��Ҫ��ӡ");
                return;
            }
            PictureBox pic = (PictureBox)this.Panel2.Controls[0];
            if (pic == null)
            {
                MessageBox.Show("û��ҳ��Ҫ��ӡ");
                return;
            }
            if (this.rdoContinuePrint.Checked || this.rdoPrintSelection.Checked)
            {
                if (pic.Controls == null || pic.Controls.Count == 0)
                {
                    MessageBox.Show("������ѡ���ӡ��Χ");
                    return;
                }
            }

            System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument();
            document.BeginPrint += new PrintEventHandler(document_BeginPrint);
            document.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.document_PrintPage);
            document.EndPrint += new PrintEventHandler(document_EndPrint);

            document.Print();
            //pic.Controls.Clear();
        }

        void document_EndPrint(object sender, PrintEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        void document_BeginPrint(object sender, PrintEventArgs e)
        {
            arrPageNumber = new ArrayList();
            arrPageNumber.Clear();
            intPage = 0;
            if (this.rdoPrintAll.Checked)
            {
                for (int i = 0; i < arrPicture.Count; i++)
                {
                    arrPageNumber.Add(i + 1);
                }
            }
            else if (this.rdoPrintPages.Checked)
            {
                string[] strs1 = this.txtPages.Text.Split(',');
                foreach (string str1 in strs1)
                {
                    string[] strs2 = str1.Split('-');
                    for (int i = 0; i < strs2.Length; i++)
                    {
                        arrPageNumber.Add(int.Parse(strs2[i]));
                    }
                }
            }
            else if (this.rdoPrintActivePage.Checked)
            {
                arrPageNumber.Add(this.cboPage.SelectedIndex + 1);
            }
            else if (this.rdoContinuePrint.Checked)
            {
                for (int i = this.cboPage.SelectedIndex; i < arrPicture.Count; i++)
                {
                    arrPageNumber.Add(i + 1);
                }
            }
            else
            {
                arrPageNumber.Add(this.cboPage.SelectedIndex + 1);
            }
        }

        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Image img = (Image)((Image)arrPicture[((int)arrPageNumber[intPage]) - 1]).Clone();
            e.Graphics.DrawImage(img, 0, 0);
            Graphics grap = Graphics.FromImage(img);
            if ((this.rdoContinuePrint.Checked || this.rdoPrintSelection.Checked) && intPage == 0)
            {
                PictureBox pic = (PictureBox)this.Panel2.Controls[0];
                if (pic.Controls.Count >= 1)
                {
                    Control picPanel = pic.Controls[0];
                    grap.FillRectangle(Brushes.White, 0, 0, pic.Width, picPanel.Top);
                    grap.FillRectangle(Brushes.White, 0, picPanel.Top, picPanel.Left, picPanel.Height);
                    grap.FillRectangle(Brushes.White, picPanel.Right, picPanel.Top, pic.Width - picPanel.Right, picPanel.Height);
                    if (pic.Controls.Count == 1)
                    {
                        grap.FillRectangle(Brushes.White, 0, picPanel.Bottom, pic.Width, pic.Height - picPanel.Bottom);
                    }
                }
            }
            e.Graphics.DrawImage(img, 0, 0);
            intPage++;
            if (intPage >= arrPageNumber.Count)
            {
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }
        }

        #endregion "�¼����¼�������"

        #region "����"
        //private void SetBitmap(bmp Bitmap)

        //    for( i int = 0 To bmp.Width - 1
        //        for( j int = 0 To bmp.Height - 1
        //            bmp.SetPixel(i, j, Color.White)
        //        }
        //    }

        //}


        /// <summary>
        /// ����X��Y����Control
        /// </summary>
        /// <param name="panel"></param>
        /// <returns>ArrayList��ArrayList�ļ��ϣ���ArrayList������Control</returns>
        private ArrayList SortControls(Control panel)
        {
            ArrayList arrControls = new ArrayList();
            bool isInsert = false;
            foreach (Control ctrCurrent in panel.Controls)
            {
                isInsert = false;
                //���û�У������һ�У���ctrCurrent�ŵ���һ��
                if (arrControls.Count == 0)
                {
                    ArrayList arrNewRow = new ArrayList();
                    arrNewRow.Add(ctrCurrent);
                    arrControls.Add(arrNewRow);
                    isInsert = true;
                }
                //�������˳��ŵ�ArrayList����
                else
                {
                    for (int i = arrControls.Count - 1; i >= 0; i--) //�������ÿһ��
                    {
                        ArrayList arrRowi = (ArrayList)arrControls[i];
                        Control ctri0 = (Control)arrRowi[0];
                        if (ctrCurrent.Top > ctri0.Top + 8) //��i��֮�£�����i�к������һ�У���ctrCurrent�ŵ���һ��
                        {
                            ArrayList arrNewRow = new ArrayList();
                            arrNewRow.Add(ctrCurrent);
                            arrControls.Insert(i + 1, arrNewRow);
                            isInsert = true;
                            break;
                        }
                        else if (ctrCurrent.Top >= ctri0.Top - 8 && ctrCurrent.Top <= ctri0.Top + 8) //��ͬһ��
                        {
                            for (int j = arrRowi.Count - 1; j >= 0; j--) //�������ÿһ���ؼ�
                            {
                                Control ctrij = (Control)arrRowi[j];
                                if (ctrCurrent.Left > ctrij.Left) //��j�ؼ�֮��
                                {
                                    arrRowi.Insert(j + 1, ctrCurrent); //���ؼ�����j�ؼ�֮��
                                    isInsert = true;
                                    break;
                                }
                            }
                            if (!isInsert)  //�����пؼ�֮ǰ
                            {
                                arrRowi.Insert(0, ctrCurrent); //�ŵ���һ��λ��
                                isInsert = true;
                            }
                            break;
                        }
                    }
                    if (!isInsert)   //��������֮ǰ
                    {
                        ArrayList arrNewRow = new ArrayList();
                        arrNewRow.Add(ctrCurrent);
                        arrControls.Insert(0, arrNewRow);
                    }
                }
            }
            return arrControls;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void Print()
        {
            if (arrControls == null || arrControls.Count == 0)
            {
                return;
            }
            arrPicture = new ArrayList();
            //checkprint = 0;

            checkprints = new int[20];
            yEnds = new int[20];
            tops = new int[20];
            isPrinteds = new bool[20];
            //index = 0;
            row = 0;
            top = 0;

            Image img = new Bitmap(827, 1169);
            Graphics grap = Graphics.FromImage(img);
            PageSettings setting = new PageSettings();
            setting.Color = true;
            setting.Landscape = false;
            setting.Margins = new Margins(50,100,50, 80);
            setting.PaperSize = new PaperSize("A4", 827, 1169);
            System.Drawing.Printing.PrintPageEventArgs e = new System.Drawing.Printing.PrintPageEventArgs(
                grap, new Rectangle(50, 100, 727, 989), new Rectangle(0, 0, 827, 1169), setting);
            e.HasMorePages = true;
            while (e.HasMorePages)
            {
                e.HasMorePages = false;
                PrintPage(e);
            }
            for (int i = 0; i < TotalPage; i++)
            {
                this.cboPage.Items.Add((i + startPage).ToString());
            }
            cboPage.SelectedIndexChanged += new EventHandler(this.ComboBox1_SelectedIndexChanged);
            if (this.cboPage.Items.Count > 0)
            {
                this.cboPage.SelectedIndex = 0;
            }
        }
        
        /// <summary>
        /// ��ӡҳ
        /// </summary>
        /// <param name="e"></param>
        private void PrintPage(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Image img = new Bitmap(e.PageBounds.Width, e.PageBounds.Height);
            Graphics grap = Graphics.FromImage(img);
            grap.Clear(Color.White);

            if (TotalPage != 0)
            {
                grap.DrawString("����: " + patient.Name, new Font("����", 12, FontStyle.Regular), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top - 30);
                grap.DrawString("�Ʊ�: " + patient.PVisit.PatientLocation.Dept.Name, new Font("����", 12, FontStyle.Regular), Brushes.Black, e.MarginBounds.Left + 150, e.MarginBounds.Top - 30);
                grap.DrawString("����: " + patient.PVisit.PatientLocation.NurseCell.Name, new Font("����", 12, FontStyle.Regular), Brushes.Black, e.MarginBounds.Left + 300, e.MarginBounds.Top - 30);
                grap.DrawString("����: " + patient.PVisit.PatientLocation.Bed.Name, new Font("����", 12, FontStyle.Regular), Brushes.Black, e.MarginBounds.Left + 450, e.MarginBounds.Top - 30);
                grap.DrawString("סԺ��: " + patient.PID.ID, new Font("����", 12, FontStyle.Regular), Brushes.Black, e.MarginBounds.Left + 550, e.MarginBounds.Top - 30);
                grap.DrawLine(new Pen(Color.Black, 2), e.MarginBounds.Left, e.MarginBounds.Top - 8, e.MarginBounds.Right, e.MarginBounds.Top - 8);
            }
            grap.DrawString("�� " + (TotalPage + startPage).ToString() + " ҳ", new Font("����", 12), Brushes.Black, e.PageBounds.Width / 2 - 40, e.MarginBounds.Bottom + 4);
            //}
            TotalPage += 1;
            top = 0;
            tops = new int[20];
            //bool isPrinted = false;
            for (int i = row; i < arrControls.Count; i++)
            {
                //if (checkprints[0] + checkprints[1] + checkprints[2] + checkprints[3] + checkprints[4] + checkprints[5] + checkprints[6] + checkprints[7] + checkprints[8] + checkprints[9] == 0)
                //{
                //    isPrinted = false;
                //}
                //else
                //{
                //    isPrinted = true;
                //}
                //��ǰ��
                ArrayList arrRow = (ArrayList)arrControls[i];
                //�����е�ÿһ���ؼ�
                for (int j = 0; j < arrRow.Count; j++)
                {
                    Control ctr = (Control)arrRow[j];
                    #region emrMultiLineTextBox
                    if (ctr.GetType() == typeof(FS.FrameWork.EPRControl.emrMultiLineTextBox) || ctr.GetType().IsSubclassOf(typeof(FS.FrameWork.EPRControl.emrMultiLineTextBox)))
                    {
                        FS.FrameWork.EPRControl.emrMultiLineTextBox ctrCurrent =
                            (FS.FrameWork.EPRControl.emrMultiLineTextBox)ctr;
                        //�����ӡ��ɣ��򲻴�ӡ
                        if (checkprints[j] >= ctrCurrent.TextLength)
                        {
                            tops[j] = 0;
                            continue;
                        }
                        //���򣬽��Ŵ�ӡ����ؼ�
                        else
                        {
                            rect = new Rectangle(ctrCurrent.Left, top, ctrCurrent.Width, 0);
                            if (checkprints[j] > 0)
                            {
                                yEnds[j] = ctrCurrent.GetLineFromCharIndex(checkprints[j] - 1);
                            }
                            else
                            {
                                yEnds[j] = 0;
                            }
                            ctrCurrent.print(checkprints[j], ctrCurrent.TextLength, e, rect, grap);
                            checkprints[j] = ctrCurrent.print(checkprints[j], ctrCurrent.TextLength, e, rect);
                            //if (checkprint > textPos) {
                            //    startPage += 1
                            //}

                            //look for more pages 
                            // �����ǰ��RichTextBox������������������һҳ���Ŵ�ӡ
                            if (checkprints[j] < ctrCurrent.TextLength)
                            {
                                e.HasMorePages = true;
                            }
                            // �����ǰ��RichTextBoxû�г���������������һ�е�λ��
                            else
                            {
                                int temp = ctrCurrent.GetLineFromCharIndex(checkprints[j]) - yEnds[j];
                                //Font ft = ctrCurrent.Font;
                                //double space = 0;
                                //int asc = 0;
                                //space = ft.FontFamily.GetLineSpacing(System.Drawing.FontStyle.Regular);
                                //asc = ft.FontFamily.GetEmHeight(System.Drawing.FontStyle.Regular);
                                //temp = (int)(ft.Height * space * temp * 1.000 / asc);

                                //ֻ��һ�У�ȡĬ���и�
                                if (temp == 0)
                                {
                                    temp = (int)e.Graphics.MeasureString("��", ctrCurrent.Font).Height + 2;
                                }
                                //��ͷ��ʼ������0-30�У�����31�У�ֻȡ��30�У���Ҫ����Ĭ���и�
                                else if (yEnds[j] == 0)
                                {
                                    temp = (int)((ctrCurrent.GetPositionFromCharIndex(checkprints[j]).Y - ctrCurrent.GetPositionFromCharIndex(ctrCurrent.GetFirstCharIndexFromLine(yEnds[j])).Y) * 1.000) + (int)e.Graphics.MeasureString("��", ctrCurrent.Font).Height + 2;
                                }
                                //������ӡ�������30-60��ʵ�ʴ�31�д�ӡ����30�У����ü�Ĭ���и�
                                else
                                {
                                    temp = (int)((ctrCurrent.GetPositionFromCharIndex(checkprints[j]).Y - ctrCurrent.GetPositionFromCharIndex(ctrCurrent.GetFirstCharIndexFromLine(yEnds[j])).Y) * 1.000);
                                }
                                tops[j] = top + temp;
                                if (tops[j] + 20 > e.MarginBounds.Height)
                                {
                                    e.HasMorePages = true;
                                }
                                //checkprint = 0;
                            }
                        }
                    }
                    #endregion emrMultiLineTextBox
                    #region ucEMRInput
                    else if (ctr.GetType() == typeof(FS.HISFC.Components.EPR.Controls.ucEMRInput) || ctr.GetType().IsSubclassOf(typeof(FS.HISFC.Components.EPR.Controls.ucEMRInput)))
                    {
                        FS.HISFC.Components.EPR.Controls.ucEMRInput ctrCurrent =
                            (FS.HISFC.Components.EPR.Controls.ucEMRInput)ctr;
                        //�����ӡ��ɣ��򲻴�ӡ
                        if (checkprints[j] >= ctrCurrent.GetTextLength())
                        {
                            tops[j] = 0;
                            continue;
                        }
                        //���򣬽��Ŵ�ӡ����ؼ�
                        else
                        {
                            rect = new Rectangle(ctrCurrent.Left, top, ctrCurrent.Width, 0);
                            if (checkprints[j] > 0)
                            {
                                yEnds[j] = ctrCurrent.GetLineFromCharIndex(checkprints[j] - 1);
                            }
                            else
                            {
                                yEnds[j] = 0;
                            }
                            ctrCurrent.print(checkprints[j], ctrCurrent.GetTextLength(), e, rect, grap);
                            checkprints[j] = ctrCurrent.print(checkprints[j], ctrCurrent.GetTextLength(), e, rect);
                            //if (checkprint > textPos) {
                            //    startPage += 1
                            //}

                            //look for more pages 
                            // �����ǰ��RichTextBox������������������һҳ���Ŵ�ӡ
                            if (checkprints[j] < ctrCurrent.GetTextLength())
                            {
                                e.HasMorePages = true;
                            }
                            // �����ǰ��RichTextBoxû�г���������������һ�е�λ��
                            else
                            {
                                int temp = ctrCurrent.GetLineFromCharIndex(checkprints[j]) - yEnds[j];
                                //Font ft = ctrCurrent.Font;
                                //double space = 0;
                                //int asc = 0;
                                //space = ft.FontFamily.GetLineSpacing(System.Drawing.FontStyle.Regular);
                                //asc = ft.FontFamily.GetEmHeight(System.Drawing.FontStyle.Regular);
                                //temp = (int)(ft.Height * space * temp * 1.000 / asc);
                                //tops[j] = top + temp;
                                //checkprint = 0;
                                
                                //ֻ��һ�У�ȡĬ���и�
                                if (temp == 0)
                                {
                                    temp = (int)e.Graphics.MeasureString("��", ctrCurrent.Font).Height + 2;
                                }
                                //��ͷ��ʼ������0-30�У�����31�У�ֻȡ��30�У���Ҫ����Ĭ���и�
                                else if (yEnds[j] == 0)
                                {
                                    temp = (int)((ctrCurrent.GetPositionFromCharIndex(checkprints[j]).Y - ctrCurrent.GetPositionFromCharIndex(ctrCurrent.GetFirstCharIndexFromLine(yEnds[j])).Y) * 1.000) + (int)e.Graphics.MeasureString("��", ctrCurrent.Font).Height + 2;
                                }
                                //������ӡ�������30-60��ʵ�ʴ�31�д�ӡ����30�У����ü�Ĭ���и�
                                else
                                {
                                    temp = (int)((ctrCurrent.GetPositionFromCharIndex(checkprints[j]).Y - ctrCurrent.GetPositionFromCharIndex(ctrCurrent.GetFirstCharIndexFromLine(yEnds[j])).Y) * 1.000);
                                }
                                tops[j] = top + temp;
                                if (tops[j] + 20 > e.MarginBounds.Height)
                                {
                                    e.HasMorePages = true;
                                }
                            }
                        }
                    }
                    #endregion ucEMRInput
                    else
                    {
                        //û�д�ӡ
                        if (!isPrinteds[j])
                        {
                            if (ctr.Height + top > e.MarginBounds.Height && top != 0)
                            {
                                e.HasMorePages = true;
                                tops[j] = 0;
                            }
                            else
                            {
                                this.DrawForm(e, grap, new Point(ctr.Left, 0), ctr);
                                tops[j] = top + ctr.Height + 4;
                                isPrinteds[j] = true;
                            }
                        }
                    }
                }
                // �����ǰ���е�RichTextBox��û�г����������ҵ�Ҫ��ӡ��λ��
                if (!e.HasMorePages)
                {
                    for (int j = 0; j < arrRow.Count; j++)
                    {
                        if (tops[j] > top)
                        {
                            top = tops[j];
                        }
                    }
                    checkprints = new int[20];
                    yEnds = new int[20];
                    tops = new int[20];
                    row = i + 1;
                    isPrinteds = new bool[20];
                }
                //�����˳�
                else
                {
                    top = 0;
                    arrPicture.Add(img);
                    img.Save("d:\\" + this.TotalPage.ToString() + "1.bmp");
                    return;
                }
            }
            arrPicture.Add(img);
            img.Save("d:\\" + this.TotalPage.ToString() + "1.bmp");
        }

        /// <summary>
        /// ��Container�ؼ�
        /// </summary>
        /// <param name="e"></param>
        /// <param name="grap"></param>
        /// <param name="pointForm"></param>
        /// <param name="form"></param>
        private void DrawForm(PrintPageEventArgs e, Graphics grap, Point pointForm, Control form)
        {
            this.DrawControl(e, grap, pointForm, form);
            foreach (Control c in form.Controls)
            {
                Point pointCtr = new Point(pointForm.X + c.Location.X, pointForm.Y + c.Location.Y);
                if (c != null && c.Visible)
                {
                    this.DrawForm(e, grap, pointCtr, c);
                }
            }
        }

        /// <summary>
        /// ���ؼ�
        /// </summary>
        /// <param name="e"></param>
        /// <param name="grap"></param>
        /// <param name="pointCtr"></param>
        /// <param name="ctr"></param>
        private void DrawControl(PrintPageEventArgs e, Graphics grap, Point pointCtr, Control ctr)
        {
            //Label:��ӡ�ı�����ʽ���ÿؼ�ԭ���ĸ�ʽ
            if (ctr.GetType() == typeof(Label) || ctr.GetType().IsSubclassOf(typeof(Label)))
            {
                //e.Graphics.DrawString(ctr.Text, ctr.Font, Brushes.Black, new Point(pointParent.X + e.MarginBounds.Left, (top + pointParent.Y) + e.MarginBounds.Top));
                grap.DrawString(ctr.Text, ctr.Font, new SolidBrush(ctr.ForeColor), new Point(pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top));
            }
            //TextBox:��ӡ�ı�����ʽ���ÿؼ�ԭ���ĸ�ʽ
            else if (ctr.GetType() == typeof(TextBox) || ctr.GetType().IsSubclassOf(typeof(TextBox)))
            {
                //e.Graphics.DrawString(ctr.Text, ctr.Font, Brushes.Black, new Point(pointParent.X + e.MarginBounds.Left, (top + pointParent.Y) + e.MarginBounds.Top));
                grap.DrawString(ctr.Text, ctr.Font, new SolidBrush(ctr.ForeColor), new Point(pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top));
            }
            //else if (ctr.GetType() == typeof(PictureBox) || ctr.GetType().IsSubclassOf(typeof(PictureBox)))
            //{
            //    //e.Graphics.DrawImage(((PictureBox)ctr).Image, new Point(pointParent.X + e.MarginBounds.Left, (top + pointParent.Y) + e.MarginBounds.Top));
            //    grap.DrawImage(((PictureBox)ctr).Image, new Point(pointParent.X + e.MarginBounds.Left, (int)((top + pointParent.Y) / 1.00) + e.MarginBounds.Top));
            //    tops[j] = (top + pointParent.Y) + ctr.Height;
            //}

            //emrLine:��ӡ�ڲ����
            else if (ctr.GetType() == typeof(FS.FrameWork.EPRControl.emrLine) || ctr.GetType().IsSubclassOf(typeof(FS.FrameWork.EPRControl.emrLine)))
            {
                //e.Graphics.DrawString(ctr.Text, ctr.Font, Brushes.Black, new Point(pointParent.X + e.MarginBounds.Left, (top + pointParent.Y) + e.MarginBounds.Top));
                grap.FillRectangle(new SolidBrush(ctr.BackColor), pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, ctr.Width, ctr.Height);
            }
            //FarPoint:����FarPoint�Ĵ�ӡ
            else if (ctr.GetType() == typeof(FarPoint.Win.Spread.FpSpread) || ctr.GetType().IsSubclassOf(typeof(FarPoint.Win.Spread.FpSpread)))
            {
                DrawFarpoint(grap, ctr, pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, ctr.Width, ctr.Height);

            }
            //CheckBox����ӡѡ�����ı�
            else if (ctr.GetType() == typeof(CheckBox) || ctr.GetType().IsSubclassOf(typeof(CheckBox)))
            {
                CheckBox t = ctr as CheckBox;
                if (t.Checked)
                {
                    grap.DrawImage(gCheked, pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top + 2);
                }
                else
                {
                    grap.DrawImage(gUnCheked, pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top + 2);
                }
                grap.DrawString(ctr.Text, ctr.Font, new SolidBrush(ctr.ForeColor), pointCtr.X + e.MarginBounds.Left + gCheked.Width, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, new StringFormat());
            }
            //RadioButton����ӡѡ�����ı�
            else if (ctr.GetType() == typeof(RadioButton) || ctr.GetType().IsSubclassOf(typeof(RadioButton)))
            {
                RadioButton t = ctr as RadioButton;
                if (t.Checked)
                {
                    grap.DrawImage(gRadioCheked, pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top + 2);
                }
                else
                {
                    grap.DrawImage(gRadioUnCheked, pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top + 2);
                }
                grap.DrawString(ctr.Text, ctr.Font, new SolidBrush(ctr.ForeColor), pointCtr.X + e.MarginBounds.Left + gCheked.Width, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, new StringFormat());
            }
            //GroupBox����ӡ�ڲ����ͱ߽��Լ��ڲ��ı�
            else if (ctr.GetType() == typeof(GroupBox) || ctr.GetType().IsSubclassOf(typeof(GroupBox)))
            {
                ControlPaint.DrawBorder(grap, new Rectangle(pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, ctr.Width, ctr.Height), Color.Black, ButtonBorderStyle.Solid);
                grap.FillRectangle(new SolidBrush(ctr.BackColor), pointCtr.X + e.MarginBounds.Left + 10, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top - 8, grap.MeasureString(ctr.Text, ctr.Font).Width, grap.MeasureString(ctr.Text, ctr.Font).Height);
                grap.DrawString(ctr.Text, ctr.Font, new SolidBrush(ctr.ForeColor), pointCtr.X + e.MarginBounds.Left + 10, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top - 8, new StringFormat());
            }
            //PictureBox����ӡͼƬ
            else if (ctr.GetType() == typeof(PictureBox) || ctr.GetType().IsSubclassOf(typeof(PictureBox)))
            {
                PictureBox t = ctr as PictureBox;
                Image m = null;
                try
                {
                    m = t.Image.Clone() as Image;
                    grap.DrawImage(m, pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, ctr.Width, ctr.Height);
                }
                catch { }
            }
            //Panel����ӡ�����ɫ�����ݿؼ�
            else if (ctr.GetType() == typeof(Panel) || ctr.GetType().IsSubclassOf(typeof(Panel)))
            {
                grap.FillRectangle(new SolidBrush(ctr.BackColor), pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, ctr.Width, ctr.Height);
            }
            //TabPage������ӡ
            else if (ctr.GetType() == typeof(TabPage) || ctr.GetType().IsSubclassOf(typeof(TabPage)))
            {

            }
            //TabControl������ӡ
            else if (ctr.GetType() == typeof(TabControl) || ctr.GetType().IsSubclassOf(typeof(TabControl)))
            {

            }
            //SpreadView������ӡ
            else if (ctr.GetType() == typeof(FarPoint.Win.Spread.SpreadView) || ctr.GetType().IsSubclassOf(typeof(FarPoint.Win.Spread.SpreadView)))
            {

            }
            //HScrollBar������ӡ
            else if (ctr.GetType() == typeof(HScrollBar) || ctr.GetType().IsSubclassOf(typeof(HScrollBar)))
            {

            }
            //VScrollBar������ӡ
            else if (ctr.GetType() == typeof(VScrollBar) || ctr.GetType().IsSubclassOf(typeof(VScrollBar)))
            {

            }
            //ScrollBar������ӡ
            else if (ctr.GetType() == typeof(ScrollBar) || ctr.GetType().IsSubclassOf(typeof(ScrollBar)))
            {

            }
            //DataGrid������ӡ
            else if (ctr.GetType() == typeof(DataGrid) || ctr.GetType().IsSubclassOf(typeof(DataGrid)))
            {
                #region ����� --������dtTable
                DataGrid t = ctr as DataGrid;
                int CaptionHeight = 20;
                //�����
                grap.FillRectangle(new SolidBrush(t.BackColor), pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, ctr.Width, ctr.Height);
                if (t.CaptionVisible)
                {
                    grap.FillRectangle(new SolidBrush(t.CaptionBackColor), pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, ctr.Width, ctr.Height);
                    grap.DrawString(t.CaptionText, t.Font, new SolidBrush(t.CaptionForeColor), pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top);
                }

                //����
                System.Data.DataTable dt = t.DataSource as System.Data.DataTable;
                Rectangle r;
                string sTemp = "";
                if (dt != null)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        r = t.GetCellBounds(0, col);
                        grap.FillRectangle(new SolidBrush(t.HeaderBackColor), pointCtr.X + e.MarginBounds.Left + r.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top + CaptionHeight, r.Width, r.Height);
                        grap.DrawString(dt.Columns[col].ColumnName, t.Font, new SolidBrush(t.HeaderForeColor), pointCtr.X + e.MarginBounds.Left + r.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top + CaptionHeight);

                    }
                    for (int row1 = 0; row1 < dt.Rows.Count; row1++)
                    {
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            try
                            {
                                sTemp = dt.Rows[row1][col].ToString();
                            }
                            catch
                            {
                                sTemp = "";
                            }
                            r = t.GetCellBounds(row1, col);
                            grap.FillRectangle(new SolidBrush(t.BackColor), pointCtr.X + e.MarginBounds.Left + r.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top + r.Top, r.Width, r.Height);
                            grap.DrawString(sTemp, t.Font, new SolidBrush(t.ForeColor), pointCtr.X + e.MarginBounds.Left + r.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top + r.Top);
                            //��grid ����
                            Pen pen = new Pen(t.GridLineColor);
                            grap.DrawRectangle(pen, pointCtr.X + e.MarginBounds.Left + r.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top + r.Top, r.Width, r.Height);

                        }
                    }
                }
                #endregion ����� --������dtTable
            }
            else if (ctr.GetType() == typeof(DateTimePicker) || ctr.GetType().IsSubclassOf(typeof(DateTimePicker)))
            {
                //���ڣ���������ؼ�
                try
                {
                    grap.FillRectangle(new SolidBrush(ctr.BackColor), pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, ctr.Width, ctr.Height);
                    if (((DateTimePicker)ctr).Value == DateTime.MinValue)
                    {
                        grap.DrawString("00-00-00", ctr.Font, new SolidBrush(ctr.ForeColor), pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, new StringFormat());
                    }
                    else
                    {
                        grap.DrawString(ctr.Text, ctr.Font, new SolidBrush(ctr.ForeColor), pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, new StringFormat());
                    }
                }
                catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
            }
            else if (ctr.GetType() == typeof(Button) || ctr.GetType().IsSubclassOf(typeof(Button)))//����ӡ��ť
            {
            }
            else if (ctr.GetType() == typeof(Form) || ctr.GetType().IsSubclassOf(typeof(Form)))//����ӡ����
            {

            }
            else
            {
                if (ctr.Text != "")
                {
                    grap.FillRectangle(new SolidBrush(ctr.BackColor), pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, ctr.Width, ctr.Height);
                    if (ctr.Text != "")
                    {
                        grap.DrawString(ctr.Text, AutoFont(ctr, grap), new SolidBrush(ctr.ForeColor), pointCtr.X + e.MarginBounds.Left, (int)((top + pointCtr.Y) / 1.00) + e.MarginBounds.Top, new StringFormat());//2+pointParent.X + e.MarginBounds.Left+(int)pBlankMargin.X,3+allTop+ctr.Height /2 -grap.MeasureString("��",ctr.Font).Height/2+(int)pBlankMargin.Y		break;
                    }
                }

            }
        }

        /// <summary>
        /// ��farpoint
        /// </summary>
        /// <param name="g"></param>
        /// <param name="c"></param>
        /// <param name="ControlLeft"></param>
        /// <param name="ControlTop"></param>
        /// <param name="ControlWidth"></param>
        /// <param name="ControlHeight"></param>
        private void DrawFarpoint(System.Drawing.Graphics g, Control c, int ControlLeft, int ControlTop, int ControlWidth, int ControlHeight)
        {
            //#region farpoint
            //FarPoint.Win.Spread.FpSpread t = c as FarPoint.Win.Spread.FpSpread;
            //if (bIsDataAutoExtend)//�Զ���չ
            //{
            //    ControlHeight = this.iPageHeight - ControlTop - 5;
            //    ControlWidth = this.printDocument1.DefaultPageSettings.PaperSize.Width - ControlLeft - 5;
            //}
            //Rectangle rect = new Rectangle(ControlLeft, ControlTop, ControlWidth, ControlHeight);//new Rectangle(ControlLeft ,ControlTop ,ControlWidth,ControlHeight);//ControlWidth,ControlHeight );
            //FarPoint.Win.Spread.PrintInfo printinfo = new FarPoint.Win.Spread.PrintInfo();
            //for (int iSheet = 0; iSheet < t.Sheets.Count; iSheet++)
            //{
            //    if (this.ControlBorder == enuControlBorder.None)
            //        printinfo.ShowBorder = false;
            //    printinfo.ShowRowHeaders = t.Sheets[iSheet].RowHeader.Visible;
            //    printinfo.ShowColumnHeaders = t.Sheets[iSheet].ColumnHeader.Visible;
            //    printinfo.PageStart = 0;
            //    t.Sheets[iSheet].PrintInfo = printinfo;
            //}
            //int iCount = t.GetOwnerPrintPageCount(g, rect, 0);
            //if (maxpage < iCount) maxpage = iCount; //Ϊ���farpoint��ӡ�õ�
            //if (addpage == 0 && maxpage == iCount)
            //{
            //    addpage = 1;
            //}
            //if (addpage <= iCount)
            //{
            //    t.OwnerPrintDraw(g, rect, t.ActiveSheetIndex, addpage);
            //    addpage++;
            //}
            //if (addpage > maxpage)
            //{
            //    addpage = 0;
            //}
            //#endregion
        }

        /// <summary>
        /// ��ӡ�����Զ��䶯
        /// </summary>
        /// <param name="t"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        private Font AutoFont(Control t, Graphics g)
        {
            Font newFont = null;
            if (this.bIsAutoFont)//�Զ�
            {
                if (g.MeasureString(t.Text, t.Font).Width > t.Width)
                {
                    newFont = new System.Drawing.Font(t.Font.FontFamily, (t.Font.Size * (float)0.8), t.Font.Style, t.Font.Unit);
                }
                else
                {
                    newFont = t.Font;
                }
            }
            else
            {
                newFont = t.Font;
            }
            return newFont;
        }

        #endregion "����"

        /// <summary>
        /// ������̰����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPrintPreview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (this.Panel2.Controls.Count > 0)
                {
                    Control pic = this.Panel2.Controls[0];
                    pic.Controls.Clear();
                }
            }
        }

        /// <summary>
        /// ��ӡѡ��ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Panel2.Controls.Count == 0)
            {
                return;
            }
            RadioButton chkBox = (RadioButton)sender;
            if (chkBox.Checked)
            {
                Control ctr = this.Panel2.Controls[0];
                ctr.Controls.Clear();
            }
        }
    }
}
