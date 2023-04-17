using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.BizLogic.Pharmacy;
using FS.HISFC.Models.Pharmacy;
using FS.HISFC.BizLogic.Manager;


namespace FS.HISFC.Components.DrugStore.InBase
{
    /// <summary>
    /// [�ؼ�����:ucDrugControl]<br></br>
    /// [��������: ��ҩ̨����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-13]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDrugControl : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugControl( )
        {
            InitializeComponent( );
            //�¼�
            this.lvDrugControlList.DoubleClick += new System.EventHandler( this.lvDrugControlList_DoubleClick );
            this.lvDrugControlList.SelectedIndexChanged += new EventHandler( this.lvDrugControlList_SelectedIndexChanged );
        }

        #region ����

        //����ҩ��������
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugStore = new FS.HISFC.BizLogic.Pharmacy.DrugStore( );
        //���嵱ǰѡ�����
        private FS.FrameWork.Models.NeuObject currentDept = new FS.FrameWork.Models.NeuObject( );
        //���嵱ǰѡ��İ�ҩ̨��Ϣ
        private DrugControl currentDrugControlInfo = new DrugControl( );

        #endregion

        #region ����


        #region ��ʼ����Ϣ

        /// <summary>
        /// ��ʼ����ҩ̨�б���Ϣ
        /// </summary>
        private void InitDrugControlList( )
        {
            this.lvDrugControlList.SuspendLayout( );
            this.lvDrugControlList.Columns.Clear( );
            this.lvDrugControlList.Items.Clear( );
            this.lvDrugControlList.Columns.Add( "����" , 120 , HorizontalAlignment.Left );
            this.lvDrugControlList.Columns.Add( "��;" , 100 , HorizontalAlignment.Left );
            this.lvDrugControlList.Columns.Add( "��������" , 80 , HorizontalAlignment.Left );
            this.lvDrugControlList.Columns.Add( "��ʾ�ȼ�" , 120 , HorizontalAlignment.Left );

            this.lvDrugControlList.Columns.Add( "�Զ���ӡ" , 80 , HorizontalAlignment.Left );
            this.lvDrugControlList.Columns.Add( "��ҪԤ��" , 80 , HorizontalAlignment.Left );
            this.lvDrugControlList.Columns.Add( "��ӡ�����ǩ" , 120 , HorizontalAlignment.Left );

            this.lvDrugControlList.Columns.Add( "����ҩ��" , 100 , HorizontalAlignment.Left );
            this.lvDrugControlList.Columns.Add( "��ע" , 200 , HorizontalAlignment.Left );
            this.lvDrugControlList.ResumeLayout( );
        }

        /// <summary>
        /// ��ʼ����ҩ̨���ó����б�
        /// </summary>
        private void InitDrugAttribute( )
        {
            if( !this.DesignMode )
            {
                this.cmbUser.AddItems( DrugAttribute.List( ) );
            }
        }

        #endregion

        #region ��ҩ̨������Ϣ

        /// <summary>
        /// ����Ҫ�༭��ҩ̨����ϸ��Ϣ( to tabpage2)
        /// </summary>
        /// <param name="drugControl">��ǰ�༭�İ�ҩ̨</param>
        public void SetDrugControlInfo( DrugControl drugControl )
        {
            this.currentDrugControlInfo = drugControl;
            //��ҩ̨����
            this.txtName.Text = this.currentDrugControlInfo.Name;
            //��ҩ̨����
            this.cmbUser.Tag = this.currentDrugControlInfo.DrugAttribute.ID;
            //��������
            this.cmbSendType.SelectedIndex = this.currentDrugControlInfo.SendType;
            //��ʾ�ȼ�
            this.cmbShowGrade.SelectedIndex = this.currentDrugControlInfo.ShowLevel;

            //�Ƿ��Զ���ӡ��ҩ��
            this.cbxAutoPrint.Checked = this.currentDrugControlInfo.IsAutoPrint;

            //˵��
            this.RtxMark.Text = this.currentDrugControlInfo.Memo;
        }

