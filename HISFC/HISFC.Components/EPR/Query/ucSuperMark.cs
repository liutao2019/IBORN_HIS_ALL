using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.EPR;

namespace FS.HISFC.Components.EPR
{
    public partial class ucSuperMark : ucPrintPicture
    {
        #region �ֶ�
        Bitmap imgSelect;   //ѡ��ͼƬ
        Pen penDash;        //�㻮�߻���
        private InkOverlayEditingMode editingMode;  //ͼƬ�༭ģʽ
        private Color drawingColor; //������ɫ
        private InkOverlayEraserMode eraserMode;    //����ģʽ
        Point pointPosition;    //ѡ����λ��
        bool hasEraseOld;
        bool hasChanged = false;
        //private System.Windows.Forms.PictureBox frontPicture;

        #endregion �ֶ�

        public ucSuperMark()
        {
            InitializeComponent();
        }

        public ucSuperMark(TemplateDesignerApplication.ucDataFileLoader loader, FS.HISFC.Models.RADT.PatientInfo patient)
            : base(loader, patient)
        {
            InitializeComponent();
        }

        public ucSuperMark(ArrayList arrPicture)
        {
            InitializeComponent();
            this.arrPicture = arrPicture;
        }

        #region "�¼����¼�������"

        #region ��ť�¼�
        /// <summary>
        /// ���߰�ťClick�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDraw_Click(object sender, EventArgs e)
        {
            this.frontPicture.Controls.Clear();
            this.frontPicture.Cursor = Cursors.Default;
            this.tbSelect.Checked = this.tbText.Checked = this.tbEraze.Checked = false;
            this.editingMode = InkOverlayEditingMode.Ink;
        }

        /// <summary>
        /// ѡ��ťClick�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSelect_Click(object sender, EventArgs e)
        {
            this.frontPicture.Controls.Clear();
            this.frontPicture.Cursor = Cursors.Cross;

            this.tbDraw.Checked = this.tbText.Checked = this.tbEraze.Checked = false;

            this.editingMode = InkOverlayEditingMode.Select;
            this.cboPage.Focus();
        }

        /// <summary>
        /// ���ְ�ťClick�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbText_Click(object sender, EventArgs e)
        {
            this.frontPicture.Controls.Clear();

            this.tbDraw.Checked = this.tbSelect.Checked = this.tbEraze.Checked = false;
            //this.eraserMode = InkOverlayEraserMode.StrokeErase;
            this.frontPicture.Cursor = Cursors.IBeam;
            this.editingMode = InkOverlayEditingMode.Text;
        }

        /// <summary>
        /// ������ťClick�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbEraze_Click(object sender, EventArgs e)
        {
            this.frontPicture.Controls.Clear();

            this.tbDraw.Checked = this.tbSelect.Checked = this.tbText.Checked = false;
            this.eraserMode = InkOverlayEraserMode.PointErase;
            this.editingMode = InkOverlayEditingMode.Delete;
            this.frontPicture.Cursor = new Cursor(this.GetType(), "CursorErase.cur");
        }

        /// <summary>
        /// ѡ����ɫ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuColorPicker1_NewColor(object sender, FS.Toolkit.Controls.NewColorArgs e)
        {
            this.drawingColor = e.NewColor;
        }
        #endregion ��ť�¼�

