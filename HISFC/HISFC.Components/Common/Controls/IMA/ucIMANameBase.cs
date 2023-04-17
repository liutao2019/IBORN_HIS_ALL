using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.UFC.Common.Controls.IMA
{
    public partial class ucIMANameBase : UserControl
    {
        public ucIMANameBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ǰ��������Ŀ����ʵ��
        /// </summary>
        private Neusoft.HISFC.Object.IMA.NameService nameCollection = null;

        #region ����

        /// <summary>
        /// �Ƿ�ά��ͨ����
        /// </summary>
        [Description("�Ƿ���Ҫά��ͨ����"),Category("����"),DefaultValue(true)]
        public bool IsSetRegular
        {
            get
            {
                return this.panelRegular.Visible;
            }
            set
            {
                this.panelRegular.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ�����ͨ����ά�����Tab˳��
        /// </summary>
        [Description("�Ƿ�����ͨ����ά�����Tab˳��"), Category("����"), DefaultValue(false)]
        public bool IsRegularTabOrder
        {
            get
            {
                return this.txtRegularName.TabStop;
            }
            set
            {
                this.txtRegularName.TabStop = value;
                this.txtRegularUserCode.TabStop = value;
            }
        }

        /// <summary>
        /// �Ƿ�ά��ѧ��
        /// </summary>
        [Description("�Ƿ���Ҫά��ѧ��"), Category("����"), DefaultValue(true)]
        public bool IsSetFormal
        {
            get
            {
                return this.panelFormal.Visible;
            }
            set
            {
                this.panelFormal.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ�����ѧ��ά�����Tab˳��
        /// </summary>
        [Description("�Ƿ�����ѧ����ά�����Tab˳��"), Category("����"), DefaultValue(false)]
        public bool IsFormalTabOrder
        {
            get
            {
                return this.txtFormalName.TabStop;
            }
            set
            {
                this.txtFormalName.TabStop = value;
                this.txtFormalUserCode.TabStop = value;
            }
        }

        /// <summary>
        /// �Ƿ�ά������
        /// </summary>
        [Description("�Ƿ���Ҫά������"), Category("����"), DefaultValue(true)]
        public bool IsSetOhter
        {
            get
            {
                return this.panelOther.Visible;
            }
            set
            {
                this.panelOther.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ��������ά�����Tab˳��
        /// </summary>
        [Description("�Ƿ��������ά�����Tab˳��"), Category("����"), DefaultValue(false)]
        public bool IsOtherTabOrder
        {
            get
            {
                return this.txtOtherName.TabStop;
            }
            set
            {
                this.txtOtherName.TabStop = value;
                this.txtOtherName.TabStop = value;
            }
        }

        /// <summary>
        /// �Ƿ�ά��Ӣ����
        /// </summary>
        [Description("�Ƿ���Ҫά��Ӣ����"), Category("����"), DefaultValue(true)]
        public bool IsSetEnglish
        {
            get
            {
                return this.panelEnglish.Visible;
            }
            set
            {
                this.panelEnglish.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ӣ����ά�����Tab˳��
        /// </summary>
        [Description("�Ƿ�����Ӣ����ά�����Tab˳��"), Category("����"), DefaultValue(false)]
        public bool IsEnglishTabOrder
        {
            get
            {
                return this.txtEnglishName.TabStop;
            }
            set
            {
                this.txtEnglishName.TabStop = value;
                this.txtEnglishOtherName.TabStop = value;
                this.txtEnglishRegularName.TabStop = value;
            }
        }

        /// <summary>
        /// �Ƿ�ά������/���ʱ���
        /// </summary>
        [Description("�Ƿ���Ҫά������/���ʱ���"), Category("����"), DefaultValue(true)]
        public bool IsSetCode
        {
            get
            {
                return this.panelCode.Visible;
            }
            set
            {
                this.panelCode.Visible = value;
            }
        }


        /// <summary>
        /// �Ƿ��������ά�����Tab˳��
        /// </summary>
        [Description("�Ƿ��������ά�����Tab˳��"), Category("����"), DefaultValue(false)]
        public bool IsCodeTabOrder
        {
            get
            {
                return this.txtGbCode.TabStop;
            }
            set
            {
                this.txtGbCode.TabStop = value;
                this.txtInternationalCode.TabStop = value;
            }
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        public new void Focus()
        {
            this.txtName.Focus();
            this.txtName.SelectAll();
        }

        /// <summary>
        /// ���ݴ���ʵ������������ʾ
        /// </summary>
        /// <param name="nameCollection">��Ŀ����ʵ��</param>
        public void SetData(Neusoft.HISFC.Object.IMA.NameService nameCollection)
        {
            if (nameCollection != null)
            {
                this.txtName.Text = nameCollection.Name;
                this.txtSpellCode.Text = nameCollection.SpellCode;
                this.txtWbCode.Text = nameCollection.WBCode;
                this.txtUserCode.Text = nameCollection.UserCode;
                this.txtRegularName.Text = nameCollection.RegularName;
                this.txtRegularSpellCode.Text = nameCollection.RegularSpell.SpellCode;
                this.txtRegularWbCode.Text = nameCollection.RegularSpell.WBCode;
                this.txtRegularUserCode.Text = nameCollection.UserCode;
                this.txtFormalName.Text = nameCollection.FormalName;
                this.txtFormalSpellCode.Text = nameCollection.FormalSpell.SpellCode;
                this.txtFormalWbCode.Text = nameCollection.FormalSpell.WBCode;
                this.txtFormalUserCode.Text = nameCollection.FormalSpell.UserCode;
                this.txtOtherName.Text = nameCollection.OtherName;
                this.txtOtherSpellCode.Text = nameCollection.OtherSpell.SpellCode;
                this.txtOtherWbCode.Text = nameCollection.OtherSpell.WBCode;
                this.txtOtherUserCode.Text = nameCollection.OtherSpell.UserCode;
                this.txtEnglishName.Text = nameCollection.EnglishName;
                this.txtEnglishOtherName.Text = nameCollection.EnglishOtherName;
                this.txtEnglishRegularName.Text = nameCollection.EnglishRegularName;
                this.txtGbCode.Text = nameCollection.GbCode;
                this.txtInternationalCode.Text = nameCollection.InternationalCode;
            }

            this.nameCollection = nameCollection;
        }

        /// <summary>
        /// ��ȡ��ǰ��ʾ��Ŀ����ʵ��
        /// </summary>
        /// <returns></returns>
        public Neusoft.HISFC.Object.IMA.NameService GetData()
        {
            if (this.nameCollection == null)
                this.nameCollection = new Neusoft.HISFC.Object.IMA.NameService();

            this.nameCollection.Name = this.txtName.Text;
            this.nameCollection.SpellCode = this.txtSpellCode.Text;
            this.nameCollection.WBCode = this.txtWbCode.Text;
            this.nameCollection.UserCode = this.txtUserCode.Text;
            this.nameCollection.RegularName = this.txtRegularName.Text;
            this.nameCollection.RegularSpell.SpellCode = this.txtRegularSpellCode.Text;
            this.nameCollection.RegularSpell.WBCode = this.txtRegularWbCode.Text;
            this.nameCollection.RegularSpell.UserCode = this.txtRegularUserCode.Text;
            this.nameCollection.FormalName = this.txtFormalName.Text;
            this.nameCollection.FormalSpell.SpellCode = this.txtFormalSpellCode.Text;
            this.nameCollection.FormalSpell.WBCode = this.txtFormalWbCode.Text;
            this.nameCollection.FormalSpell.UserCode = this.txtFormalUserCode.Text;
            this.nameCollection.OtherName = this.txtOtherName.Text;
            this.nameCollection.OtherSpell.SpellCode = this.txtOtherSpellCode.Text;
            this.nameCollection.OtherSpell.WBCode = this.txtOtherWbCode.Text;
            this.nameCollection.OtherSpell.UserCode = this.txtOtherUserCode.Text;
            this.nameCollection.EnglishName = this.txtEnglishName.Text;
            this.nameCollection.EnglishOtherName = this.txtEnglishOtherName.Text;
            this.nameCollection.EnglishRegularName = this.txtEnglishRegularName.Text;
            this.nameCollection.GbCode = this.txtGbCode.Text;
            this.nameCollection.InternationalCode = this.txtInternationalCode.Text;

            return this.nameCollection;
        }

        /// <summary>
        /// ���ݴ�����ַ�����ȡƴ����
        /// </summary>
        ///<returns>���ش����ַ�����ƴ����ʵ��</returns>
        private Neusoft.HISFC.Object.Base.Spell GetSpell(string strData)
        {
            Neusoft.HISFC.Management.Manager.Spell spellManager = new Neusoft.HISFC.Management.Manager.Spell();
            Neusoft.HISFC.Object.Base.Spell spellCode = (Neusoft.HISFC.Object.Base.Spell)spellManager.Get(strData.Trim());
            if (spellCode == null)
                return new Neusoft.HISFC.Object.Base.Spell();
            else
                return spellCode;
        }

        /// <summary>
        /// ����ƴ����
        /// </summary>
        private void JudgeEnter()
        {
            if (this.txtName.Focused)
            {
                this.txtSpellCode.Text = this.GetSpell(this.txtName.Text.Trim()).SpellCode;
                this.txtWbCode.Text = this.GetSpell(this.txtName.Text.Trim()).WBCode;
            }
            if (this.txtRegularName.Focused)
            {
                this.txtRegularSpellCode.Text = this.GetSpell(this.txtRegularName.Text.Trim()).SpellCode;
                this.txtRegularWbCode.Text = this.GetSpell(this.txtRegularName.Text.Trim()).WBCode;
            }
            if (this.txtFormalName.Focused)
            {
                this.txtFormalSpellCode.Text = this.GetSpell(this.txtFormalName.Text.Trim()).SpellCode;
                this.txtFormalWbCode.Text = this.GetSpell(this.txtFormalName.Text.Trim()).WBCode;
            }
            if (this.txtOtherName.Focused)
            {
                this.txtOtherSpellCode.Text = this.GetSpell(this.txtOtherName.Text.Trim()).SpellCode;
                this.txtOtherWbCode.Text = this.GetSpell(this.txtOtherName.Text.Trim()).WBCode;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.JudgeEnter();
                SendKeys.Send("{TAB}");
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