        /// <summary>
        /// ��ð�ҩ̨�༭��Ϣ(from tabpage2)
        /// </summary>
        /// <returns>��ǰ�༭��ҩ̨</returns>
        public DrugControl GetNewDrugControlInfo( )
        {
            //��ҩ̨����
            this.currentDrugControlInfo.Name = this.txtName.Text;
            //��ҩ̨��;      
            this.currentDrugControlInfo.DrugAttribute.ID = this.cmbUser.Tag;
            //ҽ���������ͣ�1���з��ͣ�2��ʱ���ͣ�
            this.currentDrugControlInfo.SendType = this.cmbSendType.SelectedIndex;
            //��ʾ�ȼ���0��ʾ���һ��ܣ�1��ʾ������ϸ��2��ʾ������ϸ     
            this.currentDrugControlInfo.ShowLevel = this.cmbShowGrade.SelectedIndex;

            //���ΰ�ҩ���Զ���ӡ����
            ////�Ƿ��Զ���ӡ��ҩ��
            //this.currentDrugControlInfo.IsAutoPrint = this.cbxAutoPrint.Checked ;
            this.currentDrugControlInfo.IsAutoPrint = false;


            //��ҩ����      
            this.currentDrugControlInfo.Dept = this.currentDept;  
            //��ע
            this.currentDrugControlInfo.Memo = this.RtxMark.Text; 

            return currentDrugControlInfo;
        }
        /// <summary>
        /// ȡ�ð�ҩ̨��ʾ��������
        /// 
        /// {AB3B4EEB-A1C5-4a37-AD42-4EF66DF8F859}  ���Ӷ�����ʾ��ʽ
        /// </summary>
        /// <param name="showType"></param>
        private string GetShowTypeName( int showType )
        {
            string showTypeName;
            switch( showType )
            {
                case 0:
                    showTypeName = "��ʾ���һ���";
                    break;
                case 1:
                    showTypeName = "��ʾ������ϸ";
                    break;
                case 2:
                    showTypeName = "��ʾ������ϸ(��ҩ������)";
                    break;
                case 3:
                    showTypeName = "��ʾ������ϸ(��������)";
                    break;
                default:
                    showTypeName = "��ʾ������ϸ(��ҩ������)";
                    break;
            }
            return showTypeName;
        }
        /// <summary>
        /// ���ҩ̨�б��в����½ڵ�
        /// </summary>
        /// <param name="drugControl">��ҩ̨��Ϣ</param>
        /// <returns>����Ľڵ���Ϣ</returns>
        private ListViewItem AddDrugControlToListView( DrugControl drugControl )
        {
            //���ò���Ľڵ���Ϣ
            ListViewItem lvi = new ListViewItem( );
            //���������Ӻ��ѱ����ͼ��
            if( drugControl.ID != "" )
            {
                lvi.ImageIndex = 0;
            }
            else
            {
                lvi.ImageIndex = 1;
            }
            lvi.Text = drugControl.Name;
            //Tag���Ա����ҩ������ʵ��
            lvi.Tag = drugControl;
            //����listView���ӽڵ�

            //��;
            lvi.SubItems.Add( drugControl.DrugAttribute.Name );
            //��������
            lvi.SubItems.Add( drugControl.SendType == 0 ? "ȫ��" : ( drugControl.SendType == 1 ? "����" : "��ʱ" ) );
            //��ʾ�ȼ�
            lvi.SubItems.Add( this.GetShowTypeName(drugControl.ShowLevel) );

            //�Ƿ��Զ���ӡ��ҩ��
            lvi.SubItems.Add( drugControl.IsAutoPrint ? "��" : "��" );
            //��ҩ���Ƿ���ҪԤ�� ��ӡ�����ǩʱ���ֶ���Ч
            lvi.SubItems.Add( drugControl.IsBillPreview ? "��" : "��" );
            //�Ƿ��ӡ�����ǩ �ò���ֻ�Գ�Ժ��ҩ��ҩ��Ч
            lvi.SubItems.Add( drugControl.IsPrintLabel ? "��" : "��" );

            //����ҩ��
            lvi.SubItems.Add( this.currentDept.Name );
            //��ע
            lvi.SubItems.Add( drugControl.Memo );
            //���ز���Ľڵ�
            return this.lvDrugControlList.Items.Add( lvi );
        }

