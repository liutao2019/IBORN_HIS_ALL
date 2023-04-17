using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SOC.HISFC.Components.Nurse.Controls.Array
{
	/// <summary>
	/// ������Ļ��ʾ
	/// </summary>
	public class frmDisplay : System.Windows.Forms.Form
	{
		#region Windows ������������ɵĴ���

		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.Timer timer3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.RichTextBox rtbcontent;
		private System.Windows.Forms.Timer timer4;
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		private System.ComponentModel.IContainer components;

        public frmDisplay()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
			this.Load += new EventHandler(frmScreen_Load);
			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
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

		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.timer3 = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
			this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.rtbcontent = new System.Windows.Forms.RichTextBox();
			this.timer4 = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1024, 768);
			this.panel1.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.panel5);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(1024, 768);
			this.panel3.TabIndex = 1;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.White;
			this.panel5.Controls.Add(this.fpSpread1);
			this.panel5.Controls.Add(this.rtbcontent);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(0, 0);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(1024, 768);
			this.panel5.TabIndex = 1;
			// 
			// fpSpread1
			// 
			this.fpSpread1.BackColor = System.Drawing.Color.Black;
			this.fpSpread1.Font = new System.Drawing.Font("����", 11F);
			this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
			this.fpSpread1.Location = new System.Drawing.Point(2, 1);
			this.fpSpread1.Name = "fpSpread1";
			this.fpSpread1.SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows;
			this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
																				   this.fpSpread1_Sheet1});
			this.fpSpread1.Size = new System.Drawing.Size(445, 410);
			this.fpSpread1.TabIndex = 2;
			this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;

			// 
			// fpSpread1_Sheet1
			// 
			this.fpSpread1_Sheet1.Reset();
			this.fpSpread1_Sheet1.ColumnCount = 5;
			this.fpSpread1_Sheet1.RowCount = 8;
			this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.SystemColors.ControlText, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Black, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, false, false);
			this.fpSpread1_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.Black;
			this.fpSpread1_Sheet1.Columns.Get(0).ForeColor = System.Drawing.Color.Lime;
			this.fpSpread1_Sheet1.Columns.Get(0).Width = 21F;
			this.fpSpread1_Sheet1.Columns.Get(1).BackColor = System.Drawing.Color.Black;
			this.fpSpread1_Sheet1.Columns.Get(1).ForeColor = System.Drawing.Color.Yellow;
			this.fpSpread1_Sheet1.Columns.Get(1).Width = 68F;
			this.fpSpread1_Sheet1.Columns.Get(2).BackColor = System.Drawing.Color.Black;
			this.fpSpread1_Sheet1.Columns.Get(2).ForeColor = System.Drawing.Color.Lime;
			this.fpSpread1_Sheet1.Columns.Get(2).Width = 20F;
			this.fpSpread1_Sheet1.Columns.Get(3).BackColor = System.Drawing.Color.Black;
			this.fpSpread1_Sheet1.Columns.Get(3).ForeColor = System.Drawing.Color.LightCoral;
			this.fpSpread1_Sheet1.Columns.Get(3).Width = 65F;
			this.fpSpread1_Sheet1.Columns.Get(4).BackColor = System.Drawing.Color.Black;
			this.fpSpread1_Sheet1.Columns.Get(4).ForeColor = System.Drawing.Color.Lime;
			this.fpSpread1_Sheet1.Columns.Get(4).Width = 250F;
			this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
			this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
			this.fpSpread1_Sheet1.RowHeader.Visible = false;
			this.fpSpread1_Sheet1.SheetName = "Sheet1";
			// 
			// rtbcontent
			// 
			this.rtbcontent.BackColor = System.Drawing.Color.White;
			this.rtbcontent.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtbcontent.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.rtbcontent.ForeColor = System.Drawing.Color.Lime;
			this.rtbcontent.Location = new System.Drawing.Point(446, 2);
			this.rtbcontent.Name = "rtbcontent";
			this.rtbcontent.ReadOnly = true;
			this.rtbcontent.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.rtbcontent.Size = new System.Drawing.Size(661, 665);
			this.rtbcontent.TabIndex = 0;
			this.rtbcontent.Text = "���ڽ��г�ʼ��...";
			// 
			// timer4
			// 
			this.timer4.Interval = 1500;
			this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
			// 
			// frmNewDisplay
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1024, 768);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmNewDisplay";
			this.ShowInTaskbar = false;
			this.Text = "frmNewDisplay";
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


        private FS.HISFC.Models.Base.Employee ps = new FS.HISFC.Models.Base.Employee();

		private ArrayList alQueue = new ArrayList();
		private ArrayList alBook = new ArrayList();

        private FS.HISFC.BizLogic.Nurse.Queue queMgr = new FS.HISFC.BizLogic.Nurse.Queue();
        private FS.HISFC.BizProcess.Integrate.Manager psMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Nurse.Assign assMgr = new FS.HISFC.BizLogic.Nurse.Assign();
        private FS.HISFC.Models.Base.Employee oper = new FS.HISFC.Models.Base.Employee();

        private FS.HISFC.BizProcess.Integrate.Registration.Registration doctSchemaMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        private FS.HISFC.BizLogic.Nurse.Room roomMgr = new FS.HISFC.BizLogic.Nurse.Room();
        private FS.HISFC.BizLogic.Manager.Constant myCnst= new FS.HISFC.BizLogic.Manager.Constant();


        FS.SOC.HISFC.Assign.BizLogic.Assign assMgrTemp = new FS.SOC.HISFC.Assign.BizLogic.Assign();


		int queueNum = 0;
		int nowNum = 0; 

        string screenSize = "";
		string screenSizeX = "1024";
		string screenSizeY = "768";
		int screenDisplayNum = 8;

		public string NurseCellID
		{
			set
			{
				this.nurseCellID = value;
			}
		}
		private string nurseCellID = ""; 

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmScreen_Load(object sender, EventArgs e)
		{
            FS.HISFC.BizProcess.Integrate.Manager controlMgr =
                new FS.HISFC.BizProcess.Integrate.Manager();
			screenSize =controlMgr.QueryControlerInfo("900004");
			this.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize) ,-3); 

			screenSizeX =controlMgr.QueryControlerInfo("900008");
			screenSizeY =controlMgr.QueryControlerInfo("900009");
			string num =controlMgr.QueryControlerInfo("A00002");
            if (!string.IsNullOrEmpty(num))
            {
                screenDisplayNum = int.Parse(num);
            }

			this.Size = new Size( FS.FrameWork.Function.NConvert.ToInt32(screenSizeX),
				FS.FrameWork.Function.NConvert.ToInt32(screenSizeY));

			//ps = psMgr.GetPersonByID(this.queMgr.Operator.ID);
			DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
			DateTime current = currenttime.Date;
			string noonID = SOC.HISFC.Components.Nurse.Controls.Array.Function.GetNoon(currenttime);//���
			this.alQueue = queMgr.Query(this.nurseCellID,current,noonID);
			this.queueNum = this.alQueue.Count;

			//����
