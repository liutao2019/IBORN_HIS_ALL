using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    public delegate void myTipEvent(string result);
    /// <summary>
    /// ��ʾ�޸Ŀؼ�
    /// </summary>
    public partial class ucResultInput : UserControl
    {
        public ucResultInput()
        {
            InitializeComponent();
        }

        public event myTipEvent OKEvent;

        private void ucResultInput_Load(object sender, EventArgs e)
        {
            if (this.FindForm() != null)
            {
                this.FindForm().Activated += new EventHandler(ucResultInput_Activated);
            }
        }
        protected string defaultResult = string.Empty;
        /// <summary>
        /// Ĭ����ʾ
        /// </summary>
        public string DefaultResult
        {
            get
            {
                return this.defaultResult;
            }
            set
            {
                this.defaultResult = value;
            }
        }

        /// <summary>
        /// ��˷���
        /// </summary>
        public string Result
        {
            set
            {
                if (value == "") value = defaultResult;
                this.rtb.Text = value;
            }
            get
            {
                return this.rtb.Text;
            }
        }
       
        TabPage tPage = new TabPage();

        

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (FS.FrameWork.Public.String.ValidMaxLengh(Result, 100) == false)
                {
                    MessageBox.Show("��עֻ��¼��40������!", "��ʾ");
                    return;
                }
                this.OKEvent(Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (this.FindForm() == FS.FrameWork.WinForms.Classes.Function.PopForm)
            {
                this.FindForm().Close();
            }
            MessageBox.Show("����ɹ���");
        }



        private void ucResultInput_Activated(object sender, EventArgs e)
        {
            this.rtb.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
            if (this.FindForm() == FS.FrameWork.WinForms.Classes.Function.PopForm)
            {
                this.FindForm().Close();
            }
        }


    }
}
