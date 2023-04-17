using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls
{
    public delegate void myTipEvent(string Tip, int Hypotest);
    /// <summary>
    /// ��ʾ�޸Ŀؼ�
    /// </summary>
    public partial class ucTip : UserControl
    {
        public ucTip()
        {
            InitializeComponent();
        }

        public event myTipEvent OKEvent;

        private void ucTip_Load(object sender, EventArgs e)
        {
            //{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
            if (this.FindForm() != null)
            {
                this.FindForm().Activated += new EventHandler(ucTip_Activated);
            }
        }
        protected string defaultTip = "?";
        /// <summary>
        /// Ĭ����ʾ
        /// </summary>
        public string DefaultTip
        {
            get
            {
                return this.defaultTip;
            }
            set
            {
                this.defaultTip = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Tip
        {
            set
            {
                if (value == "") value = defaultTip;
                this.rtb.Text = value;
            }
            get
            {
                return this.rtb.Text;
            }
        }
        /// <summary>
        /// �Ƿ�����޸�Ƥ�Խ��
        /// </summary>
        public bool IsCanModifyHypotest
        {
            get
            {
                return this.neuGroupBox1.Enabled;
            }
            set
            {
                this.neuGroupBox1.Enabled = value;
                this.chkHypotest.Enabled = !value;

            }
        }
        TabPage tPage = new TabPage();

        /// <summary>
        /// Ƥ�Խ��
        /// </summary>
        public int Hypotest
        {
            get
            {
                if (this.chkHypotest.Checked == false)
                {
                    return 1;
                }
                else
                {
                    if (this.rdo1.Checked)
                    {
                        return 3;
                    }
                    else if (this.rdo2.Checked)
                    {
                        return 4;
                    }
                    return 2;
                }
            }
            set
            {
                if (value <= 1)//if (value == 1) //{7D04C498-DB29-4dc9-B636-F96F0FDEE8E9}
                {
                    //{FC21CC20-38F0-4e97-8432-235F3BEC04A9}
                    this.chkHypotest.Checked = false;
                    tPage = this.tabPage2;
                    //�Ҽ���ʾƤ�Ժ���עhouwb {D822412B-07F4-4ed8-A749-E6EC16019461}
                    if (this.tabControl1.TabPages.Contains(this.tabPage2))
                        this.tabControl1.TabPages.Remove(this.tabPage2);
                    else if (this.tabControl1.TabPages.Contains(tPage))
                        this.tabControl1.TabPages.Remove(tPage);
                }
                else
                {
                    //�Ҽ���ʾƤ�Ժ���עhouwb {D822412B-07F4-4ed8-A749-E6EC16019461}
                    if (this.tabControl1.TabPages.Count == 1)
                    {
                        this.tabControl1.TabPages.Add(tPage);
                    }
                    this.chkHypotest.Checked = true;
                    if (value == 3)
                    {
                        this.rdo1.Checked = true;
                    }
                    else if (value == 4)
                    {
                        this.rdo2.Checked = true;
                    }
                }
            }
        }
        
        

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (FS.FrameWork.Public.String.ValidMaxLengh(Tip, 80) == false)
                {
                    MessageBox.Show("��עֻ��¼��40������!", "��ʾ");
                    return;
                }
                this.OKEvent(Tip, Hypotest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //{17A8C36D-DFA8-4d4e-A2AB-893AD5B3073A}
            if (this.FindForm() == FS.FrameWork.WinForms.Classes.Function.PopForm)
            {
                this.FindForm().Close();
            }
            //�Ҽ���ʾƤ�Ժ���עhouwb {D822412B-07F4-4ed8-A749-E6EC16019461}
            MessageBox.Show("����ɹ���");
        }

       

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            this.neuGroupBox1.Enabled = this.chkHypotest.Checked;
        }

        private void ucTip_Activated(object sender, EventArgs e)
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
