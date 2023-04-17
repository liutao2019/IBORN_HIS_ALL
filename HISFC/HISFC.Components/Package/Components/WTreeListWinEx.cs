using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HISFC.Components.Package.Components
{
    public class WTreeListWinEx : TreeView
    {
        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler WMScroll;
        public event EventHandler SelectedIndexChanged;

        protected void OnWMScroll(object sender, EventArgs e)
        {
            if (WMScroll != null)
                WMScroll(sender, e);
        }

        ////不是相同的节点才算
        protected void OnSeletedIndexChaned(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(sender, e);
        }


        public WTreeListWinEx()
        {
            // Enable default double buffering processing
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            // Disable default CommCtrl painting on non-Vista systems
            //xp中画字体会有问题
            //if (!NativeInterop.IsWinVista)
            //    SetStyle(ControlStyles.UserPaint, true);

            this.InitializeComponent();
            this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WTreeListWinEx_MouseClick);
            this.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.WTreeListWinEx_DrawNode);
            this.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.WTreeListWinEx_AfterSelect);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WTreeListWinEx_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WTreeListWinEx_MouseDown);
        }
        
        #region 节点重绘
        /*1节点被选中 ,TreeView有焦点*/
        private SolidBrush brush1 = new SolidBrush(Color.FromArgb(209, 232, 255));
        private PictureBox plusPictureBox2;
        private PictureBox minusPictureBox2;
        private PictureBox minusPictureBox1;
        private PictureBox plusPictureBox1;//填充颜色
        private Pen pen1 = new Pen(Color.FromArgb(102, 167, 232), 1);//边框颜色

        /*2节点被选中 ,TreeView没有焦点*/
        private SolidBrush brush2 = new SolidBrush(Color.FromArgb(247, 247, 247));
        private Pen pen2 = new Pen(Color.FromArgb(222, 222, 222), 1);

        /*3 MouseMove的时候 画光标所在的节点的背景*/
        private SolidBrush brush3 = new SolidBrush(Color.FromArgb(229, 243, 251));
        private Pen pen3 = new Pen(Color.FromArgb(112, 192, 231), 1);

        Point pt;
        private ImageList arrowImageList;
        private System.ComponentModel.IContainer components;
        private ImageList imageList1;
        TreeNode treeNode1;
        #endregion

        private void WTreeListWinEx_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            #region 1     选中的节点背景=========================================
            Rectangle nodeRect = new Rectangle(1,
                                                e.Bounds.Top,
                                                e.Bounds.Width - 3,
                                                e.Bounds.Height - 1);

            if (e.Node.IsSelected)
            {
                //TreeView有焦点的时候 画选中的节点
                if (this.Focused)
                {
                    e.Graphics.FillRectangle(brush1, nodeRect);
                    e.Graphics.DrawRectangle(pen1, nodeRect);

                    /*测试 画聚焦的边框*/
                    //ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, Color.Black, SystemColors.Highlight);
                }
                /*TreeView失去焦点的时候 */
                else
                {
                    e.Graphics.FillRectangle(brush2, nodeRect);
                    e.Graphics.DrawRectangle(pen2, nodeRect);
                }
            }
            else if ((e.State & TreeNodeStates.Hot) != 0 && e.Node.Text != "")//|| currentMouseMoveNode == e.Node)
            {
                e.Graphics.FillRectangle(brush3, nodeRect);
                e.Graphics.DrawRectangle(pen3, nodeRect);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds);
            }
            #endregion

            #region 2     +-号绘制=========================================
            Rectangle plusRect = new Rectangle(e.Node.Bounds.Left - 32, nodeRect.Top + 7, 9, 9); // +-号的大小 是9 * 9
            if (e.Node.IsExpanded && e.Node.Nodes.Count > 0)
            {
                //treeView1.Refresh();
                for (int i = 0; i < this.Nodes.Count; i++)
                    e.Graphics.DrawImage(minusPictureBox1.Image, plusRect);
            }
            else if (e.Node.IsExpanded == false && e.Node.Nodes.Count > 0)
            {
                //treeView1.Refresh();
                for (int i = 0; i < this.Nodes.Count; i++)
                    e.Graphics.DrawImage(plusPictureBox1.Image, plusRect);
            }

            TreeViewHitTestInfo info = this.HitTest(pt);
            if (treeNode1 != null && info.Location == TreeViewHitTestLocations.PlusMinus && treeNode1 == e.Node)
            {
                if (treeNode1.IsExpanded && treeNode1.Nodes.Count > 0)
                    e.Graphics.DrawImage(minusPictureBox2.Image, plusRect);
                else if (treeNode1.IsExpanded == false && treeNode1.Nodes.Count > 0)
                    e.Graphics.DrawImage(plusPictureBox2.Image, plusRect);
            }


            /*测试用 画出+-号出现的矩形*/
            //if (e.Node.Nodes.Count > 0)
            //    e.Graphics.DrawRectangle(new Pen(Color.Red), plusRect);
            #endregion

            #region 3     画节点文本=========================================
            Rectangle nodeTextRect = new Rectangle(
                                                    e.Node.Bounds.Left,
                                                    e.Node.Bounds.Top + 2,
                                                    e.Node.Bounds.Width + 2,
                                                    e.Node.Bounds.Height
                                                    );
            nodeTextRect.Width += 4;
            nodeTextRect.Height -= 4;

            e.Graphics.DrawString(e.Node.Text,
                                  e.Node.TreeView.Font,
                                  new SolidBrush(e.Node.ForeColor.IsEmpty ? this.ForeColor : e.Node.ForeColor),
                                  nodeTextRect);


            #region
            ////画子节点上文件的个数 (111)
            //if (e.Node.Text != "" &&
            //    Directory.Exists(rootpath + "\\" + e.Node.FullPath))
            //{
            //    string[] filelist = Directory.GetFiles(rootpath + "\\" + e.Node.FullPath, "*.htm");
            //    nodeTextRect.Width += 4;
            //    nodeTextRect.Height -= 4;
            //    //画子节点个数 (111)
            //    if (e.Node.Text != "" && filelist.Length > 0)
            //    {
            //        e.Graphics.DrawString(string.Format("({0})", filelist.Length),
            //                                new Font("Arial", 8),
            //                                Brushes.Gray,
            //                                nodeTextRect.Right - 4,
            //                                nodeTextRect.Top + 2);
            //    }
            //}
            #endregion

            Rectangle r = e.Node.Bounds;
            r.Height -= 2;
            ///*测试用，画文字出现的矩形*/
            //if (e.Node.Text != "")
            //    e.Graphics.DrawRectangle(new Pen(Color.Blue), r);
            #endregion

            #region 4   画IImageList 中的图标===================================================================

            int currt_X = e.Node.Bounds.X;
            if (this.ImageList != null && this.ImageList.Images.Count > 0)
            {
                //图标大小16*16
                Rectangle imagebox = new Rectangle(
                    e.Node.Bounds.X - 3 - 16,
                    e.Node.Bounds.Y + 3,
                    16,//IMAGELIST IMAGE WIDTH
                    16);//HEIGHT


                int index = e.Node.ImageIndex;
                string imagekey = e.Node.ImageKey;
                if (imagekey != "" && this.ImageList.Images.ContainsKey(imagekey))
                    e.Graphics.DrawImage(this.ImageList.Images[imagekey], imagebox);
                else
                {
                    if (e.Node.ImageIndex < 0)
                        index = 0;
                    else if (index > this.ImageList.Images.Count - 1)
                        index = 0;
                    e.Graphics.DrawImage(this.ImageList.Images[index], imagebox);
                }
                currt_X -= 19;

                /*测试 画IMAGELIST的矩形*/
                //if (e.Node.ImageIndex > 0)
                //    e.Graphics.DrawRectangle(new Pen(Color.Black, 1), imagebox);
            }
            #endregion

            #region 测试 画所有的边框
            //nodeRect = new Rectangle(1,
            //             e.Bounds.Top + 1,
            //             e.Bounds.Width - 3,
            //             e.Bounds.Height - 2);
            //e.Graphics.DrawRectangle(new Pen(Color.Gray), nodeRect);
            #endregion
        }

        private void WTreeListWinEx_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnSeletedIndexChaned(sender, e);
            //能响应按上下键
            this.Select();
        }

        private void WTreeListWinEx_MouseClick(object sender, MouseEventArgs e)
        {
            //TreeNode td = this.GetNodeAt(e.X, e.Y);
            //selpath = root + "\\" + e.Node.FullPath;

            //if ((td != null) &&
            //    (td.Text != "") &&
            //    (td != computerNode) &&
            //    (td != documentNode) &&
            //    (td != recyclebinNode)
            //    )
            //    //节点 = 文件夹
            //    treeView1.ContextMenuStrip = 文件夹contextMenuStrip;
            //else
            //    treeView1.ContextMenuStrip = 空白contextMenuStrip;

            //这是为了响应快捷键
            //其他主要节点手动设置
        }

        private void WTreeListWinEx_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode tn = this.GetNodeAt(e.X, e.Y);
            if (tn != null)
                this.SelectedNode = tn;

            //if (e.Button == MouseButtons.Right)
            //{
                //selpath = root + "\\" + e.Node.FullPath;

                //if ((td != null) &&
                //    (td.Text != "") &&
                //    (td != computerNode) &&
                //    (td != documentNode) &&
                //    (td != recyclebinNode)
                //    )
                //    //节点 = 文件夹
                //    treeView1.ContextMenuStrip = 文件夹contextMenuStrip;
                //else
                //    treeView1.ContextMenuStrip = 空白contextMenuStrip;
            //} 
        }

        private void WTreeListWinEx_MouseMove(object sender, MouseEventArgs e)
        {
            pt = e.Location;
            treeNode1 = this.GetNodeAt(e.Location);

            if (treeNode1 == null || treeNode1.Text == "")
            {
                this.Cursor = Cursors.Arrow;
                return;
            }

            //改变光标成小手
            TreeViewHitTestInfo info = this.HitTest(e.Location);
            if (info.Location == TreeViewHitTestLocations.Image || info.Location == TreeViewHitTestLocations.Label)
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        #region 双缓存重绘


        protected void UpdateExtendedStyles()
        {
            int Style = 0;

            if (DoubleBuffered)
                Style |= TVS_EX_DOUBLEBUFFER;

            if (Style != 0)
                HISFC.Components.Package.Components.NativeInterop.SendMessage(Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)Style);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateExtendedStyles();
            if (!HISFC.Components.Package.Components.NativeInterop.IsWinXP)
                HISFC.Components.Package.Components.NativeInterop.SendMessage(Handle, TVM_SETBKCOLOR, IntPtr.Zero, (IntPtr)System.Drawing.ColorTranslator.ToWin32(BackColor));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint))
            {
                Message m = new Message();
                m.HWnd = Handle;
                m.Msg = HISFC.Components.Package.Components.NativeInterop.WM_PRINTCLIENT;
                m.WParam = e.Graphics.GetHdc();
                m.LParam = (IntPtr)HISFC.Components.Package.Components.NativeInterop.PRF_CLIENT;
                DefWndProc(ref m);
                e.Graphics.ReleaseHdc(m.WParam);
            }
            base.OnPaint(e);
        }
        #endregion

        protected override void WndProc(ref Message m)
        {
            if (HISFC.Components.Package.Components.Win32API.IsHorizontalScrollBarVisible(this))
                HISFC.Components.Package.Components.Win32API.ShowScrollBar(this.Handle, 0, false);

            if (m.Msg == WM_VSCROLL || m.Msg == WM_HSCROLL || m.Msg == WM_MOUSEWHEEL)
                OnWMScroll(new object(), new EventArgs());
            base.WndProc(ref m);
        }



        private const int WM_VSCROLL = 0x0115;
        private const int WM_HSCROLL = 0x0114;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETBKCOLOR = TV_FIRST + 29;
        private const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WTreeListWinEx));
            this.arrowImageList = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.plusPictureBox2 = new System.Windows.Forms.PictureBox();
            this.minusPictureBox2 = new System.Windows.Forms.PictureBox();
            this.minusPictureBox1 = new System.Windows.Forms.PictureBox();
            this.plusPictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.plusPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minusPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minusPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plusPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // arrowImageList
            // 
            this.arrowImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("arrowImageList.ImageStream")));
            this.arrowImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.arrowImageList.Images.SetKeyName(0, "1.png");
            this.arrowImageList.Images.SetKeyName(1, "2.png");
            this.arrowImageList.Images.SetKeyName(2, "3.png");
            this.arrowImageList.Images.SetKeyName(3, "4.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "空.png");
            this.imageList1.Images.SetKeyName(2, "我的电脑.png");
            this.imageList1.Images.SetKeyName(3, "文档.png");
            this.imageList1.Images.SetKeyName(4, "桌面1.png");
            this.imageList1.Images.SetKeyName(5, "null.ico");
            this.imageList1.Images.SetKeyName(6, "回收站.png");
            this.imageList1.Images.SetKeyName(7, "txt1.png");
            this.imageList1.Images.SetKeyName(8, "ii.png");
            // 
            // plusPictureBox2
            // 
            this.plusPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("plusPictureBox2.Image")));
            this.plusPictureBox2.Location = new System.Drawing.Point(247, 65);
            this.plusPictureBox2.Name = "plusPictureBox2";
            this.plusPictureBox2.Size = new System.Drawing.Size(9, 9);
            this.plusPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.plusPictureBox2.TabIndex = 10;
            this.plusPictureBox2.TabStop = false;
            this.plusPictureBox2.Visible = false;
            // 
            // minusPictureBox2
            // 
            this.minusPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("minusPictureBox2.Image")));
            this.minusPictureBox2.Location = new System.Drawing.Point(247, 80);
            this.minusPictureBox2.Name = "minusPictureBox2";
            this.minusPictureBox2.Size = new System.Drawing.Size(9, 9);
            this.minusPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minusPictureBox2.TabIndex = 11;
            this.minusPictureBox2.TabStop = false;
            this.minusPictureBox2.Visible = false;
            // 
            // minusPictureBox1
            // 
            this.minusPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("minusPictureBox1.Image")));
            this.minusPictureBox1.Location = new System.Drawing.Point(221, 80);
            this.minusPictureBox1.Name = "minusPictureBox1";
            this.minusPictureBox1.Size = new System.Drawing.Size(9, 9);
            this.minusPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minusPictureBox1.TabIndex = 12;
            this.minusPictureBox1.TabStop = false;
            this.minusPictureBox1.Visible = false;
            // 
            // plusPictureBox1
            // 
            this.plusPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("plusPictureBox1.Image")));
            this.plusPictureBox1.Location = new System.Drawing.Point(221, 65);
            this.plusPictureBox1.Name = "plusPictureBox1";
            this.plusPictureBox1.Size = new System.Drawing.Size(9, 9);
            this.plusPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.plusPictureBox1.TabIndex = 13;
            this.plusPictureBox1.TabStop = false;
            this.plusPictureBox1.Visible = false;
            // 
            // WTreeListWinEx
            // 
            this.ImageIndex = 0;
            this.ImageList = this.imageList1;
            this.LineColor = System.Drawing.Color.Black;
            this.SelectedImageIndex = 0;
            ((System.ComponentModel.ISupportInitialize)(this.plusPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minusPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minusPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plusPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