        /// <summary>
        /// ����ȫ����ҩ��Ϊδѡ��״̬
        /// </summary>
        private void ResetDrugBill( )
        {
            if( this.lvPutDrugBill1.Items.Count > 0 )
            {
                foreach( ListViewItem lvi in this.lvPutDrugBill1.Items )
                {
                    lvi.Checked = false;
                }
            }
        }

        /// <summary>
        /// ��ʾ��ǰ��ҩ̨�е���ϸ�б�
        /// </summary>
        private void ShowBillListByDrugControl(  )
        {
            this.lvPutDrugBill1.BeginUpdate( );
            this.ResetDrugBill( );
            //ȡ�˰�ҩ̨��Ӧ�İ�ҩ����ϸ�б�
            ArrayList al = this.drugStore.QueryDrugControlDetailList( currentDrugControlInfo.ID );
            if( al == null )
            {
                MessageBox.Show( this.drugStore.Err );
                return;
            }
            //�ڰ�ҩ�������б�����ʾ��ǰ��ҩ̨����ϸ����
            DrugBillClass billClass;
            foreach( DrugBillClass info in al )
            {
                //ȡ��ҩ̨��ϸ�е�ÿһ�����ݣ���ListView�в�����ͬ����Ŀ������checked������Ϊtrue
                foreach( ListViewItem lvi in this.lvPutDrugBill1.Items )
                {
                    billClass = lvi.Tag as DrugBillClass;
                    if( billClass.ID == info.ID )
                    {
                        lvi.Checked = true;
                    }
                }
            }
            this.lvPutDrugBill1.EndUpdate( );

        }

        /// <summary>
        /// ��������ʾ��ҩ̨��Ϣ
        /// </summary>
        /// <param name="dept">������Ϣ</param>
        /// <returns></returns>
        private void ShowDrugControlByDept( )
        {
            this.lvDrugControlList.Items.Clear( );

            //���³�ʼ����ҩ����Ϣ
            this.ResetDrugBill( );
            if( currentDept.ID  == "" )
            {
                return ;
            }
            //ȡ������ȫ����ҩ̨�б�
            ArrayList al = this.drugStore.QueryDrugControlList( currentDept.ID );
            if( al == null )
            {
                MessageBox.Show( this.drugStore.Err );
                return ;
            }
            //��ӵ�����ҩ̨�б�
            foreach( DrugControl controlInfo in al )
            {
                this.AddDrugControlToListView( controlInfo );
            }
        }

        #endregion

        #region ���ݲ���

        /// <summary>
        /// ���Ӱ�ҩ̨
        /// </summary>
        private void AddDrugControl( )
        {
            if( currentDept.ID == "" )
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ�����Ŀ���" ) );
                return;
            }

            if (this.neuTabControl1.TabPages.ContainsKey(this.tabPage2.Name))
            {
                this.neuTabControl1.TabPages.Remove(this.tabPage2);
            }

           //�����ҩ̨ѡ״̬
            //�����еķǵ�ǰ��ҩ̨Ϊδѡ��״̬
            foreach( ListViewItem L in this.lvDrugControlList.Items )
            {
                L.Selected = false;
                L.Checked = false;
            }
            //�����ҩ��ѡ��״̬
            this.ResetDrugBill();
            //����Ҫ����Ľڵ�
            DrugControl newInfo = new DrugControl( );
            newInfo.Name = "�½���ҩ̨";

            //��ListView�в����½ڵ�			
            ListViewItem lvi = this.AddDrugControlToListView( newInfo );
            //ѡ�������ӵĽڵ�
            lvi.Selected = true;
            lvi.Checked = true;

