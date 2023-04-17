using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;

namespace FS.SOC.HISFC.Components.Report.Common
{
    /// <summary>
    /// [功能描述: 公共函数]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-08]<br></br>
    /// </summary>
    public partial class ucCustomReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCustomReport()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            foreach (Control c in this.neuPanelDesign.Controls)
            {
                c.MouseDown += new MouseEventHandler(c_MouseDown);
                c.MouseMove += new MouseEventHandler(c_MouseMove);
                c.MouseUp += new MouseEventHandler(c_MouseUp);                
            }
            //foreach (Control cc in this.neuPanelControl.Controls)
            //{
            //    cc.MouseMove+=new MouseEventHandler(cc_MouseMove);
            //    cc.MouseLeave += new EventHandler(cc_MouseLeave);
            //    cc.DoubleClick += new EventHandler(cc_DoubleClick);
            //}
            base.OnLoad(e);

            //System.ComponentModel.Design.DesignSurfaceManager dsm = new System.ComponentModel.Design.DesignSurfaceManager();
            //System.ComponentModel.Design.DesignSurface ds = dsm.CreateDesignSurface();
            //ds.BeginLoad(typeof(Form));
            //dsm.ActiveDesignSurface = ds;
            //Control uc = (Control)ds.View;
            //uc.Text = "new";
            //uc.Name = "new";
            //uc.Dock = DockStyle.Fill;
            //this.propertyGrid1.SelectedObject = uc;
            //this.neuPanelDesign.Controls.Add(uc);

            //ds = dsm.CreateDesignSurface();
            //ds.BeginLoad(typeof(TextBox));
            //dsm.ActiveDesignSurface = ds;
            
