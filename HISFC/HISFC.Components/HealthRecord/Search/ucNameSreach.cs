using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.Search
{
    public partial class ucNameSreach : UserControl
    {
        public ucNameSreach()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.Manager.Spell sp = new FS.HISFC.BizLogic.Manager.Spell();
        FS.HISFC.BizLogic.HealthRecord.SearchManager sm = new FS.HISFC.BizLogic.HealthRecord.SearchManager();
        private string strName = "";
        /// <summary>
        /// ɸѡ����
        /// </summary>
        public string strWhere = "";
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int CreateName()
        {
            //���� 
            #region ǰ�ڴ���
            this.txtName.Text = this.txtName.Text.Trim();
            this.txtSpell1.Text = this.txtSpell1.Text.ToUpper();
            this.txtSpell2.Text = this.txtSpell2.Text.ToUpper();
            this.txtSpell3.Text = this.txtSpell3.Text.ToUpper();
            if (this.txtName.Text == "")
            {
                MessageBox.Show("����������");
                return -1;
            }
            if (this.comType.Text == "")
            {
                MessageBox.Show("��ѡ���ѯģʽ");
                return -1;
            }
            strName = "";
            #endregion
            switch (comType.Text)
            {
                case "��ȷ����":
                    strName = this.txtName.Text;
                    strWhere = sm.GetSqlByIndex("Case.SearchManager.GetSqlByIndex.ss");
                    if (strWhere == null)
                    {
                        MessageBox.Show("��ѯSQLʧ��" + sm.Err);
                        return -1;
                    }
                    strWhere = string.Format(strWhere, strName);
                    break;
                case "ģ������":
                    strName = this.txtName.Text;
                    strWhere = sm.GetSqlByIndex("Case.SearchManager.GetSqlByIndex.ss");
                    if (strWhere == null)
                    {
                        MessageBox.Show("��ѯSQLʧ��" + sm.Err);
                        return -1;
                    }
                    strWhere = string.Format(strWhere, strName);
                    break;
                case "���X":
                    #region ���X
                    string firWord = this.txtName.Text.Substring(0, this.txtName.Text.Length - 1);
                    string LastWord = this.txtName.Text.Substring(this.txtName.Text.Length - 1);
                    string SpellCode = this.txtSpell1.Text;
                    if (SpellCode == "")
                    {
                        SpellCode = sp.GetSpellCode(LastWord);
                    }
                    string LikeName = firWord + "_"; //like '���_'
                    if (SpellCode == null)
                    {
                        MessageBox.Show("��ѯƴ����ʧ��" + sp.Err);
                        return -1;
                    }
                    ArrayList list = sp.GetWord(SpellCode.ToUpper());
                    if (list == null)
                    {
                        MessageBox.Show("��ѯͬ���ֳ���" + sp.Err);
                        return -1;
                    }
                    foreach (FS.HISFC.Models.Base.Spell obj in list)
                    {
                        if (strName == "")
                        {
                            strName = "'" + firWord + obj.Name + "'";
                        }
                        else
                        {
                            strName += ",'" + firWord + obj.Name + "'";
                        }
                    }
                    strWhere = sm.GetSqlByIndex("Case.SearchManager.GetSqlByIndex");
                    if (strWhere == null)
                    {
                        MessageBox.Show("��ѯSQLʧ��" + sm.Err);
                        return -1;
                    }
                    strWhere = string.Format(strWhere, LikeName, strName);
                    #endregion
                    break;
                case "��X��":
                    #region ��X��
                    string strfirWord = this.txtName.Text.Substring(0, 1);
                    string strMidleWord = this.txtName.Text.Substring(1, 1);
                    string strLastWord = this.txtName.Text.Substring(this.txtName.Text.Length - 1);
                    string strSpellCode = this.txtSpell1.Text;
                    if (strSpellCode == "")
                    {
                        strSpellCode = sp.GetSpellCode(strLastWord);
                    }
                    string strLikeName = strfirWord + "_" + strLastWord; //like '��_��'
                    if (strSpellCode == null)
                    {
                        MessageBox.Show("��ѯƴ����ʧ��" + sp.Err);
                        return -1;
                    }
                    ArrayList list2 = sp.GetWord(strSpellCode.ToUpper());
                    if (list2 == null)
                    {
                        MessageBox.Show("��ѯͬ���ֳ���" + sp.Err);
                        return -1;
                    }
                    foreach (FS.HISFC.Models.Base.Spell obj in list2)
                    {
                        if (strName == "")
                        {
                            strName = "'" + strfirWord + obj.Name + strLastWord + "'";
                        }
                        else
                        {
                            strName += ",'" + strfirWord + obj.Name + strLastWord + "'";
                        }
                    }
                    strWhere = sm.GetSqlByIndex("Case.SearchManager.GetSqlByIndex");
                    if (strWhere == null)
                    {
                        MessageBox.Show("��ѯSQLʧ��" + sm.Err);
                        return -1;
                    }
                    strWhere = string.Format(strWhere, strLikeName, strName);
                    #endregion
                    break;
                case "X����":
                    #region X����
                    string firWord1 = this.txtName.Text.Substring(0, 1);
                    string LastWord1 = this.txtName.Text.Substring(1, this.txtName.Text.Length);
                    string SpellCode1 = this.txtSpell1.Text;
                    if (SpellCode1 == "")
                    {
                        SpellCode1 = sp.GetSpellCode(firWord1);
                    }
                    string LikeName1 = "_" + LastWord1; //like 'X����'

                    if (SpellCode1 == null)
                    {
                        MessageBox.Show("��ѯƴ����ʧ��" + sp.Err);
                        return -1;
                    }
                    ArrayList list1 = sp.GetWord(SpellCode1.ToUpper());
                    if (list1 == null)
                    {
                        MessageBox.Show("��ѯͬ���ֳ���" + sp.Err);
                        return -1;
                    }
                    foreach (FS.HISFC.Models.Base.Spell obj in list1)
                    {
                        if (strName == "")
                        {
                            strName = "'" + obj.Name + LastWord1 + "'";
                        }
                        else
                        {
                            strName += ",'" + obj.Name + LastWord1 + "'";
                        }
                    }
                    strName = sm.GetSqlByIndex("Case.SearchManager.GetSqlByIndex.1");
                    if (strName == null)
                    {
                        MessageBox.Show("��ѯSQLʧ��" + sm.Err);
                        return -1;
                    }
                    #endregion
                    break;
                case "ͬ������":
                    #region ͬ������
                    if (this.txtName.Text.Length == 1)
                    {
                        if (txtSpell1.Text == "")
                        {
                            MessageBox.Show("û��ƴ����");
                            this.txtSpell1.Focus();
                            return -1;
                        }
                        strName = sm.GetSqlByIndex("Case.SearchManager.GetSqlByIndex.1");
                        if (strName == null)
                        {
                            MessageBox.Show("��ѯSQLʧ��" + sm.Err);
                            return -1;
                        }
                        strWhere = string.Format(strName, txtName.Text, txtSpell1.Text);
                    }
                    else if (this.txtName.Text.Length == 2)
                    {
                        if (txtSpell1.Text == "" || txtSpell2.Text == "")
                        {
                            MessageBox.Show("û��ƴ����");
                            this.txtSpell1.Focus();
                            return -1;
                        }
                        strName = sm.GetSqlByIndex("Case.SearchManager.GetSqlByIndex.2");
                        if (strName == null)
                        {
                            MessageBox.Show("��ѯSQLʧ��" + sm.Err);
                            return -1;
                        }
                        strWhere = string.Format(strName, txtName.Text, txtSpell1.Text, txtSpell2.Text);
                    }
                    else if (this.txtName.Text.Length == 3)
                    {
                        if (txtSpell1.Text == "" || txtSpell2.Text == "" || txtSpell3.Text == "")
                        {
                            MessageBox.Show("û��ƴ����");
                            this.txtSpell1.Focus();
                            return -1;
                        }
                        strName = sm.GetSqlByIndex("Case.SearchManager.GetSqlByIndex.3");
                        if (strName == null)
                        {
                            MessageBox.Show("��ѯSQLʧ��" + sm.Err);
                            return -1;
                        }
                        strWhere = string.Format(strName, txtName.Text, txtSpell1.Text, txtSpell2.Text, txtSpell3.Text);
                    }
                    #endregion
                    break;
                default:
                    MessageBox.Show("ѡ���ģʽ������");
                    this.comType.Focus();
                    break;
            }
            return 1;
        }

        private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                if (comType.Text == "")
                {
                    this.comType.Focus();
                    MessageBox.Show("��ѡ���ѯģʽ");
                    this.comType.Focus();
                    return;
                }
                switch (comType.Text)
                {
                    case "��ȷ����":
                        break;
                    case "ģ������":
                        break;
                    case "���X":
                        #region ���X
                        string LastWord = this.txtName.Text.Substring(this.txtName.Text.Length - 1);
                        if (SetValue(txtSpell1, LastWord) == -1)
                        {
                            return;
                        }
                        #endregion
                        break;
                    case "��X��":
                        #region ��X��
                        if (this.txtName.Text.Length < 2)
                        {
                            return;
                        }
                        string strMidleWord = this.txtName.Text.Substring(1, 1);
                        if (SetValue(txtSpell1, strMidleWord) == -1)
                        {
                            return;
                        }
                        #endregion
                        break;
                    case "X����":
                        #region X����
                        string firWord1 = this.txtName.Text.Substring(0, 1);
                        if (SetValue(txtSpell1, firWord1) == -1)
                        {
                            return;
                        }
                        #endregion
                        break;
                    case "ͬ������":
                        #region ͬ������
                        if (this.txtName.Text.Length == 1)
                        {
                            this.txtSpell2.Text = "";
                            this.txtSpell3.Text = "";
                            if (SetValue(txtSpell1, this.txtName.Text) == -1)
                            {
                                return;
                            }
                        }
                        else if (this.txtName.Text.Length == 2)
                        {
                            this.txtSpell3.Text = "";
                            string Fir = this.txtName.Text.Substring(0, 1);
                            string Sec = this.txtName.Text.Substring(1, 1);
                            if (SetValue(txtSpell1, Fir) == -1)
                            {
                                return;
                            }
                            if (SetValue(txtSpell2, Sec) == -1)
                            {
                                return;
                            }
                        }
                        else if (this.txtName.Text.Length == 3)
                        {
                            string Fir = this.txtName.Text.Substring(0, 1);
                            string Sec = this.txtName.Text.Substring(1, 1);
                            string Thir = this.txtName.Text.Substring(2, 1);
                            if (SetValue(txtSpell1, Fir) == -1)
                            {
                                return;
                            }
                            if (SetValue(txtSpell2, Sec) == -1)
                            {
                                return;
                            }
                            if (SetValue(txtSpell3, Thir) == -1)
                            {
                                return;
                            }
                        }
                        #endregion
                        break;
                    default:
                        MessageBox.Show("ѡ���ģʽ������");
                        this.comType.Focus();
                        break;
                }
                this.txtSpell1.Text.ToUpper();
                this.txtSpell2.Text.ToUpper();
                this.txtSpell3.Text.ToUpper();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="Word"></param>
        /// <returns></returns>
        private int SetValue(FS.FrameWork.WinForms.Controls.NeuTextBox col, string Word)
        {
            col.Text = "";
            string SpellCode = sp.GetSpellCode(Word);
            if (SpellCode == null)
            {
                MessageBox.Show("��ѯƴ����ʧ��" + sp.Err);
                return -1;
            }
            col.Text = SpellCode.ToUpper();
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (comType.Text)
            {
                case "��ȷ����":
                    this.txtSpell1.Visible = false;
                    this.txtSpell2.Visible = false;
                    this.txtSpell3.Visible = false;
                    break;
                case "ģ������":
                    this.txtSpell1.Visible = false;
                    this.txtSpell2.Visible = false;
                    this.txtSpell3.Visible = false;
                    break;
                case "���X":
                    this.txtSpell1.Visible = true;
                    this.txtSpell2.Visible = false;
                    this.txtSpell3.Visible = false;
                    break;
                case "��X��":
                    this.txtSpell1.Visible = true;
                    this.txtSpell2.Visible = false;
                    this.txtSpell3.Visible = false;
                    break;
                case "X����":
                    this.txtSpell1.Visible = true;
                    this.txtSpell2.Visible = false;
                    this.txtSpell3.Visible = false;
                    break;
                case "ͬ������":
                    this.txtSpell1.Visible = true;
                    this.txtSpell2.Visible = true;
                    this.txtSpell3.Visible = true;
                    break;
                default:
                    MessageBox.Show("ѡ���ģʽ������");
                    this.comType.Focus();
                    break;
            }
        }

    }
}
