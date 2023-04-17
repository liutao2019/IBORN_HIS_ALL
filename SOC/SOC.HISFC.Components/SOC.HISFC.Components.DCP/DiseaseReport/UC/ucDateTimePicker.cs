using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Neusoft.UFC.DCP.UC
{
	/// <summary>
	/// ucDateTimePicker ��ժҪ˵����
	/// </summary>
	public class ucDateTimePicker : System.Windows.Forms.UserControl
	{
		private Neusoft.FrameWork.WinForms.Controls.NeuLabel lbYear;
		private Neusoft.FrameWork.WinForms.Controls.NeuLabel lbMonth;
		private Neusoft.FrameWork.WinForms.Controls.NeuLabel lbDay;
		private Neusoft.FrameWork.WinForms.Controls.NeuComboBox comboBoxYear;
		private Neusoft.FrameWork.WinForms.Controls.NeuComboBox comboBoxMonth;
		private Neusoft.FrameWork.WinForms.Controls.NeuComboBox comboBoxDay;
		private Neusoft.FrameWork.WinForms.Controls.NeuPanel panelYear;
		private Neusoft.FrameWork.WinForms.Controls.NeuPanel panellbYear;
		private Neusoft.FrameWork.WinForms.Controls.NeuPanel panelMonth;
		private Neusoft.FrameWork.WinForms.Controls.NeuPanel panellbMonth;
		private Neusoft.FrameWork.WinForms.Controls.NeuPanel panelDay;
        private Neusoft.FrameWork.WinForms.Controls.NeuPanel panellbDay;
        private IContainer components;

		public ucDateTimePicker()
		{
			// �õ����� Windows.Forms ���������������ġ�

			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��

			this.Load += new EventHandler(ucDateTimePicker_Load);
			this.SizeChanged += new EventHandler(ucDateTimePicker_SizeChanged);

			this.comboBoxYear.TextChanged += new EventHandler(comboBoxYear_TextChanged);
			this.comboBoxMonth.TextChanged += new EventHandler(comboBoxYear_TextChanged);
			this.comboBoxDay.TextChanged += new EventHandler(comboBoxYear_TextChanged);

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


		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.lbYear = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.lbMonth = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.lbDay = new Neusoft.FrameWork.WinForms.Controls.NeuLabel();
            this.comboBoxYear = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.comboBoxMonth = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.comboBoxDay = new Neusoft.FrameWork.WinForms.Controls.NeuComboBox(this.components);
            this.panelYear = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.panellbYear = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.panelMonth = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.panellbMonth = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.panelDay = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.panellbDay = new Neusoft.FrameWork.WinForms.Controls.NeuPanel();
            this.panelYear.SuspendLayout();
            this.panellbYear.SuspendLayout();
            this.panelMonth.SuspendLayout();
            this.panellbMonth.SuspendLayout();
            this.panelDay.SuspendLayout();
            this.panellbDay.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbYear
            // 
            this.lbYear.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbYear.Location = new System.Drawing.Point(0, -3);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(24, 32);
            this.lbYear.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbYear.TabIndex = 2;
            this.lbYear.Text = "��";
            this.lbYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMonth
            // 
            this.lbMonth.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMonth.Location = new System.Drawing.Point(0, -3);
            this.lbMonth.Name = "lbMonth";
            this.lbMonth.Size = new System.Drawing.Size(24, 32);
            this.lbMonth.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbMonth.TabIndex = 4;
            this.lbMonth.Text = "��";
            this.lbMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDay
            // 
            this.lbDay.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDay.Location = new System.Drawing.Point(0, -2);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(24, 32);
            this.lbDay.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDay.TabIndex = 6;
            this.lbDay.Text = "��";
            this.lbDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.ArrowBackColor = System.Drawing.Color.Silver;
            this.comboBoxYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxYear.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxYear.IsEnter2Tab = false;
            this.comboBoxYear.IsFlat = true;
            this.comboBoxYear.IsLike = true;
            this.comboBoxYear.Location = new System.Drawing.Point(0, 0);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.PopForm = null;
            this.comboBoxYear.ShowCustomerList = false;
            this.comboBoxYear.ShowID = false;
            this.comboBoxYear.Size = new System.Drawing.Size(64, 24);
            this.comboBoxYear.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.comboBoxYear.TabIndex = 7;
            this.comboBoxYear.Text = "2008";
            this.comboBoxYear.ToolBarUse = false;
            // 
            // comboBoxMonth
            // 
            this.comboBoxMonth.ArrowBackColor = System.Drawing.Color.Silver;
            this.comboBoxMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxMonth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxMonth.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxMonth.IsEnter2Tab = false;
            this.comboBoxMonth.IsFlat = true;
            this.comboBoxMonth.IsLike = true;
            this.comboBoxMonth.Location = new System.Drawing.Point(0, 0);
            this.comboBoxMonth.Name = "comboBoxMonth";
            this.comboBoxMonth.PopForm = null;
            this.comboBoxMonth.ShowCustomerList = false;
            this.comboBoxMonth.ShowID = false;
            this.comboBoxMonth.Size = new System.Drawing.Size(40, 24);
            this.comboBoxMonth.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.comboBoxMonth.TabIndex = 8;
            this.comboBoxMonth.Text = "01";
            this.comboBoxMonth.ToolBarUse = false;
            // 
            // comboBoxDay
            // 
            this.comboBoxDay.ArrowBackColor = System.Drawing.Color.Silver;
            this.comboBoxDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxDay.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxDay.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxDay.IsEnter2Tab = false;
            this.comboBoxDay.IsFlat = true;
            this.comboBoxDay.IsLike = true;
            this.comboBoxDay.Location = new System.Drawing.Point(0, 0);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.PopForm = null;
            this.comboBoxDay.ShowCustomerList = false;
            this.comboBoxDay.ShowID = false;
            this.comboBoxDay.Size = new System.Drawing.Size(40, 24);
            this.comboBoxDay.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.comboBoxDay.TabIndex = 9;
            this.comboBoxDay.Text = "01";
            this.comboBoxDay.ToolBarUse = false;
            // 
            // panelYear
            // 
            this.panelYear.Controls.Add(this.comboBoxYear);
            this.panelYear.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelYear.Location = new System.Drawing.Point(0, 0);
            this.panelYear.Name = "panelYear";
            this.panelYear.Size = new System.Drawing.Size(64, 32);
            this.panelYear.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelYear.TabIndex = 10;
            // 
            // panellbYear
            // 
            this.panellbYear.Controls.Add(this.lbYear);
            this.panellbYear.Dock = System.Windows.Forms.DockStyle.Left;
            this.panellbYear.Location = new System.Drawing.Point(64, 0);
            this.panellbYear.Name = "panellbYear";
            this.panellbYear.Size = new System.Drawing.Size(24, 32);
            this.panellbYear.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panellbYear.TabIndex = 11;
            // 
            // panelMonth
            // 
            this.panelMonth.Controls.Add(this.comboBoxMonth);
            this.panelMonth.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMonth.Location = new System.Drawing.Point(88, 0);
            this.panelMonth.Name = "panelMonth";
            this.panelMonth.Size = new System.Drawing.Size(40, 32);
            this.panelMonth.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelMonth.TabIndex = 12;
            // 
            // panellbMonth
            // 
            this.panellbMonth.Controls.Add(this.lbMonth);
            this.panellbMonth.Dock = System.Windows.Forms.DockStyle.Left;
            this.panellbMonth.Location = new System.Drawing.Point(128, 0);
            this.panellbMonth.Name = "panellbMonth";
            this.panellbMonth.Size = new System.Drawing.Size(24, 32);
            this.panellbMonth.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panellbMonth.TabIndex = 13;
            // 
            // panelDay
            // 
            this.panelDay.Controls.Add(this.comboBoxDay);
            this.panelDay.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelDay.Location = new System.Drawing.Point(152, 0);
            this.panelDay.Name = "panelDay";
            this.panelDay.Size = new System.Drawing.Size(40, 32);
            this.panelDay.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panelDay.TabIndex = 14;
            // 
            // panellbDay
            // 
            this.panellbDay.Controls.Add(this.lbDay);
            this.panellbDay.Dock = System.Windows.Forms.DockStyle.Left;
            this.panellbDay.Location = new System.Drawing.Point(192, 0);
            this.panellbDay.Name = "panellbDay";
            this.panellbDay.Size = new System.Drawing.Size(24, 32);
            this.panellbDay.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.panellbDay.TabIndex = 15;
            // 
            // ucDateTimePicker
            // 
            this.Controls.Add(this.panellbDay);
            this.Controls.Add(this.panelDay);
            this.Controls.Add(this.panellbMonth);
            this.Controls.Add(this.panelMonth);
            this.Controls.Add(this.panellbYear);
            this.Controls.Add(this.panelYear);
            this.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ucDateTimePicker";
            this.Size = new System.Drawing.Size(216, 32);
            this.panelYear.ResumeLayout(false);
            this.panellbYear.ResumeLayout(false);
            this.panelMonth.ResumeLayout(false);
            this.panellbMonth.ResumeLayout(false);
            this.panelDay.ResumeLayout(false);
            this.panellbDay.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private int initMinDay;
		private int initMaxDay;

		public delegate void PressEnterKey();
		public event PressEnterKey PressEnterKeyHandler;
		public delegate void ValueChange();
		public event ValueChange ValueChangeHandler;

		/// <summary>
		/// ����ʱ��ѡ��Χ
		/// �����յ�����Ϊ�������ռ�span����
		/// �����յ�����Ϊ�������ռ�span����
		/// </summary>
		/// <param name="yearSpan">��ļ��</param>
		/// <param name="monthSpan">�µļ��</param>
		/// <param name="daySpan">�յļ��</param>
		public void SetDefaultValueSpan(int yearLowSpan, int yearUpSpan, int monthLowSpan, int monthUpSpan, int dayLowSpan, int dayUpSpan)
		{
			int minValue = System.DateTime.Now.Year - yearLowSpan;
			int maxValue = System.DateTime.Now.Year + yearUpSpan;
			
			for(int index = maxValue; index >= minValue; index--)
			{
				this.comboBoxYear.Items.Add(index);
			}
			this.comboBoxYear.SelectedIndex = 2;

			minValue = System.DateTime.Now.Month - monthLowSpan;
			maxValue = System.DateTime.Now.Month + monthUpSpan;
			if(minValue < 1)
			{
				minValue = 1;
			}
			if(maxValue > 12)
			{
				maxValue = 12;
			}
			for(int index = minValue; index <= maxValue; index++)
			{
				this.comboBoxMonth.Items.Add(index);
			}

			minValue = System.DateTime.Now.Day - dayLowSpan;
			maxValue = System.DateTime.Now.Day + dayUpSpan;
			if(minValue < 1)
			{
				minValue = 1;
			}
			if(maxValue > 31)
			{
				maxValue = 31;
			}
			this.initMaxDay = maxValue;
			this.initMinDay = minValue;
			for(int index = minValue; index <= maxValue; index++)
			{
				this.comboBoxDay.Items.Add(index);
			}
		}

		/// <summary>
		/// �������ѡ��Χ
		/// ����Ϊ�����span����
        /// ����Ϊ�����span����
		/// </summary>
		/// <param name="span">���ֵ</param>
		public void SetDefaultValueSpan(int yearLowSpan, int yearUpSpan)
		{
			this.SetDefaultValueSpan(yearLowSpan, yearUpSpan, 40, 40, 40, 40);
		}		
		
		/// <summary>
		/// ����ʱ��ѡ��Χ
		/// �����յ�����Ϊ�������ռ�span����
		/// �����յ�����Ϊ�������ռ�span����
		/// </summary>
		/// <param name="yearSpan">��ļ��</param>
		/// <param name="monthSpan">�µļ��</param>
		/// <param name="daySpan">�յļ��</param>
		public void SetDefaultValueSpan(int yearSpan, int monthSpan, int daySpan)
		{
			this.SetDefaultValueSpan(yearSpan, yearSpan, monthSpan, monthSpan, daySpan, daySpan);
		}

		/// <summary>
		/// ������������Ϊ��ǰֵ
		/// </summary>
		/// <param name="yearIsCurYear">�Ƿ�������Ϊ��ǰֵ</param>
		/// <param name="monthIsCurMonth">�Ƿ�������Ϊ��ǰֵ</param>
		/// <param name="dayIsCurDay">�Ƿ�������Ϊ��ǰֵ</param>
		public void SetDefaultValue(bool yearIsCurYear, bool monthIsCurMonth, bool dayIsCurDay)
		{
			if(yearIsCurYear)
			{
				this.comboBoxYear.Text = System.DateTime.Now.Year.ToString();
			}
			else
			{
				this.comboBoxYear.Text = "";
			}
			if(monthIsCurMonth)
			{
				this.comboBoxMonth.Text = System.DateTime.Now.Month.ToString();
			}
			else
			{
				this.comboBoxMonth.Text = "";
			}
			if(dayIsCurDay)
			{
				this.comboBoxDay.Text = System.DateTime.Now.Day.ToString();
			}
			else
			{
				this.comboBoxDay.Text = "";
			}
		}

		/// <summary>
		/// ��ݼ��
		/// </summary>
		/// <param name="isNeedWarning">�Ƿ���ʾ����</param>
		/// <returns>true�ɹ�</returns>
		private bool checkYear(bool isNeedWarning)
		{
			//��ݼ��
			int year = 0;
			try
			{
				string strYear = this.comboBoxYear.Text.Trim();
				if(strYear == null || strYear == "")
				{
					this.comboBoxYear.Text = "";
					return false;
				}
                if (!UFC.DCP.UC.Function.Function.IsUnInt(strYear))
				{
				
					if(isNeedWarning)
					{
						MessageBox.Show(this, "�������ʾ��ݵ�����", "��ʾ>>");
					}
					this.comboBoxYear.Text = "";
					this.comboBoxYear.Select();
					this.comboBoxYear.Focus();
					return false;
				}
				//��д����ʱ�Զ�����
				if(strYear.Length == 1)
				{
					string strYearTmp = System.DateTime.Now.Year.ToString();
					strYear = strYearTmp[0].ToString() + strYearTmp[1].ToString() + strYearTmp[2].ToString() + strYear;
					this.comboBoxYear.Text = strYear;
				}
				if(strYear.Length == 2)
				{
					string strYearTmp = System.DateTime.Now.Year.ToString();
					strYear = strYearTmp[0].ToString() + strYearTmp[1].ToString() + strYear;
					this.comboBoxYear.Text = strYear;
				}
				year = System.Int32.Parse(strYear);
				if(year < System.Int32.Parse(strYear))
				{
					this.comboBoxYear.Text = this.comboBoxYear.Items[this.comboBoxYear.Items.Count - 1].ToString();
				}
				if(year > System.Int32.Parse(this.comboBoxYear.Items[0].ToString()))
				{
                    year -= 100;
                    this.comboBoxYear.Text = year.ToString();
                    if (year > System.Int32.Parse(this.comboBoxYear.Items[0].ToString()))
                    {
                        this.comboBoxYear.Text = this.comboBoxYear.Items[0].ToString();
                    }
				}
				
			}
			catch(Exception ex)
			{
				if(isNeedWarning)
				{
					MessageBox.Show(ex.Message + "\n�������ʾ��ݵ�����");
				}
				this.comboBoxYear.Text = "";
				this.comboBoxYear.Select();
				this.comboBoxYear.Focus();
				return false;
			}
			return true;
		}

		/// <summary>
		/// �·ݼ��
		/// </summary>
		/// <param name="isNeedWarning">�Ƿ���ʾ����</param>
		/// <returns>true�ɹ�</returns>
		private bool checkMonth(bool isNeedWarning)
		{
			//�·ݼ��
			int month = 0;
			try
			{
				string strMonth = this.comboBoxMonth.Text.Trim();
				if(strMonth == null || strMonth == "")
				{
					this.comboBoxMonth.Text = "";
					return false;
				}
                if (!UFC.DCP.UC.Function.Function.IsUnInt(strMonth))
				{
					if(isNeedWarning)
					{
						MessageBox.Show(this, "�������ʾ�·ݵ�����", "��ʾ>>");
					}
					this.comboBoxMonth.Text = "";
					this.comboBoxMonth.Select();
					this.comboBoxMonth.Focus();
					return false;
				}
				
				month = System.Int32.Parse(strMonth);
				if(month < System.Int32.Parse(this.comboBoxMonth.Items[0].ToString()))
				{
					this.comboBoxMonth.Text = this.comboBoxMonth.Items[0].ToString();
				}
				if(month > System.Int32.Parse(this.comboBoxMonth.Items[this.comboBoxMonth.Items.Count - 1].ToString()))
				{
					this.comboBoxMonth.Text = this.comboBoxMonth.Items[this.comboBoxMonth.Items.Count - 1].ToString();
				}
			}
			catch(Exception ex)
			{
				if(isNeedWarning)
				{
					MessageBox.Show(ex.Message + "\n�������ʾ�·ݵ�����");
				}
				this.comboBoxMonth.Text = "";
				this.comboBoxMonth.Select();
				this.comboBoxMonth.Focus();
				return false;
			}
			return true;
		}

		/// <summary>
		/// ���ڼ��
		/// </summary>
		/// <param name="isNeedWarning">�Ƿ���ʾ����</param>
		/// <returns>true�ɹ�</returns>
		private bool checkDay(bool isNeedWarning)
		{
			//�·ݼ��
			int day = 0;
			try
			{
				string strDay = this.comboBoxDay.Text.Trim();
				if(strDay == null || strDay == "")
				{
					this.comboBoxDay.Text = "";
					return false;
				}
                if (!UFC.DCP.UC.Function.Function.IsUnInt(strDay))
				{
					if(isNeedWarning)
					{
						MessageBox.Show(this, "�������ʾ���ڵ�����", "��ʾ>>");
					}
					this.comboBoxDay.Text = "";
					this.comboBoxDay.Select();
					this.comboBoxDay.Focus();
					return false;
				}
				day = System.Int32.Parse(strDay);
				if(day < System.Int32.Parse(this.comboBoxDay.Items[0].ToString()))
				{
					this.comboBoxDay.Text = this.comboBoxDay.Items[0].ToString();
				}
				if(day > System.Int32.Parse(this.comboBoxDay.Items[this.comboBoxDay.Items.Count - 1].ToString()))
				{
					this.comboBoxDay.Text = this.comboBoxDay.Items[this.comboBoxDay.Items.Count - 1].ToString();
				}
			}
			catch(Exception ex)
			{
				if(isNeedWarning)
				{
					MessageBox.Show(ex.Message + "\n�������ʾ���ڵ�����");
				}
				this.comboBoxDay.Text = "";
				this.comboBoxDay.Select();
				this.comboBoxDay.Focus();
				return false;
			}
			return true;
		}

		/// <summary>
		/// �ж����������Ƿ���Ч
		/// </summary>
		/// <returns>true ��Ч</returns>
		public bool IsValidDateTime()
		{
			if(this.checkYear(false))
			{
				if(this.checkMonth(false))
				{
					if(this.checkDay(false))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// ����������������ѡ��Χ
		/// </summary>
		private void setDayChooseItemsFromYearAndMonth()
		{
			//���±�����ֵ������Ч
			if(!this.checkMonth(false))
			{
				return;
			}
			if(!this.checkYear(false))
			{
				return;
			}

			//��ȡ����
			int year = System.Int32.Parse(this.comboBoxYear.Text.Trim());
			int month = System.Int32.Parse(this.comboBoxMonth.Text.Trim());

			//�������ѡ��ķ�Χ
			//����һ����1�ż�ȥ����1�ŵõ�������������ѡ�����ڷ�Χ�ڸ���������
			System.DateTime dtStart = new DateTime(year, month, 1, 0, 0, 0);
			System.DateTime dtEnd = dtStart.AddMonths(1);
			System.TimeSpan ts = dtEnd - dtStart;
			//��������
			int daySpan = ts.Days;

			//���ڷ�Χ���¸�ֵ
			this.comboBoxDay.Items.Clear();
			for(int index = this.initMinDay; index <= daySpan && index <= this.initMaxDay; index++)
			{
				this.comboBoxDay.Items.Add(index);
			}

			//���ѡ�������������¼����ȷ��
			if(checkDay(false))
			{
				int day = System.Int32.Parse(this.comboBoxDay.Text.Trim());
				if(day < this.initMinDay || day > this.initMaxDay)
				{
					this.comboBoxDay.Text = "";
				}
			}
		}

		/// <summary>
		/// ��ȡʱ��
		/// </summary>
		/// <param name="dateTime">�û�ѡ��ʱ��</param>
		/// <returns>true �ɹ�</returns>
		public bool GetDateTime(ref System.DateTime dateTime)
		{
			if(this.IsValidDateTime())
			{
				try
				{
					int year = System.Int32.Parse(this.comboBoxYear.Text.Trim());
					int month = System.Int32.Parse(this.comboBoxMonth.Text.Trim());
					int day = System.Int32.Parse(this.comboBoxDay.Text.Trim());
					dateTime = new DateTime(year, month, day, 0, 0, 0);
				}
				catch
				{
					this.comboBoxDay.Text = "";
					return false;
				}
				return true;
			}
			return false;
		}

        /// <summary>
        ///  ��������
        /// </summary>
        /// <param name="font"></param>
		public void SetFont(System.Drawing.Font font)
		{
			System.Drawing.Font newFont = font;//new Font(font.FontFamily, font.Size);
			this.Font = newFont;
			this.comboBoxYear.Font = newFont;
			this.comboBoxMonth.Font = newFont;
			this.comboBoxDay.Font = newFont;

			this.lbYear.Font = newFont;
			this.lbMonth.Font = newFont;
			this.lbDay.Font = newFont;
		}

		/// <summary>
		/// ��ʾʱ��
		/// </summary>
		/// <param name="dateTime"></param>
		public void ShowDateTime(System.DateTime dateTime)
		{
			try
			{
				if(dateTime <= new System.DateTime(1,1,1,0,0,0))
				{
					this.comboBoxYear.Text = "";
					this.comboBoxMonth.Text = "";
					this.comboBoxDay.Text = "";
					return;
				}
				this.comboBoxYear.Text = dateTime.Year.ToString();
				this.comboBoxMonth.Text = dateTime.Month.ToString();
				this.comboBoxDay.Text = dateTime.Day.ToString();
			}
			catch
			{
				//������
				this.comboBoxYear.Text = "";
				this.comboBoxMonth.Text = "";
				this.comboBoxDay.Text = "";
			}
		}

		private void ucDateTimePicker_Load(object sender, EventArgs e)
		{
			//��ʼ������
			this.SetDefaultValueSpan(100, 0);
			this.SetDefaultValue(false, false, false);

			this.comboBoxDay.KeyPress += new KeyPressEventHandler(comboBoxDay_KeyPress);
			this.comboBoxDay.Enter += new EventHandler(comboBoxDay_Enter);
			this.comboBoxDay.Leave += new EventHandler(comboBoxDay_Leave);

			this.comboBoxYear.Leave += new EventHandler(comboBoxYear_Leave);
			this.comboBoxYear.KeyPress += new KeyPressEventHandler(comboBoxYear_KeyPress);

			this.comboBoxMonth.Leave += new EventHandler(comboBoxMonth_Leave);
			this.comboBoxMonth.KeyPress += new KeyPressEventHandler(comboBoxMonth_KeyPress);

			this.SetFont(this.Font);
		}

		private void comboBoxDay_Enter(object sender, EventArgs e)
		{
			setDayChooseItemsFromYearAndMonth();
		}

		private void comboBoxYear_Leave(object sender, EventArgs e)
		{
            this.checkYear(true);
		}

		private void comboBoxMonth_Leave(object sender, EventArgs e)
		{
			this.checkMonth(true);			
		}

		private void ucDateTimePicker_SizeChanged(object sender, EventArgs e)
		{
			this.comboBoxYear.Height = this.Height;
			
		}

		private void comboBoxYear_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
                if (this.checkYear(true))
                {
                    this.comboBoxMonth.Select();
                    this.comboBoxMonth.Focus();
                }
			}
		}

		private void comboBoxMonth_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
                if (this.checkMonth(true))
                {
                    this.comboBoxDay.Select();
                    this.comboBoxDay.Focus();
                }
			}
		}

		private void comboBoxDay_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)13)
			{
                if (this.checkDay(true))
                {
                    if (this.PressEnterKeyHandler != null)
                    {
                        this.PressEnterKeyHandler();
                    }
                }
			}
		}

		private void comboBoxDay_Leave(object sender, EventArgs e)
		{
			this.checkDay(true);
		}

		private void comboBoxYear_TextChanged(object sender, EventArgs e)
		{
			if(this.ValueChangeHandler != null)
			{
				this.ValueChangeHandler();
			}
		}
	}
}