//			this.timer2.Tick += new EventHandler(timer2_Tick);
//			this.timer2.Interval = 60000;
//			this.timer2.Start();
			//��ֵ
//			this.timer1.Tick += new EventHandler(timer1_Tick);
//			this.timer1.Interval = 12000;
//			this.timer1.Start();
			//ʱ��
//			this.timer3.Interval = 1000;
//			this.timer3.Start();

			this.timer4.Start();

			this.initDisplay();
		}
		/// <summary>
		/// �������ؼ���������(����ؼ����������۵Ŀؼ�������ͬ,�򲻲���,�����������ɿؼ�)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer2_Tick(object sender, EventArgs e)
		{
			//��ȡ��ǰ�ؼ�����
			//ps = psMgr.GetPersonByID(this.queMgr.Operator.ID);
			DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
			DateTime current = currenttime.Date;
			string noonID = SOC.HISFC.Components.Nurse.Controls.Array.Function.GetNoon(currenttime);//���
//			this.alQueue = queMgr.Query(ps.Nurse.ID,current,noonID);
			this.alQueue = queMgr.Query(this.nurseCellID,current,noonID);
			int intTmp = this.alQueue.Count;
			if(intTmp <= 0)
			{
				this.Controls.Clear();
				//���ó�����������Ĵ���(û��ά������)
			}
			//�ؼ�������ԭ����Ƚ�
			if( intTmp != queueNum && intTmp > 0 )
			{
				if(queueNum > 0)
				{
					this.Controls.Clear();
				}
				//��ֵһ���µĿؼ�/��������
				this.queueNum = intTmp;
			}

			if(this.Location.X != FS.FrameWork.Function.NConvert.ToInt32(screenSize) )
			{
				this.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize) ,0); 
			}

		}
		/// <summary>
		/// �ؼ����ƶ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			//ר�Ҷ��б�־
			this.rtbcontent.Tag = "0";

			ArrayList al = new ArrayList();
			FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();
			DateTime dtNow = this.assMgr.GetDateTimeFromSysDateTime();
			DateTime current = dtNow.Date;
			for(int i = nowNum ; i < this.queueNum ; i++)
			{
				nowNum++;
				if(nowNum >= queueNum) nowNum = 0;

				//�ؼ����������
				queue = (FS.HISFC.Models.Nurse.Queue)this.alQueue[nowNum];
				if(queue.ExpertFlag == "1")
				{
					this.rtbcontent.Tag = "1";
				}
				//���ݶ��л�ȡ��������
				//���ݶ��л�ȡ��̨��������̨��ȡ����
				al = this.assMgr.QueryPatient(current,current.AddDays(1),queue.Console.ID,"3",queue.Doctor.ID);
				if(al.Count > 0) break;
			}
			FS.HISFC.BizLogic.Manager.Person psMgr = new  FS.HISFC.BizLogic.Manager.Person();
			FS.HISFC.Models.Base.Employee ps = new  FS.HISFC.Models.Base.Employee();
			ps = psMgr.GetPersonByID(queue.Doctor.ID);
			this.rtbcontent.Text = queue.SRoom.Name + "--" + queue.Console.Name + "[" + ps.Name +"]"+ "\n";

			//---�����ר�ҵĶ���,��ʾ����ר�ҵ��Ű�ʱ���.(����ǹ�ר��,��ʾר,������ʾ��ͨ)
			string zone = "";

			for(int i = 0 ; i <al.Count ;i++)
			{
				FS.HISFC.Models.Nurse.Assign info = (FS.HISFC.Models.Nurse.Assign)al[i];

					if(zone != "ʱ��:"+info.Register.DoctorInfo.Templet.Begin.ToString("HH:mm") +"-"+info.Register.DoctorInfo.Templet.End.ToString("HH:mm"))
					{
						if(zone != "")
						{
							this.rtbcontent.Text += "\n";
						}

						zone = "ʱ��:"+info.Register.DoctorInfo.Templet.Begin.ToString("HH:mm") +"-"+info.Register.DoctorInfo.Templet.End.ToString("HH:mm");
						this.rtbcontent.Text = this.rtbcontent.Text + zone +"\n";
					}

					this.rtbcontent.Text = this.rtbcontent.Text + "["+ (i+1).ToString().PadLeft(2,'0') + "]" 
						+ this.PadName(info.Register.Name);
			}

		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		private string PadName(string name)
		{
			//�����ֲ���(ԭ��6.5.4)
			int n = name.Length;
			string strname = "";
			if(n == 2 )
			{
				strname = name.PadRight(6,' ');
			}
			else if(n == 3 )
			{
				strname = name.PadRight(5,' ');
			}
			else if(n == 4 )
			{
				strname = name.PadRight(4,' ');
			}
			else
			{
				strname = name;
			}
			return strname;
		}

		/// <summary>
		/// �ر��������
		/// </summary>
		public void CloseThis()
		{
			this.Close();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmScreen_DoubleClick(object sender, System.EventArgs e)
		{
			this.CloseThis();
		}

		FS.HISFC.BizLogic.Nurse.Seat mySeat = new  FS.HISFC.BizLogic.Nurse.Seat();

		/// <summary>
		/// ��ʾ�к�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer4_Tick(object sender, EventArgs e)
        {
          
            initDisplay();
        }


		private void initDisplay()
		{

            DateTime currenttime = this.queMgr.GetDateTimeFromSysDateTime();
            string current = currenttime.Date.ToString("yyyy-MM-dd");
            string noonID = SOC.HISFC.Components.Nurse.Controls.Array.Function.GetNoon(currenttime);//���

			this.fpSpread1_Sheet1.Rows.Remove(0,this.fpSpread1_Sheet1.Rows.Count);
            ArrayList alCons = this.assMgrTemp.GetPatientByNurseCellCode(this.nurseCellID, "2",current);
					

            foreach (FS.SOC.HISFC.Assign.Models.Assign cnst in alCons)
			{

						string name = "";
						string roomNo = "";

                        name = cnst.Register.Name;
                        if (name.Length <= 2)
                        {
                            name = name.Insert(1, "  ");
                        }

                        roomNo = cnst.Queue.SRoom.Name;

						this.fpSpread1_Sheet1.Rows.Add(0,1);
						this.fpSpread1_Sheet1.Cells[0,0].Text = "��";
						this.fpSpread1_Sheet1.Cells[0,1].Text = name;
						this.fpSpread1_Sheet1.Cells[0,2].Text = "��";
						this.fpSpread1_Sheet1.Cells[0,3].Text = roomNo;
						this.fpSpread1_Sheet1.Cells[0,4].Text = "����";

				
			}
			if(this.fpSpread1_Sheet1.Rows.Count==0)
			{
				this.fpSpread1_Sheet1.Rows.Add(0,1);	
				this.fpSpread1_Sheet1.Cells[0,0].ColumnSpan=5;
				//this.fpSpread1_Sheet1.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
				this.fpSpread1_Sheet1.Cells[0,0].Text="         ��ܰ��ʾ";

				this.fpSpread1_Sheet1.Rows.Add(1,1);
				this.fpSpread1_Sheet1.Cells[1,0].ColumnSpan=5;
				this.fpSpread1_Sheet1.Cells[1,0].Text="    ����ִ�����͹Һŵ��ں�";		

				this.fpSpread1_Sheet1.Rows.Add(2,1);
				this.fpSpread1_Sheet1.Cells[2,0].ColumnSpan=5;
				this.fpSpread1_Sheet1.Cells[2,0].Text="���ҵȴ������������������";		

				this.fpSpread1_Sheet1.Rows.Add(3,1);
				this.fpSpread1_Sheet1.Cells[3,0].ColumnSpan=5;
				this.fpSpread1_Sheet1.Cells[3,0].Text="Ϣ������ʾ����Ϣʱ��������";	

				this.fpSpread1_Sheet1.Rows.Add(4,1);
				this.fpSpread1_Sheet1.Cells[4,0].ColumnSpan=5;
				this.fpSpread1_Sheet1.Cells[4,0].Text="Ӧ���Ҿ��лл������";			
			}
		}
	}
}
