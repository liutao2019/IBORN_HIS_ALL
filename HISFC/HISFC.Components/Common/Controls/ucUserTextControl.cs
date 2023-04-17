using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// [��������: �û������ı���ӿؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucUserTextControl : UserControl
    {
        public ucUserTextControl()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        
        #region ����
        private bool bShowGeneral = false;
        /// <summary>
        /// �Ƿ������ȫԺ�����б�
        /// </summary>
        public bool IsShowGeneral
        {
            set
            {
                bShowGeneral = value;
                this.radioButton1.Visible = !value;
                this.radioButton2.Visible = !value;
                this.cmb.Visible = value;
            }
        }
        #endregion
        #region ����
        protected FS.HISFC.Models.Base.UserText myUserText = null;
        /// <summary>
        /// ��ǰ�û��ı�
        /// </summary>
        public FS.HISFC.Models.Base.UserText UserText
        {
            get
            {
                SetValue();
                return myUserText;
            }
            set
            {

                myUserText = value;
                GetValue();

            }
        }
        public FS.HISFC.Models.Base.Employee curUser = null;
        FS.HISFC.BizLogic.Manager.Spell manager = new FS.HISFC.BizLogic.Manager.Spell();
        /// <summary>
        /// ������ֵ
        /// </summary>
        /// <returns></returns>
        protected int SetValue()
        {
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("���������ƣ�");
                return -1;
            }
            if (this.richTextBox1.Text == "")
            {
                MessageBox.Show("������Ҫ������ı���");
                return -1;
            }
            if (myUserText == null) myUserText = new FS.HISFC.Models.Base.UserText();

            curUser = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            if (curUser == null || curUser.Dept.ID == "" || curUser.ID == "")
            {
                MessageBox.Show("��ǰ�û�ά�������û��ı���");
                return -1;
            }

            myUserText.Name = this.textBox1.Text;
            myUserText.Text = this.richTextBox1.Text;
            myUserText.RichText = myUserText.RichText.Replace("'", "''");

            if (this.bShowGeneral)
            {
                myUserText.Type = "2";
                if (this.cmb.SelectedIndex == 0)
                {
                    myUserText.Code = "SIGN";
                }
                else if (this.cmb.SelectedIndex == 1)
                {
                    myUserText.Code = "WORD";
                }
                else if (this.cmb.SelectedIndex == 2)
                {
                    myUserText.Code = "RELATION";
                }
                else
                {
                    MessageBox.Show("�޷���λ�ַ����ͣ�");
                    return -1;
                }

            }
            else
            {
                if (this.radioButton1.Checked)
                {
                    myUserText.Type = "1";
                    myUserText.Code = curUser.Dept.ID;
                }
                else
                {
                    myUserText.Type = "0";
                    myUserText.Code = curUser.ID;
                }
            }

            try
            {
                myUserText.SpellCode = manager.Get(myUserText.Name).SpellCode;
            }
            catch { }
            return 0;
        }
        protected int GetValue()
        {
            if (myUserText == null) return -1;
            if (myUserText.Name == "")
            {
                if (myUserText.Text.Trim().Length > 5)
                    myUserText.Name = myUserText.Text.Trim().Substring(0, 5);
                else
                    myUserText.Name = myUserText.Text;
            }
            this.txtMemo.Text = myUserText.Memo;
            this.textBox1.Text = myUserText.Name;

            if (this.richTextBox1.Text == "")
                this.richTextBox1.Text = myUserText.Text;

            if (myUserText.Type == "1")
            {
                this.radioButton1.Checked = true;
            }
            else
            {
                this.radioButton2.Checked = true;
            }
            try
            {
                string code = string.Empty;
                string type = string.Empty;
                curUser = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                FS.HISFC.BizLogic.Manager.UserText m = new FS.HISFC.BizLogic.Manager.UserText();
                if (this.radioButton1.Checked)
                {
                    type = "1";
                    code = curUser.Dept.ID;
                }
                else
                {
                    type = "0";
                    code = curUser.ID;
                }
                ArrayList al = m.GetGroupList(code, type);
                if (txtMemo.Items.Count > 0)
                {
                    txtMemo.Items.Clear();
                    txtMemo.Text = "";
                }
                int count=0;
                foreach (FS.FrameWork.Models.NeuObject userText in al)
                {
                    userText.ID = (count++).ToString();
                }
                this.txtMemo.AddItems(al);
            }
            catch
            {
                return -1;
            }
            return 0;
        }

       
     
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (this.SetValue() == -1) return -1;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.BizLogic.Manager.UserText m = new FS.HISFC.BizLogic.Manager.UserText();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(m.Connection);
            //t.BeginTransaction();
            //m.SetTrans(t.Trans);
            int i = 0;
            this.myUserText.Memo = this.txtMemo.Text.Trim();
            if (this.myUserText.ID == "")
            {
                i = m.Insert(this.myUserText);
            }
            else
            {
                i = m.Update(this.myUserText);
            }
            if (i == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(m.Err);
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("����ɹ���");
                this.FindForm().Close();
            }
            return i;

        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            string code = string.Empty;
            string type = string.Empty;
            curUser = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.BizLogic.Manager.UserText m = new FS.HISFC.BizLogic.Manager.UserText();
            if (this.radioButton1.Checked)
            {
                type = "1";
                code = curUser.Dept.ID;
            }
            else
            {
                type = "0";
                code = curUser.ID;
            }
            ArrayList al = m.GetGroupList(code, type);
            if (txtMemo.Items.Count > 0)
            {
                this.txtMemo.Items.Clear();
                txtMemo.Text = "";
            }
            int count = 0;
            foreach (FS.FrameWork.Models.NeuObject userText in al)
            {
                userText.ID = (count++).ToString();
            }
            this.txtMemo.AddItems(al);
        }

    }
}
