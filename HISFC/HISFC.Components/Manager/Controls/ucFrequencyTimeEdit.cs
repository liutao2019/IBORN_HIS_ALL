using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// [��������: Ƶ��ά��-ʱ������ÿؼ�]<br></br>
    /// [�� �� ��: Ѧռ��]<br></br>
    /// [����ʱ��: 2006��12��18]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucFrequencyTimeEdit : UserControl,FarPoint.Win.Spread.CellType.ISubEditor
    {
        public ucFrequencyTimeEdit()
        {
            InitializeComponent();
        }

        #region ����
        private string strTime
        {
            get
            {
                return this.neuTextBox1.Text;
            }
            set
            {
                this.neuTextBox1.Text = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ��ʼ��ʱ��
        /// </summary>
        /// <param name="Interval"></param>
        private void InitialTime(decimal Interval)
        {
            this.neuCheckedListBox1.Items.Clear();
            this.neuCheckedListBox1.MultiColumn = true;
            for (decimal i = 0; i < 24; i = i + Interval)
            {
                try
                {
                    int k = (int)i; ;
                    if (i >= (decimal)(k + .59))
                    {

                        i = k + 1;
                    }
                }
                catch { }
                this.neuCheckedListBox1.Items.Add(ConvertToTime(i));
            }
        }
        /// <summary>
        /// ת��ʱ��
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private string ConvertToTime(decimal d)
        {
            string s;
            decimal k;
            k = d - (int)d;
            try
            {
                if (k.ToString().Length >= 4)
                    s = k.ToString().Substring(0, 4);
                else
                    s = k.ToString();
                s = s.Substring(2).PadRight(2, '0');
            }
            catch
            {
                s = "00";
            }
            s = ((int)d).ToString() + ":" + s;
            return s;
        }

        /// <summary>
        /// ���ѡ����
        /// </summary>
        /// <returns></returns>
        private string GetSelectTime()
        {
            string s = "";
            try
            {
                for (int i = 0; i < this.neuCheckedListBox1.Items.Count; i++)
                {
                    if (this.neuCheckedListBox1.GetItemChecked(i))
                    {
                        s = s + "-" + this.neuCheckedListBox1.Items[i];
                    }
                }
                s = s.Substring(1);
            }
            catch { }
            return s;
        }
        #endregion

        #region �¼�
        private void neuCheckedListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.neuTextBox1.Text = this.GetSelectTime();

        }

        private void ucFrequencyTimeEdit_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuCheckedListBox1.CheckedItems.Count; i++)
            {
                this.neuCheckedListBox1.SetItemCheckState(i, CheckState.Unchecked);
            }
            this.neuCheckBox1.Checked = false;
            InitialTime(1);
        }

        private void neuCheckedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.neuTextBox1.Text = this.GetSelectTime();
        }

        private void neuCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuCheckedListBox1.Items.Count; i++)
            {
                this.neuCheckedListBox1.SetItemChecked(i, this.neuCheckBox1.Checked);
            }
            this.neuTextBox1.Text = this.GetSelectTime();
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.ValueChanged(sender, System.EventArgs.Empty);
            this.CloseUp(sender, System.EventArgs.Empty);
            this.Hide();
            for (int i = 0; i < this.neuCheckedListBox1.Items.Count; i++)
            {
                this.neuCheckedListBox1.SetItemChecked(i, false);
            }

        }
        private void neuTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    this.InitialTime(decimal.Parse(this.neuTextBox2.Text));
                }
                catch { }
            }
        }
        #endregion

        #region ISubEditor ��Ա

        public event EventHandler CloseUp;

        public Point GetLocation(Rectangle rect)
        {
            System.Drawing.Point pt = new Point();
            System.Drawing.Size sz = GetPreferredSize();
            pt.Y = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (sz.Height / 2);
            pt.X = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (sz.Width / 2);
            return pt;
        }

        public Size GetPreferredSize()
        {
            return new Size(this.Width, this.Height);
        }

        public Control GetSubEditorControl()
        {
            return this;
        }

        public object GetValue()
        {
            return this.strTime;
        }

        public void SetValue(object value)
        {
            this.strTime = value.ToString();
        }

        public event EventHandler ValueChanged;

        #endregion
    }
}
