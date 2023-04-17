using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.NFC.Management;

namespace Neusoft.UFC.DrugStore.Outpatient
{
    /// <summary>
    /// [�ؼ�����:DrugTerminalTemplate]<br></br>
    /// [��������: �����ն�ģ������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-27]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class DrugTerminalTemplate : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public DrugTerminalTemplate( )
        {
            InitializeComponent( );
            this.ucDrugTerminalList1.SelectTerminalEvent += new ucDrugTerminalList.SelectTerminalHandler( ucDrugTerminalList1_SelectTerminalEvent );
            this.ucDrugTerminalList1.SelectTerminalDoubleClickedEvent += new ucDrugTerminalList.SelectTerminalDoubleClickedHandler( ucDrugTerminalList1_SelectTerminalDoubleClickedEvent );
        }

        #region ����

        /// <summary>
        /// ����ԱȨ�޿���
        /// </summary>
        private Neusoft.NFC.Object.NeuObject privDept = new Neusoft.NFC.Object.NeuObject( );

        /// <summary>
        /// ҵ��������
        /// </summary>
        private Neusoft.HISFC.Management.Pharmacy.DrugStore drugStore = new Neusoft.HISFC.Management.Pharmacy.DrugStore( );

        /// <summary>
        /// ��ǰ������ģ����Ϣ
        /// </summary>
        private Neusoft.NFC.Object.NeuObject currTemplate = null;

        /// <summary>
        /// ��ǰ��ѡ�е��ն���Ϣ
        /// </summary>
        private Neusoft.HISFC.Object.Pharmacy.DrugTerminal currTerminal = null;

        /// <summary>
        /// �Ƿ������ڵ�
        /// </summary>
        private bool isNew = false;

