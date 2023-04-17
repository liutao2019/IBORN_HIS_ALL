using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HISFC.Components.Package.Components
{
    public partial class FsPanel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
             
        public FsPanel()
        {
            InitializeComponent();
            this.Paint += new System.Windows.Forms.PaintEventHandler(FsPanel_Paint);
        }

        private void FsPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle rt1 = new Rectangle(0, 0, this.Width, (this.Height));
            LinearGradientBrush br = new LinearGradientBrush(rt1, Color.FromArgb(212, 229, 251), Color.FromArgb(133, 172, 227), LinearGradientMode.Vertical);
            Graphics g = e.Graphics;//为控件创建Graphics
            g.FillRectangle(br, rt1);//填充矩形

            //实现圆角形状的panel
            //List<Point> list = new List<Point>();//建立点集合list
            //int width = this.pnlTop.Width;
            //int height = this.pnlTop.Height;
            ////panel左上的点
            //list.Add(new Point(0, 0));
            ////panel右上的点
            //list.Add(new Point(width,0));
            ////panel右下角的点
            //list.Add(new Point(width, height));
            ////panel左下角的点
            //list.Add(new Point(0, height));
            //Point[] points = list.ToArray();//将以上圆角形状的点集合转换成点数组
            //GraphicsPath shape = new GraphicsPath();//新建绘图路径对象
            //shape.AddPolygon(points);
            ////将窗体的显示区域设为GraphicsPath的实例
            //this.pnlTop.Region = new System.Drawing.Region(shape);
        }

        public FsPanel(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }

    public enum BorderLine
    {
        None   = 0,   // 无
        Top    = 1,   // 上
        Right  = 2,   // 右
        Bottom = 4,   // 下
        Left   = 8    // 左
    }
}