        /// <summary>
        /// �򿪴����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.tbDraw,
            this.tbSelect,
            this.tbEraze,
            this.tbText,
            this.toolStripSeparator3});
            //this.pictureBox1.BackColor = Color.FromArgb(128, Color.Red);
            //this.panel1.BackColor = Color.FromArgb(128, Color.Red);
            penDash = new Pen(Color.Black, 1);
            penDash.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            //Bitmap bmp = new Bitmap(this.frontPicture.Width, this.frontPicture.Height);

            //this.SetBitmap(bmp);
            //this.frontPicture.Image = bmp;
            this.drawingColor = this.neuColorPicker1.Color;
            this.hasEraseOld = false;
            this.cboPage.DropDown -=  new EventHandler(cboPage_DropDown);
            this.cboPage.DropDown += new EventHandler(cboPage_DropDown);
            if (this.FindForm() != null)
            {
                this.FindForm().FormClosing -= new FormClosingEventHandler(ucSuperMark_FormClosing);
                this.FindForm().FormClosing += new FormClosingEventHandler(ucSuperMark_FormClosing);
            }
        }

        #region ��ͼͼ���¼�
        /// <summary>
        /// ��ͼͼ����갴���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private

        void  ucSuperMark_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.hasChanged)
            {
                DialogResult result = MessageBox.Show("�Ƿ��ύ�ϼ��޸�", "�ϼ��޸�", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    this.SetSuperMark();
                }
                hasChanged = false;
            }
        }
        void cboPage_DropDown(object sender, EventArgs e)
        {
            if(this.hasChanged)
            {
                DialogResult result = MessageBox.Show("�Ƿ��ύ�ϼ��޸�", "�ϼ��޸�", MessageBoxButtons.OKCancel);
                if(result == DialogResult.OK)
                {
                    this.SetSuperMark();
                }
                hasChanged = false;
            }
        }
        private void SetSuperMark()
        {
            //FS.HISFC.Management.EPR.SuperMark supermarkManager = new FS.HISFC.Management.EPR.SuperMark();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = this.loader.CurrentLoader.dt.ID;
            obj.Name = (this.cboPage.SelectedIndex + 1).ToString();
            obj.Memo = this.loader.CurrentLoader.dt.Name;
            obj.User03 = this.loader.CurrentLoader.dt.ID + ".xml";
            obj.User02 = this.loader.CurrentLoader.dt.Index1;
            obj.User03 = FS.FrameWork.Management.Connection.Operator.ID;
            string fileName = FS.FrameWork.WinForms.Classes.Function.GetTempFileName() + ".bmp";
            this.frontPicture.Image.Save(fileName);
            System.IO.FileStream stream = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
            byte[] byteimg = new byte[stream.Length];

            stream.Read(byteimg, 0, (int)stream.Length);
            stream.Close();
            try
            {
                System.IO.File.Delete(fileName);
            }
            catch
            {
            }
            if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.SetSuperMark(obj, byteimg) == -1)
            {
                MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
                return;
            }
            //�����ϼ��޸ĺۼ�
        }

        protected override void pic_MouseDown(object sender, MouseEventArgs e)
        {
            Image img = this.frontPicture.Image;
            Graphics grap = Graphics.FromImage(img);
            if (e.Button == MouseButtons.Left)
            {
                switch (this.editingMode)
                {
                    case InkOverlayEditingMode.Delete:
                        //���ģʽ�����ͼƬ
                        this.SetBitmap(this.frontPicture.Image as Bitmap, new Rectangle((int)(e.Location.X * times), (int)(e.Location.Y * times), (int)(8 * times), (int)(8 * times)));
                        this.frontPicture.Refresh();
                        this.hasChanged = true;
                        break;
                    case InkOverlayEditingMode.Text:
                        //�ı�ģʽ�������ı�
                        if (this.frontPicture.Controls.Count > 0 && this.frontPicture.Controls[0].GetType() == typeof(TextBox))
                        {
                            TextBox textBox = this.frontPicture.Controls[0] as TextBox;
                            if (textBox != null && textBox.Text != "")
                            {
                                grap.DrawString(textBox.Text, textBox.Font, new SolidBrush(textBox.ForeColor), new Point((int)(textBox.Left * times), (int)(textBox.Top * times)));
                                hasChanged = true;
                            }
                            this.frontPicture.Controls.Clear();
                            this.frontPicture.Refresh();
                            frontPicture.Cursor = Cursors.IBeam;
                        }
                        else
                        {
                            TextBox txtBox = new TextBox();
                            txtBox.Location = e.Location;
                            txtBox.ForeColor = this.drawingColor;
                            txtBox.BackColor = Color.White;
                            txtBox.Font = this.neuFontPicker1.SelectedFont;
                            txtBox.ImeMode = ImeMode.On;
                            txtBox.BorderStyle = BorderStyle.FixedSingle;
                            txtBox.KeyDown += new KeyEventHandler(this.Text_KeyDown);
                            this.frontPicture.Controls.Add(txtBox);
                            txtBox.Focus();
                            frontPicture.Cursor = Cursors.Default;
                        }
                        break;
                    case InkOverlayEditingMode.Select:
                        //ѡ��ģʽ��ȷ��ѡ���λ��
                        this.frontPicture.Controls.Clear();
                        pointStart = e.Location;
                        hasChanged = true;
                        break;
                    default:
                        break;
                }
            }
            pointStart = e.Location;

        }

        /// <summary>
        /// ��ͼͼ������ƶ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void pic_MouseMove(object sender, MouseEventArgs e)
        {
            Image img = this.frontPicture.Image;
            Graphics grap = Graphics.FromImage(img);
            if (e.Button == MouseButtons.Left)
            {
                switch (this.editingMode)
                {
                    case InkOverlayEditingMode.Ink:
                        //����ģʽ��Ǧ�ʻ�
                        grap.FillEllipse(new SolidBrush(Color.FromArgb(255, this.drawingColor)), (int)(e.X * times) - 1, (int)(e.Y * times) - 1, 2, 2);
                        grap.DrawLine(new Pen(Color.FromArgb(255, this.drawingColor), 2), (int)(pointStart.X * times), (int)(pointStart.Y * times), (int)(e.X * times), (int)(e.Y * times));
                        this.frontPicture.Refresh();
                        pointStart = e.Location;
                        hasChanged = true;
                        break;
                    case InkOverlayEditingMode.Delete:
                        if (this.eraserMode == InkOverlayEraserMode.PointErase)
                        {
                            //���ģʽ�����ͼƬ
                            if (pointStart != null)
                            {
                                grap.DrawLine(new Pen(Color.White, (int)(8 * times)), new Point((int)((pointStart.X + 4) * times), (int)((pointStart.Y + 4) * times)), new Point((int)((e.Location.X + 4) * times), (int)((e.Location.Y + 4) * times)));
                                hasChanged = true;
                                //this.SetBitmap((Bitmap)img);
                            }
                        }

                        this.frontPicture.Refresh();
                        pointStart = e.Location;
                        break;
                    case InkOverlayEditingMode.Select:
                        //ѡ��ģʽ���ı�ѡ����С
                        this.frontPicture.Controls.Clear();
                        PictureBox picSelect = new PictureBox();
                        picSelect.Location = new Point(System.Math.Min(pointStart.X, e.X), System.Math.Min(pointStart.Y, e.Y));
                        picSelect.Size = new Size(System.Math.Abs(pointStart.X - e.X), System.Math.Abs(pointStart.Y - e.Y));
                        picSelect.BackColor = Color.Transparent;
                        if (picSelect.Width > 1 && picSelect.Height > 1)
                        {
                            Image imgSelect = new Bitmap((int)(picSelect.Width * times), (int)(picSelect.Height * times));
                            Graphics grapSelect = Graphics.FromImage(imgSelect);
                            grapSelect.DrawRectangle(penDash, 0, 0, imgSelect.Width - 1, imgSelect.Height - 1);
                            picSelect.BorderStyle = BorderStyle.None;
                            picSelect.SizeMode = PictureBoxSizeMode.StretchImage;
                            picSelect.Image = imgSelect;
                        }
                        picSelect.Cursor = Cursors.Hand;
                        picSelect.MouseDown += new MouseEventHandler(picSelect_MouseDown);
                        picSelect.MouseUp += new MouseEventHandler(picSelect_MouseUp);

                        picSelect.MouseMove += new MouseEventHandler(picSelect_MouseMove);
                        this.frontPicture.Controls.Add(picSelect);
                        picSelect.SendToBack();
                        this.frontPicture.Refresh();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// ��ͼͼ�����̧���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void pic_MouseUp(object sender, MouseEventArgs e)
        {
            Image img = this.frontPicture.Image;
            Graphics grap = Graphics.FromImage(img);
            if (e.Button == MouseButtons.Left)
            {
                switch (this.editingMode)
                {
                    case InkOverlayEditingMode.Ink:
                        break;
                    case InkOverlayEditingMode.Delete:
                        if (this.eraserMode == InkOverlayEraserMode.PointErase)
                        {
                            //���ģʽ����������Χ
                            this.SetBitmap(this.frontPicture.Image as Bitmap, Color.White);
                            this.frontPicture.Refresh();
                        }
                        break;
                    case InkOverlayEditingMode.Select:
                        //ѡ��ģʽ��ȷ���ƶ���Χλ�����С
                        if (this.frontPicture.Controls.Count > 0)
                        {
                            PictureBox picSelect = this.frontPicture.Controls[0] as PictureBox;
                            if (picSelect != null && picSelect.Width != 0 && picSelect.Height != 0)
                            {
                                picSelect.Image = null;
                                pointStart = picSelect.Location;
                                imgSelect = new Bitmap((int)(picSelect.Width * times), (int)(picSelect.Height * times));
                                this.SetBitmap(imgSelect, img as Bitmap, new Point((int)(pointStart.X * times), (int)(pointStart.Y * times)));
                                picSelect.BorderStyle = BorderStyle.None;
                                picSelect.Location = new Point(0, 0);
                                picSelect.Size = frontPicture.Size;
                                picSelect.SizeMode = PictureBoxSizeMode.StretchImage;
                                picSelect.Image = new Bitmap(pageSize.Width, pageSize.Height);
                                Graphics grapSelect = Graphics.FromImage(picSelect.Image);
                                grapSelect.DrawImage(imgSelect, new Point((int)(pointStart.X * times), (int)(pointStart.Y * times)));
                                grapSelect.DrawRectangle(penDash, (int)((pointStart.X - 1) * times), (int)((pointStart.Y - 1) * times), imgSelect.Width + 2, imgSelect.Height + 2);
                                picSelect.Refresh();
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion ��ͼͼ���¼�

        #region ѡ����¼�
        /// <summary>
        /// ѡ�����갴���¼��������ѡ��Χ�ڣ������ѡ������ͼƬ������ѡ���ʧЧ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void picSelect_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X >= pointStart.X && e.X <= pointStart.X + imgSelect.Width / times && e.Y >= pointStart.Y && e.Y <= pointStart.Y + imgSelect.Height / times)
                {
                    if (Control.ModifierKeys != Keys.Control && !hasEraseOld)
                    {
                        Bitmap bmp = this.frontPicture.Image as Bitmap;
                        Control ctr = sender as Control;
                        Graphics grapTemp = Graphics.FromImage(this.frontPicture.Image);
                        grapTemp.FillRectangle(Brushes.White,  new Rectangle(new Point((int)(pointStart.X * times), (int)(pointStart.Y * times)), imgSelect.Size));
                        this.SetBitmap(bmp);
                        //this.SetBitmap(bmp, new Rectangle(pointStart, imgSelect.Size));
                        hasEraseOld = true;
                    }
                    else if (Control.ModifierKeys == Keys.Control)
                    {
                        Graphics grapInk = Graphics.FromImage(this.frontPicture.Image);
                        if (this.chkOverride.Checked)
                        {
                            this.SetBitmap((Bitmap)this.frontPicture.Image, new Rectangle((int)(pointStart.X * times), (int)(pointStart.Y * times), imgSelect.Width, imgSelect.Height)); //(pointStart.X + e.X - pointPosition.X, pointStart.Y + e.Y - pointPosition.Y, imgSelect.Width, imgSelect.Height));
                        }
                        grapInk.DrawImage(imgSelect, (int)(pointStart.X * times), (int)(pointStart.Y * times)); //new Point(pointStart.X + e.X - pointPosition.X, pointStart.Y + e.Y - pointPosition.Y));
                    }
                    pointPosition = e.Location;
                }
                else if (imgSelect != null && this.frontPicture.Controls.Count > 0)
                {
                    hasEraseOld = false;
                    Graphics grapInk = Graphics.FromImage(this.frontPicture.Image);
                    if (this.chkOverride.Checked)
                    {
                        this.SetBitmap((Bitmap)this.frontPicture.Image, new Rectangle((int)(pointStart.X * times), (int)(pointStart.Y * times), imgSelect.Width, imgSelect.Height)); //(pointStart.X + e.X - pointPosition.X, pointStart.Y + e.Y - pointPosition.Y, imgSelect.Width, imgSelect.Height));
                    }
                    grapInk.DrawImage(imgSelect, (int)(pointStart.X * times), (int)(pointStart.Y * times)); //new Point(pointStart.X + e.X - pointPosition.X, pointStart.Y + e.Y - pointPosition.Y));
                    this.frontPicture.Controls.Clear();
                }
                this.hasChanged = true;
            }
            frontPicture.Refresh();
        }

        /// <summary>
        /// ѡ������̧���¼��������ѡ��Χ���Ƴ������ƶ�ѡ�������ͼƬ���µ�λ��
        /// ����ѡ���ʧЧ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void picSelect_MouseUp(object sender, MouseEventArgs e)
        {
            pointStart = new Point(pointStart.X + e.X - pointPosition.X, pointStart.Y + e.Y - pointPosition.Y);
        }

        /// <summary>
        /// ѡ�����갴���¼��������ѡ��Χ�ڣ������ѡ������ͼƬ������ѡ���ʧЧ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void picSelect_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox picSelect = sender as PictureBox;
            if (e.Button == MouseButtons.Left)
            {
                Graphics grap = Graphics.FromImage(picSelect.Image);
                grap.Clear(Color.Transparent);
                if (this.chkOverride.Checked)
                {
                    grap.FillRectangle(new SolidBrush(Color.White), new Rectangle((int)((pointStart.X + e.X - pointPosition.X) * times), (int)((pointStart.Y + e.Y - pointPosition.Y) * times), imgSelect.Width, imgSelect.Height));
                }
                grap.DrawImage(imgSelect, new Point((int)((pointStart.X + e.X - pointPosition.X) * times), (int)((pointStart.Y + e.Y - pointPosition.Y) * times)));

                grap.DrawRectangle(penDash, (int)((pointStart.X + e.X - pointPosition.X) * times), (int)((pointStart.Y + e.Y - pointPosition.Y) * times), imgSelect.Width, imgSelect.Height);
                this.frontPicture.Refresh();
            }
            else
            {
                if (e.X >= pointStart.X && e.X <= pointStart.X + (int)(imgSelect.Width / times) && e.Y >= pointStart.Y && e.Y <= pointStart.Y + (int)(imgSelect.Height / times))
                {
                    picSelect.Cursor = Cursors.SizeAll;
                }
                else
                {
                    picSelect.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// ch
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Delete)
            {
                if (this.editingMode == InkOverlayEditingMode.Select && imgSelect != null && this.frontPicture.Controls.Count > 0)
                {
                    //this.SetBitmap((Bitmap)this.frontPicture.Image, new Rectangle(pointStart.X, pointStart.Y, imgSelect.Width, imgSelect.Height)); //(pointStart.X + e.X - pointPosition.X, pointStart.Y + e.Y - pointPosition.Y, imgSelect.Width, imgSelect.Height));
                    Graphics grapTemp = Graphics.FromImage(this.frontPicture.Image);
                    grapTemp.FillRectangle(Brushes.White, new Rectangle(pointStart.X, pointStart.Y, imgSelect.Width, imgSelect.Height));
                    this.SetBitmap((Bitmap)this.frontPicture.Image);
                    this.frontPicture.Controls.Clear();
                    this.hasEraseOld = false;
                    this.hasChanged = true;
                }
                else if (this.editingMode == InkOverlayEditingMode.Text)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
            }
            else if (keyData == Keys.Escape)
            {
                this.frontPicture.Controls.Clear();
                this.hasEraseOld = false;
            }
            else if (this.editingMode == InkOverlayEditingMode.Text)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            return true;
            //return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion ѡ����¼�

        #region �ı����¼�
        private void Text_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (e.KeyCode == Keys.Enter)
            {
                //�س��ύ�ı�
                if (textBox != null && textBox.Text != "")
                {
                    Graphics grap = Graphics.FromImage(this.frontPicture.Image);
                    grap.DrawString(textBox.Text, textBox.Font, new SolidBrush(textBox.ForeColor), (int)(textBox.Left * times), (int)(textBox.Top * times));
                }
                sender = null;
                this.frontPicture.Controls.Clear();
                //this.frontPicture.Refresh();
            }
            else if (e.Control && e.KeyCode == Keys.Left)
            {
                textBox.Left = textBox.Left - 1;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Right)
            {
                textBox.Left = textBox.Left + 1;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Up)
            {
                textBox.Top = textBox.Top - 1;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {
                textBox.Top = textBox.Top + 1;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                sender = null;
                this.frontPicture.Controls.Clear();
                this.frontPicture.Refresh();
            }
        }
        #endregion �ı����¼�

        #endregion "�¼����¼�������"

        protected override void Page_SelectedChanged()
        {
            //// 
            //// frontPicture
            //// 
            //this.frontPicture = new PictureBox();
            //this.frontPicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            //this.frontPicture.Location = new System.Drawing.Point(0, 0);
            //this.frontPicture.Dock = DockStyle.Fill;
            //this.frontPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frontPicture_MouseMove);
            //this.frontPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frontPicture_MouseDown);
            //this.frontPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frontPicture_MouseUp);

            //PictureBox pic = (PictureBox)this.Panel2.Controls[0];
            //pic.Controls.Add(this.frontPicture);
            //Bitmap bmp = new Bitmap(this.frontPicture.Width, this.frontPicture.Height);
            //this.SetBitmap(bmp);
            //this.frontPicture.Image = bmp;
        }
    }
    public enum InkOverlayEditingMode
    {
        [Description("InkOverlayEditingModeDelete ɾ��")]
        Delete = 1,
        [Description("InkOverlayEditingModeInk ����")]
        Ink = 0,
        [Description("InkOverlayEditingModeSelect ѡ��")]
        Select = 2,
        [Description("InkOverlayEditingModeText �ı�")]
        Text = 3
    }

    [Description("InkOverlayEraserMode")]
    public enum InkOverlayEraserMode
    {
        [Description("InkOverlayEraserModePointErase")]
        PointErase = 1,
        [Description("InkOverlayEraserModeStrokeErase")]
        StrokeErase = 0
    }
}
