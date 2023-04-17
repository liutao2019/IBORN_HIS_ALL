using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Neusoft.UFC.Privilege.Forms
{	
    /// <summary>
    /// [��������: 	
    /// ���ര�� created by wolf 2004-6-21
    /// ���д��ڵĻ���
    /// 1��ʵ�����Թ��ʻ�
    /// 2��ʵ��״̬��
    /// <br></br>
    /// [�� �� ��: huangxw]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
	public class BaseForm : System.Windows.Forms.Form
    {
        protected StatusStrip MainStatusStrip;
        private Timer timer1;
        private ToolStripStatusLabel statusLabel1;
        private ToolStripStatusLabel statusLabel2;
        private ToolStripStatusLabel statusLabel3;
        private IContainer components;
		
		/// <summary>
		/// 
		/// </summary>
		public BaseForm()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

            //����״̬��
            this.SetStatusStrip();

            //��ʱ��ˢ��ʱ��
            this.timer1.Interval = 1000;
            this.timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Enabled = true;            
		}

        public bool IsStatusStripVisible
        {
            get
            {
                return this.MainStatusStrip.Visible;
            }
            set
            {
                this.MainStatusStrip.Visible = value;
            }
        }


		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );			
		}
		
		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel3,
            this.statusLabel1,
            this.statusLabel2});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 341);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(478, 22);
            this.MainStatusStrip.TabIndex = 0;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // statusLabel3
            // 
            this.statusLabel3.AutoSize = false;
            this.statusLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel3.Name = "statusLabel3";
            this.statusLabel3.Size = new System.Drawing.Size(103, 17);
            this.statusLabel3.Spring = true;
            this.statusLabel3.Text = "����";
            this.statusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabel1
            // 
            this.statusLabel1.AutoSize = false;
            this.statusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.statusLabel1.Size = new System.Drawing.Size(130, 17);
            this.statusLabel1.Text = "����Ա: ";
            this.statusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabel2
            // 
            this.statusLabel2.AutoSize = false;
            this.statusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel2.Name = "statusLabel2";
            this.statusLabel2.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.statusLabel2.Size = new System.Drawing.Size(230, 17);
            this.statusLabel2.Text = "ʱ��: yyyyniddniddni hhnimmnissni";
            this.statusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(478, 363);
            this.Controls.Add(this.MainStatusStrip);
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBase";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBase_Load);
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
              
		private void frmBase_Load(object sender, System.EventArgs e)
		{   
            //���Թ��ʻ�
            this.ChangeControlLanguage(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            SetToolBar();

            base.OnLoad(e);
        }

        #region ���Թ��ʻ�
        /// <summary>
        /// ת���ؼ���ʾ�ı�
        /// </summary>
        /// <param name="control"></param>
        public void ChangeControlLanguage(object control)
        {            
            //if (Neusoft.NFC.Management.Language.IsUseLanguage == false) return;   

            this.ReplaceText(control);

            Control c = control as Control;
            if (c != null && c.Controls.Count>0)
            {
                foreach (Control c1 in c.Controls)
                {
                    this.ChangeControlLanguage(c1);
                }
            }
        }

        /// <summary>
        /// �滻�ı�
        /// </summary>
        protected void ReplaceText(object c)
        {
            try
            {
                if (c.GetType().IsSubclassOf(typeof(TabPage)) 
                    || c.GetType().IsSubclassOf(typeof(Label))
                    || c.GetType().IsSubclassOf(typeof(ButtonBase))
                    || c.GetType() == typeof(TabPage)
                    || c.GetType() == typeof(Label))
                {
                    Control control = c as Control;
                    //control.Text = Neusoft.NFC.Management.Language.Msg(control.Text);

                }
                else if (c.GetType().IsSubclassOf(typeof(ToolBar)) || c.GetType() == typeof(ToolBar))
                {
                    ToolBar tb = c as ToolBar;
                    foreach (ToolBarButton button in tb.Controls)
                    {
                        //button.Text = Neusoft.NFC.Management.Language.Msg(button.Text);
                        //button.ToolTipText = Neusoft.NFC.Management.Language.Msg(button.ToolTipText);
                    }
                }
                else if (c.GetType().IsSubclassOf(typeof(ToolStrip)) || c.GetType() == typeof(ToolStrip))
                {
                    ToolStrip ts = c as ToolStrip;
                    foreach (ToolStripItem button in ts.Items)
                    {
                        //button.Text = Neusoft.NFC.Management.Language.Msg(button.Text);
                        //button.ToolTipText = Neusoft.NFC.Management.Language.Msg(button.ToolTipText);
                    }
                }
                //else if (c.GetType().IsSubclassOf(typeof(FarPoint.Win.Spread.FpSpread)) || c.GetType() == typeof(FarPoint.Win.Spread.FpSpread))
                //{
                //    FarPoint.Win.Spread.FpSpread fp = c as FarPoint.Win.Spread.FpSpread;
                //    foreach (FarPoint.Win.Spread.SheetView sv in fp.Sheets)
                //    {
                //        foreach (FarPoint.Win.Spread.Column column in sv.Columns)
                //        {
                //            column.Label = Neusoft.NFC.Management.Language.Msg(column.Label);
                //        }
                //    }
                //}
            }
            catch { }

        }
        #endregion

        #region ״̬��
        /// <summary>
        /// ����״̬��
        /// </summary>
        protected void SetStatusStrip()
        {
            statusLabel3.Text = "����";                          

            statusLabel1.Text = "����Ա: " + "test";
            statusLabel1.Width = 130;

            statusLabel2.Text = "ʱ��: " + DateTime.Now.ToString("yyyy��MM��dd�� HHʱmm��ss��");
            statusLabel2.Width = 230;
        }

        /// <summary>
        /// ����״̬����Ϣ
        /// </summary>
        /// <param name="msg"></param>
        public void SetStatusMsg(string msg)
        {
            this.SetStatusMsg(msg, Color.Black);
        }

        /// <summary>
        /// ����״̬����Ϣ
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="fontColor"></param>
        public void SetStatusMsg(string msg, Color fontColor)
        {
            statusLabel3.Text = msg;
            statusLabel3.ForeColor = fontColor;
        }

        public void SetStatusOper(string operName)
        {
            statusLabel1.Text = "����Ա: " + operName;
        }

        public void SetStatusDate(DateTime operDate)
        {
            statusLabel2.Text = "ʱ��: " + operDate.ToString("yyyy��MM��dd�� HHʱmm��ss��");
        }

        /// <summary>
        /// ���ò���Ա�Ͳ���ʱ��״̬�����ɼ�
        /// </summary>
        public void SetOperAndDateInvisible()
        {
            this.statusLabel1.Visible = false;
            this.statusLabel2.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            statusLabel2.Text = "ʱ��: " + DateTime.Now.ToString("yyyy��MM��dd�� HHʱmm��ss��");
        }
        
        #endregion

        #region ToolBarService

        protected IToolBarService MyToolBarService = null;
        
        public string CurrentPath = "";
        public string PluginPath = "Plugins\\";

        protected virtual int SetToolBar(string filename)
        {
            filename = CurrentPath + PluginPath + "TOOLBAR" +
                "\\" + filename + ".dll";

            if (!System.IO.File.Exists(filename)) return 0;

            if (this.LoadDll(filename) == 0)
            {
                this.GetToolBar(this);
            }

            return 0;
        }

        protected virtual int SetToolBar()
        {
            string fileName = this.Name;
            return this.SetToolBar(fileName);
        }

        protected void GetToolBar(Control parentControl)
        {
            foreach (Control c in parentControl.Controls)
            {
                if (c.GetType() == typeof(System.Windows.Forms.ToolStrip))
                {
                    if (c.Visible)
                    {
                        this.MyToolBarService.Init((ToolStrip)c);
                        return;
                    }
                }
                if (c.Controls.Count > 0) this.GetToolBar(c);
            }
        }

        protected virtual int LoadDll(String fileName)
        {
            try
            {
                Assembly a = Assembly.LoadFrom(fileName);
                System.Type[] types = a.GetTypes();
                foreach (System.Type type in types)
                {
                    if (type.GetInterface("IToolBarService") != null)
                    {
                        this.MyToolBarService = (IToolBarService)System.Activator.CreateInstance(type);
                        return 0;
                    }
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        #endregion
    }	
}