            //Control ct = (Control)ds.View;
            //ct.Text = "new";
            //ct.Name = "new";
            //ct.Size = new Size(100, 200);
            //ct.Parent = uc;
            //this.propertyGrid1.SelectedObject = ct;
        }

        void cc_DoubleClick(object sender, EventArgs e)
        {
            //FS.FrameWork.WinForms.Controls.NeuLabel neuLable = new FS.FrameWork.WinForms.Controls.NeuLabel();
            //neuLable.Name = "neuLable";
            //neuLable.Text = "neuLable";
            //neuLable.Location = new Point(this.neuPanelDesign.Size.Width / 2, this.neuPanelDesign.Size.Height / 2);
            //neuLable.MouseDown += new MouseEventHandler(c_MouseDown);
            //neuLable.MouseMove += new MouseEventHandler(c_MouseMove);
            //neuLable.MouseUp += new MouseEventHandler(c_MouseUp);    
            //this.neuPanelDesign.Controls.Add(neuLable);

            //System.ComponentModel.Design.DesignSurfaceManager dsm = new System.ComponentModel.Design.DesignSurfaceManager();
            //System.ComponentModel.Design.DesignSurface ds = dsm.CreateDesignSurface();
            //ds.BeginLoad(typeof(TextBox));
            //dsm.ActiveDesignSurface = ds;
            //Control c = (Control)ds.View;
            //c.Text = "new";
            //c.Name = "new";
            //c.Size = new Size(100, 200);
            //c.Parent = this.neuPanelDesign.Controls[0];
            //this.propertyGrid1.SelectedObject = c;
        }

        void cc_MouseLeave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = System.Drawing.Color.LightCyan;
        }

        void cc_MouseMove(object sender, MouseEventArgs e)
        {
            ((Control)sender).BackColor = System.Drawing.Color.LightBlue;
        }

        void c_MouseUp(object sender, MouseEventArgs e)
        {
            xLocation = 0;
            yLocation = 0;
            xSize = 0;
            ySize = 0;
            if (((Control)sender).Parent != null)
            {
                foreach (Control c in ((Control)sender).Parent.Controls)
                {
                    c.Enabled = true;
                }
            }
            this.DrawGrid(this.neuPanelDesign);
        }

        int xLocation, yLocation, xSize, ySize, with, height;

        void c_MouseMove(object sender, MouseEventArgs e)
        {          
            if (e.Button == MouseButtons.Left)
            {
              
                if (((Control)sender).Cursor == System.Windows.Forms.Cursors.SizeAll)
                {
                    ((Control)sender).Location = new Point(((Control)sender).Location.X + e.X - xLocation, ((Control)sender).Location.Y + e.Y - yLocation);
                }
                else if (((Control)sender).Cursor == System.Windows.Forms.Cursors.SizeWE)
                {
                    ((Control)sender).Size = new Size(with + e.X - xSize, ((Control)sender).Size.Height);
                }
                else if (((Control)sender).Cursor == System.Windows.Forms.Cursors.SizeNS)
                {
                    ((Control)sender).Size = new Size(((Control)sender).Size.Width, height + e.Y - ySize);
                }
                return ;
            }
          
            if (e.X < ((Control)sender).Size.Width && e.X > ((Control)sender).Size.Width - ((Control)sender).Margin.Left - ((Control)sender).Margin.Right - 2)
            {
                ((Control)sender).Cursor = System.Windows.Forms.Cursors.SizeWE;

            }
            else if (e.Y < ((Control)sender).Size.Height
                && e.Y > ((Control)sender).Size.Height - ((Control)sender).Margin.Top - ((Control)sender).Margin.Bottom
                && e.X < ((Control)sender).Size.Width - ((Control)sender).Margin.Left - ((Control)sender).Margin.Right - 2
                )
            {
                ((Control)sender).Cursor = System.Windows.Forms.Cursors.SizeNS;
            }
            else
            {
                ((Control)sender).Cursor = System.Windows.Forms.Cursors.SizeAll;
            }
        }

      

        private void c_MouseDown(object sender, MouseEventArgs e)
        {
            xLocation = e.X;
            yLocation = e.Y;
            xSize = e.X;
            ySize = e.Y;
            with = ((Control)sender).Width;
            height = ((Control)sender).Height;
            if (((Control)sender).Parent != null)
            {
                foreach (Control c in ((Control)sender).Parent.Controls)
                {
                    if (c.Name != ((Control)sender).Name)
                    {
                        c.Enabled = false;
                    }
                }
            }

            this.propertyGrid1.SelectedObject = sender;
        }

        #region 绘制网格

        /// <summary>
        /// 绘制网格
        /// </summary>
        /// <param name="c"></param>
        private void DrawGrid(Control control)
        {

            Graphics graphics = control.CreateGraphics();
            Pen pen = new Pen(Color.LightGray);

            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            //横线：从左上角到左下角
            for (int y = 0; y < control.Height; y = y + 8)
            {
                graphics.DrawLine(pen, 0, y, control.Width, y);

            }

            //纵线：从左上角到右上角
            for (int x = 0; x < control.Width; x = x + 8)
            {
                graphics.DrawLine(pen, x, 0, x, control.Height);
            }

        }

        #endregion

        #region 绘制选中框
        private void DrawControlSelectedGrid(Control control)
        {
            //选择控件,不使用默认窗口
            //if (propertyform != null) propertyform.isUseDefault = false;
            if (control.Parent == null) return;

            Rectangle[] rect = new Rectangle[4];
            int PointWidth = 5;
            Graphics g;
            Pen pen = new Pen(Color.Blue);
            SolidBrush myBrush = new SolidBrush(Color.Blue);

            control.Parent.Refresh();
            this.DrawGrid(control.Parent);

            g = control.Parent.CreateGraphics();
            rect[0].Location = new System.Drawing.Point(control.Left - PointWidth, control.Top - PointWidth);
            rect[0].Size = new System.Drawing.Size(PointWidth, PointWidth);

            rect[1].Location = new System.Drawing.Point(control.Left + control.Width, control.Top - PointWidth);
            rect[1].Size = new System.Drawing.Size(PointWidth, PointWidth);

            rect[2].Location = new System.Drawing.Point(control.Left - PointWidth, control.Top + control.Height);
            rect[2].Size = new System.Drawing.Size(PointWidth, PointWidth);

            rect[3].Location = new System.Drawing.Point(control.Left + control.Width, control.Top + control.Height);
            rect[3].Size = new System.Drawing.Size(PointWidth, PointWidth);


            for (int i = 0; i <= 3; i++)
            {
                g.FillRectangle(myBrush, rect[i]);
            }
            Pen pen1 = new Pen(Color.Blue);
            //pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            g.DrawLine(pen1, control.Left, control.Top - 2, control.Right, control.Top - 2);
            g.DrawLine(pen1, control.Left, control.Bottom + 2, control.Right, control.Bottom + 2);
            g.DrawLine(pen1, control.Left - 2, control.Top, control.Left - 2, control.Bottom);
            g.DrawLine(pen1, control.Right + 2, control.Top, control.Right + 2, control.Bottom);

        }
        #endregion

    }
}