            //�ڱ༭��Ϣ����ʾ�����ӵİ�ҩ̨���л���tabpage2
            this.SetDrugControlInfo( newInfo );
            this.neuTabControl1.TabPages.Add( this.tabPage2 );
            this.neuTabControl1.SelectedIndex = 1;
        }


        /// <summary>
        /// ɾ��������ҩ̨
        /// </summary>
        private void DeleteDrugControl( )
        {
            //�ж��Ƿ�ѡ��һ����ҩ̨
            if( this.lvDrugControlList.SelectedItems.Count > 0 )
            {
                //��ȡ��ǰ��ҩ̨��Ϣ
                this.GetNewDrugControlInfo( );
            }
            else
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ����Ҫɾ���İ�ҩ̨��") );
                return;
            }

            if( this.currentDrugControlInfo.ID != "" )
            {
                DialogResult result = MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ȷ��Ҫɾ����" + this.currentDrugControlInfo.Name + "����ҩ̨��" ), FS.FrameWork.Management.Language.Msg( "ɾ����ʾ") , System.Windows.Forms.MessageBoxButtons.OKCancel );
                if( result == DialogResult.Cancel ) return;

                int parm;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction( FS.FrameWork.Management.Connection.Instance );

                this.drugStore.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //ɾ���ɰ�ҩ̨��ҩ��ϸ�е���������
                parm = this.drugStore.DeleteDrugControl( this.currentDrugControlInfo.ID );

                if( parm == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( this.drugStore.Err ,FS.FrameWork.Management.Language.Msg( "��ʾ"));
                    return;
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }

            //ɾ��Listview�еĽڵ�
            this.lvDrugControlList.SelectedItems[ 0 ].Remove( );

            //���ListView���нڵ㣬��ѡ�е�һ��
            if( this.lvDrugControlList.Items.Count > 0 )
            {
                this.lvDrugControlList.Items[ 0 ].Selected = true;
            }
            this.neuTabControl1.SelectedIndex = 0;
        }

        /// <summary>
        /// ������Ч�Բ�ѯ
        /// </summary>
        /// <returns>�ɹ�����True  ʧ�ܷ���False</returns>
        private bool IsSaveDrugControlValid(DrugControl drugControl)
        {
            foreach (ListViewItem lv in this.lvDrugControlList.Items)
            {
                DrugControl tempDrugControl = lv.Tag as DrugControl;

                if (tempDrugControl != null && tempDrugControl.ID != drugControl.ID && tempDrugControl.Name == drugControl.Name)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(drugControl.Name + " ��ҩ̨�����ظ�"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// �����ҩ̨��Ϣ
        /// </summary>
        private void SaveDrugControl( )
        {
            if( currentDept.ID == "" )
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ�����Ŀ���" ) );
                return;
            }
            //�ж��Ƿ�ѡ���ҩ��
            if( this.lvPutDrugBill1.CheckedItems.Count == 0 )
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��Ϊ��ҩ̨��Ӱ�ҩ��!" ));
                return;
            }

            //�ж��Ƿ�ѡ��һ����ҩ̨
            if( this.lvDrugControlList.SelectedItems.Count > 0 )
            {
                //��ȡ��ǰ��ҩ̨���µı༭��ϢmyDrugControlInfo
                this.GetNewDrugControlInfo( );
            }
            else
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ��һ����ҩ̨��" ));
                return;
            }

            if (!this.IsSaveDrugControlValid(this.currentDrugControlInfo))
            {
                return;
            }

            if (this.currentDrugControlInfo.Name.Length > 16)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩ̨���Ƴ���,���ʵ�����"));
                return;
            }
            if (this.currentDrugControlInfo.Memo.Length > 30)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ע����,���ʵ�����"));
                return;
            }

            //����������ӵİ�ҩ̨����ȡ�˰�ҩ̨����ˮ��
            if( currentDrugControlInfo.ID == "" )
            {
                currentDrugControlInfo.ID = this.drugStore.GetDrugControlNO( );
                if( currentDrugControlInfo.ID == "-1" )
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ȡ��ҩ̨��ˮ��ʱʧ��:") + this.drugStore.Err );
                    return;
                }
            }

            int parm;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction( FS.FrameWork.Management.Connection.Instance );
            //t.BeginTransaction( );

            drugStore.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                //��ɾ���ɰ�ҩ̨��ҩ��ϸ�е��������ݣ�Ȼ������µ����ݡ�
                parm = drugStore.DeleteDrugControl( currentDrugControlInfo.ID );
                if( parm == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( this.drugStore.Err );
                    return;
                }
                else
                {
                    //�����ҩ̨����
                    foreach( ListViewItem lvi in this.lvPutDrugBill1.CheckedItems )
                    {
                        DrugBillClass info = lvi.Tag as DrugBillClass;
                        //Ϊ��ҩ̨��ϸ��ֵ
                        currentDrugControlInfo.DrugBillClass.ID = info.ID;
                        currentDrugControlInfo.DrugBillClass.Name = info.Name;

                        //�����ҩ̨��ϸ����
                        parm = this.drugStore.InsertDrugControl( currentDrugControlInfo );
                        if( parm != 1 )
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show( this.drugStore.Err );
                            return;
                        }
                    }
                }
                //�ύ���ݿ�
                FS.FrameWork.Management.PublicTrans.Commit();
                this.neuTabControl1.SelectedIndex = 0;
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ҩ̨���óɹ���" ));
            }
            catch( Exception ex )
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�����ҩ̨����ʱ����" + ex.Message ));
                return;
            }

            //����listView�Ͻڵ�
            this.lvDrugControlList.SelectedItems[ 0 ].Tag = currentDrugControlInfo;
            this.lvDrugControlList.SelectedItems[ 0 ].Text = currentDrugControlInfo.Name;
            //���������Ӻ��ѱ����ͼ��
            if( currentDrugControlInfo.ID != "" )
            {
                this.lvDrugControlList.SelectedItems[ 0 ].ImageIndex = 0;
            }
            else
            {
                this.lvDrugControlList.SelectedItems[ 0 ].ImageIndex = 1;
            }
            //����listView���ӽڵ�
            
            this.lvDrugControlList.SelectedItems[ 0 ].SubItems[ 1 ].Text = currentDrugControlInfo.DrugAttribute.Name;
            this.lvDrugControlList.SelectedItems[ 0 ].SubItems[ 2 ].Text = currentDrugControlInfo.SendType == 0 ? "ȫ��" : ( currentDrugControlInfo.SendType == 1 ? "����" : "��ʱ" );

            //{9738B143-F85C-4255-8C5B-FD3EA729FE32}
            //currentDrugControlInfo.SendType ��Ϊ currentDrugControlInfo.ShowLevel
            this.lvDrugControlList.SelectedItems[ 0 ].SubItems[ 3 ].Text = this.GetShowTypeName( currentDrugControlInfo.ShowLevel );

            //this.lvDrugControlList.SelectedItems[ 0 ].SubItems[ 4 ].Text = this.currentDept.Name;
            //this.lvDrugControlList.SelectedItems[ 0 ].SubItems[ 5 ].Text = currentDrugControlInfo.Memo;

            this.lvDrugControlList.SelectedItems[0].SubItems[4].Text = currentDrugControlInfo.IsAutoPrint ? "��" : "��";
            this.lvDrugControlList.SelectedItems[0].SubItems[5].Text = currentDrugControlInfo.IsBillPreview ? "��" : "��";
            this.lvDrugControlList.SelectedItems[0].SubItems[6].Text = currentDrugControlInfo.IsPrintLabel ? "��" : "��";

            this.lvDrugControlList.SelectedItems[0].SubItems[8].Text = currentDrugControlInfo.Memo;
        }
        #endregion

        #endregion

        #region �¼�
     
        /// <summary>
        /// ѡ�����ʱ����������ҿؼ�ͨ�ţ�
        /// </summary>
        /// <param name="neuObject">�ؼ�����</param>
        /// <param name="e">��ǰѡ�е����ڵ���Ϣ</param>
        /// <returns></returns>
        protected override int OnSetValue( object neuObject , TreeNode e )
        {
            try
            {
                if( e != null )
                {
                    if( e.Level == 1 )
                    {
                        //��ǰѡ��Ŀ���
                        this.currentDept = e.Tag as FS.FrameWork.Models.NeuObject;
                        //��ʾ��ǰ���ҵİ�ҩ̨��Ϣ
                        this.ShowDrugControlByDept( );
                    }
                    else
                    {
                        this.currentDept = new FS.FrameWork.Models.NeuObject( );

                        if (this.isLoad)
                        {
                            this.isLoad = false;
                        }
                        else
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ�����Ŀ���"), FS.FrameWork.Management.Language.Msg("��ʾ"));
                        }
                    }
                }
                else
                {
                    this.currentDept = new FS.FrameWork.Models.NeuObject( );
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message ,FS.FrameWork.Management.Language.Msg( "��ʾ"));
            }
            return base.OnSetValue( neuObject , e );
        }
        /// <summary>
        /// ��ҩ̨���˫�������ҩ̨�༭״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvDrugControlList_DoubleClick( object sender , EventArgs e )
        {
            if( this.lvDrugControlList.SelectedItems.Count > 0 )
            {
                //��ʾ��ҩ̨�༭��Ϣ
                this.neuTabControl1.TabPages.Add( this.tabPage2 );
                this.SetDrugControlInfo( this.lvDrugControlList.SelectedItems[ 0 ].Tag as DrugControl );
                this.neuTabControl1.SelectedIndex = 1;
            }
            else
            {
                //�����ð�ҩ��Ϊδѡ��״̬
                this.ResetDrugBill();
                this.currentDrugControlInfo = new DrugControl( );
            }
        }

        /// <summary>
        /// ��ҩ̨ѡ��ı�ʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvDrugControlList_SelectedIndexChanged( object sender , EventArgs e )
        {
            if( this.lvDrugControlList.SelectedItems.Count > 0 )
            {
                //���õ�ǰ��ҩ̨��Ϣ
                this.SetDrugControlInfo( this.lvDrugControlList.SelectedItems[ 0 ].Tag as DrugControl );
                //��ʾ��ҩ̨��Ӧ�İ�ҩ����Ϣ
                this.ShowBillListByDrugControl(  );
            }
            else
            {
                //�����ð�ҩ��Ϊδѡ��״̬
                this.ResetDrugBill( );
                this.currentDrugControlInfo = new DrugControl( );
            }
        }
        /// <summary>
        /// tab�л��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl1_SelectedIndexChanged( object sender , EventArgs e )
        {
            if( this.neuTabControl1.SelectedIndex == 0 )
            {
                this.neuTabControl1.TabPages.Remove( this.tabPage2 );
            }
        }
        /// <summary>
        /// ��ҩ̨���Ը����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUser_SelectedIndexChanged( object sender , EventArgs e )
        {

        }
        /// <summary>
        /// ��ҩ���Ƿ���ҪԤ��ѡ��״̬�ı��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxPrintClinicLabel_CheckedChanged( object sender , EventArgs e )
        {

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
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService( );

        private bool isLoad = false;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            this.isLoad = true;

            //��ʼ����ҩ̨�б���Ϣ
            this.InitDrugControlList( );
            //��ʼ����ҩ̨������Ϣ
            this.InitDrugAttribute( );
            //����tabpage2
            this.neuTabControl1.TabPages.Remove( this.tabPage2 );
            base.OnLoad( e );
        }

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit( object sender , object neuObject , object param )
        {
            //this.ToolButtonClicked += new EventHandler( ToolButton_clicked );
            //���ӹ�����
            this.toolBarService.AddToolButton( "����" , "���Ӱ�ҩ̨" , FS.FrameWork.WinForms.Classes.EnumImageList.T��� , true , false ,null );
            this.toolBarService.AddToolButton( "ɾ��" , "ɾ����ҩ̨" , FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ�� , true , false , null );
            return this.toolBarService;
        }

        /// <summary>
        /// �����ҩ̨����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave( object sender , object neuObject )
        {
            //����
            this.SaveDrugControl( );
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
                    this.AddDrugControl( );
                    break;
                case "ɾ��":
                    this.DeleteDrugControl( );
                    break;
            }
           
        }
        #endregion

 
    }
}
