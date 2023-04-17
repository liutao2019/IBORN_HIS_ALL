using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Neusoft.UFC.DCP.UC
{
	/// <summary>
	/// ucDateTimePicker 的摘要说明。
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
			// 该调用是 Windows.Forms 窗体设计器所必需的。

			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

			this.Load += new EventHandler(ucDateTimePicker_Load);
			this.SizeChanged += new EventHandler(ucDateTimePicker_SizeChanged);

			this.comboBoxYear.TextChanged += new EventHandler(comboBoxYear_TextChanged);
			this.comboBoxMonth.TextChanged += new EventHandler(comboBoxYear_TextChanged);
			this.comboBoxDay.TextChanged += new EventHandler(comboBoxYear_TextChanged);

		}

		/// <summary> 
		/// 清理所有正在使用的资源。
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


		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
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
            this.lbYear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbYear.Location = new System.Drawing.Point(0, -3);
            this.lbYear.Name = "lbYear";
            this.lbYear.Size = new System.Drawing.Size(24, 32);
            this.lbYear.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbYear.TabIndex = 2;
            this.lbYear.Text = "年";
            this.lbYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMonth
            // 
            this.lbMonth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMonth.Location = new System.Drawing.Point(0, -3);
            this.lbMonth.Name = "lbMonth";
            this.lbMonth.Size = new System.Drawing.Size(24, 32);
            this.lbMonth.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbMonth.TabIndex = 4;
            this.lbMonth.Text = "月";
            this.lbMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDay
            // 
            this.lbDay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDay.Location = new System.Drawing.Point(0, -2);
            this.lbDay.Name = "lbDay";
            this.lbDay.Size = new System.Drawing.Size(24, 32);
            this.lbDay.Style = Neusoft.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lbDay.TabIndex = 6;
            this.lbDay.Text = "日";
            this.lbDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.ArrowBackColor = System.Drawing.Color.Silver;
            this.comboBoxYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxYear.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.comboBoxMonth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.comboBoxDay.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
		/// 设置时间选择范围
		/// 年月日的下限为现年月日减span所得
		/// 年月日的上限为现年月日加span所得
		/// </summary>
		/// <param name="yearSpan">年的间隔</param>
		/// <param name="monthSpan">月的间隔</param>
		/// <param name="daySpan">日的间隔</param>
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
		/// 设置年的选择范围
		/// 下限为现年减span所得
        /// 上限为现年加span所得
		/// </summary>
		/// <param name="span">间隔值</param>
		public void SetDefaultValueSpan(int yearLowSpan, int yearUpSpan)
		{
			this.SetDefaultValueSpan(yearLowSpan, yearUpSpan, 40, 40, 40, 40);
		}		
		
		/// <summary>
		/// 设置时间选择范围
		/// 年月日的下限为现年月日减span所得
		/// 年月日的上限为现年月日加span所得
		/// </summary>
		/// <param name="yearSpan">年的间隔</param>
		/// <param name="monthSpan">月的间隔</param>
		/// <param name="daySpan">日的间隔</param>
		public void SetDefaultValueSpan(int yearSpan, int monthSpan, int daySpan)
		{
			this.SetDefaultValueSpan(yearSpan, yearSpan, monthSpan, monthSpan, daySpan, daySpan);
		}

		/// <summary>
		/// 将年月日设置为当前值
		/// </summary>
		/// <param name="yearIsCurYear">是否将年设置为当前值</param>
		/// <param name="monthIsCurMonth">是否将月设置为当前值</param>
		/// <param name="dayIsCurDay">是否将日设置为当前值</param>
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
		/// 年份检测
		/// </summary>
		/// <param name="isNeedWarning">是否显示警告</param>
		/// <returns>true成功</returns>
		private bool checkYear(bool isNeedWarning)
		{
			//年份检测
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
						MessageBox.Show(this, "请输入表示年份的数字", "提示>>");
					}
					this.comboBoxYear.Text = "";
					this.comboBoxYear.Select();
					this.comboBoxYear.Focus();
					return false;
				}
				//简写输入时自动补充
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
					MessageBox.Show(ex.Message + "\n请输入表示年份的数字");
				}
				this.comboBoxYear.Text = "";
				this.comboBoxYear.Select();
				this.comboBoxYear.Focus();
				return false;
			}
			return true;
		}

		/// <summary>
		/// 月份检测
		/// </summary>
		/// <param name="isNeedWarning">是否显示警告</param>
		/// <returns>true成功</returns>
		private bool checkMonth(bool isNeedWarning)
		{
			//月份检测
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
						MessageBox.Show(this, "请输入表示月份的数字", "提示>>");
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
					MessageBox.Show(ex.Message + "\n请输入表示月份的数字");
				}
				this.comboBoxMonth.Text = "";
				this.comboBoxMonth.Select();
				this.comboBoxMonth.Focus();
				return false;
			}
			return true;
		}

		/// <summary>
		/// 日期检测
		/// </summary>
		/// <param name="isNeedWarning">是否显示警告</param>
		/// <returns>true成功</returns>
		private bool checkDay(bool isNeedWarning)
		{
			//月份检测
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
						MessageBox.Show(this, "请输入表示日期的数字", "提示>>");
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
					MessageBox.Show(ex.Message + "\n请输入表示日期的数字");
				}
				this.comboBoxDay.Text = "";
				this.comboBoxDay.Select();
				this.comboBoxDay.Focus();
				return false;
			}
			return true;
		}

		/// <summary>
		/// 判断日期设置是否有效
		/// </summary>
		/// <returns>true 有效</returns>
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
		/// 根据年月设置日期选择范围
		/// </summary>
		private void setDayChooseItemsFromYearAndMonth()
		{
			//年月必须有值而且有效
			if(!this.checkMonth(false))
			{
				return;
			}
			if(!this.checkYear(false))
			{
				return;
			}

			//获取年月
			int year = System.Int32.Parse(this.comboBoxYear.Text.Trim());
			int month = System.Int32.Parse(this.comboBoxMonth.Text.Trim());

			//计算可以选择的范围
			//用下一个月1号减去该月1号得到该月天数，则选择日期范围在该月天数内
			System.DateTime dtStart = new DateTime(year, month, 1, 0, 0, 0);
			System.DateTime dtEnd = dtStart.AddMonths(1);
			System.TimeSpan ts = dtEnd - dtStart;
			//该月天数
			int daySpan = ts.Days;

			//日期范围重新赋值
			this.comboBoxDay.Items.Clear();
			for(int index = this.initMinDay; index <= daySpan && index <= this.initMaxDay; index++)
			{
				this.comboBoxDay.Items.Add(index);
			}

			//如果选择了天数，重新检查正确性
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
		/// 获取时间
		/// </summary>
		/// <param name="dateTime">用户选择时间</param>
		/// <returns>true 成功</returns>
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
        ///  字体设置
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
		/// 显示时间
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
				//不处理
				this.comboBoxYear.Text = "";
				this.comboBoxMonth.Text = "";
				this.comboBoxDay.Text = "";
			}
		}

		private void ucDateTimePicker_Load(object sender, EventArgs e)
		{
			//初始化设置
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
