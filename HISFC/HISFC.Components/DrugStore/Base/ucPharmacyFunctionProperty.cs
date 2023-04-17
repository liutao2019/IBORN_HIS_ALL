using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.DrugStore.Base
{
    /// <summary>
    /// [�ؼ�����:ucPharmacyFunctionProperty]<br></br>
    /// [��������: ҩ���������Կؼ�<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-17]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPharmacyFunctionProperty : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// �������Ĺ�����
        /// </summary>
        /// <param name="nodeCode">�ڵ����</param>
        /// <param name="operKind">��������</param>
        public ucPharmacyFunctionProperty(string nodecode, string operkind, int gridLevel)
        {
            InitializeComponent( );

            this.operKind = operkind;
            this.nodeCode = nodecode;
            //{FF5503FA-0057-413e-BF08-5A8C1DCF7ED8}  ҩ�����ü���
            this.gridLevel = gridLevel;

            this.cmbparent.Enabled = false;
        }

        /// <summary>
        /// ���������Ĺ�������ͨ�����Ը�ֵ
        /// </summary>
        public ucPharmacyFunctionProperty( )
        {
            this.cmbparent.Enabled = false;
        }

        #region ����

        /// <summary>
        /// �ɸ����ڴ������Ľڵ����
        /// </summary>
        private string nodeCode;

        /// <summary>
        /// ��������UPDATE/DELETE/INSERT
        /// </summary>
        private string operKind;

        /// <summary>
        /// ҩ��������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant pharmacyConstant = new FS.HISFC.BizLogic.Pharmacy.Constant( );

        private FS.HISFC.Models.Pharmacy.PhaFunction functionObject = null ;

        /// <summary>
        /// ҩ�����ü���
        /// 
        /// //{C8D1015E-41B3-4e90-B624-8B47CF81E665} У��ҩ�����������ظ�
        /// </summary>
        private static Hashtable hsFunctionNameDictionary = new Hashtable();

        /// <summary>
        /// ��ǰҩ�����ü���   {FF5503FA-0057-413e-BF08-5A8C1DCF7ED8}
        /// </summary>
        private int gridLevel;

        #endregion

        #region ����

        /// <summary>
        /// �ɸ����ڴ������Ľڵ����
        /// </summary>
        [Description( "�ɸ����ڴ������Ľڵ����" )]
        public string NodeCode
        {
            get
            {
                return nodeCode;
            }
            set
            {
                nodeCode = value;
            }
        }

        /// <summary>
        /// ��������UPDATE/DELETE/INSERT
        /// </summary>
        [Description( "��������UPDATE/DELETE/INSERT" )]
        public string OperKind
        {
            get
            {
                return operKind;
            }
            set
            {
                operKind = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ҩ����������У��
        /// 
        /// //{C8D1015E-41B3-4e90-B624-8B47CF81E665} У��ҩ�����������ظ�
        /// </summary>
        protected bool VerifyFunctionName()
        {
            if (this.txtName.Tag != null)
            {
                if (this.txtName.Tag.ToString() == this.txtName.Text)           //����δ��
                {
                    return true;
                }
            }

            if (hsFunctionNameDictionary.Count == 0)
            {
                FS.HISFC.BizProcess.Integrate.Manager drugconstant = new FS.HISFC.BizProcess.Integrate.Manager();

                ArrayList al = drugconstant.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PHYFUNCTION);
                if (al == null)
                {
                    MessageBox.Show("����ҩ�����ü��Ϸ�������" + drugconstant.Err);
                    return false;
                }

                foreach (FS.FrameWork.Models.NeuObject info in al)
                {
                    if (hsFunctionNameDictionary.ContainsKey(info.Name) == false)
                    {
                        hsFunctionNameDictionary.Add(info.Name, null);
                    }
                }
            }

            if (this.operKind == "DELETE")          //ɾ��
            {
                if (hsFunctionNameDictionary.ContainsKey(this.txtName.Text) == true)
                {
                    hsFunctionNameDictionary.Remove(this.txtName.Text);
                }
            }
            else
            {
                if (hsFunctionNameDictionary.ContainsKey(this.txtName.Text) == true)
                {
                    DialogResult rs = MessageBox.Show(this.txtName.Text + "  ҩ�����������Ѵ��ڣ��Ƿ�ȷ����ӣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.Yes)
                    {
                        return true;
                    }
                    else if (rs == DialogResult.No)
                    {
                        return false;
                    }
                }
                else
                {
                    hsFunctionNameDictionary.Add(this.txtName.Text, null);
                }
            }

            return true;
        }

        /// <summary>
        /// ������Ч�Լ���  {FF5503FA-0057-413e-BF08-5A8C1DCF7ED8}
        /// </summary>
        /// <returns>�ɹ�����True  ʧ�ܷ��� False</returns>
        protected bool IsDataValid()
        {
            #region ��Ч���ж�

            //�ڵ����Ʋ���Ϊ��
            if (this.txtName.Text.Trim() == null || this.txtName.Text.Trim() == "")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ҩ���������Ʋ�����գ�" ), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return false;
            }
            if (this.txtName.Text.Trim().Length > 25)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ҩ���������Ƴ��� �����" ) );
                return false;
            }

            //�ڵ���벻��Ϊ���Ҳ���Ϊ0
            if (this.txtCode.Text.Trim() == null || this.txtCode.Text.Trim() == "" || this.txtCode.Text.Trim() == "0")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "���벻�����ֵ���㣡" ), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return false;
            }
            if (this.txtCode.Text.Trim().Length > 30)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "���볬�� �����" ), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return false;
            }

            //�ַ���Ч�ж�
            if (this.txtCode.Text.IndexOfAny( new char[] { '@', '.', ',', '!', '-', '#', '$', '%', '^', '&', '*', '[', ']', '|', '}', '\'', '?' } ) != -1)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��Ч�ı���.\n" +
                    "��Ч�ַ�:  '@', '.', ',', '!', '-', '#', '$', '%', '^', '&', '*', '[', ']', '|', '}','\'','?'" ), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return false;
            }

            if (this.txtName.Text.IndexOfAny( new char[] { '@', '.', ',', '!', '-', '#', '$', '%', '^', '&', '*', '[', ']', '|', '}', '\'', '?' } ) != -1)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��Ч��ҩ����������.\n" +
                    "��Ч�ַ�:  '@', '.', ',', '!', '-', '#', '$', '%', '^', '&', '*', '[', ']', '|', '}','\'','?'" ), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return false;
            }

            //��ע�����ж�
            if (this.txtMark.Text.Length > 200)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ע�ֶγ��� ���ʵ�����" ), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return false;
            }

            #endregion

            return true;
        }

        /// <summary>
        /// �ɽ����ȡҩ��������Ϣ   {FF5503FA-0057-413e-BF08-5A8C1DCF7ED8}
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int GetPhaFunction()
        {
            //����ǵ�ǰû�и��ڵ㣬��Ϊ��һ��
            if (string.IsNullOrEmpty( this.cmbparent.Text ) == false)       //�ϼ��ڵ�ǿ�
            {
                functionObject.ParentNode = this.cmbparent.SelectedItem.ID;
            }
            else
            {
                functionObject.ParentNode = "0";
            }

            functionObject.ID = this.txtCode.Text.Trim();//ҩ�����ñ���
            functionObject.Name = this.txtName.Text.Trim();  //ҩ����������
            functionObject.WBCode = this.txtWBCode.Text.Trim(); //�����
            functionObject.SpellCode = this.txtSpellCode.Text.Trim();    //ƴ����

            functionObject.GradeLevel = this.gridLevel;

            functionObject.Memo = this.txtMark.Text.Trim();//��ע
            if (this.txtSortId.Text == "")
            {
                functionObject.SortID = 0;
            }
            else
            {
                functionObject.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.txtSortId.Text);
            }
            functionObject.Oper.ID = pharmacyConstant.Operator.ID;
            functionObject.Oper.OperTime = pharmacyConstant.GetDateTimeFromSysDateTime();

            //ҩ�������Ƿ���Ч
            functionObject.IsValid = this.chkIsValid.Checked;

            return 1;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            //{FF5503FA-0057-413e-BF08-5A8C1DCF7ED8} ��Ч��У��  ����ʵ���ȡ��ʽ
            if (this.IsDataValid() == false)
            {
                return -1;
            }

            //ʵ���ʼ��
            this.functionObject = new FS.HISFC.Models.Pharmacy.PhaFunction();

            this.GetPhaFunction();
            //{FF5503FA-0057-413e-BF08-5A8C1DCF7ED8}

            //�ж��Ƿ����ӽڵ�
            ArrayList al = new ArrayList();
            al = pharmacyConstant.QueryFunctionByParentNode( functionObject.ID );
            if (al != null)
            {
                int ifleave = al.Count;
                if (ifleave > 0)                 //���ӽڵ�   ��������
                {
                    functionObject.NodeKind = 1;//Ҷ�ӽڵ�
                }
                else
                {
                    functionObject.NodeKind = 0;//��Ҷ�ӽڵ�
                }
            }
            else
            {
                functionObject.NodeKind = 0;//��Ҷ�ӽڵ�
            }

            //�ж��Ƿ��ظ�
            if (this.operKind == "INSERT")//����
            {
                int i = 0;
                ArrayList alfun = new ArrayList();
                alfun = pharmacyConstant.QueryFunctionByNode( functionObject.ID );
                if (alfun != null)
                {
                    i = alfun.Count;
                    if (i >= 1)
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�ýڵ���" ) + this.txtCode.Text.Trim() + FS.FrameWork.Management.Language.Msg( "�Ѵ��ڣ�" ), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );//�ڵ��Ѵ��ڵĲ����ٲ���
                        return -1;
                    }
                }
            }
            ////����ɾ���Ĳ���
            try
            {
                if (pharmacyConstant.SetFunction( functionObject, this.operKind ) == -1)
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "����ҩ��������Ϣʧ��" ), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
                return -1;
            }
        }

        #endregion

        #region �¼�

        private void btnOk_Click( object sender , EventArgs e )
        {
            //{C8D1015E-41B3-4e90-B624-8B47CF81E665} У��ҩ����������
            if (this.VerifyFunctionName() == false)
            {
                return;
            }

            if( Save( ) == 0 )
            {
                this.ParentForm.Close( );
                this.FindForm( ).DialogResult = DialogResult.OK;
            }
        }
        /// <summary>
        /// ȡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click( object sender , EventArgs e )
        {
            this.ParentForm.Close( );
            this.FindForm( ).DialogResult = DialogResult.Cancel;
        }
         /// <summary>
         /// .�Զ�����ƴ����������
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void txtName_Leave( object sender , EventArgs e )
        {
            if (this.txtName.Text.IndexOfAny(new char[] { '@', '.', ',', '!', '-', '#', '$', '%', '^', '&', '*', '[', ']', '|', '}', '\'', '?' }) != -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ч��ҩ����������.\n" +
                    "��Ч�ַ�:  '@', '.', ',', '!', '-', '#', '$', '%', '^', '&', '*', '[', ']', '|', '}','\'','?'"));
                this.txtName.Focus();
                this.txtName.SelectAll();
                return;
            }

            FS.HISFC.Models.Base.Spell spellobj = new FS.HISFC.Models.Base.Spell( );
            FS.HISFC.BizLogic.Manager.Spell spell = new FS.HISFC.BizLogic.Manager.Spell( );

            spellobj = spell.Get( this.txtName.Text.Trim() ) as FS.HISFC.Models.Base.Spell;

            this.txtSpellCode.Text = spellobj.SpellCode;
            this.txtWBCode.Text = spellobj.WBCode;
        }
        /// <summary>
        /// ��Ӧ�����¼����ûس�������Tab�¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey( Keys keyData )
        {
            if( keyData == Keys.Enter )
            {
                System.Windows.Forms.SendKeys.Send( "{TAB}" );
            }
            return base.ProcessDialogKey( keyData );
        }
        /// <summary>
        /// �ؼ���ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            functionObject = new FS.HISFC.Models.Pharmacy.PhaFunction( );
            ArrayList al = new ArrayList( );
            al = this.pharmacyConstant.QueryPhaFunction( );
            if( al != null)
            {
                //��ʼ�������ڵ��б�
                this.cmbparent.AddItems(al );
            }
            //������޸Ļ���ɾ��
            if( this.operKind == "UPDATE" || this.operKind == "DELETE" )
            {
                functionObject = ( FS.HISFC.Models.Pharmacy.PhaFunction )this.pharmacyConstant.QueryFunctionByNode( this.nodeCode )[ 0 ];

                ////{FF5503FA-0057-413e-BF08-5A8C1DCF7ED8}  ��ȡԭʼֵ��ԭʼҩ�����ýڵ�
                this.gridLevel = functionObject.GradeLevel;

                this.cmbparent.Tag = functionObject.ParentNode;//�����ڵ�
                this.txtName.Text = functionObject.Name;//�ڵ���
                //{C8D1015E-41B3-4e90-B624-8B47CF81E665} У��ҩ�����������ظ�
                this.txtName.Tag = functionObject.Name;

                this.txtCode.Enabled = false;
                this.txtCode.Text = functionObject.ID;//�ڵ�ID
                this.txtWBCode.Text = functionObject.WBCode;
                this.txtSpellCode.Text = functionObject.SpellCode;
                this.txtMark.Text = functionObject.Memo;
                this.txtSortId.Text = functionObject.SortID.ToString();
                if( functionObject.IsValid  )
                {
                    this.chkIsValid.Checked = true;
                }
  
            }
            //���������
            if( this.operKind == "INSERT" )
            {
                this.txtName.Text = null;//�ڵ���
                this.txtCode.Text = null;
                this.txtWBCode.Text = null;
                this.txtSpellCode.Text = null;
                this.txtMark.Text = null;
                this.chkIsValid.Checked = true;
                this.cmbparent.Tag = this.nodeCode;//����Ľڵ���Ϊ���ڵ�
            }
            base.OnLoad( e );
        }
        #endregion
    }
}
