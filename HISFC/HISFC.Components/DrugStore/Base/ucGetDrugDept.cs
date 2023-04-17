using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.HISFC.Components.DrugStore.Base
{
    /// <summary>
    /// [�ؼ�����:ucGetDrugDept]<br></br>
    /// [��������: Ĭ��ȡҩ����ά��<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-5]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='������' 
    ///		�޸�ʱ��='2007-01-31' 
    ///		�޸�Ŀ��='bug'
    ///		�޸�����='���������޸�'
    ///  />
    /// <�޸ļ�¼>
    ///    1������ҩ�������м�ɾ����ʾ by Sunjh 2010-8-23 {D77FC0F8-4BE1-4ce5-A303-AC788C9FA773} 
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucGetDrugDept : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucGetDrugDept( )
        {
            InitializeComponent( );
        }

        #region ����

        /// <summary>
        /// ���嵱ǰѡ�����
        /// </summary>
        private FS.FrameWork.Models.NeuObject currentDept = new FS.FrameWork.Models.NeuObject();

        //����Ĭ��ȡҩ����
        private FS.HISFC.BizLogic.Pharmacy.Constant phaConstant = new FS.HISFC.BizLogic.Pharmacy.Constant();
        //�������
        FS.FrameWork.Public.ObjectHelper objHelper = new FS.FrameWork.Public.ObjectHelper();

        //������
        private DataSet ds = new DataSet();
        private DataView dv;

        FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();       

        /// <summary>
        /// ҩ��������
        /// </summary>
        private string piStatCode = "S001";

        /// <summary>
        /// ҩ���������
        /// </summary>
        private string pStatCode = "S002";

        /// <summary>
        /// �Ƿ�ʹ�ÿ��ҽṹ����ʾ
        /// </summary>
        private bool isUseDeptStruct = false;

        #endregion

        #region ����

        /// <summary>
        /// ҩ��������
        /// </summary>
        [Description("���ҽṹ��ά����ҩ�������룬Ĭ��ΪS001"), Category("����"), DefaultValue("S001")]
        public string PIStatCode
        {
            get
            {
                return this.piStatCode;
            }
            set
            {
                this.piStatCode = value;
            }
        }

        /// <summary>
        /// ҩ���������
        /// </summary>
        [Description("���ҽṹ��ά����ҩ��������룬Ĭ��ΪS002"), Category("����"), DefaultValue("S002")]
        public string PStatCode
        {
            get
            {
                return this.pStatCode;
            }
            set
            {
                this.pStatCode = value;
            }
        }

        /// <summary>
        /// �Ƿ�ʹ�ÿ��ҽṹ����ʾ
        /// </summary>
        [Description("�Ƿ�ʹ�ÿ��ҽṹ����ʾ������ΪTrueʹ�ÿ��ҽṹ��ʾʱ����ע������PIStatCode��PStatCode����ֵ"), Category("����"), DefaultValue(false)]
        public bool IsUseDeptStruct
        {
            get
            {
                return this.isUseDeptStruct;
            }
            set
            {
                this.isUseDeptStruct = value;
            }
        }

        #endregion
        
        #region ����
        /// <summary>
        /// ��ʼ���б�
        /// </summary>
        private void SetFp( )
        {
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType( );
            FarPoint.Win.Spread.CellType.DateTimeCellType dateTimeCellType1 = new FarPoint.Win.Spread.CellType.DateTimeCellType( );
            dateTimeCellType1.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.TimeOnly;

            this.neuSpread1_Sheet1.Columns.Get( 0 ).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get( 0 ).Label = "ȡҩ���ұ���";
            this.neuSpread1_Sheet1.Columns.Get( 0 ).Visible = false;

            this.neuSpread1_Sheet1.Columns.Get( 1 ).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get( 1 ).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpread1_Sheet1.Columns.Get( 1 ).Label = "ȡҩ����";
            this.neuSpread1_Sheet1.Columns.Get( 1 ).Locked = true;
            this.neuSpread1_Sheet1.Columns.Get( 1 ).Width = 165F;

            this.neuSpread1_Sheet1.Columns.Get( 2 ).AllowAutoSort = true;
            this.neuSpread1_Sheet1.Columns.Get( 2 ).Label = "ҩƷ���";
            this.neuSpread1_Sheet1.Columns.Get( 2 ).Width = 82F;
            this.neuSpread1_Sheet1.Columns.Get( 2 ).CellType = comboBoxCellType1;

            this.neuSpread1_Sheet1.Columns.Get( 3 ).Label = "��ʼʱ��";
            this.neuSpread1_Sheet1.Columns.Get( 3 ).Width = 92F;
            this.neuSpread1_Sheet1.Columns.Get( 3 ).CellType = dateTimeCellType1;

            this.neuSpread1_Sheet1.Columns.Get( 4 ).Label = "����ʱ��";
            this.neuSpread1_Sheet1.Columns.Get( 4 ).Width = 92F;
            this.neuSpread1_Sheet1.Columns.Get( 4 ).CellType = dateTimeCellType1;

            this.neuSpread1_Sheet1.Columns.Get( 5 ).Label = "��ע";
            this.neuSpread1_Sheet1.Columns.Get( 5 ).Width = 119F;

            this.neuSpread1_Sheet1.Columns.Get( 6 ).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get( 6 ).Label = "ƴ����";
            this.neuSpread1_Sheet1.Columns.Get( 6 ).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get( 6 ).Width = 129F;

            this.neuSpread1_Sheet1.Columns.Get( 7 ).CellType = textCellType1;
            this.neuSpread1_Sheet1.Columns.Get( 7 ).Label = "�����";
            this.neuSpread1_Sheet1.Columns.Get( 7 ).Visible = false;
            this.neuSpread1_Sheet1.Columns.Get( 7 ).Width = 129F;
        }

        /// <summary>
        /// ��ʼ��DataSet���󶨵�neuSpread1_Sheet1
        /// </summary>
        private void InitDataSet( )
        {
            //��������
            System.Type dtStr = System.Type.GetType( "System.String" );
            System.Type dtTime = System.Type.GetType( "System.DateTime" );

            this.ds.Tables.Clear( );
            this.ds.Tables.Add( );

            //��DataSet�������
            this.ds.Tables[ 0 ].Columns.AddRange( new DataColumn[ ] {
																	new DataColumn("ȡҩ���ұ���",dtStr),
																	new DataColumn("ȡҩ����",    dtStr),
																	new DataColumn("ҩƷ���",	  dtStr),
																	new DataColumn("��ʼʱ��",	  dtTime),
																	new DataColumn("����ʱ��",    dtTime),
																	new DataColumn("��ע",	      dtStr),
																	new DataColumn("ƴ����",      dtStr),
																	new DataColumn("�����",      dtStr),
																} );
            this.dv = new DataView( this.ds.Tables[ 0 ] );

            this.dv.AllowDelete = true;
            this.dv.AllowEdit = true;
            this.dv.AllowNew = true;

            //�趨���ڶ�DataView�����ظ��м���������
            DataColumn[ ] keys = new DataColumn[ 3 ];
            keys[ 0 ] = this.ds.Tables[ 0 ].Columns[ "ȡҩ���ұ���"];
            keys[1] = this.ds.Tables[0].Columns["ҩƷ���"];
            keys[2] = this.ds.Tables[0].Columns["��ʼʱ��"];
            this.ds.Tables[ 0 ].PrimaryKey = keys;

            //���ݰ�
            this.neuSpread1_Sheet1.DataSource = this.dv;

            //�������ݸ�ʽ
            this.SetFp( );
        }

        /// <summary>
        /// Ϊ�������ҩƷ���������ؼ�
        /// </summary>
        private void InitGetDrugType( )
        {
            //ȡҩƷ��������
            FS.HISFC.BizLogic.Manager.Constant cons = new FS.HISFC.BizLogic.Manager.Constant( );
            FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject( );
            ArrayList alDrugType = new ArrayList( );

            alDrugType = cons.GetList( FS.HISFC.Models.Base.EnumConstant.ITEMTYPE );
            if( alDrugType == null ) 
                return;

            //��ҩƷ���͸�ֵ��objHelper�����ڸ��ݱ����������
            neuObj.ID = "A";
            neuObj.Name = "ȫ��";
            objHelper.ArrayObject.Add( neuObj );
            objHelper.ArrayObject.AddRange( alDrugType );

            //���������б�
            string[ ] str = new string[ objHelper.ArrayObject.Count ];
            for( int i = 0 ; i < objHelper.ArrayObject.Count ; i++ )
            {
                neuObj = objHelper.ArrayObject[ i ] as FS.FrameWork.Models.NeuObject;
                str[ i ] = neuObj.ID + neuObj.Name;
            }
            comboBoxCellType1.Items = str;
        }

        /// <summary>
        /// ��ʼ��Dataset
        /// </summary>
        /// <param name="al"></param>
        private void AddToDataSet( ArrayList al )
        {
            FS.HISFC.Models.Base.Spell spellobj = new FS.HISFC.Models.Base.Spell( );
            FS.HISFC.BizLogic.Manager.Spell spell = new FS.HISFC.BizLogic.Manager.Spell( );

            if( al.Count <= 0 )
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg("���ڼ�������.���Ժ�..."));
            Application.DoEvents();

            for( int i = 0 ; i < al.Count ; i++ )
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject( );
                obj = ( FS.FrameWork.Models.NeuObject )al[ i ];
                spellobj = spell.Get( obj.Name ) as FS.HISFC.Models.Base.Spell;
                if (spellobj == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(obj.Name + " �Զ����ɸÿ�ƴ����ʱ��������." + obj.ID + "����"));
                    continue;
                }

                //{BB505126-A265-4c62-9392-30D99503E36E}
                //�滻obj.User01.tostring() Ϊ FS.FrameWork.Function.NConvert.ToDateTime(obj.User01).ToLongTimeString()
                string[] key = { obj.ID, obj.User03 + this.objHelper.GetName(obj.User03), FS.FrameWork.Function.NConvert.ToDateTime(obj.User01).ToLongTimeString() };
                if (this.ds.Tables[0].Rows.Find(key) != null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(obj.Name + "�ÿ��Ѵ��� �����ظ����" + obj.ID + "�ÿ���"));
                    continue;
                }

                this.ds.Tables[ 0 ].Rows.Add( new object[ ]{
                    obj.ID,//ȡҩ���ұ���
                    obj.Name,//ȡҩ��������
                    obj.User03 + this.objHelper.GetName( obj.User03 ),//ҩƷ����
                    FS.FrameWork.Function.NConvert.ToDateTime(obj.User01).ToLongTimeString(),//��ʼʱ��
                    FS.FrameWork.Function.NConvert.ToDateTime(obj.User02).ToLongTimeString(),//����ʱ��
                    obj.Memo,//��ע
                    spellobj.SpellCode.Length > 10 ? spellobj.SpellCode.Substring(0,10):spellobj.SpellCode,//ƴ���뱣��ʮλ
                    spellobj.WBCode.Length > 10 ? spellobj.WBCode.Substring(0,10):spellobj.WBCode    //����뱣��ʮλ
                } );

            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.ds.Tables[ 0 ].AcceptChanges( );
            this.SetFp( );
        }

        /// <summary>
        /// ȡҩ������ҩ����
        /// </summary>
        private void GetDeptByStore( )
        {
            try
            {
                this.Clear();

                //������ű��Ϊ�շ���
                if( this.currentDept.ID == "" ) 
                    return;

                //���ȡҩ�����б�
                ArrayList al = phaConstant.QueryReciveDrugDept( this.currentDept.ID );

                //���������ӵ�DataSet
                this.AddToDataSet(al);
            }
            catch( Exception ex )
            {
                MessageBox.Show( "��ѡ��ҩ��/ҩ���ţ�" + ex.Message );
                return;
            }
        }

        /// <summary>
        /// ���ӿ���
        /// </summary>
        private void AddDept( )
        {
            if( this.currentDept.ID == "" ) 
                return;

            List<FS.HISFC.Models.Base.Department> selectedDeptList = Common.Classes.Function.ChooseMultiDept( ); ;
            ArrayList al = new ArrayList( );

            try
            {
                for( int i = 0 ; i < selectedDeptList.Count ; i++ )
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject( );
                    obj = ( FS.FrameWork.Models.NeuObject )selectedDeptList[ i ];
                    obj.ID = selectedDeptList[ i ].ID;
                    obj.Name = selectedDeptList[ i ].Name;
                    obj.User03 = "Aȫ��";
                    obj.User01 = "2000-01-01 00:00:00";
                    obj.User02 = "2000-01-01 23:59:59";

                    al.Add( obj );
                }

                //���������ӵ�DataSet
                this.AddToDataSet( al );
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        private void DeleteDept( )
        {
            //����ҩ�������м�ɾ����ʾ by Sunjh 2010-8-23 {D77FC0F8-4BE1-4ce5-A303-AC788C9FA773}
            if (MessageBox.Show("ȷ��ɾ����ǰѡ�������? ɾ�����豣����Ч", "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            try
            {
                //ѡ��һ����¼����ɾ����ҩ��/ҩ���Ų���Ϊ�գ��Ҳ�������ҩ����0������ѡ����
                if( this.neuSpread1_Sheet1.RowCount > 0 && this.neuSpread1_Sheet1.ActiveRowIndex >= 0 )
                {
                    //this.neuSpread1_Sheet1.RemoveRows( this.neuSpread1_Sheet1.ActiveRowIndex , 1 );
                    string[] key = { this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text ,
                                     this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex,2].Text,
                                     this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex,3].Text};
                    DataRow dr = this.ds.Tables[0].Rows.Find(key);
                    if (dr != null)
                    {
                        this.ds.Tables[0].Rows.Remove(dr);
                    }
                    //this.ds.Tables[ 0 ].Rows.RemoveAt( this.neuSpread1_Sheet1.ActiveRowIndex );
                    //this.ds.Tables[ 0 ].AcceptChanges( );
                }
                else
                {
                    MessageBox.Show( "��ѡ��ɾ���ļ�¼��" );
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show( this , ex.Message , "ȡҩ����ά��" , MessageBoxButtons.OK , MessageBoxIcon.Information );
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void SaveDept( )
        {
            if( this.currentDept.ID == "" )
                return;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction( FS.FrameWork.Management.Connection.Instance );
            //t.BeginTransaction( );

            this.phaConstant.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                //���ԭ������
                if( this.phaConstant.DelAllDrugRoom( this.currentDept.ID ) == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( this , "���ݱ������" + this.phaConstant.Err , "�������" , MessageBoxButtons.OK , MessageBoxIcon.Information );
                    return;
                }

                for(int i = 0;i < this.ds.Tables[0].Rows.Count;i++)
                {
                    this.ds.Tables[0].Rows[i].EndEdit();
                }

                this.dv.RowFilter = "1=1";

                //װ��������
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject( );
                for( int i = 0 ; i < this.neuSpread1_Sheet1.RowCount ; i++ )
                {
                    obj.ID = this.currentDept.ID;									        //ҩ��/ҩ����
                    obj.Name = this.neuSpread1_Sheet1.Cells[ i , 0 ].Text;					//ȡҩ���ұ��
                    obj.User03 = this.neuSpread1_Sheet1.Cells[ i , 2 ].Text.Substring( 0 , 1 );	//ҩƷ����
                    obj.User01 = "2000-01-01 " + this.neuSpread1_Sheet1.Cells[ i , 3 ].Text;	//��ʼʱ�� ���ڶ��̶�Ϊ2000-01-01
                    obj.User02 = "2000-01-01 " + this.neuSpread1_Sheet1.Cells[ i , 4 ].Text;	//����ʱ�� ���ڶ��̶�Ϊ2000-01-01
                    obj.Memo = this.neuSpread1_Sheet1.Cells[ i , 5 ].Text;					//��ע

                    //�������¼���Ƿ���Ч
                    try
                    {
                        if( DateTime.Parse( obj.User01 ) >= DateTime.Parse( obj.User02 ) )
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.neuSpread1_Sheet1.ActiveRowIndex = i;
                            MessageBox.Show( "��ʼʱ�����С�ڽ���ʱ��" + "\n��������:" + this.neuSpread1_Sheet1.Cells[ i , 1 ].Text , "������ʾ" );
                            return;
                        }
                    }
                    catch
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.neuSpread1_Sheet1.ActiveRowIndex = i;
                        MessageBox.Show( "��������Ч�Ŀ�ʼʱ��ͽ���ʱ��" + "\n��������:" + this.neuSpread1_Sheet1.Cells[ i , 1 ].Text , "������ʾ" );
                        return;
                    }

                    //���ݸ���
                    if( this.phaConstant.InsertDrugRoom( obj ) != 1 )
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        if( this.phaConstant.DBErrCode == 1 )
                        {
                            this.neuSpread1_Sheet1.ActiveRowIndex = i;
                            MessageBox.Show( "�����ظ�,������ͬ�ļ�¼����.��ά����ͬ�ļ�¼." , "������ʾ" );
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.ActiveRowIndex = i;
                            MessageBox.Show( this , "���ݱ������" + this.phaConstant.Err , "�������" , MessageBoxButtons.OK , MessageBoxIcon.Information );
                        }
                        return;
                    }
                }
            }
            catch( Exception e )
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show( e.Message );
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show( "����ɹ���" );
        }

        /// <summary>
        /// ��ʾ���
        /// </summary>
        protected void Clear()
        {
            if (this.ds != null && this.ds.Tables.Count > 0)
                this.ds.Tables[0].Rows.Clear();
        }
 
        #endregion

        #region �¼�

        /// <summary>
        /// �ؼ�װ���¼�
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.InitDataSet();

                this.InitGetDrugType();

                FS.HISFC.Components.DrugStore.Base.tvDeptTree tvDeptTree = this.tv as FS.HISFC.Components.DrugStore.Base.tvDeptTree;

                if (tvDeptTree != null)
                {
                    tvDeptTree.IsUseDeptStruct = this.isUseDeptStruct;
                    tvDeptTree.Reset();
                }
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// ��������ؼ�ͨѶ
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue( object neuObject , TreeNode e )
        {
            try
            {
                if( e != null && e.Tag != null)
                {
                    //��ǰѡ��Ŀ���
                    this.currentDept = e.Tag as FS.FrameWork.Models.NeuObject;

                    //��ʾ��ǰҩ����ҩ�����ҩ����
                    this.GetDeptByStore( );
                }
                else
                {
                    this.Clear();

                    this.currentDept = new FS.FrameWork.Models.NeuObject( );
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message , "��ʾ" );
            }
            return base.OnSetValue( neuObject , e );
        }

        /// <summary>
        /// ���ҹ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBox1_TextChanged( object sender , EventArgs e )
        {
            if( this.ds.Tables[ 0 ].Rows.Count == 0 ) return;

            try
            {
                string queryCode = "";
                queryCode = "%" + this.neuTextBox1.Text.Trim( ) + "%";

                string filter = "(ƴ���� LIKE '" + queryCode + "') OR " + "(����� LIKE '" + queryCode + "') OR " + "(ȡҩ���ұ��� LIKE '" + queryCode + "') ";

                //���ù�������
                this.dv.RowFilter = filter;
                this.SetFp( );
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }

        }

        /// <summary>
        /// ��Ӧ�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBox1_KeyDown( object sender , KeyEventArgs e )
        {
            if( e.KeyCode == Keys.Down )
            {
                this.neuSpread1_Sheet1.ActiveRowIndex++;
                this.neuSpread1_Sheet1.AddSelection( this.neuSpread1_Sheet1.ActiveRowIndex , 0 , 1 , 0 );
                return;
            }

            if( e.KeyCode == Keys.Up )
            {
                this.neuSpread1_Sheet1.ActiveRowIndex--;
                this.neuSpread1_Sheet1.AddSelection( this.neuSpread1_Sheet1.ActiveRowIndex , 0 , 1 , 0 );
                return;
            }
        }

        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService( );
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit( object sender , object NeuObject , object param )
        {
            //���ӹ�����
            this.toolBarService.AddToolButton( "����" , "����ȡҩ����" , FS.FrameWork.WinForms.Classes.EnumImageList.T��� , true , false , null );
            this.toolBarService.AddToolButton( "ɾ��" , "ɾ��ȡҩ����" , FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ�� , true , false , null );
            return this.toolBarService;
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave( object sender , object neuObject )
        {
            this.SaveDept( );
            return base.OnSave( sender , neuObject );
        }

        /// <summary>
        /// ��������ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked( object sender , ToolStripItemClickedEventArgs e )
        {
            switch( e.ClickedItem.Text )
            {
                case "����":
                    this.AddDept( );
                    break;
                case "ɾ��":
                    this.DeleteDept( );
                    break;
            }

        }

        #endregion

    }
}