        /// <summary>
        /// �Ƿ���Ȩ�ޱ༭
        /// </summary>
        private bool isPrivilegeEdit = false;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ�����༭
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return this.isPrivilegeEdit;
            }
            set
            {
                this.isPrivilegeEdit = value;
                this.toolBarService.SetToolButtonEnabled( "�½�ģ��" , value );
                this.toolBarService.SetToolButtonEnabled( "ɾ��ģ��" , value );
                this.toolBarService.SetToolButtonEnabled( "����" , value );
                //this.toolBarService.SetToolButtonEnabled( "ִ��ģ��" , value );
                this.toolBarService.SetToolButtonEnabled( "ɾ���ն�" , value );
                this.toolBarService.SetToolButtonEnabled( "�����ն�" , value );
                //��Ȩ�ޱ༭���û������ն�����
                if( !this.isPrivilegeEdit )
                {
                    this.HideDrugTerminal( );
                }
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʾģ���б�
        /// </summary>
        private void ShowTemplateList( )
        {
            //����б�
            this.neuTreeView1.Nodes.Clear( );

            ArrayList al = this.drugStore.QueryDrugOpenTerminalByDeptCode( this.privDept.ID );

            if( al == null )
            {
                MessageBox.Show( this.drugStore.Err );
                return;
            }

            //��Ӹ��ڵ�
            if( al.Count > 0 )
            {
                this.neuTreeView1.Nodes.Add( new TreeNode( "ģ���б�" , 0 , 0 ) );
            }
            else
            {
                this.neuTreeView1.Nodes.Add( new TreeNode( "�޿���ģ��" , 0 , 0 ) );
            }

            //���ģ����Ϣ�б� Text ���� Tag ģ����
            foreach( Neusoft.NFC.Object.NeuObject info in al )
            {
                TreeNode node = new TreeNode( );
                node.Text = info.Name;				//ģ������
                node.ImageIndex = 1;
                node.SelectedImageIndex = 1;
                node.Tag = info.ID;					//ģ����

                this.neuTreeView1.Nodes[ 0 ].Nodes.Add( node );
            }

            this.neuTreeView1.Nodes[ 0 ].ExpandAll( );
            this.neuTreeView1.SelectedNode = this.neuTreeView1.Nodes[ 0 ];
        }

        /// <summary>
        /// ��ʾģ������
        /// </summary>
        private void ShowTemplateData( )
        {
            //���
           this.neuSpread1_Sheet1.Rows.Count = 0;
            //�����ǰû��ѡ��ģ�壬�򷵻�
            if( string.IsNullOrEmpty(this.currTemplate.ID ))
            {
                return;
            }

            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm( Language.Msg("���ڼ����ն�ģ����Ϣ...." ));
            Application.DoEvents( );

            //��ȡģ����ϸ��Ϣ
            ArrayList al = this.drugStore.QueryDrugOpenTerminalById( this.currTemplate.ID );
            if( al == null )
            {
                MessageBox.Show( Language.Msg("��ȡ�����ն�ģ����Ϣ����!") + this.drugStore.Err );
                return;
            }

            this.neuSpread1_Sheet1.Rows.Count = al.Count;
            Neusoft.NFC.Object.NeuObject info;
            Neusoft.HISFC.Object.Pharmacy.DrugTerminal temp;
            for( int i = 0 ; i < al.Count ; i++ )
            {
                info = al[ i ] as Neusoft.NFC.Object.NeuObject;
                //ģ����
                this.neuSpread1_Sheet1.Cells[ i , 0 ].Text = info.ID;
                //ģ������
                this.neuSpread1_Sheet1.Cells[ i , 1 ].Text = info.Name;
                //�ն˱���
                this.neuSpread1_Sheet1.Cells[ i , 2 ].Text = info.User01;
                //�ն�����
                temp = this.drugStore.GetDrugTerminalById( info.User01 );
                this.neuSpread1_Sheet1.Cells[ i , 3 ].Text = temp.Name; 
                //�Ƿ�ر� 0 ���� 1 �ر�
                this.neuSpread1_Sheet1.Cells[ i , 4 ].Text = info.User02 == "0" ? "��" : "��";
                //��ע
                this.neuSpread1_Sheet1.Cells[ i , 5 ].Text = info.Memo;
                //�Ƿ����� 1 ���� 0 ��
                this.neuSpread1_Sheet1.Cells[ i , 6 ].Text = "0";				
            }
            Neusoft.NFC.Interface.Classes.Function.HideWaitForm( );
        }

        /// <summary>
        /// �����ն�
        /// </summary>
        private void AddTerminal(  )
        {
            //�жϱ༭Ȩ��
            if( !this.isPrivilegeEdit )
            {
                return;
            }
            if( this.currTerminal == null )
            {
                return;
            }
            if( this.currTemplate == null )
            {
                MessageBox.Show( Language.Msg( "����ѡ��ģ��!" ) );
                return;
            }
            //�ж��Ѿ���ӵ��ն��У��Ƿ��Ѿ������������ӵ������ն�
            int rowCount = this.neuSpread1_Sheet1.Rows.Count;
            if( rowCount > 0 )
            {
                int row = 0;
                int col = 0;
                string find = this.neuSpread1.Search( 0 , this.currTerminal.ID , false , true , false , false , 0 , 0 , ref row , ref col );
                //����Ѿ����ڴ��նˣ�������ӡ�
                if( find == this.currTerminal.ID )
                {
                    MessageBox.Show( Language.Msg( "�նˡ�" ) + this.currTerminal.Name + Language.Msg( "���Ѿ����ڣ������ظ����" ) );
                    return;
                }
            }
            this.neuSpread1_Sheet1.Rows.Add( rowCount , 1 );
            //ģ����
            this.neuSpread1_Sheet1.Cells[ rowCount , 0 ].Text = this.currTemplate.ID;
            //ģ������
            this.neuSpread1_Sheet1.Cells[ rowCount , 1 ].Text = this.currTemplate.Name;
            //�ն˱���
            this.neuSpread1_Sheet1.Cells[ rowCount , 2 ].Text = this.currTerminal.ID;
            //�ն�����
            this.neuSpread1_Sheet1.Cells[ rowCount , 3 ].Text = this.currTerminal.Name;
            //�Ƿ񿪷� 0 ���� 1 �ر�(Ĭ��ѡ���ȫ������)
            this.neuSpread1_Sheet1.Cells[ rowCount , 4 ].Text = "��";//this.currTerminal.IsClose ? "��" : "��";
            //��ע
            this.neuSpread1_Sheet1.Cells[ rowCount , 5 ].Text = this.currTerminal.Memo;
            //�Ƿ����� 1 ���� 0 ��
            this.neuSpread1_Sheet1.Cells[ rowCount , 6 ].Text = "1";
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="flag">��־ɾ����ʽ:
        /// 1 ��ģ���š��ն˱�ŵ���ɾ��
        /// 2 ��ģ����ɾ����ģ��
        /// 3 �Ը�ģ�尴��ҩ̨ɾ��
        /// 4 �Ը�ģ�尴��ҩ��ɾ��
        /// </param>
        private void DeleteTerminal(string flag )
        {
            //�жϱ༭Ȩ��
            if( !this.isPrivilegeEdit )
            {
                return;
            }
            if( this.neuSpread1_Sheet1.Rows.Count == 0 )
            {
                return;
            }

			//��ʾ�û��Ƿ�ȷ��ɾ��
			DialogResult result = MessageBox.Show(Language.Msg("ȷ��ɾ����ǰ��¼?"),"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1,MessageBoxOptions.RightAlign);
			if(result == DialogResult.No) {
				return;
			}
			//�������ݿ�����
			Transaction t = new  Transaction(Connection.Instance);
			t.BeginTransaction();
			
			//ģ�����
            string tempTemplateCode = this.neuSpread1_Sheet1.Cells[ this.neuSpread1_Sheet1.ActiveRowIndex , 0 ].Text;
			//�ն˱���
            string tempTerminalCode = this.neuSpread1_Sheet1.Cells[ this.neuSpread1_Sheet1.ActiveRowIndex , 2 ].Text;

			try{
				this.drugStore.SetTrans(t.Trans);
				int parm;
				switch(flag){
					case "1":		//����ɾ��
						parm = this.drugStore.DeleteDrugOpenTerminalById(tempTemplateCode,tempTerminalCode);
						break;
					case "2":		//��ģ��ɾ��
						parm = this.drugStore.DeleteDrugOpenTerminalByTemplateCode(tempTemplateCode);
						break;
					case "3":		//��ҩ̨
						parm = this.drugStore.DeleteDrugOpenTerminalByType(tempTemplateCode,"1");
						break;	
					default:		//��ҩ��
						parm = this.drugStore.DeleteDrugOpenTerminalByType(tempTemplateCode,"0");
						break;
				}
				if (parm == -1) {
					t.RollBack();
					MessageBox.Show(this.drugStore.Err);
					return;
				}
			}
			catch(Exception ex) {
				t.RollBack();
				MessageBox.Show(ex.Message);
				return;
			}

			t.Commit();
			MessageBox.Show(Language.Msg("ɾ���ɹ�"));

            // ˢ��
			if(flag =="1")
            {
                 this.neuSpread1_Sheet1.Rows.Remove( this.neuSpread1_Sheet1.ActiveRowIndex , 1 );
                 if( this.neuSpread1_Sheet1.Rows.Count == 0 )
                 {
                     this.ShowTemplateList( );
                 }
			}
            else
            {
                this.neuSpread1_Sheet1.Rows.Count = 0;
                this.ShowTemplateList( );
			}
        }

        /// <summary>
        /// �½�ģ��
        /// </summary>
        private void NewTemplate( )
        {
            //�жϱ༭Ȩ��
            if( !this.isPrivilegeEdit )
            {
                return;
            }
            TreeNode node = new TreeNode( );

            node.Text = "�½�ģ��";
            node.Tag = "";
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;

            this.neuTreeView1.Nodes[ 0 ].Nodes.Add( node );
            this.neuTreeView1.SelectedNode = node;

            this.neuTreeView1.LabelEdit = true;
            node.BeginEdit( );
            this.isNew = true;
        }

        /// <summary>
        /// ����Ƿ�Ϸ�
        /// </summary>
        private bool isValid( )
        {
            bool isValid = false;
            for( int i = 0 ; i < this.neuSpread1_Sheet1.Rows.Count ; i++ )
            {
                Neusoft.HISFC.Object.Pharmacy.DrugTerminal info = this.drugStore.GetDrugTerminalById( this.neuSpread1_Sheet1.Cells[ i , 2 ].Text );
                if( info.TerminalProperty == Neusoft.HISFC.Object.Pharmacy.EnumTerminalProperty.��ͨ)
                {
                    isValid = true;
                    break;
                }
            }
            if( !isValid )
            {
                MessageBox.Show(Language.Msg( "��������һ��ģ����ֻά��������ҩ̨ ����������һ��ͨ��ҩ̨" ));
            }
            return isValid;
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void SaveTerminal( )
        {
            //�жϱ༭Ȩ��
            if( !this.isPrivilegeEdit )
            {
                return;
            }
            if( this.neuSpread1_Sheet1.Rows.Count == 0 )
            {
                return;
            }

            if( !this.isValid( ) )
            {
                return;
            }

            //�������ݿ�����
            Transaction t = new Transaction( Connection.Instance );
            t.BeginTransaction( );
            try
            {
                this.drugStore.SetTrans( t.Trans );

                bool isSave = true;
                bool isGetCode = false;
                string tempTemplateCode = "";
                Neusoft.NFC.Object.NeuObject info;
                //��������
                for( int i = 0 ; i < this.neuSpread1_Sheet1.Rows.Count ; i++ )
                {
                    info = new Neusoft.NFC.Object.NeuObject( );
                    info.ID = this.neuSpread1_Sheet1.Cells[ i , 0 ].Text;		//ģ����
                    info.Name = this.neuSpread1_Sheet1.Cells[ i , 1 ].Text;		//ģ������
                    info.User01 = this.neuSpread1_Sheet1.Cells[ i , 2 ].Text;	//�ն˱��
                    info.User02 = this.neuSpread1_Sheet1.Cells[ i , 4 ].Text == "��" ? "1" : "0";	//�Ƿ�ر� 0 ���� 1 �ر�
                    info.Memo = this.neuSpread1_Sheet1.Cells[ i , 5 ].Text;		//��ע
                    info.User03 = this.privDept.ID;

                    //������������ģ���� ֻȡһ��ģ����
                    if( info.ID == "" || info.ID == null )
                    {		
                        if( isGetCode )
                            info.ID = tempTemplateCode;
                        else
                        {
                            info.ID = this.drugStore.GetSequence( "Pharmacy.Constant.GetNewCompanyID" );
                            tempTemplateCode = info.ID;
                            isGetCode = true;
                        }
                    }

                    if( this.drugStore.SetDrugOpenTerminal( info ) == -1 )
                    {	//�Ƚ��и��²������������������
                        isSave = false;
                        t.RollBack( );
                        MessageBox.Show(Language.Msg( "�����") + i.ToString( ) + Language.Msg("��ʱ����\n") + this.drugStore.Err );
                        break;
                    }
                }

                if( isSave )
                {
                    t.Commit( );
                    MessageBox.Show( Language.Msg("����ɹ�" ));
                }
                else
                {
                    return;		
                }
            }
            catch( Exception ex )
            {
                t.RollBack( );
                MessageBox.Show( ex.Message );
                return;
            }
        }

        /// <summary>
        /// ִ��ģ������
        /// </summary>
        private void Exec( )
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
				return;
            }
            if( this.currTemplate == null )
            {
                MessageBox.Show( Language.Msg("����ѡ��ִ��ģ��"));
                return;
            }
			//��ʾ�û��Ƿ�ȷ��
			DialogResult result = MessageBox.Show(Language.Msg("ȷ��ִ�е�ǰ��¼��?"),"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1,MessageBoxOptions.RightAlign);
			if(result == DialogResult.No) 
            {
				return;
			}
			
			//�������ݿ�����
		    Transaction t = new Transaction(Connection.Instance);
			t.BeginTransaction();

			try{
				this.drugStore.SetTrans(t.Trans);

				if (this.drugStore.ExecOpenTerminal(this.privDept.ID,this.currTemplate.ID) == -1) {
					t.RollBack();
					MessageBox.Show(this.drugStore.Err);
					return;
				}
			}
			catch(Exception ex) {
				t.RollBack();
				MessageBox.Show(ex.Message);
				return;
			}

			t.Commit();
			MessageBox.Show(Language.Msg("ִ�гɹ�"));
		}

        /// <summary>
        /// �����նˣ�����Ȩ�޵��û���
        /// </summary>
        private void HideDrugTerminal( )
        {
            this.splitContainer2.Panel1Collapsed = true;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �ն�ѡ���¼�
        /// </summary>
        /// <param name="drugTerminal">ѡ�е��ն�ʵ��</param>
        private void ucDrugTerminalList1_SelectTerminalEvent( Neusoft.HISFC.Object.Pharmacy.DrugTerminal drugTerminal )
        {
            this.currTerminal = drugTerminal;
        }

        /// <summary>
        /// �ն�˫���¼�
        /// </summary>
        /// <param name="drugTerminal">ѡ�е��ն�ʵ��</param>
        private void ucDrugTerminalList1_SelectTerminalDoubleClickedEvent( Neusoft.HISFC.Object.Pharmacy.DrugTerminal drugTerminal )
        {
            currTerminal = drugTerminal;
            this.AddTerminal(  );
        }

        /// <summary>
        /// ��ά���ն�˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick( object sender , FarPoint.Win.Spread.CellClickEventArgs e )
        {
            this.DeleteTerminal( "1" );
        }

        /// <summary>
        /// ģ����ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTreeView1_AfterSelect( object sender , TreeViewEventArgs e )
        {
            if( e.Node.Tag == null )
            {
                return;
            }
            try
            {
                this.currTemplate = new Neusoft.NFC.Object.NeuObject( );
                // ģ�����
                this.currTemplate.ID = e.Node.Tag as string;
                //ģ������
                this.currTemplate.Name = e.Node.Text;
                //��ʾģ���ն�����
                this.ShowTemplateData( );
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message );
                return;
            }
        }

        /// <summary>
        /// ģ�����Ʊ༭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTreeView1_AfterLabelEdit( object sender , NodeLabelEditEventArgs e )
        {
            if( e.Label != null )
            {
                if( e.Label.Length > 0 )
                {
                    if( e.Label.IndexOfAny( new char[ ] { '@' , '.' , ',' , '!' } ) == -1 )
                    {
                        e.Node.EndEdit( false );
                    }
                    else
                    {
                        e.CancelEdit = true;
                        MessageBox.Show(Language.Msg( "������Ч�ַ�!����������" ));
                        e.Node.BeginEdit( );
                    }
                }
                else
                {
                    e.CancelEdit = true;
                    MessageBox.Show( Language.Msg("ģ�����Ʋ���Ϊ��" ));
                    e.Node.BeginEdit( );
                }

                if( !this.isNew )
                {
                    this.neuTreeView1.LabelEdit = false;
                    this.currTemplate.Name = e.Label;
                    for( int i = 0 ; i < this.neuSpread1_Sheet1.Rows.Count ; i++ )
                    {
                        this.neuSpread1_Sheet1.Cells[ i , 1 ].Text = this.currTemplate.Name;
                    }
                    //�������ݿ�
                    this.SaveTerminal( );
                }
                else
                {
                    this.currTemplate.ID = "";
                    this.currTemplate.Name = e.Label;
                    this.isNew = false;
                }
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            //ȡ����ԱȨ�޿��ң���ʱ�����ڿ��Ҵ��� ��
            this.privDept = ( ( Neusoft.HISFC.Object.Base.Employee )this.drugStore.Operator ).Dept;

            //�ж��Ƿ���ģ��ά��Ȩ��
            Neusoft.HISFC.Management.Manager.UserPowerDetailManager user = new Neusoft.HISFC.Management.Manager.UserPowerDetailManager( );
            //ArrayList alPrivDetail = user.QueryUserPriv( this.drugStore.Operator.ID , "0350" , this.privDept.ID );
            //if( alPrivDetail != null )
            //{
            //    foreach( Neusoft.NFC.Object.NeuObject privInfo in alPrivDetail )
            //    {
            //        //�����ն�ά��Ȩ��
            //        if( privInfo.ID == "01" )
            //        {
            //            this.isPrivilegeEdit = true;
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    this.isPrivilegeEdit = true;
            //}
            Neusoft.NFC.Interface.Classes.Function.ShowWaitForm(Language.Msg( "���ڼ��������ն���Ϣ...." ));
            Application.DoEvents( );

            //��ʼ�������ն�����
            this.ucDrugTerminalList1.InitDeptTerminal( this.privDept.ID );
            //��ʼ��ģ���б�
            this.ShowTemplateList( );

            Neusoft.NFC.Interface.Classes.Function.HideWaitForm( );

            base.OnLoad( e );
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <returns></returns>
        protected override int OnSave( object sender , object NeuObject )
        {
            this.SaveTerminal( );
            return base.OnSave( sender , NeuObject );
        }

        #endregion

        #region ��������Ϣ

        ///// <summary>
        ///// �����¼�ί��
        ///// </summary>
        //private event System.EventHandler ToolButtonClicked;
        /// <summary>
        /// ���幤��������
        /// </summary>
        protected Neusoft.NFC.Interface.Forms.ToolBarService toolBarService = new Neusoft.NFC.Interface.Forms.ToolBarService( );

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.NFC.Interface.Forms.ToolBarService OnInit( object sender , object NeuObject , object param )
        {
            //this.ToolButtonClicked += new EventHandler( ToolButton_clicked );
            //���ӹ�����
            this.toolBarService.AddToolButton( "�½�ģ��" , "�½�ģ��" , 0 , true , false , null );
            this.toolBarService.AddToolButton( "ɾ��ģ��" , "ɾ������ģ��" , 1 , true , false , null );
            this.toolBarService.AddToolButton( "ִ��ģ��" , "ִ��ģ������" , 2 , true , false , null );
            this.toolBarService.AddToolButton( "�����ն�" , "�����ն˵�ģ��" , 3 , true , false , null );
            this.toolBarService.AddToolButton( "ɾ���ն�" , "ɾ�������ն�" , 4 , true , false , null );
            this.toolBarService.AddToolButton( "ɾ������" , "ɾ��ģ���еķ�ҩ����" , 5 , true , false , null );
            this.toolBarService.AddToolButton( "ɾ����ҩ̨" , "ɾ��ģ���е���ҩ̨" , 6 , true , false , null );
            return this.toolBarService;
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
                case "�½�ģ��":
                    this.NewTemplate( );
                    break;
                case "ɾ��ģ��":
                    this.DeleteTerminal( "2" );
                    break;
                case "�����ն�":
                    this.AddTerminal( );
                    break;
                case "ɾ���ն�":
                    this.DeleteTerminal( "1" );
                    break;
                case "ִ��ģ��":
                    this.Exec( );
                    break;
                case "ɾ������":
                    this.DeleteTerminal( "4" );
                    break;
                case "ɾ����ҩ̨":
                    this.DeleteTerminal( "3" );
                    break;
            }

        }

        #endregion
     
    }
}
